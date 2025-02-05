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
    public partial class clsOrdenDeProduccionDetalleArticuloNav : LibBaseNavDetail<IList<OrdenDeProduccionDetalleArticulo>, IList<OrdenDeProduccionDetalleArticulo>>, IOrdenDeProduccionDetalleArticuloPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsOrdenDeProduccionDetalleArticuloNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<OrdenDeProduccionDetalleArticulo>, IList<OrdenDeProduccionDetalleArticulo>> GetDataInstance() {
            return new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionDetalleArticuloDat();
        }

        private void FillWithForeignInfo(ref IList<OrdenDeProduccionDetalleArticulo> refData) {
            FillWithForeignInfoOrdenDeProduccionDetalleArticulo(ref refData);
        }
        #region OrdenDeProduccionDetalleArticulo

        private void FillWithForeignInfoOrdenDeProduccionDetalleArticulo(ref IList<OrdenDeProduccionDetalleArticulo> refData) {
            XElement vInfoConexionAlmacen = FindInfoAlmacen(refData);
            var vListAlmacen = (from vRecord in vInfoConexionAlmacen.Descendants("GpResult")
                                select new {
                                    ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                    ConsecutivoAlmacen = LibConvert.ToInt(vRecord.Element("Consecutivo")),
                                    CodigoAlmancen = vRecord.Element("Codigo").Value,
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

            foreach (OrdenDeProduccionDetalleArticulo vItem in refData) {
                var vItemAlmacen = vListAlmacen.Where(p => p.ConsecutivoAlmacen == vItem.ConsecutivoAlmacen).Select(p => p).FirstOrDefault();
                vItem.CodigoAlmacen = vItemAlmacen.CodigoAlmancen;
                vItem.NombreAlmacen = vItemAlmacen.NombreAlmacen;                
                var vItemArticulo = vListArticuloInventario.Where(p => p.Codigo == vItem.CodigoArticulo).Select(p => p).FirstOrDefault();
                vItem.DescripcionArticulo = vItemArticulo.Descripcion;
                vItem.UnidadDeVenta = vItemArticulo.UnidadDeVenta;
                var ItemLote = vListArticuloInventarioLote.Where(p => p.Consecutivo == vItem.ConsecutivoLoteDeInventario).FirstOrDefault();
                vItem.TipoArticuloInvAsEnum  = vItemArticulo.TipoArticuloInv;
                vItem.CodigoLote = ItemLote == null ? "" : ItemLote.CodigoLote;
                vItem.FechaDeVencimiento = ItemLote == null ? LibDate.EmptyDate() : ItemLote.FechaDeVencimiento;
                vItem.FechaDeElaboracion = ItemLote == null ? LibDate.EmptyDate() : ItemLote.FechaDeElaboracion;
            }
        }

        internal XElement FindInfoArticuloInventario(IList<OrdenDeProduccionDetalleArticulo> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (OrdenDeProduccionDetalleArticulo vItem in valData) {
                vXElement.Add(FilterOrdenDeProduccionDetalleMaterialesByDistinctArticuloInventario(vItem).Descendants("GpResult"));
            }
            ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            XElement vXElementResult = insArticuloInventario.GetFk("Orden de Producción", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterOrdenDeProduccionDetalleMaterialesByDistinctArticuloInventario(OrdenDeProduccionDetalleArticulo valRecord) {
            XElement vXElement = new XElement("GpData",
               new XElement("GpResult",
                    new XElement("Codigo", valRecord.CodigoArticulo)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKListaDeMaterialesForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }


        private XElement FindInfoAlmacen(IList<OrdenDeProduccionDetalleArticulo> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (OrdenDeProduccionDetalleArticulo vItem in valData) {
                vXElement.Add(FilterOrdenDeProduccionDetalleArticuloByDistinctAlmacen(vItem).Descendants("GpResult"));
            }
            ILibPdn insAlmacen = new Galac.Saw.Brl.Inventario.clsAlmacenNav();
            XElement vXElementResult = insAlmacen.GetFk("OrdenDeProduccionDetalleArticulo", ParametersGetFKAlmacenForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterOrdenDeProduccionDetalleArticuloByDistinctAlmacen(OrdenDeProduccionDetalleArticulo valRecord) {
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

        private StringBuilder ParametersGetFKArticuloInventarioForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }

        #endregion //OrdenDeProduccionDetalleArticulo

        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IOrdenDeProduccionDetalleArticuloPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<OrdenDeProduccionDetalleArticulo>, IList<OrdenDeProduccionDetalleArticulo>> instanciaDal = new clsOrdenDeProduccionDetalleArticuloDat();
            IList<OrdenDeProduccionDetalleArticulo> vLista = new List<OrdenDeProduccionDetalleArticulo>();
            OrdenDeProduccionDetalleArticulo vCurrentRecord = new Galac.Adm.Dal.GestionProduccionOrdenDeProduccionDetalleArticulo();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.ConsecutivoOrdenDeProduccion = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.ConsecutivoAlmacen = 0;
            vCurrentRecord.ConsecutivoLoteDeInventario = 0;
            vCurrentRecord.CodigoArticulo = "";
            vCurrentRecord.CantidadOriginalLista = 0;
            vCurrentRecord.CantidadSolicitada = 0;
            vCurrentRecord.CantidadProducida = 0;
            vCurrentRecord.CostoUnitario = 0;
            vCurrentRecord.MontoSubTotal = 0;
            vCurrentRecord.AjustadoPostCierreAsBool = false;
            vCurrentRecord.CantidadAjustada = 0;
            vCurrentRecord.PorcentajeCostoEstimado = 0;
            vCurrentRecord.PorcentajeCostoCierre = 0;
            vCurrentRecord.Costo = 0;
            vCurrentRecord.PorcentajeMermaNormalOriginal = 0;
            vCurrentRecord.CantidadMermaNormal = 0;
            vCurrentRecord.PorcentajeMermaNormal = 0;
            vCurrentRecord.CantidadMermaAnormal = 0;
            vCurrentRecord.PorcentajeMermaAnormal = 0;
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<OrdenDeProduccionDetalleArticulo> ParseToListEntity(XElement valXmlEntity) {
            List<OrdenDeProduccionDetalleArticulo> vResult = new List<OrdenDeProduccionDetalleArticulo>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                OrdenDeProduccionDetalleArticulo vRecord = new OrdenDeProduccionDetalleArticulo();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoOrdenDeProduccion"), null))) {
                    vRecord.ConsecutivoOrdenDeProduccion = LibConvert.ToInt(vItem.Element("ConsecutivoOrdenDeProduccion"));
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
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CantidadOriginalLista"), null))) {
                    vRecord.CantidadOriginalLista = LibConvert.ToDec(vItem.Element("CantidadOriginalLista"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CantidadSolicitada"), null))) {
                    vRecord.CantidadSolicitada = LibConvert.ToDec(vItem.Element("CantidadSolicitada"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CantidadProducida"), null))) {
                    vRecord.CantidadProducida = LibConvert.ToDec(vItem.Element("CantidadProducida"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CostoUnitario"), null))) {
                    vRecord.CostoUnitario = LibConvert.ToDec(vItem.Element("CostoUnitario"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoSubTotal"), null))) {
                    vRecord.MontoSubTotal = LibConvert.ToDec(vItem.Element("MontoSubTotal"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AjustadoPostCierre"), null))) {
                    vRecord.AjustadoPostCierre = vItem.Element("AjustadoPostCierre").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CantidadAjustada"), null))) {
                    vRecord.CantidadAjustada = LibConvert.ToDec(vItem.Element("CantidadAjustada"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeCostoEstimado"), null))) {
                    vRecord.PorcentajeCostoEstimado = LibConvert.ToDec(vItem.Element("PorcentajeCostoEstimado"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeCostoCierre"), null))) {
                    vRecord.PorcentajeCostoCierre = LibConvert.ToDec(vItem.Element("PorcentajeCostoCierre"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Costo"), null))) {
                    vRecord.Costo = LibConvert.ToDec(vItem.Element("Costo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeMermaNormalOriginal"), null))) {
                    vRecord.PorcentajeMermaNormalOriginal = LibConvert.ToDec(vItem.Element("PorcentajeMermaNormalOriginal"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CantidadMermaNormal"), null))) {
                    vRecord.CantidadMermaNormal = LibConvert.ToDec(vItem.Element("CantidadMermaNormal"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeMermaNormal"), null))) {
                    vRecord.PorcentajeMermaNormal = LibConvert.ToDec(vItem.Element("PorcentajeMermaNormal"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CantidadMermaAnormal"), null))) {
                    vRecord.CantidadMermaAnormal = LibConvert.ToDec(vItem.Element("CantidadMermaAnormal"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeMermaAnormal"), null))) {
                    vRecord.PorcentajeMermaAnormal = LibConvert.ToDec(vItem.Element("PorcentajeMermaAnormal"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo

        internal ObservableCollection<OrdenDeProduccionDetalleArticulo> FillWithForeignInfo(OrdenDeProduccion valOrdenDeProduccion) {
            IList<OrdenDeProduccionDetalleArticulo> vList = valOrdenDeProduccion.DetailOrdenDeProduccionDetalleArticulo.ToList();
            FillWithForeignInfo(ref vList);
            return new ObservableCollection<OrdenDeProduccionDetalleArticulo>(vList);
        }
        /*
        public List<OrdenDeProduccionDetalleMateriales> ObtenerDetalleInicialDeListaDemateriales(int valConsecutivoCompania, int valConsecutivoListaDeMateriales, int valConsecutivoAlmacen, decimal valCantidadSolicitada) {
            IList<ListaDeMaterialesDetalleArticulo> vData;
            vData = new clsListaDeMaterialesDetalleArticuloNav().DetalleArticulos(valConsecutivoCompania, valConsecutivoListaDeMateriales);
            List<OrdenDeProduccionDetalleMateriales> vResult = new List<OrdenDeProduccionDetalleMateriales>();
            XElement vXElement = FilterOrdenDeProduccionDetalleArticuloByDistinctAlmacen(new OrdenDeProduccionDetalleArticulo() { ConsecutivoAlmacen = valConsecutivoAlmacen });
            //XElement vXElement = FilterOrdenDeProduccionDetalleArticuloByDistinctAlmacen(new OrdenDeProduccionDetalleArticulo() { ConsecutivoAlmacen = valConsecutivoAlmacen });
            //ILibPdn insAlmacen = new Galac.Saw.Brl.Inventario.clsAlmacenNav();
            //XElement vXElementResult = insAlmacen.GetFk("OrdenDeProduccionDetalleArticulo", ParametersGetFKAlmacenForXmlSubSet(valConsecutivoCompania, vXElement));
            //var vAlmacen = (from vRecord in vXElementResult.Descendants("GpResult")
            //                select new {
            //                    ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
            //                    Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo")),
            //                    Codigo = vRecord.Element("Codigo").Value,
            //                    NombreAlmacen = vRecord.Element("NombreAlmacen").Value
            //                }).FirstOrDefault();


            foreach (var item in vData) {
                vResult.Add(new OrdenDeProduccionDetalleMateriales() {
                    CodigoArticulo = item.CodigoArticuloInventario,
                    DescripcionArticulo = item.DescripcionArticuloInventario,
                    ConsecutivoAlmacen = valConsecutivoAlmacen,
                    Cantidad = item.Cantidad,
                    CantidadReservadaInventario = LibMath.RoundToNDecimals(item.Cantidad * valCantidadSolicitada, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales")),
                    CostoUnitarioArticuloInventario = 0,
                    MontoSubtotal = 0,
                    TipoDeArticuloAsEnum  = item.TipoDeArticuloAsEnum 
                });
            }
            return vResult;
        }	
        */

        private XElement FindInfoArticuloInventarioLote(IList<OrdenDeProduccionDetalleArticulo> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (OrdenDeProduccionDetalleArticulo vItem in valData) {
                vXElement.Add(FilterCompraDetalleArticuloInventarioByDistinctArticuloInventarioLote(vItem).Descendants("GpResult"));
            }
            ILibPdn insLoteDeInventario = new Galac.Saw.Brl.Inventario.clsLoteDeInventarioNav();
            XElement vXElementResult = insLoteDeInventario.GetFk("Compra", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterCompraDetalleArticuloInventarioByDistinctArticuloInventarioLote(OrdenDeProduccionDetalleArticulo valRecord) {
            XElement vXElement = new XElement("GpData",
                new XElement("GpResult",
                    new XElement("Consecutivo", valRecord.ConsecutivoLoteDeInventario)));
            return vXElement;
        }
    } //End of class clsOrdenDeProduccionDetalleArticuloNav

} //End of namespace Galac.Adm.Brl.GestionProduccion

