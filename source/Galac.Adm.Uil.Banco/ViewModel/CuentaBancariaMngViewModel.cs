using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.Catching;
using LibGalac.Aos.ARRpt.Reports;
using Galac.Adm.Brl.Banco;
using Galac.Adm.Brl.Banco.Reportes;
using Galac.Adm.Ccl.Banco;
using System.Xml;
using LibGalac.Aos.Uil;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Uil.Banco.ViewModel {

    public class CuentaBancariaMngViewModel : LibMngViewModelMfc<CuentaBancariaViewModel, CuentaBancaria> {
        #region Propiedades

        public override string ModuleName {
            get { return "Cuenta Bancaria"; }
        }
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public LibXmlMemInfo AppMemoryInfo { get; set; }
        */
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores

        public CuentaBancariaMngViewModel()
            : base(LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Title = "Buscar " + ModuleName;
            OrderByMember = "ConsecutivoCompania, Codigo";
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            OrderByDirection = "DESC";
            AppMemoryInfo = LibGlobalValues.Instance.GetAppMemInfo();
        */
        #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override CuentaBancariaViewModel CreateNewElement(CuentaBancaria valModel, eAccionSR valAction) {
            var vNewModel = valModel;
            if (vNewModel == null) {
                vNewModel = new CuentaBancaria();
            }
            return new CuentaBancariaViewModel(vNewModel, valAction);
        }

        protected override LibSearchCriteria GetMFCCriteria() {
            return LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
        }

        protected override ILibBusinessComponentWithSearch<IList<CuentaBancaria>, IList<CuentaBancaria>> GetBusinessComponent() {
            return new clsCuentaBancariaNav();
        }

        protected override ILibReportInfo GetDataRetrievesInstance() {
            return new clsCuentaBancariaRpt();
        }

        protected override ILibRpt GetReportForList(string valModuleName, ILibReportInfo valReportInfo, LibCollFieldFormatForGrid valFieldsFormat, LibGpParams valParams) {
            return new LibGenericList(valModuleName, valReportInfo, valFieldsFormat, valParams);
        }

        protected override void InitializeCommands() {
           base.InitializeCommands();
           RecalcularSaldoCommand = new RelayCommand(ExecuteRecalcularSaldoCommand, CanExecuteRecalcularSaldoCommand);
        }

        protected override void InitializeRibbon() {
           base.InitializeRibbon();

           if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
              RibbonData.TabDataCollection[0].AddTabGroupData(CreateRecalcularSaldoRibbonGroup());
           }
        }
        #endregion //Metodos Generados
        protected override void ExecuteCommandsRaiseCanExecuteChanged() {
           base.ExecuteCommandsRaiseCanExecuteChanged();
           RecalcularSaldoCommand.RaiseCanExecuteChanged();
        }

        private LibRibbonGroupData CreateRecalcularSaldoRibbonGroup() {
           LibRibbonGroupData vResult = new LibRibbonGroupData("Recalcular");
           vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
              Label = "Recalcular",
              Command = RecalcularSaldoCommand,
              LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/edit.png", UriKind.Relative),
              ToolTipDescription = "Recalcula el saldo de todas las cuentas bancarias.",
              ToolTipTitle = "Recalcular Saldos Cuentas Bancarias",
              IsVisible = !LibDefGen.DataBaseInfo.IsReadOnlyRMDB
           });
           return vResult;
        }

        public RelayCommand RecalcularSaldoCommand {
           get;
           private set;
        }

        private bool CanExecuteRecalcularSaldoCommand() {
           return CurrentItem != null && LibSecurityManager.CurrentUserHasAccessTo(ModuleName, "Recalcular");
        }

        private void ExecuteRecalcularSaldoCommand() {
           try {
              Galac.Adm.Ccl.Banco.ICuentaBancariaPdn insCtaBanNav = new Galac.Adm.Brl.Banco.clsCuentaBancariaNav();
              bool vResult = false;
              if (insCtaBanNav.ExistenMovimientosCuentaBancaria(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"))) {
                 CuentaBancariaViewModel vViewModel = CreateNewElementForRecalcular(CurrentItem.GetModel(), eAccionSR.Recalcular);
                 vViewModel.InitializeViewModel(eAccionSR.Recalcular);

                 vResult = insCtaBanNav.RecalculaSaldoCuentasBancarias(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                 if (vResult) {
                    LibMessages.MessageBox.Information(this, "Recálculo de Saldos de Cuentas Bancarias exitoso.", "Recalcular Saldos de las Cuentas Bancarias");
                 } 
              } else {
                 LibMessages.MessageBox.Information(this, "No existen registros para Recalcular.", "Recalcular Saldos de las Cuentas Bancarias");
              }

           } catch (System.AccessViolationException) {
              throw;
           } catch (System.Exception vEx) {
              LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
           }
        }

        CuentaBancariaViewModel CreateNewElementForRecalcular(CuentaBancaria valModel, eAccionSR valAction) {
           var vNewModel = valModel;
           return new CuentaBancariaViewModel(vNewModel, valAction);
        }

    } //End of class CuentaBancariaMngViewModel

} //End of namespace Galac.Adm.Uil.Banco

