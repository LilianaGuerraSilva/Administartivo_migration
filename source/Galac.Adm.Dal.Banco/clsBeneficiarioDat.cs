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
using System.Data;

namespace Galac.Adm.Dal.Banco{
    public class clsBeneficiarioDat : LibData, ILibDataComponentWithSearch<IList<Beneficiario>, IList<Beneficiario>>, ILibDataImport, IBeneficiarioDat, ILibDataImportBulkInsert {

        #region Variables
        Beneficiario _CurrentRecord;
        #endregion //Variables

        #region Propiedades
        private Beneficiario CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        eActionImpExp ILibDataImportBulkInsert.Action { get; set; }
        #endregion //Propiedades

        #region Constructores
        public clsBeneficiarioDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores

        #region Metodos Generados
        private StringBuilder ParametrosActualizacion(Beneficiario valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("Codigo", valRecord.Codigo, 10);
            vParams.AddInString("NumeroRIF", valRecord.NumeroRIF, 20);
            vParams.AddInString("NombreBeneficiario", valRecord.NombreBeneficiario, 80);
            vParams.AddInEnum("Origen", valRecord.OrigenAsDB);
            vParams.AddInEnum("TipoDeBeneficiario", valRecord.TipoDeBeneficiarioAsDB);
            vParams.AddInString("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(Beneficiario valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

        private StringBuilder ParametrosProximoConsecutivo(Beneficiario valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }

        #region Miembros de ILibDataComponent<IList<Beneficiario>, IList<Beneficiario>>
        LibResponse ILibDataComponent<IList<Beneficiario>, IList<Beneficiario>>.CanBeChoosen(IList<Beneficiario> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Beneficiario vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase(Galac.Adm.Ccl.Banco.LibCkn.ConfigKeyForDbService);
            if (valAction == eAccionSR.Eliminar) {
                if (LibDefGen.IsProduct(LibProduct.GetInitialsSaw())) {
                    if (getParametrosCompania<string>(vRecord.ConsecutivoCompania, "BeneficiarioGenerico", this).Equals(vRecord.Consecutivo.ToString())) {
                        vSbInfo.AppendLine("Parámetros Compañía");
                    }
                } else {
                    if (insDB.ExistsValueOnMultifile("dbo.ParametrosCompania", "BeneficiarioGenerico", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Consecutivo), insDB.InsSql.ToSqlValue(vRecord.Consecutivo), true)) {
                        vSbInfo.AppendLine("Parámetros Compañía");
                    }
                }
                if (insDB.ExistsValueOnMultifile("Saw.RenglonSolicitudesDePago", "ConsecutivoBeneficiario", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Consecutivo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Renglon Solicitudes De Pago");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Beneficiario.Eliminar")]
        LibResponse ILibDataComponent<IList<Beneficiario>, IList<Beneficiario>>.Delete(IList<Beneficiario> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                if (insDb.ExistsValueOnMultifile("Saw.RenglonSolicitudesDePago", "ConsecutivoBeneficiario", "ConsecutivoCompania", insDb.InsSql.ToSqlValue(CurrentRecord.Consecutivo), insDb.InsSql.ToSqlValue(CurrentRecord.ConsecutivoCompania), true)) {
                    throw new GalacAlertException(LibResMsg.InfoForeignKeyCanNotBeDeleted("Renglon Solicitudes De Pago"));
                } else {
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "BeneficiarioDEL"), ParametrosClave(CurrentRecord, true, true));
                    insDb.Dispose();
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<Beneficiario> ILibDataComponent<IList<Beneficiario>, IList<Beneficiario>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<Beneficiario> vResult = new List<Beneficiario>();
            LibDatabase insDb = new LibDatabase(Galac.Adm.Ccl.Banco.LibCkn.ConfigKeyForDbService);
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Beneficiario>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<Beneficiario>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Beneficiario.Insertar")]
        LibResponse ILibDataComponent<IList<Beneficiario>, IList<Beneficiario>>.Insert(IList<Beneficiario> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase(Galac.Adm.Ccl.Banco.LibCkn.ConfigKeyForDbService);
                    CurrentRecord.Consecutivo = insDb.NextLngConsecutive("Saw.Beneficiario", "Consecutivo", ParametrosProximoConsecutivo(CurrentRecord));
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "BeneficiarioINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<Beneficiario>, IList<Beneficiario>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase(Galac.Adm.Ccl.Banco.LibCkn.ConfigKeyForDbService);
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoConsecutivo")) {
                        vResult = LibXml.ValueToXElement(insDb.NextLngConsecutive(DbSchema + ".Beneficiario", "Consecutivo", valParameters), "Consecutivo");
                    }
                    break;
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                case eProcessMessageType.Query:
                    vResult = LibXml.ToXElement(insDb.LoadData(valParameters.ToString(), CmdTimeOut));
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Beneficiario.Modificar")]
        LibResponse ILibDataComponent<IList<Beneficiario>, IList<Beneficiario>>.Update(IList<Beneficiario> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase(Galac.Adm.Ccl.Banco.LibCkn.ConfigKeyForDbService);
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "BeneficiarioUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<Beneficiario>, IList<Beneficiario>>.ValidateAll(IList<Beneficiario> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (Beneficiario vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<Beneficiario>, IList<Beneficiario>>.SpecializedUpdate(IList<Beneficiario> refRecord, string valSpecializedAction) {
            LibResponse vResult = new LibResponse();
            switch (valSpecializedAction) {
                case "Nomina":
                    vResult.Success = InsertaBeneficiariosDeNomina(refRecord);
                    break;
                default:
                    vResult.Success = false;
                    break;
            }
            return vResult;
        }
        #endregion //ILibDataComponent<IList<Beneficiario>, IList<Beneficiario>>

        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo);
            vResult = IsValidConsecutivo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo) && vResult;
            vResult = IsValidCodigo(valAction, CurrentRecord.Codigo) && vResult;
            vResult = IsValidNumeroRIF(valAction, CurrentRecord.NumeroRIF) && vResult;
            vResult = IsValidNombreBeneficiario(valAction, CurrentRecord.NombreBeneficiario) && vResult;
            vResult = IsValidTipoDeBeneficiario(valAction, CurrentRecord.TipoDeBeneficiarioAsEnum) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo) {
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

        private bool IsValidConsecutivo(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Insertar)) {
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

        private bool IsValidCodigo(eAccionSR valAction, string valCodigo) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigo = LibString.Trim(valCodigo);
            if (LibString.IsNullOrEmpty(valCodigo, true)) {
                BuildValidationInfo(MsgRequiredField("Código"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidNumeroRIF(eAccionSR valAction, string valNumeroRIF) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumeroRIF = LibString.Trim(valNumeroRIF);
            if (LibString.IsNullOrEmpty(valNumeroRIF, true)) {
                BuildValidationInfo(MsgRequiredField("Número RIF"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidNombreBeneficiario(eAccionSR valAction, string valNombreBeneficiario) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNombreBeneficiario = LibString.Trim(valNombreBeneficiario);
            if (LibString.IsNullOrEmpty(valNombreBeneficiario, true)) {
                BuildValidationInfo(MsgRequiredField("Nombre Beneficiario"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidTipoDeBeneficiario(eAccionSR valAction, eTipoDeBeneficiario valTipoDeBeneficiario) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivo) {
            bool vResult = false;
            Beneficiario vRecordBusqueda = new Beneficiario();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".Beneficiario", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, Beneficiario valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".Beneficiario", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Beneficiario.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Beneficiario.Importar")]
        LibXmlResult ILibDataImport.Import(XmlReader refRecord, LibProgressManager valManager, bool valShowMessage) {
            //throw new ProgrammerMissingCodeException("PROGRAMADOR: El codigo generado bajo el atributo IMPEXP del record, es solo referencial. DEBE AJUSTARLO ya que el Narrador actualmente desconoce la estructura de su archivo de importacion!!!!");
            try {
                string vMessage = "";
                int vIndex = 0;
                LibXmlResult vResult = new LibXmlResult();
                vResult.AddTitle("Importación Beneficiario");
                List<Beneficiario> vList = ParseToListEntity(refRecord);
                if (vList.Count > 0) {
                    LibDatabase insDb = new LibDatabase(Galac.Adm.Ccl.Banco.LibCkn.ConfigKeyForDbService);
                    if (((ILibDataImportBulkInsert)this).Action == eActionImpExp.eAIE_Instalar) {
                        try {
                            string vTablename = DbSchema + ".Beneficiario";
                            DataTable vDataTable = insDb.ParseListToDt<Beneficiario>(vList, vTablename, valManager);
                            valManager.ReportProgress(vList.Count, "Realizando inserción en lote, por favor espere...", string.Empty, false);
                            insDb.BulkInsert(vDataTable, vTablename);
                        } catch (System.Data.SqlClient.SqlException vEx) {
                            throw new GalacException("Error procesando los datos, debe verificar los datos a importar.", eExceptionManagementType.Controlled, vEx);
                        }
                    } else {
                        int vTotal = vList.Count;
                        foreach (Beneficiario item in vList) {
                            try {
                                vMessage = string.Format("Insertando {0:n0} de {1:n0}", vIndex, vTotal);
                                insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "BeneficiarioINST"), ParametrosActualizacion(item, eAccionSR.ReInstalar));
                            } catch (System.Data.SqlClient.SqlException vEx) {
                                if (LibExceptionMng.IsPrimaryKeyViolation(vEx)) {
                                    vResult.AddDetailWithAttribute(item.Codigo, "Ya existe", eXmlResultType.Error);
                                } else {
                                    throw;
                                }
                            }
                            if (valManager.CancellationPending) {
                                break;
                            }
                            vIndex++;
                            valManager.ReportProgress(vIndex, "Ejecutando por favor espere...", vMessage, (vIndex >= vTotal) && (valShowMessage));
                        }
                    }
                    insDb.Dispose();
                }
                return vResult;
            } catch (Exception) {
                throw;
            }
        }

        private List<Beneficiario> ParseToListEntity(XmlReader valXmlEntity) {
            List<Beneficiario> vResult = new List<Beneficiario>();
            XDocument xDoc = XDocument.Load(valXmlEntity);
            var vEntity = from vRecord in xDoc.Descendants("GpResult")
                          select vRecord;
            int vConsecutivo = 0;
            foreach (XElement vItem in vEntity) {
                Beneficiario vRecord = new Beneficiario();
                vRecord.Clear();
                vRecord.ConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
                vRecord.Consecutivo = vConsecutivo++;
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Codigo"), null))) {
                    vRecord.Codigo = vItem.Element("Codigo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroRIF"), null))) {
                    vRecord.NumeroRIF = vItem.Element("NumeroRIF").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NombreBeneficiario"), null))) {
                    vRecord.NombreBeneficiario = vItem.Element("NombreBeneficiario").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Origen"), null))) {
                    vRecord.Origen = vItem.Element("Origen").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDeBeneficiario"), null))) {
                    vRecord.TipoDeBeneficiario = vItem.Element("TipoDeBeneficiario").Value;
                }
                vRecord.NombreOperador = ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login;
                vResult.Add(vRecord);
            }
            return vResult;
        }
        #endregion //Metodos Generados

        #region Metodos Creados
        private T getParametrosCompania<T>(int valConsecutivoCompania, string ValParametro, ILibDataComponent<IList<Beneficiario>, IList<Beneficiario>> instanciaDal) {
            string vDbSchema = "";
            if (LibDefGen.IsProduct(LibProduct.GetInitialsAdmEcuador())) {
                vDbSchema = "Adme";
            } else {
                vDbSchema = "Comun";
            }
            StringBuilder sql = new StringBuilder("SELECT " + vDbSchema + ".SettValueByCompany.Value AS Valor, " + vDbSchema + ".SettValueByCompany.NameSettDefinition FROM " + vDbSchema + ".SettDefinition INNER JOIN " + vDbSchema + ".SettValueByCompany ON " + vDbSchema + ".SettDefinition.Name = " + vDbSchema + ".SettValueByCompany.NameSettDefinition WHERE (" + vDbSchema + ".SettDefinition.Name = '" + ValParametro + "') AND (" + vDbSchema + ".SettValueByCompany.ConsecutivoCompania = " + valConsecutivoCompania + ")", 300);
            XElement Auxiliar = instanciaDal.QueryInfo(eProcessMessageType.Query, null, sql);
            object vValor = LibXml.GetPropertyString(Auxiliar, "Valor");
            return (T)vValor;
        }

        private bool InsertaBeneficiariosDeNomina(IList<Beneficiario> valRecord) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase(LibCkn.ConfigKeyForDbService);
            foreach (Beneficiario item in valRecord) {
                item.Consecutivo = insDb.NextLngConsecutive("Saw.Beneficiario", "Consecutivo", ParametrosProximoConsecutivo(item));
                vResult = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "BeneficiarioInsIntegracion"), ParametrosActualizacion(item, eAccionSR.Insertar));
            }
            insDb.Dispose();
            return vResult;
        }

        private int FindConsecutivoEntidad(string valUniqueKeyFieldName, string valUniqueKeyFieldValue, int valUniqueKeyCMFValue, string valNameTable, string valNameColumn, bool valUsarUniqueKeyFieldValue) {
            StringBuilder vSqlSb = new StringBuilder();
            int vResult = 0;
            string vSqlWhere = "";
            QAdvSql insQAdvSql = new QAdvSql("");
            LibDatabase insDb = new LibDatabase(LibCkn.ConfigKeyForDbService);
            if (valUsarUniqueKeyFieldValue) {
                vSqlWhere = insQAdvSql.SqlValueWithAnd(vSqlWhere, valNameTable + "." + valUniqueKeyFieldName, valUniqueKeyFieldValue);
            }
            vSqlWhere = insQAdvSql.SqlIntValueWithAnd(vSqlWhere, valNameTable + ".ConsecutivoCompania", valUniqueKeyCMFValue);
            vSqlWhere = insQAdvSql.WhereSql(vSqlWhere);

            vSqlSb.AppendLine(" SELECT " + valNameColumn + " FROM " + valNameTable);
            vSqlSb.AppendLine(vSqlWhere);

            object vValue = insDb.ExecuteScalar(vSqlSb.ToString(), -1, false);
            if (vValue != null) {
                vResult = LibConvert.ToInt(vValue);
            }
            insDb.Dispose();
            return vResult;
        }

        private int FindConsecutivoBeneficiarioGenerico(int valConsecutivoCompania) {
            StringBuilder vSqlSb = new StringBuilder();
            int vResult = 0;
            string vSqlWhere = "";
            string vDbSchema = "";
            if (LibDefGen.ProgramInfo.IsCountryEcuador()) {
                vDbSchema = "Adme";
            } else {
                vDbSchema = "Comun";
            }
            QAdvSql insQAdvSql = new QAdvSql("");
            LibDatabase insDb = new LibDatabase(LibCkn.ConfigKeyForDbService);

            vSqlWhere = insQAdvSql.SqlValueWithAnd(vSqlWhere, "NameSettDefinition", "BeneficiarioGenerico");
            vSqlWhere = insQAdvSql.SqlIntValueWithAnd(vSqlWhere, "ConsecutivoCompania", valConsecutivoCompania);
            vSqlWhere = insQAdvSql.WhereSql(vSqlWhere);

            vSqlSb.AppendLine("SELECT Value FROM " + vDbSchema + ".SettValueByCompany ");
            vSqlSb.AppendLine(vSqlWhere);

            object vValue = insDb.ExecuteScalar(vSqlSb.ToString(), -1, false);
            if (vValue != null) {
                vResult = LibConvert.ToInt(vValue);
            }
            insDb.Dispose();
            return vResult;
        }

        int IBeneficiarioDat.BeneficiarioGenerico(int valConsecutivoCompania) {
            int vResult;
            if (ExisteTablaSettValueByCompany()) {
                vResult = FindConsecutivoBeneficiarioGenerico(valConsecutivoCompania);
            } else {
                vResult = FindConsecutivoEntidad("", "", valConsecutivoCompania, "parametrosCompania", "BeneficiarioGenerico", false);
            }
            return vResult;
        }

        int IBeneficiarioDat.BeneficiarioGenericoParaCrearEmpresa(int valConsecutivoCompania) {
            int vResult;
            vResult = FindConsecutivoEntidad("", "", valConsecutivoCompania, "Saw.Beneficiario", "Consecutivo", false);
            return vResult;
        }

        public bool ExisteTablaSettValueByCompany() {
            return new LibDbo(LibCkn.ConfigKeyForDbService).Exists("Comun.SettValueByCompany", eDboType.Tabla) || new LibDbo(LibCkn.ConfigKeyForDbService).Exists("Adme.SettValueByCompany", eDboType.Tabla);
        }
        #endregion //Metodos Creados
    } //End of class clsBeneficiarioDat

}//End of namespace Galac.Adm.Dal.Banco

