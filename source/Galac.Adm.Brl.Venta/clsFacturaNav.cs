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
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Ccl.CajaChica;
using Microsoft.SqlServer.Server;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Brl.Venta {
    public partial class clsFacturaNav : LibBaseNavMaster<IList<Factura>, IList<Factura>>, IFacturaPdn {
        #region Variables
        #endregion //Variables

        #region Propiedades
        #endregion //Propiedades

        #region Constructores
        public clsFacturaNav() {
        }
        #endregion //Constructores

        #region Metodos Generados
        protected override ILibDataMasterComponentWithSearch<IList<Factura>, IList<Factura>> GetDataInstance() {
            return new Galac.Adm.Dal.Venta.clsFacturaDat();
        }

        #region Miembros de ILibPdn
        bool IFacturaPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsFacturaDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool IFacturaPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Dal.Venta.clsFacturaDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_FacturaSCH", valXmlParamsExpression);
        }

        XElement IFacturaPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<Factura>, IList<Factura>> instanciaDal = new Galac.Adm.Dal.Venta.clsFacturaDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_FacturaGetFk", valParameters);
        }
        #endregion //Miembros de IFacturaPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Factura":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Artículo Inventario":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
                    vResult = vPdnModule.GetDataForList("Factura", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Vendedor":
                    vPdnModule = new Galac.Adm.Brl.Vendedor.clsVendedorNav();
                    vResult = vPdnModule.GetDataForList("Factura", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Impresora Fiscal":
                    vPdnModule = new Galac.Saw.Brl.Tablas.clsMaquinaFiscalNav();
                    vResult = vPdnModule.GetDataForList("Factura", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Forma Del Cobro":
                    vPdnModule = new Galac.Saw.Brl.Tablas.clsFormaDelCobroNav();
                    vResult = vPdnModule.GetDataForList("Factura", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Banco":
                    vPdnModule = new Galac.Comun.Brl.TablasGen.clsBancoNav();
                    vResult = vPdnModule.GetDataForList("Factura", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Usuario":
                    vPdnModule = new LibGalac.Aos.Brl.Usal.LibGUserNav();
                    vResult = vPdnModule.GetDataForList("Factura", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<Factura> refData) {
            //FillWithForeignInfoRenglonFactura(ref refData);
            //FillWithForeignInfoRenglonCobroDeFactura(ref refData);
        }

        #region RenglonFactura
        /*private void FillWithForeignInfoRenglonFactura(ref IList<Factura> refData) {
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
            XElement vInfoConexionVendedor = FindInfoVendedor(refData);
            var vListVendedor = (from vRecord in vInfoConexionVendedor.Descendants("GpResult")
                                      select new {
                                          ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                          Codigo = vRecord.Element("Codigo").Value, 
                                          Nombre = vRecord.Element("Nombre").Value, 
                                          RIF = vRecord.Element("RIF").Value, 
                                          StatusVendedor = vRecord.Element("StatusVendedor").Value, 
                                          Direccion = vRecord.Element("Direccion").Value, 
                                          Ciudad = vRecord.Element("Ciudad").Value, 
                                          ZonaPostal = vRecord.Element("ZonaPostal").Value, 
                                          Telefono = vRecord.Element("Telefono").Value, 
                                          Fax = vRecord.Element("Fax").Value, 
                                          email = vRecord.Element("email").Value, 
                                          Notas = vRecord.Element("Notas").Value, 
                                          Comisiones = vRecord.Element("Comisiones").Value, 
                                          ComisionPorVenta = LibConvert.ToDec(vRecord.Element("ComisionPorVenta")), 
                                          ComisionPorCobro = LibConvert.ToDec(vRecord.Element("ComisionPorCobro")), 
                                          TopeInicialVenta1 = LibConvert.ToDec(vRecord.Element("TopeInicialVenta1")), 
                                          TopeFinalVenta1 = LibConvert.ToDec(vRecord.Element("TopeFinalVenta1")), 
                                          PorcentajeVentas1 = LibConvert.ToDec(vRecord.Element("PorcentajeVentas1")), 
                                          TopeFinalVenta2 = LibConvert.ToDec(vRecord.Element("TopeFinalVenta2")), 
                                          PorcentajeVentas2 = LibConvert.ToDec(vRecord.Element("PorcentajeVentas2")), 
                                          TopeFinalVenta3 = LibConvert.ToDec(vRecord.Element("TopeFinalVenta3")), 
                                          PorcentajeVentas3 = LibConvert.ToDec(vRecord.Element("PorcentajeVentas3")), 
                                          TopeFinalVenta4 = LibConvert.ToDec(vRecord.Element("TopeFinalVenta4")), 
                                          PorcentajeVentas4 = LibConvert.ToDec(vRecord.Element("PorcentajeVentas4")), 
                                          TopeFinalVenta5 = LibConvert.ToDec(vRecord.Element("TopeFinalVenta5")), 
                                          PorcentajeVentas5 = LibConvert.ToDec(vRecord.Element("PorcentajeVentas5")), 
                                          TopeInicialCobranza1 = LibConvert.ToDec(vRecord.Element("TopeInicialCobranza1")), 
                                          TopeFinalCobranza1 = LibConvert.ToDec(vRecord.Element("TopeFinalCobranza1")), 
                                          PorcentajeCobranza1 = LibConvert.ToDec(vRecord.Element("PorcentajeCobranza1")), 
                                          TopeFinalCobranza2 = LibConvert.ToDec(vRecord.Element("TopeFinalCobranza2")), 
                                          PorcentajeCobranza2 = LibConvert.ToDec(vRecord.Element("PorcentajeCobranza2")), 
                                          TopeFinalCobranza3 = LibConvert.ToDec(vRecord.Element("TopeFinalCobranza3")), 
                                          PorcentajeCobranza3 = LibConvert.ToDec(vRecord.Element("PorcentajeCobranza3")), 
                                          TopeFinalCobranza4 = LibConvert.ToDec(vRecord.Element("TopeFinalCobranza4")), 
                                          PorcentajeCobranza4 = LibConvert.ToDec(vRecord.Element("PorcentajeCobranza4")), 
                                          TopeFinalCobranza5 = LibConvert.ToDec(vRecord.Element("TopeFinalCobranza5")), 
                                          PorcentajeCobranza5 = LibConvert.ToDec(vRecord.Element("PorcentajeCobranza5")), 
                                          UsaComisionPorVenta = vRecord.Element("UsaComisionPorVenta").Value, 
                                          UsaComisionPorCobranza = vRecord.Element("UsaComisionPorCobranza").Value, 
                                          TipoDocumentoIdentificacion = vRecord.Element("TipoDocumentoIdentificacion").Value, 
                                          SeccionComisionesLinea = vRecord.Element("SeccionComisionesLinea").Value
                                      }).Distinct();
            foreach(Factura vItem in refData) {
                vItem.DetailRenglonFactura = 
                    new System.Collections.ObjectModel.ObservableCollection<RenglonFactura>((
                        from vDetail in vItem.DetailRenglonFactura
                        join vArticuloInventario in vListArticuloInventario
                        on new {Codigo = vDetail.Articulo, ConsecutivoCompania = vDetail.ConsecutivoCompania}
                        equals
                        new { Codigo = vArticuloInventario.Codigo, ConsecutivoCompania = vArticuloInventario.ConsecutivoCompania}
                        join vVendedor in vListVendedor
                        on new {Codigo = vDetail.CodigoVendedor1, ConsecutivoCompania = vDetail.ConsecutivoCompania}
                        equals
                        new { Codigo = vVendedor.Codigo, ConsecutivoCompania = vVendedor.ConsecutivoCompania}
                        on new {Codigo = vDetail.CodigoVendedor2, ConsecutivoCompania = vDetail.ConsecutivoCompania}
                        equals
                        new { Codigo = vVendedor.Codigo, ConsecutivoCompania = vVendedor.ConsecutivoCompania}
                        on new {Codigo = vDetail.CodigoVendedor3, ConsecutivoCompania = vDetail.ConsecutivoCompania}
                        equals
                        new { Codigo = vVendedor.Codigo, ConsecutivoCompania = vVendedor.ConsecutivoCompania}
                        select new RenglonFactura {
                            ConsecutivoCompania = vDetail.ConsecutivoCompania, 
                            NumeroFactura = vDetail.NumeroFactura, 
                            TipoDeDocumentoAsEnum = vDetail.TipoDeDocumentoAsEnum, 
                            ConsecutivoRenglon = vDetail.ConsecutivoRenglon, 
                            Articulo = vDetail.Articulo, 
                            Descripcion = vDetail.Descripcion, 
                            CodigoVendedor1 = vDetail.CodigoVendedor1, 
                            CodigoVendedor2 = vDetail.CodigoVendedor2, 
                            CodigoVendedor3 = vDetail.CodigoVendedor3, 
                            AlicuotaIVAAsEnum = vDetail.AlicuotaIVAAsEnum, 
                            Cantidad = vDetail.Cantidad, 
                            PrecioSinIVA = vDetail.PrecioSinIVA, 
                            PrecioConIVA = vDetail.PrecioConIVA, 
                            MontoBrutoSinIva = vDetail.MontoBrutoSinIva, 
                            MontoBrutoConIva = vDetail.MontoBrutoConIva, 
                            PorcentajeDescuento = vDetail.PorcentajeDescuento, 
                            MontoDescuento = vDetail.MontoDescuento, 
                            TotalRenglon = vDetail.TotalRenglon, 
                            PorcentajeBaseImponible = vDetail.PorcentajeBaseImponible, 
                            CampoExtraEnRenglonFactura1 = vDetail.CampoExtraEnRenglonFactura1, 
                            CampoExtraEnRenglonFactura2 = vDetail.CampoExtraEnRenglonFactura2, 
                            PorcentajeAlicuota = vDetail.PorcentajeAlicuota, 
                            Serial = vDetail.Serial, 
                            Rollo = vDetail.Rollo, 
                            Dimension1 = vDetail.Dimension1, 
                            Dimension2 = vDetail.Dimension2
                        }).ToList<RenglonFactura>());
            
        }*/

        /*private XElement FindInfoArticuloInventario(IList<Factura> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(Factura vItem in valData) {
                vXElement.Add(FilterRenglonFacturaByDistinctArticuloInventario(vItem).Descendants("GpResult"));
            }
            ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            XElement vXElementResult = insArticuloInventario.GetFk("Factura", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }*/

        /*private XElement FilterRenglonFacturaByDistinctArticuloInventario(Factura valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailRenglonFactura.Distinct()
                select new XElement("GpResult",
                    new XElement("Articulo", vEntity.Articulo)));
            return vXElement;
        }*/
        #endregion //RenglonCobroDeFactura

        private StringBuilder ParametersGetFKArticuloInventarioForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }

        /*private XElement FindInfoVendedor(IList<Factura> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(Factura vItem in valData) {
                vXElement.Add(FilterRenglonFacturaByDistinctVendedor(vItem).Descendants("GpResult"));
            }
            ILibPdn insVendedor = new Galac.Saw.Brl.Vendedor.clsVendedorNav();
            XElement vXElementResult = insVendedor.GetFk("Factura", ParametersGetFKVendedorForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }*/

        /*private XElement FilterRenglonFacturaByDistinctVendedor(Factura valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailRenglonFactura.Distinct()
                select new XElement("GpResult",
                    new XElement("CodigoVendedor1", vEntity.CodigoVendedor1)));
            return vXElement;
        }*/

        private StringBuilder ParametersGetFKVendedorForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }
        #endregion //RenglonFactura

        #region RenglonCobroDeFactura
        /*private void FillWithForeignInfoRenglonCobroDeFactura(ref IList<Factura> refData) {
            XElement vInfoConexionFormaDelCobro = FindInfoFormaDelCobro(refData);
            var vListFormaDelCobro = (from vRecord in vInfoConexionFormaDelCobro.Descendants("GpResult")
                                      select new {
                                          Codigo = vRecord.Element("Codigo").Value, 
                                          Nombre = vRecord.Element("Nombre").Value, 
                                          TipoDePago = vRecord.Element("TipoDePago").Value
                                      }).Distinct();
            XElement vInfoConexionBanco = FindInfoBanco(refData);
            var vListBanco = (from vRecord in vInfoConexionBanco.Descendants("GpResult")
                                      select new {
                                          Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo")), 
                                          Codigo = vRecord.Element("Codigo").Value, 
                                          Nombre = vRecord.Element("Nombre").Value, 
                                          Status = vRecord.Element("Status").Value, 
                                          fldOrigen = vRecord.Element("fldOrigen").Value
                                      }).Distinct();
            foreach(Factura vItem in refData) {
                vItem.DetailRenglonCobroDeFactura = 
                    new System.Collections.ObjectModel.ObservableCollection<RenglonCobroDeFactura>((
                        from vDetail in vItem.DetailRenglonCobroDeFactura
                        join vFormaDelCobro in vListFormaDelCobro
                        on new {Codigo = vDetail.CodigoFormaDelCobro}
                        equals
                        new { Codigo = vFormaDelCobro.Codigo}
                        join vBanco in vListBanco
                        on new {codigo = vDetail.CodigoBanco}
                        equals
                        new { codigo = vBanco.Codigo}
                        select new RenglonCobroDeFactura {
                            ConsecutivoCompania = vDetail.ConsecutivoCompania, 
                            NumeroFactura = vDetail.NumeroFactura, 
                            TipoDeDocumentoAsEnum = vDetail.TipoDeDocumentoAsEnum, 
                            ConsecutivoRenglon = vDetail.ConsecutivoRenglon, 
                            CodigoFormaDelCobro = vDetail.CodigoFormaDelCobro, 
                            NumeroDelDocumento = vDetail.NumeroDelDocumento, 
                            CodigoBanco = vDetail.CodigoBanco, 
                            Monto = vDetail.Monto, 
                            CodigoPuntoDeVenta = vDetail.CodigoPuntoDeVenta, 
                            NumeroDocumentoAprobacion = vDetail.NumeroDocumentoAprobacion, 
                            CodigoMoneda = vDetail.CodigoMoneda, 
                            CambioAMonedaLocal = vDetail.CambioAMonedaLocal
                        }).ToList<RenglonCobroDeFactura>());
            }
        }*/

        private XElement FindInfoFormaDelCobro(IList<Factura> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (Factura vItem in valData) {
                vXElement.Add(FilterRenglonCobroDeFacturaByDistinctFormaDelCobro(vItem).Descendants("GpResult"));
            }
            ILibPdn insFormaDelCobro = new Galac.Saw.Brl.Tablas.clsFormaDelCobroNav();
            XElement vXElementResult = insFormaDelCobro.GetFk("Factura", ParametersGetFKFormaDelCobroForXmlSubSet(vXElement));
            return vXElementResult;
        }

        private XElement FilterRenglonCobroDeFacturaByDistinctFormaDelCobro(Factura valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailRenglonCobroDeFactura.Distinct()
                select new XElement("GpResult",
                    new XElement("CodigoFormaDelCobro", vEntity.CodigoFormaDelCobro)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKFormaDelCobroForXmlSubSet(XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement FindInfoBanco(IList<Factura> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (Factura vItem in valData) {
                vXElement.Add(FilterRenglonCobroDeFacturaByDistinctBanco(vItem).Descendants("GpResult"));
            }
            ILibPdn insBanco = new Galac.Comun.Brl.TablasGen.clsBancoNav();
            XElement vXElementResult = insBanco.GetFk("Factura", ParametersGetFKBancoForXmlSubSet(vXElement));
            return vXElementResult;
        }

        private XElement FilterRenglonCobroDeFacturaByDistinctBanco(Factura valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailRenglonCobroDeFactura.Distinct()
                select new XElement("GpResult",
                    new XElement("CodigoBanco", vEntity.CodigoBanco)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKBancoForXmlSubSet(XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }

        public decimal BuscaAlicuotaImpTranscBancarias(DateTime valFechaVigencia, eTipoDeContribuyenteDelIva valTipoDeContribuyenteDelIva) {
            decimal vResult = 0;
            DateTime vFechaVigenciaIGTF = new DateTime(2022, 03, 28);
            const int vSujetoIGTF_0_4 = 0;
            const int vSujetoIGTF_0_6 = 3;
            Saw.Brl.Tablas.clsImpuestoBancarioNav insTablas = new Saw.Brl.Tablas.clsImpuestoBancarioNav();
            if (LibDate.F1IsGreaterOrEqualThanF2(valFechaVigencia, vFechaVigenciaIGTF)) {
                int vSujetoIGTF = valTipoDeContribuyenteDelIva == eTipoDeContribuyenteDelIva.ContribuyenteEspecial ? vSujetoIGTF_0_6 : vSujetoIGTF_0_4;
                vResult = LibConvert.ToDec(insTablas.BuscaAlicuotasReformaIGTFGO6687(valFechaVigencia, vSujetoIGTF), 2);
            } else {
                vResult = LibConvert.ToDec(insTablas.BuscaAlicuotaImpTranscBancarias(valFechaVigencia, true), 2);
            }
            return vResult;
        }

        string IFacturaPdn.MensajeDeNotificacionSiEsNecesario(string valActionStr, int valConsecutivoCompania, string valNumero, int valTipoDocumento) {
            bool vExistenNESinFacturar = new LibDatabase().RecordCountOfSql(SqlExistenNotasEntregaSinFacturar(valConsecutivoCompania)) > 0;
            if (vExistenNESinFacturar) {
                StringBuilder vMensaje = new StringBuilder();
                vMensaje.AppendLine(InformacionDelContribuyente(valConsecutivoCompania));

                return "prueba de envío de correo";
            } else {
                return "";
            }            
        }

        string InformacionDelContribuyente(int valConsecutivoCompania) {
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vMensaje = new StringBuilder();   
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT Nombre, NumeroDeRif, Ciudad, Estado, Telefono1, CodArea1, TipoDeContribuyenteIva FROM COMPANIA WHERE ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            XElement vExlement = LibBusiness.ExecuteSelect(vSql.ToString(), new StringBuilder(), string.Empty, 0);
            if (vExlement != null) {
                vMensaje.AppendLine("Se ha detectado que el contribuyente: " + LibXml.GetPropertyString(vExlement, "Nombre") + " - RIF: " + LibXml.GetPropertyString(vExlement, "NumeroDeRif"));
                vMensaje.Append("Domiciliado en la ciudad de " + LibXml.GetPropertyString(vExlement, "Ciudad") + " del estado " + LibXml.GetPropertyString(vExlement, "Estado"));
                vMensaje.AppendLine(". Teléfono: (" + LibXml.GetPropertyString(vExlement, "CodArea1") + ") " + LibXml.GetPropertyString(vExlement, "Telefono1"));
                vMensaje.AppendLine("Usuario del Sistema Administrativo de Gálac Software");
                vMensaje.AppendLine("Tiene al menos un registro de Nota de Entrega de al menos el mes anterior al actual, que debería ser facturado.");
                vMensaje.AppendLine();
                vMensaje.AppendLine();
                vMensaje.AppendLine("Información que se hace llegar al ente regulador en materia de impuesto para su conocimiento.");
                vMensaje.AppendLine();
                vMensaje.AppendLine("Correo automático generado desde la aplicación a través de una cuenta no monitoreada.");
                vMensaje.AppendLine();
            }
            return vMensaje.ToString();
        }

        string SqlExistenNotasEntregaSinFacturar(int valConsecutivoCompania) {
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            DateTime vFechaDesde = LibDate.DateFromMonthAndYear(1, 2005, true);
            DateTime vFechaHasta = LibDate.AddDays(LibDate.DateFromMonthAndYear(LibDate.Today().Month, LibDate.Today().Year, true), -1);
            vSql.AppendLine("");
            //'Notas de Entregas Emitidas y no Generadas
            vSql.AppendLine("SELECT Numero FROM Factura");
            vSql.AppendLine(" WHERE (EmitidaEnFacturaNumero = " + insSql.ToSqlValue("") + " OR EmitidaEnFacturaNumero IS NULL)");
            vSql.AppendLine(" AND Fecha  >= " + insSql.ToSqlValue(vFechaDesde));
            vSql.AppendLine(" AND Fecha  <= " + insSql.ToSqlValue(vFechaHasta));
            vSql.AppendLine(" AND StatusFactura = " + insSql.EnumToSqlValue((int)eStatusFactura.Emitida));
            vSql.AppendLine(" AND TipoDeDocumento = " + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaEntrega));
            vSql.AppendLine(" AND ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            //'Facturas Generadas desde Notas de Entrega y no Emitidas
            vSql.AppendLine(" UNION SELECT Numero FROM Factura");
            vSql.AppendLine(" WHERE GeneradaPorNotaEntrega = " + insSql.EnumToSqlValue(1));//enum_FacturaGeneraNotaEntrega.eFGNE_Generada
            vSql.AppendLine(" AND Fecha  >= " + insSql.ToSqlValue(vFechaDesde));
            vSql.AppendLine(" AND Fecha  <= " + insSql.ToSqlValue(vFechaHasta));
            vSql.AppendLine(" AND StatusFactura = " + insSql.EnumToSqlValue((int)eStatusFactura.Borrador));
            vSql.AppendLine(" AND TipoDeDocumento = " + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura));
            vSql.AppendLine(" AND ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            return vSql.ToString();
        }

        #endregion //Metodos Generados

    } //End of class clsFacturaNav

} //End of namespace Galac.Adm.Brl.Venta

