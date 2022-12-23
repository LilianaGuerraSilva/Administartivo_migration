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

namespace Galac.Adm.Ccl.Vendedor {
    [Serializable]
    public class Vendedor {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private string _Codigo;
        private string _Nombre;
        private string _RIF;
        private eStatusVendedor _StatusVendedor;
        private string _Direccion;
        private string _Ciudad;
        private string _ZonaPostal;
        private string _Telefono;
        private string _Fax;
        private string _Email;
        private string _Notas;
        private decimal _ComisionPorVenta;
        private decimal _ComisionPorCobro;
        private decimal _TopeInicialVenta1;
        private decimal _TopeFinalVenta1;
        private decimal _PorcentajeVentas1;
        private decimal _TopeFinalVenta2;
        private decimal _PorcentajeVentas2;
        private decimal _TopeFinalVenta3;
        private decimal _PorcentajeVentas3;
        private decimal _TopeFinalVenta4;
        private decimal _PorcentajeVentas4;
        private decimal _TopeFinalVenta5;
        private decimal _PorcentajeVentas5;
        private decimal _TopeInicialCobranza1;
        private decimal _TopeFinalCobranza1;
        private decimal _PorcentajeCobranza1;
        private decimal _TopeFinalCobranza2;
        private decimal _PorcentajeCobranza2;
        private decimal _TopeFinalCobranza3;
        private decimal _PorcentajeCobranza3;
        private decimal _TopeFinalCobranza4;
        private decimal _PorcentajeCobranza4;
        private decimal _TopeFinalCobranza5;
        private decimal _PorcentajeCobranza5;
        private bool _UsaComisionPorVenta;
        private bool _UsaComisionPorCobranza;
        private string _CodigoLote;
        private eTipoDocumentoIdentificacion _TipoDocumentoIdentificacion;
        private eRutaDeComercializacion _RutaDeComercializacion;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
		private ObservableCollection<RenglonComisionesDeVendedor> _DetailRenglonComisionesDeVendedor;
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

        public string Codigo {
            get { return _Codigo; }
            set { _Codigo = LibString.Mid(value, 0, 5); }
        }

        public string Nombre {
            get { return _Nombre; }
            set { _Nombre = LibString.Mid(value, 0, 35); }
        }

        public string RIF {
            get { return _RIF; }
            set { _RIF = LibString.Mid(value, 0, 20); }
        }

        public eStatusVendedor StatusVendedorAsEnum {
            get { return _StatusVendedor; }
            set { _StatusVendedor = value; }
        }

        public string StatusVendedor {
            set { _StatusVendedor = (eStatusVendedor)LibConvert.DbValueToEnum(value); }
        }

        public string StatusVendedorAsDB {
            get { return LibConvert.EnumToDbValue((int) _StatusVendedor); }
        }

        public string StatusVendedorAsString {
            get { return LibEnumHelper.GetDescription(_StatusVendedor); }
        }

        public string Direccion {
            get { return _Direccion; }
            set { _Direccion = LibString.Mid(value, 0, 255); }
        }

        public string Ciudad {
            get { return _Ciudad; }
            set { _Ciudad = LibString.Mid(value, 0, 100); }
        }

        public string ZonaPostal {
            get { return _ZonaPostal; }
            set { _ZonaPostal = LibString.Mid(value, 0, 7); }
        }

        public string Telefono {
            get { return _Telefono; }
            set { _Telefono = LibString.Mid(value, 0, 40); }
        }

        public string Fax {
            get { return _Fax; }
            set { _Fax = LibString.Mid(value, 0, 20); }
        }

        public string Email {
            get { return _Email; }
            set { _Email = LibString.Mid(value, 0, 40); }
        }

        public string Notas {
            get { return _Notas; }
            set { _Notas = LibString.Mid(value, 0, 255); }
        }

        public decimal ComisionPorVenta {
            get { return _ComisionPorVenta; }
            set { _ComisionPorVenta = value; }
        }

        public decimal ComisionPorCobro {
            get { return _ComisionPorCobro; }
            set { _ComisionPorCobro = value; }
        }

        public decimal TopeInicialVenta1 {
            get { return _TopeInicialVenta1; }
            set { _TopeInicialVenta1 = value; }
        }

        public decimal TopeFinalVenta1 {
            get { return _TopeFinalVenta1; }
            set { _TopeFinalVenta1 = value; }
        }

