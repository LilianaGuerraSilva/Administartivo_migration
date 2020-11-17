using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows  ;
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
using Galac.Adm.Brl.CajaChica;
using Galac.Adm.Ccl.CajaChica;
using Galac.Adm.Ccl.Banco;


namespace Galac.Adm.Uil.CajaChica.ViewModel {
    public class RendicionViewModel:LibInputMasterViewModelMfc<Rendicion> {
        #region Constantes
        public const string NumeroPropertyName = "Numero";
        public const string FechaAperturaPropertyName = "FechaApertura";
        public const string CodigoCtaBancariaCajaChicaPropertyName = "CodigoCtaBancariaCajaChica";
        public const string NombreCuentaBancariaCajaChicaPropertyName = "NombreCuentaBancariaCajaChica";
        public const string DescripcionPropertyName = "Descripcion";
        public const string StatusRendicionPropertyName = "StatusRendicion";
        public const string SeccionFacturasPropertyName = "SeccionFacturas";
        public const string SeccionCierrePropertyName = "SeccionCierre";
        public const string FechaCierrePropertyName = "FechaCierre";
        public const string CodigoCuentaBancariaPropertyName = "CodigoCuentaBancaria";
        public const string NombreCuentaBancariaPropertyName = "NombreCuentaBancaria";
        public const string NumeroDocumentoPropertyName = "NumeroDocumento";
        public const string BeneficiarioChequePropertyName = "BeneficiarioCheque";
        public const string SeccionAnulacionPropertyName = "SeccionAnulacion";
        public const string FechaAnulacionPropertyName = "FechaAnulacion";
        public const string TotalExentoPropertyName = "TotalExento";
        public const string TotalGravablePropertyName = "TotalGravable";
        public const string TotalIVAPropertyName = "TotalIVA";
        public const string TotalRetencionPropertyName = "TotalRetencion";
        public const string SaldoPropertyName = "Saldo";
        public const string ObservacionesPropertyName = "Observaciones";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        public const string DetailDetalleDeRendicionPropertyName = "DetailDetalleDeRendicion";
        public const string SelectedTabIndexPropertyName = "SelectedTabIndex";
        public const string GeneraImpuestoBancarioPropertyName = "GeneraImpuestoBancario";
        #endregion
        #region Variables
        private FkCuentaBancariaViewModel _ConexionCodigoCtaBancariaCajaChica = null;
        private FkCuentaBancariaViewModel _ConexionCodigoCuentaBancaria = null;
        private FkRendicionViewModel _ConexionNumeroRendicion = null;
        private bool _PuedoGenerarITF = false;
        public int _EmpresaAplicaIVAEspecial;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Reposición de Caja Chica"; }
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

        [LibGridColum("Número")]
        public string Numero {
            get {
                return Model.Numero;
            }
            set {
                if(Model.Numero != value) {
                    Model.Numero = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroPropertyName);
                }
            }
        }

        public eTipoDeDocumentoRendicion TipoDeDocumento {
            get {
                return Model.TipoDeDocumentoAsEnum;
            }
            set {
                if(Model.TipoDeDocumentoAsEnum != value) {
                    Model.TipoDeDocumentoAsEnum = value;
                }
            }
        }

        public int ConsecutivoBeneficiario {
            get {
                return Model.ConsecutivoBeneficiario;
            }
            set {
                if(Model.ConsecutivoBeneficiario != value) {
                    Model.ConsecutivoBeneficiario = value;
                }
            }
        }

        public string CodigoBeneficiario {
            get {
                return Model.CodigoBeneficiario;
            }
            set {
                if(Model.CodigoBeneficiario != value) {
                    Model.CodigoBeneficiario = value;
                }
            }
        }

        public string NombreBeneficiario {
            get {
                return Model.NombreBeneficiario;
            }
            set {
                if(Model.NombreBeneficiario != value) {
                    Model.NombreBeneficiario = value;
                }
            }
        }

