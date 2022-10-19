using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Uil.Inventario.ViewModel {
    public class AlmacenViewModel : LibInputViewModelMfc<Almacen> {
        #region Constantes
        public const string ConsecutivoPropertyName = "Consecutivo";
        public const string CodigoPropertyName = "Codigo";
        public const string NombreAlmacenPropertyName = "NombreAlmacen";
        public const string TipoDeAlmacenPropertyName = "TipoDeAlmacen";
        public const string ConsecutivoClientePropertyName = "ConsecutivoCliente";
        public const string CodigoClientePropertyName = "CodigoCliente";
        public const string NombreClientePropertyName = "NombreCliente";
        public const string CodigoCcPropertyName = "CodigoCc";
        public const string DescripcionPropertyName = "Descripcion";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        #endregion
        #region Variables
        private FkClienteViewModel _ConexionCodigoCliente = null;
        private FkCentroDeCostosViewModel _ConexionCodigoCc = null;
        private string _CodigoCliente;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Almacén"; }
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

        [LibRequired(ErrorMessage = "El campo Código es requerido.")]
        [LibGridColum("Código", DbMemberPath = "Gv_Almacen_B1.Codigo")]
        public string  Codigo {
            get {
                return Model.Codigo;
            }
            set {
                if (Model.Codigo != value) {
                    Model.Codigo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nombre es requerido.")]
        [LibGridColum("Nombre Almacén", Width = 300)]
        public string  NombreAlmacen {
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

        [LibGridColum("Tipo de Almacén", eGridColumType.Enum, PrintingMemberPath = "TipoDeAlmacenStr")]
        public eTipoDeAlmacen  TipoDeAlmacen {
            get {
                return Model.TipoDeAlmacenAsEnum;
            }
            set {
                if (Model.TipoDeAlmacenAsEnum != value) {
                    Model.TipoDeAlmacenAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDeAlmacenPropertyName);
                    RaisePropertyChanged("IsVisibleCliente");
                }
            }
        }

        public int  ConsecutivoCliente {
            get {
                return Model.ConsecutivoCliente;
            }
            set {
                if (Model.ConsecutivoCliente != value) {
                    Model.ConsecutivoCliente = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConsecutivoClientePropertyName);
                }
            }
        }

        [LibCustomValidation("ValidaCliente")]
        public string  CodigoCliente {
            get {
                return _CodigoCliente;
            }
            set {
                if (_CodigoCliente != value) {
                    _CodigoCliente = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoClientePropertyName);
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
                }
            }
        }

        [LibCustomValidation("ValidaCECO")]
        public string  CodigoCc {
            get {
                return Model.CodigoCc;
            }
            set {
                if (Model.CodigoCc != value) {
                    Model.CodigoCc = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoCcPropertyName);
                }
            }
        }

        public string  Descripcion {
            get {
                return Model.Descripcion;
            }
            set {
                if (Model.Descripcion != value) {
                    Model.Descripcion = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionPropertyName);
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

        public eTipoDeAlmacen[] ArrayTipoDeAlmacen {
            get {
                return LibEnumHelper<eTipoDeAlmacen>.GetValuesInArray();
            }
        }

        public FkClienteViewModel ConexionCodigoCliente {
            get {
                return _ConexionCodigoCliente;
            }
            set {
                if (_ConexionCodigoCliente != value) {
                    _ConexionCodigoCliente = value;
                    RaisePropertyChanged(CodigoClientePropertyName);

                    if (_ConexionCodigoCliente != null) {
                        NombreCliente = _ConexionCodigoCliente.Nombre;
                        CodigoCliente = _ConexionCodigoCliente.Codigo;
                    }
                    if (_ConexionCodigoCliente == null) {
                        NombreCliente = string.Empty;
                        CodigoCliente = string.Empty;
                    }                    

                }
            }
        }

        public FkCentroDeCostosViewModel ConexionCodigoCc {
            get {
                return _ConexionCodigoCc;
            }
            set {
                if (_ConexionCodigoCc != value) {
                    _ConexionCodigoCc = value;
                    RaisePropertyChanged(CodigoCcPropertyName);
                }
            }
        }

        public RelayCommand<string> ChooseCodigoClienteCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoCcCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseDescripcionCommand {
            get;
            private set;
        }

        public bool IsEnabledCodigo {
            get {
                return Action == eAccionSR.Insertar;
            }
        }

        #endregion //Propiedades
        #region Constructores
        public AlmacenViewModel()
            : this(new Almacen(), eAccionSR.Insertar) {
        }
        public AlmacenViewModel(Almacen initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
                Model.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
                DefaultFocusedPropertyName = CodigoPropertyName;
           
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(Almacen valModel) {
            Model.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"); 
            base.InitializeLookAndFeel(valModel);
            ConexionCodigoCliente = null; 
        }

        protected override Almacen FindCurrentRecord(Almacen valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "AlmacenGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<Almacen>, IList<Almacen>> GetBusinessComponent() {
            return new clsAlmacenNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoClienteCommand = new RelayCommand<string>(ExecuteChooseCodigoClienteCommand);
            ChooseCodigoCcCommand = new RelayCommand<string>(ExecuteChooseCodigoCcCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();

            LibSearchCriteria vClienteCriteria = LibSearchCriteria.CreateCriteria("Consecutivo", ConsecutivoCliente);
            vClienteCriteria.Add("ConsecutivoCompania", Mfc.GetInt("Compania"));
            ConexionCodigoCliente = FirstConnectionRecordOrDefault<FkClienteViewModel>("Cliente", vClienteCriteria);
            ConexionCodigoCc = FirstConnectionRecordOrDefault<FkCentroDeCostosViewModel>("Centro De Costos", LibSearchCriteria.CreateCriteria("Codigo", CodigoCc));
        }

        private void ExecuteChooseCodigoClienteCommand(string valcodigo) {
            try {
                if (LibString.IsNullOrEmpty(valcodigo, true)) {
                    valcodigo = string.Empty;
                }
                ConsecutivoCliente = 0;
                CodigoCliente = string.Empty;
                NombreCliente = string.Empty;

                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("codigo", valcodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCodigoCliente = ChooseRecord<FkClienteViewModel>("Cliente", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoCliente != null) {
                    ConsecutivoCliente = ConexionCodigoCliente.Consecutivo;
                    CodigoCliente = ConexionCodigoCliente.Codigo;
                    NombreCliente = ConexionCodigoCliente.Nombre;
                }
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCodigoCcCommand(string valCodigo) {
            try {
                if (LibString.IsNullOrEmpty(valCodigo,true)) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoPeriodo", Mfc.GetInt("Periodo"));
                ConexionCodigoCc = ChooseRecord<FkCentroDeCostosViewModel>("Centro De Costos", vDefaultCriteria, vFixedCriteria, "");
                if (ConexionCodigoCc != null) {
                    CodigoCc = ConexionCodigoCc.Codigo;
                    Descripcion = ConexionCodigoCc.Descripcion;
                } else {
                    CodigoCc = "";
                    Descripcion = "";
                }
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        public bool IsVisibleCeco {
            get {
                if (LibString.IsNullOrEmpty(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("RecordName", "AsociarCentroDeCostos")) || LibString.IsNullOrEmpty(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("RecordName", "UsaCentroDeCostos"))) {
                    return false;
                }
                return (eFormaDeAsociarCentroDeCostos)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("RecordName", "AsociarCentroDeCostos") == eFormaDeAsociarCentroDeCostos.PorAlmacen &&
                LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("RecordName", "UsaCentroDeCostos");
            }
        }
        
        private ValidationResult ValidaCECO() {
            ValidationResult vResult = ValidationResult.Success;
            if (Action == eAccionSR.Consultar || Action == eAccionSR.Eliminar) {
                return ValidationResult.Success;
            } else {
                if (Model.CodigoCc.Length == 0 && this.IsVisibleCeco) {
                    vResult = new ValidationResult("Debe seleccionar un Centro de Costos");
                }
            }
            return vResult;
        }

        public bool IsVisibleCliente {
            get {
                return TipoDeAlmacen == eTipoDeAlmacen.Consignacion;

            }
        }

        private ValidationResult ValidaCliente() {
            ValidationResult vResult = ValidationResult.Success;
            if (Action == eAccionSR.Consultar || Action == eAccionSR.Eliminar) {
                return ValidationResult.Success;
            } else {
                if (TipoDeAlmacen == eTipoDeAlmacen.Consignacion && LibString.IsNullOrEmpty(CodigoCliente)) {
                    vResult = new ValidationResult("Debe seleccionar un Cliente");
                }
            }
            return vResult;
        }
            


        #endregion //Metodos Generados


    } //End of class AlmacenViewModel

} //End of namespace Galac.Saw.Uil.Inventario