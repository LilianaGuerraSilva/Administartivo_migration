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
using Galac.Saw.Lib;

namespace Galac.Adm.Uil.Venta.Reportes {

	public class clsCxCPendientesEntreFechasViewModel : LibInputRptViewModelBase<CxC> {
		#region Variables
		private DateTime _FechaDesde;
		private DateTime _FechaHasta;
		private bool _MostrarContacto;
		private eMonedaDelInformeMM _MonedaDelInformeAsEnum;
		private eTasaDeCambioParaImpresion _TipoTasaDeCambioAsEnum;
		private ObservableCollection<eMonedaDelInformeMM> _ListaMonedaDelInforme = new ObservableCollection<eMonedaDelInformeMM>();
		#endregion //Variables

		#region Propiedades
		public override string DisplayName { get { return "CxC Pendientes entre Fechas"; } }
		public LibXmlMemInfo AppMemoryInfo { get; set; }
		public LibXmlMFC Mfc { get; set; }
		public override bool IsSSRS => false;

		public DateTime FechaDesde {
			get { return _FechaDesde; }
			set {
				if (_FechaDesde != value) {
					_FechaDesde = value;
					RaisePropertyChanged(() => FechaDesde);
				}
			}
		}

		public DateTime FechaHasta {
			get { return _FechaHasta; }
			set {
				if (_FechaHasta != value) {
					_FechaHasta = value;
					RaisePropertyChanged(() => FechaHasta);
				}
			}
		}

		public bool MostrarContacto {
			get { return _MostrarContacto; }
			set {
				if (_MostrarContacto != value) {
					_MostrarContacto = value;
					RaisePropertyChanged(() => MostrarContacto);
				}
			}
		}

		public eMonedaDelInformeMM MonedaDelInforme {
			get { return _MonedaDelInformeAsEnum; }
			set {
				if (_MonedaDelInformeAsEnum != value) {
					_MonedaDelInformeAsEnum = value;
					TipoTasaDeCambio = eTasaDeCambioParaImpresion.DelDia;
					RaisePropertyChanged(() => MonedaDelInforme);
					RaisePropertyChanged(() => IsVisibleMonedasActivas);
					RaisePropertyChanged(() => IsVisibleTipoTasaDeCambio);
				}
			}
		}

		public eTasaDeCambioParaImpresion TipoTasaDeCambio {
			get { return _TipoTasaDeCambioAsEnum; }
			set {
				if (_TipoTasaDeCambioAsEnum != value) {
					_TipoTasaDeCambioAsEnum = value;
					RaisePropertyChanged(() => TipoTasaDeCambio);
				}
			}
		}

		public eTasaDeCambioParaImpresion[] ArrayTiposTasaDeCambio {
			get { return LibEnumHelper<eTasaDeCambioParaImpresion>.GetValuesInArray(); }
		}

		public ObservableCollection<eMonedaDelInformeMM> ListaMonedaDelInforme {
			get { return _ListaMonedaDelInforme; }
			set { _ListaMonedaDelInforme = value; }
		}

		public ObservableCollection<string> ListaMonedasActivas { get; set; }
		public string Moneda { get; set; }
		#endregion //Propiedades

		#region Constructores
		public clsCxCPendientesEntreFechasViewModel() {
			FechaDesde = LibDate.DateFromMonthAndYear(LibDate.Today().Month, LibDate.Today().Year, true);
			FechaHasta = LibDate.Today();
			MostrarContacto = false;
			MonedaDelInforme = eMonedaDelInformeMM.EnMonedaOriginal;
			TipoTasaDeCambio = eTasaDeCambioParaImpresion.DelDia;
			LlenarEnumerativosMonedas();
			LlenarListaMonedasActivas();
		}
		#endregion //Constructores

		#region Metodos Generados
		protected override ILibBusinessSearch GetBusinessComponent() {
			return new clsCXCNav();
		}
		#endregion //Metodos Generados

		#region Código Programador
		public bool IsVisibleTipoTasaDeCambio { get { return MonedaDelInforme == eMonedaDelInformeMM.EnBolivares || MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa; } }
		public bool IsVisibleMonedasActivas { get { return MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa; } }


		private void LlenarEnumerativosMonedas() {
			ListaMonedaDelInforme = new ObservableCollection<eMonedaDelInformeMM>();
			ListaMonedaDelInforme.Clear();
			ListaMonedaDelInforme.Add(eMonedaDelInformeMM.EnBolivares);
			ListaMonedaDelInforme.Add(eMonedaDelInformeMM.EnMonedaOriginal);
			ListaMonedaDelInforme.Add(eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa);
			MonedaDelInforme = eMonedaDelInformeMM.EnMonedaOriginal;
		}
		void LlenarListaMonedasActivas() {
			ListaMonedasActivas = new Galac.Saw.Lib.clsLibSaw().ListaDeMonedasActivasParaInformes(false);
			if (ListaMonedasActivas.Count > 0) {
				Moneda = ListaMonedasActivas[0];
			}
		}


		#endregion //Código Programador

	} //End of class clsCxCPendientesEntreFechasViewModel

} //End of namespace Galac.Adm.Uil.Venta