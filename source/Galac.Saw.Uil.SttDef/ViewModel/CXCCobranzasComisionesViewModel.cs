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
    public class CXCCobranzasComisionesViewModel : LibInputViewModelMfc<ComisionesStt> {
        #region Constantes
        public const string FormaDeCalcularComisionesSobreCobranzaPropertyName = "FormaDeCalcularComisionesSobreCobranza";
        public const string NombrePlantillaComisionSobreCobranzaPropertyName = "NombrePlantillaComisionSobreCobranza";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "4.3.- Comisiones"; }
        }

        public eCalculoParaComisionesSobreCobranzaEnBaseA  FormaDeCalcularComisionesSobreCobranza {
            get {
                return Model.FormaDeCalcularComisionesSobreCobranzaAsEnum;
            }
            set {
                if (Model.FormaDeCalcularComisionesSobreCobranzaAsEnum != value) {
                    Model.FormaDeCalcularComisionesSobreCobranzaAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(FormaDeCalcularComisionesSobreCobranzaPropertyName);
                }
            }
        }

        [LibCustomValidation("NombrePlantillaComisionSobreCobranzaValidating")]
        public string  NombrePlantillaComisionSobreCobranza {
            get {
                return Model.NombrePlantillaComisionSobreCobranza;
            }
            set {
                if (Model.NombrePlantillaComisionSobreCobranza != value) {
                    Model.NombrePlantillaComisionSobreCobranza = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombrePlantillaComisionSobreCobranzaPropertyName);
                }
            }
        }

        public eCalculoParaComisionesSobreCobranzaEnBaseA[] ArrayCalculoParaComisionesSobreCobranzaEnBaseA {
            get {
                return LibEnumHelper<eCalculoParaComisionesSobreCobranzaEnBaseA>.GetValuesInArray();
            }
        }

        public RelayCommand ChooseTemplateCommand {
            get;
            private set;
        }

        public bool IsVisibleFormaDeCalcularComisionesSobreCobranza {
            get {
                return !AppMemoryInfo.GlobalValuesGetBool("Parametros", "EsSistemaParaIG");
            }
        }

        #endregion //Propiedades
        #region Constructores
        public CXCCobranzasComisionesViewModel()
            : this(new ComisionesStt(), eAccionSR.Insertar) {
        }
        public CXCCobranzasComisionesViewModel(ComisionesStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = FormaDeCalcularComisionesSobreCobranzaPropertyName;
            mEsFacturadorBasico = new clsLibSaw().EsFacturadorBasico();
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Variables
        bool mEsFacturadorBasico;
        #endregion
        #region Metodos Generados

        protected override void InitializeCommands() {
            ChooseTemplateCommand = new RelayCommand(ExecuteBuscarPlantillaCommand);
        }

        protected override void InitializeLookAndFeel(ComisionesStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ComisionesStt FindCurrentRecord(ComisionesStt valModel) {
            if (valModel == null) {
                return new ComisionesStt();
            }
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInEnum("FormaDeCalcularComisionesSobreCobranza", LibConvert.EnumToDbValue((int)valModel.FormaDeCalcularComisionesSobreCobranzaAsEnum));
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "CXCCobranzasComisionesGET", vParams.Get()).FirstOrDefault();
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<ComisionesStt>, IList<ComisionesStt>> GetBusinessComponent() {
            return null;
        }

        private void ExecuteBuscarPlantillaCommand() {
            try {
                NombrePlantillaComisionSobreCobranza = new clsUtilParameters().BuscarNombrePlantilla("rpx de Comisiones por Cobranza (*.rpx)|*Comision*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult NombrePlantillaComisionSobreCobranzaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(!LibString.IsNullOrEmpty(NombrePlantillaComisionSobreCobranza) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaComisionSobreCobranza)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaComisionSobreCobranza + ", en " + this.ModuleName + ", no EXISTE.");
                }
            }
            return vResult;
        }
        
        public bool IsVisiblecmbFormaDeCalcularComisionesSobreCobranza {
            get {
                return !mEsFacturadorBasico;
            }
        }

        #endregion //Metodos Generados
        


    } //End of class CXCCobranzasComisionesViewModel

} //End of namespace Galac.Saw.Uil.SttDef

