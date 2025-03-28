using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Ccl.Inventario {
    [Serializable]
    public class RenglonExistenciaAlmacen {
        #region Variables
        private int _ConsecutivoCompania;
        private string _CodigoAlmacen;
        private string _CodigoArticulo;
        private int _ConsecutivoRenglon;
        private string _CodigoSerial;
        private string _CodigoRollo;
        private decimal _Cantidad;
        private string _Ubicacion;
        private int _ConsecutivoAlmacen;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string CodigoAlmacen {
            get { return _CodigoAlmacen; }
            set { _CodigoAlmacen = LibString.Mid(value, 0, 5); }
        }

        public string CodigoArticulo {
            get { return _CodigoArticulo; }
            set { _CodigoArticulo = LibString.Mid(value, 0, 30); }
        }

        public int ConsecutivoRenglon {
            get { return _ConsecutivoRenglon; }
            set { _ConsecutivoRenglon = value; }
        }

        public string CodigoSerial {
            get { return _CodigoSerial; }
            set { _CodigoSerial = LibString.Mid(value, 0, 50); }
        }

        public string CodigoRollo {
            get { return _CodigoRollo; }
            set { _CodigoRollo = LibString.Mid(value, 0, 20); }
        }

        public decimal Cantidad {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        public string Ubicacion {
            get { return _Ubicacion; }
            set { _Ubicacion = LibString.Mid(value, 0, 30); }
        }

        public int ConsecutivoAlmacen {
            get { return _ConsecutivoAlmacen; }
            set { _ConsecutivoAlmacen = value; }
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

        public RenglonExistenciaAlmacen() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            CodigoAlmacen = string.Empty;
            CodigoArticulo = string.Empty;
            ConsecutivoRenglon = 0;
            CodigoSerial = string.Empty;
            CodigoRollo = string.Empty;
            Cantidad = 0;
            Ubicacion = string.Empty;
            ConsecutivoAlmacen = 0;
            fldTimeStamp = 0;
        }

        public RenglonExistenciaAlmacen Clone() {
            RenglonExistenciaAlmacen vResult = new RenglonExistenciaAlmacen();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.CodigoAlmacen = _CodigoAlmacen;
            vResult.CodigoArticulo = _CodigoArticulo;
            vResult.ConsecutivoRenglon = _ConsecutivoRenglon;
            vResult.CodigoSerial = _CodigoSerial;
            vResult.CodigoRollo = _CodigoRollo;
            vResult.Cantidad = _Cantidad;
            vResult.Ubicacion = _Ubicacion;
            vResult.ConsecutivoAlmacen = _ConsecutivoAlmacen;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nCodigo Almacen = " + _CodigoAlmacen +
               "\nCodigo Articulo = " + _CodigoArticulo +
               "\nConsecutivo Renglon = " + _ConsecutivoRenglon.ToString() +
               "\nCodigo Serial = " + _CodigoSerial +
               "\nCodigo Rollo = " + _CodigoRollo +
               "\nCantidad = " + _Cantidad.ToString() +
               "\nUbicación = " + _Ubicacion +
               "\nConsecutivo Almacen = " + _ConsecutivoAlmacen.ToString();
        }
        #endregion //Metodos Generados


    } //End of class RenglonExistenciaAlmacen

} //End of namespace Galac.Saw.Ccl.Inventario

