using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Contracts;
using LibGalac.Aos.UI.WpfControls;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using Galac.Adm.Uil.Venta.ViewModel;
using Galac.Adm.Uil.Venta.Views;

namespace Galac.Adm.Uil.Venta {
    [LibMefUilComponentMetadata(typeof(UIMefVentaFacturaRapidaCobroMultimoneda), "Adm")]
    public class UIMefVentaFacturaRapidaCobroMultimoneda : ILibMefUilComponent {
        #region Variables
        private CobroRapidoMultimonedaViewModel _ViewModel;
        private ContentControl _View;
        #endregion //Variables
        #region Propiedades

        public string Name {
            get { return "Cobro Rápido en Multimoneda"; }
        }

        public Uri Image {
            get { return null; }
        }

        public ObservableCollection<LibRibbonTabData> RibbonTabData {
            get {
                if (_ViewModel != null) {
                    return _ViewModel.RibbonData.TabDataCollection;
                } else {
                    return null;
                }
            }
        }

        public ContentControl View {
            get {
                if (_View == null) {
                    _View = new GSCobroRapidoMultimonedaView() {
                        DataContext = _ViewModel
                    };
                }
                return _View;
            }
        }

        public bool IsInitialized {
            get;
            private set;
        }

        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }
        #endregion //Propiedades
        #region Constructores

        public UIMefVentaFacturaRapidaCobroMultimoneda() {
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            AppMemoryInfo = LibGlobalValues.Instance.GetAppMemInfo();
        */
        #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        public void InitializeIfNecessary() {
            try {
                //if (!IsInitialized) {
                //    IsInitialized = true;
                //    _ViewModel = new CobroRapidoMultimonedaViewModel();
                //}
                new clsCobroRapidoMultimonedaMenu().Ejecuta(eAccionSR.Generar, 1);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Name);
            }
        }

        public void Reload() {
            try {
                InitializeIfNecessary();
                if (_ViewModel != null) {
                    //_ViewModel.ExecuteSearchAndInitLookAndFeel();
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Name);
            }
        }
        #endregion //Metodos Generados


    } //End of class UIMefVentaFacturaRapida

} //End of namespace Galac.Adm.Uil.Venta

