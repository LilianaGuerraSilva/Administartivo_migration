using System.Collections.Generic;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class InventarioProduccionViewModel : LibInputViewModelMfc<ProduccionStt> {
        #region Constantes
        public const string CalcularCostoDelArticuloTerminadoAPartirDePropertyName = "CalcularCostoDelArticuloTerminadoAPartirDe";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "5.5.- Producción"; }
        }

        public eCostoTerminadoCalculadoAPartirDe  CalcularCostoDelArticuloTerminadoAPartirDe {
            get {
                return Model.CalcularCostoDelArticuloTerminadoAPartirDeAsEnum;
            }
            set {
                if (Model.CalcularCostoDelArticuloTerminadoAPartirDeAsEnum != value) {
                    Model.CalcularCostoDelArticuloTerminadoAPartirDeAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(CalcularCostoDelArticuloTerminadoAPartirDePropertyName);
                }
            }
        }

        public eCostoTerminadoCalculadoAPartirDe[] ArrayCostoTerminadoCalculadoAPartirDe {
            get {
                return LibEnumHelper<eCostoTerminadoCalculadoAPartirDe>.GetValuesInArray();
            }
        }
        #endregion //Propiedades
        #region Constructores
        public InventarioProduccionViewModel()
            : this(new ProduccionStt(), eAccionSR.Insertar) {
        }
        public InventarioProduccionViewModel(ProduccionStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = CalcularCostoDelArticuloTerminadoAPartirDePropertyName;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(ProduccionStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ProduccionStt FindCurrentRecord(ProduccionStt valModel) {
            if (valModel == null) {
                return null;
            }
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<ProduccionStt>, IList<ProduccionStt>> GetBusinessComponent() {
            return null;
        }
        #endregion //Metodos Generados
    } //End of class InventarioProduccionViewModel
} //End of namespace Galac.Comun.Uil.SttDef

