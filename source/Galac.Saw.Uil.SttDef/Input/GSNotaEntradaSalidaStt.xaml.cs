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
    /// Lógica de interacción para GSNotaEntradaSalidaStt.xaml
    /// </summary>
    internal partial class GSNotaEntradaSalidaStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        NotaEntradaSalidaStt _CurrentInstance;
        
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
        internal NotaEntradaSalidaStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSNotaEntradaSalidaStt() {
            InitializeComponent();
            InitializeEvents();
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
            _CurrentInstance = (NotaEntradaSalidaStt)initInstance;

            Title = "Nota Entrada Salida";
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
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);
                if (insSettValueByCompanyIpl.EsSistemaParaIG()) {
                    label1.Visibility = System.Windows.Visibility.Hidden;
                    chkImprimirReporteAlIngresarNotaEntradaSalida.Visibility = System.Windows.Visibility.Hidden;
                    lblNombrePlantillaNotaEntradaSalida.Visibility = System.Windows.Visibility.Hidden;
                    txtNombrePlantillaNotaEntradaSalida.Visibility = System.Windows.Visibility.Hidden;
                    btnBuscarRpxNES.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.ImprimirReporteAlIngresarNotaEntradaSalidaAsBool = chkImprimirReporteAlIngresarNotaEntradaSalida.IsChecked.Value;
            _CurrentInstance.NombrePlantillaNotaEntradaSalida = txtNombrePlantillaNotaEntradaSalida.Text;
            _CurrentInstance.ImprimirNotaESconPrecioAsBool = chkImprimirNotaESconPrecio.IsChecked.Value;
            _CurrentInstance.NombrePlantillaCodigoDeBarras= txtNombrePlantillaCodigoDeBarras.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            chkImprimirReporteAlIngresarNotaEntradaSalida.IsChecked = _CurrentInstance.ImprimirReporteAlIngresarNotaEntradaSalidaAsBool;
            txtNombrePlantillaNotaEntradaSalida.Text = _CurrentInstance.NombrePlantillaNotaEntradaSalida;
            chkImprimirNotaESconPrecio.IsChecked = _CurrentInstance.ImprimirNotaESconPrecioAsBool;
            txtNombrePlantillaCodigoDeBarras.Text = _CurrentInstance.NombrePlantillaCodigoDeBarras;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
            this.txtNombrePlantillaNotaEntradaSalida.Validating += new CancelEventHandler(txtNombrePlantillaNotaEntradaSalida_Validating);
            this.btnBuscarRpxNES.Click += new RoutedEventHandler(btnBuscarRpxNES_Click);
            this.txtNombrePlantillaCodigoDeBarras.Validating += new CancelEventHandler(txtNombrePlantillaCodigoDeBarras_Validating);
            this.btnBuscarRpxImpresionCodigoDeBarras.Click += new RoutedEventHandler(btnBuscarRpxImpresionCodigoDeBarras_Click);
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
        
        void txtNombrePlantillaNotaEntradaSalida_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaNotaEntradaSalida.Text) == 0) {
                txtNombrePlantillaNotaEntradaSalida.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaNotaEntradaSalida.Text)) {
                BuscarRpxNotaEntradaSalida();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaNotaEntradaSalida.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaNotaEntradaSalida.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }
        
        void btnBuscarRpxNES_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxNotaEntradaSalida();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void BuscarRpxNotaEntradaSalida() {
            string paramBusqueda = "rpx de Nota E/S (*.rpx)|*Nota*ES*.rpx";
            txtNombrePlantillaNotaEntradaSalida.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }

        void txtNombrePlantillaCodigoDeBarras_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaCodigoDeBarras.Text) == 0) {
                txtNombrePlantillaCodigoDeBarras.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaCodigoDeBarras.Text)) {
                BuscarRpxImpresionCodigoDeBarras();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaCodigoDeBarras.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaCodigoDeBarras.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }

        void btnBuscarRpxImpresionCodigoDeBarras_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxImpresionCodigoDeBarras();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void BuscarRpxImpresionCodigoDeBarras() {
            string paramBusqueda = "rpx de Código de Barras (*.rpx)|*Codigo*Barras*.rpx";
            txtNombrePlantillaCodigoDeBarras.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }


    } //End of class GSNotaEntradaSalidaStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

