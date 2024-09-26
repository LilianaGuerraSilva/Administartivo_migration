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
using System.Transactions;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Brl.Inventario;
using LibGalac.Aos.Catching;
using System.Collections.ObjectModel;
using LibGalac.Aos.DefGen;
using Galac.Comun.Brl.TablasGen;
using Galac.Saw.Ccl.SttDef;
using System.Diagnostics.Eventing.Reader;

namespace Galac.Adm.Brl.GestionProduccion {
    public partial class clsOrdenDeProduccionNav : LibBaseNavMaster<IList<OrdenDeProduccion>, IList<OrdenDeProduccion>>, IOrdenDeProduccionPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsOrdenDeProduccionNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataMasterComponentWithSearch<IList<OrdenDeProduccion>, IList<OrdenDeProduccion>> GetDataInstance() {
            return new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Dal.GestionProduccion.clsOrdenDeProduccionDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_OrdenDeProduccionSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<OrdenDeProduccion>, IList<OrdenDeProduccion>> instanciaDal = new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_OrdenDeProduccionGetFk", valParameters);
        }

        private bool ContabGetDataForList(ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Dal.GestionProduccion.clsOrdenDeProduccionDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_OrdenDeProduccionDatContaSCH", valXmlParamsExpression);
        }

        private bool ArticuloGetDataForList(ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Dal.GestionProduccion.clsOrdenDeProduccionDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_OrdenDeProduccionDetalleArticuloSCH", valXmlParamsExpression);
        }

