using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Uil.Venta.ViewModel;
using LibGalac.Aos.UI.Mvvm.Validation;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Uil.Venta.Reportes {

	public class clsCxCPorClienteViewModel : LibInputRptViewModelBase<CxC> {
		#region Constantes
		public const string CantidadAImprimirPropertyName = "CantidadAImprimir";
		public const string NombreDelClientePropertyName = "NombreDelCliente";
		public const string IsEnabledNombreDelClientePropertyName = "IsEnabledNombreDelCliente";
		public const string CodigoDelClientePropertyName = "CodigoDelCliente";
		public const string ZonasAImprimirPropertyName = "ZonasAImprimir";
		public const string ZonaCobranzaPropertyName = "ZonaCobranza";
		public const string IsEnabledZonaCobranzaPropertyName = "IsEnabledZonaCobranza";
		public const string FechaDesdePropertyName = "FechaDesde";
		public const string FechaHastaPropertyName = "FechaHasta";
		public const string MostrarContactoPropertyName = "MostrarContacto";
		public const string ClientesOrdenadosPorAsEnumPropertyName = "ClientesOrdenadosPor";
		public const string MonedaDelInformePropertyName = "MonedaDelInforme";
		public const string TipoTasaDeCambioPropertyName = "TipoTasaDeCambio";
		public const string IsVisibleTipoTasaDeCambioPropertyName = "IsVisibleTipoTasaDeCambio";
		#endregion //Constantes

		#region Variables
		private Saw.Lib.eCantidadAImprimir _CantidadAImprimirAsEnum;
		private string _NombreDelCliente;
		private string _CodigoDelCliente;
		private Saw.Lib.eCantidadAImprimir _ZonasAImprimirAsEnum;
		private string _ZonaCobranza;
		private DateTime _FechaDesde;
		private DateTime _FechaHasta;
		private bool _MostrarContacto;
		private eClientesOrdenadosPor _ClientesOrdenadosPorAsEnum;
		private Saw.Lib.eMonedaParaImpresion _MonedaDelInformeAsEnum;
		private Saw.Lib.eTasaDeCambioParaImpresion _TipoTasaDeCambioAsEnum;

		private FkClienteViewModel _ConexionNombreDelCliente = null;
		private FkZonaCobranzaViewModel _ConexionZonaCobranza = null;

		private ObservableCollection<Saw.Lib.eMonedaParaImpresion> _ListaMonedaDelInforme = new ObservableCollection<Saw.Lib.eMonedaParaImpresion>();
		#endregion //Variables

		#region Propiedades
		public override string DisplayName {
			get { return "CxC por Cliente"; }
		}

		public LibXmlMemInfo AppMemoryInfo { get; set; }

		public LibXmlMFC Mfc { get; set; }

		public override bool IsSSRS => throw new NotImplementedException();

		public RelayCommand<string> ChooseNombreDelClienteCommand {
			get;
			private set;
		}

		public RelayCommand<string> ChooseZonaCobranzaCommand {
			get;
			private set;
		}

		public Saw.Lib.eCantidadAImprimir CantidadAImprimir {
			get {
				return _CantidadAImprimirAsEnum;
			}
			set {
				if (_CantidadAImprimirAsEnum != value) {
					_CantidadAImprimirAsEnum = value;
					NombreDelCliente = string.Empty;
					CodigoDelCliente = string.Empty;
					RaisePropertyChanged(CantidadAImprimirPropertyName);
					RaisePropertyChanged(IsEnabledNombreDelClientePropertyName);
				}
			}
		}

		[LibCustomValidation("NombreDelClienteValidating")]
		public string NombreDelCliente {
			get {
				return _NombreDelCliente;
			}
			set {
				if (_NombreDelCliente != value) {
					_NombreDelCliente = value;
					RaisePropertyChanged(NombreDelClientePropertyName);
				}
			}
		}

		public string CodigoDelCliente {
			get {
				return _CodigoDelCliente;
			}
			set {
				if (_CodigoDelCliente != value) {
					_CodigoDelCliente = value;
					RaisePropertyChanged(CodigoDelClientePropertyName);
				}
			}
		}

		public Saw.Lib.eCantidadAImprimir ZonasAImprimir {
			get {
				return _ZonasAImprimirAsEnum;
			}
			set {
				if (_ZonasAImprimirAsEnum != value) {
					_ZonasAImprimirAsEnum = value;
					ZonaCobranza = string.Empty;
					RaisePropertyChanged(ZonasAImprimirPropertyName);
					RaisePropertyChanged(IsEnabledZonaCobranzaPropertyName);
				}
			}
		}

		[LibCustomValidation("ZonaCobranzaValidating")]
		public string ZonaCobranza {
			get {
				return _ZonaCobranza;
			}
			set {
				if (_ZonaCobranza != value) {
					_ZonaCobranza = value;
					RaisePropertyChanged(ZonaCobranzaPropertyName);
				}
			}
		}

		[LibCustomValidation("FechaDesdeValidating")]
		public DateTime FechaDesde {
			get {
				return _FechaDesde;
			}
			set {
				if (_FechaDesde != value) {
					_FechaDesde = value;
					RaisePropertyChanged(FechaDesdePropertyName);
				}
			}
		}

		[LibCustomValidation("FechaHastaValidating")]
		public DateTime FechaHasta {
			get {
				return _FechaHasta;
			}
			set {
				if (_FechaHasta != value) {
					_FechaHasta = value;
					RaisePropertyChanged(FechaHastaPropertyName);
				}
			}
		}

		public bool MostrarContacto {
			get {
				return _MostrarContacto;
			}
			set {
				if (_MostrarContacto != value) {
					_MostrarContacto = value;
					RaisePropertyChanged(MostrarContactoPropertyName);
				}
			}
		}

		public eClientesOrdenadosPor ClientesOrdenadosPor {
			get {
				return _ClientesOrdenadosPorAsEnum;
			}
			set {
				if (_ClientesOrdenadosPorAsEnum != value) {
					_ClientesOrdenadosPorAsEnum = value;
					RaisePropertyChanged(ClientesOrdenadosPorAsEnumPropertyName);
				}
			}
		}

		public Saw.Lib.eMonedaParaImpresion MonedaDelInforme {
			get {
				return _MonedaDelInformeAsEnum;
			}
			set {
				if (_MonedaDelInformeAsEnum != value) {
					_MonedaDelInformeAsEnum = value;
					TipoTasaDeCambio = Saw.Lib.eTasaDeCambioParaImpresion.DelDia;
					RaisePropertyChanged(MonedaDelInformePropertyName);
					RaisePropertyChanged(IsVisibleTipoTasaDeCambioPropertyName);
				}
			}
		}

		public Saw.Lib.eTasaDeCambioParaImpresion TipoTasaDeCambio {
			get
			{
				return _TipoTasaDeCambioAsEnum;
			}
			set {
				if (_TipoTasaDeCambioAsEnum != value) {
					_TipoTasaDeCambioAsEnum = value;
					RaisePropertyChanged(TipoTasaDeCambioPropertyName);
				}
			}
		}

		public FkClienteViewModel ConexionNombreDelCliente {
			get {
				return _ConexionNombreDelCliente;
			}
			set {
				if (_ConexionNombreDelCliente != value) {
					_ConexionNombreDelCliente = value;
				}
				if (_ConexionNombreDelCliente != null) {
					NombreDelCliente = _ConexionNombreDelCliente.Nombre;
					CodigoDelCliente = _ConexionNombreDelCliente.Codigo;
				}
				if (_ConexionNombreDelCliente == null) {
					NombreDelCliente = string.Empty;
				}
			}
		}

		public FkZonaCobranzaViewModel ConexionZonaCobranza {
			get {
				return _ConexionZonaCobranza;
			}
			set {
				if (_ConexionZonaCobranza != value) {
					_ConexionZonaCobranza = value;
				}
				if (_ConexionZonaCobranza != null) {
					ZonaCobranza = _ConexionZonaCobranza.Nombre;
				}
				if (_ConexionZonaCobranza == null) {
					ZonaCobranza = string.Empty;
				}
			}
		}

		public Saw.Lib.eCantidadAImprimir[] ArrayCantidadAImprimir {
			get {
				return LibEnumHelper<Saw.Lib.eCantidadAImprimir>.GetValuesInArray();
			}
		}

		public Saw.Lib.eCantidadAImprimir[] ArrayZonasAImprimir {
			get {
				return LibEnumHelper<Saw.Lib.eCantidadAImprimir>.GetValuesInArray();
			}
		}

		public eClientesOrdenadosPor[] ArrayClientesOrdenadosPor {
			get {
				return LibEnumHelper<eClientesOrdenadosPor>.GetValuesInArray();
			}
		}

		public Saw.Lib.eTasaDeCambioParaImpresion[] ArrayTiposTasaDeCambio {
			get {
				return LibEnumHelper<Saw.Lib.eTasaDeCambioParaImpresion>.GetValuesInArray();
			}
		}

		public ObservableCollection<Saw.Lib.eMonedaParaImpresion> ListaMonedaDelInforme {
			get {
				return _ListaMonedaDelInforme;
			}
			set {
				_ListaMonedaDelInforme = value;
			}
		}
		#endregion //Propiedades

		#region Constructores
		public clsCxCPorClienteViewModel() {
			CantidadAImprimir = Saw.Lib.eCantidadAImprimir.Todos;
			NombreDelCliente = string.Empty;
			ZonasAImprimir = Saw.Lib.eCantidadAImprimir.Todos;
			ZonaCobranza = string.Empty;
			FechaDesde = LibDate.Today();
			FechaHasta = LibDate.Today();
			MostrarContacto = false;
			ClientesOrdenadosPor = eClientesOrdenadosPor.PorNombre;
			MonedaDelInforme = Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal;
			TipoTasaDeCambio = Saw.Lib.eTasaDeCambioParaImpresion.DelDia;
			LlenarEnumerativosMonedas();
		}

		protected override void InitializeCommands() {
			base.InitializeCommands();
			ChooseNombreDelClienteCommand = new RelayCommand<string>(ExecuteChooseNombreDelClienteCommand);
			ChooseZonaCobranzaCommand = new RelayCommand<string>(ExecuteChooseZonaCobranzaCommand);
		}

		private void ExecuteChooseNombreDelClienteCommand(string valNombre) {
			try {
				if (valNombre == null) {
					valNombre = string.Empty;
				}
				LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
				LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
				ConexionNombreDelCliente = null;
				ConexionNombreDelCliente = ChooseRecord<FkClienteViewModel>("Cliente", vDefaultCriteria, vFixedCriteria, string.Empty);
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
			}
		}

		private void ExecuteChooseZonaCobranzaCommand(string valNombre) {
			try {
				if (valNombre == null) {
					valNombre = string.Empty;
				}
				LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
				LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
				ConexionZonaCobranza = ChooseRecord<FkZonaCobranzaViewModel>("Zona Cobranza", vDefaultCriteria, vFixedCriteria, string.Empty);
				if (ConexionZonaCobranza != null) {
					ZonaCobranza = ConexionZonaCobranza.Nombre;
				} else {
					ZonaCobranza = string.Empty;
				}
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
			}
		}
		#endregion //Constructores

		#region Metodos Generados
		protected override ILibBusinessSearch GetBusinessComponent() {
			return new clsCXCNav();
		}
		#endregion //Metodos Generados

		#region Código Programador
		private ValidationResult FechaDesdeValidating() {
			ValidationResult vResult = ValidationResult.Success;
			if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDesde, false, eAccionSR.InformesPantalla)) {
				vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha Desde"));
			} else if (LibDate.F1IsGreaterThanF2(FechaDesde, FechaHasta)) {
				vResult = new ValidationResult("La fecha desde no puede ser mayor a la fecha hasta");
			}
			return vResult;
		}

		private ValidationResult FechaHastaValidating() {
			ValidationResult vResult = ValidationResult.Success;
			if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaHasta, false, eAccionSR.InformesPantalla)) {
				vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha Desde"));
			} else if (LibDate.F1IsLessThanF2(FechaHasta, FechaDesde)) {
				vResult = new ValidationResult("La fecha hasta no puede ser menor a la fecha desde");
			}
			return vResult;
		}

		private ValidationResult NombreDelClienteValidating() {
			ValidationResult vResult = ValidationResult.Success;
			if (LibString.IsNullOrEmpty(NombreDelCliente) && CantidadAImprimir == Saw.Lib.eCantidadAImprimir.Uno) {
				vResult = new ValidationResult("El nombre del cliente no puede estar en blanco");
			}
			return vResult;
		}

		private ValidationResult ZonaCobranzaValidating() {
			ValidationResult vResult = ValidationResult.Success;
			if (LibString.IsNullOrEmpty(ZonaCobranza) && ZonasAImprimir == Saw.Lib.eCantidadAImprimir.Uno) {
				vResult = new ValidationResult("La zona de cobranza no puede estar en blanco");
			}
			return vResult;
		}

		public bool IsEnabledNombreDelCliente {
			get { return CantidadAImprimir == Saw.Lib.eCantidadAImprimir.Uno; }
		}

		public bool IsEnabledZonaCobranza {
			get { return ZonasAImprimir == Saw.Lib.eCantidadAImprimir.Uno; }
		}

		public bool IsVisibleTipoTasaDeCambio {
			get { return MonedaDelInforme != Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal; }
		}

		private void LlenarEnumerativosMonedas() {
			ListaMonedaDelInforme.Clear();
			if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
				ListaMonedaDelInforme.Add(Saw.Lib.eMonedaParaImpresion.EnBolivares);
			}
			else if (LibDefGen.ProgramInfo.IsCountryPeru()) {
				ListaMonedaDelInforme.Add(Saw.Lib.eMonedaParaImpresion.EnSoles);
			}
			ListaMonedaDelInforme.Add(Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal);
		}
		#endregion //Código Programador

	} //End of class clsCxCPorClienteViewModel

} //End of namespace Galac.Adm.Uil.Venta

