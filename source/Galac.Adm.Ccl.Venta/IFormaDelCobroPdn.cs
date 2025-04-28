using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
namespace Galac.Adm.Ccl.Venta {

    public interface IFormaDelCobroPdn : ILibPdn {
        #region Metodos Generados
        XElement FindByConsecutivoCompaniaCodigo(int valConsecutivoCompania, string valCodigo);
        XElement FindByConsecutivoCompaniaNombre(int valConsecutivoCompania, string valNombre);
        void InsertDefaultRecord(int valConsecutivoCompania);
        #endregion //Metodos Generados


    } //End of class IFormaDelCobroPdn

} //End of namespace Galac.Adm.Ccl.Venta

