using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Adm.Ccl.GestionCompras {

    public interface IFkCompraViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          int Consecutivo { get; set; }
          string Serie { get; set; }
          string Numero { get; set; }
          DateTime Fecha { get; set; }
          string CodigoProveedor { get; set; }
          string NombreProveedor { get; set; }
          string CodigoAlmacen { get; set; }
          eStatusCompra StatusCompra { get; set; }
        #endregion //Propiedades


    } //End of class IFkCompraViewModel

} //End of namespace Galac.Adm.Ccl.GestionCompras

