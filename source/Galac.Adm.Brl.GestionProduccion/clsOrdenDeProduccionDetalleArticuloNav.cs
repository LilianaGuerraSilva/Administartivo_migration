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
    public partial class clsOrdenDeProduccionDetalleArticuloNav: LibBaseNavDetail<IList<OrdenDeProduccionDetalleArticulo>, IList<OrdenDeProduccionDetalleArticulo>>, IOrdenDeProduccionDetalleArticuloPdn {
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
            FillWithForeignInfoOrdenDeProduccionDetalleMateriales(ref refData);
        }
        #region OrdenDeProduccionDetalleArticulo

        private void FillWithForeignInfoOrdenDeProduccionDetalleArticulo(ref IList<OrdenDeProduccionDetalleArticulo> refData) {
            XElement vInfoConexionListaDeMateriales = FindInfoListaDeMateriales(refData);
            var vListListaDeMateriales = (from vRecord in vInfoConexionListaDeMateriales.Descendants("GpResult")
                                          select new {
                                              ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                              Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo")),
                                              Codigo = vRecord.Element("Codigo").Value,
                                              Nombre = vRecord.Element("Nombre").Value,
                                              CodigoArticuloInventario = vRecord.Element("CodigoArticuloInventario").Value,
                                              DescripcionArticuloInventario = vRecord.Element("DescripcionArticuloInventario").Value,
                                              FechaCreacion = vRecord.Element("FechaCreacion").Value
                                          }).Distinct();
            XElement vInfoConexionAlmacen = FindInfoAlmacen(refData);
            var vListAlmacen = (from vRecord in vInfoConexionAlmacen.Descendants("GpResult")
                                select new {
                                    ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                    Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo")),
                                    Codigo = vRecord.Element("Codigo").Value,
                                    NombreAlmacen = vRecord.Element("NombreAlmacen").Value,
                                    TipoDeAlmacen = vRecord.Element("TipoDeAlmacen").Value,
                                    ConsecutivoCliente = LibConvert.ToInt(vRecord.Element("ConsecutivoCliente")),
                                    CodigoCc = vRecord.Element("CodigoCc").Value,
                                    Descripcion = vRecord.Element("Descripcion").Value
                                }).Distinct();

            foreach (OrdenDeProduccionDetalleArticulo vItem in refData) {
                var vItemListaDeMatriales = vListListaDeMateriales.Where(p => p.Consecutivo == vItem.ConsecutivoListaDeMateriales).Select(p => p).FirstOrDefault();
                vItem.CodigoListaDeMateriales = vItemListaDeMatriales.Codigo;
                vItem.NombreListaDeMateriales = vItemListaDeMatriales.Nombre;
                vItem.DescripcionArticulo = vItemListaDeMatriales.DescripcionArticuloInventario;
                var vItemAlmacen = vListAlmacen.Where(p => p.Consecutivo == vItem.ConsecutivoAlmacen).Select(p => p).FirstOrDefault();
                vItem.CodigoAlmacen = vItemAlmacen.Codigo;
                vItem.NombreAlmacen = vItemAlmacen.NombreAlmacen;                
            }
        }

        private XElement FindInfoListaDeMateriales(IList<OrdenDeProduccionDetalleArticulo> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (OrdenDeProduccionDetalleArticulo vItem in valData) {
                vXElement.Add(FilterOrdenDeProduccionDetalleArticuloByDistinctListaDeMateriales(vItem).Descendants("GpResult"));
            }
            ILibPdn insListaDeMateriales = new Galac.Adm.Brl.GestionProduccion.clsListaDeMaterialesNav();
            XElement vXElementResult = insListaDeMateriales.GetFk("OrdenDeProduccionDetalleArticulo", ParametersGetFKListaDeMaterialesForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterOrdenDeProduccionDetalleArticuloByDistinctListaDeMateriales(OrdenDeProduccionDetalleArticulo valRecord) {
            XElement vXElement = new XElement("GpData",
                    new XElement("GpResult",
                    new XElement("Consecutivo", valRecord.ConsecutivoListaDeMateriales)));
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
        #endregion //OrdenDeProduccionDetalleArticulo
        #region OrdenDeProduccionDetalleMateriales

        private void FillWithForeignInfoOrdenDeProduccionDetalleMateriales(ref IList<OrdenDeProduccionDetalleArticulo> refData) {            
            foreach (OrdenDeProduccionDetalleArticulo vItem in refData) {      
                vItem.DetailOrdenDeProduccionDetalleMateriales = new clsOrdenDeProduccionDetalleMaterialesNav().FillWithForeignInfo(vItem);
            }
        }
        #endregion //OrdenDeProduccionDetalleMateriales
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
            vCurrentRecord.ConsecutivoListaDeMateriales = 0;
            vCurrentRecord.ConsecutivoAlmacen = 0;
            vCurrentRecord.CodigoArticulo = "";
            vCurrentRecord.CantidadSolicitada = 0;
            vCurrentRecord.CantidadProducida = 0;
            vCurrentRecord.CostoUnitario = 0;
            vCurrentRecord.MontoSubTotal = 0;
            vCurrentRecord.AjustadoPostCierreAsBool = false;
            vCurrentRecord.CantidadAjustada = 0;
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
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoListaDeMateriales"), null))) {
                    vRecord.ConsecutivoListaDeMateriales = LibConvert.ToInt(vItem.Element("ConsecutivoListaDeMateriales"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoAlmacen"), null))) {
                    vRecord.ConsecutivoAlmacen = LibConvert.ToInt(vItem.Element("ConsecutivoAlmacen"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoArticulo"), null))) {
                    vRecord.CodigoArticulo = vItem.Element("CodigoArticulo").Value;
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

        public XElement BuscaExistenciaDeArticulos(int valConsecutivoCompania, IList<OrdenDeProduccionDetalleArticulo> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (OrdenDeProduccionDetalleArticulo vItem in valData) {
                vXElement.Add(FilterOrdenDeProduccionDetalleArticuloByDistinctArticulo(vItem).Descendants("GpResult"));
            }
            IArticuloInventarioPdn insArticulo = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav ();
            XElement vXElementResult = insArticulo.DisponibilidadDeArticuloPorAlmacen(valConsecutivoCompania, vXElement);
            return vXElementResult;
        }

        private XElement FilterOrdenDeProduccionDetalleArticuloByDistinctArticulo(OrdenDeProduccionDetalleArticulo valRecord) {
            List<XElement> vList = new List<XElement>();
            foreach (var item in valRecord.DetailOrdenDeProduccionDetalleMateriales ) {
                vList.Add(new XElement ("GpResult", new XElement("CodigoArticulo", item.CodigoArticulo), new XElement("ConsecutivoAlmacen", item.ConsecutivoAlmacen)));
            }
            XElement vXElement = new XElement("GpData", vList.ToArray());
            return vXElement;
        }
    } //End of class clsOrdenDeProduccionDetalleArticuloNav

} //End of namespace Galac.Adm.Brl.GestionProduccion

