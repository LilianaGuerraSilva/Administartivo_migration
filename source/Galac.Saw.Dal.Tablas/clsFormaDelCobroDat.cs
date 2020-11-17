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

namespace Galac.Saw.Dal.Tablas {
    public class clsFormaDelCobroDat : LibData, ILibDataComponentWithSearch<IList<FormaDelCobro>, IList<FormaDelCobro>> {
        #region Variables
        FormaDelCobro _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private FormaDelCobro CurrentRecord {
            get {
                return _CurrentRecord;
            }
            set {
                _CurrentRecord = value;
            }
        }
        #endregion //Propiedades
        #region Constructores

        public clsFormaDelCobroDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(FormaDelCobro valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInString("Codigo", valRecord.Codigo, 5);
            vParams.AddInString("Nombre", valRecord.Nombre, 50);
            vParams.AddInEnum("TipoDePago", valRecord.TipoDePagoAsDB);
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(FormaDelCobro valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInString("Codigo", valRecord.Codigo, 5);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<FormaDelCobro>, IList<FormaDelCobro>>

        LibResponse ILibDataComponent<IList<FormaDelCobro>, IList<FormaDelCobro>>.CanBeChoosen(IList<FormaDelCobro> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            FormaDelCobro vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValue("dbo.renglonCobroDeFactura", "CodigoFormaDelCobro", insDB.InsSql.ToSqlValue(vRecord.Codigo), true)) {
                    vSbInfo.AppendLine("Renglon Cobro de Factura");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Eliminar")]
        LibResponse ILibDataComponent<IList<FormaDelCobro>, IList<FormaDelCobro>>.Delete(IList<FormaDelCobro> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "FormaDelCobroDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<FormaDelCobro> ILibDataComponent<IList<FormaDelCobro>, IList<FormaDelCobro>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<FormaDelCobro> vResult = new List<FormaDelCobro>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<FormaDelCobro>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<FormaDelCobro>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default:
                    throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        LibResponse ILibDataComponent<IList<FormaDelCobro>, IList<FormaDelCobro>>.Insert(IList<FormaDelCobro> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "FormaDelCobroINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<FormaDelCobro>, IList<FormaDelCobro>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoCodigo")) {
                        vResult = LibXml.ValueToXElement(insDb.NextStrConsecutive(DbSchema + ".FormaDelCobro", "Codigo", valParameters, true, 5), "Codigo");
                    }
                    break;
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                case eProcessMessageType.Query:
                    vResult = LibXml.ToXElement(insDb.LoadData(valProcessMessage, CmdTimeOut, valParameters));
                    break;
                default:
                    throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Modificar")]
        LibResponse ILibDataComponent<IList<FormaDelCobro>, IList<FormaDelCobro>>.Update(IList<FormaDelCobro> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            try {
                if (ExecuteProcessBeforeUpdate()) {
                    if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                        LibDatabase insDb = new LibDatabase();
                        vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "FormaDelCobroUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                        insDb.Dispose();
                    } else {
                        throw new GalacValidationException(vErrMsg);
                    }
                }
            } catch (GalacAlertException ex) {
                vErrMsg = ex.Message;
                if (LibText.S1IsInS2("Ya existe la clave", ex.Message))
                    vErrMsg = "Ya existe el código: " + CurrentRecord.Codigo;
                throw new GalacAlertException(vErrMsg);
            }
            return vResult;
        }

        bool ILibDataComponent<IList<FormaDelCobro>, IList<FormaDelCobro>>.ValidateAll(IList<FormaDelCobro> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (FormaDelCobro vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<FormaDelCobro>, IList<FormaDelCobro>>.SpecializedUpdate(IList<FormaDelCobro> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<FormaDelCobro>, IList<FormaDelCobro>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigo(valAction, CurrentRecord.Codigo);
            outErrorMessage = Information.ToString();
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
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valCodigo)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Código", valCodigo));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool KeyExists(string valCodigo) {
            bool vResult = false;
            FormaDelCobro vRecordBusqueda = new FormaDelCobro();
            vRecordBusqueda.Codigo = valCodigo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".FormaDelCobro", "Codigo", ParametrosClave(vRecordBusqueda, false, false));
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


    } //End of class clsFormaDelCobroDat

} //End of namespace Galac.Saw.Dal.Tablas

