using System.Collections.ObjectModel;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.Base;
using Galac.Adm.Ccl.Vendedor;

namespace Galac.Adm.Uil.Vendedor.ViewModel {

    public class VendedorDetalleComisionesMngViewModel : LibMngDetailViewModelMfc<VendedorDetalleComisionesViewModel, VendedorDetalleComisiones> {
        #region Propiedades

        public override string ModuleName {
            get { return "Comisiones de Vendedor por Línea de Producto"; }
        }

        public VendedorViewModel Master {
            get;
            set;
        }
        #endregion //Propiedades
        #region Constructores

        public VendedorDetalleComisionesMngViewModel(VendedorViewModel initMaster )
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            Title = "Buscar " + ModuleName;
        }

        public VendedorDetalleComisionesMngViewModel(VendedorViewModel initMaster, ObservableCollection<VendedorDetalleComisiones> initDetail, eAccionSR initAction)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            foreach (var vItem in initDetail) {
                var vViewModel = new VendedorDetalleComisionesViewModel(Master, vItem, initAction);
                vViewModel.InitializeViewModel(initAction);
                Add(vViewModel);
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override VendedorDetalleComisionesViewModel CreateNewElement(VendedorDetalleComisiones valModel) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new VendedorDetalleComisiones();
            }
            return new VendedorDetalleComisionesViewModel(Master, vNewModel, eAccionSR.Insertar);
        }

        protected override void RaiseOnCreatedEvent(VendedorDetalleComisionesViewModel valViewModel) {
            valViewModel.Master = Master;
            base.RaiseOnCreatedEvent(valViewModel);
        }
        #endregion //Metodos Generados

    } //End of class VendedorDetalleComisionesMngViewModel
} //End of namespace Galac.Adm.Uil.Vendedor

