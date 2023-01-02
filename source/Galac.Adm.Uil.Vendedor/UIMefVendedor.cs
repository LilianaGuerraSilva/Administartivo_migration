using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Contracts;
using LibGalac.Aos.UI.WpfControls;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using Galac.Adm.Uil.Vendedor.ViewModel;

namespace Galac.Adm.Uil.Vendedor {
    [LibMefUilComponentMetadata(typeof(UIMefVendedor), "Adm")]
    public class UIMefVendedor : ILibMefUilComponent {
        #region Variables
        private VendedorMngViewModel _ViewModel;
        private ContentControl _View;
        #endregion //Variables
        #region Propiedades

        public string Name {
            get { return "Vendedor"; }
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

        public UIMefVendedor() { 
        }
        #endregion //Constructores
        #region Metodos Generados

        public void InitializeIfNecessary() {
            try {
                if (!IsInitialized) {
                    IsInitialized = true;
                    _ViewModel = new VendedorMngViewModel();
                }
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Name);
            }
        }

        public void Reload() {
            try {
                InitializeIfNecessary();
                if (_ViewModel != null) {
                    _ViewModel.ExecuteSearchAndInitLookAndFeel();
                }
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Name);
            }
        }
        #endregion //Metodos Generados


    } //End of class UIMefVendedorVendedor

} //End of namespace Galac.Adm.Uil.Vendedor

