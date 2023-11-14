using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class FacturaImprentaDigitalViewModel : LibInputViewModelMfc<FacturaImprentaDigitalStt> {
        #region Constantes
        public const string UsaImprentaDigitalPropertyName = "UsaImprentaDigital";
        public const string FechaInicioImprentaDigitalPropertyName = "FechaInicioImprentaDigital";
        public const string ProveedorServicioImprentaDigitalPropertyName = "ProveedorServicioImprentaDigital";
        private const string IsEnabledUsaImprentaDigitalPropertyName = "IsEnabledUsaImprentaDigital";
        private const string IsEnabledDatosImprentaDigitalPropertyName = "IsEnabledDatosImprentaDigital";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "2.8.- Imprenta Digital"; }
        }

        public bool UsaImprentaDigital {
            get {
                return Model.UsaImprentaDigitalAsBool;
            }
            set {
                if (Model.UsaImprentaDigitalAsBool != value) {
                    Model.UsaImprentaDigitalAsBool = value;
                    IsDirty = true;
                    if (!value) {
                        FechaInicioImprentaDigital = LibDate.MinDateForDB();
                        ProveedorServicioImprentaDigital = eProveedorImprentaDigital.NoAplica;
                        RaisePropertyChanged(FechaInicioImprentaDigitalPropertyName);
                    }
                    RaisePropertyChanged(UsaImprentaDigitalPropertyName);
                    RaisePropertyChanged(ProveedorServicioImprentaDigitalPropertyName);
                    RaisePropertyChanged(IsEnabledUsaImprentaDigitalPropertyName);
                    RaisePropertyChanged(IsEnabledDatosImprentaDigitalPropertyName);
                    LibMessages.Notification.Send<bool>(Model.UsaImprentaDigitalAsBool, UsaImprentaDigitalPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaInicioImprentaDigitalValidating")]
        public DateTime FechaInicioImprentaDigital {
            get {
                return Model.FechaInicioImprentaDigital;
            }
            set {
                if (Model.FechaInicioImprentaDigital != value) {
                    Model.FechaInicioImprentaDigital = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaInicioImprentaDigitalPropertyName);
                    LibMessages.Notification.Send<DateTime>(Model.FechaInicioImprentaDigital, FechaInicioImprentaDigitalPropertyName);
                }
            }
        }

        [LibCustomValidation("ProveedorServicioImprentaDigitalValidating")]
        public eProveedorImprentaDigital ProveedorServicioImprentaDigital {
            get {
                return Model.ProveedorImprentaDigitalAsEnum;
            }
            set {
                if (Model.ProveedorImprentaDigitalAsEnum != value) {
                    Model.ProveedorImprentaDigitalAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ProveedorServicioImprentaDigitalPropertyName);
                    LibMessages.Notification.Send<eProveedorImprentaDigital>(Model.ProveedorImprentaDigitalAsEnum, ProveedorServicioImprentaDigitalPropertyName);
                }
            }
        }

        public bool IsEnabledUsaImprentaDigital {
            get { return false; }
        }

        public bool IsEnabledDatosImprentaDigital {
            get {
                return false;
            }
        }

        public eProveedorImprentaDigital[] ArrayProveedorImprentaDigital {
            get {
                return LibEnumHelper<eProveedorImprentaDigital>.GetValuesInArray();
            }
        }
        #endregion //Propiedades
        #region Constructores
        public FacturaImprentaDigitalViewModel()
            : this(new FacturaImprentaDigitalStt(), eAccionSR.Insertar) {
        }
        public FacturaImprentaDigitalViewModel(FacturaImprentaDigitalStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = UsaImprentaDigitalPropertyName;
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(FacturaImprentaDigitalStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override FacturaImprentaDigitalStt FindCurrentRecord(FacturaImprentaDigitalStt valModel) {
            if (valModel == null) {
                return null;
            }
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<FacturaImprentaDigitalStt>, IList<FacturaImprentaDigitalStt>> GetBusinessComponent() {
            return null;
        }

        private ValidationResult FechaInicioImprentaDigitalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaInicioImprentaDigital, false, Action)) {
                vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de inicio del uso de Imprenta Digital fuera de los límites permitidos."));
            } else if (FechaInicioImprentaDigital > LibDate.Today()) {
                vResult = new ValidationResult("La Fecha de inicio del uso de Imprenta Digital no puede ser mayor a la fecha actual.");
            }
            return vResult;
        }

        private ValidationResult ProveedorServicioImprentaDigitalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (UsaImprentaDigital && ProveedorServicioImprentaDigital != eProveedorImprentaDigital.NoAplica) {
                return ValidationResult.Success;
            } else if (UsaImprentaDigital && ProveedorServicioImprentaDigital == eProveedorImprentaDigital.NoAplica) {
                vResult = new ValidationResult("Debe escoger un Proveedor del servicio de Imprenta Digital.");
            }
            return vResult;
        }
        #endregion //Metodos Generados
    } //End of class FacturaImprentaDigitalViewModel
} //End of namespace Galac.Comun.Uil.SttDef

