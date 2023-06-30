using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Saw.Ccl.Inventario {

    public interface IFkBuscarUbicacionDeArticuloViewModel {
        #region Propiedades
        int ConsecutivoCompania { get; set; }
        string CodigoAlmacen { get; set; }
        string NombreAlmacen { get; set; }
        string CodigoArticulo { get; set; }
        string Descripcion { get; set; }
        string Ubicacion { get; set; }
        decimal Existencia { get; set; }
        #endregion //Propiedades


    } //End of class IFkBuscarUbicacionDeArticuloViewModel

} //End of namespace Galac.Saw.Ccl.Inventario

