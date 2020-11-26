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
    /// Lógica de interacción para GSInventarioStt.xaml
    /// </summary>
    internal partial class GSInventarioStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        InventarioStt _CurrentInstance;
        
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
        internal InventarioStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSInventarioStt() {
            InitializeComponent();
            InitializeEvents();
            cmbPermitirSobregiro.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(ePermitirSobregiro)));
            cmbCantidadDeDecimales.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eCantidadDeDecimales)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Almacén genérico ...";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (InventarioStt)initInstance;
            
            Title = "Inventario";
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
                if ((bool)chkUsaAlmacen.IsChecked) {
                    LibApiAwp.EnableControl(lblUsarAlmacen, !(bool)chkUsaAlmacen.IsChecked);
                    LibApiAwp.EnableControl(chkUsaAlmacen, !(bool)chkUsaAlmacen.IsChecked);
                }
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.UsarBaseImponibleDiferenteA0Y100AsBool = chkUsarBaseImponibleDiferenteA0Y100.IsChecked.Value;
            _CurrentInstance.UsaAlmacenAsBool = chkUsaAlmacen.IsChecked.Value;
            _CurrentInstance.CodigoAlmacenGenerico = txtCodigoAlmacenGenerico.Text;
            _CurrentInstance.PermitirSobregiroAsEnum = (ePermitirSobregiro) cmbPermitirSobregiro.SelectedItemToInt();
            _CurrentInstance.ActivarFacturacionPorAlmacenAsBool = chkActivarFacturacionPorAlmacen.IsChecked.Value;
            _CurrentInstance.SinonimoGrupo = txtSinonimoGrupo.Text;
            _CurrentInstance.SinonimoTalla = txtSinonimoTalla.Text;
            _CurrentInstance.SinonimoColor = txtSinonimoColor.Text;
            _CurrentInstance.SinonimoSerial = txtSinonimoSerial.Text;
            _CurrentInstance.SinonimoRollo = txtSinonimoRollo.Text;
            _CurrentInstance.ImprimirTransferenciaAlInsertarAsBool = chkImprimirTransferenciaAlInsertar.IsChecked.Value;
            _CurrentInstance.CantidadDeDecimalesAsEnum = (eCantidadDeDecimales) cmbCantidadDeDecimales.SelectedItemToInt();
            _CurrentInstance.NombreCampoDefinibleInventario1 = txtNombreCampoDefinibleInventario1.Text;
            _CurrentInstance.NombreCampoDefinibleInventario2 = txtNombreCampoDefinibleInventario2.Text;
            _CurrentInstance.NombreCampoDefinibleInventario3 = txtNombreCampoDefinibleInventario3.Text;
            _CurrentInstance.NombreCampoDefinibleInventario4 = txtNombreCampoDefinibleInventario4.Text;
            _CurrentInstance.NombreCampoDefinibleInventario5 = txtNombreCampoDefinibleInventario5.Text;
            _CurrentInstance.AsociaCentroDeCostoyAlmacenAsBool = chkAsociaCentroDeCostoyAlmacen.IsChecked.Value;
            _CurrentInstance.AvisoDeReservasvencidasAsBool = chkAvisoDeReservasvencidas.IsChecked.Value;
            _CurrentInstance.VerificarStockAsBool = chkVerificarStock.IsChecked.Value;
            _CurrentInstance.ImprimeSerialRolloLuegoDeDescripArticuloAsBool = chkImprimeSerialRolloLuegoDeDescripArticulo.IsChecked.Value;
        }

        private int ObtieneConsecutivoAlmacenGenerico( string CodigoAlmacenGenerico) {
            int vResult = 1;


            return vResult;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            chkUsarBaseImponibleDiferenteA0Y100.IsChecked = _CurrentInstance.UsarBaseImponibleDiferenteA0Y100AsBool;
            chkUsaAlmacen.IsChecked = _CurrentInstance.UsaAlmacenAsBool;
            txtCodigoAlmacenGenerico.Text = _CurrentInstance.CodigoAlmacenGenerico;
            txtNombreAlmacenGenerico.Text = BuscaNombreAlmacenGenerico(txtCodigoAlmacenGenerico.Text);
            cmbPermitirSobregiro.SelectItem(_CurrentInstance.PermitirSobregiroAsEnum);
            chkActivarFacturacionPorAlmacen.IsChecked = _CurrentInstance.ActivarFacturacionPorAlmacenAsBool;
            txtSinonimoGrupo.Text = _CurrentInstance.SinonimoGrupo;
            txtSinonimoTalla.Text = _CurrentInstance.SinonimoTalla;
            txtSinonimoColor.Text = _CurrentInstance.SinonimoColor;
            txtSinonimoSerial.Text = _CurrentInstance.SinonimoSerial;
            txtSinonimoRollo.Text = _CurrentInstance.SinonimoRollo;
            chkImprimirTransferenciaAlInsertar.IsChecked = _CurrentInstance.ImprimirTransferenciaAlInsertarAsBool;
            cmbCantidadDeDecimales.SelectItem(_CurrentInstance.CantidadDeDecimalesAsEnum);
            txtNombreCampoDefinibleInventario1.Text = _CurrentInstance.NombreCampoDefinibleInventario1;
            txtNombreCampoDefinibleInventario2.Text = _CurrentInstance.NombreCampoDefinibleInventario2;
            txtNombreCampoDefinibleInventario3.Text = _CurrentInstance.NombreCampoDefinibleInventario3;
            txtNombreCampoDefinibleInventario4.Text = _CurrentInstance.NombreCampoDefinibleInventario4;
            txtNombreCampoDefinibleInventario5.Text = _CurrentInstance.NombreCampoDefinibleInventario5;
            chkAsociaCentroDeCostoyAlmacen.IsChecked = _CurrentInstance.AsociaCentroDeCostoyAlmacenAsBool;
            chkAvisoDeReservasvencidas.IsChecked = _CurrentInstance.AvisoDeReservasvencidasAsBool;
            chkVerificarStock.IsChecked = _CurrentInstance.VerificarStockAsBool;
            chkImprimeSerialRolloLuegoDeDescripArticulo.IsChecked = _CurrentInstance.ImprimeSerialRolloLuegoDeDescripArticuloAsBool;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtCodigoAlmacenGenerico.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoAlmacenGenerico_Validating);
            this.cmbPermitirSobregiro.Validating += new System.ComponentModel.CancelEventHandler(cmbPermitirSobregiro_Validating);
            this.cmbCantidadDeDecimales.Validating += new System.ComponentModel.CancelEventHandler(cmbCantidadDeDecimales_Validating);
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


        void txtCodigoAlmacenGenerico_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCodigoAlmacenGenerico.Text)==0) {
                    txtCodigoAlmacenGenerico.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_Almacen_B1.Codigo=" + txtCodigoAlmacenGenerico.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "Gv_Almacen_B1.ConsecutivoCompania=" + clsGlobalValues.Mfc.GetInt("Compania");
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (clsSettValueByCompanyList.ChooseAlmacen(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCodigoAlmacenGenerico.Text = insParse.GetString(0, "Codigo", "");
                    txtNombreAlmacenGenerico.Text = insParse.GetString(0, "NombreAlmacen", "");
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

        private string BuscaNombreAlmacenGenerico(string valCodigoAlmacenGenerico) {
            string vResult = "";
            string vParamsInitializationList;
            string vParamsFixedList = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            vParamsInitializationList = "Gv_Almacen_B1.Codigo=" + valCodigoAlmacenGenerico + LibText.ColumnSeparator();
            vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
            vParamsFixedList = "Gv_Almacen_B1.ConsecutivoCompania=" + clsGlobalValues.Mfc.GetInt("Compania");
            vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
            XmlDocument XmlProperties = new XmlDocument();
            if (clsSettValueByCompanyList.ChooseAlmacen(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                txtCodigoAlmacenGenerico.Text = insParse.GetString(0, "Codigo", "");
                vResult = insParse.GetString(0, "NombreAlmacen", "");
            }
            return vResult;
        }

        void cmbPermitirSobregiro_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbPermitirSobregiro.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbCantidadDeDecimales_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbCantidadDeDecimales.ValidateTextInCombo();
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


    } //End of class GSInventarioStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

