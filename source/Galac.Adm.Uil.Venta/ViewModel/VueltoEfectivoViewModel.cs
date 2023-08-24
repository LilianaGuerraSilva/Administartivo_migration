using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Command;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class VueltoEfectivoViewModel : LibGenericViewModel {
        public override string ModuleName { get { return "Vuelto en Efectivo"; } }
        private const string VueltoEfectivoPropertyName = "VueltoEfectivo";
        public string CambioAMonedaLocalParaMostrar {
            get { return LibConvert.NumToString(CambioAMonedaLocal, 4); }
        }

        decimal _CambioAMonedaLocal;
        public decimal CambioAMonedaLocal{
            get { return _CambioAMonedaLocal; }
            set {
                if (_CambioAMonedaLocal != value) {
                    _CambioAMonedaLocal = value;
                    RaisePropertyChanged(() => CambioAMonedaLocal);
                } 
            }
        }

        public string PorVueltoMonedaLocalParaMostrar {
            get { return LibConvert.NumToString(PorVueltoMonedaLocal, 2); }
        }

        decimal _PorVueltoMonedaLocal;
        public decimal PorVueltoMonedaLocal {
            get { return _PorVueltoMonedaLocal; }
            set {
                if (_PorVueltoMonedaLocal != value) {
                    _PorVueltoMonedaLocal = value;
                    RaisePropertyChanged(() => _PorVueltoMonedaLocal);
                }
            }
        }

        public string PorVueltoDivisaParaMostrar {
            get { return LibConvert.NumToString(PorVueltoDivisa, 2); }
        }

        decimal _PorVueltoDivisa;
        public decimal PorVueltoDivisa {
            get { return _PorVueltoDivisa; }
            set {
                if (_PorVueltoDivisa != value) {
                    _PorVueltoDivisa = value;
                    RaisePropertyChanged(() => _PorVueltoDivisa);
                }
            }
        }

        string _NombreDeMonedaLocal;
        public string NombreDeMonedaLocal {
            get { return _NombreDeMonedaLocal; }
            set { _NombreDeMonedaLocal = value; }
        }

        string _NombreDeDivisa;
        public string NombreDeDivisa {
            get { return _NombreDeDivisa; }
            set { _NombreDeDivisa = value; }
        }

        decimal _EfectivoMonedaLocal;
        public decimal EfectivoMonedaLocal {
            get { return _EfectivoMonedaLocal; }
            set { _EfectivoMonedaLocal = value; }
        }

        decimal _EfectivoMonedaDivisa;

        public decimal EfectivoMonedaDivisa {
            get { return _EfectivoMonedaDivisa; }
            set { _EfectivoMonedaDivisa = value; }
        }

        public VueltoEfectivoViewModel(decimal initCambioAMonedaLocal, decimal initPorVueltoMonedaLocal, decimal initPorVueltoDivisa, string initNombreDeMonedaLocal, string initNombreDeDivisa) {
            CambioAMonedaLocal = initCambioAMonedaLocal;
            PorVueltoMonedaLocal = -1 * initPorVueltoMonedaLocal;
            PorVueltoDivisa = -1 * initPorVueltoDivisa;
            NombreDeMonedaLocal = initNombreDeMonedaLocal;
            NombreDeDivisa = initNombreDeDivisa;
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateVueltoEfectivoRibbonButtonGroup());
                var tempRibbon = RibbonData.TabDataCollection[0].GroupDataCollection[0];
                RibbonData.TabDataCollection[0].GroupDataCollection[0] = RibbonData.TabDataCollection[0].GroupDataCollection[1];
                RibbonData.TabDataCollection[0].GroupDataCollection[1] = tempRibbon;
            }
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            PagarCommand = new RelayCommand(ExecuteCobrarCommand, CanExecuteCobrarCommand);
            LimpiarCommand = new RelayCommand(ExecuteLimpiarCommand, CanExecuteLimpiarCommand);
        }

        public RelayCommand PagarCommand { get; private set; }
        public RelayCommand LimpiarCommand { get; private set; }

        LibRibbonGroupData CreateVueltoEfectivoRibbonButtonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Comandos");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Pagar",
                Command = PagarCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F6.png", UriKind.Relative),
                ToolTipDescription = "Guarda los cambios en " + ModuleName + ".",
                ToolTipTitle = "Ejecutar Acción (F6)",
                IsVisible = true,
                KeyTip = "F6"
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Limpiar",
                Command = LimpiarCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F7.png", UriKind.Relative),
                ToolTipDescription = "Guarda los cambios en " + ModuleName + ".",
                ToolTipTitle = "Ejecutar Acción (F7)",
                IsVisible = true,
                KeyTip = "F7"
            });
            return vResult;
        }

        private void ExecuteLimpiarCommand() {
            EfectivoMonedaDivisa = 0;
            EfectivoMonedaLocal = 0;
            RaiseMoveFocus(VueltoEfectivoPropertyName);
        }

        private void ExecuteCobrarCommand() {
            //RaiseMoveFocus(EfectivoEnMonedaLocalPropertyName);
        }

        private bool CanExecuteLimpiarCommand() { return true; }
        private bool CanExecuteCobrarCommand() { return true; }

    }
}