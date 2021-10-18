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
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Dal.GestionCompras {
    public class clsOrdenDeCompraDat: LibData, ILibDataMasterComponentWithSearch<IList<OrdenDeCompra>, IList<OrdenDeCompra>>, LibGalac.Aos.Base.ILibDataRpt {
        #region Variables
        LibTrn insTrn;
        OrdenDeCompra _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private OrdenDeCompra CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsOrdenDeCompraDat() {
            DbSchema = "Adm";
            insTrn = new LibTrn();
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(OrdenDeCompra valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("Serie", valRecord.Serie, 20);
            vParams.AddInString("Numero", valRecord.Numero, 20);
            vParams.AddInDateTime("Fecha", valRecord.Fecha);
            vParams.AddInInteger("ConsecutivoProveedor", valRecord.ConsecutivoProveedor);
            vParams.AddInString("NumeroCotizacion", valRecord.NumeroCotizacion, 11);
            vParams.AddInString("Moneda", valRecord.Moneda, 80);
            vParams.AddInString("CodigoMoneda", valRecord.CodigoMoneda, 4);
            vParams.AddInDecimal("CambioABolivares", valRecord.CambioABolivares, 4);
            vParams.AddInDecimal("TotalRenglones", valRecord.TotalRenglones, 2);
            vParams.AddInDecimal("TotalCompra", valRecord.TotalCompra, 2);
            vParams.AddInEnum("TipoDeCompra", valRecord.TipoDeCompraAsDB);
            vParams.AddInString("Comentarios", valRecord.Comentarios, 255);
            vParams.AddInEnum("StatusOrdenDeCompra", valRecord.StatusOrdenDeCompraAsDB);
            vParams.AddInDateTime("FechaDeAnulacion", valRecord.FechaDeAnulacion);
            vParams.AddInString("CondicionesDeEntrega", valRecord.CondicionesDeEntrega, 500);
            vParams.AddInInteger("CondicionesDePago", valRecord.CondicionesDePago);
            vParams.AddInEnum("CondicionesDeImportacion", valRecord.CondicionesDeImportacionAsDB);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        protected override bool ExecuteProcessBeforeInsert(){
            StringBuilder vParametro = ParametrosProximoConsecutivo(CurrentRecord);
            LibDataScope insDb = new LibDataScope();
            CurrentRecord.Consecutivo = insDb.NextLngConsecutive(DbSchema + ".OrdenDeCompra", "Consecutivo", vParametro);
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "SugerirNumeroDeOrdenDeCompra")){
                CurrentRecord.Numero = ProximoNumero();
            }
            return base.ExecuteProcessBeforeInsert();
        }

        private string ProximoNumero() {
            StringBuilder SQL = new StringBuilder();
            QAdvSql vAdvSql = new QAdvSql("");
            LibDatabase insDb = new LibDatabase();
            int vNumero = 0;
            string vNumeroString = "";
            string vNuevoNumero = "";
            int LongitudParaCorte = 0;
            string vSelectMax = "";
            if (LibDefGen.ProgramInfo.IsCountryVenezuela()){
                vSelectMax = " SELECT ( ISNULL ( MAX( CAST(SUBSTRING(OrdenDeCompra.Numero,4,LEN(OrdenDeCompra.Numero)) AS int) ) , 0 )  +1) AS  Numero FROM Adm.OrdenDeCompra ";
            } else {
                vSelectMax = " SELECT(" + vAdvSql.IsNull("MAX(" + vAdvSql.ToInt("OrdenDeCompra.Numero") + ")", "0") + " + 1) AS Numero FROM Adm.OrdenDeCompra ";
            }
            SQL.AppendLine(vSelectMax);
            SQL.AppendLine(" WHERE ConsecutivoCompania = " + CurrentRecord.ConsecutivoCompania);
            SQL.AppendLine("AND TipoDeCompra = " + LibConvert.ToInt(CurrentRecord.TipoDeCompraAsDB));
            SQL.AppendLine("AND ISNUMERIC(SUBSTRING(OrdenDeCompra.Numero,4,LEN(OrdenDeCompra.Numero)))=1");
            if (LibDefGen.ProgramInfo.IsCountryPeru()){
                SQL.AppendLine(" AND Serie = @Serie ");
            }

            object vNumero2 = insDb.ExecuteScalar(SQL.ToString(), -1, false);
            if (vNumero2 != null){
                vNumero = LibConvert.ToInt(vNumero2);
            }
            vNumeroString = LibConvert.IntToStr2(vNumero);
            LongitudParaCorte = 8 - vNumeroString.Length;
            for (int i = 0; i < LongitudParaCorte; i++) vNumeroString = "0"+ vNumeroString;
            if (LibDefGen.ProgramInfo.IsCountryVenezuela()){
                vNuevoNumero = "OC-" + vNumeroString;
            }
            insDb.Dispose();
            return vNuevoNumero;
        }
        private StringBuilder ParametrosClave(OrdenDeCompra valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

        private StringBuilder ParametrosProximoConsecutivo(OrdenDeCompra valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }

        #region Miembros de ILibDataMasterComponent<IList<OrdenDeCompra>, IList<OrdenDeCompra>>

        LibResponse ILibDataMasterComponent<IList<OrdenDeCompra>, IList<OrdenDeCompra>>.CanBeChoosen(IList<OrdenDeCompra> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            OrdenDeCompra vRecord = refRecord[0];
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Orden De Compra.Eliminar")]
        LibResponse ILibDataMasterComponent<IList<OrdenDeCompra>, IList<OrdenDeCompra>>.Delete(IList<OrdenDeCompra> refRecord) {
            LibResponse vResult = new LibResponse();
            try {
                string vErrMsg = "";
                CurrentRecord = refRecord[0];
                if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                    if (ExecuteProcessBeforeDelete()) {
                        insTrn.StartTransaction();
                        vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "OrdenDeCompraDEL"), ParametrosClave(CurrentRecord, true, true));
                        if (vResult.Success) {
                            ExecuteProcessAfterDelete();
                        }
                        insTrn.CommitTransaction();
                    }
                } else {
                    throw new GalacException(vErrMsg, eExceptionManagementType.Validation);
                }
                return vResult;
            } finally {
                if (!vResult.Success) {
                    insTrn.RollBackTransaction();
                }
            }
        }

        IList<OrdenDeCompra> ILibDataMasterComponent<IList<OrdenDeCompra>, IList<OrdenDeCompra>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters, bool valUseDetail) {
            List<OrdenDeCompra> vResult = new List<OrdenDeCompra>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<OrdenDeCompra>(valProcessMessage, valParameters, CmdTimeOut);
                    if (valUseDetail && vResult != null && vResult.Count > 0) {
                        new clsOrdenDeCompraDetalleArticuloInventarioDat().GetDetailAndAppendToMaster(ref vResult);
                    }
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Orden De Compra.Insertar")]
        LibResponse ILibDataMasterComponent<IList<OrdenDeCompra>, IList<OrdenDeCompra>>.Insert(IList<OrdenDeCompra> refRecord, bool valUseDetail) {
            LibResponse vResult = new LibResponse();
            try {
                CurrentRecord = refRecord[0];
                insTrn.StartTransaction();
                if (ExecuteProcessBeforeInsert()) {
                    if (ValidateMasterDetail(eAccionSR.Insertar, CurrentRecord, valUseDetail)) {
                        if (insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "OrdenDeCompraINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
                            if (valUseDetail) {
							    vResult.Success = true;
                                InsertDetail(CurrentRecord);
                            } else {
                                vResult.Success = true;
                            }
                            if (vResult.Success) {
                                ExecuteProcessAfterInsert();
                            }
                        }
                    }
                }
                insTrn.CommitTransaction();
                return vResult;
            } finally {
                if (!vResult.Success) {
                    insTrn.RollBackTransaction();
                }
            }
        }

        XElement ILibDataMasterComponent<IList<OrdenDeCompra>, IList<OrdenDeCompra>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoConsecutivo")) {
                        vResult = LibXml.ValueToXElement(insDb.NextLngConsecutive(DbSchema + ".OrdenDeCompra", "Consecutivo", valParameters), "Consecutivo");
                    }
                    break;
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

        LibResponse ILibDataMasterComponent<IList<OrdenDeCompra>, IList<OrdenDeCompra>>.SpecializedUpdate(IList<OrdenDeCompra> refRecord,  bool valUseDetail, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Orden De Compra.Modificar")]
        LibResponse ILibDataMasterComponent<IList<OrdenDeCompra>, IList<OrdenDeCompra>>.Update(IList<OrdenDeCompra> refRecord, bool valUseDetail, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            try {
                CurrentRecord = refRecord[0];
                if (ValidateMasterDetail(valAction, CurrentRecord, valUseDetail)) {
                    insTrn.StartTransaction();
                    if (ExecuteProcessBeforeUpdate()) {
                        if (valUseDetail) {
                            vResult = UpdateMasterAndDetail(CurrentRecord, valAction);
                        } else {
                            vResult = UpdateMaster(CurrentRecord, valAction); //por si requiriese especialización por acción
                        }
                        if (vResult.Success) {
                            ExecuteProcessAfterUpdate();
                        }
                    }
                    insTrn.CommitTransaction();
                }
                return vResult;
            } finally {
                if (!vResult.Success) {
                    insTrn.RollBackTransaction();
                }
            }
        }

        bool ILibDataMasterComponent<IList<OrdenDeCompra>, IList<OrdenDeCompra>>.ValidateAll(IList<OrdenDeCompra> refRecords, bool valUseDetail, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (OrdenDeCompra vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }
        #endregion //ILibDataMasterComponent<IList<OrdenDeCompra>, IList<OrdenDeCompra>>

        LibResponse UpdateMaster(OrdenDeCompra refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "OrdenDeCompraUPD"), ParametrosActualizacion(refRecord, valAction));
            return vResult;
        }

        LibResponse UpdateMasterAndDetail(OrdenDeCompra refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            string vErrorMessage = "";
            if (ValidateDetail(refRecord, eAccionSR.Modificar,out vErrorMessage)) {
                if (UpdateDetail(refRecord)) {
                    vResult = UpdateMaster(refRecord, valAction);
                }
            }
            return vResult;
        }

        private bool InsertDetail(OrdenDeCompra valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailOrdenDeCompraDetalleArticuloInventarioAndUpdateDb(valRecord);
            return vResult;
        }

        private bool SetPkInDetailOrdenDeCompraDetalleArticuloInventarioAndUpdateDb(OrdenDeCompra valRecord) {
            bool vResult = false;
            int vConsecutivo = 1;
            clsOrdenDeCompraDetalleArticuloInventarioDat insOrdenDeCompraDetalleArticuloInventario = new clsOrdenDeCompraDetalleArticuloInventarioDat();
            foreach (OrdenDeCompraDetalleArticuloInventario vDetail in valRecord.DetailOrdenDeCompraDetalleArticuloInventario) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.ConsecutivoOrdenDeCompra = valRecord.Consecutivo;
                vDetail.Consecutivo = vConsecutivo;
                vConsecutivo++;
            }
            vResult = insOrdenDeCompraDetalleArticuloInventario.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool UpdateDetail(OrdenDeCompra valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailOrdenDeCompraDetalleArticuloInventarioAndUpdateDb(valRecord);
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo);
            vResult = IsValidConsecutivo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo) && vResult;
            vResult = IsValidSerie(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Serie) && vResult;
            vResult = IsValidNumero(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Numero) && vResult;
            vResult = IsValidFecha(valAction, CurrentRecord.Fecha) && vResult;
            vResult = IsValidConsecutivoProveedor(valAction, CurrentRecord.ConsecutivoProveedor) && vResult;
            vResult = IsValidCodigoMoneda(valAction, CurrentRecord.CodigoMoneda) && vResult;
            vResult = IsValidCambioABolivares(valAction, CurrentRecord.CambioABolivares) && vResult;
            vResult = IsValidFechaDeAnulacion(valAction, CurrentRecord.FechaDeAnulacion) && vResult;
            vResult = IsValidCondicionesDePago(valAction, CurrentRecord.CondicionesDePago) && vResult;
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

        private bool IsValidSerie(eAccionSR valAction, int valConsecutivoCompania, string valSerie){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                return true;
            }
            valSerie = LibString.Trim(valSerie);
            if (LibString.IsNullOrEmpty(valSerie, true)) {
                BuildValidationInfo(MsgRequiredField("Serie"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidNumero(eAccionSR valAction, int valConsecutivoCompania, string valNumero){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumero = LibString.Trim(valNumero);
            if (LibString.IsNullOrEmpty(valNumero, true)) {
                BuildValidationInfo(MsgRequiredField("Numero"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                OrdenDeCompra vRecBusqueda = new OrdenDeCompra();
                vRecBusqueda.Numero = valNumero;
                if (KeyExists(valConsecutivoCompania, vRecBusqueda)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Numero", valNumero));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidFecha(eAccionSR valAction, DateTime valFecha){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFecha, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidConsecutivoProveedor(eAccionSR valAction, int valConsecutivoProveedor){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoProveedor == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Proveedor"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidCodigoMoneda(eAccionSR valAction, string valCodigoMoneda) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoMoneda = LibString.Trim(valCodigoMoneda);
            if (LibString.IsNullOrEmpty(valCodigoMoneda, true)) {
                BuildValidationInfo(MsgRequiredField("Código"));
                vResult = false;
            } else {
                LibDataScope insDb = new LibDataScope();
                if (!insDb.ExistsValue("dbo.Moneda", "Codigo", insDb.InsSql.ToSqlValue(valCodigoMoneda), true)) {
                    BuildValidationInfo("El valor asignado al campo Código no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }
        private bool IsValidCambioABolivares(eAccionSR valAction, decimal valCambioABolivares){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }

            return vResult;
        }

        private bool IsValidFechaDeAnulacion(eAccionSR valAction, DateTime valFechaDeAnulacion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (CurrentRecord.StatusOrdenDeCompraAsEnum == eStatusCompra.Anulada) {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaDeAnulacion, false, valAction)) {
                    BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCondicionesDePago(eAccionSR valAction, int valCondicionesDePago){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            //if (valCondicionesDePago == 0) {
            //    BuildValidationInfo(MsgRequiredField("Condiciones de Pago"));
            //    vResult = false;
           // }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivo) {
            bool vResult = false;
            OrdenDeCompra vRecordBusqueda = new OrdenDeCompra();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".OrdenDeCompra", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, OrdenDeCompra valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            vResult = insDb.ExistsRecord(DbSchema + ".OrdenDeCompra", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool ValidateMasterDetail(eAccionSR valAction, OrdenDeCompra valRecordMaster, bool valUseDetail) {
            bool vResult = false;
            string vErrMsg;
            if (Validate(valAction, out vErrMsg)) {
                if (valUseDetail) {
                    if (ValidateDetail(valRecordMaster, eAccionSR.Insertar, out vErrMsg)) {
                        vResult = true;
                    } else {
                        throw new GalacValidationException("Orden De Compra (detalle)\n" + vErrMsg);
                    }
                } else {
                    vResult = true;
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        private bool ValidateDetail(OrdenDeCompra valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            outErrorMessage = "";
            vResult = vResult && ValidateDetailOrdenDeCompraDetalleArticuloInventario(valRecord, valAction, out outErrorMessage);
            return vResult;
        }

        private bool ValidateDetailOrdenDeCompraDetalleArticuloInventario(OrdenDeCompra valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            outErrorMessage = string.Empty;
            foreach (OrdenDeCompraDetalleArticuloInventario vDetail in valRecord.DetailOrdenDeCompraDetalleArticuloInventario) {
                bool vLineHasError = true;
                //agregar validaciones
                if (LibString.IsNullOrEmpty(vDetail.CodigoArticulo)) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Código Inventario.");
                } else {
                    vLineHasError = false;
                }
                vResult = vResult && (!vLineHasError);
                vNumeroDeLinea++;
            }
            if (!vResult) {
                outErrorMessage = "Orden De Compra Detalle Articulo Inventario"  + Environment.NewLine + vSbErrorInfo.ToString();
            }
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

        #region //Miembros de ILibDataRpt

        System.Data.DataTable ILibDataRpt.GetDt(string valSqlStringCommand, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSqlStringCommand, valCmdTimeout);
        }

        System.Data.DataTable ILibDataRpt.GetDt(string valSpName, StringBuilder valXmlParamsExpression, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSpName, valXmlParamsExpression, valCmdTimeout);
        }
        #endregion ////Miembros de ILibDataRpt
        #endregion //Metodos Generados


    } //End of class clsOrdenDeCompraDat

} //End of namespace Galac.Adm.Dal.GestionCompras

