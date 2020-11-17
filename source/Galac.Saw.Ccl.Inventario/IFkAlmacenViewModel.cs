using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Saw.Ccl.Inventario {

    public interface IFkAlmacenViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          int Consecutivo { get; set; }
          string Codigo { get; set; }
          string NombreAlmacen { get; set; }
          eTipoDeAlmacen TipoDeAlmacen { get; set; }
          int ConsecutivoCliente { get; set; }
          string CodigoCliente { get; set; }
          string NombreCliente { get; set; }
          string CodigoCc { get; set; }
          string Descripcion { get; set; }
        #endregion //Propiedades


    } //End of class IFkAlmacenViewModel

} //End of namespace Galac.Saw.Ccl.Inventario

