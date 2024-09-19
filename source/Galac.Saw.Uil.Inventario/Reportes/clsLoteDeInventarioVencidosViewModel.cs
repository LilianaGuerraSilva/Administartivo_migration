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
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Uil.Inventario.ViewModel;
using System.Data;
using LibGalac.Aos.Uil;
using LibGalac.Aos.UI.Mvvm.Validation;
using LibGalac.Aos.DefGen;
using System.ComponentModel.DataAnnotations;
using Galac.Saw.Lib;
namespace Galac.Saw.Uil.Inventario.Reportes {

    public class clsLoteDeInventarioVencidosViewModel : LibInputRptViewModelBase<LoteDeInventario> {
        #region Constantes
        private const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        private const string LineaDeProductoPropertyName = "LineaDeProducto";
        private const string CodigoArticuloPropertyName = "CodigoArticulo";
        private const string IsVisibleArticuloPropertyName = "IsVisibleArticulo";
        private const string IsVisibleLineadeProductoPropertyName = "IsVisibleLineadeProducto";
        private const string OrdenarFechaPropertyName = "OrdenarFecha";

        #endregion //Constantes
        #region Variables
        private FkLineaDeProductoViewModel _ConexionLineaDeProducto = null;
        private FkArticuloInventarioRptViewModel _ConexionArticulo = null;
        private string _CodigoArticulo;
        private string _LineaDeProducto;
        private eCantidadAImprimirArticulo _CantidadAImprimir;
        private eOrdenarFecha _OrdenarFecha;
        #endregion //Constantes
        #region Propiedades

        public override string DisplayName {
            get { return "Artículos Vencidos"; }
        }

        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }

        public override bool IsSSRS { get { return false; } }

        public eCantidadAImprimirArticulo CantidadAImprimir {
            get {
                return _CantidadAImprimir;
            }
            set {
                if (_CantidadAImprimir != value) {
                    _CantidadAImprimir = value;
                    if (_CantidadAImprimir == eCantidadAImprimirArticulo.Articulo) {
                        LineaDeProducto = string.Empty;
                    } else if (_CantidadAImprimir == eCantidadAImprimirArticulo.LineaDeProducto) {
                        CodigoArticulo = string.Empty;
                    } else {
                        LineaDeProducto = string.Empty;
                        CodigoArticulo = string.Empty;
                    }
                    RaisePropertyChanged(CantidadAImprimirPropertyName);
                    RaisePropertyChanged(IsVisibleArticuloPropertyName);
                    RaisePropertyChanged(IsVisibleLineadeProductoPropertyName);
                }
            }
        }

        public eOrdenarFecha OrdenarFecha {
            get {
                return _OrdenarFecha;
            }
            set {
                if (_OrdenarFecha != value) {
                    _OrdenarFecha = value;
                    RaisePropertyChanged(OrdenarFechaPropertyName);
                }
            }
        }

        public int ConsecutivoLineaDeProducto { get; set; }

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

        [LibCustomValidation("ArticuloValidating")]
        public string CodigoArticulo {
            get {
                return _CodigoArticulo;
            }
            set {
                if (_CodigoArticulo != value) {
                    _CodigoArticulo = value;
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoArticulo, true)) {
                        ConexionArticulo = null;
                    }
                }
            }
        }

        public eOrdenarFecha[] ArrayOrdenarFecha {
            get {
                return LibEnumHelper<eOrdenarFecha>.GetValuesInArray();
            }
        }

        public eCantidadAImprimirArticulo[] ArrayCantidadAImprimir {
            get {
                return LibEnumHelper<eCantidadAImprimirArticulo>.GetValuesInArray();
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
                } else {
                    LineaDeProducto = _ConexionLineaDeProducto.Nombre;
                    ConsecutivoLineaDeProducto = _ConexionLineaDeProducto.Consecutivo;
                }
            }
        }

        public FkArticuloInventarioRptViewModel ConexionArticulo {
            get {
                return _ConexionArticulo;
            }
            set {
                if (_ConexionArticulo != value) {
                    _ConexionArticulo = value;
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                }
                if (_ConexionArticulo == null) {
                    CodigoArticulo = string.Empty;
                } else {
                    CodigoArticulo = _ConexionArticulo.Codigo;
                }
            }
        }

        public RelayCommand<string> ChooseLineaDeProductoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoArticuloCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores

        public clsLoteDeInventarioVencidosViewModel() {
            OrdenarFecha = eOrdenarFecha.Descendente;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsLoteDeInventarioNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseLineaDeProductoCommand = new RelayCommand<string>(ExecuteChooseLineaDeProductoCommand);
            ChooseCodigoArticuloCommand = new RelayCommand<string>(ExecuteChooseCodigoArticuloCommand);
        }

        private void ExecuteChooseLineaDeProductoCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionLineaDeProducto = ChooseRecord<FkLineaDeProductoViewModel>("Línea de Producto", vDefaultCriteria, vFixedCriteria, "Nombre");
                if (ConexionLineaDeProducto != null) {
                    LineaDeProducto = ConexionLineaDeProducto.Nombre;
                    ConsecutivoLineaDeProducto = ConexionLineaDeProducto.Consecutivo;
                } else {
                    LineaDeProducto = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }

        private void ExecuteChooseCodigoArticuloCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_ArticuloInventario_B1.Codigo", valCodigo);
                vDefaultCriteria.Add(LibSearchCriteria.CreateCriteria("TipoArticuloInv", eTipoArticuloInv.LoteFechadeVencimiento), eLogicOperatorType.And);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_ArticuloInventario_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionArticulo = ChooseRecord<FkArticuloInventarioRptViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionArticulo != null) {
                    CodigoArticulo = ConexionArticulo.Codigo;
                } else {
                    CodigoArticulo = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }

        public bool IsVisibleArticulo {
            get {
                return CantidadAImprimir == eCantidadAImprimirArticulo.Articulo;
            }
        }

        public bool IsVisibleLineadeProducto {
            get {
                return CantidadAImprimir == eCantidadAImprimirArticulo.LineaDeProducto;
            }
        }    

        private ValidationResult LineaDeProductoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(LineaDeProducto) && CantidadAImprimir == eCantidadAImprimirArticulo.LineaDeProducto) {
                vResult = new ValidationResult("La Línea de producto es requerida.");
            }
            return vResult;
        }

        private ValidationResult ArticuloValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(CodigoArticulo) && CantidadAImprimir == eCantidadAImprimirArticulo.Articulo) {
                vResult = new ValidationResult("El Artículo es requerido.");
            }
            return vResult;
        }
        #endregion //Metodos Generados
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
        
    } //End of class clsLoteDeInventarioVencidosViewModel

} //End of namespace Galac.Saw.Uil.Inventario