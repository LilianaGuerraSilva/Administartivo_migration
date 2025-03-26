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

using System.Windows.Forms ;
using Galac.Saw.Lib;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class CxPComprasRetIVAViewModel : LibInputViewModelMfc<RetencionIVAStt> {
        #region Constantes
        public const string EnDondeRetenerIVAPropertyName = "EnDondeRetenerIVA";
        public const string ImprimirComprobanteDeRetIvaPropertyName = "ImprimirComprobanteDeRetIva";
        public const string FormaDeReiniciarElNumeroDeComprobanteRetIvaPropertyName = "FormaDeReiniciarElNumeroDeComprobanteRetIva";
        public const string GenerarNumCompDeRetIvasoloSiPorcentajeEsMayorAceroPropertyName = "GenerarNumCompDeRetIvasoloSiPorcentajeEsMayorAcero";
        public const string UsaMismoNumeroCompRetTodasCxPPropertyName = "UsaMismoNumeroCompRetTodasCxP";
        public const string UnComprobanteDeRetIvaporHojaPropertyName = "UnComprobanteDeRetIvaporHoja";
        public const string PrimerNumeroComprobanteRetIvaPropertyName = "PrimerNumeroComprobanteRetIva";
        public const string NumeroDeCopiasComprobanteRetencionIvaPropertyName = "NumeroDeCopiasComprobanteRetencionIva";
        public const string NombrePlantillaComprobanteDeRetIVAPropertyName = "NombrePlantillaComprobanteDeRetIVA";
        public const string IsVisibleUsaMismoNumeroCompRetTodasCxPPropertyName = "IsVisibleUsaMismoNumeroCompRetTodasCxP";
        public const string ArrayDondeSeEfectuaLaRetencionIVAPropertyName = "ArrayDondeSeEfectuaLaRetencionIVA";



       
            
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "6.3.- Retención IVA"; }
        }

        [LibCustomValidation("EnDondeRetenerIVAValidating")]
        public eDondeSeEfectuaLaRetencionIVA EnDondeRetenerIVA {
            get {
                return Model.EnDondeRetenerIVAAsEnum;
            }
            set {
                if (Model.EnDondeRetenerIVAAsEnum != value) {
                    Model.EnDondeRetenerIVAAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(EnDondeRetenerIVAPropertyName);
                   
                    RaisePropertyChanged(IsVisibleUsaMismoNumeroCompRetTodasCxPPropertyName);
                    if (EnDondeRetenerIVA != eDondeSeEfectuaLaRetencionIVA.Pago) {
                        UsaMismoNumeroCompRetTodasCxP = false;
                    }
                    OnRetenerIvaChanged();
                }
            }
        }       

        public bool  ImprimirComprobanteDeRetIva {
            get {
                return Model.ImprimirComprobanteDeRetIVAAsBool;
            }
            set {
                if (Model.ImprimirComprobanteDeRetIVAAsBool != value) {
                    Model.ImprimirComprobanteDeRetIVAAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimirComprobanteDeRetIvaPropertyName);
                }
            }
        }

        [LibCustomValidation("FormaDeReiniciarElNumeroDeComprobanteRetIvaValidating")]
        public eFormaDeReiniciarComprobanteRetIVA  FormaDeReiniciarElNumeroDeComprobanteRetIva {
            get {
                return Model.FormaDeReiniciarElNumeroDeComprobanteRetIVAAsEnum;
            }
            set {
                if (Model.FormaDeReiniciarElNumeroDeComprobanteRetIVAAsEnum != value) {
                    Model.FormaDeReiniciarElNumeroDeComprobanteRetIVAAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(FormaDeReiniciarElNumeroDeComprobanteRetIvaPropertyName);
                }
            }
        }

        public bool  GenerarNumCompDeRetIvasoloSiPorcentajeEsMayorAcero {
            get {
                return Model.GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACeroAsBool;
            }
            set {
                if (Model.GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACeroAsBool != value) {
                    Model.GenerarNumCompDeRetIVASoloSiPorcentajeEsMayorACeroAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(GenerarNumCompDeRetIvasoloSiPorcentajeEsMayorAceroPropertyName);
                }
            }
        }

        public bool  UsaMismoNumeroCompRetTodasCxP {
            get {
                return Model.UsaMismoNumeroCompRetTodasCxPAsBool;
            }
            set {
                if (Model.UsaMismoNumeroCompRetTodasCxPAsBool != value) {
                    Model.UsaMismoNumeroCompRetTodasCxPAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaMismoNumeroCompRetTodasCxPPropertyName);
                }
            }
        }

        public bool  UnComprobanteDeRetIvaporHoja {
            get {
                return Model.UnComprobanteDeRetIVAPorHojaAsBool;
            }
            set {
                if (Model.UnComprobanteDeRetIVAPorHojaAsBool != value) {
                    Model.UnComprobanteDeRetIVAPorHojaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UnComprobanteDeRetIvaporHojaPropertyName);
                }
            }
        }

        [LibCustomValidation("PrimerNumeroComprobanteRetIvaValidating")]
        public int  PrimerNumeroComprobanteRetIva {
            get {
                return Model.PrimerNumeroComprobanteRetIVA;
            }
            set {
                if (Model.PrimerNumeroComprobanteRetIVA != value) {
                    Model.PrimerNumeroComprobanteRetIVA = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrimerNumeroComprobanteRetIvaPropertyName);
                }
            }
        }

        [LibCustomValidation("NumeroDeCopiasComprobanteRetencionIvaValidating")]
        public int  NumeroDeCopiasComprobanteRetencionIva {
            get {
                return Model.NumeroDeCopiasComprobanteRetencionIVA;
            }
            set {
                if (Model.NumeroDeCopiasComprobanteRetencionIVA != value) {
                    Model.NumeroDeCopiasComprobanteRetencionIVA = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroDeCopiasComprobanteRetencionIvaPropertyName);
                }
            }
        }

        [LibCustomValidation("NombrePlantillaComprobanteDeRetIVAValidating")]
        public string  NombrePlantillaComprobanteDeRetIVA {
            get {
                return Model.NombrePlantillaComprobanteDeRetIVA;
            }
            set {
                if (Model.NombrePlantillaComprobanteDeRetIVA != value) {
                    Model.NombrePlantillaComprobanteDeRetIVA = value;
                    IsDirty = true;
                    if (LibString.IsNullOrEmpty(NombrePlantillaComprobanteDeRetIVA)) {
                        ExecuteBuscarPlantillaCommand();
                    }
                    RaisePropertyChanged(NombrePlantillaComprobanteDeRetIVAPropertyName);
                }
            }
        }

        public eDondeSeEfectuaLaRetencionIVA[] ArrayDondeSeEfectuaLaRetencionIVA {
               
            get {
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsPrimeraVezCompania")) {
                    return new eDondeSeEfectuaLaRetencionIVA[] { eDondeSeEfectuaLaRetencionIVA.NoRetenida, eDondeSeEfectuaLaRetencionIVA.CxP };
                }
                else {
                    eDondeSeEfectuaLaRetencionIVA[] ArregloRetencion;
                    if (EnDondeRetenerIVA == eDondeSeEfectuaLaRetencionIVA.CxP) {
                        ArregloRetencion = new eDondeSeEfectuaLaRetencionIVA[] { eDondeSeEfectuaLaRetencionIVA.NoRetenida, eDondeSeEfectuaLaRetencionIVA.CxP };
                    } else if (EnDondeRetenerIVA == eDondeSeEfectuaLaRetencionIVA.Pago) {
                        ArregloRetencion =  new eDondeSeEfectuaLaRetencionIVA[] { eDondeSeEfectuaLaRetencionIVA.NoRetenida, eDondeSeEfectuaLaRetencionIVA.CxP, eDondeSeEfectuaLaRetencionIVA.Pago };
                    } else {
					   ArregloRetencion = new eDondeSeEfectuaLaRetencionIVA[] { eDondeSeEfectuaLaRetencionIVA.NoRetenida, eDondeSeEfectuaLaRetencionIVA.CxP };
                    }
                    return ArregloRetencion;
                    }
            }
        }

        public eFormaDeReiniciarComprobanteRetIVA[] ArrayFormaDeReiniciarComprobanteRetIVA {
            get {
                if ((FormaDeReiniciarElNumeroDeComprobanteRetIva != eFormaDeReiniciarComprobanteRetIVA.PorMes ) ||
                   (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsPrimeraVezCompania"))) {
                    return new eFormaDeReiniciarComprobanteRetIVA[] { eFormaDeReiniciarComprobanteRetIVA.SinEscoger, eFormaDeReiniciarComprobanteRetIVA.PorAno, eFormaDeReiniciarComprobanteRetIVA.AlCompletar  };
                    } else {
                        return LibEnumHelper<eFormaDeReiniciarComprobanteRetIVA>.GetValuesInArray();
                }
              
            }
        }

        public RelayCommand ChooseTemplateCommand {
            get;
            private set;
        }
        
        public bool IsVisibleUsaMismoNumeroCompRetTodasCxP {
            get { 
                return (EnDondeRetenerIVA==eDondeSeEfectuaLaRetencionIVA.Pago);
            }
        }

        public bool IsEnabledDatosRetIva {
            get {
     
                return IsEnabled && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "PuedoUsarOpcionesDeContribuyenteEspecial");
            }
        }

        public string CaptionImprimirComprobanteDeRetIVA {
            get {
                return "Primer Número de Comprobante de Retención de " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PromptIVA") + " ...";
            }
        }

        public string CaptionEnDondeRetenerIVA {
           get {
              return string.Format("En Donde Retener {0}.......................................................................", AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA"));
           }
        }

        public string CaptionFormaDeReiniciarElNumeroDeComprobanteRetIva {
           get {
              return string.Format("Forma de Reiniciar el Número de Comprobante Ret {0}..................", AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA"));
           }
        }

        public string CaptionImprimirComprobanteDeRetIva {
           get {
              return string.Format("Imprimir Comprobante De Ret {0}......................................................", AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA"));
           }
        }
        public string CaptionNumeroDeCopiasComprobanteRetencionIva {
           get {
              return string.Format("Número de Copias Comprobante Retención {0}...............................", AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA"));
           }
        }

       public string CaptionComprobanteDeRetIvaporHoja {
           get {
              return string.Format("Un Comprobante de Ret {0} por Hoja...............................................", AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA"));
           }
        }
       
        public string CaptionNombrePlantillaComprobanteDeRetIVA {
           get {
              return string.Format("Nombre Plantilla Comprobante de Ret {0}........................................", AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA"));
           }
        }
       
        public string CaptionGenerarNumCompDeRetIvasoloSiPorcentajeEsMayorAcero {
           get {
              return string.Format("Generar NumComp de Ret {0} solo Si Porcentaje es Mayor A Cero", AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA"));
           }
        }

        #endregion //Propiedades
        #region Constructores
        public CxPComprasRetIVAViewModel()
            : this(new RetencionIVAStt(), eAccionSR.Insertar) {
        }
        #region  Variables
        bool mEsFacturadorBasico;
        #endregion

        public CxPComprasRetIVAViewModel(RetencionIVAStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = EnDondeRetenerIVAPropertyName;
            mEsFacturadorBasico = new clsLibSaw().EsVersionFacturadorBasico();
            // Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeCommands() {
            ChooseTemplateCommand = new RelayCommand(ExecuteBuscarPlantillaCommand);
        }

        protected override void InitializeLookAndFeel(RetencionIVAStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override RetencionIVAStt FindCurrentRecord(RetencionIVAStt valModel) {
            if (valModel == null) {
                return new RetencionIVAStt();
            }
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInEnum("EnDondeRetenerIVA", LibConvert.EnumToDbValue((int)valModel.EnDondeRetenerIVAAsEnum));
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "CxPComprasRetIVAGET", vParams.Get()).FirstOrDefault();
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<RetencionIVAStt>, IList<RetencionIVAStt>> GetBusinessComponent() {
            return null;
        }

        private void ExecuteBuscarPlantillaCommand() {
            try {
                NombrePlantillaComprobanteDeRetIVA = new clsUtilParameters().BuscarNombrePlantilla("rpx de Retención IVA (*.rpx)|*Retencion*IVA*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult PrimerNumeroComprobanteRetIvaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "PuedoUsarOpcionesDeContribuyenteEspecial")) {
                    if (PrimerNumeroComprobanteRetIva < 1) {
                        vResult = new ValidationResult("El campo " + this.ModuleName + "-> Primer Número de Comprobante de Retención de " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PromptIVA") + " debe ser Mayor a 0");                        
                    }
                }               
            }
            return vResult;
        }

        private ValidationResult NumeroDeCopiasComprobanteRetencionIvaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(NumeroDeCopiasComprobanteRetencionIva == 0) {
                    vResult = new ValidationResult("El campo " + ModuleName + "-> Número de copias debe ser Mayor a 0");                                            
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados

        private ValidationResult EnDondeRetenerIVAValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "PuedoUsarOpcionesDeContribuyenteEspecial")) {
                    if (EnDondeRetenerIVA == eDondeSeEfectuaLaRetencionIVA.NoRetenida) {
                        vResult = new ValidationResult(ModuleName + "-> Debe seleccionar dónde efectuar la Retención del " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PromptIVA"));
                    }
                } else {
                    EnDondeRetenerIVA = eDondeSeEfectuaLaRetencionIVA.NoRetenida;
                    return ValidationResult.Success;
                }
            }
            return vResult;
        }

        private bool SonIgualesContabilizacionYAplicacionDeRetIVA() {
            bool vResult = true;
            vResult = (EnDondeRetenerIVA == eDondeSeEfectuaLaRetencionIVA.CxP && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "DondeContabilizarRet") == 1)
                || (EnDondeRetenerIVA == eDondeSeEfectuaLaRetencionIVA.Pago && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "DondeContabilizarRet") == 2);
            return vResult;

        }

        private ValidationResult FormaDeReiniciarElNumeroDeComprobanteRetIvaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "PuedoUsarOpcionesDeContribuyenteEspecial")) {
                    if (FormaDeReiniciarElNumeroDeComprobanteRetIva  == eFormaDeReiniciarComprobanteRetIVA.SinEscoger) {
                        vResult = new ValidationResult(ModuleName + "-> Debe Seleccionar una Forma de Reiniciar el Número de Comprobante de Retención del " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PromptIVA"));
                    }
                }       
            }
            return vResult;
        }

        private ValidationResult NombrePlantillaComprobanteDeRetIVAValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (IsEnabledDatosRetIva && LibString.IsNullOrEmpty(NombrePlantillaComprobanteDeRetIVA)) {
                    vResult = new ValidationResult("El campo " + ModuleName +"-> planilla de impresión, es requerido.");
                }else if (IsEnabledDatosRetIva && !LibString.IsNullOrEmpty(NombrePlantillaComprobanteDeRetIVA) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaComprobanteDeRetIVA)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaComprobanteDeRetIVA + ", en " + ModuleName + ", no EXISTE.");
                }
            }
            return vResult;
        }

        private void OnRetenerIvaChanged() {
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaModuloDeContabilidad") && !SonIgualesContabilizacionYAplicacionDeRetIVA()) {
                StringBuilder vMessageBuilder = new StringBuilder();
                eDondeSeEfectuaLaRetencionIVA vDondeSeEfectuaLaRetencionIVA;
                vDondeSeEfectuaLaRetencionIVA = (eDondeSeEfectuaLaRetencionIVA)LibConvert.DbValueToEnum(LibConvert.ToStr(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "DondeContabilizarRet")));
                string MsgDondeContabilizarRetIVA = LibEnumHelper.GetDescription(vDondeSeEfectuaLaRetencionIVA, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Parametros", "DondeContabilizarRet"));
                vMessageBuilder.AppendLine("El momento en el cual se efectúa la Retención de " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PromptIVA"));
                vMessageBuilder.AppendLine("(Parámetros Administrativos -> Efectuar la Retención del " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PromptIVA") + " en: " + (EnDondeRetenerIVA).ToString() + ")");
                vMessageBuilder.AppendLine("es diferente al momento en el cual se está contabilizando dicha retención");
                vMessageBuilder.AppendLine("(Reglas de Contabilización -> Contabilizar la Retención de " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "PromptIVA") + " en: " + MsgDondeContabilizarRetIVA + ").");
                LibMessages.MessageBox.Information(this, vMessageBuilder.ToString(), ModuleName);
            }
        }

        public bool IsVisibleRetencionIVA {
            get {
                return !mEsFacturadorBasico;
            }
        }


    } //End of class CxPComprasRetIVAViewModel

} //End of namespace Galac.Saw.Uil.SttDef

