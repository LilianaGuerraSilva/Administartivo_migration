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
using Galac.Adm.Ccl.CajaChica;
using Galac.Adm.Uil.CajaChica.Input;

namespace Galac.Adm.Uil.CajaChica {
    /// <summary>
    /// Lógica de interacción para frmRendicionesInput.xaml
    /// </summary>
    public partial class frmReposicionInput:LibFrmInputBase { 
        #region Variables
        IList<Rendicion> insRendicion;
        ILibView insModel;
        string _Result = "";
        #endregion //Variables
        #region Propiedades
        public string ResultadoOperacion {
            get { return _Result; }
            set { _Result = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public frmReposicionInput(string initRecordUserName, eAccionSR initAction, string initExtendedAction)
            : base(initRecordUserName, initAction, initExtendedAction) {
            InitializeComponent();
            try {
                gucRendicion.Action = initAction;
                gucRendicion.ExtendedAction = initExtendedAction;
                gucRendicion.CancelValidations = (initAction == eAccionSR.Consultar || initAction == eAccionSR.Eliminar || initAction == eAccionSR.Anular || initAction == eAccionSR.Cerrar);
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
            gucRendicion.ClearControl();
        }

        protected override void DisableAllFields() {
            gucRendicion.DisableControl();
        }

        public bool InitLookAndFeelAndSetValues(IList<Rendicion> refRendicion, ILibView refModel) {
            bool vResult = true;
            insModel = refModel;
            if (Action == eAccionSR.Insertar) {
                insRendicion = refRendicion;
            } else {
                if (refRendicion == null) {
                    throw new ApplicationException("Falta información de inicialización al invocar la ventana de Rendicion.");
                } else {
                    insRendicion = refRendicion;
                }
            }
            gucRendicion.InitializeControl(insRendicion[0], insModel, Action, ExtendedAction);
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action == eAccionSR.Insertar) {
                lneLastModified.Visibility = Visibility.Hidden;
                lblTitleLastModified.Visibility = Visibility.Hidden;
            } else {
                lneLastModified.SetContent(insRendicion[0].NombreOperador, insRendicion[0].FechaUltimaModificacion);
            }
        }

        protected override void SetCancelValidations(bool valValue) {
            base.SetCancelValidations(valValue);
            gucRendicion.CancelValidations = valValue;
        }

        protected override bool StoreRecord(bool valStoreAndExit) {
            string vErrorMsg;
            bool vResult = false;
            switch (Action) {
                case eAccionSR.Insertar:
                    gucRendicion.SetNavigatorValuesFromForm();
                    insRendicion[0] = gucRendicion.CurrentInstance;
                    insRendicion[0].Numero = LibConvert.ToStr(insModel.NextSequential("Numero"));
                    vResult = insModel.InsertRecord(insRendicion[0], out vErrorMsg);
                    if (vResult) {
                        gucRendicion.SetFormValuesFromNavigator(true);
                        gucRendicion.InitializeControl(insRendicion[0], insModel, Action, ExtendedAction);
                        MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                    } else {
                        LibNotifier.ValidationError(this, vErrorMsg, insModel.MessageName);
                        
                    }
                    break;
                case eAccionSR.Modificar:
                    gucRendicion.SetNavigatorValuesFromForm();
                    insRendicion[0] = gucRendicion.CurrentInstance;
                    vResult = insModel.UpdateRecord(insRendicion[0], eAccionSR.Modificar, out vErrorMsg);
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
                    gucRendicion.SetNavigatorValuesFromForm();
                    insRendicion[0] = gucRendicion.CurrentInstance;
                    if (LibNotifier.Confirm(this, "¿Está seguro de que desea eliminar este registro.?", this.Title)) {
                        insModel.DeleteRecord(insRendicion[0]);
                    }
                    this.Close();
                    break;
                case eAccionSR.Cerrar:
                    gucRendicion.SetNavigatorValuesFromForm();
                    insRendicion[0] = gucRendicion.CurrentInstance;
                    if (LibNotifier.Confirm(this, "¿Está seguro de que desea cerrar este registro.?", this.Title)) {
                            vResult = insModel.SpecializedUpdateRecord(insRendicion[0], eAccionSR.Cerrar.ToString(), out vErrorMsg);
                            ResultadoOperacion = ((clsReposicionIpl)insModel).ResultadoOperacion;
                        if (vResult) {
                         if (valStoreAndExit) {
                             Action = eAccionSR.Salir;
                             //this.Close();
                             ((LibFrmSearch)Owner).Close();
                             
                         }
                     } else {
                         if (ResultadoOperacion.Equals("1")) {
                             LibApiAwp.EnableControl(gucRendicion.DetalleRendicionUc.txtNumeroDocumento, true);
                             LibApiAwp.EnableControl(gucRendicion.DetalleRendicionUc.txtCodigoProveedor, true);
                             LibApiAwp.EnableControl(gucRendicion.DetalleRendicionUc.dtpFecha, true);
                             gucRendicion.DetalleRendicionUc.txtNumeroDocumento.Background = new SolidColorBrush(Colors.White);
                             gucRendicion.DetalleRendicionUc.txtCodigoProveedor.Background = new SolidColorBrush(Colors.White);
                             gucRendicion.DetalleRendicionUc.dtpFecha.Background = new SolidColorBrush(Colors.White);
                             gucRendicion.tabControl.SelectedItem = gucRendicion.tbiSeccionGastos;

                             gucRendicion.DetalleRendicionUc.txtNumeroDocumento.Focus();
                         } else if (ResultadoOperacion.Equals("0")) {
                             gucRendicion.tabControl.SelectedItem = gucRendicion.tbiSeccionCierre;
                         }
                         ResultadoOperacion = string.Empty;

                            LibNotifier.ValidationError(this, vErrorMsg, insModel.MessageName);
                     }
                     if (!valStoreAndExit) {
                         MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                     }
                    }
                    break;
                case eAccionSR.Anular:
                    gucRendicion.SetNavigatorValuesFromForm();
                    insRendicion[0] = gucRendicion.CurrentInstance;
                    if (LibNotifier.Confirm(this, "¿Está seguro de que desea anular este registro con fecha de anulacion "
                        + insRendicion[0].FechaAnulacion.ToShortDateString() + "?", this.Title)) {
                        vResult = insModel.SpecializedUpdateRecord(insRendicion[0], eAccionSR.Anular.ToString(), out vErrorMsg);
                        ResultadoOperacion = ((clsReposicionIpl)insModel).ResultadoOperacion;
                        if (vResult) {
                            if (valStoreAndExit) {
                                Action = eAccionSR.Salir;
                                //this.Close();
                                ((LibFrmSearch)Owner).Close();
                            }
                        } else {
                            gucRendicion.tbiSeccionAnular.BringIntoView();
                            gucRendicion.dtpFechaAnulacion.Focus();
                            LibNotifier.ValidationError(this, vErrorMsg, insModel.MessageName);
                        }
                        if (!valStoreAndExit) {
                            MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                        }
                    }
                    break;
                case eAccionSR.Contabilizar :
                    gucRendicion.SetNavigatorValuesFromForm();
                    insRendicion[0] = gucRendicion.CurrentInstance;
                    if (LibNotifier.Confirm(this, "¿Está seguro de que desea contabilizar este registro?"
                        , this.Title)) {
                        vResult = insModel.SpecializedUpdateRecord(insRendicion[0], Action.ToString(), out vErrorMsg);
                        ResultadoOperacion = ((clsReposicionIpl)insModel).ResultadoOperacion;
                        if (vResult) {
                                if (valStoreAndExit) {
                                    Action = eAccionSR.Salir;
                                    ((LibFrmSearch)Owner).Close();
                                }
                            } else {
                                LibNotifier.ValidationError(this, vErrorMsg, insModel.MessageName);
                            }
                            if (!valStoreAndExit) {
                                 MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                        }
                    }
                    break;
                case eAccionSR.ReImprimir:
                    gucRendicion.SetNavigatorValuesFromForm();
                    insRendicion[0] = gucRendicion.CurrentInstance;
                    if (LibNotifier.Confirm(this, "¿Está seguro de que desea ReImprimir este registro?"
                        , this.Title)) {
                        vResult = insModel.SpecializedUpdateRecord(insRendicion[0], Action.ToString(), out vErrorMsg);
                        ResultadoOperacion = ((clsReposicionIpl)insModel).ResultadoOperacion;
                        if (vResult) {
                            if (valStoreAndExit) {
                                Action = eAccionSR.Salir;
                                ((LibFrmSearch)Owner).Close();
                            }
                        } else {
                            LibNotifier.ValidationError(this, vErrorMsg, insModel.MessageName);
                        }
                        if (!valStoreAndExit) {
                            MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                        }
                    }
                    break;



                default:
                    LibNotifier.Warning(this, "Falta Programarlo para esta acción");
                    break;
            }
            return vResult;
         
        }
        #endregion //Metodos Generados

        }



} //End of namespace Galac.Saw.Uil.Rendicion

