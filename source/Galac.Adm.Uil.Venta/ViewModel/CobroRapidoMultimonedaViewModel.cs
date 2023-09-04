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

namespace Galac.Adm.Uil.Venta.ViewModel {
    public class CobroRapidoMultimonedaViewModel : CobroRapidoVzlaViewModelBase {
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
        private const string VueltoEnDivisasPropertyName = "VueltoEnDivisas";
        private const string MontoRestantePorPagarEnDivisasPropertyName = "MontoRestantePorPagarEnDivisas";
        private const string MontoRestantePorPagarEnMonedaLocalParaMostrarPropertyName = "MontoRestantePorPagarEnMonedaLocalParaMostrar";
        private const string MontoRestantePorPagarEnDivisasParaMostrarPropertyName = "MontoRestantePorPagarEnDivisasParaMostrar";
        private const string lblPorPagarYVueltoPropertyName = "lblPorPagarYVuelto";
        private const string ConexionCodigoMonedaPropertyName = "ConexionCodigoMoneda";
        private const string IsVisibleSeccionIGTFPropertyName = "IsVisibleSeccionIGTF";
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
        private const string TotalAPagarMLPropertyName = "TotalAPagarML";
        private const string TotalAPagarMEPropertyName = "TotalAPagarME";
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
        private decimal VueltoEfectivoDivisas { get; set; }
        private decimal VueltoC2pMonedaLocal { get; set; }

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
        
        public RelayCommand VueltoConPagoMovil { get; private set; }

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
                decimal vTotalPagosME = LibMath.RoundToNDecimals((EfectivoEnDivisas + TransferenciaEnDivisas) * CambioAMonedaLocal, 2);
                vTotalPagosME = IsVisibleSeccionIGTF ? vTotalPagosME : 0;
                return (TipoDeDocumento == eTipoDocumentoFactura.Factura || TipoDeDocumento == eTipoDocumentoFactura.ComprobanteFiscal) ? LibMath.RoundToNDecimals(System.Math.Min(TotalFactura, vTotalPagosME), 2) : 0;
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

        public decimal TotalAPagarME {
            get {
                return TotalFacturaEnDivisas + IGTFME;
            }
        }

