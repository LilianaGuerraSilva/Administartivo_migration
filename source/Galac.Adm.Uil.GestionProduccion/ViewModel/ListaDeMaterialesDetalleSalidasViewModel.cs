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
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Uil.GestionProduccion.ViewModel {
    public class ListaDeMaterialesDetalleSalidasViewModel : LibInputDetailViewModelMfc<ListaDeMaterialesDetalleSalidas> {
        #region Constantes
        public const string CodigoArticuloInventarioPropertyName = "CodigoArticuloInventario";
        public const string DescripcionArticuloInventarioPropertyName = "DescripcionArticuloInventario";
        public const string CantidadPropertyName = "Cantidad";
        public const string UnidadDeVentaPropertyName = "UnidadDeVenta";
        public const string PorcentajeDeCostoPropertyName = "PorcentajeDeCosto";
        public const string MermaNormalPropertyName = "MermaNormal";
        public const string PorcentajeMermaNormalPropertyName = "PorcentajeMermaNormal";
        
        #endregion
        #region Variables
        private FkArticuloInventarioViewModel _ConexionCodigoArticuloInventario = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Salidas"; }
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

        public int  ConsecutivoListaDeMateriales {
            get {
                return Model.ConsecutivoListaDeMateriales;
            }
            set {
                if (Model.ConsecutivoListaDeMateriales != value) {
                    Model.ConsecutivoListaDeMateriales = value;
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

        [LibGridColum("Código Artículo", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoArticuloInventario", ConnectionSearchCommandName = "ChooseCodigoArticuloInventarioCommand", Width=120, ColumnOrder = 0)]
        public string  CodigoArticuloInventario {
            get {
                return Model.CodigoArticuloInventario;
            }
            set {
                if (Model.CodigoArticuloInventario != value) {
                    Model.CodigoArticuloInventario = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoArticuloInventarioPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoArticuloInventario, true)) {
                        ConexionCodigoArticuloInventario = null;
                    }
                }
            }
        }

        //[LibGridColum("Descripción", eGridColumType.Connection, ConnectionDisplayMemberPath = "Descripcion", ConnectionModelPropertyName = "DescripcionArticuloInventario", ConnectionSearchCommandName = "ChooseDescripcionArticuloInventarioCommand", Width = 250, Trimming = System.Windows.TextTrimming.WordEllipsis, ColumnOrder = 1)]
        public string  DescripcionArticuloInventario {
            get {
                return Model.DescripcionArticuloInventario;
            }
            set {
                if (Model.DescripcionArticuloInventario != value) {
                    Model.DescripcionArticuloInventario = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionArticuloInventarioPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Cantidad es requerido.")]
        [LibGridColum("Cantidad", eGridColumType.Numeric, Alignment = eTextAlignment.Right, ConditionalPropertyDecimalDigits = "DecimalDigits", Width = 130, ColumnOrder = 1)]
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

        [LibGridColum("Unidad", eGridColumType.Connection, ConnectionDisplayMemberPath = "UnidadDeVenta", ConnectionModelPropertyName = "UnidadDeVenta", Width=150, ColumnOrder = 2)]
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

        [LibCustomValidation("PorcentajeDeCostoValidating")]
        [LibGridColum("% Costo", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 120, ConditionalPropertyDecimalDigits = "DecimalDigits", ColumnOrder = 5)]
        public decimal  PorcentajeDeCosto {
            get {
                return Model.PorcentajeDeCosto;
            }
            set {
                if (Model.PorcentajeDeCosto != value) {
                    Model.PorcentajeDeCosto = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeDeCostoPropertyName);
                }
            }
        }

        [LibCustomValidation("MermaNormalValidating")]
        [LibGridColum("Merma Normal (en Unidades)", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 250, ConditionalPropertyDecimalDigits = "DecimalDigits", ColumnOrder = 3)]
        public decimal  MermaNormal {
            get {
                return Model.MermaNormal;
            }
            set {
                if (Model.MermaNormal != value) {
                    Model.MermaNormal = value;
                    IsDirty = true;
                    RaisePropertyChanged(MermaNormalPropertyName);
                    CalculaPorcentajeMerma();
                    RaisePropertyChanged(PorcentajeMermaNormalPropertyName);
                }
            }
        }

        [LibGridColum("% Merma Normal", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 120, ConditionalPropertyDecimalDigits = "DecimalDigits", ColumnOrder = 4)]
        public decimal  PorcentajeMermaNormal {
            get {
                return Model.PorcentajeMermaNormal;
            }
            set {
                if (Model.PorcentajeMermaNormal != value) {
                    Model.PorcentajeMermaNormal = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeMermaNormalPropertyName);
                }
            }
        }

        public ListaDeMaterialesViewModel Master {
            get;
            set;
        }

        public FkArticuloInventarioViewModel ConexionCodigoArticuloInventario {
            get {
                return _ConexionCodigoArticuloInventario;
            }
            set {
                if (_ConexionCodigoArticuloInventario != value) {
                    _ConexionCodigoArticuloInventario = value;
                    RaisePropertyChanged(CodigoArticuloInventarioPropertyName);
                    RaisePropertyChanged(() => IsVisbleTipoArticuloInvStr);
                    RaisePropertyChanged(() => TipoArticuloInvStr);
                }
                if (_ConexionCodigoArticuloInventario == null) {
                    CodigoArticuloInventario = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoArticuloInventarioCommand {
            get;
            private set;
        }

        public bool IsVisbleTipoArticuloInvStr {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaLoteFechaDeVencimiento") &&
                    (!LibString.IsNullOrEmpty(CodigoArticuloInventario));
            }
        }

        public eTipoArticuloInv TipoArticuloInvAsEnum {
            get {
                return Model.TipoArticuloInvAsEnum;
            }
            set {
                if (Model.TipoArticuloInvAsEnum != value) {
                    Model.TipoArticuloInvAsEnum = value;
                }
            }
        }

        public string TipoArticuloInvStr {
            get { return LibEnumHelper.GetDescription(TipoArticuloInvAsEnum); }
        }

        #endregion //Propiedades
        #region Constructores
        public ListaDeMaterialesDetalleSalidasViewModel()
            : base(new ListaDeMaterialesDetalleSalidas(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public ListaDeMaterialesDetalleSalidasViewModel(ListaDeMaterialesViewModel initMaster, ListaDeMaterialesDetalleSalidas initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(ListaDeMaterialesDetalleSalidas valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ILibBusinessDetailComponent<IList<ListaDeMaterialesDetalleSalidas>, IList<ListaDeMaterialesDetalleSalidas>> GetBusinessComponent() {
            return new clsListaDeMaterialesDetalleSalidasNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoArticuloInventarioCommand = new RelayCommand<string>(ExecuteChooseCodigoArticuloInventarioCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionCodigoArticuloInventario = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Artículo Inventario", LibSearchCriteria.CreateCriteria("Codigo", CodigoArticuloInventario));
        }

        private void ExecuteChooseCodigoArticuloInventarioCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("StatusdelArticulo ", eStatusArticulo.Vigente), eLogicOperatorType.And);                
                vFixedCriteria.Add("TipoDeArticulo", eBooleanOperatorType.IdentityEquality, eTipoDeArticulo.Mercancia, eLogicOperatorType.And);
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaLoteFechaDeVencimiento")) {
                    vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityInequality, LibConvert.EnumToDbValue((int)eTipoArticuloInv.UsaSerialRollo));
                    vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityInequality, LibConvert.EnumToDbValue((int)eTipoArticuloInv.UsaTallaColorySerial));
                    vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityInequality, LibConvert.EnumToDbValue((int)eTipoArticuloInv.UsaTallaColor));
                    vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityInequality, LibConvert.EnumToDbValue((int)eTipoArticuloInv.UsaSerial));
                } else {
                    vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityEquality, eTipoArticuloInv.Simple, eLogicOperatorType.And);
                }
                ConexionCodigoArticuloInventario = Master.ChooseRecord<FkArticuloInventarioViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);                
                if (ConexionCodigoArticuloInventario == null) {
                    CodigoArticuloInventario = string.Empty;
                    DescripcionArticuloInventario = string.Empty;
                    UnidadDeVenta = string.Empty;
                    Master.CodigoDescripcionArticuloPrincipalProducir = string.Empty;
                } else {
                    CodigoArticuloInventario = ConexionCodigoArticuloInventario.Codigo;
                    DescripcionArticuloInventario = ConexionCodigoArticuloInventario.Descripcion;
                    UnidadDeVenta = ConexionCodigoArticuloInventario.UnidadDeVenta;
                    TipoArticuloInvAsEnum = ConexionCodigoArticuloInventario.TipoArticuloInv;
                    RaisePropertyChanged(() => IsVisbleTipoArticuloInvStr);
                    RaisePropertyChanged(() => TipoArticuloInvStr);
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        public int DecimalDigits {
            get {
                return 8;
            }
        }

        private ValidationResult PorcentajeDeCostoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Insertar || Action == eAccionSR.Modificar) && (PorcentajeDeCosto >= 0 || PorcentajeDeCosto <= 100)) {
                return ValidationResult.Success;
            }
            return vResult;
        }

        public bool IsVisibleCantidadMerma {
            get {
                return Master.ManejaMerma;
            }
        }

        private ValidationResult MermaNormalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (Master.ManejaMerma) {
                if ((Action == eAccionSR.Insertar || Action == eAccionSR.Modificar) && (MermaNormal >= 0)) {
                    return ValidationResult.Success;
                } else {
                    vResult = new ValidationResult("La cantidad de merma normal (Salidas) debe ser igual o superior a 0. ");
                }
            }else {
                MermaNormal = 0;
                PorcentajeMermaNormal = 0;
                return ValidationResult.Success;
            }
            return vResult;
        }

        private void CalculaPorcentajeMerma() {
            PorcentajeMermaNormal = 0;
            if (Cantidad != 0) {
                PorcentajeMermaNormal = LibMath.RoundToNDecimals(((MermaNormal * 100) / Cantidad), 8);
            }
        }

        internal void IsVisbleMermaSalida() {
            RaisePropertyChanged(() => IsVisibleCantidadMerma);
        }
        #endregion //Metodos Generados
    } //End of class ListaDeMaterialesDetalleSalidasViewModel

} //End of namespace Galac.Adm.Uil.GestionProduccion

