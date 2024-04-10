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
using LibGalac.Aos.Cib;
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Dal.CajaChica {
    public class clsCxPDat : LibData, ILibDataMasterComponentWithSearch<IList<CxP>, IList<CxP>> {
        #region Variables
        LibDataScope insTrn;
        CxP _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private CxP CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCxPDat() {
            DbSchema = "dbo";
            insTrn = new LibDataScope();
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(CxP valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Numero", valRecord.Numero, 20);
            vParams.AddInInteger("ConsecutivoCxP", valRecord.ConsecutivoCxP);
            vParams.AddInEnum("TipoDeCxP", valRecord.TipoDeCxPAsDB);
            vParams.AddInEnum("Status", valRecord.StatusAsDB);
            vParams.AddInString("CodigoProveedor", valRecord.CodigoProveedor, 10);
            vParams.AddInDateTime("Fecha", valRecord.Fecha);
            vParams.AddInDateTime("FechaCancelacion", valRecord.FechaCancelacion);
            vParams.AddInDateTime("FechaVencimiento", valRecord.FechaVencimiento);
            vParams.AddInDateTime("FechaAnulacion", valRecord.FechaAnulacion);
            vParams.AddInString("Moneda", valRecord.Moneda, 10);
            vParams.AddInDecimal("CambioABolivares", valRecord.CambioABolivares, 2);
            vParams.AddInBoolean("AplicaParaLibrodeCompras", valRecord.AplicaParaLibrodeComprasAsBool);
            vParams.AddInDecimal("MontoExento", valRecord.MontoExento, 2);
            vParams.AddInDecimal("MontoGravado", valRecord.MontoGravado, 2);
            vParams.AddInDecimal("MontoIva", valRecord.MontoIva, 2);
            vParams.AddInDecimal("MontoAbonado", valRecord.MontoAbonado, 2);
            vParams.AddInInteger("DiaDeAplicacion", valRecord.DiaDeAplicacion);
            vParams.AddInInteger("MesDeAplicacion", valRecord.MesDeAplicacion);
            vParams.AddInInteger("AnoDeAplicacion", valRecord.AnoDeAplicacion);
            vParams.AddInString("Observaciones", valRecord.Observaciones, 255);
            vParams.AddInEnum("CreditoFiscal", valRecord.CreditoFiscalAsDB);
            vParams.AddInEnum("TipoDeCompra", valRecord.TipoDeCompraAsDB);
            vParams.AddInBoolean("SeHizoLaRetencion", valRecord.SeHizoLaRetencionAsBool);
            vParams.AddInDecimal("MontoGravableAlicuotaGeneral", valRecord.MontoGravableAlicuotaGeneral, 2);
            vParams.AddInDecimal("MontoGravableAlicuota2", valRecord.MontoGravableAlicuota2, 2);
            vParams.AddInDecimal("MontoGravableAlicuota3", valRecord.MontoGravableAlicuota3, 2);
            vParams.AddInDecimal("MontoIVAAlicuotaGeneral", valRecord.MontoIVAAlicuotaGeneral, 2);
            vParams.AddInDecimal("MontoIVAAlicuota2", valRecord.MontoIVAAlicuota2, 2);
            vParams.AddInDecimal("MontoIVAAlicuota3", valRecord.MontoIVAAlicuota3, 2);
            vParams.AddInString("NumeroPlanillaDeImportacion", valRecord.NumeroPlanillaDeImportacion, 20);
            vParams.AddInString("NumeroExpedienteDeImportacion", valRecord.NumeroExpedienteDeImportacion, 20);
            vParams.AddInEnum("TipoDeTransaccion", valRecord.TipoDeTransaccionAsDB);
            vParams.AddInString("NumeroDeFacturaAfectada", valRecord.NumeroDeFacturaAfectada, 11);
            vParams.AddInString("NumeroControl", valRecord.NumeroControl, 20);
            vParams.AddInBoolean("SeHizoLaRetencionIVA", valRecord.SeHizoLaRetencionIVAAsBool);
            vParams.AddInString("NumeroComprobanteRetencion", valRecord.NumeroComprobanteRetencion, 8);
            vParams.AddInDateTime("FechaAplicacionRetIVA", valRecord.FechaAplicacionRetIVA);
            vParams.AddInDecimal("PorcentajeRetencionAplicado", valRecord.PorcentajeRetencionAplicado, 2);
            vParams.AddInDecimal("MontoRetenido", valRecord.MontoRetenido, 2);
            vParams.AddInEnum("OrigenDeLaRetencion", valRecord.OrigenDeLaRetencionAsDB);
            vParams.AddInBoolean("RetencionAplicadaEnPago", valRecord.RetencionAplicadaEnPagoAsBool);
            vParams.AddInEnum("OrigenInformacionRetencion", valRecord.OrigenInformacionRetencionAsDB);
            vParams.AddInEnum("CxPgeneradaPor", valRecord.CxPgeneradaPorAsDB);
            vParams.AddInBoolean("EsCxPhistorica", valRecord.EsCxPhistoricaAsBool);
            vParams.AddInInteger("NumDiasDeVencimiento", valRecord.NumDiasDeVencimiento);
            vParams.AddInString("NumeroDocOrigen", valRecord.NumeroDocOrigen, 15);
            vParams.AddInString("CodigoLote", valRecord.CodigoLote, 10);
            vParams.AddInBoolean("GenerarAsientoDeRetiroEnCuenta", valRecord.GenerarAsientoDeRetiroEnCuentaAsBool);
            vParams.AddInDecimal("TotalOtrosImpuestos", valRecord.TotalOtrosImpuestos, 2);
            vParams.AddInBoolean("SeContabilRetIva", valRecord.SeContabilRetIvaAsBool);
            vParams.AddInString("DondeContabilRetIva", valRecord.DondeContabilRetIva, 1);
            vParams.AddInString("CodigoMoneda", valRecord.CodigoMoneda, 4);
            vParams.AddInBoolean("EstaAsociadoARendicion", valRecord.EstaAsociadoARendicionAsBool);
            vParams.AddInInteger("ConsecutivoRendicion", valRecord.ConsecutivoRendicion);
            vParams.AddInBoolean("OrigenDeLaRetencionISLR", valRecord.OrigenDeLaRetencionISLRAsBool);
            vParams.AddInBoolean("DondeContabilISLR", valRecord.DondeContabilISLRAsBool);
            vParams.AddInBoolean("ISLRAplicadaEnPago", valRecord.ISLRAplicadaEnPagoAsBool);
            vParams.AddInDecimal("MontoRetenidoISLR", valRecord.MontoRetenidoISLR, 2);
            vParams.AddInBoolean("SeContabilISLR", valRecord.SeContabilISLRAsBool);
            vParams.AddInDateTime("FechaAplicacionImpuestoMunicipal", valRecord.FechaAplicacionImpuestoMunicipal);
            vParams.AddInString("NumeroComprobanteImpuestoMunicipal", valRecord.NumeroComprobanteImpuestoMunicipal, 50);
            vParams.AddInDecimal("MontoRetenidoImpuestoMunicipal", valRecord.MontoRetenidoImpuestoMunicipal, 2);
            vParams.AddInBoolean("ImpuestoMunicipalRetenido", valRecord.ImpuestoMunicipalRetenidoAsBool);
            vParams.AddInString("NumeroControlDeFacturaAfectada", valRecord.NumeroControlDeFacturaAfectada, 11);
            vParams.AddInString("NumeroDeclaracionAduana", valRecord.NumeroDeclaracionAduana, 20);
            vParams.AddInDateTime("FechaDeclaracionAduana", valRecord.FechaDeclaracionAduana);
            vParams.AddInBoolean("UsaPrefijoSerie", valRecord.UsaPrefijoSerieAsBool);
            vParams.AddInString("CodigoProveedorOriginalServicio", valRecord.CodigoProveedorOriginalServicio, 10);
            vParams.AddInBoolean("EsUnaCuentaATerceros", valRecord.EsUnaCuentaATercerosAsBool);
            vParams.AddInBoolean("SeHizoLaDetraccion", valRecord.SeHizoLaDetraccionAsBool);
            vParams.AddInBoolean("AplicaIvaAlicuotaEspecial", valRecord.AplicaIvaAlicuotaEspecialAsBool);
            vParams.AddInDecimal("MontoGravableAlicuotaEspecial1", valRecord.MontoGravableAlicuotaEspecial1, 2);
            vParams.AddInDecimal("MontoIVAAlicuotaEspecial1", valRecord.MontoIVAAlicuotaEspecial1, 2);
            vParams.AddInDecimal("PorcentajeIvaAlicuotaEspecial1", valRecord.PorcentajeIvaAlicuotaEspecial1, 2);
            vParams.AddInDecimal("MontoGravableAlicuotaEspecial2", valRecord.MontoGravableAlicuotaEspecial2, 2);
            vParams.AddInDecimal("MontoIVAAlicuotaEspecial2", valRecord.MontoIVAAlicuotaEspecial2, 2);
            vParams.AddInDecimal("PorcentajeIvaAlicuotaEspecial2", valRecord.PorcentajeIvaAlicuotaEspecial2, 2);
            vParams.AddInDecimal("BaseImponibleIGTFML", valRecord.BaseImponibleIGTFML, 2);
            vParams.AddInDecimal("AlicuotaIGTFML", valRecord.AlicuotaIGTFML, 2);
            vParams.AddInDecimal("MontoIGTFML", valRecord.MontoIGTFML, 2);
            vParams.AddInString("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());

            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(CxP valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoCxP", valRecord.ConsecutivoCxP);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(CxP valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataMasterComponent<IList<CxP>, IList<CxP>>

        LibResponse ILibDataMasterComponent<IList<CxP>, IList<CxP>>.CanBeChoosen(IList<CxP> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            CxP vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDataScope insDB = new LibDataScope();
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValueOnMultifile("Saw.DocumentoPagado", "NumeroDelDocumentoPagado", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Numero), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Documento Pagado");
                }
                if (insDB.ExistsValueOnMultifile("Saw.DocumentoPagado", "CodigoMonedaDeCxP", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.CodigoMoneda), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Documento Pagado");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "CxP.Eliminar")]
        LibResponse ILibDataMasterComponent<IList<CxP>, IList<CxP>>.Delete(IList<CxP> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                if (ExecuteProcessBeforeDelete()) {
                    vResult.Success = insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "CxPDEL"), ParametrosClave(CurrentRecord, true, true));
                    if (vResult.Success) {
                        ExecuteProcessAfterDelete();
                    }
                }
            } else {
                throw new GalacException(vErrMsg, eExceptionManagementType.Validation);
            }
            return vResult;

        }

        IList<CxP> ILibDataMasterComponent<IList<CxP>, IList<CxP>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters, bool valUseDetail) {
            List<CxP> vResult = new List<CxP>();
            LibDataScope insDb = new LibDataScope();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<CxP>(valProcessMessage, valParameters, CmdTimeOut);
                    if (valUseDetail && vResult != null && vResult.Count > 0) {
                        new clsRenglonImpuestoMunicipalRetDat().GetDetailAndAppendToMaster(ref vResult);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "CxP.Insertar")]
        LibResponse ILibDataMasterComponent<IList<CxP>, IList<CxP>>.Insert(IList<CxP> refRecord, bool valUseDetail) {
            LibResponse vResult = new LibResponse();
            try {
                if (refRecord.Count > 0) {
                    CurrentRecord = refRecord[0];
                    int vConsecutivoCxP = insTrn.NextLngConsecutive(DbSchema + ".CXP", "ConsecutivoCXP", ParametrosProximoConsecutivo(CurrentRecord));
                    foreach (var item in refRecord) {
                        CurrentRecord = item;
                        if (ExecuteProcessBeforeInsert()) {
                            if (ValidateMasterDetail(eAccionSR.Insertar, CurrentRecord, valUseDetail)) {
                                CurrentRecord.ConsecutivoCxP = vConsecutivoCxP++;
                                if (insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "CxPINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
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
                    }
                }
            } catch (Exception vEx) {
                if (LibString.S1IsInS2("Ya existe la clave única.", vEx.Message)) {
                    throw new GalacException("Ya existe una CXP con el número (" + CurrentRecord.Numero + ") para el proveedor " + CurrentRecord.NombreProveedor + ", verifique e intente de nuevo", eExceptionManagementType.Alert);
                } else {
                    throw vEx;
                }
            }
            return vResult;
        }

        XElement ILibDataMasterComponent<IList<CxP>, IList<CxP>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDataScope insDb = new LibDataScope();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoConsecutivoCxP")) {
                        vResult = LibXml.ValueToXElement(insDb.NextLngConsecutive("dbo.CxP", "ConsecutivoCxP", valParameters), "ConsecutivoCxP");
                    }
                    break;
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                default:
                    throw new NotImplementedException();
            }
            insDb.Dispose();
            return vResult;
        }

        LibResponse ILibDataMasterComponent<IList<CxP>, IList<CxP>>.SpecializedUpdate(IList<CxP> refRecord, bool valUseDetail, string valSpecializedAction) {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "CxP.Modificar")]
        LibResponse ILibDataMasterComponent<IList<CxP>, IList<CxP>>.Update(IList<CxP> refRecord, bool valUseDetail, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
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
        }

        bool ILibDataMasterComponent<IList<CxP>, IList<CxP>>.ValidateAll(IList<CxP> refRecords, bool valUseDetail, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (CxP vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }
        #endregion //ILibDataMasterComponent<IList<CxP>, IList<CxP>>

        LibResponse UpdateMaster(CxP refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            vResult.Success = insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "CxPUPD"), ParametrosActualizacion(refRecord, valAction));
            return vResult;
        }

        LibResponse UpdateMasterAndDetail(CxP refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            string vErrorMessage = "";
            if (ValidateDetail(refRecord, eAccionSR.Modificar, out vErrorMessage)) {
                if (UpdateDetail(refRecord)) {
                    vResult = UpdateMaster(refRecord, valAction);
                }
            }
            return vResult;
        }

        private bool InsertDetail(CxP valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailRenglonImpuestoMunicipalRetAndUpdateDb(valRecord);
            return vResult;
        }


        private bool SetPkInDetailRenglonImpuestoMunicipalRetAndUpdateDb(CxP valRecord) {
            bool vResult = false;
            int vConsecutivo = 1;
            clsRenglonImpuestoMunicipalRetDat insRenglonImpuestoMunicipalRet = new clsRenglonImpuestoMunicipalRetDat();
            foreach (RenglonImpuestoMunicipalRet vDetail in valRecord.DetailRenglonImpuestoMunicipalRet) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.Consecutivo = valRecord.ConsecutivoCxP;
                vConsecutivo++;
            }
            vResult = insRenglonImpuestoMunicipalRet.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool UpdateDetail(CxP valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailRenglonImpuestoMunicipalRetAndUpdateDb(valRecord);
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.ConsecutivoCxP);
            vResult = IsValidNumero(valAction, CurrentRecord.Numero) && vResult;
            vResult = IsValidCodigoProveedor(valAction, CurrentRecord.CodigoProveedor) && vResult;
            vResult = IsValidFecha(valAction, CurrentRecord.Fecha) && vResult;
            vResult = IsValidFechaCancelacion(valAction, CurrentRecord.FechaCancelacion) && vResult;
            vResult = IsValidFechaVencimiento(valAction, CurrentRecord.FechaVencimiento) && vResult;
            vResult = IsValidFechaAnulacion(valAction, CurrentRecord.FechaAnulacion) && vResult;
            vResult = IsValidFechaAplicacionRetIVA(valAction, CurrentRecord.FechaAplicacionRetIVA) && vResult;
            //vResult = IsValidCodigoTipoDeDocumentoLey(valAction, CurrentRecord.CodigoTipoDeDocumentoLey) && vResult;
            vResult = IsValidFechaAplicacionImpuestoMunicipal(valAction, CurrentRecord.FechaAplicacionImpuestoMunicipal) && vResult;
            vResult = IsValidConsecutivoRendicion(valAction, CurrentRecord.ConsecutivoRendicion) && vResult;
            vResult = IsValidFechaDeclaracionAduana(valAction, CurrentRecord.FechaDeclaracionAduana) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivoCxP) {
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

        private bool IsValidNumero(eAccionSR valAction, string valNumero) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumero = LibString.Trim(valNumero);
            if (LibString.IsNullOrEmpty(valNumero, true)) {
                BuildValidationInfo(MsgRequiredField("Número"));
                vResult = false;
            }
            return vResult;
        }
        private bool IsValidConsecutivoCxP(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivoCxP) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoCxP == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo CxP"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valConsecutivoCxP)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Consecutivo CxP", valConsecutivoCxP));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCodigoProveedor(eAccionSR valAction, string valCodigoProveedor) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoProveedor = LibString.Trim(valCodigoProveedor);
            if (LibString.IsNullOrEmpty(valCodigoProveedor, true)) {
                BuildValidationInfo(MsgRequiredField("Código del Proveedor"));
                vResult = false;
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

        private bool IsValidFechaCancelacion(eAccionSR valAction, DateTime valFechaCancelacion) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaCancelacion, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaVencimiento(eAccionSR valAction, DateTime valFechaVencimiento) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaVencimiento, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaAnulacion(eAccionSR valAction, DateTime valFechaAnulacion) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaAnulacion, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
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

        private bool IsValidFechaAplicacionImpuestoMunicipal(eAccionSR valAction, DateTime valFechaAplicacionImpuestoMunicipal) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaAplicacionImpuestoMunicipal, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidConsecutivoRendicion(eAccionSR valAction, int valConsecutivoRendicion) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoRendicion == 0) {
                BuildValidationInfo(MsgRequiredField("Rendicion Asociada"));
                vResult = false;
            }
            return vResult;
        }
        private bool IsValidFechaDeclaracionAduana(eAccionSR valAction, DateTime valFechaDeclaracionAduana) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaDeclaracionAduana, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivoCxP) {
            bool vResult = false;
            CxP vRecordBusqueda = new CxP();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.ConsecutivoCxP = valConsecutivoCxP;
            LibDataScope insDb = new LibDataScope();
            vResult = insDb.ExistsRecord("dbo.CxP", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool ValidateMasterDetail(eAccionSR valAction, CxP valRecordMaster, bool valUseDetail) {
            bool vResult = false;
            string vErrMsg;
            if (Validate(valAction, out vErrMsg)) {
                if (valUseDetail) {
                    if (ValidateDetail(valRecordMaster, eAccionSR.Insertar, out vErrMsg)) {
                        vResult = true;
                    } else {
                        throw new GalacValidationException("CxP (detalle)\n" + vErrMsg);
                    }
                } else {
                    vResult = true;
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }

            return vResult;
        }

        private bool ValidateDetail(CxP valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            outErrorMessage = "";
            vResult = vResult && ValidateDetailRenglonImpuestoMunicipalRet(valRecord, valAction, out outErrorMessage);
            return vResult;
        }


        private bool ValidateDetailRenglonImpuestoMunicipalRet(CxP valRecord, eAccionSR eAccionSR, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            foreach (RenglonImpuestoMunicipalRet vDetail in valRecord.DetailRenglonImpuestoMunicipalRet) {
                bool vLineHasError = true;
                //agregar validaciones
                if (LibString.IsNullOrEmpty(vDetail.CodigoRetencion)) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Codigo Retencion.");
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
            LibDataScope insDb = new LibDataScope();
            refResulset = insDb.LoadForConnect(valProcessMessage, valXmlParamsExpression, CmdTimeOut);
            if (refResulset != null && refResulset.DocumentElement != null && refResulset.DocumentElement.HasChildNodes) {
                vResult = true;
            }
            return vResult;
        }
        #endregion //Miembros de ILibDataFKSearch


        #endregion //Metodos Generados


    } //End of class clsCxPDat

} //End of namespace Galac.Dbo.Dal.CajaChica
