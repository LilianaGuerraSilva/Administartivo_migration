using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Transactions;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.DefGen;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Brl.GestionCompras {
    public partial class clsCompraNav : LibBaseNavMaster<IList<Compra>, IList<Compra>>, ILibPdn, ICompraPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCompraNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataMasterComponentWithSearch<IList<Compra>, IList<Compra>> GetDataInstance() {
            return new Galac.Adm.Dal.GestionCompras.clsCompraDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.GestionCompras.clsCompraDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.GestionCompras.clsCompraDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_CompraSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<Compra>, IList<Compra>> instanciaDal = new Galac.Adm.Dal.GestionCompras.clsCompraDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_CompraGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Compra Nacional":
                case "Importación":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Proveedor":
                    vPdnModule = new Galac.Adm.Brl.GestionCompras.clsProveedorNav();
                    vResult = vPdnModule.GetDataForList("Compra", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Almacén":
                    vPdnModule = new clsAlmacenNav();
                    vResult = vPdnModule.GetDataForList("Compra", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Articulo Inventario":
                    vPdnModule = new clsArticuloInventarioNav();
                    vResult = vPdnModule.GetDataForList("Compra", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "CxP":
                    vPdnModule = new clsCxPNav();
                    vResult = vPdnModule.GetDataForList("Compra", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Moneda":
                    vPdnModule = new Galac.Comun.Brl.TablasGen.clsMonedaNav();
                    vResult = vPdnModule.GetDataForList("Compra", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "OrdenDeCompra":
                    vPdnModule = new clsOrdenDeCompraNav();
                    vResult = vPdnModule.GetDataForList("Compra", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<Compra> refData) {
            // FillWithForeignInfoCompra(ref refData);
            new clsCompraDetalleArticuloInventarioNav().FillWithForeignInfo(ref refData);
            //new clsCompraDetalleGastoNav().FillWithForeignInfo(ref refData);
        }
        #region Compra

        private void FillWithForeignInfoCompra(ref IList<Compra> refData) {
            XElement vInfoConexionProveedor = FindInfoProveedor(refData);
            var vListProveedor = (from vRecord in vInfoConexionProveedor.Descendants("GpResult")
                                  select new {
                                      ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                      CodigoProveedor = vRecord.Element("CodigoProveedor").Value,
                                      Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo")),
                                      NombreProveedor = vRecord.Element("NombreProveedor").Value
                                  }).Distinct();
            XElement vInfoConexionAlmacen = FindInfoAlmacen(refData);
            var vListAlmacen = (from vRecord in vInfoConexionAlmacen.Descendants("GpResult")
                                select new {
                                    ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                    Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo")),
                                    Codigo = vRecord.Element("Codigo").Value,
                                    NombreAlmacen = vRecord.Element("NombreAlmacen").Value,
                                    TipoDeAlmacen = vRecord.Element("TipoDeAlmacen").Value
                                }).Distinct();

            foreach (Compra vItem in refData) {
                var vProveedor = vListProveedor
                    .Where(p => p.Consecutivo == vItem.ConsecutivoProveedor && p.ConsecutivoCompania == vItem.ConsecutivoCompania)
                    .Select(p => new {
                        CodigoProveedor = p.CodigoProveedor,
                        NombreProveedor = p.NombreProveedor
                    });
                var vAlmacen = vListAlmacen
                    .Where(p => p.Consecutivo == vItem.ConsecutivoAlmacen && p.ConsecutivoCompania == vItem.ConsecutivoCompania)
                    .Select(p => new {
                        CodigoAlmace = p.Codigo,
                        NombreAlmacen = p.NombreAlmacen
                    });
                vItem.CodigoProveedor = vProveedor.FirstOrDefault().CodigoProveedor;
                vItem.NombreProveedor = vProveedor.FirstOrDefault().NombreProveedor;
                vItem.CodigoAlmacen = vAlmacen.FirstOrDefault().CodigoAlmace;
                vItem.NombreAlmacen = vAlmacen.FirstOrDefault().NombreAlmacen;
            }
        }

        private XElement FindInfoProveedor(IList<Compra> valData) {
            XElement vXElement = new XElement("GpData");

            vXElement.Add(FilterCompraByDistinctProveedor(valData).Descendants("GpResult"));

            ILibPdn insProveedor = new Galac.Adm.Brl.GestionCompras.clsProveedorNav();
            XElement vXElementResult = insProveedor.GetFk("Compra", ParametersGetFKProveedorForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterCompraByDistinctProveedor(IList<Compra> valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.Distinct()
                select new XElement("GpResult",
                    new XElement("ConsecutivoProveedor", vEntity.ConsecutivoProveedor)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKProveedorForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            // vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement FindInfoAlmacen(IList<Compra> valData) {
            XElement vXElement = new XElement("GpData");

            vXElement.Add(FilterCompraByDistinctAlmacen(valData).Descendants("GpResult"));

            ILibPdn insAlmacen = new Galac.Saw.Brl.Inventario.clsAlmacenNav();
            XElement vXElementResult = insAlmacen.GetFk("Compra", ParametersGetFKAlmacenForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterCompraByDistinctAlmacen(IList<Compra> valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.Distinct()
                select new XElement("GpResult",
                    new XElement("ConsecutivoAlmacen", vEntity.ConsecutivoAlmacen)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKAlmacenForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            // vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }


        #endregion //Compra

        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool ICompraPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<Compra>, IList<Compra>> instanciaDal = new clsCompraDat();
            IList<Compra> vLista = new List<Compra>();
            Compra vCurrentRecord = new Galac.Adm.Dal.GestionComprasCompra();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.Serie = "";
            vCurrentRecord.Numero = "";
            vCurrentRecord.Fecha = LibDate.Today();
            vCurrentRecord.ConsecutivoProveedor = 0;
            vCurrentRecord.ConsecutivoAlmacen = 0;
            vCurrentRecord.Moneda = "";
            vCurrentRecord.CambioABolivares = 0;
            vCurrentRecord.GenerarCXPAsBool = false;
            vCurrentRecord.TotalRenglones = 0;
            vCurrentRecord.TotalCompra = 0;
            vCurrentRecord.Comentarios = "";
            vCurrentRecord.DistribuirGastosAsEnum = eMontoPorcentaje.PorMonto;
            vCurrentRecord.StatusCompraAsEnum = eStatusCompra.Vigente;
            vCurrentRecord.FechaDeAnulacion = LibDate.Today();
            vCurrentRecord.TipoDeCompraAsEnum = eTipoDeCompra.ComprasExentas;
            vCurrentRecord.NoFacturaNotaEntrega = "";
            vCurrentRecord.TipoDeCompraParaCxPAsEnum = eTipoDeCompra.ComprasExentas;
            vCurrentRecord.CodigoMoneda = "";
            vCurrentRecord.NombreOperador = "";
            vCurrentRecord.FechaUltimaModificacion = LibDate.Today();
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<Compra> ParseToListEntity(XElement valXmlEntity) {
            List<Compra> vResult = new List<Compra>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                Compra vRecord = new Compra();
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
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoAlmacen"), null))) {
                    vRecord.ConsecutivoAlmacen = LibConvert.ToInt(vItem.Element("ConsecutivoAlmacen"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Moneda"), null))) {
                    vRecord.Moneda = vItem.Element("Moneda").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CambioABolivares"), null))) {
                    vRecord.CambioABolivares = LibConvert.ToDec(vItem.Element("CambioABolivares"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("GenerarCXP"), null))) {
                    vRecord.GenerarCXP = vItem.Element("GenerarCXP").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TotalRenglones"), null))) {
                    vRecord.TotalRenglones = LibConvert.ToDec(vItem.Element("TotalRenglones"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("OtrosGastos"), null))) {
                    vRecord.OtrosGastos = LibConvert.ToDec(vItem.Element("OtrosGastos"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TotalCompra"), null))) {
                    vRecord.TotalCompra = LibConvert.ToDec(vItem.Element("TotalCompra"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Comentarios"), null))) {
                    vRecord.Comentarios = vItem.Element("Comentarios").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("StatusCompra"), null))) {
                    vRecord.StatusCompra = vItem.Element("StatusCompra").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDeOrdenDeCompra"), null))) {
                    vRecord.NumeroDeOrdenDeCompra = vItem.Element("NumeroDeOrdenDeCompra").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaDeAnulacion"), null))) {
                    vRecord.FechaDeAnulacion = LibConvert.ToDate(vItem.Element("FechaDeAnulacion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDeCompra"), null))) {
                    vRecord.TipoDeCompra = vItem.Element("TipoDeCompra").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NoFacturaNotaEntrega"), null))) {
                    vRecord.NoFacturaNotaEntrega = vItem.Element("NoFacturaNotaEntrega").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDeCompraParaCxP"), null))) {
                    vRecord.TipoDeCompraParaCxP = vItem.Element("TipoDeCompraParaCxP").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoMoneda"), null))) {
                    vRecord.CodigoMoneda = vItem.Element("CodigoMoneda").Value;
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
        bool ICompraPdn.ExisteTasaDeCambioParaElDia(string valMoneda, DateTime valFecha, out decimal outTasa) {
            StringBuilder vSQL = new StringBuilder();
            bool vResult = false;
            outTasa = 0;
            vSQL.AppendLine("SELECT CodigoMoneda, CambioAbolivares FROM dbo.Cambio WHERE CodigoMoneda = @CodigoMoneda AND FechaDeVigencia = @Fecha ");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInDateTime("Fecha", valFecha);
            vParams.AddInString("CodigoMoneda", valMoneda, 4);
            XElement vData = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            if (vData != null) {
                var vEntity = (from vRecord in vData.Descendants("GpResult")
                               select new {
                                   CodigoMoneda = vRecord.Element("CodigoMoneda").Value,
                                   CambioAbolivares = LibConvert.ToDec(vRecord.Element("CambioAbolivares"), 4)
                               }).Distinct();
                foreach (var item in vEntity) {
                    outTasa = item.CambioAbolivares;
                    vResult = true;
                }
            }

            return vResult;
        }

        bool ICompraPdn.InsertaTasaDeCambioParaElDia(string valMoneda, DateTime valFechaVigencia, string valNombre, decimal valCambioAbolivares) {
            StringBuilder vSQL = new StringBuilder();
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            vParams.AddInDateTime("FechaDeVigencia", valFechaVigencia);
            vParams.AddInString("CodigoMoneda", valMoneda, 4);
            vParams.AddInString("Nombre", valNombre, 80);
            vParams.AddInDecimal("CambioAbolivares", valCambioAbolivares, 4);
            vParams.AddInString("NombreOperador", ((CustomIdentity)System.Threading.Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            vSQL.AppendLine("INSERT INTO dbo.Cambio (CodigoMoneda,FechaDeVigencia,Nombre,CambioAbolivares,NombreOperador, FechaUltimaModificacion) VALUES (@CodigoMoneda,@FechaDeVigencia,@Nombre,@CambioAbolivares,@NombreOperador,@FechaUltimaModificacion)");
            vResult = LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0) > 0;
            return vResult;
        }

        bool ICompraPdn.CambiarStatusCompra(Compra valCompra, eAccionSR valAction) {
            RegisterClient();
            IList<Compra> vList = new List<Compra>();
            vList.Add(valCompra);
            LibResponse vResult = _Db.SpecializedUpdate(vList, false, LibEnumHelper.GetDescription(valAction));
            if (vResult.Success) {
                vResult = ActualizaCantidadArticuloInventario(valCompra, valAction == eAccionSR.Abrir);

            }
            return vResult.Success;
        }

        private LibResponse ActualizaCantidadArticuloInventario(Compra valCompra, bool valAumentaCantidad) {
            LibResponse vResult = new LibResponse();
            vResult.Success = true;
            IArticuloInventarioPdn vArticuloPdn = new clsArticuloInventarioNav();
            decimal vCantidad = 0;
            List<ArticuloInventarioExistencia> vList = new List<ArticuloInventarioExistencia>();
            foreach (CompraDetalleArticuloInventario item in valCompra.DetailCompraDetalleArticuloInventario) {
                vCantidad = item.Cantidad;
                if (!valAumentaCantidad) {
                    vCantidad = vCantidad * -1;
                }
                vList.Add(new ArticuloInventarioExistencia() {
                    ConsecutivoCompania = item.ConsecutivoCompania,
                    CodigoAlmacen = valCompra.CodigoAlmacen,
                    CodigoArticulo = item.CodigoArticulo ,
                    Cantidad = vCantidad,
                    Ubicacion = "",
                    ConsecutivoAlmacen = valCompra.ConsecutivoAlmacen,
                    DetalleArticuloInventarioExistenciaSerial = GeneraDetalleArticuloInventarioSerial(item.CodigoArticulo, item.CodigoArticuloInv, valCompra.CodigoAlmacen, valCompra.ConsecutivoAlmacen, valCompra.DetailCompraDetalleSerialRollo, valAumentaCantidad)

                });
            }

            vResult.Success = vArticuloPdn.ActualizarExistencia(valCompra.ConsecutivoCompania, vList) && vResult.Success;
            return vResult;
        }

      
        private bool ValidaExistenciaSerialRollo(Compra refRecord) {
             XElement vData = new XElement("GpData");
             foreach (CompraDetalleSerialRollo item in refRecord.DetailCompraDetalleSerialRollo) {
                 vData.Add(new XElement("GpResult",
                    new XElement("CodigoArticulo", item.CodigoArticulo),
                    new XElement("Serial", item.Serial ),
                    new XElement("Rollo", item.Rollo)));
             }
             Galac.Saw.Ccl.Inventario.IArticuloInventarioPdn vArticuloPdn = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
             return vArticuloPdn.ValidaExistenciaDeArticuloSerial(refRecord.ConsecutivoCompania, vData);

        }


        private List<ArticuloInventarioExistenciaSerial> GeneraDetalleArticuloInventarioSerial(string valCodigoArticulo, string valCodigoArticuloInv, string valCodigoAlmacen, int valConsecutivoAlmacen, System.Collections.ObjectModel.ObservableCollection<CompraDetalleSerialRollo> valDetailCompraDetalleSerialRollo, bool valAumentaCantidad) {
            List<ArticuloInventarioExistenciaSerial> vResult = new List<ArticuloInventarioExistenciaSerial>();
            var vList = valDetailCompraDetalleSerialRollo.Where(p => p.CodigoArticulo == valCodigoArticulo || p.CodigoArticulo == valCodigoArticuloInv).Select(p => p);
            decimal vCantidad = 0;
            int vIndex = 1;
            foreach (var item in vList) {
                vCantidad = item.Cantidad;
                if (!valAumentaCantidad) {
                    vCantidad = vCantidad * -1;
                }
                ArticuloInventarioExistenciaSerial vArticuloInventarioExistenciaSerial = new ArticuloInventarioExistenciaSerial() {
                    ConsecutivoCompania = item.ConsecutivoCompania,
                    CodigoAlmacen = valCodigoAlmacen,
                    CodigoArticulo = item.CodigoArticulo,
                    ConsecutivoRenglon = vIndex,
                    CodigoSerial = item.Serial,
                    CodigoRollo = (!LibString.IsNullOrEmpty(item.Rollo) ? item.Rollo : "0")  ,
                    Cantidad = vCantidad,
                    Ubicacion = "",
                    ConsecutivoAlmacen = valConsecutivoAlmacen

                };
                vIndex++;
                vResult.Add(vArticuloInventarioExistenciaSerial);

            }
            return vResult;
        }

        protected override LibResponse InsertRecord(IList<Compra> refRecord, bool valUseDetail) {
            LibResponse vResult = new LibResponse();
            if (ValidaExistenciaSerialRollo(refRecord[0])) {
                if (VerificaExistenciaDeCxPConDatosIguales(refRecord [0])) {
                    throw new GalacAlertException("Ya existe una CxP Generada con este Número para este Proveedor. En caso de que desee continuar Desmarque la opción Generar CxP para completarl a operación.");
                }
                using (TransactionScope vScope = LibBusiness.CreateScope()) {
                    vResult = base.InsertRecord(refRecord, valUseDetail);
                    if (vResult.Success) {
                        vResult = ActualizaCantidadArticuloInventario(refRecord[0], true);
                    }
                    if (refRecord[0].ConsecutivoOrdenDeCompra != 0) {
                        ActualizarOrdenDeCompra(refRecord, eAccionSR.Insertar );
                    }

                    if (vResult.Success) {
                        vScope.Complete();
                    }
                }
            }
            return vResult;
        }
        
        protected override LibResponse UpdateRecord(IList<Compra> refRecord, bool valUseDetail, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            LibGpParams vParams = new LibGpParams();
            using (TransactionScope vScope = LibBusiness.CreateScope()) {
                vParams.AddInInteger("ConsecutivoCompania", refRecord[0].ConsecutivoCompania);
                vParams.AddInInteger("Consecutivo", refRecord[0].Consecutivo);
                IList<Compra> vCompras = _Db.GetData(eProcessMessageType.SpName, "CompraGET", vParams.Get(), true);
                if (vCompras != null && vCompras.Count > 0) {
                    vResult = ActualizaCantidadArticuloInventario(vCompras[0], false);
                } else {
                    throw new LibGalac.Aos.Catching.GalacConcurrencyException();
                }
                if (vResult.Success) {
                    vResult = base.UpdateRecord(refRecord, valUseDetail, valAction);
                    if (vResult.Success && refRecord[0].StatusCompraAsEnum != eStatusCompra.Anulada) {
                        vResult = ActualizaCantidadArticuloInventario(refRecord[0], true);
                    }
                }
                if (vResult.Success) {
                    vScope.Complete();
                }
            }
            return vResult;
        }

        protected override LibResponse DeleteRecord(IList<Compra> refRecord) {
            LibResponse vResult = new LibResponse();
            using (TransactionScope vScope = LibBusiness.CreateScope()) {
                if (refRecord[0].StatusCompraAsEnum != eStatusCompra.Anulada) {
                    vResult = ActualizaCantidadArticuloInventario(refRecord[0], false);
                }
                vResult = base.DeleteRecord(refRecord);
				if (vResult.Success) {
                	string vNumero = refRecord[0].Numero;
                	if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                    	vNumero = refRecord[0].Serie + "-" + refRecord[0].Numero;
                	}
                	new clsCxPNav().EliminarCxPDesdeCompra(refRecord[0].ConsecutivoCompania, vNumero, refRecord[0].CodigoProveedor);              
                    if (refRecord[0].ConsecutivoOrdenDeCompra != 0) {
                        ActualizarOrdenDeCompra(refRecord, eAccionSR.Eliminar);
                    }
                }
                if (vResult.Success) {
                    vScope.Complete();
                }
            }
            return vResult;
        }

        decimal ICompraPdn.TasaDeDolarVigente(string valMoneda) {
            StringBuilder vSQL = new StringBuilder();
            decimal vResult = 0;
            vSQL.AppendLine("SELECT CodigoMoneda, CambioAbolivares FROM dbo.Cambio WHERE CodigoMoneda = @CodigoMoneda ORDER BY FechaDeVigencia DESC ");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("CodigoMoneda", valMoneda, 4);
            XElement vData = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            if (vData != null) {
                var vEntity = (from vRecord in vData.Descendants("GpResult")
                               select new {
                                   CodigoMoneda = vRecord.Element("CodigoMoneda").Value,
                                   CambioAbolivares = LibConvert.ToDec(vRecord.Element("CambioAbolivares"), 4)
                               }).Distinct();
                foreach (var item in vEntity) {
                    vResult = item.CambioAbolivares;
                    break;
                }
            }
            return vResult;
        }

        bool ICompraPdn.GenerarCxP(Compra valRecord, string valNumeroControl, eAccionSR valAction, string valNumeroDeCompraOriginal, string valCodigoDeProveedorOriginal) {
            if (valAction == eAccionSR.Modificar) {
                string vNumero = valNumeroDeCompraOriginal;
                if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                    vNumero = valRecord.Serie + "-" + valNumeroDeCompraOriginal;
                }
                new clsCxPNav().EliminarCxPDesdeCompra(valRecord.ConsecutivoCompania, vNumero, valCodigoDeProveedorOriginal);
            }
            decimal MontoExento = valRecord.DetailCompraDetalleArticuloInventario.Where(p=>p.TipoDeAlicuota == 0).Sum(p => p.CostoUnitario * p.Cantidad);
            decimal MontoIVAGeneral = valRecord.DetailCompraDetalleArticuloInventario.Where(p => p.TipoDeAlicuota == 1).Sum(p => p.CostoUnitario * p.Cantidad);
            decimal MontoIVA2 = valRecord.DetailCompraDetalleArticuloInventario.Where(p => p.TipoDeAlicuota == 2).Sum(p => p.CostoUnitario * p.Cantidad);
            decimal MontoIVA3 = valRecord.DetailCompraDetalleArticuloInventario.Where(p => p.TipoDeAlicuota == 3).Sum(p => p.CostoUnitario * p.Cantidad);
            decimal MontoServicios = valRecord.DetailCompraDetalleArticuloInventario.Where(p => p.TipoDeArticulo == 1).Sum(p => p.CostoUnitario * p.Cantidad);

            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania",valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo",valRecord.ConsecutivoProveedor);
            IProveedorPdn proveedorNav = new clsProveedorNav();
            Proveedor proveedor = proveedorNav.GetProveedor(valRecord.ConsecutivoCompania,valRecord.CodigoProveedor);
            return new clsCxPNav().GenerarDesdeCompra(
                valRecord.ConsecutivoCompania
                , valRecord.Numero
                , valNumeroControl
                , valRecord.Fecha
                , valRecord.Moneda
                , valRecord.CambioABolivares
                , valRecord.CodigoMoneda
                , valRecord.Serie
                , valRecord.TipoDeCompraAsEnum
                , MontoExento
                , MontoIVAGeneral
                , MontoIVA2
                , MontoIVA3
                , proveedor
                ,MontoServicios
                , valRecord.SimboloMoneda);
        }

        XElement ICompraPdn.BuscarSerial(int valConsecutivoCompania, string valCodigoArticulo, string valCodigoGrupo) {
            LibGpParams vPrams = new LibGpParams();
            vPrams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);            
            vPrams.AddInString("CodigoGrupo", (!LibString.IsNullOrEmpty(valCodigoGrupo)) ? valCodigoGrupo : "0" , 10);
            vPrams.AddInString("CodigoArticulo", valCodigoArticulo, 36);
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("SELECT Color.CodigoColor, Color.DescripcionColor,");
            vSQL.AppendLine("  Talla.CodigoTalla,Talla.DescripcionTalla,");
            vSQL.AppendLine("ExistenciaPorGrupo.ConsecutivoCompania,ExistenciaPorGrupo.CodigoArticulo,");
            vSQL.AppendLine("ExistenciaPorGrupo.CodigoGrupo,ExistenciaPorGrupo.Existencia,ExistenciaPorGrupo.Serial,ExistenciaPorGrupo.Rollo");
            vSQL.AppendLine(" FROM (ExistenciaPorGrupo LEFT JOIN Talla");
            vSQL.AppendLine(" ON (ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla)");
            vSQL.AppendLine(" AND (ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania))");
            vSQL.AppendLine(" LEFT JOIN Color ON (ExistenciaPorGrupo.CodigoColor = Color.CodigoColor)");
            vSQL.AppendLine(" AND (ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania)");
            vSQL.AppendLine(" WHERE ");
            vSQL.AppendLine(" ExistenciaPorGrupo.CodigoGrupo = @CodigoGrupo");
            vSQL.AppendLine(" AND (ExistenciaPorGrupo.CodigoArticulo+ExistenciaPorGrupo.CodigoColor+ExistenciaPorGrupo.CodigoTalla) = @CodigoArticulo");
            vSQL.AppendLine(" AND ExistenciaPorGrupo.ConsecutivoCompania = @ConsecutivoCompania");
            XElement vData = LibBusiness.ExecuteSelect(vSQL.ToString(), vPrams.Get(), "", 0);
            return vData;
        }

        void ICompraPdn.ActualizaElCostoUnitario(Compra valRecord, bool valEsMonedaLocal) {
          
         
            decimal vCostoUnitarioMasGasto = 0;
            decimal vCostoMonedaExtranjera = 0;
            decimal vCostoMonedaLocal = 0;
            XElement vData = new XElement("GpData");
            foreach (CompraDetalleArticuloInventario item in valRecord.DetailCompraDetalleArticuloInventario) {
                vCostoUnitarioMasGasto = item.CostoUnitario;
                if (valRecord.CodigoMoneda == LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreMonedaLocal")) {
                    vCostoMonedaLocal = item.CostoUnitario;
                } else {
                    vCostoMonedaLocal = LibMath.RoundToNDecimals(item.CostoUnitario * valRecord.CambioABolivares, 2);
                }
                if (valRecord.CambioABolivares != 0) {
                    if (valRecord.CodigoMoneda == LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreMonedaLocal")) {
                        vCostoMonedaExtranjera = LibMath.RoundToNDecimals(item.CostoUnitario / valRecord.CambioABolivares, 2);
                    } else {
                        vCostoMonedaExtranjera = item.CostoUnitario;
                    }
                }
                vData.Add(new XElement("GpResult",
                    new XElement("CodigoArticulo", item.CodigoArticulo),
                    new XElement("CostoMonedaLocal", vCostoMonedaLocal),
                    new XElement("CostoMonedaExtranjera", vCostoMonedaExtranjera)));
            }
            Galac.Saw.Ccl.Inventario.IArticuloInventarioPdn vArticuloPdn = new clsArticuloInventarioNav();
            vArticuloPdn.ActualizarCostoUnitario(valRecord.ConsecutivoCompania, vData, valEsMonedaLocal) ;
           
        }

        bool ICompraPdn.SePuedeEjecutarElAjusteDePrecios() {
            return LibGalac.Aos.Dal.LibDBVersion.SqlServerVersionIs2005OrHigher()
                 && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "CalculoAutomaticoDeCosto")
                 && (Galac.Saw.Ccl.Inventario.eTipoDeMetodoDeCosteo) LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "MetodoDeCosteo") == Galac.Saw.Ccl.Inventario.eTipoDeMetodoDeCosteo.UltimoCosto ;
        }


        bool ICompraPdn.AjustaPreciosxCostos(bool valUsaFormulaAlterna, int valConsecutivoCompania, eRedondearPrecio valRedondearPrecio, ePrecioAjustar valPrecioAjustar, bool valEstablecerMargen, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4, decimal valMargen1, decimal valMargen2, decimal valMargen3, decimal valMargen4, DateTime valFechaOperacion, string valNumeroOperacion, bool valMonedaLocal) {
            bool vResult = false;
            IArticuloInventarioPdn vPdn = new clsArticuloInventarioNav();
            vResult = vPdn.AjustaPreciosxCostos(valUsaFormulaAlterna, valConsecutivoCompania, "", "", "", valRedondearPrecio, valPrecioAjustar, valEstablecerMargen, "", "", valPrecio1, valPrecio2, valPrecio3, valPrecio4, valMargen1, valMargen2, valMargen3, valMargen4, true, valFechaOperacion, valNumeroOperacion, "compra", valMonedaLocal);
            vPdn.ProcesaCostoPromedio(valConsecutivoCompania, true, valFechaOperacion, "", valNumeroOperacion, "compra");
            return vResult;
        }

        void ICompraPdn.ProcesaCostoPromedio(int valConsecutivoCompania, bool valVieneDeOperaciones, DateTime valFechaOperacion, string valCodigo, string valDocumento, string valOperacion) {
            
        }


        internal bool ExistenRegistroConOrdenDeCompra(int valConsecutivoCompania, int valConsecutivoOrdenDeCompra) {
            bool vResult = false;
            StringBuilder vSQL = new StringBuilder();
            vSQL.Append("select COUNT(consecutivocompania) AS Cantidad FROM Adm.Compra WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoOrdenDeCompra = @ConsecutivoOrdenDeCompra AND StatusCompra = @StatusCompra");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoOrdenDeCompra", valConsecutivoOrdenDeCompra);
            vParams.AddInEnum("StatusCompra", (int)eStatusCompra.Vigente);
            RegisterClient();
            XElement vData = _Db.QueryInfo(eProcessMessageType.Query, vSQL.ToString(), vParams.Get());
            if (vData != null) {
                vResult = LibConvert.ToInt(LibXml.GetPropertyString(vData, "Cantidad")) > 0;
            }
            return vResult;
        }




        void ICompraPdn.AsignarDetalleArticuloInventarioDesdeOrdenDeCompra(int valConsecutivoCompania, Compra valCompra, int valConsecutivoOrdenDeCompra) {
            OrdenDeCompra vOrdenDeCompra = new clsOrdenDeCompraNav().GetDataParaCompra(valConsecutivoCompania, valConsecutivoOrdenDeCompra);
            if (vOrdenDeCompra != null) {
                for (int vIndex = 0; vIndex< vOrdenDeCompra.DetailOrdenDeCompraDetalleArticuloInventario.Count; vIndex++) {  
                    if (vOrdenDeCompra.DetailOrdenDeCompraDetalleArticuloInventario[vIndex].Cantidad  - vOrdenDeCompra.DetailOrdenDeCompraDetalleArticuloInventario [vIndex].CantidadRecibida > 0) {
                        AsignaDatosDeAlicuotaYTipoArticulo(vOrdenDeCompra.DetailOrdenDeCompraDetalleArticuloInventario [vIndex]);
                        valCompra.DetailCompraDetalleArticuloInventario.Add(new CompraDetalleArticuloInventario() {
                            Cantidad = vOrdenDeCompra.DetailOrdenDeCompraDetalleArticuloInventario [vIndex].TipoArticuloInv == eTipoArticuloInv.Simple ? vOrdenDeCompra.DetailOrdenDeCompraDetalleArticuloInventario [vIndex].Cantidad - vOrdenDeCompra.DetailOrdenDeCompraDetalleArticuloInventario [vIndex].CantidadRecibida : 0,
                            CodigoArticulo = vOrdenDeCompra.DetailOrdenDeCompraDetalleArticuloInventario [vIndex].CodigoArticulo,
                            CantidadRecibida = vOrdenDeCompra.DetailOrdenDeCompraDetalleArticuloInventario [vIndex].Cantidad - vOrdenDeCompra.DetailOrdenDeCompraDetalleArticuloInventario [vIndex].CantidadRecibida,
                            DescripcionArticulo = vOrdenDeCompra.DetailOrdenDeCompraDetalleArticuloInventario [vIndex].DescripcionArticulo,
                            PrecioUnitario = vOrdenDeCompra.DetailOrdenDeCompraDetalleArticuloInventario [vIndex].CostoUnitario,
                            TipoArticuloInv = vOrdenDeCompra.DetailOrdenDeCompraDetalleArticuloInventario [vIndex].TipoArticuloInv,
                            CodigoGrupo = vOrdenDeCompra.DetailOrdenDeCompraDetalleArticuloInventario [vIndex].CodigoGrupo
                            , TipoDeAlicuota = vOrdenDeCompra.DetailOrdenDeCompraDetalleArticuloInventario [vIndex].TipoDeAlicuota
                            , TipoDeArticulo = vOrdenDeCompra.DetailOrdenDeCompraDetalleArticuloInventario [vIndex].TipoDeArticulo
                            , CostoUnitario = vOrdenDeCompra.DetailOrdenDeCompraDetalleArticuloInventario [vIndex].CostoUnitario
                        });
                    }
                }
            }
        }

        private void AsignaDatosDeAlicuotaYTipoArticulo(OrdenDeCompraDetalleArticuloInventario refItem) {
            XElement xData = LibBusiness.ExecuteSelect(SQLBuscaDatosInventario(),GetParamDatosInventario(refItem.ConsecutivoCompania,refItem.CodigoArticulo).Get(),"",0);
            if (xData != null) {
                refItem.TipoDeAlicuota = LibConvert.ToInt(LibXml.GetPropertyString(xData, "AlicuotaIva"));
                refItem.TipoDeArticulo  = LibConvert.ToInt(LibXml.GetPropertyString(xData,"TipoDeArticulo"));
            }
        }

        private LibGpParams GetParamDatosInventario(int valConsecutivoCompania, string valCodigoArticulo) {
            LibGpParams vParam = new LibGpParams();
            vParam.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            vParam.AddInString("Codigo",valCodigoArticulo, 30);
            return vParam;
        }
        private string SQLBuscaDatosInventario() {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("SELECT TipoDeArticulo, AlicuotaIva");
            vSQL.AppendLine("		FROM ArticuloInventario");
            vSQL.AppendLine("	WHERE Codigo = @Codigo");
            vSQL.AppendLine("		AND ConsecutivoCompania = @ConsecutivoCompania");
            return vSQL.ToString();
        }

        private static void ActualizarOrdenDeCompra(IList<Compra> refRecord, eAccionSR valAction) {
            XElement vData = new XElement("GpData");
            foreach (CompraDetalleArticuloInventario item in refRecord[0].DetailCompraDetalleArticuloInventario) {
                vData.Add(new XElement("GpResult",
                   new XElement("CodigoArticulo", item.CodigoArticulo),
                   new XElement("Cantidad", (valAction== eAccionSR.Eliminar)? item.Cantidad * -1: item.Cantidad )));
            }
            new clsOrdenDeCompraNav().ActualizarOrdenDeCompraDesdeCompra(refRecord[0].ConsecutivoCompania, refRecord[0].ConsecutivoOrdenDeCompra, vData);
        }


        bool ICompraPdn.VerificaExistenciaEnOrdenDeCompra(int valConsecutivoCompania, int valConsecutivoOrdenDeCompra) {
            return new clsOrdenDeCompraNav().VerificaExistenciaEnOrdenDeCompra(valConsecutivoCompania, valConsecutivoOrdenDeCompra);
        }

        protected override bool CanBeChoosenForAction(IList<Compra> refRecord,eAccionSR valAction) {
            bool vResult = true;
            if (valAction == eAccionSR.Modificar || valAction == eAccionSR.Anular || valAction == eAccionSR.Eliminar) {
                foreach (Compra vCompra in refRecord) {
                    vResult = !(new clsCxPNav().VerificaSiCxPEstaPorCancelar(vCompra.ConsecutivoCompania,vCompra.Numero,vCompra.CodigoProveedor));
                }
            }
            if (!vResult) {
                throw new GalacAlertException("No se puede " + valAction.ToString() + " una Compra, cuya CxP tenga Pagos asociados.");
            }
            return vResult;
        }

        private bool VerificaExistenciaDeCxPConDatosIguales(Compra valCompra) {
            bool vResult = valCompra.GenerarCXPAsBool;
            if (valCompra.GenerarCXPAsBool) {
                vResult = new clsCxPNav().VerificaSiExisteCxP(valCompra.ConsecutivoCompania,valCompra.Numero,valCompra.CodigoProveedor);
            }
            return vResult;
        }
    } //End of class clsCompraNav


}//End of namespace Galac.Adm.Brl.GestionCompras

