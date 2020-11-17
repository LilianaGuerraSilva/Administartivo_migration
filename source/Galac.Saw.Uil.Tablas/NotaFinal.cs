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
    public class NotaFinal: LibMROMF {
        #region Variables
        ILibBusinessComponent _Reglas;
        private int _ConsecutivoCompania;
        private string _CodigoDeLaNota;
        private string _Descripcion;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        #endregion //Variables
        #region Propiedades

        public string MessageName {
            get { return "Nota Final"; }
        }

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string CodigoDeLaNota {
            get { return _CodigoDeLaNota; }
            set { _CodigoDeLaNota = LibString.Mid(value, 0, 10); }
        }

        public string Descripcion {
            get { return _Descripcion; }
            set { _Descripcion = LibString.Mid(value, 0, 255); }
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

        public NotaFinal(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
        }

        public NotaFinal() {
           // TODO: Complete member initialization
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
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoDeLaNota"), null)) {
                        this.CodigoDeLaNota = vItem.Element("CodigoDeLaNota").Value;
                    }
                    if (!System.NullReferenceException.ReferenceEquals(vItem.Element("Descripcion"), null)) {
                        this.Descripcion = vItem.Element("Descripcion").Value;
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

        XmlReader ParseToXml(NotaFinal valEntidad) {
            List<NotaFinal> vListEntidades = new List<NotaFinal>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", Mfc.GetInt("Compania")),
                    new XElement("CodigoDeLaNota", vEntity.CodigoDeLaNota),
                    new XElement("Descripcion", vEntity.Descripcion),
                    new XElement("fldTimeStamp", vEntity.fldTimeStamp)));
            XmlReader xmlReader = vXElement.CreateReader();
            return xmlReader;
        }
        #endregion //Parsing
        internal void Clear() {
            ConsecutivoCompania = Mfc.GetInt("Compania");
            CodigoDeLaNota = "";
            Descripcion = "";
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponent)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Tablas.clsNotaFinalNav();
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

        public void FindAndSetObject(int valConsecutivoCompania, string valCodigoDeLaNota) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoDeLaNota", valCodigoDeLaNota, 10);
            XmlReader vXmlDoc = _Reglas.GetData(eProcessMessageType.SpName, "NotaFinalGET", vParams.Get());
            Parse(vXmlDoc);
        }

        public bool ValidateAll(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigoDeLaNota(valAction, CodigoDeLaNota, false);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidCodigoDeLaNota(eAccionSR valAction, string valCodigoDeLaNota, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            valCodigoDeLaNota = LibString.Trim(valCodigoDeLaNota);
            if (LibString.IsNullOrEmpty(valCodigoDeLaNota, true)) {
                BuildValidationInfo(MsgRequiredField("Codigo De La Nota"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class NotaFinal

} //End of namespace Galac.Saw.Uil.Tablas

