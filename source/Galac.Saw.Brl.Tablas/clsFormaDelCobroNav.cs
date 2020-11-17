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
    public partial class clsFormaDelCobroNav: LibBaseNav<IList<FormaDelCobro>, IList<FormaDelCobro>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsFormaDelCobroNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<FormaDelCobro>, IList<FormaDelCobro>> GetDataInstance() {
            return new Galac.Saw.Dal.Tablas.clsFormaDelCobroDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.SAW.Dal.Tablas.clsFormaDelCobroDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Tablas.clsFormaDelCobroDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "SAW.Gp_FormaDelCobroSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<FormaDelCobro>, IList<FormaDelCobro>> instanciaDal = new Galac.Saw.Dal.Tablas.clsFormaDelCobroDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "SAW.Gp_FormaDelCobroGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            switch (valModule) {
                case "Forma Del Cobro":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsFormaDelCobroNav

} //End of namespace Galac.Saw.Brl.Tablas

