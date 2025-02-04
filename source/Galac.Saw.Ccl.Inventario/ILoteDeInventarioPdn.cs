using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;

namespace Galac.Saw.Ccl.Inventario {
    public interface ILoteDeInventarioPdn : ILibPdn {
        XElement FindByConsecutivoCompaniaCodigoLoteCodigoArticulo(int valConsecutivoCompania, string valCodigoLote, string valCodigoArticulo);
        XElement FindByConsecutivoCompaniaConsecutivoLoteCodigoArticulo(int valConsecutivoCompania, int valCodigoLote, string valCodigoArticulo);
        XElement FindByConsecutivoCompaniaCodigoArticulo(int valConsecutivoCompania, string valCodigoArticulo);
        //LibResponse AgregarLote(IList<LoteDeInventario> valListaLote, bool valUsaDetalle);
        bool ActualizarLote(IList<LoteDeInventario> valListaLote);
        bool ExisteLoteDeInventario(int valConsecutivoCompania, string valCodigoArticulo, string valLoteDeInventario);
        bool ExisteLoteDeInventario(int valConsecutivoCompania, string valCodigoArticulo, int valConsecutivoLoteDeInventario);
        bool ActualizarLoteYReversarMov(IList<LoteDeInventario> valListaLote, eOrigenLoteInv valOrigen, int valConsecutivoDocumentoOrigen, string valNumeroDocumentoOrigen, bool valSoloAnulados);
        bool RecalcularMovimientosDeLoteDeInventario(int valConsecutivoCompania, eCantidadAImprimir valCantidadArticulos, string valCodigoArticulo, eCantidadAImprimir valCantidadLineas, string valLineaDeProducto);
    } //End of class ILoteDeInventarioPdn
} //End of namespace Galac.Saw.Ccl.Inventario
