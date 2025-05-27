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
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Brl.Tablas;
using LibGalac.Aos.Uil;

namespace Galac.Saw.Uil.Inventario.ViewModel {
    public class ArticuloInventarioRecalcularExistenciaViewModel : LibGenericViewModel {
        #region Constantes
        public const string CantidadLineaDeProductoPropertyName = "CantidadLineaDeProducto";
        public const string LineaDeProductoPropertyName = "LineaDeProducto";
        public const string CantidadArtInvPropertyName = "CantidadArtInv";
        public const string CodigoArticuloPropertyName = "CodigoArticulo";
        public const string DescripcionArticuloPropertyName = "DescripcionArticulo";
        #endregion
        #region Variables
        private FkLineaDeProductoViewModel _ConexionLineaDeProducto = null;
        private FkArticuloInventarioViewModel _ConexionCodigoArticulo = null;
        private eCantidadAImprimir _CantidadLineaDeProducto;
        private eCantidadAImprimir _CantidadArtInv;
        private string _LineaDeProducto;
        private string _CodigoArticulo;
        private string _DescripcionArticulo;
        private ArticuloInventarioRecalcularExistencia Model;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Recalcular Existencia"; }
        }

        public eCantidadAImprimir  CantidadLineaDeProducto {
            get {
                return _CantidadLineaDeProducto;
            }
            set {
                if (_CantidadLineaDeProducto != value) {
                    _CantidadLineaDeProducto = value;
                    RaisePropertyChanged(() =>  CantidadLineaDeProducto);
                    RaisePropertyChanged(CantidadLineaDeProductoPropertyName);
                    RaisePropertyChanged(() => IsEnabledLineaDeProducto);
                    if (CantidadLineaDeProducto == eCantidadAImprimir.All) {
                        LineaDeProducto = string.Empty;
                        CantidadLineaDeProducto = eCantidadAImprimir.All;
                    }
                }
            }
        }

        [LibCustomValidation("LineaDeProductoValidating")]
        //[LibRequired(ErrorMessage = "El campo Linea de Producto es requerido.")]
        public string  LineaDeProducto {
            get {
                return _LineaDeProducto;
            }
            set {
                if (_LineaDeProducto != value) {
                    _LineaDeProducto = value;
                    RaisePropertyChanged(LineaDeProductoPropertyName);
                    if (LibString.IsNullOrEmpty(LineaDeProducto, true)) {
                        ConexionLineaDeProducto = null;
                    } else {
                        LineaDeProducto = _ConexionLineaDeProducto.Nombre;
                    }
                }
            }
        }

