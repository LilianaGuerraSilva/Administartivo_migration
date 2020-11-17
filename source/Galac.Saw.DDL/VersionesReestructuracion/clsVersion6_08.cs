using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Brl;
using System.Data;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_08 : clsVersionARestructurar {
        public clsVersion6_08(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.08";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarMonedaDeCobroEnFactura();
            CrearTablaCambio();
            MigrarTablaCambio();
            BorrarTabladboCambio();
            AjustarRegistrosConCambioIgualACero();
            AgregarCampoEnumerativoMoneda();
            AgregarNuevosCamposEnParametrosConciliacion();
            ValidarQueExistanFormasDeCobroParaMultioneda();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarMonedaDeCobroEnFactura() {
            StringBuilder vSQL = new StringBuilder();
            AddColumnString("dbo.factura","CodigoMonedaDeCobro",4, "", "");
            if (ColumnExists("dbo.factura", "CodigoMonedaDeCobro")) {
                if (!ForeignKeyNameExists("fk_FacturaMonedaCobro")) {
                    vSQL.AppendLine("WHILE EXISTS (SELECT CodigoMoneda FROM dbo.factura WHERE dbo.factura.CodigoMonedaDeCobro = '')");
                    vSQL.AppendLine("BEGIN");
                    vSQL.AppendLine("   UPDATE TOP (1000) dbo.factura ");
                    vSQL.AppendLine("       SET    dbo.factura.CodigoMonedaDeCobro = dbo.factura.CodigoMoneda");
                    vSQL.AppendLine("   FROM  dbo.factura");
                    vSQL.AppendLine("   WHERE dbo.factura.CodigoMonedaDeCobro = ''");
                    vSQL.AppendLine("END");
                    Execute(vSQL.ToString(), 0);
                    AddForeignKey("dbo.Moneda", "dbo.factura", new string[] { "Codigo" }, new string[] { "CodigoMonedaDeCobro" }, false, false);
                }
            }
        }

        #region Cambio

        private void CrearTablaCambio() {
            if (!TableExists("Comun.Cambio")) {
                new Galac.Comun.Dal.TablasGen.clsCambioED().InstalarTabla();
            }
        }

        private void MigrarTablaCambio() {
            if (TableExists("Comun.Cambio") && TableExists("dbo.Cambio")) {
                new Galac.Saw.DbMigrator.clsMigrarData(new string[] { "Cambio" }).MigrarData();
            }
        }

        private void BorrarTabladboCambio() {
            if (TableExists("dbo.Cambio")) {
                ExecuteDropTable("dbo.Cambio");
            }
        }

        private void AjustarRegistrosConCambioIgualACero() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Comun.Cambio");
            vSql.AppendLine("SET CambioAMonedaLocal = 1, CambioAMonedaLocalVenta = 1");
            vSql.AppendLine("WHERE CambioAMonedaLocal = 0");
            Execute(vSql.ToString(), 0);
        }

        #endregion //Cambio

        #region Moneda

        private void AgregarCampoEnumerativoMoneda() {
            if (TableExists("dbo.Moneda")) {
                if (!ColumnExists("dbo.Moneda", "TipoDeMoneda")) {
                    AddColumnEnumerative("dbo.Moneda", "TipoDeMoneda", " d_MonTipoDeMoneda NOT NULL", "0");
                    StringBuilder vSql = new StringBuilder();
                    vSql.AppendLine("INSERT INTO dbo.Moneda (Codigo, Nombre, Simbolo, Activa, TipoDeMoneda) VALUES ('1PT', 'Petro', '', 'S', '1');");
                    vSql.AppendLine("INSERT INTO dbo.Moneda (Codigo, Nombre, Simbolo, Activa, TipoDeMoneda) VALUES ('1BT', 'Bitcoin', '', 'S', '1');");
                    vSql.AppendLine("INSERT INTO dbo.Moneda (Codigo, Nombre, Simbolo, Activa, TipoDeMoneda) VALUES ('9DC', 'Dicon', '', 'S', '2');");
                    vSql.AppendLine("INSERT INTO dbo.Moneda (Codigo, Nombre, Simbolo, Activa, TipoDeMoneda) VALUES ('9E1', 'Estimado 1', '', 'S', '2');");
                    vSql.AppendLine("INSERT INTO dbo.Moneda (Codigo, Nombre, Simbolo, Activa, TipoDeMoneda) VALUES ('9E2', 'Estimado 2', '', 'S', '2');");
                    vSql.AppendLine("INSERT INTO dbo.Moneda (Codigo, Nombre, Simbolo, Activa, TipoDeMoneda) VALUES ('9E3', 'Estimado 3', '', 'S', '2');");
                    vSql.AppendLine("INSERT INTO dbo.Moneda (Codigo, Nombre, Simbolo, Activa, TipoDeMoneda) VALUES ('9E4', 'Estimado 4', '', 'S', '2');");
                    vSql.AppendLine("INSERT INTO dbo.Moneda (Codigo, Nombre, Simbolo, Activa, TipoDeMoneda) VALUES ('9E5', 'Estimado 5', '', 'S', '2');");
                    Execute(vSql.ToString(), -1);
                }
            }
        }

        #endregion //Moneda

        #region Parametro Conciliación Contabilidad

        private void AgregarNuevosCamposEnParametrosConciliacion() {
            if (!ColumnExists("Contab.ParametrosConciliacion", "ExpresarBalancesEnDiferentesMonedas")) {
                AddColumnBoolean("Contab.ParametrosConciliacion", "ExpresarBalancesEnDiferentesMonedas", "CONSTRAINT nnParExpresarBalances NOT NULL", false);
            }
            if (!ColumnExists("Contab.ParametrosConciliacion", "DiferenciaPorReexpresionEnOtraMoneda")) {
                AddColumnString("Contab.ParametrosConciliacion", "DiferenciaPorReexpresionEnOtraMoneda", 30, "CONSTRAINT nnParDiferenciaPorReexpresion NOT NULL", "");
            }
        }

        #endregion //Parametro Conciliación Contabilidad

        #region Formas De Cobro

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

        #endregion
    }
}
