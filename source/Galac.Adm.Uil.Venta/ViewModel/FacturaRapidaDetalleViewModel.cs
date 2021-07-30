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
using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Ccl.Inventario;
using System.Collections.ObjectModel;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class FacturaRapidaDetalleViewModel : LibInputDetailViewModelMfc<FacturaRapidaDetalle> {
        #region Constantes
        public const string TipoDeDocumentoPropertyName = "TipoDeDocumento";
        public const string ArticuloPropertyName = "Articulo";
        public const string DescripcionPropertyName = "Descripcion";
        public const string AlicuotaIvaPropertyName = "AlicuotaIva";
        public const string CantidadPropertyName = "Cantidad";
        public const string PrecioSinIVAPropertyName = "PrecioSinIVA";
        public const string PrecioConIVAPropertyName = "PrecioConIVA";
        public const string PorcentajeDescuentoPropertyName = "PorcentajeDescuento";
        public const string TotalRenglonPropertyName = "TotalRenglon";
        public const string PorcentajeBaseImponiblePropertyName = "PorcentajeBaseImponible";
        public const string SerialPropertyName = "Serial";
        public const string RolloPropertyName = "Rollo";
        public const string PorcentajeAlicuotaPropertyName = "PorcentajeAlicuota";
        #endregion
        #region Variables
        //private FkArticuloInventarioViewModel _ConexionArticulo = null;
        //private FkArticuloInventarioViewModel _ConexionDescripcion = null;
        private decimal _OldCantidad;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Punto de Venta Detalle"; }
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

        public string NumeroFactura {
            set {
                if (Model.NumeroFactura != value) {
                    Model.NumeroFactura = value;
                }
            }
        }

        public eTipoDocumentoFactura TipoDeDocumento {
            get {
                return Model.TipoDeDocumentoAsEnum;
            }
            set {
                if (Model.TipoDeDocumentoAsEnum != value) {
                    Model.TipoDeDocumentoAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDeDocumentoPropertyName);
                }
            }
        }

        public int ConsecutivoRenglon {
            get {
                return Model.ConsecutivoRenglon;
            }
            set {
                if (Model.ConsecutivoRenglon != value) {
                    Model.ConsecutivoRenglon = value;
                }
            }
        }

        [LibGridColum("Código", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "Codigo", ConnectionSearchCommandName = "ChooseCodigoCommand", Width = 250, MaxLength = 250, ColumnOrder = 0, ConditionalPropertyIsReadOnly = "IsReadOnlyColunm")]
        public string Articulo {
            get {
                return Model.Articulo;
            }
            set {
                if (Model.Articulo != value) {
                    Model.Articulo = value;
                    IsDirty = true;
                    RaisePropertyChanged(ArticuloPropertyName);
                    //if (LibString.IsNullOrEmpty(Articulo, true)) {
                    //    ConexionArticulo = null;
                    //}

                }
            }
        }

        [LibGridColum("Descripción", eGridColumType.Generic, ConnectionDisplayMemberPath = "Descripcion", ConnectionModelPropertyName = "Descripcion", Trimming = System.Windows.TextTrimming.WordEllipsis, ColumnOrder = 1, Width = 0)]
        public string Descripcion {
            get {
                return Model.Descripcion;
            }
            set {
                if (Model.Descripcion != value) {
                    Model.Descripcion = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionPropertyName);
                    //if (LibString.IsNullOrEmpty(Descripcion, true)) {
                    //    ConexionDescripcion = null;
                    //}
                }
            }
        }

        public eTipoDeAlicuota AlicuotaIva {
            get {
                return Model.AlicuotaIvaAsEnum;
            }
            set {
                if (Model.AlicuotaIvaAsEnum != value) {
                    Model.AlicuotaIvaAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(AlicuotaIvaPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Cantidad es requerido.")]
        //[LibCustomValidation("CantidadValidating")]
        [LibGridColum("Cantidad", eGridColumType.Numeric, Width = 80, ConditionalPropertyDecimalDigits = "CantidadDeDecimales", MaxLength = 7, ColumnOrder = 2)]
        public decimal Cantidad {
            get {
                return Model.Cantidad;
            }
            set {
                if (Model.Cantidad != value) {
                    _OldCantidad = Model.Cantidad;
                    Model.Cantidad = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadPropertyName);
                }
            }
        }

        public int CantidadDeDecimales {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("FacturaRapida", "CantidadDeDecimales");
            }
        }
        //[LibCustomValidation("PrecioSinIvaValidating")]
        [LibGridColum("Precio", eGridColumType.Numeric, Width = 150, ConditionalPropertyDecimalDigits = "CantidadDeDecimales", MaxLength = 10, ColumnOrder = 3)]
        public decimal PrecioSinIVA {
            get {
                return Model.PrecioSinIVA;
            }
            set {
                if (Model.PrecioSinIVA != value) {
                    Model.PrecioSinIVA = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrecioSinIVAPropertyName);
                }
            }
        }

        //[LibCustomValidation("PrecioConIvaValidating")]
        [LibGridColum("Precio", eGridColumType.Numeric, Width = 150, ConditionalPropertyDecimalDigits = "CantidadDeDecimales", MaxLength = 10, ColumnOrder = 4)]
        public decimal PrecioConIVA {
            get {
                return Model.PrecioConIVA;
            }
            set {
                if (Model.PrecioConIVA != value) {
                    Model.PrecioConIVA = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrecioConIVAPropertyName);
                }
            }
        }

        public decimal PorcentajeDescuento {
            get {
                return Model.PorcentajeDescuento;
            }
            set {
                if (Model.PorcentajeDescuento != value) {
                    Model.PorcentajeDescuento = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeDescuentoPropertyName);
                }
            }
        }

        public decimal MontoBrutoSinIva {
            get {
                return Model.MontoBrutoSinIva;
            }
            set {
                if (Model.MontoBrutoSinIva != value) {
                    Model.MontoBrutoSinIva = value;
                }
            }
        }

        public decimal MontoBrutoConIva {
            get {
                return Model.MontoBrutoConIva;
            }
            set {
                if (Model.MontoBrutoConIva != value) {
                    Model.MontoBrutoConIva = value;
                }
            }
        }

        [LibGridColum("Total", eGridColumType.Numeric, Width = 200, BindingStringFormat = "N2", MaxLength = 200, ColumnOrder = 5, ConditionalPropertyIsReadOnly = "IsReadOnlyColunm")]
        public decimal TotalRenglon {
            get {
                return Model.TotalRenglon;
            }
            set {
                if (Model.TotalRenglon != value) {
                    Model.TotalRenglon = value;
                    IsDirty = true;
                    RaisePropertyChanged(TotalRenglonPropertyName);
                }
            }
        }

        public decimal PorcentajeBaseImponible {
            get {
                return Model.PorcentajeBaseImponible;
            }
            set {
                if (Model.PorcentajeBaseImponible != value) {
                    Model.PorcentajeBaseImponible = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeBaseImponiblePropertyName);
                }
            }
        }

        public string Serial {
            get {
                return Model.Serial;
            }
            set {
                if (Model.Serial != value) {
                    Model.Serial = value;
                    IsDirty = true;
                    RaisePropertyChanged(SerialPropertyName);
                }
            }
        }

        public string Rollo {
            get {
                return Model.Rollo;
            }
            set {
                if (Model.Rollo != value) {
                    Model.Rollo = value;
                    IsDirty = true;
                    RaisePropertyChanged(RolloPropertyName);
                }
            }
        }

        public decimal PorcentajeAlicuota {
            get {
                return Model.PorcentajeAlicuota;
            }
            set {
                if (Model.PorcentajeAlicuota != value) {
                    Model.PorcentajeAlicuota = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeAlicuotaPropertyName);
                }
            }
        }


        public eTipoDeArticulo TipoDeArticulo {
            get {
                return Model.TipoDeArticuloAsEnum;
            }
            set {
                if (Model.TipoDeArticuloAsEnum != value) {
                    Model.TipoDeArticuloAsEnum = value;
                    IsDirty = true;
                }
            }
        }

        public eTipoArticuloInv TipoArticuloInv {
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

        public eTipoDocumentoFactura[] ArrayTipoDocumentoFactura {
            get {
                return LibEnumHelper<eTipoDocumentoFactura>.GetValuesInArray();
            }
        }

        public eTipoDeAlicuota[] ArrayTipoDeAlicuota {
            get {
                return LibEnumHelper<eTipoDeAlicuota>.GetValuesInArray();
            }
        }

        public eTipoDeArticulo[] ArrayTipoDeArticulo {
            get {
                return LibEnumHelper<eTipoDeArticulo>.GetValuesInArray();
            }
        }

        public eTipoArticuloInv[] ArrayTipoArticuloInv {
            get {
                return LibEnumHelper<eTipoArticuloInv>.GetValuesInArray();
            }
        }

        public FacturaRapidaViewModel Master {
            get;
            set;
        }

        public string Categoria {
            get;
            set;
        }
        #endregion //Propiedades
        #region Constructores
        public FacturaRapidaDetalleViewModel()
            : base(new FacturaRapidaDetalle(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public FacturaRapidaDetalleViewModel(FacturaRapidaViewModel initMaster, FacturaRapidaDetalle initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;

        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(FacturaRapidaDetalle valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ILibBusinessDetailComponent<IList<FacturaRapidaDetalle>, IList<FacturaRapidaDetalle>> GetBusinessComponent() {
            return new clsFacturaRapidaDetalleNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            //ChooseArticuloCommand = new RelayCommand<string>(ExecuteChooseArticuloCommand);
            //CantidadCommand = new RelayCommand(ExecuteCantidadCommand);
            //ChooseDescripcionCommand = new RelayCommand<string>(ExecuteChooseDescripcionCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            //ConexionArticulo = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Articulo Inventario", LibSearchCriteria.CreateCriteria("Codigo", Articulo));
            //ConexionDescripcion = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Articulo Inventario", LibSearchCriteria.CreateCriteria("Descripcion", Descripcion));
        }

        #endregion //Metodos Generados

        public bool IsReadOnlyColunm {
            get {
                return true;
            }
        }

        internal decimal CalcularPreciosPorDetail(decimal valPrecio, decimal valPorcentaje, bool valEsPrecioSinIva, decimal valPorcentajeBaseImponible) {
            decimal vResult = 0;
            if (valPrecio > 0) {
                if (valEsPrecioSinIva) {
                    vResult = valPrecio * (1 + (valPorcentajeBaseImponible * valPorcentaje) / 10000);
                } else {
                    vResult = valPrecio / (1 + (valPorcentajeBaseImponible * valPorcentaje) / 10000);
                }
            }
            return vResult;
        }
    } //End of class FacturaRapidaDetalleViewModel

} //End of namespace Galac.Adm.Uil.Venta

