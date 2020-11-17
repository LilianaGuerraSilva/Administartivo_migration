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
    public class clsMaquinaFiscalDat: LibData, ILibDataComponentWithSearch<IList<MaquinaFiscal>, IList<MaquinaFiscal>> {
        #region Variables
        MaquinaFiscal _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private MaquinaFiscal CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsMaquinaFiscalDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(MaquinaFiscal valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("ConsecutivoMaquinaFiscal", valRecord.ConsecutivoMaquinaFiscal, 9);
            vParams.AddInString("Descripcion", valRecord.Descripcion, 35);
            vParams.AddInString("NumeroRegistro", valRecord.NumeroRegistro, 20);
            vParams.AddInEnum("Status", valRecord.StatusAsDB);
            vParams.AddInInteger("LongitudNumeroFiscal", valRecord.LongitudNumeroFiscal);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(MaquinaFiscal valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("ConsecutivoMaquinaFiscal", valRecord.ConsecutivoMaquinaFiscal, 9);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(MaquinaFiscal valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<MaquinaFiscal>, IList<MaquinaFiscal>>

        LibResponse ILibDataComponent<IList<MaquinaFiscal>, IList<MaquinaFiscal>>.CanBeChoosen(IList<MaquinaFiscal> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            MaquinaFiscal vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValueOnMultifile("dbo.Factura", "CodigoMaquinaRegistradora", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.ConsecutivoMaquinaFiscal), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Factura");
                }
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
        LibResponse ILibDataComponent<IList<MaquinaFiscal>, IList<MaquinaFiscal>>.Delete(IList<MaquinaFiscal> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "MaquinaFiscalDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<MaquinaFiscal> ILibDataComponent<IList<MaquinaFiscal>, IList<MaquinaFiscal>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<MaquinaFiscal> vResult = new List<MaquinaFiscal>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<MaquinaFiscal>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<MaquinaFiscal>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "Caja Registradora.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        LibResponse ILibDataComponent<IList<MaquinaFiscal>, IList<MaquinaFiscal>>.Insert(IList<MaquinaFiscal> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "MaquinaFiscalINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<MaquinaFiscal>, IList<MaquinaFiscal>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoConsecutivoMaquinaFiscal")) {
                        vResult = LibXml.ValueToXElement(insDb.NextStrConsecutive("Saw.MaquinaFiscal", "ConsecutivoMaquinaFiscal", valParameters, true, 9), "ConsecutivoMaquinaFiscal");
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

        [PrincipalPermission(SecurityAction.Demand,Role = "Caja Registradora.Modificar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Modificar")]
        LibResponse ILibDataComponent<IList<MaquinaFiscal>, IList<MaquinaFiscal>>.Update(IList<MaquinaFiscal> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "MaquinaFiscalUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<MaquinaFiscal>, IList<MaquinaFiscal>>.ValidateAll(IList<MaquinaFiscal> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (MaquinaFiscal vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<MaquinaFiscal>, IList<MaquinaFiscal>>.SpecializedUpdate(IList<MaquinaFiscal> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<MaquinaFiscal>, IList<MaquinaFiscal>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.ConsecutivoMaquinaFiscal);
            vResult = IsValidConsecutivoMaquinaFiscal(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.ConsecutivoMaquinaFiscal) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, string valConsecutivoMaquinaFiscal){
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

        private bool IsValidConsecutivoMaquinaFiscal(eAccionSR valAction, int valConsecutivoCompania, string valConsecutivoMaquinaFiscal){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valConsecutivoMaquinaFiscal = LibString.Trim(valConsecutivoMaquinaFiscal);
            if (LibString.IsNullOrEmpty(valConsecutivoMaquinaFiscal, true)) {
                BuildValidationInfo(MsgRequiredField("Consecutivo"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valConsecutivoMaquinaFiscal)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Consecutivo", valConsecutivoMaquinaFiscal));
                    vResult = false;
                }
            }
            return vResult;
        }

               
        private bool KeyExists(int valConsecutivoCompania, string valConsecutivoMaquinaFiscal) {
            bool vResult = false;
            MaquinaFiscal vRecordBusqueda = new MaquinaFiscal();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.ConsecutivoMaquinaFiscal = valConsecutivoMaquinaFiscal;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord("Saw.MaquinaFiscal", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(StringBuilder valParametros) {
            bool vResult = false;            
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord("Saw.MaquinaFiscal", "ConsecutivoCompania", valParametros);
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


    } //End of class clsMaquinaFiscalDat

} //End of namespace Galac.Saw.Dal.Tablas

