using Galac.Adm.Dal.Vendedor;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_67: clsVersionARestructurar {
        public clsVersion6_67(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            CrearRutaDeComercializacion();
            CrearTablaAdmVendedor();
            AmpliarCampoUbicacion();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void CrearTablaAdmVendedor() {
            if (!TableExists("Adm.Vendedor")) {
                new clsVendedorED().InstalarTabla();
                new DbMigrator.clsMigrarData(new string[] { "Vendedor" }).MigrarData();
                try {
                    DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.Cobranza");
                    CrearColumnaConsecutivoVendedor("dbo.Cobranza", "ConsecutivoCobrador", " CONSTRAINT nnCobCoCo NOT NULL");
                    LlenarColumnaConsecutivoVendedor("dbo.Cobranza", "ConsecutivoCobrador", "CodigoCobrador");
                    AddForeignKey("Adm.Vendedor", "dbo.Cobranza", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoCobrador" }, false, false);
                    DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.Contrato");
                    CrearColumnaConsecutivoVendedor("dbo.Contrato", "ConsecutivoVendedor", " CONSTRAINT nnConCoVe NOT NULL");
                    LlenarColumnaConsecutivoVendedor("dbo.Contrato", "ConsecutivoVendedor", "CodigoVendedor");
                    AddForeignKey("Adm.Vendedor", "dbo.Contrato", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoVendedor" }, false, false);
                    DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.Cotizacion");
                    CrearColumnaConsecutivoVendedor("dbo.Cotizacion", "ConsecutivoVendedor", " CONSTRAINT nnCotCoVe NOT NULL");
                    LlenarColumnaConsecutivoVendedor("dbo.Cotizacion", "ConsecutivoVendedor", "CodigoVendedor");
                    AddForeignKey("Adm.Vendedor", "dbo.Cotizacion", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoVendedor" }, false, false);
                    DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.CxC");
                    CrearColumnaConsecutivoVendedor("dbo.CxC", "ConsecutivoVendedor", " CONSTRAINT nnCxCCoVe NOT NULL");
                    LlenarColumnaConsecutivoVendedor("dbo.CxC", "ConsecutivoVendedor", "CodigoVendedor");
                    AddForeignKey("Adm.Vendedor", "dbo.CxC", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoVendedor" }, false, false);
                    DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.factura");
                    CrearColumnaConsecutivoVendedor("dbo.factura", "ConsecutivoVendedor", " CONSTRAINT nnFacCoVe NOT NULL");
                    LlenarColumnaConsecutivoVendedor("dbo.factura", "ConsecutivoVendedor", "CodigoVendedor");
                    AddForeignKey("Adm.Vendedor", "dbo.factura", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoVendedor" }, false, false);
                    DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.RenglonComisionesDeVendedor");
                    DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Vendedor", "dbo.RetirosACuenta");
                    CrearColumnaConsecutivoVendedor("dbo.RetirosACuenta", "ConsecutivoVendedor", " CONSTRAINT nnRetACuenCoVe NOT NULL");
                    LlenarColumnaConsecutivoVendedor("dbo.RetirosACuenta", "ConsecutivoVendedor", "CodigoVendedor");
                    AddForeignKey("Adm.Vendedor", "dbo.RetirosACuenta", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoVendedor" }, false, false);
                    CrearColumnaConsecutivoVendedor("dbo.Cliente", "ConsecutivoVendedor", " CONSTRAINT nnConCliVe NOT NULL");
                    LlenarColumnaConsecutivoVendedor("dbo.Cliente", "ConsecutivoVendedor", "CodigoVendedor");
                    if (TableExists("dbo.RenglonComisionesDeVendedor")) {
                        ExecuteDropTable("dbo.RenglonComisionesDeVendedor");
                    }
                    if (TableExists("dbo.Vendedor")) {
                        ExecuteDropTable("dbo.Vendedor");
                    }
                    clsCompatViews.CrearVistaDboVendedor();
                    clsCompatViews.CrearVistaDboRenglonComisionesDeVendedor();
                } catch (GalacException vEx) {
                    throw vEx;
                }
            }
        }

        private void CrearColumnaConsecutivoVendedor(string valTabla, string valNombreColumna, string valConstraint) {
            if (AddColumnInteger(valTabla, valNombreColumna, valConstraint)) {
                AddNotNullConstraint(valTabla, valNombreColumna, InsSql.NumericTypeForDb(10, 0));
            }
        }

        private void LlenarColumnaConsecutivoVendedor(string valTabla, string valColumnaNueva, string valColumnaAnterior) {
            StringBuilder vSQL = new StringBuilder();
            LibDataScope vDb = new LibDataScope();
            vSQL.AppendLine("UPDATE " + valTabla + " SET " + valTabla + "." + valColumnaNueva + " = Adm.Vendedor.Consecutivo ");
            vSQL.AppendLine("FROM " + valTabla);
            vSQL.AppendLine("INNER JOIN Adm.Vendedor ON " + valTabla + ".ConsecutivoCompania = Adm.Vendedor.ConsecutivoCompania ");
            vSQL.AppendLine("AND " + valTabla + "." + valColumnaAnterior + " = Adm.Vendedor.Codigo");
            vDb.ExecuteWithScope(vSQL.ToString());
        }


        private void CrearRutaDeComercializacion() {
            if (new Saw.Dal.Tablas.clsRutaDeComercializacionED().InstalarTabla()) {
                LLenarRutaDeComercializacionPorCompania();
            }
        }

        private void LLenarRutaDeComercializacionPorCompania() {
            try {
                StringBuilder vSql = new StringBuilder();
                string vFechaDeHoy = InsSql.ToSqlValue(LibDate.Today());
                vSql.AppendLine("INSERT INTO Saw.RutaDeComercializacion (ConsecutivoCompania,Consecutivo,Descripcion,NombreOperador,FechaUltimaModificacion)");
                vSql.AppendLine("SELECT ConsecutivoCompania,1,'NO ASIGNADA','JEFE'," + vFechaDeHoy);
                vSql.AppendLine("FROM Compania");
                LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), null, "", 0);
            } catch (Exception) {
                throw;
            }
        }

        private void AmpliarCampoUbicacion() {
            string vSQL = "ALTER TABLE " + LibDbo.ToFullDboName("dbo.ExistenciaPorAlmacen") + " ALTER COLUMN Ubicacion VARCHAR(100) NULL";
            Execute(vSQL, 0);
        }
    }
}
