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
    class ImprentaDigitalDatosDeConexionViewModel : LibGenericViewModel {
        #region Constantes
        public const string ProveedorPropertyName = "Proveedor";
        public const string UrlPropertyName = "Url";
        public const string UsuarioPropertyName = "Usuario";
        public const string ClavePropertyName = "Clave";
        #endregion
        #region Propiedades
        eProveedorImprentaDigital _ProveedorImprentaDigital;

        public bool IsDirty { get; private set; }
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
        public RelayCommand GuardarCommand {
            get;
            private set;
        }

        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados
        protected override void InitializeCommands() {
            base.InitializeCommands();
            InicializaValores();
            GuardarCommand = new RelayCommand(ExecuteGuardarCommand, CanExecuteGuardarCommand);
        }

        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            LibRibbonControlData vRibbonControlSalir = RibbonData.TabDataCollection[0].GroupDataCollection[0].ControlDataCollection[0];
            RibbonData.RemoveRibbonGroup("Acciones");
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateAccionesRibbonGroup());
                RibbonData.TabDataCollection[0].GroupDataCollection[0].AddRibbonControlData(vRibbonControlSalir);
            }
        }

        private void ExecuteGuardarCommand() {
            ExecuteActualizarConexion();
        }

        private void InicializaValores() {
            _ProveedorImprentaDigital = (eProveedorImprentaDigital)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "ProveedorImprentaDigital");
            _Url = string.Empty;
            _Usuario = string.Empty;
            _Clave = string.Empty;
        }
        
        #endregion //Metodos Generados

        protected void ExecuteActualizarConexion() {
            if (LibString.IsNullOrEmpty(Usuario) || LibString.IsNullOrEmpty(Clave)) {
                LibMessages.MessageBox.ValidationError(this, "Los campos Usuario y Clave son obligatorios.", ModuleName);
            } else {                
                ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).GuardarDatosImprentaDigitalAppSettings(_ProveedorImprentaDigital, Usuario, Clave);
                RaiseRequestCloseEvent();
            }
        }

        private bool CanExecuteGuardarCommand() {
            return true;
        }

        private LibRibbonGroupData CreateAccionesRibbonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Acciones");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Guardar",
                Command = GuardarCommand,
                LargeImage = new Uri("/LibGalac.Aos.UI.WpfRD;component/Images/saveAndClose.png", UriKind.Relative),
                ToolTipDescription = "Guardar",
                ToolTipTitle = "Guardar",
                KeyTip = "F6"
            });
            return vResult;
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