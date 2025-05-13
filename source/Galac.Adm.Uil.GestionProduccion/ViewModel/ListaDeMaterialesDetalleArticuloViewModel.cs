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
    public class ListaDeMaterialesDetalleArticuloViewModel : LibInputDetailViewModelMfc<ListaDeMaterialesDetalleArticulo> {

        #region Constantes

        private const string CodigoArticuloInventarioPropertyName = "CodigoArticuloInventario";
        private const string DescripcionArticuloInventarioPropertyName = "DescripcionArticuloInventario";
        private const string CantidadPropertyName = "Cantidad";
        private const string UnidadDeVentaPropertyName = "UnidadDeVenta";
        private const string MermaNormalPropertyName = "MermaNormal";
        private const string PorcentajeMermaNormalPropertyName = "PorcentajeMermaNormal";
        #endregion
        #region Variables

        private FkArticuloInventarioViewModel _ConexionCodigoArticuloInventario = null;
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

        [LibGridColum("C�digo Art�culo", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoArticuloInventario", ConnectionSearchCommandName = "ChooseCodigoArticuloInventarioCommand", Width = 150, ColumnOrder = 0)]
        public string CodigoArticuloInventario {
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

        [LibRequired(ErrorMessage = "El campo Descripci�n es requerido.")]
        //[LibGridColum("Descripci�n", eGridColumType.Connection, ConnectionDisplayMemberPath = "Descripcion", ConnectionModelPropertyName = "DescripcionArticuloInventario", ConnectionSearchCommandName = "ChooseDescripcionArticuloInventarioCommand", Width = 335, Trimming = System.Windows.TextTrimming.WordEllipsis, ColumnOrder = 1)]
        public string DescripcionArticuloInventario {
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
        [LibGridColum("Cantidad", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 150, ConditionalPropertyDecimalDigits = "DecimalDigits", ColumnOrder = 1)]
        public decimal Cantidad {
            get {
                return Model.Cantidad;
            }
            set {
                if (Model.Cantidad != value) {
                    Model.Cantidad = value;
                    RaisePropertyChanged(CantidadPropertyName);
                }
            }
        }

        [LibGridColum("Unidad", eGridColumType.Connection, ConnectionDisplayMemberPath = "UnidadDeVenta", ConnectionModelPropertyName = "UnidadDeVenta", Width = 190, ColumnOrder = 2)]
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

        [LibGridColum("Merma Normal (en Unidades)", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 245, ConditionalPropertyDecimalDigits = "DecimalDigits", ColumnOrder = 3)]
        [LibCustomValidation("MermaNormalValidating")]
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
        
        [LibGridColum("% Merma Normal", eGridColumType.Numeric, Alignment = eTextAlignment.Right, Width = 160, ConditionalPropertyDecimalDigits = "DecimalDigits", ColumnOrder = 4)]
        public decimal PorcentajeMermaNormal {
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

        public int DecimalDigits {
            get {
                return 8;
            }
        }

        public bool IsVisbleTipoArticuloInvStr {
            get {
                return (!LibString.IsNullOrEmpty(CodigoArticuloInventario));
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
            get {
                if (Model.TipoDeArticuloAsEnum == eTipoDeArticulo.Mercancia) {
                    return LibEnumHelper.GetDescription(TipoArticuloInvAsEnum);
                } else if (Model.TipoDeArticuloAsEnum == eTipoDeArticulo.Servicio) {
                    return LibEnumHelper.GetDescription(eTipoDeArticulo.Servicio);
                } else {
                    return "";
                }
            }
        }

        public eTipoDeArticulo TipoDeArticuloAsEnum {
            get {
                return Model.TipoDeArticuloAsEnum;
            }
            set {
                if (Model.TipoDeArticuloAsEnum != value) {
                    Model.TipoDeArticuloAsEnum = value;
                }
            }
        }

        public bool IsVisibleCantidadMerma {
            get {
                return Master.ManejaMerma;
            }
        }
        #endregion //Propiedades

        #region Constructores

        public ListaDeMaterialesDetalleArticuloViewModel()
            : base(new ListaDeMaterialesDetalleArticulo(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }

        public ListaDeMaterialesDetalleArticuloViewModel(ListaDeMaterialesViewModel initMaster, ListaDeMaterialesDetalleArticulo initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
        }

        #endregion //Constructores

        #region Metodos Generados

        protected override void InitializeLookAndFeel(ListaDeMaterialesDetalleArticulo valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ILibBusinessDetailComponent<IList<ListaDeMaterialesDetalleArticulo>, IList<ListaDeMaterialesDetalleArticulo>> GetBusinessComponent() {
            return new clsListaDeMaterialesDetalleArticuloNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoArticuloInventarioCommand = new RelayCommand<string>(ExecuteChooseCodigoArticuloInventarioCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            // ConexionCodigoArticuloInventario = Master.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Art�culo Inventario", LibSearchCriteria.CreateCriteria("CodigoArticuloInventario", CodigoArticuloInventario));

        }

        private void ExecuteChooseCodigoArticuloInventarioCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_ArticuloInventario_B2.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("StatusdelArticulo ", eStatusArticulo.Vigente), eLogicOperatorType.And);                
                vFixedCriteria.Add("TipoDeArticulo", eBooleanOperatorType.IdentityInequality, eTipoDeArticulo.ProductoCompuesto, eLogicOperatorType.And);
                vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityInequality, LibConvert.EnumToDbValue((int)eTipoArticuloInv.UsaSerial));
                vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityInequality, LibConvert.EnumToDbValue((int)eTipoArticuloInv.UsaSerialRollo));
                vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityInequality, LibConvert.EnumToDbValue((int)eTipoArticuloInv.UsaTallaColorySerial));
                vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityInequality, LibConvert.EnumToDbValue((int)eTipoArticuloInv.UsaTallaColor));                
                ConexionCodigoArticuloInventario = Master.ChooseRecord<FkArticuloInventarioViewModel>("Art�culo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoArticuloInventario == null) {
                    CodigoArticuloInventario = string.Empty;
                    DescripcionArticuloInventario = string.Empty;
                    UnidadDeVenta = string.Empty;                    
                } else {
                    CodigoArticuloInventario = ConexionCodigoArticuloInventario.Codigo;
                    DescripcionArticuloInventario = ConexionCodigoArticuloInventario.Descripcion;
                    UnidadDeVenta = ConexionCodigoArticuloInventario.UnidadDeVenta;
                    TipoArticuloInvAsEnum = ConexionCodigoArticuloInventario.TipoArticuloInv;
                    TipoDeArticuloAsEnum = ConexionCodigoArticuloInventario.TipoDeArticulo ;
                    RaisePropertyChanged(() => IsVisbleTipoArticuloInvStr);
                    RaisePropertyChanged(() => TipoArticuloInvStr);
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult MermaNormalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (Master.ManejaMerma) {
                if ((Action == eAccionSR.Insertar || Action == eAccionSR.Modificar) && (MermaNormal >= 0)) {
                    return ValidationResult.Success;
                } else {
                    vResult = new ValidationResult("La cantidad de merma normal (Insumos) debe ser igual o superior a 0. ");
                }
            } else {
                MermaNormal = 0;
                PorcentajeMermaNormal= 0;
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

        internal void IsVisbleMermaArticulo() {
            RaisePropertyChanged(() => IsVisibleCantidadMerma);
        }
        #endregion //Metodos Generados
    } //End of class ListaDeMaterialesDetalleArticuloViewModel
} //End of namespace Galac.Saw.Uil.Inventario

