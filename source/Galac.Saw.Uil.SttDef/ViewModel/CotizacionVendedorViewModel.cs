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

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class CotizacionVendedorViewModel : LibInputViewModelMfc<VendedorStt> {
        #region Constantes
        public const string UsaCodigoVendedorEnPantallaPropertyName = "UsaCodigoVendedorEnPantalla";
        public const string LongitudCodigoVendedorPropertyName = "LongitudCodigoVendedor";
        public const string CodigoGenericoVendedorPropertyName = "CodigoGenericoVendedor";
        public const string NombreGenericoVendedorPropertyName = "NombreGenericoVendedor";
        #endregion
        #region Variables
        private FkVendedorViewModel _ConexionVendedorGenerico = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "3.3.- Vendedor"; }
        }

        public bool  UsaCodigoVendedorEnPantalla {
            get {
                return Model.UsaCodigoVendedorEnPantallaAsBool;
            }
            set {
                if (Model.UsaCodigoVendedorEnPantallaAsBool != value) {
                    Model.UsaCodigoVendedorEnPantallaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaCodigoVendedorEnPantallaPropertyName);
                }
            }
        }

        [LibCustomValidation("LongitudCodigoVendedorValidating")]
        public int  LongitudCodigoVendedor {
            get {
                return Model.LongitudCodigoVendedor;
            }
            set {
                if (Model.LongitudCodigoVendedor != value) {
                    Model.LongitudCodigoVendedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(LongitudCodigoVendedorPropertyName);
                }
            }
        }

        [LibCustomValidation("CodigoGenericoVendedorValidating")]
       [LibRequired]
        public string  CodigoGenericoVendedor {
            get {
                return Model.CodigoGenericoVendedor;
            }
            set {
                if (Model.CodigoGenericoVendedor != value) {
                    Model.CodigoGenericoVendedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoGenericoVendedorPropertyName);
                    RaisePropertyChanged(NombreGenericoVendedorPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoGenericoVendedor, true)) {
                        ConexionVendedorGenerico = null;
                    }
                }
            }
        }

        public string NombreGenericoVendedor {
            get {
                return _ConexionVendedorGenerico.Nombre;
            }
        }

        public FkVendedorViewModel ConexionVendedorGenerico {
            get {
                return _ConexionVendedorGenerico;
            }
            set {
                if (_ConexionVendedorGenerico != value) {
                    _ConexionVendedorGenerico = value;
                    if(value != null) {
                        CodigoGenericoVendedor = _ConexionVendedorGenerico.Codigo;
                    }
                }
                if (_ConexionVendedorGenerico == null) {
                    CodigoGenericoVendedor = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseVendedorGenericoCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public CotizacionVendedorViewModel()
            : this(new VendedorStt(), eAccionSR.Insertar) {
        }
        public CotizacionVendedorViewModel(VendedorStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = UsaCodigoVendedorEnPantallaPropertyName;
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(VendedorStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override VendedorStt FindCurrentRecord(VendedorStt valModel) {
            if (valModel == null) {
                return new VendedorStt();
            }
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<VendedorStt>, IList<VendedorStt>> GetBusinessComponent() {
            return null;
        }
        
        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseVendedorGenericoCommand = new RelayCommand<string>(ExecuteChooseVendedorGenericoCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionVendedorGenerico = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkVendedorViewModel>("Vendedor", LibSearchCriteria.CreateCriteria("Codigo", CodigoGenericoVendedor),new clsSettValueByCompanyNav());
        }

        private void ExecuteChooseVendedorGenericoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Vendedor_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_Vendedor_B1.ConsecutivoCompania", LibConvert.ToStr(Mfc.GetInt("Compania")));                
                ConexionVendedorGenerico = LibFKRetrievalHelper.ChooseRecord<FkVendedorViewModel>("Vendedor", vDefaultCriteria, vFixedCriteria, string.Empty);
                if(ConexionVendedorGenerico != null) {
                    CodigoGenericoVendedor = ConexionVendedorGenerico.Codigo;
                } else {
                    CodigoGenericoVendedor = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult LongitudCodigoVendedorValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {                 
                if(LibText.Len(LongitudCodigoVendedor.ToString()) == 0) {
                    vResult = new ValidationResult(this.ModuleName + "-> La longitud del Código del Vendedor no puede ser igual a cero. ");
                } else if(LongitudCodigoVendedor < 1) {
                    vResult = new ValidationResult(this.ModuleName + "-> La longitud del Código del Vendedor no puede ser igual a cero.");
                } else if(LongitudCodigoVendedor > 5) {
                    vResult = new ValidationResult(this.ModuleName + "-> La longitud del Código del Vendedor no puede ser mayor a 5.");
                }
            }
            return vResult;
        }


        private ValidationResult CodigoGenericoVendedorValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {                 
                if(LibString.IsNullOrEmpty(CodigoGenericoVendedor)) {
                    vResult = new ValidationResult(this.ModuleName + "-> Vendedor Genérico es requerido");
                } else if(LibText.Len(CodigoGenericoVendedor) > 5) {
                    vResult = new ValidationResult(this.ModuleName + "-> La longitud del Código del Vendedor no puede ser mayor a 5.");
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class CotizacionVendedorViewModel

} //End of namespace Galac.Saw.Uil.SttDef

