using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
namespace Galac.Adm.Ccl.GestionProduccion {

    public interface IOrdenDeProduccionPdn : ILibPdn {
        #region Metodos Generados
        XElement FindByConsecutivo(int valConsecutivoCompania, int valConsecutivo);
        XElement FindByConsecutivoCompaniaCodigo(int valConsecutivoCompania, string valCodigo);
        #endregion //Metodos Generados
        

    } //End of class IOrdenDeProduccionPdn

} //End of namespace Galac.Adm.Ccl.GestionProduccion

