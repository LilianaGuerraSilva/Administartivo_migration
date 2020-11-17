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
using Galac.Adm.Uil.CajaChica.ViewModel;
using LibGalac.Aos.UI.Mvvm.Messaging;

namespace Galac.Adm.Uil.CajaChica {
    [LibMefUilComponentMetadata(typeof(UIMefCajaChicaCierre), "Caja Chica")]
    public class UIMefCajaChicaCierre : ILibMefUilComponent {
        #region Variables        
        private RendicionViewModel _ViewModel;
        private ContentControl _View;
        bool IsShow;
        #endregion //Variables
        #region Propiedades

        public string Name {
            get { return "Cierre de Caja Chica"; }
        }

        public Uri Image {
            get { return null; }
        }

        public ObservableCollection<LibRibbonTabData> RibbonTabData {
            get {
                //if (_ViewModel != null) {
                //    return _ViewModel.RibbonData.TabDataCollection;
                //} else {
                    return null;
                //}
            }
        }

        public ContentControl View {
            get {
                if (_View == null) {
                    _View = new GSSearchView() {
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

        public UIMefCajaChicaCierre() {
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            AppMemoryInfo = LibGlobalValues.Instance.GetAppMemInfo();
        */
        #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        public void InitializeIfNecessary() {
            if(!IsInitialized) {
                IsInitialized = true;
                _ViewModel = new RendicionViewModel(eAccionSR.Cerrar);
            }
        }

        public void Reload() {
            InitializeIfNecessary();
            if (_ViewModel != null) {
                _ViewModel.InitializeViewModel(eAccionSR.Cerrar);
                if(!IsShow) {}
                LibMessages.EditViewModel.ShowEditor(_ViewModel,false);
                IsShow = true;
            }
        }
        #endregion //Metodos Generados


    } //End of class UIMefCajaChicaCierre

} //End of namespace Galac.Adm.Uil.CajaChica

