using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.IntegracionMS.Venta {
    public class C2PMegasoftViewModel : LibGenericViewModel {
        #region Constantes
        #endregion
        #region Propiedades
        public override string ModuleName {
            get { return "Vuelto por Pago Móvil C2P"; }
        }

        public string NombreCliente { get; set; }
        public string NroFactura { get; set; }
        public string Monto { get; set; }
        public eIdFiscalPM IDFiscal { get; set; }
        public string Rif { get; set; }
        public eCodigoCel CodigoTelefono { get; set; }
        public string NumeroTelefono { get; set; }
        public eBancoPM Banco { get; set; }
        public decimal Vuelto { get; set; }
        public string CodigoAfiliacion { get; set; }

        public eCodigoCel[] ArrayCodigoCel { get { return LibEnumHelper<eCodigoCel>.GetValuesInArray(); } }
        public eBancoPM[] ArrayBancoPM { get { return LibEnumHelper<eBancoPM>.GetValuesInArray(); } }
        public eIdFiscalPM[] ArrayIdFiscalPM { get { return LibEnumHelper<eIdFiscalPM>.GetValuesInArray(); } }
        #endregion //Propiedades
        #region Constructores
        public C2PMegasoftViewModel(string initNombreCliente, string initNroFactura, decimal initMonto) {
            NombreCliente = initNombreCliente;
            NroFactura = initNroFactura;
            Monto = LibConvert.NumToString(LibMath.Abs(initMonto), 2);
            CodigoAfiliacion = "{código de afiliación}";
        }
        #endregion //Constructores
        #region Metodos Generados

        public RelayCommand PagarCommand { get; private set; }
        public RelayCommand LimpiarCommand { get; private set; }
        public bool IsVisibleAfiliacionControl { get { return true; } }

        protected override void InitializeLookAndFeel() {
        }
        #endregion //Metodos Generados
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
            IDFiscal = eIdFiscalPM.V;
            Rif = string.Empty;
            CodigoTelefono = eCodigoCel.Cod_0412;
            NumeroTelefono = string.Empty;
            Banco = eBancoPM.Bco_0102;
            Vuelto = 0M;
        }

        private void ExecuteCobrarCommand() {
            //cerrar
        }

        private bool CanExecuteLimpiarCommand() { return true; }
        private bool CanExecuteCobrarCommand() { return true; }

    } //End of class C2PMegasoftViewModel

} //End of namespace Galac.Adm.Uil.Venta.ViewModel