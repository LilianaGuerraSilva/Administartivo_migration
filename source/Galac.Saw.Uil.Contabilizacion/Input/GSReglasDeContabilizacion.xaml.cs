using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Contabilizacion;

namespace Galac.Saw.Uil.Contabilizacion.Input {
    /// <summary>
    /// Lógica de interacción para GSReglasDeContabilizacion.xaml
    /// </summary>
    internal partial class GSReglasDeContabilizacion : UserControl {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        ReglasDeContabilizacion _CurrentInstance;
        ILibView _CurrentModel;
        bool _EscogerAuxiliar;
        bool _UsaAuxiliares;
        bool _UsaModuloDeActivoFijo;
        bool _UsarResumenDiarioDeVentas;
        bool _UsarRenglonesEnResumenVtas;
        bool _PuedoUsarOpcionesDeContribuyenteEspecial;
        bool _MuestraTipoComprobante;
        bool _DesactivaFacturaTipoComprobante;
        bool _DesactivaCxCTipoComprobante;
        bool _DesactivaCxPTipoComprobante;
        bool _DesactivaCobranzaTipoComprobante;
        bool _DesactivaPagoTipoComprobante;
        bool _DesactivaBancarioTipoComprobante;
        bool _DesactivaAnticipoTipoComprobante;
        bool _DesactivaCajaChicaTipoComprobante;
        bool _RetenerIVAEnCxp;
        int _ConsecutivoPeriodo;
        string _GetCierreDelEjercicio;
        int _MaxNumLevels;
        int _MaxNumLevelsAtMatrix;
        int _MinNumLevels;
        int _MaxLength;
        bool _UseZeroAtRigth;
        string _Niveles;
         
