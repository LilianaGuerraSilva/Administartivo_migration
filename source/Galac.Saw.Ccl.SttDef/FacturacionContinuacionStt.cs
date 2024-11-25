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
    public class FacturacionContinuacionStt : ISettDefinition {
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
        private bool _ForzarFechaFacturaAmesEspecifico;
        private eMes _MesFacturacionEnCurso;
        private bool _PermitirIncluirFacturacionHistorica;
        private DateTime _UltimaFechaDeFacturacionHistorica;
        private bool _GenerarCxCalEmitirUnaFacturaHistorica;
        private eAccionAlAnularFactDeMesesAnt _AccionAlAnularFactDeMesesAnt;
        private bool _UsarOtrosCargoDeFactura;
        private bool _UsaCamposExtrasEnRenglonFactura;
        //private bool _EmitirDirecto;
        //private bool _UsaCobroDirecto;
        //private bool _UsaCobroDirectoEnMultimoneda;
        //private string _CuentaBancariaCobroDirecto;
        //private string _ConceptoBancarioCobroDirecto;
        private bool _PermitirDobleDescuentoEnFactura;
        private decimal _MaximoDescuentoEnFactura;
        private eBloquearEmision _BloquearEmision;
        private bool _MostrarMtoTotalBsFEnObservaciones;
        //private string _CuentaBancariaCobroMultimoneda;
        //private string _ConceptoBancarioCobroMultimoneda;
        private bool _SeMuestraTotalEnDivisas;
        private bool _UsaListaDePrecioEnMonedaExtranjera;
        private bool _UsaListaDePrecioEnMonedaExtranjeraCXC;
        private int _NroDiasMantenerTasaCambio;
        //private bool _UsaMediosElectronicosDeCobro;
        //private bool _UsaCreditoElectronico;
        //private string _NombreCreditoElectronico;
        //private int _DiasUsualesCreditoElectronico;
        //private int _DiasMaximoCreditoElectronico;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public bool ForzarFechaFacturaAmesEspecificoAsBool {
            get { return _ForzarFechaFacturaAmesEspecifico; }
            set { _ForzarFechaFacturaAmesEspecifico = value; }
        }

        public string ForzarFechaFacturaAmesEspecifico {
            set { _ForzarFechaFacturaAmesEspecifico = LibConvert.SNToBool(value); }
        }


        public eMes MesFacturacionEnCursoAsEnum {
            get { return _MesFacturacionEnCurso; }
            set { _MesFacturacionEnCurso = value; }
        }

        public string MesFacturacionEnCurso {
            set { _MesFacturacionEnCurso = (eMes)LibConvert.DbValueToEnum(value); }
        }

        public string MesFacturacionEnCursoAsDB {
            get { return LibConvert.EnumToDbValue((int)_MesFacturacionEnCurso); }
        }

        public string MesFacturacionEnCursoAsString {
            get { return LibEnumHelper.GetDescription(_MesFacturacionEnCurso); }
        }

        public bool PermitirIncluirFacturacionHistoricaAsBool {
            get { return _PermitirIncluirFacturacionHistorica; }
            set { _PermitirIncluirFacturacionHistorica = value; }
        }

        public string PermitirIncluirFacturacionHistorica {
            set { _PermitirIncluirFacturacionHistorica = LibConvert.SNToBool(value); }
        }


        public DateTime UltimaFechaDeFacturacionHistorica {
            get { return _UltimaFechaDeFacturacionHistorica; }
            set { _UltimaFechaDeFacturacionHistorica = LibConvert.DateToDbValue(value); }
        }

        public bool GenerarCxCalEmitirUnaFacturaHistoricaAsBool {
            get { return _GenerarCxCalEmitirUnaFacturaHistorica; }
            set { _GenerarCxCalEmitirUnaFacturaHistorica = value; }
        }

        public string GenerarCxCalEmitirUnaFacturaHistorica {
            set { _GenerarCxCalEmitirUnaFacturaHistorica = LibConvert.SNToBool(value); }
        }


        public eAccionAlAnularFactDeMesesAnt AccionAlAnularFactDeMesesAntAsEnum {
            get { return _AccionAlAnularFactDeMesesAnt; }
            set { _AccionAlAnularFactDeMesesAnt = value; }
        }

        public string AccionAlAnularFactDeMesesAnt {
            set { _AccionAlAnularFactDeMesesAnt = (eAccionAlAnularFactDeMesesAnt)LibConvert.DbValueToEnum(value); }
        }

        public string AccionAlAnularFactDeMesesAntAsDB {
            get { return LibConvert.EnumToDbValue((int)_AccionAlAnularFactDeMesesAnt); }
        }

        public string AccionAlAnularFactDeMesesAntAsString {
            get { return LibEnumHelper.GetDescription(_AccionAlAnularFactDeMesesAnt); }
        }

        public bool UsarOtrosCargoDeFacturaAsBool {
            get { return _UsarOtrosCargoDeFactura; }
            set { _UsarOtrosCargoDeFactura = value; }
        }

        public string UsarOtrosCargoDeFactura {
            set { _UsarOtrosCargoDeFactura = LibConvert.SNToBool(value); }
        }


        public bool UsaCamposExtrasEnRenglonFacturaAsBool {
            get { return _UsaCamposExtrasEnRenglonFactura; }
            set { _UsaCamposExtrasEnRenglonFactura = value; }
        }

        public string UsaCamposExtrasEnRenglonFactura {
            set { _UsaCamposExtrasEnRenglonFactura = LibConvert.SNToBool(value); }
        }


        //public bool EmitirDirectoAsBool {
        //    get { return _EmitirDirecto; }
        //    set { _EmitirDirecto = value; }
        //}

        //public string EmitirDirecto {
        //    set { _EmitirDirecto = LibConvert.SNToBool(value); }
        //}

        //public bool UsaCobroDirectoAsBool {
        //    get { return _UsaCobroDirecto; }
        //    set { _UsaCobroDirecto = value; }
        //}

        //public string UsaCobroDirecto {
        //    set { _UsaCobroDirecto = LibConvert.SNToBool(value); }
        //}

        //public bool UsaCobroDirectoEnMultimonedaAsBool {
        //    get { return _UsaCobroDirectoEnMultimoneda; }
        //    set { _UsaCobroDirectoEnMultimoneda = value; }
        //}

        //public string UsaCobroDirectoEnMultimoneda {
        //    set { _UsaCobroDirectoEnMultimoneda = LibConvert.SNToBool(value); }
        //}

        //public string CuentaBancariaCobroDirecto {
        //    get { return _CuentaBancariaCobroDirecto; }
        //    set { _CuentaBancariaCobroDirecto = LibString.Mid(value, 0, 5); }
        //}

        //public string ConceptoBancarioCobroDirecto {
        //    get { return _ConceptoBancarioCobroDirecto; }
        //    set { _ConceptoBancarioCobroDirecto = LibString.Mid(value, 0, 8); }
        //}

        //public string CuentaBancariaCobroMultimoneda {
        //    get { return _CuentaBancariaCobroMultimoneda; }
        //    set { _CuentaBancariaCobroMultimoneda = LibString.Mid(value, 0, 5); }
        //}

        //public string ConceptoBancarioCobroMultimoneda {
        //    get { return _ConceptoBancarioCobroMultimoneda; }
        //    set { _ConceptoBancarioCobroMultimoneda = LibString.Mid(value, 0, 8); }
        //}

        public bool PermitirDobleDescuentoEnFacturaAsBool {
            get { return _PermitirDobleDescuentoEnFactura; }
            set { _PermitirDobleDescuentoEnFactura = value; }
        }

        public string PermitirDobleDescuentoEnFactura {
            set { _PermitirDobleDescuentoEnFactura = LibConvert.SNToBool(value); }
        }


        public decimal MaximoDescuentoEnFactura {
            get { return _MaximoDescuentoEnFactura; }
            set { _MaximoDescuentoEnFactura = value; }
        }

        public eBloquearEmision BloquearEmisionAsEnum {
            get { return _BloquearEmision; }
            set { _BloquearEmision = value; }
        }

        public string BloquearEmision {
            set { _BloquearEmision = (eBloquearEmision)LibConvert.DbValueToEnum(value); }
        }

        public string BloquearEmisionAsDB {
            get { return LibConvert.EnumToDbValue((int)_BloquearEmision); }
        }

        public string BloquearEmisionAsString {
            get { return LibEnumHelper.GetDescription(_BloquearEmision); }
        }

        public bool MostrarMtoTotalBsFEnObservacionesAsBool {
            get { return _MostrarMtoTotalBsFEnObservaciones; }
            set { _MostrarMtoTotalBsFEnObservaciones = value; }
        }

        public string MostrarMtoTotalBsFEnObservaciones {
            set { _MostrarMtoTotalBsFEnObservaciones = LibConvert.SNToBool(value); }
        }

        public bool SeMuestraTotalEnDivisasAsBool {
            get { return _SeMuestraTotalEnDivisas; }
            set { _SeMuestraTotalEnDivisas = value; }
        }

        public string SeMuestraTotalEnDivisas {
            set { _SeMuestraTotalEnDivisas = LibConvert.SNToBool(value); }
        }

        public bool UsaListaDePrecioEnMonedaExtranjeraAsBool {
            get { return _UsaListaDePrecioEnMonedaExtranjera; }
            set { _UsaListaDePrecioEnMonedaExtranjera = value; }
        }

        public string UsaListaDePrecioEnMonedaExtranjera {
            set { _UsaListaDePrecioEnMonedaExtranjera = LibConvert.SNToBool(value); }
        }

        public bool UsaListaDePrecioEnMonedaExtranjeraCXCAsBool {
            get { return _UsaListaDePrecioEnMonedaExtranjeraCXC; }
            set { _UsaListaDePrecioEnMonedaExtranjeraCXC = value; }
        }

        public int NroDiasMantenerTasaCambio {
            get { return _NroDiasMantenerTasaCambio; }
            set { _NroDiasMantenerTasaCambio = value; }
        }

        //public bool UsaMediosElectronicosDeCobroAsBool {
        //          get { return _UsaMediosElectronicosDeCobro; }
        //          set { _UsaMediosElectronicosDeCobro = value; }
        //      }

        //      public string UsaMediosElectronicosDeCobro {
        //          set { _UsaMediosElectronicosDeCobro = LibConvert.SNToBool(value); }
        //      }
        //      public bool UsaCreditoElectronicoAsBool { 
        //          get { return _UsaCreditoElectronico; } 
        //          set { _UsaCreditoElectronico = value; } 
        //      }
        //      public string UsaCreditoElectronico
        //      {
        //          set { _UsaCreditoElectronico = LibConvert.SNToBool(value); }
        //      }

        //      public string NombreCreditoElectronico
        //      {
        //          get { return _NombreCreditoElectronico; }
        //          set { _NombreCreditoElectronico = value; }
        //      }
        //      public int DiasUsualesCreditoElectronico
        //      {
        //          get { return _DiasUsualesCreditoElectronico; }
        //          set { _DiasUsualesCreditoElectronico = value; }
        //      }

        //      public int DiasMaximoCreditoElectronico
        //      {
        //          get { return _DiasMaximoCreditoElectronico; }
        //          set { _DiasMaximoCreditoElectronico = value; }
        //      }

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
        public FacturacionContinuacionStt() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ForzarFechaFacturaAmesEspecificoAsBool = false;
            MesFacturacionEnCursoAsEnum = eMes.Enero;
            PermitirIncluirFacturacionHistoricaAsBool = false;
            UltimaFechaDeFacturacionHistorica = LibDate.Today();
            GenerarCxCalEmitirUnaFacturaHistoricaAsBool = false;
            AccionAlAnularFactDeMesesAntAsEnum = eAccionAlAnularFactDeMesesAnt.PermitirAnularSinChequear;
            UsarOtrosCargoDeFacturaAsBool = false;
            UsaCamposExtrasEnRenglonFacturaAsBool = false;
            //EmitirDirectoAsBool = false;
            //UsaCobroDirectoAsBool = false;
            //UsaCobroDirectoEnMultimonedaAsBool = false;
            //CuentaBancariaCobroDirecto = "";
            //ConceptoBancarioCobroDirecto = "";
            //CuentaBancariaCobroMultimoneda = "";
            //ConceptoBancarioCobroMultimoneda = "";
            PermitirDobleDescuentoEnFacturaAsBool = false;
            MaximoDescuentoEnFactura = 0;
            BloquearEmisionAsEnum = eBloquearEmision.NoBloquear;
            MostrarMtoTotalBsFEnObservacionesAsBool = false;
            SeMuestraTotalEnDivisasAsBool = false;
            UsaListaDePrecioEnMonedaExtranjeraAsBool = false;
            UsaListaDePrecioEnMonedaExtranjeraCXCAsBool = false;
            NroDiasMantenerTasaCambio = 0;
            //UsaMediosElectronicosDeCobroAsBool = false;
            fldTimeStamp = 0;
        }

        public FacturacionContinuacionStt Clone() {
            FacturacionContinuacionStt vResult = new FacturacionContinuacionStt();
            vResult.ForzarFechaFacturaAmesEspecificoAsBool = _ForzarFechaFacturaAmesEspecifico;
            vResult.MesFacturacionEnCursoAsEnum = _MesFacturacionEnCurso;
            vResult.PermitirIncluirFacturacionHistoricaAsBool = _PermitirIncluirFacturacionHistorica;
            vResult.UltimaFechaDeFacturacionHistorica = _UltimaFechaDeFacturacionHistorica;
            vResult.GenerarCxCalEmitirUnaFacturaHistoricaAsBool = _GenerarCxCalEmitirUnaFacturaHistorica;
            vResult.AccionAlAnularFactDeMesesAntAsEnum = _AccionAlAnularFactDeMesesAnt;
            vResult.UsarOtrosCargoDeFacturaAsBool = _UsarOtrosCargoDeFactura;
            vResult.UsaCamposExtrasEnRenglonFacturaAsBool = _UsaCamposExtrasEnRenglonFactura;
            //vResult.EmitirDirectoAsBool = _EmitirDirecto;
            //vResult.UsaCobroDirectoAsBool = _UsaCobroDirecto;
            //vResult.UsaCobroDirectoEnMultimonedaAsBool = _UsaCobroDirectoEnMultimoneda;
            //vResult.CuentaBancariaCobroDirecto = _CuentaBancariaCobroDirecto;
            //vResult.ConceptoBancarioCobroDirecto = _ConceptoBancarioCobroDirecto;
            //vResult.CuentaBancariaCobroMultimoneda = _CuentaBancariaCobroMultimoneda;
            //vResult.ConceptoBancarioCobroMultimoneda = _ConceptoBancarioCobroMultimoneda;
            vResult.PermitirDobleDescuentoEnFacturaAsBool = _PermitirDobleDescuentoEnFactura;
            vResult.MaximoDescuentoEnFactura = _MaximoDescuentoEnFactura;
            vResult.BloquearEmisionAsEnum = _BloquearEmision;
            vResult.MostrarMtoTotalBsFEnObservacionesAsBool = _MostrarMtoTotalBsFEnObservaciones;
            vResult.SeMuestraTotalEnDivisasAsBool = _SeMuestraTotalEnDivisas;
            vResult.UsaListaDePrecioEnMonedaExtranjeraAsBool = _UsaListaDePrecioEnMonedaExtranjera;
            vResult.UsaListaDePrecioEnMonedaExtranjeraCXCAsBool = _UsaListaDePrecioEnMonedaExtranjeraCXC;
            vResult.NroDiasMantenerTasaCambio = _NroDiasMantenerTasaCambio;
            //vResult.UsaMediosElectronicosDeCobroAsBool = _UsaMediosElectronicosDeCobro;
            //         vResult.UsaCreditoElectronicoAsBool = _UsaCobroDirecto;
            //         vResult.NombreCreditoElectronico =_NombreCreditoElectronico;
            //         vResult.DiasUsualesCreditoElectronico = _DiasUsualesCreditoElectronico;
            //         vResult.DiasMaximoCreditoElectronico  = _DiasMaximoCreditoElectronico;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
            return "Forzar fecha Factura a mes específico ... = " + _ForzarFechaFacturaAmesEspecifico +
                "\nMes Facturacion En Curso = " + _MesFacturacionEnCurso.ToString() +
                "\nPermitir incluir Facturación Histórica .... = " + _PermitirIncluirFacturacionHistorica +
                "\nUltima fecha de Facturación Histórica ... = " + _UltimaFechaDeFacturacionHistorica.ToShortDateString() +
                "\nGenerarCxCalEmitirUnaFacturaHistorica = " + _GenerarCxCalEmitirUnaFacturaHistorica +
                "\nAcción Al Anular Factura de Meses Anteriores = " + _AccionAlAnularFactDeMesesAnt.ToString() +
                "\nUsar Otros Cargo de Factura ..... = " + _UsarOtrosCargoDeFactura +
                "\nUsar campos Extras en el Detalle de Fac = " + _UsaCamposExtrasEnRenglonFactura +
                "\nUsar Doble Descuento = " + _PermitirDobleDescuentoEnFactura +
                "\nMáximo de Descuento .... = " + _MaximoDescuentoEnFactura.ToString() +
                "\nSe muestra total en Divisas .... = " + _SeMuestraTotalEnDivisas +
                "\nUsar Lista De Precios En Moneda Extranjera = " + _UsaListaDePrecioEnMonedaExtranjera +
                "\nGenerar CxC En Moneda Extranjera = " + _UsaListaDePrecioEnMonedaExtranjeraCXC +
                "\nMostrar Total En Bolívares Fuertes = " + _MostrarMtoTotalBsFEnObservaciones +
                "\nNumero De Dias a Mantener Tasa De Cambio = " + _NroDiasMantenerTasaCambio;
        }
        #endregion //Metodos Generados


    } //End of class FacturacionContinuacionStt

} //End of namespace Galac.Saw.Ccl.SttDef