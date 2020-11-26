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
using LibGalac.Aos.Uil;using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Uil.SttDef.Input {
    /// <summary>
    /// Lógica de interacción para GSClienteStt.xaml
    /// </summary>
    internal partial class GSClienteStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        ClienteStt _CurrentInstance;
        
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
        internal ClienteStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSClienteStt() {
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
            _CurrentInstance = (ClienteStt)initInstance;
            
            Title = "Cliente";
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
                this.lblLongitudCodigoCliente.Visibility = System.Windows.Visibility.Hidden;
                this.txtLongitudCodigoCliente.Visibility = System.Windows.Visibility.Hidden;
                LibApiAwp.EnableControl(lblRellenaCerosAlaIzquierda, LibGalac.Aos.DefGen.LibDefGen.ProgramIsInAdvancedWay);
                LibApiAwp.EnableControl(chkRellenaCerosAlaIzquierda, LibGalac.Aos.DefGen.LibDefGen.ProgramIsInAdvancedWay);
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.CodigoGenericoCliente = txtCodigoGenericoCliente.Text;
            _CurrentInstance.LongitudCodigoCliente = LibConvert.ToInt(txtLongitudCodigoCliente.Value);
            _CurrentInstance.AvisoDeClienteConDeudaAsBool = chkAvisoDeClienteConDeuda.IsChecked.Value;
            _CurrentInstance.MontoApartirDelCualEnviarAvisoDeuda = txtMontoApartirDelCualEnviarAvisoDeuda.Value;
            _CurrentInstance.UsaCodigoClienteEnPantallaAsBool = chkUsaCodigoClienteEnPantalla.IsChecked.Value;
            _CurrentInstance.BuscarClienteXRifAlFacturarAsBool = chkBuscarClienteXRifAlFacturar.IsChecked.Value;
            _CurrentInstance.ColocarEnFacturaElVendedorAsinagoAlClienteAsBool = chkColocarEnFacturaElVendedorAsinagoAlCliente.IsChecked.Value;
            _CurrentInstance.ImprimirDatosClienteEnCompFiscalAsBool = chkImprimirDatosClienteEnCompFiscal.IsChecked.Value;
            _CurrentInstance.AvisoDeFacturacionMenorAsBool = chkAvisoDeFacturacionMenor.IsChecked.Value;
            _CurrentInstance.NombreCampoDefinibleCliente1 = txtNombreCampoDefinibleCliente1.Text;
            _CurrentInstance.RellenaCerosAlaIzquierdaAsBool = chkRellenaCerosAlaIzquierda.IsChecked.Value;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            txtCodigoGenericoCliente.Text = _CurrentInstance.CodigoGenericoCliente;
            txtNombreGenericoCliente.Text = BuscaNombreGenericoCliente(txtCodigoGenericoCliente.Text);
            txtLongitudCodigoCliente.Value = _CurrentInstance.LongitudCodigoCliente;
            chkAvisoDeClienteConDeuda.IsChecked = _CurrentInstance.AvisoDeClienteConDeudaAsBool;
            txtMontoApartirDelCualEnviarAvisoDeuda.Value = _CurrentInstance.MontoApartirDelCualEnviarAvisoDeuda;
            chkUsaCodigoClienteEnPantalla.IsChecked = _CurrentInstance.UsaCodigoClienteEnPantallaAsBool;
            chkBuscarClienteXRifAlFacturar.IsChecked = _CurrentInstance.BuscarClienteXRifAlFacturarAsBool;
            chkColocarEnFacturaElVendedorAsinagoAlCliente.IsChecked = _CurrentInstance.ColocarEnFacturaElVendedorAsinagoAlClienteAsBool;
            chkImprimirDatosClienteEnCompFiscal.IsChecked = _CurrentInstance.ImprimirDatosClienteEnCompFiscalAsBool;
            chkAvisoDeFacturacionMenor.IsChecked = _CurrentInstance.AvisoDeFacturacionMenorAsBool;
            txtNombreCampoDefinibleCliente1.Text = _CurrentInstance.NombreCampoDefinibleCliente1;
            chkRellenaCerosAlaIzquierda.IsChecked = _CurrentInstance.RellenaCerosAlaIzquierdaAsBool;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtCodigoGenericoCliente.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoGenericoCliente_Validating);
            this.txtLongitudCodigoCliente.Validating += new CancelEventHandler(txtLongitudCodigoCliente_Validating);
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


        void txtCodigoGenericoCliente_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCodigoGenericoCliente.Text) == 0) {
                    txtCodigoGenericoCliente.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_Cliente_B1.codigo=" + txtCodigoGenericoCliente.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "Gv_Cliente_B1.ConsecutivoCompania=" + clsGlobalValues.Mfc.GetInt("Compania");
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (clsSettValueByCompanyList.ChooseCliente(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCodigoGenericoCliente.Text = insParse.GetString(0, "Codigo", "");
                    txtNombreGenericoCliente.Text = insParse.GetString(0, "Nombre", "");
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

        private string BuscaNombreGenericoCliente(string valCodigoGenericoCliente) {
            string vResult = "";
            string vParamsInitializationList;
            string vParamsFixedList = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            vParamsInitializationList = "Gv_Cliente_B1.codigo=" + valCodigoGenericoCliente + LibText.ColumnSeparator();
            vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
            vParamsFixedList = "Gv_Cliente_B1.ConsecutivoCompania=" + clsGlobalValues.Mfc.GetInt("Compania");
            vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
            XmlDocument XmlProperties = new XmlDocument();
            if (clsSettValueByCompanyList.ChooseCliente(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                txtCodigoGenericoCliente.Text = insParse.GetString(0, "Codigo", "");
                vResult = insParse.GetString(0, "Nombre", "");
            }
        return vResult;
        }

        private void RealizaLosCalculos() {
            if (Action != eAccionSR.Consultar && Action != eAccionSR.Eliminar) {
                //throw new NotImplementedException("Debe sobreescribir el metodo RealizaLosCalculos para su caso especifico. Si no lo requiere no lo invoque.");
            }
        }
        #endregion //Metodos Generados

        void txtLongitudCodigoCliente_Validating(object sender, CancelEventArgs e) {
            try {
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);                                
                if(!insSettValueByCompanyIpl.EsValidaLongitudCodigoCliente(txtLongitudCodigoCliente.Value)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettValueByCompanyIpl.Information.ToString(), Title);
                    txtLongitudCodigoCliente.Value = insSettValueByCompanyIpl.DefaultLongitudCodigoCliente();
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

    } //End of class GSClienteStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

