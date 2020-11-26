using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Brl;
using System.Data;
using LibGalac.Aos.Dal.Usal;
using System.Collections.Generic;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_10 : clsVersionARestructurar {

        public clsVersion6_10(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.10";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AjustarCamposDeTasaDeCambioDeCobranzaYDocumentoCobradoSiSonIgualACero();
            AgregaCampoEnDocumentoCobrado();
            AgregarTablaListaDeMateriales();
            AgregarTablaOrdenDeProduccion();
            AgregaParametrosParaVerificadorDePrecios();
            AgregarCamposEnNotaEntradaSalida();
            NuevoCamposFacturaLimitedFechaCambio();
            AgregarCmbioABolivaresYCambioMostrarTotalEnDivisasEnCotizacion();
            AgregaCampoEnAnticipoCobrado();
            AgregaParametrosParaNroDiasAMantenerTasaDeCambio();
            ActualizarPermisosEnCobranzaModificarTasaDeCambio();
            AgregarCamposEnCotizacion();
            ActualizarPermisosEnCobranzaBuscarTasaDeCambioOriginalDeCxC();
            DisposeConnectionNoTransaction();
            return true;
        }

        public void AgregaParametrosParaVerificadorDePrecios() {
            AgregarNuevoParametro("UsaMostrarPreciosEnDivisa", "Inventario", 5, "5.4-. Verificador de Precios", 4, "", eTipoDeDatoParametros.String, "", 'N', "N");
            AgregarNuevoParametro("TipoDeConversionParaPrecios", "Inventario", 5, "5.4-. Verificador de Precios", 4, "", eTipoDeDatoParametros.String, "", 'N', "0");
        }

        private void AgregaCampoEnDocumentoCobrado() {
            if(!ColumnExists("dbo.DocumentoCobrado", "MontoAbonadoEnMonedaOriginal")) {
                AddColumnCurrency("dbo.DocumentoCobrado", "MontoAbonadoEnMonedaOriginal", "", 0);
            }
            if(!ColumnExists("dbo.DocumentoCobrado", "CambioAMonedaLocal")) {
                AddColumnCurrency("dbo.DocumentoCobrado", "CambioAMonedaLocal", "", 1);
            }
            if(!ColumnExists("dbo.DocumentoCobrado", "SimboloMonedaDelAbono")) {
                AddColumnString("dbo.DocumentoCobrado", "SimboloMonedaDelAbono", 4, "", "");
            }
            BuscarConsecutivosCompanias();
        }

        private void BuscarConsecutivosCompanias() {
            StringBuilder vSQL = new StringBuilder();
            Galac.Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocal = new Galac.Comun.Brl.TablasGen.clsMonedaLocalActual();
            vMonedaLocal.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            if(ColumnExists("dbo.DocumentoCobrado", "MontoAbonadoEnMonedaOriginal") &&
                ColumnExists("dbo.DocumentoCobrado", "CambioAMonedaLocal") &&
                ColumnExists("dbo.DocumentoCobrado", "SimboloMonedaDelAbono")) {
                vSQL.AppendLine("SELECT ConsecutivoCompania FROM dbo.Compania");
                DataSet ds = ExecuteDataset(vSQL.ToString(), -1);
                DataTableReader rd = ds.Tables[0].CreateDataReader();
                while(rd.Read()) {
                    EstablecerValoresEnCamposDeDocumentoCobrado((int)rd[0], vMonedaLocal.CodigoMoneda(LibDate.Today()));
                }
            }
        }

        private void EstablecerValoresEnCamposDeDocumentoCobrado(int valConsecutivoCompania, string valParametroMonedaLocal) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("WHILE EXISTS (SELECT TOP (1) dbo.DocumentoCobrado.MontoAbonadoEnMonedaOriginal FROM dbo.DocumentoCobrado WHERE dbo.DocumentoCobrado.MontoAbonadoEnMonedaOriginal = 0 AND dbo.DocumentoCobrado.CambioAMonedaLocal = 1 AND  dbo.DocumentoCobrado.SimboloMonedaDelAbono = '' AND DocumentoCobrado.ConsecutivoCompania =  " + valConsecutivoCompania.ToString() + ")");
            vSql.AppendLine("BEGIN");
            vSql.AppendLine("   UPDATE TOP (1000) dbo.DocumentoCobrado");
            vSql.AppendLine("   SET		MontoAbonadoEnMonedaOriginal =");
            vSql.AppendLine("			(CASE");
            vSql.AppendLine("				WHEN (Cobranza.CodigoMoneda = DocumentoCobrado.CodigoMonedaDeCxC)	THEN (DocumentoCobrado.MontoAbonado)");
            vSql.AppendLine("				ELSE (CASE");
            vSql.AppendLine("						  WHEN (DocumentoCobrado.CambioAMonedaDeCobranza > 0)	THEN ");
            vSql.AppendLine("				          (CASE");
            vSql.AppendLine("						        WHEN (Cobranza.CodigoMoneda = '" + valParametroMonedaLocal + "')	THEN (ROUND((DocumentoCobrado.MontoAbonado / DocumentoCobrado.CambioAMonedaDeCobranza),2))");
            vSql.AppendLine("						        ELSE (ROUND((DocumentoCobrado.MontoAbonado * DocumentoCobrado.CambioAMonedaDeCobranza),2))");
            vSql.AppendLine("					        END)");
            vSql.AppendLine("				          ELSE (ROUND((DocumentoCobrado.MontoAbonado),2))");
            vSql.AppendLine("			    END)");
            vSql.AppendLine("			  END),");
            vSql.AppendLine("		CambioAMonedaLocal =");
            vSql.AppendLine("			(CASE");
            vSql.AppendLine("				WHEN (DocumentoCobrado.CodigoMonedaDeCxC = '" + valParametroMonedaLocal + "')	THEN (1)");
            vSql.AppendLine("				ELSE (CASE");
            vSql.AppendLine("						 WHEN (Cobranza.CodigoMoneda = DocumentoCobrado.CodigoMonedaDeCxC)	THEN (Cobranza.CambioAbolivares)");
            vSql.AppendLine("						 ELSE (DocumentoCobrado.CambioAMonedaDeCobranza)");
            vSql.AppendLine("					  END)");
            vSql.AppendLine("			END),");
            vSql.AppendLine("		SimboloMonedaDelAbono = DocumentoCobrado.SimboloMonedaDeCxC ");
            vSql.AppendLine("   FROM ");
            vSql.AppendLine("	    DocumentoCobrado");
            vSql.AppendLine("	    LEFT JOIN Cobranza");
            vSql.AppendLine("	    ON DocumentoCobrado.NumeroCobranza = Cobranza.Numero");
            vSql.AppendLine("	    AND  DocumentoCobrado.ConsecutivoCompania = Cobranza.ConsecutivoCompania");
            vSql.AppendLine("   WHERE");
            vSql.AppendLine("	    dbo.DocumentoCobrado.MontoAbonadoEnMonedaOriginal = 0 AND dbo.DocumentoCobrado.CambioAMonedaLocal = 1 AND  dbo.DocumentoCobrado.SimboloMonedaDelAbono = '' ");
            vSql.AppendLine("	    AND DocumentoCobrado.ConsecutivoCompania = " + valConsecutivoCompania.ToString());
            vSql.AppendLine("END");
            Execute(vSql.ToString());
        }

        private void AgregarTablaListaDeMateriales() {
            if(!TableExists("Adm.ListaDeMateriales")) {
                new Galac.Adm.Dal.GestionProduccion.clsListaDeMaterialesED().InstalarTabla();
            }
        }

        private void AgregarTablaOrdenDeProduccion() {
            if(!TableExists("Adm.OrdenDeProduccion")) {
                new Galac.Adm.Dal.GestionProduccion.clsOrdenDeProduccionED().InstalarTabla();
            }
        }

        private void AgregarCamposEnNotaEntradaSalida() {
            if(TableExists("dbo.NotaDeEntradaSalida")) {
                AddColumnEnumerative("dbo.NotaDeEntradaSalida", "GeneradoPor", "d_NotDeEntSalGePo DEFAULT ('0')", 0);
                AddColumnInteger("dbo.NotaDeEntradaSalida", "ConsecutivoDocumentoOrigen", "d_NotDeEntSalCoDoOr DEFAULT (0)", 0);
                AddColumnEnumerative("dbo.NotaDeEntradaSalida", "TipoNotaProduccion", "d_NotDeEntSalTiNoPr DEFAULT ('0')", 0);
                if(TableExists("dbo.RenglonNotaES")) {
                    AddColumnNumeric("dbo.RenglonNotaES", "CostoUnitario", 19, 4, "d_RenNotESCoUn DEFAULT (0)", 0);
                }
            }
        }

        private void NuevoCamposFacturaLimitedFechaCambio() {
            AddColumnInteger("dbo.factura", "NroDiasMantenerCambioAMonedaLocal", "");
            AddColumnDate("dbo.factura", "FechaLimiteCambioAMonedaLocal", "");
            AddColumnDate("dbo.cxC", "FechaLimiteCambioAMonedaLocal", "");
            StringBuilder vSQL = new StringBuilder();
            if(ColumnExists("dbo.factura", "NroDiasMantenerCambioAMonedaLocal")) {
                vSQL.AppendLine("SET dateformat dmy ");
                vSQL.AppendLine("WHILE EXISTS (SELECT NroDiasMantenerCambioAMonedaLocal FROM dbo.factura WHERE dbo.factura.NroDiasMantenerCambioAMonedaLocal IS NULL)");
                vSQL.AppendLine("BEGIN");
                vSQL.AppendLine("   UPDATE TOP (1000) dbo.factura ");
                vSQL.AppendLine("       SET    dbo.factura.NroDiasMantenerCambioAMonedaLocal = 0");
                vSQL.AppendLine("   FROM  dbo.factura");
                vSQL.AppendLine("   WHERE dbo.factura.NroDiasMantenerCambioAMonedaLocal IS NULL");
                vSQL.AppendLine("END");
                Execute(vSQL.ToString(), 0);
            }
            if(ColumnExists("dbo.factura", "FechaLimiteCambioAMonedaLocal")) {
                vSQL.Clear();
                vSQL.AppendLine("SET dateformat dmy ");
                vSQL.AppendLine("WHILE EXISTS (SELECT FechaLimiteCambioAMonedaLocal FROM dbo.factura WHERE dbo.factura.FechaLimiteCambioAMonedaLocal <> dbo.factura.Fecha)");
                vSQL.AppendLine("BEGIN");
                vSQL.AppendLine("   UPDATE TOP (1000) dbo.factura ");
                vSQL.AppendLine("       SET dbo.factura.FechaLimiteCambioAMonedaLocal = dbo.factura.Fecha");
                vSQL.AppendLine("   FROM  dbo.factura");
                vSQL.AppendLine("   WHERE dbo.factura.FechaLimiteCambioAMonedaLocal <> dbo.factura.Fecha");
                vSQL.AppendLine("END");
                Execute(vSQL.ToString(), 0);
            }
            if(ColumnExists("dbo.cxC", "FechaLimiteCambioAMonedaLocal")) {
                vSQL.Clear();
                vSQL.AppendLine("SET dateformat dmy ");
                vSQL.AppendLine("WHILE EXISTS (SELECT FechaLimiteCambioAMonedaLocal FROM dbo.cxC WHERE dbo.cxC.FechaLimiteCambioAMonedaLocal <> dbo.cxC.Fecha)");
                vSQL.AppendLine("BEGIN");
                vSQL.AppendLine("   UPDATE TOP (1000) dbo.cxC ");
                vSQL.AppendLine("       SET dbo.cxC.FechaLimiteCambioAMonedaLocal = dbo.cxC.Fecha");
                vSQL.AppendLine("   FROM  dbo.cxC");
                vSQL.AppendLine("   WHERE dbo.cxC.FechaLimiteCambioAMonedaLocal <> dbo.cxC.Fecha");
                vSQL.AppendLine("END");
                Execute(vSQL.ToString(), 0);
            }
        }

        private void AgregarCmbioABolivaresYCambioMostrarTotalEnDivisasEnCotizacion() {
            AddColumnCurrency("dbo.cotizacion", "CambioABolivares", "", 1);
            AddColumnCurrency("dbo.cotizacion", "CambioMostrarTotalEnDivisas", "", 1);
        }

        private void AgregaCampoEnAnticipoCobrado() {
            if(!ColumnExists("dbo.anticipoCobrado", "MontoAplicadoMonedaOriginal")) {
                AddColumnCurrency("dbo.anticipoCobrado", "MontoAplicadoMonedaOriginal", "", 0);
            }
            if(!ColumnExists("dbo.anticipoCobrado", "CambioAMonedaLocal")) {
                AddColumnCurrency("dbo.anticipoCobrado", "CambioAMonedaLocal", "", 1);
            }
            if(!ColumnExists("dbo.anticipoCobrado", "SimboloMonedaDelAbono")) {
                AddColumnString("dbo.anticipoCobrado", "SimboloMonedaDelAbono", 4, "", "");
            }
            BuscarConsecutivosCompaniasAnticipo();
        }

        private void BuscarConsecutivosCompaniasAnticipo() {
            StringBuilder vSQL = new StringBuilder();
            Galac.Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocal = new Galac.Comun.Brl.TablasGen.clsMonedaLocalActual();
            vMonedaLocal.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            if(ColumnExists("dbo.anticipoCobrado", "MontoAplicadoMonedaOriginal") &&
                ColumnExists("dbo.anticipoCobrado", "CambioAMonedaLocal") &&
                ColumnExists("dbo.anticipoCobrado", "SimboloMonedaDelAbono")) {
                vSQL.AppendLine("SELECT ConsecutivoCompania FROM dbo.Compania");
                DataSet ds = ExecuteDataset(vSQL.ToString(), -1);
                DataTableReader rd = ds.Tables[0].CreateDataReader();
                while(rd.Read()) {
                    EstablecerValoresEnCamposDeAnticipoCobrado((int)rd[0], vMonedaLocal.CodigoMoneda(LibDate.Today()));
                }
            }
        }

        private void EstablecerValoresEnCamposDeAnticipoCobrado(int valConsecutivoCompania, string valParametroMonedaLocal) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("WHILE EXISTS (SELECT TOP (1) dbo.anticipoCobrado.MontoAplicadoMonedaOriginal FROM dbo.anticipoCobrado WHERE dbo.anticipoCobrado.MontoAplicadoMonedaOriginal = 0 AND dbo.anticipoCobrado.CambioAMonedaLocal = 1 AND  dbo.anticipoCobrado.SimboloMonedaDelAbono = '' AND anticipoCobrado.ConsecutivoCompania =  " + valConsecutivoCompania.ToString() + ")");
            vSql.AppendLine("BEGIN");
            vSql.AppendLine("   UPDATE TOP(1000) dbo.anticipoCobrado");
            vSql.AppendLine("   SET		MontoAplicadoMonedaOriginal =");
            vSql.AppendLine("			(CASE");
            vSql.AppendLine("				WHEN (Cobranza.CodigoMoneda = anticipoCobrado.CodigoMoneda)	THEN (anticipoCobrado.MontoAplicado)");
            vSql.AppendLine("				ELSE (CASE");
            vSql.AppendLine("						  WHEN (anticipoCobrado.Cambio > 0)	THEN ");
            vSql.AppendLine("				          (CASE");
            vSql.AppendLine("						        WHEN (Cobranza.CodigoMoneda = '" + valParametroMonedaLocal + "')	THEN (ROUND((anticipoCobrado.MontoAplicado / anticipoCobrado.Cambio),2))");
            vSql.AppendLine("						        ELSE (ROUND((anticipoCobrado.MontoAplicado * anticipoCobrado.Cambio),2))");
            vSql.AppendLine("					        END)");
            vSql.AppendLine("				          ELSE (ROUND((anticipoCobrado.MontoAplicado),2))");
            vSql.AppendLine("			    END)");
            vSql.AppendLine("			  END),");
            vSql.AppendLine("		CambioAMonedaLocal =");
            vSql.AppendLine("			(CASE");
            vSql.AppendLine("				WHEN (anticipoCobrado.CodigoMoneda = '" + valParametroMonedaLocal + "')	THEN (1)");
            vSql.AppendLine("				ELSE (CASE");
            vSql.AppendLine("						 WHEN (Cobranza.CodigoMoneda = anticipoCobrado.CodigoMoneda)	THEN (Cobranza.CambioAbolivares)");
            vSql.AppendLine("						 ELSE (anticipoCobrado.Cambio)");
            vSql.AppendLine("					  END)");
            vSql.AppendLine("			END),");
            vSql.AppendLine("		SimboloMonedaDelAbono = anticipoCobrado.SimboloMoneda ");
            vSql.AppendLine("   FROM");
            vSql.AppendLine("	    anticipoCobrado");
            vSql.AppendLine("	    LEFT JOIN Cobranza");
            vSql.AppendLine("	    ON anticipoCobrado.NumeroCobranza = Cobranza.Numero");
            vSql.AppendLine("	    AND  anticipoCobrado.ConsecutivoCompania = Cobranza.ConsecutivoCompania");
            vSql.AppendLine("   WHERE");
            vSql.AppendLine("	    anticipoCobrado.MontoAplicadoMonedaOriginal = 0 AND dbo.anticipoCobrado.CambioAMonedaLocal = 1 AND  dbo.anticipoCobrado.SimboloMonedaDelAbono = ''  ");
            vSql.AppendLine("	    AND anticipoCobrado.ConsecutivoCompania = " + valConsecutivoCompania.ToString());
            vSql.AppendLine("END ");
            Execute(vSql.ToString());
        }

        public void AgregaParametrosParaNroDiasAMantenerTasaDeCambio() {
            AgregarNuevoParametro("NroDiasMantenerTasaCambio", "Factura", 2, "2.2.- Facturación (Continuación)", 2, "", eTipoDeDatoParametros.Int, "", '0', "N");
        }

        private void AjustarCamposDeTasaDeCambioDeCobranzaYDocumentoCobradoSiSonIgualACero() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT ConsecutivoCompania FROM dbo.Compania");
            DataSet ds = ExecuteDataset(vSql.ToString(), -1);
            DataTableReader rd = ds.Tables[0].CreateDataReader();
            while(rd.Read()) {
                AjustarCampoCambioABolivaresSiEsIgualACero((int)rd[0]);
                AjustarCampoCambioAMonedaDeCobranzaSiEsIgualACero((int)rd[0]);
            } 
        }

        private void AjustarCampoCambioABolivaresSiEsIgualACero(int valConsecutivoCompania) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("WHILE EXISTS (SELECT TOP(1) dbo.Cobranza.CambioAbolivares FROM dbo.Cobranza");
            vSql.AppendLine("			   WHERE (dbo.Cobranza.CambioAbolivares IS NULL  OR  dbo.Cobranza.CambioAbolivares = 0)");
            vSql.AppendLine("			   AND ConsecutivoCompania = " + valConsecutivoCompania.ToString() + ")");
            vSql.AppendLine("BEGIN");
            vSql.AppendLine("    UPDATE TOP(10000) dbo.Cobranza");
            vSql.AppendLine("           SET Cobranza.CambioAbolivares = 1");
            vSql.AppendLine("    WHERE (dbo.Cobranza.CambioAbolivares IS NULL  OR  dbo.Cobranza.CambioAbolivares = 0)");
            vSql.AppendLine("	 AND ConsecutivoCompania = " + valConsecutivoCompania.ToString());
            vSql.AppendLine("END");
            Execute(vSql.ToString());
        }

        private void AjustarCampoCambioAMonedaDeCobranzaSiEsIgualACero(int valConsecutivoCompania) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("WHILE EXISTS (SELECT TOP (1) dbo.DocumentoCobrado.CambioAMonedaDeCobranza FROM dbo.DocumentoCobrado");
            vSql.AppendLine("			   WHERE (dbo.DocumentoCobrado.CambioAMonedaDeCobranza IS NULL OR  dbo.DocumentoCobrado.CambioAMonedaDeCobranza = 0)");
            vSql.AppendLine("			   AND ConsecutivoCompania = " + valConsecutivoCompania.ToString() + ")");
            vSql.AppendLine("BEGIN");
            vSql.AppendLine("    UPDATE TOP(10000) dbo.DocumentoCobrado");
            vSql.AppendLine("           SET DocumentoCobrado.CambioAMonedaDeCobranza =  Cobranza.CambioAbolivares");
            vSql.AppendLine("    FROM dbo.DocumentoCobrado");
            vSql.AppendLine("    INNER JOIN dbo.Cobranza");
            vSql.AppendLine("    ON dbo.DocumentoCobrado.NumeroCobranza = dbo.Cobranza.Numero");
            vSql.AppendLine("    AND dbo.DocumentoCobrado.ConsecutivoCompania = dbo.Cobranza.ConsecutivoCompania");
            vSql.AppendLine("    WHERE (dbo.DocumentoCobrado.CambioAMonedaDeCobranza IS NULL");
            vSql.AppendLine("	 OR  dbo.DocumentoCobrado.CambioAMonedaDeCobranza = 0)");
            vSql.AppendLine("	 AND Documentocobrado.ConsecutivoCompania = " + valConsecutivoCompania.ToString());
            vSql.AppendLine("END");
            Execute(vSql.ToString());
        }

        private void ActualizarPermisosEnCobranzaModificarTasaDeCambio() {
            LibGUserReestScripts SqlSecurityLevel = new LibGUserReestScripts();
            StringBuilder vSql = new StringBuilder();
            List<string> vActions = new List<string>();

            vActions.Add("Modificar Tasa de Cambio");
            vSql.Append(SqlSecurityLevel.SqlAddSecurityLevel("Cobranza", vActions, "Cliente / CxC", 3, "SAW", null));
            foreach(var item in vActions) {
                vSql.Append(SqlUpdateSecurityLevelCobranzaModificarTasaDeCambio("Cobranza", item, false));
            }
            Execute(vSql.ToString(), -1);
        }

        public string SqlUpdateSecurityLevelCobranzaModificarTasaDeCambio(string valProjectModule, string valProjectAction, bool valHasHacces) {
            StringBuilder vSQLUpdate = new StringBuilder();
            QAdvSql vQAdvSql = new QAdvSql("");
            if(!LibString.IsNullOrEmpty(valProjectModule) && !LibString.S1IsEqualToS2(valProjectModule, "Usuario") && !LibString.IsNullOrEmpty(valProjectAction)) {
                vSQLUpdate.AppendLine("UPDATE Lib.GUserSecurity ");
                vSQLUpdate.AppendLine("SET HasAccess = " + vQAdvSql.ToSqlValue(valHasHacces));
                vSQLUpdate.AppendLine("      WHERE UserName <> " + vQAdvSql.ToSqlValue("JEFE"));
                vSQLUpdate.AppendLine("      AND ProjectModule = " + vQAdvSql.ToSqlValue(valProjectModule));
                vSQLUpdate.AppendLine("      AND ProjectAction = " + vQAdvSql.ToSqlValue(valProjectAction));
                vSQLUpdate.AppendLine("      AND Lib.GUserSecurity.UserName IN (SELECT Lib.GUserSecurity.UserName FROM Lib.GUserSecurity WHERE ");
                vSQLUpdate.AppendLine(" (ProjectModule = " + vQAdvSql.ToSqlValue(valProjectModule) + " AND ProjectAction = " + vQAdvSql.ToSqlValue("Insertar") + " AND HasAccess = " + vQAdvSql.ToSqlValue(false) + "))");
            }
            return vSQLUpdate.ToString();
        }

        private void AgregarCamposEnCotizacion() {
            if(!ColumnExists("dbo.cotizacion","NroDiasMantenerCambioAMonedaLocal")) {
                AddColumnInteger("dbo.cotizacion","NroDiasMantenerCambioAMonedaLocal","");
            }
            if(!ColumnExists("dbo.cotizacion","FechaLimiteCambioAMonedaLocal")) {
                AddColumnDate("dbo.cotizacion","FechaLimiteCambioAMonedaLocal","");
            }
            StringBuilder vSQL = new StringBuilder();
            if(ColumnExists("dbo.cotizacion","NroDiasMantenerCambioAMonedaLocal")) {
                vSQL.AppendLine("SET dateformat dmy ");
                vSQL.AppendLine("WHILE EXISTS (SELECT NroDiasMantenerCambioAMonedaLocal FROM dbo.cotizacion WHERE dbo.cotizacion.NroDiasMantenerCambioAMonedaLocal IS NULL)");
                vSQL.AppendLine("BEGIN");
                vSQL.AppendLine("   UPDATE TOP (1000) dbo.cotizacion ");
                vSQL.AppendLine("       SET    dbo.cotizacion.NroDiasMantenerCambioAMonedaLocal = 0");
                vSQL.AppendLine("   FROM  dbo.cotizacion");
                vSQL.AppendLine("   WHERE dbo.cotizacion.NroDiasMantenerCambioAMonedaLocal IS NULL");
                vSQL.AppendLine("END");
                Execute(vSQL.ToString(),0);
            }
            if(ColumnExists("dbo.cotizacion","FechaLimiteCambioAMonedaLocal")) {
                vSQL.Clear();
                vSQL.AppendLine("SET dateformat dmy ");
                vSQL.AppendLine("WHILE EXISTS (SELECT FechaLimiteCambioAMonedaLocal FROM dbo.cotizacion WHERE dbo.cotizacion.FechaLimiteCambioAMonedaLocal <> dbo.cotizacion.Fecha)");
                vSQL.AppendLine("BEGIN");
                vSQL.AppendLine("   UPDATE TOP (1000) dbo.cotizacion ");
                vSQL.AppendLine("       SET dbo.cotizacion.FechaLimiteCambioAMonedaLocal = dbo.cotizacion.Fecha");
                vSQL.AppendLine("   FROM  dbo.cotizacion");
                vSQL.AppendLine("   WHERE dbo.cotizacion.FechaLimiteCambioAMonedaLocal <> dbo.cotizacion.Fecha");
                vSQL.AppendLine("END");
                Execute(vSQL.ToString(),0);
            }

        }
        
        private void ActualizarPermisosEnCobranzaBuscarTasaDeCambioOriginalDeCxC() {
            LibGUserReestScripts SqlSecurityLevel = new LibGUserReestScripts();
            StringBuilder vSql = new StringBuilder();
            List<string> vActions = new List<string>();
            vActions.Add("Buscar Tasa de Cambio Original de CxC");
            vSql.Append(SqlSecurityLevel.SqlAddSecurityLevel("Cobranza", vActions, "Cliente / CxC", 3, "SAW", null));
            Execute(vSql.ToString(),-1);
        }
    }
}
