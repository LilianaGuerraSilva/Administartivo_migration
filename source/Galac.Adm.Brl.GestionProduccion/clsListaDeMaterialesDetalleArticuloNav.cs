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
using Galac.Saw.Brl.Inventario ;

namespace Galac.Adm.Brl.GestionProduccion {
    public partial class clsListaDeMaterialesDetalleArticuloNav: LibBaseNavDetail<IList<ListaDeMaterialesDetalleArticulo>, IList<ListaDeMaterialesDetalleArticulo>> {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsListaDeMaterialesDetalleArticuloNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<ListaDeMaterialesDetalleArticulo>, IList<ListaDeMaterialesDetalleArticulo>> GetDataInstance() {
            return new Galac.Adm.Dal.GestionProduccion.clsListaDeMaterialesDetalleArticuloDat();
        }

        private void FillWithForeignInfo(ref IList<ListaDeMaterialesDetalleArticulo> refData) {
            //FillWithForeignInfoListaDeMaterialesDetalleArticulo(ref refData);
        }
        #region ListaDeMaterialesDetalleArticulo

        private void FillWithForeignInfoListaDeMaterialesDetalleArticulo(ref IList<ListaDeMaterialesDetalleArticulo> refData) {
            XElement vInfoConexionArticuloInventario = FindInfoArticuloInventario(refData);
            var vListArticuloInventario = (from vRecord in vInfoConexionArticuloInventario.Descendants("GpResult")
                                      select new {
                                          ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")), 
                                          Codigo = vRecord.Element("Codigo").Value, 
                                          Descripcion = vRecord.Element("Descripcion").Value, 
                                          LineaDeProducto = vRecord.Element("LineaDeProducto").Value, 
                                          StatusdelArticulo = vRecord.Element("StatusdelArticulo").Value, 
                                          TipoDeArticulo = vRecord.Element("TipoDeArticulo").Value, 
                                          AlicuotaIVA = vRecord.Element("AlicuotaIVA").Value, 
                                          PrecioSinIVA = LibConvert.ToDec(vRecord.Element("PrecioSinIVA")), 
                                          PrecioConIVA = LibConvert.ToDec(vRecord.Element("PrecioConIVA")), 
                                          PrecioSinIVA2 = LibConvert.ToDec(vRecord.Element("PrecioSinIVA2")), 
                                          PrecioConIVA2 = LibConvert.ToDec(vRecord.Element("PrecioConIVA2")), 
                                          PrecioSinIVA3 = LibConvert.ToDec(vRecord.Element("PrecioSinIVA3")), 
                                          PrecioConIVA3 = LibConvert.ToDec(vRecord.Element("PrecioConIVA3")), 
                                          PrecioSinIVA4 = LibConvert.ToDec(vRecord.Element("PrecioSinIVA4")), 
                                          PrecioConIVA4 = LibConvert.ToDec(vRecord.Element("PrecioConIVA4")), 
                                          PorcentajeBaseImponible = LibConvert.ToDec(vRecord.Element("PorcentajeBaseImponible")), 
                                          CostoUnitario = LibConvert.ToDec(vRecord.Element("CostoUnitario")), 
                                          Existencia = LibConvert.ToDec(vRecord.Element("Existencia")), 
                                          CantidadMinima = LibConvert.ToDec(vRecord.Element("CantidadMinima")), 
                                          CantidadMaxima = LibConvert.ToDec(vRecord.Element("CantidadMaxima")), 
                                          EstadisticasdeProducto = vRecord.Element("EstadisticasdeProducto").Value, 
                                          Categoria = vRecord.Element("Categoria").Value, 
                                          TipoDeProducto = vRecord.Element("TipoDeProducto").Value, 
                                          NombrePrograma = vRecord.Element("NombrePrograma").Value, 
                                          OtrosDatos = vRecord.Element("OtrosDatos").Value, 
                                          Marca = vRecord.Element("Marca").Value, 
                                          FechaDeVencimiento = vRecord.Element("FechaDeVencimiento").Value, 
                                          UnidadDeVenta = vRecord.Element("UnidadDeVenta").Value, 
                                          CampoDefinible1 = vRecord.Element("CampoDefinible1").Value, 
                                          CampoDefinible2 = vRecord.Element("CampoDefinible2").Value, 
                                          CampoDefinible3 = vRecord.Element("CampoDefinible3").Value, 
                                          CampoDefinible4 = vRecord.Element("CampoDefinible4").Value, 
                                          CampoDefinible5 = vRecord.Element("CampoDefinible5").Value, 
                                          MeCostoUnitario = LibConvert.ToDec(vRecord.Element("MeCostoUnitario")), 
                                          UnidadDeVentaSecundaria = LibConvert.ToDec(vRecord.Element("UnidadDeVentaSecundaria")), 
                                          CodigoLote = vRecord.Element("CodigoLote").Value, 
                                          PorcentajeComision = LibConvert.ToDec(vRecord.Element("PorcentajeComision")), 
                                          ExcluirDeComision = vRecord.Element("ExcluirDeComision").Value, 
                                          CantArtReservado = LibConvert.ToDec(vRecord.Element("CantArtReservado")), 
                                          CodigoGrupo = vRecord.Element("CodigoGrupo").Value, 
                                          RetornaAAlmacen = vRecord.Element("RetornaAAlmacen").Value, 
                                          PedirDimension = vRecord.Element("PedirDimension").Value, 
                                          MargenGanancia = LibConvert.ToDec(vRecord.Element("MargenGanancia")), 
                                          MargenGanancia2 = LibConvert.ToDec(vRecord.Element("MargenGanancia2")), 
                                          MargenGanancia3 = LibConvert.ToDec(vRecord.Element("MargenGanancia3")), 
                                          MargenGanancia4 = LibConvert.ToDec(vRecord.Element("MargenGanancia4")), 
                                          TipoArticuloInv = vRecord.Element("TipoArticuloInv").Value, 
                                          ComisionaPorcentaje = vRecord.Element("ComisionaPorcentaje").Value, 
                                          UsaBalanza = vRecord.Element("UsaBalanza").Value
                                      }).Distinct();

            foreach (ListaDeMaterialesDetalleArticulo vItem in refData) {
                vItem.DescripcionArticuloInventario = vInfoConexionArticuloInventario.Descendants("GpResult")
                    .Where(p => p.Element("Codigo").Value == vItem.CodigoArticuloInventario)
                    .Select(p => p.Element("Descripcion").Value).FirstOrDefault();
                vItem.UnidadDeVenta = vInfoConexionArticuloInventario.Descendants("GpResult")
                    .Where(p => p.Element("Codigo").Value == vItem.CodigoArticuloInventario)
                    .Select(p => p.Element("UnidadDeVenta").Value).FirstOrDefault();
            }
        }

