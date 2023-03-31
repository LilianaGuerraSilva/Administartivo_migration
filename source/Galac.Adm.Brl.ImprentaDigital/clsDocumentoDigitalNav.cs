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
using Galac.Adm.Ccl.ImprentaDigital;

namespace Galac.Adm.Brl.ImprentaDigital {
    public partial class clsDocumentoDigitalNav: LibBaseNav<IList<DocumentoDigital>, IList<DocumentoDigital>>, IDocumentoDigitalPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsDocumentoDigitalNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<DocumentoDigital>, IList<DocumentoDigital>> GetDataInstance() {
            return new Galac.Adm.Dal.ImprentaDigital.clsDocumentoDigitalDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.ImprentaDigital.clsDocumentoDigitalDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.ImprentaDigital.clsDocumentoDigitalDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_DocumentoDigitalSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<DocumentoDigital>, IList<DocumentoDigital>> instanciaDal = new Galac.Adm.Dal.ImprentaDigital.clsDocumentoDigitalDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_DocumentoDigitalGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            switch (valModule) {
                case "Imprenta Digital":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<DocumentoDigital> refData) {
        }

        XElement IDocumentoDigitalPdn.FindByConsecutivoCompaniaConsecutivo(int valConsecutivoCompania, int valConsecutivo) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valConsecutivo);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Adm.DocumentoDigital");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND Consecutivo = @Consecutivo");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IDocumentoDigitalPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<DocumentoDigital>, IList<DocumentoDigital>> instanciaDal = new clsDocumentoDigitalDat();
            IList<DocumentoDigital> vLista = new List<DocumentoDigital>();
            DocumentoDigital vCurrentRecord = new Galac.Adm.Dal.ImprentaDigitalDocumentoDigital();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.NumeroControl = "";
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<DocumentoDigital> ParseToListEntity(XElement valXmlEntity) {
            List<DocumentoDigital> vResult = new List<DocumentoDigital>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                DocumentoDigital vRecord = new DocumentoDigital();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroControl"), null))) {
                    vRecord.NumeroControl = vItem.Element("NumeroControl").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo


    } //End of class clsDocumentoDigitalNav

} //End of namespace Galac.Adm.Brl.ImprentaDigital

