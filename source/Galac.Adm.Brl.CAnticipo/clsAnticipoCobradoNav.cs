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
using Galac.Adm.Ccl.CAnticipo;

namespace Galac.Adm.Brl.CAnticipo {
    public partial class clsAnticipoCobradoNav: LibBaseNav<IList<AnticipoCobrado>, IList<AnticipoCobrado>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsAnticipoCobradoNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<AnticipoCobrado>, IList<AnticipoCobrado>> GetDataInstance() {
            return new Galac.Adm.Dal.CAnticipo.clsAnticipoCobradoDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.CAnticipo.clsAnticipoCobradoDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.CAnticipo.clsAnticipoCobradoDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_AnticipoCobradoSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<AnticipoCobrado>, IList<AnticipoCobrado>> instanciaDal = new Galac.Adm.Dal.CAnticipo.clsAnticipoCobradoDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "dbo.Gp_AnticipoCobradoGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Anticipo Cobrado":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Anticipo":
                    vPdnModule = new clsAnticipoNav();
                    vResult = vPdnModule.GetDataForList("Anticipo Cobrado", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Moneda":
                    vPdnModule = new Galac.Comun.Brl.TablasGen.clsMonedaNav();
                    vResult = vPdnModule.GetDataForList("Anticipo Cobrado", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IAnticipoCobradoPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<AnticipoCobrado>, IList<AnticipoCobrado>> instanciaDal = new clsAnticipoCobradoDat();
            IList<AnticipoCobrado> vLista = new List<AnticipoCobrado>();
            AnticipoCobrado vCurrentRecord = new Galac.Adm.Dal.CAnticipoAnticipoCobrado();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.NumeroCobranza = "";
            vCurrentRecord.Secuencial = 0;
            vCurrentRecord.ConsecutivoAnticipoUsado = 0;
            vCurrentRecord.NumeroAnticipo = "";
            vCurrentRecord.MontoOriginal = 0;
            vCurrentRecord.MontoRestanteAlDia = 0;
            vCurrentRecord.SimboloMoneda = "";
            vCurrentRecord.CodigoMoneda = "";
            vCurrentRecord.MontoTotalDelAnticipo = 0;
            vCurrentRecord.MontoAplicado = 0;
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<AnticipoCobrado> ParseToListEntity(XElement valXmlEntity) {
            List<AnticipoCobrado> vResult = new List<AnticipoCobrado>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                AnticipoCobrado vRecord = new AnticipoCobrado();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroCobranza"), null))) {
                    vRecord.NumeroCobranza = vItem.Element("NumeroCobranza").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Secuencial"), null))) {
                    vRecord.Secuencial = LibConvert.ToInt(vItem.Element("Secuencial"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoAnticipoUsado"), null))) {
                    vRecord.ConsecutivoAnticipoUsado = LibConvert.ToInt(vItem.Element("ConsecutivoAnticipoUsado"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroAnticipo"), null))) {
                    vRecord.NumeroAnticipo = vItem.Element("NumeroAnticipo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoOriginal"), null))) {
                    vRecord.MontoOriginal = LibConvert.ToDec(vItem.Element("MontoOriginal"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoRestanteAlDia"), null))) {
                    vRecord.MontoRestanteAlDia = LibConvert.ToDec(vItem.Element("MontoRestanteAlDia"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("SimboloMoneda"), null))) {
                    vRecord.SimboloMoneda = vItem.Element("SimboloMoneda").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoMoneda"), null))) {
                    vRecord.CodigoMoneda = vItem.Element("CodigoMoneda").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoTotalDelAnticipo"), null))) {
                    vRecord.MontoTotalDelAnticipo = LibConvert.ToDec(vItem.Element("MontoTotalDelAnticipo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoAplicado"), null))) {
                    vRecord.MontoAplicado = LibConvert.ToDec(vItem.Element("MontoAplicado"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo


    } //End of class clsAnticipoCobradoNav

} //End of namespace Galac.Adm.Brl.CAnticipo

