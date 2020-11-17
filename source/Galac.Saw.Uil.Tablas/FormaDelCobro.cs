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

namespace Galac.Saw.Uil.Tablas {
    public class FormaDelCobro: LibMRO {
        #region Variables
        ILibBusinessComponent _Reglas;
        private string _Codigo;
        private string _Nombre;
        private eTipoDeFormaDePago _TipoDePago;
        private long _fldTimeStamp;
        #endregion //Variables
        #region Propiedades

        public string MessageName {
            get { return "Forma Del Cobro"; }
        }

        public string Codigo {
            get { return _Codigo; }
            set { _Codigo = LibString.Mid(value, 0, 5); }
        }

        public string Nombre {
            get { return _Nombre; }
            set { _Nombre = LibString.Mid(value, 0, 50); }
        }

        public eTipoDeFormaDePago TipoDePago {
            get { return _TipoDePago; }
            set { _TipoDePago = value; }
        }

        public string TipoDePagoAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDePago); }
        }

        public string TipoDePagoAsString {
            get { return LibEnumHelper.GetDescription(_TipoDePago); }
        }

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public FormaDelCobro() {
        }
        public FormaDelCobro(string initCodigo) {
            FindAndSetObject(initCodigo);
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
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Codigo"), null)) {
                        this.Codigo = vItem.Element("Codigo").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Nombre"), null)) {
                        this.Nombre = vItem.Element("Nombre").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDePago"), null)) {
                        this.TipoDePago = (eTipoDeFormaDePago)LibConvert.DbValueToEnum(vItem.Element("TipoDePago"));
                    }
                    this.fldTimeStamp = LibConvert.ToLong(vItem.Element("fldTimeStampBigint").Value);
                }
            }
        }

        XmlReader ParseToXml(FormaDelCobro valEntidad) {
            List<FormaDelCobro> vListEntidades = new List<FormaDelCobro>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("Codigo", vEntity.Codigo),
                    new XElement("Nombre", vEntity.Nombre),
                    new XElement("TipoDePago", vEntity.TipoDePagoAsDB),
                    new XElement("fldTimeStamp", vEntity.fldTimeStamp)));
            XmlReader xmlReader = vXElement.CreateReader();
            return xmlReader;
        }
        #endregion //Parsing
        internal void Clear() {
            Codigo = "";
            Nombre = "";
            TipoDePago = eTipoDeFormaDePago.Efectivo;
            fldTimeStamp = 0;
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponent)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Tablas.clsFormaDelCobroNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Modificar")]
        internal bool UpdateRecord(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(valAction, out outErrorMessage)) {
                RegistraCliente();
                XmlReader vXmlRecord = ParseToXml(this);
                vResult = _Reglas.DoAction(vXmlRecord, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Eliminar")]
        internal bool DeleteRecord() {
            bool vResult = false;
            RegistraCliente();
            XmlReader vXmlRecord = ParseToXml(this);
            vResult = _Reglas.DoAction(vXmlRecord, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(string valCodigo) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Codigo", valCodigo, 5);
            XmlReader vXmlDoc = _Reglas.GetData(eProcessMessageType.SpName, "FormaDelCobroGET", vParams.Get());
            Parse(vXmlDoc);
        }

        public string GenerarProximoCodigo() {
            string vResult = "";
            RegistraCliente();
            XmlReader vData = _Reglas.GetData(eProcessMessageType.Message, "ProximoCodigo", null);
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
        #endregion //Metodos Generados


    } //End of class FormaDelCobro

} //End of namespace Galac.Saw.Uil.Tablas

