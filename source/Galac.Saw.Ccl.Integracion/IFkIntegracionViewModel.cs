using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Saw.Ccl.Integracion;
namespace Galac.Saw.Ccl.Integracion {

    public interface IFkIntegracionViewModel {
        #region Propiedades
          eTipoIntegracion TipoIntegracion { get; set; }
          string version { get; set; }
        #endregion //Propiedades


    } //End of class IFkIntegracionViewModel

} //End of namespace Galac.Saw.Ccl.Integracion

