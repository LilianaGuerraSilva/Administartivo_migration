using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Dal;
using LibGalac.Aos.DefGen;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Dal.GestionCompras {
    public class clsCargaInicialDat: LibData, ILibDataComponentWithSearch<IList<CargaInicial>, IList<CargaInicial>> {
        #region Variables
        CargaInicial _CurrentRecord;
        #endregion //Variables

        #region Propiedades
        private CargaInicial CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades

        #region Constructores

        public clsCargaInicialDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores

        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(CargaInicial valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInDateTime("Fecha", valRecord.Fecha.AddDays(-1));
            vParams.AddInDecimal("Existencia", valRecord.Existencia, 2);
            vParams.AddInDecimal("Costo", valRecord.Costo, 2);
            vParams.AddInString("CodigoArticulo", valRecord.CodigoArticulo, 30);
            vParams.AddInString("EsCargaInicial", valRecord.EsCargaInicial, 1);
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(CargaInicial valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

        private StringBuilder ParametrosProximoConsecutivo(CargaInicial valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }

        #region Miembros de ILibDataComponent<IList<CargaInicial>, IList<CargaInicial>>

        LibResponse ILibDataComponent<IList<CargaInicial>, IList<CargaInicial>>.CanBeChoosen(IList<CargaInicial> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            CargaInicial vRecord = refRecord[0];
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Carga Inicial.Eliminar")]
        LibResponse ILibDataComponent<IList<CargaInicial>, IList<CargaInicial>>.Delete(IList<CargaInicial> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CargaInicialDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<CargaInicial> ILibDataComponent<IList<CargaInicial>, IList<CargaInicial>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<CargaInicial> vResult = new List<CargaInicial>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<CargaInicial>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<CargaInicial>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Carga Inicial.Insertar")]
        LibResponse ILibDataComponent<IList<CargaInicial>, IList<CargaInicial>>.Insert(IList<CargaInicial> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CargaInicialINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<CargaInicial>, IList<CargaInicial>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Carga Inicial.Modificar")]
        LibResponse ILibDataComponent<IList<CargaInicial>, IList<CargaInicial>>.Update(IList<CargaInicial> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CargaInicialUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<CargaInicial>, IList<CargaInicial>>.ValidateAll(IList<CargaInicial> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (CargaInicial vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<CargaInicial>, IList<CargaInicial>>.SpecializedUpdate(IList<CargaInicial> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<CargaInicial>, IList<CargaInicial>>

        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo);
            vResult = IsValidConsecutivo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo) && vResult;
            vResult = IsValidFecha(valAction, CurrentRecord.Fecha) && vResult;
            vResult = IsValidCodigoArticulo(valAction, CurrentRecord.CodigoArticulo) && vResult;
            vResult = IsValidEsCargaInicial(valAction, CurrentRecord.EsCargaInicial) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoCompania <= 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Compañía"));
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

        //private bool IsValidCodigo(eAccionSR valAction, string valCodigo){
        //    bool vResult = true;
        //    if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
        //        return true;
        //    }
        //    valCodigo = LibString.Trim(valCodigo);
        //    if (LibString.IsNullOrEmpty(valCodigo, true)) {
        //        BuildValidationInfo(MsgRequiredField("Código"));
        //        vResult = false;
        //    }
        //    return vResult;
        //}

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

        private bool IsValidCodigoArticulo(eAccionSR valAction, string valCodigoArticulo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoArticulo = LibString.Trim(valCodigoArticulo);
            if (LibString.IsNullOrEmpty(valCodigoArticulo, true)) {
                BuildValidationInfo(MsgRequiredField("Código del Artículo"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidEsCargaInicial(eAccionSR valAction, string valEsCargaInicial){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valEsCargaInicial = LibString.Trim(valEsCargaInicial);
            if (LibString.IsNullOrEmpty(valEsCargaInicial, true)) {
                BuildValidationInfo(MsgRequiredField("Es Carga Inicial"));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivo) {
            bool vResult = false;
            CargaInicial vRecordBusqueda = new CargaInicial();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".CargaInicial", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, CargaInicial valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            vResult = insDb.ExistsRecord(DbSchema + ".CargaInicial", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
            insDb.Dispose();
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

        #region Insertar,Modificar

        [PrincipalPermission(SecurityAction.Demand, Role = "Carga Inicial.Insertar")]
        public bool InsertarCargaInicial(IList<CargaInicial> cargaInicial) {
            string vErrMsg = "";
            bool vResult = true;
            LibDatabase insDb = new LibDatabase();
            foreach (var record in cargaInicial) {
                CurrentRecord = record;
                record.Consecutivo = insDb.NextLngConsecutive("Adm.CargaInicial", "Consecutivo", "");
                if (ExecuteProcessBeforeInsert()) {
                    if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                        vResult = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CargaInicialINS"), ParametrosActualizacion(record, eAccionSR.Insertar)) && vResult;
                    } else {
                        throw new GalacValidationException(vErrMsg);
                    }
                }
                insDb.Dispose();
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Carga Inicial.Modificar")]
        public bool ModificarCargaInicial(IList<CargaInicial> vArticulosACargar) {
            string vErrMsg = "";
            bool vResult = vArticulosACargar.Any(t => t.TieneCambios);
            LibDatabase insDb = new LibDatabase();
            foreach (var record in vArticulosACargar) {
                if (!record.TieneCambios)
                    continue;
                CurrentRecord = record;
                if (ExecuteProcessBeforeInsert()) {
                    if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                        vResult = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CargaInicialUPD"), ParametrosActualizacion(record, eAccionSR.Modificar)) && vResult;
                        if (vResult) {
                            XmlDocument updatedRecord = insDb.LoadData(DbSchema + ".Gp_CargaInicialGETUPDRecord",
                                ParametrosGetUpdatedRecord(record.Consecutivo), 0);
                        }
                        record.TieneCambios = false;
                    } else {
                        throw new GalacValidationException(vErrMsg);
                    }
                }
                insDb.Dispose();
            }
            return vResult;
        }

        private StringBuilder ParametrosGetUpdatedRecord(int valConsecutivo) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("consecutivo", valConsecutivo);
            vParams.AddInInteger("consecutivoCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania"));
            return vParams.Get();
        }
        #endregion


    } //End of class clsCargaInicialDat

} //End of namespace Galac.Adm.Dal.GestionCompras

