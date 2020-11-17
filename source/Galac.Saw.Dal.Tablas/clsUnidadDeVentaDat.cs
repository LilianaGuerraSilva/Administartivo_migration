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
    public class clsUnidadDeVentaDat: LibData, ILibDataComponentWithSearch<IList<UnidadDeVenta>, IList<UnidadDeVenta>> {
        #region Variables
        UnidadDeVenta _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private UnidadDeVenta CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsUnidadDeVentaDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(UnidadDeVenta valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInString("Nombre", valRecord.Nombre, 20);
            if (valAction == eAccionSR.Modificar ) {
                vParams.AddInString("NombreOriginal", valRecord.NombreOriginal, 20);
            }
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(UnidadDeVenta valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInString("Nombre", valRecord.Nombre, 20);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<UnidadDeVenta>, IList<UnidadDeVenta>>

        LibResponse ILibDataComponent<IList<UnidadDeVenta>, IList<UnidadDeVenta>>.CanBeChoosen(IList<UnidadDeVenta> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            UnidadDeVenta vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValue("dbo.ArticuloInventario", "UnidadDeVenta", insDB.InsSql.ToSqlValue(vRecord.Nombre), true)) {
                    vSbInfo.AppendLine("ArticuloInventario");
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
        LibResponse ILibDataComponent<IList<UnidadDeVenta>, IList<UnidadDeVenta>>.Delete(IList<UnidadDeVenta> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "UnidadDeVentaDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<UnidadDeVenta> ILibDataComponent<IList<UnidadDeVenta>, IList<UnidadDeVenta>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<UnidadDeVenta> vResult = new List<UnidadDeVenta>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<UnidadDeVenta>(valProcessMessage, valParameters, CmdTimeOut);
                    foreach (UnidadDeVenta vUnidadDeVenta in vResult) {
                       vUnidadDeVenta.NombreOriginal = vUnidadDeVenta.Nombre;
                    }
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<UnidadDeVenta>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        LibResponse ILibDataComponent<IList<UnidadDeVenta>, IList<UnidadDeVenta>>.Insert(IList<UnidadDeVenta> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "UnidadDeVentaINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<UnidadDeVenta>, IList<UnidadDeVenta>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                case eProcessMessageType.Query:
                    vResult = LibXml.ToXElement(insDb.LoadData(valProcessMessage, CmdTimeOut, valParameters));
                    break;
                //default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Modificar")]
        LibResponse ILibDataComponent<IList<UnidadDeVenta>, IList<UnidadDeVenta>>.Update(IList<UnidadDeVenta> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "UnidadDeVentaUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<UnidadDeVenta>, IList<UnidadDeVenta>>.ValidateAll(IList<UnidadDeVenta> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (UnidadDeVenta vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<UnidadDeVenta>, IList<UnidadDeVenta>>.SpecializedUpdate(IList<UnidadDeVenta> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<UnidadDeVenta>, IList<UnidadDeVenta>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidNombre(valAction, CurrentRecord.Nombre);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidNombre(eAccionSR valAction, string valNombre){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNombre = LibString.Trim(valNombre);
            if (LibString.IsNullOrEmpty(valNombre, true)) {
                BuildValidationInfo(MsgRequiredField("Unidad de Venta"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valNombre)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Unidad de Venta", valNombre));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool KeyExists(string valNombre) {
            bool vResult = false;
            UnidadDeVenta vRecordBusqueda = new UnidadDeVenta();
            vRecordBusqueda.Nombre = valNombre;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord("Saw.UnidadDeVenta", "Nombre", ParametrosClave(vRecordBusqueda, false, false));
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


    } //End of class clsUnidadDeVentaDat

} //End of namespace Galac.Saw.Dal.Tablas

