using LibGalac.Aos.Uil.Usal.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Adm.Ccl.Venta {

    public interface IFkCajaAperturaViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          int Consecutivo { get; set; }
          int ConsecutivoCaja { get; set; }
          string NombreCaja { get; set; }
        #endregion //Propiedades
    }
    

    //End of class IFkCajaAperturaViewModel

} //End of namespace Galac.Adm.Ccl.Venta

