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
using Galac.Saw.Ccl.Inventario;
using Galac.Adm.Ccl.GestionProduccion;

namespace Galac.Adm.Brl.GestionProduccion {
    public partial class clsListaDeMaterialesNav : LibBaseNavMaster<IList<ListaDeMateriales>, IList<ListaDeMateriales>>, IListaDeMaterialesPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsListaDeMaterialesNav() {
        }
        #endregion //Constructores
        #region Metodos Generados
        
        protected override ILibDataMasterComponentWithSearch<IList<ListaDeMateriales>, IList<ListaDeMateriales>> GetDataInstance() {
            return new Galac.Adm.Dal.GestionProduccion.clsListaDeMaterialesDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Inventario.clsListaDeMaterialesDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.GestionProduccion.clsListaDeMaterialesDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_ListaDeMaterialesSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<ListaDeMateriales>, IList<ListaDeMateriales>> instanciaDal = new Galac.Adm.Dal.GestionProduccion.clsListaDeMaterialesDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_ListaDeMaterialesGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            if (LibString.S1IsEqualToS2(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreParaMostrarListaDeMateriales"), valModule)) {
                valModule = "Lista de Materiales";
            }
            switch (valModule) {
                case "Lista de Materiales":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Artículo Inventario":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
                    vResult = vPdnModule.GetDataForList("Lista de Materiales", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<ListaDeMateriales> refData) {
            //FillWithForeignInfoListaDeMateriales(ref refData);
            //FillWithForeignInfoListaDeMaterialesDetalle(ref refData);
        }

        #endregion //Metodos Generados

        #region ListaDeMateriales

        private void FillWithForeignInfoListaDeMateriales(ref IList<ListaDeMateriales> refData) {
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

            //foreach (ListaDeMateriales vItem in refData) {
            //    //vItem.DescripcionArticuloInventario = vInfoConexionArticuloInventario.Descendants("GpResult")
            //    //    .Where(p => p.Element("Codigo").Value == vItem.CodigoArticuloInventario)
            //    //    .Select(p => p.Element("Descripcion").Value).FirstOrDefault();
            //}
        }

        private XElement FindInfoArticuloInventario(IList<ListaDeMateriales> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (ListaDeMateriales vItem in valData) {
                vXElement.Add(FilterListaDeMaterialesByDistinctArticuloInventario(vItem).Descendants("GpResult"));
            }
            ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            XElement vXElementResult = insArticuloInventario.GetFk("Lista de Materiales", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterListaDeMaterialesByDistinctArticuloInventario(ListaDeMateriales valMaster) {
            XElement vXElement = new XElement("GpData", "0");
            //from vEntity in valMaster.DetailListaDeMaterialesDetalle.Distinct()
            //select new XElement("GpResult",
            //    new XElement("DescripcionArticuloInventario", vEntity.DescripcionArticuloInventario));
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

        #endregion //ListaDeMateriales

        #region ListaDeMaterialesDetalleArticulo

        private void FillWithForeignInfoListaDeMaterialesDetalleArticulo(ref IList<ListaDeMateriales> refData) {
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
            foreach (ListaDeMateriales vItem in refData) {
                vItem.DetailListaDeMaterialesDetalleArticulo =
                    new System.Collections.ObjectModel.ObservableCollection<ListaDeMaterialesDetalleArticulo>((
                        from vDetail in vItem.DetailListaDeMaterialesDetalleArticulo
                        join vArticuloInventario in vListArticuloInventario
                        on new { Codigo = vDetail.CodigoArticuloInventario, ConsecutivoCompania = vDetail.ConsecutivoCompania }
                        equals
                        new { Codigo = vArticuloInventario.Codigo, ConsecutivoCompania = vArticuloInventario.ConsecutivoCompania }
                        select new ListaDeMaterialesDetalleArticulo {
                            ConsecutivoCompania = vDetail.ConsecutivoCompania,
                            ConsecutivoListaDeMateriales = vDetail.ConsecutivoListaDeMateriales,
                            Consecutivo = vDetail.Consecutivo,
                            CodigoArticuloInventario = vDetail.CodigoArticuloInventario,
                            DescripcionArticuloInventario = vArticuloInventario.Descripcion,
                            Cantidad = vDetail.Cantidad,
                            UnidadDeVenta = vArticuloInventario.UnidadDeVenta
                        }).ToList<ListaDeMaterialesDetalleArticulo>());
            }
        }

        #region ListaDeMaterialesDetalleSalidas

        private void FillWithForeignInfoListaDeMaterialesDetalleSalidas(ref IList<ListaDeMateriales> refData) {
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
            foreach (ListaDeMateriales vItem in refData) {
                vItem.DetailListaDeMaterialesDetalleSalidas =
                    new System.Collections.ObjectModel.ObservableCollection<ListaDeMaterialesDetalleSalidas>((
                        from vDetail in vItem.DetailListaDeMaterialesDetalleSalidas
                        join vArticuloInventario in vListArticuloInventario
                        on new { Codigo = vDetail.CodigoArticuloInventario, ConsecutivoCompania = vDetail.ConsecutivoCompania }
                        equals
                        new { Codigo = vArticuloInventario.Codigo, ConsecutivoCompania = vArticuloInventario.ConsecutivoCompania }
                        select new ListaDeMaterialesDetalleSalidas {
                            ConsecutivoCompania = vDetail.ConsecutivoCompania,
                            ConsecutivoListaDeMateriales = vDetail.ConsecutivoListaDeMateriales,
                            Consecutivo = vDetail.Consecutivo,
                            CodigoArticuloInventario = vDetail.CodigoArticuloInventario,
                            DescripcionArticuloInventario = vArticuloInventario.Descripcion,
                            Cantidad = vDetail.Cantidad,
                            UnidadDeVenta = vArticuloInventario.UnidadDeVenta,
                            PorcentajeDeCosto = vDetail.PorcentajeDeCosto
                        }).ToList<ListaDeMaterialesDetalleSalidas>());
            }
        }
        #endregion //ListaDeMaterialesDetalleSalidas

        XElement IListaDeMaterialesPdn.FindByConsecutivoCompaniaCodigo(int valConsecutivoCompania, string valCodigo) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Codigo", valCodigo, 30);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Adm.ListaDeMateriales");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND Codigo = @Codigo");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }

        XElement IListaDeMaterialesPdn.FindByConsecutivoCompaniaNombre(int valConsecutivoCompania, string valNombre) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Nombre", valNombre, 255);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Adm.ListaDeMateriales");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND Nombre = @Nombre");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }
        #endregion //Metodos Generados

        #region Código Programador
        string IListaDeMaterialesPdn.NombreParaMostrarListaDeMateriales() {
            string vResul = "Lista de Materiales";
            if (!LibString.IsNullOrEmpty(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("ParametrosCompania", "NombreParaMostrarListaDeMateriales"))) {
                vResul = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("ParametrosCompania", "NombreParaMostrarListaDeMateriales");
            }
            return vResul;
        }

        protected override LibResponse UpdateRecord(IList<ListaDeMateriales> refRecord, bool valUseDetail, eAccionSR valAction) {
            ValidaListaDeMateriaContraArticuloAProducir(refRecord);
            return base.UpdateRecord(refRecord, valUseDetail, valAction);
        }

        protected override LibResponse InsertRecord(IList<ListaDeMateriales> refRecord, bool valUseDetail) {
            ValidaListaDeMateriaContraArticuloAProducir(refRecord);
            return base.InsertRecord(refRecord, valUseDetail);
        }

        private void ValidaListaDeMateriaContraArticuloAProducir(IList<ListaDeMateriales> refRecord) {
            foreach (ListaDeMateriales vListaDeMateriales in refRecord) {
                if (vListaDeMateriales.DetailListaDeMaterialesDetalleArticulo != null && vListaDeMateriales.DetailListaDeMaterialesDetalleArticulo.Count > 0) {
                    if (vListaDeMateriales.DetailListaDeMaterialesDetalleArticulo.Count(p => p.CodigoArticuloInventario == vListaDeMateriales.CodigoArticuloInventario) > 0) {
                        throw new LibGalac.Aos.Catching.GalacValidationException("En la lista de materiales/servicios a utilizar se encuentra el articulo a producir.");
                    }
                }
            }
        }

        public bool ExisteListaDeMaterialesConEsteCodigo(int valConsecutivoCompania, string valCodigo) {
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Codigo", valCodigo, 30);
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Adm.ListaDeMateriales SET Codigo = Codigo WHERE Codigo = @Codigo AND ConsecutivoCompania = @ConsecutivoCompania");
            vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) > 0;
            return vResult;
        }

        public bool ExisteListaDeMaterialesConEsteNombre(int valConsecutivoCompania, string valNombre) {
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Nombre", valNombre.Trim(), 30);
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Adm.ListaDeMateriales SET Codigo = Codigo WHERE Nombre = @Nombre AND ConsecutivoCompania = @ConsecutivoCompania");
            vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) > 0;
            return vResult;
        }
        #endregion //Código Programador

    } //End of class clsListaDeMaterialesNav

} //End of namespace Galac.Adm.Brl.GestionProduccion