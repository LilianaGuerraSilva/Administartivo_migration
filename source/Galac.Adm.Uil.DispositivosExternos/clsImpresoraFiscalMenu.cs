using System;
using LibGalac.Aos.Base;
using Galac.Adm.Ccl.DispositivosExternos;
using System.Xml.Linq;
using Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal;
using LibGalac.Aos.UI.Mvvm.Messaging;

namespace Galac.Adm.Uil.DispositivosExternos.ViewModel {

    public class clsImpresoraFiscalMenu :ILibMenu {


        public void Ejecuta(eAccionSR valAction,int handler) {
            throw new NotImplementedException();
        }

        public bool ComprobarEstado() {
            throw new NotImplementedException();
        }

        public eStatusImpresorasFiscales EstadoDelPapel(bool valAbrirConexion) {
            throw new NotImplementedException();
        }

        public bool ImprimirFacturaFiscal(string valDatosImpresoraFiscal,string valDocumentoFiscal) {
            try {
                bool vResult = false;
                valDatosImpresoraFiscal = LimpiarXmlAntesDeParsear(valDatosImpresoraFiscal);
                valDocumentoFiscal = LimpiarXmlAntesDeParsear(valDocumentoFiscal);
                XElement xmlImpresoraFiscal = LibXml.ToXElement(valDatosImpresoraFiscal);
                XElement xmlDocumentoFiscal = LibXml.ToXElement(valDocumentoFiscal);                                
                ImpresoraFiscalViewModel insImpresoraFiscalViewModel = new ImpresoraFiscalViewModel(xmlImpresoraFiscal,xmlDocumentoFiscal,eTipoDocumentoFiscal.FacturaFiscal);
                LibMessages.EditViewModel.ShowEditor(insImpresoraFiscalViewModel,true);                
                vResult = insImpresoraFiscalViewModel.SeImprimioDocumento;                
                insImpresoraFiscalViewModel = null;
                return vResult;
            } catch (Exception vEx) {
                throw vEx;
            }            
        }

        public bool ImprimirNotaCredito(string valDatosImpresoraFiscal,string valDocumentoFiscal) {
            try {
                bool vResult = false;
                valDatosImpresoraFiscal = LimpiarXmlAntesDeParsear(valDatosImpresoraFiscal);
                valDocumentoFiscal = LimpiarXmlAntesDeParsear(valDocumentoFiscal);
                XElement xmlImpresoraFiscal = LibXml.ToXElement(valDatosImpresoraFiscal);
                XElement xmlDocumentoFiscal = LibXml.ToXElement(valDocumentoFiscal);                                
                ImpresoraFiscalViewModel insImpresoraFiscalViewModel = new ImpresoraFiscalViewModel(xmlImpresoraFiscal,xmlDocumentoFiscal,eTipoDocumentoFiscal.NotadeCredito);
                LibMessages.EditViewModel.ShowEditor(insImpresoraFiscalViewModel,true);
                vResult = insImpresoraFiscalViewModel.SeImprimioDocumento;                
                insImpresoraFiscalViewModel = null;
                return vResult;
            } catch(Exception vEx) {                
                throw vEx;
            }
        }

        public bool RealizarReporteX(bool valAbrirConexion,string valDatosImpresoraFiscal) {
            try {
                bool vResult = false;
                valDatosImpresoraFiscal = LimpiarXmlAntesDeParsear(valDatosImpresoraFiscal);
                XElement xmlImpresoraFiscal = LibXml.ToXElement(valDatosImpresoraFiscal);
                ImpresoraFiscalViewModel insImpresoraFiscalViewModel = new ImpresoraFiscalViewModel(xmlImpresoraFiscal,xmlImpresoraFiscal,eTipoDocumentoFiscal.ReporteX);
                LibMessages.EditViewModel.ShowEditor(insImpresoraFiscalViewModel,true);
                vResult = insImpresoraFiscalViewModel.SeImprimioDocumento;               
                insImpresoraFiscalViewModel = null;
                return vResult;
            } catch(Exception) {
                throw;
            }
        }

        public bool RealizarReporteZ(bool valAbrirConexion,string valDatosImpresoraFiscal) {
            try {
                bool vResult = false;
                valDatosImpresoraFiscal = LimpiarXmlAntesDeParsear(valDatosImpresoraFiscal);
                XElement xmlImpresoraFiscal = LibXml.ToXElement(valDatosImpresoraFiscal);
                ImpresoraFiscalViewModel insImpresoraFiscalViewModel = new ImpresoraFiscalViewModel(xmlImpresoraFiscal,xmlImpresoraFiscal,eTipoDocumentoFiscal.ReporteZ);
                LibMessages.EditViewModel.ShowEditor(insImpresoraFiscalViewModel,true);
                vResult = insImpresoraFiscalViewModel.SeImprimioDocumento;              
                insImpresoraFiscalViewModel = null;
                return vResult;
            } catch(Exception) {
                throw;
            }
        }

        public bool AnularDocumentoFiscal(string valDatosImpresoraFiscal,bool valAbrirConexion) {
            bool vResult = false;
            valDatosImpresoraFiscal = LimpiarXmlAntesDeParsear(valDatosImpresoraFiscal);
            XElement xmlImpresoraFiscal = LibXml.ToXElement(valDatosImpresoraFiscal);
            clsImpresoraFiscalCreator vCreatorMaquinaFiscal = new clsImpresoraFiscalCreator();
            IImpresoraFiscalPdn insIMaquinaFiscal = vCreatorMaquinaFiscal.Crear(xmlImpresoraFiscal);
            vResult = insIMaquinaFiscal.CancelarDocumentoFiscalEnImpresion(valAbrirConexion);
            insIMaquinaFiscal = null;
            return vResult;
        }

