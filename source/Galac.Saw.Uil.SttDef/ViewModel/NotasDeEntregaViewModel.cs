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
    public class NotasDeEntregaViewModel : LibInputViewModelMfc<NotaEntregaStt> {
        #region Constantes
        public const string TipoPrefijoNotaEntregaPropertyName = "TipoPrefijoNotaEntrega";
        public const string NumCopiasOrdenDeDespachoPropertyName = "NumCopiasOrdenDeDespacho";
        public const string NotaEntregaPreNumeradaPropertyName = "NotaEntregaPreNumerada";
        public const string NombrePlantillaOrdenDeDespachoPropertyName = "NombrePlantillaOrdenDeDespacho";
        public const string NombrePlantillaNotaEntregaPropertyName = "NombrePlantillaNotaEntrega";
        public const string PrimeraNotaEntregaPropertyName = "PrimeraNotaEntrega";
        public const string PrefijoNotaEntregaPropertyName = "PrefijoNotaEntrega";
        public const string ModeloNotaEntregaPropertyName = "ModeloNotaEntrega";
        public const string ModeloNotaEntregaModoTextoPropertyName = "ModeloNotaEntregaModoTexto";
        public const string IsEnabledPlantillaNotaEntregaPropertyName="IsEnabledPlantillaNotaEntrega";
        public const string IsEnableDatosNumeroNotaEntregaPropertyName = "IsEnableDatosNumeroNotaEntrega";

        public const string IsVisibleModeloNotaEntregaModoTextoPropertyName = "IsVisibleModeloNotaEntregaModoTexto";
        public const string IsEnabledPrefijoNotaEntregaPropertyName = "IsEnabledPrefijoNotaEntrega";
        private const string IsEnabledUsaImprentaDigitalPropertyName = "IsEnabledUsaImprentaDigital";
        #endregion
        #region Variables
        private bool _IsEnabledUsaImprentaDigital;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "8.1-Notas de entrega"; }
        }

        public eTipoDePrefijo  TipoPrefijoNotaEntrega {
            get {
                return Model.TipoPrefijoNotaEntregaAsEnum;
            }
            set {
                if (Model.TipoPrefijoNotaEntregaAsEnum != value) {
                    Model.TipoPrefijoNotaEntregaAsEnum = value;
                    IsDirty = true;
                    if (TipoPrefijoNotaEntrega == eTipoDePrefijo.SinPrefijo) {
                        PrefijoNotaEntrega = "";
                    }
                    RaisePropertyChanged(TipoPrefijoNotaEntregaPropertyName);
                    RaisePropertyChanged(IsEnabledPrefijoNotaEntregaPropertyName);                    
                }
            }
        }

        public int  NumCopiasOrdenDeDespacho {
            get {
                return Model.NumCopiasOrdenDeDespacho;
            }
            set {
                if (Model.NumCopiasOrdenDeDespacho != value) {
                    Model.NumCopiasOrdenDeDespacho = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumCopiasOrdenDeDespachoPropertyName);
                }
            }
        }

        public bool  NotaEntregaPreNumerada {
            get {
                return Model.NotaEntregaPreNumeradaAsBool;
            }
            set {
                if (Model.NotaEntregaPreNumeradaAsBool != value) {
                    Model.NotaEntregaPreNumeradaAsBool = value;
                    IsDirty = true;
                    if (NotaEntregaPreNumerada) {
                        TipoPrefijoNotaEntrega = eTipoDePrefijo.SinPrefijo;
                    }
                    RaisePropertyChanged(NotaEntregaPreNumeradaPropertyName);
                    RaisePropertyChanged(IsEnableDatosNumeroNotaEntregaPropertyName);
                }
            }
        }

        [LibCustomValidation("NombrePlantillaOrdenDeDespachoValidating")]
        public string  NombrePlantillaOrdenDeDespacho {
            get {
                return Model.NombrePlantillaOrdenDeDespacho;
            }
            set {
                if (Model.NombrePlantillaOrdenDeDespacho != value) {
                    Model.NombrePlantillaOrdenDeDespacho = value;
                    IsDirty = true;
                    if (LibString.IsNullOrEmpty(NombrePlantillaOrdenDeDespacho)) {
                        ExecuteBuscarPlantillaCommandOrdenDespacho();
                    }
                    RaisePropertyChanged(NombrePlantillaOrdenDeDespachoPropertyName);
                }
            }
        }

        [LibCustomValidation ("NombrePlantillaNotaEntregaValidating")]
        public string  NombrePlantillaNotaEntrega {
            get {
                return Model.NombrePlantillaNotaEntrega;
            }
            set {
                if (Model.NombrePlantillaNotaEntrega != value) {                    
                    Model.NombrePlantillaNotaEntrega = value;
                    IsDirty = true;
                    if (LibString.IsNullOrEmpty(NombrePlantillaNotaEntrega)) {
                        ExecuteBuscarPlantillaCommandNotaEntrega ();
                    }
                    RaisePropertyChanged(NombrePlantillaNotaEntregaPropertyName);
                }
            }
        }

        public string  PrimeraNotaEntrega {
            get {
                return Model.PrimeraNotaEntrega;
            }
            set {
                if (Model.PrimeraNotaEntrega != value) {
                    Model.PrimeraNotaEntrega = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrimeraNotaEntregaPropertyName);
                }
            }
        }

        public string  PrefijoNotaEntrega {
            get {
                return Model.PrefijoNotaEntrega;
            }
            set {
                if (Model.PrefijoNotaEntrega != value) {
                    Model.PrefijoNotaEntrega = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrefijoNotaEntregaPropertyName);
                }
            }
        }

        public eModeloDeFactura  ModeloNotaEntrega {
            get {
                return Model.ModeloNotaEntregaAsEnum;
            }
            set {
                if (Model.ModeloNotaEntregaAsEnum != value) {
                    Model.ModeloNotaEntregaAsEnum = value;
                    IsDirty = true;
                    if ((ModeloNotaEntrega == eModeloDeFactura.eMD_OTRO) && LibString.IsNullOrEmpty(NombrePlantillaNotaEntrega, true)) {
                        NombrePlantillaNotaEntrega = "rpxNotaDeEntregaFormatoLibre";
                    }
                    RaisePropertyChanged(ModeloNotaEntregaPropertyName);
                    RaisePropertyChanged(IsEnabledPlantillaNotaEntregaPropertyName);
                    RaisePropertyChanged(IsVisibleModeloNotaEntregaModoTextoPropertyName);
                }
            }
        }

        public string  ModeloNotaEntregaModoTexto {
            get {
                return Model.ModeloNotaEntregaModoTexto;
            }
            set {
                if (Model.ModeloNotaEntregaModoTexto != value) {
                    Model.ModeloNotaEntregaModoTexto = value;
                    IsDirty = true;
                    RaisePropertyChanged(ModeloNotaEntregaModoTextoPropertyName);
                }
            }
        }

        public eTipoDePrefijo[] ArrayTipoDePrefijo {
            get {
                return LibEnumHelper<eTipoDePrefijo>.GetValuesInArray();
            }
        }

        public eModeloDeFactura[] ArrayModeloDeFactura {
            get {
                return LibEnumHelper<eModeloDeFactura>.GetValuesInArray();
            }
        }

        public RelayCommand ChooseTemplateCommandNotaEntrega {
            get;
            private set;
        }

        public RelayCommand ChooseTemplateCommandOrdenDespacho {
            get;
            private set;
        }

        public bool IsEnabledPlantillaNotaEntrega {
            get {
                return (ModeloNotaEntrega == eModeloDeFactura.eMD_OTRO) && IsNotEnabledUsaImprentaDigital;
            }
        }
       

        public string[] ArrayModeloNotaEntregaModoTexto {
            get {
                return new clsUtilParameters().GetModelosPlanillasList().ToArray();
            }
        }

        public bool IsVisibleModeloNotaEntregaModoTexto {
            get {
                return ModeloNotaEntrega == eModeloDeFactura.eMD_IMPRESION_MODO_TEXTO;
            }
        }

        public bool IsEnabledTipoPrefijo {
            get {
                return Model.NotaEntregaPreNumeradaAsBool;
            }
        }

        public bool IsEnableDatosNumeroNotaEntrega {
            get {
                return IsEnabled && !NotaEntregaPreNumerada;
            }
        }

        public bool IsEnabledPrefijoNotaEntrega {
            get {
                return IsEnabled && IsEnableDatosNumeroNotaEntrega && TipoPrefijoNotaEntrega == eTipoDePrefijo.Indicar;
            }
        }

        public bool IsEnabledUsaImprentaDigital {
            get { return (_IsEnabledUsaImprentaDigital || UsaImprentaDigital()); }
            set {
                if (_IsEnabledUsaImprentaDigital != value) {
                    _IsEnabledUsaImprentaDigital = value;
                    RaisePropertyChanged(IsEnabledUsaImprentaDigitalPropertyName);
                }
            }
        }

        public bool IsNotEnabledUsaImprentaDigital {
            get {
                return IsEnabled && !IsEnabledUsaImprentaDigital;
            }
        }
        #endregion //Propiedades
        #region Constructores
        public NotasDeEntregaViewModel()
            : this(new NotaEntregaStt(), eAccionSR.Insertar) {
        }
        public NotasDeEntregaViewModel(NotaEntregaStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = TipoPrefijoNotaEntregaPropertyName;
            LibMessages.Notification.Register<bool>(this, OnUsaImprentaDigitalChanged);
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeCommands() {
            ChooseTemplateCommandNotaEntrega = new RelayCommand(ExecuteBuscarPlantillaCommandNotaEntrega);
            ChooseTemplateCommandOrdenDespacho = new RelayCommand(ExecuteBuscarPlantillaCommandOrdenDespacho);
        }

        protected override void InitializeLookAndFeel(NotaEntregaStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override NotaEntregaStt FindCurrentRecord(NotaEntregaStt valModel) {
            if (valModel == null) {
                return new NotaEntregaStt();
            }
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInEnum("TipoPrefijoNotaEntrega", LibConvert.EnumToDbValue((int)valModel.TipoPrefijoNotaEntregaAsEnum));
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "NotaEntregaSttGET", vParams.Get()).FirstOrDefault();
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<NotaEntregaStt>, IList<NotaEntregaStt>> GetBusinessComponent() {
            return null;
        }

        private void ExecuteBuscarPlantillaCommandNotaEntrega() {
            try {
                NombrePlantillaNotaEntrega = new clsUtilParameters().BuscarNombrePlantilla("rpx de Nota Entrega (*.rpx)|*Nota*Entrega*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteBuscarPlantillaCommandOrdenDespacho() {
            try {
                NombrePlantillaOrdenDeDespacho = new clsUtilParameters().BuscarNombrePlantilla("rpx de Orden Despacho (*.rpx)|*Orden*Despacho*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult NombrePlantillaOrdenDeDespachoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(NombrePlantillaOrdenDeDespacho)) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> Nombre de la Plantilla de Orden de Despacho, es requerido.");
                } else if (!LibString.IsNullOrEmpty(NombrePlantillaOrdenDeDespacho) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaOrdenDeDespacho)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaOrdenDeDespacho + ", en " + this.ModuleName + ", no EXISTE.");
                }
            }
            return vResult;
        }

        private ValidationResult NombrePlantillaNotaEntregaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (ModeloNotaEntrega == eModeloDeFactura.eMD_OTRO) {
                    if (LibString.IsNullOrEmpty(NombrePlantillaNotaEntrega)) {
                        vResult = new ValidationResult("El campo " + ModuleName + "-> Nombre de la Plantilla de Nota Entrega, es requerido.");                        
                    }else if (!clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaNotaEntrega)) {
                        vResult = new ValidationResult("El RPX " + NombrePlantillaNotaEntrega + ", en " + this.ModuleName + ", no EXISTE.");
                    }
                }
                
            }
            return vResult;
        }

        private void OnUsaImprentaDigitalChanged(NotificationMessage<bool> valMessage) {
            if (LibString.S1IsEqualToS2(valMessage.Notification, "UsaImprentaDigital")) {
                IsEnabledUsaImprentaDigital = valMessage.Content || UsaImprentaDigital();
            }
        }

        private bool UsaImprentaDigital() {
            return LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaImprentaDigital"));
        }
        #endregion //Metodos Generados

    } //End of class NotaEntregaSttViewModel

} //End of namespace Galac.Saw.Uil.SttDef

