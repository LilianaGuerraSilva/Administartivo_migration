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

namespace Galac.Adm.Uil.Venta.Reportes {

	public class clsCxCPendientesEntreFechasViewModel : LibInputRptViewModelBase<CxC> {
		#region Variables
		DateTime _FechaDesde;
		DateTime _FechaHasta;
		bool _UsaContacto;
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
					RaisePropertyChanged("FechaDesde");
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
					RaisePropertyChanged("FechaHasta");
				}
			}
		}

		public bool UsaContacto {
			get {
				return _UsaContacto;
			}
			set {
				if (_UsaContacto != value) {
					_UsaContacto = value;
					RaisePropertyChanged("UsaContacto");
				}
			}

		}
		#endregion //Propiedades

		#region Constructores

		public clsCxCPendientesEntreFechasViewModel() {
			#region Codigo Ejemplo
			FechaDesde = LibDate.Today();
			FechaHasta = LibDate.Today();
			UsaContacto = false;
			#endregion //Codigo Ejemplo
		}
		#endregion //Constructores
		#region Metodos Generados

		protected override ILibBusinessSearch GetBusinessComponent() {
			return new clsCXCNav();
		}
		#endregion //Metodos Generados

	} //End of class clsCxCPendientesEntreFechasViewModel

} //End of namespace Galac.Adm.Uil.Venta

