using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using Entity = Galac.Saw.Ccl.Cliente;
using Galac.Saw.Ccl.Cliente;
using LibGalac.Aos.Base.Dal;
using System.Xml.Linq;
using Galac.Saw.Lib;

namespace Galac.Saw.Brl.Cliente {
    public partial class clsClienteNav : LibBaseNav<IList<Entity.Cliente>, IList<Entity.Cliente>>, ILibPdn, IClientePdn, ILookupDataService {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsClienteNav() {

        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<Entity.Cliente>, IList<Entity.Cliente>> GetDataInstance() {

            return new Galac.Saw.Dal.Cliente.clsClienteDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Clientes.clsClienteDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Cliente.clsClienteDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_ClienteSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<Entity.Cliente>, IList<Entity.Cliente>> instanciaDal = new Galac.Saw.Dal.Cliente.clsClienteDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "dbo.Gp_ClienteGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            //ILibPdn vPdnModule;
            switch (valModule) {
                case "Cliente":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                //case "Ciudad":
                //    vPdnModule = new Galac.dbo.Brl.Ciudad.clsCiudadNav();
                //    vResult = vPdnModule.GetDataForList("Cliente", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                //case "Zona Cobranza":
                //    vPdnModule = new Galac.Saw.Brl.Tablas.clsZonaCobranzaNav();
                //    vResult = vPdnModule.GetDataForList("Cliente", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                //case "Vendedor":
                //    vPdnModule = new Galac.dbo.Brl.Vendedor.clsVendedorNav();
                //    vResult = vPdnModule.GetDataForList("Cliente", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                //case "Cuenta":
                //    vPdnModule = new Galac.dbo.Brl.Cuenta.clsCuentaNav();
                //    vResult = vPdnModule.GetDataForList("Cliente", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                //case "Sector de Negocio":
                //    vPdnModule = new Galac.dbo.Brl.SectorDeNegocio.clsSectorDeNegocioNav();
                //    vResult = vPdnModule.GetDataForList("Cliente", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                default: break;
            }
            return vResult;
        }
        #endregion //Metodos Generados

        XElement ClienteToXml(Entity.Cliente valEntidad) {
            XElement vXElement = new XElement("GpData",
                    new XElement("GpResult",
                    new XElement("ConsecutivoCompania", valEntidad.ConsecutivoCompania),
                    new XElement("Codigo", valEntidad.Codigo)));
            return vXElement;
        }

        Entity.Cliente ClientePorDefecto(int valConcecutivoCompania) {
            Entity.Cliente insCliente = new Entity.Cliente();
            insCliente.Codigo = "000000000A";
            insCliente.ConsecutivoCompania = valConcecutivoCompania;
            return insCliente;
        }
        XElement IClientePdn.ClientePorDefecto(int valConcecutivoCompania) {
            return ClienteToXml(ClientePorDefecto(valConcecutivoCompania));
        }


        object IClientePdn.ConsultaCampoClientePorCodigo(string valCampo, string valCodigo, int valConsecutivoCompania) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Codigo", valCodigo, 10);
            IList<Galac.Saw.Ccl.Cliente.Cliente> ListCliente = ((ILibBusinessComponentWithSearch<IList<Galac.Saw.Ccl.Cliente.Cliente>, IList<Galac.Saw.Ccl.Cliente.Cliente>>)this).GetData(eProcessMessageType.SpName, "ClienteGET", vParams.Get());
            Galac.Saw.Ccl.Cliente.Cliente cl1 = ListCliente[0];
            return cl1.GetType().GetProperty(valCampo).GetValue(cl1, null);
        }

        private int BuscarConsecutivoVendedor(string valCodigoVendedor) {
            int vResult = 1;
            string vSql = "SELECT Consecutivo FROM ADM.Vendedor WHERE Codigo =" + new QAdvSql("").ToSqlValue(valCodigoVendedor);
            var vSqlResult = LibBusiness.ExecuteSelect(vSql, null, "", 0);
            if (vSqlResult != null && vSqlResult.HasElements) {
                vResult = LibConvert.ToInt(LibXml.GetPropertyString(vSqlResult, "Consecutivo"));
            }
            return vResult;
        }

