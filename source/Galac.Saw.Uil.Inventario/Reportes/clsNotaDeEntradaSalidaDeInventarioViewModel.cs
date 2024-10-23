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
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Brl.Inventario;

namespace Galac.Saw.Uil.Inventario.Reportes {
    public class clsNotaDeEntradaSalidaDeInventarioViewModel : LibInputRptViewModelBase<NotaDeEntradaSalida> {
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
            get { return "Nota de Entrada/Salida de Inventario";}
        }

        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }

        public override bool IsSSRS => false;
        #endregion //Propiedades
        #region Constructores

        public clsNotaDeEntradaSalidaDeInventarioViewModel() {
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            FechaDesde = LibDate.AddsNMonths(LibDate.Today(), - 1, false);
            FechaHasta = LibDate.Today();
        */
        #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        //protected override ILibBusinessComponentWithSearch<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>> GetBusinessComponent() {
        //    return new clsNotaDeEntradaSalidaNav();
        //}

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsNotaDeEntradaSalidaNav();
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


    } //End of class clsNotaDeEntradaSalidaDeInventarioViewModel

} //End of namespace Galac.Saw.Uil.Inventario

