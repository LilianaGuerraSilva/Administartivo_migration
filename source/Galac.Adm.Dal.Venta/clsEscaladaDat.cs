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

namespace Galac.Adm.Dal.ComponenteNoEspecificado {
    public class clsEscaladaDat: LibData, ILibDataComponentWithSearch<IList<Escalada>, IList<Escalada>> {
        #region Variables
        Escalada _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private Escalada CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsEscaladaDat() {
            DbSchema = "dbo";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(Escalada valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInGuid("Id", valRecord.Id);
            vParams.AddInInteger("Escalada41", valRecord.Escalada41);
            vParams.AddInDateTime("Escalada32", valRecord.Escalada32);
            vParams.AddInString("Escalada73", valRecord.Escalada73, 40);
            vParams.AddInString("Escalada24", valRecord.Escalada24, 40);
            vParams.AddInString("Escalada85", valRecord.Escalada85, 30);
            vParams.AddInString("Escalada100", valRecord.Escalada100, 255);
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(Escalada valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInGuid("Id", valRecord.Id);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<Escalada>, IList<Escalada>>

        LibResponse ILibDataComponent<IList<Escalada>, IList<Escalada>>.CanBeChoosen(IList<Escalada> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Escalada vRecord = refRecord[0];
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Escalada.Eliminar")]
        LibResponse ILibDataComponent<IList<Escalada>, IList<Escalada>>.Delete(IList<Escalada> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "EscaladaDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<Escalada> ILibDataComponent<IList<Escalada>, IList<Escalada>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<Escalada> vResult = new List<Escalada>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Escalada>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<Escalada>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Escalada.Insertar")]
        LibResponse ILibDataComponent<IList<Escalada>, IList<Escalada>>.Insert(IList<Escalada> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "EscaladaINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<Escalada>, IList<Escalada>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoId")) {
                        vResult = LibXml.ValueToXElement(insDb.NextStrConsecutive(DbSchema + ".Escalada", "Id", valParameters, true, 50), "Id");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Escalada.Modificar")]
        LibResponse ILibDataComponent<IList<Escalada>, IList<Escalada>>.Update(IList<Escalada> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "EscaladaUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<Escalada>, IList<Escalada>>.ValidateAll(IList<Escalada> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (Escalada vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<Escalada>, IList<Escalada>>.SpecializedUpdate(IList<Escalada> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<Escalada>, IList<Escalada>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidId(valAction, CurrentRecord.Id);
            vResult = IsValidEscalada41(valAction, CurrentRecord.Escalada41) && vResult;
            vResult = IsValidEscalada32(valAction, CurrentRecord.Escalada32) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidId(eAccionSR valAction, string valId){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibString.IsNullOrEmpty(valId, true)) {
                BuildValidationInfo(MsgRequiredField("Id"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valId)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Id", valId));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidEscalada41(eAccionSR valAction, int valEscalada41){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valEscalada41 == 0) {
                BuildValidationInfo(MsgRequiredField("Escalada 41"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidEscalada32(eAccionSR valAction, DateTime valEscalada32){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valEscalada32, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(string valId) {
            bool vResult = false;
            Escalada vRecordBusqueda = new Escalada();
            vRecordBusqueda.Id = valId;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".Escalada", "Id", ParametrosClave(vRecordBusqueda, false, false));
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


    } //End of class clsEscaladaDat

} //End of namespace Galac..Dal.ComponenteNoEspecificado

