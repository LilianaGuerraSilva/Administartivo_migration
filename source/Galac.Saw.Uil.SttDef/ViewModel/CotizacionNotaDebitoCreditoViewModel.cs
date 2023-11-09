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

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class CotizacionNotaDebitoCreditoViewModel : LibInputViewModelMfc<NotasDebitoCreditoEntregaStt> {
        #region Constantes
        public const string NombrePlantillaBoletaPropertyName = "NombrePlantillaBoleta";
        public const string NCPreNumeradaPropertyName = "NCPreNumerada";
        public const string NDPreNumeradaPropertyName = "NDPreNumerada";
        public const string NombrePlantillaNotaDeCreditoPropertyName = "NombrePlantillaNotaDeCredito";
        public const string NombrePlantillaNotaDeDebitoPropertyName = "NombrePlantillaNotaDeDebito";
        public const string PrimeraNotaDeCreditoPropertyName = "PrimeraNotaDeCredito";
        public const string PrimeraNotaDeDebitoPropertyName = "PrimeraNotaDeDebito";
        public const string PrimeraBoletaPropertyName = "PrimeraBoleta";
        public const string PrefijoNCPropertyName = "PrefijoNC";
        public const string PrefijoNDPropertyName = "PrefijoND";
        public const string TipoDePrefijoNCPropertyName = "TipoDePrefijoNC";
        public const string TipoDePrefijoNDPropertyName = "TipoDePrefijoND";
        public const string IsEnabledNCPreNumeradaPropertyName = "IsEnabledNCPreNumerada";
        public const string IsEnabledNDPreNumeradaPropertyName = "IsEnabledNDPreNumerada";
        public const string IsEnabledPrefijoNCPropertyName = "IsEnabledPrefijoNC";
        public const string IsEnabledPrefijoNDPropertyName = "IsEnabledPrefijoND";
        private const string IsEnabledUsaImprentaDigitalPropertyName = "IsEnabledUsaImprentaDigital";
        #endregion
        #region Variables
        private bool _IsEnabledUsaImprentaDigital;
        #endregion //Variables
        #region Propiedades
      
        public override string ModuleName {
            get { return "3.2.- Nota de Débito / Crédito"; }
        }

        [LibCustomValidation("NombrePlantillaBoletaValidating")]
        public string  NombrePlantillaBoleta {
            get {
                return Model.NombrePlantillaBoleta;
            }
            set {
                if (Model.NombrePlantillaBoleta != value) {
                    Model.NombrePlantillaBoleta = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombrePlantillaBoletaPropertyName);
                }
            }
        }

        public bool  NCPreNumerada {
            get {
                return Model.NCPreNumeradaAsBool;
            }
            set {
                if (Model.NCPreNumeradaAsBool != value) {
                    Model.NCPreNumeradaAsBool = value;
                   if (value ) {
                      TipoDePrefijoNC  = eTipoDePrefijo.SinPrefijo;
                      }
                    IsDirty = true;
                    RaisePropertyChanged(NCPreNumeradaPropertyName);
                    RaisePropertyChanged(IsEnabledNCPreNumeradaPropertyName);
                }
            }
        }

        public bool  NDPreNumerada {
            get {
                return Model.NDPreNumeradaAsBool;
            }
            set {
                if (Model.NDPreNumeradaAsBool != value) {
                    Model.NDPreNumeradaAsBool = value;
                    if(value) {
                       TipoDePrefijoND = eTipoDePrefijo.SinPrefijo;
                    }
                    IsDirty = true;
                    RaisePropertyChanged(NDPreNumeradaPropertyName);
                    RaisePropertyChanged(IsEnabledNDPreNumeradaPropertyName);
                }
            }
        }

        [LibCustomValidation("NombrePlantillaNotaDeCreditoValidating")]
        public string  NombrePlantillaNotaDeCredito {
            get {
                return Model.NombrePlantillaNotaDeCredito;
            }
            set {
                if (Model.NombrePlantillaNotaDeCredito != value) {
                    Model.NombrePlantillaNotaDeCredito = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombrePlantillaNotaDeCreditoPropertyName);
                }
            }
        }

        [LibCustomValidation("NombrePlantillaNotaDeDebitoValidating")]
        public string  NombrePlantillaNotaDeDebito {
            get {
                return Model.NombrePlantillaNotaDeDebito;
            }
            set {
                if (Model.NombrePlantillaNotaDeDebito != value) {
                    Model.NombrePlantillaNotaDeDebito = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombrePlantillaNotaDeDebitoPropertyName);
                }
            }
        }

        [LibCustomValidation("EsValidaPrimeraNotaDeCreditoValidating")]
        public string PrimeraNotaDeCredito {
            get {
                return Model.PrimeraNotaDeCredito;
            }
            set {
                if(Model.PrimeraNotaDeCredito != value) {
                    Model.PrimeraNotaDeCredito = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrimeraNotaDeCreditoPropertyName);
                }
            }
        }

        [LibCustomValidation("EsValidaPrimeraNotaDeDebitoValidating")]
        public string  PrimeraNotaDeDebito {
            get {
                return Model.PrimeraNotaDeDebito;
            }
            set {
                if(Model.PrimeraNotaDeDebito != value) {
                    Model.PrimeraNotaDeDebito = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrimeraNotaDeDebitoPropertyName);
                }
            }
        }

        [LibCustomValidation("EsValidaPrimeraBoletaValidating")]
        public string PrimeraBoleta {
            get {
                return Model.PrimeraBoleta;
            }
            set {
                if(Model.PrimeraBoleta != value) {
                    Model.PrimeraBoleta = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrimeraBoletaPropertyName);
                }
            }
        }
       [LibCustomValidation("PrefijoNCValidating")]
        public string  PrefijoNC {
            get {
                return Model.PrefijoNC;
            }
            set {
                if (Model.PrefijoNC != value) {
                    Model.PrefijoNC = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrefijoNCPropertyName);
                }
            }
        }

       [LibCustomValidation("PrefijoNDValidating")]
        public string PrefijoND {
            get {
                return Model.PrefijoND;
            }
            set {
                if(Model.PrefijoND != value) {
                    Model.PrefijoND = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrefijoNDPropertyName);
                }
            }
        }

        public eTipoDePrefijo TipoDePrefijoNC {
            get {
                return Model.TipoDePrefijoNCAsEnum;
            }
            set {
                if (Model.TipoDePrefijoNCAsEnum != value) {
                    Model.TipoDePrefijoNCAsEnum = value;
                    if(value != eTipoDePrefijo.Indicar) {
                       PrefijoNC = "";
                    }
                    IsDirty = true;
                    RaisePropertyChanged(TipoDePrefijoNCPropertyName);
                    RaisePropertyChanged(IsEnabledPrefijoNCPropertyName);
                }
            }
        }

        public eTipoDePrefijo TipoDePrefijoND {
            get {
                return Model.TipoDePrefijoNDAsEnum;
            }
            set {
                if(Model.TipoDePrefijoNDAsEnum != value) {
                    Model.TipoDePrefijoNDAsEnum = value;
                    if(value != eTipoDePrefijo.Indicar) {
                       PrefijoND = "";
                    }
                    IsDirty = true;
                    RaisePropertyChanged(TipoDePrefijoNDPropertyName);
                    RaisePropertyChanged(IsEnabledPrefijoNDPropertyName);
                }
            }
        }

        public eTipoDePrefijo[] ArrayTipoDePrefijo {
            get {
                return LibEnumHelper<eTipoDePrefijo>.GetValuesInArray();
            }
        }

        public RelayCommand ChooseTemplateCommandNC {
            get;
            private set;
        }

        public RelayCommand ChooseTemplateCommandND {
            get;
            private set;
        }

        public RelayCommand ChooseTemplateCommandBoleta {
            get;
            private set;
        }

        public bool IsEnabledNCPreNumerada {
            get {
                return IsEnabled && !NCPreNumerada;
            }
        }

        public bool IsEnabledNDPreNumerada {
            get {
                return IsEnabled && !NDPreNumerada;
            }
        }
        
        public bool IsEnabledPrefijoNC {
            get {
                return IsEnabled && TipoDePrefijoNC == eTipoDePrefijo.Indicar;
            }
        }

        public bool IsEnabledPrefijoND {
            get {
                return IsEnabled && TipoDePrefijoND == eTipoDePrefijo.Indicar;
            }
        }

        public bool IsEnabledUsaImprentaDigital {
            get { return (_IsEnabledUsaImprentaDigital || UsaImprentaDigital()); }
            set {
                if (_IsEnabledUsaImprentaDigital != value) {
                    _IsEnabledUsaImprentaDigital = value;
                    RaisePropertyChanged(IsEnabledUsaImprentaDigitalPropertyName);
                }
            }
        }

        public bool IsNotEnabledUsaImprentaDigital {
            get {
                return IsEnabled && !IsEnabledUsaImprentaDigital;
            }
        }
        #endregion //Propiedades
        #region Constructores
        public CotizacionNotaDebitoCreditoViewModel()
            : this(new NotasDebitoCreditoEntregaStt(), eAccionSR.Insertar) {
        }
        public CotizacionNotaDebitoCreditoViewModel(NotasDebitoCreditoEntregaStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = NombrePlantillaBoletaPropertyName;
            LibMessages.Notification.Register<bool>(this, OnUsaImprentaDigitalChanged);
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeCommands() {
            ChooseTemplateCommandNC = new RelayCommand(ExecuteBuscarPlantillaCommandNC);
            ChooseTemplateCommandND = new RelayCommand(ExecuteBuscarPlantillaCommandND);
            ChooseTemplateCommandBoleta = new RelayCommand(ExecuteBuscarPlantillaCommandBoleta);
        }

        protected override void InitializeLookAndFeel(NotasDebitoCreditoEntregaStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override NotasDebitoCreditoEntregaStt FindCurrentRecord(NotasDebitoCreditoEntregaStt valModel) {
            if (valModel == null) {
                return new NotasDebitoCreditoEntregaStt();
            }
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInString("NombrePlantillaBoleta", valModel.NombrePlantillaBoleta, 50);
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "CotizacionNotaDebitoCreditoGET", vParams.Get()).FirstOrDefault();
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<NotasDebitoCreditoEntregaStt>, IList<NotasDebitoCreditoEntregaStt>> GetBusinessComponent() {
            return null;
        }
        
        private void ExecuteBuscarPlantillaCommandNC() {
            try {
                NombrePlantillaNotaDeCredito = new clsUtilParameters().BuscarNombrePlantilla("rpx de Nota de Crédito (*.rpx)|*Nota*Credito*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        
        private void ExecuteBuscarPlantillaCommandND() {
            try {
                NombrePlantillaNotaDeDebito = new clsUtilParameters().BuscarNombrePlantilla("rpx de Nota de Débito (*.rpx)|*Nota*Debito*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
       
        private void ExecuteBuscarPlantillaCommandBoleta() {
            try {
                NombrePlantillaBoleta = new clsUtilParameters().BuscarNombrePlantilla("rpx de Boleta (*.rpx)|*Boleta*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        
        private ValidationResult EsValidaPrimeraNotaDeCreditoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                string outPrimeraNotaCredito = string.Empty;
                outPrimeraNotaCredito = ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).GeneraPriemraNotaDeCredito(Mfc.GetInt("Compania"), LibConvert.ToInt(Model.PrimeraNotaDeCredito));
                outPrimeraNotaCredito = LibText.Mid(outPrimeraNotaCredito, LibText.InStr(outPrimeraNotaCredito, "-") + 1);
                if(LibConvert.ToInt(Model.PrimeraNotaDeCredito) < LibConvert.ToInt(outPrimeraNotaCredito)) {
                    outPrimeraNotaCredito = LibText.FillWithCharToLeft(outPrimeraNotaCredito, "0", 11);
                    vResult = new ValidationResult(string.Format(this.ModuleName + "-> El número de inicial de la Nota de Crédito que se está indicando podría generar inconsistencias. \n Se recomienda colocar el número: {0}", outPrimeraNotaCredito));
                }
            }
            return vResult;
        }

        private ValidationResult EsValidaPrimeraNotaDeDebitoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                string outPrimeraNotaDebito = string.Empty;
                outPrimeraNotaDebito = ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).GeneraPriemraNotaDeDebito(Mfc.GetInt("Compania"), LibConvert.ToInt(Model.PrimeraNotaDeDebito));
                outPrimeraNotaDebito = LibText.Mid(outPrimeraNotaDebito, LibText.InStr(outPrimeraNotaDebito, "-") + 1);
                if(LibConvert.ToInt(Model.PrimeraNotaDeDebito) < LibConvert.ToInt(outPrimeraNotaDebito)) {
                    outPrimeraNotaDebito = LibText.FillWithCharToLeft(outPrimeraNotaDebito, "0", 11);
                    vResult = new ValidationResult(string.Format(this.ModuleName + "-> El número de inicial de la Nota de Débito que se está indicando podría generar inconsistencias. \n Se recomienda colocar el número: {0}", outPrimeraNotaDebito));
                }
            }
            return vResult;
        }

        private ValidationResult EsValidaPrimeraBoletaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
               if(!LibString.IsNullOrEmpty(Model.PrimeraBoleta)) {
                  string outPrimeraBoleta = string.Empty;
                  outPrimeraBoleta = ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).GeneraPriemraBoleta(Mfc.GetInt("Compania"), LibConvert.ToInt(Model.PrimeraBoleta));
                  outPrimeraBoleta = LibText.Mid(outPrimeraBoleta, LibText.InStr(outPrimeraBoleta, "-") + 1);
                  if(LibConvert.ToInt(Model.PrimeraBoleta) < LibConvert.ToInt(outPrimeraBoleta)) {
                     outPrimeraBoleta = LibText.FillWithCharToLeft(outPrimeraBoleta, "0", 11);
                     vResult = new ValidationResult(string.Format(this.ModuleName + "-> El número de inicial de la Boleta que se está indicando podría generar inconsistencias. \n Se recomienda colocar el número: {0}", outPrimeraBoleta));
                  }
                  if(LibString.IsNullOrEmpty(Model.PrimeraBoleta)) {
                     PrimeraBoleta = LibText.FillWithCharToLeft("1", "0", 11);
                  }
               }
            }
            return vResult;
        }

        private ValidationResult NombrePlantillaBoletaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(!LibString.IsNullOrEmpty(NombrePlantillaBoleta) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaBoleta)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaBoleta + ", en " + this.ModuleName + ", no EXISTE.");
                }
            }
            return vResult;
        }

        private ValidationResult NombrePlantillaNotaDeCreditoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(!LibString.IsNullOrEmpty(NombrePlantillaNotaDeCredito) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaNotaDeCredito)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaNotaDeCredito + ", en " + this.ModuleName + ", no EXISTE.");
                } else if(LibString.IsNullOrEmpty(NombrePlantillaNotaDeCredito)) {
                    vResult = new ValidationResult(this.ModuleName + "-> Plantilla de Impresión Nota de Crédito es requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult NombrePlantillaNotaDeDebitoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(!LibString.IsNullOrEmpty(NombrePlantillaNotaDeDebito) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaNotaDeDebito)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaNotaDeDebito + ", en " + this.ModuleName + ", no EXISTE.");
                } else if(LibString.IsNullOrEmpty(NombrePlantillaNotaDeDebito)) {
                    vResult = new ValidationResult(this.ModuleName + "-> Plantilla de Impresión Nota de Débito es requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult PrefijoNCValidating() {
           ValidationResult vResult = ValidationResult.Success;
           if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
              return ValidationResult.Success;
           } else {
              
              if(PrefijoNC.Length > 5 ) {
                 vResult = new ValidationResult(string.Format(this.ModuleName + "-> El Prefijo de Nota de Crédito solo admite hasta 5 carácteres."));
              }
           }
           return vResult;
        }

        private ValidationResult PrefijoNDValidating() {
           ValidationResult vResult = ValidationResult.Success;
           if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
              return ValidationResult.Success;
           } else {

              if(PrefijoND.Length > 5) {
                 vResult = new ValidationResult(string.Format(this.ModuleName + "-> El Prefijo de Nota de Débito solo admite hasta 5 carácteres."));
              }
           }
           return vResult;
        }

        private void OnUsaImprentaDigitalChanged(NotificationMessage<bool> valMessage) {
            if (LibString.S1IsEqualToS2(valMessage.Notification, "UsaImprentaDigital")) {
                IsEnabledUsaImprentaDigital = valMessage.Content || UsaImprentaDigital();
            }
        }

        private bool UsaImprentaDigital() {
            return LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaImprentaDigital"));
        }
        #endregion //Metodos Generados

    } //End of class CotizacionNotaDebitoCreditoViewModel

} //End of namespace Galac.Saw.Uil.SttDef

