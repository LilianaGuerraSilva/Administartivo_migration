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
using Galac.Saw.Ccl.Integracion;

namespace Galac.Saw.Dal.Integracion {
    public class clsIntegracionSawDat : LibData, ILibDataComponentWithSearch<IList<IntegracionSaw>, IList<IntegracionSaw>>, IIntegracionSawDat {
        #region Variables
        IntegracionSaw _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private IntegracionSaw CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsIntegracionSawDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(IntegracionSaw valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInEnum("TipoIntegracion", valRecord.TipoIntegracionAsDB);
            vParams.AddInString("version", valRecord.version, 8);
            if (valAction == eAccionSR.Modificar ) {
                vParams.AddInEnum("TipoIntegracionOriginal", valRecord.TipoIntegracionOriginalAsDB);
                vParams.AddInString("versionOriginal", valRecord.versionOriginal, 8);
            }
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(IntegracionSaw valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInEnum("TipoIntegracion", valRecord.TipoIntegracionAsDB);
            vParams.AddInString("version", valRecord.version, 8);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<IntegracionSaw>, IList<IntegracionSaw>>

        LibResponse ILibDataComponent<IList<IntegracionSaw>, IList<IntegracionSaw>>.CanBeChoosen(IList<IntegracionSaw> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            IntegracionSaw vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase(Galac.Saw.Ccl.Integracion.LibCkn.ConfigKeyForDbService);
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

        LibResponse ILibDataComponent<IList<IntegracionSaw>, IList<IntegracionSaw>>.Delete(IList<IntegracionSaw> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase(Galac.Saw.Ccl.Integracion.LibCkn.ConfigKeyForDbService);
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "IntegracionSawDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<IntegracionSaw> ILibDataComponent<IList<IntegracionSaw>, IList<IntegracionSaw>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<IntegracionSaw> vResult = new List<IntegracionSaw>();
            LibDatabase insDb = new LibDatabase(Galac.Saw.Ccl.Integracion.LibCkn.ConfigKeyForDbService);
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<IntegracionSaw>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<IntegracionSaw>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        LibResponse ILibDataComponent<IList<IntegracionSaw>, IList<IntegracionSaw>>.Insert(IList<IntegracionSaw> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase(Galac.Saw.Ccl.Integracion.LibCkn.ConfigKeyForDbService);
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "IntegracionSawINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<IntegracionSaw>, IList<IntegracionSaw>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase(Galac.Saw.Ccl.Integracion.LibCkn.ConfigKeyForDbService);
            switch (valType) {
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                case eProcessMessageType.Query:
                     //vResult = LibXml.ToXElement(insDb.LoadData(valParameters.ToString(), CmdTimeOut));
                     vResult = LibXml.ToXElement(insDb.LoadData(valProcessMessage, CmdTimeOut, valParameters));
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        LibResponse ILibDataComponent<IList<IntegracionSaw>, IList<IntegracionSaw>>.Update(IList<IntegracionSaw> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase(Galac.Saw.Ccl.Integracion.LibCkn.ConfigKeyForDbService);
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "IntegracionSawUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<IntegracionSaw>, IList<IntegracionSaw>>.ValidateAll(IList<IntegracionSaw> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (IntegracionSaw vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<IntegracionSaw>, IList<IntegracionSaw>>.SpecializedUpdate(IList<IntegracionSaw> refRecord, string valSpecializedAction) {
            throw new NotImplementedException();
        }
        #endregion //ILibDataComponent<IList<IntegracionSaw>, IList<IntegracionSaw>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidTipoIntegracion(valAction, CurrentRecord.TipoIntegracionAsEnum, CurrentRecord.version);
            vResult = IsValidversion(valAction, CurrentRecord.TipoIntegracionAsEnum, CurrentRecord.version) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidTipoIntegracion(eAccionSR valAction, eTipoIntegracion valTipoIntegracionAsEnum, string valversion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool IsValidversion(eAccionSR valAction, eTipoIntegracion valTipoIntegracionAsEnum, string valversion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valversion = LibString.Trim(valversion);
            if (LibString.IsNullOrEmpty(valversion, true)) {
                BuildValidationInfo(MsgRequiredField("Versión"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valTipoIntegracionAsEnum, valversion)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Versión", valversion));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool KeyExists(eTipoIntegracion valTipoIntegracion, string valversion) {
            bool vResult = false;
            IntegracionSaw vRecordBusqueda = new IntegracionSaw();
            vRecordBusqueda.TipoIntegracionAsEnum = valTipoIntegracion;
            vRecordBusqueda.version = valversion;
            LibDatabase insDb = new LibDatabase(Galac.Saw.Ccl.Integracion.LibCkn.ConfigKeyForDbService);
            vResult = insDb.ExistsRecord("Saw.Integracion", "TipoIntegracion", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones
        #region Miembros de ILibDataFKSearch
        bool ILibDataFKSearch.ConnectFk(ref XmlDocument refResulset, eProcessMessageType valType, string valProcessMessage, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase(Galac.Saw.Ccl.Integracion.LibCkn.ConfigKeyForDbService);
            refResulset = insDb.LoadForConnect(valProcessMessage, valXmlParamsExpression, CmdTimeOut);
            if (refResulset != null && refResulset.DocumentElement != null && refResulset.DocumentElement.HasChildNodes) {
                vResult = true;
            }
            return vResult;
        }
        #endregion //Miembros de ILibDataFKSearch
        #endregion //Metodos Generados



       bool IIntegracionSawDat.ActualizaVersion(IList<IntegracionSaw> valRecord) {
            string vErrMsg = "";
            bool vResult=false ;
            CurrentRecord = valRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase(Galac.Saw.Ccl.Integracion.LibCkn.ConfigKeyForDbService);
                    vResult = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "IntegracionSawUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

       bool IIntegracionSawDat.ConectarCompanias(string  valCodigoCompania, string valCodigoConexion) {
           bool vResult = false;
           int vConsecutivoCompania;
           vConsecutivoCompania = FindConsecutivoCompania("Codigo", valCodigoCompania);
           LibDatabase insDb = new LibDatabase(Galac.Saw.Ccl.Integracion.LibCkn.ConfigKeyForDbService);
           vResult = insDb.ExecSpNonQueryNonTransaction("dbo.Gp_CompaniaUpdateConexionNomina", ParametrosActualizacion(vConsecutivoCompania, valCodigoConexion, true));
           insDb.Dispose();
           return vResult;
     
        }

        bool IIntegracionSawDat.DesConectarCompanias(string valCodigoIntegracion) {
           bool vResult = true;
           int vConsecutivoCompania;
           vConsecutivoCompania = FindConsecutivoCompania("CodigoDeIntegracion", valCodigoIntegracion);
           LibDatabase insDb = new LibDatabase(Galac.Saw.Ccl.Integracion.LibCkn.ConfigKeyForDbService);
           vResult = insDb.ExecSpNonQueryNonTransaction("dbo.Gp_CompaniaUpdateConexionNomina", ParametrosActualizacion(vConsecutivoCompania, "", false));
           insDb.Dispose();
           return vResult;
        }

       

        bool IIntegracionSawDat.VersionesCompatibles(string valVersion) {
            QAdvSql insQAdvSql = new QAdvSql("");
            LibDatabase insDb = new LibDatabase(Galac.Saw.Ccl.Integracion.LibCkn.ConfigKeyForDbService);
            LibDbo insDbo = new LibDbo(Galac.Saw.Ccl.Integracion.LibCkn.ConfigKeyForDbService);
            StringBuilder vSql = new StringBuilder();
            string vWhere = "";
            bool vResult = false;
            if (insDbo.Exists("Saw.Integracion", eDboType.Tabla)) {
                vWhere = insQAdvSql.SqlValueWithAnd("", "version", valVersion);
                vWhere = insQAdvSql.SqlEnumValueWithAnd(vWhere, "TipoIntegracion", (int)eTipoIntegracion.NOMINA);
                vWhere = insQAdvSql.WhereSql(vWhere);
                vSql.Append(" SELECT version FROM Saw.Integracion  ");
                vSql.Append(vWhere);
                object vValue = insDb.ExecuteScalar(vSql.ToString(), -1, false);
                if (vValue != null) {
                    vResult = true;
                }
            }
            insDb.Dispose();
            return vResult;
        }

        private StringBuilder ParametrosActualizacion(int valConsecutivoCompania, string valCodigoConexion, bool valEstaIntegradaConNomina ) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoDeIntegracion", valCodigoConexion, 40);
            vParams.AddInBoolean("EstaIntegradaConNomina", valEstaIntegradaConNomina);
            vResult = vParams.Get();
            return vResult;
        }

        private static int FindConsecutivoCompania(string valUniqueKeyFieldName, string valUniqueKeyFieldValue) {
            int vResult = 0;
            LibDatabase insDb = new LibDatabase(Galac.Saw.Ccl.Integracion.LibCkn.ConfigKeyForDbService);
            string vSql = "SELECT ConsecutivoCompania FROM COMPANIA WHERE " + valUniqueKeyFieldName + " = " + new QAdvSql("").ToSqlValue(valUniqueKeyFieldValue);
            object vValue = insDb.ExecuteScalar(vSql, -1, false);
            if (vValue != null) {
                vResult = LibConvert.ToInt(vValue);
            } else {
                throw new GalacValidationException("Codigo no encontrado: " + valUniqueKeyFieldValue);

            }
            insDb.Dispose();
            return vResult;
        }

        public bool ExisteTablaSettValueByCompany() {
            return new LibDbo(LibCkn.ConfigKeyForDbService).Exists("Comun.SettValueByCompany",eDboType.Tabla) || new LibDbo(LibCkn.ConfigKeyForDbService).Exists("Adme.SettValueByCompany",eDboType.Tabla);
        }

      
       
    } //End of class clsIntegracionSawDat

} //End of namespace Galac.Saw.Dal.Integracion

