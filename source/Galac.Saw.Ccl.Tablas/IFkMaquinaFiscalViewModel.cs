using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Saw.Ccl.Tablas {

    public interface IFkMaquinaFiscalViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          string ConsecutivoMaquinaFiscal { get; set; }
          string Descripcion { get; set; }
          string NumeroRegistro { get; set; }
          eStatusMaquinaFiscal Status { get; set; }
        #endregion //Propiedades


    } //End of class IFkMaquinaFiscalViewModel

} //End of namespace Galac.Saw.Ccl.Tablas

