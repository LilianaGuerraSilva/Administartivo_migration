using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Venta {

    public interface IFacturaPdn : ILibPdn {
        #region Metodos Generados
        XElement FindByNumero(int valConsecutivoCompania, string valNumero);
        #endregion //Metodos Generados

    } //End of class IFacturaPdn

} //End of namespace Galac.Adm.Ccl.Venta

