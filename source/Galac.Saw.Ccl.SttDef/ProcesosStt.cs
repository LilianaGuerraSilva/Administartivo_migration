using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Ccl.SttDef {
    [Serializable]
    public class ProcesosStt : ISettDefinition {
        private string _GroupName = null;
        private string _Module = null;

        public string GroupName {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        public string Module {
            get { return _Module; }
            set { _Module = value; }
        }
        #region Variables
        private bool _SeResincronizaronLosSupervisores;
        private bool _InsertandoPorPrimeraVez;
        private string _CodigoEmpresaIntegrada;        
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public bool SeResincronizaronLosSupervisoresAsBool {
            get { return _SeResincronizaronLosSupervisores; }
            set { _SeResincronizaronLosSupervisores = value; }
        }

        public string SeResincronizaronLosSupervisores {
            set { _SeResincronizaronLosSupervisores = LibConvert.SNToBool(value); }
        }


        public bool InsertandoPorPrimeraVezAsBool {
            get { return _InsertandoPorPrimeraVez; }
            set { _InsertandoPorPrimeraVez = value; }
        }

        public string InsertandoPorPrimeraVez {
            set { _InsertandoPorPrimeraVez = LibConvert.SNToBool(value); }
        }


        public string CodigoEmpresaIntegrada {
            get { return _CodigoEmpresaIntegrada; }
            set { _CodigoEmpresaIntegrada = LibString.Mid(value, 0, 50); }
        }      

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public ProcesosStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            SeResincronizaronLosSupervisoresAsBool = false;
            InsertandoPorPrimeraVezAsBool = false;
            CodigoEmpresaIntegrada = "";           
            fldTimeStamp = 0;
        }

        public ProcesosStt Clone() {
            ProcesosStt vResult = new ProcesosStt();
            vResult.SeResincronizaronLosSupervisoresAsBool = _SeResincronizaronLosSupervisores;
            vResult.InsertandoPorPrimeraVezAsBool = _InsertandoPorPrimeraVez;
            vResult.CodigoEmpresaIntegrada = _CodigoEmpresaIntegrada;            
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
            return "Se Resincronizaron Los Supervisores = " + _SeResincronizaronLosSupervisores +
                "\nInsertando Por Primera Vez = " + _InsertandoPorPrimeraVez +
                "\nCodigoEmpresaIntegrada = " + _CodigoEmpresaIntegrada;
        }
        #endregion //Metodos Generados
    } //End of class ProcesosStt

} //End of namespace Galac.Saw.Ccl.SttDef

