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
    public class VendedorStt : ISettDefinition {
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
        private bool _UsaCodigoVendedorEnPantalla;
        private string _CodigoGenericoVendedor;
        private int _LongitudCodigoVendedor;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public bool UsaCodigoVendedorEnPantallaAsBool {
            get { return _UsaCodigoVendedorEnPantalla; }
            set { _UsaCodigoVendedorEnPantalla = value; }
        }

        public string UsaCodigoVendedorEnPantalla {
            set { _UsaCodigoVendedorEnPantalla = LibConvert.SNToBool(value); }
        }


        public string CodigoGenericoVendedor {
            get { return _CodigoGenericoVendedor; }
            set { _CodigoGenericoVendedor = LibString.Mid(value, 0, 5); }
        }

        public int LongitudCodigoVendedor {
            get { return _LongitudCodigoVendedor; }
            set { _LongitudCodigoVendedor = value; }
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

        public VendedorStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            UsaCodigoVendedorEnPantallaAsBool = false;
            CodigoGenericoVendedor = "";
            LongitudCodigoVendedor = 0;
            fldTimeStamp = 0;
        }

        public VendedorStt Clone() {
            VendedorStt vResult = new VendedorStt();
            vResult.UsaCodigoVendedorEnPantallaAsBool = _UsaCodigoVendedorEnPantalla;
            vResult.CodigoGenericoVendedor = _CodigoGenericoVendedor;
            vResult.LongitudCodigoVendedor = _LongitudCodigoVendedor;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "sar el código del Vendedor... = " + _UsaCodigoVendedorEnPantalla +
               "\nVendedor genérico .......... = " + _CodigoGenericoVendedor +
               "\nLongitud del código del Vendedor = " + _LongitudCodigoVendedor.ToString();
        }
        #endregion //Metodos Generados


    } //End of class VendedorStt

} //End of namespace Galac.Saw.Ccl.SttDef

