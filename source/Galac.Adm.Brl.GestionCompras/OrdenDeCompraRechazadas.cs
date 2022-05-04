using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Brl.GestionCompras {
    [Serializable]
    public class OrdenDeCompraRechazadas : IEquatable<OrdenDeCompraRechazadas>, INotifyPropertyChanged, ICloneable {
        #region Variables
        private string _NumeroOC;
        #endregion //Variables
        #region Propiedades

        public string NumeroOC
        {
            get { return _NumeroOC; }
            set{
                _NumeroOC = LibString.Mid(value, 0, 20);
                OnPropertyChanged("NumeroOC");
            }
        }
        #endregion //Propiedades
        #region Constructores

        public OrdenDeCompraRechazadas() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            NumeroOC = string.Empty;
        }

        public OrdenDeCompraRechazadas Clone() {
            OrdenDeCompraRechazadas vResult = new OrdenDeCompraRechazadas();
            vResult.NumeroOC = _NumeroOC;
            return vResult;
        }

        public override string ToString() {
            return "NumeroOC = " + _NumeroOC.ToString();
        }

        #region Miembros de IEquatable<OrdenDeCompraDetalleArticuloInventario>
        bool IEquatable<OrdenDeCompraRechazadas>.Equals(OrdenDeCompraRechazadas other) {
            return Object.ReferenceEquals(this, other);
        }
        #endregion //IEquatable<OrdenDeCompraRechazadas>

        #region Miembros de ICloneable<OrdenDeCompraRechazadas>
        object ICloneable.Clone() {
            return this.Clone();
        }
        #endregion //ICloneable<OrdenDeCompraRechazadas>

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion //Metodos Generados


    } //End of class OrdenDeCompraDetalleArticuloInventario

} //End of namespace Galac.Adm.Ccl.GestionCompras

