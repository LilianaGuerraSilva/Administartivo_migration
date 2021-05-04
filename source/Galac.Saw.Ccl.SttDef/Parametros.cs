using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Reflection;
using System.ComponentModel;

namespace Galac.Saw.Ccl.SttDef {
    public class Parametros {
        AnticipoStt _ParametrosAnticipoStt;
        BancosStt _ParametrosBancosStt;
        CamposDefiniblesStt _ParametrosCamposDefiniblesStt;
        ClienteStt _ParametrosClienteStt;
        CobranzasStt _ParametrosCobranzasStt;
        ComisionesStt _ParametrosComisionesStt;
        CompaniaStt _ParametrosCompaniaStt;
        ComprasStt _ParametrosComprasStt;
        CotizacionStt _ParametrosCotizacionStt;
        CxPProveedorPagosStt _ParametrosCxPProveedorPagosStt;
        FacturacionContinuacionStt _ParametrosFacturacionContinuacionStt;
        FacturacionStt _ParametrosFacturacionStt;
        GeneralStt _ParametrosGeneralStt;
        ImpresiondeFacturaStt _ParametrosImpresiondeFacturaStt;
        InventarioStt _ParametrosInventarioStt;
        MetododecostosStt _ParametrosMetododecostosStt;
        ModeloDeFacturaStt _ParametrosModeloDeFacturaStt;
        MonedaStt _ParametrosMonedaStt;
        MovimientoBancarioStt _ParametrosMovimientoBancarioStt;
        NotaEntradaSalidaStt _ParametrosNotaEntradaSalidaStt;
        NotaEntregaStt _ParametrosNotaEntregaStt;
        NotasDebitoCreditoEntregaStt _ParametrosNotasDebitoCreditoEntregaStt;
        PlanillaDeIVAStt _ParametrosPlanillaDeIVAStt;
        ProcesosStt _ParametrosProcesosStt;
        RetencionISLRStt _ParametrosRetencionISLRStt;
        RetencionIVAStt _ParametrosRetencionIVAStt;
        VendedorStt _ParametrosVendedorStt;
        VerificadorDePreciosStt _ParametrosVerificadorDePreciosStt;
        ImagenesComprobantesRetencionStt _ParametrosImagenesComprobantesRetencionStt;

        public CotizacionStt ParametrosCotizacionStt {
            get { return _ParametrosCotizacionStt; }
            set { _ParametrosCotizacionStt = value; }
        }

        public AnticipoStt ParametrosAnticipoStt {
            get { return _ParametrosAnticipoStt; }
            set { _ParametrosAnticipoStt = value; }
        }

        public BancosStt ParametrosBancosStt {
            get { return _ParametrosBancosStt; }
            set { _ParametrosBancosStt = value; }
        }

        public CamposDefiniblesStt ParametrosCamposDefiniblesStt {
            get { return _ParametrosCamposDefiniblesStt; }
            set { _ParametrosCamposDefiniblesStt = value; }
        }

        public ClienteStt ParametrosClienteStt {
            get { return _ParametrosClienteStt; }
            set { _ParametrosClienteStt = value; }
        }

        public CobranzasStt ParametrosCobranzasStt {
            get { return _ParametrosCobranzasStt; }
            set { _ParametrosCobranzasStt = value; }
        }

        public ComisionesStt ParametrosComisionesStt {
            get { return _ParametrosComisionesStt; }
            set { _ParametrosComisionesStt = value; }
        }

        public CompaniaStt ParametrosCompaniaStt {
            get { return _ParametrosCompaniaStt; }
            set { _ParametrosCompaniaStt = value; }
        }

        public ComprasStt ParametrosComprasStt {
            get { return _ParametrosComprasStt; }
            set { _ParametrosComprasStt = value; }
        }

        public CxPProveedorPagosStt ParametrosCxPProveedorPagosStt {
            get { return _ParametrosCxPProveedorPagosStt; }
            set { _ParametrosCxPProveedorPagosStt = value; }
        }

        public FacturacionContinuacionStt ParametrosFacturacionContinuacionStt {
            get { return _ParametrosFacturacionContinuacionStt; }
            set { _ParametrosFacturacionContinuacionStt = value; }
        }

        public FacturacionStt ParametrosFacturacionStt {
            get { return _ParametrosFacturacionStt; }
            set { _ParametrosFacturacionStt = value; }
        }

