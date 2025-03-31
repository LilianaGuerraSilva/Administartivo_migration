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

    public class clsNotasDeEntregaNoFacturadasViewModel: LibInputRptViewModelBase<FacturaRapida> {
        #region Constantes
        private const string FechaDesdePropertyName = "FechaDesde";
        private const string FechaHastaPropertyName = "FechaHasta";
        private DateTime _FechaDesde;
        private DateTime _FechaHasta;
        #endregion
        #region Variables
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
        public const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        private eCantidadAImprimir _CantidadAImprimir;
        */
        #endregion //Codigo Ejemplo
        #endregion //Variables
        #region Propiedades

        public override string DisplayName {
            get { return "Notas de Entregas Emitidas y No Facturadas";}
        }

        public override bool IsSSRS {
            get { return false; }
        }

        [LibRequired(ErrorMessage = "El campo Fecha Desde es requerido.")]
        public DateTime FechaDesde {
            get {
                return _FechaDesde;
            }
            set {
                if (_FechaDesde != value) {
                    _FechaDesde = value;
                    RaisePropertyChanged(FechaDesdePropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Fecha Hasta es requerido.")]
        public DateTime FechaHasta {
            get {
                return _FechaHasta;
            }
            set {
                if (_FechaHasta != value) {
                    _FechaHasta = value;
                    RaisePropertyChanged(FechaHastaPropertyName);
                }
            }
        }

        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }
        #endregion //Propiedades
        #region Constructores

        public clsNotasDeEntregaNoFacturadasViewModel() {
            FechaDesde = LibDate.DateFromMonthAndYear(1, LibDate.Today().Year, true);
            FechaHasta = LibDate.Today();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsFacturaRapidaNav();
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


    } //End of class clsNotasDeEntregaNoFacturadasViewModel

} //End of namespace Galac.Adm.Uil.Venta

