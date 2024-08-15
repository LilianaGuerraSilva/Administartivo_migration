using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Dal;
using Galac.Saw.Lib;
using LibGalac.Aos.Base.Dal;
using System.Xml.Schema;
using System.Data.SqlTypes;
using LibGalac.Aos.Catching;

namespace Galac.Saw.Brl.Inventario {
    public partial class clsArticuloInventarioNav: LibBaseNavMaster<IList<ArticuloInventario>, IList<ArticuloInventario>>, ILibPdn, IArticuloInventarioPdn, ILookupDataService {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsArticuloInventarioNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataMasterComponentWithSearch<IList<ArticuloInventario>, IList<ArticuloInventario>> GetDataInstance() {
            return new Galac.Saw.Dal.Inventario.clsArticuloInventarioDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Inventario.clsArticuloInventarioDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Inventario.clsArticuloInventarioDat();
            switch (valCallingModule) {
                case "Compra":
                case "Orden De Compra":
                case "Lista de Materiales":
                case "Orden de Producción":
                case "Nota de Entrada/Salida":
                    return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_ArticuloInventarioCompraSCH", valXmlParamsExpression);
                case "Punto de Venta Ubicación":
                    return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_ArticuloInventarioUbicacionSCH", valXmlParamsExpression);
                default:
                    return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_ArticuloInventarioSCH", valXmlParamsExpression);
            }
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<ArticuloInventario>, IList<ArticuloInventario>> instanciaDal = new Galac.Saw.Dal.Inventario.clsArticuloInventarioDat();
            switch (valCallingModule) {
                case "Compra":
                case "OrdenDeCompra":
                case "Lista de Materiales":
                case "Orden de Producción":
                case "Lote De Inventario":
                    return instanciaDal.QueryInfo(eProcessMessageType.SpName, "dbo.Gp_ArticuloInventarioCompraGetFk", valParameters);
                case "Punto de Venta Ubicación":
                    return instanciaDal.QueryInfo(eProcessMessageType.SpName, "dbo.Gp_ArticuloInventarioUbicacionGetFk", valParameters);
                default:
                    return instanciaDal.QueryInfo(eProcessMessageType.SpName, "dbo.Gp_ArticuloInventarioGetFk", valParameters);
            }
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Articulo Inventario":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Línea de Producto":
                    vPdnModule = new Galac.Saw.Brl.Tablas.clsLineaDeProductoNav();
                    vResult = vPdnModule.GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Moneda":
                    vPdnModule = new Galac.Comun.Brl.TablasGen.clsMonedaNav();
                    vResult = vPdnModule.GetDataForList("Articulo Inventario", ref refXmlDocument, valXmlParamsExpression);
                    break;
                    //case "Categoria":
                    //    vPdnModule = new Galac.Saw.Brl.Inventario.clsCategoriaNav();
                    //    vResult = vPdnModule.GetDataForList("Articulo Inventario", ref refXmlDocument, valXmlParamsExpression);
                    //    break;
                    //case "Unidad De Venta":
                    //    vPdnModule = new Galac.Saw.Brl.Tablas.clsUnidadDeVentaNav();
                    //    vResult = vPdnModule.GetDataForList("Articulo Inventario", ref refXmlDocument, valXmlParamsExpression);
                    //    break;
                    //case "Articulo Inventario":
                    //    vPdnModule = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
                    //    vResult = vPdnModule.GetDataForList("Articulo Inventario", ref refXmlDocument, valXmlParamsExpression);
                    //    break;
                    //case "Grupo Talla Color":
                    //    vPdnModule = new Galac.dbo.Brl.ComponenteNoEspecificado.clsGrupoTallaColorNav();
                    //    vResult = vPdnModule.GetDataForList("Articulo Inventario", ref refXmlDocument, valXmlParamsExpression);
                    //    break;
                    //case "Talla":
                    //    vPdnModule = new Galac.Saw.Brl.Inventario.clsTallaNav();
                    //    vResult = vPdnModule.GetDataForList("Articulo Inventario", ref refXmlDocument, valXmlParamsExpression);
                    //    break;
                    //case "Color":
                    //    vPdnModule = new Galac.Saw.Brl.Inventario.clsColorNav();
                    //    vResult = vPdnModule.GetDataForList("Articulo Inventario", ref refXmlDocument, valXmlParamsExpression);
                    //    break;
                    //    default: throw new NotImplementedException();
            }
            return vResult;
        }

