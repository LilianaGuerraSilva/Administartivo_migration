using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Saw.Ccl.Inventario {

    public interface IFkSerialRolloViewModel {
        #region Propiedades
        int ConsecutivoCompania { get; set; }
        string CodigoAlmacen { get; set; }
        string CodigoArticulo { get; set; }
        string CodigoSerial { get; set; }
        string CodigoRollo { get; set; }
        decimal Cantidad { get; set; }
        int ConsecutivoAlmacen { get; set; }
        #endregion //Propiedades

    } //End of class IFkSerialRolloViewModel

} //End of namespace Galac.Saw.Ccl.Inventario
