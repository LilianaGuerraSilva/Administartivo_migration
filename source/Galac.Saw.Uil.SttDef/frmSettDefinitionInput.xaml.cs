using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.ComponentModel;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Uil.SttDef {
    /// <summary>
    /// Lógica de interacción para frmSettDefinitionInput.xaml
    /// </summary>
    public partial class frmSettDefinitionInput:LibFrmInputBase {
        #region Variables
        IList<SettDefinition> insSettDefinition;
        ILibView insModel;
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public frmSettDefinitionInput(string initRecordUserName, eAccionSR initAction, string initExtendedAction)
            : base(initRecordUserName, initAction, initExtendedAction) {
            InitializeComponent();
            try {
                gucSettDefinition.Action = initAction;
                gucSettDefinition.ExtendedAction = initExtendedAction;
                gucSettDefinition.CancelValidations = (initAction == eAccionSR.Consultar || initAction == eAccionSR.Eliminar);
                if (ExtendedAction.Length > 0) {
                    btnStore.Content = string.Format("_{0}", ExtendedAction);
                } else {
                    btnStore.Content = string.Format("_{0}", LibEAccionSR.ToString(Action));
                }
                if (Action == eAccionSR.Consultar) {
                    btnStore.IsEnabled = false;
                    btnStore.Visibility = Visibility.Hidden;
                }
                InitializeEvents();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void OnLoaded() {
            try {
                MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        protected override void ResyncListOnClosing() {
            if ((Owner != null) && (Owner is LibFrmSearch)) {
                ((LibFrmSearch)Owner).ResyncList();
            }
        }

        protected override void ClearAllControls() {
            gucSettDefinition.ClearControl();
        }

        protected override void DisableAllFields() {
            gucSettDefinition.DisableControl();
        }

        public bool InitLookAndFeelAndSetValues(IList<SettDefinition> refSettDefinition, ILibView refModel) {
            bool vResult = true;
            insModel = refModel;
            if (Action == eAccionSR.Insertar) {
                insSettDefinition = refSettDefinition;
                insModel.Clear(insSettDefinition[0]);
            } else {
                if (refSettDefinition == null) {
                    insSettDefinition = new List<SettDefinition>();
                    insSettDefinition.Add(new SettDefinition());
                } else {
                    insSettDefinition = refSettDefinition;
                }
            }
            gucSettDefinition.InitializeControl(insSettDefinition[0], Action, ExtendedAction);
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action == eAccionSR.Insertar) {
            }
        }

        protected override void SetCancelValidations(bool valValue) {
            base.SetCancelValidations(valValue);
            gucSettDefinition.CancelValidations = valValue;
        }

        protected override bool StoreRecord(bool valStoreAndExit) {
            string vErrorMsg;
            bool vResult = false;
            switch (Action) {
                case eAccionSR.Insertar:
                    gucSettDefinition.SetNavigatorValuesFromForm();
                    insSettDefinition[0] = gucSettDefinition.CurrentInstance;
                    vResult = insModel.InsertRecord(insSettDefinition[0], out vErrorMsg);
                    if (vResult) {
                        gucSettDefinition.SetFormValuesFromNavigator(true);
                        MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                    } else {
                        LibNotifier.ValidationError(this, vErrorMsg, insModel.MessageName);
                    }
                    break;
                case eAccionSR.Modificar:
                    gucSettDefinition.SetNavigatorValuesFromForm();
                    insSettDefinition[0] = gucSettDefinition.CurrentInstance;
                    vResult = insModel.UpdateRecord(insSettDefinition[0], eAccionSR.Modificar, out vErrorMsg);
                    if (vResult) {
                        if (valStoreAndExit) {
                            Action = eAccionSR.Salir;
                            this.Close();
                        }
                    } else {
                        LibNotifier.ValidationError(this, vErrorMsg, insModel.MessageName);
                    }
                    if (!valStoreAndExit) {
                        MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                    }
                    break;
                case eAccionSR.Eliminar:
                    gucSettDefinition.SetNavigatorValuesFromForm();
                    insSettDefinition[0] = gucSettDefinition.CurrentInstance;
                    if (LibNotifier.Confirm(this, "¿Está seguro de que desea eliminar este registro.?", this.Title)) {
                        insModel.DeleteRecord(insSettDefinition[0]);
                    }
                    this.Close();
                    break;
                default:
                    LibNotifier.Warning(this, "Falta Programarlo para esta acción");
                    break;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class frmSettDefinitionInput.xaml

} //End of namespace Galac.Saw.Uil.SttDef

