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
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Lib;

namespace Galac.Saw.Dal.Inventario {
    public class clsNotaDeEntradaSalidaDat: LibData, ILibDataMasterComponentWithSearch<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>>, ILibDataRpt {
        #region Variables
        LibDataScope insTrn;
        NotaDeEntradaSalida _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private NotaDeEntradaSalida CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsNotaDeEntradaSalidaDat() {
            DbSchema = "dbo";
            insTrn = new LibDataScope();
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(NotaDeEntradaSalida valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NumeroDocumento", valRecord.NumeroDocumento, 11);
            vParams.AddInEnum("TipodeOperacion", valRecord.TipodeOperacionAsDB);
            vParams.AddInString("CodigoCliente", valRecord.CodigoCliente, 10);
            vParams.AddInString("CodigoAlmacen", valRecord.CodigoAlmacen, 5);
            vParams.AddInDateTime("Fecha", valRecord.Fecha);
            vParams.AddInString("Comentarios", valRecord.Comentarios, 255);
            vParams.AddInString("CodigoLote", valRecord.CodigoLote, 10);
            vParams.AddInEnum("StatusNotaEntradaSalida", valRecord.StatusNotaEntradaSalidaAsDB);
            vParams.AddInInteger("ConsecutivoAlmacen", valRecord.ConsecutivoAlmacen);
            vParams.AddInEnum("GeneradoPor", valRecord.GeneradoPorAsDB);
            vParams.AddInInteger("ConsecutivoDocumentoOrigen", valRecord.ConsecutivoDocumentoOrigen);
            vParams.AddInEnum("TipoNotaProduccion", valRecord.TipoNotaProduccionAsDB);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar || valAction == eAccionSR.Anular) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(NotaDeEntradaSalida valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NumeroDocumento", valRecord.NumeroDocumento, 11);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(NotaDeEntradaSalida valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataMasterComponent<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>>

        LibResponse ILibDataMasterComponent<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>>.CanBeChoosen(IList<NotaDeEntradaSalida> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            NotaDeEntradaSalida vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar || valAction == eAccionSR.Anular || valAction == eAccionSR.Reversar) {
                if (!PuedeSerEliminadaOAnuladaNotaDeESPorLoteFdV(refRecord, valAction, out vErrMsg)) {
                    throw new GalacAlertException(vErrMsg);
                }
                vResult.Success = true;
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Nota de Entrada/Salida.Eliminar")]
        LibResponse ILibDataMasterComponent<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>>.Delete(IList<NotaDeEntradaSalida> refRecord) {
            LibResponse vResult = new LibResponse();
            try {
                string vErrMsg = "";
                CurrentRecord = refRecord[0];
                if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                    if (ExecuteProcessBeforeDelete()) {
                        vResult.Success = insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "NotaDeEntradaSalidaDEL"), ParametrosClave(CurrentRecord, true, true));
                        if (vResult.Success) {
                            ExecuteProcessAfterDelete();
                        }
                        
                    }
                } else {
                    throw new GalacException(vErrMsg, eExceptionManagementType.Validation);
                }
                return vResult;
            } finally {                
            }
        }

