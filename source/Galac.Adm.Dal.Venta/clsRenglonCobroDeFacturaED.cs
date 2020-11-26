using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
//using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Dal.Venta {
    [LibMefDalComponentMetadata(typeof(clsRenglonCobroDeFacturaED))]
    public class clsRenglonCobroDeFacturaED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsRenglonCobroDeFacturaED(): base(){
            DbSchema = "Dbo";
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Miembros de ILibMefDalComponent
        string ILibMefDalComponent.DbSchema {
            get { return DbSchema; }
        }

        string ILibMefDalComponent.Name {
            get { return GetType().Name; }
        }

        string ILibMefDalComponent.Table {
            get { return "RenglonCobroDeFactura"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("RenglonCobroDeFactura", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnRenCobDeFacConsecutiv NOT NULL, ");
            SQL.AppendLine("NumeroFactura" + InsSql.VarCharTypeForDb(11) + " CONSTRAINT nnRenCobDeFacNumeroFact NOT NULL, ");
            SQL.AppendLine("TipoDeDocumento" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnRenCobDeFacTipoDeDocu NOT NULL, ");
            SQL.AppendLine("ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnRenCobDeFacConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoFormaDelCobro" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT d_RenCobDeFacCoFoDeCo DEFAULT (''), ");
            SQL.AppendLine("NumeroDelDocumento" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_RenCobDeFacNuDeDo DEFAULT (''), ");
            SQL.AppendLine("CodigoBanco" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT d_RenCobDeFacCoBa DEFAULT (0), ");
            SQL.AppendLine("Monto" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_RenCobDeFacMo DEFAULT (0), ");
            SQL.AppendLine("CodigoPuntoDeVenta" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT d_RenCobDeFacCoPuDeVe DEFAULT (0), ");
            SQL.AppendLine("NumeroDocumentoAprobacion" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_RenCobDeFacNuDoAp DEFAULT (''), ");
            SQL.AppendLine("CodigoMoneda" + InsSql.VarCharTypeForDb(4) + " CONSTRAINT d_RenCobDeFacCoMon DEFAULT ('VES'), ");
            SQL.AppendLine("CambioAMonedaLocal" + InsSql.NumericTypeForDb(25, 4) + " CONSTRAINT d_RenCobDeFacCaAMonLoc DEFAULT (1), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_RenglonCobroDeFactura PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, NumeroFactura ASC, TipoDeDocumento ASC, ConsecutivoRenglon ASC)");
            SQL.AppendLine(",CONSTRAINT fk_RenglonCobroDeFacturaFactura FOREIGN KEY (ConsecutivoCompania, NumeroFactura, TipoDeDocumento)");
            SQL.AppendLine("REFERENCES dbo.Factura(ConsecutivoCompania, Numero, TipoDeDocumento)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_RenglonCobroDeFacturaFormaDelCobro FOREIGN KEY (CodigoFormaDelCobro)");
            SQL.AppendLine("REFERENCES SAW.FormaDelCobro(Codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_RenglonCobroDeFacturaBanco FOREIGN KEY (CodigoBanco)");
            SQL.AppendLine("REFERENCES Comun.Banco(codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_RenglonCobroDeFacturaBanco FOREIGN KEY (CodigoPuntoDeVenta)");
            SQL.AppendLine("REFERENCES Comun.Banco(codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT RenglonCobroDeFactura.ConsecutivoCompania, RenglonCobroDeFactura.NumeroFactura, RenglonCobroDeFactura.TipoDeDocumento, " + DbSchema + ".Gv_EnumTipoDocumentoFactura.StrValue AS TipoDeDocumentoStr, RenglonCobroDeFactura.ConsecutivoRenglon");
            SQL.AppendLine(", RenglonCobroDeFactura.CodigoFormaDelCobro, RenglonCobroDeFactura.NumeroDelDocumento, RenglonCobroDeFactura.CodigoBanco, RenglonCobroDeFactura.Monto");
            SQL.AppendLine(", RenglonCobroDeFactura.CodigoPuntoDeVenta, RenglonCobroDeFactura.NumeroDocumentoAprobacion");
            SQL.AppendLine(", RenglonCobroDeFactura.fldTimeStamp, CAST(RenglonCobroDeFactura.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".RenglonCobroDeFactura");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoDocumentoFactura");
            SQL.AppendLine("ON " + DbSchema + ".RenglonCobroDeFactura.TipoDeDocumento COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDocumentoFactura.DbValue");
            SQL.AppendLine("INNER JOIN SAW.FormaDelCobro ON  " + DbSchema + ".RenglonCobroDeFactura.CodigoFormaDelCobro = SAW.FormaDelCobro.Codigo");
            //SQL.AppendLine("INNER JOIN Comun.Banco ON  " + DbSchema + ".RenglonCobroDeFactura.CodigoBanco = Comun.Banco.codigo");
            //SQL.AppendLine("INNER JOIN Comun.Banco ON  " + DbSchema + ".RenglonCobroDeFactura.CodigoPuntoDeVenta = Comun.Banco.codigo");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroFactura" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@TipoDeDocumento" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoFormaDelCobro" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@NumeroDelDocumento" + InsSql.VarCharTypeForDb(30) + " = '',");
            SQL.AppendLine("@CodigoBanco" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Monto" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@CodigoPuntoDeVenta" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroDocumentoAprobacion" + InsSql.VarCharTypeForDb(30) + " = ''");
            return SQL.ToString();
        }

        private string SqlSpIns() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".RenglonCobroDeFactura(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            NumeroFactura,");
            SQL.AppendLine("            TipoDeDocumento,");
            SQL.AppendLine("            ConsecutivoRenglon,");
            SQL.AppendLine("            CodigoFormaDelCobro,");
            SQL.AppendLine("            NumeroDelDocumento,");
            SQL.AppendLine("            CodigoBanco,");
            SQL.AppendLine("            Monto,");
            SQL.AppendLine("            CodigoPuntoDeVenta,");
            SQL.AppendLine("            NumeroDocumentoAprobacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @NumeroFactura,");
            SQL.AppendLine("            @TipoDeDocumento,");
            SQL.AppendLine("            @ConsecutivoRenglon,");
            SQL.AppendLine("            @CodigoFormaDelCobro,");
            SQL.AppendLine("            @NumeroDelDocumento,");
            SQL.AppendLine("            @CodigoBanco,");
            SQL.AppendLine("            @Monto,");
            SQL.AppendLine("            @CodigoPuntoDeVenta,");
            SQL.AppendLine("            @NumeroDocumentoAprobacion)");
            SQL.AppendLine("            SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("        COMMIT TRAN");
            SQL.AppendLine("        RETURN @ReturnValue ");
            SQL.AppendLine("	END");
            SQL.AppendLine("	ELSE");
            SQL.AppendLine("		RETURN -1");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpUpdParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroFactura" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@TipoDeDocumento" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoFormaDelCobro" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@NumeroDelDocumento" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@CodigoBanco" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Monto" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@CodigoPuntoDeVenta" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroDocumentoAprobacion" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@TimeStampAsInt" + InsSql.BigintTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpUpd() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @CurrentTimeStamp timestamp");
            SQL.AppendLine("   DECLARE @ValidationMsg " + InsSql.VarCharTypeForDb(1500) + " --No puede ser más");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            //SQL.AppendLine("--DECLARE @CanBeChanged bit");
            SQL.AppendLine("   SET @ReturnValue = -1");
            SQL.AppendLine("   SET @ValidationMsg = ''");
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".RenglonCobroDeFactura WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroFactura = @NumeroFactura AND TipoDeDocumento = @TipoDeDocumento AND ConsecutivoRenglon = @ConsecutivoRenglon)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".RenglonCobroDeFactura WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroFactura = @NumeroFactura AND TipoDeDocumento = @TipoDeDocumento AND ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_RenglonCobroDeFacturaCanBeUpdated @ConsecutivoCompania,@NumeroFactura,@TipoDeDocumento,@ConsecutivoRenglon, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".RenglonCobroDeFactura");
            SQL.AppendLine("            SET CodigoFormaDelCobro = @CodigoFormaDelCobro,");
            SQL.AppendLine("               NumeroDelDocumento = @NumeroDelDocumento,");
            SQL.AppendLine("               CodigoBanco = @CodigoBanco,");
            SQL.AppendLine("               Monto = @Monto,");
            SQL.AppendLine("               CodigoPuntoDeVenta = @CodigoPuntoDeVenta,");
            SQL.AppendLine("               NumeroDocumentoAprobacion = @NumeroDocumentoAprobacion");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND NumeroFactura = @NumeroFactura");
            SQL.AppendLine("               AND TipoDeDocumento = @TipoDeDocumento");
            SQL.AppendLine("               AND ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("         SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("         IF @@ERROR = 0");
            SQL.AppendLine("         BEGIN");
            SQL.AppendLine("            COMMIT TRAN");
            SQL.AppendLine("            IF @ReturnValue = 0");
            SQL.AppendLine("               RAISERROR('El registro ha sido modificado o eliminado por otro usuario', 14, 1)");
            SQL.AppendLine("         END");
            SQL.AppendLine("         ELSE");
            SQL.AppendLine("         BEGIN");
            SQL.AppendLine("            SET @ReturnValue = -1");
            SQL.AppendLine("            SET @ValidationMsg = 'Se ha producido un error al Modificar: ' + CAST(@@ERROR AS NVARCHAR(8))");
            SQL.AppendLine("            RAISERROR(@ValidationMsg, 14 ,1)");
            SQL.AppendLine("            ROLLBACK");
            SQL.AppendLine("         END");
            //SQL.AppendLine("--END");
            //SQL.AppendLine("--ELSE");
            //SQL.AppendLine("--	RAISERROR('El registro no puede ser modificado: %s', 14, 1, @ValidationMsg)");
            SQL.AppendLine("      END");
            SQL.AppendLine("      ELSE");
            SQL.AppendLine("         RAISERROR('El registro ha sido modificado o eliminado por otro usuario.', 14, 1)");
            SQL.AppendLine("   END");
            SQL.AppendLine("   ELSE");
            SQL.AppendLine("      RAISERROR('El registro no existe.', 14, 1)");
            SQL.AppendLine("   RETURN @ReturnValue");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroFactura" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@TipoDeDocumento" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@TimeStampAsInt" + InsSql.BigintTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpDel() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @CurrentTimeStamp timestamp");
            SQL.AppendLine("   DECLARE @ValidationMsg " + InsSql.VarCharTypeForDb(1500) + " --No puede ser más");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            //SQL.AppendLine("--DECLARE @CanBeDeleted bit");
            SQL.AppendLine("   SET @ReturnValue = -1");
            SQL.AppendLine("   SET @ValidationMsg = ''");
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".RenglonCobroDeFactura WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroFactura = @NumeroFactura AND TipoDeDocumento = @TipoDeDocumento AND ConsecutivoRenglon = @ConsecutivoRenglon)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".RenglonCobroDeFactura WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroFactura = @NumeroFactura AND TipoDeDocumento = @TipoDeDocumento AND ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_RenglonCobroDeFacturaCanBeDeleted @ConsecutivoCompania,@NumeroFactura,@TipoDeDocumento,@ConsecutivoRenglon, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".RenglonCobroDeFactura");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND NumeroFactura = @NumeroFactura");
            SQL.AppendLine("               AND TipoDeDocumento = @TipoDeDocumento");
            SQL.AppendLine("               AND ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("         SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("         IF @@ERROR = 0");
            SQL.AppendLine("         BEGIN");
            SQL.AppendLine("            COMMIT TRAN");
            SQL.AppendLine("            IF @ReturnValue = 0");
            SQL.AppendLine("               RAISERROR('El registro ha sido modificado o eliminado por otro usuario', 14, 1)");
            SQL.AppendLine("         END");
            SQL.AppendLine("         ELSE");
            SQL.AppendLine("         BEGIN");
            SQL.AppendLine("            SET @ReturnValue = -1");
            SQL.AppendLine("            SET @ValidationMsg = 'Se ha producido un error al Eliminar: ' + CAST(@@ERROR AS NVARCHAR(8))");
            SQL.AppendLine("            RAISERROR(@ValidationMsg, 14 ,1)");
            SQL.AppendLine("            ROLLBACK");
            SQL.AppendLine("         END");
            //SQL.AppendLine("--END");
            //SQL.AppendLine("--ELSE");
            //SQL.AppendLine("--	RAISERROR('El registro no puede ser eliminado: %s', 14, 1, @ValidationMsg)");
            SQL.AppendLine("      END");
            SQL.AppendLine("      ELSE");
            SQL.AppendLine("         RAISERROR('El registro ha sido modificado o eliminado por otro usuario.', 14, 1)");
            SQL.AppendLine("   END");
            SQL.AppendLine("   ELSE");
            SQL.AppendLine("      RAISERROR('El registro no existe.', 14, 1)");
            SQL.AppendLine("   RETURN @ReturnValue");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpGetParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroFactura" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@TipoDeDocumento" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         NumeroFactura,");
            SQL.AppendLine("         TipoDeDocumento,");
            SQL.AppendLine("         ConsecutivoRenglon,");
            SQL.AppendLine("         CodigoFormaDelCobro,");
            SQL.AppendLine("         NumeroDelDocumento,");
            SQL.AppendLine("         CodigoBanco,");
            SQL.AppendLine("         Monto,");
            SQL.AppendLine("         CodigoPuntoDeVenta,");
            SQL.AppendLine("         NumeroDocumentoAprobacion,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".RenglonCobroDeFactura");
            SQL.AppendLine("      WHERE RenglonCobroDeFactura.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND RenglonCobroDeFactura.NumeroFactura = @NumeroFactura");
            SQL.AppendLine("         AND RenglonCobroDeFactura.TipoDeDocumento = @TipoDeDocumento");
            SQL.AppendLine("         AND RenglonCobroDeFactura.ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroFactura" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@TipoDeDocumento" + InsSql.CharTypeForDb(1));
            return SQL.ToString();
        }

        private string SqlSpSelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SELECT ");
            SQL.AppendLine("        ConsecutivoCompania,");
            SQL.AppendLine("        NumeroFactura,");
            SQL.AppendLine("        TipoDeDocumento,");
            SQL.AppendLine("        ConsecutivoRenglon,");
            SQL.AppendLine("        CodigoFormaDelCobro,");
            SQL.AppendLine("        NumeroDelDocumento,");
            SQL.AppendLine("        CodigoBanco,");
            SQL.AppendLine("        Monto,");
            SQL.AppendLine("        CodigoPuntoDeVenta,");
            SQL.AppendLine("        NumeroDocumentoAprobacion,");
            SQL.AppendLine("        fldTimeStamp");
            SQL.AppendLine("    FROM RenglonCobroDeFactura");
            SQL.AppendLine(" 	WHERE TipoDeDocumento = @TipoDeDocumento");
            SQL.AppendLine(" 	AND NumeroFactura = @NumeroFactura");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroFactura" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@TipoDeDocumento" + InsSql.CharTypeForDb(1));
            return SQL.ToString();
        }

        private string SqlSpDelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	DELETE FROM RenglonCobroDeFactura");
            SQL.AppendLine(" 	WHERE TipoDeDocumento = @TipoDeDocumento");
            SQL.AppendLine(" 	AND NumeroFactura = @NumeroFactura");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInsDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroFactura" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@TipoDeDocumento" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@XmlDataDetail" + InsSql.XmlTypeForDb());
            return SQL.ToString();
        }
        
        private string SqlSpInsDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SET NOCOUNT ON;");
            SQL.AppendLine("	DECLARE @ReturnValue  " + InsSql.NumericTypeForDb(10, 0));
	        SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
	        SQL.AppendLine("	    BEGIN");
            SQL.AppendLine("	    EXEC dbo.Gp_RenglonCobroDeFacturaDelDet @ConsecutivoCompania = @ConsecutivoCompania, @NumeroFactura = @NumeroFactura, @TipoDeDocumento = @TipoDeDocumento");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO dbo.renglonCobroDeFactura(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        NumeroFactura,");
			SQL.AppendLine("	        TipoDeDocumento,");
			SQL.AppendLine("	        ConsecutivoRenglon,");
			SQL.AppendLine("	        CodigoFormaDelCobro,");
			SQL.AppendLine("	        NumeroDelDocumento,");
			SQL.AppendLine("	        CodigoBanco,");
			SQL.AppendLine("	        Monto,");
			SQL.AppendLine("	        CodigoPuntoDeVenta,");
			SQL.AppendLine("	        NumeroDocumentoAprobacion,");
			SQL.AppendLine("	        CodigoMoneda,");
			SQL.AppendLine("	        CambioAMonedaLocal)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @NumeroFactura,");
			SQL.AppendLine("	        TipoDeDocumento,");
			SQL.AppendLine("	        ConsecutivoRenglon,");
			SQL.AppendLine("	        CodigoFormaDelCobro,");
			SQL.AppendLine("	        NumeroDelDocumento,");
			SQL.AppendLine("	        CodigoBanco,");
			SQL.AppendLine("	        Monto,");
			SQL.AppendLine("	        CodigoPuntoDeVenta,");
            SQL.AppendLine("	        NumeroDocumentoAprobacion,");
            SQL.AppendLine("	        CodigoMoneda,");
            SQL.AppendLine("	        CambioAMonedaLocal");
            SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        TipoDeDocumento " + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("	        ConsecutivoRenglon " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        CodigoFormaDelCobro " + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("	        NumeroDelDocumento " + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("	        CodigoBanco " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        Monto " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        CodigoPuntoDeVenta " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        NumeroDocumentoAprobacion " + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("	        CodigoMoneda " + InsSql.VarCharTypeForDb(4) + ",");
            SQL.AppendLine("	        CambioAMonedaLocal " + InsSql.DecimalTypeForDb(25,4) + ") AS XmlDocDetailOfFactura");
            SQL.AppendLine("	    EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("	    SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("	    RETURN @ReturnValue");
	        SQL.AppendLine("	END");
	        SQL.AppendLine("	ELSE");
            SQL.AppendLine("	    RETURN -1");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".RenglonCobroDeFactura", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDocumentoFactura", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDocumentoFactura), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_RenglonCobroDeFactura_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonCobroDeFacturaINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonCobroDeFacturaUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonCobroDeFacturaDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonCobroDeFacturaGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonCobroDeFacturaSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonCobroDeFacturaDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonCobroDeFacturaInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
            insSps.Dispose();
            return vResult;
        }

        public bool InstalarTabla() {
            bool vResult = false;
            if (CrearTabla()) {
                CrearVistas();
                CrearProcedimientos();
                vResult = true;
            }
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = false;
            if (insDbo.Exists(DbSchema + ".RenglonCobroDeFactura", eDboType.Tabla)) {
                CrearVistas();
                CrearProcedimientos();
                vResult = new clsCobroDeFacturaRapidaDetalleED().InstalarVistasYSps();
                vResult = true;
            }
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = false;
            LibStoredProc insSp = new LibStoredProc();
            LibViews insVista = new LibViews();
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonCobroDeFacturaINS");
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonCobroDeFacturaUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonCobroDeFacturaDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonCobroDeFacturaGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonCobroDeFacturaInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonCobroDeFacturaDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonCobroDeFacturaSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_RenglonCobroDeFactura_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDocumentoFactura") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsRenglonCobroDeFacturaED

} //End of namespace Galac..Dal.ComponenteNoEspecificado

