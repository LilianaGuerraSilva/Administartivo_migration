using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Ccl.GestionCompras;
using LibGalac.Aos.Brl;
using System.ComponentModel;
using Galac.Adm.Uil.GestionCompras.Reportes;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Brl.TablasGen;
using Galac.Comun.Uil.TablasGen.ViewModel;
using Galac.Saw.Lib;

namespace Galac.Adm.Uil.GestionCompras.ViewModel {
    public class OrdenDeCompraViewModel : LibInputMasterViewModelMfc<OrdenDeCompra> {

        #region Constantes

        private const string SeriePropertyName = "Serie";
        private const string NumeroPropertyName = "Numero";
        private const string FechaPropertyName = "Fecha";
        private const string ConsecutivoProveedorPropertyName = "ConsecutivoProveedor";
        private const string CodigoProveedorPropertyName = "CodigoProveedor";
        private const string NombreProveedorPropertyName = "NombreProveedor";
        private const string MonedaPropertyName = "Moneda";
        private const string CodigoMonedaPropertyName = "CodigoMoneda";
        private const string CambioAMonedaLocalPropertyName = "CambioAMonedaLocal";
        private const string TotalRenglonesPropertyName = "TotalRenglones";
        private const string TotalCompraPropertyName = "TotalCompra";
        private const string TipoDeCompraPropertyName = "TipoDeCompra";
        private const string ComentariosPropertyName = "Comentarios";
        private const string StatusOrdenDeCompraPropertyName = "StatusOrdenDeCompra";
        private const string FechaDeAnulacionPropertyName = "FechaDeAnulacion";
        private const string CondicionesDeEntregaPropertyName = "CondicionesDeEntrega";
        private const string CondicionesDePagoPropertyName = "CondicionesDePago";
        private const string DescripcionCondicionesDePagoPropertyName = "DescripcionCondicionesDePago";
        private const string CondicionesDeImportacionPropertyName = "CondicionesDeImportacion";
        private const string NumeroCotizacionPropertyName = "NumeroCotizacion";
        private const string NombreOperadorPropertyName = "NombreOperador";
        private const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        private const string IsEnabledCambioPropertyName = "IsEnabledCambio";
        private const string MonedaActualPropertyName = "MonedaActual";
        private const string IsVisibleMonedaActualPropertyName = "IsVisibleMonedaActual";
        private const string IsEnableSeriePropertyName = "IsEnableSerie";
        private const string IsEnableNumeroPropertyName = "IsEnableNumero";
        private const string IsVisibleCondicionesImportacionPropertyName = "IsVisibleCondicionesImportacion";

        #endregion

        #region Variables

        private FkProveedorViewModel _ConexionConsecutivoProveedor = null;
        private FkProveedorViewModel _ConexionCodigoProveedor = null;
        private FkProveedorViewModel _ConexionNombreProveedor = null;
        private FkCotizacionViewModel _ConexionNumeroCotizacion = null;
        private FkCondicionesDePagoViewModel _ConexionCondicionesDePago = null;
        private FkCondicionesDePagoViewModel _ConexionDescripcionCondicionesDePago = null;
        private FkMonedaViewModel _ConexionCodigoMoneda = null;
        private bool _IsEnableNumero = true;
        private bool _IsEnableSerie = true;
        private Saw.Lib.clsNoComunSaw vMonedaLocal = null;

        #endregion //Variables

        #region Propiedades

        internal eTipoCompra TipoModulo { get; set; }

