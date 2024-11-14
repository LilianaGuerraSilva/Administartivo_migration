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
using System.Collections.ObjectModel;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.Brl;

namespace Galac.Adm.Uil.GestionProduccion.ViewModel {
    public class OrdenDeProduccionDetalleMaterialesViewModel : LibInputDetailViewModelMfc<OrdenDeProduccionDetalleMateriales> {
        #region Constantes
        public const string ConsecutivoLoteDeInventarioPropertyName = "ConsecutivoLoteDeInventario";
        public const string CodigoAlmacenPropertyName = "CodigoAlmacen";
        public const string NombreAlmacenPropertyName = "NombreAlmacen";
        public const string CodigoArticuloPropertyName = "CodigoArticulo";
        public const string DescripcionArticuloPropertyName = "DescripcionArticulo";
        public const string UnidadDeVentaPropertyName = "UnidadDeVenta";
        public const string CantidadPropertyName = "Cantidad";
        public const string CantidadReservadaInventarioPropertyName = "CantidadReservadaInventario";
        public const string CantidadConsumidaPropertyName = "CantidadConsumida";
        public const string CostoUnitarioArticuloInventarioPropertyName = "CostoUnitarioArticuloInventario";
        public const string MontoSubtotalPropertyName = "MontoSubtotal";
        public const string AjustadoPostCierrePropertyName = "AjustadoPostCierre";
        public const string CantidadAjustadaPropertyName = "CantidadAjustada";
        public const string PorcentajeMermaNormalOriginalPropertyName = "PorcentajeMermaNormalOriginal";
        public const string CantidadMermaNormalPropertyName = "CantidadMermaNormal";
        public const string PorcentajeMermaNormalPropertyName = "PorcentajeMermaNormal";
        public const string CantidadMermaAnormalPropertyName = "CantidadMermaAnormal";
        public const string PorcentajeMermaAnormalPropertyName = "PorcentajeMermaAnormal";
        #endregion
        #region Variables
        private FkLoteDeInventarioViewModel _ConexionLoteDeInventario = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Insumos"; }
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

        public int ConsecutivoOrdenDeProduccion {
            get {
                return Model.ConsecutivoOrdenDeProduccion;
            }
            set {
                if (Model.ConsecutivoOrdenDeProduccion != value) {
                    Model.ConsecutivoOrdenDeProduccion = value;
                }
            }
        }

        public int Consecutivo {
            get {
                return Model.Consecutivo;
            }
            set {
                if (Model.Consecutivo != value) {
                    Model.Consecutivo = value;
                }
            }
        }

        public int ConsecutivoAlmacen {
            get {
                return Model.ConsecutivoAlmacen;
            }
            set {
                if (Model.ConsecutivoAlmacen != value) {
                    Model.ConsecutivoAlmacen = value;
                }
            }
        }

        public int  ConsecutivoLoteDeInventario {
            get {
                return Model.ConsecutivoLoteDeInventario;
            }
            set {
                if (Model.ConsecutivoLoteDeInventario != value) {
                    Model.ConsecutivoLoteDeInventario = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConsecutivoLoteDeInventarioPropertyName);
                }
            }
        }
        

