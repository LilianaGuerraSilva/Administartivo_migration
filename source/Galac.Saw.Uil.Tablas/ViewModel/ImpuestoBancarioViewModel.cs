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
        public const string AlicuotaAlDebitoPropertyName = "AlicuotaAlDebito";
        public const string AlicuotaAlCreditoPropertyName = "AlicuotaAlCredito";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Alícuota ITF"; }
        }

        [LibGridColum("Fecha De Inicio De Vigencia",eGridColumType.DatePicker,Width =200)]
        [LibCustomValidation("FechaDeInicioDeVigenciaValidating")]
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

        [LibGridColum("Alícuota al Débito",eGridColumType.Numeric)] 
        public decimal  AlicuotaAlDebito {
            get {
                return Model.AlicuotaAlDebito;
            }
            set {
                if (Model.AlicuotaAlDebito != value) {
                    Model.AlicuotaAlDebito = value;
                    IsDirty = true;
                    RaisePropertyChanged(AlicuotaAlDebitoPropertyName);
                }
            }
        }

        [LibGridColum("Alícuota al Crédito",eGridColumType.Numeric)]
        public decimal  AlicuotaAlCredito {
            get {
                return Model.AlicuotaAlCredito;
            }
            set {
                if (Model.AlicuotaAlCredito != value) {
                    Model.AlicuotaAlCredito = value;
                    IsDirty = true;
                    RaisePropertyChanged(AlicuotaAlCreditoPropertyName);
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
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de Inicio De Vigencia"));
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class ImpuestoBancarioViewModel

} //End of namespace Galac.Comun.Uil.TablasLey

