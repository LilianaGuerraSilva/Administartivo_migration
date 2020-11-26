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
    /// Lógica de interacción para GSCompaniaStt.xaml
    /// </summary>
    internal partial class GSCompaniaStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        CompaniaStt _CurrentInstance;
        
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
        internal CompaniaStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSCompaniaStt() {
            InitializeComponent();
            InitializeEvents();
            cmbFormaDeEscogerCompania.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eFormaDeEscogerCompania)));
            cmbTipoDeAgrupacionParaLibrosDeVenta.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeAgrupacionParaLibrosDeVenta)));
            cmbTipoNegocio.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoNegocio)));
            
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Forma De Escoger Compania";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (CompaniaStt)initInstance;            
            Title = "Compañia";
            Action = initAction;
            ExtendedAction = initExtendedAction;
            //LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar });
            Action = ((GSSettValueByCompany)((Grid)((HeaderedContentControl)((StackPanel)((Grid)((ContentPresenter)this.TemplatedParent).Parent).Parent).TemplatedParent).Parent).Parent).Action;
            if (Action == eAccionSR.Insertar) {
                SetFormValuesFromNavigator(true);
            } else {
                SetFormValuesFromNavigator(false);
            }

            SetLookAndFeelForCurrentRecord();
            Tool.AllDisabled(gwMain.Children, Action);
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
                if (LibGalac.Aos.DefGen.LibDefGen.ProgramIsInAdvancedWay) {
                    chkIntegracionRIS.Visibility = System.Windows.Visibility.Visible;
                    label3.Visibility = System.Windows.Visibility.Visible;
                }
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);
                if (!insSettValueByCompanyIpl.PuedeUsarTallerMecanico()) {
                    lblTipoNegocio.Visibility = System.Windows.Visibility.Hidden;
                    cmbTipoNegocio.Visibility = System.Windows.Visibility.Hidden;
                }
                if (!LibGalac.Aos.DefGen.LibDefGen.ProgramIsInAdvancedWay) {
                    label3.Visibility = System.Windows.Visibility.Hidden;
                    chkIntegracionRIS.Visibility = System.Windows.Visibility.Hidden;
                }
                if (!LibConvert.ToBool(clsGlobalValues.AppMemoryInfo.GlobalValuesGetBool("Parametros", "UsaModuloDeContabilidad"))) {
                    lblFechaDeInicioContabilizacion.Visibility = System.Windows.Visibility.Hidden;
                    dtpFechaDeInicioContabilizacion.Visibility = System.Windows.Visibility.Hidden;
                }


                HabilitarFormaDeEscogerCompania();
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.FormaDeEscogerCompaniaAsEnum = (eFormaDeEscogerCompania) cmbFormaDeEscogerCompania.SelectedItemToInt();
            _CurrentInstance.EscogerCompaniaAlEntrarAsBool = chkEscogerCompaniaAlEntrar.IsChecked.Value;
            _CurrentInstance.FechaDeInicioContabilizacion = dtpFechaDeInicioContabilizacion.Date;
            _CurrentInstance.EsAsociadoEnCtaDeParticipacionAsBool = chkEsAsociadoEnCtaDeParticipacion.IsChecked.Value;
            _CurrentInstance.VerificarDocumentoSinContabilizarAsBool = chkVerificarDocumentoSinContabilizar.IsChecked.Value;
            _CurrentInstance.TipoDeAgrupacionParaLibrosDeVentaAsEnum = (eTipoDeAgrupacionParaLibrosDeVenta) cmbTipoDeAgrupacionParaLibrosDeVenta.SelectedItemToInt();
            _CurrentInstance.IntegracionRISAsBool = chkIntegracionRIS.IsChecked.Value;
            _CurrentInstance.FechaMinimaIngresarDatos = dtpFechaMinimaIngresarDatos.Date;
            _CurrentInstance.AutorellenaResumenDiarioAsBool = chkAutorellenaResumenDiario.IsChecked.Value;
            _CurrentInstance.TipoNegocioAsEnum = (eTipoNegocio) cmbTipoNegocio.SelectedItemToInt();
            clsGlobalValues.SetPropertyvalue("Parametros", "FechaDeInicioContabilizacion", dtpFechaDeInicioContabilizacion.Date);
                
            
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            cmbFormaDeEscogerCompania.SelectItem(_CurrentInstance.FormaDeEscogerCompaniaAsEnum);
            chkEscogerCompaniaAlEntrar.IsChecked = _CurrentInstance.EscogerCompaniaAlEntrarAsBool;
            dtpFechaDeInicioContabilizacion.Date = _CurrentInstance.FechaDeInicioContabilizacion;
            chkEsAsociadoEnCtaDeParticipacion.IsChecked = _CurrentInstance.EsAsociadoEnCtaDeParticipacionAsBool;
            chkVerificarDocumentoSinContabilizar.IsChecked = _CurrentInstance.VerificarDocumentoSinContabilizarAsBool;
            cmbTipoDeAgrupacionParaLibrosDeVenta.SelectItem(_CurrentInstance.TipoDeAgrupacionParaLibrosDeVentaAsEnum);
            chkIntegracionRIS.IsChecked = _CurrentInstance.IntegracionRISAsBool;
            dtpFechaMinimaIngresarDatos.Date = _CurrentInstance.FechaMinimaIngresarDatos;
            chkAutorellenaResumenDiario.IsChecked = _CurrentInstance.AutorellenaResumenDiarioAsBool;
            cmbTipoNegocio.SelectItem(_CurrentInstance.TipoNegocioAsEnum);
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.cmbFormaDeEscogerCompania.Validating += new System.ComponentModel.CancelEventHandler(cmbFormaDeEscogerCompania_Validating);
            this.cmbTipoDeAgrupacionParaLibrosDeVenta.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoDeAgrupacionParaLibrosDeVenta_Validating);
            this.cmbTipoNegocio.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoNegocio_Validating);
            this.chkEscogerCompaniaAlEntrar.Click += new RoutedEventHandler(chkEscogerCompaniaAlEntrar_Click);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
            this.dtpFechaDeInicioContabilizacion.Validating += new EventHandler<LibGalac.Aos.UI.WpfControls.DatePickerCancelEventArgs>(dtpFechaDeInicioContabilizacion_Validating);

             
        }

        void dtpFechaDeInicioContabilizacion_Validating(object sender, LibGalac.Aos.UI.WpfControls.DatePickerCancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);
                if (!insSettValueByCompanyIpl.IsValidaFechaDeInicioContabilizacion(dtpFechaDeInicioContabilizacion.Date)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettValueByCompanyIpl.Information.ToString(), Title);
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

        void chkEscogerCompaniaAlEntrar_Click(object sender, RoutedEventArgs e) {
            try {
                HabilitarFormaDeEscogerCompania();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void HabilitarFormaDeEscogerCompania() {
            if ((bool)chkEscogerCompaniaAlEntrar.IsChecked) {
                cmbFormaDeEscogerCompania.IsEnabled = true;
            } else {
                cmbFormaDeEscogerCompania.IsEnabled = false;
            }
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


        void cmbFormaDeEscogerCompania_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbFormaDeEscogerCompania.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbTipoDeAgrupacionParaLibrosDeVenta_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoDeAgrupacionParaLibrosDeVenta.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbTipoNegocio_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoNegocio.ValidateTextInCombo();
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


    } //End of class GSCompaniaStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

