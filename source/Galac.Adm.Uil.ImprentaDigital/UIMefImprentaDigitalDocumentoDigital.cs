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
using Galac.Adm.Uil.ImprentaDigital.ViewModel;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.UI.Mvvm.Messaging;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Uil.ImprentaDigital {
    [LibMefUilComponentMetadata(typeof(UIMefImprentaDigitalDocumentoDigital), "Adm")]
    public class UIMefImprentaDigitalDocumentoDigital : ILibMefUilComponent {
        #region Variables
        private clsDocumentoDigitalMenu _DocumentoDigitalMenu;
        private ContentControl _View;
        #endregion //Variables
        #region Propiedades

        public string Name {
            get { return "Imprenta Digital"; }
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
                        DataContext = _DocumentoDigitalMenu
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

        public UIMefImprentaDigitalDocumentoDigital() {
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
                    _DocumentoDigitalMenu = new clsDocumentoDigitalMenu();
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
                string vRefNumControl = "";
                eTipoDocumentoFactura _TipoDeDocumento = 0;
                eAccionSR _Action = (eAccionSR)2;
                LibMessages.MessageBox.Information(this, $"Numero de Control {vRefNumControl}", Name);
                bool vReq = _DocumentoDigitalMenu.EjecutarAccion(_TipoDeDocumento, "0000007777", _Action, false, ref vRefNumControl);
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, Name);
            }
        }
        #endregion //Metodos Generados
    } //End of class UIMefImprentaDigitalDocumentoDigital
} //End of namespace Galac.Adm.Uil.ImprentaDigital

