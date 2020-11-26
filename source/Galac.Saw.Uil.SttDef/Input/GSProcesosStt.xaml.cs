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
    /// Lógica de interacción para GSProcesosStt.xaml
    /// </summary>
    internal partial class GSProcesosStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        ProcesosStt _CurrentInstance;
        
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
        internal ProcesosStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSProcesosStt() {
            InitializeComponent();
            InitializeEvents();
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "CodigoEmpresaIntegrada";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (ProcesosStt)initInstance;

            Title = "Procesos";
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
                gbMain.Visibility = System.Windows.Visibility.Hidden;
                chkInsertandoPorPrimeraVez.IsEnabled = false;
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.SeResincronizaronLosSupervisoresAsBool = chkSeResincronizaronLosSupervisores.IsChecked.Value;
            _CurrentInstance.InsertandoPorPrimeraVezAsBool = chkInsertandoPorPrimeraVez.IsChecked.Value;
            _CurrentInstance.CodigoEmpresaIntegrada = txtCodigoEmpresaIntegrada.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            chkSeResincronizaronLosSupervisores.IsChecked = _CurrentInstance.SeResincronizaronLosSupervisoresAsBool;
            chkInsertandoPorPrimeraVez.IsChecked = _CurrentInstance.InsertandoPorPrimeraVezAsBool;
            txtCodigoEmpresaIntegrada.Text = _CurrentInstance.CodigoEmpresaIntegrada;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtCodigoEmpresaIntegrada.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoEmpresaIntegrada_Validating);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
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


        void txtCodigoEmpresaIntegrada_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(null, null);
                if (!insSettValueByCompanyIpl.IsValidCodigoEmpresaIntegrada(Action, txtCodigoEmpresaIntegrada.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettValueByCompanyIpl.Information.ToString(), Title);
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

        private void RealizaLosCalculos() {
            if (Action != eAccionSR.Consultar && Action != eAccionSR.Eliminar) {
                //throw new NotImplementedException("Debe sobreescribir el metodo RealizaLosCalculos para su caso especifico. Si no lo requiere no lo invoque.");
            }
        }
        #endregion //Metodos Generados


    } //End of class GSProcesosStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

