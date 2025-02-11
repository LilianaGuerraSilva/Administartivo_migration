using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Uil.Venta.ViewModel;
using LibGalac.Aos.UI.Mvvm.Validation;

namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsFacturacionEntreFechasVerificacionViewModel: LibInputRptViewModelBase<Escalada> {
        #region Constantes
        public const string FechaDeInicioPropertyName = "FechaDeInicio";
        public const string FechaFinalPropertyName = "FechaFinal";
        #endregion
        #region Variables
        private DateTime _FechaDeInicio { get; set; }
        private DateTime _FechaFinal { get; set; }
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
        public const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        private eCantidadAImprimir _CantidadAImprimir;
        */
        #endregion //Codigo Ejemplo
        #endregion //Variables
        #region Propiedades

        public override string DisplayName {
            get { return "Auditoría Facturas entre Fechas";}
        }

        [LibCustomValidation("FechaDeInicioValidating")]
        public DateTime FechaDeInicio {
            get {
                return _FechaDeInicio;
            }
            set {
                if (_FechaDeInicio != value) {
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
                if (_FechaFinal != value) {
                    _FechaFinal = value;
                    RaisePropertyChanged(FechaFinalPropertyName);
                }
            }
        }

        public override bool IsSSRS {
            get { return false; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsFacturacionEntreFechasVerificacionViewModel() {
            FechaDeInicio = LibDate.Today();
            FechaFinal = LibDate.Today();
            #region Codigo Ejemplo
            /* Codigo de Ejemplo
                FechaDesde = LibDate.AddsNMonths(LibDate.Today(), - 1, false);
                FechaHasta = LibDate.Today();
            */
            #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsEscaladaNav();
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public eCantidadAImprimir CantidadAImprimir {
            get {
                return _CantidadAImprimir;
            }
            set {
                if (_CantidadAImprimir != value) {
                    _CantidadAImprimir = value;
                    RaisePropertyChanged(CantidadAImprimirPropertyName);
                }
            }
        }

        public eCantidadAImprimir[] ECantidadAImprimir {
            get {
                return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray();
            }
        }
        */
        #endregion //Codigo Ejemplo


    } //End of class clsFacturacionEntreFechasVerificacionViewModel

} //End of namespace Galac.Adm.Uil.Venta

