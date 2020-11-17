using Galac.Saw.Uil.Inventario.ViewModel;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Contracts;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.WpfControls;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Galac.Saw.Uil.Inventario {
    [LibMefUilComponentMetadata(typeof(UIMefInventarioVerificadorDePrecios), "Inventario")]
    public class UIMefInventarioVerificadorDePrecios : ILibMefUilComponent {

        #region Variables
        private VerificadorDePreciosMngViewModel _ViewModel;
        private ContentControl _View;
        #endregion

        #region Propiedades

        public string Name {
            get { return "Verificador de Precios"; }
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
        #endregion

        #region Constructores
        public UIMefInventarioVerificadorDePrecios() {
        }
        #endregion

        #region Metodos Generados
        public void InitializeIfNecessary() {
            if (!IsInitialized) {
                IsInitialized = true;
                _ViewModel = new VerificadorDePreciosMngViewModel();
                _ViewModel.RequestLoginAtClosing = true;
            }
        }

        public void Reload() {
            InitializeIfNecessary();
            if (_ViewModel != null) {
            }
        }
        #endregion
    }
}
