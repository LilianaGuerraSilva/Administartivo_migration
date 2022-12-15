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
    public class InventarioNotaEntradaSalidaViewModel : LibInputViewModelMfc<NotaEntradaSalidaStt> {
        #region Constantes
        public const string NombrePlantillaCodigoDeBarrasPropertyName = "NombrePlantillaCodigoDeBarras";
        public const string ImprimirReporteAlIngresarNotaEntradaSalidaPropertyName = "ImprimirReporteAlIngresarNotaEntradaSalida";
        public const string ImprimirNotaESconPrecioPropertyName = "ImprimirNotaESconPrecio";
        public const string NombrePlantillaNotaEntradaSalidaPropertyName = "NombrePlantillaNotaEntradaSalida";
        public const string IsEnabledImpresionDePlanillaPropertyName = "IsEnabledImpresionDePlanilla";        
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "5.2.- Nota Entrada/Salida"; }
        }

        [LibCustomValidation ("NombrePlantillaCodigoDeBarrasValidating")]
        public string  NombrePlantillaCodigoDeBarras {
            get {
                return Model.NombrePlantillaCodigoDeBarras;
            }
            set {
                if (Model.NombrePlantillaCodigoDeBarras != value) {
                    Model.NombrePlantillaCodigoDeBarras = value;
                    IsDirty = true;
                    if (LibString.IsNullOrEmpty(NombrePlantillaCodigoDeBarras)) {
                        ExecuteBuscarPlantillaCommandCodBarra();
                    }
                    RaisePropertyChanged(NombrePlantillaCodigoDeBarrasPropertyName);
                }
            }
        }

        public bool  ImprimirReporteAlIngresarNotaEntradaSalida {
            get {
                return Model.ImprimirReporteAlIngresarNotaEntradaSalidaAsBool;
            }
            set {
                if (Model.ImprimirReporteAlIngresarNotaEntradaSalidaAsBool != value) {
                    Model.ImprimirReporteAlIngresarNotaEntradaSalidaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimirReporteAlIngresarNotaEntradaSalidaPropertyName);                    
                }
            }
        }

        public bool  ImprimirNotaESconPrecio {
            get {
                return Model.ImprimirNotaESconPrecioAsBool;
            }
            set {
                if (Model.ImprimirNotaESconPrecioAsBool != value) {
                    Model.ImprimirNotaESconPrecioAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimirNotaESconPrecioPropertyName);
                    RaisePropertyChanged(IsEnabledImpresionDePlanillaPropertyName);                    
                }
            }
        }

        [LibCustomValidation("NombrePlantillaNotaEntradaSalidaValidating")]
        public string  NombrePlantillaNotaEntradaSalida {
            get {
                return Model.NombrePlantillaNotaEntradaSalida;
            }
            set {
                if (Model.NombrePlantillaNotaEntradaSalida != value) {
                    Model.NombrePlantillaNotaEntradaSalida = value;
                    IsDirty = true;
                    if (LibString.IsNullOrEmpty(NombrePlantillaNotaEntradaSalida)) {
                        ExecuteBuscarPlantillaCommandImpresion();
                    }
                    RaisePropertyChanged(NombrePlantillaNotaEntradaSalidaPropertyName);
                }
            }
        }

        public RelayCommand ChooseTemplateCommandImpresion {
            get;
            private set;
        }

        public RelayCommand ChooseTemplateCommandCodBarra {
            get;
            private set;
        }

        public bool IsVisibleDatosNotaEntradaSalida {
            get {
                return !clsUtilParameters.EsSistemaParaIG();
            }
        }

        public bool IsEnabledImpresionDePlanilla {
            get {
                return IsEnabled && ImprimirNotaESconPrecio;
            }
        }
        #endregion //Propiedades
        #region Constructores
        public InventarioNotaEntradaSalidaViewModel()
            : this(new NotaEntradaSalidaStt(), eAccionSR.Insertar) {
        }
        public InventarioNotaEntradaSalidaViewModel(NotaEntradaSalidaStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = NombrePlantillaCodigoDeBarrasPropertyName;
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeCommands() {
            ChooseTemplateCommandImpresion = new RelayCommand(ExecuteBuscarPlantillaCommandImpresion);
            ChooseTemplateCommandCodBarra = new RelayCommand(ExecuteBuscarPlantillaCommandCodBarra);
        }

        protected override void InitializeLookAndFeel(NotaEntradaSalidaStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override NotaEntradaSalidaStt FindCurrentRecord(NotaEntradaSalidaStt valModel) {
            if (valModel == null) {
                return new NotaEntradaSalidaStt();
            }
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInString("NombrePlantillaCodigoDeBarras", valModel.NombrePlantillaCodigoDeBarras, 50);
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "InventarioNotaEntradaSalidaGET", vParams.Get()).FirstOrDefault();
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<NotaEntradaSalidaStt>, IList<NotaEntradaSalidaStt>> GetBusinessComponent() {
            return null;
        }

        private void ExecuteBuscarPlantillaCommandImpresion() {
            try {
                NombrePlantillaNotaEntradaSalida = new clsUtilParameters().BuscarNombrePlantilla("rpx de Nota E/S (*.rpx)|*Nota*ES*.rpx");                
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteBuscarPlantillaCommandCodBarra() {
            try {
                NombrePlantillaCodigoDeBarras = new clsUtilParameters().BuscarNombrePlantilla("rpx de Código de Barras (*.rpx)|*Codigo*Barras*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //Metodos Generados

        private ValidationResult NombrePlantillaNotaEntradaSalidaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(NombrePlantillaNotaEntradaSalida)){
                    vResult = new ValidationResult("El campo " + ModuleName + "-> Plantilla de impresión, es requerido.");
                }else if (!LibString.IsNullOrEmpty(NombrePlantillaNotaEntradaSalida) && ! clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaNotaEntradaSalida)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaNotaEntradaSalida + ", en " + this.ModuleName + ", no EXISTE.");
                }
            }
            return vResult;
        }

        private ValidationResult NombrePlantillaCodigoDeBarrasValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(NombrePlantillaCodigoDeBarras)) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> Plantilla de impresión de Códigos de Barra, es requerido.");
                }else if (!LibString.IsNullOrEmpty(NombrePlantillaCodigoDeBarras) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaCodigoDeBarras)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaCodigoDeBarras + ", en " + this.ModuleName + ", no EXISTE.");
                }
            }
            return vResult;
        }


    } //End of class InventarioNotaEntradaSalidaViewModel

} //End of namespace Galac.Saw.Uil.SttDef