        private XElement FindInfoArticuloInventario(IList<ListaDeMaterialesDetalleArticulo> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(ListaDeMaterialesDetalleArticulo vItem in valData) {
                vXElement.Add(FilterListaDeMaterialesDetalleArticuloByDistinctArticuloInventario(vItem).Descendants("GpResult"));
            }
            ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            XElement vXElementResult = insArticuloInventario.GetFk("Lista de Materiales", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterListaDeMaterialesDetalleArticuloByDistinctArticuloInventario(ListaDeMaterialesDetalleArticulo valMaster) {
            XElement vXElement = new XElement("GpData","0");
                //from vEntity in valMaster.DetailListaDeMaterialesDetalleArticulo.Distinct()
                //select new XElement("GpResult",
                //    new XElement("CodigoArticuloInventario", vEntity.CodigoArticuloInventario)));
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

        internal IList<ListaDeMaterialesDetalleArticulo> DetalleArticulos(int valConsecutivoCompania, int valConsecutivoListaDeMateriales) {
            RegisterClient();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoListaDeMateriales", valConsecutivoListaDeMateriales);
            IList<ListaDeMaterialesDetalleArticulo> vList = _Db.GetData(eProcessMessageType.SpName, "ListaDeMaterialesDetalleArticuloSelDet", vParams.Get());
            return vList;
        }
        #endregion //ListaDeMaterialesDetalleArticulo
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IListaDeMaterialesDetalleArticuloPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<ListaDeMaterialesDetalleArticulo>, IList<ListaDeMaterialesDetalleArticulo>> instanciaDal = new clsListaDeMaterialesDetalleArticuloDat();
            IList<ListaDeMaterialesDetalleArticulo> vLista = new List<ListaDeMaterialesDetalleArticulo>();
            ListaDeMaterialesDetalleArticulo vCurrentRecord = new Galac.Saw.Dal.InventarioListaDeMaterialesDetalleArticulo();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.ConsecutivoListaDeMateriales = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.CodigoArticuloInventario = "";
            vCurrentRecord.Cantidad = 0;
            vCurrentRecord.MermaNormal = 0;
            vCurrentRecord.PorcentajeMermaNormal = 0;
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<ListaDeMaterialesDetalleArticulo> ParseToListEntity(XElement valXmlEntity) {
            List<ListaDeMaterialesDetalleArticulo> vResult = new List<ListaDeMaterialesDetalleArticulo>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                ListaDeMaterialesDetalleArticulo vRecord = new ListaDeMaterialesDetalleArticulo();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoListaDeMateriales"), null))) {
                    vRecord.ConsecutivoListaDeMateriales = LibConvert.ToInt(vItem.Element("ConsecutivoListaDeMateriales"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoArticuloInventario"), null))) {
                    vRecord.CodigoArticuloInventario = vItem.Element("CodigoArticuloInventario").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Cantidad"), null))) {
                    vRecord.Cantidad = LibConvert.ToDec(vItem.Element("Cantidad"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MermaNormal"), null))) {
                    vRecord.MermaNormal = LibConvert.ToDec(vItem.Element("MermaNormal"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeMermaNormal"), null))) {
                    vRecord.PorcentajeMermaNormal = LibConvert.ToDec(vItem.Element("PorcentajeMermaNormal"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo


    } //End of class clsListaDeMaterialesDetalleArticuloNav

} //End of namespace Galac.Adm.Brl.GestionProduccion

