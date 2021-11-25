using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.Cliente;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Ccl.Cliente {
    [Serializable]
    public class Cliente {
        #region Variables
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private string _Codigo;
        private string _Nombre;
        private string _NumeroRIF;
        private string _NumeroNIT;
        private string _Direccion;
        private string _Ciudad;
        private string _ZonaPostal;
        private string _Telefono;
        private string _FAX;
        private eStatusCliente _Status;
        private string _Contacto;
        private string _ZonaDeCobranza;
        private string _CodigoVendedor;
        private string _RazonInactividad;
        private string _Email;
        private bool _ActivarAvisoAlEscoger;
        private string _TextoDelAviso;
        private string _CuentaContableCxC;
        private string _CuentaContableIngresos;
        private string _CuentaContableAnticipo;
        private string _InfoGalac;
        private string _SectorDeNegocio;
        private string _CodigoLote;
        private eNivelDePrecio _NivelDePrecio;
        private eOrigenFacturacionOManual _Origen;
        private int _DiaCumpleanos;
        private int _MesCumpleanos;
        private bool _CorrespondenciaXEnviar;
        private bool _EsExtranjero;
        private DateTime _ClienteDesdeFecha;
        private string _AQueSeDedicaElCliente;
        private eTipoDocumentoIdentificacion _TipoDocumentoIdentificacion;
        private eTipoDeContribuyente _TipoDeContribuyente;
        private string _CampoDefinible1;
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
        public int Consecutivo {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }

        public string Codigo {
            get { return _Codigo; }
            set { _Codigo = LibString.Mid(value, 0, 10); }
        }

        public string Nombre {
            get { return _Nombre; }
            set { _Nombre = LibString.Mid(value, 0, 160); }
        }

        public string NumeroRIF {
            get { return _NumeroRIF; }
            set { _NumeroRIF = LibString.Mid(value, 0, 20); }
        }

        public string NumeroNIT {
            get { return _NumeroNIT; }
            set { _NumeroNIT = LibString.Mid(value, 0, 12); }
        }

        public string Direccion {
            get { return _Direccion; }
            set { _Direccion = LibString.Mid(value, 0, 255); }
        }

        public string Ciudad {
            get { return _Ciudad; }
            set { _Ciudad = LibString.Mid(value, 0, 100); }
        }

        public string ZonaPostal {
            get { return _ZonaPostal; }
            set { _ZonaPostal = LibString.Mid(value, 0, 7); }
        }

        public string Telefono {
            get { return _Telefono; }
            set { _Telefono = LibString.Mid(value, 0, 40); }
        }

        public string FAX {
            get { return _FAX; }
            set { _FAX = LibString.Mid(value, 0, 25); }
        }

        public eStatusCliente StatusAsEnum {
            get { return _Status; }
            set { _Status = value; }
        }

        public string Status {
            set { _Status = (eStatusCliente)LibConvert.DbValueToEnum(value); }
        }

        public string StatusAsDB {
            get { return LibConvert.EnumToDbValue((int) _Status); }
        }

        public string StatusAsString {
            get { return LibEnumHelper.GetDescription(_Status); }
        }

        public string Contacto {
            get { return _Contacto; }
            set { _Contacto = LibString.Mid(value, 0, 35); }
        }

        public string ZonaDeCobranza {
            get { return _ZonaDeCobranza; }
            set { _ZonaDeCobranza = LibString.Mid(value, 0, 20); }
        }

        public string CodigoVendedor {
            get { return _CodigoVendedor; }
            set { _CodigoVendedor = LibString.Mid(value, 0, 5); }
        }

        public string RazonInactividad {
            get { return _RazonInactividad; }
            set { _RazonInactividad = LibString.Mid(value, 0, 35); }
        }

        public string Email {
            get { return _Email; }
            set { _Email = LibString.Mid(value, 0, 100); }
        }

        public bool ActivarAvisoAlEscogerAsBool {
            get { return _ActivarAvisoAlEscoger; }
            set { _ActivarAvisoAlEscoger = value; }
        }

        public string ActivarAvisoAlEscoger {
            set { _ActivarAvisoAlEscoger = LibConvert.SNToBool(value); }
        }


        public string TextoDelAviso {
            get { return _TextoDelAviso; }
            set { _TextoDelAviso = LibString.Mid(value, 0, 150); }
        }

        public string CuentaContableCxC {
            get { return _CuentaContableCxC; }
            set { _CuentaContableCxC = LibString.Mid(value, 0, 30); }
        }

        public string CuentaContableIngresos {
            get { return _CuentaContableIngresos; }
            set { _CuentaContableIngresos = LibString.Mid(value, 0, 30); }
        }

        public string CuentaContableAnticipo {
            get { return _CuentaContableAnticipo; }
            set { _CuentaContableAnticipo = LibString.Mid(value, 0, 30); }
        }

        public string InfoGalac {
            get { return _InfoGalac; }
            set { _InfoGalac = LibString.Mid(value, 0, 1); }
        }

        public string SectorDeNegocio {
            get { return _SectorDeNegocio; }
            set { _SectorDeNegocio = LibString.Mid(value, 0, 20); }
        }

        public string CodigoLote {
            get { return _CodigoLote; }
            set { _CodigoLote = LibString.Mid(value, 0, 10); }
        }

        public eNivelDePrecio NivelDePrecioAsEnum {
            get { return _NivelDePrecio; }
            set { _NivelDePrecio = value; }
        }

        public string NivelDePrecio {
            set { _NivelDePrecio = (eNivelDePrecio)LibConvert.DbValueToEnum(value); }
        }

        public string NivelDePrecioAsDB {
            get { return LibConvert.EnumToDbValue((int) _NivelDePrecio); }
        }

        public string NivelDePrecioAsString {
            get { return LibEnumHelper.GetDescription(_NivelDePrecio); }
        }

        public eOrigenFacturacionOManual OrigenAsEnum {
            get { return _Origen; }
            set { _Origen = value; }
        }

        public string Origen {
            set { _Origen = (eOrigenFacturacionOManual)LibConvert.DbValueToEnum(value); }
        }

        public string OrigenAsDB {
            get { return LibConvert.EnumToDbValue((int) _Origen); }
        }

        public string OrigenAsString {
            get { return LibEnumHelper.GetDescription(_Origen); }
        }

        public int DiaCumpleanos {
            get { return _DiaCumpleanos; }
            set { _DiaCumpleanos = value; }
        }

        public int MesCumpleanos {
            get { return _MesCumpleanos; }
            set { _MesCumpleanos = value; }
        }

        public bool CorrespondenciaXEnviarAsBool {
            get { return _CorrespondenciaXEnviar; }
            set { _CorrespondenciaXEnviar = value; }
        }

        public string CorrespondenciaXEnviar {
            set { _CorrespondenciaXEnviar = LibConvert.SNToBool(value); }
        }


        public bool EsExtranjeroAsBool {
            get { return _EsExtranjero; }
            set { _EsExtranjero = value; }
        }

        public string EsExtranjero {
            set { _EsExtranjero = LibConvert.SNToBool(value); }
        }


        public DateTime ClienteDesdeFecha {
            get { return _ClienteDesdeFecha; }
            set { _ClienteDesdeFecha = LibConvert.DateToDbValue(value); }
        }

        public string AQueSeDedicaElCliente {
            get { return _AQueSeDedicaElCliente; }
            set { _AQueSeDedicaElCliente = LibString.Mid(value, 0, 100); }
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

        public eTipoDeContribuyente TipoDeContribuyenteAsEnum {
            get { return _TipoDeContribuyente; }
            set { _TipoDeContribuyente = value; }
        }

        public string TipoDeContribuyente {
            set { _TipoDeContribuyente = (eTipoDeContribuyente)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeContribuyenteAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeContribuyente); }
        }

        public string TipoDeContribuyenteAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeContribuyente); }
        }

        public string CampoDefinible1 {
            get { return _CampoDefinible1; }
            set { _CampoDefinible1 = LibString.Mid(value, 0, 20); }
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

        public Cliente() {
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
            Codigo = string.Empty;
            Nombre = string.Empty;
            NumeroRIF = string.Empty;
            NumeroNIT = string.Empty;
            Direccion = string.Empty;
            Ciudad = string.Empty;
            ZonaPostal = string.Empty;
            Telefono = string.Empty;
            FAX = string.Empty;
            StatusAsEnum = eStatusCliente.Activo;
            Contacto = string.Empty;
            ZonaDeCobranza = string.Empty;
            CodigoVendedor = string.Empty;
            RazonInactividad = string.Empty;
            Email = string.Empty;
            ActivarAvisoAlEscogerAsBool = false;
            TextoDelAviso = string.Empty;
            CuentaContableCxC = string.Empty;
            CuentaContableIngresos = string.Empty;
            CuentaContableAnticipo = string.Empty;
            InfoGalac = string.Empty;
            SectorDeNegocio = string.Empty;
            CodigoLote = string.Empty;
            NivelDePrecioAsEnum = eNivelDePrecio.Precio1;
            OrigenAsEnum = eOrigenFacturacionOManual.Factura;
            DiaCumpleanos = 0;
            MesCumpleanos = 0;
            CorrespondenciaXEnviarAsBool = false;
            EsExtranjeroAsBool = false;
            ClienteDesdeFecha = LibDate.Today();
            AQueSeDedicaElCliente = string.Empty;
            TipoDocumentoIdentificacionAsEnum = eTipoDocumentoIdentificacion.RUC;
            TipoDeContribuyenteAsEnum = eTipoDeContribuyente.Contribuyente;
            CampoDefinible1 = string.Empty;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public Cliente Clone() {
            Cliente vResult = new Cliente();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Consecutivo = _Consecutivo;
            vResult.Codigo = _Codigo;
            vResult.Nombre = _Nombre;
            vResult.NumeroRIF = _NumeroRIF;
            vResult.NumeroNIT = _NumeroNIT;
            vResult.Direccion = _Direccion;
            vResult.Ciudad = _Ciudad;
            vResult.ZonaPostal = _ZonaPostal;
            vResult.Telefono = _Telefono;
            vResult.FAX = _FAX;
            vResult.StatusAsEnum = _Status;
            vResult.Contacto = _Contacto;
            vResult.ZonaDeCobranza = _ZonaDeCobranza;
            vResult.CodigoVendedor = _CodigoVendedor;
            vResult.RazonInactividad = _RazonInactividad;
            vResult.Email = _Email;
            vResult.ActivarAvisoAlEscogerAsBool = _ActivarAvisoAlEscoger;
            vResult.TextoDelAviso = _TextoDelAviso;
            vResult.CuentaContableCxC = _CuentaContableCxC;
            vResult.CuentaContableIngresos = _CuentaContableIngresos;
            vResult.CuentaContableAnticipo = _CuentaContableAnticipo;
            vResult.InfoGalac = _InfoGalac;
            vResult.SectorDeNegocio = _SectorDeNegocio;
            vResult.CodigoLote = _CodigoLote;
            vResult.NivelDePrecioAsEnum = _NivelDePrecio;
            vResult.OrigenAsEnum = _Origen;
            vResult.DiaCumpleanos = _DiaCumpleanos;
            vResult.MesCumpleanos = _MesCumpleanos;
            vResult.CorrespondenciaXEnviarAsBool = _CorrespondenciaXEnviar;
            vResult.EsExtranjeroAsBool = _EsExtranjero;
            vResult.ClienteDesdeFecha = _ClienteDesdeFecha;
            vResult.AQueSeDedicaElCliente = _AQueSeDedicaElCliente;
            vResult.TipoDocumentoIdentificacionAsEnum = _TipoDocumentoIdentificacion;
            vResult.TipoDeContribuyenteAsEnum = _TipoDeContribuyente;
            vResult.CampoDefinible1 = _CampoDefinible1;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nCódigo = " + _Codigo +
               "\nNombre = " + _Nombre +
               "\nN° R.I.F. = " + _NumeroRIF +
               "\nN° N.I.T. = " + _NumeroNIT +
               "\nDirección = " + _Direccion +
               "\nCiudad = " + _Ciudad +
               "\nZona Postal = " + _ZonaPostal +
               "\nTeléfonos = " + _Telefono +
               "\nNº Fax = " + _FAX +
               "\nStatus = " + _Status.ToString() +
               "\nContacto = " + _Contacto +
               "\nZona De Cobranza = " + _ZonaDeCobranza +
               "\nCódigo del Vendedor = " + _CodigoVendedor +
               "\nRazon Inactividad = " + _RazonInactividad +
               "\nEmail = " + _Email +
               "\nActivar Aviso Al Escoger = " + _ActivarAvisoAlEscoger +
               "\nTexto Del Aviso = " + _TextoDelAviso +
               "\nCuenta Contable Cx C = " + _CuentaContableCxC +
               "\nCuenta Contable Ingresos = " + _CuentaContableIngresos +
               "\nCuenta Contable Anticipo = " + _CuentaContableAnticipo +
               "\nInfo Galac = " + _InfoGalac +
               "\nSector De Negocio = " + _SectorDeNegocio +
               "\nCodigo Lote = " + _CodigoLote +
               "\nNivel De Precio = " + _NivelDePrecio.ToString() +
               "\nOrigen = " + _Origen.ToString() +
               "\nDia Cumpleanos = " + _DiaCumpleanos.ToString() +
               "\nMes Cumpleanos = " + _MesCumpleanos.ToString() +
               "\nCorrespondencia XEnviar = " + _CorrespondenciaXEnviar +
               "\nEs Extranjero = " + _EsExtranjero +
               "\nCliente Desde = " + _ClienteDesdeFecha.ToShortDateString() +
               "\nA Que Se Dedica El Cliente = " + _AQueSeDedicaElCliente +
               "\nTipo Documento Identificacion = " + _TipoDocumentoIdentificacion.ToString() +
               "\nTipo De Contribuyente = " + _TipoDeContribuyente.ToString() +
               "\nCampo Definible 1 = " + _CampoDefinible1 +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class Cliente

} //End of namespace Galac.Saw.Ccl.Cliente

