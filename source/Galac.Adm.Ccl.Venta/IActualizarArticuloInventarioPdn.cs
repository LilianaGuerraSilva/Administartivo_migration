using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Comun.Ccl.SttDef;
using System.Xml.Linq;

namespace Galac.Adm.Ccl.Venta {
   public interface IActualizarArticuloInventarioPdn {
      bool DescontarExistencia(int vConsecutivoCompania, string vNumeroDeFactura, string vCodigoAlmacen, eTipoDocumentoFactura vTipodeDocumento, DateTime vFechaFactura);
      bool DescontarEnAlmacen(int valConsecutivoCompania, string valNumeroFactura, string valCodigoAlmacen, eTipoDocumentoFactura valTipodeDocumento, DateTime valFechaFactura);
      bool DescontarExistenciaProductoCompuesto(int vConsecutivoCompania, string vNumeroDeFactura, string vCodigoAlmacen, eTipoDocumentoFactura vTipodeDocumento, DateTime vFechaFactura);
      bool DescontarEnAlmacenProductoCompuesto(int valConsecutivoCompania, string valNumeroFactura, string valCodigoAlmacen, eTipoDocumentoFactura valTipodeDocumento, DateTime valFechaFactura);
      XElement ConjuntoProductosCompuestos(int valConsecutivoCompania, string valNumeroFactura, eTipoDocumentoFactura valTipoDeDocumento);
   }
}
