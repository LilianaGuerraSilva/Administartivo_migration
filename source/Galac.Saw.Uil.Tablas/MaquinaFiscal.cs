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
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Uil.Tablas {
    public class MaquinaFiscal: LibMROMF {
        #region Variables
        ILibBusinessComponent _Reglas;
        private int _ConsecutivoCompania;
        private string _ConsecutivoMaquinaFiscal;
        private string _Descripcion;
        private string _NumeroRegistro;
        private eStatusMaquinaFiscal _Status;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        #endregion //Variables
        #region Propiedades

        public string MessageName {
            get { return "Maquina Fiscal"; }
        }

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string ConsecutivoMaquinaFiscal {
            get { return _ConsecutivoMaquinaFiscal; }
            set { _ConsecutivoMaquinaFiscal = LibString.Mid(value, 0, 9); }
        }

        public string Descripcion {
            get { return _Descripcion; }
            set { _Descripcion = LibString.Mid(value, 0, 35); }
        }

        public string NumeroRegistro {
            get { return _NumeroRegistro; }
            set { _NumeroRegistro = LibString.Mid(value, 0, 20); }
        }

        public eStatusMaquinaFiscal Status {
            get { return _Status; }
            set { _Status = value; }
        }
        public string StatusAsDB {
            get { return LibConvert.EnumToDbValue((int) _Status); }
        }
        public string StatusAsString {
            get { return LibEnumHelper.GetDescription(_Status); }
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
        #endregion //Propiedades
        #region Constructores

        public MaquinaFiscal(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
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
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoMaquinaFiscal"), null)) {
                        this.ConsecutivoMaquinaFiscal = vItem.Element("ConsecutivoMaquinaFiscal").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Descripcion"), null)) {
                        this.Descripcion = vItem.Element("Descripcion").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroRegistro"), null)) {
                        this.NumeroRegistro = vItem.Element("NumeroRegistro").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Status"), null)) {
                        this.Status = (eStatusMaquinaFiscal)LibConvert.DbValueToEnum(vItem.Element("Status"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("NombreOperador"), null)) {
                        this.NombreOperador = vItem.Element("NombreOperador").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("FechaUltimaModificacion"), null)) {
                        this.FechaUltimaModificacion = LibConvert.ToDate(vItem.Element("FechaUltimaModificacion"));
                    }
                    this.fldTimeStamp = LibConvert.ToLong(vItem.Element("fldTimeStampBigint").Value);
                }
            }
        }

        XmlReader ParseToXml(MaquinaFiscal valEntidad) {
            List<MaquinaFiscal> vListEntidades = new List<MaquinaFiscal>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", Mfc.GetInt("Compania")),
                    new XElement("ConsecutivoMaquinaFiscal", vEntity.ConsecutivoMaquinaFiscal),
                    new XElement("Descripcion", vEntity.Descripcion),
                    new XElement("NumeroRegistro", vEntity.NumeroRegistro),
                    new XElement("Status", vEntity.StatusAsDB),
                    new XElement("fldTimeStamp", vEntity.fldTimeStamp)));
            XmlReader xmlReader = vXElement.CreateReader();
            return xmlReader;
        }
        #endregion //Parsing
        internal void Clear() {
            ConsecutivoCompania = Mfc.GetInt("Compania");
            ConsecutivoMaquinaFiscal = "";
            Descripcion = "";
            NumeroRegistro = "";
            Status = eStatusMaquinaFiscal.Activa;
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponent)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Tablas.clsMaquinaFiscalNav();
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

        public void FindAndSetObject(int valConsecutivoCompania, string valConsecutivoMaquinaFiscal) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("ConsecutivoMaquinaFiscal", valConsecutivoMaquinaFiscal, 9);
            XmlReader vXmlDoc = _Reglas.GetData(eProcessMessageType.SpName, "MaquinaFiscalGET", vParams.Get());
            Parse(vXmlDoc);
        }
        public string GenerarProximoConsecutivoMaquinaFiscal() {
            string vResult = "";
            RegistraCliente();
            XmlReader vData = _Reglas.GetData(eProcessMessageType.Message, "ProximoConsecutivoMaquinaFiscal", Mfc.GetIntAsParam("Compania"));
            LibXmlDataParse insParser = new LibXmlDataParse(vData);
            if (insParser.CanRead()) {
                vResult = insParser.GetString(0, "ConsecutivoMaquinaFiscal", vResult);
            }
            insParser.Dispose();
            return vResult;
        }

        public bool ValidateAll(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoMaquinaFiscal(valAction, ConsecutivoMaquinaFiscal, false);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidConsecutivoMaquinaFiscal(eAccionSR valAction, string valConsecutivoMaquinaFiscal, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            valConsecutivoMaquinaFiscal = LibString.Trim(valConsecutivoMaquinaFiscal);
            if (LibString.IsNullOrEmpty(valConsecutivoMaquinaFiscal, true)) {
                BuildValidationInfo(MsgRequiredField("Consecutivo"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class MaquinaFiscal

} //End of namespace Galac.Saw.Uil.Tablas

