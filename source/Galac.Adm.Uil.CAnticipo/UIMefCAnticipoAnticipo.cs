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
using Galac.Saw.Lib;

namespace Galac.Adm.Uil.CAnticipo {
    [LibMefUilComponentMetadata(typeof(UIMefCAnticipoAnticipo), "Adm")]
    public class UIMefCAnticipoAnticipo : ILibMefUilComponent {
        #region Variables      
        private ContentControl _View;
        private ILibMenu _InformeAnticpoMenu;
        #endregion //Variables
        #region Propiedades

        public string Name {
            get { return "Anticipo"; }
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
                    _View = new GSSearchView() {
                        DataContext = _InformeAnticpoMenu
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

        public UIMefCAnticipoAnticipo() {
       
        }
        #endregion //Constructores
        #region Metodos Generados

        public void InitializeIfNecessary() {
            try {
                if (!IsInitialized) {
                    IsInitialized = true;
                    _InformeAnticpoMenu = new clsAnticipoInformesMenu();
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
                if (_InformeAnticpoMenu != null) {
                    _InformeAnticpoMenu.Ejecuta(eAccionSR.InformesPantalla, (int)eSystemModules.CxC);//Temporal solo para ejecucion en IDE
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Name);
            }
        }
        #endregion //Metodos Generados


    } //End of class UIMefCAnticipoAnticipo

} //End of namespace Galac.Adm.Uil.CAnticipo

