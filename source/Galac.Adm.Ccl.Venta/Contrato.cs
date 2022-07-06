using System;
using System.Collections.ObjectModel;
using System.Xml;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Venta
{
    [Serializable]
    public class Contrato {
        #region Variables
        private int _ConsecutivoCompania;
        private string _NumeroContrato;
        private eStatusContrato _StatusContrato;
        private string _CodigoCliente;
        private string _NombreCliente;
        private eDuracionDelContrato _DuracionDelContrato;
        private DateTime _FechaDeInicio;
        private DateTime _FechaFinal;
        private string _Observaciones;
        private string _NombreOperador;
        private string _CodigoVendedor;
        private string _NombreVendedor;
        private string _Moneda;
        private DateTime _FechaUltimaModificacion;
        private long _fldTimeStamp;
		private ObservableCollection<RenglonContrato> _DetailRenglonContrato;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string NumeroContrato {
            get { return _NumeroContrato; }
            set { _NumeroContrato = LibString.Mid(value, 0, 5); }
        }

        public eStatusContrato StatusContratoAsEnum {
            get { return _StatusContrato; }
            set { _StatusContrato = value; }
        }

        public string StatusContrato {
            set { _StatusContrato = (eStatusContrato)LibConvert.DbValueToEnum(value); }
        }

        public string StatusContratoAsDB {
            get { return LibConvert.EnumToDbValue((int) _StatusContrato); }
        }

        public string StatusContratoAsString {
            get { return LibEnumHelper.GetDescription(_StatusContrato); }
        }

        public string CodigoCliente {
            get { return _CodigoCliente; }
            set { _CodigoCliente = LibString.Mid(value, 0, 10); }
        }

        public string NombreCliente {
            get { return _NombreCliente; }
            set { _NombreCliente = LibString.Mid(value, 0, 80); }
        }

        public eDuracionDelContrato DuracionDelContratoAsEnum {
            get { return _DuracionDelContrato; }
            set { _DuracionDelContrato = value; }
        }

        public string DuracionDelContrato {
            set { _DuracionDelContrato = (eDuracionDelContrato)LibConvert.DbValueToEnum(value); }
        }

        public string DuracionDelContratoAsDB {
            get { return LibConvert.EnumToDbValue((int) _DuracionDelContrato); }
        }

        public string DuracionDelContratoAsString {
            get { return LibEnumHelper.GetDescription(_DuracionDelContrato); }
        }

        public DateTime FechaDeInicio {
            get { return _FechaDeInicio; }
            set { _FechaDeInicio = LibConvert.DateToDbValue(value); }
        }

        public DateTime FechaFinal {
            get { return _FechaFinal; }
            set { _FechaFinal = LibConvert.DateToDbValue(value); }
        }

        public string Observaciones {
            get { return _Observaciones; }
            set { _Observaciones = LibString.Mid(value, 0, 255); }
        }

        public string NombreOperador {
            get { return _NombreOperador; }
            set { _NombreOperador = LibString.Mid(value, 0, 10); }
        }

        public string CodigoVendedor {
            get { return _CodigoVendedor; }
            set { _CodigoVendedor = LibString.Mid(value, 0, 5); }
        }

        public string NombreVendedor {
            get { return _NombreVendedor; }
            set { _NombreVendedor = LibString.Mid(value, 0, 35); }
        }

        public string Moneda {
            get { return _Moneda; }
            set { _Moneda = LibString.Mid(value, 0, 10); }
        }

        public DateTime FechaUltimaModificacion {
            get { return _FechaUltimaModificacion; }
            set { _FechaUltimaModificacion = LibConvert.DateToDbValue(value); }
        }

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public ObservableCollection<RenglonContrato> DetailRenglonContrato {
            get { return _DetailRenglonContrato; }
            set { _DetailRenglonContrato = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public Contrato() {
            _DetailRenglonContrato = new ObservableCollection<RenglonContrato>();
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            NumeroContrato = string.Empty;
            StatusContratoAsEnum = eStatusContrato.Vigente;
            CodigoCliente = string.Empty;
            NombreCliente = string.Empty;
            DuracionDelContratoAsEnum = eDuracionDelContrato.DuracionFija;
            FechaDeInicio = LibDate.Today();
            FechaFinal = LibDate.Today();
            Observaciones = string.Empty;
            NombreOperador = string.Empty;
            CodigoVendedor = string.Empty;
            NombreVendedor = string.Empty;
            Moneda = string.Empty;
            FechaUltimaModificacion = LibDate.Today();
            fldTimeStamp = 0;
            DetailRenglonContrato = new ObservableCollection<RenglonContrato>();
        }

        public Contrato Clone() {
            Contrato vResult = new Contrato();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.NumeroContrato = _NumeroContrato;
            vResult.StatusContratoAsEnum = _StatusContrato;
            vResult.CodigoCliente = _CodigoCliente;
            vResult.NombreCliente = _NombreCliente;
            vResult.DuracionDelContratoAsEnum = _DuracionDelContrato;
            vResult.FechaDeInicio = _FechaDeInicio;
            vResult.FechaFinal = _FechaFinal;
            vResult.Observaciones = _Observaciones;
            vResult.NombreOperador = _NombreOperador;
            vResult.CodigoVendedor = _CodigoVendedor;
            vResult.NombreVendedor = _NombreVendedor;
            vResult.Moneda = _Moneda;
            vResult.FechaUltimaModificacion = _FechaUltimaModificacion;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = "        + _ConsecutivoCompania.ToString() +
                  "\nNº Contrato = "               + _NumeroContrato +
                  "\nStatus Contrato = "           + _StatusContrato.ToString() +
                  "\nCódigo del Cliente = "        + _CodigoCliente +
                  "\nDuracion Del Contrato = "     + _DuracionDelContrato.ToString() +
                  "\nFecha De Inicio = "           + _FechaDeInicio.ToShortDateString() +
                  "\nFecha Final = "               + _FechaFinal.ToShortDateString() +
                  "\nObservaciones = "             + _Observaciones +
                  "\nNombre Operador = "           + _NombreOperador +
                  "\nCódigo del Vendedor = "       + _CodigoVendedor +
                  "\nMoneda = "                    + _Moneda +
                  "\nFecha Ultima Modificacion = " + _FechaUltimaModificacion.ToShortDateString();
        }
        #endregion //Metodos Generados

    } //End of class Contrato

} //End of namespace Galac.Dbo.Ccl.Venta

