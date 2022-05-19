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
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Ccl.GestionCompras;
using System.ComponentModel;
using Galac.Adm.Uil.GestionCompras.Reportes;
using LibGalac.Aos.Brl;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Brl.TablasGen;
using Galac.Comun.Uil.TablasGen.ViewModel;
using System.Text.RegularExpressions;
using Galac.Saw.Lib;
using System.Text;

namespace Galac.Adm.Uil.GestionCompras.ViewModel {
    public class CompraViewModel : LibInputMasterViewModelMfc<Compra> {

        #region Constantes y Variables

        private const string SeriePropertyName = "Serie";
        private const string NumeroPropertyName = "Numero";
        private const string FechaPropertyName = "Fecha";
        private const string ConsecutivoProveedorPropertyName = "ConsecutivoProveedor";
        private const string CodigoProveedorPropertyName = "CodigoProveedor";
        private const string NombreProveedorPropertyName = "NombreProveedor";
        private const string ConsecutivoAlmacenPropertyName = "ConsecutivoAlmacen";
        private const string CodigoAlmacenPropertyName = "CodigoAlmacen";
        private const string NombreAlmacenPropertyName = "NombreAlmacen";
        private const string MonedaPropertyName = "Moneda";
        private const string CodigoMonedaPropertyName = "CodigoMoneda";
        private const string CambioAMonedaLocalPropertyName = "CambioAMonedaLocal";
        private const string GenerarCXPPropertyName = "GenerarCXP";
        private const string UsaSeguroPropertyName = "UsaSeguro";
        private const string TipoDeDistribucionPropertyName = "TipoDeDistribucion";
        private const string TasaAduaneraPropertyName = "TasaAduanera";
        private const string TasaDolarPropertyName = "TasaDolar";
        private const string TotalRenglonesPropertyName = "TotalRenglones";
        private const string TotalOtrosGastosPropertyName = "TotalOtrosGastos";
        private const string TotalCompraPropertyName = "TotalCompra";
        private const string ComentariosPropertyName = "Comentarios";
        private const string StatusCompraPropertyName = "StatusCompra";
        private const string TipoDeCompraPropertyName = "TipoDeCompra";
        private const string FechaDeAnulacionPropertyName = "FechaDeAnulacion";
        private const string NumeroDeOrdenDeCompraPropertyName = "NumeroDeOrdenDeCompra";
        private const string NoFacturaNotaEntregaPropertyName = "NoFacturaNotaEntrega";
        private const string TipoDeCompraParaCxPPropertyName = "TipoDeCompraParaCxP";
        private const string NombreOperadorPropertyName = "NombreOperador";
        private const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        private const string IsEnabledCambioPropertyName = "IsEnabledCambio";
        private const string MonedaActualPropertyName = "MonedaActual";
        private const string IsVisibleMonedaActualPropertyName = "IsVisibleMonedaActual";
        private const string DifereciaDistribucionPropertyName = "DifereciaDistribucion";
        private const string IsVisibleParaDistribucionAutomaticaPropertyName = "IsVisibleParaDistribucionAutomatica";
        private const string GenerarOActualizarCxPPropertyName = "GenerarOActualizarCxP";
        public const string CodigoMonedaCostoUltimaCompraPropertyName = "CodigoMonedaCostoUltimaCompra";
        public const string MonedaCostoUltimaCompraPropertyName = "MonedaCostoUltimaCompra";
        public const string CambioCostoUltimaCompraPropertyName = "CambioCostoUltimaCompra";
        public const string IsEnabledCambioCostoUltimaCompraPropertyName = "IsEnabledCambioCostoUltimaCompra";

        bool _IsEnabledTipoDistribucion;
        private FkProveedorViewModel _ConexionCodigoProveedor = null;
        private FkProveedorViewModel _ConexionNombreProveedor = null;
        private FkAlmacenViewModel _ConexionCodigoAlmacen = null;
        private FkOrdenDeCompraViewModel _ConexionNumeroDeOrdenDeCompra = null;
        private FkMonedaViewModel _ConexionCodigoMoneda = null;
        private FkMonedaViewModel _ConexionMonedaCostoUltimaCompra = null;
        private Saw.Lib.clsNoComunSaw vMonedaLocal = null;
        private string _NumeroDeCompraOriginal;
        private string _CodigoProveedorOriginal;
        private string _GenerarOActualizarCxP;
        public event EventHandler MoveFocusArticuloInventarioEvent;
        public event EventHandler AjustaColumnasSegunTipoEvent;
        bool _ElProgramaEstaEnModoAvanzado = false;
        bool _UsarLimiteMaximoParaIngresoDeTasaDeCambio = false;
        int _MaximoLimitePermitidoParaLaTasaDeCambio = 0;
        #endregion //Constantes y Variables

        #region Propiedades

        internal eTipoCompra TipoModulo {
            get; set;
        }

