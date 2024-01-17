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
using System.Collections.ObjectModel;
using LibGalac.Aos.UI.Mvvm.Validation;
using System.ComponentModel.DataAnnotations;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsResumenDiarioDeVentasEntreFechasViewModel : LibInputRptViewModelBase<Factura> {
        #region Constantes
        public const string FechaDesdePropertyName = "FechaDesde";
        public const string FechaHastaPropertyName = "FechaHasta";
        public const string AgruparPorMaquinaFiscalPropertyName = "AgruparPorMaquinaFiscal";
        public const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        public const string IsEnabledConsecutivoMaquinaFiscalPropertyName = "IsEnabledConsecutivoMaquinaFiscal";
        public const string ConsecutivoMaquinaFiscalPropertyName = "ConsecutivoMaquinaFiscal";
        #endregion //Constantes

        #region Variables
        private DateTime _FechaDesde;
        private DateTime _FechaHasta;
        bool _AgruparPorMaquinaFiscal;
        private eCantidadAImprimir _CantidadAImprimirAsEnum;
        private string _ConsecutivoMaquinaFiscal;

        private FkMaquinaFiscalViewModel _ConexionConsecutivoMaquinaFiscal = null;
        #endregion //Variables

        #region Propiedades
        public override string DisplayName {
            get { return "Resumen Diario de Ventas entre Fechas"; }
		}

		public LibXmlMemInfo AppMemoryInfo { get; set; }

		public LibXmlMFC Mfc { get; set; }

		public override bool IsSSRS => throw new NotImplementedException();

		public RelayCommand<string> ChooseConsecutivoMaquinaFiscalCommand
		{
			get;
			private set;
		}

		[LibCustomValidation("FechaDesdeValidating")]
		public DateTime FechaDesde
		{
			get
			{
				return _FechaDesde;
			}
			set
			{
				if (_FechaDesde != value) {
					_FechaDesde = value;
					RaisePropertyChanged(FechaDesdePropertyName);
				}
			}
		}

		[LibCustomValidation("FechaHastaValidating")]
		public DateTime FechaHasta
		{
			get
			{
				return _FechaHasta;
			}
			set
			{
				if (_FechaHasta != value) {
					_FechaHasta = value;
					RaisePropertyChanged(FechaHastaPropertyName);
				}
			}
		}

		public bool AgruparPorMaquinaFiscal
		{
			get
			{
				return _AgruparPorMaquinaFiscal;
			}
			set
			{
				if (_AgruparPorMaquinaFiscal != value) {
					_AgruparPorMaquinaFiscal = value;
					CantidadAImprimir = eCantidadAImprimir.All;
					RaisePropertyChanged(AgruparPorMaquinaFiscalPropertyName);
				}
			}
		}

		public eCantidadAImprimir CantidadAImprimir
		{
			get
			{
				return _CantidadAImprimirAsEnum;
			}
			set
			{
				if (_CantidadAImprimirAsEnum != value) {
					_CantidadAImprimirAsEnum = value;
					ConsecutivoMaquinaFiscal = string.Empty;
					RaisePropertyChanged(CantidadAImprimirPropertyName);
					RaisePropertyChanged(IsEnabledConsecutivoMaquinaFiscalPropertyName);
				}
			}
		}

		[LibCustomValidation("ConsecutivoMaquinaFiscalValidating")]
		public string ConsecutivoMaquinaFiscal
		{
			get
			{
				return _ConsecutivoMaquinaFiscal;
			}
			set
			{
				if (_ConsecutivoMaquinaFiscal != value) {
					_ConsecutivoMaquinaFiscal = value;
					RaisePropertyChanged(ConsecutivoMaquinaFiscalPropertyName);
				}
			}
		}

		public FkMaquinaFiscalViewModel ConexionConsecutivoMaquinaFiscal
		{
			get
			{
				return _ConexionConsecutivoMaquinaFiscal;
			}
			set
			{
				if (_ConexionConsecutivoMaquinaFiscal != value) {
					_ConexionConsecutivoMaquinaFiscal = value;
				}
				if (_ConexionConsecutivoMaquinaFiscal != null) {
					ConsecutivoMaquinaFiscal = _ConexionConsecutivoMaquinaFiscal.ConsecutivoMaquinaFiscal;
				}
				if (_ConexionConsecutivoMaquinaFiscal == null) {
					ConsecutivoMaquinaFiscal = string.Empty;
				}
			}
		}

		public eCantidadAImprimir[] ArrayCantidadAImprimir
		{
			get
			{
				return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray();
			}
		}
		#endregion //Propiedades

		#region Constructores
		public clsResumenDiarioDeVentasEntreFechasViewModel() {
			FechaDesde = LibDate.Today();
			FechaHasta = LibDate.Today();
			AgruparPorMaquinaFiscal = false;
			CantidadAImprimir = eCantidadAImprimir.All;
			ConsecutivoMaquinaFiscal = string.Empty;
		}

		protected override void InitializeCommands()
		{
			base.InitializeCommands();
			ChooseConsecutivoMaquinaFiscalCommand = new RelayCommand<string>(ExecuteChooseConsecutivoMaquinaFiscalCommand);
		}

		private void ExecuteChooseConsecutivoMaquinaFiscalCommand(string valNombre) {
			try {
				if (valNombre == null) {
					valNombre = string.Empty;
				}
				LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("ConsecutivoMaquinaFiscal", valNombre);
				LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania",LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
				ConexionConsecutivoMaquinaFiscal = null;
				ConexionConsecutivoMaquinaFiscal = ChooseRecord<FkMaquinaFiscalViewModel>("Máquina Fiscal", vDefaultCriteria, vFixedCriteria, string.Empty);
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
		protected override ILibBusinessSearch GetBusinessComponent()
		{
			return new clsFacturaNav();
		}
		#endregion //Metodos Generados

		#region Código Programador
		private ValidationResult FechaDesdeValidating()
		{
			ValidationResult vResult = ValidationResult.Success;
			if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDesde, false, eAccionSR.InformesPantalla)) {
				vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha Desde"));
			}
			else if (LibDate.F1IsGreaterThanF2(FechaDesde, FechaHasta)) {
				vResult = new ValidationResult("La fecha desde no puede ser mayor a la fecha hasta");
			}
			return vResult;
		}

		private ValidationResult FechaHastaValidating()
		{
			ValidationResult vResult = ValidationResult.Success;
			if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaHasta, false, eAccionSR.InformesPantalla)) {
				vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha Desde"));
			}
			else if (LibDate.F1IsLessThanF2(FechaHasta, FechaDesde)) {
				vResult = new ValidationResult("La fecha hasta no puede ser menor a la fecha desde");
			}
			return vResult;
		}

		private ValidationResult ConsecutivoMaquinaFiscalValidating()
		{
			ValidationResult vResult = ValidationResult.Success;
			if (LibString.IsNullOrEmpty(ConsecutivoMaquinaFiscal) && CantidadAImprimir == eCantidadAImprimir.One) {
				vResult = new ValidationResult("El consecutivo de la Máquina Fiscal no puede estar en blanco");
			}
			return vResult;
		}

		public bool IsEnabledConsecutivoMaquinaFiscal
		{
			get { return CantidadAImprimir == eCantidadAImprimir.One; }
		}
		#endregion //Código Programador

	} //End of class clsResumenDiarioDeVentasEntreFechasViewModel

} //End of namespace Galac.Adm.Uil.Venta

