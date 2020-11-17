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
    public class Vehiculo: LibMROMF {
        #region Variables
        ILibBusinessComponent _Reglas;
        private int _ConsecutivoCompania;
        private int _Consecutivo;
        private string _Placa;
        private string _serialVIN;
        private string _NombreModelo;
        private string _Marca;
        private int _Ano;
        private string _CodigoColor;
        private string _DescripcionColor;
        private string _CodigoCliente;
        private string _NombreCliente;
        private string _RIFCliente;
        private string _NumeroPoliza;
        private string _SerialMotor;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        #endregion //Variables
        #region Propiedades

        public string MessageName {
            get { return "Vehiculo"; }
        }

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int Consecutivo {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }

        public string Placa {
            get { return _Placa; }
            set { _Placa = LibString.Mid(value, 0, 20); }
        }

        public string serialVIN {
            get { return _serialVIN; }
            set { _serialVIN = LibString.Mid(value, 0, 40); }
        }

        public string NombreModelo {
            get { return _NombreModelo; }
            set { _NombreModelo = LibString.Mid(value, 0, 20); }
        }

        public string Marca {
            get { return _Marca; }
            set { _Marca = LibString.Mid(value, 0, 20); }
        }

        public int Ano {
            get { return _Ano; }
            set { _Ano = value; }
        }

        public string CodigoColor {
            get { return _CodigoColor; }
            set { _CodigoColor = LibString.Mid(value, 0, 3); }
        }
        public string DescripcionColor {
            get { return _DescripcionColor; }
            set { _DescripcionColor = LibString.Mid(value, 0, 20); }
        }

        public string CodigoCliente {
            get { return _CodigoCliente; }
            set { _CodigoCliente = LibString.Mid(value, 0, 10); }
        }

        public string NombreCliente {
            get { return _NombreCliente; }
            set { _NombreCliente = LibString.Mid(value, 0, 80); }
        }

        public string RIFCliente {
            get { return _RIFCliente; }
            set { _RIFCliente = LibString.Mid(value, 0, 80); }
        }

        public string NumeroPoliza {
            get { return _NumeroPoliza; }
            set { _NumeroPoliza = LibString.Mid(value, 0, 20); }
        }

        public string SerialMotor {
            get { return _SerialMotor; }
            set { _SerialMotor = LibString.Mid(value, 0, 40); }
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

        public Vehiculo(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
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
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null)) {
                        this.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Placa"), null)) {
                        this.Placa = vItem.Element("Placa").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("serialVIN"), null)) {
                        this.serialVIN = vItem.Element("serialVIN").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("NombreModelo"), null)) {
                        this.NombreModelo = vItem.Element("NombreModelo").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Marca"), null)) {
                        this.Marca = vItem.Element("Marca").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Ano"), null)) {
                        this.Ano = LibConvert.ToInt(vItem.Element("Ano"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoColor"), null)) {
                        this.CodigoColor = vItem.Element("CodigoColor").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("DescripcionColor"), null)) {
                        this.DescripcionColor = vItem.Element("DescripcionColor").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoCliente"), null)) {
                        this.CodigoCliente = vItem.Element("CodigoCliente").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("NombreCliente"), null)) {
                        this.NombreCliente = vItem.Element("NombreCliente").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("RIFCliente"), null)) {
                        this.RIFCliente = vItem.Element("RIFCliente").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroPoliza"), null)) {
                        this.NumeroPoliza = vItem.Element("NumeroPoliza").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("SerialMotor"), null)) {
                        this.SerialMotor = vItem.Element("SerialMotor").Value;
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

        XmlReader ParseToXml(Vehiculo valEntidad) {
            List<Vehiculo> vListEntidades = new List<Vehiculo>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", Mfc.GetInt("Compania")),
                    new XElement("Consecutivo", vEntity.Consecutivo),
                    new XElement("Placa", vEntity.Placa),
                    new XElement("serialVIN", vEntity.serialVIN),
                    new XElement("NombreModelo", vEntity.NombreModelo),
                    new XElement("Marca", vEntity.Marca),
                    new XElement("Ano", vEntity.Ano),
                    new XElement("CodigoColor", vEntity.CodigoColor),
                    new XElement("DescripcionColor", vEntity.DescripcionColor),
                    new XElement("CodigoCliente", vEntity.CodigoCliente),
                    new XElement("NombreCliente", vEntity.NombreCliente),
                    new XElement("RIFCliente", vEntity.RIFCliente),
                    new XElement("NumeroPoliza", vEntity.NumeroPoliza),
                    new XElement("SerialMotor", vEntity.SerialMotor),
                    new XElement("fldTimeStamp", vEntity.fldTimeStamp)));
            XmlReader xmlReader = vXElement.CreateReader();
            return xmlReader;
        }
        #endregion //Parsing
        internal void Clear() {
            ConsecutivoCompania = Mfc.GetInt("Compania");
            Consecutivo = 0;
            Placa = "";
            serialVIN = "";
            NombreModelo = "";
            Marca = "";
            Ano = 0;
            CodigoColor = "";
            DescripcionColor = "";
            CodigoCliente = "";
            NombreCliente = "";
            RIFCliente = "";
            NumeroPoliza = "";
            SerialMotor = "";
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponent)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Vehiculo.clsVehiculoNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        [PrincipalPermission(SecurityAction.Demand, Role = "Vehiculo.Insertar")]
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Vehiculo.Modificar")]
        internal bool UpdateRecord(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(valAction, out outErrorMessage)) {
                RegistraCliente();
                XmlReader vXmlRecord = ParseToXml(this);
                vResult = _Reglas.DoAction(vXmlRecord, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Vehiculo.Eliminar")]
        internal bool DeleteRecord() {
            bool vResult = false;
            RegistraCliente();
            XmlReader vXmlRecord = ParseToXml(this);
            vResult = _Reglas.DoAction(vXmlRecord, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(int valConsecutivoCompania, int valConsecutivo) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valConsecutivo);
            XmlReader vXmlDoc = _Reglas.GetData(eProcessMessageType.SpName, "VehiculoGET", vParams.Get());
            Parse(vXmlDoc);
        }

        public bool ValidateAll(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidPlaca(valAction, Placa, false);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidPlaca(eAccionSR valAction, string valPlaca, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            valPlaca = LibString.Trim(valPlaca);
            if (LibString.IsNullOrEmpty(valPlaca, true)) {
                BuildValidationInfo(MsgRequiredField("Placa"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class Vehiculo

} //End of namespace Galac.Saw.Uil.Vehiculo

