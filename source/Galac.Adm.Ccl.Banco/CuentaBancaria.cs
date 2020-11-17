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
    public class CuentaBancaria {
        #region Variables
        private int _ConsecutivoCompania;
        private string _Codigo;
        private eStatusCtaBancaria _Status;
        private string _NumeroCuenta;
        private string _NombreCuenta;
        private int _CodigoBanco;
        private string _CodigoBancoPant;
        private string _NombreBanco;
        private string _NombreSucursal;
        private eTipoDeCtaBancaria _TipoCtaBancaria;
        private bool _ManejaDebitoBancario;
        private bool _ManejaCreditoBancario;
        private decimal _SaldoDisponible;
        private string _NombreDeLaMoneda;
        private string _NombrePlantillaCheque;
        private string _CuentaContable;
        private string _CodigoMoneda;
        private string _NombreOperador;
        private DateTime _FechaUltimaModificacion;
        private bool _EsCajaChica;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string Codigo {
            get { return _Codigo; }
            set { _Codigo = LibString.Mid(value, 0, 5); }
        }

        public eStatusCtaBancaria StatusAsEnum {
            get { return _Status; }
            set { _Status = value; }
        }

        public string Status {
            set { _Status = (eStatusCtaBancaria)LibConvert.DbValueToEnum(value); }
        }

        public string StatusAsDB {
            get { return LibConvert.EnumToDbValue((int) _Status); }
        }

        public string StatusAsString {
            get { return LibEnumHelper.GetDescription(_Status); }
        }

        public string NumeroCuenta {
            get { return _NumeroCuenta; }
            set { _NumeroCuenta = LibString.Mid(value, 0, 30); }
        }

        public string NombreCuenta {
            get { return _NombreCuenta; }
            set { _NombreCuenta = LibString.Mid(value, 0, 40); }
        }

        public int CodigoBanco {
            get { return _CodigoBanco; }
            set { _CodigoBanco = value; }
        }

        public string CodigoBancoPant {
            get { return _CodigoBancoPant; }
            set { _CodigoBancoPant = LibString.Mid(value, 0, 4); }
        }
		
        public string NombreBanco {
            get { return _NombreBanco; }
            set { _NombreBanco = LibString.Mid(value, 0, 40); }
        }

        public string NombreSucursal {
            get { return _NombreSucursal; }
            set { _NombreSucursal = LibString.Mid(value, 0, 40); }
        }

        public eTipoDeCtaBancaria TipoCtaBancariaAsEnum {
            get { return _TipoCtaBancaria; }
            set { _TipoCtaBancaria = value; }
        }

        public string TipoCtaBancaria {
            set { _TipoCtaBancaria = (eTipoDeCtaBancaria)LibConvert.DbValueToEnum(value); }
        }

        public string TipoCtaBancariaAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoCtaBancaria); }
        }

        public string TipoCtaBancariaAsString {
            get { return LibEnumHelper.GetDescription(_TipoCtaBancaria); }
        }

        public bool ManejaDebitoBancarioAsBool {
            get { return _ManejaDebitoBancario; }
            set { _ManejaDebitoBancario = value; }
        }

        public string ManejaDebitoBancario {
            set { _ManejaDebitoBancario = LibConvert.SNToBool(value); }
        }


        public bool ManejaCreditoBancarioAsBool {
            get { return _ManejaCreditoBancario; }
            set { _ManejaCreditoBancario = value; }
        }

        public string ManejaCreditoBancario {
            set { _ManejaCreditoBancario = LibConvert.SNToBool(value); }
        }


        public decimal SaldoDisponible {
            get { return _SaldoDisponible; }
            set { _SaldoDisponible = value; }
        }

        public string NombreDeLaMoneda {
            get { return _NombreDeLaMoneda; }
            set { _NombreDeLaMoneda = LibString.Mid(value, 0, 80); }
        }

        public string NombrePlantillaCheque {
            get { return _NombrePlantillaCheque; }
            set { _NombrePlantillaCheque = LibString.Mid(value, 0, 50); }
        }

        public string CuentaContable {
            get { return _CuentaContable; }
            set { _CuentaContable = LibString.Mid(value, 0, 30); }
        }

        public string CodigoMoneda {
            get { return _CodigoMoneda; }
            set { _CodigoMoneda = LibString.Mid(value, 0, 4); }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 20); }
        }

        public DateTime FechaUltimaModificacion {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = LibConvert.DateToDbValue(value); }
        }
        public bool EsCajaChicaAsBool {
            get { return _EsCajaChica; }
            set { _EsCajaChica = value; }
        }
        public string EsCajaChica {
            set { _EsCajaChica = LibConvert.SNToBool(value); }
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

        public CuentaBancaria() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return string.Empty;
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            Codigo = string.Empty;
            StatusAsEnum = eStatusCtaBancaria.Activo;
            NumeroCuenta = string.Empty;
            NombreCuenta = string.Empty;
            CodigoBanco = 0;
            CodigoBancoPant = string.Empty;
            NombreBanco = string.Empty;
            NombreSucursal = string.Empty;
            TipoCtaBancariaAsEnum = eTipoDeCtaBancaria.Corriente;
            ManejaDebitoBancarioAsBool = false;
            ManejaCreditoBancarioAsBool = false;
            SaldoDisponible = 0;
            NombreDeLaMoneda = string.Empty;
            NombrePlantillaCheque = "rpxChequeGenerico";
            CuentaContable = string.Empty;
            CodigoMoneda = string.Empty;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            EsCajaChicaAsBool = false;
            fldTimeStamp = 0;
        }

        public CuentaBancaria Clone() {
            CuentaBancaria vResult = new CuentaBancaria();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Codigo = _Codigo;
            vResult.StatusAsEnum = _Status;
            vResult.NumeroCuenta = _NumeroCuenta;
            vResult.NombreCuenta = _NombreCuenta;
            vResult.CodigoBanco = _CodigoBanco;
            vResult.CodigoBancoPant = _CodigoBancoPant;
            vResult.NombreBanco = _NombreBanco;
            vResult.NombreSucursal = _NombreSucursal;
            vResult.TipoCtaBancariaAsEnum = _TipoCtaBancaria;
            vResult.ManejaDebitoBancarioAsBool = _ManejaDebitoBancario;
            vResult.ManejaCreditoBancarioAsBool = _ManejaCreditoBancario;
            vResult.SaldoDisponible = _SaldoDisponible;
            vResult.NombreDeLaMoneda = _NombreDeLaMoneda;
            vResult.NombrePlantillaCheque = _NombrePlantillaCheque;
            vResult.CuentaContable = _CuentaContable;
            vResult.CodigoMoneda = _CodigoMoneda;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.EsCajaChicaAsBool = _EsCajaChica;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nCódigo Cuenta Bancaria = " + _Codigo +
               "\nStatus = " + _Status.ToString() +
               "\nNº de Cuenta Bancaria = " + _NumeroCuenta +
               "\nNombre de la Cuenta = " + _NombreCuenta +
               "\nCódigo del Banco = " + _CodigoBanco.ToString() +
               "\nNombre del Banco = " + _NombreBanco +
               "\nNombre de la Sucursal = " + _NombreSucursal +
               "\nTipo de Cuenta = " + _TipoCtaBancaria.ToString() +
               "\nManeja Débito Bancario = " + _ManejaDebitoBancario +
               "\nManeja Crédito Bancario = " + _ManejaCreditoBancario +
               "\nSaldo Disponible = " + _SaldoDisponible.ToString() +
               "\nNombre de la Moneda = " + _NombreDeLaMoneda +
               "\nNombre Plantilla Cheque = " + _NombrePlantillaCheque +
               "\nCuenta Contable = " + _CuentaContable +
               "\nCódigo = " + _CodigoMoneda +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString() +
               "\nEs Caja Chica  = " + _EsCajaChica;
        }
        #endregion //Metodos Generados


    } //End of class CuentaBancaria

} //End of namespace Galac.Adm.Ccl.Banco

