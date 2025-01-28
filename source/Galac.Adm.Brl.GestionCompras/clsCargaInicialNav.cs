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
using Galac.Adm.Ccl.GestionCompras;
using Galac.Saw.Ccl.Inventario;
using System.Threading.Tasks;
using System.Threading;
using Galac.Adm.Dal.GestionCompras;

namespace Galac.Adm.Brl.GestionCompras {
    public partial class clsCargaInicialNav : LibBaseNav<IList<CargaInicial>, IList<CargaInicial>>, ICargaInicialPdn, IServicioDeDatosCargaInicial {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCargaInicialNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<CargaInicial>, IList<CargaInicial>> GetDataInstance() {
            return new Galac.Adm.Dal.GestionCompras.clsCargaInicialDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.GestionCompras.clsCargaInicialDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.GestionCompras.clsCargaInicialDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_CargaInicialSCH", valXmlParamsExpression);
            
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<CargaInicial>, IList<CargaInicial>> instanciaDal = new Galac.Adm.Dal.GestionCompras.clsCargaInicialDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_CargaInicialGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Carga Inicial":
                    valXmlParamsExpression.Replace("Existencia", "Adm.Gv_CargaInicial_B1.Existencia");
                    valXmlParamsExpression.Replace("\"@SQLOrderBy\" valor=\"\"", "\"@SQLOrderBy\" valor=\"CodigoArticulo\"");
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Articulo Inventario":
                    string valWhere, valOrderBy;
                    ObtenerParametrosDeBusquedaAlInsertarCargaInicial(valXmlParamsExpression, out valWhere, out valOrderBy);
                    var vArticulos = ObtenerArticulosInsertarCargaInicial(
                        LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), "", 
                        LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("CargaInicial", "FechaInicial"),
                        string.Empty, string.Empty, string.Empty, string.Empty, valWhere, valOrderBy);
                    var vArticulosXml = new XmlDocument();
                    if (vArticulos != null) {
                        string vArticulosString = vArticulos.ToString();
                        vArticulosString = vArticulosString.Replace("Cantidad", "Existencia");
                        vArticulosXml.LoadXml(vArticulosString);
                        refXmlDocument = vArticulosXml;
                    }
                    vResult = refXmlDocument != null;
                    break;
                case "Articulo Inventario - Carga Inicial":
                    valXmlParamsExpression.Replace("dbo.Gv_ArticuloInventario_B1", "Adm.Gv_CargaInicial_B1");
                    valXmlParamsExpression.Replace("LineaDeProducto", "Adm.Gv_CargaInicial_B1.LineaDeProducto");
                    valXmlParamsExpression.Replace("Descripcion", "Adm.Gv_CargaInicial_B1.Descripcion");
                    valXmlParamsExpression.Replace("Codigo ", "CodigoArticulo");
                    valXmlParamsExpression.Replace("CostoUnitario", "Costo");
                    valXmlParamsExpression.Replace("\"@SQLOrderBy\" valor=\"\"", "\"@SQLOrderBy\" valor=\"CodigoArticulo\"");
                    //new LibGpParams().
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression); //CargarListaDeArticulos(ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Linea De Producto":
                    vPdnModule = new Galac.Saw.Brl.Tablas.clsLineaDeProductoNav();
                    vResult = vPdnModule.GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Categoria":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsCategoriaNav();
                    vResult = vPdnModule.GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        //protected override void FillWithForeignInfo(ref IList<CargaInicial> refData) {
        //    FillWithForeignInfoCargaInicial(ref refData);
        //}
        #region CargaInicial

