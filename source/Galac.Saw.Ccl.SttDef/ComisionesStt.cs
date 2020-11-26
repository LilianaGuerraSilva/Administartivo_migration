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
    public class ComisionesStt : ISettDefinition {
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
        private eCalculoParaComisionesSobreCobranzaEnBaseA _FormaDeCalcularComisionesSobreCobranza;
        private string _NombrePlantillaComisionSobreCobranza;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public eCalculoParaComisionesSobreCobranzaEnBaseA FormaDeCalcularComisionesSobreCobranzaAsEnum {
            get { return _FormaDeCalcularComisionesSobreCobranza; }
            set { _FormaDeCalcularComisionesSobreCobranza = value; }
        }

        public string FormaDeCalcularComisionesSobreCobranza {
            set { _FormaDeCalcularComisionesSobreCobranza = (eCalculoParaComisionesSobreCobranzaEnBaseA)LibConvert.DbValueToEnum(value); }
        }

        public string FormaDeCalcularComisionesSobreCobranzaAsDB {
            get { return LibConvert.EnumToDbValue((int) _FormaDeCalcularComisionesSobreCobranza); }
        }

        public string FormaDeCalcularComisionesSobreCobranzaAsString {
            get { return LibEnumHelper.GetDescription(_FormaDeCalcularComisionesSobreCobranza); }
        }

        public string NombrePlantillaComisionSobreCobranza {
            get { return _NombrePlantillaComisionSobreCobranza; }
            set { _NombrePlantillaComisionSobreCobranza = LibString.Mid(value, 0, 50); }
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

        public ComisionesStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            FormaDeCalcularComisionesSobreCobranzaAsEnum = eCalculoParaComisionesSobreCobranzaEnBaseA.Monto;
            NombrePlantillaComisionSobreCobranza = "";
            fldTimeStamp = 0;
        }

        public ComisionesStt Clone() {
            ComisionesStt vResult = new ComisionesStt();
            vResult.FormaDeCalcularComisionesSobreCobranzaAsEnum = _FormaDeCalcularComisionesSobreCobranza;
            vResult.NombrePlantillaComisionSobreCobranza = _NombrePlantillaComisionSobreCobranza;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Forma De Calcular Comisiones Sobre Cobranza = " + _FormaDeCalcularComisionesSobreCobranza.ToString() +
               "\nNombre Plantilla Comision Sobre Cobranza = " + _NombrePlantillaComisionSobreCobranza;
        }
        #endregion //Metodos Generados


    } //End of class ComisionesStt

} //End of namespace Galac.Saw.Ccl.SttDef

