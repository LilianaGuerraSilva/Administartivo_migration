using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;
using Galac.Comun.Brl.TablasGen;
using Galac.Saw.Ccl.SttDef;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Uil.TablasGen.ViewModel;
using Galac.Saw.Lib;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.Uil;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Windows.Media;
using Galac.Saw.Ccl.Cliente;
using Galac.Adm.Ccl.CajaChica;
using Galac.Adm.IntegracionMS.Venta;
using Galac.Saw.Ccl.Tablas;
using System.Linq;
using Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal;
using Galac.Adm.Ccl.DispositivosExternos;
using System.IO;
using System.Windows;
using System.Text.RegularExpressions;
using LibGalac.Aos.DefGen;
using System.ComponentModel.DataAnnotations;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Comun.Ccl.TablasGen;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Uil.Venta.ViewModel {

    internal class CobroCobroMediosElectonicosVPOS {
        internal decimal MontoTransaccion { get; set; }
        internal string InfoAdicional { get; set; }
        internal eBancoPM BancoTransaccion { get; set; }
        internal string NumReferencia { get; set; }
        internal int BancoTrans { get; set; }
        internal string CodigoFormaDelCobro { get; set; }
        internal string MonedaTransaccion { get; set; }
    }
    public class CobroRapidoMultimonedaViewModel: CobroRapidoVzlaViewModelBase {
        #region Variables y Constantes
        private const string NombreDeMonedaLocalPropertyName = "NombreDeMonedaLocal";
        private const string NombreDeMonedaDivisaPropertyName = "NombreMonedaDivisa";
        private const string TotalFacturaEnDivisasPropertyName = "TotalFacturaEnDivisas";
        private const string CambioAMonedaLocalPropertyName = "CambioAMonedaLocal";
        private const string EfectivoEnMonedaLocalPropertyName = "EfectivoEnMonedaLocal";
        private const string EfectivoEnDivisasPropertyName = "EfectivoEnDivisas";
        private const string TarjetaUnoPropertyName = "TarjetaUno";
        private const string TarjetaDosPropertyName = "TarjetaDos";
        private const string TransferenciaEnMonedaLocalPropertyName = "TransferenciaEnMonedaLocal";
        private const string TransferenciaEnDivisasPropertyName = "TransferenciaEnDivisas";
        private const string VueltoEnMonedaLocalPropertyName = "VueltoEnMonedaLocal";
        private const string VueltoC2pPropertyName = "VueltoC2p";
        private const string TotalMediosElectronicosPropertyName = "TotalMediosElectronicos";
        private const string TotalMediosElectronicosMEPropertyName = "TotalMediosElectronicosME";
        private const string VueltoEnDivisasPropertyName = "VueltoEnDivisas";
        private const string MontoRestantePorPagarEnDivisasPropertyName = "MontoRestantePorPagarEnDivisas";
        private const string MontoRestantePorPagarEnMonedaLocalParaMostrarPropertyName = "MontoRestantePorPagarEnMonedaLocalParaMostrar";
        private const string MontoRestantePorPagarEnDivisasParaMostrarPropertyName = "MontoRestantePorPagarEnDivisasParaMostrar";
        private const string lblPorPagarYVueltoPropertyName = "lblPorPagarYVuelto";
        private const string ConexionCodigoMonedaPropertyName = "ConexionCodigoMoneda";
        private const string IsVisibleSeccionIGTFPropertyName = "IsVisibleSeccionIGTF";
        private const string IsVisibleCreditoElectronicoPropertyName = "IsVisibleCreditoElectronico";
        private const string NombreCreditoElectronicoPropertyName = "NombreCreditoElectronico";
        private const string CantidadCuotasUsualesCreditoElectronicoPropertyName = "CantidadCuotasUsualesCreditoElectronico";
        private const string DiasDeCreditoPorCuotaCreditoElectronicoPropertyName = "DiasDeCreditoPorCuotaCreditoElectronico";
        private const string MaximaCantidadCuotasCreditoElectronicoPropertyName = "MaximaCantidadCuotasCreditoElectronico";
        private const string MontoCreditoElectronicoPropertyName = "MontoCreditoElectronico";
        private string _NombreDeMonedaLocal;
        private string _NombreDeMonedaDivisa;
        private string _SimboloMonedaLocal;
        private string _SimboloDivisa;
        private string _CodigoMonedaDivisa;
        private decimal _TotalFacturaEnDivisas;
        private decimal _CambioAMonedaLocal;
        private decimal _EfectivoEnMonedaLocal;
        private decimal _TotalEnEfectivoDivisas;
        private decimal _TarjetaUno;
        private decimal _TarjetaDos;
        private decimal _MontoRestantePorPagarEnDivisas;
        private decimal _TransferenciaEnMonedaLocal;
        private decimal _TransferenciaEnDivisas;
        private decimal _VueltoEnMonedaLocal;
        private decimal _VueltoC2p;
        private decimal _VueltoEnDivisas;
        private string _MontoRestantePorPagarEnMonedaLocalParaMostrar;
        private string _MontoRestantePorPagarEnDivisasParaMostrar;
        private FkMonedaViewModel _ConexionCodigoMoneda;
        private string _IsVisibleSeccionEfectivo;
        private string _IsVisibleSeccionTarjeta;
        private string _IsVisibleSeccionTransferencia;
        private bool _IsEnabledEfectivoDivisa;
        private XElement _XmlDatosDelCobro;
        private XElement _XmlDatosIGTF;
        private readonly bool _EsFacturaTradicional;
        public Action<bool> SeCobro;
        private decimal _AlicuotaIGTF;
        private eTipoDeContribuyenteDelIva _TipoDeContribuyenteIVA;
        private eBorderBackMontoXPagarColor _MontoXPagarColor = eBorderBackMontoXPagarColor.Falta;
        private const string BaseImponibleIGTFPropertyName = "BaseImponibleIGTF";
        private const string IGTFMLPropertyName = "IGTFML";
        private const string IGTFMEPropertyName = "IGTFME";
        private const string AlicuotaIGTFPropertyName = "AlicuotaIGTF";
        private const string TotalAPagarMLParaMostrarPropertyName = "TotalAPagarMLParaMostrar";
        private const string TotalAPagarMEParaMostrarPropertyName = "TotalAPagarMEParaMostrar";
        private const string IsEnableVueltoPropertyName = "IsEnableVuelto";
        private const string IsVisibleTotalMediosElectronicosPropertyName = "IsVisibleTotalMediosElectronicos";
        private string numReferencia;
        private string infoAdicional;
        public decimal TotalPagosME;
        public decimal TotalPagosML;
        private decimal _TotalMediosElectronicos;
        private decimal _TotalMediosElectronicosME;
        public string cedulaRif;
        public decimal montoTDDTDC;
        private bool vResultCobroTDDTDC = false;
        private const string CantidadTarjetasProcesadasPropertyName = "CantidadTarjetasProcesadas";
        private const string IsVisibleCantidadTarjetasProcesadasPropertyName = "IsVisibleCantidadTarjetasProcesadas";
        private decimal _CantidadTarjetasProcesadas;
        private bool _ImprimirComprobante;
        private bool _EsVueltoPagoMovil;
        private string _ListaVoucherMediosElectronicos;
        private bool _IsVisibleCreditoElectronico;
        private string _NombreCreditoElectronico;
        private decimal _CantidadCuotasUsualesCreditoElectronico;
        private decimal _DiasDeCreditoPorCuotaCreditoElectronico;
        private decimal _MaximaCantidadCuotasCreditoElectronico;
        private decimal _MontoCreditoElectronico;
        #endregion

        public enum eBorderBackMontoXPagarColor {
            Totalmente = 1,
            Falta,
            FaltaPeroSePuede
        }

        public eBorderBackMontoXPagarColor MontoXPagarColor {
            get {
                return _MontoXPagarColor;
            }
            set {
                _MontoXPagarColor = value;
                RaisePropertyChanged("MontoXPagarColor");
                RaisePropertyChanged("BGColor");
            }
        }

        #region Propiedades

        public override string ModuleName {
            get {
                return "Cobro Rápido en Multimoneda";
            }
        }

        public string NombreDeMonedaLocal {
            get {
                return _NombreDeMonedaLocal;
            }
            set {
                if (_NombreDeMonedaLocal != value) {
                    _NombreDeMonedaLocal = value;
                    RaisePropertyChanged(NombreDeMonedaLocalPropertyName);
                }
            }
        }

        public string NombreDeDivisa {
            get {
                return "Divisa (" + SimboloDivisa + ")";
            }
        }

        public string SimboloMonedaLocal {
            get {
                return _SimboloMonedaLocal;
            }
            set {
                if (_SimboloMonedaLocal != value) {
                    _SimboloMonedaLocal = value;
                }
            }
        }

        public string CodigoMonedaDivisa {
            get {
                return _CodigoMonedaDivisa;
            }
            set {
                if (_CodigoMonedaDivisa != value) {
                    _CodigoMonedaDivisa = value;
                }
            }
        }

        public string NombreMonedaDivisa {
            get {
                return _NombreDeMonedaDivisa;
            }
            set {
                if (_NombreDeMonedaDivisa != value) {
                    _NombreDeMonedaDivisa = value;
                    RaisePropertyChanged(NombreDeMonedaDivisaPropertyName);
                }
            }
        }

        public string SimboloDivisa {
            get {
                return _SimboloDivisa;
            }
            set {
                if (_SimboloDivisa != value) {
                    _SimboloDivisa = value;
                }
            }
        }

        public decimal CambioAMonedaLocal {
            get {
                return _CambioAMonedaLocal;
            }
            set {
                if (_CambioAMonedaLocal != value) {
                    _CambioAMonedaLocal = value;
                    RaisePropertyChanged(CambioAMonedaLocalPropertyName);
                }
            }
        }

        public string TotalFacturaParaMostrar {
            get {
                return SimboloMonedaLocal + ". " + LibConvert.ToStr(TotalFactura);
            }
        }

        public decimal TotalFacturaEnDivisas {
            get {
                return _TotalFacturaEnDivisas;
            }
            set {
                if (_TotalFacturaEnDivisas != value) {
                    _TotalFacturaEnDivisas = value;
                    RaisePropertyChanged(TotalFacturaEnDivisasPropertyName);
                }
            }
        }

        public string TotalFacturaEnDivisasParaMostrar {
            get {
                return SimboloDivisa + LibConvert.ToStr(TotalFacturaEnDivisas);
            }
        }

        public decimal EfectivoEnMonedaLocal {
            get {
                return _EfectivoEnMonedaLocal;
            }
            set {
                if (_EfectivoEnMonedaLocal != value) {
                    _EfectivoEnMonedaLocal = value;
                    RaisePropertyChanged(EfectivoEnMonedaLocalPropertyName);
                }
            }
        }

        public decimal EfectivoEnDivisas {
            get {
                return _TotalEnEfectivoDivisas;
            }
            set {
                if (_TotalEnEfectivoDivisas != value) {
                    _TotalEnEfectivoDivisas = value;
                    RaisePropertyChanged(EfectivoEnDivisasPropertyName);
                }
            }
        }

        public decimal TarjetaUno {
            get {
                return _TarjetaUno;
            }
            set {
                if (_TarjetaUno != value) {
                    _TarjetaUno = value;
                    RaisePropertyChanged(TarjetaUnoPropertyName);
                }
            }
        }

        public decimal TarjetaDos {
            get {
                return _TarjetaDos;
            }
            set {
                if (_TarjetaDos != value) {
                    _TarjetaDos = value;
                    RaisePropertyChanged(TarjetaDosPropertyName);
                }
            }
        }

        public decimal TransferenciaEnMonedaLocal {
            get {
                return _TransferenciaEnMonedaLocal;
            }
            set {
                if (_TransferenciaEnMonedaLocal != value) {
                    _TransferenciaEnMonedaLocal = value;
                    RaisePropertyChanged(TransferenciaEnMonedaLocalPropertyName);
                }
            }
        }

        public decimal TransferenciaEnDivisas {
            get {
                return _TransferenciaEnDivisas;
            }
            set {
                if (_TransferenciaEnDivisas != value) {
                    _TransferenciaEnDivisas = value;
                    RaisePropertyChanged(TransferenciaEnDivisasPropertyName);
                }
            }
        }

        private decimal VueltoEfectivoMonedaLocal {
            get;
            set;
        }

        public decimal VueltoEnMonedaLocal {
            get {
                return _VueltoEnMonedaLocal;
            }
            set {
                if (_VueltoEnMonedaLocal != value) {
                    _VueltoEnMonedaLocal = value;
                    RaisePropertyChanged(VueltoEnMonedaLocalPropertyName);
                }
            }
        }

        public decimal VueltoC2p {
            get {
                return _VueltoC2p;
            }
            set {
                if (_VueltoC2p != value) {
                    _VueltoC2p = value;
                    RaisePropertyChanged(VueltoC2pPropertyName);
                }
            }
        }

        public decimal VueltoEnDivisas {
            get {
                return _VueltoEnDivisas;
            }
            set {
                if (_VueltoEnDivisas != value) {
                    _VueltoEnDivisas = value;
                    RaisePropertyChanged(VueltoEnDivisasPropertyName);
                }
            }
        }

        public decimal MontoRestantePorPagarEnDivisas {
            get {
                return _MontoRestantePorPagarEnDivisas;
            }
            set {
                if (_MontoRestantePorPagarEnDivisas != value) {
                    _MontoRestantePorPagarEnDivisas = value;
                    RaisePropertyChanged(MontoRestantePorPagarEnDivisasPropertyName);
                    CobrarCommand.RaiseCanExecuteChanged();
                    VueltoConPagoMovilCommand.RaiseCanExecuteChanged();
                    //AnularTransaccionCommand.RaiseCanExecuteChanged();
                    CobroMediosElectonicosCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string MontoRestantePorPagarEnMonedaLocalParaMostrar {
            get {
                return _MontoRestantePorPagarEnMonedaLocalParaMostrar;
            }
            set {
                if (_MontoRestantePorPagarEnMonedaLocalParaMostrar != value) {
                    _MontoRestantePorPagarEnMonedaLocalParaMostrar = value;
                    RaisePropertyChanged(MontoRestantePorPagarEnMonedaLocalParaMostrarPropertyName);
                }
            }
        }

        public string MontoRestantePorPagarEnDivisasParaMostrar {
            get {
                return _MontoRestantePorPagarEnDivisasParaMostrar;
            }
            set {
                if (_MontoRestantePorPagarEnDivisasParaMostrar != value) {
                    _MontoRestantePorPagarEnDivisasParaMostrar = value;
                    RaisePropertyChanged(MontoRestantePorPagarEnDivisasParaMostrarPropertyName);
                }
            }
        }

        public string lblPorPagarYVuelto {
            get {
                if (MontoXPagarColor == eBorderBackMontoXPagarColor.FaltaPeroSePuede) {
                    return "DIFERENCIA x CAMBIO";
                } else if (MontoRestantePorPagar <= 0) {
                    return "VUELTO";
                } else if (VueltoEnMonedaLocal > 0 || VueltoEnDivisas > 0) {
                    return "VUELTO EXCEDIDO";
                } else if (VueltoC2p > 0) {
                    return "VUELTO EXCEDIDO";
                } else {
                    return "POR PAGAR";
                }
            }
        }

        public string CambioAMonedaLocalParaMostrar {
            get {
                return LibConvert.NumToString(CambioAMonedaLocal, 4);
            }
        }

        public string NumeroControlVueltoPagoMovil { get; set; }

        public FkMonedaViewModel ConexionCodigoMoneda {
            get {
                return _ConexionCodigoMoneda;
            }
            set {
                if (_ConexionCodigoMoneda != value) {
                    _ConexionCodigoMoneda = value;
                    RaisePropertyChanged(ConexionCodigoMonedaPropertyName);
                }
            }
        }

        public RelayCommand LimpiarCommand { get; private set; }

        public RelayCommand VueltoConPagoMovilCommand { get; private set; }

        public RelayCommand CobroMediosElectonicosCommand { get; private set; }

        //public RelayCommand AnularTransaccionCommand { get; private set;}

        public string IsVisibleSeccionEfectivo {
            get {
                return _IsVisibleSeccionEfectivo;
            }
            set {
                if (_IsVisibleSeccionEfectivo != value) {
                    _IsVisibleSeccionEfectivo = value;
                }
            }
        }

        public string IsVisibleSeccionTarjeta {
            get {
                return _IsVisibleSeccionTarjeta;
            }
            set {
                if (_IsVisibleSeccionTarjeta != value) {
                    _IsVisibleSeccionTarjeta = value;
                }
            }
        }

        public string IsVisibleSeccionTransferencia {
            get {
                return _IsVisibleSeccionTransferencia;
            }
            set {
                if (_IsVisibleSeccionTransferencia != value) {
                    _IsVisibleSeccionTransferencia = value;
                }
            }
        }

        public bool IsVisibleSeccionVuelto {
            get {
                return (TipoDeDocumento == eTipoDocumentoFactura.Factura
                            || TipoDeDocumento == eTipoDocumentoFactura.NotaDeDebito
                            || TipoDeDocumento == eTipoDocumentoFactura.NotaDeDebitoComprobanteFiscal
                            || TipoDeDocumento == eTipoDocumentoFactura.ComprobanteFiscal
                            || TipoDeDocumento == eTipoDocumentoFactura.Boleta)
                    && ((VueltoEnMonedaLocal + VueltoEnDivisas) != 0);
            }
        }

        public bool IsEnabledEfectivoDivisa {
            get {
                return _IsEnabledEfectivoDivisa;
            }
            set {
                if (_IsEnabledEfectivoDivisa != value) {
                    _IsEnabledEfectivoDivisa = value;
                    RaisePropertyChanged(EfectivoEnDivisasPropertyName);
                }
            }
        }

        public XElement XmlDatosDelCobro {
            get {
                return _XmlDatosDelCobro;
            }
        }

        public SolidColorBrush BGColor {
            get {
                if (MontoXPagarColor == eBorderBackMontoXPagarColor.Totalmente) {
                    return new SolidColorBrush(Colors.Green);
                } else if (MontoXPagarColor == eBorderBackMontoXPagarColor.FaltaPeroSePuede) {
                    return new SolidColorBrush(Colors.Orange);
                } else {
                    return new SolidColorBrush(Colors.Red);
                }
            }
        }

        public decimal BaseImponibleIGTF {
            get {                
                decimal vTotalPagosME = LibMath.RoundToNDecimals((EfectivoEnDivisas + TransferenciaEnDivisas + TotalMediosElectronicosME) * CambioAMonedaLocal, 2);
                vTotalPagosME = IsVisibleSeccionIGTF ? vTotalPagosME : 0;
                return (TipoDeDocumento == eTipoDocumentoFactura.Factura || TipoDeDocumento == eTipoDocumentoFactura.ComprobanteFiscal || TipoDeDocumento == eTipoDocumentoFactura.NotaDeDebitoComprobanteFiscal) ? LibMath.RoundToNDecimals(System.Math.Min(TotalFactura, vTotalPagosME), 2) : 0;
            }
        }

        public decimal IGTFML {
            get {
                return LibMath.RoundToNDecimals(BaseImponibleIGTF * (AlicuotaIGTF / 100), 2);
            }
        }

        public decimal IGTFME {
            get {
                return LibMath.RoundToNDecimals(IGTFML / CambioAMonedaLocal, 2);
            }
        }

        public decimal TotalAPagarML {
            get {
                return LibMath.RoundToNDecimals(TotalFactura + IGTFML, 2);
            }
        }

        public string TotalAPagarMLParaMostrar {
            get {
                return LibConvert.ToStr(TotalAPagarML);
            }
        }

        public decimal TotalAPagarME {
            get {
                return TotalFacturaEnDivisas + IGTFME;
            }
        }

        public string TotalAPagarMEParaMostrar {
            get {
                return LibConvert.ToStr(TotalAPagarME);
            }
        }

        private bool IsVisibleSeccionIGTF {
            get {
                return _TipoDeContribuyenteIVA == eTipoDeContribuyenteDelIva.ContribuyenteEspecial && (TipoDeDocumento == eTipoDocumentoFactura.Factura || TipoDeDocumento == eTipoDocumentoFactura.ComprobanteFiscal || TipoDeDocumento == eTipoDocumentoFactura.NotaDeDebitoComprobanteFiscal);
            }
        }

        public string IsVisibleSeccionIGTFPrompt {
            get {
                return IsVisibleSeccionIGTF ? "Visible" : "Collapsed";
            }
        }

        public decimal AlicuotaIGTF {
            get {
                return IsVisibleSeccionIGTF ? _AlicuotaIGTF : 0;
            }
        }

        public XElement XmlDatosIGTF {
            get {
                return _XmlDatosIGTF;
            }
        }

        public string ListaVoucherMediosElectronicos {
            get {
                return _ListaVoucherMediosElectronicos;
            }
        }

        public bool IsEnableVuelto {
            get {
                decimal vTotalPagosML = EfectivoEnMonedaLocal + TarjetaUno + TarjetaDos + TotalMediosElectronicos + TransferenciaEnMonedaLocal;
                decimal vTotalPagosME = EfectivoEnDivisas + TransferenciaEnDivisas + TotalMediosElectronicosME + MontoCreditoElectronico;
                if (VueltoEnMonedaLocal > 0 || VueltoEnDivisas > 0) { // Vuelto en exceso
                    return true;
                } else if (vTotalPagosML > 0 || vTotalPagosME > 0) { // Vuelto en 0
                    return (MontoRestantePorPagar < 0) || (MontoRestantePorPagarEnDivisas < 0);
                } else {
                    return (MontoRestantePorPagar <= 0) || (MontoRestantePorPagarEnDivisas <= 0); // Caso Inicio de Pantalla
                }
            }
        }

        public bool IsVisibleVuelto {
            get {
                return TipoDeDocumento == eTipoDocumentoFactura.Factura || TipoDeDocumento == eTipoDocumentoFactura.ComprobanteFiscal || TipoDeDocumento == eTipoDocumentoFactura.NotaDeDebitoComprobanteFiscal;
            }
        }

        public bool IsVisibleMediosElectronicosDeCobro {
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMediosElectronicosDeCobro");
            }
        }

        public decimal TotalMediosElectronicos {
            get {
                return _TotalMediosElectronicos;
            }
            set {
                if (_TotalMediosElectronicos != value) {
                    _TotalMediosElectronicos = value;
                    RaisePropertyChanged(TotalMediosElectronicosPropertyName);
                }
            }
        }

        public decimal TotalMediosElectronicosME {
            get {
                return _TotalMediosElectronicosME;
            }
            set {
                if (_TotalMediosElectronicosME != value) {
                    _TotalMediosElectronicosME = value;
                    RaisePropertyChanged(TotalMediosElectronicosMEPropertyName);
                }
            }
        }

        public decimal CantidadTarjetasProcesadas {
            get {
                return _CantidadTarjetasProcesadas;
            }
            set {
                if (_CantidadTarjetasProcesadas != value) {
                    _CantidadTarjetasProcesadas = value;
                    RaisePropertyChanged(CantidadTarjetasProcesadasPropertyName);
                }
            }
        }

        public bool IsVisibleCantidadTarjetasProcesadas {
            get {
                return _CantidadTarjetasProcesadas > 0;
            }
        }

        private List<CobroCobroMediosElectonicosVPOS> ListaCobroCobroMediosElectonicosVPOS { get; set; }

        bool _IsVisibleTotalMediosElectronicos;
        public bool IsVisibleTotalMediosElectronicos {
            get {
                return _IsVisibleTotalMediosElectronicos;
            }
            set {
                if (_IsVisibleTotalMediosElectronicos != value) {
                    _IsVisibleTotalMediosElectronicos = value;
                    RaisePropertyChanged(IsVisibleTotalMediosElectronicosPropertyName);
                }
            }
        }

        public string vMonedaTransaccion { get; private set; }

        public bool IsVisibleCreditoElectronico {
            get
            {
                return _IsVisibleCreditoElectronico;
            }
            set
            {
                _IsVisibleCreditoElectronico = value;
                RaisePropertyChanged(IsVisibleCreditoElectronicoPropertyName);
            }
        }
        [LibCustomValidation("MaximaCuotaCreditoEelctronicoValidating")]
        public decimal CantidadCuotasUsualesCreditoElectronico {
            get { return _CantidadCuotasUsualesCreditoElectronico; }
            set {
                _CantidadCuotasUsualesCreditoElectronico = value;
                RaisePropertyChanged(CantidadCuotasUsualesCreditoElectronicoPropertyName);
            }
        }
        public decimal DiasDeCreditoPorCuotaCreditoElectronico {
            get { return _DiasDeCreditoPorCuotaCreditoElectronico; }
            set { 
                _DiasDeCreditoPorCuotaCreditoElectronico = value;
                RaisePropertyChanged(DiasDeCreditoPorCuotaCreditoElectronicoPropertyName);
            }
        }
        
        public decimal MaximaCantidadCuotasCreditoElectronico {
            get { return _MaximaCantidadCuotasCreditoElectronico; }
            set { 
                _MaximaCantidadCuotasCreditoElectronico = value;
                RaisePropertyChanged(MaximaCantidadCuotasCreditoElectronicoPropertyName);
            }
        }

        public string NombreCreditoElectronico
        {
            get { return _NombreCreditoElectronico + ":"; }
            set { 
                _NombreCreditoElectronico = value;
                RaisePropertyChanged(NombreCreditoElectronicoPropertyName);
            }
        }
        [LibCustomValidation("MontoCreditoElectronicoValidating")]
        public decimal MontoCreditoElectronico
        {
            get { return _MontoCreditoElectronico; }
            set { 
                _MontoCreditoElectronico = value;
                RaisePropertyChanged(MontoCreditoElectronicoPropertyName);
            }
        }
        #endregion

        #region Constructores e Inicializaciores
        public CobroRapidoMultimonedaViewModel() { }

        public CobroRapidoMultimonedaViewModel(int valConsecutivoCompania, string valNumeroDeDocumento, DateTime valFechaDeDocumento, decimal valTotalFactura, eTipoDocumentoFactura valTipoDeDocumento, string valCodigoMonedaDeLaFactura, string valCodigoMonedaDeCobro, bool valEsFacturaTradicional, decimal valAlicuotaIGTF, eTipoDeContribuyenteDelIva valTipoDeContribuyenteDelIva, string valCedulaRIF) {
            _MonedaLocalNav = new Saw.Lib.clsNoComunSaw(); // Se Llama  desde VB6            
            ConsecutivoCompania = valConsecutivoCompania;
            TipoDeDocumento = valTipoDeDocumento;
            NumeroFactura = valNumeroDeDocumento;
            FechaDeFactura = valFechaDeDocumento;
            _TipoDeContribuyenteIVA = valTipoDeContribuyenteDelIva;
            _AlicuotaIGTF = valAlicuotaIGTF;
            AsignarValoresDeMonedas(valCodigoMonedaDeLaFactura, valCodigoMonedaDeCobro);
            AsignarValoresInicialesDeTotales(valCodigoMonedaDeLaFactura, valTotalFactura);
            DeshabilitarControlesSegunTipoDeDocumento(TipoDeDocumento);
            CalcularTotales();
            _EsFacturaTradicional = valEsFacturaTradicional;
            ListaCobroCobroMediosElectonicosVPOS = new List<CobroCobroMediosElectonicosVPOS>();
            cedulaRif = valCedulaRIF;
            InicializarValoresCreditoElectronico();
        }

        public CobroRapidoMultimonedaViewModel(eAccionSR valAction, FacturaRapida valFactura, List<RenglonCobroDeFactura> valListDeCobroMaster, int valAlicuotaIvaASustituir, bool valEsFacturaTradicional, decimal valAlicuotaIGTF, eTipoDeContribuyenteDelIva valTipoDeContribuyenteDelIva, string valCedulaRIF) {
            _MonedaLocalNav = new clsNoComunSaw(); // Se Llama desde POS
            insFactura = valFactura;
            ConsecutivoCompania = insFactura.ConsecutivoCompania;
            TipoDeDocumento = insFactura.TipoDeDocumentoAsEnum;
            NumeroFactura = insFactura.Numero;
            FechaDeFactura = insFactura.Fecha;
            _TipoDeContribuyenteIVA = valTipoDeContribuyenteDelIva;
            _AlicuotaIGTF = valAlicuotaIGTF;
            AsignarValoresDeMonedas(insFactura.CodigoMoneda, insFactura.CodigoMonedaDeCobro);
            AsignarValoresInicialesDeTotales(insFactura.CodigoMoneda, insFactura.TotalFactura);
            DeshabilitarControlesSegunTipoDeDocumento(TipoDeDocumento);
            CalcularTotales();
            _EsFacturaTradicional = valEsFacturaTradicional;
            ListaCobroCobroMediosElectonicosVPOS = new List<CobroCobroMediosElectonicosVPOS>();
            cedulaRif = valCedulaRIF;
            InicializarValoresCreditoElectronico();
        }


        protected override void InitializeRibbon() {
            base.InitializeRibbon();
            if (RibbonData.TabDataCollection != null && RibbonData.TabDataCollection.Count > 0) {
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateCobrarRibbonButtonGroup());
                var tempRibbon = RibbonData.TabDataCollection[0].GroupDataCollection[0];
                RibbonData.TabDataCollection[0].GroupDataCollection.Remove(tempRibbon);
                RibbonData.TabDataCollection[0].AddTabGroupData(CreateMegasoftRibbonButtonGroup());
                RibbonData.TabDataCollection[0].GroupDataCollection.Insert(2, tempRibbon);
            }
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            LimpiarCommand = new RelayCommand(ExecuteLimpiarCommand, CanExecuteLimpiarCommand);
            VueltoConPagoMovilCommand = new RelayCommand(ExecuteVueltoConPagoMovilCommand, CanExecuteVueltoConPagoMovilCommand);
            CobroMediosElectonicosCommand = new RelayCommand(ExecuteCobroMediosElectonicosCommand, CanExecuteCobroMediosElectonicosCommand);
            //AnularTransaccionCommand = new RelayCommand(ExecuteAnularTransaccionCommand, CanExecuteAnularTransaccionCommand);
        }

        protected override void InitializeLookAndFeel() {
            base.InitializeLookAndFeel();
            RaiseMoveFocus(EfectivoEnMonedaLocalPropertyName);
        }

        protected override LibRibbonGroupData CreateCobrarRibbonButtonGroup() {
            var vResult = base.CreateCobrarRibbonButtonGroup();
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Limpiar",
                Command = LimpiarCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F7.png", UriKind.Relative),
                ToolTipDescription = "Limpia los valores en " + ModuleName,
                ToolTipTitle = "Limpiar pantalla (F7)",
                IsVisible = true,
                KeyTip = "F7"
            });
            return vResult;
        }

        protected LibRibbonGroupData CreateMegasoftRibbonButtonGroup() {
            LibRibbonGroupData vResult = new LibRibbonGroupData("Medios electrónicos");
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Vuelto con Pago Móvil",
                Command = VueltoConPagoMovilCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F10.png", UriKind.Relative),
                ToolTipDescription = "Al efectuarse el Pago Móvil culminará el Cobro",
                ToolTipTitle = "Vuelto Pago Móvil y Cobrar",
            });
            vResult.ControlDataCollection.Add(new LibRibbonButtonData() {
                Label = "Cobros Medios Electrónicos",
                Command = CobroMediosElectonicosCommand,
                LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F11.png", UriKind.Relative),
                ToolTipDescription = "Cobro Medios Electrónicos",
                ToolTipTitle = "Cobros TDD/TDC, Pago Móvil, Zelle, Transferencia",
            });
            return vResult;
        }
        #endregion

        #region Comandos
        protected override void ExecuteCobrarCommand() {
            MoveFocusIfNecessary();
            if (!IsValid) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(new GalacValidationException(Error), ModuleName, ModuleName);
                return;
            }
            bool SeImprimio = true;
            IRenglonCobroDeFacturaPdn insRenglonCobroDeFacturaPdn = new clsRenglonCobroDeFacturaNav();
            if (MontoRestantePorPagar <= 0 || (MontoRestantePorPagar > 0 && MontoRestantePorPagarEnDivisas == 0)) {
                clsCobroDeFacturaNav insCobroNav = new clsCobroDeFacturaNav();
                List<RenglonCobroDeFactura> vListaDecobro = new List<RenglonCobroDeFactura>();
                int vCodigoBancoParaMonedaLocal = insCobroNav.ObtenerCodigoBancoAsociadoACuentaBancaria(ConsecutivoCompania, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CuentaBancariaCobroDirecto"));
                int vCodigoBancoParaDivisa = insCobroNav.ObtenerCodigoBancoAsociadoACuentaBancaria(ConsecutivoCompania, LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CuentaBancariaCobroMultimoneda"));
                vListaDecobro = CrearListaDeCobro(TipoDeDocumento, vCodigoBancoParaMonedaLocal, vCodigoBancoParaDivisa);
                if (_EsFacturaTradicional) {
                    _XmlDatosDelCobro = CrearXmlDatosDelCobro(vListaDecobro);
                    _XmlDatosIGTF = CrearXmlDatosIGTF();
                    _ListaVoucherMediosElectronicos = CrearListaVoucherMediosElectronicos();
                    if (SeCobro != null) {
                        SeCobro.Invoke(true);
                    }
                } else {
                    try {
                        insFactura.BaseImponibleIGTF = BaseImponibleIGTF;
                        insFactura.IGTFML = IGTFML;
                        insFactura.IGTFME = IGTFME;
                        insFactura.AlicuotaIGTF = AlicuotaIGTF;
                        insFactura.TotalAPagar = TotalAPagarML;
                        clsRenglonCobroDeFacturaNav vRenglonCobro = new clsRenglonCobroDeFacturaNav();
                        IRenglonCobroDeFacturaPdn vRenglonCobroDeFactura = new clsRenglonCobroDeFacturaNav();
                        string vPath = Path.Combine(new PagosElectronicosMngViewModel().RutaMegasoft, "vouchers");
                        DialogResult = vRenglonCobro.InsertChildRenglonCobroDeFactura(ConsecutivoCompania, NumeroFactura, eTipoDocumentoFactura.ComprobanteFiscal, vListaDecobro).Success;
                        if (DialogResult) {
                            vListaDecobro.RemoveAll(x => x.CodigoFormaDelCobro == insRenglonCobroDeFacturaPdn.BuscarCodigoFormaDelCobro(eTipoDeFormaDePago.VueltoEfectivo) || x.CodigoFormaDelCobro == insRenglonCobroDeFacturaPdn.BuscarCodigoFormaDelCobro(eTipoDeFormaDePago.VueltoC2P));
                            XElement vDatosCreditoElectronico = DatosCreditoElectronico();
                            SeImprimio = ImprimirFacturaFiscal(vListaDecobro, vDatosCreditoElectronico);
                            if (_ImprimirComprobante) {
                                if (_EsVueltoPagoMovil) {
                                    if (XmlDatosImprFiscal != null) {
                                        ImprimirComprobanteNoFiscalAdicional("Vuelto con Pago Móvil", infoAdicional);
                                    } else {
                                        vPath = Path.Combine(vPath, infoAdicional);
                                        System.Diagnostics.Process.Start(vPath);
                                    }
                                    _EsVueltoPagoMovil = false;
                                } else {
                                    if (XmlDatosImprFiscal != null) {
                                        foreach (var vTarjeta in ListaCobroCobroMediosElectonicosVPOS) {
                                            ImprimirComprobanteNoFiscalAdicional("Cobro con Medios Electrónicos", vTarjeta.InfoAdicional);
                                        }
                                    } else {
                                        foreach (var vTarjeta in ListaCobroCobroMediosElectonicosVPOS) {
                                            vPath = Path.Combine(vPath, vTarjeta.InfoAdicional);
                                            System.Diagnostics.Process.Start(vPath);
                                        }
                                    }
                                }
                                _ImprimirComprobante = false;
                            }
                        }
                        if (SeCobro != null)
                            SeCobro.Invoke(SeImprimio);
                    } catch (Exception vEx) {
                        throw vEx;
                    }
                }
                base.ExecuteCancel();
            } else {
                LibMessages.MessageBox.Information(this, "Aun tiene un monto por pagar de " + SimboloMonedaLocal + "." + MontoRestantePorPagar
                    + " / " + SimboloDivisa + MontoRestantePorPagarEnDivisas, "Monto restante por pagar");
            }
            XElement DatosCreditoElectronico() {
                XElement vResult = null;
                var vUsarCreditoElectronico = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaCreditoElectronico");
                if (vUsarCreditoElectronico) { 
                    var vCambioMostrarTotalEnDivisas = CambioAMonedaLocalParaMostrar;
                    var vGenerarVariasCxC = ! LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "GenerarUnaUnicaCuotaCreditoElectronico");
                    var vUsaClienteUnicoCreditoElectronico = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaClienteUnicoCreditoElectronico");
                    var vCodigoClienteCreditoElectronico = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoClienteCreditoElectronico");
                    vResult = new XElement("GpCobroElectronico", new XElement("GenerarVariasCxC", vGenerarVariasCxC));
                    vResult.Add(new XElement("CodigoClienteCreditoElectronico", vCodigoClienteCreditoElectronico));
                    vResult.Add(new XElement("CantidadCuotasCreditoElectronico", CantidadCuotasUsualesCreditoElectronico));
                    vResult.Add(new XElement("MontoCreditoElectronico", MontoCreditoElectronico));
                    vResult.Add(new XElement("UsaClienteUnicoCreditoElectronico", vUsaClienteUnicoCreditoElectronico));
                    vResult.Add(new XElement("CambioAMonedaExtranjera", vCambioMostrarTotalEnDivisas));
                }
                return vResult;
            }
        }

        private void ExecuteVueltoConPagoMovilCommand() {
            try {
                C2PMegasoftNav insVueltoMegasoft = new C2PMegasoftNav();
                if (insVueltoMegasoft.EjecutaProcesarCambioPagoMovil(CodigoCliente, LibMath.Abs(MontoRestantePorPagar))) {
                    VueltoC2p = (MontoRestantePorPagar - VueltoEfectivoMonedaLocal);
                    infoAdicional = insVueltoMegasoft.infoAdicional;
                    numReferencia = insVueltoMegasoft.numeroReferencia;
                    if (MontoRestantePorPagar <= 0 || (MontoRestantePorPagar > 0 && MontoRestantePorPagarEnDivisas == 0)) {
                        if (LibMessages.MessageBox.YesNo(this, "¿Desea imprimir comprobante de Vuelto Pago Móvil?", ModuleName)) {
                            _ImprimirComprobante = true;
                            _EsVueltoPagoMovil = true;
                        }
                        ExecuteCobrarCommand();
                    }
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void ImprimirComprobanteNoFiscalAdicional(string valDescripcion, string valNombreVoucher) {
            string vPath = Path.Combine(new PagosElectronicosMngViewModel().RutaMegasoft, "vouchers", valNombreVoucher);
            string vTexto = string.Empty;
            if (LibFile.FileExists(vPath)) {
                vTexto = LibFile.ReadFile(vPath);
                clsImpresoraFiscalCreator vCreatorMaquinaFiscal = new clsImpresoraFiscalCreator();
                IImpresoraFiscalPdn vImpresoraFiscal = vCreatorMaquinaFiscal.Crear(XmlDatosImprFiscal);
                vImpresoraFiscal.ImprimirDocumentoNoFiscal(vTexto, valDescripcion);
            } else {
                LibMessages.MessageBox.Information(this, $"el archivo de comprobante: {valNombreVoucher} no fue encontrado", ModuleName);
            }
        }

        private void ExecuteCobroMediosElectonicosCommand() {
            try {
                Regex vListInvalidChars = new Regex("[V|E|J|G|C|P|-]", RegexOptions.Compiled);
                cedulaRif = vListInvalidChars.Replace(cedulaRif, "");
                if (MontoRestantePorPagar > 0) {
                    DatosVPosViewModel vDatosVpos = new DatosVPosViewModel(MontoRestantePorPagar);
                    vDatosVpos.vResultCobroTDDTDC += (arg) => vResultCobroTDDTDC = arg;
                    vDatosVpos.InitializeViewModel(cedulaRif, vDatosVpos.Monto, AlicuotaIGTF);
                    LibMessages.EditViewModel.ShowEditor(vDatosVpos, true);
                    _ImprimirComprobante = vDatosVpos.ImprimirComprobanteDePago;
                    C2PMegasoftNav insMegasoft = new C2PMegasoftNav();
                    if (insMegasoft.EjecutarCobroMediosElectonicos(vDatosVpos.CedulaRif, LibMath.Abs(vDatosVpos.Monto))) {
                        if (insMegasoft.montoTransaccion > 0) {
                            if (LibString.S1IsEqualToS2(insMegasoft.tipoTransaccion, "164")) {
                                ValidacionZelleViewModel vValidacionZelleViewModel = new ValidacionZelleViewModel(LibConvert.ToDec(insMegasoft.montoTransaccion, 2));
                                LibMessages.EditViewModel.ShowEditor(vValidacionZelleViewModel, true);
                                insMegasoft.montoTransaccion = vValidacionZelleViewModel.MontoARegistrar;
                                insMegasoft.monedaTransaccion = "USD";
                            }
                            if (insMegasoft.monedaTransaccion == CodigoMonedaDivisa) {
                                vMonedaTransaccion = insMegasoft.monedaTransaccion;
                                TotalMediosElectronicosME += insMegasoft.montoTransaccion;
                                CalcularTotales();
                            } else {
                                vMonedaTransaccion = _MonedaLocalNav.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today());
                                TotalMediosElectronicos += insMegasoft.montoTransaccion;
                                CalcularTotales();
                            }
                            clsCobroDeFacturaNav insCobroNav = new clsCobroDeFacturaNav();
                            ListaCobroCobroMediosElectonicosVPOS.Add(new CobroCobroMediosElectonicosVPOS() {
                                MontoTransaccion = insMegasoft.montoTransaccion,
                                NumReferencia = LibString.IsNullOrEmpty(insMegasoft.numeroAutorizacion) ? insMegasoft.numeroReferencia : insMegasoft.numeroAutorizacion,
                                InfoAdicional = insMegasoft.infoAdicional,
                                CodigoFormaDelCobro = TipoDeTransaccionCobro(insMegasoft.tipoTransaccion),
                                MonedaTransaccion = vMonedaTransaccion
                            });
                            CantidadTarjetasProcesadas += 1;
                        }
                    }
                }
            } catch (LibGalac.Aos.Catching.GalacAlertException vGx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vGx);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private string TipoDeTransaccionCobro(string vTransaccion) {
            IRenglonCobroDeFacturaPdn insRenglonCobroDeFactura = new clsRenglonCobroDeFacturaNav();
            string vResult;
            switch (vTransaccion) {
                case "137":
                    vResult = insRenglonCobroDeFactura.BuscarCodigoFormaDelCobro(eTipoDeFormaDePago.PagoMovil);
                    break;
                case "138":
                    vResult = insRenglonCobroDeFactura.BuscarCodigoFormaDelCobro(eTipoDeFormaDePago.C2P);
                    break;
                case "164":
                    vResult = insRenglonCobroDeFactura.BuscarCodigoFormaDelCobro(eTipoDeFormaDePago.Zelle);
                    break;
                case "191":
                    vResult = insRenglonCobroDeFactura.BuscarCodigoFormaDelCobro(eTipoDeFormaDePago.TarjetaMS);
                    break;
                case "128":
                    vResult = insRenglonCobroDeFactura.BuscarCodigoFormaDelCobro(eTipoDeFormaDePago.TransferenciaMS);
                    break;
                case "129":
                    vResult = insRenglonCobroDeFactura.BuscarCodigoFormaDelCobro(eTipoDeFormaDePago.DepositoMS);
                    break;
                default:
                    vResult = insRenglonCobroDeFactura.BuscarCodigoFormaDelCobro(eTipoDeFormaDePago.PagoMovil);
                    break;
            }
            return vResult;
        }

        protected override void ExecuteCancel() {
            if (LibMessages.MessageBox.YesNo(this, "¿Está seguro que desea salir?", "Cobro Rápido en Multimoneda")) {
                _XmlDatosDelCobro = null;
                _XmlDatosIGTF = null;
                if (SeCobro != null)
                    SeCobro.Invoke(false);
                base.ExecuteCancel();
            }
        }

        protected override bool CanExecuteCobrarCommand() {
            bool vResult = false;
            vResult = SePuedeCobrar() && (_EsFacturaTradicional || base.CanExecuteCobrarCommand());
            return vResult;
        }

        private bool SePuedeCobrar() {
            bool vResult;
            CalcularTotales();
            TotalPagosME = LibMath.Abs(EfectivoEnDivisas) + LibMath.Abs(TransferenciaEnDivisas) + LibMath.Abs(TotalMediosElectronicosME) + LibMath.Abs(MontoCreditoElectronico) - LibMath.Abs(VueltoEnDivisas);
            TotalPagosML = LibMath.Abs(EfectivoEnMonedaLocal) + LibMath.Abs(TarjetaUno) + LibMath.Abs(TarjetaDos) + LibMath.Abs(TotalMediosElectronicos) + LibMath.Abs(TransferenciaEnMonedaLocal) - LibMath.Abs(VueltoEnMonedaLocal + VueltoC2p);
            vResult = ((TotalPagosML == 0) && (MontoRestantePorPagar <= 0))
                   || ((TotalPagosME == 0) && (MontoRestantePorPagarEnDivisas <= 0));
            if (!vResult) {
                vResult = (TotalPagosME != 0) && (TotalPagosML != 0) && (MontoRestantePorPagar <= 0);
            }
            if (vResult) {
                MontoXPagarColor = (MontoRestantePorPagar <= 0) ? eBorderBackMontoXPagarColor.Totalmente : eBorderBackMontoXPagarColor.FaltaPeroSePuede;
            } else {
                MontoXPagarColor = eBorderBackMontoXPagarColor.Falta;
            }
            return vResult;
        }

        private bool CanExecuteLimpiarCommand() { return true; }

        private bool CanExecuteVueltoConPagoMovilCommand() {
            bool vResult = (MontoRestantePorPagar < 0) || (MontoRestantePorPagarEnDivisas < 0);
            return vResult;
        }

        private bool CanExecuteCobroMediosElectonicosCommand() {
            bool vResult = (MontoRestantePorPagar > 0) || (MontoRestantePorPagarEnDivisas > 0);
            return vResult;
        }

        #endregion

        #region Metodos
        private void AsignarValoresDeMonedas(string valCodigoMonedaDeLaFactura, string valCodigoMonedaDeCobro) {
            if (_MonedaLocalNav.InstanceMonedaLocalActual.EsMonedaLocalDelPais(valCodigoMonedaDeLaFactura)) {
                string vMonedaExtranjeraEnParametros = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
                CodigoMonedaDivisa = _MonedaLocalNav.InstanceMonedaLocalActual.EsMonedaLocalDelPais(valCodigoMonedaDeCobro) ? vMonedaExtranjeraEnParametros : valCodigoMonedaDeCobro;
            } else {
                CodigoMonedaDivisa = valCodigoMonedaDeLaFactura;
            }
            ObtenerNombresYSimbolosDeMonedas();
            AsignarTasaDeCambioDelDia(CodigoMonedaDivisa, FechaDeFactura);
        }

        private void AsignarValoresInicialesDeTotales(string valCodigoMonedaDeLaFactura, decimal valTotalFactura) {
            if (_MonedaLocalNav.InstanceMonedaLocalActual.EsMonedaLocalDelPais(valCodigoMonedaDeLaFactura)) {
                TotalFactura = valTotalFactura;
                TotalFacturaEnDivisas = LibMath.RoundToNDecimals(TotalFactura / CambioAMonedaLocal, 2);
            } else {
                TotalFacturaEnDivisas = valTotalFactura;
                TotalFactura = LibMath.RoundToNDecimals(TotalFacturaEnDivisas * CambioAMonedaLocal, 2);
            }
            MontoRestantePorPagar = TotalFactura;
            MontoRestantePorPagarEnDivisas = TotalFacturaEnDivisas;
        }

        public override void CalcularTotales() {
            decimal TotalPagosMe = LibMath.Abs(EfectivoEnDivisas) + LibMath.Abs(TransferenciaEnDivisas) + LibMath.Abs(TotalMediosElectronicosME) + LibMath.Abs(MontoCreditoElectronico);
            decimal TotalPagosML = LibMath.Abs(EfectivoEnMonedaLocal) + LibMath.Abs(TarjetaUno) + LibMath.Abs(TarjetaDos) + LibMath.Abs(TotalMediosElectronicos) + LibMath.Abs(TransferenciaEnMonedaLocal);
            LimpiarVuelto(TotalPagosML, TotalPagosMe);
            TotalPagosML = TotalPagosML - (LibMath.Abs(VueltoEnMonedaLocal) + LibMath.Abs(VueltoC2p));
            TotalPagosMe = TotalPagosMe - LibMath.Abs(VueltoEnDivisas);
            MontoRestantePorPagar = LibMath.RoundToNDecimals(TotalAPagarML - (TotalPagosML + LibMath.RoundToNDecimals(TotalPagosMe * CambioAMonedaLocal, 2)), 2);
            MontoRestantePorPagarEnDivisas = LibMath.RoundToNDecimals(MontoRestantePorPagar / CambioAMonedaLocal, 2);
            MontoRestantePorPagarEnMonedaLocalParaMostrar = SimboloMonedaLocal + ". " + LibConvert.ToStr(LibMath.Abs(MontoRestantePorPagar));
            MontoRestantePorPagarEnDivisasParaMostrar = SimboloDivisa + LibConvert.ToStr(LibMath.Abs(MontoRestantePorPagarEnDivisas));
            RaisePropertyChanged(lblPorPagarYVueltoPropertyName);
            RaisePropertyChanged(BaseImponibleIGTFPropertyName);
            RaisePropertyChanged(IGTFMLPropertyName);
            RaisePropertyChanged(IGTFMEPropertyName);
            RaisePropertyChanged(AlicuotaIGTFPropertyName);
            RaisePropertyChanged(TotalAPagarMLParaMostrarPropertyName);
            RaisePropertyChanged(TotalAPagarMEParaMostrarPropertyName);
            RaisePropertyChanged(IsEnableVueltoPropertyName);
            RaisePropertyChanged(IsVisibleTotalMediosElectronicosPropertyName);
            RaisePropertyChanged(IsVisibleCantidadTarjetasProcesadasPropertyName);
        }

        private void LimpiarVuelto(decimal valTotalPagoML, decimal valTotalPagosMe) {
            if (valTotalPagoML == 0 && valTotalPagosMe == 0) {
                VueltoEnMonedaLocal = 0;
                VueltoEnDivisas = 0;
            }
        }

        private void AsignarTasaDeCambioDelDia(string valCodigoMoneda, DateTime valFechaDeVigencia) {
            bool vElProgramaEstaEnModoAvanzado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsModoAvanzado");
            bool vUsarLimiteMaximoParaIngresoDeTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarLimiteMaximoParaIngresoDeTasaDeCambio");
            decimal vMaximoLimitePermitidoParaLaTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros", "MaximoLimitePermitidoParaLaTasaDeCambio");
            bool vObtenerAutomaticamenteTasaDeCambioDelBCV = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "ObtenerAutomaticamenteTasaDeCambioDelBCV");
            CambioAMonedaLocal = clsSawCambio.InsertaTasaDeCambioParaElDia(valCodigoMoneda, LibDate.Today(), vUsarLimiteMaximoParaIngresoDeTasaDeCambio, vMaximoLimitePermitidoParaLaTasaDeCambio, vElProgramaEstaEnModoAvanzado, vObtenerAutomaticamenteTasaDeCambioDelBCV);
            ;
        }

        private void ObtenerNombresYSimbolosDeMonedas() {
            SimboloMonedaLocal = _MonedaLocalNav.InstanceMonedaLocalActual.SimboloMoneda(LibDate.Today());
            NombreDeMonedaLocal = _MonedaLocalNav.InstanceMonedaLocalActual.GetHoyNombreMoneda() + " (" + SimboloMonedaLocal + ")";
            ConexionCodigoMoneda = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", CodigoMonedaDivisa), new clsCobroDeFacturaNav());
            if (ConexionCodigoMoneda != null) {
                SimboloDivisa = ConexionCodigoMoneda.Simbolo;
                NombreMonedaDivisa = ConexionCodigoMoneda.Nombre;
            }
        }

        private List<RenglonCobroDeFactura> CrearListaDeCobro(eTipoDocumentoFactura valTipoDeDocumento, int valCodigoBancoParaMonedaLocal, int valCodigoBancoParaDivisa) {
            IRenglonCobroDeFacturaPdn insRenglonCobroDeFactura = new clsRenglonCobroDeFacturaNav();
            List<RenglonCobroDeFactura> vRenglonesDeCobro = new List<RenglonCobroDeFactura>();
            int vConsecutivoRenglon = 0;
            string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
            if (LibDate.F1IsGreaterOrEqualThanF2(LibDate.Today(), Galac.Saw.Reconv.clsUtilReconv.GetFechaReconversion())) {
                vCodigoMonedaLocal = LibString.IsNullOrEmpty(vCodigoMonedaLocal) ? "VED" : vCodigoMonedaLocal;
            } else {
                vCodigoMonedaLocal = LibString.IsNullOrEmpty(vCodigoMonedaLocal) ? "VES" : vCodigoMonedaLocal;
            }
            decimal TotalPagosML = LibMath.Abs(EfectivoEnMonedaLocal) + LibMath.Abs(TarjetaUno) + LibMath.Abs(TarjetaDos) + LibMath.Abs(TotalMediosElectronicos) + LibMath.Abs(TransferenciaEnMonedaLocal) - LibMath.Abs(VueltoEnMonedaLocal + VueltoC2p);
            if (TotalPagosML == 0) { //Se cobró todo en ME
                decimal vCobradoEnDivisasConvertido = 0;
                if (EfectivoEnDivisas != 0) {
                    vConsecutivoRenglon += 1;
                    vRenglonesDeCobro.Add(new RenglonCobroDeFactura() {
                        ConsecutivoCompania = ConsecutivoCompania,
                        NumeroFactura = NumeroFactura,
                        TipoDeDocumento = LibConvert.EnumToDbValue((int)valTipoDeDocumento),
                        ConsecutivoRenglon = vConsecutivoRenglon,
                        CodigoFormaDelCobro = "00001",
                        Monto = EfectivoEnDivisas,
                        CodigoMoneda = CodigoMonedaDivisa,
                        CambioAMonedaLocal = CambioAMonedaLocal
                    });
                    vCobradoEnDivisasConvertido += LibMath.RoundToNDecimals(EfectivoEnDivisas * CambioAMonedaLocal, 2);
                }
                if (TransferenciaEnDivisas != 0) {
                    vConsecutivoRenglon += 1;
                    vRenglonesDeCobro.Add(new RenglonCobroDeFactura() {
                        ConsecutivoCompania = ConsecutivoCompania,
                        NumeroFactura = NumeroFactura,
                        TipoDeDocumento = LibConvert.EnumToDbValue((int)valTipoDeDocumento),
                        ConsecutivoRenglon = vConsecutivoRenglon,
                        CodigoFormaDelCobro = "00006",
                        CodigoBanco = valCodigoBancoParaDivisa,
                        Monto = TransferenciaEnDivisas,
                        CodigoMoneda = CodigoMonedaDivisa,
                        CambioAMonedaLocal = CambioAMonedaLocal
                    });
                    vCobradoEnDivisasConvertido += LibMath.RoundToNDecimals(TransferenciaEnDivisas * CambioAMonedaLocal, 2);
                }
                if (MontoCreditoElectronico != 0) {
                    vConsecutivoRenglon += 1;
                    vRenglonesDeCobro.Add(new RenglonCobroDeFactura() {
                        ConsecutivoCompania = ConsecutivoCompania,
                        NumeroFactura = NumeroFactura,
                        TipoDeDocumento = LibConvert.EnumToDbValue((int)valTipoDeDocumento),
                        ConsecutivoRenglon = vConsecutivoRenglon,
                        CodigoFormaDelCobro = "00015",
                        Monto = MontoCreditoElectronico,
                        CodigoMoneda = CodigoMonedaDivisa,
                        CambioAMonedaLocal = CambioAMonedaLocal,
                        InfoAdicional = LibConvert.ToStr(CantidadCuotasUsualesCreditoElectronico, 0)
                    });
                    vCobradoEnDivisasConvertido += LibMath.RoundToNDecimals(MontoCreditoElectronico * CambioAMonedaLocal, 2);
                }
                decimal vDiferencia = TotalAPagarML - vCobradoEnDivisasConvertido - LibMath.Abs(VueltoEnDivisas);
                if (vCobradoEnDivisasConvertido != 0 && vDiferencia > 0) {
                    vConsecutivoRenglon += 1;
                    vRenglonesDeCobro.Add(new RenglonCobroDeFactura() {
                        ConsecutivoCompania = ConsecutivoCompania,
                        NumeroFactura = NumeroFactura,
                        TipoDeDocumento = LibConvert.EnumToDbValue((int)valTipoDeDocumento),
                        ConsecutivoRenglon = vConsecutivoRenglon,
                        CodigoFormaDelCobro = "00001",
                        CodigoBanco = valCodigoBancoParaDivisa,
                        Monto = vDiferencia,
                        CodigoMoneda = vCodigoMonedaLocal,
                        CambioAMonedaLocal = 1
                    });
                }
            } else {
                if (EfectivoEnMonedaLocal != 0) {
                    vConsecutivoRenglon += 1;
                    vRenglonesDeCobro.Add(new RenglonCobroDeFactura() {
                        ConsecutivoCompania = ConsecutivoCompania,
                        NumeroFactura = NumeroFactura,
                        TipoDeDocumento = LibConvert.EnumToDbValue((int)valTipoDeDocumento),
                        ConsecutivoRenglon = vConsecutivoRenglon,
                        CodigoFormaDelCobro = insRenglonCobroDeFactura.BuscarCodigoFormaDelCobro(eTipoDeFormaDePago.Efectivo),
                        Monto = EfectivoEnMonedaLocal,
                        CodigoMoneda = vCodigoMonedaLocal,
                        CambioAMonedaLocal = 1
                    });
                }
                if (EfectivoEnDivisas != 0) {
                    vConsecutivoRenglon += 1;
                    vRenglonesDeCobro.Add(new RenglonCobroDeFactura() {
                        ConsecutivoCompania = ConsecutivoCompania,
                        NumeroFactura = NumeroFactura,
                        TipoDeDocumento = LibConvert.EnumToDbValue((int)valTipoDeDocumento),
                        ConsecutivoRenglon = vConsecutivoRenglon,
                        CodigoFormaDelCobro = "00001",
                        Monto = EfectivoEnDivisas,
                        CodigoMoneda = CodigoMonedaDivisa,
                        CambioAMonedaLocal = CambioAMonedaLocal
                    });
                }
                if (MontoCreditoElectronico != 0) { // ESTE MONTO SIEMPRE ES EN DIVISAS
                    vConsecutivoRenglon += 1;
                    vRenglonesDeCobro.Add(new RenglonCobroDeFactura() {
                        ConsecutivoCompania = ConsecutivoCompania,
                        NumeroFactura = NumeroFactura,
                        TipoDeDocumento = LibConvert.EnumToDbValue((int)valTipoDeDocumento),
                        ConsecutivoRenglon = vConsecutivoRenglon,
                        CodigoFormaDelCobro = "00015",
                        Monto = MontoCreditoElectronico,
                        CodigoMoneda = CodigoMonedaDivisa,
                        CambioAMonedaLocal = CambioAMonedaLocal,
                        InfoAdicional = LibConvert.ToStr(CantidadCuotasUsualesCreditoElectronico, 0)
                    });
                }
                if (TarjetaUno != 0) {
                    vConsecutivoRenglon += 1;
                    vRenglonesDeCobro.Add(new RenglonCobroDeFactura() {
                        ConsecutivoCompania = ConsecutivoCompania,
                        NumeroFactura = NumeroFactura,
                        TipoDeDocumento = LibConvert.EnumToDbValue((int)valTipoDeDocumento),
                        ConsecutivoRenglon = vConsecutivoRenglon,
                        CodigoFormaDelCobro = "00003",
                        CodigoBanco = valCodigoBancoParaMonedaLocal,
                        Monto = TarjetaUno,
                        CodigoMoneda = vCodigoMonedaLocal,
                        CambioAMonedaLocal = 1
                    });
                }
                if (TarjetaDos != 0) {
                    vConsecutivoRenglon += 1;
                    vRenglonesDeCobro.Add(new RenglonCobroDeFactura() {
                        ConsecutivoCompania = ConsecutivoCompania,
                        NumeroFactura = NumeroFactura,
                        TipoDeDocumento = LibConvert.EnumToDbValue((int)valTipoDeDocumento),
                        ConsecutivoRenglon = vConsecutivoRenglon,
                        CodigoFormaDelCobro = "00003",
                        CodigoBanco = valCodigoBancoParaMonedaLocal,
                        Monto = TarjetaDos,
                        CodigoMoneda = vCodigoMonedaLocal,
                        CambioAMonedaLocal = 1
                    });
                }
                if (TransferenciaEnMonedaLocal != 0) {
                    vConsecutivoRenglon += 1;
                    vRenglonesDeCobro.Add(new RenglonCobroDeFactura() {
                        ConsecutivoCompania = ConsecutivoCompania,
                        NumeroFactura = NumeroFactura,
                        TipoDeDocumento = LibConvert.EnumToDbValue((int)valTipoDeDocumento),
                        ConsecutivoRenglon = vConsecutivoRenglon,
                        CodigoFormaDelCobro = "00006",
                        CodigoBanco = valCodigoBancoParaMonedaLocal,
                        Monto = TransferenciaEnMonedaLocal,
                        CodigoMoneda = vCodigoMonedaLocal,
                        CambioAMonedaLocal = 1
                    });
                }
                if (TransferenciaEnDivisas != 0) {
                    vConsecutivoRenglon += 1;
                    vRenglonesDeCobro.Add(new RenglonCobroDeFactura() {
                        ConsecutivoCompania = ConsecutivoCompania,
                        NumeroFactura = NumeroFactura,
                        TipoDeDocumento = LibConvert.EnumToDbValue((int)valTipoDeDocumento),
                        ConsecutivoRenglon = vConsecutivoRenglon,
                        CodigoFormaDelCobro = "00006",
                        CodigoBanco = valCodigoBancoParaDivisa,
                        Monto = TransferenciaEnDivisas,
                        CodigoMoneda = CodigoMonedaDivisa,
                        CambioAMonedaLocal = CambioAMonedaLocal
                    });
                }
            }
            if (VueltoEnMonedaLocal != 0) {
                vConsecutivoRenglon += 1;
                vRenglonesDeCobro.Add(new RenglonCobroDeFactura() {
                    ConsecutivoCompania = ConsecutivoCompania,
                    NumeroFactura = NumeroFactura,
                    TipoDeDocumento = LibConvert.EnumToDbValue((int)valTipoDeDocumento),
                    ConsecutivoRenglon = vConsecutivoRenglon,
                    CodigoFormaDelCobro = insRenglonCobroDeFactura.BuscarCodigoFormaDelCobro(eTipoDeFormaDePago.VueltoEfectivo),
                    CodigoBanco = valCodigoBancoParaDivisa,
                    Monto = LibMath.Abs(VueltoEnMonedaLocal),
                    CodigoMoneda = vCodigoMonedaLocal,
                    CambioAMonedaLocal = 1
                });
            }
            if (VueltoC2p != 0) {
                vConsecutivoRenglon += 1;
                vRenglonesDeCobro.Add(new RenglonCobroDeFactura() {
                    ConsecutivoCompania = ConsecutivoCompania,
                    NumeroFactura = NumeroFactura,
                    TipoDeDocumento = LibConvert.EnumToDbValue((int)valTipoDeDocumento),
                    ConsecutivoRenglon = vConsecutivoRenglon,
                    CodigoFormaDelCobro = insRenglonCobroDeFactura.BuscarCodigoFormaDelCobro(eTipoDeFormaDePago.VueltoC2P),
                    CodigoBanco = valCodigoBancoParaMonedaLocal,
                    Monto = LibMath.Abs(VueltoC2p),
                    NumeroDocumentoAprobacion = numReferencia,
                    CodigoMoneda = vCodigoMonedaLocal,
                    CambioAMonedaLocal = 1,
                    InfoAdicional = LibConvert.ToStr(infoAdicional)
                });
            }
            if (VueltoEnDivisas != 0) {
                vConsecutivoRenglon += 1;
                vRenglonesDeCobro.Add(new RenglonCobroDeFactura() {
                    ConsecutivoCompania = ConsecutivoCompania,
                    NumeroFactura = NumeroFactura,
                    TipoDeDocumento = LibConvert.EnumToDbValue((int)valTipoDeDocumento),
                    ConsecutivoRenglon = vConsecutivoRenglon,
                    CodigoFormaDelCobro = insRenglonCobroDeFactura.BuscarCodigoFormaDelCobro(eTipoDeFormaDePago.VueltoEfectivo),
                    CodigoBanco = valCodigoBancoParaDivisa,
                    Monto = LibMath.Abs(VueltoEnDivisas),
                    CodigoMoneda = CodigoMonedaDivisa,
                    CambioAMonedaLocal = CambioAMonedaLocal
                });
            }
            foreach (CobroCobroMediosElectonicosVPOS vItem in ListaCobroCobroMediosElectonicosVPOS) {
                vConsecutivoRenglon += 1;
                vRenglonesDeCobro.Add(new RenglonCobroDeFactura() {
                    ConsecutivoCompania = ConsecutivoCompania,
                    NumeroFactura = NumeroFactura,
                    TipoDeDocumento = LibConvert.EnumToDbValue((int)valTipoDeDocumento),
                    ConsecutivoRenglon = vConsecutivoRenglon,
                    CodigoFormaDelCobro = vItem.CodigoFormaDelCobro,
                    CodigoBanco = LibString.S1IsEqualToS2(vCodigoMonedaLocal, vItem.MonedaTransaccion) ? valCodigoBancoParaMonedaLocal : valCodigoBancoParaDivisa,
                    Monto = vItem.MontoTransaccion,
                    NumeroDocumentoAprobacion = LibString.IsNullOrEmpty(vItem.NumReferencia) ? "" : vItem.NumReferencia,
                    CodigoMoneda = vItem.MonedaTransaccion,
                    CambioAMonedaLocal = LibString.S1IsEqualToS2(vCodigoMonedaLocal, vItem.MonedaTransaccion) ? 1 : CambioAMonedaLocal,
                    InfoAdicional = vItem.InfoAdicional
                });
            }
            return vRenglonesDeCobro;
        }

        private XElement CrearXmlDatosDelCobro(List<RenglonCobroDeFactura> valListaDeCobros) {
            XElement vXmlDatosDelCobro = new XElement("GpData");
            foreach (var Cobro in valListaDeCobros) {
                vXmlDatosDelCobro.Add(new XElement("GpResult",
                    new XElement("ConsecutivoCompania", Cobro.ConsecutivoCompania),
                    new XElement("NumeroFactura", Cobro.NumeroFactura),
                    new XElement("TipoDeDocumento", Cobro.TipoDeDocumentoAsString),
                    new XElement("ConsecutivoRenglon", Cobro.ConsecutivoRenglon),
                    new XElement("CodigoFormaDelCobro", Cobro.CodigoFormaDelCobro),
                    new XElement("CodigoBanco", Cobro.CodigoBanco),
                    new XElement("Monto", Cobro.Monto.ToString("#.##")),
                    new XElement("CodigoMoneda", Cobro.CodigoMoneda),
                    new XElement("CambioAMonedaLocal", Cobro.CambioAMonedaLocal.ToString("#.####")),
                    new XElement("NumeroDocumentoAprobacion", Cobro.NumeroDocumentoAprobacion),
                    new XElement("InfoAdicional", Cobro.InfoAdicional)));
            }
            return vXmlDatosDelCobro;
        }

        private XElement CrearXmlDatosIGTF() {
            XElement vXElementIGTF = new XElement("GpData", new XElement("GpResult",
                  new XElement("BaseImponibleIGTF", BaseImponibleIGTF.ToString("#.##")),
                  new XElement("IGTFML", IGTFML.ToString("#.##")),
                  new XElement("IGTFME", IGTFME.ToString("#.##")),
                  new XElement("AlicuotaIGTF", AlicuotaIGTF.ToString("#.###")),
                  new XElement("TotalAPagar", TotalAPagarML.ToString("#.##"))));
            return vXElementIGTF;
        }

        private string CrearListaVoucherMediosElectronicos() {
            string vPath = Path.Combine(new PagosElectronicosMngViewModel().RutaMegasoft, "vouchers");
            string vResult = "";
            if (_ImprimirComprobante) {
                if (_EsVueltoPagoMovil) {
                    vResult = Path.Combine(vPath, infoAdicional) + ",";
                } else {
                    foreach (var vVoucherName in ListaCobroCobroMediosElectonicosVPOS) {
                        vResult = vResult + "," + Path.Combine(vPath, Path.Combine(vPath, vVoucherName.InfoAdicional));
                    }
                }
            } else {
                vResult = "";
            }
            return vResult;
        }

        private void DeshabilitarControlesSegunTipoDeDocumento(eTipoDocumentoFactura valTipoDeDocumento) {
            IsVisibleTotalMediosElectronicos = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMediosElectronicosDeCobro");
            switch (valTipoDeDocumento) {
                case eTipoDocumentoFactura.Factura:
                    IsVisibleSeccionEfectivo = "Visible";
                    IsVisibleSeccionTarjeta = "Visible";
                    IsVisibleSeccionTransferencia = "Visible";
                    IsEnabledEfectivoDivisa = true;
                    if (!LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMediosElectronicosDeCobro")) {
                        RibbonData.RemoveRibbonGroup("Medios electrónicos");
                    }
                    break;
                case eTipoDocumentoFactura.NotaDeCredito:
                    IsVisibleSeccionEfectivo = "Visible";
                    IsVisibleSeccionTarjeta = "Collapsed";
                    IsVisibleSeccionTransferencia = "Collapsed";
                    EfectivoEnMonedaLocal = TotalFactura;
                    IsEnabledEfectivoDivisa = false;
                    RibbonData.RemoveRibbonGroup("Medios electrónicos");
                    break;
                case eTipoDocumentoFactura.NotaDeDebito:
                    IsVisibleSeccionEfectivo = "Visible";
                    IsVisibleSeccionTarjeta = "Visible";
                    IsVisibleSeccionTransferencia = "Visible";
                    IsEnabledEfectivoDivisa = true;
                    if (!LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMediosElectronicosDeCobro")) {
                        RibbonData.RemoveRibbonGroup("Medios electrónicos");
                    }
                    break;
                case eTipoDocumentoFactura.ResumenDiarioDeVentas:
                    break;
                case eTipoDocumentoFactura.NoAsignado:
                    break;
                case eTipoDocumentoFactura.ComprobanteFiscal:
                    IsVisibleSeccionEfectivo = "Visible";
                    IsVisibleSeccionTarjeta = "Visible";
                    IsVisibleSeccionTransferencia = "Visible";
                    IsEnabledEfectivoDivisa = true;
                    if (!LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMediosElectronicosDeCobro")) {
                        RibbonData.RemoveRibbonGroup("Medios electrónicos");
                    }
                    break;
                case eTipoDocumentoFactura.Boleta:
                    IsVisibleSeccionEfectivo = "Visible";
                    IsVisibleSeccionTarjeta = "Visible";
                    IsVisibleSeccionTransferencia = "Visible";
                    IsEnabledEfectivoDivisa = true;
                    RibbonData.RemoveRibbonGroup("Medios electrónicos");
                    break;
                case eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal:
                    IsVisibleSeccionEfectivo = "Visible";
                    IsVisibleSeccionTarjeta = "Collapsed";
                    IsVisibleSeccionTransferencia = "Collapsed";
                    EfectivoEnMonedaLocal = TotalFactura;
                    IsEnabledEfectivoDivisa = false;
                    RibbonData.RemoveRibbonGroup("Medios electrónicos");
                    break;
                case eTipoDocumentoFactura.NotaDeDebitoComprobanteFiscal:
                    IsVisibleSeccionEfectivo = "Visible";
                    IsVisibleSeccionTarjeta = "Visible";
                    IsVisibleSeccionTransferencia = "Visible";
                    IsEnabledEfectivoDivisa = true;
                    if (!LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaMediosElectronicosDeCobro")) {
                        RibbonData.RemoveRibbonGroup("Medios electrónicos");
                    }
                    break;
                case eTipoDocumentoFactura.NotaEntrega:                    
                    break;
                default:
                    break;
            }
        }

        private void ExecuteLimpiarCommand() {
            EfectivoEnMonedaLocal = 0;
            EfectivoEnDivisas = 0;
            TarjetaUno = 0;
            TarjetaDos = 0;
            TransferenciaEnMonedaLocal = 0;
            TransferenciaEnDivisas = 0;
            MontoCreditoElectronico = 0;
            VueltoEnMonedaLocal = 0;
            VueltoEnDivisas = 0;
            MontoRestantePorPagar = TotalFactura;
            MontoRestantePorPagarEnDivisas = TotalFacturaEnDivisas;
            RaiseMoveFocus(EfectivoEnMonedaLocalPropertyName);
        }
		
        private void InicializarValoresCreditoElectronico() {
            IsVisibleCreditoElectronico = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaCreditoElectronico");
            NombreCreditoElectronico = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreCreditoElectronico");
            CantidadCuotasUsualesCreditoElectronico  = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros", "CantidadCuotasUsualesCreditoElectronico");
            MaximaCantidadCuotasCreditoElectronico = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros", "MaximaCantidadCuotasCreditoElectronico");
        }
        #endregion
        #region Validaciones
        private ValidationResult MaximaCuotaCreditoEelctronicoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            string vNombreCreditoElectronico = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreCreditoElectronico");
            if (CantidadCuotasUsualesCreditoElectronico > MaximaCantidadCuotasCreditoElectronico ) {
                vResult = new ValidationResult("La cantidad de cuotas (" + CantidadCuotasUsualesCreditoElectronico  + ") es superior al máximo permitido (" + MaximaCantidadCuotasCreditoElectronico + ") para: " + vNombreCreditoElectronico);
            }
            if (IsVisibleCreditoElectronico && CantidadCuotasUsualesCreditoElectronico <= 0  && MontoCreditoElectronico > 0) {
                vResult = new ValidationResult("La cantidad de cuotas de " + vNombreCreditoElectronico + ", debe ser mayor a cero.");
            }
            return vResult;
        }

        private ValidationResult MontoCreditoElectronicoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            string vNombreCreditoElectronico = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombreCreditoElectronico");
            if (MontoCreditoElectronico >= TotalFacturaEnDivisas){

                vResult = new ValidationResult("El monto de: " + vNombreCreditoElectronico +  " debe ser menor al total de la factura.");
            }
            if (IsVisibleCreditoElectronico && CantidadCuotasUsualesCreditoElectronico > 0 && MontoCreditoElectronico < 0) {
                vResult = new ValidationResult("El monto de: " + vNombreCreditoElectronico + ", debe ser mayor a cero.");
            }
            return vResult;
        }
        #endregion
    }
}