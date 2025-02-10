using Galac.Adm.Uil.Venta.ViewModel;
using Galac.Adm.Uil.Venta.Views;
using LibGalac.Aos.UI.Contracts;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Wpf.Views;
using LibGalac.Aos.UI.WpfControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Galac.Adm.Uil.Venta {
    [LibMefUilComponentMetadata(typeof(UIMefAudiEnte), "Venta")]
    public class UIMefAudiEnte : ILibMefUilComponent {
        #region Variables
        private AuditoriaEnteMngViewModel _ViewModel;
        private ContentControl _View;
        #endregion //Variables
        #region Propiedades

        public string Name { get { return "Auditoría SENIAT"; } }
        public Uri Image { get { return null; } }

        public ObservableCollection<LibRibbonTabData> RibbonTabData {
            get {
                if (_ViewModel == null) {
                    return null;
                } else {
                    return _ViewModel.RibbonData.TabDataCollection;
                }
            }
        }

        public ContentControl View {
            get {
                if (_View == null) {
                    _View = new GSAuditoriaEnteView() { DataContext = _ViewModel };
                }
                return _View;
            }
        }

        public bool IsInitialized {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public UIMefAudiEnte() {
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
                    _ViewModel = new AuditoriaEnteMngViewModel();                    
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
                    
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Name);
            }
        }
#endregion //Metodos Generados
    }
}
