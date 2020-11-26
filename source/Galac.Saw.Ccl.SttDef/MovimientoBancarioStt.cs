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
    public class MovimientoBancarioStt : ISettDefinition {
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
        private bool _MandarMensajeNumeroDeMovimientoBancario;
        private bool _GenerarMovBancarioDesdeCobro;
        private bool _UsaCodigoConceptoBancarioEnPantalla;
        private bool _GenerarMovBancarioDesdePago;
        private int _NumCopiasComprobanteMovBancario;
        private string _NombrePlantillaComprobanteDeMovBancario;
        private bool _ConfirmarImpresionMovBancarioPorSecciones;
        private bool _ImprimirCompContDespuesDeChequeMovBancario;
        private bool _ImprimirComprobanteDeMovBancario;
        private int _BeneficiarioGenerico;
        private string _ConceptoBancarioReversoSolicitudDePago;
        private string _NombrePlantillaComprobanteDePagoSueldo;
        private bool _GenerarMovReversoSiAnulaPago;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public bool MandarMensajeNumeroDeMovimientoBancarioAsBool {
            get { return _MandarMensajeNumeroDeMovimientoBancario; }
            set { _MandarMensajeNumeroDeMovimientoBancario = value; }
        }

        public string MandarMensajeNumeroDeMovimientoBancario {
            set { _MandarMensajeNumeroDeMovimientoBancario = LibConvert.SNToBool(value); }
        }


        public bool GenerarMovBancarioDesdeCobroAsBool {
            get { return _GenerarMovBancarioDesdeCobro; }
            set { _GenerarMovBancarioDesdeCobro = value; }
        }

        public string GenerarMovBancarioDesdeCobro {
            set { _GenerarMovBancarioDesdeCobro = LibConvert.SNToBool(value); }
        }


        public bool UsaCodigoConceptoBancarioEnPantallaAsBool {
            get { return _UsaCodigoConceptoBancarioEnPantalla; }
            set { _UsaCodigoConceptoBancarioEnPantalla = value; }
        }

        public string UsaCodigoConceptoBancarioEnPantalla {
            set { _UsaCodigoConceptoBancarioEnPantalla = LibConvert.SNToBool(value); }
        }


        public bool GenerarMovBancarioDesdePagoAsBool {
            get { return _GenerarMovBancarioDesdePago; }
            set { _GenerarMovBancarioDesdePago = value; }
        }

        public string GenerarMovBancarioDesdePago {
            set { _GenerarMovBancarioDesdePago = LibConvert.SNToBool(value); }
        }


        public int NumCopiasComprobanteMovBancario {
            get { return _NumCopiasComprobanteMovBancario; }
            set { _NumCopiasComprobanteMovBancario = value; }
        }

        public string NombrePlantillaComprobanteDeMovBancario {
            get { return _NombrePlantillaComprobanteDeMovBancario; }
            set { _NombrePlantillaComprobanteDeMovBancario = LibString.Mid(value, 0, 60); }
        }

        public bool ConfirmarImpresionMovBancarioPorSeccionesAsBool {
            get { return _ConfirmarImpresionMovBancarioPorSecciones; }
            set { _ConfirmarImpresionMovBancarioPorSecciones = value; }
        }

        public string ConfirmarImpresionMovBancarioPorSecciones {
            set { _ConfirmarImpresionMovBancarioPorSecciones = LibConvert.SNToBool(value); }
        }


        public bool ImprimirCompContDespuesDeChequeMovBancarioAsBool {
            get { return _ImprimirCompContDespuesDeChequeMovBancario; }
            set { _ImprimirCompContDespuesDeChequeMovBancario = value; }
        }

        public string ImprimirCompContDespuesDeChequeMovBancario {
            set { _ImprimirCompContDespuesDeChequeMovBancario = LibConvert.SNToBool(value); }
        }


        public bool ImprimirComprobanteDeMovBancarioAsBool {
            get { return _ImprimirComprobanteDeMovBancario; }
            set { _ImprimirComprobanteDeMovBancario = value; }
        }

        public string ImprimirComprobanteDeMovBancario {
            set { _ImprimirComprobanteDeMovBancario = LibConvert.SNToBool(value); }
        }


        public int BeneficiarioGenerico {
            get { return _BeneficiarioGenerico; }
            set { _BeneficiarioGenerico = value; }
        }

        public string ConceptoBancarioReversoSolicitudDePago {
            get { return _ConceptoBancarioReversoSolicitudDePago; }
            set { _ConceptoBancarioReversoSolicitudDePago = LibString.Mid(value, 0, 8); }
        }

        public string NombrePlantillaComprobanteDePagoSueldo {
            get { return _NombrePlantillaComprobanteDePagoSueldo; }
            set { _NombrePlantillaComprobanteDePagoSueldo = LibString.Mid(value, 0, 50); }
        }

        public bool GenerarMovReversoSiAnulaPagoAsBool {
            get { return _GenerarMovReversoSiAnulaPago; }
            set { _GenerarMovReversoSiAnulaPago = value; }
        }

        public string GenerarMovReversoSiAnulaPago {
            set { _GenerarMovReversoSiAnulaPago = LibConvert.SNToBool(value); }
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

        public MovimientoBancarioStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            MandarMensajeNumeroDeMovimientoBancarioAsBool = false;
            GenerarMovBancarioDesdeCobroAsBool = false;
            UsaCodigoConceptoBancarioEnPantallaAsBool = false;
            GenerarMovBancarioDesdePagoAsBool = false;
            NumCopiasComprobanteMovBancario = 0;
            NombrePlantillaComprobanteDeMovBancario = "";
            ConfirmarImpresionMovBancarioPorSeccionesAsBool = false;
            ImprimirCompContDespuesDeChequeMovBancarioAsBool = false;
            ImprimirComprobanteDeMovBancarioAsBool = false;
            BeneficiarioGenerico = 0;
            ConceptoBancarioReversoSolicitudDePago = "";
            NombrePlantillaComprobanteDePagoSueldo = "";
            GenerarMovReversoSiAnulaPagoAsBool = false;
            fldTimeStamp = 0;
        }

        public MovimientoBancarioStt Clone() {
            MovimientoBancarioStt vResult = new MovimientoBancarioStt();
            vResult.MandarMensajeNumeroDeMovimientoBancarioAsBool = _MandarMensajeNumeroDeMovimientoBancario;
            vResult.GenerarMovBancarioDesdeCobroAsBool = _GenerarMovBancarioDesdeCobro;
            vResult.UsaCodigoConceptoBancarioEnPantallaAsBool = _UsaCodigoConceptoBancarioEnPantalla;
            vResult.GenerarMovBancarioDesdePagoAsBool = _GenerarMovBancarioDesdePago;
            vResult.NumCopiasComprobanteMovBancario = _NumCopiasComprobanteMovBancario;
            vResult.NombrePlantillaComprobanteDeMovBancario = _NombrePlantillaComprobanteDeMovBancario;
            vResult.ConfirmarImpresionMovBancarioPorSeccionesAsBool = _ConfirmarImpresionMovBancarioPorSecciones;
            vResult.ImprimirCompContDespuesDeChequeMovBancarioAsBool = _ImprimirCompContDespuesDeChequeMovBancario;
            vResult.ImprimirComprobanteDeMovBancarioAsBool = _ImprimirComprobanteDeMovBancario;
            vResult.BeneficiarioGenerico = _BeneficiarioGenerico;
            vResult.ConceptoBancarioReversoSolicitudDePago = _ConceptoBancarioReversoSolicitudDePago;
            vResult.NombrePlantillaComprobanteDePagoSueldo = _NombrePlantillaComprobanteDePagoSueldo;
            vResult.GenerarMovReversoSiAnulaPagoAsBool = _GenerarMovReversoSiAnulaPago;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Mandar Mensaje Numero De Movimiento Bancario = " + _MandarMensajeNumeroDeMovimientoBancario +
               "\nGenerar Mov Bancario Desde Cobro = " + _GenerarMovBancarioDesdeCobro +
               "\nUsa Codigo Concepto Bancario En Pantalla = " + _UsaCodigoConceptoBancarioEnPantalla +
               "\nGenerar Mov Bancario Desde Pago = " + _GenerarMovBancarioDesdePago +
               "\nNum Copias Comprobante Mov Bancario = " + _NumCopiasComprobanteMovBancario.ToString() +
               "\nNombre Plantilla Comprobante De Mov Bancario = " + _NombrePlantillaComprobanteDeMovBancario +
               "\nConfirmar Impresion Mov Bancario Por Secciones = " + _ConfirmarImpresionMovBancarioPorSecciones +
               "\nImprimir Comp Cont Despues De Cheque Mov Bancario = " + _ImprimirCompContDespuesDeChequeMovBancario +
               "\nImprimir Comprobante De Mov Bancario = " + _ImprimirComprobanteDeMovBancario +
               "\nBeneficiario Generico = " + _BeneficiarioGenerico.ToString() +
               "\nConceptoBancarioReversoSolicitudDePago = " + _ConceptoBancarioReversoSolicitudDePago +
               "\nNombre Plantilla Comprobante de PagoSueldo = " + _NombrePlantillaComprobanteDePagoSueldo +
               "\nGenerar Mov Reverso Si Anula Pago = " + _GenerarMovReversoSiAnulaPago;
        }
        #endregion //Metodos Generados


    } //End of class MovimientoBancarioStt

} //End of namespace Galac.Saw.Ccl.SttDef

