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
    public class ModeloDeFacturaStt : ISettDefinition {
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
        private eModeloDeFactura _ModeloDeFactura;
        private string _NombrePlantillaFactura;
        private bool _FacturaPreNumerada;
        private string _PrimeraFactura;
        private eTipoDePrefijo _TipoDePrefijo;
        private string _Prefijo;
        private string _ModeloFacturaModoTexto;
        private eModeloDeFactura _ModeloDeFactura2;
        private string _NombrePlantillaFactura2;
        private bool _FacturaPreNumerada2;
        private string _PrimeraFactura2;
        private eTipoDePrefijo _TipoDePrefijo2;
        private string _Prefijo2;
        private string _ModeloFacturaModoTexto2;
        private bool _UsarDosTalonarios;
        private string _NombrePlantillaSubFacturaConOtrosCargos;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public eModeloDeFactura ModeloDeFacturaAsEnum {
            get { return _ModeloDeFactura; }
            set { _ModeloDeFactura = value; }
        }

        public string ModeloDeFactura {
            set { _ModeloDeFactura = (eModeloDeFactura)LibConvert.DbValueToEnum(value); }
        }

        public string ModeloDeFacturaAsDB {
            get { return LibConvert.EnumToDbValue((int) _ModeloDeFactura); }
        }

        public string ModeloDeFacturaAsString {
            get { return LibEnumHelper.GetDescription(_ModeloDeFactura); }
        }

        public string NombrePlantillaFactura {
            get { return _NombrePlantillaFactura; }
            set { _NombrePlantillaFactura = LibString.Mid(value, 0, 50); }
        }

        public string PrimeraFactura {
            get { return _PrimeraFactura; }
            set { _PrimeraFactura = LibString.Mid(value, 0, 11); }
        }

        public eTipoDePrefijo TipoDePrefijoAsEnum {
            get { return _TipoDePrefijo; }
            set { _TipoDePrefijo = value; }
        }

        public string TipoDePrefijo {
            set { _TipoDePrefijo = (eTipoDePrefijo)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDePrefijoAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDePrefijo); }
        }

        public string TipoDePrefijoAsString {
            get { return LibEnumHelper.GetDescription(_TipoDePrefijo); }
        }

        public string Prefijo {
            get { return _Prefijo; }
            set { _Prefijo = LibString.Mid(value, 0, 5); }
        }

        public string ModeloFacturaModoTexto {
            get { return _ModeloFacturaModoTexto; }
            set { _ModeloFacturaModoTexto = LibString.Mid(value, 0, 50); }
        }

        public bool FacturaPreNumeradaAsBool {
            get { return _FacturaPreNumerada; }
            set { _FacturaPreNumerada = value; }
        }

        public string FacturaPreNumerada {
            set { _FacturaPreNumerada = LibConvert.SNToBool(value); }
        }

        public eModeloDeFactura ModeloDeFactura2AsEnum {
            get { return _ModeloDeFactura2; }
            set { _ModeloDeFactura2 = value; }
        }

        public string ModeloDeFactura2 {
            set { _ModeloDeFactura2 = (eModeloDeFactura)LibConvert.DbValueToEnum(value); }
        }

        public string ModeloDeFactura2AsDB {
            get { return LibConvert.EnumToDbValue((int)_ModeloDeFactura2); }
        }

        public string ModeloDeFactura2AsString {
            get { return LibEnumHelper.GetDescription(_ModeloDeFactura2); }
        }

        public string NombrePlantillaFactura2 {
            get { return _NombrePlantillaFactura2; }
            set { _NombrePlantillaFactura2 = LibString.Mid(value, 0, 50); }
        }

        public bool FacturaPreNumerada2AsBool {
            get { return _FacturaPreNumerada2; }
            set { _FacturaPreNumerada2 = value; }
        }

        public string FacturaPreNumerada2 {
            set { _FacturaPreNumerada2 = LibConvert.SNToBool(value); }
        }


        public string PrimeraFactura2 {
            get { return _PrimeraFactura2; }
            set { _PrimeraFactura2 = LibString.Mid(value, 0, 11); }
        }

        public eTipoDePrefijo TipoDePrefijo2AsEnum {
            get { return _TipoDePrefijo2; }
            set { _TipoDePrefijo2 = value; }
        }

        public string TipoDePrefijo2 {
            set { _TipoDePrefijo2 = (eTipoDePrefijo)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDePrefijo2AsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoDePrefijo2); }
        }

        public string TipoDePrefijo2AsString {
            get { return LibEnumHelper.GetDescription(_TipoDePrefijo2); }
        }

        public string Prefijo2 {
            get { return _Prefijo2; }
            set { _Prefijo2 = LibString.Mid(value, 0, 5); }
        }

        public string ModeloFacturaModoTexto2 {
            get { return _ModeloFacturaModoTexto2; }
            set { _ModeloFacturaModoTexto2 = LibString.Mid(value, 0, 50); }
        }

        public bool UsarDosTalonariosAsBool {
            get { return _UsarDosTalonarios; }
            set { _UsarDosTalonarios = value; }
        }

        public string UsarDosTalonarios {
            set { _UsarDosTalonarios = LibConvert.SNToBool(value); }
        }

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        public string NombrePlantillaSubFacturaConOtrosCargos{
            get { return _NombrePlantillaSubFacturaConOtrosCargos; }
            set { _NombrePlantillaSubFacturaConOtrosCargos = LibString.Mid(value, 0, 50); }
        }
        #endregion //Propiedades
        #region Constructores

        public ModeloDeFacturaStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ModeloDeFacturaAsEnum = eModeloDeFactura.eMD_FORMALIBRE;
            NombrePlantillaFactura = "";
            PrimeraFactura = "";
            TipoDePrefijoAsEnum = eTipoDePrefijo.SinPrefijo;
            Prefijo = "";
            ModeloFacturaModoTexto = "";
            FacturaPreNumeradaAsBool = false;
            ModeloDeFactura2AsEnum = eModeloDeFactura.eMD_FORMALIBRE;
            NombrePlantillaFactura2 = "";
            FacturaPreNumerada2AsBool = false;
            PrimeraFactura2 = "";
            TipoDePrefijo2AsEnum = eTipoDePrefijo.SinPrefijo;
            Prefijo2 = "";
            ModeloFacturaModoTexto2 = "";
            UsarDosTalonariosAsBool = false;
            fldTimeStamp = 0;
            NombrePlantillaSubFacturaConOtrosCargos = "";
        }

        public ModeloDeFacturaStt Clone() {
            ModeloDeFacturaStt vResult = new ModeloDeFacturaStt();
            vResult.ModeloDeFacturaAsEnum = _ModeloDeFactura;
            vResult.NombrePlantillaFactura = _NombrePlantillaFactura;
            vResult.PrimeraFactura = _PrimeraFactura;
            vResult.TipoDePrefijoAsEnum = _TipoDePrefijo;
            vResult.Prefijo = _Prefijo;
            vResult.ModeloFacturaModoTexto = _ModeloFacturaModoTexto;
            vResult.FacturaPreNumeradaAsBool = _FacturaPreNumerada;
            vResult.fldTimeStamp = _fldTimeStamp;
            vResult.ModeloDeFactura2AsEnum = _ModeloDeFactura2;
            vResult.NombrePlantillaFactura2 = _NombrePlantillaFactura2;
            vResult.FacturaPreNumerada2AsBool = _FacturaPreNumerada2;
            vResult.PrimeraFactura2 = _PrimeraFactura2;
            vResult.TipoDePrefijo2AsEnum = _TipoDePrefijo2;
            vResult.Prefijo2 = _Prefijo2;
            vResult.ModeloFacturaModoTexto2 = _ModeloFacturaModoTexto2;
            vResult.UsarDosTalonariosAsBool = _UsarDosTalonarios;
            vResult.NombrePlantillaSubFacturaConOtrosCargos = _NombrePlantillaSubFacturaConOtrosCargos;
            return vResult;
        }

        public override string ToString() {
            return "Modelo De Factura = " + _ModeloDeFactura.ToString() +
               "\nNombre Plantilla Factura = " + _NombrePlantillaFactura +
               "\nPrimera Factura = " + _PrimeraFactura +
               "\nTipo De Prefijo = " + _TipoDePrefijo.ToString() +
               "\nPrefijo = " + _Prefijo +
               "\nModelo Factura Modo Texto = " + _ModeloFacturaModoTexto +
               "\nFactura Pre Numerada = " + _FacturaPreNumerada +
               "\nModelo De Factura 2 = " + _ModeloDeFactura2.ToString() +
               "\nNombre Plantilla Factura 2 = " + _NombrePlantillaFactura2 +
               "\nFactura Pre Numerada 2 = " + _FacturaPreNumerada2 +
               "\nPrimera Factura 2 = " + _PrimeraFactura2 +
               "\nTipo De Prefijo 2 = " + _TipoDePrefijo2.ToString() +
               "\nPrefijo 2 = " + _Prefijo2 +
               "\nModelo Factura Modo Texto 2 = " + _ModeloFacturaModoTexto2 +
               "\nUsar Dos Talonarios = " + _UsarDosTalonarios +
               "\nNombre Plantilla Sub Factura Con Otros Cargos = " + _NombrePlantillaSubFacturaConOtrosCargos;

 
        }
        #endregion //Metodos Generados


    } //End of class ModeloDeFacturaStt

} //End of namespace Galac.Saw.Ccl.SttDef

