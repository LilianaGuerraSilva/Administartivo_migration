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
    /// Lógica de interacción para GSSettValueByCompany.xaml
    /// </summary>
    internal partial class GSSettValueByCompany : UserControl {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        ILibView _CurrentModel;

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
        public ILibView CurrentModel {
            get { return _CurrentModel; }
            set { _CurrentModel = value; }
        }        
        #endregion //Propiedades
        #region Constructores

        public GSSettValueByCompany() {
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

        internal bool InitializeControl(ILibView initModel, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentModel = initModel;
            Title = initModel.MessageName;
            Action = initAction;
            ExtendedAction = initExtendedAction;
            LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar });
            if (Action == eAccionSR.Insertar) {
                SetFormValuesFromNavigator(true);
            } else {
                SetFormValuesFromNavigator(false);
            }
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
            }
        }


        void BuscaUltimoUserControl(DependencyObject fe, List<IInputView> lo) {
            int cont = VisualTreeHelper.GetChildrenCount(fe);
            if (fe is IInputView) {
                lo.Add((IInputView)fe);
            }
            for (int i = 0; i < cont; i++) {
                BuscaUltimoUserControl(VisualTreeHelper.GetChild(fe, i), lo);
            }
        }

        public void SetNavigatorValuesFromForm() {
            List<IInputView> lo = new List<IInputView>();
            BuscaUltimoUserControl(this.Parent, lo);
            //var tipo = lo[153].GetType();
            foreach (IInputView item in lo) {
                item.SetNavigatorValuesFromForm();
            }
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {           
            ClearControl();
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
        }

        private void RealizaLosCalculos() {
            if (Action != eAccionSR.Consultar && Action != eAccionSR.Eliminar) {
                //throw new NotImplementedException("Debe sobreescribir el metodo RealizaLosCalculos para su caso especifico. Si no lo requiere no lo invoque.");
            }
        }
        #endregion //Metodos Generados

   
    } //End of class GSSettValueByCompany.xaml
} //End of namespace Galac.Saw.Uil.SttDef

