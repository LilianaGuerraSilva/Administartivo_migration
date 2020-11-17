using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
namespace Galac.Adm.Ccl.GestionCompras {

    public interface IOrdenDeCompraPdn : ILibPdn {
        #region Metodos Generados
        XElement FindBySerie(int valConsecutivoCompania, string valSerie);
        XElement FindByNumero(int valConsecutivoCompania, string valNumero);
        XElement FindByConsecutivoCompaniaSerieNumeroConsecutivoProveedor(int valConsecutivoCompania, string valSerie, string valNumero, int valConsecutivoProveedor);
        string FindNextNumeroBySerieYTipoDeCompra(int valConsecutivoCompania, string valSerie, eTipoCompra valTipoDeCompra);
        #endregion //Metodos Generados


    } //End of class IOrdenDeCompraPdn

} //End of namespace Galac.Adm.Ccl.GestionCompras

