using LibGalac.Aos.UI.Mvvm;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Uil.GestionCompras {

    public class ArticuloCargaInicialViewModel : ObservableObject {

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

        public ArticuloCargaInicialViewModel(CargaInicial articulo) {
            this.articulo = articulo;
        }

        public ArticuloCargaInicial ToArticuloCargaInicial() {
            return new ArticuloCargaInicial(articulo);
        }
    }
}
