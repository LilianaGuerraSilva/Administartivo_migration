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

namespace Galac.Adm.Uil.GestionProduccion.ViewModel {
    public class OrdenDeProduccionDetalleMaterialesViewModel : LibInputDetailViewModelMfc<OrdenDeProduccionDetalleMateriales> {
        #region Constantes
        public const string CodigoAlmacenPropertyName = "CodigoAlmacen";
        public const string NombreAlmacenPropertyName = "NombreAlmacen";
        public const string CodigoArticuloPropertyName = "CodigoArticulo";
        public const string DescripcionArticuloPropertyName = "DescripcionArticulo";
        public const string CantidadPropertyName = "Cantidad";
        public const string CantidadReservadaInventarioPropertyName = "CantidadReservadaInventario";
        public const string CantidadConsumidaPropertyName = "CantidadConsumida";
        public const string CostoUnitarioArticuloInventarioPropertyName = "CostoUnitarioArticuloInventario";
        public const string MontoSubtotalPropertyName = "MontoSubtotal";
        public const string AjustadoPostCierrePropertyName = "AjustadoPostCierre";
        public const string CantidadAjustadaPropertyName = "CantidadAjustada";
        #endregion
        #region Variables
        
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Orden De Produccion Detalle Materiales"; }
        }

        public int  ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if (Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                }
            }
        }

        public int  ConsecutivoOrdenDeProduccion {
            get {
                return Model.ConsecutivoOrdenDeProduccion;
            }
            set {
                if (Model.ConsecutivoOrdenDeProduccion != value) {
                    Model.ConsecutivoOrdenDeProduccion = value;
                }
            }
        }

        public int  ConsecutivoOrdenDeProduccionDetalleArticulo {
            get {
                return Model.ConsecutivoOrdenDeProduccionDetalleArticulo;
            }
            set {
                if (Model.ConsecutivoOrdenDeProduccionDetalleArticulo != value) {
                    Model.ConsecutivoOrdenDeProduccionDetalleArticulo = value;
                }
            }
        }

        public int  Consecutivo {
            get {
                return Model.Consecutivo;
            }
            set {
                if (Model.Consecutivo != value) {
                    Model.Consecutivo = value;
                }
            }
        }

        public int  ConsecutivoAlmacen {
            get {
                return Model.ConsecutivoAlmacen;
            }
            set {
                if (Model.ConsecutivoAlmacen != value) {
                    Model.ConsecutivoAlmacen = value;
                }
            }
        }

      
        public string  CodigoAlmacen {
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

      
        public string  NombreAlmacen {
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

        [LibGridColum("Codigo", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoArticulo", ConnectionSearchCommandName = "ChooseCodigoArticuloCommand", MaxWidth=120, ColumnOrder= 0)]
        public string  CodigoArticulo {
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

        
        [LibGridColum("Descripción", eGridColumType.Connection, ConnectionDisplayMemberPath = "Descripcion", ConnectionModelPropertyName = "DescripcionArticulo", ConnectionSearchCommandName = "ChooseDescripcionArticuloCommand", MaxWidth=480, Width = 480, ColumnOrder = 1)]
        public string  DescripcionArticulo {
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

        
        [LibGridColum(" Cantidad Original", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ConditionalPropertyDecimalDigits = "DecimalDigitsCantidadOriginal", ColumnOrder = 2)]
        public decimal  Cantidad {
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

        
        [LibGridColum("Cantidad Reservada Inventario", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 200, ConditionalPropertyDecimalDigits = "DecimalDigits",  ColumnOrder = 3)]
        public decimal  CantidadReservadaInventario {
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

      
       // [LibGridColum("Cantidad Consumida", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        public decimal  CantidadConsumida {
            get {
                return Model.CantidadConsumida;
            }
            set {
                if (Model.CantidadConsumida != value) {
                    Model.CantidadConsumida = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadConsumidaPropertyName);
                }
            }
        }

        public decimal  CostoUnitarioArticuloInventario {
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

        public decimal  MontoSubtotal {
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

        public bool  AjustadoPostCierre {
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

        
        public decimal  CantidadAjustada {
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

        public OrdenDeProduccionDetalleArticuloViewModel Master {
            get;
            set;
        }

        [LibGridColum("Existencia", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ColumnOrder = 5)]
        public Decimal Existencia { get; set; }

        
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
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales");
            }
        }

        public int DecimalDigitsCantidadOriginal {
            get {
                return 8;
            }
        }

        



        #endregion //Propiedades
        #region Constructores
        public OrdenDeProduccionDetalleMaterialesViewModel()
            : base(new OrdenDeProduccionDetalleMateriales(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public OrdenDeProduccionDetalleMaterialesViewModel(OrdenDeProduccionDetalleArticuloViewModel initMaster, OrdenDeProduccionDetalleMateriales initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(OrdenDeProduccionDetalleMateriales valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ILibBusinessDetailComponent<IList<OrdenDeProduccionDetalleMateriales>, IList<OrdenDeProduccionDetalleMateriales>> GetBusinessComponent() {
            return new clsOrdenDeProduccionDetalleMaterialesNav();
        }

        #endregion //Metodos Generados

        public override void InitializeViewModel(eAccionSR valAction) {
            if (valAction == eAccionSR.Cerrar) {
                CantidadConsumida = CantidadReservadaInventario;
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

    } //End of class OrdenDeProduccionDetalleMaterialesViewModel

} //End of namespace Galac.Adm.Uil.GestionProduccion

