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
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Uil.Inventario.Input {
    /// <summary>
    /// Lógica de interacción para GSAlmacen.xaml
    /// </summary>
    internal partial class GSAlmacen: UserControl {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        Almacen _CurrentInstance;
        ILibView _CurrentModel;
        int _ConsecutivoCliente;
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
        internal Almacen CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        internal ILibView CurrentModel {
            get { return _CurrentModel; }
            set { _CurrentModel = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSAlmacen() {
            InitializeComponent();
            InitializeEvents();
            cmbTipoDeAlmacen.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeAlmacen)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Consecutivo, Codigo";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, ILibView initModel, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (Almacen)initInstance;
            _CurrentModel = initModel;
            Title = initModel.MessageName;
            Action = initAction;
            ExtendedAction = initExtendedAction;
            LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar });
            if (Action == eAccionSR.Insertar) {
                SetFormValuesFromNavigator(true);
            } else {
                SetFormValuesFromNavigator(false);
            }
            LibApiAwp.EnableControl(txtCodigo, Action == eAccionSR.Insertar);
            if (LibConvert.SNToBool(((clsAlmacenIpl)_CurrentModel).AppMemoryInfo.GlobalValuesGetString("RecordName", "AsociaCentroDeCostoyAlmacen"))
                &&
                LibConvert.SNToBool(((clsAlmacenIpl)_CurrentModel).AppMemoryInfo.GlobalValuesGetString("RecordName", "UsaCentroDeCostos"))) {
                txtCodigoCc.Visibility = System.Windows.Visibility.Visible;
                txtDescripcion.Visibility = System.Windows.Visibility.Visible;
                lblCodigoCc.Visibility = System.Windows.Visibility.Visible;
            }
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
            }
        }

        internal void SetNavigatorValuesFromForm() {
            _CurrentInstance.Codigo = txtCodigo.Text;
            _CurrentInstance.NombreAlmacen = txtNombreAlmacen.Text;
            _CurrentInstance.TipoDeAlmacenAsEnum = (eTipoDeAlmacen) cmbTipoDeAlmacen.SelectedItemToInt();
            _CurrentInstance.ConsecutivoCliente = _ConsecutivoCliente;
            _CurrentInstance.CodigoCc = txtCodigoCc.Text;
            _CurrentInstance.Descripcion = txtDescripcion.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            if (valClearRecord) {
                _CurrentModel.Clear(_CurrentInstance);
            }
            ClearControl();
            txtCodigo.Text = _CurrentInstance.Codigo;
            txtNombreAlmacen.Text = _CurrentInstance.NombreAlmacen;
            cmbTipoDeAlmacen.SelectItem(_CurrentInstance.TipoDeAlmacenAsEnum);
            if (Action != eAccionSR.Insertar) {
                lneNombreCliente.Content = BuscaNombreCliente(_CurrentInstance.ConsecutivoCliente); ;
            } else {
                lneNombreCliente.Content = _CurrentInstance.NombreCliente;
            }
            txtCodigoCc.Text = _CurrentInstance.CodigoCc;
            txtDescripcion.Text = _CurrentInstance.Descripcion;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtCodigo.Validating += new System.ComponentModel.CancelEventHandler(txtCodigo_Validating);
            this.cmbTipoDeAlmacen.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoDeAlmacen_Validating);
            this.txtCodigoDelCliente.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoDelCliente_Validating);
            this.txtCodigoCc.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoCc_Validating);
            this.txtDescripcion.Validating += new System.ComponentModel.CancelEventHandler(txtDescripcion_Validating);
        }
  

        void txtCodigo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsAlmacenIpl insAlmacenIpl = new clsAlmacenIpl(((clsAlmacenIpl)CurrentModel).AppMemoryInfo, ((clsAlmacenIpl)CurrentModel).Mfc);
                if (!insAlmacenIpl.IsValidCodigo(Action, txtCodigo.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insAlmacenIpl.Information.ToString(), _CurrentModel.MessageName);
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

        void cmbTipoDeAlmacen_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoDeAlmacen.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCodigoDelCliente_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCodigoDelCliente.Text) == 0) {
                    txtCodigoDelCliente.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "dbo.Gv_Cliente_B1.Codigo=" + txtCodigoDelCliente.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "ConsecutivoCompania=" + _CurrentInstance.ConsecutivoCompania;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Inventario.clsAlmacenList.ChooseCliente(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCodigoDelCliente.Text = insParse.GetString(0, "Codigo", "");
                    _ConsecutivoCliente = insParse.GetInt(0, "Consecutivo", 0);
                    lneNombreCliente.Content = insParse.GetString(0, "Nombre", "");
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

        private string BuscaNombreCliente(int valConsecutivoCliente) {
            string vResult = "";
            string vParamsInitializationList;
            string vParamsFixedList = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            vParamsInitializationList = "dbo.Gv_Cliente_B1.Consecutivo=" + valConsecutivoCliente.ToString() + LibText.ColumnSeparator();
            vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
            vParamsFixedList = "ConsecutivoCompania=" + _CurrentInstance.ConsecutivoCompania;
            vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
            XmlDocument XmlProperties = new XmlDocument();
            if (Galac.Saw.Uil.Inventario.clsAlmacenList.ChooseCliente(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                txtCodigoDelCliente.Text = insParse.GetString(0, "Codigo", "");
                _ConsecutivoCliente = insParse.GetInt(0, "Consecutivo", 0);
                vResult = insParse.GetString(0, "Nombre", "");
            }
            return vResult;
        }

        void txtCodigoCc_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCodigoCc.Text)==0) {
                    txtCodigoCc.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_CentroDeCostos_B1.Codigo=" + txtCodigoCc.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                int vConsecutivoPeriodo = LibConvert.ToInt(((clsAlmacenIpl)_CurrentModel).AppMemoryInfo.GlobalValuesGetInt("RecordName", "ConsecutivoPeriodo"));
                vParamsFixedList = "ConsecutivoPeriodo=" + vConsecutivoPeriodo;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Inventario.clsAlmacenList.ChooseCentroDeCostos(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCodigoCc.Text = insParse.GetString(0, "Codigo", "");
                    txtDescripcion.Text = insParse.GetString(0, "Descripcion", "");
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

        void txtDescripcion_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtDescripcion.Text)==0) {
                    txtDescripcion.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_CentroDeCostos_B1.Descripcion=" + txtDescripcion.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                vParamsFixedList = "ConsecutivoPeriodo=" + _CurrentInstance.ConsecutivoCompania;
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Inventario.clsAlmacenList.ChooseCentroDeCostos(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtDescripcion.Text = insParse.GetString(0, "Descripcion", "");
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


    } //End of class GSAlmacen.xaml

} //End of namespace Galac.Saw.Uil.Inventario

