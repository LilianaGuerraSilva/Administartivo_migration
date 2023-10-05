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
using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;
using System.Xml.Linq;
using LibGalac.Aos.Cnf;
using Galac.Adm.Ccl.DispositivosExternos;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.DefGen;
using Galac.Adm.IntegracionMS.Venta;

namespace Galac.Adm.Uil.Venta.ViewModel {

    public class PagosElectronicosMngViewModel : LibGenericMngViewModel {

        #region Variables

        #endregion

        #region Propiedades

        public override string ModuleName {
            get {
                return "Gestionar V-POS";
            }
        }

        public RelayCommand CierreCommand {
            get;
            private set;
        }

        public RelayCommand PreCierreCommand {
            get;
            private set;
        }

        public RelayCommand ReImprimirCierreCommand {
            get;
            private set;
        }
        public RelayCommand ImprimirUltimoProCommand {
            get;
            private set;
        }
        public RelayCommand ImprimirUltimoAproCommand {
            get;
            private set;
        }

        #endregion //Propiedades

        #region Constructores e Inicializadores

        protected override void InitializeCommands() {
            base.InitializeCommands();
            CierreCommand = new RelayCommand(ExecuteCierreCommand, CanExecuteCierreCommand);
            PreCierreCommand = new RelayCommand(ExecutePreCierreCommand, CanExecutePreCierreCommand);
            ReImprimirCierreCommand = new RelayCommand(ExecuteReImprimirCierreCommand, CanExecuteReImprimirCierreCommand);
            ImprimirUltimoProCommand = new RelayCommand(ExecuteImprimirUltimoProCommand, CanExecuteImprimirUltimoProCommand);
            ImprimirUltimoAproCommand = new RelayCommand(ExecuteImprimirUltimoAproCommand, CanExecuteImprimirUltimoAproCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            RibbonData.AddTabData(CreateModuleRibbonTab());
        }
        private LibRibbonTabData CreateModuleRibbonTab() {
            LibRibbonTabData vResult = new LibRibbonTabData(ModuleName) {
                KeyTip = "G"
            };

            vResult.GroupDataCollection.Add(CreateAccionesRibbonGroup());
            return vResult;
        }

        #endregion //Constructores e Inicializadores

        #region Comandos

        private void ExecuteCierreCommand() {
            C2PMegasoftNav insVueltoMegasoft = new C2PMegasoftNav();            
            insVueltoMegasoft.EjecutaCierre();
        }

        private void ExecutePreCierreCommand() {
            C2PMegasoftNav insVueltoMegasoft = new C2PMegasoftNav();
            insVueltoMegasoft.EjecutaPrecierre();
        }

        private void ExecuteReImprimirCierreCommand() {
            C2PMegasoftNav insVueltoMegasoft = new C2PMegasoftNav();
            insVueltoMegasoft.EjecutaUltimoCierre();
        }

        private void ExecuteImprimirUltimoProCommand() {
            C2PMegasoftNav insVueltoMegasoft = new C2PMegasoftNav();
            insVueltoMegasoft.EjecutaUltimoVoucherProcesado();
        }

        private void ExecuteImprimirUltimoAproCommand() {
            C2PMegasoftNav insVueltoMegasoft = new C2PMegasoftNav();
            insVueltoMegasoft.EjecutaUltimoVoucherAprobado();
        }

        private bool CanExecuteCierreCommand() {
            return true; 
        }

        private bool CanExecutePreCierreCommand() {
            return true; 
        }

        private bool CanExecuteReImprimirCierreCommand() {
            return true; 
        }

        private bool CanExecuteImprimirUltimoProCommand() {
            return true; 
        }

        private bool CanExecuteImprimirUltimoAproCommand() {
            return true; 
        }

        private LibRibbonGroupData CreateAccionesRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Acciones");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Cierre",
                Command = CierreCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/Close.png", UriKind.Relative),
                ToolTipDescription = "Cierre",
                ToolTipTitle = "Cierre"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Pre-Cierre",
                Command = PreCierreCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/Close.png", UriKind.Relative),
                ToolTipDescription = "Pre-Cierre",
                ToolTipTitle = "Pre-Cierre"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Re-Imprimir Cierre",
                Command = ReImprimirCierreCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/Print.png", UriKind.Relative),
                ToolTipDescription = "Re-Imprimir Cierre",
                ToolTipTitle = "Re-Imprimir Cierre"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Imprimir Último Voucher Procesado",
                Command = ImprimirUltimoProCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/Print.png", UriKind.Relative),
                ToolTipDescription = "Imprimir Último Voucher Procesado",
                ToolTipTitle = "Imprimir Último Voucher Procesado"
                //KeyTip = "A"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Imprimir Último Voucher Aprobado",
                Command = ImprimirUltimoAproCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/Print.png", UriKind.Relative),
                ToolTipDescription = "Imprimir Último Voucher Aprobado",
                ToolTipTitle = "Imprimir Último Voucher Aprovado"
                //KeyTip = "A"
            });
            return vResult;
        }
        #endregion

        #region Metodos

        #endregion //Metodos 

    } //End of class CajaAperturaMngViewModel

} //End of namespace Galac.Adm.Uil.Venta

