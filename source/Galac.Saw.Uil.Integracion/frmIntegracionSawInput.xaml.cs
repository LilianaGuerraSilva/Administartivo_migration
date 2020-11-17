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
using Galac.Saw.Ccl.Integracion;

namespace Galac.Saw.Uil.Integracion {
    /// <summary>
    /// Lógica de interacción para frmIntegracionSawInput.xaml
    /// </summary>
    public partial class frmIntegracionSawInput:LibFrmInputBase {
        #region Variables
        IList<IntegracionSaw> insIntegracionSaw;
        ILibView insModel;
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public frmIntegracionSawInput(string initRecordUserName, eAccionSR initAction, string initExtendedAction)
            : base(initRecordUserName, initAction, initExtendedAction) {
            InitializeComponent();
            try {
                gucIntegracionSaw.Action = initAction;
                gucIntegracionSaw.ExtendedAction = initExtendedAction;
                gucIntegracionSaw.CancelValidations = (initAction == eAccionSR.Consultar || initAction == eAccionSR.Eliminar);
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
            gucIntegracionSaw.ClearControl();
        }

        protected override void DisableAllFields() {
            gucIntegracionSaw.DisableControl();
        }

        public bool InitLookAndFeelAndSetValues(IList<IntegracionSaw> refIntegracionSaw, ILibView refModel) {
            bool vResult = true;
            insModel = refModel;
            if (Action == eAccionSR.Insertar) {
                insIntegracionSaw = refIntegracionSaw;
                insModel.Clear(insIntegracionSaw[0]);
            } else {
                if (refIntegracionSaw == null) {
                    insIntegracionSaw = new List<IntegracionSaw>();
                    insIntegracionSaw.Add(new IntegracionSaw());
                } else {
                    insIntegracionSaw = refIntegracionSaw;
                }
            }
            gucIntegracionSaw.InitializeControl(insIntegracionSaw[0], insModel, Action, ExtendedAction);
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action == eAccionSR.Insertar) {
                lneLastModified.Visibility = Visibility.Hidden;
                lblTitleLastModified.Visibility = Visibility.Hidden;
            } else {
                lneLastModified.SetContent(insIntegracionSaw[0].NombreOperador, insIntegracionSaw[0].FechaUltimaModificacion);
            }
        }

        protected override void SetCancelValidations(bool valValue) {
            base.SetCancelValidations(valValue);
            gucIntegracionSaw.CancelValidations = valValue;
        }

        protected override bool StoreRecord(bool valStoreAndExit) {
            string vErrorMsg;
            bool vResult = false;
            switch (Action) {
                case eAccionSR.Insertar:
                    gucIntegracionSaw.SetNavigatorValuesFromForm();
                    insIntegracionSaw[0] = gucIntegracionSaw.CurrentInstance;
                    vResult = insModel.InsertRecord(insIntegracionSaw[0], out vErrorMsg);
                    if (vResult) {
                        gucIntegracionSaw.SetFormValuesFromNavigator(true);
                        MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                    } else {
                        LibNotifier.ValidationError(this, vErrorMsg, insModel.MessageName);
                    }
                    break;
                case eAccionSR.Modificar:
                    gucIntegracionSaw.SetNavigatorValuesFromForm();
                    insIntegracionSaw[0] = gucIntegracionSaw.CurrentInstance;
                    vResult = insModel.UpdateRecord(insIntegracionSaw[0], eAccionSR.Modificar, out vErrorMsg);
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
                    gucIntegracionSaw.SetNavigatorValuesFromForm();
                    insIntegracionSaw[0] = gucIntegracionSaw.CurrentInstance;
                    if (LibNotifier.Confirm(this, "¿Está seguro de que desea eliminar este registro.?", this.Title)) {
                        insModel.DeleteRecord(insIntegracionSaw[0]);
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


    } //End of class frmIntegracionSawInput.xaml

} //End of namespace Galac.Saw.Uil.Integracion

