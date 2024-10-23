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
    public class LoteDeInventario {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private string _CodigoLote;
        private string _CodigoArticulo;
        private DateTime _FechaDeElaboracion;
        private DateTime _FechaDeVencimiento;
        private decimal _Existencia;
        private eStatusLoteDeInventario _StatusLoteInv;
        private string _DescripcionArticulo;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
		private ObservableCollection<LoteDeInventarioMovimiento> _DetailLoteDeInventarioMovimiento;
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

        public string CodigoLote {
            get { return _CodigoLote; }
            set { _CodigoLote = LibString.Mid(value, 0, 30); }
        }

        public string CodigoArticulo {
            get { return _CodigoArticulo; }
            set { _CodigoArticulo = LibString.Mid(value, 0, 30); }
        }

        public DateTime FechaDeElaboracion {
            get { return _FechaDeElaboracion; }
            set { _FechaDeElaboracion = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaDeVencimiento {
            get { return _FechaDeVencimiento; }
            set { _FechaDeVencimiento = LibConvert.DateToDbValue(value); }
        }

        public decimal Existencia {
            get { return _Existencia; }
            set { _Existencia = value; }
        }

        public eStatusLoteDeInventario StatusLoteInvAsEnum {
            get { return _StatusLoteInv; }
            set { _StatusLoteInv = value; }
        }

        public string StatusLoteInv {
            set { _StatusLoteInv = (eStatusLoteDeInventario)LibConvert.DbValueToEnum(value); }
        }

        public string StatusLoteInvAsDB {
            get { return LibConvert.EnumToDbValue((int) _StatusLoteInv); }
        }

        public string StatusLoteInvAsString {
            get { return LibEnumHelper.GetDescription(_StatusLoteInv); }
        }

        public string DescripcionArticulo {
            get { return _DescripcionArticulo; }
            set { _DescripcionArticulo = LibString.Mid(value, 0, 150); }
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

        public ObservableCollection<LoteDeInventarioMovimiento> DetailLoteDeInventarioMovimiento {
            get { return _DetailLoteDeInventarioMovimiento; }
            set { _DetailLoteDeInventarioMovimiento = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public LoteDeInventario() {
            _DetailLoteDeInventarioMovimiento = new ObservableCollection<LoteDeInventarioMovimiento>();
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
            CodigoLote = string.Empty;
            CodigoArticulo = string.Empty;
            FechaDeElaboracion = LibDate.Today();
            FechaDeVencimiento = LibDate.Today();
            Existencia = 0;
            StatusLoteInvAsEnum = eStatusLoteDeInventario.Vigente;
            DescripcionArticulo = string.Empty;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
            DetailLoteDeInventarioMovimiento = new ObservableCollection<LoteDeInventarioMovimiento>();
        }

        public LoteDeInventario Clone() {
            LoteDeInventario vResult = new LoteDeInventario();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.CodigoLote = _CodigoLote;
            vResult.CodigoArticulo = _CodigoArticulo;
            vResult.FechaDeElaboracion = _FechaDeElaboracion;
            vResult.FechaDeVencimiento = _FechaDeVencimiento;
            vResult.Existencia = _Existencia;
            vResult.StatusLoteInvAsEnum = _StatusLoteInv;
            vResult.DescripcionArticulo = _DescripcionArticulo;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nCódigo = " + _CodigoLote +
               "\nCódigo de Artículo = " + _CodigoArticulo +
               "\nFecha Elab. = " + _FechaDeElaboracion.ToShortDateString() +
               "\nFecha Vcto. = " + _FechaDeVencimiento.ToShortDateString() +
               "\nExistencia = " + _Existencia.ToString() +
               "\nStatus = " + _StatusLoteInv.ToString() +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class LoteDeInventario

} //End of namespace Galac.Saw.Ccl.Inventario

