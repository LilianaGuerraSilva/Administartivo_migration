using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Brl.Contracts;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.UI.Mvvm.Messaging;

namespace Galac.Saw.Uil.SttDef.ViewModel {

    [LibMefInstallValuesMetadata(typeof(DatosGeneralesCompaniaMngViewModel))]
    public class DatosGeneralesCompaniaMngViewModel : LibMngViewModel<DatosGeneralesCompaniaViewModel, CompaniaStt> {

        #region Propiedades

        public override string ModuleName {
            get { return "1.1.- Compania"; }
        }
        
        #endregion //Propiedades

        #region Constructores

        public DatosGeneralesCompaniaMngViewModel() {
            Title = "Buscar " + ModuleName;
        }
        #endregion


        protected override DatosGeneralesCompaniaViewModel CreateNewElement(CompaniaStt model, LibGalac.Aos.Base.eAccionSR valAction) {
           return new DatosGeneralesCompaniaViewModel();
        }

        protected override LibGalac.Aos.Base.ILibBusinessComponentWithSearch<IList<CompaniaStt>, IList<CompaniaStt>> GetBusinessComponent() {
            return null;
        }

        protected override LibGalac.Aos.Base.Report.ILibReportInfo GetDataRetrievesInstance() {
            return null;
        }

        protected override LibGalac.Aos.Base.ILibRpt GetReportForList(string valModuleName, LibGalac.Aos.Base.Report.ILibReportInfo valReportInfo, LibGalac.Aos.Base.LibCollFieldFormatForGrid valFieldsFormat, LibGalac.Aos.Base.LibGpParams valParams) {
            return null;
        }

        protected override void SearchItems() {
            
        }


        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if(RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.RemoveRibbonControl("Administrar", "Insertar");
                RibbonData.RemoveRibbonControl("Administrar", "Eliminar");
                RibbonData.RemoveRibbonControl("Consultas", "Buscar");
                RibbonData.RemoveRibbonControl("Consultas", "Imprimir Lista");
            }
        }
        

        protected override bool CanExecuteReadCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo("Parámetros", "Consultar");
        }

        protected override bool CanExecuteUpdateCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo("Parámetros", "Modificar");
        }

        protected override bool CanExecuteSearchCommand() {
            return HasAccessToModule();
        }

        protected override bool HasAccessToModule() {
            bool vResult = false;
            vResult = (LibSecurityManager.CurrentUserHasAccessTo("Parámetros", "Modificar") ||
                LibSecurityManager.CurrentUserHasAccessTo("Parámetros", "Informes") ||
                LibSecurityManager.CurrentUserHasAccessTo("Parámetros", "Consultar"));
            return vResult;
        }

        protected override void ExecuteUpdateCommand() {
            InitializarViewModel(eAccionSR.Modificar);
        }

        private void InitializarViewModel(eAccionSR accion) {
            try {
                DatosGeneralesCompaniaViewModel vViewModel = new DatosGeneralesCompaniaViewModel(new CompaniaStt(), eAccionSR.Modificar);
                vViewModel.InitializeViewModel(accion);
                bool result = LibMessages.EditViewModel.ShowEditor(vViewModel);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }
        
    }
}
