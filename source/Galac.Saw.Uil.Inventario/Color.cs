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

namespace Galac.Saw.Uil.Inventario {
    public class Color: LibMROMF {
        #region Variables
        ILibBusinessComponent _Reglas;
        private int _ConsecutivoCompania;
        private string _CodigoColor;
        private string _CodigoColorOriginal;
        private string _DescripcionColor;
        private string _CodigoLote;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        #endregion //Variables
        #region Propiedades

        public string MessageName {
            get { return "Color"; }
        }

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string CodigoColor {
            get { return _CodigoColor; }
            set { _CodigoColor = LibString.Mid(value, 0, 3); }
        }

        public string CodigoColorOriginal {
            get { return _CodigoColorOriginal; }
            set { _CodigoColorOriginal = LibString.Mid(value, 0, 3); }
        }
        public string DescripcionColor {
            get { return _DescripcionColor; }
            set { _DescripcionColor = LibString.Mid(value, 0, 20); }
        }

        public string CodigoLote {
            get { return _CodigoLote; }
            set { _CodigoLote = LibString.Mid(value, 0, 10); }
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

        public Color(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
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
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoColor"), null)) {
                        this.CodigoColor = vItem.Element("CodigoColor").Value;
                        this.CodigoColorOriginal  = vItem.Element("CodigoColor").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("DescripcionColor"), null)) {
                        this.DescripcionColor = vItem.Element("DescripcionColor").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoLote"), null)) {
                        this.CodigoLote = vItem.Element("CodigoLote").Value;
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

        XmlReader ParseToXml(Color valEntidad) {
            List<Color> vListEntidades = new List<Color>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", Mfc.GetInt("Compania")),
                    new XElement("CodigoColor", vEntity.CodigoColor),
                    new XElement("CodigoColorOriginal", vEntity.CodigoColorOriginal),
                    new XElement("DescripcionColor", vEntity.DescripcionColor),
                    new XElement("CodigoLote", vEntity.CodigoLote),
                    new XElement("fldTimeStamp", vEntity.fldTimeStamp)));
            XmlReader xmlReader = vXElement.CreateReader();
            return xmlReader;
        }
        #endregion //Parsing
        internal void Clear() {
            ConsecutivoCompania = Mfc.GetInt("Compania");
            CodigoColor = "";
            DescripcionColor = "";
            CodigoLote = "";
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponent)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Inventario.clsColorNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        [PrincipalPermission(SecurityAction.Demand, Role = "Color.Insertar")]
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Color.Modificar")]
        internal bool UpdateRecord(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(valAction, out outErrorMessage)) {
                RegistraCliente();
                XmlReader vXmlRecord = ParseToXml(this);
                vResult = _Reglas.DoAction(vXmlRecord, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Color.Eliminar")]
        internal bool DeleteRecord() {
            bool vResult = false;
            RegistraCliente();
            XmlReader vXmlRecord = ParseToXml(this);
            vResult = _Reglas.DoAction(vXmlRecord, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(int valConsecutivoCompania, string valCodigoColor) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoColor", valCodigoColor, 3);
            XmlReader vXmlDoc = _Reglas.GetData(eProcessMessageType.SpName, "ColorGET", vParams.Get());
            Parse(vXmlDoc);
        }

        public bool ValidateAll(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigoColor(valAction, CodigoColor, false);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidCodigoColor(eAccionSR valAction, string valCodigoColor, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            valCodigoColor = LibString.Trim(valCodigoColor);
            if (LibString.IsNullOrEmpty(valCodigoColor, true)) {
                BuildValidationInfo(MsgRequiredField("Codigo Color"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class Color

} //End of namespace Galac.Saw.Uil.Inventario

