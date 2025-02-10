using Galac.Comun.Uil.TablasGen.ViewModel;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class AuditoriaEnteMngViewModel : LibMngViewModel<CiudadViewModel, Ciudad> {//acá está Ciudad por ser el model más simple
        #region Propiedades
        public override string ModuleName {
            get { return "Auditoría SENIAT"; }
        }
        #endregion //Propiedades
        #region Constructores
        public AuditoriaEnteMngViewModel() {
            Title = ModuleName;
            OrderByMember = "";
        }
        #endregion Constructores

        protected override CiudadViewModel CreateNewElement(Ciudad model, eAccionSR valAction) {
            return null;
        }

        protected override ILibBusinessComponentWithSearch<IList<Ciudad>, IList<Ciudad>> GetBusinessComponent() {
            return null;
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return null;
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return null;
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
            }
        }
    }
}

