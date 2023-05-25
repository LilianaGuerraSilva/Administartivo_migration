using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
namespace Galac.Saw.Ccl.Tablas {

    public interface IRutaDeComercializacionPdn : ILibPdn {
        #region Metodos Generados
        XElement FindByConsecutivoCompaniaDescripcion(int valConsecutivoCompania, string valDescripcion);
        #endregion //Metodos Generados


    } //End of class IRutaDeComercializacionPdn

} //End of namespace Galac.Saw.Ccl.Tablas

