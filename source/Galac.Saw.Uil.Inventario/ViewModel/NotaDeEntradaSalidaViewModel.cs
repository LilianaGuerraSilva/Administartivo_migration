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
using Galac.Saw.Lib;
using System.Text;
using Galac.Saw.Uil.Inventario.Reportes;
using LibGalac.Aos.Brl;

namespace Galac.Saw.Uil.Inventario.ViewModel {
    public class NotaDeEntradaSalidaViewModel : LibInputMasterViewModelMfc<NotaDeEntradaSalida> {
        #region Constantes
        public const string NumeroDocumentoPropertyName = "NumeroDocumento";
        public const string TipodeOperacionPropertyName = "TipodeOperacion";
        public const string CodigoClientePropertyName = "CodigoCliente";
        public const string NombreClientePropertyName = "NombreCliente";
        public const string CodigoAlmacenPropertyName = "CodigoAlmacen";
        public const string NombreAlmacenPropertyName = "NombreAlmacen";
        public const string FechaPropertyName = "Fecha";
        public const string ComentariosPropertyName = "Comentarios";
        public const string StatusNotaEntradaSalidaPropertyName = "StatusNotaEntradaSalida";
        public const string ConsecutivoAlmacenPropertyName = "ConsecutivoAlmacen";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        #endregion
        #region Variables
        private FkClienteViewModel _ConexionCodigoCliente = null;
        private FkClienteViewModel _ConexionNombreCliente = null;
        private FkAlmacenViewModel _ConexionCodigoAlmacen = null;
        private FkAlmacenViewModel _ConexionNombreAlmacen = null;
        #endregion //Variables
        #region Propiedades
        public override string ModuleName {
            get { return "Nota de Entrada/Salida"; }
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

        [LibRequired(ErrorMessage = "El campo Numero Documento es requerido.")]
        [LibGridColum("N° Documento", MaxLength=11)]
        public string  NumeroDocumento {
            get {
                return Model.NumeroDocumento;
            }
            set {
                if (Model.NumeroDocumento != value) {
                    Model.NumeroDocumento = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroDocumentoPropertyName);
                }
            }
        }

        [LibGridColum("Operación", eGridColumType.Enum, PrintingMemberPath = "TipodeOperacionStr", Width =150)]
        public eTipodeOperacion  TipodeOperacion {
            get {
                return Model.TipodeOperacionAsEnum;
            }
            set {
                if (Model.TipodeOperacionAsEnum != value) {
                    Model.TipodeOperacionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipodeOperacionPropertyName);
                }
            }
        }

