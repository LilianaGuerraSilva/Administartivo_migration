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
    /// Lógica de interacción para GSCobranzasStt.xaml
    /// </summary>
    internal partial class GSCobranzasStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        CobranzasStt _CurrentInstance;
        
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
        internal CobranzasStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSCobranzasStt() {
            InitializeComponent();
            InitializeEvents();
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Concepto Reverso de Cobranza";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (CobranzasStt)initInstance;
            
            Title = "Cobranza";
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
            _CurrentInstance.UsarZonaCobranzaAsBool = chkUsarZonaCobranza.IsChecked.Value;
            _CurrentInstance.SugerirConsecutivoEnCobranzaAsBool = chkSugerirConsecutivoEnCobranza.IsChecked.Value;
            _CurrentInstance.ConceptoReversoCobranza = txtConceptoReversoCobranza.Text;
            _CurrentInstance.ImprimirCombrobanteAlIngresarCobranzaAsBool = chkImprimirCombrobanteAlIngresarCobranza.IsChecked.Value;
            _CurrentInstance.NombrePlantillaCompobanteCobranza = txtNombrePlantillaCompobanteCobranza.Text;
            _CurrentInstance.AsignarComisionDeVendedorEnCobranzaAsBool = chkAsignarComisionDeVendedorEnCobranza.IsChecked.Value;
            _CurrentInstance.CambiarCobradorVendedorAsBool = chkCambiarCobradorVendedor.IsChecked.Value;
            _CurrentInstance.BloquearNumeroCobranzaAsBool = chkBloquearNumeroCobranza.IsChecked.Value;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            chkUsarZonaCobranza.IsChecked = _CurrentInstance.UsarZonaCobranzaAsBool;
            chkSugerirConsecutivoEnCobranza.IsChecked = _CurrentInstance.SugerirConsecutivoEnCobranzaAsBool;
            txtConceptoReversoCobranza.Text = _CurrentInstance.ConceptoReversoCobranza;
            chkImprimirCombrobanteAlIngresarCobranza.IsChecked = _CurrentInstance.ImprimirCombrobanteAlIngresarCobranzaAsBool;
            txtNombrePlantillaCompobanteCobranza.Text = _CurrentInstance.NombrePlantillaCompobanteCobranza;
            chkAsignarComisionDeVendedorEnCobranza.IsChecked = _CurrentInstance.AsignarComisionDeVendedorEnCobranzaAsBool;
            chkCambiarCobradorVendedor.IsChecked = _CurrentInstance.CambiarCobradorVendedorAsBool;
            chkBloquearNumeroCobranza.IsChecked = _CurrentInstance.BloquearNumeroCobranzaAsBool;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtConceptoReversoCobranza.Validating += new System.ComponentModel.CancelEventHandler(txtConceptoReversoCobranza_Validating);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
            this.txtNombrePlantillaCompobanteCobranza.Validating += new CancelEventHandler(txtNombrePlantillaCompobanteCobranza_Validating);
            this.BtnBuscarRpxComprobanteCobranza.Click += new RoutedEventHandler(BtnBuscarRpxComprobanteCobranza_Click);
            this.chkImprimirCombrobanteAlIngresarCobranza.Click += new RoutedEventHandler(chkImprimirCombrobanteAlIngresarCobranza_Click);
        }

        void chkImprimirCombrobanteAlIngresarCobranza_Click(object sender, RoutedEventArgs e) {
            if ((bool)chkImprimirCombrobanteAlIngresarCobranza.IsChecked && LibText.IsNullOrEmpty(txtNombrePlantillaCompobanteCobranza.Text)) {
                txtNombrePlantillaCompobanteCobranza.Text = "rpxComprobanteDeCobro";
            } else if (!(bool)chkImprimirCombrobanteAlIngresarCobranza.IsChecked) {
                txtNombrePlantillaCompobanteCobranza.Text = "";
            }
        }

        void txtNombrePlantillaCompobanteCobranza_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaCompobanteCobranza.Text) == 0) {
                txtNombrePlantillaCompobanteCobranza.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaCompobanteCobranza.Text)) {
                BuscarRpxCobranza();
            } 
            //else {
            //    if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaCompobanteCobranza.Text)) {
            //        MessageBox.Show("El RPX " + txtNombrePlantillaCompobanteCobranza.Text + ", en " + this.Title + ", no EXISTE.");
            //        e.Cancel = true;
            //    }
            //}
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


        void txtConceptoReversoCobranza_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtConceptoReversoCobranza.Text)==0) {
                    txtConceptoReversoCobranza.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Adm.Gv_ConceptoBancario_B1.codigo=" + txtConceptoReversoCobranza.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "Adm.Gv_ConceptoBancario_B1.TipoStr=" + eIngresoEgreso.Egreso;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (clsSettValueByCompanyList.ChooseConceptoBancario(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtConceptoReversoCobranza.Text = insParse.GetString(0, "Codigo", "");
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

        private void RealizaLosCalculos() {
            if (Action != eAccionSR.Consultar && Action != eAccionSR.Eliminar) {
                //throw new NotImplementedException("Debe sobreescribir el metodo RealizaLosCalculos para su caso especifico. Si no lo requiere no lo invoque.");
            }
        }
        #endregion //Metodos Generados

        private void BuscarRpxCobranza() {
            string paramBusqueda = "rpx de Cobranza (*.rpx)|*Cobr*.rpx";
            txtNombrePlantillaCompobanteCobranza.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }   

        void BtnBuscarRpxComprobanteCobranza_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxCobranza();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }



    } //End of class GSCobranzasStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

