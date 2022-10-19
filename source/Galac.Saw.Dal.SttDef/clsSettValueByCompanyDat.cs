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
    public class clsSettValueByCompanyDat: LibData, ILibDataComponentWithSearch<IList<SettValueByCompany>, IList<SettValueByCompany>> {
        #region Variables
        SettValueByCompany _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private SettValueByCompany CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsSettValueByCompanyDat() {
            DbSchema = "Comun";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(SettValueByCompany valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NameSettDefinition", valRecord.NameSettDefinition, 50);
            vParams.AddInString("Value", valRecord.Value, 200);
            vParams.AddInString("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(SettValueByCompany valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NameSettDefinition", valRecord.NameSettDefinition, 50);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(SettValueByCompany valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>>

        LibResponse ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>>.CanBeChoosen(IList<SettValueByCompany> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            SettValueByCompany vRecord = refRecord[0];
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Sett Value By Company.Eliminar")]
        LibResponse ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>>.Delete(IList<SettValueByCompany> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "SettValueByCompanyDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<SettValueByCompany> ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<SettValueByCompany> vResult = new List<SettValueByCompany>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<SettValueByCompany>(valProcessMessage, valParameters, CmdTimeOut);
                    break;

                default: break;
            }
            insDb.Dispose();
            return vResult;
        }

     //   [PrincipalPermission(SecurityAction.Demand, Role = "Sett Value By Company.Insertar")]
        LibResponse ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>>.Insert(IList<SettValueByCompany> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            foreach (SettValueByCompany item in refRecord) {
                if (ExecuteProcessBeforeInsert()) {
                    CurrentRecord = item;
                    if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                        try {
                            LibDatabase insDb = new LibDatabase();
                            vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "SettValueByCompanyINS"), ParametrosActualizacion(item, eAccionSR.Insertar));
                            insDb.Dispose();
                        } catch (System.Data.SqlClient.SqlException vEx) {

                        } catch (GalacException valException) {

                        }
                    }
                }
            }
           
            return vResult;
        }

        XElement ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                case eProcessMessageType.Query:
                    vResult = LibXml.ToXElement(insDb.LoadData(valProcessMessage, CmdTimeOut, valParameters));                    
                    break;
                default: break;
            }
            insDb.Dispose();
            return vResult;
        }


        //[PrincipalPermission(SecurityAction.Demand, Role = "Sett Value By Company.Modificar")]
        LibResponse ILibDataComponent<IList<SettValueByCompany>,IList<SettValueByCompany>>.Update(IList<SettValueByCompany> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            //string tira = "";
            //List<string> lista = new List<string>();
            //CurrentRecord = refRecord[0];
            try {
                foreach(var vCurrentRecord in refRecord) {
                    CurrentRecord = vCurrentRecord;
                    if(ExecuteProcessBeforeUpdate()) {
                        if(Validate(eAccionSR.Modificar,out vErrMsg)) {
                            LibDatabase insDb = new LibDatabase();
                            //tira = CurrentRecord.NameSettDefinition;
                            //lista.Add(tira);
                            vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema,"SettValueByCompanyUPD"),ParametrosActualizacion(CurrentRecord,eAccionSR.Modificar));
                            insDb.Dispose();
                        } else {
                            throw new GalacValidationException(vErrMsg);
                        }
                    }
                }
                return vResult;
            } catch(Exception vEx) {
                if(LibString.S1IsInS2("El registro actual ha sido MODIFICADO o ELIMINADO por otro usuario.\r\nLa actualización será cancelada, para ver los cambios vuelva a escoger.",vEx.Message)) {
                    throw new GalacException("El registro '" + CurrentRecord.NameSettDefinition + "' no se encuentra en el archivo de tablas SettDefinition.txt.\r\nPor favor, comuníquese con Soporte Gálac Software.",eExceptionManagementType.Controlled);
                } else {
                    throw vEx;
                }
            }
        }

        bool ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>>.ValidateAll(IList<SettValueByCompany> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (SettValueByCompany vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>>.SpecializedUpdate(IList<SettValueByCompany> refRecord, string valSpecializedAction) {            
            LibResponse vResult = new LibResponse();
            if(LibString.S1IsEqualToS2(valSpecializedAction, "ActualizaValor")) {
                vResult = ActualizaValorSettings(refRecord);
            }
            return vResult;
        }
        #endregion //ILibDataComponent<IList<SettValueByCompany>, IList<SettValueByCompany>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.NameSettDefinition);
            vResult = IsValidNameSettDefinition(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.NameSettDefinition) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, string valNameSettDefinition){
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

        private bool IsValidNameSettDefinition(eAccionSR valAction, int valConsecutivoCompania, string valNameSettDefinition){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNameSettDefinition = LibString.Trim(valNameSettDefinition);
            if (LibString.IsNullOrEmpty(valNameSettDefinition, true)) {
                BuildValidationInfo(MsgRequiredField("Name Sett Definition"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valNameSettDefinition)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Name Sett Definition", valNameSettDefinition));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valNameSettDefinition) {
            bool vResult = false;
            SettValueByCompany vRecordBusqueda = new SettValueByCompany();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.NameSettDefinition = valNameSettDefinition;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord("Comun.SettValueByCompany", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
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

        private LibResponse ActualizaValorSettings(IList<SettValueByCompany> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            vResult.Success = true;
            foreach(var vRecord in refRecord) {
                CurrentRecord = vRecord;
                if(Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = vResult.Success && insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "SettValueByCompanyUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.ModificarEspecial));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        

    } //End of class clsSettValueByCompanyDat

} //End of namespace Galac.Saw.Dal.SttDef

