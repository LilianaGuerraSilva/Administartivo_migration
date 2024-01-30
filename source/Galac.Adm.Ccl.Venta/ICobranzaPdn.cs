using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Galac.Saw.Lib;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Venta {
    public interface ICobranzaPdn : ILibPdn {
        XElement FindByNumero(int valConsecutivoCompania, string valNumero);
        string GenerarProximoNumeroCobranza(int valConsecutivoCompania);
        string GenerarSiguienteNumeroDeCobranzaAPartirDe(string valNumeroDeCobranza);
        bool InsertarCobranzaDesdePuntoDeVenta(int valConsecutivoCompania, XElement valData, string valNumeroCobranza);
        bool InsertarDocumentoCobradoDesdePuntoDeVenta(int valConsecutivoCompania, XElement valData, string valNumeroCobranza);
        void InsertarCobranzasDeCobroEnMultimoneda(int valConsecutivoCompania, XElement valDatosFactura, XElement valDatosRenglonCobro, XElement valDataCXC, out List<string> outNumerosDeCobranzas);
        void InsertarCobranzaDeNotaDeCredito(int valConsecutivoCompania, XElement valDatosFactura, XElement valDatosRenglonCobro, eTipoDeTransaccion vTipoDeCxc, out string outNumerosDeCobranzas);
    }
}
