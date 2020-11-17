using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Adm.Ccl.GestionCompras {

    public interface IFkOrdenDeCompraViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          int Consecutivo { get; set; }
          string Serie { get; set; }
          string Numero { get; set; }
          DateTime Fecha { get; set; }
          string CodigoProveedor { get; set; }
          string NombreProveedor { get; set; }
          eStatusCompra StatusOrdenDeCompra { get; set; }
        #endregion //Propiedades


    } //End of class IFkOrdenDeCompraViewModel

} //End of namespace Galac.Adm.Ccl.GestionCompras

