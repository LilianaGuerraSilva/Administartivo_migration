using LibGalac.Aos.Base;
using System;
using System.Xml;

namespace Galac.Saw.Ccl.SttDef {
    [Serializable]
    public class VerificadorDePreciosStt : ISettDefinition {
        #region Variables
        private string _GroupName;
        private string _Module;
        private string rutaImagen;
        private int duracionEnPantallaEnSegundos;
        private eTipoDeBusquedaArticulo _TipoDeBusquedaArticulo;
        private eNivelDePrecio _NivelDePrecioAMostrar;
        private eTipoDePrecioAMostrarEnVerificador _TipoDePrecioAMostrarEnVerificador;
        private long _fldTimeStamp;
        XmlDocument _datos;
        private bool _UsaMostrarPreciosEnDivisa;
        private eTipoDeConversionParaPrecios _TipoDeConversionParaPrecios;
        #endregion //Variables

        #region Propiedades
        public string GroupName {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        public string Module {
            get { return _Module; }
            set { _Module = value; }
        }

        public int DuracionEnPantallaEnSegundos {
            get { return duracionEnPantallaEnSegundos; }
            set { duracionEnPantallaEnSegundos = value; }
        }

        public eTipoDeBusquedaArticulo TipoDeBusquedaArticuloAsEnum {
            get { return _TipoDeBusquedaArticulo; }
            set { _TipoDeBusquedaArticulo = value; }
        }

        public string TipoDeBusquedaArticulo {
            set { _TipoDeBusquedaArticulo = (eTipoDeBusquedaArticulo)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeBusquedaArticuloAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeBusquedaArticulo); }
        }

        public string TipoDeBusquedaArticuloAsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoDeBusquedaArticulo); }
        }

        public eNivelDePrecio NivelDePrecioAMostrarAsEnum {
            get { return _NivelDePrecioAMostrar; }
            set { _NivelDePrecioAMostrar = value; }
        }

        public string NivelDePrecioAMostrar {
            set { _NivelDePrecioAMostrar = (eNivelDePrecio)LibConvert.DbValueToEnum(value); }
        }

        public string NivelDePrecioAMostrarAsString {
            get { return LibEnumHelper.GetDescription(_NivelDePrecioAMostrar); }
        }

        public string NivelDePrecioAMostrarAsDB {
            get { return LibConvert.EnumToDbValue((int)_NivelDePrecioAMostrar); }
        }

        public eTipoDePrecioAMostrarEnVerificador TipoDePrecioAMostrarEnVerificadorAsEnum {
            get { return _TipoDePrecioAMostrarEnVerificador; }
            set { _TipoDePrecioAMostrarEnVerificador = value; }
        }

        public string TipoDePrecioAMostrarEnVerificador {
            set { _TipoDePrecioAMostrarEnVerificador = (eTipoDePrecioAMostrarEnVerificador)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDePrecioAMostrarEnVerificadorAsString {
            get { return LibEnumHelper.GetDescription(_TipoDePrecioAMostrarEnVerificador); }
        }

        public string TipoDePrecioAMostrarEnVerificadorAsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoDePrecioAMostrarEnVerificador); }
        }

        public string RutaImagen {
            get { return rutaImagen; }
            set { rutaImagen = value; }
        }

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }

        public bool UsaMostrarPreciosEnDivisaAsBool {
            get { return _UsaMostrarPreciosEnDivisa; }
            set { _UsaMostrarPreciosEnDivisa = value; }
        }

        public string UsaMostrarPreciosEnDivisa {
            set { _UsaMostrarPreciosEnDivisa = LibConvert.SNToBool(value); }
        }

        public eTipoDeConversionParaPrecios TipoDeConversionParaPreciosAsEnum {
            get { return _TipoDeConversionParaPrecios; }
            set { _TipoDeConversionParaPrecios = value; }
        }

        public string TipoDeConversionParaPrecios {
            set { _TipoDeConversionParaPrecios = (eTipoDeConversionParaPrecios)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeConversionParaPreciosAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeConversionParaPrecios); }
        }

        public string TipoDeConversionParaPreciosAsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoDeConversionParaPrecios); }
        }

        #endregion //Propiedades

        #region Constructores
        public VerificadorDePreciosStt() {
            Clear();
        }
        #endregion //Constructores

        #region Metodos Generados
        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            DuracionEnPantallaEnSegundos = 5;
            TipoDeBusquedaArticuloAsEnum = eTipoDeBusquedaArticulo.Codigo;
            NivelDePrecioAMostrarAsEnum = eNivelDePrecio.Nivel1;
            TipoDePrecioAMostrarEnVerificadorAsEnum = eTipoDePrecioAMostrarEnVerificador.PrecioDesglosado;
            RutaImagen = string.Empty; ;
            fldTimeStamp = 0;
        }

        public VerificadorDePreciosStt Clone() {
            VerificadorDePreciosStt vResult = new VerificadorDePreciosStt();
            vResult.fldTimeStamp = _fldTimeStamp;
            vResult.DuracionEnPantallaEnSegundos = duracionEnPantallaEnSegundos;
            vResult.RutaImagen = rutaImagen;
            vResult.TipoDePrecioAMostrarEnVerificadorAsEnum = _TipoDePrecioAMostrarEnVerificador;
            vResult.NivelDePrecioAMostrarAsEnum = _NivelDePrecioAMostrar;
			TipoDeBusquedaArticuloAsEnum = _TipoDeBusquedaArticulo;
            return vResult;
        }

        public override string ToString() {
            return "\r\nDuración en Pantalla (Segundos): " + DuracionEnPantallaEnSegundos.ToString()
                 + "\r\nTipo de Precio a Mostrar: " + TipoDePrecioAMostrarEnVerificadorAsEnum.GetDescription()
                 + "\r\nNivel de precio a Mostrar: " + NivelDePrecioAMostrarAsEnum.GetDescription()
                 + "\r\nTipo de Búsqueda de Artículos: " + TipoDeBusquedaArticuloAsEnum.GetDescription()
                 + "\r\nRuta Imagen: " + RutaImagen;
        }
        #endregion //Metodos Generados


    } //End of class FacturacionElectronica

} //End of namespace Galac.Saw.Ccl.SttDef

