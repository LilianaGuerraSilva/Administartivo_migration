using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Vendedor {
    public interface IVendedorPdn : ILibPdn {
        System.Xml.Linq.XElement VendedorPorDefecto(int valConsecutivoCompania);
        string BuscarNombreVendedor(int valConsecutivoCompania, string valCodigo);
        void CalculaMontoPorcentajeYNivelDeComisionInforme(string valCodigo, decimal valMontoComisionable, decimal valMontoComisionableEnMonedaLocal, ref decimal refMontoComision, ref decimal refPorcentajeComision, ref string refNivelDeComision);

        void InsertarElPrimerVendedor(int valConsecutivoCompania);
        int RecordCount(int valConsecutivoCompania);
    }
}
