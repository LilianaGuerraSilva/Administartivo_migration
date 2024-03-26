using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Uil.GestionCompras.ViewModel;
using Entity = Galac.Adm.Ccl.GestionCompras;
using Galac.Saw.Lib;
using System.Collections.ObjectModel;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Validation;
using System.ComponentModel.DataAnnotations;
using LibGalac.Aos.Uil;

namespace Galac.Adm.Uil.GestionCompras.Reportes {
    public class clsHistoricoDeProveedorViewModel:  LibInputRptViewModelBase<Entity.Proveedor> {
        #region Constantes
        const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        const string NombreProveedorPropertyName = "NombreProveedor";
        const string SaltoDePaginaPorProveedorPropertyName = "SaltoDePaginaPorProveedor";
        const string FechaDesdePropertyName = "FechaDesde";
        const string FechaHastaPropertyName = "FechaHasta";
        const string MonedaDelInformePropertyName = "MonedaDelInforme";
        const string MonedaPropertyName = "Moneda";
        const string TasaDeCambioPropertyName = "TasaDeCambio";
        const string OrdenarPorPropertyName = "OrdenarPor";
        const string IsVisibleNombreDelProveedorPropertyName = "IsVisibleNombreDelProveedor";
        const string IsVisibleSaltoDePaginaPorProveedorPropertyName = "IsVisibleSaltoDePaginaPorProveedor";
        const string IsVisibleMonedasActivasPropertyName = "IsVisibleMonedasActivas";
        const string IsVisibleTasaDeCambioPropertyName = "IsVisibleTasaDeCambio";
        #endregion Constantes
        #region Variables
        string _NombreProveedor;
        eMonedaDelInformeMM _MonedaDelInforme;
        eTasaDeCambioParaImpresion _TasaDeCambio;
        eCantidadAImprimir _CantidadAImprimir;
        FkProveedorViewModel _ConexionNombreProveedor = null;
        bool _SaltoDePaginaPorProveedor;
        DateTime _FechaDesde;
        DateTime _FechaHasta;
        eProveedorOrdenadosPor _OrdenarPor;
        string _Moneda;
        #endregion //Variables
        #region Propiedades
        public override string DisplayName { get { return "Histórico de Proveedor"; } }
        public override bool IsSSRS { get { return false; } }
        public eCantidadAImprimir CantidadAImprimir {
            get { return _CantidadAImprimir; }
            set {
                if (_CantidadAImprimir != value) {
                    _CantidadAImprimir = value;
                    if (_CantidadAImprimir == eCantidadAImprimir.All) {
                        CodigoProveedor = string.Empty;
                        NombreProveedor = string.Empty;
                    }
                    RaisePropertyChanged(CantidadAImprimirPropertyName);
                    RaisePropertyChanged(IsVisibleNombreDelProveedorPropertyName);
                    RaisePropertyChanged(IsVisibleSaltoDePaginaPorProveedorPropertyName);
                }
            }
        }
        public string CodigoProveedor { get; set; }
        [LibCustomValidation("NombreProveedorValidating")]
        public string NombreProveedor {
            get { return _NombreProveedor; }
            set {
                if (_NombreProveedor != value) {
                    _NombreProveedor = value;
                    RaisePropertyChanged(NombreProveedorPropertyName);
                }
            }
        }

        public bool SaltoDePaginaPorProveedor {
            get { return _SaltoDePaginaPorProveedor; }
            set {
                if (_SaltoDePaginaPorProveedor != value) {
                    _SaltoDePaginaPorProveedor = value;
                    RaisePropertyChanged(SaltoDePaginaPorProveedorPropertyName);
                }
            }
        }

        public DateTime FechaDesde {
            get { return _FechaDesde; }
            set {
                if (_FechaDesde != value) {
                    _FechaDesde = value;
                    RaisePropertyChanged(FechaDesdePropertyName);
                }
            }
        }

        public DateTime FechaHasta {
            get { return _FechaHasta; }
            set {
                if (_FechaHasta != value) {
                    _FechaHasta = value;
                    RaisePropertyChanged(FechaHastaPropertyName);
                }
            }
        }

        public eMonedaDelInformeMM MonedaDelInforme {
            get { return _MonedaDelInforme; }
            set {
                if (_MonedaDelInforme != value) {
                    _MonedaDelInforme = value;
                    RaisePropertyChanged(MonedaDelInformePropertyName);
                    RaisePropertyChanged(IsVisibleMonedasActivasPropertyName);
                    RaisePropertyChanged(IsVisibleTasaDeCambioPropertyName);
                }
            }
        }

