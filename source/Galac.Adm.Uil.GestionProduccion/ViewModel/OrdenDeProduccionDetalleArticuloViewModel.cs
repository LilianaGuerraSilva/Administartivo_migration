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
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.Brl;
using Galac.Saw.Brl.Inventario;

namespace Galac.Adm.Uil.GestionProduccion.ViewModel {
    public class OrdenDeProduccionDetalleArticuloViewModel : LibInputDetailViewModelMfc<OrdenDeProduccionDetalleArticulo> {

        #region Constantes
        private const string ConsecutivoLoteDeInventarioPropertyName = "ConsecutivoLoteDeInventario";
        private const string CodigoAlmacenPropertyName = "CodigoAlmacen";
        private const string NombreAlmacenPropertyName = "NombreAlmacen";
        private const string CodigoArticuloPropertyName = "CodigoArticulo";
        private const string DescripcionArticuloPropertyName = "DescripcionArticulo";
        private const string UnidadDeVentaPropertyName = "UnidadDeVenta";
        private const string CantidadOriginalListaPropertyName = "CantidadOriginalLista";
        private const string CantidadSolicitadaPropertyName = "CantidadSolicitada";
        private const string CantidadProducidaPropertyName = "CantidadProducida";
        private const string AjustadoPostCierrePropertyName = "AjustadoPostCierre";
        private const string CantidadAjustadaPropertyName = "CantidadAjustada";
        private const string PorcentajeCostoEstimadoPropertyName = "PorcentajeCostoEstimado";
        private const string PorcentajeCostoCierrePropertyName = "PorcentajeCostoCierre";
        private const string CostoPropertyName = "Costo";
        private decimal _TotalPorcentajeCosto;
        private const string PorcentajeMermaNormalOriginalPropertyName = "PorcentajeMermaNormalOriginal";
        private const string CantidadMermaNormalPropertyName = "CantidadMermaNormal";
        private const string PorcentajeMermaNormalPropertyName = "PorcentajeMermaNormal";
        private const string CantidadMermaAnormalPropertyName = "CantidadMermaAnormal";
        private const string PorcentajeMermaAnormalPropertyName = "PorcentajeMermaAnormal";
        #endregion

        #region Variables
        private FkLoteDeInventarioViewModel _ConexionLoteDeInventario = null;
        #endregion //Variables

        #region Propiedades

        public override string ModuleName {
            get { return "Salidas"; }
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

        [LibGridColum("Unidad", eGridColumType.Generic, Width = 80, ColumnOrder = 2)]
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

        [LibGridColum("Cant. Original en Lista", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ConditionalPropertyDecimalDigits = "DecimalDigits", ColumnOrder = 3, Width =130)]
        public decimal CantidadOriginalLista {
            get {
                return Model.CantidadOriginalLista;
            }
            set {
                if (Model.CantidadOriginalLista != value) {
                    Model.CantidadOriginalLista = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadOriginalListaPropertyName);
                }
            }
        }


        [LibGridColum("Cant. Solicitada", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ConditionalPropertyDecimalDigits = "DecimalDigits", ColumnOrder = 4, Width =100)]
        public decimal CantidadSolicitada {
            get {
                return Model.CantidadSolicitada;
            }
            set {
                if (Model.CantidadSolicitada != value) {
                    Model.CantidadSolicitada = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadSolicitadaPropertyName);
                }
            }
        }

