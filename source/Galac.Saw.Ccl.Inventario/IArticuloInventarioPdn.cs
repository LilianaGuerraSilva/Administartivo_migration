using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using System.Xml.Linq;
using System.Collections;

namespace Galac.Saw.Ccl.Inventario {
    public interface IArticuloInventarioPdn : ILibPdn {
        decimal DisponibilidadDeArticulo(int valConsecutivoCompania, string valCodigoAlmacen, string valCodigoArticulo, int valTipoDeArticulo, string valSerial, string valRollo);
        XElement BuscarDetalleArticuloCompuesto(int valConsecutivoCompania, string valCodigoArticulo);
        bool ActualizarExistencia(int valConsecutivoCompania, List<ArticuloInventarioExistencia> valList);
        bool ActualizarCostoUnitario(int valConsecutivoCompania, XElement valDataArticulo, bool valEsMonedaLocal);
        bool ValidaExistenciaDeArticuloSerial(int valConsecutivoCompania, XElement valDataArticulo);
        bool AjustaPreciosxCostos(bool valFormulaAlternativa, int valConsecutivoCia, string valMarca, string valDesde, string valHasta, eRedondearPrecio valRedondeo, ePrecioAjustar valPrecioConOSinIVA, bool valMargenesNuevos, string valLineaProducto, string valCategoria, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4, decimal valMargen1, decimal valMargen2, decimal valMargen3, decimal valMargen4, bool valVieneDeCompras, DateTime valFechaOperacion, string valNumero, string valOperacion, bool valMonedaLocal);
        void ProcesaCostoPromedio(int valConsecutivoCompania, bool valVieneDeOperaciones, DateTime valFechaOperacion, string valCodigo, string valDocumento, string valOperacion);
        XElement DisponibilidadDeArticuloPorAlmacen(int valConsecutivoCompania, XElement valDataArticulo);
    }
}
