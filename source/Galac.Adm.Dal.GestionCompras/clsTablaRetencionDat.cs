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
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Dal.GestionCompras {
    public class clsTablaRetencionDat: LibData, ILibDataComponentWithSearch<IList<TablaRetencion>, IList<TablaRetencion>> {
        #region Variables
        TablaRetencion _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private TablaRetencion CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsTablaRetencionDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(TablaRetencion valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInEnum("TipoDePersona", valRecord.TipoDePersonaAsDB);
            vParams.AddInString("Codigo", valRecord.Codigo, 6);
            vParams.AddInString("CodigoSeniat", valRecord.CodigoSeniat, 3);
            vParams.AddInString("TipoDePago", valRecord.TipoDePago, 50);
            vParams.AddInString("Comentarios", valRecord.Comentarios, 255);
            vParams.AddInDecimal("BaseImponible", valRecord.BaseImponible, 2);
            vParams.AddInDecimal("Tarifa", valRecord.Tarifa, 2);
            vParams.AddInDecimal("ParaPagosMayoresDe", valRecord.ParaPagosMayoresDe, 2);
            vParams.AddInDateTime("FechaAplicacion", valRecord.FechaAplicacion);
            vParams.AddInDecimal("Sustraendo", valRecord.Sustraendo, 2);
            vParams.AddInBoolean("AcumulaParaPJND", valRecord.AcumulaParaPJNDAsBool);
            vParams.AddInString("SecuencialDePlantilla", valRecord.SecuencialDePlantilla, 5);
            vParams.AddInString("CodigoMoneda", valRecord.CodigoMoneda, 4);
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(TablaRetencion valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInEnum("TipoDePersona", valRecord.TipoDePersonaAsDB);
            vParams.AddInString("Codigo", valRecord.Codigo, 6);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<TablaRetencion>, IList<TablaRetencion>>

        LibResponse ILibDataComponent<IList<TablaRetencion>, IList<TablaRetencion>>.CanBeChoosen(IList<TablaRetencion> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            TablaRetencion vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValue("Adm.Proveedor", "CodigoRetencionUsual", insDB.InsSql.ToSqlValue(vRecord.Codigo), true)) {
                    vSbInfo.AppendLine("Proveedor");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Tabla Retencion.Eliminar")]
        LibResponse ILibDataComponent<IList<TablaRetencion>, IList<TablaRetencion>>.Delete(IList<TablaRetencion> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "TablaRetencionDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<TablaRetencion> ILibDataComponent<IList<TablaRetencion>, IList<TablaRetencion>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<TablaRetencion> vResult = new List<TablaRetencion>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<TablaRetencion>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                default: throw new NotImplementedException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tabla Retencion.Insertar")]
        LibResponse ILibDataComponent<IList<TablaRetencion>, IList<TablaRetencion>>.Insert(IList<TablaRetencion> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "TablaRetencionINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<TablaRetencion>, IList<TablaRetencion>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                default: throw new NotImplementedException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tabla Retencion.Modificar")]
        LibResponse ILibDataComponent<IList<TablaRetencion>, IList<TablaRetencion>>.Update(IList<TablaRetencion> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "TablaRetencionUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<TablaRetencion>, IList<TablaRetencion>>.ValidateAll(IList<TablaRetencion> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (TablaRetencion vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<TablaRetencion>, IList<TablaRetencion>>.SpecializedUpdate(IList<TablaRetencion> refRecord, string valSpecializedAction) {
            throw new NotImplementedException();
        }
        #endregion //ILibDataComponent<IList<TablaRetencion>, IList<TablaRetencion>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidTipoDePersona(valAction, CurrentRecord.TipoDePersonaAsEnum, CurrentRecord.Codigo);
            vResult = IsValidCodigo(valAction, CurrentRecord.TipoDePersonaAsEnum, CurrentRecord.Codigo) && vResult;
            vResult = IsValidFechaAplicacion(valAction, CurrentRecord.FechaAplicacion) && vResult;
            vResult = IsValidCodigoMoneda(valAction, CurrentRecord.CodigoMoneda) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidTipoDePersona(eAccionSR valAction, eTipodePersonaRetencion valTipoDePersonaAsEnum, string valCodigo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool IsValidCodigo(eAccionSR valAction, eTipodePersonaRetencion valTipoDePersonaAsEnum, string valCodigo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigo = LibString.Trim(valCodigo);
            if (LibString.IsNullOrEmpty(valCodigo, true)) {
                BuildValidationInfo(MsgRequiredField("Código de Retención"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valTipoDePersonaAsEnum, valCodigo)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Código de Retención", valCodigo));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidFechaAplicacion(eAccionSR valAction, DateTime valFechaAplicacion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaAplicacion, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidCodigoMoneda(eAccionSR valAction, string valCodigoMoneda){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoMoneda = LibString.Trim(valCodigoMoneda);
            return vResult;
        }

        private bool KeyExists(eTipodePersonaRetencion valTipoDePersona, string valCodigo) {
            bool vResult = false;
            TablaRetencion vRecordBusqueda = new TablaRetencion();
            vRecordBusqueda.TipoDePersonaAsEnum = valTipoDePersona;
            vRecordBusqueda.Codigo = valCodigo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".TablaRetencion", "TipoDePersona", ParametrosClave(vRecordBusqueda, false, false));
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


    } //End of class clsTablaRetencionDat

} //End of namespace Galac.Adm.Dal.GestionCompras

