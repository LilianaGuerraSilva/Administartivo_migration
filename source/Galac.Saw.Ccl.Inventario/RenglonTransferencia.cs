using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Inventario {
    [Serializable]
    public class RenglonTransferencia: IEquatable<RenglonTransferencia> {
        #region Variables
        private int _ConsecutivoCompania;
        private string _NumeroDocumento;
        private int _consecutivoRenglon;
        private string _CodigoArticulo;
        private decimal _Cantidad;
        private string _Serial;
        private string _Rollo;
        private eTipoArticuloInv _TipoArticuloInv;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public string NumeroDocumento {
            get { return _NumeroDocumento; }
            set { _NumeroDocumento = LibString.Mid(value, 0, 10); }
        }

        public int consecutivoRenglon {
            get { return _consecutivoRenglon; }
            set { _consecutivoRenglon = value; }
        }

        public string CodigoArticulo {
            get { return _CodigoArticulo; }
            set { _CodigoArticulo = LibString.Mid(value, 0, 15); }
        }

        public decimal Cantidad {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        public string Serial {
            get { return _Serial; }
            set { _Serial = LibString.Mid(value, 0, 50); }
        }

        public string Rollo {
            get { return _Rollo; }
            set { _Rollo = LibString.Mid(value, 0, 15); }
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
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = 0;
            NumeroDocumento = "";
            consecutivoRenglon = 0;
            CodigoArticulo = "";
            Cantidad = 0;
            Serial = "";
            Rollo = "";
            TipoArticuloInvAsEnum = eTipoArticuloInv.Simple;
        }

        public RenglonTransferencia Clone() {
            RenglonTransferencia vResult = new RenglonTransferencia();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.NumeroDocumento = _NumeroDocumento;
            vResult.consecutivoRenglon = _consecutivoRenglon;
            vResult.CodigoArticulo = _CodigoArticulo;
            vResult.Cantidad = _Cantidad;
            vResult.Serial = _Serial;
            vResult.Rollo = _Rollo;
            vResult.TipoArticuloInvAsEnum = _TipoArticuloInv;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nNumero Documento = " + _NumeroDocumento +
               "\nconsecutivo Renglon = " + _consecutivoRenglon.ToString() +
               "\nCódigo Inventario = " + _CodigoArticulo +
               "\nCantidad = " + _Cantidad.ToString() +
               "\nSerial = " + _Serial +
               "\nRollo = " + _Rollo +
               "\nTipo Articulo Inv = " + _TipoArticuloInv.ToString();
        }

        #region Miembros de IEquatable<RenglonTransferencia>
        bool IEquatable<RenglonTransferencia>.Equals(RenglonTransferencia other) {
            if (this._ConsecutivoCompania == other.ConsecutivoCompania
                && this._NumeroDocumento == other.NumeroDocumento
                && this._consecutivoRenglon == other.consecutivoRenglon) {
                return true;
            } else {
                return false;
            }
        }
        #endregion //IEquatable<RenglonTransferencia>
        #endregion //Metodos Generados


    } //End of class RenglonTransferencia

} //End of namespace Galac.Saw.Ccl.Inventario

