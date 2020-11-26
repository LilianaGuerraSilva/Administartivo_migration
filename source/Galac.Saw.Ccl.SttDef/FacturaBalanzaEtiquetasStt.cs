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
    public class FacturaBalanzaEtiquetasStt: ISettDefinition {
      private string _GroupName = null;
      private string _Module = null;

      public string GroupName
      {
         get { return _GroupName; }
         set { _GroupName = value; }
      }

      public string Module
      {
         get { return _Module; }
         set { _Module = value; }
      }
        #region Variables
        private bool _UsaPesoEnCodigo;
        private string _PrefijoCodigoPeso;
        private int _NumDigitosCodigoArticuloPeso;
        private int _PosicionCodigoArticuloPeso;
        private int _NumDigitosPeso;
        private int _NumDecimalesPeso;
        private bool _UsaPrecioEnCodigo;
        private string _PrefijoCodigoPrecio;
        private int _NumDigitosCodigoArticuloPrecio;
        private int _PosicionCodigoArticuloPrecio;
        private int _NumDigitosPrecio;
        private int _NumDecimalesPrecio;
        private bool _PrecioIncluyeIva;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public bool UsaPesoEnCodigoAsBool {
            get { return _UsaPesoEnCodigo; }
            set { _UsaPesoEnCodigo = value; }
        }

        public string UsaPesoEnCodigo {
            set { _UsaPesoEnCodigo = LibConvert.SNToBool(value); }
        }


        public string PrefijoCodigoPeso {
            get { return _PrefijoCodigoPeso; }
            set { _PrefijoCodigoPeso = LibString.Mid(value, 0, 2); }
        }

        public int NumDigitosCodigoArticuloPeso {
            get { return _NumDigitosCodigoArticuloPeso; }
            set { _NumDigitosCodigoArticuloPeso = value; }
        }

        public int PosicionCodigoArticuloPeso {
            get { return _PosicionCodigoArticuloPeso; }
            set { _PosicionCodigoArticuloPeso = value; }
        }

        public int NumDigitosPeso {
            get { return _NumDigitosPeso; }
            set { _NumDigitosPeso = value; }
        }

        public int NumDecimalesPeso {
            get { return _NumDecimalesPeso; }
            set { _NumDecimalesPeso = value; }
        }

        public bool UsaPrecioEnCodigoAsBool {
            get { return _UsaPrecioEnCodigo; }
            set { _UsaPrecioEnCodigo = value; }
        }

        public string UsaPrecioEnCodigo {
            set { _UsaPrecioEnCodigo = LibConvert.SNToBool(value); }
        }


        public string PrefijoCodigoPrecio {
            get { return _PrefijoCodigoPrecio; }
            set { _PrefijoCodigoPrecio = LibString.Mid(value, 0, 2); }
        }

        public int NumDigitosCodigoArticuloPrecio {
            get { return _NumDigitosCodigoArticuloPrecio; }
            set { _NumDigitosCodigoArticuloPrecio = value; }
        }

        public int PosicionCodigoArticuloPrecio {
            get { return _PosicionCodigoArticuloPrecio; }
            set { _PosicionCodigoArticuloPrecio = value; }
        }

        public int NumDigitosPrecio {
            get { return _NumDigitosPrecio; }
            set { _NumDigitosPrecio = value; }
        }

        public int NumDecimalesPrecio {
            get { return _NumDecimalesPrecio; }
            set { _NumDecimalesPrecio = value; }
        }

        public bool PrecioIncluyeIvaAsBool {
            get { return _PrecioIncluyeIva; }
            set { _PrecioIncluyeIva = value; }
        }

        public string PrecioIncluyeIva {
            set { _PrecioIncluyeIva = LibConvert.SNToBool(value); }
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

        public FacturaBalanzaEtiquetasStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            UsaPesoEnCodigoAsBool = false;
            PrefijoCodigoPeso = string.Empty;
            NumDigitosCodigoArticuloPeso = 0;
            PosicionCodigoArticuloPeso = 0;
            NumDigitosPeso = 0;
            NumDecimalesPeso = 0;
            UsaPrecioEnCodigoAsBool = false;
            PrefijoCodigoPrecio = string.Empty;
            NumDigitosCodigoArticuloPrecio = 0;
            PosicionCodigoArticuloPrecio = 0;
            NumDigitosPrecio = 0;
            NumDecimalesPrecio = 0;
            PrecioIncluyeIvaAsBool = true;
            fldTimeStamp = 0;
        }

        public FacturaBalanzaEtiquetasStt Clone() {
            FacturaBalanzaEtiquetasStt vResult = new FacturaBalanzaEtiquetasStt();
            vResult.UsaPesoEnCodigoAsBool = _UsaPesoEnCodigo;
            vResult.PrefijoCodigoPeso = _PrefijoCodigoPeso;
            vResult.NumDigitosCodigoArticuloPeso = _NumDigitosCodigoArticuloPeso;
            vResult.PosicionCodigoArticuloPeso = _PosicionCodigoArticuloPeso;
            vResult.NumDigitosPeso = _NumDigitosPeso;
            vResult.NumDecimalesPeso = _NumDecimalesPeso;
            vResult.UsaPrecioEnCodigoAsBool = _UsaPrecioEnCodigo;
            vResult.PrefijoCodigoPrecio = _PrefijoCodigoPrecio;
            vResult.NumDigitosCodigoArticuloPrecio = _NumDigitosCodigoArticuloPrecio;
            vResult.PosicionCodigoArticuloPrecio = _PosicionCodigoArticuloPrecio;
            vResult.NumDigitosPrecio = _NumDigitosPrecio;
            vResult.NumDecimalesPrecio = _NumDecimalesPrecio;
            vResult.PrecioIncluyeIvaAsBool = _PrecioIncluyeIva;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return  "\nUsa Peso en el Codigo = " + _UsaPesoEnCodigo +
               "\nPrefijo del Codigo = " + _PrefijoCodigoPeso +
               "\nNumero de Digitos del Codigo del Articulo = " + _NumDigitosCodigoArticuloPeso.ToString() +
               "\nPosicion en el codigo de barras = " + _PosicionCodigoArticuloPeso.ToString() +
               "\nNumero de Digitos del Peso = " + _NumDigitosPeso.ToString() +
               "\nNumero de Decimales del Peso = " + _NumDecimalesPeso.ToString() +
               "\nUsa Precio en el Codigo = " + _UsaPrecioEnCodigo +
               "\nPrefijo del Codigo = " + _PrefijoCodigoPrecio +
               "\nNumero de Digitos del Codigo del Articulo = " + _NumDigitosCodigoArticuloPrecio.ToString() +
               "\nPosicion en el codigo de barras = " + _PosicionCodigoArticuloPrecio.ToString() +
               "\nNumero de Digitos del Precio = " + _NumDigitosPrecio.ToString() +
               "\nNumero de Decimales del Precio = " + _NumDecimalesPrecio.ToString() +
               "\nEl Precio en el Codigo Incluye Iva = " + _PrecioIncluyeIva;
        }
        #endregion //Metodos Generados


    } //End of class FacturaBalanzaEtiquetasStt

} //End of namespace Galac.Saw.Ccl.SttDef

