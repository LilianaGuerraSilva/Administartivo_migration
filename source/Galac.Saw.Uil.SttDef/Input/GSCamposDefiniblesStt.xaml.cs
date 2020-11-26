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
    /// Lógica de interacción para GSCamposDefiniblesStt.xaml
    /// </summary>
    internal partial class GSCamposDefiniblesStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        CamposDefiniblesStt _CurrentInstance;
        
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
        internal CamposDefiniblesStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSCamposDefiniblesStt() {
            InitializeComponent();
            InitializeEvents();
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
            _CurrentInstance = (CamposDefiniblesStt)initInstance;

            Title = "Campos Definibles";
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
            _CurrentInstance.UsaCamposDefiniblesAsBool = chkUsaCamposDefinibles.IsChecked.Value;
            _CurrentInstance.NombreCampoDefinible1 = txtNombreCampoDefinible1.Text;
            _CurrentInstance.NombreCampoDefinible2 = txtNombreCampoDefinible2.Text;
            _CurrentInstance.NombreCampoDefinible3 = txtNombreCampoDefinible3.Text;
            _CurrentInstance.NombreCampoDefinible4 = txtNombreCampoDefinible4.Text;
            _CurrentInstance.NombreCampoDefinible5 = txtNombreCampoDefinible5.Text;
            _CurrentInstance.NombreCampoDefinible6 = txtNombreCampoDefinible6.Text;
            _CurrentInstance.NombreCampoDefinible7 = txtNombreCampoDefinible7.Text;
            _CurrentInstance.NombreCampoDefinible8 = txtNombreCampoDefinible8.Text;
            _CurrentInstance.NombreCampoDefinible9 = txtNombreCampoDefinible9.Text;
            _CurrentInstance.NombreCampoDefinible10 = txtNombreCampoDefinible10.Text;
            _CurrentInstance.NombreCampoDefinible11 = txtNombreCampoDefinible11.Text;
            _CurrentInstance.NombreCampoDefinible12 = txtNombreCampoDefinible12.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            chkUsaCamposDefinibles.IsChecked = _CurrentInstance.UsaCamposDefiniblesAsBool;
            txtNombreCampoDefinible1.Text = _CurrentInstance.NombreCampoDefinible1;
            txtNombreCampoDefinible2.Text = _CurrentInstance.NombreCampoDefinible2;
            txtNombreCampoDefinible3.Text = _CurrentInstance.NombreCampoDefinible3;
            txtNombreCampoDefinible4.Text = _CurrentInstance.NombreCampoDefinible4;
            txtNombreCampoDefinible5.Text = _CurrentInstance.NombreCampoDefinible5;
            txtNombreCampoDefinible6.Text = _CurrentInstance.NombreCampoDefinible6;
            txtNombreCampoDefinible7.Text = _CurrentInstance.NombreCampoDefinible7;
            txtNombreCampoDefinible8.Text = _CurrentInstance.NombreCampoDefinible8;
            txtNombreCampoDefinible9.Text = _CurrentInstance.NombreCampoDefinible9;
            txtNombreCampoDefinible10.Text = _CurrentInstance.NombreCampoDefinible10;
            txtNombreCampoDefinible11.Text = _CurrentInstance.NombreCampoDefinible11;
            txtNombreCampoDefinible12.Text = _CurrentInstance.NombreCampoDefinible12;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
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


        private void RealizaLosCalculos() {
            if (Action != eAccionSR.Consultar && Action != eAccionSR.Eliminar) {
                //throw new NotImplementedException("Debe sobreescribir el metodo RealizaLosCalculos para su caso especifico. Si no lo requiere no lo invoque.");
            }
        }
        #endregion //Metodos Generados


    } //End of class GSCamposDefiniblesStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

