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
using LibGalac.Aos.DefGen;
using System.IO;
using Galac.Comun.Brl.TablasGen;
using Galac.Comun.Ccl.TablasGen;

namespace Galac.Adm.Brl.GestionCompras {
    public partial class clsOrdenDeCompraNav : LibBaseNavMaster<IList<OrdenDeCompra>, IList<OrdenDeCompra>>, IOrdenDeCompraPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsOrdenDeCompraNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataMasterComponentWithSearch<IList<OrdenDeCompra>, IList<OrdenDeCompra>> GetDataInstance() {
            return new Galac.Adm.Dal.GestionCompras.clsOrdenDeCompraDat();
        }
        #region Miembros de ILibPdn

        protected override bool CanBeChoosenForAction(IList<OrdenDeCompra> refRecord, eAccionSR valAction) {
            bool vResult = base.CanBeChoosenForAction(refRecord, valAction);
            if (valAction == eAccionSR.Eliminar) {
                if (!SePuedeEliminarOrdenDeCompra(refRecord)) {
                    throw new LibGalac.Aos.Catching.GalacAlertException("No se puede eliminar una Orden de Compra que este siendo usada en el módulo de Compras");
                }
            }
            return vResult;
        }
        private bool SePuedeEliminarOrdenDeCompra(IList<OrdenDeCompra> refRecord) {
            return !new clsCompraNav().ExistenRegistroConOrdenDeCompra(refRecord[0].ConsecutivoCompania, refRecord[0].Consecutivo);
        }
        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.GestionCompras.clsOrdenDeCompraDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }
        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.GestionCompras.clsOrdenDeCompraDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_OrdenDeCompraSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<OrdenDeCompra>, IList<OrdenDeCompra>> instanciaDal = new Galac.Adm.Dal.GestionCompras.clsOrdenDeCompraDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_OrdenDeCompraGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Orden de Compra Nacional":
                case "Orden de Compra Importación":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Proveedor":
                    vPdnModule = new Galac.Adm.Brl.GestionCompras.clsProveedorNav();
                    vResult = vPdnModule.GetDataForList("Orden De Compra", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Condiciones De Pago":
                    vPdnModule = (new Galac.Saw.Brl.Tablas.clsCondicionesDePagoNav());
                    vResult = vPdnModule.GetDataForList("Orden De Compra", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Articulo Inventario":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
                    vResult = vPdnModule.GetDataForList("Orden De Compra", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Moneda":
                    vPdnModule = new Galac.Comun.Brl.TablasGen.clsMonedaNav();
                    vResult = vPdnModule.GetDataForList("Compra", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Cotizacion":
                    //vPdnModule = new Galac.Adm.Brl.Venta.clsCotizacionNav();
                    //vResult = vPdnModule.GetDataForList("Orden De Compra", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<OrdenDeCompra> refData) {
            FillWithForeignInfoOrdenDeCompra(ref refData);
            //FillWithForeignInfoOrdenDeCompraDetalleArticuloInventario(ref refData);
        }
        #region OrdenDeCompra

        private void FillWithForeignInfoOrdenDeCompra(ref IList<OrdenDeCompra> refData) {
            XElement vInfoConexionProveedor = FindInfoProveedor(refData);
            var vListProveedor = (from vRecord in vInfoConexionProveedor.Descendants("GpResult")
                                  select new {
                                      ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                      CodigoProveedor = vRecord.Element("CodigoProveedor").Value,
                                      Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo")),
                                      NombreProveedor = vRecord.Element("NombreProveedor").Value,

                                      TipodeProveedor = vRecord.Element("TipodeProveedor").Value

                                  }).Distinct();

            foreach (OrdenDeCompra vItem in refData) {
                var vProveedor = vListProveedor.Where(p => p.Consecutivo == vItem.ConsecutivoProveedor).Select(p => p).FirstOrDefault();
                vItem.CodigoProveedor = vProveedor.CodigoProveedor;
                vItem.NombreProveedor = vProveedor.NombreProveedor;
            }
        }

        private XElement FindInfoProveedor(IList<OrdenDeCompra> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (OrdenDeCompra vItem in valData) {
                vXElement.Add(FilterOrdenDeCompraByDistinctProveedor(vItem).Descendants("GpResult"));
            }
            ILibPdn insProveedor = new Galac.Adm.Brl.GestionCompras.clsProveedorNav();
            XElement vXElementResult = insProveedor.GetFk("OrdenDeCompra", ParametersGetFKProveedorForXmlSubSet(valData[0].ConsecutivoCompania, vXElement, valData[0].ConsecutivoProveedor));
            return vXElementResult;
        }

        private XElement FilterOrdenDeCompraByDistinctProveedor(OrdenDeCompra valMaster) {
            XElement vXElement = new XElement("GpData",
                    new XElement("GpResult",
                    new XElement("ConsecutivoProveedor", valMaster.ConsecutivoProveedor)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKProveedorForXmlSubSet(int valConsecutivoCompania, XElement valXElement, int valConsecutivoProveedor) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            //vParams.AddInInteger("ConsecutivoProveedor",valConsecutivoProveedor);
            // vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }
        //private XElement FindInfoCotizacion(IList<OrdenDeCompra> valData) {
        //    XElement vXElement = new XElement("GpData");
        //    foreach(OrdenDeCompra vItem in valData) {
        //        vXElement.Add(FilterOrdenDeCompraByDistinctCotizacion(vItem).Descendants("GpResult"));
        //    }
        //    ILibPdn insCotizacion = new Galac.Comun.Brl.SttDef.clsCotizacionNav();
        //    XElement vXElementResult = insCotizacion.GetFk("OrdenDeCompra", ParametersGetFKCotizacionForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
        //    return vXElementResult;
        //}
        private StringBuilder ParametersGetFKCotizacionForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }
        //private XElement FindInfoCondicionesDePago(IList<OrdenDeCompra> valData) {
        //    XElement vXElement = new XElement("GpData");
        //    foreach(OrdenDeCompra vItem in valData) {
        //        vXElement.Add(FilterOrdenDeCompraByDistinctCondicionesDePago(vItem).Descendants("GpResult"));
        //    }
        //    ILibPdn insCondicionesDePago = new Galac.Saw.Brl.Tablas.clsCondicionesDePagoNav();
        //    XElement vXElementResult = insCondicionesDePago.GetFk("OrdenDeCompra", ParametersGetFKCondicionesDePagoForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
        //    return vXElementResult;
        //}

        private StringBuilder ParametersGetFKCondicionesDePagoForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }
        #endregion //OrdenDeCompra
        #region OrdenDeCompraDetalleArticuloInventario

        private void FillWithForeignInfoOrdenDeCompraDetalleArticuloInventario(ref IList<OrdenDeCompra> refData) {
            XElement vInfoConexionArticuloInventario = FindInfoArticuloInventario(refData);
            var vListArticuloInventario = (from vRecord in vInfoConexionArticuloInventario.Descendants("GpResult")
                                           select new {
                                               ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                               Codigo = vRecord.Element("Codigo").Value,
                                               Descripcion = vRecord.Element("Descripcion").Value,
                                               TipoDeArticuloInv = (eTipoArticuloInv)LibConvert.DbValueToEnum(vRecord.Element("TipoArticuloInv").Value),
                                               CodigoCompuesto = vRecord.Element("CodigoCompuesto").Value,
                                               CodigoGrupo = vRecord.Element("CodigoGrupo").Value,

                                           }).Distinct();
            foreach (OrdenDeCompra vItem in refData) {
                vItem.DetailOrdenDeCompraDetalleArticuloInventario =
                    new System.Collections.ObjectModel.ObservableCollection<OrdenDeCompraDetalleArticuloInventario>((
                        from vDetail in vItem.DetailOrdenDeCompraDetalleArticuloInventario
                        from vArticuloInventario in vListArticuloInventario
                        where (vDetail.ConsecutivoCompania == vArticuloInventario.ConsecutivoCompania
                        && (vDetail.CodigoArticulo == vArticuloInventario.Codigo || vDetail.CodigoArticulo == vArticuloInventario.CodigoCompuesto))

                        select new OrdenDeCompraDetalleArticuloInventario {
                            ConsecutivoCompania = vDetail.ConsecutivoCompania,
                            ConsecutivoOrdenDeCompra = vDetail.ConsecutivoOrdenDeCompra,
                            Consecutivo = vDetail.Consecutivo,
                            CodigoArticulo = vDetail.CodigoArticulo,
                            DescripcionArticulo = vDetail.DescripcionArticulo,
                            Cantidad = vDetail.Cantidad,
                            CostoUnitario = vDetail.CostoUnitario,
                            CantidadRecibida = vDetail.CantidadRecibida,
                            TipoArticuloInv = vArticuloInventario.TipoDeArticuloInv,
                            CodigoGrupo = vArticuloInventario.CodigoGrupo
                        }).ToList<OrdenDeCompraDetalleArticuloInventario>());
            }
        }
        private XElement FindInfoArticuloInventario(IList<OrdenDeCompra> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (OrdenDeCompra vItem in valData) {
                vXElement.Add(FilterOrdenDeCompraDetalleArticuloInventarioByDistinctArticuloInventario(vItem).Descendants("GpResult"));
            }
            ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            XElement vXElementResult = insArticuloInventario.GetFk("OrdenDeCompra", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }
        private XElement FilterOrdenDeCompraDetalleArticuloInventarioByDistinctArticuloInventario(OrdenDeCompra valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailOrdenDeCompraDetalleArticuloInventario.Distinct()
                select new XElement("GpResult",
                    new XElement("Codigo", vEntity.CodigoArticulo)));
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
        XElement IOrdenDeCompraPdn.FindBySerie(int valConsecutivoCompania, string valSerie) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Serie", valSerie, 20);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Adm.OrdenDeCompra");
            SQL.AppendLine("WHERE Serie = @Serie");
            SQL.AppendLine("AND ConsecutivoCompania = @ConsecutivoCompania");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }
        XElement IOrdenDeCompraPdn.FindByNumero(int valConsecutivoCompania, string valNumero) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Numero", valNumero, 20);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Adm.OrdenDeCompra");
            SQL.AppendLine("WHERE Numero = @Numero");
            SQL.AppendLine("AND ConsecutivoCompania = @ConsecutivoCompania");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }
        XElement IOrdenDeCompraPdn.FindByConsecutivoCompaniaSerieNumeroConsecutivoProveedor(int valConsecutivoCompania, string valSerie, string valNumero, int valConsecutivoProveedor) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Serie", valSerie, 20);
            vParams.AddInString("Numero", valNumero, 20);
            vParams.AddInInteger("ConsecutivoProveedor", valConsecutivoProveedor);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Adm.OrdenDeCompra");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND Serie = @Serie");
            SQL.AppendLine("AND Numero = @Numero");
            SQL.AppendLine("AND ConsecutivoProveedor = @ConsecutivoProveedor");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }
        string IOrdenDeCompraPdn.FindNextNumeroBySerieYTipoDeCompra(int valConsecutivoCompania, string valSerie, eTipoCompra valTipoDeCompra) {
            LibGpParams vParams = new LibGpParams();
            XElement vXMLOrdenDeCompra;
            QAdvSql vAdvSql = new QAdvSql("");
            string Numero = "";
            int LongitudParaCorte = 0;
            string vSelectMax = "";
            vParams.AddInString("Serie", valSerie, 20);
            vParams.AddInEnum("TipoDeCompra", (int)valTipoDeCompra);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                vSelectMax = " SELECT ( ISNULL ( MAX( CAST(SUBSTRING(OrdenDeCompra.Numero,4,LEN(OrdenDeCompra.Numero)) AS int) ) , 0 )  +1) AS  Numero FROM Adm.OrdenDeCompra ";
            } else {
                vSelectMax = " SELECT(" + vAdvSql.IsNull("MAX(" + vAdvSql.ToInt("OrdenDeCompra.Numero") + ")", "0") + " + 1) AS Numero FROM Adm.OrdenDeCompra ";
            }
            SQL.AppendLine(vSelectMax);
            SQL.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            SQL.AppendLine("AND TipoDeCompra = @TipoDeCompra ");
            SQL.AppendLine("AND ISNUMERIC(SUBSTRING(OrdenDeCompra.Numero,4,LEN(OrdenDeCompra.Numero)))=1");
            if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                SQL.AppendLine(" AND Serie = @Serie ");
            }
            vXMLOrdenDeCompra = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
            Numero = LibConvert.IntToStr2((int)LibConvert.ToDouble(LibXml.GetPropertyString(vXMLOrdenDeCompra, "Numero")));
            LongitudParaCorte = 8 - Numero.Length;
            for (int i = 0; i < LongitudParaCorte; i++) Numero = "0" + Numero;
            return Numero;
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IOrdenDeCompraPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<OrdenDeCompra>, IList<OrdenDeCompra>> instanciaDal = new clsOrdenDeCompraDat();
            IList<OrdenDeCompra> vLista = new List<OrdenDeCompra>();
            OrdenDeCompra vCurrentRecord = new Galac.Adm.Dal.GestionComprasOrdenDeCompra();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.Serie = "";
            vCurrentRecord.Numero = "";
            vCurrentRecord.Fecha = LibDate.Today();
            vCurrentRecord.ConsecutivoProveedor = 0;
            vCurrentRecord.Moneda = "";
            vCurrentRecord.CodigoMoneda = "";
            vCurrentRecord.CambioABolivares = 0;
            vCurrentRecord.TotalRenglones = 0;
            vCurrentRecord.TotalCompra = (0);
            vCurrentRecord.TipoDeCompraAsEnum = eTipoCompra.Nacional;
            vCurrentRecord.Comentarios = "";
            vCurrentRecord.StatusOrdenDeCompraAsEnum = eStatusCompra.Vigente;
            vCurrentRecord.FechaDeAnulacion = LibDate.Today();
            vCurrentRecord.CondicionesDeEntrega = "";
            vCurrentRecord.CondicionesDePago = 0;
            vCurrentRecord.CondicionesDeImportacionAsEnum = eCondicionDeImportacion.NoAplica;
            vCurrentRecord.NombreOperador = "";
            vCurrentRecord.FechaUltimaModificacion = LibDate.Today();
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<OrdenDeCompra> ParseToListEntity(XElement valXmlEntity) {
            List<OrdenDeCompra> vResult = new List<OrdenDeCompra>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                OrdenDeCompra vRecord = new OrdenDeCompra();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Serie"), null))) {
                    vRecord.Serie = vItem.Element("Serie").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Numero"), null))) {
                    vRecord.Numero = vItem.Element("Numero").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Fecha"), null))) {
                    vRecord.Fecha = LibConvert.ToDate(vItem.Element("Fecha"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoProveedor"), null))) {
                    vRecord.ConsecutivoProveedor = LibConvert.ToInt(vItem.Element("ConsecutivoProveedor"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Moneda"), null))) {
                    vRecord.Moneda = vItem.Element("Moneda").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoMoneda"), null))) {
                    vRecord.CodigoMoneda = vItem.Element("CodigoMoneda").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CambioABolivares"), null))) {
                    vRecord.CambioABolivares = LibConvert.ToDec(vItem.Element("CambioABolivares"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TotalRenglones"), null))) {
                    vRecord.TotalRenglones = LibConvert.ToDec(vItem.Element("TotalRenglones"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TotalCompra"), null))) {
                    vRecord.TotalCompra = LibConvert.ToDec(vItem.Element("TotalCompra"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDeCompra"), null))) {
                    vRecord.TipoDeCompra = vItem.Element("TipoDeCompra").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Comentarios"), null))) {
                    vRecord.Comentarios = vItem.Element("Comentarios").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("StatusOrdenDeCompra"), null))) {
                    vRecord.StatusOrdenDeCompra = vItem.Element("StatusOrdenDeCompra").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaDeAnulacion"), null))) {
                    vRecord.FechaDeAnulacion = LibConvert.ToDate(vItem.Element("FechaDeAnulacion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CondicionesDeEntrega"), null))) {
                    vRecord.CondicionesDeEntrega = vItem.Element("CondicionesDeEntrega").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CondicionesDePago"), null))) {
                    vRecord.CondicionesDePago = LibConvert.ToInt(vItem.Element("CondicionesDePago"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CondicionesDeImportacion"), null))) {
                    vRecord.CondicionesDeImportacion = vItem.Element("CondicionesDeImportacion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NombreOperador"), null))) {
                    vRecord.NombreOperador = vItem.Element("NombreOperador").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaUltimaModificacion"), null))) {
                    vRecord.FechaUltimaModificacion = LibConvert.ToDate(vItem.Element("FechaUltimaModificacion"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo
        internal OrdenDeCompra GetDataParaCompra(int valConsecutivoCompania, int valConsecutivoOrdenDeCompra) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valConsecutivoOrdenDeCompra);
            RegisterClient();
            IList<OrdenDeCompra> vResult = _Db.GetData(eProcessMessageType.SpName, "OrdenDeCompraGET", vParams.Get(), true);
            FillWithForeignInfoOrdenDeCompraDetalleArticuloInventario(ref vResult);
            return vResult.FirstOrDefault();

        }
        internal bool VerificaExistenciaEnOrdenDeCompra(int valConsecutivoCompania, int valConsecutivoOrdenDeCompra) {
            bool vResult = true;
            RegisterClient();
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine(" SELECT SUM(Cantidad - CantidadRecibida) as Cantidad FROM Adm.Gv_OrdenDeCompraDetalleArticuloInventario_B1 WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoOrdenDeCompra = @ConsecutivoOrdenDeCompra");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoOrdenDeCompra", valConsecutivoOrdenDeCompra);
            XElement vData = _Db.QueryInfo(eProcessMessageType.Query, vSQL.ToString(), vParams.Get());
            if (vData != null) {
                vResult = LibConvert.ToDec(LibXml.GetPropertyString(vData, "Cantidad")) > 0;
            }
            return vResult;
        }
        internal void ActualizarOrdenDeCompraDesdeCompra(int valConsecutivoCompania, int valConsecutivoOrdenDeCompra, XElement valData) {
            var vListArticulo = (from vRecord in valData.Descendants("GpResult")
                                 select new {
                                     CodigoArticulo = vRecord.Element("CodigoArticulo").Value,
                                     Cantidad = LibConvert.ToDec(vRecord.Element("Cantidad"))
                                 });
            clsOrdenDeCompraDetalleArticuloInventarioNav vOrdenDeCompraDetalleArticuloInventarioNav = new clsOrdenDeCompraDetalleArticuloInventarioNav();
            foreach (var item in vListArticulo) {
                vOrdenDeCompraDetalleArticuloInventarioNav.ActualizarCantidadRecibida(valConsecutivoCompania, valConsecutivoOrdenDeCompra, item.CodigoArticulo, item.Cantidad);
            }
        }
        internal bool ValidarDatosParaImportarOrdenCompra(eAccionSR valAction, XElement valValores, out StringBuilder refErrorMessage) {
            bool vResult = true;
            IMonedaPdn insMoneda = new clsMonedaNav();
            decimal vCambioABolivares = 0;
            IOrdenDeCompraPdn insOrdenCompra = new clsOrdenDeCompraNav();
            StringBuilder vErrMsg = new StringBuilder();
            string vMensaje = string.Empty;
            if (valValores != null && valValores.HasElements && !valValores.IsEmpty) {
                if (LibString.IsNullOrEmpty(LibXml.GetElementValueOrEmpty(valValores, "Numero"))) {
                    BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "Numero") + " - Número Orden de Compra vacía");
                    vResult = false;
                } else {
                    if (insOrdenCompra.ValidaNumeroOC(LibXml.GetElementValueOrEmpty(valValores, "Numero"), LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"))) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "Numero") + " - Número Orden de Compra repetida");
                        vResult = false;
                    }
                    if  (LibString.IsNullOrEmpty(LibXml.GetElementValueOrEmpty(valValores, "Fecha"))) {
                        vResult = false;
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "Fecha") + " - Fecha vacía ");
                    } else if (!LibDate.IsDate(LibXml.GetElementValueOrEmpty(valValores, "Fecha")) || (!LibDate.IsValidForDB(LibConvert.ToDate(LibXml.GetElementValueOrEmpty(valValores, "Fecha"))))) {
                        vResult = false;
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "Fecha") + " - Fecha no válida");
                    }
                    if (LibString.IsNullOrEmpty(LibXml.GetElementValueOrEmpty(valValores, "CodigoProveedor"))) {
                        vResult = false;
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "CodigoProveedor") + " - El Código del proveedor vacío ");
                    } else if (!insOrdenCompra.ValidaProveedor(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), LibText.UCase(LibXml.GetElementValueOrEmpty(valValores, "CodigoProveedor")))) {
                        vResult = false;
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "CodigoProveedor") + " - El Código del proveedor no es válido o no existe");
                    }
                    if (LibString.IsNullOrEmpty(LibXml.GetElementValueOrEmpty(valValores, "TipoDeCompra"))) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "TipoDeCompra") + " - Tipo de Orden de Compra vacía");
                        vResult = false;
                    } else if (!LibString.S1IsEqualToS2(LibConvert.ToStr(LibXml.GetElementValueOrEmpty(valValores, "TipoDeCompra")), "Nac") && !LibString.S1IsEqualToS2(LibConvert.ToStr(LibXml.GetElementValueOrEmpty(valValores, "TipoDeCompra")), "Imp")) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "TipoDeCompra") + " - Tipo de Orden de Compra no valida");
                        vResult = false;
                    }
                    if (LibString.IsNullOrEmpty(LibXml.GetElementValueOrEmpty(valValores, "CodigoMoneda"))) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "TipoDeCompra") + " - Código de Moneda vacío");
                        vResult = false;
                    } else if (LibString.IsNullOrEmpty(insMoneda.GetNombreMoneda(LibXml.GetElementValueOrEmpty(valValores, "CodigoMoneda")))) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "CodigoMoneda") + " - Código de Moneda inválido");
                        vResult = false;
                    }
                    string vStringCambioABolivares = LibText.EraseInvalidCharsFromText(LibXml.GetElementValueOrEmpty(valValores, "CambioABolivares"), new char[] { ',', '.' });
                    if (LibString.IsNullOrEmpty(LibXml.GetElementValueOrEmpty(valValores, "CambioABolivares"))) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "CambioABolivares") + " - Cambio a Bolívares vacío");
                        vResult = false;
                    } else if ((LibXml.GetElementValueOrEmpty(valValores, "CodigoMoneda") == "VED") && !LibConvert.IsNumeric(vStringCambioABolivares)) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "CambioABolivares") + " - Cambio a Bolívares inválido, el valor no es numérico");
                        vResult = false;
                    }else if ((LibXml.GetElementValueOrEmpty(valValores, "CodigoMoneda") == "VED") && LibImportData.ToDec(LibString.Replace(LibText.SubString(LibXml.GetElementValueOrEmpty(valValores, "CambioABolivares"), 0, 4), ",", ".")) < 0 && LibImportData.ToDec(LibString.Replace(LibText.SubString(LibXml.GetElementValueOrEmpty(valValores, "CambioABolivares"), 0, 4), ",", ".")) != 1) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "CambioABolivares") + " - Cambio a Bolívares inválido menor a 0");
                        vResult = false;
                    } else {
                        vCambioABolivares = LibImportData.ToDec(LibString.Replace(LibXml.GetElementValueOrEmpty(valValores, "CambioABolivares"), ",", "."));
                    }
                    if (LibString.IsNullOrEmpty(LibXml.GetElementValueOrEmpty(valValores, "CondicionesDePago"))) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "CondicionesDePago") + " - Condiciones de Pago Vacía");
                        vResult = false;
                    } else if (!insOrdenCompra.ValidaCondicionesPago(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), LibConvert.ToInt(LibXml.GetElementValueOrEmpty(valValores, "CondicionesDePago")))) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "CondicionesDePago") + " - Las Condiciones de Pago inválidas o no existen");
                        vResult = false;
                    }
                    if (LibString.IsNullOrEmpty(LibXml.GetElementValueOrEmpty(valValores, "CondicionesDeImportacion"))) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "CondicionesDeImportacion") + " - Las Condiciones de Importación vacía");
                        vResult = false;
                    } else if (!insOrdenCompra.ValidaCondicionesdeImportacion(LibConvert.ToStr(LibXml.GetElementValueOrEmpty(valValores, "CondicionesDeImportacion")))) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "CondicionesDeImportacion") + " - Las Condiciones de Importación son inválidas o no existen");
                        vResult = false;
                    }
                    if (LibString.IsNullOrEmpty(LibXml.GetElementValueOrEmpty(valValores, "CodigoArticulo"))) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "CodigoArticulo") + " - El Código Artículo vacío");
                        vResult = false;
                    } else if (!insOrdenCompra.ValidaCodigoArticulo(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), LibConvert.ToStr(LibXml.GetElementValueOrEmpty(valValores, "CodigoArticulo")))) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "CodigoArticulo") + " - El Código de Artículo inválido o no existe");
                        vResult = false;
                    }
                    if (LibString.IsNullOrEmpty(LibXml.GetElementValueOrEmpty(valValores, "Cantidad"))) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "Cantidad") + " - Cantidad vacío");
                        vResult = false;
                    } else if (!LibConvert.IsNumeric(LibXml.GetElementValueOrEmpty(valValores, "Cantidad"))) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "Cantidad") + " - Cantidad inválida, el valor no es numérico ");
                        vResult = false;
                    } else if (LibImportData.ToDec(LibString.Replace(LibText.SubString(LibXml.GetElementValueOrEmpty(valValores, "Cantidad"), 0, 4), ",", ".")) < 0) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "Cantidad") + " - Cantidad inválida menor a 0");
                        vResult = false;
                    }
                    string vStringCostoUnitario = LibText.EraseInvalidCharsFromText(LibXml.GetElementValueOrEmpty(valValores, "CambioABolivares"), new char[] { ',', '.' });
                    if (LibString.IsNullOrEmpty(LibXml.GetElementValueOrEmpty(valValores, "CostoUnitario"))) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "CostoUnitario") + " - Costo Unitario vacío");
                        vResult = false;
                    } else if (!LibConvert.IsNumeric(LibXml.GetElementValueOrEmpty(valValores, "CostoUnitario"))) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "CostoUnitario") + " - Costo Unitario inválido, el valor no es numérico ");
                        vResult = false;
                    } else if (LibImportData.ToDec(LibString.Replace(LibText.SubString(LibXml.GetElementValueOrEmpty(valValores, "CostoUnitario"), 0, 4), ",", ".")) < 0) {
                        BuildValidationInfo(LibText.Bullet() + LibXml.GetElementValueOrEmpty(valValores, "CostoUnitario") + " - Costo Unitario inválida menor a 0");
                        vResult = false;
                    }
                }
                if (Information.Length > 0) {
                    vErrMsg.AppendLine(Information.ToString());
                }
            }
            refErrorMessage = vErrMsg;
            return vResult;
        }
        bool IOrdenDeCompraPdn.ValidaNumeroOC(string valNumero, int valConsecutivoCompania) {
            LibGpParams vParams = new LibGpParams();
            bool vResult = false;
            vParams.AddInString("Numero", valNumero, 20);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(" SELECT Numero FROM Adm.OrdenDeCompra ");
            SQL.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            SQL.AppendLine("AND Numero = @Numero ");
            XElement vResulset = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), string.Empty, -1);
            if (vResulset != null) {
                vResult = true;
            }
            return vResult;
        }
        bool IOrdenDeCompraPdn.ValidaProveedor(int valConsecutivoCompania, string valCodigoProveedor) {
            LibGpParams vParams = new LibGpParams();
            bool vResult = false;
            vParams.AddInString("CodigoProveedor", valCodigoProveedor, 10);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(" SELECT Consecutivo FROM Adm.Proveedor ");
            SQL.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            SQL.AppendLine("AND CodigoProveedor = @CodigoProveedor ");
            XElement vResulset = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), string.Empty, -1);
            if (vResulset != null) {
                vResult = true;
            }
            return vResult;
        }
        bool IOrdenDeCompraPdn.ValidaCondicionesPago(int valConsecutivoCompania, int valCondicionesDePago) {
            LibGpParams vParams = new LibGpParams();
            bool vResult = false;
            vParams.AddInInteger("Consecutivo", valCondicionesDePago);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(" SELECT Consecutivo FROM Saw.CondicionesDePago ");
            SQL.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            SQL.AppendLine("AND Consecutivo = @Consecutivo ");
            XElement vResulset = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), string.Empty, -1);
            if (vResulset != null) {
                vResult = true;
            }
            return vResult;
        }
        bool IOrdenDeCompraPdn.ValidaCodigoArticulo(int valConsecutivoCompania, string valCodigoArticulo) {
            LibGpParams vParams = new LibGpParams();
            bool vResult = false;
            vParams.AddInString("Codigo", valCodigoArticulo, 30);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(" SELECT Codigo FROM dbo.ArticuloInventario ");
            SQL.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            SQL.AppendLine("AND Codigo = @Codigo ");
            SQL.AppendLine("AND TipoDeArticulo <> 2 ");
            XElement vResulset = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), string.Empty, -1);
            if (vResulset != null) {
                vResult = true;
            }
            return vResult;
        }
        bool IOrdenDeCompraPdn.ValidaCondicionesdeImportacion(string valCondicionesImportacion) {
            LibGpParams vParams = new LibGpParams();
            OrdenDeCompra vOrdenCompra = new OrdenDeCompra();
            int vCantidadCondicionesImportacion = 12;
            int vvalorEnumerativo = 0;
            bool vResult = false;
            vvalorEnumerativo = LibConvert.ToInt(valCondicionesImportacion);
            if (vCantidadCondicionesImportacion > vvalorEnumerativo) {
                vResult = true;
            }
            return vResult;
        }
        int IOrdenDeCompraPdn.ValidaTipoDeCompra(string valTipoDeCompra) {
            int vResult = 0;
            if (LibString.S1IsEqualToS2(LibConvert.ToStr(valTipoDeCompra), "Imp")) {
                vResult = 1;
            } else {
                vResult = 0;
            }
            return vResult;
        }
        int IOrdenDeCompraPdn.InfoProveedor(int valConsecutivoCompania, string valCodigoProveedor) {
            LibGpParams vParams = new LibGpParams();
            int vConsecutivo = 0;
            vParams.AddInString("CodigoProveedor", valCodigoProveedor, 10);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(" SELECT Consecutivo FROM Adm.Proveedor ");
            SQL.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            SQL.AppendLine("AND CodigoProveedor = @CodigoProveedor ");
            XElement vResulset = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), string.Empty, -1);
            if (vResulset != null) {
                vConsecutivo = (from vRecord in vResulset.Descendants("GpResult")
                                select new {
                                    Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo"))
                                }).FirstOrDefault().Consecutivo;
            }
            return vConsecutivo;
        }
        decimal IOrdenDeCompraPdn.CalculaTotalRenglon(string ValCodigoMoneda, decimal valCambioABolivares, decimal valCantidad, decimal valCosto) {
            IMonedaPdn insMoneda = new clsMonedaNav();
            string vMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaLocal");
            decimal vTotalRenglon = 0;
            if (vMonedaLocal == ValCodigoMoneda) {
                ;
                vTotalRenglon = LibMath.RoundToNDecimals(valCantidad * valCosto, 2);
            } else {
                vTotalRenglon = LibMath.RoundToNDecimals((valCantidad * valCosto) * valCambioABolivares, 2);
            }
            return vTotalRenglon;
        }
        decimal IOrdenDeCompraPdn.CalculaTotalRenglonOC(XElement valRecord, string valNumero, string valMoneda, decimal valCambio) {
            OrdenDeCompra vOrdenCompra = new OrdenDeCompra();
            IOrdenDeCompraPdn OrdenDeCompraPdn = new clsOrdenDeCompraNav();
            decimal vResult = 0;
            decimal vTotalRenglones = 0;
            decimal vTotalRenglonesTemp = 0;
            int vCantidadRegistros = 0;
            int Regis = 0;
            StringBuilder vErrMssg = new StringBuilder();
            string vNumeroAnterior = valNumero;
            string vNumeroActual = valNumero;
            foreach (XElement vItem in valRecord.Nodes()) {
                string vNumeroNuevo = LibText.UCase(LibXml.GetElementValueOrEmpty(vItem, "Numero"));
                if (vNumeroActual == vNumeroNuevo) {
                    vCantidadRegistros++;
                    vNumeroAnterior = vNumeroNuevo;
                }
                while (Regis < vCantidadRegistros) {
                    vTotalRenglonesTemp = 0;
                    vTotalRenglonesTemp = OrdenDeCompraPdn.CalculaTotalRenglon(valMoneda, valCambio, LibConvert.ToDec(LibXml.GetElementValueOrEmpty(vItem, "Cantidad")), LibConvert.ToDec(LibXml.GetElementValueOrEmpty(vItem, "CostoUnitario")));
                    Regis++;
                }
                if (Regis == vCantidadRegistros && vNumeroAnterior == vNumeroNuevo && vNumeroActual == vNumeroNuevo) {
                    vTotalRenglones += vTotalRenglonesTemp;
                    Regis = 0;
                    vCantidadRegistros = 0;
                } else if (vNumeroActual == vNumeroAnterior) {
                    vNumeroAnterior = vNumeroNuevo;
                } else {
                    vNumeroAnterior = vNumeroNuevo;
                }
            }
            vResult = vTotalRenglones;
            return vResult;
        }
        string IOrdenDeCompraPdn.InfoArticulo(int valConsecutivoCompania, string valCodigoArticulo) {
            LibGpParams vParams = new LibGpParams();
            string vDescripcion = "";
            vParams.AddInString("Codigo", valCodigoArticulo, 30);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(" SELECT Descripcion FROM dbo.ArticuloInventario ");
            SQL.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            SQL.AppendLine("AND Codigo = @Codigo ");
            SQL.AppendLine("AND TipoDeArticulo <> 2 ");
            XElement vResulset = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), string.Empty, -1);
            if (vResulset != null) {
                vDescripcion = (from vRecord in vResulset.Descendants("GpResult")
                                select new {
                                    Descripcion = LibText.UCase(LibXml.GetElementValueOrEmpty(vRecord, "Descripcion"))
                                }).FirstOrDefault().Descripcion;
            }
            return vDescripcion;
        }
        int IOrdenDeCompraPdn.InfoNumeroOC(int valConsecutivoCompania, string valNumero) {
            LibGpParams vParams = new LibGpParams();
            int vResult = 0;
            vParams.AddInString("Numero", valNumero, 20);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(" SELECT Consecutivo FROM Adm.OrdenDeCompra ");
            SQL.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            SQL.AppendLine("AND Numero = @Numero ");

            XElement vResulset = LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), string.Empty, -1);
            if (vResulset != null) {
                vResult = (from vRecord in vResulset.Descendants("GpResult")
                           select new {
                               Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo"))
                           }).FirstOrDefault().Consecutivo;
            }
            return vResult;
        }
    } //End of class clsOrdenDeCompraNav

} //End of namespace Galac.Adm.Brl.GestionCompras