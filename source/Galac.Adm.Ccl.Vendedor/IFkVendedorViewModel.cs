using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.Ccl.Vendedor {
    public interface IFkVendedorViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
		  int Consecutivo { get; set; }
          string Codigo { get; set; }
          string Nombre { get; set; }
          string RIF { get; set; }
          eStatusVendedor StatusVendedor { get; set; }
          string Ciudad { get; set; }
          string ZonaPostal { get; set; }
          string Telefono { get; set; }
        #endregion //Propiedades


    } //End of class IFkVendedorViewModel

} //End of namespace Galac.Saw.Ccl.Vendedor

