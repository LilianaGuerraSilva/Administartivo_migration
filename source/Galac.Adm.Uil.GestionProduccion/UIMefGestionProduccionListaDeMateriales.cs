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
using Galac.Adm.Uil.GestionProduccion.ViewModel;

namespace Galac.Adm.Uil.GestionProduccion {
    [LibMefUilComponentMetadata(typeof(UIMefGestionProduccionListaDeMateriales), "Adm")]
    public class UIMefGestionProduccionListaDeMateriales : ILibMefUilComponent {
        #region Variables
        private ListaDeMaterialesMngViewModel _ViewModel;
        private ContentControl _View;
        #endregion //Variables
        #region Propiedades

        public string Name {
            get {
                string vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreParaMostrarListaDeMateriales");
                if (LibString.IsNullOrEmpty(vResult)) {
                    vResult = "Lista de Materiales";
                }
                return vResult;
            }
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

        public UIMefGestionProduccionListaDeMateriales() {
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
                if (!IsInitialized) {
                    IsInitialized = true;
                    _ViewModel = new ListaDeMaterialesMngViewModel();
                }
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
                    _ViewModel.ExecuteSearchAndInitLookAndFeel();
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Name);
            }
        }
        #endregion //Metodos Generados


    } //End of class UIMefGestionProduccionListaDeMateriales

} //End of namespace Galac.Adm.Uil.GestionProduccion

