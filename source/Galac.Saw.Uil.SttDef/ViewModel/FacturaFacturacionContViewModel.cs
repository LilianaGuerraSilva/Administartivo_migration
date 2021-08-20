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

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class FacturaFacturacionContViewModel : LibInputViewModelMfc<FacturacionContinuacionStt> {

        #region Constantes
        public const string UsarOtrosCargoDeFacturaPropertyName = "UsarOtrosCargoDeFactura";
        public const string UltimaFechaDeFacturacionHistoricaPropertyName = "UltimaFechaDeFacturacionHistorica";
        public const string UsaCamposExtrasEnRenglonFacturaPropertyName = "UsaCamposExtrasEnRenglonFactura";
        public const string UsaCobroDirectoPropertyName = "UsaCobroDirecto";
        public const string PermitirIncluirFacturacionHistoricaPropertyName = "PermitirIncluirFacturacionHistorica";
        public const string PermitirDobleDescuentoEnFacturaPropertyName = "PermitirDobleDescuentoEnFactura";
        public const string ForzarFechaFacturaAmesEspecificoPropertyName = "ForzarFechaFacturaAmesEspecifico";
        public const string GenerarCxCalEmitirUnaFacturaHistoricaPropertyName = "GenerarCxCalEmitirUnaFacturaHistorica";
        public const string MaximoDescuentoEnFacturaPropertyName = "MaximoDescuentoEnFactura";
        public const string MesFacturacionEnCursoPropertyName = "MesFacturacionEnCurso";
        public const string AccionAlAnularFactDeMesesAntPropertyName = "AccionAlAnularFactDeMesesAnt";
        public const string EmitirDirectoPropertyName = "EmitirDirecto";
        public const string ConceptoBancarioCobroDirectoPropertyName = "ConceptoBancarioCobroDirecto";
        public const string CuentaBancariaCobroDirectoPropertyName = "CuentaBancariaCobroDirecto";
        public const string IsEnabledMesFacturacionEnCursoPropertyName = "IsEnabledMesFacturacionEnCurso";
        public const string IsEnabledEmitirDirectoPropertyName = "IsEnabledEmitirDirecto";
        public const string BloquearEmisionPropertyName = "BloquearEmision";
        public const string IsEnabledUltimaFechaDeFacturacionHistoricaPropertyName = "IsEnabledUltimaFechaDeFacturacionHistorica";
        public const string IsEnabledUsaCobroDirectoPropertyName = "IsEnabledUsaCobroDirecto";
        public const string IsEnabledCuentaBancariaCobroDirectoPropertyName = "IsEnabledCuentaBancariaCobroDirecto";
        public const string IsEnabledConceptoBancarioCobroDirectoPropertyName = "IsEnabledConceptoBancarioCobroDirecto";
        public const string MostrarMtoTotalBsFEnObservacionesPropertyName = "MostrarMtoTotalBsFEnObservaciones";
        private const string SeMuestraTotalEnDivisasPropertyName = "SeMuestraTotalEnDivisas";
        private const string UsaCobroDirectoEnMultimonedaPropertyName = "UsaCobroDirectoEnMultimoneda";
        private const string IsEnabledUsaCobroDirectoEnMultimonedaPropertyName = "IsEnabledUsaCobroDirectoEnMultimoneda";
        private const string ConceptoBancarioCobroMultimonedaPropertyName = "ConceptoBancarioCobroMultimoneda";
        private const string CuentaBancariaCobroMultimonedaPropertyName = "CuentaBancariaCobroMultimoneda";
        public const string IsEnabledCuentaBancariaCobroMultimonedaPropertyName = "IsEnabledCuentaBancariaCobroMultimoneda";
        public const string IsEnabledConceptoBancarioCobroMultimonedaPropertyName = "IsEnabledConceptoBancarioCobroMultimoneda";
        public const string UsaListaDePrecioEnMonedaExtranjeraPropertyName = "UsaListaDePrecioEnMonedaExtranjera";
        public const string UsaListaDePrecioEnMonedaExtranjeraCXCPropertyName = "UsaListaDePrecioEnMonedaExtranjeraCXC";
        public const string NroDiasMantenerTasaCambioPropertyName = "NroDiasMantenerTasaCambio";
        #endregion

        #region Variables
        private FkConceptoBancarioViewModel _ConexionConceptoBancarioCobroDirecto = null;
        private FkCuentaBancariaViewModel _ConexionCuentaBancariaCobroDirecto = null;
        private FkConceptoBancarioViewModel _ConexionConceptoBancarioCobroMultimoneda = null;
        private FkCuentaBancariaViewModel _ConexionCuentaBancariaCobroMultimoneda = null;
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

        [LibCustomValidation("CobroDirectoValidating")]
        public bool UsaCobroDirecto {
            get {
                return Model.UsaCobroDirectoAsBool;
            }
            set {
                if (Model.UsaCobroDirectoAsBool != value) {
                    Model.UsaCobroDirectoAsBool = value;
                    if (!Model.UsaCobroDirectoAsBool) {
                        CuentaBancariaCobroDirecto = string.Empty;
                        ConceptoBancarioCobroDirecto = string.Empty;
                        UsaCobroDirectoEnMultimoneda = false;
                        CuentaBancariaCobroMultimoneda = string.Empty;
                        ConceptoBancarioCobroMultimoneda = string.Empty;
                    } else {
                        EmitirDirecto = true;
                        if (LibString.IsNullOrEmpty(CuentaBancariaCobroDirecto)) {
                            ReloadCodigoGenericoCuentaBancaria();
                        }
                    }
                    IsDirty = true;
                    RaisePropertyChanged(UsaCobroDirectoPropertyName);
                    RaisePropertyChanged(IsEnabledUsaCobroDirectoPropertyName);
                    RaisePropertyChanged(ConceptoBancarioCobroDirectoPropertyName);
                    RaisePropertyChanged(ConceptoBancarioCobroMultimonedaPropertyName);
                    RaisePropertyChanged(CuentaBancariaCobroMultimonedaPropertyName);
                    RaisePropertyChanged(IsEnabledCuentaBancariaCobroDirectoPropertyName);
                    RaisePropertyChanged(IsEnabledConceptoBancarioCobroDirectoPropertyName);
                    RaisePropertyChanged(UsaCobroDirectoEnMultimonedaPropertyName);
                    RaisePropertyChanged(IsEnabledUsaCobroDirectoEnMultimonedaPropertyName);
                    RaisePropertyChanged(CuentaBancariaCobroMultimonedaPropertyName);
                    RaisePropertyChanged(ConceptoBancarioCobroMultimonedaPropertyName);
                    RaisePropertyChanged(IsEnabledConceptoBancarioCobroMultimonedaPropertyName);
                    RaisePropertyChanged(IsEnabledCuentaBancariaCobroMultimonedaPropertyName);
                    LibMessages.Notification.Send<bool>(Model.UsaCobroDirectoAsBool, UsaCobroDirectoPropertyName);
                }
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

        public bool EmitirDirecto {
            get {
                return Model.EmitirDirectoAsBool;
            }
            set {
                if (Model.EmitirDirectoAsBool != value) {
                    Model.EmitirDirectoAsBool = value;
                    if (!Model.EmitirDirectoAsBool) {
                        LibMessages.MessageBox.Warning(this, "Al no tener activado Emitir Directo, la opción de Cobro Directo será desactivada.", string.Empty);
                        CuentaBancariaCobroDirecto = string.Empty;
                        ConceptoBancarioCobroDirecto = string.Empty;
                        UsaCobroDirecto = EmitirDirecto;
                    }
                    IsDirty = true;
                    RaisePropertyChanged(EmitirDirectoPropertyName);
                    RaisePropertyChanged(IsEnabledEmitirDirectoPropertyName);
                    LibMessages.Notification.Send<bool>(Model.EmitirDirectoAsBool, EmitirDirectoPropertyName);
                }
            }
        }

        [LibCustomValidation("ConceptoBancarioCobroDirectoValidating")]
        public string ConceptoBancarioCobroDirecto {
            get {
                return Model.ConceptoBancarioCobroDirecto;
            }
            set {
                if (Model.ConceptoBancarioCobroDirecto != value) {
                    Model.ConceptoBancarioCobroDirecto = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConceptoBancarioCobroDirectoPropertyName);
                    LibMessages.Notification.Send<string>(Model.ConceptoBancarioCobroDirecto, ConceptoBancarioCobroDirectoPropertyName);
                    if (LibString.IsNullOrEmpty(ConceptoBancarioCobroDirecto, true)) {
                        ConexionConceptoBancarioCobroDirecto = null;
                    }
                }
            }
        }

        [LibCustomValidation("CuentaBancariaCobroDirectoValidating")]
        public string CuentaBancariaCobroDirecto {
            get {
                return Model.CuentaBancariaCobroDirecto;
            }
            set {
                if (Model.CuentaBancariaCobroDirecto != value) {
                    if (UsaCobroDirecto && value == null) {
                        ExecuteChooseCuentaBancariaCobroDirectoCommand(string.Empty);
                    } else {
                        Model.CuentaBancariaCobroDirecto = value;
                    }
                    IsDirty = true;
                    RaisePropertyChanged(CuentaBancariaCobroDirectoPropertyName);
                    LibMessages.Notification.Send<string>(Model.CuentaBancariaCobroDirecto, CuentaBancariaCobroDirectoPropertyName);
                    if (LibString.IsNullOrEmpty(CuentaBancariaCobroDirecto, true)) {
                        ConexionCuentaBancariaCobroDirecto = null;
                    }
                }
            }
        }

        public bool UsaCobroDirectoEnMultimoneda {
            get {
                return Model.UsaCobroDirectoEnMultimonedaAsBool;
            }
            set {
                if (Model.UsaCobroDirectoEnMultimonedaAsBool != value) {
                    Model.UsaCobroDirectoEnMultimonedaAsBool = value;
                    if (!Model.UsaCobroDirectoEnMultimonedaAsBool) {
                        CuentaBancariaCobroMultimoneda = string.Empty;
                        ConceptoBancarioCobroMultimoneda = string.Empty;
                    }
                    RaisePropertyChanged(UsaCobroDirectoEnMultimonedaPropertyName);
                    RaisePropertyChanged(ConceptoBancarioCobroMultimonedaPropertyName);
                    RaisePropertyChanged(CuentaBancariaCobroMultimonedaPropertyName);
                    RaisePropertyChanged(IsEnabledUsaCobroDirectoEnMultimonedaPropertyName);
                    RaisePropertyChanged(IsEnabledCuentaBancariaCobroMultimonedaPropertyName);
                    RaisePropertyChanged(IsEnabledConceptoBancarioCobroMultimonedaPropertyName);
                }
            }
        }

        [LibCustomValidation("ConceptoBancarioCobroMultimonedaValidating")]
        public string ConceptoBancarioCobroMultimoneda {
            get {
                return Model.ConceptoBancarioCobroMultimoneda;
            }
            set {
                if (Model.ConceptoBancarioCobroMultimoneda != value) {
                    Model.ConceptoBancarioCobroMultimoneda = value;
                    RaisePropertyChanged(ConceptoBancarioCobroMultimonedaPropertyName);
                    LibMessages.Notification.Send<string>(Model.ConceptoBancarioCobroMultimoneda, ConceptoBancarioCobroMultimonedaPropertyName);
                    if (LibString.IsNullOrEmpty(ConceptoBancarioCobroMultimoneda, true)) {
                        ConexionConceptoBancarioCobroMultimoneda = null;
                    }
                }
            }
        }

        [LibCustomValidation("CuentaBancariaCobroMultimonedaValidating")]
        public string CuentaBancariaCobroMultimoneda {
            get {
                return Model.CuentaBancariaCobroMultimoneda;
            }
            set {
                if (Model.CuentaBancariaCobroMultimoneda != value) {
                    if (UsaCobroDirectoEnMultimoneda && value == null) {
                        ExecuteChooseCuentaBancariaCobroMultimonedaCommand(string.Empty);
                    } else {
                        Model.CuentaBancariaCobroMultimoneda = value;
                    }
                    RaisePropertyChanged(CuentaBancariaCobroMultimonedaPropertyName);
                    LibMessages.Notification.Send<string>(Model.CuentaBancariaCobroMultimoneda, CuentaBancariaCobroMultimonedaPropertyName);
                    if (LibString.IsNullOrEmpty(CuentaBancariaCobroMultimoneda, true)) {
                        ConexionCuentaBancariaCobroMultimoneda = null;
                    }
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
            get {
                return LibEnumHelper<eMes>.GetValuesInArray();
            }
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
        public FkConceptoBancarioViewModel ConexionConceptoBancarioCobroDirecto {
            get {
                return _ConexionConceptoBancarioCobroDirecto;
            }
            set {
                if (_ConexionConceptoBancarioCobroDirecto != value) {
                    _ConexionConceptoBancarioCobroDirecto = value;
                    if (_ConexionConceptoBancarioCobroDirecto != null) {
                        ConceptoBancarioCobroDirecto = _ConexionConceptoBancarioCobroDirecto.Codigo;
                    }
                }
                if (_ConexionConceptoBancarioCobroDirecto == null) {
                    ConceptoBancarioCobroDirecto = string.Empty;
                }
                RaisePropertyChanged(ConceptoBancarioCobroDirectoPropertyName);
            }
        }

        public FkCuentaBancariaViewModel ConexionCuentaBancariaCobroDirecto {
            get {
                return _ConexionCuentaBancariaCobroDirecto;
            }
            set {
                if (_ConexionCuentaBancariaCobroDirecto != value) {
                    _ConexionCuentaBancariaCobroDirecto = value;
                    if (_ConexionCuentaBancariaCobroDirecto != null) {
                        CuentaBancariaCobroDirecto = _ConexionCuentaBancariaCobroDirecto.Codigo;
                    } else if (_ConexionCuentaBancariaCobroDirecto == null) {
                        CuentaBancariaCobroDirecto = string.Empty;
                    }
                }
                if (_ConexionCuentaBancariaCobroDirecto == null) {
                    CuentaBancariaCobroDirecto = string.Empty;
                }
                RaisePropertyChanged(CuentaBancariaCobroDirectoPropertyName);
            }
        }

        public FkConceptoBancarioViewModel ConexionConceptoBancarioCobroMultimoneda {
            get {
                return _ConexionConceptoBancarioCobroMultimoneda;
            }
            set {
                if (_ConexionConceptoBancarioCobroMultimoneda != value) {
                    _ConexionConceptoBancarioCobroMultimoneda = value;
                    if (_ConexionConceptoBancarioCobroMultimoneda != null) {
                        ConceptoBancarioCobroMultimoneda = _ConexionConceptoBancarioCobroMultimoneda.Codigo;
                    }
                }
                if (_ConexionConceptoBancarioCobroMultimoneda == null) {
                    ConceptoBancarioCobroMultimoneda = string.Empty;
                }
                RaisePropertyChanged(ConceptoBancarioCobroMultimonedaPropertyName);
            }
        }

        public FkCuentaBancariaViewModel ConexionCuentaBancariaCobroMultimoneda {
            get {
                return _ConexionCuentaBancariaCobroMultimoneda;
            }
            set {
                if (_ConexionCuentaBancariaCobroMultimoneda != value) {
                    _ConexionCuentaBancariaCobroMultimoneda = value;
                    if (_ConexionCuentaBancariaCobroMultimoneda != null) {
                        CuentaBancariaCobroMultimoneda = _ConexionCuentaBancariaCobroMultimoneda.Codigo;
                    } else if (_ConexionCuentaBancariaCobroMultimoneda == null) {
                        CuentaBancariaCobroMultimoneda = string.Empty;
                    }
                }
                if (_ConexionCuentaBancariaCobroMultimoneda == null) {
                    CuentaBancariaCobroMultimoneda = string.Empty;
                }
                RaisePropertyChanged(CuentaBancariaCobroMultimonedaPropertyName);
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

        public RelayCommand<string> ChooseConceptoBancarioCobroDirectoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaBancariaCobroDirectoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseConceptoBancarioCobroMultimonedaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaBancariaCobroMultimonedaCommand {
            get;
            private set;
        }

        public bool IsVisibleUsarOtrosCargoDeFactura {
            get {
                return !AppMemoryInfo.GlobalValuesGetBool("Parametros", "EsSistemaParaIG");
            }
        }

        public bool IsEnabledMesFacturacionEnCurso {
            get {
                return IsEnabled && ForzarFechaFacturaAmesEspecifico;
            }
        }

        public bool IsEnabledEmitirDirecto {
            get {
                return IsEnabled && EmitirDirecto;
            }
        }

        public bool IsEnabledUsaCobroDirecto {
            get {
                return IsEnabled && UsaCobroDirecto;
            }
        }

        public bool IsEnabledUltimaFechaDeFacturacionHistorica {
            get {
                return IsEnabled && PermitirIncluirFacturacionHistorica;
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

        public bool IsEnabledUsaCobroDirectoEnMultimoneda {
            get {
                return IsEnabled && UsaCobroDirecto;
            }
        }

        public bool isVisibleParaPeru {
            get {
                bool vResult = true;
                if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                    vResult = false;
                }
                return vResult;
            }
        }

        public bool UsaListaDePrecioEnMonedaExtranjera {
            get {
                return Model.UsaListaDePrecioEnMonedaExtranjeraAsBool;
            }
            set {
                if (Model.UsaListaDePrecioEnMonedaExtranjeraAsBool != value) {
                    Model.UsaListaDePrecioEnMonedaExtranjeraAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaListaDePrecioEnMonedaExtranjeraPropertyName);
                    //RaisePropertyChanged(UsaListaDePrecioEnMonedaExtranjeraCXCPropertyName);
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
        #endregion //Propiedades

        #region Constructores
        public FacturaFacturacionContViewModel()
            : this(new FacturacionContinuacionStt(), eAccionSR.Insertar) {
        }
        public FacturaFacturacionContViewModel(FacturacionContinuacionStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = UsarOtrosCargoDeFacturaPropertyName;
            LibMessages.Notification.Register<string>(this, OnStringParametrosComunesChanged);
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
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
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInString("UsarOtrosCargoDeFactura", valModel.UsarOtrosCargoDeFactura, 0);
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "FacturaFacturacionContGET", vParams.Get()).FirstOrDefault();

            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<FacturacionContinuacionStt>, IList<FacturacionContinuacionStt>> GetBusinessComponent() {
            return null;
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseConceptoBancarioCobroDirectoCommand = new RelayCommand<string>(ExecuteChooseConceptoBancarioCobroDirectoCommand);
            ChooseCuentaBancariaCobroDirectoCommand = new RelayCommand<string>(ExecuteChooseCuentaBancariaCobroDirectoCommand);
            ChooseConceptoBancarioCobroMultimonedaCommand = new RelayCommand<string>(ExecuteChooseConceptoBancarioCobroMultimonedaCommand);
            ChooseCuentaBancariaCobroMultimonedaCommand = new RelayCommand<string>(ExecuteChooseCuentaBancariaCobroMultimonedaCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionConceptoBancarioCobroDirecto = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", LibSearchCriteria.CreateCriteria("codigo", ConceptoBancarioCobroDirecto), new clsSettValueByCompanyNav());
            ConexionCuentaBancariaCobroDirecto = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkCuentaBancariaViewModel>("Cuenta Bancaria", LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.Codigo", CuentaBancariaCobroDirecto), new clsSettValueByCompanyNav());
            ConexionConceptoBancarioCobroMultimoneda = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", LibSearchCriteria.CreateCriteria("codigo", ConceptoBancarioCobroMultimoneda), new clsSettValueByCompanyNav());
            ConexionCuentaBancariaCobroMultimoneda = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkCuentaBancariaViewModel>("Cuenta Bancaria", LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.Codigo", CuentaBancariaCobroMultimoneda), new clsSettValueByCompanyNav());
        }

        private void ExecuteChooseConceptoBancarioCobroDirectoCommand(string valcodigo) {
            try {
                if (valcodigo == null) {
                    valcodigo = string.Empty;
                }

                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valcodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Ingreso));
                ConexionConceptoBancarioCobroDirecto = null;
                ConexionConceptoBancarioCobroDirecto = LibFKRetrievalHelper.ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCuentaBancariaCobroDirectoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_CuentaBancaria_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.EsCajaChica", LibConvert.BoolToSN(false));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.ConsecutivoCompania", Mfc.GetInt("Compania")), eLogicOperatorType.And);
                ConexionCuentaBancariaCobroDirecto = null;
                ConexionCuentaBancariaCobroDirecto = LibFKRetrievalHelper.ChooseRecord<FkCuentaBancariaViewModel>("Cuenta Bancaria", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCuentaBancariaCobroDirecto != null) {
                    CuentaBancariaCobroDirecto = ConexionCuentaBancariaCobroDirecto.Codigo;
                } else {
                    CuentaBancariaCobroDirecto = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseConceptoBancarioCobroMultimonedaCommand(string valcodigo) {
            try {
                if (valcodigo == null) {
                    valcodigo = string.Empty;
                }

                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valcodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Ingreso));
                ConexionConceptoBancarioCobroMultimoneda = null;
                ConexionConceptoBancarioCobroMultimoneda = LibFKRetrievalHelper.ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCuentaBancariaCobroMultimonedaCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_CuentaBancaria_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.EsCajaChica", LibConvert.BoolToSN(false));
                vFixedCriteria.Add("Gv_CuentaBancaria_B1.CodigoMoneda", eBooleanOperatorType.IdentityInequality, "VES");
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.ConsecutivoCompania", Mfc.GetInt("Compania")), eLogicOperatorType.And);
                ConexionCuentaBancariaCobroMultimoneda = null;
                ConexionCuentaBancariaCobroMultimoneda = LibFKRetrievalHelper.ChooseRecord<FkCuentaBancariaViewModel>("Cuenta Bancaria", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCuentaBancariaCobroMultimoneda != null) {
                    CuentaBancariaCobroMultimoneda = ConexionCuentaBancariaCobroMultimoneda.Codigo;
                } else {
                    CuentaBancariaCobroMultimoneda = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult UltimaFechaDeFacturacionHistoricaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (PermitirIncluirFacturacionHistorica) {
                    if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(UltimaFechaDeFacturacionHistorica, false, Action)) {
                        vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram(this.ModuleName + "-> Ultima Fecha de Facturación Histórica"));
                    } else if (LibDate.DateIsGreaterThanToday(UltimaFechaDeFacturacionHistorica, false, string.Empty)) {
                        vResult = new ValidationResult(this.ModuleName + "-> Ultima Fecha de Facturación Histórica debe ser menor a la fecha actual");
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
                if (Model.UsaCobroDirectoAsBool) {
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

        private void ReloadCodigoGenericoCuentaBancaria() {
            Galac.Adm.Ccl.Banco.ICuentaBancariaPdn insCuentaBancariaPdn = new Galac.Adm.Brl.Banco.clsCuentaBancariaNav();
            ConexionCuentaBancariaCobroDirecto = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkCuentaBancariaViewModel>("Cuenta Bancaria", LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.Codigo", insCuentaBancariaPdn.GetCuentaBancariaGenericaPorDefecto()), new clsSettValueByCompanyNav());
        }



        private ValidationResult ConceptoBancarioCobroDirectoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (UsaCobroDirecto && LibString.IsNullOrEmpty(ConceptoBancarioCobroDirecto)) {
                    vResult = new ValidationResult(this.ModuleName + "-> Debe indicar un Concepto Bancario de Cobro Directo");
                }
            }
            return vResult;
        }

        private ValidationResult CuentaBancariaCobroDirectoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (UsaCobroDirecto && LibString.IsNullOrEmpty(CuentaBancariaCobroDirecto)) {
                    vResult = new ValidationResult(this.ModuleName + "-> Debe indicar una cuenta bancaria cobro directo");
                }
            }
            return vResult;
        }

        private ValidationResult ConceptoBancarioCobroMultimonedaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (UsaCobroDirectoEnMultimoneda && LibString.IsNullOrEmpty(ConceptoBancarioCobroMultimoneda)) {
                    vResult = new ValidationResult(this.ModuleName + "-> Debe indicar un Concepto Bancario de Cobro en Multimoneda");
                }
            }
            return vResult;
        }

        private ValidationResult CuentaBancariaCobroMultimonedaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (UsaCobroDirectoEnMultimoneda && LibString.IsNullOrEmpty(CuentaBancariaCobroMultimoneda)) {
                    vResult = new ValidationResult(this.ModuleName + "-> Debe indicar una cuenta bancaria en moneda extranjera para Cobro en Multimoneda");
                }
            }
            return vResult;
        }

        private ValidationResult CobroDirectoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (UsaCobroDirecto && UsaListaDePrecioEnMonedaExtranjeraCXC) {
                    vResult = new ValidationResult($"No es posible activar los parámetros \"{this.ModuleName} - Generar CxC en Moneda Extranjera\" y \"{this.ModuleName} - Cobro Directo\" simultaneamente. Para hacer uso del parámetro \"Cobro Directo\", por favor desactive \"Generar CXC en Moneda Extranjera\".");
                }
            }
            return vResult;
        }

        public bool IsEnabledCuentaBancariaCobroDirecto {
            get {
                return IsEnabled && UsaCobroDirecto;
            }
        }

        public bool IsEnabledConceptoBancarioCobroDirecto {
            get {
                return IsEnabled && UsaCobroDirecto;
            }
        }

        public bool IsEnabledCuentaBancariaCobroMultimoneda {
            get {
                return IsEnabled && UsaCobroDirectoEnMultimoneda;
            }
        }

        public bool IsEnabledConceptoBancarioCobroMultimoneda {
            get {
                return IsEnabled && UsaCobroDirectoEnMultimoneda;
            }
        }

        private void OnStringParametrosComunesChanged(NotificationMessage<string> valMessage) {
            try {
                if (LibString.S1IsEqualToS2(LibConvert.ToStr(valMessage.Notification), CuentaBancariaCobroDirectoPropertyName)) {
                    CuentaBancariaCobroDirecto = valMessage.Content;
                } else if (LibString.S1IsEqualToS2(LibConvert.ToStr(valMessage.Notification), ConceptoBancarioCobroDirectoPropertyName)) {
                    ConceptoBancarioCobroDirecto = valMessage.Content;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        public bool IsVisibleIFFechaReconversion {
            get {
                return true;
            }
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
        #endregion //Metodos Generados
    } //End of class FacturaFacturacionContViewModel
} //End of namespace Galac.Saw.Uil.SttDef


