using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Saw.Ccl.SttDef {
    [Serializable]
    public class AnticipoStt : ISettDefinition {
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
        private string _CuentaBancariaAnticipo;
        private bool _SugerirConsecutivoAnticipo;
        private string _ConceptoBancarioAnticipoCobrado;
        private string _ConceptoBancarioReversoAnticipoCobrado;
        private string _NombrePlantillaReciboDeAnticipoCobrado;
        private string _NombrePlantillaReciboDeAnticipoPagado;
        private string _ConceptoBancarioReversoAnticipoPagado;
        private eComprobanteConCheque _TipoComprobanteDeAnticipoAImprimir;
        private string _ConceptoBancarioAnticipoPagado;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string CuentaBancariaAnticipo {
            get { return _CuentaBancariaAnticipo; }
            set { _CuentaBancariaAnticipo = LibString.Mid(value, 0, 5); }
        }

        public bool SugerirConsecutivoAnticipoAsBool {
            get { return _SugerirConsecutivoAnticipo; }
            set { _SugerirConsecutivoAnticipo = value; }
        }

        public string SugerirConsecutivoAnticipo {
            set { _SugerirConsecutivoAnticipo = LibConvert.SNToBool(value); }
        }


        public string ConceptoBancarioAnticipoCobrado {
            get { return _ConceptoBancarioAnticipoCobrado; }
            set { _ConceptoBancarioAnticipoCobrado = LibString.Mid(value, 0, 8); }
        }

        public string ConceptoBancarioReversoAnticipoCobrado {
            get { return _ConceptoBancarioReversoAnticipoCobrado; }
            set { _ConceptoBancarioReversoAnticipoCobrado = LibString.Mid(value, 0, 8); }
        }

        public string NombrePlantillaReciboDeAnticipoCobrado {
            get { return _NombrePlantillaReciboDeAnticipoCobrado; }
            set { _NombrePlantillaReciboDeAnticipoCobrado = LibString.Mid(value, 0, 50); }
        }

        public string NombrePlantillaReciboDeAnticipoPagado {
            get { return _NombrePlantillaReciboDeAnticipoPagado; }
            set { _NombrePlantillaReciboDeAnticipoPagado = LibString.Mid(value, 0, 50); }
        }

        public string ConceptoBancarioReversoAnticipoPagado {
            get { return _ConceptoBancarioReversoAnticipoPagado; }
            set { _ConceptoBancarioReversoAnticipoPagado = LibString.Mid(value, 0, 8); }
        }

        public eComprobanteConCheque TipoComprobanteDeAnticipoAImprimirAsEnum {
            get { return _TipoComprobanteDeAnticipoAImprimir; }
            set { _TipoComprobanteDeAnticipoAImprimir = value; }
        }

        public string TipoComprobanteDeAnticipoAImprimir {
            set { _TipoComprobanteDeAnticipoAImprimir = (eComprobanteConCheque)LibConvert.DbValueToEnum(value); }
        }

        public string TipoComprobanteDeAnticipoAImprimirAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoComprobanteDeAnticipoAImprimir); }
        }

        public string TipoComprobanteDeAnticipoAImprimirAsString {
            get { return LibEnumHelper.GetDescription(_TipoComprobanteDeAnticipoAImprimir); }
        }

        public string ConceptoBancarioAnticipoPagado {
            get { return _ConceptoBancarioAnticipoPagado; }
            set { _ConceptoBancarioAnticipoPagado = LibString.Mid(value, 0, 8); }
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

        public AnticipoStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            CuentaBancariaAnticipo = "";
            SugerirConsecutivoAnticipoAsBool = false;
            ConceptoBancarioAnticipoCobrado = "";
            ConceptoBancarioReversoAnticipoCobrado = "";
            NombrePlantillaReciboDeAnticipoCobrado = "";
            NombrePlantillaReciboDeAnticipoPagado = "";
            ConceptoBancarioReversoAnticipoPagado = "";
            TipoComprobanteDeAnticipoAImprimirAsEnum = eComprobanteConCheque.ComprobanteconCheque;
            ConceptoBancarioAnticipoPagado = "";
            fldTimeStamp = 0;
        }

        public AnticipoStt Clone() {
            AnticipoStt vResult = new AnticipoStt();
            vResult.CuentaBancariaAnticipo = _CuentaBancariaAnticipo;
            vResult.SugerirConsecutivoAnticipoAsBool = _SugerirConsecutivoAnticipo;
            vResult.ConceptoBancarioAnticipoCobrado = _ConceptoBancarioAnticipoCobrado;
            vResult.ConceptoBancarioReversoAnticipoCobrado = _ConceptoBancarioReversoAnticipoCobrado;
            vResult.NombrePlantillaReciboDeAnticipoCobrado = _NombrePlantillaReciboDeAnticipoCobrado;
            vResult.NombrePlantillaReciboDeAnticipoPagado = _NombrePlantillaReciboDeAnticipoPagado;
            vResult.ConceptoBancarioReversoAnticipoPagado = _ConceptoBancarioReversoAnticipoPagado;
            vResult.TipoComprobanteDeAnticipoAImprimirAsEnum = _TipoComprobanteDeAnticipoAImprimir;
            vResult.ConceptoBancarioAnticipoPagado = _ConceptoBancarioAnticipoPagado;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Cuenta Bancaria = " + _CuentaBancariaAnticipo +
               "\nSugerir Consecutivo Anticipo = " + _SugerirConsecutivoAnticipo +
               "\nCódigo Concepto para Anticipo Cobrado = " + _ConceptoBancarioAnticipoCobrado +
               "\nCódigo Concepto de Reverso de Anticipo Cobrado = " + _ConceptoBancarioReversoAnticipoCobrado +
               "\nNombre Plantilla Recibo De Anticipo Cobrado = " + _NombrePlantillaReciboDeAnticipoCobrado +
               "\nNombre Plantilla Recibo De Anticipo Pagado = " + _NombrePlantillaReciboDeAnticipoPagado +
               "\nCódigo Concepto de Reverso de Anticipo Pagado = " + _ConceptoBancarioReversoAnticipoPagado +
               "\nTipo Comprobante De Anticipo AImprimir = " + _TipoComprobanteDeAnticipoAImprimir.ToString() +
               "\nConceptoBancarioAnticipoPagado  = " + _ConceptoBancarioAnticipoPagado;
        }
        #endregion //Metodos Generados


    } //End of class AnticipoStt

} //End of namespace Galac.Saw.Ccl.SttDef

