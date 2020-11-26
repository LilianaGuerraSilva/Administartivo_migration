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
    public class CotizacionViewModel : LibInputViewModelMfc<CotizacionStt> {
        #region Constantes
        public const string ValidarArticulosAlGenerarFacturaPropertyName = "ValidarArticulosAlGenerarFactura";
        public const string NombrePlantillaCotizacionPropertyName = "NombrePlantillaCotizacion";
        public const string UsaControlDespachoPropertyName = "UsaControlDespacho";
        public const string LimpiezaDeCotizacionXFacturaPropertyName = "LimpiezaDeCotizacionXFactura";
        public const string DetalleProdCompCotizacionPropertyName = "DetalleProdCompCotizacion";
        public const string CampoCodigoAlternativoDeArticuloPropertyName = "CampoCodigoAlternativoDeArticulo";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "3.1.- Cotización"; }
        }

        public bool  ValidarArticulosAlGenerarFactura {
            get {
                return Model.ValidarArticulosAlGenerarFacturaAsBool;
            }
            set {
                if (Model.ValidarArticulosAlGenerarFacturaAsBool != value) {
                    Model.ValidarArticulosAlGenerarFacturaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ValidarArticulosAlGenerarFacturaPropertyName);
                }
            }
        }

        [LibCustomValidation("NombrePlantillaCotizacionValidating")]
        public string  NombrePlantillaCotizacion {
            get {
                return Model.NombrePlantillaCotizacion;
            }
            set {
                if (Model.NombrePlantillaCotizacion != value) {
                    Model.NombrePlantillaCotizacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombrePlantillaCotizacionPropertyName);
                }
            }
        }

        public bool  UsaControlDespacho {
            get {
                return Model.UsaControlDespachoAsBool;
            }
            set {
                if (Model.UsaControlDespachoAsBool != value) {
                    Model.UsaControlDespachoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaControlDespachoPropertyName);
                }
            }
        }

        public bool  LimpiezaDeCotizacionXFactura {
            get {
                return Model.LimpiezaDeCotizacionXFacturaAsBool;
            }
            set {
                if (Model.LimpiezaDeCotizacionXFacturaAsBool != value) {
                    Model.LimpiezaDeCotizacionXFacturaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(LimpiezaDeCotizacionXFacturaPropertyName);
                }
            }
        }

        public bool  DetalleProdCompCotizacion {
            get {
                return Model.DetalleProdCompCotizacionAsBool;
            }
            set {
                if (Model.DetalleProdCompCotizacionAsBool != value) {
                    Model.DetalleProdCompCotizacionAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(DetalleProdCompCotizacionPropertyName);
                }
            }
        }

        public eCampoCodigoAlternativoDeArticulo  CampoCodigoAlternativoDeArticulo {
            get {
                return Model.CampoCodigoAlternativoDeArticuloAsEnum;
            }
            set {
                if (Model.CampoCodigoAlternativoDeArticuloAsEnum != value) {
                    Model.CampoCodigoAlternativoDeArticuloAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(CampoCodigoAlternativoDeArticuloPropertyName);
                }
            }
        }

        public eCampoCodigoAlternativoDeArticulo[] ArrayCampoCodigoAlternativoDeArticulo {
            get {
                return LibEnumHelper<eCampoCodigoAlternativoDeArticulo>.GetValuesInArray();
            }
        }

        public RelayCommand ChooseTemplateCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public CotizacionViewModel()
            : this(new CotizacionStt(), eAccionSR.Insertar) {
        }
        public CotizacionViewModel(CotizacionStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = ValidarArticulosAlGenerarFacturaPropertyName;
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeCommands() {
            ChooseTemplateCommand = new RelayCommand(ExecuteBuscarPlantillaCommand);
        }

        protected override void InitializeLookAndFeel(CotizacionStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override CotizacionStt FindCurrentRecord(CotizacionStt valModel) {
            if (valModel == null) {
                return new CotizacionStt();
            }
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInString("ValidarArticulosAlGenerarFactura", valModel.ValidarArticulosAlGenerarFactura, 0);
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "CotizacionGET", vParams.Get()).FirstOrDefault();
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<CotizacionStt>, IList<CotizacionStt>> GetBusinessComponent() {
            return null;
        }

        private void ExecuteBuscarPlantillaCommand() {
            try {
                NombrePlantillaCotizacion = new clsUtilParameters().BuscarNombrePlantilla("rpx de Cotización (*.rpx)|*Cotizacion*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult NombrePlantillaCotizacionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(!LibString.IsNullOrEmpty(NombrePlantillaCotizacion) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaCotizacion)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaCotizacion + ", en " + this.ModuleName + ", no EXISTE.");
                } else if(LibString.IsNullOrEmpty(NombrePlantillaCotizacion)) {
                    vResult = new ValidationResult(this.ModuleName + "-> Nombre Plantilla Cotización es requerido.");
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class CotizacionViewModel

} //End of namespace Galac.Saw.Uil.SttDef

