using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;


namespace Galac.Adm.Ccl.Venta {
    [Serializable]
    public class RenglonCobroDeFactura: IEquatable<RenglonCobroDeFactura>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private string _NumeroFactura;
        private eTipoDocumentoFactura _TipoDeDocumento;
        private int _ConsecutivoRenglon;
        private string _CodigoFormaDelCobro;
        private string _NumeroDelDocumento;
        private int _CodigoBanco;
        private decimal _Monto;
        private int _CodigoPuntoDeVenta;
        private string _NumeroDocumentoAprobacion;
        private string _NombreBanco;
        private string _NombreBancoPuntoDeVenta;
        private string _CodigoMoneda;
        private decimal _CambioAMonedaLocal;
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

        public eTipoDocumentoFactura TipoDeDocumentoAsEnum {
            get { return _TipoDeDocumento; }
            set { _TipoDeDocumento = value; }
        }

        public string TipoDeDocumento {
            set { _TipoDeDocumento = (eTipoDocumentoFactura)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeDocumentoAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeDocumento); }
        }

        public string TipoDeDocumentoAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeDocumento); }
        }

        public int ConsecutivoRenglon {
            get { return _ConsecutivoRenglon; }
            set { _ConsecutivoRenglon = value; }
        }

        public string CodigoFormaDelCobro {
            get { return _CodigoFormaDelCobro; }
            set { _CodigoFormaDelCobro = LibString.Mid(value, 0, 5); }
        }

        public string NumeroDelDocumento {
            get { return _NumeroDelDocumento; }
            set { _NumeroDelDocumento = LibString.Mid(value, 0, 30); }
        }

        public int CodigoBanco {
            get { return _CodigoBanco; }
            set { _CodigoBanco = value; }
        }

        public decimal Monto {
            get { return _Monto; }
            set { _Monto = value; }
        }

        public int CodigoPuntoDeVenta {
            get { return _CodigoPuntoDeVenta; }
            set { _CodigoPuntoDeVenta = value; }
        }

        public string NumeroDocumentoAprobacion {
            get { return _NumeroDocumentoAprobacion; }
            set { _NumeroDocumentoAprobacion = LibString.Mid(value, 0, 30); }
        }

        public string NombreBanco {
            get { return _NombreBanco; }
            set { _NombreBanco = LibString.Mid(value, 0, 40); }
        }

        public string NombreBancoPuntoDeVenta {
            get { return _NombreBancoPuntoDeVenta; }
            set { _NombreBancoPuntoDeVenta = LibString.Mid(value, 0, 40); }
        }

        public string CodigoMoneda {
            get { return _CodigoMoneda; }
            set { _CodigoMoneda = value; }
        }

        public decimal CambioAMonedaLocal {
            get { return _CambioAMonedaLocal; }
            set { _CambioAMonedaLocal = value; }
        }


        #endregion //Propiedades
        #region Constructores

        public RenglonCobroDeFactura() {
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
            TipoDeDocumentoAsEnum = eTipoDocumentoFactura.Factura;
            ConsecutivoRenglon = 0;
            CodigoFormaDelCobro = string.Empty;
            NumeroDelDocumento = string.Empty;
            CodigoBanco = 0;
            Monto = 0;
            CodigoPuntoDeVenta = 0;
            NumeroDocumentoAprobacion = string.Empty;
            NombreBanco = string.Empty;
            NombreBancoPuntoDeVenta = string.Empty;
            CodigoMoneda = string.Empty;
            CambioAMonedaLocal = 0;
        }

        public RenglonCobroDeFactura Clone() {
            RenglonCobroDeFactura vResult = new RenglonCobroDeFactura();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.NumeroFactura = _NumeroFactura;
            vResult.TipoDeDocumentoAsEnum = _TipoDeDocumento;
            vResult.ConsecutivoRenglon = _ConsecutivoRenglon;
            vResult.CodigoFormaDelCobro = _CodigoFormaDelCobro;
            vResult.NumeroDelDocumento = _NumeroDelDocumento;
            vResult.CodigoBanco = _CodigoBanco;
            vResult.Monto = _Monto;
            vResult.CodigoPuntoDeVenta = _CodigoPuntoDeVenta;
            vResult.NumeroDocumentoAprobacion = _NumeroDocumentoAprobacion;
            vResult.NombreBanco = _NombreBanco;
            vResult.NombreBancoPuntoDeVenta = _NombreBancoPuntoDeVenta;
            vResult.CodigoMoneda = _CodigoMoneda;
            vResult.CambioAMonedaLocal = _CambioAMonedaLocal;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nNumero Factura = " + _NumeroFactura +
               "\nTipo De Documento = " + _TipoDeDocumento.ToString() +
               "\nConsecutivo Renglon = " + _ConsecutivoRenglon.ToString() +
               "\nCodigo Forma Del Cobro = " + _CodigoFormaDelCobro +
               "\nNumero Del Documento = " + _NumeroDelDocumento +
               "\nCodigo Banco = " + _CodigoBanco.ToString() +
               "\nMonto = " + _Monto.ToString() +
               "\nCodigo Punto De Venta = " + _CodigoPuntoDeVenta.ToString() +
               "\nNumero Documento Aprobacion = " + _NumeroDocumentoAprobacion +
               "\nNombreBanco = " + _NombreBanco +
               "\nNombreBancoPuntoDeVenta = " + _NombreBancoPuntoDeVenta +
               "\nCodigoMoneda = " + _CodigoMoneda +
               "\nCambioAMonedaLocal = " + _CambioAMonedaLocal;
        }

        #region Miembros de IEquatable<RenglonCobroDeFactura>
        bool IEquatable<RenglonCobroDeFactura>.Equals(RenglonCobroDeFactura other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<RenglonCobroDeFactura>

        #region Miembros de ICloneable<RenglonCobroDeFactura>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<RenglonCobroDeFactura>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class RenglonCobroDeFactura

} //End of namespace Galac..Ccl.ComponenteNoEspecificado

