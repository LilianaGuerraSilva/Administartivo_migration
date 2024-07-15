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
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Ccl.CajaChica { 
    [Serializable]
    public class CxP {
        #region Variables
        private int _ConsecutivoCompania;
        private string _Numero;
        private int _ConsecutivoCxP;
        private eTipoDeCxC _TipoDeCxP;
        private eStatusDocumento _Status;
        private string _CodigoProveedor;
        private string _NombreProveedor;
        private DateTime _Fecha;
        private DateTime _FechaCancelacion;
        private DateTime _FechaVencimiento;
        private DateTime _FechaAnulacion;
        private string _CodigoMoneda;
        private string _Moneda;
        private decimal _CambioABolivares;
        private bool _AplicaParaLibrodeCompras;
        private decimal _MontoExento;
        private decimal _MontoGravado;
        private decimal _MontoIva;
        private decimal _MontoAbonado;
        private int _MesDeAplicacion;
        private int _AnoDeAplicacion;
        private string _Observaciones;
        private eCreditoFiscal _CreditoFiscal;
        private eTipoDeCompra _TipoDeCompra;
        private bool _SeHizoLaRetencion;
        private decimal _MontoGravableAlicuotaGeneral;
        private decimal _MontoGravableAlicuota2;
        private decimal _MontoGravableAlicuota3;
        private decimal _MontoIVAAlicuotaGeneral;
        private decimal _MontoIVAAlicuota2;
        private decimal _MontoIVAAlicuota3;
        private string _NumeroPlanillaDeImportacion;
        private string _NumeroExpedienteDeImportacion;
        private eTipoDeTransaccionDeLibrosFiscales _TipoDeTransaccion;
        private string _NumeroDeFacturaAfectada;
        private string _NumeroControl;
        private bool _SeHizoLaRetencionIVA;
        private string _NumeroComprobanteRetencion;
        private DateTime _FechaAplicacionRetIVA;
        private decimal _PorcentajeRetencionAplicado;
        private decimal _MontoRetenido;
        private eDondeSeEfectuaLaRetencionIVA _OrigenDeLaRetencion;
        private bool _RetencionAplicadaEnPago;
        private eTipoDeContribuyenteDelIva _OrigenInformacionRetencion;
        private eGeneradoPor _CxPgeneradaPor;
        private bool _EsCxPhistorica;
        private int _NumDiasDeVencimiento;
        private string _NumeroDocOrigen;
        private string _CodigoLote;
        private bool _GenerarAsientoDeRetiroEnCuenta;
        private decimal _TotalOtrosImpuestos;
        private bool _SeContabilRetIva;
        private string _DondeContabilRetIva;
        private bool _OrigenDeLaRetencionISLR;
        private bool _DondeContabilISLR;
        private bool _ISLRAplicadaEnPago;
        private decimal _MontoRetenidoISLR;
        private bool _SeContabilISLR;
        private DateTime _FechaAplicacionImpuestoMunicipal;
        private string _NumeroComprobanteImpuestoMunicipal;
        private decimal _MontoRetenidoImpuestoMunicipal;
        private bool _ImpuestoMunicipalRetenido;
        private string _NumeroControlDeFacturaAfectada;
        private int _ConsecutivoRendicion;
        private bool _EstaAsociadoARendicion;
		private string _NumeroDeclaracionAduana;
        private DateTime _FechaDeclaracionAduana;
        private bool _UsaPrefijoSerie;
        private string _CodigoProveedorOriginalServicio;
        private bool _EsUnaCuentaATerceros;
        private bool _SeHizoLaDetraccion;
        private bool _AplicaIvaAlicuotaEspecial;
    	private decimal _MontoGravableAlicuotaEspecial1;
        private decimal _MontoIVAAlicuotaEspecial1;
        private decimal _PorcentajeIvaAlicuotaEspecial1;
        private decimal _MontoGravableAlicuotaEspecial2;
        private decimal _MontoIVAAlicuotaEspecial2;
        private decimal _PorcentajeIvaAlicuotaEspecial2;
        private int _DiaDeAplicacion;
		private decimal _BaseImponibleIGTFML;
        private decimal _AlicuotaIGTFML;
        private decimal _MontoIGTFML;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
		private ObservableCollection<RenglonImpuestoMunicipalRet> _DetailRenglonImpuestoMunicipalRet;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string Numero {
            get { return _Numero; }
            set { _Numero = LibString.Mid(value, 0, 25); }
        }
        public int ConsecutivoCxP {
            get { return _ConsecutivoCxP; }
            set { _ConsecutivoCxP = value; }
        }

        public eTipoDeCxC TipoDeCxPAsEnum {
            get { return _TipoDeCxP; }
            set { _TipoDeCxP = value; }
        }

        public string TipoDeCxP {
            set { _TipoDeCxP = (eTipoDeCxC)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeCxPAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeCxP); }
        }

        public string TipoDeCxPAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeCxP); }
        }

        public eStatusDocumento StatusAsEnum {
            get { return _Status; }
            set { _Status = value; }
        }

        public string Status {
            set { _Status = (eStatusDocumento)LibConvert.DbValueToEnum(value); }
        }

        public string StatusAsDB {
            get { return LibConvert.EnumToDbValue((int) _Status); }
        }

        public string StatusAsString {
            get { return LibEnumHelper.GetDescription(_Status); }
        }

        public string CodigoProveedor {
            get { return _CodigoProveedor; }
            set { _CodigoProveedor = LibString.Mid(value, 0, 10); }
        }

        public string NombreProveedor {
            get { return _NombreProveedor; }
            set { _NombreProveedor = LibString.Mid(value, 0, 60); }
        }

        public DateTime Fecha {
            get { return _Fecha; }
            set { _Fecha = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaCancelacion {
            get { return _FechaCancelacion; }
            set { _FechaCancelacion = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaVencimiento {
            get { return _FechaVencimiento; }
            set { _FechaVencimiento = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaAnulacion {
            get { return _FechaAnulacion; }
            set { _FechaAnulacion = LibConvert.DateToDbValue(value); }
        }
        public string CodigoMoneda {
            get { return _CodigoMoneda; }
            set { _CodigoMoneda = LibString.Mid(value, 0, 4); }
        }

        public string Moneda {
            get { return _Moneda; }
            set { _Moneda = LibString.Mid(value, 0, 10); }
        }

        public decimal CambioABolivares {
            get { return _CambioABolivares; }
            set { _CambioABolivares = value; }
        }

        public bool AplicaParaLibrodeComprasAsBool {
            get { return _AplicaParaLibrodeCompras; }
            set { _AplicaParaLibrodeCompras = value; }
        }

        public string AplicaParaLibrodeCompras {
            set { _AplicaParaLibrodeCompras = LibConvert.SNToBool(value); }
        }

        public decimal MontoExento {
            get { return _MontoExento; }
            set { _MontoExento = value; }
        }

        public decimal MontoGravado {
            get { return _MontoGravado; }
            set { _MontoGravado = value; }
        }

        public decimal MontoIva {
            get { return _MontoIva; }
            set { _MontoIva = value; }
        }

        public decimal MontoAbonado {
            get { return _MontoAbonado; }
            set { _MontoAbonado = value; }
        }

        public int MesDeAplicacion {
            get { return _MesDeAplicacion; }
            set { _MesDeAplicacion = value; }
        }

        public int AnoDeAplicacion {
            get { return _AnoDeAplicacion; }
            set { _AnoDeAplicacion = value; }
        }

        public string Observaciones {
            get { return _Observaciones; }
            set { _Observaciones = LibString.Mid(value, 0, 255); }
        }

        public eCreditoFiscal CreditoFiscalAsEnum {
            get { return _CreditoFiscal; }
            set { _CreditoFiscal = value; }
        }

        public string CreditoFiscal {
            set { _CreditoFiscal = (eCreditoFiscal)LibConvert.DbValueToEnum(value); }
        }

        public string CreditoFiscalAsDB {
            get { return LibConvert.EnumToDbValue((int) _CreditoFiscal); }
        }

        public string CreditoFiscalAsString {
            get { return LibEnumHelper.GetDescription(_CreditoFiscal); }
        }

        public eTipoDeCompra TipoDeCompraAsEnum {
            get { return _TipoDeCompra; }
            set { _TipoDeCompra = value; }
        }

        public string TipoDeCompra {
            set { _TipoDeCompra = (eTipoDeCompra)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeCompraAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeCompra); }
        }

        public string TipoDeCompraAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeCompra); }
        }

        public bool SeHizoLaRetencionAsBool {
            get { return _SeHizoLaRetencion; }
            set { _SeHizoLaRetencion = value; }
        }

        public string SeHizoLaRetencion {
            set { _SeHizoLaRetencion = LibConvert.SNToBool(value); }
        }

        public decimal MontoGravableAlicuotaGeneral {
            get { return _MontoGravableAlicuotaGeneral; }
            set { _MontoGravableAlicuotaGeneral = value; }
        }

        public decimal MontoGravableAlicuota2 {
            get { return _MontoGravableAlicuota2; }
            set { _MontoGravableAlicuota2 = value; }
        }

        public decimal MontoGravableAlicuota3 {
            get { return _MontoGravableAlicuota3; }
            set { _MontoGravableAlicuota3 = value; }
        }

        public decimal MontoIVAAlicuotaGeneral {
            get { return _MontoIVAAlicuotaGeneral; }
            set { _MontoIVAAlicuotaGeneral = value; }
        }

        public decimal MontoIVAAlicuota2 {
            get { return _MontoIVAAlicuota2; }
            set { _MontoIVAAlicuota2 = value; }
        }

        public decimal MontoIVAAlicuota3 {
            get { return _MontoIVAAlicuota3; }
            set { _MontoIVAAlicuota3 = value; }
        }

        public string NumeroPlanillaDeImportacion {
            get { return _NumeroPlanillaDeImportacion; }
            set { _NumeroPlanillaDeImportacion = LibString.Mid(value, 0, 20); }
        }

        public string NumeroExpedienteDeImportacion {
            get { return _NumeroExpedienteDeImportacion; }
            set { _NumeroExpedienteDeImportacion = LibString.Mid(value, 0, 20); }
        }

        public eTipoDeTransaccionDeLibrosFiscales TipoDeTransaccionAsEnum {
            get { return _TipoDeTransaccion; }
            set { _TipoDeTransaccion = value; }
        }

        public string TipoDeTransaccion {
            set { _TipoDeTransaccion = (eTipoDeTransaccionDeLibrosFiscales)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeTransaccionAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeTransaccion); }
        }

        public string TipoDeTransaccionAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeTransaccion); }
        }

        public string NumeroDeFacturaAfectada {
            get { return _NumeroDeFacturaAfectada; }
            set { _NumeroDeFacturaAfectada = LibString.Mid(value, 0, 11); }
        }

        public string NumeroControl {
            get { return _NumeroControl; }
            set { _NumeroControl = LibString.Mid(value, 0, 20); }
        }

        public bool SeHizoLaRetencionIVAAsBool {
            get { return _SeHizoLaRetencionIVA; }
            set { _SeHizoLaRetencionIVA = value; }
        }

        public string SeHizoLaRetencionIVA {
            set { _SeHizoLaRetencionIVA = LibConvert.SNToBool(value); }
        }

        public string NumeroComprobanteRetencion {
            get { return _NumeroComprobanteRetencion; }
            set { _NumeroComprobanteRetencion = LibString.Mid(value, 0, 8); }
        }

        public DateTime FechaAplicacionRetIVA {
            get { return _FechaAplicacionRetIVA; }
            set { _FechaAplicacionRetIVA = LibConvert.DateToDbValue(value); }
        }

        public decimal PorcentajeRetencionAplicado {
            get { return _PorcentajeRetencionAplicado; }
            set { _PorcentajeRetencionAplicado = value; }
        }

        public decimal MontoRetenido {
            get { return _MontoRetenido; }
            set { _MontoRetenido = value; }
        }

        public eDondeSeEfectuaLaRetencionIVA OrigenDeLaRetencionAsEnum {
            get { return _OrigenDeLaRetencion; }
            set { _OrigenDeLaRetencion = value; }
        }

        public string OrigenDeLaRetencion {
            set { _OrigenDeLaRetencion = (eDondeSeEfectuaLaRetencionIVA)LibConvert.DbValueToEnum(value); }
        }

        public string OrigenDeLaRetencionAsDB {
            get { return LibConvert.EnumToDbValue((int) _OrigenDeLaRetencion); }
        }

        public string OrigenDeLaRetencionAsString {
            get { return LibEnumHelper.GetDescription(_OrigenDeLaRetencion); }
        }

        public bool RetencionAplicadaEnPagoAsBool {
            get { return _RetencionAplicadaEnPago; }
            set { _RetencionAplicadaEnPago = value; }
        }

        public string RetencionAplicadaEnPago {
            set { _RetencionAplicadaEnPago = LibConvert.SNToBool(value); }
        }


        public eTipoDeContribuyenteDelIva OrigenInformacionRetencionAsEnum {
            get { return _OrigenInformacionRetencion; }
            set { _OrigenInformacionRetencion = value; }
        }

        public string OrigenInformacionRetencion {
            set { _OrigenInformacionRetencion = (eTipoDeContribuyenteDelIva)LibConvert.DbValueToEnum(value); }
        }

        public string OrigenInformacionRetencionAsDB {
            get { return LibConvert.EnumToDbValue((int) _OrigenInformacionRetencion); }
        }

        public string OrigenInformacionRetencionAsString {
            get { return LibEnumHelper.GetDescription(_OrigenInformacionRetencion); }
        }

        public eGeneradoPor CxPgeneradaPorAsEnum {
            get { return _CxPgeneradaPor; }
            set { _CxPgeneradaPor = value; }
        }

        public string CxPgeneradaPor {
            set { _CxPgeneradaPor = (eGeneradoPor)LibConvert.DbValueToEnum(value); }
        }

        public string CxPgeneradaPorAsDB {
            get { return LibConvert.EnumToDbValue((int) _CxPgeneradaPor); }
        }

        public string CxPgeneradaPorAsString {
            get { return LibEnumHelper.GetDescription(_CxPgeneradaPor); }
        }

        public bool EsCxPhistoricaAsBool {
            get { return _EsCxPhistorica; }
            set { _EsCxPhistorica = value; }
        }

        public string EsCxPhistorica {
            set { _EsCxPhistorica = LibConvert.SNToBool(value); }
        }

        public int NumDiasDeVencimiento {
            get { return _NumDiasDeVencimiento; }
            set { _NumDiasDeVencimiento = value; }
        }

        public string NumeroDocOrigen {
            get { return _NumeroDocOrigen; }
            set { _NumeroDocOrigen = LibString.Mid(value, 0, 15); }
        }

        public string CodigoLote {
            get { return _CodigoLote; }
            set { _CodigoLote = LibString.Mid(value, 0, 10); }
        }

        public bool GenerarAsientoDeRetiroEnCuentaAsBool {
            get { return _GenerarAsientoDeRetiroEnCuenta; }
            set { _GenerarAsientoDeRetiroEnCuenta = value; }
        }

        public string GenerarAsientoDeRetiroEnCuenta {
            set { _GenerarAsientoDeRetiroEnCuenta = LibConvert.SNToBool(value); }
        }

        public decimal TotalOtrosImpuestos {
            get { return _TotalOtrosImpuestos; }
            set { _TotalOtrosImpuestos = value; }
        }

        public bool SeContabilRetIvaAsBool {
            get { return _SeContabilRetIva; }
            set { _SeContabilRetIva = value; }
        }

        public string SeContabilRetIva {
            set { _SeContabilRetIva = LibConvert.SNToBool(value); }
        }        

        public string DondeContabilRetIva {
            get { return _DondeContabilRetIva; }
            set { _DondeContabilRetIva = value; }

        }

        public bool OrigenDeLaRetencionISLRAsBool {
            get { return _OrigenDeLaRetencionISLR; }
            set { _OrigenDeLaRetencionISLR = value; }
        }

        public string OrigenDeLaRetencionISLR {
            set { _OrigenDeLaRetencionISLR = LibConvert.SNToBool(value); }
        }

        public bool DondeContabilISLRAsBool {
            get { return _DondeContabilISLR; }
            set { _DondeContabilISLR = value; }
        }

        public string DondeContabilISLR {
            set { _DondeContabilISLR = LibConvert.SNToBool(value); }
        }

        public bool ISLRAplicadaEnPagoAsBool {
            get { return _ISLRAplicadaEnPago; }
            set { _ISLRAplicadaEnPago = value; }
        }

        public string ISLRAplicadaEnPago {
            set { _ISLRAplicadaEnPago = LibConvert.SNToBool(value); }
        }

        public decimal MontoRetenidoISLR {
            get { return _MontoRetenidoISLR; }
            set { _MontoRetenidoISLR = value; }
        }

        public bool SeContabilISLRAsBool {
            get { return _SeContabilISLR; }
            set { _SeContabilISLR = value; }
        }

        public string SeContabilISLR {
            set { _SeContabilISLR = LibConvert.SNToBool(value); }
        }

        public DateTime FechaAplicacionImpuestoMunicipal {
            get { return _FechaAplicacionImpuestoMunicipal; }
            set { _FechaAplicacionImpuestoMunicipal = LibConvert.DateToDbValue(value); }
        }

        public string NumeroComprobanteImpuestoMunicipal {
            get { return _NumeroComprobanteImpuestoMunicipal; }
            set { _NumeroComprobanteImpuestoMunicipal = LibString.Mid(value, 0, 50); }
        }

        public decimal MontoRetenidoImpuestoMunicipal {
            get { return _MontoRetenidoImpuestoMunicipal; }
            set { _MontoRetenidoImpuestoMunicipal = value; }
        }

        public bool ImpuestoMunicipalRetenidoAsBool {
            get { return _ImpuestoMunicipalRetenido; }
            set { _ImpuestoMunicipalRetenido = value; }
        }

        public string ImpuestoMunicipalRetenido {
            set { _ImpuestoMunicipalRetenido = LibConvert.SNToBool(value); }
        }

        public string NumeroControlDeFacturaAfectada {
            get { return _NumeroControlDeFacturaAfectada; }
            set { _NumeroControlDeFacturaAfectada = LibString.Mid(value, 0, 11); }
        }

        public int ConsecutivoRendicion {
            get { return _ConsecutivoRendicion; }
            set { _ConsecutivoRendicion = value; }
        }
        public bool EstaAsociadoARendicionAsBool {
            get { return _EstaAsociadoARendicion; }
            set { _EstaAsociadoARendicion = value; }
        }
        public string EstaAsociadoARendicion {
            set { _EstaAsociadoARendicion = LibConvert.SNToBool(value); }
        }
		 public string NumeroDeclaracionAduana {
            get { return _NumeroDeclaracionAduana; }
            set { _NumeroDeclaracionAduana = LibString.Mid(value, 0, 20); }
        }
        public DateTime FechaDeclaracionAduana {
            get { return _FechaDeclaracionAduana; }
            set { _FechaDeclaracionAduana = LibConvert.DateToDbValue(value); }
        }
        public bool UsaPrefijoSerieAsBool {
            get { return _UsaPrefijoSerie; }
            set { _UsaPrefijoSerie = value; }
        }
        public string UsaPrefijoSerie {
            set { _UsaPrefijoSerie = LibConvert.SNToBool(value); }
        }

        public string CodigoProveedorOriginalServicio {
            get { return _CodigoProveedorOriginalServicio; }
            set { _CodigoProveedorOriginalServicio = LibString.Mid(value, 0, 10); }
        }

        public bool EsUnaCuentaATercerosAsBool {
            get { return _EsUnaCuentaATerceros; }
            set { _EsUnaCuentaATerceros = value; }
        }

        public string EsUnaCuentaATerceros {
            set { _EsUnaCuentaATerceros = LibConvert.SNToBool(value); }
        }

        public bool SeHizoLaDetraccionAsBool {
            get { return _SeHizoLaDetraccion; }
            set { _SeHizoLaDetraccion = value; }
        }

        public string SeHizoLaDetraccion {
            set { _SeHizoLaDetraccion = LibConvert.SNToBool(value); }
        }
		
		public bool AplicaIvaAlicuotaEspecialAsBool {
            get { return _AplicaIvaAlicuotaEspecial; }
            set { _AplicaIvaAlicuotaEspecial = value; }
        }

        public string AplicaIvaAlicuotaEspecial {
            set { _AplicaIvaAlicuotaEspecial = LibConvert.SNToBool(value); }
        }

        public decimal MontoGravableAlicuotaEspecial1 {
            get { return _MontoGravableAlicuotaEspecial1; }
            set { _MontoGravableAlicuotaEspecial1 = value; }
        }

        public decimal MontoIVAAlicuotaEspecial1 {
            get { return _MontoIVAAlicuotaEspecial1; }
            set { _MontoIVAAlicuotaEspecial1 = value; }
        }

        public decimal PorcentajeIvaAlicuotaEspecial1 {
            get { return _PorcentajeIvaAlicuotaEspecial1; }
            set { _PorcentajeIvaAlicuotaEspecial1 = value; }
        }

        public decimal MontoGravableAlicuotaEspecial2 {
            get { return _MontoGravableAlicuotaEspecial2; }
            set { _MontoGravableAlicuotaEspecial2 = value; }
        }

        public decimal MontoIVAAlicuotaEspecial2 {
            get { return _MontoIVAAlicuotaEspecial2; }
            set { _MontoIVAAlicuotaEspecial2 = value; }
        }

        public decimal PorcentajeIvaAlicuotaEspecial2 {
            get { return _PorcentajeIvaAlicuotaEspecial2; }
            set { _PorcentajeIvaAlicuotaEspecial2 = value; }
        }

        public int  DiaDeAplicacion {
            get { return _DiaDeAplicacion; }
            set { _DiaDeAplicacion = value; }
        }
		
		public decimal BaseImponibleIGTFML {
            get { return _BaseImponibleIGTFML; }
            set { _BaseImponibleIGTFML = value; }
        }

        public decimal AlicuotaIGTFML {
            get { return _AlicuotaIGTFML; }
            set { _AlicuotaIGTFML = value; }
        }

        public decimal MontoIGTFML {
            get { return _MontoIGTFML; }
            set { _MontoIGTFML = value; }
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

        public ObservableCollection<RenglonImpuestoMunicipalRet> DetailRenglonImpuestoMunicipalRet {
            get { return _DetailRenglonImpuestoMunicipalRet; }
            set { _DetailRenglonImpuestoMunicipalRet = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public CxP() {
            _DetailRenglonImpuestoMunicipalRet = new ObservableCollection<RenglonImpuestoMunicipalRet>();
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados
        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            Numero = "";
            ConsecutivoCxP = 0;
            TipoDeCxPAsEnum = eTipoDeCxC.Factura;
            StatusAsEnum = eStatusDocumento.PorCancelar;
            CodigoProveedor = "";
            NombreProveedor = "";
            Fecha = LibDate.Today();
            FechaCancelacion = LibDate.Today();
            FechaVencimiento = LibDate.Today();
            FechaAnulacion = LibDate.Today();
            CodigoMoneda = "";
            Moneda = "";
            CambioABolivares = 0;
            AplicaParaLibrodeComprasAsBool = false;
            MontoExento = 0;
            MontoGravado = 0;
            MontoIva = 0;
            MontoAbonado = 0;
            MesDeAplicacion = 0;
            AnoDeAplicacion = 0;
            Observaciones = "";
            CreditoFiscalAsEnum = eCreditoFiscal.Deducible;
            TipoDeCompraAsEnum = eTipoDeCompra.ComprasExentas;
            SeHizoLaRetencionAsBool = false;
            MontoGravableAlicuotaGeneral = 0;
            MontoGravableAlicuota2 = 0;
            MontoGravableAlicuota3 = 0;
            MontoIVAAlicuotaGeneral = 0;
            MontoIVAAlicuota2 = 0;
            MontoIVAAlicuota3 = 0;
            NumeroPlanillaDeImportacion = "";
            NumeroExpedienteDeImportacion = "";
            TipoDeTransaccionAsEnum = eTipoDeTransaccionDeLibrosFiscales.Registro;
            NumeroDeFacturaAfectada = "";
            NumeroControl = "";
            SeHizoLaRetencionIVAAsBool = false;
            NumeroComprobanteRetencion = "";
            FechaAplicacionRetIVA = LibDate.Today();
            PorcentajeRetencionAplicado = 0;
            MontoRetenido = 0;
            OrigenDeLaRetencionAsEnum = eDondeSeEfectuaLaRetencionIVA.NoRetenida;
            RetencionAplicadaEnPagoAsBool = false;
            OrigenInformacionRetencionAsEnum = eTipoDeContribuyenteDelIva.ContribuyenteFormal;
            CxPgeneradaPorAsEnum = eGeneradoPor.Usuario;
            EsCxPhistoricaAsBool = false;
            NumDiasDeVencimiento = 0;
            NumeroDocOrigen = "";
            CodigoLote = "";
            GenerarAsientoDeRetiroEnCuentaAsBool = false;
            TotalOtrosImpuestos = 0;
            SeContabilRetIvaAsBool = false;
            OrigenDeLaRetencionISLRAsBool = false;
            DondeContabilISLRAsBool = false;
            ISLRAplicadaEnPagoAsBool = false;
            MontoRetenidoISLR = 0;
            SeContabilISLRAsBool = false;
            FechaAplicacionImpuestoMunicipal = LibDate.Today();
            NumeroComprobanteImpuestoMunicipal = "";
            MontoRetenidoImpuestoMunicipal = 0;
            ImpuestoMunicipalRetenidoAsBool = false;
            NumeroControlDeFacturaAfectada = "";
            ConsecutivoRendicion = 0;
            EstaAsociadoARendicionAsBool = false;
            NumeroDeclaracionAduana = "";
            FechaDeclaracionAduana = LibDate.Today();
            UsaPrefijoSerieAsBool = false;
            CodigoProveedorOriginalServicio = "";
            EsUnaCuentaATercerosAsBool = false;
            SeHizoLaDetraccionAsBool = false;
            AplicaIvaAlicuotaEspecialAsBool = false;
            MontoGravableAlicuotaEspecial1 = 0;
            MontoIVAAlicuotaEspecial1 = 0;
            PorcentajeIvaAlicuotaEspecial1 = 0;
            MontoGravableAlicuotaEspecial2 = 0;
            MontoIVAAlicuotaEspecial2 = 0;
            PorcentajeIvaAlicuotaEspecial2 = 0;
            DiaDeAplicacion = 0;
		    BaseImponibleIGTFML = 0;
            AlicuotaIGTFML = 0;
            MontoIGTFML = 0;
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
            DetailRenglonImpuestoMunicipalRet.Clear();
        }

        public CxP Clone() {
            CxP vResult = new CxP();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Numero = _Numero;
            vResult.ConsecutivoCxP = _ConsecutivoCxP;
            vResult.TipoDeCxPAsEnum = _TipoDeCxP;
            vResult.StatusAsEnum = _Status;
            vResult.CodigoProveedor = _CodigoProveedor;
            vResult.NombreProveedor = _NombreProveedor;
            vResult.Fecha = _Fecha;
            vResult.FechaCancelacion = _FechaCancelacion;
            vResult.FechaVencimiento = _FechaVencimiento;
            vResult.FechaAnulacion = _FechaAnulacion;
            vResult.CodigoMoneda = _CodigoMoneda;
            vResult.Moneda = _Moneda;
            vResult.CambioABolivares = _CambioABolivares;
            vResult.AplicaParaLibrodeComprasAsBool = _AplicaParaLibrodeCompras;
            vResult.MontoExento = _MontoExento;
            vResult.MontoGravado = _MontoGravado;
            vResult.MontoIva = _MontoIva;
            vResult.MontoAbonado = _MontoAbonado;
            vResult.MesDeAplicacion = _MesDeAplicacion;
            vResult.AnoDeAplicacion = _AnoDeAplicacion;
            vResult.Observaciones = _Observaciones;
            vResult.CreditoFiscalAsEnum = _CreditoFiscal;
            vResult.TipoDeCompraAsEnum = _TipoDeCompra;
            vResult.SeHizoLaRetencionAsBool = _SeHizoLaRetencion;
            vResult.MontoGravableAlicuotaGeneral = _MontoGravableAlicuotaGeneral;
            vResult.MontoGravableAlicuota2 = _MontoGravableAlicuota2;
            vResult.MontoGravableAlicuota3 = _MontoGravableAlicuota3;
            vResult.MontoIVAAlicuotaGeneral = _MontoIVAAlicuotaGeneral;
            vResult.MontoIVAAlicuota2 = _MontoIVAAlicuota2;
            vResult.MontoIVAAlicuota3 = _MontoIVAAlicuota3;
            vResult.NumeroPlanillaDeImportacion = _NumeroPlanillaDeImportacion;
            vResult.NumeroExpedienteDeImportacion = _NumeroExpedienteDeImportacion;
            vResult.TipoDeTransaccionAsEnum = _TipoDeTransaccion;
            vResult.NumeroDeFacturaAfectada = _NumeroDeFacturaAfectada;
            vResult.NumeroControl = _NumeroControl;
            vResult.SeHizoLaRetencionIVAAsBool = _SeHizoLaRetencionIVA;
            vResult.NumeroComprobanteRetencion = _NumeroComprobanteRetencion;
            vResult.FechaAplicacionRetIVA = _FechaAplicacionRetIVA;
            vResult.PorcentajeRetencionAplicado = _PorcentajeRetencionAplicado;
            vResult.MontoRetenido = _MontoRetenido;
            vResult.OrigenDeLaRetencionAsEnum = _OrigenDeLaRetencion;
            vResult.RetencionAplicadaEnPagoAsBool = _RetencionAplicadaEnPago;
            vResult.OrigenInformacionRetencionAsEnum = _OrigenInformacionRetencion;
            vResult.CxPgeneradaPorAsEnum = _CxPgeneradaPor;
            vResult.EsCxPhistoricaAsBool = _EsCxPhistorica;
            vResult.NumDiasDeVencimiento = _NumDiasDeVencimiento;
            vResult.NumeroDocOrigen = _NumeroDocOrigen;
            vResult.CodigoLote = _CodigoLote;
            vResult.GenerarAsientoDeRetiroEnCuentaAsBool = _GenerarAsientoDeRetiroEnCuenta;
            vResult.TotalOtrosImpuestos = _TotalOtrosImpuestos;
            vResult.SeContabilRetIvaAsBool = _SeContabilRetIva;
            vResult.OrigenDeLaRetencionISLRAsBool = _OrigenDeLaRetencionISLR;
            vResult.DondeContabilISLRAsBool = _DondeContabilISLR;
            vResult.ISLRAplicadaEnPagoAsBool = _ISLRAplicadaEnPago;
            vResult.MontoRetenidoISLR = _MontoRetenidoISLR;
            vResult.SeContabilISLRAsBool = _SeContabilISLR;
            vResult.FechaAplicacionImpuestoMunicipal = _FechaAplicacionImpuestoMunicipal;
            vResult.NumeroComprobanteImpuestoMunicipal = _NumeroComprobanteImpuestoMunicipal;
            vResult.MontoRetenidoImpuestoMunicipal = _MontoRetenidoImpuestoMunicipal;
            vResult.ImpuestoMunicipalRetenidoAsBool = _ImpuestoMunicipalRetenido;
            vResult.NumeroControlDeFacturaAfectada = _NumeroControlDeFacturaAfectada;
            vResult.ConsecutivoRendicion = _ConsecutivoRendicion;
            vResult.EstaAsociadoARendicionAsBool = _EstaAsociadoARendicion;
            vResult.NumeroDeclaracionAduana = _NumeroDeclaracionAduana;
            vResult.FechaDeclaracionAduana = _FechaDeclaracionAduana;
            vResult.UsaPrefijoSerieAsBool = _UsaPrefijoSerie;
            vResult.CodigoProveedorOriginalServicio = _CodigoProveedorOriginalServicio;
            vResult.EsUnaCuentaATercerosAsBool = _EsUnaCuentaATerceros;
            vResult.SeHizoLaDetraccionAsBool = _SeHizoLaDetraccion;
            vResult.AplicaIvaAlicuotaEspecialAsBool = _AplicaIvaAlicuotaEspecial;
            vResult.AplicaIvaAlicuotaEspecialAsBool = _AplicaIvaAlicuotaEspecial;
            vResult.MontoGravableAlicuotaEspecial1 = _MontoGravableAlicuotaEspecial1;
            vResult.MontoIVAAlicuotaEspecial1 = _MontoIVAAlicuotaEspecial1;
            vResult.PorcentajeIvaAlicuotaEspecial1 = _PorcentajeIvaAlicuotaEspecial1;
            vResult.MontoGravableAlicuotaEspecial2 = _MontoGravableAlicuotaEspecial2;
            vResult.MontoIVAAlicuotaEspecial2 = _MontoIVAAlicuotaEspecial2;
            vResult.PorcentajeIvaAlicuotaEspecial2 = _PorcentajeIvaAlicuotaEspecial2;
            vResult.DiaDeAplicacion = _DiaDeAplicacion;
			vResult.BaseImponibleIGTFML = _BaseImponibleIGTFML;
            vResult.AlicuotaIGTFML = _AlicuotaIGTFML;
            vResult.MontoIGTFML = _MontoIGTFML;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nNúmero = " + _Numero +
               "\nConsecutivo Cx P = " + _ConsecutivoCxP.ToString() +
               "\nTipo De Cx P = " + _TipoDeCxP.ToString() +
               "\nStatus = " + _Status.ToString() +
               "\nCódigo del Proveedor = " + _CodigoProveedor +
               "\nFecha = " + _Fecha.ToShortDateString() +
               "\nFecha de Cancelación = " + _FechaCancelacion.ToShortDateString() +
               "\nFecha de Vencimiento = " + _FechaVencimiento.ToShortDateString() +
               "\nFecha de Anulación = " + _FechaAnulacion.ToShortDateString() +
               "\nCódigo = " + _CodigoMoneda +
               "\nNombre de la Moneda = " + _Moneda +
               "\nCambio ABolivares = " + _CambioABolivares.ToString() +
               "\nAplica Para Librode Compras = " + _AplicaParaLibrodeCompras +
               "\nMonto Exento = " + _MontoExento.ToString() +
               "\nMonto Gravado = " + _MontoGravado.ToString() +
               "\nMonto Iva = " + _MontoIva.ToString() +
               "\nMonto Abonado = " + _MontoAbonado.ToString() +
               "\nMes De Aplicacion = " + _MesDeAplicacion.ToString() +
               "\nAno De Aplicacion = " + _AnoDeAplicacion.ToString() +
               "\nObservaciones = " + _Observaciones +
               "\nCredito Fiscal = " + _CreditoFiscal.ToString() +
               "\nTipo De Compra = " + _TipoDeCompra.ToString() +
               "\nSe Hizo La Retencion = " + _SeHizoLaRetencion +
               "\nMonto Gravable Alicuota General = " + _MontoGravableAlicuotaGeneral.ToString() +
               "\nMonto Gravable Alicuota 2 = " + _MontoGravableAlicuota2.ToString() +
               "\nMonto Gravable Alicuota 3 = " + _MontoGravableAlicuota3.ToString() +
               "\nMonto IVAAlicuota General = " + _MontoIVAAlicuotaGeneral.ToString() +
               "\nMonto IVAAlicuota 2 = " + _MontoIVAAlicuota2.ToString() +
               "\nMonto IVAAlicuota 3 = " + _MontoIVAAlicuota3.ToString() +
               "\nNumero Planilla De Importacion = " + _NumeroPlanillaDeImportacion +
               "\nNumero Expediente De Importacion = " + _NumeroExpedienteDeImportacion +
               "\nTipo De Transaccion = " + _TipoDeTransaccion.ToString() +
               "\nNumero De Factura Afectada = " + _NumeroDeFacturaAfectada +
               "\nNumero Control = " + _NumeroControl +
               "\nSe Hizo La Retencion IVA = " + _SeHizoLaRetencionIVA +
               "\nNumero Comprobante Retencion = " + _NumeroComprobanteRetencion +
               "\nFecha Aplicacion Ret IVA = " + _FechaAplicacionRetIVA.ToShortDateString() +
               "\nPorcentaje Retencion Aplicado = " + _PorcentajeRetencionAplicado.ToString() +
               "\nMonto Retenido = " + _MontoRetenido.ToString() +
               "\nOrigen De La Retencion = " + _OrigenDeLaRetencion.ToString() +
               "\nRetencion Aplicada En Pago = " + _RetencionAplicadaEnPago +
               "\nOrigen Informacion Retencion = " + _OrigenInformacionRetencion.ToString() +
               "\nCx Pgenerada Por = " + _CxPgeneradaPor.ToString() +
               "\nEs Cx Phistorica = " + _EsCxPhistorica +
               "\nNum Dias De Vencimiento = " + _NumDiasDeVencimiento.ToString() +
               "\nNumero Doc Origen = " + _NumeroDocOrigen +
               "\nCodigo Lote = " + _CodigoLote +
               "\nGenerar Asiento De Retiro En Cuenta = " + _GenerarAsientoDeRetiroEnCuenta +
               "\nTotal Otros Impuestos = " + _TotalOtrosImpuestos.ToString() +
               "\nSe Contabil Ret Iva = " + _SeContabilRetIva +
               "\nDonde Contabil Ret Iva = " + _DondeContabilRetIva +
               "\nOrigen De La Retencion ISLR = " + _OrigenDeLaRetencionISLR +
               "\nDonde Contabil ISLR = " + _DondeContabilISLR +
               "\nI SLRAplicada En Pago = " + _ISLRAplicadaEnPago +
               "\nMonto Retenido ISLR = " + _MontoRetenidoISLR.ToString() +
               "\nSe Contabil ISLR = " + _SeContabilISLR +
               "\nFecha Aplicacion Impuesto Municipal = " + _FechaAplicacionImpuestoMunicipal.ToShortDateString() +
               "\nNumero Comprobante Impuesto Municipal = " + _NumeroComprobanteImpuestoMunicipal +
               "\nMonto Retenido Impuesto Municipal = " + _MontoRetenidoImpuestoMunicipal.ToString() +
               "\nImpuesto Municipal Retenido = " + _ImpuestoMunicipalRetenido +
               "\nNumero Control De Factura Afectada = " + _NumeroControlDeFacturaAfectada +
               "\nRendicion Asociada = " + _ConsecutivoRendicion.ToString() +
               "\nEsta Asociado ARendicion = " + _EstaAsociadoARendicion +
               "\nNúmero de Declaración Aduanal = " + _NumeroDeclaracionAduana +
               "\nFecha de declaración Aduanal = " + _FechaDeclaracionAduana.ToShortDateString() +
               "\nDía de Aplicación = " + _DiaDeAplicacion.ToString() +
               "\nUsar Prefijo Serie = " + _UsaPrefijoSerie +
               "\nCodigo Proveedor Original Servicio = " + _CodigoProveedorOriginalServicio +
               "\nEs Una Cuenta A Terceros = " + _EsUnaCuentaATerceros +
			   "\nBase Imponible IGTFML = " + _BaseImponibleIGTFML.ToString() +
               "\nAlicuota IGTFML = " + _AlicuotaIGTFML.ToString() +
               "\nMonto IGTFML = " + _MontoIGTFML.ToString() +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados
    } //End of class CxP
} //End of namespace Galac.Dbo.Ccl.CajaChica

