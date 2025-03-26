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
using Galac.Adm.Uil.Banco.ViewModel;
using LibGalac.Aos.Uil;
using Galac.Saw.Lib;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class FacturaPuntoDeVentaViewModel : LibInputViewModelMfc<FacturaPuntoDeVentaStt> {
        #region Constantes
        public const string AcumularItemsEnRenglonesDeFacturaPropertyName = "AcumularItemsEnRenglonesDeFactura";
        public const string UsaPrecioSinIvaPropertyName = "UsaPrecioSinIva";
        public const string TipoDeNivelDePreciosPropertyName = "TipoDeNivelDePrecios";
        public const string ConceptoBancarioCobroDirectoPropertyName = "ConceptoBancarioCobroDirecto";
        public const string CuentaBancariaCobroDirectoPropertyName = "CuentaBancariaCobroDirecto";
        public const string ImprimeDireccionAlFinalDelComprobanteFiscalPropertyName = "ImprimeDireccionAlFinalDelComprobanteFiscal";
        public const string UsaCobroDirectoPropertyName = "UsaCobroDirecto";
        public const string IsEnabledCuentaBancariaCobroDirectoPropertyName = "IsEnabledCuentaBancariaCobroDirecto";
        public const string IsEnabledConceptoBancarioCobroDirectoPropertyName = "IsEnabledConceptoBancarioCobroDirecto";
        public const string UsaClienteGenericoAlFacturarPropertyName = "UsaClienteGenericoAlFacturar";
        public const string UsarBalanzaPropertyName = "UsarBalanza";
        public const string UsaBusquedaDinamicaEnPuntoDeVentaPropertyName = "UsaBusquedaDinamicaEnPuntoDeVenta";
        #endregion
        #region Variables
        private FkConceptoBancarioViewModel _ConexionConceptoBancarioCobroDirecto = null;
        private FkCuentaBancariaViewModel _ConexionCuentaBancariaCobroDirecto = null;
        #endregion //Variables
        #region Propiedades
        public bool InitFirstTime { get; set; }        
        public override string ModuleName {
            get { return "2.6.- Punto de Venta"; }
        }        

        public bool  AcumularItemsEnRenglonesDeFactura {
            get {
                return Model.AcumularItemsEnRenglonesDeFacturaAsBool;                
            }
            set {
                if (Model.AcumularItemsEnRenglonesDeFacturaAsBool != value) {
                    Model.AcumularItemsEnRenglonesDeFacturaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(AcumularItemsEnRenglonesDeFacturaPropertyName);                    
                }
            }
        }

        public bool  UsaPrecioSinIva {
            get {
                return Model.UsaPrecioSinIvaAsBool;
            }
            set {
                if (Model.UsaPrecioSinIvaAsBool != value) {
                    Model.UsaPrecioSinIvaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaPrecioSinIvaPropertyName);
                    LibMessages.Notification.Send<bool>(Model.UsaPrecioSinIvaAsBool, UsaPrecioSinIvaPropertyName);
                }
            }
        }

        public eTipoDeNivelDePrecios  TipoDeNivelDePrecios {
            get {
                return Model.TipoDeNivelDePreciosAsEnum;
            }
            set {
                if (Model.TipoDeNivelDePreciosAsEnum != value) {
                    Model.TipoDeNivelDePreciosAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDeNivelDePreciosPropertyName);
                    LibMessages.Notification.Send<eTipoDeNivelDePrecios>(Model.TipoDeNivelDePreciosAsEnum, TipoDeNivelDePreciosPropertyName);
                }
            }
        }
       [LibCustomValidation("ConceptoBancarioCobroDirectoValidating")]
        public string ConceptoBancarioCobroDirecto {
            get {
                return Model.ConceptoBancarioCobroDirecto;
            }
            set {
                if (Model.ConceptoBancarioCobroDirecto != value) {
                    Model.ConceptoBancarioCobroDirecto = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConceptoBancarioCobroDirectoPropertyName);
                    LibMessages.Notification.Send<string>(Model.ConceptoBancarioCobroDirecto, ConceptoBancarioCobroDirectoPropertyName);
                    if (LibString.IsNullOrEmpty(ConceptoBancarioCobroDirecto, true)) {
                        ConexionConceptoBancarioCobroDirecto = null;
                    }
                }
            }
        }

       public string CuentaBancariaCobroDirecto {
           get {
               return Model.CuentaBancariaCobroDirecto;
           }
           set {
               if (Model.CuentaBancariaCobroDirecto != value) {
                   if (UsaCobroDirecto && value == null) {
                       ExecuteChooseCuentaBancariaCobroDirectoCommand(string.Empty);
                   } else {
                       Model.CuentaBancariaCobroDirecto = value;
                   }
                   IsDirty = true;
                   RaisePropertyChanged(CuentaBancariaCobroDirectoPropertyName);
                   LibMessages.Notification.Send<string>(Model.CuentaBancariaCobroDirecto, CuentaBancariaCobroDirectoPropertyName);
                   if (LibString.IsNullOrEmpty(CuentaBancariaCobroDirecto, true)) {
                       ConexionCuentaBancariaCobroDirecto = null;
                   }
               }
           }
       }

        public bool ImprimeDireccionAlFinalDelComprobanteFiscal {
            get {
               return Model.ImprimeDireccionAlFinalDelComprobanteFiscalAsBool;                
            }
            set {                
                if (Model.ImprimeDireccionAlFinalDelComprobanteFiscalAsBool != value) {                    
                    Model.ImprimeDireccionAlFinalDelComprobanteFiscalAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimeDireccionAlFinalDelComprobanteFiscalPropertyName);                    
                    LibMessages.Notification.Send<bool>(Model.ImprimeDireccionAlFinalDelComprobanteFiscalAsBool, ImprimeDireccionAlFinalDelComprobanteFiscalPropertyName);
                }
            }
        }

        public bool UsaCobroDirecto {
            get {
                return Model.UsaCobroDirectoAsBool;
            }
            set {
                if (Model.UsaCobroDirectoAsBool != value) {
                    Model.UsaCobroDirectoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaCobroDirectoPropertyName);
                    RaisePropertyChanged(IsEnabledConceptoBancarioCobroDirectoPropertyName);
                    RaisePropertyChanged(IsEnabledCuentaBancariaCobroDirectoPropertyName);
                }
            }
        }
		
        public bool  UsarBalanza {
            get {
                return Model.UsarBalanzaAsBool;
            }
            set {
                if (Model.UsarBalanzaAsBool != value) {
                    Model.UsarBalanzaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsarBalanzaPropertyName);
                }
            }
        }

        public eTipoDeNivelDePrecios[] ArrayTipoDeNivelDePrecios {
            get {
                return LibEnumHelper<eTipoDeNivelDePrecios>.GetValuesInArray();
            }
        }

        public FkConceptoBancarioViewModel ConexionConceptoBancarioCobroDirecto
        {
           get
           {
              return _ConexionConceptoBancarioCobroDirecto;
           }
           set
           {
              if (_ConexionConceptoBancarioCobroDirecto != value)
              {
                 _ConexionConceptoBancarioCobroDirecto = value;
                 if (_ConexionConceptoBancarioCobroDirecto != null)
                 {
                    ConceptoBancarioCobroDirecto = _ConexionConceptoBancarioCobroDirecto.Codigo;
                 }
              }
              if (_ConexionConceptoBancarioCobroDirecto == null)
              {
                 ConceptoBancarioCobroDirecto = string.Empty;
              }
              RaisePropertyChanged(ConceptoBancarioCobroDirectoPropertyName);
           }
        }

        public FkCuentaBancariaViewModel ConexionCuentaBancariaCobroDirecto {
            get {
                return _ConexionCuentaBancariaCobroDirecto;
            }
            set {
                if (_ConexionCuentaBancariaCobroDirecto != value) {
                    _ConexionCuentaBancariaCobroDirecto = value;
                    RaisePropertyChanged(CuentaBancariaCobroDirectoPropertyName);                    
                }
                if (_ConexionCuentaBancariaCobroDirecto == null) {
                    CuentaBancariaCobroDirecto = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseConceptoBancarioCobroDirectoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaBancariaCobroDirectoCommand {
            get;
            private set;
        }

        public bool UsaClienteGenericoAlFacturar {
            get {
                return Model.UsaClienteGenericoAlFacturarAsBool;
            }
            set {
                if (Model.UsaClienteGenericoAlFacturarAsBool != value) {
                    Model.UsaClienteGenericoAlFacturarAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaClienteGenericoAlFacturarPropertyName);
                }
            }
        }

        public bool UsaBusquedaDinamicaEnPuntoDeVenta {
            get {
                return Model.UsaBusquedaDinamicaEnPuntoDeVentaAsBool;
            }
            set {
                if(Model.UsaBusquedaDinamicaEnPuntoDeVentaAsBool != value) {
                    Model.UsaBusquedaDinamicaEnPuntoDeVentaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaBusquedaDinamicaEnPuntoDeVentaPropertyName);
                }
            }
        }
        #endregion //Propiedades
        #region Constructores
        public FacturaPuntoDeVentaViewModel()
            : this(new FacturaPuntoDeVentaStt(), eAccionSR.Insertar, true) {
        }
        public FacturaPuntoDeVentaViewModel(FacturaPuntoDeVentaStt initModel, eAccionSR initAction, bool firstTime)
           : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            InitFirstTime = firstTime;
            LibMessages.Notification.Register<bool>(this, OnBooleanParametrosComunesChanged);
            LibMessages.Notification.Register<eTipoDeNivelDePrecios>(this, OnTipoDeNivelDePreciosChanged);
            LibMessages.Notification.Register<string>(this, OnStringParametrosComunesChanged);
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(FacturaPuntoDeVentaStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override FacturaPuntoDeVentaStt FindCurrentRecord(FacturaPuntoDeVentaStt valModel) {
            if (valModel == null) {
                return new FacturaPuntoDeVentaStt();
            }
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<FacturaPuntoDeVentaStt>, IList<FacturaPuntoDeVentaStt>> GetBusinessComponent()
        {
            return null;
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseConceptoBancarioCobroDirectoCommand = new RelayCommand<string>(ExecuteChooseConceptoBancarioCobroDirectoCommand);
            ChooseCuentaBancariaCobroDirectoCommand = new RelayCommand<string>(ExecuteChooseCuentaBancariaCobroDirectoCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();            
            ConexionConceptoBancarioCobroDirecto = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", LibSearchCriteria.CreateCriteria("codigo", ConceptoBancarioCobroDirecto), new clsSettValueByCompanyNav());            
            ConexionCuentaBancariaCobroDirecto = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkCuentaBancariaViewModel>("Cuenta Bancaria", LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.Codigo", CuentaBancariaCobroDirecto), new clsSettValueByCompanyNav());
        }

        private void ExecuteChooseConceptoBancarioCobroDirectoCommand(string valcodigo)
        {
           try
           {
              if (valcodigo == null)
              {
                 valcodigo = string.Empty;
              }              
              LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valcodigo);
              LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Ingreso));
              ConexionConceptoBancarioCobroDirecto = null;
              ConexionConceptoBancarioCobroDirecto = LibFKRetrievalHelper.ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);
           }
           catch (System.AccessViolationException)
           {
              throw;
           }
           catch (System.Exception vEx)
           {
              LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
           }
        }

        private void ExecuteChooseCuentaBancariaCobroDirectoCommand(string valCodigo)
        {
           try
           {
              if (valCodigo == null)
              {
                 valCodigo = string.Empty;
              }
              LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_CuentaBancaria_B1.Codigo", valCodigo);
              LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.EsCajaChica", LibConvert.BoolToSN(false));
              vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.ConsecutivoCompania", Mfc.GetInt("Compania")), eLogicOperatorType.And);
              ConexionCuentaBancariaCobroDirecto = null;
              ConexionCuentaBancariaCobroDirecto = LibFKRetrievalHelper.ChooseRecord<FkCuentaBancariaViewModel>("Cuenta Bancaria", vDefaultCriteria, vFixedCriteria, string.Empty);
              if (ConexionCuentaBancariaCobroDirecto != null)
              {
                 CuentaBancariaCobroDirecto = ConexionCuentaBancariaCobroDirecto.Codigo;
              }
              else
              {
                 CuentaBancariaCobroDirecto = string.Empty;
              }
           }
           catch (System.AccessViolationException)
           {
              throw;
           }
           catch (System.Exception vEx)
           {
              LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
           }
        }
        
        private void ReloadCodigoGenericoCuentaBancaria()
        {
           Galac.Adm.Ccl.Banco.ICuentaBancariaPdn insCuentaBancariaPdn = new Galac.Adm.Brl.Banco.clsCuentaBancariaNav();
           ConexionCuentaBancariaCobroDirecto = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkCuentaBancariaViewModel>("Cuenta Bancaria", LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.Codigo", insCuentaBancariaPdn.GetCuentaBancariaGenericaPorDefecto()), new clsSettValueByCompanyNav());
        }
       
        private ValidationResult ConceptoBancarioCobroDirectoValidating()
        {
           ValidationResult vResult = ValidationResult.Success;
           if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar))
           {
              return ValidationResult.Success;
           }
           else
           {
              if (UsaCobroDirecto && LibString.IsNullOrEmpty(ConceptoBancarioCobroDirecto))
              {
                 vResult = new ValidationResult(this.ModuleName + "-> Debe indicar un Concepto Bancario de Cobro Directo");
              }
           }
           return vResult;
        }

        private ValidationResult CuentaBancariaCobroDirectoValidating()
        {
           ValidationResult vResult = ValidationResult.Success;
           if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar))
           {
              return ValidationResult.Success;
           }
           else
           {
              if (UsaCobroDirecto && LibString.IsNullOrEmpty(CuentaBancariaCobroDirecto))
              {
                 vResult = new ValidationResult(this.ModuleName + "-> Debe indicar una cuenta bancaria cobro directo");
              }
           }
           return vResult;
        }

        public bool IsEnabledCuentaBancariaCobroDirecto
        {
           get
           {
              return IsEnabled && UsaCobroDirecto;
           }
        }

        public bool IsEnabledConceptoBancarioCobroDirecto
        {
           get
           {
              return IsEnabled && UsaCobroDirecto;
           }
        }
        public bool IsEnabledUsaPrecioSinIva {
            get {
                    if (LibString.IsNullOrEmpty(AppMemoryInfo.GlobalValuesGetString("Parametros", "SesionEspecialPrecioSinIva")) ) {
                        return false;
	                } else {
                        return InitFirstTime || AppMemoryInfo.GlobalValuesGetBool("Parametros", "SesionEspecialPrecioSinIva");
                    }
                }
        }

        public bool IsEnabledPuntoDeVenta {
            get {
                return !UsaImprentaDigital();
            }
        }

        private void OnBooleanParametrosComunesChanged(NotificationMessage<bool> valMessage) {
            try {
                if (LibString.S1IsEqualToS2(LibConvert.ToStr(valMessage.Notification), ImprimeDireccionAlFinalDelComprobanteFiscalPropertyName)) {
                    ImprimeDireccionAlFinalDelComprobanteFiscal = valMessage.Content;
                } else if (LibString.S1IsEqualToS2(LibConvert.ToStr(valMessage.Notification), UsaPrecioSinIvaPropertyName)) {
                    UsaPrecioSinIva = valMessage.Content;
                } else if (LibString.S1IsEqualToS2(LibConvert.ToStr(valMessage.Notification), UsaCobroDirectoPropertyName)) {
                    UsaCobroDirecto = valMessage.Content;
                }

            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void OnTipoDeNivelDePreciosChanged(NotificationMessage<eTipoDeNivelDePrecios> valMessage) {
            try {
                if (LibString.S1IsEqualToS2(LibConvert.ToStr(valMessage.Notification), TipoDeNivelDePreciosPropertyName)) {
                    TipoDeNivelDePrecios = valMessage.Content;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void OnStringParametrosComunesChanged(NotificationMessage<string> valMessage) {
            try {
                if (LibString.S1IsEqualToS2(LibConvert.ToStr(valMessage.Notification), CuentaBancariaCobroDirectoPropertyName)) {
                    CuentaBancariaCobroDirecto = valMessage.Content;
                } else if (LibString.S1IsEqualToS2(LibConvert.ToStr(valMessage.Notification), ConceptoBancarioCobroDirectoPropertyName)) {
                    ConceptoBancarioCobroDirecto = valMessage.Content;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private bool UsaImprentaDigital() {
            return LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaImprentaDigital"));
        }

        public bool EsFacturadorBasico {
            get {
                clsLibSaw inslibsaw = new clsLibSaw();
                return inslibsaw.EsVersionFacturadorBasico();
            }
        }
        
        public bool IsVisibleCuentaBancariaCobroDirecto {
            get {
                return !EsFacturadorBasico;
            }
        }
        public bool IsVisibleConceptoBancarioCobroDirecto {
            get {
                return !EsFacturadorBasico;
            }
        }
        #endregion //Metodos Generados
    } //End of class FacturaPuntoDeVentaViewModel

} //End of namespace Galac.Saw.Uil.SttDef

