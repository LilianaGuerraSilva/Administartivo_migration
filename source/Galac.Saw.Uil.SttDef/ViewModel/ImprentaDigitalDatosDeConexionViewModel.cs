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
using Galac.Saw.Brl.SttDef;
using Galac.Saw.Ccl.SttDef;
using System.Text;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    class ImprentaDigitalDatosDeConexionViewModel : LibInputViewModel<ImprentaDigitalDatosDeConexion> {
        #region Constantes
        public const string ProveedorPropertyName = "Proveedor";
        public const string UrlPropertyName = "Url";
        public const string UsuarioPropertyName = "Usuario";
        public const string ClavePropertyName = "Clave";
        #endregion
        #region Propiedades
        eProveedorImprentaDigital _ProveedorImprentaDigital;
        public override string ModuleName {
            get { return "Actualización de Datos de Conexión"; }
        }

        public string Proveedor {
            get {
                return LibEnumHelper.GetDescription(_ProveedorImprentaDigital);
            }
        }

        string _Url;
        [LibCustomValidation("UrlValidating")]
        public string Url {
            get { return _Url; }
            set {
                if (_Url != value) {
                    _Url = value;
                    IsDirty = true;
                    RaisePropertyChanged(UrlPropertyName);
                }
            }
        }

        string _Usuario;
        [LibCustomValidation("UsuarioValidating")]
        public string Usuario {
            get { return _Usuario; }
            set {
                if (_Usuario != value) {
                    _Usuario = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsuarioPropertyName);
                }
            }
        }

        string _Clave;
        [LibCustomValidation("ClaveValidating")]
        public string Clave {
            get { return _Clave; }
            set {
                if (_Clave != value) {
                    _Clave = value;
                    IsDirty = true;
                    RaisePropertyChanged(ClavePropertyName);
                }
            }
        }

        #endregion //Propiedades
        #region Constructores
        public ImprentaDigitalDatosDeConexionViewModel()
            : this(new ImprentaDigitalDatosDeConexion(), eAccionSR.Activar) {
        }
        public ImprentaDigitalDatosDeConexionViewModel(ImprentaDigitalDatosDeConexion initModel, eAccionSR initAction)
            : base(initModel, initAction) {
            DefaultFocusedPropertyName = ProveedorPropertyName;
        }
        #endregion //Constructores
        #region Metodos Generados
        protected override void InitializeLookAndFeel(ImprentaDigitalDatosDeConexion valModel) {
            base.InitializeLookAndFeel(valModel);
            InicializaValores();
        }

        private void InicializaValores() {
            _ProveedorImprentaDigital = (eProveedorImprentaDigital)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "ProveedorImprentaDigital");
            _Url = string.Empty;
            _Usuario = string.Empty;
            _Clave = string.Empty;
        }

        protected override ImprentaDigitalDatosDeConexion FindCurrentRecord(ImprentaDigitalDatosDeConexion valModel) {
            return null;
        }

        protected override ILibBusinessComponentWithSearch<IList<ImprentaDigitalDatosDeConexion>, IList<ImprentaDigitalDatosDeConexion>> GetBusinessComponent() {
            return null;
        }
        #endregion //Metodos Generados

        protected override void ExecuteAction() {
            if (LibString.IsNullOrEmpty(Usuario) || LibString.IsNullOrEmpty(Clave)) {
                LibMessages.MessageBox.ValidationError(this, "Los campos Usuario y Clave son obligatorios.", ModuleName);
            } else {
                base.ExecuteAction();
                ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).GuardarDatosImprentaDigitalAppSettings(_ProveedorImprentaDigital, Usuario, Clave);
            }
        }

        #region Validanting
        private ValidationResult UrlValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(Url)) {
                vResult = new ValidationResult("El valor Url es obligatorio. Este valor debe ser proporcionado por su proveedor de Imprenta Digital.");
            }
            return vResult;
        }

        private ValidationResult UsuarioValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(Usuario)) {
                vResult = new ValidationResult("El valor de Usuario es obligatorio. Este valor debe ser proporcionado por su proveedor de Imprenta Digital.");
            }
            return vResult;
        }

        private ValidationResult ClaveValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(Clave)) {
                vResult = new ValidationResult("El valor de la Clave es obligatorio. Este valor debe ser proporcionado por su proveedor de Imprenta Digital.");
            }
            return vResult;
        }
        #endregion Validanting

    } //End of class ImprentaDigitalDatosDeConexionViewModel

} //End of namespace Galac.Saw.Uil.SttDef.ViewModel 