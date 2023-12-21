using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Saw.Ccl.Cliente;
using Galac.Saw.Brl.Cliente;
using Galac.Saw.Uil.Cliente.ViewModel;
using Entity = Galac.Saw.Ccl.Cliente;
using Galac.Saw.Lib;
using System.Collections.ObjectModel;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Validation;
using System.ComponentModel.DataAnnotations;

namespace Galac.Saw.Uil.Cliente.Reportes {
    public class clsHistoricoDeClienteViewModel : LibInputRptViewModelBase<Entity.Cliente> {
        #region Variables
        string _NombreCliente;
        eMonedaDelInformeMM _MonedaDelInforme;
        eTasaDeCambioParaImpresion _TasaDeCambio;
        eCantidadAImprimir _CantidadAImprimir;
        FkClienteViewModel _ConexionNombreCliente = null;
        #endregion //Variables
        #region Propiedades
        public override string DisplayName { get { return "Histórico de Cliente"; } }
        public override bool IsSSRS { get { return false; } }
        public eCantidadAImprimir CantidadAImprimir {
            get { return _CantidadAImprimir; }
            set {
                if (_CantidadAImprimir != value) {
                    _CantidadAImprimir = value;
                    RaisePropertyChanged(() => IsVisibleNombreDelCliente);
                    RaisePropertyChanged(() => IsVisibleSaltoDePaginaPorCliente);
                }
            }
        }
        public string CodigoCliente { get; set; }
        [LibCustomValidation("NombreClienteValidating")]
        public string NombreCliente {
            get { return _NombreCliente; }
            set {
                if (_NombreCliente != value) {
                    _NombreCliente = value;
                    RaisePropertyChanged(() => NombreCliente);
                }
            }
        }
        public bool SaltoDePaginaPorCliente { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string Moneda { get; set; }
        public eMonedaDelInformeMM MonedaDelInforme {
            get { return _MonedaDelInforme; }
            set {
                if (_MonedaDelInforme != value) {
                    _MonedaDelInforme = value;
                    RaisePropertyChanged(() => MonedaDelInforme);
                    RaisePropertyChanged(() => IsVisibleMonedasActivas);
                    RaisePropertyChanged(() => IsVisibleTasaDeCambio);
                }
            }
        }
        public eTasaDeCambioParaImpresion TasaDeCambio {
            get { return _TasaDeCambio; }
            set {
                if (_TasaDeCambio != value) {
                    _TasaDeCambio = value;
                    RaisePropertyChanged(() => TasaDeCambio);
                }
            }
        }
        public eClientesOrdenadosPor OrdenarPor { get; set; }
        public eTasaDeCambioParaImpresion[] ListaTasaDeCambio { get { return LibEnumHelper<eTasaDeCambioParaImpresion>.GetValuesInArray(); } }
        public eMonedaDelInformeMM[] ListaMonedaDelInforme { get { return LibEnumHelper<eMonedaDelInformeMM>.GetValuesInArray(); } }
        public ObservableCollection<string> ListaMonedasActivas { get; set; }
        public eCantidadAImprimir[] ListaCantidadAImprimir { get { return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray(); } }
        public eClientesOrdenadosPor[] ListaOrdenarPor { get { return LibEnumHelper<eClientesOrdenadosPor>.GetValuesInArray(); } }
        public bool IsVisibleMonedasActivas { get { return MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa; } }
        public bool IsVisibleTasaDeCambio { get { return MonedaDelInforme == eMonedaDelInformeMM.EnBolivares || MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa; } }
        public bool IsVisibleSaltoDePaginaPorCliente { get { return CantidadAImprimir == eCantidadAImprimir.All; } }
        public bool IsVisibleNombreDelCliente { get { return CantidadAImprimir == eCantidadAImprimir.One; } }
        public FkClienteViewModel ConexionNombreCliente {
            get {
                return _ConexionNombreCliente;
            }
            set {
                if (_ConexionNombreCliente != value) {
                    _ConexionNombreCliente = value;
                }
                if (_ConexionNombreCliente == null) {
                    CodigoCliente = string.Empty;
                    NombreCliente = string.Empty;
                } else {
                    CodigoCliente = _ConexionNombreCliente.Codigo;
                    NombreCliente = _ConexionNombreCliente.Nombre;
                }
            }
        }

        public RelayCommand<string> ChooseNombreClienteCommand { get; private set; }

        #endregion Propiedades
        #region Constructores
        public clsHistoricoDeClienteViewModel() {
            FechaDesde = LibDate.DateFromMonthAndYear(1, LibDate.Today().Year, true);
            FechaHasta = LibDate.Today();
            LlenarListaMonedasActivas();
        }
        #endregion //Constructores
        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsClienteNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNombreClienteCommand = new RelayCommand<string>(ExecuteChooseNombreClienteCommand);
        }

        private void ExecuteChooseNombreClienteCommand(string valNombreCliente) {
            try {
                if (valNombreCliente == null) {
                    valNombreCliente = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombreCliente);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionNombreCliente = null;
                ConexionNombreCliente = ChooseRecord<FkClienteViewModel>("Cliente", vDefaultCriteria, vFixedCriteria, string.Empty);

            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }

        ValidationResult NombreClienteValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (CantidadAImprimir == eCantidadAImprimir.One && LibString.IsNullOrEmpty(NombreCliente, true)) {
                vResult = new ValidationResult("El nombre del cliente no puede estar en blanco.");
            }
            return vResult;
        }

        void LlenarListaMonedasActivas() {
            ListaMonedasActivas = new Galac.Saw.Lib.clsLibSaw().ListaDeMonedasActivasParaInformes();
            if (ListaMonedasActivas.Count > 0) {
                Moneda = ListaMonedasActivas[0];
            }
        }

    } //End of class clsHistoricoDeClienteViewModel

} //End of namespace Galac.Saw.Uil.Cliente