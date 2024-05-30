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
        public const string UnidadDeVentaPropertyName = "UnidadDeVenta";
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


        [LibGridColum("Descripción", eGridColumType.Generic, MaxWidth = 220, Width = 480, ColumnOrder = 1)]
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


        [LibGridColum("Unidad", eGridColumType.Generic, MaxWidth = 100, ColumnOrder = 2)]
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

        [LibGridColum("Cantidad Original en Lista", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ColumnOrder = 4, ConditionalPropertyDecimalDigits = "DecimalDigits", Width =150)]
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
        [LibGridColum("Cantidad a Consumir", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 150, ConditionalPropertyDecimalDigits = "DecimalDigits", ColumnOrder = 5)]
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
        [LibGridColum("Cantidad Consumida", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 150, ColumnOrder = 6, ConditionalPropertyDecimalDigits = "DecimalDigits")]
        public decimal CantidadConsumida {
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

        [LibGridColum("Costo Total", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 100, ColumnOrder = 7, ConditionalPropertyDecimalDigits = "2")]
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

        [LibGridColum("Costo Unitario", eGridColumType.Numeric, ColumnOrder = 8, ConditionalPropertyDecimalDigits = "DecimalDigits")]
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


    } //End of class OrdenDeProduccionDetalleMaterialesViewModel

} //End of namespace Galac.Adm.Uil.GestionProduccion

