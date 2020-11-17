using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Ccl.GestionCompras {
    [Serializable]
    public class TablaRetencion {
        #region Variables
        private eTipodePersonaRetencion _TipoDePersona;
        private string _Codigo;
        private string _CodigoSeniat;
        private string _TipoDePago;
        private string _Comentarios;
        private decimal _BaseImponible;
        private decimal _Tarifa;
        private decimal _ParaPagosMayoresDe;
        private DateTime _FechaAplicacion;
        private decimal _Sustraendo;
        private bool _AcumulaParaPJND;
        private string _SecuencialDePlantilla;
        private string _CodigoMoneda;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public eTipodePersonaRetencion TipoDePersonaAsEnum {
            get { return _TipoDePersona; }
            set { _TipoDePersona = value; }
        }

        public string TipoDePersona {
            set { _TipoDePersona = (eTipodePersonaRetencion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDePersonaAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDePersona); }
        }

        public string TipoDePersonaAsString {
            get { return LibEnumHelper.GetDescription(_TipoDePersona); }
        }

        public string Codigo {
            get { return _Codigo; }
            set { _Codigo = LibString.Mid(value, 0, 6); }
        }

        public string CodigoSeniat {
            get { return _CodigoSeniat; }
            set { _CodigoSeniat = LibString.Mid(value, 0, 3); }
        }

        public string TipoDePago {
            get { return _TipoDePago; }
            set { _TipoDePago = LibString.Mid(value, 0, 50); }
        }

        public string Comentarios {
            get { return _Comentarios; }
            set { _Comentarios = LibString.Mid(value, 0, 255); }
        }

        public decimal BaseImponible {
            get { return _BaseImponible; }
            set { _BaseImponible = value; }
        }

        public decimal Tarifa {
            get { return _Tarifa; }
            set { _Tarifa = value; }
        }

        public decimal ParaPagosMayoresDe {
            get { return _ParaPagosMayoresDe; }
            set { _ParaPagosMayoresDe = value; }
        }

        public DateTime FechaAplicacion {
            get { return _FechaAplicacion; }
            set { _FechaAplicacion = LibConvert.DateToDbValue(value); }
        }

        public decimal Sustraendo {
            get { return _Sustraendo; }
            set { _Sustraendo = value; }
        }

        public bool AcumulaParaPJNDAsBool {
            get { return _AcumulaParaPJND; }
            set { _AcumulaParaPJND = value; }
        }

        public string AcumulaParaPJND {
            set { _AcumulaParaPJND = LibConvert.SNToBool(value); }
        }


        public string SecuencialDePlantilla {
            get { return _SecuencialDePlantilla; }
            set { _SecuencialDePlantilla = LibString.Mid(value, 0, 5); }
        }

        public string CodigoMoneda {
            get { return _CodigoMoneda; }
            set { _CodigoMoneda = LibString.Mid(value, 0, 4); }
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

        public TablaRetencion() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            TipoDePersonaAsEnum = eTipodePersonaRetencion.PJ_Domiciliada;
            Codigo = "";
            CodigoSeniat = "";
            TipoDePago = "";
            Comentarios = "";
            BaseImponible = 0;
            Tarifa = 0;
            ParaPagosMayoresDe = 0;
            FechaAplicacion = LibDate.Today();
            Sustraendo = 0;
            AcumulaParaPJNDAsBool = false;
            SecuencialDePlantilla = "";
            CodigoMoneda = "";
            fldTimeStamp = 0;
        }

        public TablaRetencion Clone() {
            TablaRetencion vResult = new TablaRetencion();
            vResult.TipoDePersonaAsEnum = _TipoDePersona;
            vResult.Codigo = _Codigo;
            vResult.CodigoSeniat = _CodigoSeniat;
            vResult.TipoDePago = _TipoDePago;
            vResult.Comentarios = _Comentarios;
            vResult.BaseImponible = _BaseImponible;
            vResult.Tarifa = _Tarifa;
            vResult.ParaPagosMayoresDe = _ParaPagosMayoresDe;
            vResult.FechaAplicacion = _FechaAplicacion;
            vResult.Sustraendo = _Sustraendo;
            vResult.AcumulaParaPJNDAsBool = _AcumulaParaPJND;
            vResult.SecuencialDePlantilla = _SecuencialDePlantilla;
            vResult.CodigoMoneda = _CodigoMoneda;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Tipo De Persona = " + _TipoDePersona.ToString() +
               "\nCódigo de Retención = " + _Codigo +
               "\nCodigo Seniat = " + _CodigoSeniat +
               "\nDescripción = " + _TipoDePago +
               "\nComentarios = " + _Comentarios +
               "\n% Base Imponible = " + _BaseImponible.ToString() +
               "\nPorcentaje de retención = " + _Tarifa.ToString() +
               "\nPara pagos mayores de = " + _ParaPagosMayoresDe.ToString() +
               "\nFecha Aplicacion = " + _FechaAplicacion.ToShortDateString() +
               "\nSustraendo = " + _Sustraendo.ToString() +
               "\nAcumula Para PJND = " + _AcumulaParaPJND +
               "\nSecuencial De Plantilla = " + _SecuencialDePlantilla +
               "\nCódigo = " + _CodigoMoneda;
        }
        #endregion //Metodos Generados


    } //End of class TablaRetencion

} //End of namespace Galac.Adm.Ccl.GestionCompras

