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

namespace Galac.Saw.Uil.Tablas.ViewModel {

    public class UnidadDeVentaMngViewModel : LibMngViewModel<UnidadDeVentaViewModel, UnidadDeVenta> {
        #region Propiedades

        public override string ModuleName {
            get { return "Unidad De Venta"; }
        }
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public LibXmlMemInfo AppMemoryInfo { get; set; }
        */
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores

        public UnidadDeVentaMngViewModel() {
            Title = "Buscar " + ModuleName;
            OrderByMember = "Nombre";
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            OrderByDirection = "DESC";
            AppMemoryInfo = LibGlobalValues.Instance.GetAppMemInfo();
        */
        #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override UnidadDeVentaViewModel CreateNewElement(UnidadDeVenta valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new UnidadDeVenta();
            }
            return new UnidadDeVentaViewModel(vNewModel, valAction);
        }

        protected override ILibBusinessComponentWithSearch<IList<UnidadDeVenta>, IList<UnidadDeVenta>> GetBusinessComponent() {
            return new clsUnidadDeVentaNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return new clsUnidadDeVentaRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
        #region Codigo Ejemplo

        #endregion //Codigo Ejemplo
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
               RibbonData.RemoveRibbonControl("Consultas", "Imprimir Lista");
            }
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
           return LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Consultar");
        }

        protected override bool HasAccessToModule() {
           bool vResult = false;
           vResult = (LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Insertar") ||
               LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Eliminar") ||
               LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Modificar") ||
               LibSecurityManager.CurrentUserHasAccessTo("Tablas", "Consultar"));
           return vResult;
        }
        #endregion
        #region Codigo Ejemplo
        
        #endregion //Codigo Ejemplo


    } //End of class UnidadDeVentaMngViewModel

} //End of namespace Galac.Saw.Uil.Tablas

