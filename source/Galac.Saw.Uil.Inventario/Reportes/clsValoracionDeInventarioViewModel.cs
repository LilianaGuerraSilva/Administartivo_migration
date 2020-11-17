using System.Collections.Generic;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Lib;
using LibGalac.Aos.DefGen;
using Galac.Comun.Ccl.TablasGen;
using Galac.Saw.Uil.Inventario.ViewModel;
using LibGalac.Aos.UI.Mvvm.Validation;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using LibGalac.Aos.UI.Mvvm.Helpers;

namespace Galac.Saw.Uil.Inventario.Reportes {

    public class clsValoracionDeInventarioViewModel : LibInputRptViewModelBase<ArticuloInventario> {
        #region Constantes
        public const string CodigoDesdePropertyName = "CodigoDesde";
        public const string CodigoHastaPropertyName = "CodigoHasta";
        public const string TipoDeInformePropertyName = "TipoDeInforme";
        public const string LineaDeProductoPropertyName = "LineaDeProducto";
        public const string TipoDeMonedaDelReportePropertyName = "TipoDeMonedaDelReporte";
        public const string TasaDeCambioPropertyName = "TasaDeCambio";
        public const string MostrarPreciosPropertyName = "MostrarPrecios";
        public const string MonedaPropertyName = "Moneda";
        public const string IsVisibleLineaDeProductoPropertyName = "IsVisibleLineaDeProducto";
        public const string IsVisibleParaDivisasPropertyName = "IsVisibleParaDivisas";      
        #endregion
        #region Variables
        private FkArticuloInventarioRptViewModel _ConexionCodigoDesde = null;
        private FkArticuloInventarioRptViewModel _ConexionCodigoHasta = null;
        private FkLineaDeProductoViewModel _ConexionLineaDeProducto = null;
        private FkMonedaViewModel _ConexionMoneda = null;
        private string _CodigoDesde = "";
        private string _CodigoHasta = "";
        private string _LineaDeProducto = "";
        decimal _TasaDeCambio = 0;
        private eMonedaPresentacionDeReporte _TipoDeMonedaDelReporteAsEnum;
        eListarPorLineaDeProducto _TipoDeInforme;
	    ePrecioDeLosArticulos _PrecioDeLosArticulos;
        string _Moneda = "";
        string _CodigoMoneda = "";

        #endregion //Variables
        #region Propiedades

        public override string DisplayName {
            get { return "Valoración De Inventario";}
        }

        [LibCustomValidation("CodigoDesdeValidating")]        
        public string CodigoDesde {
            get {
                return _CodigoDesde;
            }
            set {
                if (_CodigoDesde != value) {
                    _CodigoDesde = value;                  
                    RaisePropertyChanged(CodigoDesdePropertyName);
                    if (LibString.IsNullOrEmpty(CodigoDesde, true)) {
                        ConexionCodigoDesde = null;
                    }
                }
            }
        }

        [LibCustomValidation("CodigoHastaValidating")]        
        public string CodigoHasta {
            get {
                return _CodigoHasta;
            }
            set {
                if (_CodigoHasta != value) {
                    _CodigoHasta = value;                  
                    RaisePropertyChanged(CodigoHastaPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoHasta, true)) {
                        ConexionCodigoHasta = null;
                    }
                }
            }
        }

        [LibCustomValidation("LineaDeProductoValidating")]
        public string LineaDeProducto {
            get {
                return _LineaDeProducto;
            }
            set {
                if (_LineaDeProducto != value) {
                    _LineaDeProducto = value;                 
                    RaisePropertyChanged(LineaDeProductoPropertyName);
                    if (LibString.IsNullOrEmpty(LineaDeProducto, true)) {
                        ConexionLineaDeProducto = null;
                    }
                }
            }
        }        
       
        [LibCustomValidation("MonedaValidating")]
        public string  Moneda {
            get {
                return _Moneda;
            }
            set {
                if (_Moneda != value) {
                    _Moneda = value;                  
                    RaisePropertyChanged(MonedaPropertyName);
                    if (LibString.IsNullOrEmpty(Moneda, true)) {
                        ConexionMoneda = null;
                    }
                }
            }
        }

