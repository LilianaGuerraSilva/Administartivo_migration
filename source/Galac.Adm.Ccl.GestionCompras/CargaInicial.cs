using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Ccl.GestionCompras {
    [Serializable]
    public class CargaInicial {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private DateTime _Fecha;
        private bool tieneCambios = false;
        private decimal _Existencia;
        private decimal _Costo;
        private string _CodigoArticulo;
        private string _EsCargaInicial;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables

        #region Propiedades
        public decimal CostoInicial { get; set; }

        public bool TieneCambios {
            get { return tieneCambios; }
            set { tieneCambios = value; }
        }

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int Consecutivo {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }

        public DateTime Fecha {
            get { return _Fecha; }
            set { _Fecha = LibConvert.DateToDbValue(value); }
        }

        public decimal Existencia {
            get { return decimal.Round(_Existencia, 2); }
            set { _Existencia = value; }
        }

        public decimal Costo {
            get { return decimal.Round(_Costo, 2); }
            set {
                _Costo = value;
                if (_Costo != CostoInicial) {
                    TieneCambios = true;
                } else TieneCambios = false;
            }
        }

        public string CodigoArticulo {
            get { return _CodigoArticulo; }
            set { _CodigoArticulo = LibString.Mid(value, 0, 30); }
        }

        public string EsCargaInicial {
            get { return _EsCargaInicial; }
            set { _EsCargaInicial = LibString.Mid(value, 0, 1); }
        }

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades

        #region Constructores

        public CargaInicial() {
            Clear();
        }

        public CargaInicial(decimal costoInicial) {
            Clear();
            this.CostoInicial = costoInicial;
        }
        #endregion //Constructores

        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            Consecutivo = 0;
            Fecha = LibDate.Today();
            Existencia = 0;
            Costo = 0;
            CodigoArticulo = string.Empty;
            EsCargaInicial = string.Empty;
            fldTimeStamp = 0;
            CostoInicial = 0;
            TieneCambios = false;

        }

        public CargaInicial Clone() {
            CargaInicial vResult = new CargaInicial();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.Fecha = _Fecha;
            vResult.Existencia = _Existencia;
            vResult.Costo = _Costo;
            vResult.CodigoArticulo = _CodigoArticulo;
            vResult.EsCargaInicial = _EsCargaInicial;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
            return "Consecutivo Compañía = " + _ConsecutivoCompania.ToString() +
                "\nConsecutivo = " + _Consecutivo.ToString() +
                "\nFecha = " + _Fecha.ToShortDateString() +
                "\nCantidad del Artículo = " + _Existencia.ToString() +
                "\nCosto = " + _Costo.ToString() +
                "\nCódigo del Artículo = " + _CodigoArticulo +
                "\nEs Carga Inicial = " + _EsCargaInicial;
        }
        #endregion //Metodos Generados
    } //End of class CargaInicial
} //End of namespace Galac.Adm.Ccl.GestionCompras

