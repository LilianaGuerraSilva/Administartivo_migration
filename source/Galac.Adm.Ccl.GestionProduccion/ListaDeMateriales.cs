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
using Galac.Adm.Ccl.GestionProduccion;

namespace Galac.Adm.Ccl.GestionProduccion {
    [Serializable]
    public class ListaDeMateriales {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private string _Codigo;
        private string _Nombre;
        private string _CodigoArticuloInventario;
        private DateTime _FechaCreacion;
        private bool _ManejaMerma;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
		private ObservableCollection<ListaDeMaterialesDetalleArticulo> _DetailListaDeMaterialesDetalleArticulo;
		private ObservableCollection<ListaDeMaterialesDetalleSalidas> _DetailListaDeMaterialesDetalleSalidas;
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
            set { _Codigo = LibString.Mid(value, 0, 30); }
        }

        public string Nombre {
            get { return _Nombre; }
            set { _Nombre = LibString.Mid(value, 0, 255); }
        }

        public string CodigoArticuloInventario {
            get { return _CodigoArticuloInventario; }
            set { _CodigoArticuloInventario = LibString.Mid(value, 0, 30); }
        }

        public DateTime FechaCreacion {
            get { return _FechaCreacion; }
            set { _FechaCreacion = LibConvert.DateToDbValue(value); }
        }

        public bool ManejaMermaAsBool {
            get { return _ManejaMerma; }
            set { _ManejaMerma = value; }
        }

        public string ManejaMerma {
            set { _ManejaMerma = LibConvert.SNToBool(value); }
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

        public ObservableCollection<ListaDeMaterialesDetalleArticulo> DetailListaDeMaterialesDetalleArticulo {
            get { return _DetailListaDeMaterialesDetalleArticulo; }
            set { _DetailListaDeMaterialesDetalleArticulo = value; }
        }

        public ObservableCollection<ListaDeMaterialesDetalleSalidas> DetailListaDeMaterialesDetalleSalidas {
            get { return _DetailListaDeMaterialesDetalleSalidas; }
            set { _DetailListaDeMaterialesDetalleSalidas = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public ListaDeMateriales() {
            _DetailListaDeMaterialesDetalleArticulo = new ObservableCollection<ListaDeMaterialesDetalleArticulo>();
            _DetailListaDeMaterialesDetalleSalidas = new ObservableCollection<ListaDeMaterialesDetalleSalidas>();
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
            Nombre = string.Empty;
            CodigoArticuloInventario = string.Empty;
            FechaCreacion = LibDate.Today();
            ManejaMermaAsBool = false;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
            DetailListaDeMaterialesDetalleArticulo = new ObservableCollection<ListaDeMaterialesDetalleArticulo>();
            DetailListaDeMaterialesDetalleSalidas = new ObservableCollection<ListaDeMaterialesDetalleSalidas>();
        }

        public ListaDeMateriales Clone() {
            ListaDeMateriales vResult = new ListaDeMateriales();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.Codigo = _Codigo;
            vResult.Nombre = _Nombre;
            vResult.CodigoArticuloInventario = _CodigoArticuloInventario;
            vResult.FechaCreacion = _FechaCreacion;
            vResult.ManejaMermaAsBool = _ManejaMerma;
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
               "\nFecha de Creación = " + _FechaCreacion.ToShortDateString() +
               "\nManejo de Merma = " + _ManejaMerma +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class ListaDeMateriales

} //End of namespace Galac.Adm.Ccl.GestionProduccion

