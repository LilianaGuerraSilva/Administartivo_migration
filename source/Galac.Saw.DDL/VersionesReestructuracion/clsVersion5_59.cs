using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using Galac.Contab.Dal.Tablas;
using System.Threading;


namespace Galac.Saw.DDL.VersionesReestructuracion {
   class clsVersion5_59 :clsVersionARestructurar {

      public clsVersion5_59(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.59";
      }

      public override bool UpdateToVersion() {
         StartConnectionNoTransaction();
         ActualizarPermisologiaParaVehiculo();
         AjustesACambiosDeWincont14_5();
         CorrigeNUllsEnSimboloMoneda();
         InsertaElRecordDeFormaDeCobro();
         DisposeConnectionNoTransaction();
         return true;
      }

      private void AjustesACambiosDeWincont14_5() {
         if(CrearCampoPorcentajePorcentajeGastoAdmisible()) {
         }
         if(CrearCampoPorcentajeGananciaMaxima()) {
         }
         if(CrearCampoPorcentajePorcentajeGastoEnCriterioDeDistribucion()) {
         }
         if(CrearCampoDetallarCostoPorElementodelCosto()) {
         }
         if(!TableExists("Comun.ElementoDelCosto")) {
            new Galac.Comun.Dal.LeyCosto.clsElementoDelCostoED().InstalarTabla();
            if(InsertarElementoDelCostoPorDefectos()) {
            }
         }
         if(CrearCampoConsecutivoElementoDelCostoEnCuenta()) {
         }
         ActualizaEnumerativosIngresosPropiosActividad("D", "0", "9");
         ActualizaEnumerativosIngresosPropiosActividad("B", "0", "8");
         ActualizaEnumerativosIngresosPropiosActividad("@", "0", "7");
         ActualizaEnumerativosIngresosPropiosActividad("<", "0", "6");
         ActualizaEnumerativosIngresosPropiosActividad(":", "0", "5");
         ActualizaEnumerativosIngresosPropiosActividad("9", "0", "4");

         ActualizaEnumerativosCostoDeVentas("D", "1", "?");
         ActualizaEnumerativosCostoDeVentas("E", "1", "@");

         ActualizaEnumerativosGastos("N", "2", "7");
         ActualizaEnumerativosGastos("7", "2", "6");
         ActualizaEnumerativosGastos("6", "2", "5");
         ActualizaEnumerativosGastos("5", "2", "4");
         ActualizaEnumerativosGastos("4", "2", "3");
         ActualizaEnumerativosGastos("3", "2", "2");
         ActualizaEnumerativosGastos("2", "2", "1");
         CrearCampoCorreoElectronico();
         ExecuteDropConstraint("Comun.Producto", "u_Proniaion", true);
         if(StoredProcedureExists("dbo.Gp_CompaniaGetFk")) {
            Execute("DROP PROCEDURE dbo.Gp_CompaniaGetFk;");
         }
         #region Migracion de Tipo Comprobante.
         
         if(!TableExists("Contab.TipoDeComprobante")) {
            ActualizarDuplicadosEnTipoDeComprobante();
            new Galac.Contab.Dal.Tablas.clsTipoDeComprobanteED().InstalarTabla();
            new Galac.Contab.Dal.Tablas.clsTipoDeComprobanteMD().MigrarTipoDeComprobante();
            new Galac.Contab.Dal.Tablas.clsTipoDeComprobanteMD().EliminarTablaDboTipoDeComprobante();
            new Galac.Contab.Dal.Tablas.clsTipoDeComprobanteMD().CrearVistaDboTipoDeComprobante();
         }
         #endregion
         if(!TableExists("Contab.CentroDeCostos")) {
            new Galac.Contab.Dal.WinCont.clsCentroDeCostosED().InstalarTabla();
            new Galac.Contab.Dal.WinCont.clsCentroDeCostosMD().MigrarCentroDeCostos();
            new Galac.Contab.Dal.WinCont.clsCentroDeCostosMD().EliminarTablaDboCentroDeCostos();
            new Galac.Contab.Dal.WinCont.clsCentroDeCostosMD().CrearVistaDboCentroDeCostos();
         }
         if(!TableExists("Comun.MonedaLocal")) {
            new Galac.Comun.Dal.TablasGen.clsMonedaLocalED().InstalarTabla();
            new Galac.Comun.Dal.TablasGen.clsMonedaLocalMD().MigrarMonedaLocal();
            new Galac.Comun.Dal.TablasGen.clsMonedaLocalMD().EliminarTablaDboMonedaLocal();
            new Galac.Comun.Dal.TablasGen.clsMonedaLocalMD().CrearVistaDboMonedaLocal();
         }
         if(ModificaLongitudCamposCentroDeCostos()) {
         }
         //EliminaUKTablaMoneda(); Contabilidad pudo eliminarla porque no tenia otras relaciones, SAW tiene claves foraneas definidas, mejor dejarla hasta que migren estructuralmente Moneda.
         if(!TableExists("Contab.Auxiliar")) {
            MigrarAuxiliar();
         }
      }

