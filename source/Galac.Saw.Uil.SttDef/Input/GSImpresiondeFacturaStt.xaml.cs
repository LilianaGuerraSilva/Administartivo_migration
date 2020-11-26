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
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Uil.SttDef.Input {
    /// <summary>
    /// Lógica de interacción para GSImpresiondeFacturaStt.xaml
    /// </summary>
    internal partial class GSImpresiondeFacturaStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        ImpresiondeFacturaStt _CurrentInstance;
        
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
        internal ImpresiondeFacturaStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSImpresiondeFacturaStt() {
            InitializeComponent();
            InitializeEvents();
            cmbFormaDeOrdenarDetalleFactura.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eFormaDeOrdenarDetalleFactura)));
            cmbAccionLimiteItemsFactura.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eAccionLimiteItemsFactura)));
            CmbFormato.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeFormatoFecha)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (ImpresiondeFacturaStt)initInstance;

            Title = "Impresion de Factura";
            Action = initAction;
            ExtendedAction = initExtendedAction;
            //LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar });
            Action = ((GSSettValueByCompany)((Grid)((HeaderedContentControl)((StackPanel)((Grid)((ContentPresenter)this.TemplatedParent).Parent).Parent).TemplatedParent).Parent).Parent).Action;
            if (Action == eAccionSR.Insertar) {
                SetFormValuesFromNavigator(true);
            } else {
                SetFormValuesFromNavigator(false);
            }
            
            SetLookAndFeelForCurrentRecord();
            Tool.AllDisabled(gwMain.Children, Action);
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
                bool vSesionEspecialImprimirTipoCobroFactura = clsGlobalValues.AppMemoryInfo.GlobalValuesGetBool("Parametros", "SesionEspecialImprimirTipoCobroFactura");
                if (vSesionEspecialImprimirTipoCobroFactura) {
                    chkImprimirTipoCobroEnFactura.Visibility = System.Windows.Visibility.Visible;
                    lblImprimirTipoCobroEnFactura.Visibility = System.Windows.Visibility.Visible;
                } else {
                    chkImprimirTipoCobroEnFactura.Visibility = System.Windows.Visibility.Hidden;
                    lblImprimirTipoCobroEnFactura.Visibility = System.Windows.Visibility.Hidden;
                }
                LibApiAwp.EnableControl(chkImprimirTipoCobroEnFactura, vSesionEspecialImprimirTipoCobroFactura);
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.NumeroDeCerosALaIzquierda = LibConvert.ToInt(txtNumeroDeCerosALaIzquierda.Value);
            _CurrentInstance.CantidadDeCopiasDeLaFacturaAlImprimir = LibConvert.ToInt(txtCantidadDeCopiasDeLaFacturaAlImprimir.Value);
            _CurrentInstance.UsarDecimalesAlImprimirCantidadAsBool = chkUsarDecimalesAlImprimirCantidad.IsChecked.Value;
            _CurrentInstance.DetalleProdCompFacturaAsBool = chkDetalleProdCompFactura.IsChecked.Value;
            _CurrentInstance.FormaDeOrdenarDetalleFacturaAsEnum = (eFormaDeOrdenarDetalleFactura) cmbFormaDeOrdenarDetalleFactura.SelectedItemToInt();
            _CurrentInstance.ImprimirFacturaConSubtotalesPorLineaDeProductoAsBool = chkImprimirFacturaConSubtotalesPorLineaDeProducto.IsChecked.Value;
            _CurrentInstance.NoImprimirFacturaAsBool = chkNoImprimirFactura.IsChecked.Value;
            _CurrentInstance.ImprimirBorradorAlInsertarFacturaAsBool = chkImprimirBorradorAlInsertarFactura.IsChecked.Value;
            _CurrentInstance.ImprimeDireccionAlFinalDelComprobanteFiscalAsBool = chkImprimeDireccionAlFinalDelComprobanteFiscal.IsChecked.Value;
            _CurrentInstance.ConcatenaLetraEaArticuloExentoAsBool = chkConcatenaLetraEaArticuloExento.IsChecked.Value;
            _CurrentInstance.ImprimirTipoCobroEnFacturaAsBool = chkImprimirTipoCobroEnFactura.IsChecked.Value;
            _CurrentInstance.NumItemImprimirFactura = LibConvert.ToInt(txtNumItemImprimirFactura.Value);
            _CurrentInstance.AccionLimiteItemsFacturaAsEnum = (eAccionLimiteItemsFactura) cmbAccionLimiteItemsFactura.SelectedItemToInt();
            _CurrentInstance.FormatoDeFecha = txtFormatoDeFecha.Text;
            _CurrentInstance.ImprimirAnexoDeSerialAsBool = chkImprimirAnexoDeSerial.IsChecked.Value;
            _CurrentInstance.NombrePlantillaAnexoSeriales = txtNombrePlantillaAnexoSeriales.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            txtNumeroDeCerosALaIzquierda.Value = _CurrentInstance.NumeroDeCerosALaIzquierda;
            txtCantidadDeCopiasDeLaFacturaAlImprimir.Value = _CurrentInstance.CantidadDeCopiasDeLaFacturaAlImprimir;
            chkUsarDecimalesAlImprimirCantidad.IsChecked = _CurrentInstance.UsarDecimalesAlImprimirCantidadAsBool;
            chkDetalleProdCompFactura.IsChecked = _CurrentInstance.DetalleProdCompFacturaAsBool;
            cmbFormaDeOrdenarDetalleFactura.SelectItem(_CurrentInstance.FormaDeOrdenarDetalleFacturaAsEnum);
            chkImprimirFacturaConSubtotalesPorLineaDeProducto.IsChecked = _CurrentInstance.ImprimirFacturaConSubtotalesPorLineaDeProductoAsBool;
            chkNoImprimirFactura.IsChecked = _CurrentInstance.NoImprimirFacturaAsBool;
            chkImprimirBorradorAlInsertarFactura.IsChecked = _CurrentInstance.ImprimirBorradorAlInsertarFacturaAsBool;
            chkImprimeDireccionAlFinalDelComprobanteFiscal.IsChecked = _CurrentInstance.ImprimeDireccionAlFinalDelComprobanteFiscalAsBool;
            chkConcatenaLetraEaArticuloExento.IsChecked = _CurrentInstance.ConcatenaLetraEaArticuloExentoAsBool;
            chkImprimirTipoCobroEnFactura.IsChecked = _CurrentInstance.ImprimirTipoCobroEnFacturaAsBool;
            txtNumItemImprimirFactura.Value = _CurrentInstance.NumItemImprimirFactura;
            cmbAccionLimiteItemsFactura.SelectItem(_CurrentInstance.AccionLimiteItemsFacturaAsEnum);
            txtFormatoDeFecha.Text = _CurrentInstance.FormatoDeFecha;
            chkImprimirAnexoDeSerial.IsChecked = _CurrentInstance.ImprimirAnexoDeSerialAsBool;
            txtNombrePlantillaAnexoSeriales.Text = _CurrentInstance.NombrePlantillaAnexoSeriales;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.cmbFormaDeOrdenarDetalleFactura.Validating += new System.ComponentModel.CancelEventHandler(cmbFormaDeOrdenarDetalleFactura_Validating);
            this.cmbAccionLimiteItemsFactura.Validating += new System.ComponentModel.CancelEventHandler(cmbAccionLimiteItemsFactura_Validating);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.btnNombrePlantilla.Click += new RoutedEventHandler(btnNombrePlantilla_Click);
            this.txtNombrePlantillaAnexoSeriales.Validating += new CancelEventHandler(txtNombrePlantillaAnexoSeriales_Validating);
            this.CmbFormato.Validating += new CancelEventHandler(CmbFormato_Validating);
            this.txtFormatoDeFecha.Validating += new CancelEventHandler(txtFormatoDeFecha_Validating);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
            this.Loaded += new RoutedEventHandler(GSImpresiondeFacturaStt_Loaded);
        }

        void GSImpresiondeFacturaStt_Loaded(object sender, RoutedEventArgs e) {
            LibApiAwp.EnableControl(chkImprimirBorradorAlInsertarFactura, !ObieneEmitirDirectoTabFactura());
        }

        
        void txtFormatoDeFecha_Validating(object sender, CancelEventArgs e) {
            if (!EsFormatoDeFechaValido(txtFormatoDeFecha.Text)) {
                MessageBox.Show("El Formato de Fecha es inválido.");
                CmbFormato.SelectedIndex = 0;
                e.Cancel = true;
            }
        }

        bool EsFormatoDeFechaValido(string valFormatoFecha) {
            string valDia = "dd";
            string valMes = "mm";
            string valAno = "yyyy";
            string[] valArray;
            bool valCadenaValida;
            string valTextoFormatoLimpio = LibString.CleanFromCharsInBothSides(valFormatoFecha, " ");
            valArray = valTextoFormatoLimpio.Select(c => c.ToString()).ToArray();
            if (LibString.Len(valFormatoFecha) == 10) {
                string valTextoTemp = valArray[0] + valArray[1];
                if (LibString.S1IsInS2(valDia, valTextoTemp)) valCadenaValida = true; else valCadenaValida = false;
                valTextoTemp = valArray[3] + valArray[4];
                if (LibString.S1IsInS2(valMes, valTextoTemp)) valCadenaValida = true && valCadenaValida; else valCadenaValida = false && valCadenaValida;
                valTextoTemp = valArray[6] + valArray[7] + valArray[8] + valArray[9];
                if (LibString.S1IsInS2(valAno, valTextoTemp)) valCadenaValida = true && valCadenaValida; else valCadenaValida = false && valCadenaValida;
            } else {
                valCadenaValida = false;
            }
            if (valCadenaValida) {
                if (( LibString.S1IsInS2("/",valArray[2]) || LibString.S1IsInS2(".",valArray[2]) || LibString.S1IsInS2("-",valArray[2]) || LibString.S1IsInS2("_",valArray[2]) ) 
                && ( LibString.S1IsInS2("/",valArray[5]) || LibString.S1IsInS2(".",valArray[5]) || LibString.S1IsInS2("-",valArray[5]) || LibString.S1IsInS2("_",valArray[5]) ))
                    valCadenaValida = true && valCadenaValida;
                else valCadenaValida = false && valCadenaValida;
            }
            return valCadenaValida;
        }

        void CmbFormato_Validating(object sender, CancelEventArgs e) {
            LibApiAwp.EnableControl(txtFormatoDeFecha, (eTipoDeFormatoFecha)CmbFormato.SelectedItem == eTipoDeFormatoFecha.eCSF_CON_OTRO);
            txtFormatoDeFecha.Text = (eTipoDeFormatoFecha)CmbFormato.SelectedItem == eTipoDeFormatoFecha.eCSF_CON_OTRO ? "" : LibEnumHelper.GetDescription((eTipoDeFormatoFecha)CmbFormato.SelectedItem);
        }

        private void OnUnloaded(object sender, RoutedEventArgs e) {
            SetNavigatorValuesFromForm();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            try {
                if (DataContext == null) {
                    return;
                }

                InitializeControl(DataContext, eAccionSR.Modificar, null);

            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }


        void cmbFormaDeOrdenarDetalleFactura_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbFormaDeOrdenarDetalleFactura.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbAccionLimiteItemsFactura_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbAccionLimiteItemsFactura.ValidateTextInCombo();
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

        List<string> ListModeloPlanillas() {
            List<string> vResult = new List<string>();
            clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);
            vResult = insSettValueByCompanyIpl.GetModelosPlanillasList();
            return vResult;
        }

        void txtNombrePlantillaAnexoSeriales_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaAnexoSeriales.Text) == 0) {
                txtNombrePlantillaAnexoSeriales.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaAnexoSeriales.Text)) {
                BuscarRpxAnexosSeriales();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaAnexoSeriales.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaAnexoSeriales.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }

        private void btnNombrePlantilla_Click(object sender, RoutedEventArgs e) {
             try {
                BuscarRpxAnexosSeriales();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }
        
        private void BuscarRpxAnexosSeriales() {
            string paramBusqueda = "rpx de Anexo Seriales (*.rpx)|*Anexo*Seriales*.rpx";
            txtNombrePlantillaAnexoSeriales.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }

        private bool ObieneEmitirDirectoTabFactura() {
            var modulos = ((clsSettValueByCompanyIpl)((GSSettValueByCompany)((Grid)((HeaderedContentControl)((StackPanel)((Grid)((ContentPresenter)this.TemplatedParent).Parent).Parent).TemplatedParent).Parent).Parent).CurrentModel).ModuleList;
            var modfact = modulos[1].Groups[1].Content;
            object Result = (modfact.GetType().GetProperty("EmitirDirectoAsBool").GetValue(modfact, null));
            if (Result == null) { Result = false; }
            return (bool)Result;
        }
    } //End of class GSImpresiondeFacturaStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

