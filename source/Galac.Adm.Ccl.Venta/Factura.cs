using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Ccl.Venta {
    [Serializable]
    public class Factura {
        #region Variables
        private int _ConsecutivoCompania;
        private string _Numero;
        private DateTime _Fecha;
        private string _CodigoCliente;
        private string _CodigoVendedor;
        private int _ConsecutivoVendedor;
        private string _Observaciones;
        private decimal _TotalMontoExento;
        private decimal _TotalBaseImponible;
        private decimal _TotalRenglones;
        private decimal _TotalIVA;
        private decimal _TotalFactura;
        private decimal _PorcentajeDescuento;
        private string _CodigoNota1;
        private string _CodigoNota2;
        private string _Moneda;
        private bool _NivelDePrecio;
        private bool _ReservarMercancia;
        private DateTime _FechaDeRetiro;
        private string _CodigoAlmacen;
        private bool _StatusFactura;
        private bool _TipoDeDocumento;
        private bool _InsertadaManualmente;
        private bool _FacturaHistorica;
        private bool _Cancelada;
        private bool _UsarDireccionFiscal;
        private int _NoDirDespachoAimprimir;
        private decimal _CambioABolivares;
        private decimal _MontoDelAbono;
        private DateTime _FechaDeVencimiento;
        private string _CondicionesDePago;
        private bool _FormaDeLaInicial;
        private decimal _PorcentajeDeLaInicial;
        private int _NumeroDeCuotas;
        private decimal _MontoDeLasCuotas;
        private decimal _MontoUltimaCuota;
        private bool _Talonario;
        private bool _FormaDePago;
        private int _NumDiasDeVencimiento1aCuota;
        private bool _EditarMontoCuota;
        private string _NumeroControl;
        private bool _TipoDeTransaccion;
        private string _NumeroFacturaAfectada;
        private string _NumeroPlanillaExportacion;
        private bool _TipoDeVenta;
        private bool _UsaMaquinaFiscal;
        private string _CodigoMaquinaRegistradora;
        private string _NumeroDesde;
        private string _NumeroHasta;
        private string _NumeroControlHasta;
        private decimal _MontoIvaRetenido;
        private DateTime _FechaAplicacionRetIVA;
        private int _NumeroComprobanteRetIVA;
        private DateTime _FechaComprobanteRetIVA;
        private bool _SeRetuvoIVA;
        private bool _FacturaConPreciosSinIva;
        private decimal _VueltoDelCobroDirecto;
        private int _ConsecutivoCaja;
        private bool _GeneraCobroDirecto;
        private DateTime _FechaDeFacturaAfectada;
        private DateTime _FechaDeEntrega;
        private decimal _PorcentajeDescuento1;
        private decimal _PorcentajeDescuento2;
        private decimal _MontoDescuento1;
        private decimal _MontoDescuento2;
        private string _CodigoLote;
        private bool _Devolucion;
        private decimal _PorcentajeAlicuota1;
        private decimal _PorcentajeAlicuota2;
        private decimal _PorcentajeAlicuota3;
        private decimal _MontoIVAAlicuota1;
        private decimal _MontoIVAAlicuota2;
        private decimal _MontoIVAAlicuota3;
        private decimal _MontoGravableAlicuota1;
        private decimal _MontoGravableAlicuota2;
        private decimal _MontoGravableAlicuota3;
        private bool _RealizoCierreZ;
        private string _NumeroComprobanteFiscal;
        private string _SerialMaquinaFiscal;
        private bool _AplicarPromocion;
        private bool _RealizoCierreX;
        private string _HoraModificacion;
        private bool _FormaDeCobro;
        private string _OtraFormaDeCobro;
        private string _NoCotizacionDeOrigen;
        private string _NoContrato;
        private int _ConsecutivoVehiculo;
        private int _ConsecutivoAlmacen;
        private string _NumeroResumenDiario;
        private string _NoControlDespachoDeOrigen;
        private bool _ImprimeFiscal;
        private bool _EsDiferida;
        private bool _EsOriginalmenteDiferida;
        private bool _SeContabilizoIvaDiferido;
        private bool _AplicaDecretoIvaEspecial;
        private bool _EsGeneradaPorPuntoDeVenta;
        private decimal _CambioMonedaCXC;
        private decimal _CambioMostrarTotalEnDivisas;
        private string _CodigoMonedaDeCobro;
        private bool _GeneradaPorNotaEntrega;
        private string _EmitidaEnFacturaNumero;
        private string _CodigoMoneda;
        private int _NumeroParaResumen;
        private int _NroDiasMantenerCambioAMonedaLocal;
        private DateTime _FechaLimiteCambioAMonedaLocal;
        private bool _GeneradoPor;
		private decimal _BaseImponibleIGTF;
        private decimal _IGTFML;
        private decimal _IGTFME;
        private decimal _AlicuotaIGTF;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
		//private ObservableCollection<RenglonFactura> _DetailRenglonFactura;
		private ObservableCollection<RenglonCobroDeFactura> _DetailRenglonCobroDeFactura;
        XmlDocument _datos;
        #endregion //Variables

        #region Propiedades
        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string Numero {
            get { return _Numero; }
            set { _Numero = LibString.Mid(value, 0, 11); }
        }

        public DateTime Fecha {
            get { return _Fecha; }
            set { _Fecha = LibConvert.DateToDbValue(value); }
        }

        public string CodigoCliente {
            get { return _CodigoCliente; }
            set { _CodigoCliente = LibString.Mid(value, 0, 10); }
        }
				
        public string CodigoVendedor {
            get { return _CodigoVendedor; }
            set { _CodigoVendedor = LibString.Mid(value, 0, 5); }
        }

        public int ConsecutivoVendedor {
            get { return _ConsecutivoVendedor; }
            set { _ConsecutivoVendedor = value; }
        }

        public string Observaciones {
            get { return _Observaciones; }
            set { _Observaciones = LibString.Mid(value, 0, 7000); }
        }

        public decimal TotalMontoExento {
            get { return _TotalMontoExento; }
            set { _TotalMontoExento = value; }
        }

        public decimal TotalBaseImponible {
            get { return _TotalBaseImponible; }
            set { _TotalBaseImponible = value; }
        }

        public decimal TotalRenglones {
            get { return _TotalRenglones; }
            set { _TotalRenglones = value; }
        }

        public decimal TotalIVA {
            get { return _TotalIVA; }
            set { _TotalIVA = value; }
        }

        public decimal TotalFactura {
            get { return _TotalFactura; }
            set { _TotalFactura = value; }
        }

        public decimal PorcentajeDescuento {
            get { return _PorcentajeDescuento; }
            set { _PorcentajeDescuento = value; }
        }

        public string CodigoNota1 {
            get { return _CodigoNota1; }
            set { _CodigoNota1 = LibString.Mid(value, 0, 10); }
        }

        public string CodigoNota2 {
            get { return _CodigoNota2; }
            set { _CodigoNota2 = LibString.Mid(value, 0, 10); }
        }

        public string Moneda {
            get { return _Moneda; }
            set { _Moneda = LibString.Mid(value, 0, 80); }
        }

        public bool NivelDePrecioAsBool {
            get { return _NivelDePrecio; }
            set { _NivelDePrecio = value; }
        }

        public string NivelDePrecio {
            set { _NivelDePrecio = LibConvert.SNToBool(value); }
        }


        public bool ReservarMercanciaAsBool {
            get { return _ReservarMercancia; }
            set { _ReservarMercancia = value; }
        }

        public string ReservarMercancia {
            set { _ReservarMercancia = LibConvert.SNToBool(value); }
        }

        public DateTime FechaDeRetiro {
            get { return _FechaDeRetiro; }
            set { _FechaDeRetiro = LibConvert.DateToDbValue(value); }
        }

        public string CodigoAlmacen {
            get { return _CodigoAlmacen; }
            set { _CodigoAlmacen = LibString.Mid(value, 0, 5); }
        }

        public bool StatusFacturaAsBool {
            get { return _StatusFactura; }
            set { _StatusFactura = value; }
        }

        public string StatusFactura {
            set { _StatusFactura = LibConvert.SNToBool(value); }
        }

        public bool TipoDeDocumentoAsBool {
            get { return _TipoDeDocumento; }
            set { _TipoDeDocumento = value; }
        }

        public string TipoDeDocumento {
            get { return LibConvert.BoolToSN(_TipoDeDocumento); }
            set { _TipoDeDocumento = LibConvert.SNToBool(value); }
        }

        public bool InsertadaManualmenteAsBool {
            get { return _InsertadaManualmente; }
            set { _InsertadaManualmente = value; }
        }

        public string InsertadaManualmente {
            set { _InsertadaManualmente = LibConvert.SNToBool(value); }
        }

        public bool FacturaHistoricaAsBool {
            get { return _FacturaHistorica; }
            set { _FacturaHistorica = value; }
        }

        public string FacturaHistorica {
            set { _FacturaHistorica = LibConvert.SNToBool(value); }
        }

        public bool CanceladaAsBool {
            get { return _Cancelada; }
            set { _Cancelada = value; }
        }

        public string Cancelada {
            set { _Cancelada = LibConvert.SNToBool(value); }
        }

        public bool UsarDireccionFiscalAsBool {
            get { return _UsarDireccionFiscal; }
            set { _UsarDireccionFiscal = value; }
        }

        public string UsarDireccionFiscal {
            set { _UsarDireccionFiscal = LibConvert.SNToBool(value); }
        }

        public int NoDirDespachoAimprimir {
            get { return _NoDirDespachoAimprimir; }
            set { _NoDirDespachoAimprimir = value; }
        }

        public decimal CambioABolivares {
            get { return _CambioABolivares; }
            set { _CambioABolivares = value; }
        }

        public decimal MontoDelAbono {
            get { return _MontoDelAbono; }
            set { _MontoDelAbono = value; }
        }

        public DateTime FechaDeVencimiento {
            get { return _FechaDeVencimiento; }
            set { _FechaDeVencimiento = LibConvert.DateToDbValue(value); }
        }

        public string CondicionesDePago {
            get { return _CondicionesDePago; }
            set { _CondicionesDePago = LibString.Mid(value, 0, 30); }
        }

        public bool FormaDeLaInicialAsBool {
            get { return _FormaDeLaInicial; }
            set { _FormaDeLaInicial = value; }
        }

        public string FormaDeLaInicial {
            set { _FormaDeLaInicial = LibConvert.SNToBool(value); }
        }

        public decimal PorcentajeDeLaInicial {
            get { return _PorcentajeDeLaInicial; }
            set { _PorcentajeDeLaInicial = value; }
        }

        public int NumeroDeCuotas {
            get { return _NumeroDeCuotas; }
            set { _NumeroDeCuotas = value; }
        }

        public decimal MontoDeLasCuotas {
            get { return _MontoDeLasCuotas; }
            set { _MontoDeLasCuotas = value; }
        }

        public decimal MontoUltimaCuota {
            get { return _MontoUltimaCuota; }
            set { _MontoUltimaCuota = value; }
        }

        public bool TalonarioAsBool {
            get { return _Talonario; }
            set { _Talonario = value; }
        }

        public string Talonario {
            set { _Talonario = LibConvert.SNToBool(value); }
        }

        public bool FormaDePagoAsBool {
            get { return _FormaDePago; }
            set { _FormaDePago = value; }
        }

        public string FormaDePago {
            set { _FormaDePago = LibConvert.SNToBool(value); }
        }

        public int NumDiasDeVencimiento1aCuota {
            get { return _NumDiasDeVencimiento1aCuota; }
            set { _NumDiasDeVencimiento1aCuota = value; }
        }

        public bool EditarMontoCuotaAsBool {
            get { return _EditarMontoCuota; }
            set { _EditarMontoCuota = value; }
        }

        public string EditarMontoCuota {
            set { _EditarMontoCuota = LibConvert.SNToBool(value); }
        }

        public string NumeroControl {
            get { return _NumeroControl; }
            set { _NumeroControl = LibString.Mid(value, 0, 11); }
        }

        public bool TipoDeTransaccionAsBool {
            get { return _TipoDeTransaccion; }
            set { _TipoDeTransaccion = value; }
        }

        public string TipoDeTransaccion {
            set { _TipoDeTransaccion = LibConvert.SNToBool(value); }
        }

        public string NumeroFacturaAfectada {
            get { return _NumeroFacturaAfectada; }
            set { _NumeroFacturaAfectada = LibString.Mid(value, 0, 11); }
        }

        public string NumeroPlanillaExportacion {
            get { return _NumeroPlanillaExportacion; }
            set { _NumeroPlanillaExportacion = LibString.Mid(value, 0, 20); }
        }

        public bool TipoDeVentaAsBool {
            get { return _TipoDeVenta; }
            set { _TipoDeVenta = value; }
        }

        public string TipoDeVenta {
            set { _TipoDeVenta = LibConvert.SNToBool(value); }
        }

        public bool UsaMaquinaFiscalAsBool {
            get { return _UsaMaquinaFiscal; }
            set { _UsaMaquinaFiscal = value; }
        }

        public string UsaMaquinaFiscal {
            set { _UsaMaquinaFiscal = LibConvert.SNToBool(value); }
        }

        public string CodigoMaquinaRegistradora {
            get { return _CodigoMaquinaRegistradora; }
            set { _CodigoMaquinaRegistradora = LibString.Mid(value, 0, 9); }
        }

        public string NumeroDesde {
            get { return _NumeroDesde; }
            set { _NumeroDesde = LibString.Mid(value, 0, 20); }
        }

        public string NumeroHasta {
            get { return _NumeroHasta; }
            set { _NumeroHasta = LibString.Mid(value, 0, 20); }
        }

        public string NumeroControlHasta {
            get { return _NumeroControlHasta; }
            set { _NumeroControlHasta = LibString.Mid(value, 0, 11); }
        }

        public decimal MontoIvaRetenido {
            get { return _MontoIvaRetenido; }
            set { _MontoIvaRetenido = value; }
        }

        public DateTime FechaAplicacionRetIVA {
            get { return _FechaAplicacionRetIVA; }
            set { _FechaAplicacionRetIVA = LibConvert.DateToDbValue(value); }
        }

        public int NumeroComprobanteRetIVA {
            get { return _NumeroComprobanteRetIVA; }
            set { _NumeroComprobanteRetIVA = value; }
        }

        public DateTime FechaComprobanteRetIVA {
            get { return _FechaComprobanteRetIVA; }
            set { _FechaComprobanteRetIVA = LibConvert.DateToDbValue(value); }
        }

        public bool SeRetuvoIVAAsBool {
            get { return _SeRetuvoIVA; }
            set { _SeRetuvoIVA = value; }
        }

        public string SeRetuvoIVA {
            set { _SeRetuvoIVA = LibConvert.SNToBool(value); }
        }

        public bool FacturaConPreciosSinIvaAsBool {
            get { return _FacturaConPreciosSinIva; }
            set { _FacturaConPreciosSinIva = value; }
        }

        public string FacturaConPreciosSinIva {
            set { _FacturaConPreciosSinIva = LibConvert.SNToBool(value); }
        }

        public decimal VueltoDelCobroDirecto {
            get { return _VueltoDelCobroDirecto; }
            set { _VueltoDelCobroDirecto = value; }
        }

        public int ConsecutivoCaja {
            get { return _ConsecutivoCaja; }
            set { _ConsecutivoCaja = value; }
        }

        public bool GeneraCobroDirectoAsBool {
            get { return _GeneraCobroDirecto; }
            set { _GeneraCobroDirecto = value; }
        }

        public string GeneraCobroDirecto {
            set { _GeneraCobroDirecto = LibConvert.SNToBool(value); }
        }

        public DateTime FechaDeFacturaAfectada {
            get { return _FechaDeFacturaAfectada; }
            set { _FechaDeFacturaAfectada = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaDeEntrega {
            get { return _FechaDeEntrega; }
            set { _FechaDeEntrega = LibConvert.DateToDbValue(value); }
        }

        public decimal PorcentajeDescuento1 {
            get { return _PorcentajeDescuento1; }
            set { _PorcentajeDescuento1 = value; }
        }

        public decimal PorcentajeDescuento2 {
            get { return _PorcentajeDescuento2; }
            set { _PorcentajeDescuento2 = value; }
        }

        public decimal MontoDescuento1 {
            get { return _MontoDescuento1; }
            set { _MontoDescuento1 = value; }
        }

        public decimal MontoDescuento2 {
            get { return _MontoDescuento2; }
            set { _MontoDescuento2 = value; }
        }

        public string CodigoLote {
            get { return _CodigoLote; }
            set { _CodigoLote = LibString.Mid(value, 0, 10); }
        }

        public bool DevolucionAsBool {
            get { return _Devolucion; }
            set { _Devolucion = value; }
        }

        public string Devolucion {
            set { _Devolucion = LibConvert.SNToBool(value); }
        }

        public decimal PorcentajeAlicuota1 {
            get { return _PorcentajeAlicuota1; }
            set { _PorcentajeAlicuota1 = value; }
        }

        public decimal PorcentajeAlicuota2 {
            get { return _PorcentajeAlicuota2; }
            set { _PorcentajeAlicuota2 = value; }
        }

        public decimal PorcentajeAlicuota3 {
            get { return _PorcentajeAlicuota3; }
            set { _PorcentajeAlicuota3 = value; }
        }

        public decimal MontoIVAAlicuota1 {
            get { return _MontoIVAAlicuota1; }
            set { _MontoIVAAlicuota1 = value; }
        }

        public decimal MontoIVAAlicuota2 {
            get { return _MontoIVAAlicuota2; }
            set { _MontoIVAAlicuota2 = value; }
        }

        public decimal MontoIVAAlicuota3 {
            get { return _MontoIVAAlicuota3; }
            set { _MontoIVAAlicuota3 = value; }
        }

        public decimal MontoGravableAlicuota1 {
            get { return _MontoGravableAlicuota1; }
            set { _MontoGravableAlicuota1 = value; }
        }

        public decimal MontoGravableAlicuota2 {
            get { return _MontoGravableAlicuota2; }
            set { _MontoGravableAlicuota2 = value; }
        }

        public decimal MontoGravableAlicuota3 {
            get { return _MontoGravableAlicuota3; }
            set { _MontoGravableAlicuota3 = value; }
        }

        public bool RealizoCierreZAsBool {
            get { return _RealizoCierreZ; }
            set { _RealizoCierreZ = value; }
        }

        public string RealizoCierreZ {
            set { _RealizoCierreZ = LibConvert.SNToBool(value); }
        }

        public string NumeroComprobanteFiscal {
            get { return _NumeroComprobanteFiscal; }
            set { _NumeroComprobanteFiscal = LibString.Mid(value, 0, 12); }
        }

        public string SerialMaquinaFiscal {
            get { return _SerialMaquinaFiscal; }
            set { _SerialMaquinaFiscal = LibString.Mid(value, 0, 15); }
        }

        public bool AplicarPromocionAsBool {
            get { return _AplicarPromocion; }
            set { _AplicarPromocion = value; }
        }

        public string AplicarPromocion {
            set { _AplicarPromocion = LibConvert.SNToBool(value); }
        }

        public bool RealizoCierreXAsBool {
            get { return _RealizoCierreX; }
            set { _RealizoCierreX = value; }
        }

        public string RealizoCierreX {
            set { _RealizoCierreX = LibConvert.SNToBool(value); }
        }

        public string HoraModificacion {
            get { return _HoraModificacion; }
            set { _HoraModificacion = LibString.Mid(value, 0, 5); }
        }

        public bool FormaDeCobroAsBool {
            get { return _FormaDeCobro; }
            set { _FormaDeCobro = value; }
        }

        public string FormaDeCobro {
            set { _FormaDeCobro = LibConvert.SNToBool(value); }
        }

        public string OtraFormaDeCobro {
            get { return _OtraFormaDeCobro; }
            set { _OtraFormaDeCobro = LibString.Mid(value, 0, 20); }
        }

        public string NoCotizacionDeOrigen {
            get { return _NoCotizacionDeOrigen; }
            set { _NoCotizacionDeOrigen = LibString.Mid(value, 0, 20); }
        }

        public string NoContrato {
            get { return _NoContrato; }
            set { _NoContrato = LibString.Mid(value, 0, 5); }
        }

        public int ConsecutivoVehiculo {
            get { return _ConsecutivoVehiculo; }
            set { _ConsecutivoVehiculo = value; }
        }

        public int ConsecutivoAlmacen {
            get { return _ConsecutivoAlmacen; }
            set { _ConsecutivoAlmacen = value; }
        }

        public string NumeroResumenDiario {
            get { return _NumeroResumenDiario; }
            set { _NumeroResumenDiario = LibString.Mid(value, 0, 8); }
        }

        public string NoControlDespachoDeOrigen {
            get { return _NoControlDespachoDeOrigen; }
            set { _NoControlDespachoDeOrigen = LibString.Mid(value, 0, 30); }
        }

        public bool ImprimeFiscalAsBool {
            get { return _ImprimeFiscal; }
            set { _ImprimeFiscal = value; }
        }

        public string ImprimeFiscal {
            set { _ImprimeFiscal = LibConvert.SNToBool(value); }
        }

        public bool EsDiferidaAsBool {
            get { return _EsDiferida; }
            set { _EsDiferida = value; }
        }

        public string EsDiferida {
            set { _EsDiferida = LibConvert.SNToBool(value); }
        }

        public bool EsOriginalmenteDiferidaAsBool {
            get { return _EsOriginalmenteDiferida; }
            set { _EsOriginalmenteDiferida = value; }
        }

        public string EsOriginalmenteDiferida {
            set { _EsOriginalmenteDiferida = LibConvert.SNToBool(value); }
        }

        public bool SeContabilizoIvaDiferidoAsBool {
            get { return _SeContabilizoIvaDiferido; }
            set { _SeContabilizoIvaDiferido = value; }
        }

        public string SeContabilizoIvaDiferido {
            set { _SeContabilizoIvaDiferido = LibConvert.SNToBool(value); }
        }

        public bool AplicaDecretoIvaEspecialAsBool {
            get { return _AplicaDecretoIvaEspecial; }
            set { _AplicaDecretoIvaEspecial = value; }
        }

        public string AplicaDecretoIvaEspecial {
            set { _AplicaDecretoIvaEspecial = LibConvert.SNToBool(value); }
        }

        public bool EsGeneradaPorPuntoDeVentaAsBool {
            get { return _EsGeneradaPorPuntoDeVenta; }
            set { _EsGeneradaPorPuntoDeVenta = value; }
        }

        public string EsGeneradaPorPuntoDeVenta {
            set { _EsGeneradaPorPuntoDeVenta = LibConvert.SNToBool(value); }
        }

        public decimal CambioMonedaCXC {
            get { return _CambioMonedaCXC; }
            set { _CambioMonedaCXC = value; }
        }

        public decimal CambioMostrarTotalEnDivisas {
            get { return _CambioMostrarTotalEnDivisas; }
            set { _CambioMostrarTotalEnDivisas = value; }
        }

        public string CodigoMonedaDeCobro {
            get { return _CodigoMonedaDeCobro; }
            set { _CodigoMonedaDeCobro = LibString.Mid(value, 0, 4); }
        }

        public bool GeneradaPorNotaEntregaAsBool {
            get { return _GeneradaPorNotaEntrega; }
            set { _GeneradaPorNotaEntrega = value; }
        }

        public string GeneradaPorNotaEntrega {
            set { _GeneradaPorNotaEntrega = LibConvert.SNToBool(value); }
        }

        public string EmitidaEnFacturaNumero {
            get { return _EmitidaEnFacturaNumero; }
            set { _EmitidaEnFacturaNumero = LibString.Mid(value, 0, 11); }
        }

        public string CodigoMoneda {
            get { return _CodigoMoneda; }
            set { _CodigoMoneda = LibString.Mid(value, 0, 4); }
        }

        public int NumeroParaResumen {
            get { return _NumeroParaResumen; }
            set { _NumeroParaResumen = value; }
        }

        public int NroDiasMantenerCambioAMonedaLocal {
            get { return _NroDiasMantenerCambioAMonedaLocal; }
            set { _NroDiasMantenerCambioAMonedaLocal = value; }
        }

        public DateTime FechaLimiteCambioAMonedaLocal {
            get { return _FechaLimiteCambioAMonedaLocal; }
            set { _FechaLimiteCambioAMonedaLocal = LibConvert.DateToDbValue(value); }
        }

        public bool GeneradoPorAsBool {
            get { return _GeneradoPor; }
            set { _GeneradoPor = value; }
        }

        public string GeneradoPor {
            set { _GeneradoPor = LibConvert.SNToBool(value); }
        }
		
		public decimal BaseImponibleIGTF {
            get { return _BaseImponibleIGTF; }
            set { _BaseImponibleIGTF = value; }
        }

        public decimal IGTFML {
            get { return _IGTFML; }
            set { _IGTFML = value; }
        }

        public decimal IGTFME {
            get { return _IGTFME; }
            set { _IGTFME = value; }
        }

        public decimal AlicuotaIGTF {
            get { return _AlicuotaIGTF; }
            set { _AlicuotaIGTF = value; }
        }
		
        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 10); }
        }

        public DateTime FechaUltimaModificacion {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = LibConvert.DateToDbValue(value); }
        }

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        //public ObservableCollection<RenglonFactura> DetailRenglonFactura {
        //    get { return _DetailRenglonFactura; }
        //    set { _DetailRenglonFactura = value; }
        //}

        public ObservableCollection<RenglonCobroDeFactura> DetailRenglonCobroDeFactura {
            get { return _DetailRenglonCobroDeFactura; }
            set { _DetailRenglonCobroDeFactura = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades

        #region Constructores
        public Factura() {
            //_DetailRenglonFactura = new ObservableCollection<RenglonFactura>();
            _DetailRenglonCobroDeFactura = new ObservableCollection<RenglonCobroDeFactura>();
            Clear();
        }
        #endregion //Constructores

        #region Metodos Generados
        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            Numero = string.Empty;
            Fecha = LibDate.Today();
            CodigoCliente = string.Empty;
            CodigoVendedor = string.Empty;
            ConsecutivoVendedor = 0;
            Observaciones = string.Empty;
            TotalMontoExento = 0;
            TotalBaseImponible = 0;
            TotalRenglones = 0;
            TotalIVA = 0;
            TotalFactura = 0;
            PorcentajeDescuento = 0;
            CodigoNota1 = string.Empty;
            CodigoNota2 = string.Empty;
            Moneda = string.Empty;
            NivelDePrecioAsBool = false;
            ReservarMercanciaAsBool = false;
            FechaDeRetiro = LibDate.Today();
            CodigoAlmacen = string.Empty;
            StatusFacturaAsBool = false;
            TipoDeDocumentoAsBool = false;
            InsertadaManualmenteAsBool = false;
            FacturaHistoricaAsBool = false;
            CanceladaAsBool = false;
            UsarDireccionFiscalAsBool = false;
            NoDirDespachoAimprimir = 0;
            CambioABolivares = 0;
            MontoDelAbono = 0;
            FechaDeVencimiento = LibDate.Today();
            CondicionesDePago = string.Empty;
            FormaDeLaInicialAsBool = false;
            PorcentajeDeLaInicial = 0;
            NumeroDeCuotas = 0;
            MontoDeLasCuotas = 0;
            MontoUltimaCuota = 0;
            TalonarioAsBool = false;
            FormaDePagoAsBool = false;
            NumDiasDeVencimiento1aCuota = 0;
            EditarMontoCuotaAsBool = false;
            NumeroControl = string.Empty;
            TipoDeTransaccionAsBool = false;
            NumeroFacturaAfectada = string.Empty;
            NumeroPlanillaExportacion = string.Empty;
            TipoDeVentaAsBool = false;
            UsaMaquinaFiscalAsBool = false;
            CodigoMaquinaRegistradora = string.Empty;
            NumeroDesde = string.Empty;
            NumeroHasta = string.Empty;
            NumeroControlHasta = string.Empty;
            MontoIvaRetenido = 0;
            FechaAplicacionRetIVA = LibDate.Today();
            NumeroComprobanteRetIVA = 0;
            FechaComprobanteRetIVA = LibDate.Today();
            SeRetuvoIVAAsBool = false;
            FacturaConPreciosSinIvaAsBool = false;
            VueltoDelCobroDirecto = 0;
            ConsecutivoCaja = 0;
            GeneraCobroDirectoAsBool = false;
            FechaDeFacturaAfectada = LibDate.Today();
            FechaDeEntrega = LibDate.Today();
            PorcentajeDescuento1 = 0;
            PorcentajeDescuento2 = 0;
            MontoDescuento1 = 0;
            MontoDescuento2 = 0;
            CodigoLote = string.Empty;
            DevolucionAsBool = false;
            PorcentajeAlicuota1 = 0;
            PorcentajeAlicuota2 = 0;
            PorcentajeAlicuota3 = 0;
            MontoIVAAlicuota1 = 0;
            MontoIVAAlicuota2 = 0;
            MontoIVAAlicuota3 = 0;
            MontoGravableAlicuota1 = 0;
            MontoGravableAlicuota2 = 0;
            MontoGravableAlicuota3 = 0;
            RealizoCierreZAsBool = false;
            NumeroComprobanteFiscal = string.Empty;
            SerialMaquinaFiscal = string.Empty;
            AplicarPromocionAsBool = false;
            RealizoCierreXAsBool = false;
            HoraModificacion = string.Empty;
            FormaDeCobroAsBool = false;
            OtraFormaDeCobro = string.Empty;
            NoCotizacionDeOrigen = string.Empty;
            NoContrato = string.Empty;
            ConsecutivoVehiculo = 0;
            ConsecutivoAlmacen = 0;
            NumeroResumenDiario = string.Empty;
            NoControlDespachoDeOrigen = string.Empty;
            ImprimeFiscalAsBool = false;
            EsDiferidaAsBool = false;
            EsOriginalmenteDiferidaAsBool = false;
            SeContabilizoIvaDiferidoAsBool = false;
            AplicaDecretoIvaEspecialAsBool = false;
            EsGeneradaPorPuntoDeVentaAsBool = false;
            CambioMonedaCXC = (1);
            CambioMostrarTotalEnDivisas = (1);
            CodigoMonedaDeCobro = string.Empty;
            GeneradaPorNotaEntregaAsBool = false;
            EmitidaEnFacturaNumero = string.Empty;
            CodigoMoneda = string.Empty;
            NumeroParaResumen = 0;
            NroDiasMantenerCambioAMonedaLocal = 0;
            FechaLimiteCambioAMonedaLocal = LibDate.Today();
            GeneradoPorAsBool = false;
			BaseImponibleIGTF = 0;
            IGTFML = 0;
            IGTFME = 0;
            AlicuotaIGTF = 0;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
            //DetailRenglonFactura = new ObservableCollection<RenglonFactura>();
            DetailRenglonCobroDeFactura = new ObservableCollection<RenglonCobroDeFactura>();
        }

        public Factura Clone() {
            Factura vResult = new Factura();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Numero = _Numero;
            vResult.Fecha = _Fecha;
            vResult.CodigoCliente = _CodigoCliente;
            vResult.CodigoVendedor = _CodigoVendedor;
            vResult.ConsecutivoVendedor = _ConsecutivoVendedor;
            vResult.Observaciones = _Observaciones;
            vResult.TotalMontoExento = _TotalMontoExento;
            vResult.TotalBaseImponible = _TotalBaseImponible;
            vResult.TotalRenglones = _TotalRenglones;
            vResult.TotalIVA = _TotalIVA;
            vResult.TotalFactura = _TotalFactura;
            vResult.PorcentajeDescuento = _PorcentajeDescuento;
            vResult.CodigoNota1 = _CodigoNota1;
            vResult.CodigoNota2 = _CodigoNota2;
            vResult.Moneda = _Moneda;
            vResult.NivelDePrecioAsBool = _NivelDePrecio;
            vResult.ReservarMercanciaAsBool = _ReservarMercancia;
            vResult.FechaDeRetiro = _FechaDeRetiro;
            vResult.CodigoAlmacen = _CodigoAlmacen;
            vResult.StatusFacturaAsBool = _StatusFactura;
            vResult.TipoDeDocumentoAsBool = _TipoDeDocumento;
            vResult.InsertadaManualmenteAsBool = _InsertadaManualmente;
            vResult.FacturaHistoricaAsBool = _FacturaHistorica;
            vResult.CanceladaAsBool = _Cancelada;
            vResult.UsarDireccionFiscalAsBool = _UsarDireccionFiscal;
            vResult.NoDirDespachoAimprimir = _NoDirDespachoAimprimir;
            vResult.CambioABolivares = _CambioABolivares;
            vResult.MontoDelAbono = _MontoDelAbono;
            vResult.FechaDeVencimiento = _FechaDeVencimiento;
            vResult.CondicionesDePago = _CondicionesDePago;
            vResult.FormaDeLaInicialAsBool = _FormaDeLaInicial;
            vResult.PorcentajeDeLaInicial = _PorcentajeDeLaInicial;
            vResult.NumeroDeCuotas = _NumeroDeCuotas;
            vResult.MontoDeLasCuotas = _MontoDeLasCuotas;
            vResult.MontoUltimaCuota = _MontoUltimaCuota;
            vResult.TalonarioAsBool = _Talonario;
            vResult.FormaDePagoAsBool = _FormaDePago;
            vResult.NumDiasDeVencimiento1aCuota = _NumDiasDeVencimiento1aCuota;
            vResult.EditarMontoCuotaAsBool = _EditarMontoCuota;
            vResult.NumeroControl = _NumeroControl;
            vResult.TipoDeTransaccionAsBool = _TipoDeTransaccion;
            vResult.NumeroFacturaAfectada = _NumeroFacturaAfectada;
            vResult.NumeroPlanillaExportacion = _NumeroPlanillaExportacion;
            vResult.TipoDeVentaAsBool = _TipoDeVenta;
            vResult.UsaMaquinaFiscalAsBool = _UsaMaquinaFiscal;
            vResult.CodigoMaquinaRegistradora = _CodigoMaquinaRegistradora;
            vResult.NumeroDesde = _NumeroDesde;
            vResult.NumeroHasta = _NumeroHasta;
            vResult.NumeroControlHasta = _NumeroControlHasta;
            vResult.MontoIvaRetenido = _MontoIvaRetenido;
            vResult.FechaAplicacionRetIVA = _FechaAplicacionRetIVA;
            vResult.NumeroComprobanteRetIVA = _NumeroComprobanteRetIVA;
            vResult.FechaComprobanteRetIVA = _FechaComprobanteRetIVA;
            vResult.SeRetuvoIVAAsBool = _SeRetuvoIVA;
            vResult.FacturaConPreciosSinIvaAsBool = _FacturaConPreciosSinIva;
            vResult.VueltoDelCobroDirecto = _VueltoDelCobroDirecto;
            vResult.ConsecutivoCaja = _ConsecutivoCaja;
            vResult.GeneraCobroDirectoAsBool = _GeneraCobroDirecto;
            vResult.FechaDeFacturaAfectada = _FechaDeFacturaAfectada;
            vResult.FechaDeEntrega = _FechaDeEntrega;
            vResult.PorcentajeDescuento1 = _PorcentajeDescuento1;
            vResult.PorcentajeDescuento2 = _PorcentajeDescuento2;
            vResult.MontoDescuento1 = _MontoDescuento1;
            vResult.MontoDescuento2 = _MontoDescuento2;
            vResult.CodigoLote = _CodigoLote;
            vResult.DevolucionAsBool = _Devolucion;
            vResult.PorcentajeAlicuota1 = _PorcentajeAlicuota1;
            vResult.PorcentajeAlicuota2 = _PorcentajeAlicuota2;
            vResult.PorcentajeAlicuota3 = _PorcentajeAlicuota3;
            vResult.MontoIVAAlicuota1 = _MontoIVAAlicuota1;
            vResult.MontoIVAAlicuota2 = _MontoIVAAlicuota2;
            vResult.MontoIVAAlicuota3 = _MontoIVAAlicuota3;
            vResult.MontoGravableAlicuota1 = _MontoGravableAlicuota1;
            vResult.MontoGravableAlicuota2 = _MontoGravableAlicuota2;
            vResult.MontoGravableAlicuota3 = _MontoGravableAlicuota3;
            vResult.RealizoCierreZAsBool = _RealizoCierreZ;
            vResult.NumeroComprobanteFiscal = _NumeroComprobanteFiscal;
            vResult.SerialMaquinaFiscal = _SerialMaquinaFiscal;
            vResult.AplicarPromocionAsBool = _AplicarPromocion;
            vResult.RealizoCierreXAsBool = _RealizoCierreX;
            vResult.HoraModificacion = _HoraModificacion;
            vResult.FormaDeCobroAsBool = _FormaDeCobro;
            vResult.OtraFormaDeCobro = _OtraFormaDeCobro;
            vResult.NoCotizacionDeOrigen = _NoCotizacionDeOrigen;
            vResult.NoContrato = _NoContrato;
            vResult.ConsecutivoVehiculo = _ConsecutivoVehiculo;
            vResult.ConsecutivoAlmacen = _ConsecutivoAlmacen;
            vResult.NumeroResumenDiario = _NumeroResumenDiario;
            vResult.NoControlDespachoDeOrigen = _NoControlDespachoDeOrigen;
            vResult.ImprimeFiscalAsBool = _ImprimeFiscal;
            vResult.EsDiferidaAsBool = _EsDiferida;
            vResult.EsOriginalmenteDiferidaAsBool = _EsOriginalmenteDiferida;
            vResult.SeContabilizoIvaDiferidoAsBool = _SeContabilizoIvaDiferido;
            vResult.AplicaDecretoIvaEspecialAsBool = _AplicaDecretoIvaEspecial;
            vResult.EsGeneradaPorPuntoDeVentaAsBool = _EsGeneradaPorPuntoDeVenta;
            vResult.CambioMonedaCXC = _CambioMonedaCXC;
            vResult.CambioMostrarTotalEnDivisas = _CambioMostrarTotalEnDivisas;
            vResult.CodigoMonedaDeCobro = _CodigoMonedaDeCobro;
            vResult.GeneradaPorNotaEntregaAsBool = _GeneradaPorNotaEntrega;
            vResult.EmitidaEnFacturaNumero = _EmitidaEnFacturaNumero;
            vResult.CodigoMoneda = _CodigoMoneda;
            vResult.NumeroParaResumen = _NumeroParaResumen;
            vResult.NroDiasMantenerCambioAMonedaLocal = _NroDiasMantenerCambioAMonedaLocal;
            vResult.FechaLimiteCambioAMonedaLocal = _FechaLimiteCambioAMonedaLocal;
            vResult.BaseImponibleIGTF = _BaseImponibleIGTF;
            vResult.IGTFML = _IGTFML;
            vResult.IGTFME = _IGTFME;
            vResult.AlicuotaIGTF = _AlicuotaIGTF;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nNumero = " + _Numero +
               "\nFecha = " + _Fecha.ToShortDateString() +
               "\nCodigo Cliente = " + _CodigoCliente +
               "\nCodigo Vendedor = " + _CodigoVendedor +
               "\nConsecutivo del Vendedor = " + _ConsecutivoVendedor.ToString() +
               "\nObservaciones = " + _Observaciones +
               "\nTotal Monto Exento = " + _TotalMontoExento.ToString() +
               "\nTotal Base Imponible = " + _TotalBaseImponible.ToString() +
               "\nTotal Renglones = " + _TotalRenglones.ToString() +
               "\nTotal IVA = " + _TotalIVA.ToString() +
               "\nTotal Factura = " + _TotalFactura.ToString() +
               "\nPorcentaje Descuento = " + _PorcentajeDescuento.ToString() +
               "\nCodigo Nota 1 = " + _CodigoNota1 +
               "\nCodigo Nota 2 = " + _CodigoNota2 +
               "\nMoneda = " + _Moneda +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _NivelDePrecio +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _ReservarMercancia +
               "\nFecha De Retiro = " + _FechaDeRetiro.ToShortDateString() +
               "\nCodigo Almacen = " + _CodigoAlmacen +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _StatusFactura +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _TipoDeDocumento +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _InsertadaManualmente +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _FacturaHistorica +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _Cancelada +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _UsarDireccionFiscal +
               "\nNo Dir Despacho Aimprimir = " + _NoDirDespachoAimprimir.ToString() +
               "\nCambio ABolivares = " + _CambioABolivares.ToString() +
               "\nMonto Del Abono = " + _MontoDelAbono.ToString() +
               "\nFecha De Vencimiento = " + _FechaDeVencimiento.ToShortDateString() +
               "\nCondiciones De Pago = " + _CondicionesDePago +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _FormaDeLaInicial +
               "\nPorcentaje De La Inicial = " + _PorcentajeDeLaInicial.ToString() +
               "\nNumero De Cuotas = " + _NumeroDeCuotas.ToString() +
               "\nMonto De Las Cuotas = " + _MontoDeLasCuotas.ToString() +
               "\nMonto Ultima Cuota = " + _MontoUltimaCuota.ToString() +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _Talonario +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _FormaDePago +
               "\nNum Dias De Vencimiento 1a Cuota = " + _NumDiasDeVencimiento1aCuota.ToString() +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _EditarMontoCuota +
               "\nNumero Control = " + _NumeroControl +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _TipoDeTransaccion +
               "\nNumero Factura Afectada = " + _NumeroFacturaAfectada +
               "\nNumero Planilla Exportacion = " + _NumeroPlanillaExportacion +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _TipoDeVenta +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _UsaMaquinaFiscal +
               "\nCodigo Maquina Registradora = " + _CodigoMaquinaRegistradora +
               "\nNumero Desde = " + _NumeroDesde +
               "\nNumero Hasta = " + _NumeroHasta +
               "\nNumero Control Hasta = " + _NumeroControlHasta +
               "\nMonto Iva Retenido = " + _MontoIvaRetenido.ToString() +
               "\nFecha Aplicacion Ret IVA = " + _FechaAplicacionRetIVA.ToShortDateString() +
               "\nNumero Comprobante Ret IVA = " + _NumeroComprobanteRetIVA.ToString() +
               "\nFecha Comprobante Ret IVA = " + _FechaComprobanteRetIVA.ToShortDateString() +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _SeRetuvoIVA +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _FacturaConPreciosSinIva +
               "\nVuelto Del Cobro Directo = " + _VueltoDelCobroDirecto.ToString() +
               "\nConsecutivo Caja = " + _ConsecutivoCaja.ToString() +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _GeneraCobroDirecto +
               "\nFecha De Factura Afectada = " + _FechaDeFacturaAfectada.ToShortDateString() +
               "\nFecha De Entrega = " + _FechaDeEntrega.ToShortDateString() +
               "\nPorcentaje Descuento 1 = " + _PorcentajeDescuento1.ToString() +
               "\nPorcentaje Descuento 2 = " + _PorcentajeDescuento2.ToString() +
               "\nMonto Descuento 1 = " + _MontoDescuento1.ToString() +
               "\nMonto Descuento 2 = " + _MontoDescuento2.ToString() +
               "\nCodigo Lote = " + _CodigoLote +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _Devolucion +
               "\nPorcentaje Alicuota 1 = " + _PorcentajeAlicuota1.ToString() +
               "\nPorcentaje Alicuota 2 = " + _PorcentajeAlicuota2.ToString() +
               "\nPorcentaje Alicuota 3 = " + _PorcentajeAlicuota3.ToString() +
               "\nMonto IVAAlicuota 1 = " + _MontoIVAAlicuota1.ToString() +
               "\nMonto IVAAlicuota 2 = " + _MontoIVAAlicuota2.ToString() +
               "\nMonto IVAAlicuota 3 = " + _MontoIVAAlicuota3.ToString() +
               "\nMonto Gravable Alicuota 1 = " + _MontoGravableAlicuota1.ToString() +
               "\nMonto Gravable Alicuota 2 = " + _MontoGravableAlicuota2.ToString() +
               "\nMonto Gravable Alicuota 3 = " + _MontoGravableAlicuota3.ToString() +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _RealizoCierreZ +
               "\nNumero Comprobante Fiscal = " + _NumeroComprobanteFiscal +
               "\nSerial Maquina Fiscal = " + _SerialMaquinaFiscal +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _AplicarPromocion +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _RealizoCierreX +
               "\nHora Modificacion = " + _HoraModificacion +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _FormaDeCobro +
               "\nOtra Forma De Cobro = " + _OtraFormaDeCobro +
               "\nNo Cotizacion De Origen = " + _NoCotizacionDeOrigen +
               "\nNo Contrato = " + _NoContrato +
               "\nConsecutivo Vehiculo = " + _ConsecutivoVehiculo.ToString() +
               "\nConsecutivo Almacen = " + _ConsecutivoAlmacen.ToString() +
               "\nNumero Resumen Diario = " + _NumeroResumenDiario +
               "\nNo Control Despacho De Origen = " + _NoControlDespachoDeOrigen +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _ImprimeFiscal +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _EsDiferida +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _EsOriginalmenteDiferida +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _SeContabilizoIvaDiferido +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _AplicaDecretoIvaEspecial +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _EsGeneradaPorPuntoDeVenta +
               "\nCambio Moneda CXC = " + _CambioMonedaCXC.ToString() +
               "\nCambio Mostrar Total En Divisas = " + _CambioMostrarTotalEnDivisas.ToString() +
               "\nCodigo Moneda De Cobro = " + _CodigoMonedaDeCobro +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _GeneradaPorNotaEntrega +
               "\nEmitida En Factura Numero = " + _EmitidaEnFacturaNumero +
               "\nCodigo Moneda = " + _CodigoMoneda +
               "\nNumero Para Resumen = " + _NumeroParaResumen.ToString() +
               "\nNro Dias Mantener Cambio AMoneda Local = " + _NroDiasMantenerCambioAMonedaLocal.ToString() +
               "\nFecha Limite Cambio AMoneda Local = " + _FechaLimiteCambioAMonedaLocal.ToShortDateString() +
               "\n¿ENUMERATIVO O BOOLEAN? = " + _GeneradoPor +
			   "\nBase Imponible IGTF = " + _BaseImponibleIGTF.ToString() +
               "\nI GTFML = " + _IGTFML.ToString() +
               "\nI GTFME = " + _IGTFME.ToString() +
               "\nAlicuota IGTF = " + _AlicuotaIGTF.ToString() +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados

    } //End of class Factura

} //End of namespace Galac.Adm.Ccl.Venta

