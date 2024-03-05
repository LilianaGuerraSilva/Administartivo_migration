using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Base.Dal;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Brl.GestionCompras {
    public partial class clsProveedorNav : LibBaseNav<IList<Proveedor>, IList<Proveedor>>, IProveedorPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsProveedorNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<Proveedor>, IList<Proveedor>> GetDataInstance() {
            return new Galac.Adm.Dal.GestionCompras.clsProveedorDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.GestionCompras.clsProveedorDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.GestionCompras.clsProveedorDat();
            if (valCallingModule == "ProveedorForPago")
                return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_ProveedorForPagoSCH", valXmlParamsExpression);
            else
                return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_ProveedorSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<Proveedor>, IList<Proveedor>> instanciaDal = new Galac.Adm.Dal.GestionCompras.clsProveedorDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_ProveedorGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Proveedor":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Tabla Retención":
                    vPdnModule = new Galac.Comun.Brl.TablasLey.clsTablaRetencionNav();
                    vResult = vPdnModule.GetDataForList("Proveedor", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Tipo Proveedor":
                    vPdnModule = new Galac.Saw.Brl.Tablas.clsTipoProveedorNav();
                    vResult = vPdnModule.GetDataForList("Proveedor", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Cuenta":
                    vPdnModule = new Galac.Contab.Brl.WinCont.clsCuentaNav();
                    vResult = vPdnModule.GetDataForList("Proveedor", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "ProveedorForPago":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "País Sunat":
                    vPdnModule = new Galac.Comun.Brl.TablasGen.clsPaisSunatNav();
                    vResult = vPdnModule.GetDataForList("Proveedor", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Convenios Sunat":
                    vPdnModule = new Galac.Comun.Brl.TablasGen.clsConveniosSunatNav();
                    vResult = vPdnModule.GetDataForList("Proveedor", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "ProveedorInforme":
                    vResult = ((ILibPdn)this).GetDataForList("Proveedor", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }
        #endregion //Metodos Generados

        string IProveedorPdn.ValidaRifWeb(string valRif) {
            LibGalac.Aos.Ccl.IdFiscal.ILibIdFiscalPdn insBrlIdFiscal = new LibGalac.Aos.Brl.IdFiscal.LibIdFiscalNav();
            return insBrlIdFiscal.WebVerification(valRif);
        }

        protected override LibResponse InsertRecord(IList<Proveedor> refRecord) {
            LibResponse vResult = base.InsertRecord(refRecord);
            if (vResult.Success) {
                if (EjecutaProcesosPostGrabacion(eAccionSR.Insertar, refRecord)) {
                }
            }
            return vResult;
        }
        protected override LibResponse UpdateRecord(IList<Proveedor> refRecord) {
            LibResponse vResult = base.UpdateRecord(refRecord);
            if (vResult.Success) {
                if (EjecutaProcesosPostGrabacion(eAccionSR.Modificar, refRecord)) {
                }
            }
            return vResult;
        }

        protected override LibResponse DeleteRecord(IList<Proveedor> refRecord) {
            LibResponse vResult = base.DeleteRecord(refRecord);
            if (vResult.Success) {
                if (EjecutaProcesosPostGrabacion(eAccionSR.Eliminar, refRecord)) {                     
                }                
            }
            return vResult;
        }

        private bool EjecutaProcesosPostGrabacion(eAccionSR valAction,IList<Proveedor> refRecord) {
            Proveedor vCurrentRecord = refRecord[0];
            bool vResult = true;
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Compania","AccesoCaracteristicaDeContabilidadActiva") &&
                LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Compania", "UsaAuxiliares")){
                    Galac.Contab.Ccl.WinCont.IAuxiliarPdn vAuxiliarPdn = new Galac.Contab.Brl.WinCont.clsAuxiliarNav();
                XElement vData = new XElement("GpData",
                    new XElement("GpResult",
                        new XElement("ConsecutivoPeriodo",LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros","ConsecutivoPeriodo")),
                        new XElement("TipodeAuxiliar",  LibConvert.EnumToDbValue((int)Galac.Contab.Ccl.WinCont.eGrupoAuxiliar.Proveedores)),
                        new XElement("Codigo", vCurrentRecord.CodigoProveedor),
                        new XElement("Nombre", vCurrentRecord.NombreProveedor),
                        new XElement("NoRif", vCurrentRecord.NumeroRIF),
                        new XElement("NoNit", vCurrentRecord.NumeroNIT),
                        new XElement("numeroPasaporte", vCurrentRecord.NumeroRUC)));
                vResult = vAuxiliarPdn.ExecuteAccionSobreRecordFromExternalModule(valAction.GetDescription(), vData.ToString());
            }
            return vResult;
        }

        public Proveedor GetProveedor(int ConsecutivoCompania, string CodigoProveedor) {
            RegisterClient();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", ConsecutivoCompania);
            vParams.AddInString("CodigoProveedor",CodigoProveedor,10);
            return _Db.GetData(eProcessMessageType.SpName,"ProveedorGET",vParams.Get()).FirstOrDefault();
        }
    } //End of class clsProveedorNav

} //End of namespace Galac.Adm.Brl.GestionCompras

