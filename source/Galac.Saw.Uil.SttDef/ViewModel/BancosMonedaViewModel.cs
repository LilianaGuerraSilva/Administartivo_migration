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
using Galac.Saw.Brl.SttDef;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Uil;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Brl.TablasGen;
using System.Text;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class BancosMonedaViewModel:LibInputViewModelMfc<MonedaStt> {

        #region Constantes

        private const string CodigoMonedaExtranjeraPropertyName = "CodigoMonedaExtranjera";
        private const string CodigoMonedaLocalPropertyName = "CodigoMonedaLocal";
        private const string NombreMonedaExtranjeraPropertyName = "NombreMonedaExtranjera";
        private const string NombreMonedaLocalPropertyName = "NombreMonedaLocal";
        private const string UsaMonedaExtranjeraPropertyName = "UsaMonedaExtranjera";
        private const string SolicitarIngresoDeTasaDeCambioAlEmitirPropertyName = "SolicitarIngresoDeTasaDeCambioAlEmitir";
        private const string IsEnabledDatosMonedaExtrangeraPropertyName = "IsEnabledDatosMonedaExtrangera";
        private const string UsaDivisaComoMonedaPrincipalDeIngresoDeDatosPropertyName = "UsaDivisaComoMonedaPrincipalDeIngresoDeDatos";
        private const string UsarLimiteMaximoParaIngresoDeTasaDeCambioPropertyName = "UsarLimiteMaximoParaIngresoDeTasaDeCambio";
        private const string MaximoLimitePermitidoParaLaTasaDeCambioPropertyName = "MaximoLimitePermitidoParaLaTasaDeCambio";
        private const string IsEnebaledMaximoLimitePermitidoParaLaTasaDeCambioPropertyName = "IsEnebaledMaximoLimitePermitidoParaLaTasaDeCambio";
        private const string IsEnebaledUsarLimiteMaximoParaIngresoDeTasaDeCambioPropertyName = "IsEnebaledUsarLimiteMaximoParaIngresoDeTasaDeCambio";        
        #endregion

        #region Variables

        private FkMonedaViewModel _ConexionNombreMonedaExtranjera = null;
        private FkMonedaViewModel _ConexionNombreMonedaLocal = null;
        private IMonedaLocalActual vMonedaLocal = null;
        private ParametersViewModel _ParametrosViewModel;

        #endregion //Variables

        #region Propiedades

        public override string ModuleName {
            get { return "7.2-Moneda"; }
        }

        [LibCustomValidation("CodigoMonedaExtranjeraValidating")]
        public string CodigoMonedaExtranjera {
            get {
                return Model.CodigoMonedaExtranjera;
            }
            set {
                if(Model.CodigoMonedaExtranjera != value) {
                    Model.CodigoMonedaExtranjera = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoMonedaExtranjeraPropertyName);
                }
            }
        }
        [LibCustomValidation("CodigoMonedaLocalValidating")]
        public string CodigoMonedaLocal {
            get {
                return Model.CodigoMonedaLocal;
            }
            set {
                if(Model.CodigoMonedaLocal != value) {
                    Model.CodigoMonedaLocal = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoMonedaLocalPropertyName);
                }
            }
        }

        public string NombreMonedaExtranjera {
            get {
                return Model.NombreMonedaExtranjera;
            }
            set {
                if(Model.NombreMonedaExtranjera != value) {
                    Model.NombreMonedaExtranjera = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreMonedaExtranjeraPropertyName);
                    if(LibString.IsNullOrEmpty(NombreMonedaExtranjera,true)) {
                        ConexionNombreMonedaExtranjera = null;
                    }
                }
            }
        }

        public string NombreMonedaLocal {
            get {
                return Model.NombreMonedaLocal;
            }
            set {
                if(Model.NombreMonedaLocal != value) {
                    Model.NombreMonedaLocal = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreMonedaLocalPropertyName);
                    if(LibString.IsNullOrEmpty(NombreMonedaLocal,true)) {
                        ConexionNombreMonedaLocal = null;
                    }
                }
            }
        }

        public bool UsaMonedaExtranjera {
            get {
                return Model.UsaMonedaExtranjeraAsBool;
            }
            set {
                if(Model.UsaMonedaExtranjeraAsBool != value) {
                    Model.UsaMonedaExtranjeraAsBool = value;
                    UsaDivisaComoMonedaPrincipalDeIngresoDeDatos = value == false ? false : UsaDivisaComoMonedaPrincipalDeIngresoDeDatos;
                    RaisePropertyChanged(UsaMonedaExtranjeraPropertyName);
                    RaisePropertyChanged(IsEnabledDatosMonedaExtrangeraPropertyName);
                    RaisePropertyChanged(IsEnebaledUsarLimiteMaximoParaIngresoDeTasaDeCambioPropertyName);
                    LibMessages.Notification.Send<bool>(Model.UsaMonedaExtranjeraAsBool,UsaMonedaExtranjeraPropertyName);
                }
            }
        }

        [LibCustomValidation("UsaDivisaComoMonedaPrincipalDeIngresoDeDatosValidating")]
        public bool UsaDivisaComoMonedaPrincipalDeIngresoDeDatos {
            get {
                return Model.UsaDivisaComoMonedaPrincipalDeIngresoDeDatosAsBool;
            }
            set {
                if(Model.UsaDivisaComoMonedaPrincipalDeIngresoDeDatosAsBool != value) {
                    Model.UsaDivisaComoMonedaPrincipalDeIngresoDeDatosAsBool = value;
                    RaisePropertyChanged(UsaDivisaComoMonedaPrincipalDeIngresoDeDatosPropertyName);
                }
            }
        }

        public eTipoDeSolicitudDeIngresoDeTasaDeCambio SolicitarIngresoDeTasaDeCambioAlEmitir {
            get {
                return Model.SolicitarIngresoDeTasaDeCambioAlEmitirAsEnum;
            }
            set {
                if(Model.SolicitarIngresoDeTasaDeCambioAlEmitirAsEnum != value) {
                    Model.SolicitarIngresoDeTasaDeCambioAlEmitirAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(SolicitarIngresoDeTasaDeCambioAlEmitirPropertyName);
                }
            }
        }

        public eTipoDeSolicitudDeIngresoDeTasaDeCambio[] ArrayTipoDeSolicitudDeIngresoDeTasaDeCambio {
            get {
                return LibEnumHelper<eTipoDeSolicitudDeIngresoDeTasaDeCambio>.GetValuesInArray();
            }
        }

        public bool UsarLimiteMaximoParaIngresoDeTasaDeCambio {
            get {
                return Model.UsarLimiteMaximoParaIngresoDeTasaDeCambio;
            }
            set {
                if(Model.UsarLimiteMaximoParaIngresoDeTasaDeCambio != value) {
                    Model.UsarLimiteMaximoParaIngresoDeTasaDeCambio = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsarLimiteMaximoParaIngresoDeTasaDeCambioPropertyName);
                    RaisePropertyChanged(IsEnebaledMaximoLimitePermitidoParaLaTasaDeCambioPropertyName);
                }
            }
        }

        [LibCustomValidation("MaximoLimitePermitidoParaLaTasaDeCambioValidating")]
        public decimal MaximoLimitePermitidoParaLaTasaDeCambio {
            get {
                return Model.MaximoLimitePermitidoParaLaTasaDeCambio;
            }
            set {
               if(Model.MaximoLimitePermitidoParaLaTasaDeCambio != value) {
                    Model.MaximoLimitePermitidoParaLaTasaDeCambio = value;
                    IsDirty = true;
                    RaisePropertyChanged(MaximoLimitePermitidoParaLaTasaDeCambioPropertyName);
                }
            }
        }

        public FkMonedaViewModel ConexionNombreMonedaExtranjera {
            get {
                return _ConexionNombreMonedaExtranjera;
            }
            set {
                if(_ConexionNombreMonedaExtranjera != value) {
                    _ConexionNombreMonedaExtranjera = value;
                    if(_ConexionNombreMonedaExtranjera != null) {
                        NombreMonedaExtranjera = _ConexionNombreMonedaExtranjera.Nombre;
                        CodigoMonedaExtranjera = _ConexionNombreMonedaExtranjera.Codigo;
                    } else if(_ConexionNombreMonedaExtranjera == null) {
                        NombreMonedaExtranjera = string.Empty;
                        CodigoMonedaExtranjera = string.Empty;
                    }
                }
            }
        }

        public FkMonedaViewModel ConexionNombreMonedaLocal {
            get {
                return _ConexionNombreMonedaLocal;
            }
            set {
                if(_ConexionNombreMonedaLocal != value) {
                    _ConexionNombreMonedaLocal = value;
                    if(_ConexionNombreMonedaLocal != null) {
                        NombreMonedaLocal = _ConexionNombreMonedaLocal.Nombre;
                        CodigoMonedaLocal = _ConexionNombreMonedaLocal.Codigo;
                    } else if(_ConexionNombreMonedaLocal == null) {
                        NombreMonedaLocal = string.Empty;
                        CodigoMonedaLocal = string.Empty;
                    }
                }
            }
        }

        public RelayCommand<string> ChooseNombreMonedaExtranjeraCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseNombreMonedaLocalCommand {
            get;
            private set;
        }

        public bool IsVisibleMonedaLocal {
            get {
                if(LibString.IsNullOrEmpty(AppMemoryInfo.GlobalValuesGetString("Parametros","SesionEspecialProgramador"))) {
                    return false;
                } else {
                    return AppMemoryInfo.GlobalValuesGetBool("Parametros","SesionEspecialProgramador");
                }
            }
        }

        public bool IsEnabledDatosMonedaExtrangera {
            get {
                return IsEnabled && UsaMonedaExtranjera;
            }
        }

        public bool IsVisibleDatosMonedaExtrangera {
            get {
                return !clsUtilParameters.EsSistemaParaIG();
            }
        }

        public bool IsEnebaledUsarLimiteMaximoParaIngresoDeTasaDeCambio {
            get {
                return IsEnabled && UsaMonedaExtranjera && AppMemoryInfo.GlobalValuesGetBool("Parametros","EsUsuarioSupervisor");
            }
        }

        public bool IsEnebaledMaximoLimitePermitidoParaLaTasaDeCambio {
            get {
                return IsEnabled && UsarLimiteMaximoParaIngresoDeTasaDeCambio && IsEnebaledUsarLimiteMaximoParaIngresoDeTasaDeCambio;
            }
        }

        public ParametersViewModel ParametrosViewModel {
            get { return _ParametrosViewModel; }
            set { _ParametrosViewModel = value; }
        }


        #endregion //Propiedades

        #region Constructores

        public BancosMonedaViewModel()
            : this(new MonedaStt(),eAccionSR.Insertar) {
        }
        public BancosMonedaViewModel(MonedaStt initModel,eAccionSR initAction)
            : base(initModel,initAction,LibGlobalValues.Instance.GetAppMemInfo(),LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = CodigoMonedaExtranjeraPropertyName;
            vMonedaLocal = new clsMonedaLocalActual();
            vMonedaLocal.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country,LibDate.Today());
        }

        #endregion //Constructores

        #region Metodos Generados

        protected override void InitializeLookAndFeel(MonedaStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override MonedaStt FindCurrentRecord(MonedaStt valModel) {
            if(valModel == null) {
                return new MonedaStt();
            }
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<MonedaStt>,IList<MonedaStt>> GetBusinessComponent() {
            return null;
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNombreMonedaExtranjeraCommand = new RelayCommand<string>(ExecuteChooseNombreMonedaExtranjeraCommand);
            ChooseNombreMonedaLocalCommand = new RelayCommand<string>(ExecuteChooseNombreMonedaLocalCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionNombreMonedaExtranjera = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda",LibSearchCriteria.CreateCriteria("Nombre",NombreMonedaExtranjera),new clsSettValueByCompanyNav());
            ConexionNombreMonedaLocal = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda",LibSearchCriteria.CreateCriteria("Nombre",NombreMonedaLocal),new clsSettValueByCompanyNav());
        }

        private void ExecuteChooseNombreMonedaExtranjeraCommand(string valNombre) {
            try {
                if(valNombre == null) {
                    valNombre = string.Empty;
                }
                XElement vXmlMonedaLocales = ((IMonedaLocalPdn)new clsMonedaLocalProcesos()).BusquedaTodasLasMonedasLocales(LibDefGen.ProgramInfo.Country);
                IList<MonedaLocalActual> vListaDeMonedaLocales = new List<MonedaLocalActual>();
                vListaDeMonedaLocales = vXmlMonedaLocales != null ? LibParserHelper.ParseToList<MonedaLocalActual>(new XDocument(vXmlMonedaLocales)) : null;
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Moneda_B1.Nombre",valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Activa",LibConvert.BoolToSN(true));
                vFixedCriteria.Add("TipoDeMoneda",eBooleanOperatorType.IdentityEquality,eTipoDeMoneda.Fisica);
                if(vListaDeMonedaLocales != null && !LibDefGen.ProgramInfo.IsCountryEcuador()) {
                    foreach(MonedaLocalActual vMoneda in vListaDeMonedaLocales) {
                        vFixedCriteria.Add("Codigo",eBooleanOperatorType.IdentityInequality,vMoneda.CodigoMoneda);
                    }
                }
                ConexionNombreMonedaExtranjera = null;
                ConexionNombreMonedaExtranjera = LibFKRetrievalHelper.ChooseRecord<FkMonedaViewModel>("Moneda",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        private void ExecuteChooseNombreMonedaLocalCommand(string valNombre) {
            try {
                if(valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Moneda_B1.Nombre",valNombre);
                LibSearchCriteria vFixedCriteria = null;
                ConexionNombreMonedaLocal = null;
                ConexionNombreMonedaLocal = LibFKRetrievalHelper.ChooseRecord<FkMonedaViewModel>("Moneda",vDefaultCriteria,vFixedCriteria,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        #endregion //Metodos Generados

        #region Validations

        private ValidationResult CodigoMonedaLocalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(LibString.IsNullOrEmpty(CodigoMonedaLocal)) {
                    vResult = new ValidationResult("El Campo " + ModuleName + "-> Nombre Moneda Local, es Requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult CodigoMonedaExtranjeraValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(LibString.IsNullOrEmpty(CodigoMonedaExtranjera)) {
                    vResult = new ValidationResult("El Campo " + ModuleName + "-> Nombre Moneda Extranjera, es Requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult UsaDivisaComoMonedaPrincipalDeIngresoDeDatosValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                StringBuilder vResponse = new StringBuilder();
                bool vRequirementsAreMissing = false;
                vResponse.AppendLine($"Para usar el parámetro {this.ModuleName} - Usar Divisa como Moneda Principal de Ingreso de Datos, debe ajustar los siguientes parámetros previamente:");
                vResponse.AppendLine();
                FacturaFacturacionContViewModel vFacturaFacturacionContViewModel = ParametrosViewModel.ModuleList[1].Groups[1].Content as FacturaFacturacionContViewModel;
                BancosAnticipoViewModel vBancosAnticipoViewModel = ParametrosViewModel.ModuleList[6].Groups[2].Content as BancosAnticipoViewModel;
                bool vUsaCobroDirecto = vFacturaFacturacionContViewModel.UsaCobroDirecto;
                bool vUsaCobroMultimoneda = vFacturaFacturacionContViewModel.UsaCobroDirectoEnMultimoneda;
                bool vUsaListaDePrecioEnMonedaExtranjera = vFacturaFacturacionContViewModel.UsaListaDePrecioEnMonedaExtranjera;
                bool vMostrarTotalEnDivisa = vFacturaFacturacionContViewModel.SeMuestraTotalEnDivisas;
                FkCuentaBancariaViewModel vCuentaBancariaGenericaAnticipo = vBancosAnticipoViewModel.ConexionCuentaBancariaAnticipo;
                if(vUsaCobroDirecto && !vUsaCobroMultimoneda) {
                    vResponse.AppendLine($"- Activar {vFacturaFacturacionContViewModel.ModuleName} - Usa Cobro Directo en Multimoneda.");
                    vRequirementsAreMissing = true;
                }
                if(!vUsaListaDePrecioEnMonedaExtranjera) {
                    vResponse.AppendLine($"- Activar {vFacturaFacturacionContViewModel.ModuleName} - Usar Lista de Precios en Moneda Extranjera.");
                    vRequirementsAreMissing = true;
                }
                if(!vMostrarTotalEnDivisa) {
                    vResponse.AppendLine($"- Activar {vFacturaFacturacionContViewModel.ModuleName} - Mostrar Total en Divisas.");
                    vRequirementsAreMissing = true;
                }
                if(vCuentaBancariaGenericaAnticipo.CodigoMoneda != CodigoMonedaExtranjera) {
                    vResponse.AppendLine($"- Asignar cuenta en moneda extranjera ({CodigoMonedaExtranjera} - {NombreMonedaExtranjera}) en {vBancosAnticipoViewModel.ModuleName} - Cuenta Bancaria Genérica.");
                    vRequirementsAreMissing = true;
                }
                if(UsaDivisaComoMonedaPrincipalDeIngresoDeDatos && vRequirementsAreMissing) {
                    vResult = new ValidationResult(vResponse.ToString());
                }
            }
            return vResult;
        }

        private ValidationResult MaximoLimitePermitidoParaLaTasaDeCambioValidating() {
            ValidationResult vResult = ValidationResult.Success;
            decimal vTop = 1m;
            if(MaximoLimitePermitidoParaLaTasaDeCambio < vTop) {
                vResult = new ValidationResult("El máximo limite permitido no puede ser menor a " + LibConvert.NumToString(vTop,2));
            }
            return vResult;
        }
        #endregion
    } //End of class BancosMonedaViewModel

} //End of namespace Galac.Saw.Uil.SttDef

