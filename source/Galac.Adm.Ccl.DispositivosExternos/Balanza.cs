using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;


namespace Galac.Adm.Ccl.DispositivosExternos {
    [Serializable]
    public class Balanza {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private eModeloDeBalanza _Modelo;
        private string _Nombre;
        private ePuerto _Puerto;
        private eBitsDeDatos _BitsDeDatos;
        private eParidad _Paridad;
        private eBitsDeParada _BitDeParada;
        private eBaudRate _BaudRate;
        private eControlDeFlujo _ControlDeFlujo;
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

        public eModeloDeBalanza ModeloAsEnum {
            get { return _Modelo; }
            set { _Modelo = value; }
        }

        public string Modelo {
            set { _Modelo = (eModeloDeBalanza)LibConvert.DbValueToEnum(value); }
        }

        public string ModeloAsDB {
            get { return LibConvert.EnumToDbValue((int) _Modelo); }
        }

        public string ModeloAsString {
            get { return LibEnumHelper.GetDescription(_Modelo); }
        }

        public string Nombre {
            get { return _Nombre; }
            set { _Nombre = LibString.Mid(value, 0, 40); }
        }

        public ePuerto PuertoAsEnum {
            get { return _Puerto; }
            set { _Puerto = value; }
        }

        public string Puerto {
            set { _Puerto = (ePuerto)LibConvert.DbValueToEnum(value); }
        }

        public string PuertoAsDB {
            get { return LibConvert.EnumToDbValue((int) _Puerto); }
        }

        public string PuertoAsString {
            get { return LibEnumHelper.GetDescription(_Puerto); }
        }

        public eBitsDeDatos BitsDeDatosAsEnum {
            get { return _BitsDeDatos; }
            set { _BitsDeDatos = value; }
        }

        public string BitsDeDatos {
            set { _BitsDeDatos = (eBitsDeDatos)LibConvert.DbValueToEnum(value); }
        }

        public string BitsDeDatosAsDB {
            get { return LibConvert.EnumToDbValue((int) _BitsDeDatos); }
        }

        public string BitsDeDatosAsString {
            get { return LibEnumHelper.GetDescription(_BitsDeDatos); }
        }

        public eParidad ParidadAsEnum {
            get { return _Paridad; }
            set { _Paridad = value; }
        }

        public string Paridad {
            set { _Paridad = (eParidad)LibConvert.DbValueToEnum(value); }
        }

        public string ParidadAsDB {
            get { return LibConvert.EnumToDbValue((int) _Paridad); }
        }

        public string ParidadAsString {
            get { return LibEnumHelper.GetDescription(_Paridad); }
        }

        public eBitsDeParada BitDeParadaAsEnum {
            get { return _BitDeParada; }
            set { _BitDeParada = value; }
        }

        public string BitDeParada {
            set { _BitDeParada = (eBitsDeParada)LibConvert.DbValueToEnum(value); }
        }

        public string BitDeParadaAsDB {
            get { return LibConvert.EnumToDbValue((int) _BitDeParada); }
        }

        public string BitDeParadaAsString {
            get { return LibEnumHelper.GetDescription(_BitDeParada); }
        }

        public eBaudRate BaudRateAsEnum {
            get { return _BaudRate; }
            set { _BaudRate = value; }
        }

        public string BaudRate {
            set { _BaudRate = (eBaudRate)LibConvert.DbValueToEnum(value); }
        }

        public string BaudRateAsDB {
            get { return LibConvert.EnumToDbValue((int) _BaudRate); }
        }

        public string BaudRateAsString {
            get { return LibEnumHelper.GetDescription(_BaudRate); }
        }

        public eControlDeFlujo ControlDeFlujoAsEnum {
            get { return _ControlDeFlujo; }
            set { _ControlDeFlujo = value; }
        }

        public string ControlDeFlujo {
            set { _ControlDeFlujo = (eControlDeFlujo)LibConvert.DbValueToEnum(value); }
        }

        public string ControlDeFlujoAsDB {
            get { return LibConvert.EnumToDbValue((int) _ControlDeFlujo); }
        }

        public string ControlDeFlujoAsString {
            get { return LibEnumHelper.GetDescription(_ControlDeFlujo); }
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

        public Balanza() {
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
            ModeloAsEnum = eModeloDeBalanza.Xacta;
            Nombre = string.Empty;
            PuertoAsEnum = ePuerto.COM1;
            BitsDeDatosAsEnum = eBitsDeDatos.d8;
            ParidadAsEnum = eParidad.Ninguna;
            BitDeParadaAsEnum = eBitsDeParada.Uno;
            BaudRateAsEnum = eBaudRate.b9600;
            ControlDeFlujoAsEnum = eControlDeFlujo.Ninguno;
            fldTimeStamp = 0;
        }

        public Balanza Clone() {
            Balanza vResult = new Balanza();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.ModeloAsEnum = _Modelo;
            vResult.Nombre = _Nombre;
            vResult.PuertoAsEnum = _Puerto;
            vResult.BitsDeDatosAsEnum = _BitsDeDatos;
            vResult.ParidadAsEnum = _Paridad;
            vResult.BitDeParadaAsEnum = _BitDeParada;
            vResult.BaudRateAsEnum = _BaudRate;
            vResult.ControlDeFlujoAsEnum = _ControlDeFlujo;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nModelo = " + _Modelo.ToString() +
               "\nNombre = " + _Nombre +
               "\nPuerto = " + _Puerto.ToString() +
               "\nBits Datos = " + _BitsDeDatos.ToString() +
               "\nParidad = " + _Paridad.ToString() +
               "\nBit De Parada = " + _BitDeParada.ToString() +
               "\nBaud Rate = " + _BaudRate.ToString() +
               "\nControl De Flujo = " + _ControlDeFlujo.ToString();
        }
        #endregion //Metodos Generados


    } //End of class Balanza

} //End of namespace Galac.Adm.Ccl.DispositivosExternos

