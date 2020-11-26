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
    public class GeneralStt : ISettDefinition {
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
        private bool _PermitirEditarIVAenCxC_CxP;
        private bool _UsaMultiplesAlicuotas;
        private eFormaDeOrdenarCodigos _OrdenamientoDeCodigoString;
        private bool _ImprimirComprobanteDeCxC;
        private bool _ImprimirComprobanteDeCxP;
        private bool _ValidarRifEnLaWeb;
        private bool _EsSistemaParaIG;
        private bool _UsaNotaEntrega;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public bool PermitirEditarIVAenCxC_CxPAsBool {
            get { return _PermitirEditarIVAenCxC_CxP; }
            set { _PermitirEditarIVAenCxC_CxP = value; }
        }

        public string PermitirEditarIVAenCxC_CxP {
            set { _PermitirEditarIVAenCxC_CxP = LibConvert.SNToBool(value); }
        }


        public bool UsaMultiplesAlicuotasAsBool {
            get { return _UsaMultiplesAlicuotas; }
            set { _UsaMultiplesAlicuotas = value; }
        }

        public string UsaMultiplesAlicuotas {
            set { _UsaMultiplesAlicuotas = LibConvert.SNToBool(value); }
        }


        public eFormaDeOrdenarCodigos OrdenamientoDeCodigoStringAsEnum {
            get { return _OrdenamientoDeCodigoString; }
            set { _OrdenamientoDeCodigoString = value; }
        }

        public string OrdenamientoDeCodigoString {
            set { _OrdenamientoDeCodigoString = (eFormaDeOrdenarCodigos)LibConvert.DbValueToEnum(value); }
        }

        public string OrdenamientoDeCodigoStringAsDB {
            get { return LibConvert.EnumToDbValue((int) _OrdenamientoDeCodigoString); }
        }

        public string OrdenamientoDeCodigoStringAsString {
            get { return LibEnumHelper.GetDescription(_OrdenamientoDeCodigoString); }
        }

        public bool ImprimirComprobanteDeCxCAsBool {
            get { return _ImprimirComprobanteDeCxC; }
            set { _ImprimirComprobanteDeCxC = value; }
        }

        public string ImprimirComprobanteDeCxC {
            set { _ImprimirComprobanteDeCxC = LibConvert.SNToBool(value); }
        }


        public bool ImprimirComprobanteDeCxPAsBool {
            get { return _ImprimirComprobanteDeCxP; }
            set { _ImprimirComprobanteDeCxP = value; }
        }

        public string ImprimirComprobanteDeCxP {
            set { _ImprimirComprobanteDeCxP = LibConvert.SNToBool(value); }
        }


        public bool ValidarRifEnLaWebAsBool {
            get { return _ValidarRifEnLaWeb; }
            set { _ValidarRifEnLaWeb = value; }
        }

        public string ValidarRifEnLaWeb {
            set { _ValidarRifEnLaWeb = LibConvert.SNToBool(value); }
        }


        public bool EsSistemaParaIGAsBool {
            get { return _EsSistemaParaIG; }
            set { _EsSistemaParaIG = value; }
        }

        public string EsSistemaParaIG {
            set { _EsSistemaParaIG = LibConvert.SNToBool(value); }
        }

        public bool UsaNotaEntregaAsBool {
            get { return _UsaNotaEntrega; }
            set { _UsaNotaEntrega = value; }
        }

        public string UsaNotaEntrega {
            set { _UsaNotaEntrega = LibConvert.SNToBool(value); }
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

        public GeneralStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            PermitirEditarIVAenCxC_CxPAsBool = false;
            UsaMultiplesAlicuotasAsBool = false;
            OrdenamientoDeCodigoStringAsEnum = eFormaDeOrdenarCodigos.NORMAL;
            ImprimirComprobanteDeCxCAsBool = false;
            ImprimirComprobanteDeCxPAsBool = false;
            ValidarRifEnLaWebAsBool = false;
            EsSistemaParaIGAsBool = false;
            UsaNotaEntregaAsBool = false;
            fldTimeStamp = 0;
        }

        public GeneralStt Clone() {
            GeneralStt vResult = new GeneralStt();
            vResult.PermitirEditarIVAenCxC_CxPAsBool = _PermitirEditarIVAenCxC_CxP;
            vResult.UsaMultiplesAlicuotasAsBool = _UsaMultiplesAlicuotas;
            vResult.OrdenamientoDeCodigoStringAsEnum = _OrdenamientoDeCodigoString;
            vResult.ImprimirComprobanteDeCxCAsBool = _ImprimirComprobanteDeCxC;
            vResult.ImprimirComprobanteDeCxPAsBool = _ImprimirComprobanteDeCxP;
            vResult.ValidarRifEnLaWebAsBool = _ValidarRifEnLaWeb;
            vResult.EsSistemaParaIGAsBool = _EsSistemaParaIG;
            vResult.UsaNotaEntregaAsBool = _UsaNotaEntrega;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
            return "Permitir Editar IVAen Cx C_ Cx P = " + _PermitirEditarIVAenCxC_CxP +
                "\nUsa Multiples Alicuotas = " + _UsaMultiplesAlicuotas +
                "\nOrdenamiento De Codigo String = " + _OrdenamientoDeCodigoString.ToString() +
                "\nImprimir Comprobante De Cx C = " + _ImprimirComprobanteDeCxC +
                "\nImprimir Comprobante De Cx P = " + _ImprimirComprobanteDeCxP +
                "\nValidar Rif En La Web = " + _ValidarRifEnLaWeb +
                "\nEs Sistema Para IG = " + _EsSistemaParaIG +
                "\nUsa Nota Entrega = " + _UsaNotaEntrega;
        }
        #endregion //Metodos Generados


    } //End of class GeneralStt

} //End of namespace Galac.Saw.Ccl.SttDef

