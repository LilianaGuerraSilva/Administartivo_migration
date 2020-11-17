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

namespace Galac.Adm.Brl.GestionCompras {
    public partial class clsOrdenDeCompraDetalleArticuloInventarioNav: LibBaseNavDetail<IList<OrdenDeCompraDetalleArticuloInventario>, IList<OrdenDeCompraDetalleArticuloInventario>> {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsOrdenDeCompraDetalleArticuloInventarioNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<OrdenDeCompraDetalleArticuloInventario>, IList<OrdenDeCompraDetalleArticuloInventario>> GetDataInstance() {
            return new Galac.Adm.Dal.GestionCompras.clsOrdenDeCompraDetalleArticuloInventarioDat();
        }

        private void FillWithForeignInfo(ref IList<OrdenDeCompraDetalleArticuloInventario> refData) {
            FillWithForeignInfoOrdenDeCompraDetalleArticuloInventario(ref refData);
        }
        #region OrdenDeCompraDetalleArticuloInventario

        private void FillWithForeignInfoOrdenDeCompraDetalleArticuloInventario(ref IList<OrdenDeCompraDetalleArticuloInventario> refData) {
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
                                          Existencia = LibConvert.ToDec(vRecord.Element("Existencia")), 
                                          Categoria = vRecord.Element("Categoria").Value, 
                                          Marca = vRecord.Element("Marca").Value, 
                                          FechaDeVencimiento = vRecord.Element("FechaDeVencimiento").Value, 
                                          UnidadDeVenta = vRecord.Element("UnidadDeVenta").Value, 
                                          TipoArticuloInv = vRecord.Element("TipoArticuloInv").Value
                                      }).Distinct();

            foreach (OrdenDeCompraDetalleArticuloInventario vItem in refData) {
            }
        }

        private XElement FindInfoArticuloInventario(IList<OrdenDeCompraDetalleArticuloInventario> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(OrdenDeCompraDetalleArticuloInventario vItem in valData) {
                vXElement.Add(FilterOrdenDeCompraDetalleArticuloInventarioByDistinctArticuloInventario(vItem).Descendants("GpResult"));
            }
            ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            XElement vXElementResult = insArticuloInventario.GetFk("OrdenDeCompraDetalleArticuloInventario", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterOrdenDeCompraDetalleArticuloInventarioByDistinctArticuloInventario(OrdenDeCompraDetalleArticuloInventario vEntity) {
            XElement vXElement = new XElement("GpData",
               new XElement("GpResult",
                    new XElement("CodigoArticulo", vEntity.CodigoArticulo)));
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
        #endregion //OrdenDeCompraDetalleArticuloInventario
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IOrdenDeCompraDetalleArticuloInventarioPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<OrdenDeCompraDetalleArticuloInventario>, IList<OrdenDeCompraDetalleArticuloInventario>> instanciaDal = new clsOrdenDeCompraDetalleArticuloInventarioDat();
            IList<OrdenDeCompraDetalleArticuloInventario> vLista = new List<OrdenDeCompraDetalleArticuloInventario>();
            OrdenDeCompraDetalleArticuloInventario vCurrentRecord = new Galac.Adm.Dal.GestionComprasOrdenDeCompraDetalleArticuloInventario();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.ConsecutivoOrdenDeCompra = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.CodigoArticulo = "";
            vCurrentRecord.Cantidad = 0;
            vCurrentRecord.CostoUnitario = 0;
            vCurrentRecord.CantidadRecibida = 0;
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<OrdenDeCompraDetalleArticuloInventario> ParseToListEntity(XElement valXmlEntity) {
            List<OrdenDeCompraDetalleArticuloInventario> vResult = new List<OrdenDeCompraDetalleArticuloInventario>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                OrdenDeCompraDetalleArticuloInventario vRecord = new OrdenDeCompraDetalleArticuloInventario();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoOrdenDeCompra"), null))) {
                    vRecord.ConsecutivoOrdenDeCompra = LibConvert.ToInt(vItem.Element("ConsecutivoOrdenDeCompra"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoArticulo"), null))) {
                    vRecord.CodigoArticulo = vItem.Element("CodigoArticulo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Cantidad"), null))) {
                    vRecord.Cantidad = LibConvert.ToDec(vItem.Element("Cantidad"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CostoUnitario"), null))) {
                    vRecord.CostoUnitario = LibConvert.ToDec(vItem.Element("CostoUnitario"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CantidadRecibida"), null))) {
                    vRecord.CantidadRecibida = LibConvert.ToDec(vItem.Element("CantidadRecibida"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo

        internal void ActualizarCantidadRecibida(int valConsecutivoCompania, int valConsecutivoOrdenDeCompra, string valCodigoArticulo, decimal valCantidad) {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("UPDATE Adm.OrdenDeCompraDetalleArticuloInventario SET CantidadRecibida = CantidadRecibida + @Cantidad");
            vSQL.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoOrdenDeCompra = @ConsecutivoOrdendeCompra AND CodigoArticulo = @CodigoArticulo");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoOrdenDeCompra", valConsecutivoOrdenDeCompra);
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            vParams.AddInDecimal("Cantidad", valCantidad, 4);
            LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0);
            
            
        }


      
    } //End of class clsOrdenDeCompraDetalleArticuloInventarioNav

} //End of namespace Galac.Adm.Brl.GestionCompras

