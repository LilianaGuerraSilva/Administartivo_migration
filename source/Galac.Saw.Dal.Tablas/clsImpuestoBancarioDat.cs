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
using System.Data;

namespace Galac.Saw.Dal.Tablas {
    public class clsImpuestoBancarioDat: LibData, ILibDataComponentWithSearch<IList<ImpuestoBancario>, IList<ImpuestoBancario>>, ILibDataImport, ILibDataImportBulkInsert {
        #region Variables
        ImpuestoBancario _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private ImpuestoBancario CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        eActionImpExp ILibDataImportBulkInsert.Action { get; set; }
        #endregion //Propiedades
        #region Constructores

        public clsImpuestoBancarioDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(ImpuestoBancario valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInDateTime("FechaDeInicioDeVigencia", valRecord.FechaDeInicioDeVigencia);
            vParams.AddInDecimal("AlicuotaAlDebito", valRecord.AlicuotaAlDebito, 2);
            vParams.AddInDecimal("AlicuotaAlCredito", valRecord.AlicuotaAlCredito, 2);
            vParams.AddInDecimal("AlicuotaC1Al4", valRecord.AlicuotaC1Al4, 2);
            vParams.AddInDecimal("AlicuotaC5", valRecord.AlicuotaC5, 2);
            vParams.AddInDecimal("AlicuotaC6", valRecord.AlicuotaC6, 2);
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(ImpuestoBancario valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInDateTime("FechaDeInicioDeVigencia", valRecord.FechaDeInicioDeVigencia);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<ImpuestoBancario>, IList<ImpuestoBancario>>

        LibResponse ILibDataComponent<IList<ImpuestoBancario>, IList<ImpuestoBancario>>.CanBeChoosen(IList<ImpuestoBancario> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            ImpuestoBancario vRecord = refRecord[0];
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
        LibResponse ILibDataComponent<IList<ImpuestoBancario>, IList<ImpuestoBancario>>.Delete(IList<ImpuestoBancario> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema,"ImpTransacBancariasDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<ImpuestoBancario> ILibDataComponent<IList<ImpuestoBancario>, IList<ImpuestoBancario>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<ImpuestoBancario> vResult = new List<ImpuestoBancario>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<ImpuestoBancario>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<ImpuestoBancario>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        LibResponse ILibDataComponent<IList<ImpuestoBancario>, IList<ImpuestoBancario>>.Insert(IList<ImpuestoBancario> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema,"ImpTransacBancariasINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<ImpuestoBancario>, IList<ImpuestoBancario>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
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
        LibResponse ILibDataComponent<IList<ImpuestoBancario>, IList<ImpuestoBancario>>.Update(IList<ImpuestoBancario> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema,"ImpTransacBancariasUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<ImpuestoBancario>, IList<ImpuestoBancario>>.ValidateAll(IList<ImpuestoBancario> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (ImpuestoBancario vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<ImpuestoBancario>, IList<ImpuestoBancario>>.SpecializedUpdate(IList<ImpuestoBancario> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<ImpuestoBancario>, IList<ImpuestoBancario>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidFechaDeInicioDeVigencia(valAction, CurrentRecord.FechaDeInicioDeVigencia);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidFechaDeInicioDeVigencia(eAccionSR valAction, DateTime valFechaDeInicioDeVigencia){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaDeInicioDeVigencia, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(DateTime valFechaDeInicioDeVigencia) {
            bool vResult = false;
            ImpuestoBancario vRecordBusqueda = new ImpuestoBancario();
            vRecordBusqueda.FechaDeInicioDeVigencia = valFechaDeInicioDeVigencia;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".ImpTransacBancarias", "FechaDeInicioDeVigencia", ParametrosClave(vRecordBusqueda, false, false));
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Importar")]
        LibXmlResult ILibDataImport.Import(XmlReader refRecord, LibProgressManager valManager, bool valShowMessage) {
            //throw new ProgrammerMissingCodeException("PROGRAMADOR: El codigo generado bajo el atributo IMPEXP del record, es solo referencial. DEBE AJUSTARLO ya que el Narrador actualmente desconoce la estructura de su archivo de importacion!!!!");
            try {
                string vMessage = "";
                int vIndex = 0;
                LibXmlResult vResult = new LibXmlResult();
                vResult.AddTitle("Importación Alícuota ITF");
                List<ImpuestoBancario> vList = ParseToListEntity(refRecord);
                if (vList.Count > 0) {
                    LibDatabase insDb = new LibDatabase();
                    if (((ILibDataImportBulkInsert)this).Action == eActionImpExp.eAIE_Instalar) {
                        try {
                            string vTablename = DbSchema + ".ImpTransacBancarias";
                            DataTable vDataTable = insDb.ParseListToDt<ImpuestoBancario>(vList, vTablename, valManager);
                            valManager.ReportProgress(vList.Count, "Realizando inserción en lote, por favor espere...", string.Empty, false);
                            insDb.BulkInsert(vDataTable, vTablename);
                        } catch (System.Data.SqlClient.SqlException vEx) {
                            throw new GalacException("Error procesando los datos, debe verificar los datos a importar.", eExceptionManagementType.Controlled, vEx);
                        }
                    } else {
                        int vTotal = vList.Count;
                        foreach (ImpuestoBancario item in vList) {
                            try {
                                vMessage = string.Format("Insertando {0:n0} de {1:n0}", vIndex, vTotal);
                                insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ImpTransacBancariasINST"), ParametrosActualizacion(item, eAccionSR.ReInstalar));
                            } catch (System.Data.SqlClient.SqlException vEx) {
                                if (LibExceptionMng.IsPrimaryKeyViolation(vEx)) {
                                    vResult.AddDetailWithAttribute(LibConvert.ToStr(item.FechaDeInicioDeVigencia, "dd/MM/yyyy"), "Ya existe", eXmlResultType.Error);
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

        private List<ImpuestoBancario> ParseToListEntity(XmlReader valXmlEntity) {
            List<ImpuestoBancario> vResult = new List<ImpuestoBancario>();
            XDocument xDoc = XDocument.Load(valXmlEntity);
            var vEntity = from vRecord in xDoc.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                ImpuestoBancario vRecord = new ImpuestoBancario();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaDeInicioDeVigencia"), null))) {
                    vRecord.FechaDeInicioDeVigencia = LibConvert.ToDate(vItem.Element("FechaDeInicioDeVigencia"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AlicuotaAlDebito"), null))) {
                    vRecord.AlicuotaAlDebito = LibConvert.ToDec(vItem.Element("AlicuotaAlDebito"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AlicuotaAlCredito"), null))) {
                    vRecord.AlicuotaAlCredito = LibConvert.ToDec(vItem.Element("AlicuotaAlCredito"));
                }
				if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AlicuotaC1Al4"), null))) {
                    vRecord.AlicuotaC1Al4 = LibConvert.ToDec(vItem.Element("AlicuotaC1Al4"));
                }
				if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AlicuotaC5"), null))) {
                    vRecord.AlicuotaC5 = LibConvert.ToDec(vItem.Element("AlicuotaC5"));
                }
				if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AlicuotaC6"), null))) {
                    vRecord.AlicuotaC6 = LibConvert.ToDec(vItem.Element("AlicuotaC6"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsImpuestoBancarioDat

} //End of namespace Galac.Saw.Dal.Tablas

