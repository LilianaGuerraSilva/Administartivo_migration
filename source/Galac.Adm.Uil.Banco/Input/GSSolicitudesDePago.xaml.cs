using System;
using System.Collections;
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
//using Syncfusion.Windows.Controls.Grid;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.Banco;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Uil.Banco.Input {
    /// <summary>
    /// Lógica de interacción para GSSolicitudesDePago.xaml
    /// </summary>
    internal partial class GSSolicitudesDePago: UserControl {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        SolicitudesDePago _CurrentInstance;
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
        internal SolicitudesDePago CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        internal ILibView CurrentModel {
            get { return _CurrentModel; }
            set { _CurrentModel = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSSolicitudesDePago() {
            InitializeComponent();
            InitializeEvents();
            cmbStatus.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eStatusSolicitud)));
            cmbGeneradoPor.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eSolicitudGeneradaPor)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Numero Documento Origen, Fecha de Solicitud, Status, Generada";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, ILibView initModel, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (SolicitudesDePago)initInstance;
            _CurrentModel = initModel;
            Title = initModel.MessageName;
            Action = initAction;
            ExtendedAction = initExtendedAction;
            LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar });
            if (Action == eAccionSR.Insertar) {
                SetFormValuesFromNavigator(true);
                ((clsSolicitudesDePagoIpl)initModel).InitDetailForInsert();
            } else {
                SetFormValuesFromNavigator(false);
            }
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            bool vAllowGridEdition = !(Action == eAccionSR.Consultar || Action == eAccionSR.Eliminar);
            //grdRenglonSolicitudesDePago.AllowEdit = vAllowGridEdition;
            //grdRenglonSolicitudesDePago.AllowDelete = vAllowGridEdition;
            //grdRenglonSolicitudesDePago.ItemsSource = _CurrentInstance.DetailRenglonSolicitudesDePago;
        }

        internal void SetNavigatorValuesFromForm() {
            _CurrentInstance.NumeroDocumentoOrigen = LibConvert.ToInt(txtNumeroDocumentoOrigen.Text);
            _CurrentInstance.FechaSolicitud = dtpFechaSolicitud.Date;
            _CurrentInstance.StatusAsEnum = (eStatusSolicitud) cmbStatus.SelectedItemToInt();
            _CurrentInstance.GeneradoPorAsEnum = (eSolicitudGeneradaPor) cmbGeneradoPor.SelectedItemToInt();
            _CurrentInstance.Observaciones = txtObservaciones.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            if (valClearRecord) {
                _CurrentModel.Clear(_CurrentInstance);
            }
            ClearControl();
            txtNumeroDocumentoOrigen.Text = LibConvert.ToStr(_CurrentInstance.NumeroDocumentoOrigen);
            dtpFechaSolicitud.Date = _CurrentInstance.FechaSolicitud;
            cmbStatus.SelectItem(_CurrentInstance.StatusAsEnum);
            cmbGeneradoPor.SelectItem(_CurrentInstance.GeneradoPorAsEnum);
            txtObservaciones.Text = _CurrentInstance.Observaciones;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtNumeroDocumentoOrigen.Validating += new System.ComponentModel.CancelEventHandler(txtNumeroDocumentoOrigen_Validating);
            //this.dtpFechaSolicitud.Validating += new System.ComponentModel.CancelEventHandler(dtpFechaSolicitud_Validating);
            this.cmbStatus.Validating += new System.ComponentModel.CancelEventHandler(cmbStatus_Validating);
            this.cmbGeneradoPor.Validating += new System.ComponentModel.CancelEventHandler(cmbGeneradoPor_Validating);
        }

        void txtNumeroDocumentoOrigen_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsSolicitudesDePagoIpl insSolicitudesDePagoIpl = new clsSolicitudesDePagoIpl(((clsSolicitudesDePagoIpl)CurrentModel).AppMemoryInfo, ((clsSolicitudesDePagoIpl)CurrentModel).Mfc);
                if (!insSolicitudesDePagoIpl.IsValidNumeroDocumentoOrigen(Action, LibConvert.ToInt(txtNumeroDocumentoOrigen.Text), true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSolicitudesDePagoIpl.Information.ToString(), _CurrentModel.MessageName);
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

        void dtpFechaSolicitud_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsSolicitudesDePagoIpl insSolicitudesDePagoIpl = new clsSolicitudesDePagoIpl(((clsSolicitudesDePagoIpl)CurrentModel).AppMemoryInfo, ((clsSolicitudesDePagoIpl)CurrentModel).Mfc);
                if (!insSolicitudesDePagoIpl.IsValidFechaSolicitud(Action, dtpFechaSolicitud.Date, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSolicitudesDePagoIpl.Information.ToString(), _CurrentModel.MessageName);
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

        void cmbGeneradoPor_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbGeneradoPor.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtConsecutivoSolicitud_GotFocus(object sender, RoutedEventArgs e) {
            try {
                if (Action == eAccionSR.Insertar) {
                    //txtConsecutivoSolicitud.Text = LibConvert.ToStr(CurrentModel.NextSequential("ConsecutivoSolicitud"));
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

        internal void FormatColumnsGridRenglonSolicitudesDePago() {
        }

        private void RealizaLosCalculos() {
            if (Action != eAccionSR.Consultar && Action != eAccionSR.Eliminar) {
                //throw new NotImplementedException("Debe sobreescribir el metodo RealizaLosCalculos para su caso especifico. Si no lo requiere no lo invoque.");
            }
        }
        #endregion //Metodos Generados


    } //End of class GSSolicitudesDePago.xaml

} //End of namespace Galac.Adm.Uil.Banco

