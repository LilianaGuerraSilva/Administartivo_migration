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
        DateTime MinimaFechaLoteFdV = new DateTime(2000, 01, 01);
        #endregion
        #region Variables
        private FkArticuloInventarioViewModel _ConexionCodigoArticulo = null;
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

        [LibRequired(ErrorMessage = "El Código del Artículo es requerido.")]
        [LibGridColum("Cód. Articulo", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoArticulo", ConnectionSearchCommandName = "ChooseCodigoArticuloCommand", MaxWidth = 120)]
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

        public string DescripcionArticulo {
            get {
                return Model.DescripcionArticulo;
            }
            set {
                if (Model.DescripcionArticulo != value) {
                    Model.DescripcionArticulo = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionArticuloPropertyName);
                }
            }
        }

        [LibCustomValidation("CantidadValidating")]
        [LibGridColum("Cantidad", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 100)]
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

        [LibCustomValidation("LoteDeInventarioValidating")]
        [LibGridColum("Lote de Inventario", MaxLength = 50)]
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
        [LibGridColum("Fecha Elab.", eGridColumType.DatePicker, Width = 100)]
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
        [LibGridColum("Fecha Vcto.", eGridColumType.DatePicker, Width = 100)]
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
            get { return IsEnabled && SePuedeEditarLote(); }
        }

        public bool IsVisbleLoteDeInventario {
            get { return ConexionCodigoArticulo != null && ConexionCodigoArticulo.TipoDeArticulo == eTipoDeArticulo.Mercancia && ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento; }
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
                    RaisePropertyChanged(() => DescripcionArticulo);
                    RaisePropertyChanged(() => Cantidad);
                    RaisePropertyChanged(() => TipoArticuloInv);
                    RaisePropertyChanged(() => LoteDeInventario);
                    RaisePropertyChanged(() => FechaDeElaboracion);
                    RaisePropertyChanged(() => FechaDeVencimiento);
                }
                if (_ConexionCodigoArticulo == null) {
                    CodigoArticulo = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoArticuloCommand {
            get;
            private set;
        }

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
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionCodigoArticulo = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Artículo Inventario", LibSearchCriteria.CreateCriteria("Descripcion", CodigoArticulo));
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
                    TipoArticuloInv = ConexionCodigoArticulo.TipoArticuloInv;
                    LoteDeInventario = ConexionCodigoArticulo.CodigoLote;
                    FechaDeElaboracion = ConexionCodigoArticulo.FechaDeElaboracion;
                    FechaDeVencimiento = ConexionCodigoArticulo.FechaDeElaboracion;

                    RaisePropertyChanged(() => IsVisbleLoteDeInventario);
                    RaisePropertyChanged(() => IsEnabledLoteDeInventario);
                } else {
                    CodigoArticulo = string.Empty;
                    DescripcionArticulo = string.Empty;
                    LoteDeInventario = string.Empty;
                    FechaDeElaboracion = new DateTime(2000, 01, 01);
                    FechaDeVencimiento = new DateTime(2000, 01, 01);
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult FechaDeElaboracionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Anular)) {
                return ValidationResult.Success;
            } else if (TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento) {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDeElaboracion, false, Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de Elaboración"));
                } else if (LibDate.F1IsGreaterThanF2(FechaDeElaboracion, FechaDeVencimiento)) {
                    vResult = new ValidationResult("La Fecha de Elaboración debe ser menor o igual a la Fecha de Vencimiento.");
                } else if (LibDate.F1IsLessThanF2(FechaDeElaboracion, MinimaFechaLoteFdV)) {
                    vResult = new ValidationResult("La Fecha de Elaboración debe ser mayor o igual a: " + LibConvert.ToStr(MinimaFechaLoteFdV));
                }
            } else if (TipoArticuloInv != eTipoArticuloInv.LoteFechadeVencimiento) {
                FechaDeElaboracion = LibDate.MinDateForDB();
            }
            return vResult;
        }

        private ValidationResult FechaDeVencimientoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else if (TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento) {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDeVencimiento, false, Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de Vencimiento"));
                } else if (LibDate.F1IsGreaterThanF2(FechaDeElaboracion, FechaDeVencimiento)) {
                    vResult = new ValidationResult("La Fecha de Vencimiento debe ser mayor o igual a la Fecha de Elaboración.");
                } else if (LibDate.F1IsLessThanF2(FechaDeVencimiento, MinimaFechaLoteFdV)) {
                    vResult = new ValidationResult("La Fecha de Vencimiento debe ser mayor o igual a: " + LibConvert.ToStr(MinimaFechaLoteFdV));
                }
            } else if (TipoArticuloInv != eTipoArticuloInv.LoteFechadeVencimiento) {
                FechaDeVencimiento = LibDate.MinDateForDB();
            }
            return vResult;
        }

        private ValidationResult LoteDeInventarioValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else if (TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento) {
                if (LibString.IsNullOrEmpty(LoteDeInventario, true)) {
                    vResult = new ValidationResult("Para los Artículos de Inventario del tipo Lote/Fecha de Vencimiento, el Lote de Inventario es requerido.");
                }
            } else if (TipoArticuloInv != eTipoArticuloInv.LoteFechadeVencimiento) {
                LoteDeInventario = string.Empty;
                FechaDeElaboracion = new DateTime(2000, 1, 1);
                FechaDeVencimiento = new DateTime(2000, 1, 1);
            }
            return vResult;
        }

        private ValidationResult CantidadValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else if (Cantidad <= 0) {
                vResult = new ValidationResult("El valor de Cantidad debe ser mayor a cero (0).");
            }
            return vResult;
        }
        #endregion //Metodos Generados

        private bool SePuedeEditarLote() {
            if ((Action == eAccionSR.Insertar)
                && (Master.TipodeOperacion == eTipodeOperacion.EntradadeInventario)
                && (ConexionCodigoArticulo != null)
                && (ConexionCodigoArticulo.TipoDeArticulo == eTipoDeArticulo.Mercancia)
                && (ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento)) {
                return true;
                //bool vExisteLote = ((ILoteDeInventarioPdn)new clsLoteDeInventarioNav()).ExisteLoteDeInventario(Mfc.GetInt("Compania"), CodigoArticulo, LoteDeInventario);
                //return !vExisteLote;
            } else {
                return false;
            }
        }

    } //End of class RenglonNotaESViewModel
} //End of namespace Galac.Saw.Uil.Inventario