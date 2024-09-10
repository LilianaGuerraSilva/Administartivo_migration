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
using System.Security.Policy;

namespace Galac.Adm.Brl.GestionCompras {
    public partial class clsCompraDetalleArticuloInventarioNav: LibBaseNavDetail<IList<CompraDetalleArticuloInventario>, IList<CompraDetalleArticuloInventario>> {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCompraDetalleArticuloInventarioNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataDetailComponent<IList<CompraDetalleArticuloInventario>, IList<CompraDetalleArticuloInventario>> GetDataInstance() {
            return new Galac.Adm.Dal.GestionCompras.clsCompraDetalleArticuloInventarioDat();
        }

        internal  void FillWithForeignInfo(ref IList<Compra> refData) {
            FillWithForeignInfoCompraDetalleArticuloInventario(ref refData);
        }
        #region CompraDetalleArticuloInventario       
        private void FillWithForeignInfoCompraDetalleArticuloInventario(ref IList<Compra> refData) {
            XElement vInfoConexionArticuloInventario = FindInfoArticuloInventario(refData);
            XElement vInfoConexionArticuloInventarioLote = FindInfoArticuloInventarioLote(refData);
            var vListArticuloInventario = (from vRecord in vInfoConexionArticuloInventario.Descendants("GpResult")                                           
                                            
                                           select new {
                                               ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                               Codigo = vRecord.Element("Codigo").Value,
                                               Descripcion = vRecord.Element("Descripcion").Value,
                                               TipoDeArticuloInv = (eTipoArticuloInv)LibConvert.DbValueToEnum(vRecord.Element("TipoArticuloInv").Value),                                               
                                               CodigoCompuesto = vRecord.Element("CodigoCompuesto").Value,
                                               CodigoGrupo = vRecord.Element("CodigoGrupo").Value,
                                               TipoDeAlicuota = LibConvert.ToInt(vRecord.Element("AlicuotaIVA").Value),
                                               TipoDeArticulo = LibConvert.ToInt(vRecord.Element("TipoDeArticulo").Value)
                                               
                                           }).Distinct();
            if (vInfoConexionArticuloInventarioLote == null) {
                vInfoConexionArticuloInventarioLote = new XElement("GpData", 
                    new XElement("GpResult", new XElement("ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"))
                    , new XElement("Consecutivo", -1), new XElement("CodigoLote", ""), new XElement("FechaDeElaboracion",""), new XElement("FechaDeVencimiento", "")));
            }
                var vListArticuloInventarioLote = (from vRecordLote in vInfoConexionArticuloInventarioLote.Descendants("GpResult")
                                                   select new {
                                                       ConsecutivoCompania = LibConvert.ToInt(vRecordLote.Element("ConsecutivoCompania")),
                                                       Consecutivo = LibConvert.ToInt(vRecordLote.Element("Consecutivo")),
                                                       CodigoLote = vRecordLote.Element("CodigoLote").Value,
                                                       FechaDeElaboracion = LibConvert.ToDate(vRecordLote.Element("FechaDeElaboracion").Value),
                                                       FechaDeVencimiento = LibConvert.ToDate(vRecordLote.Element("FechaDeVencimiento").Value)
                                                   }).Distinct();
             




            
            foreach (Compra vItem in refData) {
                vItem.DetailCompraDetalleArticuloInventario =
                    new System.Collections.ObjectModel.ObservableCollection<CompraDetalleArticuloInventario>((
                        from vDetail in vItem.DetailCompraDetalleArticuloInventario
                        join vLote in vListArticuloInventarioLote
                                                           on vDetail.ConsecutivoLoteDeInventario 
                                                           equals vLote.Consecutivo into joinedResults
                        from joinresult in joinedResults.DefaultIfEmpty()
                        from vArticuloInventario in vListArticuloInventario
                        where (vDetail.ConsecutivoCompania == vArticuloInventario.ConsecutivoCompania                        
                        && (vDetail.CodigoArticulo == vArticuloInventario.Codigo || vDetail.CodigoArticulo == vArticuloInventario.CodigoCompuesto))
                      
                        select new CompraDetalleArticuloInventario {
                            ConsecutivoCompania = vDetail.ConsecutivoCompania,
                            ConsecutivoCompra = vDetail.ConsecutivoCompra,
                            Consecutivo = vDetail.Consecutivo,
                            CodigoArticulo = vDetail.CodigoArticulo,
                            Cantidad = vDetail.Cantidad,
                            PrecioUnitario = vDetail.PrecioUnitario,
                            CantidadRecibida = vDetail.CantidadRecibida,
                            MontoDistribucion = vDetail.MontoDistribucion,
                            PorcentajeDeDistribucion = vDetail.PorcentajeDeDistribucion,
                            PorcentajeSeguro = vDetail.PorcentajeSeguro,                             
                            DescripcionArticulo = vArticuloInventario.Descripcion,
                            TipoArticuloInv = vArticuloInventario.TipoDeArticuloInv,
                            CodigoGrupo = vArticuloInventario.CodigoGrupo,
                            TipoDeAlicuota = vArticuloInventario.TipoDeAlicuota,
                            TipoDeArticulo  = vArticuloInventario.TipoDeArticulo,
                            CostoUnitario = vDetail.PrecioUnitario,
                            ConsecutivoLoteDeInventario = vDetail.ConsecutivoLoteDeInventario,
                            CodigoLote = !System.NullReferenceException.ReferenceEquals(joinresult, null)? joinresult.CodigoLote:"",
                            FechaDeElaboracion = !System.NullReferenceException.ReferenceEquals(joinresult, null) ? joinresult.FechaDeElaboracion : LibDate.EmptyDate() ,  
                           FechaDeVencimiento = !System.NullReferenceException.ReferenceEquals(joinresult, null) ? joinresult.FechaDeVencimiento : LibDate.EmptyDate()

                        }).ToList<CompraDetalleArticuloInventario>());
            }
        }

