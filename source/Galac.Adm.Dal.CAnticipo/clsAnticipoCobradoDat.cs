using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
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
using Galac.Adm.Ccl.CAnticipo;

namespace Galac.Adm.Dal.CAnticipo {
    public class clsAnticipoCobradoDat: LibData, ILibDataComponentWithSearch<IList<AnticipoCobrado>, IList<AnticipoCobrado>> {
        #region Variables
        AnticipoCobrado _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private AnticipoCobrado CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsAnticipoCobradoDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(AnticipoCobrado valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NumeroCobranza", valRecord.NumeroCobranza, 10);
            vParams.AddInInteger("Secuencial", valRecord.Secuencial);
            vParams.AddInInteger("ConsecutivoAnticipoUsado", valRecord.ConsecutivoAnticipoUsado);
            vParams.AddInString("NumeroAnticipo", valRecord.NumeroAnticipo, 20);
            vParams.AddInDecimal("MontoOriginal", valRecord.MontoOriginal, 2);
            vParams.AddInDecimal("MontoRestanteAlDia", valRecord.MontoRestanteAlDia, 2);
            vParams.AddInString("SimboloMoneda", valRecord.SimboloMoneda, 4);
            vParams.AddInString("CodigoMoneda", valRecord.CodigoMoneda, 4);
            vParams.AddInDecimal("MontoTotalDelAnticipo", valRecord.MontoTotalDelAnticipo, 2);
            vParams.AddInDecimal("MontoAplicado", valRecord.MontoAplicado, 2);
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(AnticipoCobrado valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NumeroCobranza", valRecord.NumeroCobranza, 10);
            vParams.AddInInteger("Secuencial", valRecord.Secuencial);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(AnticipoCobrado valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NumeroCobranza", valRecord.NumeroCobranza, 10);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<AnticipoCobrado>, IList<AnticipoCobrado>>

        LibResponse ILibDataComponent<IList<AnticipoCobrado>, IList<AnticipoCobrado>>.CanBeChoosen(IList<AnticipoCobrado> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            AnticipoCobrado vRecord = refRecord[0];
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Anticipo Cobrado.Eliminar")]
        LibResponse ILibDataComponent<IList<AnticipoCobrado>, IList<AnticipoCobrado>>.Delete(IList<AnticipoCobrado> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "AnticipoCobradoDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<AnticipoCobrado> ILibDataComponent<IList<AnticipoCobrado>, IList<AnticipoCobrado>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<AnticipoCobrado> vResult = new List<AnticipoCobrado>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<AnticipoCobrado>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<AnticipoCobrado>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Anticipo Cobrado.Insertar")]
        LibResponse ILibDataComponent<IList<AnticipoCobrado>, IList<AnticipoCobrado>>.Insert(IList<AnticipoCobrado> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "AnticipoCobradoINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<AnticipoCobrado>, IList<AnticipoCobrado>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoSecuencial")) {
                        vResult = LibXml.ValueToXElement(insDb.NextLngConsecutive(DbSchema + ".AnticipoCobrado", "Secuencial", valParameters), "Secuencial");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Anticipo Cobrado.Modificar")]
        LibResponse ILibDataComponent<IList<AnticipoCobrado>, IList<AnticipoCobrado>>.Update(IList<AnticipoCobrado> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "AnticipoCobradoUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<AnticipoCobrado>, IList<AnticipoCobrado>>.ValidateAll(IList<AnticipoCobrado> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (AnticipoCobrado vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<AnticipoCobrado>, IList<AnticipoCobrado>>.SpecializedUpdate(IList<AnticipoCobrado> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<AnticipoCobrado>, IList<AnticipoCobrado>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            // revisar SAMUEL OJO
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.NumeroCobranza, CurrentRecord.Secuencial);
            vResult = IsValidNumeroCobranza(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.NumeroCobranza, CurrentRecord.Secuencial) && vResult;
            vResult = IsValidSecuencial(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Secuencial, CurrentRecord.NumeroCobranza ) && vResult;
            vResult = IsValidConsecutivoAnticipoUsado(valAction, CurrentRecord.ConsecutivoAnticipoUsado) && vResult;
            vResult = IsValidNumeroAnticipo(valAction, CurrentRecord.NumeroAnticipo) && vResult;
            vResult = IsValidCodigoMoneda(valAction, CurrentRecord.CodigoMoneda) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, string valNumeroCobranza, int valSecuencial){
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

        private bool IsValidNumeroCobranza(eAccionSR valAction, int valConsecutivoCompania, string valNumeroCobranza, int valSecuencial){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumeroCobranza = LibString.Trim(valNumeroCobranza);
            if (LibString.IsNullOrEmpty(valNumeroCobranza, true)) {
                BuildValidationInfo(MsgRequiredField("Numero Cobranza"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valNumeroCobranza, valSecuencial)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Numero Cobranza", valNumeroCobranza));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidSecuencial(eAccionSR valAction, int valConsecutivoCompania, int valSecuencial, string valNumeroCobranza){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valSecuencial == 0) {
                BuildValidationInfo(MsgRequiredField("Secuencial"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valNumeroCobranza, valSecuencial)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Secuencial", valSecuencial));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidConsecutivoAnticipoUsado(eAccionSR valAction, int valConsecutivoAnticipoUsado){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoAnticipoUsado == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Anticipo Usado"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidNumeroAnticipo(eAccionSR valAction, string valNumeroAnticipo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumeroAnticipo = LibString.Trim(valNumeroAnticipo);
            if (LibString.IsNullOrEmpty(valNumeroAnticipo , true)) {
                BuildValidationInfo(MsgRequiredField("Numero Anticipo"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Adm.Anticipo", "Numero", insDb.InsSql.ToSqlValue(valNumeroAnticipo), true)) {
                    BuildValidationInfo("El valor asignado al campo Numero Anticipo no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCodigoMoneda(eAccionSR valAction, string valCodigoMoneda){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoMoneda = LibString.Trim(valCodigoMoneda);
            if (LibString.IsNullOrEmpty(valCodigoMoneda , true)) {
                BuildValidationInfo(MsgRequiredField("Codigo Moneda"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Comun.Moneda", "Codigo", insDb.InsSql.ToSqlValue(valCodigoMoneda), true)) {
                    BuildValidationInfo("El valor asignado al campo Codigo Moneda no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valNumeroCobranza, int valSecuencial) {
            bool vResult = false;
            AnticipoCobrado vRecordBusqueda = new AnticipoCobrado();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.NumeroCobranza = valNumeroCobranza;
            vRecordBusqueda.Secuencial = valSecuencial;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".AnticipoCobrado", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
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


    } //End of class clsAnticipoCobradoDat

} //End of namespace Galac.Adm.Dal.CAnticipo

