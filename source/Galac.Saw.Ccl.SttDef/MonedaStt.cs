using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Ccl.SttDef {
    [Serializable]
    public class MonedaStt : ISettDefinition {
        private string _GroupName = null;
        private string _Module = null;

        public string GroupName {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        public string Module {
            get { return _Module; }
            set { _Module = value; }
        }
        #region Variables
        private string _CodigoMonedaLocal;
        private string _NombreMonedaLocal;
        private bool _UsaMonedaExtranjera;
        private eTipoDeSolicitudDeIngresoDeTasaDeCambio _SolicitarIngresoDeTasaDeCambioAlEmitir;
        private string _CodigoMonedaExtranjera;
        private string _NombreMonedaExtranjera;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string CodigoMonedaLocal {
            get { return _CodigoMonedaLocal; }
            set { _CodigoMonedaLocal = LibString.Mid(value, 0, 4); }
        }

        public string NombreMonedaLocal {
            get { return _NombreMonedaLocal; }
            set { _NombreMonedaLocal = LibString.Mid(value, 0, 80); }
        }

        public bool UsaMonedaExtranjeraAsBool {
            get { return _UsaMonedaExtranjera; }
            set { _UsaMonedaExtranjera = value; }
        }

        public string UsaMonedaExtranjera {
            set { _UsaMonedaExtranjera = LibConvert.SNToBool(value); }
        }


        public eTipoDeSolicitudDeIngresoDeTasaDeCambio SolicitarIngresoDeTasaDeCambioAlEmitirAsEnum {
            get { return _SolicitarIngresoDeTasaDeCambioAlEmitir; }
            set { _SolicitarIngresoDeTasaDeCambioAlEmitir = value; }
        }

        public string SolicitarIngresoDeTasaDeCambioAlEmitir {
            set { _SolicitarIngresoDeTasaDeCambioAlEmitir = (eTipoDeSolicitudDeIngresoDeTasaDeCambio)LibConvert.DbValueToEnum(value); }
        }

        public string SolicitarIngresoDeTasaDeCambioAlEmitirAsDB {
            get { return LibConvert.EnumToDbValue((int) _SolicitarIngresoDeTasaDeCambioAlEmitir); }
        }

        public string SolicitarIngresoDeTasaDeCambioAlEmitirAsString {
            get { return LibEnumHelper.GetDescription(_SolicitarIngresoDeTasaDeCambioAlEmitir); }
        }

        public string CodigoMonedaExtranjera {
            get { return _CodigoMonedaExtranjera; }
            set { _CodigoMonedaExtranjera = LibString.Mid(value, 0, 4); }
        }

        public string NombreMonedaExtranjera {
            get { return _NombreMonedaExtranjera; }
            set { _NombreMonedaExtranjera = LibString.Mid(value, 0, 80); }
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

        public MonedaStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            CodigoMonedaLocal = "";
            NombreMonedaLocal = "";
            UsaMonedaExtranjeraAsBool = false;
            SolicitarIngresoDeTasaDeCambioAlEmitirAsEnum = eTipoDeSolicitudDeIngresoDeTasaDeCambio.SiempreAlEmitirPrimeraFactura;
            CodigoMonedaExtranjera = "";
            NombreMonedaExtranjera = "";
            fldTimeStamp = 0;
        }

        public MonedaStt Clone() {
            MonedaStt vResult = new MonedaStt();
            vResult.CodigoMonedaLocal = _CodigoMonedaLocal;
            vResult.NombreMonedaLocal = _NombreMonedaLocal;
            vResult.UsaMonedaExtranjeraAsBool = _UsaMonedaExtranjera;
            vResult.SolicitarIngresoDeTasaDeCambioAlEmitirAsEnum = _SolicitarIngresoDeTasaDeCambioAlEmitir;
            vResult.CodigoMonedaExtranjera = _CodigoMonedaExtranjera;
            vResult.NombreMonedaExtranjera = _NombreMonedaExtranjera;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Código = " + _CodigoMonedaLocal +
               "\nNombre Moneda Local = " + _NombreMonedaLocal +
               "\nUsa Moneda Extranjera = " + _UsaMonedaExtranjera +
               "\nSolicitar Ingreso De Tasa De Cambio Al Emitir = " + _SolicitarIngresoDeTasaDeCambioAlEmitir.ToString() +
               "\nCodigo Moneda Extranjera = " + _CodigoMonedaExtranjera +
               "\nNombre Moneda Extranjera = " + _NombreMonedaExtranjera;
        }
        #endregion //Metodos Generados


    } //End of class MonedaStt

} //End of namespace Galac.Saw.Ccl.SttDef

