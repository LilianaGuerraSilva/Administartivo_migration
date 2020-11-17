using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Saw.Ccl.Vehiculo {
    [Serializable]
    public class Vehiculo {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private string _Placa;
        private string _serialVIN;
        private string _NombreModelo;
        private string _Marca;
        private int _Ano;
        private string _CodigoColor;
        private string _DescripcionColor;
        private string _CodigoCliente;
        private string _NombreCliente;
        private string _RIFCliente;
        private string _NumeroPoliza;
        private string _SerialMotor;
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

        public string Marca {
            get { return _Marca; }
            set { _Marca = LibString.Mid(value, 0, 20); }
        }

        public int Ano {
            get { return _Ano; }
            set { _Ano = value; }
        }

        public string CodigoColor {
            get { return _CodigoColor; }
            set { _CodigoColor = LibString.Mid(value, 0, 3); }
        }

        public string DescripcionColor {
            get { return _DescripcionColor; }
            set { _DescripcionColor = LibString.Mid(value, 0, 20); }
        }

        public string CodigoCliente {
            get { return _CodigoCliente; }
            set { _CodigoCliente = LibString.Mid(value, 0, 10); }
        }

        public string NombreCliente {
            get { return _NombreCliente; }
            set { _NombreCliente = LibString.Mid(value, 0, 80); }
        }

        public string RIFCliente {
            get { return _RIFCliente; }
            set { _RIFCliente = LibString.Mid(value, 0, 80); }
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

        public Vehiculo() {
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
            Placa = string.Empty;
            serialVIN = string.Empty;
            NombreModelo = string.Empty;
            Marca = string.Empty;
            Ano = 0;
            CodigoColor = string.Empty;
            DescripcionColor = string.Empty;
            CodigoCliente = string.Empty;
            NombreCliente = string.Empty;
            RIFCliente = string.Empty;
            NumeroPoliza = string.Empty;
            SerialMotor = string.Empty;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public Vehiculo Clone() {
            Vehiculo vResult = new Vehiculo();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.Placa = _Placa;
            vResult.serialVIN = _serialVIN;
            vResult.NombreModelo = _NombreModelo;
            vResult.Marca = _Marca;
            vResult.Ano = _Ano;
            vResult.CodigoColor = _CodigoColor;
            vResult.DescripcionColor = _DescripcionColor;
            vResult.CodigoCliente = _CodigoCliente;
            vResult.NombreCliente = _NombreCliente;
            vResult.RIFCliente = _RIFCliente;
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
               "\nMarca = " + _Marca +
               "\nAño del Vehículo = " + _Ano.ToString() +
               "\nColor = " + _CodigoColor +
               "\nDescripción = " + _DescripcionColor +
               "\nCódigo Cliente = " + _CodigoCliente +
               "\nNúmero de Póliza = " + _NumeroPoliza +
               "\nSerial del Motor = " + _SerialMotor +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class Vehiculo

} //End of namespace Galac.Saw.Ccl.Vehiculo

