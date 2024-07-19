using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Saw.Ccl.Inventario {

    public interface IFkNotaDeEntradaSalidaViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          string NumeroDocumento { get; set; }
          eTipodeOperacion TipodeOperacion { get; set; }
          string CodigoCliente { get; set; }
          string NombreCliente { get; set; }
          string CodigoAlmacen { get; set; }
          DateTime Fecha { get; set; }
          string Comentarios { get; set; }
          int ConsecutivoAlmacen { get; set; }
        #endregion //Propiedades


    } //End of class IFkNotaDeEntradaSalidaViewModel

} //End of namespace Galac.Saw.Ccl.Inventario

