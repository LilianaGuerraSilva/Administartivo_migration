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
using Galac.Adm.Ccl.GestionCompras;
using Galac.Comun.Ccl.TablasLey;

namespace Galac.Adm.Uil.GestionCompras.Input {
    /// <summary>
    /// Lógica de interacción para GSProveedor.xaml
    /// </summary>
    internal partial class GSProveedor: UserControl {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        Proveedor _CurrentInstance;
        ILibView _CurrentModel;
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
        internal Proveedor CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        internal ILibView CurrentModel {
            get { return _CurrentModel; }
            set { _CurrentModel = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSProveedor() {
            InitializeComponent();
            InitializeEvents();
            cmbTipoDePersona.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipodePersonaRetencion)));
            cmbTipoDeProveedorDeLibrosFiscales.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeProveedorDeLibrosFiscales)));
            cmbPorcentajeRetencionIVA.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(ePorcentajeDeRetencionDeIVA)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Código, Consecutivo, Nombre Proveedor, Codigo Retencion Usual";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
            lblValidarRIF.Content = "";
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, ILibView initModel, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (Proveedor)initInstance;
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
            LibApiAwp.EnableControl(txtCodigoProveedor, Action == eAccionSR.Insertar);
            AllDisabled(gwMain.Children, Action);
            lblValidarRIF.Content = "";
            if (Action == eAccionSR.Consultar || Action == eAccionSR.Eliminar) {
                frameDatos.IsEnabled = false;
                frameISLR.IsEnabled = false;
                frameImpMunicipales.IsEnabled = false;
                framePago.IsEnabled = false;
                frameReglas.IsEnabled = false;
            }
            if (Action == eAccionSR.Modificar)
                cmbTipoDePersona.IsEnabled = false;

            txtBeneficiario.IsEnabled = false;
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action == eAccionSR.Insertar) {
                cmbTipoDePersona.SelectedIndex = 0;
                cmbTipoDeProveedorDeLibrosFiscales.SelectedIndex = 0;
                cmbPorcentajeRetencionIVA.SelectedIndex = 1;
                lblCuentaContableAnticipo.Content = "";
                lblCuentaContableCxP.Content = "";
                lblCuentaContableGastos.Content = "";
                lblValidarRIF.Content = "";
            }
            if (!LibConvert.SNToBool(((clsProveedorIpl)_CurrentModel).AppMemoryInfo.GlobalValuesGetString("RecordName", "UsarOpcionesDeContribuyenteEspecial"))) {
                frameDatosFiscales.Visibility = System.Windows.Visibility.Hidden;
            }
            if (!LibConvert.SNToBool(((clsProveedorIpl)_CurrentModel).AppMemoryInfo.GlobalValuesGetString("RecordName", "ValidarRifEnLaWeb"))) {
                btnValidaRIF.Visibility = System.Windows.Visibility.Hidden;
                lblValidarRIF.Visibility = System.Windows.Visibility.Hidden;
            }
            if (!LibConvert.SNToBool(((clsProveedorIpl)_CurrentModel).AppMemoryInfo.GlobalValuesGetString("RecordName", "RetieneImpuestoMunicipal"))) {
                frameImpMunicipales.Visibility = System.Windows.Visibility.Hidden;
            }
            if (!LibConvert.SNToBool(((clsProveedorIpl)_CurrentModel).AppMemoryInfo.GlobalValuesGetString("RecordName", "UsaRetencion"))) {
                frameISLR.Visibility = System.Windows.Visibility.Hidden;
            }
            if (!LibConvert.SNToBool(((clsProveedorIpl)_CurrentModel).AppMemoryInfo.GlobalValuesGetString("RecordName", "AccesoCaracteristicaDeContabilidadActiva"))) {
                tab2.Visibility = System.Windows.Visibility.Hidden;
                frameReglas.Visibility = System.Windows.Visibility.Hidden;
            }
            if (Action == eAccionSR.Modificar) {
                txtBeneficiario.IsEnabled = (chkUsarBeneficiarioImpCheq.IsChecked.Value) ? true : false;
            }
        }

        internal void SetNavigatorValuesFromForm() {
            _CurrentInstance.CodigoProveedor = txtCodigoProveedor.Text;
            _CurrentInstance.NombreProveedor = txtNombreProveedor.Text;
            _CurrentInstance.Contacto = txtContacto.Text;
            _CurrentInstance.NumeroRIF = txtNumeroRIF.Text;
            _CurrentInstance.NumeroNIT = txtNumeroNIT.Text;
            _CurrentInstance.TipoDePersonaAsEnum = (eTipodePersonaRetencion) cmbTipoDePersona.SelectedItemToInt();
            if (txtCodigoRetencionUsual.Text == "") {
                txtCodigoRetencionUsual.Text = "NORET";
            }
            _CurrentInstance.CodigoRetencionUsual = txtCodigoRetencionUsual.Text;
            _CurrentInstance.Telefonos = txtTelefonos.Text;
            _CurrentInstance.Direccion = txtDireccion.Text;
            _CurrentInstance.Fax = txtFax.Text;
            _CurrentInstance.Email = txtEmail.Text;
            _CurrentInstance.TipodeProveedor = txtTipodeProveedor.Text;
            _CurrentInstance.TipoDeProveedorDeLibrosFiscalesAsEnum = (eTipoDeProveedorDeLibrosFiscales) cmbTipoDeProveedorDeLibrosFiscales.SelectedItemToInt();
            _CurrentInstance.PorcentajeRetencionIVA = LibConvert.ToInt(LibEnumHelper.GetDescription((ePorcentajeDeRetencionDeIVA)cmbPorcentajeRetencionIVA.SelectedItem));
            _CurrentInstance.CuentaContableCxP = txtCuentaContableCxP.Text;
            _CurrentInstance.CuentaContableGastos = txtCuentaContableGastos.Text;
            _CurrentInstance.CuentaContableAnticipo = txtCuentaContableAnticipo.Text;
            _CurrentInstance.CodigoLote = "";
            _CurrentInstance.Beneficiario = txtBeneficiario.Text;
            _CurrentInstance.UsarBeneficiarioImpCheqAsBool = chkUsarBeneficiarioImpCheq.IsChecked.Value;
            _CurrentInstance.TipoDocumentoIdentificacionAsEnum = (eTipoDocumentoIdentificacion.OtrosTiposdeDocumentos);
            _CurrentInstance.EsAgenteDeRetencionIvaAsBool = false;
            _CurrentInstance.Nombre = "";
            _CurrentInstance.ApellidoPaterno = "";
            _CurrentInstance.ApellidoMaterno = "";
            _CurrentInstance.NumeroCuentaBancaria = txtNumeroCuentaBancaria.Text;
            _CurrentInstance.CodigoContribuyente = txtCodigoContribuyente.Text;
            _CurrentInstance.ConsecutivoPeriodo = LibConvert.ToInt(((clsProveedorIpl)_CurrentModel).AppMemoryInfo.GlobalValuesGetInt("RecordName", "ConsecutivoPeriodo"));
            _CurrentInstance.AccesoCaracteristicaDeContabilidadActiva = LibConvert.SNToBool(((clsProveedorIpl)_CurrentModel).AppMemoryInfo.GlobalValuesGetString("RecordName", "AccesoCaracteristicaDeContabilidadActiva"));
            _CurrentInstance.UsaAuxiliares = LibConvert.SNToBool(((clsProveedorIpl)_CurrentModel).AppMemoryInfo.GlobalValuesGetString("RecordName", "UsaAuxiliares"));
			_CurrentInstance.NumeroRUC = txtNumeroRUC.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            if (valClearRecord) {
                _CurrentModel.Clear(_CurrentInstance);
            }
            ClearControl();
            txtCodigoProveedor.Text = _CurrentInstance.CodigoProveedor;
            txtNombreProveedor.Text = _CurrentInstance.NombreProveedor;
            txtContacto.Text = _CurrentInstance.Contacto;
            txtNumeroRIF.Text = _CurrentInstance.NumeroRIF;
            txtNumeroNIT.Text = _CurrentInstance.NumeroNIT;
            cmbTipoDePersona.SelectItem(_CurrentInstance.TipoDePersonaAsEnum);
            txtCodigoRetencionUsual.Text = _CurrentInstance.CodigoRetencionUsual;
            lblDescripcionRetencion.Content = LibText.IsNullOrEmpty(txtCodigoRetencionUsual.Text) ? "" : GetDescripcionRetencionUsual();
            txtTelefonos.Text = _CurrentInstance.Telefonos;
            txtDireccion.Text = _CurrentInstance.Direccion;
            txtFax.Text = _CurrentInstance.Fax;
            txtEmail.Text = _CurrentInstance.Email;
            txtTipodeProveedor.Text = _CurrentInstance.TipodeProveedor;
            cmbTipoDeProveedorDeLibrosFiscales.SelectItem(_CurrentInstance.TipoDeProveedorDeLibrosFiscalesAsEnum);
            EstablecePorcentajeRetencionIVA(_CurrentInstance.PorcentajeRetencionIVA);
            txtCuentaContableCxP.Text = _CurrentInstance.CuentaContableCxP;
            lblCuentaContableCxP.Content = BuscaDescripcionContable(txtCuentaContableCxP.Text);
            txtCuentaContableGastos.Text = _CurrentInstance.CuentaContableGastos;
            lblCuentaContableGastos.Content = BuscaDescripcionContable(txtCuentaContableGastos.Text);
            txtCuentaContableAnticipo.Text = _CurrentInstance.CuentaContableAnticipo;
            lblCuentaContableAnticipo.Content = BuscaDescripcionContable(txtCuentaContableAnticipo.Text);
            txtBeneficiario.Text = _CurrentInstance.Beneficiario;
            chkUsarBeneficiarioImpCheq.IsChecked = _CurrentInstance.UsarBeneficiarioImpCheqAsBool;
            txtNumeroCuentaBancaria.Text = _CurrentInstance.NumeroCuentaBancaria;
            txtCodigoContribuyente.Text = _CurrentInstance.CodigoContribuyente;
            txtNumeroRUC.Text = _CurrentInstance.NumeroRUC;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtCodigoProveedor.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoProveedor_Validating);
            this.txtCodigoProveedor.GotFocus += new RoutedEventHandler(txtCodigoProveedor_GotFocus);
            this.txtNumeroRIF.Validating += new CancelEventHandler(txtNumeroRIF_Validating);
            this.txtNombreProveedor.Validating += new System.ComponentModel.CancelEventHandler(txtNombreProveedor_Validating);
            this.cmbTipoDePersona.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoDePersona_Validating);
            this.txtCodigoRetencionUsual.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoRetencionUsual_Validating);
            this.txtNumeroCuentaBancaria.Validating += new CancelEventHandler(txtNumeroCuentaBancaria_Validating);
            this.txtTipodeProveedor.Validating += new System.ComponentModel.CancelEventHandler(txtTipodeProveedor_Validating);
            this.cmbTipoDeProveedorDeLibrosFiscales.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoDeProveedorDeLibrosFiscales_Validating);
            this.txtCuentaContableAnticipo.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaContableAnticipo_Validating);
            this.txtCuentaContableGastos.Validating += new CancelEventHandler(txtCuentaContableGastos_Validating);
            this.txtCuentaContableCxP.Validating += new CancelEventHandler(txtCuentaContableCxP_Validating);
            this.chkUsarBeneficiarioImpCheq.Click += new RoutedEventHandler(chkUsarBeneficiarioImpCheq_Click);
            this.btnValidaRIF.Click += new RoutedEventHandler(btnValidaRIF_Click);
        }

        void btnValidaRIF_Click(object sender, RoutedEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsProveedorIpl insProveedorIpl = new clsProveedorIpl(((clsProveedorIpl)CurrentModel).AppMemoryInfo, ((clsProveedorIpl)CurrentModel).Mfc);
                string xmlWeb = insProveedorIpl.ValidaRifWeb(txtNumeroRIF.Text);
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xmlWeb);
                LibXmlDataParse insParse = new LibXmlDataParse(xDoc);
                txtNombreProveedor.Text = insParse.GetString(0, "Nombre", "");
                bool validadoEnLaWeb = insParse.GetBool(0, "ValidadoEnLaWeb", false);
                bool idFiscalValido = insParse.GetBool(0, "IdFiscalValido", false);
                if (validadoEnLaWeb) {
                    if (idFiscalValido) {
                        lblValidarRIF.Content = "RIF Válido";
                        lblValidarRIF.Foreground = Brushes.DarkGreen;
                        txtNumeroNIT.Focus();
                    } else {
                        lblValidarRIF.Content = "Rif Inválido";
                        lblValidarRIF.Foreground = Brushes.DarkRed;
                    }
                } else {
                    lblValidarRIF.Content = "Falló Conexión";
                    lblValidarRIF.Foreground = Brushes.Black;
                }
                btnValidaRIF.Background = Brushes.LightGray;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void chkUsarBeneficiarioImpCheq_Click(object sender, RoutedEventArgs e) {
            txtBeneficiario.IsEnabled = (chkUsarBeneficiarioImpCheq.IsChecked.Value) ? true : false;
        }

        void txtCodigoProveedor_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsProveedorIpl insProveedorIpl = new clsProveedorIpl(((clsProveedorIpl)CurrentModel).AppMemoryInfo, ((clsProveedorIpl)CurrentModel).Mfc);
                if (!insProveedorIpl.IsValidCodigoProveedor(Action, txtCodigoProveedor.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insProveedorIpl.Information.ToString(), _CurrentModel.MessageName);
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

        void txtNumeroRIF_Validating(object sender, CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsProveedorIpl insProveedorIpl = new clsProveedorIpl(((clsProveedorIpl)CurrentModel).AppMemoryInfo, ((clsProveedorIpl)CurrentModel).Mfc);
                if (!insProveedorIpl.IsValidNumeroRIF(Action, txtNumeroRIF.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insProveedorIpl.Information.ToString(), _CurrentModel.MessageName);
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


        void txtNombreProveedor_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsProveedorIpl insProveedorIpl = new clsProveedorIpl(((clsProveedorIpl)CurrentModel).AppMemoryInfo, ((clsProveedorIpl)CurrentModel).Mfc);
                if (!insProveedorIpl.IsValidNombreProveedor(Action, txtNombreProveedor.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insProveedorIpl.Information.ToString(), _CurrentModel.MessageName);
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

        void cmbTipoDePersona_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoDePersona.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCodigoRetencionUsual_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCodigoRetencionUsual.Text)==0) {
                    txtCodigoRetencionUsual.Text = "*";
                }
                XmlDocument XmlProperties = new XmlDocument();
                if (EncuentraRetencionUsual(out XmlProperties)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCodigoRetencionUsual.Text = insParse.GetString(0, "Codigo", "");
                    lblDescripcionRetencion.Content = insParse.GetString(0, "TipoDePago", "");
                    _CurrentInstance.TipoDePersonaDeCodigoretencionAsDB = insParse.GetString(0, "TipoDePersonaStr", "");
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

        bool EncuentraRetencionUsual(out XmlDocument XmlProperties) {
            string vParamsInitializationList;
            string vParamsFixedList = "";
            DateTime vFechaInicioVigencia;

            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();

            DateTime vFechaHoy = LibDate.Today();
            Galac.Comun.Ccl.TablasLey.ITablaRetencionPdn insTablaRetencion = new Galac.Comun.Brl.TablasLey.clsTablaRetencionNav();
            vFechaInicioVigencia = insTablaRetencion.BuscaFechaDeInicioDeVigencia(vFechaHoy);

            vParamsInitializationList = "Comun.Gv_TablaRetencion_B1.codigo=" + txtCodigoRetencionUsual.Text + LibText.ColumnSeparator();
            vParamsFixedList = "Comun.Gv_TablaRetencion_B1.TipoDePersona=" + cmbTipoDePersona.SelectedItemToDbValue().ToString() + LibText.ColumnSeparator();
            vParamsFixedList = vParamsFixedList + "Comun.Gv_TablaRetencion_B1.FechaDeInicioDeVigencia=" + vFechaInicioVigencia.ToShortDateString() + LibText.ColumnSeparator();

            vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
            vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
            XmlProperties = new XmlDocument();
            return (Galac.Adm.Uil.GestionCompras.clsProveedorList.ChooseTablaRetencion(null, ref XmlProperties, vSearchValues, vFixedValues));
        }

        string GetDescripcionRetencionUsual() {
            string vResult = "";
            XmlDocument XmlProperties = new XmlDocument();
            if (EncuentraRetencionUsual(out XmlProperties)) {
                LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                vResult = insParse.GetString(0, "TipoDePago", "");
                _CurrentInstance.TipoDePersonaDeCodigoretencionAsDB = insParse.GetString(0, "TipoDePersonaStr", "");
            }
            return vResult;
        }

        void txtTipodeProveedor_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtTipodeProveedor.Text)==0) {
                    txtTipodeProveedor.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Saw.Gv_TipoProveedor_B1.nombre=" + txtTipodeProveedor.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "Saw.Gv_TipoProveedor_B1.ConsecutivoCompania=" + _CurrentInstance.ConsecutivoCompania;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Adm.Uil.GestionCompras.clsProveedorList.ChooseTipoProveedor(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtTipodeProveedor.Text = insParse.GetString(0, "Nombre", "");
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

        void cmbTipoDeProveedorDeLibrosFiscales_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoDeProveedorDeLibrosFiscales.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaContableAnticipo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCuentaContableAnticipo.Text) != 0) {
                    //txtCuentaContableAnticipo.Text = "*";

                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "Contab.Gv_Cuenta_B1.codigo=" + txtCuentaContableAnticipo.Text + LibText.ColumnSeparator();
                    vParamsInitializationList = vParamsInitializationList + "Contab.Gv_Cuenta_B1.TieneSubCuentas=" + LibConvert.BoolToSN(false) + LibText.ColumnSeparator();
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    //vParamsFixedList = "Contab.Gv_Cuenta_B1.ConsecutivoPeriodo=" + ((clsProveedorIpl)_CurrentModel).Mfc.GetInt("Periodo");
                    int vConsecutivoPeriodo = LibConvert.ToInt(((clsProveedorIpl)_CurrentModel).AppMemoryInfo.GlobalValuesGetInt("RecordName", "ConsecutivoPeriodo"));
                    vParamsFixedList = "Contab.Gv_Cuenta_B1.ConsecutivoPeriodo=" + vConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Adm.Uil.GestionCompras.clsProveedorList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaContableAnticipo.Text = insParse.GetString(0, "Codigo", "");
                        lblCuentaContableAnticipo.Content = insParse.GetString(0, "Descripcion", "");
                        bool vTieneSubCuentas = insParse.GetBool(0, "TieneSubCuentas", false);
                        if (vTieneSubCuentas) {
                            txtCuentaContableAnticipo.Text = "";
                            MessageBox.Show("No puede escoger cuentas contables que sean de Título.");
                            lblCuentaContableAnticipo.Content = "";
                            txtCuentaContableAnticipo.Focus();
                        }
                    } else {
                        e.Cancel = true;
                    }
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

        void txtCuentaContableCxP_Validating(object sender, CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCuentaContableCxP.Text) != 0) {
                    //txtCuentaContableCxP.Text = "*";

                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "Contab.Gv_Cuenta_B1.codigo=" + txtCuentaContableCxP.Text + LibText.ColumnSeparator();
                    vParamsInitializationList = vParamsInitializationList + "Contab.Gv_Cuenta_B1.TieneSubCuentas=" + LibConvert.BoolToSN(false) + LibText.ColumnSeparator();
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    //vParamsFixedList = "Contab.Gv_Cuenta_B1.ConsecutivoPeriodo=" + ((clsProveedorIpl)_CurrentModel).Mfc.GetInt("Periodo");
                    int vConsecutivoPeriodo = LibConvert.ToInt(((clsProveedorIpl)_CurrentModel).AppMemoryInfo.GlobalValuesGetInt("RecordName", "ConsecutivoPeriodo"));
                    vParamsFixedList = "Contab.Gv_Cuenta_B1.ConsecutivoPeriodo=" + vConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Adm.Uil.GestionCompras.clsProveedorList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaContableCxP.Text = insParse.GetString(0, "Codigo", "");
                        lblCuentaContableCxP.Content = insParse.GetString(0, "Descripcion", "");
                        bool vTieneSubCuentas = insParse.GetBool(0, "TieneSubCuentas", false);
                        if (vTieneSubCuentas) {
                            txtCuentaContableCxP.Text = "";
                            MessageBox.Show("No puede escoger cuentas contables que sean de Título.");
                            lblCuentaContableCxP.Content = "";
                            txtCuentaContableCxP.Focus();
                        }
                    } else {
                        e.Cancel = true;
                    }
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

        void txtCuentaContableGastos_Validating(object sender, CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCuentaContableGastos.Text) != 0) {
                    //txtCuentaContableGastos.Text = "*";

                    string vParamsInitializationList;
                    string vParamsFixedList = "";
                    LibSearch insLibSearch = new LibSearch();
                    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                    vParamsInitializationList = "Contab.Gv_Cuenta_B1.codigo=" + txtCuentaContableGastos.Text + LibText.ColumnSeparator();
                    vParamsInitializationList = vParamsInitializationList + "Contab.Gv_Cuenta_B1.TieneSubCuentas=" + LibConvert.BoolToSN(false) + LibText.ColumnSeparator();
                    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                    //vParamsFixedList = "Contab.Gv_Cuenta_B1.ConsecutivoPeriodo=" + ((clsProveedorIpl)_CurrentModel).Mfc.GetInt("Periodo");
                    int vConsecutivoPeriodo = LibConvert.ToInt(((clsProveedorIpl)_CurrentModel).AppMemoryInfo.GlobalValuesGetInt("RecordName", "ConsecutivoPeriodo"));
                    vParamsFixedList = "Contab.Gv_Cuenta_B1.ConsecutivoPeriodo=" + vConsecutivoPeriodo;
                    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                    XmlDocument XmlProperties = new XmlDocument();
                    if (Galac.Adm.Uil.GestionCompras.clsProveedorList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                        txtCuentaContableGastos.Text = insParse.GetString(0, "Codigo", "");
                        lblCuentaContableGastos.Content = insParse.GetString(0, "Descripcion", "");
                        bool vTieneSubCuentas = insParse.GetBool(0, "TieneSubCuentas", false);
                        if (vTieneSubCuentas) {
                            txtCuentaContableGastos.Text = "";
                            MessageBox.Show("No puede escoger cuentas contables que sean de Título.");
                            lblCuentaContableGastos.Content = "";
                            txtCuentaContableGastos.Focus();
                        }
                    } else {
                        e.Cancel = true;
                    }
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

        void txtNumeroCuentaBancaria_Validating(object sender, CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsProveedorIpl insProveedorIpl = new clsProveedorIpl(((clsProveedorIpl)CurrentModel).AppMemoryInfo, ((clsProveedorIpl)CurrentModel).Mfc);
                if (!insProveedorIpl.IsValidCtaBancaria(Action, txtNumeroCuentaBancaria.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insProveedorIpl.Information.ToString(), _CurrentModel.MessageName);
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

        void txtCodigoProveedor_GotFocus(object sender, RoutedEventArgs e) {
            try {
                if (Action == eAccionSR.Insertar) {
                    txtCodigoProveedor.Text = LibConvert.ToStr(CurrentModel.NextSequential("CodigoProveedor"));
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

        private void EstablecePorcentajeRetencionIVA(decimal valPorcentajeEnum) {
            if (valPorcentajeEnum ==0) 
                    cmbPorcentajeRetencionIVA.SelectedIndex = 0;
                else if (valPorcentajeEnum ==75)
                    cmbPorcentajeRetencionIVA.SelectedIndex = 1;
                else if (valPorcentajeEnum ==100)
                    cmbPorcentajeRetencionIVA.SelectedIndex = 2;
        }


        public static void AllDisabled(UIElementCollection UICol, eAccionSR vAction) {
            if (vAction == eAccionSR.Consultar || vAction == eAccionSR.Eliminar) {
                if (UICol != null) {
                    LibApiAwp.DisableAllFieldsIfActionIn(UICol, (int)vAction, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar });
                    foreach (UIElement aux in UICol) {
                        try {
                            ContentControl cc;
                            ItemsControl ic;
                            Panel p;
                            Control i;
                            if (aux is System.Windows.Controls.Button) {
                                ((System.Windows.Controls.Button)aux).IsEnabled = false;
                            }
                            if (aux is System.Windows.Controls.ComboBox) {
                                ((System.Windows.Controls.ComboBox)aux).IsEnabled = false;
                            }
                            if (aux is System.Windows.Controls.CheckBox) {
                                ((System.Windows.Controls.CheckBox)aux).IsEnabled = false;
                            }
                            if (aux is System.Windows.Controls.TabControl) {
                                System.Windows.Controls.TabControl x = (System.Windows.Controls.TabControl)aux;
                                AllDisabled(((UIElementCollection)x.Items[0]), vAction);
                                AllDisabled(((UIElementCollection)x.Items[1]), vAction);
                                AllDisabled(((UIElementCollection)x.Items[2]), vAction);
                            }
                            if (!(aux is System.Windows.Controls.Panel)) {
                                cc = (System.Windows.Controls.ContentControl)aux;
                                p = (System.Windows.Controls.Panel)cc.Content;
                            } else {
                                p = (System.Windows.Controls.Panel)aux;
                            }
                            if (p != null)
                                AllDisabled(p.Children, vAction);
                        } catch (InvalidCastException e) {
                        }
                            
                    }
                }
            }
        }

        string BuscaDescripcionContable(string valCodigoCuenta) {
            if (!LibText.IsNullOrEmpty(valCodigoCuenta, true)) {

                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Contab.Gv_Cuenta_B1.codigo=" + valCodigoCuenta + LibText.ColumnSeparator();
                vParamsInitializationList = vParamsInitializationList + "Contab.Gv_Cuenta_B1.TieneSubCuentas=" + LibConvert.BoolToSN(false) + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                int vConsecutivoPeriodo = LibConvert.ToInt(((clsProveedorIpl)_CurrentModel).AppMemoryInfo.GlobalValuesGetInt("RecordName", "ConsecutivoPeriodo"));
                vParamsFixedList = "Contab.Gv_Cuenta_B1.ConsecutivoPeriodo=" + vConsecutivoPeriodo;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Adm.Uil.GestionCompras.clsProveedorList.ChooseCuenta(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    return insParse.GetString(0, "Descripcion", "");
                } else {
                    return "";
                }
            } else {
                return "";
            }
        }

    } //End of class GSProveedor.xaml

  
} //End of namespace Galac.Adm.Uil.GestionCompras