        public override string ModuleName {
            get {
                if (TipoModulo == eTipoCompra.Importacion) {
                    return "Importación";
                }
                return "Compra Nacional";
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
        [LibGridColum("Serie", MaxLength = 20, ColumnOrder = 0)]
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
        [LibGridColum("Número", MaxLength = 20, ColumnOrder = 1)]
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
        [LibGridColum("Fecha", eGridColumType.DatePicker, Width = 100, ColumnOrder = 2)]
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
                    AsignaTasaCostoUltimaCompraDelDia(CodigoMonedaCostoUltimaCompra);
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
        [LibGridColum("Nombre Proveedor", eGridColumType.Connection, ConnectionDisplayMemberPath = "nombreProveedor", ConnectionModelPropertyName = "Adm.Gv_Proveedor_B1.NombreProveedor", ConnectionSearchCommandName = "ChooseNombreProveedorCommand", Width = 250, DbMemberPath = "Gv_Compra_B1.NombreProveedor", ColumnOrder = 3)]
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

        public int ConsecutivoAlmacen {
            get {
                return Model.ConsecutivoAlmacen;
            }
            set {
                if (Model.ConsecutivoAlmacen != value) {
                    Model.ConsecutivoAlmacen = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConsecutivoAlmacenPropertyName);
                    if (ConsecutivoAlmacen == 0) {
                        ConexionCodigoAlmacen = null;
                    }
                }
            }
        }

        [LibCustomValidation("CodigoAlmacenValidating")]
        public string CodigoAlmacen {
            get {
                return Model.CodigoAlmacen;
            }
            set {
                if (Model.CodigoAlmacen != value) {
                    Model.CodigoAlmacen = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoAlmacenPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoAlmacen, true)) {
                        ConexionCodigoAlmacen = null;
                    }
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

        [LibRequired(ErrorMessage = "El campo Moneda es requerido.")]
        public string Moneda {
            get {
                return Model.Moneda;
            }
            set {
                if (Model.Moneda != value) {
                    Model.Moneda = value;
                    if (LibString.IsNullOrEmpty(Model.Moneda)) {
                        Model.Moneda = vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(Fecha);
                        CodigoMoneda = vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(Fecha);
                        AsignarValoresDeCostoMonedaUltimaCompraPorDefecto();
                    } else {
                        if (Model.Moneda == vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(Fecha)) {
                            AsignarValoresDeCostoMonedaUltimaCompraPorDefecto();
                        } else {
                            CodigoMonedaCostoUltimaCompra = vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(Fecha);
                            MonedaCostoUltimaCompra = vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(Fecha);
                            CambioCostoUltimaCompra = 1;
                        }
                    }
                    RaisePropertyChanged(MonedaCostoUltimaCompraPropertyName);
                    RaisePropertyChanged(CambioCostoUltimaCompraPropertyName);
                    RaisePropertyChanged(MonedaPropertyName);
                    RaisePropertyChanged(MonedaActualPropertyName);
                    RaisePropertyChanged(IsEnabledCambioPropertyName);
                    RaisePropertyChanged(IsVisibleMonedaActualPropertyName);
                    RaisePropertyChanged(IsEnabledCambioCostoUltimaCompraPropertyName);
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

        public bool GenerarCXP {
            get {
                return Model.GenerarCXPAsBool;
            }
            set {
                if (Model.GenerarCXPAsBool != value) {
                    Model.GenerarCXPAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(GenerarCXPPropertyName);
                }
            }
        }

        public string GenerarOActualizarCxP {
            get {
                return _GenerarOActualizarCxP;
            }
            set {
                if (_GenerarOActualizarCxP != value) {
                    _GenerarOActualizarCxP = value;
                    RaisePropertyChanged(GenerarOActualizarCxPPropertyName);
                }
            }
        }
        public bool UsaSeguro {
            get {
                return Model.UsaSeguroAsBool;
            }
            set {
                if (Model.UsaSeguroAsBool != value) {
                    Model.UsaSeguroAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaSeguroPropertyName);
                    RaiseAjustaColumnasSegunTipo();
                    ActualizaVisibleUsaDeSeguro();
                }
            }
        }

        [LibGridColum("Tipo De Distribución", eGridColumType.Enum, PrintingMemberPath = "TipoDeDistribucionStr", Width = 150, ColumnOrder = 4)]
        [LibCustomValidation("TipoDeDistribucionValidating")]
        public eTipoDeDistribucion TipoDeDistribucion {
            get {
                return Model.TipoDeDistribucionAsEnum;
            }
            set {
                if (Model.TipoDeDistribucionAsEnum != value) {
                    Model.TipoDeDistribucionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDeDistribucionPropertyName);
                    IsEnabledTipoDistribucion = false;
                    RaiseAjustaColumnasSegunTipo();
                    ActualizaVisiblePorTipoDeDistribucion();
                    if (value == eTipoDeDistribucion.Automatica) {
                        BuscaTasaDolar();
                        RaisePropertyChanged(IsVisibleParaDistribucionAutomaticaPropertyName);
                        VerDistribucionCommand.RaiseCanExecuteChanged();
                    }
                    VerDistribucionCommand.RaiseCanExecuteChanged();

                }
            }
        }

        [LibCustomValidation("TasaAduaneraValidating")]
        public decimal TasaAduanera {
            get {
                return Model.TasaAduanera;
            }
            set {
                if (Model.TasaAduanera != value) {
                    Model.TasaAduanera = value;
                    IsDirty = true;
                    RaisePropertyChanged(TasaAduaneraPropertyName);
                }
            }
        }

        [LibCustomValidation("TasaDolarValidating")]
        public decimal TasaDolar {
            get {
                return LibMath.RoundToNDecimals(Model.TasaDolar, LibDefGen.ProgramInfo.IsCountryPeru() ? 3 : 4);
            }
            set {
                if (Model.TasaDolar != value) {
                    Model.TasaDolar = value;
                    IsDirty = true;
                    RaisePropertyChanged(TasaDolarPropertyName);
                }
            }
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
                    RaisePropertyChanged(DifereciaDistribucionPropertyName);
                }
            }
        }

        [LibCustomValidation("TotalOtrosGastosvalidating")]
        public decimal TotalOtrosGastos {
            get {
                return Model.TotalOtrosGastos;
            }
            set {
                if (Model.TotalOtrosGastos != value) {
                    Model.TotalOtrosGastos = value;
                    IsDirty = true;
                    RaisePropertyChanged(TotalOtrosGastosPropertyName);
                    RaisePropertyChanged(DifereciaDistribucionPropertyName);
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

        [LibGridColum("Status Compra", eGridColumType.Enum, PrintingMemberPath = "StatusCompraStr", ColumnOrder = 5)]
        public eStatusCompra StatusCompra {
            get {
                return Model.StatusCompraAsEnum;
            }
            set {
                if (Model.StatusCompraAsEnum != value) {
                    Model.StatusCompraAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(StatusCompraPropertyName);
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

        public int ConsecutivoOrdenDeCompra {
            get {
                return Model.ConsecutivoOrdenDeCompra;
            }
            set {
                if (Model.ConsecutivoOrdenDeCompra != value) {
                    Model.ConsecutivoOrdenDeCompra = value;
                }
            }
        }

        public string NumeroDeOrdenDeCompra {
            get {
                return Model.NumeroDeOrdenDeCompra;
            }
            set {
                if (Model.NumeroDeOrdenDeCompra != value) {
                    Model.NumeroDeOrdenDeCompra = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroDeOrdenDeCompraPropertyName);
                }
            }
        }

        public string NoFacturaNotaEntrega {
            get {
                return Model.NoFacturaNotaEntrega;
            }
            set {
                if (Model.NoFacturaNotaEntrega != value) {
                    Model.NoFacturaNotaEntrega = value;
                    IsDirty = true;
                    RaisePropertyChanged(NoFacturaNotaEntregaPropertyName);
                }
            }
        }

        public string CodigoMonedaCostoUltimaCompra {
            get {
                return Model.CodigoMonedaCostoUltimaCompra;
            }
            set {
                if (Model.CodigoMonedaCostoUltimaCompra != value) {
                    Model.CodigoMonedaCostoUltimaCompra = value;
                    RaisePropertyChanged(CodigoMonedaCostoUltimaCompraPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Moneda Costo Última Compra es requerido.")]
        public string MonedaCostoUltimaCompra {
            get {
                return LibString.IsNullOrEmpty(Model.MonedaCostoUltimaCompra) ? vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(Fecha) : Model.MonedaCostoUltimaCompra;
            }
            set {
                if (Model.MonedaCostoUltimaCompra != value) {
                    Model.MonedaCostoUltimaCompra = value;
                    IsDirty = true;
                    RaisePropertyChanged(MonedaCostoUltimaCompraPropertyName);
                    if (LibString.IsNullOrEmpty(MonedaCostoUltimaCompra, true)) {
                        ConexionMonedaCostoUltimaCompra = null;
                    }
                }
            }
        }
        public decimal CambioCostoUltimaCompra {
            get {
                return Model.CambioCostoUltimaCompra;
            }
            set {
                if (Model.CambioCostoUltimaCompra != value) {
                    Model.CambioCostoUltimaCompra = value;
                    IsDirty = true;
                    RaisePropertyChanged(CambioCostoUltimaCompraPropertyName);
                }
            }
        }
        public eTipoOrdenDeCompra TipoDeCompraParaCxP {
            get {
                return Model.TipoDeCompraParaCxPAsEnum;
            }
            set {
                if (Model.TipoDeCompraParaCxPAsEnum != value) {
                    Model.TipoDeCompraParaCxPAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDeCompraParaCxPPropertyName);
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

        public eTipoDeDistribucion[] ArrayTipoDeDistribucion {
            get {
                if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                    return LibEnumHelper<eTipoDeDistribucion>.GetValuesInArray();
                } else {
                    List<eTipoDeDistribucion> vResult = LibEnumHelper<eTipoDeDistribucion>.GetValuesInArray().ToList();
                    vResult.Remove(eTipoDeDistribucion.Automatica);
                    return vResult.ToArray();
                }
            }
        }

        public eStatusCompra[] ArrayStatusCompra {
            get {
                return LibEnumHelper<eStatusCompra>.GetValuesInArray();
            }
        }

        public eTipoCompra[] ArrayTipoCompra {
            get {
                return LibEnumHelper<eTipoCompra>.GetValuesInArray();
            }
        }

        public eTipoOrdenDeCompra[] ArrayTipoOrdenDeCompra {
            get {
                return LibEnumHelper<eTipoOrdenDeCompra>.GetValuesInArray();
            }
        }


        [LibDetailRequired(ErrorMessage = "Compra Detalle Articulo Inventario es requerido.")]
        public CompraDetalleArticuloInventarioMngViewModel DetailCompraDetalleArticuloInventario {
            get;
            set;
        }

        public CompraDetalleGastoMngViewModel DetailCompraDetalleGasto {
            get;
            set;
        }

        public CompraDetalleSerialRolloMngViewModel DetailCompraDetalleSerialRollo {
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

        public FkAlmacenViewModel ConexionCodigoAlmacen {
            get {
                return _ConexionCodigoAlmacen;
            }
            set {
                if (_ConexionCodigoAlmacen != value) {
                    _ConexionCodigoAlmacen = value;
                    RaisePropertyChanged(CodigoAlmacenPropertyName);
                    if (_ConexionCodigoAlmacen != null) {
                        ConsecutivoAlmacen = ConexionCodigoAlmacen.Consecutivo;
                        CodigoAlmacen = ConexionCodigoAlmacen.Codigo;
                        NombreAlmacen = ConexionCodigoAlmacen.NombreAlmacen;
                    }
                }
                if (_ConexionCodigoAlmacen == null) {
                    ConsecutivoAlmacen = 0;
                    CodigoAlmacen = string.Empty;
                    NombreAlmacen = string.Empty;
                }
            }
        }

        public FkOrdenDeCompraViewModel ConexionNumeroDeOrdenDeCompra {
            get {
                return _ConexionNumeroDeOrdenDeCompra;
            }
            set {
                if (_ConexionNumeroDeOrdenDeCompra != value) {
                    _ConexionNumeroDeOrdenDeCompra = value;
                    RaisePropertyChanged(NumeroDeOrdenDeCompraPropertyName);
                }
                if (_ConexionNumeroDeOrdenDeCompra == null) {
                    //  NumeroDeOrdenDeCompra = string.Empty;
                }
            }
        }

        public FkMonedaViewModel ConexionMonedaCostoUltimaCompra {
            get {
                return _ConexionMonedaCostoUltimaCompra;
            }
            set {
                if (_ConexionMonedaCostoUltimaCompra != value) {
                    _ConexionMonedaCostoUltimaCompra = value;
                    MonedaCostoUltimaCompra = _ConexionMonedaCostoUltimaCompra.Nombre;
                    CodigoMonedaCostoUltimaCompra = _ConexionMonedaCostoUltimaCompra.Codigo;
                } else if (_ConexionMonedaCostoUltimaCompra == null) {
                    MonedaCostoUltimaCompra = vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(Fecha);
                    CodigoMonedaCostoUltimaCompra = vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(Fecha);
                } else {
                    MonedaCostoUltimaCompra = _ConexionMonedaCostoUltimaCompra.Nombre;
                    CodigoMonedaCostoUltimaCompra = _ConexionMonedaCostoUltimaCompra.Codigo;
                }
                RaisePropertyChanged(MonedaCostoUltimaCompraPropertyName);
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
                        Model.SimboloMoneda = _ConexionCodigoMoneda.Simbolo;
                    }
                }
                if (_ConexionCodigoMoneda == null) {
                    CodigoMoneda = string.Empty;
                    Moneda = string.Empty;
                    Model.SimboloMoneda = string.Empty;
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

        public RelayCommand<string> ChooseCodigoAlmacenCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseNumeroDeOrdenDeCompraCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoMonedaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseMonedaCostoUltimaCompraCommand {
            get;
            private set;
        }
        public RelayCommand<string> CreateCompraDetalleArticuloInventarioCommand {
            get {
                return DetailCompraDetalleArticuloInventario.CreateCommand;
            }
        }

        public RelayCommand<string> UpdateCompraDetalleArticuloInventarioCommand {
            get {
                return DetailCompraDetalleArticuloInventario.UpdateCommand;
            }
        }

        public RelayCommand<string> DeleteCompraDetalleArticuloInventarioCommand {
            get {
                return DetailCompraDetalleArticuloInventario.DeleteCommand;
            }
        }

        public RelayCommand<string> CreateCompraDetalleGastoCommand {
            get {
                return DetailCompraDetalleGasto.CreateCommand;
            }
        }

        public RelayCommand<string> UpdateCompraDetalleGastoCommand {
            get {
                return DetailCompraDetalleGasto.UpdateCommand;
            }
        }

        public RelayCommand<string> DeleteCompraDetalleGastoCommand {
            get {
                return DetailCompraDetalleGasto.DeleteCommand;
            }
        }

        public bool VieneDeOrdenDeCompra {
            get; set;
        }

        public bool IsVisibleDatosDeOrdenDeCompra {
            get {
                return VieneDeOrdenDeCompra;
            }
        }

        public bool IsVisibleFechaAnulacion {
            get {
                return Action == eAccionSR.Anular;
            }
        }

        public bool IsVisibleAlmacen {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaAlmacen");
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

        public bool IsVisibleNumOrdenDeCompra {
            get {
                return Action != eAccionSR.Insertar && !LibString.IsNullOrEmpty(NumeroDeOrdenDeCompra);
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

        public decimal DifereciaDistribucion {
            get {

                return TotalOtrosGastos - DetailCompraDetalleArticuloInventario.Items.Sum(p => p.MontoDistribucion);
            }
            set {

            }
        }

        public bool IsVisibleUsaSeguro {
            get {
                return TipoDeCompra == eTipoCompra.Importacion;
            }
        }

        public bool IsEnabledTipoDistribucion {
            get {
                return IsEnabled && _IsEnabledTipoDistribucion;
            }
            set {
                if (TipoDeDistribucion == eTipoDeDistribucion.Ninguno || !DetailCompraDetalleArticuloInventario.HasItems) {
                    _IsEnabledTipoDistribucion = true;
                } else {

                    if (_IsEnabledTipoDistribucion != value) {
                        _IsEnabledTipoDistribucion = value;
                        RaisePropertyChanged("IsEnabledTipoDistribucion");
                    }
                }
            }
        }

        public bool IsVisibleDiferenciaDistribucion {
            get {
                return TipoDeDistribucion == eTipoDeDistribucion.ManualPorMonto;
            }
        }

        public bool IsVisibleParaDistribucionAutomatica {
            get {
                return TipoDeCompra != eTipoCompra.Nacional && TipoDeDistribucion == eTipoDeDistribucion.Automatica && LibDefGen.ProgramInfo.IsCountryPeru();
            }
        }

        public bool IsVisibleGenerarCxPDesdeCompra {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "GenerarCxPDesdeCompra")
                    && LibSecurityManager.CurrentUserHasAccessTo("CxP", "Insertar");
            }
        }

        public int DecimalDigits {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales");
            }
        }

        public bool IsEnabledProveedor {
            get {
                return IsEnabled && !VieneDeOrdenDeCompra;
            }
        }

        public RelayCommand VerDistribucionCommand {
            get;
            private set;
        }

        public int MaxLengthSegunPais {
            get {
                int vResult = 20;
                if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                    vResult = 8;
                }
                return vResult;
            }
        }

        public bool IsEnabledOrdenDeCompra {
            get; set;
        }

        #endregion //Propiedades

        #region Constructores e Inicializadores

        public CompraViewModel()
            : this(new Compra(), eAccionSR.Insertar) {
        }

        public CompraViewModel(Compra initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = SeriePropertyName;
            if (Action != eAccionSR.Listar) {
                vMonedaLocal = new Saw.Lib.clsNoComunSaw();
            }
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            ActualizarNumeroYCodigoProveedorOriginal();
            DecimalesTasaDeCambio = LibDefGen.ProgramInfo.IsCountryPeru() ? 3 : 4;
            InitializeDetails();
        }

        protected override void InitializeLookAndFeel(Compra valModel) {
            base.InitializeLookAndFeel(valModel);
            string vCodigoMonedaSegunModulo = string.Empty;
            if (LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaDivisaComoMonedaPrincipalDeIngresoDeDatos"))) {
                vCodigoMonedaSegunModulo = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
            } else {
                vCodigoMonedaSegunModulo = vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today());
            }
            if (Consecutivo == 0) {
                Consecutivo = GenerarProximoConsecutivo();
            }
            if (Action == eAccionSR.Insertar) {
                if (TipoModulo == eTipoCompra.Importacion) {
                    vCodigoMonedaSegunModulo = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
                }
                ConexionCodigoMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", vCodigoMonedaSegunModulo));
                CodigoMoneda = ConexionCodigoMoneda.Codigo;
                Moneda = ConexionCodigoMoneda.Nombre;
                CambioAMonedaLocal = 1;
                GetModel().ValorUT = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros", "ValorUT");
                if (!LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaAlmacen")) {
                    LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Saw.Gv_Almacen_B1.Codigo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoAlmacenPorDefecto"));
                    vDefaultCriteria.Add(LibSearchCriteria.CreateCriteria("Saw.Gv_Almacen_B1.ConsecutivoCompania", Mfc.GetInt("Compania")), eLogicOperatorType.And);
                    ConexionCodigoAlmacen = FirstConnectionRecordOrDefault<FkAlmacenViewModel>("Almacén", vDefaultCriteria);

                }
                GenerarCXP = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "GenerarCxPDesdeCompra");
                AsignaTasaDelDia(CodigoMoneda);
            }
            vMonedaLocal.InstanceMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            TipoDeCompra = TipoModulo;
            if (Action == eAccionSR.Insertar) {

                if (TipoDeCompra == eTipoCompra.Nacional) {
                    TipoDeDistribucion = eTipoDeDistribucion.Ninguno;
                    //Model.TipoDeDistribucionAsEnum = eTipoDeDistribucion.Ninguno;
                } else {
                    if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                        TipoDeDistribucion = eTipoDeDistribucion.Automatica;
                    } else {
                        TipoDeDistribucion = eTipoDeDistribucion.Ninguno;
                    }
                }

                IsEnabledTipoDistribucion = true;
            } else {
                IsEnabledTipoDistribucion = false;
            }
            RaiseAjustaColumnasSegunTipo();
            if (Action == eAccionSR.ReImprimir) {

            }
            LibBusinessProcess.Register(this, "MensajeDeRecalcularSiEsElCaso", EjecutarProcesosMensajeDeRecalcularSiEsElCaso);
            RaiseMoveFocus(DefaultFocusedPropertyName);
            IsEnabledOrdenDeCompra = VieneDeOrdenDeCompra;
            AsignarMensajeDeGeneracionOActualizacionDeCxP();            
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoProveedorCommand = new RelayCommand<string>(ExecuteChooseCodigoProveedorCommand);
            ChooseNombreProveedorCommand = new RelayCommand<string>(ExecuteChooseNombreProveedorCommand);
            ChooseCodigoAlmacenCommand = new RelayCommand<string>(ExecuteChooseCodigoAlmacenCommand);
            ChooseNumeroDeOrdenDeCompraCommand = new RelayCommand<string>(ExecuteChooseNumeroDeOrdenDeCompraCommand);
            ChooseCodigoMonedaCommand = new RelayCommand<string>(ExecuteChooseCodigoMonedaCommand);
            VerDistribucionCommand = new RelayCommand(ExecuteVerDistribucion, CanExecuteVerDistribucion);
            ChooseMonedaCostoUltimaCompraCommand = new RelayCommand<string>(ExecuteChooseMonedaCostoUltimaCompraCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            RibbonData.TabDataCollection[0].AddTabGroupData(CreateAdicionalRibbonGroup());
        }

        protected override void InitializeDetails() {
            DetailCompraDetalleArticuloInventario = new CompraDetalleArticuloInventarioMngViewModel(this, Model.DetailCompraDetalleArticuloInventario, Action);
            DetailCompraDetalleArticuloInventario.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<CompraDetalleArticuloInventarioViewModel>>(DetailCompraDetalleArticuloInventario_OnCreated);
            DetailCompraDetalleArticuloInventario.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<CompraDetalleArticuloInventarioViewModel>>(DetailCompraDetalleArticuloInventario_OnUpdated);
            DetailCompraDetalleArticuloInventario.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<CompraDetalleArticuloInventarioViewModel>>(DetailCompraDetalleArticuloInventario_OnDeleted);
            DetailCompraDetalleArticuloInventario.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<CompraDetalleArticuloInventarioViewModel>>(DetailCompraDetalleArticuloInventario_OnSelectedItemChanged);

            DetailCompraDetalleGasto = new CompraDetalleGastoMngViewModel(this, Model.DetailCompraDetalleGasto, Action);
            DetailCompraDetalleGasto.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<CompraDetalleGastoViewModel>>(DetailCompraDetalleGasto_OnCreated);
            DetailCompraDetalleGasto.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<CompraDetalleGastoViewModel>>(DetailCompraDetalleGasto_OnUpdated);
            DetailCompraDetalleGasto.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<CompraDetalleGastoViewModel>>(DetailCompraDetalleGasto_OnDeleted);
            DetailCompraDetalleGasto.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<CompraDetalleGastoViewModel>>(DetailCompraDetalleGasto_OnSelectedItemChanged);

            DetailCompraDetalleSerialRollo = new CompraDetalleSerialRolloMngViewModel(this, Model.DetailCompraDetalleSerialRollo, Action);
            DetailCompraDetalleSerialRollo.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<CompraDetalleSerialRolloViewModel>>(DetailCompraDetalleSerialRollo_OnCreated);
            DetailCompraDetalleSerialRollo.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<CompraDetalleSerialRolloViewModel>>(DetailCompraDetalleSerialRollo_OnUpdated);
            DetailCompraDetalleSerialRollo.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<CompraDetalleSerialRolloViewModel>>(DetailCompraDetalleSerialRollo_OnDeleted);
            DetailCompraDetalleSerialRollo.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<CompraDetalleSerialRolloViewModel>>(DetailCompraDetalleSerialRollo_OnSelectedItemChanged);
        }

        #endregion //Constructores

        #region CompraDetalleArticuloInventario

        private void DetailCompraDetalleArticuloInventario_OnSelectedItemChanged(object sender, SearchCollectionChangedEventArgs<CompraDetalleArticuloInventarioViewModel> e) {
            try {
                UpdateCompraDetalleArticuloInventarioCommand.RaiseCanExecuteChanged();
                DeleteCompraDetalleArticuloInventarioCommand.RaiseCanExecuteChanged();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailCompraDetalleArticuloInventario_OnDeleted(object sender, SearchCollectionChangedEventArgs<CompraDetalleArticuloInventarioViewModel> e) {
            try {
                IsEnabledTipoDistribucion = false;
                IsDirty = true;
                var vCompraSerialRollo = Model.DetailCompraDetalleSerialRollo.Where(p => p.CodigoArticulo == e.ViewModel.GetModel().CodigoArticulo).FirstOrDefault();
                Model.DetailCompraDetalleSerialRollo.Remove(vCompraSerialRollo);
                Model.DetailCompraDetalleArticuloInventario.Remove(e.ViewModel.GetModel());
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

        private void DetailCompraDetalleArticuloInventario_OnUpdated(object sender, SearchCollectionChangedEventArgs<CompraDetalleArticuloInventarioViewModel> e) {
            try {
                IsEnabledTipoDistribucion = false;
                IsDirty = e.ViewModel.IsDirty;
                ActualizarDistribucion();
                ActualizaTotales();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailCompraDetalleArticuloInventario_OnCreated(object sender, SearchCollectionChangedEventArgs<CompraDetalleArticuloInventarioViewModel> e) {
            try {
                IsEnabledTipoDistribucion = false;
                Model.DetailCompraDetalleArticuloInventario.Add(e.ViewModel.GetModel());
                e.ViewModel.PropertyChanged += OnDetailPropertyChanged;
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        internal void RaiseMoveFocusArticuloInventario() {
            var handle = MoveFocusArticuloInventarioEvent;
            if (handle != null) {
                handle(this, EventArgs.Empty);
            }
        }

        private void RaiseAjustaColumnasSegunTipo() {
            var handle = AjustaColumnasSegunTipoEvent;
            if (handle != null) {
                handle(this, EventArgs.Empty);
            }
        }

        #endregion //CompraDetalleArticuloInventario

        #region CompraDetalleGasto

        private void DetailCompraDetalleGasto_OnSelectedItemChanged(object sender, SearchCollectionChangedEventArgs<CompraDetalleGastoViewModel> e) {
            try {
                UpdateCompraDetalleGastoCommand.RaiseCanExecuteChanged();
                DeleteCompraDetalleGastoCommand.RaiseCanExecuteChanged();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailCompraDetalleGasto_OnDeleted(object sender, SearchCollectionChangedEventArgs<CompraDetalleGastoViewModel> e) {
            try {
                IsDirty = true;
                Model.DetailCompraDetalleGasto.Remove(e.ViewModel.GetModel());
                e.ViewModel.PropertyChanged -= OnDetailPropertyChanged;
                ActualizarDistribucion();
                ActualizaTotales();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailCompraDetalleGasto_OnUpdated(object sender, SearchCollectionChangedEventArgs<CompraDetalleGastoViewModel> e) {
            try {
                IsDirty = e.ViewModel.IsDirty;
                ActualizaTotales();
                ActualizarDistribucion();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailCompraDetalleGasto_OnCreated(object sender, SearchCollectionChangedEventArgs<CompraDetalleGastoViewModel> e) {
            try {
                Model.DetailCompraDetalleGasto.Add(e.ViewModel.GetModel());
                //DetailCompraDetalleGasto.SelectedItem = e.ViewModel;
                //DetailCompraDetalleGasto.SelectedIndex = DetailCompraDetalleGasto.Items.Count - 1;
                e.ViewModel.PropertyChanged += OnDetailPropertyChanged;
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        #endregion //CompraDetalleGasto

        #region CompraDetalleSerialRollo

        private void DetailCompraDetalleSerialRollo_OnSelectedItemChanged(object sender, SearchCollectionChangedEventArgs<CompraDetalleSerialRolloViewModel> e) {

        }

        private void DetailCompraDetalleSerialRollo_OnDeleted(object sender, SearchCollectionChangedEventArgs<CompraDetalleSerialRolloViewModel> e) {
            try {
                IsDirty = true;
                Model.DetailCompraDetalleSerialRollo.Remove(e.ViewModel.GetModel());
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailCompraDetalleSerialRollo_OnUpdated(object sender, SearchCollectionChangedEventArgs<CompraDetalleSerialRolloViewModel> e) {
            try {
                IsDirty = e.ViewModel.IsDirty;
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailCompraDetalleSerialRollo_OnCreated(object sender, SearchCollectionChangedEventArgs<CompraDetalleSerialRolloViewModel> e) {
            try {
                Model.DetailCompraDetalleSerialRollo.Add(e.ViewModel.GetModel());
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        #endregion //CompraDetalleSerialRollo

        #region Comandos

        protected override void ExecuteAction() {
            base.ExecuteAction();
            TipoDeCompra = TipoModulo;
            RaiseAjustaColumnasSegunTipo();
        }

        protected override void ExecuteProcessAfterAction() {
            base.ExecuteProcessAfterAction();
            if (Action == eAccionSR.Insertar) {
                if (TipoDeCompra == eTipoCompra.Nacional) {
                    TipoDeDistribucion = eTipoDeDistribucion.Ninguno;
                    IsEnabledTipoDistribucion = true;
                } else {
                    IsEnabledTipoDistribucion = true;
                    if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                        TipoDeDistribucion = eTipoDeDistribucion.Automatica;
                    } else {
                        TipoDeDistribucion = eTipoDeDistribucion.Ninguno;
                    }
                }
            }
            ActualizaTotales();
            ActualizarNumeroYCodigoProveedorOriginal();
        }

        protected override void ExecuteSpecialAction(eAccionSR valAction) {
            if (valAction == eAccionSR.ReImprimir) {
                CloseOnActionComplete = true;
                DialogResult = true;
                clsCompraInformesViewModel insViewModel = new clsCompraInformesViewModel();
                insViewModel.ConfigReportCompra(Consecutivo, NumeroDeOrdenDeCompra);
            }

        }

        protected override bool CreateRecord() {
            bool vContinue = true;
            string vTextIN = string.Empty;
            if (GenerarCXP) {
                Views.InputDialog inputDialog = new Views.InputDialog("Introduzca el Número de Control", "");
                inputDialog.Title = "Número de Control de la CxP";
                if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                    vContinue = inputDialog.ShowDialog() == true;
                }
                if (vContinue) {
                    vTextIN = inputDialog.Answer;
                    vTextIN = LibText.Left(vTextIN, 20);
                } else {
                    throw new GalacException("Debe asignar el Número de Control, para poder Continuar.", eExceptionManagementType.Alert);
                }
            }
            bool vResut = base.CreateRecord();
            if (vResut) {
                if (GenerarCXP && vContinue) {
                    ICompraPdn vPdn = new clsCompraNav();
                    vPdn.GenerarCxP(Model, vTextIN, Action);
                }
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "ImprimirCompraAlInsertar")) {
                    string vNumeroOperacion = Numero;
                    if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                        vNumeroOperacion = Serie + "-" + Numero;
                    }
                    if (LibMessages.MessageBox.YesNo(this, "Se va a imprimir el documento " + vNumeroOperacion + ". ¿Desea continuar con la impresión?", ModuleName)) {
                        clsCompraInformesViewModel insViewModel = new clsCompraInformesViewModel();
                        insViewModel.ConfigReportCompra(Consecutivo, NumeroDeOrdenDeCompra);
                    }
                }
                ActualizaElCostoUnitario();
                if (VieneDeOrdenDeCompra) {
                    CloseOnActionComplete = true;

                }
            }
            return vResut;
        }

        protected override bool UpdateRecord() {
            bool vResut = base.UpdateRecord();
            bool vContinue = true;
            string vTextIN = string.Empty;
            if (vResut) {
                if (GenerarCXP) {
                    Views.InputDialog inputDialog = new Views.InputDialog("Introduzca el Numero de Control", "");
                    inputDialog.Title = "Número de Control de la CxP";
                    if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                        vContinue = inputDialog.ShowDialog() == true;
                    }
                    if (vContinue) {
                        ICompraPdn vPdn = new clsCompraNav();
                        vTextIN = inputDialog.Answer;
                        vTextIN = LibString.Left(vTextIN, 20);
                        vPdn.GenerarCxP(Model, vTextIN, Action, _NumeroDeCompraOriginal, _CodigoProveedorOriginal);
                    } else {
                        throw new GalacException("Debe asignar el Número de Control, para poder Continuar.", eExceptionManagementType.Alert);
                    }
                }
                ActualizaElCostoUnitario();
            }
            return vResut;
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

        private void ExecuteChooseCodigoAlmacenCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Saw.Gv_Almacen_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Saw.Gv_Almacen_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCodigoAlmacen = ChooseRecord<FkAlmacenViewModel>("Almacén", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoAlmacen != null) {
                    ConsecutivoAlmacen = ConexionCodigoAlmacen.Consecutivo;
                    CodigoAlmacen = ConexionCodigoAlmacen.Codigo;
                    NombreAlmacen = ConexionCodigoAlmacen.NombreAlmacen;
                } else {
                    ConsecutivoAlmacen = 0;
                    CodigoAlmacen = string.Empty;
                    NombreAlmacen = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseNumeroDeOrdenDeCompraCommand(string valNumero) {
            try {
                if (valNumero == null) {
                    valNumero = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Numero", valNumero);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_OrdendeCompra_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Adm.Gv_OrdendeCompra_B1.TipoDeCompra", LibConvert.EnumToDbValue((int)TipoDeCompra)), eLogicOperatorType.And);
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Adm.Gv_OrdendeCompra_B1.StatusOrdenDeCompra", LibConvert.EnumToDbValue((int)eStatusCompra.Vigente)), eLogicOperatorType.And);

                FkOrdenDeCompraViewModel vConexionNumeroDeOrdenDeCompra = ChooseRecord<FkOrdenDeCompraViewModel>("OrdenDeCompra", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (vConexionNumeroDeOrdenDeCompra != null && !SePuedeUsarOrdenDeCompra(vConexionNumeroDeOrdenDeCompra.ConsecutivoCompania, vConexionNumeroDeOrdenDeCompra.Consecutivo)) {
                    LibMessages.MessageBox.Alert(this, "Todos los artículos solicitados, a través de esta Orden ya fueron procesados en su totalidad." + Environment.NewLine + "Por favor, ingrese una orden diferente!", ModuleName);

                } else {
                    ConexionNumeroDeOrdenDeCompra = vConexionNumeroDeOrdenDeCompra;
                }
                if (ConexionNumeroDeOrdenDeCompra != null) {
                    NumeroDeOrdenDeCompra = ConexionNumeroDeOrdenDeCompra.Numero;
                    ConsecutivoOrdenDeCompra = ConexionNumeroDeOrdenDeCompra.Consecutivo;
                    ExecuteChooseNombreProveedorCommand(ConexionNumeroDeOrdenDeCompra.NombreProveedor);
                    LibSearchCriteria vDefaultCriteriaMoneda = LibSearchCriteria.CreateCriteriaFromText("Nombre", ConexionNumeroDeOrdenDeCompra.Moneda);
                    LibSearchCriteria vFixedCriteriaMoneda = LibSearchCriteria.CreateCriteria("Activa", LibConvert.BoolToSN(true));
                    ConexionCodigoMoneda = ChooseRecord<FkMonedaViewModel>("Moneda", vDefaultCriteriaMoneda, vFixedCriteriaMoneda, string.Empty);
                    if (ConexionCodigoMoneda != null) {
                        CodigoMoneda = ConexionCodigoMoneda.Codigo;
                        Moneda = ConexionCodigoMoneda.Nombre;
                    }
                    CambioAMonedaLocal = ConexionNumeroDeOrdenDeCompra.CambioABolivares;
                    Comentarios = ConexionNumeroDeOrdenDeCompra.Comentarios;
                    AsignarLosItemsDeLaOrdenDeCompra();
                    IsEnabledOrdenDeCompra = false;
                    RaisePropertyChanged(() => IsEnabledOrdenDeCompra);
                } else {
                    NumeroDeOrdenDeCompra = string.Empty;
                    ConsecutivoOrdenDeCompra = 0;

                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCodigoMonedaCommand(string valCodigo) {
            string vCodigoMonedaAnterior = CodigoMoneda;
            vMonedaLocal.InstanceMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Activa", LibConvert.BoolToSN(true));
                vFixedCriteria.Add("TipoDeMoneda", eBooleanOperatorType.IdentityEquality, eTipoDeMoneda.Fisica);
                if (ConexionNumeroDeOrdenDeCompra != null) {
                    vFixedCriteria.Add("Codigo", eBooleanOperatorType.IdentityEquality, vMonedaLocal.InstanceMonedaLocalActual.GetHoyCodigoMoneda());
                    vFixedCriteria.Add("Codigo", eBooleanOperatorType.IdentityEquality, ConexionNumeroDeOrdenDeCompra.CodigoMoneda, eLogicOperatorType.Or);
                }
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
                    ConexionCodigoMoneda = null;
                    ConexionCodigoMoneda = vConexionCodigoMoneda;
                    CodigoMoneda = ConexionCodigoMoneda.Codigo;
                    Moneda = ConexionCodigoMoneda.Nombre;
                    AsignaTasaDelDia(CodigoMoneda);
                    if (ConexionNumeroDeOrdenDeCompra != null && DetailCompraDetalleArticuloInventario.Items.Count > 0) {
                        ActualizarMontosPorCambioDeMoneda(vCodigoMonedaAnterior);
                    }
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

        private void ExecuteChooseMonedaCostoUltimaCompraCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Activa", LibConvert.BoolToSN(true));
                vFixedCriteria.Add("TipoDeMoneda", eBooleanOperatorType.IdentityEquality, eTipoDeMoneda.Fisica);
                ConexionMonedaCostoUltimaCompra = ChooseRecord<FkMonedaViewModel>("Moneda", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionMonedaCostoUltimaCompra != null) {
                    if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMonedaExtranjera") && LibString.S1IsEqualToS2(CodigoMoneda, ConexionMonedaCostoUltimaCompra.Codigo)) {
                        LibMessages.MessageBox.Information(this, $"La moneda para el costo ({  ConexionMonedaCostoUltimaCompra.Nombre  }) no debe ser igual a la moneda de la compra ({ Moneda })", "");
                        CodigoMonedaCostoUltimaCompra = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
                        ConexionMonedaCostoUltimaCompra = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteria("Codigo", CodigoMonedaCostoUltimaCompra));
                        MonedaCostoUltimaCompra = ConexionMonedaCostoUltimaCompra.Nombre;
                    } else {
                        CodigoMonedaCostoUltimaCompra = ConexionMonedaCostoUltimaCompra.Codigo;
                        MonedaCostoUltimaCompra = ConexionMonedaCostoUltimaCompra.Nombre;
                    }
                    AsignaTasaCostoUltimaCompraDelDia(CodigoMonedaCostoUltimaCompra);
                } else {
                    MonedaCostoUltimaCompra = string.Empty;
                    CodigoMonedaCostoUltimaCompra = string.Empty;
                    CambioCostoUltimaCompra = 1;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
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

        private void ExecuteVerDistribucion() {
            try {
                CompraDetalleDistribucionViewModel vViewModel = new CompraDetalleDistribucionViewModel(this);
                LibMessages.EditViewModel.ShowEditor(vViewModel, true);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private bool CanExecuteVerDistribucion() {
            return TipoDeDistribucion == eTipoDeDistribucion.Automatica;
        }

        #endregion

        #region Validations

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
                if (StatusCompra == eStatusCompra.Anulada) {
                    if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDeAnulacion, false, Action)) {
                        vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha De Anulacion"));
                    }
                }
            }
            return vResult;
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

        private ValidationResult TotalOtrosGastosvalidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (TipoDeDistribucion == eTipoDeDistribucion.ManualPorMonto) {
                    if (DetailCompraDetalleArticuloInventario.Items.Sum(p => p.MontoDistribucion) != DetailCompraDetalleGasto.Items.Sum(p => p.Monto)) {
                        vResult = new ValidationResult("El monto Distribuido debe ser igual al Monto Total de los Gastos.");
                    }
                }

            }
            return vResult;
        }

        private ValidationResult CodigoAlmacenValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (IsVisibleAlmacen) {
                    if (LibString.IsNullOrEmpty(CodigoAlmacen)) {
                        vResult = new ValidationResult("El campo Codigo Almacen es requerido.");
                    }
                }

            }
            return vResult;
        }

        private ValidationResult TasaAduaneraValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (TipoDeCompra == eTipoCompra.Importacion && TipoDeDistribucion == eTipoDeDistribucion.Automatica && TasaAduanera == 0) {
                    vResult = new ValidationResult("El campo Tasa Aduanera es requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult TasaDolarValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (TipoDeCompra == eTipoCompra.Importacion && TipoDeDistribucion == eTipoDeDistribucion.Automatica && TasaDolar == 0) {
                    vResult = new ValidationResult("El campo Tasa Dolar es requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult TipoDeDistribucionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (TipoDeCompra == eTipoCompra.Importacion && TipoDeDistribucion == eTipoDeDistribucion.Ninguno) {
                    vResult = new ValidationResult("El campo Tipo De Distribucion es requerido.");
                }
            }
            return vResult;
        }

        #endregion

        #region Metodos Generados

        protected override Compra FindCurrentRecord(Compra valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "CompraGET", vParams.Get(), UseDetail).FirstOrDefault();
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<Compra>, IList<Compra>> GetBusinessComponent() {
            return new clsCompraNav();
        }

        private int GenerarProximoConsecutivo() {
            int vResult = 0;
            XElement vResulset = GetBusinessComponent().QueryInfo(eProcessMessageType.Message, "ProximoConsecutivo", Mfc.GetIntAsParam("Compania"), false);
            vResult = LibConvert.ToInt(LibXml.GetPropertyString(vResulset, "Consecutivo"));
            return vResult;
        }

        protected override void ReloadRelatedConnections() {
            //    base.ReloadRelatedConnections();
            //   ConexionCodigoProveedor = FirstConnectionRecordOrDefault<FkProveedorViewModel>("Proveedor", LibSearchCriteria.CreateCriteria("codigoProveedor", CodigoProveedor));
            //  ConexionCodigoAlmacen = FirstConnectionRecordOrDefault<FkAlmacenViewModel>("Almacén", LibSearchCriteria.CreateCriteria("Codigo", CodigoAlmacen));
            // ConexionNumeroDeOrdenDeCompra = FirstConnectionRecordOrDefault<FkCompraViewModel>("Compra", LibSearchCriteria.CreateCriteria("Numero", NumeroDeOrdenDeCompra));
            ConexionMonedaCostoUltimaCompra = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteria("Codigo", CodigoMonedaCostoUltimaCompra));
        }

        #endregion //Metodos Generados

        #region Metodos

        bool Continue = true;

        private void ActualizarDistribucion() {
            if (Continue) {
                Continue = false;
                foreach (var item in DetailCompraDetalleArticuloInventario.Items) {
                    item.ActualizaCostoUnitario();
                }
                Continue = true;
            }
        }

        private void ActualizaTotales() {
            TotalRenglones = DetailCompraDetalleArticuloInventario.Items.Sum(p => p.Cantidad * p.PrecioUnitario);
            TotalOtrosGastos = DetailCompraDetalleGasto.Items.Sum(P => P.Monto);
            TotalCompra = TotalRenglones + TotalOtrosGastos;
            RaisePropertyChanged(DifereciaDistribucionPropertyName);
        }

        private void ActualizaElCostoUnitario() {
            bool vEsMonedaLocal = true;
            string NombreMonedaLocal = vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(LibDate.Today());
            if (Moneda != NombreMonedaLocal) {
                vEsMonedaLocal = false;
            }
            ICompraPdn vPdn = new clsCompraNav();
            string vNumeroOperacion = GetModel().Numero;
            if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                vNumeroOperacion = GetModel().Serie + "-" + GetModel().Numero;
            }
            vPdn.ActualizaElCostoUnitario(Model, vEsMonedaLocal);
            if (vPdn.SePuedeEjecutarElAjusteDePrecios()) {
                EjecutarAjustesdePreciosCostosUltimacompraSiEsElcaso();
            }
        }

        private void EjecutarAjustesdePreciosCostosUltimacompraSiEsElcaso() {
            string vNumeroOperacion = GetModel().Numero;
            bool vEsMonedaLocal = true;
            if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                vNumeroOperacion = GetModel().Serie + "-" + GetModel().Numero;
            }
            string vCodigoMonedaLocal = vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(LibDate.Today());
            if (Moneda != vCodigoMonedaLocal) {
                vEsMonedaLocal = false;
            }
            AjusteDePrecioPorCostosViewModel vViewModel = new AjusteDePrecioPorCostosViewModel(vNumeroOperacion, GetModel().Fecha, true, vEsMonedaLocal);
            bool result = LibMessages.EditViewModel.ShowEditor(vViewModel, true);
        }

        protected override bool RecordIsReadOnly() {
            return (Action == eAccionSR.ReImprimir) || base.RecordIsReadOnly();
        }

        private void BuscaTasaDolar() {
            ICompraPdn vCompraPdn = new clsCompraNav();
            if (TipoDeDistribucion == eTipoDeDistribucion.Automatica) {
                TasaDolar = vCompraPdn.TasaDeDolarVigente(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania"));
            }
        }

        private LibRibbonGroupData CreateAdicionalRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Ver Distribución",
                Command = VerDistribucionCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/report.png", UriKind.Relative),
                ToolTipDescription = "Ver Distribución",
                ToolTipTitle = "Ver Distribución"
            });
            return vResult;
        }

        internal bool BuscarCodigoRepetidoEnElGrid(string valCodigo) {
            var vList = DetailCompraDetalleArticuloInventario.Items.Where(p => p.CodigoArticulo == valCodigo || p.CodigoArticuloInv == valCodigo).Select(p => p);
            if (vList != null && vList.Count() >= 1) {
                int vIndex = DetailCompraDetalleArticuloInventario.Items.IndexOf(vList.First());
                if (vIndex != DetailCompraDetalleArticuloInventario.Items.IndexOf(DetailCompraDetalleArticuloInventario.SelectedItem)) {
                    LibMessages.MessageBox.Alert(this, "El artículo que está intentando ingresar ya se encuentra en el Grid por favor dirijase a la linea " + (vIndex + 1).ToString() + Environment.NewLine +
                    "sí desea Agregar o Modificar alguna información del Artículo " + valCodigo, "INFORMACIÓN");

                    return true;
                }
            }
            return false;
        }

        private void EjecutarProcesosMensajeDeRecalcularSiEsElCaso(LibBusinessProcessMessage valMessage) {
            valMessage.Result = LibMessages.MessageBox.Alert(null, valMessage.Content.ToString(), ModuleName);
        }

        private void ActualizaVisiblePorTipoDeDistribucion() {
            if (DetailCompraDetalleArticuloInventario.HasItems) {
                CompraDetalleArticuloInventarioViewModel vDetailViewModel = DetailCompraDetalleArticuloInventario.SelectedItem;
                vDetailViewModel.RaiseVisiblePorTipoDeDistribucion();
            }
        }

        private void AsignarLosItemsDeLaOrdenDeCompra() {
            Compra vModel = new Compra();
            if (Action == eAccionSR.Insertar) {
                ((ICompraPdn)GetBusinessComponent()).AsignarDetalleArticuloInventarioDesdeOrdenDeCompra(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), vModel, ConsecutivoOrdenDeCompra);
                DetailCompraDetalleArticuloInventario.ActualizaDetalleDesdeOrdenDeCompra(this, vModel.DetailCompraDetalleArticuloInventario, Action);
            }
            ActualizaTotales();
        }

        private bool SePuedeUsarOrdenDeCompra(int valConsecutivoCompania, int valConsecutivoordenDeCompra) {
            return ((ICompraPdn)GetBusinessComponent()).VerificaExistenciaEnOrdenDeCompra(valConsecutivoCompania, valConsecutivoordenDeCompra);
        }

        private void ActualizaVisibleUsaDeSeguro() {
            if (DetailCompraDetalleArticuloInventario.HasItems) {
                CompraDetalleArticuloInventarioViewModel vDetailViewModel = DetailCompraDetalleArticuloInventario.SelectedItem;
                vDetailViewModel.RaiseVisibleUsaSeguro();
            }
        }

        public bool AsignaTasaDelDia(string valCodigoMoneda) {
            vMonedaLocal.InstanceMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            if (!vMonedaLocal.InstanceMonedaLocalActual.EsMonedaLocalDelPais(valCodigoMoneda)) {
                decimal vTasa = 1;
                ConexionCodigoMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigoMoneda));
                CodigoMoneda = ConexionCodigoMoneda.Codigo;
                Moneda = ConexionCodigoMoneda.Nombre;
                if (((ICambioPdn)new clsCambioNav()).ExisteTasaDeCambioParaElDia(CodigoMoneda, Fecha, out vTasa)) {
                    CambioAMonedaLocal = vTasa;
                    return true;
                } else {
                    _ElProgramaEstaEnModoAvanzado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsModoAvanzado");
                    _UsarLimiteMaximoParaIngresoDeTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarLimiteMaximoParaIngresoDeTasaDeCambio");
                    _MaximoLimitePermitidoParaLaTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "MaximoLimitePermitidoParaLaTasaDeCambio");
                    CambioViewModel vViewModel = new CambioViewModel(valCodigoMoneda, _UsarLimiteMaximoParaIngresoDeTasaDeCambio, _MaximoLimitePermitidoParaLaTasaDeCambio, _ElProgramaEstaEnModoAvanzado);
                    vViewModel.InitializeViewModel(eAccionSR.Insertar);
                    vViewModel.OnCambioAMonedaLocalChanged += CambioChanged;
                    vViewModel.FechaDeVigencia = Fecha;
                    vViewModel.CodigoMoneda = CodigoMoneda;
                    vViewModel.NombreMoneda = Moneda;
                    vViewModel.IsEnabledFecha = false;
                    bool vResult = LibMessages.EditViewModel.ShowEditor(vViewModel, true);
                    if (!vResult) {
                        if (LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaDivisaComoMonedaPrincipalDeIngresoDeDatos"))) {
                            return false;
                        }
                        AsignarValoresDeMonedaPorDefecto();
                    }
                    return true;
                }
            } else {
                CodigoMoneda = vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(Fecha);
                Moneda = vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(Fecha);
                CambioAMonedaLocal = 1;
                return true;
            }
        }

        private void CambioChanged(decimal valCambio) {
            CambioAMonedaLocal = valCambio;
        }

        private void CambioUltimaCompraChanged(decimal valCambio) {
            CambioCostoUltimaCompra = valCambio;
        }
        private void AsignarValoresDeMonedaPorDefecto() {
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

        private void ActualizarMontosPorCambioDeMoneda(string valCodigoMonedaAnterior) {
            decimal vCambioParaRecalcular = 1;
            vMonedaLocal.InstanceMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            if (CodigoMoneda != ConexionNumeroDeOrdenDeCompra.CodigoMoneda
                && CodigoMoneda != valCodigoMonedaAnterior
                && LibMessages.MessageBox.YesNo(this, $"¿Desea realizar la conversión de los montos a {ConexionCodigoMoneda.Nombre}?", "Recalcular Montos")) {
                vCambioParaRecalcular = EscogerTasaDeCambioParaLaConversion();
                if (vCambioParaRecalcular > 0) {
                    foreach (var item in DetailCompraDetalleArticuloInventario.Items) {
                        item.PrecioUnitario = item.PrecioUnitario * vCambioParaRecalcular;
                    }
                } else {
                    DetailCompraDetalleArticuloInventario.Items.Clear();
                    ExecuteChooseNumeroDeOrdenDeCompraCommand(ConexionNumeroDeOrdenDeCompra.Numero);
                }
            } else if (CodigoMoneda == ConexionNumeroDeOrdenDeCompra.CodigoMoneda && CodigoMoneda != valCodigoMonedaAnterior) {
                DetailCompraDetalleArticuloInventario.Items.Clear();
                ExecuteChooseNumeroDeOrdenDeCompraCommand(ConexionNumeroDeOrdenDeCompra.Numero);
            }
        }

        private decimal EscogerTasaDeCambioParaLaConversion() {
            decimal vCambioResult = 1;
            if (Fecha == ConexionNumeroDeOrdenDeCompra.Fecha
                && !vMonedaLocal.InstanceMonedaLocalActual.EsMonedaLocalDelPais(ConexionNumeroDeOrdenDeCompra.CodigoMoneda)) {
                vCambioResult = ConexionNumeroDeOrdenDeCompra.CambioABolivares;
            } else if (Fecha != ConexionNumeroDeOrdenDeCompra.Fecha
                && !vMonedaLocal.InstanceMonedaLocalActual.EsMonedaLocalDelPais(ConexionNumeroDeOrdenDeCompra.CodigoMoneda)) {
                decimal vCambioDelDía = 1;
                bool vExisteCambioDelDía = ((ICambioPdn)new clsCambioNav()).ExisteTasaDeCambioParaElDia(ConexionNumeroDeOrdenDeCompra.CodigoMoneda, Fecha, out vCambioDelDía);
                StringBuilder vMessage = new StringBuilder();
                vMessage.AppendLine($"La Orden de Compra {ConexionNumeroDeOrdenDeCompra.Numero}, realizada con moneda {ConexionNumeroDeOrdenDeCompra.Moneda} el día {ConexionNumeroDeOrdenDeCompra.Fecha.ToShortDateString()}, ");
                vMessage.Append($"fue registrada con una Tasa de Cambio de {LibConvert.ToStr(ConexionNumeroDeOrdenDeCompra.CambioABolivares, 4)}. ");
                if (vExisteCambioDelDía) {
                    vMessage.Append($"Para el día de hoy la Tasa de Cambio de la moneda {ConexionNumeroDeOrdenDeCompra.Moneda} " +
                        $"es de {LibConvert.ToStr(vCambioDelDía, 2)}");
                    vMessage.AppendLine();
                    vMessage.AppendLine();
                    vMessage.AppendLine("¿Desea realizar la conversíón de los montos con la tasa de Cambio del día?");
                } else {
                    vMessage.AppendLine();
                    vMessage.AppendLine();
                    vMessage.AppendLine("¿Desea ingresar la Tasa de Cambio del día de hoy para realizar la conversión de los montos con la tasa de día?");
                }
                if (LibMessages.MessageBox.YesNo(this, vMessage.ToString(), "")) {
                    if (vExisteCambioDelDía) {
                        vCambioResult = vCambioDelDía;
                    } else {
                        _ElProgramaEstaEnModoAvanzado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsModoAvanzado");
                        _UsarLimiteMaximoParaIngresoDeTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarLimiteMaximoParaIngresoDeTasaDeCambio");
                        _MaximoLimitePermitidoParaLaTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "MaximoLimitePermitidoParaLaTasaDeCambio");
                        CambioViewModel vViewModel = new CambioViewModel(ConexionNumeroDeOrdenDeCompra.CodigoMoneda, _UsarLimiteMaximoParaIngresoDeTasaDeCambio, _MaximoLimitePermitidoParaLaTasaDeCambio, _ElProgramaEstaEnModoAvanzado);
                        vViewModel.InitializeViewModel(eAccionSR.Insertar);
                        vViewModel.FechaDeVigencia = LibDate.Today();
                        vViewModel.IsEnabledFecha = false;
                        bool vResult = LibMessages.EditViewModel.ShowEditor(vViewModel, true);
                        if (!vResult) {
                            vCambioResult = 0;
                        } else {
                            ((ICambioPdn)new clsCambioNav()).ExisteTasaDeCambioParaElDia(ConexionNumeroDeOrdenDeCompra.CodigoMoneda, Fecha, out vCambioResult);
                        }
                    }
                } else {
                    vCambioResult = ConexionNumeroDeOrdenDeCompra.CambioABolivares;
                }
            }
            return vCambioResult;
        }

        private void ActualizarNumeroYCodigoProveedorOriginal() {
            if (Action == eAccionSR.Modificar) {
                _NumeroDeCompraOriginal = Model.Numero;
                _CodigoProveedorOriginal = Model.CodigoProveedor;
            }
        }
        private void AsignarMensajeDeGeneracionOActualizacionDeCxP() {
            GenerarOActualizarCxP = "Generar CxP";
            if (Action == eAccionSR.Modificar) {
                if (Model.GenerarCXPAsBool) {
                    GenerarOActualizarCxP = "Actualizar CxP";
                }
            }
        }

        private void AsignarValoresDeCostoMonedaUltimaCompraPorDefecto() {
            string vCodigoMoneda;
            bool vUsaMonedaExtranejera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMonedaExtranjera") || LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaListaDePrecioEnMonedaExtranjera");
            if (vUsaMonedaExtranejera && (LibString.S1IsEqualToS2(vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today()), Model.CodigoMoneda) || LibString.S1IsEqualToS2(vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(LibDate.Today()), Model.Moneda))) {
                vCodigoMoneda = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
                AsignaTasaCostoUltimaCompraDelDia(vCodigoMoneda);
                FkMonedaViewModel vConexionMonedaCostoUltimaCompra = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", vCodigoMoneda));
                CodigoMonedaCostoUltimaCompra = vConexionMonedaCostoUltimaCompra.Codigo;
                MonedaCostoUltimaCompra = vConexionMonedaCostoUltimaCompra.Nombre;
            } else {
                MonedaCostoUltimaCompra = vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(LibDate.Today());
                CodigoMonedaCostoUltimaCompra = vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today());
                CambioCostoUltimaCompra = 1;
            }
        }

        private bool AsignaTasaCostoUltimaCompraDelDia(string valCodigoMoneda) {
            bool vUsaMonedaExtranejera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMonedaExtranjera") || LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaListaDePrecioEnMonedaExtranjera");
            vMonedaLocal.InstanceMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            if (vMonedaLocal.InstanceMonedaLocalActual.EsMonedaLocalDelPais(valCodigoMoneda) || !vUsaMonedaExtranejera) {
                CambioCostoUltimaCompra = 1;
                return true;
            } else {
                decimal vTasa = 1;
                FkMonedaViewModel vConexionMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigoMoneda));
                if (((ICambioPdn)new clsCambioNav()).ExisteTasaDeCambioParaElDia(vConexionMoneda.Codigo, Fecha, out vTasa)) {
                    CambioCostoUltimaCompra = vTasa;
                    return true;
                } else {
                    _ElProgramaEstaEnModoAvanzado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsModoAvanzado");
                    _UsarLimiteMaximoParaIngresoDeTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarLimiteMaximoParaIngresoDeTasaDeCambio");
                    _MaximoLimitePermitidoParaLaTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "MaximoLimitePermitidoParaLaTasaDeCambio");
                    CambioViewModel vViewModel = new CambioViewModel(valCodigoMoneda, _UsarLimiteMaximoParaIngresoDeTasaDeCambio, _MaximoLimitePermitidoParaLaTasaDeCambio, _ElProgramaEstaEnModoAvanzado);
                    vViewModel.InitializeViewModel(eAccionSR.Insertar);
                    vViewModel.OnCambioAMonedaLocalChanged += CambioUltimaCompraChanged;
                    vViewModel.FechaDeVigencia = Fecha;
                    vViewModel.CodigoMoneda = vConexionMoneda.Codigo;
                    vViewModel.NombreMoneda = vConexionMoneda.Nombre;
                    vViewModel.IsEnabledFecha = false;
                    bool vResult = LibMessages.EditViewModel.ShowEditor(vViewModel, true);
                    if (!vResult) {
                        if (LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaDivisaComoMonedaPrincipalDeIngresoDeDatos"))) {
                            return false;
                        }
                        CambioCostoUltimaCompra = 1;
                    }
                    return true;
                }
            }
        }
        public bool IsEnabledCambioCostoUltimaCompra {
            get {
                return IsEnabled && LibString.S1IsEqualToS2(vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today()), CodigoMoneda);
            }
        }
        public bool IsVisibleMonedaParaCostos {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMonedaExtranjera") || LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaListaDePrecioEnMonedaExtranjera");
            }
        }
        #endregion
    } //End of class CompraViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

