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
    public class OrdenDeCompra {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private string _Serie;
        private string _Numero;
        private DateTime _Fecha;
        private int _ConsecutivoProveedor;
        private string _CodigoProveedor;
        private string _NombreProveedor;
        private string _Moneda;
        private string _CodigoMoneda;
        private decimal _CambioABolivares;
        private decimal _TotalRenglones;
        private decimal _TotalCompra;
        private eTipoCompra _TipoDeCompra;
        private string _Comentarios;
        private eStatusCompra _StatusOrdenDeCompra;
        private DateTime _FechaDeAnulacion;
        private string _CondicionesDeEntrega;
        private int _CondicionesDePago;
        private string _DescripcionCondicionesDePago;
        private eCondicionDeImportacion _CondicionesDeImportacion;
        private string _NumeroCotizacion;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
		private ObservableCollection<OrdenDeCompraDetalleArticuloInventario> _DetailOrdenDeCompraDetalleArticuloInventario;
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
            set { _Serie = LibString.Mid(value, 0, 20); }
        }

        public string Numero {
            get { return _Numero; }
            set { _Numero = LibString.Mid(value, 0, 20); }
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
            set { _CodigoProveedor = LibString.Mid(value, 0, 10); }
        }

        public string NombreProveedor {
            get { return _NombreProveedor; }
            set { _NombreProveedor = LibString.Mid(value, 0, 60); }
        }

        public string Moneda {
            get { return _Moneda; }
            set { _Moneda = LibString.Mid(value, 0, 80); }
        }

        public string CodigoMoneda {
            get { return _CodigoMoneda; }
            set { _CodigoMoneda = LibString.Mid(value, 0, 4); }
        }

        public decimal CambioABolivares {
            get { return _CambioABolivares; }
            set { _CambioABolivares = value; }
        }

        public decimal TotalRenglones {
            get { return _TotalRenglones; }
            set { _TotalRenglones = value; }
        }

        public decimal TotalCompra {
            get { return _TotalCompra; }
            set { _TotalCompra = value; }
        }

        public eTipoCompra TipoDeCompraAsEnum {
            get { return _TipoDeCompra; }
            set { _TipoDeCompra = value; }
        }

        public string TipoDeCompra {
            set { _TipoDeCompra = (eTipoCompra)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeCompraAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeCompra); }
        }

        public string TipoDeCompraAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeCompra); }
        }

        public string Comentarios {
            get { return _Comentarios; }
            set { _Comentarios = LibString.Mid(value, 0, 255); }
        }

        public eStatusCompra StatusOrdenDeCompraAsEnum {
            get { return _StatusOrdenDeCompra; }
            set { _StatusOrdenDeCompra = value; }
        }

        public string StatusOrdenDeCompra {
            set { _StatusOrdenDeCompra = (eStatusCompra)LibConvert.DbValueToEnum(value); }
        }

        public string StatusOrdenDeCompraAsDB {
            get { return LibConvert.EnumToDbValue((int) _StatusOrdenDeCompra); }
        }

        public string StatusOrdenDeCompraAsString {
            get { return LibEnumHelper.GetDescription(_StatusOrdenDeCompra); }
        }

        public DateTime FechaDeAnulacion {
            get { return _FechaDeAnulacion; }
            set { _FechaDeAnulacion = LibConvert.DateToDbValue(value); }
        }

        public string CondicionesDeEntrega {
            get { return _CondicionesDeEntrega; }
            set { _CondicionesDeEntrega = LibString.Mid(value, 0, 500); }
        }

        public int CondicionesDePago {
            get { return _CondicionesDePago; }
            set { _CondicionesDePago = value; }
        }

        public string DescripcionCondicionesDePago {
            get { return _DescripcionCondicionesDePago; }
            set { _DescripcionCondicionesDePago = LibString.Mid(value, 0, 80); }
        }

        public eCondicionDeImportacion CondicionesDeImportacionAsEnum {
            get { return _CondicionesDeImportacion; }
            set { _CondicionesDeImportacion = value; }
        }

        public string CondicionesDeImportacion {
            set { _CondicionesDeImportacion = (eCondicionDeImportacion)LibConvert.DbValueToEnum(value); }
        }

        public string CondicionesDeImportacionAsDB {
            get { return LibConvert.EnumToDbValue((int) _CondicionesDeImportacion); }
        }

        public string CondicionesDeImportacionAsString {
            get { return LibEnumHelper.GetDescription(_CondicionesDeImportacion); }
        }

        public string NumeroCotizacion {
            get { return _NumeroCotizacion; }
            set { _NumeroCotizacion = LibString.Mid(value, 0, 60); }
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

        public ObservableCollection<OrdenDeCompraDetalleArticuloInventario> DetailOrdenDeCompraDetalleArticuloInventario {
            get { return _DetailOrdenDeCompraDetalleArticuloInventario; }
            set { _DetailOrdenDeCompraDetalleArticuloInventario = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public OrdenDeCompra() {
            _DetailOrdenDeCompraDetalleArticuloInventario = new ObservableCollection<OrdenDeCompraDetalleArticuloInventario>();
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
            Moneda = string.Empty;
            CodigoMoneda = string.Empty;
            CambioABolivares = 0;
            TotalRenglones = 0;
            TotalCompra = (0);
            TipoDeCompraAsEnum = eTipoCompra.Nacional;
            Comentarios = string.Empty;
            StatusOrdenDeCompraAsEnum = eStatusCompra.Vigente;
            FechaDeAnulacion = LibDate.Today();
            CondicionesDeEntrega = string.Empty;
            CondicionesDePago = 0;
            DescripcionCondicionesDePago = string.Empty;
            CondicionesDeImportacionAsEnum = eCondicionDeImportacion.AConvenir;
            NumeroCotizacion = string.Empty;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
            DetailOrdenDeCompraDetalleArticuloInventario = new ObservableCollection<OrdenDeCompraDetalleArticuloInventario>();
        }

        public OrdenDeCompra Clone() {
            OrdenDeCompra vResult = new OrdenDeCompra();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.Serie = _Serie;
            vResult.Numero = _Numero;
            vResult.Fecha = _Fecha;
            vResult.ConsecutivoProveedor = _ConsecutivoProveedor;
            vResult.CodigoProveedor = _CodigoProveedor;
            vResult.NombreProveedor = _NombreProveedor;
            vResult.Moneda = _Moneda;
            vResult.CodigoMoneda = _CodigoMoneda;
            vResult.CambioABolivares = _CambioABolivares;
            vResult.TotalRenglones = _TotalRenglones;
            vResult.TotalCompra = _TotalCompra;
            vResult.TipoDeCompraAsEnum = _TipoDeCompra;
            vResult.Comentarios = _Comentarios;
            vResult.StatusOrdenDeCompraAsEnum = _StatusOrdenDeCompra;
            vResult.FechaDeAnulacion = _FechaDeAnulacion;
            vResult.CondicionesDeEntrega = _CondicionesDeEntrega;
            vResult.CondicionesDePago = _CondicionesDePago;
            vResult.DescripcionCondicionesDePago = _DescripcionCondicionesDePago;
            vResult.CondicionesDeImportacionAsEnum = _CondicionesDeImportacion;
            vResult.NumeroCotizacion = _NumeroCotizacion;
            vResult.NombreOperador = _NombreOperador;
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
               "\nNumero Cotizacion = " + _NumeroCotizacion +
               "\nNombre de la Moneda = " + _Moneda +
               "\nCodigo Moneda = " + _CodigoMoneda +
               "\nCambio a Bolívares = " + _CambioABolivares.ToString() +
               "\nTotal Renglones = " + _TotalRenglones.ToString() +
               "\nTotal Compra = " + _TotalCompra.ToString() +
               "\nTipo De Compra = " + _TipoDeCompra.ToString() +
               "\nComentarios = " + _Comentarios +
               "\nStatus Orden De Compra = " + _StatusOrdenDeCompra.ToString() +
               "\nFecha De Anulacion = " + _FechaDeAnulacion.ToShortDateString() +
               "\nCondiciones de Entrega = " + _CondicionesDeEntrega +
               "\nCondiciones de Pago = " + _CondicionesDePago.ToString() +
               "\nCondiciones de Importación = " + _CondicionesDeImportacion.ToString() +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class OrdenDeCompra

} //End of namespace Galac.Adm.Ccl.GestionCompras

