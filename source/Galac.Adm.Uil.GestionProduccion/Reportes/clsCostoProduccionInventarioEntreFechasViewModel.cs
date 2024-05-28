using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Cib;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.GestionProduccion;
using Galac.Adm.Brl.GestionProduccion;
using Galac.Adm.Uil.GestionProduccion.ViewModel;
using LibGalac.Aos.UI.Mvvm.Validation;
using System.ComponentModel.DataAnnotations;
using Galac.Saw.Lib;
using System.Collections.ObjectModel;

namespace Galac.Adm.Uil.GestionProduccion.Reportes {
    public class clsCostoProduccionInventarioEntreFechasViewModel : LibInputRptViewModelBase<OrdenDeProduccion> {
        #region Constantes
        public const string GeneradoPorPropertyName = "GeneradoPor";
        public const string CodigoDeOrdenPropertyName = "CodigoDeOrden";
        public const string IsEnabledCodigoDeOrdenPropertyName = "IsEnabledCodigoDeOrden";
        public const string IsEnabledFechaPropertyName = "IsEnabledFecha";
        public const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        public const string CodigoArticuloInventarioPropertyName = "CodigoArticuloInventario";
        public const string DescripcionArticuloInventarioPropertyName = "DescripcionArticuloInventario";
        public const string IsEnabledCodigoArticuloInventarioPropertyName = "IsEnabledCodigoArticuloInventario";
        public const string FechaInicialPropertyName = "FechaInicial";
        public const string FechaFinalPropertyName = "FechaFinal";
        #endregion
        #region Variables
        private Galac.Adm.Ccl.GestionProduccion.eGeneradoPor _GeneradoPor;
        private string _CodigoDeOrden;
        private bool _IsEnabledCodigoDeOrden;
        private bool _IsEnabledFecha;
        private eCantidadAImprimir _CantidadAImprimir;
        private string _CodigoArticuloInventario;
        private string _DescripcionArticuloInventario;
        private bool _IsEnabledCodigoArticuloInventario;
        private FkOrdenProduccionArticuloProducirViewModel _ConexionCodigoArticuloInventario = null;
        private DateTime _FechaInicial;
        private DateTime _FechaFinal;
        private FkOrdenDeProduccionViewModel _ConexionCodigoDeOrden = null;
        eMonedaDelInformeMM _MonedaDelInforme;
        eTasaDeCambioParaImpresion _TasaDeCambio;
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
        public const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        private eCantidadAImprimir _CantidadAImprimir;
        */
        #endregion //Codigo Ejemplo
        #endregion //Variables
        #region Propiedades
        public FkOrdenDeProduccionViewModel ConexionCodigoDeOrden {
            get {
                return _ConexionCodigoDeOrden;
            }
            set {
                if (_ConexionCodigoDeOrden != value) {
                    _ConexionCodigoDeOrden = value;
                    RaisePropertyChanged(CodigoDeOrdenPropertyName);
                }
                if (_ConexionCodigoDeOrden == null) {
                    _CodigoDeOrden = string.Empty;
                }
            }
        }

        [LibCustomValidation("IsCodigoDeOrdenRequired")]
        public string CodigoDeOrden {
            get {
                return _CodigoDeOrden;
            }
            set {
                if (_CodigoDeOrden != value) {
                    _CodigoDeOrden = value;
                    RaisePropertyChanged(CodigoDeOrdenPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoDeOrden, true)) {
                        ConexionCodigoDeOrden = null;
                    }
                }
            }
        }

        public eGeneradoPor GeneradoPor {
            get {
                return _GeneradoPor;
            }
            set {
                if (_GeneradoPor != value) {
                    _GeneradoPor = value;
                    RaisePropertyChanged(GeneradoPorPropertyName);
                    RaisePropertyChanged(IsEnabledCodigoDeOrdenPropertyName);
                    RaisePropertyChanged(IsEnabledFechaPropertyName);
                    if (eGeneradoPor.Orden.Equals(_GeneradoPor)) {
                        IsEnabledCodigoDeOrden = true;
                        IsEnabledFecha = false;
                    } else {
                        IsEnabledCodigoDeOrden = false;
                        IsEnabledFecha = true;
                        CodigoDeOrden = string.Empty;
                    }
                }
            }
        }

        public bool IsEnabledCodigoDeOrden {
            get {
                return _IsEnabledCodigoDeOrden;
            }
            set {
                if (_IsEnabledCodigoDeOrden != value) {
                    _IsEnabledCodigoDeOrden = value;
                    RaisePropertyChanged(IsEnabledCodigoDeOrdenPropertyName);
                }
            }
        }

        public bool IsEnabledFecha {
            get {
                return _IsEnabledFecha;
            }
            set {
                if (_IsEnabledFecha != value) {
                    _IsEnabledFecha = value;
                    RaisePropertyChanged(IsEnabledFechaPropertyName);
                }
            }
        }

