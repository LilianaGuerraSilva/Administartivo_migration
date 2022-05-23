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

namespace Galac.Adm.Ccl.GestionCompras {
    [Serializable]
    public class Compra {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private string _Serie;
        private string _Numero;
        private DateTime _Fecha;
        private int _ConsecutivoProveedor;
        private string _CodigoProveedor;
        private string _NombreProveedor;
        private int _ConsecutivoAlmacen;
        private string _CodigoAlmacen;
        private string _NombreAlmacen;
        private string _Moneda;
        private string _CodigoMoneda;
        private decimal _CambioABolivares;
        private bool _GenerarCXP;
        private bool _UsaSeguro;
        private eTipoDeDistribucion _TipoDeDistribucion;
        private decimal _TasaAduanera;
        private decimal _TasaDolar;
        private decimal _ValorUT;
        private decimal _TotalRenglones;
        private decimal _TotalOtrosGastos;
        private decimal _TotalCompra;
        private string _Comentarios;
        private eStatusCompra _StatusCompra;
        private eTipoCompra _TipoDeCompra;
        private DateTime _FechaDeAnulacion;
        private int _ConsecutivoOrdenDeCompra;
        private string _NumeroDeOrdenDeCompra;
        private string _NoFacturaNotaEntrega;
        private eTipoOrdenDeCompra _TipoDeCompraParaCxP;
        private string _NombreOperador;
        private string _SimboloMoneda;
        private string _CodigoMonedaCostoUltimaCompra;		
        private string _MonedaCostoUltimaCompra;
		private decimal _CambioCostoUltimaCompra;
		private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        private ObservableCollection<CompraDetalleArticuloInventario> _DetailCompraDetalleArticuloInventario;
        private ObservableCollection<CompraDetalleGasto> _DetailCompraDetalleGasto;
        private ObservableCollection<CompraDetalleSerialRollo> _DetailCompraDetalleSerialRollo;
        XmlDocument _datos;


        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int Consecutivo {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }

        public string Serie {
            get { return _Serie; }
            set { _Serie = LibString.Mid(value,0,20); }
        }

        public string Numero {
            get { return _Numero; }
            set { _Numero = LibString.Mid(value,0,20); }
        }

        public DateTime Fecha {
            get { return _Fecha; }
            set { _Fecha = LibConvert.DateToDbValue(value); }
        }

        public int ConsecutivoProveedor {
            get { return _ConsecutivoProveedor; }
            set { _ConsecutivoProveedor = value; }
        }

        public string CodigoProveedor {
            get { return _CodigoProveedor; }
            set { _CodigoProveedor = LibString.Mid(value,0,10); }
        }

        public string NombreProveedor {
            get { return _NombreProveedor; }
            set { _NombreProveedor = LibString.Mid(value,0,60); }
        }

        public int ConsecutivoAlmacen {
            get { return _ConsecutivoAlmacen; }
            set { _ConsecutivoAlmacen = value; }
        }

        public string CodigoAlmacen {
            get { return _CodigoAlmacen; }
            set { _CodigoAlmacen = LibString.Mid(value,0,5); }
        }

        public string NombreAlmacen {
            get { return _NombreAlmacen; }
            set { _NombreAlmacen = LibString.Mid(value,0,40); }
        }

        public string Moneda {
            get { return _Moneda; }
            set { _Moneda = LibString.Mid(value,0,80); }
        }

        public string CodigoMoneda {
            get { return _CodigoMoneda; }
            set { _CodigoMoneda = LibString.Mid(value,0,4); }
        }

        public decimal CambioABolivares {
            get { return _CambioABolivares; }
            set { _CambioABolivares = value; }
        }


        public bool GenerarCXPAsBool {
            get { return _GenerarCXP; }
            set { _GenerarCXP = value; }
        }

        public string GenerarCXP {
            set { _GenerarCXP = LibConvert.SNToBool(value); }
        }


        public bool UsaSeguroAsBool {
            get { return _UsaSeguro; }
            set { _UsaSeguro = value; }
        }

