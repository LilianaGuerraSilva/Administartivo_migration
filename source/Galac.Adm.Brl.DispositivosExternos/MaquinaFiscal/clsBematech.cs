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
    public class clsBematech : IImpresoraFiscalPdn {
        #region comandos
        #region Funciones de Inicialización
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ProgramaAlicuota(string Alicuota, int ICMS_ISS);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ProgramaRedondeo();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ProgramaTruncamiento();
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
        public static extern int Bematech_FI_AbreNotaDeCredito(string Nombre, string NumeroSerie, string RIF, string Dia, string Mes, string Ano, string Hora, string Minuto, string Segundo, string COO, string MsjPromocional);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_EfectuaFormaPagoDescripcionForma(string FormaDePago, string MontoCancelado, string DescripcionPago);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FinalizarCierreCupon(string vParametros);
        [DllImport("BemaFI32.dll")]
        public static extern int Bematech_FI_NumeroComprobanteFiscal([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroComprobante);
        #endregion

        #region Funciones de los Informes Fiscales
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LecturaX();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LecturaXSerial();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ReduccionZ(string FechaINI, string FechaFIN);
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
        #endregion

        #region Funciones de las Operaciones No Fiscales
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_RecebimientoNoFiscal(string IndiceTotalizador, string Valor, string FormaPago);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreComprobanteNoFiscalVinculado(string FormaPago, string Valor, string NumeroCupon);
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
        public static extern int Bematech_FI_NumeroSerie([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroSerial);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_Agregado([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorIncrementos);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_Cancelamientos([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorCancelamientos);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_DatosUltimaReduccion([MarshalAs(UnmanagedType.VBByRefStr)] ref string DatosReduccion);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_Descuentos([MarshalAs(UnmanagedType.VBByRefStr)] ref string ValorDescuentos);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_NumeroCuponesAnulados([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroCancelamientos);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_RetornoAlicuotas([MarshalAs(UnmanagedType.VBByRefStr)] ref string Alicuotas);
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
        public static extern int Bematech_FI_VerificaImpresoraPrendida();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaEstadoImpresora(ref int ACK, ref int ST1, ref int ST2);
        #endregion
        #endregion

        private string CommPort = "";
        private eImpresoraFiscal vModelo;

        public clsBematech(XElement valXmlDatosImpresora) {
            ConfigurarPuerto(valXmlDatosImpresora);
        }

        private void ConfigurarPuerto(XElement valXmlDatosImpresora) {
            bool vResult = false;
            bool EsLogFile = false;
            string valPuerto = LibXml.GetPropertyString(valXmlDatosImpresora, "PuertoMaquinaFiscal");
            vModelo = (eImpresoraFiscal)LibConvert.DbValueToEnum(LibXml.GetPropertyString(valXmlDatosImpresora, "ModeloDeMaquinaFiscal"));
            string vBemaIniPath = PathFile(EsLogFile);

            try {

                if (!LibConvert.IsNumeric(valPuerto)) {
                    CommPort = "USB";
                } else {
                    CommPort = "COM" + valPuerto;
                }

                if (LibFile.FileExists(vBemaIniPath)) {
                    vResult = EditFileBemaINI(vBemaIniPath, "Puerta", CommPort);
                } else {
                    throw new GalacException("Error al configurar el puerto de comunicaciones", eExceptionManagementType.Controlled);
                }
            } catch (Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private bool EditFileBemaINI(string valBemaPath, string valParameter, string valComPort) {
            try {
                string vText = LibFile.ReadFile(valBemaPath);
                string[] vLines = LibText.Split(vText, "\r\n", true);
                var replaced = vLines.Select(x => {
                    if (x.StartsWith(valParameter)) {
                        return valParameter + "=" + valComPort;
                    } else if (x.StartsWith("Path=")) {
                        return @"Path=C:\Bema\";
                    }
                    return x;
                });
                vText = string.Join(LibText.CRLF(), replaced);
                LibIO.WriteLineInFile(valBemaPath, vText, false);
                return true;
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException("BemaFi32.INI" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerFechaYHora() {
            string vResult = "";
            int retorno = 0;
            string vFecha = LibText.Space(6);
            string vHora = LibText.Space(6);
            retorno = Bematech_FI_FechaHoraImpresora(ref vFecha, ref vHora);
            if (retorno.Equals(1)) {
                vFecha = LibText.SubString(vFecha, 0, 2) + "/" + LibText.SubString(vFecha, 2, 2) + "/" + LibText.SubString(vFecha, 4, 2);
                vHora = LibText.SubString(vHora, 0, 2) + ":" + LibText.SubString(vHora, 2, 2);
                vResult = vFecha + LibText.Space(1) + vHora;
            }
            return vResult;
        }

        private string PathFile(bool valFileLog) {
            try {
                string vresult = "";
                if (valFileLog) {
                    if (LibFile.FileExists(@"\C:\Bema")) {
                        vresult = @"\C:\Bema\BemaFI32.log";
                    } else {
                        LibFile.CreateDir(@"\C:\Bema");
                        vresult = @"\C:\Bema\BemaFI32.log";
                    }
                } else {
                    vresult = LibApp.SysDir() + "\\BemaFi32.ini";
                }
                return vresult;
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException("BemaFI32.INI " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool AbrirConexion() {
            bool vResult = false;
            try {
                vResult = (Bematech_FI_AbrePuertaSerial() != -5);
                return vResult;
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Abrir Conexion " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool CerrarConexion() {
            bool vResult = false;
            try {
                vResult = (Bematech_FI_CierraPuertaSerial() == 1);
                Thread.Sleep(500);
                return vResult;
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Cerrar Conexion " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool ComprobarConexion() {
            bool vResult = false;
            int valStatus = 0;
            string MensajeStatus = "";

            valStatus = Bematech_FI_VerificaImpresoraPrendida();
            if (RetornoStatus(valStatus, out MensajeStatus)) {
                vResult = true;
            }
            return vResult;
        }

        public string ObtenerSerial() {
            string vSerial = LibText.Space(15);
            string MensajeStatus = "";
            int vRepuesta = 0;
            bool vResult;

            try {
                if (AbrirConexion()) {
                    vRepuesta = Bematech_FI_NumeroSerie(ref vSerial);
                    vResult = vRepuesta.Equals(1);
                    CerrarConexion();
                    if (vResult) {
                        vSerial = LibText.Trim(vSerial);
                    } else {
                        throw new LibGalac.Aos.Catching.GalacException(MensajeStatus, eExceptionManagementType.Controlled);
                    }
                }
                return vSerial;
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Obtener ultimo serial:" + vEx.Message, eExceptionManagementType.Controlled);
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
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Estado del Papel " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool RealizarReporteZ() {
            bool vResult = false;
            int vEstatus = 0;
            string MensajeStatus = "";
            try {
                if (AbrirConexion()) {
                    vEstatus = Bematech_FI_ReduccionZ("", "");
                    vResult = RevisarEstadoImpresora(ref MensajeStatus);
                    CerrarConexion();
                    if (!vResult) {
                        throw new GalacException(MensajeStatus, eExceptionManagementType.Controlled);
                    }
                }
                return vResult;
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Realizar Cierre Diario " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool RealizarReporteX() {
            bool vResult = false;
            int vEstado = 0;
            string MensajeStatus = "";
            try {
                if (AbrirConexion()) {
                    vEstado = Bematech_FI_LecturaX();
                    vResult = RevisarEstadoImpresora(ref MensajeStatus);
                    CerrarConexion();
                    if (!vResult) {
                        throw new GalacException("Realizar De Caja" + MensajeStatus, eExceptionManagementType.Controlled);
                    }
                }
                return vResult;
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerUltimoNumeroFactura() {
            string vUltimaFactura = LibText.Space(8);
            int vRetorno = 0;
            bool vResult = false;
            string MensajeStatus = "";

            try {
                if (AbrirConexion()) {
                    vRetorno = Bematech_FI_NumeroComprobanteFiscal(ref vUltimaFactura);
                    vResult = RevisarEstadoImpresora(ref MensajeStatus);
                    CerrarConexion();
                    vResult = RevisarEstadoImpresora(ref MensajeStatus);
                    if (vResult) {
                        MensajeStatus = LibText.CleanSpacesToBothSides(MensajeStatus);
                    } else {
                        throw new GalacException(MensajeStatus, eExceptionManagementType.Controlled);
                    }
                }
                return vUltimaFactura;
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Obtener ultimo número de factura" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerUltimoNumeroNotaDeCredito() {
            string vUltimoNotaCredito = "";
            //S1PrinterData estado;
            //string vRuta = vRutaData();
            try {
                if (AbrirConexion()) {
                    string vCmd = "S1";
                    //TfhkPrinter.UploadStatusCmd(vCmd, vRuta);
                    //estado = TfhkPrinter.GetS1PrinterData();
                    //vUltimoNotaCredito = LibConvert.ToStr(estado.LastCreditNoteNumber);
                    CerrarConexion();
                }
                return vUltimoNotaCredito;
            } catch (GalacException vEx) {
                CerrarConexion();
                throw new LibGalac.Aos.Catching.GalacException("Obtener Ultima Nota de Credito" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerUltimoNumeroReporteZ() {
            string vUltimoNumero = "";
            //ReportData estado;
            //string vRuta = vRutaData();
            try {
                if (AbrirConexion()) {
                    string vCmd = "";
                    //TfhkPrinter.UploadReportCmd(vCmd, vRuta);
                    //estado = TfhkPrinter.GetZReport();
                    //vUltimoNumero = LibConvert.ToStr(estado.NumberOfLastZReport);
                    CerrarConexion();
                }
                return vUltimoNumero;
            } catch (GalacException vEx) {
                CerrarConexion();
                throw new GalacException("Obtener Ultimo Reporte Z " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool CancelarDocumentoFiscalEnImpresion(bool valAbrirConexion) {
            int vResult = 0;
            try {
                if (valAbrirConexion) {
                    AbrirConexion();
                }
                vResult = Bematech_FI_AnulaCupon();
                return (vResult == 1);
            } catch (GalacException vEx) {
                throw new GalacException("Cancelar Documento Fiscal en Impresion " + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        public bool ImprimirFacturaFiscal(XElement valDocumentoFiscal) {
            string vDireccion = LibXml.GetPropertyString(valDocumentoFiscal, "DireccionCliente");
            string vRif = LibXml.GetPropertyString(valDocumentoFiscal, "NumeroRIF");
            string vRazonSocial = LibXml.GetPropertyString(valDocumentoFiscal, "NombreCliente");
            string vVacio = "";
            bool vResult = false;
            try {
                if (AbrirConexion()) {
                    if (AbrirComprobanteFiscal(vRazonSocial, vRif, vDireccion, vVacio, vVacio, vVacio, vVacio, vVacio, vVacio, vVacio)) {
                        vResult = ImprimirTodosLosArticulos(valDocumentoFiscal, false);
                        vResult &= CerrarComprobanteFiscal(valDocumentoFiscal, false);
                        CerrarConexion();
                    }
                } else {
                    throw new GalacException("Error de Conexión con la impresora fiscal", eExceptionManagementType.Controlled);
                }
            } catch (LibGalac.Aos.Catching.GalacException vEx) {
                CerrarConexion();
                throw new GalacException("imprimir factura fiscal " + vEx.Message, eExceptionManagementType.Controlled);
            }
            return vResult;
        }

        private bool AbrirComprobanteFiscal(string valRazonSocial, string valRif, string valDireccion, string valTelefono, string valObservaciones, string refTipo = "", string refNumeroFacturaOriginal = "", string refSerialMaquina = "", string refFecha = "", string refHora = "") {
            try {
                bool vResult = false;
                int vRepuesta = 0;
                string MensajeStatus = "";
                valRazonSocial = LibText.SubString(valRazonSocial, 0, 41);
                valRif = LibText.SubString(valRif, 0, 18);
                valDireccion = LibText.SubString(valDireccion, 0, 133);
                vRepuesta = Bematech_FI_AbreComprobanteDeVentaEx(valRif, valRazonSocial, valDireccion);
                vResult = RetornoStatus(vRepuesta, out MensajeStatus);
                if (!vResult) {
                    throw new GalacException("error al abrir comprobante fiscal " + MensajeStatus, eExceptionManagementType.Controlled);
                }
                return vResult;
            } catch (LibGalac.Aos.Catching.GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private bool CerrarComprobanteFiscal(XElement valDocumentoFiscal, bool valEsNotaDeCredito) {
            int vResult = 0;
            const string AplicaDescuento = "D";
            const string TipoDescuento = "%";
            string valDescuentoTotal = "";
            string vDireccionFiscal = "";
            bool vImprimeDireccionALFinalDeLaFactura = false;
            string vObservaciones = "";
            bool vUsaCamposDefinibles = false;
            bool Seguir = false;
            string vMensaje = "";
            string vTexto = "";
            try {
                vImprimeDireccionALFinalDeLaFactura = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "ImprimeDireccionAlFinalDelComprobanteFiscal");
                vUsaCamposDefinibles = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "UsaCamposDefinibles");
                valDescuentoTotal = LibXml.GetPropertyString(valDocumentoFiscal, "PorcentajeDescuento");
                vDireccionFiscal = LibXml.GetPropertyString(valDocumentoFiscal, "DireccionCliente");
                vObservaciones = LibXml.GetPropertyString(valDocumentoFiscal, "Observaciones");
                valDescuentoTotal = DarFormatoNumericoParaDescuento(valDescuentoTotal);
                vResult = Bematech_FI_IniciaCierreCupon(AplicaDescuento, TipoDescuento, valDescuentoTotal);
                Seguir = EnviarPagos(valDocumentoFiscal);
                if (!vDireccionFiscal.Equals("") && vImprimeDireccionALFinalDeLaFactura) {
                    vTexto = LibText.Left(vDireccionFiscal, 96);
                } else if (LibText.Len(vObservaciones) > 0) {
                    vTexto = LibText.Left(vObservaciones, 96);
                } else if (vUsaCamposDefinibles) {
                    vTexto = ImprimeCamposDefinibles(valDocumentoFiscal);
                }
                vResult = Bematech_FI_FinalizarCierreCupon(vTexto);
                Seguir = RetornoStatus(vResult, out vMensaje);
                if (!Seguir) {
                    throw new GalacException(vMensaje, eExceptionManagementType.Controlled);
                }
                return true;
            } catch (GalacException vEx) {
                throw new GalacException("Cerrar Comprobante" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private string ImprimeCamposDefinibles(XElement valData) {
            string vResult = "";

            List<XElement> vRecord = valData.Descendants("CAMPODEFINIBLE").ToList();
            foreach (XElement vXElement in vRecord) {
                vResult = vResult + LibText.Left(LibText.CleanSpacesToBothSides(LibXml.GetPropertyString(vXElement, "CDEF")) + ",", 41);
            }
            vResult = LibText.SubString(vResult, 0, LibText.Len(vResult) - 1);
            if (LibText.Len(vResult) > 491) {
                vResult = LibText.Left(vResult, 491);
            }
            return vResult;
        }

        private bool ImprimirTodosLosArticulos(XElement valDocumentoFiscal, bool valIsNotaDeCredito) {
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
            const int vCantidaDecimales = 3;
            const string vFormatoDescuento = "%";
            string vSerial;
            string vRollo;
            eStatusImpresorasFiscales PrintStatus;
            eTipoDeAlicuota vAlicuotaEspecial = (eTipoDeAlicuota)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "AplicacionAlicuotaEspecial"));
            DateTime vFechaFactura;
            bool AplicaIVADecreto;

            try {
                vFechaFactura = LibConvert.ToDate(LibString.SubString(LibXml.GetElementValueOrEmpty(valDocumentoFiscal, "FechaDeFacturaAfectada"), 0, 10));
                AplicaIVADecreto = LibConvert.SNToBool(LibXml.GetElementValueOrEmpty(valDocumentoFiscal, "AplicaDecretoIvaEspecial"));
                List<XElement> vRecord = valDocumentoFiscal.Descendants("GpResultDetailRenglonFactura").ToList();
                foreach (XElement vXElement in vRecord) {
                    PrintStatus = EstadoDelPapel(false);
                    if (!PrintStatus.Equals(eStatusImpresorasFiscales.eSinPapel)) {
                        vCodigo = LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement, "Articulo"), 0, 12);
                        vDescripcionResumida = LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement, "Descripcion"), 0, 29);
                        vDescripcionExtendida = LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement, "Descripcion"), 0, 150);
                        vCantidad = LibXml.GetElementValueOrEmpty(vXElement, "Cantidad");
                        vCantidad = DarFormatoNumericoParaImpresion(vCantidad);
                        vMonto = LibXml.GetElementValueOrEmpty(vXElement, "PrecioSinIVA");
                        vMonto = DarFormatoNumericoParaImpresion(vMonto);
                        vTipoAlicuota = (eTipoDeAlicuota)LibConvert.DbValueToEnum(LibXml.GetElementValueOrEmpty(vXElement, "AlicuotaIva"));
                        vPorcentajeAlicuota = LibXml.GetElementValueOrEmpty(vXElement, "PorcentajeAlicuota");
                        vPorcentajeAlicuota = DarFormatoAAlicuotaIva(vPorcentajeAlicuota, vTipoAlicuota);
                        vPrcDescuento = (LibXml.GetElementValueOrEmpty(vXElement, "PorcentajeDescuento1"));
                        vPrcDescuento = DarFormatoNumericoParaDescuento(vPrcDescuento);
                        vSerial = LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement, "Serial"), 20);
                        vRollo = LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement, "Rollo"), 20);
                        vDescripcionExtendida = vDescripcionExtendida + LibText.Space(1) + vSerial + LibText.Space(1) + vRollo;
                        vResultado = vResultado & Bematech_FI_ExtenderDescripcionArticulo(vDescripcionExtendida);
                        vResultado = Bematech_FI_VendeArticulo(vCodigo, vDescripcionResumida, vPorcentajeAlicuota, vFormatoCantidad, vCantidad, vCantidaDecimales, vMonto, vFormatoDescuento, vPrcDescuento);
                        if (vResultado != 1) {
                            throw new GalacException("Error al Imprimir Articulo", eExceptionManagementType.Controlled);
                        }
                        vEstatus = true;
                    } else {
                        vEstatus = false;
                        throw new LibGalac.Aos.Catching.GalacException("Impresora sin papel, colocar otro nuevo", eExceptionManagementType.Controlled);
                    }
                }
            } catch (GalacException vEx) {
                CancelarDocumentoFiscalEnImpresion(false);
                CerrarConexion();
            }
            return vEstatus;
        }

        private string SetDecimalSeparator(string valNumero) {
            string vResult = "";
            if (LibText.IndexOf(valNumero, '.') > 0) {
                vResult = LibText.Replace(valNumero, ".", ",");
            } else {
                vResult = valNumero;
            }
            return vResult;
        }

        private string DarFormatoAAlicuotaIva(string valNumero, eTipoDeAlicuota valTipoAlicuota) {
            string vValorFinal = "";
            string vParteEntera = "";
            string vParteDecimal = "";
            int TokenPosition = 0;

            valNumero = SetDecimalSeparator(valNumero);
            TokenPosition = LibText.InStr(valNumero, ",");
            if (TokenPosition > 0) {
                vParteEntera = LibText.Left(valNumero, TokenPosition);
                vParteEntera = LibText.FillWithCharToLeft(vParteEntera, "0", 2);
                vParteDecimal = LibText.Right(valNumero, LibText.Len(valNumero) - TokenPosition - 1);
                vParteDecimal = LibText.FillWithCharToRight(vParteDecimal, "0", 2);
                vValorFinal = vParteEntera + "," + vParteDecimal;
            } else {
                vValorFinal = valNumero + ",00";
            }

            if (valTipoAlicuota == eTipoDeAlicuota.Exento) {
                vValorFinal = "FF";
            }
            return vValorFinal;
        }

        private string DarFormatoNumericoParaDescuento(string valNumero) {
            string vResult = "";
            int TokenPosition = 0;
            string vParteEntera = "";
            string vParteDecimal = "";

            vResult = SetDecimalSeparator(valNumero);
            TokenPosition = LibText.IndexOf(vResult, ",");
            if (TokenPosition > 0) {
                vParteEntera = LibText.Left(valNumero, TokenPosition);
                vParteEntera = LibText.FillWithCharToLeft(vParteEntera, "0", 2);
                vParteDecimal = LibText.Right(valNumero, LibText.Len(valNumero) - TokenPosition - 1);
                vParteDecimal = LibText.FillWithCharToRight(vParteDecimal, "0", 2);
                vResult = vParteEntera + "," + vParteDecimal;
            } else {
                vParteEntera = LibText.Left(valNumero, TokenPosition);
                vParteEntera = LibText.FillWithCharToRight(vParteEntera, "0", 2);
                vResult = vParteEntera + ",00";
            }
            return vResult;
        }

        private string DarFormatoNumericoParaImpresion(string valNumero) {
            string vResult = "";
            int TokenPosition = 0;
            string vParteEntera = "";
            string vParteDecimal = "";

            vResult = SetDecimalSeparator(valNumero);
            TokenPosition = LibText.IndexOf(vResult, ",");
            if (TokenPosition > 0) {
                vParteEntera = LibText.Left(vResult, TokenPosition + 1);
                vParteDecimal = LibText.Right(vResult, LibText.Len(vResult) - TokenPosition - 1);
                vParteDecimal = LibText.FillWithCharToRight(vParteDecimal, "0", 3);
                vResult = vParteEntera + vParteDecimal;
            } else {
                vResult = vResult + ",000";
            }
            return vResult;
        }

        private string DarFormatoNumericoParaLosPagos(string valNumero) {
            string vResult = "";
            int TokenPosition = 0;
            string vParteEntera = "";
            string vParteDecimal = "";

            vResult = SetDecimalSeparator(valNumero);
            TokenPosition = LibText.IndexOf(vResult, ",");
            if (TokenPosition > 0) {
                vParteEntera = LibText.Left(valNumero, TokenPosition);
                vParteDecimal = LibText.Right(valNumero, LibText.Len(valNumero) - TokenPosition - 1);
                vParteDecimal = LibText.FillWithCharToRight(vParteDecimal, "0", 2);
                vResult = vParteEntera + "," + vParteDecimal;
            } else {
                vResult = vResult + ",00";
            }
            return vResult;
        }

        private bool RetornoStatus(int valStatus, out string valMensajeRetorno) {
            bool vResult = false;
            valMensajeRetorno = "";
            switch (valStatus) {
                case 0:
                    vResult = false;
                    valMensajeRetorno = " Error de Comunicación";
                    break;
                case 1:
                    vResult = true;
                    break;
                case -1:
                    vResult = false;
                    valMensajeRetorno = " Error de Ejecucion, comando no Ejecutado";
                    break;
                case -2:
                    vResult = false;
                    valMensajeRetorno = " Parametro Invalido en el comando";
                    break;
                case -3:
                    vResult = false;
                    valMensajeRetorno = " Alícuota no Programada";
                    break;
                case -4:
                    vResult = false;
                    valMensajeRetorno = " Archivo  BEMAFI32.ini no encontrado en el directorio del sistema";
                    break;
                case -5:
                    vResult = false;
                    valMensajeRetorno = " Error al abrir Puerto de Comunicaciones";
                    break;
                case -8:
                    vResult = false;
                    valMensajeRetorno = " Error al guardar o crear el archivo Status.TXT o Retorno.txt ";
                    break;
                case -27:
                    vResult = false;
                    valMensajeRetorno = " Estatus de la Impresora Fiscal Distinto de (6,0,0) ";
                    break;
            }
            return vResult;
        }

        private bool RetornoStatus1(int valStatus, out  string valMensajeRetorno) {
            bool vResult = true;
            valMensajeRetorno = "";

            if (valStatus >= 128) { // bit 7 
                valStatus = valStatus - 128;
                valMensajeRetorno = valMensajeRetorno + " Fin del Papel";
                vResult = false;
            }
            if (valStatus >= 64) { // bit 6 
                valStatus = valStatus - 64;
                valMensajeRetorno = valMensajeRetorno + " Poco Papel";
                vResult = false;
            }
            if (valStatus >= 32) { // bit 5 
                valStatus = valStatus - 32;
                valMensajeRetorno = valMensajeRetorno + " Error en el Relój";
                vResult = false;
            }
            if (valStatus >= 16) { // bit 4 
                valStatus = valStatus - 16;
                valMensajeRetorno = valMensajeRetorno + " Impresora con Error";
                vResult = false;
            }
            if (valStatus >= 8) { // bit 3 
                valStatus = valStatus - 8;
                valMensajeRetorno = valMensajeRetorno + " Comando no empieza con ESC";
                vResult = false;
            }
            if (valStatus >= 4) { // bit 2 
                valStatus = valStatus - 4;
                valMensajeRetorno = valMensajeRetorno + " Comando Inexistente";
                vResult = false;
            }
            if (valStatus >= 2) { // bit 1 
                valStatus = valStatus - 2;
                valMensajeRetorno = valMensajeRetorno + " Cupón Abierto";
                vResult = false;
            }
            if (valStatus >= 1) { // bit 0 
                valStatus = valStatus - 1;
                valMensajeRetorno = valMensajeRetorno + " Número de Parámetro(s) Inválido(s)";
                vResult = false;
            }
            if (valStatus == 0) { // bit 0 
                valStatus = valStatus - 1;
                valMensajeRetorno = valMensajeRetorno + "";
                //vResult = true;
            }
            return vResult;
        }

        private bool RetornoStatus2(int valStatus, out  string valMensajeRetorno) {
            bool vResult = false;
            valMensajeRetorno = "";

            if (valStatus >= 128) { // bit 7 
                valStatus = valStatus - 128;
                valMensajeRetorno = valMensajeRetorno + " Comando Invalido";
                vResult = false;
            }
            if (valStatus >= 64) { // bit 6 
                valStatus = valStatus - 64;
                valMensajeRetorno = valMensajeRetorno + " Memoria Fiscal Llena";
                vResult = false;
            }
            if (valStatus >= 32) { // bit 5 
                valStatus = valStatus - 32;
                valMensajeRetorno = " Error en memoria RAM";
                vResult = false;
            }
            if (valStatus >= 16) { // bit 4 
                valStatus = valStatus - 16;
                valMensajeRetorno = valMensajeRetorno + " Alicuota no programada";
                vResult = false;
            }
            if (valStatus >= 8) { // bit 3 
                valStatus = valStatus - 8;
                valMensajeRetorno = valMensajeRetorno + " Capacidad de Alicuota Llena";
                vResult = false;
            }
            if (valStatus >= 4) { // bit 2 
                valStatus = valStatus - 4;
                valMensajeRetorno = valMensajeRetorno + " No se permite Cancelar";
                vResult = false;
            }
            if (valStatus >= 2) { // bit 1 
                valStatus = valStatus - 2;
                valMensajeRetorno = valMensajeRetorno + " RIF del propietario No está programado en la impresora";
                vResult = false;
            }
            if (valStatus >= 1) { // bit 0 
                valStatus = valStatus - 1;
                valMensajeRetorno = valMensajeRetorno + " Comando no Ejecutado";
                vResult = false;
            }
            if (valStatus == 0) { // bit 0 
                valStatus = valStatus - 1;
                valMensajeRetorno = valMensajeRetorno + "";
                vResult = true;
            }
            return vResult;
        }

        private bool RetornoACK(int valACK, ref  string refMensajeRetorno) {
            bool vResult = false;
            refMensajeRetorno = "";

            switch (valACK) {
                case 6:
                    refMensajeRetorno = "";
                    vResult = true;
                    break;
                case 21:
                    refMensajeRetorno = "Comando no Reconocido";
                    vResult = false;
                    break;
            }
            return vResult;
        }

        private bool RevisarEstadoImpresora(ref  string refMensajeRetorno) {
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
            } catch (GalacException vEx) {
                throw new GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private bool EnviarPagos(XElement valMedioDePago) {
            string vMedioDePago = "";
            string vMontoCancelado = "";
            int vResult = 0;

            try {
                List<XElement> vNodos = valMedioDePago.Descendants("GpResultDetailRenglonCobro").ToList();
                if (vNodos.Count > 0) {
                    foreach (XElement vXElement in vNodos) {
                        vMedioDePago = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "CodigoFormaDelCobro"));
                        vMontoCancelado = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "Monto"));
                        vMedioDePago = FormaDeCobro(vMedioDePago);
                        vMontoCancelado = DarFormatoNumericoParaLosPagos(vMontoCancelado);
                        vResult = Bematech_FI_EfectuaFormaPagoDescripcionForma(vMedioDePago, vMontoCancelado, "");
                    }
                }
                return true;
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
                    vResultado = "Cheque";
                    break;
                case "00003":
                    vResultado = "Tarjeta";
                    break;
                case "00004":
                    vResultado = "Deposito";
                    break;
                default:
                    vResultado = "Efectivo";
                    break;
            }
            return vResultado;
        }

        public bool ImprimirNotaCredito(XElement valDocumentoFiscal) {
            string vDireccion = LibXml.GetPropertyString(valDocumentoFiscal, "Direccion");
            string vRif = LibXml.GetPropertyString(valDocumentoFiscal, "RIF");
            string vRazonSocial = LibXml.GetPropertyString(valDocumentoFiscal, "RAZON_SOCIAL");
            string vObservaciones = LibXml.GetPropertyString(valDocumentoFiscal, "OBSERVACION");
            string vFacturaAfectada = LibXml.GetPropertyString(valDocumentoFiscal, "NUMERO_FACTURA_AFECTADA");
            string vSerialMaquina = LibXml.GetPropertyString(valDocumentoFiscal, "SERIAL_MAQUINA");
            string vFecha = LibXml.GetPropertyString(valDocumentoFiscal, "FECHA");
            string vHora = LibXml.GetPropertyString(valDocumentoFiscal, "HORA");
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

    }
}
