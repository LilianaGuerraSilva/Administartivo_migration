using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Brl.ImprentaDigital;
using Galac.Saw.Ccl.SttDef;
using System.Threading.Tasks;

namespace Galac.Adm.Uil.ImprentaDigital.ViewModel {
    public class EnviarDocumentoViewModel: LibGenericViewModel {
        #region Constantes
        public const string NumeroFacturaPropertyName = "NumeroFactura";
        public const string TipoDocumentoPropertyName = "TipoDocumento";
        public const string TextoBtnEnviarPropertyName = "TextoBtnEnviar";
        public const string BtnIsEnablePropertyName = "BtnIsEnable";
        #endregion
        #region Variables
        string _NumeroFactura;
        string _NumeroControl;
        string _TipoDocumento;
        bool _DocumentoEnviado;
        string _TextoBtnEnviar;
        bool _BtnIsEnable;
        eTipoDocumentoFactura _TipoDeDocumento;
        clsImprentaDigitalBase _insImprentaDigital;
        eAccionSR _Accion;
        #endregion Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Imprenta Digital"; }
        }

        public string NumeroFactura {
            get {
                return _NumeroFactura;
            }
            set {
                if (_NumeroFactura != value) {
                    _NumeroFactura = value;
                    RaisePropertyChanged(NumeroFacturaPropertyName);
                }
            }
        }

        public string NumeroControl {
            get {
                return _NumeroControl;
            }
            set {
                if (_NumeroControl != value) {
                    _NumeroControl = value;
                }
            }
        }

        public bool DocumentoEnviado {
            get {
                return _DocumentoEnviado;
            }
            set {
                if (_DocumentoEnviado != value) {
                    _DocumentoEnviado = value;
                }
            }
        }

        public string TipoDocumento {
            get {
                return _TipoDocumento;
            }
            set {
                if (_TipoDocumento != value) {
                    _TipoDocumento = value;
                    RaisePropertyChanged(TipoDocumentoPropertyName);
                }
            }
        }

        public string TextoBtnEnviar {
            get {
                return _TextoBtnEnviar;
            }
            set {
                if (_TextoBtnEnviar != value) { }
                _TextoBtnEnviar = value;
            }
        }

        public eAccionSR Accion {
            get {
                return _Accion;
            }
            set {
                if (_Accion != value) {
                    _Accion = value;
                }
            }
        }    

        public bool IsVisibleButton {
            get {
                return Accion == eAccionSR.Exportar;
            }
        }

        public bool BtnIsEnable {
            get {
                return _BtnIsEnable;
            }
            set {
                if (_BtnIsEnable != value) {
                    _BtnIsEnable = value;
                    RaisePropertyChanged(BtnIsEnablePropertyName);
                }
            }
        }

        #endregion //Propiedades
        #region Constructores
        public EnviarDocumentoViewModel(eTipoDocumentoFactura initTipoDocumento, string intiNumeroFactura, bool initEsPorLote,eAccionSR initAction) {
            _TipoDeDocumento = initTipoDocumento;
            _NumeroFactura = intiNumeroFactura;
            Accion = initAction;
            eProveedorImprentaDigital vProveedorImprentaDigital = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ProveedorImprentaDigital"));
            _insImprentaDigital = ImprentaDigitalCreator.Create(vProveedorImprentaDigital, _TipoDeDocumento, _NumeroFactura);
        }

        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel() {
            base.InitializeLookAndFeel();
            RibbonData.HideAllControls();
            TipoDocumento = "Enviando " + LibEnumHelper.GetDescription(_TipoDeDocumento) + " No";
            TextoBtnEnviar = "Enviar";
            BtnIsEnable = true;
        }      

        public void EjecutarProcesos() {
            try {
                switch (Accion) {
                    case eAccionSR.Emitir:
                        EnviarDocumento();
                        break;
                }                
            } catch (Exception vEx) {
                LibMessages.MessageBox.Error(this, vEx.Message, ModuleName);
            } finally {
                RaiseRequestCloseEvent();
            }

        }

        private bool DoEnviarDocumento(ref string refNumeroControl) {
            try {
                bool vDocumentoEnviado = _insImprentaDigital.EnviarDocumento();
                refNumeroControl = _insImprentaDigital.NumeroControl;
                return vDocumentoEnviado;
            } catch (Exception) {
                throw;
            }
        }

        private void EnviarDocumento() {
            try {
                string vMensaje = string.Empty;
                var taskTestConnection = Task.Factory.StartNew(() => DoEnviarDocumento(ref vMensaje));
                Task.WaitAll(taskTestConnection);
                NumeroControl = vMensaje;
                DocumentoEnviado = taskTestConnection.Result;
                if (!DocumentoEnviado) {
                    DocumentoEnviado = LibMessages.MessageBox.YesNo(this, "El documento no pudo ser enviado, desea reintentar?", ModuleName);
                }
            } catch (Exception) {
                throw;
            }            
        }              
        #endregion //Metodos Generados
    } //End of class EnviarDocumentoViewModel
} //End of namespace Galac.Adm.Uil.ImprentaDigital

