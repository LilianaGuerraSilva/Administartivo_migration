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
using Galac.Saw.Lib;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class DatosGeneralesViewModel : LibInputViewModelMfc<GeneralStt> {
        #region Constantes
        public const string UsaMultiplesAlicuotasPropertyName = "UsaMultiplesAlicuotas";
        public const string ValidarRifEnLaWebPropertyName = "ValidarRifEnLaWeb";
        public const string PermitirEditarIVAenCxC_CxPPropertyName = "PermitirEditarIVAenCxCCxP";
        public const string OrdenamientoDeCodigoStringPropertyName = "OrdenamientoDeCodigoString";
        public const string ImprimirComprobanteDeCxCPropertyName = "ImprimirComprobanteDeCxC";
        public const string ImprimirComprobanteDeCxPPropertyName = "ImprimirComprobanteDeCxP";
        public const string EsSistemaParaIGPropertyName = "EsSistemaParaIG";
        public const string UsaNotaEntregaPropertyName = "UsaNotaEntrega";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "1.2.- General"; }
        }

        public bool  UsaMultiplesAlicuotas {
            get {
                return Model.UsaMultiplesAlicuotasAsBool;
            }
            set {
                if (Model.UsaMultiplesAlicuotasAsBool != value) {
                    Model.UsaMultiplesAlicuotasAsBool = value;
                    if(Model.UsaMultiplesAlicuotasAsBool) {
                        LibMessages.MessageBox.Information(this, "Recuerde seleccionar en factura y cotización los modelos adecuados al manejo de 3 alícuotas para la impresión de sus documentos.",string.Empty);
                    }
                    IsDirty = true;
                    RaisePropertyChanged(UsaMultiplesAlicuotasPropertyName);
                }
            }
        }

        public bool  ValidarRifEnLaWeb {
            get {
                return Model.ValidarRifEnLaWebAsBool;
            }
            set {
                if (Model.ValidarRifEnLaWebAsBool != value) {
                    Model.ValidarRifEnLaWebAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ValidarRifEnLaWebPropertyName);
                }
            }
        }

        public bool PermitirEditarIVAenCxCCxP {
            get {
                return Model.PermitirEditarIVAenCxC_CxPAsBool;
            }
            set {
                if (Model.PermitirEditarIVAenCxC_CxPAsBool != value) {
                    Model.PermitirEditarIVAenCxC_CxPAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(PermitirEditarIVAenCxC_CxPPropertyName);
                }
            }
        }

        public eFormaDeOrdenarCodigos  OrdenamientoDeCodigoString {
            get {
                return Model.OrdenamientoDeCodigoStringAsEnum;
            }
            set {
                if (Model.OrdenamientoDeCodigoStringAsEnum != value) {
                    Model.OrdenamientoDeCodigoStringAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(OrdenamientoDeCodigoStringPropertyName);
                }
            }
        }

        public bool  ImprimirComprobanteDeCxC {
            get {
                return Model.ImprimirComprobanteDeCxCAsBool;
            }
            set {
                if (Model.ImprimirComprobanteDeCxCAsBool != value) {
                    Model.ImprimirComprobanteDeCxCAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimirComprobanteDeCxCPropertyName);
                }
            }
        }

        public bool  ImprimirComprobanteDeCxP {
            get {
                return Model.ImprimirComprobanteDeCxPAsBool;
            }
            set {
                if (Model.ImprimirComprobanteDeCxPAsBool != value) {
                    Model.ImprimirComprobanteDeCxPAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimirComprobanteDeCxPPropertyName);
                }
            }
        }

        public bool  EsSistemaParaIG {
            get {
                return Model.EsSistemaParaIGAsBool;
            }
            set {
                if (Model.EsSistemaParaIGAsBool != value) {
                    Model.EsSistemaParaIGAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(EsSistemaParaIGPropertyName);
                }
            }
        }

        public eFormaDeOrdenarCodigos[] ArrayFormaDeOrdenarCodigosString {
            get {
                return LibEnumHelper<eFormaDeOrdenarCodigos>.GetValuesInArray();
            }
        }
 
        public bool IsVisibleNotaEntrega {
            get {
                if (LibString.IsNullOrEmpty(AppMemoryInfo.GlobalValuesGetString("Parametros", "EsPilotoNotaEntrega"))|| mEsFacturadorBasico) {
                    return false;
                } else {
                     return AppMemoryInfo.GlobalValuesGetBool("Parametros", "EsPilotoNotaEntrega");
                }
            }
        }

        public bool IsVisibleEditarIVAenCxCCxP {
            get {
                return !mEsFacturadorBasico;
            }
        }
        public bool IsVisibleImprimirComprobanteCxC {
            get {
                return !mEsFacturadorBasico;
            }
        }
        public bool IsVisibleImprimirComprobanteDeCxP {
            get {
                return !mEsFacturadorBasico;
            }
        }
        public string PromptIVA {
            get {
                return string.Format("Permitir Editar {0} en CxC y CxP..........................................................", AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA"));
            }
        }

        public bool UsaNotaEntrega {
            get {
                return Model.UsaNotaEntregaAsBool;
            }
            set {
                if(Model.UsaNotaEntregaAsBool != value) {
                    Model.UsaNotaEntregaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaNotaEntregaPropertyName);
                }
            }
        }


        public bool isVisibleRifEnLaWeb{
            get { return !mEsFacturadorBasico;
            }
        }
        public string CaptionValidarRifEnLaWeb {
           get {
              return string.Format("Validar {0} en la Web..............................................................................", AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptRIF"));
           }
        }

        public bool IsEnabledUsaNotaEntrega {
            get { return !UsaImprentaDigital(); }
        }
        #endregion //Propiedades
        #region Constructores
        public DatosGeneralesViewModel()
            : this(new GeneralStt(), eAccionSR.Insertar) {
        }
        public DatosGeneralesViewModel(GeneralStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = UsaMultiplesAlicuotasPropertyName;
            mEsFacturadorBasico = new clsLibSaw().EsFacturadorBasico();
        }
        #endregion //Constructores

        #region Variables
        bool mEsFacturadorBasico;
        #endregion

        #region Metodos Generados

        protected override void InitializeLookAndFeel(GeneralStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override GeneralStt FindCurrentRecord(GeneralStt valModel) {
            if (valModel == null) {
                return new GeneralStt();
            }
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<GeneralStt>, IList<GeneralStt>> GetBusinessComponent() {
            return null;
        }

        private bool UsaImprentaDigital() {
            return LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaImprentaDigital"));
        }

        #endregion //Metodos Generados


    } //End of class DatosGeneralesViewModel

} //End of namespace Galac.Saw.Uil.SttDef

