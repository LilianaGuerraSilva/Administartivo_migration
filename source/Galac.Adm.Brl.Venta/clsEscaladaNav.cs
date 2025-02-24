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
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Brl.Venta {
    public partial class clsEscaladaNav: LibBaseNav<IList<Escalada>, IList<Escalada>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsEscaladaNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<Escalada>, IList<Escalada>> GetDataInstance() {
            return new Galac.Adm.Dal.Venta.clsEscaladaDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.dbo.Dal.Venta.clsEscaladaDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsEscaladaDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_EscaladaSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<Escalada>, IList<Escalada>> instanciaDal = new Galac.Adm.Dal.Venta.clsEscaladaDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "dbo.Gp_EscaladaGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            switch (valModule) {
                case "Escalada":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<Escalada> refData) {
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IEscaladaPdn.InsertDefaultRecord() {
            ILibDataComponent<IList<Escalada>, IList<Escalada>> instanciaDal = new clsEscaladaDat();
            IList<Escalada> vLista = new List<Escalada>();
            Escalada vCurrentRecord = new Galac.Dbo.Dal.VentaEscalada();
            vCurrentRecord.Id = "";
            vCurrentRecord.Escalada41 = 0;
            vCurrentRecord.Escalada32 = LibDate.Today();
            vCurrentRecord.Escalada73 = "";
            vCurrentRecord.Escalada24 = "";
            vCurrentRecord.Escalada85 = "";
            vCurrentRecord.Escalada100 = "";
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<Escalada> ParseToListEntity(XElement valXmlEntity) {
            List<Escalada> vResult = new List<Escalada>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                Escalada vRecord = new Escalada();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Id"), null))) {
                    vRecord.Id = vItem.Element("Id").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Escalada41"), null))) {
                    vRecord.Escalada41 = LibConvert.ToInt(vItem.Element("Escalada41"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Escalada32"), null))) {
                    vRecord.Escalada32 = LibConvert.ToDate(vItem.Element("Escalada32"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Escalada73"), null))) {
                    vRecord.Escalada73 = vItem.Element("Escalada73").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Escalada24"), null))) {
                    vRecord.Escalada24 = vItem.Element("Escalada24").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Escalada85"), null))) {
                    vRecord.Escalada85 = vItem.Element("Escalada85").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Escalada100"), null))) {
                    vRecord.Escalada100 = vItem.Element("Escalada100").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo


    } //End of class clsEscaladaNav

} //End of namespace Galac.Adm.Brl.Venta

