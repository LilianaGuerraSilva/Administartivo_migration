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
    /// Lógica de interacción para GSComisionesStt.xaml
    /// </summary>
    internal partial class GSComisionesStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        ComisionesStt _CurrentInstance;
        
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
        internal ComisionesStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSComisionesStt() {
            InitializeComponent();
            InitializeEvents();
            cmbFormaDeCalcularComisionesSobreCobranza.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eCalculoParaComisionesSobreCobranzaEnBaseA)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Forma De Calcular Comisiones Sobre Cobranza";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (ComisionesStt)initInstance;
            
            Title = "Comisiones";
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
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);
                if (insSettValueByCompanyIpl.EsSistemaParaIG()) {
                    lblFormaDeCalcularComisionesSobreCobranza.Visibility = System.Windows.Visibility.Hidden;
                    cmbFormaDeCalcularComisionesSobreCobranza.Visibility = System.Windows.Visibility.Hidden;
                }
                lblNombrePlantillaComisionSobreCobranza.Visibility = System.Windows.Visibility.Hidden;
                txtNombrePlantillaComisionSobreCobranza.Visibility = System.Windows.Visibility.Hidden;
                btnBuscarRpxComisionSobreCobranza.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.FormaDeCalcularComisionesSobreCobranzaAsEnum = (eCalculoParaComisionesSobreCobranzaEnBaseA) cmbFormaDeCalcularComisionesSobreCobranza.SelectedItemToInt();
            _CurrentInstance.NombrePlantillaComisionSobreCobranza = txtNombrePlantillaComisionSobreCobranza.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            cmbFormaDeCalcularComisionesSobreCobranza.SelectItem(_CurrentInstance.FormaDeCalcularComisionesSobreCobranzaAsEnum);
            txtNombrePlantillaComisionSobreCobranza.Text = _CurrentInstance.NombrePlantillaComisionSobreCobranza;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.cmbFormaDeCalcularComisionesSobreCobranza.Validating += new System.ComponentModel.CancelEventHandler(cmbFormaDeCalcularComisionesSobreCobranza_Validating);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
            this.txtNombrePlantillaComisionSobreCobranza.Validating += new CancelEventHandler(txtNombrePlantillaComisionSobreCobranza_Validating);
            this.btnBuscarRpxComisionSobreCobranza.Click +=new RoutedEventHandler(btnBuscarRpxComisionSobreCobranza_Click);
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


        void cmbFormaDeCalcularComisionesSobreCobranza_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbFormaDeCalcularComisionesSobreCobranza.ValidateTextInCombo();
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

        void btnBuscarRpxComisionSobreCobranza_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxComisionSobreCobranza();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void BuscarRpxComisionSobreCobranza() {
            string paramBusqueda = "rpx de Comisiones por Cobranza (*.rpx)|*Comision*.rpx";
            txtNombrePlantillaComisionSobreCobranza.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }

        void txtNombrePlantillaComisionSobreCobranza_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaComisionSobreCobranza.Text) == 0) {
                txtNombrePlantillaComisionSobreCobranza.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaComisionSobreCobranza.Text)) {
                BuscarRpxComisionSobreCobranza();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaComisionSobreCobranza.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaComisionSobreCobranza.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }

    } //End of class GSComisionesStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

