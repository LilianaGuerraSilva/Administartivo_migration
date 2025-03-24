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
    public class CxPComprasCxCProveedorViewModel : LibInputViewModelMfc<CxPProveedorPagosStt> {
        #region Constantes
        public const string NombrePlantillaComprobanteDePagoPropertyName = "NombrePlantillaComprobanteDePago";
        public const string OrdenarCxPPorFacturaDocumentoPropertyName = "OrdenarCxPPorFacturaDocumento";
        public const string NombrePlantillaRetencionImpuestoMunicipalPropertyName = "NombrePlantillaRetencionImpuestoMunicipal";
        public const string NumCopiasComprobantepagoPropertyName = "NumCopiasComprobantepago";
        public const string PrimerNumeroComprobanteRetImpuestoMunicipalPropertyName = "PrimerNumeroComprobanteRetImpuestoMunicipal";
        public const string RetieneImpuestoMunicipalPropertyName = "RetieneImpuestoMunicipal";
        public const string TipoDeOrdenDePagoAImprimirPropertyName = "TipoDeOrdenDePagoAImprimir";
        public const string UsarCodigoProveedorEnPantallaPropertyName = "UsarCodigoProveedorEnPantalla";
        public const string LongitudCodigoProveedorPropertyName = "LongitudCodigoProveedor";
        public const string ImprimirComprobanteContableDePagoPropertyName = "ImprimirComprobanteContableDePago";
        public const string NoImprimirComprobanteDePagoPropertyName = "NoImprimirComprobanteDePago";
        public const string AvisarSiProveedorTieneAnticiposPropertyName = "AvisarSiProveedorTieneAnticipos";
        public const string ExigirInformacionLibroDeComprasPropertyName = "ExigirInformacionLibroDeCompras";
        public const string ConfirmarImpresionPorSeccionesPropertyName = "ConfirmarImpresionPorSecciones";
        public const string ConceptoBancarioReversoDePagoPropertyName = "ConceptoBancarioReversoDePago";
        public const string NombrePlantillaRetencionImpuestoMunicipalInformePropertyName = "NombrePlantillaRetencionImpuestoMunicipalInforme";
        public const string FechaSugeridaRetencionesCxPPropertyName = "FechaSugeridaRetencionesCxP";
        #endregion
        #region Variables
        private FkConceptoBancarioViewModel _ConexionConceptoBancarioReversoDePago = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "6.2.- CxP / Proveedor / Pagos"; }
        }

        [LibCustomValidation("NombrePlantillaComprobanteDePagoValidating")]
        public string NombrePlantillaComprobanteDePago {
            get {
                return Model.NombrePlantillaComprobanteDePago;
            }
            set {
                if (Model.NombrePlantillaComprobanteDePago != value) {
                    Model.NombrePlantillaComprobanteDePago = value;
                    IsDirty = true;
                    if (LibString.IsNullOrEmpty(NombrePlantillaComprobanteDePago)) {
                        ExecuteBuscarPlantillaCommandOrdenPago();
                    }
                    RaisePropertyChanged(NombrePlantillaComprobanteDePagoPropertyName);
                }
            }
        }

       [LibCustomValidation("NombrePlantillaRetencionImpuestoMunicipalInformeValidating")]
        public string NombrePlantillaRetencionImpuestoMunicipalInforme {
           get {
              return Model.NombrePlantillaRetencionImpuestoMunicipalInforme;
           }
           set {
              if (Model.NombrePlantillaRetencionImpuestoMunicipalInforme != value) {
                 Model.NombrePlantillaRetencionImpuestoMunicipalInforme = value;
                 IsDirty = true;
                 if (LibString.IsNullOrEmpty(NombrePlantillaRetencionImpuestoMunicipalInforme)) {
                    ExecuteBuscarPlantillaCommandRetencionImpuestoMunicipalInforme();
                 }
                 RaisePropertyChanged(NombrePlantillaRetencionImpuestoMunicipalInformePropertyName);
              }
           }
        }

        public bool OrdenarCxPPorFacturaDocumento {
            get {
                return Model.OrdenarCxPPorFacturaDocumentoAsBool;
            }
            set {
                if (Model.OrdenarCxPPorFacturaDocumentoAsBool != value) {
                    Model.OrdenarCxPPorFacturaDocumentoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(OrdenarCxPPorFacturaDocumentoPropertyName);
                }
            }
        }

        [LibCustomValidation("NombrePlantillaRetencionImpuestoMunicipalValidating")]
        public string NombrePlantillaRetencionImpuestoMunicipal {
            get {
                return Model.NombrePlantillaRetencionImpuestoMunicipal;
            }
            set {
                if (Model.NombrePlantillaRetencionImpuestoMunicipal != value) {
                    Model.NombrePlantillaRetencionImpuestoMunicipal = value;
                    IsDirty = true;
                    if (LibString.IsNullOrEmpty(NombrePlantillaRetencionImpuestoMunicipal)) {
                        ExecuteBuscarPlantillaCommandRetencionImpuestoMunicipal();
                    }
                    RaisePropertyChanged(NombrePlantillaRetencionImpuestoMunicipalPropertyName);
                }
            }
        }

        [LibCustomValidation("NumCopiasComprobantepagoValidating")]
        public int NumCopiasComprobantepago {
            get {
                return Model.NumCopiasComprobantepago;
            }
            set {
                if (Model.NumCopiasComprobantepago != value) {
                    Model.NumCopiasComprobantepago = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumCopiasComprobantepagoPropertyName);
                }
            }
        }

        public int PrimerNumeroComprobanteRetImpuestoMunicipal {
            get {
                return Model.PrimerNumeroComprobanteRetImpuestoMunicipal;
            }
            set {
                if (Model.PrimerNumeroComprobanteRetImpuestoMunicipal != value) {
                    Model.PrimerNumeroComprobanteRetImpuestoMunicipal = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrimerNumeroComprobanteRetImpuestoMunicipalPropertyName);
                }
            }
        }

        public bool RetieneImpuestoMunicipal {
            get {
                return Model.RetieneImpuestoMunicipalAsBool;
            }
            set {
                if (Model.RetieneImpuestoMunicipalAsBool != value) {
                    Model.RetieneImpuestoMunicipalAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(RetieneImpuestoMunicipalPropertyName);
                }
            }
        }

        public eTipoDeOrdenDePagoAImprimir TipoDeOrdenDePagoAImprimir {
            get {
                return Model.TipoDeOrdenDePagoAImprimirAsEnum;
            }
            set {
                if (Model.TipoDeOrdenDePagoAImprimirAsEnum != value) {
                    Model.TipoDeOrdenDePagoAImprimirAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDeOrdenDePagoAImprimirPropertyName);
                }
            }
        }

        public bool UsarCodigoProveedorEnPantalla {
            get {
                return Model.UsarCodigoProveedorEnPantallaAsBool;
            }
            set {
                if (Model.UsarCodigoProveedorEnPantallaAsBool != value) {
                    Model.UsarCodigoProveedorEnPantallaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsarCodigoProveedorEnPantallaPropertyName);
                }
            }
        }

        [LibCustomValidation("LongitudCodigoProveedorValidating")]
        public int LongitudCodigoProveedor {
            get {
                return Model.LongitudCodigoProveedor;
            }
            set {
                if (Model.LongitudCodigoProveedor != value) {
                    Model.LongitudCodigoProveedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(LongitudCodigoProveedorPropertyName);
                }
            }
        }

        public bool ImprimirComprobanteContableDePago {
            get {
                return Model.ImprimirComprobanteContableDePagoAsBool;
            }
            set {
                if (Model.ImprimirComprobanteContableDePagoAsBool != value) {
                    Model.ImprimirComprobanteContableDePagoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimirComprobanteContableDePagoPropertyName);
                }
            }
        }

        public bool NoImprimirComprobanteDePago {
            get {
                return Model.NoImprimirComprobanteDePagoAsBool;
            }
            set {
                if (Model.NoImprimirComprobanteDePagoAsBool != value) {
                    Model.NoImprimirComprobanteDePagoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(NoImprimirComprobanteDePagoPropertyName);
                }
            }
        }

        public bool AvisarSiProveedorTieneAnticipos {
            get {
                return Model.AvisarSiProveedorTieneAnticiposAsBool;
            }
            set {
                if (Model.AvisarSiProveedorTieneAnticiposAsBool != value) {
                    Model.AvisarSiProveedorTieneAnticiposAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(AvisarSiProveedorTieneAnticiposPropertyName);
                }
            }
        }

        public bool ExigirInformacionLibroDeCompras {
            get {
                return Model.ExigirInformacionLibroDeComprasAsBool;
            }
            set {
                if (Model.ExigirInformacionLibroDeComprasAsBool != value) {
                    Model.ExigirInformacionLibroDeComprasAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ExigirInformacionLibroDeComprasPropertyName);
                }
            }
        }

        public bool ConfirmarImpresionPorSecciones {
            get {
                return Model.ConfirmarImpresionPorSeccionesAsBool;
            }
            set {
                if (Model.ConfirmarImpresionPorSeccionesAsBool != value) {
                    Model.ConfirmarImpresionPorSeccionesAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConfirmarImpresionPorSeccionesPropertyName);
                }
            }
        }

        [LibCustomValidation ("ConceptoBancarioReversoDePagoValidating")]
        public string ConceptoBancarioReversoDePago {
            get {
                return Model.ConceptoBancarioReversoDePago;
            }
            set {
                if (Model.ConceptoBancarioReversoDePago != value) {
                    Model.ConceptoBancarioReversoDePago = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConceptoBancarioReversoDePagoPropertyName);
                    if (LibString.IsNullOrEmpty(ConceptoBancarioReversoDePago, true)) {
                        ConexionConceptoBancarioReversoDePago = null;
                    }
                }
            }
        }

        public eFechaSugeridaRetencionesCxP  FechaSugeridaRetencionesCxP {
            get {
                return Model.FechaSugeridaRetencionesCxPAsEnum;
            }
            set {
                if (Model.FechaSugeridaRetencionesCxPAsEnum != value) {
                    Model.FechaSugeridaRetencionesCxPAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaSugeridaRetencionesCxPPropertyName);
                }
            }
        }

        public eTipoDeOrdenDePagoAImprimir[] ArrayTipoDeOrdenDePagoAImprimir {
            get {
                return LibEnumHelper<eTipoDeOrdenDePagoAImprimir>.GetValuesInArray();
            }
        }

        public eFechaSugeridaRetencionesCxP[] ArrayFechaSugeridaRetencionesCxP {
            get {
                return LibEnumHelper<eFechaSugeridaRetencionesCxP>.GetValuesInArray();
            }
        }

        public FkConceptoBancarioViewModel ConexionConceptoBancarioReversoDePago {
            get {
                return _ConexionConceptoBancarioReversoDePago;
            }
            set {
                if (_ConexionConceptoBancarioReversoDePago != value) {
                    _ConexionConceptoBancarioReversoDePago = value;
                    if (value != null) {
                        ConceptoBancarioReversoDePago = _ConexionConceptoBancarioReversoDePago.Codigo;
                    }
                }
                if (_ConexionConceptoBancarioReversoDePago == null) {
                    ConceptoBancarioReversoDePago = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseConceptoBancarioReversoDePagoCommand {
            get;
            private set;
        }

        public RelayCommand ChooseTemplateCommandOrdenPago {
            get;
            private set;
        }

        public RelayCommand ChooseTemplateCommandRetencionImpuestoMunicipal {
            get;
            private set;
        }

        public RelayCommand ChooseTemplateCommandRetencionImpuestoMunicipalInforme {
           get;
           private set;
        }

        public bool IsVisibleRetieneImpuestoMunicipal {
            get {
                if(EsFacturadorBasico) {
                    return false;
                } else {
                    string vNombreCiudad = AppMemoryInfo.GlobalValuesGetString("Parametros", "Ciudad");
                    int vConsecutivoMunicipio = AppMemoryInfo.GlobalValuesGetInt("Parametros", "ConsecutivoMunicipio");
                    string vCodigoMunicipio = AppMemoryInfo.GlobalValuesGetString("Parametros", "CodigoMunicipio");
                    return clsUtilParameters.SePuedeRetenerParaEsteMunicipio(vNombreCiudad, vConsecutivoMunicipio) && clsUtilParameters.PuedeActivarModulo(vCodigoMunicipio);
                }
            }
        }
        #endregion //Propiedades
        #region Constructores
        public CxPComprasCxCProveedorViewModel()
            : this(new CxPProveedorPagosStt(), eAccionSR.Insertar) {
        }
        public CxPComprasCxCProveedorViewModel(CxPProveedorPagosStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = NombrePlantillaComprobanteDePagoPropertyName;
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(CxPProveedorPagosStt valModel) {
            base.InitializeLookAndFeel(valModel);
            if (IsVisibleRetieneImpuestoMunicipal && LibString.IsNullOrEmpty("NombrePlantillaRetencionImpuestoMunicipal", true)) {
                NombrePlantillaRetencionImpuestoMunicipal = "rpxComprobanteDeRetencion" + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreMunicipio");
            }
            if (IsVisibleRetieneImpuestoMunicipal && LibString.IsNullOrEmpty("NombrePlantillaRetencionImpuestoMunicipalInforme", true)) {
               NombrePlantillaRetencionImpuestoMunicipalInforme = "rpxResumenRetencionDeActividadesEconomicas" + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreMunicipio");
            }
        }

        protected override CxPProveedorPagosStt FindCurrentRecord(CxPProveedorPagosStt valModel) {
            if (valModel == null) {
                return new CxPProveedorPagosStt();
            }
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInString("NombrePlantillaComprobanteDePago", valModel.NombrePlantillaComprobanteDePago, 50);
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "CxPComprasCxCProveedorGET", vParams.Get()).FirstOrDefault();
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<CxPProveedorPagosStt>, IList<CxPProveedorPagosStt>> GetBusinessComponent() {
            return null;
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseConceptoBancarioReversoDePagoCommand = new RelayCommand<string>(ExecuteChooseConceptoBancarioReversoDePagoCommand);
            ChooseTemplateCommandOrdenPago = new RelayCommand(ExecuteBuscarPlantillaCommandOrdenPago);
            ChooseTemplateCommandRetencionImpuestoMunicipal = new RelayCommand(ExecuteBuscarPlantillaCommandRetencionImpuestoMunicipal);
            ChooseTemplateCommandRetencionImpuestoMunicipalInforme = new RelayCommand(ExecuteBuscarPlantillaCommandRetencionImpuestoMunicipalInforme);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Ingreso));
            vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("codigo", ConceptoBancarioReversoDePago), eLogicOperatorType.And);
            ConexionConceptoBancarioReversoDePago = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", vFixedCriteria, new clsSettValueByCompanyNav());
        }

        private void ExecuteChooseConceptoBancarioReversoDePagoCommand(string valcodigo) {
            try {
                if (valcodigo == null) {
                    valcodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("codigo", valcodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Ingreso));
                ConexionConceptoBancarioReversoDePago = null;
                ConexionConceptoBancarioReversoDePago = LibFKRetrievalHelper.ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteBuscarPlantillaCommandOrdenPago() {
            try {
                NombrePlantillaComprobanteDePago = new clsUtilParameters().BuscarNombrePlantilla("rpx de Comprobante Pago (*.rpx)|*Comprobante*Pago*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteBuscarPlantillaCommandRetencionImpuestoMunicipal() {
            try {
               string MunicipioName = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NombreMunicipio");
               NombrePlantillaRetencionImpuestoMunicipal = new clsUtilParameters().BuscarNombrePlantilla("rpxComprobanteDeRetencion" + MunicipioName + " (*.rpx)|*Comprobante*Retencion*" + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NombreMunicipio") + "*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        private void ExecuteBuscarPlantillaCommandRetencionImpuestoMunicipalInforme() {
           try {
              string MunicipioName = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NombreMunicipio");
              NombrePlantillaRetencionImpuestoMunicipalInforme = new clsUtilParameters().BuscarNombrePlantilla("rpxResumenRetencionDeActividadesEconomicas" + MunicipioName + " (*.rpx)|*Resumen*Retencion*" + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NombreMunicipio") + "*.rpx");
           }
           catch (System.AccessViolationException) {
              throw;
           }
           catch (System.Exception vEx) {
              LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
           }
        }
        #endregion //Metodos Generados

        private ValidationResult NombrePlantillaComprobanteDePagoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(NombrePlantillaComprobanteDePago)) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> Plantilla de impresión, es requerido.");
                } else if (!LibString.IsNullOrEmpty(NombrePlantillaComprobanteDePago) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaComprobanteDePago)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaComprobanteDePago + ", en " + this.ModuleName + ", no EXISTE.");
                }
            }
            return vResult;
        }

        private ValidationResult NombrePlantillaRetencionImpuestoMunicipalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (IsVisibleRetieneImpuestoMunicipal && LibString.IsNullOrEmpty(NombrePlantillaRetencionImpuestoMunicipal)) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> Plantilla de impresión Impuesto Municipales, es requerido.");
                } else if (IsVisibleRetieneImpuestoMunicipal && !LibString.IsNullOrEmpty(NombrePlantillaRetencionImpuestoMunicipal) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaRetencionImpuestoMunicipal)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaRetencionImpuestoMunicipal + ", en " + this.ModuleName + ", no EXISTE.");
                }
            }
            return vResult;
        }

        private ValidationResult NombrePlantillaRetencionImpuestoMunicipalInformeValidating() {
           ValidationResult vResult = ValidationResult.Success;
           if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
              return ValidationResult.Success;
           }
           else {
              if (IsVisibleRetieneImpuestoMunicipal && LibString.IsNullOrEmpty(NombrePlantillaRetencionImpuestoMunicipalInforme)) {
                 vResult = new ValidationResult("El campo " + ModuleName + "-> Plantilla de impresión Impuesto Municipales, es requerido.");
              }
              else if (IsVisibleRetieneImpuestoMunicipal && !LibString.IsNullOrEmpty(NombrePlantillaRetencionImpuestoMunicipal) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaRetencionImpuestoMunicipalInforme)) {
                 vResult = new ValidationResult("El RPX " + NombrePlantillaRetencionImpuestoMunicipalInforme + ", en " + this.ModuleName + ", no EXISTE.");
              }
           }
           return vResult;
        }

        private ValidationResult LongitudCodigoProveedorValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LongitudCodigoProveedor == 0) {
                    vResult = new ValidationResult(ModuleName + "-> Longitud Codigo Proveedor, es requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult NumCopiasComprobantepagoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (NumCopiasComprobantepago == 0) {
                    vResult = new ValidationResult(ModuleName + "-> NumCopias Comprobantepago, es requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult ConceptoBancarioReversoDePagoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(ConceptoBancarioReversoDePago, true)) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> Concepto Bancario Reverso de Pago, es requerido.");                    
                }
            }
            return vResult;
        }

        public bool IsVisibleCxPProveedorPagos {
            get {
                if(EsFacturadorBasico) {
                    return false;
                } else {
                    return true;
                }
            }
        }

        public bool EsFacturadorBasico {
            get {
                clsLibSaw inslibsaw = new clsLibSaw();
                return inslibsaw.EsVersionFacturadorBasico();
            }
        }

    } //End of class CxPComprasCxCProveedorViewModel

} //End of namespace Galac.Saw.Uil.SttDef
