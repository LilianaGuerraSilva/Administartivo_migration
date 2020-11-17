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
using Galac.Saw.Ccl.Contabilizacion;

namespace Galac.Saw.Uil.Contabilizacion {
    /// <summary>
    /// Lógica de interacción para frmReglasDeContabilizacionInput.xaml
    /// </summary>
    public partial class frmReglasDeContabilizacionInput:LibFrmInputBase {
        #region Variables
        IList<ReglasDeContabilizacion> insReglasDeContabilizacion;
        ILibView insModel;
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public frmReglasDeContabilizacionInput(string initRecordUserName, eAccionSR initAction, string initExtendedAction)
            : base(initRecordUserName, initAction, initExtendedAction) {
            InitializeComponent();
            try {
                gucReglasDeContabilizacion.Action = initAction;
                gucReglasDeContabilizacion.ExtendedAction = initExtendedAction;
                gucReglasDeContabilizacion.CancelValidations = (initAction == eAccionSR.Consultar || initAction == eAccionSR.Eliminar);
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
            gucReglasDeContabilizacion.ClearControl();
        }

        protected override void DisableAllFields() {
            gucReglasDeContabilizacion.DisableControl();
        }

        public bool InitLookAndFeelAndSetValues(IList<ReglasDeContabilizacion> refReglasDeContabilizacion, ILibView refModel) {
            bool vResult = true;
            insModel = refModel;
            if (Action == eAccionSR.Insertar) {
                insReglasDeContabilizacion = refReglasDeContabilizacion;
                insModel.Clear(insReglasDeContabilizacion[0]);
            } else {
                if (refReglasDeContabilizacion == null) {
                    throw new ApplicationException("Falta información de inicialización al invocar la ventana de Reglas De Contabilizacion.");
                } else {
                    insReglasDeContabilizacion = refReglasDeContabilizacion;
                }
            }
            gucReglasDeContabilizacion.InitializeControl(insReglasDeContabilizacion[0], insModel, Action, ExtendedAction);
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action == eAccionSR.Insertar) {
                lneLastModified.Visibility = Visibility.Hidden;
                lblTitleLastModified.Visibility = Visibility.Hidden;
            } else {
                lneLastModified.SetContent(insReglasDeContabilizacion[0].NombreOperador, insReglasDeContabilizacion[0].FechaUltimaModificacion);
            }
        }

        protected override void SetCancelValidations(bool valValue) {
            base.SetCancelValidations(valValue);
            gucReglasDeContabilizacion.CancelValidations = valValue;
        }

        protected override bool StoreRecord(bool valStoreAndExit) {
            string vErrorMsg;
            bool vResult = false;
            switch (Action) {
                case eAccionSR.Insertar:
                    gucReglasDeContabilizacion.SetNavigatorValuesFromForm();
                    insReglasDeContabilizacion[0] = gucReglasDeContabilizacion.CurrentInstance;
                    vResult = insModel.InsertRecord(insReglasDeContabilizacion[0], out vErrorMsg);
                    if (vResult) {
                        gucReglasDeContabilizacion.SetFormValuesFromNavigator(true);
                        MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                    } else {
                        LibNotifier.ValidationError(this, vErrorMsg, insModel.MessageName);
                    }
                    break;
                case eAccionSR.Modificar:
                    gucReglasDeContabilizacion.SetNavigatorValuesFromForm();
                    insReglasDeContabilizacion[0] = gucReglasDeContabilizacion.CurrentInstance;
                    vResult = insModel.UpdateRecord(insReglasDeContabilizacion[0], eAccionSR.Modificar, out vErrorMsg);
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
                    gucReglasDeContabilizacion.SetNavigatorValuesFromForm();
                    insReglasDeContabilizacion[0] = gucReglasDeContabilizacion.CurrentInstance;
                    if (LibNotifier.Confirm(this, "¿Está seguro de que desea eliminar este registro.?", this.Title)) {
                        insModel.DeleteRecord(insReglasDeContabilizacion[0]);
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


    } //End of class frmReglasDeContabilizacionInput.xaml

} //End of namespace Galac.Saw.Uil.Contabilizacion

