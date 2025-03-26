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
    public class FacturaModeloFacturaViewModel : LibInputViewModelMfc<ModeloDeFacturaStt> {
        #region Constantes
        public const string UsarDosTalonariosPropertyName = "UsarDosTalonarios";
        public const string FacturaPreNumeradaPropertyName = "FacturaPreNumerada";
        public const string TipoDePrefijoPropertyName = "TipoDePrefijo";
        public const string TipoDePrefijo2PropertyName = "TipoDePrefijo2";
        public const string NombrePlantillaFacturaPropertyName = "NombrePlantillaFactura";
        public const string NombrePlantillaFactura2PropertyName = "NombrePlantillaFactura2";
        public const string PrefijoPropertyName = "Prefijo";
        public const string Prefijo2PropertyName = "Prefijo2";
        public const string PrimeraFacturaPropertyName = "PrimeraFactura";
        public const string PrimeraFactura2PropertyName = "PrimeraFactura2";
        public const string ModeloDeFacturaPropertyName = "ModeloDeFactura";
        public const string ModeloDeFactura2PropertyName = "ModeloDeFactura2";
        public const string ModeloFacturaModoTextoPropertyName = "ModeloFacturaModoTexto";
        public const string ModeloFacturaModoTexto2PropertyName = "ModeloFacturaModoTexto2";
        public const string FacturaPreNumerada2PropertyName = "FacturaPreNumerada2";
        public const string IsEnabledPlantillaFacturaPropertyName = "IsEnabledPlantillaFactura";
        public const string IsEnabledPlantillaFactura2PropertyName = "IsEnabledPlantillaFactura2";
        public const string IsVisibleModeloFacturaModoTextoPropertyName = "IsVisibleModeloFacturaModoTexto";
        public const string IsVisibleModeloFacturaModoTexto2PropertyName = "IsVisibleModeloFacturaModoTexto2";
        public const string IsEnabledPrimeraFacturaPropertyName = "IsEnabledPrimeraFactura";
        public const string IsEnabledPrimeraFactura2PropertyName = "IsEnabledPrimeraFactura2";
        public const string IsEnabledTipoPrefijoPropertyName = "IsEnabledTipoPrefijo";
        public const string IsEnabledTipoPrefijo2PropertyName = "IsEnabledTipoPrefijo2";
        public const string IsEnabledPrefijoPropertyName = "IsEnabledPrefijo";
        public const string IsEnabledPrefijo2PropertyName = "IsEnabledPrefijo2";
        public const string IsVisibleModeloOtorsCargosyDescuentosPropertyName = "IsVisibleUsarOtrosCargoDeFactura";
        public const string NombrePlantillaSubFacturaConOtrosCargosPropertyName = "NombrePlantillaSubFacturaConOtrosCargos";
        public const string IsEnabledPlantillaFacturaOyDPropertyName = "IsEnabledPlantillaFacturaOyD";
        #endregion
        #region Variables
        bool mEsFacturadorBasico;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "2.4.- Modelo de Factura"; }
        }

        public bool UsarDosTalonarios {
            get {
                return Model.UsarDosTalonariosAsBool;
            }
            set {
                if (Model.UsarDosTalonariosAsBool != value) {
                    Model.UsarDosTalonariosAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsarDosTalonariosPropertyName);
                }
            }
        }

        public bool FacturaPreNumerada {
            get {
                return Model.FacturaPreNumeradaAsBool;
            }
            set {
                if (Model.FacturaPreNumeradaAsBool != value) {
                    Model.FacturaPreNumeradaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(FacturaPreNumeradaPropertyName);
                    RaisePropertyChanged(IsEnabledPrimeraFacturaPropertyName);
                    RaisePropertyChanged(IsEnabledTipoPrefijoPropertyName);
                }
            }
        }

        public eTipoDePrefijo TipoDePrefijo {
            get {
                return Model.TipoDePrefijoAsEnum;
            }
            set {
                if (Model.TipoDePrefijoAsEnum != value) {
                    Model.TipoDePrefijoAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDePrefijoPropertyName);
                    RaisePropertyChanged(IsEnabledPrefijoPropertyName);
                }
            }
        }

        public eTipoDePrefijo TipoDePrefijo2 {
            get {
                return Model.TipoDePrefijo2AsEnum;
            }
            set {
                if (Model.TipoDePrefijo2AsEnum != value) {
                    Model.TipoDePrefijo2AsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDePrefijo2PropertyName);
                    RaisePropertyChanged(IsEnabledPrefijo2PropertyName);
                }
            }
        }

        [LibCustomValidation("NombrePlantillaFacturaValidating")]
        public string NombrePlantillaFactura {
            get {
                return Model.NombrePlantillaFactura;
            }
            set {
                if (Model.NombrePlantillaFactura != value) {
                    Model.NombrePlantillaFactura = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombrePlantillaFacturaPropertyName);
                }
            }
        }

        [LibCustomValidation("NombrePlantillaFactura2Validating")]
        public string NombrePlantillaFactura2 {
            get {
                return Model.NombrePlantillaFactura2;
            }
            set {
                if (Model.NombrePlantillaFactura2 != value) {
                    Model.NombrePlantillaFactura2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombrePlantillaFactura2PropertyName);
                }
            }
        }
        [LibCustomValidation("NombrePlantillaFacturaOyDValidating")]
        public string NombrePlantillaSubFacturaConOtrosCargos {
            get {
                return Model.NombrePlantillaSubFacturaConOtrosCargos;
            }
            set {
                if (Model.NombrePlantillaSubFacturaConOtrosCargos != value) {
                    Model.NombrePlantillaSubFacturaConOtrosCargos = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombrePlantillaSubFacturaConOtrosCargosPropertyName);

                }
            }
        }

        public string Prefijo {
            get {
                return Model.Prefijo;
            }
            set {
                if (Model.Prefijo != value) {
                    Model.Prefijo = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrefijoPropertyName);
                }
            }
        }

        public string Prefijo2 {
            get {
                return Model.Prefijo2;
            }
            set {
                if (Model.Prefijo2 != value) {
                    Model.Prefijo2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(Prefijo2PropertyName);
                }
            }
        }

        public string PrimeraFactura {
            get {
                return Model.PrimeraFactura;
            }
            set {
                if (Model.PrimeraFactura != value) {
                    Model.PrimeraFactura = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrimeraFacturaPropertyName);
                }
            }
        }

        public string PrimeraFactura2 {
            get {
                return Model.PrimeraFactura2;
            }
            set {
                if (Model.PrimeraFactura2 != value) {
                    Model.PrimeraFactura2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrimeraFactura2PropertyName);
                }
            }
        }

        public eModeloDeFactura ModeloDeFactura {
            get {
                return Model.ModeloDeFacturaAsEnum;
            }
            set {
                if (Model.ModeloDeFacturaAsEnum != value) {
                    Model.ModeloDeFacturaAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ModeloDeFacturaPropertyName);
                    RaisePropertyChanged(IsEnabledPlantillaFacturaPropertyName);
                    RaisePropertyChanged(IsVisibleModeloFacturaModoTextoPropertyName);
                }
            }
        }

        public eModeloDeFactura ModeloDeFactura2 {
            get {
                return Model.ModeloDeFactura2AsEnum;
            }
            set {
                if (Model.ModeloDeFactura2AsEnum != value) {
                    Model.ModeloDeFactura2AsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ModeloDeFactura2PropertyName);
                    RaisePropertyChanged(IsEnabledPlantillaFactura2PropertyName);
                    RaisePropertyChanged(IsVisibleModeloFacturaModoTexto2PropertyName);
                }
            }
        }

        public string ModeloFacturaModoTexto {
            get {
                return Model.ModeloFacturaModoTexto;
            }
            set {
                if (Model.ModeloFacturaModoTexto != value) {
                    Model.ModeloFacturaModoTexto = value;
                    IsDirty = true;
                    RaisePropertyChanged(ModeloFacturaModoTextoPropertyName);
                }
            }
        }

        public string ModeloFacturaModoTexto2 {
            get {
                return Model.ModeloFacturaModoTexto2;
            }
            set {
                if (Model.ModeloFacturaModoTexto2 != value) {
                    Model.ModeloFacturaModoTexto2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(ModeloFacturaModoTexto2PropertyName);
                }
            }
        }

        public bool FacturaPreNumerada2 {
            get {
                return Model.FacturaPreNumerada2AsBool;
            }
            set {
                if (Model.FacturaPreNumerada2AsBool != value) {
                    Model.FacturaPreNumerada2AsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(FacturaPreNumerada2PropertyName);
                    RaisePropertyChanged(IsEnabledPrimeraFactura2PropertyName);
                    RaisePropertyChanged(IsEnabledTipoPrefijo2PropertyName);
                }
            }
        }

        public eTipoDePrefijo[] ArrayTipoDePrefijo {
            get {
                return LibEnumHelper<eTipoDePrefijo>.GetValuesInArray();
            }
        }

        public eTipoDePrefijo[] ArrayTipoDePrefijo2 {
            get {
                return LibEnumHelper<eTipoDePrefijo>.GetValuesInArray();
            }
        }

        public eModeloDeFactura[] ArrayModeloDeFactura {
            get {
                return LibEnumHelper<eModeloDeFactura>.GetValuesInArray();
            }
        }

        public eModeloDeFactura[] ArrayModeloDeFactura2 {
            get {
                return LibEnumHelper<eModeloDeFactura>.GetValuesInArray();
            }
        }

        public RelayCommand ChooseTemplateCommandTalonario1 {
            get;
            private set;
        }

        public RelayCommand ChooseTemplateCommandTalonario2 {
            get;
            private set;
        }

        public RelayCommand ChooseTemplateCommandOyD {
            get;
            private set;
        }

        public bool IsEnabledTalonario2 {
            get {
                return (IsEnabled && UsarDosTalonarios) && !UsaImprentaDigital();
            }
        }

        public bool IsEnabledPlantillaFactura {
            get {
                return (ModeloDeFactura == eModeloDeFactura.eMD_OTRO && IsEnabled) && !UsaImprentaDigital();
            }
        }

        public bool IsEnabledPlantillaFactura2 {
            get {
                return (ModeloDeFactura2 == eModeloDeFactura.eMD_OTRO && IsEnabled) && !UsaImprentaDigital();
            }
        }

        public bool IsEnabledPlantillaFacturaOyD {
            get{                
                    return (Action != eAccionSR.Consultar) && !UsaImprentaDigital();        
            }
        }
        public bool IsEnabledTipoPrefijo {
            get {
                return IsEnabled && !FacturaPreNumerada && !UsaImprentaDigital();
            }
        }

        public string[] ArrayModeloFacturaModoTexto {
            get {
                return new clsUtilParameters().GetModelosPlanillasList().ToArray();
            }
        }

        public bool IsVisibleModeloFacturaModoTexto {
            get {
                return ModeloDeFactura == eModeloDeFactura.eMD_IMPRESION_MODO_TEXTO;
            }
        }

        public bool IsVisibleModeloFacturaModoTexto2 {
            get {
                return ModeloDeFactura2 == eModeloDeFactura.eMD_IMPRESION_MODO_TEXTO;
            }
        }

        public bool IsEnabledPrimeraFactura {
            get {
                return IsEnabled && !FacturaPreNumerada && !UsaImprentaDigital();
            }
        }

        public bool IsEnabledPrimeraFactura2 {
            get {
                return IsEnabled && !FacturaPreNumerada2 && UsarDosTalonarios && !UsaImprentaDigital();
            }
        }

        public bool IsEnabledTipoPrefijo2 {
            get {
                return IsEnabled && !FacturaPreNumerada2 && UsarDosTalonarios && !UsaImprentaDigital();
            }
        }

        public bool IsEnabledPrefijo {
            get {
                return IsEnabled && TipoDePrefijo == eTipoDePrefijo.Indicar && !FacturaPreNumerada && !UsaImprentaDigital();
            }
        }

        public bool IsEnabledPrefijo2 {
            get {
                return IsEnabled && (TipoDePrefijo2 == eTipoDePrefijo.Indicar && !FacturaPreNumerada2) && !UsaImprentaDigital();
            }
        }
        public bool IsEnabledUsaDosTalonarios {
            get {
                return IsEnabled && !UsaImprentaDigital();
            }
        }
		
        public bool IsVisibleUsarOtrosCargoDeFactura {
            get {
				return !mEsFacturadorBasico;
				}
        }
        public bool UsaOtrosCyD { 
            get {
                return true;
            }
        }

        public bool IsEnabledModeloDeFactura {
            get {
                return !UsaImprentaDigital();
            }
        }

        public bool IsEnabledModeloDeFacturaModoTexto {
            get {
                return !UsaImprentaDigital();
            }
        }

        public bool IsEnabledOtrosCargoDeFactura {
            get {
                return !UsaImprentaDigital();
            }
        }

        public bool IsEnabledModeloDeFactura2 {
            get {
                return !UsaImprentaDigital();
            }
        }

        public bool IsEnabledFacturaPreNumerada2 {
            get {
                return !UsaImprentaDigital();
            }
        }

        public bool IsEnabledFacturaPreNumerada {
            get {
                return !UsaImprentaDigital();
            }
        }

        public bool IsEnabledModeloDeFacturaModoTexto2 {
            get {
                return !UsaImprentaDigital();
            }
        }

        public bool IsVisibleNombrePlantillaFacturaOyD {
            get {
                return !mEsFacturadorBasico;
            }
        }
        #endregion //Propiedades
        #region Constructores
        public FacturaModeloFacturaViewModel()
            : this(new ModeloDeFacturaStt(), eAccionSR.Insertar) {
        }
        public FacturaModeloFacturaViewModel(ModeloDeFacturaStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = UsarDosTalonariosPropertyName;
            mEsFacturadorBasico = new clsLibSaw().EsFacturadorBasico();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeCommands() {
            ChooseTemplateCommandTalonario1 = new RelayCommand(ExecuteBuscarPlantillaCommandTalonario1);
            ChooseTemplateCommandTalonario2 = new RelayCommand(ExecuteBuscarPlantillaCommandTalonario2);
            ChooseTemplateCommandOyD = new RelayCommand(ExecuteBuscarPlantillaCommandOyD);
        }

        protected override void InitializeLookAndFeel(ModeloDeFacturaStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ModeloDeFacturaStt FindCurrentRecord(ModeloDeFacturaStt valModel) {
            if (valModel == null) {
                return new ModeloDeFacturaStt();
            }
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<ModeloDeFacturaStt>, IList<ModeloDeFacturaStt>> GetBusinessComponent() {
            return null;
        }

        private void ExecuteBuscarPlantillaCommandTalonario1() {
            try {
                NombrePlantillaFactura = new clsUtilParameters().BuscarNombrePlantilla("rpx de Plantilla Factura (*.rpx)|*Factura*.rpx");
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteBuscarPlantillaCommandTalonario2() {
            try {
                NombrePlantillaFactura2 = new clsUtilParameters().BuscarNombrePlantilla("rpx de Plantilla Factura (*.rpx)|*Factura*.rpx");
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        private void ExecuteBuscarPlantillaCommandOyD() {
            try {
                NombrePlantillaSubFacturaConOtrosCargos = new clsUtilParameters().BuscarNombrePlantilla("rpx de Plantilla Factura (*.rpx)|*SubFactura*.rpx");
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        public List<string> GetModelosPlanillasList() {
            List<string> vResult = new List<string>();
            List<string> vListModelosPlantillas = new List<string>();
            LibXmlDataParse insDataParse = new LibXmlDataParse(AppMemoryInfo);
            string[] vModelosPlanillasArray;
            string vModelosPlanillas = insDataParse.GetString("Parametros", 0, "ModelosPlanillas", "");
            vModelosPlanillasArray = LibString.Split(vModelosPlanillas, ',');
            for (int i = 0; i < vModelosPlanillasArray.Length; i += 1) {
                vListModelosPlantillas.Add(vModelosPlanillasArray[i]);
            }
            vResult = vListModelosPlantillas;
            return vResult;
        }

        private ValidationResult NombrePlantillaFacturaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (!LibString.IsNullOrEmpty(NombrePlantillaFactura) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaFactura)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaFactura + ", en " + this.ModuleName + ", no EXISTE.");
                } else if (LibString.IsNullOrEmpty(NombrePlantillaFactura) && ModeloDeFactura == eModeloDeFactura.eMD_OTRO) {
                    vResult = new ValidationResult(this.ModuleName + "-> Nombre Plantilla Factura es requerido.");
                }
            }
            return vResult;
        }


        private ValidationResult NombrePlantillaFactura2Validating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (UsarDosTalonarios && ModeloDeFactura2 == eModeloDeFactura.eMD_OTRO) {
                    if (!LibString.IsNullOrEmpty(NombrePlantillaFactura2) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaFactura2)) {
                        vResult = new ValidationResult("El RPX " + NombrePlantillaFactura2 + ", en " + this.ModuleName + ", no EXISTE.");
                    } else if (LibString.IsNullOrEmpty(NombrePlantillaFactura2) && ModeloDeFactura2 == eModeloDeFactura.eMD_OTRO) {
                        vResult = new ValidationResult(this.ModuleName + "-> Nombre Plantilla Factura2 es requerido.");
                    }
                }
            }
            return vResult;
        }
        private ValidationResult NombrePlantillaFacturaOyDValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (!LibString.IsNullOrEmpty(NombrePlantillaSubFacturaConOtrosCargos) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaSubFacturaConOtrosCargos)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaSubFacturaConOtrosCargos + ", en " + this.ModuleName + ", no EXISTE.");
                } else if (LibString.IsNullOrEmpty(NombrePlantillaSubFacturaConOtrosCargos)) {
                    NombrePlantillaSubFacturaConOtrosCargos = "rpxSubFacturaConOtrosCargos";
                    vResult = ValidationResult.Success;
                }
            }
            return vResult;
        }

        private bool UsaImprentaDigital() {
            return LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaImprentaDigital"));
        }
        #endregion //Metodos Generados
    } //End of class FacturaModeloFacturaViewModel
} //End of namespace Galac.Saw.Uil.SttDef