        public decimal PorcentajeVentas1 {
            get { return _PorcentajeVentas1; }
            set { _PorcentajeVentas1 = value; }
        }

        public decimal TopeFinalVenta2 {
            get { return _TopeFinalVenta2; }
            set { _TopeFinalVenta2 = value; }
        }

        public decimal PorcentajeVentas2 {
            get { return _PorcentajeVentas2; }
            set { _PorcentajeVentas2 = value; }
        }

        public decimal TopeFinalVenta3 {
            get { return _TopeFinalVenta3; }
            set { _TopeFinalVenta3 = value; }
        }

        public decimal PorcentajeVentas3 {
            get { return _PorcentajeVentas3; }
            set { _PorcentajeVentas3 = value; }
        }

        public decimal TopeFinalVenta4 {
            get { return _TopeFinalVenta4; }
            set { _TopeFinalVenta4 = value; }
        }

        public decimal PorcentajeVentas4 {
            get { return _PorcentajeVentas4; }
            set { _PorcentajeVentas4 = value; }
        }

        public decimal TopeFinalVenta5 {
            get { return _TopeFinalVenta5; }
            set { _TopeFinalVenta5 = value; }
        }

        public decimal PorcentajeVentas5 {
            get { return _PorcentajeVentas5; }
            set { _PorcentajeVentas5 = value; }
        }

        public decimal TopeInicialCobranza1 {
            get { return _TopeInicialCobranza1; }
            set { _TopeInicialCobranza1 = value; }
        }

        public decimal TopeFinalCobranza1 {
            get { return _TopeFinalCobranza1; }
            set { _TopeFinalCobranza1 = value; }
        }

        public decimal PorcentajeCobranza1 {
            get { return _PorcentajeCobranza1; }
            set { _PorcentajeCobranza1 = value; }
        }

        public decimal TopeFinalCobranza2 {
            get { return _TopeFinalCobranza2; }
            set { _TopeFinalCobranza2 = value; }
        }

        public decimal PorcentajeCobranza2 {
            get { return _PorcentajeCobranza2; }
            set { _PorcentajeCobranza2 = value; }
        }

        public decimal TopeFinalCobranza3 {
            get { return _TopeFinalCobranza3; }
            set { _TopeFinalCobranza3 = value; }
        }

        public decimal PorcentajeCobranza3 {
            get { return _PorcentajeCobranza3; }
            set { _PorcentajeCobranza3 = value; }
        }

        public decimal TopeFinalCobranza4 {
            get { return _TopeFinalCobranza4; }
            set { _TopeFinalCobranza4 = value; }
        }

        public decimal PorcentajeCobranza4 {
            get { return _PorcentajeCobranza4; }
            set { _PorcentajeCobranza4 = value; }
        }

        public decimal TopeFinalCobranza5 {
            get { return _TopeFinalCobranza5; }
            set { _TopeFinalCobranza5 = value; }
        }

        public decimal PorcentajeCobranza5 {
            get { return _PorcentajeCobranza5; }
            set { _PorcentajeCobranza5 = value; }
        }

        public bool UsaComisionPorVentaAsBool {
            get { return _UsaComisionPorVenta; }
            set { _UsaComisionPorVenta = value; }
        }

        public string UsaComisionPorVenta {
            set { _UsaComisionPorVenta = LibConvert.SNToBool(value); }
        }


        public bool UsaComisionPorCobranzaAsBool {
            get { return _UsaComisionPorCobranza; }
            set { _UsaComisionPorCobranza = value; }
        }

        public string UsaComisionPorCobranza {
            set { _UsaComisionPorCobranza = LibConvert.SNToBool(value); }
        }


        public string CodigoLote {
            get { return _CodigoLote; }
            set { _CodigoLote = LibString.Mid(value, 0, 10); }
        }

        public eTipoDocumentoIdentificacion TipoDocumentoIdentificacionAsEnum {
            get { return _TipoDocumentoIdentificacion; }
            set { _TipoDocumentoIdentificacion = value; }
        }

        public string TipoDocumentoIdentificacion {
            set { _TipoDocumentoIdentificacion = (eTipoDocumentoIdentificacion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDocumentoIdentificacionAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDocumentoIdentificacion); }
        }

        public string TipoDocumentoIdentificacionAsString {
            get { return LibEnumHelper.GetDescription(_TipoDocumentoIdentificacion); }
        }

