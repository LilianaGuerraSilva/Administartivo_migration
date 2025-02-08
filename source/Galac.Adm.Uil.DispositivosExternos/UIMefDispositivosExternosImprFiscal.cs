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
using Galac.Adm.Uil.DispositivosExternos.ViewModel;
using System.Xml.Linq;
using Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal;
using LibGalac.Aos.UI.Wpf;

namespace Galac.Adm.Uil.DispositivosExternos {
    [LibMefUilComponentMetadata(typeof(UIMefDispositivosExternosImprFiscal),"Adm")]
    public class UIMefDispositivosExternosImprFiscal:ILibMefUilComponent {
        #region Variables
        private clsImpresoraFiscalMenu _ImpresoraMenu;
        private ContentControl _View;
        string _File = "";
        #endregion //Variables
        #region Propiedades

        public string Name {
            get { return "Impresora Fiscal"; }
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
                    _View = new GSSearchView() {
                        DataContext = _ImpresoraMenu
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

        public UIMefDispositivosExternosImprFiscal() {
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
                _ImpresoraMenu = new clsImpresoraFiscalMenu();
            }
        }

        public void Reload() {
            string _ImprFile = LibFile.ReadFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ImprFiscal.XML");
            string _FacFile = LibFile.ReadFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\FACT.xml");
            InitializeIfNecessary();
            try {
                //_ImpresoraMenu.ImprimirNotaCredito(_ImprFile,_FacFile);
                _ImpresoraMenu.ImprimirFacturaFiscal(_ImprFile,_FacFile);
            } catch(Exception vEx) {
                LibExceptionDisplay.Show(vEx);
            }
        }
        #endregion //Metodos Generados


    } //End of class UIMefDispositivosExternosBalanza

} //End of namespace Galac.Adm.Uil.DispositivosExternos

