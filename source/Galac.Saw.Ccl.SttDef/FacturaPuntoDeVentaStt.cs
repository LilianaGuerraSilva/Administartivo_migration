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
    public class FacturaPuntoDeVentaStt :ISettDefinition {
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
        private bool _AcumularItemsEnRenglonesDeFactura;
        private bool _UsaPrecioSinIva;
        private eTipoDeNivelDePrecios _TipoDeNivelDePrecios;
        private string _ConceptoBancarioCobroDirecto;
        private string _CuentaBancariaCobroDirecto;
        private bool _ImprimeDireccionAlFinalDelComprobanteFiscal;
        private bool _UsaCobroDirecto;
        private bool _UsaClienteGenericoAlFacturar;
        private bool _UsarBalanza;
        private bool _UsaBusquedaDinamicaEnPuntoDeVenta;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public bool AcumularItemsEnRenglonesDeFacturaAsBool {
            get { return _AcumularItemsEnRenglonesDeFactura; }
            set { _AcumularItemsEnRenglonesDeFactura = value; }
        }

        public string AcumularItemsEnRenglonesDeFactura {
            set { _AcumularItemsEnRenglonesDeFactura = LibConvert.SNToBool(value); }
        }

        public bool UsaPrecioSinIvaAsBool {
            get { return _UsaPrecioSinIva; }
            set { _UsaPrecioSinIva = value; }
        }

        public string UsaPrecioSinIva {
            set { _UsaPrecioSinIva = LibConvert.SNToBool(value); }
        }


        public eTipoDeNivelDePrecios TipoDeNivelDePreciosAsEnum {
            get { return _TipoDeNivelDePrecios; }
            set { _TipoDeNivelDePrecios = value; }
        }

        public string TipoDeNivelDePrecios {
            set { _TipoDeNivelDePrecios = (eTipoDeNivelDePrecios)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeNivelDePreciosAsDB {
            get { return LibConvert.EnumToDbValue((int)_TipoDeNivelDePrecios); }
        }

        public string TipoDeNivelDePreciosAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeNivelDePrecios); }
        }

        public string ConceptoBancarioCobroDirecto {
            get { return _ConceptoBancarioCobroDirecto; }
            set { _ConceptoBancarioCobroDirecto = LibString.Mid(value,0,8); }
        }

        public string CuentaBancariaCobroDirecto {
            get { return _CuentaBancariaCobroDirecto; }
            set { _CuentaBancariaCobroDirecto = LibString.Mid(value,0,5); }
        }

        public bool ImprimeDireccionAlFinalDelComprobanteFiscalAsBool {
            get { return _ImprimeDireccionAlFinalDelComprobanteFiscal; }
            set { _ImprimeDireccionAlFinalDelComprobanteFiscal = value; }
        }

        public string ImprimeDireccionAlFinalDelComprobanteFiscal {
            set { _ImprimeDireccionAlFinalDelComprobanteFiscal = LibConvert.SNToBool(value); }
        }

        public bool UsaCobroDirectoAsBool {
            get { return _UsaCobroDirecto; }
            set { _UsaCobroDirecto = value; }
        }

        public string UsaCobroDirecto {
            set { _UsaCobroDirecto = LibConvert.SNToBool(value); }
        }

        public bool UsaClienteGenericoAlFacturarAsBool {
            get { return _UsaClienteGenericoAlFacturar; }
            set { _UsaClienteGenericoAlFacturar = value; }
        }

        public string UsaClienteGenericoAlFacturar {
            set { _UsaClienteGenericoAlFacturar = LibConvert.SNToBool(value); }
        }
        public bool UsarBalanzaAsBool {
            get { return _UsarBalanza; }
            set { _UsarBalanza = value; }
        }

        public string UsarBalanza {
            set { _UsarBalanza = LibConvert.SNToBool(value); }
        }

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }

        public bool UsaBusquedaDinamicaEnPuntoDeVentaAsBool {
            get {
                return _UsaBusquedaDinamicaEnPuntoDeVenta;
            }
            set {
                _UsaBusquedaDinamicaEnPuntoDeVenta = value;
            }
        }

        public string UsaBusquedaDinamicaEnPuntoDeVenta {
            set {
                _UsaBusquedaDinamicaEnPuntoDeVenta = LibConvert.SNToBool(value);
            }
        }
        #endregion //Propiedades
        #region Constructores

        public FacturaPuntoDeVentaStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            AcumularItemsEnRenglonesDeFacturaAsBool = false;
            UsaPrecioSinIvaAsBool = false;
            TipoDeNivelDePreciosAsEnum = eTipoDeNivelDePrecios.PorUsuario;
            ConceptoBancarioCobroDirecto = string.Empty;
            CuentaBancariaCobroDirecto = string.Empty;
            ImprimeDireccionAlFinalDelComprobanteFiscalAsBool = false;
            UsaCobroDirectoAsBool = false;
            UsaClienteGenericoAlFacturarAsBool = false;
            UsarBalanzaAsBool = false;
            fldTimeStamp = 0;
            UsaBusquedaDinamicaEnPuntoDeVentaAsBool = false;
        }

        public FacturaPuntoDeVentaStt Clone() {
            FacturaPuntoDeVentaStt vResult = new FacturaPuntoDeVentaStt();
            vResult.AcumularItemsEnRenglonesDeFacturaAsBool = _AcumularItemsEnRenglonesDeFactura;
            vResult.UsaPrecioSinIvaAsBool = _UsaPrecioSinIva;
            vResult.TipoDeNivelDePreciosAsEnum = _TipoDeNivelDePrecios;
            vResult.ConceptoBancarioCobroDirecto = _ConceptoBancarioCobroDirecto;
            vResult.CuentaBancariaCobroDirecto = _CuentaBancariaCobroDirecto;
            vResult.ImprimeDireccionAlFinalDelComprobanteFiscalAsBool = _ImprimeDireccionAlFinalDelComprobanteFiscal;
            vResult.UsaCobroDirectoAsBool = _UsaCobroDirecto;
            vResult.UsaClienteGenericoAlFacturarAsBool = _UsaClienteGenericoAlFacturar;
            vResult.UsarBalanzaAsBool = _UsarBalanza;
            vResult.fldTimeStamp = _fldTimeStamp;
            vResult.UsaBusquedaDinamicaEnPuntoDeVentaAsBool = _UsaBusquedaDinamicaEnPuntoDeVenta;
            return vResult;
        }

        public override string ToString() {
            return "Acumular Items En Renglones De Factura = " + _AcumularItemsEnRenglonesDeFactura +
                "\nUsa Precio Sin Iva = " + _UsaPrecioSinIva +
                "\nTipo de Nivel de Precios = " + _TipoDeNivelDePrecios.ToString() +
                "\nConcepto Bancario = " + _ConceptoBancarioCobroDirecto +
                "\nCuenta Bancaria Cobro Directo = " + _CuentaBancariaCobroDirecto +
                "\nImprime Direccion al Final del Comprobante Fiscal  = " + _ImprimeDireccionAlFinalDelComprobanteFiscal +
                "\nUsa Cobro Directo  = " + _UsaCobroDirecto +
                "\nUsa Cliente Generico Al Facturar" + _UsaClienteGenericoAlFacturar +
                "\nUsar Balanza = " + _UsarBalanza +
                "\nUsa Búsqueda Dinámica En Punto De Venta = " + _UsaBusquedaDinamicaEnPuntoDeVenta;
        }
        #endregion //Metodos Generados


    } //End of class FacturaPuntoDeVentaStt

} //End of namespace Galac.Saw.Ccl.SttDef