      public bool CrearCampoPorcentajeGananciaMaxima() {
         bool vResult = false;
         try {
            vResult = AddColumnDecimal("Contab.ParametrosConciliacion", "PorcentajeGananciaMaxima", 25, 4, "", LibImportData.ToDec("30.0"));
         } catch(Exception) {
            throw;
         }
         return vResult;
      }

      public bool CrearCampoPorcentajePorcentajeGastoAdmisible() {
         bool vResult = false;
         try {
            vResult = AddColumnDecimal("Contab.ParametrosConciliacion", "PorcentajeGastoAdmisible", 25, 4, "", LibImportData.ToDec("12.5"));
         } catch(Exception) {
            throw;
         }
         return vResult;
      }
      public bool CrearCampoPorcentajePorcentajeGastoEnCriterioDeDistribucion() {
         bool vResult = false;
         try {
            vResult = AddColumnDecimal("Comun.CriterioDeDistribucion", "PorcentajeGasto", 25, 4, "", 0);
         } catch(Exception) {
            throw;
         }
         return vResult;
      }
      public bool CrearCampoDetallarCostoPorElementodelCosto() {
         bool vResult = false;
         try {
            vResult = AddColumnBoolean("Contab.ParametrosConciliacion", "DetallarCostoPorElementodelCosto", "CONSTRAINT nnParDetallarCostoPor NOT NULL", false);
         } catch(Exception) {
            throw;
         }
         return vResult;
      }
      public bool CrearCampoConsecutivoElementoDelCostoEnCuenta() {
         bool vResult = false;
         try {
            vResult = AddColumnInteger("dbo.CUENTA", "ConsecutivoElementoDelCosto", "CONSTRAINT nnConsecElem NOT NULL", 1);
            vResult = AddColumnInteger("dbo.tblCuentaTemp", "ConsecutivoElementoDelCosto", "CONSTRAINT nnConsecElem NOT NULL", 1) && vResult;
         } catch(Exception) {
            throw;
         }
         return vResult;
      }
      public bool InsertarElementoDelCostoPorDefectos() {
         bool vResult = false;
         vResult = InsertarLosElementoDelCostoPorDefectos("Sin Asignar", 1, 1, 0);
         vResult = InsertarLosElementoDelCostoPorDefectos("Adquisición", 2, 0, 1) && vResult;
         vResult = InsertarLosElementoDelCostoPorDefectos("Conversión", 3, 0, 1) && vResult;
         return vResult;
      }
      public bool InsertarLosElementoDelCostoPorDefectos(string valNombre, int valConsecutivo, int valTipo, int valOrdenParaInforme) {
         bool vResult = false;
         string vSql = "IF NOT EXISTS (SELECT * FROM Comun.ElementoDelCosto WHERE Nombre = " + InsSql.ToSqlValue(valNombre) + "AND " + "Consecutivo = " + valConsecutivo;
         vSql = vSql + " AND ConsecutivoCompania  IN (SELECT ConsecutivoCompania FROM Compania)" + ") ";
         vSql = vSql + "INSERT INTO Comun.ElementoDelCosto ";
         vSql = vSql + "(ConsecutivoCompania,Consecutivo,Nombre,Tipo,OrdenParaInforme,NombreOperador,FechaUltimaModificacion) ";
         vSql = vSql + "SELECT ";
         vSql = vSql + "ConsecutivoCompania,";
         vSql = vSql + InsSql.ToSqlValue(valConsecutivo) + ", ";
         vSql = vSql + InsSql.ToSqlValue(valNombre) + ", ";
         vSql = vSql + InsSql.ToSqlValue(valTipo) + ", ";
         vSql = vSql + InsSql.ToSqlValue(valOrdenParaInforme) + ", ";
         vSql = vSql + InsSql.ToSqlValue(((CustomIdentity)Thread.CurrentPrincipal.Identity).Login) + ", ";
         vSql = vSql + InsSql.ToSqlValue(LibDate.Today());
         vSql = vSql + " FROM Compania";
         Execute(vSql, 0);
         return vResult;
      }
      private void ActualizaEnumerativosIngresosPropiosActividad(string valNuevoValor, string valSeccionACambiar, string valCondicionaCambiar) {
         string vSql = "UPDATE CUENTA SET IngresosPropiosActividad = ";
         vSql = vSql + InsSql.ToSqlValue(valNuevoValor);
         vSql = vSql + " WHERE SeccionEstadoDemostrativo = ";
         vSql = vSql + InsSql.ToSqlValue(valSeccionACambiar);
         vSql = vSql + " AND IngresosPropiosActividad = ";
         vSql = vSql + InsSql.ToSqlValue(valCondicionaCambiar);
         Execute(vSql, 0);
      }
      private void ActualizaEnumerativosCostoDeVentas(string valNuevoValor, string valSeccionACambiar, string valCondicionaCambiar) {
         string vSql = "UPDATE CUENTA SET CostoDeVentas = ";
         vSql = vSql + InsSql.ToSqlValue(valNuevoValor);
         vSql = vSql + " WHERE SeccionEstadoDemostrativo = ";
         vSql = vSql + InsSql.ToSqlValue(valSeccionACambiar);
         vSql = vSql + " AND CostoDeVentas = ";
         vSql = vSql + InsSql.ToSqlValue(valCondicionaCambiar);
         Execute(vSql, 0);
      }
      private void ActualizaEnumerativosGastos(string valNuevoValor, string valSeccionACambiar, string valCondicionaCambiar) {
         string vSql = "UPDATE CUENTA SET Gastos = ";
         vSql = vSql + InsSql.ToSqlValue(valNuevoValor);
         vSql = vSql + " WHERE SeccionEstadoDemostrativo = ";
         vSql = vSql + InsSql.ToSqlValue(valSeccionACambiar);
         vSql = vSql + " AND Gastos = ";
         vSql = vSql + InsSql.ToSqlValue(valCondicionaCambiar);
         Execute(vSql, 0);
      }
      public bool CrearCampoCorreoElectronico() {
         bool vResult = false;
         try {
            //vResult = AddColumnString("dbo.Compania", "CorreoElectronico", 40, "", "");
         } catch(Exception) {
            throw;
         }
         return vResult;
      }
      public bool ModificaLongitudCamposCentroDeCostos() {
         bool vResult = true;
         vResult = ModifyLengthOfColumnString("dbo.Asiento", "CentrodeCostos", 20, "");
         vResult = ModifyLengthOfColumnString("dbo.AsientoSaldoInicial", "CentroDeCostos", 20, "") && vResult;
         vResult = ModifyLengthOfColumnString("dbo.ActivoFijo", "CentroDeCostos", 20, "") && vResult;
         vResult = ModifyLengthOfColumnString("dbo.tblDepMensualDetTemp", "CentroDeCostos", 20, "") && vResult;
         vResult = ModifyLengthOfColumnString("dbo.tblMovAuxiliarCentroDeCostoTemp", "CodeAux", 20, "") && vResult;
         return vResult;
      }

