using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Galac.Adm.Uil.Venta.ViewModel {
    internal class DatosVPOS {
        internal decimal MontoTransaccion { get; set; }
        internal string CedulaRif { get; set; }
    }
    public class DatosVPosViewModel : LibGenericViewModel {
        #region Constantes
        public const string CedulaRifPropertyName = "CedulaRif";
        public const string MontoPropertyName = "Monto";
        public Action<bool> vResultCobroTDDTDC;
        #endregion

        #region Variables
        string _CedulaRif;
        decimal _Monto;
        decimal vMontoPorCobrar;
        bool vCancel = false;
        #endregion

        #region Propiedades

        public override string ModuleName {
            get { return "Cobro con Tajeta"; }
        }

        public string CedulaRif {
            get {
                return _CedulaRif;
            }
            set {
                if (_CedulaRif != value) {
                    _CedulaRif = value;
                    RaisePropertyChanged(CedulaRifPropertyName);
                }
            }
        }

        [LibCustomValidation("MontoValidating")]
        public decimal Monto {
            get {
                return _Monto;
            }
            set {
                if (_Monto != value) {
                    _Monto = value;
                    RaisePropertyChanged(MontoPropertyName);
                }
            }
        }

        public RelayCommand ContinuarCommand { get; private set; }

        #endregion //Propiedades

        #region Constructores

        public DatosVPosViewModel()
            : base() {
        }
        #endregion //Constructores

        #region Metodos Generados

        internal void InitLookAndFeel(string cedulaRif, decimal valMonto) {
            CedulaRif = cedulaRif;
            Monto = valMonto;
            vMontoPorCobrar = LibConvert.ToDec(valMonto, 2);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ContinuarCommand = new RelayCommand(ExecuteInsertCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            RibbonData.ApplicationMenuData = new LibRibbonMenuButtonData() {
                IsVisible = false
            };
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection.Insert(0, CreateActionRibbonButton());
            }
        }

        protected override void ExecuteCancel() {
            vCancel = true;
            vMontoPorCobrar = 0;
            Monto = 0;
            CedulaRif = "";
            base.ExecuteCancel();
        }

        public void InitializeViewModel(string cedulaRif, decimal valMonto) {
            CedulaRif = cedulaRif;
            Monto = valMonto;
            vMontoPorCobrar = LibConvert.ToDec(valMonto,2);
        }

        private LibRibbonButtonData CreateActionRibbonButton() {
            LibRibbonButtonData vResult = new LibRibbonButtonData() {
                Label = "Continuar",
                Command = ContinuarCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/saveAndClose.png", UriKind.Relative),
                ToolTipDescription = "Continuar",
                ToolTipTitle = "Continuar",
                IsVisible = true
            };
            return vResult;
        }

        private List<DatosVPOS> ListaDatosVPOS { get; set; }

        public void ExecuteInsertCommand() {
            if (Monto > 0 && Monto <= vMontoPorCobrar) {
                ListaDatosVPOS.Add(new DatosVPOS() {
                    MontoTransaccion = LibConvert.ToDec(Monto, 2),
                    CedulaRif = CedulaRif,
                });
                base.ExecuteCancel();
            } else {
                LibMessages.MessageBox.Alert(this,"Monto a cobrar debe ser Mayor que 0 y Menor o igual al monto pendiente por cobrar: " + LibMath.RoundToNDecimals(vMontoPorCobrar, 2) + ".", "Advertencia");
            }
        }

        public DatosVPosViewModel(decimal valMonto) {
            Monto = valMonto;
            CedulaRif = "";
            ListaDatosVPOS = new List<DatosVPOS>();
        }
        #endregion //Metodos Generados

        #region Métodos Programados
        #endregion
        #region Validaciones
        private ValidationResult MontoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Monto > 0 && Monto <= vMontoPorCobrar)) {
                return ValidationResult.Success;
            } 
            return vResult;
        }
        #endregion
    } //End of class DatosVPosViewModel

} //End of namespace Galac.Adm.Uil.Venta

