using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Dbo.Ccl.Inventario {

    public interface IFkRenglonNotaESViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          string NumeroDocumento { get; set; }
          int ConsecutivoRenglon { get; set; }
        #endregion //Propiedades


    } //End of class IFkRenglonNotaESViewModel

} //End of namespace Galac.Dbo.Ccl.Inventario

