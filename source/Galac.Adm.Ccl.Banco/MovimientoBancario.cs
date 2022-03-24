using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Ccl.Banco { 
    [Serializable]
    public class MovimientoBancario {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoMovimiento;
        private string _CodigoCtaBancaria;
        private string _NombreCtaBancaria;
        private string _CodigoConcepto;
        private string _DescripcionConcepto;
        private DateTime _Fecha;
        private eIngresoEgreso _TipoConcepto;
        private decimal _Monto;
        private string _NumeroDocumento;
        private string _Descripcion;
        private bool _GeneraImpuestoBancario;
        private decimal _AlicuotaImpBancario;
        private string _NroMovimientoRelacionado;
        private eGeneradoPor _GeneradoPor;
        private string _Moneda;
        private decimal _CambioABolivares;
        private bool _ImprimirCheque;
        private string _Beneficiario;
        private string _NumeroDeCheque;
        private bool _ConciliadoSN;
        private string _NroConciliacion;
        private bool _GenerarAsientoDeRetiroEnCuenta;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int ConsecutivoMovimiento {
            get { return _ConsecutivoMovimiento; }
            set { _ConsecutivoMovimiento = value; }
        }

        public string CodigoCtaBancaria {
            get { return _CodigoCtaBancaria; }
            set { _CodigoCtaBancaria = LibString.Mid(value, 0, 5); }
        }

        public string NombreCtaBancaria {
            get { return _NombreCtaBancaria; }
            set { _NombreCtaBancaria = LibString.Mid(value, 0, 40); }
        }

        public string CodigoConcepto {
            get { return _CodigoConcepto; }
            set { _CodigoConcepto = LibString.Mid(value, 0, 8); }
        }

        public string DescripcionConcepto {
            get { return _DescripcionConcepto; }
            set { _DescripcionConcepto = LibString.Mid(value, 0, 30); }
        }

        public DateTime Fecha {
            get { return _Fecha; }
            set { _Fecha = LibConvert.DateToDbValue(value); }
        }

        public eIngresoEgreso TipoConceptoAsEnum {
            get { return _TipoConcepto; }
            set { _TipoConcepto = value; }
        }

        public string TipoConcepto {
            set { _TipoConcepto = (eIngresoEgreso)LibConvert.DbValueToEnum(value); }
        }

        public string TipoConceptoAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoConcepto); }
        }

        public string TipoConceptoAsString {
            get { return LibEnumHelper.GetDescription(_TipoConcepto); }
        }

        public decimal Monto {
            get { return _Monto; }
            set { _Monto = value; }
        }

        public string NumeroDocumento {
            get { return _NumeroDocumento; }
            set { _NumeroDocumento = LibString.Mid(value, 0, 15); }
        }

        public string Descripcion {
            get { return _Descripcion; }
            set { _Descripcion = LibString.Mid(value, 0, 255); }
        }

        public bool GeneraImpuestoBancarioAsBool {
            get { return _GeneraImpuestoBancario; }
            set { _GeneraImpuestoBancario = value; }
        }

        public string GeneraImpuestoBancario {
            set { _GeneraImpuestoBancario = LibConvert.SNToBool(value); }
        }


        public decimal AlicuotaImpBancario {
            get { return _AlicuotaImpBancario; }
            set { _AlicuotaImpBancario = value; }
        }
		
        public string NroMovimientoRelacionado {
            get { return _NroMovimientoRelacionado; }
            set { _NroMovimientoRelacionado = LibString.Mid(value, 0, 15); }
        }

        public eGeneradoPor GeneradoPorAsEnum {
            get { return _GeneradoPor; }
            set { _GeneradoPor = value; }
        }

        public string GeneradoPor {
            set { _GeneradoPor = (eGeneradoPor)LibConvert.DbValueToEnum(value); }
        }

        public string GeneradoPorAsDB {
            get { return LibConvert.EnumToDbValue((int) _GeneradoPor); }
        }

        public string GeneradoPorAsString {
            get { return LibEnumHelper.GetDescription(_GeneradoPor); }
        }

        public string Moneda {
            get { return _Moneda; }
            set { _Moneda = LibString.Mid(value, 0, 10); }
        }

        public decimal CambioABolivares {
            get { return _CambioABolivares; }
            set { _CambioABolivares = value; }
        }

        public bool ImprimirChequeAsBool {
            get { return _ImprimirCheque; }
            set { _ImprimirCheque = value; }
        }

        public string ImprimirCheque {
            set { _ImprimirCheque = LibConvert.SNToBool(value); }
        }


        public string Beneficiario {
            get { return _Beneficiario; }
            set { _Beneficiario = LibString.Mid(value, 0, 60); }
        }

        public string NumeroDeCheque {
            get { return _NumeroDeCheque; }
            set { _NumeroDeCheque = LibString.Mid(value, 0, 8); }
        }

        public bool ConciliadoSNAsBool {
            get { return _ConciliadoSN; }
            set { _ConciliadoSN = value; }
        }

        public string ConciliadoSN {
            set { _ConciliadoSN = LibConvert.SNToBool(value); }
        }


        public string NroConciliacion {
            get { return _NroConciliacion; }
            set { _NroConciliacion = LibString.Mid(value, 0, 9); }
        }

        public bool GenerarAsientoDeRetiroEnCuentaAsBool {
            get { return _GenerarAsientoDeRetiroEnCuenta; }
            set { _GenerarAsientoDeRetiroEnCuenta = value; }
        }

        public string GenerarAsientoDeRetiroEnCuenta {
            set { _GenerarAsientoDeRetiroEnCuenta = LibConvert.SNToBool(value); }
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

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public MovimientoBancario() {
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            ConsecutivoMovimiento = 0;
            CodigoCtaBancaria = "";
            NombreCtaBancaria = "";
            CodigoConcepto = "";
            DescripcionConcepto = "";
            Fecha = LibDate.Today();
            TipoConceptoAsEnum = eIngresoEgreso.Ingreso;
            Monto = 0;
            NumeroDocumento = "";
            Descripcion = "";
            GeneraImpuestoBancarioAsBool = false;
            AlicuotaImpBancario = 0;
            NroMovimientoRelacionado = "";
            GeneradoPorAsEnum = eGeneradoPor.Usuario;
            Moneda = "";
            CambioABolivares = 0;
            ImprimirChequeAsBool = false;
            Beneficiario = "";
            NumeroDeCheque = "";
            ConciliadoSNAsBool = false;
            NroConciliacion = "";
            GenerarAsientoDeRetiroEnCuentaAsBool = false;
            NombreOperador = "";
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public MovimientoBancario Clone() {
            MovimientoBancario vResult = new MovimientoBancario();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoMovimiento = _ConsecutivoMovimiento;
            vResult.CodigoCtaBancaria = _CodigoCtaBancaria;
            vResult.NombreCtaBancaria = _NombreCtaBancaria;
            vResult.CodigoConcepto = _CodigoConcepto;
            vResult.DescripcionConcepto = _DescripcionConcepto;
            vResult.Fecha = _Fecha;
            vResult.TipoConceptoAsEnum = _TipoConcepto;
            vResult.Monto = _Monto;
            vResult.NumeroDocumento = _NumeroDocumento;
            vResult.Descripcion = _Descripcion;
            vResult.GeneraImpuestoBancarioAsBool = _GeneraImpuestoBancario;
            vResult.AlicuotaImpBancario = _AlicuotaImpBancario;
            vResult.NroMovimientoRelacionado = _NroMovimientoRelacionado;
            vResult.GeneradoPorAsEnum = _GeneradoPor;
            vResult.Moneda = _Moneda;
            vResult.CambioABolivares = _CambioABolivares;
            vResult.ImprimirChequeAsBool = _ImprimirCheque;
            vResult.Beneficiario = _Beneficiario;
            vResult.NumeroDeCheque = _NumeroDeCheque;
            vResult.ConciliadoSNAsBool = _ConciliadoSN;
            vResult.NroConciliacion = _NroConciliacion;
            vResult.GenerarAsientoDeRetiroEnCuentaAsBool = _GenerarAsientoDeRetiroEnCuenta;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nNº Movimiento = " + _ConsecutivoMovimiento.ToString() +
               "\nCuenta Bancaria = " + _CodigoCtaBancaria +
               "\nCódigo Concepto = " + _CodigoConcepto +
               "\nFecha = " + _Fecha.ToShortDateString() +
               "\nTipo Concepto = " + _TipoConcepto.ToString() +
               "\nMonto = " + _Monto.ToString() +
               "\nNº Documento = " + _NumeroDocumento +
               "\nDescripcion = " + _Descripcion +
               "\nGenerar Impuesto Bancario = " + _GeneraImpuestoBancario +
               "\nAlicuota Imp Bancario = " + _AlicuotaImpBancario.ToString() +
               "\nNro Movimiento Relacionado = " + _NroMovimientoRelacionado +
               "\nGenerado Por = " + _GeneradoPor.ToString() +
               "\nCambio ABolivares = " + _CambioABolivares.ToString() +
               "\nImprimir Cheque = " + _ImprimirCheque +
               "\nConciliado SN = " + _ConciliadoSN +
               "\nNro Conciliacion = " + _NroConciliacion +
               "\nGenerar Asiento De Retiro En Cuenta = " + _GenerarAsientoDeRetiroEnCuenta +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class MovimientoBancario

} //End of namespace Galac.Adm.Ccl.Banco

