using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Cib;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Uil.GestionCompras.ViewModel;
using Galac.Saw.Lib;
using System.Collections.ObjectModel;
using Galac.Adm.Brl.GestionCompras.Reportes;

namespace Galac.Adm.Uil.GestionCompras.Reportes {
    public class clsCxPEntreFechasViewModel : LibInputRptViewModelBase<CxP> {
        #region Variables
        eMonedaDelInformeMM _MonedaDelInforme;
        eTasaDeCambioParaImpresion _TasaDeCambio;
        #endregion //Variables
        #region Propiedades
        public override string DisplayName { get { return "CxP entre Fechas"; } }
        public override bool IsSSRS { get { return false; } }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public eInformeStatusCXC_CXP StatusCxP { get; set; }
        public ObservableCollection<eInformeStatusCXC_CXP> ListaStatusCxP { get; set; }
        public eMonedaDelInformeMM MonedaDelInforme {
            get { return _MonedaDelInforme; }
            set {
                if (_MonedaDelInforme != value) {
                    _MonedaDelInforme = value;
                    RaisePropertyChanged(() => MonedaDelInforme);
                    RaisePropertyChanged(() => IsVisibleMonedasActivas);
                    RaisePropertyChanged(() => IsVisibleTasaDeCambio);
                }
            }
        }
        public eTasaDeCambioParaImpresion TasaDeCambio {
            get { return _TasaDeCambio; }
            set {
                if (_TasaDeCambio != value) {
                    _TasaDeCambio = value;
                    RaisePropertyChanged(() => TasaDeCambio);
                }
            }
        }
        public ObservableCollection<eTasaDeCambioParaImpresion> ListaTasaDeCambio { get; set; }
        public ObservableCollection<eMonedaDelInformeMM> ListaMonedaDelInforme { get; set; }
        public bool IncluirInfoAdicional { get; set; }
        public bool IncluirNroComprobanteContable { get; set; }
        public string Moneda { get; set; }
        public ObservableCollection<string> ListaMonedasActivas { get; set; }
        public bool IsVisibleMonedasActivas { get { return MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa; } }
        public bool IsVisibleIncluirNroCC { get { return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "CaracteristicaDeContabilidadActiva"); } }
        public bool IsVisibleTasaDeCambio { get { return MonedaDelInforme == eMonedaDelInformeMM.EnBolivares || MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa; } }

        #endregion Propiedades
        #region Constructores
        public clsCxPEntreFechasViewModel() {
            FechaDesde = LibDate.DateFromMonthAndYear(LibDate.Today().Month, LibDate.Today().Year, true);
            FechaHasta = LibDate.Today();
            LlenarListaStatusCxP();
            LlenarListaMonedaDelInforme();
            LlenarListaMonedasActivas();
            LlentarListaTasaDeCambio();
            RaisePropertyChanged(() => IsVisibleIncluirNroCC);
            RaisePropertyChanged(() => IsVisibleTasaDeCambio);
        }

        #endregion //Constructores
        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsCxPNav();
        }

        void LlenarListaStatusCxP() {
            ListaStatusCxP = new ObservableCollection<eInformeStatusCXC_CXP>();
            ListaStatusCxP.Clear();
            ListaStatusCxP.Add(eInformeStatusCXC_CXP.Todos);
            ListaStatusCxP.Add(eInformeStatusCXC_CXP.PorCancelar);
            ListaStatusCxP.Add(eInformeStatusCXC_CXP.Cancelado);
            ListaStatusCxP.Add(eInformeStatusCXC_CXP.ChequeDevuelto);
            ListaStatusCxP.Add(eInformeStatusCXC_CXP.Abonado);
            ListaStatusCxP.Add(eInformeStatusCXC_CXP.Anulado);
            ListaStatusCxP.Add(eInformeStatusCXC_CXP.Refinanciado);
            StatusCxP = eInformeStatusCXC_CXP.Todos;
        }

        void LlenarListaMonedaDelInforme() {
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

        void LlentarListaTasaDeCambio() {
            ListaTasaDeCambio = new ObservableCollection<eTasaDeCambioParaImpresion>();
            ListaTasaDeCambio.Clear();
            ListaTasaDeCambio.Add(eTasaDeCambioParaImpresion.Original);
            ListaTasaDeCambio.Add(eTasaDeCambioParaImpresion.DelDia);
            TasaDeCambio = eTasaDeCambioParaImpresion.Original;
        }

    } //End of class clsCuentasPorPagarEntreFechasViewModel

} //End of namespace Galac.Dbo.Uil.ComponenteNoEspecificado