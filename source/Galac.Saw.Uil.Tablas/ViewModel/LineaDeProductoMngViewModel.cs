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
using Galac.Saw.Brl.Tablas;
using Galac.Saw.Brl.Tablas.Reportes;
using Galac.Saw.Ccl.Tablas;
using LibGalac.Aos.UI.Wpf;

namespace Galac.Saw.Uil.Tablas.ViewModel {

    [LibMefInstallValuesMetadata(typeof(LineaDeProductoMngViewModel))]
    public class LineaDeProductoMngViewModel : LibMngViewModelMfc<LineaDeProductoViewModel, LineaDeProducto> , ILibMefInstallValues{
        #region Propiedades

        public override string ModuleName {
            get { return "Línea de Producto"; }
        }

        public RelayCommand ReinstallCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores

        public LineaDeProductoMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, Consecutivo";
            CanPrint = true;

            #region Codigo Ejemplo
            /* Codigo de Ejemplo
            OrderByDirection = "DESC";
        */
            #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override LineaDeProductoViewModel CreateNewElement(LineaDeProducto valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new LineaDeProducto();
            }
            return new LineaDeProductoViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_LineaDeProducto_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
        }

        protected override ILibBusinessComponentWithSearch<IList<LineaDeProducto>, IList<LineaDeProducto>> GetBusinessComponent() {
            return new clsLineaDeProductoNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return new clsLineaDeProductoRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
           ReinstallCommand = new RelayCommand(ExecuteReinstallCommand, CanExecuteReinstallCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            //if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {                
            //}
            RibbonData.RemoveRibbonControl("Consultas", "Imprimir Lista");
        }
        #endregion //Metodos Generados
        #region CanExecute
        protected override bool CanExecuteCreateCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Insertar");
        }

        protected override bool CanExecuteUpdateCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Modificar") && CurrentItem != null;
        }

        protected override bool CanExecuteDeleteCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Eliminar") && CurrentItem != null;
        }

        protected override bool CanExecuteReadCommand() {
            return LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Consultar") && CurrentItem != null;
        }

        protected override bool CanExecuteSearchCommand() {
            return HasAccessToModule();
        }

        private bool CanExecuteReinstallCommand() {
            if (LibGalac.Aos.DefGen.LibDefGen.IsProduct(LibGalac.Aos.DefGen.LibProduct.GetInitialsSaw())) {
                return false;
            } else {
                return HasAccessToModule();
            }
        }

        private bool CanExecuteImprimirCommand() {
            return true;
        }
        protected override bool HasAccessToModule() {
            bool vResult = false;
            vResult = (LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Insertar") ||
                LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Eliminar") ||
                LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Modificar") ||
                LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Consultar"));
            return vResult;
        }

        protected override void ExecutePrintCommand() {
            base.ExecutePrintCommand();
        }

        private void ExecuteReinstallCommand() {
            try {
                InstallOrReInstallDataFromFile(eAccionSR.ReInstalar);
                LibMessages.RefreshList.Send(ModuleName);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        bool ILibMefInstallValues.InstallFromFile() {
            return InstallOrReInstallDataFromFile(eAccionSR.Instalar);        
        }

        internal bool InstallOrReInstallDataFromFile(eAccionSR valAction) {
            bool vResult = false;
            string vFileName = string.Empty;
            if (valAction == eAccionSR.Instalar) {
                vFileName = System.IO.Path.Combine(LibWorkPaths.TablesDir, "LineaDeproducto.txt");
                vResult = LibImportExport.InstallData(vFileName, ModuleName, new clsLineaDeProductoImpExp(), LibEExportDelimiterType.ToDelimiter(eExportDelimiterType.Csv));
            } else {
                vFileName = System.IO.Path.Combine(LibWorkPaths.TablesDir, "LineaDeproducto.txt");
                vResult = LibImportExport.ReInstallData(vFileName, ModuleName, new clsLineaDeProductoImpExp(), LibEExportDelimiterType.ToDelimiter(eExportDelimiterType.Csv));
            }
            return vResult;
        }

        #endregion

    }
}


    