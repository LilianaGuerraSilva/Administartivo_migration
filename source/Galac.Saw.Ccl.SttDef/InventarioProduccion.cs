using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Comun.Ccl.SttDef {
    [Serializable]
    public class InventarioProduccion {
        #region Variables
        private int _ConsecutivoCompania;
        private eCostoTerminadoCalculadoAPartirDe _CalcularCostoDelArticuloTerminadoAPartirDe;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public eCostoTerminadoCalculadoAPartirDe CalcularCostoDelArticuloTerminadoAPartirDeAsEnum {
            get { return _CalcularCostoDelArticuloTerminadoAPartirDe; }
            set { _CalcularCostoDelArticuloTerminadoAPartirDe = value; }
        }

        public string CalcularCostoDelArticuloTerminadoAPartirDe {
            set { _CalcularCostoDelArticuloTerminadoAPartirDe = (eCostoTerminadoCalculadoAPartirDe)LibConvert.DbValueToEnum(value); }
        }

        public string CalcularCostoDelArticuloTerminadoAPartirDeAsDB {
            get { return LibConvert.EnumToDbValue((int) _CalcularCostoDelArticuloTerminadoAPartirDe); }
        }

        public string CalcularCostoDelArticuloTerminadoAPartirDeAsString {
            get { return LibEnumHelper.GetDescription(_CalcularCostoDelArticuloTerminadoAPartirDe); }
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

        public InventarioProduccion() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            CalcularCostoDelArticuloTerminadoAPartirDeAsEnum = eCostoTerminadoCalculadoAPartirDe.MonedaLocal;
            fldTimeStamp = 0;
        }

        public InventarioProduccion Clone() {
            InventarioProduccion vResult = new InventarioProduccion();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.CalcularCostoDelArticuloTerminadoAPartirDeAsEnum = _CalcularCostoDelArticuloTerminadoAPartirDe;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nCalcular el costo del artículo terminado a partir de = " + _CalcularCostoDelArticuloTerminadoAPartirDe.ToString();
        }
        #endregion //Metodos Generados


    } //End of class InventarioProduccion

} //End of namespace Galac.Comun.Ccl.SttDef

