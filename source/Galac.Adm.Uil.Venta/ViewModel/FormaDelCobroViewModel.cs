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
using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class FormaDelCobroViewModel : LibInputViewModelMfc<FormaDelCobro> {
        #region Constantes
        public const string CodigoPropertyName = "Codigo";
        public const string NombrePropertyName = "Nombre";
        public const string TipoDePagoPropertyName = "TipoDePago";
        public const string CodigoCuentaBancariaPropertyName = "CodigoCuentaBancaria";
        public const string NombreCuentaBancariaPropertyName = "NombreCuentaBancaria";
        public const string CodigoMonedaPropertyName = "CodigoMoneda";
        public const string NombreMonedaPropertyName = "MombreMoneda";
        public const string CodigoTheFactoryPropertyName = "CodigoTheFactory";
        public const string OrigenPropertyName = "Origen";
        #endregion
        #region Variables
        private FkCuentaBancariaViewModel _ConexionCodigoCuentaBancaria = null;
        private FkCuentaBancariaViewModel _ConexionNombreCuentaBancaria = null;
        private FkMonedaViewModel _ConexionCodigoMoneda = null;
        private string valCodigo;
        private string valMoneda;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Forma de Cobro"; }
        }

        public int  ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if (Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                }
            }
        }

        public int  Consecutivo {
            get {
                return Model.Consecutivo;
            }
            set {
                if (Model.Consecutivo != value) {
                    Model.Consecutivo = value;
                }
            }
        }
        [LibGridColum("Código", Width = 80, ColumnOrder = 0)]
        public string  Codigo {
            get {
                return Model.Codigo;
            }
            set {
                if (Model.Codigo != value) {
                    Model.Codigo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoPropertyName);
                }
            }
        }
        
        [LibRequired(ErrorMessage = "El campo Nombre es requerido.")]
        [LibGridColum("Nombre",Width = 350, ColumnOrder = 1)]
        public string  Nombre {
            get {
                return Model.Nombre;
            }
            set {
                if (Model.Nombre != value) {
                    Model.Nombre = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombrePropertyName);
                }
            }
        }

        [LibGridColum("Tipo", eGridColumType.Enum, PrintingMemberPath = "TipoDePagoStr", Width = 250, ColumnOrder = 2)]
        public eFormaDeCobro TipoDePago {
            get {
                return Model.TipoDePagoAsEnum;
            }
            set {
                if (Model.TipoDePagoAsEnum != value) {
                    Model.TipoDePagoAsEnum = value;
                    RaisePropertyChanged(TipoDePagoPropertyName);
                }
            }
        }
        [LibGridColum("Código Cuenta Bancaria", Width = 180, ColumnOrder = 4)]
        public string  CodigoCuentaBancaria {
            get {
                return Model.CodigoCuentaBancaria;
            }
            set {
                if (Model.CodigoCuentaBancaria != value) {
                    Model.CodigoCuentaBancaria = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoCuentaBancariaPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoCuentaBancaria, true)) {
                        ConexionCodigoCuentaBancaria = null;
                    }
                }
            }
        }

        
        public string  NombreCuentaBancaria {
            get {
                return Model.NombreCuentaBancaria;
            }
            set {
                if (Model.NombreCuentaBancaria != value) {
                    Model.NombreCuentaBancaria = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCuentaBancariaPropertyName);
                    if (LibString.IsNullOrEmpty(NombreCuentaBancaria, true)) {
                        
                    }
                }
            }
        }
        [LibGridColum("Moneda", Width = 100, ColumnOrder = 3)]
        public string  CodigoMoneda {
            get {
                return Model.CodigoMoneda;
            }
            set {
                if (Model.CodigoMoneda != value) {
                    Model.CodigoMoneda = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoMonedaPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoCuentaBancaria, true)) {
                        ConexionCodigoCuentaBancaria = null;
                    }
                }
            }
        }
        
        public string  NombreMoneda {
            get {
                return Model.NombreMoneda;
            }
            set {
                if (Model.NombreMoneda != value) {
                    Model.NombreMoneda = value;
                }
            }
        }

        public string  CodigoTheFactory {
            get {
                return Model.CodigoTheFactory;
            }
            set {
                if (Model.CodigoTheFactory != value) {
                    Model.CodigoTheFactory = value;
                    RaisePropertyChanged(CodigoTheFactoryPropertyName);
                }
            }
        }

        public eOrigen  Origen {
            get {
                return Model.OrigenAsEnum;
            }
            set {
                if (Model.OrigenAsEnum != value) {
                    Model.OrigenAsEnum = value;
                    RaisePropertyChanged(OrigenPropertyName);
                }
            }
        }

        public eFormaDeCobro[] ArrayTipoDeFormaDePago {
            get {
                return LibEnumHelper<eFormaDeCobro>.GetValuesInArray();
            }
        }

        public eOrigen[] ArrayOrigen {
            get {
                return LibEnumHelper<eOrigen>.GetValuesInArray();
            }
        }

        public bool IsEnabledCodigo {
            get {
                return false;
            }
        }

        public bool IsEnabledNombre {
            get {
                return Action == eAccionSR.Modificar;
            }
        }

        public bool IsEnabledCuenta {
            get {
                return Action == eAccionSR.Modificar;
            }
        }

        public bool IsEnabledTipoDePago {
            get {
                return false;
            }
        }
        public FkCuentaBancariaViewModel ConexionCodigoCuentaBancaria {
            get {
                return _ConexionCodigoCuentaBancaria;
            }
            set {
                if (_ConexionCodigoCuentaBancaria != value) {
                    _ConexionCodigoCuentaBancaria = value;
                    RaisePropertyChanged(CodigoCuentaBancariaPropertyName);
                    RaisePropertyChanged(NombreCuentaBancariaPropertyName);
                }
                if (_ConexionCodigoCuentaBancaria == null) {
                    CodigoCuentaBancaria = string.Empty;
                    NombreCuentaBancaria = string.Empty;
                }
            }
        }

        public FkMonedaViewModel ConexionCodigoMoneda {
            get {
                return _ConexionCodigoMoneda;
            }
            set {
                if (_ConexionCodigoMoneda != value) {
                    _ConexionCodigoMoneda = value;
                    RaisePropertyChanged(CodigoMonedaPropertyName);
                    RaisePropertyChanged(NombreMonedaPropertyName);
                }
                if (_ConexionCodigoMoneda == null) {
                    CodigoMoneda = string.Empty;
                    NombreMoneda = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoCuentaBancariaCommand {
            get;
            private set;
        }

        #endregion //Propiedades
        #region Constructores
        public FormaDelCobroViewModel()
            : this(new FormaDelCobro(), eAccionSR.Insertar) {
        }
        public FormaDelCobroViewModel(FormaDelCobro initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()){
            DefaultFocusedPropertyName = CodigoPropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(FormaDelCobro valModel) {
            base.InitializeLookAndFeel(valModel);

        }

        protected override FormaDelCobro FindCurrentRecord(FormaDelCobro valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "FormaDelCobroGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<FormaDelCobro>, IList<FormaDelCobro>> GetBusinessComponent() {
            return new clsFormaDelCobroNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoCuentaBancariaCommand = new RelayCommand<string>(ExecuteChooseCodigoCuentaBancariaCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            if (Model.CodigoCuentaBancaria != null && Model.CodigoCuentaBancaria != string.Empty) {
                LibSearchCriteria vCuentaBancariaCriteria = LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.ConsecutivoCompania", Model.ConsecutivoCompania);
                vCuentaBancariaCriteria.Add("Gv_CuentaBancaria_B1.Codigo", Model.CodigoCuentaBancaria);
                ConexionCodigoCuentaBancaria = FirstConnectionRecordOrDefault<FkCuentaBancariaViewModel>("Cuenta Bancaria", vCuentaBancariaCriteria);
                if (ConexionCodigoCuentaBancaria != null) {
                    CodigoCuentaBancaria = ConexionCodigoCuentaBancaria.Codigo;
                    NombreCuentaBancaria = ConexionCodigoCuentaBancaria.NombreCuenta;
                }
            }
            LibSearchCriteria vCodigoMoneda = LibSearchCriteria.CreateCriteria("dbo.Gv_Moneda_B1.Codigo", Model.CodigoMoneda);
            ConexionCodigoMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", vCodigoMoneda);
            if (ConexionCodigoMoneda != null) {
                CodigoMoneda = ConexionCodigoMoneda.Codigo;
                NombreMoneda = ConexionCodigoMoneda.Nombre;
            }
        }

        private void ExecuteChooseCodigoCuentaBancariaCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_CuentaBancaria_B1.Codigo", valCodigo);
                vDefaultCriteria.Add("Gv_CuentaBancaria_B1.CodigoMoneda", Model.CodigoMoneda);
                vDefaultCriteria.Add("Gv_CuentaBancaria_B1.Status", Ccl.Banco.eStatusCtaBancaria.Activo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.ConsecutivoCompania", Model.ConsecutivoCompania);
                ConexionCodigoCuentaBancaria = ChooseRecord<FkCuentaBancariaViewModel>("Cuenta Bancaria", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoCuentaBancaria != null) {
                    CodigoCuentaBancaria = ConexionCodigoCuentaBancaria.Codigo;
                    NombreCuentaBancaria = ConexionCodigoCuentaBancaria.NombreCuenta;
                } else {
                    CodigoCuentaBancaria = string.Empty;
                    NombreCuentaBancaria = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //Metodos Generados

        #region Validaciones

        #endregion //Validaciones
    } //End of class FormaDelCobroViewModel

} //End of namespace Galac.Saw.Uil.Tablas

