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
using Galac.Adm.Brl.Venta.Reportes;

namespace Galac.Adm.Uil.Venta.Reportes {
    public class clsCxCEntreFechasViewModel : LibInputRptViewModelBase<CxC> {
        #region Variables
        eInformeAgruparPor _AgruparPor;
        eMonedaDelInformeMM _MonedaDelInforme;
        eTasaDeCambioParaImpresion _TasaDeCambio;
        #endregion //Variables
        #region Propiedades
        public override string DisplayName { get { return "CxC entre fechas"; } }
        public LibXmlMemInfo AppMemoryInfo { get; set; }
        public LibXmlMFC Mfc { get; set; }
        public override bool IsSSRS => false;
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public eInformeStatusCXC_CXP StatusCxC { get; set; }
        public ObservableCollection<eInformeStatusCXC_CXP> ListaStatusCxC { get; set; }
        public eInformeAgruparPor AgruparPor {
            get { return _AgruparPor; }
            set {
                if (_AgruparPor != value) {
                    _AgruparPor = value;
                    RaisePropertyChanged(() => AgruparPor);
                    RaisePropertyChanged(() => IsVisibleSectoresDeNegocio);
                    RaisePropertyChanged(() => IsVisibleZonasDeCobranzas);
                }
            }
        }
        public ObservableCollection<eInformeAgruparPor> ListaAgruparPor { get; set; }
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
        public bool IncluirContacto { get; set; }
        public bool IncluirInfoAdicional { get; set; }
        public bool IncluirNroComprobanteContable { get; set; }
        public string SectorDeNegocio { get; set; }
        public ObservableCollection<string> ListaSectoresDeNegocio { get; set; }
        public string ZonaDeCobranza { get; set; }
        public ObservableCollection<string> ListaZonasDeCobranza { get; set; }
        public string Moneda { get; set; }
        public ObservableCollection<string> ListaMonedasActivas { get; set; }
        public bool IsVisibleSectoresDeNegocio { get { return AgruparPor == eInformeAgruparPor.SectorDeNegocio; } }
        public bool IsVisibleZonasDeCobranzas { get { return AgruparPor == eInformeAgruparPor.ZonaDeCobranza; } }
        public bool IsVisibleMonedasActivas { get { return MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa; } }
        public bool IsVisibleIncluirNroCC { get { return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "CaracteristicaDeContabilidadActiva"); } }
        public bool IsVisibleTasaDeCambio { get { return MonedaDelInforme == eMonedaDelInformeMM.EnBolivares || MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa; } }
        #endregion //Propiedades
        #region Constructores
        public clsCxCEntreFechasViewModel() {
            FechaDesde = LibDate.DateFromMonthAndYear(LibDate.Today().Month, LibDate.Today().Year, true);
            FechaHasta = LibDate.Today();
            AgruparPor = eInformeAgruparPor.NoAgurpar;
            LlenarListaStatusCxC();
            LlenarListaAgruparPor();
            LlenarListaZonasDeCobranza();
            LlenarListaSectorDeNegocio();
            LlenarListaMonedaDelInforme();
            LlenarListaMonedasActivas();
            LlentarListaTasaDeCambio();
            RaisePropertyChanged(() => IsVisibleIncluirNroCC);
            RaisePropertyChanged(() => IsVisibleTasaDeCambio);
        }
        #endregion //Constructores

        #region Metodos Generados
        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsCXCNav();
        }
        #endregion //Metodos Generados

        void LlenarListaStatusCxC() {
            ListaStatusCxC = new ObservableCollection<eInformeStatusCXC_CXP>();
            ListaStatusCxC.Clear();
            ListaStatusCxC.Add(eInformeStatusCXC_CXP.Todos);
            ListaStatusCxC.Add(eInformeStatusCXC_CXP.PorCancelar);
            ListaStatusCxC.Add(eInformeStatusCXC_CXP.Cancelado);
            ListaStatusCxC.Add(eInformeStatusCXC_CXP.ChequeDevuelto);
            ListaStatusCxC.Add(eInformeStatusCXC_CXP.Abonado);
            ListaStatusCxC.Add(eInformeStatusCXC_CXP.Anulado);
            ListaStatusCxC.Add(eInformeStatusCXC_CXP.Refinanciado);
            StatusCxC = eInformeStatusCXC_CXP.Todos;
        }

        void LlenarListaAgruparPor() {
            ListaAgruparPor = new ObservableCollection<eInformeAgruparPor>();
            ListaAgruparPor.Clear();
            ListaAgruparPor.Add(eInformeAgruparPor.NoAgurpar);
            ListaAgruparPor.Add(eInformeAgruparPor.SectorDeNegocio);
            ListaAgruparPor.Add(eInformeAgruparPor.ZonaDeCobranza);
            AgruparPor = eInformeAgruparPor.NoAgurpar;
        }

        void LlenarListaZonasDeCobranza() {
            ListaZonasDeCobranza = ((ICxCInformes)new clsCxCRpt()).ListaDeZonasDeCobranzaParaInformes();
            ZonaDeCobranza = "TODAS";
        }

        void LlenarListaSectorDeNegocio() {
            ListaSectoresDeNegocio = ((ICxCInformes)new clsCxCRpt()).ListaDeSectoresDeNegocioParaInformes();
            SectorDeNegocio = "TODOS";
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
    } //End of class clsCxCEntreFechasViewModel

} //End of namespace Galac.Adm.Uil.Venta