        private XElement FindInfoArticuloInventario(IList<ArticuloInventario> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (ArticuloInventario vItem in valData) {
                vXElement.Add(FilterProductoCompuestoByDistinctArticuloInventario(vItem).Descendants("GpResult"));
            }
            ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            XElement vXElementResult = insArticuloInventario.GetFk("ArticuloInventario", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterProductoCompuestoByDistinctArticuloInventario(ArticuloInventario valMaster) {
            return null;
        }

        private StringBuilder ParametersGetFKArticuloInventarioForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }
        #endregion //Metodos Generados     

        string CodigoArticuloResumenDiario(eTipoDeAlicuota valAlicuota) {
            string vCodigoArticuloResumidoDiario = "";
            switch (valAlicuota) {
                case eTipoDeAlicuota.Exento:
                    vCodigoArticuloResumidoDiario = "RD_AliExenta  @";
                    break;
                case eTipoDeAlicuota.AlicuotaGeneral:
                    vCodigoArticuloResumidoDiario = "RD_AliGeneral @";
                    break;
                case eTipoDeAlicuota.Alicuota2:
                    vCodigoArticuloResumidoDiario = "RD_AliReducida@";
                    break;
                case eTipoDeAlicuota.Alicuota3:
                    vCodigoArticuloResumidoDiario = "RD_AliExtendida";
                    break;
                case eTipoDeAlicuota.ExentoNC:
                    vCodigoArticuloResumidoDiario = "RD_AliExentaNC @";
                    break;
                case eTipoDeAlicuota.AlicuotaGeneralNC:
                    vCodigoArticuloResumidoDiario = "RD_AliGeneralNC @";
                    break;
                case eTipoDeAlicuota.Alicuota2NC:
                    vCodigoArticuloResumidoDiario = "RD_AliReducidaNC @";
                    break;
                case eTipoDeAlicuota.Alicuota3NC:
                    vCodigoArticuloResumidoDiario = "RD_AliExtendidaNC @";
                    break;
            }
            return vCodigoArticuloResumidoDiario;
        }

        string CodigoComisionXPorcentajeDeAlmacenaje() {
            string vResult = "";
            vResult = "RD_ComXPorcDeAlmacen @";
            return vResult;

        }

        bool UsaPrecioSinIvaSegunTipoDocumento(eTipoDocumentoFactura valTipoDocumentoFactura) {
            bool vResult = false;
            if (valTipoDocumentoFactura == eTipoDocumentoFactura.ResumenDiarioDeVentas) {
                vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "UsaPrecioSinIvaEnResumenVtas");
            } else {
                vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "UsaPrecioSinIva");
            }
            return vResult;
        }

        bool HayExistenciaParaLaSalidaSegunParametro(int valConsecutivoCompania, string valCodigoAlmacen, string valCodigoArticulo, string valSerial, string valRollo) {
            bool vResult = false;
            if (BuscarSiExisteElArticulo(valConsecutivoCompania, valCodigoArticulo, valSerial, valRollo)) {

            }
            return vResult;
        }

        bool BuscarSiExisteElArticulo(int valConsecutivoCompania, string valCodigoArticulo, string valSerial, string valRollo) {
            bool vResult = false;
            bool vFiltrarPorTipoArticuloInv = false;
            StringBuilder SQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);

            SQL.AppendLine("SELECT Codigo FROM articuloInventario");
            SQL.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine(" AND Codigo = @CodigoArticulo");
            if (vFiltrarPorTipoArticuloInv) {
                vParams.AddInEnum("TipoArticuloInv", (int)eTipoArticuloInv.Simple);
                SQL.AppendLine(" AND TipoArticuloInv = @TipoArticuloInv");
            }
            XElement xRecord = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", 0);
            if (xRecord != null) {
                var vEntity = 0;
                if (xRecord.IsEmpty) {
                    vEntity = (from vRecord in xRecord.Descendants("GpResult")
                               select vRecord).Count();
                }
                if (vEntity > 0) {
                    vResult = true;
                }
            }
            return vResult;
        }


        bool ExisteArticuloPorGrupo(int valConsecutivoCompania, string valCodigoArticulo, string valSerial, string valRollo) {
            bool vResult = false;
            bool vFiltrarPorTipoArticuloInv = false;
            StringBuilder SQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();

            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            if (vFiltrarPorTipoArticuloInv) {
                vParams.AddInEnum("TipoArticuloInv", (int)eTipoArticuloInv.Simple);
            }
            SQL.AppendLine("SELECT Serial, Rollo FROM ExistenciaPorGrupo");
            SQL.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine(" AND (ExistenciaPorGrupo.CodigoArticulo + ExistenciaPorGrupo.CodigoColor + ExistenciaPorGrupo.CodigoTalla) = @CodigoArticulo");
            if ((valSerial != "0") || (valRollo != "0")) {
                vParams.AddInString("Serial", valSerial, 50);
                vParams.AddInString("Rollo", valRollo, 30);
                SQL.AppendLine("AND ExistenciaPorGrupo.Serial = @Serial");
                SQL.AppendLine("AND ExistenciaPorGrupo.Rollo = @Rollo");
            }
            XElement xRecord = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", 0);
            if (xRecord != null) {
                vResult = true;
            }
            return vResult;
        }

        decimal IArticuloInventarioPdn.DisponibilidadDeArticulo(int valConsecutivoCompania, string valCodigoAlmacen, string valCodigoArticulo, int valTipoDeArticulo, string valSerial, string valRollo) {
            decimal vResult = 0;
            StringBuilder SQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            string vTabla = string.Empty;
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            vParams.AddInInteger("TipoArticuloInv", 1);
            ExisteArticuloPorGrupo(valConsecutivoCompania, valCodigoArticulo, valSerial, valRollo);

            SQL.AppendLine(" SELECT Codigo, CASE WHEN ArticuloInventario.TipoArticuloInv <> '0' THEN ISNULL( ExistenciaPorGrupo.Existencia  - ExistenciaPorGrupo.CantReservada,0) ELSE ISNULL(ArticuloInventario.Existencia - ArticuloInventario.CantArtReservado,0) END AS ");
            SQL.AppendLine(" Disponibilidad FROM ArticuloInventario LEFT JOIN ExistenciaPorGrupo ");
            SQL.AppendLine(" ON ArticuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania ");
            SQL.AppendLine(" AND ArticuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo ");
            SQL.AppendLine(" WHERE ArticuloInventario.codigo = @CodigoArticulo ");
            SQL.AppendLine(" AND ArticuloInventario.ConsecutivoCompania = @ConsecutivoCompania ");
            SQL.AppendLine(" AND ArticuloInventario.TipoArticuloInv <> @TipoArticuloInv ");

            XElement xRecord = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", 0);
            if (xRecord != null) {
                vResult = LibImportData.ToDec(LibXml.GetPropertyString(xRecord, "Disponibilidad"), 3);
            }
            return vResult;
        }

        XElement IArticuloInventarioPdn.BuscarDetalleArticuloCompuesto(int valConsecutivoCompania, string valCodigoArticulo) {
            StringBuilder SQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            string vTabla = string.Empty;
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            SQL.AppendLine(" SELECT AI2.Codigo, AI2.TipoArticuloInv, AI2.TipoDeArticulo, ProductoCompuesto.Cantidad ");
            SQL.AppendLine(" FROM ArticuloInventario AI1 ");
            SQL.AppendLine(" INNER JOIN ProductoCompuesto  ");
            SQL.AppendLine(" ON (AI1.ConsecutivoCompania = ProductoCompuesto.ConsecutivoCompania ");
            SQL.AppendLine(" AND AI1.Codigo = ProductoCompuesto.CodigoConexionConElMaster) ");
            SQL.AppendLine(" INNER JOIN ArticuloInventario AI2 ");
            SQL.AppendLine(" ON (AI2.ConsecutivoCompania = ProductoCompuesto.ConsecutivoCompania ");
            SQL.AppendLine(" AND AI2.Codigo = ProductoCompuesto.CodigoArticulo) ");
            SQL.AppendLine(" WHERE AI1.codigo = @CodigoArticulo ");
            SQL.AppendLine(" AND AI1.ConsecutivoCompania = @ConsecutivoCompania ");

            XElement xRecord = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", 0);
            return xRecord;
        }


        bool IArticuloInventarioPdn.ActualizarExistencia(int valConsecutivoCompania, List<ArticuloInventarioExistencia> valList) {
            StringBuilder vSQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            XElement vXmlArticulo = GenerarXmlArticulo(valList);
            XElement vData = BuscaInformacionDeArticulo(valConsecutivoCompania, vXmlArticulo);
            if (vData != null) {
                var vDataArticulos = vData.Descendants("GpResult").Select(p => new {
                    CodigoArticuloCompuesto = p.Element("CodigoCompuesto").Value,
                    TipoArticuloInv = (eTipoArticuloInv)LibConvert.DbValueToEnum(p.Element("TipoArticuloInv")),
                    CodigoGrupo = p.Element("CodigoGrupo").Value,
                    CodigoArticulo = p.Element("Codigo").Value,
                    CodigoTalla = ValidaNull(p.Element("CodigoTalla")),
                    CodigoColor = ValidaNull(p.Element("CodigoColor")),
                    TipoArticulo = (eTipoDeArticulo)LibConvert.DbValueToEnum(p.Element("TipoDeArticulo"))
                });
                foreach (var item in valList) {
                    var vArticulo = vDataArticulos.Where(p => p.CodigoArticulo == item.CodigoArticulo || p.CodigoArticuloCompuesto == item.CodigoArticulo).Select(p => p).FirstOrDefault();
                    if (item.TipoActualizacion == eTipoActualizacion.Existencia || item.TipoActualizacion == eTipoActualizacion.ExistenciayCosto) {
                        if (vArticulo.TipoArticulo != eTipoDeArticulo.Servicio) {
                            if (vArticulo.TipoArticulo != eTipoDeArticulo.Servicio) {
                                ActualizarExistenciaPorAlmacen(valConsecutivoCompania, item.CodigoAlmacen, item.CodigoArticulo, item.Cantidad, item.Ubicacion, item.ConsecutivoAlmacen);
                            }
                            if (vArticulo.TipoArticuloInv == eTipoArticuloInv.UsaTallaColor) {
                                ActualizarExistenciaPorGrupoTallaColor(valConsecutivoCompania, vArticulo.CodigoArticulo, vArticulo.CodigoGrupo, vArticulo.CodigoTalla, vArticulo.CodigoColor, item.Cantidad);
                            }
                            if (vArticulo.TipoArticuloInv == eTipoArticuloInv.UsaSerial || vArticulo.TipoArticuloInv == eTipoArticuloInv.UsaTallaColorySerial
                                || vArticulo.TipoArticuloInv == eTipoArticuloInv.UsaSerialRollo) {
                                ActualizarExistenciaPorGrupoSerial(valConsecutivoCompania, vArticulo.CodigoArticulo, vArticulo.CodigoGrupo, vArticulo.CodigoTalla, vArticulo.CodigoColor, item.DetalleArticuloInventarioExistenciaSerial, vArticulo.TipoArticuloInv);

                            }
                            if (vArticulo.TipoArticulo != eTipoDeArticulo.Servicio) {
                                ActualizarExistenciaArticuloInventario(valConsecutivoCompania, vArticulo.CodigoArticulo, item.Cantidad);
                            }
                        }
                    }
                    if (item.TipoActualizacion == eTipoActualizacion.ExistenciayCosto || item.TipoActualizacion == eTipoActualizacion.Costo) {
                        //TODO: Revisar parametro es moneda local del siguiente metodo.
                        ActualizarCostoUnitarioArticuloInventario(valConsecutivoCompania, vArticulo.CodigoArticulo, item.CostoUnitario, item.CostoUnitarioME, true);
                    }
                }
            }
            return true;
        }

        private void ActualizarExistenciaPorGrupoSerial(int valConsecutivoCompania, string valCodigoArticulo, string valCodigoGrupo, string valCodigoTalla, string valCodigoColor, List<ArticuloInventarioExistenciaSerial> valListArticuloInventarioExistenciaSerial, eTipoArticuloInv valTipoArticuloInv) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            vParams.AddInString("Serial", "0", 50);
            vParams.AddInString("Rollo", "0", 20);
            string vDefaultValue = string.Empty;
            if (valTipoArticuloInv == eTipoArticuloInv.UsaSerial || valTipoArticuloInv == eTipoArticuloInv.UsaSerialRollo) {
                vDefaultValue = " ";
            }
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("DELETE FROM ExistenciaPorGrupo  WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoArticulo = @CodigoArticulo AND Serial = @Serial AND Rollo = @Rollo");
            LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0);
            foreach (var itemSerial in valListArticuloInventarioExistenciaSerial) {
                vParams = new LibGpParams();
                vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
                vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
                vParams.AddInString("CodigoGrupo", valCodigoGrupo, 10);
                vParams.AddInString("CodigoTalla", (!LibString.IsNullOrEmpty(valCodigoTalla)) ? valCodigoTalla : vDefaultValue, 3);
                vParams.AddInString("CodigoColor", (!LibString.IsNullOrEmpty(valCodigoColor)) ? valCodigoColor : vDefaultValue, 3);
                vParams.AddInDecimal("Existencia", itemSerial.Cantidad, 4);
                vParams.AddInString("Serial", itemSerial.CodigoSerial, 50);
                vParams.AddInString("Rollo", itemSerial.CodigoRollo, 20);
                vParams.AddInDateTime("Fecha", LibDate.Today());
                vParams.AddInString("Ubicacion", " ", 100);
                vSQL = new StringBuilder();
                vSQL.AppendLine("UPDATE ExistenciaPorGrupo  SET Existencia = Existencia + @Existencia WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoArticulo = @CodigoArticulo AND Serial = @Serial AND Rollo = @Rollo");
                vSQL.AppendLine(" IF @@ROWCOUNT = 0");
                vSQL.AppendLine(" INSERT INTO ExistenciaPorGrupo(ConsecutivoCompania, CodigoArticulo, CodigoGrupo, CodigoTalla, CodigoColor, Existencia, Serial, Rollo, CantReservada, Fecha, Ubicacion)");
                vSQL.AppendLine(" VALUES( @ConsecutivoCompania, @CodigoArticulo, @CodigoGrupo, @CodigoTalla, @CodigoColor, @Existencia, @Serial, @Rollo, 0 , @Fecha, @Ubicacion)");
                LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0);
                ActualizarExistenciaRenglonExistenciaAlmacen(valConsecutivoCompania, itemSerial);
            }
        }

        private static void ActualizarExistenciaRenglonExistenciaAlmacen(int valConsecutivoCompania, ArticuloInventarioExistenciaSerial valArticuloInventarioExistenciaSerial) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoAlmacen", valArticuloInventarioExistenciaSerial.CodigoAlmacen, 5);
            vParams.AddInString("CodigoArticulo", valArticuloInventarioExistenciaSerial.CodigoArticulo, 30);
            vParams.AddInInteger("ConsecutivoRenglon", valArticuloInventarioExistenciaSerial.ConsecutivoRenglon);
            vParams.AddInString("CodigoSerial", valArticuloInventarioExistenciaSerial.CodigoSerial, 50);
            vParams.AddInString("CodigoRollo", valArticuloInventarioExistenciaSerial.CodigoRollo, 20);
            vParams.AddInDecimal("Cantidad", valArticuloInventarioExistenciaSerial.Cantidad, 4);
            vParams.AddInInteger("ConsecutivoAlmacen", valArticuloInventarioExistenciaSerial.ConsecutivoAlmacen);
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("UPDATE RenglonExistenciaAlmacen  SET Cantidad = Cantidad + @Cantidad WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoAlmacen = @ConsecutivoAlmacen AND CodigoArticulo = @CodigoArticulo AND CodigoSerial = @CodigoSerial AND CodigoRollo = @CodigoRollo");
            vSQL.AppendLine(" IF @@ROWCOUNT = 0");
            vSQL.AppendLine(" INSERT INTO RenglonExistenciaAlmacen(ConsecutivoCompania, CodigoAlmacen, CodigoArticulo, ConsecutivoRenglon, CodigoSerial, CodigoRollo , Cantidad ,ConsecutivoAlmacen)");
            vSQL.AppendLine(" VALUES(@ConsecutivoCompania, @CodigoAlmacen, @CodigoArticulo, @ConsecutivoRenglon, @CodigoSerial, @CodigoRollo , @Cantidad ,@ConsecutivoAlmacen)");
            LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0);
        }

        private void ActualizarExistenciaPorGrupoTallaColor(int valConsecutivoCompania, string valCodigoArticulo, string valCodigoGrupo, string valCodigoTalla, string valCodigoColor, decimal valCantidad) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            vParams.AddInString("CodigoGrupo", valCodigoGrupo, 10);
            vParams.AddInString("CodigoTalla", valCodigoTalla, 3);
            vParams.AddInString("CodigoColor", valCodigoColor, 3);
            vParams.AddInDecimal("Existencia", valCantidad, 4);
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("UPDATE ExistenciaPorGrupo  SET Existencia = Existencia + @Existencia WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoArticulo = @CodigoArticulo AND CodigoGrupo = @CodigoGrupo AND CodigoTalla = @CodigoTalla AND CodigoColor = @CodigoColor");
            LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0);
        }

        private static void ActualizarExistenciaPorAlmacen(int valConsecutivoCompania, string valCodigoAlmacen, string valCodigoArticulo, decimal valCantidad, string valUbicacion, int valConsecutivoAlmacen) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoAlmacen", valCodigoAlmacen, 5);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            vParams.AddInDecimal("Cantidad", valCantidad, 4);
            vParams.AddInString("Ubicacion", valUbicacion, 100);
            vParams.AddInInteger("ConsecutivoAlmacen", valConsecutivoAlmacen);
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("UPDATE  ExistenciaPorAlmacen SET Cantidad = Cantidad + @Cantidad WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoArticulo = @CodigoArticulo AND ConsecutivoAlmacen = @ConsecutivoAlmacen");
            vSQL.AppendLine(" IF @@ROWCOUNT = 0");
            vSQL.AppendLine(" INSERT INTO ExistenciaPorAlmacen(ConsecutivoCompania, CodigoAlmacen, CodigoArticulo, Cantidad, Ubicacion, ConsecutivoAlmacen)");
            vSQL.AppendLine(" VALUES( @ConsecutivoCompania, @CodigoAlmacen, @CodigoArticulo, @Cantidad, @Ubicacion, @ConsecutivoAlmacen)");
            LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0);
        }

        private XElement BuscaInformacionDeArticulo(int valConsecutivoCompania, XElement valXmlArticulo) {
            StringBuilder vSQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXmlArticulo);
            vSQL.AppendLine(" DECLARE @hdoc int ");
            vSQL.AppendLine(" EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlData ");
            vSQL.AppendLine("   SELECT ");
            vSQL.AppendLine("	 CodigoCompuesto  ");
            vSQL.AppendLine(", LineaDeProducto  ");
            vSQL.AppendLine(", CASE WHEN Gv_ArticuloInventario_B2.CodigoGrupo IS NULL OR LEN(Gv_ArticuloInventario_B2.CodigoGrupo) = 0 THEN '0' ELSE Gv_ArticuloInventario_B2.CodigoGrupo END AS  CodigoGrupo  ");
            vSQL.AppendLine(", TipoArticuloInv  ");
            vSQL.AppendLine(", Codigo  ");
            vSQL.AppendLine(", Gv_ArticuloInventario_B2.ConsecutivoCompania  ");
            vSQL.AppendLine(", StatusdelArticulo  ");
            vSQL.AppendLine(", TipoDeArticulo  ");
            vSQL.AppendLine(", RenglonGrupoColor.CodigoColor  ");
            vSQL.AppendLine(",RenglonGrupoTalla.CodigoTalla  ");
            vSQL.AppendLine("FROM  Gv_ArticuloInventario_B2  LEFT JOIN ");
            vSQL.AppendLine(" GrupoTallaColor  ON  Gv_ArticuloInventario_B2.ConsecutivoCompania = GrupoTallaColor.ConsecutivoCompania  ");
            vSQL.AppendLine("AND  Gv_ArticuloInventario_B2.CodigoGrupo = GrupoTallaColor.CodigoGrupo ");
            vSQL.AppendLine("LEFT JOIN  RenglonGrupoColor ON  GrupoTallaColor.ConsecutivoCompania = RenglonGrupoColor.ConsecutivoCompania AND GrupoTallaColor.CodigoGrupo = RenglonGrupoColor.CodigoGrupo  ");
            vSQL.AppendLine("LEFT JOIN  RenglonGrupoTalla  ON  GrupoTallaColor.ConsecutivoCompania = RenglonGrupoTalla.ConsecutivoCompania AND GrupoTallaColor.CodigoGrupo = RenglonGrupoTalla.CodigoGrupo  ");
            vSQL.AppendLine("      WHERE Gv_ArticuloInventario_B2.ConsecutivoCompania = @ConsecutivoCompania");
            vSQL.AppendLine("          AND CodigoCompuesto IN (");
            vSQL.AppendLine("            SELECT  Codigo ");
            vSQL.AppendLine("            FROM OPENXML( @hdoc, 'GpData/GpResult',2) ");
            vSQL.AppendLine("            WITH (Codigo varchar(30)) AS XmlDocOfFK) ");
            vSQL.AppendLine(" EXEC sp_xml_removedocument @hdoc");

            XElement vData = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            return vData;
        }

        private void ActualizarExistenciaArticuloInventario(int valConsecutivoCompania, string valCodigoArticulo, decimal valCantidad) {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine(" UPDATE dbo.ArticuloInventario SET Existencia = Existencia + @Existencia ");
            vSQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @CodigoArticulo");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInDecimal("Existencia", valCantidad, 4);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0);
        }

        private string ValidaNull(XElement xElement) {
            if (xElement != null) {
                return xElement.Value;
            } else {
                return "";
            }
        }

        private XElement GenerarXmlArticulo(List<ArticuloInventarioExistencia> valList) {
            XElement vResult = new XElement("GpData");
            foreach (ArticuloInventarioExistencia vItem in valList) {
                XElement vRow = new XElement("GpResult", new XElement("Codigo", vItem.CodigoArticulo));
                vResult.Add(vRow);
            }
            return vResult;
        }


        private XElement GenerarXmlArticulo(XElement valList) {
            XElement vResult = new XElement("GpData");
            var vData = valList.Descendants("GpResult");
            foreach (var vItem in vData) {
                XElement vRow = new XElement("GpResult", new XElement("Codigo", vItem.Element("CodigoArticulo").Value));
                vResult.Add(vRow);
            }
            return vResult;
        }

        bool IArticuloInventarioPdn.ActualizarCostoUnitario(int valConsecutivoCompania, XElement valDataArticulo, bool valEsMonedaLocal) {
            StringBuilder vSQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            XElement vXmlArticulo = GenerarXmlArticulo(valDataArticulo);
            XElement vData = BuscaInformacionDeArticulo(valConsecutivoCompania, vXmlArticulo);
            if (vData != null) {
                var vDataArticulos = vData.Descendants("GpResult").Select(p => new {
                    CodigoArticuloCompuesto = p.Element("CodigoCompuesto").Value,
                    CodigoArticulo = p.Element("Codigo").Value
                });
                var vDataXml = valDataArticulo.Descendants("GpResult").Select(p => new {
                    CodigoArticulo = p.Element("CodigoArticulo").Value,
                    CostoMonedaLocal = LibConvert.ToDec(p.Element("CostoMonedaLocal")),
                    CostoMonedaExtranjera = LibConvert.ToDec(p.Element("CostoMonedaExtranjera"))

                });

                foreach (var item in vDataXml) {
                    var vArticulo = vDataArticulos.Where(p => p.CodigoArticuloCompuesto == item.CodigoArticulo || p.CodigoArticulo == item.CodigoArticulo).Select(p => p).FirstOrDefault();
                    ActualizarCostoUnitarioArticuloInventario(valConsecutivoCompania, vArticulo.CodigoArticulo, item.CostoMonedaLocal, item.CostoMonedaExtranjera, valEsMonedaLocal);
                }
            }
            return true;
        }

        private void ActualizarCostoUnitarioArticuloInventario(int valConsecutivoCompania, string valCodigoArticulo, decimal valCostoUnitario, decimal valMeCostoUnitario, bool valEsMonedaLocal) {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine(" UPDATE dbo.ArticuloInventario SET CostoUnitario = @CostoUnitario ");
            vSQL.AppendLine(" ,MeCostoUnitario = @MeCostoUnitario ");
            vSQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @CodigoArticulo");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInDecimal("CostoUnitario", valCostoUnitario, 4);
            vParams.AddInDecimal("MeCostoUnitario", valMeCostoUnitario, 4);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0);
        }



        bool IArticuloInventarioPdn.ValidaExistenciaDeArticuloSerial(int valConsecutivoCompania, XElement valDataArticulo) {
            bool vResult = true;
            StringBuilder vSQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            XElement vXmlArticulo = GenerarXmlArticulo(valDataArticulo);
            XElement vData = BuscaInformacionDeArticulo(valConsecutivoCompania, vXmlArticulo);
            if (vData != null) {
                var vDataArticulos = vData.Descendants("GpResult").Select(p => new {
                    CodigoArticuloCompuesto = p.Element("CodigoCompuesto").Value,
                    TipoArticuloInv = (eTipoArticuloInv)LibConvert.DbValueToEnum(p.Element("TipoArticuloInv")),
                    CodigoGrupo = p.Element("CodigoGrupo").Value,
                    CodigoArticulo = p.Element("Codigo").Value,
                    CodigoTalla = ValidaNull(p.Element("CodigoTalla")),
                    CodigoColor = ValidaNull(p.Element("CodigoColor"))
                });
                var vDataXml = valDataArticulo.Descendants("GpResult").Select(p => new {
                    CodigoArticulo = p.Element("CodigoArticulo").Value,
                    Serial = p.Element("Serial").Value,
                    Rollo = p.Element("Rollo").Value
                });

                foreach (var item in vDataXml) {
                    var vArticulo = vDataArticulos.Where(p => p.CodigoArticuloCompuesto == item.CodigoArticulo || p.CodigoArticulo == item.CodigoArticulo).Select(p => p).FirstOrDefault();
                    if (!IsValidSerial(valConsecutivoCompania, vArticulo.CodigoArticulo, vArticulo.TipoArticuloInv, item.Serial, item.Rollo, vArticulo.CodigoColor, vArticulo.CodigoTalla)) {
                        if (vArticulo.TipoArticuloInv == eTipoArticuloInv.UsaSerial) {
                            throw new LibGalac.Aos.Catching.GalacValidationException("El " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "SinonimoSerial") + " " + item.Serial + "   Existe En Inventario");
                        }
                        /*else {
                            throw new LibGalac.Aos.Catching.GalacValidationException("El " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "SinonimoSerial") + " " + item.Serial + " y/o el " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "SinonimoRollo") + " " + item.Rollo + "   Existe En Inventario");
                        }*/
                    }
                }
            }
            return vResult;
        }

        private bool IsValidSerial(int valConsecutivoCompania, string valCodigoArticulo, eTipoArticuloInv valTipoArticuloInv, string valCodigoSerial, string valCodigoRollo, string valCodigoColor, string valCodigoTalla) {
            bool vResult = true;
            StringBuilder vSQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            vParams.AddInString("CodigoSerial", valCodigoSerial, 50);

            vSQL.AppendLine(" SELECT ExistenciaPorGrupo.Serial,Rollo FROM ExistenciaPorGrupo WHERE Serial  = @CodigoSerial  AND CodigoArticulo  = @CodigoArticulo");
            if (valTipoArticuloInv != eTipoArticuloInv.UsaSerial) {
                vSQL.AppendLine(" AND Rollo  = @CodigoRollo AND CodigoColor =@CodigoColor AND CodigoTalla =@CodigoTalla");
                vParams.AddInString("CodigoTalla", valCodigoTalla, 3);
                vParams.AddInString("CodigoColor", valCodigoColor, 3);
                vParams.AddInString("CodigoRollo", valCodigoRollo, 20);
            }
            vSQL.AppendLine(" AND ConsecutivoCompania = @ConsecutivoCompania ");
            XElement vData = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            if (vData != null && vData.Descendants("GpResult").Count() > 0) {
                vResult = false;
            }
            return vResult;
        }


        bool IArticuloInventarioPdn.AjustaPreciosxCostos(bool valFormulaAlternativa, int valConsecutivoCia, string valMarca, string valDesde, string valHasta, eRedondearPrecio valRedondeo, ePrecioAjustar valPrecioConOSinIVA, bool valMargenesNuevos, string valLineaProducto, string valCategoria, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4, decimal valMargen1, decimal valMargen2, decimal valMargen3, decimal valMargen4, bool valVieneDeCompras, DateTime valFechaOperacion, string valNumero, string valOperacion, bool valMonedaLocal) {
            bool vResult = false;
            if (valMargenesNuevos) {
                if (valVieneDeCompras) {

                    vResult = ContinuarConElCalculo(valFormulaAlternativa, valMargenesNuevos, valConsecutivoCia, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros", "PorcentajeAlicuota1"), LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros", "PorcentajeAlicuota2"), LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros", "PorcentajeAlicuota3"), valFechaOperacion, valNumero, valPrecio1, valPrecio2, valPrecio3, valPrecio4, valMargen1, valMargen2, valMargen3, valMargen4, valMonedaLocal);
                } else {
                    ActualizarPreciosConMargenesNuevos(valFormulaAlternativa, (Galac.Saw.Ccl.Inventario.eTipoDeMetodoDeCosteo)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "MetodoDeCosteo") == Ccl.Inventario.eTipoDeMetodoDeCosteo.CostoPromedio, valDesde, valHasta, valConsecutivoCia, valMarca, valMargen1, valMargen2, valMargen3, valMargen4, valPrecio1, valPrecio2, valPrecio3, valPrecio4, valLineaProducto, valCategoria);
                    vResult = true;
                }
            } else if (!valMargenesNuevos) {
                if (valVieneDeCompras) {
                    vResult = ContinuarConElCalculo(valFormulaAlternativa, valMargenesNuevos, valConsecutivoCia, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros", "PorcentajeAlicuota1"), LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros", "PorcentajeAlicuota2"), LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros", "PorcentajeAlicuota3"), valFechaOperacion, valNumero, valPrecio1, valPrecio2, valPrecio3, valPrecio4, valMargen1, valMargen2, valMargen3, valMargen4, valMonedaLocal);
                } else {
                    ActualizarPreciosSinMargenesNuevos(valFormulaAlternativa, (Galac.Saw.Ccl.Inventario.eTipoDeMetodoDeCosteo)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "MetodoDeCosteo") == Ccl.Inventario.eTipoDeMetodoDeCosteo.CostoPromedio, valDesde, valHasta, valConsecutivoCia, valMarca, valPrecio1, valPrecio2, valPrecio3, valPrecio4, valLineaProducto, valCategoria);
                    vResult = true;
                }
            }
            if (valRedondeo != eRedondearPrecio.SinRedondear) {
                RealizaRedondeoSegunElCaso(valRedondeo, valPrecioConOSinIVA, valPrecio1, valPrecio2, valPrecio3, valPrecio4, valConsecutivoCia, valVieneDeCompras, (Galac.Saw.Ccl.Inventario.eTipoDeMetodoDeCosteo)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "MetodoDeCosteo"), valDesde, valHasta, valFechaOperacion, valNumero, valOperacion);
                vResult = true;
            }
            return vResult;
        }

        private void RealizaRedondeoSegunElCaso(eRedondearPrecio valRedondeo, ePrecioAjustar valPrecioConOSinIVA, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4, int valConsecutivoCompania, bool valVieneDeCompra, Galac.Saw.Ccl.Inventario.eTipoDeMetodoDeCosteo valTipoDeMetodoDeCosteo, string valDesde, string valHasta, DateTime valFechaOperacion, string valDocumento, string valOperacion) {
            decimal PrecioConIVA;
            decimal PrecioConIVA2;
            decimal PrecioConIVA3;
            decimal PrecioConIVA4;
            decimal PrecioSinIVA;
            decimal PrecioSinIVA2;
            decimal PrecioSinIVA3;
            decimal PrecioSinIVA4;
            XElement vData = ArticulosParaAplicarRedondeo(valConsecutivoCompania, valVieneDeCompra, valTipoDeMetodoDeCosteo, valDesde, valHasta, valFechaOperacion, valDocumento, valOperacion);
            decimal PrecioAAjustar = 0;
            if (vData != null) {
                var vDataArticulo = vData.Descendants("GpResult")
                    .Select(p => new {
                        ConsecutivoCompania = LibConvert.ToInt(p.Element("ConsecutivoCompania")),
                        CodigoArticulo = p.Element("Codigo").Value,
                        PrecioConIva = LibConvert.ToDec(p.Element("PrecioConIva")),
                        PrecioSinIva = LibConvert.ToDec(p.Element("PrecioSinIva")),
                        PrecioConIva2 = LibConvert.ToDec(p.Element("PrecioConIva2")),
                        PrecioSinIva2 = LibConvert.ToDec(p.Element("PrecioSinIva2")),
                        PrecioConIva3 = LibConvert.ToDec(p.Element("PrecioConIva3")),
                        PrecioSinIva3 = LibConvert.ToDec(p.Element("PrecioSinIva3")),
                        PrecioConIva4 = LibConvert.ToDec(p.Element("PrecioConIva4")),
                        PrecioSinIva4 = LibConvert.ToDec(p.Element("PrecioSinIva4")),
                        PorcentajeBaseImponible = LibConvert.ToDec(p.Element("PorcentajeBaseImponible")),
                        AlicuotaIVA = (eTipoDeAlicuota)LibConvert.DbValueToEnum(p.Element("AlicuotaIVA"))
                    });

                foreach (var item in vDataArticulo) {
                    if (valPrecioConOSinIVA == ePrecioAjustar.PrecioConIVA) {
                        if (valPrecio1) {
                            PrecioAAjustar = item.PrecioConIva;
                            PrecioAAjustar = ValidaPrecioConIva(PrecioAAjustar, item.PrecioSinIva, item.PorcentajeBaseImponible, item.AlicuotaIVA);
                            PrecioAAjustar = RedondearPrecio(valRedondeo, PrecioAAjustar);
                            PrecioConIVA = PrecioAAjustar;
                            PrecioSinIVA = CalculaElPrecioSinIVAAPartirDelPrecioConIVA(PrecioAAjustar, item.PorcentajeBaseImponible, LibDate.Today(), item.AlicuotaIVA);
                        }
                        if (valPrecio2) {
                            PrecioAAjustar = item.PrecioConIva2;
                            PrecioAAjustar = RedondearPrecio(valRedondeo, PrecioAAjustar);
                            PrecioConIVA2 = PrecioAAjustar;
                            PrecioSinIVA2 = CalculaElPrecioSinIVAAPartirDelPrecioConIVA(PrecioAAjustar, item.PorcentajeBaseImponible, LibDate.Today(), item.AlicuotaIVA);
                        }
                        if (valPrecio3) {
                            PrecioAAjustar = item.PrecioConIva3;
                            PrecioAAjustar = RedondearPrecio(valRedondeo, PrecioAAjustar);
                            PrecioConIVA3 = PrecioAAjustar;
                            PrecioSinIVA3 = CalculaElPrecioSinIVAAPartirDelPrecioConIVA(PrecioAAjustar, item.PorcentajeBaseImponible, LibDate.Today(), item.AlicuotaIVA);
                        }
                        if (valPrecio4) {
                            PrecioAAjustar = item.PrecioConIva4;
                            PrecioAAjustar = RedondearPrecio(valRedondeo, PrecioAAjustar);
                            PrecioConIVA4 = PrecioAAjustar;
                            PrecioSinIVA4 = CalculaElPrecioSinIVAAPartirDelPrecioConIVA(PrecioAAjustar, item.PorcentajeBaseImponible, LibDate.Today(), item.AlicuotaIVA);
                        }
                    } else {
                        if (valPrecio1) {
                            PrecioAAjustar = item.PrecioSinIva;
                            PrecioAAjustar = ValidaPrecioSinIva(PrecioAAjustar, item.PrecioConIva, item.PorcentajeBaseImponible, item.AlicuotaIVA);
                            PrecioAAjustar = RedondearPrecio(valRedondeo, PrecioAAjustar);
                            PrecioSinIVA = PrecioAAjustar;
                            PrecioConIVA = CalculaElPrecioConIVAAPartirDelPrecioSinIVA(PrecioAAjustar, item.PorcentajeBaseImponible, LibDate.Today(), item.AlicuotaIVA);
                        }
                        if (valPrecio2) {
                            PrecioAAjustar = item.PrecioSinIva2;
                            PrecioAAjustar = RedondearPrecio(valRedondeo, PrecioAAjustar);
                            PrecioSinIVA2 = PrecioAAjustar;
                            PrecioConIVA2 = CalculaElPrecioConIVAAPartirDelPrecioSinIVA(PrecioAAjustar, item.PorcentajeBaseImponible, LibDate.Today(), item.AlicuotaIVA);
                        }
                        if (valPrecio3) {
                            PrecioAAjustar = item.PrecioSinIva3;
                            PrecioAAjustar = RedondearPrecio(valRedondeo, PrecioAAjustar);
                            PrecioSinIVA3 = PrecioAAjustar;
                            PrecioConIVA3 = CalculaElPrecioConIVAAPartirDelPrecioSinIVA(PrecioAAjustar, item.PorcentajeBaseImponible, LibDate.Today(), item.AlicuotaIVA);
                        }
                        if (valPrecio4) {
                            PrecioAAjustar = item.PrecioSinIva4;
                            PrecioAAjustar = RedondearPrecio(valRedondeo, PrecioAAjustar);
                            PrecioSinIVA4 = PrecioAAjustar;
                            PrecioConIVA4 = CalculaElPrecioConIVAAPartirDelPrecioSinIVA(PrecioAAjustar, item.PorcentajeBaseImponible, LibDate.Today(), item.AlicuotaIVA);
                        }
                    }
                    ActualizaElRegistroPorRedondeo(item.ConsecutivoCompania, item.CodigoArticulo, item.PrecioConIva, item.PrecioSinIva, item.PrecioConIva2, item.PrecioSinIva2, item.PrecioConIva3, item.PrecioSinIva3, item.PrecioConIva4, item.PrecioSinIva4);
                }

            }
        }

        private XElement ArticulosParaAplicarRedondeo(int valConsecutivoCompania, bool valVieneDeCompra, Galac.Saw.Ccl.Inventario.eTipoDeMetodoDeCosteo valTipoDeMetodoDeCosteo, string valDesde, string valHasta, DateTime valFechaOperacion, string valDocumento, string valOperacion) {
            XElement vConjuntoArticulo = new XElement("GpData");
            if (valVieneDeCompra && valTipoDeMetodoDeCosteo == Ccl.Inventario.eTipoDeMetodoDeCosteo.CostoPromedio) {
                vConjuntoArticulo = CodigoArticuloDeOperaciones(valConsecutivoCompania, valFechaOperacion, valDocumento, valOperacion);
            } else {
                vConjuntoArticulo.Add(new XElement("GpResult", new XElement("Codigo", valDesde)));
                vConjuntoArticulo.Add(new XElement("GpResult", new XElement("Codigo", valDesde)));

            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", vConjuntoArticulo);
            string vSQL = SqlArticulosAjustarCosto();
            XElement vResult = LibBusiness.ExecuteSelect(vSQL, vParams.Get(), "", 0);
            return vResult;

        }

        private XElement CodigoArticuloDeOperaciones(int valConsecutivoCompania, DateTime valFechaOperacion, string valDocumento, string valOperacion) {
            XElement vResult = null;
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInEnum("StatusdelArticulo", (int)eStatusArticulo.Vigente);
            vParams.AddInEnum("TipoDeArticulo", (int)eTipoDeArticulo.Mercancia);
            vParams.AddInString("TipoDeDocumento", valOperacion, 34);
            vParams.AddInString("NumeroDocumento", valDocumento, 30);
            vParams.AddInDateTime("Fecha", valFechaOperacion);
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine(" SELECT articuloInventario.codigo ");
            vSQL.AppendLine(" FROM   articuloInventario");
            vSQL.AppendLine(" INNER JOIN  IGV_ArticuloInvMovimiento ON ");
            vSQL.AppendLine("    ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND ");
            vSQL.AppendLine("    ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo ");
            vSQL.AppendLine("    WHERE articuloInventario.ConsecutivoCompania = @ConsecutivoCompania ");
            vSQL.AppendLine("    AND articuloInventario.StatusdelArticulo = @StatusdelArticulo ");
            vSQL.AppendLine("    AND articuloInventario.TipoDeArticulo = @TipoDeArticulo ");
            vSQL.AppendLine("    AND IGV_ArticuloInvMovimiento.TipoDeDocumento = @TipoDeDocumento ");
            vSQL.AppendLine("    AND IGV_ArticuloInvMovimiento.NumeroDocumento = @NumeroDocumento ");
            vSQL.AppendLine("    AND IGV_ArticuloInvMovimiento.Fecha = @Fecha ");
            vResult = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            return vResult;
            throw new NotImplementedException();
        }
        private string SqlArticulosAjustarCosto() {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine(" DECLARE @hdoc int ");
            vSQL.AppendLine(" EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlData ");
            vSQL.AppendLine(" SELECT * FROM articuloInventario");
            vSQL.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania");
            vSQL.AppendLine(" AND  CODIGO IN ( ");
            vSQL.AppendLine("     SELECT  Codigo ");
            vSQL.AppendLine("     FROM OPENXML( @hdoc, 'GpData/GpResult',2) ");
            vSQL.AppendLine("     WITH (Codigo varchar(15)) AS XmlDocOfFK) ");
            vSQL.AppendLine(" EXEC sp_xml_removedocument @hdoc");
            return vSQL.ToString();
        }


        private void ActualizaElRegistroPorRedondeo(int valConsecutivoCompania, string valCodigoArticulo, decimal valPrecioConIva, decimal valPrecioSinIva, decimal valPrecioConIva2, decimal valPrecioSinIva2, decimal valPrecioConIva3, decimal valPrecioSinIva3, decimal valPrecioConIva4, decimal valPrecioSinIva4) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            vParams.AddInDecimal("PrecioConIva", valPrecioConIva, 2);
            vParams.AddInDecimal("PrecioSinIva", valPrecioSinIva, 2);
            vParams.AddInDecimal("PrecioConIva2", valPrecioConIva2, 2);
            vParams.AddInDecimal("PrecioSinIva2", valPrecioSinIva2, 2);
            vParams.AddInDecimal("PrecioConIva3", valPrecioConIva3, 2);
            vParams.AddInDecimal("PrecioSinIva3", valPrecioSinIva3, 2);
            vParams.AddInDecimal("PrecioConIva4", valPrecioConIva4, 2);
            vParams.AddInDecimal("PrecioSinIva4", valPrecioSinIva4, 2);
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("UPDATE ArticuloInventario SET PrecioConIva = @PrecioConIva, PrecioSinIva = @PrecioSinIva");
            vSQL.AppendLine(",PrecioConIva2 = @PrecioConIva2, PrecioSinIva2 = @PrecioSinIva2");
            vSQL.AppendLine(",PrecioConIva3 = @PrecioConIva3, PrecioSinIva3 = @PrecioSinIva3");
            vSQL.AppendLine(",PrecioConIva4 = @PrecioConIva4, PrecioSinIva4 = @PrecioSinIva4");
            vSQL.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @CodigoArticulo");
            LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0);
        }

        private decimal ValidaPrecioSinIva(decimal valPrecioSinIVA, decimal valPrecioConIVA, decimal valPorcentajeBaseImponible, eTipoDeAlicuota valTipoDeAlicuota) {
            decimal vResult = CalculaElPrecioSinIVAAPartirDelPrecioConIVA(valPrecioConIVA, valPorcentajeBaseImponible, LibDate.Today(), valTipoDeAlicuota);
            if (valPrecioSinIVA != vResult && vResult > valPrecioSinIVA) {
                return vResult;
            } else {
                return valPrecioSinIVA;
            }
        }



        private decimal RedondearPrecio(eRedondearPrecio valRedondeo, decimal valPrecioAAjustar) {
            switch (valRedondeo) {
                case eRedondearPrecio.ProximaCentena:
                    return RedondearACentena(valPrecioAAjustar);
                case eRedondearPrecio.ProximaDecena:
                    return RedondearADecena(valPrecioAAjustar);
                case eRedondearPrecio.ProximaUnidad:
                    return RedondearAUnidad(valPrecioAAjustar);
                case eRedondearPrecio.ProximaUnidadDeMil:
                    return RedondearAUnidadDeMil(valPrecioAAjustar);
                case eRedondearPrecio.SinRedondear:
                    return valPrecioAAjustar;
                default:
                    return valPrecioAAjustar;
            }
        }

        private decimal RedondearAUnidadDeMil(decimal valPrecioAAjustar) {
            decimal vAuxiliar = RedondearACentena(valPrecioAAjustar);
            if (vAuxiliar > 1000) {
                string vAuxiliar2 = LibString.Right(LibConvert.ToStr(vAuxiliar), 3);
                if (LibConvert.ToInt(vAuxiliar2) > 0) {
                    return vAuxiliar + (1000 - LibConvert.ToInt(vAuxiliar2));
                } else {
                    return vAuxiliar;
                }
            } else {
                return vAuxiliar;
            }
        }

        private decimal RedondearACentena(decimal valPrecioAAjustar) {
            decimal vAuxiliar = RedondearADecena(valPrecioAAjustar);
            if (vAuxiliar > 100) {
                string vAuxiliar2 = LibString.Right(LibConvert.ToStr(vAuxiliar), 2);
                if (LibConvert.ToInt(vAuxiliar2) > 0) {
                    return vAuxiliar + (100 - LibConvert.ToInt(vAuxiliar2));
                } else {
                    return vAuxiliar;
                }
            } else {
                return vAuxiliar;
            }
        }

        private decimal RedondearADecena(decimal valPrecioAAjustar) {
            decimal vAuxiliar = RedondearAUnidad(valPrecioAAjustar);
            if (vAuxiliar > 10) {
                string vAuxiliar2 = LibString.Right(LibConvert.ToStr(vAuxiliar), 1);
                if (LibConvert.ToInt(vAuxiliar2) > 0) {
                    return vAuxiliar + (10 - LibConvert.ToInt(vAuxiliar2));
                } else {
                    return vAuxiliar;
                }
            } else {
                return vAuxiliar;
            }
        }

        private decimal RedondearAUnidad(decimal valPrecioAAjustar) {
            decimal vAuxiliar = valPrecioAAjustar - Math.Truncate(valPrecioAAjustar);
            if (vAuxiliar > 0) {
                return Math.Truncate(valPrecioAAjustar) + 1;
            } else {
                return valPrecioAAjustar;
            }
        }

        private decimal ValidaPrecioConIva(decimal valPrecioConIVA, decimal valPrecioSinIVA, decimal valPorcentajeBaseImponible, eTipoDeAlicuota valTipoDeAlicuota) {
            decimal vResult;
            vResult = CalculaElPrecioConIVAAPartirDelPrecioSinIVA(valPrecioSinIVA, valPorcentajeBaseImponible, LibDate.Today(), valTipoDeAlicuota);
            if (valPrecioConIVA != vResult && vResult > valPrecioConIVA) {
                return vResult;
            } else {
                return valPrecioConIVA;
            }
        }

        private decimal CalculaElPrecioConIVAAPartirDelPrecioSinIVA(decimal valPrecioSinIVA, decimal valPorcentajeBaseImponible, DateTime valFecha, eTipoDeAlicuota valTipoDeAlicuota) {
            if (valTipoDeAlicuota == eTipoDeAlicuota.Exento) {
                return valPrecioSinIVA;
            } else {
                decimal vMontoImponible = valPrecioSinIVA * valPorcentajeBaseImponible / 100;
                if (LibDate.F1IsLessOrEqualThanF2(valFecha, new DateTime(2003, 1, 1))) {
                    return LibMath.RoundToNDecimals(valPrecioSinIVA + (vMontoImponible * GetAlicuotaIVA(valFecha, eTipoDeAlicuota.AlicuotaGeneral) / 100), LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales"));
                } else {
                    return LibMath.RoundToNDecimals(valPrecioSinIVA + (vMontoImponible * GetAlicuotaIVA(valFecha, valTipoDeAlicuota) / 100), LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales"));
                }
            }
        }

        private decimal CalculaElPrecioSinIVAAPartirDelPrecioConIVA(decimal valPrecioConIVA, decimal valPorcentajeDeLaBaseImponible, DateTime valFecha, eTipoDeAlicuota valTipoDeAlicuota) {
            if (LibDate.F1IsLessOrEqualThanF2(valFecha, new DateTime(2003, 1, 1))) {
                return LibMath.RoundToNDecimals(valPrecioConIVA / (1 + (valPorcentajeDeLaBaseImponible * GetAlicuotaIVA(valFecha, eTipoDeAlicuota.AlicuotaGeneral))), LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales"));
            } else {
                return LibMath.RoundToNDecimals(valPrecioConIVA / (1 + (valPorcentajeDeLaBaseImponible * GetAlicuotaIVA(valFecha, valTipoDeAlicuota))), LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales"));
            }
        }

        XElement vDataAlicuota = null;
        private decimal GetAlicuotaIVA(DateTime valFecha, eTipoDeAlicuota valTipoDeAlicuota) {
            if (vDataAlicuota == null) {
                vDataAlicuota = LibBusiness.ExecuteSelect("SELECT  FechaDeInicioDeVigencia,MontoAlicuotaGeneral,MontoAlicuota2,MontoAlicuota3 FROM dbo.alicuotaIVA", null, "", 0);
            }
            var vData = vDataAlicuota.Descendants("GpResult")
                .Select(p => new {
                    FechaDeInicioDeVigencia = LibConvert.ToDate(p.Element("FechaDeInicioDeVigencia")),
                    MontoAlicuotaGeneral = LibConvert.ToDec(p.Element("MontoAlicuotaGeneral")),
                    MontoAlicuota2 = LibConvert.ToDec(p.Element("MontoAlicuota2")),
                    MontoAlicuota3 = LibConvert.ToDec(p.Element("MontoAlicuota3"))
                }).Where(p => p.FechaDeInicioDeVigencia <= valFecha)
                .OrderByDescending(p => p.FechaDeInicioDeVigencia);
            if (vData != null) {
                switch (valTipoDeAlicuota) {
                    case eTipoDeAlicuota.Alicuota2:
                        return vData.FirstOrDefault().MontoAlicuota2;
                    case eTipoDeAlicuota.Alicuota3:
                        return vData.FirstOrDefault().MontoAlicuota3;
                    case eTipoDeAlicuota.AlicuotaGeneral:
                        return vData.FirstOrDefault().MontoAlicuotaGeneral;
                    default:
                        break;
                }
            }
            return 0;
        }



        private void ActualizarPreciosSinMargenesNuevos(bool valUsarNuevaFormula, bool valUsaCostoPromedio, string valDesde, string valHasta, int valConsecutivoCia, string valMarca, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4, string valLineaProducto, string valCategoria) {
            string SQLNameColum = "CostoUnitario";
            string vAumento;
            string vSQLAlicuotasVigentes = SQLAlicuotasVigentes();
            if (valUsaCostoPromedio) {
                SQLNameColum = "CostoPromedio";
            }
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine(" UPDATE  articuloInventario  SET ");
            if (valPrecio1) {
                vAumento = FormulaAumentoSinMargenesNuevos(SQLNameColum, "MargenGanancia", valUsarNuevaFormula);
                vSQL.AppendLine(" PrecioSinIva  = " + vAumento);
                vSQL.AppendLine(", PrecioConIva  = " + vAumento + " ( " + vAumento + " * " + vSQLAlicuotasVigentes + ") / 100");
                if (valPrecio2 || valPrecio3 || valPrecio4) {
                    vSQL.AppendLine(",");
                }
            }
            if (valPrecio2) {
                vAumento = FormulaAumentoSinMargenesNuevos(SQLNameColum, "MargenGanancia2", valUsarNuevaFormula);
                vSQL.AppendLine(" PrecioSinIva2  = " + vAumento);
                vSQL.AppendLine(", PrecioConIva2  = " + vAumento + " ( " + vAumento + " * " + vSQLAlicuotasVigentes + ") / 100");
                if (valPrecio3 || valPrecio4) {
                    vSQL.AppendLine(",");
                }
            }
            if (valPrecio3) {
                vAumento = FormulaAumentoSinMargenesNuevos(SQLNameColum, "MargenGanancia3", valUsarNuevaFormula);
                vSQL.AppendLine(" PrecioSinIva3  = " + vAumento);
                vSQL.AppendLine(", PrecioConIva3  = " + vAumento + " ( " + vAumento + " * " + vSQLAlicuotasVigentes + ") / 100");
                if (valPrecio4) {
                    vSQL.AppendLine(",");
                }
            }
            if (valPrecio4) {
                vAumento = FormulaAumentoSinMargenesNuevos(SQLNameColum, "MargenGanancia4", valUsarNuevaFormula);
                vSQL.AppendLine(" PrecioSinIva4  = " + vAumento);
                vSQL.AppendLine(", PrecioConIva4  = " + vAumento + " ( " + vAumento + " * " + vSQLAlicuotasVigentes + ") / 100");
            }
            LibGpParams vParams = new LibGpParams();
            vSQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            vSQL.AppendLine("AND  articuloInventario.codigo between  @Desde AND @Hasta");
            vSQL.AppendLine("AND  (articuloInventario.tipodearticulo = '0' OR    articuloInventario.tipodearticulo = '2')");
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCia);
            vParams.AddInString("Desde", valDesde, 30);
            vParams.AddInString("Hasta", valHasta, 30);
            if (!LibString.IsNullOrEmpty(valLineaProducto)) {
                vSQL.AppendLine("AND  LineaProducto = @LineaProducto");
                vParams.AddInString("LineaProducto", valLineaProducto, 20);
            }
            if (!LibString.IsNullOrEmpty(valCategoria)) {
                vSQL.AppendLine("AND  Categoria = @Categoria");
                vParams.AddInString("Categoria", valCategoria, 20);
            }
            if (!LibString.IsNullOrEmpty(valCategoria)) {
                vSQL.AppendLine("AND  marca = @marca");
                vParams.AddInString("marca", valMarca, 30);
            }
            LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0);
        }

        private string SQLAlicuotasVigentes() {
            decimal valAlicuota1 = GetAlicuotaIVA(LibDate.Today(), eTipoDeAlicuota.AlicuotaGeneral);
            decimal valAlicuota2 = GetAlicuotaIVA(LibDate.Today(), eTipoDeAlicuota.Alicuota2);
            decimal valAlicuota3 = GetAlicuotaIVA(LibDate.Today(), eTipoDeAlicuota.Alicuota3);
            string SQL = " (CASE WHEN alicuotaiva = 1 THEN " + valAlicuota1 + " WHEN alicuotaiva = 2 THEN " + valAlicuota2 + " WHEN alicuotaiva = 3 THEN " + valAlicuota3 + " ELSE " + valAlicuota1 + " END)";
            return SQL;

        }

        private string FormulaAumentoSinMargenesNuevos(string valNameColumna, string valMargen, bool valUsarNuevaFormula) {
            string vResult;
            if (valUsarNuevaFormula) {
                vResult = " (1 / (1 - (" + valMargen + " / 100  )))";
                vResult = "(" + valNameColumna + "*" + vResult + ")";
            } else {
                vResult = "(((" + valNameColumna + "  * " + valMargen + "  ) / 100) + " + valNameColumna + " )";
            }
            return vResult;
        }

        private void ActualizarPreciosConMargenesNuevos(bool valUsarNuevaFormula, bool valUsaCostoPromedio, string valDesde, string valHasta, int valConsecutivoCia, string valMarca, decimal valMargen1, decimal valMargen2, decimal valMargen3, decimal valMargen4, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4, string valLineaProducto, string valCategoria) {
            string SQLNameColum = "CostoUnitario";
            string vAumento;
            string vSQLAlicuotasVigentes = SQLAlicuotasVigentes();
            if (valUsaCostoPromedio) {
                SQLNameColum = "CostoPromedio";
            }
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine(" UPDATE  articuloInventario  SET ");
            if (valPrecio1) {
                vAumento = FormulaAumento(SQLNameColum, valMargen1, valUsarNuevaFormula);
                vSQL.AppendLine(" margenganancia  = " + valMargen1);
                vSQL.AppendLine(",PrecioSinIva  = " + vAumento);
                vSQL.AppendLine(", PrecioConIva  = " + vAumento + " ( " + vAumento + " * " + vSQLAlicuotasVigentes + ") / 100");
                if (valPrecio2 || valPrecio3 || valPrecio4) {
                    vSQL.AppendLine(",");
                }
            }
            if (valPrecio2) {
                vAumento = FormulaAumento(SQLNameColum, valMargen2, valUsarNuevaFormula);
                vSQL.AppendLine(" margenganancia2  = " + valMargen2);
                vSQL.AppendLine(", PrecioSinIva2  = " + vAumento);
                vSQL.AppendLine(", PrecioConIva2  = " + vAumento + " ( " + vAumento + " * " + vSQLAlicuotasVigentes + ") / 100");
                if (valPrecio3 || valPrecio4) {
                    vSQL.AppendLine(",");
                }
            }
            if (valPrecio3) {
                vAumento = FormulaAumento(SQLNameColum, valMargen2, valUsarNuevaFormula);
                vSQL.AppendLine(" margenganancia3  = " + valMargen3);
                vSQL.AppendLine(", PrecioSinIva3  = " + vAumento);
                vSQL.AppendLine(", PrecioConIva3  = " + vAumento + " ( " + vAumento + " * " + vSQLAlicuotasVigentes + ") / 100");
                if (valPrecio4) {
                    vSQL.AppendLine(",");
                }
            }
            if (valPrecio4) {
                vAumento = FormulaAumento(SQLNameColum, valMargen4, valUsarNuevaFormula);
                vSQL.AppendLine(" margenganancia4  = " + valMargen4);
                vSQL.AppendLine(", PrecioSinIva4  = " + vAumento);
                vSQL.AppendLine(", PrecioConIva4  = " + vAumento + " ( " + vAumento + " * " + vSQLAlicuotasVigentes + ") / 100");
            }
            LibGpParams vParams = new LibGpParams();
            vSQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            vSQL.AppendLine("AND  articuloInventario.codigo between  @Desde AND @Hasta");
            vSQL.AppendLine("AND  (articuloInventario.tipodearticulo = '0' OR    articuloInventario.tipodearticulo = '2')");
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCia);
            vParams.AddInString("Desde", valDesde, 30);
            vParams.AddInString("Hasta", valHasta, 30);
            if (!LibString.IsNullOrEmpty(valLineaProducto)) {
                vSQL.AppendLine("AND  LineaProducto = @LineaProducto");
                vParams.AddInString("LineaProducto", valLineaProducto, 20);
            }
            if (!LibString.IsNullOrEmpty(valCategoria)) {
                vSQL.AppendLine("AND  Categoria = @Categoria");
                vParams.AddInString("Categoria", valCategoria, 20);
            }
            if (!LibString.IsNullOrEmpty(valCategoria)) {
                vSQL.AppendLine("AND  marca = @marca");
                vParams.AddInString("marca", valMarca, 30);
            }
            LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0);
        }

        private string FormulaAumento(string valNameColumna, decimal valMargen, bool valUsarNuevaFormula) {
            string vResult;
            if (valUsarNuevaFormula) {
                vResult = " (1 / (1 - " + valMargen + " ))";
                vResult = "(" + valNameColumna + "*" + vResult + ")";
            } else {
                vResult = "(((" + valNameColumna + "  * " + valMargen + " )) + " + valNameColumna + " )";
            }
            return vResult;
        }

        private bool ContinuarConElCalculo(bool valFormulaAlternativa, bool valMargenesNuevos, int valConsecutivoCompania, decimal valAlicuota1, decimal valAlicuota2, decimal valAlicuota3, DateTime valFechaOperacion, string valNumero, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4, decimal valMargen1, decimal valMargen2, decimal valMargen3, decimal valMargen4, bool valMonedaLocal) {
            if (valMargenesNuevos) {
                return ActualizaCostosyMargenes(valFormulaAlternativa, valConsecutivoCompania, valAlicuota1, valAlicuota2, valAlicuota3, valFechaOperacion, valNumero, valPrecio1, valPrecio2, valPrecio3, valPrecio4, valMargen1, valMargen2, valMargen3, valMargen4, valMonedaLocal);
            } else {
                return ActualizaCostos(valFormulaAlternativa, valConsecutivoCompania, valAlicuota1, valAlicuota2, valAlicuota3, valFechaOperacion, valNumero, valPrecio1, valPrecio2, valPrecio3, valPrecio4, valMargen1, valMargen2, valMargen3, valMargen4, valMonedaLocal);
            }
        }

        private bool ActualizaCostos(bool valFormulaAlternativa, int valConsecutivoCompania, decimal valAlicuota1, decimal valAlicuota2, decimal valAlicuota3, DateTime valFechaOperacion, string valNumero, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4, decimal valMargen1, decimal valMargen2, decimal valMargen3, decimal valMargen4, bool valMonedaLocal) {
            if ((Galac.Saw.Ccl.Inventario.eTipoDeMetodoDeCosteo)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "MetodoDeCosteo") == Ccl.Inventario.eTipoDeMetodoDeCosteo.CostoPromedio) {
                if (valFormulaAlternativa) {
                    return ExecuteActualizaCostosFormulaOpcional(valConsecutivoCompania, valAlicuota1, valAlicuota2, valAlicuota3, valFechaOperacion, valNumero, valPrecio1, valPrecio2, valPrecio3, valPrecio4);
                } else {
                    return ExecuteActualizaCostos(valConsecutivoCompania, valAlicuota1, valAlicuota2, valAlicuota3, valFechaOperacion, valNumero, valPrecio1, valPrecio2, valPrecio3, valPrecio4);
                }
            } else {
                if (valFormulaAlternativa) {
                    return ExecuteActualizaCostosFormulaOpcionalxUltimaCompra(valConsecutivoCompania, valAlicuota1, valAlicuota2, valAlicuota3, valFechaOperacion, valNumero, valPrecio1, valPrecio2, valPrecio3, valPrecio4, valMonedaLocal, valFormulaAlternativa);
                } else {
                    return ExecuteActualizaCostosxUltimaCompra(valConsecutivoCompania, valAlicuota1, valAlicuota2, valAlicuota3, valFechaOperacion, valNumero, valPrecio1, valPrecio2, valPrecio3, valPrecio4, valMonedaLocal, valFormulaAlternativa);
                }
            }
        }

        private bool ExecuteActualizaCostosxUltimaCompra(int valConsecutivoCompania, decimal valAlicuota1, decimal valAlicuota2, decimal valAlicuota3, DateTime valFechaOperacion, string valNumero, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4, bool valMonedaLocal, bool valFormulaAlternativa) {
            if (!valMonedaLocal) {
                ActualizarCostoEnMonedaExtranjera(valConsecutivoCompania, valAlicuota1, valAlicuota2, valAlicuota3, valFechaOperacion, valNumero, valPrecio1, valPrecio2, valPrecio3, valPrecio4, 0, 0, 0, 0, valFormulaAlternativa, false);
            }
            LibDataScope insDb = new LibDataScope();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("valConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInBoolean("valPrecio", valPrecio1);
            vParams.AddInBoolean("valPrecio2", valPrecio2);
            vParams.AddInBoolean("valPrecio3", valPrecio3);
            vParams.AddInBoolean("valPrecio4", valPrecio4);
            vParams.AddInDecimal("valAlicuotaGeneral", valAlicuota1, 2);
            vParams.AddInDecimal("valAlicuota2", valAlicuota2, 2);
            vParams.AddInDecimal("valAlicuota3", valAlicuota3, 2);
            vParams.AddInDateTime("valFecha", valFechaOperacion);
            vParams.AddInString("valNumeroDocumento", valNumero, 20);
            vParams.AddInString("valTipoDeDocumento", "compra", 15);
            return insDb.ExecSpNonQueryWithScope("Gp_ArticuloInventarioActualizarCostosxUltimaCompra", vParams.Get());
        }

        private bool ExecuteActualizaCostosFormulaOpcionalxUltimaCompra(int valConsecutivoCompania, decimal valAlicuota1, decimal valAlicuota2, decimal valAlicuota3, DateTime valFechaOperacion, string valNumero, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4, bool valMonedaLocal, bool valFormulaAlternativa) {
            if (!valMonedaLocal) {
                ActualizarCostoEnMonedaExtranjera(valConsecutivoCompania, valAlicuota1, valAlicuota2, valAlicuota3, valFechaOperacion, valNumero, valPrecio1, valPrecio2, valPrecio3, valPrecio4, 0, 0, 0, 0, valFormulaAlternativa, false);
            }
            LibDataScope insDb = new LibDataScope();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("valConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInBoolean("valPrecio", valPrecio1);
            vParams.AddInBoolean("valPrecio2", valPrecio2);
            vParams.AddInBoolean("valPrecio3", valPrecio3);
            vParams.AddInBoolean("valPrecio4", valPrecio4);
            vParams.AddInDecimal("valAlicuotaGeneral", valAlicuota1, 2);
            vParams.AddInDecimal("valAlicuota2", valAlicuota2, 2);
            vParams.AddInDecimal("valAlicuota3", valAlicuota3, 2);
            vParams.AddInDateTime("valFecha", valFechaOperacion);
            vParams.AddInString("valNumeroDocumento", valNumero, 20);
            vParams.AddInString("valTipoDeDocumento", "compra", 15);
            return insDb.ExecSpNonQueryWithScope("Gp_ArticuloInventarioActualizarCostosFormulaOpcionalxUltimaCompra", vParams.Get());
        }

        private bool ExecuteActualizaCostos(int valConsecutivoCompania, decimal valAlicuota1, decimal valAlicuota2, decimal valAlicuota3, DateTime valFechaOperacion, string valNumero, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4) {
            LibDataScope insDb = new LibDataScope();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("valConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInBoolean("valPrecio", valPrecio1);
            vParams.AddInBoolean("valPrecio2", valPrecio2);
            vParams.AddInBoolean("valPrecio3", valPrecio3);
            vParams.AddInBoolean("valPrecio4", valPrecio4);
            vParams.AddInDecimal("valAlicuotaGeneral", valAlicuota1, 2);
            vParams.AddInDecimal("valAlicuota2", valAlicuota2, 2);
            vParams.AddInDecimal("valAlicuota3", valAlicuota3, 2);
            vParams.AddInDateTime("valFecha", valFechaOperacion);
            vParams.AddInString("valNumeroDocumento", valNumero, 20);
            vParams.AddInString("valTipoDeDocumento", "compra", 15);
            return insDb.ExecSpNonQueryWithScope("Gp_ArticuloInventarioActualizarCostos", vParams.Get());
        }

        private bool ExecuteActualizaCostosFormulaOpcional(int valConsecutivoCompania, decimal valAlicuota1, decimal valAlicuota2, decimal valAlicuota3, DateTime valFechaOperacion, string valNumero, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4) {
            LibDataScope insDb = new LibDataScope();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("valConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInBoolean("valPrecio", valPrecio1);
            vParams.AddInBoolean("valPrecio2", valPrecio2);
            vParams.AddInBoolean("valPrecio3", valPrecio3);
            vParams.AddInBoolean("valPrecio4", valPrecio4);
            vParams.AddInDecimal("valAlicuotaGeneral", valAlicuota1, 2);
            vParams.AddInDecimal("valAlicuota2", valAlicuota2, 2);
            vParams.AddInDecimal("valAlicuota3", valAlicuota3, 2);
            vParams.AddInDateTime("valFecha", valFechaOperacion);
            vParams.AddInString("valNumeroDocumento", valNumero, 20);
            vParams.AddInString("valTipoDeDocumento", "compra", 15);
            return insDb.ExecSpNonQueryWithScope("Gp_ArticuloInventarioActualizarCostosFormulaOpcional", vParams.Get());
        }

        private bool ActualizaCostosyMargenes(bool valFormulaAlternativa, int valConsecutivoCompania, decimal
            valAlicuota1, decimal valAlicuota2, decimal valAlicuota3, DateTime valFechaOperacion, string valNumero, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4, decimal valMargen1, decimal valMargen2, decimal valMargen3, decimal valMargen4, bool valMonedaLocal) {
            if ((Galac.Saw.Ccl.Inventario.eTipoDeMetodoDeCosteo)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "MetodoDeCosteo") == Ccl.Inventario.eTipoDeMetodoDeCosteo.CostoPromedio) {
                if (valFormulaAlternativa) {
                    return ExecuteActualizaCostosyMargenesFormulaOpcional(valConsecutivoCompania, valAlicuota1, valAlicuota2, valAlicuota3, valFechaOperacion, valNumero, valPrecio1, valPrecio2, valPrecio3, valPrecio4, valMargen1, valMargen2, valMargen3, valMargen4);
                } else {
                    return ExecuteActualizaCostosyMargenes(valConsecutivoCompania, valAlicuota1, valAlicuota2, valAlicuota3, valFechaOperacion, valNumero, valPrecio1, valPrecio2, valPrecio3, valPrecio4, valMargen1, valMargen2, valMargen3, valMargen4);
                }
            } else {
                if (valFormulaAlternativa) {
                    return ExecuteActualizaCostosyMargenesFormulaOpcionalxUltimaCompra(valConsecutivoCompania, valAlicuota1, valAlicuota2, valAlicuota3, valFechaOperacion, valNumero, valPrecio1, valPrecio2, valPrecio3, valPrecio4, valMargen1, valMargen2, valMargen3, valMargen4, valMonedaLocal, valFormulaAlternativa);
                } else {
                    return ExecuteActualizaCostosyMargenesxUltimaCompra(valConsecutivoCompania, valAlicuota1, valAlicuota2, valAlicuota3, valFechaOperacion, valNumero, valPrecio1, valPrecio2, valPrecio3, valPrecio4, valMargen1, valMargen2, valMargen3, valMargen4, valMonedaLocal, valFormulaAlternativa);
                }
            }
        }

        private bool ExecuteActualizaCostosyMargenesFormulaOpcional(int valConsecutivoCompania, decimal valAlicuota1, decimal valAlicuota2, decimal valAlicuota3, DateTime valFechaOperacion, string valNumero, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4, decimal valMargen1, decimal valMargen2, decimal valMargen3, decimal valMargen4) {
            LibDataScope insDb = new LibDataScope();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("valConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInDecimal("valMargen", valMargen1, 2);
            vParams.AddInDecimal("valMargen2", valMargen2, 2);
            vParams.AddInDecimal("valMargen3", valMargen3, 2);
            vParams.AddInDecimal("valMargen4", valMargen4, 2);
            vParams.AddInBoolean("valPrecio", valPrecio1);
            vParams.AddInBoolean("valPrecio2", valPrecio2);
            vParams.AddInBoolean("valPrecio3", valPrecio3);
            vParams.AddInBoolean("valPrecio4", valPrecio4);
            vParams.AddInDecimal("valAlicuotaGeneral", valAlicuota1, 2);
            vParams.AddInDecimal("valAlicuota2", valAlicuota2, 2);
            vParams.AddInDecimal("valAlicuota3", valAlicuota3, 2);
            vParams.AddInDateTime("valFecha", valFechaOperacion);
            vParams.AddInString("valNumeroDocumento", valNumero, 20);
            vParams.AddInString("valTipoDeDocumento", "compra", 15);
            return insDb.ExecSpNonQueryWithScope("Gp_InventarioActualizarCostosyMargenesFormulaOpcional", vParams.Get());
        }

        private bool ExecuteActualizaCostosyMargenes(int valConsecutivoCompania, decimal valAlicuota1, decimal valAlicuota2, decimal valAlicuota3, DateTime valFechaOperacion, string valNumero, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4, decimal valMargen1, decimal valMargen2, decimal valMargen3, decimal valMargen4) {
            LibDataScope insDb = new LibDataScope();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("valConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInDecimal("valMargen", valMargen1, 2);
            vParams.AddInDecimal("valMargen2", valMargen2, 2);
            vParams.AddInDecimal("valMargen3", valMargen3, 2);
            vParams.AddInDecimal("valMargen4", valMargen4, 2);
            vParams.AddInBoolean("valPrecio", valPrecio1);
            vParams.AddInBoolean("valPrecio2", valPrecio2);
            vParams.AddInBoolean("valPrecio3", valPrecio3);
            vParams.AddInBoolean("valPrecio4", valPrecio4);
            vParams.AddInDecimal("valAlicuotaGeneral", valAlicuota1, 2);
            vParams.AddInDecimal("valAlicuota2", valAlicuota2, 2);
            vParams.AddInDecimal("valAlicuota3", valAlicuota3, 2);
            vParams.AddInDateTime("valFecha", valFechaOperacion);
            vParams.AddInString("valNumeroDocumento", valNumero, 20);
            vParams.AddInString("valTipoDeDocumento", "compra", 15);
            return insDb.ExecSpNonQueryWithScope("Gp_InventarioActualizarCostosyMargenes", vParams.Get());
        }

        private bool ExecuteActualizaCostosyMargenesFormulaOpcionalxUltimaCompra(int valConsecutivoCompania, decimal valAlicuota1, decimal valAlicuota2, decimal valAlicuota3, DateTime valFechaOperacion, string valNumero, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4, decimal valMargen1, decimal valMargen2, decimal valMargen3, decimal valMargen4, bool valMonedaLocal, bool valFormulaAlternativa) {
            if (!valMonedaLocal) {
                ActualizarCostoEnMonedaExtranjera(valConsecutivoCompania, valAlicuota1, valAlicuota2, valAlicuota3, valFechaOperacion, valNumero, valPrecio1, valPrecio2, valPrecio3, valPrecio4, valMargen1, valMargen2, valMargen3, valMargen4, valFormulaAlternativa, true);
            }
            LibDataScope insDb = new LibDataScope();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("valConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInDecimal("valMargen", valMargen1, 2);
            vParams.AddInDecimal("valMargen2", valMargen2, 2);
            vParams.AddInDecimal("valMargen3", valMargen3, 2);
            vParams.AddInDecimal("valMargen4", valMargen4, 2);
            vParams.AddInBoolean("valPrecio", valPrecio1);
            vParams.AddInBoolean("valPrecio2", valPrecio2);
            vParams.AddInBoolean("valPrecio3", valPrecio3);
            vParams.AddInBoolean("valPrecio4", valPrecio4);
            vParams.AddInDecimal("valAlicuotaGeneral", valAlicuota1, 2);
            vParams.AddInDecimal("valAlicuota2", valAlicuota2, 2);
            vParams.AddInDecimal("valAlicuota3", valAlicuota3, 2);
            vParams.AddInDateTime("valFecha", valFechaOperacion);
            vParams.AddInString("valNumeroDocumento", valNumero, 20);
            vParams.AddInString("valTipoDeDocumento", "compra", 15);
            return insDb.ExecSpNonQueryWithScope("Gp_InventarioActualizarCostosyMargenesFormulaOpcionalxUltimaCompra", vParams.Get());
        }

        private bool ExecuteActualizaCostosyMargenesxUltimaCompra(int valConsecutivoCompania, decimal valAlicuota1, decimal valAlicuota2, decimal valAlicuota3, DateTime valFechaOperacion, string valNumero, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4, decimal valMargen1, decimal valMargen2, decimal valMargen3, decimal valMargen4, bool valMonedaLocal, bool valUsaFormulaAlternativa) {

            if (!valMonedaLocal) {
                ActualizarCostoEnMonedaExtranjera(valConsecutivoCompania, valAlicuota1, valAlicuota2, valAlicuota3, valFechaOperacion, valNumero, valPrecio1, valPrecio2, valPrecio3, valPrecio4, valMargen1, valMargen2, valMargen3, valMargen4, valUsaFormulaAlternativa, true);
            }
            LibDataScope insDb = new LibDataScope();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("valConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInDecimal("valMargen", valMargen1, 2);
            vParams.AddInDecimal("valMargen2", valMargen2, 2);
            vParams.AddInDecimal("valMargen3", valMargen3, 2);
            vParams.AddInDecimal("valMargen4", valMargen4, 2);
            vParams.AddInBoolean("valPrecio", valPrecio1);
            vParams.AddInBoolean("valPrecio2", valPrecio2);
            vParams.AddInBoolean("valPrecio3", valPrecio3);
            vParams.AddInBoolean("valPrecio4", valPrecio4);
            vParams.AddInDecimal("valAlicuotaGeneral", valAlicuota1, 2);
            vParams.AddInDecimal("valAlicuota2", valAlicuota2, 2);
            vParams.AddInDecimal("valAlicuota3", valAlicuota3, 2);
            vParams.AddInDateTime("valFecha", valFechaOperacion);
            vParams.AddInString("valNumeroDocumento", valNumero, 20);
            vParams.AddInString("valTipoDeDocumento", "compra", 15);
            return insDb.ExecSpNonQueryWithScope("Gp_InventarioActualizarCostosyMargenesxUltimaCompra", vParams.Get());
        }


        void IArticuloInventarioPdn.ProcesaCostoPromedio(int valConsecutivoCompania, bool valVieneDeOperaciones, DateTime valFechaOperacion, string valCodigo, string valDocumento, string valOperacion) {
            if (LibDate.F1IsGreaterOrEqualThanF2(valFechaOperacion, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Parametros", "FechaDesdeUsoMetodoDeCosteo"))
                && (Galac.Saw.Ccl.Inventario.eTipoDeMetodoDeCosteo)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "MetodoDeCosteo") == Ccl.Inventario.eTipoDeMetodoDeCosteo.CostoPromedio) {
                if (valVieneDeOperaciones) {
                    MarcaComoRecalcularSiEsElCaso(valConsecutivoCompania, valFechaOperacion, valDocumento, valOperacion);
                }
                if (valVieneDeOperaciones && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "CalculoAutomaticoDeCosto")) {
                    EjecutaElCalculoDelCostoPromedioCuandoVieneDeOperaciones(valConsecutivoCompania, valFechaOperacion, valDocumento, valOperacion);
                    if (LibString.S1IsEqualToS2("Nota de Crédito", valOperacion)) {
                        //EjecutarAjustesdePreciosCostos(valFechaOperacion, valDocumento);
                    }
                } else {
                    UpdateCostoPromedioPonderado(valConsecutivoCompania, valCodigo, FechaUltimoCierre(valConsecutivoCompania), LibDate.Today());
                }
            }
        }

        private void EjecutaElCalculoDelCostoPromedioCuandoVieneDeOperaciones(int valConsecutivoCompania, DateTime valFechaOperacion, string valDocumento, string valOperacion) {
            XElement vXmlData = CodigoArticuloDeOperaciones(valConsecutivoCompania, valFechaOperacion, valDocumento, valOperacion);
            var vData = vXmlData.Descendants("GpResul").Select(p => new { Codigo = p.Element("codigo").Value });
            foreach (var item in vData) {
                UpdateCostoPromedioPonderado(valConsecutivoCompania, item.Codigo, FechaUltimoCierre(valConsecutivoCompania), LibDate.Today());
            }
        }

        private void UpdateCostoPromedioPonderado(int valConsecutivoCompania, string valCodigo, DateTime valFechaInicial, DateTime valFechaFinal) {

            LibGpParams vParams = new LibGpParams();
            vParams.AddInBoolean("RecalcularCosto", false);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInDateTime("FechaInicial", valFechaInicial);
            vParams.AddInDateTime("FechaFinal", valFechaFinal);
            vParams.AddInString("Codigo", valCodigo, 30);
            vParams.AddInBoolean("CostoALaFecha", true);
            vParams.AddInEnum("StatusdelArticulo", (int)eStatusArticulo.Vigente);
            vParams.AddInEnum("TipoDeArticulo", (int)eTipoDeArticulo.Mercancia);

            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine(" UPDATE articuloInventario ");
            vSQL.AppendLine(" SET   RecalcularCosto =  @RecalcularCosto");// & gUtilSQL.fBooleanToSqlValue(False) & " , "
            vSQL.AppendLine("   CostoPromedio = A.CostoFinal  ");
            vSQL.AppendLine("   FROM   articuloInventario AS B ");
            vSQL.AppendLine("   INNER JOIN  dbo.Gf_CostoPromedioPonderado(@ConsecutivoCompania,@FechaInicial,@FechaFinal,@Codigo,@Codigo)) AS A ON ");
            vSQL.AppendLine("   B.codigo = A.Codigo AND ");
            vSQL.AppendLine("   B.consecutivoCompania = @ConsecutivoCompania");
            vSQL.AppendLine(" WHERE B.ConsecutivoCompania = @ConsecutivoCompania");
            vSQL.AppendLine(" AND A.CostoALaFecha = @CostoALaFecha");
            vSQL.AppendLine(" AND B.TipoDeArticulo = @TipoDeArticulo");
            vSQL.AppendLine(" AND B.StatusdelArticulo = @StatusdelArticulo");
            vSQL.AppendLine(" AND B.StatusdelArticulo = @StatusdelArticulo");
            LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0);

        }

        private DateTime FechaUltimoCierre(int valConsecutivoCompania) {
            DateTime vResult = LibDate.Today();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine(" SELECT Max(Fecha ) As Fecha  FROM CierreCostoDelPeriodo ");
            vSQL.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            vSQL.AppendLine("   GROUP BY   ConsecutivoCompania ");
            XElement vData = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            if (LibXml.CountXElement(vData) > 0) {
                vResult = LibConvert.ToDate(LibXml.GetPropertyString(vData, "Fecha"));
            }
            return vResult;
        }


        private void MarcaComoRecalcularSiEsElCaso(int valConsecutivoCompania, DateTime valFechaOperacion, string valDocumento, string valOperacion) {
            XElement vData = FechaCierresCostoPromedio(valConsecutivoCompania, valFechaOperacion);
            if (LibXml.CountXElement(vData) > 0) {
                LibBusinessProcessMessage vMessage = new LibBusinessProcessMessage();
                vMessage.Content = "Usted ingreso una operación anterior a los cierres actuales, tome esto en consideración, ya que para el cálculo del costo promedio se deberá re calcular los cierres anuales de los costos .";
                LibBusinessProcess.Call("MensajeDeRecalcularSiEsElCaso", vMessage);
                UpdateArticuloInventarioFechaCierreOperacion(valConsecutivoCompania, FechaReporteCostoPromedio(valConsecutivoCompania, valFechaOperacion, false), valFechaOperacion, valDocumento, valOperacion);
            }
            if (!LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "CalculoAutomaticoDeCosto")) {
                UpdateArticuloInventarioRecalulaCostoOperacion(valConsecutivoCompania, valFechaOperacion, valDocumento, valOperacion);
            }
        }

        private void UpdateArticuloInventarioRecalulaCostoOperacion(int valConsecutivoCompania, DateTime valFechaOperacion, string valDocumento, string valOperacion) {
            StringBuilder vSQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInBoolean("RecalcularCierre", true);
            vParams.AddInBoolean("RecalcularCosto", true);
            vParams.AddInBoolean("RecalcularCosto", true);

            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInEnum("StatusdelArticulo", (int)eStatusArticulo.Vigente);
            vParams.AddInEnum("TipoDeArticulo", (int)eTipoDeArticulo.Mercancia);
            vParams.AddInString("TipoDeDocumento", valOperacion, 34);
            vParams.AddInString("NumeroDocumento", valDocumento, 30);
            vParams.AddInDateTime("Fecha", valFechaOperacion);
            vSQL.AppendLine("  UPDATE articuloInventario ");
            vSQL.AppendLine(" SET   RecalcularCierre = @RecalcularCierre  ");
            vSQL.AppendLine("  ,RecalcularCosto = @RecalcularCosto");

            vSQL.AppendLine("  FROM   articuloInventario ");
            vSQL.AppendLine("  INNER JOIN  IGV_ArticuloInvMovimiento ON ");
            vSQL.AppendLine("  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND ");
            vSQL.AppendLine("  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo ");
            vSQL.AppendLine("  WHERE articuloInventario.ConsecutivoCompania = @ConsecutivoCompania");
            vSQL.AppendLine("  AND articuloInventario.StatusdelArticulo = @StatusdelArticulo");
            vSQL.AppendLine("  AND articuloInventario.TipoDeArticulo =@TipoDeArticulo");
            vSQL.AppendLine("  AND IGV_ArticuloInvMovimiento.TipoDeDocumento = @TipoDeDocumento");
            vSQL.AppendLine("  AND IGV_ArticuloInvMovimiento.NumeroDocumento = @NumeroDocumento");
            vSQL.AppendLine("  AND IGV_ArticuloInvMovimiento.Fecha = @Fecha");
            LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0);
        }

        private void UpdateArticuloInventarioFechaCierreOperacion(int valConsecutivoCompania, DateTime valFechaCierre, DateTime valFechaOperacion, string valDocumento, string valOperacion) {
            StringBuilder vSQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInBoolean("RecalcularCierre", true);
            vParams.AddInBoolean("RecalcularCosto", true);
            vParams.AddInBoolean("RecalcularCosto", true);
            vParams.AddInDateTime("FechaCierreActualizada", valFechaCierre);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInEnum("StatusdelArticulo", (int)eStatusArticulo.Vigente);
            vParams.AddInEnum("TipoDeArticulo", (int)eTipoDeArticulo.Mercancia);
            vParams.AddInString("TipoDeDocumento", valOperacion, 34);
            vParams.AddInString("NumeroDocumento", valDocumento, 30);
            vParams.AddInDateTime("Fecha", valFechaOperacion);
            vSQL.AppendLine("  UPDATE articuloInventario ");
            vSQL.AppendLine(" SET   RecalcularCierre = @RecalcularCierre  ");
            vSQL.AppendLine("  ,RecalcularCosto = @RecalcularCosto");
            vSQL.AppendLine("  ,FechaCierreActualizada = @FechaCierreActualizada");
            vSQL.AppendLine("  FROM   articuloInventario ");
            vSQL.AppendLine("  INNER JOIN  IGV_ArticuloInvMovimiento ON ");
            vSQL.AppendLine("  ArticuloInventario.ConsecutivoCompania = IGV_ArticuloInvMovimiento.ConsecutivoCompania AND ");
            vSQL.AppendLine("  ArticuloInventario.Codigo = IGV_ArticuloInvMovimiento.Codigo ");
            vSQL.AppendLine("  WHERE articuloInventario.ConsecutivoCompania = @ConsecutivoCompania");
            vSQL.AppendLine("  AND articuloInventario.StatusdelArticulo = @StatusdelArticulo");
            vSQL.AppendLine("  AND articuloInventario.TipoDeArticulo =@TipoDeArticulo");
            vSQL.AppendLine("  AND IGV_ArticuloInvMovimiento.TipoDeDocumento = @TipoDeDocumento");
            vSQL.AppendLine("  AND IGV_ArticuloInvMovimiento.NumeroDocumento = @NumeroDocumento");
            vSQL.AppendLine("  AND IGV_ArticuloInvMovimiento.Fecha = @Fecha");
            LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0);
        }

        private DateTime FechaReporteCostoPromedio(int valConsecutivoCompania, DateTime valFechaOperacion, bool valReporte) {
            DateTime vResult = LibDate.Today();
            XElement vData = FechaCierresCostoPromedio(valConsecutivoCompania, valFechaOperacion);
            if (LibXml.CountXElement(vData) > 0) {
                vResult = LibConvert.ToDate(LibXml.GetPropertyString(vData, "Fecha"));

            } else {
                vResult = LibDate.AddDays(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Parametros", "FechaDesdeUsoMetodoDeCosteo"), -1);
            }
            return vResult;
        }

        private XElement FechaCierresCostoPromedio(int valConsecutivoCompania, DateTime valFechaOperacion) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInBoolean("EscargaInicial", false);
            vParams.AddInDateTime("Fecha", valFechaOperacion);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine(" SELECT Max(Fecha ) As Fecha  FROM CierreCostoDelPeriodo ");
            vSQL.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            vSQL.AppendLine(" AND  CierreCostoDelPeriodo.Fecha >= @Fecha ");
            vSQL.AppendLine(" AND  EscargaInicial = @EscargaInicial ");
            vSQL.AppendLine("   GROUP BY   ConsecutivoCompania ");
            return LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);

        }

        public XElement GetDataPageByCode(string valCodeFilter, int valCompanyCode, int valPage) {
            if (valCodeFilter == null)
                valCodeFilter = string.Empty;
            StringBuilder SQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("filtro", valCodeFilter, 50);
            vParams.AddInInteger("pagina", valPage);
            vParams.AddInInteger("consecutivoCompania", valCompanyCode);
            SQL.AppendLine("EXEC dbo.Gp_ObtenerPaginaDeArticulosPorCodigo @filtro, @consecutivoCompania, @pagina, @articulosPorPagina=10");
            XElement xRecord = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", 0);
            return xRecord;
        }

        public XElement GetDataPageByDescription(string valDescriptionFilter, int valCompanyCode, int valPage) {
            if (valDescriptionFilter == null)
                valDescriptionFilter = string.Empty;
            StringBuilder SQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("filtro", valDescriptionFilter, 50);
            vParams.AddInInteger("pagina", valPage);
            vParams.AddInInteger("consecutivoCompania", valCompanyCode);
            SQL.AppendLine("EXEC dbo.Gp_ObtenerPaginaDeArticulosPorDescripcion @filtro, @consecutivoCompania, @pagina, @articulosPorPagina=10");
            XElement xRecord = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", 0);
            return xRecord;
        }

        private void ActualizarCostoEnMonedaExtranjera(int valConsecutivoCompania, decimal valAlicuota1, decimal valAlicuota2, decimal valAlicuota3, DateTime valFechaOperacion, string valNumero, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4, decimal valMargen1, decimal valMargen2, decimal valMargen3, decimal valMargen4, bool valUsaFormulaAlternativa, bool valUsaMargenNuevo) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("valConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInDecimal("valMargen1", valMargen1, 2);
            vParams.AddInDecimal("valMargen2", valMargen2, 2);
            vParams.AddInDecimal("valMargen3", valMargen3, 2);
            vParams.AddInDecimal("valMargen4", valMargen4, 2);
            vParams.AddInBoolean("valPrecio1", valPrecio1);
            vParams.AddInBoolean("valPrecio2", valPrecio2);
            vParams.AddInBoolean("valPrecio3", valPrecio3);
            vParams.AddInBoolean("valPrecio4", valPrecio4);
            vParams.AddInDecimal("valAlicuotaGeneral", valAlicuota1, 2);
            vParams.AddInDecimal("valAlicuota2", valAlicuota2, 2);
            vParams.AddInDecimal("valAlicuota3", valAlicuota3, 2);
            vParams.AddInDateTime("valFecha", valFechaOperacion);
            vParams.AddInString("valNumeroDocumento", valNumero, 20);
            vParams.AddInString("valTipoDeDocumento", "compra", 15);

            StringBuilder vSQL = new StringBuilder();
            if (valPrecio1) {
                vSQL.Append(ConstruirPrimerSQLParaMonedaExtranjeraConMargenes(valUsaFormulaAlternativa, valUsaMargenNuevo));
            }
            if (valPrecio2) {
                vSQL.Append(ConstruirSegundoSQLParaMonedaExtranjeraConMargenes(valUsaFormulaAlternativa, valUsaMargenNuevo));
            }
            if (valPrecio3) {
                vSQL.Append(ConstruirTercerSQLParaMonedaExtranjeraConMargenes(valUsaFormulaAlternativa, valUsaMargenNuevo));
            }
            if (valPrecio4) {
                vSQL.Append(ConstruirCuartoSQLParaMonedaExtranjeraConMargenes(valUsaFormulaAlternativa, valUsaMargenNuevo));
            }
            LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0);
        }

        private StringBuilder ConstruirPrimerSQLParaMonedaExtranjeraConMargenes(bool valUsaFormulaAlterna, bool valUsaNuevosMargenes) {
            string vTipoDeMargen;
            vTipoDeMargen = "@valMargen1";
            if (!valUsaNuevosMargenes) {
                vTipoDeMargen = "ai.MargenGanancia";
            }
            StringBuilder instancia = new StringBuilder();
            instancia.AppendLine("UPDATE cme ");
            if (valUsaFormulaAlterna) {
                instancia.AppendLine("SET MePrecioSinIva = (ai.MeCostoUnitario * (1 / ((1 - " + vTipoDeMargen + " / 100)))), ");
                instancia.AppendLine("MePrecioConIva = (ai.MeCostoUnitario * (1 / ((1 - " + vTipoDeMargen + " / 100)))) + (ai.MeCostoUnitario * (1 / ((1 - " + vTipoDeMargen + " / 100)))) * ");
                instancia.AppendLine("((CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)/100) ");
            } else {
                instancia.AppendLine("SET MePrecioSinIva = (((ai.MeCostoUnitario * " + vTipoDeMargen + ") / 100) + ai.MeCostoUnitario), ");
                instancia.AppendLine("MePrecioConIva = (((ai.MeCostoUnitario * " + vTipoDeMargen + ") / 100) + ai.MeCostoUnitario) + ((((ai.MeCostoUnitario * " + vTipoDeMargen + ") / 100) + ai.MeCostoUnitario) * ");
                instancia.AppendLine("((CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)/100)) ");
            }
            instancia.Append(FromInnerJoinComunesEnLaConsulta());
            return instancia;
        }

        private StringBuilder ConstruirSegundoSQLParaMonedaExtranjeraConMargenes(bool valUsaFormulaAlterna, bool valUsaNuevosMargenes) {
            string vTipoDeMargen;
            vTipoDeMargen = "@valMargen2";
            if (!valUsaNuevosMargenes) {
                vTipoDeMargen = "ai.MargenGanancia2";
            }
            StringBuilder instancia = new StringBuilder();
            instancia.AppendLine("UPDATE cme ");
            if (valUsaFormulaAlterna) {
                instancia.AppendLine("SET MePrecioSinIva2 = (ai.MeCostoUnitario * (1 / ((1 - " + vTipoDeMargen + " / 100)))), ");
                instancia.AppendLine("MePrecioConIva2 = (ai.MeCostoUnitario * (1 / ((1 - " + vTipoDeMargen + " / 100)))) + (ai.MeCostoUnitario * (1 / ((1 - " + vTipoDeMargen + " / 100)))) * ");
                instancia.AppendLine("((CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)/100) ");
            } else {
                instancia.AppendLine("SET MePrecioSinIva2 = (((ai.MeCostoUnitario * " + vTipoDeMargen + ") / 100) + ai.MeCostoUnitario), ");
                instancia.AppendLine("MePrecioConIva2 = (((ai.MeCostoUnitario * " + vTipoDeMargen + ") / 100) + ai.MeCostoUnitario) + ((((ai.MeCostoUnitario * " + vTipoDeMargen + ") / 100) + ai.MeCostoUnitario) * ");
                instancia.AppendLine("((CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)/100)) ");
            }
            instancia.Append(FromInnerJoinComunesEnLaConsulta());
            return instancia;
        }

        private StringBuilder ConstruirTercerSQLParaMonedaExtranjeraConMargenes(bool valUsaFormulaAlterna, bool valUsaNuevosMargenes) {
            string vTipoDeMargen;
            vTipoDeMargen = "@valMargen3";
            if (!valUsaNuevosMargenes) {
                vTipoDeMargen = "ai.MargenGanancia3";
            }
            StringBuilder instancia = new StringBuilder();
            instancia.AppendLine("UPDATE cme ");
            if (valUsaFormulaAlterna) {
                instancia.AppendLine("SET MePrecioSinIva3 = (ai.MeCostoUnitario * (1 / ((1 - " + vTipoDeMargen + " / 100)))), ");
                instancia.AppendLine("MePrecioConIva3 = (ai.MeCostoUnitario * (1 / ((1 - " + vTipoDeMargen + " / 100)))) + (ai.MeCostoUnitario * (1 / ((1 - " + vTipoDeMargen + " / 100)))) * ");
                instancia.AppendLine("((CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)/100) ");
            } else {
                instancia.AppendLine("SET MePrecioSinIva3 = (((ai.MeCostoUnitario * " + vTipoDeMargen + ") / 100) + ai.MeCostoUnitario), ");
                instancia.AppendLine("MePrecioConIva3 = (((ai.MeCostoUnitario * " + vTipoDeMargen + ") / 100) + ai.MeCostoUnitario) + ((((ai.MeCostoUnitario * " + vTipoDeMargen + ") / 100) + ai.MeCostoUnitario) * ");
                instancia.AppendLine("((CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)/100)) ");
            }
            instancia.Append(FromInnerJoinComunesEnLaConsulta());
            return instancia;
        }

        private StringBuilder ConstruirCuartoSQLParaMonedaExtranjeraConMargenes(bool valUsaFormulaAlterna, bool valUsaNuevosMargenes) {
            string vTipoDeMargen;
            vTipoDeMargen = "@valMargen4";
            if (!valUsaNuevosMargenes) {
                vTipoDeMargen = "ai.MargenGanancia4";
            }
            StringBuilder instancia = new StringBuilder();
            instancia.AppendLine("UPDATE cme ");
            if (valUsaFormulaAlterna) {
                instancia.AppendLine("SET MePrecioSinIva4 = (ai.MeCostoUnitario * (1 / ((1 - " + vTipoDeMargen + " / 100)))), ");
                instancia.AppendLine("MePrecioConIva4 = (ai.MeCostoUnitario * (1 / ((1 - " + vTipoDeMargen + " / 100)))) + (ai.MeCostoUnitario * (1 / ((1 - " + vTipoDeMargen + " / 100)))) * ");
                instancia.AppendLine("((CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)/100) ");
            } else {
                instancia.AppendLine("SET MePrecioSinIva4 = (((ai.MeCostoUnitario * " + vTipoDeMargen + ") / 100) + ai.MeCostoUnitario), ");
                instancia.AppendLine("MePrecioConIva4 = (((ai.MeCostoUnitario * " + vTipoDeMargen + ") / 100) + ai.MeCostoUnitario) + ((((ai.MeCostoUnitario * " + vTipoDeMargen + ") / 100) + ai.MeCostoUnitario) * ");
                instancia.AppendLine("((CASE WHEN alicuotaiva = 1 THEN  @valAlicuotaGeneral WHEN alicuotaiva = 2 THEN  @valAlicuota2    WHEN  alicuotaiva = 3 THEN  @valAlicuota3  ELSE 0 END)/100)) ");
            }
            instancia.Append(FromInnerJoinComunesEnLaConsulta());
            return instancia;
        }

        private StringBuilder FromInnerJoinComunesEnLaConsulta() {
            StringBuilder instancia = new StringBuilder();
            instancia.AppendLine("FROM dbo.CamposMonedaExtranjera as cme ");
            instancia.AppendLine("INNER JOIN IGV_ArticuloInvMovimiento as aim ");
            instancia.AppendLine("ON cme.Codigo = aim.Codigo  ");
            instancia.AppendLine("AND cme.ConsecutivoCompania = aim.ConsecutivoCompania  ");
            instancia.AppendLine("INNER JOIN  ArticuloInventario as ai ON ");
            instancia.AppendLine("ai.ConsecutivoCompania = aim.ConsecutivoCompania AND ");
            instancia.AppendLine("ai.Codigo = aim.Codigo ");
            instancia.AppendLine("WHERE aim.TipoDeDocumento = @valTipoDeDocumento ");
            instancia.AppendLine("AND aim.NumeroDocumento = @valNumeroDocumento ");
            instancia.AppendLine("AND  aim.Fecha = @valFecha ");
            instancia.AppendLine("AND cme.ConsecutivoCompania = @valConsecutivoCompania; ");
            return instancia;
        }

        public XElement DisponibilidadDeArticuloPorAlmacen(int valConsecutivoCompania, XElement valDataArticulo) {
            StringBuilder vSQL = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            QAdvSql InsSql = new QAdvSql("");
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valDataArticulo);
            vSQL.AppendLine(" DECLARE @hdoc int ");
            vSQL.AppendLine(" EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlData ");
            vSQL.AppendLine("SELECT ");
            vSQL.AppendLine("	 XmlDoc.CodigoArticulo, XmlDoc.ConsecutivoAlmacen, ISNULL(Cantidad, 0)  AS Cantidad  ");
            vSQL.AppendLine("FROM   OPENXML(@hdoc, 'GpData/GpResult', 2) ");
            vSQL.AppendLine(" WITH( ");
            vSQL.AppendLine("	   ConsecutivoAlmacen " + InsSql.NumericTypeForDb(10, 0) + ",");
            vSQL.AppendLine("	   CodigoArticulo " + InsSql.VarCharTypeForDb(30) + ") AS XmlDoc");
            vSQL.AppendLine(" LEFT JOIN ExistenciaPorAlmacen ON XmlDoc.CodigoArticulo = ExistenciaPorAlmacen.CodigoArticulo AND XmlDoc.ConsecutivoAlmacen = ExistenciaPorAlmacen.ConsecutivoAlmacen");
            vSQL.AppendLine(" AND ExistenciaPorAlmacen.ConsecutivoCompania = @ConsecutivoCompania ");
            vSQL.AppendLine(" EXEC sp_xml_removedocument @hdoc");
            XElement vData = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            return vData;
        }

        bool IArticuloInventarioPdn.RecalcularExistencia(int valConsecutivoCompania, string valCodigoAlmacen, List<XElement> valListaDeArticulos) {
            string vCodigoArticulo = "";
            StringBuilder vParams = new StringBuilder();
            decimal vNuevaCantidad = 0;
            string vSqlCantidad = string.Empty;
            eTipoArticuloInv vTipoArticuloInv;
            string vCodigoCompuestoPorGrupo;
            string vSerial;
            string vRollo;
            int vConsecutivoAlmacen = 0;
            try {
                vConsecutivoAlmacen = BuscarConsecutivoAlmacen(valConsecutivoCompania, valCodigoAlmacen);
                foreach (XElement vArticulo in valListaDeArticulos) {
                    vCodigoArticulo = LibXml.GetElementValueOrEmpty(vArticulo, "Codigo");
                    vTipoArticuloInv = (eTipoArticuloInv)LibConvert.DbValueToEnum(LibXml.GetElementValueOrEmpty(vArticulo, "TipoArticuloInv"));
                    vSerial = LibXml.GetElementValueOrEmpty(vArticulo, "Serial");
                    vRollo = LibXml.GetElementValueOrEmpty(vArticulo, "Rollo");
                    var ListGrupoArticulo = BuscarArticuloPorGrupo(valConsecutivoCompania, vCodigoArticulo, true);
                    vCodigoCompuestoPorGrupo = LibXml.GetElementValueOrEmpty(ListGrupoArticulo, "CodigoCompuesto");
                    vSqlCantidad = SqlCantidadPorNotaES(valConsecutivoCompania, valCodigoAlmacen, vCodigoArticulo, eTipodeOperacion.EntradadeInventario, vTipoArticuloInv, vCodigoCompuestoPorGrupo, vSerial, vRollo, ref vParams);
                    vNuevaCantidad = BuscarCantidadDelProductosSegunSQL(vSqlCantidad, vParams);
                    vSqlCantidad = SqlCantidadPorNotaES(valConsecutivoCompania, valCodigoAlmacen, vCodigoArticulo, eTipodeOperacion.SalidadeInventario, vTipoArticuloInv, vCodigoCompuestoPorGrupo, vSerial, vRollo, ref vParams);
                    vNuevaCantidad = vNuevaCantidad - BuscarCantidadDelProductosSegunSQL(vSqlCantidad, vParams);
                    vSqlCantidad = SqlCantidadPorNotaES(valConsecutivoCompania, valCodigoAlmacen, vCodigoArticulo, eTipodeOperacion.Retiro, vTipoArticuloInv, vCodigoCompuestoPorGrupo, vSerial, vRollo, ref vParams);
                    vNuevaCantidad = vNuevaCantidad - BuscarCantidadDelProductosSegunSQL(vSqlCantidad, vParams);
                    vSqlCantidad = SqlCantidadPorNotaES(valConsecutivoCompania, valCodigoAlmacen, vCodigoArticulo, eTipodeOperacion.Autoconsumo, vTipoArticuloInv, vCodigoCompuestoPorGrupo, vSerial, vRollo, ref vParams);
                    vNuevaCantidad = vNuevaCantidad - BuscarCantidadDelProductosSegunSQL(vSqlCantidad, vParams);
                    vSqlCantidad = SqlCantidadPorTransferenciaAlmacenes(valConsecutivoCompania, valCodigoAlmacen, vCodigoArticulo, eTipodeOperacion.EntradadeInventario, vTipoArticuloInv, vCodigoCompuestoPorGrupo, vSerial, vRollo, ref vParams);
                    vNuevaCantidad = vNuevaCantidad + BuscarCantidadDelProductosSegunSQL(vSqlCantidad, vParams);
                    vSqlCantidad = SqlCantidadPorTransferenciaAlmacenes(valConsecutivoCompania, valCodigoAlmacen, vCodigoArticulo, eTipodeOperacion.SalidadeInventario, vTipoArticuloInv, vCodigoCompuestoPorGrupo, vSerial, vRollo, ref vParams);
                    vNuevaCantidad = vNuevaCantidad - BuscarCantidadDelProductosSegunSQL(vSqlCantidad, vParams);
                    vSqlCantidad = SqlCantidadPorFacturas(valConsecutivoCompania, valCodigoAlmacen, vCodigoArticulo, vTipoArticuloInv, vCodigoCompuestoPorGrupo, vSerial, vRollo, ref vParams);
                    vNuevaCantidad = vNuevaCantidad - BuscarCantidadDelProductosSegunSQL(vSqlCantidad, vParams);
                    vSqlCantidad = SqlCantidadPorFacturasPC(valConsecutivoCompania, valCodigoAlmacen, vCodigoArticulo, vTipoArticuloInv, vCodigoCompuestoPorGrupo, vSerial, vRollo, ref vParams);
                    vNuevaCantidad = vNuevaCantidad - BuscarCantidadDelProductosSegunSQL(vSqlCantidad, vParams);
                    vSqlCantidad = SqlCantidadPorCompras(valConsecutivoCompania, vConsecutivoAlmacen, vCodigoArticulo, vTipoArticuloInv, vCodigoCompuestoPorGrupo, vSerial, vRollo, ref vParams);
                    vNuevaCantidad = vNuevaCantidad + BuscarCantidadDelProductosSegunSQL(vSqlCantidad, vParams);
                    vSqlCantidad = SqlCantidadPorConteoFisico(valConsecutivoCompania, valCodigoAlmacen, vCodigoArticulo, vTipoArticuloInv, vCodigoCompuestoPorGrupo, vSerial, vRollo, ref vParams);
                    vNuevaCantidad = vNuevaCantidad + BuscarCantidadDelProductosSegunSQL(vSqlCantidad, vParams);
                    if (!ExisteArticuloEnAlmacen(valConsecutivoCompania, vCodigoArticulo, valCodigoAlmacen) && vNuevaCantidad != 0) {
                        InsertarEnExistencia(valConsecutivoCompania, vConsecutivoAlmacen, valCodigoAlmacen, vCodigoArticulo);
                    }
                    ActualizaCantidades(valConsecutivoCompania, valCodigoAlmacen, vNuevaCantidad, vCodigoArticulo, vSerial, vRollo, vCodigoCompuestoPorGrupo, vTipoArticuloInv);
                    vNuevaCantidad = 0;
                    vParams = new StringBuilder();
                }
                return true;
            } catch (GalacException) {
                throw;
            } catch (Exception) {
                throw;
            }
        }

        private string SqlInsertarEnExistencia(int valConsecutivoCompania, int vConsecutivoAlmacen, string valCodigoAlmacen, string vCodigoArticulo, ref StringBuilder refParams) {
            QAdvSql insUtilSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoAlmacen", valCodigoAlmacen, 5);
            vParams.AddInString("CodigoArticulo", vCodigoArticulo, 30);
            vParams.AddInInteger("ConsecutivoAlmacen", vConsecutivoAlmacen);
            refParams = vParams.Get();
            vSql.AppendLine("INSERT INTO ExistenciaPorAlmacen (ConsecutivoCompania, CodigoAlmacen, ConsecutivoAlmacen,CodigoArticulo, Cantidad, Ubicacion) ");
            vSql.AppendLine($"VALUES (@ConsecutivoCompania, @CodigoAlmacen, @CodigoArticulo,@ConsecutivoAlmacen, {insUtilSql.ToSqlValue(0m)}, {insUtilSql.ToSqlValue("")}) ");
            return vSql.ToString();
        }

        private void InsertarEnExistencia(int valConsecutivoCompania, int vConsecutivoAlmacen, string valCodigoAlmacen, string vCodigoArticulo) {
            try {
                StringBuilder vParams = new StringBuilder();
                string vSql = SqlInsertarEnExistencia(valConsecutivoCompania, vConsecutivoAlmacen, valCodigoAlmacen, vCodigoArticulo, ref vParams);
                LibBusiness.ExecuteUpdateOrDelete(vSql, vParams, "", 0);
            } catch (GalacException) {
                throw;
            }
        }

        private decimal BuscarCantidadDelProductosSegunSQL(string valSql, StringBuilder valParams) {
            decimal vResult = 0;
            try {
                XElement vData = LibBusiness.ExecuteSelect(valSql, valParams, "", 0);
                if (vData != null && vData.HasElements) {
                    vResult = LibConvert.ToDec(LibXml.GetPropertyString(vData, "SumCantidad"));
                }
                return vResult;
            } catch (GalacException) {
                throw;
            }
        }

        private int BuscarConsecutivoAlmacen(int valConsecutivoCompania, string valCodigoAlmacen) {
            int vResult = 0;
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            try {
                vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
                vParams.AddInString("CodigoAlmacen", valCodigoAlmacen, 5);
                vSql.AppendLine("SELECT Consecutivo");
                vSql.AppendLine(" FROM ALMACEN");
                vSql.AppendLine(" WHERE");
                vSql.AppendLine(" ConsecutivoCompania= @ConsecutivoCompania");
                vSql.AppendLine(" AND Codigo = @CodigoAlmacen");
                XElement vData = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), "", 0);
                if (vData != null && vData.HasElements) {
                    vResult = LibConvert.ToInt(LibXml.GetPropertyString(vData, "Consecutivo"));
                }
                return vResult;
            } catch (GalacException) {
                throw;
            }
        }

        private XElement BuscarArticuloPorGrupo(int valConsecutivoCompania, string valCodigoArticulo, bool valBuscarArticulosColoryTalla) {
            XElement vData = new XElement("GpData");
            StringBuilder vSql = new StringBuilder();
            QAdvSql insUtilSql = new QAdvSql("");
            LibGpParams vParams = new LibGpParams();
            try {
                vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
                vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
                vSql.AppendLine("SELECT ArticuloInventario.TipoArticuloInv,");
                vSql.AppendLine(" ArticuloInventario.Codigo,");
                vSql.AppendLine(" (ISNULL(ExistenciaporGrupo.CodigoArticulo, '') + ISNULL(ExistenciaPorGrupo.CodigoColor, '') + ISNULL(ExistenciaporGRupo.CodigoTalla, '')) AS CodigoCompuesto");
                vSql.AppendLine(" FROM articuloInventario");
                vSql.AppendLine(" LEFT JOIN ExistenciaPorGrupo  ON articuloinventario.consecutivocompania = ExistenciaPorGrupo.consecutivoCompania  ");
                vSql.AppendLine(" AND ArticuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo");
                vSql.AppendLine(" WHERE ArticuloInventario.TipoDeArticulo = " + insUtilSql.EnumToSqlValue((int)eTipoDeArticulo.Mercancia));
                vSql.AppendLine(" AND ArticuloInventario.ConsecutivoCompania = @ConsecutivoCompania ");
                if (valBuscarArticulosColoryTalla) {
                    vSql.AppendLine(" AND ArticuloInventario.Codigo = @CodigoArticulo");
                }
                vSql.AppendLine(" GROUP BY (ISNULL(ExistenciaporGrupo.CodigoArticulo, '') + ISNULL(ExistenciaPorGrupo.CodigoColor, '') + ISNULL(ExistenciaporGRupo.CodigoTalla, '')),");
                vSql.AppendLine(" ArticuloInventario.Codigo,  ArticuloInventario.TipoArticuloInv");
                vData = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), "", 0);
                vData = vData.Element("GpResult");
                return vData;
            } catch (GalacException) {
                throw;
            }
        }

        private string SqlCantidadPorNotaES(int valConsecutivoCompania, string valCodigoAlmacen, string valCodigoArticulo, eTipodeOperacion valTipodeOperacion, eTipoArticuloInv valTipoArticuloInv, string valCodigoCompuesto, string valSerial, string valRollo, ref StringBuilder refParams) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insUtilSql = new QAdvSql("");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoAlmacen", valCodigoAlmacen, 5);
            refParams = vParams.Get();
            vSql.AppendLine("SELECT ISNULL(SUM(RenglonNotaES.Cantidad), 0) AS SumCantidad ");
            vSql.AppendLine(" FROM notaDeEntradaSalida ");
            vSql.AppendLine(" INNER JOIN renglonNotaES  ON notaDeEntradaSalida.NumeroDocumento = renglonNotaES.NumeroDocumento ");
            vSql.AppendLine(" AND notaDeEntradaSalida.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania ");
            vSql.AppendLine(" WHERE notaDeEntradaSalida.ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND notaDeEntradaSalida.StatusNotaEntradaSalida = " + insUtilSql.EnumToSqlValue((int)eStatusNotaEntradaSalida.Vigente));
            vSql.AppendLine(" AND notaDeEntradaSalida.CodigoAlmacen = @CodigoAlmacen ");
            vSql.AppendLine(" AND notaDeEntradaSalida.TipodeOperacion = " + insUtilSql.EnumToSqlValue((int)valTipodeOperacion));
            if (valTipoArticuloInv == eTipoArticuloInv.UsaTallaColor || valTipoArticuloInv == eTipoArticuloInv.UsaTallaColorySerial) {
                vSql.AppendLine(" AND renglonNotaES.CodigoArticulo = " + insUtilSql.ToSqlValue(valCodigoCompuesto));
                if (valTipoArticuloInv == eTipoArticuloInv.UsaTallaColorySerial) {
                    if (!LibString.IsNullOrEmpty(valSerial) && !LibString.S1IsEqualToS2(valSerial, "0")) {
                        vSql.AppendLine(" AND RenglonNotaES.Serial = " + insUtilSql.ToSqlValue(valSerial));
                    }
                    if (!LibString.IsNullOrEmpty(valRollo) && !LibString.S1IsEqualToS2(valRollo, "0")) {
                        vSql.AppendLine(" AND renglonNotaES.Rollo = " + insUtilSql.ToSqlValue(valRollo));
                    }
                }
            } else {
                vSql.AppendLine(" AND renglonNotaES.CodigoArticulo = " + insUtilSql.ToSqlValue(valCodigoArticulo));
                if (valTipoArticuloInv == eTipoArticuloInv.UsaSerial) {
                    if (!LibString.IsNullOrEmpty(valSerial) && !LibString.S1IsEqualToS2(valSerial, "0")) {
                        vSql.AppendLine(" AND RenglonNotaES.Serial = " + insUtilSql.ToSqlValue(valSerial));
                    }
                } else if (valTipoArticuloInv == eTipoArticuloInv.UsaSerialRollo) {
                    if (!LibString.IsNullOrEmpty(valSerial) && !LibString.S1IsEqualToS2(valSerial, "0")) {
                        vSql.AppendLine(" AND RenglonNotaES.Serial = " + insUtilSql.ToSqlValue(valSerial));
                    }
                    if (!LibString.IsNullOrEmpty(valRollo) && !LibString.S1IsEqualToS2(valRollo, "0")) {
                        vSql.AppendLine(" AND renglonNotaES.Rollo = " + insUtilSql.ToSqlValue(valRollo));
                    }
                }
            }
            return vSql.ToString();
        }

        private string SqlCantidadPorTransferenciaAlmacenes(int valConsecutivoCompania, string valCodigoAlmacen, string valCodigoArticulo, eTipodeOperacion valTipodeOperacion, eTipoArticuloInv valTipoArticuloInv, string valCodigoCompuesto, string valSerial, string valRollo, ref StringBuilder refParams) {
            QAdvSql insUtilSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoAlmacen", valCodigoAlmacen, 5);
            refParams = vParams.Get();
            vSql.AppendLine("SELECT ISNULL(SUM(RenglonTransferencia.Cantidad), 0) AS SumCantidad ");
            vSql.AppendLine(" FROM Transferencia ");
            vSql.AppendLine(" INNER JOIN RenglonTransferencia  ON Transferencia.NumeroDocumento = RenglonTransferencia.NumeroDocumento ");
            vSql.AppendLine(" AND Transferencia.ConsecutivoCompania = RenglonTransferencia.ConsecutivoCompania ");
            vSql.AppendLine(" WHERE Transferencia.ConsecutivoCompania = @ConsecutivoCompania ");
            if (valTipodeOperacion == eTipodeOperacion.EntradadeInventario) {
                vSql.AppendLine(" AND Transferencia.CodigoAlmacenEntrada = @CodigoAlmacen ");
            } else if (valTipodeOperacion == eTipodeOperacion.SalidadeInventario) {
                vSql.AppendLine(" AND Transferencia.CodigoAlmacenSalida = @CodigoAlmacen ");
            }
            if (valTipoArticuloInv == eTipoArticuloInv.UsaTallaColor || valTipoArticuloInv == eTipoArticuloInv.UsaTallaColorySerial) {
                vSql.AppendLine(" AND RenglonTransferencia.CodigoArticulo = " + insUtilSql.ToSqlValue(valCodigoCompuesto));
                if (valTipoArticuloInv == eTipoArticuloInv.UsaTallaColorySerial) {
                    if (!LibString.IsNullOrEmpty(valSerial) && !LibString.S1IsEqualToS2(valSerial, "0")) {
                        vSql.AppendLine(" AND RenglonTransferencia.Serial = " + insUtilSql.ToSqlValue(valSerial));
                    }
                    if (!LibString.IsNullOrEmpty(valRollo) && !LibString.S1IsEqualToS2(valRollo, "0")) {
                        vSql.AppendLine(" AND RenglonTransferencia.Rollo = " + insUtilSql.ToSqlValue(valRollo));
                    }
                }
            } else {
                vSql.AppendLine(" AND RenglonTransferencia.CodigoArticulo = " + insUtilSql.ToSqlValue(valCodigoArticulo));
                if (valTipoArticuloInv == eTipoArticuloInv.UsaSerial) {
                    if (!LibString.IsNullOrEmpty(valSerial) && !LibString.S1IsEqualToS2(valSerial, "0")) {
                        vSql.AppendLine(" AND RenglonTransferencia.Serial = " + insUtilSql.ToSqlValue(valSerial));
                    }
                } else if (valTipoArticuloInv == eTipoArticuloInv.UsaSerialRollo) {
                    if (!LibString.IsNullOrEmpty(valSerial) && !LibString.S1IsEqualToS2(valSerial, "0")) {
                        vSql.AppendLine(" AND RenglonTransferencia.Serial = " + insUtilSql.ToSqlValue(valSerial));
                    }
                    if (!LibString.IsNullOrEmpty(valRollo) && !LibString.S1IsEqualToS2(valRollo, "0")) {
                        vSql.AppendLine(" AND RenglonTransferencia.Rollo = " + insUtilSql.ToSqlValue(valRollo));
                    }
                }
            }
            return vSql.ToString();
        }

        private string SqlCantidadPorFacturas(int valConsecutivoCompania, string valCodigoAlmacen, string valCodigoArticulo, eTipoArticuloInv valTipoArticuloInv, string valCodigoCompuesto, string valSerial, string valRollo, ref StringBuilder refParams) {
            QAdvSql insUtilSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoAlmacen", valCodigoAlmacen, 5);
            refParams = vParams.Get();
            vSql.AppendLine("SELECT ISNULL(SUM(Renglonfactura.Cantidad), 0) AS SumCantidad ");
            vSql.AppendLine(" FROM factura ");
            vSql.AppendLine(" INNER JOIN renglonFactura  ON ");
            vSql.AppendLine(" factura.Numero = renglonFactura.NumeroFactura  ");
            vSql.AppendLine(" AND factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania ");
            vSql.AppendLine(" AND factura.TipoDeDocumento = renglonFactura.TipoDeDocumento ");
            vSql.AppendLine(" WHERE factura.ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND factura.CodigoAlmacen = @CodigoAlmacen ");
            vSql.AppendLine(" AND factura.StatusFactura = '0' ");
            vSql.AppendLine(" AND factura.GeneradaPorNotaEntrega = '0' ");
            if (valTipoArticuloInv == eTipoArticuloInv.UsaTallaColor || valTipoArticuloInv == eTipoArticuloInv.UsaTallaColorySerial) {
                vSql.AppendLine(" AND renglonFactura.Articulo = " + insUtilSql.ToSqlValue(valCodigoCompuesto));
                if (valTipoArticuloInv == eTipoArticuloInv.UsaTallaColorySerial) {
                    if (!LibString.IsNullOrEmpty(valSerial) && !LibString.S1IsEqualToS2(valSerial, "0")) {
                        vSql.AppendLine(" AND renglonFactura.Serial = " + insUtilSql.ToSqlValue(valSerial));
                    }
                    if (!LibString.IsNullOrEmpty(valRollo) && !LibString.S1IsEqualToS2(valRollo, "0")) {
                        vSql.AppendLine(" AND renglonFactura.Rollo = " + insUtilSql.ToSqlValue(valRollo));
                    }
                }
            } else {
                vSql.AppendLine(" AND renglonFactura.Articulo = " + insUtilSql.ToSqlValue(valCodigoArticulo));
                if (valTipoArticuloInv == eTipoArticuloInv.UsaSerial) {
                    if (!LibString.IsNullOrEmpty(valSerial) && !LibString.S1IsEqualToS2(valSerial, "0")) {
                        vSql.AppendLine(" AND renglonFactura.Serial = " + insUtilSql.ToSqlValue(valSerial));
                    }
                } else if (valTipoArticuloInv == eTipoArticuloInv.UsaSerialRollo) {
                    if (!LibString.IsNullOrEmpty(valSerial) && !LibString.S1IsEqualToS2(valSerial, "0")) {
                        vSql.AppendLine(" AND renglonFactura.Serial = " + insUtilSql.ToSqlValue(valSerial));
                    }
                    if (!LibString.IsNullOrEmpty(valRollo) && !LibString.S1IsEqualToS2(valRollo, "0")) {
                        vSql.AppendLine(" AND renglonFactura.Rollo = " + insUtilSql.ToSqlValue(valRollo));
                    }
                }
            }
            return vSql.ToString();
        }

        private string SqlCantidadPorFacturasPC(int valConsecutivoCompania, string valCodigoAlmacen, string valCodigoArticulo, eTipoArticuloInv valTipoArticuloInv, string valCodigoCompuesto, string valSerial, string valRollo, ref StringBuilder refParams) {
            QAdvSql insUtilSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoAlmacen", valCodigoAlmacen, 5);
            refParams = vParams.Get();
            vSql.AppendLine("SELECT ISNULL(SUM(Renglonfactura.Cantidad*productoCompuesto.cantidad), 0) AS SumCantidad ");
            vSql.AppendLine(" FROM ProductoCompuesto ");
            vSql.AppendLine(" INNER JOIN ArticuloInventario   ON ProductoCompuesto.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania ");
            vSql.AppendLine(" AND ProductoCompuesto.CodigoConexionConElMaster = ArticuloInventario.Codigo ");
            vSql.AppendLine(" INNER JOIN factura ");
            vSql.AppendLine(" INNER JOIN renglonFactura ON factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania  ");
            vSql.AppendLine(" AND factura.Numero = renglonFactura.NumeroFactura ");
            vSql.AppendLine(" AND factura.TipoDeDocumento = renglonFactura.TipoDeDocumento ON ");
            vSql.AppendLine(" ArticuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania ");
            vSql.AppendLine(" AND ArticuloInventario.Codigo = renglonFactura.Articulo ");
            vSql.AppendLine(" WHERE factura.ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND factura.CodigoAlmacen = @CodigoAlmacen ");
            vSql.AppendLine(" AND factura.StatusFactura = '0' ");
            vSql.AppendLine(" AND factura.GeneradaPorNotaEntrega = '0' ");
            if (valTipoArticuloInv == eTipoArticuloInv.UsaTallaColor || valTipoArticuloInv == eTipoArticuloInv.UsaTallaColorySerial) {
                vSql.AppendLine(" AND productoCompuesto.CodigoArticulo = " + insUtilSql.ToSqlValue(valCodigoCompuesto));
                if (valTipoArticuloInv == eTipoArticuloInv.UsaTallaColorySerial) {
                    if (!LibString.IsNullOrEmpty(valSerial) && !LibString.S1IsEqualToS2(valSerial, "0")) {
                        vSql.AppendLine(" AND renglonFactura.Serial = " + insUtilSql.ToSqlValue(valSerial));
                    }
                    if (!LibString.IsNullOrEmpty(valRollo) && !LibString.S1IsEqualToS2(valRollo, "0")) {
                        vSql.AppendLine(" AND renglonFactura.Rollo = " + insUtilSql.ToSqlValue(valRollo));
                    }
                }
            } else {
                vSql.AppendLine(" AND productoCompuesto.CodigoArticulo = " + insUtilSql.ToSqlValue(valCodigoArticulo));
                if (valTipoArticuloInv == eTipoArticuloInv.UsaSerial) {
                    if (!LibString.IsNullOrEmpty(valSerial) && !LibString.S1IsEqualToS2(valSerial, "0")) {
                        vSql.AppendLine(" AND renglonFactura.Serial = " + insUtilSql.ToSqlValue(valSerial));
                    }
                } else if (valTipoArticuloInv == eTipoArticuloInv.UsaSerialRollo) {
                    if (!LibString.IsNullOrEmpty(valSerial) && !LibString.S1IsEqualToS2(valSerial, "0")) {
                        vSql.AppendLine(" AND renglonFactura.Serial = " + insUtilSql.ToSqlValue(valSerial));
                    }
                    if (!LibString.IsNullOrEmpty(valRollo) && !LibString.S1IsEqualToS2(valRollo, "0")) {
                        vSql.AppendLine(" AND renglonFactura.Rollo = " + insUtilSql.ToSqlValue(valRollo));
                    }
                }
            }
            return vSql.ToString();
        }

        private string SqlCantidadPorCompras(int valConsecutivoCompania, int valConsecutivoAlmacen, string valCodigoArticulo, eTipoArticuloInv valTipoArticuloInv, string valCodigoCompuesto, string valSerial, string valRollo, ref StringBuilder refParams) {
            QAdvSql insUtilSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoAlmacen", valConsecutivoAlmacen);
            refParams = vParams.Get();
            if (valTipoArticuloInv == eTipoArticuloInv.UsaTallaColor || valTipoArticuloInv == eTipoArticuloInv.UsaTallaColorySerial) {
                vSql.AppendLine("SELECT ISNULL(SUM(CompraDetalleArticuloInventario.Cantidad) , 0) AS SumCantidad ");
                vSql.AppendLine(" FROM Adm.Compra ");
                vSql.AppendLine(" INNER JOIN Adm.CompraDetalleArticuloInventario ON Compra.Consecutivo = CompraDetalleArticuloInventario.ConsecutivoCompra AND ");
                vSql.AppendLine(" Compra.ConsecutivoCompania = CompraDetalleArticuloInventario.ConsecutivoCompania ");
                vSql.AppendLine(" WHERE CompraDetalleArticuloInventario.CodigoArticulo = " + insUtilSql.ToSqlValue(valCodigoArticulo));
            } else {
                vSql.AppendLine("SELECT ISNULL(SUM(CompraDetalleSerialRollo.Cantidad) , 0) AS SumCantidad");
                vSql.AppendLine(" FROM Adm.compra INNER JOIN Adm.CompraDetalleSerialRollo ");
                vSql.AppendLine(" ON compra.Consecutivo = CompraDetalleSerialRollo.ConsecutivoCompra ");
                vSql.AppendLine(" AND compra.ConsecutivoCompania = CompraDetalleSerialRollo.ConsecutivoCompania");
                vSql.AppendLine(" WHERE CompraDetalleSerialRollo.CodigoArticulo = " + insUtilSql.ToSqlValue(valCodigoArticulo));
                if (!LibString.IsNullOrEmpty(valSerial) && !LibString.S1IsEqualToS2(valSerial, "0")) {
                    vSql.AppendLine(" AND CompraDetalleSerialRollo.Serial = " + insUtilSql.ToSqlValue(valSerial));
                }
                if (!LibString.IsNullOrEmpty(valRollo) && !LibString.S1IsEqualToS2(valRollo, "0")) {
                    vSql.AppendLine(" AND CompraDetalleSerialRollo.Rollo = " + insUtilSql.ToSqlValue(valRollo));
                }
                vSql.AppendLine(" AND compra.ConsecutivoCompania = @ConsecutivoCompania");
                vSql.AppendLine(" AND compra.ConsecutivoAlmacen = @ConsecutivoAlmacen");
                vSql.AppendLine(" AND compra.StatusCompra = " + insUtilSql.ToSqlValue("0")); //Vigente
            }
            return vSql.ToString();
        }

        private string SqlCantidadPorConteoFisico(int valConsecutivoCompania, string valCodigoAlmacen, string valCodigoArticulo, eTipoArticuloInv valTipoArticuloInv, string valCodigoCompuesto, string valSerial, string valRollo, ref StringBuilder refParams) {
            QAdvSql insUtilSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoAlmacen", valCodigoAlmacen, 5);
            refParams = vParams.Get();
            vSql.AppendLine("SELECT ISNULL(SUM(Diferencia) ,0) AS SumCantidad");
            vSql.AppendLine(" FROM ConteoFisico");
            vSql.AppendLine(" INNER JOIN RenglonConteoFisico  ON ConteoFisico.ConsecutivoConteo = RenglonConteoFisico.ConsecutivoConteo ");
            vSql.AppendLine(" AND ConteoFisico.ConsecutivoCompania = RenglonConteoFisico.ConsecutivoCompania");
            vSql.AppendLine(" WHERE ConteoFisico.ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND ConteoFisico.CodigoAlmacen = @CodigoAlmacen ");
            vSql.AppendLine(" AND ConteoFisico.Status = '0' ");
            vSql.AppendLine(" AND RenglonConteoFisico.CodigoArticulo = " + insUtilSql.ToSqlValue(valCodigoArticulo));
            if (valTipoArticuloInv == eTipoArticuloInv.UsaTallaColor || valTipoArticuloInv == eTipoArticuloInv.UsaTallaColorySerial) {
                vSql.AppendLine(" AND RenglonConteoFisico.CodigoArticulo = " + insUtilSql.ToSqlValue(valCodigoCompuesto));
                if (valTipoArticuloInv == eTipoArticuloInv.UsaTallaColorySerial) {
                    if (!LibString.IsNullOrEmpty(valSerial) && !LibString.S1IsEqualToS2(valSerial, "0")) {
                        vSql.AppendLine(" AND RenglonConteoFisico.Serial = " + insUtilSql.ToSqlValue(valSerial));
                    }
                    if (!LibString.IsNullOrEmpty(valRollo) && !LibString.S1IsEqualToS2(valRollo, "0")) {
                        vSql.AppendLine(" AND RenglonConteoFisico.Rollo = " + insUtilSql.ToSqlValue(valRollo));
                    }
                }
            } else {
                vSql.AppendLine(" AND RenglonConteoFisico.CodigoArticulo = " + insUtilSql.ToSqlValue(valCodigoArticulo));
                if (valTipoArticuloInv == eTipoArticuloInv.UsaSerial) {
                    if (!LibString.IsNullOrEmpty(valSerial) && !LibString.S1IsEqualToS2(valSerial, "0")) {
                        vSql.AppendLine(" AND RenglonConteoFisico.Serial = " + insUtilSql.ToSqlValue(valSerial));
                    }
                } else if (valTipoArticuloInv == eTipoArticuloInv.UsaSerialRollo) {
                    if (!LibString.IsNullOrEmpty(valSerial) && !LibString.S1IsEqualToS2(valSerial, "0")) {
                        vSql.AppendLine(" AND RenglonConteoFisico.Serial = " + insUtilSql.ToSqlValue(valSerial));
                    }
                    if (!LibString.IsNullOrEmpty(valRollo) && !LibString.S1IsEqualToS2(valRollo, "0")) {
                        vSql.AppendLine(" AND RenglonConteoFisico.Rollo = " + insUtilSql.ToSqlValue(valRollo));
                    }
                }
            }
            return vSql.ToString();
        }

        private bool ExisteArticuloEnAlmacen(int valConsecutivoCompania, string valCodigoArticulo, string valCodigoAlmacen) {
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            XElement vData = new XElement("GpData");
            try {
                vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
                vParams.AddInString("CodigoAlmacen", valCodigoAlmacen, 5);
                vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
                vSql.AppendLine(" SELECT ExistenciaPorAlmacen.CodigoAlmacen,");
                vSql.AppendLine(" ExistenciaPorAlmacen.CodigoArticulo,");
                vSql.AppendLine(" ExistenciaPorAlmacen.ConsecutivoAlmacen");
                vSql.AppendLine(" FROM ExistenciaPorAlmacen");
                vSql.AppendLine(" WHERE ExistenciaPorAlmacen.ConsecutivoCompania = @ConsecutivoCompania");
                vSql.AppendLine(" AND CodigoAlmacen = @CodigoAlmacen");
                vSql.AppendLine(" AND CodigoArticulo = @CodigoArticulo");
                vSql.AppendLine(" GROUP BY CodigoAlmacen, CodigoArticulo, ConsecutivoAlmacen");
                vData = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), "", 0);
                if (vData != null && vData.HasElements) {
                    return (vData.Descendants("GpResult").ToList().Count() > 0);
                } else {
                    return false;
                }
            } catch (GalacException) {
                throw;
            }
        }

        private void ActualizaCantidades(int valConsecutivoCompania, string valCodigoAlmacen, decimal valCantidad, string valCodigoArticulo, string valSerial, string valRollo, string valCodigoCompuesto, eTipoArticuloInv valTipoArticuloInvAsEnum) {
            string vSql = "";
            try {
                vSql = SqlActualizarCantidad(valConsecutivoCompania, valCodigoAlmacen, valCantidad, valCodigoArticulo, valSerial, valRollo, valCodigoCompuesto, valTipoArticuloInvAsEnum, 1);
                if (!LibString.IsNullOrEmpty(vSql)) {
                    LibBusiness.ExecuteUpdateOrDelete(vSql, null, "", 0);
                }
                vSql = SqlActualizarCantidad(valConsecutivoCompania, valCodigoAlmacen, valCantidad, valCodigoArticulo, valSerial, valRollo, valCodigoCompuesto, valTipoArticuloInvAsEnum, 2);
                if (!LibString.IsNullOrEmpty(vSql)) {
                    LibBusiness.ExecuteUpdateOrDelete(vSql, null, "", 0);
                }
                vSql = SqlActualizarCantidad(valConsecutivoCompania, valCodigoAlmacen, valCantidad, valCodigoArticulo, valSerial, valRollo, valCodigoCompuesto, valTipoArticuloInvAsEnum, 3);
                if (!LibString.IsNullOrEmpty(vSql)) {
                    LibBusiness.ExecuteUpdateOrDelete(vSql, null, "", 0);
                }
                vSql = SqlActualizarCantidad(valConsecutivoCompania, valCodigoAlmacen, valCantidad, valCodigoArticulo, valSerial, valRollo, valCodigoCompuesto, valTipoArticuloInvAsEnum, 4);
                if (!LibString.IsNullOrEmpty(vSql)) {
                    LibBusiness.ExecuteUpdateOrDelete(vSql, null, "", 0);
                }
            } catch (GalacException) {
                throw;
            }
        }

        private string SqlActualizarCantidad(int valConsecutivoCompania, string valCodigoAlmacen, decimal valCantidad, string valCodigo, string valSerial, string valRollo, string valCodigoCompuesto, eTipoArticuloInv valTipoArticuloInvAsEnum, int valNsql) {
            string vResult = "";
            QAdvSql insUtilSql = new QAdvSql("");
            if (valNsql == 1) { //existenciaPorAlmacen
                vResult = "UPDATE existenciaPorAlmacen SET Cantidad = Cantidad + " + insUtilSql.ToSqlValue(valCantidad);
                vResult = vResult + " WHERE CodigoAlmacen = " + insUtilSql.ToSqlValue(valCodigoAlmacen);
                if (valTipoArticuloInvAsEnum == eTipoArticuloInv.UsaTallaColor || valTipoArticuloInvAsEnum == eTipoArticuloInv.UsaTallaColorySerial) {
                    vResult = vResult + " AND CodigoArticulo = " + insUtilSql.ToSqlValue(valCodigoCompuesto);
                } else {
                    vResult = vResult + " AND CodigoArticulo = " + insUtilSql.ToSqlValue(valCodigo);
                }
                vResult = vResult + " AND ConsecutivoCompania = " + insUtilSql.ToSqlValue(valConsecutivoCompania);
            } else if (valNsql == 2) { //articuloInventario
                vResult = "UPDATE articuloInventario SET Existencia = Existencia + " + insUtilSql.ToSqlValue(valCantidad);
                vResult = vResult + " WHERE Codigo=" + insUtilSql.ToSqlValue(valCodigo);
                vResult = vResult + " AND ConsecutivoCompania=" + insUtilSql.ToSqlValue(valConsecutivoCompania);
            } else if (valNsql == 3) {  //ExistenciaPorGrupo
                if (valTipoArticuloInvAsEnum != eTipoArticuloInv.Simple) {
                    vResult = "UPDATE ExistenciaPorGrupo SET Existencia = Existencia + " + insUtilSql.ToSqlValue(valCantidad);
                    vResult = vResult + " WHERE ConsecutivoCompania = " + insUtilSql.ToSqlValue(valConsecutivoCompania);
                    if (valTipoArticuloInvAsEnum == eTipoArticuloInv.UsaTallaColor || valTipoArticuloInvAsEnum == eTipoArticuloInv.UsaTallaColorySerial) {
                        vResult = vResult + " AND (CodigoArticulo+CodigoColor+CodigoTalla) = " + insUtilSql.ToSqlValue(valCodigoCompuesto);
                        if (valTipoArticuloInvAsEnum == eTipoArticuloInv.UsaTallaColorySerial) {
                            vResult = vResult + " AND Serial = " + insUtilSql.ToSqlValue(valSerial);
                            vResult = vResult + " AND Rollo = " + insUtilSql.ToSqlValue(valRollo);
                        }
                    } else {
                        vResult = vResult + " AND CodigoArticulo = " + insUtilSql.ToSqlValue(valCodigo);
                        if (valTipoArticuloInvAsEnum == eTipoArticuloInv.UsaSerial) {
                            vResult = vResult + " AND Serial = " + insUtilSql.ToSqlValue(valSerial);
                        } else if (valTipoArticuloInvAsEnum == eTipoArticuloInv.UsaSerialRollo) {
                            vResult = vResult + " AND Serial = " + insUtilSql.ToSqlValue(valSerial);
                            vResult = vResult + " AND Rollo = " + insUtilSql.ToSqlValue(valRollo);
                        }
                    }
                }
            } else if (valNsql == 4) {  //RenglonExistenciaAlmacen
                if (valTipoArticuloInvAsEnum != eTipoArticuloInv.Simple) {
                    vResult = "UPDATE RenglonExistenciaAlmacen SET Cantidad = " + insUtilSql.ToSqlValue(valCantidad);
                    vResult = vResult + " WHERE ConsecutivoCompania = " + insUtilSql.ToSqlValue(valConsecutivoCompania);
                    vResult = vResult + " AND CodigoAlmacen = " + insUtilSql.ToSqlValue(valCodigoAlmacen);
                    if (valTipoArticuloInvAsEnum == eTipoArticuloInv.UsaTallaColor || valTipoArticuloInvAsEnum == eTipoArticuloInv.UsaTallaColorySerial) {
                        vResult = vResult + "AND CodigoArticulo = " + insUtilSql.ToSqlValue(valCodigoCompuesto);
                        if (valTipoArticuloInvAsEnum == eTipoArticuloInv.UsaTallaColorySerial) {
                            vResult = vResult + "AND CodigoSerial = " + insUtilSql.ToSqlValue(valSerial);
                            vResult = vResult + "AND CodigoRollo = " + insUtilSql.ToSqlValue(valRollo);
                        }
                    } else {
                        vResult = vResult + "AND CodigoArticulo = " + insUtilSql.ToSqlValue(valCodigo);
                        if (valTipoArticuloInvAsEnum == eTipoArticuloInv.UsaSerial) {
                            vResult = vResult + "AND CodigoSerial = " + insUtilSql.ToSqlValue(valSerial);
                        } else if (valTipoArticuloInvAsEnum == eTipoArticuloInv.UsaSerialRollo) {
                            vResult = vResult + "AND CodigoSerial = " + insUtilSql.ToSqlValue(valSerial);
                            vResult = vResult + "AND CodigoRollo = " + insUtilSql.ToSqlValue(valRollo);
                        }
                    }
                }
            }
            return vResult;
        }
    }
} //End of namespace Galac.Saw.Brl.Inventario