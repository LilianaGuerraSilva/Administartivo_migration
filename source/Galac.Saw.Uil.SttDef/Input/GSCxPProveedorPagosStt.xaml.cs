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
    /// Lógica de interacción para GSCxPProveedorPagosStt.xaml
    /// </summary>
    internal partial class GSCxPProveedorPagosStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        CxPProveedorPagosStt _CurrentInstance;
        
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
        internal CxPProveedorPagosStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSCxPProveedorPagosStt() {
            InitializeComponent();
            InitializeEvents();
            cmbTipoDeOrdenDePagoAImprimir.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeOrdenDePagoAImprimir)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Longitud Codigo Proveedor, NumCopias Comprobantepago, Tipo De Orden De Pago A Imprimir";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (CxPProveedorPagosStt)initInstance;

            Title = "CxP Proveedor Pagos";
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
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(null, null);
                if (insSettValueByCompanyIpl.SePuedeRetenerParaEsteMunicipio() && insSettValueByCompanyIpl.PuedeActivarModulo()) {
                    gImpuestoMunicipal.Visibility = System.Windows.Visibility.Visible;
                    if (txtNombrePlantillaRetencionImpuestoMunicipal.Text == "") {
                        txtNombrePlantillaRetencionImpuestoMunicipal.Text = "rpxComprobanteDeRetencion" + clsGlobalValues.AppMemoryInfo.GlobalValuesGetString("Parametros", "NombreMunicipio");
                    }
                } else {
                    gImpuestoMunicipal.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.ExigirInformacionLibroDeComprasAsBool = chkExigirInformacionLibroDeCompras.IsChecked.Value;
            _CurrentInstance.UsarCodigoProveedorEnPantallaAsBool = chkUsarCodigoProveedorEnPantalla.IsChecked.Value;
            _CurrentInstance.LongitudCodigoProveedor = LibConvert.ToInt(txtLongitudCodigoProveedor.Value);
            _CurrentInstance.NumCopiasComprobantepago = LibConvert.ToInt(txtNumCopiasComprobantepago.Value);
            _CurrentInstance.NombrePlantillaComprobanteDePago = txtNombrePlantillaComprobanteDePago.Text;
            _CurrentInstance.TipoDeOrdenDePagoAImprimirAsEnum = (eTipoDeOrdenDePagoAImprimir) cmbTipoDeOrdenDePagoAImprimir.SelectedItemToInt();
            _CurrentInstance.ConfirmarImpresionPorSeccionesAsBool = chkConfirmarImpresionPorSecciones.IsChecked.Value;
            _CurrentInstance.NoImprimirComprobanteDePagoAsBool = chkNoImprimirComprobanteDePago.IsChecked.Value;
            _CurrentInstance.ImprimirComprobanteContableDePagoAsBool = chkImprimirComprobanteContableDePago.IsChecked.Value;
            _CurrentInstance.ConceptoBancarioReversoDePago = txtConceptoBancarioReversoDePago.Text;
            _CurrentInstance.AvisarSiProveedorTieneAnticiposAsBool = chkAvisarSiProveedorTieneAnticipos.IsChecked.Value;
            _CurrentInstance.OrdenarCxPPorFacturaDocumentoAsBool = chkOrdenarCxPPorFacturaDocumento.IsChecked.Value;
            _CurrentInstance.NombrePlantillaRetencionImpuestoMunicipal = txtNombrePlantillaRetencionImpuestoMunicipal.Text;
            _CurrentInstance.RetieneImpuestoMunicipalAsBool = chkRetieneImpuestoMunicipal.IsChecked.Value;
            _CurrentInstance.PrimerNumeroComprobanteRetImpuestoMunicipal = LibConvert.ToInt(txtPrimerNumeroComprobanteRetImpuestoMunicipal.Value);
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            chkExigirInformacionLibroDeCompras.IsChecked = _CurrentInstance.ExigirInformacionLibroDeComprasAsBool;
            chkUsarCodigoProveedorEnPantalla.IsChecked = _CurrentInstance.UsarCodigoProveedorEnPantallaAsBool;
            txtLongitudCodigoProveedor.Value = _CurrentInstance.LongitudCodigoProveedor;
            txtNumCopiasComprobantepago.Value = _CurrentInstance.NumCopiasComprobantepago;
            txtNombrePlantillaComprobanteDePago.Text = _CurrentInstance.NombrePlantillaComprobanteDePago;
            cmbTipoDeOrdenDePagoAImprimir.SelectItem(_CurrentInstance.TipoDeOrdenDePagoAImprimirAsEnum);
            chkConfirmarImpresionPorSecciones.IsChecked = _CurrentInstance.ConfirmarImpresionPorSeccionesAsBool;
            chkNoImprimirComprobanteDePago.IsChecked = _CurrentInstance.NoImprimirComprobanteDePagoAsBool;
            chkImprimirComprobanteContableDePago.IsChecked = _CurrentInstance.ImprimirComprobanteContableDePagoAsBool;
            txtConceptoBancarioReversoDePago.Text = _CurrentInstance.ConceptoBancarioReversoDePago;
            chkAvisarSiProveedorTieneAnticipos.IsChecked = _CurrentInstance.AvisarSiProveedorTieneAnticiposAsBool;
            chkOrdenarCxPPorFacturaDocumento.IsChecked = _CurrentInstance.OrdenarCxPPorFacturaDocumentoAsBool;
            txtNombrePlantillaRetencionImpuestoMunicipal.Text = _CurrentInstance.NombrePlantillaRetencionImpuestoMunicipal;
            chkRetieneImpuestoMunicipal.IsChecked = _CurrentInstance.RetieneImpuestoMunicipalAsBool;
            txtPrimerNumeroComprobanteRetImpuestoMunicipal.Value = _CurrentInstance.PrimerNumeroComprobanteRetImpuestoMunicipal;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtLongitudCodigoProveedor.Validating += new System.ComponentModel.CancelEventHandler(txtLongitudCodigoProveedor_Validating);
            this.txtNumCopiasComprobantepago.Validating += new System.ComponentModel.CancelEventHandler(txtNumCopiasComprobantepago_Validating);
            this.cmbTipoDeOrdenDePagoAImprimir.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoDeOrdenDePagoAImprimir_Validating);
            this.txtConceptoBancarioReversoDePago.Validating += new System.ComponentModel.CancelEventHandler(txtConceptoBancarioReversoDePago_Validating);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
            this.txtNombrePlantillaComprobanteDePago.Validating += new CancelEventHandler(txtNombrePlantillaComprobanteDePago_Validating);
            this.btnBuscarRpxComprobanteDePago.Click +=new RoutedEventHandler(btnBuscarRpxComprobanteDePago_Click);
            this.txtNombrePlantillaRetencionImpuestoMunicipal.Validating += new CancelEventHandler(txtNombrePlantillaRetencionImpuestoMunicipal_Validating);
            btnBuscarRpxRetencionImpuestoMunicipal.Click +=new RoutedEventHandler(btnBuscarRpxRetencionImpuestoMunicipal_Click);
        }
        void btnBuscarRpxRetencionImpuestoMunicipal_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxPlantillaRetencionImpuestoMunicipal();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void BuscarRpxPlantillaRetencionImpuestoMunicipal() {
            string vNombreMunicipio = clsGlobalValues.AppMemoryInfo.GlobalValuesGetString("Parametros", "NombreMunicipio");
            string paramBusqueda = "rpx de Retención de Impuesto Municipal (*.rpx)|*Comprobante*Retencion*" + vNombreMunicipio + "*.rpx";
            string vReturnBusqueda  = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
            if (LibText.Trim(vReturnBusqueda) != "") {
                txtNombrePlantillaRetencionImpuestoMunicipal.Text = vReturnBusqueda;
            }
        }


        void txtNombrePlantillaRetencionImpuestoMunicipal_Validating(object sender, CancelEventArgs e) {
            clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(null, null);
            if (insSettValueByCompanyIpl.SePuedeRetenerParaEsteMunicipio()) {
                if (insSettValueByCompanyIpl.PuedeActivarModulo()) {

                    if (LibString.Len(txtNombrePlantillaRetencionImpuestoMunicipal.Text) == 0) {
                        txtNombrePlantillaRetencionImpuestoMunicipal.Text = "*";
                    }
                    if (LibString.S1IsInS2("*", txtNombrePlantillaRetencionImpuestoMunicipal.Text)) {
                        BuscarRpxPlantillaRetencionImpuestoMunicipal();
                    } else {
                        if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaRetencionImpuestoMunicipal.Text)) {
                            MessageBox.Show("El RPX " + txtNombrePlantillaRetencionImpuestoMunicipal.Text + ", en " + this.Title + ", no EXISTE.");
                            e.Cancel = true;
                        }
                    }
                }
            }
        }

        void txtNombrePlantillaComprobanteDePago_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaComprobanteDePago.Text) == 0) {
                txtNombrePlantillaComprobanteDePago.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaComprobanteDePago.Text)) {
                BuscarRpxPlantillaComprobanteDePago();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaComprobanteDePago.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaComprobanteDePago.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }

        void btnBuscarRpxComprobanteDePago_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxPlantillaComprobanteDePago();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void BuscarRpxPlantillaComprobanteDePago() {
            string paramBusqueda = "rpx de Comprobante Pago (*.rpx)|*Comprobante*Pago*.rpx";
            txtNombrePlantillaComprobanteDePago.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
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


        void txtLongitudCodigoProveedor_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(null, null);
                if (!insSettValueByCompanyIpl.IsValidLongitudCodigoProveedor(Action, LibConvert.ToInt(txtLongitudCodigoProveedor.Value), true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettValueByCompanyIpl.Information.ToString(), Title);
                    txtLongitudCodigoProveedor.Value = insSettValueByCompanyIpl.DefaultLongitudCodigoProveedor();
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

        void txtNumCopiasComprobantepago_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(null, null);
                if (!insSettValueByCompanyIpl.IsValidNumCopiasComprobantepago(Action, LibConvert.ToInt(txtNumCopiasComprobantepago.Value), true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettValueByCompanyIpl.Information.ToString(), Title);
                    txtNumCopiasComprobantepago.Value = insSettValueByCompanyIpl.DefaultNumCopiasComprobantepago();
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

        void cmbTipoDeOrdenDePagoAImprimir_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoDeOrdenDePagoAImprimir.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtConceptoBancarioReversoDePago_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtConceptoBancarioReversoDePago.Text)==0) {
                    txtConceptoBancarioReversoDePago.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Adm.Gv_ConceptoBancario_B1.codigo=" + txtConceptoBancarioReversoDePago.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "Adm.Gv_ConceptoBancario_B1.TipoStr=" + eIngresoEgreso.Ingreso;
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (clsSettValueByCompanyList.ChooseConceptoBancario(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtConceptoBancarioReversoDePago.Text = insParse.GetString(0, "Codigo", "");
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


    } //End of class GSCxPProveedorPagosStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

