using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Brl;
using System.Data;
using LibGalac.Aos.Dal.Usal;
using System.Collections.Generic;
using LibGalac.Aos.DefGen;
using Galac.Comun.Ccl.SttDef;
using LibGalac.Aos.Base.Dal;
using System.Linq;
using System.Xml.Linq;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_11 : clsVersionARestructurar {

        public clsVersion6_11(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.11";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();           
            InstalarTablaAdmCaja();
            BuscarConsecutivosCompanias();
            BuscarConsecutivosCompaniasAnticipo();
            AjustarCampoCambioABolivaresSiEsIgualACeroEnCompras();
            AgregarParametroBusquedaDinamica();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void PrepararConsecutivoDeCaja() {
            LibGpParams vParams = new LibGpParams();
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT ConsecutivoCompania FROM dbo.Compania ORDER BY ConsecutivoCompania");
            XElement RetXml = LibBusiness.ExecuteSelect(vSql.ToString(),vParams.Get(),"",0);
            if(RetXml != null && RetXml.HasElements) {
                List<XElement> vListCompania = RetXml.Descendants("GpResult").ToList();
                vSql.Clear();
                vSql.AppendLine("; WITH CajaTemp AS (SELECT ROW_NUMBER() OVER (ORDER BY ConsecutivoCompania,ConsecutivoCaja) -1 AS  RowNumber ");
                vSql.AppendLine(",ConsecutivoCaja FROM dbo.Caja ");
                vSql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania)");
                vSql.AppendLine("UPDATE CajaTemp ");
                vSql.AppendLine("SET ConsecutivoCaja = RowNumber ");
                foreach(XElement vXmlCompania in vListCompania) {
                    int vConsecutivo = LibImportData.ToInt(LibXml.GetElementValueOrEmpty(vXmlCompania,"ConsecutivoCompania"));
                    vParams = new LibGpParams();
                    vParams.AddInInteger("ConsecutivoCompania",vConsecutivo);
                    LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(),vParams.Get(),"",0);
                }
            }
        }

        private void InstalarTablaAdmCaja() {
            bool IsReady = false;
            IsReady = new Galac.Adm.Dal.Venta.clsCajaED().InstalarTabla();
            if (IsReady) {
                IsReady = new Galac.Adm.Dal.Venta.clsCajaAperturaED().InstalarTabla();
                if (IsReady) {
                    PrepararConsecutivoDeCaja();
                    new DbMigrator.clsMigrarData(new string[] { "Caja", "CajaApertura" }).MigrarData();
                    if (TableExists("dbo.cajaapertura") && TableExists("dbo.caja")) {
                        if (ForeignKeyNameExists("fk_facturacaja")) {
                            IsReady &= DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Caja", "dbo.Factura");
                            IsReady &= AddForeignKey("Adm.Caja", "dbo.factura", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoCaja" }, false);
                        }
                        if (ForeignKeyNameExists("fk_AnticipoCaja")) {
                            IsReady &= DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.Caja", "dbo.Anticipo");
                        }
                        //IsReady &= AddForeignKey("Adm.Caja", "dbo.Anticipo", new string[] { "ConsecutivoCompania", "Consecutivo" }, new string[] { "ConsecutivoCompania", "ConsecutivoCaja" }, false);
                        IsReady &= ExecuteDropTable("dbo.cajaapertura");
                        IsReady &= ExecuteDropTable("dbo.caja");
                    }
                }
            }
            if (ViewExists("dbo.IGV_CajaTiposDeCobros")) {
                new LibGalac.Aos.Dal.LibViews().Drop("dbo.IGV_CajaTiposDeCobros");
            }
            if (IsReady) {
                clsCompatViews.CrearVistaDboCaja();
            }
            ReasignarCajaRegistradoraSiExiste();
        }

        private void ReasignarCajaRegistradoraSiExiste() {
            string vPath = System.IO.Path.Combine(LibWorkPaths.ProgramDir,"ADMCONF.INI");
            string vCajaLocal = "";
            int vPos = 0;
            if(LibIO.FileExists(vPath)) {
                vCajaLocal = LibIO.ReadFile(vPath);
                vCajaLocal = LibString.Replace(vCajaLocal,"\"","");
                vCajaLocal = LibString.Replace(vCajaLocal,"\r\n","");
                vPos = LibString.IndexOf(vCajaLocal,"=") + 1;
                vCajaLocal = LibString.SubString(vCajaLocal,vPos);
                vCajaLocal = LibString.Trim(vCajaLocal);
                Lib.ConfigHelper.AddKeyToAppSettings("CAJALOCAL",vCajaLocal);
            }
        }

        private void BuscarConsecutivosCompanias() {
            StringBuilder vSQL = new StringBuilder();
            Galac.Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocal = new Galac.Comun.Brl.TablasGen.clsMonedaLocalActual();
            vMonedaLocal.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            if (ColumnExists("dbo.DocumentoCobrado", "MontoAbonadoEnMonedaOriginal") &&
                ColumnExists("dbo.DocumentoCobrado", "CambioAMonedaLocal") &&
                ColumnExists("dbo.DocumentoCobrado", "SimboloMonedaDelAbono")) {
                vSQL.AppendLine("SELECT ConsecutivoCompania FROM dbo.Compania");
                DataSet ds = ExecuteDataset(vSQL.ToString(), -1);
                DataTableReader rd = ds.Tables[0].CreateDataReader();
                while (rd.Read()) {
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

        private void BuscarConsecutivosCompaniasAnticipo() {
            StringBuilder vSQL = new StringBuilder();
            Galac.Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocal = new Galac.Comun.Brl.TablasGen.clsMonedaLocalActual();
            vMonedaLocal.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            if (ColumnExists("dbo.anticipoCobrado", "MontoAplicadoMonedaOriginal") &&
                ColumnExists("dbo.anticipoCobrado", "CambioAMonedaLocal") &&
                ColumnExists("dbo.anticipoCobrado", "SimboloMonedaDelAbono")) {
                vSQL.AppendLine("SELECT ConsecutivoCompania FROM dbo.Compania");
                DataSet ds = ExecuteDataset(vSQL.ToString(), -1);
                DataTableReader rd = ds.Tables[0].CreateDataReader();
                while (rd.Read()) {
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

        private void AjustarCampoCambioABolivaresSiEsIgualACeroEnCompras() {
            int vConsecutivoCompania = 0;
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT ConsecutivoCompania FROM dbo.Compania");
            DataSet ds = ExecuteDataset(vSql.ToString(),-1);
            DataTableReader rd = ds.Tables[0].CreateDataReader();
            vSql.Clear();
            vSql.AppendLine("WHILE EXISTS (SELECT TOP (1) Adm.Compra.CambioABolivares FROM Adm.Compra ");
            vSql.AppendLine("			   WHERE (Adm.Compra.CambioABolivares = 0");
            vSql.AppendLine("			   AND ConsecutivoCompania = " + LibConvert.ToStr(vConsecutivoCompania) + ")");
            vSql.AppendLine("BEGIN ");
            vSql.AppendLine("    UPDATE TOP(5000) Adm.Compra");
            vSql.AppendLine("           SET Adm.Compra.CambioABolivares = 1");
            vSql.AppendLine("    FROM Adm.Compra");
            vSql.AppendLine("    WHERE (Adm.Compra.CambioABolivares = 0");
            vSql.AppendLine("	   AND ConsecutivoCompania = " + LibConvert.ToStr(vConsecutivoCompania) + ")");
            vSql.AppendLine(" END ");
        }

        private void AgregarParametroBusquedaDinamica() {
            AgregarNuevoParametro("UsaBusquedaDinamicaEnPuntoDeVenta","Factura",2,"2.6.- Punto De Venta",6,"",eTipoDeDatoParametros.String,"",'N',"N");
        }

    }
}