        [LibCustomValidation("PorcentajeCostoEstimadoValidating")]
        [LibGridColum("% Costo Est.", eGridColumType.Numeric, ConditionalPropertyDecimalDigits = "DecimalDigits", Alignment = eTextAlignment.Right, ColumnOrder = 5, Width =110)]
        public decimal PorcentajeCostoEstimado {
            get {
                return Model.PorcentajeCostoEstimado;
            }
            set {
                if (Model.PorcentajeCostoEstimado != value) {
                    Model.PorcentajeCostoEstimado = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeCostoEstimadoPropertyName);
                    Master.ActualizaTotalProcentajeDeCosto();
                }
            }
        }


        [LibCustomValidation("CantidadProducidaValidating")]
        [LibGridColum("Cant. Producida", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ConditionalPropertyDecimalDigits = "DecimalDigits", ColumnOrder = 6, Width =110)]
        public decimal CantidadProducida {
            get {
                return Model.CantidadProducida;
            }
            set {
                if (Model.CantidadProducida != value) {
                    Model.CantidadProducida = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadProducidaPropertyName);
                    CantMermaNormal();
                    CalcularPorcentajeMermaNormal();
                    CantMermaAnormal();
                    CalcularPorcentajeMermaAnormal();
                }
            }
        }

        [LibCustomValidation("PorcentajeCostoCierreValidating")]
        [LibGridColum("% Costo Cierre", eGridColumType.Numeric, ConditionalPropertyDecimalDigits = "DecimalDigits", Alignment = eTextAlignment.Right, ColumnOrder = 7, Width =100)]
        public decimal PorcentajeCostoCierre {
            get {
                return Model.PorcentajeCostoCierre;
            }
            set {
                if (Model.PorcentajeCostoCierre != value) {
                    Model.PorcentajeCostoCierre = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeCostoCierrePropertyName);
                    Master.ActualizaTotalProcentajeDeCosto();
                }
            }
        }

        [LibGridColum("Costo Unitario", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ConditionalPropertyDecimalDigits = "2", ColumnOrder = 10, Width =120)]
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



        [LibGridColum("Costo", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ColumnOrder = 11)]
        public decimal Costo {
            get {
                return Model.Costo;
            }
            set {
                if (Model.Costo != value) {
                    Model.Costo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CostoPropertyName);
                }
            }
        }
		
        public decimal MontoSubTotal {
            get {
                return Model.MontoSubTotal;
            }
            set {
                if (Model.MontoSubTotal != value) {
                    Model.MontoSubTotal = value;
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

        public bool IsVisiblePorcentajeCostoEstimado {
            get { return ((Master.Action == eAccionSR.Insertar) || (Master.Action == eAccionSR.Modificar)); }
        }

        public bool IsVisiblePorcentajeCostoCierre {
            get { return (Master.Action == eAccionSR.Cerrar); }
        }
		
        public bool IsVisibleCantidadProducida {
            get { return (Master.Action == eAccionSR.Cerrar); }
        }

        protected override bool RecordIsReadOnly() {
            return Master.IsReadOnly;
        }

        public int DecimalDigits {
            get {
                return Master == null ? 8 : Master.DecimalDigits;
            }
        }

        public bool IsEnabledPorcentajeCostoEstimado {
            get { return Master.Action == eAccionSR.Insertar ||  Master.Action == eAccionSR.Modificar; }
        }

        public bool IsEnabledPorcentajeCostoCierre {
            get { return Master.Action == eAccionSR.Cerrar; }
        }

        public bool IsEnabledCantidadProducida {
            get { return Master.Action == eAccionSR.Cerrar; }
        }

        [LibGridColum("Lote", MaxWidth = 120, ColumnOrder = 1)]
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
                if (Model.TipoArticuloInvAsEnum != value) {
                    Model.TipoArticuloInvAsEnum = value;
                    IsDirty = true;
                }
            }
        }

        public bool IsVisbleLoteDeInventario {
            get {
                return ((Master.Action == eAccionSR.Cerrar) ||( Master.Action == eAccionSR.Consultar && Master.StatusOp == eTipoStatusOrdenProduccion.Cerrada))
                    && (TipoArticuloInvAsEnum == eTipoArticuloInv.Lote 
                    || TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeVencimiento
                    || TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeElaboracion);
            }
        }

        public bool IsVisibleFechaLoteDeInventario {
            get { return IsVisbleLoteDeInventario && TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeVencimiento && ConsecutivoLoteDeInventario != 0; }
        }

        public bool IsEnabledLoteDeInventario {
            get { return Master.Action == eAccionSR.Cerrar; }
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
		
        public decimal  PorcentajeMermaNormalOriginal {
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
        [LibGridColum("Cant. Merma Normal", eGridColumType.Numeric, ColumnOrder = 8, ConditionalPropertyDecimalDigits = "DecimalDigits", Alignment = eTextAlignment.Right, Width = 130)]
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

        public bool IsVisibleCantidadMermaNormal {
            get { return Master.IsVisibleListaUsaMerma && (Master.Action == eAccionSR.Cerrar); }
        }

        public bool IsVisibleCantidadMermaAnormal {
            get { return Master.IsVisibleListaUsaMerma && (Master.Action == eAccionSR.Cerrar); }
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
        [LibGridColum("Cant. Merma Anormal", eGridColumType.Numeric, ColumnOrder = 9, Alignment = eTextAlignment.Right, Width = 130, ConditionalPropertyDecimalDigits = "DecimalDigits")]
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

        public bool IsEnabledMerma {
            get { return Master.Action == eAccionSR.Cerrar && (MermaTotalSalida >= 0); }
        }

        #endregion //Propiedades

        #region Constructores e Inicializadores

        public OrdenDeProduccionDetalleArticuloViewModel()
            : base(new OrdenDeProduccionDetalleArticulo(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public OrdenDeProduccionDetalleArticuloViewModel(OrdenDeProduccionViewModel initMaster, OrdenDeProduccionDetalleArticulo initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
        }

        protected override void InitializeLookAndFeel(OrdenDeProduccionDetalleArticulo valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        public override void InitializeViewModel(eAccionSR valAction) {
            if (valAction == eAccionSR.Cerrar) {
                CantidadProducida = CantidadSolicitada;
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

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseLoteDeInventarioCommand = new RelayCommand<string>(ExecuteChooseLoteDeInventarioCommand);
        }
        #endregion //Constructores e Inicializadores

        #region Metodos Generados

        protected override ILibBusinessDetailComponent<IList<OrdenDeProduccionDetalleArticulo>, IList<OrdenDeProduccionDetalleArticulo>> GetBusinessComponent() {
            return new clsOrdenDeProduccionDetalleArticuloNav();
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
        }
        #endregion //Metodos Generados

        #region Metodos        
        #endregion //Metodos

        private ValidationResult CantidadProducidaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (Master.Action == eAccionSR.Cerrar && CantidadProducida < 0) {
                return new ValidationResult("La Cantidad Producida debe ser mayor o igual a cero.");
            } else {
                return vResult;
            }
        }

        private ValidationResult PorcentajeCostoEstimadoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Master.Action == eAccionSR.Insertar || Master.Action == eAccionSR.Modificar) && (PorcentajeCostoEstimado < 0 || PorcentajeCostoEstimado > 100)) {
                return new ValidationResult("El % Costo Estimado debe ser mayor o igual a cero y menor o igual a 100.");
            } else {
                return vResult;
            }
        }

        private ValidationResult PorcentajeCostoCierreValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (Master.Action == eAccionSR.Cerrar && (PorcentajeCostoCierre < 0 || PorcentajeCostoCierre > 100)) {
                return new ValidationResult("El % Costo al Cierre debe ser mayor o igual a cero y menor o igual a 100.");
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

        private void ExecuteChooseLoteDeInventarioCommand(string valCodigoLote) {
            try {
                if (valCodigoLote == null) {
                    valCodigoLote = string.Empty;
                }
                bool vInvocarCrear = true;
                vInvocarCrear = vInvocarCrear && !LibString.IsNullOrEmpty(valCodigoLote, true);
                vInvocarCrear = vInvocarCrear && !LibString.S1IsInS2("*", valCodigoLote);
                vInvocarCrear = vInvocarCrear && !((ILoteDeInventarioPdn)new clsLoteDeInventarioNav()).ExisteLoteDeInventario(Mfc.GetInt("Compania"), CodigoArticulo, valCodigoLote);
                if (vInvocarCrear) {
                    LibBusinessProcessMessage libBusinessProcessMessage = new LibBusinessProcessMessage();
                    libBusinessProcessMessage.Content = valCodigoLote + "|" + CodigoArticulo + "|" + (int)TipoArticuloInvAsEnum;
                    LibBusinessProcess.Call("InsertarLoteInventarioDesdeModuloExterno", libBusinessProcessMessage);
                    valCodigoLote = libBusinessProcessMessage.Result.ToString();
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("CodigoLote", valCodigoLote);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("CodigoArticulo", CodigoArticulo), eLogicOperatorType.And);
                ConexionLoteDeInventario = Master.ChooseRecord<FkLoteDeInventarioViewModel>("Lote de Inventario", vDefaultCriteria, vFixedCriteria, "FechaDeVencimiento, FechaDeElaboracion, CodigoLote");
                if (ConexionLoteDeInventario == null) {
                    CodigoLote = string.Empty;
                    FechaDeElaboracion = LibDate.MinDateForDB();
                    FechaDeVencimiento = LibDate.MaxDateForDB();
                } else {
                    if (TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeVencimiento && LibDate.F1IsLessThanF2(ConexionLoteDeInventario.FechaDeVencimiento, LibDate.Today())) {
                        LibMessages.MessageBox.Information(this, $"El Articulo:{CodigoArticulo} - {LibString.Left(DescripcionArticulo, 15) + "..."} Lote: {ConexionLoteDeInventario.CodigoLote} venció el {ConexionLoteDeInventario.FechaDeVencimiento.ToString("dd/MM/yyyy")}.", ModuleName);
                    }
                    ConsecutivoLoteDeInventario = ConexionLoteDeInventario.Consecutivo;
                    CodigoLote = ConexionLoteDeInventario.CodigoLote;
                    FechaDeElaboracion = ConexionLoteDeInventario.FechaDeElaboracion;
                    FechaDeVencimiento = ConexionLoteDeInventario.FechaDeVencimiento;
                }
                RaisePropertyChanged(() => IsVisbleLoteDeInventario);
                RaisePropertyChanged(() => IsVisibleFechaLoteDeInventario);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void RaisePropertyLote() {
            RaisePropertyChanged(() => IsVisbleLoteDeInventario);
            RaisePropertyChanged(() => IsVisibleFechaLoteDeInventario);
        }

        public void CantMermaNormal() {
            if (MermaTotalSalida >= 0) {
                CantidadMermaNormal = LibMath.RoundToNDecimals(CantidadSolicitada * (PorcentajeMermaNormalOriginal / 100),8);
            } else {
                CantidadMermaNormal = 0;
            }
            RaisePropertyChanged(CantidadMermaNormalPropertyName);
            RaisePropertyChanged(() => IsEnabledMerma);
        }

        public void CalcularPorcentajeMermaNormal() {
            if (MermaTotalSalida >= 0) {
                PorcentajeMermaNormal = LibMath.RoundToNDecimals(CantidadMermaNormal * (100 / CantidadSolicitada), 8);
            } else {
                PorcentajeMermaNormal = 0;
            }
            RaisePropertyChanged(() => IsEnabledMerma);
        }

        public void CantMermaAnormal() {
            if (MermaTotalSalida >= 0 && (MermaTotalSalida - CantidadMermaNormal) > 0) {
                CantidadMermaAnormal = LibMath.RoundToNDecimals(MermaTotalSalida - CantidadMermaNormal, 8);
            } else {
                CantidadMermaAnormal = 0;
            }
            RaisePropertyChanged(CantidadMermaAnormalPropertyName);
            RaisePropertyChanged(() => IsEnabledMerma);
        }

        public void CalcularPorcentajeMermaAnormal() {
            if (MermaTotalSalida >= 0 && (MermaTotalSalida - CantidadMermaNormal) > 0) {
                PorcentajeMermaAnormal = LibMath.RoundToNDecimals(CantidadMermaAnormal * (100 / CantidadSolicitada), 8);
            } else {
                PorcentajeMermaAnormal = 0;
            }
            RaisePropertyChanged(() => IsEnabledMerma);
        }

        decimal MermaTotalSalida { get { return CantidadSolicitada - CantidadProducida; } }

    } //End of class OrdenDeProduccionDetalleArticuloViewModel

} //End of namespace Galac.Adm.Uil. GestionProduccion