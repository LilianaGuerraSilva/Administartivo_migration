using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Saw.Brl.SttDef;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Uil;
using Galac.Saw.Reconv;
using System.Linq;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class FacturaFacturacionContViewModel : LibInputViewModelMfc<FacturacionContinuacionStt> {

        #region Constantes
        public const string UsarOtrosCargoDeFacturaPropertyName = "UsarOtrosCargoDeFactura";
        public const string UltimaFechaDeFacturacionHistoricaPropertyName = "UltimaFechaDeFacturacionHistorica";
        public const string UsaCamposExtrasEnRenglonFacturaPropertyName = "UsaCamposExtrasEnRenglonFactura";        
        public const string PermitirIncluirFacturacionHistoricaPropertyName = "PermitirIncluirFacturacionHistorica";
        public const string PermitirDobleDescuentoEnFacturaPropertyName = "PermitirDobleDescuentoEnFactura";
        public const string ForzarFechaFacturaAmesEspecificoPropertyName = "ForzarFechaFacturaAmesEspecifico";
        public const string GenerarCxCalEmitirUnaFacturaHistoricaPropertyName = "GenerarCxCalEmitirUnaFacturaHistorica";
        public const string MaximoDescuentoEnFacturaPropertyName = "MaximoDescuentoEnFactura";
        public const string MesFacturacionEnCursoPropertyName = "MesFacturacionEnCurso";
        public const string AccionAlAnularFactDeMesesAntPropertyName = "AccionAlAnularFactDeMesesAnt";        
        public const string IsEnabledMesFacturacionEnCursoPropertyName = "IsEnabledMesFacturacionEnCurso";        
        public const string BloquearEmisionPropertyName = "BloquearEmision";
        public const string IsEnabledUltimaFechaDeFacturacionHistoricaPropertyName = "IsEnabledUltimaFechaDeFacturacionHistorica";        
        public const string MostrarMtoTotalBsFEnObservacionesPropertyName = "MostrarMtoTotalBsFEnObservaciones";
        private const string SeMuestraTotalEnDivisasPropertyName = "SeMuestraTotalEnDivisas";
        public const string UsaListaDePrecioEnMonedaExtranjeraPropertyName = "UsaListaDePrecioEnMonedaExtranjera";
        public const string UsaListaDePrecioEnMonedaExtranjeraCXCPropertyName = "UsaListaDePrecioEnMonedaExtranjeraCXC";
        public const string NroDiasMantenerTasaCambioPropertyName = "NroDiasMantenerTasaCambio";
        private const string FechaInicioImprentaDigitalPropertyName = "FechaInicioImprentaDigital";        
        public const string ParametrosBancoMonedaPropertyName = "ParametrosBancoMoneda";
        public const string UsaMaquinaFiscalPropertyName = "UsaMaquinaFiscal";
        public const string IsEnabledUsaMaquinaFiscalPropertyName = "IsEnabledUsaMaquinaFiscal";        
        #endregion
        #region Variables        
        private DateTime _FechaInicioImprentaDigital;
        private ParametersViewModel _ParametrosBancoMoneda;
        #endregion //Variables
        #region Propiedades
        public override string ModuleName {
            get { return "2.2.- Facturación (Continuación)"; }
        }

        public bool UsarOtrosCargoDeFactura {
            get {
                return Model.UsarOtrosCargoDeFacturaAsBool;
            }
            set {
                if (Model.UsarOtrosCargoDeFacturaAsBool != value) {
                    Model.UsarOtrosCargoDeFacturaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsarOtrosCargoDeFacturaPropertyName);
                }
            }
        }

        [LibCustomValidation("UltimaFechaDeFacturacionHistoricaValidating")]
        public DateTime UltimaFechaDeFacturacionHistorica {
            get {
                return Model.UltimaFechaDeFacturacionHistorica;
            }
            set {
                if (Model.UltimaFechaDeFacturacionHistorica != value) {
                    Model.UltimaFechaDeFacturacionHistorica = value;
                    IsDirty = true;
                    RaisePropertyChanged(UltimaFechaDeFacturacionHistoricaPropertyName);
                }
            }
        }

        public bool UsaCamposExtrasEnRenglonFactura {
            get {
                return Model.UsaCamposExtrasEnRenglonFacturaAsBool;
            }
            set {
                if (Model.UsaCamposExtrasEnRenglonFacturaAsBool != value) {
                    Model.UsaCamposExtrasEnRenglonFacturaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaCamposExtrasEnRenglonFacturaPropertyName);
                }
            }
        }
       
        public bool UsaCobroDirecto {
            get {
                var vFactContVM = ParametrosViewModel.ModuleList.Where(w => w.DisplayName == LibEnumHelper.GetDescription(eModulesLevelName.Factura)).FirstOrDefault().Groups.Where(y => y.DisplayName == new FacturaCobroFacturaViewModel(null, eAccionSR.Consultar).ModuleName).FirstOrDefault().Content as FacturaCobroFacturaViewModel;
                return vFactContVM.UsaCobroDirecto;
            }
        }
      
        public bool PermitirIncluirFacturacionHistorica {
            get {
                return Model.PermitirIncluirFacturacionHistoricaAsBool;
            }
            set {
                if (Model.PermitirIncluirFacturacionHistoricaAsBool != value) {
                    Model.PermitirIncluirFacturacionHistoricaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(PermitirIncluirFacturacionHistoricaPropertyName);
                    RaisePropertyChanged(IsEnabledUltimaFechaDeFacturacionHistoricaPropertyName);
                }
            }
        }

        public bool PermitirDobleDescuentoEnFactura {
            get {
                return Model.PermitirDobleDescuentoEnFacturaAsBool;
            }
            set {
                if (Model.PermitirDobleDescuentoEnFacturaAsBool != value) {
                    Model.PermitirDobleDescuentoEnFacturaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(PermitirDobleDescuentoEnFacturaPropertyName);
                }
            }
        }

        public bool ForzarFechaFacturaAmesEspecifico {
            get {
                return Model.ForzarFechaFacturaAmesEspecificoAsBool;
            }
            set {
                if (Model.ForzarFechaFacturaAmesEspecificoAsBool != value) {
                    Model.ForzarFechaFacturaAmesEspecificoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ForzarFechaFacturaAmesEspecificoPropertyName);
                    RaisePropertyChanged(IsEnabledMesFacturacionEnCursoPropertyName);
                }
            }
        }

        public bool GenerarCxCalEmitirUnaFacturaHistorica {
            get {
                return Model.GenerarCxCalEmitirUnaFacturaHistoricaAsBool;
            }
            set {
                if (Model.GenerarCxCalEmitirUnaFacturaHistoricaAsBool != value) {
                    Model.GenerarCxCalEmitirUnaFacturaHistoricaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(GenerarCxCalEmitirUnaFacturaHistoricaPropertyName);
                }
            }
        }

        [LibCustomValidation("MaximoDescuentoEnFacturaValidating")]
        public decimal MaximoDescuentoEnFactura {
            get {
                return Model.MaximoDescuentoEnFactura;
            }
            set {
                if (Model.MaximoDescuentoEnFactura != value) {
                    Model.MaximoDescuentoEnFactura = value;
                    IsDirty = true;
                    RaisePropertyChanged(MaximoDescuentoEnFacturaPropertyName);
                }
            }
        }

        public eMes MesFacturacionEnCurso {
            get {
                return Model.MesFacturacionEnCursoAsEnum;
            }
            set {
                if (Model.MesFacturacionEnCursoAsEnum != value) {
                    Model.MesFacturacionEnCursoAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(MesFacturacionEnCursoPropertyName);
                }
            }
        }

        public eAccionAlAnularFactDeMesesAnt AccionAlAnularFactDeMesesAnt {
            get {
                return Model.AccionAlAnularFactDeMesesAntAsEnum;
            }
            set {
                if (Model.AccionAlAnularFactDeMesesAntAsEnum != value) {
                    Model.AccionAlAnularFactDeMesesAntAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(AccionAlAnularFactDeMesesAntPropertyName);
                }
            }
        }
       
        public eBloquearEmision BloquearEmision {
            get {
                return Model.BloquearEmisionAsEnum;
            }
            set {
                if (Model.BloquearEmisionAsEnum != value) {
                    Model.BloquearEmisionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(BloquearEmisionPropertyName);
                }
            }
        }

        public bool MostrarMtoTotalBsFEnObservaciones {
            get {
                return Model.MostrarMtoTotalBsFEnObservacionesAsBool;
            }
            set {
                if (Model.MostrarMtoTotalBsFEnObservacionesAsBool != value) {
                    Model.MostrarMtoTotalBsFEnObservacionesAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(MostrarMtoTotalBsFEnObservacionesPropertyName);
                }
            }
        }       

        public eMes[] ArrayMes {
            get { return LibEnumHelper<eMes>.GetValuesInArray(); }
        }

        public eAccionAlAnularFactDeMesesAnt[] ArrayAccionAlAnularFactDeMesesAnt {
            get {
                return LibEnumHelper<eAccionAlAnularFactDeMesesAnt>.GetValuesInArray();
            }
        }

        public eBloquearEmision[] ArrayBloquearEmision {
            get {
                return LibEnumHelper<eBloquearEmision>.GetValuesInArray();
            }
        }        

        public bool SeMuestraTotalEnDivisas {
            get {
                return Model.SeMuestraTotalEnDivisasAsBool;
            }
            set {
                if (Model.SeMuestraTotalEnDivisasAsBool != value) {
                    Model.SeMuestraTotalEnDivisasAsBool = value;
                    RaisePropertyChanged(SeMuestraTotalEnDivisasPropertyName);
                }
            }
        }            

        public bool IsEnabledMesFacturacionEnCurso {
            get {
                return IsEnabled && ForzarFechaFacturaAmesEspecifico;
            }
        }
       
        public bool IsEnabledUltimaFechaDeFacturacionHistorica {
            get {
                return IsEnabled && PermitirIncluirFacturacionHistorica && !UsaImprentaDigital();
            }
        }

        public bool IsEnabledCamposExtrasEnRenglonFactura {
            get {
                if (UsaCamposExtrasEnRenglonFactura == true) {
                    return false;
                } else {
                    return IsEnabled;
                }
            }
        }      

        public bool IsEnabledUsaMaquinaFiscal {
            get {
                 return IsEnabled && UsaCobroDirecto && !ExisteCajaRegistradoraConMaquinaFiscal(); 
            }
        }

        public bool IsEnabledSeMuestraTotalEnDivisas {
            get {
                return IsEnabled && !UsaCobroDirectoEnMultimoneda;
            }
        }

        private bool UsaCobroDirectoEnMultimoneda {
            get {
                var vFactContVM = ParametrosViewModel.ModuleList.Where(w => w.DisplayName == LibEnumHelper.GetDescription(eModulesLevelName.Factura)).FirstOrDefault().Groups.Where(y => y.DisplayName == new FacturaCobroFacturaViewModel(null, eAccionSR.Consultar).ModuleName).FirstOrDefault().Content as FacturaCobroFacturaViewModel;
                return vFactContVM.UsaCobroDirectoEnMultimoneda;
            }
        }

        public bool UsaListaDePrecioEnMonedaExtranjera {
            get { return Model.UsaListaDePrecioEnMonedaExtranjeraAsBool; }
            set {
                if (Model.UsaListaDePrecioEnMonedaExtranjeraAsBool != value) {
                    Model.UsaListaDePrecioEnMonedaExtranjeraAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaListaDePrecioEnMonedaExtranjeraPropertyName);
                }
            }
        }

        public bool UsaListaDePrecioEnMonedaExtranjeraCXC {
            get {
                return Model.UsaListaDePrecioEnMonedaExtranjeraCXCAsBool;
            }
            set {
                if (Model.UsaListaDePrecioEnMonedaExtranjeraCXCAsBool != value) {
                    Model.UsaListaDePrecioEnMonedaExtranjeraCXCAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaListaDePrecioEnMonedaExtranjeraCXCPropertyName);
                }
            }
        }

        public int NroDiasMantenerTasaCambio {
            get {
                return Model.NroDiasMantenerTasaCambio;
            }
            set {
                if (Model.NroDiasMantenerTasaCambio != value) {
                    Model.NroDiasMantenerTasaCambio = value;
                    IsDirty = true;
                    RaisePropertyChanged(NroDiasMantenerTasaCambioPropertyName);
                }
            }

        }

        public DateTime FechaInicioImprentaDigital {
            get { return UsaImprentaDigital() ? FechaInicioServicioImprentaDigital() : _FechaInicioImprentaDigital; }
            set {
                if (_FechaInicioImprentaDigital != value) {
                    _FechaInicioImprentaDigital = value;
                    RaisePropertyChanged(FechaInicioImprentaDigitalPropertyName);
                }
            }
        }

        public bool UsaMaquinaFiscal {
            get {
                return Model.UsaMaquinaFiscalAsBool;
            }
            set {
                if (Model.UsaMaquinaFiscalAsBool != value) {
                    Model.UsaMaquinaFiscalAsBool = value;
                    RaisePropertyChanged(UsaMaquinaFiscalPropertyName);
                }
            }
        }

        public ParametersViewModel ParametrosViewModel {
            get {
                return _ParametrosBancoMoneda;
            }
            set {
                if (_ParametrosBancoMoneda != value) {
                    _ParametrosBancoMoneda = value;
                    RaisePropertyChanged(ParametrosBancoMonedaPropertyName);
                }
            }
        }

        public bool IsVisibleIFFechaReconversion {
            get { return true; }
        }
                
        #endregion //Propiedades

        #region Constructores
        public FacturaFacturacionContViewModel()
            : this(new FacturacionContinuacionStt(), eAccionSR.Insertar) {
        }
        public FacturaFacturacionContViewModel(FacturacionContinuacionStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = UsarOtrosCargoDeFacturaPropertyName;
            //LibMessages.Notification.Register<string>(this, OnStringParametrosComunesChanged);
        }
        #endregion //Constructores
        #region Metodos Generados
        protected override void InitializeLookAndFeel(FacturacionContinuacionStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override FacturacionContinuacionStt FindCurrentRecord(FacturacionContinuacionStt valModel) {
            if (valModel == null) {
                return new FacturacionContinuacionStt();
            }
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<FacturacionContinuacionStt>, IList<FacturacionContinuacionStt>> GetBusinessComponent() {
            return null;
        }      

        public string PromptMostrarReconversionEnObservacion {
            get {
                string vMensaje = "";
                if (LibDate.Today() >= clsUtilReconv.GetFechaReconversion()) {
                    vMensaje = "Mostrar Totales en Bolívares Soberanos";
                } else {
                    vMensaje = "Mostrar Totales en Bolívares Digitales";
                }
                return vMensaje;
            }
        }

        private bool UsaImprentaDigital() {
            return LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaImprentaDigital"));
        }

        private DateTime FechaInicioServicioImprentaDigital() {
            return LibConvert.ToDate(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "FechaInicioImprentaDigital"));
        }       

        #endregion //Metodos Generados
        #region Validating
        private ValidationResult UltimaFechaDeFacturacionHistoricaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (PermitirIncluirFacturacionHistorica) {
                    if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(UltimaFechaDeFacturacionHistorica, false, Action)) {
                        vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram(this.ModuleName + "-> Última Fecha de Facturación Histórica."));
                    } else if (LibDate.DateIsGreaterThanToday(UltimaFechaDeFacturacionHistorica, false, string.Empty)) {
                        vResult = new ValidationResult(this.ModuleName + "-> Última Fecha de Facturación Histórica debe ser menor a la fecha actual.");
                    } else if (UsaImprentaDigital()) {
                        FechaInicioImprentaDigital = UsaImprentaDigital() ? FechaInicioServicioImprentaDigital() : FechaInicioImprentaDigital;
                        RaisePropertyChanged(FechaInicioImprentaDigitalPropertyName);
                        vResult = (UltimaFechaDeFacturacionHistorica >= FechaInicioImprentaDigital) ?
                            new ValidationResult(this.ModuleName + "-> Última Fecha de Facturación Histórica debe ser menor a la fecha de inicio de uso de Imprenta Digital.")
                            : vResult;
                    }
                }
            }
            return vResult;
        }

        private ValidationResult EmitirDirectoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                var vFactContVM = ParametrosViewModel.ModuleList.Where(w => w.DisplayName == LibEnumHelper.GetDescription(eModulesLevelName.Factura)).FirstOrDefault().Groups.Where(y => y.DisplayName == new FacturaCobroFacturaViewModel(null, eAccionSR.Consultar).ModuleName).FirstOrDefault().Content as FacturaCobroFacturaViewModel;                
                if (vFactContVM.UsaCobroDirecto) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram(this.ModuleName + "-> Debe habilitar Emitir en Directo"));
                }
            }
            return vResult;
        }

        private ValidationResult MaximoDescuentoEnFacturaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (Model.MaximoDescuentoEnFactura > 100) {
                    vResult = new ValidationResult(this.ModuleName + "-> Debe indicar un Porcentaje menor o igual a 100%.");
                }
            }
            return vResult;
        }

        #endregion //Validating

        private bool ExisteCajaRegistradoraConMaquinaFiscal() {
            bool vResult = false;
            int vConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            ISettValueByCompanyPdn insParametrosByCompany = new clsSettValueByCompanyNav();
            vResult = insParametrosByCompany.ExisteCajaConMaquinaFiscal(vConsecutivoCompania);
            return vResult;
        }

    } //End of class FacturaFacturacionContViewModel
} //End of namespace Galac.Saw.Uil.SttDef


