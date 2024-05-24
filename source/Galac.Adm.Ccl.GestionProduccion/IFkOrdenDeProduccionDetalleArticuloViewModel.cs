using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Adm.Ccl.GestionProduccion {

    public interface IFkOrdenDeProduccionDetalleArticuloViewModel {
        #region Propiedades
        int ConsecutivoCompania { get; set; }
        int ConsecutivoOrdenDeProduccion { get; set; }
        int Consecutivo { get; set; }
        string CodigoArticulo { get; set; }
        string DescripcionArticulo { get; set; }

        #endregion //Propiedades


    } //End of class IFkOrdenDeProduccionDetalleArticuloViewModel

} //End of namespace Galac.Adm.Ccl.GestionProduccion

