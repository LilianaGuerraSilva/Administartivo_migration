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
using Entity = Galac.Saw.Ccl.Vehiculo;

namespace Galac.Saw.Uil.Vehiculo.Input {
    /// <summary>
    /// Lógica de interacción para GSVehiculo.xaml
    /// </summary>
    internal partial class GSVehiculo: UserControl {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        Entity.Vehiculo _CurrentInstance;
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
        internal Entity.Vehiculo CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        internal ILibView CurrentModel {
            get { return _CurrentModel; }
            set { _CurrentModel = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSVehiculo() {
            InitializeComponent();
            InitializeEvents();
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Consecutivo, Placa";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, ILibView initModel, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (Entity.Vehiculo)initInstance;
            _CurrentModel = initModel;
            Title = initModel.MessageName;
            Action = initAction;
            ExtendedAction = initExtendedAction;
            LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar });
            if (Action == eAccionSR.Insertar) {
                SetFormValuesFromNavigator(true);
                lblConsecutivo.Visibility = Visibility.Hidden;
                lneConsecutivo.Visibility = Visibility.Hidden;
            } else {
                SetFormValuesFromNavigator(false);
            }
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
            }
        }

        internal void SetNavigatorValuesFromForm() {
            _CurrentInstance.Placa = txtPlaca.Text;
            _CurrentInstance.serialVIN = txtserialVIN.Text;
            _CurrentInstance.NombreModelo = txtNombreModelo.Text;
            _CurrentInstance.Marca = lneMarca.Content.ToString();
            _CurrentInstance.Ano = LibConvert.ToInt(LibConvert.ToDec(txtAno.Text));
            _CurrentInstance.CodigoColor = txtCodigoColor.Text;
            _CurrentInstance.DescripcionColor = lneDescripcionColor.Content.ToString();
            _CurrentInstance.CodigoCliente = txtCodigoCliente.Text;
            _CurrentInstance.NombreCliente = lneNombreCliente.Content.ToString();
            _CurrentInstance.RIFCliente = lneRIFCliente.Content.ToString();
            _CurrentInstance.NumeroPoliza = txtNumeroPoliza.Text;
            _CurrentInstance.SerialMotor = txtSerialMotor.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            if (valClearRecord) {
                _CurrentModel.Clear(_CurrentInstance);
            }
            ClearControl();
            lneConsecutivo.Content = _CurrentInstance.Consecutivo;
            txtPlaca.Text = _CurrentInstance.Placa;
            txtserialVIN.Text = _CurrentInstance.serialVIN;
            txtNombreModelo.Text = _CurrentInstance.NombreModelo;
            lneMarca.Content = _CurrentInstance.Marca;
            txtAno.Text = LibConvert.ToStr(_CurrentInstance.Ano);
            txtCodigoColor.Text = _CurrentInstance.CodigoColor;
            lneDescripcionColor.Content = _CurrentInstance.DescripcionColor;
            txtCodigoCliente.Text = _CurrentInstance.CodigoCliente;
            lneNombreCliente.Content = _CurrentInstance.NombreCliente;
            lneRIFCliente.Content = _CurrentInstance.RIFCliente;
            txtNumeroPoliza.Text = _CurrentInstance.NumeroPoliza;
            txtSerialMotor.Text = _CurrentInstance.SerialMotor;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtPlaca.Validating += new System.ComponentModel.CancelEventHandler(txtPlaca_Validating);
            this.txtNombreModelo.Validating += new System.ComponentModel.CancelEventHandler(txtNombreModelo_Validating);
            this.txtCodigoColor.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoColor_Validating);
            this.txtCodigoCliente.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoCliente_Validating);
        }

        void txtPlaca_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsVehiculoIpl insVehiculoIpl = new clsVehiculoIpl(((clsVehiculoIpl)CurrentModel).AppMemoryInfo, ((clsVehiculoIpl)CurrentModel).Mfc);
                if (!insVehiculoIpl.IsValidPlaca(Action, txtPlaca.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insVehiculoIpl.Information.ToString(), _CurrentModel.MessageName);
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

        void txtNombreModelo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtNombreModelo.Text)==0) {
                    txtNombreModelo.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Saw.Gv_Modelo_B1.Nombre=" + txtNombreModelo.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Vehiculo.clsVehiculoList.ChooseModelo(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtNombreModelo.Text = insParse.GetString(0, "Nombre", "");
                    lneMarca.Content = insParse.GetString(0, "Marca", "");
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

        void txtCodigoColor_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCodigoColor.Text)==0) {
                    txtCodigoColor.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Saw.Gv_Color_B1.CodigoColor=" + txtCodigoColor.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "ConsecutivoCompania=" + _CurrentInstance.ConsecutivoCompania;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Vehiculo.clsVehiculoList.ChooseColor(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCodigoColor.Text = insParse.GetString(0, "CodigoColor", "");
                    lneDescripcionColor.Content  = insParse.GetString(0, "DescripcionColor", "");
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

        void txtCodigoCliente_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCodigoCliente.Text)==0) {
                    txtCodigoCliente.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "dbo.Gv_Cliente_B1.Codigo=" + txtCodigoCliente.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "ConsecutivoCompania=" + _CurrentInstance.ConsecutivoCompania;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Saw.Uil.Vehiculo.clsVehiculoList.ChooseCliente(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCodigoCliente.Text = insParse.GetString(0, "Codigo", "");
                    lneNombreCliente.Content  = insParse.GetString(0, "Nombre", "");
                    lneRIFCliente.Content  = insParse.GetString(0, "NumeroRIF", "");
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


    } //End of class GSVehiculo.xaml

} //End of namespace Galac.Saw.Uil.Vehiculo

