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
    public class BancosMovimientoBancarioViewModel : LibInputViewModelMfc<MovimientoBancarioStt> {
        #region Constantes
        public const string ConceptoBancarioReversoSolicitudDePagoPropertyName = "ConceptoBancarioReversoSolicitudDePago";
        public const string ConfirmarImpresionMovBancarioPorSeccionesPropertyName = "ConfirmarImpresionMovBancarioPorSecciones";
        public const string BeneficiarioGenericoPropertyName = "CodigoBeneficiarioGenerico";
        public const string ImprimirCompContDespuesDeChequeMovBancarioPropertyName = "ImprimirCompContDespuesDeChequeMovBancario";
        public const string GenerarMovBancarioDesdeCobroPropertyName = "GenerarMovBancarioDesdeCobro";
        public const string GenerarMovBancarioDesdePagoPropertyName = "GenerarMovBancarioDesdePago";
        public const string GenerarMovReversoSiAnulaPagoPropertyName = "GenerarMovReversoSiAnulaPago";
        public const string ImprimirComprobanteDeMovBancarioPropertyName = "ImprimirComprobanteDeMovBancario";
        public const string MandarMensajeNumeroDeMovimientoBancarioPropertyName = "MandarMensajeNumeroDeMovimientoBancario";
        public const string NombrePlantillaComprobanteDePagoSueldoPropertyName = "NombrePlantillaComprobanteDePagoSueldo";
        public const string NombrePlantillaComprobanteDeMovBancarioPropertyName = "NombrePlantillaComprobanteDeMovBancario";
        public const string NumCopiasComprobanteMovBancarioPropertyName = "NumCopiasComprobanteMovBancario";
        public const string UsaCodigoConceptoBancarioEnPantallaPropertyName = "UsaCodigoConceptoBancarioEnPantalla";
        public const string IsEnabledImprimirComprobanteDeMovBancarioPropertyName = "IsEnabledImprimirComprobanteDeMovBancario";
        
        #endregion
        #region Variables
        private FkConceptoBancarioViewModel _ConexionConceptoBancarioReversoSolicitudDePago = null;
        private FkBeneficiarioViewModel _ConexionBeneficiarioGenerico = null;
        private string _CodigoBeneficiarioGenerico = string.Empty;
        bool mEsFacturadorBasico;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "7.4.- Movimiento Bancario"; }
        }

        
        [LibCustomValidation("ConceptoBancarioReversoSolicitudDePagoValidating")]
        public string  ConceptoBancarioReversoSolicitudDePago {
            get {
                return Model.ConceptoBancarioReversoSolicitudDePago;
            }
            set {
                if (Model.ConceptoBancarioReversoSolicitudDePago != value) {
                    Model.ConceptoBancarioReversoSolicitudDePago = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConceptoBancarioReversoSolicitudDePagoPropertyName);
                    if (LibString.IsNullOrEmpty(ConceptoBancarioReversoSolicitudDePago, true)) {
                        ConexionConceptoBancarioReversoSolicitudDePago = null;
                    }
                }
            }
        }

        public bool  ConfirmarImpresionMovBancarioPorSecciones {
            get {
                return Model.ConfirmarImpresionMovBancarioPorSeccionesAsBool;
            }
            set {
                if (Model.ConfirmarImpresionMovBancarioPorSeccionesAsBool != value) {
                    Model.ConfirmarImpresionMovBancarioPorSeccionesAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConfirmarImpresionMovBancarioPorSeccionesPropertyName);
                }
            }
        }


        public int BeneficiarioGenerico {
            get {
                return Model.BeneficiarioGenerico;
            }
            set {
                if (Model.BeneficiarioGenerico != value) {
                    Model.BeneficiarioGenerico = value;
                    IsDirty = true;
                    RaisePropertyChanged(BeneficiarioGenericoPropertyName);
                    if(BeneficiarioGenerico == 0) {
                        ConexionBeneficiarioGenerico = null;
                    }
                }
            }
        }

        [LibCustomValidation("CodigoBeneficiarioGenericoValidating")]
        public string CodigoBeneficiarioGenerico {
            get {
                return _CodigoBeneficiarioGenerico;
            }
            set {
                if(_CodigoBeneficiarioGenerico != value) {
                    _CodigoBeneficiarioGenerico = value;
                    RaisePropertyChanged(BeneficiarioGenericoPropertyName);
                }
            }
        }

        public bool  ImprimirCompContDespuesDeChequeMovBancario {
            get {
                return Model.ImprimirCompContDespuesDeChequeMovBancarioAsBool;
            }
            set {
                if (Model.ImprimirCompContDespuesDeChequeMovBancarioAsBool != value) {
                    Model.ImprimirCompContDespuesDeChequeMovBancarioAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimirCompContDespuesDeChequeMovBancarioPropertyName);
                    RaisePropertyChanged(IsEnabledImprimirComprobanteDeMovBancarioPropertyName);                    
                }
            }
        }

        public bool  GenerarMovBancarioDesdeCobro {
            get {
                return Model.GenerarMovBancarioDesdeCobroAsBool;
            }
            set {
                if (Model.GenerarMovBancarioDesdeCobroAsBool != value) {
                    Model.GenerarMovBancarioDesdeCobroAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(GenerarMovBancarioDesdeCobroPropertyName);
                }
            }
        }

        public bool  GenerarMovBancarioDesdePago {
            get {
                return Model.GenerarMovBancarioDesdePagoAsBool;
            }
            set {
                if (Model.GenerarMovBancarioDesdePagoAsBool != value) {
                    Model.GenerarMovBancarioDesdePagoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(GenerarMovBancarioDesdePagoPropertyName);
                }
            }
        }

        public bool  GenerarMovReversoSiAnulaPago {
            get {
                return Model.GenerarMovReversoSiAnulaPagoAsBool;
            }
            set {
                if (Model.GenerarMovReversoSiAnulaPagoAsBool != value) {
                    Model.GenerarMovReversoSiAnulaPagoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(GenerarMovReversoSiAnulaPagoPropertyName);
                }
            }
        }

        public bool  ImprimirComprobanteDeMovBancario {
            get {
                return Model.ImprimirComprobanteDeMovBancarioAsBool;
            }
            set {
                if (Model.ImprimirComprobanteDeMovBancarioAsBool != value) {
                    Model.ImprimirComprobanteDeMovBancarioAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimirComprobanteDeMovBancarioPropertyName);
                }
            }
        }

        public bool  MandarMensajeNumeroDeMovimientoBancario {
            get {
                return Model.MandarMensajeNumeroDeMovimientoBancarioAsBool;
            }
            set {
                if (Model.MandarMensajeNumeroDeMovimientoBancarioAsBool != value) {
                    Model.MandarMensajeNumeroDeMovimientoBancarioAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(MandarMensajeNumeroDeMovimientoBancarioPropertyName);
                }
            }
        }

       [LibCustomValidation("NombrePlantillaComprobanteDePagoSueldoValidating")]
        public string  NombrePlantillaComprobanteDePagoSueldo {
            get {
                return Model.NombrePlantillaComprobanteDePagoSueldo;
            }
            set {
                if (Model.NombrePlantillaComprobanteDePagoSueldo != value) {
                    Model.NombrePlantillaComprobanteDePagoSueldo = value;
                    IsDirty = true;
                    if (LibString.IsNullOrEmpty(NombrePlantillaComprobanteDePagoSueldo)) {
                        ExecuteBuscarPlantillaCommandComprobanteSueldo ();
                    }
                    RaisePropertyChanged(NombrePlantillaComprobanteDePagoSueldoPropertyName);
                }
            }
        }
          
        [LibCustomValidation("NombrePlantillaComprobanteDeMovBancarioValidating")]
        public string  NombrePlantillaComprobanteDeMovBancario {
            get {
                return Model.NombrePlantillaComprobanteDeMovBancario;
            }
            set {
                if (Model.NombrePlantillaComprobanteDeMovBancario != value) {
                    Model.NombrePlantillaComprobanteDeMovBancario = value;
                    IsDirty = true;
                    if (LibString.IsNullOrEmpty(NombrePlantillaComprobanteDeMovBancario)) {
                        ExecuteBuscarPlantillaCommandMovBancario();
                    }
                    RaisePropertyChanged(NombrePlantillaComprobanteDeMovBancarioPropertyName);
                }
            }
        }

        public int  NumCopiasComprobanteMovBancario {
            get {
                return Model.NumCopiasComprobanteMovBancario;
            }
            set {
                if (Model.NumCopiasComprobanteMovBancario != value) {
                    Model.NumCopiasComprobanteMovBancario = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumCopiasComprobanteMovBancarioPropertyName);
                }
            }
        }

        public bool  UsaCodigoConceptoBancarioEnPantalla {
            get {
                return Model.UsaCodigoConceptoBancarioEnPantallaAsBool;
            }
            set {
                if (Model.UsaCodigoConceptoBancarioEnPantallaAsBool != value) {
                    Model.UsaCodigoConceptoBancarioEnPantallaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaCodigoConceptoBancarioEnPantallaPropertyName);
                }
            }
        }

        public FkConceptoBancarioViewModel ConexionConceptoBancarioReversoSolicitudDePago {
            get {
                return _ConexionConceptoBancarioReversoSolicitudDePago;
            }
            set {
                if (_ConexionConceptoBancarioReversoSolicitudDePago != value) {
                    _ConexionConceptoBancarioReversoSolicitudDePago = value;
                    if(_ConexionConceptoBancarioReversoSolicitudDePago != null) {
                        ConceptoBancarioReversoSolicitudDePago = _ConexionConceptoBancarioReversoSolicitudDePago.Codigo;
                    }
                }
                if (_ConexionConceptoBancarioReversoSolicitudDePago == null) {
                    ConceptoBancarioReversoSolicitudDePago = string.Empty;
                }
            }
        }
        public RelayCommand<string> ChooseConceptoBancarioReversoSolicitudDePagoCommand {
            get;
            private set;
        }
        public FkBeneficiarioViewModel ConexionBeneficiarioGenerico {
            get {
                return _ConexionBeneficiarioGenerico;
            }
            set {
                if (_ConexionBeneficiarioGenerico != value) {
                    _ConexionBeneficiarioGenerico = value;
                    if(_ConexionBeneficiarioGenerico != null) {
                       BeneficiarioGenerico = _ConexionBeneficiarioGenerico.Consecutivo;
                        CodigoBeneficiarioGenerico = _ConexionBeneficiarioGenerico.Codigo;
                    }
                }
                if (_ConexionBeneficiarioGenerico == null) {
                    BeneficiarioGenerico = 0;
                    CodigoBeneficiarioGenerico = string.Empty;
                }
            }
        }
        public RelayCommand ChooseTemplateCommandMovBancario {
            get;
            private set;
        }

        public RelayCommand ChooseTemplateCommandComprobanteSueldo {
            get;
            private set;
        }
        public RelayCommand<string> ChooseBeneficiarioGenericoCommand {
            get;
            private set;
        }
        public bool IsVisibleImprimirComprobanteDeMovBancario {
            get {
                if(LibString.IsNullOrEmpty(AppMemoryInfo.GlobalValuesGetString("Parametros", "CaracteristicaDeContabilidadActiva"))) {
                    return false;
                } else {
                    return AppMemoryInfo.GlobalValuesGetBool("Parametros", "CaracteristicaDeContabilidadActiva");
                }

            }
        }

        public bool IsVisibleMovReversoSiAnulaPago {
            get {
                return Action == eAccionSR.Insertar;
            }
        }

        public bool IsEnabledImprimirComprobanteDeMovBancario {
            get {
                return IsEnabled && ImprimirCompContDespuesDeChequeMovBancario;
            }
        }
        #endregion //Propiedades
        #region Constructores
        public BancosMovimientoBancarioViewModel()
            : this(new MovimientoBancarioStt(), eAccionSR.Insertar) {
        }
        public BancosMovimientoBancarioViewModel(MovimientoBancarioStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = ConceptoBancarioReversoSolicitudDePagoPropertyName;
            mEsFacturadorBasico = new clsLibSaw().EsFacturadorBasico();
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeCommands() {
            ChooseBeneficiarioGenericoCommand = new RelayCommand<string>(ExecuteChooseBeneficiarioGenericoCommand);
            ChooseTemplateCommandMovBancario = new RelayCommand(ExecuteBuscarPlantillaCommandMovBancario);
            ChooseTemplateCommandComprobanteSueldo = new RelayCommand(ExecuteBuscarPlantillaCommandComprobanteSueldo);
            ChooseConceptoBancarioReversoSolicitudDePagoCommand = new RelayCommand<string>(ExecuteChooseConceptoBancarioReversoSolicitudDePagoCommand);
        }

        protected override void InitializeLookAndFeel(MovimientoBancarioStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override MovimientoBancarioStt FindCurrentRecord(MovimientoBancarioStt valModel) {
            if (valModel == null) {
                return new MovimientoBancarioStt();
            }
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInString("ConceptoBancarioReversoSolicitudDePago", valModel.ConceptoBancarioReversoSolicitudDePago, 10);
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "MovimientoBancarioSttGET", vParams.Get()).FirstOrDefault();
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<MovimientoBancarioStt>, IList<MovimientoBancarioStt>> GetBusinessComponent() {
            return null;
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Ingreso));
            vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("codigo", ConceptoBancarioReversoSolicitudDePago), eLogicOperatorType.And);            
            ConexionConceptoBancarioReversoSolicitudDePago = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", vFixedCriteria, new clsSettValueByCompanyNav());

            vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
            vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Consecutivo", BeneficiarioGenerico), eLogicOperatorType.And);
            ConexionBeneficiarioGenerico = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkBeneficiarioViewModel>("Beneficiario", vFixedCriteria, new clsSettValueByCompanyNav());
        }

        private void ExecuteChooseConceptoBancarioReversoSolicitudDePagoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Ingreso));
                ConexionConceptoBancarioReversoSolicitudDePago = null;
                ConexionConceptoBancarioReversoSolicitudDePago = LibFKRetrievalHelper.ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);                
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteBuscarPlantillaCommandMovBancario() {
            try {
                NombrePlantillaComprobanteDeMovBancario = new clsUtilParameters().BuscarNombrePlantilla("rpx de Movimiento Bancario (*.rpx)|*Impresion*Cheque*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteBuscarPlantillaCommandComprobanteSueldo() {
            try {
                NombrePlantillaComprobanteDePagoSueldo = new clsUtilParameters().BuscarNombrePlantilla("rpx de Pago Sueldo (*.rpx)|*Pago*Sueldo*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseBeneficiarioGenericoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionBeneficiarioGenerico = null;
                ConexionBeneficiarioGenerico = LibFKRetrievalHelper.ChooseRecord<FkBeneficiarioViewModel>("Beneficiario", vDefaultCriteria, vFixedCriteria, string.Empty);                
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //Metodos Generados

        private ValidationResult NombrePlantillaComprobanteDePagoSueldoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(NombrePlantillaComprobanteDePagoSueldo)) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> Nombre Plantilla Comprobante de Pago Sueldo, es requerido.");
                } else if (!clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaComprobanteDePagoSueldo)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaComprobanteDePagoSueldo + ", en " + this.ModuleName + ", no EXISTE.");
                }
            }
            return vResult;
        }

        private ValidationResult NombrePlantillaComprobanteDeMovBancarioValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(NombrePlantillaComprobanteDeMovBancario)) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> Nombre Plantilla Comprobante de Mov. Bancario, es requerido.");
                } else if (!LibString.IsNullOrEmpty(NombrePlantillaComprobanteDeMovBancario) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaComprobanteDeMovBancario)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaComprobanteDeMovBancario + ", en " + this.ModuleName + ", no EXISTE.");
                }
            }
            return vResult;
        }

        private ValidationResult ConceptoBancarioReversoSolicitudDePagoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(ConceptoBancarioReversoSolicitudDePago)) {
                    vResult = new ValidationResult("El campo " + ModuleName + "->Concepto Bancario Reverso Solicitud De Pago, es requerido.");                
                }
            }
            return vResult;
        }

        private ValidationResult CodigoBeneficiarioGenericoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(CodigoBeneficiarioGenerico)) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> Codigo Beneficiario Genérico, es requerido.");                
                }
            }
            return vResult;
        }

        public bool IsVisibleMovimientoBancario {
            get {
                return !mEsFacturadorBasico;
            }
        }


    } //End of class MovimientoBancarioSttViewModel

} //End of namespace Galac.Saw.Uil.SttDef

