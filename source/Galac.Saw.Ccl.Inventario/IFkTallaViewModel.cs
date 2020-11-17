using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Saw.Ccl.Inventario {

    public interface IFkTallaViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          string CodigoTalla { get; set; }
          string DescripcionTalla { get; set; }
        #endregion //Propiedades


    } //End of class IFkTallaViewModel

} //End of namespace Galac.Saw.Ccl.Inventario

