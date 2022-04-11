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
using System.Reflection;
using LibGalac.Aos.Cnf;

namespace Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal {
    public class clsTheFactory : IImpresoraFiscalPdn {
        #region constantes
        const string VersionApi = "1.7.6.2";
        const string DllApiName = @"TfhkaNet.dll";
        const string _CierreFacturaIGTF = "199";
        #endregion

        #region variables
        #region Formatos_Numericos
        int _EnterosParaCantidad;
        int _DecimalesParaCantidad;
        int _EnterosParaMonto;
        int _DecimalesParaMonto;
        int _EnterosParaPagos;
        int _DecimalesParaPagos;
        int _EnterosParaDescuento;
        int _DecimalesParaDescuento;
        bool _EstaActivoFlag21 = false;
        bool _EstaActivoFlag50 = false;
        #endregion
        #region Longitud de Impresion
        int _LineaTextoAdicional;
        bool _ExtenderLineasAdicionales;
        #endregion  
        private string _CommPort = "";
        private Tfhka _TfhkPrinter;
        private eImpresoraFiscal _ModeloFactory;
        bool _ModelosAntiguos = false;
        bool _ModeloSoportaComandosGenerales = false;
        bool _FormatoFirmwareTipo1 = false;
        bool _FormatoFirmwareTipo2 = false;
        bool _ModeloUsaFlags = false;
        string[] _ListPrintersNotSupport;
        bool _PortIsOpen;
        int _MaxLongitudDeTexto = 0;
        #endregion

        public clsTheFactory(XElement valXmlDatosImpresora) {
            _TfhkPrinter = new Tfhka();
            _LineaTextoAdicional = 0;
            ConfigurarImpresora(valXmlDatosImpresora);
            _ListPrintersNotSupport = new string[] {
                    "Z1B"
                };
        }

