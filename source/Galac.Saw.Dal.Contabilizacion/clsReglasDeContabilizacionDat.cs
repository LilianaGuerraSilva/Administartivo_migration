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
using Galac.Saw.Ccl.Contabilizacion;

namespace Galac.Saw.Dal.Contabilizacion {
    public class clsReglasDeContabilizacionDat : LibData, ILibDataComponent<IList<ReglasDeContabilizacion>, IList<ReglasDeContabilizacion>> {
        #region Variables
        ReglasDeContabilizacion _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private ReglasDeContabilizacion CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsReglasDeContabilizacionDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(ReglasDeContabilizacion valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Numero", valRecord.Numero, 11);
            vParams.AddInString("DiferenciaEnCambioyCalculo", valRecord.DiferenciaEnCambioyCalculo, 30);
            vParams.AddInString("CuentaIva1Credito", valRecord.CuentaIva1Credito, 30);
            vParams.AddInString("CuentaIva1Debito", valRecord.CuentaIva1Debito, 30);
            vParams.AddInEnum("DondeContabilizarRetIva", valRecord.DondeContabilizarRetIvaAsDB);
            vParams.AddInString("CuentaRetencionIva", valRecord.CuentaRetencionIva, 30);
            vParams.AddInString("CuentaDiferenciaCambiaria", valRecord.CuentaDiferenciaCambiaria, 30);
            vParams.AddInString("CxCTipoComprobante", valRecord.CxCTipoComprobante, 2);
            vParams.AddInEnum("TipoContabilizacionCxC", valRecord.TipoContabilizacionCxCAsDB);
            vParams.AddInEnum("ContabIndividualCxc", valRecord.ContabIndividualCxcAsDB);
            vParams.AddInEnum("ContabPorLoteCxC", valRecord.ContabPorLoteCxCAsDB);
            vParams.AddInString("CuentaCxCClientes", valRecord.CuentaCxCClientes, 30);
            vParams.AddInString("CuentaCxCIngresos", valRecord.CuentaCxCIngresos, 30);
            vParams.AddInString("CxPTipoComprobante", valRecord.CxPTipoComprobante, 2);
            vParams.AddInEnum("TipoContabilizacionCxP", valRecord.TipoContabilizacionCxPAsDB);
            vParams.AddInEnum("ContabIndividualCxP", valRecord.ContabIndividualCxPAsDB);
            vParams.AddInEnum("ContabPorLoteCxP", valRecord.ContabPorLoteCxPAsDB);
            vParams.AddInString("CuentaCxPGasto", valRecord.CuentaCxPGasto, 30);
            vParams.AddInString("CuentaCxPProveedores", valRecord.CuentaCxPProveedores, 30);
            vParams.AddInString("CuentaRetencionImpuestoMunicipal", valRecord.CuentaRetencionImpuestoMunicipal, 30);
            vParams.AddInString("CobranzaTipoComprobante", valRecord.CobranzaTipoComprobante, 2);
            vParams.AddInEnum("TipoContabilizacionCobranza", valRecord.TipoContabilizacionCobranzaAsDB);
            vParams.AddInEnum("ContabIndividualCobranza", valRecord.ContabIndividualCobranzaAsDB);
            vParams.AddInEnum("ContabPorLoteCobranza", valRecord.ContabPorLoteCobranzaAsDB);
            vParams.AddInString("CuentaCobranzaCobradoEnEfectivo", valRecord.CuentaCobranzaCobradoEnEfectivo, 30);
            vParams.AddInString("CuentaCobranzaCobradoEnCheque", valRecord.CuentaCobranzaCobradoEnCheque, 30);
            vParams.AddInString("CuentaCobranzaCobradoEnTarjeta", valRecord.CuentaCobranzaCobradoEnTarjeta, 30);
            vParams.AddInString("cuentaCobranzaRetencionISLR", valRecord.cuentaCobranzaRetencionISLR, 30);
            vParams.AddInString("cuentaCobranzaRetencionIVA", valRecord.cuentaCobranzaRetencionIVA, 30);
            vParams.AddInString("CuentaCobranzaOtros", valRecord.CuentaCobranzaOtros, 30);
            vParams.AddInString("CuentaCobranzaCxCClientes", valRecord.CuentaCobranzaCxCClientes, 30);
            vParams.AddInString("CuentaCobranzaCobradoAnticipo", valRecord.CuentaCobranzaCobradoAnticipo, 30);
            vParams.AddInString("CuentaCobranzaIvaDiferido", valRecord.CuentaCobranzaIvaDiferido, 30);
            vParams.AddInString("PagoTipoComprobante", valRecord.PagoTipoComprobante, 2);
            vParams.AddInBoolean("ManejarDiferenciaCambiariaEnCobranza", valRecord.ManejarDiferenciaCambiariaEnCobranzaAsBool);
            vParams.AddInEnum("TipoContabilizacionPagos", valRecord.TipoContabilizacionPagosAsDB);
            vParams.AddInEnum("ContabIndividualPagos", valRecord.ContabIndividualPagosAsDB);
            vParams.AddInEnum("ContabPorLotePagos", valRecord.ContabPorLotePagosAsDB);
            vParams.AddInString("CuentaPagosCxPProveedores", valRecord.CuentaPagosCxPProveedores, 30);
            vParams.AddInString("CuentaPagosRetencionISLR", valRecord.CuentaPagosRetencionISLR, 30);
            vParams.AddInString("CuentaPagosOtros", valRecord.CuentaPagosOtros, 30);
            vParams.AddInString("CuentaPagosBanco", valRecord.CuentaPagosBanco, 30);
            vParams.AddInString("CuentaPagosPagadoAnticipo", valRecord.CuentaPagosPagadoAnticipo, 30);
            vParams.AddInBoolean("ManejarDiferenciaCambiariaEnPagos", valRecord.ManejarDiferenciaCambiariaEnPagosAsBool);
            vParams.AddInEnum("TipoContabilizacionFacturacion", valRecord.TipoContabilizacionFacturacionAsDB);
            vParams.AddInEnum("ContabIndividualFacturacion", valRecord.ContabIndividualFacturacionAsDB);
            vParams.AddInEnum("ContabPorLoteFacturacion", valRecord.ContabPorLoteFacturacionAsDB);
            vParams.AddInString("CuentaFacturacionCxCClientes", valRecord.CuentaFacturacionCxCClientes, 30);
            vParams.AddInString("CuentaFacturacionMontoTotalFactura", valRecord.CuentaFacturacionMontoTotalFactura, 30);
            vParams.AddInString("CuentaFacturacionCargos", valRecord.CuentaFacturacionCargos, 30);
            vParams.AddInString("CuentaFacturacionDescuentos", valRecord.CuentaFacturacionDescuentos, 30);
            vParams.AddInString("CuentaFacturacionIvaDiferido", valRecord.CuentaFacturacionIvaDiferido, 30);
            vParams.AddInBoolean("ContabilizarPorArticulo", valRecord.ContabilizarPorArticuloAsBool);
            vParams.AddInBoolean("AgruparPorCuentaDeArticulo", valRecord.AgruparPorCuentaDeArticuloAsBool);
            vParams.AddInBoolean("AgruparPorCargosDescuentos", valRecord.AgruparPorCargosDescuentosAsBool);
            vParams.AddInString("FacturaTipoComprobante", valRecord.FacturaTipoComprobante, 2);
            vParams.AddInEnum("TipoContabilizacionRDVtas", valRecord.TipoContabilizacionRDVtasAsDB);
            vParams.AddInEnum("ContabIndividualRDVtas", valRecord.ContabIndividualRDVtasAsDB);
            vParams.AddInEnum("ContabPorLoteRDVtas", valRecord.ContabPorLoteRDVtasAsDB);
            vParams.AddInString("CuentaRDVtasCaja", valRecord.CuentaRDVtasCaja, 30);
            vParams.AddInString("CuentaRDVtasMontoTotal", valRecord.CuentaRDVtasMontoTotal, 30);
            vParams.AddInBoolean("ContabilizarPorArticuloRDVtas", valRecord.ContabilizarPorArticuloRDVtasAsBool);
            vParams.AddInBoolean("AgruparPorCuentaDeArticuloRDVtas", valRecord.AgruparPorCuentaDeArticuloRDVtasAsBool);
            vParams.AddInString("MovimientoBancarioTipoComprobante", valRecord.MovimientoBancarioTipoComprobante, 2);
            vParams.AddInEnum("TipoContabilizacionMovBancario", valRecord.TipoContabilizacionMovBancarioAsDB);
            vParams.AddInEnum("ContabIndividualMovBancario", valRecord.ContabIndividualMovBancarioAsDB);
            vParams.AddInEnum("ContabPorLoteMovBancario", valRecord.ContabPorLoteMovBancarioAsDB);
            vParams.AddInString("CuentaMovBancarioGasto", valRecord.CuentaMovBancarioGasto, 30);
            vParams.AddInString("CuentaMovBancarioBancosHaber", valRecord.CuentaMovBancarioBancosHaber, 30);
            vParams.AddInString("CuentaMovBancarioBancosDebe", valRecord.CuentaMovBancarioBancosDebe, 30);
            vParams.AddInString("CuentaMovBancarioIngresos", valRecord.CuentaMovBancarioIngresos, 30);
            vParams.AddInString("CuentaDebitoBancarioGasto", valRecord.CuentaDebitoBancarioGasto, 30);
            vParams.AddInString("CuentaDebitoBancarioBancos", valRecord.CuentaDebitoBancarioBancos, 30);
            vParams.AddInString("CuentaCreditoBancarioGasto", valRecord.CuentaCreditoBancarioGasto, 30);
            vParams.AddInString("CuentaCreditoBancarioBancos", valRecord.CuentaCreditoBancarioBancos, 30);
            vParams.AddInString("AnticipoTipoComprobante", valRecord.AnticipoTipoComprobante, 2);
            vParams.AddInEnum("TipoContabilizacionAnticipo", valRecord.TipoContabilizacionAnticipoAsDB);
            vParams.AddInEnum("ContabIndividualAnticipo", valRecord.ContabIndividualAnticipoAsDB);
            vParams.AddInEnum("ContabPorLoteAnticipo", valRecord.ContabPorLoteAnticipoAsDB);
            vParams.AddInString("CuentaAnticipoCaja", valRecord.CuentaAnticipoCaja, 30);
            vParams.AddInString("CuentaAnticipoCobrado", valRecord.CuentaAnticipoCobrado, 30);
            vParams.AddInString("CuentaAnticipoOtrosIngresos", valRecord.CuentaAnticipoOtrosIngresos, 30);
            vParams.AddInString("CuentaAnticipoPagado", valRecord.CuentaAnticipoPagado, 30);
            vParams.AddInString("CuentaAnticipoBanco", valRecord.CuentaAnticipoBanco, 30);
            vParams.AddInString("CuentaAnticipoOtrosEgresos", valRecord.CuentaAnticipoOtrosEgresos, 30);
            vParams.AddInString("CuentaCostoDeVenta", valRecord.CuentaCostoDeVenta, 30);
            vParams.AddInString("CuentaInventario", valRecord.CuentaInventario, 30);
            vParams.AddInEnum("TipoContabilizacionInventario", valRecord.TipoContabilizacionInventarioAsDB);
            vParams.AddInBoolean("AgruparPorCuentaDeArticuloInven", valRecord.AgruparPorCuentaDeArticuloInvenAsBool);
            vParams.AddInString("InventarioTipoComprobante", valRecord.InventarioTipoComprobante, 2);
            vParams.AddInString("CtaDePagosSueldos", valRecord.CtaDePagosSueldos, 30);
            vParams.AddInString("CtaDePagosSueldosBanco", valRecord.CtaDePagosSueldosBanco, 30);
            vParams.AddInEnum("ContabIndividualPagosSueldos", valRecord.ContabIndividualPagosSueldosAsDB);
            vParams.AddInString("PagosSueldosTipoComprobante", valRecord.PagosSueldosTipoComprobante, 2);
            vParams.AddInEnum("TipoContabilizacionDePagosSueldos", valRecord.TipoContabilizacionDePagosSueldosAsDB);
            vParams.AddInBoolean("EditarComprobanteDePagosSueldos", valRecord.EditarComprobanteDePagosSueldosAsBool);
            vParams.AddInBoolean("EditarComprobanteAfterInsertCxC", valRecord.EditarComprobanteAfterInsertCxCAsBool);
            vParams.AddInBoolean("EditarComprobanteAfterInsertCxP", valRecord.EditarComprobanteAfterInsertCxPAsBool);
            vParams.AddInBoolean("EditarComprobanteAfterInsertCobranza", valRecord.EditarComprobanteAfterInsertCobranzaAsBool);
            vParams.AddInBoolean("EditarComprobanteAfterInsertPagos", valRecord.EditarComprobanteAfterInsertPagosAsBool);
            vParams.AddInBoolean("EditarComprobanteAfterInsertFactura", valRecord.EditarComprobanteAfterInsertFacturaAsBool);
            vParams.AddInBoolean("EditarComprobanteAfterInsertResDia", valRecord.EditarComprobanteAfterInsertResDiaAsBool);
            vParams.AddInBoolean("EditarComprobanteAfterInsertMovBan", valRecord.EditarComprobanteAfterInsertMovBanAsBool);
            vParams.AddInBoolean("EditarComprobanteAfterInsertImpTraBan", valRecord.EditarComprobanteAfterInsertImpTraBanAsBool);
            vParams.AddInBoolean("EditarComprobanteAfterInsertAnticipo", valRecord.EditarComprobanteAfterInsertAnticipoAsBool);
            vParams.AddInBoolean("EditarComprobanteAfterInsertInventario", valRecord.EditarComprobanteAfterInsertInventarioAsBool);
            vParams.AddInBoolean("EditarComprobanteAfterInsertCajaChica", valRecord.EditarComprobanteAfterInsertCajaChicaAsBool);
            vParams.AddInString("SiglasTipoComprobanteCajaChica", valRecord.SiglasTipoComprobanteCajaChica, 2);
            vParams.AddInEnum("ContabIndividualCajaChica", valRecord.ContabIndividualCajaChicaAsDB);
            vParams.AddInString("CuentaCajaChicaGasto", valRecord.CuentaCajaChicaGasto, 30);
            vParams.AddInBoolean("MostrarDesglosadoCajaChica", valRecord.MostrarDesglosadoCajaChicaAsBool);
            vParams.AddInString("CuentaCajaChicaBancoHaber", valRecord.CuentaCajaChicaBancoHaber, 30);
            vParams.AddInString("CuentaCajaChicaBancoDebe", valRecord.CuentaCajaChicaBancoDebe, 30);
            vParams.AddInString("CuentaCajaChicaBanco", valRecord.CuentaCajaChicaBanco, 30);
            vParams.AddInString("SiglasTipoComprobanteRendiciones", valRecord.SiglasTipoComprobanteRendiciones, 2);
            vParams.AddInEnum("ContabIndividualRendiciones", valRecord.ContabIndividualRendicionesAsDB);
            vParams.AddInString("CuentaRendicionesGasto", valRecord.CuentaRendicionesGasto, 30);
            vParams.AddInString("CuentaRendicionesBanco", valRecord.CuentaRendicionesBanco, 30);
            vParams.AddInString("CuentaRendicionesAnticipos", valRecord.CuentaRendicionesAnticipos, 30);
            vParams.AddInBoolean("MostrarDesglosadoRendiciones", valRecord.MostrarDesglosadoRendicionesAsBool);
            vParams.AddInEnum("TipoContabilizacionTransfCtas", valRecord.TipoContabilizacionTransfCtasAsDB);
            vParams.AddInEnum("ContabIndividualTransfCtas", valRecord.ContabIndividualTransfCtasAsDB);
            vParams.AddInEnum("ContabPorLoteTransfCtas", valRecord.ContabPorLoteTransfCtasAsDB);
            vParams.AddInString("CuentaTransfCtasBancoDestino", valRecord.CuentaTransfCtasBancoDestino, 30);
            vParams.AddInString("CuentaTransfCtasGastoComOrigen", valRecord.CuentaTransfCtasGastoComOrigen, 30);
            vParams.AddInString("CuentaTransfCtasGastoComDestino", valRecord.CuentaTransfCtasGastoComDestino, 30);
            vParams.AddInString("CuentaTransfCtasBancoOrigen", valRecord.CuentaTransfCtasBancoOrigen, 30);
            vParams.AddInString("TransfCtasSigasTipoComprobante", valRecord.TransfCtasSigasTipoComprobante, 2);
            vParams.AddInBoolean("EditarComprobanteAfterInsertTransfCtas", valRecord.EditarComprobanteAfterInsertTransfCtasAsBool);
            vParams.AddInEnum("TipoContabilizacionOrdenDeProduccion", valRecord.TipoContabilizacionOrdenDeProduccionAsDB);
            vParams.AddInEnum("ContabIndividualOrdenDeProduccion", valRecord.ContabIndividualOrdenDeProduccionAsDB);
            vParams.AddInEnum("ContabPorLoteOrdenDeProduccion", valRecord.ContabPorLoteOrdenDeProduccionAsDB);
            vParams.AddInString("CuentaOrdenDeProduccionProductoTerminado", valRecord.CuentaOrdenDeProduccionProductoTerminado, 30);
            vParams.AddInString("CuentaOrdenDeProduccionMateriaPrima", valRecord.CuentaOrdenDeProduccionMateriaPrima, 30);
            vParams.AddInString("CuentaMermaAnormal", valRecord.CuentaMermaAnormal, 30);
            vParams.AddInString("OrdenDeProduccionTipoComprobante", valRecord.OrdenDeProduccionTipoComprobante, 2);
            vParams.AddInBoolean("EditarComprobanteAfterInsertOrdenDeProduccion", valRecord.EditarComprobanteAfterInsertOrdenDeProduccionAsBool);
            vParams.AddInString("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(ReglasDeContabilizacion valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Numero", valRecord.Numero, 11);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(ReglasDeContabilizacion valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<ReglasDeContabilizacion>, IList<ReglasDeContabilizacion>>

        LibResponse ILibDataComponent<IList<ReglasDeContabilizacion>, IList<ReglasDeContabilizacion>>.CanBeChoosen(IList<ReglasDeContabilizacion> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            ReglasDeContabilizacion vRecord = refRecord[0];
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

        LibResponse ILibDataComponent<IList<ReglasDeContabilizacion>, IList<ReglasDeContabilizacion>>.Delete(IList<ReglasDeContabilizacion> refRecord) {
            throw new ProgrammerMissingCodeException("La acción de Eliminar no aplica para este objeto");
        }

        IList<ReglasDeContabilizacion> ILibDataComponent<IList<ReglasDeContabilizacion>, IList<ReglasDeContabilizacion>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<ReglasDeContabilizacion> vResult = new List<ReglasDeContabilizacion>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<ReglasDeContabilizacion>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<ReglasDeContabilizacion>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Reglas de Contabilización.Modificar")]
        LibResponse ILibDataComponent<IList<ReglasDeContabilizacion>, IList<ReglasDeContabilizacion>>.Insert(IList<ReglasDeContabilizacion> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ReglasDeContabilizacionINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<ReglasDeContabilizacion>, IList<ReglasDeContabilizacion>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoNumero")) {
                        vResult = LibXml.ValueToXElement(insDb.NextStrConsecutive(DbSchema + ".ReglasDeContabilizacion", "Numero", valParameters, true, 11), "Numero");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Reglas de Contabilización.Modificar")]
        LibResponse ILibDataComponent<IList<ReglasDeContabilizacion>, IList<ReglasDeContabilizacion>>.Update(IList<ReglasDeContabilizacion> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ReglasDeContabilizacionUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<ReglasDeContabilizacion>, IList<ReglasDeContabilizacion>>.ValidateAll(IList<ReglasDeContabilizacion> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (ReglasDeContabilizacion vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<ReglasDeContabilizacion>, IList<ReglasDeContabilizacion>>.SpecializedUpdate(IList<ReglasDeContabilizacion> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<ReglasDeContabilizacion>, IList<ReglasDeContabilizacion>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Numero);
            vResult = IsValidNumero(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Numero) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, string valNumero) {
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
            valNumero = LibString.Trim(valNumero);
            if (LibString.IsNullOrEmpty(valNumero, true)) {
                BuildValidationInfo(MsgRequiredField("Número"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                ReglasDeContabilizacion vRecBusqueda = new ReglasDeContabilizacion();
                vRecBusqueda.Numero = valNumero;
                if (KeyExists(valConsecutivoCompania, valNumero)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Número", valNumero));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaIva1Credito(eAccionSR valAction, string valCuentaIva1Credito) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaIva1Credito = LibString.Trim(valCuentaIva1Credito);
            if (LibString.IsNullOrEmpty(valCuentaIva1Credito, true)) {
                BuildValidationInfo(MsgRequiredField("IVA Crédito Fiscal"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaIva1Credito), true)) {
                    BuildValidationInfo("El valor asignado al campo IVA Crédito Fiscal no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }
        private bool IsValidCuentaIva1Debito(eAccionSR valAction, string valCuentaIva1Debito) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaIva1Debito = LibString.Trim(valCuentaIva1Debito);
            if (LibString.IsNullOrEmpty(valCuentaIva1Debito, true)) {
                BuildValidationInfo(MsgRequiredField("IVA Débito Fiscal"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaIva1Debito), true)) {
                    BuildValidationInfo("El valor asignado al campo IVA Débito Fiscal no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }
        private bool IsValidCuentaRetencionIva(eAccionSR valAction, string valCuentaRetencionIva) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaRetencionIva = LibString.Trim(valCuentaRetencionIva);
            if (LibString.IsNullOrEmpty(valCuentaRetencionIva, true)) {
                BuildValidationInfo(MsgRequiredField("Retención IVA"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaRetencionIva), true)) {
                    BuildValidationInfo("El valor asignado al campo Retención IVA no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }
        private bool IsValidDiferenciaEnCambioyCalculo(eAccionSR valAction, string valDiferenciaEnCambioyCalculo) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valDiferenciaEnCambioyCalculo = LibString.Trim(valDiferenciaEnCambioyCalculo);
            if (LibString.IsNullOrEmpty(valDiferenciaEnCambioyCalculo, true)) {
                BuildValidationInfo(MsgRequiredField("Diferencia En Cambio y Cálculo"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valDiferenciaEnCambioyCalculo), true)) {
                    BuildValidationInfo("El valor asignado al campo Diferencia En Cambio y Cálculo no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaDiferenciaCambiaria(eAccionSR valAction, string valCuentaDiferenciaCambiaria) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaDiferenciaCambiaria = LibString.Trim(valCuentaDiferenciaCambiaria);
            if (LibString.IsNullOrEmpty(valCuentaDiferenciaCambiaria, true)) {
                BuildValidationInfo(MsgRequiredField("Diferencia Cambiaria"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaDiferenciaCambiaria), true)) {
                    BuildValidationInfo("El valor asignado al campo Diferencia Cambiaria no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaCxCClientes(eAccionSR valAction, string valCuentaCxCClientes) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCxCClientes = LibString.Trim(valCuentaCxCClientes);
            if (LibString.IsNullOrEmpty(valCuentaCxCClientes, true)) {
                BuildValidationInfo(MsgRequiredField("CxC Clientes"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCxCClientes), true)) {
                    BuildValidationInfo("El valor asignado al campo CxC Clientes no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }
        private bool IsValidCuentaCxCIngresos(eAccionSR valAction, string valCuentaCxCIngresos) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCxCIngresos = LibString.Trim(valCuentaCxCIngresos);
            if (LibString.IsNullOrEmpty(valCuentaCxCIngresos, true)) {
                BuildValidationInfo(MsgRequiredField("Ingresos"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCxCIngresos), true)) {
                    BuildValidationInfo("El valor asignado al campo Ingresos no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }
        private bool IsValidCxCTipoComprobante(eAccionSR valAction, string valCxCTipoComprobante) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCxCTipoComprobante = LibString.Trim(valCxCTipoComprobante);
            if (LibString.IsNullOrEmpty(valCxCTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Siglas del Tipo de Comprobante"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Contab.TipoDeComprobante", "Codigo", insDb.InsSql.ToSqlValue(valCxCTipoComprobante), true)) {
                    BuildValidationInfo("El valor asignado al campo Siglas del Tipo de Comprobante no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }
        private bool IsValidCuentaCxPGasto(eAccionSR valAction, string valCuentaCxPGasto) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCxPGasto = LibString.Trim(valCuentaCxPGasto);
            if (LibString.IsNullOrEmpty(valCuentaCxPGasto, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta CxP Gasto"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCxPGasto), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta CxP Gasto no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }
        private bool IsValidCuentaCxPProveedores(eAccionSR valAction, string valCuentaCxPProveedores) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCxPProveedores = LibString.Trim(valCuentaCxPProveedores);
            if (LibString.IsNullOrEmpty(valCuentaCxPProveedores, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta CxP Proveedores"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCxPProveedores), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta CxP Proveedores no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }
        private bool IsValidCuentaRetencionImpuestoMunicipal(eAccionSR valAction, string valCuentaRetencionImpuestoMunicipal) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaRetencionImpuestoMunicipal = LibString.Trim(valCuentaRetencionImpuestoMunicipal);
            if (LibString.IsNullOrEmpty(valCuentaRetencionImpuestoMunicipal, true)) {
                BuildValidationInfo(MsgRequiredField("Retención Impuesto Municipal"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaRetencionImpuestoMunicipal), true)) {
                    BuildValidationInfo("El valor asignado al campo Retención Impuesto Municipal no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCxPTipoComprobante(eAccionSR valAction, string valCxPTipoComprobante) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCxPTipoComprobante = LibString.Trim(valCxPTipoComprobante);
            if (LibString.IsNullOrEmpty(valCxPTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Siglas del Tipo de Comprobante"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Contab.TipoDeComprobante", "Codigo", insDb.InsSql.ToSqlValue(valCxPTipoComprobante), true)) {
                    BuildValidationInfo("El valor asignado al campo Siglas del Tipo de Comprobante no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaCobranzaCobradoEnEfectivo(eAccionSR valAction, string valCuentaCobranzaCobradoEnEfectivo) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCobranzaCobradoEnEfectivo = LibString.Trim(valCuentaCobranzaCobradoEnEfectivo);
            if (LibString.IsNullOrEmpty(valCuentaCobranzaCobradoEnEfectivo, true)) {
                BuildValidationInfo(MsgRequiredField("Cobrado En Efectivo"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCobranzaCobradoEnEfectivo), true)) {
                    BuildValidationInfo("El valor asignado al campo Cobrado En Efectivo no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaCobranzaCobradoEnCheque(eAccionSR valAction, string valCuentaCobranzaCobradoEnCheque) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCobranzaCobradoEnCheque = LibString.Trim(valCuentaCobranzaCobradoEnCheque);
            if (LibString.IsNullOrEmpty(valCuentaCobranzaCobradoEnCheque, true)) {
                BuildValidationInfo(MsgRequiredField("Cobrado En Cheque"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCobranzaCobradoEnCheque), true)) {
                    BuildValidationInfo("El valor asignado al campo Cobrado En Cheque no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaCobranzaCobradoEnTarjeta(eAccionSR valAction, string valCuentaCobranzaCobradoEnTarjeta) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCobranzaCobradoEnTarjeta = LibString.Trim(valCuentaCobranzaCobradoEnTarjeta);
            if (LibString.IsNullOrEmpty(valCuentaCobranzaCobradoEnTarjeta, true)) {
                BuildValidationInfo(MsgRequiredField("Cobrado En Tarjeta"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCobranzaCobradoEnTarjeta), true)) {
                    BuildValidationInfo("El valor asignado al campo Cobrado En Tarjeta no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaCobranzaRetencionISLR(eAccionSR valAction, string valCuentaCobranzaRetencionISLR) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCobranzaRetencionISLR = LibString.Trim(valCuentaCobranzaRetencionISLR);
            if (LibString.IsNullOrEmpty(valCuentaCobranzaRetencionISLR, true)) {
                BuildValidationInfo(MsgRequiredField("Retención ISLR"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCobranzaRetencionISLR), true)) {
                    BuildValidationInfo("El valor asignado al campo Retención ISLR no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaCobranzaRetencionIVA(eAccionSR valAction, string valCuentaCobranzaRetencionIVA) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCobranzaRetencionIVA = LibString.Trim(valCuentaCobranzaRetencionIVA);
            if (LibString.IsNullOrEmpty(valCuentaCobranzaRetencionIVA, true)) {
                BuildValidationInfo(MsgRequiredField("Retención IVA"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCobranzaRetencionIVA), true)) {
                    BuildValidationInfo("El valor asignado al campo Retención IVA no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaCobranzaOtros(eAccionSR valAction, string valCuentaCobranzaOtros) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCobranzaOtros = LibString.Trim(valCuentaCobranzaOtros);
            if (LibString.IsNullOrEmpty(valCuentaCobranzaOtros, true)) {
                BuildValidationInfo(MsgRequiredField("Otros"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCobranzaOtros), true)) {
                    BuildValidationInfo("El valor asignado al campo Otros no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaCobranzaCxCClientes(eAccionSR valAction, string valCuentaCobranzaCxCClientes) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCobranzaCxCClientes = LibString.Trim(valCuentaCobranzaCxCClientes);
            if (LibString.IsNullOrEmpty(valCuentaCobranzaCxCClientes, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Cobranza CxC Clientes"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCobranzaCxCClientes), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta Cobranza CxC Clientes no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaCobranzaCobradoAnticipo(eAccionSR valAction, string valCuentaCobranzaCobradoAnticipo) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCobranzaCobradoAnticipo = LibString.Trim(valCuentaCobranzaCobradoAnticipo);
            if (LibString.IsNullOrEmpty(valCuentaCobranzaCobradoAnticipo, true)) {
                BuildValidationInfo(MsgRequiredField("Cobrado Anticipo"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCobranzaCobradoAnticipo), true)) {
                    BuildValidationInfo("El valor asignado al campo Cobrado Anticipo no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaCobranzaIvaDiferido(eAccionSR valAction, string valCuentaCobranzaIvaDiferido) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCobranzaIvaDiferido = LibString.Trim(valCuentaCobranzaIvaDiferido);
            if (LibString.IsNullOrEmpty(valCuentaCobranzaIvaDiferido, true)) {
                BuildValidationInfo(MsgRequiredField("IVA Débito Fiscal Diferido"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCobranzaIvaDiferido), true)) {
                    BuildValidationInfo("El valor asignado al campo IVA Débito Fiscal Diferido no existe, escoja nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCobranzaTipoComprobante(eAccionSR valAction, string valCobranzaTipoComprobante) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCobranzaTipoComprobante = LibString.Trim(valCobranzaTipoComprobante);
            if (LibString.IsNullOrEmpty(valCobranzaTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Siglas del Tipo de Comprobante"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Contab.TipoDeComprobante", "Codigo", insDb.InsSql.ToSqlValue(valCobranzaTipoComprobante), true)) {
                    BuildValidationInfo("El valor asignado al campo Siglas del Tipo de Comprobante no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaPagosCxPProveedores(eAccionSR valAction, string valCuentaPagosCxPProveedores) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaPagosCxPProveedores = LibString.Trim(valCuentaPagosCxPProveedores);
            if (LibString.IsNullOrEmpty(valCuentaPagosCxPProveedores, true)) {
                BuildValidationInfo(MsgRequiredField("CxP Proveedores"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaPagosCxPProveedores), true)) {
                    BuildValidationInfo("El valor asignado al campo CxP Proveedores no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaPagosRetencionISLR(eAccionSR valAction, string valCuentaPagosRetencionISLR) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaPagosRetencionISLR = LibString.Trim(valCuentaPagosRetencionISLR);
            if (LibString.IsNullOrEmpty(valCuentaPagosRetencionISLR, true)) {
                BuildValidationInfo(MsgRequiredField("Retención ISLR"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaPagosRetencionISLR), true)) {
                    BuildValidationInfo("El valor asignado al campo Retención ISLR no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaPagosOtros(eAccionSR valAction, string valCuentaPagosOtros) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaPagosOtros = LibString.Trim(valCuentaPagosOtros);
            if (LibString.IsNullOrEmpty(valCuentaPagosOtros, true)) {
                BuildValidationInfo(MsgRequiredField("Otros"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaPagosOtros), true)) {
                    BuildValidationInfo("El valor asignado al campo Otros no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaPagosBanco(eAccionSR valAction, string valCuentaPagosBanco) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaPagosBanco = LibString.Trim(valCuentaPagosBanco);
            if (LibString.IsNullOrEmpty(valCuentaPagosBanco, true)) {
                BuildValidationInfo(MsgRequiredField("Banco"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaPagosBanco), true)) {
                    BuildValidationInfo("El valor asignado al campo Banco no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaPagosPagadoAnticipo(eAccionSR valAction, string valCuentaPagosPagadoAnticipo) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaPagosPagadoAnticipo = LibString.Trim(valCuentaPagosPagadoAnticipo);
            if (LibString.IsNullOrEmpty(valCuentaPagosPagadoAnticipo, true)) {
                BuildValidationInfo(MsgRequiredField("Pagado Anticipo"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaPagosPagadoAnticipo), true)) {
                    BuildValidationInfo("El valor asignado al campo Pagado Anticipo no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidPagoTipoComprobante(eAccionSR valAction, string valPagoTipoComprobante) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valPagoTipoComprobante = LibString.Trim(valPagoTipoComprobante);
            if (LibString.IsNullOrEmpty(valPagoTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Siglas del Tipo de Comprobante"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Contab.TipoDeComprobante", "Codigo", insDb.InsSql.ToSqlValue(valPagoTipoComprobante), true)) {
                    BuildValidationInfo("El valor asignado al campo Siglas del Tipo de Comprobante no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaFacturacionCxCClientes(eAccionSR valAction, string valCuentaFacturacionCxCClientes) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaFacturacionCxCClientes = LibString.Trim(valCuentaFacturacionCxCClientes);
            if (LibString.IsNullOrEmpty(valCuentaFacturacionCxCClientes, true)) {
                BuildValidationInfo(MsgRequiredField("CxC Clientes"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaFacturacionCxCClientes), true)) {
                    BuildValidationInfo("El valor asignado al campo CxC Clientes no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaFacturacionMontoTotalFactura(eAccionSR valAction, string valCuentaFacturacionMontoTotalFactura) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaFacturacionMontoTotalFactura = LibString.Trim(valCuentaFacturacionMontoTotalFactura);
            if (LibString.IsNullOrEmpty(valCuentaFacturacionMontoTotalFactura, true)) {
                BuildValidationInfo(MsgRequiredField("Total Factura"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaFacturacionMontoTotalFactura), true)) {
                    BuildValidationInfo("El valor asignado al campo Total Factura no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaFacturacionCargos(eAccionSR valAction, string valCuentaFacturacionCargos) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaFacturacionCargos = LibString.Trim(valCuentaFacturacionCargos);
            if (LibString.IsNullOrEmpty(valCuentaFacturacionCargos, true)) {
                BuildValidationInfo(MsgRequiredField("Cargos"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaFacturacionCargos), true)) {
                    BuildValidationInfo("El valor asignado al campo Cargos no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaFacturacionDescuentos(eAccionSR valAction, string valCuentaFacturacionDescuentos) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaFacturacionDescuentos = LibString.Trim(valCuentaFacturacionDescuentos);
            if (LibString.IsNullOrEmpty(valCuentaFacturacionDescuentos, true)) {
                BuildValidationInfo(MsgRequiredField("Descuentos"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaFacturacionDescuentos), true)) {
                    BuildValidationInfo("El valor asignado al campo Descuentos no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaFacturacionIvaDiferido(eAccionSR valAction, string valCuentaFacturacionIvaDiferido) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaFacturacionIvaDiferido = LibString.Trim(valCuentaFacturacionIvaDiferido);
            if (LibString.IsNullOrEmpty(valCuentaFacturacionIvaDiferido, true)) {
                BuildValidationInfo(MsgRequiredField("IVA Débito Fiscal Diferido"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaFacturacionIvaDiferido), true)) {
                    BuildValidationInfo("El valor asignado al campo IVA Débito Fiscal Diferido no existe, escoja nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidFacturaTipoComprobante(eAccionSR valAction, string valFacturaTipoComprobante) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valFacturaTipoComprobante = LibString.Trim(valFacturaTipoComprobante);
            if (LibString.IsNullOrEmpty(valFacturaTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Siglas del Tipo de Comprobante"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Contab.TipoDeComprobante", "Codigo", insDb.InsSql.ToSqlValue(valFacturaTipoComprobante), true)) {
                    BuildValidationInfo("El valor asignado al campo Siglas del Tipo de Comprobante no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaRDVtasCaja(eAccionSR valAction, string valCuentaRDVtasCaja) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaRDVtasCaja = LibString.Trim(valCuentaRDVtasCaja);
            if (LibString.IsNullOrEmpty(valCuentaRDVtasCaja, true)) {
                BuildValidationInfo(MsgRequiredField("Caja"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaRDVtasCaja), true)) {
                    BuildValidationInfo("El valor asignado al campo Caja no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaRDVtasMontoTotal(eAccionSR valAction, string valCuentaRDVtasMontoTotal) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaRDVtasMontoTotal = LibString.Trim(valCuentaRDVtasMontoTotal);
            if (LibString.IsNullOrEmpty(valCuentaRDVtasMontoTotal, true)) {
                BuildValidationInfo(MsgRequiredField("Total Resumen"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaRDVtasMontoTotal), true)) {
                    BuildValidationInfo("El valor asignado al campo Total Resumen no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaMovBancarioGasto(eAccionSR valAction, string valCuentaMovBancarioGasto) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaMovBancarioGasto = LibString.Trim(valCuentaMovBancarioGasto);
            if (LibString.IsNullOrEmpty(valCuentaMovBancarioGasto, true)) {
                BuildValidationInfo(MsgRequiredField("Gasto"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaMovBancarioGasto), true)) {
                    BuildValidationInfo("El valor asignado al campo Gasto no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaMovBancarioBancosHaber(eAccionSR valAction, string valCuentaMovBancarioBancosHaber) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaMovBancarioBancosHaber = LibString.Trim(valCuentaMovBancarioBancosHaber);
            if (LibString.IsNullOrEmpty(valCuentaMovBancarioBancosHaber, true)) {
                BuildValidationInfo(MsgRequiredField("Bancos"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaMovBancarioBancosHaber), true)) {
                    BuildValidationInfo("El valor asignado al campo Bancos no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaMovBancarioBancosDebe(eAccionSR valAction, string valCuentaMovBancarioBancosDebe) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaMovBancarioBancosDebe = LibString.Trim(valCuentaMovBancarioBancosDebe);
            if (LibString.IsNullOrEmpty(valCuentaMovBancarioBancosDebe, true)) {
                BuildValidationInfo(MsgRequiredField("Bancos"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaMovBancarioBancosDebe), true)) {
                    BuildValidationInfo("El valor asignado al campo Bancos no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaMovBancarioIngresos(eAccionSR valAction, string valCuentaMovBancarioIngresos) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaMovBancarioIngresos = LibString.Trim(valCuentaMovBancarioIngresos);
            if (LibString.IsNullOrEmpty(valCuentaMovBancarioIngresos, true)) {
                BuildValidationInfo(MsgRequiredField("Ingresos"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaMovBancarioIngresos), true)) {
                    BuildValidationInfo("El valor asignado al campo Ingresos no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidMovimientoBancarioTipoComprobante(eAccionSR valAction, string valMovimientoBancarioTipoComprobante) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valMovimientoBancarioTipoComprobante = LibString.Trim(valMovimientoBancarioTipoComprobante);
            if (LibString.IsNullOrEmpty(valMovimientoBancarioTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Siglas del Tipo de Comprobante"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Contab.TipoDeComprobante", "Codigo", insDb.InsSql.ToSqlValue(valMovimientoBancarioTipoComprobante), true)) {
                    BuildValidationInfo("El valor asignado al campo Siglas del Tipo de Comprobante no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaDebitoBancarioGasto(eAccionSR valAction, string valCuentaDebitoBancarioGasto) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaDebitoBancarioGasto = LibString.Trim(valCuentaDebitoBancarioGasto);
            if (LibString.IsNullOrEmpty(valCuentaDebitoBancarioGasto, true)) {
                BuildValidationInfo(MsgRequiredField("Gastos"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaDebitoBancarioGasto), true)) {
                    BuildValidationInfo("El valor asignado al campo Gastos no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaDebitoBancarioBancos(eAccionSR valAction, string valCuentaDebitoBancarioBancos) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaDebitoBancarioBancos = LibString.Trim(valCuentaDebitoBancarioBancos);
            if (LibString.IsNullOrEmpty(valCuentaDebitoBancarioBancos, true)) {
                BuildValidationInfo(MsgRequiredField("Bancos"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaDebitoBancarioBancos), true)) {
                    BuildValidationInfo("El valor asignado al campo Bancos no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaCreditoBancarioGasto(eAccionSR valAction, string valCuentaCreditoBancarioGasto) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCreditoBancarioGasto = LibString.Trim(valCuentaCreditoBancarioGasto);
            if (LibString.IsNullOrEmpty(valCuentaCreditoBancarioGasto, true)) {
                BuildValidationInfo(MsgRequiredField("Gastos"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCreditoBancarioGasto), true)) {
                    BuildValidationInfo("El valor asignado al campo Gastos no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaCreditoBancarioBancos(eAccionSR valAction, string valCuentaCreditoBancarioBancos) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCreditoBancarioBancos = LibString.Trim(valCuentaCreditoBancarioBancos);
            if (LibString.IsNullOrEmpty(valCuentaCreditoBancarioBancos, true)) {
                BuildValidationInfo(MsgRequiredField("Bancos"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCreditoBancarioBancos), true)) {
                    BuildValidationInfo("El valor asignado al campo Bancos no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaAnticipoCaja(eAccionSR valAction, string valCuentaAnticipoCaja) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaAnticipoCaja = LibString.Trim(valCuentaAnticipoCaja);
            if (LibString.IsNullOrEmpty(valCuentaAnticipoCaja, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Anticipo Caja"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaAnticipoCaja), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta Anticipo Caja no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaAnticipoCobrado(eAccionSR valAction, string valCuentaAnticipoCobrado) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaAnticipoCobrado = LibString.Trim(valCuentaAnticipoCobrado);
            if (LibString.IsNullOrEmpty(valCuentaAnticipoCobrado, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Anticipo Cobrado"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaAnticipoCobrado), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta Anticipo Cobrado no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaAnticipoOtrosIngresos(eAccionSR valAction, string valCuentaAnticipoOtrosIngresos) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaAnticipoOtrosIngresos = LibString.Trim(valCuentaAnticipoOtrosIngresos);
            if (LibString.IsNullOrEmpty(valCuentaAnticipoOtrosIngresos, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Anticipo Otros Ingresos"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaAnticipoOtrosIngresos), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta Anticipo Otros Ingresos no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaAnticipoPagado(eAccionSR valAction, string valCuentaAnticipoPagado) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaAnticipoPagado = LibString.Trim(valCuentaAnticipoPagado);
            if (LibString.IsNullOrEmpty(valCuentaAnticipoPagado, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Anticipo Pagado"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaAnticipoPagado), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta Anticipo Pagado no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaAnticipoBanco(eAccionSR valAction, string valCuentaAnticipoBanco) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaAnticipoBanco = LibString.Trim(valCuentaAnticipoBanco);
            if (LibString.IsNullOrEmpty(valCuentaAnticipoBanco, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Anticipo Banco"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaAnticipoBanco), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta Anticipo Banco no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaAnticipoOtrosEgresos(eAccionSR valAction, string valCuentaAnticipoOtrosEgresos) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaAnticipoOtrosEgresos = LibString.Trim(valCuentaAnticipoOtrosEgresos);
            if (LibString.IsNullOrEmpty(valCuentaAnticipoOtrosEgresos, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Anticipo Otros Egresos"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaAnticipoOtrosEgresos), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta Anticipo Otros Egresos no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidAnticipoTipoComprobante(eAccionSR valAction, string valAnticipoTipoComprobante) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valAnticipoTipoComprobante = LibString.Trim(valAnticipoTipoComprobante);
            if (LibString.IsNullOrEmpty(valAnticipoTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Siglas del Tipo de Comprobante"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Contab.TipoDeComprobante", "Codigo", insDb.InsSql.ToSqlValue(valAnticipoTipoComprobante), true)) {
                    BuildValidationInfo("El valor asignado al campo Siglas del Tipo de Comprobante no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaCostoDeVenta(eAccionSR valAction, string valCuentaCostoDeVenta) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCostoDeVenta = LibString.Trim(valCuentaCostoDeVenta);
            if (LibString.IsNullOrEmpty(valCuentaCostoDeVenta, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Costo De Venta"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCostoDeVenta), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta Costo De Venta no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaInventario(eAccionSR valAction, string valCuentaInventario) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaInventario = LibString.Trim(valCuentaInventario);
            if (LibString.IsNullOrEmpty(valCuentaInventario, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Inventario"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaInventario), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta Inventario no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidInventarioTipoComprobante(eAccionSR valAction, string valInventarioTipoComprobante) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valInventarioTipoComprobante = LibString.Trim(valInventarioTipoComprobante);
            if (LibString.IsNullOrEmpty(valInventarioTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Siglas del Tipo de Comprobante"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Contab.TipoDeComprobante", "Codigo", insDb.InsSql.ToSqlValue(valInventarioTipoComprobante), true)) {
                    BuildValidationInfo("El valor asignado al campo Siglas del Tipo de Comprobante no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCtaDePagosSueldos(eAccionSR valAction, string valCtaDePagosSueldos) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCtaDePagosSueldos = LibString.Trim(valCtaDePagosSueldos);
            if (LibString.IsNullOrEmpty(valCtaDePagosSueldos, true)) {
                BuildValidationInfo(MsgRequiredField("Solicitud de Pago"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCtaDePagosSueldos), true)) {
                    BuildValidationInfo("El valor asignado al campo Solicitud de Pago no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCtaDePagosSueldosBanco(eAccionSR valAction, string valCtaDePagosSueldosBanco) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCtaDePagosSueldosBanco = LibString.Trim(valCtaDePagosSueldosBanco);
            if (LibString.IsNullOrEmpty(valCtaDePagosSueldosBanco, true)) {
                BuildValidationInfo(MsgRequiredField("Banco"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCtaDePagosSueldosBanco), true)) {
                    BuildValidationInfo("El valor asignado al campo Banco no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidPagosSueldosTipoComprobante(eAccionSR valAction, string valPagosSueldosTipoComprobante) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valPagosSueldosTipoComprobante = LibString.Trim(valPagosSueldosTipoComprobante);
            if (LibString.IsNullOrEmpty(valPagosSueldosTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Siglas del Tipo de Comprobante"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Contab.TipoDeComprobante", "Codigo", insDb.InsSql.ToSqlValue(valPagosSueldosTipoComprobante), true)) {
                    BuildValidationInfo("El valor asignado al campo Siglas del Tipo de Comprobante no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaCajaChicaGasto(eAccionSR valAction, string valCuentaCajaChicaGasto) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCajaChicaGasto = LibString.Trim(valCuentaCajaChicaGasto);
            if (LibString.IsNullOrEmpty(valCuentaCajaChicaGasto, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Gastos"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCajaChicaGasto), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta Gastos no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaCajaChicaBancoHaber(eAccionSR valAction, string valCuentaCajaChicaBancoHaber) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCajaChicaBancoHaber = LibString.Trim(valCuentaCajaChicaBancoHaber);
            if (LibString.IsNullOrEmpty(valCuentaCajaChicaBancoHaber, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta de Banco Caja Chica"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCajaChicaBancoHaber), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta de Banco Caja Chica no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaCajaChicaBancoDebe(eAccionSR valAction, string valCuentaCajaChicaBancoDebe) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCajaChicaBancoDebe = LibString.Trim(valCuentaCajaChicaBancoDebe);
            if (LibString.IsNullOrEmpty(valCuentaCajaChicaBancoDebe, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta de Banco Caja Chica"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCajaChicaBancoDebe), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta de Banco Caja Chica no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaCajaChicaBanco(eAccionSR valAction, string valCuentaCajaChicaBanco) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaCajaChicaBanco = LibString.Trim(valCuentaCajaChicaBanco);
            if (LibString.IsNullOrEmpty(valCuentaCajaChicaBanco, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta de Banco"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaCajaChicaBanco), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta de Banco no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidSiglasTipoComprobanteCajaChica(eAccionSR valAction, string valSiglasTipoComprobanteCajaChica) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valSiglasTipoComprobanteCajaChica = LibString.Trim(valSiglasTipoComprobanteCajaChica);
            if (LibString.IsNullOrEmpty(valSiglasTipoComprobanteCajaChica, true)) {
                BuildValidationInfo(MsgRequiredField("Siglas del Tipo de Comprobante"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Contab.TipoDeComprobante", "Codigo", insDb.InsSql.ToSqlValue(valSiglasTipoComprobanteCajaChica), true)) {
                    BuildValidationInfo("El valor asignado al campo Siglas del Tipo de Comprobante no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaRendicionesGasto(eAccionSR valAction, string valCuentaRendicionesGasto) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaRendicionesGasto = LibString.Trim(valCuentaRendicionesGasto);
            if (LibString.IsNullOrEmpty(valCuentaRendicionesGasto, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Gasto"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaRendicionesGasto), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta Gasto no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaRendicionesBanco(eAccionSR valAction, string valCuentaRendicionesBanco) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaRendicionesBanco = LibString.Trim(valCuentaRendicionesBanco);
            if (LibString.IsNullOrEmpty(valCuentaRendicionesBanco, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Banco"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaRendicionesBanco), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta Banco no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaRendicionesAnticipos(eAccionSR valAction, string valCuentaRendicionesAnticipos) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaRendicionesAnticipos = LibString.Trim(valCuentaRendicionesAnticipos);
            if (LibString.IsNullOrEmpty(valCuentaRendicionesAnticipos, true)) {
                BuildValidationInfo(MsgRequiredField("Anticipos"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaRendicionesAnticipos), true)) {
                    BuildValidationInfo("El valor asignado al campo Anticipos no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidSiglasTipoComprobanteRendiciones(eAccionSR valAction, string valSiglasTipoComprobanteRendiciones) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valSiglasTipoComprobanteRendiciones = LibString.Trim(valSiglasTipoComprobanteRendiciones);
            if (LibString.IsNullOrEmpty(valSiglasTipoComprobanteRendiciones, true)) {
                BuildValidationInfo(MsgRequiredField("Siglas del Tipo de Comprobante"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Contab.TipoDeComprobante", "Codigo", insDb.InsSql.ToSqlValue(valSiglasTipoComprobanteRendiciones), true)) {
                    BuildValidationInfo("El valor asignado al campo Siglas del Tipo de Comprobante no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaTransfCtasBancoDestino(eAccionSR valAction, string valCuentaTransfCtasBancoDestino) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaTransfCtasBancoDestino = LibString.Trim(valCuentaTransfCtasBancoDestino);
            if (LibString.IsNullOrEmpty(valCuentaTransfCtasBancoDestino, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta de Banco Destino"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaTransfCtasBancoDestino), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta de Banco Destino no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }
        private bool IsValidCuentaTransfCtasGastoComOrigen(eAccionSR valAction, string valCuentaTransfCtasGastoComOrigen) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaTransfCtasGastoComOrigen = LibString.Trim(valCuentaTransfCtasGastoComOrigen);
            if (LibString.IsNullOrEmpty(valCuentaTransfCtasGastoComOrigen, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta de Gasto Comision Origen"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaTransfCtasGastoComOrigen), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta de Gasto Comision Origen no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }
        private bool IsValidCuentaTransfCtasGastoComDestino(eAccionSR valAction, string valCuentaTransfCtasGastoComDestino) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaTransfCtasGastoComDestino = LibString.Trim(valCuentaTransfCtasGastoComDestino);
            if (LibString.IsNullOrEmpty(valCuentaTransfCtasGastoComDestino, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta de Gasto Comision Destino"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaTransfCtasGastoComDestino), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta de Gasto Comision Destino no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }
        private bool IsValidCuentaTransfCtasBancoOrigen(eAccionSR valAction, string valCuentaTransfCtasBancoOrigen) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaTransfCtasBancoOrigen = LibString.Trim(valCuentaTransfCtasBancoOrigen);
            if (LibString.IsNullOrEmpty(valCuentaTransfCtasBancoOrigen, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta de Banco Origen"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaTransfCtasBancoOrigen), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta de Banco Origen no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }
        private bool IsValidTransfCtasSigasTipoComprobante(eAccionSR valAction, string valTransfCtasSigasTipoComprobante) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valTransfCtasSigasTipoComprobante = LibString.Trim(valTransfCtasSigasTipoComprobante);
            if (LibString.IsNullOrEmpty(valTransfCtasSigasTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Siglas del Tipo de Comprobante"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Contab.TipoDeComprobante", "Codigo", insDb.InsSql.ToSqlValue(valTransfCtasSigasTipoComprobante), true)) {
                    BuildValidationInfo("El valor asignado al campo Siglas del Tipo de Comprobante no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaOrdenDeProduccionProductoTerminado(eAccionSR valAction, string valCuentaOrdenDeProduccionProductoTerminado) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaOrdenDeProduccionProductoTerminado = LibString.Trim(valCuentaOrdenDeProduccionProductoTerminado);
            if (LibString.IsNullOrEmpty(valCuentaOrdenDeProduccionProductoTerminado, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Producto Terminado"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaOrdenDeProduccionProductoTerminado), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta Producto Terminado no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaOrdenDeProduccionMateriaPrima(eAccionSR valAction, string valCuentaOrdenDeProduccionMateriaPrima) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaOrdenDeProduccionMateriaPrima = LibString.Trim(valCuentaOrdenDeProduccionMateriaPrima);
            if (LibString.IsNullOrEmpty(valCuentaOrdenDeProduccionMateriaPrima, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Materia Prima"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaOrdenDeProduccionMateriaPrima), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta Materia Prima no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaMermaAnormal(eAccionSR valAction, string valCuentaMermaAnormal) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaMermaAnormal = LibString.Trim(valCuentaMermaAnormal);
            if (LibString.IsNullOrEmpty(valCuentaMermaAnormal, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Merma Anormal"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "Codigo", insDb.InsSql.ToSqlValue(valCuentaMermaAnormal), true)) {
                    BuildValidationInfo("El valor asignado al campo Cuenta Merma Anormal no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidOrdenDeProduccionTipoComprobante(eAccionSR valAction, string valOrdenDeProduccionTipoComprobante) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valOrdenDeProduccionTipoComprobante = LibString.Trim(valOrdenDeProduccionTipoComprobante);
            if (LibString.IsNullOrEmpty(valOrdenDeProduccionTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Siglas del Tipo de Comprobante"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Contab.TipoDeComprobante", "Codigo", insDb.InsSql.ToSqlValue(valOrdenDeProduccionTipoComprobante), true)) {
                    BuildValidationInfo("El valor asignado al campo Siglas del Tipo de Comprobante no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valNumero) {
            bool vResult = false;
            ReglasDeContabilizacion vRecordBusqueda = new ReglasDeContabilizacion();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Numero = valNumero;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".ReglasDeContabilizacion", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, ReglasDeContabilizacion valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".ReglasDeContabilizacion", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones
        #endregion //Metodos Generados


    } //End of class clsReglasDeContabilizacionDat

} //End of namespace Galac.Saw.Dal.Contabilizacion

