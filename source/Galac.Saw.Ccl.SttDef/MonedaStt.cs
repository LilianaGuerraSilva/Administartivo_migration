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
    public class MonedaStt:ISettDefinition {

        #region Variables

        private string _GroupName = null;
        private string _Module = null;
        private string _CodigoMonedaLocal;
        private string _NombreMonedaLocal;
        private bool _UsaMonedaExtranjera;
        private eTipoDeSolicitudDeIngresoDeTasaDeCambio _SolicitarIngresoDeTasaDeCambioAlEmitir;
        private string _CodigoMonedaExtranjera;
        private string _NombreMonedaExtranjera;
        private bool _UsaDivisaComoMonedaPrincipalDeIngresoDeDatos;
        private bool _UsarLimiteMaximoParaIngresoDeTasaDeCambio;
        private decimal _MaximoLimitePermitidoParaLaTasaDeCambio;
        private long _fldTimeStamp;
        XmlDocument _datos;

        #endregion //Variables

        #region Propiedades

        public string GroupName {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        public string Module {
            get { return _Module; }
            set { _Module = value; }
        }

        public string CodigoMonedaLocal {
            get { return _CodigoMonedaLocal; }
            set { _CodigoMonedaLocal = LibString.Mid(value,0,4); }
        }

        public string NombreMonedaLocal {
            get { return _NombreMonedaLocal; }
            set { _NombreMonedaLocal = LibString.Mid(value,0,80); }
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
            get { return LibConvert.EnumToDbValue((int)_SolicitarIngresoDeTasaDeCambioAlEmitir); }
        }

        public string SolicitarIngresoDeTasaDeCambioAlEmitirAsString {
            get { return LibEnumHelper.GetDescription(_SolicitarIngresoDeTasaDeCambioAlEmitir); }
        }

        public string CodigoMonedaExtranjera {
            get { return _CodigoMonedaExtranjera; }
            set { _CodigoMonedaExtranjera = LibString.Mid(value,0,4); }
        }

        public string NombreMonedaExtranjera {
            get { return _NombreMonedaExtranjera; }
            set { _NombreMonedaExtranjera = LibString.Mid(value,0,80); }
        }

        public bool UsaDivisaComoMonedaPrincipalDeIngresoDeDatosAsBool {
            get { return _UsaDivisaComoMonedaPrincipalDeIngresoDeDatos; }
            set { _UsaDivisaComoMonedaPrincipalDeIngresoDeDatos = value; }
        }

        public bool UsarLimiteMaximoParaIngresoDeTasaDeCambio {
            get { return _UsarLimiteMaximoParaIngresoDeTasaDeCambio; }
            set { _UsarLimiteMaximoParaIngresoDeTasaDeCambio = value; }
        }

        public decimal MaximoLimitePermitidoParaLaTasaDeCambio {
            get { return _MaximoLimitePermitidoParaLaTasaDeCambio; }
            set { _MaximoLimitePermitidoParaLaTasaDeCambio = value; }

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
            UsaDivisaComoMonedaPrincipalDeIngresoDeDatosAsBool = false;
            UsarLimiteMaximoParaIngresoDeTasaDeCambio = false;
            MaximoLimitePermitidoParaLaTasaDeCambio = 30m;
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
            vResult.UsaDivisaComoMonedaPrincipalDeIngresoDeDatosAsBool = _UsaDivisaComoMonedaPrincipalDeIngresoDeDatos;
            vResult.UsarLimiteMaximoParaIngresoDeTasaDeCambio = _UsarLimiteMaximoParaIngresoDeTasaDeCambio;
            vResult.MaximoLimitePermitidoParaLaTasaDeCambio = _MaximoLimitePermitidoParaLaTasaDeCambio;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
            return "Código = " + _CodigoMonedaLocal +
                "\nNombre Moneda Local = " + _NombreMonedaLocal +
                "\nUsa Moneda Extranjera = " + _UsaMonedaExtranjera +
                "\nSolicitar Ingreso De Tasa De Cambio Al Emitir = " + _SolicitarIngresoDeTasaDeCambioAlEmitir.ToString() +
                "\nCodigo Moneda Extranjera = " + _CodigoMonedaExtranjera +
                "\nNombre Moneda Extranjera = " + _NombreMonedaExtranjera +
                "\nUsa Divisa como Moneda Principal de Ingreso de Datos = " + _UsaDivisaComoMonedaPrincipalDeIngresoDeDatos +
                "\nUsar Limite Máximo Para Ingreso De Tasa De Cambio = " + _UsarLimiteMaximoParaIngresoDeTasaDeCambio +
                 "\nMáximo Limite Permitido Para La Tasa De Cambio = " + _MaximoLimitePermitidoParaLaTasaDeCambio;
        }
        #endregion //Metodos Generados
    } //End of class MonedaStt

} //End of namespace Galac.Saw.Ccl.SttDef

