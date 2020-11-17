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
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Dal.Banco {
    public class clsSolicitudesDePagoDat: LibData, ILibDataMasterComponentWithSearch<IList<SolicitudesDePago>, IList<SolicitudesDePago>> {
        #region Variables
        LibTrn insTrn;
        SolicitudesDePago _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private SolicitudesDePago CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsSolicitudesDePagoDat() {
            DbSchema = "Saw";
            insTrn = new LibTrn(LibCkn.ConfigKeyForDbService);
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(SolicitudesDePago valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoSolicitud", valRecord.ConsecutivoSolicitud);
            vParams.AddInInteger("NumeroDocumentoOrigen", valRecord.NumeroDocumentoOrigen);
            vParams.AddInDateTime("FechaSolicitud", valRecord.FechaSolicitud);
            vParams.AddInEnum("Status", valRecord.StatusAsDB);
            vParams.AddInEnum("GeneradoPor", valRecord.GeneradoPorAsDB);
            vParams.AddInString("Observaciones", valRecord.Observaciones, 40);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(SolicitudesDePago valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoSolicitud", valRecord.ConsecutivoSolicitud);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(SolicitudesDePago valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>>

        LibResponse ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>>.CanBeChoosen(IList<SolicitudesDePago> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            SolicitudesDePago vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase(LibCkn.ConfigKeyForDbService);
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Solicitudes De Pago.Eliminar")]
        LibResponse ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>>.Delete(IList<SolicitudesDePago> refRecord) {
            LibResponse vResult = new LibResponse();
            try {
                string vErrMsg = "";
                CurrentRecord = refRecord[0];
                if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                    if (ExecuteProcessBeforeDelete()) {
                        insTrn.StartTransaction();
                        vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "SolicitudesDePagoDEL"), ParametrosClave(CurrentRecord, true, true));
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

        IList<SolicitudesDePago> ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters, bool valUseDetail) {
            List<SolicitudesDePago> vResult = new List<SolicitudesDePago>();
            LibDatabase insDb = new LibDatabase(LibCkn.ConfigKeyForDbService);
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<SolicitudesDePago>(valProcessMessage, valParameters, CmdTimeOut);
                    if (valUseDetail && vResult != null && vResult.Count > 0) {
                        new clsRenglonSolicitudesDePagoDat().GetDetailAndAppendToMaster(ref vResult);
                    }
                    break;
                default: break;
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Solicitudes De Pago.Insertar")]
        LibResponse ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>>.Insert(IList<SolicitudesDePago> refRecord, bool valUseDetail) {
            LibResponse vResult = new LibResponse();
            try {
                CurrentRecord = refRecord[0];
                insTrn.StartTransaction();
                if (ExecuteProcessBeforeInsert()) {
                    if (ValidateMasterDetail(eAccionSR.Insertar, CurrentRecord, valUseDetail)) {
                        if (insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "SolicitudesDePagoINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
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

        XElement ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase(LibCkn.ConfigKeyForDbService);
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoConsecutivoSolicitud")) {
                        vResult = LibXml.ValueToXElement(insDb.NextLngConsecutive("Saw.SolicitudesDePago", "ConsecutivoSolicitud", valParameters), "ConsecutivoSolicitud");
                    }
                    break;
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                default: break;
            }
            insDb.Dispose();
            return vResult;
        }

        LibResponse ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>>.SpecializedUpdate(IList<SolicitudesDePago> refRecord,  bool valUseDetail, string valSpecializedAction) {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Solicitudes De Pago.Modificar")]
        LibResponse ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>>.Update(IList<SolicitudesDePago> refRecord, bool valUseDetail, eAccionSR valAction) {
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

        bool ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>>.ValidateAll(IList<SolicitudesDePago> refRecords, bool valUseDetail, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (SolicitudesDePago vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }
        #endregion //ILibDataMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>>

        LibResponse UpdateMaster(SolicitudesDePago refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "SolicitudesDePagoUPD"), ParametrosActualizacion(refRecord, valAction));
            return vResult;
        }

        LibResponse UpdateMasterAndDetail(SolicitudesDePago refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            string vErrorMessage = "";
            if (ValidateDetail(refRecord, eAccionSR.Modificar,out vErrorMessage)) {
                if (UpdateDetail(refRecord)) {
                    vResult = UpdateMaster(refRecord, valAction);
                }
            }
            return vResult;
        }

        private bool InsertDetail(SolicitudesDePago valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailRenglonSolicitudesDePagoAndUpdateDb(valRecord);
            return vResult;
        }

        private bool SetPkInDetailRenglonSolicitudesDePagoAndUpdateDb(SolicitudesDePago valRecord) {
            bool vResult = false;
            int vConsecutivo = 1;
            clsRenglonSolicitudesDePagoDat insRenglonSolicitudesDePago = new clsRenglonSolicitudesDePagoDat();
            foreach (RenglonSolicitudesDePago vDetail in valRecord.DetailRenglonSolicitudesDePago) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.ConsecutivoSolicitud = valRecord.ConsecutivoSolicitud;
                vDetail.consecutivoRenglon = vConsecutivo;
                vConsecutivo++;

            }
            vResult = insRenglonSolicitudesDePago.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool UpdateDetail(SolicitudesDePago valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailRenglonSolicitudesDePagoAndUpdateDb(valRecord);
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.ConsecutivoSolicitud);
            vResult = IsValidConsecutivoSolicitud(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.ConsecutivoSolicitud) && vResult;
            vResult = IsValidNumeroDocumentoOrigen(valAction,CurrentRecord.ConsecutivoCompania, CurrentRecord.NumeroDocumentoOrigen) && vResult;
            vResult = IsValidFechaSolicitud(valAction, CurrentRecord.FechaSolicitud) && vResult;
            vResult = IsValidStatus(valAction, CurrentRecord.StatusAsEnum) && vResult;
            vResult = IsValidGeneradoPor(valAction, CurrentRecord.GeneradoPorAsEnum) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivoSolicitud){
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

        private bool IsValidConsecutivoSolicitud(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivoSolicitud){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoSolicitud == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Solicitud"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valConsecutivoSolicitud)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Consecutivo Solicitud", valConsecutivoSolicitud));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidNumeroDocumentoOrigen(eAccionSR valAction,int valConsecutivoCompania,int valNumeroDocumentoOrigen){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valNumeroDocumentoOrigen == 0) {
                BuildValidationInfo(MsgRequiredField("Numero Documento Origen"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (ExistsSolicitud(valConsecutivoCompania, valNumeroDocumentoOrigen)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Numero Documento Origen", valNumeroDocumentoOrigen));
                    vResult = false;
                }

            }
            return vResult;
        }

        private bool IsValidFechaSolicitud(eAccionSR valAction, DateTime valFechaSolicitud){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaSolicitud, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidStatus(eAccionSR valAction, eStatusSolicitud valStatus){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool IsValidGeneradoPor(eAccionSR valAction, eSolicitudGeneradaPor valGeneradoPor){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivoSolicitud) {
            bool vResult = false;
            SolicitudesDePago vRecordBusqueda = new SolicitudesDePago();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.ConsecutivoSolicitud = valConsecutivoSolicitud;
            LibDatabase insDb = new LibDatabase(LibCkn.ConfigKeyForDbService);
            vResult = insDb.ExistsRecord("Saw.SolicitudesDePago", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool ValidateMasterDetail(eAccionSR valAction, SolicitudesDePago valRecordMaster, bool valUseDetail) {
            bool vResult = false;
            string vErrMsg;
            if (Validate(valAction, out vErrMsg)) {
                if (valUseDetail) {
                    if (ValidateDetail(valRecordMaster, eAccionSR.Insertar, out vErrMsg)) {
                        vResult = true;
                    } else {
                        throw new GalacValidationException("Solicitudes De Pago (detalle)\n" + vErrMsg);
                    }
                } else {
                    vResult = true;
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        private bool ValidateDetail(SolicitudesDePago valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            outErrorMessage = "";
            vResult = vResult && ValidateDetailRenglonSolicitudesDePago(valRecord, valAction, out outErrorMessage);
            return vResult;
        }

        private bool ValidateDetailRenglonSolicitudesDePago(SolicitudesDePago valRecord, eAccionSR eAccionSR, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            foreach (RenglonSolicitudesDePago vDetail in valRecord.DetailRenglonSolicitudesDePago) {
                bool vLineHasError = true;
                //agregar validaciones
                if (LibString.IsNullOrEmpty(vDetail.CuentaBancaria)) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignada la Cuenta Bancaria.");
                } else if (vDetail.ConsecutivoBeneficiario == 0) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Consecutivo Beneficiario.");
                } else {
                    vLineHasError = false;
                }
                vResult = vResult && (!vLineHasError);
                vNumeroDeLinea++;
            }
            outErrorMessage = vSbErrorInfo.ToString();
            return vResult;
        }
        #endregion //Validaciones
        #region Miembros de ILibDataFKSearch
        bool ILibDataFKSearch.ConnectFk(ref XmlDocument refResulset, eProcessMessageType valType, string valProcessMessage, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase(LibCkn.ConfigKeyForDbService);
            refResulset = insDb.LoadForConnect(valProcessMessage, valXmlParamsExpression, CmdTimeOut);
            if (refResulset != null && refResulset.DocumentElement != null && refResulset.DocumentElement.HasChildNodes) {
                vResult = true;
            }
            return vResult;
        }
        #endregion //Miembros de ILibDataFKSearch
        #endregion //Metodos Generados

        private StringBuilder ParametrosBusqueda(SolicitudesDePago valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("NumeroDocumentoOrigen", valRecord.NumeroDocumentoOrigen);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private bool ExistsSolicitud(int valConsecutivoCompania, int valDocumentoOrigen) {
            bool vResult = false;
            SolicitudesDePago vRecordBusqueda = new SolicitudesDePago();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.NumeroDocumentoOrigen = valDocumentoOrigen;
            LibDatabase insDb = new LibDatabase(LibCkn.ConfigKeyForDbService);
            vResult = insDb.ExistsRecord("Saw.SolicitudesDePago", "NumeroDocumentoOrigen", ParametrosBusqueda(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
    } //End of class clsSolicitudesDePagoDat

} //End of namespace Galac.Adm.Dal.Banco

