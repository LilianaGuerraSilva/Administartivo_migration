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
    /// Lógica de interacción para frmSettValueByCompanyInput.xaml
    /// </summary>
    public partial class frmSettValueByCompanyInput : LibFrmInputBase {
        #region Variables
        ILibView insModel;
        #endregion //Variables

        #region Propiedades
        #endregion //Propiedades

        #region Constructores

        public frmSettValueByCompanyInput(string initRecordUserName, eAccionSR initAction, string initExtendedAction)
            : base(initRecordUserName, initAction, initExtendedAction) {
            InitializeComponent();
            try {
                //gucSettValueByCompany.Action = initAction;
                //gucSettValueByCompany.ExtendedAction = initExtendedAction;
                //gucSettValueByCompany.CancelValidations = (initAction == eAccionSR.Consultar || initAction == eAccionSR.Eliminar);
                if (ExtendedAction.Length > 0) {
                    btnStore.Content = string.Format("_{0}", ExtendedAction);
                } else {
                    btnStore.Content = string.Format("_{0}", LibEAccionSR.ToString(Action));
                }
                if (Action == eAccionSR.Consultar) {
                    btnStore.IsEnabled = false;
                    btnStore.Visibility = Visibility.Hidden;
                }

                //InitializeEvents();
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
            //gucSettValueByCompany.ClearControl();
        }

        protected override void DisableAllFields() {
            //gucSettValueByCompany.DisableControl();
        }

        public bool InitLookAndFeelAndSetValues(IList<SettValueByCompany> refSettValueByCompany, ILibView refModel) {
            bool vResult = true;
            insModel = refModel;
            gucSettValueByCompany.DataContext = refModel;
            gucSettValueByCompany.InitializeControl(insModel, Action, ExtendedAction);
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action == eAccionSR.Insertar) {
                //lneLastModified.Visibility = Visibility.Hidden;
                //lblTitleLastModified.Visibility = Visibility.Hidden;
            } else {
                //lneLastModified.SetContent(insSettValueByCompany[0].NombreOperador, insSettValueByCompany[0].FechaUltimaModificacion);
            }
        }

        protected override void SetCancelValidations(bool valValue) {
            base.SetCancelValidations(valValue);
            //gucSettValueByCompany.CancelValidations = valValue;
        }

        protected override bool StoreRecord(bool valStoreAndExit) {
            string vErrorMsg;
            bool vResult = false;
            switch (Action) {
                //case eAccionSR.Insertar:
                //    //gucSettValueByCompany.SetNavigatorValuesFromForm();
                //    //insSettValueByCompany[0] = gucSettValueByCompany.CurrentInstance;
                //    vResult = insModel.InsertRecord(insSettValueByCompany[0], out vErrorMsg);
                //    if (vResult) {
                //        //gucSettValueByCompany.SetFormValuesFromNavigator(true);
                //        MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                //    } else {
                //        LibNotifier.ValidationError(this, vErrorMsg, insModel.MessageName);
                //    }
                //    break;
                case eAccionSR.Modificar:
                    gucSettValueByCompany.SetNavigatorValuesFromForm();
                    //gucSettValueByCompany.gbMenu.SelectedContent = this.
                    //var ListProceso = ((clsSettValueByCompanyIpl)insModel).ModuleList[8].Groups[0].Content;
                    //ListProceso.GetType().GetProperty("InsertandoPorPrimeraVezAsBool").SetValue(ListProceso, false, null);

                    //this.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));
                    //this.RemoveLogicalChild(gucSettValueByCompany);
                    //gwMain.Children.Remove(gucSettValueByCompany);
                    //Application.Current.Shutdown();
                    //this.Dispose();
                    //Action = eAccionSR.Salir;
                    //SetCancelValidations(false);
                    //this.ExitWindow();
                    vResult = insModel.UpdateRecord(((clsSettValueByCompanyIpl)insModel).ModuleList, eAccionSR.Modificar, out vErrorMsg);
                    if (vResult) {
                        if (valStoreAndExit) {
                            Action = eAccionSR.Salir;
                            this.Close();
                           // Owner.Close();
                        }
                    } else {
                        LibNotifier.ValidationError(this, vErrorMsg, insModel.MessageName);
                    }
                    if (!valStoreAndExit) {
                        MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                    }
                    break;
                //case eAccionSR.Eliminar:
                //    //gucSettValueByCompany.SetNavigatorValuesFromForm();
                //    //insSettValueByCompany[0] = gucSettValueByCompany.CurrentInstance;
                //    if (LibNotifier.Confirm(this, "¿Está seguro de que desea eliminar este registro.?", this.Title)) {
                //        insModel.DeleteRecord(insSettValueByCompany[0]);
                //    }
                //    this.Close();
                //    break;
                default:
                    LibNotifier.Warning(this, "Falta Programarlo para esta acción");
                    break;
            }
            return vResult;
        }

        public void setAction(eAccionSR action){
            Action = action;
        }
        #endregion //Metodos Generados

    } //End of class frmSettValueByCompanyInput.xaml
} //End of namespace Galac.Saw.Uil.SttDef

