using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using System;
using System.Collections.Generic;

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
        #endregion

        #region Propiedades

        public override string ModuleName {
            get { return "Datos V-Pos"; }
        }

        [LibRequired(ErrorMessage = "El campo CedulaRif es requerido.")]
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

        [LibRequired(ErrorMessage = "El campo Monto es requerido.")]
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

        public RelayCommand ContinuarCommand {
            get;
            private set;
        }

        #endregion //Propiedades

        #region Constructores

        public DatosVPosViewModel()
            : base() {
        }
        #endregion //Constructores

        #region Metodos Generados

        internal void InitLookAndFeel(decimal valMonto) {
            CedulaRif = "";
            Monto = valMonto;
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

        public void InitializeViewModel(decimal valMonto) {
            CedulaRif = "";
            Monto = valMonto;
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
            ListaDatosVPOS.Add(new DatosVPOS() {
                MontoTransaccion = LibConvert.ToDec(Monto, 2),
                CedulaRif = CedulaRif,
            });
            base.ExecuteCancel();

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
        #endregion
    } //End of class DatosVPosViewModel

} //End of namespace Galac.Adm.Uil.Venta

