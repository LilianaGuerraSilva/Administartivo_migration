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
    public class NotasDebitoCreditoEntregaStt : ISettDefinition {
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
        private string _NombrePlantillaNotaDeCredito;
        private string _PrimeraNotaDeCredito;
        private eTipoDePrefijoFactura _TipoDePrefijoNC;
        private string _PrefijoNC;
        private string _PrimeraBoleta;
        private string _NombrePlantillaBoleta;
        private string _NombrePlantillaNotaDeDebito;
        private bool _NDPreNumerada;
        private string _PrimeraNotaDeDebito;
        private eTipoDePrefijoFactura _TipoDePrefijoND;
        private string _PrefijoND;
        private bool _NCPreNumerada;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string NombrePlantillaNotaDeCredito {
            get { return _NombrePlantillaNotaDeCredito; }
            set { _NombrePlantillaNotaDeCredito = LibString.Mid(value, 0, 50); }
        }

        public string PrimeraNotaDeCredito {
            get { return _PrimeraNotaDeCredito; }
            set { _PrimeraNotaDeCredito = LibString.Mid(value, 0, 11); }
        }

        public eTipoDePrefijoFactura TipoDePrefijoNCAsEnum {
            get { return _TipoDePrefijoNC; }
            set { _TipoDePrefijoNC = value; }
        }

        public string TipoDePrefijoNC {
            set { _TipoDePrefijoNC = (eTipoDePrefijoFactura)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDePrefijoNCAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDePrefijoNC); }
        }

        public string TipoDePrefijoNCAsString {
            get { return LibEnumHelper.GetDescription(_TipoDePrefijoNC); }
        }

        public string PrefijoNC {
            get { return _PrefijoNC; }
            set { _PrefijoNC = LibString.Mid(value, 0, 5); }
        }

        public string PrimeraBoleta {
            get { return _PrimeraBoleta; }
            set { _PrimeraBoleta = LibString.Mid(value, 0, 11); }
        }

        public string NombrePlantillaBoleta {
            get { return _NombrePlantillaBoleta; }
            set { _NombrePlantillaBoleta = LibString.Mid(value, 0, 50); }
        }

        public string NombrePlantillaNotaDeDebito {
            get { return _NombrePlantillaNotaDeDebito; }
            set { _NombrePlantillaNotaDeDebito = LibString.Mid(value, 0, 50); }
        }

        public bool NDPreNumeradaAsBool {
            get { return _NDPreNumerada; }
            set { _NDPreNumerada = value; }
        }

        public string NDPreNumerada {
            set { _NDPreNumerada = LibConvert.SNToBool(value); }
        }


        public string PrimeraNotaDeDebito {
            get { return _PrimeraNotaDeDebito; }
            set { _PrimeraNotaDeDebito = LibString.Mid(value, 0, 11); }
        }

        public eTipoDePrefijoFactura TipoDePrefijoNDAsEnum {
            get { return _TipoDePrefijoND; }
            set { _TipoDePrefijoND = value; }
        }

        public string TipoDePrefijoND {
            set { _TipoDePrefijoND = (eTipoDePrefijoFactura)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDePrefijoNDAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDePrefijoND); }
        }

        public string TipoDePrefijoNDAsString {
            get { return LibEnumHelper.GetDescription(_TipoDePrefijoND); }
        }

        public string PrefijoND {
            get { return _PrefijoND; }
            set { _PrefijoND = LibString.Mid(value, 0, 5); }
        }

        public bool NCPreNumeradaAsBool {
            get { return _NCPreNumerada; }
            set { _NCPreNumerada = value; }
        }

        public string NCPreNumerada {
            set { _NCPreNumerada = LibConvert.SNToBool(value); }
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

        public NotasDebitoCreditoEntregaStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            NombrePlantillaNotaDeCredito = "";
            PrimeraNotaDeCredito = "";
            TipoDePrefijoNCAsEnum = eTipoDePrefijoFactura.SinPrefijo;
            PrefijoNC = "";
            PrimeraBoleta = "";
            NombrePlantillaBoleta = "";
            NombrePlantillaNotaDeDebito = "";
            NDPreNumeradaAsBool = false;
            PrimeraNotaDeDebito = "";
            TipoDePrefijoNDAsEnum = eTipoDePrefijoFactura.SinPrefijo;
            PrefijoND = "";
            NCPreNumeradaAsBool = false;
            fldTimeStamp = 0;
        }

        public NotasDebitoCreditoEntregaStt Clone() {
            NotasDebitoCreditoEntregaStt vResult = new NotasDebitoCreditoEntregaStt();
            vResult.NombrePlantillaNotaDeCredito = _NombrePlantillaNotaDeCredito;
            vResult.PrimeraNotaDeCredito = _PrimeraNotaDeCredito;
            vResult.TipoDePrefijoNCAsEnum = _TipoDePrefijoNC;
            vResult.PrefijoNC = _PrefijoNC;
            vResult.PrimeraBoleta = _PrimeraBoleta;
            vResult.NombrePlantillaBoleta = _NombrePlantillaBoleta;
            vResult.NombrePlantillaNotaDeDebito = _NombrePlantillaNotaDeDebito;
            vResult.NDPreNumeradaAsBool = _NDPreNumerada;
            vResult.PrimeraNotaDeDebito = _PrimeraNotaDeDebito;
            vResult.TipoDePrefijoNDAsEnum = _TipoDePrefijoND;
            vResult.PrefijoND = _PrefijoND;
            vResult.NCPreNumeradaAsBool = _NCPreNumerada;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Plantilla de impresión = " + _NombrePlantillaNotaDeCredito +
               "\nNúmero inicial .......... = " + _PrimeraNotaDeCredito +
               "\nTipo de prefijo .......... = " + _TipoDePrefijoNC.ToString() +
               "\nPrefijo = " + _PrefijoNC +
               "\nPrimera Boleta = " + _PrimeraBoleta +
               "\nNombre Plantilla Boleta = " + _NombrePlantillaBoleta +
               "\nNombre Plantilla Nota De Debito = " + _NombrePlantillaNotaDeDebito +
               "\nUsar Nota de Débito pre-numerada = " + _NDPreNumerada +
               "\nNúmero inicial .............. = " + _PrimeraNotaDeDebito +
               "\nTipoDePrefijoND = " + _TipoDePrefijoND.ToString() +
               "\nPrefijo = " + _PrefijoND +
               "\nUsar Nota de credito  pre-numerada = " + _NCPreNumerada;
        }
        #endregion //Metodos Generados


    } //End of class NotasDebitoCreditoEntregaStt

} //End of namespace Galac.Saw.Ccl.SttDef

