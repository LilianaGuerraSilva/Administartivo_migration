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
using Galac.Saw.Brl.Tablas;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Uil.Tablas.ViewModel {
    public class ImpuestoBancarioViewModel : LibInputViewModel<ImpuestoBancario> {
        #region Constantes
        public const string FechaDeInicioDeVigenciaPropertyName = "FechaDeInicioDeVigencia";
		public const string AlicuotaC1Al4PropertyName = "AlicuotaC1Al4";
        public const string AlicuotaC5PropertyName = "AlicuotaC5";
        public const string AlicuotaC6PropertyName = "AlicuotaC6";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Alícuota ITF"; }
        }

        [LibRequired(ErrorMessage = "El campo Fecha de inicio de vigencia es requerido.")]
        [LibCustomValidation("FechaDeInicioDeVigenciaValidating")]
        [LibGridColum("Fecha de inicio de vigencia", eGridColumType.DatePicker)]
        public DateTime  FechaDeInicioDeVigencia {
            get {
                return Model.FechaDeInicioDeVigencia;
            }
            set {
                if (Model.FechaDeInicioDeVigencia != value) {
                    Model.FechaDeInicioDeVigencia = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaDeInicioDeVigenciaPropertyName);
                }
            }
        }

        public decimal  AlicuotaAlDebito {
            get {
                return Model.AlicuotaAlDebito;
            }
            set {
                if (Model.AlicuotaAlDebito != value) {
                    Model.AlicuotaAlDebito = value;
                }
            }
        }

        public decimal  AlicuotaAlCredito {
            get {
                return Model.AlicuotaAlCredito;
            }
            set {
                if (Model.AlicuotaAlCredito != value) {
                    Model.AlicuotaAlCredito = value;
                }
            }
        }
        [LibGridColum("Contribuyentes 1, 2, 3, 4", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        public decimal  AlicuotaC1Al4 {
            get {
                return Model.AlicuotaC1Al4;
            }
            set {
                if (Model.AlicuotaC1Al4 != value) {
                    Model.AlicuotaC1Al4 = value;
                    IsDirty = true;
                    RaisePropertyChanged(AlicuotaC1Al4PropertyName);
                }
            }
        }

        [LibGridColum("Contribuyentes 5", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        public decimal  AlicuotaC5 {
            get {
                return Model.AlicuotaC5;
            }
            set {
                if (Model.AlicuotaC5 != value) {
                    Model.AlicuotaC5 = value;
                    IsDirty = true;
                    RaisePropertyChanged(AlicuotaC5PropertyName);
                }
            }
        }

        [LibGridColum("Contribuyentes 6", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        public decimal  AlicuotaC6 {
            get {
                return Model.AlicuotaC6;
            }
            set {
                if (Model.AlicuotaC6 != value) {
                    Model.AlicuotaC6 = value;
                    IsDirty = true;
                    RaisePropertyChanged(AlicuotaC6PropertyName);
                }
            }
        }
        #endregion //Propiedades
        #region Constructores
        public ImpuestoBancarioViewModel()
            : this(new ImpuestoBancario(), eAccionSR.Insertar) {
        }
        public ImpuestoBancarioViewModel(ImpuestoBancario initModel, eAccionSR initAction)
            : base(initModel, initAction) {
            DefaultFocusedPropertyName = FechaDeInicioDeVigenciaPropertyName;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(ImpuestoBancario valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ImpuestoBancario FindCurrentRecord(ImpuestoBancario valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInDateTime("FechaDeInicioDeVigencia", valModel.FechaDeInicioDeVigencia);
            return BusinessComponent.GetData(eProcessMessageType.SpName,"ImpTransacBancariasGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<ImpuestoBancario>, IList<ImpuestoBancario>> GetBusinessComponent() {
            return new clsImpuestoBancarioNav();
        }

        private ValidationResult FechaDeInicioDeVigenciaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDeInicioDeVigencia, false, Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de inicio de vigencia"));
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class ImpuestoBancarioViewModel

} //End of namespace Galac.Comun.Uil.TablasLey

