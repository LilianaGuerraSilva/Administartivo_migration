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
        #endregion

        #region Variables


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
       

        //[LibRequired(ErrorMessage = "El campo Código de Artículo es requerido.")]
        [LibGridColum("Código", eGridColumType.Generic, ColumnOrder=0)]
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

        //[LibRequired(ErrorMessage = "El campo Descripción Artículo es requerido.")]
        [LibGridColum("Descripción", eGridColumType.Generic,MaxWidth = 120, ColumnOrder = 1)]
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
				
        [LibGridColum("Unidad", eGridColumType.Generic, ColumnOrder = 2)]
        public string  UnidadDeVenta {
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

        [LibGridColum("Cantidad Original Lista", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ColumnOrder = 3)]
        public decimal  CantidadOriginalLista {
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

        [LibGridColum("Cantidad Solicitada", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ColumnOrder = 4)]
        public decimal CantidadSolicitada {
            get {
                return Model.CantidadSolicitada;
            }
            set {
                if (Model.CantidadSolicitada != value) {
                    Model.CantidadSolicitada = value;
                    IsDirty = true;
                    //ActualizaCantidadEnDetalles();
                    RaisePropertyChanged(CantidadSolicitadaPropertyName);
                }
            }
        }

        [LibGridColum("Cantidad Producida", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ColumnOrder = 5)]
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

        [LibGridColum("% Costo Est.", eGridColumType.Numeric, Alignment = eTextAlignment.Right,ColumnOrder = 6)]
        public decimal  PorcentajeCostoEstimado {
            get {
                return Model.PorcentajeCostoEstimado;
            }
            set {
                if (Model.PorcentajeCostoEstimado != value) {
                    Model.PorcentajeCostoEstimado = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeCostoEstimadoPropertyName);
                }
            }
        }

        [LibGridColum("% Costo Cierre", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ColumnOrder = 7)]
        public decimal  PorcentajeCostoCierre {
            get {
                return Model.PorcentajeCostoCierre;
            }
            set {
                if (Model.PorcentajeCostoCierre != value) {
                    Model.PorcentajeCostoCierre = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeCostoCierrePropertyName);
                }
            }
        }

        //[LibGridColum("Costo", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        public decimal  Costo {
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

        protected override bool RecordIsReadOnly() {
            return Master.IsReadOnly;
        }

        public int DecimalDigits {
            get {
                return 8;//LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales");
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
        }
       
		/*
        internal void InicializarRibbon() {
            if(Action == eAccionSR.Consultar || Action == eAccionSR.Eliminar || Action == eAccionSR.Contabilizar) {
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
		*/

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
       
        #endregion //Metodos Generados

        #region Metodos
		/* 
		MUDAR
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
		*/
        #endregion //Metodos

    } //End of class OrdenDeProduccionDetalleArticuloViewModel

} //End of namespace Galac.Adm.Uil. GestionProduccion

