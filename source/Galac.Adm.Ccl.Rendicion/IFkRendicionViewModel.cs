using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Adm.Ccl.CajaChica {

    public interface IFkRendicionViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          int Consecutivo { get; set; }
          string Numero { get; set; }
          DateTime FechaApertura { get; set; }
          string CodigoCtaBancariaCajaChica { get; set; }
          string NombreCuentaBancariaCajaChica { get; set; }
          eStatusRendicion StatusRendicion { get; set; }
        #endregion //Propiedades


    } //End of class IFkRendicionViewModel

} //End of namespace Galac.Adm.Ccl.CajaChica

