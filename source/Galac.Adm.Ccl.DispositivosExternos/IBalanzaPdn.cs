using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
namespace Galac.Adm.Ccl.DispositivosExternos {

    public interface IBalanzaPdn : ILibPdn {
        #region Metodos Generados
        XElement FindByConsecutivoCompaniaModeloNombre(int valConsecutivoCompania, eModeloDeBalanza valModelo, string valNombre);
        #endregion //Metodos Generados


    } //End of class IBalanzaPdn

} //End of namespace Galac.Adm.Ccl.DispositivosExternos

