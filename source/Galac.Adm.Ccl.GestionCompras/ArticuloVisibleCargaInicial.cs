using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.UI.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.Ccl.GestionCompras {

    public class ArticuloVisibleCargaInicial : ObservableObject {

        public CargaInicial articulo;
        private const string CantidadPropertyName = "Cantidad";
        private const string CodigoArticuloPropertyName = "CodigoArticulo";
        private const string CostoPropertyName = "Costo";

        public string CodigoArticulo {
            get { return articulo.CodigoArticulo; }
            set {
                if(articulo.CodigoArticulo != value) {
                    articulo.CodigoArticulo = value;
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                }
            }
        }

        public decimal Costo {
            get { return articulo.Costo; }
            set {
                if (articulo.Costo !=value) {
                    articulo.Costo = value;
                    RaisePropertyChanged(CostoPropertyName);
                }
            }
        }


        public decimal Cantidad {
            get { return articulo.Existencia; }
            set {
                if (articulo.Existencia!=value) {
                    articulo.Existencia = value;
                    RaisePropertyChanged(CantidadPropertyName);
                }
            }
        }

        public ArticuloVisibleCargaInicial(CargaInicial articulo) {
            this.articulo = articulo;
        }
    }
}
