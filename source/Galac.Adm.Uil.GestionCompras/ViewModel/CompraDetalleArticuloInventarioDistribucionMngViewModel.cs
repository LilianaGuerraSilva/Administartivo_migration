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
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Brl.GestionCompras.Reportes;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Uil.GestionCompras.ViewModel {

    public class CompraDetalleArticuloInventarioDistribucionMngViewModel : LibMngDetailViewModelMfc<CompraDetalleArticuloInventarioViewModel, CompraDetalleArticuloInventario> {
        #region Propiedades

        public override string ModuleName {
            get { return "Compra Detalle Articulo Inventario Distribucion"; }
        }

        public CompraViewModel Master {
            get;
            set;
        }
        #endregion //Propiedades
        #region Constructores

        public CompraDetalleArticuloInventarioDistribucionMngViewModel(CompraViewModel initMaster )
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            Title = "Buscar " + ModuleName;
        }

        public CompraDetalleArticuloInventarioDistribucionMngViewModel(CompraViewModel initMaster, ObservableCollection<CompraDetalleArticuloInventario> initDetail, eAccionSR initAction)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            foreach (var vItem in initDetail) {
                var vViewModel = new CompraDetalleArticuloInventarioViewModel(Master, vItem, initAction);
                vViewModel.InitializeViewModel(initAction);
                Add(vViewModel);
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override CompraDetalleArticuloInventarioViewModel CreateNewElement(CompraDetalleArticuloInventario valModel) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new CompraDetalleArticuloInventario();
            }
            return new CompraDetalleArticuloInventarioViewModel(Master, vNewModel, eAccionSR.Insertar);
        }

        protected override void RaiseOnCreatedEvent(CompraDetalleArticuloInventarioViewModel valViewModel) {
            valViewModel.Master = Master;
            base.RaiseOnCreatedEvent(valViewModel);
        }
        #endregion //Metodos Generados


    } //End of class CompraDetalleArticuloInventarioDistribucionMngViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

