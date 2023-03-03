using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Adm.Ccl. GestionProduccion {

    public interface IFkOrdenDeProduccionViewModel {
        #region Propiedades
        int ConsecutivoCompania { get; set; }
        int Consecutivo { get; set; }
        string Codigo { get; set; }
        eTipoStatusOrdenProduccion StatusOP { get; set; }
        DateTime FechaAnulacion { get; set; }
        DateTime FechaFinalizacion { get; set; }

        #endregion //Propiedades
    } //End of class IFkOrdenDeProduccionViewModel

} //End of namespace Galac.Adm.Ccl. GestionProduccion

