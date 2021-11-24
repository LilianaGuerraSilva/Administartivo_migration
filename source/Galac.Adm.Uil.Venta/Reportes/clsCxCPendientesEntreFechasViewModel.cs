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
using LibGalac.Aos.DefGen;
using System.Collections.ObjectModel;

namespace Galac.Adm.Uil.Venta.Reportes {

	public class clsCxCPendientesEntreFechasViewModel : LibInputRptViewModelBase<CxC> {
		#region Constantes
		public const string FechaDesdePropertyName = "FechaDesde";
		public const string FechaHastaPropertyName = "FechaHasta";
		public const string MostrarContactoPropertyName = "MostrarContacto";
		public const string MonedaDelInformePropertyName = "MonedaDelInforme";
		public const string TipoTasaDeCambioPropertyName = "TipoTasaDeCambio";
		public const string IsVisibleTipoTasaDeCambioPropertyName = "IsVisibleTipoTasaDeCambio";
		#endregion //Constantes

		#region Variables
		private DateTime _FechaDesde;
		private DateTime _FechaHasta;
		private bool _MostrarContacto;
		private Saw.Lib.eMonedaParaImpresion _MonedaDelInformeAsEnum;
		private Saw.Lib.eTasaDeCambioParaImpresion _TipoTasaDeCambioAsEnum;

		private ObservableCollection<Saw.Lib.eMonedaParaImpresion> _ListaMonedaDelInforme = new ObservableCollection<Saw.Lib.eMonedaParaImpresion>();
		#endregion //Variables

		#region Propiedades
		public override string DisplayName {
			get { return "CxC Pendientes entre Fechas"; }
		}

		public LibXmlMemInfo AppMemoryInfo { get; set; }

		public LibXmlMFC Mfc { get; set; }

		public override bool IsSSRS => throw new NotImplementedException();

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

		public Saw.Lib.eMonedaParaImpresion MonedaDelInforme
		{
			get
			{
				return _MonedaDelInformeAsEnum;
			}
			set
			{
				if (_MonedaDelInformeAsEnum != value) {
					_MonedaDelInformeAsEnum = value;
					TipoTasaDeCambio = Saw.Lib.eTasaDeCambioParaImpresion.DelDia;
					RaisePropertyChanged(MonedaDelInformePropertyName);
					RaisePropertyChanged(IsVisibleTipoTasaDeCambioPropertyName);
				}
			}
		}

		public Saw.Lib.eTasaDeCambioParaImpresion TipoTasaDeCambio
		{
			get
			{
				return _TipoTasaDeCambioAsEnum;
			}
			set
			{
				if (_TipoTasaDeCambioAsEnum != value) {
					_TipoTasaDeCambioAsEnum = value;
					RaisePropertyChanged(TipoTasaDeCambioPropertyName);
				}
			}
		}

		public Saw.Lib.eTasaDeCambioParaImpresion[] ArrayTiposTasaDeCambio
		{
			get
			{
				return LibEnumHelper<Saw.Lib.eTasaDeCambioParaImpresion>.GetValuesInArray();
			}
		}

		public ObservableCollection<Saw.Lib.eMonedaParaImpresion> ListaMonedaDelInforme
		{
			get
			{
				return _ListaMonedaDelInforme;
			}
			set
			{
				_ListaMonedaDelInforme = value;
			}
		}
		#endregion //Propiedades

		#region Constructores
		public clsCxCPendientesEntreFechasViewModel() {
			FechaDesde = LibDate.Today();
			FechaHasta = LibDate.Today();
			MostrarContacto = false;
			MonedaDelInforme = Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal;
			TipoTasaDeCambio = Saw.Lib.eTasaDeCambioParaImpresion.DelDia;
			LlenarEnumerativosMonedas();
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

		public bool IsVisibleTipoTasaDeCambio
		{
			get { return MonedaDelInforme != Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal; }
		}

		private void LlenarEnumerativosMonedas()
		{
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

	} //End of class clsCxCPendientesEntreFechasViewModel

} //End of namespace Galac.Adm.Uil.Venta

