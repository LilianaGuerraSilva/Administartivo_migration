using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.Base.Report;
//using LibGalac.Aos.UI.Cib;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.SttDef;
using Galac.Adm.Brl.Venta;
using LibGalac.Aos.Ccl.Usal;
using System.Collections.ObjectModel;
using Galac.Adm.Uil.Venta.ViewModel;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Brl.TablasGen;
using LibGalac.Aos.Uil;

namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsComisionDeVendedoresViewModel:LibInputRptViewModelBase<Cobranza> {
        #region Constantes
        private const string FechaInicialPropertyName = "FechaInicial";
        private const string FechaFinalPropertyName = "FechaFinal";
        private const string TipoDeInformePropertyName = "TipoDeInforme";
        private const string TipoCalculoComisionPropertyName = "TipoCalculoComision";
        private const string TipoDeComisionPropertyName = "TipoDeComision";
        private const string MonedaPropertyName = "Moneda";
        private const string TasaDeCambioPropertyName = "TasaDeCambio";
        private const string IsVisibleTasaDeCambioPropertyName = "IsVisibleTasaDeCambio";
        private const string IncluirComisionEnMonedaExtPropertyName = "IncluirComisionEnMonedaExt";
        private const string ComisionMonedaExtPropertyName = "ComisionMonedaExt";
        private const string IsVisibleIncluirCambioComisionMonedaExtPropertyName = "IsVisibleIncluirCambioComisionMonedaExt";
        private const string CambioComisionEnMonedaExtPropertyName = "CambioComisionEnMonedaExt";
        private const string TasaDeCambioComisionEnMonedaExtPropertyName = "TasaDeCambioComisionEnMonedaExt";
        private const string IsVisibleCambioComisionEnMonedaExtPropertyName = "IsVisibleCambioComisionEnMonedaExt";
        private const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        private const string NombreVendedorPropertyName = "NombreVendedor";
        private const string IsVisibleNombreVendedorPropertyName = "IsVisibleNombreVendedor";
        #endregion
        #region Variables
        private DateTime _FechaInicial;
        private DateTime _FechaFinal;
        private Saw.Lib.eTipoDeInforme _TipoDeInforme;
        private eTipoDeCalculoComision _TipoCalculoComision;
        private eCalculoParaComisionesSobreCobranzaEnBaseA _TipoDeComision;
        private Saw.Lib.eMonedaParaImpresion _Moneda;
        private Saw.Lib.eTasaDeCambioParaImpresion _TasaDeCambioImpresion;
        private bool _IsVisibleTasaDeCambio;
        private bool _IncluirComisionEnMonedaExt;
        private string _ComisionMonedaExt;
        private bool _IsVisibleIncluirCambioComisionMonedaExt;
        private string _CambioComisionEnMonedaExt;
        private decimal _TasaDeCambioComisionEnMonedaExt;
        private bool _IsVisibleCambioComisionEnMonedaExt;
        private eCantidadAImprimir _CantidadAImprimir;
        private string _NombreVendedor;
        private string _CodigoVendedor;
        private FkVendedorViewModel _ConexionNombreDelVendedor = null;
        private bool _IsVisibleNombreVendedor;

        #endregion //Variables
        #region Propiedades

        public override string DisplayName {
            get { return "Comisión de Vendedores"; }
        }

        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }
        public override bool IsSSRS {
            get { return false; }
        }

        public RelayCommand<string> ChooseNombreDelVendedorCommand {
            get;
            private set;
        }

        [LibCustomValidation("FechaInicialValidating")]
        public DateTime FechaInicial {
            get {
                return _FechaInicial;
            }
            set {
                if (_FechaInicial != value) {
                    _FechaInicial = value;
                    RaisePropertyChanged(FechaInicialPropertyName);
                }
            }
        }
        [LibCustomValidation("FechaFinalValidating")]
        public DateTime FechaFinal {
            get {
                return _FechaFinal;
            }
            set {
                if (_FechaFinal != value) {
                    _FechaFinal = value;
                    RaisePropertyChanged(FechaFinalPropertyName);
                }
            }
        }

        public Galac.Saw.Lib.eTipoDeInforme TipoDeInforme {
            get {
                return _TipoDeInforme;
            }
            set {
                if (_TipoDeInforme != value) {
                    _TipoDeInforme = value;
                    RaisePropertyChanged(TipoDeInformePropertyName);
                }
            }
        }

        public eTipoDeCalculoComision TipoCalculoComision {
            get {
                return _TipoCalculoComision;
            }
            set {
                if (_TipoCalculoComision != value) {
                    _TipoCalculoComision = value;
                    RaisePropertyChanged(TipoCalculoComisionPropertyName);
                }
            }
        }

        public eCalculoParaComisionesSobreCobranzaEnBaseA TipoDeComision {
            get {
                return _TipoDeComision;
            }
            set {
                if (_TipoDeComision != value) {
                    _TipoDeComision = value;
                    RaisePropertyChanged(TipoDeComisionPropertyName);
                }
            }
        }

        public Galac.Saw.Lib.eMonedaParaImpresion Moneda {
            get {
                return _Moneda;
            }
            set {
                if (_Moneda != value) {
                    _Moneda = value;
                    RaisePropertyChanged(MonedaPropertyName);
                    if (_Moneda == Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal) {
                        IsVisibleTasaDeCambio = false;
                        IncluirComisionEnMonedaExt = false;
                        IsVisibleIncluirCambioComisionMonedaExt = false;
                        IsVisibleCambioComisionEnMonedaExt = false;
                    } else {
                        IsVisibleTasaDeCambio = true;
                        IncluirComisionEnMonedaExt = false;
                        IsVisibleIncluirCambioComisionMonedaExt = EsPosibleIncluirComisionEnMonedaExt();
                    }
                }
            }
        }

        public bool IsVisibleTasaDeCambio {
            get {
                return _IsVisibleTasaDeCambio;
            }

            set {
                if (_IsVisibleTasaDeCambio != value) {
                    _IsVisibleTasaDeCambio = value;
                    RaisePropertyChanged(IsVisibleTasaDeCambioPropertyName);
                }
            }
        }



        public Galac.Saw.Lib.eTasaDeCambioParaImpresion TasaDeCambio {
            get {
                return _TasaDeCambioImpresion;
            }
            set {
                if (_TasaDeCambioImpresion != value) {
                    _TasaDeCambioImpresion = value;
                    RaisePropertyChanged(TasaDeCambioPropertyName);
                }
            }
        }

        public string ComisionMonedaExt {
            get {
                return _ComisionMonedaExt;
            }
            set {
                if (_ComisionMonedaExt != value) {
                    _ComisionMonedaExt = value;
                    RaisePropertyChanged(ComisionMonedaExtPropertyName);
                }
            }
        }

        public bool IsVisibleIncluirCambioComisionMonedaExt {
            get {
                return _IsVisibleIncluirCambioComisionMonedaExt;
            }
            set {
                if (_IsVisibleIncluirCambioComisionMonedaExt != value) {
                    _IsVisibleIncluirCambioComisionMonedaExt = value;
                    RaisePropertyChanged(IsVisibleIncluirCambioComisionMonedaExtPropertyName);
                }
            }
        }

        public bool IncluirComisionEnMonedaExt {
            get {
                return _IncluirComisionEnMonedaExt;
            }
            set {
                if (_IncluirComisionEnMonedaExt != value) {
                    _IncluirComisionEnMonedaExt = value;
                    RaisePropertyChanged(IncluirComisionEnMonedaExtPropertyName);
                    if (_IncluirComisionEnMonedaExt == true) {
                        IsVisibleCambioComisionEnMonedaExt = true;
                    } else {
                        IsVisibleCambioComisionEnMonedaExt = false;
                        RaisePropertyChanged(TasaDeCambioComisionEnMonedaExtPropertyName);
                    }
                }
            }
        }

        public bool IsVisibleCambioComisionEnMonedaExt {
            get {
                return _IsVisibleCambioComisionEnMonedaExt;
            }
            set {
                if (_IsVisibleCambioComisionEnMonedaExt != value) {
                    _IsVisibleCambioComisionEnMonedaExt = value;
                    RaisePropertyChanged(IsVisibleCambioComisionEnMonedaExtPropertyName);
                }
            }
        }

        public string CambioComisionEnMonedaExt {
            get {
                return _CambioComisionEnMonedaExt;
            }
            set {
                if (_CambioComisionEnMonedaExt != value) {
                    _CambioComisionEnMonedaExt = value;
                    RaisePropertyChanged(CambioComisionEnMonedaExtPropertyName);
                }
            }
        }

        public decimal TasaDeCambioComisionEnMonedaExt {
            get {
                return _TasaDeCambioComisionEnMonedaExt;
            }
            set {
                if (_TasaDeCambioComisionEnMonedaExt != value) {
                    _TasaDeCambioComisionEnMonedaExt  = value == 0 ? 1: value;
                    RaisePropertyChanged(TasaDeCambioComisionEnMonedaExtPropertyName);
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
                    if (_CantidadAImprimir == eCantidadAImprimir.One){
                        IsVisibleNombreVendedor = true;
                    } else {
                        ConexionNombreDelVendedor = null;
                        CodigoVendedor = string.Empty;
                        IsVisibleNombreVendedor = false;
                    }
                }
            }
        }

        public bool IsVisibleNombreVendedor {
            get {
                return _IsVisibleNombreVendedor;
            }
            set {
                if (_IsVisibleNombreVendedor != value) {
                    _IsVisibleNombreVendedor = value;
                    RaisePropertyChanged(IsVisibleNombreVendedorPropertyName);
                }
            }
        }

        [LibCustomValidation("NombreDelVendedorValidating")]
        [LibGridColum("Nombre del Vendedor", eGridColumType.Connection, IsForSearch = true, ConnectionDisplayMemberPath = "NombreVendedor", ConnectionModelPropertyName = "NombreVendedor", Header = "Nombre del Vendedor", ConnectionSearchCommandName = "ChooseNombreDelVendedorCommand", Width = 255)]
        public string NombreVendedor {
            get {
                return _NombreVendedor;
            }
            set {
                if (_NombreVendedor != value) {
                    _NombreVendedor = value;
                    RaisePropertyChanged(NombreVendedorPropertyName);
                }
            }
        }

        public string CodigoVendedor {
            get {
                return _CodigoVendedor;
            }
            set {
                if(_CodigoVendedor != value) {
                    _CodigoVendedor = value;
                }
            }
        }

        public FkVendedorViewModel ConexionNombreDelVendedor {
            get {
                return _ConexionNombreDelVendedor;
            }
            set {
                if (_ConexionNombreDelVendedor != value) {
                    _ConexionNombreDelVendedor = value;
                    RaisePropertyChanged(NombreVendedorPropertyName);
                }
                if (_ConexionNombreDelVendedor != null) {
                    NombreVendedor = _ConexionNombreDelVendedor.Nombre;
                }
                if (_ConexionNombreDelVendedor == null) {
                    NombreVendedor = string.Empty;
                }
            }
        }


        public Saw.Lib.eTipoDeInforme[] ArrayTipoDeInforme {
            get {
                return LibEnumHelper<Saw.Lib.eTipoDeInforme>.GetValuesInArray();
            }
        }

        public ObservableCollection<eTipoDeCalculoComision> _TipoDeCalculoComision = new ObservableCollection<eTipoDeCalculoComision>();
        public ObservableCollection<eTipoDeCalculoComision> ArrayTipoDeCalculoComision {
            get { return _TipoDeCalculoComision; }
            set { _TipoDeCalculoComision = value; }
        }

        private void LlenarArrayDeEnumerativosTipoDeCalculoComision() {
            ArrayTipoDeCalculoComision.Clear();
            ArrayTipoDeCalculoComision.Add(eTipoDeCalculoComision.PorCobranzas);
            // Aca se debe agregar el Enumerativo "Por ventas" cuando se desarrolle
        }

        public ObservableCollection<eCalculoParaComisionesSobreCobranzaEnBaseA> _CalculoParaComisionesSobreCobranzaEnBaseA = new ObservableCollection<eCalculoParaComisionesSobreCobranzaEnBaseA>();
        public ObservableCollection<eCalculoParaComisionesSobreCobranzaEnBaseA> ArrayCalculoParaComisionesSobreCobranzaEnBaseA {
            get { return _CalculoParaComisionesSobreCobranzaEnBaseA;  }
            set { _CalculoParaComisionesSobreCobranzaEnBaseA = value; }
        }

        private void LlenarArrayDeEnumerativosCalculoParaComisionesEnBaseA() {
            ArrayCalculoParaComisionesSobreCobranzaEnBaseA.Clear();
            ArrayCalculoParaComisionesSobreCobranzaEnBaseA.Add(eCalculoParaComisionesSobreCobranzaEnBaseA.Monto);
        }
        
        public ObservableCollection<Saw.Lib.eMonedaParaImpresion> _MonedaDeReporte = new ObservableCollection<Saw.Lib.eMonedaParaImpresion>();
        public ObservableCollection<Saw.Lib.eMonedaParaImpresion> ArrayMonedaDelInforme {
            get { return _MonedaDeReporte; }
            set { _MonedaDeReporte = value; }
        }

        private void LlenarArrayDeEnumerativosMonedaDelInforme() {
            ArrayMonedaDelInforme.Clear();
            ArrayMonedaDelInforme.Add(Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal);
            if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                ArrayMonedaDelInforme.Add(Saw.Lib.eMonedaParaImpresion.EnBolivares);
            } else if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                ArrayMonedaDelInforme.Add(Saw.Lib.eMonedaParaImpresion.EnSoles);
            }
        }
        
        public Saw.Lib.eTasaDeCambioParaImpresion[] ArrayTasaDeCambio {
            get {
                return LibEnumHelper<Saw.Lib.eTasaDeCambioParaImpresion>.GetValuesInArray();
            }
        }

        public eCantidadAImprimir[] ArrayCantidadAImprimir {
            get {
                return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray();
            }
        }
        #endregion //Propiedades
        #region Constructores

        public clsComisionDeVendedoresViewModel() {
            LlenarTodoArrayDeEnumerativos();
            FechaInicial = LibDate.AddsNMonths(LibDate.Today(), -1, false);
            FechaFinal = LibDate.Today();
            TipoDeInforme = Saw.Lib.eTipoDeInforme.Detallado;
            TipoCalculoComision = eTipoDeCalculoComision.PorCobranzas;
            TipoDeComision = eCalculoParaComisionesSobreCobranzaEnBaseA.Monto;
            Moneda = Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal;
            TasaDeCambio = Saw.Lib.eTasaDeCambioParaImpresion.Original;
            IsVisibleTasaDeCambio = false;
            IncluirComisionEnMonedaExt = false;
            IsVisibleIncluirCambioComisionMonedaExt = false;
            IsVisibleCambioComisionEnMonedaExt = false;
            TasaDeCambioComisionEnMonedaExt = 1;
            CantidadAImprimir = eCantidadAImprimir.All;
            NombreVendedor = string.Empty;
            CodigoVendedor = string.Empty;
            IsVisibleNombreVendedor = false;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibBusinessSearch GetBusinessComponent(){
            return new clsCobranzaNav();
        }
        private void LlenarTodoArrayDeEnumerativos() {
            LlenarArrayDeEnumerativosTipoDeCalculoComision();
            LlenarArrayDeEnumerativosCalculoParaComisionesEnBaseA();
            LlenarArrayDeEnumerativosMonedaDelInforme();
        }

        private decimal SugerirUltimaTasaDeCambioDeMonedaExt(string valCodigoMonedaExt) {
            DateTime vFechaVigencia = LibDate.Today();
            decimal vUltimoCambio = 1;
            bool vObtenerUltimoUnCambio;
            vObtenerUltimoUnCambio = ((ICambioPdn) new clsCambioNav()).BuscarUltimoCambioDeMoneda(valCodigoMonedaExt, out vFechaVigencia, out vUltimoCambio);
            return vUltimoCambio;
        }

        private bool EsPosibleIncluirComisionEnMonedaExt() {
            bool vResult = false;
            bool vCompaniaUsaMonedaExtranjera = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaMonedaExtranjera"));
            if (vCompaniaUsaMonedaExtranjera) {
                vResult = true;
                string vParametroCodigoMonedaExtranjera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
                Moneda MonedaExtranjera = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<Moneda>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", vParametroCodigoMonedaExtranjera), new clsMonedaNav());
                ComisionMonedaExt = $"Incluir comision en {MonedaExtranjera.Simbolo}";
                CambioComisionEnMonedaExt = $"Tasa Cambio (Comisión a {MonedaExtranjera.Simbolo})";
                TasaDeCambioComisionEnMonedaExt = SugerirUltimaTasaDeCambioDeMonedaExt(vParametroCodigoMonedaExtranjera);
                IsVisibleIncluirCambioComisionMonedaExt = true;
                IsVisibleCambioComisionEnMonedaExt = false;
            } else {
                IncluirComisionEnMonedaExt = false;
            }
            return vResult;
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNombreDelVendedorCommand = new RelayCommand<string>(ExecuteChooseNombreDelVendedorCommand);
        }

        private void ExecuteChooseNombreDelVendedorCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionNombreDelVendedor = null;
                _ConexionNombreDelVendedor = ChooseRecord<FkVendedorViewModel>("Vendedor", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionNombreDelVendedor != null) {
                    NombreVendedor = ConexionNombreDelVendedor.Nombre;
                    CodigoVendedor = ConexionNombreDelVendedor.Codigo;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }

        private ValidationResult FechaInicialValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaFinal, false, eAccionSR.Imprimir)) {
                vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha inicial"));
            }
            return vResult;
        }

        private ValidationResult FechaFinalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (!LibDate.F1IsGreaterOrEqualThanF2(FechaFinal, FechaInicial)) {
                vResult = new ValidationResult("La fecha final debe ser mayor o igual a la fecha inicial:" + FechaInicial.ToShortDateString());
            }
            return vResult;
        }

        private ValidationResult NombreDelVendedorValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(NombreVendedor) && CantidadAImprimir == eCantidadAImprimir.One) {
                vResult = new ValidationResult("El nombre del vendedor no puede estar en blanco");
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsComisionDeVendedoresPorCobranzaViewModel

} //End of namespace Galac.Adm.Uil.Venta

