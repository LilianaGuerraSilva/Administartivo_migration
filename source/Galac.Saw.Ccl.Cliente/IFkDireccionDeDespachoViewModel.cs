using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Saw.Ccl.Cliente {

    public interface IFkDireccionDeDespachoViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          string CodigoCliente { get; set; }
          int ConsecutivoDireccion { get; set; }
          string PersonaContacto { get; set; }
          string Direccion { get; set; }
          string Ciudad { get; set; }
        #endregion //Propiedades


    } //End of class IFkDireccionDeDespachoViewModel

} //End of namespace Galac.Saw.Ccl.Cliente

