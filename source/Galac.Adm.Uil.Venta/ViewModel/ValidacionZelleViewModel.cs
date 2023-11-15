using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using System;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class ValidacionZelleViewModel: LibGenericViewModel {
        #region Constantes
        public const string TasaDeCambioPropertyName = "TasaDeCambio";
        public const string MontoRecibidoMLPropertyName = "MontoRecibidoML";
        public const string MontoRecibidoMEPropertyName = "MontoRecibidoME";
        public const string MontoARegistrarPropertyName = "MontoARegistrar";
        public const string IsVisibleAdvertenciaPropertyName = "IsVisibleAdvertencia";        
        #endregion
        #region variables
        decimal _TasaDeCambio;
        decimal _MontoRecibidoML;
        decimal _MontoRecibidoME;
        decimal _MontoARegistrar;
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Validación de Transacción con Zelle"; }
        }

        public string TasaBCVFecha {
            get {
                return "Tasa BCV (" + LibConvert.ToStr(LibDate.Today(), "dd/MM/yyyy") + ")";
            }           
        }

        public decimal TasaDeCambio {
            get {
                return _TasaDeCambio;
            }
            set {
                if (_TasaDeCambio != value) {
                    _TasaDeCambio = value;
                    RaisePropertyChanged(TasaDeCambioPropertyName);
                }
            }
        }

        public decimal MontoRecibidoML {
            get {
                return _MontoRecibidoML;
            }
            set {
                if (_MontoRecibidoML != value) {
                    _MontoRecibidoML = value;
                    RaisePropertyChanged(MontoRecibidoMLPropertyName);
                }
            }
        }

        public decimal MontoRecibidoME {
            get {
                return _MontoRecibidoME;
            }
            set {
                if (_MontoRecibidoME != value) {
                    _MontoRecibidoME = value;
                    RaisePropertyChanged(MontoRecibidoMEPropertyName);
                }
            }
        }

        public decimal MontoARegistrar {
            get {
                return _MontoARegistrar;
            }
            set {
                if (_MontoARegistrar != value) {
                    _MontoARegistrar = value;
                    RaisePropertyChanged(MontoARegistrarPropertyName);
                    RaisePropertyChanged(IsVisibleAdvertenciaPropertyName);
                }
            }
        }

        public bool IsVisibleAdvertencia {
            get {
                return LibMath.Abs(MontoRecibidoME - MontoARegistrar) > 0.05m;
            }
        }

        public RelayCommand GuardarCommand { get; private set; }

        private bool CanExecuteGuardarCommand() { return true; }

        #endregion //Propiedades
        #region Constructores
        public ValidacionZelleViewModel(decimal valMontoRecibidoML) {
            MontoRecibidoML = valMontoRecibidoML;
            ObtenerTasaDeCambio();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel() {
            base.InitializeLookAndFeel();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            GuardarCommand = new RelayCommand(ExecuteGuardarCommand, CanExecuteGuardarCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection.Clear();
                RibbonData.AddTabData(new LibRibbonTabData());
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateGuardarRibbonButtonGroup());
            }
        }


        LibRibbonGroupData CreateGuardarRibbonButtonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Comandos");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Guardar",
                Command = GuardarCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F6.png", UriKind.Relative),
                ToolTipDescription = "Guarda los cambios en " + ModuleName + ".",
                ToolTipTitle = "Ejecutar Acción (F6)",
                IsVisible = true,
                KeyTip = "F6"
            });            
            return vResult;
        }

        protected void ExecuteGuardarCommand() {
            RaiseRequestCloseEvent();        
        }

        private void ObtenerTasaDeCambio() {
            string vMonedaExtranjera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
            Comun.Ccl.TablasGen.ICambioPdn vCambio = new Comun.Brl.TablasGen.clsCambioNav();
            decimal vTasaCambio = 1;
            DateTime vToday = LibDate.Today();
            vCambio.BuscarUltimoCambioDeMoneda(vMonedaExtranjera, out vToday, out vTasaCambio);
            TasaDeCambio = vTasaCambio;
            MontoRecibidoME = LibMath.RoundToNDecimals(MontoRecibidoML / TasaDeCambio, 2);
            MontoARegistrar = MontoRecibidoME;
        }
        #endregion //Metodos Generados
    } //End of class ValidacionZelleViewModel
} //End of namespace Galac.Adm.Uil.Venta

