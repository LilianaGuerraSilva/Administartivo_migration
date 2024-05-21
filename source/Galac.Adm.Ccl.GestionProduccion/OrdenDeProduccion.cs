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
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Ccl.GestionProduccion {
    [Serializable]
    public class OrdenDeProduccion {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private string _Codigo;
        private string _Descripcion;
        private eTipoStatusOrdenProduccion _StatusOp;
        private int _ConsecutivoAlmacenProductoTerminado;
        private string _CodigoAlmacenProductoTerminado;
        private string _NombreAlmacenProductoTerminado;
        private int _ConsecutivoAlmacenMateriales;
        private string _CodigoAlmacenMateriales;
        private string _NombreAlmacenMateriales;
        private DateTime _FechaCreacion;
        private DateTime _FechaInicio;
        private DateTime _FechaFinalizacion;
        private DateTime _FechaAnulacion;
        private DateTime _FechaAjuste;
        private bool _AjustadaPostCierre;
        private string _Observacion;
        private string _MotivoDeAnulacion;
        private int _NumeroDecimales;
        private eFormaDeCalcularCostoTerminado _CostoTerminadoCalculadoAPartirDe;
        private string _CodigoMonedaCostoProduccion;
        private string _Moneda;
        private decimal _CambioCostoProduccion;
        private int _ConsecutivoListaDeMateriales;
        private string _CodigoListaDeMateriales;
        private string _NombreListaDeMateriales;
        private decimal _CantidadAProducir;
        private decimal _CantidadProducida;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
		private ObservableCollection<OrdenDeProduccionDetalleArticulo> _DetailOrdenDeProduccionDetalleArticulo;
		private ObservableCollection<OrdenDeProduccionDetalleMateriales> _DetailOrdenDeProduccionDetalleMateriales;
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
            set { _Codigo = LibString.Mid(value, 0, 15); }
        }

        public string Descripcion {
            get { return _Descripcion; }
            set { _Descripcion = LibString.Mid(value, 0, 250); }
        }

        public eTipoStatusOrdenProduccion StatusOpAsEnum {
            get { return _StatusOp; }
            set { _StatusOp = value; }
        }

        public string StatusOp {
            set { _StatusOp = (eTipoStatusOrdenProduccion)LibConvert.DbValueToEnum(value); }
        }

        public string StatusOpAsDB {
            get { return LibConvert.EnumToDbValue((int) _StatusOp); }
        }

        public string StatusOpAsString {
            get { return LibEnumHelper.GetDescription(_StatusOp); }
        }

        public int ConsecutivoAlmacenProductoTerminado {
            get { return _ConsecutivoAlmacenProductoTerminado; }
            set { _ConsecutivoAlmacenProductoTerminado = value; }
        }

        public string CodigoAlmacenProductoTerminado {
            get { return _CodigoAlmacenProductoTerminado; }
            set { _CodigoAlmacenProductoTerminado = LibString.Mid(value, 0, 5); }
        }

        public string NombreAlmacenProductoTerminado {
            get { return _NombreAlmacenProductoTerminado; }
            set { _NombreAlmacenProductoTerminado = LibString.Mid(value, 0, 40); }
        }

        public int ConsecutivoAlmacenMateriales {
            get { return _ConsecutivoAlmacenMateriales; }
            set { _ConsecutivoAlmacenMateriales = value; }
        }

        public string CodigoAlmacenMateriales {
            get { return _CodigoAlmacenMateriales; }
            set { _CodigoAlmacenMateriales = LibString.Mid(value, 0, 5); }
        }

        public string NombreAlmacenMateriales {
            get { return _NombreAlmacenMateriales; }
            set { _NombreAlmacenMateriales = LibString.Mid(value, 0, 40); }
        }

        public DateTime FechaCreacion {
            get { return _FechaCreacion; }
            set { _FechaCreacion = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaInicio {
            get { return _FechaInicio; }
            set { _FechaInicio = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaFinalizacion {
            get { return _FechaFinalizacion; }
            set { _FechaFinalizacion = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaAnulacion {
            get { return _FechaAnulacion; }
            set { _FechaAnulacion = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaAjuste {
            get { return _FechaAjuste; }
            set { _FechaAjuste = LibConvert.DateToDbValue(value); }
        }

        public bool AjustadaPostCierreAsBool {
            get { return _AjustadaPostCierre; }
            set { _AjustadaPostCierre = value; }
        }

        public string AjustadaPostCierre {
            set { _AjustadaPostCierre = LibConvert.SNToBool(value); }
        }

        public string Observacion {
            get { return _Observacion; }
            set { _Observacion = LibString.Mid(value, 0, 600); }
        }

        public string MotivoDeAnulacion {
            get { return _MotivoDeAnulacion; }
            set { _MotivoDeAnulacion = LibString.Mid(value, 0, 600); }
        }

        public int NumeroDecimales {
            get { return _NumeroDecimales; }
            set { _NumeroDecimales = value; }
        }

        public eFormaDeCalcularCostoTerminado CostoTerminadoCalculadoAPartirDeAsEnum {
            get { return _CostoTerminadoCalculadoAPartirDe; }
            set { _CostoTerminadoCalculadoAPartirDe = value; }
        }

        public string CostoTerminadoCalculadoAPartirDe {
            set { _CostoTerminadoCalculadoAPartirDe = (eFormaDeCalcularCostoTerminado)LibConvert.DbValueToEnum(value); }
        }

        public string CostoTerminadoCalculadoAPartirDeAsDB {
            get { return LibConvert.EnumToDbValue((int) _CostoTerminadoCalculadoAPartirDe); }
        }

        public string CostoTerminadoCalculadoAPartirDeAsString {
            get { return LibEnumHelper.GetDescription(_CostoTerminadoCalculadoAPartirDe); }
        }

        public string CodigoMonedaCostoProduccion {
            get { return _CodigoMonedaCostoProduccion; }
            set { _CodigoMonedaCostoProduccion = LibString.Mid(value, 0, 4); }
        }

        public string Moneda {
            get { return _Moneda; }
            set { _Moneda = LibString.Mid(value, 0, 80); }
        }

        public decimal CambioCostoProduccion {
            get { return _CambioCostoProduccion; }
            set { _CambioCostoProduccion = value; }
        }

        public int ConsecutivoListaDeMateriales {
            get { return _ConsecutivoListaDeMateriales; }
            set { _ConsecutivoListaDeMateriales = value; }
        }

        public string CodigoListaDeMateriales {
            get { return _CodigoListaDeMateriales; }
            set { _CodigoListaDeMateriales = LibString.Mid(value, 0, 30); }
        }

        public string NombreListaDeMateriales {
            get { return _NombreListaDeMateriales; }
            set { _NombreListaDeMateriales = LibString.Mid(value, 0, 255); }
        }

        public decimal CantidadAProducir {
            get { return _CantidadAProducir; }
            set { _CantidadAProducir = value; }
        }

        public decimal CantidadProducida {
            get { return _CantidadProducida; }
            set { _CantidadProducida = value; }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 20); }
        }

        public DateTime FechaUltimaModificacion {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = LibConvert.DateToDbValue(value); }
        }

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public ObservableCollection<OrdenDeProduccionDetalleArticulo> DetailOrdenDeProduccionDetalleArticulo {
            get { return _DetailOrdenDeProduccionDetalleArticulo; }
            set { _DetailOrdenDeProduccionDetalleArticulo = value; }
        }

        public ObservableCollection<OrdenDeProduccionDetalleMateriales> DetailOrdenDeProduccionDetalleMateriales {
            get { return _DetailOrdenDeProduccionDetalleMateriales; }
            set { _DetailOrdenDeProduccionDetalleMateriales = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public OrdenDeProduccion() {
            _DetailOrdenDeProduccionDetalleArticulo = new ObservableCollection<OrdenDeProduccionDetalleArticulo>();
            _DetailOrdenDeProduccionDetalleMateriales = new ObservableCollection<OrdenDeProduccionDetalleMateriales>();
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
            Codigo = string.Empty;
            Descripcion = string.Empty;
            StatusOpAsEnum = eTipoStatusOrdenProduccion.Ingresada;
            ConsecutivoAlmacenProductoTerminado = 0;
            CodigoAlmacenProductoTerminado = string.Empty;
            NombreAlmacenProductoTerminado = string.Empty;
            ConsecutivoAlmacenMateriales = 0;
            CodigoAlmacenMateriales = string.Empty;
            NombreAlmacenMateriales = string.Empty;
            FechaCreacion = LibDate.Today();
            FechaInicio = LibDate.Today();
            FechaFinalizacion = LibDate.Today();
            FechaAnulacion = LibDate.Today();
            FechaAjuste = LibDate.Today();
            AjustadaPostCierreAsBool = false;
            Observacion = string.Empty;
            MotivoDeAnulacion = string.Empty;
            NumeroDecimales = 2;
			CostoTerminadoCalculadoAPartirDeAsEnum = eFormaDeCalcularCostoTerminado.APartirDeCostoEnMonedaLocal;
            CodigoMonedaCostoProduccion = "VED";
            Moneda = string.Empty;
            CambioCostoProduccion = 1;
            ConsecutivoListaDeMateriales = 0;
            CodigoListaDeMateriales = string.Empty;
            NombreListaDeMateriales = string.Empty;
            CantidadAProducir = 0;
            CantidadProducida = 0;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
            DetailOrdenDeProduccionDetalleArticulo = new ObservableCollection<OrdenDeProduccionDetalleArticulo>();
            DetailOrdenDeProduccionDetalleMateriales = new ObservableCollection<OrdenDeProduccionDetalleMateriales>();
        }

        public OrdenDeProduccion Clone() {
            OrdenDeProduccion vResult = new OrdenDeProduccion();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.Codigo = _Codigo;
            vResult.Descripcion = _Descripcion;
            vResult.StatusOpAsEnum = _StatusOp;
            vResult.ConsecutivoAlmacenProductoTerminado = _ConsecutivoAlmacenProductoTerminado;
            vResult.CodigoAlmacenProductoTerminado = _CodigoAlmacenProductoTerminado;
            vResult.NombreAlmacenProductoTerminado = _NombreAlmacenProductoTerminado;
            vResult.ConsecutivoAlmacenMateriales = _ConsecutivoAlmacenMateriales;
            vResult.CodigoAlmacenMateriales = _CodigoAlmacenMateriales;
            vResult.NombreAlmacenMateriales = _NombreAlmacenMateriales;
            vResult.FechaCreacion = _FechaCreacion;
            vResult.FechaInicio = _FechaInicio;
            vResult.FechaFinalizacion = _FechaFinalizacion;
            vResult.FechaAnulacion = _FechaAnulacion;
            vResult.FechaAjuste = _FechaAjuste;
            vResult.AjustadaPostCierreAsBool = _AjustadaPostCierre;
            vResult.Observacion = _Observacion;
            vResult.MotivoDeAnulacion = _MotivoDeAnulacion;
            vResult.NumeroDecimales = _NumeroDecimales;
            vResult.CostoTerminadoCalculadoAPartirDeAsEnum = _CostoTerminadoCalculadoAPartirDe;
            vResult.CodigoMonedaCostoProduccion = _CodigoMonedaCostoProduccion;
            vResult.Moneda = _Moneda;
            vResult.CambioCostoProduccion = _CambioCostoProduccion;
            vResult.ConsecutivoListaDeMateriales = _ConsecutivoListaDeMateriales;
            vResult.CodigoListaDeMateriales = _CodigoListaDeMateriales;
            vResult.NombreListaDeMateriales = _NombreListaDeMateriales;
            vResult.CantidadAProducir = _CantidadAProducir;
            vResult.CantidadProducida = _CantidadProducida;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nC�digo de Orden = " + _Codigo +
               "\nDescripci�n = " + _Descripcion +
               "\nStatus = " + _StatusOp.ToString() +
               "\nConsecutivo Almacen Producto Terminado = " + _ConsecutivoAlmacenProductoTerminado.ToString() +
               "\nConsecutivo Almacen Materiales = " + _ConsecutivoAlmacenMateriales.ToString() +
               "\nFecha de Creaci�n = " + _FechaCreacion.ToShortDateString() +
               "\nFecha de Inicio = " + _FechaInicio.ToShortDateString() +
               "\nFecha de Finalizaci�n = " + _FechaFinalizacion.ToShortDateString() +
               "\nFecha de Anulaci�n = " + _FechaAnulacion.ToShortDateString() +
               "\nFecha de Ajuste = " + _FechaAjuste.ToShortDateString() +
               "\nAjustada por Cierre = " + _AjustadaPostCierre +
               "\nObservaci�n = " + _Observacion +
               "\nMotivo de Anulaci�n = " + _MotivoDeAnulacion +
               "\nNumero Decimales = " + _NumeroDecimales.ToString() +
               "\nCosto Terminado Calculado A Partir De = " + _CostoTerminadoCalculadoAPartirDe.ToString() +
               "\nMoneda para el Costo = " + _CodigoMonedaCostoProduccion +
               "\nCambio a Moneda Local = " + _CambioCostoProduccion.ToString() +
               "\nConsecutivo Lista De Materiales = " + _ConsecutivoListaDeMateriales.ToString() +
               "\nCantidad a Producir = " + _CantidadAProducir.ToString() +
               "\nCantidad Producida = " + _CantidadProducida.ToString() +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados
    } //End of class OrdenDeProduccion
} //End of namespace Galac.Adm.Ccl.GestionProduccion

