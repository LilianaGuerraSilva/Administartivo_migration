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
    public class ImprentaDigitalActivacionViewModel : LibGenericViewModel {
        #region Constantes
        public const string ProveedorPropertyName = "Proveedor";
        public const string UrlPropertyName = "Url";
        public const string UsuarioPropertyName = "Usuario";
        public const string ClavePropertyName = "Clave";
        public const string FacturaT1PropertyName = "FacturaT1";
        public const string NotaDeCreditoPropertyName = "NotaDeCredito";
        public const string NotaDeDebitoPropertyName = "NotaDeDebito";
        public const string FechaDeInicioDeUsoPropertyName = "FechaDeInicioDeUso";
        public const string ReajustarTalonariosDeFacturaPropertyName = "ReajustarTalonariosDeFactura";
        #endregion
        #region Propiedades
        bool _UsaDosTalonarios;
        bool _UsaFacturaPreNumeradaTalonario1;
        bool _UsaNCPreNumerada;
        bool _UsaNDPreNumerada;

        public override string ModuleName {
            get { return "Activación de Imprenta Digital"; }
        }

        public RelayCommand GuardarCommand {
            get;
            private set;
        }

        eProveedorImprentaDigital _Proveedor;

        public bool IsDirty { get; private set; }

        public eProveedorImprentaDigital Proveedor {
            get { return _Proveedor; }
            set {
                if (_Proveedor != value) {
                    _Proveedor= value;
                    IsDirty = true;
                    RaisePropertyChanged(ProveedorPropertyName);
                }
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

        string _FacturaT1;
        [LibCustomValidation("FacturaT1Validating")]
        public string FacturaT1 {
            get { return _FacturaT1; }
            set {
                if (_FacturaT1 != value) {
                    _FacturaT1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(FacturaT1PropertyName);
                }
            }
        }

        string _NotaDeCredito;
        [LibCustomValidation("Validating")]
        public string NotaDeCredito {
            get { return _NotaDeCredito; }
            set {
                if (_NotaDeCredito != value) {
                    _NotaDeCredito = value;
                    IsDirty = true;
                    RaisePropertyChanged(NotaDeCreditoPropertyName);
                }
            }
        }

        string _NotaDeDebito;
        [LibCustomValidation("Validating")]
        public string NotaDeDebito {
            get { return _NotaDeDebito; }
            set {
                if (_NotaDeDebito != value) {
                    _NotaDeDebito = value;
                    IsDirty = true;
                    RaisePropertyChanged(NotaDeDebitoPropertyName);
                }
            }
        }

        DateTime _FechaDeInicioDeUso;
        [LibCustomValidation("FechaDeInicioDeUsoValidating")]
        public DateTime FechaDeInicioDeUso {
            get { return _FechaDeInicioDeUso; }
            set {
                if (_FechaDeInicioDeUso != value) {
                    _FechaDeInicioDeUso = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaDeInicioDeUsoPropertyName);
                }
            }
        }

        bool _ReajustarTalonariosDeFactura;
        public bool ReajustarTalonariosDeFactura {
            get { return _ReajustarTalonariosDeFactura; }
            set {
                if (_ReajustarTalonariosDeFactura != value) {
                    _ReajustarTalonariosDeFactura = value;
                    IsDirty = true;
                    RaisePropertyChanged(ReajustarTalonariosDeFacturaPropertyName);
                }
            }
        }

        public eProveedorImprentaDigital[] ArrayProveedorImprentaDigital {
            get { return LibEnumHelper<eProveedorImprentaDigital>.GetValuesInArray(); }
        }

        public bool IsEnabledProximoNumeroFacturaTalonario1 {
            get { return IsEnabledProximoNumero(eTipoDocumentoFactura.Factura); }
        }

        public bool IsEnabledProximoNumeroNC {
            get { return IsEnabledProximoNumero(eTipoDocumentoFactura.NotaDeCredito); }
        }
        public bool IsEnabledProximoNumeroND {
            get { return IsEnabledProximoNumero(eTipoDocumentoFactura.NotaDeDebito); }
        }

        public bool IsEnabledFechaDeInicioDeUso {
            get { return PuedeEditarFechaDeInicioDeUso(); }
        }

        public bool IsVisibleReajustarTalonariosFactura {
            get { return _UsaFacturaPreNumeradaTalonario1; }
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
            ExecuteActivacion();
        }

        private ValidationResult FechaDeInicioDeUsoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDeInicioDeUso, false, eAccionSR.Activar)) {
                vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha De Inicio De Uso"));
            }
            return vResult;
        }
        #endregion //Metodos Generados

        private bool IsEnabledProximoNumero(eTipoDocumentoFactura valTipoDoc) {
            switch (valTipoDoc) {
                case eTipoDocumentoFactura.Factura: return _UsaFacturaPreNumeradaTalonario1;
                case eTipoDocumentoFactura.NotaDeCredito: return _UsaNCPreNumerada;
                case eTipoDocumentoFactura.NotaDeDebito: return _UsaNDPreNumerada;
                default: return false;
            }
        }

        protected void ExecuteActivacion() {
            StringBuilder vMessage = new StringBuilder();
            vMessage.AppendLine("Se ejecutará la activación del uso de Imprenta Digital.");
            vMessage.AppendLine();
            vMessage.AppendLine("Esta configuración no es reversible.");
            vMessage.AppendLine();
            vMessage.AppendLine("¿Desea continuar?");
            if (LibMessages.MessageBox.YesNo(this, vMessage.ToString(), ModuleName)) {
                if (SePuedeGrabar()) {
                    ConfigurarParametros();
                    RaiseRequestCloseEvent();
                }
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

        private bool SePuedeGrabar() {
            bool vResult;
            StringBuilder vMsg = new StringBuilder();
            vResult = (Proveedor != eProveedorImprentaDigital.NoAplica)
                        && !LibString.IsNullOrEmpty(Url)
                        && !LibString.IsNullOrEmpty(Usuario)
                        && !LibString.IsNullOrEmpty(Clave)
                        && !LibString.IsNullOrEmpty(FacturaT1)
                        && !LibString.IsNullOrEmpty(NotaDeCredito)
                        && !LibString.IsNullOrEmpty(NotaDeDebito);
            if (vResult) {
                vResult = ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).SonValidosLosSiguienteNumerosDeDocumentosParaImprentaDigital(FacturaT1, NotaDeCredito, NotaDeDebito, out vMsg);
            } else {
                vMsg.AppendLine("Todos los datos son obligatorios.");
            }
            if (vResult) {
                if (LibDate.F1IsLessThanF2(FechaDeInicioDeUso, LibDate.Today())) {
                    vResult = false;
                    vMsg.AppendLine("La Fecha de Inicio de Imprenta Digital debe ser a partir del día de hoy.");
                }
            }
            LibMessages.MessageBox.ValidationError(this, vMsg.ToString(), "Activación de Imprenta Digital");
            return vResult;
        }

        private void ConfigurarParametros() {
            ConfigurarImprentaDigital();
            ConfigurarFactura();
            ConfigurarNotaDebito();
            ConfigurarNotaCredito();
        }

        private void ConfigurarImprentaDigital() {            
            ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).ConfigurarImprentaDigital(Proveedor, FechaDeInicioDeUso);
            ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).GuardarDatosImprentaDigitalAppSettings(Proveedor, Usuario, Clave, Url);
        }

        private void MoverDocumentosFactura(eTipoDocumentoFactura valTipoDocFactura, eTalonario valTalonarioOrigen, eTalonario valTalonarioDestino) {
            ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).MoverDocumentosDeTalonario(eTipoDocumentoFactura.Factura, valTalonarioOrigen, valTalonarioDestino);
        }

        private void ConfigurarFactura() {
            eTipoDePrefijo vTipoDePrefijoTalonario1 = (eTipoDePrefijo)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "TipoDePrefijoTalonario1");
            if ((_UsaFacturaPreNumeradaTalonario1 && ReajustarTalonariosDeFactura) || (vTipoDePrefijoTalonario1 != eTipoDePrefijo.SinPrefijo)) { 
                MoverDocumentosFactura(eTipoDocumentoFactura.Factura, eTalonario.Talonario1, eTalonario.Talonario3);
            }
            ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).ConfiguracionImprentaDigitalPorTipoDeDocumentoFactura(eTipoDocumentoFactura.Factura, FacturaT1);
        }

        private void ConfigurarNotaDebito() {
            MoverDocumentosFactura(eTipoDocumentoFactura.NotaDeDebito, eTalonario.Talonario1, eTalonario.Talonario3);
            ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).ConfiguracionImprentaDigitalPorTipoDeDocumentoFactura(eTipoDocumentoFactura.NotaDeDebito, NotaDeDebito);
        }

        private void ConfigurarNotaCredito() {
            MoverDocumentosFactura(eTipoDocumentoFactura.NotaDeCredito, eTalonario.Talonario1, eTalonario.Talonario3);
            ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).ConfiguracionImprentaDigitalPorTipoDeDocumentoFactura(eTipoDocumentoFactura.NotaDeCredito, NotaDeCredito);
        }

        bool PuedeEditarFechaDeInicioDeUso() {
            return false;
        }

        private void InicializaValores() {
            _UsaDosTalonarios = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarDosTalonarios");
            _UsaFacturaPreNumeradaTalonario1 = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "FacturaPreNumeradaTalonario1");
            _UsaNCPreNumerada = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "NCPreNumerada");
            _UsaNDPreNumerada = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "NDPreNumerada");
            Proveedor = eProveedorImprentaDigital.NoAplica;
            Url = string.Empty;
            Usuario = string.Empty;
            Clave = string.Empty;
            FacturaT1 = SiguienteNumeroFacturaTalonario1();
            NotaDeCredito = SiguienteNumeroNotaDeCredito();
            NotaDeDebito = SiguienteNumeroNotaDeDebito();
            FechaDeInicioDeUso = LibDate.Today();
        }

        private string SiguienteNumeroNotaDeDebito() {
            return ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).SiguienteNumeroDocumentoAntesDeImprentaDigital(eTipoDocumentoFactura.NotaDeDebito, eTalonario.Talonario1, (eTipoDePrefijo)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "TipoDePrefijoND"));
        }

        private string SiguienteNumeroNotaDeCredito() {
            return ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).SiguienteNumeroDocumentoAntesDeImprentaDigital(eTipoDocumentoFactura.NotaDeCredito, eTalonario.Talonario1, (eTipoDePrefijo)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "TipoDePrefijoNC"));
        }

        private string SiguienteNumeroFacturaTalonario1() {
            string vResult;
            string vSiguienteNumeroFacturaTalonario1 = ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).SiguienteNumeroDocumentoAntesDeImprentaDigital(eTipoDocumentoFactura.Factura, eTalonario.Talonario1, (eTipoDePrefijo)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "TipoDePrefijoTalonario1"));
            string vSiguienteNumeroFacturaTalonario2;
            if (_UsaDosTalonarios) {
                vSiguienteNumeroFacturaTalonario2 = ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).SiguienteNumeroDocumentoAntesDeImprentaDigital(eTipoDocumentoFactura.Factura, eTalonario.Talonario2, (eTipoDePrefijo)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "TipoDePrefijoTalonario2"));
                vResult = (LibConvert.ToInt(vSiguienteNumeroFacturaTalonario2) > LibConvert.ToInt(vSiguienteNumeroFacturaTalonario1)) ? vSiguienteNumeroFacturaTalonario2 : vSiguienteNumeroFacturaTalonario1;
            } else {
                vResult = vSiguienteNumeroFacturaTalonario1;
            }

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

        private ValidationResult FacturaT1Validating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(FacturaT1)) {
                vResult = new ValidationResult("El valor del próximo número de Factura es obligatorio.");
            }
            return vResult;
        }

        private ValidationResult NotaDeCreditoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(NotaDeCredito)) {
                vResult = new ValidationResult("El valor del próximo número de Nota de Crédito es obligatorio.");
            }
            return vResult;
        }

        private ValidationResult NotaDeDebitoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibString.IsNullOrEmpty(NotaDeDebito)) {
                vResult = new ValidationResult("El valor del próximo número de Nota de Débito es obligatorio.");
            }
            return vResult;
        }
        #endregion Validanting

    } //End of class ImprentaDigitalActivacionViewModel

} //End of namespace Galac.Saw.Uil.SttDef.ViewModel