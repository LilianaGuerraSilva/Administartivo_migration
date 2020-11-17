using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Ccl.Venta {
    [Serializable]
    public class CobroDeFacturaRapidaAnticipo {
        #region Variables
        private int _ConsecutivoCompania;
        private string _NumeroFactura;
        private long _fldTimeStamp;
		private ObservableCollection<CobroDeFacturaRapidaAnticipoDetalle> _DetailCobroDeFacturaRapidaAnticipoDetalle;
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

        public ObservableCollection<CobroDeFacturaRapidaAnticipoDetalle> DetailCobroDeFacturaRapidaAnticipoDetalle {
            get { return _DetailCobroDeFacturaRapidaAnticipoDetalle; }
            set { _DetailCobroDeFacturaRapidaAnticipoDetalle = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public CobroDeFacturaRapidaAnticipo() {
            _DetailCobroDeFacturaRapidaAnticipoDetalle = new ObservableCollection<CobroDeFacturaRapidaAnticipoDetalle>();
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
            DetailCobroDeFacturaRapidaAnticipoDetalle = new ObservableCollection<CobroDeFacturaRapidaAnticipoDetalle>();
        }

        public CobroDeFacturaRapidaAnticipo Clone() {
            CobroDeFacturaRapidaAnticipo vResult = new CobroDeFacturaRapidaAnticipo();
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


    } //End of class CobroDeFacturaRapidaAnticipo

} //End of namespace Galac.Adm.Ccl.Venta

