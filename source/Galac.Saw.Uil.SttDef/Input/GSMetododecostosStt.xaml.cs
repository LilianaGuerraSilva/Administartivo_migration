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
    /// Lógica de interacción para GSMetododecostosStt.xaml
    /// </summary>
    internal partial class GSMetododecostosStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        MetododecostosStt _CurrentInstance;        
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
        internal MetododecostosStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSMetododecostosStt() {
            InitializeComponent();
            InitializeEvents();
            cmbMetodoDeCosteo.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeMetodoDeCosteo)));
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
            _CurrentInstance = (MetododecostosStt)initInstance;            
            Title = "Metodo de costos";
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
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.MetodoDeCosteoAsEnum = (eTipoDeMetodoDeCosteo) cmbMetodoDeCosteo.SelectedItemToInt();
            _CurrentInstance.FechaDesdeUsoMetodoDeCosteo = dtpFechaDesdeUsoMetodoDeCosteo.Date;
            _CurrentInstance.FechaContabilizacionDeCosteo = dtpFechaContabilizacionDeCosteo.Date;
            _CurrentInstance.ComprobanteCostoDetalladoAsBool = chkComprobanteCostoDetallado.IsChecked.Value;
            _CurrentInstance.CalculoAutomaticoDeCostoAsBool = chkCalculoAutomaticoDeCosto.IsChecked.Value;
            _CurrentInstance.MaximoGastosAdmisibles = LibConvert.ToDec(txtMaximoGastosAdmisibles.Text);
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            cmbMetodoDeCosteo.SelectItem(_CurrentInstance.MetodoDeCosteoAsEnum);
            dtpFechaDesdeUsoMetodoDeCosteo.Date = _CurrentInstance.FechaDesdeUsoMetodoDeCosteo;
            dtpFechaContabilizacionDeCosteo.Date = _CurrentInstance.FechaContabilizacionDeCosteo;
            chkComprobanteCostoDetallado.IsChecked = _CurrentInstance.ComprobanteCostoDetalladoAsBool;
            chkCalculoAutomaticoDeCosto.IsChecked = _CurrentInstance.CalculoAutomaticoDeCostoAsBool;
            txtMaximoGastosAdmisibles.Text = LibConvert.ToStr(_CurrentInstance.MaximoGastosAdmisibles);
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.cmbMetodoDeCosteo.Validating += new System.ComponentModel.CancelEventHandler(cmbMetodoDeCosteo_Validating);
            this.cmbMetodoDeCosteo.SelectionChanged += new SelectionChangedEventHandler(cmbMetodoDeCosteo_SelectionChanged);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
        }
        
        private void cmbMetodoDeCosteo_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (e.AddedItems != null && e.AddedItems.Count > 0) {
                eTipoDeMetodoDeCosteo vTipoMetodo = (eTipoDeMetodoDeCosteo)e.AddedItems[0];
                if (vTipoMetodo == eTipoDeMetodoDeCosteo.CostoPromedio) {
                    gbDatosMetodoCostoPromedio.Visibility = System.Windows.Visibility.Visible;
                    dtpFechaDesdeUsoMetodoDeCosteo.Focus();
                } else {
                    gbDatosMetodoCostoPromedio.Visibility = System.Windows.Visibility.Collapsed;
                }
                chkCalculoAutomaticoDeCosto.Visibility = clsGlobalValues.AppMemoryInfo.GlobalValuesGetBool("Parametros", "CaracteristicaDeContabilidadActiva") ? Visibility.Visible : System.Windows.Visibility.Hidden;
                lblCalculoAutomaticoDeCosto.Visibility = clsGlobalValues.AppMemoryInfo.GlobalValuesGetBool("Parametros", "CaracteristicaDeContabilidadActiva") ? Visibility.Visible : System.Windows.Visibility.Hidden;
            }
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


        void cmbMetodoDeCosteo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
               
                cmbMetodoDeCosteo.ValidateTextInCombo();
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

        void dtpFechaContabilizacionDeCosteo_Validating(object sender, LibGalac.Aos.UI.WpfControls.DatePickerCancelEventArgs e) {
            try {
                if(CancelValidations) {
                    return;
                }
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);                
                DateTime vDateResult;
                if(!insSettValueByCompanyIpl.EsValidaFechaContabilizacionDeCosteo(dtpFechaContabilizacionDeCosteo.Date, dtpFechaDesdeUsoMetodoDeCosteo.Date, out vDateResult )) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettValueByCompanyIpl.Information.ToString(), Title);
                    dtpFechaContabilizacionDeCosteo.Date = vDateResult;
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

        void dtpFechaDesdeUsoMetodoDeCosteo_Validating(object sender, LibGalac.Aos.UI.WpfControls.DatePickerCancelEventArgs e) {
            try {
                if(CancelValidations) {
                    return;
                }
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);                
                DateTime vDateResult;
                string msg;
                if(!insSettValueByCompanyIpl.EsValidaFechaDesdeUsoMetodoDeCosteo(dtpFechaDesdeUsoMetodoDeCosteo.Date, out vDateResult)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(dtpFechaDesdeUsoMetodoDeCosteo), insSettValueByCompanyIpl.Information.ToString(), Title);
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


    } //End of class GSMetododecostosStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

