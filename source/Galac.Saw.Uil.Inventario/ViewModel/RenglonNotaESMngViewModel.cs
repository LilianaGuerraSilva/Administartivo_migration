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
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Brl.Inventario.Reportes;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Uil.Inventario.ViewModel {

    public class RenglonNotaESMngViewModel : LibMngDetailViewModelMfc<RenglonNotaESViewModel, RenglonNotaES> {
        #region Propiedades

        public override string ModuleName {
            get { return "Renglon Nota ES"; }
        }

        public NotaDeEntradaSalidaViewModel Master {
            get;
            set;
        }
        #endregion //Propiedades
        #region Constructores

        public RenglonNotaESMngViewModel(NotaDeEntradaSalidaViewModel initMaster )
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            Title = "Buscar " + ModuleName;
        }

        public RenglonNotaESMngViewModel(NotaDeEntradaSalidaViewModel initMaster, ObservableCollection<RenglonNotaES> initDetail, eAccionSR initAction)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            foreach (var vItem in initDetail) {
                var vViewModel = new RenglonNotaESViewModel(Master, vItem, initAction);
                vViewModel.InitializeViewModel(initAction);
                Add(vViewModel);
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override RenglonNotaESViewModel CreateNewElement(RenglonNotaES valModel) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new RenglonNotaES();
            }
            return new RenglonNotaESViewModel(Master, vNewModel, eAccionSR.Insertar);
        }

        protected override void RaiseOnCreatedEvent(RenglonNotaESViewModel valViewModel) {
            valViewModel.Master = Master;
            base.RaiseOnCreatedEvent(valViewModel);
        }
        #endregion //Metodos Generados


    } //End of class RenglonNotaESMngViewModel

} //End of namespace Galac.Saw.Uil.Inventario

