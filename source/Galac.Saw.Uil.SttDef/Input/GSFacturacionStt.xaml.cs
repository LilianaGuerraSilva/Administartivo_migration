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
    /// Lógica de interacción para GSFacturacionStt.xaml
    /// </summary>
    internal partial class GSFacturacionStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        FacturacionStt _CurrentInstance;

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
        internal FacturacionStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }

        #endregion //Propiedades
        #region Constructores

        public GSFacturacionStt() {
            InitializeComponent();
            InitializeEvents();
            cmbDevolucionReversoSeGeneraComo.Fill(from eTipoDocumentoFactura e in LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDocumentoFactura)) where (e.Equals(eTipoDocumentoFactura.Factura) || e.Equals(eTipoDocumentoFactura.NotaDeCredito)) select e );
            cmbTipoDeNivelDePrecios.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeNivelDePrecios)));
            cmbItemsMonto.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eItemsMontoFactura)));
            cmbComisionesEnFactura.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eComisionesEnFactura)));
            cmbComisionesEnRenglones.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eComisionesEnRenglones)));
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
            _CurrentInstance = (FacturacionStt)initInstance;

            Title = "Facturación";
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
            InabilitaControles();
            Tool.AllDisabled(gwMain.Children, Action);

            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
                bool vSesionEspecialPrecioSinIva = clsGlobalValues.AppMemoryInfo.GlobalValuesGetBool("Parametros", "SesionEspecialPrecioSinIva");
                LibApiAwp.EnableControl(chkUsaPrecioSinIva, vSesionEspecialPrecioSinIva || ObieneInsertandoPorPrimeraVez());
                LibApiAwp.EnableControl(chkUsaPrecioSinIvaEnResumenVtas, vSesionEspecialPrecioSinIva || ObieneInsertandoPorPrimeraVez());

                if (!LibConvert.ToBool(clsGlobalValues.AppMemoryInfo.GlobalValuesGetBool("Parametros", "HayArticuloSerial"))) {
                    chkBuscarArticuloXSerialAlFacturar.Visibility = System.Windows.Visibility.Hidden;
                    label13.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        private bool ObieneInsertandoPorPrimeraVez() {
            var modulos = ((clsSettValueByCompanyIpl)((GSSettValueByCompany)((Grid)((HeaderedContentControl)((StackPanel)((Grid)((ContentPresenter)this.TemplatedParent).Parent).Parent).TemplatedParent).Parent).Parent).CurrentModel).ModuleList;
            var modfact = modulos[8].Groups[0].Content;
            object Result = (modfact.GetType().GetProperty("InsertandoPorPrimeraVezAsBool").GetValue(modfact, null));
            if (Result == null) { Result = false; }
            return (bool)Result;
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.VerificarFacturasManualesFaltantesAsBool = chkVerificarFacturasManualesFaltantes.IsChecked.Value;
            _CurrentInstance.NumFacturasManualesFaltantes = LibConvert.ToInt(txtNumFacturasManualesFaltantes.Value);
            _CurrentInstance.PermitirFacturarConCantidadCeroAsBool = chkPermitirFacturarConCantidadCero.IsChecked.Value;
            _CurrentInstance.DevolucionReversoSeGeneraComoAsEnum = (eTipoDocumentoFactura)cmbDevolucionReversoSeGeneraComo.SelectedItemToInt();
            _CurrentInstance.ExigirRifdeClienteAlEmitirFacturaAsBool = chkExigirRifdeClienteAlEmitirFactura.IsChecked.Value;
            _CurrentInstance.SugerirNumeroControlFacturaAsBool = chkSugerirNumeroControlFactura.IsChecked.Value;
            _CurrentInstance.PedirInformacionLibroVentasXlsalEmitirFacturaAsBool = chkPedirInformacionLibroVentasXlsalEmitirFactura.IsChecked.Value;
            _CurrentInstance.TipoDeNivelDePreciosAsEnum = (eTipoDeNivelDePrecios)cmbTipoDeNivelDePrecios.SelectedItemToInt();
            _CurrentInstance.ComplConComodinEnBusqDeArtInvAsBool = chkComplConComodinEnBusqDeArtInv.IsChecked.Value;
            _CurrentInstance.UsarResumenDiarioDeVentasAsBool = chkUsarResumenDiarioDeVentas.IsChecked.Value;
            _CurrentInstance.ItemsMontoAsEnum = (eItemsMontoFactura)cmbItemsMonto.SelectedItemToInt();
            _CurrentInstance.ComisionesEnFacturaAsEnum = (eComisionesEnFactura)cmbComisionesEnFactura.SelectedItemToInt();
            _CurrentInstance.ComisionesEnRenglonesAsEnum = (eComisionesEnRenglones)cmbComisionesEnRenglones.SelectedItemToInt();
            _CurrentInstance.CambiarFechaEnCuotasLuegoDeFijarFechaEntregaAsBool = chkCambiarFechaEnCuotasLuegoDeFijarFechaEntrega.IsChecked.Value;
            _CurrentInstance.BuscarArticuloXSerialAlFacturarAsBool = chkBuscarArticuloXSerialAlFacturar.IsChecked.Value;
            _CurrentInstance.NombreVendedorUno = txtNombreVendedorUno.Text;
            _CurrentInstance.NombreVendedorDos = txtNombreVendedorDos.Text;
            _CurrentInstance.NombreVendedorTres = txtNombreVendedorTres.Text;
            _CurrentInstance.UsaPrecioSinIvaAsBool = chkUsaPrecioSinIva.IsChecked.Value;
            _CurrentInstance.UsaPrecioSinIvaEnResumenVtasAsBool = chkUsaPrecioSinIvaEnResumenVtas.IsChecked.Value;
            _CurrentInstance.ResumenVtasAfectaInventarioAsBool = chkResumenVtasAfectaInventario.IsChecked.Value;
            _CurrentInstance.UsarRenglonesEnResumenVtasAsBool = chkUsarRenglonesEnResumenVtas.IsChecked.Value;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            chkVerificarFacturasManualesFaltantes.IsChecked = _CurrentInstance.VerificarFacturasManualesFaltantesAsBool;
            txtNumFacturasManualesFaltantes.Value = _CurrentInstance.NumFacturasManualesFaltantes;
            chkPermitirFacturarConCantidadCero.IsChecked = _CurrentInstance.PermitirFacturarConCantidadCeroAsBool;
            cmbDevolucionReversoSeGeneraComo.SelectItem(_CurrentInstance.DevolucionReversoSeGeneraComoAsEnum);
            chkExigirRifdeClienteAlEmitirFactura.IsChecked = _CurrentInstance.ExigirRifdeClienteAlEmitirFacturaAsBool;
            chkSugerirNumeroControlFactura.IsChecked = _CurrentInstance.SugerirNumeroControlFacturaAsBool;
            chkPedirInformacionLibroVentasXlsalEmitirFactura.IsChecked = _CurrentInstance.PedirInformacionLibroVentasXlsalEmitirFacturaAsBool;
            cmbTipoDeNivelDePrecios.SelectItem(_CurrentInstance.TipoDeNivelDePreciosAsEnum);
            chkComplConComodinEnBusqDeArtInv.IsChecked = _CurrentInstance.ComplConComodinEnBusqDeArtInvAsBool;
            chkUsarResumenDiarioDeVentas.IsChecked = _CurrentInstance.UsarResumenDiarioDeVentasAsBool;
            cmbItemsMonto.SelectItem(_CurrentInstance.ItemsMontoAsEnum);
            cmbComisionesEnFactura.SelectItem(_CurrentInstance.ComisionesEnFacturaAsEnum);
            cmbComisionesEnRenglones.SelectItem(_CurrentInstance.ComisionesEnRenglonesAsEnum);
            chkCambiarFechaEnCuotasLuegoDeFijarFechaEntrega.IsChecked = _CurrentInstance.CambiarFechaEnCuotasLuegoDeFijarFechaEntregaAsBool;
            chkBuscarArticuloXSerialAlFacturar.IsChecked = _CurrentInstance.BuscarArticuloXSerialAlFacturarAsBool;
            txtNombreVendedorUno.Text = _CurrentInstance.NombreVendedorUno;
            txtNombreVendedorDos.Text = _CurrentInstance.NombreVendedorDos;
            txtNombreVendedorTres.Text = _CurrentInstance.NombreVendedorTres;
            chkUsaPrecioSinIva.IsChecked = _CurrentInstance.UsaPrecioSinIvaAsBool;
            chkUsaPrecioSinIvaEnResumenVtas.IsChecked = _CurrentInstance.UsaPrecioSinIvaEnResumenVtasAsBool;
            chkResumenVtasAfectaInventario.IsChecked = _CurrentInstance.ResumenVtasAfectaInventarioAsBool;
            chkUsarRenglonesEnResumenVtas.IsChecked = _CurrentInstance.UsarRenglonesEnResumenVtasAsBool;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.cmbDevolucionReversoSeGeneraComo.Validating += new System.ComponentModel.CancelEventHandler(cmbDevolucionReversoSeGeneraComo_Validating);
            this.cmbTipoDeNivelDePrecios.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoDeNivelDePrecios_Validating);
            this.cmbItemsMonto.Validating += new System.ComponentModel.CancelEventHandler(cmbItemsMonto_Validating);
            this.cmbComisionesEnFactura.Validating += new System.ComponentModel.CancelEventHandler(cmbComisionesEnFactura_Validating);
            this.cmbComisionesEnRenglones.Validating += new System.ComponentModel.CancelEventHandler(cmbComisionesEnRenglones_Validating);
            this.chkExigirRifdeClienteAlEmitirFactura.Click += new RoutedEventHandler(chkExigirRifdeClienteAlEmitirFactura_Click);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
        }

        void chkExigirRifdeClienteAlEmitirFactura_Click(object sender, RoutedEventArgs e) {
            if (!(bool)chkExigirRifdeClienteAlEmitirFactura.IsChecked) {
                MessageBox.Show("De no imprimir el Número de RIF del Cliente en la Factura \n" + "incurrirá en un incumplimiento de Deberes Formales, \n" + "que acarrea sanciones por parte de la Administración Tributaria.");
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


        void cmbDevolucionReversoSeGeneraComo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbDevolucionReversoSeGeneraComo.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbTipoDeNivelDePrecios_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoDeNivelDePrecios.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbItemsMonto_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbItemsMonto.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbComisionesEnFactura_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if ((eItemsMontoFactura)cmbItemsMonto.SelectedItem == eItemsMontoFactura.NO_PERMITIR_ITEMS_NEGATIVOS) {
                    cmbItemsMonto.IsEnabled = false;
                } else {
                    cmbItemsMonto.IsEnabled = true;
                }
                if (((eComisionesEnFactura)cmbComisionesEnFactura.SelectedItem == eComisionesEnFactura.SobreRenglones) || ((eComisionesEnFactura)cmbComisionesEnFactura.SelectedItem == eComisionesEnFactura.SobreTotalFacturayRenglones)) {
                    cmbComisionesEnRenglones.IsEnabled = true;
                } else {
                    cmbComisionesEnRenglones.IsEnabled = false;
                }
                MostrarLosTextoDeNombredeVendedor();
                cmbComisionesEnFactura.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbComisionesEnRenglones_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbComisionesEnRenglones.ValidateTextInCombo();
                InabilitaControles();
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

        private void InabilitaControles() {
            MostrarLosTextoDeNombredeVendedor();
            //LibApiAwp.EnableControl(cmbComisionesEnFactura, ((eComisionesEnFactura)cmbComisionesEnFactura.SelectedItemToInt() == eComisionesEnFactura.SobreTotalFactura));
        }
        private void MostrarLosTextoDeNombredeVendedor() {
            txtNombreVendedorUno.Visibility = System.Windows.Visibility.Hidden;
            txtNombreVendedorDos.Visibility = System.Windows.Visibility.Hidden;
            txtNombreVendedorTres.Visibility = System.Windows.Visibility.Hidden;
            lblNombreVendedorUno.Visibility = System.Windows.Visibility.Hidden;
            lblNombreVendedorDos.Visibility = System.Windows.Visibility.Hidden;
            lblNombreVendedorTres.Visibility = System.Windows.Visibility.Hidden;
            if ((eComisionesEnRenglones)cmbComisionesEnRenglones.SelectedItemToInt() == eComisionesEnRenglones.PorUnVendedor && (eComisionesEnFactura)cmbComisionesEnFactura.SelectedItem !=  eComisionesEnFactura.SobreTotalFactura) {
                txtNombreVendedorUno.Visibility = System.Windows.Visibility.Visible;
                lblNombreVendedorUno.Visibility = System.Windows.Visibility.Visible;
            } else if ((eComisionesEnRenglones)cmbComisionesEnRenglones.SelectedItemToInt() == eComisionesEnRenglones.PordosVendedores && (eComisionesEnFactura)cmbComisionesEnFactura.SelectedItem != eComisionesEnFactura.SobreTotalFactura) {
                txtNombreVendedorUno.Visibility = System.Windows.Visibility.Visible;
                txtNombreVendedorDos.Visibility = System.Windows.Visibility.Visible;
                lblNombreVendedorUno.Visibility = System.Windows.Visibility.Visible;
                lblNombreVendedorDos.Visibility = System.Windows.Visibility.Visible;
            } else if ((eComisionesEnRenglones)cmbComisionesEnRenglones.SelectedItemToInt() == eComisionesEnRenglones.PorTresVendedores && (eComisionesEnFactura)cmbComisionesEnFactura.SelectedItem != eComisionesEnFactura.SobreTotalFactura) {
                txtNombreVendedorUno.Visibility = System.Windows.Visibility.Visible;
                txtNombreVendedorDos.Visibility = System.Windows.Visibility.Visible;
                txtNombreVendedorTres.Visibility = System.Windows.Visibility.Visible;
                lblNombreVendedorUno.Visibility = System.Windows.Visibility.Visible;
                lblNombreVendedorDos.Visibility = System.Windows.Visibility.Visible;
                lblNombreVendedorTres.Visibility = System.Windows.Visibility.Visible;
            }
        }

    } //End of class GSFacturacionStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