        XmlReader _ListaGrupoDeActivos;
        #endregion //Variables
        #region Propiedades
        internal bool CancelValidations {
            get { return _CancelValidations; }
            set { _CancelValidations = value; }
        }
        internal eAccionSR Action {
            get { return _Action; }
            set { _Action = value; }
        }
        internal string ExtendedAction {
            get { return _ExtendedAction; }
            set { _ExtendedAction = value; }
        }
        internal string Title {
            get { return _Title; }
            private set { _Title = value; }
        }
        internal ReglasDeContabilizacion CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        internal ILibView CurrentModel {
            get { return _CurrentModel; }
            set { _CurrentModel = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSReglasDeContabilizacion() {
            InitializeComponent();
            InitializeEvents();
            cmbDondeContabilizarRetIva.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eDondeEfectuoContabilizacionRetIVA)));
            cmbTipoContabilizacionCxC.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeContabilizacion)));
            cmbContabIndividualCxc.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionIndividual)));
            cmbContabPorLoteCxC.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionPorLote)));
            cmbTipoContabilizacionCxP.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeContabilizacion)));
            cmbContabIndividualCxP.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionIndividual)));
            cmbContabPorLoteCxP.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionPorLote)));
            cmbTipoContabilizacionCobranza.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeContabilizacion)));
            cmbContabIndividualCobranza.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionIndividual)));
            cmbContabPorLoteCobranza.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionPorLote)));
            cmbTipoContabilizacionPagos.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeContabilizacion)));
            cmbContabIndividualPagos.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionIndividual)));
            cmbContabPorLotePagos.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionPorLote)));
            cmbTipoContabilizacionFacturacion.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeContabilizacion)));
            cmbContabIndividualFacturacion.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionIndividual)));
            cmbContabPorLoteFacturacion.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionPorLote)));
            cmbTipoContabilizacionRDVtas.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeContabilizacion)));
            cmbContabIndividualRDVtas.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionIndividual)));
            cmbContabPorLoteRDVtas.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionPorLote)));
            cmbTipoContabilizacionMovBancario.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeContabilizacion)));
            cmbContabIndividualMovBancario.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionIndividual)));
            cmbContabPorLoteMovBancario.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionPorLote)));
            cmbTipoContabilizacionAnticipo.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeContabilizacion)));
            cmbContabIndividualAnticipo.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionIndividual)));
            cmbContabPorLoteAnticipo.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionPorLote)));
            cmbTipoContabilizacionInventario.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeContabilizacion)));
            cmbContabIndividualPagosSueldos.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionIndividual)));
            cmbTipoContabilizacionDePagosSueldos.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionIndividual)));
			 cmbContabIndividualCajaChica.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionIndividual)));
            cmbContabIndividualRendiciones.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eContabilizacionIndividual)));
        
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Consecutivo Compania";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, ILibView initModel, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (ReglasDeContabilizacion)initInstance;
            _CurrentModel = initModel;
            Title = initModel.MessageName;
            Action = initAction;
            ExtendedAction = initExtendedAction;
            LibXmlDataParse insDataParse = new LibXmlDataParse(((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo);
            _EscogerAuxiliar = true;
            _UsaAuxiliares = insDataParse.GetBool("RecordName", 0, "UsaAuxiliares", false);
            _UsaModuloDeActivoFijo = insDataParse.GetBool("RecordName", 0, "UsaModuloDeActivoFijo", false);
            _UsarResumenDiarioDeVentas = insDataParse.GetBool("RecordName", 0, "UsarResumenDiarioDeVentas", false);
            _UsarRenglonesEnResumenVtas = insDataParse.GetBool("RecordName", 0, "UsarRenglonesEnResumenVtas", false);
            _MuestraTipoComprobante = insDataParse.GetBool("RecordName", 0, "MuestraTipoComprobante", false);
            _PuedoUsarOpcionesDeContribuyenteEspecial = insDataParse.GetBool("RecordName", 0, "PuedoUsarOpcionesDeContribuyenteEspecial", false);
            _ConsecutivoPeriodo = insDataParse.GetInt("RecordName", 0, "ConsecutivoPeriodo", 0);
            _DesactivaFacturaTipoComprobante = insDataParse.GetBool("RecordName", 0, "DesactivaFacturaTipoComprobante", false);
            _DesactivaCxCTipoComprobante = insDataParse.GetBool("RecordName", 0, "DesactivaCxCTipoComprobante", false);
            _DesactivaCxPTipoComprobante = insDataParse.GetBool("RecordName", 0, "DesactivaCxPTipoComprobante", false);
            _DesactivaCobranzaTipoComprobante = insDataParse.GetBool("RecordName", 0, "DesactivaCobranzaTipoComprobante", false);
            _DesactivaPagoTipoComprobante = insDataParse.GetBool("RecordName", 0, "DesactivaPagoTipoComprobante", false);
            _DesactivaBancarioTipoComprobante = insDataParse.GetBool("RecordName", 0, "DesactivaBancarioTipoComprobante", false);
            _DesactivaAnticipoTipoComprobante = insDataParse.GetBool("RecordName", 0, "DesactivaAnticipoTipoComprobante", false);
            _DesactivaCajaChicaTipoComprobante = insDataParse.GetBool("RecordName", 0, "DesactivaCajaChicaTipoComprobante", false);
            _RetenerIVAEnCxp = insDataParse.GetBool("RecordName", 0, "RetenerIVAEnCxp", false);
            _GetCierreDelEjercicio = insDataParse.GetString("RecordName", 0, "GetCierreDelEjercicio", "");
            _MaxNumLevels = insDataParse.GetInt("RecordName", 0, "MaxNumLevels", 0);
            _MaxNumLevelsAtMatrix = insDataParse.GetInt("RecordName", 0, "MaxNumLevelsAtMatrix", 0);
            _MinNumLevels = insDataParse.GetInt("RecordName", 0, "MinNumLevels", 0);
            _MaxLength = insDataParse.GetInt("RecordName", 0, "MaxLength", 0);
            _UseZeroAtRigth = insDataParse.GetBool("RecordName", 0, "UseZeroAtRigth", false);
            _Niveles = insDataParse.GetString("RecordName", 0, "Niveles", "");
            LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar });
            if (Action == eAccionSR.Insertar) {
                SetFormValuesFromNavigator(true);
            } else {
                SetFormValuesFromNavigator(false);
            }
            // LibApiAwp.EnableControl(txtConsecutivoCompania, Action == eAccionSR.Insertar);
            SetLookAndFeelForCurrentRecord();
            clsReglasDeContabilizacionIpl insReglasDeContabilizacionIpl = new clsReglasDeContabilizacionIpl(((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc);
            _ListaGrupoDeActivos =  insReglasDeContabilizacionIpl.XmlListaGrupoDeActivos(_ConsecutivoPeriodo);
            EnableControlDelFrm();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
                if (!LibConvert.SNToBool(((clsReglasDeContabilizacionIpl)_CurrentModel).AppMemoryInfo.GlobalValuesGetString("RecordName", "ServerVersionIs2008OrHigher"))) {
                    tbiCajaChica.Visibility = System.Windows.Visibility.Hidden;
                }
          
        }

        internal void SetNavigatorValuesFromForm() {
            _CurrentInstance.DiferenciaEnCambioyCalculo = txtDiferenciaEnCambioyCalculo.Text;
            _CurrentInstance.CuentaIva1Credito = txtCuentaIva1Credito.Text;
            _CurrentInstance.CuentaIva1Debito = txtCuentaIva1Debito.Text;
            _CurrentInstance.DondeContabilizarRetIvaAsEnum = (eDondeEfectuoContabilizacionRetIVA)cmbDondeContabilizarRetIva.SelectedItemToInt();
            _CurrentInstance.CuentaRetencionIva = txtCuentaRetencionIva.Text;
            _CurrentInstance.CxCTipoComprobante = txtCxCTipoComprobante.Text;
            _CurrentInstance.TipoContabilizacionCxCAsEnum = (eTipoDeContabilizacion)cmbTipoContabilizacionCxC.SelectedItemToInt();
            _CurrentInstance.ContabIndividualCxcAsEnum = (eContabilizacionIndividual)cmbContabIndividualCxc.SelectedItemToInt();
            _CurrentInstance.ContabPorLoteCxCAsEnum = (eContabilizacionPorLote)cmbContabPorLoteCxC.SelectedItemToInt();
            _CurrentInstance.CuentaCxCClientes = txtCuentaCxCClientes.Text;
            _CurrentInstance.CuentaCxCIngresos = txtCuentaCxCIngresos.Text;
            _CurrentInstance.CxPTipoComprobante = txtCxPTipoComprobante.Text;
            _CurrentInstance.TipoContabilizacionCxPAsEnum = (eTipoDeContabilizacion)cmbTipoContabilizacionCxP.SelectedItemToInt();
            _CurrentInstance.ContabIndividualCxPAsEnum = (eContabilizacionIndividual)cmbContabIndividualCxP.SelectedItemToInt();
            _CurrentInstance.ContabPorLoteCxPAsEnum = (eContabilizacionPorLote)cmbContabPorLoteCxP.SelectedItemToInt();
            _CurrentInstance.CuentaCxPGasto = txtCuentaCxPGasto.Text;
            _CurrentInstance.CuentaCxPProveedores = txtCuentaCxPProveedores.Text;
            _CurrentInstance.CuentaRetencionImpuestoMunicipal = txtCuentaRetencionImpuestoMunicipal.Text;
            _CurrentInstance.CobranzaTipoComprobante = txtCobranzaTipoComprobante.Text;
            _CurrentInstance.TipoContabilizacionCobranzaAsEnum = (eTipoDeContabilizacion)cmbTipoContabilizacionCobranza.SelectedItemToInt();
            _CurrentInstance.ContabIndividualCobranzaAsEnum = (eContabilizacionIndividual)cmbContabIndividualCobranza.SelectedItemToInt();
            _CurrentInstance.ContabPorLoteCobranzaAsEnum = (eContabilizacionPorLote)cmbContabPorLoteCobranza.SelectedItemToInt();
            _CurrentInstance.CuentaCobranzaCobradoEnEfectivo = txtCuentaCobranzaCobradoEnEfectivo.Text;
            _CurrentInstance.CuentaCobranzaCobradoEnCheque = txtCuentaCobranzaCobradoEnCheque.Text;
            _CurrentInstance.CuentaCobranzaCobradoEnTarjeta = txtCuentaCobranzaCobradoEnTarjeta.Text;
            _CurrentInstance.cuentaCobranzaRetencionISLR = txtcuentaCobranzaRetencionISLR.Text;
            _CurrentInstance.cuentaCobranzaRetencionIVA = txtcuentaCobranzaRetencionIVA.Text;
            _CurrentInstance.CuentaCobranzaOtros = txtCuentaCobranzaOtros.Text;
            _CurrentInstance.CuentaCobranzaCxCClientes = txtCuentaCobranzaCxCClientes.Text;
            _CurrentInstance.CuentaCobranzaCobradoAnticipo = txtCuentaCobranzaCobradoAnticipo.Text;
            _CurrentInstance.PagoTipoComprobante = txtPagoTipoComprobante.Text;
            _CurrentInstance.TipoContabilizacionPagosAsEnum = (eTipoDeContabilizacion)cmbTipoContabilizacionPagos.SelectedItemToInt();
            _CurrentInstance.ContabIndividualPagosAsEnum = (eContabilizacionIndividual)cmbContabIndividualPagos.SelectedItemToInt();
            _CurrentInstance.ContabPorLotePagosAsEnum = (eContabilizacionPorLote)cmbContabPorLotePagos.SelectedItemToInt();
            _CurrentInstance.CuentaPagosCxPProveedores = txtCuentaPagosCxPProveedores.Text;
            _CurrentInstance.CuentaPagosRetencionISLR = txtCuentaPagosRetencionISLR.Text;
            _CurrentInstance.CuentaPagosOtros = txtCuentaPagosOtros.Text;
            _CurrentInstance.CuentaPagosBanco = txtCuentaPagosBanco.Text;
            _CurrentInstance.CuentaPagosPagadoAnticipo = txtCuentaPagosPagadoAnticipo.Text;
            _CurrentInstance.TipoContabilizacionFacturacionAsEnum = (eTipoDeContabilizacion)cmbTipoContabilizacionFacturacion.SelectedItemToInt();
            _CurrentInstance.ContabIndividualFacturacionAsEnum = (eContabilizacionIndividual)cmbContabIndividualFacturacion.SelectedItemToInt();
            _CurrentInstance.ContabPorLoteFacturacionAsEnum = (eContabilizacionPorLote)cmbContabPorLoteFacturacion.SelectedItemToInt();
            _CurrentInstance.CuentaFacturacionCxCClientes = txtCuentaFacturacionCxCClientes.Text;
            _CurrentInstance.CuentaFacturacionMontoTotalFactura = txtCuentaFacturacionMontoTotalFactura.Text;
            _CurrentInstance.CuentaFacturacionCargos = txtCuentaFacturacionCargos.Text;
            _CurrentInstance.CuentaFacturacionDescuentos = txtCuentaFacturacionDescuentos.Text;
            _CurrentInstance.ContabilizarPorArticuloAsBool = chkContabilizarPorArticulo.IsChecked.Value;
            _CurrentInstance.AgruparPorCuentaDeArticuloAsBool = chkAgruparPorCuentaDeArticulo.IsChecked.Value;
            _CurrentInstance.AgruparPorCargosDescuentosAsBool = chkAgruparPorCargosDescuentos.IsChecked.Value;
            _CurrentInstance.FacturaTipoComprobante = txtFacturaTipoComprobante.Text;
            _CurrentInstance.TipoContabilizacionRDVtasAsEnum = (eTipoDeContabilizacion)cmbTipoContabilizacionRDVtas.SelectedItemToInt();
            _CurrentInstance.ContabIndividualRDVtasAsEnum = (eContabilizacionIndividual)cmbContabIndividualRDVtas.SelectedItemToInt();
            _CurrentInstance.ContabPorLoteRDVtasAsEnum = (eContabilizacionPorLote)cmbContabPorLoteRDVtas.SelectedItemToInt();
            _CurrentInstance.CuentaRDVtasCaja = txtCuentaRDVtasCaja.Text;
            _CurrentInstance.CuentaRDVtasMontoTotal = txtCuentaRDVtasMontoTotal.Text;
            _CurrentInstance.ContabilizarPorArticuloRDVtasAsBool = chkContabilizarPorArticuloRDVtas.IsChecked.Value;
            _CurrentInstance.AgruparPorCuentaDeArticuloRDVtasAsBool = chkAgruparPorCuentaDeArticuloRDVtas.IsChecked.Value;
            _CurrentInstance.MovimientoBancarioTipoComprobante = txtMovimientoBancarioTipoComprobante.Text;
            _CurrentInstance.TipoContabilizacionMovBancarioAsEnum = (eTipoDeContabilizacion)cmbTipoContabilizacionMovBancario.SelectedItemToInt();
            _CurrentInstance.ContabIndividualMovBancarioAsEnum = (eContabilizacionIndividual)cmbContabIndividualMovBancario.SelectedItemToInt();
            _CurrentInstance.ContabPorLoteMovBancarioAsEnum = (eContabilizacionPorLote)cmbContabPorLoteMovBancario.SelectedItemToInt();
            _CurrentInstance.CuentaMovBancarioGasto = txtCuentaMovBancarioGasto.Text;
            _CurrentInstance.CuentaMovBancarioBancosHaber = txtCuentaMovBancarioBancosHaber.Text;
            _CurrentInstance.CuentaMovBancarioBancosDebe = txtCuentaMovBancarioBancosDebe.Text;
            _CurrentInstance.CuentaMovBancarioIngresos = txtCuentaMovBancarioIngresos.Text;
            _CurrentInstance.CuentaDebitoBancarioGasto = txtCuentaDebitoBancarioGasto.Text;
            _CurrentInstance.CuentaDebitoBancarioBancos = txtCuentaDebitoBancarioBancos.Text;
            _CurrentInstance.CuentaCreditoBancarioGasto = txtCuentaCreditoBancarioGasto.Text;
            _CurrentInstance.CuentaCreditoBancarioBancos = txtCuentaCreditoBancarioBancos.Text;
            _CurrentInstance.AnticipoTipoComprobante = txtAnticipoTipoComprobante.Text;
            _CurrentInstance.TipoContabilizacionAnticipoAsEnum = (eTipoDeContabilizacion)cmbTipoContabilizacionAnticipo.SelectedItemToInt();
            _CurrentInstance.ContabIndividualAnticipoAsEnum = (eContabilizacionIndividual)cmbContabIndividualAnticipo.SelectedItemToInt();
            _CurrentInstance.ContabPorLoteAnticipoAsEnum = (eContabilizacionPorLote)cmbContabPorLoteAnticipo.SelectedItemToInt();
            _CurrentInstance.CuentaAnticipoCaja = txtCuentaAnticipoCaja.Text;
            _CurrentInstance.CuentaAnticipoCobrado = txtCuentaAnticipoCobrado.Text;
            _CurrentInstance.CuentaAnticipoOtrosIngresos = txtCuentaAnticipoOtrosIngresos.Text;
            _CurrentInstance.CuentaAnticipoPagado = txtCuentaAnticipoPagado.Text;
            _CurrentInstance.CuentaAnticipoBanco = txtCuentaAnticipoBanco.Text;
            _CurrentInstance.CuentaAnticipoOtrosEgresos = txtCuentaAnticipoOtrosEgresos.Text;
            _CurrentInstance.CuentaCostoDeVenta = txtCuentaCostoDeVenta.Text;
            _CurrentInstance.CuentaInventario = txtCuentaInventario.Text;
            _CurrentInstance.TipoContabilizacionInventarioAsEnum = (eTipoDeContabilizacion)cmbTipoContabilizacionInventario.SelectedItemToInt();
            _CurrentInstance.AgruparPorCuentaDeArticuloInvenAsBool = chkAgruparPorCuentaDeArticuloInven.IsChecked.Value;
            _CurrentInstance.InventarioTipoComprobante = txtInventarioTipoComprobante.Text;
            _CurrentInstance.CtaDePagosSueldos = txtCtaDePagosSueldos.Text;
            _CurrentInstance.CtaDePagosSueldosBanco = txtCtaDePagosSueldosBanco.Text;
            _CurrentInstance.ContabIndividualPagosSueldosAsEnum = (eContabilizacionIndividual) cmbContabIndividualPagosSueldos.SelectedItemToInt();
            _CurrentInstance.PagosSueldosTipoComprobante = txtPagosSueldosTipoComprobante.Text;
            _CurrentInstance.TipoContabilizacionDePagosSueldosAsEnum = (eContabilizacionIndividual)cmbTipoContabilizacionDePagosSueldos.SelectedItemToInt();
            _CurrentInstance.EditarComprobanteDePagosSueldosAsBool = chkEditarComprobanteDePagosSueldos.IsChecked.Value;
            _CurrentInstance.EditarComprobanteAfterInsertCxCAsBool = chkEditarComprobanteAfterInsertCxC.IsChecked.Value;
            _CurrentInstance.EditarComprobanteAfterInsertCxPAsBool = chkEditarComprobanteAfterInsertCxP.IsChecked.Value;
            _CurrentInstance.EditarComprobanteAfterInsertCobranzaAsBool = chkEditarComprobanteAfterInsertCobranza.IsChecked.Value;
            _CurrentInstance.EditarComprobanteAfterInsertPagosAsBool = chkEditarComprobanteAfterInsertPagos.IsChecked.Value;
            _CurrentInstance.EditarComprobanteAfterInsertFacturaAsBool = chkEditarComprobanteAfterInsertFactura.IsChecked.Value;
            _CurrentInstance.EditarComprobanteAfterInsertResDiaAsBool = chkEditarComprobanteAfterInsertResDia.IsChecked.Value;
            _CurrentInstance.EditarComprobanteAfterInsertMovBanAsBool = chkEditarComprobanteAfterInsertMovBan.IsChecked.Value;
            _CurrentInstance.EditarComprobanteAfterInsertImpTraBanAsBool = chkEditarComprobanteAfterInsertImpTraBan.IsChecked.Value;
            _CurrentInstance.EditarComprobanteAfterInsertAnticipoAsBool = chkEditarComprobanteAfterInsertAnticipo.IsChecked.Value;
            _CurrentInstance.EditarComprobanteAfterInsertInventarioAsBool = chkEditarComprobanteAfterInsertInventario.IsChecked.Value;
			_CurrentInstance.EditarComprobanteAfterInsertCajaChicaAsBool = chkEditarComprobanteAfterInsertCajaChica.IsChecked.Value;			
			 _CurrentInstance.SiglasTipoComprobanteCajaChica = txtSiglasTipoComprobanteCajaChica.Text;
            _CurrentInstance.ContabIndividualCajaChicaAsEnum = (eContabilizacionIndividual) cmbContabIndividualCajaChica.SelectedItemToInt();
            _CurrentInstance.CuentaCajaChicaGasto = txtCuentaCajaChicaGasto.Text;
            _CurrentInstance.MostrarDesglosadoCajaChicaAsBool = chkMostrarDesglosadoCajaChica.IsChecked.Value;
            _CurrentInstance.CuentaCajaChicaBancoHaber = txtCuentaCajaChicaBancoHaber.Text;
            _CurrentInstance.CuentaCajaChicaBancoDebe = txtCuentaCajaChicaBancoDebe.Text;
            _CurrentInstance.CuentaCajaChicaBanco = txtCuentaCajaChicaBanco.Text;
            _CurrentInstance.SiglasTipoComprobanteRendiciones = txtSiglasTipoComprobanteRendiciones.Text;
            _CurrentInstance.ContabIndividualRendicionesAsEnum = (eContabilizacionIndividual) cmbContabIndividualRendiciones.SelectedItemToInt();
            _CurrentInstance.CuentaRendicionesGasto = txtCuentaRendicionesGasto.Text;
            _CurrentInstance.CuentaRendicionesBanco = txtCuentaRendicionesBanco.Text;
            _CurrentInstance.CuentaRendicionesAnticipos = txtCuentaRendicionesAnticipos.Text;
            _CurrentInstance.MostrarDesglosadoRendicionesAsBool = chkMostrarDesglosadoRendiciones.IsChecked.Value;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            if (valClearRecord) {
                _CurrentModel.Clear(_CurrentInstance);
            }
            ClearControl();
            //  txtConsecutivoCompania.Text = LibConvert.ToStr(_CurrentInstance.ConsecutivoCompania);
            txtDiferenciaEnCambioyCalculo.Text = _CurrentInstance.DiferenciaEnCambioyCalculo;
            txtCuentaIva1Credito.Text = _CurrentInstance.CuentaIva1Credito;
            txtCuentaIva1Debito.Text = _CurrentInstance.CuentaIva1Debito;
            cmbDondeContabilizarRetIva.SelectItem(_CurrentInstance.DondeContabilizarRetIvaAsEnum);
            txtCuentaRetencionIva.Text = _CurrentInstance.CuentaRetencionIva;
            txtCxCTipoComprobante.Text = _CurrentInstance.CxCTipoComprobante;
            cmbTipoContabilizacionCxC.SelectItem(_CurrentInstance.TipoContabilizacionCxCAsEnum);
            cmbContabIndividualCxc.SelectItem(_CurrentInstance.ContabIndividualCxcAsEnum);
            cmbContabPorLoteCxC.SelectItem(_CurrentInstance.ContabPorLoteCxCAsEnum);
            txtCuentaCxCClientes.Text = _CurrentInstance.CuentaCxCClientes;
            txtCuentaCxCIngresos.Text = _CurrentInstance.CuentaCxCIngresos;
            txtCxPTipoComprobante.Text = _CurrentInstance.CxPTipoComprobante;
            cmbTipoContabilizacionCxP.SelectItem(_CurrentInstance.TipoContabilizacionCxPAsEnum);
            cmbContabIndividualCxP.SelectItem(_CurrentInstance.ContabIndividualCxPAsEnum);
            cmbContabPorLoteCxP.SelectItem(_CurrentInstance.ContabPorLoteCxPAsEnum);
            txtCuentaCxPGasto.Text = _CurrentInstance.CuentaCxPGasto;
            txtCuentaCxPProveedores.Text = _CurrentInstance.CuentaCxPProveedores;
            txtCuentaRetencionImpuestoMunicipal.Text = _CurrentInstance.CuentaRetencionImpuestoMunicipal;
            txtCobranzaTipoComprobante.Text = _CurrentInstance.CobranzaTipoComprobante;
            cmbTipoContabilizacionCobranza.SelectItem(_CurrentInstance.TipoContabilizacionCobranzaAsEnum);
            cmbContabIndividualCobranza.SelectItem(_CurrentInstance.ContabIndividualCobranzaAsEnum);
            cmbContabPorLoteCobranza.SelectItem(_CurrentInstance.ContabPorLoteCobranzaAsEnum);
            txtCuentaCobranzaCobradoEnEfectivo.Text = _CurrentInstance.CuentaCobranzaCobradoEnEfectivo;
            txtCuentaCobranzaCobradoEnCheque.Text = _CurrentInstance.CuentaCobranzaCobradoEnCheque;
            txtCuentaCobranzaCobradoEnTarjeta.Text = _CurrentInstance.CuentaCobranzaCobradoEnTarjeta;
            txtcuentaCobranzaRetencionISLR.Text = _CurrentInstance.cuentaCobranzaRetencionISLR;
            txtcuentaCobranzaRetencionIVA.Text = _CurrentInstance.cuentaCobranzaRetencionIVA;
            txtCuentaCobranzaOtros.Text = _CurrentInstance.CuentaCobranzaOtros;
            txtCuentaCobranzaCxCClientes.Text = _CurrentInstance.CuentaCobranzaCxCClientes;
            txtCuentaCobranzaCobradoAnticipo.Text = _CurrentInstance.CuentaCobranzaCobradoAnticipo;
            txtPagoTipoComprobante.Text = _CurrentInstance.PagoTipoComprobante;
            cmbTipoContabilizacionPagos.SelectItem(_CurrentInstance.TipoContabilizacionPagosAsEnum);
            cmbContabIndividualPagos.SelectItem(_CurrentInstance.ContabIndividualPagosAsEnum);
            cmbContabPorLotePagos.SelectItem(_CurrentInstance.ContabPorLotePagosAsEnum);
            txtCuentaPagosCxPProveedores.Text = _CurrentInstance.CuentaPagosCxPProveedores;
            txtCuentaPagosRetencionISLR.Text = _CurrentInstance.CuentaPagosRetencionISLR;
            txtCuentaPagosOtros.Text = _CurrentInstance.CuentaPagosOtros;
            txtCuentaPagosBanco.Text = _CurrentInstance.CuentaPagosBanco;
            txtCuentaPagosPagadoAnticipo.Text = _CurrentInstance.CuentaPagosPagadoAnticipo;
            cmbTipoContabilizacionFacturacion.SelectItem(_CurrentInstance.TipoContabilizacionFacturacionAsEnum);
            cmbContabIndividualFacturacion.SelectItem(_CurrentInstance.ContabIndividualFacturacionAsEnum);
            cmbContabPorLoteFacturacion.SelectItem(_CurrentInstance.ContabPorLoteFacturacionAsEnum);
            txtCuentaFacturacionCxCClientes.Text = _CurrentInstance.CuentaFacturacionCxCClientes;
            txtCuentaFacturacionMontoTotalFactura.Text = _CurrentInstance.CuentaFacturacionMontoTotalFactura;
            txtCuentaFacturacionCargos.Text = _CurrentInstance.CuentaFacturacionCargos;
            txtCuentaFacturacionDescuentos.Text = _CurrentInstance.CuentaFacturacionDescuentos;
            chkContabilizarPorArticulo.IsChecked = _CurrentInstance.ContabilizarPorArticuloAsBool;
            chkAgruparPorCuentaDeArticulo.IsChecked = _CurrentInstance.AgruparPorCuentaDeArticuloAsBool;
            chkAgruparPorCargosDescuentos.IsChecked = _CurrentInstance.AgruparPorCargosDescuentosAsBool;
            txtFacturaTipoComprobante.Text = _CurrentInstance.FacturaTipoComprobante;
            cmbTipoContabilizacionRDVtas.SelectItem(_CurrentInstance.TipoContabilizacionRDVtasAsEnum);
            cmbContabIndividualRDVtas.SelectItem(_CurrentInstance.ContabIndividualRDVtasAsEnum);
            cmbContabPorLoteRDVtas.SelectItem(_CurrentInstance.ContabPorLoteRDVtasAsEnum);
            txtCuentaRDVtasCaja.Text = _CurrentInstance.CuentaRDVtasCaja;
            txtCuentaRDVtasMontoTotal.Text = _CurrentInstance.CuentaRDVtasMontoTotal;
            chkContabilizarPorArticuloRDVtas.IsChecked = _CurrentInstance.ContabilizarPorArticuloRDVtasAsBool;
            chkAgruparPorCuentaDeArticuloRDVtas.IsChecked = _CurrentInstance.AgruparPorCuentaDeArticuloRDVtasAsBool;
            txtMovimientoBancarioTipoComprobante.Text = _CurrentInstance.MovimientoBancarioTipoComprobante;
            cmbTipoContabilizacionMovBancario.SelectItem(_CurrentInstance.TipoContabilizacionMovBancarioAsEnum);
            cmbContabIndividualMovBancario.SelectItem(_CurrentInstance.ContabIndividualMovBancarioAsEnum);
            cmbContabPorLoteMovBancario.SelectItem(_CurrentInstance.ContabPorLoteMovBancarioAsEnum);
            txtCuentaMovBancarioGasto.Text = _CurrentInstance.CuentaMovBancarioGasto;
            txtCuentaMovBancarioBancosHaber.Text = _CurrentInstance.CuentaMovBancarioBancosHaber;
            txtCuentaMovBancarioBancosDebe.Text = _CurrentInstance.CuentaMovBancarioBancosDebe;
            txtCuentaMovBancarioIngresos.Text = _CurrentInstance.CuentaMovBancarioIngresos;
            txtCuentaDebitoBancarioGasto.Text = _CurrentInstance.CuentaDebitoBancarioGasto;
            txtCuentaDebitoBancarioBancos.Text = _CurrentInstance.CuentaDebitoBancarioBancos;
            txtCuentaCreditoBancarioGasto.Text = _CurrentInstance.CuentaCreditoBancarioGasto;
            txtCuentaCreditoBancarioBancos.Text = _CurrentInstance.CuentaCreditoBancarioBancos;
            txtAnticipoTipoComprobante.Text = _CurrentInstance.AnticipoTipoComprobante;
            cmbTipoContabilizacionAnticipo.SelectItem(_CurrentInstance.TipoContabilizacionAnticipoAsEnum);
            cmbContabIndividualAnticipo.SelectItem(_CurrentInstance.ContabIndividualAnticipoAsEnum);
            cmbContabPorLoteAnticipo.SelectItem(_CurrentInstance.ContabPorLoteAnticipoAsEnum);
            txtCuentaAnticipoCaja.Text = _CurrentInstance.CuentaAnticipoCaja;
            txtCuentaAnticipoCobrado.Text = _CurrentInstance.CuentaAnticipoCobrado;
            txtCuentaAnticipoOtrosIngresos.Text = _CurrentInstance.CuentaAnticipoOtrosIngresos;
            txtCuentaAnticipoPagado.Text = _CurrentInstance.CuentaAnticipoPagado;
            txtCuentaAnticipoBanco.Text = _CurrentInstance.CuentaAnticipoBanco;
            txtCuentaAnticipoOtrosEgresos.Text = _CurrentInstance.CuentaAnticipoOtrosEgresos;
            txtCuentaCostoDeVenta.Text = _CurrentInstance.CuentaCostoDeVenta;
            txtCuentaInventario.Text = _CurrentInstance.CuentaInventario;
            cmbTipoContabilizacionInventario.SelectItem(_CurrentInstance.TipoContabilizacionInventarioAsEnum);
            chkAgruparPorCuentaDeArticuloInven.IsChecked = _CurrentInstance.AgruparPorCuentaDeArticuloInvenAsBool;
            txtInventarioTipoComprobante.Text = _CurrentInstance.InventarioTipoComprobante;
            txtCtaDePagosSueldos.Text = _CurrentInstance.CtaDePagosSueldos;
            txtCtaDePagosSueldosBanco.Text = _CurrentInstance.CtaDePagosSueldosBanco;
            cmbContabIndividualPagosSueldos.SelectItem(_CurrentInstance.ContabIndividualPagosSueldosAsEnum);
            txtPagosSueldosTipoComprobante.Text = _CurrentInstance.PagosSueldosTipoComprobante;
            cmbTipoContabilizacionDePagosSueldos.SelectItem(_CurrentInstance.TipoContabilizacionDePagosSueldosAsEnum);
            chkEditarComprobanteDePagosSueldos.IsChecked = _CurrentInstance.EditarComprobanteDePagosSueldosAsBool;
            chkEditarComprobanteAfterInsertCxC.IsChecked = _CurrentInstance.EditarComprobanteAfterInsertCxCAsBool;
            chkEditarComprobanteAfterInsertCxP.IsChecked = _CurrentInstance.EditarComprobanteAfterInsertCxPAsBool;
            chkEditarComprobanteAfterInsertCobranza.IsChecked = _CurrentInstance.EditarComprobanteAfterInsertCobranzaAsBool;
            chkEditarComprobanteAfterInsertPagos.IsChecked = _CurrentInstance.EditarComprobanteAfterInsertPagosAsBool;
            chkEditarComprobanteAfterInsertFactura.IsChecked = _CurrentInstance.EditarComprobanteAfterInsertFacturaAsBool;
            chkEditarComprobanteAfterInsertResDia.IsChecked = _CurrentInstance.EditarComprobanteAfterInsertResDiaAsBool;
            chkEditarComprobanteAfterInsertMovBan.IsChecked = _CurrentInstance.EditarComprobanteAfterInsertMovBanAsBool;
            chkEditarComprobanteAfterInsertImpTraBan.IsChecked = _CurrentInstance.EditarComprobanteAfterInsertImpTraBanAsBool;
            chkEditarComprobanteAfterInsertAnticipo.IsChecked = _CurrentInstance.EditarComprobanteAfterInsertAnticipoAsBool;
            chkEditarComprobanteAfterInsertInventario.IsChecked = _CurrentInstance.EditarComprobanteAfterInsertInventarioAsBool;
			chkEditarComprobanteAfterInsertCajaChica.IsChecked = _CurrentInstance.EditarComprobanteAfterInsertCajaChicaAsBool;
			txtSiglasTipoComprobanteCajaChica.Text = _CurrentInstance.SiglasTipoComprobanteCajaChica;
            cmbContabIndividualCajaChica.SelectItem(_CurrentInstance.ContabIndividualCajaChicaAsEnum);
            txtCuentaCajaChicaGasto.Text = _CurrentInstance.CuentaCajaChicaGasto;
            chkMostrarDesglosadoCajaChica.IsChecked = _CurrentInstance.MostrarDesglosadoCajaChicaAsBool;
            txtCuentaCajaChicaBancoHaber.Text = _CurrentInstance.CuentaCajaChicaBancoHaber;
            txtCuentaCajaChicaBancoDebe.Text = _CurrentInstance.CuentaCajaChicaBancoDebe;
            txtCuentaCajaChicaBanco.Text = _CurrentInstance.CuentaCajaChicaBanco;
            txtSiglasTipoComprobanteRendiciones.Text = _CurrentInstance.SiglasTipoComprobanteRendiciones;
            cmbContabIndividualRendiciones.SelectItem(_CurrentInstance.ContabIndividualRendicionesAsEnum);
            txtCuentaRendicionesGasto.Text = _CurrentInstance.CuentaRendicionesGasto;
            txtCuentaRendicionesIva.Text = _CurrentInstance.CuentaIva1Debito;
            txtCuentaRendicionesBanco.Text = _CurrentInstance.CuentaRendicionesBanco;
            txtCuentaRendicionesAnticipos.Text = _CurrentInstance.CuentaRendicionesAnticipos;
            chkMostrarDesglosadoRendiciones.IsChecked = _CurrentInstance.MostrarDesglosadoRendicionesAsBool;
            RealizaLosCalculos();
            DesactivaLosTipoComprobanteQueTenganRecordsAsociados();
            SetDescripcion();
            CopiarReglas();
            DesactivaTodosLosComboBox();
            sDecideSiMuestraTipoComprobante();
            ActivaCamposDeContribuyentesEspeciales();
            DecideSiMuestraReglasDeResumenDiarioDeVentas();
            ActivaCamposDeResumenDiarioDeVentas();
            MostarUOcultarCampos();
            EnableControlDelFrm();
          
         }

        private void InitializeEvents() {
            //   this.txtConsecutivoCompania.Validating += new System.ComponentModel.CancelEventHandler(txtConsecutivoCompania_Validating);
            this.txtDiferenciaEnCambioyCalculo.Validating += new System.ComponentModel.CancelEventHandler(txtDiferenciaEnCambioyCalculo_Validating);
            this.txtCuentaIva1Credito.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaIva1Credito_Validating);
            this.txtCuentaIva1Debito.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaIva1Debito_Validating);
            this.cmbDondeContabilizarRetIva.Validating += new System.ComponentModel.CancelEventHandler(cmbDondeContabilizarRetIva_Validating);
            this.txtCuentaRetencionIva.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaRetencionIva_Validating);
            this.txtCxCTipoComprobante.Validating += new System.ComponentModel.CancelEventHandler(txtCxCTipoComprobante_Validating);
            this.cmbTipoContabilizacionCxC.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoContabilizacionCxC_Validating);
            this.cmbContabIndividualCxc.Validating += new System.ComponentModel.CancelEventHandler(cmbContabIndividualCxc_Validating);
            this.cmbContabPorLoteCxC.Validating += new System.ComponentModel.CancelEventHandler(cmbContabPorLoteCxC_Validating);
            this.txtCuentaCxCClientes.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaCxCClientes_Validating);
            this.txtCuentaCxCIngresos.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaCxCIngresos_Validating);
            this.txtCxPTipoComprobante.Validating += new System.ComponentModel.CancelEventHandler(txtCxPTipoComprobante_Validating);
            this.cmbTipoContabilizacionCxP.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoContabilizacionCxP_Validating);
            this.cmbContabIndividualCxP.Validating += new System.ComponentModel.CancelEventHandler(cmbContabIndividualCxP_Validating);
            this.cmbContabPorLoteCxP.Validating += new System.ComponentModel.CancelEventHandler(cmbContabPorLoteCxP_Validating);
            this.txtCuentaCxPGasto.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaCxPGasto_Validating);
            this.txtCuentaCxPProveedores.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaCxPProveedores_Validating);
            this.txtCuentaRetencionImpuestoMunicipal.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaRetencionImpuestoMunicipal_Validating);
            this.txtCobranzaTipoComprobante.Validating += new System.ComponentModel.CancelEventHandler(txtCobranzaTipoComprobante_Validating);
            this.cmbTipoContabilizacionCobranza.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoContabilizacionCobranza_Validating);
            this.cmbContabIndividualCobranza.Validating += new System.ComponentModel.CancelEventHandler(cmbContabIndividualCobranza_Validating);
            this.cmbContabPorLoteCobranza.Validating += new System.ComponentModel.CancelEventHandler(cmbContabPorLoteCobranza_Validating);
            this.txtCuentaCobranzaCobradoEnEfectivo.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaCobranzaCobradoEnEfectivo_Validating);
            this.txtCuentaCobranzaCobradoEnCheque.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaCobranzaCobradoEnCheque_Validating);
            this.txtCuentaCobranzaCobradoEnTarjeta.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaCobranzaCobradoEnTarjeta_Validating);
            this.txtcuentaCobranzaRetencionISLR.Validating += new System.ComponentModel.CancelEventHandler(txtcuentaCobranzaRetencionISLR_Validating);
            this.txtcuentaCobranzaRetencionIVA.Validating += new System.ComponentModel.CancelEventHandler(txtcuentaCobranzaRetencionIVA_Validating);
            this.txtCuentaCobranzaOtros.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaCobranzaOtros_Validating);
            this.txtCuentaCobranzaCxCClientes.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaCobranzaCxCClientes_Validating);
            this.txtCuentaCobranzaCobradoAnticipo.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaCobranzaCobradoAnticipo_Validating);
            this.txtPagoTipoComprobante.Validating += new System.ComponentModel.CancelEventHandler(txtPagoTipoComprobante_Validating);
            this.cmbTipoContabilizacionPagos.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoContabilizacionPagos_Validating);
            this.cmbContabIndividualPagos.Validating += new System.ComponentModel.CancelEventHandler(cmbContabIndividualPagos_Validating);
            this.cmbContabPorLotePagos.Validating += new System.ComponentModel.CancelEventHandler(cmbContabPorLotePagos_Validating);
            this.txtCuentaPagosCxPProveedores.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaPagosCxPProveedores_Validating);
            this.txtCuentaPagosRetencionISLR.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaPagosRetencionISLR_Validating);
            this.txtCuentaPagosOtros.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaPagosOtros_Validating);
            this.txtCuentaPagosBanco.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaPagosBanco_Validating);
            this.txtCuentaPagosPagadoAnticipo.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaPagosPagadoAnticipo_Validating);
            this.cmbTipoContabilizacionFacturacion.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoContabilizacionFacturacion_Validating);
            this.cmbContabIndividualFacturacion.Validating += new System.ComponentModel.CancelEventHandler(cmbContabIndividualFacturacion_Validating);
            this.cmbContabPorLoteFacturacion.Validating += new System.ComponentModel.CancelEventHandler(cmbContabPorLoteFacturacion_Validating);
            this.txtCuentaFacturacionCxCClientes.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaFacturacionCxCClientes_Validating);
            this.txtCuentaFacturacionMontoTotalFactura.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaFacturacionMontoTotalFactura_Validating);
            this.txtCuentaFacturacionCargos.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaFacturacionCargos_Validating);
            this.txtCuentaFacturacionDescuentos.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaFacturacionDescuentos_Validating);
            this.txtFacturaTipoComprobante.Validating += new System.ComponentModel.CancelEventHandler(txtFacturaTipoComprobante_Validating);
            this.cmbTipoContabilizacionRDVtas.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoContabilizacionRDVtas_Validating);
            this.cmbContabIndividualRDVtas.Validating += new System.ComponentModel.CancelEventHandler(cmbContabIndividualRDVtas_Validating);
            this.cmbContabPorLoteRDVtas.Validating += new System.ComponentModel.CancelEventHandler(cmbContabPorLoteRDVtas_Validating);
            this.txtCuentaRDVtasCaja.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaRDVtasCaja_Validating);
            this.txtCuentaRDVtasMontoTotal.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaRDVtasMontoTotal_Validating);
            this.txtMovimientoBancarioTipoComprobante.Validating += new System.ComponentModel.CancelEventHandler(txtMovimientoBancarioTipoComprobante_Validating);
            this.cmbTipoContabilizacionMovBancario.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoContabilizacionMovBancario_Validating);
            this.cmbContabIndividualMovBancario.Validating += new System.ComponentModel.CancelEventHandler(cmbContabIndividualMovBancario_Validating);
            this.cmbContabPorLoteMovBancario.Validating += new System.ComponentModel.CancelEventHandler(cmbContabPorLoteMovBancario_Validating);
            this.txtCuentaMovBancarioGasto.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaMovBancarioGasto_Validating);
            this.txtCuentaMovBancarioBancosHaber.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaMovBancarioBancosHaber_Validating);
            this.txtCuentaMovBancarioBancosDebe.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaMovBancarioBancosDebe_Validating);
            this.txtCuentaMovBancarioIngresos.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaMovBancarioIngresos_Validating);
            this.txtCuentaDebitoBancarioGasto.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaDebitoBancarioGasto_Validating);
            this.txtCuentaDebitoBancarioBancos.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaDebitoBancarioBancos_Validating);
            this.txtCuentaCreditoBancarioGasto.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaCreditoBancarioGasto_Validating);
            this.txtCuentaCreditoBancarioBancos.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaCreditoBancarioBancos_Validating);
            this.txtAnticipoTipoComprobante.Validating += new System.ComponentModel.CancelEventHandler(txtAnticipoTipoComprobante_Validating);
            this.cmbTipoContabilizacionAnticipo.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoContabilizacionAnticipo_Validating);
            this.cmbContabIndividualAnticipo.Validating += new System.ComponentModel.CancelEventHandler(cmbContabIndividualAnticipo_Validating);
            this.cmbContabPorLoteAnticipo.Validating += new System.ComponentModel.CancelEventHandler(cmbContabPorLoteAnticipo_Validating);
            this.txtCuentaAnticipoCaja.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaAnticipoCaja_Validating);
            this.txtCuentaAnticipoCobrado.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaAnticipoCobrado_Validating);
            this.txtCuentaAnticipoOtrosIngresos.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaAnticipoOtrosIngresos_Validating);
            this.txtCuentaAnticipoPagado.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaAnticipoPagado_Validating);
            this.txtCuentaAnticipoBanco.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaAnticipoBanco_Validating);
            this.txtCuentaAnticipoOtrosEgresos.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaAnticipoOtrosEgresos_Validating);
            this.txtCuentaCostoDeVenta.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaCostoDeVenta_Validating);
            this.txtCuentaInventario.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaInventario_Validating);
            this.txtInventarioTipoComprobante.Validating += new CancelEventHandler(txtInventarioTipoComprobante_Validating);
            this.cmbTipoContabilizacionInventario.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoContabilizacionInventario_Validating);
            this.txtCtaDePagosSueldos.Validating += new System.ComponentModel.CancelEventHandler(txtCtaDePagosSueldos_Validating);
            this.txtCtaDePagosSueldosBanco.Validating += new System.ComponentModel.CancelEventHandler(txtCtaDePagosSueldosBanco_Validating);
            this.cmbContabIndividualPagosSueldos.Validating += new System.ComponentModel.CancelEventHandler(cmbContabIndividualPagosSueldos_Validating);
            this.txtPagosSueldosTipoComprobante.Validating += new System.ComponentModel.CancelEventHandler(txtPagosSueldosTipoComprobante_Validating);
            this.cmbTipoContabilizacionDePagosSueldos.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoContabilizacionDePagosSueldos_Validating);
            this.cmbContabIndividualCajaChica.Validating += new System.ComponentModel.CancelEventHandler(cmbContabIndividualCajaChica_Validating);
            this.txtCuentaCajaChicaGasto.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaCajaChicaGasto_Validating);
            this.txtCuentaCajaChicaBancoHaber.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaCajaChicaBancoHaber_Validating);
            this.txtCuentaCajaChicaBancoDebe.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaCajaChicaBancoDebe_Validating);
            this.txtCuentaCajaChicaBanco.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaCajaChicaBanco_Validating);
		    this.cmbContabIndividualRendiciones.Validating += new System.ComponentModel.CancelEventHandler(cmbContabIndividualRendiciones_Validating);
            this.txtCuentaRendicionesGasto.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaRendicionesGasto_Validating);
            this.txtCuentaRendicionesIva.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaRendicionesIva_Validating);
            this.txtCuentaRendicionesBanco.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaRendicionesBanco_Validating);
            this.txtCuentaRendicionesAnticipos.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaRendicionesAnticipos_Validating);
            this.txtSiglasTipoComprobanteCajaChica.Validating += new System.ComponentModel.CancelEventHandler(txtSiglasTipoComprobanteCajaChica_Validating);
        }

        void txtInventarioTipoComprobante_Validating(object sender, CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtInventarioTipoComprobante.Text) == 0) {
                    txtInventarioTipoComprobante.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "CodigoDelTipo=" + txtInventarioTipoComprobante.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseTipoDeComprobante(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtInventarioTipoComprobante.Text = insParse.GetString(0, "CodigoDelTipo", "");
                } else {
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }



        void txtDiferenciaEnCambioyCalculo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (_CurrentInstance.DiferenciaEnCambioyCalculo != txtDiferenciaEnCambioyCalculo.Text) {
                    if (CancelValidations) {
                        return;
                    }
                    if (LibString.Len(txtDiferenciaEnCambioyCalculo.Text) == 0) {
                        txtDiferenciaEnCambioyCalculo.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" +AjustaCodigo(txtDiferenciaEnCambioyCalculo.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                   
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtDiferenciaEnCambioyCalculo.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.DiferenciaEnCambioyCalculo = txtDiferenciaEnCambioyCalculo.Text;
                        lblDescripcionDiferenciaEnCambioyCalculo.Content = insParse.GetString(0, "Descripcion", ""); 
                        CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaIva1Credito_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaIva1Credito != txtCuentaIva1Credito.Text) {
                    if (LibString.Len(txtCuentaIva1Credito.Text) == 0) {
                        txtCuentaIva1Credito.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaIva1Credito.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaIva1Credito.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaIva1Credito = txtCuentaIva1Credito.Text;
                        lblDescripcionCuentaIva1Credito.Content = insParse.GetString(0, "Descripcion", "");
                        CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaIva1Debito_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaIva1Credito != txtCuentaIva1Debito.Text) {
                    if (LibString.Len(txtCuentaIva1Debito.Text) == 0) {
                        txtCuentaIva1Debito.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaIva1Debito.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaIva1Debito.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaIva1Credito = txtCuentaIva1Debito.Text;
                        lblDescripcionCuentaIva1Debito.Content = insParse.GetString(0, "Descripcion", ""); 
                        CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbDondeContabilizarRetIva_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                ValidacionDondeContabilizarRetIva();
                cmbDondeContabilizarRetIva.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaRetencionIva_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCuentaRetencionIva.Text) == 0) {
                    txtCuentaRetencionIva.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaRetencionIva.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCuentaRetencionIva.Text = insParse.GetString(0, "Codigo", "");
                    lblDescripcionCuentaRetencionIva.Content =   insParse.GetString(0, "Descripcion", "");
                    CopiarReglas();
                } else {
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCxCTipoComprobante_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCxCTipoComprobante.Text) == 0) {
                    txtCxCTipoComprobante.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "CodigoDelTipo=" + txtCxCTipoComprobante.Text + LibText.ColumnSeparator() ;
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseTipoDeComprobante(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCxCTipoComprobante.Text = insParse.GetString(0, "CodigoDelTipo", "");
                } else {
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }


        void cmbTipoContabilizacionCxC_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoContabilizacionCxC.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbContabIndividualCxc_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabIndividualCxc.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbContabPorLoteCxC_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabPorLoteCxC.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaCxCClientes_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaCxCClientes != txtCuentaCxCClientes.Text) {
                    if (LibString.Len(txtCuentaCxCClientes.Text) == 0) {
                        txtCuentaCxCClientes.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaCxCClientes.Text )+ LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaCxCClientes.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaCxCClientes = txtCuentaCxCClientes.Text;
                        lblDescripcionCuentaCxCClientes.Content = insParse.GetString(0, "Descripcion", "");
                        CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaCxCIngresos_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaCxCIngresos != txtCuentaCxCIngresos.Text) {
                    if (LibString.Len(txtCuentaCxCIngresos.Text) == 0) {
                        txtCuentaCxCIngresos.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaCxCIngresos.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaCxCIngresos.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaCxCIngresos = txtCuentaCxCIngresos.Text;
                        lblDescripcionCuentaCxCIngresos.Content = insParse.GetString(0, "Descripcion", "");
                        CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }

                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCxPTipoComprobante_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCxPTipoComprobante.Text) == 0) {
                    txtCxPTipoComprobante.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "CodigoDelTipo=" + txtCxPTipoComprobante.Text + LibText.ColumnSeparator() ;
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseTipoDeComprobante (null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCxPTipoComprobante.Text = insParse.GetString(0, "CodigoDelTipo", "");
                } else {
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbTipoContabilizacionCxP_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoContabilizacionCxP.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbContabIndividualCxP_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabIndividualCxP.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbContabPorLoteCxP_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabPorLoteCxP.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaCxPGasto_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaCxPGasto != txtCuentaCxPGasto.Text) {
                    if (LibString.Len(txtCuentaCxPGasto.Text) == 0) {
                        txtCuentaCxPGasto.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaCxPGasto.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaCxPGasto.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaCxPGasto = txtCuentaCxPGasto.Text;
                        blDescripcionCuentaCxPGasto.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaCxPProveedores_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaCxPProveedores != txtCuentaCxPProveedores.Text) {
                    if (LibString.Len(txtCuentaCxPProveedores.Text) == 0) {
                        txtCuentaCxPProveedores.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaCxPProveedores.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaCxPProveedores.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaCxPProveedores = txtCuentaCxPProveedores.Text;
                        lblDescripcionCuentaCxPProveedores.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaRetencionImpuestoMunicipal_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCuentaRetencionImpuestoMunicipal.Text) == 0) {
                    txtCuentaRetencionImpuestoMunicipal.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaRetencionImpuestoMunicipal.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCuentaRetencionImpuestoMunicipal.Text = insParse.GetString(0, "Codigo", "");
                    lblDescripcionlblCuentaRetencionImpuestoMunicipal.Content  = insParse.GetString(0, "Descripcion", "");
                    CopiarReglas();
                } else {
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCobranzaTipoComprobante_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCobranzaTipoComprobante.Text) == 0) {
                    txtCobranzaTipoComprobante.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "CodigoDelTipo=" + txtCobranzaTipoComprobante.Text + LibText.ColumnSeparator() ;
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseTipoDeComprobante (null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCobranzaTipoComprobante.Text = insParse.GetString(0, "CodigoDelTipo", "");
                } else {
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbTipoContabilizacionCobranza_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoContabilizacionCobranza.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbContabIndividualCobranza_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabIndividualCobranza.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbContabPorLoteCobranza_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabPorLoteCobranza.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaCobranzaCobradoEnEfectivo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaCobranzaCobradoEnEfectivo != txtCuentaCobranzaCobradoEnEfectivo.Text) {
                    if (LibString.Len(txtCuentaCobranzaCobradoEnEfectivo.Text) == 0) {
                        txtCuentaCobranzaCobradoEnEfectivo.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaCobranzaCobradoEnEfectivo.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaCobranzaCobradoEnEfectivo.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaCobranzaCobradoEnEfectivo = txtCuentaCobranzaCobradoEnEfectivo.Text;
                        lblDescripcionCuentaCobranzaCobradoEnEfectivo.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaCobranzaCobradoEnCheque_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaCobranzaCobradoEnCheque != txtCuentaCobranzaCobradoEnCheque.Text) {
                    if (LibString.Len(txtCuentaCobranzaCobradoEnCheque.Text) == 0) {
                        txtCuentaCobranzaCobradoEnCheque.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaCobranzaCobradoEnCheque.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaCobranzaCobradoEnCheque.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaCobranzaCobradoEnCheque = txtCuentaCobranzaCobradoEnCheque.Text;
                        lblDescripcionCuentaCobranzaCobradoEnCheque.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaCobranzaCobradoEnTarjeta_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaCobranzaCobradoEnTarjeta != txtCuentaCobranzaCobradoEnTarjeta.Text) {
                    if (LibString.Len(txtCuentaCobranzaCobradoEnTarjeta.Text) == 0) {
                        txtCuentaCobranzaCobradoEnTarjeta.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaCobranzaCobradoEnTarjeta.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                   
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaCobranzaCobradoEnTarjeta.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaCobranzaCobradoEnTarjeta = txtCuentaCobranzaCobradoEnTarjeta.Text;
                        lblDescripcionCuentaCobranzaCobradoEnTarjeta.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtcuentaCobranzaRetencionISLR_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.cuentaCobranzaRetencionISLR != txtcuentaCobranzaRetencionISLR.Text) {
                    if (LibString.Len(txtcuentaCobranzaRetencionISLR.Text) == 0) {
                        txtcuentaCobranzaRetencionISLR.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtcuentaCobranzaRetencionISLR.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                   
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtcuentaCobranzaRetencionISLR.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.cuentaCobranzaRetencionISLR = txtcuentaCobranzaRetencionISLR.Text;
                        lblDescripcioncuentaCobranzaRetencionISLR.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtcuentaCobranzaRetencionIVA_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.cuentaCobranzaRetencionIVA != txtcuentaCobranzaRetencionIVA.Text) {
                    if (LibString.Len(txtcuentaCobranzaRetencionIVA.Text) == 0) {
                        txtcuentaCobranzaRetencionIVA.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtcuentaCobranzaRetencionIVA.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtcuentaCobranzaRetencionIVA.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.cuentaCobranzaRetencionIVA = txtcuentaCobranzaRetencionIVA.Text;
                        lblDescripcioncuentaCobranzaRetencionIVA.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaCobranzaOtros_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaCobranzaOtros != txtCuentaCobranzaOtros.Text) {
                    if (LibString.Len(txtCuentaCobranzaOtros.Text) == 0) {
                        txtCuentaCobranzaOtros.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaCobranzaOtros.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaCobranzaOtros.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaCobranzaOtros = txtCuentaCobranzaOtros.Text;
                        lblDescripcionCuentaCobranzaOtros.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaCobranzaCxCClientes_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaCobranzaCxCClientes != txtCuentaCobranzaCxCClientes.Text) {
                    if (LibString.Len(txtCuentaCobranzaCxCClientes.Text) == 0) {
                        txtCuentaCobranzaCxCClientes.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaCobranzaCxCClientes.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                 
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaCobranzaCxCClientes.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaCobranzaCxCClientes = txtCuentaCobranzaCxCClientes.Text;
                        lblDescripcionCuentaCobranzaCxCClientes.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaCobranzaCobradoAnticipo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaCobranzaCobradoAnticipo != txtCuentaCobranzaCobradoAnticipo.Text) {
                    if (LibString.Len(txtCuentaCobranzaCobradoAnticipo.Text) == 0) {
                        txtCuentaCobranzaCobradoAnticipo.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaCobranzaCobradoAnticipo.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                   
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaCobranzaCobradoAnticipo.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaCobranzaCobradoAnticipo = txtCuentaCobranzaCobradoAnticipo.Text;
                        lblDescripcionCuentaCobranzaCobradoAnticipo.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtPagoTipoComprobante_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtPagoTipoComprobante.Text) == 0) {
                    txtPagoTipoComprobante.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "CodigoDelTipo=" + txtPagoTipoComprobante.Text + LibText.ColumnSeparator() ;
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseTipoDeComprobante(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtPagoTipoComprobante.Text = insParse.GetString(0, "CodigoDelTipo", "");
                } else {
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbTipoContabilizacionPagos_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoContabilizacionPagos.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbContabIndividualPagos_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabIndividualPagos.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbContabPorLotePagos_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabPorLotePagos.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaPagosCxPProveedores_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaPagosCxPProveedores != txtCuentaPagosCxPProveedores.Text) {
                    if (LibString.Len(txtCuentaPagosCxPProveedores.Text) == 0) {
                        txtCuentaPagosCxPProveedores.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo( txtCuentaPagosCxPProveedores.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaPagosCxPProveedores.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaPagosCxPProveedores = txtCuentaPagosCxPProveedores.Text;
                        lblDescripcionCuentaPagosCxPProveedores.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }


        void txtCuentaPagosRetencionISLR_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaPagosRetencionISLR != txtCuentaPagosRetencionISLR.Text) {
                    if (LibString.Len(txtCuentaPagosRetencionISLR.Text) == 0) {
                        txtCuentaPagosRetencionISLR.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" +AjustaCodigo( txtCuentaPagosRetencionISLR.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                   
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaPagosRetencionISLR.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaPagosRetencionISLR = txtCuentaPagosRetencionISLR.Text;
                        lblDescripcionCuentaPagosRetencionISLR.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaPagosOtros_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaPagosOtros != txtCuentaPagosOtros.Text) {
                    if (LibString.Len(txtCuentaPagosOtros.Text) == 0) {
                        txtCuentaPagosOtros.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" +AjustaCodigo( txtCuentaPagosOtros.Text )+ LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                     
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaPagosOtros.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaPagosOtros = txtCuentaPagosOtros.Text;
                        lblDescripcionCuentaPagosOtros.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaPagosBanco_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaPagosBanco != txtCuentaPagosBanco.Text) {
                    if (LibString.Len(txtCuentaPagosBanco.Text) == 0) {
                        txtCuentaPagosBanco.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" +AjustaCodigo( txtCuentaPagosBanco.Text )+ LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaPagosBanco.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaPagosBanco = txtCuentaPagosBanco.Text;
                        lblDescripcionCuentaPagosBanco.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaPagosPagadoAnticipo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaPagosPagadoAnticipo != txtCuentaPagosPagadoAnticipo.Text) {
                    if (LibString.Len(txtCuentaPagosPagadoAnticipo.Text) == 0) {
                        txtCuentaPagosPagadoAnticipo.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaPagosPagadoAnticipo.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaPagosPagadoAnticipo.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaPagosPagadoAnticipo = txtCuentaPagosPagadoAnticipo.Text;
                        lblDescripcionCuentaPagosPagadoAnticipo.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbTipoContabilizacionFacturacion_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoContabilizacionFacturacion.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbContabIndividualFacturacion_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabIndividualFacturacion.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbContabPorLoteFacturacion_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabPorLoteFacturacion.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaFacturacionCxCClientes_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaFacturacionCxCClientes != txtCuentaFacturacionCxCClientes.Text) {
                    if (LibString.Len(txtCuentaFacturacionCxCClientes.Text) == 0) {
                        txtCuentaFacturacionCxCClientes.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaFacturacionCxCClientes.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaFacturacionCxCClientes.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaFacturacionCxCClientes = txtCuentaFacturacionCxCClientes.Text;
                        lblDescripcionCuentaFacturacionCxCClientes.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaFacturacionMontoTotalFactura_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaFacturacionMontoTotalFactura != txtCuentaFacturacionMontoTotalFactura.Text) {
                    if (LibString.Len(txtCuentaFacturacionMontoTotalFactura.Text) == 0) {
                        txtCuentaFacturacionMontoTotalFactura.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaFacturacionMontoTotalFactura.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                   
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaFacturacionMontoTotalFactura.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaFacturacionMontoTotalFactura = txtCuentaFacturacionMontoTotalFactura.Text;
                        lblDescripcionCuentaFacturacionMontoTotalFactura.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaFacturacionCargos_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaFacturacionCargos != txtCuentaFacturacionCargos.Text) {
                    if (LibString.Len(txtCuentaFacturacionCargos.Text) == 0) {
                        txtCuentaFacturacionCargos.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaFacturacionCargos.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                     
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaFacturacionCargos.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaFacturacionCargos = txtCuentaFacturacionCargos.Text;
                        lblDescripcionCuentaFacturacionCargos.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaFacturacionDescuentos_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaFacturacionDescuentos != txtCuentaFacturacionDescuentos.Text) {
                    if (LibString.Len(txtCuentaFacturacionDescuentos.Text) == 0) {
                        txtCuentaFacturacionDescuentos.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" +AjustaCodigo( txtCuentaFacturacionDescuentos.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;

                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaFacturacionDescuentos.Text = insParse.GetString(0, "Codigo", "");
                        _CurrentInstance.CuentaFacturacionDescuentos = txtCuentaFacturacionDescuentos.Text;
                        lblDescripcionCuentaFacturacionDescuentos.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtFacturaTipoComprobante_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtFacturaTipoComprobante.Text) == 0) {
                    txtFacturaTipoComprobante.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "CodigoDelTipo=" + txtFacturaTipoComprobante.Text + LibText.ColumnSeparator() ;
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseTipoDeComprobante (null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtFacturaTipoComprobante.Text = insParse.GetString(0, "CodigoDelTipo", "");
                } else {
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbTipoContabilizacionRDVtas_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoContabilizacionRDVtas.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbContabIndividualRDVtas_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabIndividualRDVtas.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbContabPorLoteRDVtas_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabPorLoteRDVtas.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaRDVtasCaja_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaRDVtasCaja != txtCuentaRDVtasCaja.Text) {
                    if (LibString.Len(txtCuentaRDVtasCaja.Text) == 0) {
                        txtCuentaRDVtasCaja.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaRDVtasCaja.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                   
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaRDVtasCaja.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaRDVtasCaja.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                        _CurrentInstance.CuentaRDVtasCaja = txtCuentaRDVtasCaja.Text;
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaRDVtasMontoTotal_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaRDVtasMontoTotal != txtCuentaRDVtasMontoTotal.Text) {
                    if (LibString.Len(txtCuentaRDVtasMontoTotal.Text) == 0) {
                        txtCuentaRDVtasMontoTotal.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaRDVtasMontoTotal.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaRDVtasMontoTotal.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaRDVtasMontoTotal.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                        _CurrentInstance.CuentaRDVtasMontoTotal = txtCuentaRDVtasMontoTotal.Text;
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtMovimientoBancarioTipoComprobante_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtMovimientoBancarioTipoComprobante.Text) == 0) {
                    txtMovimientoBancarioTipoComprobante.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "CodigoDelTipo=" + txtMovimientoBancarioTipoComprobante.Text + LibText.ColumnSeparator() ;
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseTipoDeComprobante (null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtMovimientoBancarioTipoComprobante.Text = insParse.GetString(0, "CodigoDelTipo", "");
                } else {
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbTipoContabilizacionMovBancario_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoContabilizacionMovBancario.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbContabIndividualMovBancario_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabIndividualMovBancario.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbContabPorLoteMovBancario_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabPorLoteMovBancario.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaMovBancarioGasto_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaMovBancarioGasto != txtCuentaMovBancarioGasto.Text) {
                    if (LibString.Len(txtCuentaMovBancarioGasto.Text) == 0) {
                        txtCuentaMovBancarioGasto.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaMovBancarioGasto.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaMovBancarioGasto.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaMovBancarioGasto.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                        _CurrentInstance.CuentaMovBancarioGasto = txtCuentaMovBancarioGasto.Text;
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaMovBancarioBancosHaber_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaMovBancarioBancosHaber != txtCuentaMovBancarioBancosHaber.Text) {
                    if (LibString.Len(txtCuentaMovBancarioBancosHaber.Text) == 0) {
                        txtCuentaMovBancarioBancosHaber.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaMovBancarioBancosHaber.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaMovBancarioBancosHaber.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaMovBancarioBancosHaber.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                        _CurrentInstance.CuentaMovBancarioBancosHaber = txtCuentaMovBancarioBancosHaber.Text;
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaMovBancarioBancosDebe_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaMovBancarioBancosDebe != txtCuentaMovBancarioBancosDebe.Text) {
                    if (LibString.Len(txtCuentaMovBancarioBancosDebe.Text) == 0) {
                        txtCuentaMovBancarioBancosDebe.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaMovBancarioBancosDebe.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaMovBancarioBancosDebe.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaMovBancarioBancosDebe.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                        _CurrentInstance.CuentaMovBancarioBancosDebe = txtCuentaMovBancarioBancosDebe.Text;
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaMovBancarioIngresos_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaMovBancarioIngresos != txtCuentaMovBancarioIngresos.Text) {
                    if (LibString.Len(txtCuentaMovBancarioIngresos.Text) == 0) {
                        txtCuentaMovBancarioIngresos.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaMovBancarioIngresos.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                   
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaMovBancarioIngresos.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaMovBancarioIngresos.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                        _CurrentInstance.CuentaMovBancarioIngresos = txtCuentaMovBancarioIngresos.Text;
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaDebitoBancarioGasto_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaDebitoBancarioGasto != txtCuentaDebitoBancarioGasto.Text) {
                    if (LibString.Len(txtCuentaDebitoBancarioGasto.Text) == 0) {
                        txtCuentaDebitoBancarioGasto.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaDebitoBancarioGasto.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                   
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaDebitoBancarioGasto.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaDebitoBancarioGasto.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                        _CurrentInstance.CuentaDebitoBancarioGasto = txtCuentaDebitoBancarioGasto.Text;
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaDebitoBancarioBancos_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaDebitoBancarioBancos != txtCuentaDebitoBancarioBancos.Text) {
                    if (LibString.Len(txtCuentaDebitoBancarioBancos.Text) == 0) {
                        txtCuentaDebitoBancarioBancos.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaDebitoBancarioBancos.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                 
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaDebitoBancarioBancos.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaDebitoBancarioBancos.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                        _CurrentInstance.CuentaDebitoBancarioBancos = txtCuentaDebitoBancarioBancos.Text;
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaCreditoBancarioGasto_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaCreditoBancarioGasto != txtCuentaCreditoBancarioGasto.Text) {
                    if (LibString.Len(txtCuentaCreditoBancarioGasto.Text) == 0) {
                        txtCuentaCreditoBancarioGasto.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaCreditoBancarioGasto.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                   
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaCreditoBancarioGasto.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaCreditoBancarioGasto.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                        _CurrentInstance.CuentaCreditoBancarioGasto = txtCuentaCreditoBancarioGasto.Text;
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaCreditoBancarioBancos_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaCreditoBancarioBancos != txtCuentaCreditoBancarioBancos.Text) {
                    if (LibString.Len(txtCuentaCreditoBancarioBancos.Text) == 0) {
                        txtCuentaCreditoBancarioBancos.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaCreditoBancarioBancos.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                   
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaCreditoBancarioBancos.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaCreditoBancarioBancos.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                        _CurrentInstance.CuentaCreditoBancarioBancos = txtCuentaCreditoBancarioBancos.Text;
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtAnticipoTipoComprobante_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtAnticipoTipoComprobante.Text) == 0) {
                    txtAnticipoTipoComprobante.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "CodigoDelTipo=" + txtAnticipoTipoComprobante.Text + LibText.ColumnSeparator() ;
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseTipoDeComprobante (null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtAnticipoTipoComprobante.Text = insParse.GetString(0, "CodigoDelTipo", "");
                } else {
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbTipoContabilizacionAnticipo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoContabilizacionAnticipo.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbContabIndividualAnticipo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabIndividualAnticipo.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbContabPorLoteAnticipo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabPorLoteAnticipo.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaAnticipoCaja_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaAnticipoCaja != txtCuentaAnticipoCaja.Text) {
                    if (LibString.Len(txtCuentaAnticipoCaja.Text) == 0) {
                        txtCuentaAnticipoCaja.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaAnticipoCaja.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                 
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaAnticipoCaja.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaAnticipoCaja.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                        _CurrentInstance.CuentaAnticipoCaja = txtCuentaAnticipoCaja.Text;
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaAnticipoCobrado_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaAnticipoCobrado != txtCuentaAnticipoCobrado.Text) {
                    if (LibString.Len(txtCuentaAnticipoCobrado.Text) == 0) {
                        txtCuentaAnticipoCobrado.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaAnticipoCobrado.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaAnticipoCobrado.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaAnticipoCobrado.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                        _CurrentInstance.CuentaAnticipoCobrado = txtCuentaAnticipoCobrado.Text;
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaAnticipoOtrosIngresos_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaAnticipoOtrosIngresos != txtCuentaAnticipoOtrosIngresos.Text) {
                    if (LibString.Len(txtCuentaAnticipoOtrosIngresos.Text) == 0) {
                        txtCuentaAnticipoOtrosIngresos.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaAnticipoOtrosIngresos.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                 
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaAnticipoOtrosIngresos.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaAnticipoOtrosIngresos.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                        _CurrentInstance.CuentaAnticipoOtrosIngresos = txtCuentaAnticipoOtrosIngresos.Text;
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaAnticipoPagado_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaAnticipoPagado != txtCuentaAnticipoPagado.Text) {
                    if (LibString.Len(txtCuentaAnticipoPagado.Text) == 0) {
                        txtCuentaAnticipoPagado.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaAnticipoPagado.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                  
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaAnticipoPagado.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaAnticipoPagado.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                        _CurrentInstance.CuentaAnticipoPagado = txtCuentaAnticipoPagado.Text;
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaAnticipoBanco_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaAnticipoBanco != txtCuentaAnticipoBanco.Text) {
                    if (LibString.Len(txtCuentaAnticipoBanco.Text) == 0) {
                        txtCuentaAnticipoBanco.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaAnticipoBanco.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                  
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaAnticipoBanco.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaAnticipoBanco.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaAnticipoOtrosEgresos_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaAnticipoOtrosEgresos != txtCuentaAnticipoOtrosEgresos.Text) {
                    if (LibString.Len(txtCuentaAnticipoOtrosEgresos.Text) == 0) {
                        txtCuentaAnticipoOtrosEgresos.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaAnticipoOtrosEgresos.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                 
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaAnticipoOtrosEgresos.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaAnticipoOtrosEgresos.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                        _CurrentInstance.CuentaAnticipoOtrosEgresos = txtCuentaAnticipoOtrosEgresos.Text;
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaCostoDeVenta_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaCostoDeVenta != txtCuentaCostoDeVenta.Text) {
                    if (LibString.Len(txtCuentaCostoDeVenta.Text) == 0) {
                        txtCuentaCostoDeVenta.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaCostoDeVenta.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);

                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos, _GetCierreDelEjercicio, ((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaCostoDeVenta.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaCostoDeVenta.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                        _CurrentInstance.CuentaCostoDeVenta = txtCuentaCostoDeVenta.Text;

                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaInventario_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaInventario != txtCuentaInventario.Text) {

                    if (LibString.Len(txtCuentaInventario.Text) == 0) {
                        txtCuentaInventario.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                        
                    vParamsInitializationList = "codigo=" +AjustaCodigo( txtCuentaInventario.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaInventario.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaInventario.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                        _CurrentInstance.CuentaInventario = txtCuentaInventario.Text;


                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }
        void cmbTipoContabilizacionInventario_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoContabilizacionInventario.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCtaDePagosSueldos_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCtaDePagosSueldos.Text) == 0) {
                    txtCtaDePagosSueldos.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "codigo=" + AjustaCodigo(txtCtaDePagosSueldos.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCtaDePagosSueldos.Text = insParse.GetString(0, "Codigo", "");
                    lblDescripcionCtaDePagosSueldos.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();

                } else {
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCtaDePagosSueldosBanco_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCtaDePagosSueldosBanco.Text) == 0) {
                    txtCtaDePagosSueldosBanco.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "codigo=" + AjustaCodigo(txtCtaDePagosSueldosBanco.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCtaDePagosSueldosBanco.Text = insParse.GetString(0, "Codigo", "");
                    lblDescripcionCtaDePagosSueldosBanco.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();


                } else {
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbContabIndividualPagosSueldos_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabIndividualPagosSueldos.ValidateTextInCombo();

            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtPagosSueldosTipoComprobante_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtPagosSueldosTipoComprobante.Text) == 0) {
                    txtPagosSueldosTipoComprobante.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "CodigoDelTipo=" + txtPagosSueldosTipoComprobante.Text + LibText.ColumnSeparator() ;
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseTipoDeComprobante (null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtPagosSueldosTipoComprobante.Text = insParse.GetString(0, "CodigoDelTipo", "");
                } else {
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbTipoContabilizacionDePagosSueldos_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoContabilizacionDePagosSueldos.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtNumero_GotFocus(object sender, RoutedEventArgs e) {
            try {
                if (Action == eAccionSR.Insertar) {
                    //   txtNumero.Text = LibConvert.ToStr(CurrentModel.NextSequential("Numero"));
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void RealizaLosCalculos() {
            if (Action != eAccionSR.Consultar && Action != eAccionSR.Eliminar) {
                //throw new NotImplementedException("Debe sobreescribir el metodo RealizaLosCalculos para su caso especifico. Si no lo requiere no lo invoque.");
            }
        }
        #endregion //Metodos Generados
        private void CopiarReglas() {
            lblCopiaCuentaIVAHaberCxc.Content = txtCuentaIva1Credito.Text;
            lblDescripcionCopiaCuentaIVAHabercxc.Content = lblDescripcionCuentaIva1Credito.Content;

            lblCopiaCuentaIVAHaberResumen.Content = txtCuentaIva1Credito.Text;
            lblDescripcionCopiaCuentaIVAHaberResumen.Content = lblDescripcionCuentaIva1Credito.Content;

            lblCopiaCuentaIVAHaberFactura.Content = txtCuentaIva1Credito.Text;
            lblDescripcionCopiaCuentaIVAHaberFactura.Content = lblCuentaIva1Credito.Content;

            lblCopiaCuentaRetencionIVA.Content = txtCuentaRetencionIva.Text;
            lblDescripcionCopiaCuentaRetencionIVA.Content = lblDescripcionCuentaRetencionIva.Content;

            lblCopiaCuentaRetencionIVAPago.Content = txtCuentaRetencionIva.Text;
            lblDescripcionCopiaCuentaRetencionIVAPago.Content = lblDescripcionCuentaRetencionIva.Content;


            lblCopiaCuentaIVADebeCxp.Content = txtCuentaIva1Debito.Text;
            lblDescripcionCopiaCuentaIVADebecxp.Content = lblDescripcionCuentaIva1Debito.Content;
            lblCopiaCuentaCajaChicaIva.Content = txtCuentaIva1Debito.Text;
            lblDescripcionCuentaCajaChicaIva.Content = lblDescripcionCuentaIva1Debito.Content;

        }
        string DescripcionCuenta(string valCodigo) {

            string vResult = "";
            string vParamsInitializationList;
            string vParamsFixedList = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            vParamsInitializationList = "Codigo=" + valCodigo + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
            vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
            vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
            XmlDocument XmlProperties = new XmlDocument();
            if (valCodigo != "") {
                if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos,_GetCierreDelEjercicio,((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    vResult = insParse.GetString(0, "Descripcion", "");
                }
            }

            return vResult;
        }
        internal void SetDescripcion() {
            lblDescripcionDiferenciaEnCambioyCalculo.Content = DescripcionCuenta(txtDiferenciaEnCambioyCalculo.Text);
            lblDescripcionCuentaIva1Credito.Content = DescripcionCuenta(txtCuentaIva1Credito.Text);
            lblDescripcionCuentaIva1Debito.Content = DescripcionCuenta(txtCuentaIva1Debito.Text);
            lblDescripcionCuentaRetencionIva.Content = DescripcionCuenta(txtCuentaRetencionIva.Text);
            lblDescripcionCuentaCxCClientes.Content = DescripcionCuenta(txtCuentaCxCClientes.Text);
            lblDescripcionCuentaCxCIngresos.Content = DescripcionCuenta(txtCuentaCxCIngresos.Text);
            blDescripcionCuentaCxPGasto.Content = DescripcionCuenta(txtCuentaCxPGasto.Text);
            lblDescripcionCuentaCxPProveedores.Content = DescripcionCuenta(txtCuentaCxPProveedores.Text);
            lblDescripcionCuentaCobranzaCobradoEnEfectivo.Content = DescripcionCuenta(txtCuentaCobranzaCobradoEnEfectivo.Text);
            lblDescripcionCuentaCobranzaCobradoEnCheque.Content = DescripcionCuenta(txtCuentaCobranzaCobradoEnCheque.Text);
            lblDescripcionCuentaCobranzaCobradoEnTarjeta.Content = DescripcionCuenta(txtCuentaCobranzaCobradoEnTarjeta.Text);
            lblDescripcioncuentaCobranzaRetencionISLR.Content = DescripcionCuenta(txtcuentaCobranzaRetencionISLR.Text);
            lblDescripcioncuentaCobranzaRetencionIVA.Content = DescripcionCuenta(txtcuentaCobranzaRetencionIVA.Text);
            lblDescripcionCuentaCobranzaOtros.Content = DescripcionCuenta(txtCuentaCobranzaOtros.Text);
            lblDescripcionCuentaCobranzaCxCClientes.Content = DescripcionCuenta(txtCuentaCobranzaCxCClientes.Text);
            lblDescripcionCuentaCobranzaCobradoAnticipo.Content = DescripcionCuenta(txtCuentaCobranzaCobradoAnticipo.Text);
            lblDescripcionCuentaPagosCxPProveedores.Content = DescripcionCuenta(txtCuentaPagosCxPProveedores.Text);
            lblDescripcionCuentaPagosRetencionISLR.Content = DescripcionCuenta(txtCuentaPagosRetencionISLR.Text);
            lblDescripcionCuentaPagosOtros.Content = DescripcionCuenta(txtCuentaPagosOtros.Text);
            lblDescripcionCuentaPagosBanco.Content = DescripcionCuenta(txtCuentaPagosBanco.Text);
            lblDescripcionCuentaPagosPagadoAnticipo.Content = DescripcionCuenta(txtCuentaPagosPagadoAnticipo.Text);
            lblDescripcionCuentaFacturacionCxCClientes.Content = DescripcionCuenta(txtCuentaFacturacionCxCClientes.Text);
            lblDescripcionCuentaFacturacionMontoTotalFactura.Content = DescripcionCuenta(txtCuentaFacturacionMontoTotalFactura.Text);
            lblDescripcionCuentaFacturacionCargos.Content = DescripcionCuenta(txtCuentaFacturacionCargos.Text);
            lblDescripcionCuentaFacturacionDescuentos.Content = DescripcionCuenta(txtCuentaFacturacionDescuentos.Text);
            lblDescripcionCuentaRDVtasCaja.Content = DescripcionCuenta(txtCuentaRDVtasCaja.Text);
            lblDescripcionCuentaRDVtasMontoTotal.Content = DescripcionCuenta(txtCuentaRDVtasMontoTotal.Text);
            lblDescripcionCuentaMovBancarioGasto.Content = DescripcionCuenta(txtCuentaMovBancarioGasto.Text);
            lblDescripcionCuentaMovBancarioBancosHaber.Content = DescripcionCuenta(txtCuentaMovBancarioBancosHaber.Text);
            lblDescripcionCuentaMovBancarioBancosDebe.Content = DescripcionCuenta(txtCuentaMovBancarioBancosDebe.Text);
            lblDescripcionCuentaMovBancarioIngresos.Content = DescripcionCuenta(txtCuentaMovBancarioIngresos.Text);
            lblDescripcionCuentaDebitoBancarioGasto.Content = DescripcionCuenta(txtCuentaDebitoBancarioGasto.Text);
            lblDescripcionCuentaDebitoBancarioBancos.Content = DescripcionCuenta(txtCuentaDebitoBancarioBancos.Text);
            lblDescripcionCuentaCreditoBancarioGasto.Content = DescripcionCuenta(txtCuentaCreditoBancarioGasto.Text);
            lblDescripcionCuentaCreditoBancarioBancos.Content = DescripcionCuenta(txtCuentaCreditoBancarioBancos.Text);
            lblDescripcionCuentaAnticipoCaja.Content = DescripcionCuenta(txtCuentaAnticipoCaja.Text);
            lblDescripcionCuentaAnticipoCobrado.Content = DescripcionCuenta(txtCuentaAnticipoCobrado.Text);
            lblDescripcionCuentaAnticipoOtrosIngresos.Content = DescripcionCuenta(txtCuentaAnticipoOtrosIngresos.Text);
            lblDescripcionCuentaAnticipoPagado.Content = DescripcionCuenta(txtCuentaAnticipoPagado.Text);
            lblDescripcionCuentaAnticipoBanco.Content = DescripcionCuenta(txtCuentaAnticipoBanco.Text);
            lblDescripcionCuentaAnticipoOtrosEgresos.Content = DescripcionCuenta(txtCuentaAnticipoOtrosEgresos.Text);
            lblDescripcionCuentaCostoDeVenta.Content = DescripcionCuenta(txtCuentaCostoDeVenta.Text);
            lblDescripcionCuentaInventario.Content = DescripcionCuenta(txtCuentaInventario.Text);
            lblDescripcionCtaDePagosSueldos.Content = DescripcionCuenta(txtCtaDePagosSueldos.Text);
            lblDescripcionCtaDePagosSueldosBanco.Content = DescripcionCuenta(txtCtaDePagosSueldosBanco.Text);
            lblDescripcionlblCuentaRetencionImpuestoMunicipal.Content = DescripcionCuenta(txtCuentaRetencionImpuestoMunicipal.Text);
            lblDescripcionCuentaCajaChicaGasto.Content = DescripcionCuenta(txtCuentaCajaChicaGasto.Text);
            lblDescripcionCuentaBancoCajaChicaHaber.Content = DescripcionCuenta(txtCuentaCajaChicaBancoHaber.Text);
            lblDescripcionCuentaCajaChicaBancoDebe.Content = DescripcionCuenta(txtCuentaCajaChicaBancoDebe.Text);
            lblDescripcionCuentaCajaChicaBanco.Content = DescripcionCuenta(txtCuentaCajaChicaBanco.Text);
            lblDescripcionCuentaRendicionesGasto.Content = DescripcionCuenta(txtCuentaRendicionesGasto.Text);
            lblDescripcionCuentaRendicionesIva.Content = DescripcionCuenta(txtCuentaIva1Debito.Text);
            lblDescripcionCuentaRendicionesBanco.Content = DescripcionCuenta(txtCuentaRendicionesBanco.Text);
            lblDescripcionCuentaRendicionesAnticipos.Content = DescripcionCuenta(txtCuentaRendicionesAnticipos.Text);
        }
        private System.Windows.Visibility MuestraTipoComprobante() {
            System.Windows.Visibility vResult;
            if (_MuestraTipoComprobante) {
                vResult = System.Windows.Visibility.Visible;
            } else {
                vResult = System.Windows.Visibility.Hidden;
            }

            return vResult;
        }
        private void DesactivaLosTipoComprobanteQueTenganRecordsAsociados() {
            if (_Action == eAccionSR.Modificar) {
                if (_DesactivaFacturaTipoComprobante) {
                    txtFacturaTipoComprobante.IsEnabled = false;
                }
                if (_DesactivaCxCTipoComprobante) {
                    txtFacturaTipoComprobante.IsEnabled = false;
                }
                if (_DesactivaCxPTipoComprobante) {
                    txtCxPTipoComprobante.IsEnabled = false;
                }

                if (_DesactivaCobranzaTipoComprobante) {
                    txtCobranzaTipoComprobante.IsEnabled = false;
                }

                if (_DesactivaPagoTipoComprobante) {
                    txtPagoTipoComprobante.IsEnabled = false;
                }

                if (_DesactivaBancarioTipoComprobante) {
                    txtMovimientoBancarioTipoComprobante.IsEnabled = false;
                }
                if (_DesactivaAnticipoTipoComprobante) {
                    txtAnticipoTipoComprobante.IsEnabled = false;
                }
                if (_DesactivaCajaChicaTipoComprobante) {
                    txtSiglasTipoComprobanteCajaChica.IsEnabled = false;
                }
            }
        }
        private void DesactivaTodosLosComboBox() {
            cmbContabPorLoteCobranza.IsEnabled = false;
            cmbContabPorLoteCxC.IsEnabled = false;
            cmbContabPorLoteCxP.IsEnabled = false;
            cmbContabPorLoteFacturacion.IsEnabled = false;
            cmbContabPorLoteMovBancario.IsEnabled = false;
            cmbContabPorLotePagos.IsEnabled = false;
            cmbContabPorLoteRDVtas.IsEnabled = false;
            cmbContabPorLoteAnticipo.IsEnabled = false;
            cmbTipoContabilizacionCobranza.IsEnabled = false;
            cmbTipoContabilizacionCxC.IsEnabled = false;
            cmbTipoContabilizacionCxP.IsEnabled = false;
            cmbTipoContabilizacionFacturacion.IsEnabled = false;
            cmbTipoContabilizacionMovBancario.IsEnabled = false;
            cmbTipoContabilizacionPagos.IsEnabled = false;
            cmbTipoContabilizacionRDVtas.IsEnabled = false;
            cmbTipoContabilizacionAnticipo.IsEnabled = false;
        }
        private void sDecideSiMuestraTipoComprobante() {
            txtFacturaTipoComprobante.Visibility = MuestraTipoComprobante();
            txtCxCTipoComprobante.Visibility = MuestraTipoComprobante();
            txtCxPTipoComprobante.Visibility = MuestraTipoComprobante();
            txtCobranzaTipoComprobante.Visibility = MuestraTipoComprobante();
            txtPagoTipoComprobante.Visibility = MuestraTipoComprobante();
            txtMovimientoBancarioTipoComprobante.Visibility = MuestraTipoComprobante();
            txtAnticipoTipoComprobante.Visibility = MuestraTipoComprobante();
            txtInventarioTipoComprobante.Visibility = MuestraTipoComprobante();
            lblFacturaTipoComprobante.Visibility = MuestraTipoComprobante();
            lblCxCTipoComprobante.Visibility = MuestraTipoComprobante();
            lblCxPTipoComprobante.Visibility = MuestraTipoComprobante();
            lblCobranzaTipoComprobante.Visibility = MuestraTipoComprobante();
            lblPagoTipoComprobante.Visibility = MuestraTipoComprobante();
            lblMovimientoBancarioTipoComprobante.Visibility = MuestraTipoComprobante();
            lblAnticipoTipoComprobante.Visibility = MuestraTipoComprobante();
            lblInventarioTipoComprobante.Visibility = MuestraTipoComprobante();
            txtPagosSueldosTipoComprobante.Visibility = MuestraTipoComprobante();
            lblTipoDeComprobantePagoSueldo.Visibility = MuestraTipoComprobante();
            lblSiglasTipoComprobanteCajaChica.Visibility = MuestraTipoComprobante();
            txtSiglasTipoComprobanteCajaChica.Visibility = MuestraTipoComprobante();

        }
        private void ActivaCamposDeContribuyentesEspeciales() {
            if (_PuedoUsarOpcionesDeContribuyenteEspecial) {
                txtCuentaRetencionIva.IsEnabled = true;
                lblDescripcionCuentaRetencionIva.IsEnabled = true;
            } else {
                txtCuentaRetencionIva.IsEnabled = false;
                txtCuentaRetencionIva.Text = "";
                lblDescripcionCuentaRetencionIva.IsEnabled = false;
                lblDescripcionCuentaRetencionIva.Content = "";
                lblCopiaCuentaRetencionIVA.IsEnabled = false;
                lblDescripcionCopiaCuentaRetencionIVA.IsEnabled = false;
                lblCopiaCuentaRetencionIVAPago.IsEnabled = false;
                lblDescripcionCopiaCuentaRetencionIVAPago.IsEnabled = false;
            }
        }

        private void DecideSiMuestraReglasDeResumenDiarioDeVentas() {
            if (_UsarResumenDiarioDeVentas) {
                tbiResumenDiariodeVentas.Visibility = System.Windows.Visibility.Visible;
                txtCuentaRDVtasCaja.IsEnabled = true;
                txtCuentaRDVtasMontoTotal.IsEnabled = true;
                if (_UsarRenglonesEnResumenVtas) {
                    chkContabilizarPorArticuloRDVtas.IsEnabled = true;
                    chkAgruparPorCuentaDeArticuloRDVtas.IsEnabled = true;
                } else {
                    chkContabilizarPorArticuloRDVtas.IsEnabled = false;
                    chkAgruparPorCuentaDeArticuloRDVtas.IsEnabled = false;
                }
            } else {
                tbiResumenDiariodeVentas.Visibility = System.Windows.Visibility.Hidden;
                txtCuentaRDVtasCaja.IsEnabled = false;
                txtCuentaRDVtasMontoTotal.IsEnabled = false;
                chkContabilizarPorArticuloRDVtas.IsEnabled = false;
                chkAgruparPorCuentaDeArticuloRDVtas.IsEnabled = false;
            }

        }
        private void ActivaCamposDeResumenDiarioDeVentas() {
            bool vActivar = _UsarResumenDiarioDeVentas;
            tbiResumenDiariodeVentas.IsEnabled = vActivar;
            chkContabilizarPorArticuloRDVtas.Visibility = System.Windows.Visibility.Hidden;
            chkAgruparPorCuentaDeArticuloRDVtas.Visibility = System.Windows.Visibility.Hidden;
        }


        private void ValidacionDondeContabilizarRetIva() {
            if (_PuedoUsarOpcionesDeContribuyenteEspecial) {
                ValidacionDelComboDeRetencion();
            } else {
                cmbDondeContabilizarRetIva.SelectItem(LibEnumHelper.GetDescription(eDondeEfectuoContabilizacionRetIVA.NoContabilizada));
            }
        }


        private void ValidacionDelComboDeRetencion() {
            if (cmbDondeContabilizarRetIva.SelectedItemToInt() == (int)eDondeEfectuoContabilizacionRetIVA.NoContabilizada) {
                MensajeAlertaRetencionIva("La contabilización de la Retención de I.V.A. debe efectuarse en el registro de la CxP o en el registro del Pago");
                AsignacionDeLugarDeRetencion();
            } else if (!SonIgualesContabilizacionYAplicacionDeRetIVA()) {
                MensajeDeAlerta();
            }
        }
        private void MensajeDeAlerta() {
            StringBuilder vMensaje = new StringBuilder();
            vMensaje.Append("El momento de la Contabilización de la Retención de IVA ");
            vMensaje.Append("(Reglas de Contabilización -> Contabilizar la Retención de I.V.A. en: " + cmbDondeContabilizarRetIva.Text);
            vMensaje.Append("Es diferente al momento en el cual se está efectuando dicha retención");
            if (_RetenerIVAEnCxp) {
                vMensaje.Append("(Parámetros Administrativos -> Efectuar la Retención del IVA en: CXP ");
            } else {
                vMensaje.Append("(Parámetros Administrativos -> Efectuar la Retención del IVA en: Pago ");

            }
            MensajeAlertaRetencionIva(vMensaje.ToString());
        }
        private void AsignacionDeLugarDeRetencion() {
            if (_RetenerIVAEnCxp) {
                cmbDondeContabilizarRetIva.SelectItem(LibEnumHelper.GetDescription(eDondeEfectuoContabilizacionRetIVA.CxP));
            } else {
                cmbDondeContabilizarRetIva.SelectItem(LibEnumHelper.GetDescription(eDondeEfectuoContabilizacionRetIVA.Pago));
            }
        }
        private bool SonIgualesContabilizacionYAplicacionDeRetIVA() {
            bool vResult = false;
            if (_RetenerIVAEnCxp) {
                vResult = (cmbDondeContabilizarRetIva.SelectedItemToInt() == (int)eDondeEfectuoContabilizacionRetIVA.CxP);
            } else {
                vResult = (cmbDondeContabilizarRetIva.SelectedItemToInt() == (int)eDondeEfectuoContabilizacionRetIVA.Pago);
            }
            return vResult;
        }
        private void MostarUOcultarCampos() {
            if (chkContabilizarPorArticulo.IsChecked == true) {
                chkAgruparPorCuentaDeArticulo.IsChecked = true;
                chkAgruparPorCuentaDeArticulo.IsEnabled = true;
                lblLeyendaFactura.Visibility = System.Windows.Visibility.Visible;
            } else {
                chkAgruparPorCuentaDeArticulo.IsChecked = false;
                chkAgruparPorCuentaDeArticulo.IsEnabled = false;
                lblLeyendaFactura.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        private void MensajeAlertaRetencionIva(string valMensaje) {
            LibGalac.Aos.UI.Wpf.LibNotifier.Warning(null, valMensaje);
        }
        private void EnableControlDelFrm() {
            
            LibApiAwp.EnableControl(txtDiferenciaEnCambioyCalculo, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaIva1Credito, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaIva1Debito, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbDondeContabilizarRetIva, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbTipoContabilizacionCxC, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabIndividualCxc, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabPorLoteCxC, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaCxCClientes, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaCxCIngresos, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbTipoContabilizacionCxP, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabIndividualCxP, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabPorLoteCxP, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaCxPGasto, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaCxPProveedores, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbTipoContabilizacionCobranza, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabIndividualCobranza, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabPorLoteCobranza, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaCobranzaCobradoEnEfectivo, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaCobranzaCobradoEnCheque, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaCobranzaCobradoEnTarjeta, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtcuentaCobranzaRetencionISLR, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtcuentaCobranzaRetencionIVA, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaCobranzaOtros, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaCobranzaCxCClientes, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaCobranzaCobradoAnticipo, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbTipoContabilizacionPagos, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabIndividualPagos, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabPorLotePagos, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaPagosCxPProveedores, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaPagosRetencionISLR, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaPagosOtros, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaPagosBanco, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaPagosPagadoAnticipo, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbTipoContabilizacionFacturacion, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabIndividualFacturacion, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabPorLoteFacturacion, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaFacturacionCxCClientes, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaFacturacionMontoTotalFactura, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaFacturacionCargos, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaFacturacionDescuentos, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbTipoContabilizacionRDVtas, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabIndividualRDVtas, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabPorLoteRDVtas, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaRDVtasCaja, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaRDVtasMontoTotal, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbTipoContabilizacionMovBancario, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabIndividualMovBancario, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabPorLoteMovBancario, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaMovBancarioGasto, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaMovBancarioBancosHaber, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaMovBancarioBancosDebe, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaMovBancarioIngresos, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaDebitoBancarioGasto, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaDebitoBancarioBancos, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaCreditoBancarioGasto, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaCreditoBancarioBancos, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbTipoContabilizacionAnticipo, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabIndividualAnticipo, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabPorLoteAnticipo, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaAnticipoCaja, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaAnticipoCobrado, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaAnticipoOtrosIngresos, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaAnticipoPagado, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaAnticipoBanco, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaAnticipoOtrosEgresos, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtFacturaTipoComprobante, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCxCTipoComprobante, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCxPTipoComprobante, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCobranzaTipoComprobante, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtPagoTipoComprobante, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtMovimientoBancarioTipoComprobante, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtAnticipoTipoComprobante, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbTipoContabilizacionInventario, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtInventarioTipoComprobante, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaInventario, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaCostoDeVenta, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaRetencionIva, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabIndividualPagosSueldos, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCtaDePagosSueldos, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCtaDePagosSueldosBanco, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtPagosSueldosTipoComprobante, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaRetencionImpuestoMunicipal, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(chkEditarComprobanteDePagosSueldos, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));     
            LibApiAwp.EnableControl(chkEditarComprobanteAfterInsertCxC, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));     
			LibApiAwp.EnableControl(chkEditarComprobanteAfterInsertCxP, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
			LibApiAwp.EnableControl(chkEditarComprobanteAfterInsertCobranza, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
			LibApiAwp.EnableControl(chkEditarComprobanteAfterInsertPagos, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
			LibApiAwp.EnableControl(chkEditarComprobanteAfterInsertFactura, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
			LibApiAwp.EnableControl(chkEditarComprobanteAfterInsertResDia, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
			LibApiAwp.EnableControl(chkEditarComprobanteAfterInsertMovBan, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
			LibApiAwp.EnableControl(chkEditarComprobanteAfterInsertImpTraBan, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
			LibApiAwp.EnableControl(chkEditarComprobanteAfterInsertAnticipo, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
			LibApiAwp.EnableControl(chkEditarComprobanteAfterInsertInventario, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));			
			 LibApiAwp.EnableControl(txtCuentaCajaChicaGasto, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaCajaChicaBancoHaber, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaCajaChicaBancoDebe, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaCajaChicaBanco, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabIndividualCajaChica, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtSiglasTipoComprobanteCajaChica, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(chkMostrarDesglosadoCajaChica, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));

            LibApiAwp.EnableControl(txtCuentaRendicionesGasto, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaRendicionesIva, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaRendicionesBanco, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtCuentaRendicionesAnticipos, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(cmbContabIndividualRendiciones, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(txtSiglasTipoComprobanteRendiciones, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
            LibApiAwp.EnableControl(chkMostrarDesglosadoRendiciones, (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar));
			
           
        }

        private string AjustaCodigo(string valCodigo) {
            string vResult = "";
            clsReglasDeContabilizacionIpl insReglasDeContabilizacionIpl = new clsReglasDeContabilizacionIpl(((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc);
            vResult = insReglasDeContabilizacionIpl.AjustaLaCuenta(valCodigo, _MaxNumLevels, _MaxNumLevelsAtMatrix, _MinNumLevels, _MaxLength, _UseZeroAtRigth, _Niveles);
            return vResult;
        }        
        void cmbContabIndividualCajaChica_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabIndividualCajaChica.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }
        void txtCuentaCajaChicaGasto_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaCajaChicaGasto != txtCuentaCajaChicaGasto.Text) {
                    if (LibString.Len(txtCuentaCajaChicaGasto.Text) == 0) {
                        txtCuentaCajaChicaGasto.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaCajaChicaGasto.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos, _GetCierreDelEjercicio, ((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaCajaChicaGasto.Text = insParse.GetString(0, "Codigo", "");                                          
                        lblDescripcionCuentaCajaChicaGasto.Content = insParse.GetString(0, "Descripcion", ""); 
                        CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }
        void txtCuentaCajaChicaBancoHaber_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaCajaChicaBancoHaber != txtCuentaCajaChicaBancoHaber.Text) {
                    if (LibString.Len(txtCuentaCajaChicaBancoHaber.Text) == 0) {
                        txtCuentaCajaChicaBancoHaber.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaCajaChicaBancoHaber.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos, _GetCierreDelEjercicio, ((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaCajaChicaBancoHaber.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaBancoCajaChicaHaber.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }
        void txtCuentaCajaChicaBancoDebe_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaCajaChicaBancoDebe != txtCuentaCajaChicaBancoDebe.Text) {
                    if (LibString.Len(txtCuentaCajaChicaBancoDebe.Text) == 0) {
                        txtCuentaCajaChicaBancoDebe.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaCajaChicaBancoDebe.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos, _GetCierreDelEjercicio, ((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaCajaChicaBancoDebe.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaCajaChicaBancoDebe.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }
        void txtCuentaCajaChicaBanco_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaCajaChicaBanco != txtCuentaCajaChicaBanco.Text) {
                    if (LibString.Len(txtCuentaCajaChicaBanco.Text) == 0) {
                        txtCuentaCajaChicaBanco.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaCajaChicaBanco.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos, _GetCierreDelEjercicio, ((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaCajaChicaBanco.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaCajaChicaBanco.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }
        void cmbContabIndividualRendiciones_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbContabIndividualRendiciones.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }
        void txtCuentaRendicionesGasto_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaRendicionesGasto != txtCuentaRendicionesGasto.Text) {
                    if (LibString.Len(txtCuentaRendicionesGasto.Text) == 0) {
                        txtCuentaRendicionesGasto.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaRendicionesGasto.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos, _GetCierreDelEjercicio, ((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaRendicionesGasto.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaRendicionesGasto.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }
        void txtCuentaRendicionesIva_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }
        void txtCuentaRendicionesBanco_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaRendicionesBanco != txtCuentaRendicionesBanco.Text) {
                    if (LibString.Len(txtCuentaRendicionesBanco.Text) == 0) {
                        txtCuentaRendicionesBanco.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaRendicionesBanco.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos, _GetCierreDelEjercicio, ((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaRendicionesBanco.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaRendicionesBanco.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }
        void txtCuentaRendicionesAnticipos_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (_CurrentInstance.CuentaRendicionesAnticipos != txtCuentaRendicionesAnticipos.Text) {
                    if (LibString.Len(txtCuentaRendicionesAnticipos.Text) == 0) {
                        txtCuentaRendicionesAnticipos.Text = "*";
                    }
                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "codigo=" + AjustaCodigo(txtCuentaRendicionesAnticipos.Text) + LibText.ColumnSeparator() + "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    vParamsFixedList = "ConsecutivoPeriodo=" + _ConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues, _EscogerAuxiliar, _UsaAuxiliares, _UsaModuloDeActivoFijo, _ConsecutivoPeriodo, _ListaGrupoDeActivos, _GetCierreDelEjercicio, ((clsReglasDeContabilizacionIpl)CurrentModel).AppMemoryInfo, ((clsReglasDeContabilizacionIpl)CurrentModel).Mfc)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaRendicionesAnticipos.Text = insParse.GetString(0, "Codigo", "");
                        lblDescripcionCuentaRendicionesAnticipos.Content = insParse.GetString(0, "Descripcion", ""); CopiarReglas();
                    } else {
                        e.Cancel = true;
                    }
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }
        void txtSiglasTipoComprobanteCajaChica_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtSiglasTipoComprobanteCajaChica.Text) == 0) {
                    txtSiglasTipoComprobanteCajaChica.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "CodigoDelTipo=" + txtSiglasTipoComprobanteCajaChica.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Contabilizacion.clsReglasDeContabilizacionList.ChooseTipoDeComprobante(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtSiglasTipoComprobanteCajaChica.Text = insParse.GetString(0, "CodigoDelTipo", "");
                } else {
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }
    } //End of class GSReglasDeContabilizacion.xaml

}//End of namespace Galac.Saw.Uil.Contabilizacion

