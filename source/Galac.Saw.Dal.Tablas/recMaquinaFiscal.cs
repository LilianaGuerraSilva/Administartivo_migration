using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Services;
using System.Xml;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Dal.Tablas {
    [Serializable]
    public class recMaquinaFiscal {
        #region Variables
        private int _ConsecutivoCompania;
        private string _ConsecutivoMaquinaFiscal;
        private string _Descripcion;
        private string _NumeroRegistro;
        private eStatusMaquinaFiscal _Status;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string MessageName {
            get { return "Maquina Fiscal"; }
        }

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

        public eStatusMaquinaFiscal Status {
            get { return _Status; }
            set { _Status = value; }
        }
        public string StatusAsDB {
            get { return LibConvert.EnumToDbValue((int) _Status); }
        }
        public string StatusAsString {
            get { return LibEnumHelper.GetDescription(_Status); }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 10); }
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
        public recMaquinaFiscal(){
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public recMaquinaFiscal Clone() {
            recMaquinaFiscal vResult = new recMaquinaFiscal();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoMaquinaFiscal = _ConsecutivoMaquinaFiscal;
            vResult.Descripcion = _Descripcion;
            vResult.NumeroRegistro = _NumeroRegistro;
            vResult.Status = _Status;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo = " + _ConsecutivoMaquinaFiscal +
               "\nDescripcion = " + _Descripcion +
               "\nNro.Registro = " + _NumeroRegistro +
               "\nStatus = " + _Status.ToString() +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            ConsecutivoMaquinaFiscal = "";
            Descripcion = "";
            NumeroRegistro = "";
            Status = eStatusMaquinaFiscal.Activa;
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public void Fill(XmlDocument refResulset, bool valSetCurrent) {
            _datos = refResulset;
            LibXmlDataParse insParser = new LibXmlDataParse(refResulset);
            if (valSetCurrent && insParser.Count() > 0) {
                Clear();
                ConsecutivoCompania = insParser.GetInt(0, "ConsecutivoCompania", ConsecutivoCompania);
                ConsecutivoMaquinaFiscal = insParser.GetString(0, "ConsecutivoMaquinaFiscal", ConsecutivoMaquinaFiscal);
                Descripcion = insParser.GetString(0, "Descripcion", Descripcion);
                NumeroRegistro = insParser.GetString(0, "NumeroRegistro", NumeroRegistro);
                Status = (eStatusMaquinaFiscal) insParser.GetEnum(0, "Status", (int) Status);
                NombreOperador = insParser.GetString(0, "NombreOperador", NombreOperador);
                FechaUltimaModificacion = insParser.GetDateTime(0, "FechaUltimaModificacion", FechaUltimaModificacion);
                fldTimeStamp = insParser.GetTimeStamp(0);
            }
        }
        #endregion //Metodos Generados


    } //End of class recMaquinaFiscal

} //End of namespace Galac.Saw.Dal.Tablas