        public string Moneda {
            get { return _Moneda; }
            set {
                if (_Moneda != value) {
                    _Moneda = value;
                    RaisePropertyChanged(MonedaPropertyName);
                }
            }
        }

        public eTasaDeCambioParaImpresion TasaDeCambio {
            get { return _TasaDeCambio; }
            set {
                if (_TasaDeCambio != value) {
                    _TasaDeCambio = value;
                    RaisePropertyChanged(TasaDeCambioPropertyName);
                }
            }
        }


        public eProveedorOrdenadosPor OrdenarPor {
            get { return _OrdenarPor; }
            set {
                if (_OrdenarPor != value) {
                    _OrdenarPor = value;
                    RaisePropertyChanged(OrdenarPorPropertyName);
                }
            }
        }
        public eTasaDeCambioParaImpresion[] ListaTasaDeCambio { get { return LibEnumHelper<eTasaDeCambioParaImpresion>.GetValuesInArray(); } }
        public eMonedaDelInformeMM[] ListaMonedaDelInforme { get { return LibEnumHelper<eMonedaDelInformeMM>.GetValuesInArray(); } }
        public ObservableCollection<string> ListaMonedasActivas { get; set; }
        public eCantidadAImprimir[] ListaCantidadAImprimir { get { return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray(); } }
        public eProveedorOrdenadosPor[] ListaOrdenarPor { get { return LibEnumHelper<eProveedorOrdenadosPor>.GetValuesInArray(); } }
        public bool IsVisibleMonedasActivas { get { return MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa; } }
        public bool IsVisibleTasaDeCambio { get { return MonedaDelInforme == eMonedaDelInformeMM.EnBolivares || MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa; } }
        public bool IsVisibleSaltoDePaginaPorProveedor { get { return CantidadAImprimir == eCantidadAImprimir.All; } }
        public bool IsVisibleNombreDelProveedor { get { return CantidadAImprimir == eCantidadAImprimir.One; } }
        public FkProveedorViewModel ConexionNombreProveedor {
            get {
                return _ConexionNombreProveedor;
            }
            set {
                if (_ConexionNombreProveedor != value) {
                    _ConexionNombreProveedor = value;
                }
                if (_ConexionNombreProveedor == null) {
                    CodigoProveedor = string.Empty;
                    NombreProveedor = string.Empty;
                } else {
                    CodigoProveedor = _ConexionNombreProveedor.CodigoProveedor;
                    NombreProveedor = _ConexionNombreProveedor.NombreProveedor;
                }
            }
        }

        public RelayCommand<string> ChooseNombreProveedorCommand { get; private set; }

        #endregion Propiedades
        #region Constructores
        public clsHistoricoDeProveedorViewModel() {
            FechaDesde = LibDate.DateFromMonthAndYear(1, LibDate.Today().Year, true);
            FechaHasta = LibDate.Today();
            CantidadAImprimir = eCantidadAImprimir.All;
            MonedaDelInforme = eMonedaDelInformeMM.EnMonedaOriginal;
            LlenarListaMonedasActivas();
        }
        #endregion //Constructores
        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsProveedorNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNombreProveedorCommand = new RelayCommand<string>(ExecuteChooseNombreProveedorCommand);
        }

        private void ExecuteChooseNombreProveedorCommand(string valNombreProveedor) {
            try {
                if (valNombreProveedor == null) {
                    valNombreProveedor = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("NombreProveedor", valNombreProveedor);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_Proveedor_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionNombreProveedor = null;
                ConexionNombreProveedor = LibFKRetrievalHelper.ChooseRecord<FkProveedorViewModel>("ProveedorInforme", vDefaultCriteria, vFixedCriteria, new clsProveedorNav(), string.Empty);
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }

        ValidationResult NombreProveedorValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (CantidadAImprimir == eCantidadAImprimir.One && LibString.IsNullOrEmpty(NombreProveedor, true)) {
                vResult = new ValidationResult("El nombre del Proveedor no puede estar en blanco.");
            }
            return vResult;
        }

        void LlenarListaMonedasActivas() {
            ListaMonedasActivas = new Galac.Saw.Lib.clsLibSaw().ListaDeMonedasActivasParaInformes(false);
            if (ListaMonedasActivas.Count > 0) {
                Moneda = ListaMonedasActivas[0];
            }
        }

    } //End of class clsHistoricoDeProveedorViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras