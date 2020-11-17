using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Cib;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Uil.GestionCompras.ViewModel;

namespace Galac.Adm.Uil.GestionCompras.Reportes {
    public class clsImprimirComprasEntreFechasViewModel : LibInputRptViewModelBase<Compra> {
        #region Variables
        eMonedaParaImpresion _MonedaParaImpresion = eMonedaParaImpresion.EnMonedaOriginal;
        const string MonedaParaImpresionPropertyName = "MonedaParaImpresion";
        const string IsVisiblePropertyName = "IsVisible";
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
        public const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        private eCantidadAImprimir _CantidadAImprimir;
        */
        #endregion //Codigo Ejemplo
        #endregion //Variables
        #region Propiedades

        public override string DisplayName {
            get { return "Compras entre Fechas";}
        }

        public int ConsecutivoCompania {
            get;
            set;
        }
        [LibCustomValidation("FechaInicialValidating")]
        [LibGridColum("Fecha Inicial", eGridColumType.DatePicker)]
        public DateTime FechaInicial {
            get;
            set;
        }
        [LibCustomValidation("FechaFinalValidating")]
        [LibGridColum("Fecha Final", eGridColumType.DatePicker)]
        public DateTime FechaFinal {
            get;
            set;
        }

        public bool ImprimirRenglones {
            get;
            set;
        }

        public bool ImprimirTotales {
            get;
            set;
        }

        public bool MostrarComprasAnuladas {
            get;
            set;
        }

        public eMonedaParaImpresion[] ArrayMonedaParaImpresion {
            get {
                return LibEnumHelper<eMonedaParaImpresion>.GetValuesInArray();
            }
        }
        public eMonedaParaImpresion MonedaParaImpresion {
            get {
                return _MonedaParaImpresion;
            }
            set {
                if (_MonedaParaImpresion != value) {
                    _MonedaParaImpresion = value;
                    RaisePropertyChanged(MonedaParaImpresionPropertyName);
                    RaisePropertyChanged(IsVisiblePropertyName);
                }
            }
        }

        public bool Original {
            get;
            set;
        }

        public bool DelDia {
            get;
            set;
        }

        public bool IsVisible {
            get {
                return _MonedaParaImpresion == eMonedaParaImpresion.EnSol;
            }
        }
        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }
        #endregion //Propiedades
        #region Constructores

        public clsImprimirComprasEntreFechasViewModel() {
            FechaInicial = LibDate.Today();
            FechaFinal = LibDate.Today();
            Original = true;
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
            return new clsCompraNav();
        }

        private ValidationResult FechaInicialValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaFinal, false, eAccionSR.Imprimir)) {
               vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha Inicial"));
            }
            
            return vResult;
        }

        private ValidationResult FechaFinalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (!LibDate.F1IsGreaterOrEqualThanF2(FechaFinal, FechaInicial)) {
                vResult = new ValidationResult("La fecha de traslado debe ser mayor o igual a la fecha: " + FechaInicial.ToShortDateString());
            }
            return vResult;
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
        public override bool IsSSRS {
            get {
                return false;
            }
        }

    } //End of class clsImprimirCompraViewModel

} //End of namespace Galac.Dbo.Uil.ComponenteNoEspecificado

