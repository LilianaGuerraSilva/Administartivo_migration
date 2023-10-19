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
        private readonly string vWinDir = LibIO.AddSlashCharToEndOfPathIfRequired(LibApp.WinDir());
        C2PMegasoftNav insMegasoft = new C2PMegasoftNav();
        string vRutaMegasoft;
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

        public RelayCommand UltimoCierreCommand {
            get;
            private set;
        }
        public RelayCommand UltimoVoucherProcesadoCommand {
            get;
            private set;
        }
        public RelayCommand UltimoVoucherAprobadoCommand {
            get;
            private set;
        }

        public RelayCommand AnularTransaccionCommand {
            get;
            private set;
        }

        public RelayCommand RutaMegasoftCommand {
            get;
            private set;
        }
        #endregion //Propiedades

        #region Constructores e Inicializadores

        protected override void InitializeCommands() {
            base.InitializeCommands();
            CierreCommand = new RelayCommand(ExecuteCierreCommand, CanExecuteCierreCommand);
            PreCierreCommand = new RelayCommand(ExecutePreCierreCommand, CanExecutePreCierreCommand);
            UltimoCierreCommand = new RelayCommand(ExecuteUltimoCierreCommand, CanExecuteUltimoCierreCommand);
            UltimoVoucherProcesadoCommand = new RelayCommand(ExecuteUltimoVoucherProcesadoCommand, CanExecuteUltimoVoucherProcesadoCommand);
            UltimoVoucherAprobadoCommand = new RelayCommand(ExecuteUltimoVoucherAprobadoCommand, CanExecuteUltimoVoucherAprobadoCommand);
            AnularTransaccionCommand = new RelayCommand(ExecuteAnularTransaccionCommand, CanExecuteAnularTransaccionCommand);
            RutaMegasoftCommand = new RelayCommand(ExecuteRutaMegasoftCommand, CanExecuteRutaMegasoftCommand);
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

        private void ExecuteUltimoCierreCommand() {
            vRutaMegasoft = RutaMegasoft(true);
            insMegasoft.EjecutaUltimoCierre();
            if (LibIO.DirExists(vRutaMegasoft)) {
                string valPathAndFileNameWithExtension = System.IO.Path.Combine(vRutaMegasoft, insMegasoft.infoAdicional);
                if (LibIO.FileExists(vRutaMegasoft + @"\" + insMegasoft.infoAdicional)) {
                    System.Diagnostics.Process.Start(valPathAndFileNameWithExtension);
                } else {
                    LibMessages.MessageBox.Alert(this, "El Archivo " + vRutaMegasoft + @"\" + insMegasoft.infoAdicional + ".txt no existe", "Advertencia");
                }
            } else {
                LibMessages.MessageBox.Alert(this,"La ruta " + vRutaMegasoft + " no existe","Advertencia");
            }
        }

        private void ExecuteUltimoVoucherProcesadoCommand() {
            vRutaMegasoft = RutaMegasoft(false);
            insMegasoft.EjecutaUltimoVoucherProcesado();
            if (LibIO.DirExists(vRutaMegasoft)) {
                string valPathAndFileNameWithExtension = System.IO.Path.Combine(vRutaMegasoft, insMegasoft.infoAdicional);
                if (LibIO.FileExists(valPathAndFileNameWithExtension)) {
                    System.Diagnostics.Process.Start(valPathAndFileNameWithExtension);
                } else {
                    LibMessages.MessageBox.Alert(this, "El archivo " + valPathAndFileNameWithExtension + " no existe o no pudo ser encontrado.", "Advertencia");
                }
            } else {
                LibMessages.MessageBox.Alert(this, "La ruta " + vRutaMegasoft + " no existe.", "Advertencia");
            }
        }

        private void ExecuteUltimoVoucherAprobadoCommand() {
            vRutaMegasoft = RutaMegasoft(false);
            insMegasoft.EjecutaUltimoVoucherAprobado();
            if (LibIO.DirExists(vRutaMegasoft)) {
                string valPathAndFileNameWithExtension = System.IO.Path.Combine(vRutaMegasoft, insMegasoft.infoAdicional);
                if (LibIO.FileExists(valPathAndFileNameWithExtension)) {
                    System.Diagnostics.Process.Start(valPathAndFileNameWithExtension);
                } else {
                    LibMessages.MessageBox.Alert(this, "El archivo " + valPathAndFileNameWithExtension + " no existe o no pudo ser encontrado.", "Advertencia");
                }
            } else {
                LibMessages.MessageBox.Alert(this, "La ruta " + vRutaMegasoft + " no existe.", "Advertencia");
            }
        }

        private void ExecuteAnularTransaccionCommand() {
            try {
                C2PMegasoftNav insMegasoft = new C2PMegasoftNav();
                if (insMegasoft.EjecutaAnularTransaccion()) {
                    if (insMegasoft.montoTransaccion > 0) {
                        LibMessages.MessageBox.Information(this, "Anulación procesada exitosamente", "Anulacion Transacción");
                    }
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ExecuteRutaMegasoftCommand() {
            vRutaMegasoft = RutaMegasoft(false);
            if (LibIO.DirExists(vRutaMegasoft)) {
                LibDiagnostics.Shell(vWinDir + "explorer.exe", vRutaMegasoft, false, 1, System.Diagnostics.ProcessWindowStyle.Maximized, true);
            } else {
                LibDiagnostics.Shell(vWinDir + "explorer.exe", LibWorkPaths.LogicUnitDir, false, 1, System.Diagnostics.ProcessWindowStyle.Maximized, true);
            }
        }

        private bool CanExecuteCierreCommand() {
            return true; 
        }

        private bool CanExecutePreCierreCommand() {
            return true; 
        }

        private bool CanExecuteUltimoCierreCommand() {
            return true; 
        }

        private bool CanExecuteUltimoVoucherProcesadoCommand() {
            return true; 
        }

        private bool CanExecuteUltimoVoucherAprobadoCommand() {
            return true; 
        }

        private bool CanExecuteAnularTransaccionCommand() {
            return true;
        }

        private bool CanExecuteRutaMegasoftCommand() {
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
                Label = "Ver Último Cierre",
                Command = UltimoCierreCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/Print.png", UriKind.Relative),
                ToolTipDescription = "Ver Último Cierre",
                ToolTipTitle = "Ver Último Cierre"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Ver Último Voucher Procesado",
                Command = UltimoVoucherProcesadoCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/Print.png", UriKind.Relative),
                ToolTipDescription = "Ver Último Voucher Procesado",
                ToolTipTitle = "Ver Último Voucher Procesado"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Ver Último Voucher Aprobado",
                Command = UltimoVoucherAprobadoCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/Print.png", UriKind.Relative),
                ToolTipDescription = "Ver Último Voucher Aprobado",
                ToolTipTitle = "Ver Último Voucher Aprovado"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Anular Transacción",
                Command = AnularTransaccionCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/deleteImage.png", UriKind.Relative),
                ToolTipDescription = "Anular Transacción",
                ToolTipTitle = "Anular Transacción"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Abrir Ubicación",
                Command = RutaMegasoftCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/Read.png", UriKind.Relative),
                ToolTipDescription = "Ubicación de Vouchers y Cierres",
                ToolTipTitle = "Ubicación de Vouchers y Cierres"
            });
            return vResult;
        }
        #endregion

        #region Metodos

        private string RutaMegasoft(bool valUltimoCierre) {
            if (valUltimoCierre) {
                vRutaMegasoft = System.IO.Path.Combine(LibWorkPaths.ProgramDir, "Megasoft", "voucher", "cierres");
            } else {
                vRutaMegasoft = System.IO.Path.Combine(LibWorkPaths.ProgramDir, "Megasoft", "voucher");
            }
            return vRutaMegasoft;
        }

        #endregion //Metodos 

    } //End of class CajaAperturaMngViewModel

} //End of namespace Galac.Adm.Uil.Venta

