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

namespace Galac.Adm.Uil.GestionCompras.Input {
    /// <summary>
    /// Lógica de interacción para GSTablaRetencion.xaml
    /// </summary>
    internal partial class GSTablaRetencion: UserControl {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        TablaRetencion _CurrentInstance;
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
        internal TablaRetencion CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        internal ILibView CurrentModel {
            get { return _CurrentModel; }
            set { _CurrentModel = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSTablaRetencion() {
            InitializeComponent();
            InitializeEvents();
            cmbTipoDePersona.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipodePersonaRetencion)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Tipo De Persona, Código de Retención";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, ILibView initModel, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (TablaRetencion)initInstance;
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
            LibApiAwp.EnableControl(cmbTipoDePersona, Action == eAccionSR.Insertar);
            LibApiAwp.EnableControl(txtCodigo, Action == eAccionSR.Insertar);
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
            }
        }

        internal void SetNavigatorValuesFromForm() {
            _CurrentInstance.TipoDePersonaAsEnum = (eTipodePersonaRetencion) cmbTipoDePersona.SelectedItemToInt();
            _CurrentInstance.Codigo = txtCodigo.Text;
            _CurrentInstance.CodigoSeniat = txtCodigoSeniat.Text;
            _CurrentInstance.TipoDePago = txtTipoDePago.Text;
            _CurrentInstance.Comentarios = txtComentarios.Text;
            _CurrentInstance.BaseImponible = txtBaseImponible.Value;
            _CurrentInstance.Tarifa = txtTarifa.Value;
            _CurrentInstance.ParaPagosMayoresDe = txtParaPagosMayoresDe.Value;
            _CurrentInstance.FechaAplicacion = dtpFechaAplicacion.Date;
            _CurrentInstance.Sustraendo = txtSustraendo.Value;
            _CurrentInstance.AcumulaParaPJNDAsBool = chkAcumulaParaPJND.IsChecked.Value;
            _CurrentInstance.SecuencialDePlantilla = txtSecuencialDePlantilla.Text;
            _CurrentInstance.CodigoMoneda = txtCodigoMoneda.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            if (valClearRecord) {
                _CurrentModel.Clear(_CurrentInstance);
            }
            ClearControl();
            cmbTipoDePersona.SelectItem(_CurrentInstance.TipoDePersonaAsEnum);
            txtCodigo.Text = _CurrentInstance.Codigo;
            txtCodigoSeniat.Text = _CurrentInstance.CodigoSeniat;
            txtTipoDePago.Text = _CurrentInstance.TipoDePago;
            txtComentarios.Text = _CurrentInstance.Comentarios;
            txtBaseImponible.Value = _CurrentInstance.BaseImponible;
            txtTarifa.Value = _CurrentInstance.Tarifa;
            txtParaPagosMayoresDe.Value = _CurrentInstance.ParaPagosMayoresDe;
            dtpFechaAplicacion.Date = _CurrentInstance.FechaAplicacion;
            txtSustraendo.Value = _CurrentInstance.Sustraendo;
            chkAcumulaParaPJND.IsChecked = _CurrentInstance.AcumulaParaPJNDAsBool;
            txtSecuencialDePlantilla.Text = _CurrentInstance.SecuencialDePlantilla;
            txtCodigoMoneda.Text = _CurrentInstance.CodigoMoneda;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.cmbTipoDePersona.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoDePersona_Validating);
            this.txtCodigo.Validating += new System.ComponentModel.CancelEventHandler(txtCodigo_Validating);
            this.txtCodigoMoneda.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoMoneda_Validating);
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

        void txtCodigo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                Galac.Comun.Uil.TablasLey.clsTablaRetencionIpl insTablaRetencionIpl = new Galac.Comun.Uil.TablasLey.clsTablaRetencionIpl();
                if (!insTablaRetencionIpl.IsValidCodigo(Action, txtCodigo.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insTablaRetencionIpl.Information.ToString(), _CurrentModel.MessageName);
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

        void txtCodigoMoneda_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCodigoMoneda.Text)==0) {
                    txtCodigoMoneda.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Comun.Gv_Moneda_B1.Codigo=" + txtCodigoMoneda.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (Galac.Comun.Uil.TablasLey.clsTablaRetencionList.ChooseMoneda(null , ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCodigoMoneda.Text = insParse.GetString(0, "Codigo", "");
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


    } //End of class GSTablaRetencion.xaml

} //End of namespace Galac.Adm.Uil.GestionCompras

