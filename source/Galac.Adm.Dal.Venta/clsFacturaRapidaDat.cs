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
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.SttDef;
using System.Data;

namespace Galac.Adm.Dal.Venta {
    public class clsFacturaRapidaDat : LibData, ILibDataMasterComponentWithSearch<IList<FacturaRapida>, IList<FacturaRapida>>, ILibDataRpt {
        #region Variables
        LibTrn insTrn;
        FacturaRapida _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private FacturaRapida CurrentRecord {
            get {
                return _CurrentRecord;
            }
            set {
                _CurrentRecord = value;
            }
        }
        #endregion //Propiedades
        #region Constructores

        public clsFacturaRapidaDat() {
            DbSchema = "dbo";
            insTrn = new LibTrn();
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(FacturaRapida valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Numero", valRecord.Numero, 11);
            vParams.AddInDateTime("Fecha", valRecord.Fecha);
            vParams.AddInString("CodigoCliente", valRecord.CodigoCliente, 10);
            vParams.AddInString("CodigoVendedor", valRecord.CodigoVendedor, 5);
            vParams.AddInString("Observaciones", valRecord.Observaciones, 255);
            vParams.AddInDecimal("TotalMontoExento", valRecord.TotalMontoExento, 2);
            vParams.AddInDecimal("TotalBaseImponible", valRecord.TotalBaseImponible, 2);
            vParams.AddInDecimal("TotalRenglones", valRecord.TotalRenglones, 2);
            vParams.AddInDecimal("TotalIVA", valRecord.TotalIVA, 2);
            vParams.AddInDecimal("TotalFactura", valRecord.TotalFactura, 2);
            vParams.AddInDecimal("PorcentajeDescuento", valRecord.PorcentajeDescuento, 2);
            vParams.AddInString("CodigoNota1", valRecord.CodigoNota1, 10);
            vParams.AddInString("CodigoNota2", valRecord.CodigoNota2, 10);
            vParams.AddInString("Moneda", valRecord.Moneda, 80);
            vParams.AddInEnum("NivelDePrecio", valRecord.NivelDePrecioAsDB);
            vParams.AddInBoolean("ReservarMercancia", valRecord.ReservarMercanciaAsBool);
            vParams.AddInDateTime("FechaDeRetiro", valRecord.FechaDeRetiro);
            vParams.AddInString("CodigoAlmacen", valRecord.CodigoAlmacen, 5);
            vParams.AddInEnum("StatusFactura", valRecord.StatusFacturaAsDB);
            vParams.AddInEnum("TipoDeDocumento", valRecord.TipoDeDocumentoAsDB);
            vParams.AddInBoolean("InsertadaManualmente", valRecord.InsertadaManualmenteAsBool);
            vParams.AddInBoolean("FacturaHistorica", valRecord.FacturaHistoricaAsBool);
            vParams.AddInBoolean("Cancelada", valRecord.CanceladaAsBool);
            vParams.AddInBoolean("UsarDireccionFiscal", valRecord.UsarDireccionFiscalAsBool);
            vParams.AddInInteger("NoDirDespachoAimprimir", valRecord.NoDirDespachoAimprimir);
            vParams.AddInDecimal("CambioABolivares", valRecord.CambioABolivares, 2);
            vParams.AddInDecimal("MontoDelAbono", valRecord.MontoDelAbono, 2);
            vParams.AddInDateTime("FechaDeVencimiento", valRecord.FechaDeVencimiento);
            vParams.AddInString("CondicionesDePago", valRecord.CondicionesDePago, 30);
            vParams.AddInEnum("FormaDeLaInicial", valRecord.FormaDeLaInicialAsDB);
            vParams.AddInDecimal("PorcentajeDeLaInicial", valRecord.PorcentajeDeLaInicial, 2);
            vParams.AddInInteger("NumeroDeCuotas", valRecord.NumeroDeCuotas);
            vParams.AddInDecimal("MontoDeLasCuotas", valRecord.TotalFactura, 2);
            vParams.AddInDecimal("MontoUltimaCuota", valRecord.TotalFactura, 2);
            vParams.AddInEnum("Talonario", valRecord.TalonarioAsDB);
            vParams.AddInEnum("FormaDePago", valRecord.FormaDePagoAsDB);
            vParams.AddInInteger("NumDiasDeVencimiento1aCuota", valRecord.NumDiasDeVencimiento1aCuota);
            vParams.AddInBoolean("EditarMontoCuota", valRecord.EditarMontoCuotaAsBool);
            vParams.AddInString("NumeroControl", valRecord.NumeroControl, 11);
            vParams.AddInEnum("TipoDeTransaccion", valRecord.TipoDeTransaccionAsDB);
            vParams.AddInString("NumeroFacturaAfectada", valRecord.NumeroFacturaAfectada, 11);
            vParams.AddInString("NumeroPlanillaExportacion", valRecord.NumeroPlanillaExportacion, 20);
            vParams.AddInEnum("TipoDeVenta", valRecord.TipoDeVentaAsDB);
            vParams.AddInBoolean("UsaMaquinaFiscal", valRecord.UsaMaquinaFiscalAsBool);
            vParams.AddInString("CodigoMaquinaRegistradora", valRecord.CodigoMaquinaRegistradora, 9);
            vParams.AddInString("NumeroDesde", valRecord.NumeroDesde, 20);
            vParams.AddInString("NumeroHasta", valRecord.NumeroHasta, 20);
            vParams.AddInString("NumeroControlHasta", valRecord.NumeroControlHasta, 11);
            vParams.AddInDecimal("MontoIvaRetenido", valRecord.MontoIvaRetenido, 2);
            vParams.AddInDateTime("FechaAplicacionRetIva", valRecord.FechaAplicacionRetIva);
            vParams.AddInInteger("NumeroComprobanteRetIva", valRecord.NumeroComprobanteRetIva);
            vParams.AddInDateTime("FechaComprobanteRetIva", valRecord.FechaComprobanteRetIva);
            vParams.AddInBoolean("SeRetuvoIVA", valRecord.SeRetuvoIVAAsBool);
            vParams.AddInBoolean("FacturaConPreciosSinIva", valRecord.FacturaConPreciosSinIvaAsBool);
            vParams.AddInDecimal("VueltoDelCobroDirecto", valRecord.VueltoDelCobroDirecto, 2);
            vParams.AddInInteger("ConsecutivoCaja", valRecord.ConsecutivoCaja);
            vParams.AddInBoolean("GeneraCobroDirecto", valRecord.GeneraCobroDirectoAsBool);
            vParams.AddInDateTime("FechaDeFacturaAfectada", valRecord.FechaDeFacturaAfectada);
            vParams.AddInDateTime("FechaDeEntrega", valRecord.FechaDeEntrega);
            vParams.AddInDecimal("PorcentajeDescuento1", valRecord.PorcentajeDescuento1, 2);
            vParams.AddInDecimal("PorcentajeDescuento2", valRecord.PorcentajeDescuento2, 2);
            vParams.AddInDecimal("MontoDescuento1", valRecord.MontoDescuento1, 2);
            vParams.AddInDecimal("MontoDescuento2", valRecord.MontoDescuento2, 2);
            vParams.AddInString("CodigoLote", valRecord.CodigoLote, 10);
            vParams.AddInBoolean("Devolucion", valRecord.DevolucionAsBool);
            vParams.AddInDecimal("PorcentajeAlicuota1", valRecord.PorcentajeAlicuota1, 2);
            vParams.AddInDecimal("PorcentajeAlicuota2", valRecord.PorcentajeAlicuota2, 2);
            vParams.AddInDecimal("PorcentajeAlicuota3", valRecord.PorcentajeAlicuota3, 2);
            vParams.AddInDecimal("MontoIvaAlicuota1", valRecord.MontoIvaAlicuota1, 2);
            vParams.AddInDecimal("MontoIvaAlicuota2", valRecord.MontoIvaAlicuota2, 2);
            vParams.AddInDecimal("MontoIvaAlicuota3", valRecord.MontoIvaAlicuota3, 2);
            vParams.AddInDecimal("MontoGravableAlicuota1", valRecord.MontoGravableAlicuota1, 2);
            vParams.AddInDecimal("MontoGravableAlicuota2", valRecord.MontoGravableAlicuota2, 2);
            vParams.AddInDecimal("MontoGravableAlicuota3", valRecord.MontoGravableAlicuota3, 2);
            vParams.AddInBoolean("RealizoCierreZ", valRecord.RealizoCierreZAsBool);
            vParams.AddInString("NumeroComprobanteFiscal", valRecord.NumeroComprobanteFiscal, 12);
            vParams.AddInString("SerialMaquinaFiscal", valRecord.SerialMaquinaFiscal, 15);
            vParams.AddInBoolean("AplicarPromocion", valRecord.AplicarPromocionAsBool);
            vParams.AddInBoolean("RealizoCierreX", valRecord.RealizoCierreXAsBool);
            vParams.AddInString("HoraModificacion", LibConvert.ToShortTimeStr(System.DateTime.Now), 5);
            vParams.AddInEnum("FormaDeCobro", valRecord.FormaDeCobroAsDB);
            vParams.AddInString("OtraFormaDeCobro", valRecord.OtraFormaDeCobro, 20);
            vParams.AddInString("NoCotizacionDeOrigen", valRecord.NoCotizacionDeOrigen, 11);
            vParams.AddInString("NoContrato", valRecord.NoContrato, 5);
            vParams.AddInInteger("ConsecutivoVehiculo", valRecord.ConsecutivoVehiculo);
            vParams.AddInInteger("ConsecutivoAlmacen", valRecord.ConsecutivoAlmacen);
            vParams.AddInInteger("GeneradaPorNotaEntrega", LibConvert.ToInt(valRecord.GeneradaPorNotaEntregaAsBool));
            vParams.AddInString("EmitidaEnFacturaNumero", valRecord.EmitidaEnFacturaNumero, 11);
            vParams.AddInString("CodigoMoneda", valRecord.CodigoMoneda, 4);
            vParams.AddInInteger("NumeroParaResumen", valRecord.NumeroParaResumen);
            vParams.AddInString("NumeroResumenDiario", valRecord.NumeroResumenDiario, 8);
            vParams.AddInString("NoControlDespachoDeOrigen", valRecord.NoControlDespachoDeOrigen, 30);
            vParams.AddInBoolean("ImprimeFiscal", valRecord.ImprimeFiscalAsBool);
            vParams.AddInBoolean("EsDiferida", valRecord.EsDiferidaAsBool);
            vParams.AddInBoolean("EsOriginalmenteDiferida", valRecord.EsOriginalmenteDiferidaAsBool);
            vParams.AddInBoolean("SeContabilizoIvaDiferido", valRecord.SeContabilizoIvaDiferidoAsBool);
            vParams.AddInBoolean("AplicaDecretoIvaEspecial", valRecord.AplicaDecretoIvaEspecialAsBool);
            vParams.AddInBoolean("EsGeneradaPorPuntoDeVenta", valRecord.EsGeneradaPorPuntoDeVentaAsBool);
            vParams.AddInDecimal("CambioMostrarTotalEnDivisas", valRecord.CambioMostrarTotalEnDivisas, 4);
            vParams.AddInString("CodigoMonedaDeCobro", valRecord.CodigoMonedaDeCobro, 4);
            vParams.AddInString("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInInteger("NroDiasMantenerCambioAMonedaLocal",valRecord.NumeroDiasMantenerCambioAMonedaLocal);
            vParams.AddInDateTime("FechaLimiteCambioAMonedaLocal",LibDate.Today());
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(FacturaRapida valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Numero", valRecord.Numero, 11);
            vParams.AddInEnum("TipoDeDocumento", valRecord.TipoDeDocumentoAsDB);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(FacturaRapida valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Numero", valRecord.Numero, 11);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataMasterComponent<IList<FacturaRapida>, IList<FacturaRapida>>

        LibResponse ILibDataMasterComponent<IList<FacturaRapida>, IList<FacturaRapida>>.CanBeChoosen(IList<FacturaRapida> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            FacturaRapida vRecord = refRecord[0];
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Punto de Venta.Eliminar")]
        LibResponse ILibDataMasterComponent<IList<FacturaRapida>, IList<FacturaRapida>>.Delete(IList<FacturaRapida> refRecord) {
            LibResponse vResult = new LibResponse();
            try {
                string vErrMsg = "";
                CurrentRecord = refRecord[0];
                if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                    if (ExecuteProcessBeforeDelete()) {
                        insTrn.StartTransaction();
                        vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "FacturaRapidaDEL"), ParametrosClave(CurrentRecord, true, true));
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

        IList<FacturaRapida> ILibDataMasterComponent<IList<FacturaRapida>, IList<FacturaRapida>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters, bool valUseDetail) {
            List<FacturaRapida> vResult = new List<FacturaRapida>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<FacturaRapida>(valProcessMessage, valParameters, CmdTimeOut);
                    if (valUseDetail && vResult != null && vResult.Count > 0) {
                        new clsFacturaRapidaDetalleDat().GetDetailAndAppendToMaster(ref vResult);
                    }
                    break;
                default:
                    throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Punto de Venta.Insertar")]
        LibResponse ILibDataMasterComponent<IList<FacturaRapida>, IList<FacturaRapida>>.Insert(IList<FacturaRapida> refRecord, bool valUseDetail) {
            LibResponse vResult = new LibResponse();
            try {
                CurrentRecord = refRecord[0];
                insTrn.StartTransaction();
                if (ExecuteProcessBeforeInsert()) {
                    if (ValidateMasterDetail(eAccionSR.Insertar, CurrentRecord, valUseDetail)) {
                        LibDatabase insDb = new LibDatabase();
                        CurrentRecord.Numero = LibXml.GetPropertyString(LibXml.ToXElement(insDb.LoadFromSp(insDb.ToSpName(DbSchema, "FacturaGeneraProximoNumero"), ParametrosGeneraProximoNumeroBorrador(CurrentRecord, eAccionSR.Insertar), CmdTimeOut)), "Numero");
                        if (insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "FacturaRapidaINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
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

        XElement ILibDataMasterComponent<IList<FacturaRapida>, IList<FacturaRapida>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoNumero")) {
                        vResult = LibXml.ValueToXElement(insDb.NextStrConsecutive(DbSchema + ".Factura", "Numero", valParameters, true, 11), "Numero");
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

        LibResponse ILibDataMasterComponent<IList<FacturaRapida>, IList<FacturaRapida>>.SpecializedUpdate(IList<FacturaRapida> refRecord, bool valUseDetail, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Punto de Venta.Modificar")]
        LibResponse ILibDataMasterComponent<IList<FacturaRapida>, IList<FacturaRapida>>.Update(IList<FacturaRapida> refRecord, bool valUseDetail, eAccionSR valAction) {
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

        bool ILibDataMasterComponent<IList<FacturaRapida>, IList<FacturaRapida>>.ValidateAll(IList<FacturaRapida> refRecords, bool valUseDetail, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (FacturaRapida vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }
        #endregion //ILibDataMasterComponent<IList<FacturaRapida>, IList<FacturaRapida>>

        LibResponse UpdateMaster(FacturaRapida refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "FacturaRapidaUPD"), ParametrosActualizacion(refRecord, valAction));
            return vResult;
        }

        LibResponse UpdateMasterAndDetail(FacturaRapida refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            string vErrorMessage = "";
            if (ValidateDetail(refRecord, eAccionSR.Modificar, out vErrorMessage)) {
                if (UpdateDetail(refRecord)) {
                    vResult = UpdateMaster(refRecord, valAction);
                }
            }
            return vResult;
        }

        private bool InsertDetail(FacturaRapida valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailFacturaRapidaDetalleAndUpdateDb(valRecord);
            return vResult;
        }

        private bool SetPkInDetailFacturaRapidaDetalleAndUpdateDb(FacturaRapida valRecord) {
            bool vResult = false;
            int vConsecutivo = 1;
            clsFacturaRapidaDetalleDat insFacturaRapidaDetalle = new clsFacturaRapidaDetalleDat();
            foreach (FacturaRapidaDetalle vDetail in valRecord.DetailFacturaRapidaDetalle) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.NumeroFactura = valRecord.Numero;
                vDetail.TipoDeDocumento = valRecord.TipoDeDocumentoAsDB;
                vDetail.ConsecutivoRenglon = vConsecutivo;
                vConsecutivo++;

            }
            vResult = insFacturaRapidaDetalle.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool UpdateDetail(FacturaRapida valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailFacturaRapidaDetalleAndUpdateDb(valRecord);
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Numero, CurrentRecord.TipoDeDocumentoAsEnum);
            //vResult = IsValidNumero(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Numero) && vResult;
            vResult = IsValidFecha(valAction, CurrentRecord.Fecha) && vResult;
            vResult = IsValidCodigoVendedor(valAction, CurrentRecord.CodigoVendedor) && vResult;
            //            vResult = IsValidTipoDeDocumento(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.TipoDeDocumento) && vResult;
            //vResult = IsValidFechaAplicacionRetIVA(valAction, CurrentRecord.FechaAplicacionRetIVA) && vResult;
            //vResult = IsValidConsecutivoCaja(valAction, CurrentRecord.ConsecutivoCaja) && vResult;
            //vResult = IsValidNumeroComprobanteFiscal(valAction, CurrentRecord.NumeroComprobanteFiscal) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, string valNumero, eTipoDocumentoFactura valTipoDeDocumentoAsEnum) {
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

        private bool IsValidNumero(eAccionSR valAction, int valConsecutivoCompania, string valNumero) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            LibDatabase insDb = new LibDatabase();
            if (valAction == eAccionSR.Insertar) {
                CurrentRecord.Numero = LibXml.GetPropertyString(LibXml.ToXElement(insDb.LoadFromSp(insDb.ToSpName(DbSchema, "FacturaGeneraProximoNumero"), ParametrosGeneraProximoNumeroBorrador(CurrentRecord, eAccionSR.Insertar), CmdTimeOut)), "Column1");
            } else if (valAction == eAccionSR.Modificar) {
                CurrentRecord.Numero = LibString.Trim(valNumero);
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

        private bool IsValidCodigoVendedor(eAccionSR valAction, string valCodigoVendedor) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoVendedor = LibString.Trim(valCodigoVendedor);
            if (LibString.IsNullOrEmpty(valCodigoVendedor, true)) {
                BuildValidationInfo(MsgRequiredField("Código del Vendedor"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Vendedor", "codigo", insDb.InsSql.ToSqlValue(valCodigoVendedor), true)) {
                    BuildValidationInfo("El valor asignado al campo Código del Vendedor no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidTipoDeDocumento(eAccionSR valAction, int valConsecutivoCompania, eTipoDocumentoFactura valTipoDeDocumento) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool IsValidFechaAplicacionRetIVA(eAccionSR valAction, DateTime valFechaAplicacionRetIVA) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaAplicacionRetIVA, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidConsecutivoCaja(eAccionSR valAction, int valConsecutivoCaja) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibString.IsNullOrEmpty(valConsecutivoCaja.ToString(), true)) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Caja"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Adm.Caja", "Consecutivo", insDb.InsSql.ToSqlValue(valConsecutivoCaja), true)) {
                    BuildValidationInfo("El valor asignado al campo Consecutivo Caja no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        //private bool IsValidNumeroComprobanteFiscal(eAccionSR valAction, string valNumeroComprobanteFiscal){
        //    bool vResult = true;
        //    if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
        //        return true;
        //    }
        //    valNumeroComprobanteFiscal = LibString.Trim(valNumeroComprobanteFiscal);
        //    if (LibString.IsNullOrEmpty(valNumeroComprobanteFiscal, true)) {
        //        BuildValidationInfo(MsgRequiredField("Núm Comprobante Fiscal"));
        //        vResult = false;
        //    }
        //    return vResult;
        //}

        private bool KeyExists(int valConsecutivoCompania, string valNumero, eTipoDocumentoFactura valTipoDeDocumento) {
            bool vResult = false;
            FacturaRapida vRecordBusqueda = new FacturaRapida();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Numero = valNumero;
            vRecordBusqueda.TipoDeDocumentoAsEnum = valTipoDeDocumento;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".Factura", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, FacturaRapida valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            vResult = insDb.ExistsRecord(DbSchema + ".Factura", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool ValidateMasterDetail(eAccionSR valAction, FacturaRapida valRecordMaster, bool valUseDetail) {
            bool vResult = false;
            string vErrMsg;
            if (Validate(valAction, out vErrMsg)) {
                if (valUseDetail) {
                    if (ValidateDetail(valRecordMaster, eAccionSR.Insertar, out vErrMsg)) {
                        vResult = true;
                    } else {
                        throw new GalacValidationException("Punto de Venta (detalle)\n" + vErrMsg);
                    }
                } else {
                    vResult = true;
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        private bool ValidateDetail(FacturaRapida valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            outErrorMessage = "";
            vResult = vResult && ValidateDetailFacturaRapidaDetalle(valRecord, valAction, out outErrorMessage);
            return vResult;
        }

        private bool ValidateDetailFacturaRapidaDetalle(FacturaRapida valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            foreach (FacturaRapidaDetalle vDetail in valRecord.DetailFacturaRapidaDetalle) {
                bool vLineHasError = true;
                //agregar validaciones
                if (LibString.IsNullOrEmpty(vDetail.Articulo)) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Código Inventario.");
                } else if (LibString.IsNullOrEmpty(vDetail.Descripcion)) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Descripción.");
                } else {
                    vLineHasError = false;
                }
                vResult = vResult && (!vLineHasError);
                vNumeroDeLinea++;
            }
            outErrorMessage = vSbErrorInfo.ToString();
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

        private StringBuilder ParametrosGeneraProximoNumeroBorrador(FacturaRapida valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInEnum("StatusFactura", valRecord.StatusFacturaAsDB);
            vParams.AddInEnum("TipoDeDocumento", valRecord.TipoDeDocumentoAsDB);
            vParams.AddInString("PrefijoFactura", "B-", 10);
            vParams.AddInInteger("NroCerosAlaIzquiera", 10);
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        System.Data.DataTable ILibDataRpt.GetDt(string valSqlStringCommand, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSqlStringCommand, valCmdTimeout);
        }
        System.Data.DataTable ILibDataRpt.GetDt(string valSpName, StringBuilder valXmlParamsExpression, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSpName, valXmlParamsExpression, valCmdTimeout);
        }

        #endregion //Metodos Generados
    } //End of class clsFacturaRapidaDat

} //End of namespace Galac.Adm.Dal.Venta

