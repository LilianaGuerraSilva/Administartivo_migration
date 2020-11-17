using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Adm.Ccl.Banco {

    public interface IFkSolicitudesDePagoViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          int ConsecutivoSolicitud { get; set; }
          int NumeroDocumentoOrigen { get; set; }
          DateTime FechaSolicitud { get; set; }
          //eSolicitudGeneradaPor GeneradoPor { get; set; }
        #endregion //Propiedades


    } //End of class IFkSolicitudesDePagoViewModel

} //End of namespace Galac.Adm.Ccl.Banco

