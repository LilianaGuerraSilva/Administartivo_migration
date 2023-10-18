using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public abstract class CobroRapidoViewModelBase : LibGenericViewModel {

        #region Variables y Constantes

        protected const string TotalFacturaPropertyName = "TotalFactura";
        protected const string TotalCobradoPropertyName = "TotalCobrado";
        protected const string MontoRestantePorPagarPropertyName = "MontoRestantePorPagar";

        private int _ConsecutivoCompania;
        private string _NumeroFactura;
        private eTipoDocumentoFactura _TipoDeDocumento;
        private DateTime _FechaDeFactura;
        private decimal _TotalFactura;
        private decimal _TotalEnEfectivo;
        private string _CodigoCliente;
        private decimal _MontoRestantePorPagar;
        protected FacturaRapida insFactura;
		
        #endregion //Variables y Constantes

        #region Constructor e Inicializadores
        public CobroRapidoViewModelBase() {
            MontoRestantePorPagar = TotalFactura;
            TotalCobrado = 0;
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            CobrarCommand = new RelayCommand(ExecuteCobrarCommand, CanExecuteCobrarCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
        }

        #endregion

        #region Propiedades

        public int ConsecutivoCompania {
            get {
                return _ConsecutivoCompania;
            }
            set {
                if (_ConsecutivoCompania != value) {
                    _ConsecutivoCompania = value;
                }
            }
        }

        public eTipoDocumentoFactura TipoDeDocumento {
            get {
                return _TipoDeDocumento;
            }
            set {
                if (_TipoDeDocumento != value) {
                    _TipoDeDocumento = value;
                }
            }
        }

        public string NumeroFactura {
            get {
                return _NumeroFactura;
            }
            set {
                if (_NumeroFactura != value) {
                    _NumeroFactura = value;
                }
            }
        }

        public DateTime FechaDeFactura {
            get {
                return _FechaDeFactura;
            }
            set {
                if (_FechaDeFactura != null) {
                    _FechaDeFactura = value;
                }
            }
        }

        public string CodigoCliente {
            get {
                return _CodigoCliente;
            }
            set {
                if (_CodigoCliente != value) {
                    _CodigoCliente = value;
                }
            }
        }

        public decimal TotalFactura {
            get {
                return _TotalFactura;
            }
            set {
                if (_TotalFactura != value) {
                    _TotalFactura = value;
                    RaisePropertyChanged(TotalFacturaPropertyName);
                }
            }
        }

        public decimal TotalCobrado {
            get {
                return _TotalEnEfectivo;
            }
            set {
                if (_TotalEnEfectivo != value) {
                    _TotalEnEfectivo = value;
                    RaisePropertyChanged(TotalCobradoPropertyName);
                }
            }
        }

        [LibCustomValidation("MontoRestantePorPagarValidating")]
        public decimal MontoRestantePorPagar {
            get {
                return _MontoRestantePorPagar;
            }
            set {
                if (_MontoRestantePorPagar != value) {
                    _MontoRestantePorPagar = value;
                    RaisePropertyChanged(MontoRestantePorPagarPropertyName);
                    CobrarCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public RelayCommand CobrarCommand {
            get;
            private set;
        }

        #endregion //Propiedades

        #region Validation

        private ValidationResult MontoRestantePorPagarValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (MontoRestantePorPagar <= 0) {
                return ValidationResult.Success;
            } else {
                vResult = new ValidationResult("No puede grabar sin introducir una forma de pago");
            }
            return vResult;
        }

        #endregion //Validation

        #region Metodos

        protected virtual LibRibbonGroupData CreateCobrarRibbonButtonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Comandos");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Cobrar",
                Command = CobrarCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F6.png", UriKind.Relative),
                ToolTipDescription = "Guarda los cambios en " + ModuleName + ".",
                ToolTipTitle = "Ejecutar Acción (F6)",
                IsVisible = true,
                KeyTip = "F6"
            });
            return vResult;
        }

        protected virtual void ActualizarCamposEnFactura(XElement xElementFacturaRapida, string valComprobanteFiscal, string valSerialMaquinaFiscal,List<RenglonCobroDeFactura> ListDeCobro) {
            new clsCobroDeFacturaRapidaNav().ActualizarCamposEnFactura(xElementFacturaRapida, valComprobanteFiscal, valSerialMaquinaFiscal, ListDeCobro);
        }

        protected abstract void ExecuteCobrarCommand();
        protected abstract bool CanExecuteCobrarCommand();
        public abstract void CalcularTotales();

        #endregion //Metodos
    }
}
