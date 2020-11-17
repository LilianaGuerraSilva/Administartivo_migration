using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Ccl.CajaChica {
    [Serializable]
    public class Proveedor {
        #region Variables
        private int _ConsecutivoCompania;
        private string _CodigoProveedor;
        private string _NombreProveedor;
        private string _Contacto;
        private string _NumeroRIF;
        private string _NumeroNIT;
        private eTipodePersonaRetencion _TipoDePersona;
        private string _CodigoRetencionUsual;
        private string _Telefonos;
        private string _Direccion;
        private string _Fax;
        private string _Email;
        private string _TipodeProveedor;
        private eTipoDeProveedorDeLibrosFiscales _TipoDeProveedorDeLibrosFiscales;
        private decimal _PorcentajeRetencionIVA;
        private string _CuentaContableCxP;
        private string _CuentaContableGastos;
        private string _CuentaContableAnticipo;
        private string _CodigoLote;
        private string _Beneficiario;
        private bool _UsarBeneficiarioImpCheq;
        private eTipoDocumentoIdentificacion _TipoDocumentoIdentificacion;
        private bool _EsAgenteDeRetencionIva;
        private string _Nombre;
        private string _ApellidoPaterno;
        private string _ApellidoMaterno;
        private string _CodigoContribuyente;
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

        public string CodigoProveedor {
            get { return _CodigoProveedor; }
            set { _CodigoProveedor = LibString.Mid(value, 0, 10); }
        }

        public string NombreProveedor {
            get { return _NombreProveedor; }
            set { _NombreProveedor = LibString.Mid(value, 0, 60); }
        }

        public string Contacto {
            get { return _Contacto; }
            set { _Contacto = LibString.Mid(value, 0, 35); }
        }

        public string NumeroRIF {
            get { return _NumeroRIF; }
            set { _NumeroRIF = LibString.Mid(value, 0, 20); }
        }

        public string NumeroNIT {
            get { return _NumeroNIT; }
            set { _NumeroNIT = LibString.Mid(value, 0, 12); }
        }

        public eTipodePersonaRetencion TipoDePersonaAsEnum {
            get { return _TipoDePersona; }
            set { _TipoDePersona = value; }
        }

        public string TipoDePersona {
            set { _TipoDePersona = (eTipodePersonaRetencion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDePersonaAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDePersona); }
        }

        public string TipoDePersonaAsString {
            get { return LibEnumHelper.GetDescription(_TipoDePersona); }
        }

        public string CodigoRetencionUsual {
            get { return _CodigoRetencionUsual; }
            set { _CodigoRetencionUsual = LibString.Mid(value, 0, 6); }
        }

        public string Telefonos {
            get { return _Telefonos; }
            set { _Telefonos = LibString.Mid(value, 0, 40); }
        }

        public string Direccion {
            get { return _Direccion; }
            set { _Direccion = LibString.Mid(value, 0, 255); }
        }

        public string Fax {
            get { return _Fax; }
            set { _Fax = LibString.Mid(value, 0, 25); }
        }

        public string Email {
            get { return _Email; }
            set { _Email = LibString.Mid(value, 0, 40); }
        }

        public string TipodeProveedor {
            get { return _TipodeProveedor; }
            set { _TipodeProveedor = LibString.Mid(value, 0, 20); }
        }

        public eTipoDeProveedorDeLibrosFiscales TipoDeProveedorDeLibrosFiscalesAsEnum {
            get { return _TipoDeProveedorDeLibrosFiscales; }
            set { _TipoDeProveedorDeLibrosFiscales = value; }
        }

        public string TipoDeProveedorDeLibrosFiscales {
            set { _TipoDeProveedorDeLibrosFiscales = (eTipoDeProveedorDeLibrosFiscales)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeProveedorDeLibrosFiscalesAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeProveedorDeLibrosFiscales); }
        }

        public string TipoDeProveedorDeLibrosFiscalesAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeProveedorDeLibrosFiscales); }
        }

        public decimal PorcentajeRetencionIVA {
            get { return _PorcentajeRetencionIVA; }
            set { _PorcentajeRetencionIVA = value; }
        }

        public string CuentaContableCxP {
            get { return _CuentaContableCxP; }
            set { _CuentaContableCxP = LibString.Mid(value, 0, 30); }
        }

        public string CuentaContableGastos {
            get { return _CuentaContableGastos; }
            set { _CuentaContableGastos = LibString.Mid(value, 0, 30); }
        }

        public string CuentaContableAnticipo {
            get { return _CuentaContableAnticipo; }
            set { _CuentaContableAnticipo = LibString.Mid(value, 0, 30); }
        }

        public string CodigoLote {
            get { return _CodigoLote; }
            set { _CodigoLote = LibString.Mid(value, 0, 10); }
        }

        public string Beneficiario {
            get { return _Beneficiario; }
            set { _Beneficiario = LibString.Mid(value, 0, 60); }
        }

        public bool UsarBeneficiarioImpCheqAsBool {
            get { return _UsarBeneficiarioImpCheq; }
            set { _UsarBeneficiarioImpCheq = value; }
        }

        public string UsarBeneficiarioImpCheq {
            set { _UsarBeneficiarioImpCheq = LibConvert.SNToBool(value); }
        }


        public eTipoDocumentoIdentificacion TipoDocumentoIdentificacionAsEnum {
            get { return _TipoDocumentoIdentificacion; }
            set { _TipoDocumentoIdentificacion = value; }
        }

        public string TipoDocumentoIdentificacion {
            set { _TipoDocumentoIdentificacion = (eTipoDocumentoIdentificacion)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDocumentoIdentificacionAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDocumentoIdentificacion); }
        }

        public string TipoDocumentoIdentificacionAsString {
            get { return LibEnumHelper.GetDescription(_TipoDocumentoIdentificacion); }
        }

        public bool EsAgenteDeRetencionIvaAsBool {
            get { return _EsAgenteDeRetencionIva; }
            set { _EsAgenteDeRetencionIva = value; }
        }

        public string EsAgenteDeRetencionIva {
            set { _EsAgenteDeRetencionIva = LibConvert.SNToBool(value); }
        }


        public string Nombre {
            get { return _Nombre; }
            set { _Nombre = LibString.Mid(value, 0, 20); }
        }

        public string ApellidoPaterno {
            get { return _ApellidoPaterno; }
            set { _ApellidoPaterno = LibString.Mid(value, 0, 20); }
        }

        public string ApellidoMaterno {
            get { return _ApellidoMaterno; }
            set { _ApellidoMaterno = LibString.Mid(value, 0, 20); }
        }

        public string CodigoContribuyente {
            get { return _CodigoContribuyente; }
            set { _CodigoContribuyente = LibString.Mid(value, 0, 20); }
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

        public Proveedor() {
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            CodigoProveedor = "";
            NombreProveedor = "";
            Contacto = "";
            NumeroRIF = "";
            NumeroNIT = "";
            TipoDePersonaAsEnum = eTipodePersonaRetencion.PJ_Domiciliada;
            CodigoRetencionUsual = "";
            Telefonos = "";
            Direccion = "";
            Fax = "";
            Email = "";
            TipodeProveedor = "";
            TipoDeProveedorDeLibrosFiscalesAsEnum = eTipoDeProveedorDeLibrosFiscales.ConRif;
            PorcentajeRetencionIVA = 0;
            CuentaContableCxP = "";
            CuentaContableGastos = "";
            CuentaContableAnticipo = "";
            CodigoLote = "";
            Beneficiario = "";
            UsarBeneficiarioImpCheqAsBool = false;
            TipoDocumentoIdentificacionAsEnum = eTipoDocumentoIdentificacion.RUC;
            EsAgenteDeRetencionIvaAsBool = false;
            Nombre = "";
            ApellidoPaterno = "";
            ApellidoMaterno = "";
            CodigoContribuyente = "";
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public Proveedor Clone() {
            Proveedor vResult = new Proveedor();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.CodigoProveedor = _CodigoProveedor;
            vResult.NombreProveedor = _NombreProveedor;
            vResult.Contacto = _Contacto;
            vResult.NumeroRIF = _NumeroRIF;
            vResult.NumeroNIT = _NumeroNIT;
            vResult.TipoDePersonaAsEnum = _TipoDePersona;
            vResult.CodigoRetencionUsual = _CodigoRetencionUsual;
            vResult.Telefonos = _Telefonos;
            vResult.Direccion = _Direccion;
            vResult.Fax = _Fax;
            vResult.Email = _Email;
            vResult.TipodeProveedor = _TipodeProveedor;
            vResult.TipoDeProveedorDeLibrosFiscalesAsEnum = _TipoDeProveedorDeLibrosFiscales;
            vResult.PorcentajeRetencionIVA = _PorcentajeRetencionIVA;
            vResult.CuentaContableCxP = _CuentaContableCxP;
            vResult.CuentaContableGastos = _CuentaContableGastos;
            vResult.CuentaContableAnticipo = _CuentaContableAnticipo;
            vResult.CodigoLote = _CodigoLote;
            vResult.Beneficiario = _Beneficiario;
            vResult.UsarBeneficiarioImpCheqAsBool = _UsarBeneficiarioImpCheq;
            vResult.TipoDocumentoIdentificacionAsEnum = _TipoDocumentoIdentificacion;
            vResult.EsAgenteDeRetencionIvaAsBool = _EsAgenteDeRetencionIva;
            vResult.Nombre = _Nombre;
            vResult.ApellidoPaterno = _ApellidoPaterno;
            vResult.ApellidoMaterno = _ApellidoMaterno;
            vResult.CodigoContribuyente = _CodigoContribuyente;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nCódigo = " + _CodigoProveedor +
               "\nNombre Proveedor = " + _NombreProveedor +
               "\nContacto = " + _Contacto +
               "\nN° R.I.F. = " + _NumeroRIF +
               "\nN° N.I.T. = " + _NumeroNIT +
               "\nTipo De Persona = " + _TipoDePersona.ToString() +
               "\nCodigo Retencion Usual = " + _CodigoRetencionUsual +
               "\nTeléfonos = " + _Telefonos +
               "\nDirección = " + _Direccion +
               "\nNº Fax = " + _Fax +
               "\nE -mail = " + _Email +
               "\nTipode Proveedor = " + _TipodeProveedor +
               "\nTipo De Proveedor De Libros Fiscales = " + _TipoDeProveedorDeLibrosFiscales.ToString() +
               "\nPorcentaje Retencion IVA = " + _PorcentajeRetencionIVA.ToString() +
               "\nCta x pagar Proveedor = " + _CuentaContableCxP +
               "\ncta gastos de Proveedor = " + _CuentaContableGastos +
               "\nCuenta Contable Anticipo = " + _CuentaContableAnticipo +
               "\nCodigo Lote = " + _CodigoLote +
               "\nBeneficiario = " + _Beneficiario +
               "\nUsar Beficiario al Imprimir Cheque = " + _UsarBeneficiarioImpCheq +
               "\nTipo Documento Identificacion = " + _TipoDocumentoIdentificacion.ToString() +
               "\nEs Agente De Retencion Iva = " + _EsAgenteDeRetencionIva +
               "\nNombre = " + _Nombre +
               "\nApellido Paterno = " + _ApellidoPaterno +
               "\nApellido Materno = " + _ApellidoMaterno +
               "\nCódigo Contribuyente = " + _CodigoContribuyente +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class Proveedor

} //End of namespace Galac.Dbo.Ccl.CajaChica

