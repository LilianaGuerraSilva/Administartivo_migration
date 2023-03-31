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
        #endregion
        #region Variables
        string _NumeroFactura;
        string _NumeroControl;
        string _TipoDocumento;
        bool _DocumentoEnviado;
        eTipoDocumentoFactura _TipoDeDocumento;
        clsImprentaDigitalBase _insImprentaDigital;
        eAccionSR _Action;
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

        #endregion //Propiedades
        #region Constructores
        public EnviarDocumentoViewModel(eTipoDocumentoFactura initTipoDocumento, string intiNumeroFactura, eAccionSR initAction) {
            _TipoDeDocumento = initTipoDocumento;
            _NumeroFactura = intiNumeroFactura;
            _Action = initAction;
            eProveedorImprentaDigital vProveedorImprentaDigital = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ProveedorImprentaDigital"));
            _insImprentaDigital = ImprentaDigitalCreator.Create(vProveedorImprentaDigital, _TipoDeDocumento, _NumeroFactura);
        }
    
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel() {
            base.InitializeLookAndFeel();
            RibbonData.HideAllControls();
            TipoDocumento = "Enviando " + LibEnumHelper.GetDescription(_TipoDeDocumento) + " No";
        }

        public async void EjecutarProcesos() {
            try {
                switch (_Action) {
                    case eAccionSR.Emitir:
                        DocumentoEnviado = await _insImprentaDigital.EnviarDocumento();
                        NumeroControl = _insImprentaDigital.NumeroControl;
                        break;
                }                
            } catch (Exception vEx) {
                LibMessages.MessageBox.Error(this, vEx.Message, ModuleName);            
            } finally {
                RaiseRequestCloseEvent();
            }    
        }
        #endregion //Metodos Generados
    } //End of class EnviarDocumentoViewModel
} //End of namespace Galac.Adm.Uil.ImprentaDigital

