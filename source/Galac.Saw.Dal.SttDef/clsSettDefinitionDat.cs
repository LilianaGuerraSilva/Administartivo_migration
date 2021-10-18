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
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Dal.SttDef {
    public class clsSettDefinitionDat : LibData, ILibDataComponentWithSearch<IList<SettDefinition>, IList<SettDefinition>>, ILibDataImport {
        #region Variables
        SettDefinition _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private SettDefinition CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsSettDefinitionDat() {
            DbSchema = "Comun";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(SettDefinition valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInString("Name", valRecord.Name, 50);
            vParams.AddInString("Module", valRecord.Module, 50);
            vParams.AddInInteger("LevelModule", valRecord.LevelModule);
            vParams.AddInString("GroupName", valRecord.GroupName, 50);
            vParams.AddInInteger("LevelGroup", valRecord.LevelGroup);
            vParams.AddInString("Label", valRecord.Label, 50);
            vParams.AddInEnum("DataType", valRecord.DataTypeAsDB);
            vParams.AddInString("Validationrules", valRecord.Validationrules, 300);
            vParams.AddInBoolean("IsSetForAllEnterprise", valRecord.IsSetForAllEnterpriseAsBool);
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(SettDefinition valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInString("Name", valRecord.Name, 50);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<SettDefinition>, IList<SettDefinition>>

        LibResponse ILibDataComponent<IList<SettDefinition>, IList<SettDefinition>>.CanBeChoosen(IList<SettDefinition> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            SettDefinition vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValue("Comun.SettValueByCompany", "NameSettDefinition", insDB.InsSql.ToSqlValue(vRecord.Name), true)) {
                    vSbInfo.AppendLine("Sett Value By Company");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Sett Definition.Eliminar")]
        LibResponse ILibDataComponent<IList<SettDefinition>, IList<SettDefinition>>.Delete(IList<SettDefinition> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "SettDefinitionDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<SettDefinition> ILibDataComponent<IList<SettDefinition>, IList<SettDefinition>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<SettDefinition> vResult = new List<SettDefinition>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<SettDefinition>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                default: break;
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Sett Definition.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Compañía.Insertar")]
        LibResponse ILibDataComponent<IList<SettDefinition>, IList<SettDefinition>>.Insert(IList<SettDefinition> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "SettDefinitionINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<SettDefinition>, IList<SettDefinition>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                case eProcessMessageType.Query :
                    vResult = LibXml.ToXElement(insDb.LoadData(valProcessMessage, CmdTimeOut, valParameters));
                    break;
                default: break;

            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Sett Definition.Modificar")]
        LibResponse ILibDataComponent<IList<SettDefinition>, IList<SettDefinition>>.Update(IList<SettDefinition> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "SettDefinitionUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<SettDefinition>, IList<SettDefinition>>.ValidateAll(IList<SettDefinition> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (SettDefinition vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<SettDefinition>, IList<SettDefinition>>.SpecializedUpdate(IList<SettDefinition> refRecord, string valSpecializedAction) {
            throw new NotImplementedException();
        }
        #endregion //ILibDataComponent<IList<SettDefinition>, IList<SettDefinition>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidName(valAction, CurrentRecord.Name);
            vResult = IsValidLevelModule(valAction, CurrentRecord.LevelModule) && vResult;
            vResult = IsValidGroupName(valAction, CurrentRecord.GroupName) && vResult;
            vResult = IsValidLevelGroup(valAction, CurrentRecord.LevelGroup) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidName(eAccionSR valAction, string valName){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valName = LibString.Trim(valName);
            if (LibString.IsNullOrEmpty(valName, true)) {
                BuildValidationInfo(MsgRequiredField("Name"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valName)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Name", valName));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidLevelModule(eAccionSR valAction, int valLevelModule){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valLevelModule == 0) {
                BuildValidationInfo(MsgRequiredField("Nivel Modulo"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidGroupName(eAccionSR valAction, string valGroupName){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valGroupName = LibString.Trim(valGroupName);
            if (LibString.IsNullOrEmpty(valGroupName, true)) {
                BuildValidationInfo(MsgRequiredField("Group Name"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidLevelGroup(eAccionSR valAction, int valLevelGroup){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valLevelGroup == 0) {
                BuildValidationInfo(MsgRequiredField("Nivel del Grupo"));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(string valName) {
            bool vResult = false;
            SettDefinition vRecordBusqueda = new SettDefinition();
            vRecordBusqueda.Name = valName;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord("Comun.SettDefinition", "Name", ParametrosClave(vRecordBusqueda, false, false));
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

        IList<SettDefinition> ParseToListEntity(XmlReader valXmlEntityReader, eAccionSR valAction) {
            IList<SettDefinition> vResult = new List<SettDefinition>();
            SettDefinition recTmp;
            XDocument xDoc = XDocument.Load(valXmlEntityReader);
            var vEntity = from vRecord in xDoc.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                recTmp = new SettDefinition();
                if(!(System.NullReferenceException.ReferenceEquals(vItem.Element("Name"), null))) {
                   recTmp.Name = vItem.Element("Name").Value;
                };
                if(!(System.NullReferenceException.ReferenceEquals(vItem.Element("Module"), null))) {
                   recTmp.Module = vItem.Element("Module").Value;
                };
                if(!(System.NullReferenceException.ReferenceEquals(vItem.Element("LevelModule"), null))) {
                   recTmp.LevelModule = LibConvert.ToInt(vItem.Element("LevelModule"));
                };
                if(!(System.NullReferenceException.ReferenceEquals(vItem.Element("GroupName"), null))) {
                   recTmp.GroupName = vItem.Element("GroupName").Value;
                };
                if(!(System.NullReferenceException.ReferenceEquals(vItem.Element("LevelGroup"), null))) {
                   recTmp.LevelGroup = LibConvert.ToInt(vItem.Element("LevelGroup").Value);
                };
                if(!(System.NullReferenceException.ReferenceEquals(vItem.Element("Label"), null))) {
                   recTmp.Label = vItem.Element("Label").Value;
                };
                if(!(System.NullReferenceException.ReferenceEquals(vItem.Element("Datatype"), null))) {
                   recTmp.DataTypeAsEnum = TipoDeDatoDeParametros(vItem.Element("Datatype").Value);
                };
                if(!(System.NullReferenceException.ReferenceEquals(vItem.Element("Validationrules"), null))) {
                   recTmp.Validationrules = vItem.Element("Validationrules").Value;
                };
                if(!(System.NullReferenceException.ReferenceEquals(vItem.Element("Validationrules"), null))) {
                   recTmp.IsSetForAllEnterpriseAsBool = LibConvert.SNToBool(vItem.Element("IsSetForAllEnterprise"));
                };
                vResult.Add(recTmp);
            };
            return vResult;
        }

        LibXmlResult ILibDataImport.Import(XmlReader refRecord, LibProgressManager valManager, bool valShowMessage) {
            try {
                string vMessage = "";
                int vIndex = 0;
                LibXmlResult vResult = new LibXmlResult();
                vResult.AddTitle("Configuracion");
                IList<SettDefinition> vRecord = ParseToListEntity(refRecord, eAccionSR.Insertar);
                LibDatabase insDb = new LibDatabase();
                int vTotal = vRecord.Count();
              
                foreach (SettDefinition item in vRecord) {
                    try {
                        vMessage = string.Format("Insertando {0:n0} de {1:n0}", vIndex, vTotal);
                        LibDbo insDbo = new LibDbo();
                        if (insDbo.Exists(DbSchema + ".Gp_SettDefinitionINST", eDboType.Procedimiento)) {
                            insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "SettDefinitionINST"), ParametrosActualizacion(item, eAccionSR.Insertar));
                        }
                    } catch (System.Data.SqlClient.SqlException vEx) {
                        if (LibExceptionMng.IsPrimaryKeyViolation(vEx)) {
                            vResult.AddDetailWithAttribute(LibConvert.ToStr(item.Name), "Ya existe", eXmlResultType.Error);
                        } else {
                            throw;
                        }
                    } catch (GalacException valException) {
                        if (LibString.S1IsInS2("Ya existe la clave única.", valException.Message)) {
                            vResult.AddDetailWithAttribute(item.Name, "Ya existe", eXmlResultType.Error);
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
                insDb.Dispose();
                return vResult;
            } catch (Exception) {
                throw;
            }
        }
        private Saw.Ccl.SttDef.eTipoDeDatoParametros TipoDeDatoDeParametros(string  valDatatype) {
            Saw.Ccl.SttDef.eTipoDeDatoParametros vResult = Saw.Ccl.SttDef.eTipoDeDatoParametros.String;

            switch (valDatatype) {
              
                case"Decimal":
                    vResult = Saw.Ccl.SttDef.eTipoDeDatoParametros.Decimal;
                    break;
                case "Enumerativo":
                    vResult = Saw.Ccl.SttDef.eTipoDeDatoParametros.Enumerativo;
                    break;
                case "Int":
                    vResult = Saw.Ccl.SttDef.eTipoDeDatoParametros.Int;
                    break;
                case "String":
                    vResult = Saw.Ccl.SttDef.eTipoDeDatoParametros.String;
                    break;
                default:
                    vResult = Saw.Ccl.SttDef.eTipoDeDatoParametros.String;
                    break;
            }
            return vResult;
        }
    } //End of class clsSettDefinitionDat

} //End of namespace Galac.Saw.Dal.PrdStt

