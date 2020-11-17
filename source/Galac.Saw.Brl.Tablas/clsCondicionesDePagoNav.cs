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
    public partial class clsCondicionesDePagoNav: LibBaseNav<IList<CondicionesDePago>, IList<CondicionesDePago>>, ICondicionesDePagoPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCondicionesDePagoNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<CondicionesDePago>, IList<CondicionesDePago>> GetDataInstance() {
            return new Galac.Saw.Dal.Tablas.clsCondicionesDePagoDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Tablas.clsCondicionesDePagoDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Tablas.clsCondicionesDePagoDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Saw.Gp_CondicionesDePagoSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<CondicionesDePago>, IList<CondicionesDePago>> instanciaDal = new Galac.Saw.Dal.Tablas.clsCondicionesDePagoDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Saw.Gp_CondicionesDePagoGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            switch (valModule) {
                case "Condiciones De Pago":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<CondicionesDePago> refData) {
        }

        XElement ICondicionesDePagoPdn.FindByConsecutivoCompaniaDescripcion(int valConsecutivoCompania, string valDescripcion) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Descripcion", valDescripcion, 80);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Saw.CondicionesDePago");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND Descripcion = @Descripcion");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }
        #endregion //Metodos Generados

        public void InsertarValorDefault() {
            StringBuilder vSql = new StringBuilder();
            QAdvSql vUtilSql = new QAdvSql("");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("Consecutivo", 0);
            vParams.AddInString("Descripcion", "A Convenir", 10);
            vSql.AppendLine("INSERT INTO Saw.CondicionesDePago (ConsecutivoCompania, Consecutivo, Descripcion)");
            vSql.AppendLine("(SELECT ConsecutivoCompania, @Consecutivo, @Descripcion FROM dbo.Compania)");
            LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0);
        }
		
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool ICondicionesDePagoPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<CondicionesDePago>, IList<CondicionesDePago>> instanciaDal = new clsCondicionesDePagoDat();
            IList<CondicionesDePago> vLista = new List<CondicionesDePago>();
            CondicionesDePago vCurrentRecord = new Galac.Saw.Dal.TablasCondicionesDePago();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.Descripcion = "";
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<CondicionesDePago> ParseToListEntity(XElement valXmlEntity) {
            List<CondicionesDePago> vResult = new List<CondicionesDePago>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                CondicionesDePago vRecord = new CondicionesDePago();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Descripcion"), null))) {
                    vRecord.Descripcion = vItem.Element("Descripcion").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo


    } //End of class clsCondicionesDePagoNav

} //End of namespace Galac.Saw.Brl.Tablas

