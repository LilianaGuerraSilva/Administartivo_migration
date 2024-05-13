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
using LibGalac.Aos.ARRpt.Reports;
using Galac.Adm.Brl.GestionProduccion;
using Galac.Adm.Brl.GestionProduccion.Reportes;
using Galac.Adm.Ccl.GestionProduccion;

namespace Galac.Adm.Uil.GestionProduccion.ViewModel {

    public class ListaDeMaterialesDetalleSalidasMngViewModel : LibMngDetailViewModelMfc<ListaDeMaterialesDetalleSalidasViewModel, ListaDeMaterialesDetalleSalidas> {
        #region Propiedades

        public override string ModuleName {
            get { return "Salidas"; }
        }

        public ListaDeMaterialesViewModel Master {
            get;
            set;
        }
        #endregion //Propiedades
        #region Constructores

        public ListaDeMaterialesDetalleSalidasMngViewModel(ListaDeMaterialesViewModel initMaster )
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            Title = "Buscar " + ModuleName;
        }

        public ListaDeMaterialesDetalleSalidasMngViewModel(ListaDeMaterialesViewModel initMaster, ObservableCollection<ListaDeMaterialesDetalleSalidas> initDetail, eAccionSR initAction)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            foreach (var vItem in initDetail) {
                var vViewModel = new ListaDeMaterialesDetalleSalidasViewModel(Master, vItem, initAction);
                vViewModel.InitializeViewModel(initAction);
                Add(vViewModel);
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ListaDeMaterialesDetalleSalidasViewModel CreateNewElement(ListaDeMaterialesDetalleSalidas valModel) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new ListaDeMaterialesDetalleSalidas();
            }
            return new ListaDeMaterialesDetalleSalidasViewModel(Master, vNewModel, eAccionSR.Insertar);
        }

        protected override void RaiseOnCreatedEvent(ListaDeMaterialesDetalleSalidasViewModel valViewModel) {
            valViewModel.Master = Master;
            base.RaiseOnCreatedEvent(valViewModel);
        }
        #endregion //Metodos Generados


    } //End of class ListaDeMaterialesDetalleSalidasMngViewModel

} //End of namespace Galac.Adm.Uil.GestionProduccion

