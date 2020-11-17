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
    public class UrbanizacionZP: LibMRO {
        #region Variables
        ILibBusinessComponent _Reglas;
        private string _Urbanizacion;
        private string _UrbanizacionOriginal;
        private string _ZonaPostal;
        private long _fldTimeStamp;
        #endregion //Variables
        #region Propiedades

        public string MessageName {
            get { return "Urbanización - Zona Postal"; }
        }

        public string Urbanizacion {
            get { return _Urbanizacion; }
            set { _Urbanizacion = LibString.Mid(value, 0, 30); }
        }

        public string UrbanizacionOriginal {
            get { return _UrbanizacionOriginal; }
            set { _UrbanizacionOriginal = LibString.Mid(value, 0, 30); }
        }

        public string ZonaPostal {
            get { return _ZonaPostal; }
            set { _ZonaPostal = LibString.Mid(value, 0, 7); }
        }

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public UrbanizacionZP() {
        }
        public UrbanizacionZP(string initUrbanizacion) {
            FindAndSetObject(initUrbanizacion);
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
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Urbanizacion"), null)) {
                        this.Urbanizacion = vItem.Element("Urbanizacion").Value;
                        this.UrbanizacionOriginal = this.Urbanizacion;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ZonaPostal"), null)) {
                        this.ZonaPostal = vItem.Element("ZonaPostal").Value;
                    }
                    this.fldTimeStamp = LibConvert.ToLong(vItem.Element("fldTimeStampBigint").Value);
                }
            }
        }

        XmlReader ParseToXml(UrbanizacionZP valEntidad) {
            List<UrbanizacionZP> vListEntidades = new List<UrbanizacionZP>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("Urbanizacion", vEntity.Urbanizacion),
                    new XElement("UrbanizacionOriginal", vEntity.UrbanizacionOriginal),
                    new XElement("ZonaPostal", vEntity.ZonaPostal),
                    new XElement("fldTimeStamp", vEntity.fldTimeStamp)));
            XmlReader xmlReader = vXElement.CreateReader();
            return xmlReader;
        }
        #endregion //Parsing
        internal void Clear() {
            Urbanizacion = "";
            ZonaPostal = "";
            fldTimeStamp = 0;
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponent)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Tablas.clsUrbanizacionZPNav();
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

        public void FindAndSetObject(string valUrbanizacion) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Urbanizacion", valUrbanizacion, 30);
            XmlReader vXmlDoc = _Reglas.GetData(eProcessMessageType.SpName, "UrbanizacionZPGET", vParams.Get());
            Parse(vXmlDoc);
        }

        public bool ValidateAll(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidUrbanizacion(valAction, Urbanizacion, false);
            vResult = IsValidZonaPostal(valAction, ZonaPostal, false) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidUrbanizacion(eAccionSR valAction, string valUrbanizacion, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            valUrbanizacion = LibString.Trim(valUrbanizacion);
            if (LibString.IsNullOrEmpty(valUrbanizacion, true)) {
                BuildValidationInfo(MsgRequiredField("Urbanización"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidZonaPostal(eAccionSR valAction, string valZonaPostal, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            valZonaPostal = LibString.Trim(valZonaPostal);
            if (LibString.IsNullOrEmpty(valZonaPostal, true)) {
                BuildValidationInfo(MsgRequiredField("Zona Postal"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class UrbanizacionZP

} //End of namespace Galac.Saw.Uil.Tablas

