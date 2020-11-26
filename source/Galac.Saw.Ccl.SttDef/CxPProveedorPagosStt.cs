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
    public class CxPProveedorPagosStt : ISettDefinition {
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
        private bool _ExigirInformacionLibroDeCompras;
        private bool _UsarCodigoProveedorEnPantalla;
        private int _LongitudCodigoProveedor;
        private int _NumCopiasComprobantepago;
        private string _NombrePlantillaComprobanteDePago;
        private eTipoDeOrdenDePagoAImprimir _TipoDeOrdenDePagoAImprimir;
        private bool _ConfirmarImpresionPorSecciones;
        private bool _NoImprimirComprobanteDePago;
        private bool _ImprimirComprobanteContableDePago;
        private string _ConceptoBancarioReversoDePago;
        private bool _AvisarSiProveedorTieneAnticipos;
        private bool _OrdenarCxPPorFacturaDocumento;
        private string _NombrePlantillaRetencionImpuestoMunicipal;
        private bool _RetieneImpuestoMunicipal;
        private int _PrimerNumeroComprobanteRetImpuestoMunicipal;
        private string _NombrePlantillaRetencionImpuestoMunicipalInforme;
        private eFechaSugeridaRetencionesCxP _FechaSugeridaRetencionesCxP;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public bool ExigirInformacionLibroDeComprasAsBool {
            get { return _ExigirInformacionLibroDeCompras; }
            set { _ExigirInformacionLibroDeCompras = value; }
        }

        public string ExigirInformacionLibroDeCompras {
            set { _ExigirInformacionLibroDeCompras = LibConvert.SNToBool(value); }
        }


        public bool UsarCodigoProveedorEnPantallaAsBool {
            get { return _UsarCodigoProveedorEnPantalla; }
            set { _UsarCodigoProveedorEnPantalla = value; }
        }

        public string UsarCodigoProveedorEnPantalla {
            set { _UsarCodigoProveedorEnPantalla = LibConvert.SNToBool(value); }
        }


        public int LongitudCodigoProveedor {
            get { return _LongitudCodigoProveedor; }
            set { _LongitudCodigoProveedor = value; }
        }

        public int NumCopiasComprobantepago {
            get { return _NumCopiasComprobantepago; }
            set { _NumCopiasComprobantepago = value; }
        }

        public string NombrePlantillaComprobanteDePago {
            get { return _NombrePlantillaComprobanteDePago; }
            set { _NombrePlantillaComprobanteDePago = LibString.Mid(value, 0, 50); }
        }

        public eTipoDeOrdenDePagoAImprimir TipoDeOrdenDePagoAImprimirAsEnum {
            get { return _TipoDeOrdenDePagoAImprimir; }
            set { _TipoDeOrdenDePagoAImprimir = value; }
        }

        public string TipoDeOrdenDePagoAImprimir {
            set { _TipoDeOrdenDePagoAImprimir = (eTipoDeOrdenDePagoAImprimir)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeOrdenDePagoAImprimirAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeOrdenDePagoAImprimir); }
        }

        public string TipoDeOrdenDePagoAImprimirAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeOrdenDePagoAImprimir); }
        }

        public bool ConfirmarImpresionPorSeccionesAsBool {
            get { return _ConfirmarImpresionPorSecciones; }
            set { _ConfirmarImpresionPorSecciones = value; }
        }

        public string ConfirmarImpresionPorSecciones {
            set { _ConfirmarImpresionPorSecciones = LibConvert.SNToBool(value); }
        }


        public bool NoImprimirComprobanteDePagoAsBool {
            get { return _NoImprimirComprobanteDePago; }
            set { _NoImprimirComprobanteDePago = value; }
        }

        public string NoImprimirComprobanteDePago {
            set { _NoImprimirComprobanteDePago = LibConvert.SNToBool(value); }
        }


        public bool ImprimirComprobanteContableDePagoAsBool {
            get { return _ImprimirComprobanteContableDePago; }
            set { _ImprimirComprobanteContableDePago = value; }
        }

        public string ImprimirComprobanteContableDePago {
            set { _ImprimirComprobanteContableDePago = LibConvert.SNToBool(value); }
        }


        public string ConceptoBancarioReversoDePago {
            get { return _ConceptoBancarioReversoDePago; }
            set { _ConceptoBancarioReversoDePago = LibString.Mid(value, 0, 8); }
        }

        public bool AvisarSiProveedorTieneAnticiposAsBool {
            get { return _AvisarSiProveedorTieneAnticipos; }
            set { _AvisarSiProveedorTieneAnticipos = value; }
        }

        public string AvisarSiProveedorTieneAnticipos {
            set { _AvisarSiProveedorTieneAnticipos = LibConvert.SNToBool(value); }
        }


        public bool OrdenarCxPPorFacturaDocumentoAsBool {
            get { return _OrdenarCxPPorFacturaDocumento; }
            set { _OrdenarCxPPorFacturaDocumento = value; }
        }

        public string OrdenarCxPPorFacturaDocumento {
            set { _OrdenarCxPPorFacturaDocumento = LibConvert.SNToBool(value); }
        }


        public string NombrePlantillaRetencionImpuestoMunicipal {
            get { return _NombrePlantillaRetencionImpuestoMunicipal; }
            set { _NombrePlantillaRetencionImpuestoMunicipal = LibString.Mid(value, 0, 100); }
        }

        public bool RetieneImpuestoMunicipalAsBool {
            get { return _RetieneImpuestoMunicipal; }
            set { _RetieneImpuestoMunicipal = value; }
        }

        public string RetieneImpuestoMunicipal {
            set { _RetieneImpuestoMunicipal = LibConvert.SNToBool(value); }
        }

        public int PrimerNumeroComprobanteRetImpuestoMunicipal {
            get { return _PrimerNumeroComprobanteRetImpuestoMunicipal; }
            set { _PrimerNumeroComprobanteRetImpuestoMunicipal = value; }
        }
        
        public string NombrePlantillaRetencionImpuestoMunicipalInforme {
           get { return _NombrePlantillaRetencionImpuestoMunicipalInforme; }
           set { _NombrePlantillaRetencionImpuestoMunicipalInforme = LibString.Mid(value, 0, 100); }
        }

        public eFechaSugeridaRetencionesCxP FechaSugeridaRetencionesCxPAsEnum {
            get { return _FechaSugeridaRetencionesCxP; }
            set { _FechaSugeridaRetencionesCxP = value; }
        }

        public string FechaSugeridaRetencionesCxP {
            set { _FechaSugeridaRetencionesCxP = (eFechaSugeridaRetencionesCxP)LibConvert.DbValueToEnum(value); }
        }

        public string FechaSugeridaRetencionesCxPAsDB {
            get { return LibConvert.EnumToDbValue((int) _FechaSugeridaRetencionesCxP); }
        }

        public string FechaSugeridaRetencionesCxPAsString {
            get { return LibEnumHelper.GetDescription(_FechaSugeridaRetencionesCxP); }
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

        public CxPProveedorPagosStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ExigirInformacionLibroDeComprasAsBool = false;
            UsarCodigoProveedorEnPantallaAsBool = false;
            LongitudCodigoProveedor = 0;
            NumCopiasComprobantepago = 0;
            NombrePlantillaComprobanteDePago = "";
            TipoDeOrdenDePagoAImprimirAsEnum = eTipoDeOrdenDePagoAImprimir.OrdendePagoconCheque;
            ConfirmarImpresionPorSeccionesAsBool = false;
            NoImprimirComprobanteDePagoAsBool = false;
            ImprimirComprobanteContableDePagoAsBool = false;
            ConceptoBancarioReversoDePago = "";
            AvisarSiProveedorTieneAnticiposAsBool = false;
            OrdenarCxPPorFacturaDocumentoAsBool = false;
            NombrePlantillaRetencionImpuestoMunicipal = "";
            RetieneImpuestoMunicipalAsBool = false;
            PrimerNumeroComprobanteRetImpuestoMunicipal = 0;
            NombrePlantillaRetencionImpuestoMunicipalInforme = "";
            FechaSugeridaRetencionesCxPAsEnum = eFechaSugeridaRetencionesCxP.FechaFacturaCxP;
            fldTimeStamp = 0;
        }

        public CxPProveedorPagosStt Clone() {
            CxPProveedorPagosStt vResult = new CxPProveedorPagosStt();
            vResult.ExigirInformacionLibroDeComprasAsBool = _ExigirInformacionLibroDeCompras;
            vResult.UsarCodigoProveedorEnPantallaAsBool = _UsarCodigoProveedorEnPantalla;
            vResult.LongitudCodigoProveedor = _LongitudCodigoProveedor;
            vResult.NumCopiasComprobantepago = _NumCopiasComprobantepago;
            vResult.NombrePlantillaComprobanteDePago = _NombrePlantillaComprobanteDePago;
            vResult.TipoDeOrdenDePagoAImprimirAsEnum = _TipoDeOrdenDePagoAImprimir;
            vResult.ConfirmarImpresionPorSeccionesAsBool = _ConfirmarImpresionPorSecciones;
            vResult.NoImprimirComprobanteDePagoAsBool = _NoImprimirComprobanteDePago;
            vResult.ImprimirComprobanteContableDePagoAsBool = _ImprimirComprobanteContableDePago;
            vResult.ConceptoBancarioReversoDePago = _ConceptoBancarioReversoDePago;
            vResult.AvisarSiProveedorTieneAnticiposAsBool = _AvisarSiProveedorTieneAnticipos;
            vResult.OrdenarCxPPorFacturaDocumentoAsBool = _OrdenarCxPPorFacturaDocumento;
            vResult.NombrePlantillaRetencionImpuestoMunicipal = _NombrePlantillaRetencionImpuestoMunicipal;
            vResult.RetieneImpuestoMunicipalAsBool = _RetieneImpuestoMunicipal;
            vResult.PrimerNumeroComprobanteRetImpuestoMunicipal = _PrimerNumeroComprobanteRetImpuestoMunicipal;
            vResult.NombrePlantillaRetencionImpuestoMunicipalInforme = _NombrePlantillaRetencionImpuestoMunicipalInforme;
            vResult.FechaSugeridaRetencionesCxPAsEnum = _FechaSugeridaRetencionesCxP;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Exigir Informacion Libro de Compras = " + _ExigirInformacionLibroDeCompras +
               "\nUsar Codigo Proveedor En Pantalla = " + _UsarCodigoProveedorEnPantalla +
               "\nLongitud Codigo Proveedor = " + _LongitudCodigoProveedor.ToString() +
               "\nNumCopias Comprobantepago = " + _NumCopiasComprobantepago.ToString() +
               "\nNombrePlantillaComprobanteDePago = " + _NombrePlantillaComprobanteDePago +
               "\nTipo De Orden De Pago A Imprimir = " + _TipoDeOrdenDePagoAImprimir.ToString() +
               "\nConfirmarImpresionPorSecciones = " + _ConfirmarImpresionPorSecciones +
               "\nNo Imprimir Comprobante De Pago = " + _NoImprimirComprobanteDePago +
               "\nImprimir Comprobante Contable De Pago = " + _ImprimirComprobanteContableDePago +
               "\nConcepto Bancario de Reverso de Pago = " + _ConceptoBancarioReversoDePago +
               "\nAvisar Si Proveedor Tiene Anticipos = " + _AvisarSiProveedorTieneAnticipos +
               "\nOrdenar CxP por Número de Factura/Documento = " + _OrdenarCxPPorFacturaDocumento +
               "\nNombrePlantillaRetencionImpuestoMunicipal = " + _NombrePlantillaRetencionImpuestoMunicipal +
               "\nPrimer Numero Comprobante de Retención Impuesto Municipal = " + _PrimerNumeroComprobanteRetImpuestoMunicipal.ToString() +
               "\nNombrePlantillaRetencionImpuestoMunicipalInforme = " + _NombrePlantillaRetencionImpuestoMunicipalInforme +
               "\nRetiene Impuesto Municipal = " + _RetieneImpuestoMunicipal +
               "\nFecha Sugerida para las retenciones = " + _FechaSugeridaRetencionesCxP.ToString();
        }
        #endregion //Metodos Generados


    } //End of class CxPProveedorPagosStt

} //End of namespace Galac.Saw.Ccl.SttDef

