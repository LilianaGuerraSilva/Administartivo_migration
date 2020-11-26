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
    /// Lógica de interacción para GSCotizacionStt.xaml
    /// </summary>
    internal partial class GSCotizacionStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        CotizacionStt _CurrentInstance;
        
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
        internal CotizacionStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSCotizacionStt() {
            InitializeComponent();
            InitializeEvents();
            cmbCampoCodigoAlternativoDeArticulo.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eCampoCodigoAlternativoDeArticulo)));
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
            _CurrentInstance = (CotizacionStt)initInstance;
            
            Title = "Cotización";
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
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.NombrePlantillaCotizacion = txtNombrePlantillaCotizacion.Text;
            _CurrentInstance.DetalleProdCompCotizacionAsBool = chkDetalleProdCompCotizacion.IsChecked.Value;
            _CurrentInstance.UsaControlDespachoAsBool = chkUsaControlDespacho.IsChecked.Value;
            _CurrentInstance.LimpiezaDeCotizacionXFacturaAsBool = chkLimpiezaDeCotizacionXFactura.IsChecked.Value;
            _CurrentInstance.ValidarArticulosAlGenerarFacturaAsBool = chkValidarArticulosAlGenerarFactura.IsChecked.Value;
            _CurrentInstance.CampoCodigoAlternativoDeArticuloAsEnum = (eCampoCodigoAlternativoDeArticulo)cmbCampoCodigoAlternativoDeArticulo.SelectedItemToInt();
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            txtNombrePlantillaCotizacion.Text = _CurrentInstance.NombrePlantillaCotizacion;
            chkDetalleProdCompCotizacion.IsChecked = _CurrentInstance.DetalleProdCompCotizacionAsBool;
            chkUsaControlDespacho.IsChecked = _CurrentInstance.UsaControlDespachoAsBool;
            chkLimpiezaDeCotizacionXFactura.IsChecked = _CurrentInstance.LimpiezaDeCotizacionXFacturaAsBool;
            chkValidarArticulosAlGenerarFactura.IsChecked = _CurrentInstance.ValidarArticulosAlGenerarFacturaAsBool;
            cmbCampoCodigoAlternativoDeArticulo.SelectItem(_CurrentInstance.CampoCodigoAlternativoDeArticuloAsEnum);
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
            this.txtNombrePlantillaCotizacion.Validating += new CancelEventHandler(txtNombrePlantillaCotizacion_Validating);
            this.btnBuscarRpxCotizacion.Click += new RoutedEventHandler(btnBuscarRpxCotizacion_Click);
            this.cmbCampoCodigoAlternativoDeArticulo.Validating += new CancelEventHandler(cmbCampoCodigoAlternativoDeArticulo_Validating);
        }

        void cmbCampoCodigoAlternativoDeArticulo_Validating(object sender, CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbCampoCodigoAlternativoDeArticulo.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
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


        private void RealizaLosCalculos() {
            if (Action != eAccionSR.Consultar && Action != eAccionSR.Eliminar) {
                //throw new NotImplementedException("Debe sobreescribir el metodo RealizaLosCalculos para su caso especifico. Si no lo requiere no lo invoque.");
            }
        }
        #endregion //Metodos Generados

        void txtNombrePlantillaCotizacion_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaCotizacion.Text) == 0) {
                txtNombrePlantillaCotizacion.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaCotizacion.Text)) {
                BuscarRpxCotizacion();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaCotizacion.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaCotizacion.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }

        void btnBuscarRpxCotizacion_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxCotizacion();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void BuscarRpxCotizacion() {
            string paramBusqueda = "rpx de Cotización (*.rpx)|*Cotizacion*.rpx";
            txtNombrePlantillaCotizacion.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }

    } //End of class GSCotizacionStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

