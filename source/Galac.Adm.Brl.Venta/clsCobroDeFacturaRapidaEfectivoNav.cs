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
    public partial class clsCobroDeFacturaRapidaEfectivoNav: LibBaseNav<IList<CobroDeFacturaRapidaEfectivo>, IList<CobroDeFacturaRapidaEfectivo>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCobroDeFacturaRapidaEfectivoNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<CobroDeFacturaRapidaEfectivo>, IList<CobroDeFacturaRapidaEfectivo>> GetDataInstance() {
            return new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaEfectivoDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaEfectivoDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        //bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
        //    ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaEfectivoDat();
        //    return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_CobroDeFacturaRapidaEfectivoSCH", valXmlParamsExpression);
        //}

        //System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
        //    ILibDataComponent<IList<CobroDeFacturaRapidaEfectivo>, IList<CobroDeFacturaRapidaEfectivo>> instanciaDal = new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaEfectivoDat();
        //    return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_CobroDeFacturaRapidaEfectivoGetFk", valParameters);
        //}
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Cobro Efectivo de Factura":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Forma Del Cobro":
                    vPdnModule = new Galac.Saw.Brl.Tablas.clsFormaDelCobroNav();
                    vResult = vPdnModule.GetDataForList("Cobro Efectivo de Factura", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool ICobroDeFacturaRapidaEfectivoPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<CobroDeFacturaRapidaEfectivo>, IList<CobroDeFacturaRapidaEfectivo>> instanciaDal = new clsCobroDeFacturaRapidaEfectivoDat();
            IList<CobroDeFacturaRapidaEfectivo> vLista = new List<CobroDeFacturaRapidaEfectivo>();
            CobroDeFacturaRapidaEfectivo vCurrentRecord = new Galac.Adm.Dal.VentaCobroDeFacturaRapidaEfectivo();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.NumeroFactura = "";
            vCurrentRecord.CodigoFormaDelCobro = "";
            vCurrentRecord.MontoEfectivo = 0;
            vCurrentRecord.TotalACobrar = 0;
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<CobroDeFacturaRapidaEfectivo> ParseToListEntity(XElement valXmlEntity) {
            List<CobroDeFacturaRapidaEfectivo> vResult = new List<CobroDeFacturaRapidaEfectivo>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                CobroDeFacturaRapidaEfectivo vRecord = new CobroDeFacturaRapidaEfectivo();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroFactura"), null))) {
                    vRecord.NumeroFactura = vItem.Element("NumeroFactura").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoFormaDelCobro"), null))) {
                    vRecord.CodigoFormaDelCobro = vItem.Element("CodigoFormaDelCobro").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoEfectivo"), null))) {
                    vRecord.MontoEfectivo = LibConvert.ToDec(vItem.Element("MontoEfectivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TotalACobrar"), null))) {
                    vRecord.TotalACobrar = LibConvert.ToDec(vItem.Element("TotalACobrar"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo


    } //End of class clsCobroDeFacturaRapidaEfectivoNav

} //End of namespace Galac.Adm.Brl.Venta

