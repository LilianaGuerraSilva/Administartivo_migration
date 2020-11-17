using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Ccl.Venta {
    [Serializable]
    public class CobroDeFacturaRapidaEfectivo {
        #region Variables
        private int _ConsecutivoCompania;
        private string _NumeroFactura;
        private string _CodigoFormaDelCobro;
        private decimal _MontoEfectivo;
        private decimal _TotalACobrar;
        private decimal _TotalCobrado;
        private decimal _Diferencia;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string NumeroFactura {
            get { return _NumeroFactura; }
            set { _NumeroFactura = LibString.Mid(value, 0, 11); }
        }

        public string CodigoFormaDelCobro {
            get { return _CodigoFormaDelCobro; }
            set { _CodigoFormaDelCobro = LibString.Mid(value, 0, 5); }
        }

        public decimal MontoEfectivo {
            get { return _MontoEfectivo; }
            set { _MontoEfectivo = value; }
        }

        public decimal TotalACobrar {
            get { return _TotalACobrar; }
            set { _TotalACobrar = value; }
        }

        public decimal TotalCobrado {
            get { return _TotalCobrado; }
            set { _TotalCobrado = value; }
        }

        public decimal Diferencia {
            get { return _Diferencia; }
            set { _Diferencia = value; }
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

        public CobroDeFacturaRapidaEfectivo() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            NumeroFactura = string.Empty;
            CodigoFormaDelCobro = string.Empty;
            MontoEfectivo = 0;
            TotalACobrar = 0;
            TotalCobrado = 0;
            Diferencia = 0;
            fldTimeStamp = 0;
        }

        public CobroDeFacturaRapidaEfectivo Clone() {
            CobroDeFacturaRapidaEfectivo vResult = new CobroDeFacturaRapidaEfectivo();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.NumeroFactura = _NumeroFactura;
            vResult.CodigoFormaDelCobro = _CodigoFormaDelCobro;
            vResult.MontoEfectivo = _MontoEfectivo;
            vResult.TotalACobrar = _TotalACobrar;
            vResult.TotalCobrado = _TotalCobrado;
            vResult.Diferencia = _Diferencia;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nNumero Factura = " + _NumeroFactura +
               "\nCodigo Forma Del Cobro = " + _CodigoFormaDelCobro +
               "\nMonto Efectivo = " + _MontoEfectivo.ToString() +
               "\nTotal A Cobrar = " + _TotalACobrar.ToString();
        }
        #endregion //Metodos Generados


    } //End of class CobroDeFacturaRapidaEfectivo

} //End of namespace Galac.Adm.Ccl.Venta

