using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
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
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using LibGalac.Aos.UI.Cib;
using Galac.Adm.Ccl.CajaChica;
using System.Diagnostics;
using System.Globalization;


namespace Galac.Adm.Uil.CajaChica.Input {
    /// <summary>
    /// Lógica de interacción para GSRendicion.xaml
    /// </summary>
    internal partial class GSRendicion : UserControl {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        Rendicion _CurrentInstance;
        ILibView _CurrentModel;
       // ObservableCollection<Anticipo> adelantos;


        //########## AQUI

        #endregion //Variables
        #region Propiedades
        internal bool CancelValidations {
            get { return _CancelValidations; }
            set {
                _CancelValidations = value;
                 this.DetalleRendicionUc.CancelValidations = _CancelValidations;
            }
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
        internal Rendicion CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        internal ILibView CurrentModel {
            get { return _CurrentModel; }
            set { _CurrentModel = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSRendicion() {
            InitializeComponent();
            InitializeEvents();
            // cmbTipoDeDocumento.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeDocumentoRendicion)));
            //lblStatusRendicionText|.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eStatusRendicion)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Consecutivo, Numero, Código del Beneficiario";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, ILibView initModel, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (Rendicion)initInstance;
            _CurrentModel = initModel;
            Title = initModel.MessageName;
            Action = initAction;
            ExtendedAction = initExtendedAction;
            LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar, (int)eAccionSR.Anular, (int)eAccionSR.Cerrar });
            LibApiAwp.EnableControl(txtNumero, Action == eAccionSR.Insertar);
            if (Action == eAccionSR.Insertar) {
                SetFormValuesFromNavigator(true);
                ((clsRendicionIpl)initModel).InitDetailForInsert();
                _CurrentInstance.Adelantos = new ObservableCollection<Anticipo>();
                _CurrentInstance.DetailDetalleDeRendicion.Add(new DetalleDeRendicion());
                generarConsecutivo();
            } else {
                SetFormValuesFromNavigator(false);
                string xml = _CurrentInstance.Datos.InnerXml;
                if (xml != String.Empty) {
                    CargarAdelantos(new LibXmlDataParse(xml),new Galac.Adm.Brl.CajaChica.clsAnticipoNav(),_CurrentInstance.Adelantos);
                }
                if((eAccionSR.Insertar.Equals(initAction) | eAccionSR.Modificar.Equals(initAction)) ? false : true)
                foreach (var aux in grid.Columns) aux.IsReadOnly = true;
                txtCodigoCuentaBancaria.IsEnabled = false;
                txtCodigoConceptoBancario.IsEnabled = false;
                dtpFechaCierre.IsEnabled = false;
                txtNumeroDocumento.IsEnabled = false;
                dtpFechaAnulacion.IsEnabled = false;
                if (Action == eAccionSR.Cerrar) {
                    tbiSeccionCierre.Visibility = Visibility.Visible;
                    txtCodigoCuentaBancaria.IsEnabled = true;
                    txtCodigoConceptoBancario.IsEnabled = true;
                    dtpFechaCierre.IsEnabled = true;
                    txtNumeroDocumento.IsEnabled = true;
                }
                if ((Action == eAccionSR.Consultar || Action == eAccionSR.Anular)) {
                    if (Action == eAccionSR.Anular) {
                        tbiSeccionAnular.Visibility = Visibility.Visible;
                        dtpFechaAnulacion.IsEnabled = true;
                    }
                    if (_CurrentInstance.StatusRendicionAsEnum == eStatusRendicion.Cerrada) {
                        tbiSeccionCierre.Visibility = Visibility.Visible;
                    } else if (_CurrentInstance.StatusRendicionAsEnum == eStatusRendicion.Anulada) {
                        tbiSeccionCierre.Visibility = Visibility.Visible;
                        tbiSeccionAnular.Visibility = Visibility.Visible;
                    }
                }
            }

            AddEmptyRow(_CurrentInstance.Adelantos, false);
            grid.ItemsSource = _CurrentInstance.Adelantos;
            DetalleRendicionUc.DataContext = _CurrentInstance.DetailDetalleDeRendicion;
            DetalleRendicionUc.InitializeControl(initInstance, initModel, initAction, initExtendedAction);
            //DetalleRendicionUc.ConsecutivoCompania = ((clsRendicionIpl)initModel).AppMemoryInfo.GlobalValuesGetInt("RecordName", "Compania");
            CollectionViewSource.GetDefaultView(grid.ItemsSource).Filter = new Predicate<object>(filtrar);
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void CargarAdelantos(LibXmlDataParse insParse,ILibBusinessComponentWithSearch<IList<Anticipo>, IList<Anticipo>> anticipoNav,ObservableCollection<Anticipo> adelantos) {
                for (int i = 0; i < insParse.Count(); i++) {
                    Anticipo adelanto = new Anticipo();
                    LibGpParams vParams = new LibGpParams();
                    vParams.AddInInteger("ConsecutivoCompania", insParse.GetInt(i, "ConsecutivoCompania", 0));
                    vParams.AddInInteger("ConsecutivoAnticipo", insParse.GetInt(i, "ConsecutivoAnticipo", 0));
                    adelanto = anticipoNav.GetData(eProcessMessageType.SpName, "AnticipoGET", vParams.Get())[0];
                    adelantos.Add(adelanto);
                }
        }

        private void cc(object o, System.Collections.Specialized.NotifyCollectionChangedEventArgs arg) {
           
          
        }

        public bool filtrar(object ade) {
            Anticipo ant = (Anticipo)ade;
            return (ant.ConsecutivoRendicion != 0 || ant.Fecha.Year == 1);
        }
         

        private void SetLookAndFeelForCurrentRecord() {
        
        }

        internal void SetNavigatorValuesFromForm() {
		    _CurrentInstance.Numero = txtNumero.Text;
		    _CurrentInstance.CodigoBeneficiario = txtCodigoBeneficiario.Text;
            _CurrentInstance.NombreBeneficiario = txtNombreBeneficiario.Text;
            _CurrentInstance.ConsecutivoBeneficiario = Int32.Parse(lneConsecutivoBeneficiario.Content.ToString());
            _CurrentInstance.FechaApertura = dtpFechaApertura.Date;
            _CurrentInstance.FechaCierre = dtpFechaCierre.Date;
            _CurrentInstance.FechaAnulacion = dtpFechaAnulacion.Date;
            _CurrentInstance.TotalAdelantos = txtTotalAdelantos.Value;
            _CurrentInstance.TotalGastos = txtTotalGastosCalculo.Value;
            _CurrentInstance.TotalIVA = txtTotalIVA.Value;
            _CurrentInstance.CodigoCuentaBancaria = txtCodigoCuentaBancaria.Text;
            _CurrentInstance.NombreCuentaBancaria = lneNombreCuentaBancaria.Content.ToString();
            _CurrentInstance.CodigoConceptoBancario = txtCodigoConceptoBancario.Text;
            _CurrentInstance.NombreConceptoBancario = lneNombreConceptoBancario.Content.ToString();
            _CurrentInstance.NumeroDocumento = txtNumeroDocumento.Text;
            _CurrentInstance.BeneficiarioCheque = txtBeneficiarioCheque.Text;
            _CurrentInstance.Descripcion = txtDescripcion.Text;
            _CurrentInstance.Observaciones = txtObservaciones.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            if (valClearRecord) {
                _CurrentModel.Clear(_CurrentInstance);
            }
            ClearControl();
			txtNumero.Text = _CurrentInstance.Numero;
            txtNumero.Text = _CurrentInstance.Numero.ToString();
            lneConsecutivoBeneficiario.Content = _CurrentInstance.ConsecutivoBeneficiario;
            txtNombreBeneficiario.Text = _CurrentInstance.NombreBeneficiario;
            txtCodigoBeneficiario.Text = _CurrentInstance.CodigoBeneficiario;
            dtpFechaApertura.Date = _CurrentInstance.FechaApertura;
            dtpFechaCierre.Date = _CurrentInstance.FechaCierre;
            dtpFechaAnulacion.Date = _CurrentInstance.FechaAnulacion;
            lblStatusRendicionText.Content = _CurrentInstance.StatusRendicionAsString;
            txtTotalAdelantos.Value = _CurrentInstance.TotalAdelantos;
            txtTotalGastosCalculo.Value = _CurrentInstance.TotalGastos;
            txtTotalIVA.Value = _CurrentInstance.TotalIVA;
            txtCodigoCuentaBancaria.Text = _CurrentInstance.CodigoCuentaBancaria;
            lneNombreCuentaBancaria.Content = _CurrentInstance.NombreCuentaBancaria;
            txtCodigoConceptoBancario.Text = _CurrentInstance.CodigoConceptoBancario;
            lneNombreConceptoBancario.Content = _CurrentInstance.NombreConceptoBancario;
            txtNumeroDocumento.Text = _CurrentInstance.NumeroDocumento;
            txtBeneficiarioCheque.Text = _CurrentInstance.BeneficiarioCheque;
            txtDescripcion.Text = _CurrentInstance.Descripcion;
            txtObservaciones.Text = _CurrentInstance.Observaciones;
            RealizaLosCalculos();
            
        }

        private void InitializeEvents() {
            //    this.lneConsecutivo.Validating += new System.ComponentModel.CancelEventHandler(lneConsecutivo_Validating);
			this.txtNumero.Validating += new System.ComponentModel.CancelEventHandler(txtNumero_Validating);
            //    this.cmbTipoDeDocumento.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoDeDocumento_Validating);
            this.txtNombreBeneficiario.Validating += new System.ComponentModel.CancelEventHandler(txtNombreBeneficiario_Validating);
            //this.cmbStatusRendicion.Validating += new System.ComponentModel.CancelEventHandler(cmbStatusRendicion_Validating);
            this.txtCodigoCuentaBancaria.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoCuentaBancaria_Validating);
            this.txtNumeroDocumento.Validating += new System.ComponentModel.CancelEventHandler(txtNumeroDocumento_Validating);
            this.txtBeneficiarioCheque.Validating += new System.ComponentModel.CancelEventHandler(txtBeneficiarioCheque_Validating);
            this.txtCodigoConceptoBancario.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoConceptoBancario_Validating);
        }

