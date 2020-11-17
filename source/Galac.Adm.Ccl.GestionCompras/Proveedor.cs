using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Comun.Ccl.TablasLey;


namespace Galac.Adm.Ccl.GestionCompras {
    [Serializable]
    public class Proveedor {
        #region Variables
        private int _ConsecutivoCompania;
        private string _CodigoProveedor;
        private int _Consecutivo;
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
        private string _NumeroCuentaBancaria;
        private string _CodigoContribuyente;
        private string _NumeroRUC;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        private string _TipoDePersonaDeCodigoretencionAsDB;
        private int _ConsecutivoPeriodo;
        private bool _AccesoCaracteristicaDeContabilidadActiva;
        private bool _UsaAuxiliares;
        private eTipodePersonaRetencion _TipoDePersonaDeCodigoRetencion;
        private eTipoDePersonaLibrosElectronicos _TipoDePersonaLibrosElectronicos;
        private string _CodigoPaisResidencia;
        private string _NombrePaisResidencia;
        private string _CodigoConveniosSunat;
        private string _PaisConveniosSunat;
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

        public int Consecutivo {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }

        public string NombreProveedor {
            get { return _NombreProveedor; }
            set { _NombreProveedor = LibString.Mid(value, 0, 160); }
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
           get { return _PorcentajeRetencionIVA;           }
           set { _PorcentajeRetencionIVA = value; }
        }

        //public ePorcentajeDeRetencionDeIVA PorcentajeRetencionIVAAsEnum {
        //    get { return _PorcentajeRetencionIVA; }
        //    set { _PorcentajeRetencionIVA = value; }
        //}

        //public string PorcentajeRetencionDeIVA {
        //    set { _PorcentajeRetencionIVA = (ePorcentajeDeRetencionDeIVA)LibConvert.DbValueToEnum(value); }
        //}

        //public string PorcentajeRetencionDeIVAAsDB {
        //    get { return LibConvert.EnumToDbValue((int) _PorcentajeRetencionIVA); }
        //}

        //public string PorcentajeRetencionDeIVAAsString {
        //    get { return LibEnumHelper.GetDescription(_PorcentajeRetencionIVA); }
        //}

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

        public string NumeroCuentaBancaria {
            get { return _NumeroCuentaBancaria; }
            set { _NumeroCuentaBancaria = LibString.Mid(value, 0, 20); }
        }

        public string CodigoContribuyente {
            get { return _CodigoContribuyente; }
            set { _CodigoContribuyente = LibString.Mid(value, 0, 20); }
        }

        public string NumeroRUC {
            get { return _NumeroRUC; }
            set { _NumeroRUC = LibString.Mid(value, 0, 20); }
        }

        public eTipoDePersonaLibrosElectronicos TipoDePersonaLibrosElectronicosAsEnum {
            get { return _TipoDePersonaLibrosElectronicos; }
            set { _TipoDePersonaLibrosElectronicos = value; }
        }

        public string TipoDePersonaLibrosElectronicos {
            set { _TipoDePersonaLibrosElectronicos = (eTipoDePersonaLibrosElectronicos)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDePersonaLibrosElectronicosAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDePersonaLibrosElectronicos); }
        }

        public string TipoDePersonaLibrosElectronicosAsString {
            get { return LibEnumHelper.GetDescription(_TipoDePersonaLibrosElectronicos); }
        }

        public string CodigoPaisResidencia {
            get { return _CodigoPaisResidencia; }
            set { _CodigoPaisResidencia = LibString.Mid(value, 0, 4); }
        }

        public string NombrePaisResidencia {
            get { return _NombrePaisResidencia; }
            set { _NombrePaisResidencia = LibString.Mid(value, 0, 120); }
        }

        public string CodigoConveniosSunat {
            get { return _CodigoConveniosSunat; }
            set { _CodigoConveniosSunat = LibString.Mid(value, 0, 2); }
        }

