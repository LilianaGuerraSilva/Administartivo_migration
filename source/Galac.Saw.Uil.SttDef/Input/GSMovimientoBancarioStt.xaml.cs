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
using WindForm = System.Windows.Forms;

namespace Galac.Saw.Uil.SttDef.Input {
    /// <summary>
    /// Lógica de interacción para GSMovimientoBancarioStt.xaml
    /// </summary>
    internal partial class GSMovimientoBancarioStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        MovimientoBancarioStt _CurrentInstance;
        internal int _ConsecutivoBeneficiario;

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
        internal MovimientoBancarioStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSMovimientoBancarioStt() {
            InitializeComponent();
            InitializeEvents();
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "ConceptoBancarioReversoSolicitudDePago, Concepto Reverso Cobranza , Nombre Plantilla Comprobante de PagoSueldo";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (MovimientoBancarioStt)initInstance;

            Title = "Movimiento Bancario";
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
                lblGenerarMovReversoSiAnulaPago.Visibility = System.Windows.Visibility.Hidden;
                chkGenerarMovReversoSiAnulaPago.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.MandarMensajeNumeroDeMovimientoBancarioAsBool = chkMandarMensajeNumeroDeMovimientoBancario.IsChecked.Value;
            _CurrentInstance.GenerarMovBancarioDesdeCobroAsBool = chkGenerarMovBancarioDesdeCobro.IsChecked.Value;
            _CurrentInstance.UsaCodigoConceptoBancarioEnPantallaAsBool = chkUsaCodigoConceptoBancarioEnPantalla.IsChecked.Value;
            _CurrentInstance.GenerarMovBancarioDesdePagoAsBool = chkGenerarMovBancarioDesdePago.IsChecked.Value;
            _CurrentInstance.NumCopiasComprobanteMovBancario = LibConvert.ToInt(txtNumCopiasComprobanteMovBancario.Value);
            _CurrentInstance.NombrePlantillaComprobanteDeMovBancario = txtNombrePlantillaComprobanteDeMovBancario.Text;
            _CurrentInstance.ConfirmarImpresionMovBancarioPorSeccionesAsBool = chkConfirmarImpresionMovBancarioPorSecciones.IsChecked.Value;
            _CurrentInstance.ImprimirCompContDespuesDeChequeMovBancarioAsBool = chkImprimirCompContDespuesDeChequeMovBancario.IsChecked.Value;
            _CurrentInstance.ImprimirComprobanteDeMovBancarioAsBool = chkImprimirComprobanteDeMovBancario.IsChecked.Value;
            _CurrentInstance.BeneficiarioGenerico = _ConsecutivoBeneficiario;
            _CurrentInstance.ConceptoBancarioReversoSolicitudDePago = txtConceptoBancarioReversoSolicitudDePago.Text;
            _CurrentInstance.NombrePlantillaComprobanteDePagoSueldo = txtNombrePlantillaComprobanteDePagoSueldo.Text;
            _CurrentInstance.GenerarMovReversoSiAnulaPagoAsBool = chkGenerarMovReversoSiAnulaPago.IsChecked.Value;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            chkMandarMensajeNumeroDeMovimientoBancario.IsChecked = _CurrentInstance.MandarMensajeNumeroDeMovimientoBancarioAsBool;
            chkGenerarMovBancarioDesdeCobro.IsChecked = _CurrentInstance.GenerarMovBancarioDesdeCobroAsBool;
            chkUsaCodigoConceptoBancarioEnPantalla.IsChecked = _CurrentInstance.UsaCodigoConceptoBancarioEnPantallaAsBool;
            chkGenerarMovBancarioDesdePago.IsChecked = _CurrentInstance.GenerarMovBancarioDesdePagoAsBool;
            txtNumCopiasComprobanteMovBancario.Value = _CurrentInstance.NumCopiasComprobanteMovBancario;
            txtNombrePlantillaComprobanteDeMovBancario.Text = _CurrentInstance.NombrePlantillaComprobanteDeMovBancario;
            chkConfirmarImpresionMovBancarioPorSecciones.IsChecked = _CurrentInstance.ConfirmarImpresionMovBancarioPorSeccionesAsBool;
            chkImprimirCompContDespuesDeChequeMovBancario.IsChecked = _CurrentInstance.ImprimirCompContDespuesDeChequeMovBancarioAsBool;
            chkImprimirComprobanteDeMovBancario.IsChecked = _CurrentInstance.ImprimirComprobanteDeMovBancarioAsBool;
            txtBeneficiarioGenerico.Text = BuscaCodigoBeneficiarioByConsecutivo(_CurrentInstance.BeneficiarioGenerico);
            txtConceptoBancarioReversoSolicitudDePago.Text = _CurrentInstance.ConceptoBancarioReversoSolicitudDePago;
            txtNombrePlantillaComprobanteDePagoSueldo.Text = _CurrentInstance.NombrePlantillaComprobanteDePagoSueldo;
            chkGenerarMovReversoSiAnulaPago.IsChecked = _CurrentInstance.GenerarMovReversoSiAnulaPagoAsBool;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtConceptoBancarioReversoSolicitudDePago.Validating += new System.ComponentModel.CancelEventHandler(txtConceptoBancarioReversoSolicitudDePago_Validating);
            this.txtNombrePlantillaComprobanteDePagoSueldo.Validating += new System.ComponentModel.CancelEventHandler(txtNombrePlantillaComprobanteDePagoSueldo_Validating);
            this.txtNombrePlantillaComprobanteDeMovBancario.Validating += new CancelEventHandler(txtNombrePlantillaComprobanteDeMovBancario_Validating);
            this.btnNombrePlantillaComprobanteDeMovBancario.Click += new RoutedEventHandler(btnNombrePlantillaComprobanteDeMovBancario_Click);
            this.btnNombrePlantillaComprobanteDePagoSueldo.Click +=new RoutedEventHandler(btnNombrePlantillaComprobanteDePagoSueldo_Click);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.txtBeneficiarioGenerico.Validating += new CancelEventHandler(txtBeneficiarioGenerico_Validating);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
        }