        IList<NotaDeEntradaSalida> ILibDataMasterComponent<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters, bool valUseDetail) {
            List<NotaDeEntradaSalida> vResult = new List<NotaDeEntradaSalida>();
            LibDataScope insDb = new LibDataScope();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<NotaDeEntradaSalida>(valProcessMessage, valParameters, CmdTimeOut);
                    if (valUseDetail && vResult != null && vResult.Count > 0) {
                        new clsRenglonNotaESDat().GetDetailAndAppendToMaster(ref vResult);
                    }
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Nota de Entrada/Salida.Insertar")]
        LibResponse ILibDataMasterComponent<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>>.Insert(IList<NotaDeEntradaSalida> refRecord, bool valUseDetail) {
            LibResponse vResult = new LibResponse();
            try {
                CurrentRecord = refRecord[0];                
                if (ExecuteProcessBeforeInsert()) {
                    if (ValidateMasterDetail(eAccionSR.Insertar, CurrentRecord, valUseDetail)) {
                        if (insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "NotaDeEntradaSalidaINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
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
            } finally {              
            }
        }

        XElement ILibDataMasterComponent<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoNumeroDocumento")) {
                        vResult = LibXml.ValueToXElement(insDb.NextStrConsecutive(DbSchema + ".NotaDeEntradaSalida", "NumeroDocumento", valParameters, true, 11), "NumeroDocumento");
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

        LibResponse ILibDataMasterComponent<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>>.SpecializedUpdate(IList<NotaDeEntradaSalida> refRecord,  bool valUseDetail, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Nota de Entrada/Salida.Modificar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Nota de Entrada/Salida.Anular Retiro")]
        //Agregar permisos de los módulos que crean NES: Producción, Compra
        LibResponse ILibDataMasterComponent<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>>.Update(IList<NotaDeEntradaSalida> refRecord, bool valUseDetail, eAccionSR valAction) {
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
            } finally {                
            }
        }

        bool ILibDataMasterComponent<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>>.ValidateAll(IList<NotaDeEntradaSalida> refRecords, bool valUseDetail, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (NotaDeEntradaSalida vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }
        #endregion //ILibDataMasterComponent<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>>

        LibResponse UpdateMaster(NotaDeEntradaSalida refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            vResult.Success = insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "NotaDeEntradaSalidaUPD"), ParametrosActualizacion(refRecord, valAction));
            return vResult;
        }

        LibResponse UpdateMasterAndDetail(NotaDeEntradaSalida refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            string vErrorMessage = "";
            if (ValidateDetail(refRecord, eAccionSR.Modificar,out vErrorMessage)) {
                if (UpdateDetail(refRecord)) {
                    vResult = UpdateMaster(refRecord, valAction);
                }
            }
            return vResult;
        }

        private bool InsertDetail(NotaDeEntradaSalida valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailRenglonNotaESAndUpdateDb(valRecord);
            return vResult;
        }

        private bool SetPkInDetailRenglonNotaESAndUpdateDb(NotaDeEntradaSalida valRecord) {
            bool vResult = false;
            int vConsecutivo = 1;
            clsRenglonNotaESDat insRenglonNotaES = new clsRenglonNotaESDat();
            foreach (RenglonNotaES vDetail in valRecord.DetailRenglonNotaES) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.NumeroDocumento = valRecord.NumeroDocumento;
                vDetail.ConsecutivoRenglon = vConsecutivo;
                vConsecutivo++;
            }
            vResult = insRenglonNotaES.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool UpdateDetail(NotaDeEntradaSalida valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailRenglonNotaESAndUpdateDb(valRecord);
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.NumeroDocumento);
            vResult = IsValidNumeroDocumento(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.NumeroDocumento) && vResult;
            vResult = IsValidCodigoCliente(valAction, CurrentRecord.CodigoCliente) && vResult;
            vResult = IsValidCodigoAlmacen(valAction, CurrentRecord.CodigoAlmacen) && vResult;
            vResult = IsValidFecha(valAction, CurrentRecord.Fecha) && vResult;
            vResult = IsValidConsecutivoAlmacen(valAction, CurrentRecord.ConsecutivoAlmacen) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, string valNumeroDocumento){
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

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo){
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

        //private bool IsValidConsecutivo(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo){
        //    bool vResult = true;
        //    if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
        //        return true;
        //    }
        //    if (valConsecutivo == 0) {
        //        BuildValidationInfo(MsgRequiredField("Consecutivo"));
        //        vResult = false;
        //    } else if (valAction == eAccionSR.Insertar) {
        //        if (KeyExists(valConsecutivoCompania, valConsecutivo)) {
        //            BuildValidationInfo(MsgFieldValueAlreadyExist("Consecutivo", valConsecutivo));
        //            vResult = false;
        //        }
        //    }
        //    return vResult;
        //}

        private bool IsValidNumeroDocumento(eAccionSR valAction, int valConsecutivoCompania, string valNumeroDocumento){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumeroDocumento = LibString.Trim(valNumeroDocumento);
            if (LibString.IsNullOrEmpty(valNumeroDocumento, true)) {
                BuildValidationInfo(MsgRequiredField("Numero Documento"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                NotaDeEntradaSalida vRecBusqueda = new NotaDeEntradaSalida();
                vRecBusqueda.NumeroDocumento = valNumeroDocumento;
                if (KeyExists(valConsecutivoCompania, valNumeroDocumento)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Numero Documento", valNumeroDocumento));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCodigoCliente(eAccionSR valAction, string valCodigoCliente){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoCliente = LibString.Trim(valCodigoCliente);
            if (LibString.IsNullOrEmpty(valCodigoCliente , true)) {
                BuildValidationInfo(MsgRequiredField("Código del Cliente"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cliente", "codigo", insDb.InsSql.ToSqlValue(valCodigoCliente), true)) {
                    BuildValidationInfo("El valor asignado al campo Código del Cliente no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCodigoAlmacen(eAccionSR valAction, string valCodigoAlmacen){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoAlmacen = LibString.Trim(valCodigoAlmacen);
            if (LibString.IsNullOrEmpty(valCodigoAlmacen , true)) {
                BuildValidationInfo(MsgRequiredField("Codigo Almacen"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Saw.Almacen", "Codigo", insDb.InsSql.ToSqlValue(valCodigoAlmacen), true)) {
                    BuildValidationInfo("El valor asignado al campo Codigo Almacen no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidFecha(eAccionSR valAction, DateTime valFecha){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFecha, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidConsecutivoAlmacen(eAccionSR valAction, int valConsecutivoAlmacen){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoAlmacen == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Almacen"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Saw.Almacen", "Consecutivo", insDb.InsSql.ToSqlValue(valConsecutivoAlmacen), true)) {
                    BuildValidationInfo("El valor asignado al campo Consecutivo Almacen no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valNumeroDocumento) {
            bool vResult = false;
            NotaDeEntradaSalida vRecordBusqueda = new NotaDeEntradaSalida();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.NumeroDocumento = valNumeroDocumento;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".NotaDeEntradaSalida", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        //private bool KeyExists(int valConsecutivoCompania, int valConsecutivo) {
        //    bool vResult = false;
        //    NotaDeEntradaSalida vRecordBusqueda = new NotaDeEntradaSalida();
        //    vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
        //    vRecordBusqueda.Consecutivo = valConsecutivo;
        //    LibDatabase insDb = new LibDatabase();
        //    vResult = insDb.ExistsRecord(DbSchema + ".NotaDeEntradaSalida", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
        //    insDb.Dispose();
        //    return vResult;
        //}

        private bool KeyExists(int valConsecutivoCompania, NotaDeEntradaSalida valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            vResult = insDb.ExistsRecord(DbSchema + ".NotaDeEntradaSalida", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool ValidateMasterDetail(eAccionSR valAction, NotaDeEntradaSalida valRecordMaster, bool valUseDetail) {
            bool vResult = false;
            string vErrMsg;
            if (Validate(valAction, out vErrMsg)) {
                if (valUseDetail) {
                    if (ValidateDetail(valRecordMaster, eAccionSR.Insertar, out vErrMsg)) {
                        vResult = true;
                    } else {
                        throw new GalacValidationException("Nota de Entrada/Salida (detalle)\n" + vErrMsg);
                    }
                } else {
                    vResult = true;
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        private bool ValidateDetail(NotaDeEntradaSalida valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            outErrorMessage = "";
            vResult = vResult && ValidateDetailRenglonNotaES(valRecord, valAction, out outErrorMessage);
            return vResult;
        }

        private bool ValidateDetailRenglonNotaES(NotaDeEntradaSalida valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            outErrorMessage = string.Empty;
            foreach (RenglonNotaES vDetail in valRecord.DetailRenglonNotaES) {
                bool vLineHasError = true;
                //agregar validaciones
                if (LibString.IsNullOrEmpty(vDetail.CodigoArticulo)) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Código Inventario.");
                } else if (vDetail.Cantidad <= 0) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado Cantidad.");
                } else if ((vDetail.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeVencimiento || vDetail.TipoArticuloInvAsEnum == eTipoArticuloInv.Lote || vDetail.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeElaboracion) && LibString.IsNullOrEmpty(vDetail.LoteDeInventario)) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Lote de Inventario.");
                } else {
                    vLineHasError = false;
                }
                vResult = vResult && (!vLineHasError);
                vNumeroDeLinea++;
            }
            if (!vResult) {
                outErrorMessage = "Renglon Nota ES"  + Environment.NewLine + vSbErrorInfo.ToString();
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

        private bool PuedeSerEliminadaOAnuladaNotaDeESPorLoteFdV(IList<NotaDeEntradaSalida> refRecord, eAccionSR valAccion, out string outMensaje) {
            bool vResult = true;
            outMensaje = string.Empty;
            foreach (NotaDeEntradaSalida vItemNotaES in refRecord) {
                if (valAccion == eAccionSR.Anular && vItemNotaES.TipodeOperacionAsEnum != eTipodeOperacion.Retiro) {
                    outMensaje = "Solo se pueden Anular Operaciones de Retiro.";
                    return false;
                } else if ((valAccion == eAccionSR.Reversar || valAccion == eAccionSR.Eliminar) && ExistenNotasESDesdeImportacion(vItemNotaES.ConsecutivoCompania, vItemNotaES.CodigoLote)) {
                    StringBuilder vMsg = new StringBuilder();
                    vMsg.AppendLine($"No se puede " + LibEAccionSR.ToString(valAccion) + " la Nota de Entrada/Salida " + vItemNotaES.NumeroDocumento + ", la misma está asociada a un lote de importación.");
                    outMensaje = vMsg.ToString();
                    return false;
                } else if (ExistenComprobantesDeCostoDeVentasPosteriores(vItemNotaES.ConsecutivoCompania, vItemNotaES.Fecha)) {
                    StringBuilder vMsg = new StringBuilder();
                    vMsg.AppendLine("Existe al menos un Comprobante de Costo de Venta posterior a la operación de " + LibEAccionSR.ToString(valAccion) + ".");
                    vMsg.AppendLine();
                    vMsg.AppendLine("Deberá abrir el Período y/o Eliminar el Comprobante para " + LibEAccionSR.ToString(valAccion) + " el documento.");
                    outMensaje = vMsg.ToString();
                }
            }
            return vResult;
        }

        private bool ExistenComprobantesDeCostoDeVentasPosteriores(int valConsecutivoCompania, DateTime valFecha) {
            //duplicado en: IArticuloInventarioPdn.ExistenComprobantesDeCostoDeVentasPosteriores, no se invoca acá por estar en Brl (capa superior)
            bool vResult = false;
            bool vUsaContabilidad = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Compania", "UsaModuloDeContabilidad");
            bool vUsaCostoPromedio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaCostoPromedio");
            if (vUsaCostoPromedio && vUsaContabilidad) {
                if (new clsLibSaw().EsValidaLaFechaParaContabilidad(valConsecutivoCompania, valFecha)) {
                    LibDatabase insDb = new LibDatabase();
                    StringBuilder vSql = new StringBuilder();
                    vSql.AppendLine("SELECT COMPROBANTE.Numero FROM PERIODO INNER JOIN  COMPROBANTE ON periodo.ConsecutivoPeriodo = COMPROBANTE.ConsecutivoPeriodo ");
                    vSql.AppendLine("WHERE PERIODO.ConsecutivoCompania = " + insDb.InsSql.ToSqlValue(valConsecutivoCompania));
                    vSql.AppendLine("AND COMPROBANTE.GeneradoPor = " + insDb.InsSql.EnumToSqlValue((int)eComprobanteGeneradoPorVBSaw.eCG_INVENTARIO));
                    vSql.AppendLine("AND COMPROBANTE.Fecha >= " + insDb.InsSql.ToSqlValue(valFecha));
                    vResult = insDb.RecordCountOfSql(vSql.ToString()) > 0;
                }
            }
            return vResult;
        }

        private bool ExistenNotasESDesdeImportacion(int valConsecutivoCompania, string valCodigoLoteAdm) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase();
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT CodigoLote ");
            vSql.AppendLine("FROM LoteAdm");
            vSql.AppendLine("WHERE ConsecutivoCompania = " + insDb.InsSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND CodigoLote =" + insDb.InsSql.ToSqlValue(valCodigoLoteAdm));
            vResult = insDb.RecordCountOfSql(vSql.ToString()) > 0;
            return vResult;           
        }

        private bool HayAlMenosUnArtLoteFdV(NotaDeEntradaSalida valItemNotaES) {
            bool vResult;
            LibDatabase insDb = new LibDatabase();
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT ConsecutivoRenglon FROM RenglonNotaES INNER JOIN ArticuloInventario ");
            vSql.AppendLine("ON RenglonNotaES.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania ");
            vSql.AppendLine("AND RenglonNotaES.CodigoArticulo = ArticuloInventario.Codigo ");
            vSql.AppendLine("WHERE RenglonNotaES.ConsecutivoCompania = "+ insDb.InsSql.ToSqlValue(valItemNotaES.ConsecutivoCompania));
            vSql.AppendLine("AND RenglonNotaES.NumeroDocumento = " + insDb.InsSql.ToSqlValue(valItemNotaES.NumeroDocumento));
            vSql.AppendLine("AND ArticuloInventario.TipoDeArticulo = '0'");
            vSql.AppendLine("AND (ArticuloInventario.TipoArticuloInv = '5' OR ArticuloInventario.TipoArticuloInv = '6')");
            vResult = insDb.RecordCountOfSql(vSql.ToString()) > 0;
            return vResult;
        }

    } //End of class clsNotaDeEntradaSalidaDat
} //End of namespace Galac.Saw.Dal.Inventario