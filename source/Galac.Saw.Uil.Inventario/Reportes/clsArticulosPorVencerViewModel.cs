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

namespace Galac.Saw.Uil.Inventario.Reportes {

    public class clsArticulosPorVencerViewModel : LibInputRptViewModelBase<LoteDeInventario> {
        #region Constantes
        public const string DiasParaVencersePropertyName = "DiasParaVencerse";
        public const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        public const string LineaDeProductoPropertyName = "LineaDeProducto";
        public const string ArticuloPropertyName = "Articulo";
        #endregion
        #region Variables
        private FkLineaDeProductoViewModel _ConexionLineaDeProducto = null;
        private FkArticuloInventarioViewModel _ConexionArticulo = null;
        #endregion //Variables
        #region Propiedades

        public override string DisplayName {
            get { return "Articulos Por Vencer"; }
        }

        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }
		
        public int  DiasParaVencerse {
            get {
                return Model.DiasParaVencerse;
            }
            set {
                if (Model.DiasParaVencerse != value) {
                    Model.DiasParaVencerse = value;
                    IsDirty = true;
                    RaisePropertyChanged(DiasParaVencersePropertyName);
                }
            }
        }

        public eCantidadAImprimir  CantidadAImprimir {
            get {
                return Model.CantidadAImprimirAsEnum;
            }
            set {
                if (Model.CantidadAImprimirAsEnum != value) {
                    Model.CantidadAImprimirAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadAImprimirPropertyName);
                }
            }
        }

        public string  LineaDeProducto {
            get {
                return Model.LineaDeProducto;
            }
            set {
                if (Model.LineaDeProducto != value) {
                    Model.LineaDeProducto = value;
                    IsDirty = true;
                    RaisePropertyChanged(LineaDeProductoPropertyName);
                    if (LibString.IsNullOrEmpty(LineaDeProducto, true)) {
                        ConexionLineaDeProducto = null;
                    }
                }
            }
        }

        public string  Articulo {
            get {
                return Model.Articulo;
            }
            set {
                if (Model.Articulo != value) {
                    Model.Articulo = value;
                    IsDirty = true;
                    RaisePropertyChanged(ArticuloPropertyName);
                    if (LibString.IsNullOrEmpty(Articulo, true)) {
                        ConexionArticulo = null;
                    }
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

        public FkArticuloInventarioViewModel ConexionArticulo {
            get {
                return _ConexionArticulo;
            }
            set {
                if (_ConexionArticulo != value) {
                    _ConexionArticulo = value;
                    RaisePropertyChanged(ArticuloPropertyName);
                }
                if (_ConexionArticulo == null) {
                    Articulo = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseLineaDeProductoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseArticuloCommand {
            get;
            private set;
        }
		#endregion //Propiedades
        #region Constructores

        public clsArticulosPorVencerViewModel() {
        
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibBusinessComponentWithSearch GetBusinessComponent() {
            return new clsLoteDeInventarioNav();
        }
         protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseLineaDeProductoCommand = new RelayCommand<string>(ExecuteChooseLineaDeProductoCommand);
            ChooseArticuloCommand = new RelayCommand<string>(ExecuteChooseArticuloCommand);
        }		
        
        private void ExecuteChooseLineaDeProductoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionLineaDeProducto = ChooseRecord<FkLineaDeProductoViewModel>("Linea De Producto", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionLineaDeProducto != null) {
                    LineaDeProducto = ConexionLineaDeProducto.Codigo;
                } else {
                    LineaDeProducto = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseArticuloCommand(string valDescripcion) {
            try {
                if (valDescripcion == null) {
                    valDescripcion = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Descripcion", valDescripcion);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionArticulo = ChooseRecord<FkArticuloInventarioViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionArticulo != null) {
                    Articulo = ConexionArticulo.Descripcion;
                } else {
                    Articulo = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
		#endregion //Metodos Generados
       
    } //End of class clsArticulosPorVencerViewModel

} //End of namespace Galac.Saw.Uil.Inventario

