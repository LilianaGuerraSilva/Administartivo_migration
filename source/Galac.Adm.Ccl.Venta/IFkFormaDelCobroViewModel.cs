using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Adm.Ccl.Venta {

    public interface IFkFormaDelCobroViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          int Consecutivo { get; set; }
          string Codigo { get; set; }
          string Nombre { get; set; }
          eFormaDeCobro TipoDeCobro { get; set; }
		  string CodigoCuentaBancaria {get; set;}
        #endregion //Propiedades


    } //End of class IFkFormaDelCobroViewModel

} //End of namespace Galac.Adm.Ccl.Venta

