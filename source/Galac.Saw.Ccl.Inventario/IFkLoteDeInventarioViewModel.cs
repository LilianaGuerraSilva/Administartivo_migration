using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Saw.Ccl.Inventario {

    public interface IFkLoteDeInventarioViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          int Consecutivo { get; set; }
          string CodigoLote { get; set; }
          string CodigoArticulo { get; set; }
          DateTime FechaDeElaboracion { get; set; }
          DateTime FechaDeVencimiento { get; set; }
          decimal Existencia { get; set; }
          eStatusLoteDeInventario StatusLoteInv { get; set; }
        #endregion //Propiedades


    } //End of class IFkLoteDeInventarioViewModel

} //End of namespace Galac.Saw.Ccl.Inventario

