using System;
using System.Xml;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.SttDef {
    [Serializable]
    public class FacturaImprentaDigitalStt : ISettDefinition {
        #region Variables
        private bool _UsaImprentaDigital;
        private DateTime _FechaInicioImprentaDigital;
        private eProveedorImprentaDigital _ProveedorImprentaDigital;
        XmlDocument _datos;
        private string _GroupName = null;
        private string _Module = null;
        #endregion //Variables
        #region Propiedades

        public string GroupName {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        public string Module {
            get { return _Module; }
            set { _Module = value; }
        }

        public bool UsaImprentaDigitalAsBool {
            get { return _UsaImprentaDigital; }
            set { _UsaImprentaDigital = value; }
        }

        public string UsaImprentaDigital {
            set { _UsaImprentaDigital = LibConvert.SNToBool(value); }
        }

        public DateTime FechaInicioImprentaDigital {
            get { return _FechaInicioImprentaDigital; }
            set { _FechaInicioImprentaDigital = LibConvert.DateToDbValue(value); }
        }

        public eProveedorImprentaDigital ProveedorImprentaDigitalAsEnum {
            get { return _ProveedorImprentaDigital; }
            set { _ProveedorImprentaDigital = value; }
        }

        public string ProveedorImprentaDigital {
            set { _ProveedorImprentaDigital = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(value); }
        }

        public string ProveedorImprentaDigitalAsDB {
            get { return LibConvert.EnumToDbValue((int)_ProveedorImprentaDigital); }
        }

        public string ProveedorImprentaDigitalAsString {
            get { return LibEnumHelper.GetDescription(_ProveedorImprentaDigital); }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public FacturaImprentaDigitalStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public void Clear() {
            UsaImprentaDigitalAsBool = false;
            FechaInicioImprentaDigital = LibDate.MinDateForDB();
            ProveedorImprentaDigitalAsEnum = eProveedorImprentaDigital.NoAplica;
        }

        public FacturaImprentaDigitalStt Clone() {
            FacturaImprentaDigitalStt vResult = new FacturaImprentaDigitalStt();
            vResult.UsaImprentaDigitalAsBool = _UsaImprentaDigital;
            vResult.FechaInicioImprentaDigital = _FechaInicioImprentaDigital;
            vResult.ProveedorImprentaDigitalAsEnum = _ProveedorImprentaDigital;
            return vResult;
        }

        public override string ToString() {
            return "Usa Imprenta Digital = " + _UsaImprentaDigital +
                "\nFecha de inicio del uso de Imprenta Digital = " + _FechaInicioImprentaDigital.ToShortDateString() +
                "\nProveedor de Imprenta Digital = " + _ProveedorImprentaDigital.ToString();
        }
        #endregion //Metodos Generados
    } //End of class FacturaImprentaDigitalSttf
} //End of namespace Galac.Comun.Ccl.SttDef

