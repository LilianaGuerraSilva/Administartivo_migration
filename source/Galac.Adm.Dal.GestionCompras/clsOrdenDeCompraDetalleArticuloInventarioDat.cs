using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Threading;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Dal;
using LibGalac.Aos.DefGen;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Dal.GestionCompras {
    public class clsOrdenDeCompraDetalleArticuloInventarioDat : LibData, ILibDataDetailComponent<IList<OrdenDeCompraDetalleArticuloInventario>, IList<OrdenDeCompraDetalleArticuloInventario>>, ILibDataImport, IOrdenDeCompraDatDetallePdn, ILibDataImportBulkInsert {
        #region Variables
        LibTrn insTrn;
        OrdenDeCompraDetalleArticuloInventario _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private OrdenDeCompraDetalleArticuloInventario CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        eActionImpExp ILibDataImportBulkInsert.Action { get; set; }
        #endregion //Propiedades
        #region Constructores

        public clsOrdenDeCompraDetalleArticuloInventarioDat() {
            DbSchema = "Adm";
            insTrn = new LibTrn();
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(OrdenDeCompraDetalleArticuloInventario valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoOrdenDeCompra", valRecord.ConsecutivoOrdenDeCompra);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("CodigoArticulo", valRecord.CodigoArticulo, 30);
            vParams.AddInString("DescripcionArticulo", valRecord.DescripcionArticulo, 7000);
            vParams.AddInDecimal("Cantidad", valRecord.Cantidad, 2);
            vParams.AddInDecimal("CostoUnitario", valRecord.CostoUnitario, 2);
            vParams.AddInDecimal("CantidadRecibida", valRecord.CantidadRecibida, 2);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionDetail(OrdenDeCompra valRecord, eAccionSR eAccionSR) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoOrdenDeCompra", valRecord.Consecutivo);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valRecord));
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(OrdenDeCompraDetalleArticuloInventario valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoOrdenDeCompra", valRecord.ConsecutivoOrdenDeCompra);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(OrdenDeCompraDetalleArticuloInventario valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoOrdenDeCompra", valRecord.ConsecutivoOrdenDeCompra);
            vResult = vParams.Get();
            return vResult;
        }

        StringBuilder ParametrosDetail(OrdenDeCompra valMaster) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valMaster.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoOrdenDeCompra", valMaster.Consecutivo);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement ParseToXml(OrdenDeCompra valEntidad) {
            List<OrdenDeCompra> vListEntidades = new List<OrdenDeCompra>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", vEntity.ConsecutivoCompania),
                    new XElement("Consecutivo", vEntity.Consecutivo),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(OrdenDeCompra valMaster) {
            XElement vXElement = new XElement("GpDataOrdenDeCompraDetalleArticuloInventario",
                from vEntity in valMaster.DetailOrdenDeCompraDetalleArticuloInventario
                select new XElement("GpDetailOrdenDeCompraDetalleArticuloInventario",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("ConsecutivoOrdenDeCompra", valMaster.Consecutivo),
                    new XElement("Consecutivo", vEntity.Consecutivo),
                    new XElement("CodigoArticulo", vEntity.CodigoArticulo),
                    new XElement("DescripcionArticulo", vEntity.DescripcionArticulo),
                    new XElement("Cantidad", vEntity.Cantidad),
                    new XElement("CostoUnitario", vEntity.CostoUnitario),
                    new XElement("CantidadRecibida", vEntity.CantidadRecibida)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<OrdenDeCompraDetalleArticuloInventario>, IList<OrdenDeCompraDetalleArticuloInventario>>

        IList<OrdenDeCompraDetalleArticuloInventario> ILibDataDetailComponent<IList<OrdenDeCompraDetalleArticuloInventario>, IList<OrdenDeCompraDetalleArticuloInventario>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<OrdenDeCompraDetalleArticuloInventario> vResult = new List<OrdenDeCompraDetalleArticuloInventario>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<OrdenDeCompraDetalleArticuloInventario>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<OrdenDeCompraDetalleArticuloInventario>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Orden De Compra.Insertar")]
        LibResponse ILibDataDetailComponent<IList<OrdenDeCompraDetalleArticuloInventario>, IList<OrdenDeCompraDetalleArticuloInventario>>.Insert(IList<OrdenDeCompraDetalleArticuloInventario> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "OrdenDeCompraDetalleArticuloInventarioINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<OrdenDeCompraDetalleArticuloInventario>, IList<OrdenDeCompraDetalleArticuloInventario>>
        public bool InsertChild(OrdenDeCompra valRecord, LibTrn insTrn) {
            bool vResult = false;
            vResult = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "OrdenDeCompraDetalleArticuloInventarioInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigoArticulo(valAction, CurrentRecord.CodigoArticulo);
            vResult = IsValidDescripcionArticulo(valAction, CurrentRecord.DescripcionArticulo) && vResult;
            vResult = IsValidCantidad(valAction, CurrentRecord.Cantidad) && vResult;
            vResult = IsValidCostoUnitario(valAction, CurrentRecord.CostoUnitario) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }
        private bool IsValidCodigoArticulo(eAccionSR valAction, string valCodigoArticulo) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoArticulo = LibString.Trim(valCodigoArticulo);
            if (LibString.IsNullOrEmpty(valCodigoArticulo, true)) {
                BuildValidationInfo(MsgRequiredField("Código Inventario"));
                vResult = false;
            }
            return vResult;
        }
        private bool IsValidDescripcionArticulo(eAccionSR valAction, string valDescripcionArticulo) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valDescripcionArticulo = LibString.Trim(valDescripcionArticulo);
            if (LibString.IsNullOrEmpty(valDescripcionArticulo, true)) {
                BuildValidationInfo(MsgRequiredField("Descripción"));
                vResult = false;
            }
            return vResult;
        }
        private bool IsValidCantidad(eAccionSR valAction, decimal valCantidad) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCantidad == 0) {
                BuildValidationInfo(MsgRequiredField("Cantidad"));
                vResult = false;
            }
            return vResult;
        }
        private bool IsValidCostoUnitario(eAccionSR valAction, decimal valCostoUnitario) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCostoUnitario == 0) {
                BuildValidationInfo(MsgRequiredField("Costo Unitario"));
                vResult = false;
            }
            return vResult;
        }
        private bool KeyExists(int valConsecutivoCompania, int valConsecutivoOrdenDeCompra, int valConsecutivo) {
            bool vResult = false;
            OrdenDeCompraDetalleArticuloInventario vRecordBusqueda = new OrdenDeCompraDetalleArticuloInventario();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.ConsecutivoOrdenDeCompra = valConsecutivoOrdenDeCompra;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".OrdenDeCompraDetalleArticuloInventario", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones
        public bool GetDetailAndAppendToMaster(ref List<OrdenDeCompra> refMaster) {
            bool vResult = false;
            IList<OrdenDeCompraDetalleArticuloInventario> vDetail = null;
            foreach (OrdenDeCompra vItemMaster in refMaster) {
                vItemMaster.DetailOrdenDeCompraDetalleArticuloInventario = new ObservableCollection<OrdenDeCompraDetalleArticuloInventario>();
                vDetail = new LibDatabase().LoadFromSp<OrdenDeCompraDetalleArticuloInventario>("Adm.Gp_OrdenDeCompraDetalleArticuloInventarioSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (OrdenDeCompraDetalleArticuloInventario vItemDetail in vDetail) {
                    vItemMaster.DetailOrdenDeCompraDetalleArticuloInventario.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }

        LibResponse IOrdenDeCompraDatDetallePdn.InsertarListaDeOrdenDeCompraDetail(IList<OrdenDeCompraDetalleArticuloInventario> valListOfRecords) {
            return InsertDetaill(valListOfRecords);
        }
        private LibResponse InsertDetaill(IList<OrdenDeCompraDetalleArticuloInventario> refRecord) {
            LibResponse vResult = new LibResponse();
            insTrn.StartTransaction();
            string vErrMsg = "";
            try {
                foreach (var item in refRecord) {
                    CurrentRecord = item;
                    if (ExecuteProcessBeforeInsert()) {
                        if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                            if (insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "OrdenDeCompraDetalleArticuloInventarioINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
                                vResult.Success = true;
                                if (vResult.Success) {
                                    ExecuteProcessAfterInsert();
                                }
                            }
                        }
                    }
                }
                insTrn.CommitTransaction();
                return vResult;
            } finally {
                if (!vResult.Success) {
                    insTrn.RollBackTransaction();
                }
            }
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "Orden De Compra.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Orden De Compra.Importar")]
        LibXmlResult ILibDataImport.Import(XmlReader refRecord, LibProgressManager valManager, bool valShowMessage) {
            throw new ProgrammerMissingCodeException("PROGRAMADOR: El codigo generado bajo el atributo IMPEXP del record, es solo referencial. DEBE AJUSTARLO ya que el Narrador actualmente desconoce la estructura de su archivo de importacion!!!!");
            try {
                string vMessage = "";
                int vIndex = 0;
                LibXmlResult vResult = new LibXmlResult();
                vResult.AddTitle("Importación Orden De Compra Detalle Articulo Inventario");
                List<OrdenDeCompraDetalleArticuloInventario> vList = ParseToListEntity(refRecord);
                if (vList.Count > 0) {
                    LibDatabase insDb = new LibDatabase();
                    if (((ILibDataImportBulkInsert)this).Action == eActionImpExp.eAIE_Instalar) {
                        try {
                            string vTablename = DbSchema + ".OrdenDeCompraDetalleArticuloInventario";
                            DataTable vDataTable = insDb.ParseListToDt<OrdenDeCompraDetalleArticuloInventario>(vList, vTablename, valManager);
                            valManager.ReportProgress(vList.Count, "Realizando inserción en lote, por favor espere...", string.Empty, false);
                            insDb.BulkInsert(vDataTable, vTablename);
                        } catch (System.Data.SqlClient.SqlException vEx) {
                            throw new GalacException("Error procesando los datos, debe verificar los datos a importar.", eExceptionManagementType.Controlled, vEx);
                        }
                    } else {
                        int vTotal = vList.Count;
                        foreach (OrdenDeCompraDetalleArticuloInventario item in vList) {
                            try {
                                vMessage = string.Format("Insertando {0:n0} de {1:n0}", vIndex, vTotal);
                                insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "OrdenDeCompraDetalleArticuloInventarioINST"), ParametrosActualizacion(item, eAccionSR.ReInstalar));
                            } catch (System.Data.SqlClient.SqlException vEx) {
                                if (LibExceptionMng.IsPrimaryKeyViolation(vEx)) {
                                    vResult.AddDetailWithAttribute(item.CodigoArticulo, "Ya existe", eXmlResultType.Error);
                                } else {
                                    throw;
                                }
                            }
                            if (valManager.CancellationPending) {
                                break;
                            }
                            vIndex++;
                            valManager.ReportProgress(vIndex, "Ejecutando por favor espere...", vMessage, (vIndex >= vTotal) && (valShowMessage));
                        }
                    }
                    insDb.Dispose();
                }
                return vResult;
            } catch (Exception) {
                throw;
            }
        }
        private List<OrdenDeCompraDetalleArticuloInventario> ParseToListEntity(XmlReader valXmlEntity) {
            List<OrdenDeCompraDetalleArticuloInventario> vResult = new List<OrdenDeCompraDetalleArticuloInventario>();
            XDocument xDoc = XDocument.Load(valXmlEntity);
            var vEntity = from vRecord in xDoc.Descendants("GpResult")
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
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("DescripcionArticulo"), null))) {
                    vRecord.DescripcionArticulo = vItem.Element("DescripcionArticulo").Value;
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
        #endregion //Metodos Generados
    } //End of class clsOrdenDeCompraDetalleArticuloInventarioDat

} //End of namespace Galac.Adm.Dal.GestionCompras