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
    /// Lógica de interacción para GSFacturacionContinuacionStt.xaml
    /// </summary>
    internal partial class GSFacturacionContinuacionStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        FacturacionContinuacionStt _CurrentInstance;
        
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
        internal FacturacionContinuacionStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSFacturacionContinuacionStt() {
            InitializeComponent();
            InitializeEvents();
            cmbMesFacturacionEnCurso.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eMes)));
            cmbAccionAlAnularFactDeMesesAnt.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eAccionAlAnularFactDeMesesAnt)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (FacturacionContinuacionStt)initInstance;

            Title = "Facturacion Continuación";
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
                    label4.Visibility = System.Windows.Visibility.Hidden;
                    chkUsarOtrosCargoDeFactura.Visibility = System.Windows.Visibility.Hidden;
                }
                 LibApiAwp.EnableControl(chkUsaCamposExtrasEnRenglonFactura, !(bool)chkUsaCamposExtrasEnRenglonFactura.IsChecked);
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.ForzarFechaFacturaAmesEspecificoAsBool = chkForzarFechaFacturaAmesEspecifico.IsChecked.Value;
            _CurrentInstance.MesFacturacionEnCursoAsEnum = (eMes) cmbMesFacturacionEnCurso.SelectedItemToInt();
            _CurrentInstance.PermitirIncluirFacturacionHistoricaAsBool = chkPermitirIncluirFacturacionHistorica.IsChecked.Value;
            _CurrentInstance.UltimaFechaDeFacturacionHistorica = dtpUltimaFechaDeFacturacionHistorica.Date;
            _CurrentInstance.GenerarCxCalEmitirUnaFacturaHistoricaAsBool = chkGenerarCxCalEmitirUnaFacturaHistorica.IsChecked.Value;
            _CurrentInstance.AccionAlAnularFactDeMesesAntAsEnum = (eAccionAlAnularFactDeMesesAnt) cmbAccionAlAnularFactDeMesesAnt.SelectedItemToInt();
            _CurrentInstance.UsarOtrosCargoDeFacturaAsBool = chkUsarOtrosCargoDeFactura.IsChecked.Value;
            _CurrentInstance.UsaCamposExtrasEnRenglonFacturaAsBool = chkUsaCamposExtrasEnRenglonFactura.IsChecked.Value;
            _CurrentInstance.EmitirDirectoAsBool = chkEmitirDirecto.IsChecked.Value;
            _CurrentInstance.UsaCobroDirectoAsBool = chkUsaCobroDirecto.IsChecked.Value;
            _CurrentInstance.CuentaBancariaCobroDirecto = txtCuentaBancariaCobroDirecto.Text;
            _CurrentInstance.ConceptoBancarioCobroDirecto = txtConceptoBancarioCobroDirecto.Text;
            _CurrentInstance.PermitirDobleDescuentoEnFacturaAsBool = chkPermitirDobleDescuentoEnFactura.IsChecked.Value;
            _CurrentInstance.MaximoDescuentoEnFactura = txtMaximoDescuentoEnFactura.Value;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            chkForzarFechaFacturaAmesEspecifico.IsChecked = _CurrentInstance.ForzarFechaFacturaAmesEspecificoAsBool;
            cmbMesFacturacionEnCurso.SelectItem(_CurrentInstance.MesFacturacionEnCursoAsEnum);
            chkPermitirIncluirFacturacionHistorica.IsChecked = _CurrentInstance.PermitirIncluirFacturacionHistoricaAsBool;
            dtpUltimaFechaDeFacturacionHistorica.Date = _CurrentInstance.UltimaFechaDeFacturacionHistorica;
            chkGenerarCxCalEmitirUnaFacturaHistorica.IsChecked = _CurrentInstance.GenerarCxCalEmitirUnaFacturaHistoricaAsBool;
            cmbAccionAlAnularFactDeMesesAnt.SelectItem(_CurrentInstance.AccionAlAnularFactDeMesesAntAsEnum);
            chkUsarOtrosCargoDeFactura.IsChecked = _CurrentInstance.UsarOtrosCargoDeFacturaAsBool;
            chkUsaCamposExtrasEnRenglonFactura.IsChecked = _CurrentInstance.UsaCamposExtrasEnRenglonFacturaAsBool;
            chkEmitirDirecto.IsChecked = _CurrentInstance.EmitirDirectoAsBool;
            chkUsaCobroDirecto.IsChecked = _CurrentInstance.UsaCobroDirectoAsBool;
            txtCuentaBancariaCobroDirecto.Text = _CurrentInstance.CuentaBancariaCobroDirecto;
            txtConceptoBancarioCobroDirecto.Text = _CurrentInstance.ConceptoBancarioCobroDirecto;
            chkPermitirDobleDescuentoEnFactura.IsChecked = _CurrentInstance.PermitirDobleDescuentoEnFacturaAsBool;
            txtMaximoDescuentoEnFactura.Text = LibConvert.ToStr(_CurrentInstance.MaximoDescuentoEnFactura);
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.cmbMesFacturacionEnCurso.Validating += new System.ComponentModel.CancelEventHandler(cmbMesFacturacionEnCurso_Validating);
            this.cmbAccionAlAnularFactDeMesesAnt.Validating += new System.ComponentModel.CancelEventHandler(cmbAccionAlAnularFactDeMesesAnt_Validating);
            this.txtCuentaBancariaCobroDirecto.Validating += new System.ComponentModel.CancelEventHandler(txtCuentaBancariaCobroDirecto_Validating);
            this.txtConceptoBancarioCobroDirecto.Validating += new System.ComponentModel.CancelEventHandler(txtConceptoBancarioCobroDirecto_Validating);
            this.txtMaximoDescuentoEnFactura.Validating += new CancelEventHandler(txtMaximoDescuentoEnFactura_Validating);
            this.chkEmitirDirecto.Click += new RoutedEventHandler(chkEmitirDirecto_Click);
            this.chkUsaCobroDirecto.Click += new RoutedEventHandler(chkUsaCobroDirecto_Click);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
        }

        void chkUsaCobroDirecto_Click(object sender, RoutedEventArgs e) {
            try {
                if (!(bool)chkUsaCobroDirecto.IsChecked) {
                    txtCuentaBancariaCobroDirecto.Text = "";
                    txtConceptoBancarioCobroDirecto.Text = "";
                } else {
                    if (ObieneCuentaBancariaGeneriaDeTabBanco() != "") {
                        txtCuentaBancariaCobroDirecto.Text = ObieneCuentaBancariaGeneriaDeTabBanco();
                    } else {
                        txtCuentaBancariaCobroDirecto.Text = "*";
                    }
                    txtConceptoBancarioCobroDirecto.Focus();
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

        void chkEmitirDirecto_Click(object sender, RoutedEventArgs e) {
            try {
                if (!(bool)chkEmitirDirecto.IsChecked) {
                    MessageBox.Show("Al no tener activado Emitir Directo, la opción de Cobro Directo será desactivada.");
                    chkUsaCobroDirecto.IsChecked = false;
                    txtCuentaBancariaCobroDirecto.Text = "";
                    txtConceptoBancarioCobroDirecto.Text = "";
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


        void cmbMesFacturacionEnCurso_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbMesFacturacionEnCurso.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbAccionAlAnularFactDeMesesAnt_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbAccionAlAnularFactDeMesesAnt.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCuentaBancariaCobroDirecto_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCuentaBancariaCobroDirecto.Text)==0) {
                    txtCuentaBancariaCobroDirecto.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_CuentaBancaria_B1.Codigo=" + txtCuentaBancariaCobroDirecto.Text + LibText.ColumnSeparator();
                vParamsInitializationList += "Gv_CuentaBancaria_B1.EsCajaChica=" + LibConvert.BoolToSN(false);
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "Gv_CuentaBancaria_B1.ConsecutivoCompania=" + clsGlobalValues.Mfc.GetInt("Compania");
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (clsSettValueByCompanyList.ChooseCuentaBancaria(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCuentaBancariaCobroDirecto.Text = insParse.GetString(0, "Codigo", "");
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

        void txtConceptoBancarioCobroDirecto_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtConceptoBancarioCobroDirecto.Text)==0) {
                    txtConceptoBancarioCobroDirecto.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Adm.Gv_ConceptoBancario_B1.codigo=" + txtConceptoBancarioCobroDirecto.Text + LibText.ColumnSeparator();
                //vParamsInitializationList += "Adm.Gv_ConceptoBancario_B1.Tipo= " + eIngresoEgreso.Ingreso.ToString() + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "Adm.Gv_ConceptoBancario_B1.TipoStr=" + eIngresoEgreso.Ingreso;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (clsSettValueByCompanyList.ChooseConceptoBancario(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtConceptoBancarioCobroDirecto.Text = insParse.GetString(0, "Codigo", "");
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

      private   void txtMaximoDescuentoEnFactura_Validating(object sender, CancelEventArgs e) {
          try {
              if (CancelValidations) {
                  return;
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

      private string ObieneCuentaBancariaGeneriaDeTabBanco() {
          var modulos = ((clsSettValueByCompanyIpl)((GSSettValueByCompany)((Grid)((HeaderedContentControl)((StackPanel)((Grid)((ContentPresenter)this.TemplatedParent).Parent).Parent).TemplatedParent).Parent).Parent).CurrentModel).ModuleList;
          var modfact = modulos[6].Groups[0].Content;
          object Result = (modfact.GetType().GetProperty("CodigoGenericoCuentaBancaria").GetValue(modfact, null));
          if (Result == null) { Result = ""; }
          return (string)Result;
      }

    } //End of class GSFacturacionContinuacionStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

