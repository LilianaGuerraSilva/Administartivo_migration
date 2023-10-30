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
        private eIdFiscalPM _IDFiscal;
        private string _NombreCliente;
        private string _NroFactura;
        private string _Monto;
        private string _Rif;
        private eCodigoCel _CodigoTelefono;
        private string _NumeroTelefono;
        private eBancoPM _Banco;
        private decimal _Vuelto;
        private string _CodigoAfiliacion;
        #endregion
        #region Propiedades
        public override string ModuleName {
            get { return "Vuelto por Pago Móvil C2P"; }
        }

        public string NombreCliente {
            get { return _NombreCliente; }
            set {
                if (_NombreCliente != value) {
                    _NombreCliente = value;
                    RaisePropertyChanged(() => NombreCliente);
                }
            }
        }

        public string NroFactura {
            get { return _NroFactura; }
            set {
                if (_NroFactura != value) {
                    _NroFactura = value;
                    RaisePropertyChanged(() => NroFactura);
                }
            }
        }

        public string Monto {
            get { return _Monto; }
            set {
                if (_Monto != value) {
                    _Monto = value;
                    RaisePropertyChanged(() => Monto);
                }
            }
        }

        public eIdFiscalPM IDFiscal {
            get { return _IDFiscal; }
            set {
                if (_IDFiscal != value) {
                    _IDFiscal = value;
                    RaisePropertyChanged(() => IDFiscal);
                }
            }
        }

        public string Rif {
            get { return _Rif; }
            set {
                if (_Rif != value) {
                    _Rif = value;
                    RaisePropertyChanged(() => Rif);
                }
            }
        }

        public eCodigoCel CodigoTelefono {
            get { return _CodigoTelefono; }
            set {
                if (_CodigoTelefono != value) {
                    _CodigoTelefono = value;
                    RaisePropertyChanged(() => CodigoTelefono);
                }
            }
        }

        public string NumeroTelefono {
            get { return _NumeroTelefono; }
            set {
                if (_NumeroTelefono != value) {
                    _NumeroTelefono = value;
                    RaisePropertyChanged(() => NumeroTelefono);
                }
            }
        }

        public eBancoPM Banco {
            get { return _Banco; }
            set {
                if (_Banco != value) {
                    _Banco = value;
                    RaisePropertyChanged(() => Banco);
                }
            }
        }

        public decimal Vuelto {
            get { return _Vuelto; }
            set {
                if (_Vuelto != value) {
                    _Vuelto = value;
                    RaisePropertyChanged(() => Vuelto);
                }
            }
        }

        public string CodigoAfiliacion {
            get { return _CodigoAfiliacion; }
            set {
                if (_CodigoAfiliacion != value) {
                    _CodigoAfiliacion = value;
                    RaisePropertyChanged(() => CodigoAfiliacion);
                }
            }
        }
        public string NumeroControl { get; set; }
        public bool EstadoProceso { get; set; }

        public eCodigoCel[] ArrayCodigoCel { get { return LibEnumHelper<eCodigoCel>.GetValuesInArray(); } }
        public eBancoPM[] ArrayBancoPM { get { return LibEnumHelper<eBancoPM>.GetValuesInArray(); } }
        public eIdFiscalPM[] ArrayIdFiscalPM { get { return LibEnumHelper<eIdFiscalPM>.GetValuesInArray(); } }
        #endregion //Propiedades
        #region Constructores
        public C2PMegasoftViewModel(string initCodigoCliente, string initNroFactura, decimal initMonto) {
            NombreCliente = initCodigoCliente;
            NroFactura = initNroFactura;
            Monto = LibConvert.NumToString(LibMath.Abs(initMonto), 2);            
        }
        #endregion //Constructores
        #region Metodos Generados

        public RelayCommand PagarCommand { get; private set; }
        public RelayCommand LimpiarCommand { get; private set; }
        public bool IsVisibleAfiliacionControl { get { return true; } }

        protected override void InitializeLookAndFeel() {
            CodigoAfiliacion = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoAfiliacionC2PMegasoft");
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
            try {
                C2PMegasoftNav megasoftNav = new C2PMegasoftNav();
                string vCedula = LibEnumHelper.GetDescription(IDFiscal) + Rif;
                string vTelefono = LibEnumHelper.GetDescription(CodigoTelefono) + NumeroTelefono;
                string vCodigoBanco = LibText.Mid(LibEnumHelper.GetDescription(Banco), 0, 4);
                string vVuelto = LibConvert.NumberWithCommasAndDot(Vuelto, LibConvert.ToByte(Vuelto.ToString().Length+1) , 2, eAlignmentType.Right).Replace(",","");
                string vNumeroControl = "";
                //LibResponse vResult = megasoftNav.EjecutaCambioPagoMovil(vCedula, vTelefono, vCodigoBanco, vVuelto, NroFactura, ref vNumeroControl);
                //if (vResult != null) {
                //    EstadoProceso = vResult.Success;
                //    NumeroControl = vNumeroControl;
                //}
                RaiseRequestCloseEvent();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private bool CanExecuteLimpiarCommand() { return true; }
        private bool CanExecuteCobrarCommand() { return true; }

        

         //End of class C2PMegasoftViewModel
    }
} //End of namespace Galac.Adm.Uil.Venta.ViewModel