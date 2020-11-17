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
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Uil.Tablas {
    /// <summary>
    /// Lógica de interacción para frmPropAnalisisVencInput.xaml
    /// </summary>
    public partial class frmPropAnalisisVencInput:LibFrmInputBase {
        #region Variables
        IList<PropAnalisisVenc> insPropAnalisisVenc;
        ILibView insModel;
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public frmPropAnalisisVencInput(string initRecordUserName, eAccionSR initAction, string initExtendedAction)
            : base(initRecordUserName, initAction, initExtendedAction) {
            InitializeComponent();
            try {
                gucPropAnalisisVenc.Action = initAction;
                gucPropAnalisisVenc.ExtendedAction = initExtendedAction;
                gucPropAnalisisVenc.CancelValidations = (initAction == eAccionSR.Consultar || initAction == eAccionSR.Eliminar);
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
            gucPropAnalisisVenc.ClearControl();
        }

        protected override void DisableAllFields() {
            gucPropAnalisisVenc.DisableControl();
        }

        public bool InitLookAndFeelAndSetValues(IList<PropAnalisisVenc> refPropAnalisisVenc, ILibView refModel) {
            bool vResult = true;
            insModel = refModel;
            if (Action == eAccionSR.Insertar) {
                insPropAnalisisVenc = refPropAnalisisVenc;
                insModel.Clear(insPropAnalisisVenc[0]);
            } else {
                if (refPropAnalisisVenc == null) {
                    insPropAnalisisVenc = new List<PropAnalisisVenc>();
                    insPropAnalisisVenc.Add(new PropAnalisisVenc());
                } else {
                    insPropAnalisisVenc = refPropAnalisisVenc;
                }
            }
            gucPropAnalisisVenc.InitializeControl(insPropAnalisisVenc[0], insModel, Action, ExtendedAction);
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action == eAccionSR.Insertar) {
                lneLastModified.Visibility = Visibility.Hidden;
                lblTitleLastModified.Visibility = Visibility.Hidden;
            } else {
                lneLastModified.SetContent(insPropAnalisisVenc[0].NombreOperador, insPropAnalisisVenc[0].FechaUltimaModificacion);
            }
        }

        protected override void SetCancelValidations(bool valValue) {
            base.SetCancelValidations(valValue);
            gucPropAnalisisVenc.CancelValidations = valValue;
        }

        protected override bool StoreRecord(bool valStoreAndExit) {
            string vErrorMsg;
            bool vResult = false;
            switch (Action) {
                case eAccionSR.Insertar:
                    gucPropAnalisisVenc.SetNavigatorValuesFromForm();
                    insPropAnalisisVenc[0] = gucPropAnalisisVenc.CurrentInstance;
                    vResult = insModel.InsertRecord(insPropAnalisisVenc[0], out vErrorMsg);
                    if (vResult) {
                        gucPropAnalisisVenc.SetFormValuesFromNavigator(true);
                        MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                    } else {
                        LibNotifier.ValidationError(this, vErrorMsg, insModel.MessageName);
                    }
                    break;
                case eAccionSR.Modificar:
                    gucPropAnalisisVenc.SetNavigatorValuesFromForm();
                    insPropAnalisisVenc[0] = gucPropAnalisisVenc.CurrentInstance;
                    vResult = insModel.UpdateRecord(insPropAnalisisVenc[0], eAccionSR.Modificar, out vErrorMsg);
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
                    gucPropAnalisisVenc.SetNavigatorValuesFromForm();
                    insPropAnalisisVenc[0] = gucPropAnalisisVenc.CurrentInstance;
                    if (LibNotifier.Confirm(this, "¿Está seguro de que desea eliminar este registro.?", this.Title)) {
                        insModel.DeleteRecord(insPropAnalisisVenc[0]);
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


    } //End of class frmPropAnalisisVencInput.xaml

} //End of namespace Galac.Saw.Uil.Tablas

