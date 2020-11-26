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
    /// Lógica de interacción para GSRetencionIVAStt.xaml
    /// </summary>
    internal partial class GSRetencionIVAStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        RetencionIVAStt _CurrentInstance;
        
        
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
        internal RetencionIVAStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSRetencionIVAStt() {
            InitializeComponent();
            InitializeEvents();
            cmbEnDondeRetenerIVA.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eDondeSeEfectuaLaRetencionIVA)));
            cmbFormaDeReiniciarElNumeroDeComprobanteRetIVA.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eFormaDeReiniciarComprobanteRetIVA)));            
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (RetencionIVAStt)initInstance;

            Title = "Retención";
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
                bool PuedoUsarOpcionesDeContribuyenteEspecial = clsGlobalValues.AppMemoryInfo.GlobalValuesGetBool("Parametros", "PuedoUsarOpcionesDeContribuyenteEspecial");
                if (!PuedoUsarOpcionesDeContribuyenteEspecial) {
                    groupboxMain.IsEnabled = false;
                }
                lblImprimirComprobanteDeRetIVA.Content = "Primer Número de Comprobante de Retención de " + clsGlobalValues.AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA") + " ...";
                if (RetieneEnPago()) {
                    lblUsaMismoNumeroCompRetTodasCxP.Visibility = System.Windows.Visibility.Visible;
                    chkUsaMismoNumeroCompRetTodasCxP.Visibility = System.Windows.Visibility.Visible;
                } else {
                    lblUsaMismoNumeroCompRetTodasCxP.Visibility = System.Windows.Visibility.Hidden;
                    chkUsaMismoNumeroCompRetTodasCxP.Visibility = System.Windows.Visibility.Hidden;
                    chkUsaMismoNumeroCompRetTodasCxP.IsChecked = false;
                }
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.EnDondeRetenerIVAAsEnum = (eDondeSeEfectuaLaRetencionIVA) cmbEnDondeRetenerIVA.SelectedItemToInt();
            _CurrentInstance.UsaMismoNumeroCompRetTodasCxPAsBool = chkUsaMismoNumeroCompRetTodasCxP.IsChecked.Value;
            _CurrentInstance.PrimerNumeroComprobanteRetIVA = LibConvert.ToInt(txtPrimerNumeroComprobanteRetIVA.Value);
            _CurrentInstance.FormaDeReiniciarElNumeroDeComprobanteRetIVAAsEnum = (eFormaDeReiniciarComprobanteRetIVA) cmbFormaDeReiniciarElNumeroDeComprobanteRetIVA.SelectedItemToInt();
            _CurrentInstance.ImprimirComprobanteDeRetIVAAsBool = chkImprimirComprobanteDeRetIVA.IsChecked.Value;
            _CurrentInstance.NumeroDeCopiasComprobanteRetencionIVA = LibConvert.ToInt(txtNumeroDeCopiasComprobanteRetencionIVA.Value);
            _CurrentInstance.UnComprobanteDeRetIVAPorHojaAsBool = chkUnComprobanteDeRetIVAPorHoja.IsChecked.Value;
            _CurrentInstance.NombrePlantillaComprobanteDeRetIVA = txtNombrePlantillaComprobanteDeRetIVA.Text;
            _CurrentInstance.GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACeroAsBool = chkGenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACero.IsChecked.Value;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            cmbEnDondeRetenerIVA.SelectItem(_CurrentInstance.EnDondeRetenerIVAAsEnum);
            chkUsaMismoNumeroCompRetTodasCxP.IsChecked = _CurrentInstance.UsaMismoNumeroCompRetTodasCxPAsBool;
            txtPrimerNumeroComprobanteRetIVA.Value = _CurrentInstance.PrimerNumeroComprobanteRetIVA;
            cmbFormaDeReiniciarElNumeroDeComprobanteRetIVA.SelectItem(_CurrentInstance.FormaDeReiniciarElNumeroDeComprobanteRetIVAAsEnum);
            chkImprimirComprobanteDeRetIVA.IsChecked = _CurrentInstance.ImprimirComprobanteDeRetIVAAsBool;
            txtNumeroDeCopiasComprobanteRetencionIVA.Value = _CurrentInstance.NumeroDeCopiasComprobanteRetencionIVA;
            chkUnComprobanteDeRetIVAPorHoja.IsChecked = _CurrentInstance.UnComprobanteDeRetIVAPorHojaAsBool;
            txtNombrePlantillaComprobanteDeRetIVA.Text = _CurrentInstance.NombrePlantillaComprobanteDeRetIVA;
            chkGenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACero.IsChecked = _CurrentInstance.GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACeroAsBool;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.cmbFormaDeReiniciarElNumeroDeComprobanteRetIVA.Validating += new System.ComponentModel.CancelEventHandler(cmbFormaDeReiniciarElNumeroDeComprobanteRetIVA_Validating);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
            this.cmbEnDondeRetenerIVA.SelectionChanged += new SelectionChangedEventHandler(cmbEnDondeRetenerIVA_SelectionChanged);
            this.txtPrimerNumeroComprobanteRetIVA.Validating += new CancelEventHandler(txtPrimerNumeroComprobanteRetIVA_Validating);
            this.btnBuscarRpxRetIVA.Click += new RoutedEventHandler(btnBuscarRpxRetIVA_Click);
            this.txtNombrePlantillaComprobanteDeRetIVA.Validating += new CancelEventHandler(txtNombrePlantillaComprobanteDeRetIVA_Validating);      
        }

        private void OnUnloaded(object sender, RoutedEventArgs e) {
            SetNavigatorValuesFromForm();
            if (Action == eAccionSR.Modificar) {
                if (clsGlobalValues.AppMemoryInfo.GlobalValuesGetBool("Parametros", "PuedoUsarOpcionesDeContribuyenteEspecial")) {
                    cmbEnDondeRetenerIVA.ValidateTextInCombo();
                    clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);
                    if (!insSettValueByCompanyIpl.EsValidaDondeRetenerIva(cmbEnDondeRetenerIVA.SelectedItemToEnum<eDondeSeEfectuaLaRetencionIVA>())) {
                        LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettValueByCompanyIpl.Information.ToString(), Title);
                    } else {
                        if (LibText.CleanSpacesToBothSides(insSettValueByCompanyIpl.Information.ToString()) != "") {
                            LibNotifier.Alert(LibApiAwp.GetWindow(sender), insSettValueByCompanyIpl.Information.ToString(), Title);
                        }
                    }
                    txtPrimerNumeroComprobanteRetIVA_Validating(txtPrimerNumeroComprobanteRetIVA, new CancelEventArgs());
                }
            }
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

        void cmbFormaDeReiniciarElNumeroDeComprobanteRetIVA_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbFormaDeReiniciarElNumeroDeComprobanteRetIVA.ValidateTextInCombo();                                
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);                
                if(!insSettValueByCompanyIpl.EsFormaDeReiniciarElNumeroDeComprobanteRetIVA(cmbFormaDeReiniciarElNumeroDeComprobanteRetIVA.SelectedItemToEnum<eFormaDeReiniciarComprobanteRetIVA>())) {
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

        private void RealizaLosCalculos() {
            if (Action != eAccionSR.Consultar && Action != eAccionSR.Eliminar) {
                //throw new NotImplementedException("Debe sobreescribir el metodo RealizaLosCalculos para su caso especifico. Si no lo requiere no lo invoque.");
            }
        }
        #endregion //Metodos Generados

        void txtPrimerNumeroComprobanteRetIVA_Validating(object sender, CancelEventArgs e) {
            try {                
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);
                if(!insSettValueByCompanyIpl.EsValidoPrimerNumeroComprobanteRetIVA(txtPrimerNumeroComprobanteRetIVA.Value)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettValueByCompanyIpl.Information.ToString(), Title);
                    txtPrimerNumeroComprobanteRetIVA.Value = insSettValueByCompanyIpl.DefaultPrimerNumeroComprobanteRetIVA() ;
                    e.Cancel = true;
                }

            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }

        }    

        void cmbEnDondeRetenerIVA_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                if (RetieneEnPago()) {
                    lblUsaMismoNumeroCompRetTodasCxP.Visibility = System.Windows.Visibility.Visible;
                    chkUsaMismoNumeroCompRetTodasCxP.Visibility = System.Windows.Visibility.Visible;
                } else {
                    lblUsaMismoNumeroCompRetTodasCxP.Visibility = System.Windows.Visibility.Hidden;
                    chkUsaMismoNumeroCompRetTodasCxP.Visibility = System.Windows.Visibility.Hidden;
                    chkUsaMismoNumeroCompRetTodasCxP.IsChecked = false;
                }
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void btnBuscarRpxRetIVA_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxRetIVA();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void BuscarRpxRetIVA() {
            string paramBusqueda = "rpx de Retención IVA (*.rpx)|*Retencion*IVA*.rpx";
            txtNombrePlantillaComprobanteDeRetIVA.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }

        void txtNombrePlantillaComprobanteDeRetIVA_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaComprobanteDeRetIVA.Text) == 0) {
                txtNombrePlantillaComprobanteDeRetIVA.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaComprobanteDeRetIVA.Text)) {
                BuscarRpxRetIVA();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaComprobanteDeRetIVA.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaComprobanteDeRetIVA.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }

        private bool RetieneEnPago() {
            bool vResult = false;
            if ((eDondeSeEfectuaLaRetencionIVA)cmbEnDondeRetenerIVA.SelectedItem == eDondeSeEfectuaLaRetencionIVA.Pago) {
                vResult = true;
            }
            return vResult;
        }
    } //End of class GSRetencionIVAStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

