using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Saw.Ccl.Inventario {
    [Serializable]
    public class Almacen {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private string _Codigo;
        private string _NombreAlmacen;
        private eTipoDeAlmacen _TipoDeAlmacen;
        private int _ConsecutivoCliente;
        private string _NombreCliente;
        private string _CodigoCc;
        private string _Descripcion;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
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

        public string NombreAlmacen {
            get { return _NombreAlmacen; }
            set { _NombreAlmacen = LibString.Mid(value, 0, 40); }
        }

        public eTipoDeAlmacen TipoDeAlmacenAsEnum {
            get { return _TipoDeAlmacen; }
            set { _TipoDeAlmacen = value; }
        }

        public string TipoDeAlmacen {
            set { _TipoDeAlmacen = (eTipoDeAlmacen)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeAlmacenAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeAlmacen); }
        }

        public string TipoDeAlmacenAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeAlmacen); }
        }

        public int ConsecutivoCliente {
            get { return _ConsecutivoCliente; }
            set { _ConsecutivoCliente = value; }
        }

        public string NombreCliente {
            get { return _NombreCliente; }
            set { _NombreCliente = LibString.Mid(value, 0, 80); }
        }

        public string CodigoCc {
            get { return _CodigoCc; }
            set { _CodigoCc = LibString.Mid(value, 0, 5); }
        }

        public string Descripcion {
            get { return _Descripcion; }
            set { _Descripcion = LibString.Mid(value, 0, 40); }
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

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public Almacen() {
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
            NombreAlmacen = "";
            TipoDeAlmacenAsEnum = eTipoDeAlmacen.Principal;
            ConsecutivoCliente = 0;
            NombreCliente = "";
            CodigoCc = "";
            Descripcion = "";
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public Almacen Clone() {
            Almacen vResult = new Almacen();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.Codigo = _Codigo;
            vResult.NombreAlmacen = _NombreAlmacen;
            vResult.TipoDeAlmacenAsEnum = _TipoDeAlmacen;
            vResult.ConsecutivoCliente = _ConsecutivoCliente;
            vResult.NombreCliente = _NombreCliente;
            vResult.CodigoCc = _CodigoCc;
            vResult.Descripcion = _Descripcion;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nCódigo = " + _Codigo +
               "\nNombre Almacén = " + _NombreAlmacen +
               "\nTipo de Almacén = " + _TipoDeAlmacen.ToString() +
               "\nConsecutivoCliente = " + _ConsecutivoCliente.ToString() +
               "\nCódigo Centro de Costos = " + _CodigoCc +
               "\nDescripción Centro de Costos = " + _Descripcion +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class Almacen

} //End of namespace Galac.Saw.Ccl.Inventario

