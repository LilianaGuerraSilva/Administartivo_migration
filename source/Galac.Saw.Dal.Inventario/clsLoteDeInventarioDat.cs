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

namespace Galac.Saw.Dal.Inventario {
    public class clsLoteDeInventarioDat: LibData, ILibDataMasterComponentWithSearch<IList<LoteDeInventario>, IList<LoteDeInventario>>, ILibDataRpt {
        #region Variables
        LibTrn insTrn;
        LoteDeInventario _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private LoteDeInventario CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores
        public clsLoteDeInventarioDat() {
            DbSchema = "Saw";
            insTrn = new LibTrn();
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(LoteDeInventario valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("CodigoLote", valRecord.CodigoLote, 30);
            vParams.AddInString("CodigoArticulo", valRecord.CodigoArticulo, 30);
            vParams.AddInDateTime("FechaDeElaboracion", valRecord.FechaDeElaboracion);
            vParams.AddInDateTime("FechaDeVencimiento", valRecord.FechaDeVencimiento);
            vParams.AddInDecimal("Existencia", valRecord.Existencia, 2);
            vParams.AddInEnum("StatusLoteInv", valRecord.StatusLoteInvAsDB);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(LoteDeInventario valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

        private StringBuilder ParametrosClaveCodigoCodigoArticulo(LoteDeInventario valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoLote", valRecord.CodigoLote, 30);
            vParams.AddInString("CodigoArticulo", valRecord.CodigoArticulo, 30);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(LoteDeInventario valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataMasterComponent<IList<LoteDeInventario>, IList<LoteDeInventario>>

        LibResponse ILibDataMasterComponent<IList<LoteDeInventario>, IList<LoteDeInventario>>.CanBeChoosen(IList<LoteDeInventario> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            LoteDeInventario vRecord = refRecord[0];
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

        LibResponse ILibDataMasterComponent<IList<LoteDeInventario>, IList<LoteDeInventario>>.Delete(IList<LoteDeInventario> refRecord) {
            LibResponse vResult = new LibResponse();
            try {
                string vErrMsg = "";
                CurrentRecord = refRecord[0];
                if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                    if (ExecuteProcessBeforeDelete()) {
                        insTrn.StartTransaction();
                        vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "LoteDeInventarioDEL"), ParametrosClave(CurrentRecord, true, true));
                        if (vResult.Success) {
                            ExecuteProcessAfterDelete();
                        }
                        insTrn.CommitTransaction();
                    }
                } else {
                    throw new GalacException(vErrMsg, eExceptionManagementType.Validation);
                }
                return vResult;
            } finally {
                if (!vResult.Success) {
                    insTrn.RollBackTransaction();
                }
            }
        }

        IList<LoteDeInventario> ILibDataMasterComponent<IList<LoteDeInventario>, IList<LoteDeInventario>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters, bool valUseDetail) {
            List<LoteDeInventario> vResult = new List<LoteDeInventario>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<LoteDeInventario>(valProcessMessage, valParameters, CmdTimeOut);
                    if (valUseDetail && vResult != null && vResult.Count > 0) {
                        new clsLoteDeInventarioMovimientoDat().GetDetailAndAppendToMaster(ref vResult);
                    }
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        LibResponse ILibDataMasterComponent<IList<LoteDeInventario>, IList<LoteDeInventario>>.Insert(IList<LoteDeInventario> refRecord, bool valUseDetail) {
            LibResponse vResult = new LibResponse();
            try {
                CurrentRecord = refRecord[0];
                insTrn.StartTransaction();
                if (ExecuteProcessBeforeInsert()) {
                    if (ValidateMasterDetail(eAccionSR.Insertar, CurrentRecord, valUseDetail)) {
                        if (insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "LoteDeInventarioINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
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
                insTrn.CommitTransaction();
                return vResult;
            } finally {
                if (!vResult.Success) {
                    insTrn.RollBackTransaction();
                }
            }
        }

        XElement ILibDataMasterComponent<IList<LoteDeInventario>, IList<LoteDeInventario>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoCodigoLote")) {
                        vResult = LibXml.ValueToXElement(insDb.NextStrConsecutive(DbSchema + ".LoteDeInventario", "CodigoLote", valParameters, true, 30), "CodigoLote");
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

        LibResponse ILibDataMasterComponent<IList<LoteDeInventario>, IList<LoteDeInventario>>.SpecializedUpdate(IList<LoteDeInventario> refRecord,  bool valUseDetail, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }

        LibResponse ILibDataMasterComponent<IList<LoteDeInventario>, IList<LoteDeInventario>>.Update(IList<LoteDeInventario> refRecord, bool valUseDetail, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            try {
                CurrentRecord = refRecord[0];
                if (ValidateMasterDetail(valAction, CurrentRecord, valUseDetail)) {
                    insTrn.StartTransaction();
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
                    insTrn.CommitTransaction();
                }
                return vResult;
            } finally {
                if (!vResult.Success) {
                    insTrn.RollBackTransaction();
                }
            }
        }

        bool ILibDataMasterComponent<IList<LoteDeInventario>, IList<LoteDeInventario>>.ValidateAll(IList<LoteDeInventario> refRecords, bool valUseDetail, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (LoteDeInventario vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }
        #endregion //ILibDataMasterComponent<IList<LoteDeInventario>, IList<LoteDeInventario>>

        LibResponse UpdateMaster(LoteDeInventario refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "LoteDeInventarioUPD"), ParametrosActualizacion(refRecord, valAction));
            return vResult;
        }

        LibResponse UpdateMasterAndDetail(LoteDeInventario refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            string vErrorMessage = "";
            if (ValidateDetail(refRecord, eAccionSR.Modificar,out vErrorMessage)) {
                if (UpdateDetail(refRecord)) {
                    vResult = UpdateMaster(refRecord, valAction);
                }
            }
            return vResult;
        }

        private bool InsertDetail(LoteDeInventario valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailLoteDeInventarioMovimientoAndUpdateDb(valRecord);
            return vResult;
        }

        private bool SetPkInDetailLoteDeInventarioMovimientoAndUpdateDb(LoteDeInventario valRecord) {
            bool vResult = false;
            int vConsecutivo = 1;
            clsLoteDeInventarioMovimientoDat insLoteDeInventarioMovimiento = new clsLoteDeInventarioMovimientoDat();
            foreach (LoteDeInventarioMovimiento vDetail in valRecord.DetailLoteDeInventarioMovimiento) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.ConsecutivoLote = valRecord.Consecutivo;
                vDetail.Consecutivo = vConsecutivo;
                vConsecutivo++;
            }
            vResult = insLoteDeInventarioMovimiento.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool UpdateDetail(LoteDeInventario valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailLoteDeInventarioMovimientoAndUpdateDb(valRecord);
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania);
            vResult = IsValidConsecutivo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo) && vResult;
            vResult = IsValidCodigoLote(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.CodigoLote, CurrentRecord.CodigoArticulo) && vResult;
            vResult = IsValidCodigoArticulo(valAction, CurrentRecord.CodigoArticulo) && vResult;
            vResult = IsValidFechaDeElaboracion(valAction, CurrentRecord.FechaDeElaboracion, CurrentRecord.FechaDeVencimiento) && vResult;
            vResult = IsValidFechaDeVencimiento(valAction, CurrentRecord.FechaDeVencimiento, CurrentRecord.FechaDeElaboracion) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania) {
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

        private bool IsValidConsecutivo(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivo == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valConsecutivo)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Consecutivo", valConsecutivo));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCodigoLote(eAccionSR valAction, int valConsecutivoCompania, string valCodigoLote, string valCodigoArticulo) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoLote = LibString.Trim(valCodigoLote);
            if (LibString.IsNullOrEmpty(valCodigoLote, true)) {
                BuildValidationInfo(MsgRequiredField("Código"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valCodigoLote, valCodigoArticulo)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Código", valCodigoLote));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCodigoArticulo(eAccionSR valAction, string valCodigoArticulo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoArticulo = LibString.Trim(valCodigoArticulo);
            if (LibString.IsNullOrEmpty(valCodigoArticulo, true)) {
                BuildValidationInfo(MsgRequiredField("Código de Artículo"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaDeElaboracion(eAccionSR valAction, DateTime valFechaDeElaboracion, DateTime valFechaDeVencimiento) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (TipoArticuloInvEsLoteFecha(CurrentRecord.ConsecutivoCompania, CurrentRecord.CodigoArticulo)) {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaDeElaboracion, false, valAction)) {
                    BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                    vResult = false;
                }
                if (LibDate.F1IsGreaterThanF2(valFechaDeElaboracion, valFechaDeVencimiento)) {
                    BuildValidationInfo("La Fecha de Elaboración debe ser menor o igual a la Fecha de Vencimiento.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidFechaDeVencimiento(eAccionSR valAction, DateTime valFechaDeVencimiento, DateTime valFechaDeElaboracion) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (TipoArticuloInvEsLoteFecha(CurrentRecord.ConsecutivoCompania, CurrentRecord.CodigoArticulo)) {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaDeVencimiento, false, valAction)) {
                    BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                    vResult = false;
                }
                if (LibDate.F1IsGreaterThanF2(valFechaDeElaboracion, valFechaDeVencimiento)) {
                    BuildValidationInfo("La Fecha de Elaboración debe ser menor o igual a la Fecha de Vencimiento.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool TipoArticuloInvEsLoteFecha(int valConsecutivoCompania, string valCodigoArticulo) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase();
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT Codigo FROM ArticuloInventario ");
            vSql.AppendLine("WHERE ConsecutivoCompania = " + insDb.InsSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine(" AND Codigo = " + insDb.InsSql.ToSqlValue(valCodigoArticulo));
            vSql.AppendLine(" AND TipoArticuloInv = " + insDb.InsSql.EnumToSqlValue((int)eTipoArticuloInv.LoteFechadeVencimiento));
            vResult = insDb.RecordCountOfSql(vSql.ToString()) > 0;
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivo) {
            bool vResult = false;
            LoteDeInventario vRecordBusqueda = new LoteDeInventario();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".LoteDeInventario", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valCodigoLote, string valCodigoArticulo) {
            bool vResult = false;
            LoteDeInventario vRecordBusqueda = new LoteDeInventario();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.CodigoLote = valCodigoLote;
            vRecordBusqueda.CodigoArticulo = valCodigoArticulo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".LoteDeInventario", "ConsecutivoCompania", ParametrosClaveCodigoCodigoArticulo(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, LoteDeInventario valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            vResult = insDb.ExistsRecord(DbSchema + ".LoteDeInventario", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool ValidateMasterDetail(eAccionSR valAction, LoteDeInventario valRecordMaster, bool valUseDetail) {
            bool vResult = false;
            string vErrMsg;
            if (Validate(valAction, out vErrMsg)) {
                if (valUseDetail) {
                    if (ValidateDetail(valRecordMaster, eAccionSR.Insertar, out vErrMsg)) {
                        vResult = true;
                    } else {
                        throw new GalacValidationException("Lote de Inventario (detalle)\n" + vErrMsg);
                    }
                } else {
                    vResult = true;
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        private bool ValidateDetail(LoteDeInventario valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            outErrorMessage = "";
            if (valRecord.DetailLoteDeInventarioMovimiento.Count > 0) {
                vResult = vResult && ValidateDetailLoteDeInventarioMovimiento(valRecord, valAction, out outErrorMessage);
            }
            return vResult;
        }

        private bool ValidateDetailLoteDeInventarioMovimiento(LoteDeInventario valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            outErrorMessage = string.Empty;
            //foreach (LoteDeInventarioMovimiento vDetail in valRecord.DetailLoteDeInventarioMovimiento) {
            //    bool vLineHasError = true;
            //    //agregar validaciones
            //    vResult = vResult && (!vLineHasError);
            //    vNumeroDeLinea++;
            //}
            if (!vResult) {
                outErrorMessage = "Lote De Inventario Movimiento"  + Environment.NewLine + vSbErrorInfo.ToString();
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


    } //End of class clsLoteDeInventarioDat

} //End of namespace Galac.Saw.Dal.Inventario

