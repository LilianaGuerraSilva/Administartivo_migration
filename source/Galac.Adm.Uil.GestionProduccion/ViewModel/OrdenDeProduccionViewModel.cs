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
using Galac.Saw.Lib;
using System.Collections.ObjectModel;
using System.Security.Policy;

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
        private const string CodigoMonedaCostoProduccionPropertyName = "CodigoMonedaCostoProduccion";
        private const string MonedaPropertyName = "Moneda";
        private const string CambioCostoProduccionPropertyName = "CambioCostoProduccion";
        private const string CodigoListaDeMaterialesPropertyName = "CodigoListaDeMateriales";
        private const string NombreListaDeMaterialesPropertyName = "NombreListaDeMateriales";
        private const string CantidadAProducirPropertyName = "CantidadAProducir";
        private const string CantidadProducidaPropertyName = "CantidadProducida";
        private const string NombreOperadorPropertyName = "NombreOperador";
        private const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        private const string LabelFormaDeCalcularCostosDeProduccionPropertyName = "LabelFormaDeCalcularCostosDeProduccion";
        #endregion
        #region Variables

        private FkAlmacenViewModel _ConexionCodigoAlmacenProductoTerminado = null;
        private FkAlmacenViewModel _ConexionCodigoAlmacenMateriales = null;
        private FkMonedaViewModel _ConexionMoneda = null;
        private FkListaDeMaterialesViewModel _ConexionCodigoListaDeMateriales = null;
        private Saw.Lib.clsNoComunSaw vMonedaLocal = null;
        private FkOrdenDeProduccionViewModel _ConexionCodigoOrdenProduccion = null;
        private decimal _TotalPorcentajeCosto;
        private decimal _TotalPorcentajeCostoCierre;

        #endregion //Variables

        #region Propiedades

        public override string ModuleName {
            get {
                return "Orden de Producción";
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

        public string CodigoMoneda {
            get; set;
        }

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

        public decimal CambioMoneda {
            get; set;
        }

        [LibRequired(ErrorMessage = "El campo Codigo Lista De Materiales es requerido.")]
        public string CodigoListaDeMateriales {
            get {
                return Model.CodigoListaDeMateriales;
            }
            set {
                if (Model.CodigoListaDeMateriales != value) {
                    Model.CodigoListaDeMateriales = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoListaDeMaterialesPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoListaDeMateriales, true)) {
                        ConexionCodigoListaDeMateriales = null;
                    }
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nombre Lista De Materiales es requerido.")]
        public string NombreListaDeMateriales {
            get {
                return Model.NombreListaDeMateriales;
            }
            set {
                if (Model.NombreListaDeMateriales != value) {
                    Model.NombreListaDeMateriales = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreListaDeMaterialesPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Cantidad a Producir es requerido.")]
        public decimal CantidadAProducir {
            get {
                return Model.CantidadAProducir;
            }
            set {
                if (Model.CantidadAProducir != value) {
                    Model.CantidadAProducir = value;
                    IsDirty = true;
                    ActualizaCantidadEnDetalles();
                    RaisePropertyChanged(CantidadAProducirPropertyName);
                }
            }
        }

        [LibCustomValidation("CantidadProducidaValidating")]
        public decimal CantidadProducida {
            get {
                return Model.CantidadProducida;
            }
            set {
                if (Model.CantidadProducida != value) {
                    Model.CantidadProducida = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadProducidaPropertyName);
                    RecalcularCantidadProducida();
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

        public eTipoStatusOrdenProduccion[] ArrayTipoStatusOrdenProduccion {
            get {
                return LibEnumHelper<eTipoStatusOrdenProduccion>.GetValuesInArray();
            }
        }

        [LibCustomValidation("ValidateDetalleDeOrdenDeProduccion")]
        public OrdenDeProduccionDetalleArticuloMngViewModel DetailOrdenDeProduccionDetalleArticulo {
            get;
            set;
        }

        [LibDetailRequired(ErrorMessage = "Insumos es requerido.")]
        public OrdenDeProduccionDetalleMaterialesMngViewModel DetailOrdenDeProduccionDetalleMateriales {
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

        public FkListaDeMaterialesViewModel ConexionCodigoListaDeMateriales {
            get {
                return _ConexionCodigoListaDeMateriales;
            }
            set {
                if (_ConexionCodigoListaDeMateriales != value) {
                    _ConexionCodigoListaDeMateriales = value;
                    RaisePropertyChanged(CodigoListaDeMaterialesPropertyName);
                }
                if (_ConexionCodigoListaDeMateriales == null) {
                    CodigoListaDeMateriales = string.Empty;
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

        public RelayCommand<string> ChooseCodigoListaDeMaterialesCommand {
            get;
            private set;
        }

        public RelayCommand<string> CreateOrdenDeProduccionDetalleArticuloCommand {
            get {
                return DetailOrdenDeProduccionDetalleArticulo.CreateCommand;
            }
        }

        public RelayCommand<string> UpdateOrdenDeProduccionDetalleArticuloCommand {
            get {
                return DetailOrdenDeProduccionDetalleArticulo.UpdateCommand;
            }
        }

        public RelayCommand<string> DeleteOrdenDeProduccionDetalleArticuloCommand {
            get {
                return DetailOrdenDeProduccionDetalleArticulo.DeleteCommand;
            }
        }

        public RelayCommand<string> CreateOrdenDeProduccionDetalleMaterialesCommand {
            get {
                return DetailOrdenDeProduccionDetalleMateriales.CreateCommand;
            }
        }

        public RelayCommand<string> UpdateOrdenDeProduccionDetalleMaterialesCommand {
            get {
                return DetailOrdenDeProduccionDetalleMateriales.UpdateCommand;
            }
        }

        public RelayCommand<string> DeleteOrdenDeProduccionDetalleMaterialesCommand {
            get {
                return DetailOrdenDeProduccionDetalleMateriales.DeleteCommand;
            }
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

        public bool IsEnabledCantidadProducida {
            get {
                return Action == eAccionSR.Cerrar;
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

        public bool IsEnabledCodigoListaDeMateriales {
            get {
                return (IsEnabled && Action == eAccionSR.Insertar);
            }
        }

        public bool IsVisibleTotalPorcentajeDeCosto {
            get {
                return !IsVisibleTotalPorcentajeDeCostoCierre;
            }
        }

        public bool IsVisibleTotalPorcentajeDeCostoCierre {
            get {
                return StatusOp == eTipoStatusOrdenProduccion.Cerrada || Action == eAccionSR.Cerrar;
            }
        }

        public bool IsVisibleSumMontoSubTotal {
            get { return StatusOp == eTipoStatusOrdenProduccion.Cerrada || Action == eAccionSR.Cerrar; }
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
                return Model.NumeroDecimales;
            }
        }
        public bool IsVisibleEscogerCodigoOrdenProduccion {
            get {
                return Action == eAccionSR.Contabilizar;
            }
        }

        public bool IsVisibleEditarCodigoDocumento {
            get {
                return !IsVisibleEscogerCodigoOrdenProduccion;
            }
        }

        public RelayCommand<string> ChooseCodigoOrdenProduccionCommand {
            get;
            private set;
        }

        public bool IsEnabledCodigo {
            get {
                return IsEnabled && (Action != eAccionSR.Contabilizar);
            }
        }

        public bool IsEnabledCodigoOP {
            get {
                return IsVisibleEscogerCodigoOrdenProduccion;
            }
        }

        public bool IsVisbleCantidadProducida {
            get {
                if (Action == eAccionSR.Contabilizar) {
                    return (Model.Consecutivo != 0);
                } else {
                    return DetailOrdenDeProduccionDetalleArticulo.Items[0].IsVisibleFechaFinalizacion;
                }
            }
        }

        [LibCustomValidation("TotalPorcentajeDeCostoValidating")]
        public decimal TotalPorcentajeDeCosto {
            get {
                return _TotalPorcentajeCosto;
            }
            set {
                if (_TotalPorcentajeCosto != value) {
                    _TotalPorcentajeCosto = value;
                    IsDirty = true;
                    RaisePropertyChanged(() => TotalPorcentajeDeCosto);
                }
            }
        }
                
        [LibCustomValidation("TotalPorcentajeDeCostoCierreValidating")]
        public decimal TotalPorcentajeDeCostoCierre {
            get {
                return _TotalPorcentajeCostoCierre;
            }
            set {
                if (_TotalPorcentajeCostoCierre != value) {
                    _TotalPorcentajeCostoCierre = value;
                    IsDirty = true;
                    RaisePropertyChanged(() => TotalPorcentajeDeCostoCierre);
                }
            }
        }

        decimal _SumMontoSubTotal;
        public decimal SumMontoSubTotal {
            get { return _SumMontoSubTotal; }
            set {
                if (_SumMontoSubTotal != value) {
                    _SumMontoSubTotal = value;
                    IsDirty = true;
                    RaisePropertyChanged(() => _SumMontoSubTotal);
                }
            }
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
            if (Action == eAccionSR.Contabilizar) {
                CodigoMonedaCostoProduccion = Model.CodigoMonedaCostoProduccion;
                Moneda = Model.Moneda;
                AsignarNombreMoneda();
            } else {
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
            ChooseCodigoListaDeMaterialesCommand = new RelayCommand<string>(ExecuteChooseCodigoListaDeMaterialesCommand);
            if (Action == eAccionSR.Contabilizar) {
                ChooseCodigoOrdenProduccionCommand = new RelayCommand<string>(ExecuteChooseCodigoOrdenProduccionCommand);
            }
        }

        protected override void InitializeLookAndFeel(OrdenDeProduccion valModel) {
            base.InitializeLookAndFeel(valModel);
            if (LibString.IsNullOrEmpty(Codigo, true) && Action != eAccionSR.Contabilizar) {
                Codigo = GenerarProximoCodigo();
            }
        }

        protected override void InitializeDetails() {
            DetailOrdenDeProduccionDetalleArticulo = new OrdenDeProduccionDetalleArticuloMngViewModel(this, Model.DetailOrdenDeProduccionDetalleArticulo, Action);
            DetailOrdenDeProduccionDetalleArticulo.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleArticuloViewModel>>(DetailOrdenDeProduccionDetalleArticulo_OnCreated);
            DetailOrdenDeProduccionDetalleArticulo.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleArticuloViewModel>>(DetailOrdenDeProduccionDetalleArticulo_OnUpdated);
            DetailOrdenDeProduccionDetalleArticulo.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleArticuloViewModel>>(DetailOrdenDeProduccionDetalleArticulo_OnDeleted);
            DetailOrdenDeProduccionDetalleArticulo.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleArticuloViewModel>>(DetailOrdenDeProduccionDetalleArticulo_OnSelectedItemChanged);
            DetailOrdenDeProduccionDetalleMateriales = new OrdenDeProduccionDetalleMaterialesMngViewModel(this, Model.DetailOrdenDeProduccionDetalleMateriales, Action);
            DetailOrdenDeProduccionDetalleMateriales.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleMaterialesViewModel>>(DetailOrdenDeProduccionDetalleMateriales_OnCreated);
            DetailOrdenDeProduccionDetalleMateriales.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleMaterialesViewModel>>(DetailOrdenDeProduccionDetalleMateriales_OnUpdated);
            DetailOrdenDeProduccionDetalleMateriales.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleMaterialesViewModel>>(DetailOrdenDeProduccionDetalleMateriales_OnDeleted);
            DetailOrdenDeProduccionDetalleMateriales.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleMaterialesViewModel>>(DetailOrdenDeProduccionDetalleMateriales_OnSelectedItemChanged);
            ActualizaTotalProcentajeDeCosto();
            ActualizaSumMontoSubTotal();
            BuscarExistencia();
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
            return DetailOrdenDeProduccionDetalleArticulo != null && DetailOrdenDeProduccionDetalleMateriales.Items != null && DetailOrdenDeProduccionDetalleArticulo.Items.Count > 0 && DetailOrdenDeProduccionDetalleMateriales.Items.Count > 0;
        }

        public void ActualizaTotalProcentajeDeCosto() {
            TotalPorcentajeDeCosto = DetailOrdenDeProduccionDetalleArticulo.Items.Sum(s => s.PorcentajeCostoEstimado);
            TotalPorcentajeDeCostoCierre = DetailOrdenDeProduccionDetalleArticulo.Items.Sum(s =>s.PorcentajeCostoCierre);
        }

        public void ActualizaSumMontoSubTotal() {
            SumMontoSubTotal = DetailOrdenDeProduccionDetalleMateriales.Items.Sum(s => s.MontoSubtotal);
        }

        private void ExecuteVerDetalleCommand() {
            try {
                OrdenDeProduccionMasterViewModel vViewModel = new OrdenDeProduccionMasterViewModel(this);
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

        private ValidationResult CantidadProducidaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (Action == eAccionSR.Cerrar && CantidadProducida < 0) {
                return new ValidationResult("La cantidad producida no puede ser negativa");
            } else {
                return vResult;
            }
        }

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

        private ValidationResult ValidateDetalleDeOrdenDeProduccion() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Contabilizar)) {
                return ValidationResult.Success;
            } else {
                if (DetailOrdenDeProduccionDetalleArticulo == null || !DetailOrdenDeProduccionDetalleArticulo.IsValid || !DetailOrdenDeProduccionDetalleArticulo.HasItems) {
                    return new ValidationResult("Orden De Producción Detalle Artículo es requerido.");
                }

            }
            return vResult;
        }

        private ValidationResult TotalPorcentajeDeCostoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action != eAccionSR.Consultar) && (Action != eAccionSR.Eliminar)){
                ActualizaTotalProcentajeDeCosto();
                if (TotalPorcentajeDeCosto != 100) {
                    vResult = new ValidationResult("El porcentaje total de costo estimado en los artículos a producir debe ser igual a 100%.");
                }
            } else {
                return vResult;
            }
            return vResult;
        }


        private ValidationResult TotalPorcentajeDeCostoCierreValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (Action == eAccionSR.Cerrar) {
                ActualizaTotalProcentajeDeCosto();
                if (TotalPorcentajeDeCostoCierre != 100) {
                    vResult = new ValidationResult("El porcentaje total de costo de cierre en los artículos a producir debe ser igual a 100%.");
                }
            } else {
                return vResult;
            }
            return vResult;
        }

        #endregion //Validation

        #region Metodos Generados

        protected override OrdenDeProduccion FindCurrentRecord(OrdenDeProduccion valModel) {
            if (valModel == null) {
                return null;
            }
            if (Action == eAccionSR.Contabilizar && valModel.Consecutivo == 0) {
                return valModel;
            } else {
                LibGpParams vParamsConta = new LibGpParams();
                vParamsConta.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
                vParamsConta.AddInInteger("Consecutivo", valModel.Consecutivo);
                return BusinessComponent.GetData(eProcessMessageType.SpName, "OrdenDeProduccionGET", vParamsConta.Get(), UseDetail).FirstOrDefault();
            }

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

        #region OrdenDeProduccionDetalleMateriales

        private void DetailOrdenDeProduccionDetalleMateriales_OnSelectedItemChanged(object sender, SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleMaterialesViewModel> e) {
            try {
                UpdateOrdenDeProduccionDetalleMaterialesCommand.RaiseCanExecuteChanged();
                DeleteOrdenDeProduccionDetalleMaterialesCommand.RaiseCanExecuteChanged();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailOrdenDeProduccionDetalleMateriales_OnDeleted(object sender, SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleMaterialesViewModel> e) {
            try {
                IsDirty = true;
                Model.DetailOrdenDeProduccionDetalleMateriales.Remove(e.ViewModel.GetModel());
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailOrdenDeProduccionDetalleMateriales_OnUpdated(object sender, SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleMaterialesViewModel> e) {
            try {
                IsDirty = e.ViewModel.IsDirty;
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailOrdenDeProduccionDetalleMateriales_OnCreated(object sender, SearchCollectionChangedEventArgs<OrdenDeProduccionDetalleMaterialesViewModel> e) {
            try {
                Model.DetailOrdenDeProduccionDetalleMateriales.Add(e.ViewModel.GetModel());
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //OrdenDeProduccionDetalleMateriales

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
        }

        #endregion //Metodos Generados

        #region Metodos

        private void ActualizaAlmacenenProductoTerminadoEnDetalles() {
            foreach (OrdenDeProduccionDetalleArticulo vItem in Model.DetailOrdenDeProduccionDetalleArticulo) {
                vItem.ConsecutivoAlmacen = Model.ConsecutivoAlmacenProductoTerminado;
            }

        }

        internal void ActualizaAlmacenenMaterialesEnDetalles() {
            foreach (OrdenDeProduccionDetalleMateriales vItem in Model.DetailOrdenDeProduccionDetalleMateriales) {
                vItem.ConsecutivoAlmacen = Model.ConsecutivoAlmacenMateriales;
            }
        }

        protected override void ExecuteProcessBeforeAction() {
            if (Action != eAccionSR.Contabilizar) {
                if (Action == eAccionSR.Custom) {                   
                    if (DetailOrdenDeProduccionDetalleMateriales.Items
                        .Where(q => q.TipoDeArticulo == Saw.Ccl.Inventario.eTipoDeArticulo.Mercancia && q.Existencia < q.CantidadReservadaInventario).Count() > 0) {
                        if (!LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "PermitirSobregiro")) {
                            throw new GalacValidationException("No hay suficiente existencia de algunos materiales para producir este inventario.");
                        }
                    }
                }
            }
        }

        protected override bool ExecuteSpecialAction(string valCustomAction) {
            if (LibString.Equals(valCustomAction, "Iniciar")) {
                IList<OrdenDeProduccion> vList = new List<OrdenDeProduccion>();
                vList.Add(Model);
                DialogResult = GetBusinessComponent().DoAction(vList, Action, null, true).Success;
            } else if (LibString.Equals(valCustomAction, "Contabilizar")) {
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
                    if (_ConexionCodigoOrdenProduccion != null) {
                        Codigo = ConexionCodigoOrdenProduccion.Codigo;
                        StatusOp = ConexionCodigoOrdenProduccion.StatusOp;
                        FechaAnulacion = ConexionCodigoOrdenProduccion.FechaAnulacion;
                        FechaFinalizacion = ConexionCodigoOrdenProduccion.FechaFinalizacion;
                        RaisePropertyChanged(CodigoPropertyName);
                        RaisePropertyChanged(StatusOpPropertyName);
                        RaisePropertyChanged(FechaAnulacionPropertyName);
                        RaisePropertyChanged(FechaFinalizacionPropertyName);

                    }
                }
                if (_ConexionCodigoOrdenProduccion == null) {
                    Codigo = string.Empty;
                }
                ExecuteActionCommand.RaiseCanExecuteChanged();
            }
        }

        private bool AsignaTasaDelDia(string valCodigoMoneda, DateTime valFecha) {
            vMonedaLocal.InstanceMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            if (!EsMonedaLocal(valCodigoMoneda)) {
                bool vElProgramaEstaEnModoAvanzado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsModoAvanzado");
                bool vUsarLimiteMaximoParaIngresoDeTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarLimiteMaximoParaIngresoDeTasaDeCambio");
                decimal vMaximoLimitePermitidoParaLaTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros", "MaximoLimitePermitidoParaLaTasaDeCambio");
                bool vObtenerAutomaticamenteTasaDeCambioDelBCV = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "ObtenerAutomaticamenteTasaDeCambioDelBCV");
                CambioMoneda = clsSawCambio.InsertaTasaDeCambioParaElDia(CodigoMoneda, valFecha, vUsarLimiteMaximoParaIngresoDeTasaDeCambio, vMaximoLimitePermitidoParaLaTasaDeCambio, vElProgramaEstaEnModoAvanzado, vObtenerAutomaticamenteTasaDeCambioDelBCV);
                bool vResult = CambioMoneda > 0;
                ConexionMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigoMoneda));
                CodigoMoneda = ConexionMoneda.Codigo;
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

            } else {
                CodigoMoneda = vMonedaLocal.InstanceMonedaLocalActual.CodigoMoneda(valFecha);
                Moneda = vMonedaLocal.InstanceMonedaLocalActual.NombreMoneda(valFecha);
                CambioMoneda = 1;
                return true;
            }
        }

        private void ExecuteChooseCodigoListaDeMaterialesCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCodigoListaDeMateriales = ChooseRecord<FkListaDeMaterialesViewModel>("Lista de Materiales", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoListaDeMateriales != null) {
                    Model.ConsecutivoListaDeMateriales = ConexionCodigoListaDeMateriales.Consecutivo;
                    CodigoListaDeMateriales = ConexionCodigoListaDeMateriales.Codigo;
                    NombreListaDeMateriales = ConexionCodigoListaDeMateriales.Nombre;
                    CargarDetalles();
                } else {
                    Model.ConsecutivoListaDeMateriales = 0;
                    CodigoListaDeMateriales = string.Empty;
                    NombreListaDeMateriales = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void CargarDetalles() {
            ObservableCollection<OrdenDeProduccionDetalleMateriales> vListInsumos = new ObservableCollection<OrdenDeProduccionDetalleMateriales>(((IOrdenDeProduccionPdn)GetBusinessComponent()).ObtenerDetalleInicialInsumos(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), Model.ConsecutivoListaDeMateriales, ConexionCodigoAlmacenMateriales.Consecutivo, CantidadAProducir));
            ObservableCollection<OrdenDeProduccionDetalleArticulo> vListSalidas = new ObservableCollection<OrdenDeProduccionDetalleArticulo>(((IOrdenDeProduccionPdn)GetBusinessComponent()).ObtenerDetalleInicialSalidas(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), ConexionCodigoAlmacenProductoTerminado.Consecutivo, Model.ConsecutivoListaDeMateriales, CantidadAProducir));
            DetailOrdenDeProduccionDetalleMateriales = new OrdenDeProduccionDetalleMaterialesMngViewModel(this, vListInsumos, Action);
            DetailOrdenDeProduccionDetalleArticulo = new OrdenDeProduccionDetalleArticuloMngViewModel(this, vListSalidas, Action);
            Model.DetailOrdenDeProduccionDetalleMateriales = vListInsumos;
            Model.DetailOrdenDeProduccionDetalleArticulo = vListSalidas;
            ActualizaAlmacenenMaterialesEnDetalles();
			BuscarExistencia();
            ActualizaTotalProcentajeDeCosto();
            VerDetalleCommand.RaiseCanExecuteChanged();
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

        public string SinonimoListaDeMateriales {
            get {
                string vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreParaMostrarListaDeMateriales");
                if (LibString.IsNullOrEmpty(vResult)) {
                    vResult = "Lista de Materiales";
                }
                return vResult;
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

                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_OrdenDeProduccion_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                vFixedCriteria.Add("Adm.Gv_OrdenDeProduccion_B1.StatusOp", LibConvert.EnumToDbValue((int)eTipoStatusOrdenProduccion.Cerrada));
                vFixedCriteria.Add("Adm.Gv_OrdenDeProduccion_B1.FechaFinalizacion", eBooleanOperatorType.GreaterThanOrEqual, LibConvert.DateToDbValue(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("DatosDocumento", "FechaAperturaDelPeriodo")));
                vFixedCriteria.Add("Adm.Gv_OrdenDeProduccion_B1.FechaFinalizacion", eBooleanOperatorType.LessThanOrEqual, LibConvert.DateToDbValue(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("DatosDocumento", "FechaCierreDelPeriodo")));

                LibSearchCriteria vSearchcriteria = null;

                if (Action == LibGalac.Aos.Base.eAccionSR.Contabilizar) {
                    vSearchcriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_OrdenDeProduccion_B1.Consecutivo", valNumero);
                    vModuleName = "Orden de Producción a Contabilizar";
                }
                ConexionCodigoOrdenProduccion = ChooseRecord<FkOrdenDeProduccionViewModel>(vModuleName, vSearchcriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoOrdenProduccion == null) {
                    Codigo = "";
                } else {
                    Model.ConsecutivoCompania = ConexionCodigoOrdenProduccion.ConsecutivoCompania;
                    Model.Consecutivo = ConexionCodigoOrdenProduccion.Consecutivo;
                    Model.StatusOpAsEnum = ConexionCodigoOrdenProduccion.StatusOp;
                    Model.Descripcion = ConexionCodigoOrdenProduccion.Descripcion;
                    Model.Observacion = ConexionCodigoOrdenProduccion.Observacion;
                    Model.FechaCreacion = ConexionCodigoOrdenProduccion.FechaCreacion;
                    Model.CodigoAlmacenProductoTerminado = ConexionCodigoOrdenProduccion.ConsecutivoAlmacenProductoTerminado;
                    Model.CodigoAlmacenMateriales = ConexionCodigoOrdenProduccion.ConsecutivoAlmacenMateriales;
                    Model.CodigoMonedaCostoProduccion = ConexionCodigoOrdenProduccion.CodigoMonedaCostoProduccion;
                    ConexionMoneda = ChooseRecord<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", Model.CodigoMonedaCostoProduccion), null);
                    Model.Moneda = ConexionMoneda.Nombre;
                    Model.CambioCostoProduccion = ConexionCodigoOrdenProduccion.CambioCostoProduccion;
                    if (Model.Consecutivo != 0) {
                        AlmacenProductoTerminado();
                        AlmacenListaMateriales();
                    }
                    ReloadModel(FindCurrentRecord(Model));
                    InitializeDetails();
                    VerDetalleCommand.RaiseCanExecuteChanged();
                    ReloadRelatedConnections();
                    InitializeViewModel(eAccionSR.Contabilizar);
                    GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly)
                      .ToList().ForEach(p => RaisePropertyChanged(p.Name));
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void AlmacenProductoTerminado() {
            LibSearchCriteria vDefaultCriteriaAlmacen = LibSearchCriteria.CreateCriteriaFromText("Gv_Almacen_B1.Consecutivo", Model.CodigoAlmacenProductoTerminado);
            LibSearchCriteria vFixedCriteriaAlmacen = LibSearchCriteria.CreateCriteria("Gv_Almacen_B1.ConsecutivoCompania", Model.ConsecutivoCompania);
            ConexionCodigoAlmacenProductoTerminado = ChooseRecord<FkAlmacenViewModel>("Almacén", vDefaultCriteriaAlmacen, vFixedCriteriaAlmacen, string.Empty);
            if (ConexionCodigoAlmacenProductoTerminado != null) {
                Model.ConsecutivoAlmacenProductoTerminado = ConexionCodigoAlmacenProductoTerminado.Consecutivo;
                CodigoAlmacenProductoTerminado = ConexionCodigoAlmacenProductoTerminado.Codigo;
                NombreAlmacenProductoTerminado = ConexionCodigoAlmacenProductoTerminado.NombreAlmacen;
            } else {
                Model.ConsecutivoAlmacenProductoTerminado = 0;
                CodigoAlmacenProductoTerminado = string.Empty;
                NombreAlmacenProductoTerminado = string.Empty;
            }
        }

        private void AlmacenListaMateriales() {
            LibSearchCriteria vDefaultCriteriaAlmacenMateriales = LibSearchCriteria.CreateCriteriaFromText("Gv_Almacen_B1.Consecutivo", Model.CodigoAlmacenMateriales);
            LibSearchCriteria vFixedCriteriaAlmacenMateriales = LibSearchCriteria.CreateCriteria("Gv_Almacen_B1.ConsecutivoCompania", Model.ConsecutivoCompania);
            ConexionCodigoAlmacenMateriales = ChooseRecord<FkAlmacenViewModel>("Almacén", vDefaultCriteriaAlmacenMateriales, vFixedCriteriaAlmacenMateriales, string.Empty);
            if (ConexionCodigoAlmacenMateriales != null) {
                Model.ConsecutivoAlmacenMateriales = ConexionCodigoAlmacenMateriales.Consecutivo;
                CodigoAlmacenMateriales = ConexionCodigoAlmacenMateriales.Codigo;
                NombreAlmacenMateriales = ConexionCodigoAlmacenMateriales.NombreAlmacen;
            } else {
                Model.ConsecutivoAlmacenProductoTerminado = 0;
                CodigoAlmacenProductoTerminado = string.Empty;
                NombreAlmacenProductoTerminado = string.Empty;
            }
        }

        private void ActualizaCantidadEnDetalles() {
            foreach (OrdenDeProduccionDetalleMateriales vItem in Model.DetailOrdenDeProduccionDetalleMateriales) {
                vItem.CantidadReservadaInventario = LibMath.RoundToNDecimals(CantidadAProducir * vItem.Cantidad, DecimalDigits);
                if (Action == eAccionSR.Insertar) {
                    vItem.CantidadConsumida = vItem.CantidadReservadaInventario;
                }
            }
            foreach (OrdenDeProduccionDetalleArticulo vItem in Model.DetailOrdenDeProduccionDetalleArticulo) {
                vItem.CantidadSolicitada = LibMath.RoundToNDecimals(CantidadAProducir * vItem.CantidadOriginalLista, DecimalDigits);
                if (Action == eAccionSR.Insertar) {
                    vItem.CantidadProducida = vItem.CantidadSolicitada;
                }
            }
        }

        private void BuscarExistencia() {
            IOrdenDeProduccionDetalleMaterialesPdn vOrdenDeProduccionDetalleMateriales = new clsOrdenDeProduccionDetalleMaterialesNav();
            foreach (OrdenDeProduccionDetalleMaterialesViewModel item in DetailOrdenDeProduccionDetalleMateriales.Items) {
                item.Existencia = vOrdenDeProduccionDetalleMateriales.BuscaExistenciaDeArticulo(ConsecutivoCompania, item.CodigoArticulo, Model.ConsecutivoAlmacenMateriales);
            }
        }

        private void RecalcularCantidadProducida() {
            decimal vCantidadProducidaNew;
            if (Action == eAccionSR.Cerrar) {
                foreach (OrdenDeProduccionDetalleArticulo vItem in Model.DetailOrdenDeProduccionDetalleArticulo) {
                    vCantidadProducidaNew = LibMath.RoundToNDecimals(CantidadProducida * vItem.CantidadOriginalLista, DecimalDigits);
                    if (vItem.CantidadSolicitada != vCantidadProducidaNew) {
                        vItem.CantidadProducida = vCantidadProducidaNew;
                        vItem.CantidadAjustada = vItem.CantidadSolicitada - vItem.CantidadProducida;
                    }
                }
            }
        }


        #endregion //Metodos
    } //End of class OrdenDeProduccionViewModel

} //End of namespace Galac.Adm.Uil.GestionProduccion