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
    public class clsEpson : IImpresoraFiscalPdn {
        #region comandos
        [DllImport("PNPDLL.dll")]
        public static extern string PFAbreNF();
        [DllImport("PNPDLL.dll")]
        public static extern string PFabrefiscal(string Razon, string RIF);
        [DllImport("PNPDLL.dll")]
        public static extern string PFtotal();
        [DllImport("PNPDLL.dll")]
        public static extern string PFrepz();
        [DllImport("PNPDLL.dll")]
        public static extern string PFrepx();
        [DllImport("PNPDLL.dll")]
        public static extern string PFrenglon(string Descripcion, string cantidad, string monto, string iva);
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
        public static extern string PFPago(string edlinea, string monto);
        [DllImport("PNPDLL.dll")]
        public static extern string PFDescuento(string edbarra, string monto);
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
        public static extern string PFCancelaDoc(string edlinea, string monto);
        [DllImport("PNPDLL.dll")]
        public static extern string PFGaveta();
        [DllImport("PNPDLL.dll")]
        public static extern string PFDevolucion(string razon, string rif, string comp, string maqui, string fecha, string hora);
        [DllImport("PNPDLL.dll")]
        public static extern string PFSlipON();
        [DllImport("PNPDLL.dll")]
        public static extern string PFSLIPOFF();
        [DllImport("PNPDLL.dll")]
        public static extern string PFestatus(string edlinea);
        [DllImport("PNPDLL.dll")]
        public static extern string PFreset();
        [DllImport("PNPDLL.dll")]
        public static extern string PFendoso(string campo1, string campo2, string campo3, string tipoendoso);
        [DllImport("PNPDLL.dll")]
        public static extern string PFvalida675(string campo1, string campo2, string campo3, string campo4);
        [DllImport("PNPDLL.dll")]
        public static extern string PFCheque2(string mon, string ben, string fec, string c1, string c2, string c3, string c4, string campo1, string campo2);
        [DllImport("PNPDLL.dll")]
        public static extern string PFcambiofecha(string edfecha, string edhora);
        [DllImport("PNPDLL.dll")]
        public static extern string PFcambiatasa(string t1, string t2, string t3);
        [DllImport("PNPDLL.dll")]
        public static extern string PFBarra(string edbarra);
        [DllImport("PNPDLL.dll")]
        public static extern string PFVoltea();
        [DllImport("PNPDLL.dll")]
        public static extern string PFLeereloj();
        [DllImport("PNPDLL.dll")]
        public static extern string PFrepMemNF(string desf, string hasf, string modmem);
        [DllImport("PNPDLL.dll")]
        public static extern string PFRepMemoriaNumero(string desn, string hasn, string modmem);
        [DllImport("PNPDLL.dll")]
        public static extern string PFCambtipoContrib(string tip);
        [DllImport("PNPDLL.dll")]
        public static extern string PFultimo();
        #endregion

        private string CommPort = "";
        private eImpresoraFiscal vModeloEpson;

        public clsEpson(XElement valXmlDatosImpresora) {
            ConfigurarImpresora(valXmlDatosImpresora);            
        }

        private void ConfigurarImpresora(XElement valXmlDatosImpresora) {
            CommPort = clsImpresoraFiscalUtil.ObtenerValorDesdeXML(valXmlDatosImpresora, "PuertoMaquinaFiscal");
            string CampoModelo = LibXml.GetPropertyString(valXmlDatosImpresora, "ModeloDeMaquinaFiscal");
            vModeloEpson = (eImpresoraFiscal)LibConvert.DbValueToEnum(CampoModelo);
        }

        public string ObtenerFechaYHora() {
            bool vEstado =false;
            string vMensaje="";
            string vResult = "";
            string vFecha = "";
            string vHora = "";
            vResult=PFLeereloj();            
            vEstado=CheckRequest(vResult,ref vMensaje);
            vFecha = LeerRepuestaImpFiscal(6);
            vFecha=LibString.SubString(vFecha,3,2)+"/"+LibString.SubString(vFecha,1,2)+"/"+LibString.SubString(vFecha,0,2);
            vHora = LeerRepuestaImpFiscal(7);
            vHora = LibString.SubString(vHora, 3, 2) + ":" + LibString.SubString(vHora, 1, 2);
            return vResult;
        }

        public bool AbrirConexion() {
            bool vEstado = false;
            string vResult = "";
            string vMensaje="";
            try {
                vResult = PFabrepuerto(CommPort);
                vEstado = CheckRequest(vResult, ref vMensaje);
                if (!vEstado) {
                    throw new GalacException(vMensaje,eExceptionManagementType.Controlled);
                }
                return vEstado;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        public bool CerrarConexion() {
            bool vEstado = false;
            string vResult = "";
            string vMensaje = "";
            try {
                vResult = PFcierrapuerto();
                vEstado = CheckRequest(vResult, ref vMensaje);
                if (!vEstado) {
                    throw new GalacException(vMensaje, eExceptionManagementType.Controlled);
                }
                return vEstado;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        public bool ComprobarConexion() {
            bool vEstado = false;
            string vResult = "";
            string vMensaje = "";

            vResult = PFestatus("N");
            vEstado = CheckRequest(vResult, ref vMensaje);
            if (vEstado) {
                vResult = LeerRepuestaImpFiscal(4);
                vEstado = EvaluarEstadoImpresora(vResult, ref vMensaje);
            } else {
                throw new GalacException(vMensaje,eExceptionManagementType.Controlled);
            }
            return vEstado;
        }

        public bool EvaluarEstadoImpresora(string valEstado, ref string refMensaje){
            bool vResult = false;

            switch (valEstado) {
                case"00":
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
                    refMensaje = "Más de un Día desde el ultimo cierre Z, Se DebeHacer Cierre Z";                                       
                    break;
                case "05":                    
                    refMensaje = "Documento Fiscal Incompleto, Cancelar documento para continuar";                                       
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

        public string ObtenerSerial() {
            bool vEstado = false;
            string vResult = "";
            string vMensaje = "";
            string vSerial = "";

            try {
                if (AbrirConexion()) {
                    vResult = PFSerial();
                    vEstado = CheckRequest(vResult, ref vMensaje);
                    CerrarConexion();
                    if (vEstado) {
                        vSerial = LeerRepuestaImpFiscal(3);
                    } else {
                        throw new LibGalac.Aos.Catching.GalacException(vMensaje, eExceptionManagementType.Controlled);
                    }
                }
                return vResult;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        public eStatusImpresorasFiscales EstadoDelPapel(bool valAbrirPuerto) {
            eStatusImpresorasFiscales vResult = eStatusImpresorasFiscales.eListoEnEspera;            
            string vRequest = "";
            bool vEstado = false;
            int vSalida = 0;
            string vMensaje = "";

            try {
                if (valAbrirPuerto) {
                    AbrirConexion();
                }
                vRequest = PFestatus("F");
                vEstado = CheckRequest(vRequest,ref vMensaje);
                vSalida = LibConvert.ToInt(LeerRepuestaImpFiscal(6));

                switch (vSalida) {
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
            string vResult = "";
            bool vEstatus = false;
            string vMensaje = "";
            try {
                if (AbrirConexion()) {
                    vResult = PFrepz();
                    vEstatus = CheckRequest(vResult,ref vMensaje);
                    CerrarConexion();
                    if (!vEstatus) {
                        throw new GalacException(vMensaje, eExceptionManagementType.Controlled);
                    }
                }
                return vEstatus;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        public bool RealizarReporteX() {
            string vResult = "";
            bool vEstatus = false;
            string vMensaje = "";
            try {
                if (AbrirConexion()) {
                    vResult = PFrepx();
                    vEstatus = CheckRequest(vResult, ref vMensaje);
                    CerrarConexion();
                    if (!vEstatus) {
                        throw new GalacException(vMensaje, eExceptionManagementType.Controlled);
                    }
                }
                return vEstatus;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        public string ObtenerUltimoNumeroFactura() {
            string vUltimaFactura = LibText.Space(8);
            string vMensaje = "";
            string vResult = "";
            bool vEstado = false;

            try {
                if (AbrirConexion()) {

                    vResult = PFestatus("N");
                    vEstado = CheckRequest(vResult, ref vMensaje);

                    CerrarConexion();

                    if (!vEstado) {
                        throw new GalacException(vMensaje, eExceptionManagementType.Controlled);
                    }
                }
                return vUltimaFactura;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        public string ObtenerUltimoNumeroNotaDeCredito() {
            string vUltimaFactura = LibText.Space(8);
            string vMensaje = "";
            string vResult = "";
            bool vEstado = false;
            string Mensaje = "";

            try {
                if (AbrirConexion()) {

                    vResult = PFestatus("N");
                    vEstado = CheckRequest(vResult, ref vMensaje);

                    CerrarConexion();

                    if (!vEstado) {
                        throw new GalacException(Mensaje, eExceptionManagementType.Controlled);
                    }
                }
                return vUltimaFactura;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        public string ObtenerUltimoNumeroReporteZ() {
            string vUltimaFactura = LibText.Space(8);
            string vMensaje = "";
            string vResult = "";
            bool vEstado = false;
            string Mensaje = "";

            try {
                if (AbrirConexion()) {

                    vResult = PFestatus("N");
                    vEstado = CheckRequest(vResult, ref vMensaje);

                    CerrarConexion();

                    if (!vEstado) {
                        throw new GalacException(Mensaje, eExceptionManagementType.Controlled);
                    }
                }
                return vUltimaFactura;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        public bool CancelarDocumentoFiscalEnImpresion(bool valAbrirConexion) {         
            string vResult = "";
            bool vEstado = false;
            string vMensaje = "";

            try {
                if (valAbrirConexion) {
                    AbrirConexion();
                }
                vResult = PFCancelaDoc("C", "0");
                vEstado = CheckRequest(vResult, ref vMensaje);
                if (!vEstado) {
                    throw new GalacException("Cancelar Documento Fiscal en Impresion ", eExceptionManagementType.Controlled);
                }
                return vEstado;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        public bool ImprimirFacturaFiscal(XElement vDocumentoFiscal) {          
            string vRif = LibXml.GetPropertyString(vDocumentoFiscal, "NumeroRIF");
            string vRazonSocial = LibXml.GetPropertyString(vDocumentoFiscal, "NombreCliente");            
            bool vResult = false;
            try {
                if (AbrirConexion()) {
                    if (AbrirComprobanteFiscal(vRazonSocial, vRif)) {
                        vResult = ImprimirTodosLosArticulos(vDocumentoFiscal, false);
                        vResult &= CerrarComprobanteFiscal(vDocumentoFiscal, false);
                        CerrarConexion();
                    }
                } else {
                    throw new GalacException("Error de Conexión con la impresora fiscal", eExceptionManagementType.Controlled);
                }
            } catch (LibGalac.Aos.Catching.GalacException vEx) {                
                throw new GalacException("imprimir factura fiscal " + vEx.Message, eExceptionManagementType.Controlled);
            }
            return vResult;
        }

        private bool AbrirComprobanteFiscal(string valRazonSocial, string valRif) {
            try {
                bool vEstado = false;
                string vRepuesta = "";
                string vMensaje = "";
				// OJO CHEQUEAR EN PRUEBAS LO DE LIMITAR A 28/40 caracteres
                //valRazonSocial = LibText.SubString(valRazonSocial, 0, 40);
                valRif = LibText.SubString(valRif, 0, 18);              
                vRepuesta = PFabrefiscal( valRazonSocial,valRif);
                vEstado = CheckRequest(vRepuesta ,ref vMensaje);
                if (!vEstado) {
                    throw new GalacException("error al abrir comprobante fiscal " + vMensaje, eExceptionManagementType.Controlled);
                }
                return vEstado;
            } catch (LibGalac.Aos.Catching.GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private bool CerrarComprobanteFiscal(XElement vDocumentoFiscal, bool EsNotaDeCredito) {
            string vResult = "";            
            string valDescuentoTotal = "";
            string vDireccionFiscal = "";
            bool vImprimeDireccionALFinalDeLaFactura = false;
            string vObservaciones = "";
            bool vUsaCamposDefinibles = false;
            bool vEstado = false;
            string vMensaje = "";
            
            try {
                vImprimeDireccionALFinalDeLaFactura = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "ImprimeDireccionAlFinalDelComprobanteFiscal");
                vUsaCamposDefinibles = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "UsaCamposDefinibles");
                valDescuentoTotal = LibXml.GetPropertyString(vDocumentoFiscal, "PorcentajeDescuento");
                vDireccionFiscal = LibXml.GetPropertyString(vDocumentoFiscal, "DireccionCliente");
                vObservaciones = LibXml.GetPropertyString(vDocumentoFiscal, "Observaciones");
                valDescuentoTotal = DarFormatoNumericoParaDescuento(valDescuentoTotal);
                vResult = PFDescuento("D",valDescuentoTotal);
                vEstado = CheckRequest(vResult,ref vMensaje);
                vEstado &= EnviarPagos(vDocumentoFiscal);
                vResult = PFparcial();
                vEstado &= CheckRequest(vResult, ref vMensaje);

                if (vImprimeDireccionALFinalDeLaFactura) {
                    vEstado &= ImprimeDireccionFiscal(vDireccionFiscal);
                }
                vEstado &= ImprimeObservaciones(vObservaciones);
                vResult = PFtotal();
                vEstado &= CheckRequest(vResult, ref vMensaje);
                
                if (!vEstado) {
                    throw new GalacException(vMensaje, eExceptionManagementType.Controlled);
                }
                return true;
            } catch (GalacException vEx) {
                throw new GalacException("Cerrar Comprobante" + vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private bool ImprimeDireccionFiscal(string valDireccion) {
            bool vEstado = false;
            string vResult = "";
            string vMensaje = "";
            string vTextoDireccion = "";
            string vTextoAImprimir = "";
            int vMaximaLongitud = 0;
            int vPosicion = 0;
            const int vCantidadCaracteres = 40;

            vTextoDireccion = "Direccion: " + valDireccion;
            vMaximaLongitud = LibString.Len(vTextoDireccion);

            for (int vCantidadDeLineas = 0; vCantidadDeLineas < 3; vCantidadDeLineas++) {
                vTextoAImprimir = LibString.SubString(vTextoDireccion, vPosicion, vCantidadCaracteres);
                vPosicion += vCantidadCaracteres;
                vResult = PFTfiscal(vTextoAImprimir);
                vEstado &= CheckRequest(vResult, ref vMensaje);
            }
            return vEstado;
        }

        private bool ImprimeObservaciones(string valObservaciones) {
            bool vEstado = false;
            string vResult = "";
            string vMensaje = "";
            string vTextoObservaciones = "";
            string vTextoAImprimir = "";
            int vMaximaLongitud = 0;
            int vPosicion = 0;
            const int vCantidadCaracteres = 38;

            vTextoObservaciones = "Observaciones: " + valObservaciones;
            vMaximaLongitud = LibString.Len(vTextoObservaciones);

            for (int vCantidadDeLineas = 0; vCantidadDeLineas < 3; vCantidadDeLineas++) {
                vTextoAImprimir = LibString.SubString(vTextoObservaciones, vPosicion, vCantidadCaracteres);
                vPosicion += vCantidadCaracteres;
                vResult = PFTfiscal(vTextoAImprimir);
                vEstado &= CheckRequest(vResult, ref vMensaje);
            }
            return vEstado;
        }

        private string ImprimeCamposDefinibles(XElement vData) {
            string vResult = "";

            List<XElement> vRecord = vData.Descendants("CAMPODEFINIBLE").ToList();
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
            string vResultado = "";
            string vCodigo;
            string vDescripcionExtendida;
            string vCantidad;
            string vMonto;
            string vDireccionFiscal = "";
            string vPrcDescuento;
            string vPorcentajeAlicuota = "";
            string vSerial;
            string vRollo;
            string vMensaje = "";
            bool vImprimeDireccionALFinalDeLaFactura = false;
            eStatusImpresorasFiscales PrintStatus;
            eTipoDeAlicuota vAlicuotaEspecial = (eTipoDeAlicuota)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "AplicacionAlicuotaEspecial"));

            try {
                vDireccionFiscal = LibXml.GetPropertyString(valDocumentoFiscal, "DireccionCliente");
                vImprimeDireccionALFinalDeLaFactura = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "ImprimeDireccionAlFinalDelComprobanteFiscal");
                if (!vImprimeDireccionALFinalDeLaFactura) {
                    vEstatus = ImprimeDireccionFiscal(vDireccionFiscal);
                }

                List<XElement> vRecord = valDocumentoFiscal.Descendants("GpResultDetailRenglonFactura").ToList();
                foreach (XElement vXElement in vRecord) {
                    PrintStatus = EstadoDelPapel(false);
                    if (!PrintStatus.Equals(eStatusImpresorasFiscales.eSinPapel)) {
                        vCodigo = LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement, "Articulo"), 0, 12);
                        vCodigo = clsImpresoraFiscalUtil.RemoverCaracteresInvalidos(vCodigo);						
                        vDescripcionExtendida = LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement, "Descripcion"), 0, 150);
                        vDescripcionExtendida = clsImpresoraFiscalUtil.RemoverCaracteresInvalidos(vDescripcionExtendida);
                        vCantidad = LibXml.GetElementValueOrEmpty(vXElement, "Cantidad");
                        vCantidad = DarFormatoNumericoParaImpresion(vCantidad, true);
                        vMonto = LibXml.GetElementValueOrEmpty(vXElement, "PrecioSinIVA");
                        vMonto = DarFormatoNumericoParaImpresion(vMonto, false);
                        vPorcentajeAlicuota = LibXml.GetElementValueOrEmpty(vXElement, "PorcentajeAlicuota");
                        vPorcentajeAlicuota = DarFormatoAAlicuotaIva(vPorcentajeAlicuota);
                        vPrcDescuento = (LibXml.GetElementValueOrEmpty(vXElement, "PorcentajeDescuento1"));
                        vPrcDescuento = DarFormatoNumericoParaDescuento(vPrcDescuento);
                        vSerial = LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement, "Serial"), 20);
                        vRollo = LibText.SubString(LibXml.GetElementValueOrEmpty(vXElement, "Rollo"), 20);

                        if (LibString.Len(vSerial) > 0) {
                            vResultado = PFTfiscal(vSerial);
                            vEstatus &= CheckRequest(vSerial, ref vMensaje);
                        }

                        if (LibString.Len(vRollo) > 0) {
                            vResultado = PFTfiscal(vRollo);
                            vEstatus &= CheckRequest(vRollo, ref vMensaje);
                        }
                        vResultado = PFrenglon(vDescripcionExtendida, vCantidad, vMonto, vPorcentajeAlicuota);
                        vEstatus &= CheckRequest(vRollo, ref vMensaje);
                        if (vEstatus) {
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
            if (LibText.IndexOf(valNumero, ',') > 0) {
                vResult = LibText.Replace(valNumero, ",", ".");
            } else {
                vResult = valNumero;
            }
            return vResult;
        }

        private string DarFormatoAAlicuotaIva(string valNumero) {
            string vValorFinal = "";
            string vParteEntera = "";
            string vParteDecimal = "";
            int vTokenPosition = 0;

            valNumero = SetDecimalSeparator(valNumero);
            vTokenPosition = LibText.InStr(valNumero, ",");
            if (vTokenPosition > 0) {
                vParteEntera = LibText.Left(valNumero, vTokenPosition);
                vParteEntera = LibText.FillWithCharToLeft(vParteEntera, "0", 2);
                vParteDecimal = LibText.Right(valNumero, LibText.Len(valNumero) - vTokenPosition - 1);
                vParteDecimal = LibText.FillWithCharToRight(vParteDecimal, "0", 2);
                vValorFinal = vParteEntera +  vParteDecimal;
            } else {
                vValorFinal = valNumero + "00";
            }
            return vValorFinal;
        }

        private string DarFormatoNumericoParaDescuento(string valNumero) {
            string vResult = "";
            decimal vDescuento = 0;

            vDescuento = LibConvert.ToDec(valNumero,3);
            vDescuento = LibMath.Abs(100 - vDescuento);
            vDescuento = vDescuento * 100000;
            valNumero = LibConvert.ToStr(vDescuento);

            if (LibText.IndexOf(valNumero, '.') > 0) {
                vResult = LibText.Replace(valNumero, ".", "");
            } else if (LibText.IndexOf(valNumero, ',') > 0) {
                vResult = LibText.Replace(valNumero, ",", "");
            }
            return vResult;
        }

        private string DarFormatoNumericoParaImpresion(string valNumero, bool valEsCantidad) {
            string vResult = "";
            int vTokenPosition = 0;
            string vParteEntera = "";
            string vParteDecimal = "";

            vResult = SetDecimalSeparator(valNumero);
            vTokenPosition = LibText.IndexOf(vResult, ".");
            if (vTokenPosition > 0) {
                vParteEntera = LibText.Left(vResult, vTokenPosition + 1);
                vParteDecimal = LibText.Right(vResult, LibText.Len(vResult) - vTokenPosition - 1);
                if (valEsCantidad) {
                    vParteDecimal = LibText.FillWithCharToRight(vParteDecimal, "0", 3);
                } else {
                    vParteDecimal = LibText.FillWithCharToRight(vParteDecimal, "0", 2);
                }                
                vResult = vParteEntera+"." + vParteDecimal;
            } else {
                if (valEsCantidad) {
                    vResult = vResult +"."+ "000";
                } else {
                    vResult = vResult +"."+ "00";
                }
            }
            return vResult;
        }

        private string DarFormatoNumericoParaLosPagos(string valNumero) {
            string vResult = "";

            if (LibText.IndexOf(valNumero, '.') > 0) {
                vResult = LibText.Replace(valNumero, ".", "");
            } else if (LibText.IndexOf(valNumero, ',') > 0) {
                vResult = LibText.Replace(valNumero, ",", "");
            }
            return vResult;
        }                

        private bool EnviarPagos(XElement valMedioDePago) {
            string vMedioDePago = "";
            string vMontoCancelado = "";
            string vMensaje = "";
            bool vEstado = false;
            string vResult = "";

            try {
                List<XElement> vNodos = valMedioDePago.Descendants("GpResultDetailRenglonCobro").ToList();
                if (vNodos.Count > 0) {
                    foreach (XElement vXElement in vNodos) {
                        vMedioDePago = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "CodigoFormaDelCobro"));
                        vMontoCancelado = LibText.CleanSpacesToBothSides(LibXml.GetElementValueOrEmpty(vXElement, "Monto"));
                        vMedioDePago = FormaDeCobro(vMedioDePago);
                        vMontoCancelado = DarFormatoNumericoParaLosPagos(vMontoCancelado);
                        vResult  =  PFPago(vMedioDePago,vMontoCancelado);
                        vEstado &= CheckRequest(vResult, ref vMensaje);
                    }
                }
                return true;
            } catch (GalacException vEx) {
                throw new LibGalac.Aos.Catching.GalacException(vEx.Message, eExceptionManagementType.Controlled);
            }
        }

        private string FormaDeCobro(string vFormaDeCobro) {
            string vResultado = "";

            switch (vFormaDeCobro) {
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

        private int ConvierteBinarioADecimal(string valBinario) {
            int vResult = 0;
            int vPotencia = LibString.Len(valBinario) - 1;

            double vBase = 0;

            for (int posicion = 0; posicion < LibString.Len(valBinario); posicion++) {
                vBase = vBase + LibConvert.ToDouble(LibString.SubString(valBinario, posicion, 1)) * Math.Pow(2, vPotencia);
                vPotencia--;
            }
            vResult = LibConvert.ToInt(vBase);
            return vResult;
        }

        private bool EstadoFiscal(string valEstado,ref string refMensaje) {
            bool vResult = false;
            int EstadoEnBaseDecimal = ConvierteBinarioADecimal(valEstado);

            if (EstadoEnBaseDecimal >= 8192) { // bit 13
                EstadoEnBaseDecimal >>= 1; //EstadoEnBaseDecimal - 8192;
                refMensaje = refMensaje + " Documento no fiscal abierto";
            }

            if (EstadoEnBaseDecimal >= 4096) { // bit 12
                EstadoEnBaseDecimal >>= 1; //EstadoEnBaseDecimal - 4096;
                refMensaje = refMensaje + " Es necesario realizar un Cierre Z /" +
                "Se ha Excedido el maximo numero de items para la factura debe cerrarse";
            }

            if (EstadoEnBaseDecimal >= 2048) { // bit 11
                EstadoEnBaseDecimal >>= 2; //EstadoEnBaseDecimal - 3072;
                refMensaje = refMensaje + " Factura Fiscal abierta";
            }

            if (EstadoEnBaseDecimal >= 256) { // bit 8
                EstadoEnBaseDecimal >>= 1; // EstadoEnBaseDecimal - 256;
                refMensaje = refMensaje + " Memoria Fiscal Casi Llena";                
            }

            if (EstadoEnBaseDecimal >= 128) { // bit 7
                EstadoEnBaseDecimal >>= 1; //EstadoEnBaseDecimal - 128;
                refMensaje = refMensaje + " Memoria Fiscal Llena";                
            }

            if (EstadoEnBaseDecimal >= 64) { // bit 6
                EstadoEnBaseDecimal = EstadoEnBaseDecimal - 64;
                refMensaje = refMensaje + " Desbordamiento de Totales";                
            }

            if (EstadoEnBaseDecimal >= 32) { // bit 5
                EstadoEnBaseDecimal >>= 1; //EstadoEnBaseDecimal - 32;
                refMensaje = refMensaje + " Comando Invalido para el proceso";                
            }

            if (EstadoEnBaseDecimal >= 16) { // bit 4
                EstadoEnBaseDecimal >>= 1;  //EstadoEnBaseDecimal - 16;
                refMensaje = refMensaje + " Campo de datos invalido";                
            }

            if (EstadoEnBaseDecimal >= 8) { // bit 3
                EstadoEnBaseDecimal >>= 2; //EstadoEnBaseDecimal - 10;
                refMensaje = refMensaje + " Comando no reconocido";                
            }

            if (EstadoEnBaseDecimal >= 2) { // bit 1
                EstadoEnBaseDecimal = EstadoEnBaseDecimal - 2;
                refMensaje = refMensaje + " Error en memoria de trabajo";                
            }

            if (EstadoEnBaseDecimal >= 1) { // bit 0
                EstadoEnBaseDecimal >>= 1; //EstadoEnBaseDecimal - 1;
                refMensaje = refMensaje + " Error en memoria de trabajo";                
            }            
            return vResult;
        }

        private bool EstadoImpresora(string valEstado, ref string refMensaje) {
            bool vResult = false;
            int EstadoEnBaseDecimal = ConvierteBinarioADecimal(valEstado);

            if (EstadoEnBaseDecimal >= 16384) { // bit 14
                EstadoEnBaseDecimal >>= 11; 
                refMensaje = refMensaje + " Impresora sin papel";
            }

            if (EstadoEnBaseDecimal >= 8) { // bit 3
                EstadoEnBaseDecimal >>= 1; 
                refMensaje = refMensaje + " Impresora Fuera de linea";
            }

            if (EstadoEnBaseDecimal >= 4) { // bit 2
                EstadoEnBaseDecimal >>= 1; 
                refMensaje = refMensaje +  " Error en Impresora";
            }                    
            return vResult;
        }

        private string LeerRepuestaImpFiscal(int vItemCampo) {
            string vResult = "";           
            int position = 0;
            int lastposition = 0;            
            List<string> vField = new List<string>();
            string vTrama = "";

            vTrama = PFultimo();

            do {
                position = LibString.IndexOf(vTrama, ',', lastposition);
                if (position > 0) {
                    vField.Add(LibString.SubString(vTrama, lastposition, position - lastposition));
                } else {
                    vField.Add(LibString.SubString(vTrama, lastposition));
                }
                lastposition = position + 1;
            }
            while (position > 0);
            vResult = vField[vItemCampo];
            return vResult;
        }

        private bool CheckRequest(string valSendRequest, ref string vMensaje) {
            bool vResult=true;

            switch (valSendRequest) {
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
                    vMensaje = "Tiempo de Repuesta Excedido";
                    break;
            }
            return vResult;
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
