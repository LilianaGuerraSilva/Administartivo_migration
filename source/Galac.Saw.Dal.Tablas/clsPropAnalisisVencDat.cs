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
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Dal.Tablas {
    public class clsPropAnalisisVencDat: LibData, ILibDataComponentWithSearch<IList<PropAnalisisVenc>, IList<PropAnalisisVenc>> {
        #region Variables
        PropAnalisisVenc _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private PropAnalisisVenc CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsPropAnalisisVencDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(PropAnalisisVenc valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("SecuencialUnique0", valRecord.SecuencialUnique0);
            vParams.AddInInteger("PrimerVencimiento", valRecord.PrimerVencimiento);
            vParams.AddInInteger("SegundoVencimiento", valRecord.SegundoVencimiento);
            vParams.AddInInteger("TercerVencimiento", valRecord.TercerVencimiento);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(PropAnalisisVenc valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("SecuencialUnique0", valRecord.SecuencialUnique0);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<PropAnalisisVenc>, IList<PropAnalisisVenc>>

        LibResponse ILibDataComponent<IList<PropAnalisisVenc>, IList<PropAnalisisVenc>>.CanBeChoosen(IList<PropAnalisisVenc> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            PropAnalisisVenc vRecord = refRecord[0];
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Eliminar")]
        LibResponse ILibDataComponent<IList<PropAnalisisVenc>, IList<PropAnalisisVenc>>.Delete(IList<PropAnalisisVenc> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "PropAnalisisVencDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<PropAnalisisVenc> ILibDataComponent<IList<PropAnalisisVenc>, IList<PropAnalisisVenc>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<PropAnalisisVenc> vResult = new List<PropAnalisisVenc>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<PropAnalisisVenc>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<PropAnalisisVenc>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        LibResponse ILibDataComponent<IList<PropAnalisisVenc>, IList<PropAnalisisVenc>>.Insert(IList<PropAnalisisVenc> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "PropAnalisisVencINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<PropAnalisisVenc>, IList<PropAnalisisVenc>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Modificar")]
        LibResponse ILibDataComponent<IList<PropAnalisisVenc>, IList<PropAnalisisVenc>>.Update(IList<PropAnalisisVenc> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "PropAnalisisVencUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<PropAnalisisVenc>, IList<PropAnalisisVenc>>.ValidateAll(IList<PropAnalisisVenc> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (PropAnalisisVenc vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<PropAnalisisVenc>, IList<PropAnalisisVenc>>.SpecializedUpdate(IList<PropAnalisisVenc> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<PropAnalisisVenc>, IList<PropAnalisisVenc>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidSecuencialUnique0(valAction, CurrentRecord.SecuencialUnique0);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidSecuencialUnique0(eAccionSR valAction, int valSecuencialUnique0){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valSecuencialUnique0 == 0) {
                BuildValidationInfo(MsgRequiredField("Secuencial Unique 0"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valSecuencialUnique0)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Secuencial Unique 0", valSecuencialUnique0));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool KeyExists(int valSecuencialUnique0) {
            bool vResult = false;
            PropAnalisisVenc vRecordBusqueda = new PropAnalisisVenc();
            vRecordBusqueda.SecuencialUnique0 = valSecuencialUnique0;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".PropAnalisisVenc", "SecuencialUnique0", ParametrosClave(vRecordBusqueda, false, false));
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


    } //End of class clsPropAnalisisVencDat

} //End of namespace Galac.Saw.Dal.Tablas

