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
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Uil.SttDef.Input {
    /// <summary>
    /// Lógica de interacción para GSVendedorStt.xaml
    /// </summary>
    internal partial class GSVendedorStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        VendedorStt _CurrentInstance;
        
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
        internal VendedorStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSVendedorStt() {
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
            _CurrentInstance = (VendedorStt)initInstance;

            Title = "Vendedor";
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
            _CurrentInstance.UsaCodigoVendedorEnPantallaAsBool = (bool)chkUsaCodigoVendedorEnPantalla.IsChecked;
            _CurrentInstance.CodigoGenericoVendedor = txtCodigoGenericoVendedor.Text;
            _CurrentInstance.LongitudCodigoVendedor = LibConvert.ToInt(txtLongitudCodigoVendedor.Value);
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            chkUsaCodigoVendedorEnPantalla.IsChecked = _CurrentInstance.UsaCodigoVendedorEnPantallaAsBool;
            txtCodigoGenericoVendedor.Text = _CurrentInstance.CodigoGenericoVendedor;
            txtLongitudCodigoVendedor.Value = _CurrentInstance.LongitudCodigoVendedor;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtCodigoGenericoVendedor.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoGenericoVendedor_Validating);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
            this.txtLongitudCodigoVendedor.Validating +=new CancelEventHandler(txtLongitudCodigoVendedor_Validating);
            this.Loaded += new RoutedEventHandler(GSVendedorStt_Loaded);
        }

        void GSVendedorStt_Loaded(object sender, RoutedEventArgs e) {
            try {
                bool buscarVendedor = BuscaVendedor();
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


        void txtCodigoGenericoVendedor_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCodigoGenericoVendedor.Text)==0) {
                    txtCodigoGenericoVendedor.Text = "*";
                }
                //string vParamsInitializationList;
                //string vParamsFixedList = "";
                //LibSearch insLibSearch = new LibSearch();
                //List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                //List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                //vParamsInitializationList = "Gv_Vendedor_B1.Codigo=" + txtCodigoGenericoVendedor.Text + LibText.ColumnSeparator();
                //vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                //vParamsFixedList = "Gv_Vendedor_B1.ConsecutivoCompania=" + clsGlobalValues.Mfc.GetInt("Compania");
                //vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                //XmlDocument XmlProperties = new XmlDocument();
                //if (clsSettValueByCompanyList.ChooseVendedor(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                //    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                //    txtCodigoGenericoVendedor.Text = insParse.GetString(0, "Codigo", "");
                //    txtNombreVendedor.Text = insParse.GetString(0, "Nombre", "");
                //}else{
                //    e.Cancel = true;
                //}
                if (!BuscaVendedor()) {
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

        void txtLongitudCodigoVendedor_Validating(object sender, CancelEventArgs e) {
            try {
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);                                
                if(!insSettValueByCompanyIpl.EsValidaLongitudCodigoVendedor(txtLongitudCodigoVendedor.Value)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettValueByCompanyIpl.Information.ToString(), Title);
                    txtLongitudCodigoVendedor.Value = insSettValueByCompanyIpl.DefaultLongitudCodigoVendedor();
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

        private bool BuscaVendedor() {
            bool vResult;
            string vParamsInitializationList;
            string vParamsFixedList = "";
            vResult = false;
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            vParamsInitializationList = "Gv_Vendedor_B1.Codigo=" + txtCodigoGenericoVendedor.Text + LibText.ColumnSeparator();
            vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
            vParamsFixedList = "Gv_Vendedor_B1.ConsecutivoCompania=" + clsGlobalValues.Mfc.GetInt("Compania");
            vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
            XmlDocument XmlProperties = new XmlDocument();
            if (clsSettValueByCompanyList.ChooseVendedor(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                txtCodigoGenericoVendedor.Text = insParse.GetString(0, "Codigo", "");
                txtNombreVendedor.Text = insParse.GetString(0, "Nombre", "");
                vResult = true;
            } else {
                vResult = false;
            }
            return vResult;
        }

    } //End of class GSVendedorStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

