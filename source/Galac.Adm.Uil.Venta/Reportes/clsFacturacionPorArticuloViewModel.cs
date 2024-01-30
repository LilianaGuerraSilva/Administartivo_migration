using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Uil.Venta.ViewModel;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Galac.Adm.Uil.Venta.Reportes {

	public class clsFacturacionPorArticuloViewModel : LibInputRptViewModelBase<Factura> {
		#region Constantes
		public const string FechaDesdePropertyName = "FechaDesde";
		public const string FechaHastaPropertyName = "FechaHasta";
		public const string CantidadAImprimirPropertyName = "CantidadAImprimir";
		public const string CodigoDelArticuloPropertyName = "CodigoDelArticulo";
		public const string IsEnabledCodigoDelArticuloPropertyName = "IsEnabledCodigoDelArticulo";
		public const string MonedaDelInformePropertyName = "MonedaDelInforme";
		public const string TipoTasaDeCambioPropertyName = "TipoTasaDeCambio";
		public const string IsVisibleTipoTasaDeCambioPropertyName = "IsVisibleTipoTasaDeCambio";
		public const string IsInformeDetalladoPropertyName = "IsInformeDetallado";
		#endregion //Constantes

		#region Variables
		private DateTime _FechaDesde;
		private DateTime _FechaHasta;
		private eCantidadAImprimir _CantidadAImprimirAsEnum;
		private string _CodigoDelArticulo;
		private Saw.Lib.eMonedaParaImpresion _MonedaDelInformeAsEnum;
		private Saw.Lib.eTasaDeCambioParaImpresion _TipoTasaDeCambioAsEnum;
		bool _IsInformeDetallado;

		private FkArticuloInventarioViewModel _ConexionCodigoDelArticulo = null;

		private ObservableCollection<Saw.Lib.eMonedaParaImpresion> _ListaMonedaDelInforme = new ObservableCollection<Saw.Lib.eMonedaParaImpresion>();
		#endregion //Variables

		#region Propiedades
		public override string DisplayName {
			get { return "Facturación por Artículo"; }
		}

		public LibXmlMemInfo AppMemoryInfo { get; set; }

		public LibXmlMFC Mfc { get; set; }

		public override bool IsSSRS => throw new NotImplementedException();

		public RelayCommand<string> ChooseCodigoDelArticuloCommand {
			get;
			private set;
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

		public eCantidadAImprimir CantidadAImprimir {
			get {
				return _CantidadAImprimirAsEnum;
			}
			set {
				if (_CantidadAImprimirAsEnum != value) {
					_CantidadAImprimirAsEnum = value;
					CodigoDelArticulo = string.Empty;
					RaisePropertyChanged(CantidadAImprimirPropertyName);
					RaisePropertyChanged(IsEnabledCodigoDelArticuloPropertyName);
				}
			}
		}

		[LibCustomValidation("CodigoDelArticuloValidating")]
		public string CodigoDelArticulo {
			get {
				return _CodigoDelArticulo;
			}
			set {
				if (_CodigoDelArticulo != value) {
					_CodigoDelArticulo = value;
					RaisePropertyChanged(CodigoDelArticuloPropertyName);
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
			get {
				return _TipoTasaDeCambioAsEnum;
			}
			set {
				if (_TipoTasaDeCambioAsEnum != value) {
					_TipoTasaDeCambioAsEnum = value;
					RaisePropertyChanged(TipoTasaDeCambioPropertyName);
				}
			}
		}

		public bool IsInformeDetallado {
			get {
				return _IsInformeDetallado;
			}
			set {
				if (_IsInformeDetallado != value) {
					_IsInformeDetallado = value;
					RaisePropertyChanged(IsInformeDetalladoPropertyName);
				}
			}
		}

		public bool InformeDetallado {
			get {
				return _IsInformeDetallado;
			}
			set {
				if (value) {
					IsInformeDetallado = true;
				}
			}
		}

		public bool InformeResumido {
			get {
				return !_IsInformeDetallado;
			}
			set {
				if (value) {
					IsInformeDetallado = false;
				}
			}
		}

		public FkArticuloInventarioViewModel ConexionCodigoDelArticulo {
			get {
				return _ConexionCodigoDelArticulo;
			}
			set {
				if (_ConexionCodigoDelArticulo != value) {
					_ConexionCodigoDelArticulo = value;
				}
				if (_ConexionCodigoDelArticulo != null) {
					CodigoDelArticulo = _ConexionCodigoDelArticulo.Codigo;
				}
				if (_ConexionCodigoDelArticulo == null) {
					CodigoDelArticulo = string.Empty;
				}
			}
		}

		public eCantidadAImprimir[] ArrayCantidadAImprimir {
			get {
				return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray();
			}
		}

		public Saw.Lib.eTasaDeCambioParaImpresion[] ArrayTipoTasaDeCambio {
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
		public clsFacturacionPorArticuloViewModel() {
			FechaDesde = LibDate.Today();
			FechaHasta = LibDate.Today();
			CantidadAImprimir = eCantidadAImprimir.All;
			CodigoDelArticulo = string.Empty;
			MonedaDelInforme = Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal;
			TipoTasaDeCambio = Saw.Lib.eTasaDeCambioParaImpresion.DelDia;
			IsInformeDetallado = true;
			LlenarEnumerativosMonedas();
		}

		protected override void InitializeCommands() {
			base.InitializeCommands();
			ChooseCodigoDelArticuloCommand = new RelayCommand<string>(ExecuteChooseCodigoDelArticuloCommand);
		}

		private void ExecuteChooseCodigoDelArticuloCommand(string valNombre) {
			try {
				if (valNombre == null) {
					valNombre = string.Empty;
				}
				LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("dbo.Gv_ArticuloInventario_B1.Codigo", valNombre);
				LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
				ConexionCodigoDelArticulo = null;
				ConexionCodigoDelArticulo = ChooseRecord<FkArticuloInventarioViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
			}
			catch (AccessViolationException) {
				throw;
			}
			catch (Exception vEx) {
				LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
			}
		}
		#endregion //Constructores

		#region Metodos Generados
		protected override ILibBusinessSearch GetBusinessComponent() {
			return new clsFacturaNav();
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

		private ValidationResult CodigoDelArticuloValidating() {
			ValidationResult vResult = ValidationResult.Success;
			if (LibString.IsNullOrEmpty(CodigoDelArticulo) && CantidadAImprimir == eCantidadAImprimir.One) {
				vResult = new ValidationResult("El código del artículo no puede estar en blanco");
			}
			return vResult;
		}

		public bool IsEnabledCodigoDelArticulo {
			get { return CantidadAImprimir == eCantidadAImprimir.One; }
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

	} //End of class clsFacturacionPorArticuloViewModel

} //End of namespace Galac.Adm.Uil.Venta

