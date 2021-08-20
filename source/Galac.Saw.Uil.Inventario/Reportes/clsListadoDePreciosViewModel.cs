using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Uil.Inventario.ViewModel;
using Galac.Saw.Brl.Tablas;
using Galac.Saw.Reconv;
using Galac.Saw.Lib;
using Galac.Comun.Ccl.TablasGen;
using System.Xml.Linq;
using LibGalac.Aos.DefGen;

namespace Galac.Saw.Uil.Inventario.Reportes {

    public class clsListadoDePreciosViewModel : LibInputRptViewModelBase<ArticuloInventario> {
        #region Constantes
        public const string NombreLineaDeProductoPropertyName = "NombreLineaDeProducto";
        public const string MonedaDelReporteEnMonedaLocalPropertyName = "MonedaDelReporteEnMonedaLocal";
        public const string MonedaDelReporteDesdeMonedaExtranjeraPropertyName = "MonedaDelReporteDesdeMonedaExtranjera";
        public const string EtiquetaMonedaTasaPropertyName = "EtiquetaMonedaTasa";

        #endregion
        #region Variables
        private FkLineaDeProductoViewModel _ConexionNombreLineaDeProducto = null;
        string _NombreLineaDeProducto;
        decimal _TasaDeCambio = 1;
        bool _MonedaDelReporteEnMonedaLocal = true;
        bool _MonedaDelReporteDesdeMonedaExtranjera = false;
        string _Moneda = "";
        string _CodigoMoneda = "";
        string _EtiquetaMonedaTasa = string.Empty;
        #endregion //Variables
        #region Propiedades
        public override string DisplayName {
            get {
                string vMensaje = "";
                if (LibDate.Today() >= clsUtilReconv.GetFechaReconversion()) {
                    vMensaje = "Listado de Precios Bolívar Soberano";
                } else if (LibDate.Today() >= clsUtilReconv.GetFechaDisposicionesTransitorias()) {
                    vMensaje = "Listado de Precios Bolívar Digital";
                }
                return vMensaje;
            }
        }

        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }

        public string NombreLineaDeProducto {
            get {
                return _NombreLineaDeProducto;
            }
            set {
                if (_NombreLineaDeProducto != value) {
                    _NombreLineaDeProducto = value;
                    RaisePropertyChanged(NombreLineaDeProductoPropertyName);
                    if (LibString.IsNullOrEmpty(NombreLineaDeProducto, true)) {
                        ConexionNombreLineaDeProducto = null;
                    }
                }
            }
        }

        public FkLineaDeProductoViewModel ConexionNombreLineaDeProducto {
            get {
                return _ConexionNombreLineaDeProducto;
            }
            set {
                if (_ConexionNombreLineaDeProducto != value) {
                    _ConexionNombreLineaDeProducto = value;
                    RaisePropertyChanged(NombreLineaDeProductoPropertyName);
                }
                if (_ConexionNombreLineaDeProducto == null) {
                    NombreLineaDeProducto = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseNombreLineaDeProductoCommand {
            get;
            private set;
        }

        public override bool IsSSRS {
            get {
                return false;
            }
        }

        public decimal TasaDeCambio {
            get {
                return _TasaDeCambio;
            }
        }

        public bool MonedaDelReporteEnMonedaLocal {
            get {
                return _MonedaDelReporteEnMonedaLocal;
            }
            set {
                if (_MonedaDelReporteEnMonedaLocal != value) {
                    _MonedaDelReporteEnMonedaLocal = value;
                    RaisePropertyChanged(MonedaDelReporteEnMonedaLocalPropertyName);
                }
            }
        }

        public bool MonedaDelReporteDesdeMonedaExtranjera {
            get { return _MonedaDelReporteDesdeMonedaExtranjera; }
            set {
                if (_MonedaDelReporteDesdeMonedaExtranjera != value) {
                    _MonedaDelReporteDesdeMonedaExtranjera = value;
                    ActualizaEtiquetaMonedaTasa();
                    RaisePropertyChanged(MonedaDelReporteDesdeMonedaExtranjeraPropertyName);
                }
            }
        }

        public eMonedaPresentacionDeReporteRM TipoDeMonedaDelReporte {
            get { return (MonedaDelReporteDesdeMonedaExtranjera ? eMonedaPresentacionDeReporteRM.EnMonedaExtranjera : eMonedaPresentacionDeReporteRM.EnBolivares); }
        }

        public string EtiquetaMonedaTasa {
            get { return _EtiquetaMonedaTasa; }
            set {
                if (_EtiquetaMonedaTasa != value) {
                    _EtiquetaMonedaTasa = value;
                    RaisePropertyChanged(EtiquetaMonedaTasaPropertyName);
                }
            }
        }

        public bool IsVisibleME {
            get { return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMonedaExtranjera"); }
        }

        #endregion //Propiedades
        #region Constructores
        public clsListadoDePreciosViewModel() {
            //TipoDeMonedaDelReporte = eMonedaPresentacionDeReporteRM.EnBolivares;
        }
        #endregion //Constructores
        #region Metodos Generados
        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsLineaDeProductoNav();
        }

        //public eMonedaPresentacionDeReporteRM[] ArrayTipoDeMonedaDelReporte() {
        //    return LibEnumHelper<eMonedaPresentacionDeReporteRM>.GetValuesInArray();
        //}
        #endregion //Metodos Generados
        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNombreLineaDeProductoCommand = new RelayCommand<string>(ExecuteChooseNombreLineaDeProductoCommand);
        }
        private void ExecuteChooseNombreLineaDeProductoCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionNombreLineaDeProducto = ChooseRecord<FkLineaDeProductoViewModel>("Línea de Producto", vDefaultCriteria, vFixedCriteria, "Nombre");
                if (ConexionNombreLineaDeProducto != null) {
                    NombreLineaDeProducto = ConexionNombreLineaDeProducto.Nombre;
                } else {
                    NombreLineaDeProducto = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }

        private void ActualizaEtiquetaMonedaTasa() {
            try {
                decimal vTasa = 1;
                bool vUsaMonedaExtranjeta = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMonedaExtranjera");
                if (vUsaMonedaExtranjeta) {
                    _CodigoMoneda = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
                    _Moneda = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreMonedaExtranjera");
                    DateTime vFecha = LibDate.Today();
                    if (((ICambioPdn)new Comun.Brl.TablasGen.clsCambioNav()).BuscarUltimoCambioDeMoneda(_CodigoMoneda, out vFecha, out vTasa)) {
                        _TasaDeCambio = vTasa;
                    } else {
                        _TasaDeCambio = 1;
                    }
                    EtiquetaMonedaTasa = "Se usará la tasa de cambio a " + _Moneda + " más reciente: " + LibConvert.NumToString(_TasaDeCambio, 2);
                }
            } catch (System.Exception) {
                throw;
            }
        }
    } //End of class clsListadoDePreciosViewModel
} //End of namespace Galac.Saw.Uil.Inventario
