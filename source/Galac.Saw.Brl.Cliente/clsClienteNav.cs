using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using Entity = Galac.Saw.Ccl.Cliente;
using Galac.Saw.Ccl.Cliente;
using LibGalac.Aos.Base.Dal;
using System.Xml.Linq;
using Galac.Saw.Lib;

namespace Galac.Saw.Brl.Cliente {
    public partial class clsClienteNav : LibBaseNavMaster<IList<Entity.Cliente>, IList<Entity.Cliente>>, ILibPdn, IClientePdn, ILookupDataService {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsClienteNav() {

        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataMasterComponentWithSearch<IList<Entity.Cliente>, IList<Entity.Cliente>> GetDataInstance() {

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
            ILibDataMasterComponent<IList<Entity.Cliente>, IList<Entity.Cliente>> instanciaDal = new Galac.Saw.Dal.Cliente.clsClienteDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "dbo.Gp_ClienteGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Cliente":                      
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Zona Cobranza":
                    vPdnModule = new Galac.Saw.Brl.Tablas.clsZonaCobranzaNav();
                    vResult = vPdnModule.GetDataForList("Cliente", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Sector de Negocio":
                    vPdnModule = new Galac.Comun.Brl.TablasGen.clsSectorDeNegocioNav();
                    vResult = vPdnModule.GetDataForList("Cliente", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Ciudad":
                    vPdnModule = new Galac.Comun.Brl.TablasGen.clsCiudadNav();
                    vResult = vPdnModule.GetDataForList("Cliente", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Vendedor":
                    vPdnModule = new Galac.Adm.Brl.Vendedor.clsVendedorNav();
                    vResult = vPdnModule.GetDataForList("Cliente", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Cuenta":
                    vPdnModule = new Galac.Contab.Brl.WinCont.clsCuentaNav();
                    vResult = vPdnModule.GetDataForList("Cliente", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }
		protected override void FillWithForeignInfo(ref IList<Entity.Cliente> refData) {
            FillWithForeignInfoCliente(ref refData);
            FillWithForeignInfoDireccionDeDespacho(ref refData);
        }
        #endregion Cliente

        XElement ClienteToXml(Entity.Cliente valEntidad) {
            XElement vXElement = new XElement("GpData",
                    new XElement("GpResult",
                    new XElement("ConsecutivoCompania", valEntidad.ConsecutivoCompania),
                    new XElement("Codigo", valEntidad.Codigo)));
            return vXElement;
        }
		
        private void FillWithForeignInfoCliente(ref IList<Entity.Cliente> refData) {
            XElement vInfoConexionZonaCobranza = FindInfoZonaCobranza(refData);
            var vListZonaCobranza = (from vRecord in vInfoConexionZonaCobranza.Descendants("GpResult")
                                      select new {
                                          ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                          Nombre = vRecord.Element("Nombre").Value
                                      }).Distinct();
            XElement vInfoConexionSectorDeNegocio = FindInfoSectorDeNegocio(refData);
            var vListSectorDeNegocio = (from vRecord in vInfoConexionSectorDeNegocio.Descendants("GpResult")
                                      select new {
                                          Descripcion = vRecord.Element("Descripcion").Value
                                      }).Distinct();
            XElement vInfoConexionCiudad = FindInfoCiudad(refData);
            var vListCiudad = (from vRecord in vInfoConexionCiudad.Descendants("GpResult")
                                      select new {
                                          NombreCiudad = vRecord.Element("NombreCiudad").Value, 
                                          fldOrigen = vRecord.Element("fldOrigen").Value
                                      }).Distinct();
            XElement vInfoConexionVendedor = FindInfoVendedor(refData);
            var vListVendedor = (from vRecord in vInfoConexionVendedor.Descendants("GpResult")
                                      select new {
                                          ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                          Codigo = vRecord.Element("Codigo").Value, 
                                          Nombre = vRecord.Element("Nombre").Value, 
                                          RIF = vRecord.Element("RIF").Value, 
                                          StatusVendedor = vRecord.Element("StatusVendedor").Value, 
                                          Direccion = vRecord.Element("Direccion").Value, 
                                          Ciudad = vRecord.Element("Ciudad").Value, 
                                          ZonaPostal = vRecord.Element("ZonaPostal").Value, 
                                          Telefono = vRecord.Element("Telefono").Value, 
                                          Fax = vRecord.Element("Fax").Value, 
                                          email = vRecord.Element("email").Value, 
                                          Notas = vRecord.Element("Notas").Value, 
                                          Comisiones = vRecord.Element("Comisiones").Value, 
                                          ComisionPorVenta = LibConvert.ToDec(vRecord.Element("ComisionPorVenta")), 
                                          ComisionPorCobro = LibConvert.ToDec(vRecord.Element("ComisionPorCobro")), 
                                          TopeInicialVenta1 = LibConvert.ToDec(vRecord.Element("TopeInicialVenta1")), 
                                          TopeFinalVenta1 = LibConvert.ToDec(vRecord.Element("TopeFinalVenta1")), 
                                          PorcentajeVentas1 = LibConvert.ToDec(vRecord.Element("PorcentajeVentas1")), 
                                          TopeFinalVenta2 = LibConvert.ToDec(vRecord.Element("TopeFinalVenta2")), 
                                          PorcentajeVentas2 = LibConvert.ToDec(vRecord.Element("PorcentajeVentas2")), 
                                          TopeFinalVenta3 = LibConvert.ToDec(vRecord.Element("TopeFinalVenta3")), 
                                          PorcentajeVentas3 = LibConvert.ToDec(vRecord.Element("PorcentajeVentas3")), 
                                          TopeFinalVenta4 = LibConvert.ToDec(vRecord.Element("TopeFinalVenta4")), 
                                          PorcentajeVentas4 = LibConvert.ToDec(vRecord.Element("PorcentajeVentas4")), 
                                          TopeFinalVenta5 = LibConvert.ToDec(vRecord.Element("TopeFinalVenta5")), 
                                          PorcentajeVentas5 = LibConvert.ToDec(vRecord.Element("PorcentajeVentas5")), 
                                          TopeInicialCobranza1 = LibConvert.ToDec(vRecord.Element("TopeInicialCobranza1")), 
                                          TopeFinalCobranza1 = LibConvert.ToDec(vRecord.Element("TopeFinalCobranza1")), 
                                          PorcentajeCobranza1 = LibConvert.ToDec(vRecord.Element("PorcentajeCobranza1")), 
                                          TopeFinalCobranza2 = LibConvert.ToDec(vRecord.Element("TopeFinalCobranza2")), 
                                          PorcentajeCobranza2 = LibConvert.ToDec(vRecord.Element("PorcentajeCobranza2")), 
                                          TopeFinalCobranza3 = LibConvert.ToDec(vRecord.Element("TopeFinalCobranza3")), 
                                          PorcentajeCobranza3 = LibConvert.ToDec(vRecord.Element("PorcentajeCobranza3")), 
                                          TopeFinalCobranza4 = LibConvert.ToDec(vRecord.Element("TopeFinalCobranza4")), 
                                          PorcentajeCobranza4 = LibConvert.ToDec(vRecord.Element("PorcentajeCobranza4")), 
                                          TopeFinalCobranza5 = LibConvert.ToDec(vRecord.Element("TopeFinalCobranza5")), 
                                          PorcentajeCobranza5 = LibConvert.ToDec(vRecord.Element("PorcentajeCobranza5")), 
                                          UsaComisionPorVenta = vRecord.Element("UsaComisionPorVenta").Value, 
                                          UsaComisionPorCobranza = vRecord.Element("UsaComisionPorCobranza").Value, 
                                          TipoDocumentoIdentificacion = vRecord.Element("TipoDocumentoIdentificacion").Value, 
                                          SeccionComisionesLinea = vRecord.Element("SeccionComisionesLinea").Value, 
                                          RutaDeComercializacion = vRecord.Element("RutaDeComercializacion").Value
                                      }).Distinct();
            XElement vInfoConexionCuenta = FindInfoCuenta(refData);
            var vListCuenta = (from vRecord in vInfoConexionCuenta.Descendants("GpResult")
                                      select new {
                                          ConsecutivoPeriodo = LibConvert.ToInt(vRecord.Element("ConsecutivoPeriodo")),
                                          Codigo = vRecord.Element("Codigo").Value, 
                                          Descripcion = vRecord.Element("Descripcion").Value, 
                                          SaldoInicial = LibConvert.ToDec(vRecord.Element("SaldoInicial"))
                                      }).Distinct();

            foreach (Entity.Cliente vItem in refData) {
                //vItem.NombreVendedor = vInfoConexionVendedor.Descendants("GpResult")
                    //.Where(p => p.Element("Codigo").Value == vItem.CodigoVendedor)
                    //.Select(p => p.Element("Nombre").Value).FirstOrDefault();
                vItem.DescripcionCuentaContableCxC = vInfoConexionCuenta.Descendants("GpResult")
                    .Where(p => p.Element("Codigo").Value == vItem.CuentaContableCxC)
                    .Select(p => p.Element("Descripcion").Value).FirstOrDefault();
                vItem.DescripcionCuentaContableIngresos = vInfoConexionCuenta.Descendants("GpResult")
                    .Where(p => p.Element("Codigo").Value == vItem.CuentaContableCxC)
                    .Select(p => p.Element("Descripcion").Value).FirstOrDefault();
                vItem.DescripcionCuentaContableAnticipo = vInfoConexionCuenta.Descendants("GpResult")
                    .Where(p => p.Element("Codigo").Value == vItem.CuentaContableCxC)
                    .Select(p => p.Element("Descripcion").Value).FirstOrDefault();
            }
        }	
		
		
        private XElement FindInfoZonaCobranza(IList<Entity.Cliente> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(Entity.Cliente vItem in valData) {
                vXElement.Add(FilterClienteByDistinctZonaCobranza(vItem).Descendants("GpResult"));
            }
            ILibPdn insZonaCobranza = new Galac.Saw.Brl.Tablas.clsZonaCobranzaNav();
            XElement vXElementResult = insZonaCobranza.GetFk("Cliente", ParametersGetFKZonaCobranzaForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }
		
		
		private XElement FilterClienteByDistinctZonaCobranza(Entity.Cliente valMaster) {
            XElement vXElement = new XElement("GpData",
                new XElement("GpResult",
                    new XElement("ZonaDeCobranza", valMaster.ZonaDeCobranza)));
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
		
        private StringBuilder ParametersGetFKZonaCobranzaForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
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
		
		 private XElement FindInfoSectorDeNegocio(IList<Entity.Cliente> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(Entity.Cliente vItem in valData) {
                vXElement.Add(FilterClienteByDistinctSectorDeNegocio(vItem).Descendants("GpResult"));
            }
            ILibPdn insSectorDeNegocio = new Galac.Comun.Brl.TablasGen.clsSectorDeNegocioNav();
            XElement vXElementResult = insSectorDeNegocio.GetFk("Cliente", ParametersGetFKSectorDeNegocioForXmlSubSet(vXElement));
            return vXElementResult;
        }

        private XElement FilterClienteByDistinctSectorDeNegocio(Entity.Cliente valMaster) {
            XElement vXElement = new XElement("GpData",
                new XElement("GpResult",
                    new XElement("SectorDeNegocio", valMaster.SectorDeNegocio)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKSectorDeNegocioForXmlSubSet(XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vResult = vParams.Get();
            return vResult;
        }

        private XElement FindInfoCiudad(IList<Entity.Cliente> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(Entity.Cliente vItem in valData) {
                vXElement.Add(FilterClienteByDistinctCiudad(vItem).Descendants("GpResult"));
            }
            ILibPdn insCiudad = new Galac.Comun.Brl.TablasGen.clsCiudadNav();
            XElement vXElementResult = insCiudad.GetFk("Cliente", ParametersGetFKCiudadForXmlSubSet(vXElement));
            return vXElementResult;
        }

        private XElement FilterClienteByDistinctCiudad(Entity.Cliente valMaster) {
            XElement vXElement = new XElement("GpData",
                new XElement("GpResult",
                    new XElement("Ciudad", valMaster.Ciudad)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKCiudadForXmlSubSet(XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vResult = vParams.Get();
            return vResult;
        }

        private XElement FindInfoVendedor(IList<Entity.Cliente> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(Entity.Cliente vItem in valData) {
                vXElement.Add(FilterClienteByDistinctVendedor(vItem).Descendants("GpResult"));
            }
            ILibPdn insVendedor = new Galac.Adm.Brl.Vendedor.clsVendedorNav();
            XElement vXElementResult = insVendedor.GetFk("Cliente", ParametersGetFKVendedorForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterClienteByDistinctVendedor(Entity.Cliente valMaster) {
            XElement vXElement = new XElement("GpData",
                new XElement("GpResult",
                    new XElement("Consecutivo", valMaster.ConsecutivoVendedor)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKVendedorForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement FindInfoCuenta(IList<Entity.Cliente> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(Entity.Cliente vItem in valData) {
                vXElement.Add(FilterClienteByDistinctCuenta(vItem).Descendants("GpResult"));
            }
            ILibPdn insCuenta = new Galac.Contab.Brl.WinCont.clsCuentaNav();
            XElement vXElementResult = insCuenta.GetFk("Cliente", ParametersGetFKCuentaForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterClienteByDistinctCuenta(Entity.Cliente valMaster) {
            XElement vXElement = new XElement("GpData",
                new XElement("GpResult",
                    new XElement("CuentaContableCxC", valMaster.CuentaContableCxC)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKCuentaForXmlSubSet(int valConsecutivoPeriodo, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoPeriodo", valConsecutivoPeriodo);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
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
            //vCliente.ConsecutivoVendedor = BuscarConsecutivoVendedor(vCliente.CodigoVendedor);
            if (ExisteElCodigoDeCliente(vCliente.Codigo)) {
                vCliente.Codigo = GeneraCodigoClienteRapidoSinCast();
            }
            if (ExisteElCodigoDeCliente(vCliente.Codigo)) {
                vCliente.Codigo = GeneraCodigoClienteRapidoConCast();
            }

            refCodigo = vCliente.Codigo;

            List<Entity.Cliente> vList = new List<Entity.Cliente>();
            vList.Add(vCliente);
            return _Db.Insert(vList, false);
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
		
		        private void FillWithForeignInfoDireccionDeDespacho(ref IList<Entity.Cliente> refData) {
            XElement vInfoConexionCliente = FindInfoCliente(refData);
            var vListCliente = (from vRecord in vInfoConexionCliente.Descendants("GpResult")
                                      select new {
                                          ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                          Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo")), 
                                          Codigo = vRecord.Element("Codigo").Value, 
                                          Nombre = vRecord.Element("Nombre").Value, 
                                          NumeroRIF = vRecord.Element("NumeroRIF").Value, 
                                          NumeroNIT = vRecord.Element("NumeroNIT").Value, 
                                          Status = vRecord.Element("Status").Value, 
                                          SeccionGenerales = vRecord.Element("SeccionGenerales").Value, 
                                          Contacto = vRecord.Element("Contacto").Value, 
                                          Telefono = vRecord.Element("Telefono").Value, 
                                          FAX = vRecord.Element("FAX").Value, 
                                          Email = vRecord.Element("Email").Value, 
                                          ZonaDeCobranza = vRecord.Element("ZonaDeCobranza").Value, 
                                          SectorDeNegocio = vRecord.Element("SectorDeNegocio").Value, 
                                          NivelDePrecio = vRecord.Element("NivelDePrecio").Value, 
                                          Direccion = vRecord.Element("Direccion").Value, 
                                          Ciudad = vRecord.Element("Ciudad").Value, 
                                          ZonaPostal = vRecord.Element("ZonaPostal").Value, 
                                          CodigoVendedor = vRecord.Element("CodigoVendedor").Value, 
                                          ConsecutivoVendedor = LibConvert.ToInt(vRecord.Element("ConsecutivoVendedor")), 
                                         // NombreVendedor = vRecord.Element("NombreVendedor").Value, 
                                          SeccionAdvertencias = vRecord.Element("SeccionAdvertencias").Value, 
                                          RazonInactividad = vRecord.Element("RazonInactividad").Value, 
                                          ActivarAvisoAlEscoger = vRecord.Element("ActivarAvisoAlEscoger").Value, 
                                          TextoDelAviso = vRecord.Element("TextoDelAviso").Value, 
                                          SeccionDirDespacho = vRecord.Element("SeccionDirDespacho").Value, 
                                          SeccionReglasContables = vRecord.Element("SeccionReglasContables").Value, 
                                          CuentaContableCxC = vRecord.Element("CuentaContableCxC").Value, 
                                          DescripcionCuentaContableCxC = vRecord.Element("DescripcionCuentaContableCxC").Value, 
                                          CuentaContableIngresos = vRecord.Element("CuentaContableIngresos").Value, 
                                          DescripcionCuentaContableIngresos = vRecord.Element("DescripcionCuentaContableIngresos").Value, 
                                          CuentaContableAnticipo = vRecord.Element("CuentaContableAnticipo").Value, 
                                          DescripcionCuentaContableAnticipo = vRecord.Element("DescripcionCuentaContableAnticipo").Value, 
                                          InfoGalac = vRecord.Element("InfoGalac").Value, 
                                          CodigoLote = vRecord.Element("CodigoLote").Value, 
                                          Origen = vRecord.Element("Origen").Value, 
                                          DiaCumpleanos = LibConvert.ToInt(vRecord.Element("DiaCumpleanos")), 
                                          MesCumpleanos = LibConvert.ToInt(vRecord.Element("MesCumpleanos")), 
                                          CorrespondenciaXEnviar = vRecord.Element("CorrespondenciaXEnviar").Value, 
                                          EsExtranjero = vRecord.Element("EsExtranjero").Value, 
                                          ClienteDesdeFecha = vRecord.Element("ClienteDesdeFecha").Value, 
                                          AQueSeDedicaElCliente = vRecord.Element("AQueSeDedicaElCliente").Value, 
                                          TipoDocumentoIdentificacion = vRecord.Element("TipoDocumentoIdentificacion").Value, 
                                          TipoDeContribuyente = vRecord.Element("TipoDeContribuyente").Value, 
                                          CampoDefinible1 = vRecord.Element("CampoDefinible1").Value
                                      }).Distinct();
            XElement vInfoConexionCiudad = FindInfoCiudad(refData);
            var vListCiudad = (from vRecord in vInfoConexionCiudad.Descendants("GpResult")
                                      select new {
                                          NombreCiudad = vRecord.Element("NombreCiudad").Value, 
                                          fldOrigen = vRecord.Element("fldOrigen").Value
                                      }).Distinct();
            foreach(Entity.Cliente vItem in refData) {
                vItem.DetailDireccionDeDespacho = 
                    new System.Collections.ObjectModel.ObservableCollection<DireccionDeDespacho>((
                        from vDetail in vItem.DetailDireccionDeDespacho
                        join vCliente in vListCliente
                        on new {codigo = vDetail.CodigoCliente, ConsecutivoCompania = vDetail.ConsecutivoCompania}
                        equals
                        new { codigo = vCliente.Codigo, ConsecutivoCompania = vCliente.ConsecutivoCompania}
                        join vCiudad in vListCiudad
                        on new {NombreCiudad = vDetail.Ciudad}
                        equals
                        new { NombreCiudad = vCiudad.NombreCiudad}
                        select new DireccionDeDespacho {
                            ConsecutivoCompania = vDetail.ConsecutivoCompania, 
                            CodigoCliente = vDetail.CodigoCliente, 
                            ConsecutivoDireccion = vDetail.ConsecutivoDireccion, 
                            PersonaContacto = vDetail.PersonaContacto, 
                            Direccion = vDetail.Direccion, 
                            Ciudad = vDetail.Ciudad, 
                            ZonaPostal = vDetail.ZonaPostal
                        }).ToList<DireccionDeDespacho>());
            }
        }

        private XElement FindInfoCliente(IList<Entity.Cliente> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(Entity.Cliente vItem in valData) {
                vXElement.Add(FilterDireccionDeDespachoByDistinctCliente(vItem).Descendants("GpResult"));
            }
            ILibPdn insCliente = new Galac.Saw.Brl.Cliente.clsClienteNav();
            XElement vXElementResult = insCliente.GetFk("Cliente", ParametersGetFKClienteForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterDireccionDeDespachoByDistinctCliente(Entity.Cliente valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailDireccionDeDespacho.Distinct()
                select new XElement("GpResult",
                    new XElement("CodigoCliente", vEntity.CodigoCliente)));
            return vXElement;
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
		
		private StringBuilder ParametersGetFKClienteForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
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

        XElement IClientePdn.FindByConsecutivo(int valConsecutivoCompania, int valConsecutivo) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("Consecutivo", valConsecutivo);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Saw.Cliente");
            SQL.AppendLine("WHERE Consecutivo = @Consecutivo");
            SQL.AppendLine("AND ConsecutivoCompania = @ConsecutivoCompania");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }
    } //End of class clsClienteNav

} //End of namespace Galac.Saw.Brl.Clientes


