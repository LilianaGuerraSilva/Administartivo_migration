using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
namespace Galac.Saw.Ccl.Tablas {

    public interface IUnidadDeVentaPdn : ILibPdn {
        #region Metodos Generados
        XElement FindByNombreCodigo(string valNombre, string valCodigo);
        #endregion //Metodos Generados


    } //End of class IUnidadDeVentaPdn

} //End of namespace Galac.Saw.Ccl.Tablas

