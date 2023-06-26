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
    public class clsRutaDeComercializacionDat: LibData, ILibDataComponentWithSearch<IList<RutaDeComercializacion>, IList<RutaDeComercializacion>> {
        #region Variables
        RutaDeComercializacion _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private RutaDeComercializacion CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsRutaDeComercializacionDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(RutaDeComercializacion valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("Descripcion", valRecord.Descripcion, 100);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(RutaDeComercializacion valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

        private StringBuilder ParametrosProximoConsecutivo(RutaDeComercializacion valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<RutaDeComercializacion>, IList<RutaDeComercializacion>>

        LibResponse ILibDataComponent<IList<RutaDeComercializacion>, IList<RutaDeComercializacion>>.CanBeChoosen(IList<RutaDeComercializacion> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            RutaDeComercializacion vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValueOnMultifile("Adm.Vendedor", "ConsecutivoRutaDeComercializacion", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Consecutivo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Vendedor");
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
        LibResponse ILibDataComponent<IList<RutaDeComercializacion>, IList<RutaDeComercializacion>>.Delete(IList<RutaDeComercializacion> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "RutaDeComercializacionDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<RutaDeComercializacion> ILibDataComponent<IList<RutaDeComercializacion>, IList<RutaDeComercializacion>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<RutaDeComercializacion> vResult = new List<RutaDeComercializacion>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<RutaDeComercializacion>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<RutaDeComercializacion>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default:
                    throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Compañía.Insertar")]
        LibResponse ILibDataComponent<IList<RutaDeComercializacion>, IList<RutaDeComercializacion>>.Insert(IList<RutaDeComercializacion> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "RutaDeComercializacionINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<RutaDeComercializacion>, IList<RutaDeComercializacion>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoConsecutivo")) {
                        vResult = LibXml.ValueToXElement(insDb.NextLngConsecutive(DbSchema + ".RutaDeComercializacion", "Consecutivo", valParameters), "Consecutivo");
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
        LibResponse ILibDataComponent<IList<RutaDeComercializacion>, IList<RutaDeComercializacion>>.Update(IList<RutaDeComercializacion> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "RutaDeComercializacionUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<RutaDeComercializacion>, IList<RutaDeComercializacion>>.ValidateAll(IList<RutaDeComercializacion> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (RutaDeComercializacion vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<RutaDeComercializacion>, IList<RutaDeComercializacion>>.SpecializedUpdate(IList<RutaDeComercializacion> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<RutaDeComercializacion>, IList<RutaDeComercializacion>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo);
            vResult = IsValidConsecutivo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo) && vResult;
            vResult = IsValidDescripcion(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Descripcion) && vResult;
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
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
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

        private bool IsValidDescripcion(eAccionSR valAction, int valConsecutivoCompania, string valDescripcion) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibString.IsNullOrEmpty(valDescripcion, true)) {
                BuildValidationInfo(MsgRequiredField("Descripcion"));
                return false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (insDb.ExistsValue("Saw.RutaDeComercializacion", "Descripcion", insDb.InsSql.ToSqlValue(valDescripcion), true)) {
                    BuildValidationInfo("La Ruta de Comercialización ingresada ya existe, ingrese una distinta.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivo) {
            bool vResult = false;
            RutaDeComercializacion vRecordBusqueda = new RutaDeComercializacion();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".RutaDeComercializacion", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, RutaDeComercializacion valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            vResult = insDb.ExistsRecord(DbSchema + ".RutaDeComercializacion", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
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


    } //End of class clsRutaDeComercializacionDat

} //End of namespace Galac.Saw.Dal.Tablas

