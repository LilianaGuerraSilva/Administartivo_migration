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
using Entity = Galac.Saw.Ccl.Vehiculo;

namespace Galac.Saw.Dal.Vehiculo {
    public class clsVehiculoDat : LibData, ILibDataComponentWithSearch<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>> {
        #region Variables
        Entity.Vehiculo _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private Entity.Vehiculo CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsVehiculoDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(Entity.Vehiculo valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("Placa", valRecord.Placa, 20);
            vParams.AddInString("serialVIN", valRecord.serialVIN, 40);
            vParams.AddInString("NombreModelo", valRecord.NombreModelo, 20);
            vParams.AddInInteger("Ano", valRecord.Ano);
            vParams.AddInString("CodigoColor", valRecord.CodigoColor, 3);
            vParams.AddInString("CodigoCliente", valRecord.CodigoCliente, 10);
            vParams.AddInString("NumeroPoliza", valRecord.NumeroPoliza, 20);
            vParams.AddInString("SerialMotor", valRecord.SerialMotor, 40);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(Entity.Vehiculo valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

        private StringBuilder ParametrosProximoConsecutivo(Entity.Vehiculo valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        private StringBuilder ParametrosModeloVehiculo(Entity.Vehiculo valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Nombre", valRecord.NombreModelo, 20);
            vResult = vParams.Get();
            return vResult;
        }
        private StringBuilder ParametrosCodigoColor(Entity.Vehiculo valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoColor", valRecord.CodigoColor, 3);
            vResult = vParams.Get();
            return vResult;
        }
        private StringBuilder ParametrosCodigoCliente(Entity.Vehiculo valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Codigo", valRecord.CodigoCliente, 10);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>>

        LibResponse ILibDataComponent<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>>.CanBeChoosen(IList<Entity.Vehiculo> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Entity.Vehiculo vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValueOnMultifile("dbo.Factura", "ConsecutivoVehiculo", "ConsecutivoCompania",LibConvert.ToStr(vRecord.Consecutivo),LibConvert.ToStr(vRecord.ConsecutivoCompania),true)) {
                    vSbInfo.AppendLine("Factura");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Vehículo.Eliminar")]
        LibResponse ILibDataComponent<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>>.Delete(IList<Entity.Vehiculo> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                if (insDb.ExistsValueOnMultifile("dbo.Factura", "ConsecutivoVehiculo", "ConsecutivoCompania", LibConvert.ToStr(CurrentRecord.Consecutivo), LibConvert.ToStr(CurrentRecord.ConsecutivoCompania), true)) {
                    throw new GalacAlertException(LibResMsg.InfoForeignKeyCanNotBeDeleted("Factura"));
                } else {
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "VehiculoDEL"), ParametrosClave(CurrentRecord, true, true));
                    insDb.Dispose();
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<Entity.Vehiculo> ILibDataComponent<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<Entity.Vehiculo> vResult = new List<Entity.Vehiculo>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Entity.Vehiculo>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<Entity.Vehiculo>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Vehículo.Insertar")]
        LibResponse ILibDataComponent<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>>.Insert(IList<Entity.Vehiculo> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    CurrentRecord.Consecutivo = insDb.NextLngConsecutive("Saw.Vehiculo", "Consecutivo", ParametrosProximoConsecutivo(CurrentRecord));
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "VehiculoINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Vehículo.Modificar")]
        LibResponse ILibDataComponent<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>>.Update(IList<Entity.Vehiculo> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "VehiculoUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>>.ValidateAll(IList<Entity.Vehiculo> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (Entity.Vehiculo vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>>.SpecializedUpdate(IList<Entity.Vehiculo> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.ConsecutivoCompania);
            vResult = IsValidConsecutivo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo) && vResult;
            vResult = IsValidPlaca(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Placa) && vResult;
            vResult = IsValidNombreModelo(valAction, CurrentRecord.NombreModelo ) && vResult;
            vResult = IsValidCodigoColor(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.CodigoColor) && vResult;
            vResult = IsValidCodigoCliente(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.CodigoCliente) && vResult;
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

        private bool IsValidPlaca(eAccionSR valAction, int valConsecutivoCompania, string valPlaca){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valPlaca = LibString.Trim(valPlaca);
            if (LibString.IsNullOrEmpty(valPlaca, true)) {
                BuildValidationInfo(MsgRequiredField("Placa"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                Entity.Vehiculo vRecBusqueda = new Entity.Vehiculo();
                vRecBusqueda.Placa = valPlaca;
                if (KeyExists(valConsecutivoCompania,vRecBusqueda)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Placa", valPlaca));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidNombreModelo(eAccionSR valAction, string valNombreModelo) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNombreModelo = LibString.Trim(valNombreModelo);
            if (LibString.IsNullOrEmpty(valNombreModelo , true)) {
                BuildValidationInfo(MsgRequiredField("Modelo Vehículo"));
                vResult = false;
            }
            Entity.Vehiculo vRecordBusqueda = new Entity.Vehiculo();
            vRecordBusqueda.NombreModelo = valNombreModelo;
            LibDatabase insDb = new LibDatabase();
            if (!insDb.ExistsRecord(DbSchema + ".Modelo", "Nombre", ParametrosModeloVehiculo(vRecordBusqueda))) {
                BuildValidationInfo("El Modelo no existe");
                insDb.Dispose();
                vResult = false;
            }
            return vResult;
        }
        private bool IsValidCodigoColor(eAccionSR valAction, int valConsecutivoCompania, string valCodigoColor) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoColor = LibString.Trim(valCodigoColor);
            if (LibString.IsNullOrEmpty(valCodigoColor, true)) {
                BuildValidationInfo(MsgRequiredField("Color"));
                vResult = false;
            }
            Entity.Vehiculo vRecordBusqueda = new Entity.Vehiculo();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.CodigoColor = valCodigoColor;
            LibDatabase insDb = new LibDatabase();
            if (!insDb.ExistsRecord(DbSchema + ".Color", "ConsecutivoCompania", ParametrosCodigoColor(vRecordBusqueda))) {
                BuildValidationInfo("El Color no existe");
                insDb.Dispose();
                vResult = false;
            }
            return vResult;
        }
        private bool IsValidCodigoCliente(eAccionSR valAction, int valConsecutivoCompania, string valCodigoCliente) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoCliente = LibString.Trim(valCodigoCliente);
            if (LibString.IsNullOrEmpty(valCodigoCliente, true)) {
                BuildValidationInfo(MsgRequiredField("Cliente"));
                vResult = false;
            }
            Entity.Vehiculo vRecordBusqueda = new Entity.Vehiculo();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.CodigoCliente = valCodigoCliente;
            LibDatabase insDb = new LibDatabase();
            if (!insDb.ExistsRecord("dbo.Cliente", "ConsecutivoCompania", ParametrosCodigoCliente(vRecordBusqueda))) {
                BuildValidationInfo("El Cliente no existe");
                insDb.Dispose();
                vResult = false;
            }
            return vResult;
        }
        private bool KeyExists(int valConsecutivoCompania, int valConsecutivo) {
            bool vResult = false;
            Entity.Vehiculo vRecordBusqueda = new Entity.Vehiculo();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord("Saw.Vehiculo", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, Entity.Vehiculo valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord("Saw.Vehiculo", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
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


    } //End of class clsVehiculoDat

} //End of namespace Galac.Saw.Dal.Vehiculo

