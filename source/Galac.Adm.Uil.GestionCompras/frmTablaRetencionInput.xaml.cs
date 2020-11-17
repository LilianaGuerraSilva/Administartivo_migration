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
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Uil.GestionCompras {
    /// <summary>
    /// Lógica de interacción para frmTablaRetencionInput.xaml
    /// </summary>
    public partial class frmTablaRetencionInput:LibFrmInputBase {
        #region Variables
        IList<TablaRetencion> insTablaRetencion;
        ILibView insModel;
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public frmTablaRetencionInput(string initRecordUserName, eAccionSR initAction, string initExtendedAction)
            : base(initRecordUserName, initAction, initExtendedAction) {
            InitializeComponent();
            try {
                gucTablaRetencion.Action = initAction;
                gucTablaRetencion.ExtendedAction = initExtendedAction;
                gucTablaRetencion.CancelValidations = (initAction == eAccionSR.Consultar || initAction == eAccionSR.Eliminar);
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
            gucTablaRetencion.ClearControl();
        }

        protected override void DisableAllFields() {
            gucTablaRetencion.DisableControl();
        }

        public bool InitLookAndFeelAndSetValues(IList<TablaRetencion> refTablaRetencion, ILibView refModel) {
            bool vResult = true;
            insModel = refModel;
            if (Action == eAccionSR.Insertar) {
                insTablaRetencion = refTablaRetencion;
                insModel.Clear(insTablaRetencion[0]);
            } else {
                if (refTablaRetencion == null) {
                    insTablaRetencion = new List<TablaRetencion>();
                    insTablaRetencion.Add(new TablaRetencion());
                } else {
                    insTablaRetencion = refTablaRetencion;
                }
            }
            gucTablaRetencion.InitializeControl(insTablaRetencion[0], insModel, Action, ExtendedAction);
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action == eAccionSR.Insertar) {
            }
        }

        protected override void SetCancelValidations(bool valValue) {
            base.SetCancelValidations(valValue);
            gucTablaRetencion.CancelValidations = valValue;
        }

        protected override bool StoreRecord(bool valStoreAndExit) {
            string vErrorMsg;
            bool vResult = false;
            switch (Action) {
                case eAccionSR.Insertar:
                    gucTablaRetencion.SetNavigatorValuesFromForm();
                    insTablaRetencion[0] = gucTablaRetencion.CurrentInstance;
                    vResult = insModel.InsertRecord(insTablaRetencion[0], out vErrorMsg);
                    if (vResult) {
                        gucTablaRetencion.SetFormValuesFromNavigator(true);
                        MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                    } else {
                        LibNotifier.ValidationError(this, vErrorMsg, insModel.MessageName);
                    }
                    break;
                case eAccionSR.Modificar:
                    gucTablaRetencion.SetNavigatorValuesFromForm();
                    insTablaRetencion[0] = gucTablaRetencion.CurrentInstance;
                    vResult = insModel.UpdateRecord(insTablaRetencion[0], eAccionSR.Modificar, out vErrorMsg);
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
                    gucTablaRetencion.SetNavigatorValuesFromForm();
                    insTablaRetencion[0] = gucTablaRetencion.CurrentInstance;
                    if (LibNotifier.Confirm(this, "¿Está seguro de que desea eliminar este registro.?", this.Title)) {
                        insModel.DeleteRecord(insTablaRetencion[0]);
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


    } //End of class frmTablaRetencionInput.xaml

} //End of namespace Galac.Adm.Uil.GestionCompras

