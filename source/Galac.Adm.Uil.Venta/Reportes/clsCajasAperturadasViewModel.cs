using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
//using LibGalac.Aos.UI.Cib;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Brl.Venta;
namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsCajasAperturadasViewModel : LibInputRptViewModelBase<Caja> {
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
            get { return "Cajas abiertas";}
        }

        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }

        public override bool IsSSRS {
            get {
                return false;
            }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCajasAperturadasViewModel() {
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            FechaDesde = LibDate.AddsNMonths(LibDate.Today(), - 1, false);
            FechaHasta = LibDate.Today();
        */
        #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        //protected override ILibBusinessComponentWithSearch<IList<Caja>, IList<Caja>> GetBusinessComponent() {
        //    return new clsCajaNav();
        //}

        protected override ILibBusinessSearch GetBusinessComponent()
        {
            return new clsCajaNav();
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


    } //End of class clsCajasAperturadasViewModel

} //End of namespace Galac.Dbo.Uil.Venta

