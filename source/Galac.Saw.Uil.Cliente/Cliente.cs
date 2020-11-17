using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Security.Permissions;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.Cliente;

namespace Galac.Saw.Uil.Cliente {
    public class Cliente: LibMROMF {
        #region Variables
        ILibBusinessComponent _Reglas;
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
        #endregion //Propiedades
        #region Constructores

        public Cliente(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Parsing

        public void Parse(XmlReader valXml) {
            if (valXml != null) {
                XDocument xDoc = XDocument.Load(valXml);
                var vEntity = from vRecord in xDoc.Descendants("GpResult")
                              select vRecord;
                foreach (XElement vItem in vEntity) {
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null)) {
                        this.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Codigo"), null)) {
                        this.Codigo = vItem.Element("Codigo").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Nombre"), null)) {
                        this.Nombre = vItem.Element("Nombre").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroRIF"), null)) {
                        this.NumeroRIF = vItem.Element("NumeroRIF").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroNIT"), null)) {
                        this.NumeroNIT = vItem.Element("NumeroNIT").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Direccion"), null)) {
                        this.Direccion = vItem.Element("Direccion").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Ciudad"), null)) {
                        this.Ciudad = vItem.Element("Ciudad").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ZonaPostal"), null)) {
                        this.ZonaPostal = vItem.Element("ZonaPostal").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Telefono"), null)) {
                        this.Telefono = vItem.Element("Telefono").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("FAX"), null)) {
                        this.FAX = vItem.Element("FAX").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Status"), null)) {
                        this.Status = (eStatusCliente)LibConvert.DbValueToEnum(vItem.Element("Status"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Contacto"), null)) {
                        this.Contacto = vItem.Element("Contacto").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ZonaDeCobranza"), null)) {
                        this.ZonaDeCobranza = vItem.Element("ZonaDeCobranza").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoVendedor"), null)) {
                        this.CodigoVendedor = vItem.Element("CodigoVendedor").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("RazonInactividad"), null)) {
                        this.RazonInactividad = vItem.Element("RazonInactividad").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Email"), null)) {
                        this.Email = vItem.Element("Email").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ActivarAvisoAlEscoger"), null)) {
                        this.ActivarAvisoAlEscoger = LibConvert.SNToBool(vItem.Element("ActivarAvisoAlEscoger"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("TextoDelAviso"), null)) {
                        this.TextoDelAviso = vItem.Element("TextoDelAviso").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaContableCxC"), null)) {
                        this.CuentaContableCxC = vItem.Element("CuentaContableCxC").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaContableIngresos"), null)) {
                        this.CuentaContableIngresos = vItem.Element("CuentaContableIngresos").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaContableAnticipo"), null)) {
                        this.CuentaContableAnticipo = vItem.Element("CuentaContableAnticipo").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("SectorDeNegocio"), null)) {
                        this.SectorDeNegocio = vItem.Element("SectorDeNegocio").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoLote"), null)) {
                        this.CodigoLote = vItem.Element("CodigoLote").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("NivelDePrecio"), null)) {
                        this.NivelDePrecio = (eNivelDePrecio)LibConvert.DbValueToEnum(vItem.Element("NivelDePrecio"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Origen"), null)) {
                        this.Origen = (eOrigenFacturacionOManual)LibConvert.DbValueToEnum(vItem.Element("Origen"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("DiaCumpleanos"), null)) {
                        this.DiaCumpleanos = LibConvert.ToInt(vItem.Element("DiaCumpleanos"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("MesCumpleanos"), null)) {
                        this.MesCumpleanos = LibConvert.ToInt(vItem.Element("MesCumpleanos"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CorrespondenciaXEnviar"), null)) {
                        this.CorrespondenciaXEnviar = LibConvert.SNToBool(vItem.Element("CorrespondenciaXEnviar"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("EsExtranjero"), null)) {
                        this.EsExtranjero = LibConvert.SNToBool(vItem.Element("EsExtranjero"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ClienteDesdeFecha"), null)) {
                        this.ClienteDesdeFecha = LibConvert.ToDate(vItem.Element("ClienteDesdeFecha"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("NombreOperador"), null)) {
                        this.NombreOperador = vItem.Element("NombreOperador").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDocumentoIdentificacion"), null)) {
                        this.TipoDocumentoIdentificacion = (eTipoDocumentoIdentificacion)LibConvert.DbValueToEnum(vItem.Element("TipoDocumentoIdentificacion"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("FechaUltimaModificacion"), null)) {
                        this.FechaUltimaModificacion = LibConvert.ToDate(vItem.Element("FechaUltimaModificacion"));
                    }
                    this.fldTimeStamp = LibConvert.ToLong(vItem.Element("fldTimeStampBigint").Value);
                }
            }
        }

        XmlReader ParseToXml(Cliente valEntidad) {
            List<Cliente> vListEntidades = new List<Cliente>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", Mfc.GetInt("Compania")),
                    new XElement("Codigo", vEntity.Codigo),
                    new XElement("Nombre", vEntity.Nombre),
                    new XElement("NumeroRIF", vEntity.NumeroRIF),
                    new XElement("NumeroNIT", vEntity.NumeroNIT),
                    new XElement("Direccion", vEntity.Direccion),
                    new XElement("Ciudad", vEntity.Ciudad),
                    new XElement("ZonaPostal", vEntity.ZonaPostal),
                    new XElement("Telefono", vEntity.Telefono),
                    new XElement("FAX", vEntity.FAX),
                    new XElement("Status", vEntity.StatusAsDB),
                    new XElement("Contacto", vEntity.Contacto),
                    new XElement("ZonaDeCobranza", vEntity.ZonaDeCobranza),
                    new XElement("CodigoVendedor", vEntity.CodigoVendedor),
                    new XElement("RazonInactividad", vEntity.RazonInactividad),
                    new XElement("Email", vEntity.Email),
                    new XElement("ActivarAvisoAlEscoger", LibConvert.BoolToSN(vEntity.ActivarAvisoAlEscoger)),
                    new XElement("TextoDelAviso", vEntity.TextoDelAviso),
                    new XElement("CuentaContableCxC", vEntity.CuentaContableCxC),
                    new XElement("CuentaContableIngresos", vEntity.CuentaContableIngresos),
                    new XElement("CuentaContableAnticipo", vEntity.CuentaContableAnticipo),
                    new XElement("SectorDeNegocio", vEntity.SectorDeNegocio),
                    new XElement("CodigoLote", vEntity.CodigoLote),
                    new XElement("NivelDePrecio", vEntity.NivelDePrecioAsDB),
                    new XElement("Origen", vEntity.OrigenAsDB),
                    new XElement("DiaCumpleanos", vEntity.DiaCumpleanos),
                    new XElement("MesCumpleanos", vEntity.MesCumpleanos),
                    new XElement("CorrespondenciaXEnviar", LibConvert.BoolToSN(vEntity.CorrespondenciaXEnviar)),
                    new XElement("EsExtranjero", LibConvert.BoolToSN(vEntity.EsExtranjero)),
                    new XElement("ClienteDesdeFecha", vEntity.ClienteDesdeFecha),
                    new XElement("TipoDocumentoIdentificacion", vEntity.TipoDocumentoIdentificacionAsDB),
                    new XElement("fldTimeStamp", vEntity.fldTimeStamp)));
            XmlReader xmlReader = vXElement.CreateReader();
            return xmlReader;
        }
        #endregion //Parsing
        internal void Clear() {
            ConsecutivoCompania = Mfc.GetInt("Compania");
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

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponent)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Cliente.clsClienteNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        [PrincipalPermission(SecurityAction.Demand, Role = "Cliente.Insertar")]
        internal bool InsertRecord(out string outErrorMsg) {
            bool vResult = false;
            XmlReader vXmlRecord;
            LibResponse vResponse;
            if (ValidateAll(eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                vXmlRecord = ParseToXml(this);
                vResponse = _Reglas.DoAction(vXmlRecord, eAccionSR.Insertar, null);
                vResult = vResponse.Success;
                if (vResult) {
                    Parse(vResponse.Info);
                }
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cliente.Modificar")]
        internal bool UpdateRecord(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(valAction, out outErrorMessage)) {
                RegistraCliente();
                XmlReader vXmlRecord = ParseToXml(this);
                vResult = _Reglas.DoAction(vXmlRecord, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cliente.Eliminar")]
        internal bool DeleteRecord() {
            bool vResult = false;
            RegistraCliente();
            XmlReader vXmlRecord = ParseToXml(this);
            vResult = _Reglas.DoAction(vXmlRecord, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(int valConsecutivoCompania, string valCodigo) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Codigo", valCodigo, 10);
            XmlReader vXmlDoc = _Reglas.GetData(eProcessMessageType.SpName, "ClienteGET", vParams.Get());
            Parse(vXmlDoc);
        }

        public string GenerarProximoCodigo() {
            string vResult = "";
            RegistraCliente();
            XmlReader vData = _Reglas.GetData(eProcessMessageType.Message, "ProximoCodigo", Mfc.GetIntAsParam("Compania"));
            LibXmlDataParse insParser = new LibXmlDataParse(vData);
            if (insParser.CanRead()) {
                vResult = insParser.GetString(0, "Codigo", vResult);
            }
            insParser.Dispose();
            return vResult;
        }

        public bool ValidateAll(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigo(valAction, Codigo, false);
            vResult = IsValidNombre(valAction, Nombre, false) && vResult;
            vResult = IsValidNumeroRIF(valAction, NumeroRIF, false) && vResult;
            vResult = IsValidNumeroNIT(valAction, NumeroNIT, false) && vResult;
            vResult = IsValidClienteDesdeFecha(valAction, ClienteDesdeFecha, false) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidCodigo(eAccionSR valAction, string valCodigo, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            valCodigo = LibString.Trim(valCodigo);
            if (LibString.IsNullOrEmpty(valCodigo, true)) {
                BuildValidationInfo(MsgRequiredField("Código"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidNombre(eAccionSR valAction, string valNombre, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            valNombre = LibString.Trim(valNombre);
            if (LibString.IsNullOrEmpty(valNombre, true)) {
                BuildValidationInfo(MsgRequiredField("Nombre"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidNumeroRIF(eAccionSR valAction, string valNumeroRIF, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            valNumeroRIF = LibString.Trim(valNumeroRIF);
            if (LibString.IsNullOrEmpty(valNumeroRIF, true)) {
                BuildValidationInfo(MsgRequiredField("N° R.I.F."));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidNumeroNIT(eAccionSR valAction, string valNumeroNIT, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            valNumeroNIT = LibString.Trim(valNumeroNIT);
            if (LibString.IsNullOrEmpty(valNumeroNIT, true)) {
                BuildValidationInfo(MsgRequiredField("N° N.I.T."));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidClienteDesdeFecha(eAccionSR valAction, DateTime valClienteDesdeFecha, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valClienteDesdeFecha, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class Cliente

} //End of namespace Galac.Saw.Uil.Cliente

