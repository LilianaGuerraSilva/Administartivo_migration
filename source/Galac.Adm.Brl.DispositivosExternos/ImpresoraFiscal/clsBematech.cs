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
using LibGalac.Aos.Brl;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal {
    public class clsBematech: IImpresoraFiscalPdn {
        #region comandos Bema
        #region Funciones de Inicialización
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ProgramaAlicuota(string Alicuota, int ICMS_ISS);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ProgramaRedondeo();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ProgramaTruncamiento();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ActivaDesactivaVentaUnaLineaMFD(int iTipoLinea);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ActivaDesactivaReporteZAutomatico(int flag);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ActivaDesactivaCuponAdicional(int flag);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ActivaDesactivaVinculadoComprobanteNoFiscal(int flag);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ActivaDesactivaImpresionBitmapMA(int flag);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_HoraLimiteReporteZ(string Hora);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ProgramaCliche(string Cliche);
        #endregion

        #region Funciones del Cupon Fiscal
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreComprobanteDeVenta(string RIF, string Nombre);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreComprobanteDeVentaEx(string RIF, string Nombre, string Direccion);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VendeArticulo(string Codigo, string Descripcion, string Alicuota, string TipoCantidad, string Cantidad, int CasasDecimales, string ValorUnitario, string TipoDescuento, string Descuento);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ExtenderDescripcionArticulo(string DescripcionExt);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AnulaArticuloAnterior();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreCuponMFD(string cRIF, string cNombre, string cDireccion);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AnulaCupon();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CierraCupon(string FormaPago, string IncrementoDescuento, string TipoIncrementoDescuento, string ValorIncrementoDescuento, string ValorPago, string Mensaje);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_IniciaCierreCupon(string IncrementoDescuento, string TipoIncrementoDescuento, string ValorIncrementoDescuento);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_EfectuaFormaPago(string FormaPago, string ValorFormaPago);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FinalizaCierreCupon(string Mensaje);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_DevolucionArticulo(string Codigo, string Descripcion, string Alicuota, string TipoCantidad, string Cantidad, int CasasDecimales, string ValorUnit, string TipoDescuento, string ValorDesc);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreNotaDeCredito(string Nombre, string NumeroSerie, string RIF, string Dia, string Mes, string Ano, string Hora, string Minuto, string Segundo, string COO);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_EfectuaFormaPagoDescripcionForma(string FormaDePago, string MontoCancelado, string DescripcionPago);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FinalizarCierreCupon(string vParametros);
        [DllImport("BemaFI32.dll")]
        public static extern int Bematech_FI_NumeroComprobanteFiscal([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroComprobante);
        [DllImport("BemaFI32.dll")]
        public static extern int Bematech_FI_ContadorNotaDeCreditoMFD([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroComprobante);
        [DllImport("BemaFI32.dll")]
        public static extern int Bematech_FI_NumeroReducciones([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroReducciones);        
        #endregion

        #region Funciones de los Informes Fiscales
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LecturaX();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LecturaXSerial();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ReduccionZ(string Fecha, string Hora);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_InformeGerencial(string Texto);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CierraInformeGerencial();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FlagFiscalesIII(int Flag);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_InformeTransacciones(string tipo, string Fechaini, string Fechafim, string Opcion);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CierraInformeXoZ();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_IniciaCierreCuponIGTF(string TotalPagosEnDivisas);
        #endregion


        #region Funciones de las Operaciones No Fiscales
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_RecebimientoNoFiscal(string IndiceTotalizador, string Valor, string FormaPago);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreComprobanteNoFiscalVinculado(string FormaPago, string Valor, string NumeroCupon);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreComprobanteNoFiscalVinculadoMFD(string FormaPago, string Valor, string NumeroCupon, string CGC, string NombreCliente, string Direccion);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ImprimeComprobanteNoFiscalVinculado(string Texto);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CierraComprobanteNoFiscalVinculado();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_Sangria(string Valor);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_Provision(string Valor, string FormaPago);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreInformeGerencial(string NumInforme);
        #endregion

        #region Funciones de Informaciones de la Impresora
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NumeroSerieMFD([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroSerial);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_Agregado([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorIncrementos);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_Cancelamientos([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorCancelamientos);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_DatosUltimaReduccion([MarshalAs(UnmanagedType.VBByRefStr)] ref string DatosReduccion);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_DatosUltimaReduccionMFD([MarshalAs(UnmanagedType.VBByRefStr)] ref string DatosReduccion);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_Descuentos([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorDescuentos);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NumeroCuponesAnulados([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroCancelamientos);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_RetornoAlicuotas([MarshalAs(UnmanagedType.VBByRefStr)] ref string Alicuotas);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_SubTotal([MarshalAs(UnmanagedType.VBByRefStr)] ref string Subtotal);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ClavePublica(string Clave);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ContadorSecuencial(string Retorno);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VentaBrutaDiaria(string Valor);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_BaudrateProgramado(string Baudrate);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FlagActivacionAlineamientoIzquierda(string Flag);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FlagSensores(int Flag);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ImprimeClavePublica();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FechaHoraImpresora([MarshalAs(UnmanagedType.VBByRefStr)] ref string Fecha, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Hora);
        #endregion

        #region Funciones de Autenticación y Gaveta de Efectivo
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AccionaGaveta();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_Autenticacion();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ProgramaCaracterAutenticacion(string Parametros);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaEstadoGaveta(out int EstadoGaveta);
        #endregion

        #region Otras Funciones
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbrePuertaSerial();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CierraPuertaSerial();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_RetornoImpresora(ref int ACK, ref int ST1, ref int ST2);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ImpresionCintaDetalle(string cTipoImpresion, string cParametroInicial, string cParametroFinal, string cUsuarioImpresora);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaImpresoraPrendida();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaEstadoImpresora(ref int ACK, ref int ST1, ref int ST2);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VersionFirmwareMFD([MarshalAs(UnmanagedType.VBByRefStr)] ref string FirmwareVersion);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VersionDll([MarshalAs(UnmanagedType.VBByRefStr)] ref string FirmwareVersion);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ImprimeConfiguracionesImpresora();
        #endregion
        #endregion

        #region Constantes
        const string MinVersionApi = "5,4,1,40";
        const string MaxVersionApi = "5,4,1,43";
        const string DllApiName = @"BemaFI32.dll";
        const string _FirmwareVersBM4000IGTF = "010022";
        byte _EnterosMontosLargos = 10;
        const byte _EnterosCantidad = 4;
        const byte _EnterosMontosCortos = 2;
        const byte _Decimales3Digitos = 3;
        const byte _Decimales2Digitos = 2;
        #endregion Constantes
        #region Variables
        bool _RegistroDeRetornoEnTxt;
        bool _UsaPuertoUsb;
        #endregion
        private enum eTipoDeLectura {
            NoAplica = 0,
            UltimaFactura,
            UltimaNotaDeCredito,
            UltimoReporteZ
        }

        private eImpresoraFiscal vModelo;
        private bool _PortIsOpen = false;

        public clsBematech(XElement valXmlDatosImpresora) {
            ConfigurarImpresora(valXmlDatosImpresora);
            ConfigurarArchivosDeRetornoBemaFi32();
        }

        private void ConfigurarImpresora(XElement valXmlDatosImpresora) {
            eTipoConexion vTipoConexion = (eTipoConexion)LibConvert.DbValueToEnum(LibXml.GetPropertyString(valXmlDatosImpresora, "TipoConexion"));
            ePuerto ePuerto = (ePuerto)LibConvert.DbValueToEnum(LibXml.GetPropertyString(valXmlDatosImpresora, "PuertoMaquinaFiscal"));
            string vPuerto = ePuerto.GetDescription(1);
            vModelo = (eImpresoraFiscal)LibConvert.DbValueToEnum(LibXml.GetPropertyString(valXmlDatosImpresora, "ModeloDeMaquinaFiscal"));
            _RegistroDeRetornoEnTxt = LibConvert.SNToBool(LibXml.GetPropertyString(valXmlDatosImpresora, "RegistroDeRetornoEnTxt"));
            string vBemaFi32Path = PathFile();
            string vLeerFile;
            string[] vContenidoFile;
            int vIndex = 0;
            if (vModelo == eImpresoraFiscal.BEMATECH_MP_4000_FI) {
                _EnterosMontosLargos = 13;
            } else {
                _EnterosMontosLargos = 10;
            }
            try {
                if (vTipoConexion == eTipoConexion.USB) {
                    vPuerto = "USB";
                } else {
                    vPuerto = "COM" + vPuerto;
                }
                _UsaPuertoUsb = (vTipoConexion == eTipoConexion.USB);
                vLeerFile = LibFile.ReadFile(vBemaFi32Path);
                vContenidoFile = LibText.Split(vLeerFile, "\r\n", true);
                if (vContenidoFile != null && vContenidoFile.Length > 0) {
                    foreach (string vLineaTexto in vContenidoFile) { // Buscar La linea donde se configura el puerto
                        if (LibString.S1IsInS2("Puerta", vLineaTexto)) {
                            vLeerFile = vContenidoFile[vIndex];
                            break;
                        }
                        vIndex += 1;
                    }
                    if (vIndex < vContenidoFile.Length && !LibString.S1IsInS2(vPuerto, vLeerFile)) {
                        EditarFileBemaFi32INI(vBemaFi32Path, "Puerta", vPuerto);
                    }
                }
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private void ConfigurarArchivosDeRetornoBemaFi32() {
            string vBemaFi32Path = PathFile();
            string vText = LibFile.ReadFile(vBemaFi32Path);
            string[] vContenidoFile = LibText.Split(vText, "\r\n", true);
            int vContarLineas = 0;
            try {
                foreach (string vLineas in vContenidoFile) {
                    if (LibString.S1IsInS2("Status=0", vLineas)) {
                        EditarFileBemaFi32INI(vBemaFi32Path, "Status", "1");
                    } else if (LibString.S1IsInS2(vLineas, "Retorno=0") && _RegistroDeRetornoEnTxt) {
                        EditarFileBemaFi32INI(vBemaFi32Path, "Retorno", "1");
                    } else if (LibString.S1IsInS2(vLineas, "Retorno=1") && !_RegistroDeRetornoEnTxt) {
                        EditarFileBemaFi32INI(vBemaFi32Path, "Retorno", "0");
                    } else if (LibString.S1IsInS2(vLineas, "Log=0")) {
                        EditarFileBemaFi32INI(vBemaFi32Path, "Log", "1");
                    } else if (LibString.S1IsEqualToS2(vLineas, @"Path=C:\")) {
                        EditarFileBemaFi32INI(vBemaFi32Path, "Path", @"C:\Bema");
                    } else if (LibString.S1IsEqualToS2(vLineas, "ControlPuerta=0") && _UsaPuertoUsb) { //Abrir Puerto de forma Automatica, USB no necesita hacerlo manual
                        EditarFileBemaFi32INI(vBemaFi32Path, "ControlPuerta", "1");
                    } else if (LibString.S1IsEqualToS2(vLineas, "ControlPuerta=1") && !_UsaPuertoUsb) { //Abrir Puerto de forma Manual, COM  necesita hacerlo de esta manera
                        EditarFileBemaFi32INI(vBemaFi32Path, "ControlPuerta", "0");
                    } else if (LibString.S1IsInS2(vLineas, "ConfigRede=1")) {
                        EditarFileBemaFi32INI(vBemaFi32Path, "ConfigRede", "0");
                    } else if (vContarLineas >= 30) {
                        break;
                    }
                    vContarLineas += 1;
                }
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private bool EditarFileBemaFi32INI(string valBemaFi32Path, string valParameter, string valValueReplaced) {
            try {
                string vText = LibFile.ReadFile(valBemaFi32Path);
                string[] vContenidoFile = LibText.Split(vText, "\r\n", true);
                var replaced = vContenidoFile.Select(x => {
                    if (x.StartsWith(valParameter)) {
                        return valParameter + "=" + valValueReplaced;
                    }
                    return x;
                });
                vText = string.Join(LibText.CRLF(), replaced); // Inverso del split
                LibIO.WriteLineInFile(valBemaFi32Path, vText, false);
                return true;
            } catch (Exception vEx) {
                throw new GalacException("Edit BemaFi32.INI" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerFechaYHora() {
            string vResult = "";
            int retorno = 0;
            string vFecha = "";
            string vHora = "";
            string vMensaje = "";
            bool vReq;
            vFecha = LibText.Space(6);
            vHora = LibText.Space(6);
            retorno = Bematech_FI_FechaHoraImpresora(ref vFecha, ref vHora);
            vReq = RevisarEstadoImpresora(ref vMensaje);
            if (vReq) {
                vFecha = LibString.Trim(vFecha);
                vHora = LibString.Trim(vHora);
                if (_RegistroDeRetornoEnTxt && (LibString.IsNullOrEmpty(vFecha) && LibString.IsNullOrEmpty(vFecha))) {
                    vResult = LeerArchivoDeRetorno(eTipoDeLectura.NoAplica);
                    int vPosition = LibString.IndexOf(vResult, ",");
                    if (vPosition > 0) {
                        vFecha = LibString.SubString(vResult, 0, vPosition);
                        vHora = LibString.SubString(vResult, vPosition + 1);
                    }
                }
                if (!LibString.IsNullOrEmpty(vFecha) && !LibString.IsNullOrEmpty(vHora)) {
                    vFecha = LibText.SubString(vFecha, 0, 2) + "/" + LibText.SubString(vFecha, 2, 2) + "/" + LibText.SubString(vFecha, 4, 2);
                    vHora = LibText.SubString(vHora, 0, 2) + ":" + LibText.SubString(vHora, 2, 2);
                    vResult = vFecha + LibText.Space(1) + vHora;
                }
            } else {
                throw new GalacException("Error al Obtener Fecha Y Hora", eExceptionManagementType.Controlled);
            }
            return vResult;
        }

        private string PathFile(bool valEsREtorno = false) {
            try {
                string vresult = "";
                if (valEsREtorno) {
                    if (!LibFile.DirExists(@"C:\Bema")) {
                        LibFile.CreateDir(@"C:\Bema");
                    }
                    vresult = @"C:\Bema\Retorno.txt";
                } else {
                    if (Environment.Is64BitOperatingSystem) {
                        vresult = Environment.GetFolderPath(Environment.SpecialFolder.SystemX86) + @"\BemaFi32.ini";
                    } else {
                        vresult = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\BemaFi32.ini";
                    }
                    if (!LibFile.FileExists(vresult)) {
                        throw new GalacException($"El archivo de configuración \"{vresult}\" no existe o no fue suministrado.", eExceptionManagementType.Controlled);
                    }
                }
                return vresult;
            } catch (System.DllNotFoundException vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool AbrirConexion() {
            bool vResult = false;
            try {
                if (!_PortIsOpen) {
                    if (_UsaPuertoUsb) {
                        _PortIsOpen = true;
                        vResult = _PortIsOpen;
                    } else {
                        vResult = (Bematech_FI_AbrePuertaSerial() != -5);
                        _PortIsOpen = vResult;
                    }
                } else {
                    _PortIsOpen = true;
                    vResult = _PortIsOpen;
                }
                return vResult;
            } catch (System.DllNotFoundException vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            } catch (Exception vEx) {
                throw new GalacException("Abrir Conexión " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool CerrarConexion() {
            bool vResult = false;
            try {
                if (_PortIsOpen) {
                    if (_UsaPuertoUsb) {
                        _PortIsOpen = false;
                        vResult = true;
                        Thread.Sleep(250);
                    } else {
                        vResult = (Bematech_FI_CierraPuertaSerial() == 1);
                        Thread.Sleep(250);
                        vResult = true;
                        _PortIsOpen = vResult;
                    }
                } else {
                    vResult = true;
                }
                return vResult;
            } catch (Exception vEx) {
                throw new GalacException("Cerrar Conexión " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool ComprobarEstado() {
            bool vResult = false;
            int valStatus = 0;
            string MensajeStatus = "";
            valStatus = Bematech_FI_VerificaImpresoraPrendida();
            if (RevisarEstadoImpresora(ref MensajeStatus)) {
                vResult = true;
            }
            return vResult;
        }

        public string ObtenerSerial(bool valAbrirConexion) {
            string vSerial = "";
            string vMensajeStatus = "";
            bool vResult = false;
            int vRetorno;
            try {
                if (valAbrirConexion) {
                    AbrirConexion();
                }
                vSerial = LibText.Space(20);
                vRetorno = Bematech_FI_NumeroSerieMFD(ref vSerial);
                vSerial = LibText.Trim(vSerial);
                vResult = RevisarEstadoImpresora(ref vMensajeStatus);
                if (vResult) {
                    if (_RegistroDeRetornoEnTxt && LibString.IsNullOrEmpty(vSerial)) {
                        vSerial = LeerArchivoDeRetorno(eTipoDeLectura.NoAplica);
                        vSerial = LibText.Trim(vSerial);
                        vResult = !LibString.IsNullOrEmpty(vSerial);
                    } else {
                        vResult = !LibString.IsNullOrEmpty(vSerial);
                    }
                } else {
                    vMensajeStatus += "\r\nError de comunicación revisar puertos y conexiones";
                    throw new GalacException(vMensajeStatus, eExceptionManagementType.Controlled);
                }
                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vSerial;
            } catch (Exception vEx) {
                throw new GalacException("Obtener serial: " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public eStatusImpresorasFiscales EstadoDelPapel(bool valAbrirPuerto) {
            eStatusImpresorasFiscales vResult = eStatusImpresorasFiscales.eListoEnEspera;
            int vACK = 0;
            int vST1 = 0;
            int vST2 = 0;
            int vRepuesta = 0;

            try {
                if (valAbrirPuerto) {
                    AbrirConexion();
                }
                vRepuesta = Bematech_FI_VerificaEstadoImpresora(ref vACK, ref vST1, ref vST2);
                switch (vST1) {
                    case 0:
                        vResult = eStatusImpresorasFiscales.eListoEnEspera;
                        break;
                    case 128:
                        vResult = eStatusImpresorasFiscales.eSinPapel;
                        break;
                    case 64:
                        vResult = eStatusImpresorasFiscales.ePocoPapel;
                        break;
                }
                if (valAbrirPuerto) {
                    CerrarConexion();
                }
                return vResult;
            } catch (Exception vEx) {
                throw new GalacException("Estado del Papel: " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool RealizarReporteZ() {
            bool vResult = false;
            int vEstatus = 0;
            string MensajeStatus = "";
            try {
                AbrirConexion();
                vEstatus = Bematech_FI_ReduccionZ("", "");
                vResult = RevisarEstadoImpresora(ref MensajeStatus);
                Thread.Sleep(2000); // Pausa de 2 Segundos pues la impresora queda en espera luego de calcular el cierre diario
                CerrarConexion();
                if (!vResult) {
                    throw new GalacException(MensajeStatus, eExceptionManagementType.Controlled);
                }
                return vResult;
            } catch (Exception vEx) {
                throw new GalacException("Realizar Cierre Diario: " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool RealizarReporteX() {
            bool vResult = false;
            int vEstado = 0;
            string MensajeStatus = "";
            try {
                AbrirConexion();
                vEstado = Bematech_FI_LecturaX();
                vResult = RevisarEstadoImpresora(ref MensajeStatus);
                CerrarConexion();
                if (!vResult) {
                    throw new GalacException("Realizar Cierre X: " + MensajeStatus, eExceptionManagementType.Controlled);
                }
                return vResult;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerUltimoNumeroFactura(bool valAbrirConexion) {
            string vUltimaFactura;
            int vRetorno = 0;
            bool vResult = false;
            string MensajeStatus = "";

            try {
                if (valAbrirConexion) {
                    AbrirConexion();
                }
                vUltimaFactura = LibText.Space(6);
                vRetorno = Bematech_FI_NumeroComprobanteFiscal(ref vUltimaFactura);
                vUltimaFactura = LibText.Trim(vUltimaFactura);
                vResult = RevisarEstadoImpresora(ref MensajeStatus);
                if (vResult) {
                    if (_RegistroDeRetornoEnTxt && LibString.IsNullOrEmpty(vUltimaFactura)) {
                        vResult = Bematech_FI_LecturaXSerial() == 1;
                        vUltimaFactura = LeerArchivoDeRetorno(eTipoDeLectura.UltimaFactura);
                        vUltimaFactura = LibText.Trim(vUltimaFactura);
                        vResult = !LibString.IsNullOrEmpty(vUltimaFactura);
                    } else {
                        vResult = !LibString.IsNullOrEmpty(vUltimaFactura);
                    }
                } else {
                    MensajeStatus += "\r\nError de comunicación revisar puertos y conexiones.";
                    throw new GalacException(MensajeStatus, eExceptionManagementType.Controlled);
                }
                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimaFactura;
            } catch (Exception vEx) {
                CerrarConexion();
                throw new GalacException("Obtener último número de factura: " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerUltimoNumeroNotaDeCredito(bool valAbrirConexion) {
            string vUltimaNotaCredito = "";
            int vRetorno = 0;
            bool vResult = false;
            string MensajeStatus = "";

            try {
                if (valAbrirConexion) {
                    AbrirConexion();
                }
                vUltimaNotaCredito = LibText.Space(6);
                vRetorno = Bematech_FI_ContadorNotaDeCreditoMFD(ref vUltimaNotaCredito);
                vUltimaNotaCredito = LibText.Trim(vUltimaNotaCredito);
                vResult = RevisarEstadoImpresora(ref MensajeStatus);
                if (vResult) {
                    if (_RegistroDeRetornoEnTxt && LibString.IsNullOrEmpty(vUltimaNotaCredito)) {
                        vResult = Bematech_FI_LecturaXSerial() == 1;
                        vUltimaNotaCredito = LeerArchivoDeRetorno(eTipoDeLectura.UltimaNotaDeCredito);
                        vUltimaNotaCredito = LibText.Trim(vUltimaNotaCredito);
                        vResult = !LibString.IsNullOrEmpty(vUltimaNotaCredito);
                    } else {
                        vResult = !LibString.IsNullOrEmpty(vUltimaNotaCredito);
                    }
                } else {
                    MensajeStatus += "\r\nError de comunicación revisar puertos y conexiones.";
                    throw new GalacException(MensajeStatus, eExceptionManagementType.Controlled);
                }
                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimaNotaCredito;
            } catch (Exception vEx) {
                CerrarConexion();
                throw new GalacException("Obtener último número Nota de Crédito: " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }
		
		public string ObtenerUltimoNumeroNotaDeDebito(bool valAbrirConexion) {
            throw new NotImplementedException();
        }

        public string ObtenerUltimoNumeroReporteZ(bool valAbrirConexion) {
            string vUltimoZ;
            int vRetorno = 0;
            bool vResult = false;
            string MensajeStatus = "";

            try {
                if (valAbrirConexion) {
                    AbrirConexion();
                }
                vUltimoZ = LibText.Space(4);
                vRetorno = Bematech_FI_NumeroReducciones(ref vUltimoZ);
                vUltimoZ = LibText.Trim(vUltimoZ);
                vResult = RevisarEstadoImpresora(ref MensajeStatus);
                if (vResult) {
                    if (_RegistroDeRetornoEnTxt && LibString.IsNullOrEmpty(vUltimoZ)) {
                        vUltimoZ = LeerArchivoDeRetorno(eTipoDeLectura.NoAplica);
                        vUltimoZ = LibText.Trim(vUltimoZ);
                        vResult = !LibString.IsNullOrEmpty(vUltimoZ);
                    } else {
                        vResult = !LibString.IsNullOrEmpty(vUltimoZ);
                    }
                } else {
                    MensajeStatus += "\r\nError de comunicación revisar puertos y conexiones";
                    throw new GalacException(MensajeStatus, eExceptionManagementType.Controlled);
                }
                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimoZ;
            } catch (Exception vEx) {
                CerrarConexion();
                throw new GalacException("Obtener último número de reporte Z: " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private string LeerArchivoDeRetorno(eTipoDeLectura valTipoLectura) {
            string vResultFile = "";
            string vTextoBusqueda = "";
            string vPathFileRetorno = PathFile(true);
            if (LibFile.FileExists(vPathFileRetorno)) {
                vResultFile = LibString.Trim(LibFile.ReadFile(vPathFileRetorno));
                if (LibString.IsNullOrEmpty(vResultFile)) {
                    throw new GalacAlertException($"No hay acceso al archivo de retorno \"{vPathFileRetorno}\" o no fue encontrado. Verifique los permisos de la aplicación.");
                }
                if (valTipoLectura != eTipoDeLectura.NoAplica) {
                    switch (valTipoLectura) {
                        case eTipoDeLectura.UltimaFactura:
                            vTextoBusqueda = "Contador de Factura:";
                            break;
                        case eTipoDeLectura.UltimaNotaDeCredito:
                            vTextoBusqueda = "Contador de Nota de Crédito";
                            break;
                        case eTipoDeLectura.UltimoReporteZ:
                            vTextoBusqueda = "Último Reporte Z";
                            break;
                    }
                    vResultFile = BuscarValorEnTexto(vResultFile, vTextoBusqueda, valTipoLectura);
                }
                if (_RegistroDeRetornoEnTxt) {
                    LibFile.DeleteFile(vPathFileRetorno);
                }
            } else {
                string vBemaFi32Path = PathFile();
                throw new GalacAlertException($"No fue encontrado o no hay acceso al archivo de retorno \"{vPathFileRetorno}\". Verifique el archivo de configuración \"{vBemaFi32Path}\" o los permisos de la aplicación.");
            }
            return vResultFile;
        }

        private string BuscarValorEnTexto(string valCadena, string valTextoBusqueda, eTipoDeLectura valTipoLectura) {
            int vPosicionLinea = 0;
            int vPosicionCorte;
            string vResult = "";
            int vCantidadCaracteres;
            string[] vArrayTextoRetorno = LibString.Split(valCadena, '\n');
            if (vArrayTextoRetorno != null && vArrayTextoRetorno.Length > 0) {
                foreach (string vTexto in vArrayTextoRetorno) {
                    if (LibString.S1IsInS2(valTextoBusqueda, vTexto)) {
                        break;
                    }
                    vPosicionLinea += 1;
                }
                if ((vPosicionLinea) >= vArrayTextoRetorno.Length) {
                    throw new GalacAlertException($"El ítem {valTextoBusqueda} no fue encontrado en la colección. Verifique la configuración regional o los permisos de la aplicación.");
                }
                vPosicionLinea = (valTipoLectura == eTipoDeLectura.UltimoReporteZ) ? vPosicionLinea + 1 : vPosicionLinea; //Para posicionarme en la linea del Último Numero Z                                
                vCantidadCaracteres = 7;
                vResult = vArrayTextoRetorno[vPosicionLinea];
                if (valTipoLectura == eTipoDeLectura.UltimoReporteZ) {
                    vPosicionCorte = 0;
                } else {
                    vPosicionCorte = LibString.Len(vResult) - vCantidadCaracteres;
                }
                vResult = LibString.Trim(LibString.SubString(vResult, vPosicionCorte, vCantidadCaracteres));
            }
            return vResult;
        }

        public bool CancelarDocumentoFiscalEnImpresion(bool valAbrirConexion) {
            int vResult = 0;
            try {
                if (valAbrirConexion) {
                    AbrirConexion();
                }
                vResult = Bematech_FI_AnulaCupon();
                if (valAbrirConexion) {
                    CerrarConexion();
                }
                return (vResult == 1);
            } catch (Exception vEx) {
                throw new GalacException("Cancelar Documento Fiscal en Impresion: " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool ImprimirNotaCredito(XElement valDocumentoFiscal, eTipoDocumentoFactura valTipoDocumento) {
            string vRif = LibText.Trim(LibXml.GetPropertyString(valDocumentoFiscal, "NumeroRIF"));
            string vRazonSocial = LibText.Trim(LibXml.GetPropertyString(valDocumentoFiscal, "NombreCliente"));
            string vNumeroComprobanteFiscal = LibString.Trim(LibXml.GetPropertyString(valDocumentoFiscal, "NumeroComprobanteFiscal"));
            string vSerialMaquina = LibString.Trim(LibXml.GetPropertyString(valDocumentoFiscal, "SerialMaquinaFiscal"));
            string vFecha = LibXml.GetPropertyString(valDocumentoFiscal, "Fecha");
            string vHora = LibXml.GetPropertyString(valDocumentoFiscal, "HoraModificacion");
            bool vResult = false;
            try {
                AbrirConexion();
                CancelarDocumentoFiscalEnImpresion(false);
                if (AbrirNotaDeCredito(vRazonSocial, vRif, vNumeroComprobanteFiscal, vSerialMaquina, vFecha, vHora)) {
                    vResult = ImprimirTodosLosArticulos(valDocumentoFiscal);
                    vResult &= CerrarComprobanteFiscal(valDocumentoFiscal, true);
                }
                CerrarConexion();
            } catch (Exception vEx) {
                CerrarConexion();
                throw new GalacException("imprimir factura fiscal: " + vEx.Message, eExceptionManagementType.Controlled);
            }
            return vResult;
        }

        public bool ImprimirFacturaFiscal(XElement valDocumentoFiscal, eTipoDocumentoFactura valTipoDocumento) {
            string vDireccion = LibXml.GetPropertyString(valDocumentoFiscal, "DireccionCliente");
            string vRif = LibXml.GetPropertyString(valDocumentoFiscal, "NumeroRIF");
            string vRazonSocial = LibXml.GetPropertyString(valDocumentoFiscal, "NombreCliente");
            bool vResult = false;
            try {
                AbrirConexion();
                CancelarDocumentoFiscalEnImpresion(false);
                if (AbrirComprobanteFiscal(vRazonSocial, vRif, vDireccion)) {
                    vResult = ImprimirTodosLosArticulos(valDocumentoFiscal);
                    vResult &= CerrarComprobanteFiscal(valDocumentoFiscal);
                }
                CerrarConexion();
            } catch (Exception vEx) {
                CerrarConexion();
                throw new GalacException("imprimir factura fiscal: " + vEx.Message, eExceptionManagementType.Controlled);
            }
            return vResult;
        }

        public bool ImprimirNotaDebito(XElement valDocumentoFiscal, eTipoDocumentoFactura valTipoDocumento) {
            throw new NotImplementedException();
        }

        private bool AbrirComprobanteFiscal(string valRazonSocial, string valRif, string valDireccion) {
            try {
                bool vResult = false;
                int vRepuesta = 0;
                string MensajeStatus = "";
                valRazonSocial = LibText.SubString(valRazonSocial, 0, 41);
                valRif = LibText.SubString(valRif, 0, 18);
                valDireccion = LibText.SubString(valDireccion, 0, 133);
                valDireccion = LibString.IsNullOrEmpty(valDireccion) ? "-" : valDireccion;
                vRepuesta = Bematech_FI_AbreComprobanteDeVentaEx(valRif, valRazonSocial, valDireccion);
                vResult = RevisarEstadoImpresora(ref MensajeStatus);
                if (!vResult) {
                    throw new GalacException("error al abrir el Comprobante Fiscal: " + MensajeStatus, eExceptionManagementType.Controlled);
                }
                return vResult;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private bool AbrirNotaDeCredito(string valRazonSocial, string valRif, string valNumeroComprobanteFiscal, string valSerialMaquina, string valFecha, string valHora) {
            try {
                bool vResult = false;
                int vRepuesta = 0;
                string MensajeStatus = "";
                string vDia;
                string vMes;
                string vAno;
                string vHora;
                string vMinuto;
                string vSegundo;
                valNumeroComprobanteFiscal = LibText.Right(valNumeroComprobanteFiscal, 6);
                valSerialMaquina = LibText.SubString(valSerialMaquina, 0, 10);
                valRazonSocial = LibText.SubString(valRazonSocial, 0, 38);
                DateTime vFechaDT = LibConvert.ToDate(valFecha);
                DateTime vHoraTM = LibConvert.ToDate(valHora);
                valRif = LibText.SubString(valRif, 0, 14);
                vDia = LibText.FillWithCharToLeft(LibConvert.ToStr(vFechaDT.Day), "0", 2);
                vMes = LibText.FillWithCharToLeft(LibConvert.ToStr(vFechaDT.Month), "0", 2);
                vAno = LibText.Right(LibConvert.ToStr(vFechaDT.Year), 2);
                vHora = LibText.FillWithCharToLeft(LibConvert.ToStr(vHoraTM.Hour), "0", 2);
                vMinuto = LibText.FillWithCharToLeft(LibConvert.ToStr(vHoraTM.Minute), "0", 2);
                vSegundo = "00";
                vRepuesta = Bematech_FI_AbreNotaDeCredito(valRazonSocial, valSerialMaquina, valRif, vDia, vMes, vAno, vHora, vMinuto, vSegundo, valNumeroComprobanteFiscal);
                vResult = RevisarEstadoImpresora(ref MensajeStatus);
                if (!vResult) {
                    throw new GalacException("error al abrir la Nota de Crédito: " + MensajeStatus, eExceptionManagementType.Controlled);
                }
                return vResult;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private bool CerrarComprobanteFiscal(XElement valDocumentoFiscal, bool valEsNotaDeCredito = false) {
            int vResult = 0;
            const string AplicaDescuento = "D";
            const string TipoDescuento = "%";
            string valDescuentoTotal = "";
            string vDireccionFiscal = "";
            bool vImprimeDireccionALFinalDeLaFactura = false;
            string vObservaciones = "";
            bool Seguir = false;
            string vMensaje = "";
            string vTexto = "";
            string vCamposDef = "";
            bool vSinDreccionSinObservaciones = false;
            string vTotalMonedaExtranjera = "";
            int vCaracteresRestantes = 0;
            decimal vTotalPagosME = 0;
            decimal vTotalPagosML = 0;
            string vCodigoMoneda = "";
            string vTotalPagosMEConFormato = "";
            string vVersionFirmware;
            decimal vIGTF = 0;
            string vObservIGTF = "";
            try {                
                vVersionFirmware = GetFirmwareVersion();
                vIGTF = LibImportData.ToDec(LibXml.GetPropertyString(valDocumentoFiscal, "IGTFML"));
                vImprimeDireccionALFinalDeLaFactura = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "ImprimeDireccionAlFinalDelComprobanteFiscal");
                valDescuentoTotal = LibXml.GetPropertyString(valDocumentoFiscal, "PorcentajeDescuento");
                valDescuentoTotal = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(valDescuentoTotal, _EnterosMontosCortos, _Decimales2Digitos);
                vDireccionFiscal = LibXml.GetPropertyString(valDocumentoFiscal, "DireccionCliente");
                vObservaciones = LibXml.GetPropertyString(valDocumentoFiscal, "Observaciones");
                vTotalMonedaExtranjera = LibXml.GetPropertyString(valDocumentoFiscal, "TotalMonedaExtranjera");
                vCodigoMoneda = LibXml.GetPropertyString(valDocumentoFiscal, "CodigoMoneda");
                vTotalPagosME = LibImportData.ToDec(LibXml.GetPropertyString(valDocumentoFiscal, "BaseImponibleIGTF"));
                vTotalPagosML = LibImpresoraFiscalUtil.TotalMediosDePago(valDocumentoFiscal.Descendants("GpResultDetailRenglonCobro"), vCodigoMoneda, false);
                if (vModelo == eImpresoraFiscal.BEMATECH_MP_4000_FI && LibConvert.ToLong(vVersionFirmware) >= LibConvert.ToLong(_FirmwareVersBM4000IGTF)) {
                    if (vTotalPagosME > 0) {
                        vTotalPagosMEConFormato = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(LibImpresoraFiscalUtil.DecimalToStringFormat(vTotalPagosME, 2), _EnterosMontosLargos, _Decimales2Digitos, ",");
                        vResult = Bematech_FI_IniciaCierreCuponIGTF(vTotalPagosMEConFormato);
                        vTotalPagosME = LibImpresoraFiscalUtil.TotalMediosDePago(valDocumentoFiscal.Descendants("GpResultDetailRenglonCobro"), vCodigoMoneda, true);
                        vTotalPagosMEConFormato = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(LibImpresoraFiscalUtil.DecimalToStringFormat(vTotalPagosME, 2), _EnterosMontosLargos, _Decimales2Digitos, ",");
                        vResult = Bematech_FI_EfectuaFormaPagoDescripcionForma("Divisas", vTotalPagosMEConFormato, "");
                    } else {
                        vResult = Bematech_FI_IniciaCierreCuponIGTF("0");
                    }
                    Seguir = EnviarPagos(valDocumentoFiscal, vCodigoMoneda);
                } else {
                    vResult = Bematech_FI_IniciaCierreCupon(AplicaDescuento, TipoDescuento, valDescuentoTotal);
                    vTotalPagosMEConFormato = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(LibImpresoraFiscalUtil.DecimalToStringFormat(vTotalPagosME, 2), _EnterosMontosLargos, _Decimales2Digitos, ",");
                    if (vTotalPagosML < vTotalPagosME) {
                        Seguir = EnviarPagos(valDocumentoFiscal, vCodigoMoneda);
                        vResult = Bematech_FI_EfectuaFormaPagoDescripcionForma("Divisas", vTotalPagosMEConFormato, "");
                    } else {
                        if (vTotalPagosME > 0) {
                            vResult = Bematech_FI_EfectuaFormaPagoDescripcionForma("Divisas", vTotalPagosMEConFormato, "");
                        }
                        Seguir = EnviarPagos(valDocumentoFiscal, vCodigoMoneda);
                    }
                }
                if (vModelo == eImpresoraFiscal.BEMATECH_MP_20_FI_II && valEsNotaDeCredito) {
                    vResult = Bematech_FI_FinalizarCierreCupon(vTexto);
                    Seguir = RevisarEstadoImpresora(ref vMensaje);
                    return Seguir;
                } else {
                    if (LibText.Len(vTotalMonedaExtranjera) > 0 && !valEsNotaDeCredito) {
                        vCaracteresRestantes = LibString.Len(vTotalMonedaExtranjera);
                        vTexto += vTotalMonedaExtranjera;
                    } else if (valEsNotaDeCredito) {
                        vTotalMonedaExtranjera = "";
                    }
                    if (LibText.Len(vObservaciones) > 0) {
                        if (LibText.S1IsInS2("Total", vObservaciones)) {
                            vObservaciones = LibText.Replace(vObservaciones, "Total", "Tot..");
                        }
                        vTexto = (LibString.IsNullOrEmpty(vTexto) ? "" : vTexto + "\r\n");
                        vTexto += LibText.Left(vObservIGTF + "\r\n" + "Obs.:" + vObservaciones, Math.Abs(320 - vCaracteresRestantes));
                        vCaracteresRestantes = LibString.Len(vTexto) - vCaracteresRestantes;
                    } else if (LibText.Len(vObservIGTF) > 0) {
                        vTexto += LibText.Left(vObservIGTF, Math.Abs(320 - vCaracteresRestantes));
                        vCaracteresRestantes = LibString.Len(vTexto) - vCaracteresRestantes;
                    } else {
                        vCaracteresRestantes = 1;
                    }
                    vSinDreccionSinObservaciones = (LibString.Len(vDireccionFiscal) == 0 && LibString.Len(vObservaciones) == 0 && LibString.IsNullOrEmpty(vObservIGTF));
                    if (LibString.Len(vTotalMonedaExtranjera) == 0 && vCaracteresRestantes > 0) {
                        vCamposDef = ImprimeCamposDefinibles(valDocumentoFiscal, vSinDreccionSinObservaciones);
                        vCamposDef = (LibString.IsNullOrEmpty(vCamposDef) ? "" : "\r\n" + vCamposDef);
                        vTexto += vCamposDef;
                    }
                }
                vResult = Bematech_FI_FinalizarCierreCupon(vTexto);
                Seguir = RevisarEstadoImpresora(ref vMensaje);
                if (!Seguir) {
                    throw new GalacException(vMensaje, eExceptionManagementType.Controlled);
                }
                return Seguir;
            } catch (Exception vEx) {
                throw new GalacException("Cerrar Comprobante: " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private string ImprimeCamposDefinibles(XElement valData, bool valSinDreccionSinObservaciones) {
            string vResult = "";
            byte vTope = 0;
            byte vCuentaLineas = 0;
            if (valSinDreccionSinObservaciones) {
                vTope = 7;
            } else {
                vTope = 5;
            }
            List<XElement> vCamposDefinibles = valData.Descendants("GpResultDetailCamposDefinibles").ToList();
            if (vCamposDefinibles.Count > 0) {
                foreach (XElement vRecord in vCamposDefinibles) {
                    if (vCuentaLineas > vTope) {
                        break;
                    }
                    vResult += LibText.Left(LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vRecord, "CampoDefinibleValue")), 48) + "\r\n";
                    vCuentaLineas++;
                }
                vResult = LibText.SubString(vResult, 0, LibText.Len(vResult) - 2);
            } else {
                return "";
            }
            return vResult;
        }

        private bool ImprimirTodosLosArticulos(XElement valDocumentoFiscal) {
            bool vEstatus = false;
            int vResultado = 0;
            string vCodigo;
            string vDescripcionResumida;
            string vDescripcionExtendida;
            string vCantidad;
            string vMonto;
            eTipoDeAlicuota vTipoAlicuota;
            string vPrcDescuento;
            string vPorcentajeAlicuota = "";
            const string vFormatoCantidad = "F";
            const int vCantidaDecimales = 2;
            const string vFormatoDescuento = "%";
            string vSerial;
            string vRollo;
            string vMensaje = "";
            eStatusImpresorasFiscales PrintStatus;

            try {
                List<XElement> vRecord = valDocumentoFiscal.Descendants("GpResultDetailRenglonFactura").ToList();
                foreach (XElement vXElement in vRecord) {
                    PrintStatus = EstadoDelPapel(false);
                    vCodigo = LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement, "Articulo"), 0, 12);
                    vDescripcionResumida = LibImpresoraFiscalUtil.CadenaCaracteresValidos(LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement, "Descripcion"), 0, 29));
                    vDescripcionExtendida = LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement, "Descripcion"), 0, 150);
                    vCantidad = LibXml.GetElementValueOrEmpty(vXElement, "Cantidad");
                    vCantidad = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vCantidad, _EnterosCantidad, _Decimales3Digitos, ",");
                    vMonto = LibXml.GetElementValueOrEmpty(vXElement, "PrecioSinIVA");
                    vMonto = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vMonto, _EnterosMontosLargos, _Decimales2Digitos, ",");
                    vTipoAlicuota = (eTipoDeAlicuota)LibConvert.DbValueToEnum(LibXml.GetElementValueOrEmpty(vXElement, "AlicuotaIva"));
                    vPorcentajeAlicuota = LibXml.GetElementValueOrEmpty(vXElement, "PorcentajeAlicuota");
                    vPorcentajeAlicuota = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vPorcentajeAlicuota, _EnterosMontosCortos, _Decimales2Digitos);
                    vPorcentajeAlicuota = (LibImportData.ToDec(vPorcentajeAlicuota) == 0 ? "FF" : vPorcentajeAlicuota);
                    vPrcDescuento = (LibXml.GetElementValueOrEmpty(vXElement, "PorcentajeDescuento"));
                    vPrcDescuento = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vPrcDescuento, _EnterosMontosCortos, _Decimales2Digitos);
                    vSerial = LibXml.GetElementValueOrEmpty(vXElement, "Serial");
                    vRollo = LibXml.GetElementValueOrEmpty(vXElement, "Rollo");
                    if (LibString.Len(vSerial) > 0) {
                        vSerial = "\u0020" + LibText.SubString(vSerial, 0, 20);
                    }
                    if (LibString.Len(vRollo) > 0) {
                        vRollo = "\u0020" + LibText.SubString(vRollo, 0, 20);
                    }
                    vDescripcionExtendida = vDescripcionExtendida + (LibString.IsNullOrEmpty(vSerial) ? "" : vSerial) + (LibString.IsNullOrEmpty(vRollo) ? "" : vRollo);
                    vResultado = Bematech_FI_ExtenderDescripcionArticulo(vDescripcionExtendida);
                    vEstatus = RevisarEstadoImpresora(ref vMensaje);
                    vResultado = Bematech_FI_VendeArticulo(vCodigo, vDescripcionResumida, vPorcentajeAlicuota, vFormatoCantidad, vCantidad, vCantidaDecimales, vMonto, vFormatoDescuento, vPrcDescuento);
                    vEstatus &= RevisarEstadoImpresora(ref vMensaje);
                    if (!vEstatus) {
                        throw new GalacException("Error al imprimir artículo " + vMensaje, eExceptionManagementType.Controlled);
                    }
                }
                return vEstatus;
            } catch (Exception vEx) {
                CancelarDocumentoFiscalEnImpresion(false);
                CerrarConexion();
                throw (vEx);
            }
        }

        private bool RetornoStatus(int valStatus, out string valMensajeRetorno) {
            bool vResult = false;
            valMensajeRetorno = "";
            switch (valStatus) {
                case 0:
                    vResult = false;
                    valMensajeRetorno = " Error de Comunicación.";
                    break;
                case 1:
                    vResult = true;
                    valMensajeRetorno = " Estatus Fiscal OK.";
                    break;
                case -1:
                    vResult = false;
                    valMensajeRetorno = " Error de ejecución, comando no Ejecutado.";
                    break;
                case -2:
                    vResult = false;
                    valMensajeRetorno = " Parametro inválido en el comando.";
                    break;
                case -3:
                    vResult = false;
                    valMensajeRetorno = " Alícuota no programada.";
                    break;
                case -4:
                    vResult = false;
                    valMensajeRetorno = " Archivo BEMAFI32.ini no encontrado en el directorio del sistema.";
                    break;
                case -5:
                    vResult = false;
                    valMensajeRetorno = " Error al abrir Puerto de Comunicaciones.";
                    break;
                case -8:
                    vResult = false;
                    valMensajeRetorno = " Error al guardar o crear el archivo Status.TXT o Retorno.txt.";
                    break;
                case -27:
                    vResult = false;
                    valMensajeRetorno = " Estatus de la Impresora Fiscal Distinto de (6,0,0).";
                    break;
            }
            return vResult;
        }

        private bool RetornoStatus1(int valStatus, out string valMensajeRetorno) {
            bool vResult = true;
            valMensajeRetorno = "";

            if (valStatus >= 128) { // bit 7 
                valStatus = valStatus - 128;
                valMensajeRetorno = valMensajeRetorno + " Fin del Papel.";
                vResult = false;
            }
            if (valStatus >= 64) { // bit 6 
                valStatus = valStatus - 64;
                valMensajeRetorno = valMensajeRetorno + " Poco Papel.";
                vResult = false;
            }
            if (valStatus >= 32) { // bit 5 
                valStatus = valStatus - 32;
                valMensajeRetorno = valMensajeRetorno + " Error en el Reloj.";
                vResult = false;
            }
            if (valStatus >= 16) { // bit 4 
                valStatus = valStatus - 16;
                valMensajeRetorno = valMensajeRetorno + " Impresora con Error.";
                vResult = false;
            }
            if (valStatus >= 8) { // bit 3 
                valStatus = valStatus - 8;
                valMensajeRetorno = valMensajeRetorno + " Comando no empieza con ESC.";
                vResult = false;
            }
            if (valStatus >= 4) { // bit 2 
                valStatus = valStatus - 4;
                valMensajeRetorno = valMensajeRetorno + " Comando Inexistente.";
                vResult = false;
            }
            if (valStatus >= 2) { // bit 1 
                valStatus = valStatus - 2;
                valMensajeRetorno = valMensajeRetorno + " Cupón Abierto.";
                vResult = false;
            }
            if (valStatus >= 1) { // bit 0 
                valStatus = valStatus - 1;
                valMensajeRetorno = valMensajeRetorno + " Número de Parámetro(s) Inválido(s).";
                vResult = false;
            }
            if (valStatus == 0) { // bit 0 
                valStatus = valStatus - 1;
                valMensajeRetorno = valMensajeRetorno + "";
                //vResult = true;
            }
            return vResult;
        }

        private bool RetornoStatus2(int valStatus, out string valMensajeRetorno) {
            bool vResult = false;
            valMensajeRetorno = "";

            if (valStatus >= 128) { // bit 7 
                valStatus = valStatus - 128;
                valMensajeRetorno = valMensajeRetorno + " Comando Inválido.";
                vResult = false;
            }
            if (valStatus >= 64) { // bit 6 
                valStatus = valStatus - 64;
                valMensajeRetorno = valMensajeRetorno + " Memoria Fiscal Llena.";
                vResult = false;
            }
            if (valStatus >= 32) { // bit 5 
                valStatus = valStatus - 32;
                valMensajeRetorno = " Error en memoria RAM.";
                vResult = false;
            }
            if (valStatus >= 16) { // bit 4 
                valStatus = valStatus - 16;
                valMensajeRetorno = valMensajeRetorno + " Alícuota no programada.";
                vResult = false;
            }
            if (valStatus >= 8) { // bit 3 
                valStatus = valStatus - 8;
                valMensajeRetorno = valMensajeRetorno + " Capacidad de Alicuota Llena.";
                vResult = false;
            }
            if (valStatus >= 4) { // bit 2 
                valStatus = valStatus - 4;
                valMensajeRetorno = valMensajeRetorno + " No se permite Cancelar.";
                vResult = false;
            }
            if (valStatus >= 2) { // bit 1 
                valStatus = valStatus - 2;
                valMensajeRetorno = valMensajeRetorno + " RIF del propietario No está programado en la impresora.";
                vResult = false;
            }
            if (valStatus >= 1) { // bit 0 
                valStatus = valStatus - 1;
                valMensajeRetorno = valMensajeRetorno + " Comando no Ejecutado.";
                vResult = false;
            }
            if (valStatus == 0) { // bit 0 
                valStatus = valStatus - 1;
                valMensajeRetorno = valMensajeRetorno + "";
                vResult = true;
            }
            return vResult;
        }

        private bool RetornoACK(int valACK, ref string refMensajeRetorno) {
            bool vResult = false;
            refMensajeRetorno = "";
            switch (valACK) {
                case 6:
                    refMensajeRetorno = "";
                    vResult = true;
                    break;
                case 21:
                    refMensajeRetorno = "Comando no Reconocido.";
                    vResult = false;
                    break;
            }
            return vResult;
        }

        private bool RevisarEstadoImpresora(ref string refMensajeRetorno) {
            bool vResult = false;
            int Status1 = 0;
            int Status2 = 0;
            int ACK = 0;
            int vRetorno = 0;
            string vMensajeEstado = "";

            try {
                refMensajeRetorno = "";
                vRetorno = Bematech_FI_RetornoImpresora(ref ACK, ref Status1, ref Status2);
                vResult = RetornoStatus(vRetorno, out vMensajeEstado);
                refMensajeRetorno = refMensajeRetorno + LibText.CRLF() + vMensajeEstado;
                vMensajeEstado = "";
                vRetorno = 0;
                vResult = vResult && RetornoStatus1(Status1, out vMensajeEstado);
                refMensajeRetorno = refMensajeRetorno + LibText.CRLF() + vMensajeEstado;
                vMensajeEstado = "";
                vRetorno = 0;
                vResult = vResult && RetornoStatus2(Status2, out vMensajeEstado);
                refMensajeRetorno = refMensajeRetorno + LibText.CRLF() + vMensajeEstado;
                vMensajeEstado = "";
                vRetorno = 0;
                vResult = vResult && RetornoACK(ACK, ref vMensajeEstado);
                refMensajeRetorno = refMensajeRetorno + LibText.CRLF() + vMensajeEstado;
                vMensajeEstado = "";
                vRetorno = 0;
                return vResult;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private bool EnviarPagos(XElement valMedioDePago, string valCodigoMoneda) {
            string vMedioDePago = "";
            string vMontoCancelado = "";
            int vResult = 0;
            try {
                List<XElement> vNodos = valMedioDePago.Descendants("GpResultDetailRenglonCobro").Where(p => p.Element("CodigoMoneda").Value == valCodigoMoneda).ToList();
                if (vNodos.Count > 0) {
                    foreach (XElement vXElement in vNodos) {
                        vMedioDePago = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "CodigoFormaDelCobro"));
                        vMontoCancelado = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "Monto"));
                        vMedioDePago = FormaDeCobro(vMedioDePago);
                        vMontoCancelado = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vMontoCancelado, _EnterosMontosLargos, _Decimales2Digitos, ","); //DarFormatoNumericoParaLosPagos(vMontoCancelado);
                        vResult = Bematech_FI_EfectuaFormaPagoDescripcionForma(vMedioDePago, vMontoCancelado, "");
                    }
                }
                return true;
            } catch (Exception vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private string FormaDeCobro(string valFormaDeCobro) {
            string vResultado = "";
            switch (valFormaDeCobro) {
                case "00001":
                    vResultado = "Efectivo";
                    break;
                case "00002":
                    vResultado = "Cheque";
                    break;
                case "00003":
                    vResultado = "Tarjeta";
                    break;
                case "00004":
                    vResultado = "Depósito";
                    break;
                case "00005":
                    vResultado = "Anticipo";
                    break;
                case "00006":
                    vResultado = "Transferencia";
                    break;
                case "00015":
                    vResultado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreCreditoElectronico");
                    break;
                default:
                    vResultado = "Efectivo";
                    break;
            }
            return vResultado;
        }     
              
        bool IImpresoraFiscalPdn.ReimprimirDocumentoFiscal(string valDesde, string valHasta, eTipoDocumentoFiscal valTipoDocumento, eTipoDeBusqueda valTipoDeBusqueda) {
            bool vEstatus = false;
            int vResultado = 0;
            string vTipoImpresion = string.Empty;
            string vMensaje = string.Empty;
            try {
                switch(valTipoDeBusqueda) {
                    case eTipoDeBusqueda.NumeroDocumento:
                    vTipoImpresion = "2";
                    valDesde = LibText.Right(valDesde, 6);
                    valHasta = LibText.Right(valHasta, 6);
                    break;
                    case eTipoDeBusqueda.RangoDeFecha:
                    vTipoImpresion = "1";
                    valDesde = FormatoFecha(valDesde);
                    valHasta = FormatoFecha(valHasta);
                    break;
                    case eTipoDeBusqueda.NumeroRif:
                    vTipoImpresion = "0";
                    break;
                    default:
                    vTipoImpresion = "2";
                    valDesde = LibText.Right(valDesde, 6);
                    valHasta = LibText.Right(valHasta, 6);
                    break;
                }
                vResultado = Bematech_FI_ImpresionCintaDetalle(vTipoImpresion, valDesde, valHasta, "0");
                vEstatus = RevisarEstadoImpresora(ref vMensaje);
                if(!vEstatus) {
                    throw new GalacException("Error al reimprimir comprobante" + vMensaje, eExceptionManagementType.Controlled);
                }
                return vEstatus;
            } catch(GalacException) {
                throw;
            }
        }

        private string FormatoFecha(string valFecha) {
            DateTime dFecha=LibImportData.ToDate(valFecha);
            return dFecha.ToString("ddMMyy");
        }


        bool IImpresoraFiscalPdn.ReimprimirDocumentoNoFiscal(string valDesde, string valHasta) {
            throw new NotImplementedException();
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

        private string GetFirmwareVersion() {
            string vVersionFirmware;
            vVersionFirmware = LibString.Space(10);
            Bematech_FI_VersionFirmwareMFD(ref vVersionFirmware);
            vVersionFirmware = LibText.Trim(vVersionFirmware);
            if (_RegistroDeRetornoEnTxt && LibString.IsNullOrEmpty(vVersionFirmware)) {
                vVersionFirmware = LeerArchivoDeRetorno(eTipoDeLectura.NoAplica);
                vVersionFirmware = LibString.Trim(vVersionFirmware);
            }
            return vVersionFirmware;
        }

        public bool EstatusDeComunicacion(IFDiagnostico vDiagnostico) {
            bool vResult = false;
            vResult = Bematech_FI_VerificaImpresoraPrendida() == 1;
            vDiagnostico.EstatusDeComunicacionDescription = LibImpresoraFiscalUtil.EstatusDeComunicacionDescription(vResult);
            return vResult;
        }

        public bool VersionDeControladores(IFDiagnostico vDiagnostico) {
            bool vResult = false;
            bool vIsSameVersion = false;
            string vVersion = "";
            string vDir;
            if (Environment.Is64BitOperatingSystem) {
                vDir = Environment.GetFolderPath(Environment.SpecialFolder.SystemX86) + @"\BemaFi32.dll";
            } else {
                vDir = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\BemaFi32.dll";
            }
            if (LibFile.FileExists(vDir)) {
                vVersion = LibText.Space(9);
                vResult = Bematech_FI_VersionDll(ref vVersion) == 1;
                vVersion = LibString.Trim(vVersion);
                vVersion = LibString.SubString(vVersion, 0, 8);
                vResult = !LibString.IsNullOrEmpty(vVersion);
                if (_RegistroDeRetornoEnTxt && !vResult) {
                    vResult = LibImpresoraFiscalUtil.ObtenerVersionDeControlador(vDir, ref vVersion);
                    vVersion = LibString.Replace(vVersion, LibString.Space(1), "");
                }
                vIsSameVersion = (vVersion == MinVersionApi || vVersion == MaxVersionApi);
                vDiagnostico.VersionDeControladoresDescription = LibImpresoraFiscalUtil.EstatusVersionDeControladorDescription(vResult, vIsSameVersion, vDir, vVersion, vVersion);
                vResult = vIsSameVersion;
            } else {
                vDiagnostico.VersionDeControladoresDescription = LibImpresoraFiscalUtil.EstatusVersionDeControladorDescription(vResult, vIsSameVersion, vDir, vVersion, vVersion);
            }
            return vResult;
        }

        public bool AlicuotasRegistradas(IFDiagnostico vDiagnostico) {
            bool vResult = false;
            decimal AlicuotaGeneral = 0;
            decimal Alicuota2 = 0;
            decimal Alicuota3 = 0;
            int vReq = 0;
            string vRegAlicuotas = "";
            vRegAlicuotas = LibText.Space(79);
            vReq = Bematech_FI_RetornoAlicuotas(ref vRegAlicuotas);
            vRegAlicuotas = LibString.Trim(vRegAlicuotas);
            if (_RegistroDeRetornoEnTxt && LibString.IsNullOrEmpty(vRegAlicuotas)) {
                vRegAlicuotas = LeerArchivoDeRetorno(eTipoDeLectura.NoAplica);
            }
            vRegAlicuotas = LibText.Replace(vRegAlicuotas, "\0", "");
            if (!LibString.IsNullOrEmpty(vRegAlicuotas)) {
                string[] ListAlicuotas = LibString.Split(vRegAlicuotas, ',');
                if (ListAlicuotas != null && ListAlicuotas.Length > 0) {
                    AlicuotaGeneral = LibImportData.ToDec(ListAlicuotas[0], 2) * 0.01m;
                    Alicuota2 = LibImportData.ToDec(ListAlicuotas[1], 2) * 0.01m;
                    Alicuota3 = LibImportData.ToDec(ListAlicuotas[2], 2) * 0.01m;
                }
            }
            vRegAlicuotas = "";
            vResult = LibImpresoraFiscalUtil.ValidarAlicuotasRegistradas(AlicuotaGeneral, Alicuota2, Alicuota3, ref vRegAlicuotas);
            vDiagnostico.AlicoutasRegistradasDescription = vRegAlicuotas;
            vDiagnostico.AlicoutasRegistradasDescription += ":" + LibText.CRLF();
            vDiagnostico.AlicoutasRegistradasDescription += "Reducida:   " + LibConvert.NumToString(Alicuota2, 2) + "%" + LibText.CRLF() +
                                                            "General:    " + LibConvert.NumToString(AlicuotaGeneral, 2) + "%" + LibText.CRLF() +
                                                            "Adicional: " + LibConvert.NumToString(Alicuota3, 2) + "%";

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
            int vRetorno = 0;
            int ACK = 0, S1 = 0, S2 = 0;
            string vColaImpresion = "";
            vRetorno = Bematech_FI_RetornoImpresora(ref ACK, ref S1, ref S2);
            vResult = vRetorno == 1;
            vResult = vResult && RetornoStatus1(S1, out vColaImpresion);
            vDiagnostico.ColaDeImpresioDescription = (LibString.IsNullOrEmpty(vColaImpresion) ? "Listo En Espera" : vColaImpresion);
            return vResult;
        }

        public IFDiagnostico RealizarDiagnostico(bool valAbrirPuerto = false) {
            IFDiagnostico vDiagnostico = new IFDiagnostico();
            try {
                if (valAbrirPuerto) {
                    AbrirConexion();
                }
                vDiagnostico.EstatusDeComunicacion = EstatusDeComunicacion(vDiagnostico);
                vDiagnostico.VersionDeControladores = VersionDeControladores(vDiagnostico);
                if (!vDiagnostico.EstatusDeComunicacion) {
                    vDiagnostico.AlicoutasRegistradasDescription = "No se completó.";
                    vDiagnostico.FechaYHoraDescription = "No se completó.";
                    vDiagnostico.ColaDeImpresioDescription = "No se completó.";
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

        public bool ConsultarConfiguracion(IFDiagnostico iFDiagnostico) {
            throw new NotImplementedException();
        }

        public bool ImprimirDocumentoNoFiscal(string valTextoNoFiscal, string valDescripcion) {
            try {
                bool vResult = true;
                string vMensaje = string.Empty;
                int vRetorno = 0;
                if (AbrirConexion()) {
                    string[] vTextBlock = LibString.Split(valTextoNoFiscal, "\r\n");                    
                    if (vTextBlock != null && vTextBlock.Count() > 0) {
                        vRetorno = Bematech_FI_InformeGerencial(valDescripcion + "\r\n");
                        foreach (string vLines in vTextBlock) {
                            vRetorno = Bematech_FI_InformeGerencial(vLines + "\r\n");
                            vResult &= RevisarEstadoImpresora(ref vMensaje);
                        }
                        vRetorno = Bematech_FI_CierraInformeGerencial();
                        vResult &= RevisarEstadoImpresora(ref vMensaje);
                    }
                    CerrarConexion();
                }
                return vResult;
            } catch (Exception) {
                CerrarConexion();
                throw;
            }
        }
    }
}

