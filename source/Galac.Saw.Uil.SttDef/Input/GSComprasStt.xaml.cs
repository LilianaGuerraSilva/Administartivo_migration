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
    /// Lógica de interacción para GSComprasStt.xaml
    /// </summary>
    internal partial class GSComprasStt: UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        ComprasStt _CurrentInstance;

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
        internal ComprasStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }

        #endregion //Propiedades
        #region Constructores

        public GSComprasStt() {
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
            _CurrentInstance = (ComprasStt)initInstance;

            Title = "Compra";
            Action = initAction;
            ExtendedAction = initExtendedAction;
            //LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar });
            Action = ((GSSettValueByCompany)((Grid)((HeaderedContentControl)((StackPanel)((Grid)((ContentPresenter)this.TemplatedParent).Parent).Parent).TemplatedParent).Parent).Parent).Action;
            if(Action == eAccionSR.Insertar) {
                SetFormValuesFromNavigator(true);
            } else {
                SetFormValuesFromNavigator(false);
            }
            SetLookAndFeelForCurrentRecord();
            Tool.AllDisabled(gwMain.Children, Action);
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if(Action != eAccionSR.Insertar) {
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.GenerarCxPdesdeCompraAsBool = chkGenerarCxPdesdeCompra.IsChecked.Value;
            _CurrentInstance.ImprimirOrdenDeCompraAsBool = chkImprimirOrdenDeCompra.IsChecked.Value;
            _CurrentInstance.NombrePlantillaOrdenDeCompra = txtNombrePlantillaOrdenDeCompra.Text;
            _CurrentInstance.IvaEsCostoEnComprasAsBool = chkIvaEsCostoEnCompras.IsChecked.Value;
            _CurrentInstance.ImprimirCompraAlInsertarAsBool = chkImprimirCompraAlInsertar.IsChecked.Value;
            _CurrentInstance.NombrePlantillaCompra = txtNombrePlantillaCompra.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            chkGenerarCxPdesdeCompra.IsChecked = _CurrentInstance.GenerarCxPdesdeCompraAsBool;
            chkImprimirOrdenDeCompra.IsChecked = _CurrentInstance.ImprimirOrdenDeCompraAsBool;
            txtNombrePlantillaOrdenDeCompra.Text = _CurrentInstance.NombrePlantillaOrdenDeCompra;
            chkIvaEsCostoEnCompras.IsChecked = _CurrentInstance.IvaEsCostoEnComprasAsBool;
            chkImprimirCompraAlInsertar.IsChecked = _CurrentInstance.ImprimirCompraAlInsertarAsBool;
            txtNombrePlantillaCompra.Text = _CurrentInstance.NombrePlantillaCompra;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
            this.txtNombrePlantillaOrdenDeCompra.Validating += new CancelEventHandler(txtNombrePlantillaOrdenDeCompra_Validating);
            this.btnBuscarRpxOC.Click += new RoutedEventHandler(btnBuscarRpxOC_Click);
            this.txtNombrePlantillaCompra.Validating += new CancelEventHandler(txtNombrePlantillaCompra_Validating);
            this.btnBuscarRpxCompra.Click += new RoutedEventHandler(btnBuscarRpxCompra_Click);
        }

        private void OnUnloaded(object sender, RoutedEventArgs e) {
            SetNavigatorValuesFromForm();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            try {
                if(DataContext == null) {
                    return;
                }

                InitializeControl(DataContext, eAccionSR.Modificar, null);
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }


        private void RealizaLosCalculos() {
            if(Action != eAccionSR.Consultar && Action != eAccionSR.Eliminar) {
                //throw new NotImplementedException("Debe sobreescribir el metodo RealizaLosCalculos para su caso especifico. Si no lo requiere no lo invoque.");
            }
        }
        #endregion //Metodos Generados

        void txtNombrePlantillaOrdenDeCompra_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaOrdenDeCompra.Text) == 0) {
                txtNombrePlantillaOrdenDeCompra.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaOrdenDeCompra.Text)) {
                BuscarRpxOC();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaOrdenDeCompra.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaOrdenDeCompra.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }

        void btnBuscarRpxOC_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxOC();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void BuscarRpxOC() {
            string paramBusqueda = "rpx de Orden de Compra (*.rpx)|*Compra*.rpx";
            txtNombrePlantillaOrdenDeCompra.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }        

        void txtNombrePlantillaCompra_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaCompra.Text) == 0) {
                txtNombrePlantillaCompra.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaCompra.Text)) {
                BuscarRpxCompra();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaCompra.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaCompra.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }

        void btnBuscarRpxCompra_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxCompra();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void BuscarRpxCompra() {
            string paramBusqueda = "rpx de Compra (*.rpx)|*Compra*.rpx";
            txtNombrePlantillaCompra.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }  


    } //End of class GSComprasStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