        public string UsaSeguro {
            set { _UsaSeguro = LibConvert.SNToBool(value); }
        }


        public eTipoDeDistribucion TipoDeDistribucionAsEnum {
            get { return _TipoDeDistribucion; }
            set { _TipoDeDistribucion = value; }
        }

        public string TipoDeDistribucion {
            set { _TipoDeDistribucion = (eTipoDeDistribucion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeDistribucionAsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoDeDistribucion); }
        }

        public string TipoDeDistribucionAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeDistribucion); }
        }

        public decimal TasaAduanera {
            get { return _TasaAduanera; }
            set { _TasaAduanera = value; }
        }


        public decimal TasaDolar {
            get { return _TasaDolar; }
            set { _TasaDolar = value; }
        }

        public decimal ValorUT {
            get { return _ValorUT; }
            set { _ValorUT = value; }
        }


        public decimal TotalRenglones {
            get { return _TotalRenglones; }
            set { _TotalRenglones = value; }
        }

        public decimal TotalOtrosGastos {
            get { return _TotalOtrosGastos; }
            set { _TotalOtrosGastos = value; }
        }

        public decimal TotalCompra {
            get { return _TotalCompra; }
            set { _TotalCompra = value; }
        }

        public string Comentarios {
            get { return _Comentarios; }
            set { _Comentarios = LibString.Mid(value,0,255); }
        }

        public eStatusCompra StatusCompraAsEnum {
            get { return _StatusCompra; }
            set { _StatusCompra = value; }
        }

        public string StatusCompra {
            set { _StatusCompra = (eStatusCompra)LibConvert.DbValueToEnum(value); }
        }

        public string StatusCompraAsDB {
            get { return LibConvert.EnumToDbValue((int)_StatusCompra); }
        }

        public string StatusCompraAsString {
            get { return LibEnumHelper.GetDescription(_StatusCompra); }
        }

        public eTipoCompra TipoDeCompraAsEnum {
            get { return _TipoDeCompra; }
            set { _TipoDeCompra = value; }
        }

        public string TipoDeCompra {
            set { _TipoDeCompra = (eTipoCompra)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeCompraAsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoDeCompra); }
        }

        public string TipoDeCompraAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeCompra); }
        }

        public DateTime FechaDeAnulacion {
            get { return _FechaDeAnulacion; }
            set { _FechaDeAnulacion = LibConvert.DateToDbValue(value); }
        }

        public int ConsecutivoOrdenDeCompra {
            get { return _ConsecutivoOrdenDeCompra; }
            set { _ConsecutivoOrdenDeCompra = value; }
        }

        public string NumeroDeOrdenDeCompra {
            get { return _NumeroDeOrdenDeCompra; }
            set { _NumeroDeOrdenDeCompra = LibString.Mid(value,0,20); }
        }

        public string NoFacturaNotaEntrega {
            get { return _NoFacturaNotaEntrega; }
            set { _NoFacturaNotaEntrega = LibString.Mid(value,0,20); }
        }

        public eTipoOrdenDeCompra TipoDeCompraParaCxPAsEnum {
            get { return _TipoDeCompraParaCxP; }
            set { _TipoDeCompraParaCxP = value; }
        }

        public string TipoDeCompraParaCxP {
            set { _TipoDeCompraParaCxP = (eTipoOrdenDeCompra)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeCompraParaCxPAsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoDeCompraParaCxP); }
        }

        public string TipoDeCompraParaCxPAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeCompraParaCxP); }
        }

        public string SimboloMoneda {
            get { return _SimboloMoneda; }
            set { _SimboloMoneda = value; }
        }
        
        public string CodigoMonedaCostoUltimaCompra {
            get { return _CodigoMonedaCostoUltimaCompra; }
            set { _CodigoMonedaCostoUltimaCompra = LibString.Mid(value, 0, 4); }
        }
		
        public string MonedaCostoUltimaCompra {
            get { return _MonedaCostoUltimaCompra; }
            set { _MonedaCostoUltimaCompra = LibString.Mid(value, 0, 80); }
        }
		
		public decimal CambioCostoUltimaCompra {
            get { return _CambioCostoUltimaCompra; }
            set { _CambioCostoUltimaCompra = value; }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value,0,10); }
        }

        public DateTime FechaUltimaModificacion {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = LibConvert.DateToDbValue(value); }
        }

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public ObservableCollection<CompraDetalleArticuloInventario> DetailCompraDetalleArticuloInventario {
            get { return _DetailCompraDetalleArticuloInventario; }
            set { _DetailCompraDetalleArticuloInventario = value; }
        }

        public ObservableCollection<CompraDetalleGasto> DetailCompraDetalleGasto {
            get { return _DetailCompraDetalleGasto; }
            set { _DetailCompraDetalleGasto = value; }
        }

        public ObservableCollection<CompraDetalleSerialRollo> DetailCompraDetalleSerialRollo {
            get { return _DetailCompraDetalleSerialRollo; }
            set { _DetailCompraDetalleSerialRollo = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public Compra() {
            _DetailCompraDetalleArticuloInventario = new ObservableCollection<CompraDetalleArticuloInventario>();
            _DetailCompraDetalleGasto = new ObservableCollection<CompraDetalleGasto>();
            _DetailCompraDetalleSerialRollo = new ObservableCollection<GestionCompras.CompraDetalleSerialRollo>();
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            Consecutivo = 0;
            Serie = string.Empty;
            Numero = string.Empty;
            Fecha = LibDate.Today();
            ConsecutivoProveedor = 0;
            CodigoProveedor = string.Empty;
            NombreProveedor = string.Empty;
            ConsecutivoAlmacen = 0;
            CodigoAlmacen = string.Empty;
            NombreAlmacen = string.Empty;
            Moneda = string.Empty;
            CodigoMoneda = string.Empty;
            CambioABolivares = 1;
            GenerarCXPAsBool = false;
            UsaSeguroAsBool = false;
            TipoDeDistribucionAsEnum = eTipoDeDistribucion.ManualPorMonto;
            TasaAduanera = 0;
            TasaDolar = 0;
            ValorUT = 0;
            TotalRenglones = 0;
            TotalOtrosGastos = 0;
            TotalCompra = 0;
            Comentarios = string.Empty;
            StatusCompraAsEnum = eStatusCompra.Vigente;
            TipoDeCompraAsEnum = eTipoCompra.Nacional;
            FechaDeAnulacion = LibDate.Today();
            ConsecutivoOrdenDeCompra = 0;
            NumeroDeOrdenDeCompra = string.Empty;
            NoFacturaNotaEntrega = string.Empty;
            TipoDeCompraParaCxPAsEnum = eTipoOrdenDeCompra.NotadeEntrega;
			CodigoMonedaCostoUltimaCompra = string.Empty;
            MonedaCostoUltimaCompra = string.Empty;
            CambioCostoUltimaCompra = 1;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
            DetailCompraDetalleArticuloInventario = new ObservableCollection<CompraDetalleArticuloInventario>();
            DetailCompraDetalleGasto = new ObservableCollection<CompraDetalleGasto>();
            SimboloMoneda = string.Empty;
        }

        public Compra Clone() {
            Compra vResult = new Compra();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.Serie = _Serie;
            vResult.Numero = _Numero;
            vResult.Fecha = _Fecha;
            vResult.ConsecutivoProveedor = _ConsecutivoProveedor;
            vResult.CodigoProveedor = _CodigoProveedor;
            vResult.NombreProveedor = _NombreProveedor;
            vResult.ConsecutivoAlmacen = _ConsecutivoAlmacen;
            vResult.CodigoAlmacen = _CodigoAlmacen;
            vResult.NombreAlmacen = _NombreAlmacen;
            vResult.Moneda = _Moneda;
            vResult.CodigoMoneda = _CodigoMoneda;
            vResult.CambioABolivares = _CambioABolivares;
            vResult.GenerarCXPAsBool = _GenerarCXP;
            vResult.UsaSeguroAsBool = _UsaSeguro;
            vResult.TipoDeDistribucionAsEnum = _TipoDeDistribucion;
            vResult.TasaAduanera = _TasaAduanera;
            vResult.TasaDolar = _TasaDolar;
            vResult.ValorUT = _ValorUT;
            vResult.TotalRenglones = _TotalRenglones;
            vResult.TotalOtrosGastos = _TotalOtrosGastos;
            vResult.TotalCompra = _TotalCompra;
            vResult.Comentarios = _Comentarios;
            vResult.StatusCompraAsEnum = _StatusCompra;
            vResult.TipoDeCompraAsEnum = _TipoDeCompra;
            vResult.FechaDeAnulacion = _FechaDeAnulacion;
            vResult.ConsecutivoOrdenDeCompra = _ConsecutivoOrdenDeCompra;
            vResult.NumeroDeOrdenDeCompra = _NumeroDeOrdenDeCompra;
            vResult.NoFacturaNotaEntrega = _NoFacturaNotaEntrega;
			vResult.CodigoMonedaCostoUltimaCompra = _CodigoMonedaCostoUltimaCompra;
            vResult.MonedaCostoUltimaCompra = _MonedaCostoUltimaCompra;
			vResult.CambioCostoUltimaCompra = _CambioCostoUltimaCompra;
            vResult.TipoDeCompraParaCxPAsEnum = _TipoDeCompraParaCxP;
            vResult.NombreOperador = _NombreOperador;
            vResult.SimboloMoneda = _SimboloMoneda;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
            return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
                "\nConsecutivo = " + _Consecutivo.ToString() +
                "\nSerie = " + _Serie +
                "\nNumero = " + _Numero +
                "\nFecha = " + _Fecha.ToShortDateString() +
                "\nConsecutivo Proveedor = " + _ConsecutivoProveedor.ToString() +
                "\nConsecutivo Almacen = " + _ConsecutivoAlmacen.ToString() +
                "\nMoneda = " + _Moneda +
                "\nCódigo = " + _CodigoMoneda +
                "\nCambio a Bolívares = " + _CambioABolivares.ToString() +
                "\nGenerar Cuenta por Pagar = " + _GenerarCXP +
                "\nUsa Seguro = " + _UsaSeguro +
                "\nTipo De Distribucion = " + _TipoDeDistribucion.ToString() +
                "\nTasa Aduanera = " + _TasaAduanera.ToString() +
                "\nTasa Dolar = " + _TasaDolar.ToString() +
                "\nValor UT = " + _ValorUT.ToString() +
                "\nTotal Renglones = " + _TotalRenglones.ToString() +
                "\nOtros Gastos (Flete, etc.) = " + _TotalOtrosGastos.ToString() +
                "\nTotal Compra = " + _TotalCompra.ToString() +
                "\nComentarios = " + _Comentarios +
                "\nStatus Compra = " + _StatusCompra.ToString() +
                "\nTipo De Compra = " + _TipoDeCompra.ToString() +
                "\nFecha De Anulacion = " + _FechaDeAnulacion.ToShortDateString() +
                "\nConsecutivo Orden De Compra = " + _ConsecutivoOrdenDeCompra.ToString() +
                "\nNumero De Orden De Compra = " + _NumeroDeOrdenDeCompra +
                "\nNo Factura Nota Entrega = " + _NoFacturaNotaEntrega +
                "\nTipo De Compra Para CxP = " + _TipoDeCompraParaCxP.ToString() +
                "\nSímbolo de la Moneda = " + _SimboloMoneda +
                "\nCódigo Moneda = " + _CodigoMonedaCostoUltimaCompra +				
				"\nCambio Costo Última Compra = " + _CambioCostoUltimaCompra.ToString() +
                "\nNombre Operador = " + _NombreOperador +                
				"\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class Compra

} //End of namespace Galac.Adm.Ccl.GestionCompras

