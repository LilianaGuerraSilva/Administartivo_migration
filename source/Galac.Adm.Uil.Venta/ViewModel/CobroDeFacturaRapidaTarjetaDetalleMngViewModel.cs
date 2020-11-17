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
using Galac.Adm.Brl.Venta;
//using Galac.Adm.Brl.Venta.Reportes;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Uil.Venta.ViewModel {

    public class CobroDeFacturaRapidaTarjetaDetalleMngViewModel : LibMngDetailViewModelMfc<CobroDeFacturaRapidaTarjetaDetalleViewModel, CobroDeFacturaRapidaTarjetaDetalle> {
        #region Propiedades

        public override string ModuleName {
            get { return "Punto de Venta Tarjeta Detalle"; }
        }

        public CobroDeFacturaRapidaTarjetaViewModel Master {
            get;
            set;
        }
        #endregion //Propiedades
        #region Constructores

        public CobroDeFacturaRapidaTarjetaDetalleMngViewModel(CobroDeFacturaRapidaTarjetaViewModel initMaster )
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            Title = "Buscar " + ModuleName;
        }

        public CobroDeFacturaRapidaTarjetaDetalleMngViewModel(CobroDeFacturaRapidaTarjetaViewModel initMaster, ObservableCollection<CobroDeFacturaRapidaTarjetaDetalle> initDetail, eAccionSR initAction)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            foreach (var vItem in initDetail) {
                var vViewModel = new CobroDeFacturaRapidaTarjetaDetalleViewModel(Master, vItem, initAction);
                vViewModel.InitializeViewModel(initAction);
                Add(vViewModel);
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override CobroDeFacturaRapidaTarjetaDetalleViewModel CreateNewElement(CobroDeFacturaRapidaTarjetaDetalle valModel) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new CobroDeFacturaRapidaTarjetaDetalle();
            }
            return new CobroDeFacturaRapidaTarjetaDetalleViewModel(Master, vNewModel, eAccionSR.Insertar);
        }

        protected override void RaiseOnCreatedEvent(CobroDeFacturaRapidaTarjetaDetalleViewModel valViewModel) {
            valViewModel.Master = Master;
            base.RaiseOnCreatedEvent(valViewModel);
        }
        #endregion //Metodos Generados


    } //End of class CobroDeFacturaRapidaTarjetaDetalleMngViewModel

} //End of namespace Galac.Adm.Uil.Venta

