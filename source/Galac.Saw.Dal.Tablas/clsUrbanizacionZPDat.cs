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
    public class clsUrbanizacionZPDat: LibData, ILibDataComponentWithSearch<IList<UrbanizacionZP>, IList<UrbanizacionZP>> {
        #region Variables
        UrbanizacionZP _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private UrbanizacionZP CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsUrbanizacionZPDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(UrbanizacionZP valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInString("Urbanizacion", valRecord.Urbanizacion, 30);
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInString("UrbanizacionOriginal", valRecord.UrbanizacionOriginal, 30);
            }
            vParams.AddInString("ZonaPostal", valRecord.ZonaPostal, 7);
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(UrbanizacionZP valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInString("Urbanizacion", valRecord.Urbanizacion, 30);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<UrbanizacionZP>, IList<UrbanizacionZP>>

        LibResponse ILibDataComponent<IList<UrbanizacionZP>, IList<UrbanizacionZP>>.CanBeChoosen(IList<UrbanizacionZP> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            UrbanizacionZP vRecord = refRecord[0];
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
        LibResponse ILibDataComponent<IList<UrbanizacionZP>, IList<UrbanizacionZP>>.Delete(IList<UrbanizacionZP> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "UrbanizacionZPDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<UrbanizacionZP> ILibDataComponent<IList<UrbanizacionZP>, IList<UrbanizacionZP>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<UrbanizacionZP> vResult = new List<UrbanizacionZP>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<UrbanizacionZP>(valProcessMessage, valParameters, CmdTimeOut);
                    foreach (var vUrbanizacionZP in vResult) {
                        vUrbanizacionZP.UrbanizacionOriginal = vUrbanizacionZP.Urbanizacion;
                    }
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<UrbanizacionZP>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        LibResponse ILibDataComponent<IList<UrbanizacionZP>, IList<UrbanizacionZP>>.Insert(IList<UrbanizacionZP> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "UrbanizacionZPINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<UrbanizacionZP>, IList<UrbanizacionZP>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
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
        LibResponse ILibDataComponent<IList<UrbanizacionZP>, IList<UrbanizacionZP>>.Update(IList<UrbanizacionZP> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "UrbanizacionZPUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<UrbanizacionZP>, IList<UrbanizacionZP>>.ValidateAll(IList<UrbanizacionZP> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (UrbanizacionZP vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<UrbanizacionZP>, IList<UrbanizacionZP>>.SpecializedUpdate(IList<UrbanizacionZP> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<UrbanizacionZP>, IList<UrbanizacionZP>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidUrbanizacion(valAction, CurrentRecord.Urbanizacion,CurrentRecord.UrbanizacionOriginal);
            vResult = IsValidZonaPostal(valAction, CurrentRecord.ZonaPostal) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidUrbanizacion(eAccionSR valAction, string valUrbanizacion, string valUrbanizacionOriginal){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valUrbanizacion = LibString.Trim(valUrbanizacion);
            if (LibString.IsNullOrEmpty(valUrbanizacion, true)) {
                BuildValidationInfo(MsgRequiredField("Nombre Urbanización"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valUrbanizacion)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Urbanización", valUrbanizacion));
                    vResult = false;
                }
            } 
            /*else if (valAction == eAccionSR.Eliminar) {
                StringBuilder vSbInfo = new StringBuilder();
                vSbInfo = ValidateFK(valUrbanizacion);
                if (vSbInfo != null && vSbInfo.Length > 0) {
                    BuildValidationInfo(LibResMsg.InfoForeignKeyCanNotBeDeleted(vSbInfo.ToString()));
                    vResult = false;
                }
            } else if (valAction == eAccionSR.Modificar) {
                StringBuilder vSbInfo = new StringBuilder();
                vSbInfo = ValidateFK(valUrbanizacionOriginal);
                if (vSbInfo != null && vSbInfo.Length > 0) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Urbanización", valUrbanizacion));
                    vResult = false;
                }
            }*/
            return vResult;
        }

        private bool IsValidZonaPostal(eAccionSR valAction, string valZonaPostal){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valZonaPostal = LibString.Trim(valZonaPostal);
            if (LibString.IsNullOrEmpty(valZonaPostal, true)) {
                BuildValidationInfo(MsgRequiredField("Zona Postal"));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(string valUrbanizacion) {
            bool vResult = false;
            UrbanizacionZP vRecordBusqueda = new UrbanizacionZP();
            vRecordBusqueda.Urbanizacion = valUrbanizacion;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".UrbanizacionZP", "Urbanizacion", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private StringBuilder ValidateFK(string valUrbanizacion) {
            LibDatabase insDB = new LibDatabase();
            StringBuilder vResult = new StringBuilder();
            string vFieldSQLValue = insDB.InsSql.ToSqlValue(valUrbanizacion);
            bool vIsSaw = LibDefGen.IsProduct(LibProduct.GetInitialsSaw());

                if (insDB.ExistsValue("UrbanizacionZP", "Urbanizacion", vFieldSQLValue, true)) {
                    vResult.AppendLine("UrbanizacionZP");
                }
            
            insDB.Dispose();
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


    } //End of class clsUrbanizacionZPDat

} //End of namespace Galac.Saw.Dal.Tablas

