using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Galac.Adm.Ccl.GestionProduccion {
    public interface IOrdenDeProduccionDetalleMaterialesPdn {
        //List<OrdenDeProduccionDetalleMateriales> ObtenerDetalleInicialDeListaDemateriales(int valConsecutivoCompania, int valConsecutivoListaDeMateriales, int valConsecutivoAlmacen, decimal valCantidadSolicitada);        
        XElement BuscaExistenciaDeArticulos(int valConsecutivoCompania, IList<OrdenDeProduccionDetalleMateriales> valData);
        decimal BuscaExistenciaDeArticulo(int valConsecutivoCompania, string valCodigoArticulo, int valConsecutivoAlmacen);
    }
}