        public string PaisConveniosSunat {
            get { return _PaisConveniosSunat; }
            set { _PaisConveniosSunat = LibString.Mid(value, 0, 120); }
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

        public string TipoDePersonaDeCodigoretencionAsDB {
            get { return _TipoDePersonaDeCodigoretencionAsDB; }
            set { _TipoDePersonaDeCodigoretencionAsDB = LibString.Mid(value, 0, 40); }
        }

        public eTipodePersonaRetencion TipoDePersonaDeCodigoRetencionAsEnum {
            get { return _TipoDePersonaDeCodigoRetencion; }
            set { _TipoDePersonaDeCodigoRetencion = value; }
        }

        public int ConsecutivoPeriodo {
            get { return _ConsecutivoPeriodo; }
            set { _ConsecutivoPeriodo = value; }
        }

        public bool AccesoCaracteristicaDeContabilidadActiva {
            get { return _AccesoCaracteristicaDeContabilidadActiva; }
            set { _AccesoCaracteristicaDeContabilidadActiva = value; }
        }

        public bool UsaAuxiliares {
            get { return _UsaAuxiliares; }
            set { _UsaAuxiliares = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public Proveedor() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            CodigoProveedor = string.Empty;
            Consecutivo = 0;
            NombreProveedor = string.Empty;
            Contacto = string.Empty;
            NumeroRIF = string.Empty;
            NumeroNIT = string.Empty;
            TipoDePersonaAsEnum = eTipodePersonaRetencion.JuridicaDomiciliada;
            CodigoRetencionUsual = string.Empty;
            Telefonos = string.Empty;
            Direccion = string.Empty;
            Fax = string.Empty;
            Email = string.Empty;
            TipodeProveedor = string.Empty;
            TipoDeProveedorDeLibrosFiscalesAsEnum = eTipoDeProveedorDeLibrosFiscales.ConRif;
            PorcentajeRetencionIVA = 0;
            CuentaContableCxP = string.Empty;
            CuentaContableGastos = string.Empty;
            CuentaContableAnticipo = string.Empty;
            CodigoLote = string.Empty;
            Beneficiario = string.Empty;
            UsarBeneficiarioImpCheqAsBool = false;
            TipoDocumentoIdentificacionAsEnum = eTipoDocumentoIdentificacion.RUC;
            EsAgenteDeRetencionIvaAsBool = false;
            Nombre = string.Empty;
            ApellidoPaterno = string.Empty;
            ApellidoMaterno = string.Empty;
            NumeroCuentaBancaria = string.Empty;
            CodigoContribuyente = string.Empty;
            NumeroRUC = string.Empty;
            TipoDePersonaLibrosElectronicosAsEnum = eTipoDePersonaLibrosElectronicos.JuridicoDomiciliado;
            CodigoPaisResidencia = string.Empty;
            NombrePaisResidencia = string.Empty;
            CodigoConveniosSunat = string.Empty;
            PaisConveniosSunat = string.Empty;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public Proveedor Clone() {
            Proveedor vResult = new Proveedor();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.CodigoProveedor = _CodigoProveedor;
            vResult.Consecutivo = _Consecutivo;
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
            vResult.NumeroCuentaBancaria = _NumeroCuentaBancaria;
            vResult.CodigoContribuyente = _CodigoContribuyente;
            vResult.NumeroRUC = _NumeroRUC;
            vResult.TipoDePersonaLibrosElectronicosAsEnum = _TipoDePersonaLibrosElectronicos;
            vResult.CodigoPaisResidencia = _CodigoPaisResidencia;
            vResult.NombrePaisResidencia = _NombrePaisResidencia;
            vResult.CodigoConveniosSunat = _CodigoConveniosSunat;
            vResult.PaisConveniosSunat = _PaisConveniosSunat;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nCódigo = " + _CodigoProveedor +
               "\nConsecutivo = " + _Consecutivo.ToString() +
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
               "\nCxP Proveedor = " + _CuentaContableCxP +
               "\nGastos = " + _CuentaContableGastos +
               "\nAnticipo = " + _CuentaContableAnticipo +
               "\nCodigo Lote = " + _CodigoLote +
               "\nBeneficiario = " + _Beneficiario +
               "\nUsar Beficiario al Imprimir Cheque = " + _UsarBeneficiarioImpCheq +
               "\nTipo Documento Identificacion = " + _TipoDocumentoIdentificacion.ToString() +
               "\nEs Agente De Retencion Iva = " + _EsAgenteDeRetencionIva +
               "\nNombre = " + _Nombre +
               "\nApellido Paterno = " + _ApellidoPaterno +
               "\nApellido Materno = " + _ApellidoMaterno +
               "\nNúmero Cuenta Bancaria = " + _NumeroCuentaBancaria +
               "\nCódigo Contribuyente = " + _CodigoContribuyente +
               "\nR.U.C. = " + _NumeroRUC +
               "\nTipo de Persona Libros Electrónicos = " + _TipoDePersonaLibrosElectronicos.ToString() +
               "\nPaís de Residencia = " + _CodigoPaisResidencia +
               "\nPaís de Convenio = " + _CodigoConveniosSunat +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class Proveedor

} //End of namespace Galac.Adm.Ccl.GestionCompras

