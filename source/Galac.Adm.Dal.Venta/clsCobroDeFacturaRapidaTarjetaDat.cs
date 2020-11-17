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
    public class clsCobroDeFacturaRapidaTarjetaDat: LibData, ILibDataMasterComponentWithSearch<IList<CobroDeFacturaRapidaTarjeta>, IList<CobroDeFacturaRapidaTarjeta>> {
        #region Variables
        LibTrn insTrn;
        CobroDeFacturaRapidaTarjeta _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private CobroDeFacturaRapidaTarjeta CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCobroDeFacturaRapidaTarjetaDat() {
            DbSchema = "Adm";
            insTrn = new LibTrn();
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(CobroDeFacturaRapidaTarjeta valRecord, eAccionSR valAction) {
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

        private StringBuilder ParametrosClave(CobroDeFacturaRapidaTarjeta valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

        private StringBuilder ParametrosProximoConsecutivo(CobroDeFacturaRapidaTarjeta valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataMasterComponent<IList<CobroDeFacturaRapidaTarjeta>, IList<CobroDeFacturaRapidaTarjeta>>

        LibResponse ILibDataMasterComponent<IList<CobroDeFacturaRapidaTarjeta>, IList<CobroDeFacturaRapidaTarjeta>>.CanBeChoosen(IList<CobroDeFacturaRapidaTarjeta> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            CobroDeFacturaRapidaTarjeta vRecord = refRecord[0];
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Cobro Tarjeta de Factura.Eliminar")]
        LibResponse ILibDataMasterComponent<IList<CobroDeFacturaRapidaTarjeta>, IList<CobroDeFacturaRapidaTarjeta>>.Delete(IList<CobroDeFacturaRapidaTarjeta> refRecord) {
            LibResponse vResult = new LibResponse();
            try {
                string vErrMsg = "";
                CurrentRecord = refRecord[0];
                if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                    if (ExecuteProcessBeforeDelete()) {
                        insTrn.StartTransaction();
                        vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "CobroDeFacturaRapidaTarjetaDEL"), ParametrosClave(CurrentRecord, true, true));
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

        IList<CobroDeFacturaRapidaTarjeta> ILibDataMasterComponent<IList<CobroDeFacturaRapidaTarjeta>, IList<CobroDeFacturaRapidaTarjeta>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters, bool valUseDetail) {
            List<CobroDeFacturaRapidaTarjeta> vResult = new List<CobroDeFacturaRapidaTarjeta>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<CobroDeFacturaRapidaTarjeta>(valProcessMessage, valParameters, CmdTimeOut);
                    if (valUseDetail && vResult != null && vResult.Count > 0) {
                        new clsCobroDeFacturaRapidaTarjetaDetalleDat().GetDetailAndAppendToMaster(ref vResult);
                    }
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cobro Tarjeta de Factura.Insertar")]
        LibResponse ILibDataMasterComponent<IList<CobroDeFacturaRapidaTarjeta>, IList<CobroDeFacturaRapidaTarjeta>>.Insert(IList<CobroDeFacturaRapidaTarjeta> refRecord, bool valUseDetail) {
            LibResponse vResult = new LibResponse();
            try {
                CurrentRecord = refRecord[0];
                insTrn.StartTransaction();
                if (ExecuteProcessBeforeInsert()) {
                    if (ValidateMasterDetail(eAccionSR.Insertar, CurrentRecord, valUseDetail)) {
                        if (insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "CobroDeFacturaRapidaTarjetaINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
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

        XElement ILibDataMasterComponent<IList<CobroDeFacturaRapidaTarjeta>, IList<CobroDeFacturaRapidaTarjeta>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
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

        LibResponse ILibDataMasterComponent<IList<CobroDeFacturaRapidaTarjeta>, IList<CobroDeFacturaRapidaTarjeta>>.SpecializedUpdate(IList<CobroDeFacturaRapidaTarjeta> refRecord,  bool valUseDetail, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cobro Tarjeta de Factura.Modificar")]
        LibResponse ILibDataMasterComponent<IList<CobroDeFacturaRapidaTarjeta>, IList<CobroDeFacturaRapidaTarjeta>>.Update(IList<CobroDeFacturaRapidaTarjeta> refRecord, bool valUseDetail, eAccionSR valAction) {
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

        bool ILibDataMasterComponent<IList<CobroDeFacturaRapidaTarjeta>, IList<CobroDeFacturaRapidaTarjeta>>.ValidateAll(IList<CobroDeFacturaRapidaTarjeta> refRecords, bool valUseDetail, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (CobroDeFacturaRapidaTarjeta vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }
        #endregion //ILibDataMasterComponent<IList<CobroDeFacturaRapidaTarjeta>, IList<CobroDeFacturaRapidaTarjeta>>

        LibResponse UpdateMaster(CobroDeFacturaRapidaTarjeta refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "CobroDeFacturaRapidaTarjetaUPD"), ParametrosActualizacion(refRecord, valAction));
            return vResult;
        }

        LibResponse UpdateMasterAndDetail(CobroDeFacturaRapidaTarjeta refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            string vErrorMessage = "";
            if (ValidateDetail(refRecord, eAccionSR.Modificar,out vErrorMessage)) {
                if (UpdateDetail(refRecord)) {
                    vResult = UpdateMaster(refRecord, valAction);
                }
            }
            return vResult;
        }

        private bool InsertDetail(CobroDeFacturaRapidaTarjeta valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailCobroDeFacturaRapidaTarjetaDetalleAndUpdateDb(valRecord);
            return vResult;
        }

        private bool SetPkInDetailCobroDeFacturaRapidaTarjetaDetalleAndUpdateDb(CobroDeFacturaRapidaTarjeta valRecord) {
            bool vResult = false;
            int vConsecutivo = 1;
            clsCobroDeFacturaRapidaTarjetaDetalleDat insCobroDeFacturaRapidaTarjetaDetalle = new clsCobroDeFacturaRapidaTarjetaDetalleDat();
            foreach (CobroDeFacturaRapidaTarjetaDetalle vDetail in valRecord.DetailCobroDeFacturaRapidaTarjetaDetalle) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.CodigoFormaDelCobro = valRecord.NumeroFactura;
                vConsecutivo++;
            }
            vResult = insCobroDeFacturaRapidaTarjetaDetalle.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool UpdateDetail(CobroDeFacturaRapidaTarjeta valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailCobroDeFacturaRapidaTarjetaDetalleAndUpdateDb(valRecord);
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
            CobroDeFacturaRapidaTarjeta vRecordBusqueda = new CobroDeFacturaRapidaTarjeta();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.NumeroFactura = valNumeroFactura;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".CobroDeFacturaRapidaTarjeta", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool ValidateMasterDetail(eAccionSR valAction, CobroDeFacturaRapidaTarjeta valRecordMaster, bool valUseDetail) {
            bool vResult = false;
            string vErrMsg;
            if (Validate(valAction, out vErrMsg)) {
                if (valUseDetail) {
                    if (ValidateDetail(valRecordMaster, eAccionSR.Insertar, out vErrMsg)) {
                        vResult = true;
                    } else {
                        throw new GalacValidationException("Cobro Tarjeta de Factura (detalle)\n" + vErrMsg);
                    }
                } else {
                    vResult = true;
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        private bool ValidateDetail(CobroDeFacturaRapidaTarjeta valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            outErrorMessage = "";
            vResult = vResult && ValidateDetailCobroDeFacturaRapidaTarjetaDetalle(valRecord, valAction, out outErrorMessage);
            return vResult;
        }

        private bool ValidateDetailCobroDeFacturaRapidaTarjetaDetalle(CobroDeFacturaRapidaTarjeta valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            foreach (CobroDeFacturaRapidaTarjetaDetalle vDetail in valRecord.DetailCobroDeFacturaRapidaTarjetaDetalle) {
                bool vLineHasError = true;
                //agregar validaciones
                if (LibString.IsNullOrEmpty(vDetail.CodigoFormaDelCobro)) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Codigo Forma Del Cobro.");
                } else if (vDetail.CodigoBanco == 0) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Código del Banco.");
                } else if (vDetail.CodigoPuntoDeVenta == 0) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Código del Banco del Punto de Venta.");
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


    } //End of class clsCobroDeFacturaRapidaTarjetaDat

} //End of namespace Galac.Adm.Dal.Venta

