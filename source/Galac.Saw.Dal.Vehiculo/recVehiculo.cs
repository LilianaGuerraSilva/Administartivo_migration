using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Services;
using System.Xml;
using LibGalac.Aos.Base;

namespace Galac.Saw.Dal.Vehiculo {
    [Serializable]
    public class recVehiculo {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private string _Placa;
        private string _serialVIN;
        private string _NombreModelo;
        private int _Ano;
        private string _CodigoColor;
        private string _CodigoCliente;
        private string _NumeroPoliza;
        private string _SerialMotor;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string MessageName {
            get { return "Vehiculo"; }
        }

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int Consecutivo {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }

        public string Placa {
            get { return _Placa; }
            set { _Placa = LibString.Mid(value, 0, 20); }
        }

        public string serialVIN {
            get { return _serialVIN; }
            set { _serialVIN = LibString.Mid(value, 0, 40); }
        }

        public string NombreModelo {
            get { return _NombreModelo; }
            set { _NombreModelo = LibString.Mid(value, 0, 20); }
        }

        public int Ano {
            get { return _Ano; }
            set { _Ano = value; }
        }

        public string CodigoColor {
            get { return _CodigoColor; }
            set { _CodigoColor = LibString.Mid(value, 0, 3); }
        }

        public string CodigoCliente {
            get { return _CodigoCliente; }
            set { _CodigoCliente = LibString.Mid(value, 0, 10); }
        }

        public string NumeroPoliza {
            get { return _NumeroPoliza; }
            set { _NumeroPoliza = LibString.Mid(value, 0, 20); }
        }

        public string SerialMotor {
            get { return _SerialMotor; }
            set { _SerialMotor = LibString.Mid(value, 0, 40); }
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
        public recVehiculo(){
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public recVehiculo Clone() {
            recVehiculo vResult = new recVehiculo();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.Placa = _Placa;
            vResult.serialVIN = _serialVIN;
            vResult.NombreModelo = _NombreModelo;
            vResult.Ano = _Ano;
            vResult.CodigoColor = _CodigoColor;
            vResult.CodigoCliente = _CodigoCliente;
            vResult.NumeroPoliza = _NumeroPoliza;
            vResult.SerialMotor = _SerialMotor;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nPlaca = " + _Placa +
               "\nSerial VIN = " + _serialVIN +
               "\nModelo Vehículo = " + _NombreModelo +
               "\nAño del Vehículo = " + _Ano.ToString() +
               "\nColor = " + _CodigoColor +
               "\nCódigo Cliente = " + _CodigoCliente +
               "\nNúmero de Póliza = " + _NumeroPoliza +
               "\nSerial del Motor = " + _SerialMotor +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            Consecutivo = 0;
            Placa = "";
            serialVIN = "";
            NombreModelo = "";
            Ano = 0;
            CodigoColor = "";
            CodigoCliente = "";
            NumeroPoliza = "";
            SerialMotor = "";
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
                Consecutivo = insParser.GetInt(0, "Consecutivo", Consecutivo);
                Placa = insParser.GetString(0, "Placa", Placa);
                serialVIN = insParser.GetString(0, "serialVIN", serialVIN);
                NombreModelo = insParser.GetString(0, "NombreModelo", NombreModelo);
                Ano = insParser.GetInt(0, "Ano", Ano);
                CodigoColor = insParser.GetString(0, "CodigoColor", CodigoColor);
                CodigoCliente = insParser.GetString(0, "CodigoCliente", CodigoCliente);
                NumeroPoliza = insParser.GetString(0, "NumeroPoliza", NumeroPoliza);
                SerialMotor = insParser.GetString(0, "SerialMotor", SerialMotor);
                NombreOperador = insParser.GetString(0, "NombreOperador", NombreOperador);
                FechaUltimaModificacion = insParser.GetDateTime(0, "FechaUltimaModificacion", FechaUltimaModificacion);
                fldTimeStamp = insParser.GetTimeStamp(0);
            }
        }
        #endregion //Metodos Generados


    } //End of class recVehiculo

} //End of namespace Galac.Saw.Dal.Vehiculo

