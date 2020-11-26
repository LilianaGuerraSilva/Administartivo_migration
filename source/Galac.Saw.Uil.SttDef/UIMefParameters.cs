using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Contracts;
using Galac.Saw.Uil.SttDef.ViewModel;
using System.Windows.Controls;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using System.Collections.ObjectModel;
using LibGalac.Aos.UI.WpfControls;
using LibGalac.Aos.Base;

namespace Galac.Saw.Uil.SttDef {
    [LibMefUilComponentMetadata(typeof(UIMefParameters), "Parametros Administrativos")]
    public class UIMefParameters : ILibMefUilComponent {

         #region Variables
        private ParametersMngViewModel _ViewModel;
        private ContentControl _View;
        #endregion //Variables
        #region Propiedades

        public string Name {
            get { return "Parámetros Administrativos"; }
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
                if(_View == null) {
                    _View = new ContentControl() {
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

        public UIMefParameters() {
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
                _ViewModel = new ParametersMngViewModel();
            }
        }

        public void Reload() {
            InitializeIfNecessary();
            if (_ViewModel != null) {
               // _ViewModel.ExecuteSearchAndInitLookAndFeel();
            }
        }
        #endregion //Metodos Generados

    }
}
