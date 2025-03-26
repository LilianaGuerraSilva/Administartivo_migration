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
    public class CXCCobranzasClienteViewModel : LibInputViewModelMfc<ClienteStt> {
        #region Constantes
        public const string CodigoGenericoClientePropertyName = "CodigoGenericoCliente";
        public const string BuscarClienteXRifAlFacturarPropertyName = "BuscarClienteXRifAlFacturar";
        public const string AvisoDeClienteConDeudaPropertyName = "AvisoDeClienteConDeuda";
        public const string AvisoDeFacturacionMenorPropertyName = "AvisoDeFacturacionMenor";
        public const string ColocarEnFacturaElVendedorAsinagoAlClientePropertyName = "ColocarEnFacturaElVendedorAsinagoAlCliente";
        public const string LongitudCodigoClientePropertyName = "LongitudCodigoCliente";
        public const string ImprimirDatosClienteEnCompFiscalPropertyName = "ImprimirDatosClienteEnCompFiscal";
        public const string NombreCampoDefinibleCliente1PropertyName = "NombreCampoDefinibleCliente1";
        public const string UsaCodigoClienteEnPantallaPropertyName = "UsaCodigoClienteEnPantalla";
        public const string RellenaCerosAlaIzquierdaPropertyName = "RellenaCerosAlaIzquierda";
        public const string MontoApartirDelCualEnviarAvisoDeudaPropertyName = "MontoApartirDelCualEnviarAvisoDeuda";
        public const string IsEnabledUsaCodigoClienteEnPantallaPropertyName = "IsEnabledUsaCodigoClienteEnPantalla";
        public const string IsEnabledBuscarClienteXRifAlFacturarPropertyName = "IsEnabledBuscarClienteXRifAlFacturar";
        public const string NombreGenericoClientePropertyName = "NombreClienteGenerico";
        public const string IsEnabledMontoApartirDelCualEnviarAvisoDeudaPropertyName = "IsEnabledMontoApartirDelCualEnviarAvisoDeuda";
        #endregion
        #region Variables
        private FkClienteViewModel _ConexionClienteGenerico = null;
        bool mEsFacturadorBasico;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "4.1.- Cliente"; }
        }

        [LibCustomValidation("CodigoGenericoClienteValidating")]
        public string  CodigoGenericoCliente {
            get {
                return Model.CodigoGenericoCliente;
            }
            set {
                if (Model.CodigoGenericoCliente != value) {
                    Model.CodigoGenericoCliente = value;
                    if(LibString.IsNullOrEmpty(Model.CodigoGenericoCliente)) {
                        ConexionClienteGenerico = null;
                    }
                    IsDirty = true;
                    RaisePropertyChanged(CodigoGenericoClientePropertyName);
                    RaisePropertyChanged(NombreGenericoClientePropertyName);
                }
            }
        }

        
        public string NombreClienteGenerico {
            get {
                return (_ConexionClienteGenerico != null ? _ConexionClienteGenerico.Nombre : string.Empty);
            }
        }

        public bool  BuscarClienteXRifAlFacturar {
            get {
                return Model.BuscarClienteXRifAlFacturarAsBool;
            }
            set {
                if (Model.BuscarClienteXRifAlFacturarAsBool != value) {
                    Model.BuscarClienteXRifAlFacturarAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(BuscarClienteXRifAlFacturarPropertyName);
                    RaisePropertyChanged(IsEnabledUsaCodigoClienteEnPantallaPropertyName);
                }
            }
        }

        public bool  AvisoDeClienteConDeuda {
            get {
                return Model.AvisoDeClienteConDeudaAsBool;
            }
            set {
                if (Model.AvisoDeClienteConDeudaAsBool != value) {
                    Model.AvisoDeClienteConDeudaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(AvisoDeClienteConDeudaPropertyName);
                    RaisePropertyChanged(IsEnabledMontoApartirDelCualEnviarAvisoDeudaPropertyName);
                }
            }
        }

        public bool  AvisoDeFacturacionMenor {
            get {
                return Model.AvisoDeFacturacionMenorAsBool;
            }
            set {
                if (Model.AvisoDeFacturacionMenorAsBool != value) {
                    Model.AvisoDeFacturacionMenorAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(AvisoDeFacturacionMenorPropertyName);
                }
            }
        }

        public bool  ColocarEnFacturaElVendedorAsinagoAlCliente {
            get {
                return Model.ColocarEnFacturaElVendedorAsinagoAlClienteAsBool;
            }
            set {
                if (Model.ColocarEnFacturaElVendedorAsinagoAlClienteAsBool != value) {
                    Model.ColocarEnFacturaElVendedorAsinagoAlClienteAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ColocarEnFacturaElVendedorAsinagoAlClientePropertyName);
                }
            }
        }

        public int  LongitudCodigoCliente {
            get {
                return Model.LongitudCodigoCliente;
            }
            set {
                if (Model.LongitudCodigoCliente != value) {
                    Model.LongitudCodigoCliente = value;
                    IsDirty = true;
                    RaisePropertyChanged(LongitudCodigoClientePropertyName);
                }
            }
        }

        public bool  ImprimirDatosClienteEnCompFiscal {
            get {
                return Model.ImprimirDatosClienteEnCompFiscalAsBool;
            }
            set {
                if (Model.ImprimirDatosClienteEnCompFiscalAsBool != value) {
                    Model.ImprimirDatosClienteEnCompFiscalAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimirDatosClienteEnCompFiscalPropertyName);
                }
            }
        }

        public string  NombreCampoDefinibleCliente1 {
            get {
                return Model.NombreCampoDefinibleCliente1;
            }
            set {
                if (Model.NombreCampoDefinibleCliente1 != value) {
                    Model.NombreCampoDefinibleCliente1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinibleCliente1PropertyName);
                }
            }
        }

        public bool  UsaCodigoClienteEnPantalla {
            get {
                return Model.UsaCodigoClienteEnPantallaAsBool;
            }
            set {
                if (Model.UsaCodigoClienteEnPantallaAsBool != value) {
                    Model.UsaCodigoClienteEnPantallaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaCodigoClienteEnPantallaPropertyName);
                    RaisePropertyChanged(IsEnabledBuscarClienteXRifAlFacturarPropertyName);
                }
            }
        }

        public bool  RellenaCerosAlaIzquierda {
            get {
                return Model.RellenaCerosAlaIzquierdaAsBool;
            }
            set {
                if (Model.RellenaCerosAlaIzquierdaAsBool != value) {
                    Model.RellenaCerosAlaIzquierdaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(RellenaCerosAlaIzquierdaPropertyName);
                }
            }
        }

        private ValidationResult MontoApartirDelCualEnviarAvisoDeudaValidating() {
           ValidationResult vResult = ValidationResult.Success;
           if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
              return ValidationResult.Success;
           } else {
              if(MontoApartirDelCualEnviarAvisoDeuda < 0) {
                 vResult = new ValidationResult("Sección 4.1, El  Monto a Partir del Cual Enviar Aviso Deuda; debe ser un monto mayor o igual a 0.");
              }
           }
           return vResult;
        }

       [LibCustomValidation("MontoApartirDelCualEnviarAvisoDeudaValidating")]
        public decimal  MontoApartirDelCualEnviarAvisoDeuda {
            get {
                return Model.MontoApartirDelCualEnviarAvisoDeuda;
            }
            set {
                if (Model.MontoApartirDelCualEnviarAvisoDeuda != value) {
                    Model.MontoApartirDelCualEnviarAvisoDeuda = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoApartirDelCualEnviarAvisoDeudaPropertyName);
                }
            }
        }

        public bool IsEnabledBuscarClienteXRifAlFacturar {
            get {
                return (IsEnabled && !UsaCodigoClienteEnPantalla ? true : false);
            }
        }

        public bool IsEnabledUsaCodigoClienteEnPantalla {
            get {
                return (IsEnabled && !BuscarClienteXRifAlFacturar ? true : false);
            }
        }

        public FkClienteViewModel ConexionClienteGenerico {
            get {
                return _ConexionClienteGenerico;
            }
            set {
                if (_ConexionClienteGenerico != value) {
                    _ConexionClienteGenerico = value;
                    if(value != null) {
                        CodigoGenericoCliente = _ConexionClienteGenerico.Codigo;
                    }
                }
                if (_ConexionClienteGenerico == null) {
                    CodigoGenericoCliente = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseClienteGenericoCommand {
            get;
            private set;
        }

        public bool IsVisibleLongitudCodigoCliente {
            get {
                return Action != eAccionSR.Insertar;
            }
        }

        public bool IsEnabledMontoApartirDelCualEnviarAvisoDeuda {
            get {
                return IsEnabled && AvisoDeClienteConDeuda;
            }
        }

        public bool IsVisibleAvisoDeClienteConDeuda {
            get {
                return !mEsFacturadorBasico;
            }
        }

        public string PromptRIF {
           get {
              return string.Format("Usar el {0} del Cliente al Realizar una Búsqueda............................", AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptRIF"));
           }
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

        #endregion //Propiedades
        #region Constructores
        public CXCCobranzasClienteViewModel()
            : this(new ClienteStt(), eAccionSR.Insertar) {
        }
        public CXCCobranzasClienteViewModel(ClienteStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = CodigoGenericoClientePropertyName;
            mEsFacturadorBasico = new clsLibSaw().EsFacturadorBasico();
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(ClienteStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ClienteStt FindCurrentRecord(ClienteStt valModel) {
            if (valModel == null) {
                return new ClienteStt();
            }
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<ClienteStt>, IList<ClienteStt>> GetBusinessComponent() {
            return null;
        }
        
        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseClienteGenericoCommand = new RelayCommand<string>(ExecuteChooseClienteGenericoCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionClienteGenerico = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkClienteViewModel>("Cliente", LibSearchCriteria.CreateCriteria("codigo", CodigoGenericoCliente), new clsSettValueByCompanyNav());
        }

        private void ExecuteChooseClienteGenericoCommand(string valcodigo) {
            try {
                if (valcodigo == null) {
                    valcodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Cliente_B1.Codigo", valcodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_Cliente_B1.ConsecutivoCompania", LibConvert.ToStr(Mfc.GetInt("Compania")));
                ConexionClienteGenerico = null;
                ConexionClienteGenerico = LibFKRetrievalHelper.ChooseRecord<FkClienteViewModel>("Cliente", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }


        private ValidationResult CodigoGenericoClienteValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if(LibString.IsNullOrEmpty(CodigoGenericoCliente)) {
                    vResult = new ValidationResult(this.ModuleName + "-> Cliente Génerico es requerido.");
                }
            }
            return vResult;
        }

        #endregion //Metodos Generados

    } //End of class CXCCobranzasClienteViewModel

} //End of namespace Galac.Saw.Uil.SttDef

