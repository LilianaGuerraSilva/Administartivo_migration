using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Security.Permissions;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.Contabilizacion;

namespace Galac.Saw.Uil.Contabilizacion {
    public class clsReglasDeContabilizacionIpl : LibMROMF, ILibView {
        #region Variables
        ILibBusinessComponent<IList<ReglasDeContabilizacion>, IList<ReglasDeContabilizacion>> _Reglas;
        IList<ReglasDeContabilizacion> _ListReglasDeContabilizacion;
        #endregion //Variables
        #region Propiedades

        public IList<ReglasDeContabilizacion> ListReglasDeContabilizacion {
            get { return _ListReglasDeContabilizacion; }
            set { _ListReglasDeContabilizacion = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsReglasDeContabilizacionIpl(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
            ListReglasDeContabilizacion = new List<ReglasDeContabilizacion>();
            ListReglasDeContabilizacion.Add(new ReglasDeContabilizacion());
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(ReglasDeContabilizacion refRecord) {
            refRecord.Clear();
            refRecord.ConsecutivoCompania = Mfc.GetInt("Compania");
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponent<IList<ReglasDeContabilizacion>, IList<ReglasDeContabilizacion>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Contabilizacion.clsReglasDeContabilizacionNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((ReglasDeContabilizacion)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            throw new NotImplementedException();
        }

        string ILibView.MessageName {
            get { return "Reglas de Contabilización"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((ReglasDeContabilizacion)refRecord, valAction, out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            throw new NotImplementedException();
        }

        object ILibView.NextSequential(string valSequentialName) {
            return GenerarProximoNumero();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Reglas de Contabilización.Modificar")]
        internal bool UpdateRecord(ReglasDeContabilizacion refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<ReglasDeContabilizacion> vBusinessObject = new List<ReglasDeContabilizacion>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        public bool FindAndSetObject(int valConsecutivoCompania) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            //vParams.AddInString("Numero", valNumero, 11);
            ListReglasDeContabilizacion = _Reglas.GetData(eProcessMessageType.SpName, "ReglasDeContabilizacionGET", vParams.Get());
            return (ListReglasDeContabilizacion.Count > 0);
        }

        private string GenerarProximoNumero() {
            string vResult = "";
            RegistraCliente();
            XElement vResulset = _Reglas.QueryInfo(eProcessMessageType.Message, "ProximoNumero", Mfc.GetIntAsParam("Compania"));
            vResult = LibXml.GetPropertyString(vResulset, "Numero");
            return vResult;
        }

        public bool ValidateAll(ReglasDeContabilizacion refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            //vResult = IsValidConsecutivoCompania(valAction, refRecord.ConsecutivoCompania, false);
            vResult = IsValidNumero(valAction, refRecord.Numero, false) && vResult;
            //  vResult = IsValidDiferenciaEnCambioyCalculo(valAction, refRecord.DiferenciaEnCambioyCalculo, false) && vResult;
            //  vResult = IsValidCuentaIva1Credito(valAction, refRecord.CuentaIva1Credito, false) && vResult;
            //  vResult = IsValidCuentaIva1Debito(valAction, refRecord.CuentaIva1Debito, false) && vResult;
            //  vResult = IsValidCxCTipoComprobante(valAction, refRecord.CxCTipoComprobante, false) && vResult;
            //  vResult = IsValidCuentaCxCClientes(valAction, refRecord.CuentaCxCClientes, false) && vResult;
            //  vResult = IsValidCuentaCxCIngresos(valAction, refRecord.CuentaCxCIngresos, false) && vResult;
            //  vResult = IsValidCxPTipoComprobante(valAction, refRecord.CxPTipoComprobante, false) && vResult;
            //  vResult = IsValidCuentaCxPGasto(valAction, refRecord.CuentaCxPGasto, false) && vResult;
            //  vResult = IsValidCuentaCxPProveedores(valAction, refRecord.CuentaCxPProveedores, false) && vResult;
            //  vResult = IsValidCuentaRetencionImpuestoMunicipal(valAction, refRecord.CuentaRetencionImpuestoMunicipal, false) && vResult;
            //  vResult = IsValidCuentaCobranzaCobradoEnEfectivo(valAction, refRecord.CuentaCobranzaCobradoEnEfectivo, false) && vResult;
            //  vResult = IsValidCuentaCobranzaCobradoEnCheque(valAction, refRecord.CuentaCobranzaCobradoEnCheque, false) && vResult;
            //  vResult = IsValidCuentaCobranzaCobradoEnTarjeta(valAction, refRecord.CuentaCobranzaCobradoEnTarjeta, false) && vResult;
            //  vResult = IsValidcuentaCobranzaRetencionISLR(valAction, refRecord.cuentaCobranzaRetencionISLR, false) && vResult;
            //  vResult = IsValidcuentaCobranzaRetencionIVA(valAction, refRecord.cuentaCobranzaRetencionIVA, false) && vResult;
            //  vResult = IsValidCuentaCobranzaOtros(valAction, refRecord.CuentaCobranzaOtros, false) && vResult;
            //  vResult = IsValidCuentaCobranzaCxCClientes(valAction, refRecord.CuentaCobranzaCxCClientes, false) && vResult;
            //  vResult = IsValidCuentaCobranzaCobradoAnticipo(valAction, refRecord.CuentaCobranzaCobradoAnticipo, false) && vResult;
            //  vResult = IsValidCuentaPagosCxPProveedores(valAction, refRecord.CuentaPagosCxPProveedores, false) && vResult;
            //  vResult = IsValidCuentaPagosRetencionISLR(valAction, refRecord.CuentaPagosRetencionISLR, false) && vResult;
            //  vResult = IsValidCuentaPagosOtros(valAction, refRecord.CuentaPagosOtros, false) && vResult;
            //  vResult = IsValidCuentaPagosBanco(valAction, refRecord.CuentaPagosBanco, false) && vResult;
            //  vResult = IsValidCuentaPagosPagadoAnticipo(valAction, refRecord.CuentaPagosPagadoAnticipo, false) && vResult;
            //  vResult = IsValidCuentaFacturacionCxCClientes(valAction, refRecord.CuentaFacturacionCxCClientes, false) && vResult;
            //  vResult = IsValidCuentaFacturacionMontoTotalFactura(valAction, refRecord.CuentaFacturacionMontoTotalFactura, false) && vResult;
            //  vResult = IsValidCuentaFacturacionCargos(valAction, refRecord.CuentaFacturacionCargos, false) && vResult;
            //  vResult = IsValidCuentaFacturacionDescuentos(valAction, refRecord.CuentaFacturacionDescuentos, false) && vResult;
            //  vResult = IsValidCuentaRDVtasCaja(valAction, refRecord.CuentaRDVtasCaja, false) && vResult;
            //  vResult = IsValidCuentaRDVtasMontoTotal(valAction, refRecord.CuentaRDVtasMontoTotal, false) && vResult;
            //  vResult = IsValidCuentaMovBancarioGasto(valAction, refRecord.CuentaMovBancarioGasto, false) && vResult;
            //  vResult = IsValidCuentaMovBancarioBancosHaber(valAction, refRecord.CuentaMovBancarioBancosHaber, false) && vResult;
            //  vResult = IsValidCuentaMovBancarioBancosDebe(valAction, refRecord.CuentaMovBancarioBancosDebe, false) && vResult;
            //  vResult = IsValidCuentaMovBancarioIngresos(valAction, refRecord.CuentaMovBancarioIngresos, false) && vResult;
            //  vResult = IsValidCuentaDebitoBancarioGasto(valAction, refRecord.CuentaDebitoBancarioGasto, false) && vResult;
            //  vResult = IsValidCuentaDebitoBancarioBancos(valAction, refRecord.CuentaDebitoBancarioBancos, false) && vResult;
            //  vResult = IsValidCuentaCreditoBancarioGasto(valAction, refRecord.CuentaCreditoBancarioGasto, false) && vResult;
            //  vResult = IsValidCuentaCreditoBancarioBancos(valAction, refRecord.CuentaCreditoBancarioBancos, false) && vResult;
            //  vResult = IsValidCuentaAnticipoCaja(valAction, refRecord.CuentaAnticipoCaja, false) && vResult;
            //  vResult = IsValidCuentaAnticipoCobrado(valAction, refRecord.CuentaAnticipoCobrado, false) && vResult;
            //  vResult = IsValidCuentaAnticipoOtrosIngresos(valAction, refRecord.CuentaAnticipoOtrosIngresos, false) && vResult;
            //  vResult = IsValidCuentaAnticipoPagado(valAction, refRecord.CuentaAnticipoPagado, false) && vResult;
            //  vResult = IsValidCuentaAnticipoBanco(valAction, refRecord.CuentaAnticipoBanco, false) && vResult;
            //  vResult = IsValidCuentaAnticipoOtrosEgresos(valAction, refRecord.CuentaAnticipoOtrosEgresos, false) && vResult;
            //  vResult = IsValidFacturaTipoComprobante(valAction, refRecord.FacturaTipoComprobante, false) && vResult;
            //  vResult = IsValidCxCTipoComprobante(valAction, refRecord.CxCTipoComprobante, false) && vResult;
            //  vResult = IsValidCxPTipoComprobante(valAction, refRecord.CxPTipoComprobante, false) && vResult;
            //  vResult = IsValidCobranzaTipoComprobante(valAction, refRecord.CobranzaTipoComprobante, false) && vResult;
            //  vResult = IsValidPagoTipoComprobante(valAction, refRecord.PagoTipoComprobante, false) && vResult;
            //  vResult = IsValidMovimientoBancarioTipoComprobante(valAction, refRecord.MovimientoBancarioTipoComprobante, false) && vResult;
            //  vResult = IsValidAnticipoTipoComprobante(valAction, refRecord.AnticipoTipoComprobante, false) && vResult;
            //  vResult = IsValidCtaDePagosSueldos(valAction, refRecord.CtaDePagosSueldos, false) && vResult;
            //  vResult = IsValidCtaDePagosSueldosBanco(valAction, refRecord.CtaDePagosSueldosBanco, false) && vResult;
            //  vResult = IsValidContabIndividualPagosSueldos(valAction, refRecord.ContabIndividualPagosSueldos, false) && vResult;
            //  vResult = IsValidPagosSueldosTipoComprobante(valAction, refRecord.PagosSueldosTipoComprobante, false) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidNumero(eAccionSR valAction, string valNumero, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valNumero, true)) {
                BuildValidationInfo(MsgRequiredField("Número"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidDiferenciaEnCambioyCalculo(eAccionSR valAction, string valDiferenciaEnCambioyCalculo, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valDiferenciaEnCambioyCalculo, true)) {
                BuildValidationInfo(MsgRequiredField("Diferencia En Cambioy Calculo"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaIva1Credito(eAccionSR valAction, string valCuentaIva1Credito, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaIva1Credito, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Iva 1Credito"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaIva1Debito(eAccionSR valAction, string valCuentaIva1Debito, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaIva1Debito, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Iva 1Debito"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaCxCClientes(eAccionSR valAction, string valCuentaCxCClientes, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaCxCClientes, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Cx CClientes"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaCxCIngresos(eAccionSR valAction, string valCuentaCxCIngresos, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaCxCIngresos, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Cx CIngresos"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaCxPGasto(eAccionSR valAction, string valCuentaCxPGasto, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaCxPGasto, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Cx PGasto"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaCxPProveedores(eAccionSR valAction, string valCuentaCxPProveedores, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaCxPProveedores, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Cx PProveedores"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaRetencionImpuestoMunicipal(eAccionSR valAction, string valCuentaRetencionImpuestoMunicipal, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaRetencionImpuestoMunicipal, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Retencion Impuesto Municipal"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaCobranzaCobradoEnEfectivo(eAccionSR valAction, string valCuentaCobranzaCobradoEnEfectivo, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaCobranzaCobradoEnEfectivo, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Cobranza Cobrado En Efectivo"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaCobranzaCobradoEnCheque(eAccionSR valAction, string valCuentaCobranzaCobradoEnCheque, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaCobranzaCobradoEnCheque, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Cobranza Cobrado En Cheque"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaCobranzaCobradoEnTarjeta(eAccionSR valAction, string valCuentaCobranzaCobradoEnTarjeta, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaCobranzaCobradoEnTarjeta, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Cobranza Cobrado En Tarjeta"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidcuentaCobranzaRetencionISLR(eAccionSR valAction, string valcuentaCobranzaRetencionISLR, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valcuentaCobranzaRetencionISLR, true)) {
                BuildValidationInfo(MsgRequiredField("cuenta Cobranza Retencion ISLR"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidcuentaCobranzaRetencionIVA(eAccionSR valAction, string valcuentaCobranzaRetencionIVA, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valcuentaCobranzaRetencionIVA, true)) {
                BuildValidationInfo(MsgRequiredField("cuenta Cobranza Retencion IVA"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaCobranzaOtros(eAccionSR valAction, string valCuentaCobranzaOtros, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaCobranzaOtros, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Cobranza Otros"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaCobranzaCxCClientes(eAccionSR valAction, string valCuentaCobranzaCxCClientes, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaCobranzaCxCClientes, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Cobranza Cx CClientes"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaCobranzaCobradoAnticipo(eAccionSR valAction, string valCuentaCobranzaCobradoAnticipo, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaCobranzaCobradoAnticipo, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Cobranza Cobrado Anticipo"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaPagosCxPProveedores(eAccionSR valAction, string valCuentaPagosCxPProveedores, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaPagosCxPProveedores, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Pagos Cx PProveedores"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaPagosRetencionIVA(eAccionSR valAction, string valCuentaPagosRetencionIVA, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaPagosRetencionIVA, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Pagos Retencion IVA"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaPagosRetencionISLR(eAccionSR valAction, string valCuentaPagosRetencionISLR, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaPagosRetencionISLR, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Pagos Retencion ISLR"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaPagosOtros(eAccionSR valAction, string valCuentaPagosOtros, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaPagosOtros, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Pagos Otros"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaPagosBanco(eAccionSR valAction, string valCuentaPagosBanco, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaPagosBanco, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Pagos Banco"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaPagosPagadoAnticipo(eAccionSR valAction, string valCuentaPagosPagadoAnticipo, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaPagosPagadoAnticipo, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Pagos Pagado Anticipo"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaFacturacionCxCClientes(eAccionSR valAction, string valCuentaFacturacionCxCClientes, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaFacturacionCxCClientes, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Facturacion Cx CClientes"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaFacturacionMontoTotalFactura(eAccionSR valAction, string valCuentaFacturacionMontoTotalFactura, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaFacturacionMontoTotalFactura, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Facturacion Monto Total Factura"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaFacturacionCargos(eAccionSR valAction, string valCuentaFacturacionCargos, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaFacturacionCargos, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Facturacion Cargos"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaFacturacionDescuentos(eAccionSR valAction, string valCuentaFacturacionDescuentos, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaFacturacionDescuentos, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Facturacion Descuentos"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaRDVtasCaja(eAccionSR valAction, string valCuentaRDVtasCaja, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaRDVtasCaja, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta RDVtas Caja"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaRDVtasMontoTotal(eAccionSR valAction, string valCuentaRDVtasMontoTotal, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaRDVtasMontoTotal, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta RDVtas Monto Total"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaMovBancarioGasto(eAccionSR valAction, string valCuentaMovBancarioGasto, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaMovBancarioGasto, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Mov Bancario Gasto"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaMovBancarioBancosHaber(eAccionSR valAction, string valCuentaMovBancarioBancosHaber, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaMovBancarioBancosHaber, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Mov Bancario Bancos Haber"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaMovBancarioBancosDebe(eAccionSR valAction, string valCuentaMovBancarioBancosDebe, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaMovBancarioBancosDebe, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Mov Bancario Bancos Debe"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaMovBancarioIngresos(eAccionSR valAction, string valCuentaMovBancarioIngresos, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaMovBancarioIngresos, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Mov Bancario Ingresos"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaDebitoBancarioGasto(eAccionSR valAction, string valCuentaDebitoBancarioGasto, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaDebitoBancarioGasto, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Debito Bancario Gasto"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaDebitoBancarioBancos(eAccionSR valAction, string valCuentaDebitoBancarioBancos, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaDebitoBancarioBancos, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Debito Bancario Bancos"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaCreditoBancarioGasto(eAccionSR valAction, string valCuentaCreditoBancarioGasto, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaCreditoBancarioGasto, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Credito Bancario Gasto"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaCreditoBancarioBancos(eAccionSR valAction, string valCuentaCreditoBancarioBancos, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaCreditoBancarioBancos, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Credito Bancario Bancos"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaAnticipoCaja(eAccionSR valAction, string valCuentaAnticipoCaja, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaAnticipoCaja, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Anticipo Caja"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaAnticipoCobrado(eAccionSR valAction, string valCuentaAnticipoCobrado, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaAnticipoCobrado, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Anticipo Cobrado"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaAnticipoOtrosIngresos(eAccionSR valAction, string valCuentaAnticipoOtrosIngresos, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaAnticipoOtrosIngresos, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Anticipo Otros Ingresos"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaAnticipoPagado(eAccionSR valAction, string valCuentaAnticipoPagado, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaAnticipoPagado, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Anticipo Pagado"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaAnticipoBanco(eAccionSR valAction, string valCuentaAnticipoBanco, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaAnticipoBanco, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Anticipo Banco"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaAnticipoOtrosEgresos(eAccionSR valAction, string valCuentaAnticipoOtrosEgresos, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaAnticipoOtrosEgresos, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Anticipo Otros Egresos"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidFacturaTipoComprobante(eAccionSR valAction, string valFacturaTipoComprobante, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valFacturaTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Factura Tipo Comprobante"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCxCTipoComprobante(eAccionSR valAction, string valCxCTipoComprobante, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCxCTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Cx CTipo Comprobante"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCxPTipoComprobante(eAccionSR valAction, string valCxPTipoComprobante, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCxPTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Cx PTipo Comprobante"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCobranzaTipoComprobante(eAccionSR valAction, string valCobranzaTipoComprobante, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCobranzaTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Cobranza Tipo Comprobante"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidPagoTipoComprobante(eAccionSR valAction, string valPagoTipoComprobante, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valPagoTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Pago Tipo Comprobante"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidMovimientoBancarioTipoComprobante(eAccionSR valAction, string valMovimientoBancarioTipoComprobante, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valMovimientoBancarioTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Movimiento Bancario Tipo Comprobante"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidAnticipoTipoComprobante(eAccionSR valAction, string valAnticipoTipoComprobante, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valAnticipoTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Anticipo Tipo Comprobante"));
                vResult = false;
            }
            return vResult;
        }

       public bool EscogerReglasDeContabilizacionActual(int valConsecutivoCompania) {
           bool vResult = false;
           if (FindAndSetObject(valConsecutivoCompania)) {
               vResult = true;
           } else {
           /* IMPLEMENTE EL SIGUIENTE CODIGO
               if (valConsecutivoCompania != 0) {
                   if (((IReglasDeContabilizacionPdn)_Reglas).InsertarValoresPorDefecto(valConsecutivoCompania)) {
                       vResult = FindAndSetObject(valConsecutivoCompania);
                   }
               }
           */
           }
           if (vResult) {
               //FALTA IMPLEMENTAR Set Valores Globales (LibGlobalValues)
           }
           return vResult;
       }
        #endregion //Metodos Generados
		
        public bool IsValidCtaDePagosSueldos(eAccionSR valAction, string valCtaDePagosSueldos, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCtaDePagosSueldos, true)) {
                BuildValidationInfo(MsgRequiredField("Cta De Pagos Sueldos"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCtaDePagosSueldosBanco(eAccionSR valAction, string valCtaDePagosSueldosBanco, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCtaDePagosSueldosBanco, true)) {
                BuildValidationInfo(MsgRequiredField("Cta De Pagos Sueldos Banco"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidContabIndividualPagosSueldos(eAccionSR valAction, string valContabIndividualPagosSueldos, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valContabIndividualPagosSueldos, true)) {
                BuildValidationInfo(MsgRequiredField("Contab Individual Pagos Sueldos"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidPagosSueldosTipoComprobante(eAccionSR valAction, string valPagosSueldosTipoComprobante, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valPagosSueldosTipoComprobante, true)) {
                BuildValidationInfo(MsgRequiredField("Pagos Sueldos Tipo Comprobante"));
                vResult = false;
            }
            return vResult;
        }
    
        public XmlReader XmlListaGrupoDeActivos(int valConsecutivoPeriodo) {
            XmlReader vResult = null;
            XElement vXElement;
            RegistraCliente();
            vXElement = ((IReglasDeContabilizacionPdn)_Reglas).GetListaGrupoDeActivos("Grupo De Activos", valConsecutivoPeriodo);
            if ((vXElement != null) && (vXElement.HasElements)) {
                vResult = vXElement.CreateReader();
            }
            return vResult;
        }

        private IList<ReglasDeContabilizacion> BuscarReglasDeContabilizacionParaEscoger() {
            LibGpParams vParams = new LibGpParams();
            LibXmlDataParse insDataParse = new LibXmlDataParse((LibXmlMemInfo)AppMemoryInfo);
            RegistraCliente();
            vParams.AddInInteger("ConsecutivoCompania", Mfc.GetInt("Compania"));
            vParams.AddInString("Numero", insDataParse.GetString("RecordName", 0, "Numero", "*"), 11);
            IList<ReglasDeContabilizacion> vResulset = _Reglas.GetData(eProcessMessageType.SpName, "ReglasDeContabilizacionGET", vParams.Get());
            return vResulset;
        }

        public bool SePuedeUsarEstaCuenta(bool valUsaAuxiliares, bool valUsaModuloDeActivoFijo, bool valEscogerAuxiliares, string valGetCierreDelEjercicio, ref string valMensaje, XmlDocument valXmlDocument) {
            bool vResult = false;
            RegistraCliente();
            vResult = ((IReglasDeContabilizacionPdn)_Reglas).SePuedeUsarEstaCuenta(valUsaAuxiliares, valUsaModuloDeActivoFijo, valEscogerAuxiliares, valGetCierreDelEjercicio, ref valMensaje, valXmlDocument);
            return vResult;
        }

        public string AjustaLaCuenta(string valCode, int valMaxNumLevels, int valMaxNumLevelsAtMatrix, int valMinNumLevels, int valMaxLength, bool valUseZeroAtRigth, string valNiveles  ) {
            string  vResult ="";
            RegistraCliente();
            vResult = ((IReglasDeContabilizacionPdn)_Reglas).CorrigeYAjustaLaCuenta(valCode, valMaxNumLevels, valMaxNumLevelsAtMatrix, valMinNumLevels, valMaxLength, valUseZeroAtRigth, LibString.Split(valNiveles, ','));
            return vResult;
        }
        public bool IsValidCuentaCajaChicaGasto(eAccionSR valAction, string valCuentaCajaChicaGasto, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaCajaChicaGasto, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Caja Chica Gasto"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaCajaChicaBancoHaber(eAccionSR valAction, string valCuentaCajaChicaBancoHaber, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaCajaChicaBancoHaber, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Caja Chica Banco Haber"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaCajaChicaBancoDebe(eAccionSR valAction, string valCuentaCajaChicaBancoDebe, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaCajaChicaBancoDebe, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Caja Chica Banco Debe"));
                vResult = false;
            }
            return vResult;
        }
        public bool IsValidCuentaCajaChicaBanco(eAccionSR valAction, string valCuentaCajaChicaBanco, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaCajaChicaBanco, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Caja Chica Banco"));
                vResult = false;
            }
            return vResult;
        }
        public bool IsValidCuentaRendicionesGasto(eAccionSR valAction, string valCuentaRendicionesGasto, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaRendicionesGasto, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Rendiciones Gasto"));
                vResult = false;
            }
            return vResult;
        }
        public bool IsValidCuentaRendicionesBanco(eAccionSR valAction, string valCuentaRendicionesBanco, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaRendicionesBanco, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Rendiciones Banco"));
                vResult = false;
            }
            return vResult;
        }
        public bool IsValidCuentaRendicionesAnticipos(eAccionSR valAction, string valCuentaRendicionesAnticipos, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaRendicionesAnticipos, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Rendiciones Anticipos"));
                vResult = false;
            }
            return vResult;
        }
    } //End of class clsReglasDeContabilizacionIpl

} //End of namespace Galac.Saw.Uil.Contabilizacion

