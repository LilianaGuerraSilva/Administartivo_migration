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

    public class OrdenDeProduccionDetalleMaterialesMngViewModel : LibMngDetailViewModelMfc<OrdenDeProduccionDetalleMaterialesViewModel, OrdenDeProduccionDetalleMateriales> {
        #region Propiedades

        public override string ModuleName {
            get { return "Insumos"; }
        }

        public OrdenDeProduccionViewModel Master {
            get;
            set;
        }

        public new ObservableCollection<LibGridColumModel> VisibleColumns {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores

        public OrdenDeProduccionDetalleMaterialesMngViewModel(OrdenDeProduccionViewModel initMaster )
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            Title = "Buscar " + ModuleName;
        }

        public OrdenDeProduccionDetalleMaterialesMngViewModel(OrdenDeProduccionViewModel initMaster, ObservableCollection<OrdenDeProduccionDetalleMateriales> initDetail, eAccionSR initAction)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            foreach (var vItem in initDetail) {
                var vViewModel = new OrdenDeProduccionDetalleMaterialesViewModel(Master, vItem, initAction);
                vViewModel.InitializeViewModel(initAction);
                Add(vViewModel);
            }         
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override OrdenDeProduccionDetalleMaterialesViewModel CreateNewElement(OrdenDeProduccionDetalleMateriales valModel) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new OrdenDeProduccionDetalleMateriales();
            }
            return new OrdenDeProduccionDetalleMaterialesViewModel(Master, vNewModel, eAccionSR.Insertar);
        }

        protected override void RaiseOnCreatedEvent(OrdenDeProduccionDetalleMaterialesViewModel valViewModel) {
            valViewModel.Master = Master;
            base.RaiseOnCreatedEvent(valViewModel);
        }
        #endregion //Metodos Generados

        public void OcultarColumnas() {
            VisibleColumns = LibGridColumModel.GetGridColumsFromType(typeof(OrdenDeProduccionDetalleMaterialesViewModel));
            ObservableCollection<LibGridColumModel> VisibleColumnsCpy = LibGridColumModel.GetGridColumsFromType(typeof(OrdenDeProduccionDetalleArticuloViewModel));
            if (Master.Action == eAccionSR.Consultar) {
                foreach (var item in VisibleColumnsCpy) {
                    if (VisibleColumns.Count > 2) {
                        VisibleColumns.RemoveAt(2);
                    }
                }
            }
        }
    } //End of class OrdenDeProduccionDetalleMaterialesMngViewModel

} //End of namespace Galac.Adm.Uil.GestionProduccion

