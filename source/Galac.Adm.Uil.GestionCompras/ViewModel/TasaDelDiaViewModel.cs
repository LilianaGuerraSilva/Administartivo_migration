using System;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Ribbon;


namespace Galac.Adm.Uil.GestionCompras.ViewModel {
    public class TasaDelDiaViewModel : LibGenericViewModel {
        #region Constantes
        public const string MonedaPropertyName = "Moneda";
        public const string NombreMonedaPropertyName = "NombreMoneda";
        public const string CambioAMonedaLocalPropertyName = "CambioAMonedaLocal";
        private string _Moneda;
        private string _NombreMoneda;
        private decimal _CambioAMonedaLocal;
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Tasa del Dia " + LibConvert.ToStr(Fecha); }
        }

        public string Moneda {
            get {
                return _Moneda;
            }
            set {
                if (_Moneda != value) {
                    _Moneda = value;
                    RaisePropertyChanged(MonedaPropertyName);
                }
            }
        }

        public string NombreMoneda {
            get {
                return _NombreMoneda;
            }
            set {
                if (_NombreMoneda != value) {
                    _NombreMoneda = value;
                    RaisePropertyChanged(NombreMonedaPropertyName);
                }
            }
        }

        public decimal CambioAMonedaLocal {
            get {
                return LibMath.RoundToNDecimals(_CambioAMonedaLocal, LibDefGen.ProgramInfo.IsCountryPeru() ? 3 : 4 );
            }
            set {
                if (_CambioAMonedaLocal != value) {
                    _CambioAMonedaLocal = value;
                    RaisePropertyChanged(CambioAMonedaLocalPropertyName);
                }
            }
        }

        public int DecimalesTasaDeCambio {
            get { return LibDefGen.ProgramInfo.IsCountryPeru() ? 3 : 4; }
        }
        public DateTime Fecha { get; set; }
		
        #endregion //Propiedades
        #region Constructores
        public TasaDelDiaViewModel(DateTime initFecha)
            : base() {
            Fecha = initFecha;
            Title = ModuleName;
            RibbonData.TabDataCollection[0].Header = ModuleName;
        }

        #endregion //Constructores
        #region Metodos Generados

        private LibRibbonButtonData CreateAccionRibbonGroup() {
            LibRibbonButtonData vResult = new LibRibbonButtonData() {
                Command = ModificarCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/saveAndClose.png", UriKind.Relative),
                Label = "Modificar y Salir",
                ToolTipDescription = string.Format("Ejecuta la acción Modificar y sale de la ventana."),
                ToolTipTitle = "Ejecutar Acción"
            };
            return vResult;
        }

        public RelayCommand ModificarCommand {
            get;
            private set;
        }

        private bool CanExecuteCommand() {
            return true;
        }

        private void ExecuteCommand() {
            try {
                DialogResult = true;
                RaiseRequestCloseEvent();

            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection.Insert(0, CreateAccionRibbonGroup());
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ModificarCommand = new RelayCommand(ExecuteCommand, CanExecuteCommand);
        }

        protected override void InitializeLookAndFeel() {
            base.InitializeLookAndFeel();


        }
        #endregion //Metodos Generados
    } //End of class TasaDelDiaViewModel
} //End of namespace Galac..Uil.ComponenteNoEspecificado

