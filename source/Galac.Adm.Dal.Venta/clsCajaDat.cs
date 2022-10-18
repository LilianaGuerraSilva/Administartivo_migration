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
    public class clsCajaDat: LibData, ILibDataComponentWithSearch<IList<Caja>, IList<Caja>>, ILibDataRpt {
        #region Variables
        Caja _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private Caja CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCajaDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(Caja valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("NombreCaja", valRecord.NombreCaja, 60);
            vParams.AddInBoolean("UsaGaveta", valRecord.UsaGavetaAsBool);
            vParams.AddInEnum("Puerto", valRecord.PuertoAsDB);
            vParams.AddInString("Comando", valRecord.Comando, 10);
            vParams.AddInBoolean("PermitirAbrirSinSupervisor", valRecord.PermitirAbrirSinSupervisorAsBool);
            vParams.AddInBoolean("UsaAccesoRapido", valRecord.UsaAccesoRapidoAsBool);
            vParams.AddInBoolean("UsaMaquinaFiscal", valRecord.UsaMaquinaFiscalAsBool);
            vParams.AddInEnum("FamiliaImpresoraFiscal", valRecord.FamiliaImpresoraFiscalAsDB);
            vParams.AddInEnum("ModeloDeMaquinaFiscal", valRecord.ModeloDeMaquinaFiscalAsDB);
            vParams.AddInString("SerialDeMaquinaFiscal", valRecord.SerialDeMaquinaFiscal, 15);
            vParams.AddInEnum("TipoConexion", valRecord.TipoConexionAsDB);
            vParams.AddInEnum("PuertoMaquinaFiscal", valRecord.PuertoMaquinaFiscalAsDB);
            vParams.AddInBoolean("AbrirGavetaDeDinero", valRecord.AbrirGavetaDeDineroAsBool);
            vParams.AddInString("UltimoNumeroCompFiscal", valRecord.UltimoNumeroCompFiscal, 12);
            vParams.AddInString("UltimoNumeroNCFiscal", valRecord.UltimoNumeroNCFiscal, 12);
            vParams.AddInString("IpParaConexion", valRecord.IpParaConexion, 15);
            vParams.AddInString("MascaraSubred", valRecord.MascaraSubred, 15);
            vParams.AddInString("Gateway", valRecord.Gateway, 15);
            vParams.AddInBoolean("PermitirDescripcionDelArticuloExtendida", valRecord.PermitirDescripcionDelArticuloExtendidaAsBool);
            vParams.AddInBoolean("PermitirNombreDelClienteExtendido", valRecord.PermitirNombreDelClienteExtendidoAsBool);
            vParams.AddInBoolean("UsarModoDotNet", valRecord.UsarModoDotNetAsBool);
            vParams.AddInBoolean("RegistroDeRetornoEnTxt", valRecord.RegistroDeRetornoEnTxtAsBool);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(Caja valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

        private StringBuilder ParametrosProximoConsecutivo(Caja valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<Caja>, IList<Caja>>

        LibResponse ILibDataComponent<IList<Caja>, IList<Caja>>.CanBeChoosen(IList<Caja> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Caja vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValueOnMultifile("dbo.Factura", "ConsecutivoCaja", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Consecutivo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Factura");
                }
                if (insDB.ExistsValueOnMultifile("Adm.CajaApertura", "ConsecutivoCaja", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Consecutivo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Caja Apertura");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Caja Registradora.Eliminar")]
        LibResponse ILibDataComponent<IList<Caja>, IList<Caja>>.Delete(IList<Caja> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CajaDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<Caja> ILibDataComponent<IList<Caja>, IList<Caja>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<Caja> vResult = new List<Caja>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Caja>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<Caja>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Caja Registradora.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Caja Registradora.Crear Caja Generica")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Compañía.Insertar")]
        LibResponse ILibDataComponent<IList<Caja>, IList<Caja>>.Insert(IList<Caja> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    if(CurrentRecord.NombreCaja != "CAJA GENÉRICA" && CurrentRecord.Consecutivo==0) {
                        CurrentRecord.Consecutivo = insDb.NextLngConsecutive(DbSchema + ".Caja", "Consecutivo", ParametrosProximoConsecutivo(CurrentRecord));
                    }                    
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CajaINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<Caja>, IList<Caja>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Caja Registradora.Modificar")]
        LibResponse ILibDataComponent<IList<Caja>, IList<Caja>>.Update(IList<Caja> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CajaUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<Caja>, IList<Caja>>.ValidateAll(IList<Caja> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (Caja vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<Caja>, IList<Caja>>.SpecializedUpdate(IList<Caja> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<Caja>, IList<Caja>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo);
            vResult = IsValidConsecutivo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo) && vResult;
            vResult = IsValidNombreCaja(valAction, CurrentRecord.NombreCaja) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo){
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

        private bool IsValidConsecutivo(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Insertar)) {
                return true;
            }
            if (valConsecutivo < 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                Caja vRecBusqueda = new Caja();
                vRecBusqueda.Consecutivo = valConsecutivo;
                if (KeyExists(valConsecutivoCompania,valConsecutivo)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Consecutivo", valConsecutivo));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidNombreCaja(eAccionSR valAction, string valNombreCaja){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNombreCaja = LibString.Trim(valNombreCaja);
            if (LibString.IsNullOrEmpty(valNombreCaja, true)) {
                BuildValidationInfo(MsgRequiredField("Nombre Caja"));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivo) {
            bool vResult = false;
            Caja vRecordBusqueda = new Caja();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".Caja", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, Caja valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            vResult = insDb.ExistsRecord(DbSchema + ".Caja", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        public bool ActualizaUltimoNumComprobante(StringBuilder valXmlParamsExpression) {
            LibDatabase insDb = new LibDatabase();
            bool vResult = false;
            vResult = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema,"ActualizaUltimoNumComprobante"),valXmlParamsExpression,0);
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
        #region //Miembros de ILibDataRpt
        System.Data.DataTable ILibDataRpt.GetDt(string valSqlStringCommand, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSqlStringCommand, valCmdTimeout);
        }
        System.Data.DataTable ILibDataRpt.GetDt(string valSpName, StringBuilder valXmlParamsExpression, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSpName, valXmlParamsExpression, valCmdTimeout);
        }
        #endregion ////Miembros de ILibDataRpt
        #endregion //Metodos Generados


    } //End of class clsCajaDat

} //End of namespace Galac.Adm.Dal.Venta

