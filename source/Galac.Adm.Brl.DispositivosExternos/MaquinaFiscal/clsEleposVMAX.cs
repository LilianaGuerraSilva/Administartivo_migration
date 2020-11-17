using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using System.Windows.Forms.Design;
using System.ComponentModel;
using AxVMAXOCX;
using System.Threading;
using LibGalac.Aos.Brl;
using Galac.Adm.Brl.DispositivosExternos;
using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal {
    public class clsEleposVMAX : IImpresoraFiscalPdn {

        private AxVMAXOCX.AxVMAX mVMax;
        private int mNDecimalParaCantidad = 3;
        private int mNDecimalParaMonto = 2;
        private int mNEnterosParaMonto = 8;
        private int mNEnterosParaCantidad = 5;
        private int mNEnterosParaMontoDecuento = 2;

        public clsEleposVMAX(XElement valXmlDatosImpresora) {
            ConfigurarImpresora(valXmlDatosImpresora);
        }

        private void ConfigurarImpresora(XElement valXmlDatosImpresora) {
            try {
                mVMax = new AxVMAXOCX.AxVMAX();
                mVMax.CreateControl();
                string vNPuertoCom = LibXml.GetPropertyString(valXmlDatosImpresora, "PuertoMaquinaFiscal");
                string vNPuertoVisor = LibXml.GetPropertyString(valXmlDatosImpresora, "Puerto");
                short vValorPuertoCom = short.Parse(vNPuertoCom);
                mVMax.NPuerto = vValorPuertoCom;
                mVMax.NPuertoVisor = vNPuertoVisor;
            } catch (Exception vEx) {
                //throw new LibGalac.Aos.Catching.GalacWrapperException("Constructor Elepos VMAX", vEx);
            }
        }

        public bool ComprobarConexion() {
            bool vResult = false;
            try {
                mVMax.LeeEstatus();
                return vResult = mVMax.ErrorMemoriaFiscal;
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacWrapperException("Comprobar Conexión", vEx);
            }
        }

        private bool ImprimirArticuloVenta(ref string valDescripcion, ref  string valCantidad, ref  string valMonto, ref string valImpuesto, ref  string valPrcDescuento, string valSerial, string valRollo, bool valIsNotaCredito) {
            bool vResult = false;
            short vTipo = 1;
            double vPrcPorcentajeDesc = LibGalac.Aos.Base.LibConvert.ToDouble(valPrcDescuento) / 10000.0;
            double vMonto = LibGalac.Aos.Base.LibConvert.ToDouble(valMonto) / 100.0;
            double vCantidad = LibGalac.Aos.Base.LibConvert.ToDouble(valCantidad) / 1000.0;
            string vDescripcionDescuento = "Descuento";
            string vMontoDescuento = clsImpresoraFiscalUtil.RedondearNumeroANDecimalesyDarFormatoSeparadorDeMiles(vPrcPorcentajeDesc * vMonto * vCantidad, 2);
            vMontoDescuento = clsImpresoraFiscalUtil.DarFormatoANumero(vMontoDescuento, mNEnterosParaMonto, mNDecimalParaMonto, false);
            if (!valSerial.Trim().Equals("")) {
                valDescripcion += " " + valSerial;
            }
            if (!valRollo.Trim().Equals("")) {
                valDescripcion += " " + valRollo;
            }
            valDescripcion = LibGalac.Aos.Base.LibText.SubString(valDescripcion, 0, 100);
            if (!valIsNotaCredito) {
                vResult = mVMax.Item(ref valDescripcion, ref valCantidad, ref valMonto, ref valImpuesto, ref vTipo);
            } else
                vResult = mVMax.ItemDev(ref valDescripcion, ref valCantidad, ref valMonto, ref valImpuesto, ref vTipo);

            if (LibGalac.Aos.Base.LibConvert.ToDouble(vMontoDescuento) > 0) {
                vResult = mVMax.ItemDesc(ref vDescripcionDescuento, ref vMontoDescuento, ref valImpuesto);
            }
            return vResult;
        }

        public eStatusImpresorasFiscales EstadoDelPapel(bool AbrirPuerto) {
            eStatusImpresorasFiscales vResult = (eStatusImpresorasFiscales)0;
            try {
                mVMax.LeeEstatus();
                if (mVMax.ErrorImpresora) {
                    vResult = (eStatusImpresorasFiscales)1;
                }
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacAlertException("Estado del Papel " + vEx.Message);
            }
            return vResult;
        }

        public string ObtenerFechaYHora() {
            string vResult = "";


            return vResult;
        }

        private bool ImprimirTodosLosArticulos(XElement valDocumentoFiscal, bool valIsNotaDeCredito) {
            string vDescripcion;
            string vCantidad;
            string vMonto;
            string vTipoTasa;            
            string vPrcDescuento;
            string vPrcDescuentoTotal;
            string vSerial;
            string vRollo;
            double vMontoDescuentoAlicuotaE = 0;
            double vMontoDescuentoAlicuotaA = 0;
            double vMontoDescuentoAlicuotaR = 0;
            double vMontoDescuentoAlicuotaG = 0;
            string vSDescuentoAlicuotaE;
            string vSDescuentoAlicuotaA;
            string vSDescuentoAlicuotaR;
            string vSDescuentoAlicuotaG;
            double vMontoDescuento = 0;
            string vDescripcionDescuentoTotal = "Descuento Global";
            try {
                List<XElement> vNodos = valDocumentoFiscal.Descendants("GpResultDetailRenglonFactura").ToList();
                vPrcDescuentoTotal = LibXml.GetPropertyString(valDocumentoFiscal, "PorcentajeDescuento");
                vPrcDescuentoTotal = clsImpresoraFiscalUtil.DarFormatoANumero(vPrcDescuentoTotal, mNEnterosParaMontoDecuento, mNDecimalParaMonto, false);
                foreach (XElement vXElement in vNodos) {
                    vDescripcion = LibXml.GetElementValueOrEmpty(vXElement, "Articulo") + LibString.Space(2) + LibXml.GetElementValueOrEmpty(vXElement, "Descripcion");
                    vCantidad = LibXml.GetElementValueOrEmpty(vXElement, "Cantidad");
                    vCantidad = clsImpresoraFiscalUtil.DarFormatoANumero(vCantidad, mNEnterosParaCantidad, mNDecimalParaCantidad, false);
                    vMonto = LibXml.GetElementValueOrEmpty(vXElement, "PrecioSinIVA");
                    vMonto = clsImpresoraFiscalUtil.DarFormatoANumero(vMonto, mNEnterosParaMonto, mNDecimalParaMonto, false);
                    vTipoTasa = LibXml.GetElementValueOrEmpty(vXElement, "AlicuotaIva");
                    vTipoTasa = DarFormatoAlicuotaIva((eTipoDeAlicuota)LibConvert.DbValueToEnum(vTipoTasa));
                    vPrcDescuento = LibXml.GetElementValueOrEmpty(vXElement, "PorcentajeDescuento");
                    vPrcDescuento = clsImpresoraFiscalUtil.DarFormatoANumero(vPrcDescuento, mNEnterosParaMontoDecuento, mNDecimalParaMonto, false);
                    vSerial = LibXml.GetElementValueOrEmpty(vXElement, "Serial");
                    vRollo = LibXml.GetElementValueOrEmpty(vXElement, "Rollo");
                    vMontoDescuento = CalcularMontoDescuentoTotal(vPrcDescuento, vCantidad, vMonto, vPrcDescuentoTotal);
                    
                    if (!ImprimirArticuloVenta(ref vDescripcion, ref vCantidad, ref vMonto, ref vTipoTasa, ref vPrcDescuento, vSerial, vRollo, valIsNotaDeCredito)) {
                        throw new Exception("Articulo no impreso");
                    }

                }
                if (LibGalac.Aos.Base.LibConvert.ToDouble(vPrcDescuentoTotal) > 0) {
                    vSDescuentoAlicuotaA = clsImpresoraFiscalUtil.DarFormatoANumero("" + vMontoDescuentoAlicuotaA, 10, mNDecimalParaMonto, false);
                    vSDescuentoAlicuotaG = clsImpresoraFiscalUtil.DarFormatoANumero("" + vMontoDescuentoAlicuotaG, 10, mNDecimalParaMonto, false);
                    vSDescuentoAlicuotaR = clsImpresoraFiscalUtil.DarFormatoANumero("" + vMontoDescuentoAlicuotaR, 10, mNDecimalParaMonto, false);
                    vSDescuentoAlicuotaE = clsImpresoraFiscalUtil.DarFormatoANumero("" + vMontoDescuentoAlicuotaE, 10, mNDecimalParaMonto, false);
                    mVMax.DescuentoCF(ref vDescripcionDescuentoTotal, ref vSDescuentoAlicuotaE, ref vSDescuentoAlicuotaG, ref vSDescuentoAlicuotaR, ref vSDescuentoAlicuotaA);
                }
                mVMax.SubTotal();
                return true;
            } catch (Exception vEx) {
                CancelarDocumentoFiscalEnImpresion(false);
                CerrarConexion();
                throw new LibGalac.Aos.Catching.GalacWrapperException("Imprimir Todos los articulos", vEx);
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

        public bool AbrirConexion() {
            try {
                Thread.Sleep(800);
                mVMax.AbrirPuerto();
                Thread.Sleep(800);
                return true;
            } catch (Exception vEx) {
                CerrarConexion();
                throw new LibGalac.Aos.Catching.GalacWrapperException("Abrir Puerto", vEx);
            }
        }

        public bool CerrarConexion() {
            try {
                Thread.Sleep(800);
                mVMax.CerrarPuerto();
                Thread.Sleep(800);
                mVMax.Dispose();
                return true;
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacWrapperException("Cerrar Puerto", vEx);
            }
        }

        public bool RealizarReporteZ() {
            bool vResult = false;
            try {
                if (AbrirConexion()) {
                    string vReporteZ = "Z";
                    vResult = mVMax.CierreDiario(ref vReporteZ);
                    CerrarConexion();
                }
                return vResult;
            } catch (Exception vEx) {
                CerrarConexion();
                throw new LibGalac.Aos.Catching.GalacWrapperException("Realizar Reporte Z", vEx);
            }
        }

        public bool RealizarReporteX() {
            bool vResult = false;
            try {
                if (AbrirConexion()) {
                    string vReporteX = "X";
                    vResult = mVMax.CierreDiario(ref vReporteX);
                    CerrarConexion();
                }
                return vResult;
            } catch (Exception vEx) {
                CerrarConexion();
                throw new LibGalac.Aos.Catching.GalacWrapperException("Realizar Reporte X", vEx);
            }
        }

        public bool ImprimirFacturaFiscal(XElement valDocumentoFiscal) {
            string vDireccion = LibXml.GetPropertyString(valDocumentoFiscal, "DireccionCliente");
            string vRif = LibXml.GetPropertyString(valDocumentoFiscal, "NumeroRIF");
            string vRazonSocial = LibXml.GetPropertyString(valDocumentoFiscal, "NombreCliente");
            string vObservaciones = LibXml.GetPropertyString(valDocumentoFiscal, "Observaciones");
            string vTipoFactura = "1";
            string vVacio = "";
            bool vResult = false;
            try {
                if (AbrirConexion()) {
                    vResult = AbrirComprobanteFiscal(vRazonSocial, vRif, vTipoFactura, vVacio, vVacio, vVacio, vVacio, vDireccion, vVacio, vObservaciones);
                    vResult &= ImprimirTodosLosArticulos(valDocumentoFiscal, false);
                    vResult &= EnviarPagos(valDocumentoFiscal);
                    vResult &= mVMax.CerrarCF();
                    CerrarConexion();
                    Thread.Sleep(800);
                }
                return vResult;
            } catch (Exception vEx) {
                CancelarDocumentoFiscalEnImpresion(false);
                throw new LibGalac.Aos.Catching.GalacWrapperException("Imprimir Venta", vEx);
            }
        }

        private bool EnviarPagos(XElement valDocumentoFiscal) {
            string vDescripcion = "Efectivo";
            string vMonto = mVMax.TotalFactura;
            short vTipoPago = 1;
            try {
                List<XElement> vNodos = valDocumentoFiscal.Descendants("GpResultDetailRenglonCobro").ToList();
                if (vNodos.Count <= 0) {
                    mVMax.PagoCF(ref vDescripcion, ref vMonto, ref vTipoPago);
                }
                foreach (XElement vXElement in vNodos) {
                    vDescripcion = LibXml.GetElementValueOrEmpty(vXElement, "CodigoFormaDelCobro");
                    vDescripcion = FormaDeCobro(vDescripcion);
                    vMonto = LibXml.GetElementValueOrEmpty(vXElement, "Monto");
                    vMonto = clsImpresoraFiscalUtil.DarFormatoANumero(vMonto, mNEnterosParaMonto, mNDecimalParaMonto, false);
                    mVMax.PagoCF(ref vDescripcion, ref vMonto, ref vTipoPago);
                }
                return true;
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacWrapperException("Imprimir Todos los articulos", vEx);
            }
        }

        private string DarFormatoAlicuotaIva(eTipoDeAlicuota valTipoAlicuota) {
            string vResult = "";

            switch (valTipoAlicuota) {
                case eTipoDeAlicuota.AlicuotaGeneral:
                case eTipoDeAlicuota.AlicuotaGeneralNC:
                    vResult = "G";
                    break;
                case eTipoDeAlicuota.Alicuota2:
                case eTipoDeAlicuota.Alicuota2NC:
                    vResult = "R";
                    break;
                case eTipoDeAlicuota.Alicuota3:
                case eTipoDeAlicuota.Alicuota3NC:
                    vResult = "A";
                    break;
                case eTipoDeAlicuota.Exento:
                case eTipoDeAlicuota.ExentoNC:
                    vResult = "E";
                    break;
                default:
                    vResult = "G";
                    break;
            }
            return vResult;
        }

        private string FormaDeCobro(string valMedioDePago) {
            string vResult = "";

            switch (valMedioDePago) {
                case "00001":
                    vResult = "Efectivo";
                    break;
                case "00002":
                    vResult = "Cheque";
                    break;
                case "00003":
                    vResult = "Tarjeta";
                    break;
                case "00004":
                    vResult = "Deposito";
                    break;
                default:
                    vResult = "Efectivo";
                    break;
            }
            return vResult;
        }

        public string ObtenerSerial() {
            string vSerial = "";
            try {
                AbrirConexion();
                mVMax.LeeDatosFiscales();
                vSerial = mVMax.Serial;
                CerrarConexion();
                return vSerial;
            } catch (Exception vEx) {
                CerrarConexion();
                throw new LibGalac.Aos.Catching.GalacWrapperException("Obtener Serial", vEx);
            }
        }

        public string ObtenerUltimoNumeroFactura() {
            string vUltimoNumeroFactura = "";
            try {
                AbrirConexion();
                while (vUltimoNumeroFactura == "") {
                    mVMax.LeeDatosFiscales();
                    vUltimoNumeroFactura = mVMax.UltimoCFAbierto;
                }
                CerrarConexion();
                return vUltimoNumeroFactura;
            } catch (Exception vEx) {
                CerrarConexion();
                throw new LibGalac.Aos.Catching.GalacWrapperException("Obtener UltimoNumero Factura", vEx);
            }
        }

        public string ObtenerUltimoNumeroNotaDeCredito() {
            string vUltimoNumeroNotaDeCredito = "";
            try {
                AbrirConexion();
                while (vUltimoNumeroNotaDeCredito == "") {
                    mVMax.LeeDatosFiscales();
                    vUltimoNumeroNotaDeCredito = mVMax.UltimaNCAbierta;
                }
                CerrarConexion();
                return vUltimoNumeroNotaDeCredito;
            } catch (Exception vEx) {
                CerrarConexion();
                throw new LibGalac.Aos.Catching.GalacWrapperException("Obtener UltimoNumero Nota de Credito", vEx);
            }
        }

        public string ObtenerUltimoNumeroReporteZ() {
            string vUltimoNumeroReporteZ = "";
            try {
                AbrirConexion();
                mVMax.LeeZ();
                vUltimoNumeroReporteZ = mVMax.rNumZ;
                CerrarConexion();
                return vUltimoNumeroReporteZ;
            } catch (Exception vEx) {
                CerrarConexion();
                throw new LibGalac.Aos.Catching.GalacWrapperException("Obtener UltimoNumero Reporte Z", vEx);
            }
        }

        private bool AbrirComprobanteFiscal(string refRazonSocial, string refRif, string refTipo, string refNumeroFacturaOriginal, string refSerialMaquina, string refFecha, string refHora, string valDireccion, string valTelefono, string valObservaciones) {
            bool vResult = false;
            try {
                if (!LibString.IsNullOrEmpty(valDireccion)) {
                    refRazonSocial += " Dir: " + valDireccion;
                }

                if (!LibString.IsNullOrEmpty(valDireccion)) {
                    refRazonSocial += " Obs: " + valObservaciones;
                }
                refRazonSocial = LibGalac.Aos.Base.LibText.SubString(refRazonSocial, 0, 120);
                vResult = mVMax.AbrirCF(ref refRazonSocial, ref refRif, ref refTipo, ref refNumeroFacturaOriginal, ref refSerialMaquina, ref refFecha, ref refHora);
                return vResult;
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacWrapperException("Abrir Comprobante Fiscal", vEx);
            }
        }

        public bool ImprimirNotaCredito(XElement vDocumentoFiscal) {
            string vDireccion = LibXml.GetPropertyString(vDocumentoFiscal, "DireccionCliente");
            string vRif = LibXml.GetPropertyString(vDocumentoFiscal, "NumeroRIF");
            string vRazonSocial = LibXml.GetPropertyString(vDocumentoFiscal, "NombreCliente");
            string vObservaciones = LibXml.GetPropertyString(vDocumentoFiscal, "Observaciones");
            string vFacturaAfectada = LibXml.GetPropertyString(vDocumentoFiscal, "NumeroComprobanteFiscal");
            string vSerialMaquina = LibXml.GetPropertyString(vDocumentoFiscal, "SerialMaquinaFiscal");
            string vFecha = LibXml.GetPropertyString(vDocumentoFiscal, "FechaDeFacturaAfectada");
            string vHora = LibXml.GetPropertyString(vDocumentoFiscal, "HoraModificacion");
            vFecha = LibGalac.Aos.Base.LibString.Replace(vFecha, "/", "/");
            vHora = LibGalac.Aos.Base.LibString.Replace(vHora, "/", "/");

            string vTipoFactura = "D";
            try {
                mVMax.AbrirPuerto();
                AbrirComprobanteFiscal(vRazonSocial, vRif, vTipoFactura, vFacturaAfectada, vSerialMaquina, vFecha, vFecha, vDireccion, "", vObservaciones);
                ImprimirTodosLosArticulos(vDocumentoFiscal, true);
                mVMax.CerrarCF();
                CerrarConexion();
                Thread.Sleep(800);
                return true;
            } catch (Exception vEx) {
                CancelarDocumentoFiscalEnImpresion(false);
                throw new LibGalac.Aos.Catching.GalacWrapperException("Imprimir Venta", vEx);
            }
        }

        public bool CancelarDocumentoFiscalEnImpresion(bool valAbrirConexion) {
            bool vResult;
            try {
                if (valAbrirConexion) {
                    AbrirConexion();
                }
                mVMax.CancelaCF();
                CerrarConexion();
                vResult = true;
                return vResult;
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacWrapperException("Cancelar Documento Fiscal en Impresion", vEx);
            }
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
            try {
                AbrirConexion();
                mVMax.ReimpresionMA(ref vTipo, ref vModo, ref vNumero);
                CerrarConexion();
                return true;
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacWrapperException("Re-Imprimir Documento", vEx);
            }
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

    }
}
