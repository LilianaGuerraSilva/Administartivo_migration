using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Ccl.Inventario {
    [Serializable]
    public class LoteDeInventarioMovimiento: IEquatable<LoteDeInventarioMovimiento>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private int _ConsecutivoCompania;
        private int _ConsecutivoLote;
        private int _Consecutivo;
        private DateTime _Fecha;
        private eOrigenLoteInv _Modulo;
        private decimal _Cantidad;
        private int _ConsecutivoDocumentoOrigen;
        private string _NumeroDocumentoOrigen;
        private eStatusDocOrigenLoteInv _StatusDocumentoOrigen;
        #endregion //Variables
        #region Propiedades

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }

        public int ConsecutivoLote {
            get { return _ConsecutivoLote; }
            set { _ConsecutivoLote = value; }
        }

        public int Consecutivo {
            get { return _Consecutivo; }
            set { _Consecutivo = value; }
        }

        public DateTime Fecha {
            get { return _Fecha; }
            set { 
                _Fecha = LibConvert.DateToDbValue(value);
                OnPropertyChanged("Fecha");
            }
        }

        public eOrigenLoteInv ModuloAsEnum {
            get { return _Modulo; }
            set { 
                _Modulo = value;
                OnPropertyChanged("Modulo");
            }
        }

        public string Modulo {
            set { _Modulo = (eOrigenLoteInv)LibConvert.DbValueToEnum(value); }
        }

        public string ModuloAsDB {
            get { return LibConvert.EnumToDbValue((int) _Modulo); }
        }

        public string ModuloAsString {
            get { return LibEnumHelper.GetDescription(_Modulo); }
        }

        public decimal Cantidad {
            get { return _Cantidad; }
            set { 
                _Cantidad = value;
                OnPropertyChanged("Cantidad");
            }
        }

        public int ConsecutivoDocumentoOrigen {
            get { return _ConsecutivoDocumentoOrigen; }
            set { _ConsecutivoDocumentoOrigen = value; }
        }

        public string NumeroDocumentoOrigen {
            get { return _NumeroDocumentoOrigen; }
            set { 
                _NumeroDocumentoOrigen = LibString.Mid(value, 0, 30);
                OnPropertyChanged("NumeroDocumentoOrigen");
            }
        }

        public eStatusDocOrigenLoteInv StatusDocumentoOrigenAsEnum {
            get { return _StatusDocumentoOrigen; }
            set { 
                _StatusDocumentoOrigen = value;
                OnPropertyChanged("StatusDocumentoOrigen");
            }
        }

        public string StatusDocumentoOrigen {
            set { _StatusDocumentoOrigen = (eStatusDocOrigenLoteInv)LibConvert.DbValueToEnum(value); }
        }

        public string StatusDocumentoOrigenAsDB {
            get { return LibConvert.EnumToDbValue((int) _StatusDocumentoOrigen); }
        }

        public string StatusDocumentoOrigenAsString {
            get { return LibEnumHelper.GetDescription(_StatusDocumentoOrigen); }
        }
        #endregion //Propiedades
        #region Constructores

        public LoteDeInventarioMovimiento() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            ConsecutivoLote = 0;
            Consecutivo = 0;
            Fecha = LibDate.Today();
            ModuloAsEnum = eOrigenLoteInv.Factura;
            Cantidad = 0;
            ConsecutivoDocumentoOrigen = 0;
            NumeroDocumentoOrigen = string.Empty;
            StatusDocumentoOrigenAsEnum = eStatusDocOrigenLoteInv.Vigente;
        }

        public LoteDeInventarioMovimiento Clone() {
            LoteDeInventarioMovimiento vResult = new LoteDeInventarioMovimiento();
            vResult.ConsecutivoCompania = _ConsecutivoCompania;
            vResult.ConsecutivoLote = _ConsecutivoLote;
            vResult.Consecutivo = _Consecutivo;
            vResult.Fecha = _Fecha;
            vResult.ModuloAsEnum = _Modulo;
            vResult.Cantidad = _Cantidad;
            vResult.ConsecutivoDocumentoOrigen = _ConsecutivoDocumentoOrigen;
            vResult.NumeroDocumentoOrigen = _NumeroDocumentoOrigen;
            vResult.StatusDocumentoOrigenAsEnum = _StatusDocumentoOrigen;
            return vResult;
        }

        public override string ToString() {
           return "Consecutivo Compania = " + _ConsecutivoCompania.ToString() +
               "\nConsecutivo Lote = " + _ConsecutivoLote.ToString() +
               "\nConsecutivo = " + _Consecutivo.ToString() +
               "\nFecha = " + _Fecha.ToShortDateString() +
               "\nMódulo = " + _Modulo.ToString() +
               "\nCantidad = " + _Cantidad.ToString() +
               "\nConsecutivo Documento Origen = " + _ConsecutivoDocumentoOrigen.ToString() +
               "\nNúmero Doc. Origen = " + _NumeroDocumentoOrigen +
               "\nStatus del Origen = " + _StatusDocumentoOrigen.ToString();
        }

        #region Miembros de IEquatable<LoteDeInventarioMovimiento>
        bool IEquatable<LoteDeInventarioMovimiento>.Equals(LoteDeInventarioMovimiento other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<LoteDeInventarioMovimiento>

        #region Miembros de ICloneable<LoteDeInventarioMovimiento>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<LoteDeInventarioMovimiento>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class LoteDeInventarioMovimiento

} //End of namespace Galac.Saw.Ccl.Inventario