        [LibCustomValidation("FechaAperturaValidating")]
        [LibGridColum("Fecha de Apertura",eGridColumType.DatePicker,Width = 150)]
        public DateTime FechaApertura {
            get {
                return Model.FechaApertura;
            }
            set {
                if(Model.FechaApertura != value) {
                    Model.FechaApertura = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaAperturaPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Caja Chica es requerido.")]
        [LibGridColum("Caja Chica",eGridColumType.Connection,ConnectionDisplayMemberPath = "Codigo",ConnectionModelPropertyName = "CodigoCtaBancariaCajaChica",ConnectionSearchCommandName = "ChooseCodigoCtaBancariaCajaChicaCommand")]
        public string CodigoCtaBancariaCajaChica {
            get {
                return Model.CodigoCtaBancariaCajaChica;
            }
            set {
                if(Model.CodigoCtaBancariaCajaChica != value) {
                    Model.CodigoCtaBancariaCajaChica = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoCtaBancariaCajaChicaPropertyName);
                    if(LibString.IsNullOrEmpty(CodigoCtaBancariaCajaChica,true)) {
                        ConexionCodigoCtaBancariaCajaChica = null;
                    }
                }
            }
        }

        [LibGridColum("Nombre Caja Chica",Width = 300)]
        public string NombreCuentaBancariaCajaChica {
            get {
                return Model.NombreCuentaBancariaCajaChica;
            }
            set {
                if(Model.NombreCuentaBancariaCajaChica != value) {
                    Model.NombreCuentaBancariaCajaChica = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCuentaBancariaCajaChicaPropertyName);
                }
            }
        }

        public string Descripcion {
            get {
                return Model.Descripcion;
            }
            set {
                if(Model.Descripcion != value) {
                    Model.Descripcion = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionPropertyName);
                }
            }
        }

        [LibGridColum("Estado",eGridColumType.Enum,PrintingMemberPath = "StatusRendicionStr")]
        public eStatusRendicion StatusRendicion {
            get {
                return Model.StatusRendicionAsEnum;
            }
            set {
                if(Model.StatusRendicionAsEnum != value) {
                    Model.StatusRendicionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(StatusRendicionPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaCierreValidating")]
        public DateTime FechaCierre {
            get {
                return Model.FechaCierre;
            }
            set {
                if(Model.FechaCierre != value) {
                    Model.FechaCierre = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaCierrePropertyName);
                }
            }
        }

        [LibCustomValidation("CodigoCuentaBancariaValidating")]
        public string CodigoCuentaBancaria {
            get {
                return Model.CodigoCuentaBancaria;
            }
            set {
                if(Model.CodigoCuentaBancaria != value) {
                    Model.CodigoCuentaBancaria = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoCuentaBancariaPropertyName);
                    if(LibString.IsNullOrEmpty(CodigoCuentaBancaria,true)) {
                        ConexionCodigoCuentaBancaria = null;
                    } else {
                        OnCodigoCuentaBancariaChanged();
                    }
                }
            }
        }

        public string NombreCuentaBancaria {
            get {
                return Model.NombreCuentaBancaria;
            }
            set {
                if(Model.NombreCuentaBancaria != value) {
                    Model.NombreCuentaBancaria = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCuentaBancariaPropertyName);
                }
            }
        }

        public bool GeneraImpuestoBancario {
            get {
                return Model.GeneraImpuestoBancarioAsBool;

            }
            set {
                if(Model.GeneraImpuestoBancarioAsBool != value) {
                    Model.GeneraImpuestoBancarioAsBool = value;
                    IsDirty = true;
                    bool maneja = (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros","ManejaDebitoBancario"));
                    RaisePropertyChanged(GeneraImpuestoBancarioPropertyName);

                }
            }
        }

        [LibCustomValidation("NumeroDocumentoValidating")]
        public string NumeroDocumento {
            get {
                return Model.NumeroDocumento;
            }
            set {
                if(Model.NumeroDocumento != value) {
                    Model.NumeroDocumento = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroDocumentoPropertyName);
                }
            }
        }

        [LibCustomValidation("BeneficiarioChequeValidating")]
        public string BeneficiarioCheque {
            get {
                return Model.BeneficiarioCheque;
            }
            set {
                if(Model.BeneficiarioCheque != value) {
                    Model.BeneficiarioCheque = value;
                    IsDirty = true;
                    RaisePropertyChanged(BeneficiarioChequePropertyName);
                }
            }
        }

        public string CodigoConceptoBancario {
            get {
                return Model.CodigoConceptoBancario;
            }
            set {
                if(Model.CodigoConceptoBancario != value) {
                    Model.CodigoConceptoBancario = value;
                }
            }
        }

        public string NombreConceptoBancario {
            get {
                return Model.NombreConceptoBancario;
            }
            set {
                if(Model.NombreConceptoBancario != value) {
                    Model.NombreConceptoBancario = value;
                }
            }
        }

        [LibCustomValidation("FechaAnulacionValidating")]
        public DateTime FechaAnulacion {
            get {
                return Model.FechaAnulacion;
            }
            set {
                if(Model.FechaAnulacion != value) {
                    Model.FechaAnulacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaAnulacionPropertyName);
                }
            }
        }
        decimal _TotalExento;
        public decimal TotalExento {
            get {
                return _TotalExento;
            }
            set {
                if(_TotalExento != value) {
                    _TotalExento = value;
                    IsDirty = true;
                    RaisePropertyChanged(TotalExentoPropertyName);
                }
            }
        }
        decimal _TotalGravable;
        public decimal TotalGravable {
            get {
                return _TotalGravable;
            }
            set {
                if(_TotalGravable != value) {
                    _TotalGravable = value;
                    IsDirty = true;
                    RaisePropertyChanged(TotalGravablePropertyName);
                }
            }
        }

