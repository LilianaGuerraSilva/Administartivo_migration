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
using Galac.Adm.Ccl.GestionCompras;
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Uil.GestionCompras.ViewModel;

namespace Galac.Adm.Uil.GestionCompras.Reportes {
    public class clsCuentasPorPagarEntreFechasViewModel : LibInputRptViewModelBase<CxP> {

        #region Variables
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
        public const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        private eCantidadAImprimir _CantidadAImprimir;
        */
        #endregion //Codigo Ejemplo
        #endregion //Variables

        #region Propiedades

        #endregion Propiedades


        #region Constructores
        public clsCuentasPorPagarEntreFechasViewModel() {
            #region Codigo Ejemplo
            /* Codigo de Ejemplo
                FechaDesde = LibDate.AddsNMonths(LibDate.Today(), - 1, false);
                FechaHasta = LibDate.Today();
            */
            #endregion //Codigo Ejemplo
        }

        public override string DisplayName {
            get { return "Cuentas por Pagar entre Fechas"; }
        }

        public override bool IsSSRS {
            get { return false; }
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsCxPNav();
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


    } //End of class clsCuentasPorPagarEntreFechasViewModel

} //End of namespace Galac.Dbo.Uil.ComponenteNoEspecificado