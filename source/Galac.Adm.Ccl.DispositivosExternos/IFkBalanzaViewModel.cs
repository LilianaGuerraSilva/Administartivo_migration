using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Adm.Ccl.DispositivosExternos {

    public interface IFkBalanzaViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          int Consecutivo { get; set; }
          string Nombre { get; set; }
          eModeloDeBalanza Modelo { get; set; }
          ePuerto Puerto { get; set; }
        #endregion //Propiedades


    } //End of class IFkBalanzaViewModel

} //End of namespace Galac.Adm.Ccl.DispositivosExternos

