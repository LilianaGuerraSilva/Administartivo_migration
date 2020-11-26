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


namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class BancosAnticipoViewModel : LibInputViewModelMfc<AnticipoStt> {
        #region Constantes
        public const string SugerirConsecutivoAnticipoPropertyName = "SugerirConsecutivoAnticipo";
        public const string TipoComprobanteDeAnticipoAImprimirPropertyName = "TipoComprobanteDeAnticipoAImprimir";
        public const string NombrePlantillaReciboDeAnticipoCobradoPropertyName = "NombrePlantillaReciboDeAnticipoCobrado";
        public const string NombrePlantillaReciboDeAnticipoPagadoPropertyName = "NombrePlantillaReciboDeAnticipoPagado";
        public const string ConceptoBancarioAnticipoCobradoPropertyName = "ConceptoBancarioAnticipoCobrado";
        public const string ConceptoBancarioAnticipoPagadoPropertyName = "ConceptoBancarioAnticipoPagado";
        public const string ConceptoBancarioReversoAnticipoCobradoPropertyName = "ConceptoBancarioReversoAnticipoCobrado";
        public const string ConceptoBancarioReversoAnticipoPagadoPropertyName = "ConceptoBancarioReversoAnticipoPagado";
        public const string CuentaBancariaAnticipoPropertyName = "CuentaBancariaAnticipo";
        #endregion
        #region Variables
        private FkConceptoBancarioViewModel _ConexionConceptoBancarioAnticipoCobrado = null;
        private FkConceptoBancarioViewModel _ConexionConceptoBancarioAnticipoPagado = null;
        private FkConceptoBancarioViewModel _ConexionConceptoBancarioReversoAnticipoCobrado = null;
        private FkConceptoBancarioViewModel _ConexionConceptoBancarioReversoAnticipoPagado = null;
        private FkCuentaBancariaViewModel _ConexionCuentaBancariaAnticipo = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "7.3.- Anticipo"; }
        }

        public bool  SugerirConsecutivoAnticipo {
            get {
                return Model.SugerirConsecutivoAnticipoAsBool;
            }
            set {
                if (Model.SugerirConsecutivoAnticipoAsBool != value) {
                    Model.SugerirConsecutivoAnticipoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(SugerirConsecutivoAnticipoPropertyName);
                }
            }
        }

        public eComprobanteConCheque  TipoComprobanteDeAnticipoAImprimir {
            get {
                return Model.TipoComprobanteDeAnticipoAImprimirAsEnum;
            }
            set {
                if (Model.TipoComprobanteDeAnticipoAImprimirAsEnum != value) {
                    Model.TipoComprobanteDeAnticipoAImprimirAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoComprobanteDeAnticipoAImprimirPropertyName);
                }
            }
        }

        [LibCustomValidation("NombrePlantillaReciboDeAnticipoCobradoValidating")]
        public string  NombrePlantillaReciboDeAnticipoCobrado {
            get {
                return Model.NombrePlantillaReciboDeAnticipoCobrado;
            }
            set {
                if (Model.NombrePlantillaReciboDeAnticipoCobrado != value) {
                    Model.NombrePlantillaReciboDeAnticipoCobrado = value;
                    IsDirty = true;
                    if (LibString.IsNullOrEmpty(NombrePlantillaReciboDeAnticipoCobrado)) {
                        ExecuteBuscarPlantillaCommandAntCobrado();
                    }
                    RaisePropertyChanged(NombrePlantillaReciboDeAnticipoCobradoPropertyName);
                }
            }
        }

        [LibCustomValidation("NombrePlantillaReciboDeAnticipoPagadoValidating")]
        public string  NombrePlantillaReciboDeAnticipoPagado {
            get {
                return Model.NombrePlantillaReciboDeAnticipoPagado;
            }
            set {
                if (Model.NombrePlantillaReciboDeAnticipoPagado != value) {
                    Model.NombrePlantillaReciboDeAnticipoPagado = value;
                    IsDirty = true;
                    if (LibString.IsNullOrEmpty(NombrePlantillaReciboDeAnticipoPagado)) {
                        ExecuteBuscarPlantillaCommandAntPagado ();
                    }
                    RaisePropertyChanged(NombrePlantillaReciboDeAnticipoPagadoPropertyName);
                }
            }
        }

        [LibCustomValidation("ConceptoBancarioAnticipoCobradoValidating")]
        public string  ConceptoBancarioAnticipoCobrado {
            get {
                return Model.ConceptoBancarioAnticipoCobrado;
            }
            set {
                if (Model.ConceptoBancarioAnticipoCobrado != value) {
                    Model.ConceptoBancarioAnticipoCobrado = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConceptoBancarioAnticipoCobradoPropertyName);
                    if (LibString.IsNullOrEmpty(ConceptoBancarioAnticipoCobrado, true)) {
                        ConexionConceptoBancarioAnticipoCobrado = null;
                    }
                }
            }
        }

        [LibCustomValidation("ConceptoBancarioAnticipoPagadoValidating")]
        public string  ConceptoBancarioAnticipoPagado {
            get {
                return Model.ConceptoBancarioAnticipoPagado;
            }
            set {
                if (Model.ConceptoBancarioAnticipoPagado != value) {
                    Model.ConceptoBancarioAnticipoPagado = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConceptoBancarioAnticipoPagadoPropertyName);
                    if (LibString.IsNullOrEmpty(ConceptoBancarioAnticipoPagado, true)) {
                        ConexionConceptoBancarioAnticipoPagado = null;
                    }
                }
            }
        }

        [LibCustomValidation("ConceptoBancarioReversoAnticipoCobradoValidating")]
        public string  ConceptoBancarioReversoAnticipoCobrado {
            get {
                return Model.ConceptoBancarioReversoAnticipoCobrado;
            }
            set {
                if (Model.ConceptoBancarioReversoAnticipoCobrado != value) {
                    Model.ConceptoBancarioReversoAnticipoCobrado = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConceptoBancarioReversoAnticipoCobradoPropertyName);
                    if (LibString.IsNullOrEmpty(ConceptoBancarioReversoAnticipoCobrado, true)) {
                        ConexionConceptoBancarioReversoAnticipoCobrado = null;
                    }
                }
            }
        }

        [LibCustomValidation("ConceptoBancarioReversoAnticipoPagadoValidating")]
        public string  ConceptoBancarioReversoAnticipoPagado {
            get {
                return Model.ConceptoBancarioReversoAnticipoPagado;
            }
            set {
                if (Model.ConceptoBancarioReversoAnticipoPagado != value) {
                    Model.ConceptoBancarioReversoAnticipoPagado = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConceptoBancarioReversoAnticipoPagadoPropertyName);
                    if (LibString.IsNullOrEmpty(ConceptoBancarioReversoAnticipoPagado, true)) {
                        ConexionConceptoBancarioReversoAnticipoPagado = null;
                    }
                }
            }
        }

        [LibCustomValidation("CuentaBancariaAnticipoValidating")]
        public string  CuentaBancariaAnticipo {
            get {
                return Model.CuentaBancariaAnticipo;
            }
            set {
                if (Model.CuentaBancariaAnticipo != value) {
                    Model.CuentaBancariaAnticipo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaBancariaAnticipoPropertyName);
                    if (LibString.IsNullOrEmpty(CuentaBancariaAnticipo, true)) {
                        ConexionCuentaBancariaAnticipo = null;
                    }
                }
            }
        }

        public eComprobanteConCheque[] ArrayComprobanteConCheque {
            get {
                return LibEnumHelper<eComprobanteConCheque>.GetValuesInArray();
            }
        }

        public FkConceptoBancarioViewModel ConexionConceptoBancarioAnticipoCobrado {
            get {
                return _ConexionConceptoBancarioAnticipoCobrado;
            }
            set {
                if (_ConexionConceptoBancarioAnticipoCobrado != value) {
                    _ConexionConceptoBancarioAnticipoCobrado = value;
                    if(_ConexionConceptoBancarioAnticipoCobrado != null) {
                        ConceptoBancarioAnticipoCobrado = _ConexionConceptoBancarioAnticipoCobrado.Codigo;
                    }
                }
                if (_ConexionConceptoBancarioAnticipoCobrado == null) {
                    ConceptoBancarioAnticipoCobrado = string.Empty;
                }
            }
        }

        public FkConceptoBancarioViewModel ConexionConceptoBancarioAnticipoPagado {
            get {
                return _ConexionConceptoBancarioAnticipoPagado;
            }
            set {
                if (_ConexionConceptoBancarioAnticipoPagado != value) {
                    _ConexionConceptoBancarioAnticipoPagado = value;
                    if(_ConexionConceptoBancarioAnticipoPagado != null) {
                        ConceptoBancarioAnticipoPagado = _ConexionConceptoBancarioAnticipoPagado.Codigo;
                    }
                }
                if (_ConexionConceptoBancarioAnticipoPagado == null) {
                    ConceptoBancarioAnticipoPagado = string.Empty;
                }
            }
        }

        public FkConceptoBancarioViewModel ConexionConceptoBancarioReversoAnticipoCobrado {
            get {
                return _ConexionConceptoBancarioReversoAnticipoCobrado;
            }
            set {
                if (_ConexionConceptoBancarioReversoAnticipoCobrado != value) {
                    _ConexionConceptoBancarioReversoAnticipoCobrado = value;
                    if(_ConexionConceptoBancarioReversoAnticipoCobrado != null) {
                        ConceptoBancarioReversoAnticipoCobrado = _ConexionConceptoBancarioReversoAnticipoCobrado.Codigo;
                    }
                }
                if (_ConexionConceptoBancarioReversoAnticipoCobrado == null) {
                    ConceptoBancarioReversoAnticipoCobrado = string.Empty;
                }
            }
        }

        public FkConceptoBancarioViewModel ConexionConceptoBancarioReversoAnticipoPagado {
            get {
                return _ConexionConceptoBancarioReversoAnticipoPagado;
            }
            set {
                if (_ConexionConceptoBancarioReversoAnticipoPagado != value) {
                    _ConexionConceptoBancarioReversoAnticipoPagado = value;
                    if(_ConexionConceptoBancarioReversoAnticipoPagado != null) {
                        ConceptoBancarioReversoAnticipoPagado = _ConexionConceptoBancarioReversoAnticipoPagado.Codigo;
                    }
                }
                if (_ConexionConceptoBancarioReversoAnticipoPagado == null) {
                    ConceptoBancarioReversoAnticipoPagado = string.Empty;
                }
            }
        }

        public FkCuentaBancariaViewModel ConexionCuentaBancariaAnticipo {
            get {
                return _ConexionCuentaBancariaAnticipo;
            }
            set {
                if (_ConexionCuentaBancariaAnticipo != value) {
                    _ConexionCuentaBancariaAnticipo = value;
                    if(_ConexionCuentaBancariaAnticipo != null) {
                        CuentaBancariaAnticipo = _ConexionCuentaBancariaAnticipo.Codigo;
                    }
                }
                if (_ConexionCuentaBancariaAnticipo == null) {
                    CuentaBancariaAnticipo = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseConceptoBancarioAnticipoCobradoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseConceptoBancarioAnticipoPagadoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseConceptoBancarioReversoAnticipoCobradoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseConceptoBancarioReversoAnticipoPagadoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaBancariaAnticipoCommand {
            get;
            private set;
        }
        public RelayCommand ChooseTemplateCommandAntCobrado {
            get;
            private set;
        }

        public RelayCommand ChooseTemplateCommandAntPagado {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public BancosAnticipoViewModel()
            : this(new AnticipoStt(), eAccionSR.Insertar) {
        }
        public BancosAnticipoViewModel(AnticipoStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = SugerirConsecutivoAnticipoPropertyName;
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseTemplateCommandAntCobrado = new RelayCommand(ExecuteBuscarPlantillaCommandAntCobrado);
            ChooseTemplateCommandAntPagado = new RelayCommand(ExecuteBuscarPlantillaCommandAntPagado);
            ChooseConceptoBancarioAnticipoCobradoCommand = new RelayCommand<string>(ExecuteChooseConceptoBancarioAnticipoCobradoCommand);
            ChooseConceptoBancarioAnticipoPagadoCommand = new RelayCommand<string>(ExecuteChooseConceptoBancarioAnticipoPagadoCommand);
            ChooseConceptoBancarioReversoAnticipoCobradoCommand = new RelayCommand<string>(ExecuteChooseConceptoBancarioReversoAnticipoCobradoCommand);
            ChooseConceptoBancarioReversoAnticipoPagadoCommand = new RelayCommand<string>(ExecuteChooseConceptoBancarioReversoAnticipoPagadoCommand);
            ChooseCuentaBancariaAnticipoCommand = new RelayCommand<string>(ExecuteChooseCuentaBancariaAnticipoCommand);
        }
   
        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Ingreso));
            vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Codigo", ConceptoBancarioAnticipoCobrado), eLogicOperatorType.And);
            ConexionConceptoBancarioAnticipoCobrado = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", vFixedCriteria, new clsSettValueByCompanyNav());

            vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Egreso));
            vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Codigo", ConceptoBancarioAnticipoPagado), eLogicOperatorType.And);
            ConexionConceptoBancarioAnticipoPagado = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", vFixedCriteria, new clsSettValueByCompanyNav());

            vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Egreso));
            vFixedCriteria.Add( LibSearchCriteria.CreateCriteria("Codigo", ConceptoBancarioReversoAnticipoCobrado), eLogicOperatorType.And);
            ConexionConceptoBancarioReversoAnticipoCobrado = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", vFixedCriteria, new clsSettValueByCompanyNav());

            vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Ingreso));
            vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Codigo", ConceptoBancarioReversoAnticipoPagado), eLogicOperatorType.And);
            ConexionConceptoBancarioReversoAnticipoPagado = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", vFixedCriteria, new clsSettValueByCompanyNav());

            vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
            vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.EsCajaChica", LibConvert.BoolToSN(false)), eLogicOperatorType.And);            
            vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.Codigo", CuentaBancariaAnticipo), eLogicOperatorType.And);
            ConexionCuentaBancariaAnticipo = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkCuentaBancariaViewModel>("Cuenta Bancaria", vFixedCriteria, new clsSettValueByCompanyNav());
        }

        protected override void InitializeLookAndFeel(AnticipoStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override AnticipoStt FindCurrentRecord(AnticipoStt valModel) {
            if (valModel == null) {
                return new AnticipoStt();
            }
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInString("SugerirConsecutivoAnticipo", valModel.SugerirConsecutivoAnticipo, 0);
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "AnticipoSttGET", vParams.Get()).FirstOrDefault();
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<AnticipoStt>, IList<AnticipoStt>> GetBusinessComponent() {
            return null;
        }

        private void ExecuteBuscarPlantillaCommandAntCobrado() {
            try {
                NombrePlantillaReciboDeAnticipoCobrado = new clsUtilParameters().BuscarNombrePlantilla("rpx de Anticipo Cobrado (*.rpx)|*Comprobante*Anticipo*Cobrado*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteBuscarPlantillaCommandAntPagado() {
            try {
                NombrePlantillaReciboDeAnticipoPagado = new clsUtilParameters().BuscarNombrePlantilla("rpx de Anticipo Pagado (*.rpx)|*Comprobante*Anticipo*Pagado*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseConceptoBancarioAnticipoCobradoCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Adm.Gv_ConceptoBancario_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Ingreso));
                ConexionConceptoBancarioAnticipoCobrado = null;
                ConexionConceptoBancarioAnticipoCobrado = LibFKRetrievalHelper.ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);               
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseConceptoBancarioAnticipoPagadoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Egreso));
                ConexionConceptoBancarioAnticipoPagado = null;
                ConexionConceptoBancarioAnticipoPagado = LibFKRetrievalHelper.ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseConceptoBancarioReversoAnticipoCobradoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Egreso));
                ConexionConceptoBancarioReversoAnticipoCobrado = null;
                ConexionConceptoBancarioReversoAnticipoCobrado = LibFKRetrievalHelper.ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseConceptoBancarioReversoAnticipoPagadoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Ingreso));
                ConexionConceptoBancarioReversoAnticipoPagado = null;
                ConexionConceptoBancarioReversoAnticipoPagado = LibFKRetrievalHelper.ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCuentaBancariaAnticipoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_CuentaBancaria_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.EsCajaChica", LibConvert.BoolToSN(false)), eLogicOperatorType.And);
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.EsCajaChica", LibConvert.BoolToSN(false)), eLogicOperatorType.And);
                ConexionCuentaBancariaAnticipo = null;
                ConexionCuentaBancariaAnticipo = LibFKRetrievalHelper.ChooseRecord<FkCuentaBancariaViewModel>("Cuenta Bancaria", vDefaultCriteria, vFixedCriteria, string.Empty);               
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        #endregion //Metodos Generados

        private ValidationResult NombrePlantillaReciboDeAnticipoCobradoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(NombrePlantillaReciboDeAnticipoCobrado)) {
                    vResult = new ValidationResult("El campo  planilla de impresión Anticipo cobrado, en " + this.ModuleName + ", es requerido.");
                }else if (!LibString.IsNullOrEmpty(NombrePlantillaReciboDeAnticipoCobrado) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaReciboDeAnticipoCobrado)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaReciboDeAnticipoCobrado + ", en " + this.ModuleName + ", no EXISTE.");
                }
            }
            return vResult;
        }

        private ValidationResult NombrePlantillaReciboDeAnticipoPagadoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(NombrePlantillaReciboDeAnticipoPagado)) {
                    vResult = new ValidationResult("El campo  planilla de impresión Anticipo Pagado, en " + this.ModuleName + ", es requerido.");
                }else if (!LibString.IsNullOrEmpty(NombrePlantillaReciboDeAnticipoPagado) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaReciboDeAnticipoPagado)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaReciboDeAnticipoPagado + ", en " + this.ModuleName + ", no EXISTE.");
                }
            }
            return vResult;
        }

        private ValidationResult ConceptoBancarioAnticipoCobradoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(ConceptoBancarioAnticipoCobrado)) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> Concepto Bancario Anticipo Cobrado, es requerido.");
                }
                return vResult;
            }
        }


        private ValidationResult ConceptoBancarioAnticipoPagadoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(ConceptoBancarioAnticipoPagado)) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> Concepto Bancario Anticipo Pagado, es requerido.");
                }
                return vResult;
            }
        }


        private ValidationResult ConceptoBancarioReversoAnticipoCobradoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(ConceptoBancarioReversoAnticipoCobrado)) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> Concepto Bancario Reverso Anticipo Cobrado, es requerido.");
                }
                return vResult;
            }
        }


        private ValidationResult ConceptoBancarioReversoAnticipoPagadoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(ConceptoBancarioReversoAnticipoPagado)) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> Concepto Bancario Reverso Anticipo Pagado, es requerido.");
                }
                return vResult;
            }
        }


        private ValidationResult CuentaBancariaAnticipoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(CuentaBancariaAnticipo)) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> Cuenta Bancaria Anticipo, es requerido.");
                }
                return vResult;
            }
        }






    } //End of class AnticipoSttViewModel

} //End of namespace Galac.Saw.Uil.SttDef

