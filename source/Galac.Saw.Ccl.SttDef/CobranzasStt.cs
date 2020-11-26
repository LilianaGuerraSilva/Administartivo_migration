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
    public class CobranzasStt : ISettDefinition {
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
        private bool _UsarZonaCobranza;
        private bool _SugerirConsecutivoEnCobranza;
        private string _ConceptoReversoCobranza;
        private bool _ImprimirCombrobanteAlIngresarCobranza;
        private string _NombrePlantillaCompobanteCobranza;
        private bool _AsignarComisionDeVendedorEnCobranza;
        private bool _CambiarCobradorVendedor;
        private bool _BloquearNumeroCobranza;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public bool UsarZonaCobranzaAsBool {
            get { return _UsarZonaCobranza; }
            set { _UsarZonaCobranza = value; }
        }

        public string UsarZonaCobranza {
            set { _UsarZonaCobranza = LibConvert.SNToBool(value); }
        }


        public bool SugerirConsecutivoEnCobranzaAsBool {
            get { return _SugerirConsecutivoEnCobranza; }
            set { _SugerirConsecutivoEnCobranza = value; }
        }

        public string SugerirConsecutivoEnCobranza {
            set { _SugerirConsecutivoEnCobranza = LibConvert.SNToBool(value); }
        }


        public string ConceptoReversoCobranza {
            get { return _ConceptoReversoCobranza; }
            set { _ConceptoReversoCobranza = LibString.Mid(value, 0, 8); }
        }

        public bool ImprimirCombrobanteAlIngresarCobranzaAsBool {
            get { return _ImprimirCombrobanteAlIngresarCobranza; }
            set { _ImprimirCombrobanteAlIngresarCobranza = value; }
        }

        public string ImprimirCombrobanteAlIngresarCobranza {
            set { _ImprimirCombrobanteAlIngresarCobranza = LibConvert.SNToBool(value); }
        }


        public string NombrePlantillaCompobanteCobranza {
            get { return _NombrePlantillaCompobanteCobranza; }
            set { _NombrePlantillaCompobanteCobranza = LibString.Mid(value, 0, 50); }
        }

        public bool AsignarComisionDeVendedorEnCobranzaAsBool {
            get { return _AsignarComisionDeVendedorEnCobranza; }
            set { _AsignarComisionDeVendedorEnCobranza = value; }
        }

        public string AsignarComisionDeVendedorEnCobranza {
            set { _AsignarComisionDeVendedorEnCobranza = LibConvert.SNToBool(value); }
        }


        public bool CambiarCobradorVendedorAsBool {
            get { return _CambiarCobradorVendedor; }
            set { _CambiarCobradorVendedor = value; }
        }

        public string CambiarCobradorVendedor {
            set { _CambiarCobradorVendedor = LibConvert.SNToBool(value); }
        }


        public bool BloquearNumeroCobranzaAsBool {
            get { return _BloquearNumeroCobranza; }
            set { _BloquearNumeroCobranza = value; }
        }

        public string BloquearNumeroCobranza {
            set { _BloquearNumeroCobranza = LibConvert.SNToBool(value); }
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

        public CobranzasStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            UsarZonaCobranzaAsBool = false;
            SugerirConsecutivoEnCobranzaAsBool = false;
            ConceptoReversoCobranza = "";
            ImprimirCombrobanteAlIngresarCobranzaAsBool = false;
            NombrePlantillaCompobanteCobranza = "";
            AsignarComisionDeVendedorEnCobranzaAsBool = false;
            CambiarCobradorVendedorAsBool = false;
            BloquearNumeroCobranzaAsBool = false;
            fldTimeStamp = 0;
        }

        public CobranzasStt Clone() {
            CobranzasStt vResult = new CobranzasStt();
            vResult.UsarZonaCobranzaAsBool = _UsarZonaCobranza;
            vResult.SugerirConsecutivoEnCobranzaAsBool = _SugerirConsecutivoEnCobranza;
            vResult.ConceptoReversoCobranza = _ConceptoReversoCobranza;
            vResult.ImprimirCombrobanteAlIngresarCobranzaAsBool = _ImprimirCombrobanteAlIngresarCobranza;
            vResult.NombrePlantillaCompobanteCobranza = _NombrePlantillaCompobanteCobranza;
            vResult.AsignarComisionDeVendedorEnCobranzaAsBool = _AsignarComisionDeVendedorEnCobranza;
            vResult.CambiarCobradorVendedorAsBool = _CambiarCobradorVendedor;
            vResult.BloquearNumeroCobranzaAsBool = _BloquearNumeroCobranza;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Usar Zona Cobranza = " + _UsarZonaCobranza +
               "\nSugerir Consecutivo En Cobranza = " + _SugerirConsecutivoEnCobranza +
               "\nConcepto Reverso de Cobranza = " + _ConceptoReversoCobranza +
               "\nImprimir Combrobante Al Ingresar Cobranza = " + _ImprimirCombrobanteAlIngresarCobranza +
               "\nNombre Plantilla Compobante Cobranza = " + _NombrePlantillaCompobanteCobranza +
               "\nAsignar Comision De Vendedor En Cobranza = " + _AsignarComisionDeVendedorEnCobranza +
               "\nCambiar Cobrador Vendedor = " + _CambiarCobradorVendedor +
               "\nBloquear Numero Cobranza = " + _BloquearNumeroCobranza;
        }
        #endregion //Metodos Generados


    } //End of class CobranzasStt

} //End of namespace Galac.Saw.Ccl.SttDef

