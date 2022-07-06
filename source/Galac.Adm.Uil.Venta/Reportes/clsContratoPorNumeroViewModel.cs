using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Brl.Venta;
using LibGalac.Aos.UI.Mvvm.Command;
using Galac.Adm.Uil.Venta.ViewModel;

namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsContratoPorNumeroViewModel : LibInputRptViewModelBase<Contrato> {
        #region Variables
        public const string NumeroContratoPropertyName = "NumeroContrato";
        string _NumeroContrato;
        private FkContratoViewModel _ConexionNumeroContrato;
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
        public const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        private eCantidadAImprimir _CantidadAImprimir;
        */
        #endregion //Codigo Ejemplo
        #endregion //Variables
        #region Propiedades
        public override string DisplayName {
            get { return "Contrato Por Número"; }
        }

        public string NumeroContrato {
            get {
                return _NumeroContrato;
            }
            set {
                if(_NumeroContrato != value) {
                    _NumeroContrato = value;
                    RaisePropertyChanged(NumeroContratoPropertyName);
                    if(LibString.IsNullOrEmpty(NumeroContrato, true)) {
                        ConexionNumeroContrato = null;
                    }
                }
            }
        }

        public override bool IsSSRS {
            get { return false; }
        }
        public FkContratoViewModel ConexionNumeroContrato {
            get {
                return _ConexionNumeroContrato;
            }
            set {
                if(_ConexionNumeroContrato != value) {
                    _ConexionNumeroContrato = value;
                    RaisePropertyChanged(NumeroContratoPropertyName);
                }
                if(_ConexionNumeroContrato == null) {
                    NumeroContrato = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseNumeroContratoCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores

        public clsContratoPorNumeroViewModel() {
            
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsContratoNav();
        
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public eCantidadAImprimir CantidadAImprimir {
            get {
                return _CantidadAImprimir;
            }
            set {
                if (_CantidadAImprimir != value) {
                    _CantidadAImprimir = value;
                    RaisePropertyChanged(CantidadAImprimirPropertyName);
                }
            }
        }

        public eCantidadAImprimir[] ECantidadAImprimir {
            get {
                return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray();
            }
        }
        */
        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNumeroContratoCommand = new RelayCommand<string>(ExecuteChooseNumeroContratoCommand);
        }

        private void ExecuteChooseNumeroContratoCommand(string valNumero) {
            try {
                if(valNumero == null) {
                    valNumero = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("NumeroContrato", valNumero);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionNumeroContrato = ChooseRecord<FkContratoViewModel>("Contrato", vDefaultCriteria, vFixedCriteria, string.Empty);
                if(ConexionNumeroContrato != null) {
                    NumeroContrato = ConexionNumeroContrato.NumeroContrato;
                } else {
                    NumeroContrato = string.Empty;
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }
        #endregion //Codigo Ejemplo
    } //End of class clsContratoPorNumeroViewModel
} //End of namespace Galac.Adm.Uil.Venta

