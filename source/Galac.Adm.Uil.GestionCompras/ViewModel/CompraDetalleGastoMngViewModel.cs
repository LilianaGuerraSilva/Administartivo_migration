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

    public class CompraDetalleGastoMngViewModel : LibMngDetailViewModelMfc<CompraDetalleGastoViewModel, CompraDetalleGasto> {
        //private int _SelectedIndex;
        #region Propiedades

        public override string ModuleName {
            get { return "Compra Detalle Gasto"; }
        }

        public CompraViewModel Master {
            get;
            set;
        }

        //public int SelectedIndex {
        //    get {
        //        return _SelectedIndex;
        //    }
        //    set {
        //        _SelectedIndex = value;
        //        //RaisePropertyChanged("SelectedIndex");
        //    }
        //}
        #endregion //Propiedades
        #region Constructores

        public CompraDetalleGastoMngViewModel(CompraViewModel initMaster )
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            Title = "Buscar " + ModuleName;
        }

        public CompraDetalleGastoMngViewModel(CompraViewModel initMaster, ObservableCollection<CompraDetalleGasto> initDetail, eAccionSR initAction)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            foreach (var vItem in initDetail) {
                var vViewModel = new CompraDetalleGastoViewModel(Master, vItem, initAction);
                vViewModel.InitializeViewModel(initAction);
                Add(vViewModel);
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override CompraDetalleGastoViewModel CreateNewElement(CompraDetalleGasto valModel) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new CompraDetalleGasto();
            }
            return new CompraDetalleGastoViewModel(Master, vNewModel, eAccionSR.Insertar);
        }

        protected override void RaiseOnCreatedEvent(CompraDetalleGastoViewModel valViewModel) {
            valViewModel.Master = Master;
            base.RaiseOnCreatedEvent(valViewModel);
        }
        #endregion //Metodos Generados


    } //End of class CompraDetalleGastoMngViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

