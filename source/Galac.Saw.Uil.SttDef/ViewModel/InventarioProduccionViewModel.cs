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
using Galac.Saw.Uil.SttDef.ViewModel;
using LibGalac.Aos.Uil;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class InventarioProduccionViewModel : LibInputViewModelMfc<ProduccionStt> {
        #region Constantes
        public const string CalcularCostoDelArticuloTerminadoAPartirDePropertyName = "CalcularCostoDelArticuloTerminadoAPartirDe";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "5.5.- Producción"; }
        }

        public int  ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if (Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                }
            }
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
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "InventarioProduccionGET", vParams.Get()).FirstOrDefault();
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<ProduccionStt>, IList<ProduccionStt>> GetBusinessComponent() {
            return null;
        }
        #endregion //Metodos Generados


    } //End of class InventarioProduccionViewModel

} //End of namespace Galac.Comun.Uil.SttDef

