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

    public class CobroDeFacturaRapidaAnticipoDetalleMngViewModel : LibMngDetailViewModelMfc<CobroDeFacturaRapidaAnticipoDetalleViewModel, CobroDeFacturaRapidaAnticipoDetalle> {
        #region Propiedades

        public override string ModuleName {
            get { return "Cobro De Factura Rapida Anticipo Detalle"; }
        }

        public CobroDeFacturaRapidaAnticipoViewModel Master {
            get;
            set;
        }
        #endregion //Propiedades
        #region Constructores

        public CobroDeFacturaRapidaAnticipoDetalleMngViewModel(CobroDeFacturaRapidaAnticipoViewModel initMaster )
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            Title = "Buscar " + ModuleName;
        }

        public CobroDeFacturaRapidaAnticipoDetalleMngViewModel(CobroDeFacturaRapidaAnticipoViewModel initMaster, ObservableCollection<CobroDeFacturaRapidaAnticipoDetalle> initDetail, eAccionSR initAction)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            foreach (var vItem in initDetail) {
                var vViewModel = new CobroDeFacturaRapidaAnticipoDetalleViewModel(Master, vItem, initAction);
                vViewModel.InitializeViewModel(initAction);
                Add(vViewModel);
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override CobroDeFacturaRapidaAnticipoDetalleViewModel CreateNewElement(CobroDeFacturaRapidaAnticipoDetalle valModel) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new CobroDeFacturaRapidaAnticipoDetalle();
            }
            return new CobroDeFacturaRapidaAnticipoDetalleViewModel(Master, vNewModel, eAccionSR.Insertar);
        }

        protected override void RaiseOnCreatedEvent(CobroDeFacturaRapidaAnticipoDetalleViewModel valViewModel) {
            valViewModel.Master = Master;
            base.RaiseOnCreatedEvent(valViewModel);
        }
        #endregion //Metodos Generados


    } //End of class CobroDeFacturaRapidaAnticipoDetalleMngViewModel

} //End of namespace Galac.Adm.Uil.Venta

