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
    public class PlanillaDeIVAStt : ISettDefinition {
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
        private bool _ImprimirCentimosEnPlanilla;
        private eModeloPlanillaForma00030 _ModeloPlanillaForma00030;
        private string _NombreContador;
        private string _CedulaContador;
        private string _NumeroCPC;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public bool ImprimirCentimosEnPlanillaAsBool {
            get { return _ImprimirCentimosEnPlanilla; }
            set { _ImprimirCentimosEnPlanilla = value; }
        }

        public string ImprimirCentimosEnPlanilla {
            set { _ImprimirCentimosEnPlanilla = LibConvert.SNToBool(value); }
        }


        public eModeloPlanillaForma00030 ModeloPlanillaForma00030AsEnum {
            get { return _ModeloPlanillaForma00030; }
            set { _ModeloPlanillaForma00030 = value; }
        }

        public string ModeloPlanillaForma00030 {
            set { _ModeloPlanillaForma00030 = (eModeloPlanillaForma00030)LibConvert.DbValueToEnum(value); }
        }

        public string ModeloPlanillaForma00030AsDB {
            get { return LibConvert.EnumToDbValue((int) _ModeloPlanillaForma00030); }
        }

        public string ModeloPlanillaForma00030AsString {
            get { return LibEnumHelper.GetDescription(_ModeloPlanillaForma00030); }
        }

        public string NombreContador {
            get { return _NombreContador; }
            set { _NombreContador = LibString.Mid(value, 0, 20); }
        }

        public string CedulaContador {
            get { return _CedulaContador; }
            set { _CedulaContador = LibString.Mid(value, 0, 10); }
        }

        public string NumeroCPC {
            get { return _NumeroCPC; }
            set { _NumeroCPC = LibString.Mid(value, 0, 10); }
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

        public PlanillaDeIVAStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ImprimirCentimosEnPlanillaAsBool = false;
            ModeloPlanillaForma00030AsEnum = eModeloPlanillaForma00030.F00030_F03_Grafibond;
            NombreContador = "";
            CedulaContador = "";
            NumeroCPC = "";
            fldTimeStamp = 0;
        }

        public PlanillaDeIVAStt Clone() {
            PlanillaDeIVAStt vResult = new PlanillaDeIVAStt();
            vResult.ImprimirCentimosEnPlanillaAsBool = _ImprimirCentimosEnPlanilla;
            vResult.ModeloPlanillaForma00030AsEnum = _ModeloPlanillaForma00030;
            vResult.NombreContador = _NombreContador;
            vResult.CedulaContador = _CedulaContador;
            vResult.NumeroCPC = _NumeroCPC;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Imprimir Céntimos en Planilla Forma 00030 = " + _ImprimirCentimosEnPlanilla +
               "\nModelo Planilla Forma 00030 = " + _ModeloPlanillaForma00030.ToString() +
               "\nNombre Contador = " + _NombreContador +
               "\nCédula Contador = " + _CedulaContador +
               "\nNúmero CPC = " + _NumeroCPC;
        }
        #endregion //Metodos Generados


    } //End of class PlanillaDeIVAStt

} //End of namespace Galac.Saw.Ccl.SttDef