        public string CodigoAlmacen {
            get {
                return Model.CodigoAlmacen;
            }
            set {
                if (Model.CodigoAlmacen != value) {
                    Model.CodigoAlmacen = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoAlmacenPropertyName);

                }
            }
        }


        public string NombreAlmacen {
            get {
                return Model.NombreAlmacen;
            }
            set {
                if (Model.NombreAlmacen != value) {
                    Model.NombreAlmacen = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreAlmacenPropertyName);

                }
            }
        }

        [LibGridColum("Código", eGridColumType.Generic, MaxWidth = 120, ColumnOrder = 0)]
        public string CodigoArticulo {
            get {
                return Model.CodigoArticulo;
            }
            set {
                if (Model.CodigoArticulo != value) {
                    Model.CodigoArticulo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoArticuloPropertyName);

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

        
        [LibGridColum("Unidad", eGridColumType.Generic, MaxWidth = 80, ColumnOrder = 2)]
        public string UnidadDeVenta {
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

        [LibGridColum("Existencia Actual", eGridColumType.Numeric, ConditionalPropertyDecimalDigits = "2", Alignment = eTextAlignment.Right, ColumnOrder = 3, Width = 120)]
        public Decimal Existencia { get; set; }

        [LibGridColum("Cant. Original en Lista", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ColumnOrder = 4, ConditionalPropertyDecimalDigits = "DecimalDigits", Width =140)]
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

        [LibCustomValidation("CantidadReservadaInventarioValidating")]
        [LibGridColum("Cant. a Consumir", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 110, ConditionalPropertyDecimalDigits = "DecimalDigits", ColumnOrder = 5)]
        public decimal CantidadReservadaInventario {
            get {
                return Model.CantidadReservadaInventario;
            }
            set {
                if (Model.CantidadReservadaInventario != value) {
                    Model.CantidadReservadaInventario = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadReservadaInventarioPropertyName);
                }
            }
        }

        [LibCustomValidation("ValidatingCantidadConsumida")]
        [LibGridColum("Cant. Consumida", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 110, ColumnOrder = 6, ConditionalPropertyDecimalDigits = "DecimalDigits")]
        public decimal CantidadConsumida {
            get {
                return Model.CantidadConsumida;
            }
            set {
                if (Model.CantidadConsumida != value) {
                    Model.CantidadConsumida = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadConsumidaPropertyName);
                    CantMermaNormal();
                    CalcularPorcentajeMermaNormal();
                    CantMermaAnormal();
                    CalcularPorcentajeMermaAnormal();
                }
            }
        }

        [LibGridColum("Costo Total", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 80, ColumnOrder = 7, ConditionalPropertyDecimalDigits = "2")]
        public decimal MontoSubtotal {
            get {
                return Model.MontoSubtotal;
            }
            set {
                if (Model.MontoSubtotal != value) {
                    Model.MontoSubtotal = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoSubtotalPropertyName);
                }
            }
        }

        [LibGridColum("Costo Unitario", eGridColumType.Numeric, ColumnOrder = 10, ConditionalPropertyDecimalDigits = "DecimalDigits")]
        public decimal CostoUnitarioArticuloInventario {
            get {
                return Model.CostoUnitarioArticuloInventario;
            }
            set {
                if (Model.CostoUnitarioArticuloInventario != value) {
                    Model.CostoUnitarioArticuloInventario = value;
                    IsDirty = true;
                    RaisePropertyChanged(CostoUnitarioArticuloInventarioPropertyName);
                }
            }
        }

        public bool AjustadoPostCierre {
            get {
                return Model.AjustadoPostCierreAsBool;
            }
            set {
                if (Model.AjustadoPostCierreAsBool != value) {
                    Model.AjustadoPostCierreAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(AjustadoPostCierrePropertyName);
                }
            }
        }

        public decimal CantidadAjustada {
            get {
                return Model.CantidadAjustada;
            }
            set {
                if (Model.CantidadAjustada != value) {
                    Model.CantidadAjustada = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadAjustadaPropertyName);
                }
            }
        }

        public OrdenDeProduccionViewModel Master {
            get;
            set;
        }     

        public eTipoDeArticulo TipoDeArticulo {
            get { return Model.TipoDeArticuloAsEnum; }
        }

        public bool IsEnabledFechaFinalizacion {
            get {
                return Master.IsEnabledFechaFinalizacion;
            }
        }

        public bool IsVisibleFechaFinalizacion {
            get {
                return Master.IsVisibleFechaFinalizacion;
            }
        }

        public int DecimalDigits {
            get {
                return Master == null ? 8 : Master.DecimalDigits;
            }
        }

        public int DecimalDigitsCantidadOriginal {
            get {
                return Master == null ? 8 : Master.DecimalDigits;
            }
        }

        public bool IsEnabledCantidadReservadaInventario {
            get { return Master.Action == eAccionSR.Insertar || Master.Action == eAccionSR.Modificar; }
        }

        public bool IsVisibleCantidadReservadaInventario {
            get { return Master.Action == eAccionSR.Insertar || Master.Action == eAccionSR.Modificar; }
        }

        public bool IsVisibleCantidadConsumida {
            get { return Master.Action == eAccionSR.Cerrar; }
        }

        public bool IsEnabledCantidadConsumida {
            get { return Master.Action == eAccionSR.Cerrar; }
        }

        [LibGridColum("Lote", MaxWidth = 100, ColumnOrder = 1)]
        public string CodigoLote {
            get {
                return Model.CodigoLote;
            }
            set {
                if (Model.CodigoLote != value) {
                    Model.CodigoLote = value;
                    IsDirty = true;
                    RaisePropertyChanged(() => CodigoLote);
                }
            }
        }

        public decimal PorcentajeMermaNormalOriginal {
            get {
                return Model.PorcentajeMermaNormalOriginal;
            }
            set {
                if (Model.PorcentajeMermaNormalOriginal != value) {
                    Model.PorcentajeMermaNormalOriginal = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeMermaNormalOriginalPropertyName);
                }
            }
        }

        [LibCustomValidation("CantidadMermaNormalValidating")]
        [LibGridColum("Cant. Merma Normal", eGridColumType.Numeric, ColumnOrder = 8, Alignment = eTextAlignment.Right, ConditionalPropertyDecimalDigits = "DecimalDigits", Width = 140)]
        public decimal  CantidadMermaNormal {
            get {
                return Model.CantidadMermaNormal;
            }
            set {
                if (Model.CantidadMermaNormal != value) {
                    Model.CantidadMermaNormal = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadMermaNormalPropertyName);
                    CalcularPorcentajeMermaNormal();
                }
            }
        }

        public decimal  PorcentajeMermaNormal {
            get {
                return Model.PorcentajeMermaNormal;
            }
            set {
                if (Model.PorcentajeMermaNormal != value) {
                    Model.PorcentajeMermaNormal = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeMermaNormalPropertyName);
                }
            }
        }

        [LibCustomValidation("CantidadMermaAnormalValidating")]
        [LibGridColum("Cant. Merma Anormal", eGridColumType.Numeric, ColumnOrder = 9, Alignment = eTextAlignment.Right, ConditionalPropertyDecimalDigits = "DecimalDigits", Width = 140)]
        public decimal  CantidadMermaAnormal {
            get {
                return Model.CantidadMermaAnormal;
            }
            set {
                if (Model.CantidadMermaAnormal != value) {
                    Model.CantidadMermaAnormal = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadMermaAnormalPropertyName);
                    CalcularPorcentajeMermaAnormal();
                }
            }
        }

        public decimal  PorcentajeMermaAnormal {
            get {
                return Model.PorcentajeMermaAnormal;
            }
            set {
                if (Model.PorcentajeMermaAnormal != value) {
                    Model.PorcentajeMermaAnormal = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeMermaAnormalPropertyName);
                }
            }
        }

        public RelayCommand<string> ChooseLoteDeInventarioCommand {
            get;
            private set;
        }

        public DateTime FechaDeElaboracion {
            get {
                return Model.FechaDeElaboracion;
            }
            set {
                if (Model.FechaDeElaboracion != value) {
                    Model.FechaDeElaboracion = value;
                    IsDirty = true;
                    RaisePropertyChanged(() => FechaDeElaboracion);
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
                    RaisePropertyChanged(() => FechaDeVencimiento);
                }
            }
        }


        public eTipoArticuloInv TipoArticuloInvAsEnum {
            get {
                return Model.TipoArticuloInvAsEnum;
            }
            set {
                if (Model.TipoArticuloInvAsEnum  != value) {
                    Model.TipoArticuloInvAsEnum = value;
                    IsDirty = true;
                }
            }
        }

        public bool IsVisbleLoteDeInventario {
            get { return (Master.Action == eAccionSR.Consultar || Master.Action == eAccionSR.Abrir ||
                          Master.Action == eAccionSR.Anular || Master.Action == eAccionSR.Cerrar || Master.Action == eAccionSR.Custom)
                    && (TipoArticuloInvAsEnum == eTipoArticuloInv.Lote || TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeVencimiento); }
        }

        public bool IsVisibleFechaLoteDeInventario {
            get { return IsVisbleLoteDeInventario && TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeVencimiento && ConsecutivoLoteDeInventario != 0; }
        }

        public bool IsEnabledLoteDeInventario {
            get { return Master.Action == eAccionSR.Custom; }
        }

        public FkLoteDeInventarioViewModel ConexionLoteDeInventario {
            get {
                return _ConexionLoteDeInventario;
            }
            set {
                if (_ConexionLoteDeInventario != value) {
                    _ConexionLoteDeInventario = value;                    
                    if (_ConexionLoteDeInventario != null) {
                        CodigoLote = _ConexionLoteDeInventario.CodigoLote;
                        FechaDeElaboracion = _ConexionLoteDeInventario.FechaDeElaboracion;
                        FechaDeVencimiento = _ConexionLoteDeInventario.FechaDeVencimiento;
                        ConsecutivoLoteDeInventario = _ConexionLoteDeInventario.Consecutivo;
                    }
                    RaisePropertyLote();
                }
            }
        }

        public bool IsEnabledMerma {
            get { return Master.Action == eAccionSR.Cerrar && (MermaTotalInsumos >= 0); }
        }

        public bool IsVisibleCantidadMermaNormal {
            get { return Master.ListaUsaMerma && (Master.Action == eAccionSR.Cerrar); }
        }

        public bool IsVisibleCantidadMermaAnormal {
            get { return Master.ListaUsaMerma && (Master.Action == eAccionSR.Cerrar); }
        }
        #endregion //Propiedades
        #region Constructores
        public OrdenDeProduccionDetalleMaterialesViewModel()
            : base(new OrdenDeProduccionDetalleMateriales(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public OrdenDeProduccionDetalleMaterialesViewModel(OrdenDeProduccionViewModel initMaster, OrdenDeProduccionDetalleMateriales initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(OrdenDeProduccionDetalleMateriales valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseLoteDeInventarioCommand = new RelayCommand<string>(ExecuteChooseLoteDeInventarioCommand);
        }
        protected override ILibBusinessDetailComponent<IList<OrdenDeProduccionDetalleMateriales>, IList<OrdenDeProduccionDetalleMateriales>> GetBusinessComponent() {
            return new clsOrdenDeProduccionDetalleMaterialesNav();
        }

        #endregion //Metodos Generados

        public override void InitializeViewModel(eAccionSR valAction) {
            if (valAction == eAccionSR.Cerrar) {
                CantidadConsumida = CantidadReservadaInventario;
                CantMermaNormal();
                CalcularPorcentajeMermaNormal();
                CantMermaAnormal();
                CalcularPorcentajeMermaAnormal();
            }
            base.InitializeViewModel(valAction);
        }

        public override void InitializeViewModel(eAccionSR valAction, string valCustomAction) {
            if (valAction == eAccionSR.Custom) {
                base.InitializeViewModel(valAction, "Iniciar");
            } else {
                base.InitializeViewModel(valAction, valCustomAction);
            }
        }
        protected override bool RecordIsReadOnly() {
            return Master.IsReadOnly;
        }


        private ValidationResult CantidadReservadaInventarioValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Master.Action == eAccionSR.Insertar || Master.Action == eAccionSR.Modificar) && CantidadReservadaInventario < 0) {
                return new ValidationResult("La Cantidad a Consumir debe ser mayor igual a 0.");
            } else {
                return vResult;
            }
        }

        private ValidationResult ValidatingCantidadConsumida() {
            ValidationResult vResult = ValidationResult.Success;
            if (Master.Action == eAccionSR.Cerrar && CantidadConsumida < 0) {
                return new ValidationResult("La Cantidad Consumida debe ser mayor igual a 0.");
            } else {
                return vResult;
            }
        }

        private ValidationResult CantidadMermaNormalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (Master.ListaUsaMerma) {
                if (Master.Action == eAccionSR.Cerrar && CantidadMermaNormal < 0) {
                    return new ValidationResult("La Cantidad Merma Normal debe ser mayor igual a 0.");
                }
            } else {
                PorcentajeMermaNormalOriginal = 0;
                CantidadMermaNormal = 0;
                PorcentajeMermaNormal = 0;
                CantidadMermaAnormal = 0;
                PorcentajeMermaAnormal = 0;
            }
            return vResult;
        }

        private ValidationResult CantidadMermaAnormalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (Master.ListaUsaMerma) {
                if (Master.Action == eAccionSR.Cerrar && CantidadMermaAnormal < 0) {
                    return new ValidationResult("La Cantidad Merma Anormal debe ser mayor igual a 0.");
                } else {
                    return vResult;
                }
            } else {
                PorcentajeMermaNormalOriginal = 0;
                CantidadMermaNormal = 0;
                PorcentajeMermaNormal = 0;
                CantidadMermaAnormal = 0;
                PorcentajeMermaAnormal = 0;
                return vResult;
            }
        }


        private void ExecuteChooseLoteDeInventarioCommand(string valCodigoLote) {
            try {
                if (valCodigoLote == null) {
                    valCodigoLote = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("CodigoLote", valCodigoLote);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("CodigoArticulo", CodigoArticulo), eLogicOperatorType.And);
                if (!LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "PermitirSobregiro")) {                    
                    vFixedCriteria.Add("Existencia", eBooleanOperatorType.GreaterThan, 0, eLogicOperatorType.And);
                }                
                var ConexionLoteDeInventarioTmp = Master.ChooseRecord<FkLoteDeInventarioViewModel>("Lote de Inventario", vDefaultCriteria, vFixedCriteria, "FechaDeVencimiento, FechaDeElaboracion, CodigoLote");
                if (ConexionLoteDeInventarioTmp == null) {
                    CodigoLote = string.Empty;
                    FechaDeElaboracion = LibDate.MinDateForDB();
                    FechaDeVencimiento = LibDate.MaxDateForDB();
                } else {
                    if (Master.DetailOrdenDeProduccionDetalleMateriales.Items != null &&
                        Master.DetailOrdenDeProduccionDetalleMateriales.Items.Count(p => p.ConsecutivoLoteDeInventario == ConexionLoteDeInventarioTmp.Consecutivo) > 0) {
                        LibMessages.MessageBox.Error(this, $"El Artículo: {ConexionLoteDeInventarioTmp.CodigoArticulo} - {LibString.Left(DescripcionArticulo, 15) + "..."} Lote: {ConexionLoteDeInventarioTmp.CodigoLote} ya está ingresado en la lista.", ModuleName);
                        return;
                    }
                    ConexionLoteDeInventario = ConexionLoteDeInventarioTmp;
                    if (TipoArticuloInvAsEnum  == eTipoArticuloInv.LoteFechadeVencimiento && LibDate.F1IsLessThanF2(ConexionLoteDeInventario.FechaDeVencimiento, LibDate.Today())) {
                        LibMessages.MessageBox.Information(this, $"El Artículo: {CodigoArticulo} - {LibString.Left(DescripcionArticulo, 15) + "..."} Lote: {ConexionLoteDeInventario.CodigoLote} venció el {ConexionLoteDeInventario.FechaDeVencimiento.ToString("dd/MM/yyyy")}.", ModuleName);
                    }
                    if ((ConexionLoteDeInventario.Existencia < CantidadReservadaInventario)
                        && (!LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "PermitirSobregiro"))) {
                        AgregarNuevoDetalle();
                    }
                    ConsecutivoLoteDeInventario = ConexionLoteDeInventario.Consecutivo;
                    CodigoLote  = ConexionLoteDeInventario.CodigoLote;
                    FechaDeElaboracion = ConexionLoteDeInventario.FechaDeElaboracion;
                    FechaDeVencimiento = ConexionLoteDeInventario.FechaDeVencimiento;
                    Existencia = ConexionLoteDeInventario.Existencia;
                    RaisePropertyChanged(() => Existencia);
                }
                RaisePropertyChanged(() => IsVisbleLoteDeInventario);                
                RaisePropertyChanged(() => IsVisibleFechaLoteDeInventario);                
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void AgregarNuevoDetalle() {
            CantidadReservadaInventario = ConexionLoteDeInventario.Existencia;
            var CantidadReservadaEnLista = Master.DetailOrdenDeProduccionDetalleMateriales.Items.Where(p => p.CodigoArticulo == CodigoArticulo).Sum(q => q.CantidadReservadaInventario);
            var newModel = new OrdenDeProduccionDetalleMateriales() {
                ConsecutivoCompania = ConsecutivoCompania,
                ConsecutivoOrdenDeProduccion = ConsecutivoOrdenDeProduccion,
                ConsecutivoAlmacen = ConsecutivoAlmacen,
                CodigoAlmacen = CodigoAlmacen,
                NombreAlmacen = NombreAlmacen,
                CodigoArticulo = CodigoArticulo,
                DescripcionArticulo = DescripcionArticulo,
                UnidadDeVenta = UnidadDeVenta,
                Cantidad = Cantidad,
                CantidadReservadaInventario = (Cantidad * Master.CantidadAProducir) - CantidadReservadaEnLista,
                CantidadConsumida = CantidadConsumida,
                CostoUnitarioArticuloInventario = CostoUnitarioArticuloInventario,
                MontoSubtotal = MontoSubtotal,
                AjustadoPostCierreAsBool = AjustadoPostCierre,
                CantidadAjustada = CantidadAjustada,
                TipoDeArticuloAsEnum = TipoDeArticulo,
                TipoArticuloInvAsEnum = TipoArticuloInvAsEnum,
                CodigoLote = "",
                ConsecutivoLoteDeInventario = 0,
                FechaDeElaboracion = LibDate.MinDateForDB(),
                FechaDeVencimiento = LibDate.MinDateForDB(),
                PorcentajeMermaNormalOriginal = 0,
                CantidadMermaNormal = 0,
                PorcentajeMermaNormal = 0,
                CantidadMermaAnormal = 0,
                PorcentajeMermaAnormal= 0,
            };
            var newViewModel = new OrdenDeProduccionDetalleMaterialesViewModel(Master, newModel, Action);
            Master.DetailOrdenDeProduccionDetalleMateriales.Items.Add(newViewModel);
        }

        private void RaisePropertyLote() {
            RaisePropertyChanged(() => IsVisbleLoteDeInventario);            
            RaisePropertyChanged(() => IsVisibleFechaLoteDeInventario);            
        }

        public void CantMermaNormal() {            
            if (MermaTotalInsumos >= 0) {
                CantidadMermaNormal = LibMath.RoundToNDecimals(CantidadReservadaInventario * (PorcentajeMermaNormalOriginal / 100), 8);
            } else {
                CantidadMermaNormal = 0;
            }
            RaisePropertyChanged(CantidadMermaNormalPropertyName);
            RaisePropertyChanged(() => IsEnabledMerma);
        }

        public void CalcularPorcentajeMermaNormal() {
            if (MermaTotalInsumos >= 0) {
                PorcentajeMermaNormal = LibMath.RoundToNDecimals(CantidadMermaNormal * 100 / CantidadReservadaInventario, 8);
            } else {
                PorcentajeMermaNormal = 0;
            }
            RaisePropertyChanged(() => IsEnabledMerma);
        }

        public void CantMermaAnormal() {
            if (MermaTotalInsumos >= 0 && (MermaTotalInsumos - CantidadMermaNormal) > 0) {
                CantidadMermaAnormal = LibMath.RoundToNDecimals(MermaTotalInsumos - CantidadMermaNormal, 8);
            } else {
                CantidadMermaAnormal = 0;
            }
            RaisePropertyChanged(CantidadMermaAnormalPropertyName);
            RaisePropertyChanged(() => IsEnabledMerma);
        }

        public void CalcularPorcentajeMermaAnormal() {
            if (MermaTotalInsumos >= 0 && (MermaTotalInsumos - CantidadMermaNormal) > 0) {
                PorcentajeMermaAnormal = LibMath.RoundToNDecimals(CantidadMermaAnormal * 100 / CantidadReservadaInventario, 8);
            } else {
                PorcentajeMermaAnormal = 0;
            }
            RaisePropertyChanged(() => IsEnabledMerma);
        }

        decimal MermaTotalInsumos { get { return CantidadConsumida - CantidadReservadaInventario; } }

    } //End of class OrdenDeProduccionDetalleMaterialesViewModel

} //End of namespace Galac.Adm.Uil.GestionProduccion

