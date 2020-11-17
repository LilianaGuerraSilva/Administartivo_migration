using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Cib;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.GestionProduccion;
using Galac.Adm.Brl.GestionProduccion;
using Galac.Adm.Uil.GestionProduccion.ViewModel;
using LibGalac.Aos.UI.Mvvm.Validation;
using System.ComponentModel.DataAnnotations;

namespace Galac.Adm.Uil. GestionProduccion.Reportes {
    public class clsGestionMaterialesServiciosParaOrdenesViewModel : LibInputRptViewModelBase<OrdenDeProduccion> {
        #region Constantes
        public const string FechaDesdePropertyName = "FechaDesde";
        public const string FechaHastaPropertyName = "FechaHasta";
        #endregion
        #region Variables
        private DateTime _FechaDesde;
        private DateTime _FechaHasta;
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
        public const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        private eCantidadAImprimir _CantidadAImprimir;
        */
        #endregion //Codigo Ejemplo
        #endregion //Variables
        #region Propiedades
        public DateTime FechaDesde {
            get {
                return _FechaDesde;
            }
            set {
                if (_FechaDesde != value) {
                    _FechaDesde = value;
                    RaisePropertyChanged(FechaDesdePropertyName);
                    if (LibDate.F1IsGreaterThanF2(FechaDesde, FechaHasta)) {
                        FechaHasta = FechaDesde;
                        RaisePropertyChanged(FechaHastaPropertyName);
                    }
                }
            }
        }

        public DateTime FechaHasta {
            get {
                return _FechaHasta;
            }
            set {
                if (_FechaHasta != value) {
                    _FechaHasta = value;
                    RaisePropertyChanged(FechaHastaPropertyName);
                    if (LibDate.F1IsLessThanF2(FechaHasta, FechaDesde)) {
                        FechaDesde = FechaHasta;
                        RaisePropertyChanged(FechaHastaPropertyName);
                    }
                }
            }
        }

        public override string DisplayName {
            get { return "Gestión de Materiales/Servicios para iniciar Ordenes"; }
        }

        public override bool IsSSRS {
            get { return false; }
        }
        #endregion
        #region Constructores
        public clsGestionMaterialesServiciosParaOrdenesViewModel() {
            _FechaDesde = DateTime.Today;
            _FechaHasta = DateTime.Today;
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            FechaDesde = LibDate.AddsNMonths(LibDate.Today(), - 1, false);
            FechaHasta = LibDate.Today();
        */
        #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados
        protected override void InitializeCommands() {
            base.InitializeCommands();
        }

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsOrdenDeProduccionNav();
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
    } //End of class clsGestionMaterialesServiciosParaOrdenesViewModel
} //End of namespace Galac.Adm.Uil. GestionProduccion

