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
using Galac.Adm.Brl.CajaChica;
using Galac.Adm.Brl.CajaChica.Reportes;
using Galac.Adm.Ccl.CajaChica;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Uil.CajaChica.ViewModel {

    public class DetalleDeRendicionMngViewModel : LibMngDetailViewModelMfc<DetalleDeRendicionViewModel, DetalleDeRendicion> {
        #region Propiedades

        public override string ModuleName {
            get { return "Detalle De Rendicion"; }
        }

        public RendicionViewModel Master {
            get;
            set;
        }

        //public new ObservableCollection<LibGridColumModel> VisibleColumns {
        //    get;
        //    private set;
        //}
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public LibXmlMemInfo AppMemoryInfo { get; set; }
        */
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores

        public DetalleDeRendicionMngViewModel(RendicionViewModel initMaster )
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            Title = "Buscar " + ModuleName;
        }

        public DetalleDeRendicionMngViewModel(RendicionViewModel initMaster, ObservableCollection<DetalleDeRendicion> initDetail, eAccionSR initAction)
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            foreach (var vItem in initDetail) {
                Add(new DetalleDeRendicionViewModel(Master, vItem, initAction));
            }
            //ColumnaDeAlicuotasEspecialAMostrar();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override DetalleDeRendicionViewModel CreateNewElement(DetalleDeRendicion valModel) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new DetalleDeRendicion();
            }
            return new DetalleDeRendicionViewModel(Master, vNewModel, eAccionSR.Insertar);
        }

        protected override bool CanExecuteDeleteCommand(string valUseExternalEditorStr) {
            if (SelectedItem != null) {
                if (SelectedItem.GeneradaPor != eGeneradoPor.Rendicion)
                    return false;
            }
            return base.CanExecuteDeleteCommand(valUseExternalEditorStr);
        }

        protected override void ExecuteDeleteCommand(string valUseExternalEditorStr) {
            base.ExecuteDeleteCommand(valUseExternalEditorStr);
        }

        //private void ColumnaDeAlicuotasEspecialAMostrar() {
        //    VisibleColumns = LibGridColumModel.GetGridColumsFromType(typeof(DetalleDeRendicionViewModel));
        //    if (Master._EmpresaAplicaIVAEspecial == (int)eAplicacionAlicuota.No_Aplica) {
        //        VisibleColumns.RemoveAt(13);
        //        VisibleColumns.RemoveAt(12);
        //        VisibleColumns.RemoveAt(11);
        //        VisibleColumns.RemoveAt(10);
        //        VisibleColumns.RemoveAt(9);
        //    }
        //}
        #endregion //Metodos Generados


    } //End of class DetalleDeRendicionMngViewModel

} //End of namespace Galac.Adm.Uil.CajaChica