        void lneConsecutivo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            //try {
            //    if (CancelValidations) {
            //        return;
            //    }
            //    clsRendicionIpl insRendicionIpl = new clsRendicionIpl(((clsRendicionIpl)CurrentModel).AppMemoryInfo, ((clsRendicionIpl)CurrentModel).Mfc);
            //    if (!insRendicionIpl.IsValidConsecutivo(Action, LibConvert.ToInt(lneConsecutivo.Value), true)) {
            //        LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insRendicionIpl.Information.ToString(), _CurrentModel.MessageName);
            //        e.Cancel = true;
            //    }
            //} catch (GalacException gEx) {
            //    LibExceptionDisplay.Show(gEx, this.Title);
            //} catch (Exception vEx) {
            //    if (vEx is System.AccessViolationException) {
            //        throw;
            //    }
            //    LibExceptionDisplay.Show(vEx);
            //}
        }

        void txtNumero_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsRendicionIpl insRendicionIpl = new clsRendicionIpl(((clsRendicionIpl)CurrentModel).AppMemoryInfo, ((clsRendicionIpl)CurrentModel).Mfc);
                if (!insRendicionIpl.IsValidNumero(Action, txtNumero.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insRendicionIpl.Information.ToString(), _CurrentModel.MessageName);
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
        void cmbTipoDeDocumento_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                //     cmbTipoDeDocumento.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbStatusRendicion_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                //cmbStatusRendicion.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCodigoCuentaBancaria_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    if (!CurrentInstance.CodigoCuentaBancaria.Equals(string.Empty))
                    return;
                }
                if (LibString.Len(txtCodigoCuentaBancaria.Text) == 0) {
                    txtCodigoCuentaBancaria.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_CuentaBancaria_B1.Codigo=" + txtCodigoCuentaBancaria.Text + LibText.ColumnSeparator();
                vParamsInitializationList += "Gv_CuentaBancaria_B1.EsCajaChica=" + LibConvert.BoolToSN(false) + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "Gv_CuentaBancaria_B1.ConsecutivoCompania=" + _CurrentInstance.ConsecutivoCompania;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Adm.Uil.CajaChica.clsRendicionList.ChooseCuentaBancaria(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCodigoCuentaBancaria.Text = insParse.GetString(0, "Codigo", "");
                    lneNombreCuentaBancaria.Content = insParse.GetString(0, "NombreCuenta", "");
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

        void txtNumeroDocumento_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsRendicionIpl insRendicionIpl = new clsRendicionIpl(((clsRendicionIpl)CurrentModel).AppMemoryInfo, ((clsRendicionIpl)CurrentModel).Mfc);
                if (!insRendicionIpl.IsValidNumeroDocumento(Action, txtNumeroDocumento.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insRendicionIpl.Information.ToString(), _CurrentModel.MessageName);
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

        void txtBeneficiarioCheque_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsRendicionIpl insRendicionIpl = new clsRendicionIpl(((clsRendicionIpl)CurrentModel).AppMemoryInfo, ((clsRendicionIpl)CurrentModel).Mfc);
                if (!insRendicionIpl.IsValidBeneficiarioCheque(Action, txtBeneficiarioCheque.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insRendicionIpl.Information.ToString(), _CurrentModel.MessageName);
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
        void txtCodigoConceptoBancario_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    if (!CurrentInstance.CodigoConceptoBancario.Equals(string.Empty))
                    return;
                }
                if (LibString.Len(txtCodigoConceptoBancario.Text) == 0) {
                    txtCodigoConceptoBancario.Text = "*";
                }
                string vParamsInitializationList;
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_ConceptoBancario_B1.Codigo=" + txtCodigoConceptoBancario.Text + LibText.ColumnSeparator();
                if (txtTotalSaldo.Value > 0) {
                    vParamsInitializationList += "Gv_ConceptoBancario_B1.Tipo=" + (int)eIngresoEgreso.Ingreso + LibText.ColumnSeparator();
                } else if (txtTotalSaldo.Value < 0) {
                    vParamsInitializationList += "Gv_ConceptoBancario_B1.Tipo=" + (int)eIngresoEgreso.Egreso + LibText.ColumnSeparator();
                }
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Adm.Uil.Banco.clsConceptoBancarioList.ChooseConceptoBancario(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCodigoConceptoBancario.Text = insParse.GetString(0, "Codigo", "");
                    lneNombreConceptoBancario.Content = insParse.GetString(0, "Descripcion", "");
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

        void txtNombreBeneficiario_Validating(object sender, System.ComponentModel.CancelEventArgs e) { 
            try { 
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtNombreBeneficiario.Text) == 0) {
                    txtNombreBeneficiario.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                //##### para buscar por nombre o por consecutivo
                vParamsInitializationList = "Gv_Beneficiario_B1.NombreBeneficiario=" + txtNombreBeneficiario.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "Gv_Beneficiario_B1.ConsecutivoCompania=" + _CurrentInstance.ConsecutivoCompania;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();               
                if (Galac.Adm.Uil.CajaChica.clsRendicionList.ChooseBeneficiario(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtNombreBeneficiario.Text = insParse.GetString(0, "NombreBeneficiario", "");
                    lneConsecutivoBeneficiario.Content = insParse.GetString(0, "Consecutivo", "");
                    txtCodigoBeneficiario.Text = insParse.GetString(0, "Codigo", "");
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
                
        private void RealizaLosCalculos() {
            txtTotalSaldo.Value = (txtTotalAdelantos.Value) - (DetalleRendicionUc.txtTotalFacturas.Value);
        }

        #endregion //Metodos Generados
        #region Metodos Creados

        private void calculaTotalAdelantos()
        {
            txtTotalAdelantos.Value = ((ICollection<Anticipo>)grid.ItemsSource).Cast<Anticipo>().Where(a => a.ConsecutivoRendicion != 0).Sum(d => d.MontoTotal);
            RealizaLosCalculos();
        }

        private void txtConsecutivo_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void generarConsecutivo()
        {
            try
            {
                _CurrentInstance.Consecutivo = Int32.Parse(LibConvert.ToStr(_CurrentModel.NextSequential("Consecutivo")));
            }
            catch (GalacException gEx)
            {
                LibExceptionDisplay.Show(gEx, this.Title);
            }
            catch (Exception vEx)
            {
                if (vEx is System.AccessViolationException)
                {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }


        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

            //if (((TextBox)e.Source).Text == String.Empty) {
            //    ((TextBox)e.Source).IsReadOnly = false;
            //}

        }


        private XmlDocument BuscarAdelanto(string sqlwhere, string consecutivoBeneficiario, LibSearch insLibSearch, XmlDocument XmlProperties)
        {
            string vParamsInitializationList;
            string vParamsFixedList = "";
            string consecutivo = consecutivoBeneficiario;
            if (string.IsNullOrEmpty(consecutivo))
                return null;
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            vParamsInitializationList = "dbo.Gv_Anticipo_B1.CodigoBeneficiario=" + txtCodigoBeneficiario.Text + LibText.ColumnSeparator();
            vParamsInitializationList += "dbo.Gv_Anticipo_B1.ConsecutivoRendicion=" + " 0 " + LibText.ColumnSeparator();
            vParamsInitializationList += "dbo.Gv_Anticipo_B1.Status=" + LibConvert.EnumToDbValue((int)eStatusAnticipo.Vigente) + LibText.ColumnSeparator();
            if (!(sqlwhere.Length == 0))
            {
                vParamsInitializationList += "dbo.Gv_Anticipo_B1.ConsecutivoAnticipo=" + sqlwhere + LibText.ColumnSeparator();
            }
            vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
            vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
            if (Galac.Adm.Uil.CajaChica.clsAnticipoList.ChooseAnticipo(null, ref XmlProperties, vSearchValues, vFixedValues))
            {
                return XmlProperties;
            }
            else
            {
                return null;
            }
        }



        //  private XmlDocument BuscarAdelanto(string sqlwhere) {
        //    string vParamsInitializationList;
        //    string vParamsFixedList = "";
        //    string consecutivo = txtBeneficiario.Text;
        //    if (string.IsNullOrEmpty(consecutivo))
        //        return null;
        //    LibSearch insLibSearch = new LibSearch();
        //    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
        //    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
        //    vParamsInitializationList = "dbo.Gv_Anticipo_B1.CodigoBeneficiario=" + txtCodigoBeneficiario.Text + LibText.ColumnSeparator();
        //    vParamsInitializationList += "dbo.Gv_Anticipo_B1.ConsecutivoRendicion=" + " 0 " + LibText.ColumnSeparator();
        //    vParamsInitializationList += "dbo.Gv_Anticipo_B1.Status=" + LibConvert.EnumToDbValue((int)eStatusAnticipo.Vigente) + LibText.ColumnSeparator();

        //    if (!(sqlwhere.Length == 0)) {
        //        vParamsInitializationList += "dbo.Gv_Anticipo_B1.ConsecutivoAnticipo=" + sqlwhere + LibText.ColumnSeparator();
        //    }

        //    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
        //    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
        //    XmlDocument XmlProperties = new XmlDocument();
        //    if (Galac.Adm.Uil.CajaChica.clsAnticipoList.ChooseAnticipo(null, ref XmlProperties, vSearchValues, vFixedValues)) {
        //        return XmlProperties;
        //    } else {
        //        return null;
        //    }

        //}


        private Anticipo materializarAdelanto(LibXmlDataParse insParse, Anticipo adelanto, ILibBusinessComponentWithSearch<IList<Anticipo>, IList<Anticipo>> AnticipoNav)
        {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", insParse.GetInt(0, "ConsecutivoCompania", 0));
            vParams.AddInInteger("ConsecutivoAnticipo", insParse.GetInt(0, "ConsecutivoAnticipo", 0));
            Anticipo aux = AnticipoNav.GetData(eProcessMessageType.SpName, "AnticipoGET", vParams.Get())[0];
            copyObject(adelanto, aux);
            adelanto.ConsecutivoRendicion = _CurrentInstance.Consecutivo;
            return adelanto;
        }

        private void copyObject(object aux1, object aux2)
        {

            foreach (var p in aux1.GetType().GetProperties())
            {
                if (aux1.GetType().GetProperty(p.Name).CanRead && aux1.GetType().GetProperty(p.Name).CanWrite)
                    aux1.GetType().GetProperty(p.Name).SetValue(aux1, aux2.GetType().GetProperty(p.Name).GetValue(aux2, null), null);
            }
        }

        private void grdDetalleDeRendicion_KeyDown(object sender, KeyEventArgs e)
        {


        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)e.Source).IsReadOnly)
                return;
            XmlDocument doc = BuscarAdelanto(((TextBox)e.Source).Text, txtNombreBeneficiario.Text, new LibSearch(), new XmlDocument());
        }


        private void grid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var ValorOriginal = e.Row.Item.GetType().GetProperty(e.Column.SortMemberPath).GetValue(e.Row.Item, null);
            var ValorActural = ((TextBox)e.EditingElement).Text;
            var beneficiario = txtNombreBeneficiario.Text;
            ValorOriginal = ValorOriginal == null ? "" : ValorOriginal;


            if (ValorOriginal.ToString() == ValorActural.ToString())
            {
                return;
            }


            XmlDocument doc;
            try
            {
                doc = BuscarAdelanto(ValorActural, beneficiario, new LibSearch(), new XmlDocument());
            }
            catch (Exception)
            {
                doc = null;
            }

            if (doc != null)
            {
                int consecutivoSch = new LibXmlDataParse(doc).GetInt(0, "ConsecutivoAnticipo", 0);
                int consecutivoRendicionSch = new LibXmlDataParse(doc).GetInt(0, "ConsecutivoRendicion", 0);

                //if (((ObservableCollection<Anticipo>)((DataGrid)sender).ItemsSource).Any(a => a.ConsecutivoAnticipo == consecutivoSch)) {
                if (((ObservableCollection<Anticipo>)((DataGrid)sender).ItemsSource).Any(a => a.ConsecutivoAnticipo == consecutivoSch && a.ConsecutivoRendicion != 0))
                {
                    LibNotifier.Alert(null, "Ya este Adelanto se encuentra asociado a la Rendicion!!", "Advertencia");
                    if (e.Row.Item.GetType().GetProperty("Fecha").GetValue(e.Row.Item, null).Equals(Activator.CreateInstance(e.Row.Item.GetType().GetProperty("Fecha").GetValue(e.Row.Item, null).GetType())))
                    {
                        ((TextBox)e.EditingElement).Text = "0";
                    }
                    else
                    {
                        ((TextBox)e.EditingElement).Text = ValorOriginal.ToString();
                    }
                    return;
                }
                else
                {
                    ((Anticipo)e.Row.Item).ConsecutivoRendicion = _CurrentInstance.Consecutivo;
                }
                //}
                materializarAdelanto(new LibXmlDataParse(doc), (Anticipo)e.Row.Item, new Brl.CajaChica.clsAnticipoNav());
                calculaTotalAdelantos();
                AddEmptyRow(((ObservableCollection<Anticipo>)((DataGrid)sender).ItemsSource), true);
                //((DataGrid)sender).InvalidateVisual();
                //((DataGrid)sender).UpdateLayout();
            }
            else
            {
                if (e.Row.Item.GetType().GetProperty("Fecha").GetValue(e.Row.Item, null).Equals(Activator.CreateInstance(e.Row.Item.GetType().GetProperty("Fecha").GetValue(e.Row.Item, null).GetType())))
                {
                    ((TextBox)e.EditingElement).Text = "0";
                }
                else
                {
                    ((TextBox)e.EditingElement).Text = ValorOriginal.ToString();
                }
                return;
            }
        }

        private void grid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var column = ((DataGrid)sender).CurrentCell.Column;
            var content = ((TextBlock)((DataGridTextColumn)column).GetCellContent(e.Row)).Text.ToString();
            var index = ((DataGrid)sender).SelectedIndex;
            if ((content != String.Empty))
            {
            }
        }

        private void grid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
        }

        private void grid_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
        }

        private void grid_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
        }

