using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
namespace Galac.Adm.Ccl.GestionProduccion {

    public interface IOrdenDeProduccionPdn : ILibPdn {        
        XElement FindByConsecutivo(int valConsecutivoCompania, int valConsecutivo);
        XElement FindByConsecutivoCompaniaCodigo(int valConsecutivoCompania, string valCodigo);
        List<OrdenDeProduccionDetalleMateriales> ObtenerDetalleInicialInsumos(int valConsecutivoCompania, int valConsecutivoListaDeMateriales, int valConsecutivoAlmacen, decimal valCantidadSolicitada);
        List<OrdenDeProduccionDetalleArticulo> ObtenerDetalleInicialSalidas(int valConsecutivoCompania, int valConsecutivoAlmacenEntrada, int valConsecutivoListaDeMateriales, decimal valCantidadSolicitada);        
    } //End of class IOrdenDeProduccionPdn

} //End of namespace Galac.Adm.Ccl.GestionProduccion

