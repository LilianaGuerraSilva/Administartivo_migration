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
    /// Lógica de interacción para GSAnticipoStt.xaml
    /// </summary>
    internal partial class GSAnticipoStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        AnticipoStt _CurrentInstance;
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
        internal AnticipoStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        #endregion //Propiedades
        
        #region Constructores

        public GSAnticipoStt() {
            InitializeComponent();
            
      

            InitializeEvents();
          

            cmbTipoComprobanteDeAnticipoAImprimir.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eComprobanteConCheque)));
        }
        #endregion //Constructores
        
        #region Metodos Generados

        private string RequiredFields() {
            return "Cuenta Bancaria";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (AnticipoStt)initInstance;
            Title = "Anticipo";
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
            _CurrentInstance.CuentaBancariaAnticipo = txtCuentaBancariaAnticipo.Text;
            _CurrentInstance.SugerirConsecutivoAnticipoAsBool = chkSugerirConsecutivoAnticipo.IsChecked.Value;
            _CurrentInstance.ConceptoBancarioAnticipoCobrado = txtConceptoBancarioAnticipoCobrado.Text;
            _CurrentInstance.ConceptoBancarioReversoAnticipoCobrado = txtConceptoBancarioReversoAnticipoCobrado.Text;
            _CurrentInstance.NombrePlantillaReciboDeAnticipoCobrado = txtNombrePlantillaReciboDeAnticipoCobrado.Text;
            _CurrentInstance.NombrePlantillaReciboDeAnticipoPagado = txtNombrePlantillaReciboDeAnticipoPagado.Text;
            _CurrentInstance.ConceptoBancarioReversoAnticipoPagado = txtConceptoBancarioReversoAnticipoPagado.Text;
            _CurrentInstance.TipoComprobanteDeAnticipoAImprimirAsEnum = (eComprobanteConCheque) cmbTipoComprobanteDeAnticipoAImprimir.SelectedItemToInt();
            _CurrentInstance.ConceptoBancarioAnticipoPagado = txtConceptoBancarioAnticipoPagado.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            txtCuentaBancariaAnticipo.Text = _CurrentInstance.CuentaBancariaAnticipo;
            chkSugerirConsecutivoAnticipo.IsChecked = _CurrentInstance.SugerirConsecutivoAnticipoAsBool;
            txtConceptoBancarioAnticipoCobrado.Text = _CurrentInstance.ConceptoBancarioAnticipoCobrado;
            txtConceptoBancarioReversoAnticipoCobrado.Text = _CurrentInstance.ConceptoBancarioReversoAnticipoCobrado;
            txtNombrePlantillaReciboDeAnticipoCobrado.Text = _CurrentInstance.NombrePlantillaReciboDeAnticipoCobrado;
            txtNombrePlantillaReciboDeAnticipoPagado.Text = _CurrentInstance.NombrePlantillaReciboDeAnticipoPagado;
            txtConceptoBancarioReversoAnticipoPagado.Text = _CurrentInstance.ConceptoBancarioReversoAnticipoPagado;
            cmbTipoComprobanteDeAnticipoAImprimir.SelectItem(_CurrentInstance.TipoComprobanteDeAnticipoAImprimirAsEnum);
            txtConceptoBancarioAnticipoPagado.Text = _CurrentInstance.ConceptoBancarioAnticipoPagado;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtCuentaBancariaAnticipo.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaBancariaAnticipo_Validating);
            this.txtConceptoBancarioAnticipoCobrado.Validating += new System.ComponentModel.CancelEventHandler(txtConceptoBancarioAnticipoCobrado_Validating);
            this.txtConceptoBancarioReversoAnticipoCobrado.Validating += new System.ComponentModel.CancelEventHandler(txtConceptoBancarioReversoAnticipoCobrado_Validating);
            this.txtConceptoBancarioReversoAnticipoPagado.Validating += new System.ComponentModel.CancelEventHandler(txtConceptoBancarioReversoAnticipoPagado_Validating);
            this.cmbTipoComprobanteDeAnticipoAImprimir.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoComprobanteDeAnticipoAImprimir_Validating);
            this.txtConceptoBancarioAnticipoPagado.Validating += new System.ComponentModel.CancelEventHandler(txtConceptoBancarioAnticipoPagado_Validating);
            this.btnBuscarRpxAnticipoCob.Click += new RoutedEventHandler(btnBuscarRpxAnticipoCob_Click);
            this.btnBuscarRpxAnticipoPag.Click += new RoutedEventHandler(btnBuscarRpxAnticipoPag_Click);
            this.txtNombrePlantillaReciboDeAnticipoCobrado.Validating += new CancelEventHandler(txtNombrePlantillaReciboDeAnticipoCobrado_Validating);
            this.txtNombrePlantillaReciboDeAnticipoPagado.Validating += new CancelEventHandler(txtNombrePlantillaReciboDeAnticipoPagado_Validating);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
        }

        
        private void OnUnloaded(object sender, RoutedEventArgs e) {
            SetNavigatorValuesFromForm();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            try {
                if (DataContext == null) {
                    return;
                }
                //this.Action = ((GSSettValueByCompany)((Grid)((HeaderedContentControl)((StackPanel)((Grid)((ContentPresenter)this.TemplatedParent).Parent).Parent).TemplatedParent).Parent).Parent).Action;
                InitializeControl(DataContext, Action, null);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaBancariaAnticipo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCuentaBancariaAnticipo.Text)==0) {
                    txtCuentaBancariaAnticipo.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_CuentaBancaria_B1.Codigo=" + txtCuentaBancariaAnticipo.Text + LibText.ColumnSeparator();
                vParamsInitializationList += "Gv_CuentaBancaria_B1.EsCajaChica=" + LibConvert.BoolToSN(false);
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "Gv_CuentaBancaria_B1.ConsecutivoCompania=" + clsGlobalValues.Mfc.GetInt("Compania");
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (clsSettValueByCompanyList.ChooseCuentaBancaria(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCuentaBancariaAnticipo.Text = insParse.GetString(0, "Codigo", "");
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

        void txtConceptoBancarioAnticipoCobrado_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if(CancelValidations) {
                    return;
                }
                if(LibString.Len(txtConceptoBancarioAnticipoCobrado.Text) == 0) {
                    txtConceptoBancarioAnticipoCobrado.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Adm.Gv_ConceptoBancario_B1.Codigo=" + txtConceptoBancarioAnticipoCobrado.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if(clsSettValueByCompanyList.ChooseConceptoBancario(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtConceptoBancarioAnticipoCobrado.Text = insParse.GetString(0, "Codigo", "");
                } else {
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

        void txtConceptoBancarioReversoAnticipoCobrado_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if(CancelValidations) {
                    return;
                }
                if(LibString.Len(txtConceptoBancarioReversoAnticipoCobrado.Text) == 0) {
                    txtConceptoBancarioReversoAnticipoCobrado.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Adm.Gv_ConceptoBancario_B1.Codigo=" + txtConceptoBancarioReversoAnticipoCobrado.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if(clsSettValueByCompanyList.ChooseConceptoBancario(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtConceptoBancarioReversoAnticipoCobrado.Text = insParse.GetString(0, "Codigo", "");
                } else {
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

        void txtConceptoBancarioReversoAnticipoPagado_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtConceptoBancarioReversoAnticipoPagado.Text)==0) {
                    txtConceptoBancarioReversoAnticipoPagado.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Adm.Gv_ConceptoBancario_B1.Codigo=" + txtConceptoBancarioReversoAnticipoPagado.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if(clsSettValueByCompanyList.ChooseConceptoBancario(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtConceptoBancarioReversoAnticipoPagado.Text = insParse.GetString(0, "Codigo", "");
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

        void cmbTipoComprobanteDeAnticipoAImprimir_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoComprobanteDeAnticipoAImprimir.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }
        
        void txtConceptoBancarioAnticipoPagado_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtConceptoBancarioAnticipoPagado.Text)==0) {
                    txtConceptoBancarioAnticipoPagado.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Adm.Gv_ConceptoBancario_B1.Codigo=" + txtConceptoBancarioAnticipoPagado.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if(clsSettValueByCompanyList.ChooseConceptoBancario(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtConceptoBancarioAnticipoPagado.Text = insParse.GetString(0, "Codigo", "");
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

        void btnBuscarRpxAnticipoPag_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxAnticipoPag();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void btnBuscarRpxAnticipoCob_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxAnticipoCob();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void BuscarRpxAnticipoPag() {
            string paramBusqueda = "rpx de Anticipo Pagado (*.rpx)|*Comprobante*Anticipo*Pagado*.rpx";
            txtNombrePlantillaReciboDeAnticipoPagado.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }

        private void BuscarRpxAnticipoCob() {
            string paramBusqueda = "rpx de Anticipo Cobrado (*.rpx)|*Comprobante*Anticipo*Cobrado*.rpx";
            txtNombrePlantillaReciboDeAnticipoCobrado.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }
      

        void txtNombrePlantillaReciboDeAnticipoPagado_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaReciboDeAnticipoPagado.Text) == 0) {
                txtNombrePlantillaReciboDeAnticipoPagado.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaReciboDeAnticipoPagado.Text)) {
                BuscarRpxAnticipoPag();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaReciboDeAnticipoPagado.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaReciboDeAnticipoPagado.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }

        void txtNombrePlantillaReciboDeAnticipoCobrado_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaReciboDeAnticipoCobrado.Text) == 0) {
                txtNombrePlantillaReciboDeAnticipoCobrado.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaReciboDeAnticipoCobrado.Text)) {
                BuscarRpxAnticipoCob();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaReciboDeAnticipoCobrado.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaReciboDeAnticipoCobrado.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }


    } //End of class GSAnticipoStt.xaml
} //End of namespace Galac.Saw.Uil.SttDef

