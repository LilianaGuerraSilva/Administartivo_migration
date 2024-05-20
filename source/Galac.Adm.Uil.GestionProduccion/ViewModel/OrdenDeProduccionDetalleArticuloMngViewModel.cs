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

    public class OrdenDeProduccionDetalleArticuloMngViewModel : LibMngDetailViewModelMfc<OrdenDeProduccionDetalleArticuloViewModel, OrdenDeProduccionDetalleArticulo> {
        #region Propiedades

        public override string ModuleName {
            get { return "Salidas"; }
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

        public OrdenDeProduccionDetalleArticuloMngViewModel(OrdenDeProduccionViewModel initMaster )
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            Title = "Buscar " + ModuleName;
        }

        public OrdenDeProduccionDetalleArticuloMngViewModel(OrdenDeProduccionViewModel initMaster, ObservableCollection<OrdenDeProduccionDetalleArticulo> initDetail, eAccionSR initAction)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            foreach (var vItem in initDetail) {
                var vViewModel = new OrdenDeProduccionDetalleArticuloViewModel(Master, vItem, initAction);
                vViewModel.InitializeViewModel(initAction);
                Add(vViewModel);
               
            }
            ColumnasAMostrar();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override OrdenDeProduccionDetalleArticuloViewModel CreateNewElement(OrdenDeProduccionDetalleArticulo valModel) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new OrdenDeProduccionDetalleArticulo();
            }
            return new OrdenDeProduccionDetalleArticuloViewModel(Master, vNewModel, eAccionSR.Insertar);
        }

        protected override void RaiseOnCreatedEvent(OrdenDeProduccionDetalleArticuloViewModel valViewModel) {
            valViewModel.Master = Master;
            base.RaiseOnCreatedEvent(valViewModel);
        }

        private void ColumnasAMostrar() {
            VisibleColumns = LibGridColumModel.GetGridColumsFromType(typeof(OrdenDeProduccionDetalleMaterialesViewModel));
            //if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("FacturaRapida", "UsaPrecioSinIva")) {
            //    VisibleColumns.RemoveAt(4);
            //} else {
            //    VisibleColumns.RemoveAt(3);
            //}
        }
        #endregion //Metodos Generados
    } //End of class OrdenDeProduccionDetalleArticuloMngViewModel

} //End of namespace Galac.Adm.Uil. GestionProduccion

