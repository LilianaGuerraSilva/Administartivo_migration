using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Galac.Adm.Ccl.GestionCompras {

    public interface IServicioDeDatosCargaInicial {

        XElement ObtenerArticulosInsertarCargaInicial(int consecutivoCompania, string valMarca, DateTime valFechaInicial, string valCategoria, 
                                                        string valLinea, string valCodigoDesde, string valCodigoHasta, string valWhere=null, string valOrderBy=null);
        XElement ObtenerArticulosModificarCargaInicial(int consecutivoCompania, string valMarca, DateTime valFechaInicial, string valCategoria,
                                                        string valLinea, string valCodigoDesde, string valCodigoHasta);
        XElement ActualizarRecordModificado(int valConsecutivoCompania, int valConsecutivo);
        bool InsertarCargaDeArticulos(int valConsecutivoCompania, IEnumerable<ArticuloCargaInicial> cargaInicial, DateTime valFecha, bool valEsCargaInicial);
        bool ModificarCargaDeArticulos(int valConsecutivoCompania, IEnumerable<ArticuloCargaInicial> cargaDeArticulos, DateTime valFecha, bool valEsCargaInicial=false);
        bool ExisteAlMenosUnArticulo(int consecutivoCompania);
    }
}
