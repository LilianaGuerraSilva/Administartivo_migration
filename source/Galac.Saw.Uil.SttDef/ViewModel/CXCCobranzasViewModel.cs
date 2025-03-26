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
    public class CXCCobranzasViewModel : LibInputViewModelMfc<CobranzasStt> {
        #region Constantes
        public const string NombrePlantillaCompobanteCobranzaPropertyName = "NombrePlantillaCompobanteCobranza";
        public const string SugerirConsecutivoEnCobranzaPropertyName = "SugerirConsecutivoEnCobranza";
        public const string UsarZonaCobranzaPropertyName = "UsarZonaCobranza";
        public const string ImprimirCombrobanteAlIngresarCobranzaPropertyName = "ImprimirCombrobanteAlIngresarCobranza";
        public const string ConceptoReversoCobranzaPropertyName = "ConceptoReversoCobranza";
        public const string AsignarComisionDeVendedorEnCobranzaPropertyName = "AsignarComisionDeVendedorEnCobranza";
        public const string BloquearNumeroCobranzaPropertyName = "BloquearNumeroCobranza";
        public const string CambiarCobradorVendedorPropertyName = "CambiarCobradorVendedor";
        #endregion
        #region Variables
        private FkConceptoBancarioViewModel _ConexionConceptoReversoCobranza = null;
        bool mEsFacturadorBasico;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "4.2.- Cobranzas"; }
        }

        [LibCustomValidation("NombrePlantillaCompobanteCobranzaValidating")]
        public string  NombrePlantillaCompobanteCobranza {
            get {
                if(!ImprimirCombrobanteAlIngresarCobranza) {
                    return string.Empty;
                } else {
                    if(LibString.IsNullOrEmpty(Model.NombrePlantillaCompobanteCobranza)) {
                        Model.NombrePlantillaCompobanteCobranza = "rpxComprobanteDeCobro";
                        return Model.NombrePlantillaCompobanteCobranza;
                    } else {
                        return Model.NombrePlantillaCompobanteCobranza;
                    }
                }
            }
            set {
                if (Model.NombrePlantillaCompobanteCobranza != value) {
                    Model.NombrePlantillaCompobanteCobranza = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombrePlantillaCompobanteCobranzaPropertyName);
                }
            }
        }

        public bool  SugerirConsecutivoEnCobranza {
            get {
                return Model.SugerirConsecutivoEnCobranzaAsBool;
            }
            set {
                if (Model.SugerirConsecutivoEnCobranzaAsBool != value) {
                    Model.SugerirConsecutivoEnCobranzaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(SugerirConsecutivoEnCobranzaPropertyName);
                }
            }
        }

        public bool  UsarZonaCobranza {
            get {
                return Model.UsarZonaCobranzaAsBool;
            }
            set {
                if (Model.UsarZonaCobranzaAsBool != value) {
                    Model.UsarZonaCobranzaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsarZonaCobranzaPropertyName);
                }
            }
        }

        public bool  ImprimirCombrobanteAlIngresarCobranza {
            get {
                return Model.ImprimirCombrobanteAlIngresarCobranzaAsBool;
            }
            set {
                if (Model.ImprimirCombrobanteAlIngresarCobranzaAsBool != value) {
                    Model.ImprimirCombrobanteAlIngresarCobranzaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimirCombrobanteAlIngresarCobranzaPropertyName);
                    RaisePropertyChanged(NombrePlantillaCompobanteCobranzaPropertyName);
                }
            }
        }


       [LibRequired]
        public string  ConceptoReversoCobranza {
            get {
                return Model.ConceptoReversoCobranza;
            }
            set {
                if (Model.ConceptoReversoCobranza != value) {
                    Model.ConceptoReversoCobranza = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConceptoReversoCobranzaPropertyName);
                    if (LibString.IsNullOrEmpty(ConceptoReversoCobranza, true)) {
                        ConexionConceptoReversoCobranza = null;
                    }
                }
            }
        }

        public bool  AsignarComisionDeVendedorEnCobranza {
            get {
                return Model.AsignarComisionDeVendedorEnCobranzaAsBool;
            }
            set {
                if (Model.AsignarComisionDeVendedorEnCobranzaAsBool != value) {
                    Model.AsignarComisionDeVendedorEnCobranzaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(AsignarComisionDeVendedorEnCobranzaPropertyName);
                }
            }
        }

        public bool  BloquearNumeroCobranza {
            get {
                return Model.BloquearNumeroCobranzaAsBool;
            }
            set {
                if (Model.BloquearNumeroCobranzaAsBool != value) {
                    Model.BloquearNumeroCobranzaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(BloquearNumeroCobranzaPropertyName);
                }
            }
        }

        public bool  CambiarCobradorVendedor {
            get {
                return Model.CambiarCobradorVendedorAsBool;
            }
            set {
                if (Model.CambiarCobradorVendedorAsBool != value) {
                    Model.CambiarCobradorVendedorAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(CambiarCobradorVendedorPropertyName);
                }
            }
        }

        public FkConceptoBancarioViewModel ConexionConceptoReversoCobranza {
            get {
                return _ConexionConceptoReversoCobranza;
            }
            set {
                if (_ConexionConceptoReversoCobranza != value) {
                    _ConexionConceptoReversoCobranza = value;
                    RaisePropertyChanged(ConceptoReversoCobranzaPropertyName);
                }
                if (_ConexionConceptoReversoCobranza == null) {
                    ConceptoReversoCobranza = string.Empty;
                }
            }
        }

        public RelayCommand ChooseTemplateCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseConceptoReversoCobranzaCommand {
            get;
            private set;
        }

        #endregion //Propiedades
        #region Constructores
        public CXCCobranzasViewModel()
            : this(new CobranzasStt(), eAccionSR.Insertar) {
        }
        public CXCCobranzasViewModel(CobranzasStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = NombrePlantillaCompobanteCobranzaPropertyName;
            mEsFacturadorBasico = new clsLibSaw().EsFacturadorBasico();
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseTemplateCommand = new RelayCommand(ExecuteBuscarPlantillaCommand);
            ChooseConceptoReversoCobranzaCommand = new RelayCommand<string>(ExecuteChooseConceptoReversoCobranzaCommand);
        }

        protected override void InitializeLookAndFeel(CobranzasStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override CobranzasStt FindCurrentRecord(CobranzasStt valModel) {
            if (valModel == null) {
                return new CobranzasStt();
            }
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInString("NombrePlantillaCompobanteCobranza", valModel.NombrePlantillaCompobanteCobranza, 50);
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "CXCCobranzasGET", vParams.Get()).FirstOrDefault();
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<CobranzasStt>, IList<CobranzasStt>> GetBusinessComponent() {
            return null;
        }

        private void ExecuteBuscarPlantillaCommand() {
            try {
                NombrePlantillaCompobanteCobranza = new clsUtilParameters().BuscarNombrePlantilla("rpx de Cobranza (*.rpx)|*Cobr*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionConceptoReversoCobranza = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", LibSearchCriteria.CreateCriteria("codigo", ConceptoReversoCobranza), new clsSettValueByCompanyNav());
        }

        private void ExecuteChooseConceptoReversoCobranzaCommand(string valcodigo) {
            try {
                if (valcodigo == null) {
                    valcodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valcodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Egreso));
                ConceptoReversoCobranza = null;
                ConexionConceptoReversoCobranza = LibFKRetrievalHelper.ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionConceptoReversoCobranza != null) {
                    ConceptoReversoCobranza = ConexionConceptoReversoCobranza.Codigo;
                } else {
                    ConceptoReversoCobranza = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult NombrePlantillaCompobanteCobranzaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
               if (ImprimirCombrobanteAlIngresarCobranza) {
                  if(!LibString.IsNullOrEmpty(NombrePlantillaCompobanteCobranza) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaCompobanteCobranza)) {
                     vResult = new ValidationResult("El RPX " + NombrePlantillaCompobanteCobranza + ", en " + this.ModuleName + ", no EXISTE.");
                  }
                  if(LibString.IsNullOrEmpty(NombrePlantillaCompobanteCobranza)) {
                     vResult = new ValidationResult("El RPX " + NombrePlantillaCompobanteCobranza + ", en " + this.ModuleName + ", es EXISTE.");
                  }
               }
            }
            return vResult;
        }

        public bool IsVisibleUsarZonaCobranza {
            get {
                return !mEsFacturadorBasico;
            }
        }

        public bool IsVisibleNombrePlantillaCompobanteCobranza {
            get {
                return !mEsFacturadorBasico;
            }
        }

        public bool IsVisiblelAsignarComisionDeVendedorEnCobranza {
            get {
                return !mEsFacturadorBasico;
            }
        }

        public bool IsVisibleIsSugerirConsecutivoEnCobranzaUsarZonaCobranza {
            get {
                return !mEsFacturadorBasico;
            }
        }

        public bool IsVisibleBloquearNumeroCobranza {
            get {
                return !mEsFacturadorBasico;
            }
        }

        public bool IsVisibleConceptoReversoCobranza {
            get {
                return !mEsFacturadorBasico;
            }
        }

        public bool IsVisibleImprimirCombrobanteAlIngresarCobranza {
            get {
                return !mEsFacturadorBasico;
            }
        }

        public bool IsVisibleAsignarComisionDeVendedorEnCobranza {
            get {
                return !mEsFacturadorBasico;
            }
        }

        public bool IsVisibleTexto {
            get {
                return !mEsFacturadorBasico;
            }
        }

        public bool IsVisibleCambiarCobradorVendedor {
            get {
                return !mEsFacturadorBasico;
            }
        }

        public bool IsVisibleSugerirConsecutivoEnCobranza {
            get {
                return !mEsFacturadorBasico;
            }
        }

        #endregion //Metodos Generados


    } //End of class CXCCobranzasViewModel

} //End of namespace Galac.Saw.Uil.SttDef

