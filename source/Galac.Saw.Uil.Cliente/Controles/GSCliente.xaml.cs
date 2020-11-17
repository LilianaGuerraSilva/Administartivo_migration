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
using Entity = Galac.Saw.Ccl.Cliente;
          
namespace Galac.Saw.Uil.Cliente.Controles {
    /// <summary>
    /// Lógica de interacción para GSCliente.xaml
    /// </summary>
    internal partial class GSCliente: UserControl {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        Entity.Cliente _CurrentInstance;
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
        internal Entity.Cliente CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        internal ILibView CurrentModel {
            get { return _CurrentModel; }
            set { _CurrentModel = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSCliente() {
            InitializeComponent();
            InitializeEvents();
            cmbStatus.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(Entity.eStatusCliente)));
            cmbNivelDePrecio.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(Entity.eNivelDePrecio)));
            cmbOrigen.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(Entity.eOrigenFacturacionOManual)));
            cmbTipoDocumentoIdentificacion.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(Entity.eTipoDocumentoIdentificacion)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Código, Nombre";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, ILibView initModel, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (Entity.Cliente)initInstance;
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
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
            }
        }

        internal void SetNavigatorValuesFromForm() {
            _CurrentInstance.Codigo = txtCodigo.Text;
            _CurrentInstance.Nombre = txtNombre.Text;
            _CurrentInstance.NumeroRIF = txtNumeroRIF.Text;
            _CurrentInstance.NumeroNIT = txtNumeroNIT.Text;
            _CurrentInstance.Direccion = txtDireccion.Text;
            _CurrentInstance.Ciudad = txtCiudad.Text;
            _CurrentInstance.ZonaPostal = txtZonaPostal.Text;
            _CurrentInstance.Telefono = txtTelefono.Text;
            _CurrentInstance.FAX = txtFAX.Text;
            _CurrentInstance.StatusAsEnum = (Entity.eStatusCliente)cmbStatus.SelectedItemToInt();
            _CurrentInstance.Contacto = txtContacto.Text;
            _CurrentInstance.ZonaDeCobranza = txtZonaDeCobranza.Text;
            _CurrentInstance.CodigoVendedor = txtCodigoVendedor.Text;
            _CurrentInstance.RazonInactividad = txtRazonInactividad.Text;
            _CurrentInstance.Email = txtEmail.Text;
            _CurrentInstance.ActivarAvisoAlEscogerAsBool = chkActivarAvisoAlEscoger.IsChecked.Value;
            _CurrentInstance.TextoDelAviso = txtTextoDelAviso.Text;
            _CurrentInstance.CuentaContableCxC = txtCuentaContableCxC.Text;
            _CurrentInstance.CuentaContableIngresos = txtCuentaContableIngresos.Text;
            _CurrentInstance.CuentaContableAnticipo = txtCuentaContableAnticipo.Text;
            _CurrentInstance.SectorDeNegocio = txtSectorDeNegocio.Text;
            _CurrentInstance.CodigoLote = txtCodigoLote.Text;
            _CurrentInstance.NivelDePrecioAsEnum = (Entity.eNivelDePrecio)cmbNivelDePrecio.SelectedItemToInt();
            _CurrentInstance.OrigenAsEnum = (Entity.eOrigenFacturacionOManual)cmbOrigen.SelectedItemToInt();
            _CurrentInstance.DiaCumpleanos = LibConvert.ToInt(txtDiaCumpleanos.Text);
            _CurrentInstance.MesCumpleanos = LibConvert.ToInt(txtMesCumpleanos.Text);
            _CurrentInstance.CorrespondenciaXEnviarAsBool = chkCorrespondenciaXEnviar.IsChecked.Value;
            _CurrentInstance.EsExtranjeroAsBool = chkEsExtranjero.IsChecked.Value;
            _CurrentInstance.ClienteDesdeFecha = dtpClienteDesdeFecha.Date;
            _CurrentInstance.TipoDocumentoIdentificacionAsEnum = (Entity.eTipoDocumentoIdentificacion)cmbTipoDocumentoIdentificacion.SelectedItemToInt();
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            if (valClearRecord) {
                _CurrentModel.Clear(_CurrentInstance);
            }
            ClearControl();
            txtCodigo.Text = _CurrentInstance.Codigo;
            txtNombre.Text = _CurrentInstance.Nombre;
            txtNumeroRIF.Text = _CurrentInstance.NumeroRIF;
            txtNumeroNIT.Text = _CurrentInstance.NumeroNIT;
            txtDireccion.Text = _CurrentInstance.Direccion;
            txtCiudad.Text = _CurrentInstance.Ciudad;
            txtZonaPostal.Text = _CurrentInstance.ZonaPostal;
            txtTelefono.Text = _CurrentInstance.Telefono;
            txtFAX.Text = _CurrentInstance.FAX;
            cmbStatus.SelectItem(_CurrentInstance.StatusAsEnum);
            txtContacto.Text = _CurrentInstance.Contacto;
            txtZonaDeCobranza.Text = _CurrentInstance.ZonaDeCobranza;
            txtCodigoVendedor.Text = _CurrentInstance.CodigoVendedor;
            txtRazonInactividad.Text = _CurrentInstance.RazonInactividad;
            txtEmail.Text = _CurrentInstance.Email;
            chkActivarAvisoAlEscoger.IsChecked = _CurrentInstance.ActivarAvisoAlEscogerAsBool;
            txtTextoDelAviso.Text = _CurrentInstance.TextoDelAviso;
            txtCuentaContableCxC.Text = _CurrentInstance.CuentaContableCxC;
            txtCuentaContableIngresos.Text = _CurrentInstance.CuentaContableIngresos;
            txtCuentaContableAnticipo.Text = _CurrentInstance.CuentaContableAnticipo;
            txtSectorDeNegocio.Text = _CurrentInstance.SectorDeNegocio;
            txtCodigoLote.Text = _CurrentInstance.CodigoLote;
            cmbNivelDePrecio.SelectItem(_CurrentInstance.NivelDePrecioAsEnum);
            cmbOrigen.SelectItem(_CurrentInstance.OrigenAsEnum);
            txtDiaCumpleanos.Text = LibConvert.ToStr(_CurrentInstance.DiaCumpleanos);
            txtMesCumpleanos.Text = LibConvert.ToStr(_CurrentInstance.MesCumpleanos);
            chkCorrespondenciaXEnviar.IsChecked = _CurrentInstance.CorrespondenciaXEnviarAsBool;
            chkEsExtranjero.IsChecked = _CurrentInstance.EsExtranjeroAsBool;
            dtpClienteDesdeFecha.Date = _CurrentInstance.ClienteDesdeFecha;
            cmbTipoDocumentoIdentificacion.SelectItem(_CurrentInstance.TipoDocumentoIdentificacionAsEnum);
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtCodigo.Validating += new System.ComponentModel.CancelEventHandler(txtCodigo_Validating);
            this.txtCodigo.GotFocus += new RoutedEventHandler(txtCodigo_GotFocus);
            this.txtNombre.Validating += new System.ComponentModel.CancelEventHandler(txtNombre_Validating);
            this.txtCiudad.Validating += new System.ComponentModel.CancelEventHandler(txtCiudad_Validating);
            this.cmbStatus.Validating += new System.ComponentModel.CancelEventHandler(cmbStatus_Validating);
            this.txtZonaDeCobranza.Validating += new System.ComponentModel.CancelEventHandler(txtZonaDeCobranza_Validating);
            this.txtCodigoVendedor.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoVendedor_Validating);
            this.txtCuentaContableCxC.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaContableCxC_Validating);
            this.txtCuentaContableIngresos.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaContableIngresos_Validating);
            this.txtCuentaContableAnticipo.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaContableAnticipo_Validating);
            this.txtSectorDeNegocio.Validating += new System.ComponentModel.CancelEventHandler(txtSectorDeNegocio_Validating);
            this.cmbNivelDePrecio.Validating += new System.ComponentModel.CancelEventHandler(cmbNivelDePrecio_Validating);
            this.cmbOrigen.Validating += new System.ComponentModel.CancelEventHandler(cmbOrigen_Validating);
            this.cmbTipoDocumentoIdentificacion.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoDocumentoIdentificacion_Validating);
        }

        void txtCodigo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsClienteIpl insClienteIpl = new clsClienteIpl(((clsClienteIpl)CurrentModel).AppMemoryInfo, ((clsClienteIpl)CurrentModel).Mfc);
                if (!insClienteIpl.IsValidCodigo(Action, txtCodigo.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insClienteIpl.Information.ToString(), _CurrentModel.MessageName);
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

        void txtNombre_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsClienteIpl insClienteIpl = new clsClienteIpl(((clsClienteIpl)CurrentModel).AppMemoryInfo, ((clsClienteIpl)CurrentModel).Mfc);
                if (!insClienteIpl.IsValidNombre(Action, txtNombre.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insClienteIpl.Information.ToString(), _CurrentModel.MessageName);
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

        void txtCiudad_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCiudad.Text)==0) {
                    txtCiudad.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_Ciudad_B1.NombreCiudad=" + txtCiudad.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Cliente.clsClienteList.ChooseCiudad(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCiudad.Text = insParse.GetString(0, "NombreCiudad", "");
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

        void cmbStatus_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbStatus.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtZonaDeCobranza_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtZonaDeCobranza.Text)==0) {
                    txtZonaDeCobranza.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_ZonaCobranza_B1.nombre=" + txtZonaDeCobranza.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                vParamsFixedList = "ConsecutivoCompania=" + _CurrentInstance.ConsecutivoCompania;
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Cliente.clsClienteList.ChooseZonaCobranza(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtZonaDeCobranza.Text = insParse.GetString(0, "nombre", "");
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

        void txtCodigoVendedor_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCodigoVendedor.Text)==0) {
                    txtCodigoVendedor.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_Vendedor_B1.codigo=" + txtCodigoVendedor.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                vParamsFixedList = "ConsecutivoCompania=" + _CurrentInstance.ConsecutivoCompania;
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Cliente.clsClienteList.ChooseVendedor(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCodigoVendedor.Text = insParse.GetString(0, "codigo", "");
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

        void txtCuentaContableCxC_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCuentaContableCxC.Text)==0) {
                    txtCuentaContableCxC.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_Cuenta_B1.codigo=" + txtCuentaContableCxC.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                vParamsFixedList = "ConsecutivoPeriodo=" + _CurrentInstance.ConsecutivoCompania;
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Cliente.clsClienteList.ChooseCuenta(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCuentaContableCxC.Text = insParse.GetString(0, "codigo", "");
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

        void txtCuentaContableIngresos_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCuentaContableIngresos.Text)==0) {
                    txtCuentaContableIngresos.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_Cuenta_B1.codigo=" + txtCuentaContableIngresos.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                vParamsFixedList = "ConsecutivoPeriodo=" + _CurrentInstance.ConsecutivoCompania;
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Cliente.clsClienteList.ChooseCuenta(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCuentaContableIngresos.Text = insParse.GetString(0, "codigo", "");
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

        void txtCuentaContableAnticipo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCuentaContableAnticipo.Text)==0) {
                    txtCuentaContableAnticipo.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_Cuenta_B1.codigo=" + txtCuentaContableAnticipo.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                vParamsFixedList = "ConsecutivoPeriodo=" + _CurrentInstance.ConsecutivoCompania;
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Cliente.clsClienteList.ChooseCuenta(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCuentaContableAnticipo.Text = insParse.GetString(0, "codigo", "");
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

        void txtSectorDeNegocio_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtSectorDeNegocio.Text)==0) {
                    txtSectorDeNegocio.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_SectorDeNegocio_B1.Descripcion=" + txtSectorDeNegocio.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Cliente.clsClienteList.ChooseSectorDeNegocio(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtSectorDeNegocio.Text = insParse.GetString(0, "Descripcion", "");
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

        void cmbNivelDePrecio_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbNivelDePrecio.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbOrigen_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbOrigen.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbTipoDocumentoIdentificacion_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoDocumentoIdentificacion.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCodigo_GotFocus(object sender, RoutedEventArgs e) {
            try {
                if (Action == eAccionSR.Insertar) {
                    txtCodigo.Text = LibConvert.ToStr(CurrentModel.NextSequential("Codigo"));
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


    } //End of class GSCliente.xaml

} //End of namespace Galac.Saw.Uil.Clientes