        public string CodigoMoneda {
            get {
                return _CodigoMoneda;
            }
            set {
                if(_CodigoMoneda != value) {
                    _CodigoMoneda = value;                 
                }
            }
        }
		
        [LibCustomValidation("TasaDeCambioValidating")]
        public decimal  TasaDeCambio {
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

        public eListarPorLineaDeProducto TipoDeInforme {
            get {
                return _TipoDeInforme;
            }
            set {
                if(_TipoDeInforme != value) {
                    _TipoDeInforme = value;
                    LineaDeProducto = string.Empty;                    
                    RaisePropertyChanged(LineaDeProductoPropertyName);
                    RaisePropertyChanged(IsVisibleLineaDeProductoPropertyName);
                }
            }
        }

        public eListarPorLineaDeProducto[] ArrayTipoDeInforme {
            get {
                return LibEnumHelper<eListarPorLineaDeProducto>.GetValuesInArray();
            }
        }

        public eMonedaPresentacionDeReporte TipoDeMonedaDelReporte {
            get {
                return _TipoDeMonedaDelReporteAsEnum;
            }
            set {
                if(_TipoDeMonedaDelReporteAsEnum != value) {
                    _TipoDeMonedaDelReporteAsEnum = value;
                    RaisePropertyChanged(TipoDeMonedaDelReportePropertyName);
                    RaisePropertyChanged(IsVisibleParaDivisasPropertyName);
                }
            }
        }
		
        public eMonedaPresentacionDeReporte[] ArrayTipoDeMonedaDelReporte {
            get {
                return LibEnumHelper<eMonedaPresentacionDeReporte>.GetValuesInArray();
            }
        }

        public bool UsaPrecioConIvaAsBool {
            get {
                return (PrecioDeLosArticulos == ePrecioDeLosArticulos.PrecioConIva);
            }
        }
		
		public ePrecioDeLosArticulos  PrecioDeLosArticulos {
            get {
                return _PrecioDeLosArticulos;
            }
            set {
                if (_PrecioDeLosArticulos != value) {
                    _PrecioDeLosArticulos = value;                 
                    RaisePropertyChanged(MostrarPreciosPropertyName);
                }
            }
        }		
		
        public ePrecioDeLosArticulos[] ArrayPreciosDeLosProductos {
            get {
                return LibEnumHelper<ePrecioDeLosArticulos>.GetValuesInArray();
            }
        }
		
        public FkArticuloInventarioRptViewModel ConexionCodigoDesde {
            get {
                return _ConexionCodigoDesde;
            }
            set {
                if (_ConexionCodigoDesde != value) {
                    _ConexionCodigoDesde = value;
                    RaisePropertyChanged(CodigoDesdePropertyName);
                }
                if (_ConexionCodigoDesde == null) {
                    CodigoDesde = string.Empty;
                }
            }
        }

        public FkArticuloInventarioRptViewModel ConexionCodigoHasta {
            get {
                return _ConexionCodigoHasta;
            }
            set {
                if (_ConexionCodigoHasta != value) {
                    _ConexionCodigoHasta = value;
                    RaisePropertyChanged(CodigoHastaPropertyName);
                }
                if (_ConexionCodigoHasta == null) {
                    CodigoHasta = string.Empty;
                }
            }
        }

        public FkLineaDeProductoViewModel ConexionLineaDeProducto {
            get {
                return _ConexionLineaDeProducto;
            }
            set {
                if (_ConexionLineaDeProducto != value) {
                    _ConexionLineaDeProducto = value;
                    RaisePropertyChanged(LineaDeProductoPropertyName);
                }
                if (_ConexionLineaDeProducto == null) {
                    LineaDeProducto = string.Empty;
                }
            }
        }

        public FkMonedaViewModel ConexionMoneda {
            get {
                return _ConexionMoneda;
            }
            set {
                if (_ConexionMoneda != value) {
                    _ConexionMoneda = value;
                    RaisePropertyChanged(MonedaPropertyName);
                }
                if (_ConexionMoneda == null) {
                    Moneda = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoDesdeCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoHastaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseLineaDeProductoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseMonedaCommand {
            get;
            private set;
        }
		
        public override bool IsSSRS {
            get {
                return false;
            }
        }
        #endregion //Propiedades
        #region Constructores

        public clsValoracionDeInventarioViewModel() {
            TipoDeInforme = eListarPorLineaDeProducto.Todos;
            TipoDeMonedaDelReporte = eMonedaPresentacionDeReporte.EnMonedaLocal;
            TasaDeCambio = 1m;
            PrecioDeLosArticulos = ePrecioDeLosArticulos.PrecioConIva;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsArticuloInventarioNav();
        }        
       
		
        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoDesdeCommand = new RelayCommand<string>(ExecuteChooseCodigoDesdeCommand);
            ChooseCodigoHastaCommand = new RelayCommand<string>(ExecuteChooseCodigoHastaCommand);
            ChooseLineaDeProductoCommand = new RelayCommand<string>(ExecuteChooseLineaDeProductoCommand);
            ChooseMonedaCommand = new RelayCommand<string>(ExecuteChooseMonedaCommand);
        }

        private void ExecuteChooseCodigoDesdeCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("dbo.Gv_ArticuloInventario_B1.Codigo",valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("dbo.Gv_ArticuloInventario_B1.ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionCodigoDesde = ChooseRecord<FkArticuloInventarioRptViewModel>("Articulo Inventario",vDefaultCriteria,vFixedCriteria,string.Empty);
                if (ConexionCodigoDesde != null) {
                    CodigoDesde = ConexionCodigoDesde.Codigo;                    
                } else {
                    CodigoDesde = string.Empty;                    
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,DisplayName);
            }
        }

        private void ExecuteChooseCodigoHastaCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("dbo.Gv_ArticuloInventario_B1.Codigo",valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("dbo.Gv_ArticuloInventario_B1.ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionCodigoHasta = ChooseRecord<FkArticuloInventarioRptViewModel>("Articulo Inventario",vDefaultCriteria,vFixedCriteria,string.Empty);
                if (ConexionCodigoHasta != null) {                    
                    CodigoHasta = ConexionCodigoHasta.Codigo;
                } else {                    
                    CodigoHasta = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }

        private void ExecuteChooseLineaDeProductoCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionLineaDeProducto = ChooseRecord<FkLineaDeProductoViewModel>("Línea de Producto",vDefaultCriteria,vFixedCriteria,"Nombre");
                if (ConexionLineaDeProducto != null) {
                    LineaDeProducto = ConexionLineaDeProducto.Nombre;
                } else {
                    LineaDeProducto = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }

        private void ExecuteChooseMonedaCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Activa",LibConvert.BoolToSN(true));
                vFixedCriteria.Add("TipoDeMoneda",eBooleanOperatorType.IdentityEquality,eTipoDeMoneda.Fisica);
                AgregarCriteriaParaExcluirMonedasLocalesNoVigentesAlDiaActual(ref vFixedCriteria);
                ConexionMoneda = ChooseRecord<FkMonedaViewModel>("Moneda", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionMoneda != null) {
                    Moneda = ConexionMoneda.Nombre;
                    CodigoMoneda = ConexionMoneda.Codigo;
                    BuscarMonedaYTasaDeCambioSegunMoneda(CodigoMoneda);
                } else {
                    Moneda = string.Empty;
                    CodigoMoneda = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }

        private void AgregarCriteriaParaExcluirMonedasLocalesNoVigentesAlDiaActual(ref LibSearchCriteria vFixedCriteria) {
            XElement vXmlMonedaLocales = ((Comun.Ccl.TablasGen.IMonedaLocalPdn)new Comun.Brl.TablasGen.clsMonedaLocalProcesos()).BusquedaTodasLasMonedasLocales(LibDefGen.ProgramInfo.Country);
            IList<Comun.Ccl.TablasGen.MonedaLocalActual> vListaDeMonedaLocales = new List<Comun.Ccl.TablasGen.MonedaLocalActual>();
            vListaDeMonedaLocales = vXmlMonedaLocales != null ? LibParserHelper.ParseToList<Comun.Ccl.TablasGen.MonedaLocalActual>(new XDocument(vXmlMonedaLocales)) : null;
            if(vListaDeMonedaLocales != null) {
                foreach(Comun.Ccl.TablasGen.MonedaLocalActual vMoneda in vListaDeMonedaLocales) {
                    vFixedCriteria.Add("Codigo",eBooleanOperatorType.IdentityInequality,vMoneda.CodigoMoneda);
                }
            }
        }

        public void BuscarMonedaYTasaDeCambioSegunMoneda(string valMoneda) {
            decimal vTasa = 1;
            FkMonedaViewModel vConexionMoneda = null;
            try {
                vConexionMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda",LibSearchCriteria.CreateCriteriaFromText("Codigo",valMoneda));
                if(vConexionMoneda != null) {
                    CodigoMoneda = vConexionMoneda.Codigo;
                    Moneda = vConexionMoneda.Nombre;
                    if(((ICambioPdn)new Comun.Brl.TablasGen.clsCambioNav()).ExisteTasaDeCambioParaElDia(valMoneda,LibDate.Today(),out vTasa)) {
                        TasaDeCambio = vTasa;
                    } else {
                        TasaDeCambio = 0;
                    }
                }
            } catch(System.Exception) {
                throw;
            }
        }

        public bool IsVisibleLineaDeProducto {
            get {
                bool vResult = false;
                vResult = (TipoDeInforme == eListarPorLineaDeProducto.LineaDeProducto);
                return vResult;
            }
        }

        public bool IsVisibleParaDivisas {
            get {
                bool vResult = false;
                string vMoneda = string.Empty;
                vResult = (_TipoDeMonedaDelReporteAsEnum != eMonedaPresentacionDeReporte.EnMonedaLocal);
                if(vResult) {
                    vMoneda = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","CodigoMonedaExtranjera");
                    BuscarMonedaYTasaDeCambioSegunMoneda(vMoneda);
                } else {
                    clsNoComunSaw vclsNoComunSaw = new clsNoComunSaw();
                    TasaDeCambio = 1m;
                    Moneda = vclsNoComunSaw.InstanceMonedaLocalActual.NombreMoneda(LibDate.Today());
                    CodigoMoneda = vclsNoComunSaw.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today());
                }
                return vResult;
            }
        }

        private ValidationResult LineaDeProductoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if(TipoDeInforme == eListarPorLineaDeProducto.LineaDeProducto && LibString.IsNullOrEmpty(LineaDeProducto)) {
                vResult = new ValidationResult("La linea de producto es requerida.");
            } else {
                return ValidationResult.Success;
            }
            return vResult;
        }


        private ValidationResult TasaDeCambioValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if(_TipoDeMonedaDelReporteAsEnum != eMonedaPresentacionDeReporte.EnMonedaLocal && TasaDeCambio == 0) {
                vResult = new ValidationResult("La tasa de cambio es requerida, podrá colocarla de forma manual");
            } else {
                return ValidationResult.Success;
            }
            return vResult;
        }

        private ValidationResult MonedaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if(_TipoDeMonedaDelReporteAsEnum != eMonedaPresentacionDeReporte.EnMonedaLocal && LibString.IsNullOrEmpty(Moneda)) {
                vResult = new ValidationResult("La moneda es requerida");
            } else {
                return ValidationResult.Success;
            }
            return vResult;
        }

        private ValidationResult CodigoDesdeValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if(LibString.IsNullOrEmpty(CodigoDesde)) {
                vResult = new ValidationResult("El código desde es requerido");
            } else {
                return ValidationResult.Success;
            }
            return vResult;
        }

        private ValidationResult CodigoHastaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if(LibString.IsNullOrEmpty(CodigoHasta)) {
                vResult = new ValidationResult("El código hasta es requerido");
            } else {
                return ValidationResult.Success;
            }
            return vResult;
        }
        #endregion Metodos Generados
    } //End of class clsValoracionDeInventarioViewModel
} //End of namespace Galac.Saw.Uil.Inventario