        void txtNombrePlantillaComprobanteDeMovBancario_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaComprobanteDeMovBancario.Text) == 0) {
                txtNombrePlantillaComprobanteDeMovBancario.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaComprobanteDeMovBancario.Text)) {
                BuscarRpxComprobanteDeMovBancario();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaComprobanteDeMovBancario.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaComprobanteDeMovBancario.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }

        void txtBeneficiarioGenerico_Validating(object sender, CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtBeneficiarioGenerico.Text) <= 0) {
                    txtBeneficiarioGenerico.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Saw.Gv_Beneficiario_B1.Codigo=" + txtBeneficiarioGenerico.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "Saw.Gv_Beneficiario_B1.ConsecutivoCompania=" + clsGlobalValues.Mfc.GetInt("Compania");
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (clsSettValueByCompanyList.ChooseBeneficiario(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtBeneficiarioGenerico.Text = insParse.GetString(0, "Codigo", "");
                    _ConsecutivoBeneficiario = insParse.GetInt(0, "Consecutivo", 0);
                } else {
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

        private string BuscaCodigoBeneficiarioByConsecutivo(int valConsecutivoBeneficiario) {
            string vResult = "";
            string vParamsInitializationList;
            string vParamsFixedList = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            vParamsInitializationList = "Saw.Gv_Beneficiario_B1.Consecutivo=" + valConsecutivoBeneficiario + LibText.ColumnSeparator();
            vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
            vParamsFixedList = "Saw.Gv_Beneficiario_B1.ConsecutivoCompania=" + clsGlobalValues.Mfc.GetInt("Compania");
            vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
            XmlDocument XmlProperties = new XmlDocument();
            if (clsSettValueByCompanyList.ChooseBeneficiario(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                vResult = insParse.GetString(0, "Codigo", "");
                _ConsecutivoBeneficiario = insParse.GetInt(0, "Consecutivo", 0);
            }
            return vResult;
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


        void txtConceptoBancarioReversoSolicitudDePago_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtConceptoBancarioReversoSolicitudDePago.Text)==0) {
                    txtConceptoBancarioReversoSolicitudDePago.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Adm.Gv_ConceptoBancario_B1.codigo=" + txtConceptoBancarioReversoSolicitudDePago.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "Adm.Gv_ConceptoBancario_B1.TipoStr=" + eIngresoEgreso.Ingreso;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (clsSettValueByCompanyList.ChooseConceptoBancario(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtConceptoBancarioReversoSolicitudDePago.Text = insParse.GetString(0, "Codigo", "");
                }else{
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


        void txtNombrePlantillaComprobanteDePagoSueldo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaComprobanteDePagoSueldo.Text) == 0) {
                txtNombrePlantillaComprobanteDePagoSueldo.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaComprobanteDePagoSueldo.Text)) {
                BuscarRpxComprobanteDePagoSueldo();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaComprobanteDePagoSueldo.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaComprobanteDePagoSueldo.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }

        private void RealizaLosCalculos() {
            if (Action != eAccionSR.Consultar && Action != eAccionSR.Eliminar) {
                //throw new NotImplementedException("Debe sobreescribir el metodo RealizaLosCalculos para su caso especifico. Si no lo requiere no lo invoque.");
            }
        }
        #endregion //Metodos Generados

        private void btnNombrePlantillaComprobanteDePagoSueldo_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxComprobanteDePagoSueldo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void btnNombrePlantillaComprobanteDeMovBancario_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxComprobanteDeMovBancario();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void BuscarRpxComprobanteDePagoSueldo() {
            string paramBusqueda = "rpx de Pago Sueldo (*.rpx)|*Pago*Sueldo*.rpx";
            txtNombrePlantillaComprobanteDePagoSueldo.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }

        private void BuscarRpxComprobanteDeMovBancario() {
            string paramBusqueda = "rpx de Movimiento Bancario (*.rpx)|*Impresion*Cheque*.rpx";
            txtNombrePlantillaComprobanteDeMovBancario.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }



    } //End of class GSMovimientoBancarioStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

