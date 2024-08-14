using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
namespace Galac.Saw.Ccl.Inventario {

    public interface ILoteDeInventarioPdn : ILibPdn {
        #region Metodos Generados
        XElement FindByConsecutivoCompaniaCodigoLoteCodigoArticulo(int valConsecutivoCompania, string valCodigoLote, string valCodigoArticulo);
        XElement FindByConsecutivoCompaniaCodigoArticulo(int valConsecutivoCompania, string valCodigoArticulo);
        LibResponse AgregarLote(IList<LoteDeInventario> valListaLote, bool valUsaDetalle);
        LibResponse ActualizarLote(IList<LoteDeInventario> valListaLote);
        bool ExisteLoteDeInventario(int valConsecutivoCompania, string valCodigoArticulo, string valLoteDeInventario);
        #endregion //Metodos Generados


    } //End of class ILoteDeInventarioPdn

} //End of namespace Galac.Saw.Ccl.Inventario

