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
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Dal.Venta {
    public class clsCobroDeFacturaRapidaDepositoTransfDat: LibData, ILibDataMasterComponentWithSearch<IList<CobroDeFacturaRapidaDepositoTransf>, IList<CobroDeFacturaRapidaDepositoTransf>> {
        #region Variables
        LibTrn insTrn;
        CobroDeFacturaRapidaDepositoTransf _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private CobroDeFacturaRapidaDepositoTransf CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCobroDeFacturaRapidaDepositoTransfDat() {
            DbSchema = "Adm";
            insTrn = new LibTrn();
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(CobroDeFacturaRapidaDepositoTransf valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", valRecord.NumeroFactura, 11);
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(CobroDeFacturaRapidaDepositoTransf valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", valRecord.NumeroFactura, 11);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(CobroDeFacturaRapidaDepositoTransf valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataMasterComponent<IList<CobroDeFacturaRapidaDepositoTransf>, IList<CobroDeFacturaRapidaDepositoTransf>>

        LibResponse ILibDataMasterComponent<IList<CobroDeFacturaRapidaDepositoTransf>, IList<CobroDeFacturaRapidaDepositoTransf>>.CanBeChoosen(IList<CobroDeFacturaRapidaDepositoTransf> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            CobroDeFacturaRapidaDepositoTransf vRecord = refRecord[0];
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Cobro Tarjeta de Deposito Transferencia.Eliminar")]
        LibResponse ILibDataMasterComponent<IList<CobroDeFacturaRapidaDepositoTransf>, IList<CobroDeFacturaRapidaDepositoTransf>>.Delete(IList<CobroDeFacturaRapidaDepositoTransf> refRecord) {
            LibResponse vResult = new LibResponse();
            try {
                string vErrMsg = "";
                CurrentRecord = refRecord[0];
                if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                    if (ExecuteProcessBeforeDelete()) {
                        insTrn.StartTransaction();
                        vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "CobroDeFacturaRapidaDepositoTransfDEL"), ParametrosClave(CurrentRecord, true, true));
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

        IList<CobroDeFacturaRapidaDepositoTransf> ILibDataMasterComponent<IList<CobroDeFacturaRapidaDepositoTransf>, IList<CobroDeFacturaRapidaDepositoTransf>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters, bool valUseDetail) {
            List<CobroDeFacturaRapidaDepositoTransf> vResult = new List<CobroDeFacturaRapidaDepositoTransf>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<CobroDeFacturaRapidaDepositoTransf>(valProcessMessage, valParameters, CmdTimeOut);
                    if (valUseDetail && vResult != null && vResult.Count > 0) {
                        new clsCobroDeFacturaRapidaDepositoTransfDetalleDat().GetDetailAndAppendToMaster(ref vResult);
                    }
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cobro Tarjeta de Deposito Transferencia.Insertar")]
        LibResponse ILibDataMasterComponent<IList<CobroDeFacturaRapidaDepositoTransf>, IList<CobroDeFacturaRapidaDepositoTransf>>.Insert(IList<CobroDeFacturaRapidaDepositoTransf> refRecord, bool valUseDetail) {
            LibResponse vResult = new LibResponse();
            try {
                CurrentRecord = refRecord[0];
                insTrn.StartTransaction();
                if (ExecuteProcessBeforeInsert()) {
                    if (ValidateMasterDetail(eAccionSR.Insertar, CurrentRecord, valUseDetail)) {
                        if (insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "CobroDeFacturaRapidaDepositoTransfINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
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

        XElement ILibDataMasterComponent<IList<CobroDeFacturaRapidaDepositoTransf>, IList<CobroDeFacturaRapidaDepositoTransf>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
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

        LibResponse ILibDataMasterComponent<IList<CobroDeFacturaRapidaDepositoTransf>, IList<CobroDeFacturaRapidaDepositoTransf>>.SpecializedUpdate(IList<CobroDeFacturaRapidaDepositoTransf> refRecord,  bool valUseDetail, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cobro Tarjeta de Deposito Transferencia.Modificar")]
        LibResponse ILibDataMasterComponent<IList<CobroDeFacturaRapidaDepositoTransf>, IList<CobroDeFacturaRapidaDepositoTransf>>.Update(IList<CobroDeFacturaRapidaDepositoTransf> refRecord, bool valUseDetail, eAccionSR valAction) {
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

        bool ILibDataMasterComponent<IList<CobroDeFacturaRapidaDepositoTransf>, IList<CobroDeFacturaRapidaDepositoTransf>>.ValidateAll(IList<CobroDeFacturaRapidaDepositoTransf> refRecords, bool valUseDetail, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (CobroDeFacturaRapidaDepositoTransf vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }
        #endregion //ILibDataMasterComponent<IList<CobroDeFacturaRapidaDepositoTransf>, IList<CobroDeFacturaRapidaDepositoTransf>>

        LibResponse UpdateMaster(CobroDeFacturaRapidaDepositoTransf refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "CobroDeFacturaRapidaDepositoTransfUPD"), ParametrosActualizacion(refRecord, valAction));
            return vResult;
        }

        LibResponse UpdateMasterAndDetail(CobroDeFacturaRapidaDepositoTransf refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            string vErrorMessage = "";
            if (ValidateDetail(refRecord, eAccionSR.Modificar,out vErrorMessage)) {
                if (UpdateDetail(refRecord)) {
                    vResult = UpdateMaster(refRecord, valAction);
                }
            }
            return vResult;
        }

        private bool InsertDetail(CobroDeFacturaRapidaDepositoTransf valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailCobroDeFacturaRapidaDepositoTransfDetalleAndUpdateDb(valRecord);
            return vResult;
        }

        private bool SetPkInDetailCobroDeFacturaRapidaDepositoTransfDetalleAndUpdateDb(CobroDeFacturaRapidaDepositoTransf valRecord) {
            bool vResult = false;
            int vConsecutivo = 1;
            clsCobroDeFacturaRapidaDepositoTransfDetalleDat insCobroDeFacturaRapidaDepositoTransfDetalle = new clsCobroDeFacturaRapidaDepositoTransfDetalleDat();
            foreach (CobroDeFacturaRapidaDepositoTransfDetalle vDetail in valRecord.DetailCobroDeFacturaRapidaDepositoTransfDetalle) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.CodigoFormaDelCobro = valRecord.NumeroFactura;
                vConsecutivo++;
            }
            vResult = insCobroDeFacturaRapidaDepositoTransfDetalle.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool UpdateDetail(CobroDeFacturaRapidaDepositoTransf valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailCobroDeFacturaRapidaDepositoTransfDetalleAndUpdateDb(valRecord);
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.NumeroFactura);
            vResult = IsValidNumeroFactura(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.NumeroFactura) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, string valNumeroFactura){
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

        private bool IsValidNumeroFactura(eAccionSR valAction, int valConsecutivoCompania, string valNumeroFactura){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumeroFactura = LibString.Trim(valNumeroFactura);
            if (LibString.IsNullOrEmpty(valNumeroFactura, true)) {
                BuildValidationInfo(MsgRequiredField("Numero Factura"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valNumeroFactura)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Numero Factura", valNumeroFactura));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valNumeroFactura) {
            bool vResult = false;
            CobroDeFacturaRapidaDepositoTransf vRecordBusqueda = new CobroDeFacturaRapidaDepositoTransf();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.NumeroFactura = valNumeroFactura;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".CobroDeFacturaRapidaDepositoTransf", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool ValidateMasterDetail(eAccionSR valAction, CobroDeFacturaRapidaDepositoTransf valRecordMaster, bool valUseDetail) {
            bool vResult = false;
            string vErrMsg;
            if (Validate(valAction, out vErrMsg)) {
                if (valUseDetail) {
                    if (ValidateDetail(valRecordMaster, eAccionSR.Insertar, out vErrMsg)) {
                        vResult = true;
                    } else {
                        throw new GalacValidationException("Cobro Tarjeta de Deposito Transferencia (detalle)\n" + vErrMsg);
                    }
                } else {
                    vResult = true;
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        private bool ValidateDetail(CobroDeFacturaRapidaDepositoTransf valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            outErrorMessage = "";
            vResult = vResult && ValidateDetailCobroDeFacturaRapidaDepositoTransfDetalle(valRecord, valAction, out outErrorMessage);
            return vResult;
        }

        private bool ValidateDetailCobroDeFacturaRapidaDepositoTransfDetalle(CobroDeFacturaRapidaDepositoTransf valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            foreach (CobroDeFacturaRapidaDepositoTransfDetalle vDetail in valRecord.DetailCobroDeFacturaRapidaDepositoTransfDetalle) {
                bool vLineHasError = true;
                //agregar validaciones
                if (LibString.IsNullOrEmpty(vDetail.CodigoFormaDelCobro)) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Codigo Forma Del Cobro.");
                } else if (vDetail.CodigoBanco == 0) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Código del Banco.");
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
            LibDatabase insDb = new LibDatabase();
            refResulset = insDb.LoadForConnect(valProcessMessage, valXmlParamsExpression, CmdTimeOut);
            if (refResulset != null && refResulset.DocumentElement != null && refResulset.DocumentElement.HasChildNodes) {
                vResult = true;
            }
            return vResult;
        }
        #endregion //Miembros de ILibDataFKSearch
        #endregion //Metodos Generados


    } //End of class clsCobroDeFacturaRapidaDepositoTransfDat

} //End of namespace Galac.Adm.Dal.Venta

