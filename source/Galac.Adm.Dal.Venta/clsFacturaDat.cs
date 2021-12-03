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

namespace Galac.Adm.Dal.Venta {
    public class clsFacturaDat: LibData, ILibDataMasterComponentWithSearch<IList<Factura>, IList<Factura>>, ILibDataRpt {
        #region Variables
        LibTrn insTrn;
        Factura _CurrentRecord;
        #endregion //Variables

        #region Propiedades
        private Factura CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades

        #region Constructores
        public clsFacturaDat() {
            DbSchema = "Adm";
            insTrn = new LibTrn();
        }
        #endregion //Constructores

        #region Metodos Generados
        private StringBuilder ParametrosActualizacion(Factura valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Numero", valRecord.Numero, 11);
            vParams.AddInDateTime("Fecha", valRecord.Fecha);
            vParams.AddInString("CodigoCliente", valRecord.CodigoCliente, 10);
            vParams.AddInString("CodigoVendedor", valRecord.CodigoVendedor, 5);
            vParams.AddInString("Observaciones", valRecord.Observaciones, 7000);
            vParams.AddInDecimal("TotalMontoExento", valRecord.TotalMontoExento, 2);
            vParams.AddInDecimal("TotalBaseImponible", valRecord.TotalBaseImponible, 2);
            vParams.AddInDecimal("TotalRenglones", valRecord.TotalRenglones, 2);
            vParams.AddInDecimal("TotalIVA", valRecord.TotalIVA, 2);
            vParams.AddInDecimal("TotalFactura", valRecord.TotalFactura, 2);
            vParams.AddInDecimal("PorcentajeDescuento", valRecord.PorcentajeDescuento, 2);
            vParams.AddInString("CodigoNota1", valRecord.CodigoNota1, 10);
            vParams.AddInString("CodigoNota2", valRecord.CodigoNota2, 10);
            vParams.AddInString("Moneda", valRecord.Moneda, 80);
            vParams.AddInBoolean("NivelDePrecio", valRecord.NivelDePrecioAsBool);
            vParams.AddInBoolean("ReservarMercancia", valRecord.ReservarMercanciaAsBool);
            vParams.AddInDateTime("FechaDeRetiro", valRecord.FechaDeRetiro);
            vParams.AddInString("CodigoAlmacen", valRecord.CodigoAlmacen, 5);
            vParams.AddInBoolean("StatusFactura", valRecord.StatusFacturaAsBool);
            vParams.AddInBoolean("TipoDeDocumento", valRecord.TipoDeDocumentoAsBool);
            vParams.AddInBoolean("InsertadaManualmente", valRecord.InsertadaManualmenteAsBool);
            vParams.AddInBoolean("FacturaHistorica", valRecord.FacturaHistoricaAsBool);
            vParams.AddInBoolean("Cancelada", valRecord.CanceladaAsBool);
            vParams.AddInBoolean("UsarDireccionFiscal", valRecord.UsarDireccionFiscalAsBool);
            vParams.AddInInteger("NoDirDespachoAimprimir", valRecord.NoDirDespachoAimprimir);
            vParams.AddInDecimal("CambioABolivares", valRecord.CambioABolivares, 2);
            vParams.AddInDecimal("MontoDelAbono", valRecord.MontoDelAbono, 2);
            vParams.AddInDateTime("FechaDeVencimiento", valRecord.FechaDeVencimiento);
            vParams.AddInString("CondicionesDePago", valRecord.CondicionesDePago, 30);
            vParams.AddInBoolean("FormaDeLaInicial", valRecord.FormaDeLaInicialAsBool);
            vParams.AddInDecimal("PorcentajeDeLaInicial", valRecord.PorcentajeDeLaInicial, 2);
            vParams.AddInInteger("NumeroDeCuotas", valRecord.NumeroDeCuotas);
            vParams.AddInDecimal("MontoDeLasCuotas", valRecord.MontoDeLasCuotas, 2);
            vParams.AddInDecimal("MontoUltimaCuota", valRecord.MontoUltimaCuota, 2);
            vParams.AddInBoolean("Talonario", valRecord.TalonarioAsBool);
            vParams.AddInBoolean("FormaDePago", valRecord.FormaDePagoAsBool);
            vParams.AddInInteger("NumDiasDeVencimiento1aCuota", valRecord.NumDiasDeVencimiento1aCuota);
            vParams.AddInBoolean("EditarMontoCuota", valRecord.EditarMontoCuotaAsBool);
            vParams.AddInString("NumeroControl", valRecord.NumeroControl, 11);
            vParams.AddInBoolean("TipoDeTransaccion", valRecord.TipoDeTransaccionAsBool);
            vParams.AddInString("NumeroFacturaAfectada", valRecord.NumeroFacturaAfectada, 11);
            vParams.AddInString("NumeroPlanillaExportacion", valRecord.NumeroPlanillaExportacion, 20);
            vParams.AddInBoolean("TipoDeVenta", valRecord.TipoDeVentaAsBool);
            vParams.AddInBoolean("UsaMaquinaFiscal", valRecord.UsaMaquinaFiscalAsBool);
            vParams.AddInString("CodigoMaquinaRegistradora", valRecord.CodigoMaquinaRegistradora, 9);
            vParams.AddInString("NumeroDesde", valRecord.NumeroDesde, 20);
            vParams.AddInString("NumeroHasta", valRecord.NumeroHasta, 20);
            vParams.AddInString("NumeroControlHasta", valRecord.NumeroControlHasta, 11);
            vParams.AddInDecimal("MontoIvaRetenido", valRecord.MontoIvaRetenido, 2);
            vParams.AddInDateTime("FechaAplicacionRetIVA", valRecord.FechaAplicacionRetIVA);
            vParams.AddInInteger("NumeroComprobanteRetIVA", valRecord.NumeroComprobanteRetIVA);
            vParams.AddInDateTime("FechaComprobanteRetIVA", valRecord.FechaComprobanteRetIVA);
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
            vParams.AddInDecimal("MontoIVAAlicuota1", valRecord.MontoIVAAlicuota1, 2);
            vParams.AddInDecimal("MontoIVAAlicuota2", valRecord.MontoIVAAlicuota2, 2);
            vParams.AddInDecimal("MontoIVAAlicuota3", valRecord.MontoIVAAlicuota3, 2);
            vParams.AddInDecimal("MontoGravableAlicuota1", valRecord.MontoGravableAlicuota1, 2);
            vParams.AddInDecimal("MontoGravableAlicuota2", valRecord.MontoGravableAlicuota2, 2);
            vParams.AddInDecimal("MontoGravableAlicuota3", valRecord.MontoGravableAlicuota3, 2);
            vParams.AddInBoolean("RealizoCierreZ", valRecord.RealizoCierreZAsBool);
            vParams.AddInString("NumeroComprobanteFiscal", valRecord.NumeroComprobanteFiscal, 12);
            vParams.AddInString("SerialMaquinaFiscal", valRecord.SerialMaquinaFiscal, 15);
            vParams.AddInBoolean("AplicarPromocion", valRecord.AplicarPromocionAsBool);
            vParams.AddInBoolean("RealizoCierreX", valRecord.RealizoCierreXAsBool);
            vParams.AddInString("HoraModificacion", valRecord.HoraModificacion, 5);
            vParams.AddInBoolean("FormaDeCobro", valRecord.FormaDeCobroAsBool);
            vParams.AddInString("OtraFormaDeCobro", valRecord.OtraFormaDeCobro, 20);
            vParams.AddInString("NoCotizacionDeOrigen", valRecord.NoCotizacionDeOrigen, 20);
            vParams.AddInString("NoContrato", valRecord.NoContrato, 5);
            vParams.AddInInteger("ConsecutivoVehiculo", valRecord.ConsecutivoVehiculo);
            vParams.AddInInteger("ConsecutivoAlmacen", valRecord.ConsecutivoAlmacen);
            vParams.AddInString("NumeroResumenDiario", valRecord.NumeroResumenDiario, 8);
            vParams.AddInString("NoControlDespachoDeOrigen", valRecord.NoControlDespachoDeOrigen, 30);
            vParams.AddInBoolean("ImprimeFiscal", valRecord.ImprimeFiscalAsBool);
            vParams.AddInBoolean("EsDiferida", valRecord.EsDiferidaAsBool);
            vParams.AddInBoolean("EsOriginalmenteDiferida", valRecord.EsOriginalmenteDiferidaAsBool);
            vParams.AddInBoolean("SeContabilizoIvaDiferido", valRecord.SeContabilizoIvaDiferidoAsBool);
            vParams.AddInBoolean("AplicaDecretoIvaEspecial", valRecord.AplicaDecretoIvaEspecialAsBool);
            vParams.AddInBoolean("EsGeneradaPorPuntoDeVenta", valRecord.EsGeneradaPorPuntoDeVentaAsBool);
            vParams.AddInDecimal("CambioMonedaCXC", valRecord.CambioMonedaCXC, 2);
            vParams.AddInDecimal("CambioMostrarTotalEnDivisas", valRecord.CambioMostrarTotalEnDivisas, 2);
            vParams.AddInString("CodigoMonedaDeCobro", valRecord.CodigoMonedaDeCobro, 4);
            vParams.AddInBoolean("GeneradaPorNotaEntrega", valRecord.GeneradaPorNotaEntregaAsBool);
            vParams.AddInString("EmitidaEnFacturaNumero", valRecord.EmitidaEnFacturaNumero, 11);
            vParams.AddInString("CodigoMoneda", valRecord.CodigoMoneda, 4);
            vParams.AddInInteger("NumeroParaResumen", valRecord.NumeroParaResumen);
            vParams.AddInInteger("NroDiasMantenerCambioAMonedaLocal", valRecord.NroDiasMantenerCambioAMonedaLocal);
            vParams.AddInDateTime("FechaLimiteCambioAMonedaLocal", valRecord.FechaLimiteCambioAMonedaLocal);
            vParams.AddInBoolean("GeneradoPor", valRecord.GeneradoPorAsBool);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(Factura valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Numero", valRecord.Numero, 11);
            vParams.AddInString("TipoDeDocumento", valRecord.TipoDeDocumento, 0);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(Factura valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Numero", valRecord.Numero, 11);
            vResult = vParams.Get();
            return vResult;
        }

        #region Miembros de ILibDataMasterComponent<IList<Factura>, IList<Factura>>
        LibResponse ILibDataMasterComponent<IList<Factura>, IList<Factura>>.CanBeChoosen(IList<Factura> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Factura vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValueOnMultifile("dbo.Despacho", "NoFactura", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Numero), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Despacho");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Participante", "NumeroFactura", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Numero), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Participante");
                }
                if (insDB.ExistsValueOnMultifile("dbo.FacturasVendedor", "NumeroFactura", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Numero), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Facturas Vendedor");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Factura.Eliminar")]
        LibResponse ILibDataMasterComponent<IList<Factura>, IList<Factura>>.Delete(IList<Factura> refRecord) {
            LibResponse vResult = new LibResponse();
            try {
                string vErrMsg = "";
                CurrentRecord = refRecord[0];
                if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                    if (ExecuteProcessBeforeDelete()) {
                        insTrn.StartTransaction();
                        vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "FacturaDEL"), ParametrosClave(CurrentRecord, true, true));
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

        IList<Factura> ILibDataMasterComponent<IList<Factura>, IList<Factura>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters, bool valUseDetail) {
            List<Factura> vResult = new List<Factura>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Factura>(valProcessMessage, valParameters, CmdTimeOut);
                    if (valUseDetail && vResult != null && vResult.Count > 0) {
                        //new clsRenglonFacturaDat().GetDetailAndAppendToMaster(ref vResult);
                        //new clsRenglonCobroDeFacturaDat().GetDetailAndAppendToMaster(ref vResult);
                    }
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Factura.Insertar")]
        LibResponse ILibDataMasterComponent<IList<Factura>, IList<Factura>>.Insert(IList<Factura> refRecord, bool valUseDetail) {
            LibResponse vResult = new LibResponse();
            try {
                CurrentRecord = refRecord[0];
                insTrn.StartTransaction();
                if (ExecuteProcessBeforeInsert()) {
                    if (ValidateMasterDetail(eAccionSR.Insertar, CurrentRecord, valUseDetail)) {
                        if (insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "FacturaINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
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

        XElement ILibDataMasterComponent<IList<Factura>, IList<Factura>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
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

        LibResponse ILibDataMasterComponent<IList<Factura>, IList<Factura>>.SpecializedUpdate(IList<Factura> refRecord,  bool valUseDetail, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Factura.Modificar")]
        LibResponse ILibDataMasterComponent<IList<Factura>, IList<Factura>>.Update(IList<Factura> refRecord, bool valUseDetail, eAccionSR valAction) {
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

        bool ILibDataMasterComponent<IList<Factura>, IList<Factura>>.ValidateAll(IList<Factura> refRecords, bool valUseDetail, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (Factura vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }
        #endregion //ILibDataMasterComponent<IList<Factura>, IList<Factura>>

        LibResponse UpdateMaster(Factura refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            vResult.Success = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "FacturaUPD"), ParametrosActualizacion(refRecord, valAction));
            return vResult;
        }

        LibResponse UpdateMasterAndDetail(Factura refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            string vErrorMessage = "";
            if (ValidateDetail(refRecord, eAccionSR.Modificar,out vErrorMessage)) {
                if (UpdateDetail(refRecord)) {
                    vResult = UpdateMaster(refRecord, valAction);
                }
            }
            return vResult;
        }

        private bool InsertDetail(Factura valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailRenglonFacturaAndUpdateDb(valRecord);
            vResult = vResult && SetPkInDetailRenglonCobroDeFacturaAndUpdateDb(valRecord);
            return vResult;
        }

        private bool SetPkInDetailRenglonFacturaAndUpdateDb(Factura valRecord) {
            bool vResult = false;
            //int vConsecutivo = 1;
            //clsRenglonFacturaDat insRenglonFactura = new clsRenglonFacturaDat();
            //foreach (RenglonFactura vDetail in valRecord.DetailRenglonFactura) {
            //    vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
            //    vDetail.NumeroFactura = valRecord.Numero;
            //    vDetail.TipoDeDocumento = valRecord.TipoDeDocumento;
            //    vDetail.ConsecutivoRenglon = vConsecutivo;
            //    vConsecutivo++;
            //}
            //vResult = insRenglonFactura.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool SetPkInDetailRenglonCobroDeFacturaAndUpdateDb(Factura valRecord) {
            bool vResult = false;
            /*int vConsecutivo = 1;
            clsRenglonCobroDeFacturaDat insRenglonCobroDeFactura = new clsRenglonCobroDeFacturaDat();
            foreach (RenglonCobroDeFactura vDetail in valRecord.DetailRenglonCobroDeFactura) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.NumeroFactura = valRecord.Numero;
                vDetail.TipoDeDocumento = valRecord.TipoDeDocumento;
                vDetail.ConsecutivoRenglon = vConsecutivo;
                vConsecutivo++;
            }
            vResult = insRenglonCobroDeFactura.InsertChild(valRecord, insTrn);*/
            return vResult;
        }

        private bool UpdateDetail(Factura valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailRenglonFacturaAndUpdateDb(valRecord);
            vResult = vResult && SetPkInDetailRenglonCobroDeFacturaAndUpdateDb(valRecord);
            return vResult;
        }

        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Numero, CurrentRecord.TipoDeDocumentoAsBool);
            vResult = IsValidNumero(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Numero, CurrentRecord.TipoDeDocumentoAsBool) && vResult;
            vResult = IsValidFecha(valAction, CurrentRecord.Fecha) && vResult;
            vResult = IsValidFechaDeRetiro(valAction, CurrentRecord.FechaDeRetiro) && vResult;
            vResult = IsValidFechaDeVencimiento(valAction, CurrentRecord.FechaDeVencimiento) && vResult;
            vResult = IsValidFechaAplicacionRetIVA(valAction, CurrentRecord.FechaAplicacionRetIVA) && vResult;
            vResult = IsValidFechaComprobanteRetIVA(valAction, CurrentRecord.FechaComprobanteRetIVA) && vResult;
            vResult = IsValidFechaDeFacturaAfectada(valAction, CurrentRecord.FechaDeFacturaAfectada) && vResult;
            vResult = IsValidFechaDeEntrega(valAction, CurrentRecord.FechaDeEntrega) && vResult;
            vResult = IsValidFechaLimiteCambioAMonedaLocal(valAction, CurrentRecord.FechaLimiteCambioAMonedaLocal) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, string valNumero, bool valTipoDeDocumentoAsBool){
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

        private bool IsValidNumero(eAccionSR valAction, int valConsecutivoCompania, string valNumero, bool valTipoDeDocumentoAsBool)
        {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumero = LibString.Trim(valNumero);
            if (LibString.IsNullOrEmpty(valNumero, true)) {
                BuildValidationInfo(MsgRequiredField("Numero"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valNumero, valTipoDeDocumentoAsBool)) {
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

        private bool IsValidFechaDeRetiro(eAccionSR valAction, DateTime valFechaDeRetiro){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaDeRetiro, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaDeVencimiento(eAccionSR valAction, DateTime valFechaDeVencimiento){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaDeVencimiento, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaAplicacionRetIVA(eAccionSR valAction, DateTime valFechaAplicacionRetIVA){
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

        private bool IsValidFechaComprobanteRetIVA(eAccionSR valAction, DateTime valFechaComprobanteRetIVA){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaComprobanteRetIVA, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaDeFacturaAfectada(eAccionSR valAction, DateTime valFechaDeFacturaAfectada){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaDeFacturaAfectada, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaDeEntrega(eAccionSR valAction, DateTime valFechaDeEntrega){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaDeEntrega, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaLimiteCambioAMonedaLocal(eAccionSR valAction, DateTime valFechaLimiteCambioAMonedaLocal){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaLimiteCambioAMonedaLocal, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valNumero, bool valTipoDeDocumento) {
            bool vResult = false;
            Factura vRecordBusqueda = new Factura();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Numero = valNumero;
            vRecordBusqueda.TipoDeDocumentoAsBool = valTipoDeDocumento;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".Factura", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool ValidateMasterDetail(eAccionSR valAction, Factura valRecordMaster, bool valUseDetail) {
            bool vResult = false;
            string vErrMsg;
            if (Validate(valAction, out vErrMsg)) {
                if (valUseDetail) {
                    if (ValidateDetail(valRecordMaster, eAccionSR.Insertar, out vErrMsg)) {
                        vResult = true;
                    } else {
                        throw new GalacValidationException("Factura (detalle)\n" + vErrMsg);
                    }
                } else {
                    vResult = true;
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        private bool ValidateDetail(Factura valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            outErrorMessage = "";
            vResult = vResult && ValidateDetailRenglonFactura(valRecord, valAction, out outErrorMessage);
            vResult = vResult && ValidateDetailRenglonCobroDeFactura(valRecord, valAction, out outErrorMessage);
            return vResult;
        }

        private bool ValidateDetailRenglonFactura(Factura valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            //StringBuilder vSbErrorInfo = new StringBuilder();
            //int vNumeroDeLinea = 1;
            outErrorMessage = string.Empty;
            //foreach (RenglonFactura vDetail in valRecord.DetailRenglonFactura) {
            //    bool vLineHasError = true;
            //    //agregar validaciones
            //    if (LibString.IsNullOrEmpty(vDetail.Articulo)) {
            //        vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Código Inventario.");
            //    } else if (LibString.IsNullOrEmpty(vDetail.CodigoVendedor1)) {
            //        vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Cód Vendededor1.");
            //    } else if (LibString.IsNullOrEmpty(vDetail.CodigoVendedor2)) {
            //        vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Cód Vendededor2.");
            //    } else if (LibString.IsNullOrEmpty(vDetail.CodigoVendedor3)) {
            //        vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Cód Vendededor3.");
            //    } else {
            //        vLineHasError = false;
            //    }
            //    vResult = vResult && (!vLineHasError);
            //    vNumeroDeLinea++;
            //}
            //if (!vResult) {
            //    outErrorMessage = "Renglon Factura"  + Environment.NewLine + vSbErrorInfo.ToString();
            //}
            return vResult;
        }

        private bool ValidateDetailRenglonCobroDeFactura(Factura valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            outErrorMessage = string.Empty;
            foreach (RenglonCobroDeFactura vDetail in valRecord.DetailRenglonCobroDeFactura) {
                bool vLineHasError = true;
                //agregar validaciones
                if (LibString.IsNullOrEmpty(vDetail.CodigoFormaDelCobro)) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Codigo Forma Del Cobro.");
                } else if (vDetail.CodigoBanco == 0) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Codigo Banco.");
                } else {
                    vLineHasError = false;
                }
                vResult = vResult && (!vLineHasError);
                vNumeroDeLinea++;
            }
            if (!vResult) {
                outErrorMessage = "Renglon Cobro De Factura"  + Environment.NewLine + vSbErrorInfo.ToString();
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

    } //End of class clsFacturaDat

} //End of namespace Galac.Adm.Dal.Venta

