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
using Galac.Saw.Uil.Inventario.ViewModel;

namespace Galac.Saw.Uil.Inventario {
    [LibMefUilComponentMetadata(typeof(UIMefInventarioAlmacen), "Inventario")]
    public class UIMefInventarioAlmacen : ILibMefUilComponent {
        #region Variables
        private AlmacenMngViewModel _ViewModel;
        private ContentControl _View;
        #endregion //Variables
        #region Propiedades

        public string Name {
            get { return "Almacenes"; }
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

        public UIMefInventarioAlmacen() {
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
                _ViewModel = new AlmacenMngViewModel();
            }
        }

        public void Reload() {
            InitializeIfNecessary();
            if (_ViewModel != null) {
                _ViewModel.ExecuteSearchAndInitLookAndFeel();
            }
        }
        #endregion //Metodos Generados


    } //End of class UIMefInventarioAlmacen

} //End of namespace Galac.Saw.Uil.Inventario

