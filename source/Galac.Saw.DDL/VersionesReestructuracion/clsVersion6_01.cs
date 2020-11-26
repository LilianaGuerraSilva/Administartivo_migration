using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal.Settings;
using LibGalac.Aos.Base;
using System.Threading;
using System.Data;
using LibGalac.Aos.Dal.Usal;
using LibGalac.Aos.Dal;
using LibGalac.Aos.DefGen;
using System.Transactions;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_01:clsVersionARestructurar {
        public clsVersion6_01(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.01";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregaColumnaEnCxP();
            ActualizarNuevosCamposEnCxP();
            AgregarParametroEnOrdenDeCompra();
            AgregaColumnaEnProveedor();
            CrearComunArancel();
            CrearCamposEnArticuloInventario();
            InstalarVistasySpArticuloInventario();
            CrearCargaInicial();
            CrearCondicionDePago();
            CrearAdmCompra();
            MigrarDataDeComprasYArancelEnInventario();
            CrearPermisosParaOrdenDeCompra();
            DeleteTabladboCompra();
            AgregarTablaImpTranscBancariaAOS();
            CrearCiudad();
            AgregaDetalleCasilla66EnPlanillaForma00030();
            ActualizaDetalleCasilla66EnPlanillaForma00030();
            AgregaParametroUsaListaDePrecioEnMonedaExtranjera();
            ActualizarParametros();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregaColumnaEnProveedor() {
            AlterColumnIfExist("Adm.Proveedor","NombreProveedor",InsSql.VarCharTypeForDb(160),"CONSTRAINT nnProNombreProv NOT NULL","");
            if(!ColumnExists("Saw","UnidadDeVenta")) {
                AddColumnString("Saw.UnidadDeVenta","Codigo",10,"","");
            }
            if(!ColumnExists("Adm.Proveedor","TipoDePersonaLibrosElectronicos")) {
                AddColumnEnumerative("Adm.Proveedor","TipoDePersonaLibrosElectronicos","",0);
            }
            if(!ColumnExists("Adm.Proveedor","CodigoPaisResidencia")) {
                AddColumnString("Adm.Proveedor","CodigoPaisResidencia",4,"","");
            }
            if(!ColumnExists("Adm.Proveedor","CodigoConveniosSunat")) {
                AddColumnString("Adm.Proveedor","CodigoConveniosSunat",2,"","");
            }            
        } 

        private void AgregaColumnaEnCxP() {
            if(!ColumnExists("CxP","NumeroSerie")) {
                AddColumnString("CxP","NumeroSerie",10,"","");
            }

            if(!ColumnExists("CxP","NumeroDeDocumento")) {
                AddColumnString("CxP","NumeroDeDocumento",20,"","");
            }

            if(!ColumnExists("CxP","NumeroSerieDocAfectado")) {
                AddColumnString("CxP","NumeroSerieDocAfectado",10,"","");
            }

            if(!ColumnExists("CxP","NumeroDeDocumentoAfectado")) {
                AddColumnString("CxP","NumeroDeDocumentoAfectado",20,"","");
            }

            if(!ColumnExists("CxP","CodigoTipoDeComprobante")) {
                AddColumnString("CxP","CodigoTipoDeComprobante",2,"","");
            }
        }       

        private void InstalarVistasySpArticuloInventario() {
            new Saw.Dal.Inventario.clsArticuloInventarioED().InstalarVistasYSps();
        }

        private void CrearCargaInicial() {
            new Adm.Dal.GestionCompras.clsCargaInicialED().InstalarTabla();
        }

        private void CrearCondicionDePago() {
            new Galac.Saw.Dal.Tablas.clsCondicionesDePagoED().InstalarTabla();
        }

        private void CrearCamposEnArticuloInventario() {
            if(!ColumnExists("ArticuloInventario","Peso")) {
                AddColumnCurrency("ArticuloInventario","Peso","",0);
            }
            if(!ColumnExists("ArticuloInventario","ArancelesCodigo")) {
                AddColumnString("ArticuloInventario","ArancelesCodigo",13,"","0000.00.00.00");
            }
            if(!ColumnExists("dbo.ArticuloInventario","TipoDeMercanciaProduccion")) {
                AddColumnEnumerative("ArticuloInventario","TipoDeMercanciaProduccion","",0);
            }
        }        

        private void AgregarParametroEnOrdenDeCompra() {
            AgregarNuevoParametro("SugerirNumeroDeOrdenDeCompra","CxP/Compras", 6,"6.1.- Compras",1,"",eTipoDeDatoParametros.String,"",'S',"");          
        }

        private void CrearComunArancel() {
            if(!TableExists("Comun.Aranceles")) {
                new Galac.Comun.Dal.Impuesto.clsArancelesED().InstalarTabla();
            }
        }

        private void CrearAdmCompra() {
            if(!TableExists("Adm.OrdenDeCompra")) {
                new Galac.Adm.Dal.GestionCompras.clsOrdenDeCompraED().InstalarTabla();
            }
            if(!TableExists("Adm.Compra")) {
                new Galac.Adm.Dal.GestionCompras.clsCompraED().InstalarTabla();
            }
        }

        private void MigrarDataDeComprasYArancelEnInventario() {
            if(TableExists("dbo.RenglonCompra") && TableExists("dbo.Compra")) {
                using(TransactionScope vScope = LibGalac.Aos.Brl.LibBusiness.CreateScope()) {
                    if(LibDefGen.ProgramInfo.IsCountryPeru()) {                        
                        InsertarvalorPorDefectoAranceles();
                    }
                    ActualizaArancelesEnArticuloInventario();
                    InsertarValoresPorDefectoCondicionDepago();
                    new DbMigrator.clsMigrarData(new string[] { "Compra","CargaInicial" }).MigrarData();
                    vScope.Complete();
                }
            }
        }

        private void ActualizarNuevosCamposEnCxP() {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("UPDATE CxP SET ");
            vSQL.AppendLine("NumeroSerie= LTRIM(RIGHT(Numero,10)), ");
            vSQL.AppendLine("NumeroDeDocumento = LTRIM(RIGHT(Numero,20)), ");
            vSQL.AppendLine("NumeroSerieDocAfectado = '',");
            vSQL.AppendLine("NumeroDeDocumentoAfectado = '',");
            vSQL.AppendLine("CodigoTipoDeComprobante = TipoDeCxp");
            LibDataScope vDb = new LibDataScope();
            vDb.ExecuteWithScope(vSQL.ToString());
        }

        private void InsertarValoresPorDefectoCondicionDepago() {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine(" IF NOT EXISTS(SELECT TOP 1 * FROM Saw.CondicionesDePago WHERE Descripcion = 'A Convenir')");
            vSQL.AppendLine("BEGIN");
            vSQL.AppendLine("   INSERT Saw.CondicionesDePago(ConsecutivoCompania, Consecutivo, Descripcion) ");
            vSQL.AppendLine("   SELECT ConsecutivoCompania,0  AS Consecutivo, 'A Convenir' AS Descripcion FROM COMPANIA ");
            vSQL.AppendLine("END");
            LibDataScope vDb = new LibDataScope();
            vDb.ExecuteWithScope(vSQL.ToString());
        }

        private void InsertarvalorPorDefectoAranceles() {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine(" IF NOT EXISTS(SELECT * FROM Comun.Aranceles WHERE Codigo = '0000.00.00.00')");
            vSQL.AppendLine("BEGIN");
            vSQL.AppendLine("   INSERT Comun.Aranceles ");
            vSQL.AppendLine("   (Codigo, Descripcion, AdValorem, ImpuestoSelectivoalConsumo, ImpuestoGeneralalasVentas, ImpuestodePromocionMunicipal, DerechoEspecificos, DerechoAntidumping, Seguro, Sobretasa, UnidaddeMedida) ");
            vSQL.AppendLine("   VALUES(N'0000.00.00.00', N'No Aplica', CAST(0.00000 AS Decimal(30, 5)), CAST(0.00000 AS Decimal(30, 5)), CAST(0.00000 AS Decimal(30, 5)), CAST(0.00000 AS Decimal(30, 5)), CAST(0.00000 AS Decimal(30, 5)), CAST(0.00000 AS Decimal(30, 5)), CAST(0.00000 AS Decimal(30, 5)), CAST(0.00000 AS Decimal(30, 5)), N'')");
            vSQL.AppendLine("END");
            LibDataScope vDb = new LibDataScope();
            vDb.ExecuteWithScope(vSQL.ToString());
        }

        private void ActualizaArancelesEnArticuloInventario() {
            string vCodigoArancel = "";
            if(LibDefGen.ProgramInfo.IsCountryPeru()) {
                vCodigoArancel = "'0000.00.00.00'";
            } else {
                vCodigoArancel = "'000'";
            }
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine(" UPDATE ArticuloInventario ");
            vSQL.AppendLine(" SET ArancelesCodigo = " + vCodigoArancel);
            LibDataScope vDb = new LibDataScope();
            vDb.ExecuteWithScope(vSQL.ToString());
        }

        private void DeleteTabladboCompra() {
            if(TableExists("dbo.RenglonCompra")) {
                ExecuteDropTable("dbo.RenglonCompra");
                ExecuteDropTable("dbo.RenglonCompraXSerial");
                ExecuteDropTable("dbo.Compra");
            }
            Galac.Saw.DDL.clsCompatViews.CrearVistaDboCompra();
        }

        private void CrearPermisosParaOrdenDeCompra() {
            LibGUserReestScripts SqlSecurityLevel = new LibGUserReestScripts();
            StringBuilder vSql = new StringBuilder();
            List<string> vActions = new List<string>();
            System.Collections.Hashtable vFiltros = new System.Collections.Hashtable();
            vActions.Add("Consultar");
            vActions.Add("Insertar");
            vActions.Add("Modificar");
            vActions.Add("Eliminar");
            vActions.Add("Anular");
            vSql.Append(SqlSecurityLevel.SqlAddSecurityLevel("Orden de Compra",vActions,"Inventario",4,"SAW",null));
            foreach(var item in vActions) {
                vSql.Append(SqlUpdateSecurityLevel("Orden de Compra",item,false,"Compra"));
            }
            Execute(vSql.ToString(),-1);
        }

        public string SqlUpdateSecurityLevel(string valProjectModule,string valProjectAction,bool valHasHacces,string valOtherProjectModule) {
            StringBuilder vSQLUpdate = new StringBuilder();
            QAdvSql vQAdvSql = new QAdvSql("");
            if(!LibString.IsNullOrEmpty(valProjectModule) && !LibString.S1IsEqualToS2(valProjectModule,"Usuario") && !LibString.IsNullOrEmpty(valProjectAction)) {
                vSQLUpdate.AppendLine("UPDATE Lib.GUserSecurity ");
                vSQLUpdate.AppendLine("SET HasAccess = " + vQAdvSql.ToSqlValue(valHasHacces));
                vSQLUpdate.AppendLine("      WHERE UserName <> " + vQAdvSql.ToSqlValue("JEFE"));
                vSQLUpdate.AppendLine("      AND ProjectModule = " + vQAdvSql.ToSqlValue(valProjectModule));
                vSQLUpdate.AppendLine("      AND ProjectAction = " + vQAdvSql.ToSqlValue(valProjectAction));
                if(!LibString.IsNullOrEmpty(valOtherProjectModule)) {
                    vSQLUpdate.Append("      AND Lib.GUserSecurity.UserName IN (SELECT Lib.GUserSecurity.UserName FROM Lib.GUserSecurity WHERE ");
                    vSQLUpdate.AppendLine(" (ProjectModule = " + vQAdvSql.ToSqlValue(valOtherProjectModule) + " AND ProjectAction = " + vQAdvSql.ToSqlValue(LibConvert.ToStr(valProjectAction)) + " AND HasAccess = " + vQAdvSql.ToSqlValue(LibConvert.ToBool(valHasHacces)) + "))");
                }
            }
            return vSQLUpdate.ToString();
        }

        private void AgregarTablaImpTranscBancariaAOS() {
            new Galac.Saw.Dal.Tablas.clsImpuestoBancarioED().InstalarTabla();
        }

        public bool CrearCiudad() {
            bool vResult = new Galac.Comun.Dal.TablasGen.clsCiudadED().InstalarTabla();
            clsCompatViews.CrearVistaDboCiudad();
            return vResult;
        }

        public void CrearVistasYSP() {
            new clsReestructurarDatabase().CrearVistasYSps();
        }

        private void AgregaDetalleCasilla66EnPlanillaForma00030() {
            if(!ColumnExists("dbo.planillaForma00030","DetalleCasilla66Retencion") &&
                !ColumnExists("dbo.planillaForma00030","DetalleCasilla66Anticipo")) {
                AddColumnCurrency("dbo.planillaForma00030","DetalleCasilla66Retencion","",0);
                AddColumnCurrency("dbo.planillaForma00030","DetalleCasilla66Anticipo","",0);
            }
        }

        private void ActualizaDetalleCasilla66EnPlanillaForma00030() {
            if(ColumnExists("dbo.planillaForma00030","DetalleCasilla66Retencion")) {
                QAdvSql insQAdvSsl = new QAdvSql("");
                StringBuilder vSQL = new StringBuilder();
                vSQL.AppendLine("UPDATE dbo.planillaForma00030 SET ");
                vSQL.AppendLine("DetalleCasilla66Retencion  = RetencionesdelPeriodo");
                Execute(vSQL.ToString(),0);                     
            }
        }


        private void ActualizarParametros() {
            QAdvSql insQAdvSsl = new QAdvSql("");
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("UPDATE comun.SettValueByCompany ");
            vSQL.AppendLine("SET VALUE = 'Bolívar' ");
            vSQL.AppendLine("WHERE VALUE = 'Bolívar Soberano' ");
            vSQL.AppendLine(" AND NameSettDefinition = 'NombreMonedaLocal' ");
            Execute(vSQL.ToString(),0);
            vSQL.Clear();
            vSQL.AppendLine(" UPDATE comun.SettValueByCompany ");
            vSQL.AppendLine(" SET value = 'rpxOrdenDeCompraFormatoLibre' ");
            vSQL.AppendLine(" WHERE value = 'rpxOrdenDeCompra' ");
            vSQL.AppendLine(" AND NameSettDefinition = 'NombrePlantillaOrdenDeCompra' ");
            Execute(vSQL.ToString(),0);
            vSQL.Clear();
            vSQL.AppendLine(" UPDATE Comun.SettValueByCompany ");
            vSQL.AppendLine(" SET Value = 'S' ");
            vSQL.AppendLine(" WHERE NameSettDefinition = 'SugerirNumeroDeOrdenDeCompra'");
            Execute(vSQL.ToString(),0);
        }

        private void AgregaParametroUsaListaDePrecioEnMonedaExtranjera() {
            AgregarNuevoParametro("UsaListaDePrecioEnMonedaExtranjera","Factura",2," 2.1.- Facturación ",4,"",eTipoDeDatoParametros.String,"",'N',"N");
        }
    }
}
