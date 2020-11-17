using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Adm.Ccl.CAnticipo {

    public interface IFkAnticipoViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          int ConsecutivoAnticipo { get; set; }
          eStatusAnticipo Status { get; set; }
          DateTime Fecha { get; set; }
          string Numero { get; set; }
          string CodigoCliente { get; set; }
          string NombreCliente { get; set; }
          string CodigoProveedor { get; set; }
          string NombreProveedor { get; set; }
          string NumeroCheque { get; set; }
          int ConsecutivoRendicion { get; set; }
          int ConsecutivoCaja { get; set; }
          decimal MontoTotal { get; set; }
          decimal MontoUsado { get; set; }
        #endregion //Propiedades


    } //End of class IFkAnticipoViewModel

} //End of namespace Galac.Adm.Ccl.CAnticipo

