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
    /// Lógica de interacción para GSSettDefinition.xaml
    /// </summary>
    internal partial class GSSettDefinition : UserControl {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        SettDefinition _CurrentInstance;
        
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
        internal SettDefinition CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSSettDefinition() {
            InitializeComponent();
            InitializeEvents();
            cmbDataType.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeDatoParametros)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Name, Nivel Modulo, Group Name, Nivel del Grupo";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (SettDefinition)initInstance;

            Title = "SettDefinition";
            Action = initAction;
            ExtendedAction = initExtendedAction;
            LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar });
            if (Action == eAccionSR.Insertar) {
                SetFormValuesFromNavigator(true);
            } else {
                SetFormValuesFromNavigator(false);
            }
            LibApiAwp.EnableControl(txtName, Action == eAccionSR.Insertar);
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.Name = txtName.Text;
            _CurrentInstance.Module = txtModule.Text;
            _CurrentInstance.LevelModule = LibConvert.ToInt(txtLevelModule.Text);
            _CurrentInstance.GroupName = txtGroupName.Text;
            _CurrentInstance.LevelGroup = LibConvert.ToInt(txtLevelGroup.Text);
            _CurrentInstance.Label = txtLabel.Text;
            _CurrentInstance.DataTypeAsEnum = (eTipoDeDatoParametros) cmbDataType.SelectedItemToInt();
            _CurrentInstance.Validationrules = txtValidationrules.Text;
            _CurrentInstance.IsSetForAllEnterpriseAsBool = chkIsSetForAllEnterprise.IsChecked.Value;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            txtName.Text = _CurrentInstance.Name;
            txtModule.Text = _CurrentInstance.Module;
            txtLevelModule.Text = LibConvert.ToStr(_CurrentInstance.LevelModule);
            txtGroupName.Text = _CurrentInstance.GroupName;
            txtLevelGroup.Text = LibConvert.ToStr(_CurrentInstance.LevelGroup);
            txtLabel.Text = _CurrentInstance.Label;
            cmbDataType.SelectItem(_CurrentInstance.DataTypeAsEnum);
            txtValidationrules.Text = _CurrentInstance.Validationrules;
            chkIsSetForAllEnterprise.IsChecked = _CurrentInstance.IsSetForAllEnterpriseAsBool;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(txtName_Validating);
            this.txtLevelModule.Validating += new System.ComponentModel.CancelEventHandler(txtLevelModule_Validating);
            this.txtGroupName.Validating += new System.ComponentModel.CancelEventHandler(txtGroupName_Validating);
            this.txtLevelGroup.Validating += new System.ComponentModel.CancelEventHandler(txtLevelGroup_Validating);
            this.cmbDataType.Validating += new System.ComponentModel.CancelEventHandler(cmbDataType_Validating);
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


        void txtName_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsSettDefinitionIpl insSettDefinitionIpl = new clsSettDefinitionIpl();
                if (!insSettDefinitionIpl.IsValidName(Action, txtName.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettDefinitionIpl.Information.ToString(), Title);
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

        void txtLevelModule_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsSettDefinitionIpl insSettDefinitionIpl = new clsSettDefinitionIpl();
                if (!insSettDefinitionIpl.IsValidLevelModule(Action, LibConvert.ToInt(txtLevelModule.Text), true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettDefinitionIpl.Information.ToString(), Title);
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

        void txtGroupName_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsSettDefinitionIpl insSettDefinitionIpl = new clsSettDefinitionIpl();
                if (!insSettDefinitionIpl.IsValidGroupName(Action, txtGroupName.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettDefinitionIpl.Information.ToString(), Title);
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

        void txtLevelGroup_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsSettDefinitionIpl insSettDefinitionIpl = new clsSettDefinitionIpl();
                if (!insSettDefinitionIpl.IsValidLevelGroup(Action, LibConvert.ToInt(txtLevelGroup.Text), true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettDefinitionIpl.Information.ToString(), Title);
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

        void cmbDataType_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbDataType.ValidateTextInCombo();
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


    } //End of class GSSettDefinition.xaml

} //End of namespace Galac.Saw.Uil.SttDef