        private bool ArticuloGetDataForListRpt(ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Dal.GestionProduccion.clsOrdenDeProduccionDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_OrdenDeProdCerradaDetArtSchForRpt", valXmlParamsExpression);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Orden de Producción":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Almacén":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsAlmacenNav();
                    vResult = vPdnModule.GetDataForList("Orden de Producción", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Moneda":
                    vPdnModule = new clsMonedaNav();
                    vResult = vPdnModule.GetDataForList("Orden De Produccion", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Lista de Materiales":
                    vPdnModule = new Galac.Adm.Brl.GestionProduccion.clsListaDeMaterialesNav();
                    vResult = vPdnModule.GetDataForList("Orden de Producción", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Artículo Inventario":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
                    vResult = vPdnModule.GetDataForList("Orden de Producción", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Orden de Producción a Contabilizar":
                    vResult = ContabGetDataForList(ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Orden de Producción Artículo":
                    vResult = ArticuloGetDataForList(ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Orden de Producción Artículo para reportes":
                    vResult = ArticuloGetDataForListRpt(ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Lote de Inventario":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsLoteDeInventarioNav();
                    vResult = vPdnModule.GetDataForList("Orden de Producción", ref refXmlDocument, valXmlParamsExpression);
                break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<OrdenDeProduccion> refData) {
            FillWithForeignInfoOrdenDeProduccion(ref refData);
            FillWithForeignInfoOrdenDeProduccionDetalleArticulo(ref refData);
            FillWithForeignInfoOrdenDeProduccionDetalleMateriales(ref refData);
        }
        #region OrdenDeProduccion

        private void FillWithForeignInfoOrdenDeProduccion(ref IList<OrdenDeProduccion> refData) {
            XElement vInfoAlmacenInsumos = FindInfoAlmacenInsumos(refData);
            if (vInfoAlmacenInsumos != null && vInfoAlmacenInsumos.HasElements) {
                var vListAlmacenInsumos = (from vRecord in vInfoAlmacenInsumos.Descendants("GpResult")
                                           select new {
                                               ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                               Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo")),
                                               Codigo = vRecord.Element("Codigo").Value,
                                               NombreAlmacen = vRecord.Element("NombreAlmacen").Value,
                                           }).Distinct();
            }
            XElement vInfoAlmacenSalidas = FindInfoAlmacenSalidas(refData);
            if (vInfoAlmacenSalidas != null && vInfoAlmacenSalidas.HasElements) {
                var vListAlmacenSalidas = (from vRecord in vInfoAlmacenSalidas.Descendants("GpResult")
                                           select new {
                                               ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                               Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo")),
                                               Codigo = vRecord.Element("Codigo").Value,
                                               NombreAlmacen = vRecord.Element("NombreAlmacen").Value,
                                           }).Distinct();
            }
            XElement vInfoConexionMoneda = FindInfoMoneda(refData);
            if (vInfoConexionMoneda != null && vInfoConexionMoneda.HasElements) {
                var vListMoneda = (from vRecord in vInfoConexionMoneda.Descendants("GpResult")
                                   select new {
                                       Codigo = vRecord.Element("Codigo").Value,
                                       Nombre = vRecord.Element("Nombre").Value,
                                       Simbolo = vRecord.Element("Simbolo").Value,
                                       Activa = vRecord.Element("Activa").Value
                                   }).Distinct();
            }
            XElement vInfoConexionListaDeMateriales = FindInfoListaDeMateriales(refData);
            if (vInfoConexionListaDeMateriales != null && vInfoConexionListaDeMateriales.HasElements) {
                var vListListaDeMateriales = (from vRecord in vInfoConexionListaDeMateriales.Descendants("GpResult")
                                              select new {
                                                  ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
                                                  Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo")),
                                                  Codigo = vRecord.Element("Codigo").Value,
                                                  Nombre = vRecord.Element("Nombre").Value,
                                                  CodigoArticuloInventario = vRecord.Element("CodigoArticuloInventario").Value,
                                                  FechaCreacion = vRecord.Element("FechaCreacion").Value
                                              }).Distinct();
            }
            foreach (OrdenDeProduccion vItem in refData) {
                vItem.Moneda = vInfoConexionMoneda.Descendants("GpResult").Where(p => p.Element("Codigo").Value == vItem.CodigoMonedaCostoProduccion).Select(p => p.Element("Nombre").Value).FirstOrDefault();
                vItem.NombreAlmacenMateriales = vInfoAlmacenInsumos.Descendants("GpResult").Where(p => p.Element("Consecutivo").Value == vItem.ConsecutivoAlmacenMateriales.ToString()).Select(p => p.Element("NombreAlmacen").Value).FirstOrDefault();
                vItem.NombreAlmacenProductoTerminado = vInfoAlmacenSalidas.Descendants("GpResult").Where(p => p.Element("Consecutivo").Value == vItem.ConsecutivoAlmacenProductoTerminado.ToString()).Select(p => p.Element("NombreAlmacen").Value).FirstOrDefault();
                vItem.CodigoListaDeMateriales = vInfoConexionListaDeMateriales.Descendants("GpResult").Where(p => p.Element("Consecutivo").Value == vItem.ConsecutivoListaDeMateriales.ToString()).Select(p => p.Element("Codigo").Value).FirstOrDefault();
                vItem.NombreListaDeMateriales = vInfoConexionListaDeMateriales.Descendants("GpResult").Where(p => p.Element("Consecutivo").Value == vItem.ConsecutivoListaDeMateriales.ToString()).Select(p => p.Element("Nombre").Value).FirstOrDefault();
            }
        }

        #region Almacen
        private XElement FindInfoAlmacenInsumos(IList<OrdenDeProduccion> valData) {
            XElement vXElementResult = new XElement("GpData");
            foreach (OrdenDeProduccion vItem in valData) {
                XElement vXElement = BuscaDatosDeAlmacen(vItem.ConsecutivoCompania, vItem.ConsecutivoAlmacenMateriales);
                vXElementResult.Add(vXElement.Descendants("GpResult"));
            }
            return vXElementResult;
        }
        private XElement FindInfoAlmacenSalidas(IList<OrdenDeProduccion> valData) {
            XElement vXElementResult = new XElement("GpData");
            foreach (OrdenDeProduccion vItem in valData) {
                XElement vXElement = BuscaDatosDeAlmacen(vItem.ConsecutivoCompania, vItem.ConsecutivoAlmacenProductoTerminado);
                vXElementResult.Add(vXElement.Descendants("GpResult"));
            }
            return vXElementResult;
        }

        private XElement BuscaDatosDeAlmacen(int valConsecutivoCompania, int valConsecutivoAlmacen) {
            XElement vResult;
            LibGpParams vParms = new LibGpParams();
            vParms.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParms.AddInInteger("Consecutivo", valConsecutivoAlmacen);
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT * FROM Almacen ");
            vSql.AppendLine("WHERE ConsecutivoCompania= @ConsecutivoCompania");
            vSql.AppendLine(" AND Consecutivo= @Consecutivo");
            vResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParms.Get(), "", 0);
            return vResult;
        }
        #endregion Almacen

        #region Moneda
        private XElement FindInfoMoneda(IList<OrdenDeProduccion> valData) {
            XElement vXElementResult = new XElement("GpData");
            foreach (OrdenDeProduccion vItem in valData) {
                XElement vXElement = BuscaDatosDeMoneda(vItem.CodigoMonedaCostoProduccion);
                vXElementResult.Add(vXElement.Descendants("GpResult"));
            }
            return vXElementResult;
        }

        private XElement BuscaDatosDeMoneda(string valCodigoMoneda) {
            XElement vResult;
            LibGpParams vParms = new LibGpParams();
            vParms.AddInString("Codigo", valCodigoMoneda, 4);
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT * FROM Moneda ");
            vSql.AppendLine("WHERE Codigo= @Codigo");
            vResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParms.Get(), "", 0);
            return vResult;
        }

        #endregion Moneda

        #region Lista de Materiales
        private XElement FindInfoListaDeMateriales(IList<OrdenDeProduccion> valData) {
            XElement vXElementResult = new XElement("GpData");
            foreach (OrdenDeProduccion vItem in valData) {
                XElement vXElement = BuscaDatosDeListaDeMateriales(vItem.ConsecutivoCompania, vItem.ConsecutivoListaDeMateriales);
                vXElementResult.Add(vXElement.Descendants("GpResult"));
            }
            return vXElementResult;
        }

        private XElement BuscaDatosDeListaDeMateriales(int valConsecutivoCompania, int valConsecutivoLista) {
            XElement vResult;
            LibGpParams vParms = new LibGpParams();
            vParms.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParms.AddInInteger("Consecutivo", valConsecutivoLista);
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT * FROM Adm.ListaDeMateriales ");
            vSql.AppendLine("WHERE ConsecutivoCompania= @ConsecutivoCompania");
            vSql.AppendLine(" AND Consecutivo= @Consecutivo");
            vResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParms.Get(), "", 0);
            return vResult;
        }
        #endregion Lista de Materiales

        #endregion //OrdenDeProduccion
        #region OrdenDeProduccionDetalleArticulo

        private void FillWithForeignInfoOrdenDeProduccionDetalleArticulo(ref IList<OrdenDeProduccion> refData) {
            foreach (OrdenDeProduccion vItem in refData) {
                vItem.DetailOrdenDeProduccionDetalleArticulo = new clsOrdenDeProduccionDetalleArticuloNav().FillWithForeignInfo(vItem);
            }
        }
        #endregion //OrdenDeProduccionDetalleArticulo
        #region OrdenDeProduccionDetalleMateriales

        private void FillWithForeignInfoOrdenDeProduccionDetalleMateriales(ref IList<OrdenDeProduccion> refData) {
            foreach (OrdenDeProduccion vItem in refData) {
                vItem.DetailOrdenDeProduccionDetalleMateriales = new clsOrdenDeProduccionDetalleMaterialesNav().FillWithForeignInfo(vItem);
            }
        }

        #endregion //OrdenDeProduccionDetalleMateriales
        XElement IOrdenDeProduccionPdn.FindByConsecutivo(int valConsecutivoCompania, int valConsecutivo) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("Consecutivo", valConsecutivo);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Adm.OrdenDeProduccion");
            SQL.AppendLine("WHERE Consecutivo = @Consecutivo");
            SQL.AppendLine("AND ConsecutivoCompania = @ConsecutivoCompania");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }

        XElement IOrdenDeProduccionPdn.FindByConsecutivoCompaniaCodigo(int valConsecutivoCompania, string valCodigo) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Codigo", valCodigo, 15);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Adm.OrdenDeProduccion");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND Codigo = @Codigo");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IOrdenDeProduccionPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<OrdenDeProduccion>, IList<OrdenDeProduccion>> instanciaDal = new clsOrdenDeProduccionDat();
            IList<OrdenDeProduccion> vLista = new List<OrdenDeProduccion>();
            OrdenDeProduccion vCurrentRecord = new Galac.Adm.Dal.GestionProduccionOrdenDeProduccion();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.Codigo = "";
            vCurrentRecord.Descripcion = "";
            vCurrentRecord.StatusOp = '0';
            vCurrentRecord.ConsecutivoAlmacenProductoTerminado = 0;
            vCurrentRecord.ConsecutivoAlmacenMateriales = 0;
            vCurrentRecord.FechaCreacion = LibDate.Today();
            vCurrentRecord.FechaInicio = LibDate.Today();
            vCurrentRecord.FechaFinalizacion = LibDate.Today();
            vCurrentRecord.FechaAnulacion = LibDate.Today();
            vCurrentRecord.FechaAjuste = LibDate.Today();
            vCurrentRecord.AjustadaPostCierreAsBool = false;
            vCurrentRecord.Observacion = "''";
            vCurrentRecord.MotivoDeAnulacion = "''";
            vCurrentRecord.NumeroDecimales = 2;
            vCurrentRecord.CostoTerminadoCalculadoAPartirDeAsEnum = eFormaDeCalcularCostoTerminado.APartirDeMonedaLocal;
            vCurrentRecord.CodigoMonedaCostoProduccion = "";
            vCurrentRecord.CambioCostoProduccion = 0;
            vCurrentRecord.ConsecutivoListaDeMateriales = 0;
            vCurrentRecord.CantidadAProducir = 0;
            vCurrentRecord.CantidadProducida = 0;
            vCurrentRecord.NombreOperador = "";
            vCurrentRecord.FechaUltimaModificacion = LibDate.Today();
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<OrdenDeProduccion> ParseToListEntity(XElement valXmlEntity) {
            List<OrdenDeProduccion> vResult = new List<OrdenDeProduccion>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                OrdenDeProduccion vRecord = new OrdenDeProduccion();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Codigo"), null))) {
                    vRecord.Codigo = vItem.Element("Codigo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Descripcion"), null))) {
                    vRecord.Descripcion = vItem.Element("Descripcion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("StatusOp"), null))) {
                    vRecord.StatusOp = vItem.Element("StatusOp").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoAlmacenProductoTerminado"), null))) {
                    vRecord.ConsecutivoAlmacenProductoTerminado = LibConvert.ToInt(vItem.Element("ConsecutivoAlmacenProductoTerminado"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoAlmacenMateriales"), null))) {
                    vRecord.ConsecutivoAlmacenMateriales = LibConvert.ToInt(vItem.Element("ConsecutivoAlmacenMateriales"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaCreacion"), null))) {
                    vRecord.FechaCreacion = LibConvert.ToDate(vItem.Element("FechaCreacion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaInicio"), null))) {
                    vRecord.FechaInicio = LibConvert.ToDate(vItem.Element("FechaInicio"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaFinalizacion"), null))) {
                    vRecord.FechaFinalizacion = LibConvert.ToDate(vItem.Element("FechaFinalizacion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaAnulacion"), null))) {
                    vRecord.FechaAnulacion = LibConvert.ToDate(vItem.Element("FechaAnulacion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaAjuste"), null))) {
                    vRecord.FechaAjuste = LibConvert.ToDate(vItem.Element("FechaAjuste"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AjustadaPostCierre"), null))) {
                    vRecord.AjustadaPostCierre = vItem.Element("AjustadaPostCierre").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Observacion"), null))) {
                    vRecord.Observacion = vItem.Element("Observacion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MotivoDeAnulacion"), null))) {
                    vRecord.MotivoDeAnulacion = vItem.Element("MotivoDeAnulacion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDecimales"), null))) {
                    vRecord.NumeroDecimales = LibConvert.ToInt(vItem.Element("NumeroDecimales"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CostoTerminadoCalculadoAPartirDe"), null))) {
                    vRecord.CostoTerminadoCalculadoAPartirDe = vItem.Element("CostoTerminadoCalculadoAPartirDe").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoMonedaCostoProduccion"), null))) {
                    vRecord.CodigoMonedaCostoProduccion = vItem.Element("CodigoMonedaCostoProduccion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CambioCostoProduccion"), null))) {
                    vRecord.CambioCostoProduccion = LibConvert.ToDec(vItem.Element("CambioCostoProduccion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoListaDeMateriales"), null))) {
                    vRecord.ConsecutivoListaDeMateriales = LibConvert.ToInt(vItem.Element("ConsecutivoListaDeMateriales"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CantidadAProducir"), null))) {
                    vRecord.CantidadAProducir = LibConvert.ToDec(vItem.Element("CantidadAProducir"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CantidadProducida"), null))) {
                    vRecord.CantidadProducida = LibConvert.ToDec(vItem.Element("CantidadProducida"));
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

        protected override LibResponse DoSpecializedAction(IList<OrdenDeProduccion> refRecord, eAccionSR valAction, XmlReader valExtended, bool valUseDetail) {
            if (valAction == eAccionSR.Custom) {
                return IniciarOrdenDeProduccion(refRecord);
            } else if (valAction == eAccionSR.Anular) {
                return AnularOrdenDeProduccion(refRecord);
            } else if (valAction == eAccionSR.Cerrar) {
                return CerrarOrdenDeProduccion(refRecord);
            } else {
                return null;
            }
        }

        private LibResponse IniciarOrdenDeProduccion(IList<OrdenDeProduccion> refRecord) {
            LibResponse vResult = new LibResponse();
            if (refRecord[0].StatusOpAsEnum == eTipoStatusOrdenProduccion.Iniciada) {
                if (VerificaAsignacionDeLote(refRecord[0], true)
                    && VerificarExistenciaAlIniciar(refRecord[0])) {
                    using (TransactionScope vScope = LibBusiness.CreateScope()) {
                        vResult = base.UpdateRecord(refRecord, true, eAccionSR.Modificar);
                        //vResult.Success = vResult.Success && ActualizaCantidadMateriales(refRecord[0], false).Success;
                        vResult.Success = vResult.Success && CrearNotaDeEntradaSalidaAlIniciar(refRecord[0]).Success;
                        if (vResult.Success) {
                            vScope.Complete();
                        }
                    }
                }
            }
            return vResult;
        }
        private bool VerificarExistenciaAlIniciar(OrdenDeProduccion valOrdenDeProduccion) {
            IArticuloInventarioPdn vArticuloPdn = new clsArticuloInventarioNav();
            foreach (OrdenDeProduccionDetalleMateriales vOrdenDeProduccionDetalleMateriales in valOrdenDeProduccion.DetailOrdenDeProduccionDetalleMateriales) {
                if (vOrdenDeProduccionDetalleMateriales.TipoDeArticuloAsEnum == eTipoDeArticulo.Mercancia) {
                    decimal vDisponibilidad = 0;
                    if (vOrdenDeProduccionDetalleMateriales.TipoArticuloInvAsEnum == eTipoArticuloInv.Lote || vOrdenDeProduccionDetalleMateriales.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeVencimiento) {
                        vDisponibilidad = vArticuloPdn.DisponibilidadDeArticulo(vOrdenDeProduccionDetalleMateriales.ConsecutivoCompania, vOrdenDeProduccionDetalleMateriales.CodigoArticulo, vOrdenDeProduccionDetalleMateriales.ConsecutivoLoteDeInventario);
                    } else {
                        vDisponibilidad = vArticuloPdn.DisponibilidadDeArticulo(vOrdenDeProduccionDetalleMateriales.ConsecutivoCompania, vOrdenDeProduccionDetalleMateriales.CodigoAlmacen, vOrdenDeProduccionDetalleMateriales.CodigoArticulo, 1, "", "");
                    }
                    if (vDisponibilidad < vOrdenDeProduccionDetalleMateriales.CantidadReservadaInventario &&
                            (!LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "PermitirSobregiro"))) {
                        throw new GalacValidationException("No hay suficiente existencia de algunos materiales para producir este inventario.");
                    }
                }
            }
            return true;
        }



        private bool VerificaAsignacionDeLote(OrdenDeProduccion valOrdenDeProduccion, bool valAlIniciar) {
            if (valAlIniciar) {
                foreach (OrdenDeProduccionDetalleMateriales vOrdenDeProduccionDetalleMateriales in valOrdenDeProduccion.DetailOrdenDeProduccionDetalleMateriales) {
                    if (vOrdenDeProduccionDetalleMateriales.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeVencimiento || vOrdenDeProduccionDetalleMateriales.TipoArticuloInvAsEnum == eTipoArticuloInv.Lote) {
                        if (vOrdenDeProduccionDetalleMateriales.ConsecutivoLoteDeInventario == 0) {
                            throw new GalacValidationException("Hay artículos que no tienen lote asignado.");
                        }
                    }
                }
                return true;
            } else {
                foreach (OrdenDeProduccionDetalleArticulo vOrdenDeProduccionDetallArticulo in valOrdenDeProduccion.DetailOrdenDeProduccionDetalleArticulo ) {
                    if (vOrdenDeProduccionDetallArticulo.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeVencimiento || vOrdenDeProduccionDetallArticulo.TipoArticuloInvAsEnum == eTipoArticuloInv.Lote) {
                        if (vOrdenDeProduccionDetallArticulo.ConsecutivoLoteDeInventario == 0) {
                            throw new GalacValidationException("Hay artículos que no tienen lote asignado.");
                        }
                    }
                }
                return true;
            }
        }


        private LibResponse AnularOrdenDeProduccion(IList<OrdenDeProduccion> refRecord) {
            LibResponse vResult = new LibResponse();
            if (refRecord[0].StatusOpAsEnum == eTipoStatusOrdenProduccion.Anulada) {
                using (TransactionScope vScope = LibBusiness.CreateScope()) {
                    vResult = base.UpdateRecord(refRecord, true, eAccionSR.Modificar);
                    //vResult.Success = vResult.Success && ActualizaCantidadMateriales(refRecord[0], true).Success;
                    vResult.Success = vResult.Success && AnularOrdenDeEntradaSalida(refRecord[0]).Success;
                    if (vResult.Success) {
                        vScope.Complete();
                    }
                }
            }
            return vResult;
        }

        private LibResponse CerrarOrdenDeProduccion(IList<OrdenDeProduccion> refRecord) {
            LibResponse vResult = new LibResponse();
            if (refRecord != null && refRecord.Count > 0) {
                if (VerificaAsignacionDeLote(refRecord[0], false)){
                    vResult.Success = true;
                    XElement vDataXmlArticulo = BuscarInfoDeCostoDeArticulos(refRecord[0].DetailOrdenDeProduccionDetalleMateriales);
                    IList<OrdenDeProduccionDetalleMateriales> vList = new List<OrdenDeProduccionDetalleMateriales>();

                    foreach (OrdenDeProduccionDetalleMateriales vDetailOrdenDeProduccionDetalleMateriales in refRecord[0].DetailOrdenDeProduccionDetalleMateriales) {
                        vList.Add(vDetailOrdenDeProduccionDetalleMateriales);
                    }

                    XElement vData = new clsOrdenDeProduccionDetalleMaterialesNav().BuscaExistenciaDeArticulos(refRecord[0].ConsecutivoCompania, vList);
                    XElement vDataLote = new clsOrdenDeProduccionDetalleMaterialesNav().BuscaExistenciaDeArticulosPorLote(refRecord[0].ConsecutivoCompania, vList);
                    string vFNCostoUnitario = (refRecord[0].CostoTerminadoCalculadoAPartirDeAsEnum == eFormaDeCalcularCostoTerminado.APartirDeCostoEnMonedaLocal) ? "CostoUnitario" : "MeCostoUnitario";
                    var vDataArticulo = vDataXmlArticulo.Descendants("GpResult").Select(p => new {
                        CodigoArticulo = p.Element("Codigo").Value,
                        CostoUnitario = LibConvert.ToDec(p.Element(vFNCostoUnitario)),
                        Existencia = LibConvert.ToDec(p.Element("Existencia"))
                    }).ToList();
                    var vDataExistencia = vData.Descendants("GpResult").Select(p => new {
                        Existencia = LibConvert.ToDec(p.Element("Cantidad"), 8),
                        CodigoArticulo = p.Element("CodigoArticulo").Value,
                        ConsecutivoAlmacen = LibConvert.ToInt(p.Element("ConsecutivoAlmacen"))
                    }).ToList();
                    var vDataExistenciaLote = vDataLote.Descendants("GpResult").Select(p => new {
                        Existencia = LibConvert.ToDec(p.Element("Cantidad"), 8),
                        CodigoArticulo = p.Element("CodigoArticulo").Value,
                        ConsecutivoLoteDeInventario = LibConvert.ToInt(p.Element("ConsecutivoLoteDeInventario"))
                    }).ToList();
                    foreach (OrdenDeProduccionDetalleMateriales vOrdenDeProduccionDetalleMateriales in refRecord[0].DetailOrdenDeProduccionDetalleMateriales) {
                        vOrdenDeProduccionDetalleMateriales.CostoUnitarioArticuloInventario = LibMath.RoundToNDecimals(vDataArticulo.Where(p => p.CodigoArticulo == vOrdenDeProduccionDetalleMateriales.CodigoArticulo).FirstOrDefault().CostoUnitario, 2);
                        vOrdenDeProduccionDetalleMateriales.MontoSubtotal = LibMath.RoundToNDecimals(vOrdenDeProduccionDetalleMateriales.CostoUnitarioArticuloInventario * vOrdenDeProduccionDetalleMateriales.CantidadConsumida, 2);
                        if (vOrdenDeProduccionDetalleMateriales.TipoDeArticuloAsEnum == eTipoDeArticulo.Mercancia &&
                            (!LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "PermitirSobregiro")) &&
                            vOrdenDeProduccionDetalleMateriales.CantidadConsumida > vOrdenDeProduccionDetalleMateriales.CantidadReservadaInventario) {
                            if (vOrdenDeProduccionDetalleMateriales.TipoArticuloInvAsEnum == eTipoArticuloInv.Lote || vOrdenDeProduccionDetalleMateriales.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeVencimiento) {
                                if ((vOrdenDeProduccionDetalleMateriales.CantidadConsumida - vOrdenDeProduccionDetalleMateriales.CantidadReservadaInventario) > vDataExistenciaLote.Where(p => p.CodigoArticulo == vOrdenDeProduccionDetalleMateriales.CodigoArticulo && p.ConsecutivoLoteDeInventario == vOrdenDeProduccionDetalleMateriales.ConsecutivoLoteDeInventario).FirstOrDefault().Existencia) {
                                    throw new GalacValidationException("No hay suficiente existencia de algunos materiales para producir este inventario. (" + vOrdenDeProduccionDetalleMateriales.CodigoArticulo + "-"  + vOrdenDeProduccionDetalleMateriales.CodigoLote + ")");
                                }
                            } else {
                                if ((vOrdenDeProduccionDetalleMateriales.CantidadConsumida - vOrdenDeProduccionDetalleMateriales.CantidadReservadaInventario) > vDataExistencia.Where(p => p.CodigoArticulo == vOrdenDeProduccionDetalleMateriales.CodigoArticulo && p.ConsecutivoAlmacen == vOrdenDeProduccionDetalleMateriales.ConsecutivoAlmacen).FirstOrDefault().Existencia) {
                                    throw new GalacValidationException("No hay suficiente existencia de algunos materiales para producir este inventario. (" + vOrdenDeProduccionDetalleMateriales.CodigoArticulo + ")");
                                }
                            }
                        }
                    }
                    decimal vCostoTotal = refRecord[0].DetailOrdenDeProduccionDetalleMateriales.Sum(p => p.MontoSubtotal);

                    foreach (OrdenDeProduccionDetalleArticulo vOrdenDeProduccionDetalleArticulo in refRecord[0].DetailOrdenDeProduccionDetalleArticulo) {
                        vOrdenDeProduccionDetalleArticulo.MontoSubTotal = LibMath.RoundToNDecimals(vCostoTotal * (vOrdenDeProduccionDetalleArticulo.PorcentajeCostoCierre / 100), 2);
                        if (vOrdenDeProduccionDetalleArticulo.CantidadProducida == 0) {
                            vOrdenDeProduccionDetalleArticulo.CostoUnitario = 0;
                        } else {
                            vOrdenDeProduccionDetalleArticulo.CostoUnitario = LibMath.RoundToNDecimals(vOrdenDeProduccionDetalleArticulo.MontoSubTotal / vOrdenDeProduccionDetalleArticulo.CantidadProducida, 2);
                        }
                    }
                    using (TransactionScope vScope = LibBusiness.CreateScope()) {
                        vResult = base.UpdateRecord(refRecord, true, eAccionSR.Modificar);
                        vResult.Success = vResult.Success && ActualizaCantidadyCostoPorCierre(refRecord[0]).Success;
                        vResult.Success = vResult.Success && CrearNotaDeEntradaSalidaAlCerrar(refRecord[0]).Success;
                        if (vResult.Success) {
                            vScope.Complete();
                        }
                    }
                } else {
                    vResult.Success = false;
                    vResult.AddError("No se puede procesar la acción: cerrar");
                } }
            return vResult;
        }

        private XElement BuscarInfoDeCostoDeArticulos(ObservableCollection<OrdenDeProduccionDetalleMateriales> valOrdenDeProduccionDetalleMateriales) {
            XElement vResult = new XElement("GpData");
            clsOrdenDeProduccionDetalleMaterialesNav vOrdenDeProduccionDetalleMaterialesNav = new clsOrdenDeProduccionDetalleMaterialesNav();
            vResult.Add(vOrdenDeProduccionDetalleMaterialesNav.FindInfoArticuloInventario(valOrdenDeProduccionDetalleMateriales.ToList()).Descendants("GpResult"));
            return vResult;
        }

        private LibResponse ActualizaCantidadyCostoPorCierre(OrdenDeProduccion valOrdenDeProduccion) {
            LibResponse vResult = new LibResponse();
            vResult.Success = true;
            IArticuloInventarioPdn vArticuloPdn = new clsArticuloInventarioNav();
            List<ArticuloInventarioExistencia> vList = new List<ArticuloInventarioExistencia>();
            decimal vCantidad = 0;
            //foreach (OrdenDeProduccionDetalleMateriales vOrdenDeProduccionDetalleMateriales in valOrdenDeProduccion.DetailOrdenDeProduccionDetalleMateriales) {
            //    if (vOrdenDeProduccionDetalleMateriales.TipoDeArticuloAsEnum == eTipoDeArticulo.Mercancia) {
            //        vCantidad = vOrdenDeProduccionDetalleMateriales.CantidadReservadaInventario;
            //        bool vAgregar = false;
            //        if (vOrdenDeProduccionDetalleMateriales.CantidadReservadaInventario < vOrdenDeProduccionDetalleMateriales.CantidadConsumida) {
            //            vCantidad = (vOrdenDeProduccionDetalleMateriales.CantidadConsumida - vOrdenDeProduccionDetalleMateriales.CantidadReservadaInventario) * -1;
            //            vAgregar = true;
            //        } else if (vOrdenDeProduccionDetalleMateriales.CantidadReservadaInventario > vOrdenDeProduccionDetalleMateriales.CantidadConsumida) {
            //            vCantidad = vOrdenDeProduccionDetalleMateriales.CantidadReservadaInventario - vOrdenDeProduccionDetalleMateriales.CantidadConsumida;
            //            vAgregar = true;
            //        }
            //        if (vAgregar) {
            //            vList.Add(new ArticuloInventarioExistencia() {
            //                ConsecutivoCompania = vOrdenDeProduccionDetalleMateriales.ConsecutivoCompania,
            //                CodigoAlmacen = vOrdenDeProduccionDetalleMateriales.CodigoAlmacen,
            //                CodigoArticulo = vOrdenDeProduccionDetalleMateriales.CodigoArticulo,
            //                Cantidad = LibMath.RoundToNDecimals(vCantidad, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales")),
            //                Ubicacion = "",
            //                ConsecutivoAlmacen = vOrdenDeProduccionDetalleMateriales.ConsecutivoAlmacen,
            //                TipoActualizacion = eTipoActualizacion.Existencia,
            //                DetalleArticuloInventarioExistenciaSerial = new List<ArticuloInventarioExistenciaSerial>()
            //            });
            //        }
            //    }
            //}
            foreach (OrdenDeProduccionDetalleArticulo vOrdenDeProduccionDetalleArticulo in valOrdenDeProduccion.DetailOrdenDeProduccionDetalleArticulo) {
                vList.Add(new ArticuloInventarioExistencia() {
                    ConsecutivoCompania = vOrdenDeProduccionDetalleArticulo.ConsecutivoCompania,
                    CodigoAlmacen = vOrdenDeProduccionDetalleArticulo.CodigoAlmacen,
                    CodigoArticulo = vOrdenDeProduccionDetalleArticulo.CodigoArticulo,
                    Cantidad = LibMath.RoundToNDecimals(vOrdenDeProduccionDetalleArticulo.CantidadProducida, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales")),
                    Ubicacion = "",
                    ConsecutivoAlmacen = vOrdenDeProduccionDetalleArticulo.ConsecutivoAlmacen,
                    CostoUnitario = valOrdenDeProduccion.CostoTerminadoCalculadoAPartirDeAsEnum == eFormaDeCalcularCostoTerminado.APartirDeCostoEnMonedaExtranjera ?
                                    LibMath.RoundToNDecimals((vOrdenDeProduccionDetalleArticulo.CostoUnitario * valOrdenDeProduccion.CambioCostoProduccion), 2) : vOrdenDeProduccionDetalleArticulo.CostoUnitario,
                    CostoUnitarioME = valOrdenDeProduccion.CostoTerminadoCalculadoAPartirDeAsEnum == eFormaDeCalcularCostoTerminado.APartirDeCostoEnMonedaExtranjera ?
                                    vOrdenDeProduccionDetalleArticulo.CostoUnitario : LibMath.RoundToNDecimals((vOrdenDeProduccionDetalleArticulo.CostoUnitario / valOrdenDeProduccion.CambioCostoProduccion), 2),
                    TipoActualizacion = eTipoActualizacion.Costo,
                    DetalleArticuloInventarioExistenciaSerial = new List<ArticuloInventarioExistenciaSerial>()
                });
            }
            if (!LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaMonedaExtranjera"))) {
                foreach (ArticuloInventarioExistencia vArticuloInventario in vList) {
                    vArticuloInventario.CostoUnitarioME = 0;
                }
            }
            if (vList.Count > 0) {
                vResult.Success = vArticuloPdn.ActualizarExistencia(valOrdenDeProduccion.ConsecutivoCompania, vList) && vResult.Success;
            }
            return vResult;
        }

        private LibResponse ActualizaCantidadMateriales(OrdenDeProduccion valOrdenDeProduccion, bool valAumentaCantidad) {
            LibResponse vResult = new LibResponse();
            vResult.Success = true;
            IArticuloInventarioPdn vArticuloPdn = new clsArticuloInventarioNav();
            decimal vCantidad = 0;
            List<ArticuloInventarioExistencia> vList = new List<ArticuloInventarioExistencia>();
            foreach (OrdenDeProduccionDetalleMateriales vOrdenDeProduccionDetalleMateriales in valOrdenDeProduccion.DetailOrdenDeProduccionDetalleMateriales) {
                if (vOrdenDeProduccionDetalleMateriales.TipoDeArticuloAsEnum == eTipoDeArticulo.Mercancia) {
                    vCantidad = vOrdenDeProduccionDetalleMateriales.CantidadReservadaInventario;
                    if (!valAumentaCantidad) {
                        vCantidad = vCantidad * -1;
                    }
                    vList.Add(new ArticuloInventarioExistencia() {
                        ConsecutivoCompania = vOrdenDeProduccionDetalleMateriales.ConsecutivoCompania,
                        CodigoAlmacen = vOrdenDeProduccionDetalleMateriales.CodigoAlmacen,
                        CodigoArticulo = vOrdenDeProduccionDetalleMateriales.CodigoArticulo,
                        Cantidad = LibMath.RoundToNDecimals(vCantidad, 8),
                        Ubicacion = "",
                        ConsecutivoAlmacen = vOrdenDeProduccionDetalleMateriales.ConsecutivoAlmacen,
                        TipoActualizacion = eTipoActualizacion.Existencia,
                        DetalleArticuloInventarioExistenciaSerial = new List<ArticuloInventarioExistenciaSerial>()
                    });
                }
            }
            vResult.Success = vArticuloPdn.ActualizarExistencia(valOrdenDeProduccion.ConsecutivoCompania, vList) && vResult.Success;
            return vResult;
        }

        private LibResponse CrearNotaDeEntradaSalidaAlIniciar(OrdenDeProduccion valOrdenDeProduccion) {
            NotaDeEntradaSalida vNotaDeEntradaSalida = new NotaDeEntradaSalida();
            string vNumeroDocumento = "";
            vNotaDeEntradaSalida.ConsecutivoCompania = valOrdenDeProduccion.ConsecutivoCompania;
            vNotaDeEntradaSalida.NumeroDocumento = vNumeroDocumento;
            vNotaDeEntradaSalida.TipodeOperacionAsEnum = eTipodeOperacion.SalidadeInventario;
            vNotaDeEntradaSalida.CodigoCliente = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoGenericoCliente");
            vNotaDeEntradaSalida.CodigoLote = "0000000000";
            vNotaDeEntradaSalida.CodigoAlmacen = valOrdenDeProduccion.CodigoAlmacenMateriales;
            vNotaDeEntradaSalida.Fecha = valOrdenDeProduccion.FechaInicio;
            vNotaDeEntradaSalida.Comentarios = "Nota salida Inicio de Orden de Producción " + valOrdenDeProduccion.Codigo;
            vNotaDeEntradaSalida.StatusNotaEntradaSalidaAsEnum = eStatusNotaEntradaSalida.Vigente;
            vNotaDeEntradaSalida.ConsecutivoAlmacen = valOrdenDeProduccion.ConsecutivoAlmacenMateriales;
            vNotaDeEntradaSalida.GeneradoPorAsEnum = eTipoGeneradoPorNotaDeEntradaSalida.OrdenDeProduccion;
            vNotaDeEntradaSalida.ConsecutivoDocumentoOrigen = valOrdenDeProduccion.Consecutivo;
            vNotaDeEntradaSalida.TipoNotaProduccionAsEnum = eTipoNotaProduccion.Abrir;
            vNotaDeEntradaSalida.DetailRenglonNotaES = new ObservableCollection<RenglonNotaES>();
            int vIndex = 0;
            foreach (OrdenDeProduccionDetalleMateriales vOrdenDeProduccionDetalleMateriales in valOrdenDeProduccion.DetailOrdenDeProduccionDetalleMateriales) {
                vIndex++;
                RenglonNotaES vRenglonNotaES = new RenglonNotaES();
                vRenglonNotaES.ConsecutivoCompania = valOrdenDeProduccion.ConsecutivoCompania;                
                vRenglonNotaES.ConsecutivoRenglon = vIndex;
                vRenglonNotaES.CodigoArticulo = vOrdenDeProduccionDetalleMateriales.CodigoArticulo;
                vRenglonNotaES.Cantidad = vOrdenDeProduccionDetalleMateriales.CantidadReservadaInventario;
                vRenglonNotaES.TipoArticuloInvAsEnum = vOrdenDeProduccionDetalleMateriales.TipoArticuloInvAsEnum;
                vRenglonNotaES.Serial = "0";
                vRenglonNotaES.Rollo = "0";
                vRenglonNotaES.CostoUnitario = vOrdenDeProduccionDetalleMateriales.CostoUnitarioArticuloInventario;
                vRenglonNotaES.LoteDeInventario = vOrdenDeProduccionDetalleMateriales.CodigoLote;
                vRenglonNotaES.TipoArticuloInvAsEnum = vOrdenDeProduccionDetalleMateriales.TipoArticuloInvAsEnum;
                vRenglonNotaES.NumeroDocumento = valOrdenDeProduccion.Codigo;
                vNotaDeEntradaSalida.DetailRenglonNotaES.Add(vRenglonNotaES);
            }

            INotaDeEntradaSalidaPdn vNotaDeEntradaSalidaPdn = new clsNotaDeEntradaSalidaNav();
            IList<NotaDeEntradaSalida> valListNotaDeEntradaSalida = new List<NotaDeEntradaSalida>();
            valListNotaDeEntradaSalida.Add(vNotaDeEntradaSalida);
            return vNotaDeEntradaSalidaPdn.AgregarNotaDeEntradaSalida(valListNotaDeEntradaSalida);
        }

        private LibResponse CrearNotaDeEntradaSalidaAlCerrar(OrdenDeProduccion valOrdenDeProduccion) {
            NotaDeEntradaSalida vNotaDeEntrada = new NotaDeEntradaSalida();
            NotaDeEntradaSalida vNotaDeSalida = new NotaDeEntradaSalida();
            string vNumeroDocumento = valOrdenDeProduccion.Codigo;
            vNotaDeEntrada.ConsecutivoCompania = valOrdenDeProduccion.ConsecutivoCompania;
            vNotaDeEntrada.NumeroDocumento = vNumeroDocumento;
            vNotaDeEntrada.TipodeOperacionAsEnum = eTipodeOperacion.EntradadeInventario;
            vNotaDeEntrada.CodigoCliente = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoGenericoCliente");
            vNotaDeEntrada.CodigoLote = "0000000000";
            vNotaDeEntrada.CodigoAlmacen = valOrdenDeProduccion.CodigoAlmacenProductoTerminado;
            vNotaDeEntrada.Fecha = valOrdenDeProduccion.FechaFinalizacion;
            vNotaDeEntrada.Comentarios = "Nota entrada Cierre de Orden de Producción " + valOrdenDeProduccion.Codigo;
            vNotaDeEntrada.StatusNotaEntradaSalidaAsEnum = eStatusNotaEntradaSalida.Vigente;
            vNotaDeEntrada.ConsecutivoAlmacen = valOrdenDeProduccion.ConsecutivoAlmacenProductoTerminado;
            vNotaDeEntrada.GeneradoPorAsEnum = eTipoGeneradoPorNotaDeEntradaSalida.OrdenDeProduccion;
            vNotaDeEntrada.ConsecutivoDocumentoOrigen = valOrdenDeProduccion.Consecutivo;
            vNotaDeEntrada.TipoNotaProduccionAsEnum = eTipoNotaProduccion.Cerrar;
            vNotaDeEntrada.DetailRenglonNotaES = new ObservableCollection<RenglonNotaES>();
            vNotaDeSalida.ConsecutivoCompania = valOrdenDeProduccion.ConsecutivoCompania;
            vNotaDeSalida.NumeroDocumento = vNumeroDocumento;
            vNotaDeSalida.TipodeOperacionAsEnum = eTipodeOperacion.SalidadeInventario;
            vNotaDeSalida.CodigoCliente = "000000000A";
            vNotaDeSalida.CodigoAlmacen = valOrdenDeProduccion.CodigoAlmacenMateriales;
            vNotaDeSalida.Fecha = valOrdenDeProduccion.FechaFinalizacion;
            vNotaDeSalida.Comentarios = "Nota salida Cierre de Orden de Producción " + valOrdenDeProduccion.Codigo;
            vNotaDeSalida.CodigoLote = "0000000000";
            vNotaDeSalida.StatusNotaEntradaSalidaAsEnum = eStatusNotaEntradaSalida.Vigente;
            vNotaDeSalida.ConsecutivoAlmacen = valOrdenDeProduccion.ConsecutivoAlmacenMateriales;
            vNotaDeSalida.GeneradoPorAsEnum = eTipoGeneradoPorNotaDeEntradaSalida.OrdenDeProduccion;
            vNotaDeSalida.ConsecutivoDocumentoOrigen = valOrdenDeProduccion.Consecutivo;
            vNotaDeSalida.TipoNotaProduccionAsEnum = eTipoNotaProduccion.Cerrar;
            vNotaDeSalida.DetailRenglonNotaES = new ObservableCollection<RenglonNotaES>();
            foreach (var vOrdenDeProduccionDetalleArticulo in valOrdenDeProduccion.DetailOrdenDeProduccionDetalleArticulo) {
                vNotaDeEntrada.DetailRenglonNotaES.Add(new RenglonNotaES() {
                    ConsecutivoCompania = valOrdenDeProduccion.ConsecutivoCompania,
                    NumeroDocumento = vNumeroDocumento,
                    CodigoArticulo = vOrdenDeProduccionDetalleArticulo.CodigoArticulo,
                    Cantidad = vOrdenDeProduccionDetalleArticulo.CantidadProducida,                    
                    Serial = "0",
                    Rollo = "0",
                    CostoUnitario = valOrdenDeProduccion.CostoTerminadoCalculadoAPartirDeAsEnum == eFormaDeCalcularCostoTerminado.APartirDeCostoEnMonedaExtranjera ? LibMath.RoundToNDecimals((vOrdenDeProduccionDetalleArticulo.CostoUnitario * valOrdenDeProduccion.CambioCostoProduccion), 2) : vOrdenDeProduccionDetalleArticulo.CostoUnitario,
                    CostoUnitarioME = valOrdenDeProduccion.CostoTerminadoCalculadoAPartirDeAsEnum == eFormaDeCalcularCostoTerminado.APartirDeCostoEnMonedaExtranjera ? vOrdenDeProduccionDetalleArticulo.CostoUnitario : LibMath.RoundToNDecimals((vOrdenDeProduccionDetalleArticulo.CostoUnitario / valOrdenDeProduccion.CambioCostoProduccion), 2),
                    LoteDeInventario = vOrdenDeProduccionDetalleArticulo.CodigoLote,
                    TipoArticuloInvAsEnum = vOrdenDeProduccionDetalleArticulo.TipoArticuloInvAsEnum
            });
            }
            foreach (var vOrdenDeProduccionDetalleMateriales in valOrdenDeProduccion.DetailOrdenDeProduccionDetalleMateriales) {
                if (vOrdenDeProduccionDetalleMateriales.CantidadReservadaInventario < vOrdenDeProduccionDetalleMateriales.CantidadConsumida) {
                    vNotaDeSalida.DetailRenglonNotaES.Add(new RenglonNotaES() {
                        ConsecutivoCompania = valOrdenDeProduccion.ConsecutivoCompania,
                        NumeroDocumento = vNumeroDocumento,
                        CodigoArticulo = vOrdenDeProduccionDetalleMateriales.CodigoArticulo,
                        Cantidad = vOrdenDeProduccionDetalleMateriales.CantidadConsumida - vOrdenDeProduccionDetalleMateriales.CantidadReservadaInventario,
                        TipoArticuloInvAsEnum = vOrdenDeProduccionDetalleMateriales.TipoArticuloInvAsEnum,
                        Serial = "0",
                        Rollo = "0",
                        CostoUnitario = vOrdenDeProduccionDetalleMateriales.CostoUnitarioArticuloInventario,
                        LoteDeInventario = vOrdenDeProduccionDetalleMateriales.CodigoLote,
                    });
                } else if (vOrdenDeProduccionDetalleMateriales.CantidadReservadaInventario > vOrdenDeProduccionDetalleMateriales.CantidadConsumida) {
                    vNotaDeEntrada.DetailRenglonNotaES.Add(new RenglonNotaES() {
                        ConsecutivoCompania = valOrdenDeProduccion.ConsecutivoCompania,
                        NumeroDocumento = vNumeroDocumento,
                        CodigoArticulo = vOrdenDeProduccionDetalleMateriales.CodigoArticulo,
                        Cantidad = vOrdenDeProduccionDetalleMateriales.CantidadReservadaInventario - vOrdenDeProduccionDetalleMateriales.CantidadConsumida,
                        TipoArticuloInvAsEnum = vOrdenDeProduccionDetalleMateriales.TipoArticuloInvAsEnum,
                        Serial = "0",
                        Rollo = "0",
                        CostoUnitario = vOrdenDeProduccionDetalleMateriales.CostoUnitarioArticuloInventario,
                        LoteDeInventario = vOrdenDeProduccionDetalleMateriales.CodigoLote,
                    });
                }
            }
            INotaDeEntradaSalidaPdn vNotaDeEntradaSalidaPdn = new clsNotaDeEntradaSalidaNav();
            IList<NotaDeEntradaSalida> valListNotaDeEntradaSalida = new List<NotaDeEntradaSalida>();
            valListNotaDeEntradaSalida.Add(vNotaDeEntrada);
            if (vNotaDeSalida.DetailRenglonNotaES.Count > 0) {
                valListNotaDeEntradaSalida.Add(vNotaDeSalida);
            }
            return vNotaDeEntradaSalidaPdn.AgregarNotaDeEntradaSalida(valListNotaDeEntradaSalida);
        }

        private LibResponse AnularOrdenDeEntradaSalida(OrdenDeProduccion valOrdenDeProduccion) {
            INotaDeEntradaSalidaPdn vNotaDeEntradaSalidaPdn = new clsNotaDeEntradaSalidaNav();
            return vNotaDeEntradaSalidaPdn.AnularNotaDeSalidaAsociadaProduccion(valOrdenDeProduccion.ConsecutivoCompania, valOrdenDeProduccion.Consecutivo);
        }

        List<OrdenDeProduccionDetalleMateriales> IOrdenDeProduccionPdn.ObtenerDetalleInicialInsumos(int valConsecutivoCompania, int valConsecutivoListaDeMateriales, int valConsecutivoAlmacen, decimal valCantidadSolicitada) {
            IList<ListaDeMaterialesDetalleArticulo> vData;
            vData = new clsListaDeMaterialesDetalleArticuloNav().DetalleArticulos(valConsecutivoCompania, valConsecutivoListaDeMateriales);
            List<OrdenDeProduccionDetalleMateriales> vResult = new List<OrdenDeProduccionDetalleMateriales>();
            foreach (var item in vData) {
                vResult.Add(new OrdenDeProduccionDetalleMateriales() {
                    CodigoArticulo = item.CodigoArticuloInventario,
                    DescripcionArticulo = item.DescripcionArticuloInventario,
                    ConsecutivoAlmacen = valConsecutivoAlmacen,
                    UnidadDeVenta = item.UnidadDeVenta,
                    Cantidad = item.Cantidad,
                    CantidadReservadaInventario = LibMath.RoundToNDecimals(item.Cantidad * valCantidadSolicitada, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales")),
                    CostoUnitarioArticuloInventario = 0,
                    CostoUnitarioMEArticuloInventario = 0,
                    MontoSubtotal = 0,
                    TipoDeArticuloAsEnum = item.TipoDeArticuloAsEnum
                });
            }
            return vResult;
        }

        List<OrdenDeProduccionDetalleArticulo> IOrdenDeProduccionPdn.ObtenerDetalleInicialSalidas(int valConsecutivoCompania, int valConsecutivoAlmacenEntrada, int valConsecutivoListaDeMateriales, decimal valCantidadSolicitada) {
            IList<ListaDeMaterialesDetalleSalidas> vData;
            vData = new clsListaDeMaterialesDetalleSalidasNav().DetalleSalidas(valConsecutivoCompania, valConsecutivoListaDeMateriales);
            List<OrdenDeProduccionDetalleArticulo> vResult = new List<OrdenDeProduccionDetalleArticulo>();
            foreach (ListaDeMaterialesDetalleSalidas item in vData) {
                vResult.Add(new OrdenDeProduccionDetalleArticulo() {
                    ConsecutivoAlmacen = valConsecutivoAlmacenEntrada,
                    CodigoArticulo = item.CodigoArticuloInventario,
                    CantidadOriginalLista = item.Cantidad,
                    CantidadSolicitada = valCantidadSolicitada,
                    CantidadProducida = 0,
                    DescripcionArticulo = item.DescripcionArticuloInventario,
                    CostoUnitario = 0,
                    MontoSubTotal = 0,
                    UnidadDeVenta = item.UnidadDeVenta,
                    AjustadoPostCierreAsBool = false,
                    CantidadAjustada = 0,
                    PorcentajeCostoEstimado = item.PorcentajeDeCosto,
                    PorcentajeCostoCierre = item.PorcentajeDeCosto,
                    Costo = 0
                });
            }
            return vResult;
        }
    } //End of class clsOrdenDeProduccionNav

} //End of namespace Galac.Adm.Brl.GestionProduccion

