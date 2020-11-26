using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Ccl.Venta {
    [Serializable]
    public class FacturaRapidaDetalle: IEquatable<FacturaRapidaDetalle>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private string _NumeroFactura;
        private eTipoDocumentoFactura _TipoDeDocumento;
        private int _ConsecutivoRenglon;
        private string _Articulo;
        private string _Descripcion;
        private string _CodigoVendedor1;
        private string _CodigoVendedor2;
        private string _CodigoVendedor3;
        private eTipoDeAlicuota _AlicuotaIva;
        private decimal _Cantidad;
        private decimal _PrecioSinIVA;
        private decimal _PrecioConIVA;
        private decimal _PorcentajeDescuento;
        private decimal _MontoBrutoSinIva;
        private decimal _MontoBrutoConIva;
        private decimal _TotalRenglon;
        private decimal _PorcentajeBaseImponible;
        private string _Serial;
        private string _Rollo;
        private decimal _PorcentajeAlicuota;
        private string _CampoExtraEnRenglonFactura1;
        private string _CampoExtraEnRenglonFactura2;
        private eTipoDeArticulo _TipoDeArticulo;
        private eTipoArticuloInv _TipoArticuloInv;
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

        public string Articulo {
            get { return _Articulo; }
            set { 
                _Articulo = LibString.Mid(value, 0, 30);
                OnPropertyChanged("Articulo");
            }
        }

        public string Descripcion {
            get { return _Descripcion; }
            set { 
                _Descripcion = LibString.Mid(value, 0, 255);
                OnPropertyChanged("Descripcion");
            }
        }

        public string CodigoVendedor1 {
            get { return _CodigoVendedor1; }
            set { _CodigoVendedor1 = LibString.Mid(value, 0, 5); }
        }

        public string CodigoVendedor2 {
            get { return _CodigoVendedor2; }
            set { _CodigoVendedor2 = LibString.Mid(value, 0, 5); }
        }

        public string CodigoVendedor3 {
            get { return _CodigoVendedor3; }
            set { _CodigoVendedor3 = LibString.Mid(value, 0, 5); }
        }

        public eTipoDeAlicuota AlicuotaIvaAsEnum {
            get { return _AlicuotaIva; }
            set { _AlicuotaIva = value; }
        }

        public string AlicuotaIva {
            set { _AlicuotaIva = (eTipoDeAlicuota)LibConvert.DbValueToEnum(value); }
        }

        public string AlicuotaIvaAsDB {
            get { return LibConvert.EnumToDbValue((int) _AlicuotaIva); }
        }

        public string AlicuotaIvaAsString {
            get { return LibEnumHelper.GetDescription(_AlicuotaIva); }
        }
		
        public decimal Cantidad {
            get { return _Cantidad; }
            set { 
                _Cantidad = value;
                OnPropertyChanged("Cantidad");
            }
        }

        public decimal PrecioSinIVA {
            get { return _PrecioSinIVA; }
            set { 
                _PrecioSinIVA = value;
                OnPropertyChanged("PrecioSinIVA");
            }
        }

        public decimal PrecioConIVA {
            get { return _PrecioConIVA; }
            set { 
                _PrecioConIVA = value;
                OnPropertyChanged("PrecioConIVA");
            }
        }

        public decimal PorcentajeDescuento {
            get { return _PorcentajeDescuento; }
            set { _PorcentajeDescuento = value; }
        }

        public decimal MontoBrutoSinIva {
            get { return _MontoBrutoSinIva; }
            set { _MontoBrutoSinIva = value; }
        }

        public decimal MontoBrutoConIva {
            get { return _MontoBrutoConIva; }
            set { _MontoBrutoConIva = value; }
        }

        public decimal TotalRenglon {
            get { return _TotalRenglon; }
            set { 
                _TotalRenglon = value;
                OnPropertyChanged("TotalRenglon");
            }
        }

        public decimal PorcentajeBaseImponible {
            get { return _PorcentajeBaseImponible; }
            set { _PorcentajeBaseImponible = value; }
        }

        public string Serial {
            get { return _Serial; }
            set { _Serial = LibString.Mid(value, 0, 50); }
        }

        public string Rollo {
            get { return _Rollo; }
            set { _Rollo = LibString.Mid(value, 0, 20); }
        }

        public decimal PorcentajeAlicuota {
            get { return _PorcentajeAlicuota; }
            set { _PorcentajeAlicuota = value; }
        }

        public string CampoExtraEnRenglonFactura1 {
            get { return _CampoExtraEnRenglonFactura1; }
            set { _CampoExtraEnRenglonFactura1 = LibString.Mid(value, 0, 60); }
        }

        public string CampoExtraEnRenglonFactura2 {
            get { return _CampoExtraEnRenglonFactura2; }
            set { _CampoExtraEnRenglonFactura2 = LibString.Mid(value, 0, 60); }
        }

        public eTipoDeArticulo TipoDeArticuloAsEnum {
            get { return _TipoDeArticulo; }
            set { _TipoDeArticulo = value; }
        }

        public string TipoDeArticulo {
            set { _TipoDeArticulo = (eTipoDeArticulo)LibConvert.DbValueToEnum(value); }
        }

        public string TipoDeArticuloAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoDeArticulo); }
        }

        public string TipoDeArticuloAsString {
            get { return LibEnumHelper.GetDescription(_TipoDeArticulo); }
        }

        public eTipoArticuloInv TipoArticuloInvAsEnum {
            get { return _TipoArticuloInv; }
            set { _TipoArticuloInv = value; }
        }

        public string TipoArticuloInv {
            set { _TipoArticuloInv = (eTipoArticuloInv)LibConvert.DbValueToEnum(value); }
        }

        public string TipoArticuloInvAsDB {
            get { return LibConvert.EnumToDbValue((int) _TipoArticuloInv); }
        }

        public string TipoArticuloInvAsString {
            get { return LibEnumHelper.GetDescription(_TipoArticuloInv); }
        }
        #endregion //Propiedades
        #region Constructores

        public FacturaRapidaDetalle() {
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
            Articulo = string.Empty;
            Descripcion = string.Empty;
            CodigoVendedor1 = string.Empty;
            CodigoVendedor2 = string.Empty;
            CodigoVendedor3 = string.Empty;
            AlicuotaIvaAsEnum = eTipoDeAlicuota.Exento;
            Cantidad = 0;
            PrecioSinIVA = 0;
            PrecioConIVA = 0;
            PorcentajeDescuento = 0;
            MontoBrutoSinIva = 0;
            MontoBrutoConIva = 0;
            TotalRenglon = 0;
            PorcentajeBaseImponible = 0;
            Serial = "0";
            Rollo = "0";
            PorcentajeAlicuota = 0;
            CampoExtraEnRenglonFactura1 = string.Empty;
            CampoExtraEnRenglonFactura2 = string.Empty;
            TipoDeArticuloAsEnum = eTipoDeArticulo.Mercancia;
            TipoArticuloInvAsEnum = eTipoArticuloInv.Simple;
        }

        public FacturaRapidaDetalle Clone() {
            FacturaRapidaDetalle vResult = new FacturaRapidaDetalle();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.NumeroFactura = _NumeroFactura;
            vResult.TipoDeDocumentoAsEnum = _TipoDeDocumento;
            vResult.ConsecutivoRenglon = _ConsecutivoRenglon;
            vResult.Articulo = _Articulo;
            vResult.Descripcion = _Descripcion;
            vResult.CodigoVendedor1 = _CodigoVendedor1;
            vResult.CodigoVendedor2 = _CodigoVendedor2;
            vResult.CodigoVendedor3 = _CodigoVendedor3;
            vResult.AlicuotaIvaAsEnum = _AlicuotaIva;
            vResult.Cantidad = _Cantidad;
            vResult.PrecioSinIVA = _PrecioSinIVA;
            vResult.PrecioConIVA = _PrecioConIVA;
            vResult.PorcentajeDescuento = _PorcentajeDescuento;
            vResult.MontoBrutoSinIva = _MontoBrutoSinIva;
            vResult.MontoBrutoConIva = _MontoBrutoConIva;
            vResult.TotalRenglon = _TotalRenglon;
            vResult.PorcentajeBaseImponible = _PorcentajeBaseImponible;
            vResult.Serial = _Serial;
            vResult.Rollo = _Rollo;
            vResult.PorcentajeAlicuota = _PorcentajeAlicuota;
            vResult.CampoExtraEnRenglonFactura1 = _CampoExtraEnRenglonFactura1;
            vResult.CampoExtraEnRenglonFactura2 = _CampoExtraEnRenglonFactura2;
            vResult.TipoDeArticuloAsEnum = _TipoDeArticulo;
            vResult.TipoArticuloInvAsEnum = _TipoArticuloInv;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nNumero Factura = " + _NumeroFactura +
               "\nTipo De Documento = " + _TipoDeDocumento.ToString() +
               "\nConsecutivo Renglon = " + _ConsecutivoRenglon.ToString() +
               "\nCódigo Inventario = " + _Articulo +
               "\nDescripción = " + _Descripcion +
               "\nAlicuota Iva = " + _AlicuotaIva.ToString() +
               "\nCantidad = " + _Cantidad.ToString() +
               "\nPrecio sin IVA = " + _PrecioSinIVA.ToString() +
               "\nPrecio Con IVA = " + _PrecioConIVA.ToString() +
               "\nPorcentaje Descuento = " + _PorcentajeDescuento.ToString() +
               "\nTotal del Renglón = " + _TotalRenglon.ToString() +
               "\nPorcentaje Base Imponible = " + _PorcentajeBaseImponible.ToString() +
               "\nSerial = " + _Serial +
               "\nRollo = " + _Rollo +
               "\nPorcentaje Alicuota = " + _PorcentajeAlicuota.ToString();
        }

        #region Miembros de IEquatable<FacturaRapidaDetalle>
        bool IEquatable<FacturaRapidaDetalle>.Equals(FacturaRapidaDetalle other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<FacturaRapidaDetalle>

        #region Miembros de ICloneable<FacturaRapidaDetalle>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<FacturaRapidaDetalle>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class FacturaRapidaDetalle

} //End of namespace Galac.Adm.Ccl.Venta

