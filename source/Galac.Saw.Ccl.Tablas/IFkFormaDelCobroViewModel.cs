using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Galac.Saw.Ccl.Tablas {

    public interface IFkFormaDelCobroViewModel {
        #region Propiedades
          string Codigo { get; set; }
          string Nombre { get; set; }
          eTipoDeFormaDePago TipoDePago { get; set; }
        #endregion //Propiedades


    } //End of class IFkFormaDelCobroViewModel

} //End of namespace Galac.Saw.Ccl.Tablas

