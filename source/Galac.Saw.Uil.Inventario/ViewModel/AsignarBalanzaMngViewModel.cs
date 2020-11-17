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
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Brl.Inventario.Reportes;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Uil.Inventario.ViewModel {
    
    public class AsignarBalanzaMngViewModel : LibGenericMngViewModel {
        private bool _IsShow = false;
        #region Propiedades

        public override string ModuleName {
            get { return "Inventario Asignar Balanza"; }
        }

        public RelayCommand ShowViewCommand {
            get;
            private set;
        }

        private bool CanExecuteShowViewCommand() {
            return true;
        }
        
        #endregion //Propiedades
        #region Constructores

        public AsignarBalanzaMngViewModel(){          
        }
        #endregion //Constructores
        #region Metodos Generados        
        
        protected override void InitializeCommands() {
            base.InitializeCommands();
            ShowViewCommand = new RelayCommand(ExecuteShowViewCommand, CanExecuteShowViewCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            LibRibbonTabData LibRibbonTabDataItem = new LibRibbonTabData();
            LibRibbonTabDataItem.AddTabGroupData(new LibRibbonGroupData("Administrar"));
            LibRibbonTabDataItem.Header = "Inventario";
            LibRibbonTabDataItem.IsVisible = true;
            RibbonData.TabDataCollection.Add(LibRibbonTabDataItem);
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].GroupDataCollection[0].AddRibbonControlData(CreateShowViewRibbonButtonData());
            }
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
       
        #endregion //Codigo Ejemplo         
     
        private LibRibbonButtonData CreateShowViewRibbonButtonData() {
            return new LibRibbonButtonData() {
                Label = "Insertar",
                Command = ShowViewCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/add.png", UriKind.Relative),
                ToolTipDescription = "Insertar",
                ToolTipTitle = "Insertar"
            };
        }

        public void ExecuteShowViewCommand() {
            if (!_IsShow) {
                _IsShow = true;
                AsignarBalanzaViewModel vViewModel = new AsignarBalanzaViewModel();
                _IsShow= LibMessages.EditViewModel.ShowEditor(vViewModel, true);                
            }
        }
    } //End of class InventarioAsignarBalanzaMngViewModel

} //End of namespace Galac.Saw.Uil.Inventario

