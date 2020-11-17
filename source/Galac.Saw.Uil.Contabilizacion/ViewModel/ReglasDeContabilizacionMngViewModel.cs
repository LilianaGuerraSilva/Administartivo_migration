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
using Galac.Saw.Brl.Contabilizacion;
using Galac.Saw.Ccl.Contabilizacion;

namespace Galac.Saw.Uil.Contabilizacion.ViewModel {

   public class ReglasDeContabilizacionMngViewModel : LibMngViewModelMfc<ReglasDeContabilizacionViewModel, ReglasDeContabilizacion> {
      #region Propiedades

      public override string ModuleName {
         get { return "Reglas de Contabilización"; }
      }
      
      #endregion //Propiedades
      #region Constructores

      public ReglasDeContabilizacionMngViewModel()
         : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
         Title = "Buscar " + ModuleName;
         OrderByMember = "ConsecutivoCompania, Numero";       
      }
      #endregion //Constructores
      #region Metodos Generados

      protected override ReglasDeContabilizacionViewModel CreateNewElement(ReglasDeContabilizacion valModel, eAccionSR valAction) {
         var vNewModel = valModel;
         if (vNewModel == null) {
            vNewModel = new ReglasDeContabilizacion();
         }
         return new ReglasDeContabilizacionViewModel(vNewModel, valAction);
      }

      protected override LibSearchCriteria GetMFCCriteria() {
         return LibSearchCriteria.CreateCriteria("Gv_ReglasDeContabilizacion_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
      }

      protected override ILibBusinessComponentWithSearch<IList<ReglasDeContabilizacion>, IList<ReglasDeContabilizacion>> GetBusinessComponent() {
         return new clsReglasDeContabilizacionNav();
      }

      protected override ILibReportInfo GetDataRetrievesInstance() {
         return null;
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
            RibbonData.RemoveRibbonControl("Administrar", "Insertar");
            RibbonData.RemoveRibbonControl("Administrar", "Eliminar");
            RibbonData.RemoveRibbonControl("Consultas", "Buscar");
         }
      }
      #endregion //Metodos Generados

      protected override bool HasAccessToModule() {
         return LibSecurityManager.CurrentUserHasAccessToModule(ModuleName);
      }

      protected override bool CanExecuteUpdateCommand() {
         return LibSecurityManager.CurrentUserHasAccessTo(ModuleName, "Modificar");
      }

      protected override bool CanExecuteReadCommand() {
         return LibSecurityManager.CurrentUserHasAccessTo(ModuleName, "Consultar");
      }

      protected override void ExecuteUpdateCommand() {
         InitializarReglasViewModel(eAccionSR.Modificar);
      }

      protected override void ExecuteReadCommand() {
         InitializarReglasViewModel(eAccionSR.Consultar);
      }

      private void InitializarReglasViewModel(eAccionSR accion) {
         try {
            ReglasDeContabilizacionViewModel vViewModel = new ReglasDeContabilizacionViewModel();
            vViewModel.InitializeViewModel(accion);
            bool result = LibMessages.EditViewModel.ShowEditor(vViewModel);
         } catch (System.AccessViolationException) {
            throw;
         } catch (System.Exception vEx) {
            LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
         }
      }

      protected override void SearchItems() {
         //base.SearchItems();
      }

   } //End of class ReglasDeContabilizacionMngViewModel

} //End of namespace Galac.Saw.Uil.Contabilizacion

