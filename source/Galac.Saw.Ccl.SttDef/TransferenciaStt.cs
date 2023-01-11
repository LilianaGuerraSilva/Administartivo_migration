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
    public class TransferenciaStt : ISettDefinition {
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
        private string _ConceptoBancarioReversoTransfIngreso;
        private string _ConceptoBancarioReversoTransfEgreso;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string ConceptoBancarioReversoTransfIngreso {
            get { return _ConceptoBancarioReversoTransfIngreso; }
            set { _ConceptoBancarioReversoTransfIngreso = LibString.Mid(value, 0, 10); }
        }

        public string ConceptoBancarioReversoTransfEgreso {
            get { return _ConceptoBancarioReversoTransfEgreso; }
            set { _ConceptoBancarioReversoTransfEgreso = LibString.Mid(value, 0, 10); }
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

        public TransferenciaStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConceptoBancarioReversoTransfIngreso = string.Empty;
            ConceptoBancarioReversoTransfEgreso = string.Empty;
            fldTimeStamp = 0;
        }

        public TransferenciaStt Clone() {
            TransferenciaStt vResult = new TransferenciaStt();
            vResult.ConceptoBancarioReversoTransfIngreso = _ConceptoBancarioReversoTransfIngreso;
            vResult.ConceptoBancarioReversoTransfEgreso = _ConceptoBancarioReversoTransfEgreso;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Concepto Bancario Reverso Transferencia Ingreso = " + _ConceptoBancarioReversoTransfIngreso +
               "\nConcepto Bancario Reverso Transferencia Egreso = " + _ConceptoBancarioReversoTransfEgreso;
        }
        #endregion //Metodos Generados


    } //End of class BancosTransferencia

} //End of namespace Galac.Comun.Ccl.SttDef

