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
    internal partial class GSReposicion : UserControl { 
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

        public GSReposicion() {
            InitializeComponent();
            InitializeEvents();
            // cmbTipoDeDocumento.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeDocumentoRendicion)));
            //lblStatusRendicionText|.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eStatusRendicion)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Numero";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, ILibView initModel, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            txtNumero.IsEnabled = false;
            _CurrentInstance = (Rendicion)initInstance;
            _CurrentModel = initModel;
            Title = initModel.MessageName;
            Action = initAction;
            ExtendedAction = initExtendedAction;
            LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.ReImprimir, (int)eAccionSR.Anular, (int)eAccionSR.Cerrar, (int)eAccionSR.Contabilizar, (int)eAccionSR.Eliminar });
            LibApiAwp.EnableControl(txtNumero, Action == eAccionSR.Insertar);
            if (Action == eAccionSR.Insertar) {
                SetFormValuesFromNavigator(true);
                ((clsReposicionIpl)initModel).InitDetailForInsert();
                _CurrentInstance.Adelantos = new ObservableCollection<Anticipo>();
                _CurrentInstance.DetailDetalleDeRendicion.Add(new DetalleDeRendicion());
                txtTotalGastosCalculo.Value = 0;
                txtTotalSaldo.Value = 0;
                _CurrentInstance.Consecutivo =  Int32.Parse(generarSiguiente("Consecutivo"));
                txtNumero.Text = generarSiguiente("Numero").ToString();
            } else {

                if (_CurrentInstance.DetailDetalleDeRendicion.Count == 0)
                    _CurrentInstance.DetailDetalleDeRendicion.Add(new DetalleDeRendicion());
                SetFormValuesFromNavigator(false);
                string xml = _CurrentInstance.Datos.InnerXml;
                if (xml != String.Empty) {
                    CargarAdelantos(new LibXmlDataParse(xml),new Galac.Adm.Brl.CajaChica.clsAnticipoNav(),_CurrentInstance.Adelantos);
                }
                if((eAccionSR.Insertar.Equals(initAction) | eAccionSR.Modificar.Equals(initAction)) ? false : true)
                txtCodigoCuentaBancaria.IsEnabled = false;
                txtCodigoConceptoBancario.IsEnabled = false;
                dtpFechaCierre.IsEnabled = false;
                txtNumeroDocumento.IsEnabled = false;
                dtpFechaAnulacion.IsEnabled = false;
                txtBeneficiarioCheque.IsEnabled = false;
                if (Action == eAccionSR.Cerrar) {
                    tbiSeccionCierre.Visibility = Visibility.Visible;
                  //  tbiSeccionCierre.BringIntoView();
                    tabControl.SelectedItem = tbiSeccionCierre;
                    txtCodigoCuentaBancaria.IsEnabled = true;
                    txtCodigoConceptoBancario.IsEnabled = true;
                    dtpFechaCierre.IsEnabled = true;
                    txtNumeroDocumento.IsEnabled = true;
                    txtBeneficiarioCheque.IsEnabled = true;
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
            DetalleRendicionUc.DataContext = _CurrentInstance.DetailDetalleDeRendicion;
            DetalleRendicionUc.InitializeControl(initInstance, initModel, initAction, initExtendedAction);
            //DetalleRendicionUc.ConsecutivoCompania = ((clsRendicionIpl)initModel).AppMemoryInfo.GlobalValuesGetInt("RecordName", "Compania");
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

            if (_CurrentInstance.StatusRendicionAsEnum.Equals(eStatusRendicion.Cerrada))
                lblStatusRendicionText.Foreground = new SolidColorBrush(Colors.Red);
            else if (_CurrentInstance.StatusRendicionAsEnum.Equals(eStatusRendicion.EnProceso))
                lblStatusRendicionText.Foreground = new SolidColorBrush(Colors.Blue);
            else
                lblStatusRendicionText.Foreground = new SolidColorBrush(Colors.Gray);
        }

        internal void SetNavigatorValuesFromForm() {
		    _CurrentInstance.Numero = txtNumero.Text;
            string vParamsInitializationList;
            string vParamsFixedList = "";

            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            vParamsInitializationList = "Gv_Beneficiario_B1.Consecutivo=" + "1" + LibText.ColumnSeparator();
            vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
            vParamsFixedList = "Gv_Beneficiario_B1.ConsecutivoCompania=" + _CurrentInstance.ConsecutivoCompania;
            vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
            XmlDocument XmlProperties = new XmlDocument();
            if (Galac.Adm.Uil.CajaChica.clsRendicionList.ChooseBeneficiario(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                _CurrentInstance.NombreBeneficiario = insParse.GetString(0, "NombreBeneficiario", "");
                _CurrentInstance.ConsecutivoBeneficiario = Int32.Parse(insParse.GetString(0, "Consecutivo", ""));
            }
            _CurrentInstance.FechaApertura = dtpFechaApertura.Date;
            _CurrentInstance.FechaCierre = dtpFechaCierre.Date;
            _CurrentInstance.FechaAnulacion = dtpFechaAnulacion.Date;
            _CurrentInstance.TotalGastos = txtTotalGastosCalculo.Value;
            _CurrentInstance.TotalIVA = txtTotalIVA.Value;
            _CurrentInstance.CodigoCuentaBancaria = txtCodigoCuentaBancaria.Text;
            _CurrentInstance.NombreCuentaBancaria = lneNombreCuentaBancaria.Content.ToString();
            _CurrentInstance.CodigoConceptoBancario = txtCodigoConceptoBancario.Text;
            _CurrentInstance.NombreConceptoBancario = lneNombreConceptoBancario.Content.ToString();
            _CurrentInstance.NumeroDocumento = txtNumeroDocumento.Text;
            _CurrentInstance.NombreBeneficiario = txtBeneficiarioCheque.Text;
            _CurrentInstance.CodigoCtaBancariaCajaChica = txtCodigoCtaBancariaCajaChica.Text;
            _CurrentInstance.NombreCuentaBancariaCajaChica = lblNombreCtaBancariaCajaChica.Content.ToString();
            _CurrentInstance.Descripcion = txtDescripcion.Text;
            _CurrentInstance.Observaciones = txtObservaciones.Text;
            _CurrentInstance.TipoDeDocumentoAsEnum = eTipoDeDocumentoRendicion.Reposicion;
            _CurrentInstance.BeneficiarioCheque = txtBeneficiarioCheque.Text;

            if (Action.Equals(eAccionSR.Insertar) || Action.Equals(eAccionSR.Modificar))
            {
                _CurrentInstance.FechaCierre = _CurrentInstance.FechaApertura;
                _CurrentInstance.FechaAnulacion = _CurrentInstance.FechaApertura;
            }
	    }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            if (valClearRecord) {
                _CurrentModel.Clear(_CurrentInstance);
            }
            ClearControl();
			txtNumero.Text = _CurrentInstance.Numero;
            txtNumero.Text = _CurrentInstance.Numero.ToString();
            dtpFechaApertura.Text = _CurrentInstance.FechaApertura.ToShortDateString();
            dtpFechaCierre.Text = _CurrentInstance.FechaCierre.ToShortDateString();
            dtpFechaAnulacion.Text = _CurrentInstance.FechaAnulacion.ToShortDateString();
            lblStatusRendicionText.Content = _CurrentInstance.StatusRendicionAsString;
            txtTotalGastosCalculo.Value = _CurrentInstance.TotalGastos;
            txtTotalIVA.Value = _CurrentInstance.TotalIVA;
            txtCodigoCuentaBancaria.Text = _CurrentInstance.CodigoCuentaBancaria;
            lneNombreCuentaBancaria.Content = _CurrentInstance.NombreCuentaBancaria;
            txtCodigoConceptoBancario.Text = _CurrentInstance.CodigoConceptoBancario;
            lneNombreConceptoBancario.Content = _CurrentInstance.NombreConceptoBancario;
            txtNumeroDocumento.Text = _CurrentInstance.NumeroDocumento;
            txtBeneficiarioCheque.Text = _CurrentInstance.BeneficiarioCheque;
            txtCodigoCtaBancariaCajaChica.Text = _CurrentInstance.CodigoCtaBancariaCajaChica;
            txtDescripcion.Text = _CurrentInstance.Descripcion;
            txtObservaciones.Text = _CurrentInstance.Observaciones;
            lblNombreCtaBancariaCajaChica.Content = _CurrentInstance.NombreCuentaBancariaCajaChica;
            RealizaLosCalculos();
            
        }

        private void InitializeEvents() {
            //    this.lneConsecutivo.Validating += new System.ComponentModel.CancelEventHandler(lneConsecutivo_Validating);
			this.txtNumero.Validating += new System.ComponentModel.CancelEventHandler(txtNumero_Validating);
            //    this.cmbTipoDeDocumento.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoDeDocumento_Validating);
            //this.cmbStatusRendicion.Validating += new System.ComponentModel.CancelEventHandler(cmbStatusRendicion_Validating);
            this.txtCodigoCuentaBancaria.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoCuentaBancaria_Validating);
            this.txtNumeroDocumento.Validating += new System.ComponentModel.CancelEventHandler(txtNumeroDocumento_Validating);
            this.txtBeneficiarioCheque.Validating += new System.ComponentModel.CancelEventHandler(txtBeneficiarioCheque_Validating);
            this.txtCodigoCtaBancariaCajaChica.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoCtaBancariaCajaChica_Validating);
            //this.txtCodigoConceptoBancario.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoConceptoBancario_Validating);
        }

        void lneConsecutivo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            //try {
            //    if (CancelValidations) {
            //        return;
            //    }
            //    clsReposicionIpl insRendicionIpl = new clsReposicionIpl (((clsReposicionIpl )CurrentModel).AppMemoryInfo, ((clsReposicionIpl )CurrentModel).Mfc);
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
                clsReposicionIpl insReposicionIpl = new clsReposicionIpl(((clsReposicionIpl)CurrentModel).AppMemoryInfo, ((clsReposicionIpl)CurrentModel).Mfc);
                if (!insReposicionIpl.IsValidNumero(Action, txtNumero.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insReposicionIpl.Information.ToString(), _CurrentModel.MessageName);
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
                    if(!CurrentInstance.CodigoCuentaBancaria.Equals(string.Empty))
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
                vParamsInitializationList += "Gv_CuentaBancaria_B1.CodigoMoneda=" + "VEF" + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "Gv_CuentaBancaria_B1.ConsecutivoCompania=" + _CurrentInstance.ConsecutivoCompania;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Adm.Uil.CajaChica.clsRendicionList.ChooseCuentaBancaria(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    if (insParse.GetString(0, "CodigoMoneda", "").Equals("VEF"))
                    {
                        txtCodigoCuentaBancaria.Text = insParse.GetString(0, "Codigo", "");
                        lneNombreCuentaBancaria.Content = insParse.GetString(0, "NombreCuenta", "");
                    }
                    else {
                        e.Cancel = true;
                    }
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
                    if (!CurrentInstance.NumeroDocumento.Equals(string.Empty))
                    return;
                }
                clsReposicionIpl insRendicionIpl = new clsReposicionIpl(((clsReposicionIpl)CurrentModel).AppMemoryInfo, ((clsReposicionIpl)CurrentModel).Mfc);
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
                    if (!CurrentInstance.BeneficiarioCheque.Equals(string.Empty))
                    return;
                }
                clsReposicionIpl insRendicionIpl = new clsReposicionIpl (((clsReposicionIpl )CurrentModel).AppMemoryInfo, ((clsReposicionIpl )CurrentModel).Mfc);
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

        void txtCodigoCtaBancariaCajaChica_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCodigoCtaBancariaCajaChica.Text) == 0) {
                    txtCodigoCtaBancariaCajaChica.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_CuentaBancaria_B1.Codigo=" + txtCodigoCtaBancariaCajaChica.Text + LibText.ColumnSeparator();
                vParamsInitializationList += "Gv_CuentaBancaria_B1.EsCajaChica=" + LibConvert.BoolToSN(true) + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "Gv_CuentaBancaria_B1.ConsecutivoCompania=" + _CurrentInstance.ConsecutivoCompania;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Adm.Uil.CajaChica.clsRendicionList.ChooseCuentaBancaria(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCodigoCtaBancariaCajaChica.Text = insParse.GetString(0, "Codigo", "");
                    lblNombreCtaBancariaCajaChica.Content = insParse.GetString(0, "NombreCuenta", "");
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


        //void txtCodigoConceptoBancario_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
        //    try {
        //        if (CancelValidations) {
        //            if(!CurrentInstance.CodigoConceptoBancario.Equals(string.Empty))
        //            return;
        //        }
        //        if (LibString.Len(txtCodigoConceptoBancario.Text) == 0) {
        //            txtCodigoConceptoBancario.Text = "*";
        //        }
        //        string vParamsInitializationList;
        //        LibSearch insLibSearch = new LibSearch();
        //        List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
        //        List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
        //        vParamsInitializationList = "Gv_ConceptoBancario_B1.Codigo=" + txtCodigoConceptoBancario.Text + LibText.ColumnSeparator();
        //        if (txtTotalSaldo.Value > 0) {
        //            vParamsInitializationList += "Gv_ConceptoBancario_B1.Tipo=" + (int)eIngresoEgreso.Ingreso + LibText.ColumnSeparator();
        //        } else if (txtTotalSaldo.Value < 0) {
        //            vParamsInitializationList += "Gv_ConceptoBancario_B1.Tipo=" + (int)eIngresoEgreso.Egreso + LibText.ColumnSeparator();
        //        }
        //        vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
        //        XmlDocument XmlProperties = new XmlDocument();
        //        if (Galac.Adm.Uil.CajaChica.clsConceptoBancarioList.ChooseConceptoBancario(null, ref XmlProperties, vSearchValues, vFixedValues)) {
        //            LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
        //            txtCodigoConceptoBancario.Text = insParse.GetString(0, "Codigo", "");
        //            lneNombreConceptoBancario.Content = insParse.GetString(0, "Descripcion", "");
        //        } else {
        //            e.Cancel = true;
        //        }
        //    } catch (GalacException gEx) {
        //        LibExceptionDisplay.Show(gEx, this.Title);
        //    } catch (Exception vEx) {
        //        if (vEx is System.AccessViolationException) {
        //            throw;
        //        }
        //        LibExceptionDisplay.Show(vEx);
        //    }
        //}

      
                
        private void RealizaLosCalculos() {
           
            txtTotalSaldo.Value = DetalleRendicionUc.txtTotalFacturas.Value;
            txtTotalExento.Value = DetalleRendicionUc.TotalExento;
            txtTotalGravable.Value = DetalleRendicionUc.TotalGravable;
            txtTotalIVA.Value = DetalleRendicionUc.TotalIVA;
        }

        #endregion //Metodos Generados
        #region Metodos Creados
        private void calculaTotalAdelantos()
        {
            RealizaLosCalculos();
        }

        private void txtConsecutivo_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private string generarSiguiente(string Campo)
        {
            try
            {
                return LibConvert.ToStr(_CurrentModel.NextSequential(Campo));
            }
            catch (GalacException gEx)
            {
                LibExceptionDisplay.Show(gEx, this.Title);
                return "-1";
            }
            catch (Exception vEx)
            {
                if (vEx is System.AccessViolationException)
                {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
                return "-1";
            }
        }


        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

            //if (((TextBox)e.Source).Text == String.Empty) {
            //    ((TextBox)e.Source).IsReadOnly = false;
            //}

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


        private void limpiar(ObservableCollection<object> list, int value)
        {
            var elements = from e in list
                           where ((AdelantoHelper)e).Consecutivo == value
                           select e;

        }


        private void txtConsecutivo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
        }

        private void DetalleRendicionUc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RealizaLosCalculos();
        }

        private void dtpFechaApertura_Validating(object sender, LibGalac.Aos.UI.WpfControls.DatePickerCancelEventArgs e)
        {
            try
            {
                if (CancelValidations)
                {
                    return;
                }
                clsReposicionIpl insReposicionIpl = (clsReposicionIpl)_CurrentModel;
                if (!insReposicionIpl.IsValidFechaApertura(Action, dtpFechaApertura.Date, true))
                {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insReposicionIpl.Information.ToString(), _CurrentModel.MessageName);
                    e.Cancel = true;
                }
                else
                {
                    dtpFechaCierre.Date = dtpFechaApertura.Date;
                    dtpFechaAnulacion.Date = dtpFechaApertura.Date;
                }

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

        private void DetalleRendicionUc_Loaded(object sender, RoutedEventArgs e)
        {

        } 
        #endregion

        

       


        } //End of class GSRendicion.xaml
   
    
} //End of namespace Galac.Adm.Uil.CajaChica

