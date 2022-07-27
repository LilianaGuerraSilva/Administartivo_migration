using System;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Brl.Venta;
using LibGalac.Aos.UI.Mvvm.Validation;

namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsContratoEntreFechasViewModel : LibInputRptViewModelBase<Contrato> {
        #region Constantes
        public const string FechaDeInicioPropertyName = "FechaDeInicio";
        public const string FechaFinalPropertyName = "FechaFinal";
        public const string StatusContratoPropertyName = "StatusContrato";
        public const string FiltrarPorStatusPropertyName = "FiltrarPorStatus";
        public const string FiltrarPorFechaFinalPropertyName = "FiltrarPorFechaFinal";
        #endregion
        #region Variables
        private DateTime _FechaDeInicio { get; set; }
        private DateTime _FechaFinal { get; set; }
        private eStatusContrato _StatusContrato { get; set; }
        private bool _FiltrarPorStatus { get; set; }
        private bool _FiltrarPorFechaFinal { get; set; }
        #endregion
        #region Propiedades
        public override string DisplayName {
            get { return "Contratos entre Fechas"; }
        }
        public override bool IsSSRS {
            get { return false; }
        }

        [LibCustomValidation("FechaDeInicioValidating")]
        public DateTime FechaDeInicio {
            get {
                return _FechaDeInicio;
            }
            set {
                if(_FechaDeInicio != value) {
                    _FechaDeInicio = value;
                    RaisePropertyChanged(FechaDeInicioPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaFinalValidating")]
        public DateTime FechaFinal {
            get {
                return _FechaFinal;
            }
            set {
                if(_FechaFinal != value) {
                    _FechaFinal = value;
                    RaisePropertyChanged(FechaFinalPropertyName);
                }
            }
        }

        public eStatusContrato StatusContrato {
            get {
                return _StatusContrato;
            }
            set {
                if(_StatusContrato != value) {
                    _StatusContrato = value;
                    RaisePropertyChanged(StatusContratoPropertyName);
                }
            }
        }

        public bool FiltrarPorStatus {
            get {
                return _FiltrarPorStatus;
            }
            set {
                if(_FiltrarPorStatus != value) {
                    _FiltrarPorStatus = value;
                    RaisePropertyChanged(FiltrarPorStatusPropertyName);
                }
            }
        }

        public bool FiltrarPorFechaFinal {
            get {
                return _FiltrarPorFechaFinal;
            }
            set {
                if(_FiltrarPorFechaFinal != value) {
                    _FiltrarPorFechaFinal = value;
                    RaisePropertyChanged(FiltrarPorFechaFinalPropertyName);
                }
            }
        }

        public eStatusContrato[] ArrayStatusContrato {
            get {
                return LibEnumHelper<eStatusContrato>.GetValuesInArray();
            }
        }

        #endregion //Propiedades
        #region Constructores
        public clsContratoEntreFechasViewModel() {
            FechaDeInicio = LibDate.Today();
            FechaFinal = LibDate.Today();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsContratoNav();
        }
        #endregion //Metodos Generados

        protected override void RaiseMoveFocus(string valFocusedPropertyName) {
            base.RaiseMoveFocus(valFocusedPropertyName);
        }

    } //End of class clsContratoEntreFechasViewModel

} //End of namespace Galac.Adm.Uil.Venta

