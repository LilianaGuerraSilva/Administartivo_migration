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
using Galac.Saw.Lib;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class BancosViewModel : LibInputViewModelMfc<BancosStt> {
        #region Constantes
        public const string RedondeaMontoCreditoBancarioPropertyName = "RedondeaMontoCreditoBancario";
        public const string RedondeaMontoDebitoBancarioPropertyName = "RedondeaMontoDebitoBancario";
        public const string UsaCodigoBancoEnPantallaPropertyName = "UsaCodigoBancoEnPantalla";
        public const string ManejaCreditoBancarioPropertyName = "ManejaCreditoBancario";
        public const string ManejaDebitoBancarioPropertyName = "ManejaDebitoBancario";
        public const string CodigoGenericoCuentaBancariaPropertyName = "CodigoGenericoCuentaBancaria";
        public const string ConsideraConciliadosLosMovIngresadosAntesDeFechaPropertyName = "ConsideraConciliadosLosMovIngresadosAntesDeFecha";
        public const string ConceptoCreditoBancarioPropertyName = "ConceptoCreditoBancario";
        public const string ConceptoDebitoBancarioPropertyName = "ConceptoDebitoBancario";
        public const string FechaDeInicioConciliacionPropertyName = "FechaDeInicioConciliacion";
        public const string IsEnabledDatosDebitoBancarioPropertyName = "IsEnabledDatosDebitoBancario";
        
        #endregion
        #region Variables
        private FkCuentaBancariaViewModel _ConexionCodigoGenericoCuentaBancaria = null;
        private FkConceptoBancarioViewModel _ConexionConceptoDebitoBancario = null;
        private FkConceptoBancarioViewModel _ConexionConceptoCreditoBancario = null;
        bool mEsFacturadorBasico;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "7.1.- Bancos"; }
        }

        public bool  RedondeaMontoCreditoBancario {
            get {
                return Model.RedondeaMontoCreditoBancarioAsBool;
            }
            set {
                if (Model.RedondeaMontoCreditoBancarioAsBool != value) {
                    Model.RedondeaMontoCreditoBancarioAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(RedondeaMontoCreditoBancarioPropertyName);
                }
            }
        }

        public bool  RedondeaMontoDebitoBancario {
            get {
                return Model.RedondeaMontoDebitoBancarioAsBool;
            }
            set {
                if (Model.RedondeaMontoDebitoBancarioAsBool != value) {
                    Model.RedondeaMontoDebitoBancarioAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(RedondeaMontoDebitoBancarioPropertyName);
                }
            }
        }

        public bool  UsaCodigoBancoEnPantalla {
            get {
                return Model.UsaCodigoBancoEnPantallaAsBool;
            }
            set {
                if (Model.UsaCodigoBancoEnPantallaAsBool != value) {
                    Model.UsaCodigoBancoEnPantallaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaCodigoBancoEnPantallaPropertyName);
                }
            }
        }

        public bool  ManejaCreditoBancario {
            get {
                return Model.ManejaCreditoBancarioAsBool;
            }
            set {
                if (Model.ManejaCreditoBancarioAsBool != value) {
                    Model.ManejaCreditoBancarioAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ManejaCreditoBancarioPropertyName);
                }
            }
        }

        public bool  ManejaDebitoBancario {
            get {
                return Model.ManejaDebitoBancarioAsBool;
            }
            set {
                if (Model.ManejaDebitoBancarioAsBool != value) {
                    Model.ManejaDebitoBancarioAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ManejaDebitoBancarioPropertyName);
                    RaisePropertyChanged(IsEnabledDatosDebitoBancarioPropertyName);
                    
                }
            }
        }
        [LibCustomValidation("CodigoGenericoCuentaBancariaValidating")]
        public string  CodigoGenericoCuentaBancaria {
            get {
                return Model.CodigoGenericoCuentaBancaria;
            }
            set {
                if (Model.CodigoGenericoCuentaBancaria != value) {
                    Model.CodigoGenericoCuentaBancaria = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoGenericoCuentaBancariaPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoGenericoCuentaBancaria, true)) {
                        ConexionCodigoGenericoCuentaBancaria = null;
                    }
                }
            }
        }

        public bool  ConsideraConciliadosLosMovIngresadosAntesDeFecha {
            get {
                return Model.ConsideraConciliadosLosMovIngresadosAntesDeFechaAsBool;
            }
            set {
                if (Model.ConsideraConciliadosLosMovIngresadosAntesDeFechaAsBool != value) {
                    Model.ConsideraConciliadosLosMovIngresadosAntesDeFechaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConsideraConciliadosLosMovIngresadosAntesDeFechaPropertyName);
                }
            }
        }
        
        public string  ConceptoCreditoBancario {
            get {
                return Model.ConceptoCreditoBancario;
            }
            set {
                if (Model.ConceptoCreditoBancario != value) {
                    Model.ConceptoCreditoBancario = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConceptoCreditoBancarioPropertyName);
                }
            }
        }

        [LibCustomValidation("ConceptoDebitoBancarioValidating")]
        public string  ConceptoDebitoBancario {
            get {
                return Model.ConceptoDebitoBancario;
            }
            set {
                if (Model.ConceptoDebitoBancario != value) {
                    Model.ConceptoDebitoBancario = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConceptoDebitoBancarioPropertyName);
                    if (LibString.IsNullOrEmpty(ConceptoDebitoBancario, true)) {
                        ConexionConceptoDebitoBancario = null;
                    }
                }
            }
        }

        [LibCustomValidation("FechaDeInicioConciliacionValidating")]
        public DateTime  FechaDeInicioConciliacion {
            get {
                return Model.FechaDeInicioConciliacion;
            }
            set {
                if (Model.FechaDeInicioConciliacion != value) {
                    Model.FechaDeInicioConciliacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaDeInicioConciliacionPropertyName);
                }
            }
        }
        public FkCuentaBancariaViewModel ConexionCodigoGenericoCuentaBancaria {
            get {
                return _ConexionCodigoGenericoCuentaBancaria;
            }
            set {
                if (_ConexionCodigoGenericoCuentaBancaria != value) {
                    _ConexionCodigoGenericoCuentaBancaria = value;
                    if(_ConexionCodigoGenericoCuentaBancaria != null) {
                        CodigoGenericoCuentaBancaria = _ConexionCodigoGenericoCuentaBancaria.Codigo;
                    }
                }
                if (_ConexionCodigoGenericoCuentaBancaria == null) {
                    CodigoGenericoCuentaBancaria = string.Empty;
                }
            }
        }

        public FkConceptoBancarioViewModel ConexionConceptoDebitoBancario {
            get {
                return _ConexionConceptoDebitoBancario;
            }
            set {
                if (_ConexionConceptoDebitoBancario != value) {
                    _ConexionConceptoDebitoBancario = value;
                    if(_ConexionConceptoDebitoBancario != null) {
                        ConceptoDebitoBancario = _ConexionConceptoDebitoBancario.Codigo;
                    }
                }
                if (_ConexionConceptoDebitoBancario == null) {
                    ConceptoDebitoBancario = string.Empty;
                }
            }
        }

        public FkConceptoBancarioViewModel ConexionConceptoCreditoBancario {
            get {
                return _ConexionConceptoCreditoBancario;
            }
            set {
                if(_ConexionConceptoCreditoBancario != value) {
                    _ConexionConceptoCreditoBancario = value;
                    if(_ConexionConceptoCreditoBancario != null) {
                        ConceptoCreditoBancario = _ConexionConceptoCreditoBancario.Codigo;
                    }
                }
                if(_ConexionConceptoCreditoBancario == null) {
                    ConceptoCreditoBancario = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoGenericoCuentaBancariaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseConceptoDebitoBancarioCommand {
            get;
            private set;
        }
        
        public RelayCommand<string> ChooseConceptoCreditoBancarioCommand {
            get;
            private set;
        }

        public bool IsEnabledConsideraConciliadosLosMovIngresadosAntesDeFecha {
            get { 
                return IsEnabled && ((Ccl.SttDef.ISettValueByCompanyPdn) new Galac.Saw.Brl.SttDef.clsSettValueByCompanyNav()).SePuedeModificarParametrosDeConciliacion();
            }
        }

        public bool IsEnabledDatosDebitoBancario {
            get {
                return IsEnabled && ManejaDebitoBancario;
            }
        }

        #endregion //Propiedades
        #region Constructores
        public BancosViewModel()
            : this(new BancosStt(), eAccionSR.Insertar) {
        }
        public BancosViewModel(BancosStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = RedondeaMontoCreditoBancarioPropertyName;
            mEsFacturadorBasico = new clsLibSaw().EsVersionFacturadorBasico();
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(BancosStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override BancosStt FindCurrentRecord(BancosStt valModel) {
            if (valModel == null) {
                return new BancosStt();
            }
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInString("RedondeaMontoCreditoBancario", valModel.RedondeaMontoCreditoBancario, 0);
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "BancosGET", vParams.Get()).FirstOrDefault();
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<BancosStt>, IList<BancosStt>> GetBusinessComponent() {
            return null;
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoGenericoCuentaBancariaCommand = new RelayCommand<string>(ExecuteChooseCodigoGenericoCuentaBancariaCommand);
            ChooseConceptoDebitoBancarioCommand = new RelayCommand<string>(ExecuteChooseConceptoDebitoBancarioCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
            vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.EsCajaChica", LibConvert.BoolToSN(false)), eLogicOperatorType.And);
            vFixedCriteria.Add( LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.Codigo", CodigoGenericoCuentaBancaria), eLogicOperatorType.And);
            ConexionCodigoGenericoCuentaBancaria = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkCuentaBancariaViewModel>("Cuenta Bancaria", vFixedCriteria, new clsSettValueByCompanyNav());
            vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Egreso));
            vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Codigo", ConceptoDebitoBancario), eLogicOperatorType.And);
            ConexionConceptoDebitoBancario = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", vFixedCriteria, new clsSettValueByCompanyNav());
            vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Ingreso));
            vFixedCriteria.Add( LibSearchCriteria.CreateCriteria("Codigo", ConceptoCreditoBancario), eLogicOperatorType.And);
            ConexionConceptoCreditoBancario = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", vFixedCriteria, new clsSettValueByCompanyNav());
        }

        private void ExecuteChooseCodigoGenericoCuentaBancariaCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }

                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_CuentaBancaria_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.EsCajaChica", LibConvert.BoolToSN(false)), eLogicOperatorType.And);
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.EsCajaChica", LibConvert.BoolToSN(false)), eLogicOperatorType.And);
                ConexionCodigoGenericoCuentaBancaria = null;
                ConexionCodigoGenericoCuentaBancaria = LibFKRetrievalHelper.ChooseRecord<FkCuentaBancariaViewModel>("Cuenta Bancaria", vDefaultCriteria, vFixedCriteria, string.Empty);             
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseConceptoDebitoBancarioCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Egreso));
                ConexionConceptoDebitoBancario = null;
                ConexionConceptoDebitoBancario = LibFKRetrievalHelper.ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);                
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult FechaDeInicioConciliacionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDeInicioConciliacion, false, Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de Inicio Conciliación"));
                }
            }
            return vResult;
        }

        private void ExecuteChooseConceptoCreditoBancarioCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Ingreso));
                ConexionConceptoCreditoBancario = null;
                ConexionConceptoCreditoBancario = LibFKRetrievalHelper.ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);               
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        #endregion //Metodos Generados


        private ValidationResult ConceptoDebitoBancarioValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(ConceptoDebitoBancario , true)) {
                    vResult = new ValidationResult("El campo " + ModuleName + " -> Concepto Débito Bancario, es requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult CodigoGenericoCuentaBancariaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(CodigoGenericoCuentaBancaria,true)) {
                    vResult = new ValidationResult("El campo " + ModuleName + " -> Codigo Generico Cuenta Bancaria, es requerido.");                    
                }
            }
            return vResult;
        }

        public bool IsVisibleBancos {
            get {
                return !mEsFacturadorBasico;
            }
        }

        public bool IsVisibleBancosIGTF {
            get {
                return !mEsFacturadorBasico;
            }
        }

    } //End of class BancosViewModel

} //End of namespace Galac.Saw.Uil.SttDef

