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
    public class clsCostoMatServUtilizadosEnProduccionInvViewModel : LibInputRptViewModelBase<OrdenDeProduccion> {

        #region Constantes

        private const string GeneradoPorPropertyName = "GeneradoPor";
        private const string CodigoDeOrdenPropertyName = "CodigoDeOrden";
        private const string IsEnabledCodigoDeOrdenPropertyName = "IsEnabledCodigoDeOrden";
        private const string IsEnabledFechaPropertyName = "IsEnabledFecha";
        private const string FechaInicialPropertyName = "FechaInicial";
        private const string FechaFinalPropertyName = "FechaFinal";

        #endregion

        #region Variables

        private eGeneradoPor _GeneradoPor;
        private string _CodigoDeOrden;
        private DateTime _FechaInicial;
        private DateTime _FechaFinal;
        private bool _IsEnabledCodigoDeOrden;
        private bool _IsEnabledFecha;
        private FkOrdenDeProduccionViewModel _ConexionCodigoDeOrden = null;
        eMonedaDelInformeMM _MonedaDelInforme;
        eTasaDeCambioParaImpresion _TasaDeCambio;

        #endregion

        #region Propiedades

        public override string DisplayName {
            get { return "Costos de Materiales o Servicios utilizados en Producción de Inventario"; }
        }

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

        public override bool IsSSRS {
            get { return false; }
        }

        public RelayCommand<string> ChooseCodigoDeOrdenCommand {
            get;
            private set;
        }

        public bool IsVisibleTasaDeCambio { get { return MonedaDelInforme == eMonedaDelInformeMM.EnBolivares || MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa; } }

        public bool IsVisibleMonedasActivas { get { return MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa; } }

        public string Moneda { get; set; }

        public ObservableCollection<eTasaDeCambioParaImpresion> ListaTasaDeCambio { get; set; }

        public ObservableCollection<eMonedaDelInformeMM> ListaMonedaDelInforme { get; set; }

        public ObservableCollection<string> ListaMonedasActivas { get; set; }
        #endregion

        #region Constructores

        public clsCostoMatServUtilizadosEnProduccionInvViewModel() {
            _GeneradoPor = eGeneradoPor.Orden;
            _CodigoDeOrden = string.Empty;
            _FechaInicial = DateTime.Today;
            _FechaFinal = DateTime.Today;
            _IsEnabledFecha = false;
            _IsEnabledCodigoDeOrden = true;
            LlenarListaMonedaDelInforme();
            LlenarListaMonedasActivas();
            LlenarListaTasaDeCambio();
            RaisePropertyChanged(() => IsVisibleTasaDeCambio);
        }

        #endregion //Constructores

        #region Metodos Generados

        public eGeneradoPor[] EGeneradoPor {
            get {
                return LibEnumHelper<eGeneradoPor>.GetValuesInArray();
            }
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoDeOrdenCommand = new RelayCommand<string>(ExecuteChooseCodigoDeOrdenCommand);

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
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Rpt.GestionProduccion.clsListaDeMaterialesDeSalida.ReportName);
            }
        }

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsOrdenDeProduccionNav();
        }

        #endregion //Metodos Generados

        #region Código Programador

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
            ListaMonedasActivas = new clsLibSaw().ListaDeMonedasActivasParaInformes(false);
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

    } //End of class clsCostoMatServUtilizadosEnProduccionInvViewModel
} //End of namespace Galac.Adm.Uil. GestionProduccion