        //private void FillWithForeignInfoCargaInicial(ref IList<CargaInicial> refData) {
        //    XElement vInfoConexionArticuloInventario = FindInfoArticuloInventario(refData);
        //    var vListArticuloInventario = (from vRecord in vInfoConexionArticuloInventario.Descendants("GpResult")
        //                              select new {
        //                                  ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),                                          
        //                                  Codigo = vRecord.Element("Codigo").Value, 
        //                                  Descripcion = vRecord.Element("Descripcion").Value, 
        //                                  LineaDeProducto = vRecord.Element("LineaDeProducto").Value, 
        //                                  StatusdelArticulo = vRecord.Element("StatusdelArticulo").Value, 
        //                                  TipoDeArticulo = vRecord.Element("TipoDeArticulo").Value, 
        //                                  AlicuotaIVA = vRecord.Element("AlicuotaIVA").Value, 
        //                                  PrecioSinIVA = LibConvert.ToDec(vRecord.Element("PrecioSinIVA")), 
        //                                  PrecioConIVA = LibConvert.ToDec(vRecord.Element("PrecioConIVA")), 
        //                                  Existencia = LibConvert.ToDec(vRecord.Element("Existencia")), 
        //                                  Categoria = vRecord.Element("Categoria").Value, 
        //                                  Marca = vRecord.Element("Marca").Value, 
        //                                  FechaDeVencimiento = vRecord.Element("FechaDeVencimiento").Value, 
        //                                  UnidadDeVenta = vRecord.Element("UnidadDeVenta").Value, 
        //                                  TipoArticuloInv = vRecord.Element("TipoArticuloInv").Value
        //                              }).Distinct();

        //    foreach (CargaInicial vItem in refData) {
        //    }
        //}

        //private XElement FindInfoArticuloInventario(IList<CargaInicial> valData) {
        //    XElement vXElement = new XElement("GpData");
        //    foreach(CargaInicial vItem in valData) {
        //        vXElement.Add(FilterCargaInicialByDistinctArticuloInventario(vItem).Descendants("GpResult"));
        //    }
        //    ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
        //    XElement vXElementResult = insArticuloInventario.GetFk("CargaInicial", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
        //    return vXElementResult;
        //}

        //private XElement FilterCargaInicialByDistinctArticuloInventario(CargaInicial valMaster) {
        //    XElement vXElement = new XElement("GpData",
        //        from vEntity in valMaster.CodigoArticulo.Distinct()
        //        select new XElement("GpResult",
        //            new XElement("CodigoArticulo", vEntity.CodigoArticulo)));
        //    return vXElement;
        //}

