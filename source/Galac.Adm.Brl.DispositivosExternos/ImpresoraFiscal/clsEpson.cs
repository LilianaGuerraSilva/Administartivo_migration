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
    public class clsEpson:IImpresoraFiscalPdn {
        #region comandos PNP
        [DllImport("PNPDLL.dll")]
        public static extern string PFAbreNF();
        [DllImport("PNPDLL.dll")]
        public static extern string PFabrefiscal(string Razon,string RIF);
        [DllImport("PNPDLL.dll")]
        public static extern string PFtotal();
        [DllImport("PNPDLL.dll")]
        public static extern string PFrepz();
        [DllImport("PNPDLL.dll")]
        public static extern string PFrepx();
        [DllImport("PNPDLL.dll")]
        public static extern string PFrenglon(string Descripcion,string cantidad,string monto,string iva);
        [DllImport("PNPDLL.dll")]
        public static extern string PFabrepuerto(string numero);
        [DllImport("PNPDLL.dll")]
        public static extern string PFcierrapuerto();
        [DllImport("PNPDLL.dll")]
        public static extern string PFDisplay950(string edlinea);
        [DllImport("PNPDLL.dll")]
        public static extern string PFLineaNF(string edlinea);
        [DllImport("PNPDLL.dll")]
        public static extern string PFCierraNF();
        [DllImport("PNPDLL.dll")]
        public static extern string PFCortar();
        [DllImport("PNPDLL.dll")]
        public static extern string PFTfiscal(string edlinea);
        [DllImport("PNPDLL.dll")]
        public static extern string PFparcial();
        [DllImport("PNPDLL.dll")]
        public static extern string PFSerial();
        [DllImport("PNPDLL.dll")]
        public static extern string PFtoteconomico();
        [DllImport("PNPDLL.dll")]
        public static extern string PFCancelaDoc(string edlinea,string monto);
        [DllImport("PNPDLL.dll")]
        public static extern string PFGaveta();
        [DllImport("PNPDLL.dll")]
        public static extern string PFDevolucion(string razon,string rif,string comp,string maqui,string fecha,string hora);
        [DllImport("PNPDLL.dll")]
        public static extern string PFSlipON();
        [DllImport("PNPDLL.dll")]
        public static extern string PFSLIPOFF();
        [DllImport("PNPDLL.dll")]
        public static extern string PFestatus(string edlinea);
        [DllImport("PNPDLL.dll")]
        public static extern string PFreset();
        [DllImport("PNPDLL.dll")]
        public static extern string PFendoso(string campo1,string campo2,string campo3,string tipoendoso);
        [DllImport("PNPDLL.dll")]
        public static extern string PFvalida675(string campo1,string campo2,string campo3,string campo4);
        [DllImport("PNPDLL.dll")]
        public static extern string PFCheque2(string mon,string ben,string fec,string c1,string c2,string c3,string c4,string campo1,string campo2);
        [DllImport("PNPDLL.dll")]
        public static extern string PFcambiofecha(string edfecha,string edhora);
        [DllImport("PNPDLL.dll")]
        public static extern string PFcambiatasa(string t1,string t2,string t3);
        [DllImport("PNPDLL.dll")]
        public static extern string PFBarra(string edbarra);
        [DllImport("PNPDLL.dll")]
        public static extern string PFVoltea();
        [DllImport("PNPDLL.dll")]
        public static extern string PFLeereloj();
        [DllImport("PNPDLL.dll")]
        public static extern string PFrepMemNF(string desf,string hasf,string modmem);
        [DllImport("PNPDLL.dll")]
        public static extern string PFRepMemoriaNumero(string desn,string hasn,string modmem);
        [DllImport("PNPDLL.dll")]
        public static extern string PFCambtipoContrib(string tip);
        [DllImport("PNPDLL.dll")]
        public static extern string PFultimo();
        [DllImport("PNPDLL.dll")]
        public static extern string PFTipoImp(string TipoImpresora);
        #endregion

        #region constantes
        const string VersionApi = "10.2.1.2";
        const string DllApiName = @"PNPDLL.dll";
        #endregion

        #region Comandos Con Retorno
        const string ConsultaEstatusPapel = "F";
        const string ConsultaEstatusContadores = "N";
        const string ConsultaUltimaNotaDeCredito = "T";
        const string ConsultaEstadoPapel = "U";
        const int _EnterosParaMonto = 10;
        const int _DecimalesParaMonto = 2;
        const int _EnterosParaCantidad = 5;
        const int _DecimalesParaCantidad = 3;
        const int EstatusFiscal = 3;
        const int LeeFecha = 2;
        const int LeeHora = 3;
        const int LeeSerial = 2;
        const int LeeEstadoPapel = 0;
        const int LeeUltimaFacturaEmitida = 9;
        const int LeeUltimaNotaDeCredito = 7;
        const int LeeUltimoCierreZRealizado = 11;
        #endregion Comandos Con Retorno

        #region variables
        private string CommPort = "";
        private eImpresoraFiscal _ModeloEpson;
        private int _MaxCantidadCaracteres = 0;
        private bool _PortIsOpen = false;
        private string _PorcAlicuotaExcenta = string.Empty;
        private string _PorcAlicuotaGeneral = string.Empty;
        private string _PorcAlicuotaReducida = string.Empty;
        private string _PorcAlicuotaAdicional = string.Empty;
        #endregion

        public clsEpson(XElement valXmlDatosImpresora) {
            ConfigurarImpresora(valXmlDatosImpresora);
        }

        private void ConfigurarImpresora(XElement valXmlDatosImpresora) {
            ePuerto ePuerto = (ePuerto)LibConvert.DbValueToEnum(LibXml.GetPropertyString(valXmlDatosImpresora,"PuertoMaquinaFiscal"));
            CommPort = ePuerto.GetDescription(1);
            string CampoModelo = LibXml.GetPropertyString(valXmlDatosImpresora,"ModeloDeMaquinaFiscal");
            _ModeloEpson = (eImpresoraFiscal)LibConvert.DbValueToEnum(CampoModelo);
        }

        private void AjustarAnchoDeLineaSegunModelo() {
            if(_ModeloEpson.Equals(eImpresoraFiscal.EPSON_PF_300)) {
                PFTipoImp("300");
                _MaxCantidadCaracteres = 48;
            } else {
                _MaxCantidadCaracteres = 38;
                PFTipoImp("0");
            }
        }

        public string ObtenerFechaYHora() {
            bool vEstado = false;
            string vMensaje = "";
            string vResult = "";
            string vFecha = "";
            string vHora = "";

            vResult = PFLeereloj();
            vEstado = CheckRequest(vResult,ref vMensaje);
            vFecha = LeerRepuestaImpFiscal(LeeFecha);
            vFecha = LibString.SubString(vFecha,4,2) + "/" + LibString.SubString(vFecha,2,2) + "/" + LibString.SubString(vFecha,0,2);
            vHora = LeerRepuestaImpFiscal(LeeHora);
            vHora = LibString.SubString(vHora,0,2) + ":" + LibString.SubString(vHora,2,2);
            vResult = vFecha + LibString.Space(1) + vHora;
            return vResult;
        }

        public bool AbrirConexion() {
            bool vEstado = false;
            string vResult = "";
            string vMensaje = "";
            try {
                if(!_PortIsOpen) {
                    vResult = PFabrepuerto(CommPort);
                    vEstado = CheckRequest(vResult,ref vMensaje);
                    _PortIsOpen = true;
                    if(!vEstado) {
                        throw new GalacException(vMensaje,eExceptionManagementType.Controlled);
                    }
                } else {
                    vEstado = true;
                }
                return vEstado;
            } catch(Exception vEx) {
                throw new GalacException("Puerto de comunicación no disponible, Revisar Conexiones " + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        public bool CerrarConexion() {
            bool vEstado = false;
            string vResult = "";
            string vMensaje = "";
            try {
                if(_PortIsOpen) {
                    vResult = PFcierrapuerto();
                    vEstado = CheckRequest(vResult,ref vMensaje);
                    _PortIsOpen = false;
                    if(!vEstado) {
                        throw new GalacException(vMensaje,eExceptionManagementType.Controlled);
                    }
                } else {
                    vEstado = true;
                }
                return vEstado;
            } catch(Exception vEx) {
                throw vEx;
            }
        }

        public bool ComprobarEstado() {
            bool vEstado = false;
            string vResult = "";
            string vMensaje = "";

            vResult = PFestatus(ConsultaEstatusContadores);
            vEstado = CheckRequest(vResult,ref vMensaje);
            if(vEstado) {
                vResult = LeerRepuestaImpFiscal(EstatusFiscal);
                vEstado = EvaluarEstadoImpresora(vResult,ref vMensaje);
                if(!vEstado) {
                    PFcierrapuerto();
                    throw new GalacException(vMensaje,eExceptionManagementType.Controlled);
                }
            } else {
                PFcierrapuerto();
                throw new GalacException(vMensaje,eExceptionManagementType.Controlled);
            }
            return vEstado;
        }

        private bool EvaluarEstadoImpresora(string valEstado,ref string refMensaje) {
            bool vResult = false;

            switch(valEstado) {
            case "00":
                vResult = true;
                refMensaje = "Lista en Espera";
                break;
            case "01":
                refMensaje = "Factura Fiscal Abierta/ Esperando Item o Cierre de Factura";
                break;
            case "02":
                refMensaje = "Documento No Fiscal en Curso / Esperando Texto de descripción o Cierre";
                break;
            case "03":
                refMensaje = "Modo Slip Activado/ Esperando Documento no Fiscal  o Cheque";
                break;
            case "04":
                refMensaje = "Más de un Día desde el ultimo cierre Z, Se Debe Hacer Cierre Z";
                break;
            case "05":
                refMensaje = "Primeras Lineas de Documento Impresas / Listo en Espera";
                vResult = true;
                break;
            case "08":
                refMensaje = "Equipo Bloqueado por Operación no Completada es Necesario un Reset de la Impresora";
                break;
            case "10":
                refMensaje = "Error de memoria Ram / El equipo necesita servicio técnico";
                break;
            case "11":
            case "13":
            case "14":
                refMensaje = "Error de memoria Rom / El equipo necesita servicio técnico";
                break;
            case "12":
                refMensaje = "Error de Fecha-Hora / El equipo necesita servicio técnico";
                break;
            default:
                refMensaje = "Error Desconocido / El equipo necesita servicio técnico";
                break;
            }
            return vResult;
        }

        public string ObtenerSerial(bool valAbrirConexion) {
            bool vEstado = false;
            string vResult = "";
            string vMensaje = "";
            string vSerial = "";

            try {
                if(valAbrirConexion) {
                    AbrirConexion();
                }
                vResult = PFSerial();
                vEstado = CheckRequest(vResult,ref vMensaje);
                if(vEstado) {
                    vSerial = LeerRepuestaImpFiscal(LeeSerial);
                } else {
                    throw new LibGalac.Aos.Catching.GalacException(vMensaje,eExceptionManagementType.Controlled);
                }
                if(valAbrirConexion) {
                    CerrarConexion();
                }
                return vSerial;
            } catch(Exception vEx) {
                CerrarConexion();
                throw vEx;
            }
        }

        public eStatusImpresorasFiscales EstadoDelPapel(bool valAbrirPuerto) {
            eStatusImpresorasFiscales vResult = eStatusImpresorasFiscales.eListoEnEspera;
            string vRequest = "";
            bool vEstado = true;
            int vSalida = 0;
            string vMensaje = "";

            try {
                //if(valAbrirPuerto) {
                //    AbrirConexion();
                //}
                //vRequest = PFestatus(ConsultaEstatusPapel);
                //vEstado = CheckRequest(vRequest,ref vMensaje);
                //vSalida = LibConvert.ToInt(LeerRepuestaImpFiscal(LeeEstadoPapel));
                //switch(vSalida) {
                //case 0:
                //vResult = eStatusImpresorasFiscales.eListoEnEspera;
                //break;
                //case 128:
                //vResult = eStatusImpresorasFiscales.eSinPapel;
                //break;
                //case 64:
                //vResult = eStatusImpresorasFiscales.ePocoPapel;
                //break;
                //}
                //if(valAbrirPuerto) {
                //    CerrarConexion();
                //}
                return vResult;
            } catch(Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException("Estado del Papel " + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        public bool RealizarReporteZ() {
            string vResult = "";
            bool vEstatus = false;
            string vMensaje = "";
            try {
                AbrirConexion();
                vResult = PFrepz();
                vEstatus = CheckRequest(vResult,ref vMensaje);
                Thread.Sleep(400);
                CerrarConexion();
                if(!vEstatus) {
                    throw new GalacException(vMensaje,eExceptionManagementType.Controlled);
                }
                return vEstatus;
            } catch(Exception vEx) {
                throw vEx;
            }
        }

        public bool RealizarReporteX() {
            string vResult = "";
            bool vEstatus = false;
            string vMensaje = "";
            try {
                AbrirConexion();
                vResult = PFrepx();
                vEstatus = CheckRequest(vResult,ref vMensaje);
                CerrarConexion();
                if(!vEstatus) {
                    throw new GalacException(vMensaje,eExceptionManagementType.Controlled);
                }
                return vEstatus;
            } catch(Exception vEx) {
                throw vEx;
            }
        }

        public string ObtenerUltimoNumeroFactura(bool valAbrirConexion) {
            string vUltimaFactura = "";
            string vMensaje = "";
            string vResult = "";
            bool vEstado = false;
            try {
                if(valAbrirConexion) {
                    AbrirConexion();
                }
                Thread.Sleep(400);
                vResult = PFestatus(ConsultaEstatusContadores);
                vEstado = CheckRequest(vResult,ref vMensaje);
                if(vEstado) {
                    vUltimaFactura = LeerRepuestaImpFiscal(LeeUltimaFacturaEmitida);
                    vUltimaFactura = LibText.FillWithCharToLeft(vUltimaFactura,"0",8);
                } else {
                    throw new LibGalac.Aos.Catching.GalacException(vMensaje,eExceptionManagementType.Controlled);
                }
                if(valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimaFactura;
            } catch(Exception vEx) {
                CerrarConexion();
                throw vEx;
            }
        }

        public string ObtenerUltimoNumeroNotaDeCredito(bool valAbrirConexion) {
            string vUltimaNDC = "";
            string vMensaje = "";
            string vResult = "";
            bool vEstado = false;

            try {
                if(valAbrirConexion) {
                    AbrirConexion();
                }
                Thread.Sleep(400);
                vResult = PFestatus(ConsultaUltimaNotaDeCredito);
                vEstado = CheckRequest(vResult,ref vMensaje);
                if(vEstado) {
                    vUltimaNDC = LeerRepuestaImpFiscal(LeeUltimaNotaDeCredito);
                    vUltimaNDC = LibText.FillWithCharToLeft(vUltimaNDC,"0",8);
                } else {
                    throw new LibGalac.Aos.Catching.GalacException(vMensaje,eExceptionManagementType.Controlled);
                }
                if(valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimaNDC;
            } catch(Exception vEx) {
                CerrarConexion();
                throw vEx;
            }
        }

        public string ObtenerUltimoNumeroReporteZ(bool valAbrirConexion) {
            string vUltimoCierreZ = "";
            string vMensaje = "";
            string vResult = "";
            bool vEstado = false;

            try {
                if(valAbrirConexion) {
                    AbrirConexion();
                }
                vResult = PFestatus(ConsultaEstatusContadores);
                vEstado = CheckRequest(vResult,ref vMensaje);
                if(vEstado) {
                    vUltimoCierreZ = LeerRepuestaImpFiscal(LeeUltimoCierreZRealizado);
                    vUltimoCierreZ = LibText.FillWithCharToLeft(vUltimoCierreZ,"0",8);
                } else {
                    throw new LibGalac.Aos.Catching.GalacException(vMensaje,eExceptionManagementType.Controlled);
                }
                if(valAbrirConexion) {
                    CerrarConexion();
                }
                return vUltimoCierreZ;
            } catch(Exception vEx) {
                CerrarConexion();
                throw vEx;
            }
        }

        public bool CancelarDocumentoFiscalEnImpresion(bool valAbrirConexion) {
            string vResult = "";
            bool vEstado = false;
            string vMensaje = "";
            const string vMontoACancelar = "0";
            const string vCancelarDocumento = "C";

            try {
                if(valAbrirConexion) {
                    AbrirConexion();
                }
                vResult = PFCancelaDoc(vCancelarDocumento,vMontoACancelar);
                vEstado = CheckRequest(vResult,ref vMensaje);
                if(!vEstado) {
                    throw new GalacException("Cancelar Documento Fiscal en Impresion ",eExceptionManagementType.Controlled);
                }
                if(valAbrirConexion) {
                    CerrarConexion();
                }
                return vEstado;
            } catch(Exception vEx) {
                CerrarConexion();
                throw vEx;
            }
        }

        public bool ImprimirFacturaFiscal(XElement vDocumentoFiscal) {
            bool vResult = false;
            try {
                AbrirConexion();                
                if(AbrirComprobanteFiscal(vDocumentoFiscal)) {
                    vResult = ImprimirTodosLosArticulos(vDocumentoFiscal);
                    vResult &= CerrarComprobanteFiscal(vDocumentoFiscal,false);
                }
                CerrarConexion();
                return vResult;
            } catch(Exception vEx) {
                CerrarConexion();
                throw new Exception("imprimir factura fiscal " + vEx.Message);
            }
        }

        private bool AbrirComprobanteFiscal(XElement valDocumentoFiscal) {
            try {
                string vRazonSocial = LibText.Trim(LibXml.GetPropertyString(valDocumentoFiscal,"NombreCliente"));
                string vRif = LibString.Left(LibXml.GetPropertyString(valDocumentoFiscal,"NumeroRIF"),12);               
                string vRazonSocialCont = "";
                bool vEstado = false;
                string vRepuesta = "";
                string vMensaje = "";
                AjustarAnchoDeLineaSegunModelo();
                if(LibString.Len(vRazonSocial) > 38) {
                    vRazonSocialCont = "NOMBRE:" + vRazonSocial; 
                    vRazonSocial = LibText.FillWithCharToLeft("","\u0020",20);
                }
                vRepuesta = PFabrefiscal(vRazonSocial,vRif);
                vEstado = CheckRequest(vRepuesta,ref vMensaje);                
                if(LibString.Len(vRazonSocialCont) > 0) {
                    vRazonSocial = LibString.Left(vRazonSocialCont,_MaxCantidadCaracteres);
                    vRazonSocialCont = LibString.SubString(vRazonSocialCont,_MaxCantidadCaracteres,_MaxCantidadCaracteres);
                    PFTfiscal(vRazonSocial);
                    PFTfiscal(vRazonSocialCont);
                    PFTfiscal(LineaSeparador());
                }
                if(!vEstado) {
                    throw new GalacException("error al abrir comprobante fiscal " + vMensaje,eExceptionManagementType.Controlled);
                }
                return vEstado;
            } catch(LibGalac.Aos.Catching.GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException(vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        private bool CerrarComprobanteFiscal(XElement vDocumentoFiscal,bool EsNotaDeCredito) {
            string vResult = "";
            string vObservaciones = "";           
            bool vEstado = true;
            string vMensaje = "";
            string vTexto = "";
            string vDireccionFiscal = "";
            string vTotalMonedaExtranjera = "";
            bool vObsrvacionesCortas = false;
            try {
                vResult = PFparcial();
                vEstado &= CheckRequest(vResult,ref vMensaje);
                if(!vEstado) {
                    throw new Exception("Error en totales");
                }
                vDireccionFiscal = LibXml.GetPropertyString(vDocumentoFiscal,"DireccionCliente");
                vObservaciones = LibXml.GetPropertyString(vDocumentoFiscal,"Observaciones");
                vTotalMonedaExtranjera = LibXml.GetPropertyString(vDocumentoFiscal,"TotalMonedaExtranjera");
                vEstado = EnviarPagos(vDocumentoFiscal);
                vEstado &= CheckRequest(vResult,ref vMensaje);
                if(!vTotalMonedaExtranjera.Equals("") && !EsNotaDeCredito) {
                    string[] vTotal = LibString.Split(vTotalMonedaExtranjera,"\n");
                    vObsrvacionesCortas = true;
                    PFTfiscal(LineaSeparador());
                    foreach(string vLinea in vTotal) {
                        PFTfiscal(vLinea);
                        vEstado &= CheckRequest(vResult,ref vMensaje);
                    }
                }
                if(!vDireccionFiscal.Equals(string.Empty)) {
                    PFTfiscal(LineaSeparador());
                    vTexto = "Direccion: " + LibText.Trim(LibText.SubString(vDireccionFiscal,0,29));
                    vResult = PFTfiscal(vTexto);
                    vEstado &= CheckRequest(vResult,ref vMensaje);
                    vTexto = LibText.Trim(LibText.SubString(vDireccionFiscal,29,40));
                    vResult = PFTfiscal(vTexto);
                    vEstado &= CheckRequest(vResult,ref vMensaje);
                }
                if(!vObservaciones.Equals(string.Empty)) {
                    PFTfiscal(LineaSeparador());
                    vTexto = "Observaciones: " + LibText.Trim(LibText.SubString(vObservaciones,0,25));
                    PFTfiscal(vTexto);
                    vEstado &= CheckRequest(vResult,ref vMensaje);
                    vTexto = LibText.Trim(LibText.SubString(vObservaciones,25,40));
                    PFTfiscal(vTexto);
                    vEstado &= CheckRequest(vResult,ref vMensaje);
                    if(!vObsrvacionesCortas) {
                        vTexto = LibText.Trim(LibText.SubString(vObservaciones,65,40));
                        PFTfiscal(vTexto);
                    }
                    vEstado &= CheckRequest(vResult,ref vMensaje);
                } else {
                    if(!vObsrvacionesCortas) {
                        vTexto = LibText.Trim(LibText.SubString(vDireccionFiscal,69,40));
                        vResult = PFTfiscal(vTexto);
                        vEstado &= CheckRequest(vResult,ref vMensaje);
                    }
                }
                PFTfiscal(LineaSeparador());
                ImprmirCamposDefinibles(vDocumentoFiscal);
                vResult = PFtotal();
                vEstado &= CheckRequest(vResult,ref vMensaje);
                if(!vEstado) {
                    throw new GalacException(vMensaje,eExceptionManagementType.Controlled);
                }
                return vEstado;
            } catch(Exception vEx) {
                CancelarDocumentoFiscalEnImpresion(false);
                throw new GalacException("Cerrar Comprobante" + vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        private bool ImprmirCamposDefinibles(XElement valData) {
            bool vResult = false;
            string vTexto = "";
            int vLineas = 0;

            List<XElement> vCamposDefinibles = valData.Descendants("GpResultDetailCamposDefinibles").ToList();
            if(vCamposDefinibles.Count > 0) {
                foreach(XElement vRecord in vCamposDefinibles) {
                    if(vLineas < 12) {
                        vTexto = LibText.SubString(LibXml.GetElementValueOrEmpty(vRecord,"CampoDefinibleValue"),0,20);
                        PFTfiscal(vTexto);
                        vLineas++;
                    } else {
                        break;
                    }
                }
                vResult = true;
            } else {
                vResult = true;
            }
            return vResult;
        }

        private bool ImprimirTodosLosArticulos(XElement valDocumentoFiscal) {
            bool vEstatus = true;
            string vResultado = string.Empty;
            string vCodigo = string.Empty; ;
            string vDescripcion;
            string vCantidad = string.Empty;
            string vTipoAlicuota = string.Empty;
            string vMonto = string.Empty;
            string vPrcDescuento = string.Empty;
            string vPorcentajeAlicuota = string.Empty;
            string vSerial = string.Empty;
            string vRollo = string.Empty;
            string vMensaje = string.Empty;
            decimal vPrcDctoDec = 0;
            string vPorcDescuentoGlobal = string.Empty;
            decimal vPorcDescuentoGlobalDec = 0;
            eStatusImpresorasFiscales PrintStatus;
            string vTotalFactura = string.Empty;
            decimal vTotalFacturaDec = 0;
            string vTotalDcto = string.Empty;
            string vDescipcionExtendida = "";
            decimal vTotalDctoDec = 0;
            decimal vMontoDctoAlicuotaGDec = 0;
            decimal vMontoDctoAlicuotaRDec = 0;
            decimal vMontoDctoAlicuotaADec = 0;
            decimal vMontoDctoAlicuotaEDec = 0;

            try {
                vPorcDescuentoGlobal = LibXml.GetPropertyString(valDocumentoFiscal,"PorcentajeDescuento");
                vPorcDescuentoGlobal = SetDecimalSeparator(vPorcDescuentoGlobal,_DecimalesParaMonto);
                vPorcDescuentoGlobalDec = LibImportData.ToDec(vPorcDescuentoGlobal);
                vTotalFactura = LibXml.GetPropertyString(valDocumentoFiscal,"TotalFactura");
                vTotalFactura = SetDecimalSeparator(vTotalFactura,_DecimalesParaMonto);
                vTotalFacturaDec = LibImportData.ToDec(vTotalFactura);
                List<XElement> vRecord = valDocumentoFiscal.Descendants("GpResultDetailRenglonFactura").ToList();
                foreach(XElement vXElement in vRecord) {
                    PrintStatus = EstadoDelPapel(false);
                    vDescripcion = LibXml.GetElementValueOrEmpty(vXElement,"Descripcion");
                    vCodigo = LibXml.GetElementValueOrEmpty(vXElement,"Articulo");
                    vSerial = LibXml.GetElementValueOrEmpty(vXElement,"Serial");
                    vRollo = LibXml.GetElementValueOrEmpty(vXElement,"Rollo");                
                    vCantidad = LibXml.GetElementValueOrEmpty(vXElement,"Cantidad");
                    vMonto = LibXml.GetElementValueOrEmpty(vXElement,"PrecioSinIVA");
                    vPorcentajeAlicuota = LibXml.GetElementValueOrEmpty(vXElement,"PorcentajeAlicuota");
                    vPorcentajeAlicuota = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vPorcentajeAlicuota,2,2);
                    vPrcDescuento = LibXml.GetElementValueOrEmpty(vXElement,"PorcentajeDescuento");
                    vTipoAlicuota = LibXml.GetElementValueOrEmpty(vXElement,"AlicuotaIva");
                    vMonto = SetDecimalSeparator(vMonto,_DecimalesParaMonto);
                    vCantidad = SetDecimalSeparator(vCantidad,_DecimalesParaCantidad);
                    vPrcDescuento = SetDecimalSeparator(vPrcDescuento,_DecimalesParaMonto);
                    vPrcDctoDec = LibImportData.ToDec(vPrcDescuento,2);
                    if(LibString.Len(vSerial) > 0 && LibString.Len(vDescipcionExtendida) == 0) {
                        vResultado = PFTfiscal(LibText.SubString(vSerial,0,20));
                        vEstatus &= CheckRequest(vResultado,ref vMensaje);
                    }
                    if(LibString.Len(vRollo) > 0 && LibString.Len(vDescipcionExtendida) == 0) {
                        vResultado = PFTfiscal(LibText.SubString(vRollo,0,20));
                        vEstatus &= CheckRequest(vResultado,ref vMensaje);
                    }
                    if(vPrcDctoDec > 0) {
                        vResultado = PFTfiscal(" Valor Art: " + vMonto + " Desc: " + LibImpresoraFiscalUtil.DecimalToStringFormat(vPrcDctoDec,2) + " %");
                        vEstatus &= CheckRequest(vResultado,ref vMensaje);
                        var vMontoDec = CalcularDescuentoRenglon(vPrcDctoDec,LibImportData.ToDec(vMonto));
                        vMonto = LibImpresoraFiscalUtil.DecimalToStringFormat(vMontoDec,_DecimalesParaMonto);
                    }
                    if(vPorcDescuentoGlobalDec > 0) {
                        vTotalDctoDec = CalcularDescuentoGlobal(vPorcDescuentoGlobalDec,LibImportData.ToDec(vMonto),LibImportData.ToDec(vCantidad));                       
                        TotalizarDescuentoGlobalPorAlicuota(vTipoAlicuota,vPorcentajeAlicuota,vTotalDctoDec,ref vMontoDctoAlicuotaGDec,ref vMontoDctoAlicuotaRDec,ref vMontoDctoAlicuotaADec,ref vMontoDctoAlicuotaEDec);
                    }
                    vCantidad = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vCantidad,_EnterosParaCantidad,_DecimalesParaCantidad,".");
                    vMonto = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vMonto,_EnterosParaMonto,_DecimalesParaMonto,".");
                    vDescripcion = vCodigo+" | " + vDescripcion;
                    vResultado = PFrenglon(vDescripcion,vCantidad,vMonto,vPorcentajeAlicuota);
                    vEstatus &= CheckRequest(vResultado,ref vMensaje);                    
                    if(!vEstatus) {
                        throw new GalacException("Error al Imprimir Articulo",eExceptionManagementType.Controlled);
                    }
                }
                if(vPorcDescuentoGlobalDec > 0) {                    
                    PFTfiscal(LineaSeparador());
                    vEstatus &= AplicaDctoGlobal(vTipoAlicuota,vPorcDescuentoGlobal,vMontoDctoAlicuotaGDec,vMontoDctoAlicuotaRDec,vMontoDctoAlicuotaADec,vMontoDctoAlicuotaEDec);
                    if(!vEstatus) {
                        throw new GalacException("Error Aplicar Dcto al Imprimir Articulo",eExceptionManagementType.Controlled);
                    }
                }
            } catch(Exception vEx) {
                CancelarDocumentoFiscalEnImpresion(false);
                throw vEx;
            }
            return vEstatus;
        }

        private string LineaSeparador() {
            return LibText.FillWithCharToLeft("","-",(byte)_MaxCantidadCaracteres);
        }

        private decimal CalcularDescuentoRenglon(decimal valPrcDescuento,decimal valMonto) {          
            decimal vRenglonDescuento = 0;                      
            vRenglonDescuento = valMonto * valPrcDescuento / 100m;
            vRenglonDescuento = LibMath.RoundToNDecimals(valMonto - vRenglonDescuento,3);   
            return vRenglonDescuento;
        }

        private decimal CalcularDescuentoGlobal(decimal valPrcDescuento,decimal valMonto,decimal valCantidad) {            
            decimal vDescuentoGlobal = 0;                        
            vDescuentoGlobal = LibMath.RoundToNDecimals((valCantidad * valMonto) * valPrcDescuento / 100m,3);           
            return vDescuentoGlobal;
        }

        private string SetDecimalSeparator(string valNumero,int vCantidaDecimales) {
            string vResult = "";
            string vDecimalSeparator = LibConvert.CurrentDecimalSeparator();
            string vGroupSeparator = LibConvert.CurrentGroupSeparator();
            int vPost = 0;

            vPost = LibString.Len(valNumero) - vCantidaDecimales - 1;
            vPost = LibString.IndexOf(valNumero,vGroupSeparator,vPost);
            if(vPost > 0) {
                vResult = LibText.Replace(valNumero,vGroupSeparator,vDecimalSeparator);
            } else {
                vResult = valNumero;
            }
            vResult = LibString.Replace(vResult,vGroupSeparator,"");
            return vResult;
        }

        private bool AplicaDctoGlobal(string valTipoAlicuota,string valPorcDescuentoGlobal,decimal valMontoDctoAlicuotaGDec,decimal valMontoDctoAlicuotaRDec,decimal valMontoDctoAlicuotaADec,decimal valMontoDctoAlicuotaEDec) {
            string vMontoDctoAlicuotaG = "";
            string vMontoDctoAlicuotaR = "";
            string vMontoDctoAlicuotaA = "";
            string vMontoDctoAlicuotaE = "";
            string vMensaje = "";
            string vResultado = "";
            bool vEstatus = true;

            if(valMontoDctoAlicuotaEDec > 0) {
                vMontoDctoAlicuotaE = LibImpresoraFiscalUtil.DecimalToStringFormat(valMontoDctoAlicuotaEDec,_DecimalesParaMonto);                
                vMontoDctoAlicuotaE = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vMontoDctoAlicuotaE,_EnterosParaMonto,_DecimalesParaMonto,".");
                vResultado = PFrenglon("Dcto Total " + valPorcDescuentoGlobal + " %","-1",vMontoDctoAlicuotaE,_PorcAlicuotaExcenta);
                vEstatus &= CheckRequest(vResultado,ref vMensaje);
            }
            if(valMontoDctoAlicuotaGDec > 0) {
                vMontoDctoAlicuotaG = LibImpresoraFiscalUtil.DecimalToStringFormat(valMontoDctoAlicuotaGDec,_DecimalesParaMonto);                
                vMontoDctoAlicuotaG = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vMontoDctoAlicuotaG,_EnterosParaMonto,_DecimalesParaMonto,".");
                vResultado = PFrenglon("Dcto Total " + valPorcDescuentoGlobal + " %","-1",vMontoDctoAlicuotaG,_PorcAlicuotaGeneral);
                vEstatus &= CheckRequest(vResultado,ref vMensaje);
            }
            if(valMontoDctoAlicuotaRDec > 0) {
                vMontoDctoAlicuotaR = LibImpresoraFiscalUtil.DecimalToStringFormat(valMontoDctoAlicuotaRDec,_DecimalesParaMonto);            
                vMontoDctoAlicuotaR = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vMontoDctoAlicuotaR,_EnterosParaMonto,_DecimalesParaMonto,".");
                vResultado = PFrenglon("Dcto Total " + valPorcDescuentoGlobal + " %","-1",vMontoDctoAlicuotaR,_PorcAlicuotaReducida);
                vEstatus &= CheckRequest(vResultado,ref vMensaje);
            }
            if(valMontoDctoAlicuotaADec > 0) {
                vMontoDctoAlicuotaA = LibImpresoraFiscalUtil.DecimalToStringFormat(valMontoDctoAlicuotaADec,_DecimalesParaMonto);                
                vMontoDctoAlicuotaA = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vMontoDctoAlicuotaA,_EnterosParaMonto,_DecimalesParaMonto,".");
                vResultado = PFrenglon("Dcto Total " + valPorcDescuentoGlobal + " %","-1",vMontoDctoAlicuotaA,_PorcAlicuotaAdicional);
                vEstatus &= CheckRequest(vResultado,ref vMensaje);
            }
            return vEstatus;
        }

        private bool EnviarPagos(XElement valMedioDePago) {
            string vMedioDePago = string.Empty;
            string vMonto = string.Empty;
            decimal vMontoDec = 0;
            string vMensaje = string.Empty;
            bool vEstado = false;
            string vResult = string.Empty;
            decimal vTotalMontosPagados = 0;
            decimal vTotalFacturaDec = 0;
            string vTotalFacturaStr = string.Empty;
            decimal vMontoDiferencia = 0;

            try {
                vTotalFacturaStr = LibXml.GetPropertyString(valMedioDePago,"TotalFactura");
                vTotalFacturaStr = SetDecimalSeparator(vTotalFacturaStr,_DecimalesParaMonto);
                vTotalFacturaDec = LibMath.Abs(LibImportData.ToDec(vTotalFacturaStr,3));
                List<XElement> vNodos = valMedioDePago.Descendants("GpResultDetailRenglonCobro").ToList();
                if(vNodos.Count > 1) {
                    foreach(XElement vXElement in vNodos) {
                        vMedioDePago = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement,"CodigoFormaDelCobro"));
                        vMonto = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement,"Monto"));
                        vMedioDePago = FormaDeCobro(vMedioDePago);
                        vMontoDec = LibImportData.ToDec(vMonto,3);
                        vMonto = LibImpresoraFiscalUtil.DarFormatoNumericoParaImpresion(vMonto,_EnterosParaMonto,_DecimalesParaMonto,".");
                        vTotalMontosPagados += vMontoDec;
                        vResult = PFTfiscal(vMedioDePago + " : Bs " + LibImpresoraFiscalUtil.DecimalToStringFormat(vMontoDec,2));
                        vEstado = CheckRequest(vResult,ref vMensaje);
                    }
                } else {
                    return true;
                }
                vMontoDiferencia = LibMath.Abs(vTotalFacturaDec - vTotalMontosPagados);
                if(vMontoDiferencia > 0.01M) {
                    vResult = PFTfiscal("Cambio Bs : " + LibImpresoraFiscalUtil.DecimalToStringFormat(vMontoDiferencia,2));
                    vEstado = CheckRequest(vResult,ref vMensaje);
                }
                return vEstado;
            } catch(Exception vEx) {
                throw new LibGalac.Aos.Catching.GalacException(vEx.Message,eExceptionManagementType.Controlled);
            }
        }

        private string FormaDeCobro(string vFormaDeCobro) {
            string vResultado = "";
            vResultado = LibImpresoraFiscalUtil.GetFormaDePago(vFormaDeCobro);
            if(vResultado.Equals("")) {
                vResultado = "Efectivo";
            }
            return vResultado;
        }

        private bool AbrirNC(XElement valDocumentoFiscal) {
            bool vResult = false;
            string vDireccion = LibXml.GetPropertyString(valDocumentoFiscal,"DireccionCliente");
            string vRif = LibXml.GetPropertyString(valDocumentoFiscal,"NumeroRIF");
            string vRazonSocial = LibXml.GetPropertyString(valDocumentoFiscal,"NombreCliente");
            string vObservaciones = LibXml.GetPropertyString(valDocumentoFiscal,"Observaciones");
            string vNumFactura = LibXml.GetPropertyString(valDocumentoFiscal,"NumeroComprobanteFiscal");
            string vSerialMaquina = LibXml.GetPropertyString(valDocumentoFiscal,"SerialMaquinaFiscal");
            string vFecha = LibXml.GetPropertyString(valDocumentoFiscal,"Fecha");
            string vHora = LibXml.GetPropertyString(valDocumentoFiscal,"HoraModificacion");
            string vRazonSocialCont = "";
            vFecha = LibGalac.Aos.Base.LibString.Replace(vFecha,"-","/");
            vFecha = LibConvert.ToStr(LibConvert.ToDate(vFecha),"dd/MM/yy");
            vRif = LibText.SubString(vRif,0,12);
            string vReq = "";
            string vMensaje = "";
            AjustarAnchoDeLineaSegunModelo();
            if(LibString.Len(vRazonSocial) > 38) {
                vRazonSocialCont = "NOMBRE:" + vRazonSocial;
                vRazonSocial = LibText.FillWithCharToLeft("","\u0020",20);
            }
            vReq = PFDevolucion(vRazonSocial,vRif,vNumFactura,vSerialMaquina,vFecha,vHora);
            vResult = CheckRequest(vReq,ref vMensaje);
            if(LibString.Len(vRazonSocialCont) > 0) {
                vRazonSocial = LibString.Left(vRazonSocialCont,_MaxCantidadCaracteres);
                vRazonSocialCont = LibString.SubString(vRazonSocialCont,_MaxCantidadCaracteres,_MaxCantidadCaracteres);
                PFTfiscal(vRazonSocial);
                PFTfiscal(vRazonSocialCont);
                PFTfiscal(LineaSeparador());
            }
            if(!vResult) {
                throw new GalacException("error al abrir la nota de crédito " + vMensaje,eExceptionManagementType.Controlled);
            }
            return vResult;
        }

        public bool ImprimirNotaCredito(XElement valDocumentoFiscal) {
            bool vResult = false;
            try {
                AbrirConexion();
                if(AbrirNC(valDocumentoFiscal)) {
                    vResult = ImprimirTodosLosArticulos(valDocumentoFiscal);
                    vResult &= CerrarComprobanteFiscal(valDocumentoFiscal,true);
                }
                CerrarConexion();
                return vResult;
            } catch(Exception vEx) {
                CerrarConexion();
                throw new Exception("imprimir factura fiscal " + vEx.Message);
            }
        }

        private int ConvierteBinarioADecimal(string valBinario) {
            int vResult = 0;
            int vPotencia = LibString.Len(valBinario) - 1;

            double vBase = 0;

            for(int posicion = 0;posicion < LibString.Len(valBinario);posicion++) {
                vBase = vBase + LibConvert.ToDouble(LibString.SubString(valBinario,posicion,1)) * Math.Pow(2,vPotencia);
                vPotencia--;
            }
            vResult = LibConvert.ToInt(vBase);
            return vResult;
        }       

        private void TotalizarDescuentoGlobalPorAlicuota(string valAlicuotas,string valPorcAlicuota,decimal valMontoDesctotal,ref decimal refMontoDescuentoAlicuotaG,ref decimal refMontoDescuentoAlicuotaR,ref decimal refMontoDescuentoAlicuotaA,ref decimal refMontoDescuentoAlicuotaE) {
            switch(valAlicuotas) {
            case "1":
                _PorcAlicuotaGeneral = valPorcAlicuota;
                refMontoDescuentoAlicuotaG = refMontoDescuentoAlicuotaG + valMontoDesctotal;
                break;
            case "2":
                _PorcAlicuotaReducida = valPorcAlicuota;
                refMontoDescuentoAlicuotaR = refMontoDescuentoAlicuotaR + valMontoDesctotal;
                break;
            case "3":
                _PorcAlicuotaAdicional = valPorcAlicuota;
                refMontoDescuentoAlicuotaA = refMontoDescuentoAlicuotaA + valMontoDesctotal;
                break;
            case "0":
                _PorcAlicuotaExcenta = valPorcAlicuota;
                refMontoDescuentoAlicuotaE = refMontoDescuentoAlicuotaE + valMontoDesctotal;
                break;
            }
        }

        private bool EstadoImpresora(string valEstado,ref string refMensaje) {
            bool vResult = false;
            int EstadoEnBaseDecimal = ConvierteBinarioADecimal(valEstado);

            if(EstadoEnBaseDecimal >= 16384) { // bit 14
                EstadoEnBaseDecimal >>= 11;
                refMensaje = refMensaje + " Impresora sin papel";
            }

            if(EstadoEnBaseDecimal >= 8) { // bit 3
                EstadoEnBaseDecimal >>= 1;
                refMensaje = refMensaje + " Impresora Fuera de linea";
            }

            if(EstadoEnBaseDecimal >= 4) { // bit 2
                EstadoEnBaseDecimal >>= 1;
                refMensaje = refMensaje + " Error en Impresora";
            }
            return vResult;
        }

        private string LeerRepuestaImpFiscal(int vItemCampo) {
            string vResult = "";
            string[] vField = new string[] { };
            string vTrama = "";
            vTrama = PFultimo();
            vField = LibString.Split(vTrama,',');
            vResult = vField[vItemCampo];
            return vResult;
        }

        private bool CheckRequest(string valSendRequest,ref string vMensaje) {
            bool vResult = true;

            switch(valSendRequest) {
            case "OK":
                vResult = true;
                vMensaje = "Satisfactorio";
                break;
            case "ER":
                vResult = false;
                vMensaje = "Error de Comando";
                break;
            case "NP":
                vResult = false;
                vMensaje = "Puerto No Abierto";
                break;
            case "TO":
                vResult = false;
                vMensaje = "La impresora fiscal no responde. Revisar Conexiones";
                break;
            }
            return vResult;
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
            string vReq = "";
            bool vResult = false;
            string vMensaje = "";
            vReq = PFestatus(ConsultaEstatusContadores);
            vResult = CheckRequest(vReq,ref vMensaje);
            vDiagnostico.EstatusDeComunicacionDescription = vMensaje + "\r\n" + LibImpresoraFiscalUtil.EstatusDeComunicacionDescription(vResult);
            return vResult;
        }

        public bool VersionDeControladores(IFDiagnostico vDiagnostico) {
            bool vResult = false;
            bool vIsSameVersion = false;
            string vVersion = "";
            string vDir = "";
            vDir = System.IO.Path.Combine(LibApp.AppPath()+"CDP",DllApiName);
            vResult = LibImpresoraFiscalUtil.ObtenerVersionDeControlador(vDir,ref vVersion);
            vIsSameVersion = (vVersion == VersionApi);
            vDiagnostico.VersionDeControladoresDescription = LibImpresoraFiscalUtil.EstatusVersionDeControladorDescription(vResult,vIsSameVersion,vDir);
            vResult = vIsSameVersion;
            return vResult;
        }

        public bool AlicuotasRegistradas(IFDiagnostico vDiagnostico) {
            bool vResult = true;
            decimal AlicuotaGeneral;
            decimal Alicuota2;
            decimal Alicuota3;
            int vReq = 0;
            string RecAlicuotas = "";
            //RecAlicuotas = LibText.FillWithCharToRight(RecAlicuotas," ",80);
            //vReq = Bematech_FI_RetornoAlicuotas(ref RecAlicuotas);
            //RecAlicuotas = LibText.CleanSpacesToBothSides(LibText.Replace(RecAlicuotas,"\0",""));
            //string[] ListAlicuotas = LibString.Split(RecAlicuotas,',');
            //AlicuotaGeneral = LibImportData.ToDec(LibString.InsertAt(ListAlicuotas[0],".",2),2);
            //Alicuota2 = LibImportData.ToDec(LibString.InsertAt(ListAlicuotas[1],".",2),2);
            //Alicuota3 = LibImportData.ToDec(LibString.InsertAt(ListAlicuotas[2],".",2),2);
            //vResult = LibImpresoraFiscalUtil.ValidarAlicuotasRegistradas(AlicuotaGeneral,Alicuota2,Alicuota3,ref refAlicoutasRegistradasDescription);
            vDiagnostico.AlicoutasRegistradasDescription = "Función no Implementada para este Modelo";
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
            string vRetorno = "";
            string vMensaje = "";
            vRetorno = PFestatus(ConsultaEstatusContadores);
            vResult = CheckRequest(vRetorno,ref vMensaje);
            vRetorno = LeerRepuestaImpFiscal(EstatusFiscal);
            vResult = EvaluarEstadoImpresora(vRetorno,ref vMensaje);
            vDiagnostico.ColaDeImpresioDescription = (LibString.IsNullOrEmpty(vMensaje) ? "Listo En Espera" : vMensaje);
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
