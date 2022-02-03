using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.Banco;
using Galac.Adm.Brl.Banco;
using Galac.Adm.Uil.Banco.ViewModel;
using LibGalac.Aos.UI.Mvvm.Validation;
using System.ComponentModel.DataAnnotations;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Uil.Banco.Reportes {

    public class clsSaldosBancariosViewModel : LibInputRptViewModelBase<CuentaBancaria> {
        #region Constantes
        public const string FechaDesdePropertyName = "FechaDesde";
        public const string FechaHastaPropertyName = "FechaHasta";
        public const string SoloCuentasActivasPropertyName = "SoloCuentasActivas";
        #endregion //Constantes

        #region Variables
        private DateTime _FechaDesde;
        private DateTime _FechaHasta;
        bool _SoloCuentasActivas;
        #endregion //Variables

        #region Propiedades
        public override string DisplayName {
            get { return "Saldos Bancarios";}
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

		public bool SoloCuentasActivas {
			get {
				return _SoloCuentasActivas;
			}
			set {
				if (_SoloCuentasActivas != value) {
					_SoloCuentasActivas = value;
					RaisePropertyChanged(SoloCuentasActivasPropertyName);
				}
			}
		}
		#endregion //Propiedades

		#region Constructores
		public clsSaldosBancariosViewModel() {
			FechaDesde = LibDate.Today();
			FechaHasta = LibDate.Today();
			SoloCuentasActivas = true;
		}
		#endregion //Constructores

		#region Metodos Generados
		protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsCuentaBancariaNav();
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
		#endregion //Código Programador

	} //End of class clsSaldosBancariosViewModel

} //End of namespace Galac.Adm.Uil.Banco

