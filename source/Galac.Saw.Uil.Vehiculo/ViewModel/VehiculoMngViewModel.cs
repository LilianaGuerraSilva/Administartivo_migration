using System;
using System.Collections.Generic;
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
using Galac.Saw.Brl.Vehiculo;
using Galac.Saw.Brl.Vehiculo.Reportes;
using Galac.Saw.Ccl.Vehiculo;
using Entity = Galac.Saw.Ccl.Vehiculo;

namespace Galac.Saw.Uil.Vehiculo.ViewModel {

    public class VehiculoMngViewModel : LibMngViewModelMfc<VehiculoViewModel, Entity.Vehiculo> {
        #region Propiedades

        public override string ModuleName {
            get { return "Vehículo"; }
        }
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public LibXmlMemInfo AppMemoryInfo { get; set; }
        */
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores

        public VehiculoMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, Consecutivo";
            #region Codigo Ejemplo
            /* Codigo de Ejemplo
            OrderByDirection = "DESC";
            AppMemoryInfo = LibGlobalValues.Instance.GetAppMemInfo();
        */
            #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override VehiculoViewModel CreateNewElement(Entity.Vehiculo valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new Entity.Vehiculo();
            }
            return new VehiculoViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_Vehiculo_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessComponentWithSearch<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>> GetBusinessComponent() {
            return new clsVehiculoNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return new clsVehiculoRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            #region Codigo Ejemplo
            /* Codigo de Ejemplo
            SUPROCESOPARTICULARCommand = new RelayCommand(ExecuteSUPROCESOPARTICULARCommand, CanExecuteSUPROCESOPARTICULARCommand);
        */
            #endregion //Codigo Ejemplo
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.RemoveRibbonControl("Consultas", "Imprimir Lista");
            }
        }
        #endregion //Metodos Generados

        protected override bool CanExecuteDeleteCommand() {
            return base.CanExecuteDeleteCommand() && CurrentItem != null;
        }

        protected override bool CanExecuteUpdateCommand() {
            return base.CanExecuteUpdateCommand() && CurrentItem != null;
        }

        protected override bool CanExecuteReadCommand() {
            return base.CanExecuteReadCommand() && CurrentItem != null;
        }

    } //End of class VehiculoMngViewModel

} //End of namespace Galac.Saw.Uil.Vehiculo

