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
		public const string MonedasAgrupadasPorPropertyName = "MonedasAgrupadasPor";
		public const string IsVisibleMonedasAgrupadasPorPropertyName = "IsVisibleMonedasAgrupadasPor";
		#endregion //Constantes

		#region Variables
		private DateTime _FechaDesde;
		private DateTime _FechaHasta;
		private bool _MostrarContacto;
		private Saw.Lib.eMonedaParaImpresion _MonedaDelInformeAsEnum;
		private Saw.Lib.eMonedaParaImpresion _MonedasAgrupadasPorAsEnum;

		private ObservableCollection<Saw.Lib.eMonedaParaImpresion> _ListaMonedaDelInforme = new ObservableCollection<Saw.Lib.eMonedaParaImpresion>();
		private ObservableCollection<Saw.Lib.eMonedaParaImpresion> _ListaMonedasAgrupadasPor = new ObservableCollection<Saw.Lib.eMonedaParaImpresion>();
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
					MonedasAgrupadasPor = Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal;
					RaisePropertyChanged(MonedaDelInformePropertyName);
					RaisePropertyChanged(IsVisibleMonedasAgrupadasPorPropertyName);
				}
			}
		}

		public Saw.Lib.eMonedaParaImpresion MonedasAgrupadasPor
		{
			get
			{
				return _MonedasAgrupadasPorAsEnum;
			}
			set
			{
				if (_MonedasAgrupadasPorAsEnum != value) {
					_MonedasAgrupadasPorAsEnum = value;
					RaisePropertyChanged(MonedasAgrupadasPorPropertyName);
				}
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

		public ObservableCollection<Saw.Lib.eMonedaParaImpresion> ListaMonedasAgrupadasPor
		{
			get
			{
				return _ListaMonedasAgrupadasPor;
			}
			set
			{
				_ListaMonedasAgrupadasPor = value;
			}
		}
		#endregion //Propiedades

		#region Constructores
		public clsCxCPendientesEntreFechasViewModel() {
			FechaDesde = LibDate.Today();
			FechaHasta = LibDate.Today();
			MostrarContacto = false;
			MonedaDelInforme = Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal;
			MonedasAgrupadasPor = Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal;
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

		public bool IsVisibleMonedasAgrupadasPor
		{
			get { return MonedaDelInforme != Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal; }
		}

		private void LlenarEnumerativosMonedas()
		{
			ListaMonedaDelInforme.Clear();
			ListaMonedasAgrupadasPor.Clear();
			if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
				ListaMonedaDelInforme.Add(Saw.Lib.eMonedaParaImpresion.EnBolivares);
				ListaMonedasAgrupadasPor.Add(Saw.Lib.eMonedaParaImpresion.EnBolivares);
			}
			else if (LibDefGen.ProgramInfo.IsCountryPeru()) {
				ListaMonedaDelInforme.Add(Saw.Lib.eMonedaParaImpresion.EnSoles);
				ListaMonedasAgrupadasPor.Add(Saw.Lib.eMonedaParaImpresion.EnSoles);
			}
			ListaMonedaDelInforme.Add(Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal);
			ListaMonedasAgrupadasPor.Add(Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal);
		}
		#endregion //Código Programador

	} //End of class clsCxCPendientesEntreFechasViewModel

} //End of namespace Galac.Adm.Uil.Venta

