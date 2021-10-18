using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Ccl.Cliente;
using System.Xml.Linq;
using System.Linq;
using LibGalac.Aos.Base.Dal;

namespace Galac.Saw.Brl.Inventario {
    public partial class clsAlmacenNav : LibBaseNav<IList<Almacen>, IList<Almacen>>, IAlmacenPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsAlmacenNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<Almacen>, IList<Almacen>> GetDataInstance() {
            return new Galac.Saw.Dal.Inventario.clsAlmacenDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Inventario.clsAlmacenDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Inventario.clsAlmacenDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Saw.Gp_AlmacenSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<Almacen>, IList<Almacen>> instanciaDal = new Galac.Saw.Dal.Inventario.clsAlmacenDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Saw.Gp_AlmacenGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Almacén":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;                
                case "Cliente":
                    vPdnModule = new Galac.Saw.Brl.Cliente.clsClienteNav();
                    vResult = vPdnModule.GetDataForList("Almacén", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Centro De Costos":
                    vPdnModule = new Galac.Contab.Brl.WinCont.clsCentroDeCostosNav();
                    vResult = vPdnModule.GetDataForList("Almacén", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }
        #endregion //Metodos Generados


        private StringBuilder ParametroCompania(int valConsecutivoCompania) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }

        int GetNextConsecutivo(int valConsecutivoCompania, ILibDataComponent<IList<Almacen>, IList<Almacen>> instanciaDal) {
            XElement vResult;
            int vNumero;
            vResult = instanciaDal.QueryInfo(eProcessMessageType.Message, "ProximoConsecutivo", ParametroCompania(valConsecutivoCompania));
            vNumero = LibConvert.ToInt(LibXml.GetPropertyString(vResult, "Consecutivo"));
            return vNumero;
        }

        void IAlmacenPdn.InsertarAlmacenPorDefecto(int valConsecutivoCompania) {
            ILibDataComponent<IList<Almacen>, IList<Almacen>> instanciaDal = new Dal.Inventario.clsAlmacenDat();
            List<Almacen> vLista = new List<Almacen>();
            Almacen vCurrentRecord = new Almacen();
            IClientePdn ReglasCliente = new Galac.Saw.Brl.Cliente.clsClienteNav();

            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.Consecutivo = GetNextConsecutivo(valConsecutivoCompania, instanciaDal);
            vCurrentRecord.Codigo = "UNICO";
            vCurrentRecord.NombreAlmacen = "GENERICO";
            vCurrentRecord.TipoDeAlmacenAsEnum = (eTipoDeAlmacen.Principal);
            vCurrentRecord.ConsecutivoCliente = 1;
            vCurrentRecord.CodigoCc = "";
            vCurrentRecord.Descripcion = "";
            vLista.Add(vCurrentRecord);
            instanciaDal.Insert(vLista);
        }

        string IAlmacenPdn.GetCodigoAlmacenPorDefecto(int valConsecutivoCompania) {
            StringBuilder vSql = new StringBuilder();
            string vResult = "";
            vSql.AppendLine(" DECLARE @ConsecutivoCompania INT");
            vSql.AppendLine(" DECLARE @NombreAlmacen VARCHAR(40)");

            vSql.AppendLine(" SELECT ConsecutivoCompania, Consecutivo, Codigo, NombreAlmacen");
            vSql.AppendLine(" FROM Saw.Almacen");
            vSql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania");
            vSql.AppendLine(" AND NombreAlmacen = @NombreAlmacen");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString ("NombreAlmacen", "GENERICO",40);
            ILibDataComponent<IList<Almacen>, IList<Almacen>> insDal = GetDataInstance();
            XElement valRecord = insDal.QueryInfo(eProcessMessageType.Query,null, vSql);
            if(!(System.NullReferenceException.ReferenceEquals(valRecord, null))) {
                var vEntity = from vRecord in valRecord.Descendants("GpResult")
                              select vRecord;
                foreach(XElement vItem in vEntity) {
                    if(!(System.NullReferenceException.ReferenceEquals(vItem.Element("Codigo"), null))) {
                        vResult = LibConvert.ToStr(vItem.Element("Codigo").Value);
                    }
                }
            } else {
                vResult = "UNICO";
            }
            return vResult;            
        }
        public object ConsultaCampoClientePorCodigo(string valCampo, string valCodigo, int valConsecutivoCompania) { 
            IClientePdn ReglasCliente = new Galac.Saw.Brl.Cliente.clsClienteNav();
            return ReglasCliente.ConsultaCampoClientePorCodigo(valCampo, valCodigo, valConsecutivoCompania);
        }

        int IAlmacenPdn.ObtenerConsecutivoAlmacen(string valCodigo, int valConsecutivoCompania) {
            LibGpParams vParams = new LibGpParams();
            QAdvSql insQAdvSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            int vResult = 0;
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Codigo", valCodigo, 5);
            vSql.Append("SELECT ");
            vSql.Append(" Consecutivo");
            vSql.Append(" FROM");
            vSql.Append(" Saw.Gv_Almacen_B1");
            vSql.Append(" WHERE ConsecutivoCompania = @ConsecutivoCompania");
            vSql.Append(" AND Codigo = @Codigo");
            
            ILibDataComponent<IList<Almacen>, IList<Almacen>> instanciaDal = GetDataInstance();
            XElement vResulset = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), string.Empty, 0);
            if (vResulset != null){
                vResult = (from vRecord in vResulset.Descendants("GpResult")
                       select new {
                           Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo"))
                       }).FirstOrDefault().Consecutivo;
            }
            return vResult;
        }


        protected override LibResponse InsertRecord(IList<Almacen> refRecord) {
            foreach (Almacen element in refRecord) {
                if (element.ConsecutivoCliente == 0) {
                    element.ConsecutivoCliente = LibConvert.ToInt(ConsultaCampoClientePorCodigo("Consecutivo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("RecordName", "CodigoGenericoCliente"), element.ConsecutivoCompania));
                }
            }
            return base.InsertRecord(refRecord);
        }

    } //End of class clsAlmacenNav

} //End of namespace Galac.Saw.Brl.Inventario

