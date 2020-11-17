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

    public class OrdenDeCompraDetalleArticuloInventarioMngViewModel : LibMngDetailViewModelMfc<OrdenDeCompraDetalleArticuloInventarioViewModel, OrdenDeCompraDetalleArticuloInventario> {
        #region Propiedades

        public override string ModuleName {
            get { return "Orden De Compra Detalle Articulo Inventario"; }
        }

        public OrdenDeCompraViewModel Master {
            get;
            set;
        }

        public int DecimalDigits {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "CantidadDeDecimales");
            }
        }
       
        #endregion //Propiedades
        #region Constructores

        public OrdenDeCompraDetalleArticuloInventarioMngViewModel(OrdenDeCompraViewModel initMaster )
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            Title = "Buscar " + ModuleName;
        }

        public OrdenDeCompraDetalleArticuloInventarioMngViewModel(OrdenDeCompraViewModel initMaster, ObservableCollection<OrdenDeCompraDetalleArticuloInventario> initDetail, eAccionSR initAction)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            foreach (var vItem in initDetail) {
                var vViewModel = new OrdenDeCompraDetalleArticuloInventarioViewModel(Master, vItem, initAction);
                vViewModel.InitializeViewModel(initAction);
                Add(vViewModel);
            }
            if (Master.Action == eAccionSR.Insertar) {
                RemoveColumnByDisplayMemberPath("CantidadRecibida");
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override OrdenDeCompraDetalleArticuloInventarioViewModel CreateNewElement(OrdenDeCompraDetalleArticuloInventario valModel) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new OrdenDeCompraDetalleArticuloInventario();
            }
            return new OrdenDeCompraDetalleArticuloInventarioViewModel(Master, vNewModel, eAccionSR.Insertar);
        }

        protected override void RaiseOnCreatedEvent(OrdenDeCompraDetalleArticuloInventarioViewModel valViewModel) {
            valViewModel.Master = Master;
            base.RaiseOnCreatedEvent(valViewModel);
        }
        #endregion //Metodos Generados


    } //End of class OrdenDeCompraDetalleArticuloInventarioMngViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