        LibResponse IClientePdn.InsertClienteForExternalRecord(string valNombre, string valNumeroRIF, string valDireccion, string valTelefono, ref string refCodigo, eTipoDocumentoIdentificacion valTipoDocumentoIdentificacion) {
            Entity.Cliente vCliente = new Entity.Cliente();
            //llenar los campos que voy a insertar
            vCliente.Nombre = valNombre;
            vCliente.NumeroRIF = valNumeroRIF;
            vCliente.Direccion = valDireccion;
            vCliente.Telefono = valTelefono;
            vCliente.ClienteDesdeFecha = LibDate.Today();
            vCliente.CodigoVendedor = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "CodigoGenericoVendedor");
            vCliente.SectorDeNegocio = "No Asignado";
            vCliente.StatusAsEnum = eStatusCliente.Activo;
            vCliente.Ciudad = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "Ciudad");
            vCliente.ZonaDeCobranza = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "Ciudad");
            vCliente.TipoDocumentoIdentificacion = LibConvert.EnumToDbValue((int)valTipoDocumentoIdentificacion);
            RegisterClient();
            QAdvSql vQAdvSQL = new QAdvSql("");
            StringBuilder vParam = new StringBuilder();

            vParam.AppendLine(" ConsecutivoCompania = " + vQAdvSQL.ToSqlValue(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania")));
            vParam.AppendLine(" AND  Codigo NOT IN (" + vQAdvSQL.ToSqlValue("RD_Cliente") + ", " + vQAdvSQL.ToSqlValue("000000000A") + ")");

            LibGalac.Aos.Dal.LibDatabase insDb = new LibGalac.Aos.Dal.LibDatabase(clsCkn.ConfigKeyForDbService);
            vCliente.Codigo = insDb.NextStrConsecutive("Cliente", "Codigo", new QAdvSql("").SqlIntValueWithAnd("", "ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania")) + " AND Codigo <> '000000000A' AND codigo <> 'RD_Cliente'", true, 10);
            vCliente.ConsecutivoVendedor = BuscarConsecutivoVendedor(vCliente.CodigoVendedor);
            if (ExisteElCodigoDeCliente(vCliente.Codigo)) {
                vCliente.Codigo = GeneraCodigoClienteRapidoSinCast();
            }
            if (ExisteElCodigoDeCliente(vCliente.Codigo)) {
                vCliente.Codigo = GeneraCodigoClienteRapidoConCast();
            }

            refCodigo = vCliente.Codigo;

            List<Entity.Cliente> vList = new List<Entity.Cliente>();
            vList.Add(vCliente);
            return _Db.Insert(vList);
        }


        bool ExisteElCodigoDeCliente(string valCodigo) {
            LibGpParams vParams = new LibGpParams();
            QAdvSql insQAdvSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            bool vResult = false;

            vParams.AddInInteger("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            vParams.AddInString("Codigo", valCodigo, 10);

            vSql.Append("SELECT COUNT(Codigo) as Existe");
            vSql.Append(" FROM Cliente");
            vSql.Append(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.Append(" AND Codigo = @Codigo ");
            XElement vData = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), string.Empty, 0);
            if (vData != null) {
                vResult = LibConvert.ToInt(LibXml.GetPropertyString(vData, "Existe")) > 0;
            }
            return vResult;
        }

        string GeneraCodigoClienteRapidoSinCast() {
            LibGpParams vParams = new LibGpParams();
            QAdvSql insQAdvSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            string vResult = string.Empty;
            string vMaximo = string.Empty;

            vParams.AddInInteger("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            vParams.AddInString("IsNumeric", "1", 1);
            vParams.AddInString("ClienteOficina", "000000000A", 10);
            vParams.AddInString("ClienteResumen", "RD_Cliente", 10);

            vSql.Append("SELECT MAX(Codigo) + 1 AS Maximo");
            vSql.Append(" FROM Cliente");
            vSql.Append(" WHERE consecutivoCompania = @ConsecutivoCompania");
            vSql.Append(" AND ISNUMERIC(Codigo) = @IsNumeric");
            vSql.Append(" AND Codigo <> @ClienteOficina");
            vSql.Append(" AND Codigo <> @ClienteResumen");
            vSql.Append(" GROUP BY ConsecutivoCompania");
            XElement vResultSet = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), string.Empty, 0);

            if (vResultSet != null) {
                vMaximo = LibXml.GetPropertyString(vResultSet, "Maximo");
            } else {
                vMaximo = "1";
            }

            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "RellenaCerosAlaIzquierda")) {
                vResult = LibText.FillWithCharToLeft(vMaximo.ToString(), "0", 10);
            } else {
                vResult = vMaximo;
            }
            return vResult;
        }

        string GeneraCodigoClienteRapidoConCast() {
            LibGpParams vParams = new LibGpParams();
            StringBuilder vSql = new StringBuilder();
            string vResult = string.Empty;
            string vMaximo = string.Empty;

            vParams.AddInInteger("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            vParams.AddInString("IsNumeric", "1", 1);
            vParams.AddInString("ClienteOficina", "000000000A", 10);
            vParams.AddInString("ClienteResumen", "RD_Cliente", 10);

            vSql.Append("SELECT MAX(CAST(Codigo AS bigint)) + 1 AS Maximo");
            vSql.Append(" FROM Cliente");
            vSql.Append(" WHERE consecutivoCompania = @ConsecutivoCompania");
            vSql.Append(" AND ISNUMERIC(Codigo) = @IsNumeric");
            vSql.Append(" AND Codigo <> @ClienteOficina");
            vSql.Append(" AND Codigo <> @ClienteResumen");
            vSql.Append(" GROUP BY ConsecutivoCompania");
            XElement vResultSet = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), string.Empty, 0);

            if (vResultSet != null) {
                vMaximo = LibXml.GetPropertyString(vResultSet, "Maximo");
            } else {
                vMaximo = "1";
            }

            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "RellenaCerosAlaIzquierda")) {
                vResult = LibText.FillWithCharToLeft(vMaximo.ToString(), "0", 10);
            } else {
                vResult = vMaximo;
            }
            return vResult;
        }

        public string ObtenerEmailCliente(int valConsecutivoCompania, string valCodigoCliente) {
            string vResult = string.Empty;
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Codigo", valCodigoCliente, 10);
            vSql.AppendLine(" SELECT Email FROM dbo.Cliente");
            vSql.AppendLine(" WHERE ConsecutivoCompania= @ConsecutivoCompania AND Codigo = @Codigo");
            XElement xResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), "", 0);
            vResult = xResult.Value;
            return vResult;
        }

        public XElement GetDataPageByCode(string valCodeFilter,int companyCode,int valPage)
        {
            if (valCodeFilter == null) valCodeFilter = string.Empty;
            StringBuilder SQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("filtro", valCodeFilter, 50);
            vParams.AddInInteger("pagina", valPage);
            vParams.AddInInteger("consecutivoCompania", companyCode);
            SQL.AppendLine("EXEC dbo.Gp_ObtenerPaginaDeClientesPorRIF @filtro, @consecutivoCompania, @pagina, @articulosPorPagina=10");
            XElement xRecord = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", 0);
            return xRecord;
        }

        public XElement GetDataPageByDescription(string valDescriptionFilter, int companyCode,int valPage)
        {
            StringBuilder SQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("filtro", valDescriptionFilter, 50);
            vParams.AddInInteger("pagina", valPage);
            vParams.AddInInteger("consecutivoCompania", companyCode);
            SQL.AppendLine("EXEC dbo.Gp_ObtenerPaginaDeClientesPorNombre @filtro, @consecutivoCompania, @pagina, @articulosPorPagina=10");
            XElement xRecord = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", 0);
            return xRecord;
        }
    } //End of class clsClienteNav

} //End of namespace Galac.Saw.Brl.Clientes


