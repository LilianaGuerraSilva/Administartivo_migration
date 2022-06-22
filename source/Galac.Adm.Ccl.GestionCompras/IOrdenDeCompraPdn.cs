using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using System.ComponentModel;

namespace Galac.Adm.Ccl.GestionCompras {

    public interface IOrdenDeCompraPdn : ILibPdn {
        #region Metodos Generados
        XElement FindBySerie(int valConsecutivoCompania, string valSerie);
        XElement FindByNumero(int valConsecutivoCompania, string valNumero);
        XElement FindByConsecutivoCompaniaSerieNumeroConsecutivoProveedor(int valConsecutivoCompania, string valSerie, string valNumero, int valConsecutivoProveedor);
        string FindNextNumeroBySerieYTipoDeCompra(int valConsecutivoCompania, string valSerie, eTipoCompra valTipoDeCompra);
        bool ValidaNumeroOC(string valNumero, int valConsecutivoCompania);
        bool ValidaProveedor(int valConsecutivoCompania, string valCodigoProveedor);
        bool ValidaCondicionesPago(int valConsecutivoCompania, int valCondicionesDePago);
        bool ValidaCodigoArticulo(int valConsecutivoCompania, string valCodigoArticulo);
        bool ValidaCondicionesdeImportacion(string valCondicionesImportacion);
        int ValidaTipoDeCompra(string valTipoDeCompra);
        int InfoProveedor(int valConsecutivoCompania, string valCodigoProveedor);
        decimal CalculaTotalRenglon(string ValCodigo, decimal valCambioABolivares, decimal valCantidad, decimal valCosto);
        decimal CalculaTotalRenglonOC(XElement valRecord, string valNumero, string valMoneda, decimal valCambio);
        string InfoArticulo(int valConsecutivoCompania, string valCodigoArticulo);
        int InfoNumeroOC(int valConsecutivoCompania, string valNumero);
        #endregion //Metodos Generados


    } //End of class IOrdenDeCompraPdn

} //End of namespace Galac.Adm.Ccl.GestionCompras

