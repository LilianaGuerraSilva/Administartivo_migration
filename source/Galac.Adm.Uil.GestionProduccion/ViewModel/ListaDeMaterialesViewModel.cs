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
    public class ListaDeMaterialesViewModel : LibInputMasterViewModelMfc<ListaDeMateriales> {

        #region Constantes

        private const string CodigoPropertyName = "Codigo";
        private const string NombrePropertyName = "Nombre";
        private const string CodigoArticuloInventarioPropertyName = "CodigoArticuloInventario";
        private const string DescripcionArticuloInventarioPropertyName = "DescripcionArticuloInventario";
        private const string FechaCreacionPropertyName = "FechaCreacion";
        private const string NombreOperadorPropertyName = "NombreOperador";
        private const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";

        #endregion

        #region Variables

        private FkArticuloInventarioViewModel _ConexionCodigoArticuloInventario = null;

        #endregion //Variables

        #region Propiedades

        public override string ModuleName {
            get {
                string vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreParaMostrarListaDeMateriales");
                if(LibString.IsNullOrEmpty(vResult)) {
                    vResult = "Lista de Materiales";
                }
                return vResult;
            }
        }

        public int ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if(Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                }
            }
        }

        public int Consecutivo {
            get {
                return Model.Consecutivo;
            }
            set {
                if(Model.Consecutivo != value) {
                    Model.Consecutivo = value;
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Código es requerido.")]
        [LibGridColum("Código", DbMemberPath = "Adm.Gv_ListaDeMateriales_B1.Codigo", MaxLength = 30)]
        [LibCustomValidation("ExisteCodigoValidating")]
        public string Codigo {
            get {
                return Model.Codigo;
            }
            set {
                if(Model.Codigo != value) {
                    Model.Codigo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoPropertyName);
                }
            }
        }

        [LibGridColum("Nombre", Width = 400, Trimming = System.Windows.TextTrimming.WordEllipsis)]
        [LibRequired(ErrorMessage = "El campo Nombre es requerido.")]
        [LibCustomValidation("ExisteNombreValidating")]
        public string Nombre {
            get {
                return Model.Nombre;
            }
            set {
                if(Model.Nombre != value) {
                    Model.Nombre = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombrePropertyName);
                }
            }
        }

        [LibGridColum("Código Artículo a Producir", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoArticuloInventario", ConnectionSearchCommandName = "ChooseCodigoArticuloInventarioCommand", Width = 200)]
        [LibRequired(ErrorMessage = "El campo Código Artículo a Producir es requerido.")]
        public string CodigoArticuloInventario {
            get {
                return Model.CodigoArticuloInventario;
            }
            set {
                if(Model.CodigoArticuloInventario != value) {
                    Model.CodigoArticuloInventario = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoArticuloInventarioPropertyName);
                    if(LibString.IsNullOrEmpty(CodigoArticuloInventario, true)) {
                        ConexionCodigoArticuloInventario = null;
                    }
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Descripción Artículo es requerido.")]
        [LibGridColum("Descripción del Artículo a Producir", Width = 600, IsForSearch = false, Trimming = System.Windows.TextTrimming.WordEllipsis)]
        public string DescripcionArticuloInventario {
            get {
                return Model.DescripcionArticuloInventario;
            }
            set {
                if(Model.DescripcionArticuloInventario != value) {
                    Model.DescripcionArticuloInventario = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionArticuloInventarioPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Fecha de Creación es requerido.")]
        [LibCustomValidation("FechaCreacionValidating")]
        [LibGridColum("Fecha de Creación", eGridColumType.DatePicker, BindingStringFormat = "dd/MM/yyyy")]
        public DateTime FechaCreacion {
            get {
                return Model.FechaCreacion;
            }
            set {
                if(Model.FechaCreacion != value) {
                    Model.FechaCreacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaCreacionPropertyName);
                }
            }
        }

        public string NombreOperador {
            get {
                return Model.NombreOperador;
            }
            set {
                if(Model.NombreOperador != value) {
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
                if(Model.FechaUltimaModificacion != value) {
                    Model.FechaUltimaModificacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaUltimaModificacionPropertyName);
                }
            }
        }

        [LibDetailRequired(ErrorMessage = "Productos y/o Servicios es requerido.")]
        public ListaDeMaterialesDetalleArticuloMngViewModel DetailListaDeMaterialesDetalleArticulo {
            get;
            set;
        }

        public FkArticuloInventarioViewModel ConexionCodigoArticuloInventario {
            get {
                return _ConexionCodigoArticuloInventario;
            }
            set {
                if(_ConexionCodigoArticuloInventario != value) {
                    _ConexionCodigoArticuloInventario = value;
                    RaisePropertyChanged(CodigoArticuloInventarioPropertyName);
                }
                if(_ConexionCodigoArticuloInventario == null) {
                    CodigoArticuloInventario = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoArticuloInventarioCommand {
            get;
            private set;
        }

        public RelayCommand<string> CreateListaDeMaterialesDetalleArticuloCommand {
            get { return DetailListaDeMaterialesDetalleArticulo.CreateCommand; }
        }

        public RelayCommand<string> UpdateListaDeMaterialesDetalleArticuloCommand {
            get { return DetailListaDeMaterialesDetalleArticulo.UpdateCommand; }
        }

        public RelayCommand<string> DeleteListaDeMaterialesDetalleArticuloCommand {
            get { return DetailListaDeMaterialesDetalleArticulo.DeleteCommand; }
        }

        public bool IsEnabledCodigo {
            get {
                return IsEnabled && Action == eAccionSR.Insertar;
            }
        }

        public string NombreTemp { get; set; }


        #endregion //Propiedades

        #region Constructores e Inicializadores

        public ListaDeMaterialesViewModel()
            : this(new ListaDeMateriales(), eAccionSR.Insertar) {
        }
        public ListaDeMaterialesViewModel(ListaDeMateriales initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = CodigoPropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            InitializeDetails();
            NombreTemp = Nombre;
        }

        protected override void InitializeLookAndFeel(ListaDeMateriales valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        public override void InitializeViewModel(eAccionSR valAction, string valCustomAction) {
            base.InitializeViewModel(valAction, valCustomAction);
            if(valAction == eAccionSR.Insertar) {
                FechaCreacion = LibDefGen.DateForInitializeInputValue();
            }
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoArticuloInventarioCommand = new RelayCommand<string>(ExecuteChooseCodigoArticuloInventarioCommand);
        }

        protected override void InitializeDetails() {
            DetailListaDeMaterialesDetalleArticulo = new ListaDeMaterialesDetalleArticuloMngViewModel(this, Model.DetailListaDeMaterialesDetalleArticulo, Action);
            DetailListaDeMaterialesDetalleArticulo.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<ListaDeMaterialesDetalleArticuloViewModel>>(DetailListaDeMaterialesDetalleArticulo_OnCreated);
            DetailListaDeMaterialesDetalleArticulo.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<ListaDeMaterialesDetalleArticuloViewModel>>(DetailListaDeMaterialesDetalleArticulo_OnUpdated);
            DetailListaDeMaterialesDetalleArticulo.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<ListaDeMaterialesDetalleArticuloViewModel>>(DetailListaDeMaterialesDetalleArticulo_OnDeleted);
            DetailListaDeMaterialesDetalleArticulo.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<ListaDeMaterialesDetalleArticuloViewModel>>(DetailListaDeMaterialesDetalleArticulo_OnSelectedItemChanged);
        }

        #endregion //Constructores e Inicializadores

        #region Metodos Generados

        protected override ListaDeMateriales FindCurrentRecord(ListaDeMateriales valModel) {
            if(valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "ListaDeMaterialesGET", vParams.Get(), UseDetail).FirstOrDefault();
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<ListaDeMateriales>, IList<ListaDeMateriales>> GetBusinessComponent() {
            return new clsListaDeMaterialesNav();
        }

        #region ListaDeMaterialesDetalleArticulo

        private void DetailListaDeMaterialesDetalleArticulo_OnSelectedItemChanged(object sender, SearchCollectionChangedEventArgs<ListaDeMaterialesDetalleArticuloViewModel> e) {
            try {
                UpdateListaDeMaterialesDetalleArticuloCommand.RaiseCanExecuteChanged();
                DeleteListaDeMaterialesDetalleArticuloCommand.RaiseCanExecuteChanged();
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailListaDeMaterialesDetalleArticulo_OnDeleted(object sender, SearchCollectionChangedEventArgs<ListaDeMaterialesDetalleArticuloViewModel> e) {
            try {
                IsDirty = true;
                Model.DetailListaDeMaterialesDetalleArticulo.Remove(e.ViewModel.GetModel());
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailListaDeMaterialesDetalleArticulo_OnUpdated(object sender, SearchCollectionChangedEventArgs<ListaDeMaterialesDetalleArticuloViewModel> e) {
            try {
                IsDirty = e.ViewModel.IsDirty;
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailListaDeMaterialesDetalleArticulo_OnCreated(object sender, SearchCollectionChangedEventArgs<ListaDeMaterialesDetalleArticuloViewModel> e) {
            try {
                Model.DetailListaDeMaterialesDetalleArticulo.Add(e.ViewModel.GetModel());
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        #endregion //ListaDeMaterialesDetalleArticulo

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            //ConexionCodigoArticuloInventario = FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Artículo Inventario", LibSearchCriteria.CreateCriteria("Codigo", CodigoArticuloInventario));
        }

        private void ExecuteChooseCodigoArticuloInventarioCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_ArticuloInventario_B2.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("StatusdelArticulo ", eStatusArticulo.Vigente), eLogicOperatorType.And);
                vFixedCriteria.Add("TipoArticuloInv", eBooleanOperatorType.IdentityEquality, eTipoArticuloInv.Simple, eLogicOperatorType.And);
                vFixedCriteria.Add("TipoDeArticulo", eBooleanOperatorType.IdentityEquality, eTipoDeArticulo.Mercancia, eLogicOperatorType.And);
                ConexionCodigoArticuloInventario = ChooseRecord<FkArticuloInventarioViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if(ConexionCodigoArticuloInventario != null) {
                    CodigoArticuloInventario = ConexionCodigoArticuloInventario.Codigo;
                    DescripcionArticuloInventario = ConexionCodigoArticuloInventario.Descripcion;
                } else {
                    CodigoArticuloInventario = string.Empty;
                    DescripcionArticuloInventario = string.Empty;
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult FechaCreacionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaCreacion, false, Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de Creación"));
                }
            }
            return vResult;
        }

        private ValidationResult ExisteCodigoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if(Action == eAccionSR.Insertar && ((IListaDeMaterialesPdn)BusinessComponent).ExisteListaDeMaterialesConEsteNombre(ConsecutivoCompania, Nombre)) {
                vResult = new ValidationResult("Ya existe una Lista con este Código.");
            }
            return vResult;
        }

        private ValidationResult ExisteNombreValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if(Action == eAccionSR.Insertar || Action == eAccionSR.Modificar) {
                if(!LibString.S1IsEqualToS2(Nombre.Trim(), NombreTemp.Trim())) {
                    vResult = ((IListaDeMaterialesPdn)BusinessComponent).ExisteListaDeMaterialesConEsteNombre(ConsecutivoCompania, Nombre) ?
                        new ValidationResult("Ya existe una Lista con este Nombre.")
                        : ValidationResult.Success;
                }
            }
            return vResult;
        }

        #endregion //Metodos Generados

    } //End of class ListaDeMaterialesViewModel

} //End of namespace Galac.Saw.Uil.Inventario

