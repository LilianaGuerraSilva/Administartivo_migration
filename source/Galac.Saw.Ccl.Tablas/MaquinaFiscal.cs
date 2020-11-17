using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Saw.Ccl.Tablas {
    [Serializable]
    public class MaquinaFiscal {
        #region Variables
        private int _ConsecutivoCompania;
        private string _ConsecutivoMaquinaFiscal;
        private string _Descripcion;
        private string _NumeroRegistro;
        private eStatusMaquinaFiscal _Status;
        private int _LongitudNumeroFiscal;
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

        public string ConsecutivoMaquinaFiscal {
            get { return _ConsecutivoMaquinaFiscal; }
            set { _ConsecutivoMaquinaFiscal = LibString.Mid(value, 0, 9); }
        }

        public string Descripcion {
            get { return _Descripcion; }
            set { _Descripcion = LibString.Mid(value, 0, 35); }
        }

        public string NumeroRegistro {
            get { return _NumeroRegistro; }
            set { _NumeroRegistro = LibString.Mid(value, 0, 20); }
        }

        public eStatusMaquinaFiscal StatusAsEnum {
            get { return _Status; }
            set { _Status = value; }
        }

        public string Status {
            set { _Status = (eStatusMaquinaFiscal)LibConvert.DbValueToEnum(value); }
        }

        public string StatusAsDB {
            get { return LibConvert.EnumToDbValue((int) _Status); }
        }

        public string StatusAsString {
            get { return LibEnumHelper.GetDescription(_Status); }
        }

        public int LongitudNumeroFiscal {
            get { return _LongitudNumeroFiscal; }
            set { _LongitudNumeroFiscal = value; }
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

        public MaquinaFiscal() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            ConsecutivoMaquinaFiscal = string.Empty;
            Descripcion = string.Empty;
            NumeroRegistro = string.Empty;
            StatusAsEnum = eStatusMaquinaFiscal.Activa;
            LongitudNumeroFiscal = 0;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public MaquinaFiscal Clone() {
            MaquinaFiscal vResult = new MaquinaFiscal();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoMaquinaFiscal = _ConsecutivoMaquinaFiscal;
            vResult.Descripcion = _Descripcion;
            vResult.NumeroRegistro = _NumeroRegistro;
            vResult.StatusAsEnum = _Status;
            vResult.LongitudNumeroFiscal = _LongitudNumeroFiscal;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo = " + _ConsecutivoMaquinaFiscal +
               "\nDescripción = " + _Descripcion +
               "\nNro.Registro = " + _NumeroRegistro +
               "\nStatus = " + _Status.ToString() +
               "\nLongitud del Numero Fiscal = " + _LongitudNumeroFiscal.ToString() +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class MaquinaFiscal

} //End of namespace Galac.Saw.Ccl.Tablas

