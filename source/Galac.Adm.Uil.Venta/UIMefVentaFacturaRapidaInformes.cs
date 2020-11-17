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

namespace Galac.Adm.Uil.Venta {
    [LibMefUilComponentMetadata(typeof(UIMefVentaFacturaRapidaInformes), "Adm")]
    public class UIMefVentaFacturaRapidaInformes: ILibMefUilComponent {
        #region Variables        
        private ILibMenu _VentaInformesMenu;
        private ContentControl _View;
        #endregion //Variables
        #region Propiedades

        public string Name {
            get { return "Venta Informes"; }
        }

        public Uri Image {
            get { return null; }
        }

        public ObservableCollection<LibRibbonTabData> RibbonTabData {
            get {
                return null;
            }
        }

        public ContentControl View {
            get {
                if (_View == null) {
                    _View = new ContentControl() {
                        DataContext = _VentaInformesMenu
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

        public UIMefVentaFacturaRapidaInformes() {
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
                    _VentaInformesMenu = new clsVentaInformesMenu();
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
                if (_VentaInformesMenu != null) {
                    _VentaInformesMenu.Ejecuta(eAccionSR.InformesPantalla,(int)Saw.Lib.eSystemModules.Cobranza);
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

