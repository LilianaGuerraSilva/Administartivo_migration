using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal {
    public class clsQPrint : IImpresoraFiscalPdn {
        #region comandos
        [DllImport("quorion.dll")]
        unsafe public static extern int SendCommand(string vCmd, byte* vTrama);
        [DllImport("quorion.dll")]
        public static extern int OpenComPort(int vPort, int Baudrate);
        [DllImport("quorion.dll")]
        public static extern int CloseComPort();
        [DllImport("quorion.dll")]
        public static extern int OpenIPPort(int vIp0, int vIp1, int vIp2, int vIp3, int vReg);
        [DllImport("quorion.dll")]
        public static extern int CloseIPPort();
        [DllImport("quorion.dll")]
        unsafe public static extern int GetVer(byte* vVersion);
        #endregion
        #region Comandos QPrint
        const decimal _NumeroMaximo = 9999999.99M;
        const string _SeparadorDeCampos = "\u003B";
        const string _LiteralDeTexto = "\u0022";
        const string _ConsultaEstatus = "F0;3";
        const string _RealizarReporteZ = "F3;1";
        const string _RealizarDescuentoEnRenglon = "F2;6;;1;";
        const string _ImprimeTexto = "F3;1";
        const string _RealizarReporteX = "F3;1;1";
        const string _CancelarDocumentoFiscal = "F2;5";
        const string _AbreFacturaFiscal = "F2;1;0;3";
        const string _AbreNotaDeCredito = "F2;12;0;3";
        const string _InsertarItem = "F2;3";
        const string _CerrarFacturaEnviaPagos = "F2;2;";
        const string _CerrarNotaDeCredito = "F2;13";
        const string _FacturaSubTotal = "F2;4";
        #endregion
        #region Comandos con Retorno
        const int _LeeEstatusError = 1;
        const int _LeeEstatusImpresora = 2;
        const int _LeeFecha = 18;
        const int _LeeHora = 19;
        const int _LeeSerial = 17;
        const int _LeeUltimaFacturaEmitida = 21;
        const int _LeeUltimaNotaDeCredito = 20;
        const int _LeeUltimoCierreZRealizado = 22;
        #endregion
        private int _CommPort = 0;
        private int _Baud = 0;
        private bool _PortIsOpen = false;

        public clsQPrint(XElement valXmlDatosImpresora) {
            ConfigurarImpresora(valXmlDatosImpresora);
        }

        private void ConfigurarImpresora(XElement valXmlDatosImpresora) {
            ePuerto ePuerto = (ePuerto)LibConvert.DbValueToEnum(LibXml.GetPropertyString(valXmlDatosImpresora,"PuertoMaquinaFiscal"));            
            _CommPort = LibConvert.ToInt(ePuerto.GetDescription(1));            
            _Baud = 6; //567000 Bauds ByDefault
        }

        public string ObtenerFechaYHora() {
            bool vEstado = false;
            string vMensaje = "";
            int vRequest = 0;
            string vFecha = "";
            string vHora = "";
            string vResult = "";

            try {
                vRequest = SendCMD(_ConsultaEstatus, ref vFecha);
                vEstado = CheckRequest(vRequest, ref vMensaje);
                vFecha = LeerRepuestaImpFiscal(vFecha, _LeeFecha);
                vRequest = SendCMD(_ConsultaEstatus, ref vHora);
                vEstado &= CheckRequest(vRequest, ref vMensaje);
                vHora = LeerRepuestaImpFiscal(vHora, _LeeHora);
                if (vEstado) {
                    vResult = vFecha + LibString.Space(1) + vHora;
                } else {
                    throw new LibGalac.Aos.Catching.GalacException(vMensaje, eExceptionManagementType.Controlled);
                }

                return vResult;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        public bool AbrirConexion() {
            bool vEstado = false;
            int vResult = 0;
            string vMensaje = "Abrir Puerto Serial";
            try {
                if(!_PortIsOpen) {
                    vResult = OpenComPort(_CommPort,_Baud);
                    if(vResult.Equals(1)) {
                        vEstado = true;
                        _PortIsOpen = true;
                    } else {
                        _PortIsOpen = false;
                        throw new GalacException(vMensaje,eExceptionManagementType.Controlled);
                    }
                }
                return vEstado;
            } catch (ExternalException vIOEx) {
                throw new GalacException("Puerto de comunicación no disponible, Revisar Conexiones, "+ vIOEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool CerrarConexion() {
            bool vEstado = false;
            int vResult = 0;
            try {
                if (_PortIsOpen) {
                    vResult = CloseComPort();
                    _PortIsOpen = false;
                    vEstado = true;
                    Thread.Sleep(250);
                }
                return vEstado;
            } catch (ExternalException vIOEx) {
                throw new GalacException("Puerto de comunicación no disponible, Revisar Conexiones, " + vIOEx.Message,eExceptionManagementType.Controlled);                
            }
        }

        public bool ComprobarEstado() {
            int vRequest = 0;
            string vSalida = "";
            bool vResult = false;
            string vMensaje = "";        

            vRequest = SendCMD(_ConsultaEstatus, ref vSalida);
            vSalida = LeerRepuestaImpFiscal(vSalida, _LeeEstatusImpresora);
            vResult = RetornoEstadosImpresora(vSalida,ref vMensaje);
            if (!vResult) {
                throw new GalacException(vMensaje,eExceptionManagementType.Controlled);
            }            
            return vResult;
        }

        private bool RetornoEstadosImpresora(string valEstado, ref string refMensaje) {

            bool vResult = false;

            switch (valEstado) {
                case "X":
                    refMensaje = "La Impresora No Esta Lista";
                    break;
                case "R":
                    refMensaje = "Impresora Lista, En Espera";
                    vResult = true;
                    break;
                case "O":
                    refMensaje = "Documento Fiscal Abierto";
                    break;
                case "S":
                    refMensaje = "SubTotal Incompleto";
                    break;
                case "N":
                    refMensaje = "Documento No Fiscal Abierto";
                    break;
                case "C":
                    refMensaje = "Modo Recovery";
                    break;
                default:
                    break;
            }            
            return vResult;
        }

        public string ObtenerSerial(bool valAbrirConexion) {
            bool vEstado = false;
            int vResult = 0;
            string vSerial = "";
            string vMensaje = "";

            try {
                if (valAbrirConexion) {
                    AbrirConexion();
                }
                vResult = SendCMD(_ConsultaEstatus, ref vSerial);
                vEstado = CheckRequest(vResult, ref vMensaje);
                if (vEstado) {
                    vSerial = LeerRepuestaImpFiscal(vSerial, _LeeSerial);
                } else {
                    throw new LibGalac.Aos.Catching.GalacException(vMensaje, eExceptionManagementType.Controlled);
                }

                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vSerial;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        public eStatusImpresorasFiscales EstadoDelPapel(bool valAbrirPuerto) {
            eStatusImpresorasFiscales vResult = eStatusImpresorasFiscales.eListoEnEspera;
            int vRequest = 0;
            string vSalida = "";

            try {
                if (valAbrirPuerto) {
                    AbrirConexion();
                }

                vRequest = SendCMD(_ConsultaEstatus, ref vSalida);
                vSalida = LeerRepuestaImpFiscal(vSalida, _LeeEstatusError);

                switch (vSalida) {
                    case "0":
                        vResult = eStatusImpresorasFiscales.eListoEnEspera;
                        break;
                    case "32":
                    case "33":
                        vResult = eStatusImpresorasFiscales.eSinPapel;
                        break;
                }

                if (valAbrirPuerto) {
                    CerrarConexion();
                }
                return vResult;
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Estado del Papel " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool RealizarReporteZ() {
            string vResult = "";
            bool vEstatus = false;
            string vMensaje = "";
            int vRequest = 0;

            try {
                AbrirConexion();
                vRequest = SendCMD(_RealizarReporteZ, ref vResult);
                vEstatus = CheckRequest(vRequest, ref vMensaje);
                CerrarConexion();
                return vEstatus;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        public bool RealizarReporteX() {
            string vResult = "";
            bool vEstatus = false;
            string vMensaje = "";
            int vRequest = 0;

            try {
                AbrirConexion();
                vRequest = SendCMD(_RealizarReporteX, ref vResult);
                vEstatus = CheckRequest(vRequest, ref vMensaje);
                CerrarConexion();
                return vEstatus;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        public string ObtenerUltimoNumeroFactura(bool valAbrirConexion) {
            string vUltimaFactura = "";
            string vMensaje = "";
            int vResult = 0;
            bool vEstado = false;

            try {
                if (valAbrirConexion) {
                    AbrirConexion();
                }
                vResult = SendCMD(_ConsultaEstatus, ref vUltimaFactura);
                vEstado = CheckRequest(vResult, ref vMensaje);
                if (vEstado) {
                    vUltimaFactura = LeerRepuestaImpFiscal(vUltimaFactura, _LeeUltimaFacturaEmitida);
                } else {
                    throw new LibGalac.Aos.Catching.GalacException(vMensaje, eExceptionManagementType.Controlled);
                }
                if (valAbrirConexion) {
                    CerrarConexion();
                }

                return vUltimaFactura;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        public string ObtenerUltimoNumeroNotaDeCredito(bool valAbrirConexion) {
            string vUltimaNotaDeCredito = "";
            string vMensaje = "";
            int vResult = 0;
            bool vEstado = false;

            try {
                if (valAbrirConexion) {
                    AbrirConexion();
                }
                vResult = SendCMD(_ConsultaEstatus, ref vUltimaNotaDeCredito);
                vEstado = CheckRequest(vResult, ref vMensaje);
                if (vEstado) {
                    vUltimaNotaDeCredito = LeerRepuestaImpFiscal(vUltimaNotaDeCredito, _LeeUltimaNotaDeCredito);
                } else {
                    throw new LibGalac.Aos.Catching.GalacException(vMensaje, eExceptionManagementType.Controlled);
                }
                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimaNotaDeCredito;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        public string ObtenerUltimoNumeroReporteZ(bool valAbrirConexion) {
            string vUltimoReporteZ = "";
            string vMensaje = "";
            int vResult = 0;
            bool vEstado = false;

            try {
                if (valAbrirConexion) {
                    AbrirConexion();
                }
                vResult = SendCMD(_ConsultaEstatus, ref vUltimoReporteZ);
                vEstado = CheckRequest(vResult, ref vMensaje);
                if (vEstado) {
                    vUltimoReporteZ = LeerRepuestaImpFiscal(vUltimoReporteZ, _LeeUltimoCierreZRealizado);
                } else {
                    throw new LibGalac.Aos.Catching.GalacException(vMensaje, eExceptionManagementType.Controlled);
                }

                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimoReporteZ;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        public bool CancelarDocumentoFiscalEnImpresion(bool valAbrirConexion) {
            string vResult = "";
            bool vEstatus = false;
            string vMensaje = "";
            int vRequest = 0;

            try {
                if (valAbrirConexion) {
                    AbrirConexion();
                }
                vRequest = SendCMD(_CancelarDocumentoFiscal, ref vResult);
                vEstatus = CheckRequest(vRequest, ref vMensaje);
                if (!vEstatus) {
                    throw new GalacException(vMensaje, eExceptionManagementType.Controlled);
                }

                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vEstatus;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        public bool ImprimirFacturaFiscal(XElement vDocumentoFiscal) {
            string vRif = LibXml.GetPropertyString(vDocumentoFiscal, "NumeroRIF");
            string vRazonSocial = LibXml.GetPropertyString(vDocumentoFiscal, "NombreCliente");
            string vDireccion = LibXml.GetPropertyString(vDocumentoFiscal, "DireccionCliente");
            string vTelefono = LibXml.GetPropertyString(vDocumentoFiscal, "Telefono");
            string vObservaciones = LibXml.GetPropertyString(vDocumentoFiscal, "Observaciones");

            bool vResult = false;
            try {

                if (!_PortIsOpen) {
                    AbrirConexion();
                }
                if (AbrirComprobanteFiscal(vRif, vRazonSocial, vDireccion, vTelefono, vObservaciones)) {
                    vResult = ImprimirTodosLosArticulos(vDocumentoFiscal);
                    vResult &= CerrarComprobanteFiscal(vDocumentoFiscal, false);                    
                }
                if (_PortIsOpen) {
                    CerrarConexion();
                }
               
            } catch (LibGalac.Aos.Catching.GalacException vEx) {
                CerrarConexion();
                throw new GalacException("imprimir factura fiscal " + vEx.Message, eExceptionManagementType.Controlled);
            }
            return vResult;
        }

        private bool AbrirComprobanteFiscal(string valRif, string valRazonSocial, string valDireccion, string valTelefono, string valObservaciones) {
            try {
                bool vEstado = false;
                int vRepuesta = 0;
                string vMensaje = "";
                string vCMD = "";

                vCMD = _AbreFacturaFiscal;
                vCMD +=  FormatoParaDatosDelCliente("CI/Rif:"+valRif);
                vCMD += FormatoParaDatosDelCliente("Nombre: " + valRazonSocial);
                if (!LibString.IsNullOrEmpty(valDireccion)) {
                    vCMD += FormatoParaDatosDelCliente("Direccion: " + valDireccion);
                }
                if (!LibString.IsNullOrEmpty(valTelefono)) {
                    vCMD += FormatoParaDatosDelCliente("Telefono: " + valTelefono);
                }
                if (!LibString.IsNullOrEmpty(valObservaciones)) {
                    vCMD += FormatoParaDatosDelCliente("Obs.: " + valObservaciones);
                }
                vRepuesta = SendCMD(vCMD, ref vMensaje);
                vEstado = CheckRequest(vRepuesta, ref vMensaje);
                if (!vEstado) {
                    throw new GalacException("error al abrir comprobante fiscal " + vMensaje, eExceptionManagementType.Controlled);
                }
                return vEstado;
            } catch (LibGalac.Aos.Catching.GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private string FormatoParaDatosDelCliente(string valItem) {
            string vResult = "";
            int vIndiceInicio = 1;
            int vIndiceFinal = 0;
            int vCaracteresPorAnalizar = LibString.Len(valItem);

            while (vIndiceInicio < LibString.Len(valItem) && vCaracteresPorAnalizar > 0) {
                if (LibString.Len(valItem) - vIndiceInicio > 41) {
                    vIndiceFinal = vIndiceInicio + 41;
                } else {
                    vIndiceFinal = LibString.Len(valItem) + 1;
                }
                vResult += _SeparadorDeCampos + _LiteralDeTexto + LibString.SubString(valItem, vIndiceInicio-1, vIndiceFinal - vIndiceInicio) + _LiteralDeTexto;
                vCaracteresPorAnalizar = vCaracteresPorAnalizar - (vIndiceFinal - vIndiceInicio);
                vIndiceInicio = vIndiceInicio + 41;
            }
            return vResult;
        }

        private bool AplicarDescuentoGlobal(string valMontoDescuentoGlobal) {
            bool vEstado = false;           
            string vMensaje = "";
            int vRequest = 0;
            string vCmd = "";

            vCmd = "F2;6;;1;;" + valMontoDescuentoGlobal + ";;567;";
            vRequest = SendCMD(vCmd, ref vMensaje);
            vEstado = CheckRequest(vRequest, ref vMensaje);
            return vEstado;
        }

        private string CalcularTotalDevolucion(decimal valPrecios, decimal valCantidad, decimal valPrcRenglonDescuento, decimal valPrcTotalDescuento, decimal valAlicuota) {
            string vResult = "";
            decimal vAuxiliar = 0;
            vAuxiliar = LibMath.PowNDecimalsAndTruncValue(valPrecios * valCantidad, 2);
            vAuxiliar = LibMath.PowNDecimalsAndTruncValue(vAuxiliar * (1 - (valPrcRenglonDescuento / 100)), 2);
            vAuxiliar = LibMath.PowNDecimalsAndTruncValue(vAuxiliar * (1 - (valPrcTotalDescuento / 100)), 2);
            vAuxiliar = LibMath.PowNDecimalsAndTruncValue(vAuxiliar * (1 + (valAlicuota / 100)), 2);
            return vResult;
        }

        private bool CerrarComprobanteFiscal(XElement vDocumentoFiscal, bool EsDevolucion) {
            bool vResult = true;
            string vMontoDescuentoGlobal = "";
            bool vUsaCamposDefinibles = false;            
            int vRequest = 0;
            string vMensaje = "";

            try {
                vUsaCamposDefinibles = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "UsaCamposDefinibles");
                vMontoDescuentoGlobal = LibXml.GetPropertyString(vDocumentoFiscal, "PorcentajeDescuento");
                vMontoDescuentoGlobal = DarFormatoNumericoParaImpresion(vMontoDescuentoGlobal);

                if (vUsaCamposDefinibles) {
                    vResult &= ImprimeCamposDefinibles(vDocumentoFiscal);
                }

                if (LibImportData.ToDec(vMontoDescuentoGlobal) > 0) {
                    vResult &= AplicarDescuentoGlobal(vMontoDescuentoGlobal);
                }

                if (EsDevolucion) {                    
                    vRequest = SendCMD(_CerrarNotaDeCredito, ref vMensaje);                    
                } else {
                    vResult = EnviarPagos(vDocumentoFiscal);
                }
               
                if (!vResult) {
                    throw new GalacException("Proceso Cancelado", eExceptionManagementType.Controlled);
                }
                return true;
            } catch (GalacException vEx) {
                throw new GalacException("Cerrar Comprobante" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private string ValorFormaDeCobro(string vFormaDeCobro) {
            string vResultado = "";

            switch (vFormaDeCobro) {
                case "00001":
                    vResultado = "1";
                    break;
                case "00002":
                case "00004":
                    vResultado = "2";
                    break;
                case "00003":
                    vResultado = "3";
                    break;            
                default:
                    vResultado = "1";
                    break;
            }
            return vResultado;
        }

        private bool ImprimeCamposDefinibles(XElement vData) {
            bool vResult = true;
            string vCampoDefinible = "";
            string vCmd = "";
            string vTrama = "";
            string vStringField = "\"";
            int vRequest = 0;
            int vLinea = 1;

            vCmd = "U5;1";
            vRequest = SendCMD(vCmd, ref vTrama);
            List<XElement> vRecord = vData.Descendants("GpDataDetailCamposDefinibles").ToList();
            foreach (XElement vXElement in vRecord) {
                vCampoDefinible = LibString.SubString(LibXml.GetElementValueOrEmpty(vXElement, "CampoDefinible"), 0, 43);
                vCmd = "U9;" + LibConvert.ToStr(vLinea) + ";0;" + vStringField + vCampoDefinible + vStringField + vStringField;
                vRequest = SendCMD(vCmd, ref vTrama);
                vResult &= CheckRequest(vRequest, ref vTrama);
                vLinea++;
            }
            vCmd = "U5;1";
            vRequest = SendCMD(vCmd, ref vTrama);
            vResult &= CheckRequest(vRequest, ref vTrama);
            return vResult;
        }

        private bool ImprimirTodosLosArticulos(XElement valDocumentoFiscal) {
            bool vEstatus = false;
            string vCodigo;
            string vDescripcion;
            string vCantidad;
            string vMonto;
            string vPrcDescuento;
            string vTasaAlicuota = "";
            string vSerial;
            string vRollo;
            string vTotalDevolucion = "";
            eStatusImpresorasFiscales PrintStatus;

            try {
                List<XElement> vRecord = valDocumentoFiscal.Descendants("GpResultDetailRenglonFactura").ToList();
                foreach (XElement vXElement in vRecord) {
                    PrintStatus = EstadoDelPapel(false);
                    if (!PrintStatus.Equals(eStatusImpresorasFiscales.eSinPapel)) {
                        vCodigo = LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement, "Articulo"), 0, 12);
                        vCodigo = LibImpresoraFiscalUtil.RemoverCaracteresInvalidos(vCodigo);
                        vDescripcion = LibXml.GetElementValueOrEmpty(vXElement, "Descripcion");
                        vCantidad = LibXml.GetElementValueOrEmpty(vXElement, "Cantidad");
                        vCantidad = DarFormatoNumericoParaImpresion(vCantidad);
                        vMonto = LibXml.GetElementValueOrEmpty(vXElement, "PrecioSinIVA");
                        vMonto = DarFormatoNumericoParaImpresion(vMonto);
                        vTasaAlicuota = LibXml.GetElementValueOrEmpty(vXElement, "PorcentajeAlicuota");
                        vTasaAlicuota = DarFormatoAAlicuotaIva((eTipoDeAlicuota)LibConvert.DbValueToEnum(vTasaAlicuota));
                        vPrcDescuento = (LibXml.GetElementValueOrEmpty(vXElement, "PorcentajeDescuento"));
                        vSerial = LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement, "Serial"), 20);
                        vRollo = LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement, "Rollo"), 20);
                        vTotalDevolucion = CalcularTotalDevolucion(vPrcDescuento, vMonto, vCantidad);

                        if (LibImportData.ToDec(vTotalDevolucion) > _NumeroMaximo || LibImportData.ToDec(vCantidad) >= 1000 || LibImportData.ToDec(vMonto) > _NumeroMaximo) {
                            vEstatus = false;
                            CancelarDocumentoFiscalEnImpresion(false);
                            throw new GalacException("Limites Máximos Superados", eExceptionManagementType.Alert);
                        }

                        vEstatus = ImprimirArticuloVenta(vDescripcion, vCantidad, vMonto, vTasaAlicuota, vPrcDescuento);

                        if (LibString.Len(vSerial) > 0) {
                            vEstatus &= EnviarDatosAdicionales(vSerial);
                        }

                        if (LibString.Len(vRollo) > 0) {
                            vEstatus &= EnviarDatosAdicionales(vRollo);
                        }

                        if (!vEstatus) {
                            throw new GalacException("Error al Imprimir Articulo", eExceptionManagementType.Controlled);
                        }
                    } else {
                        vEstatus = false;
                        throw new LibGalac.Aos.Catching.GalacException("Impresora sin papel, colocar otro nuevo", eExceptionManagementType.Controlled);
                    }
                }
            } catch (GalacException vEx) {
                CancelarDocumentoFiscalEnImpresion(false);                
            }
            return vEstatus;
        }

        private bool ImprimirArticuloVenta(string valDescripcion, string valCantidad, string valPrecio, string valAlicuota, string valPrcDescuento) {
            bool vResult = false;
            int vRequest = 0;
            string vMensaje = "";
            string vStringField = "\"";
            string vSingleSeparator = ";";
            string vCmd = "";

            vCmd = _InsertarItem + vSingleSeparator + vStringField + LibString.SubString(valDescripcion, 0, 24) + vStringField + vSingleSeparator + valPrecio + vSingleSeparator + valAlicuota + vSingleSeparator + valCantidad;
            vRequest = SendCMD(vCmd, ref vMensaje);
            vResult = CheckRequest(vRequest, ref vMensaje);
            if (vResult) {
                if (LibString.Len(valDescripcion) > 24) {
                    EnviarDatosAdicionales(LibString.SubString(valDescripcion, 25));
                }
                if (LibImportData.ToDec(valPrcDescuento) > 0) {
                    vCmd = _RealizarDescuentoEnRenglon + vSingleSeparator + vStringField + "Monto Desc. -" + vStringField + vSingleSeparator + valPrcDescuento + ";;46;";
                    vResult = CheckRequest(vRequest, ref vMensaje);
                }
            }
            return vResult;
        }

        private string CalcularTotalDevolucion(string valPrcDescuento, string valMonto, string valCantidad) {
            string vResult = "";
            decimal vRenglonDescuento = 0;
            decimal vMontoDescuento = 0;            
            vRenglonDescuento = LibImportData.ToDec(valCantidad) * LibImportData.ToDec(valMonto) * LibImportData.ToDec(valPrcDescuento) / 100;
            vMontoDescuento = LibImportData.ToDec(valMonto) - vRenglonDescuento;
            vResult = LibImpresoraFiscalUtil.DecimalToStringFormat(vMontoDescuento, 2);            
            return vResult;
        }

        private string SetDecimalSeparator(string valNumero) {
            string vResult = "";
            if (LibText.IndexOf(valNumero, ',') > 0) {
                vResult = LibText.Replace(valNumero, ",", ".");
            } else {
                vResult = valNumero;
            }
            return vResult;
        }

        private string DarFormatoAAlicuotaIva(eTipoDeAlicuota valTipoAlicuota) {
            string vValorFinal = "";

            switch (valTipoAlicuota) {

                case eTipoDeAlicuota.AlicuotaGeneral:
                    vValorFinal = "1";
                    break;
                case eTipoDeAlicuota.Alicuota2:
                    vValorFinal = "2";
                    break;
                case eTipoDeAlicuota.Alicuota3:
                    vValorFinal = "3";
                    break;
                case eTipoDeAlicuota.Exento:
                    vValorFinal = "4";
                    break;
            }
            return vValorFinal;
        }

        private string DarFormatoNumericoParaImpresion(string valNumero) {
            string vResult = "";
            int vTokenPosition = 0;
            string vParteEntera = "";
            string vParteDecimal = "";

            vResult = SetDecimalSeparator(valNumero);
            vTokenPosition = LibText.IndexOf(vResult, ".");
            if (vTokenPosition > 0) {
                vParteEntera = LibText.Left(vResult, vTokenPosition);
                vParteDecimal = LibText.Right(vResult, LibText.Len(vResult) - vTokenPosition - 1);
                vParteDecimal = LibText.FillWithCharToRight(vParteDecimal, "0", 2);
                vResult = vParteEntera + "," + vParteDecimal;
            } else {
                vResult = vResult + "," + "00";
            }
            return vResult;
        }       

        string DarFormatoParaTextoQPrint(string vTextoEntrada) {
            string vResult = "";
            vResult = _LiteralDeTexto + vTextoEntrada + _LiteralDeTexto;
            return vResult;
        }

        private bool EnviarPagos(XElement valMedioDePago) {
            string vMedioDePago = "";
            string vMontoCancelado = "";
            string vMensaje = "";
            bool vResult = false;
            int vRequets = 0;
            string vCmd = "";
            string vTexto = "";

            try {
                List<XElement> vNodos = valMedioDePago.Descendants("GpResultDetailRenglonCobro").ToList();
                if (vNodos.Count > 0) {
                    vCmd = _FacturaSubTotal;
                    vRequets = SendCMD(vCmd, ref vMensaje);
                    vResult = CheckRequest(vRequets, ref vMensaje);

                    foreach (XElement vXElement in vNodos) {
                        vMedioDePago = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "CodigoFormaDelCobro"));
                        vMontoCancelado = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "Monto"));
                        vTexto = FormaDeCobro(vMedioDePago);
                        vMontoCancelado = DarFormatoNumericoParaImpresion(vMontoCancelado);
                        vCmd = _CerrarFacturaEnviaPagos + vMontoCancelado + _SeparadorDeCampos + ValorFormaDeCobro(vMedioDePago) + _SeparadorDeCampos + DarFormatoParaTextoQPrint(vTexto) + LibText.FillWithCharToRight("",_SeparadorDeCampos,8);
                        vRequets = SendCMD(vCmd, ref vMensaje);
                        vResult &= CheckRequest(vRequets, ref vMensaje);
                    }
                }
                return vResult;
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private string FormaDeCobro(string valFormaDeCobro) {
            string vResultado = "";
            switch (valFormaDeCobro) {
                case "00001":
                    vResultado = "Efectivo";
                    break;
                case "00002":
                    vResultado = "Tarjeta";
                    break;
                case "00003":
                    vResultado = "Cheque";
                    break;
                case "00004":
                    vResultado = "Depósito";
                    break;
                case "00005":
                    vResultado = "Anticipo";
                    break;
                default:
                    vResultado = "Efectivo";
                    break;
            }
            return vResultado;
        }

        public bool ImprimirNotaCredito(XElement vDocumentoFiscal) {
            string vDireccion = LibXml.GetPropertyString(vDocumentoFiscal, "Direccion");
            string vRif = LibXml.GetPropertyString(vDocumentoFiscal, "RIF");
            string vRazonSocial = LibXml.GetPropertyString(vDocumentoFiscal, "RAZON_SOCIAL");
            string vObservaciones = LibXml.GetPropertyString(vDocumentoFiscal, "OBSERVACION");
            string vFacturaAfectada = LibXml.GetPropertyString(vDocumentoFiscal, "NUMERO_FACTURA_AFECTADA");
            string vSerialMaquina = LibXml.GetPropertyString(vDocumentoFiscal, "SERIAL_MAQUINA");
            string vFecha = LibXml.GetPropertyString(vDocumentoFiscal, "FECHA");
            string vHora = LibXml.GetPropertyString(vDocumentoFiscal, "HORA");
            vFecha = LibGalac.Aos.Base.LibString.Replace(vFecha, "/", "/");
            vHora = LibGalac.Aos.Base.LibString.Replace(vHora, "/", "/");

            string vTipoFactura = "D";
            /*
            try {
            mVMax.AbrirPuerto();
            AbrirComprobanteFiscal(ref vRazonSocial, ref vRif, ref vTipoFactura, ref vFacturaAfectada, ref vSerialMaquina, ref vFecha, ref vFecha, vDireccion, vObservaciones);
            ImprimirTodosLosArticulos(vDocumentoFiscal, true);
            mVMax.CerrarCF();
            CerrarConexion();
            Thread.Sleep(800);
            return true;
            } catch (Exception vEx) {
            CancelarDocumentoFiscalEnImpresion(false);
            throw new LibGalac.Aos.Catching.GalacException("Imprimir Venta", vEx);
            }
            */
            return false;
        }

        private bool EnviarDatosAdicionales(string valTextoEntrada) {
            string vCommand = "";
            bool vResult = false;
            string vTrama = "";
            string vTextoEnviar = "";
            string vMensaje = "";
            int vIndiceInicio = 0;
            int vIndiceFinal = 0;
            int vNumeroCaracteresPorEnviar = 0;
            int vRequest = 0;
            string StringField = "\"";

            try {
                vIndiceInicio = 1;
                vNumeroCaracteresPorEnviar = LibString.Len(valTextoEntrada);
                while (vNumeroCaracteresPorEnviar > 0 && vIndiceInicio <= LibString.Len(valTextoEntrada)) {
                    if (LibString.Len(valTextoEntrada) - vIndiceInicio > 24) {
                        vIndiceFinal = vIndiceInicio + 24;
                    } else {
                        vIndiceFinal = LibString.Len(valTextoEntrada) + 1;
                    }
                    vTextoEnviar = LibString.SubString(valTextoEntrada, vIndiceInicio, vIndiceFinal - vIndiceInicio);
                    vCommand = "U4000;1;" + StringField + vTextoEnviar + StringField;
                    vRequest = SendCMD(vCommand, ref  vTrama);
                    vResult = CheckRequest(vRequest, ref vMensaje);
                    vCommand = "U5;4001;";
                    vRequest = SendCMD(vCommand, ref vTrama);
                    vResult &= CheckRequest(vRequest, ref vMensaje);
                    vNumeroCaracteresPorEnviar = vNumeroCaracteresPorEnviar - (vIndiceFinal - vIndiceInicio);
                    vIndiceInicio = vIndiceInicio + 24;
                }
                return vResult;
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private string LeerRepuestaImpFiscal(string valDatos, int valItemCampo) {
            string vResult = "";
            char vSeparator = '|';
            string[] vListTrama = LibString.Split(valDatos, vSeparator);
            vResult = vListTrama[valItemCampo];
            return vResult;
        }

        private bool CheckRequest(int valSendRequest, ref string vMensaje) {
            bool vResult = true;

            switch (valSendRequest) {
                case 0:
                    vResult = true;
                    vMensaje = "Satisfactorio";
                    break;
                case -1:
                    vResult = false;
                    vMensaje = "Accion no Ejecutada";
                    break;
                default:
                    vResult = false;
                    vMensaje = "Error de Comando";
                    break;
            }
            return vResult;
        }

        unsafe static int SendCMD(string cmd, ref string trama) {
            byte* mytrama = stackalloc byte[1024];
            int error = SendCommand(cmd, mytrama);
            trama = new string((sbyte*)mytrama);
            return error;
        }

        unsafe static int GetVersionLib(out string ver) {
            byte* myvers = stackalloc byte[30];
            int error = GetVer(myvers);
            ver = new string((sbyte*)myvers);
            return error;
        }


        public bool ReimprimirFactura(string valDesde, string valHasta) {
            short vModo = 0;
            short vTipo = 0;
            return true;
        }

        public bool ReimprimirNotaDeCredito(string valDesde, string valHasta) {
            short vModo = 0;
            short vTipo = 1;
            return true;
        }

        public bool ReimprimirReporteZ(string valDesde, string valHasta) {
            short vModo = 0;
            short vTipo = 2;
            return true;
        }

        public bool ReimprimirReporteX(string valDesde, string valHasta) {
            short vModo = 0;
            short vTipo = 4;
            return true;
        }

        public bool ReimprimirDocumentoNoFiscal(string valDesde, string valHasta) {
            short vModo = 0;
            short vTipo = 3;
            return true;
        }

        bool IImpresoraFiscalPdn.ReimprimirDocumentoFiscal(string valDesde, string valHasta, string valTipo) {
            bool vResult = false;
            eTipoDocumentoFiscal TipoDeDocumento = (eTipoDocumentoFiscal)LibConvert.DbValueToEnum(valTipo);

            switch (TipoDeDocumento) {
                case eTipoDocumentoFiscal.FacturaFiscal:
                    vResult = ReimprimirFactura(valDesde, valHasta);
                    break;
                case eTipoDocumentoFiscal.NotadeCredito:
                    vResult = ReimprimirNotaDeCredito(valDesde, valHasta);
                    break;
                case eTipoDocumentoFiscal.ReporteX:
                    vResult = ReimprimirReporteX(valDesde, valHasta);
                    break;
                case eTipoDocumentoFiscal.ReporteZ:
                    vResult = ReimprimirReporteZ(valDesde, valHasta);
                    break;
            }
            return vResult;
        }

        
        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            return false;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            return false;
        }

        XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            return null;
        }

        public IFDiagnostico RealizarDiagnostico(bool valAbrirPuerto = false) {
            throw new NotImplementedException();
        }

        public bool EstatusDeComunicacion(IFDiagnostico vDiagnostico) {
            throw new NotImplementedException();
        }

        public bool VersionDeControladores(IFDiagnostico vDiagnostico) {
            throw new NotImplementedException();
        }

        public bool AlicuotasRegistradas(IFDiagnostico vDiagnostico) {
            throw new NotImplementedException();
        }

        public bool FechaYHora(IFDiagnostico vDiagnostico) {
            throw new NotImplementedException();
        }

        public bool ColaDeImpresion(IFDiagnostico vDiagnostico) {
            throw new NotImplementedException();
        }

        public bool ConsultarConfiguracion(IFDiagnostico iFDiagnostico) {
            throw new NotImplementedException();
        }
    }
}
