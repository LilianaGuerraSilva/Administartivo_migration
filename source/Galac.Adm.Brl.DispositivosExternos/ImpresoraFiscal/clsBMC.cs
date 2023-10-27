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
    public class clsBMC:IImpresoraFiscalPdn {
        #region constantes
        const string VersionApi = "2, 3, 1, 5";
        const string DllApiName = @"FPCTRL.dll";
        const string _S1Test = "01\n00000000001000000\n00000015\n00008\n00000000\n00000\n00000005\n00000\n0001\n0001\nJ-395211990\nNA00500441\n070410\n200720\n";
        #endregion

        #region BMC Commands
        [DllImport("FPCTRL.dll")]
        private static extern int OpenFpctrl(string lpPortName);
        [DllImport("FPCTRL.dll")]
        private static extern int CloseFpctrl();
        [DllImport("FPCTRL.dll")]
        private static extern bool CheckFprinter();
        [DllImport("FPCTRL.dll")]
        private static extern int ReadFpStatus(ref int vStatus,ref int mError);
        [DllImport("FPCTRL.dll")]
        private static extern int SendCmd  (ref int mStatus,ref int mError, string mCmd);
        [DllImport("FPCTRL.dll")]
        private static extern int SendNCmd  (ref int mStatus,ref int mError,string mBuffer);
        [DllImport("FPCTRL.dll")]
        private static extern int SendFileCmd  (ref int mStatus,ref int mError,string mFile);
        [DllImport("FPCTRL.dll")]
        private static extern int UploadReportCmd  (ref int mStatus,ref int mError, string mCmd,string mFile);
        [DllImport("FPCTRL.dll")]
        private static extern int UploadStatusCmd  (ref int mStatus,ref int mError, string mCmd,[MarshalAs(UnmanagedType.VBByRefStr)]ref string mRespuesta);
        [DllImport("kernel32.dll")]
        private static extern int Sleep(int mSeg);
        #endregion BMC Commands
        
        #region Formatos_Numericos
        const int _EnterosParaCantidad=5;
        const int _DecimalesParaCantidad=3;
        const int _EnterosParaMonto=8;
        const int _DecimalesParaMonto=2;
        const int _EnterosParaPagos=10;
        const int _DecimalesParaPagos=2;
        const int _EnterosParaDescuento=2;
        const int _DecimalesParaDescuento=2;
        #endregion

        #region DatosDeLectura
        const int _SerialFiscal = 11;
        const int _UltimoNumeroFactura = 2;
        const int _UltimoNumeroNC = 6;
        const int _UltimoNumeroReporteZ = 8;
        const int _FechaMaqFiscal = 13;
        #endregion

        #region variables
        int _LineaTextoAdicional;
        private string _CommPort = "";        
        private eImpresoraFiscal _ModeloBMC;        
        int _MaxLongitudDeTexto = 0;
        bool _PortIsOpen = false;        
        #endregion

        public clsBMC(XElement valXmlDatosImpresora) {           
            _LineaTextoAdicional = 0;
            _MaxLongitudDeTexto = 38;
            ePuerto ePuerto = (ePuerto)LibConvert.DbValueToEnum(LibXml.GetPropertyString(valXmlDatosImpresora,"PuertoMaquinaFiscal"));
            _CommPort = ePuerto.GetDescription(0);
            _ModeloBMC = (eImpresoraFiscal)LibConvert.DbValueToEnum(LibImpresoraFiscalUtil.ObtenerValorDesdeXML(valXmlDatosImpresora,"ModeloDeMaquinaFiscal"));

        }

        #region Status and Error
        Dictionary<int,string> BMCStatus = new Dictionary<int,string> {
            { 0,"OK" },
            { 1,"Modo Entrenador / Disponible" },
            { 2,"Modo Entrenador / Doc Fiscal Abierto" },
            { 3,"Modo Entrenador / Doc No Fiscal Abierto" },
            { 4,"Modo Fiscal / Disponible" },
            { 5,"Modo Fiscal / Doc Fiscal Abierto" },
            { 6,"Modo Fiscal / Doc No Fiscal Abierto" },
            { 7,"Modo Fiscal / Memoria fiscal casi llena / Disponible" },
            { 8,"Modo Fiscal / Memoria fiscal casi llena / En Doc Fiscal" },
            { 9,"Modo Fiscal / Memoria fiscal casi llena / En Doc No Fiscal" },
            { 10,"Modo Fiscal / Memoria fiscal llena / Disponible" },
            { 11,"Modo Fiscal / Memoria fiscal llena / En Doc Fiscal" },
            { 12,"Modo Fiscal / Memoria fiscal llena / En Doc No Fiscal" },
        };

        Dictionary<int,string> BMCError = new Dictionary<int,string> {            
            { 0,"OK" },
            { 1,"Fin del papel" },
            { 2,"Error mecánico impr de recibo" },
            { 3,"Error mecánico impr auditoria" },
            { 80,"Comando o Valor Invalido" },
            { 84,"Alicuota Invalida" },
            { 88,"Cajero No Asignado" },
            { 92,"Comando Inválido" },
            { 93,"Fin del Papel" },
            { 96,"Error Fiscal" },
            { 100,"Error de Memoria Fiscal" },
            { 108,"Memoria Fiscal Llena" },
            { 112,"Buffer Lleno" },
            { 128,"Error de Comunicación" },
            { 137,"Sin Respuesta" },
            { 144,"LRC Error" },
            { 145,"Error de API" },
            { 153,"Error en la apertura del archivo" }
        };

        private bool BMCCheckRequest(int valStaus,int valError,ref string refMensaje) {
            bool vResult = false;
            vResult = (valStaus == 0 ? true : valStaus == 1 ? true : valStaus == 4 ? true : valStaus == 7 ? true : valStaus == 10 ? true : false);
            vResult &= (valError == 0);
            refMensaje = (valStaus == 0 ? "" : (BMCStatus[valStaus]) + "\r\n");
            refMensaje += (valError == 0 ? "" : BMCError[valError]);
            return vResult;
        }

        #endregion Status and Error

        #region BMC Command´s

        private string ObtenerValorDelstatus(int valorItem,string vStatusLista) {
            string vResult = "";
            string[] arLista = new string[] { };
            if(LibString.IndexOf(vStatusLista,'\n') > 0) {
                arLista = LibString.Split(vStatusLista,'\n');
                vResult = arLista[valorItem];
            }
            return vResult;
        }

        private bool BMCSendCmd(string valCmd) {
            bool vResult = false;
            string vMensaje = "";
            int vStatus = 0, vError = 0;
            try {
                vResult = (SendCmd(ref vStatus,ref vError,valCmd)==1);                
                vResult &= BMCCheckRequest(vStatus,vError,ref vMensaje);
                if(!vResult) {
                    throw new Exception(vMensaje);
                }
                return vResult;
            } catch(Exception vEx) {
                throw vEx;
            }                        
        }

        private bool BMCGetStatusCmd(string valCmd,ref string valRespuesta) {
            bool vResult = false;
            int vStatus = 0, vError = 0;
            string vMensaje = "";
            try {
                valRespuesta = new string('\u0020',250);
                vResult = (UploadStatusCmd(ref vStatus,ref vError,valCmd,ref valRespuesta)==1);
                //valRespuesta = (valCmd!="S1"?valRespuesta: _S1Test);
                vResult &= BMCCheckRequest(vStatus,vError,ref vMensaje);
                if(!vResult) {
                    throw new Exception(vMensaje);
                }
                return vResult;
            } catch(Exception vEx) {
                throw vEx;
            }
        }

        public bool ComprobarEstado() {
            bool vResult = false;
            int vStatus = 0, vError = 0;
            string vMensaje = "";
            vResult = (ReadFpStatus(ref vStatus,ref vError)==1);            
            vResult &= BMCCheckRequest(vStatus,vError,ref vMensaje);
            if(!vResult) {
                throw new Exception(vMensaje);
            }
            return vResult;
        }
        #endregion BMC Command´s

        public bool AbrirConexion() {           
            int vSt = 0;
            bool vResult = false;
            try {
                if(!_PortIsOpen) {
                    vResult = (OpenFpctrl(_CommPort)==1);                    
                    if(vResult) {
                        Sleep(200);
                        vResult &= CheckFprinter();
                        if(!vResult) {
                            throw new Exception("Error de comunicación.\r\nVerifique conexiónes de la impresora.");
                        }
                        vResult =  (SendCmd(ref vSt,ref vSt,"e")==1);
                    } else {
                        throw new Exception("Error de comunicación.\r\nVerifique conexiónes de la impresora.");
                    }
                }
                _PortIsOpen = true;
                return vResult;
            } catch(Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Abrir Conexión\r\n" + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        public bool CerrarConexion() {            
            try {
                if(_PortIsOpen) {
                    CloseFpctrl();
                    Sleep(250);
                    _PortIsOpen = false;
                    return true;                    
                } else {
                    return true;
                }
            } catch(Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Cerrar Conexión\r\n" + vEx.Message,eExceptionManagementType.Controlled);
            }
        }        

        public eStatusImpresorasFiscales EstadoDelPapel(bool valAbrirPuerto) {
            eStatusImpresorasFiscales vResult;
            try {
                if(valAbrirPuerto) {
                    AbrirConexion();
                }            
                vResult = eStatusImpresorasFiscales.eListoEnEspera;
                if(valAbrirPuerto) {
                    CerrarConexion();
                }
            } catch(GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Estado del Papel\r\n" + vEx.Message,eExceptionManagementType.Controlled);
            }
            return vResult;
        }

        public bool RealizarReporteZ() {
            bool vResult = false;
            try {
                AbrirConexion();
                vResult = BMCSendCmd("I0Z");
                Thread.Sleep(350);                
                CerrarConexion();
                return vResult;
            } catch(GalacException vEx) {
                CerrarConexion();
                throw new LibGalac.Aos.Catching.GalacException("Realizar Reporte Z\r\n" + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        public bool RealizarReporteX() {
            bool vResult = false;
            try {
                AbrirConexion();
                vResult = BMCSendCmd("I0X");
                Thread.Sleep(350);                
                CerrarConexion();
                return vResult;
            } catch(GalacException vEx) {
                CerrarConexion();
                throw new LibGalac.Aos.Catching.GalacException("Realizar Reporte X\r\n" + vEx.Message,eExceptionManagementType.Controlled);
            }
        }        

        public string ObtenerSerial(bool valAbrirConexion) {
            bool vReq = false;
            string vSerial = "";
            try {
                if(valAbrirConexion ) {
                    AbrirConexion();
                }
                vReq = BMCGetStatusCmd("S1",ref vSerial);
                vSerial = ObtenerValorDelstatus(_SerialFiscal,vSerial);
                if(valAbrirConexion) {
                    CerrarConexion();
                }
                return vSerial;
            } catch(Exception vEx) {
                throw new GalacException("No se pudo obtener serial, verifique puertos y conexiones\r\n" + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerFechaYHora() {
            string vFecha = "";
            bool vReq = false;
            try {               
                vReq = BMCGetStatusCmd("S1",ref vFecha);
                vFecha = ObtenerValorDelstatus(_FechaMaqFiscal,vFecha);                                
                vFecha = LibString.InsertAt(vFecha,"/",2);
                vFecha = LibString.InsertAt(vFecha,"/",5);                
                return vFecha;
            } catch(Exception vEx) {
                throw new GalacException("No se pudo obtener fecha , verifique puertos y conexiones\r\n" + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerUltimoNumeroFactura(bool valAbrirConexion) {
            string vUltimaFactura = "";
            bool vReq = false;
            try {
                if(valAbrirConexion ) {
                    AbrirConexion();
                }
                vReq =BMCGetStatusCmd("S1",ref vUltimaFactura);
                vUltimaFactura = ObtenerValorDelstatus(_UltimoNumeroFactura,vUltimaFactura);
                if(valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimaFactura;
            } catch(Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Obtener último Numero de factura\r\n" + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        public string ObtenerUltimoNumeroNotaDeCredito(bool valAbrirConexion) {
            string vUltimaNC = "";
            bool vReq = false;
            try {
                if(valAbrirConexion ) {
                    AbrirConexion();
                }
                vReq = BMCGetStatusCmd("S1",ref vUltimaNC);
                vUltimaNC = ObtenerValorDelstatus(_UltimoNumeroNC,vUltimaNC);                
                if(valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimaNC;
            } catch(Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Obtener último Numero de Nota de credito\r\n" + vEx.Message,eExceptionManagementType.Controlled);
            }
        }        

        public string ObtenerUltimoNumeroReporteZ(bool valAbrirConexion) {
            string vUltimoNumeroZ = "";
            bool vEstado;            

            try {
                if(valAbrirConexion ) {
                    AbrirConexion();
                }
                vEstado = BMCGetStatusCmd("S1",ref vUltimoNumeroZ);
                vUltimoNumeroZ = ObtenerValorDelstatus(_UltimoNumeroReporteZ,vUltimoNumeroZ);
                if(valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimoNumeroZ;
            } catch(Exception) {
                return "0000";
            }
        }

        public bool CancelarDocumentoFiscalEnImpresion(bool valAbrirConexion) {
            bool vResult = false;
            string vCMD = "7";
            try {
                if(valAbrirConexion ) {
                    AbrirConexion();
                }
                vResult = BMCSendCmd(vCMD);
                if(valAbrirConexion) {
                    CerrarConexion();
                }
                return vResult;
            } catch(Exception vEx) {
                throw new GalacException("Cancelar Documento Fiscal en Cola\r\n" + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        private void ImprimeSerialyRollo(string valSerial,string valRollo) {
            string vCmd;
            try {
                if(!valSerial.Equals("")) {
                    vCmd = LibText.Left("@" + valSerial,40);
                    BMCSendCmd(vCmd);
                }

                if(!valRollo.Equals("")) {
                    vCmd = LibText.Left("@" + valRollo,40);
                    BMCSendCmd(vCmd);
                }
                Thread.Sleep(100);
            } catch(Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Imprimir Serial y Rollo\r\n" + vEx.Message,LibGalac.Aos.Catching.eExceptionManagementType.Controlled);
            }
        }

        private string DarFormatoAAlicuotaIva(eTipoDeAlicuota valTipoAlicuota) {
            string vValorFinal = "";
            switch(valTipoAlicuota) {
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
            return vValorFinal;
        }

        private string DarFormatoAAlicuotaIvaNC(eTipoDeAlicuota valTipoAlicuota) {
            string vValorFinal = "";
            switch(valTipoAlicuota) {
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

        private string DarFormatoNumericoParaComandosGenerales(string valNumero,int valCantidadEnteros) {
            string vResult = "";
            vResult = LibString.InsertAt(valNumero,",",valCantidadEnteros);
            return vResult;
        }

        public bool ImprimirNotaCredito(XElement valDocumentoFiscal) {
            string vDireccion = LibXml.GetPropertyString(valDocumentoFiscal,"DireccionCliente");
            string vRif = LibXml.GetPropertyString(valDocumentoFiscal,"NumeroRIF");
            string vRazonSocial = LibXml.GetPropertyString(valDocumentoFiscal,"NombreCliente");
            string vTelefono = LibXml.GetPropertyString(valDocumentoFiscal,"TelefonoCliente");
            string vObservaciones = LibXml.GetPropertyString(valDocumentoFiscal,"Observaciones");
            string vNumeroComprobante = LibXml.GetPropertyString(valDocumentoFiscal,"NumeroComprobanteFiscal");
            string vSerialMaquina = LibXml.GetPropertyString(valDocumentoFiscal,"SerialMaquinaFiscal");
            string vFecha = LibXml.GetPropertyString(valDocumentoFiscal,"Fecha");
            string vHora = LibXml.GetPropertyString(valDocumentoFiscal,"HoraModificacion");

            bool vResult = false;

            try {
                List<XElement> vCamposDefinibles = valDocumentoFiscal.Descendants("GpResultDetailCamposDefinibles").ToList();
                AbrirConexion();
                if(AbrirNotaDeCredito(vRazonSocial,vRif,vDireccion,vTelefono,vObservaciones,vNumeroComprobante,vSerialMaquina,vFecha,vHora)) {
                    vResult = ImprimirTodosLosArticulos(valDocumentoFiscal,true);
                    vResult &= CerrarComprobanteFiscal(valDocumentoFiscal,true);
                }
                CerrarConexion();
                Thread.Sleep(250);
            } catch(Exception vEx) {
                vResult = false;
                throw vEx;
            }
            return vResult;
        }

        public bool ImprimirFacturaFiscal(XElement valDocumentoFiscal) {

            string vDireccion = LibXml.GetPropertyString(valDocumentoFiscal,"DireccionCliente");
            string vRif = LibXml.GetPropertyString(valDocumentoFiscal,"NumeroRIF");
            string vRazonSocial = LibXml.GetPropertyString(valDocumentoFiscal,"NombreCliente");
            string vTelefono = LibXml.GetPropertyString(valDocumentoFiscal,"TelefonoCliente");
            string vObservaciones = LibXml.GetPropertyString(valDocumentoFiscal,"Observaciones");

            bool vResult = false;
            try {
                List<XElement> vCamposDefinibles = valDocumentoFiscal.Descendants("GpResultDetailCamposDefinibles").ToList();
                AbrirConexion();
                if(AbrirComprobanteFiscal(vRazonSocial,vRif,vDireccion,vTelefono,vObservaciones)) {
                    vResult = ImprimirTodosLosArticulos(valDocumentoFiscal,false);
                    vResult &= CerrarComprobanteFiscal(valDocumentoFiscal,false);
                }
                CerrarConexion();
                Thread.Sleep(250);
            } catch(Exception vEx) {
                vResult = false;
                throw vEx;
            }
            return vResult;
        }

        private bool ImprimirTodosLosArticulos(XElement valDocumentoFiscal,bool valIsNotaDeCredito) {
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
                foreach(XElement vXElement in vRecord) {
                    vDescripcion = LibXml.GetElementValueOrEmpty(vXElement,"Descripcion");
                    vCantidad = LibXml.GetElementValueOrEmpty(vXElement,"Cantidad");
                    vCantidad = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vCantidad,_EnterosParaCantidad,_DecimalesParaCantidad);
                    vMonto = LibXml.GetElementValueOrEmpty(vXElement,"PrecioSinIVA");
                    vMonto = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vMonto,_EnterosParaMonto,_DecimalesParaMonto);
                    vTipoTasa = LibXml.GetElementValueOrEmpty(vXElement,"AlicuotaIva");
                    vPrcDescuento = LibXml.GetElementValueOrEmpty(vXElement,"PorcentajeDescuento");
                    vPrcDescuento = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vPrcDescuento,_EnterosParaDescuento,_DecimalesParaDescuento);
                    vSerial = LibXml.GetElementValueOrEmpty(vXElement,"Serial");
                    vRollo = LibXml.GetElementValueOrEmpty(vXElement,"Rollo");
                    if(valIsNotaDeCredito) {
                        vTipoTasa = DarFormatoAAlicuotaIvaNC((eTipoDeAlicuota)LibConvert.DbValueToEnum(vTipoTasa));
                    } else {
                        vTipoTasa = DarFormatoAAlicuotaIva((eTipoDeAlicuota)LibConvert.DbValueToEnum(vTipoTasa));
                    }
                    ImprimeSerialyRollo(vSerial,vRollo);
                    if(!ImprimirArticuloVenta(vDescripcion,vCantidad,vMonto,vTipoTasa,vPrcDescuento,valIsNotaDeCredito)) {
                        vEstatus &= false;
                        throw new Exception("Documento no impreso");
                    }
                    vEstatus &= true;
                }
                return vEstatus;
            } catch(Exception vEx) {
                CancelarDocumentoFiscalEnImpresion(false);
                CerrarConexion();
                throw new GalacException(vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        private bool CerrarComprobanteFiscal(XElement valDocumentoFiscal,bool valIsNotaDeCredito) {
            bool vEstatus = true;
            string vCmd = "";
            string vTotalesEnDivisa;
            string vDescuentoTotal = LibXml.GetPropertyString(valDocumentoFiscal,"PorcentajeDescuento");

            vDescuentoTotal = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vDescuentoTotal,_EnterosParaDescuento,_DecimalesParaDescuento);
            vTotalesEnDivisa = LibXml.GetPropertyString(valDocumentoFiscal,"TotalMonedaExtranjera");
            if(LibText.Len(vTotalesEnDivisa) > 0) {
                vEstatus = ImprimirTotalesEnDivisas(vTotalesEnDivisa);
            } else if(!valIsNotaDeCredito && LibString.Len(vTotalesEnDivisa) == 0) {
                vEstatus = ImprmirCamposDefinibles(valDocumentoFiscal);
            }
            if(LibConvert.ToInt(vDescuentoTotal) != 0) {
                vCmd = "3"; //SubTotal
                vEstatus &= BMCSendCmd(vCmd);
                vCmd = "p-" + vDescuentoTotal;
                vEstatus &= BMCSendCmd(vCmd);
            }
            vEstatus &= EnviarPagos(valDocumentoFiscal);
            return vEstatus;
        }

        private bool ImprimirTotalesEnDivisas(string vTotales) {
            bool vResult = true;
            string vCmd = "";
            string[] vList = LibString.Split(vTotales,'\n');
            foreach(string vText in vList) {
                vCmd = GetLineaTexto() + vText;
                vResult = BMCSendCmd(vCmd);
                if(_LineaTextoAdicional >= 13) {
                    break;
                }
            }
            return vResult;
        }

        private bool ImprimirArticuloVenta(string valDescripcion,string valCantidad,string valPrecio,string valTipoTasa,string valPorcetajeDesRenglon,bool valEsDevolucion) {
            bool vResult = true;
            string vCmd = "";            

            if(valEsDevolucion) {
                vCmd = "d" + valTipoTasa;
            } else {
                vCmd = valTipoTasa;
            }
            vCmd = vCmd + valPrecio;
            vCmd = vCmd + valCantidad;
            vCmd = vCmd + LibText.Left(valDescripcion,105);
            vResult = BMCSendCmd(vCmd);
            Thread.Sleep(600);
            if(LibConvert.ToInt(valPorcetajeDesRenglon) > 0) {
                vCmd = "p-" + valPorcetajeDesRenglon;
                vResult &= BMCSendCmd(vCmd);
            }
            return vResult;
        }

        private bool ImprmirCamposDefinibles(XElement valData) {
            bool vResult = true;
            string vCmd;
            List<XElement> vCamposDefinibles = valData.Descendants("GpResultDetailCamposDefinibles").ToList();
            if(vCamposDefinibles.Count > 0) {
                foreach(XElement vRecord in vCamposDefinibles) {
                    vCmd = GetLineaTexto() + LibXml.GetElementValueOrEmpty(vRecord,"CampoDefinibleValue");
                    vResult |= BMCSendCmd(vCmd);
                    if(_LineaTextoAdicional >= 13) {
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
            bool vResult = false;

            try {
                List<XElement> vNodos = valMedioDePago.Descendants("GpResultDetailRenglonCobro").ToList();
                if(vNodos.Count > 0) {
                    foreach(XElement vXElement in vNodos) {
                        vMedioDePago = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement,"CodigoFormaDelCobro"));
                        vFormatoDeCobro = FormaDeCobro(vMedioDePago);
                        vMonto = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement,"Monto"));
                        if(LibImportData.ToDec(vMonto) > 0) {
                            vMonto = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vMonto,_EnterosParaPagos,_DecimalesParaPagos);
                            vCmd = "2" + vFormatoDeCobro + vMonto;
                        } else {
                            vCmd = "1" + FormaDeCobro("00003");//Debito     
                        }
                        vResult = BMCSendCmd(vCmd);
                    }
                } else {
                    vCmd = "1" + FormaDeCobro("00003"); //Debito                     
                    vResult = BMCSendCmd(vCmd);
                }
                if(!vResult) {
                    CancelarDocumentoFiscalEnImpresion(false);
                    throw new GalacException(" No se pudo Emitir la factura ",eExceptionManagementType.Controlled);
                }
                return vResult;
            } catch(Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Imprimir Medio de Pago\r\n" + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        private string GetLineaTexto() {
            string vResult = "";
            vResult = "i" + String.Format("{0:D2}",_LineaTextoAdicional);
            _LineaTextoAdicional++;
            return vResult;
        }

        private bool EnviarDatosAdicionales(string valDireccion,string valTelefono,string valObservaciones) {
            bool vResult = false;
            string vCMD = "";

            if(LibText.Len(valTelefono) > 0) {
                vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString("Telf: " + valTelefono,0,_MaxLongitudDeTexto));
                vResult = BMCSendCmd(vCMD);
            }
            if(!valDireccion.Equals("") && valObservaciones.Equals("") ) {
                vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString("Direccion:" + valDireccion,0,40));
                vResult = BMCSendCmd(vCMD);
                vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString(valDireccion,30,40));
                vResult &= BMCSendCmd(vCMD);
            } else if(!valObservaciones.Equals("") && valDireccion.Equals("") ) {
                vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString("Obs.:" + valObservaciones,0,40));
                vResult = BMCSendCmd(vCMD);
                vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString(valObservaciones,35,40));
                vResult &= BMCSendCmd(vCMD);
            } else {
                vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString("Direccion:" + valDireccion,0,40));
                vResult = BMCSendCmd(vCMD);
                vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString("Obs.:" + valObservaciones,0,40));
                vResult &= BMCSendCmd(vCMD);
                vCMD = GetLineaTexto() + LibText.Trim(LibText.SubString(valObservaciones, 35, 40));
                vResult &= BMCSendCmd(vCMD);
            }
            return vResult;
        }

        private bool AbrirComprobanteFiscal(string valRazonSocial,string valRif,string valDireccion,string valTelefono,string valObservaciones) {
            try {
                bool vResult = false;
                string vCMD = "";
                _LineaTextoAdicional = 0;
                vCMD = GetLineaTexto() + "Nombre:" + LibText.Trim(LibText.SubString(valRazonSocial,0,_MaxLongitudDeTexto));
                vResult &= BMCSendCmd(vCMD);
                vCMD = GetLineaTexto() + "RIF:" + LibText.Trim(LibText.SubString(valRif,0,15));
                vResult = BMCSendCmd(vCMD);
                vResult &= EnviarDatosAdicionales(valDireccion,valTelefono,valObservaciones);
                return vResult;
            } catch(Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Abrir Comprobante Fiscal\r\n" + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        private bool AbrirNotaDeCredito(string valRazonSocial,string valRif,string valDireccion,string valTelefono,string valObservaciones,string valNumeroFacturaOriginal,string valSerialMaquinaFiscal,string valFecha,string valHora) {
            try {
                bool vResult = false;
                string vCMD = "";
                _LineaTextoAdicional = 0;
                vCMD = GetLineaTexto() + "# Factura:" + LibText.Trim(LibText.SubString(valNumeroFacturaOriginal,0,11));
                vResult = BMCSendCmd(vCMD);
                vCMD = GetLineaTexto() + "Nombre Cliente:" + LibText.Trim(LibText.SubString(valRazonSocial,0,_MaxLongitudDeTexto));
                vResult &= BMCSendCmd(vCMD);
                vCMD = GetLineaTexto() + "RIF:" + LibText.Trim(LibText.SubString(valRif,0,15));
                vResult = BMCSendCmd(vCMD);
                vCMD = GetLineaTexto() + "Serial Maquina:" + LibText.Trim(LibText.SubString(valSerialMaquinaFiscal,0,10));
                vResult = BMCSendCmd(vCMD);
                valFecha = LibString.SubString(valFecha,0,10);
                vCMD = GetLineaTexto() + "Fecha:" + LibText.Trim(LibText.SubString(valFecha,0,10));
                vResult = BMCSendCmd(vCMD);
                vResult &= EnviarDatosAdicionales(valDireccion,valTelefono,valObservaciones);
                return vResult;
            } catch(Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Abrir Comprobante Fiscal:\r\n" + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        private string FormaDeCobro(string valFormaDeCobro) {
            string vResultado = "";
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
            return vResultado;
        }

        string FormatoParaFechaDeReportes(string valFecha) {
            string vResult;
            string Dia = "";
            string Mes = "";
            string Ano = "";

            Dia = LibString.SubString(valFecha,0,2);
            Mes = LibString.SubString(valFecha,3,2);
            Ano = LibString.SubString(valFecha,8,2);
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

        bool IImpresoraFiscalPdn.ReimprimirDocumentoNoFiscal(string valDesde,string valHasta) {
            throw new NotImplementedException();
        }

        bool IImpresoraFiscalPdn.ReimprimirDocumentoFiscal(string valDesde,string valHasta,string valTipo) {
            bool vResult = false;
            string vTipoOperacion = "";
            string vCmd = "";
            eTipoDocumentoFiscal TipoDeDocumento = (eTipoDocumentoFiscal)LibConvert.DbValueToEnum(valTipo);

            try {
                switch(TipoDeDocumento) {
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

                if(LibDate.IsValidTextForDate(valDesde) && LibDate.IsValidTextForDate(valHasta)) {
                    valDesde = FormatoParaFechaDeReportes(valDesde);
                    valHasta = FormatoParaFechaDeReportes(valHasta);
                } else if(LibConvert.IsNumeric(valDesde) && LibConvert.IsNumeric(valHasta)) {
                    valDesde = LibString.SubString(valDesde,0,6);
                    valDesde = LibText.FillWithCharToLeft(valDesde,"0",7);
                    valHasta = LibString.SubString(valHasta,0,6);
                    valHasta = LibText.FillWithCharToLeft(valHasta,"0",7);
                } else {
                    throw new GalacException("Los Datos de Entrada son Incorrectos para Número o Fecha\r\n",eExceptionManagementType.Controlled);
                }
                AbrirConexion();
                vCmd = "U4" + vTipoOperacion + valDesde + valHasta;
                vResult = BMCSendCmd(vCmd);
                CerrarConexion();
                return vResult;
            } catch(Exception vEx) {
                throw;
            }
        }

        public IFDiagnostico RealizarDiagnostico(bool valAbrirPuerto = false) {
            IFDiagnostico vDiagnostico = new IFDiagnostico();
            try {
                if(valAbrirPuerto) {
                    AbrirConexion();
                }
                vDiagnostico.EstatusDeComunicacion = EstatusDeComunicacion(vDiagnostico);
                vDiagnostico.VersionDeControladores = VersionDeControladores(vDiagnostico);
                if(!vDiagnostico.EstatusDeComunicacion) {
                    vDiagnostico.AlicoutasRegistradasDescription = "No se completo";
                    vDiagnostico.FechaYHoraDescription = "No se completo";
                    vDiagnostico.ColaDeImpresioDescription = "No se completo";
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

        public bool EstatusDeComunicacion(IFDiagnostico vDiagnostico) {
            bool vResult = false;
            vResult = CheckFprinter();
            vDiagnostico.EstatusDeComunicacionDescription = LibImpresoraFiscalUtil.EstatusDeComunicacionDescription(vResult);
            return vResult;
        }

        public bool VersionDeControladores(IFDiagnostico vDiagnostico) {
            bool vResult = false;
            bool vIsSameVersion = false;
            string vVersion = "";
            string vDir = "";

            vDir = System.IO.Path.Combine(LibApp.AppPath() + "CDP",DllApiName);
            System.Windows.Forms.MessageBox.Show(vDir,"");
            vResult = LibImpresoraFiscalUtil.ObtenerVersionDeControlador(vDir,ref vVersion);
            vIsSameVersion = (vVersion == VersionApi);
            vDiagnostico.VersionDeControladoresDescription = LibImpresoraFiscalUtil.EstatusVersionDeControladorDescription(vResult,vIsSameVersion,vDir,vVersion,VersionApi);
            vResult = vIsSameVersion;
            return vResult;
        }

        private decimal AjustarYConvertirADecimalLaAlicuota(string vTextIn) {
            decimal vResult = 0;
            int vPos = LibString.IndexOf(vTextIn,"2") + 1;
            vTextIn = LibString.SubString(vTextIn,vPos);
            vTextIn = (LibString.IndexOf(vTextIn,'.') > 0 ? vTextIn : LibString.InsertAt(vTextIn,".",2));
            vResult = LibImportData.ToDec(vTextIn);
            return vResult;
        }

        public bool AlicuotasRegistradas(IFDiagnostico vDiagnostico) {
            bool vResult = false;
            decimal AlicuotaGeneral;
            decimal Alicuota2;
            decimal Alicuota3;
            string vAlicoutasRegistradasDescription = "";
            string vTexto = "";
            int vPos = 0;
            string[] vAlicuotas = new string[] { };
            vResult = BMCGetStatusCmd("S3",ref vTexto);
            if(LibString.IndexOf(vTexto,'\n') > 0) {
                vAlicuotas = LibString.Split(vTexto,'\n');
                //
                vTexto = LibString.SubString(vAlicuotas[0],vPos);
                AlicuotaGeneral = AjustarYConvertirADecimalLaAlicuota(vTexto);
                //
                vTexto = LibString.SubString(vAlicuotas[1],vPos);
                Alicuota2 = AjustarYConvertirADecimalLaAlicuota(vTexto);
                //
                vTexto = LibString.SubString(vAlicuotas[2],vPos);
                Alicuota3 = AjustarYConvertirADecimalLaAlicuota(vTexto);
                vResult = LibImpresoraFiscalUtil.ValidarAlicuotasRegistradas(AlicuotaGeneral,Alicuota2,Alicuota3,ref vAlicoutasRegistradasDescription);                
            }            
            vDiagnostico.AlicoutasRegistradasDescription = vAlicoutasRegistradasDescription;
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
            int vStatus = 0, vError = 0;
            string vMensaje = "";

            vResult = (ReadFpStatus(ref vStatus,ref vError)==1);            
            switch(vStatus) {
            case 0:
            case 4:
                vResult = true;
                vDiagnostico.ColaDeImpresioDescription = vMensaje + LibImpresoraFiscalUtil.EstatusColadeImpresionDescription(vResult);                
                break;
            case 2:
            case 3:
            case 5:
            case 6:
                vMensaje = BMCStatus[vStatus]+"\r\n";                
                vResult = false;
                vDiagnostico.ColaDeImpresioDescription = vMensaje + LibImpresoraFiscalUtil.EstatusColadeImpresionDescription(vResult);
                break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule,ref XmlDocument refXmlDocument,StringBuilder valXmlParamsExpression) {
            throw new NotImplementedException();
        }

        XElement ILibPdn.GetFk(string valCallingModule,StringBuilder valParameters) {
            throw new NotImplementedException();
        }

        bool ILibPdn.CanBeChoosen(string valCallingModule,eAccionSR valAction,string valExtendedAction,XmlDocument valXmlRow) {
            throw new NotImplementedException();
        }

        public bool ConsultarConfiguracion(IFDiagnostico iFDiagnostico) {
            throw new NotImplementedException();
        }

        public bool ImprimirDocumentoNoFiscal(string valTextoNoFiscal, string valDescripcion) {
            return true;
        }
    }
}

