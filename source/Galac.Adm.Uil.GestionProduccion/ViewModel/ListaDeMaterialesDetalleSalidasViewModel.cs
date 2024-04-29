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
using Galac.Adm.Brl.GestionProduccion;
using Galac.Adm.Ccl.GestionProduccion;

namespace Galac.Adm.Uil.GestionProduccion.ViewModel {
    public class ListaDeMaterialesDetalleSalidasViewModel : LibInputDetailViewModelMfc<ListaDeMaterialesDetalleSalidas> {
        #region Constantes
        public const string CodigoArticuloInventarioPropertyName = "CodigoArticuloInventario";
        public const string DescripcionArticuloInventarioPropertyName = "DescripcionArticuloInventario";
        public const string CantidadPropertyName = "Cantidad";
        public const string UnidadDeVentaPropertyName = "UnidadDeVenta";
        public const string PorcentajeDeCostoPropertyName = "PorcentajeDeCosto";
        public const string PorcentajeDeCostoTotalPropertyName = "PorcentajeDeCostoTotal";
        
        #endregion
        #region Variables
        private FkArticuloInventarioViewModel _ConexionCodigoArticuloInventario = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Salidas"; }
        }

        public int  ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if (Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                }
            }
        }

        public int  ConsecutivoListaDeMateriales {
            get {
                return Model.ConsecutivoListaDeMateriales;
            }
            set {
                if (Model.ConsecutivoListaDeMateriales != value) {
                    Model.ConsecutivoListaDeMateriales = value;
                }
            }
        }

        public int  Consecutivo {
            get {
                return Model.Consecutivo;
            }
            set {
                if (Model.Consecutivo != value) {
                    Model.Consecutivo = value;
                }
            }
        }

        [LibGridColum("Código Artículo", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoArticuloInventario", ConnectionSearchCommandName = "ChooseCodigoArticuloInventarioCommand", MaxWidth=100)]
        public string  CodigoArticuloInventario {
            get {
                return Model.CodigoArticuloInventario;
            }
            set {
                if (Model.CodigoArticuloInventario != value) {
                    Model.CodigoArticuloInventario = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoArticuloInventarioPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoArticuloInventario, true)) {
                        ConexionCodigoArticuloInventario = null;
                    }
                }
            }
        }

        [LibGridColum("Descripción", eGridColumType.Connection, ConnectionDisplayMemberPath = "Descripcion", ConnectionModelPropertyName = "DescripcionArticuloInventario", ConnectionSearchCommandName = "ChooseDescripcionArticuloInventarioCommand", Width = 362, Trimming = System.Windows.TextTrimming.WordEllipsis)]
        public string  DescripcionArticuloInventario {
            get {
                return Model.DescripcionArticuloInventario;
            }
            set {
                if (Model.DescripcionArticuloInventario != value) {
                    Model.DescripcionArticuloInventario = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionArticuloInventarioPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Cantidad es requerido.")]
        [LibGridColum("Cantidad", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ConditionalPropertyDecimalDigits = "DecimalDigits", MaxWidth = 90)]
        public decimal  Cantidad {
            get {
                return Model.Cantidad;
            }
            set {
                if (Model.Cantidad != value) {
                    Model.Cantidad = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadPropertyName);
                }
            }
        }

        [LibGridColum("Unidad", eGridColumType.Connection, ConnectionDisplayMemberPath = "UnidadDeVenta", ConnectionModelPropertyName = "UnidadDeVenta", MaxWidth=60)]
        public string  UnidadDeVenta {
            get {
                return Model.UnidadDeVenta;
            }
            set {
                if (Model.UnidadDeVenta != value) {
                    Model.UnidadDeVenta = value;
                    IsDirty = true;
                    RaisePropertyChanged(UnidadDeVentaPropertyName);
                }
            }
        }

        [LibCustomValidation("PorcentajeDeCostoValidating")]
        [LibGridColum("%Costo", eGridColumType.Numeric, Alignment = eTextAlignment.Right, MaxWidth = 70, ConditionalPropertyDecimalDigits = "DecimalDigitsCosto")]
        public decimal  PorcentajeDeCosto {
            get {
                return Model.PorcentajeDeCosto;
            }
            set {
                if (Model.PorcentajeDeCosto != value) {
                    Model.PorcentajeDeCosto = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeDeCostoPropertyName);
                }
            }
        }

        public ListaDeMaterialesViewModel Master {
            get;
            set;
        }

        public FkArticuloInventarioViewModel ConexionCodigoArticuloInventario {
            get {
                return _ConexionCodigoArticuloInventario;
            }
            set {
                if (_ConexionCodigoArticuloInventario != value) {
                    _ConexionCodigoArticuloInventario = value;
                    RaisePropertyChanged(CodigoArticuloInventarioPropertyName);
                }
                if (_ConexionCodigoArticuloInventario == null) {
                    CodigoArticuloInventario = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoArticuloInventarioCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public ListaDeMaterialesDetalleSalidasViewModel()
            : base(new ListaDeMaterialesDetalleSalidas(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public ListaDeMaterialesDetalleSalidasViewModel(ListaDeMaterialesViewModel initMaster, ListaDeMaterialesDetalleSalidas initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(ListaDeMaterialesDetalleSalidas valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ILibBusinessDetailComponent<IList<ListaDeMaterialesDetalleSalidas>, IList<ListaDeMaterialesDetalleSalidas>> GetBusinessComponent() {
            return new clsListaDeMaterialesDetalleSalidasNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoArticuloInventarioCommand = new RelayCommand<string>(ExecuteChooseCodigoArticuloInventarioCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionCodigoArticuloInventario = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Artículo Inventario", LibSearchCriteria.CreateCriteria("Codigo", CodigoArticuloInventario));
        }

        private void ExecuteChooseCodigoArticuloInventarioCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCodigoArticuloInventario = Master.ChooseRecord<FkArticuloInventarioViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoArticuloInventario != null) {
                    CodigoArticuloInventario = ConexionCodigoArticuloInventario.Codigo;
                    DescripcionArticuloInventario = ConexionCodigoArticuloInventario.Descripcion;
                    UnidadDeVenta = ConexionCodigoArticuloInventario.UnidadDeVenta;
                } else {
                    CodigoArticuloInventario = string.Empty;
                    DescripcionArticuloInventario = string.Empty;
                    UnidadDeVenta = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        public int DecimalDigits {
            get {
                return 8;
            }
        }

        public int DecimalDigitsCosto {
            get {
                return 4;
            }
        }

        private ValidationResult PorcentajeDeCostoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (((Action == eAccionSR.Insertar) || (Action == eAccionSR.Modificar)) && (PorcentajeDeCosto < 0 || PorcentajeDeCosto > 100)) {
                return ValidationResult.Success;
            }
            return vResult;
        }
        #endregion //Metodos Generados
    } //End of class ListaDeMaterialesDetalleSalidasViewModel

} //End of namespace Galac.Adm.Uil.GestionProduccion

