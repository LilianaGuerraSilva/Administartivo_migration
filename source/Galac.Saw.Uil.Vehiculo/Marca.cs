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

namespace Galac.Saw.Uil.Vehiculo {
    public class Marca: LibMRO {
        #region Variables
        ILibBusinessComponent _Reglas;
        private string _Nombre;
        private string _NombreOriginal;
        private long _fldTimeStamp;
        #endregion //Variables
        #region Propiedades

        public string MessageName {
            get { return "Marca"; }
        }

        public string Nombre {
            get { return _Nombre; }
            set { _Nombre = LibString.Mid(value, 0, 20); }
        }

        public string NombreOriginal {
            get { return _NombreOriginal; }
            set { _NombreOriginal = LibString.Mid(value, 0, 20); }
        }

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public Marca() {
        }
        public Marca(string initNombre) {
            FindAndSetObject(initNombre);
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
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Nombre"), null)) {
                        this.Nombre = vItem.Element("Nombre").Value;
                        this.NombreOriginal = this.Nombre;
                    }
                    this.fldTimeStamp = LibConvert.ToLong(vItem.Element("fldTimeStampBigint").Value);
                }
            }
        }

        XmlReader ParseToXml(Marca valEntidad) {
            List<Marca> vListEntidades = new List<Marca>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("Nombre", vEntity.Nombre),
                    new XElement("NombreOriginal", vEntity.NombreOriginal),
                    new XElement("fldTimeStamp", vEntity.fldTimeStamp)));
            XmlReader xmlReader = vXElement.CreateReader();
            return xmlReader;
        }
        #endregion //Parsing
        internal void Clear() {
            Nombre = "";
            fldTimeStamp = 0;
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponent)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Vehiculo.clsMarcaNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        [PrincipalPermission(SecurityAction.Demand, Role = "Marca.Insertar")]
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Marca.Modificar")]
        internal bool UpdateRecord(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(valAction, out outErrorMessage)) {
                RegistraCliente();
                XmlReader vXmlRecord = ParseToXml(this);
                vResult = _Reglas.DoAction(vXmlRecord, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Marca.Eliminar")]
        internal bool DeleteRecord() {
            bool vResult = false;
            RegistraCliente();
            XmlReader vXmlRecord = ParseToXml(this);
            vResult = _Reglas.DoAction(vXmlRecord, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(string valNombre) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Nombre", valNombre, 20);
            XmlReader vXmlDoc = _Reglas.GetData(eProcessMessageType.SpName, "MarcaGET", vParams.Get());
            Parse(vXmlDoc);
        }

        public bool ValidateAll(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidNombre(valAction, Nombre, false);
            outErrorMessage = Information.ToString();
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
        #endregion //Metodos Generados


    } //End of class Marca

} //End of namespace Galac.Saw.Uil.Vehiculo

