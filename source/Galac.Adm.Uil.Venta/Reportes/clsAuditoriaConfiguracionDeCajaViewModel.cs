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
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Brl.Tablas;
namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsAuditoriaConfiguracionDeCajaViewModel : LibInputRptViewModelBase<AuditoriaConfiguracion> {
        #region Variables       
        #endregion //Variables
        #region Propiedades
        DateTime _FechaDesde;
        DateTime _FechaHasta;

        public DateTime FechaDesde {
            get { return _FechaDesde; }
            set {
                if (_FechaDesde != value) {
                    _FechaDesde = value;
                    RaisePropertyChanged(() => FechaDesde);
                }
            }
        }

        public DateTime FechaHasta {
            get { return _FechaHasta; }
            set {
                if (_FechaHasta != value) {
                    _FechaHasta = value;
                    RaisePropertyChanged(()=> FechaHasta);
                }
            }

        }
        
        public override string DisplayName {
            get { return "Auditoría de Caja/Impresora Fiscal"; }
        }

        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }

        public override bool IsSSRS => throw new NotImplementedException();
        #endregion //Propiedades
        #region Constructores

        public clsAuditoriaConfiguracionDeCajaViewModel() {
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            FechaDesde = LibDate.AddsNMonths(LibDate.Today(), - 1, false);
            FechaHasta = LibDate.Today();
        */
        #endregion //Codigo Ejemplo
        }
        protected override ILibBusinessSearch GetBusinessComponent() {
            return (ILibBusinessSearch)new clsAuditoriaConfiguracionNav();
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


    } //End of class clsAuditoriaConfiguracionDeCajaViewModel

} //End of namespace Galac.Adm.Uil.Venta

