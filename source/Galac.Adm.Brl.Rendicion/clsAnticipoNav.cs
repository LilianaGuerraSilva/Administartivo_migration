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
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Brl.CajaChica {
    public partial class clsAnticipoNav: LibBaseNav<IList<Anticipo>, IList<Anticipo>>,IAnticipoPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsAnticipoNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<Anticipo>, IList<Anticipo>> GetDataInstance() {
            return new Galac.Adm.Dal.CajaChica.clsAnticipoDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.CajaChica.clsAnticipoDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.CajaChica.clsAnticipoDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_AnticipoSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<Anticipo>, IList<Anticipo>> instanciaDal = new Galac.Adm.Dal.CajaChica.clsAnticipoDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "dbo.Gp_AnticipoGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Anticipo":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Beneficiario":
                    vPdnModule = new Galac.Adm.Brl.Banco.clsBeneficiarioNav();
                    vResult = vPdnModule.GetDataForList("Anticipo", ref refXmlDocument, valXmlParamsExpression);
                    break;
                //case "Cliente":
                //    vPdnModule = new Galac.Saw.Brl.Cliente.clsClienteNav();
                //    vResult = vPdnModule.GetDataForList("Anticipo", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                //case "Proveedor":
                //    vPdnModule = new Galac.dbo.Brl.Proveedor.clsProveedorNav();
                //    vResult = vPdnModule.GetDataForList("Anticipo", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                //case "Moneda":
                //    vPdnModule = new Galac.dbo.Brl.Moneda.clsMonedaNav();
                //    vResult = vPdnModule.GetDataForList("Anticipo", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                //case "Cuenta Bancaria":
                //    vPdnModule = new Galac.dbo.Brl.CuentaBancaria.clsCuentaBancariaNav();
                //    vResult = vPdnModule.GetDataForList("Anticipo", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                //case "Concepto Bancario":
                //    vPdnModule = new Galac.dbo.Brl.ConceptoBancario.clsConceptoBancarioNav();
                //    vResult = vPdnModule.GetDataForList("Anticipo", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                //case "Cotizacion":
                //    vPdnModule = new Galac.dbo.Brl.Cotizacion.clsCotizacionNav();
                //    vResult = vPdnModule.GetDataForList("Anticipo", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                //case "Rendicion":
                //    vPdnModule = new Galac.Adm.Brl.CajaChica.clsRendicionNav();
                //    vResult = vPdnModule.GetDataForList("Anticipo", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                default: break;
            }
            return vResult;
        }
        #endregion //Metodos Generados
        #region MetodosCreados
        public bool actualizar(List<Anticipo> list)
        {
            ILibDataComponentWithSearch<IList<Anticipo>, IList<Anticipo>> instanciaDal = new Galac.Adm.Dal.CajaChica.clsAnticipoDat();
            return instanciaDal.Update(list).Success;
        } 
        #endregion



    } //End of class clsAnticipoNav

} //End of namespace Galac.Adm.Brl.CajaChica

