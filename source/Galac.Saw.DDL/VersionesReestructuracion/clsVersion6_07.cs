using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Brl;
using System.Data;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_07 : clsVersionARestructurar {
        public clsVersion6_07(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.07";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarParametroUsaCobroDirectoEnMultimoneda();
            AgregarCamposEnRenglonCobroDeFactura();
            AgregarCamposEnFactura();
            ValidarQueExistanFormasDeCobroParaMultioneda();
            ValidarQueExistaBancoGenerico();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarParametroUsaCobroDirectoEnMultimoneda() {
            AgregarNuevoParametro("UsaCobroDirectoEnMultimoneda", "Factura", 2, "2.2.- Facturación (Continuación)", 2, "", '2', "", 'N', "N");
            AgregarNuevoParametro("SeMuestraTotalEnDivisas", "Factura", 2, "2.2.- Facturación (Continuación)", 2, "", '2', "", 'N', "N");
            AgregarNuevoParametro("CuentaBancariaCobroMultimoneda", "Factura", 2, "2.2.- Facturación (Continuación)", 2, "", '2', "", 'N', string.Empty);
            AgregarNuevoParametro("ConceptoBancarioCobroMultimoneda", "Factura", 2, "2.2.- Facturación (Continuación)", 2, "", '2', "", 'N', string.Empty);
        }

        private void AgregarCamposEnRenglonCobroDeFactura() {
            AddColumnString("dbo.renglonCobroDeFactura", "CodigoMoneda", 4, "", "VES");
            AddColumnCurrency("dbo.renglonCobroDeFactura", "CambioAMonedaLocal", "", 1);
        }
                
        private void AgregarCamposEnFactura() {
            AddColumnCurrency("dbo.Factura", "CambioMostrarTotalEnDivisas", string.Empty, 1);
        }

        private void ValidarQueExistanFormasDeCobroParaMultioneda() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("IF NOT EXISTS (SELECT TOP(1) Codigo FROM Saw.FormaDelCobro WHERE Codigo = '00001')");
            vSql.AppendLine("BEGIN");
            vSql.AppendLine("       INSERT INTO Saw.FormaDelCobro (Codigo, Nombre, TipoDePago) VALUES ('00001', 'EFECTIVO', 0)");
            vSql.AppendLine("END");
            Execute(vSql.ToString(), 0);
            vSql = new StringBuilder();
            vSql.AppendLine("IF NOT EXISTS (SELECT TOP(1) Codigo FROM Saw.FormaDelCobro WHERE Codigo = '00003')");
            vSql.AppendLine("BEGIN");
            vSql.AppendLine("       INSERT INTO Saw.FormaDelCobro (Codigo, Nombre, TipoDePago) VALUES ('00003', 'TARJETA', 1)");
            vSql.AppendLine("END");
            Execute(vSql.ToString(), 0);
            vSql = new StringBuilder();
            vSql.AppendLine("IF NOT EXISTS (SELECT TOP(1) Codigo FROM Saw.FormaDelCobro WHERE Codigo = '00006')");
            vSql.AppendLine("BEGIN");
            vSql.AppendLine("       INSERT INTO Saw.FormaDelCobro (Codigo, Nombre, TipoDePago) VALUES ('00006', 'TRANSFERENCIA', 3)");
            vSql.AppendLine("END");
            Execute(vSql.ToString(), 0);
        }

        private void ValidarQueExistaBancoGenerico() {
            StringBuilder vSql = new StringBuilder();
            string vFechaUltimaModificacion = LibDate.Today().ToShortDateString();
            vSql.AppendLine("IF NOT EXISTS (SELECT TOP(1) Codigo FROM Comun.Banco WHERE Codigo = '37')");
            vSql.AppendLine("BEGIN");
            vSql.AppendLine("   INSERT INTO Comun.Banco (Consecutivo, Codigo, Nombre, Status, fldOrigen, NombreOperador, FechaUltimaModificacion)");
            vSql.AppendLine("   VALUES ((SELECT MAX(Consecutivo) From Comun.Banco) + 1, '37', 'BANCO GENÉRICO COBRO MULTIMONEDA', 0, 0, 'JEFE', " + vFechaUltimaModificacion + ")");
            vSql.AppendLine("END");
            Execute(vSql.ToString(), 0);
        }
    }
}
