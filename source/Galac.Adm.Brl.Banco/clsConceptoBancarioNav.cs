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
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Brl.Banco {
    public partial class clsConceptoBancarioNav : LibBaseNav<IList<ConceptoBancario>, IList<ConceptoBancario>>, IConceptoBancarioPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsConceptoBancarioNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<ConceptoBancario>, IList<ConceptoBancario>> GetDataInstance() {
            return new Galac.Adm.Dal.Banco.clsConceptoBancarioDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Banco.clsConceptoBancarioDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Banco.clsConceptoBancarioDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_ConceptoBancarioSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<ConceptoBancario>, IList<ConceptoBancario>> instanciaDal = new Galac.Adm.Dal.Banco.clsConceptoBancarioDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_ConceptoBancarioGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            switch (valModule) {
                case "Concepto Bancario":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }
        #endregion //Metodos Generados
        /* Codigo de Ejemplo

        bool IConceptoBancarioPdn.InsertarRegistroPorDefecto() {
            ILibDataComponent<IList<ConceptoBancario>, IList<ConceptoBancario>> instanciaDal = new clsConceptoBancarioDat();
            IList<ConceptoBancario> vLista = new List<ConceptoBancario>();
            ConceptoBancario vCurrentRecord = new ConceptoBancario();
            vCurrentRecord.Codigo = "";
            vCurrentRecord.Descripcion = "";
            vCurrentRecord.TipoAsEnum = eIngresoEgreso.Ingreso;
            vCurrentRecord.NombreOperador = "";
            vCurrentRecord.FechaUltimaModificacion = LibDate.Today();
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<ConceptoBancario> ParseToListEntity(XElement valXmlEntity) {
            List<ConceptoBancario> vResult = new List<ConceptoBancario>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                ConceptoBancario vRecord = new ConceptoBancario();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Codigo"), null))) {
                    vRecord.Codigo = vItem.Element("Codigo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Descripcion"), null))) {
                    vRecord.Descripcion = vItem.Element("Descripcion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Tipo"), null))) {
                    vRecord.Tipo = vItem.Element("Tipo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NombreOperador"), null))) {
                    vRecord.NombreOperador = vItem.Element("NombreOperador").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaUltimaModificacion"), null))) {
                    vRecord.FechaUltimaModificacion = LibConvert.ToDate(vItem.Element("FechaUltimaModificacion"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */

        #region Metodos agregados
        private List<ConceptoBancario> ParseToListEntity(XElement valXmlEntity) {
            List<ConceptoBancario> vResult = new List<ConceptoBancario>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                ConceptoBancario vRecord = new ConceptoBancario();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Codigo"), null))) {
                    vRecord.Codigo = vItem.Element("Codigo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Descripcion"), null))) {
                    vRecord.Descripcion = vItem.Element("Descripcion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Tipo"), null))) {
                    vRecord.Tipo = vItem.Element("Tipo").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }

        object IConceptoBancarioPdn.ConsultaCampoConceptoBancario(string valCampo, eConceptoBancarioPorDefecto valEnumConcepto) {
            LibGpParams vParams = new LibGpParams();
            XmlDocument XmlProperties = new XmlDocument();
            Galac.Adm.Dal.Banco.clsConceptoBancarioDat insDat = new Dal.Banco.clsConceptoBancarioDat();
            vParams.AddInString("SQLWhere", "Adm.Gv_ConceptoBancario_B1.Descripcion = '" + LibEnumHelper.GetDescription(valEnumConcepto) + "'", 200);
            ((ILibDataFKSearch)insDat).ConnectFk(ref XmlProperties, eProcessMessageType.SpName, "Adm.Gp_ConceptoBancarioSCH", vParams.Get());
            if (XmlProperties.OuterXml == string.Empty)
                return string.Empty;
            ConceptoBancario cb = ParseToListEntity(XElement.Parse(XmlProperties.OuterXml))[0];

            return cb.GetType().GetProperty(valCampo).GetValue(cb,null);
        }
		
        private XElement ConceptosBancariosPorDefecto() {
                XElement vXElement = new XElement("GpData",
                new XElement("GpResult",
                new XElement("DEBITO_BANCARIO_AUTOMATICO", "60335"),
                new XElement("REVERSO_AUTOMATICO_COBRANZA", "60334"),
                new XElement("REV_AUTOMATICO_ANT_COBRADO", "60336"),
                new XElement("REV_AUTOMATICO_ANT_PAGADO", "60337"),
                new XElement("ANTICIPO_COBRADO", "60338"),
                new XElement("ANTICIPO_PAGADO", "60339"),
                new XElement("COBRO_DIRECTO_DE_FACT", "60340"),
                new XElement("REVERSO_AUTOMATICO_PAGO", "60341"),
                new XElement("TRANSERENCIA_INGRESO", "60342"),
                new XElement("TRANSERENCIA_EGRESO", "60343"),
                new XElement("DETRACCION_INGRESO", "60344"),
                new XElement("DETRACCION_EGRESO", "60345"),
                new XElement("REV_AUTOMATICO_SOLI_PAGADO", "G0001")));
            return vXElement;
        }
		
        
       XElement IConceptoBancarioPdn.LisConceptosBancariosPorDefecto() {
            return ConceptosBancariosPorDefecto();
        }
		#endregion 
    } //End of class clsConceptoBancarioNav

} //End of namespace Galac.Adm.Brl.Banco

