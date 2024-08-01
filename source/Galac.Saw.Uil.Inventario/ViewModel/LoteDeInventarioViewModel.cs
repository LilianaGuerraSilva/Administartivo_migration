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
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Uil.Inventario.ViewModel {
    public class LoteDeInventarioViewModel : LibInputMasterViewModelMfc<LoteDeInventario> {
        #region Constantes
        public const string CodigoLotePropertyName = "CodigoLote";
        public const string CodigoArticuloPropertyName = "CodigoArticulo";
        public const string FechaDeElaboracionPropertyName = "FechaDeElaboracion";
        public const string FechaDeVencimientoPropertyName = "FechaDeVencimiento";
        public const string ExistenciaPropertyName = "Existencia";
        public const string StatusLoteInvPropertyName = "StatusLoteInv";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        #endregion
        #region Variables
        private FkArticuloInventarioViewModel _ConexionCodigoArticulo = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Lote de Inventario"; }
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

        [LibRequired(ErrorMessage = "El campo Código es requerido.")]
        [LibGridColum("Código", MaxLength=30)]
        public string  CodigoLote {
            get {
                return Model.CodigoLote;
            }
            set {
                if (Model.CodigoLote != value) {
                    Model.CodigoLote = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoLotePropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Código de Artículo es requerido.")]
        public string  CodigoArticulo {
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

        [LibCustomValidation("FechaDeElaboracionValidating")]
        [LibGridColum("Fecha Elab.", eGridColumType.DatePicker)]
        public DateTime  FechaDeElaboracion {
            get {
                return Model.FechaDeElaboracion;
            }
            set {
                if (Model.FechaDeElaboracion != value) {
                    Model.FechaDeElaboracion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaDeElaboracionPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaDeVencimientoValidating")]
        [LibGridColum("Fecha Vcto.", eGridColumType.DatePicker)]
        public DateTime  FechaDeVencimiento {
            get {
                return Model.FechaDeVencimiento;
            }
            set {
                if (Model.FechaDeVencimiento != value) {
                    Model.FechaDeVencimiento = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaDeVencimientoPropertyName);
                }
            }
        }

        [LibGridColum("Existencia", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        public decimal  Existencia {
            get {
                return Model.Existencia;
            }
            set {
                if (Model.Existencia != value) {
                    Model.Existencia = value;
                    IsDirty = true;
                    RaisePropertyChanged(ExistenciaPropertyName);
                }
            }
        }

        [LibGridColum("Status", eGridColumType.Enum, PrintingMemberPath = "StatusLoteInvStr")]
        public eStatusLoteDeInventario  StatusLoteInv {
            get {
                return Model.StatusLoteInvAsEnum;
            }
            set {
                if (Model.StatusLoteInvAsEnum != value) {
                    Model.StatusLoteInvAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(StatusLoteInvPropertyName);
                }
            }
        }

        public string  NombreOperador {
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

        public DateTime  FechaUltimaModificacion {
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

        public eStatusLoteDeInventario[] ArrayStatusLoteDeInventario {
            get {
                return LibEnumHelper<eStatusLoteDeInventario>.GetValuesInArray();
            }
        }

        [LibDetailRequired(ErrorMessage = "Lote De Inventario Movimiento es requerido.")]
        public LoteDeInventarioMovimientoMngViewModel DetailLoteDeInventarioMovimiento {
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

        public RelayCommand<string> CreateLoteDeInventarioMovimientoCommand {
            get { return DetailLoteDeInventarioMovimiento.CreateCommand; }
        }

        public RelayCommand<string> UpdateLoteDeInventarioMovimientoCommand {
            get { return DetailLoteDeInventarioMovimiento.UpdateCommand; }
        }

        public RelayCommand<string> DeleteLoteDeInventarioMovimientoCommand {
            get { return DetailLoteDeInventarioMovimiento.DeleteCommand; }
        }
        #endregion //Propiedades
        #region Constructores
        public LoteDeInventarioViewModel()
            : this(new LoteDeInventario(), eAccionSR.Insertar) {
        }
        public LoteDeInventarioViewModel(LoteDeInventario initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = CodigoLotePropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            InitializeDetails();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(LoteDeInventario valModel) {
            base.InitializeLookAndFeel(valModel);
            if (LibString.IsNullOrEmpty(CodigoLote, true)) {
                CodigoLote = GenerarProximoCodigoLote();
            }
        }

        protected override LoteDeInventario FindCurrentRecord(LoteDeInventario valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "LoteDeInventarioGET", vParams.Get(), UseDetail).FirstOrDefault();
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<LoteDeInventario>, IList<LoteDeInventario>> GetBusinessComponent() {
            return new clsLoteDeInventarioNav();
        }

        private string GenerarProximoCodigoLote() {
            string vResult = string.Empty;
            XElement vResulset = GetBusinessComponent().QueryInfo(eProcessMessageType.Message, "ProximoCodigoLote", Mfc.GetIntAsParam("Compania"), false);
            vResult = LibXml.GetPropertyString(vResulset, "CodigoLote");
            return vResult;
        }

        protected override void InitializeDetails() {
            DetailLoteDeInventarioMovimiento = new LoteDeInventarioMovimientoMngViewModel(this, Model.DetailLoteDeInventarioMovimiento, Action);
            DetailLoteDeInventarioMovimiento.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<LoteDeInventarioMovimientoViewModel>>(DetailLoteDeInventarioMovimiento_OnCreated);
            DetailLoteDeInventarioMovimiento.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<LoteDeInventarioMovimientoViewModel>>(DetailLoteDeInventarioMovimiento_OnUpdated);
            DetailLoteDeInventarioMovimiento.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<LoteDeInventarioMovimientoViewModel>>(DetailLoteDeInventarioMovimiento_OnDeleted);
            DetailLoteDeInventarioMovimiento.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<LoteDeInventarioMovimientoViewModel>>(DetailLoteDeInventarioMovimiento_OnSelectedItemChanged);
        }
        #region LoteDeInventarioMovimiento

        private void DetailLoteDeInventarioMovimiento_OnSelectedItemChanged(object sender, SearchCollectionChangedEventArgs<LoteDeInventarioMovimientoViewModel> e) {
            try {
                UpdateLoteDeInventarioMovimientoCommand.RaiseCanExecuteChanged();
                DeleteLoteDeInventarioMovimientoCommand.RaiseCanExecuteChanged();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailLoteDeInventarioMovimiento_OnDeleted(object sender, SearchCollectionChangedEventArgs<LoteDeInventarioMovimientoViewModel> e) {
            try {
                IsDirty = true;
                Model.DetailLoteDeInventarioMovimiento.Remove(e.ViewModel.GetModel());
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailLoteDeInventarioMovimiento_OnUpdated(object sender, SearchCollectionChangedEventArgs<LoteDeInventarioMovimientoViewModel> e) {
            try {
                IsDirty = e.ViewModel.IsDirty;
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailLoteDeInventarioMovimiento_OnCreated(object sender, SearchCollectionChangedEventArgs<LoteDeInventarioMovimientoViewModel> e) {
            try {
                Model.DetailLoteDeInventarioMovimiento.Add(e.ViewModel.GetModel());
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //LoteDeInventarioMovimiento

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoArticuloCommand = new RelayCommand<string>(ExecuteChooseCodigoArticuloCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionCodigoArticulo = FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Artículo Inventario", LibSearchCriteria.CreateCriteria("Codigo", CodigoArticulo));
        }

        private void ExecuteChooseCodigoArticuloCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCodigoArticulo = ChooseRecord<FkArticuloInventarioViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoArticulo != null) {
                    CodigoArticulo = ConexionCodigoArticulo.Codigo;
                } else {
                    CodigoArticulo = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult FechaDeElaboracionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDeElaboracion, false, Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de Elaboración"));
                }
            }
            return vResult;
        }

        private ValidationResult FechaDeVencimientoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDeVencimiento, false, Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de Vencimiento"));
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class LoteDeInventarioViewModel

} //End of namespace Galac.Saw.Uil.Inventario

