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
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Brl.GestionCompras {
    public partial class clsTablaRetencionNav: LibBaseNav<IList<TablaRetencion>, IList<TablaRetencion>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsTablaRetencionNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<TablaRetencion>, IList<TablaRetencion>> GetDataInstance() {
            return new Galac.Adm.Dal.GestionCompras.clsTablaRetencionDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.GestionCompras.clsTablaRetencionDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.GestionCompras.clsTablaRetencionDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_TablaRetencionSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<TablaRetencion>, IList<TablaRetencion>> instanciaDal = new Galac.Adm.Dal.GestionCompras.clsTablaRetencionDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_TablaRetencionGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Tabla Retencion":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Moneda":
                    //vPdnModule = new Galac.Comun.Brl.TablasGen.clsMonedaNav();
                    //vResult = vPdnModule.GetDataForList("Tabla Retencion", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }
        #endregion //Metodos Generados
        /* Codigo de Ejemplo

        bool ITablaRetencionPdn.InsertarRegistroPorDefecto() {
            ILibDataComponent<IList<TablaRetencion>, IList<TablaRetencion>> instanciaDal = new clsTablaRetencionDat();
            IList<TablaRetencion> vLista = new List<TablaRetencion>();
            TablaRetencion vCurrentRecord = new TablaRetencion();
            vCurrentRecord.TipoDePersonaAsEnum = eTipodePersonaRetencion.PJ_Domiciliada;
            vCurrentRecord.Codigo = "";
            vCurrentRecord.CodigoSeniat = "";
            vCurrentRecord.TipoDePago = "";
            vCurrentRecord.Comentarios = "";
            vCurrentRecord.BaseImponible = 0;
            vCurrentRecord.Tarifa = 0;
            vCurrentRecord.ParaPagosMayoresDe = 0;
            vCurrentRecord.FechaAplicacion = LibDate.Today();
            vCurrentRecord.Sustraendo = 0;
            vCurrentRecord.AcumulaParaPJNDAsBool = false;
            vCurrentRecord.SecuencialDePlantilla = "";
            vCurrentRecord.CodigoMoneda = "";
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<TablaRetencion> ParseToListEntity(XElement valXmlEntity) {
            List<TablaRetencion> vResult = new List<TablaRetencion>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                TablaRetencion vRecord = new TablaRetencion();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDePersona"), null))) {
                    vRecord.TipoDePersona = vItem.Element("TipoDePersona").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Codigo"), null))) {
                    vRecord.Codigo = vItem.Element("Codigo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoSeniat"), null))) {
                    vRecord.CodigoSeniat = vItem.Element("CodigoSeniat").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDePago"), null))) {
                    vRecord.TipoDePago = vItem.Element("TipoDePago").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Comentarios"), null))) {
                    vRecord.Comentarios = vItem.Element("Comentarios").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("BaseImponible"), null))) {
                    vRecord.BaseImponible = LibConvert.ToDec(vItem.Element("BaseImponible"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Tarifa"), null))) {
                    vRecord.Tarifa = LibConvert.ToDec(vItem.Element("Tarifa"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ParaPagosMayoresDe"), null))) {
                    vRecord.ParaPagosMayoresDe = LibConvert.ToDec(vItem.Element("ParaPagosMayoresDe"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaAplicacion"), null))) {
                    vRecord.FechaAplicacion = LibConvert.ToDate(vItem.Element("FechaAplicacion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Sustraendo"), null))) {
                    vRecord.Sustraendo = LibConvert.ToDec(vItem.Element("Sustraendo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AcumulaParaPJND"), null))) {
                    vRecord.AcumulaParaPJND = vItem.Element("AcumulaParaPJND").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("SecuencialDePlantilla"), null))) {
                    vRecord.SecuencialDePlantilla = vItem.Element("SecuencialDePlantilla").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoMoneda"), null))) {
                    vRecord.CodigoMoneda = vItem.Element("CodigoMoneda").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */


    } //End of class clsTablaRetencionNav

} //End of namespace Galac.Adm.Brl.GestionCompras

