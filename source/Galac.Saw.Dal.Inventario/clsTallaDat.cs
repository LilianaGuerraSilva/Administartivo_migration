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
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Dal.Inventario {
    public class clsTallaDat: LibData, ILibDataComponentWithSearch<IList<Talla>, IList<Talla>> {
        #region Variables
        Talla _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private Talla CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsTallaDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(Talla valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoTalla", valRecord.CodigoTalla, 3);
            vParams.AddInString("DescripcionTalla", valRecord.DescripcionTalla, 20);
            vParams.AddInString("CodigoLote", valRecord.CodigoLote, 10);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInString("CodigoTallaOriginal", valRecord.CodigoTallaOriginal, 3);
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(Talla valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoTalla", valRecord.CodigoTalla, 3);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(Talla valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<Talla>, IList<Talla>>

        LibResponse ILibDataComponent<IList<Talla>, IList<Talla>>.CanBeChoosen(IList<Talla> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Talla vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                vSbInfo = ValidateFK(vRecord.ConsecutivoCompania, vRecord.CodigoTalla);
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Talla.Eliminar")]
        LibResponse ILibDataComponent<IList<Talla>, IList<Talla>>.Delete(IList<Talla> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "TallaDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<Talla> ILibDataComponent<IList<Talla>, IList<Talla>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<Talla> vResult = new List<Talla>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Talla>(valProcessMessage, valParameters, CmdTimeOut);
                    foreach (var vColor in vResult) {
                        vColor.CodigoTallaOriginal = vColor.CodigoTalla;
                    }                  
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Talla.Insertar")]
        LibResponse ILibDataComponent<IList<Talla>, IList<Talla>>.Insert(IList<Talla> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "TallaINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<Talla>, IList<Talla>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Talla.Modificar")]
        LibResponse ILibDataComponent<IList<Talla>, IList<Talla>>.Update(IList<Talla> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "TallaUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<Talla>, IList<Talla>>.ValidateAll(IList<Talla> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (Talla vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<Talla>, IList<Talla>>.SpecializedUpdate(IList<Talla> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<Talla>, IList<Talla>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania);
            vResult = IsValidCodigoTalla(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.CodigoTalla, CurrentRecord.CodigoTallaOriginal) && vResult;
            vResult = IsValidDescripcionTalla(valAction, CurrentRecord.DescripcionTalla) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania){
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

        private bool IsValidCodigoTalla(eAccionSR valAction, int valConsecutivoCompania, string valCodigoTalla, string valCodigoTallaOriginal) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoTalla = LibString.Trim(valCodigoTalla);
            if (LibString.IsNullOrEmpty(valCodigoTalla, true)) {
                BuildValidationInfo(MsgRequiredField("Código Talla"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valCodigoTalla)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Código Talla", valCodigoTalla));
                    vResult = false;
                }
            } else if (valAction == eAccionSR.Eliminar) {
                StringBuilder vSbInfo = new StringBuilder();
                vSbInfo = ValidateFK(valConsecutivoCompania, valCodigoTalla);
                if (vSbInfo != null && vSbInfo.Length > 0) {
                    BuildValidationInfo(LibResMsg.InfoForeignKeyCanNotBeDeleted(vSbInfo.ToString()));
                    vResult = false;
                }
            } else if (valAction == eAccionSR.Modificar) {
                StringBuilder vSbInfo = new StringBuilder();
                if (valCodigoTalla != valCodigoTallaOriginal) {
                    if (KeyExists(valConsecutivoCompania, valCodigoTalla)) {
                        BuildValidationInfo(MsgFieldValueAlreadyExist("Código Talla", valCodigoTalla));
                        vResult = false;
                    } else {
                        vSbInfo = ValidateFK(valConsecutivoCompania, valCodigoTallaOriginal);
                    }
                }
                if (vSbInfo != null && vSbInfo.Length > 0) {
                    BuildValidationInfo(LibResMsg.InfoForeignKeyCanNotBeDeleted(vSbInfo.ToString()));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidDescripcionTalla(eAccionSR valAction, string valDescripcionTalla) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibText.IsNullOrEmpty(valDescripcionTalla)) {
                BuildValidationInfo(MsgRequiredField("Descripción Talla"));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valCodigoTalla) {
            bool vResult = false;
            Talla vRecordBusqueda = new Talla();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.CodigoTalla = valCodigoTalla;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".Talla", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
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

        private StringBuilder ValidateFK(int valConsecutivoCompania, string valValue) {
            LibDatabase insDB = new LibDatabase();
            StringBuilder vResult = new StringBuilder();
            string vFieldSQLValue = insDB.InsSql.ToSqlValue(valValue);
            bool vIsAdmInt = LibDefGen.IsProduct(LibProduct.GetInitialsAdmInterno());
            bool vIsSaw = LibDefGen.IsProduct(LibProduct.GetInitialsSaw());
            bool vIsAdmEc = LibDefGen.IsProduct(LibProduct.GetInitialsAdmEcuador());
            if (insDB.ExistsValueOnMultifile("dbo.ExistenciaPorGrupo", "CodigoTalla", "ConsecutivoCompania", vFieldSQLValue, insDB.InsSql.ToSqlValue(valConsecutivoCompania), (vIsAdmInt || vIsSaw || vIsAdmEc))) {
                vResult.AppendLine("Existencia Por Grupo");
            }
            if (insDB.ExistsValueOnMultifile("dbo.RenglonGrupoTalla", "CodigoTalla", "ConsecutivoCompania", vFieldSQLValue, insDB.InsSql.ToSqlValue(valConsecutivoCompania), (vIsAdmInt || vIsSaw || vIsAdmEc))) {
                vResult.AppendLine("Renglon Grupo Talla");
            }
            insDB.Dispose();
            return vResult;
        }

    } //End of class clsTallaDat

} //End of namespace Galac.Saw.Dal.Inventario

