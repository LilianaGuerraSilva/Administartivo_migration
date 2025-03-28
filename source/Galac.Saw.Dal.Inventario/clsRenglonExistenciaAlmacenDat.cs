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
    public class clsRenglonExistenciaAlmacenDat: LibData, ILibDataComponentWithSearch<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>> {
        #region Variables
        RenglonExistenciaAlmacen _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private RenglonExistenciaAlmacen CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsRenglonExistenciaAlmacenDat() {
            DbSchema = "dbo";
        }
        #endregion //Constructores
        #region Metodos Generados

        //private StringBuilder ParametrosActualizacion(RenglonExistenciaAlmacen valRecord, eAccionSR valAction) {
        //    StringBuilder vResult = new StringBuilder();
        //    LibGpParams vParams = new LibGpParams();
        //    vParams.AddReturn();
        //    vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
        //    vParams.AddInString("CodigoAlmacen", valRecord.CodigoAlmacen, 5);
        //    vParams.AddInString("CodigoArticulo", valRecord.CodigoArticulo, 30);
        //    vParams.AddInInteger("ConsecutivoRenglon", valRecord.ConsecutivoRenglon);
        //    vParams.AddInString("CodigoSerial", valRecord.CodigoSerial, 50);
        //    vParams.AddInString("CodigoRollo", valRecord.CodigoRollo, 20);
        //    vParams.AddInDecimal("Cantidad", valRecord.Cantidad, 2);
        //    vParams.AddInString("Ubicacion", valRecord.Ubicacion, 30);
        //    vParams.AddInInteger("ConsecutivoAlmacen", valRecord.ConsecutivoAlmacen);
        //    if (valAction == eAccionSR.Modificar) {
        //        vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
        //    }
        //    vResult = vParams.Get();
        //    return vResult;
        //}

        //private StringBuilder ParametrosClave(RenglonExistenciaAlmacen valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
        //    StringBuilder vResult = new StringBuilder();
        //    LibGpParams vParams = new LibGpParams();
        //    if (valAddReturnParameter) {
        //        vParams.AddReturn();
        //    }
        //    vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
        //    vParams.AddInString("CodigoAlmacen", valRecord.CodigoAlmacen, 5);
        //    vParams.AddInString("CodigoArticulo", valRecord.CodigoArticulo, 30);
        //    vParams.AddInInteger("ConsecutivoRenglon", valRecord.ConsecutivoRenglon);
        //    if (valIncludeTimestamp) {
        //        vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
        //    }
        //    vResult = vParams.Get();
        //    return vResult;
        //}

        //private StringBuilder ParametrosProximoConsecutivo(RenglonExistenciaAlmacen valRecord) {
        //    StringBuilder vResult = new StringBuilder();
        //    LibGpParams vParams = new LibGpParams();
        //    vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
        //    vParams.AddInString("CodigoAlmacen", valRecord.CodigoAlmacen, 5);
        //    vParams.AddInString("CodigoArticulo", valRecord.CodigoArticulo, 30);
        //    vResult = vParams.Get();
        //    return vResult;
        //}
        #region Miembros de ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>

        LibResponse ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>.CanBeChoosen(IList<RenglonExistenciaAlmacen> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
        //    RenglonExistenciaAlmacen vRecord = refRecord[0];
        //    StringBuilder vSbInfo = new StringBuilder();
        //    string vErrMsg = "";
        //    LibDatabase insDB = new LibDatabase();
        //    if (valAction == eAccionSR.Eliminar) {
        //        if (vSbInfo.Length == 0) {
        //            vResult.Success = true;
        //        }
        //    } else {
        //        vResult.Success = true;
        //    }
        //    insDB.Dispose();
        //    if (!vResult.Success) {
        //        vErrMsg = LibResMsg.InfoForeignKeyCanNotBeDeleted(vSbInfo.ToString());
        //        throw new GalacAlertException(vErrMsg);
        //    } else {
                return vResult;
        //    }
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "Renglon Existencia Almacen.Eliminar")]
        LibResponse ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>.Delete(IList<RenglonExistenciaAlmacen> refRecord) {
            LibResponse vResult = new LibResponse();

        //    string vErrMsg = "";
        //    CurrentRecord = refRecord[0];
        //    if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
        //        LibDatabase insDb = new LibDatabase();
        //        vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "RenglonExistenciaAlmacenDEL"), ParametrosClave(CurrentRecord, true, true));
        //        insDb.Dispose();
        //    } else {
        //        throw new GalacValidationException(vErrMsg);
        //    }

            return vResult;
        }

        IList<RenglonExistenciaAlmacen> ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<RenglonExistenciaAlmacen> vResult = new List<RenglonExistenciaAlmacen>();
        
        //    LibDatabase insDb = new LibDatabase();
        //    switch (valType) {
        //        case eProcessMessageType.SpName:
        //            valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
        //            vResult = insDb.LoadFromSp<RenglonExistenciaAlmacen>(valProcessMessage, valParameters, CmdTimeOut);
        //            break;
        //        case eProcessMessageType.Query:
        //            vResult = insDb.LoadData<RenglonExistenciaAlmacen>(valProcessMessage, CmdTimeOut, valParameters);
        //            break;
        //        default: throw new ProgrammerMissingCodeException();
        //    }
        //    insDb.Dispose();
        
            return vResult;
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "Renglon Existencia Almacen.Insertar")]
        LibResponse ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>.Insert(IList<RenglonExistenciaAlmacen> refRecord) {
            LibResponse vResult = new LibResponse();

        //    string vErrMsg = "";
        //    CurrentRecord = refRecord[0];
        //    if (ExecuteProcessBeforeInsert()) {
        //        if (Validate(eAccionSR.Insertar, out vErrMsg)) {
        //            LibDatabase insDb = new LibDatabase();
        //            vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "RenglonExistenciaAlmacenINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
        //            insDb.Dispose();
        //        } else {
        //            throw new GalacValidationException(vErrMsg);
        //        }
        //    }

            return vResult;
        }

        XElement ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
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

        //[PrincipalPermission(SecurityAction.Demand, Role = "Renglon Existencia Almacen.Modificar")]
        LibResponse ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>.Update(IList<RenglonExistenciaAlmacen> refRecord) {
            LibResponse vResult = new LibResponse();

        //    string vErrMsg ="";
        //    CurrentRecord = refRecord[0];
        //    if (ExecuteProcessBeforeUpdate()) {
        //        if (Validate(eAccionSR.Modificar, out vErrMsg)) {
        //            LibDatabase insDb = new LibDatabase();
        //            vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "RenglonExistenciaAlmacenUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
        //            insDb.Dispose();
        //        } else {
        //            throw new GalacValidationException(vErrMsg);
        //        }
        //    }

            return vResult;
        }

        bool ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>.ValidateAll(IList<RenglonExistenciaAlmacen> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;

        //    string vErroMessage = "";
        //    foreach (RenglonExistenciaAlmacen vItem in refRecords) {
        //        CurrentRecord = vItem;
        //        vResult = vResult && Validate(valAction, out vErroMessage);
        //        if (LibString.IsNullOrEmpty(vErroMessage, true)) {
        //            refErrorMessage.AppendLine(vErroMessage);
        //        }
        //    }

            return vResult;
        }

        LibResponse ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>.SpecializedUpdate(IList<RenglonExistenciaAlmacen> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            //vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.CodigoAlmacen, CurrentRecord.CodigoArticulo, CurrentRecord.ConsecutivoRenglon);
            //vResult = IsValidCodigoAlmacen(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.CodigoAlmacen) && vResult;
            //vResult = IsValidCodigoArticulo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.CodigoArticulo) && vResult;
            //vResult = IsValidConsecutivoRenglon(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.ConsecutivoRenglon) && vResult;
            //vResult = IsValidCodigoSerial(valAction, CurrentRecord.CodigoSerial) && vResult;
            //vResult = IsValidCodigoRollo(valAction, CurrentRecord.CodigoRollo) && vResult;
            //vResult = IsValidCantidad(valAction, CurrentRecord.Cantidad) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        //private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, string valCodigoAlmacen, string valCodigoArticulo, int valConsecutivoRenglon){
        //    bool vResult = true;
        //    if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
        //        return true;
        //    }
        //    if (valConsecutivoCompania <= 0) {
        //        BuildValidationInfo(MsgRequiredField("Consecutivo Compania"));
        //        vResult = false;
        //    }
        //    return vResult;
        //}

        //private bool IsValidCodigoAlmacen(eAccionSR valAction, int valConsecutivoCompania, string valCodigoAlmacen){
        //    bool vResult = true;
        //    if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
        //        return true;
        //    }
        //    valCodigoAlmacen = LibString.Trim(valCodigoAlmacen);
        //    if (LibString.IsNullOrEmpty(valCodigoAlmacen, true)) {
        //        BuildValidationInfo(MsgRequiredField("Codigo Almacen"));
        //        vResult = false;
        //    } else if (valAction == eAccionSR.Insertar) {
        //        if (KeyExists(valConsecutivoCompania, valCodigoAlmacen, valCodigoArticulo, valConsecutivoRenglon)) {
        //            BuildValidationInfo(MsgFieldValueAlreadyExist("Codigo Almacen", valCodigoAlmacen));
        //            vResult = false;
        //        }
        //    }
        //    return vResult;
        //}

        //private bool IsValidCodigoArticulo(eAccionSR valAction, int valConsecutivoCompania, string valCodigoArticulo){
        //    bool vResult = true;
        //    if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
        //        return true;
        //    }
        //    valCodigoArticulo = LibString.Trim(valCodigoArticulo);
        //    if (LibString.IsNullOrEmpty(valCodigoArticulo, true)) {
        //        BuildValidationInfo(MsgRequiredField("Codigo Articulo"));
        //        vResult = false;
        //    } else if (valAction == eAccionSR.Insertar) {
        //        if (KeyExists(valConsecutivoCompania, valCodigoAlmacen, valCodigoArticulo, valConsecutivoRenglon)) {
        //            BuildValidationInfo(MsgFieldValueAlreadyExist("Codigo Articulo", valCodigoArticulo));
        //            vResult = false;
        //        }
        //    }
        //    return vResult;
        //}

        //private bool IsValidConsecutivoRenglon(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivoRenglon){
        //    bool vResult = true;
        //    if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
        //        return true;
        //    }
        //    if (valConsecutivoRenglon == 0) {
        //        BuildValidationInfo(MsgRequiredField("Consecutivo Renglon"));
        //        vResult = false;
        //    } else if (valAction == eAccionSR.Insertar) {
        //        if (KeyExists(valConsecutivoCompania, valCodigoAlmacen, valCodigoArticulo, valConsecutivoRenglon)) {
        //            BuildValidationInfo(MsgFieldValueAlreadyExist("Consecutivo Renglon", valConsecutivoRenglon));
        //            vResult = false;
        //        }
        //    }
        //    return vResult;
        //}

        //private bool IsValidCodigoSerial(eAccionSR valAction, string valCodigoSerial){
        //    bool vResult = true;
        //    if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
        //        return true;
        //    }
        //    valCodigoSerial = LibString.Trim(valCodigoSerial);
        //    if (LibString.IsNullOrEmpty(valCodigoSerial, true)) {
        //        BuildValidationInfo(MsgRequiredField("Codigo Serial"));
        //        vResult = false;
        //    }
        //    return vResult;
        //}

        //private bool IsValidCodigoRollo(eAccionSR valAction, string valCodigoRollo){
        //    bool vResult = true;
        //    if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
        //        return true;
        //    }
        //    valCodigoRollo = LibString.Trim(valCodigoRollo);
        //    if (LibString.IsNullOrEmpty(valCodigoRollo, true)) {
        //        BuildValidationInfo(MsgRequiredField("Codigo Rollo"));
        //        vResult = false;
        //    }
        //    return vResult;
        //}

        //private bool IsValidCantidad(eAccionSR valAction, decimal valCantidad){
        //    bool vResult = true;
        //    if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
        //        return true;
        //    }
        //    throw new ProgrammerMissingCodeException("Campo Decimal Obligatorio, debe especificar cual es su validacion");
        //    return vResult;
        //}

        //private bool KeyExists(int valConsecutivoCompania, string valCodigoAlmacen, string valCodigoArticulo, int valConsecutivoRenglon) {
        //    bool vResult = false;
        //    RenglonExistenciaAlmacen vRecordBusqueda = new RenglonExistenciaAlmacen();
        //    vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
        //    vRecordBusqueda.CodigoAlmacen = valCodigoAlmacen;
        //    vRecordBusqueda.CodigoArticulo = valCodigoArticulo;
        //    vRecordBusqueda.ConsecutivoRenglon = valConsecutivoRenglon;
        //    LibDatabase insDb = new LibDatabase();
        //    vResult = insDb.ExistsRecord(DbSchema + ".RenglonExistenciaAlmacen", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
        //    insDb.Dispose();
        //    return vResult;
        //}
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


    } //End of class clsRenglonExistenciaAlmacenDat

} //End of namespace Galac.Saw.Dal.Inventario

