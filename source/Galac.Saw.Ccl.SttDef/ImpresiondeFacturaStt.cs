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
    public class ImpresiondeFacturaStt : ISettDefinition {
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
        private int _NumeroDeDigitosEnFactura;
        private int _CantidadDeCopiasDeLaFacturaAlImprimir;
        private bool _UsarDecimalesAlImprimirCantidad;
        private bool _DetalleProdCompFactura;
        private eFormaDeOrdenarDetalleFactura _FormaDeOrdenarDetalleFactura;
        private bool _ImprimirFacturaConSubtotalesPorLineaDeProducto;
        private bool _NoImprimirFactura;
        private bool _ImprimirBorradorAlInsertarFactura;
        private bool _ImprimeDireccionAlFinalDelComprobanteFiscal;
        private bool _ConcatenaLetraEaArticuloExento;
        private bool _ImprimirTipoCobroEnFactura;
        private int _NumItemImprimirFactura;
        private eAccionLimiteItemsFactura _AccionLimiteItemsFactura;
        private eTipoDeFormatoFecha _FormatoDeFecha;
        private bool _ImprimirAnexoDeSerial;
        private string _NombrePlantillaAnexoSeriales;
        private string _FormatoDeFechaTexto;
        private bool _ImprimirComprobanteFiscalEnContrato;

       
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int NumeroDeDigitosEnFactura {
            get { return _NumeroDeDigitosEnFactura; }
            set { _NumeroDeDigitosEnFactura = value; }
        }

        public int CantidadDeCopiasDeLaFacturaAlImprimir {
            get { return _CantidadDeCopiasDeLaFacturaAlImprimir; }
            set { _CantidadDeCopiasDeLaFacturaAlImprimir = value; }
        }

        public bool UsarDecimalesAlImprimirCantidadAsBool {
            get { return _UsarDecimalesAlImprimirCantidad; }
            set { _UsarDecimalesAlImprimirCantidad = value; }
        }

        public string UsarDecimalesAlImprimirCantidad {
            set { _UsarDecimalesAlImprimirCantidad = LibConvert.SNToBool(value); }
        }


        public bool DetalleProdCompFacturaAsBool {
            get { return _DetalleProdCompFactura; }
            set { _DetalleProdCompFactura = value; }
        }

        public string DetalleProdCompFactura {
            set { _DetalleProdCompFactura = LibConvert.SNToBool(value); }
        }


        public eFormaDeOrdenarDetalleFactura FormaDeOrdenarDetalleFacturaAsEnum {
            get { return _FormaDeOrdenarDetalleFactura; }
            set { _FormaDeOrdenarDetalleFactura = value; }
        }

        public string FormaDeOrdenarDetalleFactura {
            set { _FormaDeOrdenarDetalleFactura = (eFormaDeOrdenarDetalleFactura)LibConvert.DbValueToEnum(value); }
        }

        public string FormaDeOrdenarDetalleFacturaAsDB {
            get { return LibConvert.EnumToDbValue((int) _FormaDeOrdenarDetalleFactura); }
        }

        public string FormaDeOrdenarDetalleFacturaAsString {
            get { return LibEnumHelper.GetDescription(_FormaDeOrdenarDetalleFactura); }
        }

        public bool ImprimirFacturaConSubtotalesPorLineaDeProductoAsBool {
            get { return _ImprimirFacturaConSubtotalesPorLineaDeProducto; }
            set { _ImprimirFacturaConSubtotalesPorLineaDeProducto = value; }
        }

        public string ImprimirFacturaConSubtotalesPorLineaDeProducto {
            set { _ImprimirFacturaConSubtotalesPorLineaDeProducto = LibConvert.SNToBool(value); }
        }


        public bool NoImprimirFacturaAsBool {
            get { return _NoImprimirFactura; }
            set { _NoImprimirFactura = value; }
        }

        public string NoImprimirFactura {
            set { _NoImprimirFactura = LibConvert.SNToBool(value); }
        }


        public bool ImprimirBorradorAlInsertarFacturaAsBool {
            get { return _ImprimirBorradorAlInsertarFactura; }
            set { _ImprimirBorradorAlInsertarFactura = value; }
        }

        public string ImprimirBorradorAlInsertarFactura {
            set { _ImprimirBorradorAlInsertarFactura = LibConvert.SNToBool(value); }
        }


        public bool ImprimeDireccionAlFinalDelComprobanteFiscalAsBool {
            get { return _ImprimeDireccionAlFinalDelComprobanteFiscal; }
            set { _ImprimeDireccionAlFinalDelComprobanteFiscal = value; }
        }

        public string ImprimeDireccionAlFinalDelComprobanteFiscal {
            set { _ImprimeDireccionAlFinalDelComprobanteFiscal = LibConvert.SNToBool(value); }
        }


        public bool ConcatenaLetraEaArticuloExentoAsBool {
            get { return _ConcatenaLetraEaArticuloExento; }
            set { _ConcatenaLetraEaArticuloExento = value; }
        }

        public string ConcatenaLetraEaArticuloExento {
            set { _ConcatenaLetraEaArticuloExento = LibConvert.SNToBool(value); }
        }


        public bool ImprimirTipoCobroEnFacturaAsBool {
            get { return _ImprimirTipoCobroEnFactura; }
            set { _ImprimirTipoCobroEnFactura = value; }
        }

        public string ImprimirTipoCobroEnFactura {
            set { _ImprimirTipoCobroEnFactura = LibConvert.SNToBool(value); }
        }


        public int NumItemImprimirFactura {
            get { return _NumItemImprimirFactura; }
            set { _NumItemImprimirFactura = value; }
        }

        public eAccionLimiteItemsFactura AccionLimiteItemsFacturaAsEnum {
            get { return _AccionLimiteItemsFactura; }
            set { _AccionLimiteItemsFactura = value; }
        }

        public string AccionLimiteItemsFactura {
            set { _AccionLimiteItemsFactura = (eAccionLimiteItemsFactura)LibConvert.DbValueToEnum(value); }
        }

        public string AccionLimiteItemsFacturaAsDB {
            get { return LibConvert.EnumToDbValue((int) _AccionLimiteItemsFactura); }
        }

        public string AccionLimiteItemsFacturaAsString {
            get { return LibEnumHelper.GetDescription(_AccionLimiteItemsFactura); }
        }

        public eTipoDeFormatoFecha FormatoDeFechaAsEnum {
            get { return _FormatoDeFecha; }
            set { _FormatoDeFecha = value; }
        }

        public string FormatoDeFecha {
            set { _FormatoDeFecha = (eTipoDeFormatoFecha)LibConvert.DbValueToEnum(value); }
        }

        public string FormatoDeFechaAsDB {
            get { return LibConvert.EnumToDbValue((int) _FormatoDeFecha); }
        }

        public string FormatoDeFechaAsString {
            get { return LibEnumHelper.GetDescription(_FormatoDeFecha); }
        }
        public bool ImprimirAnexoDeSerialAsBool {
            get { return _ImprimirAnexoDeSerial; }
            set { _ImprimirAnexoDeSerial = value; }
        }

        public string ImprimirAnexoDeSerial {
            set { _ImprimirAnexoDeSerial = LibConvert.SNToBool(value); }
        }


        public string NombrePlantillaAnexoSeriales {
            get { return _NombrePlantillaAnexoSeriales; }
            set { _NombrePlantillaAnexoSeriales = LibString.Mid(value, 0, 50); }
        }

        public string FormatoDeFechaTexto {
           get { return _FormatoDeFechaTexto; }
           set {_FormatoDeFechaTexto = value;}
        }
		
        public bool ImprimirComprobanteFiscalEnContratoAsBool {
            get { return _ImprimirComprobanteFiscalEnContrato; }
            set { _ImprimirComprobanteFiscalEnContrato = value; }
        }

        public string ImprimirComprobanteFiscalEnContrato {
            set { _ImprimirComprobanteFiscalEnContrato = LibConvert.SNToBool(value); }
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

        public ImpresiondeFacturaStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            NumeroDeDigitosEnFactura = 0;
            CantidadDeCopiasDeLaFacturaAlImprimir = 0;
            UsarDecimalesAlImprimirCantidadAsBool = false;
            DetalleProdCompFacturaAsBool = false;
            FormaDeOrdenarDetalleFacturaAsEnum = eFormaDeOrdenarDetalleFactura.Comofuecargada;
            ImprimirFacturaConSubtotalesPorLineaDeProductoAsBool = false;
            NoImprimirFacturaAsBool = false;
            ImprimirBorradorAlInsertarFacturaAsBool = false;
            ImprimeDireccionAlFinalDelComprobanteFiscalAsBool = false;
            ConcatenaLetraEaArticuloExentoAsBool = false;
            ImprimirTipoCobroEnFacturaAsBool = false;
            NumItemImprimirFactura = 0;
            AccionLimiteItemsFacturaAsEnum = eAccionLimiteItemsFactura.SoloAdvertir;
            FormatoDeFecha = "";
            ImprimirAnexoDeSerialAsBool = false;
            NombrePlantillaAnexoSeriales = "";
            ImprimirComprobanteFiscalEnContratoAsBool = false;
            fldTimeStamp = 0;
        }

        public ImpresiondeFacturaStt Clone() {
            ImpresiondeFacturaStt vResult = new ImpresiondeFacturaStt();
            vResult.NumeroDeDigitosEnFactura = _NumeroDeDigitosEnFactura;
            vResult.CantidadDeCopiasDeLaFacturaAlImprimir = _CantidadDeCopiasDeLaFacturaAlImprimir;
            vResult.UsarDecimalesAlImprimirCantidadAsBool = _UsarDecimalesAlImprimirCantidad;
            vResult.DetalleProdCompFacturaAsBool = _DetalleProdCompFactura;
            vResult.FormaDeOrdenarDetalleFacturaAsEnum = _FormaDeOrdenarDetalleFactura;
            vResult.ImprimirFacturaConSubtotalesPorLineaDeProductoAsBool = _ImprimirFacturaConSubtotalesPorLineaDeProducto;
            vResult.NoImprimirFacturaAsBool = _NoImprimirFactura;
            vResult.ImprimirBorradorAlInsertarFacturaAsBool = _ImprimirBorradorAlInsertarFactura;
            vResult.ImprimeDireccionAlFinalDelComprobanteFiscalAsBool = _ImprimeDireccionAlFinalDelComprobanteFiscal;
            vResult.ConcatenaLetraEaArticuloExentoAsBool = _ConcatenaLetraEaArticuloExento;
            vResult.ImprimirTipoCobroEnFacturaAsBool = _ImprimirTipoCobroEnFactura;
            vResult.NumItemImprimirFactura = _NumItemImprimirFactura;
            vResult.AccionLimiteItemsFacturaAsEnum = _AccionLimiteItemsFactura;
            vResult.FormatoDeFechaAsEnum = _FormatoDeFecha;
            vResult.ImprimirAnexoDeSerialAsBool = _ImprimirAnexoDeSerial;
            vResult.NombrePlantillaAnexoSeriales = _NombrePlantillaAnexoSeriales;
            vResult.ImprimirComprobanteFiscalEnContratoAsBool = _ImprimirComprobanteFiscalEnContrato;
		    vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }
       
        public override string ToString() {
            return "Número de Dígitos en Factura ... = " + _NumeroDeDigitosEnFactura.ToString() +
                "\nNúmero de copias al imprimir ... = " + _CantidadDeCopiasDeLaFacturaAlImprimir.ToString() +
                "\nUsar decimales al imprimir 'Cantidad' ... = " + _UsarDecimalesAlImprimirCantidad +
                "\nImprimir detalle de Productos Compuestos . = " + _DetalleProdCompFactura +
                "\nForma de ordenar el detalle ... = " + _FormaDeOrdenarDetalleFactura.ToString() +
                "\nImprimir sub-totales por Linea de Producto .. = " + _ImprimirFacturaConSubtotalesPorLineaDeProducto +
                "\nNo imprimir la Factura despues de insertar .. = " + _NoImprimirFactura +
                "\nImprimir el Borrador de Factura al Insertar = " + _ImprimirBorradorAlInsertarFactura +
                "\nImprime Direccion Al Final Del ComprobanteFiscal        = " + _ImprimeDireccionAlFinalDelComprobanteFiscal +
                "\nConcatenar Letra (E) a Artículo Exento o Exonerado .. = " + _ConcatenaLetraEaArticuloExento +
                "\n Imprimir Tipo Cobro En Factura = " + _ImprimirTipoCobroEnFactura +
                "\nNumero de Items en Factura (*) ...... = " + _NumItemImprimirFactura.ToString() +
                "\nAcción al Limite de Items de Factura = " + _AccionLimiteItemsFactura.ToString() +
                "\nFormato De Fecha = " + _FormatoDeFecha +
                "\nImprimir  Anexo de Seriales .......... = " + _ImprimirAnexoDeSerial +
                "\nPlantilla de Impresión: = " + _NombrePlantillaAnexoSeriales +
                "\nImprimir Comprobante Fiscal En Contrato = " + _ImprimirComprobanteFiscalEnContrato;
        }
        #endregion //Metodos Generados


    } //End of class ImpresiondeFacturaStt

} //End of namespace Galac.Saw.Ccl.SttDef

