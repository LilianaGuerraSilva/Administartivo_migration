namespace Galac.Adm.Ccl.GestionCompras {
    public class ArticuloCargaInicial {

        public CargaInicial articulo { get; set;  }

        public ArticuloCargaInicial(CargaInicial articulo) {
            this.articulo = articulo;
        }

        public string CodigoArticulo {
            get { return articulo.CodigoArticulo; }
            set { articulo.CodigoArticulo = value; }
        }

        public decimal Costo {
            get { return articulo.Costo; }
            set { articulo.Costo = value; }
        }

        public decimal Cantidad {
            get { return articulo.Existencia; }
            set { articulo.Existencia = value; }
        }

    }
}