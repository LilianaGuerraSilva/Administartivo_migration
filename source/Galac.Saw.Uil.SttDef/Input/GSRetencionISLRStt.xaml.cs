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
    /// Lógica de interacción para GSRetencionISLRStt.xaml
    /// </summary>
    internal partial class GSRetencionISLRStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        RetencionISLRStt _CurrentInstance;
        
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
        internal RetencionISLRStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSRetencionISLRStt() {
            InitializeComponent();
            InitializeEvents();
            cmbEnDondeRetenerISLR.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eDondeSeEfectuaLaRetencionISLR)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Num Copias Comprobante Retencion ";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (RetencionISLRStt)initInstance;

            Title = "Retencion ISLR";
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
                chkUsaRetencion.IsEnabled = !_CurrentInstance.UsaRetencionAsBool;
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.UsaRetencionAsBool = chkUsaRetencion.IsChecked.Value;
            _CurrentInstance.NumCopiasComprobanteRetencion = LibConvert.ToInt(txtNumCopiasComprobanteRetencion.Value);
            _CurrentInstance.DiaDelCierreFiscal = LibText.FillWithCharToLeft(LibConvert.ToStr(txtDiaDelCierreFiscal.Text), "0", 2);
            _CurrentInstance.MesDelCierreFiscal = LibText.FillWithCharToLeft(LibConvert.ToStr(txtMesDelCierreFiscal.Text), "0", 2);
            _CurrentInstance.TomarEnCuentaRetencionesCeroParaARCVyRAAsBool = chkTomarEnCuentaRetencionesCeroParaARCVyRA.IsChecked.Value;
            _CurrentInstance.EnDondeRetenerISLRAsEnum = (eDondeSeEfectuaLaRetencionISLR) cmbEnDondeRetenerISLR.SelectedItemToInt();
            _CurrentInstance.NumeroRIFR = txtNumeroRIFR.Text;
            _CurrentInstance.NombreYApellidoR = txtNombreYApellidoR.Text;
            _CurrentInstance.CodTelfR = txtCodTelfR.Text;
            _CurrentInstance.TelefonoR = txtTelefonoR.Text;
            _CurrentInstance.DireccionR = txtDireccionR.Text;
            _CurrentInstance.CiudadRepLegal = txtCiudadRepLegal.Text;
            _CurrentInstance.CorreoElectronicoRepLegal = txtCorreoElectronicoRepLegal.Text;            
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            chkUsaRetencion.IsChecked = _CurrentInstance.UsaRetencionAsBool;
            txtNumCopiasComprobanteRetencion.Value = _CurrentInstance.NumCopiasComprobanteRetencion;
            txtDiaDelCierreFiscal.Text = _CurrentInstance.DiaDelCierreFiscal;
            txtMesDelCierreFiscal.Text = _CurrentInstance.MesDelCierreFiscal;
            chkTomarEnCuentaRetencionesCeroParaARCVyRA.IsChecked = _CurrentInstance.TomarEnCuentaRetencionesCeroParaARCVyRAAsBool;
            cmbEnDondeRetenerISLR.SelectItem(_CurrentInstance.EnDondeRetenerISLRAsEnum);
            txtNumeroRIFR.Text = _CurrentInstance.NumeroRIFR;
            txtNombreYApellidoR.Text = _CurrentInstance.NombreYApellidoR;
            txtCodTelfR.Text = _CurrentInstance.CodTelfR;
            txtTelefonoR.Text = _CurrentInstance.TelefonoR;
            txtDireccionR.Text = _CurrentInstance.DireccionR;
            txtCiudadRepLegal.Text = _CurrentInstance.CiudadRepLegal;
            txtCorreoElectronicoRepLegal.Text = _CurrentInstance.CorreoElectronicoRepLegal;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtNumCopiasComprobanteRetencion.Validating += new System.ComponentModel.CancelEventHandler(txtNumCopiasComprobanteRetencion_Validating);
            this.cmbEnDondeRetenerISLR.Validating += new System.ComponentModel.CancelEventHandler(cmbEnDondeRetenerISLR_Validating);
            this.txtCiudadRepLegal.Validating += new System.ComponentModel.CancelEventHandler(txtCiudadRepLegal_Validating);
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


        void txtNumCopiasComprobanteRetencion_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(null, null);
                if (!insSettValueByCompanyIpl.IsValidNumCopiasComprobanteRetencion(Action, LibConvert.ToInt(txtNumCopiasComprobanteRetencion.Value), true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettValueByCompanyIpl.Information.ToString(), Title);
                    txtNumCopiasComprobanteRetencion.Value = insSettValueByCompanyIpl.DefaultNumCopiasComprobanteRetencion(); 
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

        void cmbEnDondeRetenerISLR_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbEnDondeRetenerISLR.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCiudadRepLegal_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCiudadRepLegal.Text)==0) {
                    txtCiudadRepLegal.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_Ciudad_B1.NombreCiudad=" + txtCiudadRepLegal.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (clsSettValueByCompanyList.ChooseCiudad(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCiudadRepLegal.Text = insParse.GetString(0, "NombreCiudad", "");
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

     
 
    } //End of class GSRetencionISLRStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

