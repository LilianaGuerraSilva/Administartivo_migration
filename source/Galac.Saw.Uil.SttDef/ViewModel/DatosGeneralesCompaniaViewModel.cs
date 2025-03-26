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
using Galac.Saw.Lib;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class DatosGeneralesCompaniaViewModel:LibInputViewModelMfc<CompaniaStt> {
        #region Constantes
        public const string AutorellenaResumenDiarioPropertyName = "AutorellenaResumenDiario";
        public const string EsAsociadoEnCtaDeParticipacionPropertyName = "EsAsociadoEnCtaDeParticipacion";
        public const string FechaDeInicioContabilizacionPropertyName = "FechaDeInicioContabilizacion";
        public const string FechaMinimaIngresarDatosPropertyName = "FechaMinimaIngresarDatos";
        public const string IntegracionRISPropertyName = "IntegracionRIS";
        public const string TipoDeAgrupacionParaLibrosDeVentaPropertyName = "TipoDeAgrupacionParaLibrosDeVenta";
        public const string TipoNegocioPropertyName = "TipoNegocio";
        public const string UsaNotaEntregaPropertyName = "UsaNotaEntrega";
        public const string VerificarDocumentoSinContabilizarPropertyName = "VerificarDocumentoSinContabilizar";
        public const string UsarVentasConIvaDiferidoPropertyName = "UsarVentasConIvaDiferido";
        public const string ImprimirVentasDiferidasPropertyName = "ImprimirVentasDiferidas";
        public const string IsEnabledImprimirVentasDiferidasPropertyName = "IsEnabledImprimirVentasDiferidas";
        public const string AplicacionAlicuotaEspecialPropertyName = "AplicacionAlicuotaEspecial";
        public const string AplicarIVAEspecialPropertyName = "AplicarIVAEspecial";
        public const string FacturarPorDefectoIvaEspecialPropertyName = "FacturarPorDefectoIvaEspecial";
        public const string FechaInicioAlicuotaIva10PorcientoPropertyName = "FechaInicioAlicuotaIva10Porciento";
        public const string FechaFinAlicuotaIva10PorcientoPropertyName = "FechaFinAlicuotaIva10Porciento";
        public const string IsEnabledAplicacionAlicuotaEspecialPropertyName = "IsEnabledAplicacionAlicuotaEspecial";
        public const string IsVisibleFacturarPorDefectoIvaEspecialPropertyName = "IsVisibleFacturarPorDefectoIvaEspecial";
        public const string IsEnableFacturarPorDefectoIvaEspecialPropertyName = "IsEnabledFacturarPorDefectoIvaEspecial";
        public const string IsEnabledFechaFinAlicuotaIva10PorcientoPropertyName = "IsEnabledFechaFinAlicuotaIva10Porciento";
        public const string ImprimirMensajeAplicacionDecretoPropertyName = "ImprimirMensajeAplicacionDecreto";
        public const string BaseDeCalculoParaAlicuotaEspecialPropertyName = "BaseDeCalculoParaAlicuotaEspecial";

        #endregion

        #region Variables
        bool mEsFacturadorBasico;
        #endregion

        #region Constructores
        public DatosGeneralesCompaniaViewModel()
           : this(new CompaniaStt(), eAccionSR.Insertar) {
        }
        public DatosGeneralesCompaniaViewModel(CompaniaStt initModel, eAccionSR initAction)
           : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = AutorellenaResumenDiarioPropertyName;
            mEsFacturadorBasico = new clsLibSaw().EsVersionFacturadorBasico();
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores

        #region Propiedades

        public override string ModuleName {
            get { return "1.1.- Compañía"; }
        }

        public bool AutorellenaResumenDiario {
            get {
                return Model.AutorellenaResumenDiarioAsBool;
            }
            set {
                if(Model.AutorellenaResumenDiarioAsBool != value) {
                    Model.AutorellenaResumenDiarioAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(AutorellenaResumenDiarioPropertyName);
                }
            }
        }

        public bool EsAsociadoEnCtaDeParticipacion {
            get {
                return Model.EsAsociadoEnCtaDeParticipacionAsBool;
            }
            set {
                if(Model.EsAsociadoEnCtaDeParticipacionAsBool != value) {
                    Model.EsAsociadoEnCtaDeParticipacionAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EsAsociadoEnCtaDeParticipacionPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaDeInicioContabilizacionValidating")]
        public DateTime FechaDeInicioContabilizacion {
            get {
                return Model.FechaDeInicioContabilizacion;
            }
            set {
                if(Model.FechaDeInicioContabilizacion != value) {
                    Model.FechaDeInicioContabilizacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaDeInicioContabilizacionPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaMinimaIngresarDatosValidating")]
        public DateTime FechaMinimaIngresarDatos {
            get {
                return Model.FechaMinimaIngresarDatos;
            }
            set {
                if(Model.FechaMinimaIngresarDatos != value) {
                    Model.FechaMinimaIngresarDatos = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaMinimaIngresarDatosPropertyName);
                }
            }
        }

        public bool IntegracionRIS {
            get {
                return Model.IntegracionRISAsBool;
            }
            set {
                if(Model.IntegracionRISAsBool != value) {
                    Model.IntegracionRISAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(IntegracionRISPropertyName);
                }
            }
        }

        public bool UsarVentasConIvaDiferido { //Istayul
            get {
                return Model.UsarVentasConIvaDiferidoAsBool;
            }
            set {
                if(Model.UsarVentasConIvaDiferidoAsBool != value) {
                    Model.UsarVentasConIvaDiferidoAsBool = value;
                    IsDirty = true;
                    if(Model.UsarVentasConIvaDiferidoAsBool == false) {
                        Model.ImprimirVentasDiferidasAsBool = false;
                        RaisePropertyChanged(ImprimirVentasDiferidasPropertyName);
                    }
                    RaisePropertyChanged(UsarVentasConIvaDiferidoPropertyName);
                    RaisePropertyChanged(IsEnabledImprimirVentasDiferidasPropertyName);
                }
            }
        }

        public bool ImprimirVentasDiferidas {
            get {
                return Model.ImprimirVentasDiferidasAsBool;
            }
            set {
                if(Model.ImprimirVentasDiferidasAsBool != value) {
                    Model.ImprimirVentasDiferidasAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimirVentasDiferidasPropertyName);
                }
            }
        }


        public bool AplicarIVAEspecial {
            get {
                return Model.AplicarIVAEspecialAsBool;
            }
            set {
                if(Model.AplicarIVAEspecialAsBool != value) {
                    Model.AplicarIVAEspecialAsBool = value;
                    IsDirty = true;
                    if(Model.AplicarIVAEspecialAsBool == false) {
                        Model.FacturarPorDefectoIvaEspecialAsBool = false;
                        AplicacionAlicuotaEspecial = eAplicacionAlicuota.No_Aplica;
                        BaseDeCalculoParaAlicuotaEspecial = eBaseCalculoParaAlicuotaEspecial.Solo_Base_Imponible_Alicuota_General;
                        Model.ImprimirMensajeAplicacionDecretoAsBool = false;
                        RaisePropertyChanged(FacturarPorDefectoIvaEspecialPropertyName);
                    }
                    RaisePropertyChanged(AplicarIVAEspecialPropertyName);
                    RaisePropertyChanged(AplicacionAlicuotaEspecialPropertyName);
                    RaisePropertyChanged(BaseDeCalculoParaAlicuotaEspecialPropertyName);
                    RaisePropertyChanged(ImprimirMensajeAplicacionDecretoPropertyName);
                    RaisePropertyChanged(IsEnabledAplicacionAlicuotaEspecialPropertyName);
                    RaisePropertyChanged(IsEnableFacturarPorDefectoIvaEspecialPropertyName);
                    RaisePropertyChanged(IsEnableFacturarPorDefectoIvaEspecialPropertyName);
                    RaisePropertyChanged(IsEnabledFechaFinAlicuotaIva10PorcientoPropertyName);
                }
            }
        }


        public bool ImprimirMensajeAplicacionDecreto {
            get {
                return Model.ImprimirMensajeAplicacionDecretoAsBool;
            }
            set {
                if(Model.ImprimirMensajeAplicacionDecretoAsBool != value) {
                    Model.ImprimirMensajeAplicacionDecretoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimirMensajeAplicacionDecretoPropertyName);
                }
            }
        }

        public bool FacturarPorDefectoIvaEspecial {
            get {
                return Model.FacturarPorDefectoIvaEspecialAsBool;
            }
            set {
                if(Model.FacturarPorDefectoIvaEspecialAsBool != value) {
                    Model.FacturarPorDefectoIvaEspecialAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(FacturarPorDefectoIvaEspecialPropertyName);
                }
            }
        }

        public eTipoDeAgrupacionParaLibrosDeVenta TipoDeAgrupacionParaLibrosDeVenta {
            get {
                return Model.TipoDeAgrupacionParaLibrosDeVentaAsEnum;
            }
            set {
                if(Model.TipoDeAgrupacionParaLibrosDeVentaAsEnum != value) {
                    Model.TipoDeAgrupacionParaLibrosDeVentaAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDeAgrupacionParaLibrosDeVentaPropertyName);
                }
            }
        }

        public eTipoNegocio TipoNegocio {
            get {
                return Model.TipoNegocioAsEnum;
            }
            set {
                if(Model.TipoNegocioAsEnum != value) {
                    Model.TipoNegocioAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoNegocioPropertyName);
                }
            }
        }

        public bool VerificarDocumentoSinContabilizar {
            get {
                return Model.VerificarDocumentoSinContabilizarAsBool;
            }
            set {
                if(Model.VerificarDocumentoSinContabilizarAsBool != value) {
                    Model.VerificarDocumentoSinContabilizarAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(VerificarDocumentoSinContabilizarPropertyName);
                }
            }
        }

        public eAplicacionAlicuota AplicacionAlicuotaEspecial {
            get {
                return Model.AplicacionAlicuotaEspecialAsEnum;
            }
            set {
                if(Model.AplicacionAlicuotaEspecialAsEnum != value) {
                    Model.AplicacionAlicuotaEspecialAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(AplicacionAlicuotaEspecialPropertyName);
                }
            }
        }

        public eBaseCalculoParaAlicuotaEspecial BaseDeCalculoParaAlicuotaEspecial {
            get {
                return Model.BaseDeCalculoParaAlicuotaEspecialAsEnum;
            }
            set {
                if(Model.BaseDeCalculoParaAlicuotaEspecialAsEnum != value) {
                    Model.BaseDeCalculoParaAlicuotaEspecialAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(BaseDeCalculoParaAlicuotaEspecialPropertyName);
                }
            }
        }


        [LibCustomValidation("FechaInicioAlicuotaIva10PorcientoValidating")]
        public DateTime FechaInicioAlicuotaIva10Porciento {
            get {
                return Model.FechaInicioAlicuotaIva10Porciento;
            }
            set {
                if(Model.FechaInicioAlicuotaIva10Porciento != value) {
                    Model.FechaInicioAlicuotaIva10Porciento = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaInicioAlicuotaIva10PorcientoPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaFinAlicuotaIva10PorcientoValidating")]
        public DateTime FechaFinAlicuotaIva10Porciento {
            get {
                return Model.FechaFinAlicuotaIva10Porciento;
            }
            set {
                if(Model.FechaFinAlicuotaIva10Porciento != value) {
                    Model.FechaFinAlicuotaIva10Porciento = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaFinAlicuotaIva10PorcientoPropertyName);
                }
            }
        }
        public eTipoDeAgrupacionParaLibrosDeVenta[] ArrayTipoDeAgrupacionParaLibrosDeVenta {
            get {
                return LibEnumHelper<eTipoDeAgrupacionParaLibrosDeVenta>.GetValuesInArray();
            }
        }

        public eTipoNegocio[] ArrayTipoNegocio {
            get {
                return LibEnumHelper<eTipoNegocio>.GetValuesInArray();
            }
        }


        public eAplicacionAlicuota[] ArrayAplicacionAlicuotaEspecial {
            get {
                return LibEnumHelper<eAplicacionAlicuota>.GetValuesInArray();
            }
        }

        public eBaseCalculoParaAlicuotaEspecial[] ArrayBaseCalculoParaAlicuotaEspecial {
            get {
                return LibEnumHelper<eBaseCalculoParaAlicuotaEspecial>.GetValuesInArray();
            }
        }

        public bool IsVisibleFechaContabilizacion {
            get {
                if(LibString.IsNullOrEmpty(AppMemoryInfo.GlobalValuesGetString("Parametros","UsaModuloDeContabilidad"))) {
                    return false;
                } else {
                    return AppMemoryInfo.GlobalValuesGetBool("Parametros","UsaModuloDeContabilidad");
                }
            }
        }

        public bool IsVisibleTipoNegocio {
            get {
                if(LibString.IsNullOrEmpty(AppMemoryInfo.GlobalValuesGetString("Parametros","PuedeUsarTallerMecanico")) || mEsFacturadorBasico) {
                    return false;
                } else {
                    return AppMemoryInfo.GlobalValuesGetBool("Parametros","PuedeUsarTallerMecanico");
                }
            }
        }

        public bool IsVisibleIntegracionRIS {
            get {
                return LibGalac.Aos.DefGen.LibDefGen.ProgramIsInAdvancedWay;
            }
        }

        public bool IsVisibleImprimirVentasDiferidas { //Istayul
            get {
                return true;
            }
        }

        public bool IsEnabledImprimirVentasDiferidas {
            get {
                return IsEnabled && UsarVentasConIvaDiferido;
            }
        }

        public bool IsVisibleFacturarPorDefectoIvaEspecial {
            get {
                return false;
            }
        }

        public bool IsEnabledFacturarPorDefectoIvaEspecial {
            get {
                return IsEnabled && AplicarIVAEspecial;
            }
        }

        public bool IsEnabledAplicacionAlicuotaEspecial {
            get {
                return IsEnabled && AplicarIVAEspecial;
            }
        }

        public bool IsEnabledFechaInicioAlicuotaIva10Porciento {
            get {
                return false;
            }
        }

        public bool IsEnabledFechaFinAlicuotaIva10Porciento {
            get {
                return IsEnabled && AplicarIVAEspecial;
            }
        }

        public bool isVisibleParaPeru {
            get {
                bool vResult = true;
                if(LibDefGen.ProgramInfo.IsCountryPeru()) {
                    vResult = false;
                }
                return vResult;
            }
        }

        public bool IsVisibleVerificarDocumentoSinContabilizar {
            get {
                if(LibString.IsNullOrEmpty(AppMemoryInfo.GlobalValuesGetString("Parametros","UsaModuloDeContabilidad"))) {
                    return false;
                } else {
                    return AppMemoryInfo.GlobalValuesGetBool("Parametros","UsaModuloDeContabilidad");
                }
            }
        }

        public bool IsVisibleFechaDecretoIva {
            get {
                return false;
            }
        } 
        #endregion //Propiedades

        #region Metodos Generados

        protected override void InitializeLookAndFeel(CompaniaStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override CompaniaStt FindCurrentRecord(CompaniaStt valModel) {
            if(valModel == null) {
                return new CompaniaStt();
            }
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInString("AutorellenaResumenDiario", valModel.AutorellenaResumenDiario, 0);
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "DatosGeneralesCompaniaGET", vParams.Get()).FirstOrDefault();

            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<CompaniaStt>,IList<CompaniaStt>> GetBusinessComponent() {
            return null;
        }

        private ValidationResult FechaDeInicioContabilizacionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDeInicioContabilizacion,false,Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram(this.ModuleName + "-> Fecha de Inicio de Contabilizacion"));
                }
                return vResult;
            }
        }

        private ValidationResult FechaMinimaIngresarDatosValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaMinimaIngresarDatos,false,Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram(this.ModuleName + "-> Fecha Minima Ingresar Datos"));
                }
            }
            return vResult;
        }

        private ValidationResult FechaInicioAlicuotaIva10PorcientoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(LibDate.IsEmptyDate(FechaInicioAlicuotaIva10Porciento) && AplicarIVAEspecial) {
                    vResult = new ValidationResult(this.ModuleName + "-> Debe ingresar una fecha de Inicio del decreto 2.602");
                }
            }
            return vResult;
        }

        private ValidationResult FechaFinAlicuotaIva10PorcientoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(LibDate.IsEmptyDate(FechaFinAlicuotaIva10Porciento) && AplicarIVAEspecial) {
                    vResult = new ValidationResult(this.ModuleName + "-> Debe ingresar una fecha de finalización del decreto 2.602");
                } else if(LibDate.F1IsLessThanF2(FechaFinAlicuotaIva10Porciento,FechaInicioAlicuotaIva10Porciento)) {
                    vResult = new ValidationResult(this.ModuleName + "-> La fecha de Finalización del decreto 2.602 no puede ser menor a la fecha de inicio del mismo");
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados



    } //End of class DatosGeneralesCompaniaViewModel

} //End of namespace Galac.Saw.Uil.SttDef

