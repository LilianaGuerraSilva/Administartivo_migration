using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Saw.Ccl.Inventario {

    public interface IFkAsignarBalanzaViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }         
          string LineaDeProducto { get; set; }
          string ArticuloDesde { get; set; }
          string ArticuloHasta { get; set; }
        #endregion //Propiedades


    } //End of class IFkInventarioAsignarBalanzaViewModel

} //End of namespace Galac.Saw.Ccl.Inventario

