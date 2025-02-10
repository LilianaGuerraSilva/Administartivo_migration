using Galac.Comun.Uil.TablasGen.ViewModel;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class AuditoriaEnteMngViewModel : LibGenericMngViewModel { 
        #region Propiedades
        public override string ModuleName {
            get { return "Auditoría SENIAT"; }
        }
        public RelayCommand ViewInfoCommand {
            get;
            private set;
        }

        #endregion //Propiedades
        #region Constructores
        public AuditoriaEnteMngViewModel() {
            Title = ModuleName;
        }
        #endregion Constructores

      
        protected override void InitializeCommands() {
            base.InitializeCommands();
            ViewInfoCommand = new RelayCommand(ExecuteViewInfoCommand, CanExecuteViewInfoCommand);

        }
        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            RibbonData.AddTabData(CreateModuleRibbonTab());
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
            }
        }

        private LibRibbonTabData CreateModuleRibbonTab() {
            LibRibbonTabData vResult = new LibRibbonTabData(ModuleName);            
            vResult.GroupDataCollection.Add(CreateConsultRibbonGroup());
            return vResult;
        }
        private LibRibbonGroupData CreateConsultRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Consultar");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Informe",
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/read.png", UriKind.Relative),
                Command = ViewInfoCommand,
                ToolTipDescription = "Informe",
                ToolTipTitle = "Consultas",
                KeyTip = "5"
            });            
            return vResult;
        }

        public bool CanExecuteViewInfoCommand() {
            return true;
        }
        private void ExecuteViewInfoCommand() {
            try {                
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
    }
}