        public eCantidadAImprimir  CantidadArtInv {
            get {
                return _CantidadArtInv;
            }
            set {
                if (_CantidadArtInv != value) {
                    _CantidadArtInv = value;
                    RaisePropertyChanged(CantidadArtInvPropertyName);
                    RaisePropertyChanged(DescripcionArticuloPropertyName);
                    RaisePropertyChanged(() => IsEnabledCodigoArticulo);
                    if (CantidadArtInv == eCantidadAImprimir.All) {
                        CodigoArticulo = string.Empty;
                        DescripcionArticulo = string.Empty;
                        CantidadArtInv = eCantidadAImprimir.All;
                    }
                }
            }
        }

        
        [LibCustomValidation("ArticuloValidating")]
        //[LibRequired(ErrorMessage = "El campo Código es requerido.")]
        public string  CodigoArticulo {
            get {
                return _CodigoArticulo;
            }
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

        public string  DescripcionArticulo {
            get {
                return _DescripcionArticulo;
            }
            set {
                if (_DescripcionArticulo != value) {
                    _DescripcionArticulo = value;                   
                    RaisePropertyChanged(DescripcionArticuloPropertyName);
                }
            }
        }

        public eCantidadAImprimir[] ArrayCantidadAImprimir {
            get {
                return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray();
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

        public FkArticuloInventarioViewModel ConexionCodigoArticulo {
            get {
                return _ConexionCodigoArticulo;
            }
            set {
                if (_ConexionCodigoArticulo != value) {
                    _ConexionCodigoArticulo = value;
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                    RaisePropertyChanged(DescripcionArticuloPropertyName);
                }
                if (_ConexionCodigoArticulo == null) {
                    CodigoArticulo = string.Empty;
                    DescripcionArticulo = string.Empty;
                } else {
                    CodigoArticulo = _ConexionCodigoArticulo.Codigo;
                    DescripcionArticulo = _ConexionCodigoArticulo.Descripcion;
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

        public RelayCommand RecalcularCommand {
            get;
            private set;
        }

        public bool IsEnabledLineaDeProducto {
            get { return CantidadLineaDeProducto == eCantidadAImprimir.One; }
        }

        public bool IsEnabledCodigoArticulo {
            get { return CantidadArtInv == eCantidadAImprimir.One; }
        }
        #endregion //Propiedades
        #region Constructores
        public ArticuloInventarioRecalcularExistenciaViewModel()
            : base() {
            Model = new ArticuloInventarioRecalcularExistencia();
            CantidadLineaDeProducto = eCantidadAImprimir.All;
            CantidadArtInv = eCantidadAImprimir.All;
            LineaDeProducto = string.Empty;
            CodigoArticulo = string.Empty;
            DescripcionArticulo = string.Empty;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoArticuloCommand = new RelayCommand<string>(ExecuteChooseCodigoArticuloCommand);
            ChooseLineaDeProductoCommand = new RelayCommand<string>(ExecuteChooseLineaDeProductoCommand);
            RecalcularCommand = new RelayCommand(ExecuteRecalcularCommand, CanExecuteRecalcularCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.RemoveRibbonControl("Administrar", "Insertar");
                RibbonData.RemoveRibbonControl("Administrar", "Consultar");
                RibbonData.RemoveRibbonControl("Administrar", "Eliminar");
                RibbonData.RemoveRibbonControl("Administrar", "Modificar");
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection.Insert(0, CreateExecuteActionRibbonButtonData());
            }
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

        private bool CanExecuteRecalcularCommand() {
            return true;
        }

        private void ExecuteRecalcularCommand() {
            try {
                IArticuloInventarioPdn vArticuloInventario = new clsArticuloInventarioNav();
                if(vArticuloInventario.RecalcularExistencia(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), CantidadArtInv, CodigoArticulo, CantidadLineaDeProducto, LineaDeProducto)){
                    LibMessages.MessageBox.Information(this, "Se culminó con éxito la actualización de Artículos Inventario.", ModuleName);
                }
                DialogResult = true;
                RaiseRequestCloseEvent();
            } catch (System.AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteChooseLineaDeProductoCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
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

        private void ExecuteChooseCodigoArticuloCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_ArticuloInventario_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_ArticuloInventario_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                vFixedCriteria.Add("Gv_ArticuloInventario_B1.TipoDeArticulo", eTipoDeArticulo.Mercancia);
                if (CantidadLineaDeProducto == eCantidadAImprimir.One) {
                    vFixedCriteria.Add("Gv_ArticuloInventario_B1.LineaDeProducto", LineaDeProducto);
                }
                ConexionCodigoArticulo = LibFKRetrievalHelper.ChooseRecord<FkArticuloInventarioViewModel>("Articulo Inventario", vDefaultCriteria, vFixedCriteria, new clsArticuloInventarioNav(), string.Empty);
                if (ConexionCodigoArticulo != null) {
                    CodigoArticulo = ConexionCodigoArticulo.Codigo;
                    DescripcionArticulo = ConexionCodigoArticulo.Descripcion;
                } else {
                    CodigoArticulo = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult ArticuloValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(CodigoArticulo) && CantidadArtInv == eCantidadAImprimir.One) {
                vResult = new ValidationResult("El Artículo es requerido.");
            }
            return vResult;
        }

        private ValidationResult LineaDeProductoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(LineaDeProducto) && CantidadLineaDeProducto == eCantidadAImprimir.One) {
                vResult = new ValidationResult("La Línea de producto es requerida.");
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class ArticuloInventarioRecalcularExistenciaViewModel

} //End of namespace Galac.Saw.Uil.Inventario

