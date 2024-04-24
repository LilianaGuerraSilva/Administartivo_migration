using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
namespace Galac.Adm.Ccl.GestionProduccion {

    public interface IListaDeMaterialesPdn : ILibPdn {
        #region Metodos Generados
        XElement FindByConsecutivoCompaniaCodigo(int valConsecutivoCompania, string valCodigo);
        XElement FindByConsecutivoCompaniaNombre(int valConsecutivoCompania, string valNombre);
        string NombreParaMostrarListaDeMateriales();
        bool ExisteListaDeMaterialesConEsteCodigo(int valConsecutivoCompania, string valCodigo);
        bool ExisteListaDeMaterialesConEsteNombre(int valConsecutivoCompania, string valNombre);
		#endregion //Metodos Generados
    } //End of class IListaDeMaterialesPdn

} //End of namespace Galac.Adm.Ccl.GestionProduccion

