using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_53: clsVersionARestructurar {

        public clsVersion5_53(string valCurrentDataBaseName):base(valCurrentDataBaseName){
            _VersionDataBase="5.53";
        }

        public override bool UpdateToVersion() {
            bool vResult = EsValidaDataProveedor();
            if (vResult) {
                StartConnectionNoTransaction();
                new LibGalac.Aos.Dal.DDL.LibCreateDb().CreateSchemas(new string[] { "Adm" });
                CrearCampoUsaLeyCosto();
                CrearTablasEnNuevoEsquema();
                FillNullValuesProveedor();
                DeleteTableParametrosCompaniaAndAddNotNullConstraintToCompra();
                AgregarNuevoParametro("MaximoGastosAdmisibles", "Inventario", 5, "5.3.- Método de costos", 3, "", '3', "", 'N', "0");
                CreateFieldsArticuloInventario();
                UpgradeDBVersion();
                DisposeConnectionNoTransaction();
                return true;
            } else {
                throw new LibGalac.Aos.Catching.GalacException("Existen proveedores con números de RIF repetidos. Para más detalle revise el archivo 'ListadoProveedoresRepetidos.txt'", LibGalac.Aos.Catching.eExceptionManagementType.Controlled);
            }
        }

        private bool CreateFieldsArticuloInventario() {
            bool vResult = true;
            if (!ColumnExists("articuloInventario", "GastosAdmisibles")) {
                vResult = vResult && AddColumnCurrency("articuloInventario", "GastosAdmisibles", "");
            }
            if (!ColumnExists("articuloInventario", "MargenGananciaXLey")) {
                vResult = vResult && AddColumnCurrency("articuloInventario", "MargenGananciaXLey", "");
            }
            if (!ColumnExists("articuloInventario", "MargenGananciaXLey2")) {
                vResult = vResult && AddColumnCurrency("articuloInventario", "MargenGananciaXLey2", "");
            }
            if (!ColumnExists("articuloInventario", "MargenGananciaXLey3")) {
                vResult = vResult && AddColumnCurrency("articuloInventario", "MargenGananciaXLey3", "");
            }
            if (!ColumnExists("articuloInventario", "MargenGananciaXLey4")) {
                vResult = vResult && AddColumnCurrency("articuloInventario", "MargenGananciaXLey4", "");
            }
            return vResult;
        }

        private bool DeleteTableParametrosCompaniaAndAddNotNullConstraintToCompra() {
            if (TableExists("ParametrosCompania")) {
                ExecuteDropTable("ParametrosCompania");
            }
            if (TableExists("ParametrosAdministrativo")) {
                ExecuteDropTable("ParametrosAdministrativo");
            }
            if (TableExists("ParametrosMsdeManager")) {
                ExecuteDropTable("ParametrosMsdeManager");
            }
            return true;
        }

        private bool FillNullValuesProveedor() {

            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Adm.Proveedor ");
            vSql.AppendLine("SET Adm.Proveedor.TipoDeProveedorDeLibrosFiscales =  '0' ");
            vSql.AppendLine(" WHERE Adm.Proveedor.TipoDeProveedorDeLibrosFiscales IS NULL OR Adm.Proveedor.TipoDeProveedorDeLibrosFiscales = ''");
            Execute(vSql.ToString(), -1);

            vSql.AppendLine("UPDATE Adm.Proveedor ");
            vSql.AppendLine("SET Adm.Proveedor.PorcentajeRetencionIVA =  0 ");
            vSql.AppendLine(" WHERE Adm.Proveedor.PorcentajeRetencionIVA IS NULL");
            Execute(vSql.ToString(), -1);

            vSql.AppendLine("UPDATE Adm.Proveedor ");
            vSql.AppendLine("SET Adm.Proveedor.CuentaContableCxP =  '' ");
            vSql.AppendLine(" WHERE Adm.Proveedor.CuentaContableCxP IS NULL");
            Execute(vSql.ToString(), -1);

            vSql.AppendLine("UPDATE Adm.Proveedor ");
            vSql.AppendLine("SET Adm.Proveedor.CuentaContableGastos =  '' ");
            vSql.AppendLine(" WHERE Adm.Proveedor.CuentaContableGastos IS NULL");
            Execute(vSql.ToString(), -1);

            vSql.AppendLine("UPDATE Adm.Proveedor ");
            vSql.AppendLine("SET Adm.Proveedor.CuentaContableAnticipo =  '' ");
            vSql.AppendLine(" WHERE Adm.Proveedor.CuentaContableAnticipo IS NULL");
            Execute(vSql.ToString(), -1);

            vSql.AppendLine("UPDATE Adm.Proveedor ");
            vSql.AppendLine("SET Adm.Proveedor.CodigoLote =  '' ");
            vSql.AppendLine(" WHERE Adm.Proveedor.CodigoLote IS NULL");
            Execute(vSql.ToString(), -1);

            vSql.AppendLine("UPDATE Adm.Proveedor ");
            vSql.AppendLine("SET Adm.Proveedor.TipoDocumentoIdentificacion =  '4' ");
            vSql.AppendLine(" WHERE Adm.Proveedor.TipoDocumentoIdentificacion IS NULL");
            Execute(vSql.ToString(), -1);

            vSql.AppendLine("UPDATE Adm.Proveedor ");
            vSql.AppendLine("SET Adm.Proveedor.Beneficiario =  '' ");
            vSql.AppendLine(" WHERE Adm.Proveedor.Beneficiario IS NULL");
            Execute(vSql.ToString(), -1);

            vSql.AppendLine("UPDATE Adm.Proveedor ");
            vSql.AppendLine("SET Adm.Proveedor.NumeroCuentaBancaria =  '' ");
            vSql.AppendLine(" WHERE Adm.Proveedor.NumeroCuentaBancaria IS NULL");
            Execute(vSql.ToString(), -1);

            vSql.AppendLine("UPDATE Adm.Proveedor ");
            vSql.AppendLine("SET Adm.Proveedor.CodigoContribuyente =  '' ");
            vSql.AppendLine(" WHERE Adm.Proveedor.CodigoContribuyente IS NULL");
            Execute(vSql.ToString(), -1);

            return true;
        }

        private bool CrearTablasEnNuevoEsquema() {
            bool vResult = true;
            clsCrearDatabase vClsCrearDataBase = new clsCrearDatabase();
            if (!TableExists("Comun.Producto")) {
                vResult = vClsCrearDataBase.CrearProducto();
            }
            if (!TableExists("Comun.CriterioDeDistribucion")) {
                vResult = vResult && vClsCrearDataBase.CrearCriterioDeDistribucion();
            }
            if (!TableExists("Adm.Proveedor")) {
                vResult = vResult && DeleteAllRelationShipProveedor();
                vResult = vResult && MigrarProveedor();
                vResult = vResult && CreateAllRelationShipProveedor();
            }
            if (!TableExists("Comun.TablaRetencion")) {
                vResult = vResult && CreateViewAndSP("TablaRetencion");
            }
            return vResult;
        }


        private bool CreateAllRelationShipProveedor() {
            bool vResult = true;
            vResult = vResult && AddForeignKey("Adm.Proveedor", "Pago", new string[] { "ConsecutivoCompania", "CodigoProveedor" }, new string[] { "ConsecutivoCompania", "CodigoProveedor" }, false);
            vResult = vResult && AddForeignKey("Adm.Proveedor", "RetPago", new string[] { "ConsecutivoCompania", "CodigoProveedor" }, new string[] { "ConsecutivoCompania", "CodigoProveedor" }, false);
            vResult = vResult && AddForeignKey("Adm.Proveedor", "cxP", new string[] { "ConsecutivoCompania", "CodigoProveedor" }, new string[] { "ConsecutivoCompania", "CodigoProveedor" }, false);
            vResult = vResult && AddForeignKey("Adm.Proveedor", "Compra", new string[] { "ConsecutivoCompania", "CodigoProveedor" }, new string[] { "ConsecutivoCompania", "CodigoProveedor" }, false);
            vResult = vResult && AddForeignKey("Adm.Proveedor", "ARCV", new string[] { "ConsecutivoCompania", "CodigoProveedor" }, new string[] { "ConsecutivoCompania", "CodigoProveedor" }, false);
            return vResult;
        }

        private bool DeleteAllRelationShipProveedor() {
            DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Proveedor", "Pago");
            DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Proveedor", "RetPago");
            DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Proveedor", "cxP");
            DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Proveedor", "Compra");
            DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Proveedor", "ARCV");
            DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.Proveedor", "Pago");
            DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.Proveedor", "RetPago");
            DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.Proveedor", "cxP");
            DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.Proveedor", "Compra");
            DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.Proveedor", "ARCV");
            return true;
        }

        private bool CrearCampoUsaLeyCosto() {
            if (!ColumnExists("Contab.ParametrosConciliacion", "UsaLeyCosto")) {
                AddColumnBoolean("Contab.ParametrosConciliacion", "UsaLeyCosto", "CONSTRAINT nnParGenUsaLeyCost NOT NULL", false);
            }
            return true;
        }

        private bool EsValidaDataProveedor() {
            bool vResult = false;
            StringBuilder vSqlSelect = new StringBuilder();
            vSqlSelect.AppendLine("SELECT ConsecutivoCompania, NumeroRif, COUNT(NumeroRif) FROM Adm.Proveedor GROUP BY ConsecutivoCompania, NumeroRif HAVING COUNT(NumeroRif) > 1");
            System.Data.DataSet vDSChkProv = ExecuteDataset(vSqlSelect.ToString(), -1);
            if (vDSChkProv.Tables[0].Rows.Count > 0) {
                vSqlSelect.Clear();
                vSqlSelect.AppendLine("SELECT ' Compañía               .', 'Código Proveedor', 'Nombre Proveedor             .', 'Número RIF'");
                vSqlSelect.AppendLine(" UNION");
                vSqlSelect.AppendLine(" SELECT C.Codigo + ' / ' + C.Nombre as Compania,P2.CodigoProveedor,P2.NombreProveedor, (CASE WHEN P2.NumeroRIF = '' OR P2.NumeroRIF IS NULL THEN '(Vacío)' ELSE P2.NumeroRIF END) AS NumeroRIF");
                vSqlSelect.AppendLine(" FROM");
                vSqlSelect.AppendLine(" (SELECT ConsecutivoCompania, NumeroRif FROM Adm.Proveedor GROUP BY ConsecutivoCompania, NumeroRif HAVING COUNT(NumeroRif) > 1) P1");
                vSqlSelect.AppendLine(" INNER JOIN Compania C ON (P1.ConsecutivoCompania = C.ConsecutivoCompania)");
                vSqlSelect.AppendLine(" INNER JOIN Adm.Proveedor P2 ON (P1.ConsecutivoCompania = P2.ConsecutivoCompania AND P1.NumeroRIF = P2.NumeroRIF)");
                System.Data.DataSet vDSChkProvFull = ExecuteDataset(vSqlSelect.ToString(), -1);
                StringBuilder vTexto = new StringBuilder();
                vTexto.AppendLine("A continuación se listan los proveedores por empresa que tienen números de RIF repetidos:");
                vTexto.AppendLine("");
                foreach (System.Data.DataRow row in vDSChkProvFull.Tables[0].Rows) {
                    foreach (object item in row.ItemArray) {
                        vTexto.Append((string)item + "\t");
                    }
                    vTexto.AppendLine();
                }
                File.WriteAllText(Path.Combine(LibGalac.Aos.Base.LibWorkPaths.ProgramDir, "ListadoProveedoresRepetidos.txt"), vTexto.ToString());
                vResult = false;
            } else {
                vResult = true;
            }
            return vResult;
        }

        #region Migrar Proveedor
        private bool MigrarProveedor() {
            bool vResult = false;
            try {
                if (!TableExists("Adm.Proveedor")) {
                    string vSql = "INSERT INTO Adm.Proveedor (ConsecutivoCompania,CodigoProveedor,NombreProveedor,Contacto,NumeroRIF,NumeroNit,TipoDePersona,CodigoRetencionUsual,Telefonos,Direccion,Fax,Email,TipodeProveedor,TipoDeProveedorDeLibrosFiscales,PorcentajeRetencionIva,CuentaContableCxp,CuentaContableGastos,CuentaContableAnticipo,CodigoLote,Beneficiario,UsarBeneficiarioImpCheq,TipoDocumentoIdentificacion,EsAgenteDeRetencionIva,ApellidoPaterno,ApellidoMaterno,Nombre,NumeroCuentaBancaria,CodigoContribuyente,NombreOperador,FechaUltimaModificacion,Consecutivo) ";
                    vSql = vSql + " SELECT ConsecutivoCompania,CodigoProveedor,NombreProveedor,Contacto,NumeroRIF,NumeroNit,TipoDePersona,CodigoRetencionUsual,Telefonos,Direccion,Fax,Email,TipodeProveedor,TipoDeProveedorDeLibrosFiscales,PorcentajeRetencionIva,CuentaContableCxp,CuentaContableGastos,CuentaContableAnticipo,CodigoLote,Beneficiario,UsarBeneficiarioImpCheq,TipoDocumentoIdentificacion,EsAgenteDeRetencionIva,ApellidoPaterno,ApellidoMaterno,Nombre,NumeroCuentaBancaria,CodigoContribuyente,NombreOperador,FechaUltimaModificacion,ROW_NUMBER() OVER (PARTITION BY ConsecutivoCompania ORDER BY FechaUltimaModificacion desc) AS Consecutivo FROM Proveedor";
                    Execute(vSql.ToString(), -1);
                    Galac.Saw.DDL.clsCompatViews.CrearVistaDboProveedor();
                    vResult = true;
                    return vResult;
                }
            } catch (Exception vEx) {
                throw vEx;
            }
            return vResult;
        }
        #endregion
    }
}
