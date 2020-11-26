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
    public class InventarioStt : ISettDefinition {
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
        private bool _UsarBaseImponibleDiferenteA0Y100;
        private bool _UsaAlmacen;
        private string _CodigoAlmacenGenerico;
        private int _ConsecutivoAlmacenGenerico;
        private ePermitirSobregiro _PermitirSobregiro;
        private bool _ActivarFacturacionPorAlmacen;
        private string _SinonimoGrupo;
        private string _SinonimoTalla;
        private string _SinonimoColor;
        private string _SinonimoSerial;
        private string _SinonimoRollo;
        private bool _ImprimirTransferenciaAlInsertar;
        private eCantidadDeDecimales _CantidadDeDecimales;
        private string _NombreCampoDefinibleInventario1;
        private string _NombreCampoDefinibleInventario2;
        private string _NombreCampoDefinibleInventario3;
        private string _NombreCampoDefinibleInventario4;
        private string _NombreCampoDefinibleInventario5;
        private bool _AsociaCentroDeCostoyAlmacen;
        private bool _AvisoDeReservasvencidas;
        private bool _VerificarStock;
        private bool _ImprimeSerialRolloLuegoDeDescripArticulo;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public bool UsarBaseImponibleDiferenteA0Y100AsBool {
            get { return _UsarBaseImponibleDiferenteA0Y100; }
            set { _UsarBaseImponibleDiferenteA0Y100 = value; }
        }

        public string UsarBaseImponibleDiferenteA0Y100 {
            set { _UsarBaseImponibleDiferenteA0Y100 = LibConvert.SNToBool(value); }
        }


        public bool UsaAlmacenAsBool {
            get { return _UsaAlmacen; }
            set { _UsaAlmacen = value; }
        }

        public string UsaAlmacen {
            set { _UsaAlmacen = LibConvert.SNToBool(value); }
        }

        public string CodigoAlmacenGenerico {
            get { return _CodigoAlmacenGenerico; }
            set { _CodigoAlmacenGenerico = LibString.Mid(value, 0, 5); }
        }

        public int ConsecutivoAlmacenGenerico {
            get { return _ConsecutivoAlmacenGenerico; }
            set { _ConsecutivoAlmacenGenerico = value; }
        }

        public ePermitirSobregiro PermitirSobregiroAsEnum {
            get { return _PermitirSobregiro; }
            set { _PermitirSobregiro = value; }
        }

        public string PermitirSobregiro {
            set { _PermitirSobregiro = (ePermitirSobregiro)LibConvert.DbValueToEnum(value); }
        }

        public string PermitirSobregiroAsDB {
            get { return LibConvert.EnumToDbValue((int) _PermitirSobregiro); }
        }

        public string PermitirSobregiroAsString {
            get { return LibEnumHelper.GetDescription(_PermitirSobregiro); }
        }

        public bool ActivarFacturacionPorAlmacenAsBool {
            get { return _ActivarFacturacionPorAlmacen; }
            set { _ActivarFacturacionPorAlmacen = value; }
        }

        public string ActivarFacturacionPorAlmacen {
            set { _ActivarFacturacionPorAlmacen = LibConvert.SNToBool(value); }
        }


        public string SinonimoGrupo {
            get { return _SinonimoGrupo; }
            set { _SinonimoGrupo = LibString.Mid(value, 0, 30); }
        }

        public string SinonimoTalla {
            get { return _SinonimoTalla; }
            set { _SinonimoTalla = LibString.Mid(value, 0, 10); }
        }

        public string SinonimoColor {
            get { return _SinonimoColor; }
            set { _SinonimoColor = LibString.Mid(value, 0, 10); }
        }

        public string SinonimoSerial {
            get { return _SinonimoSerial; }
            set { _SinonimoSerial = LibString.Mid(value, 0, 10); }
        }

        public string SinonimoRollo {
            get { return _SinonimoRollo; }
            set { _SinonimoRollo = LibString.Mid(value, 0, 10); }
        }

        public bool ImprimirTransferenciaAlInsertarAsBool {
            get { return _ImprimirTransferenciaAlInsertar; }
            set { _ImprimirTransferenciaAlInsertar = value; }
        }

        public string ImprimirTransferenciaAlInsertar {
            set { _ImprimirTransferenciaAlInsertar = LibConvert.SNToBool(value); }
        }


        public eCantidadDeDecimales CantidadDeDecimalesAsEnum {
            get { return _CantidadDeDecimales; }
            set { _CantidadDeDecimales = value; }
        }

        public string CantidadDeDecimales {
            set { _CantidadDeDecimales = (eCantidadDeDecimales)LibConvert.DbValueToEnum(value); }
        }

        public string CantidadDeDecimalesAsDB {
            get { return LibConvert.EnumToDbValue((int) _CantidadDeDecimales); }
        }

        public string CantidadDeDecimalesAsString {
            get { return LibEnumHelper.GetDescription(_CantidadDeDecimales); }
        }

        public string NombreCampoDefinibleInventario1 {
            get { return _NombreCampoDefinibleInventario1; }
            set { _NombreCampoDefinibleInventario1 = LibString.Mid(value, 0, 20); }
        }

        public string NombreCampoDefinibleInventario2 {
            get { return _NombreCampoDefinibleInventario2; }
            set { _NombreCampoDefinibleInventario2 = LibString.Mid(value, 0, 20); }
        }

        public string NombreCampoDefinibleInventario3 {
            get { return _NombreCampoDefinibleInventario3; }
            set { _NombreCampoDefinibleInventario3 = LibString.Mid(value, 0, 20); }
        }

        public string NombreCampoDefinibleInventario4 {
            get { return _NombreCampoDefinibleInventario4; }
            set { _NombreCampoDefinibleInventario4 = LibString.Mid(value, 0, 20); }
        }

        public string NombreCampoDefinibleInventario5 {
            get { return _NombreCampoDefinibleInventario5; }
            set { _NombreCampoDefinibleInventario5 = LibString.Mid(value, 0, 20); }
        }

        public bool AsociaCentroDeCostoyAlmacenAsBool {
            get { return _AsociaCentroDeCostoyAlmacen; }
            set { _AsociaCentroDeCostoyAlmacen = value; }
        }

        public string AsociaCentroDeCostoyAlmacen {
            set { _AsociaCentroDeCostoyAlmacen = LibConvert.SNToBool(value); }
        }


        public bool AvisoDeReservasvencidasAsBool {
            get { return _AvisoDeReservasvencidas; }
            set { _AvisoDeReservasvencidas = value; }
        }

        public string AvisoDeReservasvencidas {
            set { _AvisoDeReservasvencidas = LibConvert.SNToBool(value); }
        }


        public bool VerificarStockAsBool {
            get { return _VerificarStock; }
            set { _VerificarStock = value; }
        }

        public string VerificarStock {
            set { _VerificarStock = LibConvert.SNToBool(value); }
        }

        public bool ImprimeSerialRolloLuegoDeDescripArticuloAsBool {
            get { return _ImprimeSerialRolloLuegoDeDescripArticulo; }
            set { _ImprimeSerialRolloLuegoDeDescripArticulo = value; }
        }

        public string ImprimeSerialRolloLuegoDeDescripArticulo {
            set { _ImprimeSerialRolloLuegoDeDescripArticulo = LibConvert.SNToBool(value); }
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

        public InventarioStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            UsarBaseImponibleDiferenteA0Y100AsBool = false;
            UsaAlmacenAsBool = false;
            CodigoAlmacenGenerico = "";
            ConsecutivoAlmacenGenerico = 0;
            PermitirSobregiroAsEnum = ePermitirSobregiro.NoChequearExistencia;
            ActivarFacturacionPorAlmacenAsBool = false;
            SinonimoGrupo = "";
            SinonimoTalla = "";
            SinonimoColor = "";
            SinonimoSerial = "";
            SinonimoRollo = "";
            ImprimirTransferenciaAlInsertarAsBool = false;
            CantidadDeDecimalesAsEnum = eCantidadDeDecimales.Dos;
            NombreCampoDefinibleInventario1 = "";
            NombreCampoDefinibleInventario2 = "";
            NombreCampoDefinibleInventario3 = "";
            NombreCampoDefinibleInventario4 = "";
            NombreCampoDefinibleInventario5 = "";
            AsociaCentroDeCostoyAlmacenAsBool = false;
            AvisoDeReservasvencidasAsBool = false;
            VerificarStockAsBool = false;
            ImprimeSerialRolloLuegoDeDescripArticuloAsBool = false;
            fldTimeStamp = 0;
        }

        public InventarioStt Clone() {
            InventarioStt vResult = new InventarioStt();
            vResult.UsarBaseImponibleDiferenteA0Y100AsBool = _UsarBaseImponibleDiferenteA0Y100;
            vResult.UsaAlmacenAsBool = _UsaAlmacen;
            vResult.CodigoAlmacenGenerico = _CodigoAlmacenGenerico;
            vResult.ConsecutivoAlmacenGenerico = _ConsecutivoAlmacenGenerico;
            vResult.PermitirSobregiroAsEnum = _PermitirSobregiro;
            vResult.ActivarFacturacionPorAlmacenAsBool = _ActivarFacturacionPorAlmacen;
            vResult.SinonimoGrupo = _SinonimoGrupo;
            vResult.SinonimoTalla = _SinonimoTalla;
            vResult.SinonimoColor = _SinonimoColor;
            vResult.SinonimoSerial = _SinonimoSerial;
            vResult.SinonimoRollo = _SinonimoRollo;
            vResult.ImprimirTransferenciaAlInsertarAsBool = _ImprimirTransferenciaAlInsertar;
            vResult.CantidadDeDecimalesAsEnum = _CantidadDeDecimales;
            vResult.NombreCampoDefinibleInventario1 = _NombreCampoDefinibleInventario1;
            vResult.NombreCampoDefinibleInventario2 = _NombreCampoDefinibleInventario2;
            vResult.NombreCampoDefinibleInventario3 = _NombreCampoDefinibleInventario3;
            vResult.NombreCampoDefinibleInventario4 = _NombreCampoDefinibleInventario4;
            vResult.NombreCampoDefinibleInventario5 = _NombreCampoDefinibleInventario5;
            vResult.AsociaCentroDeCostoyAlmacenAsBool = _AsociaCentroDeCostoyAlmacen;
            vResult.AvisoDeReservasvencidasAsBool = _AvisoDeReservasvencidas;
            vResult.VerificarStockAsBool = _VerificarStock;
            vResult.ImprimeSerialRolloLuegoDeDescripArticuloAsBool = _ImprimeSerialRolloLuegoDeDescripArticulo;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Usar Base Imponible Diferente 100%  = " + _UsarBaseImponibleDiferenteA0Y100 +
               "\nUsar Almacén ...... = " + _UsaAlmacen +
               "\nAlmacén genérico ... = " + _CodigoAlmacenGenerico +
               "\nConsecutivo Almacén genérico ... = " + _ConsecutivoAlmacenGenerico +
               "\nPermitir Sobregiro = " + _PermitirSobregiro.ToString() +
               "\nFiltrar Inventario Por Almacen al Facturar.. = " + _ActivarFacturacionPorAlmacen +
               "\nSinonimo Grupo = " + _SinonimoGrupo +
               "\nSinonimo Talla = " + _SinonimoTalla +
               "\nSinonimo Color = " + _SinonimoColor +
               "\nSinonimo Serial = " + _SinonimoSerial +
               "\nSinonimo Rollo = " + _SinonimoRollo +
               "\nMostrar Opcion de Imprimir al Insertar Transferencia = " + _ImprimirTransferenciaAlInsertar +
               "\nCantidad De Decimales = " + _CantidadDeDecimales.ToString() +
               "\nNombre Campo Definible Inventario 1 = " + _NombreCampoDefinibleInventario1 +
               "\nNombre Campo Definible Inventario 2 = " + _NombreCampoDefinibleInventario2 +
               "\nNombre Campo Definible Inventario 3 = " + _NombreCampoDefinibleInventario3 +
               "\nNombre Campo Definible Inventario 4 = " + _NombreCampoDefinibleInventario4 +
               "\nNombre Campo Definible Inventario 5 = " + _NombreCampoDefinibleInventario5 +
               "\nAsocia Centros De Costos a Almacén  = " + _AsociaCentroDeCostoyAlmacen +
               "\nAlerta de reservas vencidas = " + _AvisoDeReservasvencidas +
               "\nAlerta por  Maximo o Minimo del Stock  ... = " + _VerificarStock +
               "\nImprime Serial Rollo Luego De Descripción de Artículo... = " + _ImprimeSerialRolloLuegoDeDescripArticulo;
        }
        #endregion //Metodos Generados


    } //End of class InventarioStt

} //End of namespace Galac.Saw.Ccl.SttDef

