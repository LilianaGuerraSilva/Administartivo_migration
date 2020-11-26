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
    public class CotizacionStt : ISettDefinition {
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
        private string _NombrePlantillaCotizacion;
        private bool _DetalleProdCompCotizacion;
        private bool _UsaControlDespacho;
        private bool _LimpiezaDeCotizacionXFactura;
        private bool _ValidarArticulosAlGenerarFactura;
        private eCampoCodigoAlternativoDeArticulo _CampoCodigoAlternativoDeArticulo;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string NombrePlantillaCotizacion {
            get { return _NombrePlantillaCotizacion; }
            set { _NombrePlantillaCotizacion = LibString.Mid(value, 0, 50); }
        }

        public bool DetalleProdCompCotizacionAsBool {
            get { return _DetalleProdCompCotizacion; }
            set { _DetalleProdCompCotizacion = value; }
        }

        public string DetalleProdCompCotizacion {
            set { _DetalleProdCompCotizacion = LibConvert.SNToBool(value); }
        }

        public bool UsaControlDespachoAsBool {
            get { return _UsaControlDespacho; }
            set { _UsaControlDespacho = value; }
        }

        public string UsaControlDespacho {
            set { _UsaControlDespacho = LibConvert.SNToBool(value); }
        }

        public bool LimpiezaDeCotizacionXFacturaAsBool {
            get { return _LimpiezaDeCotizacionXFactura; }
            set { _LimpiezaDeCotizacionXFactura = value; }
        }

        public string LimpiezaDeCotizacionXFactura {
            set { _LimpiezaDeCotizacionXFactura = LibConvert.SNToBool(value); }
        }

        public bool ValidarArticulosAlGenerarFacturaAsBool {
            get { return _ValidarArticulosAlGenerarFactura; }
            set { _ValidarArticulosAlGenerarFactura = value; }
        }

        public string ValidarArticulosAlGenerarFactura {
            set { _ValidarArticulosAlGenerarFactura = LibConvert.SNToBool(value); }
        }

        public eCampoCodigoAlternativoDeArticulo CampoCodigoAlternativoDeArticuloAsEnum {
            get { return _CampoCodigoAlternativoDeArticulo; }
            set { _CampoCodigoAlternativoDeArticulo = value; }
        }

        public string CampoCodigoAlternativoDeArticulo {
            set { _CampoCodigoAlternativoDeArticulo = (eCampoCodigoAlternativoDeArticulo)LibConvert.DbValueToEnum(value); }
        }

        public string CampoCodigoAlternativoDeArticuloAsDB {
            get { return LibConvert.EnumToDbValue((int)_CampoCodigoAlternativoDeArticulo); }
        }

        public string CampoCodigoAlternativoDeArticuloAsString {
            get { return LibEnumHelper.GetDescription(_CampoCodigoAlternativoDeArticulo); }
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

        public CotizacionStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            NombrePlantillaCotizacion = "";
            DetalleProdCompCotizacionAsBool = false;
            LimpiezaDeCotizacionXFacturaAsBool = false;
            ValidarArticulosAlGenerarFacturaAsBool = false;
            CampoCodigoAlternativoDeArticuloAsEnum = eCampoCodigoAlternativoDeArticulo.eCCADA_NoAsignado;
            fldTimeStamp = 0;
        }

        public CotizacionStt Clone() {
            CotizacionStt vResult = new CotizacionStt();
            vResult.NombrePlantillaCotizacion = _NombrePlantillaCotizacion;
            vResult.DetalleProdCompCotizacionAsBool = _DetalleProdCompCotizacion;
            vResult.LimpiezaDeCotizacionXFacturaAsBool = _LimpiezaDeCotizacionXFactura;
            vResult.ValidarArticulosAlGenerarFacturaAsBool = _ValidarArticulosAlGenerarFactura;
            vResult.CampoCodigoAlternativoDeArticuloAsEnum = _CampoCodigoAlternativoDeArticulo;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Plantilla de Impresión .... = " + _NombrePlantillaCotizacion +
               "\nImprimir detalle de Productos Compuestos . = " + _DetalleProdCompCotizacion +
               "\nLimpieza de Pedidos de Cotización por Emisión de Factura....... = " + _LimpiezaDeCotizacionXFactura +
               "\nValidar Articulos Al Generar Factura desde Cotización ......... = " + _ValidarArticulosAlGenerarFactura +
               "\nCampo Definible de Cód. Alterno de Artículo ........ = " + _CampoCodigoAlternativoDeArticulo.ToString();
        }
        #endregion //Metodos Generados


    } //End of class CotizacionStt

} //End of namespace Galac.Saw.Ccl.SttDef