        public decimal TotalAdelantos {
            get {
                return Model.TotalAdelantos;
            }
            set {
                if(Model.TotalAdelantos != value) {
                    Model.TotalAdelantos = value;
                }
            }
        }

        public decimal TotalGastos {
            get {
                return Model.TotalGastos;
            }
            set {
                if(Model.TotalGastos != value) {
                    Model.TotalGastos = value;
                }
            }
        }

        decimal _TotalIVA;
        public decimal TotalIVA {
            get {
                return _TotalIVA;
            }
            set {
                if(_TotalIVA != value) {
                    _TotalIVA = value;
                    IsDirty = true;
                    RaisePropertyChanged(TotalIVAPropertyName);
                }
            }
        }

        decimal _TotalRetencion;
        public decimal TotalRetencion {
            get {
                return _TotalRetencion;
            }
            set {
                if(_TotalRetencion != value) {
                    _TotalRetencion = value;
                    IsDirty = true;
                    RaisePropertyChanged(TotalRetencionPropertyName);
                }
            }
        }

        private decimal _Saldo;
        public decimal Saldo {
            get {
                return _Saldo;
            }
            set {
                if(_Saldo != value) {
                    _Saldo = value;
                    IsDirty = true;
                    RaisePropertyChanged(SaldoPropertyName);
                }
            }
        }

