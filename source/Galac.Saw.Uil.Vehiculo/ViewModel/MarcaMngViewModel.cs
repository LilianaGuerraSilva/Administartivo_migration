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

namespace Galac.Saw.Uil.Vehiculo.ViewModel {

    public class MarcaMngViewModel : LibMngViewModel<MarcaViewModel, Marca> {
        #region Propiedades

        public override string ModuleName {
            get { return "Marca"; }
        }

        #endregion //Propiedades
        #region Constructores

        public MarcaMngViewModel() {
            Title = "Buscar " + ModuleName;
            OrderByMember = "Nombre";

        }
        #endregion //Constructores
        #region Metodos Generados



        protected override MarcaViewModel CreateNewElement(Marca valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new Marca();
            }
            return new MarcaViewModel(vNewModel, valAction);
        }

        protected override ILibBusinessComponentWithSearch<IList<Marca>, IList<Marca>> GetBusinessComponent() {
            return new clsMarcaNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return new clsMarcaRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
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


    } //End of class MarcaMngViewModel

} //End of namespace Galac.Saw.Uil.Vehiculo

