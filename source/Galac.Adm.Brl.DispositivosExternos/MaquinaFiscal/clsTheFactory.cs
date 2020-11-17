using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using LibGalac.Aos.Base;
using System.ComponentModel;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.Brl;
using System.IO;
using TfhkaNet.IF;
using TfhkaNet.IF.VE;
using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal {
    public class clsTheFactory : IImpresoraFiscalPdn {
        #region variables
        private string CommPort = "";
        private Tfhka TfhkPrinter = new Tfhka();
        private eImpresoraFiscal vModeloFactory;
        S1PrinterData mEstado;
        string _rutaStatusPrinter="";
        #endregion

        public clsTheFactory(XElement valXmlDatosImpresora) {
            ConfigurarImpresora(valXmlDatosImpresora);
        }

        private void ConfigurarImpresora(XElement valXmlDatosImpresora) {
            try {                
                CommPort = "COM" + clsImpresoraFiscalUtil.ObtenerValorDesdeXML(valXmlDatosImpresora, "PuertoMaquinaFiscal");
                vModeloFactory = (eImpresoraFiscal)LibConvert.DbValueToEnum(clsImpresoraFiscalUtil.ObtenerValorDesdeXML(valXmlDatosImpresora, "ModeloDeMaquinaFiscal"));
                LeerEstadoDeImpresora();
            } catch (GalacException vEx) {
                throw vEx;
            }

        }

        private string ObtenerRutaData() {
            try {
                string vresult = "";
                vresult = LibDefGen.DataPathUser("") + "\\GetData.DAT";
                return vresult;
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Ruta de Data", eExceptionManagementType.Controlled);
            }
        }

        public bool AbrirConexion() {
            bool vResult = false;
            try {
                if (!TfhkPrinter.StatusPort) {
                    vResult = TfhkPrinter.OpenFpCtrl(CommPort);
                    vResult = vResult && TfhkPrinter.CheckFPrinter();
                } else {
                    vResult = true;
                }
                return vResult;
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Abrir Conexión", eExceptionManagementType.Controlled);
            }
        }

        public bool CerrarConexion() {
            bool vResult = false;
            try {
                TfhkPrinter.CloseFpCtrl();
                Thread.Sleep(250);
                vResult = true;
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Abrir Conexión", eExceptionManagementType.Controlled);
            }
            return vResult;
        }

        public bool ComprobarConexion() {
            bool vResult = false;
            Thread.Sleep(250);
            vResult = TfhkPrinter.StatusPort;
            return vResult;
        }

        public string ObtenerSerial() {
            string vSerial = "";

            if (ComprobarConexion()) {
                vSerial = mEstado.RegisteredMachineNumber;
            }
            return vSerial;
        }

        public string ObtenerFechaYHora() {
            string vResult = "";
            string vTiempo = "";
            string vFecha = "";

            try {
                DateTime vDateTime = mEstado.CurrentPrinterDateTime;
                vFecha = LibConvert.ToStr(vDateTime, "dd/MM/yy");
                vTiempo = LibConvert.ToStrOnlyForHour(vDateTime, "hh:mm");
                vResult = vFecha + LibText.Space(1) + vTiempo;
                return vResult;
            } catch (GalacException vEx) {
                CerrarConexion();
                throw new LibGalac.Aos.Catching.GalacException("Obtener Fecha y Hora" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public eStatusImpresorasFiscales EstadoDelPapel(bool valAbrirPuerto) {
            eStatusImpresorasFiscales vResult;
            try {
                if (valAbrirPuerto) {
                    AbrirConexion();
                }
                Thread.Sleep(150);
                PrinterStatus ifEstatus = TfhkPrinter.GetPrinterStatus();
                vResult = (eStatusImpresorasFiscales)ifEstatus.PrinterErrorCode;
                if (valAbrirPuerto) {
                    CerrarConexion();
                }
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Estado del Papel ", eExceptionManagementType.Controlled);
            }
            return vResult;
        }

        public bool RealizarReporteZ() {
            bool vResult = false;
            try {
                if (AbrirConexion()) {
                    TfhkPrinter.PrintZReport();
                    CerrarConexion();
                }
                return vResult;
            } catch (GalacException vEx) {
                CerrarConexion();
                throw new LibGalac.Aos.Catching.GalacException("Realizar Reporte Z", eExceptionManagementType.Controlled);
            }
        }

        public bool RealizarReporteX() {
            bool vResult = false;
            try {
                if (AbrirConexion()) {
                    TfhkPrinter.PrintXReport();
                    CerrarConexion();
                }
                return vResult;
            } catch (GalacException vEx) {
                CerrarConexion();
                throw new LibGalac.Aos.Catching.GalacException("Realizar Reporte X", eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerUltimoNumeroFactura() {
            string vUltimaFactura = "";
            string vCmd = "S1";
            string vRuta = _rutaStatusPrinter;
            if (ComprobarConexion()) {
                TfhkPrinter.UploadStatusCmd(vCmd, vRuta);
                mEstado = TfhkPrinter.GetS1PrinterData();
                vUltimaFactura = LibConvert.ToStr(mEstado.LastInvoiceNumber);                
            } else {
                throw new LibGalac.Aos.Catching.GalacException("Obtener Ultimo Numero de Comprobante Fiscal", eExceptionManagementType.Controlled);
            }
            return vUltimaFactura;
        }

        public string ObtenerUltimoNumeroNotaDeCredito() {           
            string vCmd = "S1";
            string vRuta = _rutaStatusPrinter;
            string vUltimoNotaCredito = "";
            if (ComprobarConexion()) {
                TfhkPrinter.UploadStatusCmd(vCmd, vRuta);
                mEstado = TfhkPrinter.GetS1PrinterData();
                vUltimoNotaCredito = LibConvert.ToStr(mEstado.LastCreditNoteNumber);
            }
            return vUltimoNotaCredito;
        }

        public string ObtenerUltimoNumeroReporteZ() {
            string vUltimoNumero = "";
            ReportData estado;
            string vRuta = _rutaStatusPrinter;

            if (AbrirConexion()) {
                string vCmd = "";
                TfhkPrinter.UploadReportCmd(vCmd, vRuta);
                estado = TfhkPrinter.GetZReport();
                vUltimoNumero = LibConvert.ToStr(estado.NumberOfLastZReport);
                CerrarConexion();
            } else {
                throw new LibGalac.Aos.Catching.GalacException("Obtener Ultimo Reporte Z", eExceptionManagementType.Controlled);
            }
            return vUltimoNumero;
        }

        public bool CancelarDocumentoFiscalEnImpresion(bool valAbrirConexion) {
            bool vResult = false;
            try {
                if (valAbrirConexion) {
                    AbrirConexion();
                }
                string vCMD = "7";
                vResult = TfhkPrinter.SendCmd(vCMD);
                return vResult;
            } catch (Exception vEx) {
                throw new GalacException("Cancelar Documento Fiscal en Impresion", eExceptionManagementType.Controlled);
            }
        }

        private void ImprimeSerialyRollo(string valSerial, string valRollo) {
            string vCmd;
            try {
                if (!valSerial.Equals("")) {
                    vCmd = LibText.Left("@" + valSerial, 40);
                    TfhkPrinter.SendCmd(vCmd);
                }

                if (!valRollo.Equals("")) {
                    vCmd = LibText.Left("@" + valRollo, 40);
                    TfhkPrinter.SendCmd(vCmd);
                }
            } catch (PrinterException pex) {
                throw new LibGalac.Aos.Catching.GalacException("Imprimir Serial y Rollo", LibGalac.Aos.Catching.eExceptionManagementType.Uncontrolled);
            }
        }

        private string DarFormatoAAlicuotaIva(eTipoDeAlicuota valTipoAlicuota) {
            string vValorFinal = "";

            switch (valTipoAlicuota) {
                case eTipoDeAlicuota.Exento:
                    vValorFinal = LibText.Space(1);
                    break;
                case eTipoDeAlicuota.AlicuotaGeneral:
                    vValorFinal = "!";
                    break;
                case eTipoDeAlicuota.Alicuota2:
                    vValorFinal = "\"";
                    break;
                case eTipoDeAlicuota.Alicuota3:
                    vValorFinal = "#";
                    break;
            }
            return vValorFinal;
        }

        private string SetDecimalSeparator(string valNumero) {
            string vResult = "";
            string TokenSeparator = "";

            TokenSeparator = LibConvert.CurrentDecimalSeparator();
            if (LibText.S1IsInS2(".", valNumero) && TokenSeparator.Equals(",")) {
                vResult = LibText.Replace(valNumero, ".", ",");
            } else if (LibText.S1IsInS2(",", valNumero) && TokenSeparator.Equals(".")) {
                vResult = LibText.Replace(valNumero, ",", ".");
            } else {
                vResult = valNumero;
            }
            return vResult;
        }

        private string DarFormatoACantidadParaImpresion(string valNumero) {
            string vValorFinal = "";
            string vParteEntera = "";
            string vParteDecimal = "";
            int TokenPosition = 0;

            valNumero = SetDecimalSeparator(valNumero);
            TokenPosition = LibText.InStr(valNumero, LibConvert.CurrentDecimalSeparator());
            if (TokenPosition > 0) {
                vParteEntera = LibText.Left(valNumero, TokenPosition);
                vParteEntera = LibText.FillWithCharToLeft(vParteEntera, "0", 5);
                vParteDecimal = LibText.Right(valNumero, LibText.Len(valNumero) - TokenPosition - 1);
                vParteDecimal = LibText.FillWithCharToRight(vParteDecimal, "0", 3);
                vValorFinal = vParteEntera + vParteDecimal;
            } else {
                vValorFinal = LibText.FillWithCharToLeft(valNumero, "0", 5) + new String('0', 3);
            }
            vValorFinal = LibText.Left(vValorFinal, 8);
            return vValorFinal;
        }

        private string DarFormatoAPrecioParaImpresion(string valNumero) {
            string vValorFinal = "";
            string vParteEntera = "";
            string vParteDecimal = "";
            int TokenPosition = 0;

            valNumero = SetDecimalSeparator(valNumero);
            TokenPosition = LibText.InStr(valNumero, LibConvert.CurrentDecimalSeparator());
            if (TokenPosition > 0) {
                vParteEntera = LibText.Left(valNumero, TokenPosition);
                vParteEntera = LibText.FillWithCharToLeft(vParteEntera, "0", 8);
                vParteDecimal = LibText.Right(valNumero, LibText.Len(valNumero) - TokenPosition - 1);
                vParteDecimal = LibText.FillWithCharToRight(vParteDecimal, "0", 2);
                vValorFinal = vParteEntera + vParteDecimal;
            } else {
                vValorFinal = LibText.FillWithCharToLeft(valNumero, "0", 8) + new String('0', 2);
            }
            vValorFinal = LibText.Left(vValorFinal, 10);
            return vValorFinal;
        }


        private string DarFormatoADescuentoParaImpresion(string valNumero) {
            string vValorFinal = "";
            string vParteEntera = "";
            string vParteDecimal = "";
            int TokenPosition = 0;

            valNumero = SetDecimalSeparator(valNumero);
            TokenPosition = LibText.InStr(valNumero, LibConvert.CurrentDecimalSeparator());
            if (TokenPosition > 0) {
                vParteEntera = LibText.Left(valNumero, TokenPosition);
                vParteEntera = LibText.FillWithCharToLeft(vParteEntera, "0", 2);
                vParteDecimal = LibText.Right(valNumero, LibText.Len(valNumero) - TokenPosition - 1);
                vParteDecimal = LibText.FillWithCharToRight(vParteDecimal, "0", 2);
                vValorFinal = vParteEntera + vParteDecimal;
            } else {
                vValorFinal = LibText.FillWithCharToLeft(valNumero, "0", 2) + new String('0', 2);
            }
            vValorFinal = LibText.Left(vValorFinal, 4);
            return vValorFinal;
        }

        private string DarFormatoAMontoTotalParaImpresion(string valNumero) {
            string vValorFinal = "";
            string vParteEntera = "";
            string vParteDecimal = "";
            int TokenPosition = 0;

            valNumero = SetDecimalSeparator(valNumero);
            TokenPosition = LibText.InStr(valNumero, LibConvert.CurrentDecimalSeparator());
            if (TokenPosition > 0) {
                vParteEntera = LibText.Left(valNumero, TokenPosition);
                vParteEntera = LibText.FillWithCharToLeft(vParteEntera, "0", 10);
                vParteDecimal = LibText.Right(valNumero, LibText.Len(valNumero) - TokenPosition - 1);
                vParteDecimal = LibText.FillWithCharToRight(vParteDecimal, "0", 2);
                vValorFinal = vParteEntera + vParteDecimal;
            } else {
                vValorFinal = LibText.FillWithCharToLeft(valNumero, "0", 10) + new String('0', 2);
            }
            vValorFinal = LibText.Left(vValorFinal, 12);
            return vValorFinal;
        }

        public bool ImprimirNotaCredito(XElement valDocumentoFiscal) {
            string vDireccion = LibXml.GetPropertyString(valDocumentoFiscal, "DireccionCliente");
            string vRif = LibXml.GetPropertyString(valDocumentoFiscal, "NumeroRIF");
            string vRazonSocial = LibXml.GetPropertyString(valDocumentoFiscal, "NombreCliente");
            string vTelefono = LibXml.GetPropertyString(valDocumentoFiscal, "TelefonoCliente");
            string vObservaciones = LibXml.GetPropertyString(valDocumentoFiscal, "Observaciones");
            string vFacturaAfectada = LibXml.GetPropertyString(valDocumentoFiscal, "NumeroComprobanteFiscal");
            string vSerialMaquina = LibXml.GetPropertyString(valDocumentoFiscal, "SerialMaquinaFiscal");
            string vFecha = LibXml.GetPropertyString(valDocumentoFiscal, "FechaDeFacturaAfectada");
            bool vResult = false;
            string vTipoFactura = "D";

            try {
                if (ComprobarConexion()) {
                    if (AbrirComprobanteFiscal(vRazonSocial, vRif, vDireccion, vTelefono, vObservaciones, vTipoFactura, vFacturaAfectada, vSerialMaquina, vFecha)) {
                        vResult = ImprimirTodosLosArticulos(valDocumentoFiscal, true);
                        vResult &= EnviarPagos(valDocumentoFiscal);
                        CerrarConexion();
                    }
                }
                ImprimirTodosLosArticulos(valDocumentoFiscal, true);
                Thread.Sleep(800);
                vResult = true;
            } catch (Exception vEx) {
                vResult = false;
            }
            return vResult;
        }

        public bool ImprimirFacturaFiscal(XElement valDocumentoFiscal) {

            string vDireccion = LibXml.GetPropertyString(valDocumentoFiscal, "DireccionCliente");
            string vRif = LibXml.GetPropertyString(valDocumentoFiscal, "NumeroRIF");
            string vRazonSocial = LibXml.GetPropertyString(valDocumentoFiscal, "NombreCliente");
            string vTelefono = LibXml.GetPropertyString(valDocumentoFiscal, "TelefonoCliente");
            string vObservaciones = LibXml.GetPropertyString(valDocumentoFiscal, "Observaciones");
            bool vResult = false;
            try {
                if (ComprobarConexion()) {
                    if (AbrirComprobanteFiscal(vRazonSocial, vRif, vDireccion, vTelefono, vObservaciones)) {
                        vResult = ImprimirTodosLosArticulos(valDocumentoFiscal, false);
                        vResult &= EnviarPagos(valDocumentoFiscal);
                    }
                }
            } catch (Exception vEx) {
                vResult = false;
            }
            return vResult;
        }

        private bool ImprimirTodosLosArticulos(XElement valDocumentoFiscal, bool valIsNotaDeCredito) {
            bool vEstatus = false;
            string vDescripcion;
            string vCantidad;
            string vMonto;
            string vTipoTasa;
            string vPrcDescuento;
            string vPrcDescuentoTotal = "";
            bool vSerialyRolloLuegoDescripcion = false;
            string vSerial;
            string vRollo;
            bool vImprimeCamposDefinibles = false;
            string vCmd = "";
            eStatusImpresorasFiscales PrintStatus;

            try {
                List<XElement> vRecord = valDocumentoFiscal.Descendants("GpResultDetailRenglonFactura").ToList();
                vSerialyRolloLuegoDescripcion = false;
                vImprimeCamposDefinibles = false;
                foreach (XElement vXElement in vRecord) {
                    PrintStatus = EstadoDelPapel(false);
                    if (PrintStatus.Equals(eStatusImpresorasFiscales.eListoEnEspera) || PrintStatus.Equals(eStatusImpresorasFiscales.ePocoPapel)) {
                        vDescripcion = LibXml.GetElementValueOrEmpty(vXElement, "Descripcion");
                        vCantidad = LibXml.GetElementValueOrEmpty(vXElement, "Cantidad");
                        vCantidad = DarFormatoACantidadParaImpresion(vCantidad);
                        vMonto = LibXml.GetElementValueOrEmpty(vXElement, "PrecioSinIVA");
                        vMonto = DarFormatoAPrecioParaImpresion(vMonto);
                        vTipoTasa = LibXml.GetElementValueOrEmpty(vXElement, "AlicuotaIva");
                        vTipoTasa = DarFormatoAAlicuotaIva((eTipoDeAlicuota)LibConvert.DbValueToEnum(vTipoTasa));
                        vPrcDescuento = LibXml.GetElementValueOrEmpty(vXElement, "PorcentajeDescuento");
                        vPrcDescuento = DarFormatoADescuentoParaImpresion(vPrcDescuento);
                        vSerial = LibXml.GetElementValueOrEmpty(vXElement, "Serial");
                        vRollo = LibXml.GetElementValueOrEmpty(vXElement, "Rollo");
                        if (!vSerialyRolloLuegoDescripcion) {
                            ImprimeSerialyRollo(vSerial, vRollo);
                            vEstatus = true;
                        }
                        if (!ImprimirArticuloVenta(vDescripcion, vCantidad, vMonto, vTipoTasa, vPrcDescuento, valIsNotaDeCredito)) {
                            vEstatus &= true;
                            throw new Exception("Articulo no impreso");
                        }
                        if (vSerialyRolloLuegoDescripcion && vEstatus) {
                            ImprimeSerialyRollo(vSerial, vRollo);
                            vEstatus &= true;
                        }
                    } else {
                        vEstatus = false;
                        throw new GalacException("Fin del Papel", eExceptionManagementType.Controlled);
                    }
                }
                if (vImprimeCamposDefinibles & vEstatus) {
                    ImprmirCamposDefinibles(valDocumentoFiscal);
                    vEstatus &= true;
                }

                if (LibConvert.ToInt(vPrcDescuentoTotal) != 0) {
                    vCmd = "3";
                    vEstatus &= TfhkPrinter.SendCmd(vCmd);
                    vCmd = "p-" + vPrcDescuentoTotal;
                    vEstatus &= TfhkPrinter.SendCmd(vCmd);
                }
            } catch (Exception vEx) {
                CancelarDocumentoFiscalEnImpresion(false);
                CerrarConexion();
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
            return vEstatus;
        }

        private bool ImprimirArticuloVenta(string valDescripcion, string valCantidad, string valPrecio, string valTipoTasa, string valPorcetajeDesRenglon, bool valEsDevolucion) {
            bool vResult = false;
            string vCmd = "";

            if (valEsDevolucion) {
                vCmd = "d" + valTipoTasa;
            } else {
                vCmd = valTipoTasa;
            }
            vCmd = vCmd + valPrecio;
            vCmd = vCmd + valCantidad;
            vCmd = vCmd + LibText.Left(valDescripcion, 150);
            vResult = TfhkPrinter.SendCmd(vCmd);
            if (LibConvert.ToInt(valPorcetajeDesRenglon) > 0) {
                vCmd = "p-" + valPorcetajeDesRenglon;
                vResult &= TfhkPrinter.SendCmd(vCmd);
            }
            return vResult;
        }

        private void ImprmirCamposDefinibles(XElement valData) {
            string vCmd;
            int vLinea;

            vLinea = 5;
            List<XElement> vCamposDefinibles = valData.Descendants("GpResultDetailRenglonFactura").ToList();
            foreach (XElement vRecord in vCamposDefinibles) {
                vCmd = "i0" + LibConvert.ToStr(vLinea) + LibXml.GetElementValueOrEmpty(vRecord, "CDEF");
                TfhkPrinter.SendCmd(vCmd);
                vLinea++;
                if (vLinea.Equals(10)) {
                    break;
                }
            }
        }

        private double CalcularMontoDescuentoTotal(string valPrcDescuentoArticulo, string valCantidad, string valMonto, string valPrcDescuentoTotal) {
            double vMontoDescuento = 0;
            double vMontoDescuentoTotal = 0;
            double vPrcPorcentajeDesc = LibGalac.Aos.Base.LibConvert.ToDouble(valPrcDescuentoArticulo) / 10000.0;
            double vPrcPorcentajeDescTotal = LibGalac.Aos.Base.LibConvert.ToDouble(valPrcDescuentoTotal) / 10000.0;
            double vMonto = LibGalac.Aos.Base.LibConvert.ToDouble(valMonto) / 100.0;
            double vCantidad = LibGalac.Aos.Base.LibConvert.ToDouble(valCantidad) / 1000.0;
            vMontoDescuento = LibGalac.Aos.Base.LibConvert.ToDouble("" + LibGalac.Aos.Base.LibMath.RoundToNDecimals((decimal)(vPrcPorcentajeDesc * vMonto * vCantidad), 2));
            vMontoDescuentoTotal = ((vMonto * vCantidad) - vMontoDescuento) * vPrcPorcentajeDescTotal;
            vMontoDescuentoTotal = LibGalac.Aos.Base.LibConvert.ToDouble("" + LibGalac.Aos.Base.LibMath.RoundToNDecimals((decimal)vMontoDescuentoTotal, 2));
            return vMontoDescuentoTotal;
        }

        private bool EnviarPagos(XElement valMedioDePago) {
            string vMedioDePago = "";
            string vFormatoDeCobro = "";
            string vMonto = "";
            string vCmd = "";
            bool vResult = false;

            try {
                List<XElement> vNodos = valMedioDePago.Descendants("GpResultDetailRenglonCobro").ToList();
                if (vNodos.Count > 0) {
                    foreach (XElement vXElement in vNodos) {
                        vMedioDePago = LibXml.GetElementValueOrEmpty(vXElement, "CodigoFormaDelCobro");
                        vFormatoDeCobro = FormaDeCobro(vMedioDePago, vModeloFactory);
                        vMonto = LibXml.GetElementValueOrEmpty(vXElement, "Monto");
                        vMonto = DarFormatoAMontoTotalParaImpresion(vMonto);
                        vCmd = "2" + vFormatoDeCobro + vMonto;
                        vResult = TfhkPrinter.SendCmd(vCmd);
                    }
                }
                return vResult;
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Imprimir Todos los articulos", eExceptionManagementType.Controlled);
            }
        }

        private bool AbrirComprobanteFiscal(string valRazonSocial, string valRif, string valDireccion, string valTelefono, string valObservaciones, string valTipo = "", string valNumeroFacturaOriginal = "", string valSerialMaquina = "", string valFecha = "") {
            try {
                bool vResult = false;
                string vCMD = "";
                vCMD = "iR*" + LibText.Trim(LibText.SubString(valRif, 0, 11));
                vResult = TfhkPrinter.SendCmd(vCMD);
                vCMD = "iS*" + LibText.Trim(LibText.SubString(valRazonSocial, 0, 40));
                if (valTipo.Equals("D")) {
                    vCMD = "iF*" + LibText.Trim(LibText.SubString(valNumeroFacturaOriginal, 0, 11));
                    vResult = TfhkPrinter.SendCmd(vCMD);
                    vCMD = "iD*" + LibText.Trim(LibText.SubString(valFecha, 0, 12));
                    vResult = TfhkPrinter.SendCmd(vCMD);
                    vCMD = "iI*" + LibText.Trim(LibText.SubString(valSerialMaquina, 0, 14));
                    vResult = TfhkPrinter.SendCmd(vCMD);
                }
                vResult &= TfhkPrinter.SendCmd(vCMD);
                vCMD = "i00Telf: " + LibText.Trim(LibText.SubString(valTelefono, 0, 20));
                vResult &= TfhkPrinter.SendCmd(vCMD);
                vCMD = "i01Direccion: " + LibText.Trim(LibText.SubString(valDireccion, 0, 29));
                vResult &= TfhkPrinter.SendCmd(vCMD);
                vCMD = "i02" + LibText.Trim(LibText.SubString(valDireccion, 30, 40));
                vResult &= TfhkPrinter.SendCmd(vCMD);
                if (valObservaciones.Equals("")) {
                    vCMD = "i03Observaciones: " + LibText.Trim(LibText.SubString(valObservaciones, 0, 25));
                    vResult &= TfhkPrinter.SendCmd(vCMD);
                    vCMD = "i04" + LibText.Trim(LibText.SubString(valObservaciones, 26, 40));
                    vResult &= TfhkPrinter.SendCmd(vCMD);
                } else {
                    vCMD = "i03" + LibText.Trim(LibText.SubString(valDireccion, 70, 40));
                    vResult &= TfhkPrinter.SendCmd(vCMD);
                }
                return vResult;
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Abrir Comprobante Fiscal", eExceptionManagementType.Controlled);
            }
        }

        private string FormaDeCobro(string valFormaDeCobro, eImpresoraFiscal valModeloImpFiscal) {
            string vResultado = "";
            bool vEsSamsung = false;

            vEsSamsung = (valModeloImpFiscal == eImpresoraFiscal.BIXOLON270 || valModeloImpFiscal == eImpresoraFiscal.OKIML1120 || valModeloImpFiscal == eImpresoraFiscal.BIXOLON350 ||
            valModeloImpFiscal == eImpresoraFiscal.ACLASPP1F3 || valModeloImpFiscal == eImpresoraFiscal.DASCOMTALLY1125);
            if (vEsSamsung) {
                if (valFormaDeCobro.Equals("00001")) {
                    vResultado = "01";
                } else if (valFormaDeCobro.Equals("00002")) {
                    vResultado = "05";
                } else if (valFormaDeCobro.Equals("00003")) {
                    vResultado = "09";
                } else {
                    vResultado = "01";
                }
            } else {
                if (valFormaDeCobro.Equals("00001")) { //Nuevos Modelos
                    vResultado = "01";
                } else if (valFormaDeCobro.Equals("00002")) {
                    vResultado = "07";
                } else if (valFormaDeCobro.Equals("00003")) {
                    vResultado = "13";
                } else {
                    vResultado = "01";
                }
            }
            return vResultado;
        }

        public bool ReimprimirFactura(string valNumeroFactura) {
            short vModo = 0;
            short vTipo = 0;
            return ReimprimirDocumento(valNumeroFactura, vModo, vTipo);
        }

        public bool ReimprimirNotaDeCredito(string valNumeroNotaDeCredito) {
            short vModo = 0;
            short vTipo = 1;
            return ReimprimirDocumento(valNumeroNotaDeCredito, vModo, vTipo);
        }

        public bool ReimprimirReporteZ(string valNumeroReporteZ) {
            short vModo = 0;
            short vTipo = 2;
            return ReimprimirDocumento(valNumeroReporteZ, vModo, vTipo);
        }

        public bool ReimprimirReporteX(string valNumeroReporteX) {
            short vModo = 0;
            short vTipo = 4;
            return ReimprimirDocumento(valNumeroReporteX, vModo, vTipo);
        }

        public bool ReimprimirDocumentoNoFiscal(string valNumeroDocumentoNoFiscal) {
            short vModo = 0;
            short vTipo = 3;
            return ReimprimirDocumento(valNumeroDocumentoNoFiscal, vModo, vTipo);
        }

        private bool ReimprimirDocumento(string valNumero, short valModo, short valTipo) {
            short vModo = valModo;
            short vTipo = valTipo;
            string vNumero = valNumero;
            /*
            try {
            AbrirConexion();
            mVMax.ReimpresionMA(ref vTipo, ref vModo, ref vNumero);
            CerrarConexion();
            return true;
            } catch (Exception vEx) {
            throw new LibGalac.Aos.Catching.GalacException("Re-Imprimir Documento", vEx);
            }
            */
            return false;
        }

        public bool CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            return false;
        }

        public bool GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            return false;
        }

        public XElement GetFk(string valCallingModule, StringBuilder valParameters) {
            return null;
        }

        public void LeerEstadoDeImpresora() {
            string vCmd = "S1";
            _rutaStatusPrinter = ObtenerRutaData();
            if (AbrirConexion()) {
	            Thread.Sleep(500);
                TfhkPrinter.UploadStatusCmd(vCmd, _rutaStatusPrinter);
                mEstado = TfhkPrinter.GetS1PrinterData();
                CerrarConexion();
            } else {
                throw new LibGalac.Aos.Catching.GalacException("Error de Comunicaión, Revisar Impresora", eExceptionManagementType.Controlled);
            }
        }
    }
}

