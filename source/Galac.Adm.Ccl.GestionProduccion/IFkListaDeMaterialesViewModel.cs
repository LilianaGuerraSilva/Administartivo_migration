using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Adm.Ccl.GestionProduccion {

    public interface IFkListaDeMaterialesViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          int Consecutivo { get; set; }
          string Codigo { get; set; }
          string CodigoArticuloInventario { get; set; }
          DateTime FechaCreacion { get; set; }
        #endregion //Propiedades


    } //End of class IFkListaDeMaterialesViewModel

} //End of namespace Galac.Adm.Ccl.GestionProduccion

