using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class CobroDeFacturaRapidaDepositoTransfViewModel : LibInputMasterViewModelMfc<CobroDeFacturaRapidaDepositoTransf> {
        #region Constantes
        public const string TotalACobrarPropertyName = "TotalACobrar";
        public const string TotalCobradoPropertyName = "TotalCobrado";
        public const string MontoDiferenciaPropertyName = "MontoDiferencia";
        public const string SeccionCobroDeFacturaRapidaDepositoTransfDetallePropertyName = "SeccionCobroDeFacturaRapidaDepositoTransfDetalle";
        public const string DiferenciaPropertyName = "Diferencia";
        public const string ColorDiferenciaPropertyName = "ColorDiferencia";
        public const bool EsBotonGrabarPropertyName = false;
        public const string lblMontoDiferenciaPropertyName = "lblMontoDiferencia";
        public const string lblMontoTotalCobradoPropertyName = "lblMontoTotalCobrado";
        public const string lblMontoTotalACobrarPropertyName = "lblMontoTotalACobrar";

        int _ConsecutivoCompania;
        string _NumeroFactura;
        string _CodigoFormaDelCobro;
        string _MontoEfectivo;
        decimal _TotalACobrar;
        decimal _TotalCobrado;
        decimal _MontoDiferencia;
        bool _EsBotonGrabar;
        bool EsCheque;
        string NombreModule;


        #endregion
        #region Propiedades

        public override string ModuleName {
            get {
             if (EsCheque) {
                 return "Cobro con Cheque";
                }
             return "Cobro con Deposito/Transferencia";
            }
        }

        public int  ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if (Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                }
            }
        }

        public string  NumeroFactura {
            get {
                return Model.NumeroFactura;
            }
            set {
                if (Model.NumeroFactura != value) {
                    Model.NumeroFactura = value;
                }
            }
        }
  
        public decimal  TotalACobrar {
            get {
                return _TotalACobrar;
            }
            set {
                if (TotalACobrar != value) {
                    _TotalACobrar = value;
                    RaisePropertyChanged(TotalACobrarPropertyName);
                    RaisePropertyChanged(lblMontoTotalACobrarPropertyName);
                }
            }
        }

        public string lblMontoTotalACobrar {
            get {
                return LibConvert.NumToString(TotalACobrar, 2);
            }
            set {

            }
        }

        public decimal  TotalCobrado {
            get {
                return _TotalCobrado;
            }
            set {
                if (_TotalCobrado != value) {
                    _TotalCobrado = value;
                    //IsDirty = true;
                    RaisePropertyChanged(TotalCobradoPropertyName);
                    RaisePropertyChanged(lblMontoTotalCobradoPropertyName);
                }
            }
        }

        [LibCustomValidation("MontoDiferenciaValidating")]
        public decimal  MontoDiferencia {
            get {
                return _MontoDiferencia;
            }
            set {
                if (_MontoDiferencia != value) {
                    _MontoDiferencia = value;
                    Diferencia = null;
                    ColorDiferencia = null;
                    RaisePropertyChanged(MontoDiferenciaPropertyName);
                    RaisePropertyChanged(DiferenciaPropertyName);
                    RaisePropertyChanged(lblMontoDiferenciaPropertyName);
                    RaisePropertyChanged(ColorDiferenciaPropertyName);
                }
            }
        }

        public string lblMontoDiferencia {
            get {
                return LibConvert.NumToString(MontoDiferencia, 2);
            }
            set {

            }
        }

        public string lblMontoTotalCobrado {
            get {
                return LibConvert.NumToString(TotalCobrado, 2);
            }
            set {

            }
        }

        private ValidationResult MontoDiferenciaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibMath.RoundToNDecimals (MontoDiferencia,2) >=  0) {
                return ValidationResult.Success;
            } else {
                vResult = new ValidationResult("El total cobrado del " + NombreModule + " supera el monto total de la Factura");
            }
            return vResult;
        }

        decimal _TotalCobradoInicial;
        public decimal TotalCobradoInicial {
            get {
                return _TotalCobradoInicial;
            }
            set {
                if (_TotalCobradoInicial != value) {
                    _TotalCobradoInicial = value;
                    RaisePropertyChanged(TotalCobradoPropertyName);
                    RaisePropertyChanged(lblMontoTotalCobradoPropertyName);
                }
            }
        }

        string _Diferencia;
        public string Diferencia {
            get {
                return _Diferencia;
            }
            set {
                if (MontoDiferencia > 0) {
                    _Diferencia = "Falta";
                } else {
                    _Diferencia = "Cambio";
                }
                RaisePropertyChanged(ColorDiferenciaPropertyName);

            }
        }

        string _ColorDiferencia;
        public string ColorDiferencia {
            get {
                  return _ColorDiferencia;
            }
            set {
                if (MontoDiferencia > 0) {
                    _ColorDiferencia = "Red";
                } else {
                    _ColorDiferencia = "Green";
                }
                RaisePropertyChanged(DiferenciaPropertyName);
            }
        }

        public bool EsBotonGrabar {
            get {
                return _EsBotonGrabar;
            }
            set {
                if (EsBotonGrabar != value) {
                    _EsBotonGrabar = value;
                 }
            }
        }

        
        //[LibDetailRequired(ErrorMessage =  " Detalle es requerido.",AllowEmptyStrings = false)]
        public CobroDeFacturaRapidaDepositoTransfDetalleMngViewModel DetailCobroDeFacturaRapidaDepositoTransfDetalle {
            get;
            set;
        }

        public RelayCommand<string> CreateCobroDeFacturaRapidaDepositoTransfDetalleCommand {
            get { return DetailCobroDeFacturaRapidaDepositoTransfDetalle.CreateCommand; }
        }

        public RelayCommand<string> UpdateCobroDeFacturaRapidaDepositoTransfDetalleCommand {
            get { return DetailCobroDeFacturaRapidaDepositoTransfDetalle.UpdateCommand; }
        }

        public RelayCommand<string> DeleteCobroDeFacturaRapidaDepositoTransfDetalleCommand {
            get { return DetailCobroDeFacturaRapidaDepositoTransfDetalle.DeleteCommand; }
        }
        #endregion //Propiedades
        #region Constructores
        public CobroDeFacturaRapidaDepositoTransfViewModel()
            : this(new CobroDeFacturaRapidaDepositoTransf(), eAccionSR.Insertar,true ) {
        }

        public CobroDeFacturaRapidaDepositoTransfViewModel(bool initEscheque)
            : this(new CobroDeFacturaRapidaDepositoTransf(), eAccionSR.Insertar, initEscheque) {
        }
        public CobroDeFacturaRapidaDepositoTransfViewModel(CobroDeFacturaRapidaDepositoTransf initModel, eAccionSR initAction, bool initEscheque)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = TotalACobrarPropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            EsCheque = initEscheque;
            InitializeDetails();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(CobroDeFacturaRapidaDepositoTransf valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        internal void InitLookAndFeel(string valNumeroFactura, decimal valTotalACobrar, decimal valTotalCobrado, decimal valTotalCobradoOtrosTipos,bool valEsCheque) {
            NumeroFactura = valNumeroFactura;
            TotalACobrar = valTotalACobrar;
            TotalCobrado = valTotalCobrado + valTotalCobradoOtrosTipos;
            TotalCobradoInicial = valTotalCobradoOtrosTipos;
            MontoDiferencia = LibMath.RoundToNDecimals(TotalACobrar - TotalCobrado,2);
            Diferencia = "Cambio";
            ColorDiferencia = "Green";
            EsCheque = valEsCheque;
            NombreModule = "Deposito/Transferencia";
            if (EsCheque) {
                NombreModule = "Cheque";
            }
        }

        protected override CobroDeFacturaRapidaDepositoTransf FindCurrentRecord(CobroDeFacturaRapidaDepositoTransf valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", valModel.NumeroFactura, 11);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "CobroDeFacturaRapidaDepositoTransfGET", vParams.Get(), UseDetail).FirstOrDefault();
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<CobroDeFacturaRapidaDepositoTransf>, IList<CobroDeFacturaRapidaDepositoTransf>> GetBusinessComponent() {
            return new clsCobroDeFacturaRapidaDepositoTransfNav();
        }

        protected override void InitializeDetails() {
            DetailCobroDeFacturaRapidaDepositoTransfDetalle = new CobroDeFacturaRapidaDepositoTransfDetalleMngViewModel(this, Model.DetailCobroDeFacturaRapidaDepositoTransfDetalle, Action);
            DetailCobroDeFacturaRapidaDepositoTransfDetalle.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<CobroDeFacturaRapidaDepositoTransfDetalleViewModel>>(DetailCobroDeFacturaRapidaDepositoTransfDetalle_OnCreated);
            DetailCobroDeFacturaRapidaDepositoTransfDetalle.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<CobroDeFacturaRapidaDepositoTransfDetalleViewModel>>(DetailCobroDeFacturaRapidaDepositoTransfDetalle_OnUpdated);
            DetailCobroDeFacturaRapidaDepositoTransfDetalle.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<CobroDeFacturaRapidaDepositoTransfDetalleViewModel>>(DetailCobroDeFacturaRapidaDepositoTransfDetalle_OnDeleted);
            DetailCobroDeFacturaRapidaDepositoTransfDetalle.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<CobroDeFacturaRapidaDepositoTransfDetalleViewModel>>(DetailCobroDeFacturaRapidaDepositoTransfDetalle_OnSelectedItemChanged);
        }

        #region CobroDeFacturaRapidaDepositoTransfDetalle

        private void DetailCobroDeFacturaRapidaDepositoTransfDetalle_OnSelectedItemChanged(object sender, SearchCollectionChangedEventArgs<CobroDeFacturaRapidaDepositoTransfDetalleViewModel> e) {
            try {
                UpdateCobroDeFacturaRapidaDepositoTransfDetalleCommand.RaiseCanExecuteChanged();
                DeleteCobroDeFacturaRapidaDepositoTransfDetalleCommand.RaiseCanExecuteChanged();
                ActualizaSaldos();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailCobroDeFacturaRapidaDepositoTransfDetalle_OnDeleted(object sender, SearchCollectionChangedEventArgs<CobroDeFacturaRapidaDepositoTransfDetalleViewModel> e) {
            try {
                IsDirty = true;
                Model.DetailCobroDeFacturaRapidaDepositoTransfDetalle.Remove(e.ViewModel.GetModel());
                ActualizaSaldos();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailCobroDeFacturaRapidaDepositoTransfDetalle_OnUpdated(object sender, SearchCollectionChangedEventArgs<CobroDeFacturaRapidaDepositoTransfDetalleViewModel> e) {
            try {
                //IsDirty = e.ViewModel.IsDirty;
                // actualizar datos 
                // samuel
                ActualizaSaldos();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailCobroDeFacturaRapidaDepositoTransfDetalle_OnCreated(object sender, SearchCollectionChangedEventArgs<CobroDeFacturaRapidaDepositoTransfDetalleViewModel> e) {
            try {
                var vViewModel = e.ViewModel;
                if (vViewModel != null ) {
                    vViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(vViewModel_PropertyChanged);
                }
                Model.DetailCobroDeFacturaRapidaDepositoTransfDetalle.Add(e.ViewModel.GetModel());
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        void vViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            try {
                if (LibString.S1IsEqualToS2(e.PropertyName, CobroDeFacturaRapidaDepositoTransfDetalleViewModel.MontoPropertyName)) {
                    ActualizaSaldos();
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private  void ActualizaSaldos() {
            TotalCobrado = TotalCobradoInicial + (LibMath.RoundToNDecimals(DetailCobroDeFacturaRapidaDepositoTransfDetalle.Items.Sum(o => o.Monto), 2));
            MontoDiferencia = LibMath.RoundToNDecimals(TotalACobrar - TotalCobrado,2); 
        }


        #endregion //CobroDeFacturaRapidaDepositoTransfDetalle
        #endregion //Metodos Generados


        protected override void InitializeCommands() {
            base.InitializeCommands();
            InsertCommand = new RelayCommand(ExecuteInsertCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            RibbonData.ApplicationMenuData = new LibRibbonMenuButtonData() {
                IsVisible = false
            };
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection.Insert(0, CreateActionRibbonButton());
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection.RemoveAt(1);
            }
        }

        private LibRibbonButtonData CreateActionRibbonButton() {
            LibRibbonButtonData vResult = new LibRibbonButtonData() {
                Label = "Grabar",
                Command = InsertCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F6.png", UriKind.Relative),
                ToolTipDescription = "Guarda los cambios en " + ModuleName + ".",
                ToolTipTitle = "Ejecutar Acción (F6)",
                IsVisible = true,
                KeyTip = "F6"
            };
            return vResult;
        }

        private void ExecuteInsertCommand() {
            try {
                if (!IsValid) {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(new GalacValidationException(Error), ModuleName, ModuleName);
                    return;
                }
                EsBotonGrabar = true;
                RaiseRequestCloseEvent();
            } catch (System.AccessViolationException) {
                throw;
            } catch (GalacException vEx) {
                if (vEx.ExceptionManagementType == eExceptionManagementType.Validation) {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Warning(null, vEx.Message, "Validación de Consistencia");
                } else {
                    LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
                }
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        public RelayCommand InsertCommand {
            get;
            private set;
        }

        //protected override bool CreateRecord() {
        //    return true;
        //}

        //protected override bool UpdateRecord() {
        //    return true;
        //}

        public override void OnClosed() {
            //base.OnClosed();
        }

        public override bool OnClosing() {
            return false;
        }

    } //End of class CobroDeFacturaRapidaTarjetaViewModel
} //End of namespace Galac.Adm.Uil.Venta

