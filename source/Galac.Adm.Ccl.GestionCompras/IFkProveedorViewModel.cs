using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Adm.Ccl.GestionCompras {

    public interface IFkProveedorViewModel {
        #region Propiedades
          int ConsecutivoCompania { get; set; }
          string CodigoProveedor { get; set; }
          string NombreProveedor { get; set; }
          string Contacto { get; set; }
          string Telefonos { get; set; }
          string NombrePaisResidencia { get; set; }
          string PaisConveniosSunat { get; set; }
        #endregion //Propiedades


    } //End of class IFkProveedorViewModel

} //End of namespace Galac.Adm.Ccl.GestionCompras

