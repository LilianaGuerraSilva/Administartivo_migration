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
   public class FacturaImpresionViewModel :LibInputViewModelMfc<ImpresiondeFacturaStt> {
      #region Constantes
      public const string DetalleProdCompFacturaPropertyName = "DetalleProdCompFactura";
      public const string AccionLimiteItemsFacturaPropertyName = "AccionLimiteItemsFactura";
      public const string ConcatenaLetraEaArticuloExentoPropertyName = "ConcatenaLetraEaArticuloExento";
      public const string CantidadDeCopiasDeLaFacturaAlImprimirPropertyName = "CantidadDeCopiasDeLaFacturaAlImprimir";
      public const string NombrePlantillaAnexoSerialesPropertyName = "NombrePlantillaAnexoSeriales";
      public const string NoImprimirFacturaPropertyName = "NoImprimirFactura";
      public const string FormaDeOrdenarDetalleFacturaPropertyName = "FormaDeOrdenarDetalleFactura";
      public const string FormatoDeFechaPropertyName = "FormatoDeFecha";
      public const string ImprimeDireccionAlFinalDelComprobanteFiscalPropertyName = "ImprimeDireccionAlFinalDelComprobanteFiscal";
      public const string ImprimirAnexoDeSerialPropertyName = "ImprimirAnexoDeSerial";
      public const string ImprimirBorradorAlInsertarFacturaPropertyName = "ImprimirBorradorAlInsertarFactura";
      public const string ImprimirTipoCobroEnFacturaPropertyName = "ImprimirTipoCobroEnFactura";
      public const string ImprimirFacturaConSubtotalesPorLineaDeProductoPropertyName = "ImprimirFacturaConSubtotalesPorLineaDeProducto";
      public const string NumItemImprimirFacturaPropertyName = "NumItemImprimirFactura";
      public const string NumeroDeDigitosEnFacturaPropertyName = "NumeroDeDigitosEnFactura";
      public const string UsarDecimalesAlImprimirCantidadPropertyName = "UsarDecimalesAlImprimirCantidad";
      public const string IsVisibleFormatoFechaPropertyName = "IsVisibleFormatoFecha";
      public const string IsEnabledImprimirBorradorAlInsertarFacturaPropertyName = "IsEnabledImprimirBorradorAlInsertarFactura";
      public const string FormatoDeFechaTextoPropertyName = "FormatoDeFechaTexto";
      public const string ImprimirComprobanteFiscalEnContratoPropertyName = "ImprimirComprobanteFiscalEnContrato";
      #endregion
      #region Atributos
      private bool _IsEnabledImprimirBorradorAlInsertarFactura = true;
        #endregion

        #region Propiedades
        public bool InitFirstTime { get; set; }
        public override string ModuleName {
            get {
                return "2.3.- Impresión de Factura";
            }
        }

      public bool DetalleProdCompFactura {
         get {
            return Model.DetalleProdCompFacturaAsBool;
         }
         set {
            if(Model.DetalleProdCompFacturaAsBool != value) {
               Model.DetalleProdCompFacturaAsBool = value;
               IsDirty = true;
               RaisePropertyChanged(DetalleProdCompFacturaPropertyName);
            }
         }
      }

      public eAccionLimiteItemsFactura AccionLimiteItemsFactura {
         get {
            return Model.AccionLimiteItemsFacturaAsEnum;
         }
         set {
            if(Model.AccionLimiteItemsFacturaAsEnum != value) {
               Model.AccionLimiteItemsFacturaAsEnum = value;
               IsDirty = true;
               RaisePropertyChanged(AccionLimiteItemsFacturaPropertyName);
            }
         }
      }

      public bool ConcatenaLetraEaArticuloExento {
         get {
            return Model.ConcatenaLetraEaArticuloExentoAsBool;
         }
         set {
            if(Model.ConcatenaLetraEaArticuloExentoAsBool != value) {
               Model.ConcatenaLetraEaArticuloExentoAsBool = value;
               IsDirty = true;
               RaisePropertyChanged(ConcatenaLetraEaArticuloExentoPropertyName);
            }
         }
      }
      [LibCustomValidation("CantidadDeCopiasDeLaFacturaAlImprimirValidating")]
      public int CantidadDeCopiasDeLaFacturaAlImprimir {
         get {
            return Model.CantidadDeCopiasDeLaFacturaAlImprimir;
         }
         set {
            if(Model.CantidadDeCopiasDeLaFacturaAlImprimir != value) {
               Model.CantidadDeCopiasDeLaFacturaAlImprimir = value;
               IsDirty = true;
               RaisePropertyChanged(CantidadDeCopiasDeLaFacturaAlImprimirPropertyName);
            }
         }
      }

      [LibCustomValidation("NombrePlantillaAnexoSerialesValidating")]
      public string NombrePlantillaAnexoSeriales {
         get {
            return Model.NombrePlantillaAnexoSeriales;
         }
         set {
            if(Model.NombrePlantillaAnexoSeriales != value) {
               Model.NombrePlantillaAnexoSeriales = value;
               IsDirty = true;
               RaisePropertyChanged(NombrePlantillaAnexoSerialesPropertyName);
            }
         }
      }

      public bool NoImprimirFactura {
         get {
            return Model.NoImprimirFacturaAsBool;
         }
         set {
            if(Model.NoImprimirFacturaAsBool != value) {
               Model.NoImprimirFacturaAsBool = value;
               IsDirty = true;
               RaisePropertyChanged(NoImprimirFacturaPropertyName);
            }
         }
      }

      public eFormaDeOrdenarDetalleFactura FormaDeOrdenarDetalleFactura {
         get {
            return Model.FormaDeOrdenarDetalleFacturaAsEnum;
         }
         set {
            if(Model.FormaDeOrdenarDetalleFacturaAsEnum != value) {
               Model.FormaDeOrdenarDetalleFacturaAsEnum = value;
               IsDirty = true;
               RaisePropertyChanged(FormaDeOrdenarDetalleFacturaPropertyName);
            }
         }
      }

      public eTipoDeFormatoFecha FormatoDeFecha {
         get {
            return Model.FormatoDeFechaAsEnum;
         }
         set {
            if(Model.FormatoDeFechaAsEnum != value) {
               Model.FormatoDeFechaAsEnum = value;
               IsDirty = true;
               RaisePropertyChanged(FormatoDeFechaPropertyName);
               RaisePropertyChanged(IsVisibleFormatoFechaPropertyName);
               if(FormatoDeFecha != eTipoDeFormatoFecha.eCSF_CON_OTRO) {
                  FormatoDeFechaTexto = FormatoDeFecha.GetDescription();
               }
            }
         }
      }

      public bool ImprimeDireccionAlFinalDelComprobanteFiscal {
         get {
            return Model.ImprimeDireccionAlFinalDelComprobanteFiscalAsBool;
         }
         set {
            if(Model.ImprimeDireccionAlFinalDelComprobanteFiscalAsBool != value) {
               Model.ImprimeDireccionAlFinalDelComprobanteFiscalAsBool = value;
               IsDirty = true;
               RaisePropertyChanged(ImprimeDireccionAlFinalDelComprobanteFiscalPropertyName);
                LibMessages.Notification.Send<bool>(Model.ImprimeDireccionAlFinalDelComprobanteFiscalAsBool, ImprimeDireccionAlFinalDelComprobanteFiscalPropertyName);
            }
         }
      }

      public bool ImprimirAnexoDeSerial {
         get {
            return Model.ImprimirAnexoDeSerialAsBool;
         }
         set {
            if(Model.ImprimirAnexoDeSerialAsBool != value) {
               Model.ImprimirAnexoDeSerialAsBool = value;
               IsDirty = true;
               RaisePropertyChanged(ImprimirAnexoDeSerialPropertyName);
            }
         }
      }

      public bool ImprimirBorradorAlInsertarFactura {
         get {
            return Model.ImprimirBorradorAlInsertarFacturaAsBool;
         }
         set {
            if(Model.ImprimirBorradorAlInsertarFacturaAsBool != value) {
               Model.ImprimirBorradorAlInsertarFacturaAsBool = value;
               IsDirty = true;
               RaisePropertyChanged(ImprimirBorradorAlInsertarFacturaPropertyName);
            }
         }
      }

      public bool ImprimirTipoCobroEnFactura {
         get {
            return Model.ImprimirTipoCobroEnFacturaAsBool;
         }
         set {
            if(Model.ImprimirTipoCobroEnFacturaAsBool != value) {
               Model.ImprimirTipoCobroEnFacturaAsBool = value;
               IsDirty = true;
               RaisePropertyChanged(ImprimirTipoCobroEnFacturaPropertyName);
            }
         }
      }

      public bool ImprimirFacturaConSubtotalesPorLineaDeProducto {
         get {
            return Model.ImprimirFacturaConSubtotalesPorLineaDeProductoAsBool;
         }
         set {
            if(Model.ImprimirFacturaConSubtotalesPorLineaDeProductoAsBool != value) {
               Model.ImprimirFacturaConSubtotalesPorLineaDeProductoAsBool = value;
               IsDirty = true;
               RaisePropertyChanged(ImprimirFacturaConSubtotalesPorLineaDeProductoPropertyName);
            }
         }
      }

      public int NumItemImprimirFactura {
         get {
            return Model.NumItemImprimirFactura;
         }
         set {
            if(Model.NumItemImprimirFactura != value) {
               Model.NumItemImprimirFactura = value;
               IsDirty = true;
               RaisePropertyChanged(NumItemImprimirFacturaPropertyName);
            }
         }
      }

      [LibCustomValidation("NumeroDeDigitosEnFacturaValidating")]
      public int NumeroDeDigitosEnFactura {
         get {
            return Model.NumeroDeDigitosEnFactura;
         }
         set {
            if(Model.NumeroDeDigitosEnFactura != value) {
               Model.NumeroDeDigitosEnFactura = value;
               IsDirty = true;
               RaisePropertyChanged(NumeroDeDigitosEnFacturaPropertyName);
            }
            if(Model.NumeroDeDigitosEnFactura == 0) {
               Model.NumeroDeDigitosEnFactura = 1;
            }
         }
      }

      public bool UsarDecimalesAlImprimirCantidad {
         get {
            return Model.UsarDecimalesAlImprimirCantidadAsBool;
         }
         set {
            if(Model.UsarDecimalesAlImprimirCantidadAsBool != value) {
               Model.UsarDecimalesAlImprimirCantidadAsBool = value;
               IsDirty = true;
               RaisePropertyChanged(UsarDecimalesAlImprimirCantidadPropertyName);
            }
         }
      }
	  
      public bool  ImprimirComprobanteFiscalEnContrato {
            get {
                return Model.ImprimirComprobanteFiscalEnContratoAsBool;
            }
            set {
                if (Model.ImprimirComprobanteFiscalEnContratoAsBool != value) {
                    Model.ImprimirComprobanteFiscalEnContratoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimirComprobanteFiscalEnContratoPropertyName);
                }
            }
        }

      public eAccionLimiteItemsFactura[] ArrayAccionLimiteItemsFactura {
         get {
            return LibEnumHelper<eAccionLimiteItemsFactura>.GetValuesInArray();
         }
      }

      public eFormaDeOrdenarDetalleFactura[] ArrayFormaDeOrdenarDetalleFactura {
         get {
            return LibEnumHelper<eFormaDeOrdenarDetalleFactura>.GetValuesInArray();
         }
      }

      public eTipoDeFormatoFecha[] ArrayTipoDeFormatoFecha {
         get {
            return LibEnumHelper<eTipoDeFormatoFecha>.GetValuesInArray();
         }
      }

      public RelayCommand ChooseTemplateCommand {
         get;
         private set;
      }

      public bool IsVisibleFormatoFecha {
         get {
            return (FormatoDeFecha == eTipoDeFormatoFecha.eCSF_CON_OTRO);
         }
      }

      public bool IsEnabledImprimirBorradorAlInsertarFactura {
         get {
            return IsEnabled && _IsEnabledImprimirBorradorAlInsertarFactura;
         }
         set {
            _IsEnabledImprimirBorradorAlInsertarFactura = !value;
            RaisePropertyChanged(IsEnabledImprimirBorradorAlInsertarFacturaPropertyName);
         }

      }

      public bool IsVisibleImprimirTipoCobroEnFactura {
         get {
            return AppMemoryInfo.GlobalValuesGetBool("Parametros", "SesionEspecialImprimirTipoCobroFactura");
         }
      }

      [LibCustomValidation("FormatoDeFechaTextoValidating")]
      public string FormatoDeFechaTexto {
         get {
            return Model.FormatoDeFechaTexto;
         }
         set {
            if(Model.FormatoDeFechaTexto != value) {
               Model.FormatoDeFechaTexto = value;
               IsDirty = true;
               RaisePropertyChanged(FormatoDeFechaTextoPropertyName);
            }
         }
      }


      private ValidationResult FormatoDeFechaTextoValidating() {
         ValidationResult vResult = ValidationResult.Success;
         if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
            return ValidationResult.Success;
         } else {
            if(FormatoDeFecha == eTipoDeFormatoFecha.eCSF_CON_OTRO) {
               if(!EsFormatoDeFechaValido(FormatoDeFechaTexto)) {
                  vResult = new ValidationResult(this.ModuleName + "-> El Formato de Fecha es inválido.");

               }
            }
            return vResult;
         }
      }

      #endregion //Propiedades

      #region Constructores
      public FacturaImpresionViewModel()
         : this(new ImpresiondeFacturaStt(),eAccionSR.Insertar,true) {
      }
      public FacturaImpresionViewModel(ImpresiondeFacturaStt initModel,eAccionSR initAction, bool FirstTime)
         : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
         DefaultFocusedPropertyName = DetalleProdCompFacturaPropertyName;
            InitFirstTime = FirstTime;
         // Model.ConsecutivoCompania = Mfc.GetInt("Compania");
         LibMessages.Notification.Register<bool>(this, OnParametrosComunesChanged);
      }
      #endregion //Constructores
      #region Metodos Generados

      protected override void InitializeCommands() {
         ChooseTemplateCommand = new RelayCommand(ExecuteBuscarPlantillaCommand);
      }

      protected override void InitializeLookAndFeel(ImpresiondeFacturaStt valModel) {
         base.InitializeLookAndFeel(valModel);
      }

      protected override ImpresiondeFacturaStt FindCurrentRecord(ImpresiondeFacturaStt valModel) {
         if(valModel == null) {
            return new ImpresiondeFacturaStt();
         }
         //LibGpParams vParams = new LibGpParams();
         //vParams.AddInString("DetalleProdCompFactura", valModel.DetalleProdCompFactura, 0);
         //return BusinessComponent.GetData(eProcessMessageType.SpName, "FacturaImpresionGET", vParams.Get()).FirstOrDefault();
         return valModel;
      }

      protected override ILibBusinessComponentWithSearch<IList<ImpresiondeFacturaStt>, IList<ImpresiondeFacturaStt>> GetBusinessComponent() {
         return null;
      }

      private void ExecuteBuscarPlantillaCommand() {
         try {
            NombrePlantillaAnexoSeriales = new clsUtilParameters().BuscarNombrePlantilla("rpx de Anexo Seriales (*.rpx)|*Anexo*Seriales*.rpx");
         } catch(System.AccessViolationException) {
            throw;
         } catch(System.Exception vEx) {
            LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
         }
      }

      private ValidationResult NumeroDeDigitosEnFacturaValidating() {
         ValidationResult vResult = ValidationResult.Success;
         if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
            return ValidationResult.Success;
         } else {
            if(Model.NumeroDeDigitosEnFactura < 1 || Model.NumeroDeDigitosEnFactura > 10) {
               vResult = new ValidationResult("El Número De Digitos En Factura, debe estar entre 1 y 10");
            }
         }
         return vResult;
      }

      private void OnParametrosComunesChanged(NotificationMessage<bool> valMessage) {
          try {

              if (LibString.S1IsEqualToS2(LibConvert.ToStr(valMessage.Notification), "EmitirDirecto")) {
                  IsEnabledImprimirBorradorAlInsertarFactura = valMessage.Content;
              } else if (LibString.S1IsEqualToS2(LibConvert.ToStr(valMessage.Notification), "ImprimeDireccionAlFinalDelComprobanteFiscal")) {
                  ImprimeDireccionAlFinalDelComprobanteFiscal = valMessage.Content;
              }

         } catch(System.AccessViolationException) {
            throw;
         } catch(System.Exception vEx) {
            LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
         }
      }

      private ValidationResult NombrePlantillaAnexoSerialesValidating() {
         ValidationResult vResult = ValidationResult.Success;
         if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
            return ValidationResult.Success;
         } else {
            if(ImprimirAnexoDeSerial) {
               if(!LibString.IsNullOrEmpty(NombrePlantillaAnexoSeriales) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaAnexoSeriales)) {
                  vResult = new ValidationResult("El RPX " + NombrePlantillaAnexoSeriales + ", en " + this.ModuleName + ", no EXISTE.");
               } else if(LibString.IsNullOrEmpty(NombrePlantillaAnexoSeriales)) {
                  vResult = new ValidationResult(this.ModuleName + "-> Nombre Plantilla Anexo Seriales es requerido.");
               }
            }
         }
         return vResult;
      }

      bool EsFormatoDeFechaValido(string valFormatoFecha) {
         string valDia = "dd";
         string valMes = "mm";
         string valAno = "yyyy";
         string[] valArray;
         bool valCadenaValida;
         string valTextoFormatoLimpio = LibString.CleanFromCharsInBothSides(valFormatoFecha, " ");
         valArray = valTextoFormatoLimpio.Select(c => c.ToString()).ToArray();
         if(LibString.Len(valFormatoFecha) == 10) {
            string valTextoTemp = valArray[0] + valArray[1];
            if(LibString.S1IsInS2(valDia, valTextoTemp))
               valCadenaValida = true;
            else
               valCadenaValida = false;
            valTextoTemp = valArray[3] + valArray[4];
            if(LibString.S1IsInS2(valMes, valTextoTemp))
               valCadenaValida = true && valCadenaValida;
            else
               valCadenaValida = false && valCadenaValida;
            valTextoTemp = valArray[6] + valArray[7] + valArray[8] + valArray[9];
            if(LibString.S1IsInS2(valAno, valTextoTemp))
               valCadenaValida = true && valCadenaValida;
            else
               valCadenaValida = false && valCadenaValida;
         } else {
            valCadenaValida = false;
         }
         if(valCadenaValida) {
            if((LibString.S1IsInS2("/", valArray[2]) || LibString.S1IsInS2(".", valArray[2]) || LibString.S1IsInS2("-", valArray[2]) || LibString.S1IsInS2("_", valArray[2]))
            && (LibString.S1IsInS2("/", valArray[5]) || LibString.S1IsInS2(".", valArray[5]) || LibString.S1IsInS2("-", valArray[5]) || LibString.S1IsInS2("_", valArray[5])))
               valCadenaValida = true && valCadenaValida;
            else
               valCadenaValida = false && valCadenaValida;
         }
         return valCadenaValida;
      }

      private ValidationResult CantidadDeCopiasDeLaFacturaAlImprimirValidating() {
         ValidationResult vResult = ValidationResult.Success;
         if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
            return ValidationResult.Success;
         } else {
            if(Model.CantidadDeCopiasDeLaFacturaAlImprimir < 1 || Model.CantidadDeCopiasDeLaFacturaAlImprimir > 99) {
               vResult = new ValidationResult("Seccion 2.3=> La Cantidad De Copias De La Factura Al Imprimir, debe estar entre 1 y 99.");
            }
         }
         return vResult;
      }

      public bool isVisibleParaPeru {
          get {
              bool vResult = true;
              if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                  vResult = false;
              }
              return vResult;
          }
      }
	  
      public bool IsEnabledUsaModficarNumeroDigitosEnFactura {
          get {
              if(LibString.IsNullOrEmpty(AppMemoryInfo.GlobalValuesGetString("Parametros","SesionEspecialModificarNumeroDigitosEnFactura"))) {
                  return false;
              } else {
                  return (IsEnabled && InitFirstTime) || (IsEnabled && AppMemoryInfo.GlobalValuesGetBool("Parametros","SesionEspecialModificarNumeroDigitosEnFactura"));
              }
          }
      }
      #endregion //Metodos Generados


   } //End of class FacturaImpresionViewModel

} //End of namespace Galac.Saw.Uil.SttDef

