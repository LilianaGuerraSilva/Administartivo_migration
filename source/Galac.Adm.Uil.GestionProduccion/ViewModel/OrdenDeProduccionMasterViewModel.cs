using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using Galac.Adm.Ccl.GestionProduccion;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;


namespace Galac.Adm.Uil.GestionProduccion.ViewModel {
    public class OrdenDeProduccionMasterViewModel : LibGenericViewModel {
        #region Constantes
        public const string OrdenDeProduccionPropertyName = "OrdenDeProduccion";
        private OrdenDeProduccionViewModel _OrdenDeProduccion;
        #endregion
        #region Propiedades

        public override string ModuleName {
            get {
                return "Detalle Orden De Producción";
            }
        }

        public OrdenDeProduccionViewModel OrdenDeProduccion {
            get {
                return _OrdenDeProduccion;
            }
            set {
                if (_OrdenDeProduccion != value) {
                    _OrdenDeProduccion = value;
                    RaisePropertyChanged(OrdenDeProduccionPropertyName);
                }
            }
        }

        #endregion //Propiedades
        #region Constructores
        public OrdenDeProduccionMasterViewModel(OrdenDeProduccionViewModel initOrdenDeProduccion)
            : base() {
            OrdenDeProduccion = initOrdenDeProduccion;           
        }
        #endregion //Constructores
        #region Metodos Generados
        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (OrdenDeProduccion.Action == eAccionSR.Consultar || OrdenDeProduccion.Action == eAccionSR.Eliminar || OrdenDeProduccion.Action == eAccionSR.Contabilizar) {
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[0].Command = null;
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[0].IsVisible = false;
            } else {
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[0].Command = null;
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[0].IsVisible = false;
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[1].Label = "Grabar";
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[1].ToolTipDescription = "Ejecuta la acción seleccionada.";
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[1].ToolTipDescription = "Ejecutar Acción";
                RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[1].LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/saveAndClose.png", UriKind.Relative);
                OrdenDeProduccion.DetailOrdenDeProduccionDetalleArticulo.SelectedItem = OrdenDeProduccion.DetailOrdenDeProduccionDetalleArticulo.Items.FirstOrDefault();
                OrdenDeProduccion.DetailOrdenDeProduccionDetalleMateriales.SelectedItem = OrdenDeProduccion.DetailOrdenDeProduccionDetalleMateriales.Items.FirstOrDefault();
                RaisePropertyChanged("DetailOrdenDeProduccionDetalleMateriales");
            }
        }
        #endregion //Metodos Generados
    } //End of class OrdenDeProduccionMasterViewModel
} //End of namespace Galac.Adm.Uil.GestionProduccion.ViewModel