        public GeneralStt ParametrosGeneralStt {
            get { return _ParametrosGeneralStt; }
            set { _ParametrosGeneralStt = value; }
        }

        public ImpresiondeFacturaStt ParametrosImpresiondeFacturaStt {
            get { return _ParametrosImpresiondeFacturaStt; }
            set { _ParametrosImpresiondeFacturaStt = value; }
        }

        public InventarioStt ParametrosInventarioStt {
            get { return _ParametrosInventarioStt; }
            set { _ParametrosInventarioStt = value; }
        }

        public MetododecostosStt ParametrosMetododecostosStt {
            get { return _ParametrosMetododecostosStt; }
            set { _ParametrosMetododecostosStt = value; }
        }

        public ModeloDeFacturaStt ParametrosModeloDeFacturaStt {
            get { return _ParametrosModeloDeFacturaStt; }
            set { _ParametrosModeloDeFacturaStt = value; }
        }

        public MonedaStt ParametrosMonedaStt {
            get { return _ParametrosMonedaStt; }
            set { _ParametrosMonedaStt = value; }
        }

        public MovimientoBancarioStt ParametrosMovimientoBancarioStt {
            get { return _ParametrosMovimientoBancarioStt; }
            set { _ParametrosMovimientoBancarioStt = value; }
        }

        public NotaEntradaSalidaStt ParametrosNotaEntradaSalidaStt {
            get { return _ParametrosNotaEntradaSalidaStt; }
            set { _ParametrosNotaEntradaSalidaStt = value; }
        }

        public NotaEntregaStt ParametrosNotaEntregaStt {
            get { return _ParametrosNotaEntregaStt; }
            set { _ParametrosNotaEntregaStt = value; }
        }

        public NotasDebitoCreditoEntregaStt ParametrosNotasDebitoCreditoEntregaStt {
            get { return _ParametrosNotasDebitoCreditoEntregaStt; }
            set { _ParametrosNotasDebitoCreditoEntregaStt = value; }
        }

        public PlanillaDeIVAStt ParametrosPlanillaDeIVAStt {
            get { return _ParametrosPlanillaDeIVAStt; }
            set { _ParametrosPlanillaDeIVAStt = value; }
        }

        public ProcesosStt ParametrosProcesosStt {
            get { return _ParametrosProcesosStt; }
            set { _ParametrosProcesosStt = value; }
        }

        public RetencionISLRStt ParametrosRetencionISLRStt {
            get { return _ParametrosRetencionISLRStt; }
            set { _ParametrosRetencionISLRStt = value; }
        }

        public RetencionIVAStt ParametrosRetencionIVAStt {
            get { return _ParametrosRetencionIVAStt; }
            set { _ParametrosRetencionIVAStt = value; }
        }

        public VendedorStt ParametrosVendedorStt {
            get { return _ParametrosVendedorStt; }
            set { _ParametrosVendedorStt = value; }
        }

        public VerificadorDePreciosStt ParametrosVerificadorDePreciosStt {
            get { return _ParametrosVerificadorDePreciosStt; }
            set { _ParametrosVerificadorDePreciosStt = value; }
        }


        public ImagenesComprobantesRetencionStt ParametrosImagenesComprobantesRetencionStt {
            get { return _ParametrosImagenesComprobantesRetencionStt; }
            set { _ParametrosImagenesComprobantesRetencionStt = value; }
        }

    }

    public class Module {
        public string DisplayName { get; set; }
        public GroupCollection Groups { get; set; }
        public Group SelectedItem {
            get { return Groups.SelectedItem; }
            set { Groups.SelectedItem = value; }
        }

        public Module()
            : this("", new GroupCollection()) {
        }

        public Module(string initDisplayName, GroupCollection initGroups) {
            DisplayName = initDisplayName;
            Groups = initGroups;
        }
    }

    public class Group {
        public string DisplayName { get; set; }
        public object Content { get; set; }

        public Group()
            : this("", null) {
        }

        public Group(string initDisplayName, object initContent) {
            DisplayName = initDisplayName;
            Content = initContent;
        }
    }

    public class GroupCollection : ObservableCollection<Group> {
        private Group _SelectedItem;
        public Group SelectedItem {
            get {
                if (_SelectedItem == null && Items.Count > 0) {
                    _SelectedItem = Items[0];
                }
                return _SelectedItem;
            }
            set {
                _SelectedItem = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("SelectedItem"));
            }
        }
    }
}
