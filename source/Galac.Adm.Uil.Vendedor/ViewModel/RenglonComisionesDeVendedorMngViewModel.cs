using System.Collections.ObjectModel;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.Base;
using Galac.Adm.Ccl.Vendedor;

namespace Galac.Adm.Uil.Vendedor.ViewModel {

    public class RenglonComisionesDeVendedorMngViewModel : LibMngDetailViewModelMfc<RenglonComisionesDeVendedorViewModel, RenglonComisionesDeVendedor> {
        #region Propiedades

        public override string ModuleName {
            get { return "Renglon Comisiones De Vendedor"; }
        }

        public VendedorViewModel Master {
            get;
            set;
        }
        #endregion //Propiedades
        #region Constructores

        public RenglonComisionesDeVendedorMngViewModel(VendedorViewModel initMaster )
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            Title = "Buscar " + ModuleName;
        }

        public RenglonComisionesDeVendedorMngViewModel(VendedorViewModel initMaster, ObservableCollection<RenglonComisionesDeVendedor> initDetail, eAccionSR initAction)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            foreach (var vItem in initDetail) {
                var vViewModel = new RenglonComisionesDeVendedorViewModel(Master, vItem, initAction);
                vViewModel.InitializeViewModel(initAction);
                Add(vViewModel);
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override RenglonComisionesDeVendedorViewModel CreateNewElement(RenglonComisionesDeVendedor valModel) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new RenglonComisionesDeVendedor();
            }
            return new RenglonComisionesDeVendedorViewModel(Master, vNewModel, eAccionSR.Insertar);
        }

        protected override void RaiseOnCreatedEvent(RenglonComisionesDeVendedorViewModel valViewModel) {
            valViewModel.Master = Master;
            base.RaiseOnCreatedEvent(valViewModel);
        }
        #endregion //Metodos Generados


    } //End of class RenglonComisionesDeVendedorMngViewModel

} //End of namespace Galac..Uil.ComponenteNoEspecificado