        private StringBuilder ParametersGetFKArticuloInventarioForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }
        #endregion //CargaInicial

        XElement ICargaInicialPdn.FindByConsecutivoCompaniaCodigoFecha(int valConsecutivoCompania, string valCodigo, DateTime valFecha) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Codigo", valCodigo, 30);
            vParams.AddInDateTime("Fecha", valFecha);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Adm.CargaInicial");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND Codigo = @Codigo");
            SQL.AppendLine("AND Fecha = @FechaInicial");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool ICargaInicialPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<CargaInicial>, IList<CargaInicial>> instanciaDal = new clsCargaInicialDat();
            IList<CargaInicial> vLista = new List<CargaInicial>();
            CargaInicial vCurrentRecord = new Galac.Adm.Dal.GestionComprasCargaInicial();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.Codigo = "";
            vCurrentRecord.Fecha = LibDate.Today();
            vCurrentRecord.Existencia = 0;
            vCurrentRecord.Costo = 0;
            vCurrentRecord.CodigoArticulo = "";
            vCurrentRecord.EsCargaInicial = "";
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<CargaInicial> ParseToListEntity(XElement valXmlEntity) {
            List<CargaInicial> vResult = new List<CargaInicial>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                CargaInicial vRecord = new CargaInicial();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Codigo"), null))) {
                    vRecord.Codigo = vItem.Element("Codigo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Fecha"), null))) {
                    vRecord.Fecha = LibConvert.ToDate(vItem.Element("Fecha"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Existencia"), null))) {
                    vRecord.Existencia = LibConvert.ToDec(vItem.Element("Existencia"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Costo"), null))) {
                    vRecord.Costo = LibConvert.ToDec(vItem.Element("Costo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoArticulo"), null))) {
                    vRecord.CodigoArticulo = vItem.Element("CodigoArticulo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("EsCargaInicial"), null))) {
                    vRecord.EsCargaInicial = vItem.Element("EsCargaInicial").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo

        #region Miembros IServicioDeDatosCargaInicial
        public XElement ObtenerArticulosInsertarCargaInicial(int consecutivoCompania, string valMarca, DateTime valFechaInicial, string valCategoria, string valLinea,
                                                 string valCodigoDesde, string valCodigoHasta, string valWhere = null, string valOrderBy = null) {
            valFechaInicial = valFechaInicial.AddDays(-1);
            string vSqlUltimaCompra = "dbo.Gf_CompraUnaFecha( ArticuloInventario.ConsecutivoCompania " + valFechaInicial.ToShortDateString() + ", ArticuloInventario.Codigo)";
            string vSqlFechaUltimaCompra = "dbo.Gf_CompraUltimaFecha( ArticuloInventario.ConsecutivoCompania, " + valFechaInicial.ToShortDateString() + ", ArticuloInventario.Codigo)";
            LibGpParams vParametros = CrearParametrosWhere(consecutivoCompania, valMarca, valCategoria, valFechaInicial, valLinea, valCodigoDesde, valCodigoHasta);
            string vSQLSelect = ObtenerSelectArticulosInsertarCargaInicial(consecutivoCompania, valMarca, valCategoria, vSqlFechaUltimaCompra, valFechaInicial, vSqlFechaUltimaCompra, valLinea, valCodigoDesde, valCodigoHasta, vParametros, valWhere, valOrderBy);
            return LibBusiness.ExecuteSelect(vSQLSelect, vParametros.Get(), "", 0);
        }

        public XElement ActualizarRecordModificado(int valConsecutivoCompania, int valConsecutivo) {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("EXEC Adm.Gp_CargaInicialGETUPDRecord @ConsecutivoCompania, @Consecutivo");
            return LibBusiness.ExecuteSelect(vSQL.ToString(), ParametrosGetUpdatedRecord(valConsecutivo, valConsecutivoCompania), "", 0);
        }

        public XElement ObtenerArticulosModificarCargaInicial(int valConsecutivoCompania, string valMarca, DateTime valFechaInicial, string valCategoria, string valLinea, string valCodigoDesde, string valCodigoHasta) {
            var vParams = CrearParametrosWhere(valConsecutivoCompania, valMarca, valCategoria, valFechaInicial, valLinea, valCodigoDesde, valCodigoHasta);
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("SELECT carga.Consecutivo, carga.CodigoArticulo, carga.Existencia, carga.Costo, CAST(carga.fldTimeStamp AS bigint) AS fldTimeStampBigint FROM Adm.CargaInicial carga");
            vSQL.AppendLine("INNER JOIN dbo.ArticuloInventario art");
            vSQL.AppendLine("ON carga.CodigoArticulo = art.Codigo");
            vSQL.AppendLine(ObtenerWhereModificarCargaInicial(valConsecutivoCompania, valMarca, valCategoria, valLinea, valCodigoDesde, valCodigoHasta));
            return LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
        }

        public bool ExisteAlMenosUnArticulo(int consecutivoCompania) {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT TOP(1) Consecutivo FROM adm.CargaInicial");
            SQL.AppendLine("WHERE ConsecutivoCompania=" + consecutivoCompania.ToString());
            var vResult = LibBusiness.ExecuteSelect(SQL.ToString(), null, "", 0);
            if (vResult == null) return false;
            var vList = vResult.Descendants().Where(t => t.Name == "Consecutivo").Select(t => t.Value).ToList();
            return (vList.Count > 0);
        }

        public bool InsertarCargaDeArticulos(int valConsecutivoCompania, IEnumerable<ArticuloCargaInicial> cargaInicial, DateTime valFecha, bool valEsCargaInicial) {
            IList<CargaInicial> vListaDeCargaDeArticulos = RellenarListaDeCargaDeArticulos(valConsecutivoCompania, cargaInicial, valFecha, valEsCargaInicial);
            return new clsCargaInicialDat().InsertarCargaInicial(vListaDeCargaDeArticulos);
        }

        public bool ModificarCargaDeArticulos(int valConsecutivoCompania, IEnumerable<ArticuloCargaInicial> cargaDeArticulos, DateTime valFecha, bool valEsCargaInicial = false) {
            IList<CargaInicial> vArticulosACargar = RellenarListaDeCargaDeArticulos(valConsecutivoCompania, cargaDeArticulos, valFecha, valEsCargaInicial);
            return new clsCargaInicialDat().ModificarCargaInicial(vArticulosACargar);
        }
        #endregion

        #region Métodos Auxiliares
        private void ObtenerParametrosDeBusquedaAlInsertarCargaInicial(StringBuilder valXmlParamsExpression, out string valWhere, out string valOrderBy) {
            string vParams = valXmlParamsExpression.Replace("@", "").
                   Replace("dbo.Gv_ArticuloInventario_B1", "ArticuloInventario").
                   Replace("Gv_ArticuloInventario_B1.ConsecutivoCompania", "ArticuloInventario.ConsecutivoCompania").
                   Replace("Descripcion", "ArticuloInventario.Descripcion").
                   Replace("\"Costo\"", "\"CostoUnitario\"").
                   Replace("Costo ","CostoUnitario ").
                   Replace("Existencia","Cantidad").ToString();
            //if (vParams.Contains("Existencia DESC") || vParams.Contains("Existencia ASC") || vParams.Contains("Existencia")) {
            //    vParams = 
            //        vParams.Replace("Existencia DESC", "").
            //        Replace("Existencia ASC", "").
            //        Replace("Existencia", "").ToString();
            //}
            var vParametros = XElement.Parse(vParams).Descendants().Where(t => t.Name == "param");
            var vParametrosValores = vParametros.Attributes().Where(t => t.Name == "valor").Select(t => t.Value).ToList();
            valWhere = vParametrosValores[0].ToString();
            valOrderBy = vParametrosValores[1].ToString();
        }

        private string ObtenerSelectArticulosInsertarCargaInicial(int consecutivoCompania, string valMarca, string valCategoria, string vSqlFechaUltima,
                                       DateTime valFechaInicial, string vsqlFechaUltimaCompra, string valLinea, string valCodigoDesde, string valCodigoHasta,
                                       LibGpParams valParams, string valWhere, string valOrderBy) {

            StringBuilder vSQLSelect = new StringBuilder();
            vSQLSelect.AppendLine(" SELECT");
            vSQLSelect.AppendLine("     ArticuloInventario.Codigo AS CodigoArticulo, ");
            vSQLSelect.AppendLine("     ArticuloInventario.Descripcion AS Descripcion, ");
            vSQLSelect.AppendLine("     ArticuloInventario.LineaDeProducto AS LineaDeProducto, ");
            vSQLSelect.AppendLine("     SUM(dbo.IGV_ArticuloInvMovimiento.Entrada) - SUM(dbo.IGV_ArticuloInvMovimiento.Salidad) AS Cantidad,");
            vSQLSelect.AppendLine("     CASE WHEN(dbo.Gf_CompraUnaFecha(ArticuloInventario.ConsecutivoCompania, @FechaInicial , ArticuloInventario.Codigo) IS NULL)  THEN 0");
            vSQLSelect.AppendLine("     ELSE dbo.Gf_CompraUnaFecha(ArticuloInventario.ConsecutivoCompania, @FechaInicial, ArticuloInventario.Codigo)");
            vSQLSelect.AppendLine("     END");
            vSQLSelect.AppendLine("     AS CostoUnitario,0  AS CantidadRecibida, ArticuloInventario.ConsecutivoCompania AS ConsecutivoCompania, 0 AS NumeroSecuencialCompra,0  As ConsecutivoRenglon,");
            vSQLSelect.AppendLine("    IsNull(dbo.Gf_CompraUltimaFecha(ArticuloInventario.ConsecutivoCompania, " + vSqlFechaUltima + ", ArticuloInventario.Codigo), @FechaInicial)   As Fecha  ");
            vSQLSelect.AppendLine(" FROM");
            vSQLSelect.AppendLine("     ArticuloInventario");
            vSQLSelect.AppendLine("     INNER JOIN IGV_ArticuloInvMovimiento ON  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania");
            vSQLSelect.AppendLine("     AND  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo");
            vSQLSelect.AppendLine(ObtenerWhereArticulosInsertarCargaInicial(consecutivoCompania, valMarca, valCategoria, valFechaInicial, valLinea, valCodigoDesde, valCodigoHasta, valWhere));
            vSQLSelect.AppendLine(" Group By");
            vSQLSelect.AppendLine("     ArticuloInventario.ConsecutivoCompania, ");
            vSQLSelect.AppendLine("     ArticuloInventario.Codigo, ");
            vSQLSelect.AppendLine("     ArticuloInventario.Descripcion,");
            vSQLSelect.AppendLine("     ArticuloInventario.LineaDeProducto");
            if (!LibString.IsNullOrEmpty(valOrderBy)) {
                vSQLSelect.AppendLine("ORDER BY " + valOrderBy);
            }
            return vSQLSelect.ToString();
        }

        private string ObtenerWhereArticulosInsertarCargaInicial(int consecutivoCompania, string valMarca, string valCategoria,
                                        DateTime valFechaInicial, string valLinea, string valCodigoDesde, string valCodigoHasta, string valWhere) {
            StringBuilder vSqlWhere = new StringBuilder();
            vSqlWhere.AppendLine("WHERE");
            if (!LibString.IsNullOrEmpty(valWhere)) {
                vSqlWhere.AppendLine(valWhere);
            } else {
                vSqlWhere.AppendLine("ArticuloInventario.ConsecutivoCompania = @ConsecutivoCompania");
                if (!LibString.IsNullOrEmpty(valLinea)) {
                    vSqlWhere.AppendLine("AND ArticuloInventario.LineaDeProducto = @LineaDeProducto");
                }
                if (!LibString.IsNullOrEmpty(valMarca)) {
                    vSqlWhere.AppendLine("AND ArticuloInventario.Marca = @Marca");
                }
                if (!LibString.IsNullOrEmpty(valCategoria)) {
                    vSqlWhere.AppendLine("AND ArticuloInventario.Categoria = @Categoria");
                }
                if (!LibString.IsNullOrEmpty(valCodigoDesde) || !LibString.IsNullOrEmpty(valCodigoHasta)) {
                    if (!LibString.IsNullOrEmpty(valCodigoDesde) && !LibString.IsNullOrEmpty(valCodigoHasta)) {
                        vSqlWhere.AppendLine("AND ArticuloInventario.Codigo BETWEEN @CodigoDesde AND @CodigoHasta");
                    } else if (!LibString.IsNullOrEmpty(valCodigoDesde)) {
                        vSqlWhere.AppendLine("AND ArticuloInventario.Codigo >= @CodigoDesde");
                    } else if (!LibString.IsNullOrEmpty(valCodigoHasta)) {
                        vSqlWhere.AppendLine("AND ArticuloInventario.Codigo <= @CodigoHasta");
                    }
                }
            }
            vSqlWhere.AppendLine("AND ArticuloInventario.Codigo NOT IN ");
            vSqlWhere.AppendLine("(SELECT Adm.CargaInicial.CodigoArticulo FROM Adm.CargaInicial WHERE Adm.CargaInicial.ConsecutivoCompania = @ConsecutivoCompania)");
            vSqlWhere.AppendLine("AND ArticuloInventario.TipoDeArticulo= @TipoDeArticulo");
            vSqlWhere.AppendLine("AND ArticuloInventario.StatusDelArticulo= @StatusArticulo");
            vSqlWhere.AppendLine("AND IGV_ArticuloInvMovimiento.Fecha <= @FechaInicial");
            return vSqlWhere.ToString();
        }

        private LibGpParams CrearParametrosWhere(int consecutivoCompania, string valMarca, string valCategoria,
                       DateTime valFechaInicial, string valLinea, string valCodigoDesde, string valCodigoHasta) {
            LibGpParams parametros = new LibGpParams();
            AgregarParametroString(parametros, "Categoria", valCategoria);
            AgregarParametroString(parametros, "LineaDeProducto", valLinea);
            AgregarParametroString(parametros, "Marca", valMarca);
            AgregarParametroString(parametros, "CodigoDesde", valCodigoDesde, 20);
            AgregarParametroString(parametros, "CodigoHasta", valCodigoHasta, 20);
            parametros.AddInDateTime("FechaInicial", valFechaInicial);
            parametros.AddInInteger("ConsecutivoCompania", consecutivoCompania);
            parametros.AddInEnum("StatusArticulo", (int)eStatusArticulo.Vigente);
            parametros.AddInEnum("TipoDeArticulo", (int)eTipoDeArticulo.Mercancia);
            return parametros;
        }

        private string ObtenerWhereModificarCargaInicial(int valConsecutivoCompania, string valMarca, string valCategoria, string valLinea, string valCodigoDesde, string valCodigoHasta) {
            StringBuilder vSqlWhere = new StringBuilder();
            vSqlWhere.AppendLine("WHERE");
            vSqlWhere.AppendLine("art.ConsecutivoCompania=@ConsecutivoCompania");
            if (!LibString.IsNullOrEmpty(valLinea)) {
                vSqlWhere.AppendLine("AND art.LineaDeProducto=@LineaDeProducto");
            }
            if (!LibString.IsNullOrEmpty(valMarca)) {
                vSqlWhere.AppendLine("AND art.Marca=@Marca");
            }
            if (!LibString.IsNullOrEmpty(valCategoria)) {
                vSqlWhere.AppendLine("AND art.Categoria=@Categoria");
            }
            if (!LibString.IsNullOrEmpty(valCodigoDesde) || !LibString.IsNullOrEmpty(valCodigoHasta)) {
                if (!LibString.IsNullOrEmpty(valCodigoDesde) && !LibString.IsNullOrEmpty(valCodigoHasta)) {
                    vSqlWhere.AppendLine("AND carga.CodigoArticulo BETWEEN @CodigoDesde AND @CodigoHasta");
                } else if (!LibString.IsNullOrEmpty(valCodigoDesde)) {
                    vSqlWhere.AppendLine("AND carga.CodigoArticulo >= @CodigoDesde");
                } else if (!LibString.IsNullOrEmpty(valCodigoHasta)) {
                    vSqlWhere.AppendLine("AND carga.CodigoArticulo <= @CodigoHasta");
                }
            }
            return vSqlWhere.ToString();
        }

        private bool CargarListaDeArticulos(ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            StringBuilder SQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            int vConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
            vParams.AddInInteger("consecutivoCompania", vConsecutivoCompania);
            SQL.AppendLine("EXEC Adm.Gp_CargaInicialArticulosSCH @consecutivoCompania");
            refXmlDocument = LibXml.CreateXmlDocument(LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", 0));
            if (refXmlDocument != null && refXmlDocument.DocumentElement != null && refXmlDocument.DocumentElement.HasChildNodes) {
                vResult = true;
            }
            return vResult;
        }


        private void AgregarParametroString(LibGpParams parametros, string valNombre, string valValor, int valTamano = 50) {
            if (valValor != null) {
                parametros.AddInString(valNombre, valValor, valTamano);
            }
        }

        private IList<CargaInicial> RellenarListaDeCargaDeArticulos(int valConsecutivoCompania, IEnumerable<ArticuloCargaInicial> cargaDeArticulos, DateTime valFechaActual, bool valEsCargaInicial) {
            IList<CargaInicial> vArticulosCargaInicial = new List<CargaInicial>();
            foreach (var record in cargaDeArticulos) {
                vArticulosCargaInicial.Add(new CargaInicial() {
                    fldTimeStamp = record.articulo.fldTimeStamp,
                    Consecutivo = record.articulo.Consecutivo,
                    ConsecutivoCompania = valConsecutivoCompania,
                    EsCargaInicial = (valEsCargaInicial) ? "S" : "N",
                    CodigoArticulo = record.CodigoArticulo,
                    Existencia = record.Cantidad,
                    Costo = record.Costo,
                    TieneCambios = record.articulo.TieneCambios,
                    Fecha = (record.articulo.TieneCambios) ? valFechaActual : record.articulo.Fecha
                });
            }
            return vArticulosCargaInicial;
        }

        private StringBuilder ParametrosGetUpdatedRecord(int valConsecutivo, int valConsecutivoCompania) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("consecutivo", valConsecutivo);
            vParams.AddInInteger("consecutivoCompania", valConsecutivoCompania);
            return vParams.Get();
        }
        #endregion
    } //End of class clsCargaInicialNav

} //End of namespace Galac.Adm.Brl.GestionCompras

