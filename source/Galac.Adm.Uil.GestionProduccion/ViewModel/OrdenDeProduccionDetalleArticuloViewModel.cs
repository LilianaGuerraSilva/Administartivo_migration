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
    public class OrdenDeProduccionDetalleArticuloViewModel : LibInputDetailViewModelMfc<OrdenDeProduccionDetalleArticulo> {

        #region Constantes

        private const string CodigoListaDeMaterialesPropertyName = "CodigoListaDeMateriales";
        private const string NombreListaDeMaterialesPropertyName = "NombreListaDeMateriales";
        private const string CodigoAlmacenPropertyName = "CodigoAlmacen";
        private const string NombreAlmacenPropertyName = "NombreAlmacen";
        private const string CodigoArticuloPropertyName = "CodigoArticulo";
        private const string DescripcionArticuloPropertyName = "DescripcionArticulo";
        private const string CantidadSolicitadaPropertyName = "CantidadSolicitada";
        private const string CantidadProducidaPropertyName = "CantidadProducida";
        private const string AjustadoPostCierrePropertyName = "AjustadoPostCierre";
        private const string CantidadAjustadaPropertyName = "CantidadAjustada";

        #endregion

        #region Variables

        private FkListaDeMaterialesViewModel _ConexionCodigoListaDeMateriales = null;
        private FkAlmacenViewModel _ConexionCodigoAlmacen = null;

        #endregion //Variables

        #region Propiedades

        public override string ModuleName {
            get { return "Orden De Produccion Detalle Articulo"; }
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

        public int ConsecutivoListaDeMateriales {
            get {
                return Model.ConsecutivoListaDeMateriales;
            }
            set {
                if (Model.ConsecutivoListaDeMateriales != value) {
                    Model.ConsecutivoListaDeMateriales = value;
                }
            }
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

        [LibRequired(ErrorMessage = "El campo Código de Artículo es requerido.")]
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

        [LibRequired(ErrorMessage = "El campo Descripción Artículo es requerido.")]
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

        [LibRequired(ErrorMessage = "El campo Cantidad Solicitada es requerido.")]
        public decimal CantidadSolicitada {
            get {
                return Model.CantidadSolicitada;
            }
            set {
                if (Model.CantidadSolicitada != value) {
                    Model.CantidadSolicitada = value;
                    IsDirty = true;
                    ActualizaCantidadEnDetalles();
                    RaisePropertyChanged(CantidadSolicitadaPropertyName);
                }
            }
        }

        public decimal CantidadProducida {
            get {
                return Model.CantidadProducida;
            }
            set {
                if (Model.CantidadProducida != value) {
                    Model.CantidadProducida = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadProducidaPropertyName);
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

        public OrdenDeProduccionDetalleMaterialesMngViewModel DetailOrdenDeProduccionDetalleMateriales {
            get;
            set;
        }

        public OrdenDeProduccionViewModel Master {
            get;
            set;
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
                    NombreListaDeMateriales = string.Empty;
                    CodigoArticulo = string.Empty;
                    DescripcionArticulo = string.Empty;
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
                }
                if (_ConexionCodigoAlmacen == null) {
                    CodigoAlmacen = string.Empty;
                    NombreAlmacen = string.Empty;
                }
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

        public RelayCommand<string> ChooseCodigoListaDeMaterialesCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoAlmacenCommand {
            get;
            private set;
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

        protected override bool RecordIsReadOnly() {
            return Master.IsReadOnly;
        }

        public int DecimalDigits {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales");
            }
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
            if(valAction == eAccionSR.Cerrar) {
                CantidadProducida = CantidadSolicitada;
            }
            base.InitializeViewModel(valAction);
            InitializeDetails();
        }

        public override void InitializeViewModel(eAccionSR valAction, string valCustomAction) {
            if(valAction == eAccionSR.Custom) {
                base.InitializeViewModel(valAction, "Iniciar");
            } else {
                base.InitializeViewModel(valAction, valCustomAction);
            }
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoListaDeMaterialesCommand = new RelayCommand<string>(ExecuteChooseCodigoListaDeMaterialesCommand);
        }

        void InitializeDetails() {
            DetailOrdenDeProduccionDetalleMateriales = new OrdenDeProduccionDetalleMaterialesMngViewModel(this, Model.DetailOrdenDeProduccionDetalleMateriales, Action);
        }

        internal void InicializarRibbon() {
            if(Action == eAccionSR.Consultar || Action == eAccionSR.Eliminar) {
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[0].Command = null;
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[0].IsVisible = false;
            } else {
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[0].Command = null;
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[0].IsVisible = false;
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[1].Label = "Grabar";
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[1].ToolTipDescription = "Ejecuta la acción seleccionada.";
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[1].ToolTipDescription = "Ejecutar Acción";
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[1].LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/saveAndClose.png", UriKind.Relative);
                DetailOrdenDeProduccionDetalleMateriales.SelectedItem = DetailOrdenDeProduccionDetalleMateriales.Items.FirstOrDefault();
                RaisePropertyChanged("DetailOrdenDeProduccionDetalleMateriales");
            }
        }

        #endregion //Constructores e Inicializadores

        #region Metodos Generados

        protected override ILibBusinessDetailComponent<IList<OrdenDeProduccionDetalleArticulo>, IList<OrdenDeProduccionDetalleArticulo>> GetBusinessComponent() {
            return new clsOrdenDeProduccionDetalleArticuloNav();
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            //ConexionCodigoListaDeMateriales = Master.FirstConnectionRecordOrDefault<FkListaDeMaterialesViewModel>("Lista de Materiales", LibSearchCriteria.CreateCriteria("Codigo", CodigoListaDeMateriales));
            //ConexionCodigoAlmacen = Master.FirstConnectionRecordOrDefault<FkAlmacenViewModel>("Almacén", LibSearchCriteria.CreateCriteria("Codigo", CodigoAlmacen));
            //ConexionCodigoArticulo = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Articulo Inventario", LibSearchCriteria.CreateCriteria("Descripcion", DescripcionArticulo));
        }

        private void ExecuteChooseCodigoListaDeMaterialesCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_ListaDeMateriales_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_ListaDeMateriales_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCodigoListaDeMateriales = Master.ChooseRecord<FkListaDeMaterialesViewModel>("Lista de Materiales", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoListaDeMateriales != null) {
                    Model.ConsecutivoListaDeMateriales = ConexionCodigoListaDeMateriales.Consecutivo;
                    CodigoListaDeMateriales = ConexionCodigoListaDeMateriales.Codigo;
                    NombreListaDeMateriales = ConexionCodigoListaDeMateriales.Nombre;
                    CodigoArticulo = ConexionCodigoListaDeMateriales.CodigoArticuloInventario;
                    DescripcionArticulo = ConexionCodigoListaDeMateriales.DescripcionArticuloInventario;                    
                    System.Collections.ObjectModel.ObservableCollection<OrdenDeProduccionDetalleMateriales> vList = new System.Collections.ObjectModel.ObservableCollection<OrdenDeProduccionDetalleMateriales>(((IOrdenDeProduccionDetalleArticuloPdn)GetBusinessComponent()).ObtenerDetalleInicialDeListaDemateriales(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), Model.ConsecutivoListaDeMateriales, Master.ConsecutivoAlmacenMateriales, CantidadSolicitada));
                    DetailOrdenDeProduccionDetalleMateriales = new OrdenDeProduccionDetalleMaterialesMngViewModel(this, vList, Action );
                    Model.DetailOrdenDeProduccionDetalleMateriales = vList;
                    Master.ActualizaAlmacenenMaterialesEnDetalles();
                    Master.VerDetalleCommand.RaiseCanExecuteChanged();
                    BuscaExistencia();
                } else {
                    Model.ConsecutivoListaDeMateriales = 0;
                    CodigoListaDeMateriales = string.Empty;
                    NombreListaDeMateriales = string.Empty;
                    CodigoArticulo = string.Empty;
                    DescripcionArticulo = string.Empty;
                    DetailOrdenDeProduccionDetalleMateriales = new OrdenDeProduccionDetalleMaterialesMngViewModel(this, new System.Collections.ObjectModel.ObservableCollection<OrdenDeProduccionDetalleMateriales>(), Action);
                    Model.DetailOrdenDeProduccionDetalleMateriales = new System.Collections.ObjectModel.ObservableCollection<OrdenDeProduccionDetalleMateriales>();
                    Master.VerDetalleCommand.RaiseCanExecuteChanged();
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        #endregion //Metodos Generados

        #region Metodos

        private void ActualizaCantidadEnDetalles() {
            foreach(OrdenDeProduccionDetalleMateriales vItem in Model.DetailOrdenDeProduccionDetalleMateriales) {
                vItem.CantidadReservadaInventario = CantidadSolicitada * vItem.Cantidad;
            }
        }

        internal void BuscaExistencia() {
            IOrdenDeProduccionDetalleArticuloPdn vOrdenDeProduccionDetalleArticulo = new clsOrdenDeProduccionDetalleArticuloNav();
            XElement vData = vOrdenDeProduccionDetalleArticulo.BuscaExistenciaDeArticulos(ConsecutivoCompania, new List<OrdenDeProduccionDetalleArticulo> { Model });
            foreach(var item in DetailOrdenDeProduccionDetalleMateriales.Items) {
                var vExistencia = vData.Descendants("GpResult").Where(p => p.Element("CodigoArticulo").Value == item.CodigoArticulo && LibConvert.ToInt(p.Element("ConsecutivoAlmacen")) == item.ConsecutivoAlmacen).Select(q => new { Existencia = LibConvert.ToDec(q.Element("Cantidad"), 8) }).FirstOrDefault();
                if(vExistencia != null) {
                    item.Existencia = vExistencia.Existencia;
                }
            }
        }

        #endregion //Metodos

    } //End of class OrdenDeProduccionDetalleArticuloViewModel

} //End of namespace Galac.Adm.Uil. GestionProduccion

