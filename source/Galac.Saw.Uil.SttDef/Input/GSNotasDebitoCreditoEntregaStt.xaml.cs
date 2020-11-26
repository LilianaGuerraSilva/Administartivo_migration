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
    /// Lógica de interacción para GSNotasDebitoCreditoEntregaStt.xaml
    /// </summary>
    internal partial class GSNotasDebitoCreditoEntregaStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        NotasDebitoCreditoEntregaStt _CurrentInstance;
        
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
        internal NotasDebitoCreditoEntregaStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSNotasDebitoCreditoEntregaStt() {
            InitializeComponent();
            InitializeEvents();
            cmbTipoDePrefijoNC.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDePrefijoFactura)));
            cmbTipoDePrefijoND.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDePrefijoFactura)));
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
            _CurrentInstance = (NotasDebitoCreditoEntregaStt)initInstance;

            Title = "Notas Debito Credito Entrega";
            Action = initAction;
            ExtendedAction = initExtendedAction;
            //LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar });
            Action = ((GSSettValueByCompany)((Grid)((HeaderedContentControl)((StackPanel)((Grid)((ContentPresenter)this.TemplatedParent).Parent).Parent).TemplatedParent).Parent).Parent).Action;
            if (Action == eAccionSR.Insertar) {
                SetFormValuesFromNavigator(true);
            } else {
                SetFormValuesFromNavigator(false);
            }
            HabilitarNotaDebito();
            HabilitarNotaCredito();
            SetLookAndFeelForCurrentRecord();
            Tool.AllDisabled(gwMain.Children, Action);
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.NombrePlantillaNotaDeCredito = txtNombrePlantillaNotaDeCredito.Text;            
            _CurrentInstance.PrimeraNotaDeCredito = txtPrimeraNotaDeCredito.Text;
            _CurrentInstance.TipoDePrefijoNCAsEnum = (eTipoDePrefijoFactura) cmbTipoDePrefijoNC.SelectedItemToInt();
            _CurrentInstance.PrefijoNC = txtPrefijoNC.Text;
            _CurrentInstance.PrimeraBoleta = txtPrimeraBoleta.Text;
            _CurrentInstance.NombrePlantillaBoleta = txtNombrePlantillaBoleta.Text;
            _CurrentInstance.NombrePlantillaNotaDeDebito = txtNombrePlantillaNotaDeDebito.Text;
            _CurrentInstance.NDPreNumeradaAsBool = chkNDPreNumerada.IsChecked.Value;
            _CurrentInstance.PrimeraNotaDeDebito = txtPrimeraNotaDeDebito.Text;
            _CurrentInstance.TipoDePrefijoNDAsEnum = (eTipoDePrefijoFactura) cmbTipoDePrefijoND.SelectedItemToInt();
            _CurrentInstance.PrefijoND = txtPrefijoND.Text;
            _CurrentInstance.NCPreNumeradaAsBool = chkNCPreNumerada.IsChecked.Value;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            txtNombrePlantillaNotaDeCredito.Text = _CurrentInstance.NombrePlantillaNotaDeCredito;            
            txtPrimeraNotaDeCredito.Text = _CurrentInstance.PrimeraNotaDeCredito;
            cmbTipoDePrefijoNC.SelectItem(_CurrentInstance.TipoDePrefijoNCAsEnum);
            txtPrefijoNC.Text = _CurrentInstance.PrefijoNC;
            txtPrimeraBoleta.Text = _CurrentInstance.PrimeraBoleta;
            txtNombrePlantillaBoleta.Text = _CurrentInstance.NombrePlantillaBoleta;
            txtNombrePlantillaNotaDeDebito.Text = _CurrentInstance.NombrePlantillaNotaDeDebito;
            chkNDPreNumerada.IsChecked = _CurrentInstance.NDPreNumeradaAsBool;
            txtPrimeraNotaDeDebito.Text = _CurrentInstance.PrimeraNotaDeDebito;
            cmbTipoDePrefijoND.SelectItem(_CurrentInstance.TipoDePrefijoNDAsEnum);
            txtPrefijoND.Text = _CurrentInstance.PrefijoND;
            chkNCPreNumerada.IsChecked = _CurrentInstance.NCPreNumeradaAsBool;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.cmbTipoDePrefijoNC.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoDePrefijoNC_Validating);
            this.cmbTipoDePrefijoND.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoDePrefijoND_Validating);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
            this.txtNombrePlantillaNotaDeCredito.Validating +=new CancelEventHandler(txtNombrePlantillaNotaDeCredito_Validating);
            this.txtNombrePlantillaNotaDeDebito.Validating += new CancelEventHandler(txtNombrePlantillaNotaDeDebito_Validating);
            this.btnBuscarRpxNotaDeCredito.Click += new RoutedEventHandler(btnBuscarRpxNotaDeCredito_Click);
            this.btnBuscarRpxNotaDeDebito.Click += new RoutedEventHandler(btnBuscarRpxNotaDeDebito_Click);
            this.txtPrimeraNotaDeCredito.Validating += new CancelEventHandler(txtPrimeraNotaDeCredito_Validating);
            this.txtPrimeraNotaDeDebito.Validating += new CancelEventHandler(txtPrimeraNotaDeDebito_Validating);
            this.txtPrimeraBoleta.Validating += new CancelEventHandler(txtPrimeraBoleta_Validating);
            this.txtNombrePlantillaBoleta.Validating += new CancelEventHandler(txtNombrePlantillaBoleta_Validating);
            this.btnBuscarRpxBoleta.Click += new RoutedEventHandler(btnBuscarRpxBoleta_Click);
            this.chkNCPreNumerada.Click += new RoutedEventHandler(chkNCPreNumerada_Click);
            this.chkNDPreNumerada.Click += new RoutedEventHandler(chkNDPreNumerada_Click);
        }

        void chkNDPreNumerada_Click(object sender, RoutedEventArgs e) {
            HabilitarNotaDebito();
        }

        void HabilitarNotaDebito() {
            LibApiAwp.EnableControl(txtPrimeraNotaDeDebito, !(bool)chkNDPreNumerada.IsChecked);
            LibApiAwp.EnableControl(cmbTipoDePrefijoND, !(bool)chkNDPreNumerada.IsChecked);
            LibApiAwp.EnableControl(txtPrefijoND, (!(bool)chkNDPreNumerada.IsChecked && cmbTipoDePrefijoND.SelectedItemToEnum<eTipoDePrefijoFactura>() == eTipoDePrefijoFactura.Indicar));
        }

        void chkNCPreNumerada_Click(object sender, RoutedEventArgs e) {
            HabilitarNotaCredito();
        }

        void HabilitarNotaCredito() {
            LibApiAwp.EnableControl(txtPrimeraNotaDeCredito, !(bool)chkNCPreNumerada.IsChecked);
            LibApiAwp.EnableControl(cmbTipoDePrefijoNC, !(bool)chkNCPreNumerada.IsChecked);
            LibApiAwp.EnableControl(txtPrefijoNC, (!(bool)chkNCPreNumerada.IsChecked && cmbTipoDePrefijoNC.SelectedItemToEnum<eTipoDePrefijoFactura>() == eTipoDePrefijoFactura.Indicar));
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


        void cmbTipoDePrefijoNC_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                LibApiAwp.EnableControl(txtPrefijoNC, ((!(bool)chkNCPreNumerada.IsChecked) && cmbTipoDePrefijoNC.SelectedItemToEnum<eTipoDePrefijoFactura>() == eTipoDePrefijoFactura.Indicar));
                cmbTipoDePrefijoNC.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbTipoDePrefijoND_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                LibApiAwp.EnableControl(txtPrefijoND, ((!(bool)chkNDPreNumerada.IsChecked) && cmbTipoDePrefijoND.SelectedItemToEnum<eTipoDePrefijoFactura>() == eTipoDePrefijoFactura.Indicar));
                cmbTipoDePrefijoND.ValidateTextInCombo();
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


        void txtPrimeraNotaDeCredito_Validating(object sender, CancelEventArgs e) {
            try {
                if(CancelValidations) {
                    return;
                }
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);
                string vPrimeraNotaCredito = "";
                if (!insSettValueByCompanyIpl.EsValidaPrimeraNotaDeCredito(txtPrimeraNotaDeCredito.Text, out vPrimeraNotaCredito)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettValueByCompanyIpl.Information.ToString(), Title);
                    txtPrimeraNotaDeCredito.Text = vPrimeraNotaCredito;
                    e.Cancel = true;
                }
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }


        void txtNombrePlantillaNotaDeCredito_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaNotaDeCredito.Text) == 0) {
                txtNombrePlantillaNotaDeCredito.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaNotaDeCredito.Text)) {
                BuscarRpxPlantillaNotaDeCredito();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaNotaDeCredito.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaNotaDeCredito.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }

        void btnBuscarRpxNotaDeCredito_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxPlantillaNotaDeCredito();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void BuscarRpxPlantillaNotaDeCredito() {
            string paramBusqueda = "rpx de Nota de Crédito (*.rpx)|*Nota*Credito*.rpx";
            txtNombrePlantillaNotaDeCredito.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }


        void txtPrimeraNotaDeDebito_Validating(object sender, CancelEventArgs e) {
            try {
                if(CancelValidations) {
                    return;
                }
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);
                string vPrimeraNotaDebito = "";
                if (!insSettValueByCompanyIpl.EsValidaPrimeraNotaDeDebito(txtPrimeraNotaDeDebito.Text, out vPrimeraNotaDebito)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettValueByCompanyIpl.Information.ToString(), Title);
                    txtPrimeraNotaDeDebito.Text = vPrimeraNotaDebito;
                    e.Cancel = true;
                }
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void btnBuscarRpxNotaDeDebito_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxPlantillaNotaDeDebito();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtNombrePlantillaNotaDeDebito_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaNotaDeDebito.Text) == 0) {
                txtNombrePlantillaNotaDeDebito.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaNotaDeDebito.Text)) {
                BuscarRpxPlantillaNotaDeDebito();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaNotaDeDebito.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaNotaDeDebito.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }

        private void BuscarRpxPlantillaNotaDeDebito() {
            string paramBusqueda = "rpx de Nota de Débito (*.rpx)|*Nota*Debito*.rpx";
            txtNombrePlantillaNotaDeDebito.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }
       
        void txtPrimeraBoleta_Validating(object sender, CancelEventArgs e) {
            try {
                if(CancelValidations) {
                    return;
                }
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);                
                string vPrimeraBoleta = "";
                if(!insSettValueByCompanyIpl.EsValidaPrimeraBoleta(txtPrimeraBoleta.Text, out vPrimeraBoleta)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettValueByCompanyIpl.Information.ToString(), Title);
                    txtPrimeraBoleta.Text = vPrimeraBoleta;
                    e.Cancel = true;
                }
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtNombrePlantillaBoleta_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaBoleta.Text) == 0) {
                txtNombrePlantillaBoleta.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaBoleta.Text)) {
                BuscarRpxPlantillaBoleta();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaBoleta.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaBoleta.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }

        private void BuscarRpxPlantillaBoleta() {
            string paramBusqueda = "rpx de Boleta (*.rpx)|*Boleta*.rpx";
            txtNombrePlantillaBoleta.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }

        void btnBuscarRpxBoleta_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxPlantillaBoleta();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }


    } //End of class GSNotasDebitoCreditoEntregaStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

