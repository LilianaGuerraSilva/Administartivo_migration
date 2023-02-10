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
using Galac.Adm.Brl.Banco;
using Galac.Adm.Brl.Banco.Reportes;
using Galac.Adm.Ccl.Banco;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Uil.Banco.ViewModel {
	public class TransferenciaEntreCuentasBancariasMngViewModel : LibMngViewModelMfc<TransferenciaEntreCuentasBancariasViewModel, TransferenciaEntreCuentasBancarias> {
		#region Propiedades
		public override string ModuleName {
			get { return "Transferencia entre Cuentas Bancarias"; }
		}

		protected RelayCommand AnularCommand { get; private set; }
        protected RelayCommand InsertarCommand { get; private set; }
		protected RelayCommand ConsultarCommand { get; private set; }
		#endregion //Propiedades

		#region Constructores
		public TransferenciaEntreCuentasBancariasMngViewModel()
			: base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
			Title = "Buscar " + ModuleName.Substring(0, 27);
			OrderByMember = "ConsecutivoCompania, Consecutivo";
		}
		#endregion //Constructores

		#region Metodos Generados
		protected override TransferenciaEntreCuentasBancariasViewModel CreateNewElement(TransferenciaEntreCuentasBancarias valModel, eAccionSR valAction) {
			TransferenciaEntreCuentasBancarias vNewModel = valModel;
			if (vNewModel == null) {
				vNewModel = new TransferenciaEntreCuentasBancarias();
			}
			return new TransferenciaEntreCuentasBancariasViewModel(vNewModel, valAction);
		}

		protected override LibSearchCriteria GetMFCCriteria() {
			return LibSearchCriteria.CreateCriteria("Gv_TransferenciaEntreCuentasBancarias_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
		}

		protected override ILibBusinessComponentWithSearch<IList<TransferenciaEntreCuentasBancarias>, IList<TransferenciaEntreCuentasBancarias>> GetBusinessComponent() {
			return new clsTransferenciaEntreCuentasBancariasNav();
		}

		protected override ILibReportInfo GetDataRetrievesInstance() {
			return new clsTransferenciaEntreCuentasBancariasRpt();
		}

		protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
			return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
		}

		protected override bool HasAccessToModule() {
			return LibSecurityManager.CurrentUserHasAccessToModule(ModuleName.Substring(0,27));
		}

		protected override void InitializeCommands() {
			base.InitializeCommands();
			AnularCommand = new RelayCommand(ExecuteAnularCommand, CanExecuteAnularCommand);
			InsertarCommand = new RelayCommand(ExecuteInsertarCommand, CanExecuteCreateCommand);
			ConsultarCommand = new RelayCommand(ExecuteConsultarCommand, CanExecuteReadCommand);
		}

		protected override void InitializeRibbon() {
			CanPrint = true;
			base.InitializeRibbon();
			if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
				RibbonData.RemoveRibbonControl("Administrar", "Modificar");
				RibbonData.RemoveRibbonControl("Administrar", "Eliminar");
				RibbonData.TabDataCollection[0].AddTabGroupData(CreateRibbonGroup());
			}
		}
		#endregion

		#region Comandos
		private LibRibbonGroupData CreateRibbonGroup() {
			LibRibbonGroupData vResult = new LibRibbonGroupData("Especial");
			vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
				Label = "Anular",
				Command = AnularCommand,
				CommandParameter = eAccionSR.Anular,
				LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/specializedUpdate.png", UriKind.Relative),
				ToolTipDescription = "Anula el elemento actual en la lista.",
				ToolTipTitle = "Anular",
				IsVisible = !LibDefGen.DataBaseInfo.IsReadOnlyRMDB
			});
			return vResult;
		}

		protected override void ExecuteCommandsRaiseCanExecuteChanged() {
			base.ExecuteCommandsRaiseCanExecuteChanged();
			AnularCommand.RaiseCanExecuteChanged();
		}

		private void ExecuteAnularCommand() {
			try {
				TransferenciaEntreCuentasBancariasViewModel vViewModel = CreateNewElement(CurrentItem.GetModel(), eAccionSR.Anular);
				vViewModel.InitializeViewModel(eAccionSR.Anular);
				bool result = LibMessages.EditViewModel.ShowEditor(vViewModel);
				if (result) {
					SearchItems();
				}
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibMessages.RaiseError.ShowError(vEx);
			}
		}

		private void ExecuteInsertarCommand() {
			try {
				TransferenciaEntreCuentasBancariasViewModel vViewModel = CreateNewElement(CurrentItem.GetModel(), eAccionSR.Insertar);
				vViewModel.InitializeViewModel(eAccionSR.Insertar);
				bool result = LibMessages.EditViewModel.ShowEditor(vViewModel);
				if (result) {
					SearchItems();
				}
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibMessages.RaiseError.ShowError(vEx);
			}
		}

		private void ExecuteConsultarCommand() {
			try {
				TransferenciaEntreCuentasBancariasViewModel vViewModel = CreateNewElement(CurrentItem.GetModel(), eAccionSR.Consultar);
				vViewModel.InitializeViewModel(eAccionSR.Consultar);
				bool result = LibMessages.EditViewModel.ShowEditor(vViewModel);
				if (result) {
					SearchItems();
				}
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibMessages.RaiseError.ShowError(vEx);
			}
		}

		protected override bool CanExecuteCreateCommand() {
			return CanCreate && LibSecurityManager.CurrentUserHasAccessTo(ModuleName.Substring(0, 27), "Insertar");
		}

		protected override bool CanExecuteReadCommand() {
			return CurrentItem != null && LibSecurityManager.CurrentUserHasAccessTo(ModuleName.Substring(0, 27), "Consultar");
		}

		private bool CanExecuteAnularCommand() {
			return CurrentItem != null && LibSecurityManager.CurrentUserHasAccessTo(ModuleName.Substring(0, 27), "Anular") && CurrentItem.Status == eStatusTransferenciaBancaria.Vigente;
		}
		#endregion

	} //End of class TransferenciaEntreCuentasBancariasMngViewModel

} //End of namespace Galac.Adm.Uil.Banco