        public eRutaDeComercializacion RutaDeComercializacionAsEnum {
            get { return _RutaDeComercializacion; }
            set { _RutaDeComercializacion = value; }
        }

        public string RutaDeComercializacion {
            set { _RutaDeComercializacion = (eRutaDeComercializacion)LibConvert.DbValueToEnum(value); }
        }

        public string RutaDeComercializacionAsDB {
            get { return LibConvert.EnumToDbValue((int) _RutaDeComercializacion); }
        }

        public string RutaDeComercializacionAsString {
            get { return LibEnumHelper.GetDescription(_RutaDeComercializacion); }
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

        public ObservableCollection<RenglonComisionesDeVendedor> DetailRenglonComisionesDeVendedor {
            get { return _DetailRenglonComisionesDeVendedor; }
            set { _DetailRenglonComisionesDeVendedor = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public Vendedor() {
            _DetailRenglonComisionesDeVendedor = new ObservableCollection<RenglonComisionesDeVendedor>();
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            Consecutivo = 0;
            Codigo = "";
            Nombre = "";
            RIF = "";
            StatusVendedorAsEnum = eStatusVendedor.Activo;
            Direccion = "";
            Ciudad = "";
            ZonaPostal = "";
            Telefono = "";
            Fax = "";
            Email = "";
            Notas = "";
            ComisionPorVenta = 0;
            ComisionPorCobro = 0;
            TopeInicialVenta1 = 0;
            TopeFinalVenta1 = 0;
            PorcentajeVentas1 = 0;
            TopeFinalVenta2 = 0;
            PorcentajeVentas2 = 0;
            TopeFinalVenta3 = 0;
            PorcentajeVentas3 = 0;
            TopeFinalVenta4 = 0;
            PorcentajeVentas4 = 0;
            TopeFinalVenta5 = 0;
            PorcentajeVentas5 = 0;
            TopeInicialCobranza1 = 0;
            TopeFinalCobranza1 = 0;
            PorcentajeCobranza1 = 0;
            TopeFinalCobranza2 = 0;
            PorcentajeCobranza2 = 0;
            TopeFinalCobranza3 = 0;
            PorcentajeCobranza3 = 0;
            TopeFinalCobranza4 = 0;
            PorcentajeCobranza4 = 0;
            TopeFinalCobranza5 = 0;
            PorcentajeCobranza5 = 0;
            UsaComisionPorVentaAsBool = false;
            UsaComisionPorCobranzaAsBool = false;
            CodigoLote = string.Empty;
            TipoDocumentoIdentificacionAsEnum = eTipoDocumentoIdentificacion.RUC;
            RutaDeComercializacionAsEnum = eRutaDeComercializacion.Ninguna;
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
            DetailRenglonComisionesDeVendedor = new ObservableCollection<RenglonComisionesDeVendedor>();
        }

        public Vendedor Clone() {
            Vendedor vResult = new Vendedor();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.Codigo = _Codigo;
            vResult.Nombre = _Nombre;
            vResult.RIF = _RIF;
            vResult.StatusVendedorAsEnum = _StatusVendedor;
            vResult.Direccion = _Direccion;
            vResult.Ciudad = _Ciudad;
            vResult.ZonaPostal = _ZonaPostal;
            vResult.Telefono = _Telefono;
            vResult.Fax = _Fax;
            vResult.Email = _Email;
            vResult.Notas = _Notas;
            vResult.ComisionPorVenta = _ComisionPorVenta;
            vResult.ComisionPorCobro = _ComisionPorCobro;
            vResult.TopeInicialVenta1 = _TopeInicialVenta1;
            vResult.TopeFinalVenta1 = _TopeFinalVenta1;
            vResult.PorcentajeVentas1 = _PorcentajeVentas1;
            vResult.TopeFinalVenta2 = _TopeFinalVenta2;
            vResult.PorcentajeVentas2 = _PorcentajeVentas2;
            vResult.TopeFinalVenta3 = _TopeFinalVenta3;
            vResult.PorcentajeVentas3 = _PorcentajeVentas3;
            vResult.TopeFinalVenta4 = _TopeFinalVenta4;
            vResult.PorcentajeVentas4 = _PorcentajeVentas4;
            vResult.TopeFinalVenta5 = _TopeFinalVenta5;
            vResult.PorcentajeVentas5 = _PorcentajeVentas5;
            vResult.TopeInicialCobranza1 = _TopeInicialCobranza1;
            vResult.TopeFinalCobranza1 = _TopeFinalCobranza1;
            vResult.PorcentajeCobranza1 = _PorcentajeCobranza1;
            vResult.TopeFinalCobranza2 = _TopeFinalCobranza2;
            vResult.PorcentajeCobranza2 = _PorcentajeCobranza2;
            vResult.TopeFinalCobranza3 = _TopeFinalCobranza3;
            vResult.PorcentajeCobranza3 = _PorcentajeCobranza3;
            vResult.TopeFinalCobranza4 = _TopeFinalCobranza4;
            vResult.PorcentajeCobranza4 = _PorcentajeCobranza4;
            vResult.TopeFinalCobranza5 = _TopeFinalCobranza5;
            vResult.PorcentajeCobranza5 = _PorcentajeCobranza5;
            vResult.UsaComisionPorVentaAsBool = _UsaComisionPorVenta;
            vResult.UsaComisionPorCobranzaAsBool = _UsaComisionPorCobranza;
            vResult.CodigoLote = _CodigoLote;
            vResult.TipoDocumentoIdentificacionAsEnum = _TipoDocumentoIdentificacion;
            vResult.RutaDeComercializacionAsEnum = _RutaDeComercializacion;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nCódigo = " + _Codigo +
               "\nNombre = " + _Nombre +
               "\nN° R.I.F. = " + _RIF +
               "\nStatus = " + _StatusVendedor.ToString() +
               "\nDirección = " + _Direccion +
               "\nCiudad = " + _Ciudad +
               "\nZona Postal = " + _ZonaPostal +
               "\nTelefono = " + _Telefono +
               "\nFax = " + _Fax +
               "\nEmail = " + _Email +
               "\nNotas = " + _Notas +
               "\nComision Por Venta = " + _ComisionPorVenta.ToString() +
               "\nComision Por Cobro = " + _ComisionPorCobro.ToString() +
               "\nTope Inicial Venta 1 = " + _TopeInicialVenta1.ToString() +
               "\nTope Final Venta 1 = " + _TopeFinalVenta1.ToString() +
               "\nPorcentaje Ventas 1 = " + _PorcentajeVentas1.ToString() +
               "\nTope Final Venta 2 = " + _TopeFinalVenta2.ToString() +
               "\nPorcentaje Ventas 2 = " + _PorcentajeVentas2.ToString() +
               "\nTope Final Venta 3 = " + _TopeFinalVenta3.ToString() +
               "\nPorcentaje Ventas 3 = " + _PorcentajeVentas3.ToString() +
               "\nTope Final Venta 4 = " + _TopeFinalVenta4.ToString() +
               "\nPorcentaje Ventas 4 = " + _PorcentajeVentas4.ToString() +
               "\nTope Final Venta 5 = " + _TopeFinalVenta5.ToString() +
               "\nPorcentaje Ventas 5 = " + _PorcentajeVentas5.ToString() +
               "\nTope Inicial Cobranza 1 = " + _TopeInicialCobranza1.ToString() +
               "\nTope Final Cobranza 1 = " + _TopeFinalCobranza1.ToString() +
               "\nPorcentaje Cobranza 1 = " + _PorcentajeCobranza1.ToString() +
               "\nTope Final Cobranza 2 = " + _TopeFinalCobranza2.ToString() +
               "\nPorcentaje Cobranza 2 = " + _PorcentajeCobranza2.ToString() +
               "\nTope Final Cobranza 3 = " + _TopeFinalCobranza3.ToString() +
               "\nPorcentaje Cobranza 3 = " + _PorcentajeCobranza3.ToString() +
               "\nTope Final Cobranza 4 = " + _TopeFinalCobranza4.ToString() +
               "\nPorcentaje Cobranza 4 = " + _PorcentajeCobranza4.ToString() +
               "\nTope Final Cobranza 5 = " + _TopeFinalCobranza5.ToString() +
               "\nPorcentaje Cobranza 5 = " + _PorcentajeCobranza5.ToString() +
               "\nUsa Comision Por Venta = " + _UsaComisionPorVenta +
               "\nUsa Comision Por Cobranza = " + _UsaComisionPorCobranza +
               "\nCodigo Lote = " + _CodigoLote +
               "\nTipo Documento Identificacion = " + _TipoDocumentoIdentificacion.ToString() +
               "\nRuta De Comercializacion = " + _RutaDeComercializacion.ToString() +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class Vendedor

} //End of namespace Galac.Saw.Ccl.Vendedor

