using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Brl.Venta {
    public partial class clsContratoNav : LibBaseNavMaster<IList<Contrato>, IList<Contrato>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsContratoNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataMasterComponentWithSearch<IList<Contrato>, IList<Contrato>> GetDataInstance() {
            return new Galac.Adm.Dal.Venta.clsContratoDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.dbo.Dal.Venta.clsContratoDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsContratoDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_ContratoSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<Contrato>, IList<Contrato>> instanciaDal = new Galac.Adm.Dal.Venta.clsContratoDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "dbo.Gp_ContratoGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Contrato":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Cliente":
                    vPdnModule = new Galac.Saw.Brl.Cliente.clsClienteNav();
                    vResult = vPdnModule.GetDataForList("Contrato", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Vendedor":
                    vPdnModule = new Galac.Adm.Brl.Vendedor.clsVendedorNav();
                    vResult = vPdnModule.GetDataForList("Contrato", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Artículo Inventario":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
                    vResult = vPdnModule.GetDataForList("Contrato", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return vResult;
        }
        #region Contrato
        private StringBuilder ParametersGetFKClienteForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
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
        #endregion //Contrato
        #region RenglonContrato

        private void FillWithForeignInfoRenglonContrato(ref IList<Contrato> refData) {
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
                                               EstadísticasdeProducto = vRecord.Element("EstadísticasdeProducto").Value,
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
            foreach (Contrato vItem in refData) {
                vItem.DetailRenglonContrato =
                    new System.Collections.ObjectModel.ObservableCollection<RenglonContrato>((
                        from vDetail in vItem.DetailRenglonContrato
                        join vArticuloInventario in vListArticuloInventario
                        on new { Codigo = vDetail.Articulo, ConsecutivoCompania = vDetail.ConsecutivoCompania }
                        equals
                        new { Codigo = vArticuloInventario.Codigo, ConsecutivoCompania = vArticuloInventario.ConsecutivoCompania }
                        select new RenglonContrato {
                            ConsecutivoCompania = vDetail.ConsecutivoCompania,
                            NumeroContrato = vDetail.NumeroContrato,
                            ConsecutivoContrato = vDetail.ConsecutivoContrato,
                            Articulo = vDetail.Articulo,
                            Descripcion = vDetail.Descripcion,
                            Imponible = vDetail.Imponible,
                            Cantidad = vDetail.Cantidad,
                            PeriodicidadAsEnum = vDetail.PeriodicidadAsEnum,
                            PeriodoDeAplicacionAsEnum = vDetail.PeriodoDeAplicacionAsEnum,
                            FechaDeInicio = vDetail.FechaDeInicio,
                            FechaFinal = vDetail.FechaFinal,
                            FechaPrimeraFactura = vDetail.FechaPrimeraFactura
                        }).ToList<RenglonContrato>());
            }
        }

        private XElement FindInfoArticuloInventario(IList<Contrato> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (Contrato vItem in valData) {
                vXElement.Add(FilterRenglonContratoByDistinctArticuloInventario(vItem).Descendants("GpResult"));
            }
            ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            XElement vXElementResult = insArticuloInventario.GetFk("Contrato", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterRenglonContratoByDistinctArticuloInventario(Contrato valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailRenglonContrato.Distinct()
                select new XElement("GpResult",
                    new XElement("Articulo", vEntity.Articulo)));
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
        #endregion //RenglonContrato
        #endregion //Metodos Generados
    } //End of class clsContratoNav

} //End of namespace Galac.Dbo.Brl.Venta

