using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.Brl.Contracts;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.Catching;

using Galac.Adm.Brl.GestionProduccion;

using Galac.Adm.Ccl.GestionProduccion;

namespace Galac.Adm.Uil.GestionProduccion.ViewModel {

    public class ListaDeMaterialesDetalleArticuloMngViewModel : LibMngDetailViewModelMfc<ListaDeMaterialesDetalleArticuloViewModel, ListaDeMaterialesDetalleArticulo> {
        #region Propiedades

        public override string ModuleName {
            get { return "Productos y/o Servicios"; }
        }

        public ListaDeMaterialesViewModel Master {
            get;
            set;
        }
        #endregion //Propiedades
        #region Constructores

        public ListaDeMaterialesDetalleArticuloMngViewModel(ListaDeMaterialesViewModel initMaster )
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            Title = "Buscar " + ModuleName;
        }

        public ListaDeMaterialesDetalleArticuloMngViewModel(ListaDeMaterialesViewModel initMaster, ObservableCollection<ListaDeMaterialesDetalleArticulo> initDetail, eAccionSR initAction)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            foreach (var vItem in initDetail) {
                var vViewModel = new ListaDeMaterialesDetalleArticuloViewModel(Master, vItem, initAction);
                vViewModel.InitializeViewModel(initAction);
                Add(vViewModel);
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ListaDeMaterialesDetalleArticuloViewModel CreateNewElement(ListaDeMaterialesDetalleArticulo valModel) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new ListaDeMaterialesDetalleArticulo();
            }
            return new ListaDeMaterialesDetalleArticuloViewModel(Master, vNewModel, eAccionSR.Insertar);
        }

        protected override void RaiseOnCreatedEvent(ListaDeMaterialesDetalleArticuloViewModel valViewModel) {
            valViewModel.Master = Master;
            base.RaiseOnCreatedEvent(valViewModel);
        }
        #endregion //Metodos Generados


    } //End of class ListaDeMaterialesDetalleArticuloMngViewModel

} //End of namespace Galac.Saw.Uil.Inventario

