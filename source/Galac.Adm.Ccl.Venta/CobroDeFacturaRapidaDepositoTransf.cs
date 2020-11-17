using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.Venta;
using System.Collections.ObjectModel;

namespace Galac.Adm.Ccl.Venta {
    [Serializable]
    public class CobroDeFacturaRapidaDepositoTransf {
        #region Variables
        private int _ConsecutivoCompania;
        private string _NumeroFactura;
        private long _fldTimeStamp;
        private ObservableCollection<CobroDeFacturaRapidaDepositoTransfDetalle> _DetailCobroDeFacturaRapidaDepositoTransfDetalle;
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

        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }

        public ObservableCollection<CobroDeFacturaRapidaDepositoTransfDetalle> DetailCobroDeFacturaRapidaDepositoTransfDetalle {
            get { return _DetailCobroDeFacturaRapidaDepositoTransfDetalle; }
            set { _DetailCobroDeFacturaRapidaDepositoTransfDetalle = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public CobroDeFacturaRapidaDepositoTransf() {
            _DetailCobroDeFacturaRapidaDepositoTransfDetalle = new ObservableCollection<CobroDeFacturaRapidaDepositoTransfDetalle>();
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
            fldTimeStamp = 0;
            DetailCobroDeFacturaRapidaDepositoTransfDetalle = new ObservableCollection<CobroDeFacturaRapidaDepositoTransfDetalle>();

        }

        public CobroDeFacturaRapidaDepositoTransf Clone() {
            CobroDeFacturaRapidaDepositoTransf vResult = new CobroDeFacturaRapidaDepositoTransf();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.NumeroFactura = _NumeroFactura;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nNumero Factura = " + _NumeroFactura;
        }
        #endregion //Metodos Generados


    } //End of class CobroDeFacturaRapidaDepositoTransf

} //End of namespace Galac.Adm.Ccl.Venta