        private void limpiar(ObservableCollection<object> list, int value)
        {
            var elements = from e in list
                           where ((AdelantoHelper)e).Consecutivo == value
                           select e;
        }

        private void grid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            AddEmptyRow(((ObservableCollection<Anticipo>)((DataGrid)sender).ItemsSource), true);
        }

        private void AddEmptyRow(ObservableCollection<Anticipo> coll, bool validateEmptyRowExist)
        {
            if (validateEmptyRowExist)
            {
                if (!coll.Any(a => a.Fecha.Year == 1))
                {
                    coll.Add(new Anticipo());
                }
            }
            else
            {
                coll.Add(new Anticipo());
            }
        }

        private void grid_KeyDown(object sender, KeyEventArgs e)
        {



        }

        private void grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((eAccionSR.Insertar.Equals(Action) | eAccionSR.Modificar.Equals(Action)) ? false : true)
            {
                e.Handled = true;
                return;
            }
            if (e.Key == Key.Delete)
            {
                var result = LibNotifier.AcceptCancelMessage(null, "Desea borrar el(los) Registro(s) Seleccionado(s)?", "Advertencia", "Borrar Adelantos", TextAlignment.Center);
                if (!result)
                {
                    e.Handled = true;
                }
                else
                {
                    foreach (var aux in grid.SelectedItems)
                    {
                        ((Anticipo)aux).ConsecutivoRendicion = 0;
                    }
                    grid.CommitEdit();
                    CollectionViewSource.GetDefaultView(_CurrentInstance.Adelantos).Refresh();
                    e.Handled = true;
                    calculaTotalAdelantos();
                    //     txtBeneficiario.Focus();
                }
            }
        }

        private void grid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            ListSortDirection direction = (e.Column.SortDirection != ListSortDirection.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
            e.Column.SortDirection = direction;
            ListCollectionView lcv = (ListCollectionView)CollectionViewSource.GetDefaultView(((DataGrid)sender).ItemsSource);
            OrderBlank comparer = new OrderBlank();
            comparer.Campo = e.Column.SortMemberPath;
            comparer.Direccion = e.Column.SortDirection.Value;
            lcv.CustomSort = comparer;
            e.Handled = true;
        }

        private void txtConsecutivo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
        }

        private void DetalleRendicionUc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RealizaLosCalculos();
        }

        void DeleteRow(object sender, RoutedEventArgs e)
        {
            try
            {
                Anticipo vDetailTmp = ((sender as Button).Tag as Anticipo);
                ObservableCollection<Anticipo> vCollection = DataContext as ObservableCollection<Anticipo>;
                borrarItem(grid);
                grid.CommitEdit();
                CollectionViewSource.GetDefaultView(_CurrentInstance.Adelantos).Refresh();

                // txtNumeroDocumento.Focus();
            }
            catch (GalacException gEx)
            {
                LibExceptionDisplay.Show(gEx, this.Title);
            }
            catch (Exception vEx)
            {
                if (vEx is System.AccessViolationException)
                {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void borrarItem(DataGrid aux)
        {
            if (aux.SelectedItems.Count > 0)
            {
                //  var result = LibNotifier.AcceptCancelMessage(null, "Desear borrar los Registros seleccionados?", "Adverencia", "Borrar Registros", TextAlignment.Center);
                var result = true;
                if (result)
                {
                    var arr = ((System.Collections.IList)aux.SelectedItems).Cast<Anticipo>().ToList();
                    foreach (Anticipo ll in arr)
                    {
                        ll.ConsecutivoRendicion = 0;
                    }
                }
            }
        }

        private void txtDescripcion_LostFocus(object sender, RoutedEventArgs e)
        {
            //DetalleRendicionUc.Focus();
        }

        } //End of class GSRendicion.xaml

        #endregion
        #region Clases creadas
    public class Converter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            Type t = value.GetType();
            if (t.IsValueType)
            {
                if (value.Equals(Activator.CreateInstance(t)))
                    return "";
                else
                    return value;
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            return value;
        }
    }
    public class CollectionSumConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return 0.0;
            ICollection aux = (ICollection)value;
            var total = 0.0;
            MessageBox.Show(aux.Count.ToString());
            foreach (object o in aux)
            {
                total += (double)o.GetType().GetProperty(parameter.ToString()).GetValue(o, null);
            }
            MessageBox.Show(total.ToString());
            return total;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
    public class OrderBlank : IComparer
    {

        private string campo;


        public string Campo
        {
            get { return campo; }
            set { campo = value; }
        }

        private ListSortDirection direccion;

        public ListSortDirection Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }


        public int Compare(object x, object y)
        {
            if (x.GetType().GetProperty("Fecha").GetValue(x, null).Equals(Activator.CreateInstance(x.GetType().GetProperty("Fecha").GetValue(x, null).GetType())))
            {
                return 1;
            }
            else
            {
                if (y.GetType().GetProperty("Fecha").GetValue(y, null).Equals(Activator.CreateInstance(y.GetType().GetProperty("Fecha").GetValue(y, null).GetType())))
                {
                    return -1;
                }
            }
            if (Direccion.Equals(ListSortDirection.Descending))
                return (x.GetType().GetProperty(Campo).GetValue(x, null).ToString()).CompareTo(y.GetType().GetProperty(Campo).GetValue(y, null).ToString());
            else
                return -1 * (x.GetType().GetProperty(Campo).GetValue(x, null).ToString()).CompareTo(y.GetType().GetProperty(Campo).GetValue(y, null).ToString());
        }
    }
    public class DataGridIndexConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            string result;
            DataGridRow item = (DataGridRow)value;
            DataGrid dataGrid = ItemsControl.ItemsControlFromItemContainer(item) as DataGrid;
            int index = dataGrid.ItemContainerGenerator.IndexFromContainer(item) + 1;

            //  if (dataGrid.Items.Count == index) {
            //    result = "*";
            //} else {
            result = index.ToString();
            //}

            return result;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    #endregion
    
} //End of namespace Galac.Adm.Uil.CajaChica

