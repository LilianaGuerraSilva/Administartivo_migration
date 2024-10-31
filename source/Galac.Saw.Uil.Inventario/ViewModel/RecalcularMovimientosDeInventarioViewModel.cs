using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Brl.Tablas;
using LibGalac.Aos.Uil;
using Galac.Saw.Brl.Inventario;

namespace Galac.Saw.Uil.Inventario.ViewModel {
    public class RecalcularMovimientosDeInventarioViewModel : LibGenericViewModel {
        #region Constantes
        public const string LineaDeProductoPropertyName = "LineaDeProducto";
        public const string CodigoArticuloPropertyName = "CodigoArticulo";

        #endregion
        #region Variables
        private FkArticuloInventarioViewModel _ConexionCodigoArticulo = null;
        private FkLineaDeProductoViewModel _ConexionLineaDeProducto = null;
        private eCantidadAImprimir _ArticuloUnoTodos;
        private string _CodigoArticulo;
        private string _LineaDeProducto;
        private eCantidadAImprimir _LineaDeProductoUnoTodos;
        private RecalcularMovimientosDeInventario Model;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Recalcular Movimientos de Inventario"; }
        }

        public eCantidadAImprimir ArticuloUnoTodos {
            get { return _ArticuloUnoTodos; }
            set {
                if (_ArticuloUnoTodos != value) {
                    _ArticuloUnoTodos = value;
                    RaisePropertyChanged(() => ArticuloUnoTodos);
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                    RaisePropertyChanged(() => IsVisibleCodigoArticulo);
                    RaisePropertyChanged(() => IsVisibleCantidadLineasDeProducto);
                    RaisePropertyChanged(() => IsVisibleLineaDeProducto);
                }
            }
        }
        [LibCustomValidation("ArticuloValidating")]
        [LibRequired(ErrorMessage = "El Artículo es requerido.")]
        public string CodigoArticulo {
            get { return _CodigoArticulo; }
            set {
                if (_CodigoArticulo != value) {
                    _CodigoArticulo = value;
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoArticulo, true)) {
                        ConexionCodigoArticulo = null;
                    }
                }
            }
        }

        public eCantidadAImprimir LineaDeProductoUnoTodos {
            get { return _LineaDeProductoUnoTodos; }
            set {
                if (_LineaDeProductoUnoTodos != value) {
                    _LineaDeProductoUnoTodos = value;
                    RaisePropertyChanged(() => LineaDeProductoUnoTodos);
                    RaisePropertyChanged(LineaDeProductoPropertyName);
                    RaisePropertyChanged(() => IsVisibleLineaDeProducto);
                }
            }
        }
        
        [LibCustomValidation("LineaDeProductoValidating")]
        [LibRequired(ErrorMessage ="Linea de Producto es Requerida")]
        public string LineaDeProducto {
            get { return Model.LineaDeProducto; }
            set {
                if (Model.LineaDeProducto != value) {
                    Model.LineaDeProducto = value;
                    RaisePropertyChanged(LineaDeProductoPropertyName);
                    if (LibString.IsNullOrEmpty(LineaDeProducto, true)) {
                        ConexionLineaDeProducto = null;
                    }
                }
            }
        }

        public eCantidadAImprimir[] ArrayCantidadArticuloAImprimir {
            get { return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray(); }
        }

        public eCantidadAImprimir[] ArrayCantidadLineaDeProductoAImprimir {
            get { return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray(); }
        }

        public FkArticuloInventarioViewModel ConexionCodigoArticulo {
            get { return _ConexionCodigoArticulo; }
            set {
                if (_ConexionCodigoArticulo != value) {
                    _ConexionCodigoArticulo = value;
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                }
                if (_ConexionCodigoArticulo == null) {
                    CodigoArticulo = string.Empty;
                }else {
                    CodigoArticulo = _ConexionCodigoArticulo.Codigo;
                }
            }
        }

        public FkLineaDeProductoViewModel ConexionLineaDeProducto {
            get { return _ConexionLineaDeProducto; }
            set {
                if (_ConexionLineaDeProducto != value) {
                    _ConexionLineaDeProducto = value;
                    RaisePropertyChanged(LineaDeProductoPropertyName);
                }
                if (_ConexionLineaDeProducto == null) {
                    LineaDeProducto = string.Empty;
                } else {
                    LineaDeProducto = _ConexionLineaDeProducto.Nombre;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoArticuloCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseLineaDeProductoCommand {
            get;
            private set;
        }

        public RelayCommand RecalcularCommand {
            get;
            private set;
        }

        public bool IsVisibleCodigoArticulo {
            get { return (ArticuloUnoTodos == eCantidadAImprimir.One); }
        }

        public bool IsVisibleCantidadLineasDeProducto {
            get { return (ArticuloUnoTodos == eCantidadAImprimir.All); }
        }

        public bool IsVisibleLineaDeProducto { 
            get { return IsVisibleCantidadLineasDeProducto && (LineaDeProductoUnoTodos == eCantidadAImprimir.One);} 
        }
        #endregion //Propiedades
        #region Constructores
        public RecalcularMovimientosDeInventarioViewModel() 
            :base() {
            Model = new RecalcularMovimientosDeInventario();
            ArticuloUnoTodos = eCantidadAImprimir.All;
            LineaDeProductoUnoTodos = eCantidadAImprimir.All;
            CodigoArticulo = string.Empty;
            LineaDeProducto = string.Empty;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoArticuloCommand = new RelayCommand<string>(ExecuteChooseCodigoArticuloCommand);
            ChooseLineaDeProductoCommand = new RelayCommand<string>(ExecuteChooseLineaDeProductoCommand);
            RecalcularCommand = new RelayCommand(ExecuteRecalcularCommand, CanExecuteRecalcularCommand);
        }

        private LibRibbonButtonData CreateExecuteActionRibbonButtonData() {
            LibRibbonButtonData vButton = new LibRibbonButtonData() {
                Label = "Recalcular",
                Command = RecalcularCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/refresh.png", UriKind.Relative),
                ToolTipDescription = "Recalcular",
                ToolTipTitle = "Recalcular",
                IsVisible = true
            };
            return vButton;
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.RemoveRibbonControl("Administrar", "Insertar");
                RibbonData.RemoveRibbonControl("Administrar", "Consultar");
                RibbonData.RemoveRibbonControl("Administrar", "Eliminar");
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection.Insert(0, CreateExecuteActionRibbonButtonData());
            }
        }

        private bool CanExecuteRecalcularCommand() {
            return true;
        }

        private void ExecuteRecalcularCommand() {
            try {
                ILoteDeInventarioPdn vPdn = new clsLoteDeInventarioNav();
                if (vPdn.RecalcularMovimientosDeLoteDeInventario(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), ArticuloUnoTodos, CodigoArticulo, LineaDeProductoUnoTodos, LineaDeProducto)){
                    LibMessages.MessageBox.Information(this, "Se culminó con éxito la actualización de Lotes de Inventario.", ModuleName);
                }
                DialogResult = true;
                RaiseRequestCloseEvent();
            } catch (System.AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteChooseCodigoArticuloCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_ArticuloInventario_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_ArticuloInventario_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionCodigoArticulo = LibFKRetrievalHelper.ChooseRecord<FkArticuloInventarioViewModel>("Articulo Inventario", vDefaultCriteria, vFixedCriteria, new clsArticuloInventarioNav(), string.Empty);
                if (ConexionCodigoArticulo != null) {
                    CodigoArticulo = ConexionCodigoArticulo.Codigo;
                } else {
                    CodigoArticulo = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseLineaDeProductoCommand(string valLineaDeProducto) {
            try {
                if (valLineaDeProducto == null) {
                    valLineaDeProducto = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_LineaDeProducto_B1.Nombre", valLineaDeProducto);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_LineaDeProducto_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionLineaDeProducto = LibFKRetrievalHelper.ChooseRecord<FkLineaDeProductoViewModel>("Línea de Producto", vDefaultCriteria, vFixedCriteria, new clsLineaDeProductoNav(), string.Empty);
                if (ConexionLineaDeProducto != null) {
                    LineaDeProducto = ConexionLineaDeProducto.Nombre;
                } else {
                    LineaDeProducto = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult ArticuloValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(CodigoArticulo) && ArticuloUnoTodos == eCantidadAImprimir.One) {
                vResult = new ValidationResult("El Artículo es requerido.");
            }
            return vResult;
        }

        private ValidationResult LineaDeProductoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(LineaDeProducto) && LineaDeProductoUnoTodos == eCantidadAImprimir.One) {
                vResult = new ValidationResult("La Línea de producto es requerida.");
            }
            return vResult;
        }
        #endregion //Metodos Generados

    } //End of class RecalcularMovimientosDeInventarioViewModel
} //End of namespace Galac..Uil.ComponenteNoEspecificado