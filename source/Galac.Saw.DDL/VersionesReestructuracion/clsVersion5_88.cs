using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal.Settings;
using LibGalac.Aos.Base;
using System.Threading;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_88 : clsVersionARestructurar {
        public clsVersion5_88(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.88";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            //CrearTablaLineaDeProductoAOS();
            //MigrarDatosLineaDeProducto();
            //EliminarClavesForaneasHaciaLineaDeProducto();
            //CrearClaveForaneaArticuloInventario();
            //CrearVistasDeCompatibilidadDeLineaDeProducto();
            //ModificarConfiguracionParametroSugerirConsecutivoEnCobranza();
            DisposeConnectionNoTransaction();
            return true;
        }

        //private void CrearTablaLineaDeProductoAOS() {
        //    bool vResult;
        //    if (!TableExists("Adm.LineaDeProducto")) {
        //        vResult = new Galac.Saw.Dal.Tablas.clsLineaDeProductoED().InstalarTabla();
        //    }
        //}

        //private void MigrarDatosLineaDeProducto() {
        //    if (TableExists("Adm.LineaDeProducto") && TableExists("dbo.LineaDeProducto")) {
        //        DeleteAllDataForTable("Adm.LineaDeProducto");
        //        string vSql = "INSERT INTO Adm.LineaDeProducto (ConsecutivoCompania,Nombre,CentroDeCosto,PorcentajeComision,NombreOperador,FechaUltimaModificacion,Consecutivo) ";
        //        vSql = vSql + " SELECT ConsecutivoCompania,Nombre,CentroDeCosto,PorcentajeComision," + InsSql.ToSqlValue("JEFE") + "," + InsSql.ToSqlValue(LibDate.Today()) + ",ROW_NUMBER() OVER (PARTITION BY ConsecutivoCompania ORDER BY Nombre) AS Consecutivo FROM LineaDeProducto";
        //        Execute(vSql.ToString(), -1);
        //    }
        //}

        //private void EliminarClavesForaneasHaciaLineaDeProducto() {
        //    DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "LineaDeProducto", "ArticuloInventario");
        //}

        //private void CrearClaveForaneaArticuloInventario() {
        //    DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.LineaDeProducto", "ArticuloInventario");
        //    AddForeignKey("Adm.LineaDeProducto", "dbo.ArticuloInventario", new string[] { "ConsecutivoCompania", "Nombre" }, new string[] { "ConsecutivoCompania", "LineaDeProducto" }, false);
        //}

        //private void CrearVistasDeCompatibilidadDeLineaDeProducto() {
        //    if (RecordCountOfSql("SELECT * FROM Adm.LineaDeProducto") > 0) {
        //        clsCompatViews.CrearVistaDboLineaDeProducto();
        //    }
        //}
        //private void ModificarConfiguracionParametroSugerirConsecutivoEnCobranza() {
        //    string vSql = "UPDATE Comun.SettDefinition SET IsSetForAllEnterprise = " + _insSql.ToSqlValue(false) + " WHERE Name = " + _insSql.ToSqlValue("SugerirConsecutivoEnCobranza");
        //    Execute(vSql.ToString(), -1);
        //}
    }
}
