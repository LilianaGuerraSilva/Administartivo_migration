using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Ccl.Tablas {
    [Serializable]
    public class OtrosCargosDeFactura {
        #region Variables
        private int _ConsecutivoCompania;
        private string _Codigo;
        private string _Descripcion;
        private eStatusOtrosCargosyDescuentosDeFactura _Status;
        private eBaseCalculoOtrosCargosDeFactura _SeCalculaEnBasea;
        private decimal _Monto;
        private eBaseFormulaOtrosCargosDeFactura _BaseFormula;
        private decimal _PorcentajeSobreBase;
        private decimal _Sustraendo;
        private eComoAplicaOtrosCargosDeFactura _ComoAplicaAlTotalFactura;
        private string _CuentaContableOtrosCargos;
        private decimal _PorcentajeComision;
        private bool _ExcluirDeComision;
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

        public string Codigo {
            get { return _Codigo; }
            set { _Codigo = LibString.Mid(value, 0, 15); }
        }

        public string Descripcion {
            get { return _Descripcion; }
            set { _Descripcion = LibString.Mid(value, 0, 255); }
        }

        public eStatusOtrosCargosyDescuentosDeFactura StatusAsEnum {
            get { return _Status; }
            set { _Status = value; }
        }

        public string Status {
            set { _Status = (eStatusOtrosCargosyDescuentosDeFactura)LibConvert.DbValueToEnum(value); }
        }

        public string StatusAsDB {
            get { return LibConvert.EnumToDbValue((int) _Status); }
        }

        public string StatusAsString {
            get { return LibEnumHelper.GetDescription(_Status); }
        }

        public eBaseCalculoOtrosCargosDeFactura SeCalculaEnBaseaAsEnum {
            get { return _SeCalculaEnBasea; }
            set { _SeCalculaEnBasea = value; }
        }

        public string SeCalculaEnBasea {
            set { _SeCalculaEnBasea = (eBaseCalculoOtrosCargosDeFactura)LibConvert.DbValueToEnum(value); }
        }

        public string SeCalculaEnBaseaAsDB {
            get { return LibConvert.EnumToDbValue((int) _SeCalculaEnBasea); }
        }

        public string SeCalculaEnBaseaAsString {
            get { return LibEnumHelper.GetDescription(_SeCalculaEnBasea); }
        }

        public decimal Monto {
            get { return _Monto; }
            set { _Monto = value; }
        }

        public eBaseFormulaOtrosCargosDeFactura BaseFormulaAsEnum {
            get { return _BaseFormula; }
            set { _BaseFormula = value; }
        }

        public string BaseFormula {
            set { _BaseFormula = (eBaseFormulaOtrosCargosDeFactura)LibConvert.DbValueToEnum(value); }
        }

        public string BaseFormulaAsDB {
            get { return LibConvert.EnumToDbValue((int) _BaseFormula); }
        }

        public string BaseFormulaAsString {
            get { return LibEnumHelper.GetDescription(_BaseFormula); }
        }

        public decimal PorcentajeSobreBase {
            get { return _PorcentajeSobreBase; }
            set { _PorcentajeSobreBase = value; }
        }

        public decimal Sustraendo {
            get { return _Sustraendo; }
            set { _Sustraendo = value; }
        }

        public eComoAplicaOtrosCargosDeFactura ComoAplicaAlTotalFacturaAsEnum {
            get { return _ComoAplicaAlTotalFactura; }
            set { _ComoAplicaAlTotalFactura = value; }
        }

        public string ComoAplicaAlTotalFactura {
            set { _ComoAplicaAlTotalFactura = (eComoAplicaOtrosCargosDeFactura)LibConvert.DbValueToEnum(value); }
        }

        public string ComoAplicaAlTotalFacturaAsDB {
            get { return LibConvert.EnumToDbValue((int) _ComoAplicaAlTotalFactura); }
        }

        public string ComoAplicaAlTotalFacturaAsString {
            get { return LibEnumHelper.GetDescription(_ComoAplicaAlTotalFactura); }
        }

        public string CuentaContableOtrosCargos {
            get { return _CuentaContableOtrosCargos; }
            set { _CuentaContableOtrosCargos = LibString.Mid(value, 0, 30); }
        }

        public decimal PorcentajeComision {
            get { return _PorcentajeComision; }
            set { _PorcentajeComision = value; }
        }

        public bool ExcluirDeComisionAsBool {
            get { return _ExcluirDeComision; }
            set { _ExcluirDeComision = value; }
        }

        public string ExcluirDeComision {
            set { _ExcluirDeComision = LibConvert.SNToBool(value); }
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

        public OtrosCargosDeFactura() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            Codigo = string.Empty;
            Descripcion = string.Empty;
            StatusAsEnum = eStatusOtrosCargosyDescuentosDeFactura.Vigente;
            SeCalculaEnBaseaAsEnum = eBaseCalculoOtrosCargosDeFactura.Formula;
            Monto = 0;
            BaseFormulaAsEnum = eBaseFormulaOtrosCargosDeFactura.SubTotal;
            PorcentajeSobreBase = 0;
            Sustraendo = 0;
            ComoAplicaAlTotalFacturaAsEnum = eComoAplicaOtrosCargosDeFactura.Suma;
            CuentaContableOtrosCargos = string.Empty;
            PorcentajeComision = 0;
            ExcluirDeComisionAsBool = false;
            NombreOperador = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
        }

        public OtrosCargosDeFactura Clone() {
            OtrosCargosDeFactura vResult = new OtrosCargosDeFactura();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.Codigo = _Codigo;
            vResult.Descripcion = _Descripcion;
            vResult.StatusAsEnum = _Status;
            vResult.SeCalculaEnBaseaAsEnum = _SeCalculaEnBasea;
            vResult.Monto = _Monto;
            vResult.BaseFormulaAsEnum = _BaseFormula;
            vResult.PorcentajeSobreBase = _PorcentajeSobreBase;
            vResult.Sustraendo = _Sustraendo;
            vResult.ComoAplicaAlTotalFacturaAsEnum = _ComoAplicaAlTotalFactura;
            vResult.CuentaContableOtrosCargos = _CuentaContableOtrosCargos;
            vResult.PorcentajeComision = _PorcentajeComision;
            vResult.ExcluirDeComisionAsBool = _ExcluirDeComision;
            vResult.NombreOperador = _NombreOperador;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nCódigo del Cargo = " + _Codigo +
               "\nDescripción = " + _Descripcion +
               "\nStatus = " + _Status.ToString() +
               "\nSe Calcula en Base A = " + _SeCalculaEnBasea.ToString() +
               "\nMonto = " + _Monto.ToString() +
               "\nBase Formula = " + _BaseFormula.ToString() +
               "\nPorcentaje Sobre Base = " + _PorcentajeSobreBase.ToString() +
               "\nSustraendo = " + _Sustraendo.ToString() +
               "\nComo Aplica Al Total Factura = " + _ComoAplicaAlTotalFactura.ToString() +
               "\nCuenta Contable Otros Cargos = " + _CuentaContableOtrosCargos +
               "\nPorcentaje Comision = " + _PorcentajeComision.ToString() +
               "\nExcluir De Comision = " + _ExcluirDeComision +
               "\nNombre Operador = " + _NombreOperador +
               "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados


    } //End of class OtrosCargosDeFactura

} //End of namespace Galac.Saw.Ccl.Tablas

