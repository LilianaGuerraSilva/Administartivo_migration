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
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Dal.Banco {
    public class clsConceptoBancarioDat: LibData, ILibDataComponentWithSearch<IList<ConceptoBancario>, IList<ConceptoBancario>> {
        #region Variables
        ConceptoBancario _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private ConceptoBancario CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsConceptoBancarioDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(ConceptoBancario valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("Codigo", valRecord.Codigo, 8);
            vParams.AddInString("Descripcion", valRecord.Descripcion, 30);
            vParams.AddInEnum("Tipo", valRecord.TipoAsDB);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 20);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(ConceptoBancario valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<ConceptoBancario>, IList<ConceptoBancario>>

        LibResponse ILibDataComponent<IList<ConceptoBancario>, IList<ConceptoBancario>>.CanBeChoosen(IList<ConceptoBancario> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            ConceptoBancario vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (LibDefGen.IsProduct(LibProduct.GetInitialsSaw())) {
                    if (getParametrosCompania<string>("ConceptoReversoCobranza", this).Equals(vRecord.Codigo)) {
                        vSbInfo.AppendLine("Parametros Compania");
                    }
                    if (getParametrosCompania<string>("ConceptoDebitoBancario", this).Equals(vRecord.Codigo)) {
                        vSbInfo.AppendLine("Parametros Compania");
                    }
                    if (getParametrosCompania<string>("ConceptoCreditoBancario", this).Equals(vRecord.Codigo)) {
                        vSbInfo.AppendLine("Parametros Compania");
                    }
                    if (getParametrosCompania<string>("ConceptoBancarioAnticipoCobrado", this).Equals(vRecord.Codigo)) {
                        vSbInfo.AppendLine("Parametros Compania");
                    }
                    if (getParametrosCompania<string>("ConceptoBancarioAnticipoPagado", this).Equals(vRecord.Codigo)) {
                        vSbInfo.AppendLine("Parametros Compania");
                    }
                    if (getParametrosCompania<string>("ConceptoBancarioReversoAnticipoCobrado", this).Equals(vRecord.Codigo)) {
                        vSbInfo.AppendLine("Parametros Compania");
                    }
                    if (getParametrosCompania<string>("ConceptoBancarioReversoAnticipoPagado", this).Equals(vRecord.Codigo)) {
                        vSbInfo.AppendLine("Parametros Compania");
                    }
                    if (getParametrosCompania<string>("ConceptoBancarioCobroDirecto", this).Equals(vRecord.Codigo)) {
                        vSbInfo.AppendLine("Parametros Compania");
                    }
                    if (getParametrosCompania<string>("ConceptoBancarioReversoSolicitudDePago", this).Equals(vRecord.Codigo)) {
                        vSbInfo.AppendLine("Parametros Compania");
                    }
                    if (getParametrosCompania<string>("ConceptoBancarioReversoDePago", this).Equals(vRecord.Codigo)) {
                        vSbInfo.AppendLine("Parametros Compania");
                    }
                    if (insDB.ExistsValue("dbo.Pago", "CodigoConcepto", insDB.InsSql.ToSqlValue(vRecord.Codigo), true)) {
                        vSbInfo.AppendLine("Pago");
                    }
                    if (insDB.ExistsValue("dbo.MovimientoBancario", "CodigoConcepto", insDB.InsSql.ToSqlValue(vRecord.Codigo), true)) {
                        vSbInfo.AppendLine("Movimiento Bancario");
                    }
                    if (insDB.ExistsValue("dbo.Anticipo", "CodigoConceptoBancario", insDB.InsSql.ToSqlValue(vRecord.Codigo), true)) {
                        vSbInfo.AppendLine("Anticipo");
                    }
                    if (vSbInfo.Length == 0) {
                        vResult.Success = true;
                    }
                } else {
                    if (insDB.ExistsValueOnMultifile("dbo.ParametrosCompania", "ConceptoReversoCobranza","ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.Consecutivo), true)) {
                        vSbInfo.AppendLine("Parámetros Compañía");
                    }
                    if (insDB.ExistsValueOnMultifile("dbo.ParametrosCompania", "ConceptoDebitoBancario", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.Consecutivo), true)) {
                        vSbInfo.AppendLine("Parámetros Compañía");
                    }
                    if (insDB.ExistsValueOnMultifile("dbo.ParametrosCompania", "ConceptoBancarioAnticipoCobrado", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.Consecutivo), true)) {
                        vSbInfo.AppendLine("Parámetros Compañía");
                    }
                    if (insDB.ExistsValueOnMultifile("dbo.ParametrosCompania", "ConceptoBancarioAnticipoPagado", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.Consecutivo), true)) {
                        vSbInfo.AppendLine("Parámetros Compañía");
                    }
                    if (insDB.ExistsValueOnMultifile("dbo.ParametrosCompania", "ConceptoBancarioReversoAnticipoCobrado", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.Consecutivo), true)) {
                        vSbInfo.AppendLine("Parámetros Compañía");
                    }
                    if (insDB.ExistsValueOnMultifile("dbo.ParametrosCompania", "ConceptoBancarioReversoAnticipoPagado", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.Consecutivo), true)) {
                        vSbInfo.AppendLine("Parámetros Compañía");
                    }
                    if (insDB.ExistsValueOnMultifile("dbo.ParametrosCompania", "ConceptoBancarioCobroDirecto", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.Consecutivo), true)) {
                        vSbInfo.AppendLine("Parámetros Compañía");
                    }
                    if (insDB.ExistsValueOnMultifile("dbo.ParametrosCompania", "ConceptoBancarioReversoSolicitudDePago", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.Consecutivo), true)) {
                        vSbInfo.AppendLine("Parámetros Compañía");
                    }
                    if (insDB.ExistsValueOnMultifile("dbo.ParametrosCompania", "ConceptoBancarioReversoDePago", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.Consecutivo), true)) {
                        vSbInfo.AppendLine("Parámetros Compañía");
                    }
                    if (insDB.ExistsValueOnMultifile("dbo.Pago", "CodigoConcepto", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.Consecutivo), true)) {
                        vSbInfo.AppendLine("Parámetros Compañía");
                    }
                    if (insDB.ExistsValueOnMultifile("dbo.MovimientoBancario", "CodigoConcepto", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.Consecutivo), true)) {
                        vSbInfo.AppendLine("Parámetros Compañía");
                    }
                    if (insDB.ExistsValueOnMultifile("dbo.Anticipo", "CodigoConceptoBancario", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.Consecutivo), true)) {
                        vSbInfo.AppendLine("Parámetros Compañía");
                    }
                    if (vSbInfo.Length == 0) {
                        vResult.Success = true;
                    }
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

        private T getParametrosCompania<T>(string ValParametro, ILibDataComponent<IList<ConceptoBancario>, IList<ConceptoBancario>> instanciaDal){
            string vDbSchema = "";
            if (LibDefGen.IsProduct(LibProduct.GetInitialsAdmEcuador())){
                vDbSchema = "Adme";
            }else{
                vDbSchema = "Comun";
            }
            StringBuilder sql = new StringBuilder("SELECT " + vDbSchema + ".SettValueByCompany.Value AS Valor, " + vDbSchema + ".SettValueByCompany.NameSettDefinition FROM " + vDbSchema + ".SettDefinition INNER JOIN " + vDbSchema + ".SettValueByCompany ON " + vDbSchema + ".SettDefinition.Name = " + vDbSchema + ".SettValueByCompany.NameSettDefinition WHERE     (" + vDbSchema + ".SettDefinition.Name = '" + ValParametro + "')", 300);
            XElement Auxiliar = instanciaDal.QueryInfo(eProcessMessageType.Query, null, sql);
            Object vValor = LibXml.GetPropertyString(Auxiliar, "Valor");
            return (T)vValor;
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "Concepto Bancario.Eliminar")]
        LibResponse ILibDataComponent<IList<ConceptoBancario>, IList<ConceptoBancario>>.Delete(IList<ConceptoBancario> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ConceptoBancarioDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<ConceptoBancario> ILibDataComponent<IList<ConceptoBancario>, IList<ConceptoBancario>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<ConceptoBancario> vResult = new List<ConceptoBancario>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<ConceptoBancario>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                default: break;
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Concepto Bancario.Insertar")]
        LibResponse ILibDataComponent<IList<ConceptoBancario>, IList<ConceptoBancario>>.Insert(IList<ConceptoBancario> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    CurrentRecord.Consecutivo = insDb.NextLngConsecutive("Adm.ConceptoBancario", "Consecutivo", "");
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ConceptoBancarioINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<ConceptoBancario>, IList<ConceptoBancario>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                case eProcessMessageType.Query:
                    vResult = LibXml.ToXElement(insDb.LoadData(valParameters.ToString(), CmdTimeOut));
                    break;
                default: break;
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Concepto Bancario.Modificar")]
        LibResponse ILibDataComponent<IList<ConceptoBancario>, IList<ConceptoBancario>>.Update(IList<ConceptoBancario> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ConceptoBancarioUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<ConceptoBancario>, IList<ConceptoBancario>>.ValidateAll(IList<ConceptoBancario> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (ConceptoBancario vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<ConceptoBancario>, IList<ConceptoBancario>>.SpecializedUpdate(IList<ConceptoBancario> refRecord, string valSpecializedAction) {
            throw new NotImplementedException();
        }
        #endregion //ILibDataComponent<IList<ConceptoBancario>, IList<ConceptoBancario>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivo(valAction, CurrentRecord.Consecutivo);
            vResult = IsValidCodigo(valAction, CurrentRecord.Codigo) && vResult;
            vResult = IsValidDescripcion(valAction, CurrentRecord.Descripcion) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }
        private bool IsValidConsecutivo(eAccionSR valAction, int valConsecutivo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Insertar)) {
                return true;
            }
            if (valConsecutivo <= 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivo)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Consecutivo", valConsecutivo));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCodigo(eAccionSR valAction, string valCodigo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigo = LibString.Trim(valCodigo);
            if (LibString.IsNullOrEmpty(valCodigo, true)) {
                BuildValidationInfo(MsgRequiredField("Codigo"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidDescripcion(eAccionSR valAction, string valDescripcion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valDescripcion = LibString.Trim(valDescripcion);
            if (LibString.IsNullOrEmpty(valDescripcion, true)) {
                BuildValidationInfo(MsgRequiredField("Descripcion"));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivo) {
            bool vResult = false;
            ConceptoBancario vRecordBusqueda = new ConceptoBancario();
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord("Adm.ConceptoBancario", "Consecutivo", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
		public bool ExisteYEsEgresoIngreso(string valCodigo, eIngresoEgreso valTipo) {
			LibDatabase insDB = new LibDatabase();
			LibGpParams dbParam = new LibGpParams();
			dbParam.AddInString("Codigo", valCodigo, 8);
			dbParam.AddInEnum("Tipo", (int) valTipo);
			bool vResult = insDB.ExistsRecord("Adm.ConceptoBancario", "Consecutivo", dbParam.Get());
			insDB.Dispose();
			return vResult;
		}
        #endregion //Validaciones
        #region Miembros de ILibDataFKSearch
        bool ILibDataFKSearch.ConnectFk(ref XmlDocument refResulset, eProcessMessageType valType, string valProcessMessage, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            LibDataScope insDb = new LibDataScope();
            refResulset = insDb.LoadForConnect(valProcessMessage, valXmlParamsExpression, CmdTimeOut);
            if (refResulset != null && refResulset.DocumentElement != null && refResulset.DocumentElement.HasChildNodes) {
                vResult = true;
            }
            return vResult;
        }
        #endregion //Miembros de ILibDataFKSearch
        #endregion //Metodos Generados


    } //End of class clsConceptoBancarioDat

} //End of namespace Galac.Adm.Dal.Banco

