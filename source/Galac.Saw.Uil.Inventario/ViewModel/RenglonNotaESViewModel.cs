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

namespace Galac.Saw.Uil.Inventario.ViewModel {
    public class RenglonNotaESViewModel : LibInputDetailViewModelMfc<RenglonNotaES> {
        #region Constantes
        public const string CodigoArticuloPropertyName = "CodigoArticulo";
        public const string DescripcionArticuloPropertyName = "DescripcionArticulo";
        public const string CantidadPropertyName = "Cantidad";
        public const string LoteDeInventarioPropertyName = "LoteDeInventario";
        public const string FechaDeElaboracionPropertyName = "FechaDeElaboracion";
        public const string FechaDeVencimientoPropertyName = "FechaDeVencimiento";
        #endregion
        #region Variables
        private FkArticuloInventarioViewModel _ConexionCodigoArticulo = null;
        //private FkArticuloInventarioViewModel _ConexionDescripcionArticulo = null;
        #endregion //Variables
        #region Propiedades
        public override string ModuleName {
            get { return "Renglon Nota ES"; }
        }

        public int ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if (Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                }
            }
        }

        public string NumeroDocumento {
            get {
                return Model.NumeroDocumento;
            }
            set {
                if (Model.NumeroDocumento != value) {
                    Model.NumeroDocumento = value;
                }
            }
        }

        public int ConsecutivoRenglon {
            get {
                return Model.ConsecutivoRenglon;
            }
            set {
                if (Model.ConsecutivoRenglon != value) {
                    Model.ConsecutivoRenglon = value;
                }
            }
        }

        //[LibRequired(ErrorMessage = "El Código del Artículo es requerido.")]
        [LibGridColum("Código Articulo", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoArticulo", ConnectionSearchCommandName = "ChooseCodigoArticuloCommand", MaxWidth = 150)]
        public string CodigoArticulo {
            get {
                return Model.CodigoArticulo;
            }
            set {
                if (Model.CodigoArticulo != value) {
                    Model.CodigoArticulo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoArticulo, true)) {
                        ConexionCodigoArticulo = null;
                    }
                }
            }
        }

        //[LibRequired(ErrorMessage = "El campo Descripción es requerido.")]
        //[LibGridColum("Descripción", eGridColumType.Connection, ConnectionDisplayMemberPath = "Descripción", ConnectionModelPropertyName = "DescripcionArticulo", ConnectionSearchCommandName = "ChooseDescripcionArticuloCommand", MaxWidth = 120)]
        public string DescripcionArticulo {
            get {
                return Model.DescripcionArticulo;
            }
            set {
                if (Model.DescripcionArticulo != value) {
                    Model.DescripcionArticulo = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionArticuloPropertyName);
                    //if (LibString.IsNullOrEmpty(DescripcionArticulo, true)) {
                    //    ConexionDescripcionArticulo = null;
                    //}
                }
            }
        }

        [LibGridColum("Cantidad", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        public decimal Cantidad {
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

        public eTipoArticuloInv TipoArticuloInv {
            get {
                return Model.TipoArticuloInvAsEnum;
            }
            set {
                if (Model.TipoArticuloInvAsEnum != value) {
                    Model.TipoArticuloInvAsEnum = value;
                }
            }
        }

        public string Serial {
            get {
                return Model.Serial;
            }
            set {
                if (Model.Serial != value) {
                    Model.Serial = value;
                }
            }
        }

        public string Rollo {
            get {
                return Model.Rollo;
            }
            set {
                if (Model.Rollo != value) {
                    Model.Rollo = value;
                }
            }
        }

        public decimal CostoUnitario {
            get {
                return Model.CostoUnitario;
            }
            set {
                if (Model.CostoUnitario != value) {
                    Model.CostoUnitario = value;
                }
            }
        }

        public decimal CostoUnitarioME {
            get {
                return Model.CostoUnitarioME;
            }
            set {
                if (Model.CostoUnitarioME != value) {
                    Model.CostoUnitarioME = value;
                }
            }
        }

        [LibGridColum("Lote De Inventario", MaxLength = 30)]
        public string LoteDeInventario {
            get {
                return Model.LoteDeInventario;
            }
            set {
                if (Model.LoteDeInventario != value) {
                    Model.LoteDeInventario = value;
                    IsDirty = true;
                    RaisePropertyChanged(LoteDeInventarioPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaDeElaboracionValidating")]
        [LibGridColum("Fecha De Elaboracion", eGridColumType.DatePicker)]
        public DateTime FechaDeElaboracion {
            get {
                return Model.FechaDeElaboracion;
            }
            set {
                if (Model.FechaDeElaboracion != value) {
                    Model.FechaDeElaboracion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaDeElaboracionPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaDeVencimientoValidating")]
        [LibGridColum("Fecha De Vencimiento", eGridColumType.DatePicker)]
        public DateTime FechaDeVencimiento {
            get {
                return Model.FechaDeVencimiento;
            }
            set {
                if (Model.FechaDeVencimiento != value) {
                    Model.FechaDeVencimiento = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaDeVencimientoPropertyName);
                }
            }
        }

        public bool IsEnabledLoteDeInventario {
            get { return false; }// IsEnabled && ConexionCodigoArticulo.TipoDeArticulo == eTipoDeArticulo.Mercancia && ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento; }
        }

        public bool IsVisbleLoteDeInventario {
            get { return false; }// ConexionCodigoArticulo.TipoDeArticulo == eTipoDeArticulo.Mercancia && ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento; }
        }

        public eTipoArticuloInv[] ArrayTipoArticuloInv {
            get {
                return LibEnumHelper<eTipoArticuloInv>.GetValuesInArray();
            }
        }

        public NotaDeEntradaSalidaViewModel Master {
            get;
            set;
        }

        public FkArticuloInventarioViewModel ConexionCodigoArticulo {
            get {
                return _ConexionCodigoArticulo;
            }
            set {
                if (_ConexionCodigoArticulo != value) {
                    _ConexionCodigoArticulo = value;
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                }
                if (_ConexionCodigoArticulo == null) {
                    CodigoArticulo = string.Empty;
                }
            }
        }

        //public FkArticuloInventarioViewModel ConexionDescripcionArticulo {
        //    get {
        //        return _ConexionDescripcionArticulo;
        //    }
        //    set {
        //        if (_ConexionDescripcionArticulo != value) {
        //            _ConexionDescripcionArticulo = value;
        //            RaisePropertyChanged(DescripcionArticuloPropertyName);
        //        }
        //        if (_ConexionDescripcionArticulo == null) {
        //            DescripcionArticulo = string.Empty;
        //        }
        //    }
        //}

        public RelayCommand<string> ChooseCodigoArticuloCommand {
            get;
            private set;
        }

        //public RelayCommand<string> ChooseDescripcionArticuloCommand {
        //    get;
        //    private set;
        //}
        #endregion //Propiedades
        #region Constructores
        public RenglonNotaESViewModel()
            : base(new RenglonNotaES(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public RenglonNotaESViewModel(NotaDeEntradaSalidaViewModel initMaster, RenglonNotaES initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(RenglonNotaES valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ILibBusinessDetailComponent<IList<RenglonNotaES>, IList<RenglonNotaES>> GetBusinessComponent() {
            return new clsRenglonNotaESNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoArticuloCommand = new RelayCommand<string>(ExecuteChooseCodigoArticuloCommand);
            //ChooseDescripcionArticuloCommand = new RelayCommand<string>(ExecuteChooseDescripcionArticuloCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionCodigoArticulo = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Artículo Inventario", LibSearchCriteria.CreateCriteria("Descripcion", CodigoArticulo));
            //ConexionDescripcionArticulo = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Artículo Inventario", LibSearchCriteria.CreateCriteria("Descripcion", DescripcionArticulo));
        }

        private void ExecuteChooseCodigoArticuloCommand(string valDescripcion) {
            try {
                if (valDescripcion == null) {
                    valDescripcion = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Descripcion", valDescripcion);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCodigoArticulo = Master.ChooseRecord<FkArticuloInventarioViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoArticulo != null) {
                    CodigoArticulo = ConexionCodigoArticulo.Descripcion;
                    DescripcionArticulo = ConexionCodigoArticulo.Descripcion;
                } else {
                    CodigoArticulo = string.Empty;
                    DescripcionArticulo = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        //private void ExecuteChooseDescripcionArticuloCommand(string valDescripcion) {
        //    try {
        //        if (valDescripcion == null) {
        //            valDescripcion = string.Empty;
        //        }
        //        LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Descripcion", valDescripcion);
        //        LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
        //        ConexionDescripcionArticulo = Master.ChooseRecord<FkArticuloInventarioViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
        //        if (ConexionDescripcionArticulo != null) {
        //            CodigoArticulo = ConexionDescripcionArticulo.Descripcion;
        //            DescripcionArticulo = ConexionDescripcionArticulo.Descripcion;
        //        } else {
        //            CodigoArticulo = string.Empty;
        //            DescripcionArticulo = string.Empty;
        //        }
        //    } catch (System.AccessViolationException) {
        //        throw;
        //    } catch (System.Exception vEx) {
        //        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
        //    }
        //}

        private ValidationResult FechaDeElaboracionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDeElaboracion, false, Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha De Elaboracion"));
                }
            }
            return vResult;
        }

        private ValidationResult FechaDeVencimientoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDeVencimiento, false, Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha De Vencimiento"));
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados

    } //End of class RenglonNotaESViewModel
} //End of namespace Galac.Saw.Uil.Inventario