        public string  CodigoCliente {
            get {
                return Model.CodigoCliente;
            }
            set {
                if (Model.CodigoCliente != value) {
                    Model.CodigoCliente = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoClientePropertyName);
                    ExecuteChooseCodigoClienteCommand(CodigoCliente);
                    RaisePropertyChanged(NombreAlmacenPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoCliente, true)) {
                        ConexionCodigoCliente = null;
                    }
                }
            }
        }

        public string  NombreCliente {
            get {
                return Model.NombreCliente;
            }
            set {
                if (Model.NombreCliente != value) {
                    Model.NombreCliente = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreClientePropertyName);                    
                    if (LibString.IsNullOrEmpty(NombreCliente, true)) {
                        ConexionNombreCliente = null;
                    }
                }
            }
        }

        [LibGridColum("Código del Almacén", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoAlmacen", ConnectionSearchCommandName = "ChooseCodigoAlmacenCommand", MaxWidth=120)]
        public string  CodigoAlmacen {
            get {
                return Model.CodigoAlmacen;
            }
            set {
                if (Model.CodigoAlmacen != value) {
                    Model.CodigoAlmacen = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoAlmacenPropertyName);
                    ExecuteChooseCodigoAlmacenCommand(CodigoAlmacen);
                    RaisePropertyChanged(NombreAlmacenPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoAlmacen, true)) {
                        ConexionCodigoAlmacen = null;
                    }
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nombre Almacen es requerido.")]
        [LibGridColum("Nombre del Almacén", eGridColumType.Connection, ConnectionDisplayMemberPath = "NombreAlmacen", ConnectionModelPropertyName = "NombreAlmacen", ConnectionSearchCommandName = "ChooseNombreAlmacenCommand", Width = 150)]
        public string  NombreAlmacen {
            get {
                return Model.NombreAlmacen;
            }
            set {
                if (Model.NombreAlmacen != value) {
                    Model.NombreAlmacen = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreAlmacenPropertyName);
                    if (LibString.IsNullOrEmpty(NombreAlmacen, true)) {
                        ConexionNombreAlmacen = null;
                    }
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Fecha es requerido.")]
        [LibCustomValidation("FechaValidating")]
        public DateTime  Fecha {
            get {
                return Model.Fecha;
            }
            set {
                if (Model.Fecha != value) {
                    Model.Fecha = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Comentario es requerido.")]
        public string  Comentarios {
            get {
                return Model.Comentarios;
            }
            set {
                if (Model.Comentarios != value) {
                    Model.Comentarios = value;
                    IsDirty = true;
                    RaisePropertyChanged(ComentariosPropertyName);
                }
            }
        }

        public string  CodigoLote {
            get {
                return Model.CodigoLote;
            }
            set {
                if (Model.CodigoLote != value) {
                    Model.CodigoLote = value;
                }
            }
        }

        public string StatusNotaEntradaSalidaStr { 
            get { return Model.StatusNotaEntradaSalidaAsString; }
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

        public eTipoGeneradoPorNotaDeEntradaSalida  GeneradoPor {
            get {
                return Model.GeneradoPorAsEnum;
            }
            set {
                if (Model.GeneradoPorAsEnum != value) {
                    Model.GeneradoPorAsEnum = value;
                }
            }
        }

        public int  ConsecutivoDocumentoOrigen {
            get {
                return Model.ConsecutivoDocumentoOrigen;
            }
            set {
                if (Model.ConsecutivoDocumentoOrigen != value) {
                    Model.ConsecutivoDocumentoOrigen = value;
                }
            }
        }

        public eTipoNotaProduccion  TipoNotaProduccion {
            get {
                return Model.TipoNotaProduccionAsEnum;
            }
            set {
                if (Model.TipoNotaProduccionAsEnum != value) {
                    Model.TipoNotaProduccionAsEnum = value;
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

        public eTipodeOperacion[] ArrayTipodeOperacion {
            get {
                return LibEnumHelper<eTipodeOperacion>.GetValuesInArray();
            }
        }

        public eStatusNotaEntradaSalida[] ArrayStatusNotaEntradaSalida {
            get {
                return LibEnumHelper<eStatusNotaEntradaSalida>.GetValuesInArray();
            }
        }

        public eTipoGeneradoPorNotaDeEntradaSalida[] ArrayTipoGeneradoPorNotaDeEntradaSalida {
            get {
                return LibEnumHelper<eTipoGeneradoPorNotaDeEntradaSalida>.GetValuesInArray();
            }
        }

        public eTipoNotaProduccion[] ArrayTipoNotaProduccion {
            get {
                return LibEnumHelper<eTipoNotaProduccion>.GetValuesInArray();
            }
        }

        [LibDetailRequired(ErrorMessage = "Renglon Nota ES es requerido.")]
        public RenglonNotaESMngViewModel DetailRenglonNotaES {
            get;
            set;
        }

        public FkClienteViewModel ConexionCodigoCliente {
            get {                
                return _ConexionCodigoCliente;
            }
            set {
                if (_ConexionCodigoCliente != value) {
                    _ConexionCodigoCliente = value;
                    RaisePropertyChanged(CodigoClientePropertyName);
                }
                if (_ConexionCodigoCliente == null) {
                    CodigoCliente = string.Empty;
                }
            }
        }

        public FkClienteViewModel ConexionNombreCliente {
            get {
                return _ConexionNombreCliente;
            }
            set {
                if (_ConexionNombreCliente != value) {
                    _ConexionNombreCliente = value;
                    RaisePropertyChanged(NombreClientePropertyName);
                }
                if (_ConexionNombreCliente == null) {
                    NombreCliente = string.Empty;
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
                }
            }
        }

        public FkAlmacenViewModel ConexionNombreAlmacen {
            get {
                return _ConexionNombreAlmacen;
            }
            set {
                if (_ConexionNombreAlmacen != value) {
                    _ConexionNombreAlmacen = value;
                    RaisePropertyChanged(NombreAlmacenPropertyName);
                }
                if (_ConexionNombreAlmacen == null) {
                    NombreAlmacen = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoClienteCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseNombreClienteCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoAlmacenCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseNombreAlmacenCommand {
            get;
            private set;
        }

        public RelayCommand<string> CreateRenglonNotaESCommand {
            get { return DetailRenglonNotaES.CreateCommand; }
        }

        public RelayCommand<string> UpdateRenglonNotaESCommand {
            get { return DetailRenglonNotaES.UpdateCommand; }
        }

        public RelayCommand<string> DeleteRenglonNotaESCommand {
            get { return DetailRenglonNotaES.DeleteCommand; }
        }

        public RelayCommand ConsultarOrdenDeProduccionCommand {
            get;
            private set;
        }

        public bool IsVisibleConsultarOrdenDeProduccion {
            get { return GeneradoPor == eTipoGeneradoPorNotaDeEntradaSalida.OrdenDeProduccion; }
        }
        #endregion //Propiedades
        #region Constructores
        public NotaDeEntradaSalidaViewModel()
            : this(new NotaDeEntradaSalida(), eAccionSR.Insertar) {
        }
        public NotaDeEntradaSalidaViewModel(NotaDeEntradaSalida initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = NumeroDocumentoPropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");            
            InitializeDetails();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(NotaDeEntradaSalida valModel) {
            base.InitializeLookAndFeel(valModel);
            if (LibString.IsNullOrEmpty(NumeroDocumento, true)) {
                NumeroDocumento = GenerarProximoNumeroDocumento();
            }
            if (Action == eAccionSR.Insertar) {//FASE 1 Lote/FdV: No se maneja almacén
                CodigoAlmacen = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoAlmacenGenerico");
            }
            CodigoCliente = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoGenericoCliente");
        }

        protected override NotaDeEntradaSalida FindCurrentRecord(NotaDeEntradaSalida valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInString("NumeroDocumento", valModel.NumeroDocumento, 11);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "NotaDeEntradaSalidaGET", vParams.Get(), UseDetail).FirstOrDefault();
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>> GetBusinessComponent() {
            return new clsNotaDeEntradaSalidaNav();
        }

        private string GenerarProximoNumeroDocumento() {
            string vResult = string.Empty;
            XElement vResulset = GetBusinessComponent().QueryInfo(eProcessMessageType.Message, "ProximoNumeroDocumento", Mfc.GetIntAsParam("Compania"), false);
            vResult = LibXml.GetPropertyString(vResulset, "NumeroDocumento");
            return vResult;
        }

        protected override void InitializeDetails() {
            DetailRenglonNotaES = new RenglonNotaESMngViewModel(this, Model.DetailRenglonNotaES, Action);
            DetailRenglonNotaES.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<RenglonNotaESViewModel>>(DetailRenglonNotaES_OnCreated);
            DetailRenglonNotaES.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<RenglonNotaESViewModel>>(DetailRenglonNotaES_OnUpdated);
            DetailRenglonNotaES.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<RenglonNotaESViewModel>>(DetailRenglonNotaES_OnDeleted);
            DetailRenglonNotaES.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<RenglonNotaESViewModel>>(DetailRenglonNotaES_OnSelectedItemChanged);
        }

        protected override void ExecuteAction() {
            if (Action.Equals(eAccionSR.Anular)) {
                CloseOnActionComplete = true;
                if (Model.TipodeOperacionAsEnum == eTipodeOperacion.Retiro) {
                    if (LibMessages.MessageBox.YesNo(this, "¿Está seguro que desea Anular esta Nota de Retiro de Inventario?", ModuleName)) {
                        INotaDeEntradaSalidaPdn vNotaDeEntradaSalidaNav = new clsNotaDeEntradaSalidaNav();
                        IList<NotaDeEntradaSalida> vNotaDeEntradaSalida = new List<NotaDeEntradaSalida>();
                        vNotaDeEntradaSalida.Add(Model);
                        LibResponse vDialgoResult = vNotaDeEntradaSalidaNav.AnularRecord(vNotaDeEntradaSalida);
                        if (vDialgoResult.Success) {
                            LibMessages.MessageBox.Information(this, String.Format("La Nota de Entrada {0} se anuló correctamente.", Model.NumeroDocumento), "Información");
                        } else {
                            LibMessages.MessageBox.Information(this, vDialgoResult.GetInformation(), ModuleName);
                        }
                    }
                } else {
                    LibMessages.MessageBox.Information(this, "Sólo se pueden Anular las Notas de Retiro de Inventario.", ModuleName);
                }
                RaiseRequestCloseEvent();
            } else if (Action.Equals(eAccionSR.Reversar)) {
                CloseOnActionComplete = true;
                if (Model.TipodeOperacionAsEnum != eTipodeOperacion.Retiro) {
                    if (Model.GeneradoPorAsEnum == eTipoGeneradoPorNotaDeEntradaSalida.Usuario) {
                        if (LibString.S1StartsWithS2(Model.Comentarios, "Reverso de la Nota E/S:")) {
                            LibMessages.MessageBox.Information(this, "Esta Nota de E/S ya es un reverso y no puede ser reversada.", ModuleName);
                        } else {
                            if (LibMessages.MessageBox.YesNo(this, "¿Está seguro que desea reversar esta Nota de Entrada/Salida de Inventario?", ModuleName)) {
                                INotaDeEntradaSalidaPdn vNotaDeEntradaSalidaNav = new clsNotaDeEntradaSalidaNav();
                                IList<NotaDeEntradaSalida> vNotaDeEntradaSalida = new List<NotaDeEntradaSalida>();
                                vNotaDeEntradaSalida.Add(Model);
                                LibResponse vDialgoResult = vNotaDeEntradaSalidaNav.ReversarNotaES(Model.ConsecutivoCompania, Model.NumeroDocumento);
                                if (vDialgoResult.Success) {
                                    LibMessages.MessageBox.Information(this, String.Format("La Nota de Entrada/Salida de Inventario {0} se reversó correctamente.", Model.NumeroDocumento), "Información");
                                } else {
                                    LibMessages.MessageBox.Information(this, vDialgoResult.GetInformation(), ModuleName);
                                }
                            }
                        }
                    } else {
                        LibMessages.MessageBox.Information(this, "Solo se pueden reversar las Notas de E/S ingresadas por Usuarios.", ModuleName);
                    }
                } else {
                    LibMessages.MessageBox.Information(this, "El tipo de operación Retiro no puede ser reversado.", ModuleName);
                }
                RaiseRequestCloseEvent();
            } else if (Action == eAccionSR.ReImprimir) {
                CloseOnActionComplete = true;
                DialogResult = true;
                clsNotaDeEntradaSalidaInformesViewModel insViewModel = new clsNotaDeEntradaSalidaInformesViewModel();
                insViewModel.ConfigReportNotaEntradaSalida(NumeroDocumento);
            } else {
                base.ExecuteAction();
            }
        }

        public override bool OnClosing() {
            if (Action == eAccionSR.Anular || Action == eAccionSR.ReImprimir) {
                return false;
            } else {
                return base.OnClosing();
            }
        }

        #region RenglonNotaES

        private void DetailRenglonNotaES_OnSelectedItemChanged(object sender, SearchCollectionChangedEventArgs<RenglonNotaESViewModel> e) {
            try {
                UpdateRenglonNotaESCommand.RaiseCanExecuteChanged();
                DeleteRenglonNotaESCommand.RaiseCanExecuteChanged();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailRenglonNotaES_OnDeleted(object sender, SearchCollectionChangedEventArgs<RenglonNotaESViewModel> e) {
            try {
                IsDirty = true;
                Model.DetailRenglonNotaES.Remove(e.ViewModel.GetModel());
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailRenglonNotaES_OnUpdated(object sender, SearchCollectionChangedEventArgs<RenglonNotaESViewModel> e) {
            try {
                IsDirty = e.ViewModel.IsDirty;
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailRenglonNotaES_OnCreated(object sender, SearchCollectionChangedEventArgs<RenglonNotaESViewModel> e) {
            try {
                Model.DetailRenglonNotaES.Add(e.ViewModel.GetModel());
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //RenglonNotaES

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoClienteCommand = new RelayCommand<string>(ExecuteChooseCodigoClienteCommand);
            ChooseNombreClienteCommand = new RelayCommand<string>(ExecuteChooseNombreClienteCommand);
            ChooseCodigoAlmacenCommand = new RelayCommand<string>(ExecuteChooseCodigoAlmacenCommand);
            ChooseNombreAlmacenCommand = new RelayCommand<string>(ExecuteChooseNombreAlmacenCommand);
            ConsultarOrdenDeProduccionCommand = new RelayCommand(ExecuteConsultarOrdenDeProduccionCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
        }

        private void ExecuteChooseCodigoClienteCommand(string valcodigo) {
            try {
                if (valcodigo == null) {
                    valcodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("codigo", valcodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCodigoCliente = ChooseRecord<FkClienteViewModel>("Cliente", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoCliente != null) {
                    CodigoCliente = ConexionCodigoCliente.Codigo;
                    NombreCliente = ConexionCodigoCliente.Nombre;
                } else {
                    CodigoCliente = string.Empty;
                    NombreCliente = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseNombreClienteCommand(string valnombre) {
            try {
                if (valnombre == null) {
                    valnombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("nombre", valnombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionNombreCliente = ChooseRecord<FkClienteViewModel>("Cliente", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionNombreCliente != null) {
                    CodigoCliente = ConexionNombreCliente.Codigo;
                    NombreCliente = ConexionNombreCliente.Nombre;
                } else {
                    CodigoCliente = string.Empty;
                    NombreCliente = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCodigoAlmacenCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Almacen_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_Almacen_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCodigoAlmacen = ChooseRecord<FkAlmacenViewModel>("Almacén", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoAlmacen != null) {
                    CodigoAlmacen = ConexionCodigoAlmacen.Codigo;
                    NombreAlmacen = ConexionCodigoAlmacen.NombreAlmacen;
                    Model.ConsecutivoAlmacen = ConexionCodigoAlmacen.Consecutivo;
                } else {
                    CodigoAlmacen = string.Empty;
                    NombreAlmacen = string.Empty;
                    Model.ConsecutivoAlmacen = 0;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseNombreAlmacenCommand(string valNombreAlmacen) {
            try {
                if (valNombreAlmacen == null) {
                    valNombreAlmacen = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Almacen_B1.NombreAlmacen", valNombreAlmacen);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Saw.Gv_Almacen_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionNombreAlmacen = ChooseRecord<FkAlmacenViewModel>("Almacén", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionNombreAlmacen != null) {
                    CodigoAlmacen = ConexionNombreAlmacen.Codigo;
                    NombreAlmacen = ConexionNombreAlmacen.NombreAlmacen;
                    ConsecutivoAlmacen = ConexionNombreAlmacen.Consecutivo;
                } else {
                    CodigoAlmacen = string.Empty;
                    NombreAlmacen = string.Empty;
                    ConsecutivoAlmacen = 0;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        public bool NotaESIsEnabled {
            get {
                return IsEnabled && Action != eAccionSR.Anular && Action != eAccionSR.ReImprimir && Action != eAccionSR.Reversar;
            }
        }

        private ValidationResult FechaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(Fecha, false, Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha"));
                } else if (LibDate.F1IsGreaterThanF2(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Parametros", "FechaMinimaIngresarDatos"), Fecha)) {
                    string vFechaMinima = LibConvert.ToStr(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Parametros", "FechaMinimaIngresarDatos"));
                    vResult = new ValidationResult("La fecha mínima de entrada de documentos para esta Compañía es: " + vFechaMinima);
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados

        protected override bool CreateRecord() {
            bool vResult = base.CreateRecord();
            bool vImprimirReporteAlIngresarNotaEntradaSalida = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "ImprimirReporteAlIngresarNotaEntradaSalida");
            if (vResult && vImprimirReporteAlIngresarNotaEntradaSalida) {
                clsNotaDeEntradaSalidaInformesViewModel insInfViewModel = new clsNotaDeEntradaSalidaInformesViewModel();
                insInfViewModel.ConfigReportNotaEntradaSalida(NumeroDocumento);
            }
            return vResult;
        }

        //protected override void ExecuteSpecialAction(eAccionSR valAction) {
        //    if (valAction == eAccionSR.ReImprimir) {
        //        CloseOnActionComplete = true;
        //        DialogResult = true;
        //        clsNotaDeEntradaSalidaInformesViewModel insViewModel = new clsNotaDeEntradaSalidaInformesViewModel();
        //        insViewModel.ConfigReportNotaEntradaSalida(ConsecutivoCompania, NumeroDocumento);
        //    }
        //}

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                if (Action == eAccionSR.Consultar) {
                    RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection.Insert(0,CreateAccionRibbonButton());
                }
            }
        }

        LibRibbonButtonData CreateAccionRibbonButton() {
            LibRibbonButtonData vResult = new LibRibbonButtonData();
            vResult.Label = "Ver Orden de Producción";
            vResult.Command = ConsultarOrdenDeProduccionCommand;
            vResult.LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/read.png", UriKind.Relative);
            vResult.ToolTipDescription = "Ver Orden de Producción";
            vResult.ToolTipTitle = "Ver Orden de Producción";
            vResult.IsVisible = IsVisibleConsultarOrdenDeProduccion;
            return vResult;
        }

        private void ExecuteConsultarOrdenDeProduccionCommand() {
            try {
                LibBusinessProcessMessage libBusinessProcessMessage = new LibBusinessProcessMessage();
                libBusinessProcessMessage.Content = ConsecutivoDocumentoOrigen;
                LibBusinessProcess.Call("ConsultarOrdenDeProduccion", libBusinessProcessMessage);                
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }


    } //End of class NotaDeEntradaSalidaViewModel

} //End of namespace Galac.Saw.Uil.Inventario

