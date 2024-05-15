using System;
using System.Linq;
using System.Collections.Generic;
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
using Galac.Adm.Ccl.GestionProduccion;

namespace Galac.Adm.Dal.GestionProduccion {
    public class clsOrdenDeProduccionDat : LibData, ILibDataMasterComponentWithSearch<IList<OrdenDeProduccion>, IList<OrdenDeProduccion>>, ILibDataRpt {
        #region Variables
        LibDataScope insTrn;
        OrdenDeProduccion _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private OrdenDeProduccion CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsOrdenDeProduccionDat() {
            DbSchema = "Adm";
            insTrn = new LibDataScope();
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(OrdenDeProduccion valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("Codigo", valRecord.Codigo, 15);
            vParams.AddInString("Descripcion", valRecord.Descripcion, 250);
            vParams.AddInEnum("StatusOp", valRecord.StatusOpAsDB);
            vParams.AddInInteger("ConsecutivoAlmacenProductoTerminado", valRecord.ConsecutivoAlmacenProductoTerminado);
            vParams.AddInInteger("ConsecutivoAlmacenMateriales", valRecord.ConsecutivoAlmacenMateriales);
            vParams.AddInDateTime("FechaCreacion", valRecord.FechaCreacion);
            vParams.AddInDateTime("FechaInicio", valRecord.FechaInicio);
            vParams.AddInDateTime("FechaFinalizacion", valRecord.FechaFinalizacion);
            vParams.AddInDateTime("FechaAnulacion", valRecord.FechaAnulacion);
            vParams.AddInDateTime("FechaAjuste", valRecord.FechaAjuste);
            vParams.AddInBoolean("AjustadaPostCierre", valRecord.AjustadaPostCierreAsBool);
            vParams.AddInString("Observacion", valRecord.Observacion, 600);
            vParams.AddInString("MotivoDeAnulacion", valRecord.MotivoDeAnulacion, 600);
            if (valAction == eAccionSR.Insertar) {
                vParams.AddInInteger("NumeroDecimales", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales"));
            }
            vParams.AddInEnum("CostoTerminadoCalculadoAPartirDe", valRecord.CostoTerminadoCalculadoAPartirDeAsDB);
            vParams.AddInString("CodigoMonedaCostoProduccion", valRecord.CodigoMonedaCostoProduccion, 4);
            vParams.AddInDecimal("CambioCostoProduccion", valRecord.CambioCostoProduccion, 4);
            vParams.AddInInteger("ConsecutivoListaDeMateriales", valRecord.ConsecutivoListaDeMateriales);
            vParams.AddInString("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(OrdenDeProduccion valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(OrdenDeProduccion valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClavePorCodigo(OrdenDeProduccion valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Consecutivo", valRecord.Codigo, 15);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataMasterComponent<IList<OrdenDeProduccion>, IList<OrdenDeProduccion>>

        LibResponse ILibDataMasterComponent<IList<OrdenDeProduccion>, IList<OrdenDeProduccion>>.CanBeChoosen(IList<OrdenDeProduccion> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            OrdenDeProduccion vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (vSbInfo.Length == 0) {
                    vResult.Success = true;
                }
            } else {
                vResult.Success = true;
            }
            insDB.Dispose();
            if (!vResult.Success) {
                vErrMsg = LibResMsg.InfoForeignKeyCanNotBeDeleted(vSbInfo.ToString());
                throw new GalacAlertException(vErrMsg);
            } else {
                return vResult;
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Orden de Producción.Eliminar")]
        LibResponse ILibDataMasterComponent<IList<OrdenDeProduccion>, IList<OrdenDeProduccion>>.Delete(IList<OrdenDeProduccion> refRecord) {
            LibResponse vResult = new LibResponse();
            try {
                string vErrMsg = "";
                CurrentRecord = refRecord[0];
                if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                    if (ExecuteProcessBeforeDelete()) {
                        vResult.Success = insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "OrdenDeProduccionDEL"), ParametrosClave(CurrentRecord, true, true));
                        if (vResult.Success) {
                            ExecuteProcessAfterDelete();
                        }
                    }
                } else {
                    throw new GalacException(vErrMsg, eExceptionManagementType.Validation);
                }
                return vResult;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        IList<OrdenDeProduccion> ILibDataMasterComponent<IList<OrdenDeProduccion>, IList<OrdenDeProduccion>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters, bool valUseDetail) {
            List<OrdenDeProduccion> vResult = new List<OrdenDeProduccion>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<OrdenDeProduccion>(valProcessMessage, valParameters, CmdTimeOut);
                    if (valUseDetail && vResult != null && vResult.Count > 0) {
                        new clsOrdenDeProduccionDetalleArticuloDat().GetDetailAndAppendToMaster(ref vResult);
                        new clsOrdenDeProduccionDetalleMaterialesDat().GetDetailAndAppendToMaster(ref vResult);
                    }
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Orden de Producción.Insertar")]
        LibResponse ILibDataMasterComponent<IList<OrdenDeProduccion>, IList<OrdenDeProduccion>>.Insert(IList<OrdenDeProduccion> refRecord, bool valUseDetail) {
            LibResponse vResult = new LibResponse();
            try {
                CurrentRecord = refRecord[0];
                //la generacin del cosnecutivo anteriormente estaba en ExecuteProcessBeforeInsert()
                CurrentRecord.Consecutivo = new LibDatabase().NextLngConsecutive(DbSchema + ".OrdenDeProduccion", "Consecutivo", ParametrosProximoConsecutivo(CurrentRecord));
                if (ExecuteProcessBeforeInsert()) {
                    if (ValidateMasterDetail(eAccionSR.Insertar, CurrentRecord, valUseDetail)) {
                        if (insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "OrdenDeProduccionINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
                            if (valUseDetail) {
                                vResult.Success = true;
                                InsertDetail(CurrentRecord);
                            } else {
                                vResult.Success = true;
                            }
                            if (vResult.Success) {
                                ExecuteProcessAfterInsert();
                            }
                        }
                    }
                }
                return vResult;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        XElement ILibDataMasterComponent<IList<OrdenDeProduccion>, IList<OrdenDeProduccion>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoCodigo")) {
                        vResult = LibXml.ValueToXElement(insDb.NextStrConsecutive(DbSchema + ".OrdenDeProduccion", "Codigo", valParameters, true, 15), "Codigo");
                    }
                    break;
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                case eProcessMessageType.Query:
                    vResult = LibXml.ToXElement(insDb.LoadData(valProcessMessage, CmdTimeOut, valParameters));
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        LibResponse ILibDataMasterComponent<IList<OrdenDeProduccion>, IList<OrdenDeProduccion>>.SpecializedUpdate(IList<OrdenDeProduccion> refRecord, bool valUseDetail, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Orden de Producción.Modificar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Orden de Producción.Iniciar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Orden de Producción.Cerrar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Orden de Producción.Anular")]
        LibResponse ILibDataMasterComponent<IList<OrdenDeProduccion>, IList<OrdenDeProduccion>>.Update(IList<OrdenDeProduccion> refRecord, bool valUseDetail, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            try {
                CurrentRecord = refRecord[0];
                if (ValidateMasterDetail(valAction, CurrentRecord, valUseDetail)) {
                    if (ExecuteProcessBeforeUpdate()) {
                        if (valUseDetail) {
                            vResult = UpdateMasterAndDetail(CurrentRecord, valAction);
                        } else {
                            vResult = UpdateMaster(CurrentRecord, valAction); //por si requiriese especialización por acción
                        }
                        if (vResult.Success) {
                            ExecuteProcessAfterUpdate();
                        }
                    }
                }
                return vResult;
            } catch (GalacException vEx) {
                throw vEx;
            }
        }

        bool ILibDataMasterComponent<IList<OrdenDeProduccion>, IList<OrdenDeProduccion>>.ValidateAll(IList<OrdenDeProduccion> refRecords, bool valUseDetail, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (OrdenDeProduccion vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }
        #endregion //ILibDataMasterComponent<IList<OrdenDeProduccion>, IList<OrdenDeProduccion>>

        LibResponse UpdateMaster(OrdenDeProduccion refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            vResult.Success = insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "OrdenDeProduccionUPD"), ParametrosActualizacion(refRecord, valAction));
            return vResult;
        }

        LibResponse UpdateMasterAndDetail(OrdenDeProduccion refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            string vErrorMessage = "";
            if (ValidateDetail(refRecord, eAccionSR.Modificar, out vErrorMessage)) {
                if (UpdateDetail(refRecord)) {
                    vResult = UpdateMaster(refRecord, valAction);
                }
            }
            return vResult;
        }

        private bool InsertDetail(OrdenDeProduccion valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailOrdenDeProduccionDetalleArticuloAndUpdateDb(valRecord);
            vResult = vResult && SetPkInDetailOrdenDeProduccionDetalleMaterialesAndUpdateDb(valRecord);
            return vResult;
        }

        private bool SetPkInDetailOrdenDeProduccionDetalleArticuloAndUpdateDb(OrdenDeProduccion valRecord) {
            bool vResult = false;
            int vConsecutivo = 1;
            clsOrdenDeProduccionDetalleArticuloDat insOrdenDeProduccionDetalleArticulo = new clsOrdenDeProduccionDetalleArticuloDat();
            foreach (OrdenDeProduccionDetalleArticulo vDetail in valRecord.DetailOrdenDeProduccionDetalleArticulo) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.ConsecutivoOrdenDeProduccion = valRecord.Consecutivo;
                vDetail.Consecutivo = vConsecutivo;
                vConsecutivo++;
            }
            vResult = insOrdenDeProduccionDetalleArticulo.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool SetPkInDetailOrdenDeProduccionDetalleMaterialesAndUpdateDb(OrdenDeProduccion valRecord) {
            bool vResult = false;
            int vConsecutivo = 1;
            clsOrdenDeProduccionDetalleMaterialesDat insOrdenDeProduccionDetalleMateriales = new clsOrdenDeProduccionDetalleMaterialesDat();
            foreach (OrdenDeProduccionDetalleMateriales vDetail in valRecord.DetailOrdenDeProduccionDetalleMateriales) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.ConsecutivoOrdenDeProduccion = valRecord.Consecutivo;
                vDetail.Consecutivo = vConsecutivo;
                vConsecutivo++;
            }
            vResult = insOrdenDeProduccionDetalleMateriales.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool UpdateDetail(OrdenDeProduccion valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailOrdenDeProduccionDetalleArticuloAndUpdateDb(valRecord);
            vResult = vResult && SetPkInDetailOrdenDeProduccionDetalleMaterialesAndUpdateDb(valRecord);
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo);
            vResult = IsValidConsecutivo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo) && vResult;
            vResult = IsValidCodigo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Codigo) && vResult;
            vResult = IsValidDescripcion(valAction, CurrentRecord.Descripcion) && vResult;
            vResult = IsValidConsecutivoAlmacenProductoTerminado(valAction, CurrentRecord.ConsecutivoAlmacenProductoTerminado) && vResult;
            vResult = IsValidConsecutivoAlmacenMateriales(valAction, CurrentRecord.ConsecutivoAlmacenMateriales) && vResult;
            vResult = IsValidFechaCreacion(valAction, CurrentRecord.FechaCreacion) && vResult;
            vResult = IsValidFechaInicio(valAction, CurrentRecord.FechaInicio) && vResult;
            vResult = IsValidFechaFinalizacion(valAction, CurrentRecord.FechaFinalizacion) && vResult;
            vResult = IsValidFechaAnulacion(valAction, CurrentRecord.FechaAnulacion) && vResult;
            // vResult = IsValidFechaAjuste(valAction, CurrentRecord.FechaAjuste) && vResult;
            vResult = IsValidCodigoMonedaCostoProduccion(valAction, CurrentRecord.CodigoMonedaCostoProduccion) && vResult;
            vResult = IsValidConsecutivoListaDeMateriales(valAction, CurrentRecord.ConsecutivoListaDeMateriales) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoCompania <= 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Compania"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidConsecutivo(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Insertar)) {
                return true;
            }
            if (valConsecutivo == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                OrdenDeProduccion vRecBusqueda = new OrdenDeProduccion();
                vRecBusqueda.Consecutivo = valConsecutivo;
                if (KeyExists(valConsecutivoCompania, valConsecutivo)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Consecutivo", valConsecutivo));
                    vResult = false;
                }
            }
            return vResult;
        }
        private bool IsValidCodigo(eAccionSR valAction, int valConsecutivoCompania, string valCodigo) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigo = LibString.Trim(valCodigo);
            if (LibString.IsNullOrEmpty(valCodigo, true)) {
                BuildValidationInfo(MsgRequiredField("Código de Orden"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                OrdenDeProduccion valRecordBusqueda = new OrdenDeProduccion() { ConsecutivoCompania = valConsecutivoCompania, Codigo = valCodigo };
                if (KeyExists(valConsecutivoCompania, valRecordBusqueda)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Código de Orden", valCodigo));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidDescripcion(eAccionSR valAction, string valDescripcion) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valDescripcion = LibString.Trim(valDescripcion);
            if (LibString.IsNullOrEmpty(valDescripcion, true)) {
                BuildValidationInfo(MsgRequiredField("Descripción"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidConsecutivoAlmacenProductoTerminado(eAccionSR valAction, int valConsecutivoAlmacenProductoTerminado) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoAlmacenProductoTerminado == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Almacen Producto Terminado"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidConsecutivoAlmacenMateriales(eAccionSR valAction, int valConsecutivoAlmacenMateriales) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoAlmacenMateriales == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Almacen Materiales"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaCreacion(eAccionSR valAction, DateTime valFechaCreacion) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaCreacion, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaInicio(eAccionSR valAction, DateTime valFechaInicio) {
            bool vResult = true;
            if ((valAction != eAccionSR.Custom)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaInicio, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaFinalizacion(eAccionSR valAction, DateTime valFechaFinalizacion) {
            bool vResult = true;
            if ((valAction != eAccionSR.Cerrar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaFinalizacion, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaAnulacion(eAccionSR valAction, DateTime valFechaAnulacion) {
            bool vResult = true;
            if ((valAction != eAccionSR.Anular)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaAnulacion, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaAjuste(eAccionSR valAction, DateTime valFechaAjuste) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaAjuste, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidCodigoMonedaCostoProduccion(eAccionSR valAction, string valCodigoMonedaCostoProduccion) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoMonedaCostoProduccion = LibString.Trim(valCodigoMonedaCostoProduccion);
            if (LibString.IsNullOrEmpty(valCodigoMonedaCostoProduccion, true)) {
                BuildValidationInfo(MsgRequiredField("Código Moneda Para El Costo"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Moneda", "Codigo", insDb.InsSql.ToSqlValue(valCodigoMonedaCostoProduccion), true)) {
                    BuildValidationInfo("El valor asignado al campo Código Moneda Para El Costo no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }
		
		 private bool IsValidConsecutivoListaDeMateriales(eAccionSR valAction, int valConsecutivoListaDeMateriales){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoListaDeMateriales == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Lista De Materiales"));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivo) {
            bool vResult = false;
            OrdenDeProduccion vRecordBusqueda = new OrdenDeProduccion();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".OrdenDeProduccion", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, OrdenDeProduccion valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            vResult = insDb.ExistsRecord(DbSchema + ".OrdenDeProduccion", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool ValidateMasterDetail(eAccionSR valAction, OrdenDeProduccion valRecordMaster, bool valUseDetail) {
            bool vResult = false;
            string vErrMsg;
            if (Validate(valAction, out vErrMsg)) {
                if (valUseDetail) {
                    if (ValidateDetail(valRecordMaster, eAccionSR.Insertar, out vErrMsg)) {
                        vResult = true;
                    } else {
                        throw new GalacValidationException("Orden de Producción (detalle)\n" + vErrMsg);
                    }
                } else {
                    vResult = true;
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        private bool ValidateDetail(OrdenDeProduccion valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            outErrorMessage = "";
            vResult = vResult && ValidateDetailOrdenDeProduccionDetalleArticulo(valRecord, valAction, out outErrorMessage);
            vResult = vResult && ValidateDetailOrdenDeProduccionDetalleMateriales(valRecord, valAction, out outErrorMessage);
            return vResult;
        }

        private bool ValidateDetailOrdenDeProduccionDetalleArticulo(OrdenDeProduccion valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            outErrorMessage = string.Empty;
            foreach (OrdenDeProduccionDetalleArticulo vDetail in valRecord.DetailOrdenDeProduccionDetalleArticulo) {
                bool vLineHasError = true;
                //agregar validaciones                                
                if (vDetail.ConsecutivoAlmacen == 0) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Consecutivo Almacen.");
                } else if (LibString.IsNullOrEmpty(vDetail.CodigoArticulo)) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Código de Artículo.");
                } else {
                    vLineHasError = false;
                }
                vResult = vResult && (!vLineHasError);
                vNumeroDeLinea++;
            }
            if (!vResult) {
                outErrorMessage = "Salidas"  + Environment.NewLine + vSbErrorInfo.ToString();
            }
            return vResult;
        }
		
		 private bool ValidateDetailOrdenDeProduccionDetalleMateriales(OrdenDeProduccion valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            outErrorMessage = string.Empty;
            foreach (OrdenDeProduccionDetalleMateriales vDetail in valRecord.DetailOrdenDeProduccionDetalleMateriales) {
                bool vLineHasError = true;
                //agregar validaciones
                if (vDetail.ConsecutivoAlmacen == 0) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Consecutivo Almacen.");
                } else if (LibString.IsNullOrEmpty(vDetail.CodigoArticulo)) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Código de Artículo.");
                } else {
                    vLineHasError = false;
                }
                vResult = vResult && (!vLineHasError);
                vNumeroDeLinea++;
            }
            if (!vResult) {
                outErrorMessage = "Insumos"  + Environment.NewLine + vSbErrorInfo.ToString();
            }
            return vResult;
        }
		
        #endregion //Validaciones
        #region Miembros de ILibDataFKSearch
        bool ILibDataFKSearch.ConnectFk(ref XmlDocument refResulset, eProcessMessageType valType, string valProcessMessage, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase();
            refResulset = insDb.LoadForConnect(valProcessMessage, valXmlParamsExpression, CmdTimeOut);
            if (refResulset != null && refResulset.DocumentElement != null && refResulset.DocumentElement.HasChildNodes) {
                vResult = true;
            }
            return vResult;
        }
        #endregion //Miembros de ILibDataFKSearch

        #region //Miembros de ILibDataRpt

        System.Data.DataTable ILibDataRpt.GetDt(string valSqlStringCommand, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSqlStringCommand, valCmdTimeout);
        }

        System.Data.DataTable ILibDataRpt.GetDt(string valSpName, StringBuilder valXmlParamsExpression, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSpName, valXmlParamsExpression, valCmdTimeout);
        }
        #endregion ////Miembros de ILibDataRpt
        #endregion //Metodos Generados


    } //End of class clsOrdenDeProduccionDat

} //End of namespace Galac.Adm.Dal.GestionProduccion