        public string Observaciones {
            get {
                return Model.Observaciones;
            }
            set {
                if(Model.Observaciones != value) {
                    Model.Observaciones = value;
                    IsDirty = true;
                    RaisePropertyChanged(ObservacionesPropertyName);
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

        public eTipoDeDocumentoRendicion[] ArrayTipoDeDocumentoRendicion {
            get {
                return LibEnumHelper<eTipoDeDocumentoRendicion>.GetValuesInArray();
            }
        }

        public eStatusRendicion[] ArrayStatusRendicion {
            get {
                return LibEnumHelper<eStatusRendicion>.GetValuesInArray();
            }
        }

        //[LibCustomValidation("ValidateDetalleDeRendicion")]
        public DetalleDeRendicionMngViewModel DetailDetalleDeRendicion {
            get;
            set;
        }

        public FkCuentaBancariaViewModel ConexionCodigoCtaBancariaCajaChica {
            get {
                return _ConexionCodigoCtaBancariaCajaChica;
            }
            set {
                if(_ConexionCodigoCtaBancariaCajaChica != value) {
                    _ConexionCodigoCtaBancariaCajaChica = value;
                    RaisePropertyChanged(CodigoCtaBancariaCajaChicaPropertyName);
                }
                if(_ConexionCodigoCtaBancariaCajaChica == null) {
                    CodigoCtaBancariaCajaChica = string.Empty;
                }
            }
        }

        public FkCuentaBancariaViewModel ConexionCodigoCuentaBancaria {
            get {
                return _ConexionCodigoCuentaBancaria;
            }
            set {
                if(_ConexionCodigoCuentaBancaria != value) {
                    _ConexionCodigoCuentaBancaria = value;
                    RaisePropertyChanged(CodigoCuentaBancariaPropertyName);
                }
                if(_ConexionCodigoCuentaBancaria == null) {
                    CodigoCuentaBancaria = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoCtaBancariaCajaChicaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoCuentaBancariaCommand {
            get;
            private set;
        }

        public RelayCommand<string> CreateDetalleDeRendicionCommand {
            get { return DetailDetalleDeRendicion.CreateCommand; }
        }

        public RelayCommand<string> UpdateDetalleDeRendicionCommand {
            get { return DetailDetalleDeRendicion.UpdateCommand; }
        }

        public RelayCommand<string> DeleteDetalleDeRendicionCommand {
            get { return DetailDetalleDeRendicion.DeleteCommand; }
        }

        public bool IsVisibleEscogerNumeroRendicion {
            get {
                return Action == LibGalac.Aos.Base.eAccionSR.Cerrar || Action == LibGalac.Aos.Base.eAccionSR.Anular || Action == LibGalac.Aos.Base.eAccionSR.ReImprimir || Action == eAccionSR.Contabilizar;
            }
        }

        public bool IsVisibleEditarNumeroRendicion {
            get {
                return !IsVisibleEscogerNumeroRendicion;
            }
        }

        public FkRendicionViewModel ConexionNumeroRendicion {
            get {
                return _ConexionNumeroRendicion;
            }
            set {
                if(_ConexionNumeroRendicion != value) {
                    _ConexionNumeroRendicion = value;
                    RaisePropertyChanged(NumeroDocumentoPropertyName);
                    if(ConexionNumeroRendicion != null) {
                        Numero = ConexionNumeroRendicion.Numero;
                    }
                }
                if(_ConexionNumeroRendicion == null) {
                    Numero = string.Empty;
                }
                ExecuteActionCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand<string> ChooseNumeroRendicionCommand {
            get;
            private set;
        }


        public bool IsVisibleITF {
            get {
                return ((LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros","ManejaDebitoBancario")) ? !EsEcuador() : false);
            }
        }

        public bool PuedoGenerarITF {
            get {
                return _PuedoGenerarITF;
            }
            set {
                if(_PuedoGenerarITF != value) {
                    _PuedoGenerarITF = value;
                    IsDirty = true;
                    RaisePropertyChanged("PuedoGenerarITF");
                }
            }
        }

        public string TituloDebito {
            get {
                return "Genera I.T.F";
            }
        }

        private bool EsEcuador() {
            return LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.IsCountryEcuador();
        }

        #endregion //Propiedades
        #region Constructores
        public RendicionViewModel()
            : this(new Rendicion(),eAccionSR.Insertar) {
        }
        public RendicionViewModel(Rendicion initModel,eAccionSR initAction)
            : base(initModel,initAction,LibGlobalValues.Instance.GetAppMemInfo(),LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = FechaAperturaPropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            if(Action == eAccionSR.Cerrar)
                SelectedTabIndex = 1;
            else if(Action == eAccionSR.Anular)
                SelectedTabIndex = 2;
            if(LibText.IsNullOrEmpty(Model.Numero))
                Model.Numero = ((clsRendicionNav)GetBusinessComponent()).SiguienteNumero(Model.ConsecutivoCompania);
            _EmpresaAplicaIVAEspecial = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("CajaChica","AplicarIVAEspecial");
            InitializeDetails();
        }

        public RendicionViewModel(eAccionSR initAction)
            : base(new Rendicion(),initAction,LibGlobalValues.Instance.GetAppMemInfo(),LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = FechaAperturaPropertyName;
            if(Action == eAccionSR.Cerrar)
                SelectedTabIndex = 1;
            else if(Action == eAccionSR.Anular)
                SelectedTabIndex = 2;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(Rendicion valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override Rendicion FindCurrentRecord(Rendicion valModel) {
            Rendicion vResult;
            if(valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania",valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo",valModel.Consecutivo);
            vResult = BusinessComponent.GetData(eProcessMessageType.SpName,"RendicionGET",vParams.Get(),UseDetail).FirstOrDefault();
            if(vResult == null) {
                vResult = new Rendicion();
            }
            if(vResult.DetailDetalleDeRendicion != null && vResult.DetailDetalleDeRendicion.Count > 0) {
                vResult.DetailDetalleDeRendicion = new System.Collections.ObjectModel.ObservableCollection<DetalleDeRendicion>(from i in vResult.DetailDetalleDeRendicion orderby i.GeneradaPorAsEnum descending select i);
            }
            return vResult;

        }

        protected override ILibBusinessMasterComponentWithSearch<IList<Rendicion>,IList<Rendicion>> GetBusinessComponent() {
            return new clsRendicionNav();
        }

        protected override void InitializeDetails() {
            DetailDetalleDeRendicion = new DetalleDeRendicionMngViewModel(this,Model.DetailDetalleDeRendicion,Action);
            DetailDetalleDeRendicion.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<DetalleDeRendicionViewModel>>(DetailDetalleDeRendicion_OnCreated);
            DetailDetalleDeRendicion.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<DetalleDeRendicionViewModel>>(DetailDetalleDeRendicion_OnUpdated);
            DetailDetalleDeRendicion.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<DetalleDeRendicionViewModel>>(DetailDetalleDeRendicion_OnDeleted);
            DetailDetalleDeRendicion.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<DetalleDeRendicionViewModel>>(DetailDetalleDeRendicion_OnSelectedItemChanged);
            CalcularTotales();
        }
        #region DetalleDeRendicion

        private void DetailDetalleDeRendicion_OnSelectedItemChanged(object sender,SearchCollectionChangedEventArgs<DetalleDeRendicionViewModel> e) {
            try {
                UpdateDetalleDeRendicionCommand.RaiseCanExecuteChanged();
                DeleteDetalleDeRendicionCommand.RaiseCanExecuteChanged();
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        protected override bool CanExecuteDeleteSelectedDetailCommand(object valSelectedItem) {
            DetalleDeRendicionViewModel vViewModel = valSelectedItem as DetalleDeRendicionViewModel;
            if(vViewModel == null) {
                return true;
            }
            if(vViewModel.GeneradaPor != eGeneradoPor.Rendicion) {
                LibMessages.MessageBox.Alert(this,"No puede eliminar un registro generado por el usuario.","Información");
                return false;
            } else {
                return base.CanExecuteDeleteSelectedDetailCommand(valSelectedItem);
            }
        }

        private void DetailDetalleDeRendicion_OnDeleted(object sender,SearchCollectionChangedEventArgs<DetalleDeRendicionViewModel> e) {
            try {
                if(e.ViewModel.GetModel().GeneradaPorAsEnum != eGeneradoPor.Rendicion) {
                    LibMessages.MessageBox.Alert(this,"No puede eliminar un registro generado por el usuario.","Información");
                }
                IsDirty = true;
                Model.DetailDetalleDeRendicion.Remove(e.ViewModel.GetModel());
                e.ViewModel.PropertyChanged -= OnDetailPropertyChanged;
                ActualizaSaldos();
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void DetailDetalleDeRendicion_OnUpdated(object sender,SearchCollectionChangedEventArgs<DetalleDeRendicionViewModel> e) {
            try {
                IsDirty = e.ViewModel.IsDirty;
                ActualizaSaldos();
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void DetailDetalleDeRendicion_OnCreated(object sender,SearchCollectionChangedEventArgs<DetalleDeRendicionViewModel> e) {
            try {
                Model.DetailDetalleDeRendicion.Add(e.ViewModel.GetModel());
                e.ViewModel.PropertyChanged += OnDetailPropertyChanged;
                ActualizaSaldos();
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void OnDetailPropertyChanged(object sender,System.ComponentModel.PropertyChangedEventArgs e) {
            ActualizaSaldos();
        }
        #endregion //DetalleDeRendicion

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoCtaBancariaCajaChicaCommand = new RelayCommand<string>(ExecuteChooseCodigoCtaBancariaCajaChicaCommand);
            ChooseCodigoCuentaBancariaCommand = new RelayCommand<string>(ExecuteChooseCodigoCuentaBancariaCommand);
            ChooseNumeroRendicionCommand = new RelayCommand<string>(ExecuteChooseNumeroRendicionCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionCodigoCtaBancariaCajaChica = FirstConnectionRecordOrDefault<FkCuentaBancariaViewModel>("Cuenta Bancaria",CriteriaParaConexionCajaChica(CodigoCtaBancariaCajaChica));
            ConexionCodigoCuentaBancaria = FirstConnectionRecordOrDefault<FkCuentaBancariaViewModel>("Cuenta Bancaria",CriteriaParaConexionCuentaBancaria(CodigoCuentaBancaria));
        }

        private void ExecuteChooseCodigoCtaBancariaCajaChicaCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = CriteriaParaConexionCajaChica(valCodigo);
                LibSearchCriteria vFixedCriteria = CriteriaParaConexionCajaChica();

                ConexionCodigoCtaBancariaCajaChica = ChooseRecord<FkCuentaBancariaViewModel>("Cuenta Bancaria",vDefaultCriteria,vFixedCriteria,string.Empty);
                if(ConexionCodigoCtaBancariaCajaChica != null) {
                    CodigoCtaBancariaCajaChica = ConexionCodigoCtaBancariaCajaChica.Codigo;
                    NombreCuentaBancariaCajaChica = ConexionCodigoCtaBancariaCajaChica.NombreCuenta;
                } else {
                    CodigoCtaBancariaCajaChica = string.Empty;
                    NombreCuentaBancariaCajaChica = string.Empty;
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseCodigoCuentaBancariaCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = CriteriaParaConexionCuentaBancaria(valCodigo);
                LibSearchCriteria vFixedCriteria = CriteriaParaConexionCuentaBancaria();
                ConexionCodigoCuentaBancaria = ChooseRecord<FkCuentaBancariaViewModel>("Cuenta Bancaria",vDefaultCriteria,vFixedCriteria,string.Empty);
                if(ConexionCodigoCuentaBancaria != null) {
                    CodigoCuentaBancaria = ConexionCodigoCuentaBancaria.Codigo;
                    NombreCuentaBancaria = ConexionCodigoCuentaBancaria.NombreCuenta;
                } else {
                    CodigoCuentaBancaria = string.Empty;
                    NombreCuentaBancaria = string.Empty;
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private ValidationResult FechaAperturaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaApertura,false,Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de Apertura"));
                }
                if(LibDate.DateIsGreaterThanToday(FechaApertura,false,"")) {
                    vResult = new ValidationResult(LibDate.MsgDateIsGreaterThanToday("FechaApertura"));
                }
            }
            return vResult;
        }

        private ValidationResult FechaCierreValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaCierre,false,Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de Cierre"));
                }
                if(LibDate.F1IsLessThanF2(FechaCierre,FechaApertura)) {
                    vResult = new ValidationResult("Fecha de Cierre no puede ser menor que la Fecha de Apertura.");
                }
                if(LibDate.DateIsGreaterThanToday(FechaCierre,false,"")) {
                    vResult = new ValidationResult(LibDate.MsgDateIsGreaterThanToday("Fecha de Cierre"));
                }
            }
            return vResult;
        }

        private ValidationResult FechaAnulacionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Cerrar) || (Action == eAccionSR.ReImprimir) || (Action == eAccionSR.Contabilizar)) {
                return ValidationResult.Success;
            } else {
                if(LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaAnulacion,false,Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de Anulación"));
                }
                if(FechaAnulacion < FechaCierre) {
                    vResult = new ValidationResult("Fecha de Anulación no puede ser menor que la Fecha de Cierre");
                }
                if(LibDate.DateIsGreaterThanToday(FechaAnulacion,false,"")) {
                    vResult = new ValidationResult(LibDate.MsgDateIsGreaterThanToday("Fecha de Anulación"));
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados

        public bool IsVisibleTabCierre {
            get {

                if(Action.Equals(eAccionSR.Consultar)) {
                    return Model.StatusRendicionAsEnum.Equals(eStatusRendicion.Cerrada) || Model.StatusRendicionAsEnum.Equals(eStatusRendicion.Anulada);
                }

                return Action.Equals(eAccionSR.Cerrar) || Action.Equals(eAccionSR.Anular);
            }
        }

        public bool IsVisibleTabAnulacion {
            get {
                if(Action.Equals(eAccionSR.Consultar)) {
                    return Model.StatusRendicionAsEnum.Equals(eStatusRendicion.Anulada);
                }
                return Action.Equals(eAccionSR.Anular);
            }
        }

        private LibSearchCriteria CriteriaParaConexionCajaChica(string codigo) {
            LibSearchCriteria vSearchcriteria;
            vSearchcriteria = LibSearchCriteria.CreateCriteria("Saw.Gv_CuentaBancaria_B1.Codigo",codigo);
            vSearchcriteria.Add(CriteriaParaConexionCajaChica(),eLogicOperatorType.And);
            return vSearchcriteria;
        }

        private LibSearchCriteria CriteriaParaConexionCajaChica() {
            LibSearchCriteria vConsecutivoCompania;
            LibSearchCriteria vTipoCajaChica;

            vConsecutivoCompania = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",Mfc.GetInt("Compania"));
            vTipoCajaChica = LibSearchCriteria.CreateCriteria("EsCajaChica",true);

            vConsecutivoCompania.Add(vTipoCajaChica,eLogicOperatorType.And);
            return vConsecutivoCompania;
        }

        private LibSearchCriteria CriteriaParaConexionCuentaBancaria(string codigo) {
            LibSearchCriteria vSearchcriteria;
            vSearchcriteria = LibSearchCriteria.CreateCriteria("Saw.Gv_CuentaBancaria_B1.Codigo",codigo);
            vSearchcriteria.Add(CriteriaParaConexionCuentaBancaria(),eLogicOperatorType.And);
            return vSearchcriteria;
        }

        private LibSearchCriteria CriteriaParaConexionCuentaBancaria() {
            LibSearchCriteria vConsecutivoCompania;
            LibSearchCriteria vTipoCajaChica;

            vConsecutivoCompania = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",Mfc.GetInt("Compania"));
            vTipoCajaChica = LibSearchCriteria.CreateCriteria("EsCajaChica",false);

            vConsecutivoCompania.Add(vTipoCajaChica,eLogicOperatorType.And);
            return vConsecutivoCompania;
        }

        public void CalcularTotales() {
            TotalIVA = DetailDetalleDeRendicion.Items.Sum(p => p.MontoIVA + p.MontoIVAAlicuotaEspecial1 + p.MontoIVAAlicuotaEspecial2);
            TotalExento = DetailDetalleDeRendicion.Items.Sum(p => p.MontoExento);
            TotalGravable = DetailDetalleDeRendicion.Items.Sum(p => p.MontoGravable + p.MontoGravableAlicuotaEspecial1 + p.MontoGravableAlicuotaEspecial2);
            TotalRetencion = DetailDetalleDeRendicion.Items.Sum(p => p.MontoRetencion);
            Saldo = TotalIVA + TotalExento + TotalGravable - TotalRetencion;
            TotalGastos = DetailDetalleDeRendicion.Items.Sum(p => p.MontoTotal);
        }

        protected override bool RecordIsReadOnly() {
            return base.RecordIsReadOnly() || Action.Equals(eAccionSR.Cerrar) || Action.Equals(eAccionSR.Anular) || Action.Equals(eAccionSR.ReImprimir) || Action == eAccionSR.Contabilizar;
        }

        public bool IsReadCerrar {
            get {
                return Action.Equals(eAccionSR.Cerrar);
            }
        }

        public bool IsReadAnular {
            get {
                return Action.Equals(eAccionSR.Anular);
            }
        }

        protected override void ExecuteSpecialAction(eAccionSR valAction) {
            try {
                if(Action.Equals(eAccionSR.Cerrar)) {
                    List<Rendicion> vRendicion = new List<Rendicion>();
                    vRendicion.Add(Model);
                    DialogResult = GetBusinessComponent().DoAction(vRendicion,eAccionSR.Cerrar,null,true).Success;

                    if(DialogResult == true) {
                        CloseOnActionComplete = true;
                        LibMessages.MessageBox.Information(null,String.Format("La Reposición Número {0} se cerró correctamente",Model.Numero),"Información");
                    }

                } else if(Action.Equals(eAccionSR.Anular)) {
                    List<Rendicion> vRendicion = new List<Rendicion>();
                    vRendicion.Add(Model);
                    DialogResult = GetBusinessComponent().DoAction(vRendicion,eAccionSR.Anular,null,true).Success;

                    if(DialogResult == true) {
                        CloseOnActionComplete = true;
                        LibMessages.MessageBox.Information(null,String.Format("La Reposición Número {0} se anuló correctamente",Model.Numero),"Información");
                    }
                } else if(valAction == eAccionSR.ReImprimir) {
                    DialogResult = true;
                    CloseOnActionComplete = true;
                } else if(valAction == eAccionSR.Contabilizar) {
                    DialogResult = true;
                    CloseOnActionComplete = true;
                } else {
                    base.ExecuteSpecialAction(valAction);
                }
            } catch(GalacException vEx) {
                DetailDetalleDeRendicion.Items.ToList().ForEach(item => item.ValidoAsBoolRaisePropertyChanged());
                DetailDetalleDeRendicion.Items.ToList().ForEach(item => item.GeneradoPorUsuarioAsBoolRaisePropertyChanged());
                SelectedTabIndex = 0;
                throw vEx;
            } catch(Exception vEx) {
                throw vEx;
            }
        }

        protected override bool CreateRecord() {
            try {
                return base.CreateRecord();
            } catch(Exception) {
                DetailDetalleDeRendicion.Items.ToList().ForEach(item => item.ValidoAsBoolRaisePropertyChanged());
                DetailDetalleDeRendicion.Items.ToList().ForEach(item => item.GeneradoPorUsuarioAsBoolRaisePropertyChanged());
                throw;
            }
        }

        protected override bool UpdateRecord() {
            try {
                return base.UpdateRecord();
            } catch(Exception) {
                DetailDetalleDeRendicion.Items.ToList().ForEach(item => item.ValidoAsBoolRaisePropertyChanged());
                DetailDetalleDeRendicion.Items.ToList().ForEach(item => item.GeneradoPorUsuarioAsBoolRaisePropertyChanged());
                throw;
            }
        }

        private ValidationResult BeneficiarioChequeValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Anular) || (Action == eAccionSR.ReImprimir)) {
                return ValidationResult.Success;
            } else {
                if(Action == eAccionSR.Cerrar && LibText.IsNullOrEmpty(BeneficiarioCheque)) {
                    vResult = new ValidationResult("El campo Beneficiario es requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult NumeroDocumentoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Anular) || (Action == eAccionSR.ReImprimir)) {
                return ValidationResult.Success;
            } else {
                if(Action == eAccionSR.Cerrar && LibText.IsNullOrEmpty(NumeroDocumento)) {
                    vResult = new ValidationResult("El campo Número de Cheque es requerido.");

                }
            }
            return vResult;
        }

        private ValidationResult CodigoCuentaBancariaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar) || (Action == eAccionSR.Anular) || (Action == eAccionSR.ReImprimir)) {
                return ValidationResult.Success;
            } else {
                if(Action == eAccionSR.Cerrar && LibText.IsNullOrEmpty(CodigoCuentaBancaria)) {
                    vResult = new ValidationResult("El campo Cuenta Bancaria no es de tipo Caja Chica, por lo tanto no puede completar el cierre. Modifique estos valores para poder completar el cierre.");
                }
            }
            return vResult;
        }

        private int _SelectedTabIndex;
        public int SelectedTabIndex {
            get {
                return _SelectedTabIndex;
            }
            set {
                if(_SelectedTabIndex != value) {
                    _SelectedTabIndex = value;
                    IsDirty = true;
                    RaisePropertyChanged(SelectedTabIndexPropertyName);
                }
            }
        }

        private ValidationResult ValidateDetalleDeRendicion() {
            ValidationResult vResult = ValidationResult.Success;
            if(Action == eAccionSR.Consultar) {
                return ValidationResult.Success;
            } else {
                if(DetailDetalleDeRendicion.Items.Count == 0) {
                    return new ValidationResult("No se puede grabar un comprobante sin asientos.");
                }
                if(!LibString.IsNullOrEmpty(DetailDetalleDeRendicion.Error)) {
                    return new ValidationResult(DetailDetalleDeRendicion.Error);
                }
                if(DetailDetalleDeRendicion != null) {
                    var vCount = DetailDetalleDeRendicion.Items.GroupBy(p => new {
                        p.NumeroDocumento,
                        p.CodigoProveedor
                    }).Where(q => q.Count() > 1);
                    if(vCount != null && vCount.Count() > 0) {
                        return new ValidationResult("Existen Compras Duplicadas, por favor modifique el Número de Documento y Proveedor.");
                    }
                }
            }
            return vResult;
        }

        private void ActualizaSaldos() {
            TotalExento = DetailDetalleDeRendicion.Items.Sum(i => i.MontoExento);
            TotalIVA = DetailDetalleDeRendicion.Items.Sum(i => i.MontoIVA + i.MontoGravableAlicuotaEspecial1 + i.MontoIVAAlicuotaEspecial2);
            TotalGravable = DetailDetalleDeRendicion.Items.Sum(i => i.MontoGravable);
            TotalRetencion = DetailDetalleDeRendicion.Items.Sum(i => i.MontoRetencion);
            Saldo = TotalExento + TotalGravable + TotalIVA - TotalRetencion;
            TotalGastos = DetailDetalleDeRendicion.Items.Sum(i => i.MontoTotal);
        }

        private void ExecuteChooseNumeroRendicionCommand(string valNumero) {
            string vModuleName = "Reposición de Caja Chica";
            try {
                if(valNumero == null) {
                    valNumero = string.Empty;
                }
                LibSearchCriteria vSearchcriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_Rendicion_B1.Numero",valNumero);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_Rendicion_B1.ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                if(Action == LibGalac.Aos.Base.eAccionSR.Cerrar) {
                    vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("StatusRendicion",eStatusRendicion.EnProceso),eLogicOperatorType.And);
                } else if(Action == LibGalac.Aos.Base.eAccionSR.Anular) {
                    vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("StatusRendicion",eStatusRendicion.Cerrada),eLogicOperatorType.And);
                } else if(Action == LibGalac.Aos.Base.eAccionSR.ReImprimir) {
                    vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("StatusRendicion",eStatusRendicion.Cerrada),eLogicOperatorType.And);
                } else if(Action == LibGalac.Aos.Base.eAccionSR.Contabilizar) {
                    vModuleName = "ContabilizarRendicion";
                    vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("StatusRendicion",eStatusRendicion.Cerrada),eLogicOperatorType.And);
                }
                ConexionNumeroRendicion = null;
                ConexionNumeroRendicion = ChooseRecord<FkRendicionViewModel>(vModuleName,vSearchcriteria,vFixedCriteria,string.Empty);
                if(ConexionNumeroRendicion == null) {
                    Numero = "";
                } else {
                    Model.ConsecutivoCompania = ConexionNumeroRendicion.ConsecutivoCompania;
                    Model.Consecutivo = ConexionNumeroRendicion.Consecutivo;
                    InitializeViewModel(Action);
                    GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly)
                      .ToList().ForEach(p => RaisePropertyChanged(p.Name));
                    if(DetailDetalleDeRendicion != null && DetailDetalleDeRendicion.Items != null && DetailDetalleDeRendicion.Items.Count > 0) {
                        DetailDetalleDeRendicion.SelectedItem = DetailDetalleDeRendicion.Items[0];
                    }
                }

            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        public override void InitializeViewModel(eAccionSR valAction) {
            base.InitializeViewModel(valAction);
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            if(Action == eAccionSR.Cerrar)
                SelectedTabIndex = 1;
            else if(Action == eAccionSR.Anular)
                SelectedTabIndex = 2;
            if(LibText.IsNullOrEmpty(Model.Numero) && valAction != eAccionSR.Cerrar && valAction != eAccionSR.Anular && valAction != eAccionSR.ReImprimir && valAction != eAccionSR.Contabilizar)
                Model.Numero = ((clsRendicionNav)GetBusinessComponent()).SiguienteNumero(Model.ConsecutivoCompania);
        }

        protected override bool CanExecuteAction() {
            if(Action == eAccionSR.Cerrar || Action == eAccionSR.Anular || Action == eAccionSR.ReImprimir || Action == eAccionSR.Contabilizar) {
                return !LibText.IsNullOrEmpty(Model.Numero);
            } else {
                return base.CanExecuteAction();
            }
        }
        private void OnCodigoCuentaBancariaChanged() {
            PuedoGenerarITF = (ConexionCodigoCuentaBancaria.ManejaDebitoBancario && (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros","ManejaDebitoBancario")));
            GeneraImpuestoBancario = PuedoGenerarITF;
        }
    } //End of class RendicionViewModel

} //End of namespace Galac.Adm.Uil.CajaChica

