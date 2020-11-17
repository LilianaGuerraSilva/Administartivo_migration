using System;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Ccl.GestionCompras {
    public interface ICompraPdn {
        bool ExisteTasaDeCambioParaElDia(string valMoneda, DateTime valFecha, out decimal outTasa);
        bool InsertaTasaDeCambioParaElDia(string valMoneda, DateTime valFechaVigencia, string valNombre, decimal valCambioAbolivares);
        bool CambiarStatusCompra(Compra valCompra, eAccionSR valAction);
        decimal TasaDeDolarVigente(string valMoneda);
        bool GenerarCxP(Compra valRecord, string valNumeroControl, eAccionSR valAction);
        XElement BuscarSerial(int valConsecutivoCompania, string valCodigoArticulo, string valCodigoGrupo);
        void ActualizaElCostoUnitario(Compra valRecord, bool valEsMonedaLocal);
        void ProcesaCostoPromedio(int valConsecutivoCompania, bool valVieneDeOperaciones, DateTime valFechaOperacion, string valCodigo, string valDocumento, string valOperacion);
        bool SePuedeEjecutarElAjusteDePrecios();
        bool  AjustaPreciosxCostos(bool valUsaFormulaAlterna, int valConsecutivoCompania, eRedondearPrecio valRedondearPrecio, ePrecioAjustar valPrecioAjustar, bool valEstablecerMargen, bool valPrecio1, bool valPrecio2, bool valPrecio3, bool valPrecio4, decimal valMargen1, decimal valMargen2, decimal valMargen3, decimal valMargen4, DateTime valFechaOperacion, string valNumeroOperacion,bool esMonedaLocal);
        void AsignarDetalleArticuloInventarioDesdeOrdenDeCompra(int valConsecutivoCompania, Compra valCompra, int valConsecutivoOrdenDeCompra);
        bool VerificaExistenciaEnOrdenDeCompra(int valConsecutivoCompania, int valConsecutivoOrdenDeCompra);
    }
}
   