using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Services;
using System.Xml;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.Cliente;

namespace Galac.Saw.Dal.Cliente {
    [Serializable]
    public class recCliente {
        #region Variables
        private int _ConsecutivoCompania;
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
        private string _SectorDeNegocio;
        private string _CodigoLote;
        private eNivelDePrecio _NivelDePrecio;
        private eOrigenFacturacionOManual _Origen;
        private int _DiaCumpleanos;
        private int _MesCumpleanos;
        private bool _CorrespondenciaXEnviar;
        private bool _EsExtranjero;
        private DateTime _ClienteDesdeFecha;
        private string _NombreOperador;
        private eTipoDocumentoIdentificacion _TipoDocumentoIdentificacion;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string MessageName {
            get { return "Cliente"; }
        }

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string Codigo {
            get { return _Codigo; }
            set { _Codigo = LibString.Mid(value, 0, 10); }
        }

        public string Nombre {
            get { return _Nombre; }
            set { _Nombre = LibString.Mid(value, 0, 80); }
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

        public eStatusCliente Status {
            get { return _Status; }
            set { _Status = value; }
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
            set { _Email = LibString.Mid(value, 0, 40); }
        }

        public bool ActivarAvisoAlEscoger {
            get { return _ActivarAvisoAlEscoger; }
            set { _ActivarAvisoAlEscoger = value; }
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

        public string SectorDeNegocio {
            get { return _SectorDeNegocio; }
            set { _SectorDeNegocio = LibString.Mid(value, 0, 20); }
        }

        public string CodigoLote {
            get { return _CodigoLote; }
            set { _CodigoLote = LibString.Mid(value, 0, 10); }
        }

        public eNivelDePrecio NivelDePrecio {
            get { return _NivelDePrecio; }
            set { _NivelDePrecio = value; }
        }

        public string NivelDePrecioAsDB {
            get { return LibConvert.EnumToDbValue((int) _NivelDePrecio); }
        }

        public string NivelDePrecioAsString {
            get { return LibEnumHelper.GetDescription(_NivelDePrecio); }
        }

        public eOrigenFacturacionOManual Origen {
            get { return _Origen; }
            set { _Origen = value; }
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

        public bool CorrespondenciaXEnviar {
            get { return _CorrespondenciaXEnviar; }
            set { _CorrespondenciaXEnviar = value; }
        }

        public bool EsExtranjero {
            get { return _EsExtranjero; }
            set { _EsExtranjero = value; }
        }

        public DateTime ClienteDesdeFecha {
            get { return _ClienteDesdeFecha; }
            set { _ClienteDesdeFecha = LibConvert.DateToDbValue(value); }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 10); }
        }

        public eTipoDocumentoIdentificacion TipoDocumentoIdentificacion {
            get { return _TipoDocumentoIdentificacion; }
            set { _TipoDocumentoIdentificacion = value; }
        }

        public string TipoDocumentoIdentificacionAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDocumentoIdentificacion); }
        }

        public string TipoDocumentoIdentificacionAsString {
            get { return LibEnumHelper.GetDescription(_TipoDocumentoIdentificacion); }
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
        public recCliente(){
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public recCliente Clone() {
            recCliente vResult = new recCliente();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Codigo = _Codigo;
            vResult.Nombre = _Nombre;
            vResult.NumeroRIF = _NumeroRIF;
            vResult.NumeroNIT = _NumeroNIT;
            vResult.Direccion = _Direccion;
            vResult.Ciudad = _Ciudad;
            vResult.ZonaPostal = _ZonaPostal;
            vResult.Telefono = _Telefono;
            vResult.FAX = _FAX;
            vResult.Status = _Status;
            vResult.Contacto = _Contacto;
            vResult.ZonaDeCobranza = _ZonaDeCobranza;
            vResult.CodigoVendedor = _CodigoVendedor;
            vResult.RazonInactividad = _RazonInactividad;
            vResult.Email = _Email;
            vResult.ActivarAvisoAlEscoger = _ActivarAvisoAlEscoger;
            vResult.TextoDelAviso = _TextoDelAviso;
            vResult.CuentaContableCxC = _CuentaContableCxC;
            vResult.CuentaContableIngresos = _CuentaContableIngresos;
            vResult.CuentaContableAnticipo = _CuentaContableAnticipo;
            vResult.SectorDeNegocio = _SectorDeNegocio;
            vResult.CodigoLote = _CodigoLote;
            vResult.NivelDePrecio = _NivelDePrecio;
            vResult.Origen = _Origen;
            vResult.DiaCumpleanos = _DiaCumpleanos;
            vResult.MesCumpleanos = _MesCumpleanos;
            vResult.CorrespondenciaXEnviar = _CorrespondenciaXEnviar;
            vResult.EsExtranjero = _EsExtranjero;
            vResult.ClienteDesdeFecha = _ClienteDesdeFecha;
            vResult.NombreOperador = _NombreOperador;
            vResult.TipoDocumentoIdentificacion = _TipoDocumentoIdentificacion;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
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
               "\nSector De Negocio = " + _SectorDeNegocio +
               "\nCodigo Lote = " + _CodigoLote +
               "\nNivel De Precio = " + _NivelDePrecio.ToString() +
               "\nOrigen = " + _Origen.ToString() +
               "\nDia Cumpleanos = " + _DiaCumpleanos.ToString() +
               "\nMes Cumpleanos = " + _MesCumpleanos.ToString() +
               "\nCorrespondencia XEnviar = " + _CorrespondenciaXEnviar +
               "\nEs Extranjero = " + _EsExtranjero +
               "\nCliente Desde = " + _ClienteDesdeFecha.ToShortDateString() +
               "\nNombre Operador = " + _NombreOperador +
               "\nTipo Documento Identificacion = " + _TipoDocumentoIdentificacion.ToString() +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            Codigo = "";
            Nombre = "";
            NumeroRIF = "";
            NumeroNIT = "";
            Direccion = "";
            Ciudad = "";
            ZonaPostal = "";
            Telefono = "";
            FAX = "";
            Status = eStatusCliente.Activo;
            Contacto = "";
            ZonaDeCobranza = "";
            CodigoVendedor = "";
            RazonInactividad = "";
            Email = "";
            ActivarAvisoAlEscoger = false;
            TextoDelAviso = "";
            CuentaContableCxC = "";
            CuentaContableIngresos = "";
            CuentaContableAnticipo = "";
            SectorDeNegocio = "";
            CodigoLote = "";
            NivelDePrecio = eNivelDePrecio.Precio1;
            Origen = eOrigenFacturacionOManual.Factura;
            DiaCumpleanos = 0;
            MesCumpleanos = 0;
            CorrespondenciaXEnviar = false;
            EsExtranjero = false;
            ClienteDesdeFecha = LibDate.Today();
            NombreOperador = "";
            TipoDocumentoIdentificacion = eTipoDocumentoIdentificacion.RUC;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public void Fill(XmlDocument refResulset, bool valSetCurrent) {
            _datos = refResulset;
            LibXmlDataParse insParser = new LibXmlDataParse(refResulset);
            if (valSetCurrent && insParser.Count() > 0) {
                Clear();
                ConsecutivoCompania = insParser.GetInt(0, "ConsecutivoCompania", ConsecutivoCompania);
                Codigo = insParser.GetString(0, "Codigo", Codigo);
                Nombre = insParser.GetString(0, "Nombre", Nombre);
                NumeroRIF = insParser.GetString(0, "NumeroRIF", NumeroRIF);
                NumeroNIT = insParser.GetString(0, "NumeroNIT", NumeroNIT);
                Direccion = insParser.GetString(0, "Direccion", Direccion);
                Ciudad = insParser.GetString(0, "Ciudad", Ciudad);
                ZonaPostal = insParser.GetString(0, "ZonaPostal", ZonaPostal);
                Telefono = insParser.GetString(0, "Telefono", Telefono);
                FAX = insParser.GetString(0, "FAX", FAX);
                Status = (eStatusCliente) insParser.GetEnum(0, "Status", (int) Status);
                Contacto = insParser.GetString(0, "Contacto", Contacto);
                ZonaDeCobranza = insParser.GetString(0, "ZonaDeCobranza", ZonaDeCobranza);
                CodigoVendedor = insParser.GetString(0, "CodigoVendedor", CodigoVendedor);
                RazonInactividad = insParser.GetString(0, "RazonInactividad", RazonInactividad);
                Email = insParser.GetString(0, "Email", Email);
                ActivarAvisoAlEscoger = insParser.GetBool(0, "ActivarAvisoAlEscoger", ActivarAvisoAlEscoger);
                TextoDelAviso = insParser.GetString(0, "TextoDelAviso", TextoDelAviso);
                CuentaContableCxC = insParser.GetString(0, "CuentaContableCxC", CuentaContableCxC);
                CuentaContableIngresos = insParser.GetString(0, "CuentaContableIngresos", CuentaContableIngresos);
                CuentaContableAnticipo = insParser.GetString(0, "CuentaContableAnticipo", CuentaContableAnticipo);
                SectorDeNegocio = insParser.GetString(0, "SectorDeNegocio", SectorDeNegocio);
                CodigoLote = insParser.GetString(0, "CodigoLote", CodigoLote);
                NivelDePrecio = (eNivelDePrecio) insParser.GetEnum(0, "NivelDePrecio", (int) NivelDePrecio);
                Origen = (eOrigenFacturacionOManual) insParser.GetEnum(0, "Origen", (int) Origen);
                DiaCumpleanos = insParser.GetInt(0, "DiaCumpleanos", DiaCumpleanos);
                MesCumpleanos = insParser.GetInt(0, "MesCumpleanos", MesCumpleanos);
                CorrespondenciaXEnviar = insParser.GetBool(0, "CorrespondenciaXEnviar", CorrespondenciaXEnviar);
                EsExtranjero = insParser.GetBool(0, "EsExtranjero", EsExtranjero);
                ClienteDesdeFecha = insParser.GetDateTime(0, "ClienteDesdeFecha", ClienteDesdeFecha);
                NombreOperador = insParser.GetString(0, "NombreOperador", NombreOperador);
                TipoDocumentoIdentificacion = (eTipoDocumentoIdentificacion) insParser.GetEnum(0, "TipoDocumentoIdentificacion", (int) TipoDocumentoIdentificacion);
                FechaUltimaModificacion = insParser.GetDateTime(0, "FechaUltimaModificacion", FechaUltimaModificacion);
                fldTimeStamp = insParser.GetTimeStamp(0);
            }
        }
        #endregion //Metodos Generados


    } //End of class recCliente

} //End of namespace Galac.Saw.Dal.Cliente

