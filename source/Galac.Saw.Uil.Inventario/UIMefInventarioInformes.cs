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
    [LibMefUilComponentMetadata(typeof(UIMefInventarioInformes), "Inventario")]
    public class UIMefInventarioInformes: ILibMefUilComponent {
        #region Variables
        private ILibMenu _ArticuloInventarioInformesMenu;
        private ContentControl _View;
        #endregion //Variables
        #region Propiedades

        public string Name {
            get { return "Informes de Inventario"; }
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
                if(_View == null) {
                    _View = new ContentControl() {
                        DataContext = _ArticuloInventarioInformesMenu
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

        public UIMefInventarioInformes() {
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
                _ArticuloInventarioInformesMenu = new clsArticuloInventarioMenu();
            }
        }

        public void Reload() {
            InitializeIfNecessary();
            if (_ArticuloInventarioInformesMenu != null) {
                _ArticuloInventarioInformesMenu.Ejecuta(eAccionSR.InformesPantalla,0);
            }
        }
        #endregion //Metodos Generados


    } //End of class UIMefInventarioAlmacen

} //End of namespace Galac.Saw.Uil.Inventario