        public string ObtenerFechaYHora(string valDatosImpresoraFiscal) {
            try {
                string vFecha = "";
                valDatosImpresoraFiscal = LimpiarXmlAntesDeParsear(valDatosImpresoraFiscal);
                XElement xmlImpresoraFiscal = LibXml.ToXElement(valDatosImpresoraFiscal);
                clsImpresoraFiscalCreator vCreatorMaquinaFiscal = new clsImpresoraFiscalCreator();
                IImpresoraFiscalPdn insIMaquinaFiscal = vCreatorMaquinaFiscal.Crear(xmlImpresoraFiscal);
                vFecha = insIMaquinaFiscal.ObtenerFechaYHora();
                insIMaquinaFiscal = null;
                return vFecha;
            } catch(Exception) {
                throw;
            }
        }

        public string ObtenerSerial(bool valAbrirConexion,string valDatosImpresoraFiscal) {
            try {
                string vSerial = "";                
                valDatosImpresoraFiscal = LimpiarXmlAntesDeParsear(valDatosImpresoraFiscal);
                XElement xmlImpresoraFiscal = LibXml.ToXElement(valDatosImpresoraFiscal);
                clsImpresoraFiscalCreator vCreatorMaquinaFiscal = new clsImpresoraFiscalCreator();
                IImpresoraFiscalPdn insIMaquinaFiscal = vCreatorMaquinaFiscal.Crear(xmlImpresoraFiscal);
                vSerial= insIMaquinaFiscal.ObtenerSerial(true);
                insIMaquinaFiscal = null;
                return vSerial;
            } catch(Exception) {                
                throw;
            }            
        }

        public string ObtenerUltimoNumeroFactura(bool valAbrirConexion,string valDatosImpresoraFiscal) {
            try {
                string vUltimaFactura = "";
                valDatosImpresoraFiscal = LimpiarXmlAntesDeParsear(valDatosImpresoraFiscal);
                XElement xmlImpresoraFiscal = LibXml.ToXElement(valDatosImpresoraFiscal);
                clsImpresoraFiscalCreator vCreatorMaquinaFiscal = new clsImpresoraFiscalCreator();
                IImpresoraFiscalPdn insIMaquinaFiscal = vCreatorMaquinaFiscal.Crear(xmlImpresoraFiscal);
                vUltimaFactura= insIMaquinaFiscal.ObtenerUltimoNumeroFactura(true);
                insIMaquinaFiscal = null;
                return vUltimaFactura;
            } catch(Exception) {                
                throw;
            }            
        }

        public string ObtenerUltimoNumeroNotaDeCredito(bool valAbrirConexion,string valDatosImpresoraFiscal) {
            try {
                string vUltimoNC = "";
                valDatosImpresoraFiscal = LimpiarXmlAntesDeParsear(valDatosImpresoraFiscal);
                XElement xmlImpresoraFiscal = LibXml.ToXElement(valDatosImpresoraFiscal);
                clsImpresoraFiscalCreator vCreatorMaquinaFiscal = new clsImpresoraFiscalCreator();
                IImpresoraFiscalPdn insIMaquinaFiscal = vCreatorMaquinaFiscal.Crear(xmlImpresoraFiscal);
                vUltimoNC = insIMaquinaFiscal.ObtenerUltimoNumeroNotaDeCredito(true);
                insIMaquinaFiscal = null;
                return vUltimoNC;
            } catch(Exception) {                
                throw;
            }
        }

        public string ObtenerUltimoNumeroReporteZ(bool valAbrirConexion,string valDatosImpresoraFiscal) {
            try {
                string vUltimoZ = "";
                valDatosImpresoraFiscal = LimpiarXmlAntesDeParsear(valDatosImpresoraFiscal);
                XElement xmlImpresoraFiscal = LibXml.ToXElement(valDatosImpresoraFiscal);
                clsImpresoraFiscalCreator vCreatorMaquinaFiscal = new clsImpresoraFiscalCreator();
                IImpresoraFiscalPdn insIMaquinaFiscal = vCreatorMaquinaFiscal.Crear(xmlImpresoraFiscal);
                vUltimoZ = insIMaquinaFiscal.ObtenerUltimoNumeroReporteZ(true);
                insIMaquinaFiscal = null;
                return vUltimoZ;
            } catch(Exception) {                
                throw;
            }            
        }       

        public bool ReimprimirDocumentoFiscal(string valDesde,string valHasta,string valTipo) {
            throw new NotImplementedException();
        }

        public bool ReimprimirDocumentoNoFiscal(string valDesde,string valHasta) {
            throw new NotImplementedException();
        }

        private string LimpiarXmlAntesDeParsear(string vfwImpresoraFiscal) {
            return LibImpresoraFiscalUtil.LimpiarXmlAntesDeParsear(vfwImpresoraFiscal);
        }

    } //End of class ImpresoraFiscalMngViewModel

} //End of namespace Galac.Adm.Uil.DispositivosExternos