        public override string ModuleName {
            get {
                if (TipoModulo == eTipoCompra.Importacion) {
                    return "Orden De Compra Importación";
                }
                return "Orden De Compra Nacional";
            }
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
                }
            }
        }
        [LibCustomValidation("SerieValidating")]
        [LibGridColum("Serie", MaxLength = 20)]
        public string Serie {
            get {
                return Model.Serie;
            }
            set {
                if (Model.Serie != value) {
                    Model.Serie = value;
                    IsDirty = true;
                    RaisePropertyChanged(SeriePropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Numero es requerido.")]
        [LibGridColum("Número", MaxLength = 20)]
        public string Numero {
            get {
                return Model.Numero;
            }
            set {
                if (Model.Numero != value) {
                    Model.Numero = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Fecha es requerido.")]
        [LibCustomValidation("FechaValidating")]
        [LibGridColum("Fecha", MaxLength = 20, Type = eGridColumType.DatePicker, BindingStringFormat = "dd/MM/yyyy")]
        public DateTime Fecha {
            get {
                return Model.Fecha;
            }
            set {
                if (Model.Fecha != value) {
                    Model.Fecha = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaPropertyName);
                    RaisePropertyChanged(MonedaActualPropertyName);
                    RaisePropertyChanged("UsaBolivarFuerte");
                    AsignaTasaDelDia(CodigoMoneda);
                }
            }
        }

        public int ConsecutivoProveedor {
            get {
                return Model.ConsecutivoProveedor;
            }
            set {
                if (Model.ConsecutivoProveedor != value) {
                    Model.ConsecutivoProveedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConsecutivoProveedorPropertyName);
                    if (ConsecutivoProveedor == 0) {
                        ConexionCodigoProveedor = null;
                    }
                }
            }
        }
        [LibRequired(ErrorMessage = "El campo Código del Proveedor es requerido.")]
        //[LibGridColum("Código del Proveedor", eGridColumType.Connection, ConnectionDisplayMemberPath = "Adm.Gv_Proveedor_B1.CodigoProveedor", ConnectionModelPropertyName = "Adm.Gv_Proveedor_B1.CodigoProveedor", ConnectionSearchCommandName = "ChooseCodigoProveedorCommand", Width = 180)]
        public string CodigoProveedor {
            get {
                return Model.CodigoProveedor;
            }
            set {
                if (Model.CodigoProveedor != value) {
                    Model.CodigoProveedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoProveedorPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoProveedor, true)) {
                        ConexionCodigoProveedor = null;
                    }
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nombre Proveedor es requerido.")]
        [LibGridColum("Nombre Proveedor", eGridColumType.Connection, ConnectionDisplayMemberPath = "nombreProveedor", ConnectionModelPropertyName = "Adm.Gv_Proveedor_B1.NombreProveedor", ConnectionSearchCommandName = "ChooseNombreProveedorCommand", Width = 250, DbMemberPath = "Adm.Gv_Proveedor_B1.NombreProveedor")]
        public string NombreProveedor {
            get {
                return Model.NombreProveedor;
            }
            set {
                if (Model.NombreProveedor != value) {
                    Model.NombreProveedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreProveedorPropertyName);
                    if (LibString.IsNullOrEmpty(NombreProveedor, true)) {
                        ConexionNombreProveedor = null;
                    }
                }
            }
        }

        public string NumeroCotizacion {
            get {
                return Model.NumeroCotizacion;
            }
            set {
                if (Model.NumeroCotizacion != value) {
                    Model.NumeroCotizacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroCotizacionPropertyName);
                    if (LibString.IsNullOrEmpty(NumeroCotizacion, true)) {
                        ConexionNumeroCotizacion = null;
                    }
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
                    RaisePropertyChanged(IsEnabledCambioPropertyName);
                    RaisePropertyChanged(IsVisibleMonedaActualPropertyName);


                }
            }
        }

        public string CodigoMoneda {
            get {
                return Model.CodigoMoneda;
            }
            set {
                if (Model.CodigoMoneda != value) {
                    Model.CodigoMoneda = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoMonedaPropertyName);
                }
            }
        }
        [LibRequired(ErrorMessageResourceName = "CambioAMonedaLocalRequired", ErrorMessageResourceType = typeof(Properties.Resources))]
        public decimal CambioAMonedaLocal {
            get {
                return LibMath.RoundToNDecimals(Model.CambioABolivares, LibDefGen.ProgramInfo.IsCountryPeru() ? 3 : 4);
            }
            set {
                if (Model.CambioABolivares != value) {
                    Model.CambioABolivares = value;
                    IsDirty = true;
                    RaisePropertyChanged(CambioAMonedaLocalPropertyName);
                }
            }
        }

        public int DecimalesTasaDeCambio {
            get;
            set;
        }

        public decimal TotalRenglones {
            get {
                return Model.TotalRenglones;
            }
            set {
                if (Model.TotalRenglones != value) {
                    Model.TotalRenglones = value;
                    IsDirty = true;
                    RaisePropertyChanged(TotalRenglonesPropertyName);
                }
            }
        }

        public decimal TotalCompra {
            get {
                return Model.TotalCompra;
            }
            set {
                if (Model.TotalCompra != value) {
                    Model.TotalCompra = value;
                    IsDirty = true;
                    RaisePropertyChanged(TotalCompraPropertyName);
                }
            }
        }

        public string Comentarios {
            get {
                return Model.Comentarios;
            }
            set {
                if (Model.Comentarios != value) {
                    Model.Comentarios = value;
                    IsDirty = true;
                    RaisePropertyChanged(ComentariosPropertyName);
                }
            }
        }

        [LibGridColum("Status Orden De Compra", eGridColumType.Enum, PrintingMemberPath = "StatusOrdenDeCompraStr")]
        public eStatusCompra StatusOrdenDeCompra {
            get {
                return Model.StatusOrdenDeCompraAsEnum;
            }
            set {
                if (Model.StatusOrdenDeCompraAsEnum != value) {
                    Model.StatusOrdenDeCompraAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(StatusOrdenDeCompraPropertyName);
                }
            }
        }

        public eTipoCompra TipoDeCompra {
            get {
                return Model.TipoDeCompraAsEnum;
            }
            set {
                if (Model.TipoDeCompraAsEnum != value) {
                    Model.TipoDeCompraAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDeCompraPropertyName);
                    RaisePropertyChanged(IsVisibleCondicionesImportacionPropertyName);
                }
            }
        }


        [LibCustomValidation("FechaDeAnulacionValidating")]
        public DateTime FechaDeAnulacion {
            get {
                return Model.FechaDeAnulacion;
            }
            set {
                if (Model.FechaDeAnulacion != value) {
                    Model.FechaDeAnulacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaDeAnulacionPropertyName);
                }
            }
        }

        public string CondicionesDeEntrega {
            get {
                return Model.CondicionesDeEntrega;
            }
            set {
                if (Model.CondicionesDeEntrega != value) {
                    Model.CondicionesDeEntrega = value;
                    IsDirty = true;
                    RaisePropertyChanged(CondicionesDeEntregaPropertyName);
                }
            }
        }

        public int CondicionesDePago {
            get {
                return Model.CondicionesDePago;
            }
            set {
                if (Model.CondicionesDePago != value) {
                    Model.CondicionesDePago = value;
                    IsDirty = true;
                    RaisePropertyChanged(CondicionesDePagoPropertyName);
                }
            }
        }

        [LibCustomValidation("CondicionesDePagoValidating")]
        public string DescripcionCondicionesDePago {
            get {
                return Model.DescripcionCondicionesDePago;
            }
            set {
                if (Model.DescripcionCondicionesDePago != value) {
                    Model.DescripcionCondicionesDePago = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionCondicionesDePagoPropertyName);
                    if (LibString.IsNullOrEmpty(DescripcionCondicionesDePago, true)) {
                        ConexionDescripcionCondicionesDePago = null;
                    }
                }
            }
        }

        public eCondicionDeImportacion CondicionesDeImportacion {
            get {
                return Model.CondicionesDeImportacionAsEnum;
            }
            set {
                if (Model.CondicionesDeImportacionAsEnum != value) {
                    Model.CondicionesDeImportacionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(CondicionesDeImportacionPropertyName);
                }
            }
        }

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

        public eTipoCompra[] ArrayTipoCompra {
            get {
                return LibEnumHelper<eTipoCompra>.GetValuesInArray();
            }
        }

        public eStatusCompra[] ArrayStatusCompra {
            get {
                return LibEnumHelper<eStatusCompra>.GetValuesInArray();
            }
        }

        public eCondicionDeImportacion[] ArrayCondicionDeImportacion {
            get {
                return LibEnumHelper<eCondicionDeImportacion>.GetValuesInArray();
            }
        }

        [LibDetailRequired(ErrorMessage = "Orden De Compra Detalle Articulo Inventario es requerido.")]
        public OrdenDeCompraDetalleArticuloInventarioMngViewModel DetailOrdenDeCompraDetalleArticuloInventario {
            get;
            set;
        }



        public FkProveedorViewModel ConexionCodigoProveedor {
            get {
                return _ConexionCodigoProveedor;
            }
            set {
                if (_ConexionCodigoProveedor != value) {
                    _ConexionCodigoProveedor = value;
                    RaisePropertyChanged(CodigoProveedorPropertyName);
                }
                if (_ConexionCodigoProveedor == null) {
                    ConsecutivoProveedor = 0;
                    CodigoProveedor = string.Empty;
                    NombreProveedor = string.Empty;
                }
            }
        }

        public FkProveedorViewModel ConexionNombreProveedor {
            get {
                return _ConexionNombreProveedor;
            }
            set {
                if (_ConexionNombreProveedor != value) {
                    _ConexionNombreProveedor = value;
                    RaisePropertyChanged(NombreProveedorPropertyName);
                }
                if (_ConexionNombreProveedor == null) {
                    ConsecutivoProveedor = 0;
                    CodigoProveedor = string.Empty;
                    NombreProveedor = string.Empty;
                }
            }
        }

        public FkCotizacionViewModel ConexionNumeroCotizacion {
            get {
                return _ConexionNumeroCotizacion;
            }
            set {
                if (_ConexionNumeroCotizacion != value) {
                    _ConexionNumeroCotizacion = value;
                    RaisePropertyChanged(NumeroCotizacionPropertyName);
                }
                if (_ConexionNumeroCotizacion == null) {
                    NumeroCotizacion = string.Empty;
                }
            }
        }

        public FkMonedaViewModel ConexionCodigoMoneda {
            get {
                return _ConexionCodigoMoneda;
            }
            set {
                if (_ConexionCodigoMoneda != value) {
                    _ConexionCodigoMoneda = value;
                    RaisePropertyChanged(CodigoMonedaPropertyName);
                    if (_ConexionCodigoMoneda != null) {
                        CodigoMoneda = _ConexionCodigoMoneda.Codigo;
                        Moneda = _ConexionCodigoMoneda.Nombre;
                    }
                }
                if (_ConexionCodigoMoneda == null) {
                    CodigoMoneda = string.Empty;
                    Moneda = string.Empty;
                }
            }
        }

        public FkCondicionesDePagoViewModel ConexionCondicionesDePago {
            get {
                return _ConexionCondicionesDePago;
            }
            set {
                if (_ConexionCondicionesDePago != value) {
                    _ConexionCondicionesDePago = value;
                    RaisePropertyChanged(CondicionesDePagoPropertyName);
                }
                if (_ConexionCondicionesDePago == null) {
                    CondicionesDePago = 0;
                }
            }
        }

        public FkCondicionesDePagoViewModel ConexionDescripcionCondicionesDePago {
            get {
                return _ConexionDescripcionCondicionesDePago;
            }
            set {
                if (_ConexionDescripcionCondicionesDePago != value) {
                    _ConexionDescripcionCondicionesDePago = value;
                    RaisePropertyChanged(DescripcionCondicionesDePagoPropertyName);
                }
                if (_ConexionDescripcionCondicionesDePago == null) {
                    DescripcionCondicionesDePago = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoProveedorCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseNombreProveedorCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseNumeroCotizacionCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoMonedaCommand {
            get;
            private set;
        }
        public RelayCommand<string> ChooseCondicionesDePagoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseDescripcionCondicionesDePagoCommand {
            get;
            private set;
        }

        public RelayCommand<string> CreateOrdenDeCompraDetalleArticuloInventarioCommand {
            get { return DetailOrdenDeCompraDetalleArticuloInventario.CreateCommand; }
        }

        public RelayCommand<string> UpdateOrdenDeCompraDetalleArticuloInventarioCommand {
            get { return DetailOrdenDeCompraDetalleArticuloInventario.UpdateCommand; }
        }

        public RelayCommand<string> DeleteOrdenDeCompraDetalleArticuloInventarioCommand {
            get { return DetailOrdenDeCompraDetalleArticuloInventario.DeleteCommand; }
        }
        public bool IsVisibleFechaAnulacion {
            get {
                return Action == eAccionSR.Anular;
            }
        }
        public bool IsVisibleConexionProveedorPorCodigo {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaCodigoProveedorEnPantalla");
            }
        }

        public bool IsVisibleConexionProveedorPorNombe {
            get {
                return !IsVisibleConexionProveedorPorCodigo;
            }
        }

        public bool IsVisibleSerie {
            get {
                return !LibDefGen.ProgramInfo.IsCountryVenezuela();
            }
        }

        public bool IsVisibleSoloPeru {
            get {
                return !LibDefGen.ProgramInfo.IsCountryVenezuela();
            }
        }

        public bool IsEnabledCambio {
            get {
                return IsEnabled && !(CodigoMoneda == vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today()) || CodigoMoneda == vMonedaLocal.InstanceMonedaLocalActual.CodigoDeMonedaAnterior(LibDate.Today()));
            }

        }
        public string MonedaActual {
            get {
                return vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(Fecha);
            }
        }

        public bool IsVisibleMonedaActual {
            get {
                vMonedaLocal.InstanceMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
                return vMonedaLocal.InstanceMonedaLocalActual.EsMonedaLocalDelPais(CodigoMoneda);
            }
        }

        public bool UsaBolivarFuerte {
            get {
                return vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(Fecha) == vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today());
            }
        }

        public string lblCambioAMonedaLocalActual {
            get {
                return "Cambio a " + vMonedaLocal.InstanceMonedaLocalActual.SimboloMoneda(LibDate.Today());
            }
        }
        public int DecimalDigits {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales");
            }
        }
        public bool IsEnabledSerie {
            get {
                return IsEnabled && LibDefGen.ProgramInfo.IsCountryPeru() && !LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "SugerirNumeroDeOrdenDeCompra");
            }
        }

        public bool IsEnabledNumero {
            get {
                return IsEnabled && !LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "SugerirNumeroDeOrdenDeCompra");
            }
        }

        public bool IsVisibleCondicionesImportacion {
            get {
                return TipoDeCompra == eTipoCompra.Importacion;
            }
        }
        #endregion //Propiedades

        #region Constructores
        public OrdenDeCompraViewModel()
            : this(new OrdenDeCompra(), eAccionSR.Insertar) {
        }
        public OrdenDeCompraViewModel(OrdenDeCompra initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DecimalesTasaDeCambio = LibDefGen.ProgramInfo.IsCountryPeru() ? 3 : 4;
            DefaultFocusedPropertyName = SeriePropertyName;
            vMonedaLocal = new Saw.Lib.clsNoComunSaw();
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            InitializeDetails();
        }
        #endregion //Constructores

        #region Metodos Generados

        protected override void InitializeLookAndFeel(OrdenDeCompra valModel) {
            base.InitializeLookAndFeel(valModel);
            string vCodigoMonedaSegunModulo = string.Empty;
            if(LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaDivisaComoMonedaPrincipalDeIngresoDeDatos"))) {
                vCodigoMonedaSegunModulo = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
            } else {
                vCodigoMonedaSegunModulo = vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today());
            }
            if (Consecutivo == 0) {
                Consecutivo = GenerarProximoConsecutivo();
            }
            if (Action == eAccionSR.Insertar) {
                if (TipoModulo == eTipoCompra.Importacion) {
                    vCodigoMonedaSegunModulo = "USD";
                }
                ConexionCodigoMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", vCodigoMonedaSegunModulo));
                CodigoMoneda = ConexionCodigoMoneda.Codigo;
                Moneda = ConexionCodigoMoneda.Nombre;
                CambioAMonedaLocal = 1;
                if (!LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaAlmacen")) {
                    LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Saw.Gv_Almacen_B1.Codigo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoAlmacenPorDefecto"));
                    vDefaultCriteria.Add(LibSearchCriteria.CreateCriteria("Saw.Gv_Almacen_B1.ConsecutivoCompania", Mfc.GetInt("Compania")), eLogicOperatorType.And);
                }
                AsignaTasaDelDia(CodigoMoneda);
            }
            vMonedaLocal.InstanceMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            TipoDeCompra = TipoModulo;
            DeshabilitaCamposDeNumeroYGeneraConsecutivo(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "SugerirNumeroDeOrdenDeCompra"));

        }

        protected override OrdenDeCompra FindCurrentRecord(OrdenDeCompra valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "OrdenDeCompraGET", vParams.Get(), UseDetail).FirstOrDefault();
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<OrdenDeCompra>, IList<OrdenDeCompra>> GetBusinessComponent() {
            return new clsOrdenDeCompraNav();
        }

        private int GenerarProximoConsecutivo() {
            int vResult = 0;
            XElement vResulset = GetBusinessComponent().QueryInfo(eProcessMessageType.Message, "ProximoConsecutivo", Mfc.GetIntAsParam("Compania"), false);
            vResult = LibConvert.ToInt(LibXml.GetPropertyString(vResulset, "Consecutivo"));
            return vResult;
        }

        protected override void InitializeDetails() {
            DetailOrdenDeCompraDetalleArticuloInventario = new OrdenDeCompraDetalleArticuloInventarioMngViewModel(this, Model.DetailOrdenDeCompraDetalleArticuloInventario, Action);
            DetailOrdenDeCompraDetalleArticuloInventario.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<OrdenDeCompraDetalleArticuloInventarioViewModel>>(DetailOrdenDeCompraDetalleArticuloInventario_OnCreated);
            DetailOrdenDeCompraDetalleArticuloInventario.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<OrdenDeCompraDetalleArticuloInventarioViewModel>>(DetailOrdenDeCompraDetalleArticuloInventario_OnUpdated);
            DetailOrdenDeCompraDetalleArticuloInventario.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<OrdenDeCompraDetalleArticuloInventarioViewModel>>(DetailOrdenDeCompraDetalleArticuloInventario_OnDeleted);
            DetailOrdenDeCompraDetalleArticuloInventario.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<OrdenDeCompraDetalleArticuloInventarioViewModel>>(DetailOrdenDeCompraDetalleArticuloInventario_OnSelectedItemChanged);
        }
        #region OrdenDeCompraDetalleArticuloInventario

        private void DetailOrdenDeCompraDetalleArticuloInventario_OnSelectedItemChanged(object sender, SearchCollectionChangedEventArgs<OrdenDeCompraDetalleArticuloInventarioViewModel> e) {
            try {
                UpdateOrdenDeCompraDetalleArticuloInventarioCommand.RaiseCanExecuteChanged();
                DeleteOrdenDeCompraDetalleArticuloInventarioCommand.RaiseCanExecuteChanged();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailOrdenDeCompraDetalleArticuloInventario_OnDeleted(object sender, SearchCollectionChangedEventArgs<OrdenDeCompraDetalleArticuloInventarioViewModel> e) {
            try {
                IsDirty = true;
                Model.DetailOrdenDeCompraDetalleArticuloInventario.Remove(e.ViewModel.GetModel());
                e.ViewModel.PropertyChanged -= OnDetailPropertyChanged;
                ActualizaTotales();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void OnDetailPropertyChanged(object sender, PropertyChangedEventArgs e) {

            ActualizaTotales();

        }
        private void DetailOrdenDeCompraDetalleArticuloInventario_OnUpdated(object sender, SearchCollectionChangedEventArgs<OrdenDeCompraDetalleArticuloInventarioViewModel> e) {

            try {
                IsDirty = e.ViewModel.IsDirty;
                ActualizaTotales();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailOrdenDeCompraDetalleArticuloInventario_OnCreated(object sender, SearchCollectionChangedEventArgs<OrdenDeCompraDetalleArticuloInventarioViewModel> e) {
            try {
                Model.DetailOrdenDeCompraDetalleArticuloInventario.Add(e.ViewModel.GetModel());
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //OrdenDeCompraDetalleArticuloInventario

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoProveedorCommand = new RelayCommand<string>(ExecuteChooseCodigoProveedorCommand);
            ChooseNombreProveedorCommand = new RelayCommand<string>(ExecuteChooseNombreProveedorCommand);
            ChooseNumeroCotizacionCommand = new RelayCommand<string>(ExecuteChooseNumeroCotizacionCommand);
            ChooseCodigoMonedaCommand = new RelayCommand<string>(ExecuteChooseCodigoMonedaCommand);
            ChooseCondicionesDePagoCommand = new RelayCommand<string>(ExecuteChooseCondicionesDePagoCommand);
            ChooseDescripcionCondicionesDePagoCommand = new RelayCommand<string>(ExecuteChooseDescripcionCondicionesDePagoCommand);
        }

        protected override void ReloadRelatedConnections() {
            //   base.ReloadRelatedConnections();
            //   ConexionConsecutivoProveedor = FirstConnectionRecordOrDefault<FkProveedorViewModel>("Proveedor", LibSearchCriteria.CreateCriteria("consecutivo", ConsecutivoProveedor));
            //    ConexionCodigoProveedor = FirstConnectionRecordOrDefault<FkProveedorViewModel>("Proveedor", LibSearchCriteria.CreateCriteria("codigoProveedor", CodigoProveedor));
            //ConexionNombreProveedor = FirstConnectionRecordOrDefault<FkProveedorViewModel>("Proveedor", LibSearchCriteria.CreateCriteria("nombreProveedor", NombreProveedor));
            //ConexionCondicionesDePago = FirstConnectionRecordOrDefault<FkCondicionesDePagoViewModel>("Condiciones De Pago", LibSearchCriteria.CreateCriteria("Consecutivo", CondicionesDePago));
        }



        private void ExecuteChooseCodigoProveedorCommand(string valcodigoProveedor) {
            try {
                if (valcodigoProveedor == null) {
                    valcodigoProveedor = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Adm.Gv_Proveedor_B1.CodigoProveedor", valcodigoProveedor);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_Proveedor_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCodigoProveedor = ChooseRecord<FkProveedorViewModel>("Proveedor", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoProveedor != null) {
                    ConsecutivoProveedor = ConexionCodigoProveedor.Consecutivo;
                    CodigoProveedor = ConexionCodigoProveedor.CodigoProveedor;
                    NombreProveedor = ConexionCodigoProveedor.NombreProveedor;
                } else {
                    ConsecutivoProveedor = 0;
                    CodigoProveedor = string.Empty;
                    NombreProveedor = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseNombreProveedorCommand(string valnombreProveedor) {
            try {
                if (valnombreProveedor == null) {
                    valnombreProveedor = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Adm.Gv_Proveedor_B1.NombreProveedor", valnombreProveedor);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_Proveedor_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionNombreProveedor = ChooseRecord<FkProveedorViewModel>("Proveedor", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionNombreProveedor != null) {
                    ConsecutivoProveedor = ConexionNombreProveedor.Consecutivo;
                    CodigoProveedor = ConexionNombreProveedor.CodigoProveedor;
                    NombreProveedor = ConexionNombreProveedor.NombreProveedor;
                } else {
                    ConsecutivoProveedor = 0;
                    CodigoProveedor = string.Empty;
                    NombreProveedor = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseNumeroCotizacionCommand(string valNumero) {
            try {
                if (valNumero == null) {
                    valNumero = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("dbo.cotizacion.Numero", valNumero);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("dbo.cotizacion.ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionNumeroCotizacion = ChooseRecord<FkCotizacionViewModel>("Cotizacion", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionNumeroCotizacion != null) {
                    NumeroCotizacion = ConexionNumeroCotizacion.Numero;
                } else {
                    NumeroCotizacion = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }


        private void ExecuteChooseCodigoMonedaCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Activa", LibConvert.BoolToSN(true));
                vFixedCriteria.Add("TipoDeMoneda", eBooleanOperatorType.IdentityEquality, eTipoDeMoneda.Fisica);
                FkMonedaViewModel vConexionCodigoMoneda = ChooseRecord<FkMonedaViewModel>("Moneda", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (vConexionCodigoMoneda != null
                    && vMonedaLocal.InstanceMonedaLocalActual.EsMonedaLocalDelPais(vConexionCodigoMoneda.Codigo)
                    && vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(Fecha) != vConexionCodigoMoneda.Codigo) {
                    LibMessages.MessageBox.Information(this, "La Moneda local seleccionada NO es Vigente para la fecha del Documento. Se establecerá " +
                        "la moneda Local Vigente", "Moneda Local");
                    ConexionCodigoMoneda = null;
                    ConexionCodigoMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(Fecha)));
                    if (ConexionCodigoMoneda != null) {
                        CodigoMoneda = ConexionCodigoMoneda.Codigo;
                        Moneda = ConexionCodigoMoneda.Nombre;
                        CambioAMonedaLocal = 1;
                    }
                } else if (vConexionCodigoMoneda != null) {
                    ConexionCodigoMoneda = vConexionCodigoMoneda;
                    CodigoMoneda = ConexionCodigoMoneda.Codigo;
                    Moneda = ConexionCodigoMoneda.Nombre;
                    AsignaTasaDelDia(CodigoMoneda);
                } else {
                    CodigoMoneda = string.Empty;
                    Moneda = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCondicionesDePagoCommand(string valConsecutivo) {
            try {
                if (valConsecutivo == null) {
                    valConsecutivo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Consecutivo", valConsecutivo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCondicionesDePago = ChooseRecord<FkCondicionesDePagoViewModel>("Condiciones De Pago", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCondicionesDePago != null) {
                    CondicionesDePago = ConexionCondicionesDePago.Consecutivo;
                    DescripcionCondicionesDePago = ConexionCondicionesDePago.Descripcion;
                } else {
                    CondicionesDePago = 0;
                    DescripcionCondicionesDePago = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseDescripcionCondicionesDePagoCommand(string valDescripcion) {
            try {
                if (valDescripcion == null) {
                    valDescripcion = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Descripcion", valDescripcion);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionDescripcionCondicionesDePago = ChooseRecord<FkCondicionesDePagoViewModel>("Condiciones De Pago", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionDescripcionCondicionesDePago != null) {
                    CondicionesDePago = ConexionDescripcionCondicionesDePago.Consecutivo;
                    DescripcionCondicionesDePago = ConexionDescripcionCondicionesDePago.Descripcion;
                } else {
                    CondicionesDePago = 0;
                    DescripcionCondicionesDePago = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult FechaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(Fecha, false, Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha"));
                }
            }
            return vResult;
        }

        private ValidationResult FechaDeAnulacionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (StatusOrdenDeCompra == eStatusCompra.Anulada) {
                    if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDeAnulacion, false, Action)) {
                        vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha De Anulacion"));
                    }
                }
            }
            return vResult;
        }

        private ValidationResult CondicionesDePagoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (DescripcionCondicionesDePago == string.Empty) {
                    vResult = new ValidationResult("Se debe de seleccionar una condición de pago");
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados

        private void ActualizaTotales() {
            TotalRenglones = DetailOrdenDeCompraDetalleArticuloInventario.Items.Sum(p => p.Cantidad * p.PrecioUnitario);

            TotalCompra = TotalRenglones;

        }

        protected override void ExecuteAction() {
            base.ExecuteAction();
            TipoDeCompra = TipoModulo;

        }


        private ValidationResult SerieValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (!LibDefGen.ProgramInfo.IsCountryVenezuela() && LibString.IsNullOrEmpty(Serie)) {
                    vResult = new ValidationResult("El campo Serie es requerido.");
                }

            }
            return vResult;
        }

        protected override bool RecordIsReadOnly() {
            if (Action == eAccionSR.ReImprimir) {
                return true;
            } else {
                return base.RecordIsReadOnly();
            }
        }
        internal bool BuscarCodigoRepetidoEnElGrid(string valCodigo) {
            var vList = DetailOrdenDeCompraDetalleArticuloInventario.Items.Where(p => p.CodigoArticulo == valCodigo).Select(p => p);
            if (vList != null && vList.Count() >= 1) {
                int vIndex = DetailOrdenDeCompraDetalleArticuloInventario.Items.IndexOf(vList.First());
                if (vIndex != DetailOrdenDeCompraDetalleArticuloInventario.Items.IndexOf(DetailOrdenDeCompraDetalleArticuloInventario.SelectedItem)) {
                    LibMessages.MessageBox.Alert(this, "El artículo que está intentando ingresar ya se encuentra en el Grid por favor dirijase a la linea " + (vIndex + 1).ToString() + Environment.NewLine +
                    "sí desea Agregar o Modificar alguna información del Artículo " + valCodigo, "INFORMACIÓN");

                    return true;
                }
            }
            return false;
        }

        protected override bool CreateRecord() {
            bool vResut = base.CreateRecord();
            if (vResut) {
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "ImprimirOrdenDeCompraAlInsertar")) {
                    ImprimirOrdenDeCompra();
                }
            }
            return vResut;
        }

        protected override void ExecuteSpecialAction(eAccionSR valAction) {
            if (valAction == eAccionSR.ReImprimir) {
                CloseOnActionComplete = true;
                DialogResult = true;
                ImprimirOrdenDeCompra();
            }

        }
        private void DeshabilitaCamposDeNumeroYGeneraConsecutivo(bool requiereConsecutivo) {
            if (!requiereConsecutivo || Action == eAccionSR.Consultar || Action == eAccionSR.Modificar || Action == eAccionSR.ReImprimir) {
                return;
            }
            Serie = "OC";
            IOrdenDeCompraPdn insOrdenCompra = new clsOrdenDeCompraNav();
            var valResult = insOrdenCompra.FindNextNumeroBySerieYTipoDeCompra(ConsecutivoCompania, Serie, TipoDeCompra);
            if (LibConvert.IsNumeric(valResult)) {
                Numero = valResult;
            } else {
                Numero = "00000000";
            }
            if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                Numero = Serie + "-" + Numero;
            }
        }

        private void ImprimirOrdenDeCompra() {
            if (LibMessages.MessageBox.YesNo(this, "Se va a imprimir el documento " + Numero + ". ¿Desea continuar con la impresión?", ModuleName)) {
                string vRpx = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombrePlantillaOrdenDeCompra");
                Galac.Saw.Lib.clsUtilRpt vUtil = new Saw.Lib.clsUtilRpt();
                if (vUtil.EsFormatoRpxValidoParaAOS(vRpx)) {
                    clsCompraInformesViewModel insViewModel = new clsCompraInformesViewModel();
                    insViewModel.ConfigReportOrdenCompra(Consecutivo, TipoModulo);
                } else {
                    StringBuilder vMensaje = new StringBuilder();
                    vMensaje.AppendLine("No se puede imprimir la orden de compra, hay un problema con el formato seleccionado,");
                    vMensaje.AppendLine("verifique que el archivo .RPX seleccionado en parámetros tenga un formato válido");
                    vMensaje.AppendLine("y se encuentre en el directorio de reportes.");
                    LibMessages.MessageBox.Alert(this, vMensaje.ToString(), "Imprimir");
                }
            }
        }

        public bool AsignaTasaDelDia(string valCodigoMoneda) {
            vMonedaLocal.InstanceMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            if (!vMonedaLocal.InstanceMonedaLocalActual.EsMonedaLocalDelPais(valCodigoMoneda)) {
                ConexionCodigoMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigoMoneda));
                CodigoMoneda = ConexionCodigoMoneda.Codigo;
                Moneda = ConexionCodigoMoneda.Nombre;
                bool vElProgramaEstaEnModoAvanzado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsModoAvanzado");
                bool vUsarLimiteMaximoParaIngresoDeTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarLimiteMaximoParaIngresoDeTasaDeCambio");
                decimal vMaximoLimitePermitidoParaLaTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros", "MaximoLimitePermitidoParaLaTasaDeCambio");
                bool vObtenerAutomaticamenteTasaDeCambioDelBCV = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "ObtenerAutomaticamenteTasaDeCambioDelBCV");
                CambioAMonedaLocal = clsSawCambio.InsertaTasaDeCambioParaElDia(CodigoMoneda, Fecha, vUsarLimiteMaximoParaIngresoDeTasaDeCambio, vMaximoLimitePermitidoParaLaTasaDeCambio, vElProgramaEstaEnModoAvanzado, vObtenerAutomaticamenteTasaDeCambioDelBCV);
                bool vResult = CambioAMonedaLocal > 0;
                if (!vResult) {
                    if (LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaDivisaComoMonedaPrincipalDeIngresoDeDatos"))) {
                        return false;
                    }
                    AsignarValoresDeMonedaLocal();
                }
                return true;
            } else {
                CodigoMoneda = vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(Fecha);
                Moneda = vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(Fecha);
                CambioAMonedaLocal = 1;
                return true;
            }
        }

        private void AsignarValoresDeMonedaLocal() {
            if (TipoDeCompra == eTipoCompra.Importacion) {
                ConexionCodigoMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", "USD"));
                CodigoMoneda = ConexionCodigoMoneda.Codigo;
                Moneda = ConexionCodigoMoneda.Nombre;
                AsignaTasaDelDia(CodigoMoneda);
            } else {
                CodigoMoneda = vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(Fecha);
                Moneda = vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(Fecha);
                CambioAMonedaLocal = 1;
            }
        }

    } //End of class OrdenDeCompraViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