        private XElement FindInfoArticuloInventario(IList<Compra> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (Compra vItem in valData) {
                vXElement.Add(FilterCompraDetalleArticuloInventarioByDistinctArticuloInventario(vItem).Descendants("GpResult"));
            }
            ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            XElement vXElementResult = insArticuloInventario.GetFk("Compra", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FindInfoArticuloInventarioLote(IList<Compra> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (Compra vItem in valData) {
                vXElement.Add(FilterCompraDetalleArticuloInventarioByDistinctArticuloInventarioLote(vItem).Descendants("GpResult"));
            }
            ILibPdn insLoteDeInventario = new Galac.Saw.Brl.Inventario.clsLoteDeInventarioNav();
            XElement vXElementResult = insLoteDeInventario.GetFk("Compra", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterCompraDetalleArticuloInventarioByDistinctArticuloInventario(Compra valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailCompraDetalleArticuloInventario.Distinct()
                select new XElement("GpResult",
                    new XElement("Codigo", vEntity.CodigoArticulo)));
            return vXElement;
        }

        private XElement FilterCompraDetalleArticuloInventarioByDistinctArticuloInventarioLote(Compra valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailCompraDetalleArticuloInventario.Distinct()
                select new XElement("GpResult",
                    new XElement("Consecutivo", vEntity.ConsecutivoLoteDeInventario)));
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
        #endregion //CompraDetalleArticuloInventario
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool ICompraDetalleArticuloInventarioPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<CompraDetalleArticuloInventario>, IList<CompraDetalleArticuloInventario>> instanciaDal = new clsCompraDetalleArticuloInventarioDat();
            IList<CompraDetalleArticuloInventario> vLista = new List<CompraDetalleArticuloInventario>();
            CompraDetalleArticuloInventario vCurrentRecord = new Galac.Adm.Dal.GestionComprasCompraDetalleArticuloInventario();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.ConsecutivoCompra = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.CodigoArticulo = "";
            vCurrentRecord.Cantidad = 0;
            vCurrentRecord.CostoUnitario = 0;
            vCurrentRecord.CantidadRecibida = 0;
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<CompraDetalleArticuloInventario> ParseToListEntity(XElement valXmlEntity) {
            List<CompraDetalleArticuloInventario> vResult = new List<CompraDetalleArticuloInventario>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                CompraDetalleArticuloInventario vRecord = new CompraDetalleArticuloInventario();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompra"), null))) {
                    vRecord.ConsecutivoCompra = LibConvert.ToInt(vItem.Element("ConsecutivoCompra"));
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


    } //End of class clsCompraDetalleArticuloInventarioNav

} //End of namespace Galac.Adm.Brl.GestionCompras

