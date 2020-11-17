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
using Galac.Saw.Ccl.Vehiculo;

namespace Galac.Saw.Dal.Vehiculo {
    public class clsModeloDat: LibData, ILibDataComponentWithSearch<IList<Modelo>, IList<Modelo>> {
        #region Variables
        Modelo _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private Modelo CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsModeloDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(Modelo valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInString("Nombre", valRecord.Nombre, 20);
            vParams.AddInString("Marca", valRecord.Marca, 20);
            if (valAction == eAccionSR.Modificar) {
            vParams.AddInString("NombreOriginal", valRecord.NombreOriginal, 20);
         }
         if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(Modelo valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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
        #region Miembros de ILibDataComponent<IList<Modelo>, IList<Modelo>>

        LibResponse ILibDataComponent<IList<Modelo>, IList<Modelo>>.CanBeChoosen(IList<Modelo> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Modelo vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValue("Saw.Vehiculo", "NombreModelo", insDB.InsSql.ToSqlValue(vRecord.Nombre), true)) {
                    vSbInfo.AppendLine("Vehículo");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Modelo.Eliminar")]
        LibResponse ILibDataComponent<IList<Modelo>, IList<Modelo>>.Delete(IList<Modelo> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ModeloDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<Modelo> ILibDataComponent<IList<Modelo>, IList<Modelo>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<Modelo> vResult = new List<Modelo>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Modelo>(valProcessMessage, valParameters, CmdTimeOut);
               foreach (Modelo vModelo in vResult) {
                  vModelo.NombreOriginal = vModelo.Nombre;
               }
                    break;
            case eProcessMessageType.Query:
               vResult = insDb.LoadData<Modelo>(valProcessMessage, CmdTimeOut, valParameters);
               break;
            default: throw new ProgrammerMissingCodeException();
         }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Modelo.Insertar")]
        LibResponse ILibDataComponent<IList<Modelo>, IList<Modelo>>.Insert(IList<Modelo> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ModeloINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<Modelo>, IList<Modelo>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Modelo.Modificar")]
        LibResponse ILibDataComponent<IList<Modelo>, IList<Modelo>>.Update(IList<Modelo> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ModeloUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<Modelo>, IList<Modelo>>.ValidateAll(IList<Modelo> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (Modelo vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<Modelo>, IList<Modelo>>.SpecializedUpdate(IList<Modelo> refRecord, string valSpecializedAction) {
         throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<Modelo>, IList<Modelo>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidNombre(valAction, CurrentRecord.Nombre);
            vResult = IsValidMarca(valAction, CurrentRecord.Marca) && vResult;
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
                BuildValidationInfo(MsgRequiredField("Nombre"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valNombre)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Nombre", valNombre));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidMarca(eAccionSR valAction, string valMarca){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valMarca = LibString.Trim(valMarca);
            if (LibString.IsNullOrEmpty(valMarca, true)) {
                BuildValidationInfo(MsgRequiredField("Marca"));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(string valNombre) {
            bool vResult = false;
            Modelo vRecordBusqueda = new Modelo();
            vRecordBusqueda.Nombre = valNombre;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord("Saw.Modelo", "Nombre", ParametrosClave(vRecordBusqueda, false, false));
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


    } //End of class clsModeloDat

} //End of namespace Galac.Saw.Dal.Vehiculo

