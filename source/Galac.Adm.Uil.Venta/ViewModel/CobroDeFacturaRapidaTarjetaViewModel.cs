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
using Galac.Comun.Ccl.SttDef;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class CobroDeFacturaRapidaTarjetaViewModel : LibInputMasterViewModelMfc<CobroDeFacturaRapidaTarjeta> {
        #region Constantes
        public const string TotalACobrarPropertyName = "TotalACobrar";
        public const string TotalCobradoPropertyName = "TotalCobrado";
        public const string MontoDiferenciaPropertyName = "MontoDiferencia";
        public const string SeccionCobroDeFacturaRapidaTarjetaDetallePropertyName = "SeccionCobroDeFacturaRapidaTarjetaDetalle";
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
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Cobro Tarjeta de Factura"; }
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
                   // IsDirty = true;
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

        [LibCustomValidation("MontoDieferenciaValidating")]
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

        private ValidationResult MontoDieferenciaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (MontoDiferencia >=  0) {
                return ValidationResult.Success;
            } else {
                vResult = new ValidationResult("El total cobrado de la tarjeta no puede ser menor 0 (cero).");
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
                    //IsDirty = true;
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

        //[LibDetailRequired(ErrorMessage = "Punto de Venta - Se requiere el Detalle del Cobro")]
        public CobroDeFacturaRapidaTarjetaDetalleMngViewModel DetailCobroDeFacturaRapidaTarjetaDetalle {
            get;
            set;
        }

        public RelayCommand<string> CreateCobroDeFacturaRapidaTarjetaDetalleCommand {
            get { return DetailCobroDeFacturaRapidaTarjetaDetalle.CreateCommand; }
        }

        public RelayCommand<string> UpdateCobroDeFacturaRapidaTarjetaDetalleCommand {
            get { return DetailCobroDeFacturaRapidaTarjetaDetalle.UpdateCommand; }
        }

        public RelayCommand<string> DeleteCobroDeFacturaRapidaTarjetaDetalleCommand {
            get { return DetailCobroDeFacturaRapidaTarjetaDetalle.DeleteCommand; }
        }
        #endregion //Propiedades
        #region Constructores
        public CobroDeFacturaRapidaTarjetaViewModel()
            : this(new CobroDeFacturaRapidaTarjeta(), eAccionSR.Insertar) {
        }
        public CobroDeFacturaRapidaTarjetaViewModel(CobroDeFacturaRapidaTarjeta initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = TotalACobrarPropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            InitializeDetails();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(CobroDeFacturaRapidaTarjeta valModel) {
            base.InitializeLookAndFeel(valModel);
   
        }

        internal void InitLookAndFeel(string valNumeroFactura, decimal valTotalACobrar, decimal valTotalCobrado, decimal valTotalCobradoOtrosTipos) {
            NumeroFactura = valNumeroFactura;
            TotalACobrar = valTotalACobrar;
            //TotalCobrado = valTotalCobrado;
            //TotalCobradoInicial = valTotalCobrado;
            TotalCobrado = valTotalCobrado + valTotalCobradoOtrosTipos;
            TotalCobradoInicial = valTotalCobradoOtrosTipos;
            MontoDiferencia = LibMath.RoundToNDecimals(TotalACobrar - TotalCobrado,2);
            Diferencia = "Cambio";
            ColorDiferencia = "Green";
            
            // aqui tambien va el total cobrado 
            //MontoEfectivo = valTotalACobrar;
        }

        protected override CobroDeFacturaRapidaTarjeta FindCurrentRecord(CobroDeFacturaRapidaTarjeta valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", valModel.NumeroFactura, 11);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "CobroDeFacturaRapidaTarjetaGET", vParams.Get(), UseDetail).FirstOrDefault();
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<CobroDeFacturaRapidaTarjeta>, IList<CobroDeFacturaRapidaTarjeta>> GetBusinessComponent() {
            return new clsCobroDeFacturaRapidaTarjetaNav();
        }

        protected override void InitializeDetails() {
            DetailCobroDeFacturaRapidaTarjetaDetalle = new CobroDeFacturaRapidaTarjetaDetalleMngViewModel(this, Model.DetailCobroDeFacturaRapidaTarjetaDetalle, Action);
            DetailCobroDeFacturaRapidaTarjetaDetalle.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<CobroDeFacturaRapidaTarjetaDetalleViewModel>>(DetailCobroDeFacturaRapidaTarjetaDetalle_OnCreated);
            DetailCobroDeFacturaRapidaTarjetaDetalle.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<CobroDeFacturaRapidaTarjetaDetalleViewModel>>(DetailCobroDeFacturaRapidaTarjetaDetalle_OnUpdated);
            DetailCobroDeFacturaRapidaTarjetaDetalle.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<CobroDeFacturaRapidaTarjetaDetalleViewModel>>(DetailCobroDeFacturaRapidaTarjetaDetalle_OnDeleted);
            DetailCobroDeFacturaRapidaTarjetaDetalle.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<CobroDeFacturaRapidaTarjetaDetalleViewModel>>(DetailCobroDeFacturaRapidaTarjetaDetalle_OnSelectedItemChanged);
            
        }

        #region CobroDeFacturaRapidaTarjetaDetalle

        private void DetailCobroDeFacturaRapidaTarjetaDetalle_OnSelectedItemChanged(object sender, SearchCollectionChangedEventArgs<CobroDeFacturaRapidaTarjetaDetalleViewModel> e) {
            try {
                UpdateCobroDeFacturaRapidaTarjetaDetalleCommand.RaiseCanExecuteChanged();
                DeleteCobroDeFacturaRapidaTarjetaDetalleCommand.RaiseCanExecuteChanged();
                // actualizar datos
                // samuel
                ActualizaSaldos();
                //

            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailCobroDeFacturaRapidaTarjetaDetalle_OnDeleted(object sender, SearchCollectionChangedEventArgs<CobroDeFacturaRapidaTarjetaDetalleViewModel> e) {
            try {
                IsDirty = true;
                Model.DetailCobroDeFacturaRapidaTarjetaDetalle.Remove(e.ViewModel.GetModel());
                // actualizar datos
                // samuel
                ActualizaSaldos();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailCobroDeFacturaRapidaTarjetaDetalle_OnUpdated(object sender, SearchCollectionChangedEventArgs<CobroDeFacturaRapidaTarjetaDetalleViewModel> e) {
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

        private void DetailCobroDeFacturaRapidaTarjetaDetalle_OnCreated(object sender, SearchCollectionChangedEventArgs<CobroDeFacturaRapidaTarjetaDetalleViewModel> e) {
            try {
                var vViewModel = e.ViewModel;
                if (vViewModel != null ) {
                    vViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(vViewModel_PropertyChanged);
                }
                Model.DetailCobroDeFacturaRapidaTarjetaDetalle.Add(e.ViewModel.GetModel());
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        void vViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            try {
                if (LibString.S1IsEqualToS2(e.PropertyName, CobroDeFacturaRapidaTarjetaDetalleViewModel.MontoPropertyName)) {
                    ActualizaSaldos();
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        // actualizar los saldos de los totales.

        private  void ActualizaSaldos() {
             TotalCobrado = TotalCobradoInicial + (LibMath.RoundToNDecimals(DetailCobroDeFacturaRapidaTarjetaDetalle.Items.Sum(o => o.Monto), 2));
             MontoDiferencia = LibMath.RoundToNDecimals(TotalACobrar - TotalCobrado,2); 
        }


        #endregion //CobroDeFacturaRapidaTarjetaDetalle
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
                //IsDirty = false;
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

