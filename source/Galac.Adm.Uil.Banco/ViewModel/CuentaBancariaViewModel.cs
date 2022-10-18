using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Brl.Banco;
using Galac.Adm.Ccl.Banco;
using System.IO;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Brl.TablasGen;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Uil.Banco.ViewModel {
	public class CuentaBancariaViewModel : LibInputViewModelMfc<CuentaBancaria> {
		#region Constantes
		private const string CodigoPropertyName = "Codigo";
		private const string StatusPropertyName = "Status";
		private const string NumeroCuentaPropertyName = "NumeroCuenta";
		private const string NombreCuentaPropertyName = "NombreCuenta";
		private const string CodigoBancoPropertyName = "CodigoBancoPant";
		private const string NombreBancoPropertyName = "NombreBanco";
		private const string NombreSucursalPropertyName = "NombreSucursal";
		private const string TipoCtaBancariaPropertyName = "TipoCtaBancaria";
		private const string ManejaDebitoBancarioPropertyName = "ManejaDebitoBancario";
		private const string ManejaCreditoBancarioPropertyName = "ManejaCreditoBancario";
		private const string SaldoDisponiblePropertyName = "SaldoDisponible";
		private const string NombreDeLaMonedaPropertyName = "NombreDeLaMoneda";
		private const string NombrePlantillaChequePropertyName = "NombrePlantillaCheque";
		private const string CuentaContablePropertyName = "CuentaContable";
		private const string CodigoMonedaPropertyName = "CodigoMoneda";
		private const string EsCajaChicaPropertyName = "EsCajaChica";
		private const string TipoDeAlicuotaPorContribuyentePropertyName = "TipoDeAlicuotaPorContribuyente";
        private const string ExcluirDelInformeDeDeclaracionIGTFPropertyName = "ExcluirDelInformeDeDeclaracionIGTF";
		private const string GeneraMovBancarioPorIGTFPropertyName = "GeneraMovBancarioPorIGTF";
		private const string NombreOperadorPropertyName = "NombreOperador";
		private const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
		private const string IsEnabledTipoDeAlicuotaPorContribuyentePropertyName = "IsEnabledTipoDeAlicuotaPorContribuyente";
		private const string IsEnabledExcluirDelInformeDeDeclaracionIGTFPropertyName = "IsEnabledExcluirDelInformeDeDeclaracionIGTF";
		private const string IsVisibleGeneraMovBancarioPorIGTFPropertyName = "IsVisibleGeneraMovBancarioPorIGTF";
		#endregion

		#region Variables
		private FkBancoViewModel _ConexionBanco = null;
		private FkCuentaViewModel _ConexionCuentaContable = null;
		private FkMonedaViewModel _ConexionMoneda = null;
		private Saw.Lib.clsNoComunSaw _MonedaLocal;
		#endregion //Variables

		#region Propiedades
		public override string ModuleName {
			get { return "Cuenta Bancaria"; }
		}

		public int ConsecutivoCompania {
			get {
				return Model.ConsecutivoCompania;
			}
			set {
				if (Model.ConsecutivoCompania != value) {
					Model.ConsecutivoCompania = value;
				}
			}
		}

		[LibRequired(ErrorMessage = "El campo Código Cuenta Bancaria es requerido.")]
		[LibGridColum("Código", Width = 80, DbMemberPath = "Saw.Gv_CuentaBancaria_B1.Codigo")]
		public string Codigo {
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

		[LibGridColum("Status", eGridColumType.Enum, PrintingMemberPath = "StatusStr", Width = 80, DbMemberPath = "Saw.Gv_CuentaBancaria_B1.Status")]
		public eStatusCtaBancaria Status {
			get {
				return Model.StatusAsEnum;
			}
			set {
				if (Model.StatusAsEnum != value) {
					Model.StatusAsEnum = value;
					IsDirty = true;
					RaisePropertyChanged(StatusPropertyName);
				}
			}
		}

		[LibRequired(ErrorMessage = "El campo Nombre es requerido.")]
		[LibGridColum("Nombre de la Cuenta", Width = 250)]
		public string NombreCuenta {
			get {
				return Model.NombreCuenta;
			}
			set {
				if (Model.NombreCuenta != value) {
					Model.NombreCuenta = value;
					IsDirty = true;
					RaisePropertyChanged(NombreCuentaPropertyName);
				}
			}
		}

		[LibRequired(ErrorMessage = "El campo Nº de Cuenta Bancaria es requerido.")]
		[LibGridColum("Nº de Cuenta Bancaria", Width = 250)]
		public string NumeroCuenta {
			get {
				return Model.NumeroCuenta;
			}
			set {
				if (Model.NumeroCuenta != value) {
					Model.NumeroCuenta = value;
					IsDirty = true;
					RaisePropertyChanged(NumeroCuentaPropertyName);
				}
			}
		}

		[LibRequired(ErrorMessage = "El campo Código del Banco es requerido.")]
		public int CodigoBanco {
			get {
				return Model.CodigoBanco;
			}
			set {
				if (Model.CodigoBanco != value) {
					Model.CodigoBanco = value;
					IsDirty = true;
					RaisePropertyChanged(CodigoBancoPropertyName);
				}
			}
		}

		[LibRequired(ErrorMessage = "El campo Código del Banco es requerido.")]
		public string CodigoBancoPant {
			get {
				return Model.CodigoBancoPant;
			}
			set {
				if (Model.CodigoBancoPant != value) {
					Model.CodigoBancoPant = value;
					IsDirty = true;
					RaisePropertyChanged(CodigoBancoPropertyName);
				}
			}
		}

		[LibRequired(ErrorMessage = "El campo Nombre del Banco es requerido.")]
		public string NombreBanco {
			get {
				return Model.NombreBanco;
			}
			set {
				if (Model.NombreBanco != value) {
					Model.NombreBanco = value;
					IsDirty = true;
					RaisePropertyChanged(NombreBancoPropertyName);
				}
			}
		}

		public string NombreSucursal {
			get {
				return Model.NombreSucursal;
			}
			set {
				if (Model.NombreSucursal != value) {
					Model.NombreSucursal = value;
					IsDirty = true;
					RaisePropertyChanged(NombreSucursalPropertyName);
				}
			}
		}

		[LibGridColum("Tipo", eGridColumType.Enum, PrintingMemberPath = "TipoCtaBancariaStr")]
		public eTipoDeCtaBancaria TipoCtaBancaria {
			get {
				return Model.TipoCtaBancariaAsEnum;
			}
			set {
				if (Model.TipoCtaBancariaAsEnum != value) {
					Model.TipoCtaBancariaAsEnum = value;
					IsDirty = true;
					RaisePropertyChanged(TipoCtaBancariaPropertyName);
				}
			}
		}

		public bool ManejaDebitoBancario {
			get {
				return Model.ManejaDebitoBancarioAsBool;
			}
			set {
				if (Model.ManejaDebitoBancarioAsBool != value) {
					Model.ManejaDebitoBancarioAsBool = value;
					IsDirty = true;
					if (!Model.ManejaDebitoBancarioAsBool) {
						TipoDeAlicuotaPorContribuyente = eTipoAlicPorContIGTF.NoAsignado;
						GeneraMovBancarioPorIGTF = false;
					}
					RaisePropertyChanged(ManejaDebitoBancarioPropertyName);
					RaisePropertyChanged(IsEnabledTipoDeAlicuotaPorContribuyentePropertyName);
					RaisePropertyChanged(IsEnabledExcluirDelInformeDeDeclaracionIGTFPropertyName);
				}
			}
		}

		public bool ManejaCreditoBancario {
			get {
				return Model.ManejaCreditoBancarioAsBool;
			}
			set {
				if (Model.ManejaCreditoBancarioAsBool != value) {
					Model.ManejaCreditoBancarioAsBool = value;
					IsDirty = true;
					RaisePropertyChanged(ManejaCreditoBancarioPropertyName);
				}
			}
		}

		[LibGridColum("Saldo", eGridColumType.Numeric)]
		public decimal SaldoDisponible {
			get {
				return Model.SaldoDisponible;
			}
			set {
				if (Model.SaldoDisponible != value) {
					Model.SaldoDisponible = value;
					IsDirty = true;
					RaisePropertyChanged(SaldoDisponiblePropertyName);
				}
			}
		}

		[LibRequired(ErrorMessage = "El campo Nombre de la Moneda es requerido.")]
		[LibCustomValidation("IsValidMoneda")]
		public string NombreDeLaMoneda {
			get {
				return Model.NombreDeLaMoneda;
			}
			set {
				if (Model.NombreDeLaMoneda != value) {
					Model.NombreDeLaMoneda = value;
					IsDirty = true;
					RaisePropertyChanged(NombreDeLaMonedaPropertyName);
					RaisePropertyChanged(IsVisibleGeneraMovBancarioPorIGTFPropertyName);
				}
			}
		}

		public string NombrePlantillaCheque {
			get {
				return Model.NombrePlantillaCheque;
			}
			set {
				if (Model.NombrePlantillaCheque != value) {
					Model.NombrePlantillaCheque = value;
					IsDirty = true;
					RaisePropertyChanged(NombrePlantillaChequePropertyName);
				}
			}
		}

		[LibCustomValidation("IsValidCC")]
		public string CuentaContable {
			get {
				return Model.CuentaContable;
			}
			set {
				if (Model.CuentaContable != value) {
					Model.CuentaContable = value;
					IsDirty = true;
					RaisePropertyChanged(CuentaContablePropertyName);
				}
			}
		}

		[LibRequired(ErrorMessage = "El campo Código Moneda es requerido.")]
		public string CodigoMoneda {
			get {
				return Model.CodigoMoneda;
			}
			set {
				if (Model.CodigoMoneda != value) {
					Model.CodigoMoneda = value;
					IsDirty = true;
					if (LibString.S1IsEqualToS2(CodigoMoneda, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("RecordName", "CodigoMonedaLocal"))) {
						GeneraMovBancarioPorIGTF = true;
						RaisePropertyChanged(GeneraMovBancarioPorIGTFPropertyName);
					}
					RaisePropertyChanged(CodigoMonedaPropertyName);
					RaisePropertyChanged(IsVisibleGeneraMovBancarioPorIGTFPropertyName);
				}
			}
		}

		[LibCustomValidation("EsCajaChicaValidating")]
		public bool EsCajaChica {
			get {
				return Model.EsCajaChicaAsBool;
			}
			set {
				if (Model.EsCajaChicaAsBool != value) {
					Model.EsCajaChicaAsBool = value;
					IsDirty = true;
					RaisePropertyChanged(EsCajaChicaPropertyName);
					if (Model.EsCajaChicaAsBool) {
						string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("RecordName", "CodigoMonedaLocal");
						SetMonedaDefault(vCodigoMonedaLocal);
					} else {
						RaisePropertyChanged(NombreDeLaMonedaPropertyName);
					}
				}
			}
		}

		public eTipoAlicPorContIGTF TipoDeAlicuotaPorContribuyente {
			get {
				return Model.TipoDeAlicuotaPorContribuyenteAsEnum;
			}
			set {
				if (Model.TipoDeAlicuotaPorContribuyenteAsEnum != value) {
					Model.TipoDeAlicuotaPorContribuyenteAsEnum = value;
					IsDirty = true;
					RaisePropertyChanged(TipoDeAlicuotaPorContribuyentePropertyName);
                }
            }
        }

        public bool  ExcluirDelInformeDeDeclaracionIGTF {
            get {
                return Model.ExcluirDelInformeDeDeclaracionIGTFAsBool;
            }
            set {
                if (Model.ExcluirDelInformeDeDeclaracionIGTFAsBool != value) {
                    Model.ExcluirDelInformeDeDeclaracionIGTFAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ExcluirDelInformeDeDeclaracionIGTFPropertyName);
				}
			}
		}

		public bool GeneraMovBancarioPorIGTF {
			get {
				return Model.GeneraMovBancarioPorIGTFAsBool;
			}
			set {
				if (Model.GeneraMovBancarioPorIGTFAsBool != value) {
					Model.GeneraMovBancarioPorIGTFAsBool = value;
					IsDirty = true;
					RaisePropertyChanged(GeneraMovBancarioPorIGTFPropertyName);
				}
			}
		}

		public string NombreOperador {
			get {
				return Model.NombreOperador;
			}
			set {
				if (Model.NombreOperador != value) {
					Model.NombreOperador = value;
					IsDirty = true;
					RaisePropertyChanged(NombreOperadorPropertyName);
				}
			}
		}

		public DateTime FechaUltimaModificacion {
			get {
				return Model.FechaUltimaModificacion;
			}
			set {
				if (Model.FechaUltimaModificacion != value) {
					Model.FechaUltimaModificacion = value;
					IsDirty = true;
					RaisePropertyChanged(FechaUltimaModificacionPropertyName);
				}
			}
		}

		public eStatusCtaBancaria[] ArrayStatusCtaBancaria {
			get {
				return LibEnumHelper<eStatusCtaBancaria>.GetValuesInArray();
			}
		}

		public eTipoDeCtaBancaria[] ArrayTipoDeCtaBancaria {
			get {
				return LibEnumHelper<eTipoDeCtaBancaria>.GetValuesInArray();
			}
		}

		public eTipoAlicPorContIGTF[] ArrayTipoAlicPorContIGTF {
			get {
				return LibEnumHelper<eTipoAlicPorContIGTF>.GetValuesInArray();
			}
		}

		public FkBancoViewModel ConexionBanco {
			get {
				return _ConexionBanco;
			}
			set {
				if (_ConexionBanco != value) {
					_ConexionBanco = value;
					RaisePropertyChanged(CodigoBancoPropertyName);
				}
			}
		}

		public FkCuentaViewModel ConexionCuentaContable {
			get {
				return _ConexionCuentaContable;
			}
			set {
				if (_ConexionCuentaContable != value) {
					_ConexionCuentaContable = value;
					RaisePropertyChanged(CuentaContablePropertyName);
				}
			}
		}

		public FkMonedaViewModel ConexionMoneda {
			get {
				return _ConexionMoneda;
			}
			set {
				if (_ConexionMoneda != value) {
					_ConexionMoneda = value;
					RaisePropertyChanged(CodigoMonedaPropertyName);
				}
			}
		}

		public RelayCommand<string> ChooseBancoCommand {
			get;
			private set;
		}

		public RelayCommand<string> ChooseNombreBancoCommand {
			get;
			private set;
		}

		public RelayCommand<string> ChooseCuentaContableCommand {
			get;
			private set;
		}

		public RelayCommand<string> ChooseMonedaCommand {
			get;
			private set;
		}

		public RelayCommand ChooseTemplateCommand {
			get;
			private set;
		}

		public bool IsVisibleCC {
			get {
				return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("RecordName", "AccesoCaracteristicaDeContabilidadActiva");
			}
		}

		private ValidationResult IsValidMoneda() {
			ValidationResult vResult = ValidationResult.Success;
			string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("RecordName", "CodigoMonedaLocal");
			string vNombreMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("RecordName", "NombreMonedaLocal");
			if (Action == eAccionSR.Consultar || Action == eAccionSR.Eliminar) {
				return ValidationResult.Success;
			} else {
				if (LibString.IsNullOrEmpty(Model.NombreDeLaMoneda, true)) {
					vResult = new ValidationResult("Debe seleccionar una moneda");
				} else {
					if (IsVisibleCajaChica && EsCajaChica && CodigoMoneda != vCodigoMonedaLocal) {
						vResult = new ValidationResult("La moneda debe ser ==> " + vNombreMonedaLocal + "<== debido a que esta marcada la opcion Caja Chica. Por favor cambie la moneda. Moneda Inválida");
					}
				}
				return vResult;
			}
		}

		private ValidationResult IsValidCC() {
			ValidationResult vResult = ValidationResult.Success;
			if (Action == eAccionSR.Consultar || Action == eAccionSR.Eliminar) {
				return ValidationResult.Success;
			} else {
				if (LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("RecordName", "AccesoCaracteristicaDeContabilidadActiva"))) {

					if (string.IsNullOrEmpty(CuentaContable)) {
						vResult = new ValidationResult("Debe seleccionar una Cuenta Contable");
					} else
						if (ConexionCuentaContable.TieneSubCuentas) {
						vResult = new ValidationResult("No puede escoger cuentas contables que sean de Título.");
					}
				}
			}
			return vResult;
		}

		public bool IsEnabledCodigoCta {
			get {
				return Action == eAccionSR.Insertar;
			}
		}

		private ValidationResult EsCajaChicaValidating() {
			ValidationResult vResult = ValidationResult.Success;
			ICuentaBancariaPdn insCtaBanNav = new clsCuentaBancariaNav();
			if (insCtaBanNav.TieneMovimientosBancariosDeReposicionCajaChica(ConsecutivoCompania, Codigo)) {
				vResult = new ValidationResult("La Cuenta Bancaria debe ser de tipo Caja Chica, ya que tiene movimientos bancarios de Reposición de Caja Chica asociados.");
			}
			return EsCajaChica ? ValidationResult.Success : vResult;
		}

		public bool IsEnabledCodigoBanco {
			get {
				return (Action == eAccionSR.Modificar || Action == eAccionSR.Insertar) ? LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("RecordName", "UsaCodigoBancoEnPantalla")) : Action == eAccionSR.Insertar;
			}
		}

		public bool IsEnabledNombreBanco {
			get {
				return (Action == eAccionSR.Modificar || Action == eAccionSR.Insertar) ? !LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("RecordName", "UsaCodigoBancoEnPantalla")) : Action == eAccionSR.Insertar;
			}
		}

		public bool IsVisibleCajaChica {
			get {
				return !EsEcuador();
			}
		}

		public bool IsVisibleITF {
			get {
				return (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("RecordName", "ManejaDebitoBancario") || LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("RecordName", "ManejaCreditoBancario")) && !EsEcuador();
			}
		}

		public bool IsEnabledITFDebitoBancario {
			get {
				if (Action == eAccionSR.Insertar) {
					return IsEnabled && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("RecordName", "ManejaDebitoBancario") && !EsEcuador();
				} else {
					return IsEnabled && !EsEcuador() && !ManejaDebitoBancario;
				}
			}
		}

		public bool IsEnabledITFCreditoBancario {
			get {
				return IsEnabled && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("RecordName", "ManejaCreditoBancario") && !EsEcuador();
			}
		}

		public bool IsEnabledTipoDeAlicuotaPorContribuyente {
			get {
				if (Action == eAccionSR.Insertar) {
					return IsEnabled && ManejaDebitoBancario;
				} else {
					ICuentaBancariaPdn CuentaBancariaNav = new clsCuentaBancariaNav();
					return IsEnabled && ManejaDebitoBancario && !CuentaBancariaNav.ExistenMovimientosPorCuentaBancariaPosterioresAReformaIGTFGO6687ConIGTFMarcado(ConsecutivoCompania, Codigo) && (TipoDeAlicuotaPorContribuyente == eTipoAlicPorContIGTF.NoAsignado || TipoDeAlicuotaPorContribuyente == eTipoAlicPorContIGTF.Cont14);
				}
			}
		}

		public bool IsEnabledExcluirDelInformeDeDeclaracionIGTF {
			get {
				if (Action == eAccionSR.Insertar) {
					return IsEnabled && ManejaDebitoBancario;
				} else {
					return IsEnabled && ManejaDebitoBancario && !EsEcuador();
				}
			}
		}
		
		public bool IsVisibleGeneraMovBancarioPorIGTF {
			get {
				return !_MonedaLocal.InstanceMonedaLocalActual.EsMonedaLocalDelPais(CodigoMoneda);
			}
		}

		#endregion //Propiedades
		#region Constructores
		public CuentaBancariaViewModel()
			: this(new CuentaBancaria(), eAccionSR.Insertar) {
		}

		public CuentaBancariaViewModel(CuentaBancaria initModel, eAccionSR initAction)
			: base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
			Model.ConsecutivoCompania = Mfc.GetInt("Compania");
		}
		#endregion //Constructores

		#region Metodos Generados
		protected override void InitializeLookAndFeel(CuentaBancaria valModel) {
			base.InitializeLookAndFeel(valModel);
			_MonedaLocal = new Saw.Lib.clsNoComunSaw();
			_MonedaLocal.InstanceMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
			if (Action == eAccionSR.Insertar) {
				if (LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("RecordName", "UsaDivisaComoMonedaPrincipalDeIngresoDeDatos"))) {
					SetMonedaDefault(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("RecordName", "CodigoMonedaExtranjera"));
				} else {
					SetMonedaDefault(_MonedaLocal.InstanceMonedaLocalActual.GetHoyCodigoMoneda());
				}
			}
		}

		protected override CuentaBancaria FindCurrentRecord(CuentaBancaria valModel) {
			if (valModel == null) {
				return null;
			}
			LibGpParams vParams = new LibGpParams();
			vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
			vParams.AddInString("Codigo", valModel.Codigo, 5);
			return BusinessComponent.GetData(eProcessMessageType.SpName, "CuentaBancariaGET", vParams.Get()).FirstOrDefault();
		}

		protected override ILibBusinessComponentWithSearch<IList<CuentaBancaria>, IList<CuentaBancaria>> GetBusinessComponent() {
			return new clsCuentaBancariaNav();
		}

		protected override void InitializeCommands() {
			base.InitializeCommands();
			ChooseCuentaContableCommand = new RelayCommand<string>(ExecuteChooseCuentaContableCommand);
			ChooseMonedaCommand = new RelayCommand<string>(ExecuteChooseMonedaCommand);
			ChooseTemplateCommand = new RelayCommand(ExecuteBuscarPlantillaCommand);
			ChooseNombreBancoCommand = new RelayCommand<string>(ExecuteChooseNombreBancoCommand);
			ChooseBancoCommand = new RelayCommand<string>(ExecuteChooseBancoCommand);
		}

		protected override void ReloadRelatedConnections() {
			base.ReloadRelatedConnections();
			ConexionMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteria("Codigo", CodigoMoneda));
			ConexionBanco = FirstConnectionRecordOrDefault<FkBancoViewModel>("Banco", LibSearchCriteria.CreateCriteria("Nombre", NombreBanco));
			LibSearchCriteria vCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoPeriodo", LibGlobalValues.Instance.GetMfcInfo().GetInt("Periodo"));
			vCriteria.Add(LibSearchCriteria.CreateCriteria("TieneSubCuentas", "N"), eLogicOperatorType.And);
			vCriteria.Add(LibSearchCriteria.CreateCriteria("ConsecutivoCompania", ConsecutivoCompania), eLogicOperatorType.And);
			vCriteria.Add(LibSearchCriteria.CreateCriteriaFromText("Codigo", CuentaContable), eLogicOperatorType.And);
			ConexionCuentaContable = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", vCriteria);
		}

		private void ExecuteChooseBancoCommand(string valCodigo) {
			try {
				if (valCodigo == null) {
					valCodigo = string.Empty;
				}
				CodigoBanco = 0;
				CodigoBancoPant = string.Empty;
				NombreBanco = string.Empty;
				if (valCodigo != "0") {
					LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
					LibSearchCriteria vFixedCriteria = null;
					ConexionBanco = ChooseRecord<FkBancoViewModel>("Banco", vDefaultCriteria, vFixedCriteria, string.Empty);
					if (_ConexionBanco != null) {
						CodigoBanco = ConexionBanco.Consecutivo;
						CodigoBancoPant = ConexionBanco.Codigo;
						NombreBanco = ConexionBanco.Nombre;
					}
				}
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibMessages.RaiseError.ShowError(vEx, ModuleName);
			}
		}

		private void ExecuteChooseNombreBancoCommand(string valNombre) {
			try {
				if (valNombre == null) {
					valNombre = string.Empty;
				}
				CodigoBanco = 0;
				CodigoBancoPant = string.Empty;
				NombreBanco = string.Empty;
				LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
				LibSearchCriteria vFixedCriteria = null;
				ConexionBanco = ChooseRecord<FkBancoViewModel>("Banco", vDefaultCriteria, vFixedCriteria, string.Empty);
				if (_ConexionBanco != null) {
					CodigoBanco = ConexionBanco.Consecutivo;
					CodigoBancoPant = ConexionBanco.Codigo;
					NombreBanco = ConexionBanco.Nombre;
				}
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibMessages.RaiseError.ShowError(vEx, ModuleName);
			}
		}

		private void ExecuteChooseCuentaContableCommand(string valCodigo) {
			try {
				if (valCodigo == null) {
					valCodigo = string.Empty;
				}
				CuentaContable = string.Empty;
				LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
				LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoPeriodo", LibGlobalValues.Instance.GetMfcInfo().GetInt("Periodo"));
				vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("TieneSubCuentas", "N"), eLogicOperatorType.And);
				vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("ConsecutivoCompania", ConsecutivoCompania), eLogicOperatorType.And);
				ConexionCuentaContable = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);
				if (ConexionCuentaContable != null) {
					CuentaContable = ConexionCuentaContable.Codigo;
				}
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibMessages.RaiseError.ShowError(vEx, ModuleName);
			}
		}

		private void ExecuteChooseMonedaCommand(string valNombre) {
			try {
				if (valNombre == null) {
					valNombre = string.Empty;
				}
				NombreDeLaMoneda = string.Empty;
				CodigoMoneda = string.Empty;
				TipoDeAlicuotaPorContribuyente = eTipoAlicPorContIGTF.NoAsignado;
				GeneraMovBancarioPorIGTF = false;
				LibSearchCriteria vDefaultCriteria;
				if (!LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("RecordName", "UsaMonedaExtranjera"))) {
					string CodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("RecordName", "CodigoMonedaLocal");
					vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", CodigoMonedaLocal);
				} else {
					vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
				}
				LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteriaFromText("Activa", "S");
				vFixedCriteria.Add("TipoDeMoneda", eBooleanOperatorType.IdentityEquality, eTipoDeMoneda.Fisica);
				AgregarCriteriaParaExcluirMonedasLocalesNoVigentesAlDiaActual(ref vFixedCriteria);
				ConexionMoneda = ChooseRecord<FkMonedaViewModel>("Moneda", vDefaultCriteria, vFixedCriteria, string.Empty);
				if (ConexionMoneda != null) {
					CodigoMoneda = ConexionMoneda.Codigo;
					NombreDeLaMoneda = ConexionMoneda.Nombre;
				}
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibMessages.RaiseError.ShowError(vEx, ModuleName);
			}
		}

		private void AgregarCriteriaParaExcluirMonedasLocalesNoVigentesAlDiaActual(ref LibSearchCriteria vFixedCriteria) {
			XElement vXmlMonedaLocales = ((IMonedaLocalPdn)new clsMonedaLocalProcesos()).BusquedaTodasLasMonedasLocales(LibDefGen.ProgramInfo.Country);
			IList<MonedaLocalActual> vListaDeMonedaLocales = new List<MonedaLocalActual>();
			vListaDeMonedaLocales = vXmlMonedaLocales != null ? LibParserHelper.ParseToList<MonedaLocalActual>(new XDocument(vXmlMonedaLocales)) : null;
			if (vListaDeMonedaLocales != null) {
				foreach (MonedaLocalActual vMoneda in vListaDeMonedaLocales) {
					if (vMoneda.CodigoMoneda != _MonedaLocal.InstanceMonedaLocalActual.GetHoyCodigoMoneda()) {
						vFixedCriteria.Add("Codigo", eBooleanOperatorType.IdentityInequality, vMoneda.CodigoMoneda);
					}
				}
			}
		}

		private void ExecuteBuscarPlantillaCommand() {
			try {
				LibFileDialogMessage vMessage = new LibFileDialogMessage("Escoger Plantilla", BuscarPlantilla);
				vMessage.Filter = "rpx de Cheque (*.rpx*)|*Cheque*.rpx";
				vMessage.InitialDirectory = LibWorkPaths.OriginalReportDir;
				LibMessages.OpenFile.Send(vMessage);
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibMessages.RaiseError.ShowError(vEx, ModuleName);
			}
		}

		private
			void BuscarPlantilla(FileInfo valFileInfo) {
			NombrePlantillaCheque = LibFile.FileNameWithoutExtension(valFileInfo.FullName);
		}

		private bool EsEcuador() {
			return LibDefGen.ProgramInfo.IsCountryEcuador();
		}

		private void SetMonedaDefault(string Codigo) {
			ConexionMoneda = FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", Codigo));
			if (ConexionMoneda != null) {
				CodigoMoneda = ConexionMoneda.Codigo;
				NombreDeLaMoneda = ConexionMoneda.Nombre;
			}
		}
		#endregion //Metodos Generados

	} //End of class CuentaBancariaViewModel

} //End of namespace Galac.Adm.Uil.Banco

