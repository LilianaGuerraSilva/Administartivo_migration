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
    public class PropAnalisisVenc: LibMRO {
        #region Variables
        ILibBusinessComponent _Reglas;
        private int _SecuencialUnique0;
        private int _PrimerVencimiento;
        private int _SegundoVencimiento;
        private int _TercerVencimiento;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        #endregion //Variables
        #region Propiedades

        public string MessageName {
            get { return "Prop Analisis Venc"; }
        }

        public int SecuencialUnique0 {
            get { return _SecuencialUnique0; }
            set { _SecuencialUnique0 = value; }
        }

        public int PrimerVencimiento {
            get { return _PrimerVencimiento; }
            set { _PrimerVencimiento = value; }
        }

        public int SegundoVencimiento {
            get { return _SegundoVencimiento; }
            set { _SegundoVencimiento = value; }
        }

        public int TercerVencimiento {
            get { return _TercerVencimiento; }
            set { _TercerVencimiento = value; }
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

        public PropAnalisisVenc() {
        }
        public PropAnalisisVenc(int initSecuencialUnique0) {
            FindAndSetObject(initSecuencialUnique0);
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
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("SecuencialUnique0"), null)) {
                        this.SecuencialUnique0 = LibConvert.ToInt(vItem.Element("SecuencialUnique0"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("PrimerVencimiento"), null)) {
                        this.PrimerVencimiento = LibConvert.ToInt(vItem.Element("PrimerVencimiento"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("SegundoVencimiento"), null)) {
                        this.SegundoVencimiento = LibConvert.ToInt(vItem.Element("SegundoVencimiento"));
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("TercerVencimiento"), null)) {
                        this.TercerVencimiento = LibConvert.ToInt(vItem.Element("TercerVencimiento"));
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

        XmlReader ParseToXml(PropAnalisisVenc valEntidad) {
            List<PropAnalisisVenc> vListEntidades = new List<PropAnalisisVenc>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("SecuencialUnique0", vEntity.SecuencialUnique0),
                    new XElement("PrimerVencimiento", vEntity.PrimerVencimiento),
                    new XElement("SegundoVencimiento", vEntity.SegundoVencimiento),
                    new XElement("TercerVencimiento", vEntity.TercerVencimiento),
                    new XElement("fldTimeStamp", vEntity.fldTimeStamp)));
            XmlReader xmlReader = vXElement.CreateReader();
            return xmlReader;
        }
        #endregion //Parsing
        internal void Clear() {
            SecuencialUnique0 = 0;
            PrimerVencimiento = 0;
            SegundoVencimiento = 0;
            TercerVencimiento = 0;
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponent)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Tablas.clsPropAnalisisVencNav();
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

        public void FindAndSetObject(int valSecuencialUnique0) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("SecuencialUnique0", valSecuencialUnique0);
            XmlReader vXmlDoc = _Reglas.GetData(eProcessMessageType.SpName, "PropAnalisisVencGET", vParams.Get());
            Parse(vXmlDoc);
        }

        public bool ValidateAll(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidPrimerVencimiento(valAction, PrimerVencimiento, false);
            vResult = IsValidSegundoVencimiento(valAction, SegundoVencimiento, false) && vResult;
            vResult = IsValidTercerVencimiento(valAction, TercerVencimiento, false) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidPrimerVencimiento(eAccionSR valAction, int valPrimerVencimiento, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valPrimerVencimiento == 0) {
                BuildValidationInfo(MsgRequiredField("Primer Vencimiento"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidSegundoVencimiento(eAccionSR valAction, int valSegundoVencimiento, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valSegundoVencimiento == 0) {
                BuildValidationInfo(MsgRequiredField("Segundo Vencimiento"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidTercerVencimiento(eAccionSR valAction, int valTercerVencimiento, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valTercerVencimiento == 0) {
                BuildValidationInfo(MsgRequiredField("Tercer Vencimiento"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class PropAnalisisVenc

} //End of namespace Galac.Saw.Uil.Tablas

