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
    public class BancosStt : ISettDefinition {
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
        private bool _UsaCodigoBancoEnPantalla;
        private string _CodigoGenericoCuentaBancaria;
        private bool _ManejaDebitoBancario;
        private bool _RedondeaMontoDebitoBancario;
        private string _ConceptoDebitoBancario;
        private bool _ConsideraConciliadosLosMovIngresadosAntesDeFecha;
        private DateTime _FechaDeInicioConciliacion;
        private bool _ManejaCreditoBancario;
        private bool _RedondeaMontoCreditoBancario;
        private string _ConceptoCreditoBancario;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public bool UsaCodigoBancoEnPantallaAsBool {
            get { return _UsaCodigoBancoEnPantalla; }
            set { _UsaCodigoBancoEnPantalla = value; }
        }

        public string UsaCodigoBancoEnPantalla {
            set { _UsaCodigoBancoEnPantalla = LibConvert.SNToBool(value); }
        }


        public string CodigoGenericoCuentaBancaria {
            get { return _CodigoGenericoCuentaBancaria; }
            set { _CodigoGenericoCuentaBancaria = LibString.Mid(value, 0, 5); }
        }

        public bool ManejaDebitoBancarioAsBool {
            get { return _ManejaDebitoBancario; }
            set { _ManejaDebitoBancario = value; }
        }

        public string ManejaDebitoBancario {
            set { _ManejaDebitoBancario = LibConvert.SNToBool(value); }
        }


        public bool RedondeaMontoDebitoBancarioAsBool {
            get { return _RedondeaMontoDebitoBancario; }
            set { _RedondeaMontoDebitoBancario = value; }
        }

        public string RedondeaMontoDebitoBancario {
            set { _RedondeaMontoDebitoBancario = LibConvert.SNToBool(value); }
        }


        public string ConceptoDebitoBancario {
            get { return _ConceptoDebitoBancario; }
            set { _ConceptoDebitoBancario = LibString.Mid(value, 0, 8); }
        }

        public bool ConsideraConciliadosLosMovIngresadosAntesDeFechaAsBool {
            get { return _ConsideraConciliadosLosMovIngresadosAntesDeFecha; }
            set { _ConsideraConciliadosLosMovIngresadosAntesDeFecha = value; }
        }

        public string ConsideraConciliadosLosMovIngresadosAntesDeFecha {
            set { _ConsideraConciliadosLosMovIngresadosAntesDeFecha = LibConvert.SNToBool(value); }
        }


        public DateTime FechaDeInicioConciliacion {
            get { return _FechaDeInicioConciliacion; }
            set { _FechaDeInicioConciliacion = LibConvert.DateToDbValue(value); }
        }

        public bool ManejaCreditoBancarioAsBool {
            get { return _ManejaCreditoBancario; }
            set { _ManejaCreditoBancario = value; }
        }

        public string ManejaCreditoBancario {
            set { _ManejaCreditoBancario = LibConvert.SNToBool(value); }
        }


        public bool RedondeaMontoCreditoBancarioAsBool {
            get { return _RedondeaMontoCreditoBancario; }
            set { _RedondeaMontoCreditoBancario = value; }
        }

        public string RedondeaMontoCreditoBancario {
            set { _RedondeaMontoCreditoBancario = LibConvert.SNToBool(value); }
        }


        public string ConceptoCreditoBancario {
            get { return _ConceptoCreditoBancario; }
            set { _ConceptoCreditoBancario = LibString.Mid(value, 0, 8); }
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

        public BancosStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            UsaCodigoBancoEnPantallaAsBool = false;
            CodigoGenericoCuentaBancaria = "";
            ManejaDebitoBancarioAsBool = false;
            RedondeaMontoDebitoBancarioAsBool = false;
            ConceptoDebitoBancario = "";
            ConsideraConciliadosLosMovIngresadosAntesDeFechaAsBool = false;
            FechaDeInicioConciliacion = LibDate.Today();
            ManejaCreditoBancarioAsBool = false;
            RedondeaMontoCreditoBancarioAsBool = false;
            ConceptoCreditoBancario = "";
            fldTimeStamp = 0;
        }

        public BancosStt Clone() {
            BancosStt vResult = new BancosStt();
            vResult.UsaCodigoBancoEnPantallaAsBool = _UsaCodigoBancoEnPantalla;
            vResult.CodigoGenericoCuentaBancaria = _CodigoGenericoCuentaBancaria;
            vResult.ManejaDebitoBancarioAsBool = _ManejaDebitoBancario;
            vResult.RedondeaMontoDebitoBancarioAsBool = _RedondeaMontoDebitoBancario;
            vResult.ConceptoDebitoBancario = _ConceptoDebitoBancario;
            vResult.ConsideraConciliadosLosMovIngresadosAntesDeFechaAsBool = _ConsideraConciliadosLosMovIngresadosAntesDeFecha;
            vResult.FechaDeInicioConciliacion = _FechaDeInicioConciliacion;
            vResult.ManejaCreditoBancarioAsBool = _ManejaCreditoBancario;
            vResult.RedondeaMontoCreditoBancarioAsBool = _RedondeaMontoCreditoBancario;
            vResult.ConceptoCreditoBancario = _ConceptoCreditoBancario;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Usa Codigo Banco En Pantalla = " + _UsaCodigoBancoEnPantalla +
               "\nCodigo Generico Cuenta Bancaria  = " + _CodigoGenericoCuentaBancaria +
               "\nManeja Debito Bancario = " + _ManejaDebitoBancario +
               "\nRedondea Monto Debito Bancario = " + _RedondeaMontoDebitoBancario +
               "\nConcepto Débito Bancario = " + _ConceptoDebitoBancario +
               "\nConsidera Conciliados Los Mov Ingresados Antes De Fecha = " + _ConsideraConciliadosLosMovIngresadosAntesDeFecha +
               "\nFecha De Inicio Conciliacion = " + _FechaDeInicioConciliacion.ToShortDateString() +
               "\nManeja Credito Bancario = " + _ManejaCreditoBancario +
               "\nRedondea Monto Credito Bancario = " + _RedondeaMontoCreditoBancario +
               "\nConcepto Crédito Bancario = " + _ConceptoCreditoBancario;
        }
        #endregion //Metodos Generados


    } //End of class BancosStt

} //End of namespace Galac.Saw.Ccl.SttDef