        private void ConfigurarImpresora(XElement valXmlDatosImpresora) {
            try {// Pendiente  Bixlon350, HASAR HSP-7000, Qustom Kube
                ePuerto ePuerto = (ePuerto)LibConvert.DbValueToEnum(LibXml.GetPropertyString(valXmlDatosImpresora, "PuertoMaquinaFiscal"));
                _CommPort = ePuerto.GetDescription(0);
                _ModeloFactory = (eImpresoraFiscal)LibConvert.DbValueToEnum(LibImpresoraFiscalUtil.ObtenerValorDesdeXML(valXmlDatosImpresora, "ModeloDeMaquinaFiscal"));
                _ModelosAntiguos = (_ModeloFactory == eImpresoraFiscal.BIXOLON270 || _ModeloFactory == eImpresoraFiscal.OKIML1120 || _ModeloFactory == eImpresoraFiscal.BIXOLON350 || _ModeloFactory == eImpresoraFiscal.ACLASPP1F3 || _ModeloFactory == eImpresoraFiscal.HKA112);
                _MaxLongitudDeTexto = (_ModelosAntiguos ? 30 : 40);
                //                
                _ModeloSoportaComandosGenerales = (_ModeloFactory == eImpresoraFiscal.DASCOMTALLY1125 || _ModeloFactory == eImpresoraFiscal.BIXOLON812 || _ModeloFactory == eImpresoraFiscal.HKA80 || _ModeloFactory == eImpresoraFiscal.ACLASPP9 || _ModeloFactory == eImpresoraFiscal.DASCOMTALLYDT230 || _ModeloFactory == eImpresoraFiscal.DASCOMTALLY1140);
                // Pendiente por Agregar PANTUM PDL3100
                _ModeloUsaFlags = (_ModeloFactory == eImpresoraFiscal.DASCOMTALLY1125 || _ModeloFactory == eImpresoraFiscal.BIXOLON812 || _ModeloFactory == eImpresoraFiscal.DASCOMTALLYDT230 || _ModeloFactory == eImpresoraFiscal.HKA80 || _ModeloFactory == eImpresoraFiscal.ACLASPP9 || _ModeloFactory == eImpresoraFiscal.DASCOMTALLY1140);
                //                
                _FormatoFirmwareTipo1 = (_ModeloFactory == eImpresoraFiscal.DASCOMTALLY1125);
                //
                _FormatoFirmwareTipo2 = (_ModeloFactory == eImpresoraFiscal.BIXOLON812 || _ModeloFactory == eImpresoraFiscal.DASCOMTALLYDT230 || _ModeloFactory == eImpresoraFiscal.HKA80 || _ModeloFactory == eImpresoraFiscal.ACLASPP9 || _ModeloFactory == eImpresoraFiscal.DASCOMTALLY1140);
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        private bool FlagEstaActivo(int valItemflag, int valValFlag) {
            bool vResult = false;
            int vFlagValue = 0;
            S3PrinterData Report3 = _TfhkPrinter.GetS3PrinterData();
            int[] ListFlag = Report3.AllSystemFlags;
            vFlagValue = ListFlag[valItemflag];
            vResult = (vFlagValue == valValFlag);
            return vResult;
        }

        private void AjustarFormatosNumericosSegunModelo() {
            if (_ModeloUsaFlags) {
                _EstaActivoFlag21 = FlagEstaActivo(21, 30); //Flag21=30-> firmware actualizado soporta montos altos
                _ModeloSoportaComandosGenerales &= !_EstaActivoFlag21;
                if (_FormatoFirmwareTipo2) {
                    _EstaActivoFlag50 = FlagEstaActivo(50, 1); //Flag50=1-> firmware actualizado soporta montos altos / Maneja IGTF
                }
            }
            _EnterosParaDescuento = 2;
            _DecimalesParaDescuento = 2;
            string vPersonalizar = LibAppSettings.ReadAppSettingsKey("PersonalizarTheFactory");
            if (LibString.Len(vPersonalizar) > 0) {
                string[] vTheFactoryStt = LibString.Split(vPersonalizar, ';');
                if (vTheFactoryStt.Count() == 7) {
                    _EnterosParaCantidad = LibConvert.ToInt(vTheFactoryStt[0]);
                    _DecimalesParaCantidad = LibConvert.ToInt(vTheFactoryStt[1]);
                    _EnterosParaMonto = LibConvert.ToInt(vTheFactoryStt[2]);
                    _DecimalesParaMonto = LibConvert.ToInt(vTheFactoryStt[3]);
                    _EnterosParaPagos = LibConvert.ToInt(vTheFactoryStt[4]);
                    _DecimalesParaPagos = LibConvert.ToInt(vTheFactoryStt[5]);
                    _ModeloSoportaComandosGenerales = LibConvert.SNToBool(vTheFactoryStt[6]);
                    return;
                }
            }
            if (_FormatoFirmwareTipo1 && _EstaActivoFlag21) {
                _EnterosParaCantidad = 14;
                _DecimalesParaCantidad = 3; //Para Comandos Tradicionales, Firmware1 Actualizado
                _EnterosParaMonto = 14;
                _DecimalesParaMonto = 2;
                _EnterosParaPagos = 15;
                _DecimalesParaPagos = 2;
                _ModeloSoportaComandosGenerales = false;
            } else if (_FormatoFirmwareTipo1 && _ModeloSoportaComandosGenerales) {
                _EnterosParaCantidad = 7;
                _DecimalesParaCantidad = 3; //Para Comandos Generales, Firmware1 Actualizado
                _EnterosParaMonto = 10;
                _DecimalesParaMonto = 2;
                _EnterosParaPagos = 10;
                _DecimalesParaPagos = 2;
            } else if (_FormatoFirmwareTipo2 && _EstaActivoFlag21) {
                _EnterosParaCantidad = 14;
                _DecimalesParaCantidad = 3;
                _EnterosParaMonto = 14;  //Para Comandos Tradicionles, Firmware2 Actualizado
                _DecimalesParaMonto = 2;
                _EnterosParaPagos = 15;
                _DecimalesParaPagos = 2;
                _ModeloSoportaComandosGenerales = false;
            } else if (_FormatoFirmwareTipo2 && _ModeloSoportaComandosGenerales) {
                _EnterosParaCantidad = 14;
                _DecimalesParaCantidad = 3;
                _EnterosParaMonto = 14;  //Para Comandos Generales, Firmware2 Actualizado
                _DecimalesParaMonto = 2;
                _EnterosParaPagos = 10;
                _DecimalesParaPagos = 2;
            } else if (_ModeloSoportaComandosGenerales) {
                _EnterosParaCantidad = 14;
                _DecimalesParaCantidad = 3;
                _EnterosParaMonto = 14; // Firmware Anterior - Comandos Generales
                _DecimalesParaMonto = 2;
                _EnterosParaPagos = 10;
                _DecimalesParaPagos = 2;
            } else {
                _EnterosParaCantidad = 5;
                _DecimalesParaCantidad = 3; // Firmware Anterior - Comandos Tradicionales
                _EnterosParaMonto = 8;
                _DecimalesParaMonto = 2;
                _EnterosParaPagos = 10;
                _DecimalesParaPagos = 2;
                _ModeloSoportaComandosGenerales = false;
            }
        }

        public bool AbrirConexion() {
            bool vResult = false;
            try {
                if (!_PortIsOpen) {
                    if (!_TfhkPrinter.StatusPort) {
                        vResult = _TfhkPrinter.OpenFpCtrl(_CommPort);
                        Thread.Sleep(350);
                        _PortIsOpen = true;
                    }
                } else {
                    vResult = _PortIsOpen;
                }
                return vResult;
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Abrir Conexión\r\n" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool CerrarConexion() {
            bool vResult = false;
            try {
                if (_PortIsOpen) {
                    _TfhkPrinter.CloseFpCtrl();
                    Thread.Sleep(350);
                    vResult = true;
                    _PortIsOpen = false;
                } else {
                    return true;
                }
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Cerrar Conexión\r\n" + vEx.Message, eExceptionManagementType.Controlled);
            }
            return vResult;
        }

        public bool ComprobarEstado() {
            bool vResult = false;
            Thread.Sleep(250);
            vResult = _TfhkPrinter.StatusPort;
            return vResult;
        }

        public string ObtenerSerial(bool valAbrirConexion) {
            string vSerial = "";
            S1PrinterData _Estado;
            try {
                if (valAbrirConexion || !_TfhkPrinter.StatusPort) {
                    AbrirConexion();
                }
                _Estado = _TfhkPrinter.GetS1PrinterData();
                vSerial = _Estado.RegisteredMachineNumber;
                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vSerial;
            } catch (Exception vEx) {
                throw new GalacException("No se pudo obtener serial, verifique puertos y conexiones\r\n" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerFechaYHora() {
            string vResult = "";
            string vTiempo = "";
            string vFecha = "";
            S1PrinterData _Estado;
            try {
                _Estado = _TfhkPrinter.GetS1PrinterData();
                DateTime vDateTime = _Estado.CurrentPrinterDateTime;
                vFecha = LibConvert.ToStr(vDateTime, "dd/MM/yy");
                vTiempo = LibConvert.ToStrOnlyForHour(vDateTime, "hh:mm");
                vResult = vFecha + LibText.Space(1) + vTiempo;
                return vResult;
            } catch (GalacException vEx) {
                CerrarConexion();
                throw new LibGalac.Aos.Catching.GalacException("Obtener Fecha y Hora\r\n" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public eStatusImpresorasFiscales EstadoDelPapel(bool valAbrirPuerto) {
            eStatusImpresorasFiscales vResult;
            try {
                if (valAbrirPuerto) {
                    AbrirConexion();
                }
                Thread.Sleep(150);
                PrinterStatus ifEstatus = _TfhkPrinter.GetPrinterStatus();
                vResult = (eStatusImpresorasFiscales)ifEstatus.PrinterErrorCode;
                if (valAbrirPuerto) {
                    CerrarConexion();
                }
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Estado del Papel\r\n" + vEx.Message, eExceptionManagementType.Controlled);
            }
            return vResult;
        }

        public bool RealizarReporteZ() {
            bool vResult = false;
            PrinterStatus PrStatus;
            try {
                if (AbrirConexion()) {
                    _TfhkPrinter.PrintZReport();
                    do {
                        PrStatus = _TfhkPrinter.GetPrinterStatus();
                    } while (PrStatus.PrinterStatusCode != 4);
                    Thread.Sleep(350);
                    vResult = true;
                    CerrarConexion();
                }
                return vResult;
            } catch (GalacException vEx) {
                CerrarConexion();
                throw new LibGalac.Aos.Catching.GalacException("Realizar Reporte Z\r\n" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool RealizarReporteX() {
            bool vResult = false;
            try {
                if (AbrirConexion()) {
                    _TfhkPrinter.PrintXReport();
                    vResult = true;
                    CerrarConexion();
                }
                return vResult;
            } catch (GalacException vEx) {
                CerrarConexion();
                throw new LibGalac.Aos.Catching.GalacException("Realizar Cerrre X\r\n" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerUltimoNumeroFactura(bool valAbrirConexion) {
            string vUltimaFactura = "";
            S1PrinterData _Estado;
            try {
                if (valAbrirConexion || !_TfhkPrinter.StatusPort) {
                    AbrirConexion();
                }
                _Estado = _TfhkPrinter.GetS1PrinterData();
                vUltimaFactura = LibText.FillWithCharToLeft(LibConvert.ToStr(_Estado.LastInvoiceNumber), "0", 8);
                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimaFactura;
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Obtener último Numero de factura\r\n" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerUltimoNumeroNotaDeCredito(bool valAbrirConexion) {
            string vUltimoNotaCredito = "";
            S1PrinterData _Estado;
            try {
                if (valAbrirConexion || !_TfhkPrinter.StatusPort) {
                    AbrirConexion();
                }
                _Estado = _TfhkPrinter.GetS1PrinterData();
                vUltimoNotaCredito = LibText.FillWithCharToLeft(LibConvert.ToStr(_Estado.LastCreditNoteNumber), "0", 8);
                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimoNotaCredito;
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Obtener último Numero de Nota de Crédito\r\n" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private bool PrinterNotSupport() {
            bool vResult = false;
            S1PrinterData _Estados1;
            string vSerial = "";
            _Estados1 = _TfhkPrinter.GetS1PrinterData();
            vSerial = _Estados1.RegisteredMachineNumber;
            vSerial = LibString.Left(vSerial, 3);
            vResult = _ListPrintersNotSupport.Any(t => t.Contains(vSerial));
            return vResult;
        }

        public string ObtenerUltimoNumeroReporteZ(bool valAbrirConexion) {
            string vUltimoNumero = "";
            ReportData estado;
            S1PrinterData _Estados1;
            PrinterStatus PrStatus;
            bool vPrinterNotSupport = false;

            try {
                if (valAbrirConexion || !_TfhkPrinter.StatusPort) {
                    AbrirConexion();
                }
                vPrinterNotSupport = PrinterNotSupport();

                if (vPrinterNotSupport) {
                    _Estados1 = _TfhkPrinter.GetS1PrinterData();
                    vUltimoNumero = LibText.FillWithCharToLeft(LibConvert.ToStr(_Estados1.DailyClosureCounter), "0", 8);
                } else {
                    do {
                        PrStatus = _TfhkPrinter.GetPrinterStatus();
                    } while (PrStatus.PrinterStatusCode != 4);
                    estado = _TfhkPrinter.GetZReport();
                    vUltimoNumero = LibConvert.ToStr(estado.NumberOfLastZReport);
                    vUltimoNumero = LibText.FillWithCharToLeft(vUltimoNumero, "0", 8);
                }
                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimoNumero;
            } catch (Exception) {
                return "0000";
            }
        }

        public bool CancelarDocumentoFiscalEnImpresion(bool valAbrirConexion) {
            bool vResult = false;
            string vCMD = "7";
            try {
                if (valAbrirConexion || !_TfhkPrinter.StatusPort) {
                    AbrirConexion();
                }
                vResult = _TfhkPrinter.SendCmd(vCMD);
                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vResult;
            } catch (Exception vEx) {
                throw new GalacException("Cancelar Documento Fiscal en Cola\r\n" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private void ImprimeSerialyRollo(string valSerial, string valRollo) {
            string vCmd;
            try {
                if (!valSerial.Equals("")) {
                    vCmd = LibText.Left("@" + valSerial, 40);
                    _TfhkPrinter.SendCmd(vCmd);
                }

                if (!valRollo.Equals("")) {
                    vCmd = LibText.Left("@" + valRollo, 40);
                    _TfhkPrinter.SendCmd(vCmd);
                }
                Thread.Sleep(100);
            } catch (PrinterException vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Imprimir Serial y Rollo\r\n" + vEx.Message, LibGalac.Aos.Catching.eExceptionManagementType.Controlled);
            }
        }

        private string DarFormatoAAlicuotaIva(eTipoDeAlicuota valTipoAlicuota) {
            string vValorFinal = "";
            if (!_ModeloSoportaComandosGenerales) {
                switch (valTipoAlicuota) {
                    case eTipoDeAlicuota.Exento:
                        vValorFinal = "\u0020";// Space
                        break;
                    case eTipoDeAlicuota.AlicuotaGeneral:
                        vValorFinal = "\u0021";// !
                        break;
                    case eTipoDeAlicuota.Alicuota2:
                        vValorFinal = "\u0022";// "
                        break;
                    case eTipoDeAlicuota.Alicuota3:
                        vValorFinal = "\u0023";// #
                        break;
                    default:
                        vValorFinal = "\u0021";// !
                        break;
                }
            } else {
                vValorFinal = DarFormatoAAlicuotaIvaNC(valTipoAlicuota);
            }
            return vValorFinal;
        }

        private string DarFormatoAAlicuotaIvaNC(eTipoDeAlicuota valTipoAlicuota) {
            string vValorFinal = "";
            switch (valTipoAlicuota) {
                case eTipoDeAlicuota.Exento:
                    vValorFinal = "0";
                    break;
                case eTipoDeAlicuota.AlicuotaGeneral:
                    vValorFinal = "1";
                    break;
                case eTipoDeAlicuota.Alicuota2:
                    vValorFinal = "2";
                    break;
                case eTipoDeAlicuota.Alicuota3:
                    vValorFinal = "3";
                    break;
                default:
                    vValorFinal = "1";
                    break;
            }
            return vValorFinal;
        }

        private string DarFormatoNumericoParaComandosGenerales(string valNumero, int valCantidadEnteros) {
            string vResult = "";
            vResult = LibString.InsertAt(valNumero, ",", valCantidadEnteros);
            return vResult;
        }

        public bool ImprimirNotaCredito(XElement valDocumentoFiscal) {
            string vDireccion = LibXml.GetPropertyString(valDocumentoFiscal, "DireccionCliente");
            string vRif = LibXml.GetPropertyString(valDocumentoFiscal, "NumeroRIF");
            string vRazonSocial = LibXml.GetPropertyString(valDocumentoFiscal, "NombreCliente");
            string vTelefono = LibXml.GetPropertyString(valDocumentoFiscal, "TelefonoCliente");
            string vObservaciones = LibXml.GetPropertyString(valDocumentoFiscal, "Observaciones");
            string vNumeroComprobante = LibXml.GetPropertyString(valDocumentoFiscal, "NumeroComprobanteFiscal");
            string vSerialMaquina = LibXml.GetPropertyString(valDocumentoFiscal, "SerialMaquinaFiscal");
            string vFecha = LibXml.GetPropertyString(valDocumentoFiscal, "Fecha");
            string vHora = LibXml.GetPropertyString(valDocumentoFiscal, "HoraModificacion");

            bool vResult = false;
            try {
                List<XElement> vCamposDefinibles = valDocumentoFiscal.Descendants("GpResultDetailCamposDefinibles").ToList();
                _ExtenderLineasAdicionales = LibString.Len(LibXml.GetPropertyString(valDocumentoFiscal, "TotalMonedaExtranjera")) > 0 || vCamposDefinibles.Count > 0;
                if (!_TfhkPrinter.StatusPort) {
                    AbrirConexion();
                }
                AjustarFormatosNumericosSegunModelo();
                if (AbrirNotaDeCredito(vRazonSocial, vRif, vDireccion, vTelefono, vObservaciones, vNumeroComprobante, vSerialMaquina, vFecha, vHora)) {
                    vResult = ImprimirTodosLosArticulos(valDocumentoFiscal, true);
                    vResult &= CerrarComprobanteFiscal(valDocumentoFiscal, true);
                }
                if (_TfhkPrinter.StatusPort) {
                    CerrarConexion();
                }
                Thread.Sleep(250);
            } catch (Exception vEx) {
                vResult = false;
                throw vEx;
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
                List<XElement> vCamposDefinibles = valDocumentoFiscal.Descendants("GpResultDetailCamposDefinibles").ToList();
                _ExtenderLineasAdicionales = LibString.Len(LibXml.GetPropertyString(valDocumentoFiscal, "TotalMonedaExtranjera")) > 0 || vCamposDefinibles.Count > 0;
                if (!_TfhkPrinter.StatusPort) {
                    AbrirConexion();
                }
                AjustarFormatosNumericosSegunModelo();
                if (AbrirComprobanteFiscal(vRazonSocial, vRif, vDireccion, vTelefono, vObservaciones)) {
                    vResult = ImprimirTodosLosArticulos(valDocumentoFiscal, false);
                    vResult &= CerrarComprobanteFiscal(valDocumentoFiscal, false);
                }
                if (_TfhkPrinter.StatusPort) {
                    CerrarConexion();
                }
                Thread.Sleep(250);
            } catch (Exception vEx) {
                vResult = false;
                throw vEx;
            }
            return vResult;
        }

        private bool ImprimirTodosLosArticulos(XElement valDocumentoFiscal, bool valIsNotaDeCredito) {
            bool vEstatus = true;
            string vDescripcion;
            string vCantidad;
            string vMonto;
            string vTipoTasa;
            string vPrcDescuento;
            string vSerial;
            string vRollo;
            try {
                List<XElement> vRecord = valDocumentoFiscal.Descendants("GpResultDetailRenglonFactura").ToList();
                foreach (XElement vXElement in vRecord) {
                    vDescripcion = LibXml.GetElementValueOrEmpty(vXElement, "Descripcion");
                    vCantidad = LibXml.GetElementValueOrEmpty(vXElement, "Cantidad");
                    vCantidad = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vCantidad, _EnterosParaCantidad, _DecimalesParaCantidad);
                    vMonto = LibXml.GetElementValueOrEmpty(vXElement, "PrecioSinIVA");
                    vMonto = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vMonto, _EnterosParaMonto, _DecimalesParaMonto);
                    vTipoTasa = LibXml.GetElementValueOrEmpty(vXElement, "AlicuotaIva");
                    vPrcDescuento = LibXml.GetElementValueOrEmpty(vXElement, "PorcentajeDescuento");
                    vPrcDescuento = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vPrcDescuento, _EnterosParaDescuento, _DecimalesParaDescuento);
                    vSerial = LibXml.GetElementValueOrEmpty(vXElement, "Serial");
                    vRollo = LibXml.GetElementValueOrEmpty(vXElement, "Rollo");
                    if (valIsNotaDeCredito) {
                        vTipoTasa = DarFormatoAAlicuotaIvaNC((eTipoDeAlicuota)LibConvert.DbValueToEnum(vTipoTasa));
                    } else {
                        vTipoTasa = DarFormatoAAlicuotaIva((eTipoDeAlicuota)LibConvert.DbValueToEnum(vTipoTasa));
                    }
                    ImprimeSerialyRollo(vSerial, vRollo);
                    if (!ImprimirArticuloVenta(vDescripcion, vCantidad, vMonto, vTipoTasa, vPrcDescuento, valIsNotaDeCredito)) {
                        vEstatus &= false;
                        throw new Exception("Documento no impreso");
                    }
                    vEstatus &= true;
                }
                return vEstatus;
            } catch (Exception vEx) {
                CancelarDocumentoFiscalEnImpresion(false);
                CerrarConexion();
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private bool CerrarComprobanteFiscal(XElement valDocumentoFiscal, bool valIsNotaDeCredito) {
            bool vEstatus = true;
            string vCmd = "";
            string vTotalesEnDivisa;
            string vDescuentoTotal = LibXml.GetPropertyString(valDocumentoFiscal, "PorcentajeDescuento");
            vDescuentoTotal = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vDescuentoTotal, _EnterosParaDescuento, _DecimalesParaDescuento);
            vTotalesEnDivisa = LibXml.GetPropertyString(valDocumentoFiscal, "TotalMonedaExtranjera");
            if (LibText.Len(vTotalesEnDivisa) > 0 && !_ModelosAntiguos) {
                vEstatus = ImprimirTotalesEnDivisas(vTotalesEnDivisa);
            } else if (!valIsNotaDeCredito && LibString.Len(vTotalesEnDivisa) == 0) {
                vEstatus = ImprmirCamposDefinibles(valDocumentoFiscal);
            }
            if (LibConvert.ToInt(vDescuentoTotal) != 0) {
                vCmd = "3"; //SubTotal
                vEstatus &= _TfhkPrinter.SendCmd(vCmd);
                if (_ModeloSoportaComandosGenerales) {
                    vDescuentoTotal = DarFormatoNumericoParaComandosGenerales(vDescuentoTotal, _EnterosParaDescuento);
                    vCmd = "GP-*" + vDescuentoTotal;
                } else {
                    vCmd = "p-" + vDescuentoTotal;
                }
                vEstatus &= _TfhkPrinter.SendCmd(vCmd);
            }
            vEstatus &= EnviarPagos(valDocumentoFiscal);
            return vEstatus;
        }

        private bool ImprimirTotalesEnDivisas(string vTotales) {
            bool vResult = true;
            string vCmd = "";
            string[] vList = LibString.Split(vTotales, '\n');
            foreach (string vText in vList) {
                vCmd = GetLineaTexto() + vText;
                vResult = _TfhkPrinter.SendCmd(vCmd);
                if (_LineaTextoAdicional >= 10) {
                    break;
                }
            }
            return vResult;
        }

        private bool ImprimirArticuloVenta(string valDescripcion, string valCantidad, string valPrecio, string valTipoTasa, string valPorcetajeDesRenglon, bool valEsDevolucion) {
            bool vResult = true;
            string vCmd = "";
            string vSeparador = new string('\u007c', 2);

            if (_ModeloSoportaComandosGenerales) {
                valCantidad = DarFormatoNumericoParaComandosGenerales(valCantidad, _EnterosParaCantidad);
                valPrecio = DarFormatoNumericoParaComandosGenerales(valPrecio, _EnterosParaMonto);
                if (valEsDevolucion) {
                    vCmd = "GC+" + valTipoTasa;
                } else {
                    vCmd = "GF+" + valTipoTasa;
                }
                vCmd = vCmd + valPrecio + vSeparador;
                vCmd = vCmd + valCantidad + vSeparador;
                vCmd = vCmd + LibText.Left(valDescripcion, 105);
                vResult = _TfhkPrinter.SendCmd(vCmd);
                Thread.Sleep(600);
                if (LibConvert.ToInt(valPorcetajeDesRenglon) > 0) {
                    vCmd = "GP-*" + DarFormatoNumericoParaComandosGenerales(valPorcetajeDesRenglon, _EnterosParaDescuento);
                    vResult &= _TfhkPrinter.SendCmd(vCmd);
                }

            } else {
                if (valEsDevolucion) {
                    vCmd = "d" + valTipoTasa;
                } else {
                    vCmd = valTipoTasa;
                }
                vCmd = vCmd + valPrecio;
                vCmd = vCmd + valCantidad;
                vCmd = vCmd + LibText.Left(valDescripcion, 105);
                vResult = _TfhkPrinter.SendCmd(vCmd);
                Thread.Sleep(600);
                if (LibConvert.ToInt(valPorcetajeDesRenglon) > 0) {
                    vCmd = "p-" + valPorcetajeDesRenglon;
                    vResult &= _TfhkPrinter.SendCmd(vCmd);
                }
            }
            return vResult;
        }

        private bool ImprmirCamposDefinibles(XElement valData) {
            bool vResult = true;
            string vCmd;
            List<XElement> vCamposDefinibles = valData.Descendants("GpResultDetailCamposDefinibles").ToList();
            if (vCamposDefinibles.Count > 0) {
                foreach (XElement vRecord in vCamposDefinibles) {
                    vCmd = GetLineaTexto() + LibXml.GetElementValueOrEmpty(vRecord, "CampoDefinibleValue");
                    vResult |= _TfhkPrinter.SendCmd(vCmd);
                    if (_LineaTextoAdicional >= 10) {
                        break;
                    }
                }
            } else {
                vResult = true;
            }
            return vResult;
        }

        private bool EnviarPagos(XElement valMedioDePago) {
            string vMedioDePago = "";
            string vFormatoDeCobro = "";
            string vMonto = "";
            string vCmd = "";
            decimal vTotalFactura = 0;
            string vCodigoMonedaBase = "";
            decimal vTotalPagadoML = 0;
            bool vResult = true;
            try {
                decimal vTotalPagoME = LibImportData.ToDec(LibXml.GetPropertyString(valMedioDePago, "BaseImponibleIGTF"));
                vCodigoMonedaBase = LibXml.GetPropertyString(valMedioDePago, "CodigoMoneda");
                vTotalPagadoML = LibImpresoraFiscalUtil.TotalMediosDePago(valMedioDePago.Descendants("GpResultDetailRenglonCobro"), vCodigoMonedaBase, false);
                vTotalFactura = LibImportData.ToDec(LibXml.GetPropertyString(valMedioDePago, "TotalFactura"));
                if (_EstaActivoFlag50) {
                    List<XElement> vNodos = valMedioDePago.Descendants("GpResultDetailRenglonCobro").Where(p => p.Element("CodigoMoneda").Value == vCodigoMonedaBase).ToList();
                    int vNodosCount = vNodos.Count;
                    if (vNodosCount > 0) {
                        foreach (XElement vXElement in vNodos) {
                            vMonto = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "Monto"));
                            vMedioDePago = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "CodigoFormaDelCobro"));
                            vFormatoDeCobro = FormaDeCobro(vMedioDePago);
                            vMonto = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vMonto, _EnterosParaPagos, _DecimalesParaPagos);
                            if ((vNodosCount == 1) && (LibImportData.ToDec(vMonto) == vTotalFactura) && (vTotalPagoME == 0)) {
                                vCmd = "1" + vFormatoDeCobro;
                            } else if ((vNodosCount == 1) && (vTotalPagoME > 0)) {
                                vCmd = "2" + vFormatoDeCobro + vMonto;
                            } else {
                                vCmd = "2" + vFormatoDeCobro + vMonto;
                            }
                            vResult = vResult && _TfhkPrinter.SendCmd(vCmd);
                        }
                    }
                    if (vTotalPagoME > 0) {
                        if (vTotalPagadoML == 0) {
                            vCmd = "120"; // Pago Total ME
                            vResult = vResult && _TfhkPrinter.SendCmd(vCmd);
                        } else {         // Pago parcial ME                                              
                            vTotalPagoME = LibImpresoraFiscalUtil.TotalMediosDePago(valMedioDePago.Descendants("GpResultDetailRenglonCobro"), vCodigoMonedaBase, true);
                            vMonto = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(LibConvert.ToStr(vTotalPagoME), _EnterosParaPagos, _DecimalesParaPagos);
                            vCmd = "220" + vMonto;
                            vResult = vResult && _TfhkPrinter.SendCmd(vCmd);
                        }
                    }
                    vResult = vResult && _TfhkPrinter.SendCmd(_CierreFacturaIGTF);
                } else {
                    List<XElement> vNodos = valMedioDePago.Descendants("GpResultDetailRenglonCobro").ToList();
                    int vNodosCount = vNodos.Count;
                    if (vNodosCount > 0) {
                        foreach (XElement vXElement in vNodos) {
                            vMonto = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "Monto"));
                            vMedioDePago = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "CodigoFormaDelCobro"));
                            vFormatoDeCobro = FormaDeCobro(vMedioDePago);
                            if ((vNodosCount == 1) && (LibImportData.ToDec(vMonto) == vTotalFactura)) {
                                vCmd = "1" + vFormatoDeCobro;
                            } else {
                                vMonto = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vMonto, _EnterosParaPagos, _DecimalesParaPagos);
                                vCmd = "2" + vFormatoDeCobro + vMonto;
                            }
                            vResult = vResult && _TfhkPrinter.SendCmd(vCmd);
                        }
                    }
                }
                if (!vResult) {
                    CancelarDocumentoFiscalEnImpresion(false);
                    throw new GalacException(" No se pudo Emitir la factura ", eExceptionManagementType.Controlled);
                }
                return vResult;
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Imprimir Medio de Pago\r\n" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private string GetLineaTexto() {
            string vResult = "";
            vResult = "i" + String.Format("{0:D2}", _LineaTextoAdicional);
            _LineaTextoAdicional++;
            return vResult;
        }

        private bool EnviarDatosAdicionales(string valDireccion, string valTelefono, string valObservaciones, bool valExtenderLineasAdicionales) {
            bool vResult = false;
            string vCMD = "";

            if (!valExtenderLineasAdicionales) {
                if (LibText.Len(valTelefono) > 0) {
                    vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString("Telf: " + valTelefono, 0, _MaxLongitudDeTexto));
                    vResult = _TfhkPrinter.SendCmd(vCMD);
                }
                if (!valDireccion.Equals("") && valObservaciones.Equals("") && !_ModelosAntiguos) {
                    vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString("Direccion:" + valDireccion, 0, 40));
                    vResult = _TfhkPrinter.SendCmd(vCMD);
                    vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString(valDireccion, 30, 40));
                    vResult &= _TfhkPrinter.SendCmd(vCMD);
                } else if (!valObservaciones.Equals("") && valDireccion.Equals("") && !_ModelosAntiguos) {
                    vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString("Obs.:" + valObservaciones, 0, 40));
                    vResult = _TfhkPrinter.SendCmd(vCMD);
                    vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString(valObservaciones, 35, 40));
                    vResult &= _TfhkPrinter.SendCmd(vCMD);
                } else {
                    vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString("Direccion:" + valDireccion, 0, 40));
                    vResult = _TfhkPrinter.SendCmd(vCMD);
                    vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString("Obs.:" + valObservaciones, 0, 40));
                    vResult &= _TfhkPrinter.SendCmd(vCMD);
                    vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString(valObservaciones, 35, 40));
                    vResult &= _TfhkPrinter.SendCmd(vCMD);
                }
            } else {
                vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString("Direccion:" + valDireccion, 0, 40));
                vResult = _TfhkPrinter.SendCmd(vCMD);
                vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString("Obs.:" + valObservaciones, 0, 40));
                vResult &= _TfhkPrinter.SendCmd(vCMD);
                vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString(valObservaciones, 35, 40));
                vResult &= _TfhkPrinter.SendCmd(vCMD);
            }
            return vResult;
        }

        private bool AbrirComprobanteFiscal(string valRazonSocial, string valRif, string valDireccion, string valTelefono, string valObservaciones) {
            try {
                bool vResult = false;
                string vCMD = "";
                if (_ModelosAntiguos) {
                    _LineaTextoAdicional = 0;
                    vCMD = GetLineaTexto() + "Nombre:" + LibText.Trim(LibText.SubString(valRazonSocial, 0, _MaxLongitudDeTexto));
                    vResult &= _TfhkPrinter.SendCmd(vCMD);
                    vCMD = GetLineaTexto() + "RIF:" + LibText.Trim(LibText.SubString(valRif, 0, 15));
                    vResult = _TfhkPrinter.SendCmd(vCMD);
                } else {
                    vCMD = "iR*" + LibText.Trim(LibText.SubString(valRif, 0, 15));
                    vResult = _TfhkPrinter.SendCmd(vCMD);
                    vCMD = "iS*" + LibText.Trim(LibText.SubString(valRazonSocial, 0, _MaxLongitudDeTexto));
                    vResult &= _TfhkPrinter.SendCmd(vCMD);
                }
                vResult &= EnviarDatosAdicionales(valDireccion, valTelefono, valObservaciones, _ExtenderLineasAdicionales);
                return vResult;
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Abrir Comprobante Fiscal\r\n" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private bool AbrirNotaDeCredito(string valRazonSocial, string valRif, string valDireccion, string valTelefono, string valObservaciones, string valNumeroFacturaOriginal, string valSerialMaquinaFiscal, string valFecha, string valHora) {
            try {
                bool vResult = false;
                string vCMD = "";
                if (_ModelosAntiguos) {
                    _LineaTextoAdicional = 0;
                    vCMD = GetLineaTexto() + "# Factura:" + LibText.Trim(LibText.SubString(valNumeroFacturaOriginal, 0, 11));
                    vResult = _TfhkPrinter.SendCmd(vCMD);
                    vCMD = GetLineaTexto() + "Nombre Cliente:" + LibText.Trim(LibText.SubString(valRazonSocial, 0, _MaxLongitudDeTexto));
                    vResult &= _TfhkPrinter.SendCmd(vCMD);
                    vCMD = GetLineaTexto() + "RIF:" + LibText.Trim(LibText.SubString(valRif, 0, 15));
                    vResult = _TfhkPrinter.SendCmd(vCMD);
                    vCMD = GetLineaTexto() + "Serial Maquina:" + LibText.Trim(LibText.SubString(valSerialMaquinaFiscal, 0, 10));
                    vResult = _TfhkPrinter.SendCmd(vCMD);
                    valFecha = LibString.SubString(valFecha, 0, 10);
                    vCMD = GetLineaTexto() + "Fecha:" + LibText.Trim(LibText.SubString(valFecha, 0, 10));
                    vResult = _TfhkPrinter.SendCmd(vCMD);
                } else {
                    valFecha = LibString.SubString(valFecha, 0, 10);
                    vCMD = "iR*" + LibText.Trim(LibText.SubString(valRif, 0, 15));
                    vResult = _TfhkPrinter.SendCmd(vCMD);
                    vCMD = "iS*" + LibText.Trim(LibText.SubString(valRazonSocial, 0, _MaxLongitudDeTexto));
                    vResult &= _TfhkPrinter.SendCmd(vCMD);
                    vCMD = "iF*" + LibText.Trim(LibText.SubString(valNumeroFacturaOriginal, 0, 11));
                    vResult = _TfhkPrinter.SendCmd(vCMD);
                    vCMD = "iD*" + LibText.Trim(LibText.SubString(valFecha, 0, 10));
                    vResult = _TfhkPrinter.SendCmd(vCMD);
                    vCMD = "iI*" + LibText.Trim(LibText.SubString(valSerialMaquinaFiscal, 0, 10));
                    vResult = _TfhkPrinter.SendCmd(vCMD);
                }
                vResult &= EnviarDatosAdicionales(valDireccion, valTelefono, valObservaciones, _ExtenderLineasAdicionales);
                return vResult;
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Abrir Comprobante Fiscal:\r\n" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private string FormaDeCobro(string valFormaDeCobro) {
            string vResultado = "";
            if (_ModelosAntiguos || _ModeloFactory == eImpresoraFiscal.DASCOMTALLY1125) {
                switch (valFormaDeCobro) {
                    case "00001"://Los textos de descripcion estan en la memoria de la impresora y se llaman a traves de su indice
                        vResultado = "01"; //Efectivo
                        break;
                    case "00002":
                        vResultado = "05";//Cheque
                        break;
                    case "00003":
                        vResultado = "09";//Tarjeta
                        break;
                    default:
                        vResultado = "01";
                        break;
                }
            } else { // Nuevos Modelos
                switch (valFormaDeCobro) {
                    case "00001":
                        vResultado = "01";//Efectivo
                        break;
                    case "00002":
                        vResultado = "07";//Cheque
                        break;
                    case "00003":
                        vResultado = "13";//Tarjeta
                        break;
                    default:
                        vResultado = "01";
                        break;
                }
            }
            return vResultado;
        }

        string FormatoParaFechaDeReportes(string valFecha) {
            string vResult;
            string Dia = "";
            string Mes = "";
            string Ano = "";

            Dia = LibString.SubString(valFecha, 0, 2);
            Mes = LibString.SubString(valFecha, 3, 2);
            Ano = LibString.SubString(valFecha, 8, 2);
            vResult = "0" + Ano + Mes + Dia;
            return vResult;
        }

        private bool ReimprimirFacturaFiscal(string valNumeroNotaDeCredito) {
            short vModo = 0;
            short vTipo = 1;
            return true;
        }


        private bool ReimprimirNotaDeCredito(string valNumeroNotaDeCredito) {
            short vModo = 0;
            short vTipo = 1;
            return true;
        }

        private bool ReimprimirReporteZ(string valNumeroReporteZ) {
            short vModo = 0;
            short vTipo = 2;
            return true;
        }

        private bool ReimprimirReporteX(string valNumeroReporteX) {
            short vModo = 0;
            short vTipo = 4;
            return true;
        }

        bool IImpresoraFiscalPdn.ReimprimirDocumentoNoFiscal(string valDesde, string valHasta) {
            throw new NotImplementedException();
        }

        bool IImpresoraFiscalPdn.ReimprimirDocumentoFiscal(string valDesde, string valHasta, string valTipo) {
            bool vResult = false;
            string vTipoOperacion = "";
            string vCmd = "";
            eTipoDocumentoFiscal TipoDeDocumento = (eTipoDocumentoFiscal)LibConvert.DbValueToEnum(valTipo);

            try {
                switch (TipoDeDocumento) {
                    case eTipoDocumentoFiscal.FacturaFiscal:
                        vTipoOperacion = "F";
                        break;
                    case eTipoDocumentoFiscal.NotadeCredito:
                        vTipoOperacion = "C";
                        break;
                    case eTipoDocumentoFiscal.ReporteX:
                        vTipoOperacion = "X";
                        break;
                    case eTipoDocumentoFiscal.ReporteZ:
                        vTipoOperacion = "Z";
                        break;
                }

                if (LibDate.IsValidTextForDate(valDesde) && LibDate.IsValidTextForDate(valHasta)) {
                    valDesde = FormatoParaFechaDeReportes(valDesde);
                    valHasta = FormatoParaFechaDeReportes(valHasta);
                } else if (LibConvert.IsNumeric(valDesde) && LibConvert.IsNumeric(valHasta)) {
                    valDesde = LibString.SubString(valDesde, 0, 6);
                    valDesde = LibText.FillWithCharToLeft(valDesde, "0", 7);
                    valHasta = LibString.SubString(valHasta, 0, 6);
                    valHasta = LibText.FillWithCharToLeft(valHasta, "0", 7);
                } else {
                    throw new GalacException("Los Datos de Entrada son Incorrectos para Número o Fecha\r\n", eExceptionManagementType.Controlled);
                }
                AbrirConexion();
                vCmd = "U4" + vTipoOperacion + valDesde + valHasta;
                vResult = _TfhkPrinter.SendCmd(vCmd);
                CerrarConexion();
                return vResult;
            } catch (Exception vEx) {
                throw;
            }
        }

        public IFDiagnostico RealizarDiagnotsico(bool valAbrirPuerto = false) {
            IFDiagnostico vDiagnostico = new IFDiagnostico();
            try {
                if (valAbrirPuerto) {
                    AbrirConexion();
                }
                vDiagnostico.EstatusDeComunicacion = EstatusDeComunicacion(vDiagnostico);
                vDiagnostico.VersionDeControladores = VersionDeControladores(vDiagnostico);
                if (!vDiagnostico.EstatusDeComunicacion) {
                    vDiagnostico.AlicoutasRegistradasDescription = "No se completo";
                    vDiagnostico.FechaYHoraDescription = "No se completo";
                    vDiagnostico.ColaDeImpresioDescription = "No se completo";
                    return vDiagnostico;
                }
                vDiagnostico.AlicuotasRegistradas = AlicuotasRegistradas(vDiagnostico);
                vDiagnostico.FechaYHora = FechaYHora(vDiagnostico);
                vDiagnostico.ColaDeImpresion = ColaDeImpresion(vDiagnostico);
                if (valAbrirPuerto) {
                    CerrarConexion();
                }
                return vDiagnostico;
            } catch (Exception) {
                throw;
            }
        }

        public bool EstatusDeComunicacion(IFDiagnostico vDiagnostico) {
            bool vResult = false;
            vResult = _TfhkPrinter.StatusPort && _TfhkPrinter.CheckFPrinter();
            vDiagnostico.EstatusDeComunicacionDescription = LibImpresoraFiscalUtil.EstatusDeComunicacionDescription(vResult);
            return vResult;
        }

        public bool VersionDeControladores(IFDiagnostico vDiagnostico) {
            bool vResult = false;
            bool vIsSameVersion = false;
            string vVersion = "";
            string vDir = "";
            vDir = System.IO.Path.Combine(LibApp.AppPath() + "CDP", DllApiName);
            vResult = LibImpresoraFiscalUtil.ObtenerVersionDeControlador(vDir, ref vVersion);
            vIsSameVersion = (vVersion == VersionApi);
            vDiagnostico.VersionDeControladoresDescription = LibImpresoraFiscalUtil.EstatusVersionDeControladorDescription(vResult, vIsSameVersion, vDir, vVersion, VersionApi);
            vResult = vIsSameVersion;
            return vResult;
        }

        public bool AlicuotasRegistradas(IFDiagnostico vDiagnostico) {
            bool vResult = false;
            decimal AlicuotaGeneral;
            decimal Alicuota2;
            decimal Alicuota3;
            string vAlicoutasRegistradasDescription = "";
            S3PrinterData vStatus3 = _TfhkPrinter.GetS3PrinterData();
            AlicuotaGeneral = LibImportData.ToDec(LibConvert.ToStr(vStatus3.Tax1), 2);
            Alicuota2 = LibImportData.ToDec(LibConvert.ToStr(vStatus3.Tax2), 2);
            Alicuota3 = LibImportData.ToDec(LibConvert.ToStr(vStatus3.Tax3), 2);
            vResult = LibImpresoraFiscalUtil.ValidarAlicuotasRegistradas(AlicuotaGeneral, Alicuota2, Alicuota3, ref vAlicoutasRegistradasDescription);
            vDiagnostico.AlicoutasRegistradasDescription = vAlicoutasRegistradasDescription;
            return vResult;
        }

        public bool FechaYHora(IFDiagnostico vDiagnostico) {
            bool vResult = false;
            DateTime dFecha;
            string vFecha = "";
            vFecha = ObtenerFechaYHora();
            dFecha = LibConvert.ToDate(vFecha);
            vResult = !LibDate.F1IsLessThanF2(dFecha, LibDate.Today());
            vDiagnostico.FechaYHoraDescription = LibImpresoraFiscalUtil.EstatusHorayFechaDescription(vResult);
            return vResult;
        }

        public bool ColaDeImpresion(IFDiagnostico vDiagnostico) {
            bool vResult = false;
            int vStatusCode = 0;
            string vStatusDescription = "";
            PrinterStatus vPFStatus = _TfhkPrinter.GetPrinterStatus();
            vStatusDescription = vPFStatus.PrinterStatusDescription;
            vStatusCode = vPFStatus.PrinterStatusCode;
            switch (vStatusCode) {
                case 4:
                    vResult = true;
                    vDiagnostico.ColaDeImpresioDescription = LibImpresoraFiscalUtil.EstatusColadeImpresionDescription(vResult);
                    break;
                case 5:
                    vResult = false;
                    vDiagnostico.ColaDeImpresioDescription = LibImpresoraFiscalUtil.EstatusColadeImpresionDescription(vResult);
                    break;
                default:
                    vResult = false;
                    vDiagnostico.ColaDeImpresioDescription = vPFStatus.PrinterStatusDescription;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            throw new NotImplementedException();
        }

        XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            throw new NotImplementedException();
        }

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            throw new NotImplementedException();
        }
    }
}

