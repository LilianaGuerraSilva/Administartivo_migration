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
using Galac.Adm.Ccl.GestionProduccion;
using System.Collections.ObjectModel;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Brl.GestionProduccion {
    public partial class clsOrdenDeProduccionDetalleMaterialesNav : LibBaseNavDetail<IList<OrdenDeProduccionDetalleMateriales>, IList<OrdenDeProduccionDetalleMateriales>>, IOrdenDeProduccionDetalleMaterialesPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsOrdenDeProduccionDetalleMaterialesNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<OrdenDeProduccionDetalleMateriales>, IList<OrdenDeProduccionDetalleMateriales>> GetDataInstance() {
            return new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionDetalleMaterialesDat();
        }

        internal ObservableCollection<OrdenDeProduccionDetalleMateriales> FillWithForeignInfo(OrdenDeProduccion valOrdenDeProduccion) {
            IList<OrdenDeProduccionDetalleMateriales> vList = valOrdenDeProduccion.DetailOrdenDeProduccionDetalleMateriales.ToList();
            FillWithForeignInfoOrdenDeProduccionDetalleMateriales(ref vList);
            return new ObservableCollection<OrdenDeProduccionDetalleMateriales>(vList);
        }
        #region OrdenDeProduccionDetalleMateriales

        private void FillWithForeignInfoOrdenDeProduccionDetalleMateriales(ref IList<OrdenDeProduccionDetalleMateriales> refData) {
            XElement vInfoConexionAlmacen = FindInfoAlmacen(refData);
            var vListAlmacen = (from vRecord in vInfoConexionAlmacen.Descendants("GpResult")
                                select new {
                                    ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                    ConsecutivoAlmacen = LibConvert.ToInt(vRecord.Element("Consecutivo")),
                                    CodigoAlmacen = vRecord.Element("Codigo").Value,
                                    NombreAlmacen = vRecord.Element("NombreAlmacen").Value,
                                    TipoDeAlmacen = vRecord.Element("TipoDeAlmacen").Value,
                                    ConsecutivoCliente = LibConvert.ToInt(vRecord.Element("ConsecutivoCliente")),
                                    CodigoCc = vRecord.Element("CodigoCc").Value,
                                    Descripcion = vRecord.Element("Descripcion").Value
                                }).Distinct();
            XElement vInfoConexionArticuloInventario = FindInfoArticuloInventario(refData);
            var vListArticuloInventario = (from vRecord in vInfoConexionArticuloInventario.Descendants("GpResult")
                                      select new {
                                          ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                          Codigo = vRecord.Element("Codigo").Value,
                                          Descripcion = vRecord.Element("Descripcion").Value,
                                          LineaDeProducto = vRecord.Element("LineaDeProducto").Value,
                                          StatusdelArticulo = vRecord.Element("StatusdelArticulo").Value,
                                          TipoDeArticulo = (eTipoDeArticulo)LibConvert.DbValueToEnum(vRecord.Element("TipoDeArticulo").Value),
                                          TipoArticuloInv = (eTipoArticuloInv)LibConvert.DbValueToEnum(vRecord.Element("TipoArticuloInv").Value),
                                          Categoria = vRecord.Element("Categoria").Value,
                                          UnidadDeVenta = vRecord.Element("UnidadDeVenta").Value,
                                          AlicuotaIVA = vRecord.Element("AlicuotaIVA").Value
                                      }).Distinct();

            XElement vInfoConexionArticuloInventarioLote = FindInfoArticuloInventarioLote(refData);
            if (vInfoConexionArticuloInventarioLote == null) {
                vInfoConexionArticuloInventarioLote = new XElement("GpData",
                    new XElement("GpResult", new XElement("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"))
                    , new XElement("Consecutivo", -1), new XElement("CodigoLote", ""), new XElement("FechaDeElaboracion", ""), new XElement("FechaDeVencimiento", "")));
            }

            var vListArticuloInventarioLote = (from vRecordLote in vInfoConexionArticuloInventarioLote.Descendants("GpResult")
                                               select new {
                                                   ConsecutivoCompania = LibConvert.ToInt(vRecordLote.Element("ConsecutivoCompania")),
                                                   Consecutivo = LibConvert.ToInt(vRecordLote.Element("Consecutivo")),
                                                   CodigoLote = vRecordLote.Element("CodigoLote").Value,
                                                   FechaDeElaboracion = LibConvert.ToDate(vRecordLote.Element("FechaDeElaboracion").Value),
                                                   FechaDeVencimiento = LibConvert.ToDate(vRecordLote.Element("FechaDeVencimiento").Value)
                                               }).Distinct();

            foreach (OrdenDeProduccionDetalleMateriales vItem in refData) {
                var vItemAlmacen = vListAlmacen.Where(p => p.ConsecutivoAlmacen == vItem.ConsecutivoAlmacen).Select(p => p).FirstOrDefault();
                vItem.CodigoAlmacen = vItemAlmacen.CodigoAlmacen;
                vItem.NombreAlmacen = vItemAlmacen.NombreAlmacen;
                var vItemArticulo = vListArticuloInventario.Where(p => p.Codigo == vItem.CodigoArticulo).Select(p => p).FirstOrDefault();
                vItem.DescripcionArticulo = vItemArticulo.Descripcion;
                vItem.TipoDeArticuloAsEnum = vItemArticulo.TipoDeArticulo;
                vItem.UnidadDeVenta = vItemArticulo.UnidadDeVenta;
                var ItemLote = vListArticuloInventarioLote.Where(p => p.Consecutivo == vItem.ConsecutivoLoteDeInventario).FirstOrDefault();                
                vItem.TipoArticuloInvAsEnum = vItemArticulo.TipoArticuloInv;
                vItem.CodigoLote = ItemLote == null ? "" :ItemLote.CodigoLote;
                vItem.FechaDeVencimiento = ItemLote == null ? LibDate.EmptyDate() : ItemLote.FechaDeVencimiento;
                vItem.FechaDeElaboracion = ItemLote == null ? LibDate.EmptyDate() : ItemLote.FechaDeElaboracion;
            }
        }

        private XElement FindInfoAlmacen(IList<OrdenDeProduccionDetalleMateriales> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (OrdenDeProduccionDetalleMateriales vItem in valData) {
                vXElement.Add(FilterOrdenDeProduccionDetalleMaterialesByDistinctAlmacen(vItem).Descendants("GpResult"));
            }
            ILibPdn insAlmacen = new Galac.Saw.Brl.Inventario.clsAlmacenNav();
            XElement vXElementResult = insAlmacen.GetFk("OrdenDeProduccionDetalleMateriales", ParametersGetFKAlmacenForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterOrdenDeProduccionDetalleMaterialesByDistinctAlmacen(OrdenDeProduccionDetalleMateriales valRecord) {
            XElement vXElement = new XElement("GpData",
                new XElement("GpResult",
                    new XElement("Consecutivo", valRecord.ConsecutivoAlmacen)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKAlmacenForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }

        internal XElement FindInfoArticuloInventario(IList<OrdenDeProduccionDetalleMateriales> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (OrdenDeProduccionDetalleMateriales vItem in valData) {
                vXElement.Add(FilterOrdenDeProduccionDetalleMaterialesByDistinctArticuloInventario(vItem).Descendants("GpResult"));
            }
            ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            XElement vXElementResult = insArticuloInventario.GetFk("Orden de Producción", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterOrdenDeProduccionDetalleMaterialesByDistinctArticuloInventario(OrdenDeProduccionDetalleMateriales valRecord) {
            XElement vXElement = new XElement("GpData",
               new XElement("GpResult",
                    new XElement("Codigo", valRecord.CodigoArticulo)));
            return vXElement;
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



        #endregion //OrdenDeProduccionDetalleMateriales

        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IOrdenDeProduccionDetalleMaterialesPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<OrdenDeProduccionDetalleMateriales>, IList<OrdenDeProduccionDetalleMateriales>> instanciaDal = new clsOrdenDeProduccionDetalleMaterialesDat();
            IList<OrdenDeProduccionDetalleMateriales> vLista = new List<OrdenDeProduccionDetalleMateriales>();
            OrdenDeProduccionDetalleMateriales vCurrentRecord = new Galac.Adm.Dal.GestionProduccionOrdenDeProduccionDetalleMateriales();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.ConsecutivoOrdenDeProduccion = 0;
            vCurrentRecord.ConsecutivoOrdenDeProduccionDetalleArticulo = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.ConsecutivoAlmacen = 0;
            vCurrentRecord.ConsecutivoLoteDeInventario = 0;
            vCurrentRecord.CodigoArticulo = "";
            vCurrentRecord.Cantidad = 0;
            vCurrentRecord.CantidadReservadaInventario = 0;
            vCurrentRecord.CantidadConsumida = 0;
            vCurrentRecord.CostoUnitarioArticuloInventario = 0;
            vCurrentRecord.CostoUnitarioMEArticuloInventario = 0;
            vCurrentRecord.MontoSubtotal = 0;
            vCurrentRecord.AjustadoPostCierreAsBool = false;
            vCurrentRecord.CantidadAjustada = 0;
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<OrdenDeProduccionDetalleMateriales> ParseToListEntity(XElement valXmlEntity) {
            List<OrdenDeProduccionDetalleMateriales> vResult = new List<OrdenDeProduccionDetalleMateriales>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                OrdenDeProduccionDetalleMateriales vRecord = new OrdenDeProduccionDetalleMateriales();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoOrdenDeProduccion"), null))) {
                    vRecord.ConsecutivoOrdenDeProduccion = LibConvert.ToInt(vItem.Element("ConsecutivoOrdenDeProduccion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoOrdenDeProduccionDetalleArticulo"), null))) {
                    vRecord.ConsecutivoOrdenDeProduccionDetalleArticulo = LibConvert.ToInt(vItem.Element("ConsecutivoOrdenDeProduccionDetalleArticulo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoAlmacen"), null))) {
                    vRecord.ConsecutivoAlmacen = LibConvert.ToInt(vItem.Element("ConsecutivoAlmacen"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoLoteDeInventario"), null))) {
                    vRecord.ConsecutivoLoteDeInventario = LibConvert.ToInt(vItem.Element("ConsecutivoLoteDeInventario"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoArticulo"), null))) {
                    vRecord.CodigoArticulo = vItem.Element("CodigoArticulo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Cantidad"), null))) {
                    vRecord.Cantidad = LibConvert.ToDec(vItem.Element("Cantidad"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CantidadReservadaInventario"), null))) {
                    vRecord.CantidadReservadaInventario = LibConvert.ToDec(vItem.Element("CantidadReservadaInventario"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CantidadConsumida"), null))) {
                    vRecord.CantidadConsumida = LibConvert.ToDec(vItem.Element("CantidadConsumida"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CostoUnitarioArticuloInventario"), null))) {
                    vRecord.CostoUnitarioArticuloInventario = LibConvert.ToDec(vItem.Element("CostoUnitarioArticuloInventario"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CostoUnitarioMEArticuloInventario"), null))) {
                    vRecord.CostoUnitarioMEArticuloInventario = LibConvert.ToDec(vItem.Element("CostoUnitarioMEArticuloInventario"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoSubtotal"), null))) {
                    vRecord.MontoSubtotal = LibConvert.ToDec(vItem.Element("MontoSubtotal"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AjustadoPostCierre"), null))) {
                    vRecord.AjustadoPostCierre = vItem.Element("AjustadoPostCierre").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CantidadAjustada"), null))) {
                    vRecord.CantidadAjustada = LibConvert.ToDec(vItem.Element("CantidadAjustada"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo

        //internal ObservableCollection<OrdenDeProduccionDetalleMateriales> FillWithForeignInfo(OrdenDeProduccionDetalleArticulo vOrdenDeProduccionDetalleArticulo) {
        //    IList<OrdenDeProduccionDetalleMateriales> vList = vOrdenDeProduccionDetalleArticulo.DetailOrdenDeProduccionDetalleMateriales .ToList();
        //    FillWithForeignInfo(ref vList);
        //    return new ObservableCollection<OrdenDeProduccionDetalleMateriales>(vList);
        //}       

        public XElement BuscaExistenciaDeArticulos(int valConsecutivoCompania, IList<OrdenDeProduccionDetalleMateriales> valData) {
            XElement vXElementOut = new XElement("GpData");
            //XElement vXElementIn = new XElement("GpResult");
            foreach (OrdenDeProduccionDetalleMateriales vItem in valData) {
                vXElementOut.Add(new XElement("GpResult", new XElement("CodigoArticulo", vItem.CodigoArticulo), new XElement("ConsecutivoAlmacen", vItem.ConsecutivoAlmacen)));
            }
            //vXElementOut.Add(vXElementIn);
            IArticuloInventarioPdn insArticulo = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            XElement vXElementResult = insArticulo.DisponibilidadDeArticuloPorAlmacen(valConsecutivoCompania, vXElementOut);
            return vXElementResult;
        }

        XElement IOrdenDeProduccionDetalleMaterialesPdn.BuscaExistenciaDeArticulos(int valConsecutivoCompania, IList<OrdenDeProduccionDetalleMateriales> valData) {
            throw new NotImplementedException();
        }

        decimal IOrdenDeProduccionDetalleMaterialesPdn.BuscaExistenciaDeArticulo(int valConsecutivoCompania, string valCodigoArticulo, int valConsecutivoAlmacen) {
            decimal vResult = 0;
            QAdvSql vSqlUtil = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT Cantidad ");
            vSql.AppendLine("FROM ExistenciaPorAlmacen ");
            vSql.AppendLine("WHERE ConsecutivoCompania = " + vSqlUtil.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine(" AND ConsecutivoAlmacen = " + vSqlUtil.ToSqlValue(valConsecutivoAlmacen));
            vSql.AppendLine(" AND CodigoArticulo = " + vSqlUtil.ToSqlValue(valCodigoArticulo));
            XElement vXmlResult = LibBusiness.ExecuteSelect(vSql.ToString(), null, "", 0);
            if (vXmlResult != null && vXmlResult.HasElements) {
                string vCantidadStr = LibXml.GetPropertyString(vXmlResult, "Cantidad");
                vResult = LibImportData.ToDec(vCantidadStr);
            }
            return vResult;
        }

        private XElement FindInfoArticuloInventarioLote(IList<OrdenDeProduccionDetalleMateriales> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (OrdenDeProduccionDetalleMateriales vItem in valData) {
                vXElement.Add(FilterCompraDetalleArticuloInventarioByDistinctArticuloInventarioLote(vItem).Descendants("GpResult"));
            }
            ILibPdn insLoteDeInventario = new Galac.Saw.Brl.Inventario.clsLoteDeInventarioNav();
            XElement vXElementResult = insLoteDeInventario.GetFk("Compra", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterCompraDetalleArticuloInventarioByDistinctArticuloInventarioLote(OrdenDeProduccionDetalleMateriales valRecord) {
            XElement vXElement = new XElement("GpData",
                new XElement("GpResult",
                    new XElement("Consecutivo", valRecord.ConsecutivoLoteDeInventario)));
            return vXElement;
        }

    } //End of class clsOrdenDeProduccionDetalleMaterialesNav
} //End of namespace Galac.Adm.Brl.GestionProduccion

