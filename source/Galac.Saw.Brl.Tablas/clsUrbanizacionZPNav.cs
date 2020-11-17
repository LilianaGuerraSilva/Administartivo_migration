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
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Brl.Tablas {
    public partial class clsUrbanizacionZPNav: LibBaseNav<IList<UrbanizacionZP>, IList<UrbanizacionZP>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsUrbanizacionZPNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<UrbanizacionZP>, IList<UrbanizacionZP>> GetDataInstance() {
            return new Galac.Saw.Dal.Tablas.clsUrbanizacionZPDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Tablas.clsUrbanizacionZPDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Tablas.clsUrbanizacionZPDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Saw.Gp_UrbanizacionZPSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<UrbanizacionZP>, IList<UrbanizacionZP>> instanciaDal = new Galac.Saw.Dal.Tablas.clsUrbanizacionZPDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Saw.Gp_UrbanizacionZPGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            switch (valModule) {
                case "Urbanización - Zona Postal":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsUrbanizacionZPNav

} //End of namespace Galac.Saw.Brl.Tablas

