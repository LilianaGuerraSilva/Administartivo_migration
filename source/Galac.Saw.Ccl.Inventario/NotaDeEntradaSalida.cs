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
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Ccl.Inventario {
    [Serializable]
    public class NotaDeEntradaSalida {
        #region Variables
        private int _ConsecutivoCompania;
        private string _NumeroDocumento;
        private eTipodeOperacion _TipodeOperacion;
        private string _CodigoCliente;
        private string _NombreCliente;
        private string _CodigoAlmacen;
        private string _NombreAlmacen;
        private DateTime _Fecha;
        private string _DescripcionArticulo;
        private string _Comentarios;
        private string _CodigoLote;
        private int _ConsecutivoCliente;
        private int _NumeroLote;
        private eStatusNotaEntradaSalida _StatusNotaEntradaSalida;
        private int _ConsecutivoAlmacen;
        private eTipoGeneradoPorNotaDeEntradaSalida _GeneradoPor;
        private int _ConsecutivoDocumentoOrigen;
        private eTipoNotaProduccion _TipoNotaProduccion;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
		private ObservableCollection<RenglonNotaES> _DetailRenglonNotaES;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string NumeroDocumento {
            get { return _NumeroDocumento; }
            set { _NumeroDocumento = LibString.Mid(value, 0, 11); }
        }

        public eTipodeOperacion TipodeOperacionAsEnum {
            get { return _TipodeOperacion; }
            set { _TipodeOperacion = value; }
        }

        public string TipodeOperacion {
            set { _TipodeOperacion = (eTipodeOperacion)LibConvert.DbValueToEnum(value); }
        }

        public string TipodeOperacionAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipodeOperacion); }
        }

        public string TipodeOperacionAsString {
            get { return LibEnumHelper.GetDescription(_TipodeOperacion); }
        }

        public string CodigoCliente {
            get { return _CodigoCliente; }
            set { _CodigoCliente = LibString.Mid(value, 0, 10); }
        }

        public string NombreCliente {
            get { return _NombreCliente; }
            set { _NombreCliente = LibString.Mid(value, 0, 80); }
        }

        public string CodigoAlmacen {
            get { return _CodigoAlmacen; }
            set { _CodigoAlmacen = LibString.Mid(value, 0, 5); }
        }

        public string NombreAlmacen {
            get { return _NombreAlmacen; }
            set { _NombreAlmacen = LibString.Mid(value, 0, 40); }
        }

        public DateTime Fecha {
            get { return _Fecha; }
            set { _Fecha = LibConvert.DateToDbValue(value); }
        }

        public string DescripcionArticulo {
            get { return _DescripcionArticulo; }
            set { _DescripcionArticulo = LibString.Mid(value, 0, 255); }
        }

        public string Comentarios {
            get { return _Comentarios; }
            set { _Comentarios = LibString.Mid(value, 0, 255); }
        }

        public string CodigoLote {
            get { return _CodigoLote; }
            set { _CodigoLote = LibString.Mid(value, 0, 10); }
        }

        public eStatusNotaEntradaSalida StatusNotaEntradaSalidaAsEnum {
            get { return _StatusNotaEntradaSalida; }
            set { _StatusNotaEntradaSalida = value; }
        }

        public string StatusNotaEntradaSalida {
            set { _StatusNotaEntradaSalida = (eStatusNotaEntradaSalida)LibConvert.DbValueToEnum(value); }
        }

        public string StatusNotaEntradaSalidaAsDB {
            get { return LibConvert.EnumToDbValue((int) _StatusNotaEntradaSalida); }
        }

        public string StatusNotaEntradaSalidaAsString {
            get { return LibEnumHelper.GetDescription(_StatusNotaEntradaSalida); }
        }

        public int ConsecutivoAlmacen {
            get { return _ConsecutivoAlmacen; }
            set { _ConsecutivoAlmacen = value; }
        }

        public int ConsecutivoCliente {
            get { return _ConsecutivoCliente; }
            set { _ConsecutivoCliente = value; }
        }

        public int NumeroLote {
            get { return _NumeroLote; }
            set { _NumeroLote = value; }
        }

        public eTipoGeneradoPorNotaDeEntradaSalida GeneradoPorAsEnum {
            get { return _GeneradoPor; }
            set { _GeneradoPor = value; }
        }

        public string GeneradoPor {
            set { _GeneradoPor = (eTipoGeneradoPorNotaDeEntradaSalida)LibConvert.DbValueToEnum(value); }
        }

        public string GeneradoPorAsDB {
            get { return LibConvert.EnumToDbValue((int) _GeneradoPor); }
        }

        public string GeneradoPorAsString {
            get { return LibEnumHelper.GetDescription(_GeneradoPor); }
        }

        public int ConsecutivoDocumentoOrigen {
            get { return _ConsecutivoDocumentoOrigen; }
            set { _ConsecutivoDocumentoOrigen = value; }
        }

        public eTipoNotaProduccion TipoNotaProduccionAsEnum {
            get { return _TipoNotaProduccion; }
            set { _TipoNotaProduccion = value; }
        }

        public string TipoNotaProduccion {
            set { _TipoNotaProduccion = (eTipoNotaProduccion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoNotaProduccionAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoNotaProduccion); }
        }

        public string TipoNotaProduccionAsString {
            get { return LibEnumHelper.GetDescription(_TipoNotaProduccion); }
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

        public ObservableCollection<RenglonNotaES> DetailRenglonNotaES {
            get { return _DetailRenglonNotaES; }
            set { _DetailRenglonNotaES = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public NotaDeEntradaSalida() {
            _DetailRenglonNotaES = new ObservableCollection<RenglonNotaES>();
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            NumeroDocumento = string.Empty;
            TipodeOperacionAsEnum = eTipodeOperacion.EntradadeInventario;
            CodigoCliente = string.Empty;
            NombreCliente = string.Empty;
            CodigoAlmacen = string.Empty;
            NombreAlmacen = string.Empty;
            Fecha = LibDate.Today();
            DescripcionArticulo = string.Empty;
            Comentarios = string.Empty;
            CodigoLote = string.Empty;
            StatusNotaEntradaSalidaAsEnum = eStatusNotaEntradaSalida.Vigente;
            ConsecutivoAlmacen = 0;
            GeneradoPorAsEnum = eTipoGeneradoPorNotaDeEntradaSalida.Usuario;
            ConsecutivoDocumentoOrigen = 0;
            TipoNotaProduccionAsEnum = eTipoNotaProduccion.NoAplica;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
            DetailRenglonNotaES = new ObservableCollection<RenglonNotaES>();
        }

        public NotaDeEntradaSalida Clone() {
            NotaDeEntradaSalida vResult = new NotaDeEntradaSalida();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.NumeroDocumento = _NumeroDocumento;
            vResult.TipodeOperacionAsEnum = _TipodeOperacion;
            vResult.CodigoCliente = _CodigoCliente;
            vResult.NombreCliente = _NombreCliente;
            vResult.CodigoAlmacen = _CodigoAlmacen;
            vResult.NombreAlmacen = _NombreAlmacen;
            vResult.Fecha = _Fecha;
            vResult.DescripcionArticulo = _DescripcionArticulo;
            vResult.Comentarios = _Comentarios;
            vResult.CodigoLote = _CodigoLote;
            vResult.StatusNotaEntradaSalidaAsEnum = _StatusNotaEntradaSalida;
            vResult.ConsecutivoAlmacen = _ConsecutivoAlmacen;
            vResult.GeneradoPorAsEnum = _GeneradoPor;
            vResult.ConsecutivoDocumentoOrigen = _ConsecutivoDocumentoOrigen;
            vResult.TipoNotaProduccionAsEnum = _TipoNotaProduccion;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nNumero Documento = " + _NumeroDocumento +
               "\nTipode Operacion = " + _TipodeOperacion.ToString() +
               "\nCódigo del Cliente = " + _CodigoCliente +
               "\nCodigo Almacen = " + _CodigoAlmacen +
               "\nFecha = " + _Fecha.ToShortDateString() +
               "\nComentarios = " + _Comentarios +
               "\nCodigo Lote = " + _CodigoLote +
               "\nStatus Nota Entrada Salida = " + _StatusNotaEntradaSalida.ToString() +
               "\nConsecutivo Almacen = " + _ConsecutivoAlmacen.ToString() +
               "\nGenerado Por = " + _GeneradoPor.ToString() +
               "\nConsecutivo Documento Origen = " + _ConsecutivoDocumentoOrigen.ToString() +
               "\nTipo Nota Produccion = " + _TipoNotaProduccion.ToString() +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class NotaDeEntradaSalida

} //End of namespace Galac.Saw.Ccl.Inventario

