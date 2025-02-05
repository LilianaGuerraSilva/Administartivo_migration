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
    public class FacturaCobroFacturaStt : ISettDefinition {
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
        private bool _EmitirDirecto;
        private bool _UsaCobroDirecto;
        private bool _UsaCobroDirectoEnMultimoneda;
        private string _CuentaBancariaCobroDirecto;
        private bool _UsaMediosElectronicosDeCobro;
        private string _ConceptoBancarioCobroDirecto;
        private string _CuentaBancariaCobroMultimoneda;
        private string _ConceptoBancarioCobroMultimoneda;
        private bool _UsaCreditoElectronico;
        private string _NombreCreditoElectronico;
        private int _DiasDeCreditoPorCuotaCreditoElectronico;
        private int _CantidadCuotasUsualesCreditoElectronico;
        private int _MaximaCantidadCuotasCreditoElectronico;
        private bool _UsaClienteUnicoCreditoElectronico;
        private string _CodigoClienteCreditoElectronico;
        private bool _GenerarUnaUnicaCuotaCreditoElectronico;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades
        public bool EmitirDirectoAsBool {
            get { return _EmitirDirecto; }
            set { _EmitirDirecto = value; }
        }

        public string EmitirDirecto {
            set { _EmitirDirecto = LibConvert.SNToBool(value); }
        }

        public bool UsaCobroDirectoAsBool {
            get { return _UsaCobroDirecto; }
            set { _UsaCobroDirecto = value; }
        }

        public string UsaCobroDirecto {
            set { _UsaCobroDirecto = LibConvert.SNToBool(value); }
        }


        public bool UsaCobroDirectoEnMultimonedaAsBool {
            get { return _UsaCobroDirectoEnMultimoneda; }
            set { _UsaCobroDirectoEnMultimoneda = value; }
        }

        public string UsaCobroDirectoEnMultimoneda {
            set { _UsaCobroDirectoEnMultimoneda = LibConvert.SNToBool(value); }
        }


        public string CuentaBancariaCobroDirecto {
            get { return _CuentaBancariaCobroDirecto; }
            set { _CuentaBancariaCobroDirecto = LibString.Mid(value, 0, 5); }
        }

        public bool UsaMediosElectronicosDeCobroAsBool {
            get { return _UsaMediosElectronicosDeCobro; }
            set { _UsaMediosElectronicosDeCobro = value; }
        }

        public string UsaMediosElectronicosDeCobro {
            set { _UsaMediosElectronicosDeCobro = LibConvert.SNToBool(value); }
        }


        public string ConceptoBancarioCobroDirecto {
            get { return _ConceptoBancarioCobroDirecto; }
            set { _ConceptoBancarioCobroDirecto = LibString.Mid(value, 0, 8); }
        }

        public string CuentaBancariaCobroMultimoneda {
            get { return _CuentaBancariaCobroMultimoneda; }
            set { _CuentaBancariaCobroMultimoneda = LibString.Mid(value, 0, 5); }
        }

        public string ConceptoBancarioCobroMultimoneda {
            get { return _ConceptoBancarioCobroMultimoneda; }
            set { _ConceptoBancarioCobroMultimoneda = LibString.Mid(value, 0, 8); }
        }

        public bool UsaCreditoElectronicoAsBool {
            get { return _UsaCreditoElectronico; }
            set { _UsaCreditoElectronico = value; }
        }

        public string UsaCreditoElectronico {
            set { _UsaCreditoElectronico = LibConvert.SNToBool(value); }
        }


        public string NombreCreditoElectronico {
            get { return _NombreCreditoElectronico; }
            set { _NombreCreditoElectronico = LibString.Mid(value, 0, 20); }
        }

        public int DiasDeCreditoPorCuotaCreditoElectronico {
            get { return _DiasDeCreditoPorCuotaCreditoElectronico; }
            set { _DiasDeCreditoPorCuotaCreditoElectronico = value; }
        }

        public int CantidadCuotasUsualesCreditoElectronico {
            get { return _CantidadCuotasUsualesCreditoElectronico; }
            set { _CantidadCuotasUsualesCreditoElectronico = value; }
        }

        public int MaximaCantidadCuotasCreditoElectronico {
            get { return _MaximaCantidadCuotasCreditoElectronico; }
            set { _MaximaCantidadCuotasCreditoElectronico = value; }
        }

        public bool UsaClienteUnicoCreditoElectronicoAsBool {
            get { return _UsaClienteUnicoCreditoElectronico; }
            set { _UsaClienteUnicoCreditoElectronico = value; }
        }

        public string UsaClienteUnicoCreditoElectronico {
            set { _UsaClienteUnicoCreditoElectronico = LibConvert.SNToBool(value); }
        }


        public string CodigoClienteCreditoElectronico {
            get { return _CodigoClienteCreditoElectronico; }
            set { _CodigoClienteCreditoElectronico = LibString.Mid(value, 0, 20); }
        }

        public bool GenerarUnaUnicaCuotaCreditoElectronicoAsBool {
            get { return _GenerarUnaUnicaCuotaCreditoElectronico; }
            set { _GenerarUnaUnicaCuotaCreditoElectronico = value; }
        }

        public string GenerarUnaUnicaCuotaCreditoElectronico {
            set { _GenerarUnaUnicaCuotaCreditoElectronico = LibConvert.SNToBool(value); }
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
        public FacturaCobroFacturaStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            EmitirDirectoAsBool = false;
            UsaCobroDirectoAsBool = false;
            UsaCobroDirectoEnMultimonedaAsBool = false;
            CuentaBancariaCobroDirecto = string.Empty;
            UsaMediosElectronicosDeCobroAsBool = false;
            ConceptoBancarioCobroDirecto = string.Empty;
            CuentaBancariaCobroMultimoneda = string.Empty;
            ConceptoBancarioCobroMultimoneda = string.Empty;
            UsaCreditoElectronicoAsBool = false;
            NombreCreditoElectronico = string.Empty;
            DiasDeCreditoPorCuotaCreditoElectronico = 0;
            CantidadCuotasUsualesCreditoElectronico = 0;
            MaximaCantidadCuotasCreditoElectronico = 0;
            UsaClienteUnicoCreditoElectronicoAsBool = false;
            CodigoClienteCreditoElectronico = string.Empty;
            GenerarUnaUnicaCuotaCreditoElectronicoAsBool = false;
            fldTimeStamp = 0;
        }

        public FacturaCobroFacturaStt Clone() {
            FacturaCobroFacturaStt vResult = new FacturaCobroFacturaStt();
            vResult.EmitirDirectoAsBool = _EmitirDirecto;
            vResult.UsaCobroDirectoAsBool = _UsaCobroDirecto;
            vResult.UsaCobroDirectoEnMultimonedaAsBool = _UsaCobroDirectoEnMultimoneda;
            vResult.CuentaBancariaCobroDirecto = _CuentaBancariaCobroDirecto;
            vResult.UsaMediosElectronicosDeCobroAsBool = _UsaMediosElectronicosDeCobro;
            vResult.ConceptoBancarioCobroDirecto = _ConceptoBancarioCobroDirecto;
            vResult.CuentaBancariaCobroMultimoneda = _CuentaBancariaCobroMultimoneda;
            vResult.ConceptoBancarioCobroMultimoneda = _ConceptoBancarioCobroMultimoneda;
            vResult.UsaCreditoElectronicoAsBool = _UsaCreditoElectronico;
            vResult.NombreCreditoElectronico = _NombreCreditoElectronico;
            vResult.DiasDeCreditoPorCuotaCreditoElectronico = _DiasDeCreditoPorCuotaCreditoElectronico;
            vResult.CantidadCuotasUsualesCreditoElectronico = _CantidadCuotasUsualesCreditoElectronico;
            vResult.MaximaCantidadCuotasCreditoElectronico = _MaximaCantidadCuotasCreditoElectronico;
            vResult.UsaClienteUnicoCreditoElectronicoAsBool = _UsaClienteUnicoCreditoElectronico;
            vResult.CodigoClienteCreditoElectronico = _CodigoClienteCreditoElectronico;
            vResult.GenerarUnaUnicaCuotaCreditoElectronicoAsBool = _GenerarUnaUnicaCuotaCreditoElectronico;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
            return "Emitir Directo = " + _EmitirDirecto +
                "\nUsa Cobro Directo = " + _UsaCobroDirecto +
                "\nUsa Cobro Directo en Multimoneda = " + _UsaCobroDirectoEnMultimoneda +
                "\nCuenta Bancaria Cobro Directo = " + _CuentaBancariaCobroDirecto +
                "\nUsa Medios Electrónicos de Cobro = " + _UsaMediosElectronicosDeCobro +
                "\nConcepto Bancario = " + _ConceptoBancarioCobroDirecto +
                "\nCuenta Bancaria Cobro en Multimoneda = " + _CuentaBancariaCobroMultimoneda +
                "\nConcepto Bancario para Cobro en Multimoneda = " + _ConceptoBancarioCobroMultimoneda +
                "\nUsar Crédito Electrónico = " + _UsaCreditoElectronico +
                "\nNombre para Crédito Electrónico = " + _NombreCreditoElectronico +
               "\nDías de crédito por cuota = " + _DiasDeCreditoPorCuotaCreditoElectronico.ToString() +
               "\nCuotas usuales del crédito = " + _CantidadCuotasUsualesCreditoElectronico.ToString() +
               "\nMáxima cantidad de cuotas = " + _MaximaCantidadCuotasCreditoElectronico.ToString() +
               "\nUsa cliente único = " + _UsaClienteUnicoCreditoElectronico +
               "\nCódigo Cliente único = " + _CodigoClienteCreditoElectronico +
               "\nGenerar una única cuota = " + _GenerarUnaUnicaCuotaCreditoElectronico;
        }
        #endregion //Metodos Generados


    } //End of class FacturaCobroFactura

} //End of namespace Galac.Comun.Ccl.SttDef