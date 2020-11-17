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
    public class clsColorDat: LibData, ILibDataComponentWithSearch<IList<Color>, IList<Color>> {
        #region Variables
        Color _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private Color CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsColorDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(Color valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoColor", valRecord.CodigoColor, 3);
            vParams.AddInString("DescripcionColor", valRecord.DescripcionColor, 20);
            vParams.AddInString("CodigoLote", valRecord.CodigoLote, 10);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInString("CodigoColorOriginal", valRecord.CodigoColorOriginal, 3);
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(Color valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoColor", valRecord.CodigoColor, 3);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(Color valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<Color>, IList<Color>>

        LibResponse ILibDataComponent<IList<Color>, IList<Color>>.CanBeChoosen(IList<Color> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Color vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (vSbInfo.Length == 0) {
                    vSbInfo = ValidateFK(vRecord.ConsecutivoCompania, vRecord.CodigoColor);
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Color.Eliminar")]
        LibResponse ILibDataComponent<IList<Color>, IList<Color>>.Delete(IList<Color> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ColorDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<Color> ILibDataComponent<IList<Color>, IList<Color>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<Color> vResult = new List<Color>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Color>(valProcessMessage, valParameters, CmdTimeOut);
                    foreach (var vColor in vResult) {
                        vColor.CodigoColorOriginal = vColor.CodigoColor;
                    }                  
                    
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Color.Insertar")]
        LibResponse ILibDataComponent<IList<Color>, IList<Color>>.Insert(IList<Color> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ColorINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<Color>, IList<Color>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Color.Modificar")]
        LibResponse ILibDataComponent<IList<Color>, IList<Color>>.Update(IList<Color> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ColorUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<Color>, IList<Color>>.ValidateAll(IList<Color> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (Color vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<Color>, IList<Color>>.SpecializedUpdate(IList<Color> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<Color>, IList<Color>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania);
            vResult = IsValidCodigoColor(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.CodigoColor, CurrentRecord.CodigoColorOriginal) && vResult;
            vResult = IsValidDescripcionColor(valAction, CurrentRecord.DescripcionColor) && vResult;
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

        private bool IsValidCodigoColor(eAccionSR valAction, int valConsecutivoCompania, string valCodigoColor, string valCodigoColorOriginal) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoColor = LibString.Trim(valCodigoColor);
            if (LibString.IsNullOrEmpty(valCodigoColor, true)) {
                BuildValidationInfo(MsgRequiredField("Código Color"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valCodigoColor)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Código Color", valCodigoColor));
                    vResult = false;
                }
            } else if (valAction == eAccionSR.Eliminar) {
                StringBuilder vSbInfo = new StringBuilder();
                vSbInfo = ValidateFK(valConsecutivoCompania, valCodigoColor);
                if (vSbInfo != null && vSbInfo.Length > 0) {
                    BuildValidationInfo(LibResMsg.InfoForeignKeyCanNotBeDeleted(vSbInfo.ToString()));
                    vResult = false;
                }
            } else if (valAction == eAccionSR.Modificar) {
                StringBuilder vSbInfo = new StringBuilder();
                if (valCodigoColor != valCodigoColorOriginal) {
                    if (KeyExists(valConsecutivoCompania, valCodigoColor)) {
                        BuildValidationInfo(MsgFieldValueAlreadyExist("CodigoColor", valCodigoColor));
                        vResult = false;
                    } else {
                        vSbInfo = ValidateFK(valConsecutivoCompania, valCodigoColorOriginal);
                    }
                }
                if (vSbInfo != null && vSbInfo.Length > 0) {
                    BuildValidationInfo(LibResMsg.InfoForeignKeyCanNotBeDeleted(vSbInfo.ToString()));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidDescripcionColor(eAccionSR valAction, string valDescripcionColor) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibText.IsNullOrEmpty(valDescripcionColor)) {
                BuildValidationInfo(MsgRequiredField("Descripción Color"));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valCodigoColor) {
            bool vResult = false;
            Color vRecordBusqueda = new Color();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.CodigoColor = valCodigoColor;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".Color", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
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
            if (insDB.ExistsValueOnMultifile("dbo.ExistenciaPorGrupo", "CodigoColor", "ConsecutivoCompania", vFieldSQLValue, insDB.InsSql.ToSqlValue(valConsecutivoCompania), (vIsAdmInt || vIsSaw || vIsAdmEc))) {
                vResult.AppendLine("Existencia Por Grupo");
            }
            if (insDB.ExistsValueOnMultifile("dbo.RenglonGrupoColor", "CodigoColor", "ConsecutivoCompania", vFieldSQLValue, insDB.InsSql.ToSqlValue(valConsecutivoCompania), (vIsAdmInt || vIsSaw || vIsAdmEc))) {
                vResult.AppendLine("Renglon Grupo Color");
            }
            if (insDB.ExistsValueOnMultifile(DbSchema + ".Vehiculo", "CodigoColor", "ConsecutivoCompania", vFieldSQLValue, insDB.InsSql.ToSqlValue(valConsecutivoCompania), ( vIsSaw ))) {
                vResult.AppendLine("Vehiculo");
            }
            insDB.Dispose();
            return vResult;
        }

    } //End of class clsColorDat

} //End of namespace Galac.Saw.Dal.Inventario

