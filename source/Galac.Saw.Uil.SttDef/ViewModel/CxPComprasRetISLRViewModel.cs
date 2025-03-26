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
using LibGalac.Aos.Uil;
using Galac.Saw.Lib;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class CxPComprasRetISLRViewModel:LibInputViewModelMfc<RetencionISLRStt> {
        #region Variables
        private FkCiudadViewModel _ConexionCiudad = null;
        bool mEsFacturadorBasico;
        #endregion //Variables
        #region Constantes
        public const string NumeroRifrPropertyName = "NumeroRifr";
        public const string NumCopiasComprobanteRetencionPropertyName = "NumCopiasComprobanteRetencion";
        public const string NombreYapellidorPropertyName = "NombreYapellidor";
        public const string TelefonorPropertyName = "Telefonor";
        public const string TomarEnCuentaRetencionesCeroParaArcvyRaPropertyName = "TomarEnCuentaRetencionesCeroParaArcvyRa";
        public const string UsaRetencionPropertyName = "UsaRetencion";
        public const string MesDelCierreFiscalPropertyName = "MesDelCierreFiscal";
        public const string EnDondeRetenerISLRPropertyName = "EnDondeRetenerISLR";
        public const string DiaDelCierreFiscalPropertyName = "DiaDelCierreFiscal";
        public const string DireccionrPropertyName = "Direccionr";
        public const string CorreoElectronicoRepLegalPropertyName = "CorreoElectronicoRepLegal";
        public const string CiudadRepLegalPropertyName = "CiudadRepLegal";
        public const string CodTelfrPropertyName = "CodTelfr";
        public const string IsEnabledDatosRetencionPropertyName = "IsEnabledDatosRetencion";
        public const string NombrePlantillaComprobanteDeRetISRLPropertyName = "NombrePlantillaComprobanteDeRetISRL";

        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "6.4.- Retención ISLR"; }
        }

        public string NumeroRifr {
            get {
                return Model.NumeroRIFR;
            }
            set {
                if(Model.NumeroRIFR != value) {
                    Model.NumeroRIFR = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroRifrPropertyName);
                }
            }
        }

        [LibCustomValidation("NumCopiasComprobanteRetencionValidating")]
        public int NumCopiasComprobanteRetencion {
            get {
                return Model.NumCopiasComprobanteRetencion;
            }
            set {
                if(Model.NumCopiasComprobanteRetencion != value) {
                    Model.NumCopiasComprobanteRetencion = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumCopiasComprobanteRetencionPropertyName);
                }
            }
        }

        public string NombreYapellidor {
            get {
                return Model.NombreYApellidoR;
            }
            set {
                if(Model.NombreYApellidoR != value) {
                    Model.NombreYApellidoR = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreYapellidorPropertyName);
                }
            }
        }

        public string Telefonor {
            get {
                return Model.TelefonoR;
            }
            set {
                if(Model.TelefonoR != value) {
                    Model.TelefonoR = value;
                    IsDirty = true;
                    RaisePropertyChanged(TelefonorPropertyName);
                }
            }
        }

        public bool TomarEnCuentaRetencionesCeroParaArcvyRa {
            get {
                return Model.TomarEnCuentaRetencionesCeroParaARCVyRAAsBool;
            }
            set {
                if(Model.TomarEnCuentaRetencionesCeroParaARCVyRAAsBool != value) {
                    Model.TomarEnCuentaRetencionesCeroParaARCVyRAAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(TomarEnCuentaRetencionesCeroParaArcvyRaPropertyName);
                }
            }
        }

        public bool UsaRetencion {
            get {
                return Model.UsaRetencionAsBool;
            }
            set {
                if(Model.UsaRetencionAsBool != value) {
                    Model.UsaRetencionAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaRetencionPropertyName);
                    RaisePropertyChanged(IsEnabledDatosRetencionPropertyName);
                }
            }
        }

        public string MesDelCierreFiscal {
            get {
                return Model.MesDelCierreFiscal;
            }
            set {
                if(Model.MesDelCierreFiscal != value) {
                    Model.MesDelCierreFiscal = value;
                    IsDirty = true;
                    RaisePropertyChanged(MesDelCierreFiscalPropertyName);
                }
            }
        }

        [LibCustomValidation("EnDondeRetenerISLRValidating")]
        public eDondeSeEfectuaLaRetencionISLR EnDondeRetenerISLR {
            get {
                return Model.EnDondeRetenerISLRAsEnum;
            }
            set {
                if(Model.EnDondeRetenerISLRAsEnum != value) {
                    Model.EnDondeRetenerISLRAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(EnDondeRetenerISLRPropertyName);
                }
            }
        }
        [LibCustomValidation("DiaDelCierreFiscalValidating")]
        public string DiaDelCierreFiscal {
            get {
                return Model.DiaDelCierreFiscal;
            }
            set {
                if(Model.DiaDelCierreFiscal != value) {
                    if(LibConvert.ToInt(value) > 31) {
                        value = "31";
                    }
                    Model.DiaDelCierreFiscal = value;
                    IsDirty = true;
                    RaisePropertyChanged(DiaDelCierreFiscalPropertyName);
                }
            }
        }

        public string Direccionr {
            get {
                return Model.DireccionR;
            }
            set {
                if(Model.DireccionR != value) {
                    Model.DireccionR = value;
                    IsDirty = true;
                    RaisePropertyChanged(DireccionrPropertyName);
                }
            }
        }

        public string CorreoElectronicoRepLegal {
            get {
                return Model.CorreoElectronicoRepLegal;
            }
            set {
                if(Model.CorreoElectronicoRepLegal != value) {
                    Model.CorreoElectronicoRepLegal = value;
                    IsDirty = true;
                    RaisePropertyChanged(CorreoElectronicoRepLegalPropertyName);
                }
            }
        }

        [LibCustomValidation("NombrePlantillaComprobanteDeRetISRLValidating")]
        public string NombrePlantillaComprobanteDeRetISRL {
            get {
                return Model.NombrePlantillaComprobanteDeRetISRL;
            }
            set {
                if(Model.NombrePlantillaComprobanteDeRetISRL != value) {
                    Model.NombrePlantillaComprobanteDeRetISRL = value;
                    IsDirty = true;
                    if(LibString.IsNullOrEmpty(NombrePlantillaComprobanteDeRetISRL)) {
                        ExecuteBuscarPlantillaCommand();
                    }
                    RaisePropertyChanged(NombrePlantillaComprobanteDeRetISRLPropertyName);
                }
            }
        }

        private ValidationResult CiudadRepLegalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(LibString.IsNullOrEmpty(CiudadRepLegal)) {
                    vResult = new ValidationResult("En la sección 6.4.- Retención ISLR-> Debe colocar una Ciudad válida para el Representante Legal");
                }
            }
            return vResult;
        }

        [LibCustomValidation("CiudadRepLegalValidating")]
        public string CiudadRepLegal {
            get {
                return Model.CiudadRepLegal;
            }
            set {
                if(Model.CiudadRepLegal != value) {
                    Model.CiudadRepLegal = value;
                    IsDirty = true;
                    RaisePropertyChanged(CiudadRepLegalPropertyName);
                }
            }
        }

        public string CodTelfr {
            get {
                return Model.CodTelfR;
            }
            set {
                if(Model.CodTelfR != value) {
                    Model.CodTelfR = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodTelfrPropertyName);
                }
            }
        }

        public eDondeSeEfectuaLaRetencionISLR[] ArrayDondeSeEfectuaLaRetencionISLR {
            get {
                return LibEnumHelper<eDondeSeEfectuaLaRetencionISLR>.GetValuesInArray();
            }
        }

        public bool IsEnabledUsaRetencion {
            get {
                return IsEnabled && !UsaRetencion;
            }
        }

        public bool IsEnabledDatosRetencion {
            get {
                return IsEnabled && UsaRetencion;
            }
        }

        public RelayCommand ChooseTemplateCommand {
            get;
            private set;
        }

        public FkCiudadViewModel ConexionCiudad {
            get {
                return _ConexionCiudad;
            }
            set {
                if(_ConexionCiudad != value) {
                    _ConexionCiudad = value;
                    if(_ConexionCiudad != null) {
                        CiudadRepLegal = _ConexionCiudad.NombreCiudad;
                    }
                }
                if(_ConexionCiudad == null) {
                    CiudadRepLegal = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCiudadCommand {
            get;
            private set;
        }

        #endregion //Propiedades
        #region Constructores
        public CxPComprasRetISLRViewModel()
            : this(new RetencionISLRStt(),eAccionSR.Insertar) {
        }
        public CxPComprasRetISLRViewModel(RetencionISLRStt initModel,eAccionSR initAction)
            : base(initModel,initAction,LibGlobalValues.Instance.GetAppMemInfo(),LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = NumeroRifrPropertyName;
            mEsFacturadorBasico = new clsLibSaw().EsVersionFacturadorBasico();
            // Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCiudadCommand = new RelayCommand<string>(ExecuteChooseCiudadCommand);
            ChooseTemplateCommand = new RelayCommand(ExecuteBuscarPlantillaCommand);
        }

        protected override void InitializeLookAndFeel(RetencionISLRStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override RetencionISLRStt FindCurrentRecord(RetencionISLRStt valModel) {
            if(valModel == null) {
                return new RetencionISLRStt();
            }
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInString("NumeroRifr", valModel.NumeroRifr, 15);
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "CxPComprasRetISLRGET", vParams.Get()).FirstOrDefault();
            return valModel;
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionCiudad = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkCiudadViewModel>("Ciudad",LibSearchCriteria.CreateCriteria("NombreCiudad",CiudadRepLegal),new clsSettValueByCompanyNav());
        }

        protected override ILibBusinessComponentWithSearch<IList<RetencionISLRStt>,IList<RetencionISLRStt>> GetBusinessComponent() {
            return null;
        }

        private void ExecuteChooseCiudadCommand(string valNombreCiudad) {
            try {
                if(valNombreCiudad == null) {
                    valNombreCiudad = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("NombreCiudad",valNombreCiudad);
                ConexionCiudad = null;
                ConexionCiudad = LibFKRetrievalHelper.ChooseRecord<FkCiudadViewModel>("Ciudad",vDefaultCriteria,null,string.Empty);
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        #endregion //Metodos Generados

        private ValidationResult NumCopiasComprobanteRetencionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(UsaRetencion && NumCopiasComprobanteRetencion == 0) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> Num Copias Comprobante Retencion, es requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult DiaDelCierreFiscalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(UsaRetencion && LibConvert.ToInt(DiaDelCierreFiscal) <= 0) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> Dia del Cierre Fiscal, debe ser mayor a 0.");
                }
            }
            return vResult;
        }

        private ValidationResult EnDondeRetenerISLRValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(UsaRetencion && EnDondeRetenerISLR == eDondeSeEfectuaLaRetencionISLR.NoRetenida) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> En donde Retener ISLR, es requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult NombrePlantillaComprobanteDeRetISRLValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(IsEnabledDatosRetencion && LibString.IsNullOrEmpty(NombrePlantillaComprobanteDeRetISRL)) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> plantilla de impresión, es requerido.");
                } else if(IsEnabledDatosRetencion && !LibString.IsNullOrEmpty(NombrePlantillaComprobanteDeRetISRL) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaComprobanteDeRetISRL)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaComprobanteDeRetISRL + ", en " + ModuleName + ", no EXISTE.");
                }
            }
            return vResult;
        }

        private void ExecuteBuscarPlantillaCommand() {
            try {
                NombrePlantillaComprobanteDeRetISRL = new clsUtilParameters().BuscarNombrePlantilla("rpx de Retención ISRL (*.rpx)|*ComprobanteDeRetencion*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx,ModuleName);
            }
        }

        public bool IsVisibleRetencionISLR {
            get {
                return !mEsFacturadorBasico;
            }
        }
        public bool IsVisibleRepresentanteLegal {
            get {
                return !mEsFacturadorBasico;
            }
        }

    } //End of class CxPComprasRetISLRViewModel

} //End of namespace Galac.Saw.Uil.SttDef

