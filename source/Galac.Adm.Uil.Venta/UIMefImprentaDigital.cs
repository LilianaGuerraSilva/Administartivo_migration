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
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.UI.Mvvm.Messaging;
using Galac.Adm.Brl.ImprentaDigital;
using System.Threading.Tasks;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Uil.Venta {
    [LibMefUilComponentMetadata(typeof(UIMefImprentaDigital), "Adm")]
    public class UIMefImprentaDigital : ILibMefUilComponent {
        #region Variables
        private clsFacturaRapidaMenu _DocumentoDigitalMenu;
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

        public UIMefImprentaDigital() {
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
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Name);
            }
        }

        public void Reload() {
            try {
                string vNumeroControl = string.Empty;
                string vMensaje = string.Empty;
                _DocumentoDigitalMenu = new clsFacturaRapidaMenu();
                SincronizarDocumento(1, "NC-00030000", ref vNumeroControl, ref vMensaje);
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, Name);
            }
        }

        // bool SincronizarDocumento(int vfwTipoDocumento, string vfwNumeroFactura, ref string vfwNumeroControl, ref string vfwMensaje) {
        bool EnviarDocumento(int vfwTipoDocumento, string vfwNumeroFactura, ref string vfwNumeroControl, ref string vfwMensaje) {
            try {
                string vNumeroControl = "";
                bool vResult = false;
                eTipoDocumentoFactura vTipoDeDocumento = (eTipoDocumentoFactura)vfwTipoDocumento;
                eProveedorImprentaDigital vProveedorImprentaDigital = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ProveedorImprentaDigital"));
                var _insImprentaDigital = ImprentaDigitalCreator.Create(vProveedorImprentaDigital, vTipoDeDocumento, vfwNumeroFactura);
                Task vTask = Task.Factory.StartNew(() => {
                    vResult = _insImprentaDigital.EnviarDocumento();
                    vNumeroControl = _insImprentaDigital.NumeroControl;
                });
                vTask.Wait();
                vfwNumeroControl = vNumeroControl;
                vfwMensaje = _insImprentaDigital.Mensaje;
                return vResult;
            } catch (AggregateException vEx) {
                vfwMensaje = vEx.InnerException.Message;
                return false;
            } catch (GalacException gEx) {
                vfwMensaje = gEx.Message;
                return false;
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                vfwMensaje = vEx.Message;
                return false;
            }
        }

        bool AnularDocumento(int vfwTipoDocumento, string vfwNumeroFactura, ref string vfwMensaje) {
            try {
                bool vResult = false;
                eTipoDocumentoFactura vTipoDeDocumento = (eTipoDocumentoFactura)vfwTipoDocumento;
                eProveedorImprentaDigital vProveedorImprentaDigital = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ProveedorImprentaDigital"));
                var _insImprentaDigital = ImprentaDigitalCreator.Create(vProveedorImprentaDigital, vTipoDeDocumento, vfwNumeroFactura);
                Task vTask = Task.Factory.StartNew(() => {
                    vResult = _insImprentaDigital.AnularDocumento();
                });
                vTask.Wait();
                vfwMensaje = _insImprentaDigital.Mensaje;
                return vResult;
            } catch (AggregateException vEx) {
                vfwMensaje = vEx.InnerException.Message;
                return false;
            } catch (GalacException gEx) {
                vfwMensaje = gEx.Message;
                return false;
            } catch (Exception vEx) {
                vfwMensaje = vEx.Message;
                return false;
            }
        }

        bool SincronizarDocumento(int vfwTipoDocumento, string vfwNumeroFactura, ref string vfwNumeroControl, ref string vfwMensaje) {
            try {
                string vNumeroControl = "";
                bool vResult = false;
                eTipoDocumentoFactura vTipoDeDocumento = (eTipoDocumentoFactura)vfwTipoDocumento;
                eProveedorImprentaDigital vProveedorImprentaDigital = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ProveedorImprentaDigital"));
                var _insImprentaDigital = ImprentaDigitalCreator.Create(vProveedorImprentaDigital, vTipoDeDocumento, vfwNumeroFactura);
                Task vTask = Task.Factory.StartNew(() => {
                    vResult = _insImprentaDigital.SincronizarDocumento();
                    vNumeroControl = _insImprentaDigital.NumeroControl;
                });
                vTask.Wait();
                vfwNumeroControl = vNumeroControl;
                vfwMensaje = _insImprentaDigital.Mensaje;
                return vResult;
            } catch (AggregateException vEx) {
                vfwMensaje = vEx.InnerException.Message;
                return false;
            } catch (GalacException gEx) {
                vfwMensaje = gEx.Message;
                return false;
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                vfwMensaje = vEx.Message;
                return false;
            }
        }
        #endregion //Metodos Generados
    } //End of class UIMefImprentaDigitalDocumentoDigital
} //End of namespace Galac.Adm.Uil.ImprentaDigital

