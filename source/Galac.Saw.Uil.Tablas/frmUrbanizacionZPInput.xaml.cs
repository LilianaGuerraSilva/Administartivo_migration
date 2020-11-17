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
    /// L�gica de interacci�n para frmUrbanizacionZPInput.xaml
    /// </summary>
    public partial class frmUrbanizacionZPInput:LibFrmInputBase {
        #region Variables
        IList<UrbanizacionZP> insUrbanizacionZP;
        ILibView insModel;
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public frmUrbanizacionZPInput(string initRecordUserName, eAccionSR initAction, string initExtendedAction)
            : base(initRecordUserName, initAction, initExtendedAction) {
            InitializeComponent();
            try {
                gucUrbanizacionZP.Action = initAction;
                gucUrbanizacionZP.ExtendedAction = initExtendedAction;
                gucUrbanizacionZP.CancelValidations = (initAction == eAccionSR.Consultar || initAction == eAccionSR.Eliminar);
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
            gucUrbanizacionZP.ClearControl();
        }

        protected override void DisableAllFields() {
            gucUrbanizacionZP.DisableControl();
        }

        public bool InitLookAndFeelAndSetValues(IList<UrbanizacionZP> refUrbanizacionZP, ILibView refModel) {
            bool vResult = true;
            insModel = refModel;
            if (Action == eAccionSR.Insertar) {
                insUrbanizacionZP = refUrbanizacionZP;
                insModel.Clear(insUrbanizacionZP[0]);
            } else {
                if (refUrbanizacionZP == null) {
                    insUrbanizacionZP = new List<UrbanizacionZP>();
                    insUrbanizacionZP.Add(new UrbanizacionZP());
                } else {
                    insUrbanizacionZP = refUrbanizacionZP;
                }
            }
            gucUrbanizacionZP.InitializeControl(insUrbanizacionZP[0], insModel, Action, ExtendedAction);
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action == eAccionSR.Insertar) {
            }
        }

        protected override void SetCancelValidations(bool valValue) {
            base.SetCancelValidations(valValue);
            gucUrbanizacionZP.CancelValidations = valValue;
        }

        protected override bool StoreRecord(bool valStoreAndExit) {
            string vErrorMsg;
            bool vResult = false;
            switch (Action) {
                case eAccionSR.Insertar:
                    gucUrbanizacionZP.SetNavigatorValuesFromForm();
                    insUrbanizacionZP[0] = gucUrbanizacionZP.CurrentInstance;
                    vResult = insModel.InsertRecord(insUrbanizacionZP[0], out vErrorMsg);
                    if (vResult) {
                        gucUrbanizacionZP.SetFormValuesFromNavigator(true);
                        MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                    } else {
                        LibNotifier.ValidationError(this, vErrorMsg, insModel.MessageName);
                    }
                    break;
                case eAccionSR.Modificar:
                    gucUrbanizacionZP.SetNavigatorValuesFromForm();
                    insUrbanizacionZP[0] = gucUrbanizacionZP.CurrentInstance;
                    vResult = insModel.UpdateRecord(insUrbanizacionZP[0], eAccionSR.Modificar, out vErrorMsg);
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
                    gucUrbanizacionZP.SetNavigatorValuesFromForm();
                    insUrbanizacionZP[0] = gucUrbanizacionZP.CurrentInstance;
                    if (LibNotifier.Confirm(this, "�Est� seguro de que desea eliminar este registro.?", this.Title)) {
                        insModel.DeleteRecord(insUrbanizacionZP[0]);
                    }
                    this.Close();
                    break;
                default:
                    LibNotifier.Warning(this, "Falta Programarlo para esta acci�n");
                    break;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class frmUrbanizacionZPInput.xaml

} //End of namespace Galac.Saw.Uil.Tablas

