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
using Galac.Saw.Ccl.Vehiculo;
using Entity = Galac.Saw.Ccl.Vehiculo;
using Galac.Saw.Brl.Vehiculo;

namespace Galac.Saw.Uil.Vehiculo.ViewModel {
   public class VehiculoViewModel : LibInputViewModelMfc<Entity.Vehiculo> {
        #region Constantes
        public const string ConsecutivoPropertyName = "Consecutivo";
        public const string PlacaPropertyName = "Placa";
        public const string serialVINPropertyName = "serialVIN";
        public const string NombreModeloPropertyName = "NombreModelo";
        public const string MarcaPropertyName = "Marca";
        public const string AnoPropertyName = "Ano";
        public const string CodigoColorPropertyName = "CodigoColor";
        public const string DescripcionColorPropertyName = "DescripcionColor";
        public const string CodigoClientePropertyName = "CodigoCliente";
        public const string NombreClientePropertyName = "NombreCliente";
        public const string RIFClientePropertyName = "RIFCliente";
        public const string NumeroPolizaPropertyName = "NumeroPoliza";
        public const string SerialMotorPropertyName = "SerialMotor";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        #endregion
        #region Variables
        private FkModeloViewModel _ConexionNombreModelo = null;
        private FkMarcaViewModel _ConexionMarca = null;
        private FkColorViewModel _ConexionCodigoColor = null;
        private FkColorViewModel _ConexionDescripcionColor = null;
        private FkClienteViewModel _ConexionCodigoCliente = null;
        private FkClienteViewModel _ConexionNombreCliente = null;
        private FkClienteViewModel _ConexionRIFCliente = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Vehículo"; }
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
                    IsDirty = true;
                    RaisePropertyChanged(ConsecutivoPropertyName);
                }
            }
        }
      
        [LibGridColum("Placa", Width=150)]
        [LibRequired(ErrorMessage = "El campo Placa es requerido.")]
        public string  Placa {
            get {
                return Model.Placa;
            }
            set {
                if (Model.Placa != value) {
                    Model.Placa = value;
                    IsDirty = true;
                    RaisePropertyChanged(PlacaPropertyName);
                }
            }
        }

        [LibGridColum("Serial VIN", Width=200)]
        public string  serialVIN {
            get {
                return Model.serialVIN;
            }
            set {
                if (Model.serialVIN != value) {
                    Model.serialVIN = value;
                    IsDirty = true;
                    RaisePropertyChanged(serialVINPropertyName);
                }
            }
        }

        [LibGridColum("Modelo", eGridColumType.Connection, Width=150, ConnectionDisplayMemberPath = "Nombre", ConnectionModelPropertyName = "NombreModelo", ConnectionSearchCommandName = "ChooseNombreModeloCommand")]
        [LibRequired(ErrorMessage = "El Modelo del Vehículo es requerido.")]
        public string  NombreModelo {
            get {
                return Model.NombreModelo;
            }
            set {
                if (Model.NombreModelo != value) {
                    Model.NombreModelo = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreModeloPropertyName);
                    if (LibString.IsNullOrEmpty(NombreModelo, true)) {
                        ConexionNombreModelo = null;
                    }
                }
            }
        }

        public string  Marca {
            get {
                return Model.Marca;
            }
            set {
                if (Model.Marca != value) {
                    Model.Marca = value;
                    IsDirty = true;
                    RaisePropertyChanged(MarcaPropertyName);
                    if (LibString.IsNullOrEmpty(Marca, true)) {
                        ConexionMarca = null;
                    }
                }
            }
        }

        public int  Ano {
            get {
                return Model.Ano;
            }
            set {
                if (Model.Ano != value) {
                    Model.Ano = value;
                    IsDirty = true;
                    RaisePropertyChanged(AnoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El Color es requerido.")]
        [LibGridColum("Color", eGridColumType.Connection, Width = 80, ConnectionDisplayMemberPath = "CodigoColor", ConnectionModelPropertyName = "CodigoColor", ConnectionSearchCommandName = "ChooseCodigoColorCommand", IsForList = false, DbMemberPath = "Saw.Gv_Vehiculo_B1.CodigoColor")]
        public string  CodigoColor {
            get {
                return Model.CodigoColor;
            }
            set {
                if (Model.CodigoColor != value) {
                    Model.CodigoColor = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoColorPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoColor, true)) {
                        ConexionCodigoColor = null;
                    }
                }
            }
        }

        [LibGridColum("Color", eGridColumType.Connection, ConnectionDisplayMemberPath = "DescripcionColor", ConnectionModelPropertyName = "DescripcionColor", ConnectionSearchCommandName = "ChooseDescripcionColorCommand", IsForSearch= false)]
        public string  DescripcionColor {
            get {
                return Model.DescripcionColor;
            }
            set {
                if (Model.DescripcionColor != value) {
                    Model.DescripcionColor = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionColorPropertyName);
                    if (LibString.IsNullOrEmpty(DescripcionColor, true)) {
                        ConexionDescripcionColor = null;
                    }
                }
            }
        }

        [LibRequired(ErrorMessage = "El Cliente es requerido.")]
        [LibGridColum("Cliente", eGridColumType.Connection, Width = 80, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoCliente", ConnectionSearchCommandName = "ChooseCodigoClienteCommand")]
        public string  CodigoCliente {
            get {
                return Model.CodigoCliente;
            }
            set {
                if (Model.CodigoCliente != value) {
                    Model.CodigoCliente = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoClientePropertyName);
                    if (LibString.IsNullOrEmpty(CodigoCliente, true)) {
                        ConexionCodigoCliente = null;
                    }
                }
            }
        }

        [LibGridColum("Nombre Cliente", eGridColumType.Connection, Width = 170, ConnectionDisplayMemberPath = "Nombre", ConnectionModelPropertyName = "NombreCliente", ConnectionSearchCommandName = "ChooseNombreClienteCommand", IsForSearch = false)]
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

        
        public string  RIFCliente {
            get {
                return Model.RIFCliente;
            }
            set {
                if (Model.RIFCliente != value) {
                    Model.RIFCliente = value;
                    IsDirty = true;
                    RaisePropertyChanged(RIFClientePropertyName);
                    if (LibString.IsNullOrEmpty(RIFCliente, true)) {
                        ConexionRIFCliente = null;
                    }
                }
            }
        }

        [LibGridColum("Número de Póliza")]
        public string  NumeroPoliza {
            get {
                return Model.NumeroPoliza;
            }
            set {
                if (Model.NumeroPoliza != value) {
                    Model.NumeroPoliza = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroPolizaPropertyName);
                }
            }
        }

        [LibGridColum("Serial del Motor")]
        public string  SerialMotor {
            get {
                return Model.SerialMotor;
            }
            set {
                if (Model.SerialMotor != value) {
                    Model.SerialMotor = value;
                    IsDirty = true;
                    RaisePropertyChanged(SerialMotorPropertyName);
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

        public FkModeloViewModel ConexionNombreModelo {
            get {
                return _ConexionNombreModelo;
            }
            set {
                if (_ConexionNombreModelo != value) {
                    _ConexionNombreModelo = value;
                    if (_ConexionNombreModelo != null) {
                       NombreModelo = ConexionNombreModelo.Nombre;
                       Marca = ConexionNombreModelo.Marca;
                    }
                    RaisePropertyChanged(NombreModeloPropertyName);
                }
                if (_ConexionNombreModelo == null) {
                    NombreModelo = string.Empty;
                    Marca = string.Empty;
                }
            }
        }

        public FkMarcaViewModel ConexionMarca {
            get {
                return _ConexionMarca;
            }
            set {
                if (_ConexionMarca != value) {
                    _ConexionMarca = value;
                    RaisePropertyChanged(MarcaPropertyName);
                }
                if (_ConexionMarca == null) {
                    Marca = string.Empty;
                }
            }
        }

        public FkColorViewModel ConexionCodigoColor {
            get {
                return _ConexionCodigoColor;
            }
            set {
                if (_ConexionCodigoColor != value) {
                    _ConexionCodigoColor = value;
                    if (_ConexionCodigoColor != null) {
                       CodigoColor = ConexionCodigoColor.CodigoColor;
                       DescripcionColor = ConexionCodigoColor.DescripcionColor;
                    }
                    RaisePropertyChanged(CodigoColorPropertyName);
                }
                if (_ConexionCodigoColor == null) {
                    CodigoColor = string.Empty;
                    DescripcionColor = string.Empty;
                }
            }
        }

        public FkColorViewModel ConexionDescripcionColor {
            get {
                return _ConexionDescripcionColor;
            }
            set {
                if (_ConexionDescripcionColor != value) {
                    _ConexionDescripcionColor = value;
                    RaisePropertyChanged(DescripcionColorPropertyName);
                }
                if (_ConexionDescripcionColor == null) {
                    DescripcionColor = string.Empty;
                }
            }
        }

        public FkClienteViewModel ConexionCodigoCliente {
            get {
                return _ConexionCodigoCliente;
            }
            set {
                  if (_ConexionCodigoCliente != value) {
                    _ConexionCodigoCliente = value;
                    if (_ConexionCodigoCliente != null) {
                       CodigoCliente = ConexionCodigoCliente.Codigo;
                       NombreCliente = ConexionCodigoCliente.Nombre;
                       RIFCliente = ConexionCodigoCliente.NumeroRIF;
                    }
                    RaisePropertyChanged(CodigoClientePropertyName);
                }
                if (_ConexionCodigoCliente == null) {                    
                    CodigoCliente = string.Empty;
                    NombreCliente = string.Empty;
                    RIFCliente = string.Empty;
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

        public FkClienteViewModel ConexionRIFCliente {
            get {
                return _ConexionRIFCliente;
            }
            set {
                if (_ConexionRIFCliente != value) {
                    _ConexionRIFCliente = value;
                    RaisePropertyChanged(RIFClientePropertyName);
                }
                if (_ConexionRIFCliente == null) {
                    RIFCliente = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseNombreModeloCommand {
            get;
            private set;
        }


        public RelayCommand<string> ChooseCodigoColorCommand {
            get;
            private set;
        }


        public RelayCommand<string> ChooseCodigoClienteCommand {
            get;
            private set;
        }

        #endregion //Propiedades
        #region Constructores
        public VehiculoViewModel()
           : this(new Entity.Vehiculo(), eAccionSR.Insertar) {
        }
        public VehiculoViewModel(Entity.Vehiculo initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = PlacaPropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(Entity.Vehiculo valModel) {
            base.InitializeLookAndFeel(valModel);
            ConexionMarca = null;
            ConexionDescripcionColor = null;
            ConexionNombreCliente = null;
            ConexionRIFCliente = null;
        }

        protected override Entity.Vehiculo FindCurrentRecord(Entity.Vehiculo valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "VehiculoGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>> GetBusinessComponent() {
           return new clsVehiculoNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNombreModeloCommand = new RelayCommand<string>(ExecuteChooseNombreModeloCommand);
            ChooseCodigoColorCommand = new RelayCommand<string>(ExecuteChooseCodigoColorCommand);
            ChooseCodigoClienteCommand = new RelayCommand<string>(ExecuteChooseCodigoClienteCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionNombreModelo = FirstConnectionRecordOrDefault<FkModeloViewModel>("Modelo", LibSearchCriteria.CreateCriteria("Saw.Gv_Modelo_B1.Nombre", NombreModelo));
            LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
            LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("CodigoColor", CodigoColor);
            vDefaultCriteria.Add(vFixedCriteria, eLogicOperatorType.And);
            ConexionCodigoColor = FirstConnectionRecordOrDefault<FkColorViewModel>("Color", vDefaultCriteria);
            vDefaultCriteria = LibSearchCriteria.CreateCriteria("Codigo", CodigoCliente);
            vDefaultCriteria.Add(vFixedCriteria, eLogicOperatorType.And);
            ConexionCodigoCliente = FirstConnectionRecordOrDefault<FkClienteViewModel>("Cliente", vDefaultCriteria);
        }

        private void ExecuteChooseNombreModeloCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Saw.Gv_Modelo_B1.Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = null;
                ConexionNombreModelo = null;
                ConexionNombreModelo = ChooseRecord<FkModeloViewModel>("Modelo", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCodigoColorCommand(string valCodigoColor) {
            try {
                if (valCodigoColor == null) {
                    valCodigoColor = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("CodigoColor", valCodigoColor);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCodigoColor = null;
                ConexionCodigoColor = ChooseRecord<FkColorViewModel>("Color", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCodigoClienteCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCodigoCliente = null;
                ConexionCodigoCliente = ChooseRecord<FkClienteViewModel>("Cliente", vDefaultCriteria, vFixedCriteria, string.Empty);                
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        #endregion //Metodos Generados


    } //End of class VehiculoViewModel

} //End of namespace Galac.Saw.Uil.Vehiculo

