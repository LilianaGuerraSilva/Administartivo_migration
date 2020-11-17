using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Ccl.Banco {
    [Serializable]
    public class Beneficiario {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private string _Codigo;
        private string _NumeroRIF;
        private string _NombreBeneficiario;
        private eOrigenBeneficiario _Origen;
        private eTipoDeBeneficiario _TipoDeBeneficiario;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int Consecutivo {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }

        public string Codigo {
            get { return _Codigo; }
            set { _Codigo = LibString.Mid(value, 0, 10); }
        }

        public string NumeroRIF {
            get { return _NumeroRIF; }
            set { _NumeroRIF = LibString.Mid(value, 0, 20); }
        }

        public string NombreBeneficiario {
            get { return _NombreBeneficiario; }
            set { _NombreBeneficiario = LibString.Mid(value, 0, 80); }
        }

        public eOrigenBeneficiario OrigenAsEnum {
            get { return _Origen; }
            set { _Origen = value; }
        }

        public string Origen {
            set { _Origen = (eOrigenBeneficiario)LibConvert.DbValueToEnum(value); }
        }

        public string OrigenAsDB {
            get { return LibConvert.EnumToDbValue((int) _Origen); }
        }

        public string OrigenAsString {
            get { return LibEnumHelper.GetDescription(_Origen); }
        }

        public eTipoDeBeneficiario TipoDeBeneficiarioAsEnum {
            get { return _TipoDeBeneficiario; }
            set { _TipoDeBeneficiario = value; }
        }

        public string TipoDeBeneficiario {
            set { _TipoDeBeneficiario = (eTipoDeBeneficiario)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeBeneficiarioAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeBeneficiario); }
        }

        public string TipoDeBeneficiarioAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeBeneficiario); }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 20); }
        }

        public DateTime FechaUltimaModificacion {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = LibConvert.DateToDbValue(value); }
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

        public Beneficiario() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            Consecutivo = 0;
            Codigo = string.Empty;
            NumeroRIF = string.Empty;
            NombreBeneficiario = string.Empty;
            OrigenAsEnum = eOrigenBeneficiario.Administrativo;
            TipoDeBeneficiarioAsEnum = eTipoDeBeneficiario.PersonaJuridica;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public Beneficiario Clone() {
            Beneficiario vResult = new Beneficiario();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.Codigo = _Codigo;
            vResult.NumeroRIF = _NumeroRIF;
            vResult.NombreBeneficiario = _NombreBeneficiario;
            vResult.OrigenAsEnum = _Origen;
            vResult.TipoDeBeneficiarioAsEnum = _TipoDeBeneficiario;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nCódigo = " + _Codigo +
               "\nNúmero RIF = " + _NumeroRIF +
               "\nNombre Beneficiario = " + _NombreBeneficiario +
               "\nOrigen = " + _Origen.ToString() +
               "\nTipo de Beneficiario = " + _TipoDeBeneficiario.ToString() +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class Beneficiario

} //End of namespace Galac.Adm.Ccl.Banco

