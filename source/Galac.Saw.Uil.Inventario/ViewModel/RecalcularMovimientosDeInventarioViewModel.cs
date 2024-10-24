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

namespace Galac.Saw.Uil.Inventario.ViewModel {
    public class RecalcularMovimientosDeInventarioViewModel : LibInputViewModel<RecalcularMovimientosDeInventario> {
        #region Constantes
        #endregion
        #region Variables
        private FkArticuloInventarioViewModel _ConexionCodigoArticulo = null;
        private FkLineaDeProductoViewModel _ConexionLineaDeProducto = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Recalcular Movimientos de Inventario"; }
        }

        public eCantidadAImprimir ArticuloUnoTodos {
            get { return Model.ArticuloUnoTodosAsEnum; }
            set {
                if (Model.ArticuloUnoTodosAsEnum != value) {
                    Model.ArticuloUnoTodosAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(() => ArticuloUnoTodos);
                }
            }
        }

        public string CodigoArticulo {
            get { return Model.CodigoArticulo; }
            set {
                if (Model.CodigoArticulo != value) {
                    Model.CodigoArticulo = value;
                    IsDirty = true;
                    RaisePropertyChanged(() => CodigoArticulo);
                    if (LibString.IsNullOrEmpty(CodigoArticulo, true)) {
                        ConexionCodigoArticulo = null;
                    }
                }
            }
        }

        public eCantidadAImprimir LineaDeProductoUnoTodos {
            get { return Model.LineaDeProductoUnoTodosAsEnum; }
            set {
                if (Model.LineaDeProductoUnoTodosAsEnum != value) {
                    Model.LineaDeProductoUnoTodosAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(() => LineaDeProductoUnoTodos);
                }
            }
        }

        public string LineaDeProducto {
            get { return Model.LineaDeProducto; }
            set {
                if (Model.LineaDeProducto != value) {
                    Model.LineaDeProducto = value;
                    IsDirty = true;
                    RaisePropertyChanged(() => LineaDeProducto);
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
                    RaisePropertyChanged(() => CodigoArticulo);
                }
                if (_ConexionCodigoArticulo == null) {
                    CodigoArticulo = string.Empty;
                }
            }
        }

        public FkLineaDeProductoViewModel ConexionLineaDeProducto {
            get { return _ConexionLineaDeProducto; }
            set {
                if (_ConexionLineaDeProducto != value) {
                    _ConexionLineaDeProducto = value;
                    RaisePropertyChanged(() => LineaDeProducto);
                }
                if (_ConexionLineaDeProducto == null) {
                    LineaDeProducto = string.Empty;
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

        public bool IsVisibleCodigoArticulo { get { return ArticuloUnoTodos == eCantidadAImprimir.One; } }
        public bool IsVisibleCantidadLineasDeProducto { get { return ArticuloUnoTodos == eCantidadAImprimir.All; } }
        public bool IsVisibleLineaDeProducto { get { return LineaDeProductoUnoTodos == eCantidadAImprimir.One; } }
        #endregion //Propiedades
        #region Constructores
        public RecalcularMovimientosDeInventarioViewModel()
            : this(new RecalcularMovimientosDeInventario(), eAccionSR.Insertar) {
        }
        public RecalcularMovimientosDeInventarioViewModel(RecalcularMovimientosDeInventario initModel, eAccionSR initAction) : base(initModel, initAction) {
            //DefaultFocusedPropertyName = ArticuloUnoTodosPropertyName;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(RecalcularMovimientosDeInventario valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override RecalcularMovimientosDeInventario FindCurrentRecord(RecalcularMovimientosDeInventario valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInEnum("ArticuloUnoTodos", LibConvert.EnumToDbValue((int)valModel.ArticuloUnoTodosAsEnum));
            return BusinessComponent.GetData(eProcessMessageType.SpName, "RecalcularMovimientosDeInventarioGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<RecalcularMovimientosDeInventario>, IList<RecalcularMovimientosDeInventario>> GetBusinessComponent() {
            return null;
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoArticuloCommand = new RelayCommand<string>(ExecuteChooseCodigoArticuloCommand);
            ChooseLineaDeProductoCommand = new RelayCommand<string>(ExecuteChooseLineaDeProductoCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionCodigoArticulo = FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Artículo Inventario", LibSearchCriteria.CreateCriteria("Codigo", CodigoArticulo));
            ConexionLineaDeProducto = FirstConnectionRecordOrDefault<FkLineaDeProductoViewModel>("Linea De Producto", LibSearchCriteria.CreateCriteria("Nombre", LineaDeProducto));
        }

        private void ExecuteChooseCodigoArticuloCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                //LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                //ConexionCodigoArticulo = ChooseRecord<FkArticuloInventarioViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
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

        private void ExecuteChooseLineaDeProductoCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
                //LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                //ConexionCodigoLineaDeProducto = ChooseRecord<FkLineaDeProductoViewModel>("Linea De Producto", vDefaultCriteria, vFixedCriteria, string.Empty);
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
        #endregion //Metodos Generados

    } //End of class RecalcularMovimientosDeInventarioViewModel
} //End of namespace Galac..Uil.ComponenteNoEspecificado