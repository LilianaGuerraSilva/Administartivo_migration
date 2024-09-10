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
using System.Collections.ObjectModel;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Uil.GestionCompras.ViewModel {
    public class OrdenDeCompraDetalleArticuloInventarioViewModel : LibInputDetailViewModelMfc<OrdenDeCompraDetalleArticuloInventario> {
        #region Constantes
        public const string CodigoArticuloPropertyName = "CodigoArticulo";
        public const string DescripcionArticuloPropertyName = "DescripcionArticulo";
        public const string CantidadPropertyName = "Cantidad";
        public const string PrecioUnitarioPropertyName = "PrecioUnitario";
        public const string CantidadRecibidaPropertyName = "CantidadRecibida";
        public const string TipoArticuloInvStrPropertyName = "TipoArticuloInvStr";
        #endregion
        #region Variables
        private FkArticuloInventarioViewModel _ConexionCodigoArticulo = null;
        private bool _IsDirtyArticulo = false;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Orden De Compra Detalle Articulo Inventario"; }
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

        public int  ConsecutivoOrdenDeCompra {
            get {
                return Model.ConsecutivoOrdenDeCompra;
            }
            set {
                if (Model.ConsecutivoOrdenDeCompra != value) {
                    Model.ConsecutivoOrdenDeCompra = value;
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

        [LibRequired(ErrorMessage = "El campo Producto es requerido.")]
        [LibGridColum("Producto", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoArticulo", ConnectionSearchCommandName = "ChooseCodigoArticuloCommand", MaxWidth = 120)]
        public string CodigoArticulo {
            get {
                return Model.CodigoArticulo;
            }
            set {
                if (Model.CodigoArticulo != value) {
                    Model.CodigoArticulo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoArticulo, true)) {
                        ConexionCodigoArticulo = null;
                    }
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Descripción es requerido.")]
        [LibGridColum("Descripción", MaxLength=7000)]
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

        //[LibRequired(ErrorMessage = "El campo Cantidad es requerido.")]
        [LibGridColum("Cantidad", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        [LibCustomValidation("CantidadValidating")]
        public decimal  Cantidad {
            get {
                return Model.Cantidad;
            }
            set {
                if (Model.Cantidad != value) {
                    Model.Cantidad = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadPropertyName);
                    RaisePropertyChanged("SubTotal");
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Precio Unitario es requerido.")]
        [LibGridColum("Precio Unitario", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ConditionalPropertyDecimalDigits = "DecimalDigits")]
        public decimal PrecioUnitario {
            get {
                return Model.CostoUnitario;
            }
            set {
                if (Model.CostoUnitario != value) {
                    Model.CostoUnitario = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrecioUnitarioPropertyName);
                    RaisePropertyChanged("SubTotal");
                }
            }
        }

        [LibGridColum("Sub Total", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ConditionalPropertyDecimalDigits = "DecimalDigits")]
        public decimal SubTotal {
            get {

                return Model.Cantidad * Model.CostoUnitario;
            }
            set {

            }
        }

        [LibGridColum("Cantidad Recibida", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ConditionalPropertyDecimalDigits = "DecimalDigits")]
        public decimal CantidadRecibida {
            get {
                return Model.CantidadRecibida;
            }
            set {
                if (Model.CantidadRecibida != value) {
                    Model.CantidadRecibida = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadRecibidaPropertyName);
                }
            }
        }

        public OrdenDeCompraViewModel Master {
            get;
            set;
        }

        public FkArticuloInventarioViewModel ConexionCodigoArticulo {
            get {
                return _ConexionCodigoArticulo;
            }
            set {
                if (_ConexionCodigoArticulo != value) {
                    _ConexionCodigoArticulo = value;
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                    if (_ConexionCodigoArticulo != null) {
                        CodigoArticulo = ConexionCodigoArticulo.CodigoCompuesto;
                        DescripcionArticulo = ConexionCodigoArticulo.Descripcion;
                      
                        Codigo = ConexionCodigoArticulo.Codigo;
                        CodigoGrupo = ConexionCodigoArticulo.CodigoGrupo;
                        TipoArticuloInv = ConexionCodigoArticulo.TipoArticuloInv;
                        Model.TipoDeAlicuota = LibConvert.ToInt(ConexionCodigoArticulo.AlicuotaIva);
                        Model.TipoDeArticulo = (int)ConexionCodigoArticulo.TipoDeArticulo;
                        TipoArticuloInvStr = LibEnumHelper.GetDescription(TipoArticuloInv);
                    }
                }
                if (_ConexionCodigoArticulo == null) {
                    CodigoArticulo = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoArticuloCommand {
            get;
            private set;
        }
        public int DecimalDigits {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales");
            }
        }

        public bool IsVisibleCantidadRecibida {
            get {
                return Action != eAccionSR.Insertar;
            }
        }
        public string Codigo { get; set; }
        public string CodigoGrupo { get; set; }
        public eTipoArticuloInv TipoArticuloInv { get; set; }
        public string TipoArticuloInvStr {
            get {
                return Model.TipoArticuloInvStr;
            }
            set {
                if (Model.TipoArticuloInvStr != value) {
                    Model.TipoArticuloInvStr = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoArticuloInvStrPropertyName);
                }
            }
        }
        #endregion //Propiedades
        #region Constructores
        public OrdenDeCompraDetalleArticuloInventarioViewModel()
            : base(new OrdenDeCompraDetalleArticuloInventario(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public OrdenDeCompraDetalleArticuloInventarioViewModel(OrdenDeCompraViewModel initMaster, OrdenDeCompraDetalleArticuloInventario initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            if (Action == eAccionSR.Insertar) {
                _IsDirtyArticulo = true;                
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(OrdenDeCompraDetalleArticuloInventario valModel) {
            base.InitializeLookAndFeel(valModel);
           
        }

        protected override ILibBusinessDetailComponent<IList<OrdenDeCompraDetalleArticuloInventario>, IList<OrdenDeCompraDetalleArticuloInventario>> GetBusinessComponent() {
            return new clsOrdenDeCompraDetalleArticuloInventarioNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoArticuloCommand = new RelayCommand<string>(ExecuteChooseCodigoArticuloCommand);
        }

        protected override void ReloadRelatedConnections() {
            //base.ReloadRelatedConnections();
            //ConexionCodigoArticulo = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Articulo Inventario", LibSearchCriteria.CreateCriteria("CodigoCompuesto", CodigoArticulo));            
        }

        private void ExecuteChooseCodigoArticuloCommand(string valCodigo) {
            bool vAplicaProductoTerminado = false;
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                vAplicaProductoTerminado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Compania", "ProductoTerminado");
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("CodigoCompuesto", valCodigo);
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("StatusdelArticulo ", "0"), eLogicOperatorType.And);
                vFixedCriteria.Add("TipoDeArticulo", eBooleanOperatorType.IdentityInequality, "2", eLogicOperatorType.And);
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaLoteFechaDeVencimiento")){
                    vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityInequality, LibConvert.EnumToDbValue((int)eTipoArticuloInv.UsaSerialRollo));
                    vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityInequality, LibConvert.EnumToDbValue((int)eTipoArticuloInv.UsaTallaColorySerial));
                    vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityInequality, LibConvert.EnumToDbValue((int)eTipoArticuloInv.UsaTallaColor));
                    vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityInequality, LibConvert.EnumToDbValue((int)eTipoArticuloInv.UsaSerial));
                } 
                else
                {
                    vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityInequality, LibConvert.EnumToDbValue((int)eTipoArticuloInv.Lote));
                    vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityInequality, LibConvert.EnumToDbValue((int)eTipoArticuloInv.LoteFechadeVencimiento));
                }
                if (vAplicaProductoTerminado) {
                    vFixedCriteria.Add("TipoDeMercanciaProduccion", eBooleanOperatorType.IdentityInequality, LibConvert.EnumToDbValue((int)eTipoDeMercancia.ProductoTerminado));
                }
                FkArticuloInventarioViewModel ConexionCodigoArticulo = Master.ChooseRecord<FkArticuloInventarioViewModel>("Articulo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoArticulo != null) {
                    if(LibDefGen.ProgramInfo.IsCountryPeru()) {
                        PrecioUnitario = ConexionCodigoArticulo.CostoUnitario;
                    }                    
                    CodigoArticulo = ConexionCodigoArticulo.CodigoCompuesto;
                    
                    if (_IsDirtyArticulo) {
                        DescripcionArticulo = ConexionCodigoArticulo.Descripcion;
                    }
                    
                    Codigo = ConexionCodigoArticulo.Codigo;
                    CodigoGrupo = ConexionCodigoArticulo.CodigoGrupo;
                    TipoArticuloInv = ConexionCodigoArticulo.TipoArticuloInv;
                    TipoArticuloInvStr = LibEnumHelper.GetDescription(TipoArticuloInv);
                } else {
                    CodigoArticulo = string.Empty;
                    DescripcionArticulo = string.Empty;
                   
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //Metodos Generados
        private bool BuscarCodigoRepetidoEnElGrid(string valCodigo) {
            return Master.BuscarCodigoRepetidoEnElGrid(valCodigo);
        }

        public void SeModificoDescripcion(string valTextoDescripcion) {
            _IsDirtyArticulo = (DescripcionArticulo != valTextoDescripcion);
            if (_IsDirtyArticulo) {
                DescripcionArticulo = valTextoDescripcion;
            }
        }

        public void SeModificoCodigoArticulo(string valTextoCodigoArticulo) {
            _IsDirtyArticulo = (CodigoArticulo != valTextoCodigoArticulo);
        }

        private ValidationResult CantidadValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (Cantidad == 0) {
                    vResult = new ValidationResult("El campo Cantidad es requerido.");
                }
                if (Cantidad < CantidadRecibida) {
                    vResult = new ValidationResult("La Cantidad Solicitada es Menor a la Cantidad Recibida.");
                }
            }
            return vResult;
        }
    } //End of class OrdenDeCompraDetalleArticuloInventarioViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

