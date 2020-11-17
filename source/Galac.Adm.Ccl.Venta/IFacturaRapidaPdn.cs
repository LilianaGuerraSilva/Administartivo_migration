using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using Galac.Comun.Ccl.SttDef;
namespace Galac.Adm.Ccl.Venta {

    public interface IFacturaRapidaPdn : ILibPdn {
        #region Metodos Generados
        XElement FindByNumero(int valConsecutivoCompania, string valNumero);
        #endregion //Metodos Generados
        LibResponse InsertarClienteDesdeFacturaRapida(string valNombre, string valNumeroRIF, string valDireccion, string valTelefono, ref string refCodigo, Saw.Ccl.Cliente.eTipoDocumentoIdentificacion valTipoDocumentoIdentificacion);
        bool ActualizarFacturaEmitida(int valConsecutivoCompania, XElement xData, string valNumeroBorrador, int valNumeroParaResumen);
        string GenerarNumeroDeFactura(int vConsecutivoCompania);
        int SiguienteNumeroParaResumen(int valConsecutivoCompania, XElement xData, string valSerialMaquinaFiscal);
        bool ExisteTasaDeCambioParaElDia(string valMoneda, DateTime valFecha, out decimal outTasa);
        bool InsertaTasaDeCambioParaElDia(string valMoneda, DateTime valFechaVigencia, string valNombre, decimal valCambioAbolivares);
        decimal TasaDeDolarVigente(string valMoneda);
    } //End of class IFacturaRapidaPdn

} //End of namespace Galac.Adm.Ccl.Venta