        public FkOrdenProduccionArticuloProducirViewModel ConexionCodigoArticuloInventario {
            get {
                return _ConexionCodigoArticuloInventario;
            }
            set {
                if (_ConexionCodigoArticuloInventario != value) {
                    _ConexionCodigoArticuloInventario = value;
                    RaisePropertyChanged(CodigoArticuloInventarioPropertyName);
                }
                if (_ConexionCodigoArticuloInventario == null) {
                    _CodigoArticuloInventario = string.Empty;
                }
            }
        }

        [LibCustomValidation("IsCodigoArticuloRequired")]
        public string CodigoArticuloInventario {
            get {
                return _CodigoArticuloInventario;
            }
            set {
                if (_CodigoArticuloInventario != value) {
                    _CodigoArticuloInventario = value;
                    RaisePropertyChanged(CodigoArticuloInventarioPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoArticuloInventario, true)) {
                        ConexionCodigoArticuloInventario = null;
                        DescripcionArticuloInventario = string.Empty;
                    } else {
                        DescripcionArticuloInventario = ConexionCodigoArticuloInventario.DescripcionArticulo;
                    }
                }
            }
        }

        public string DescripcionArticuloInventario {
            get {
                return _DescripcionArticuloInventario;
            }
            set {
                if (_DescripcionArticuloInventario != value) {
                    _DescripcionArticuloInventario = value;
                    RaisePropertyChanged(DescripcionArticuloInventarioPropertyName);
                }
            }
        }

        public eCantidadAImprimir CantidadAImprimir {
            get {
                return _CantidadAImprimir;
            }
            set {
                if (_CantidadAImprimir != value) {
                    _CantidadAImprimir = value;
                    RaisePropertyChanged(CantidadAImprimirPropertyName);
                    RaisePropertyChanged(IsEnabledCodigoArticuloInventarioPropertyName);
                    if (eCantidadAImprimir.One.Equals(_CantidadAImprimir)) {
                        IsEnabledCodigoArticuloInventario = true;
                    } else {
                        IsEnabledCodigoArticuloInventario = false;
                        DescripcionArticuloInventario = string.Empty;
                        CodigoArticuloInventario = string.Empty;
                    }
                }
            }
        }

        public DateTime FechaInicial {
            get {
                return _FechaInicial;
            }
            set {
                if (_FechaInicial != value) {
                    _FechaInicial = value;
                    RaisePropertyChanged(FechaInicialPropertyName);
                    if (LibDate.F1IsGreaterThanF2(FechaInicial, FechaFinal)) {
                        FechaFinal = FechaInicial;
                        RaisePropertyChanged(FechaFinalPropertyName);
                    }
                }
            }
        }

        public DateTime FechaFinal {
            get {
                return _FechaFinal;
            }
            set {
                if (_FechaFinal != value) {
                    _FechaFinal = value;
                    RaisePropertyChanged(FechaFinalPropertyName);
                    if (LibDate.F1IsLessThanF2(FechaFinal, FechaInicial)) {
                        FechaInicial = FechaFinal;
                        RaisePropertyChanged(FechaFinalPropertyName);
                    }
                }
            }
        }

        public override string DisplayName {
            get { return "Costos de Inventario entre Fechas"; }
        }

        public override bool IsSSRS {
            get { return false; }
        }

        public RelayCommand<string> ChooseCodigoArticuloInventarioCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoDeOrdenCommand {
            get;
            private set;
        }

        public bool IsEnabledCodigoArticuloInventario {
            get {
                return _IsEnabledCodigoArticuloInventario;
            }
            set {
                if (_IsEnabledCodigoArticuloInventario != value) {
                    _IsEnabledCodigoArticuloInventario = value;
                    RaisePropertyChanged(IsEnabledCodigoArticuloInventarioPropertyName);
                }
            }
        }

        public bool IsVisibleTasaDeCambio { get { return MonedaDelInforme == eMonedaDelInformeMM.EnBolivares || MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa; } }

        public bool IsVisibleMonedasActivas { get { return MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa; } }

        public string Moneda { get; set; }

        public ObservableCollection<eTasaDeCambioParaImpresion> ListaTasaDeCambio { get; set; }

        public ObservableCollection<eMonedaDelInformeMM> ListaMonedaDelInforme { get; set; }

        public ObservableCollection<string> ListaMonedasActivas { get; set; }
        #endregion
        #region Constructores

        public clsCostoProduccionInventarioEntreFechasViewModel() {
            _GeneradoPor = eGeneradoPor.Fecha;
            _CodigoDeOrden = string.Empty;
            _CantidadAImprimir = eCantidadAImprimir.All;
            _CodigoArticuloInventario = string.Empty;
            _FechaInicial = DateTime.Today;
            _FechaFinal = DateTime.Today;
            _IsEnabledFecha = true;
            _IsEnabledCodigoDeOrden = false;
            GeneradoPor = eGeneradoPor.Orden;
            LlenarListaMonedaDelInforme();
            LlenarListaMonedasActivas();
            LlenarListaTasaDeCambio();
            RaisePropertyChanged(() => IsVisibleTasaDeCambio);
            #region Codigo Ejemplo
            /* Codigo de Ejemplo
            FechaDesde = LibDate.AddsNMonths(LibDate.Today(), - 1, false);
            FechaHasta = LibDate.Today();
        */
            #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        public eGeneradoPor[] EGeneradoPor {
            get {
                return LibEnumHelper<eGeneradoPor>.GetValuesInArray();
            }
        }

        private void ExecuteChooseCodigoDeOrdenCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Adm.Gv_OrdenDeProduccion_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_OrdenDeProduccion_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Adm.Gv_OrdenDeProduccion_B1.StatusOp", (int)eTipoStatusOrdenProduccion.Cerrada), eLogicOperatorType.And);
                ConexionCodigoDeOrden = ChooseRecord<FkOrdenDeProduccionViewModel>("Orden de Producción", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoDeOrden != null) {
                    CodigoDeOrden = ConexionCodigoDeOrden.Codigo;
                } else {
                    CodigoDeOrden = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Galac.Adm.Rpt.GestionProduccion.clsListaDeMaterialesDeSalida.ReportName);
            }
        }

        public eCantidadAImprimir[] ECantidadAImprimir {
            get {
                return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray();
            }
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoArticuloInventarioCommand = new RelayCommand<string>(ExecuteChooseCodigoArticuloInventarioCommand);
            ChooseCodigoDeOrdenCommand = new RelayCommand<string>(ExecuteChooseCodigoDeOrdenCommand);
        }

        private void ExecuteChooseCodigoArticuloInventarioCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Adm.Gv_OrdenDeProduccionDetalleArticulo_B1.CodigoArticulo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_OrdenDeProduccionDetalleArticulo_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionCodigoArticuloInventario = ChooseRecord<FkOrdenProduccionArticuloProducirViewModel>("Orden de Producción Articulo", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoArticuloInventario != null) {
                    CodigoArticuloInventario = ConexionCodigoArticuloInventario.CodigoArticulo;
                    DescripcionArticuloInventario = ConexionCodigoArticuloInventario.DescripcionArticulo;
                } else {
                    CodigoArticuloInventario = string.Empty;
                    DescripcionArticuloInventario = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Galac.Adm.Rpt.GestionProduccion.clsListaDeMaterialesDeSalida.ReportName);
            }
        }

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsOrdenDeProduccionNav();
        }
        #endregion //Metodos Generados
        #region Código Programador
        private ValidationResult IsCodigoArticuloRequired() {
            ValidationResult vResult = ValidationResult.Success;
            if (IsEnabledCodigoArticuloInventario && LibText.IsNullOrEmpty(CodigoArticuloInventario)) {
                vResult = new ValidationResult("Debe seleccionar un Articulo de Inventario a consultar.");
            }
            return vResult;
        }

        private ValidationResult IsCodigoDeOrdenRequired() {
            ValidationResult vResult = ValidationResult.Success;
            if (IsEnabledCodigoDeOrden && LibText.IsNullOrEmpty(CodigoDeOrden)) {
                vResult = new ValidationResult("Debe seleccionar una Orden de Producción a consultar.");
            }
            return vResult;
        }
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

        void LlenarListaMonedaDelInforme() {
            ListaMonedaDelInforme = new ObservableCollection<eMonedaDelInformeMM>();
            ListaMonedaDelInforme.Clear();
            ListaMonedaDelInforme.Add(eMonedaDelInformeMM.EnBolivares);
            ListaMonedaDelInforme.Add(eMonedaDelInformeMM.EnMonedaOriginal);
            ListaMonedaDelInforme.Add(eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa);
            MonedaDelInforme = eMonedaDelInformeMM.EnMonedaOriginal;
        }

        void LlenarListaMonedasActivas() {
            ListaMonedasActivas = new Galac.Saw.Lib.clsLibSaw().ListaDeMonedasActivasParaInformes(false);
            if (ListaMonedasActivas.Count > 0) {
                Moneda = ListaMonedasActivas[0];
            }
        }
        void LlenarListaTasaDeCambio() {
            ListaTasaDeCambio = new ObservableCollection<eTasaDeCambioParaImpresion>();
            ListaTasaDeCambio.Clear();
            ListaTasaDeCambio.Add(eTasaDeCambioParaImpresion.Original);
            ListaTasaDeCambio.Add(eTasaDeCambioParaImpresion.DelDia);
            TasaDeCambio = eTasaDeCambioParaImpresion.Original;
        }
        #endregion
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
        #endregion //Codigo Ejemplo
    } //End of class clsCostoProduccionInventarioEntreFechasViewModel
} //End of namespace Galac.Adm.Uil. GestionProduccion

