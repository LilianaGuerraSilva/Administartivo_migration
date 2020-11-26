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
    public class FacturacionStt : ISettDefinition {
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
        private bool _VerificarFacturasManualesFaltantes;
        private int _NumFacturasManualesFaltantes;
        private bool _PermitirFacturarConCantidadCero;
        private eTipoDocumentoFactura _DevolucionReversoSeGeneraComo;
        private bool _ExigirRifdeClienteAlEmitirFactura;
        private bool _SugerirNumeroControlFactura;
        private bool _PedirInformacionLibroVentasXlsalEmitirFactura;
        private eTipoDeNivelDePrecios _TipoDeNivelDePrecios;
        private bool _ComplConComodinEnBusqDeArtInv;
        private bool _UsarResumenDiarioDeVentas;
        private eItemsMontoFactura _ItemsMonto;
        private eComisionesEnFactura _ComisionesEnFactura;
        private eComisionesEnRenglones _ComisionesEnRenglones;
        private bool _CambiarFechaEnCuotasLuegoDeFijarFechaEntrega;
        private bool _BuscarArticuloXSerialAlFacturar;
        private string _NombreVendedorUno;
        private string _NombreVendedorDos;
        private string _NombreVendedorTres;
        private bool _UsaPrecioSinIva;
        private bool _UsaPrecioSinIvaEnResumenVtas;
        private bool _UsaListaDePrecioEnMonedaExtranjera;
        private bool _ResumenVtasAfectaInventario;
        private bool _UsarRenglonesEnResumenVtas;        
        private bool _PermitirCambioTasaMondExtrajalEmitirFactura;
        private bool _UsaListaDePrecioEnMonedaExtranjeraCXC;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public bool VerificarFacturasManualesFaltantesAsBool {
            get { return _VerificarFacturasManualesFaltantes; }
            set { _VerificarFacturasManualesFaltantes = value; }
        }

        public string VerificarFacturasManualesFaltantes {
            set { _VerificarFacturasManualesFaltantes = LibConvert.SNToBool(value); }
        }


        public int NumFacturasManualesFaltantes {
            get { return _NumFacturasManualesFaltantes; }
            set { _NumFacturasManualesFaltantes = value; }
        }

        public bool PermitirFacturarConCantidadCeroAsBool {
            get { return _PermitirFacturarConCantidadCero; }
            set { _PermitirFacturarConCantidadCero = value; }
        }

        public string PermitirFacturarConCantidadCero {
            set { _PermitirFacturarConCantidadCero = LibConvert.SNToBool(value); }
        }


        public eTipoDocumentoFactura DevolucionReversoSeGeneraComoAsEnum {
            get { return _DevolucionReversoSeGeneraComo; }
            set { _DevolucionReversoSeGeneraComo = value; }
        }

        public string DevolucionReversoSeGeneraComo {
            set { _DevolucionReversoSeGeneraComo = (eTipoDocumentoFactura)LibConvert.DbValueToEnum(value); }
        }

        public string DevolucionReversoSeGeneraComoAsDB {
            get { return LibConvert.EnumToDbValue((int) _DevolucionReversoSeGeneraComo); }
        }

        public string DevolucionReversoSeGeneraComoAsString {
            get { return LibEnumHelper.GetDescription(_DevolucionReversoSeGeneraComo); }
        }

        public bool ExigirRifdeClienteAlEmitirFacturaAsBool {
            get { return _ExigirRifdeClienteAlEmitirFactura; }
            set { _ExigirRifdeClienteAlEmitirFactura = value; }
        }

        public string ExigirRifdeClienteAlEmitirFactura {
            set { _ExigirRifdeClienteAlEmitirFactura = LibConvert.SNToBool(value); }
        }


        public bool SugerirNumeroControlFacturaAsBool {
            get { return _SugerirNumeroControlFactura; }
            set { _SugerirNumeroControlFactura = value; }
        }

        public string SugerirNumeroControlFactura {
            set { _SugerirNumeroControlFactura = LibConvert.SNToBool(value); }
        }


        public bool PedirInformacionLibroVentasXlsalEmitirFacturaAsBool {
            get { return _PedirInformacionLibroVentasXlsalEmitirFactura; }
            set { _PedirInformacionLibroVentasXlsalEmitirFactura = value; }
        }

        public string PedirInformacionLibroVentasXlsalEmitirFactura {
            set { _PedirInformacionLibroVentasXlsalEmitirFactura = LibConvert.SNToBool(value); }
        }


        public eTipoDeNivelDePrecios TipoDeNivelDePreciosAsEnum {
            get { return _TipoDeNivelDePrecios; }
            set { _TipoDeNivelDePrecios = value; }
        }

        public string TipoDeNivelDePrecios {
            set { _TipoDeNivelDePrecios = (eTipoDeNivelDePrecios)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeNivelDePreciosAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeNivelDePrecios); }
        }

        public string TipoDeNivelDePreciosAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeNivelDePrecios); }
        }

        public bool ComplConComodinEnBusqDeArtInvAsBool {
            get { return _ComplConComodinEnBusqDeArtInv; }
            set { _ComplConComodinEnBusqDeArtInv = value; }
        }

        public string ComplConComodinEnBusqDeArtInv {
            set { _ComplConComodinEnBusqDeArtInv = LibConvert.SNToBool(value); }
        }


        public bool UsarResumenDiarioDeVentasAsBool {
            get { return _UsarResumenDiarioDeVentas; }
            set { _UsarResumenDiarioDeVentas = value; }
        }

        public string UsarResumenDiarioDeVentas {
            set { _UsarResumenDiarioDeVentas = LibConvert.SNToBool(value); }
        }


        public eItemsMontoFactura ItemsMontoAsEnum {
            get { return _ItemsMonto; }
            set { _ItemsMonto = value; }
        }

        public string ItemsMonto {
            set { _ItemsMonto = (eItemsMontoFactura)LibConvert.DbValueToEnum(value); }
        }

        public string ItemsMontoAsDB {
            get { return LibConvert.EnumToDbValue((int) _ItemsMonto); }
        }

        public string ItemsMontoAsString {
            get { return LibEnumHelper.GetDescription(_ItemsMonto); }
        }

        public eComisionesEnFactura ComisionesEnFacturaAsEnum {
            get { return _ComisionesEnFactura; }
            set { _ComisionesEnFactura = value; }
        }

        public string ComisionesEnFactura {
            set { _ComisionesEnFactura = (eComisionesEnFactura)LibConvert.DbValueToEnum(value); }
        }

        public string ComisionesEnFacturaAsDB {
            get { return LibConvert.EnumToDbValue((int) _ComisionesEnFactura); }
        }

        public string ComisionesEnFacturaAsString {
            get { return LibEnumHelper.GetDescription(_ComisionesEnFactura); }
        }

        public eComisionesEnRenglones ComisionesEnRenglonesAsEnum {
            get { return _ComisionesEnRenglones; }
            set { _ComisionesEnRenglones = value; }
        }

        public string ComisionesEnRenglones {
            set { _ComisionesEnRenglones = (eComisionesEnRenglones)LibConvert.DbValueToEnum(value); }
        }

        public string ComisionesEnRenglonesAsDB {
            get { return LibConvert.EnumToDbValue((int) _ComisionesEnRenglones); }
        }

        public string ComisionesEnRenglonesAsString {
            get { return LibEnumHelper.GetDescription(_ComisionesEnRenglones); }
        }

        public bool CambiarFechaEnCuotasLuegoDeFijarFechaEntregaAsBool {
            get { return _CambiarFechaEnCuotasLuegoDeFijarFechaEntrega; }
            set { _CambiarFechaEnCuotasLuegoDeFijarFechaEntrega = value; }
        }

        public string CambiarFechaEnCuotasLuegoDeFijarFechaEntrega {
            set { _CambiarFechaEnCuotasLuegoDeFijarFechaEntrega = LibConvert.SNToBool(value); }
        }


        public bool BuscarArticuloXSerialAlFacturarAsBool {
            get { return _BuscarArticuloXSerialAlFacturar; }
            set { _BuscarArticuloXSerialAlFacturar = value; }
        }

        public string BuscarArticuloXSerialAlFacturar {
            set { _BuscarArticuloXSerialAlFacturar = LibConvert.SNToBool(value); }
        }


        public string NombreVendedorUno {
            get { return _NombreVendedorUno; }
            set { _NombreVendedorUno = LibString.Mid(value, 0, 10); }
        }

        public string NombreVendedorDos {
            get { return _NombreVendedorDos; }
            set { _NombreVendedorDos = LibString.Mid(value, 0, 10); }
        }

        public string NombreVendedorTres {
            get { return _NombreVendedorTres; }
            set { _NombreVendedorTres = LibString.Mid(value, 0, 10); }
        }

        public bool UsaPrecioSinIvaAsBool {
            get { return _UsaPrecioSinIva; }
            set { _UsaPrecioSinIva = value; }
        }

        public string UsaPrecioSinIva {
            set { _UsaPrecioSinIva = LibConvert.SNToBool(value); }
        }


        public bool UsaPrecioSinIvaEnResumenVtasAsBool {
            get { return _UsaPrecioSinIvaEnResumenVtas; }
            set { _UsaPrecioSinIvaEnResumenVtas = value; }
        }

        public string UsaPrecioSinIvaEnResumenVtas {
            set { _UsaPrecioSinIvaEnResumenVtas = LibConvert.SNToBool(value); }
        }

        public bool UsaListaDePrecioEnMonedaExtranjeraAsBool {
            get { return _UsaListaDePrecioEnMonedaExtranjera; }
            set { _UsaListaDePrecioEnMonedaExtranjera = value; }
        }
        public string UsaListaDePrecioEnMonedaExtranjera {
            set {_UsaListaDePrecioEnMonedaExtranjera = LibConvert.SNToBool(value);}
        }

        public bool ResumenVtasAfectaInventarioAsBool {
            get { return _ResumenVtasAfectaInventario; }
            set { _ResumenVtasAfectaInventario = value; }
        }

        public string ResumenVtasAfectaInventario {
            set { _ResumenVtasAfectaInventario = LibConvert.SNToBool(value); }
        }


        public bool UsarRenglonesEnResumenVtasAsBool {
            get { return _UsarRenglonesEnResumenVtas; }
            set { _UsarRenglonesEnResumenVtas = value; }
        }

        public string UsarRenglonesEnResumenVtas {
            set { _UsarRenglonesEnResumenVtas = LibConvert.SNToBool(value); }
        }
        public bool PermitirCambioTasaMondExtrajalEmitirFacturaAsBool {
            get { return _PermitirCambioTasaMondExtrajalEmitirFactura; }
            set { _PermitirCambioTasaMondExtrajalEmitirFactura = value; }
        }

        public string PermitirCambioTasaMondExtrajalEmitirFactura {
            set { _PermitirCambioTasaMondExtrajalEmitirFactura = LibConvert.SNToBool(value); }
        }

        public string UsaListaDePrecioEnMonedaExtranjeraCXC {
            set {
                _UsaListaDePrecioEnMonedaExtranjeraCXC = LibConvert.SNToBool(value);
            }
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

        public FacturacionStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            VerificarFacturasManualesFaltantesAsBool = false;
            NumFacturasManualesFaltantes = 0;
            PermitirFacturarConCantidadCeroAsBool = false;
            DevolucionReversoSeGeneraComoAsEnum = eTipoDocumentoFactura.Factura;
            ExigirRifdeClienteAlEmitirFacturaAsBool = false;
            SugerirNumeroControlFacturaAsBool = false;
            PedirInformacionLibroVentasXlsalEmitirFacturaAsBool = false;
            TipoDeNivelDePreciosAsEnum = eTipoDeNivelDePrecios.PorUsuario;
            ComplConComodinEnBusqDeArtInvAsBool = false;
            UsarResumenDiarioDeVentasAsBool = false;
            ItemsMontoAsEnum = eItemsMontoFactura.NO_PERMITIR_ITEMS_NEGATIVOS;
            ComisionesEnFacturaAsEnum = eComisionesEnFactura.SobreTotalFactura;
            ComisionesEnRenglonesAsEnum = eComisionesEnRenglones.PorUnVendedor;
            CambiarFechaEnCuotasLuegoDeFijarFechaEntregaAsBool = false;
            BuscarArticuloXSerialAlFacturarAsBool = false;
            NombreVendedorUno = "";
            NombreVendedorDos = "";
            NombreVendedorTres = "";
            UsaPrecioSinIvaAsBool = false;
            UsaPrecioSinIvaEnResumenVtasAsBool = false;
            ResumenVtasAfectaInventarioAsBool = false;
            UsarRenglonesEnResumenVtasAsBool = false;
            PermitirCambioTasaMondExtrajalEmitirFacturaAsBool = false;
            
            fldTimeStamp = 0;
        }

        public FacturacionStt Clone() {
            FacturacionStt vResult = new FacturacionStt();
            vResult.VerificarFacturasManualesFaltantesAsBool = _VerificarFacturasManualesFaltantes;
            vResult.NumFacturasManualesFaltantes = _NumFacturasManualesFaltantes;
            vResult.PermitirFacturarConCantidadCeroAsBool = _PermitirFacturarConCantidadCero;
            vResult.DevolucionReversoSeGeneraComoAsEnum = _DevolucionReversoSeGeneraComo;
            vResult.ExigirRifdeClienteAlEmitirFacturaAsBool = _ExigirRifdeClienteAlEmitirFactura;
            vResult.SugerirNumeroControlFacturaAsBool = _SugerirNumeroControlFactura;
            vResult.PedirInformacionLibroVentasXlsalEmitirFacturaAsBool = _PedirInformacionLibroVentasXlsalEmitirFactura;
            vResult.TipoDeNivelDePreciosAsEnum = _TipoDeNivelDePrecios;
            vResult.ComplConComodinEnBusqDeArtInvAsBool = _ComplConComodinEnBusqDeArtInv;
            vResult.UsarResumenDiarioDeVentasAsBool = _UsarResumenDiarioDeVentas;
            vResult.ItemsMontoAsEnum = _ItemsMonto;
            vResult.ComisionesEnFacturaAsEnum = _ComisionesEnFactura;
            vResult.ComisionesEnRenglonesAsEnum = _ComisionesEnRenglones;
            vResult.CambiarFechaEnCuotasLuegoDeFijarFechaEntregaAsBool = _CambiarFechaEnCuotasLuegoDeFijarFechaEntrega;
            vResult.BuscarArticuloXSerialAlFacturarAsBool = _BuscarArticuloXSerialAlFacturar;
            vResult.NombreVendedorUno = _NombreVendedorUno;
            vResult.NombreVendedorDos = _NombreVendedorDos;
            vResult.NombreVendedorTres = _NombreVendedorTres;
            vResult.UsaPrecioSinIvaAsBool = _UsaPrecioSinIva;
            vResult.UsaPrecioSinIvaEnResumenVtasAsBool = _UsaPrecioSinIvaEnResumenVtas;
            vResult.ResumenVtasAfectaInventarioAsBool = _ResumenVtasAfectaInventario;
            vResult.UsarRenglonesEnResumenVtasAsBool = _UsarRenglonesEnResumenVtas;
            vResult.PermitirCambioTasaMondExtrajalEmitirFacturaAsBool = _PermitirCambioTasaMondExtrajalEmitirFactura;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
            return "Verificar Facturas Manuales Faltantes = " + _VerificarFacturasManualesFaltantes +
                "\nNumFacturasManualesFaltantes = " + _NumFacturasManualesFaltantes.ToString() +
                "\nPermitirFacturarConCantidadCero = " + _PermitirFacturarConCantidadCero +
                "\nDevolucion Reverso Se Genera Como = " + _DevolucionReversoSeGeneraComo.ToString() +
                "\nExigirRifdeClienteAlEmitirFactura = " + _ExigirRifdeClienteAlEmitirFactura +
                "\nSugerir N° Control de Factura = " + _SugerirNumeroControlFactura +
                "\nPedir Informacion Libro VentasXlsal EmitirFactura = " + _PedirInformacionLibroVentasXlsalEmitirFactura +
                "\nTipo De Nivel De Precios = " + _TipoDeNivelDePrecios.ToString() +
                "\nUsar Lector de Código de Barra al Facturar..................... = " + _ComplConComodinEnBusqDeArtInv +
                "\nUsar Resumen Diario De Ventas = " + _UsarResumenDiarioDeVentas +
                "\nItems Monto = " + _ItemsMonto.ToString() +
                "\nComisiones en Factura = " + _ComisionesEnFactura.ToString() +
                "\nComisiones en Renglones = " + _ComisionesEnRenglones.ToString() +
                "\nCambiar fecha en cuotas luego de fijar fecha de entrega.... = " + _CambiarFechaEnCuotasLuegoDeFijarFechaEntrega +
                "\nBuscarArticuloXSerialAlFacturar = " + _BuscarArticuloXSerialAlFacturar +
                "\nNombre vendedor uno......... = " + _NombreVendedorUno +
                "\nNombre vendedor dos......... = " + _NombreVendedorDos +
                "\nNombre vendedor tres........ = " + _NombreVendedorTres +
                "\nUsa Precio Sin Iva = " + _UsaPrecioSinIva +
                "\nUsa Precio Sin Iva En Resumen Vtas = " + _UsaPrecioSinIvaEnResumenVtas +
                "\nResumen Vtas Afecta Inventario = " + _ResumenVtasAfectaInventario +
                "\nUsar Renglones En Resumen Vtas = " + _UsarRenglonesEnResumenVtas +
                "\nPermitirCambio de Tasa de Monda Extrajera al Emitir Factura = " + _PermitirCambioTasaMondExtrajalEmitirFactura +
                "\nUsar Lista De Precios En Moneda Extranjera en CXC = " + _UsaListaDePrecioEnMonedaExtranjeraCXC;
        }
        #endregion //Metodos Generados


    } //End of class FacturacionStt

} //End of namespace Galac.Saw.Ccl.SttDef

