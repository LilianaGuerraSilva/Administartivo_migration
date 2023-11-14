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

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class FacturaCamposDefiniblesViewModel : LibInputViewModelMfc<CamposDefiniblesStt> {
        #region Constantes
        public const string NombreCampoDefinible1PropertyName = "NombreCampoDefinible1";
        public const string NombreCampoDefinible10PropertyName = "NombreCampoDefinible10";
        public const string NombreCampoDefinible11PropertyName = "NombreCampoDefinible11";
        public const string NombreCampoDefinible12PropertyName = "NombreCampoDefinible12";
        public const string NombreCampoDefinible2PropertyName = "NombreCampoDefinible2";
        public const string NombreCampoDefinible3PropertyName = "NombreCampoDefinible3";
        public const string NombreCampoDefinible4PropertyName = "NombreCampoDefinible4";
        public const string NombreCampoDefinible5PropertyName = "NombreCampoDefinible5";
        public const string NombreCampoDefinible6PropertyName = "NombreCampoDefinible6";
        public const string NombreCampoDefinible7PropertyName = "NombreCampoDefinible7";
        public const string NombreCampoDefinible8PropertyName = "NombreCampoDefinible8";
        public const string NombreCampoDefinible9PropertyName = "NombreCampoDefinible9";
        public const string UsaCamposDefiniblesPropertyName = "UsaCamposDefinibles";
        private const string IsEnabledUsaImprentaDigitalPropertyName = "IsEnabledUsaImprentaDigital";
        private const string ProveedorImprentaDigitalEsTheFactoryPropertyName = "ProveedorImprentaDigitalEsTheFactory";
        #endregion
        #region Variables
        private bool _IsEnabledUsaImprentaDigital;
        private eProveedorImprentaDigital _ProveedorServicioImprentaDigital;
        private bool _ProveedorImprentaDigitalEsTheFactory;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "2.5.- Campos Definibles"; }
        }

        [LibCustomValidation("NombreCampoDefinible1Validating")]
        public string  NombreCampoDefinible1 {
            get {
                return Model.NombreCampoDefinible1;
            }
            set {
                if (Model.NombreCampoDefinible1 != value) {
                    Model.NombreCampoDefinible1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinible1PropertyName);
                }
            }
        }

        public string  NombreCampoDefinible10 {
            get {
                return Model.NombreCampoDefinible10;
            }
            set {
                if (Model.NombreCampoDefinible10 != value) {
                    Model.NombreCampoDefinible10 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinible10PropertyName);
                }
            }
        }

        public string  NombreCampoDefinible11 {
            get {
                return Model.NombreCampoDefinible11;
            }
            set {
                if (Model.NombreCampoDefinible11 != value) {
                    Model.NombreCampoDefinible11 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinible11PropertyName);
                }
            }
        }

        public string  NombreCampoDefinible12 {
            get {
                return Model.NombreCampoDefinible12;
            }
            set {
                if (Model.NombreCampoDefinible12 != value) {
                    Model.NombreCampoDefinible12 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinible12PropertyName);
                }
            }
        }

        public string  NombreCampoDefinible2 {
            get {
                return Model.NombreCampoDefinible2;
            }
            set {
                if (Model.NombreCampoDefinible2 != value) {
                    Model.NombreCampoDefinible2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinible2PropertyName);
                }
            }
        }

        public string  NombreCampoDefinible3 {
            get {
                return Model.NombreCampoDefinible3;
            }
            set {
                if (Model.NombreCampoDefinible3 != value) {
                    Model.NombreCampoDefinible3 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinible3PropertyName);
                }
            }
        }

        public string  NombreCampoDefinible4 {
            get {
                return Model.NombreCampoDefinible4;
            }
            set {
                if (Model.NombreCampoDefinible4 != value) {
                    Model.NombreCampoDefinible4 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinible4PropertyName);
                }
            }
        }

        public string  NombreCampoDefinible5 {
            get {
                return Model.NombreCampoDefinible5;
            }
            set {
                if (Model.NombreCampoDefinible5 != value) {
                    Model.NombreCampoDefinible5 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinible5PropertyName);
                }
            }
        }

        public string  NombreCampoDefinible6 {
            get {
                return Model.NombreCampoDefinible6;
            }
            set {
                if (Model.NombreCampoDefinible6 != value) {
                    Model.NombreCampoDefinible6 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinible6PropertyName);
                }
            }
        }

        public string  NombreCampoDefinible7 {
            get {
                return Model.NombreCampoDefinible7;
            }
            set {
                if (Model.NombreCampoDefinible7 != value) {
                    Model.NombreCampoDefinible7 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinible7PropertyName);
                }
            }
        }

        public string  NombreCampoDefinible8 {
            get {
                return Model.NombreCampoDefinible8;
            }
            set {
                if (Model.NombreCampoDefinible8 != value) {
                    Model.NombreCampoDefinible8 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinible8PropertyName);
                }
            }
        }

        public string  NombreCampoDefinible9 {
            get {
                return Model.NombreCampoDefinible9;
            }
            set {
                if (Model.NombreCampoDefinible9 != value) {
                    Model.NombreCampoDefinible9 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinible9PropertyName);
                }
            }
        }

        public bool  UsaCamposDefinibles {
            get {
                return Model.UsaCamposDefiniblesAsBool;
            }
            set {
                if (Model.UsaCamposDefiniblesAsBool != value) {
                    Model.UsaCamposDefiniblesAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaCamposDefiniblesPropertyName);
                }
            }
        }
        
        public bool IsEnabledUsaCamposDefinibles {
            get {
                return UsaCamposDefinibles && IsEnabledCamposDefinibles;
            }
        }

        public bool ProveedorImprentaDigitalEsTheFactory {
            get { return (_ProveedorImprentaDigitalEsTheFactory || ProveedorServicioImprentaDigitalEsTheFactory()); }
            set {
                if (_ProveedorImprentaDigitalEsTheFactory != value) {
                    _ProveedorImprentaDigitalEsTheFactory = value;
                    RaisePropertyChanged(ProveedorImprentaDigitalEsTheFactoryPropertyName);
                }
            }
        }

        public bool IsEnabledCamposDefinibles {
            get {
                return IsEnabled && (IsEnabledUsaImprentaDigital ? !ProveedorImprentaDigitalEsTheFactory : true);
            }
        }

        public bool IsEnabledUsaImprentaDigital {
            get { return IsEnabled && (_IsEnabledUsaImprentaDigital || UsaImprentaDigital()); }
            set {
                if (_IsEnabledUsaImprentaDigital != value) {
                    _IsEnabledUsaImprentaDigital = value;
                    RaisePropertyChanged(IsEnabledUsaImprentaDigitalPropertyName);
                }
            }
        }
        #endregion //Propiedades
        #region Constructores
        public FacturaCamposDefiniblesViewModel()
            : this(new CamposDefiniblesStt(), eAccionSR.Insertar) {
        }

        public FacturaCamposDefiniblesViewModel(CamposDefiniblesStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = NombreCampoDefinible1PropertyName;
            _ProveedorServicioImprentaDigital = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ProveedorImprentaDigital"));
            LibMessages.Notification.Register<eProveedorImprentaDigital>(this, OnProveedorImprentaDigitalEsTheFactoryChanged);
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(CamposDefiniblesStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override CamposDefiniblesStt FindCurrentRecord(CamposDefiniblesStt valModel) {
            if (valModel == null) {
                return new CamposDefiniblesStt();
            }
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<CamposDefiniblesStt>, IList<CamposDefiniblesStt>> GetBusinessComponent() {
            return null;
        }

        private ValidationResult NombreCampoDefinible1Validating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(LibString.IsNullOrEmpty(NombreCampoDefinible1) && UsaCamposDefinibles) {
                    vResult = new ValidationResult(this.ModuleName + "-> Campo Definible 1 es requerido debido a que ha seleccionado la opción Usa campos definibles.");
                }
            }
            return vResult;
        }

        private void OnProveedorImprentaDigitalEsTheFactoryChanged(NotificationMessage<eProveedorImprentaDigital> valMessage) {
            if (LibString.S1IsEqualToS2(valMessage.Notification, "ProveedorServicioImprentaDigital")) {
                ProveedorImprentaDigitalEsTheFactory = valMessage.Content == eProveedorImprentaDigital.TheFactoryHKA || _ProveedorServicioImprentaDigital == eProveedorImprentaDigital.TheFactoryHKA;
            }
        }

        private bool UsaImprentaDigital() {
            return LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaImprentaDigital"));
        }

        private bool ProveedorServicioImprentaDigitalEsTheFactory() {
            return (eProveedorImprentaDigital)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ProveedorImprentaDigital"))
                == eProveedorImprentaDigital.TheFactoryHKA;
        }
        #endregion //Metodos Generados
    } //End of class FacturaCamposDefiniblesViewModel

} //End of namespace Galac.Saw.Uil.SttDef
