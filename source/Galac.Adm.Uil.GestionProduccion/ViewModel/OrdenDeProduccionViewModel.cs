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
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Brl.TablasGen;
using Galac.Comun.Uil.TablasGen.ViewModel;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Uil.GestionProduccion.ViewModel {
    public class OrdenDeProduccionViewModel : LibInputMasterViewModelMfc<OrdenDeProduccion> {

        #region Constantes

        private const string ConsecutivoPropertyName = "Consecutivo";
        private const string CodigoPropertyName = "Codigo";
        private const string DescripcionPropertyName = "Descripcion";
        private const string StatusOpPropertyName = "StatusOp";
        private const string CodigoAlmacenProductoTerminadoPropertyName = "CodigoAlmacenProductoTerminado";
        private const string NombreAlmacenProductoTerminadoPropertyName = "NombreAlmacenProductoTerminado";
        private const string CodigoAlmacenMaterialesPropertyName = "CodigoAlmacenMateriales";
        private const string NombreAlmacenMaterialesPropertyName = "NombreAlmacenMateriales";
        private const string FechaCreacionPropertyName = "FechaCreacion";
        private const string FechaInicioPropertyName = "FechaInicio";
        private const string FechaFinalizacionPropertyName = "FechaFinalizacion";
        private const string FechaAnulacionPropertyName = "FechaAnulacion";
        private const string FechaAjustePropertyName = "FechaAjuste";
        private const string AjustadaPostCierrePropertyName = "AjustadaPostCierre";
        private const string ObservacionPropertyName = "Observacion";
        private const string MotivoDeAnulacionPropertyName = "MotivoDeAnulacion";
        private const string CostoTerminadoCalculadoAPartirDePropertyName = "CostoTerminadoCalculadoAPartirDe";
        private const string MonedaPropertyName = "Moneda";
        private const string LabelFormaDeCalcularCostosDeProduccionPropertyName = "LabelFormaDeCalcularCostosDeProduccion";
        private const string CodigoMonedaCostoProduccionPropertyName = "CodigoMonedaCostoProduccion";
        private const string CambioCostoProduccionPropertyName = "CambioCostoProduccion";
        private const string NombreOperadorPropertyName = "NombreOperador";
        private const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        public const string IsVisibleAlContabilizarPropertyName = "IsVisibleAlContabilizar";

        #endregion

        #region Variables

        private FkAlmacenViewModel _ConexionCodigoAlmacenProductoTerminado = null;
        private FkAlmacenViewModel _ConexionCodigoAlmacenMateriales = null;
        private FkMonedaViewModel _ConexionMoneda = null;
        private Saw.Lib.clsNoComunSaw vMonedaLocal = null;
        private bool _IsVisibleAlContabilizar;
        private FkOrdenDeProduccionViewModel _ConexionCodigoOrdenProduccion;

        #endregion //Variables

        #region Propiedades

        public override string ModuleName {
            get { return "Orden de Producción"; }
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

        public int Consecutivo {
            get {
                return Model.Consecutivo;
            }
            set {
                if (Model.Consecutivo != value) {
                    Model.Consecutivo = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConsecutivoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Código de Orden es requerido.")]
        [LibGridColum("Código de Orden", MaxLength = 15, ColumnOrder = 0, DbMemberPath = "Gv_OrdenDeProduccion_B1.Codigo")]
        public string Codigo {
            get {
                return Model.Codigo;
            }
            set {
                if (Model.Codigo != value) {
                    Model.Codigo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Descripción es requerido.")]
        [LibGridColum("Descripción", MaxLength = 200, Trimming = System.Windows.TextTrimming.WordEllipsis, Width = 350, ColumnOrder = 1, DbMemberPath = "Gv_OrdenDeProduccion_B1.Descripcion")]
        public string Descripcion {
            get {
                return Model.Descripcion;
            }
            set {
                if (Model.Descripcion != value) {
                    Model.Descripcion = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionPropertyName);
                }
            }
        }

        [LibGridColum("Estado", eGridColumType.Enum, PrintingMemberPath = "StatusOpStr", ColumnOrder = 2, Width = 70)]
        public eTipoStatusOrdenProduccion StatusOp {
            get {
                return Model.StatusOpAsEnum;
            }
            set {
                if (Model.StatusOpAsEnum != value) {
                    Model.StatusOpAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(StatusOpPropertyName);
                }
            }
        }

        public int ConsecutivoAlmacenProductoTerminado {
            get {
                return Model.ConsecutivoAlmacenProductoTerminado;
            }
            set {
                if (Model.ConsecutivoAlmacenProductoTerminado != value) {
                    Model.ConsecutivoAlmacenProductoTerminado = value;
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Código de Almacén Producto Terminado es requerido.")]
        public string CodigoAlmacenProductoTerminado {
            get {
                return Model.CodigoAlmacenProductoTerminado;
            }
            set {
                if (Model.CodigoAlmacenProductoTerminado != value) {
                    Model.CodigoAlmacenProductoTerminado = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoAlmacenProductoTerminadoPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoAlmacenProductoTerminado, true)) {
                        ConexionCodigoAlmacenProductoTerminado = null;
                    }
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nombre de Almacén Producto Terminado es requerido.")]
        public string NombreAlmacenProductoTerminado {
            get {
                return Model.NombreAlmacenProductoTerminado;
            }
            set {
                if (Model.NombreAlmacenProductoTerminado != value) {
                    Model.NombreAlmacenProductoTerminado = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreAlmacenProductoTerminadoPropertyName);
                    if (LibString.IsNullOrEmpty(NombreAlmacenProductoTerminado, true)) {

                    }
                }
            }
        }

        public int ConsecutivoAlmacenMateriales {
            get {
                return Model.ConsecutivoAlmacenMateriales;
            }
            set {
                if (Model.ConsecutivoAlmacenMateriales != value) {
                    Model.ConsecutivoAlmacenMateriales = value;
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Código de Almacén Materiales es requerido.")]
        public string CodigoAlmacenMateriales {
            get {
                return Model.CodigoAlmacenMateriales;
            }
            set {
                if (Model.CodigoAlmacenMateriales != value) {
                    Model.CodigoAlmacenMateriales = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoAlmacenMaterialesPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoAlmacenMateriales, true)) {
                        ConexionCodigoAlmacenMateriales = null;
                    }
                }
            }
        }
        
        [LibRequired(ErrorMessage = "El campo Nombre de Almacén Materiales es requerido.")]
        public string NombreAlmacenMateriales {
            get {
                return Model.NombreAlmacenMateriales;
            }
            set {
                if (Model.NombreAlmacenMateriales != value) {
                    Model.NombreAlmacenMateriales = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreAlmacenMaterialesPropertyName);
                    if (LibString.IsNullOrEmpty(NombreAlmacenMateriales, true)) {

                    }
                }
            }
        }

        [LibCustomValidation("FechaCreacionValidating")]
        [LibGridColum("Fecha de Creación", eGridColumType.DatePicker, ColumnOrder = 3)]
        public DateTime FechaCreacion {
            get {
                return Model.FechaCreacion;
            }
            set {
                if (Model.FechaCreacion != value) {
                    Model.FechaCreacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaCreacionPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaInicioValidating")]
        [LibGridColum("Fecha de Inicio", eGridColumType.DatePicker, ColumnOrder = 4)]
        public DateTime FechaInicio {
            get {
                return Model.FechaInicio;
            }
            set {
                if (Model.FechaInicio != value) {
                    Model.FechaInicio = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaInicioPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaFinalizacionValidating")]
        [LibGridColum("Fecha de Finalización", eGridColumType.DatePicker, ColumnOrder = 5, Width = 130)]
        public DateTime FechaFinalizacion {
            get {
                return Model.FechaFinalizacion;
            }
            set {
                if (Model.FechaFinalizacion != value) {
                    Model.FechaFinalizacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaFinalizacionPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaAnulacionValidating")]
        public DateTime FechaAnulacion {
            get {
                return Model.FechaAnulacion;
            }
            set {
                if (Model.FechaAnulacion != value) {
                    Model.FechaAnulacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaAnulacionPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaAjusteValidating")]
        public DateTime FechaAjuste {
            get {
                return Model.FechaAjuste;
            }
            set {
                if (Model.FechaAjuste != value) {
                    Model.FechaAjuste = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaAjustePropertyName);
                }
            }
        }

        public bool AjustadaPostCierre {
            get {
                return Model.AjustadaPostCierreAsBool;
            }
            set {
                if (Model.AjustadaPostCierreAsBool != value) {
                    Model.AjustadaPostCierreAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(AjustadaPostCierrePropertyName);
                }
            }
        }

        public string Observacion {
            get {
                return Model.Observacion;
            }
            set {
                if (Model.Observacion != value) {
                    Model.Observacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(ObservacionPropertyName);
                }
            }
        }

        [LibCustomValidation("MotivoDeAnulacionValidating")]
        public string MotivoDeAnulacion {
            get {
                return Model.MotivoDeAnulacion;
            }
            set {
                if (Model.MotivoDeAnulacion != value) {
                    Model.MotivoDeAnulacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(MotivoDeAnulacionPropertyName);
                }
            }
        }
        public string Moneda {
            get {
                return Model.Moneda;
            }
            set {
                if (Model.Moneda != value) {
                    Model.Moneda = value;
                    IsDirty = true;
                    RaisePropertyChanged(MonedaPropertyName);
                    RaisePropertyChanged(LabelFormaDeCalcularCostosDeProduccionPropertyName);
                    if (LibString.IsNullOrEmpty(Moneda, true)) {
                        ConexionMoneda = null;
                    }
                }
            }
        }

        public eFormaDeCalcularCostoTerminado CostoTerminadoCalculadoAPartirDe {
            get {
                return Model.CostoTerminadoCalculadoAPartirDeAsEnum;
            }
            set {
                if (Model.CostoTerminadoCalculadoAPartirDeAsEnum != value) {
                    Model.CostoTerminadoCalculadoAPartirDeAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(CostoTerminadoCalculadoAPartirDePropertyName);
                }
            }
        }

        public string LabelFormaDeCalcularCostosDeProduccion {
            get {
                if (CostoTerminadoCalculadoAPartirDe == eFormaDeCalcularCostoTerminado.APartirDeCostoEnMonedaLocal || !UsaMonedaExtranjera()) {
                    return "Costo de los materiales en Bolívares";
                } else {
                    return "Costo de los materiales en " + Moneda;
                }
            }
        }

        public string CodigoMonedaCostoProduccion {
            get {
                return Model.CodigoMonedaCostoProduccion;
            }
            set {
                if (Model.CodigoMonedaCostoProduccion != value) {
                    Model.CodigoMonedaCostoProduccion = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoMonedaCostoProduccionPropertyName);
                }
            }
        }

        public string CodigoMoneda { get; set; }

        public decimal CambioCostoProduccion {
            get {
                return Model.CambioCostoProduccion;
            }
            set {
                if (Model.CambioCostoProduccion != value) {
                    Model.CambioCostoProduccion = value;
                    IsDirty = true;
                    RaisePropertyChanged(CambioCostoProduccionPropertyName);
                }
            }
        }

        public decimal CambioMoneda { get; set; }

        public string NombreOperador {
            get {
                return Model.NombreOperador;
            }
            set {
                if (Model.NombreOperador != value) {
                    Model.NombreOperador = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreOperadorPropertyName);
                }
            }
        }

        public DateTime FechaUltimaModificacion {
            get {
                return Model.FechaUltimaModificacion;
            }
            set {
                if (Model.FechaUltimaModificacion != value) {
                    Model.FechaUltimaModificacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaUltimaModificacionPropertyName);
                }
            }
        }

        public eTipoStatusOrdenProduccion[] ArrayTipoStatusOrdenProduccion {
            get {
                return LibEnumHelper<eTipoStatusOrdenProduccion>.GetValuesInArray();
            }
        }

        //[LibDetailRequired(ErrorMessage = "Orden De Produccion Detalle Articulo es requerido.")]
        public OrdenDeProduccionDetalleArticuloMngViewModel DetailOrdenDeProduccionDetalleArticulo {
            get;
            set;
        }

        public FkAlmacenViewModel ConexionCodigoAlmacenProductoTerminado {
            get {
                return _ConexionCodigoAlmacenProductoTerminado;
            }
            set {
                if (_ConexionCodigoAlmacenProductoTerminado != value) {
                    _ConexionCodigoAlmacenProductoTerminado = value;
                    RaisePropertyChanged(CodigoAlmacenProductoTerminadoPropertyName);
                    Model.ConsecutivoAlmacenProductoTerminado = ConexionCodigoAlmacenProductoTerminado.Consecutivo;
                    CodigoAlmacenProductoTerminado = ConexionCodigoAlmacenProductoTerminado.Codigo;
                    NombreAlmacenProductoTerminado = ConexionCodigoAlmacenProductoTerminado.NombreAlmacen;
                    ActualizaAlmacenenProductoTerminadoEnDetalles();
                }
                if (_ConexionCodigoAlmacenProductoTerminado == null) {
                    CodigoAlmacenProductoTerminado = string.Empty;
                }
            }
        }

        public FkAlmacenViewModel ConexionCodigoAlmacenMateriales {
            get {
                return _ConexionCodigoAlmacenMateriales;
            }
            set {
                if (_ConexionCodigoAlmacenMateriales != value) {
                    _ConexionCodigoAlmacenMateriales = value;
                    RaisePropertyChanged(CodigoAlmacenMaterialesPropertyName);
                    Model.ConsecutivoAlmacenMateriales = ConexionCodigoAlmacenMateriales.Consecutivo;
                    CodigoAlmacenMateriales = ConexionCodigoAlmacenMateriales.Codigo;
                    NombreAlmacenMateriales = ConexionCodigoAlmacenMateriales.NombreAlmacen;
                    ActualizaAlmacenenMaterialesEnDetalles();
                }
                if (_ConexionCodigoAlmacenMateriales == null) {
                    CodigoAlmacenMateriales = string.Empty;
                }
            }
        }

        public FkMonedaViewModel ConexionMoneda {
            get {
                return _ConexionMoneda;
            }
            set {
                if (_ConexionMoneda != value) {
                    _ConexionMoneda = value;
                    RaisePropertyChanged(MonedaPropertyName);
                }
                if (_ConexionMoneda == null) {
                    Moneda = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoAlmacenProductoTerminadoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoAlmacenMaterialesCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseMonedaCommand {
            get;
            private set;
        }

        public RelayCommand<string> CreateOrdenDeProduccionDetalleArticuloCommand {
            get { return DetailOrdenDeProduccionDetalleArticulo.CreateCommand; }
        }

        public RelayCommand<string> UpdateOrdenDeProduccionDetalleArticuloCommand {
            get { return DetailOrdenDeProduccionDetalleArticulo.UpdateCommand; }
        }

        public RelayCommand<string> DeleteOrdenDeProduccionDetalleArticuloCommand {
            get { return DetailOrdenDeProduccionDetalleArticulo.DeleteCommand; }
        }

        public RelayCommand VerDetalleCommand {
            get;
            private set;
        }

        public bool IsVisibleAlmacen {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaAlmacen");
            }
        }

        public bool IsVisibleFechaDeInicio {
            get {
                return StatusOp != eTipoStatusOrdenProduccion.Ingresada;
            }
        }

        public bool IsEnabledFechaDeInicio {
            get {
                return StatusOp != eTipoStatusOrdenProduccion.Ingresada && Action == eAccionSR.Custom;
            }
        }

        public bool IsVisibleFechaDeAnulacion {
            get {
                return StatusOp == eTipoStatusOrdenProduccion.Anulada;
            }
        }

        public bool IsEnabledFechaDeAnulacion {
            get {
                return StatusOp == eTipoStatusOrdenProduccion.Anulada && Action == eAccionSR.Anular;
            }
        }

        public bool IsEReadOnlyMotivoDeAnulacion {
            get {
                return !IsEnabledFechaDeAnulacion;
            }
        }

        public bool IsEnabledFechaFinalizacion {
            get {
                return StatusOp == eTipoStatusOrdenProduccion.Cerrada && Action == eAccionSR.Cerrar;
            }
        }

        public bool IsEnabledCambioCostoProduccion {
            get {
                return (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar || Action == eAccionSR.Custom || Action == eAccionSR.Cerrar);
            }
        }

        public bool IsEnabledNombreMonedaProduccion {
            get {
                return (Action == eAccionSR.Insertar);
            }
        }

        public bool IsVisibleFechaFinalizacion {
            get {
                return StatusOp == eTipoStatusOrdenProduccion.Cerrada;
            }
        }

        public bool IsVisibleCambioCostoProduccion {
            get {
                return UsaMonedaExtranjera() && !EsEcuador();
            }
        }

        protected override bool RecordIsReadOnly() {
            return base.RecordIsReadOnly() || Action == eAccionSR.Custom || Action == eAccionSR.Anular || Action == eAccionSR.Cerrar || Action == eAccionSR.Contabilizar;
        }

        public bool IsReadOnlyObservacion {
            get {
                return Action != eAccionSR.Cerrar && Action != eAccionSR.Insertar && Action != eAccionSR.Modificar;
            }
        }

        public int DecimalDigits {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales");
            }
        }
        public bool IsVisibleEscogerNumeroOrdenProduccion {
            get {
                return Action == eAccionSR.Contabilizar;
            }
        }

        public bool IsVisibleEditarNumeroDocumento {
            get {
                return !IsVisibleEscogerNumeroOrdenProduccion;
            }
        }

        public bool IsVisibleAlContabilizar {
            get {
                return _IsVisibleAlContabilizar;
            }
            set {
                _IsVisibleAlContabilizar = value;
            }
        }

        public RelayCommand<string> ChooseCodigoOrdenProduccionCommand { 
            get; 
            private set; 
        }

        #endregion //Propiedades

        #region Constructores e Inicializadores

        public OrdenDeProduccionViewModel()
            : this(new OrdenDeProduccion(), eAccionSR.Insertar) {
        }

        public OrdenDeProduccionViewModel(OrdenDeProduccion initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = CodigoPropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            if (initAction == eAccionSR.Custom) {
                CustomActionLabel = "Iniciar";
            }
            vMonedaLocal = new Saw.Lib.clsNoComunSaw();
            InitializeDetails();
        }

        public OrdenDeProduccionViewModel(eAccionSR initAction)
            : base(new OrdenDeProduccion(), initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = CodigoPropertyName;
            InitializeDetails();
        }

        public override void InitializeViewModel(eAccionSR valAction) {
            base.InitializeViewModel(valAction);
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            if (LibText.IsNullOrEmpty(Model.Codigo) && valAction != eAccionSR.Cerrar && valAction != eAccionSR.Anular && valAction != eAccionSR.ReImprimir && valAction != eAccionSR.Contabilizar) {
                Model.Codigo = GenerarProximoCodigo();
            }
            if (Action != eAccionSR.Contabilizar) {
                if (Action == eAccionSR.Insertar) {
                    if (DetailOrdenDeProduccionDetalleArticulo.Items.Count() == 0) {
                        DetailOrdenDeProduccionDetalleArticulo.CreateCommand.Execute(null);
                    }
                    RaisePropertyChanged("DetailOrdenDeProduccionDetalleArticulo");
                    FechaCreacion = LibDefGen.DateForInitializeInputValue();
                    if (!LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaAlmacen")) {
                        LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Saw.Gv_Almacen_B1.Codigo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoAlmacenPorDefecto"));
                        vDefaultCriteria.Add(LibSearchCriteria.CreateCriteria("Saw.Gv_Almacen_B1.ConsecutivoCompania", Mfc.GetInt("Compania")), eLogicOperatorType.And);
                        ConexionCodigoAlmacenMateriales = FirstConnectionRecordOrDefault<FkAlmacenViewModel>("Almacén", vDefaultCriteria);
                        ConexionCodigoAlmacenProductoTerminado = FirstConnectionRecordOrDefault<FkAlmacenViewModel>("Almacén", vDefaultCriteria);
                        ActualizaAlmacenenProductoTerminadoEnDetalles();
                        ActualizaAlmacenenMaterialesEnDetalles();
                    }
                    VerDetalleCommand.RaiseCanExecuteChanged();
                }
                RaisePropertyChanged("DecimalDigits");
                if (Action == eAccionSR.Insertar) {
                    CodigoMonedaCostoProduccion = AsignarCodigoDeLaMonedaAlInsertar();
                    CostoTerminadoCalculadoAPartirDe = (eFormaDeCalcularCostoTerminado)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CostoTerminadoCalculadoAPartirDe"));
                }

                if (Action == eAccionSR.Anular || Action == eAccionSR.Consultar || Action == eAccionSR.Eliminar) {
                    Moneda = AsignarNombreMoneda().Nombre;
                } else if (UsaMonedaExtranjera()) {
                    AsignarValoresDeMonedaPorDefecto();
                    AsignarCambioCostoDeProduccion();
                } else if (!CalculaCostosAPartirDeMonedaExtranjera()) {
                    CodigoMoneda = vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today());
                    Moneda = vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(LibDate.Today());
                    CambioMoneda = 1;
                }
            }
        }

        public override void InitializeViewModel(eAccionSR valAction, string valCustomAction) {
            if (valAction == eAccionSR.Custom) {
                base.InitializeViewModel(valAction, "Iniciar");
                StatusOp = eTipoStatusOrdenProduccion.Iniciada;
                FechaInicio = LibDefGen.DateForInitializeInputValue();
                FechaAjuste = LibDefGen.DateForInitializeInputValue();
                FechaFinalizacion = LibDefGen.DateForInitializeInputValue();
                FechaAnulacion = LibDefGen.DateForInitializeInputValue();
            } else {
                base.InitializeViewModel(valAction, valCustomAction);
                if (valAction == eAccionSR.Anular) {
                    StatusOp = eTipoStatusOrdenProduccion.Anulada;
                } else if (valAction == eAccionSR.Cerrar) {
                    StatusOp = eTipoStatusOrdenProduccion.Cerrada;
                    FechaFinalizacion = LibDefGen.DateForInitializeInputValue();
                }
            }
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoAlmacenProductoTerminadoCommand = new RelayCommand<string>(ExecuteChooseCodigoAlmacenProductoTerminadoCommand);
            ChooseCodigoAlmacenMaterialesCommand = new RelayCommand<string>(ExecuteChooseCodigoAlmacenMaterialesCommand);
            VerDetalleCommand = new RelayCommand(ExecuteVerDetalleCommand, CanExecuteVerDetalleCommand);
            ChooseMonedaCommand = new RelayCommand<string>(ExecuteChooseMonedaCommand);
            if (Action == eAccionSR.Contabilizar) {
                ChooseCodigoOrdenProduccionCommand = new RelayCommand<string>(ExecuteChooseCodigoOrdenProduccionCommand);
            }
        }       

        protected override void InitializeLookAndFeel(OrdenDeProduccion valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override void InitializeDetails() {
            DetailOrdenDeProduccionDetalleArticulo = new OrdenDeProduccionDetalleArticuloMngViewModel(this, Model.DetailOrdenDeProduccionDetalleArticulo, Action);
            DetailOrdenDeProduccionDetalleArticulo.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleArticuloViewModel>>(DetailOrdenDeProduccionDetalleArticulo_OnCreated);
            DetailOrdenDeProduccionDetalleArticulo.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleArticuloViewModel>>(DetailOrdenDeProduccionDetalleArticulo_OnUpdated);
            DetailOrdenDeProduccionDetalleArticulo.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleArticuloViewModel>>(DetailOrdenDeProduccionDetalleArticulo_OnDeleted);
            DetailOrdenDeProduccionDetalleArticulo.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleArticuloViewModel>>(DetailOrdenDeProduccionDetalleArticulo_OnSelectedItemChanged);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection.Add(CreateAccionRibbonGroup());
        }

        private LibRibbonButtonData CreateAccionRibbonGroup() {
            LibRibbonButtonData vResult = new LibRibbonButtonData() {
                Label = Action == eAccionSR.Insertar || Action == eAccionSR.Modificar ? "Editar Detalle" : "Ver Detalle",
                Command = VerDetalleCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/report.png", UriKind.Relative),
                ToolTipDescription = "Detalle",
                ToolTipTitle = "Detalle"
            };
            return vResult;
        }

        #endregion //Constructores e Inicializadores

        #region Commands

        private bool CanExecuteVerDetalleCommand() {

            return DetailOrdenDeProduccionDetalleArticulo.Items.Count > 0 && DetailOrdenDeProduccionDetalleArticulo.Items[0].DetailOrdenDeProduccionDetalleMateriales != null && DetailOrdenDeProduccionDetalleArticulo.Items[0].DetailOrdenDeProduccionDetalleMateriales.Items != null && DetailOrdenDeProduccionDetalleArticulo.Items[0].DetailOrdenDeProduccionDetalleMateriales.Items.Count > 0;
        }

        private void ExecuteVerDetalleCommand() {
            try {
                OrdenDeProduccionDetalleArticuloViewModel vViewModel = DetailOrdenDeProduccionDetalleArticulo.Items[0];
                vViewModel.BuscaExistencia();
                vViewModel.InicializarRibbon();
                LibMessages.EditViewModel.ShowEditor(vViewModel, true);
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCodigoAlmacenProductoTerminadoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Almacen_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_Almacen_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCodigoAlmacenProductoTerminado = ChooseRecord<FkAlmacenViewModel>("Almacén", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoAlmacenProductoTerminado != null) {
                    Model.ConsecutivoAlmacenProductoTerminado = ConexionCodigoAlmacenProductoTerminado.Consecutivo;
                    CodigoAlmacenProductoTerminado = ConexionCodigoAlmacenProductoTerminado.Codigo;
                    NombreAlmacenProductoTerminado = ConexionCodigoAlmacenProductoTerminado.NombreAlmacen;
                    ActualizaAlmacenenProductoTerminadoEnDetalles();
                } else {
                    Model.ConsecutivoAlmacenProductoTerminado = 0;
                    CodigoAlmacenProductoTerminado = string.Empty;
                    NombreAlmacenProductoTerminado = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCodigoAlmacenMaterialesCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Almacen_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_Almacen_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCodigoAlmacenMateriales = ChooseRecord<FkAlmacenViewModel>("Almacén", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoAlmacenMateriales != null) {
                    Model.ConsecutivoAlmacenMateriales = ConexionCodigoAlmacenMateriales.Consecutivo;
                    CodigoAlmacenMateriales = ConexionCodigoAlmacenMateriales.Codigo;
                    NombreAlmacenMateriales = ConexionCodigoAlmacenMateriales.NombreAlmacen;
                    ActualizaAlmacenenMaterialesEnDetalles();
                } else {
                    Model.ConsecutivoAlmacenMateriales = 0;
                    CodigoAlmacenMateriales = string.Empty;
                    NombreAlmacenMateriales = string.Empty;
                }
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        private void ExecuteChooseMonedaCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                XElement vXmlMonedaLocales = ((IMonedaLocalPdn)new clsMonedaLocalProcesos()).BusquedaTodasLasMonedasLocales(LibDefGen.ProgramInfo.Country);
                IList<MonedaLocalActual> vListaDeMonedaLocales = new List<MonedaLocalActual>();
                vListaDeMonedaLocales = vXmlMonedaLocales != null ? LibParserHelper.ParseToList<MonedaLocalActual>(new XDocument(vXmlMonedaLocales)) : null;
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Moneda_B1.Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Activa", LibConvert.BoolToSN(true));
                vFixedCriteria.Add("TipoDeMoneda", eBooleanOperatorType.IdentityEquality, eTipoDeMoneda.Fisica);
                if (vListaDeMonedaLocales != null && !LibDefGen.ProgramInfo.IsCountryEcuador()) {
                    foreach (MonedaLocalActual vMoneda in vListaDeMonedaLocales) {
                        vFixedCriteria.Add("Codigo", eBooleanOperatorType.IdentityInequality, vMoneda.CodigoMoneda);
                    }
                }
                ConexionMoneda = null;
                ConexionMoneda = ChooseRecord<FkMonedaViewModel>("Moneda", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionMoneda != null) {
                    Moneda = ConexionMoneda.Nombre;
                    CodigoMonedaCostoProduccion = ConexionMoneda.Codigo;
                    bool vConsultarAntesDeAsignarCambio = true;
                    AsignarValoresDeMonedaPorDefecto();
                    AsignarCambioCostoDeProduccion(vConsultarAntesDeAsignarCambio);
                } else {
                    Moneda = string.Empty;
                    CodigoMonedaCostoProduccion = string.Empty;
                }
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        protected override bool CanExecuteAction() {
            if (Action == eAccionSR.Cerrar || Action == eAccionSR.Anular || Action == eAccionSR.ReImprimir || Action == eAccionSR.Contabilizar) {
                return !LibText.IsNullOrEmpty(Model.Codigo);
            } else {
                return base.CanExecuteAction();
            }
        }
        #endregion

        #region Validation

        private ValidationResult FechaCreacionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (Action != eAccionSR.Cerrar) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaCreacion, false, eAccionSR.Insertar)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de Creación"));
                }
            }
            return vResult;
        }

        private ValidationResult FechaInicioValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action != eAccionSR.Custom)) {
                return ValidationResult.Success;
            } else {
                if (Action == eAccionSR.Custom) {
                    if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaInicio, false, eAccionSR.Modificar)) {
                        vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de Inicio"));
                    }
                }
            }
            return vResult;
        }

        private ValidationResult FechaFinalizacionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action != eAccionSR.Cerrar)) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaFinalizacion, false, eAccionSR.Modificar)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de Finalización"));
                }
            }
            return vResult;
        }

        private ValidationResult FechaAnulacionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action != eAccionSR.Anular)) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaAnulacion, false, eAccionSR.Modificar)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de Anulación"));
                }
            }
            return vResult;
        }

        private ValidationResult FechaAjusteValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar || Action == eAccionSR.Contabilizar)) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaAjuste, false, Action)) {
                    // vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de Ajuste"));
                }
            }
            return vResult;
        }
        private ValidationResult MotivoDeAnulacionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Contabilizar)) {
                return ValidationResult.Success;
            } else {
                if (Action == eAccionSR.Anular) {
                    if (LibString.IsNullOrEmpty(MotivoDeAnulacion)) {
                        vResult = new ValidationResult("El campo Motivo de anulación es requerido.");

                    }
                }
            }
            return vResult;
        }
        #endregion //Validation

        #region Metodos Generados

        protected override OrdenDeProduccion FindCurrentRecord(OrdenDeProduccion valModel) {            
            if (valModel == null) {
                return null;
            }
            if (Action == eAccionSR.Contabilizar) {
                return valModel;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "OrdenDeProduccionGET", vParams.Get(), UseDetail).FirstOrDefault();
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<OrdenDeProduccion>, IList<OrdenDeProduccion>> GetBusinessComponent() {
            return new clsOrdenDeProduccionNav();
        }

        private string GenerarProximoCodigo() {
            string vResult = string.Empty;
            XElement vResulset = GetBusinessComponent().QueryInfo(eProcessMessageType.Message, "ProximoCodigo", Mfc.GetIntAsParam("Compania"), false);
            vResult = LibXml.GetPropertyString(vResulset, "Codigo");
            return vResult;
        }

        #region OrdenDeProduccionDetalleArticulo

        private void DetailOrdenDeProduccionDetalleArticulo_OnSelectedItemChanged(object sender, SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleArticuloViewModel> e) {
            try {
                UpdateOrdenDeProduccionDetalleArticuloCommand.RaiseCanExecuteChanged();
                DeleteOrdenDeProduccionDetalleArticuloCommand.RaiseCanExecuteChanged();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailOrdenDeProduccionDetalleArticulo_OnDeleted(object sender, SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleArticuloViewModel> e) {
            try {
                IsDirty = true;
                Model.DetailOrdenDeProduccionDetalleArticulo.Remove(e.ViewModel.GetModel());
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailOrdenDeProduccionDetalleArticulo_OnUpdated(object sender, SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleArticuloViewModel> e) {
            try {
                IsDirty = e.ViewModel.IsDirty;
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailOrdenDeProduccionDetalleArticulo_OnCreated(object sender, SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleArticuloViewModel> e) {
            try {
                Model.DetailOrdenDeProduccionDetalleArticulo.Add(e.ViewModel.GetModel());
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        #endregion //OrdenDeProduccionDetalleArticulo

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            //ConexionCodigoAlmacenProductoTerminado = FirstConnectionRecordOrDefault<FkAlmacenViewModel>("Almacén", LibSearchCriteria.CreateCriteria("Codigo", CodigoAlmacenProductoTerminado));
            //ConexionCodigoAlmacenMateriales = FirstConnectionRecordOrDefault<FkAlmacenViewModel>("Almacén", LibSearchCriteria.CreateCriteria("Codigo", CodigoAlmacenMateriales));
        }

        #endregion //Metodos Generados

        #region Metodos

        private void ActualizaAlmacenenProductoTerminadoEnDetalles() {
            foreach (OrdenDeProduccionDetalleArticulo vItem in Model.DetailOrdenDeProduccionDetalleArticulo) {
                vItem.ConsecutivoAlmacen = Model.ConsecutivoAlmacenProductoTerminado;
            }

        }

        internal void ActualizaAlmacenenMaterialesEnDetalles() {
            foreach (OrdenDeProduccionDetalleArticulo vItem in Model.DetailOrdenDeProduccionDetalleArticulo) {
                foreach (OrdenDeProduccionDetalleMateriales vDetail in vItem.DetailOrdenDeProduccionDetalleMateriales) {
                    vDetail.ConsecutivoAlmacen = Model.ConsecutivoAlmacenMateriales;
                }
            }
        }

        protected override void ExecuteProcessBeforeAction() {
            if (Action == eAccionSR.Custom) {
                DetailOrdenDeProduccionDetalleArticulo.Items[0].BuscaExistencia();
                if (DetailOrdenDeProduccionDetalleArticulo.Items
                    .Where(p => p.DetailOrdenDeProduccionDetalleMateriales.Items
                    .Where(q => q.TipoDeArticulo == Saw.Ccl.Inventario.eTipoDeArticulo.Mercancia && q.Existencia < q.CantidadReservadaInventario).Count() > 0).Count() > 0) {
                    if (!LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "PermitirSobregiro")) {
                        throw new GalacValidationException("No hay suficiente existencia de algunos materiales para producir este inventario.");
                    }
                }
            }
        }

        protected override bool ExecuteSpecialAction(string valCustomAction) {
            if (LibString.Equals(valCustomAction, "Iniciar")) {
                IList<OrdenDeProduccion> vList = new List<OrdenDeProduccion>();
                vList.Add(Model);
                DialogResult = GetBusinessComponent().DoAction(vList, Action, null, true).Success;
            }else if (LibString.Equals(valCustomAction, "Contabilizar")) {
                DialogResult = true;
            }
            CloseOnActionComplete = true;
            return true;
        }

        protected override void ExecuteSpecialAction(eAccionSR valAction) {
            IList<OrdenDeProduccion> vList = new List<OrdenDeProduccion>();
            vList.Add(Model);
            if (valAction == eAccionSR.Anular || valAction == eAccionSR.Cerrar) {
                DialogResult = GetBusinessComponent().DoAction(vList, Action, null, true).Success;
            } else if (valAction == eAccionSR.Contabilizar) {
                DialogResult = true;
            }
            CloseOnActionComplete = true;
        }

        public FkOrdenDeProduccionViewModel ConexionCodigoOrdenProduccion {
            get {
                return _ConexionCodigoOrdenProduccion;
            }
            set {
                if (_ConexionCodigoOrdenProduccion != value) {
                    _ConexionCodigoOrdenProduccion = value;
                    RaisePropertyChanged(CodigoPropertyName);
                    if (ConexionCodigoOrdenProduccion != null) {
                        Codigo = ConexionCodigoOrdenProduccion.Codigo;
                        StatusOp = ConexionCodigoOrdenProduccion.StatusOP;
                        FechaAnulacion = ConexionCodigoOrdenProduccion.FechaAnulacion;
                        FechaFinalizacion = ConexionCodigoOrdenProduccion.FechaFinalizacion;
                        RaisePropertyChanged(CodigoPropertyName);
                        RaisePropertyChanged(StatusOpPropertyName);
                        RaisePropertyChanged(FechaAnulacionPropertyName);
                        RaisePropertyChanged(FechaFinalizacionPropertyName);
                        RaisePropertyChanged(IsVisibleAlContabilizarPropertyName);
                    }
                }
                if (_ConexionCodigoOrdenProduccion == null) {
                    Codigo = string.Empty;
                }
                ExecuteActionCommand.RaiseCanExecuteChanged();
            }
        }

        private void CambioChanged(decimal valCambio) {
            CambioMoneda = valCambio;
        }

        private bool AsignaTasaDelDia(string valCodigoMoneda, DateTime valFecha) {
            vMonedaLocal.InstanceMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            if (!EsMonedaLocal(valCodigoMoneda)) {
                ConexionMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigoMoneda));
                CodigoMoneda = ConexionMoneda.Codigo;
                if (((ICambioPdn)new clsCambioNav()).ExisteTasaDeCambioParaElDia(CodigoMoneda, valFecha, out decimal vTasa)) {
                    CambioMoneda = vTasa;
                    return true;
                } else {
                    CambioViewModel vViewModel = new CambioViewModel(valCodigoMoneda, false, 100, false);
                    vViewModel.InitializeViewModel(eAccionSR.Insertar);
                    vViewModel.OnCambioAMonedaLocalChanged += CambioChanged;
                    vViewModel.FechaDeVigencia = valFecha;
                    vViewModel.CodigoMoneda = CodigoMoneda;
                    vViewModel.NombreMoneda = ConexionMoneda.Nombre;
                    vViewModel.IsEnabledFecha = false;
                    vViewModel.IsEnabledMoneda = false;
                    bool vResult = LibMessages.EditViewModel.ShowEditor(vViewModel, true);
                    if (!vResult) {
                        if (UsaDivisaComoMonedaPrincipalDeIngresoDeDatos()) {
                            return false;
                        }
                        if (Action == eAccionSR.Cerrar) {
                            CambioMoneda = CambioCostoProduccion;
                        } else {
                            CambioMoneda = 1;
                        }
                    }
                    return true;
                }
            } else {
                CodigoMoneda = vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(valFecha);
                Moneda = vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(valFecha);
                CambioMoneda = 1;
                return true;
            }
        }

        private static bool UsaDivisaComoMonedaPrincipalDeIngresoDeDatos() {
            return LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaDivisaComoMonedaPrincipalDeIngresoDeDatos"));
        }

        private void AsignarCambioCostoDeProduccion(bool valConsultarAntesDeAsignarCambio = false) {
            CodigoMonedaCostoProduccion = CodigoMoneda;
            if (Action == eAccionSR.Insertar) {
                CambioCostoProduccion = CambioMoneda;
            } else if (CambioMoneda != CambioCostoProduccion) {
                if (LibMessages.MessageBox.YesNo(this, "La tasa de cambio del día (" + LibConvert.NumToString(CambioMoneda, 2) + vMonedaLocal.InstanceMonedaLocalActual.SimboloMoneda(LibDate.Today()) + ".) es distinta a la de esta Orden de Producción (" + LibConvert.NumToString(CambioCostoProduccion, 2) + vMonedaLocal.InstanceMonedaLocalActual.SimboloMoneda(LibDate.Today()) + ".) ¿Desea cambiar la tasa por la de hoy?", ModuleName)) {
                    CambioCostoProduccion = CambioMoneda;
                }
            }
        }

        private void AsignarValoresDeMonedaPorDefecto() {
            DateTime vFecha = LibDate.Today();
            AsignarNombreMoneda();
            AsignaTasaDelDia(CodigoMoneda, vFecha);
        }

        protected override void ExecuteAction() {
            if (Action == eAccionSR.Cerrar) {
                if (FechaFinalizacion < FechaInicio) {
                    LibMessages.MessageBox.Alert(this, "La fecha de finalización de la Orden de Producción no puede ser anterior a su fecha de inicio.", ModuleName);
                } else if (FechaFinalizacion > LibDate.Today()) {
                    LibMessages.MessageBox.Alert(this, "La fecha de finalización de la Orden de Producción no puede ser superior a la fecha de hoy, por favor ingrese una fecha válida.", ModuleName);
                } else if (LibMessages.MessageBox.YesNo(this, "¿Está seguro de cerrar esta Orden de Producción?", ModuleName)) {
                    base.ExecuteAction();
                }
            } else if (Action == eAccionSR.Anular && FechaAnulacion < FechaInicio) {
                LibMessages.MessageBox.Alert(this, "La fecha de anulación de la Orden de Producción no puede ser anterior a su fecha de inicio.", ModuleName);
            } else {
                base.ExecuteAction();
            }
        }

        private bool EsEcuador() {
            return LibDefGen.ProgramInfo.IsCountryEcuador();
        }

        private bool EsMonedaLocal(string valCodigoMoneda) {
            return vMonedaLocal.InstanceMonedaLocalActual.EsMonedaLocalDelPais(valCodigoMoneda);
        }

        private bool UsaMonedaExtranjera() {
            return LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaMonedaExtranjera"));
        }
        private bool CalculaCostosAPartirDeMonedaExtranjera() {
            eFormaDeCalcularCostoTerminado vFormaDeCalcularCostoTerminado = (eFormaDeCalcularCostoTerminado)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CostoTerminadoCalculadoAPartirDe"));
            return (vFormaDeCalcularCostoTerminado == eFormaDeCalcularCostoTerminado.APartirDeCostoEnMonedaExtranjera);
        }

        private string AsignarCodigoDeLaMonedaAlInsertar() {
            string vCodigo = "VED";
            if (UsaMonedaExtranjera()) {
                vCodigo = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
            }
            return vCodigo;
        }

        private FkMonedaViewModel AsignarNombreMoneda() {
            ConexionMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", CodigoMonedaCostoProduccion));
            CodigoMoneda = ConexionMoneda.Codigo;
            Moneda = ConexionMoneda.Nombre;
            return ConexionMoneda;
        }

        private void ExecuteChooseCodigoOrdenProduccionCommand(string valNumero) {
            string vModuleName = "Orden de Producción";
            try {
                if (valNumero == null) {
                    valNumero = string.Empty;
                }
                LibSearchCriteria vSearchcriteria = null;
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_OrdenDeProduccion_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                if (Action == LibGalac.Aos.Base.eAccionSR.Contabilizar) {
                    vSearchcriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_OrdenDeProduccion_B1.Consecutivo", valNumero);
                    vModuleName = "ContabilizarOrdenDeProduccion";
                }
                ConexionCodigoOrdenProduccion = ChooseRecord<FkOrdenDeProduccionViewModel>(vModuleName, vSearchcriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoOrdenProduccion == null) {
                    Codigo = "";
                } else {
                    Model.ConsecutivoCompania = ConexionCodigoOrdenProduccion.ConsecutivoCompania;
                    Model.Consecutivo = ConexionCodigoOrdenProduccion.Consecutivo;
                    Model.StatusOp = LibConvert.EnumToDbValue((int)ConexionCodigoOrdenProduccion.StatusOP);
                    InitializeViewModel(Action);
                    GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly)
                      .ToList().ForEach(p => RaisePropertyChanged(p.Name));
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //Metodos
    } //End of class OrdenDeProduccionViewModel

} //End of namespace Galac.Adm.Uil.GestionProduccion