        private bool IsVisibleSeccionIGTF {
            get {
                return _TipoDeContribuyenteIVA == eTipoDeContribuyenteDelIva.ContribuyenteEspecial && (TipoDeDocumento == eTipoDocumentoFactura.Factura || TipoDeDocumento == eTipoDocumentoFactura.ComprobanteFiscal);
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

        #endregion

        #region Constructores e Inicializaciores
        public CobroRapidoMultimonedaViewModel(int valConsecutivoCompania, string valNumeroDeDocumento, DateTime valFechaDeDocumento, decimal valTotalFactura, eTipoDocumentoFactura valTipoDeDocumento, string valCodigoMonedaDeLaFactura, string valCodigoMonedaDeCobro, bool valEsFacturaTradicional, decimal valAlicuotaIGTF, eTipoDeContribuyenteDelIva valTipoDeContribuyenteDelIva) {
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
        }

        public CobroRapidoMultimonedaViewModel(eAccionSR valAction, FacturaRapida valFactura, List<RenglonCobroDeFactura> valListDeCobroMaster, int valAlicuotaIvaASustituir, bool valEsFacturaTradicional, decimal valAlicuotaIGTF, eTipoDeContribuyenteDelIva valTipoDeContribuyenteDelIva) {
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
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            LimpiarCommand = new RelayCommand(ExecuteLimpiarCommand, CanExecuteLimpiarCommand);            
            VueltoConPagoMovil = new RelayCommand(ExecuteVueltoConPagoMovil, CanExecuteVueltoConPagoMovilCommand);
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
            //vResult.ControlDataCollection.Add(new LibRibbonButtonData() { 
            //    Label = "Vuelto con Pago Móvil",
            //    Command = VueltoConPagoMovil,
            //    LargeImage = new Uri("/Galac.Adm.Uil.Venta;component/Images/F10.png", UriKind.Relative),
            //    ToolTipDescription = "Datos del Vuelto con Pago Móvil C2P",
            //    ToolTipTitle = "Vuelto con PM C2P",
            //    IsVisible = true,
            //    KeyTip = "F10"
            //});
            return vResult;
        }

        #endregion

        #region Comandos
        protected override void ExecuteCobrarCommand() {
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
                    if (SeCobro != null)
                        SeCobro.Invoke(true);
                } else {
                    try {
                        insFactura.BaseImponibleIGTF = BaseImponibleIGTF;
                        insFactura.IGTFML = IGTFML;
                        insFactura.IGTFME = IGTFME;
                        insFactura.AlicuotaIGTF = AlicuotaIGTF;
                        insFactura.TotalAPagar = TotalAPagarML;
                        clsRenglonCobroDeFacturaNav vRenglonCobro = new clsRenglonCobroDeFacturaNav();
                        IRenglonCobroDeFacturaPdn vRenglonCobroDeFactura = new clsRenglonCobroDeFacturaNav();
                        DialogResult = vRenglonCobro.InsertChildRenglonCobroDeFactura(ConsecutivoCompania, NumeroFactura, eTipoDocumentoFactura.ComprobanteFiscal, vListaDecobro).Success;
                        if (DialogResult) {
                            vListaDecobro.RemoveAll(x => x.CodigoFormaDelCobro == insRenglonCobroDeFacturaPdn.BuscarCodigoFormaDelCobro(eTipoDeFormaDePago.VueltoEfectivo) || x.CodigoFormaDelCobro == insRenglonCobroDeFacturaPdn.BuscarCodigoFormaDelCobro(eTipoDeFormaDePago.VueltoC2P));
                            SeImprimio = ImprimirFacturaFiscal(vListaDecobro);
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
        }

        private void ExecuteLimpiarCommand() {
            EfectivoEnMonedaLocal = 0;
            EfectivoEnDivisas = 0;
            TarjetaUno = 0;
            TarjetaDos = 0;
            TransferenciaEnMonedaLocal = 0;
            TransferenciaEnDivisas = 0;
            VueltoEnMonedaLocal = 0;
            VueltoEnDivisas = 0;
            MontoRestantePorPagar = TotalFactura;
            MontoRestantePorPagarEnDivisas = TotalFacturaEnDivisas;
            RaiseMoveFocus(EfectivoEnMonedaLocalPropertyName);
        }        

        private void ExecuteVueltoConPagoMovil() {
            IC2PMegaSoftMng insVueltoMegasoft = (IC2PMegaSoftMng)new C2PMegasoftNav();
            //TODO:Se pasa código mientras tanto, va el nombre del cliente que aún no se recibe acá para pasarlo a la siguiente view
            insVueltoMegasoft.EjecutaVueltoPagoMovil(CodigoCliente, NumeroFactura, MontoRestantePorPagar);
            VueltoC2pMonedaLocal = insVueltoMegasoft.MontoVueltoPagoMovil;
            VueltoEnMonedaLocal = -1 * (VueltoEfectivoMonedaLocal + VueltoC2pMonedaLocal);
            NumeroControlVueltoPagoMovil = insVueltoMegasoft.NumeroControlVueltoPagoMovil;
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
            decimal TotalPagosME = LibMath.Abs(EfectivoEnDivisas) + LibMath.Abs(TransferenciaEnDivisas) - LibMath.Abs(VueltoEnDivisas);
            decimal TotalPagosML = LibMath.Abs(EfectivoEnMonedaLocal) + LibMath.Abs(TarjetaUno) + LibMath.Abs(TarjetaDos) + LibMath.Abs(TransferenciaEnMonedaLocal) - LibMath.Abs(VueltoEnMonedaLocal);
            vResult = ((TotalPagosML == 0) && (MontoRestantePorPagarEnDivisas <= 0))
                   || ((TotalPagosME == 0) && (MontoRestantePorPagar <= 0));
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
        //private bool CanExecuteVueltoEnEfectivoCommand() { return true; }
        private bool CanExecuteVueltoConPagoMovilCommand() { return true; }
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
            decimal TotalPagosMe = LibMath.Abs(EfectivoEnDivisas) + LibMath.Abs(TransferenciaEnDivisas) - LibMath.Abs(VueltoEnDivisas);
            decimal TotalPagoML = LibMath.Abs(EfectivoEnMonedaLocal) + LibMath.Abs(TarjetaUno) + LibMath.Abs(TarjetaDos) + LibMath.Abs(TransferenciaEnMonedaLocal) - LibMath.Abs(VueltoEnMonedaLocal);
            MontoRestantePorPagar = LibMath.RoundToNDecimals(TotalAPagarML - (TotalPagoML + LibMath.RoundToNDecimals(TotalPagosMe * CambioAMonedaLocal, 2)), 2);
            MontoRestantePorPagarEnDivisas = LibMath.RoundToNDecimals(MontoRestantePorPagar / CambioAMonedaLocal, 2);
            MontoRestantePorPagarEnMonedaLocalParaMostrar = SimboloMonedaLocal + ". " + LibConvert.ToStr(LibMath.Abs(MontoRestantePorPagar));
            MontoRestantePorPagarEnDivisasParaMostrar = SimboloDivisa + LibConvert.ToStr(LibMath.Abs(MontoRestantePorPagarEnDivisas));
            RaisePropertyChanged(lblPorPagarYVueltoPropertyName);
            RaisePropertyChanged(BaseImponibleIGTFPropertyName);
            RaisePropertyChanged(IGTFMLPropertyName);
            RaisePropertyChanged(IGTFMEPropertyName);
            RaisePropertyChanged(AlicuotaIGTFPropertyName);
            RaisePropertyChanged(TotalAPagarMLPropertyName);
            RaisePropertyChanged(TotalAPagarMEPropertyName);
        }

        private void AsignarTasaDeCambioDelDia(string valCodigoMoneda, DateTime valFechaDeVigencia) {
            decimal vTasa = 1;
            if (((ICambioPdn)new clsCambioNav()).ExisteTasaDeCambioParaElDia(valCodigoMoneda, valFechaDeVigencia, out vTasa)) {
                CambioAMonedaLocal = LibMath.RoundToNDecimals(vTasa, 4);
            } else {
                bool vElProgramaEstaEnModoAvanzado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsModoAvanzado");
                bool vUsarLimiteMaximoParaIngresoDeTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarLimiteMaximoParaIngresoDeTasaDeCambio");
                decimal vMaximoLimitePermitidoParaLaTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros", "MaximoLimitePermitidoParaLaTasaDeCambio");
                CambioViewModel vViewModel = new CambioViewModel(valCodigoMoneda, vUsarLimiteMaximoParaIngresoDeTasaDeCambio, vMaximoLimitePermitidoParaLaTasaDeCambio, vElProgramaEstaEnModoAvanzado);
                vViewModel.InitializeViewModel(eAccionSR.Insertar);
                vViewModel.OnCambioAMonedaLocalChanged += CambioChanged;
                vViewModel.FechaDeVigencia = valFechaDeVigencia;
                vViewModel.IsEnabledFecha = false;
                vViewModel.CodigoMoneda = valCodigoMoneda;
                vViewModel.NombreMoneda = NombreMonedaDivisa;
                bool result = LibMessages.EditViewModel.ShowEditor(vViewModel, true);
            }
        }

        private void CambioChanged(decimal valCambio) {
            CambioAMonedaLocal = LibMath.RoundToNDecimals(valCambio, 4);
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
            decimal TotalPagosML = LibMath.Abs(EfectivoEnMonedaLocal) + LibMath.Abs(TarjetaUno) + LibMath.Abs(TarjetaDos) + LibMath.Abs(TransferenciaEnMonedaLocal) - LibMath.Abs(VueltoEnMonedaLocal);
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
                        CodigoFormaDelCobro = "00001",
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
            if (VueltoC2pMonedaLocal != 0) {
                vConsecutivoRenglon += 1;
                vRenglonesDeCobro.Add(new RenglonCobroDeFactura() {
                    ConsecutivoCompania = ConsecutivoCompania,
                    NumeroFactura = NumeroFactura,
                    TipoDeDocumento = LibConvert.EnumToDbValue((int)valTipoDeDocumento),
                    ConsecutivoRenglon = vConsecutivoRenglon,
                    CodigoFormaDelCobro = insRenglonCobroDeFactura.BuscarCodigoFormaDelCobro(eTipoDeFormaDePago.VueltoC2P),
                    CodigoBanco = valCodigoBancoParaDivisa,
                    Monto = LibMath.Abs(VueltoC2pMonedaLocal),
                    CodigoMoneda = vCodigoMonedaLocal,
                    CambioAMonedaLocal = 1
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
                    new XElement("CambioAMonedaLocal", Cobro.CambioAMonedaLocal.ToString("#.####"))));
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

        private void DeshabilitarControlesSegunTipoDeDocumento(eTipoDocumentoFactura valTipoDeDocumento) {
            switch (valTipoDeDocumento) {
                case eTipoDocumentoFactura.Factura:
                    IsVisibleSeccionEfectivo = "Visible";
                    IsVisibleSeccionTarjeta = "Visible";
                    IsVisibleSeccionTransferencia = "Visible";
                    IsEnabledEfectivoDivisa = true;
                    break;
                case eTipoDocumentoFactura.NotaDeCredito:
                    IsVisibleSeccionEfectivo = "Visible";
                    IsVisibleSeccionTarjeta = "Collapsed";
                    IsVisibleSeccionTransferencia = "Collapsed";
                    EfectivoEnMonedaLocal = TotalFactura;
                    IsEnabledEfectivoDivisa = false;
                    break;
                case eTipoDocumentoFactura.NotaDeDebito:
                    IsVisibleSeccionEfectivo = "Visible";
                    IsVisibleSeccionTarjeta = "Visible";
                    IsVisibleSeccionTransferencia = "Visible";
                    IsEnabledEfectivoDivisa = true;
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
                    break;
                case eTipoDocumentoFactura.Boleta:
                    IsVisibleSeccionEfectivo = "Visible";
                    IsVisibleSeccionTarjeta = "Visible";
                    IsVisibleSeccionTransferencia = "Visible";
                    IsEnabledEfectivoDivisa = true;
                    break;
                case eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal:
                    IsVisibleSeccionEfectivo = "Visible";
                    IsVisibleSeccionTarjeta = "Collapsed";
                    IsVisibleSeccionTransferencia = "Collapsed";
                    EfectivoEnMonedaLocal = TotalFactura;
                    IsEnabledEfectivoDivisa = false;
                    break;
                case eTipoDocumentoFactura.NotaEntrega:
                    break;
                case eTipoDocumentoFactura.Todos:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}