      private void EliminaUKTablaMoneda() {
         ExecuteDropConstraint("dbo.Moneda", "u_CodigoMoneda", true);
      }

      private void MigrarAuxiliar() {
         new Galac.Contab.Dal.WinCont.clsAuxiliarED().InstalarTabla();
         new Galac.Contab.Dal.WinCont.clsAuxiliarMD().MigrarAuxiliar();
         new LibDbo().Drop("dbo.Auxiliar", eDboType.Tabla);
         new Galac.Contab.Dal.WinCont.clsAuxiliarMD().CrearVistaDboAuxiliar();
      }

      private void ActualizarDuplicadosEnTipoDeComprobante() {
         string vSql = string.Empty;
         vSql = vSql + "UPDATE dbo.tipoDeComprobante SET NombreDelTipo = CodigoDelTipo + ' ' + substring(NombreDelTipo,1,27)";
         vSql = vSql + " WHERE NombreDelTipo In (SELECT NombreDelTipo FROM dbo.tipoDeComprobante GROUP BY NombreDelTipo HAVING Count(*) > 1)";
         Execute(vSql, 0);
      }

      private void ActualizarPermisologiaParaVehiculo() {
         string vSql = string.Empty;
         vSql = vSql + "UPDATE Lib.GUserSecurity SET ProjectModule = 'Vehículo' WHERE ProjectModule = 'Vehiculo'";
         Execute(vSql, 0);
         vSql = string.Empty;
         vSql = vSql + "UPDATE Lib.GUserSecurity SET FunctionalGroup = 'Vehículos' WHERE FunctionalGroup = 'Vehiculos'";
         Execute(vSql, 0);
      }

      private void CorrigeNUllsEnSimboloMoneda() {
         string vSQL = "ALTER TABLE " + LibDbo.ToFullDboName("dbo.Moneda") + " ALTER COLUMN Simbolo VARCHAR(20) NULL";
         Execute(vSQL, 0);
      }

      private void InsertaElRecordDeFormaDeCobro() {
         if(RecordCountOfSql("SELECT Codigo FROM  Saw.FormaDelCobro WHERE Codigo = '00005'") <= 0) {
            string vSQL = "INSERT INTO Saw.FormaDelCobro (Codigo, Nombre, TipoDePago) VALUES ('00005' , 'ANTICIPO','4')";
            Execute(vSQL, 0);
         }
      }

   }
}
