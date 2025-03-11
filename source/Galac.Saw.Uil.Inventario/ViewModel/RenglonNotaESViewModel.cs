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
using System.Text;
using System.Collections.ObjectModel;

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
        private FkLoteDeInventarioViewModel _ConexionLoteDeInventario = null;
        #endregion //Variables
        #region Propiedades
        public override string ModuleName {
            get { return "Renglón Nota ES"; }
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
        [LibGridColum("Cód. Articulo", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoArticulo", ConnectionSearchCommandName = "ChooseCodigoArticuloCommand", Width = 250)]
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
        [LibGridColum("Cantidad", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 150)]
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
                    RaisePropertyChanged(() => TipoDeMercanciaStr);
                }
            }
        }

        public string TipoDeMercanciaStr {
            get { return Model.TipoArticuloInvAsString; }
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
        [LibGridColum("Lote de Inventario", Width = 300)]
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
            get { return (ConexionCodigoArticulo != null) && (ConexionCodigoArticulo.TipoDeArticulo == eTipoDeArticulo.Mercancia) && (ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento || ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.Lote || ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.LoteFechadeElaboracion); }
        }
        public bool IsVisibleFechaDeElaboracionLoteDeInventario {
            get { return (ConexionCodigoArticulo != null) && (ConexionCodigoArticulo.TipoDeArticulo == eTipoDeArticulo.Mercancia) && (ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento || ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.LoteFechadeElaboracion); }
        }
        public bool IsVisibleFechaDeVencimientoLoteDeInventario {
            get { return (ConexionCodigoArticulo != null) && (ConexionCodigoArticulo.TipoDeArticulo == eTipoDeArticulo.Mercancia) && (ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento); }
        }
        public bool IsVisibleSerial {
            get { return (ConexionCodigoArticulo != null) && (ConexionCodigoArticulo.TipoDeArticulo == eTipoDeArticulo.Mercancia) && (ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.UsaTallaColorySerial || ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.UsaSerial  || ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.UsaSerialRollo); }
        }
        public bool IsVisibleRollo {
            get { return (ConexionCodigoArticulo != null) && (ConexionCodigoArticulo.TipoDeArticulo == eTipoDeArticulo.Mercancia) && (ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.UsaSerialRollo); }
        }
        public eTipoArticuloInv[] ArrayTipoArticuloInv {
            get { return LibEnumHelper<eTipoArticuloInv>.GetValuesInArray(); }
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
                    if (_ConexionCodigoArticulo != null) {
                        CodigoArticulo = ConexionCodigoArticulo.CodigoCompuesto;
                        DescripcionArticulo = ConexionCodigoArticulo.Descripcion;
                        TipoArticuloInv = ConexionCodigoArticulo.TipoArticuloInv;
                    }
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                    RaisePropertyChanged(() => DescripcionArticulo);
                    RaisePropertyChanged(() => Cantidad);
                    RaisePropertyChanged(() => TipoArticuloInv);
                }
                if (_ConexionCodigoArticulo == null) {
                    CodigoArticulo = string.Empty;
                }
            }
        }

        public FkLoteDeInventarioViewModel ConexionLoteDeInventario {
            get {
                return _ConexionLoteDeInventario;
            }
            set {
                if (_ConexionLoteDeInventario != value) {
                    _ConexionLoteDeInventario = value;
                    RaisePropertyChanged(LoteDeInventarioPropertyName);
                }
            }
        }

        public RelayCommand<string> ChooseCodigoArticuloCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseLoteDeInventarioCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public RenglonNotaESViewModel() : base(new RenglonNotaES(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public RenglonNotaESViewModel(NotaDeEntradaSalidaViewModel initMaster, RenglonNotaES initModel, eAccionSR initAction) : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
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
            ChooseLoteDeInventarioCommand = new RelayCommand<string>(ExecuteChooseLoteDeInventarioCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            LibSearchCriteria vDefaultCriteriaInventario = LibSearchCriteria.CreateCriteria("CodigoCompuesto", CodigoArticulo);
            vDefaultCriteriaInventario.Add(LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania")), eLogicOperatorType.And);
            ConexionCodigoArticulo = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Artículo Inventario", vDefaultCriteriaInventario);
            LibSearchCriteria vDefaultCriteriaLote = LibSearchCriteria.CreateCriteria("CodigoLote", LoteDeInventario);
            vDefaultCriteriaLote.Add(LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania")), eLogicOperatorType.And);
            ConexionLoteDeInventario = Master.FirstConnectionRecordOrDefault<FkLoteDeInventarioViewModel>("Lote de Inventario", vDefaultCriteriaLote);
        }
        private void ExecuteChooseCodigoArticuloCommand(string valCodigoArticulo) {
            try {
                if (valCodigoArticulo == null) {
                    valCodigoArticulo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("CodigoCompuesto", valCodigoArticulo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("TipoDeArticulo", LibConvert.EnumToDbValue((int)eTipoDeArticulo.Mercancia)), eLogicOperatorType.And);
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("StatusdelArticulo ", LibConvert.EnumToDbValue((int)eStatusArticulo.Vigente)), eLogicOperatorType.And);
                //      vFixedCriteria.Add("TipoDeArticulo", eBooleanOperatorType.IdentityInequality, LibConvert.EnumToDbValue((int)eTipoDeArticulo.ProductoCompuesto), eLogicOperatorType.And);
                ConexionCodigoArticulo = Master.ChooseRecord<FkArticuloInventarioViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoArticulo == null) {
                    CodigoArticulo = string.Empty;
                    DescripcionArticulo = string.Empty;
                } else {
                    CodigoArticulo = ConexionCodigoArticulo.CodigoCompuesto;
                    DescripcionArticulo = ConexionCodigoArticulo.Descripcion;
                    TipoArticuloInv = ConexionCodigoArticulo.TipoArticuloInv;
                }
                RaisePropertyChanged(() => IsVisbleLoteDeInventario);
                RaisePropertyChanged(() => IsEnabledLoteDeInventario);
                RaisePropertyChanged(() => IsVisibleFechaDeElaboracionLoteDeInventario);
                RaisePropertyChanged(() => IsVisibleFechaDeVencimientoLoteDeInventario);
                RaisePropertyChanged(() => IsVisibleSerial);
                RaisePropertyChanged(() => IsVisibleRollo);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseLoteDeInventarioCommand(string valCodigoLote) {
            try {
                if (valCodigoLote == null) {
                    valCodigoLote = string.Empty;
                }
                bool vInvocarCrear = Master.TipodeOperacion == eTipodeOperacion.EntradadeInventario;
                vInvocarCrear = vInvocarCrear && !LibString.IsNullOrEmpty(valCodigoLote, true);
                vInvocarCrear = vInvocarCrear && !LibString.S1IsInS2("*", valCodigoLote);
                vInvocarCrear = vInvocarCrear && !((ILoteDeInventarioPdn)new clsLoteDeInventarioNav()).ExisteLoteDeInventario(Mfc.GetInt("Compania"), CodigoArticulo, valCodigoLote);
                if (vInvocarCrear) {
                    new LoteDeInventarioMngViewModel().ExecuteCreateCommandEspecial(ref valCodigoLote, CodigoArticulo, TipoArticuloInv);
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("CodigoLote", valCodigoLote);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("CodigoArticulo", CodigoArticulo), eLogicOperatorType.And);
                ConexionLoteDeInventario = Master.ChooseRecord<FkLoteDeInventarioViewModel>("Lote de Inventario", vDefaultCriteria, vFixedCriteria, "FechaDeVencimiento, FechaDeElaboracion, CodigoLote");
                if (ConexionLoteDeInventario == null) {
                    LoteDeInventario = string.Empty;
                    FechaDeElaboracion = LibDate.MinDateForDB();
                    FechaDeVencimiento = LibDate.MaxDateForDB();
                } else {
                    if (TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento && LibDate.F1IsLessThanF2(ConexionLoteDeInventario.FechaDeVencimiento, LibDate.Today())){
                        LibMessages.MessageBox.Information(this, $"El Articulo:{CodigoArticulo} - {LibString.Left(DescripcionArticulo, 15) + "..."} Lote: {ConexionLoteDeInventario.CodigoLote} venció el {ConexionLoteDeInventario.FechaDeVencimiento.ToString("dd/MM/yyyy")}.", ModuleName);
                    }
                    LoteDeInventario = ConexionLoteDeInventario.CodigoLote;
                    FechaDeElaboracion = ConexionLoteDeInventario.FechaDeElaboracion;
                    FechaDeVencimiento = ConexionLoteDeInventario.FechaDeVencimiento;
                    if (Action == eAccionSR.Insertar) {
                        if (TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento) {
                            if (LibDate.F1IsLessThanF2(FechaDeVencimiento, LibDate.Today())) {
                                StringBuilder vMensaje = new StringBuilder();
                                vMensaje.AppendLine("El Artículo:" + CodigoArticulo + " - " + LibString.Left(DescripcionArticulo, 15) + "...");
                                vMensaje.AppendLine("Lote: " + LoteDeInventario + " venció el: " + LibConvert.ToStr(FechaDeVencimiento));
                                LibMessages.MessageBox.Information(this, vMensaje.ToString(), Title);
                            }
                        }
                    }
                }
                RaisePropertyChanged(() => IsVisbleLoteDeInventario);
                RaisePropertyChanged(() => IsEnabledLoteDeInventario);
                RaisePropertyChanged(() => IsVisibleFechaDeElaboracionLoteDeInventario);
                RaisePropertyChanged(() => IsVisibleFechaDeVencimientoLoteDeInventario);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult LoteDeInventarioValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else if (TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento || TipoArticuloInv == eTipoArticuloInv.Lote || TipoArticuloInv == eTipoArticuloInv.LoteFechadeElaboracion) {
                if (LibString.IsNullOrEmpty(LoteDeInventario, true)) {
                    vResult = new ValidationResult("El Lote de Inventario no fue ingresado.");
                } else if (Master.TipodeOperacion != eTipodeOperacion.EntradadeInventario) {
                    if (ConexionLoteDeInventario == null || LibString.IsNullOrEmpty(ConexionLoteDeInventario.CodigoLote, true)) {
                        vResult = new ValidationResult("El Lote de Inventario es requerido y debe existir.");
                    }
                }
            } else if (TipoArticuloInv != eTipoArticuloInv.LoteFechadeVencimiento && TipoArticuloInv != eTipoArticuloInv.Lote && TipoArticuloInv != eTipoArticuloInv.LoteFechadeElaboracion) {
                LoteDeInventario = string.Empty;
                FechaDeElaboracion = LibDate.MinDateForDB();
                FechaDeVencimiento = LibDate.MaxDateForDB();
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
                && (ConexionCodigoArticulo != null)
                && (ConexionCodigoArticulo.TipoDeArticulo == eTipoDeArticulo.Mercancia)
                && (ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.LoteFechadeVencimiento || ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.Lote || ConexionCodigoArticulo.TipoArticuloInv == eTipoArticuloInv.LoteFechadeElaboracion)) {
                return true;
            } else {
                return false;
            }
        }
        public bool NotaESIsEnabled {
            get {
                return IsEnabled && Action != eAccionSR.Anular && Action != eAccionSR.ReImprimir && Action != eAccionSR.Reversar;
            }
        }

    } //End of class RenglonNotaESViewModel
} //End of namespace Galac.Saw.Uil.Inventario