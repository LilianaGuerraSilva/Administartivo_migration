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
using System.IO;

namespace Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal {
    public class clsBematech:IImpresoraFiscalPdn {
        #region comandos
        #region Funciones de Inicialización
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ProgramaAlicuota(string Alicuota,int ICMS_ISS);
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
        public static extern int Bematech_FI_AbreComprobanteDeVenta(string RIF,string Nombre);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreComprobanteDeVentaEx(string RIF,string Nombre,string Direccion);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VendeArticulo(string Codigo,string Descripcion,string Alicuota,string TipoCantidad,string Cantidad,int CasasDecimales,string ValorUnitario,string TipoDescuento,string Descuento);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ExtenderDescripcionArticulo(string DescripcionExt);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AnulaArticuloAnterior();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AnulaCupon();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CierraCupon(string FormaPago,string IncrementoDescuento,string TipoIncrementoDescuento,string ValorIncrementoDescuento,string ValorPago,string Mensaje);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_IniciaCierreCupon(string IncrementoDescuento,string TipoIncrementoDescuento,string ValorIncrementoDescuento);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_EfectuaFormaPago(string FormaPago,string ValorFormaPago);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FinalizaCierreCupon(string Mensaje);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_DevolucionArticulo(string Codigo,string Descripcion,string Alicuota,string TipoCantidad,string Cantidad,int CasasDecimales,string ValorUnit,string TipoDescuento,string ValorDesc);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreNotaDeCredito(string Nombre,string NumeroSerie,string RIF,string Dia,string Mes,string Ano,string Hora,string Minuto,string Segundo,string COO);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_EfectuaFormaPagoDescripcionForma(string FormaDePago,string MontoCancelado,string DescripcionPago);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FinalizarCierreCupon(string vParametros);
        [DllImport("BemaFI32.dll")]
        public static extern int Bematech_FI_NumeroComprobanteFiscal([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroComprobante);
        [DllImport("BemaFI32.dll")]
        public static extern int Bematech_FI_NumeroReducciones([MarshalAs(UnmanagedType.VBByRefStr)] ref string NumeroReducciones);
        #endregion

        #region Funciones de los Informes Fiscales
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LecturaX();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_LecturaXSerial();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ReduccionZ(string FechaINI,string FechaFIN);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_InformeGerencial(string Texto);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CierraInformeGerencial();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_FlagFiscalesIII(int Flag);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_InformeTransacciones(string tipo,string Fechaini,string Fechafim,string Opcion);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CierraInformeXoZ();
        #endregion

        #region Funciones de las Operaciones No Fiscales
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_RecebimientoNoFiscal(string IndiceTotalizador,string Valor,string FormaPago);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_AbreComprobanteNoFiscalVinculado(string FormaPago,string Valor,string NumeroCupon);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_ImprimeComprobanteNoFiscalVinculado(string Texto);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_CierraComprobanteNoFiscalVinculado();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_Sangria(string Valor);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_Provision(string Valor,string FormaPago);
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
        public static extern int Bematech_FI_FechaHoraImpresora([MarshalAs(UnmanagedType.VBByRefStr)] ref string Fecha,[MarshalAs(UnmanagedType.VBByRefStr)] ref string Hora);
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
        public static extern int Bematech_FI_RetornoImpresora(ref int ACK,ref int ST1,ref int ST2);
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaImpresoraPrendida();
        [DllImport("BemaFi32.dll")]
        public static extern int Bematech_FI_VerificaEstadoImpresora(ref int ACK,ref int ST1,ref int ST2);
        #endregion
        #endregion

        #region Constantes
        const string MinVersionApi = "5, 4, 1, 0";
        const string MaxVersionApi = "5, 4, 1, 32";
        const string DllApiName = @"BemaFI32.dll";               
        const byte _EnterosMontosLargos = 10;
        const byte _EnterosCantidad = 4;
        const byte _EnterosMontosCortos = 2;
        const byte _Decimales3Digitos = 3;                
        const byte _Decimales2Digitos = 2;
        #endregion Constantes

        private string CommPort = "";
        private eImpresoraFiscal vModelo;
        private bool _PortIsOpen = false;

        public clsBematech(XElement valXmlDatosImpresora) {
            ConfigurarPuerto(valXmlDatosImpresora);
        }

        private void ConfigurarPuerto(XElement valXmlDatosImpresora) {
            bool vResult = false;
            bool EsLogFile = false;
            ePuerto ePuerto = (ePuerto)LibConvert.DbValueToEnum(LibXml.GetPropertyString(valXmlDatosImpresora,"PuertoMaquinaFiscal"));
            string valPuerto = ePuerto.GetDescription(1);
            vModelo = (eImpresoraFiscal)LibConvert.DbValueToEnum(LibXml.GetPropertyString(valXmlDatosImpresora,"ModeloDeMaquinaFiscal"));
            string vBemaIniPath = PathFile(EsLogFile);

            try {
                if(!LibConvert.IsNumeric(valPuerto)) {
                    CommPort = "USB";
                } else {
                    CommPort = "COM" + valPuerto;
                }
                if(LibFile.FileExists(vBemaIniPath)) {
                    vResult = EditFileBemaINI(vBemaIniPath,"Puerta",CommPort);
                }
            } catch(Exception vEx) {
                throw new GalacException(vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        private bool EditFileBemaINI(string valBemaPath,string valParameter,string valComPort) {
            try {
                string vText = LibFile.ReadFile(valBemaPath);
                string[] vLines = LibText.Split(vText,"\r\n",true);
                var replaced = vLines.Select(x => {
                    if(x.StartsWith(valParameter)) {
                        return valParameter + "=" + valComPort;
                    } else if(x.StartsWith("Path=")) {
                        return @"Path=C:\Bema\";
                    }
                    return x;
                });
                vText = string.Join(LibText.CRLF(),replaced);
                LibIO.WriteLineInFile(valBemaPath,vText,false);
                return true;
            } catch(Exception vEx) {
                throw new GalacException("BemaFi32.INI" + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerFechaYHora() {
            string vResult = "";
            int retorno = 0;
            string vFecha = LibText.Space(6);
            string vHora = LibText.Space(6);
            retorno = Bematech_FI_FechaHoraImpresora(ref vFecha,ref vHora);
            if(retorno.Equals(1)) {
                vFecha = LibText.SubString(vFecha,0,2) + "/" + LibText.SubString(vFecha,2,2) + "/" + LibText.SubString(vFecha,4,2);
                vHora = LibText.SubString(vHora,0,2) + ":" + LibText.SubString(vHora,2,2);
                vResult = vFecha + LibText.Space(1) + vHora;
            }
            return vResult;
        }

        private string PathFile(bool valEsREtorno=false) {
            try {
                string vresult = "";
                if(valEsREtorno) {
                    if(LibFile.FileExists(@"C:\Bema")) {
                        vresult = @"\C:\Bema\Retorno.txt";
                    } else {
                        LibFile.CreateDir(@"C:\Bema");
                        vresult = @"C:\Bema\Retorno.txt";
                    }
                } else {
                    if(Environment.Is64BitOperatingSystem) {
                        vresult = Environment.GetFolderPath(Environment.SpecialFolder.SystemX86) + @"\BemaFi32.ini";
                    } else {
                        vresult = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\BemaFi32.ini";
                    }
                    if(!LibFile.FileExists(vresult)) {
                        throw new GalacException("el archivo de configuración BemaFi32.ini no existe o no fue suministrado ",eExceptionManagementType.Controlled);
                    }
                }
                return vresult;
            } catch(System.DllNotFoundException vEx) {
                throw new GalacException(vEx.Message,eExceptionManagementType.Controlled);
            } catch(Exception vEx) {
                throw new GalacException("BemaFI32.INI " + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        public bool AbrirConexion() {
            bool vResult = false;
            try {
                if(!_PortIsOpen) {
                    vResult = (Bematech_FI_AbrePuertaSerial() != -5);
                    _PortIsOpen = true;
                } else {
                    _PortIsOpen = true;
                    vResult = _PortIsOpen;
                }
                return vResult;
            } catch(System.DllNotFoundException vEx) {
                throw new GalacException(vEx.Message,eExceptionManagementType.Controlled);
            } catch(Exception vEx) {
                throw new GalacException("Abrir Conexion " + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        public bool CerrarConexion() {
            bool vResult = false;
            try {
                if(_PortIsOpen) {
                    vResult = (Bematech_FI_CierraPuertaSerial() == 1);
                    Thread.Sleep(250);
                    _PortIsOpen = false;
                } else {
                    vResult = true;
                }
                return vResult;
            } catch(Exception vEx) {
                throw new GalacException("Cerrar Conexion " + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        public bool ComprobarEstado() {
            bool vResult = false;
            int valStatus = 0;
            string MensajeStatus = "";
            valStatus = Bematech_FI_VerificaImpresoraPrendida();
            if(RetornoStatus(valStatus,out MensajeStatus)) {
                vResult = true;
            }
            return vResult;
        }

        public string ObtenerSerial(bool valAbrirConexion) {
            string vSerial = LibText.Space(15);
            string MensajeStatus = "";
            int vRepuesta = 0;
            bool vResult;

            try {
                if(valAbrirConexion) {
                    AbrirConexion();
                }
                vRepuesta = Bematech_FI_NumeroSerie(ref vSerial);
                vResult = vRepuesta.Equals(1);                
                if(vResult) {
                    vSerial = LibText.Trim(vSerial);
                } else {
                    MensajeStatus += "\r\nError de comunicación revisar puertos y conexiones";
                    throw new GalacException(MensajeStatus,eExceptionManagementType.Controlled);
                }
                if(valAbrirConexion) {
                    CerrarConexion();
                }
                return vSerial;
            } catch(Exception vEx) {
                throw new GalacException("Obtener serial: " + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        public eStatusImpresorasFiscales EstadoDelPapel(bool valAbrirPuerto) {
            eStatusImpresorasFiscales vResult = eStatusImpresorasFiscales.eListoEnEspera;
            int vACK = 0;
            int vST1 = 0;
            int vST2 = 0;
            int vRepuesta = 0;

            try {
                if(valAbrirPuerto) {
                    AbrirConexion();
                }
                vRepuesta = Bematech_FI_VerificaEstadoImpresora(ref vACK,ref vST1,ref vST2);
                switch(vST1) {
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
                if(valAbrirPuerto) {
                    CerrarConexion();
                }
                return vResult;
            } catch(Exception vEx) {
                throw new GalacException("Estado del Papel: " + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        public bool RealizarReporteZ() {
            bool vResult = false;
            int vEstatus = 0;
            string MensajeStatus = "";
            try {
                AbrirConexion();
                vEstatus = Bematech_FI_ReduccionZ("","");
                vResult = RevisarEstadoImpresora(ref MensajeStatus);
                Thread.Sleep(400);
                CerrarConexion();
                if(!vResult) {
                    throw new GalacException(MensajeStatus,eExceptionManagementType.Controlled);
                }
                return vResult;
            } catch(Exception vEx) {
                throw new GalacException("Realizar Cierre Diario: " + vEx.Message,eExceptionManagementType.Controlled);
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
                if(!vResult) {
                    throw new GalacException("Realizar Cierre X: " + MensajeStatus,eExceptionManagementType.Controlled);
                }
                return vResult;
            } catch(Exception vEx) {
                throw new GalacException(vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerUltimoNumeroFactura(bool valAbrirConexion) {
            string vUltimaFactura = LibText.Space(8);
            int vRetorno = 0;
            bool vResult = false;
            string MensajeStatus = "";

            try {
                if(valAbrirConexion) {
                    AbrirConexion();
                }
                vRetorno = Bematech_FI_LecturaXSerial();
                vResult = RevisarEstadoImpresora(ref MensajeStatus);
                MensajeStatus = LibText.CleanSpacesToBothSides(MensajeStatus);               
                if(vResult) {
                    vUltimaFactura = LeeDeArchivoDeRetorno();
                    MensajeStatus = LibText.CleanSpacesToBothSides(MensajeStatus);
                } else {
                    MensajeStatus += "\r\nError de comunicación revisar puertos y conexiones";
                    throw new GalacException(MensajeStatus,eExceptionManagementType.Controlled);
                }
                if(valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimaFactura;
            } catch(Exception vEx) {
                CerrarConexion();
                throw new GalacException("Obtener último número de factura: " + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerUltimoNumeroNotaDeCredito(bool valAbrirConexion) {
            string vUltimaNotaCredito = "";
            int vRetorno = 0;
            bool vResult = false;
            string MensajeStatus = "";

            try {
                if(valAbrirConexion) {
                    AbrirConexion();
                }
                vRetorno = Bematech_FI_LecturaXSerial();
                vResult = RevisarEstadoImpresora(ref MensajeStatus);
                MensajeStatus = LibText.CleanSpacesToBothSides(MensajeStatus);                
                if(vResult) {
                    vUltimaNotaCredito = LeeDeArchivoDeRetorno(true);
                } else {
                    MensajeStatus += "\r\nError de comunicación revisar puertos y conexiones";
                    throw new GalacException(MensajeStatus,eExceptionManagementType.Controlled);
                }
                if(valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimaNotaCredito;
            } catch(Exception vEx) {
                CerrarConexion();
                throw new GalacException("Obtener último número Nota de Crédito: " + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        private string LeeDeArchivoDeRetorno(bool esNotaDeCredito = false) {
            string vResult = "";
            string vPath = "";
            string vTextoBusqueda = "";            
            int vPosicion = 0;
            vPath = PathFile(true);
            vTextoBusqueda = (esNotaDeCredito == true) ? "Contador de Nota de Crédito:" : "Contador de Factura:";
            if(LibFile.FileExists(vPath)) {
                vResult = LibFile.ReadFile(vPath);
                vPosicion = LibString.InStr(vResult,vTextoBusqueda) + LibString.Len(vTextoBusqueda);
                vResult = LibString.SubString(vResult,vPosicion,30);
                vPosicion = LibString.InStr(vResult,"\n");
                vResult = LibString.SubString(vResult,0,vPosicion);
                vResult = LibString.Trim(vResult);
                LibFile.DeleteFile(vPath);
            } else {
                vResult = "00000001";
            }
            return vResult;
        }

        public string ObtenerUltimoNumeroReporteZ(bool valAbrirConexion) {
            string vUltimoNumeroDeReporteZ = new string('\u0020',4);
            int vRetorno = 0;
            bool vResult = false;
            string MensajeStatus = "";
            try {
                if(valAbrirConexion) {
                    AbrirConexion();
                }
                vRetorno = Bematech_FI_NumeroReducciones(ref vUltimoNumeroDeReporteZ);
                vResult = RevisarEstadoImpresora(ref MensajeStatus);                
                vResult = RevisarEstadoImpresora(ref MensajeStatus);
                if(vResult) {
                    MensajeStatus = LibText.CleanSpacesToBothSides(MensajeStatus);
                } else {
                    MensajeStatus += "\r\nError de comunicación revisar puertos y conexiones";
                    throw new GalacException(MensajeStatus,eExceptionManagementType.Controlled);
                }
                if(valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimoNumeroDeReporteZ;
            } catch(Exception vEx) {
                CerrarConexion();
                throw new GalacException("Obtener último reporte Z: " + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        public bool CancelarDocumentoFiscalEnImpresion(bool valAbrirConexion) {
            int vResult = 0;
            try {
                if(valAbrirConexion) {
                    AbrirConexion();
                }
                vResult = Bematech_FI_AnulaCupon();
                if(valAbrirConexion) {
                    CerrarConexion();
                }
                return (vResult == 1);
            } catch(Exception vEx) {
                throw new GalacException("Cancelar Documento Fiscal en Impresion: " + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        public bool ImprimirNotaCredito(XElement valDocumentoFiscal) {
            string vRif = LibText.Trim(LibXml.GetPropertyString(valDocumentoFiscal,"NumeroRIF"));
            string vRazonSocial = LibText.Trim(LibXml.GetPropertyString(valDocumentoFiscal,"NombreCliente"));
            string vNumeroComprobanteFiscal = LibString.Trim(LibXml.GetPropertyString(valDocumentoFiscal,"NumeroComprobanteFiscal"));
            string vSerialMaquina = LibString.Trim(LibXml.GetPropertyString(valDocumentoFiscal,"SerialMaquinaFiscal"));
            string vFecha = LibString.Trim(LibXml.GetPropertyString(valDocumentoFiscal,"Fecha"));
            string vHora = LibString.Trim(LibXml.GetPropertyString(valDocumentoFiscal,"HoraModificacion"));
            if(LibText.IndexOf(vFecha,"-") > 0) {
                vFecha = LibString.Replace(vFecha,"/","/");
            }

            bool vResult = false;
            try {
                AbrirConexion();
                CancelarDocumentoFiscalEnImpresion(false);
                if (AbrirNotaDeCredito(vRazonSocial,vRif,vNumeroComprobanteFiscal,vSerialMaquina,vFecha,vHora)) {
                    vResult = ImprimirTodosLosArticulos(valDocumentoFiscal);
                    vResult &= CerrarComprobanteFiscal(valDocumentoFiscal,true);
                }
                CerrarConexion();
            } catch(Exception vEx) {
                CerrarConexion();
                throw new GalacException("imprimir factura fiscal: " + vEx.Message,eExceptionManagementType.Controlled);
            }
            return vResult;
        }


        public bool ImprimirFacturaFiscal(XElement valDocumentoFiscal) {
            string vDireccion = LibXml.GetPropertyString(valDocumentoFiscal,"DireccionCliente");
            string vRif = LibXml.GetPropertyString(valDocumentoFiscal,"NumeroRIF");
            string vRazonSocial = LibXml.GetPropertyString(valDocumentoFiscal,"NombreCliente");
            string vVacio = "";            
            bool vResult = false;
            try {
                AbrirConexion();
                CancelarDocumentoFiscalEnImpresion(false);
                if(AbrirComprobanteFiscal(vRazonSocial,vRif,vDireccion)) {
                    vResult = ImprimirTodosLosArticulos(valDocumentoFiscal);
                    vResult &= CerrarComprobanteFiscal(valDocumentoFiscal);
                }
                CerrarConexion();
            } catch(Exception vEx) {
                CerrarConexion();
                throw new GalacException("imprimir factura fiscal: " + vEx.Message,eExceptionManagementType.Controlled);
            }
            return vResult;
        }

        private bool AbrirComprobanteFiscal(string valRazonSocial,string valRif,string valDireccion) {
            try {
                bool vResult = false;
                int vRepuesta = 0;
                string MensajeStatus = "";
                valRazonSocial = LibText.SubString(valRazonSocial,0,41);
                valRif = LibText.SubString(valRif,0,18);
                valDireccion = LibText.SubString(valDireccion,0,133);
                vRepuesta = Bematech_FI_AbreComprobanteDeVentaEx(valRif,valRazonSocial,valDireccion);
                vResult = RetornoStatus(vRepuesta,out MensajeStatus);
                if(!vResult) {
                    throw new GalacException("error al abrir comprobante fiscal: " + MensajeStatus,eExceptionManagementType.Controlled);
                }
                return vResult;
            } catch(Exception vEx) {
                throw new GalacException(vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        private bool AbrirNotaDeCredito(string valRazonSocial,string valRif,string valNumeroComprobanteFiscal,string valSerialMaquina,string valFecha,string valHora) {
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
               
                valNumeroComprobanteFiscal = LibText.Right(valNumeroComprobanteFiscal,6);
                valSerialMaquina = LibText.SubString(valSerialMaquina,0,10);
                valRazonSocial = LibText.SubString(valRazonSocial,0,38);
                valRif = LibText.SubString(valRif,0,14);
                vDia = LibText.SubString(valFecha,0,2);
                vMes = LibText.SubString(valFecha,3,2);
                vAno = LibText.SubString(valFecha,8,2);
                vHora = LibText.SubString(valHora,0,2);
                vMinuto = LibText.SubString(valHora,3,2);
                vSegundo = LibText.SubString(valHora,6,2);
                if(LibString.Len(vSegundo) <= 1) {
                    vSegundo = LibText.FillWithCharToRight(vSegundo,"0",2);
                }
                vRepuesta = Bematech_FI_AbreNotaDeCredito(valRazonSocial,valSerialMaquina,valRif,vDia,vMes,vAno,vHora,vMinuto,vSegundo,valNumeroComprobanteFiscal);
                vResult = RetornoStatus(vRepuesta,out MensajeStatus);
                if(!vResult) {
                    throw new GalacException("error al abrir Nota de Credito: " + MensajeStatus,eExceptionManagementType.Controlled);
                }
                return vResult;
            } catch(Exception vEx) {
                throw new GalacException(vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        private bool CerrarComprobanteFiscal(XElement valDocumentoFiscal,bool valEsNotaDeCredito = false) {
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
            try {
                vImprimeDireccionALFinalDeLaFactura = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida","ImprimeDireccionAlFinalDelComprobanteFiscal");
                valDescuentoTotal = LibXml.GetPropertyString(valDocumentoFiscal,"PorcentajeDescuento");
                valDescuentoTotal = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(valDescuentoTotal,_EnterosMontosLargos,_Decimales2Digitos,",");
                vDireccionFiscal = LibXml.GetPropertyString(valDocumentoFiscal,"DireccionCliente");
                vObservaciones = LibXml.GetPropertyString(valDocumentoFiscal,"Observaciones");
                vTotalMonedaExtranjera = LibXml.GetPropertyString(valDocumentoFiscal,"TotalMonedaExtranjera");
                vResult = Bematech_FI_IniciaCierreCupon(AplicaDescuento,TipoDescuento,valDescuentoTotal);
                Seguir = EnviarPagos(valDocumentoFiscal);
                if(vModelo == eImpresoraFiscal.BEMATECH_MP_20_FI_II && valEsNotaDeCredito) {
                    vResult = Bematech_FI_FinalizarCierreCupon(vTexto);
                    Seguir = RetornoStatus(vResult,out vMensaje);
                    return Seguir;
                } else {
                    if(LibText.Len(vTotalMonedaExtranjera) > 0 && !valEsNotaDeCredito) {
                        vCaracteresRestantes = LibString.Len(vTotalMonedaExtranjera);
                        vTexto += vTotalMonedaExtranjera;
                    } else if(valEsNotaDeCredito) {
                        vTotalMonedaExtranjera = "";
                    }
                    if(LibText.Len(vObservaciones) > 0) {
                        if (LibText.S1IsInS2("Total", vObservaciones)) {
                            vObservaciones = LibText.Replace(vObservaciones, "Total","Tot..");
                        }
                        vTexto = (LibString.IsNullOrEmpty(vTexto) ? "" : vTexto + "\r\n");
                        vTexto += LibText.Left("Obs.:" + vObservaciones,Math.Abs(320 - vCaracteresRestantes));
                        vCaracteresRestantes = LibString.Len(vTexto) - vCaracteresRestantes;
                    } else {
                        vCaracteresRestantes = 1;
                    }
                    vSinDreccionSinObservaciones = (LibString.Len(vDireccionFiscal) == 0 && LibString.Len(vObservaciones) == 0);
                    if(LibString.Len(vTotalMonedaExtranjera) == 0 && vCaracteresRestantes > 0) {
                        vCamposDef = ImprimeCamposDefinibles(valDocumentoFiscal,vSinDreccionSinObservaciones);
                        vCamposDef = (LibString.IsNullOrEmpty(vCamposDef) ? "" : "\r\n" + vCamposDef);
                        vTexto += vCamposDef;
                    }
                }
                vResult = Bematech_FI_FinalizarCierreCupon(vTexto);
                Seguir = RetornoStatus(vResult,out vMensaje);
                if(!Seguir) {
                    throw new GalacException(vMensaje,eExceptionManagementType.Controlled);
                }
                return Seguir;
            } catch(Exception vEx) {
                throw new GalacException("Cerrar Comprobante: " + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        private string ImprimeCamposDefinibles(XElement valData,bool valSinDreccionSinObservaciones) {
            string vResult = "";
            byte vTope = 0;
            byte vCuentaLineas = 0;
            if(valSinDreccionSinObservaciones) {
                vTope = 7;
            } else {               
                vTope = 5;
            }
            List<XElement> vCamposDefinibles = valData.Descendants("GpResultDetailCamposDefinibles").ToList();
            if(vCamposDefinibles.Count > 0) {
                foreach(XElement vRecord in vCamposDefinibles) {
                    if(vCuentaLineas > vTope) {
                        break;
                    }
                    vResult += LibText.Left(LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vRecord,"CampoDefinibleValue")),48) + "\r\n";
                    vCuentaLineas++;
                }
                vResult = LibText.SubString(vResult,0,LibText.Len(vResult) - 2);
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
                foreach(XElement vXElement in vRecord) {
                    PrintStatus = EstadoDelPapel(false);
                    vCodigo = LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement,"Articulo"),0,12);
                    vDescripcionResumida = LibImpresoraFiscalUtil.CadenaCaracteresValidos(LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement,"Descripcion"),0,29));
                    vDescripcionExtendida = LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement,"Descripcion"),0,150);
                    vCantidad = LibXml.GetElementValueOrEmpty(vXElement,"Cantidad");                    
                    vCantidad = DarFormatoNumericoYCompletaConCero(vCantidad, eTipoDeAlicuota.Exento, _EnterosCantidad, _Decimales3Digitos);                    
                    vMonto = LibXml.GetElementValueOrEmpty(vXElement,"PrecioSinIVA");
                    vMonto = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vMonto,_EnterosMontosLargos,_Decimales2Digitos,",");
                    vTipoAlicuota = (eTipoDeAlicuota)LibConvert.DbValueToEnum(LibXml.GetElementValueOrEmpty(vXElement,"AlicuotaIva"));
                    vPorcentajeAlicuota = LibXml.GetElementValueOrEmpty(vXElement,"PorcentajeAlicuota");
                    vPorcentajeAlicuota = DarFormatoNumericoYCompletaConCero(vPorcentajeAlicuota,vTipoAlicuota,_EnterosMontosCortos,_Decimales2Digitos,true);
                    vPrcDescuento = (LibXml.GetElementValueOrEmpty(vXElement,"PorcentajeDescuento"));
                    vPrcDescuento = DarFormatoNumericoYCompletaConCero(vPrcDescuento,vTipoAlicuota,_EnterosMontosCortos,_Decimales2Digitos);
                    vSerial = LibXml.GetElementValueOrEmpty(vXElement,"Serial");
                    vRollo = LibXml.GetElementValueOrEmpty(vXElement,"Rollo");
                    if(LibString.Len(vSerial) > 0) {
                        vSerial = "\u0020" + LibText.SubString(vSerial,0,20);
                    }
                    if(LibString.Len(vRollo) > 0) {
                        vRollo = "\u0020" + LibText.SubString(vRollo,0,20);
                    }
                    vDescripcionExtendida = vDescripcionExtendida + (LibString.IsNullOrEmpty(vSerial) ? "" : vSerial) + (LibString.IsNullOrEmpty(vRollo) ? "" : vRollo);
                    vResultado = Bematech_FI_ExtenderDescripcionArticulo(vDescripcionExtendida);
                    vEstatus = RetornoStatus(vResultado,out vMensaje);
                    vResultado = Bematech_FI_VendeArticulo(vCodigo,vDescripcionResumida,vPorcentajeAlicuota,vFormatoCantidad,vCantidad,vCantidaDecimales,vMonto,vFormatoDescuento,vPrcDescuento);
                    vEstatus &= RetornoStatus(vResultado,out vMensaje);
                    if(vResultado != 1) {
                        throw new GalacException("Error al Imprimir Articulo " + vMensaje,eExceptionManagementType.Controlled);
                    }
                }
                return vEstatus;
            } catch(Exception vEx) {
                CancelarDocumentoFiscalEnImpresion(false);
                CerrarConexion();
                throw (vEx);
            }
        }

        private string DarFormatoNumericoYCompletaConCero(string valNumero, eTipoDeAlicuota valTipoAlicuota, byte valCantidadEnteros, byte valCantidadDecimales, bool valEsAlicuota = false) {
            string vValorFinal = "";
            if (valTipoAlicuota == eTipoDeAlicuota.Exento && valEsAlicuota) {
                vValorFinal = "FF";
            } else {
                valNumero = LibConvert.NumToString(LibImportData.ToDec(valNumero), valCantidadDecimales);
                vValorFinal = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(valNumero, valCantidadEnteros, valCantidadDecimales, ",");
                if (valEsAlicuota) {
                    vValorFinal = LibText.FillWithCharToLeft(vValorFinal, "0", (byte)(valCantidadEnteros + valCantidadDecimales + 1));
                }
            }
            return vValorFinal;
        }        

        private bool RetornoStatus(int valStatus,out string valMensajeRetorno) {
            bool vResult = false;
            valMensajeRetorno = "";
            switch(valStatus) {
            case 0:
            vResult = false;
            valMensajeRetorno = " Error de Comunicación";
            break;
            case 1:
            vResult = true;
            valMensajeRetorno = " Estatus Fiscal OK";
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

        private bool RetornoStatus1(int valStatus,out string valMensajeRetorno) {
            bool vResult = true;
            valMensajeRetorno = "";

            if(valStatus >= 128) { // bit 7 
                valStatus = valStatus - 128;
                valMensajeRetorno = valMensajeRetorno + " Fin del Papel";
                vResult = false;
            }
            if(valStatus >= 64) { // bit 6 
                valStatus = valStatus - 64;
                valMensajeRetorno = valMensajeRetorno + " Poco Papel";
                vResult = false;
            }
            if(valStatus >= 32) { // bit 5 
                valStatus = valStatus - 32;
                valMensajeRetorno = valMensajeRetorno + " Error en el Relój";
                vResult = false;
            }
            if(valStatus >= 16) { // bit 4 
                valStatus = valStatus - 16;
                valMensajeRetorno = valMensajeRetorno + " Impresora con Error";
                vResult = false;
            }
            if(valStatus >= 8) { // bit 3 
                valStatus = valStatus - 8;
                valMensajeRetorno = valMensajeRetorno + " Comando no empieza con ESC";
                vResult = false;
            }
            if(valStatus >= 4) { // bit 2 
                valStatus = valStatus - 4;
                valMensajeRetorno = valMensajeRetorno + " Comando Inexistente";
                vResult = false;
            }
            if(valStatus >= 2) { // bit 1 
                valStatus = valStatus - 2;
                valMensajeRetorno = valMensajeRetorno + " Cupón Abierto";
                vResult = false;
            }
            if(valStatus >= 1) { // bit 0 
                valStatus = valStatus - 1;
                valMensajeRetorno = valMensajeRetorno + " Número de Parámetro(s) Inválido(s)";
                vResult = false;
            }
            if(valStatus == 0) { // bit 0 
                valStatus = valStatus - 1;
                valMensajeRetorno = valMensajeRetorno + "";
                //vResult = true;
            }
            return vResult;
        }

        private bool RetornoStatus2(int valStatus,out string valMensajeRetorno) {
            bool vResult = false;
            valMensajeRetorno = "";

            if(valStatus >= 128) { // bit 7 
                valStatus = valStatus - 128;
                valMensajeRetorno = valMensajeRetorno + " Comando Invalido";
                vResult = false;
            }
            if(valStatus >= 64) { // bit 6 
                valStatus = valStatus - 64;
                valMensajeRetorno = valMensajeRetorno + " Memoria Fiscal Llena";
                vResult = false;
            }
            if(valStatus >= 32) { // bit 5 
                valStatus = valStatus - 32;
                valMensajeRetorno = " Error en memoria RAM";
                vResult = false;
            }
            if(valStatus >= 16) { // bit 4 
                valStatus = valStatus - 16;
                valMensajeRetorno = valMensajeRetorno + " Alicuota no programada";
                vResult = false;
            }
            if(valStatus >= 8) { // bit 3 
                valStatus = valStatus - 8;
                valMensajeRetorno = valMensajeRetorno + " Capacidad de Alicuota Llena";
                vResult = false;
            }
            if(valStatus >= 4) { // bit 2 
                valStatus = valStatus - 4;
                valMensajeRetorno = valMensajeRetorno + " No se permite Cancelar";
                vResult = false;
            }
            if(valStatus >= 2) { // bit 1 
                valStatus = valStatus - 2;
                valMensajeRetorno = valMensajeRetorno + " RIF del propietario No está programado en la impresora";
                vResult = false;
            }
            if(valStatus >= 1) { // bit 0 
                valStatus = valStatus - 1;
                valMensajeRetorno = valMensajeRetorno + " Comando no Ejecutado";
                vResult = false;
            }
            if(valStatus == 0) { // bit 0 
                valStatus = valStatus - 1;
                valMensajeRetorno = valMensajeRetorno + "";
                vResult = true;
            }
            return vResult;
        }

        private bool RetornoACK(int valACK,ref string refMensajeRetorno) {
            bool vResult = false;
            refMensajeRetorno = "";

            switch(valACK) {
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

        private bool RevisarEstadoImpresora(ref string refMensajeRetorno) {
            bool vResult = false;
            int Status1 = 0;
            int Status2 = 0;
            int ACK = 0;
            int vRetorno = 0;
            string vMensajeEstado = "";

            try {
                refMensajeRetorno = "";
                vRetorno = Bematech_FI_RetornoImpresora(ref ACK,ref Status1,ref Status2);
                vResult = RetornoStatus(vRetorno,out vMensajeEstado);
                refMensajeRetorno = refMensajeRetorno + LibText.CRLF() + vMensajeEstado;
                vMensajeEstado = "";
                vRetorno = 0;
                vResult = vResult && RetornoStatus1(Status1,out vMensajeEstado);
                refMensajeRetorno = refMensajeRetorno + LibText.CRLF() + vMensajeEstado;
                vMensajeEstado = "";
                vRetorno = 0;
                vResult = vResult && RetornoStatus2(Status2,out vMensajeEstado);
                refMensajeRetorno = refMensajeRetorno + LibText.CRLF() + vMensajeEstado;
                vMensajeEstado = "";
                vRetorno = 0;
                vResult = vResult && RetornoACK(ACK,ref vMensajeEstado);
                refMensajeRetorno = refMensajeRetorno + LibText.CRLF() + vMensajeEstado;
                vMensajeEstado = "";
                vRetorno = 0;
                return vResult;
            } catch(Exception vEx) {
                throw new GalacException(vEx.Message,eExceptionManagementType.Controlled);
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

        public bool ReimprimirFactura(string valDesde,string valHasta) {
            short vModo = 0;
            short vTipo = 0;
            return true;
        }

        public bool ReimprimirNotaDeCredito(string valDesde,string valHasta) {
            short vModo = 0;
            short vTipo = 1;
            return true;
        }

        public bool ReimprimirReporteZ(string valDesde,string valHasta) {
            short vModo = 0;
            short vTipo = 2;
            return true;
        }

        public bool ReimprimirReporteX(string valDesde,string valHasta) {
            short vModo = 0;
            short vTipo = 4;
            return true;
        }

        public bool ReimprimirDocumentoNoFiscal(string valDesde,string valHasta) {
            short vModo = 0;
            short vTipo = 3;
            return true;
        }

        bool IImpresoraFiscalPdn.ReimprimirDocumentoFiscal(string valDesde,string valHasta,string valTipo) {
            bool vResult = false;
            eTipoDocumentoFiscal TipoDeDocumento = (eTipoDocumentoFiscal)LibConvert.DbValueToEnum(valTipo);

            switch(TipoDeDocumento) {
            case eTipoDocumentoFiscal.FacturaFiscal:
            vResult = ReimprimirFactura(valDesde,valHasta);
            break;
            case eTipoDocumentoFiscal.NotadeCredito:
            vResult = ReimprimirNotaDeCredito(valDesde,valHasta);
            break;
            case eTipoDocumentoFiscal.ReporteX:
            vResult = ReimprimirReporteX(valDesde,valHasta);
            break;
            case eTipoDocumentoFiscal.ReporteZ:
            vResult = ReimprimirReporteZ(valDesde,valHasta);
            break;
            }
            return vResult;
        }

        bool IImpresoraFiscalPdn.ReimprimirDocumentoNoFiscal(string valDesde,string valHasta) {
            throw new NotImplementedException();
        }


        bool ILibPdn.CanBeChoosen(string valCallingModule,eAccionSR valAction,string valExtendedAction,XmlDocument valXmlRow) {
            return false;
        }

        bool ILibPdn.GetDataForList(string valCallingModule,ref XmlDocument refXmlDocument,StringBuilder valXmlParamsExpression) {
            return false;
        }

        XElement ILibPdn.GetFk(string valCallingModule,StringBuilder valParameters) {
            return null;
        }

        public bool EstatusDeComunicacion(IFDiagnostico vDiagnostico) {
            int vReq = 0;
            bool vResult = false;
            vReq = Bematech_FI_VerificaImpresoraPrendida();
            vResult = vReq == 1;            
            vDiagnostico.EstatusDeComunicacionDescription = LibImpresoraFiscalUtil.EstatusDeComunicacionDescription(vResult);
            return vResult;
        }

        public bool VersionDeControladores(IFDiagnostico vDiagnostico) {
            bool vResult = false;
            bool vIsSameVersion = false;
            string vVersion = "";
            string vDir = "";
            if(Environment.Is64BitOperatingSystem) {
                vDir = Environment.GetFolderPath(Environment.SpecialFolder.SystemX86) + @"\BemaFi32.dll";
            } else {
                vDir = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\BemaFi32.dll";
            }
            string vDllVersionModel = MinVersionApi;  //(vModelo == eImpresoraFiscal.BEMATECH_MP_4000_FI ? VersionApi2 : VersionApi1);
            vResult = LibFile.FileExists(vDir);
            if(vResult) {
                vResult &= LibImpresoraFiscalUtil.ObtenerVersionDeControlador(vDir,ref vVersion);
                vIsSameVersion = (vVersion == MinVersionApi || vVersion == MaxVersionApi);
                vDiagnostico.VersionDeControladoresDescription = LibImpresoraFiscalUtil.EstatusVersionDeControladorDescription(vResult,vIsSameVersion,vDir,vVersion,vDllVersionModel);
                vResult = vIsSameVersion;
            } else {
                vDiagnostico.VersionDeControladoresDescription = LibImpresoraFiscalUtil.EstatusVersionDeControladorDescription(vResult,vIsSameVersion,vDir,vVersion,vDllVersionModel);
            }
            return vResult;
        }

        public bool AlicuotasRegistradas(IFDiagnostico vDiagnostico) {
            bool vResult = false;
            decimal AlicuotaGeneral;
            decimal Alicuota2;
            decimal Alicuota3;
            int vReq = 0;
            string RecAlicuotas = "";
            RecAlicuotas = LibText.FillWithCharToRight(RecAlicuotas," ", 80);
            vReq = Bematech_FI_RetornoAlicuotas(ref RecAlicuotas);
            RecAlicuotas = LibText.CleanSpacesToBothSides(LibText.Replace(RecAlicuotas,"\0",""));            
            string[] ListAlicuotas = LibString.Split(RecAlicuotas,',');            
            AlicuotaGeneral = LibImportData.ToDec(LibString.InsertAt( ListAlicuotas[0],".",2),2);
            Alicuota2 = LibImportData.ToDec(LibString.InsertAt(ListAlicuotas[1],".",2),2);
            Alicuota3 = LibImportData.ToDec(LibString.InsertAt(ListAlicuotas[2],".",2),2);
            RecAlicuotas = "";
            vResult = LibImpresoraFiscalUtil.ValidarAlicuotasRegistradas(AlicuotaGeneral,Alicuota2,Alicuota3,ref RecAlicuotas);
            vDiagnostico.AlicoutasRegistradasDescription = RecAlicuotas;
            return vResult;
        }

        public bool FechaYHora(IFDiagnostico vDiagnostico) {
            bool vResult = false;
            DateTime dFecha;
            string vFecha = "";
            vFecha = ObtenerFechaYHora();
            dFecha = LibConvert.ToDate(vFecha);
            vResult = !LibDate.F1IsLessThanF2(dFecha,LibDate.Today());
            vDiagnostico.FechaYHoraDescription = LibImpresoraFiscalUtil.EstatusHorayFechaDescription(vResult);
            return vResult;
        }

        public bool ColaDeImpresion(IFDiagnostico vDiagnostico) {
            bool vResult = false;
            int vRetorno = 0;
            int ACK = 0, S1 = 0, S2 = 0;
            string vColaImpresion = "";

            vRetorno = Bematech_FI_RetornoImpresora(ref ACK,ref S1,ref S2);
            vResult = vRetorno == 1;
            vResult = vResult && RetornoStatus1(S1,out vColaImpresion);
            vDiagnostico.ColaDeImpresioDescription = (LibString.IsNullOrEmpty(vColaImpresion) ? "Listo En Espera" : vColaImpresion);
            return vResult;
        }

        public IFDiagnostico RealizarDiagnotsico(bool valAbrirPuerto = false) {
            IFDiagnostico vDiagnostico = new IFDiagnostico();           
            try {
                if(valAbrirPuerto) {
                    AbrirConexion();
                }
                vDiagnostico.EstatusDeComunicacion = EstatusDeComunicacion(vDiagnostico);                
                vDiagnostico.VersionDeControladores = VersionDeControladores(vDiagnostico);                
                if(!vDiagnostico.EstatusDeComunicacion) {
                    vDiagnostico.AlicoutasRegistradasDescription = "No se completó";
                    vDiagnostico.FechaYHoraDescription = "No se completó";
                    vDiagnostico.ColaDeImpresioDescription = "No se completó";
                    return vDiagnostico;
                }
                vDiagnostico.AlicuotasRegistradas = AlicuotasRegistradas(vDiagnostico);                
                vDiagnostico.FechaYHora = FechaYHora(vDiagnostico);                
                vDiagnostico.ColaDeImpresion = ColaDeImpresion(vDiagnostico);                
                if(valAbrirPuerto) {
                    CerrarConexion();
                }
                return vDiagnostico;
            } catch(Exception) {
                throw;
            }
        }
    }
}

