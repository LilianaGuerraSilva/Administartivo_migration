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

        public RelayCommand EnviarCommand {
            get;
            private set;
        }

        public RelayCommand SalirCommand {
            get;
            private set;
        }

        public bool IsVisibleButton {
            get {
                return Accion == eAccionSR.Emitir;
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
        public EnviarDocumentoViewModel(eTipoDocumentoFactura initTipoDocumento, string intiNumeroFactura, eAccionSR initAction) {
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

        protected override void InitializeCommands() {
            base.InitializeCommands();
            EnviarCommand = new RelayCommand(ExecuteEnviarCommand);
            SalirCommand = new RelayCommand(ExecuteSalirCommand);
        }

        public async void EjecutarProcesos() {
            try {
                switch (Accion) {
                    case eAccionSR.Emitir:
                        DocumentoEnviado = await _insImprentaDigital.EnviarDocumento();
                        NumeroControl = _insImprentaDigital.NumeroControl;
                        break;
                    case eAccionSR.Anular:
                        DocumentoEnviado = await _insImprentaDigital.AnularDocumento();
                        //NumeroControl = _insImprentaDigital.NumeroControl;
                        break;
                }
            } catch (Exception vEx) {
                LibMessages.MessageBox.Error(this, vEx.Message, ModuleName);
            } finally {
                RaiseRequestCloseEvent();
            }
        }

        private async void ExecuteEnviarCommand() {
            DocumentoEnviado = await _insImprentaDigital.EnviarDocumento();
            NumeroControl = _insImprentaDigital.NumeroControl;
            BtnIsEnable = false;
            if (!DocumentoEnviado) {
                BtnIsEnable = LibMessages.MessageBox.YesNo(this, "El documento no pudo ser enviado, desea reintentar?", ModuleName);
                TextoBtnEnviar = "Re-intentar";
                RaiseMoveFocus(TextoBtnEnviarPropertyName);
            }
            RaisePropertyChanged(BtnIsEnablePropertyName);
        }

        private void ExecuteSalirCommand() {
            RaiseRequestCloseEvent();
        }
        #endregion //Metodos Generados
    } //End of class EnviarDocumentoViewModel
} //End of namespace Galac.Adm.Uil.ImprentaDigital

