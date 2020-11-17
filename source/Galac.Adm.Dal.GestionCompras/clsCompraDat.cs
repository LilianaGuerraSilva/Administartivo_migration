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
    public class clsCompraDat : LibData, ILibDataMasterComponentWithSearch<IList<Compra>, IList<Compra>>, LibGalac.Aos.Base.ILibDataRpt {
        #region Variables
        LibDataScope insTrn;
        Compra _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private Compra CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCompraDat() {
            DbSchema = "Adm";
            insTrn = new LibDataScope();
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(Compra valRecord, eAccionSR valAction) {
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
            vParams.AddInInteger("ConsecutivoAlmacen", valRecord.ConsecutivoAlmacen);
            vParams.AddInString("Moneda", valRecord.Moneda, 80);
            vParams.AddInString("CodigoMoneda", valRecord.CodigoMoneda, 4);
            vParams.AddInDecimal("CambioABolivares", valRecord.CambioABolivares, 4);
            vParams.AddInBoolean("GenerarCXP", valRecord.GenerarCXPAsBool);
            vParams.AddInBoolean("UsaSeguro", valRecord.UsaSeguroAsBool);
            vParams.AddInEnum("TipoDeDistribucion", valRecord.TipoDeDistribucionAsDB);
            vParams.AddInDecimal("TasaAduanera", valRecord.TasaAduanera, 4);
            vParams.AddInDecimal("TasaDolar", valRecord.TasaDolar, 4);
            vParams.AddInDecimal("ValorUT", valRecord.ValorUT, 2);
            vParams.AddInDecimal("TotalRenglones", valRecord.TotalRenglones, 2);
            vParams.AddInDecimal("TotalOtrosGastos", valRecord.TotalOtrosGastos, 2);
            vParams.AddInDecimal("TotalCompra", valRecord.TotalCompra, 2);
            vParams.AddInString("Comentarios", valRecord.Comentarios, 255);
            vParams.AddInEnum("StatusCompra", valRecord.StatusCompraAsDB);
            vParams.AddInEnum("TipoDeCompra", valRecord.TipoDeCompraAsDB);
            vParams.AddInDateTime("FechaDeAnulacion", valRecord.FechaDeAnulacion);
            vParams.AddInInteger("ConsecutivoOrdenDeCompra", valRecord.ConsecutivoOrdenDeCompra);
            vParams.AddInString("NoFacturaNotaEntrega", valRecord.NoFacturaNotaEntrega, 20);
            vParams.AddInEnum("TipoDeCompraParaCxP", valRecord.TipoDeCompraParaCxPAsDB);
            vParams.AddInString("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(Compra valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

        private StringBuilder ParametrosProximoConsecutivo(Compra valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataMasterComponent<IList<Compra>, IList<Compra>>

        LibResponse ILibDataMasterComponent<IList<Compra>, IList<Compra>>.CanBeChoosen(IList<Compra> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Compra vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDataScope insDB = new LibDataScope();
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Compra.Eliminar")]
        LibResponse ILibDataMasterComponent<IList<Compra>, IList<Compra>>.Delete(IList<Compra> refRecord) {
            LibResponse vResult = new LibResponse();
            try {
                string vErrMsg = "";
                CurrentRecord = refRecord[0];
                if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                    if (ExecuteProcessBeforeDelete()) {
                      
                        vResult.Success = insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "CompraDEL"), ParametrosClave(CurrentRecord, true, true));
                        if (vResult.Success) {
                            ExecuteProcessAfterDelete();
                        }
                       
                    }
                } else {
                    throw new GalacException(vErrMsg, eExceptionManagementType.Validation);
                }
                return vResult;
            } finally {
                if (!vResult.Success) {
                   
                }
            }
        }

        IList<Compra> ILibDataMasterComponent<IList<Compra>, IList<Compra>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters, bool valUseDetail) {
            List<Compra> vResult = new List<Compra>();
            LibDataScope insDb = new LibDataScope();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Compra>(valProcessMessage, valParameters, CmdTimeOut);
                    if (valUseDetail && vResult != null && vResult.Count > 0) {
                        new clsCompraDetalleArticuloInventarioDat().GetDetailAndAppendToMaster(ref vResult);
                        new clsCompraDetalleGastoDat().GetDetailAndAppendToMaster(ref vResult);
                        new clsCompraDetalleSerialRolloDat().GetDetailAndAppendToMaster(ref vResult);
                    }
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Compra.Insertar")]
        LibResponse ILibDataMasterComponent<IList<Compra>, IList<Compra>>.Insert(IList<Compra> refRecord, bool valUseDetail) {
            LibResponse vResult = new LibResponse();
            try {
                CurrentRecord = refRecord[0];
                
                if (ExecuteProcessBeforeInsert()) {
                    if (ValidateMasterDetail(eAccionSR.Insertar, CurrentRecord, valUseDetail)) {
                        if (insTrn.ExecSpNonQueryWithScope (insTrn.ToSpName(DbSchema, "CompraINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
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
               
                return vResult;
            } finally {
                if (!vResult.Success) {
                   
                }
            }
        }

        XElement ILibDataMasterComponent<IList<Compra>, IList<Compra>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDataScope insDb = new LibDataScope();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoConsecutivo")) {
                        vResult = LibXml.ValueToXElement(insDb.NextLngConsecutive(DbSchema + ".Compra", "Consecutivo", valParameters), "Consecutivo");
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

        LibResponse ILibDataMasterComponent<IList<Compra>, IList<Compra>>.SpecializedUpdate(IList<Compra> refRecord, bool valUseDetail, string valSpecializedAction) {
            LibResponse vResult = new LibResponse();
            switch (valSpecializedAction) {
                case "Anular":
                    vResult = AnularCompra(refRecord);
                    break;
                case "Abrir":
                    vResult = AbrirComprar(refRecord);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Compra.Modificar")]
        LibResponse ILibDataMasterComponent<IList<Compra>, IList<Compra>>.Update(IList<Compra> refRecord, bool valUseDetail, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            try {
                CurrentRecord = refRecord[0];
                if (ValidateMasterDetail(valAction, CurrentRecord, valUseDetail)) {
                   
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
                    
                }
                return vResult;
            } finally {
                if (!vResult.Success) {
                   
                }
            }
        }

        bool ILibDataMasterComponent<IList<Compra>, IList<Compra>>.ValidateAll(IList<Compra> refRecords, bool valUseDetail, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (Compra vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }
        #endregion //ILibDataMasterComponent<IList<Compra>, IList<Compra>>

        LibResponse UpdateMaster(Compra refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            vResult.Success = insTrn.ExecSpNonQueryWithScope (insTrn.ToSpName(DbSchema, "CompraUPD"), ParametrosActualizacion(refRecord, valAction));
            return vResult;
        }

        LibResponse UpdateMasterAndDetail(Compra refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            string vErrorMessage = "";
            if (ValidateDetail(refRecord, eAccionSR.Modificar, out vErrorMessage)) {
                if (UpdateDetail(refRecord)) {
                    vResult = UpdateMaster(refRecord, valAction);
                }
            }
            return vResult;
        }

        private bool InsertDetail(Compra valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailCompraDetalleArticuloInventarioAndUpdateDb(valRecord);
            vResult = vResult && SetPkInDetailCompraDetalleGastoAndUpdateDb(valRecord);
            vResult = vResult && SetPkInDetailCompraDetalleSerialRolloAndUpdateDb(valRecord);
            return vResult;
        }

        private bool SetPkInDetailCompraDetalleArticuloInventarioAndUpdateDb(Compra valRecord) {
            bool vResult = false;
            int vConsecutivo = 1;
            clsCompraDetalleArticuloInventarioDat insCompraDetalleArticuloInventario = new clsCompraDetalleArticuloInventarioDat();
            foreach (CompraDetalleArticuloInventario vDetail in valRecord.DetailCompraDetalleArticuloInventario) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.ConsecutivoCompra = valRecord.Consecutivo;
                vDetail.Consecutivo = vConsecutivo;
                vConsecutivo++;
            }
            vResult = insCompraDetalleArticuloInventario.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool SetPkInDetailCompraDetalleGastoAndUpdateDb(Compra valRecord) {
            bool vResult = false;
            int vConsecutivo = 1;
            clsCompraDetalleGastoDat insCompraDetalleGasto = new clsCompraDetalleGastoDat();
            foreach (CompraDetalleGasto vDetail in valRecord.DetailCompraDetalleGasto) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.ConsecutivoCompra = valRecord.Consecutivo;
                vDetail.ConsecutivoRenglon = vConsecutivo;
                vConsecutivo++;
            }
            vResult = insCompraDetalleGasto.InsertChild(valRecord, insTrn);
            if (valRecord.DetailCompraDetalleGasto.Count == 0) {
                vResult = true;
            }
            return vResult;
        }

        private bool SetPkInDetailCompraDetalleSerialRolloAndUpdateDb(Compra valRecord) {
            bool vResult = false;
            int vConsecutivo = 1;
            clsCompraDetalleSerialRolloDat insCompraDetalleSerialRollo = new clsCompraDetalleSerialRolloDat();
            foreach (CompraDetalleSerialRollo vDetail in valRecord.DetailCompraDetalleSerialRollo) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.ConsecutivoCompra = valRecord.Consecutivo;
                vDetail.Consecutivo = vConsecutivo;
                vConsecutivo++;
            }
            vResult = insCompraDetalleSerialRollo.InsertChild(valRecord, insTrn);
            if (valRecord.DetailCompraDetalleSerialRollo.Count == 0) {
                vResult = true;
            }
            return vResult;
        }

        private bool UpdateDetail(Compra valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailCompraDetalleArticuloInventarioAndUpdateDb(valRecord);
            vResult = vResult && SetPkInDetailCompraDetalleGastoAndUpdateDb(valRecord);
            vResult = vResult && SetPkInDetailCompraDetalleSerialRolloAndUpdateDb(valRecord);
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
            vResult = IsValidConsecutivoAlmacen(valAction, CurrentRecord.ConsecutivoAlmacen) && vResult;
            vResult = IsValidCodigoMoneda(valAction, CurrentRecord.CodigoMoneda) && vResult;
            vResult = IsValidCambioABolivares(valAction, CurrentRecord.CambioABolivares) && vResult;
            vResult = IsValidFechaDeAnulacion(valAction, CurrentRecord.FechaDeAnulacion) && vResult;
            vResult = IsValidTasaAduanera(valAction, CurrentRecord.TipoDeCompraAsEnum, CurrentRecord.TasaAduanera, CurrentRecord.TipoDeDistribucionAsEnum) && vResult;
            vResult = IsValidTasaDolar(valAction, CurrentRecord.TipoDeCompraAsEnum, CurrentRecord.TasaDolar, CurrentRecord.TipoDeDistribucionAsEnum) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

      

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo) {
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

        private bool IsValidConsecutivo(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo) {
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

        private bool IsValidSerie(eAccionSR valAction, int valConsecutivoCompania, string valSerie) {
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

        private bool IsValidNumero(eAccionSR valAction, int valConsecutivoCompania, string valNumero) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumero = LibString.Trim(valNumero);
            if (LibString.IsNullOrEmpty(valNumero, true)) {
                BuildValidationInfo(MsgRequiredField("Numero"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                Compra vRecBusqueda = new Compra();
                vRecBusqueda.Numero = valNumero;
                if (KeyExists(valConsecutivoCompania, vRecBusqueda)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Numero", valNumero));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidFecha(eAccionSR valAction, DateTime valFecha) {
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

        private bool IsValidConsecutivoProveedor(eAccionSR valAction, int valConsecutivoProveedor) {
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

        private bool IsValidConsecutivoAlmacen(eAccionSR valAction, int valConsecutivoAlmacen) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoAlmacen == 0) {
                CurrentRecord.ConsecutivoAlmacen = 1;
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

        private bool IsValidCambioABolivares(eAccionSR valAction, decimal valCambioABolivares) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }

            return vResult;
        }

        private bool IsValidTipoDeCompra(eAccionSR valAction, eTipoCompra valTipoDeCompra) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool IsValidFechaDeAnulacion(eAccionSR valAction, DateTime valFechaDeAnulacion) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (CurrentRecord.StatusCompraAsEnum == eStatusCompra.Anulada) {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaDeAnulacion, false, valAction)) {
                    BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidTasaAduanera(eAccionSR valAction, eTipoCompra valTipoCompra, decimal valTasaAduanera, eTipoDeDistribucion valTipoDeDistribucion) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valTipoCompra == eTipoCompra.Importacion && valTipoDeDistribucion == eTipoDeDistribucion.Automatica && valTasaAduanera == 0) {
                BuildValidationInfo(MsgRequiredField("Tasa Aduanera"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidTasaDolar(eAccionSR valAction, eTipoCompra valTipoCompra, decimal valTasaDolar, eTipoDeDistribucion valTipoDeDistribucion) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valTipoCompra == eTipoCompra.Importacion && valTipoDeDistribucion == eTipoDeDistribucion.Automatica && valTasaDolar == 0) {
                BuildValidationInfo(MsgRequiredField("Tasa Aduanera"));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivo) {
            bool vResult = false;
            Compra vRecordBusqueda = new Compra();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDataScope insDb = new LibDataScope();
            vResult = insDb.ExistsRecord(DbSchema + ".Compra", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, Compra valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDataScope insDb = new LibDataScope();
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            vResult = insDb.ExistsRecord(DbSchema + ".Compra", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool ValidateMasterDetail(eAccionSR valAction, Compra valRecordMaster, bool valUseDetail) {
            bool vResult = false;
            string vErrMsg;
            if (Validate(valAction, out vErrMsg)) {
                if (valUseDetail) {
                    if (ValidateDetail(valRecordMaster, eAccionSR.Insertar, out vErrMsg)) {
                        vResult = true;
                    } else {
                        throw new GalacValidationException("Compra (detalle)\n" + vErrMsg);
                    }
                } else {
                    vResult = true;
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        private bool ValidateDetail(Compra valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            outErrorMessage = "";
            vResult = vResult && ValidateDetailCompraDetalleArticuloInventario(valRecord, valAction, out outErrorMessage);
            vResult = vResult && ValidateDetailCompraDetalleGasto(valRecord, valAction, out outErrorMessage);
            vResult = vResult && ValidateDetailCompraDetalleSerialRollo(valRecord, valAction, out outErrorMessage);
            return vResult;
        }

       

        private bool ValidateDetailCompraDetalleArticuloInventario(Compra valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            outErrorMessage = string.Empty;
            foreach (CompraDetalleArticuloInventario vDetail in valRecord.DetailCompraDetalleArticuloInventario) {
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
                outErrorMessage = "Compra Detalle Articulo Inventario" + Environment.NewLine + vSbErrorInfo.ToString();
            }
            return vResult;
        }

        private bool ValidateDetailCompraDetalleGasto(Compra valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            outErrorMessage = string.Empty;
            foreach (CompraDetalleGasto vDetail in valRecord.DetailCompraDetalleGasto) {
                bool vLineHasError = true;
                //agregar validaciones
                if (LibString.IsNullOrEmpty(vDetail.CxpNumero)) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Cxp Numero.");
                } else {
                    vLineHasError = false;
                }
                vResult = vResult && (!vLineHasError);
                vNumeroDeLinea++;
            }
            if (!vResult) {
                outErrorMessage = "Compra Detalle Gasto" + Environment.NewLine + vSbErrorInfo.ToString();
            }
            return vResult;
        }

        private bool ValidateDetailCompraDetalleSerialRollo(Compra valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            int vNumeroDeLineaTmp = 1;
            outErrorMessage = string.Empty;
            foreach (var vDetail in valRecord.DetailCompraDetalleSerialRollo.Select((p, i) => new { Index = i, Element = p }).GroupBy(p => new { p.Element.Serial, p.Element.Rollo }).Where(p => p.Count() != 1)) {
                //agregar validaciones
                vNumeroDeLinea = vDetail.First().Index;
                vNumeroDeLineaTmp = vDetail.Skip(1).Take(1).First().Index;
                vSbErrorInfo.AppendLine("Línea " + vNumeroDeLineaTmp.ToString() + ": tiene la misma información que la línea " + vNumeroDeLinea.ToString());
                vResult = false;
            }
            if (!vResult) {
                outErrorMessage = "Compra Detalle Serial" + Environment.NewLine + vSbErrorInfo.ToString();
            }
            return vResult;
        }
        #endregion //Validaciones
        #region Miembros de ILibDataFKSearch
        bool ILibDataFKSearch.ConnectFk(ref XmlDocument refResulset, eProcessMessageType valType, string valProcessMessage, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            LibDataScope insDb = new LibDataScope();
            refResulset = insDb.LoadForConnect(valProcessMessage, valXmlParamsExpression, CmdTimeOut);
            if (refResulset != null && refResulset.DocumentElement != null && refResulset.DocumentElement.HasChildNodes) {
                vResult = true;
            }
            return vResult;
        }
        #endregion //Miembros de ILibDataFKSearch
        #endregion //Metodos Generados

        private LibResponse AbrirComprar(IList<Compra> refRecord) {
            return UpdateStatus(refRecord[0], eAccionSR.Abrir);
        }

        private LibResponse AnularCompra(IList<Compra> refRecord) {
            return UpdateStatus(refRecord[0], eAccionSR.Abrir);
        }

        LibResponse UpdateStatus(Compra refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            LibDataScope vDB = new LibDataScope();
            vResult.Success = vDB.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "CompraUPD"), ParametrosActualizacion(refRecord, eAccionSR.Modificar));
            return vResult;
        }

        #region //Miembros de ILibDataRpt

        System.Data.DataTable ILibDataRpt.GetDt(string valSqlStringCommand, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSqlStringCommand, valCmdTimeout);
        }

        System.Data.DataTable ILibDataRpt.GetDt(string valSpName, StringBuilder valXmlParamsExpression, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSpName, valXmlParamsExpression, valCmdTimeout);
        }
        #endregion ////Miembros de ILibDataRpt
    } //End of class clsCompraDat

} //End of namespace Galac.Adm.Dal.GestionCompras

