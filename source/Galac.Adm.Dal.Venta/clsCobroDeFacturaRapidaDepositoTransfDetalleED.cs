using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Dal.Venta {
    [LibMefDalComponentMetadata(typeof(clsCobroDeFacturaRapidaDepositoTransfDetalleED))]
    public class clsCobroDeFacturaRapidaDepositoTransfDetalleED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsCobroDeFacturaRapidaDepositoTransfDetalleED(): base(){
            DbSchema = "Adm";
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
            get { return "CobroDeFacturaRapidaDepositoTransfDetalle"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("CobroDeFacturaRapidaDepositoTransfDetalle", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnCobDeFacRapDepTraDetConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoFormaDelCobro" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT nnCobDeFacRapDepTraDetCodigoForm NOT NULL, ");
            SQL.AppendLine("NumeroDelDocumento" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_CobDeFacRapDepTraDetNuDeDo DEFAULT (''), ");
            SQL.AppendLine("CodigoBanco" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnCobDeFacRapDepTraDetCodigoBanc NOT NULL, ");
            SQL.AppendLine("Monto" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobDeFacRapDepTraDetMo DEFAULT (0), ");
            SQL.AppendLine("NumeroDocumentoAprobacion" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_CobDeFacRapDepTraDetNuDoAp DEFAULT (''), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_CobroDeFacturaRapidaDepositoTransfDetalle PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, CodigoFormaDelCobro ASC)");
            SQL.AppendLine(",CONSTRAINT fk_CobroDeFacturaRapidaDepositoTransfDetalleCobroDeFacturaRapidaDepositoTransf FOREIGN KEY (ConsecutivoCompania)");
            SQL.AppendLine("REFERENCES Adm.CobroDeFacturaRapidaDepositoTransf(ConsecutivoCompania, NumeroFactura)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_CobroDeFacturaRapidaDepositoTransfDetalleFormaDelCobro FOREIGN KEY (CodigoFormaDelCobro)");
            SQL.AppendLine("REFERENCES SAW.FormaDelCobro(Codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_CobroDeFacturaRapidaDepositoTransfDetalleBanco FOREIGN KEY (CodigoBanco)");
            SQL.AppendLine("REFERENCES Comun.Banco(codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT CobroDeFacturaRapidaDepositoTransfDetalle.ConsecutivoCompania, CobroDeFacturaRapidaDepositoTransfDetalle.CodigoFormaDelCobro, CobroDeFacturaRapidaDepositoTransfDetalle.NumeroDelDocumento, CobroDeFacturaRapidaDepositoTransfDetalle.CodigoBanco");
            SQL.AppendLine(", CobroDeFacturaRapidaDepositoTransfDetalle.Monto, CobroDeFacturaRapidaDepositoTransfDetalle.NumeroDocumentoAprobacion");
            SQL.AppendLine(", Comun.Banco.Nombre AS NombreBanco");
            SQL.AppendLine(", CobroDeFacturaRapidaDepositoTransfDetalle.fldTimeStamp, CAST(CobroDeFacturaRapidaDepositoTransfDetalle.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".CobroDeFacturaRapidaDepositoTransfDetalle");
            SQL.AppendLine("INNER JOIN SAW.FormaDelCobro ON  " + DbSchema + ".CobroDeFacturaRapidaDepositoTransfDetalle.CodigoFormaDelCobro = SAW.FormaDelCobro.Codigo");
            SQL.AppendLine("INNER JOIN Comun.Banco ON  " + DbSchema + ".CobroDeFacturaRapidaDepositoTransfDetalle.CodigoBanco = Comun.Banco.codigo");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoFormaDelCobro" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@NumeroDelDocumento" + InsSql.VarCharTypeForDb(30) + " = '',");
            SQL.AppendLine("@CodigoBanco" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Monto" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".CobroDeFacturaRapidaDepositoTransfDetalle(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            CodigoFormaDelCobro,");
            SQL.AppendLine("            NumeroDelDocumento,");
            SQL.AppendLine("            CodigoBanco,");
            SQL.AppendLine("            Monto,");
            SQL.AppendLine("            NumeroDocumentoAprobacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @CodigoFormaDelCobro,");
            SQL.AppendLine("            @NumeroDelDocumento,");
            SQL.AppendLine("            @CodigoBanco,");
            SQL.AppendLine("            @Monto,");
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
            SQL.AppendLine("@CodigoFormaDelCobro" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@NumeroDelDocumento" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@CodigoBanco" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Monto" + InsSql.DecimalTypeForDb(25, 4) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CobroDeFacturaRapidaDepositoTransfDetalle WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoFormaDelCobro = @CodigoFormaDelCobro)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CobroDeFacturaRapidaDepositoTransfDetalle WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoFormaDelCobro = @CodigoFormaDelCobro");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_CobroDeFacturaRapidaDepositoTransfDetalleCanBeUpdated @ConsecutivoCompania,@CodigoFormaDelCobro, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".CobroDeFacturaRapidaDepositoTransfDetalle");
            SQL.AppendLine("            SET NumeroDelDocumento = @NumeroDelDocumento,");
            SQL.AppendLine("               CodigoBanco = @CodigoBanco,");
            SQL.AppendLine("               Monto = @Monto,");
            SQL.AppendLine("               NumeroDocumentoAprobacion = @NumeroDocumentoAprobacion");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND CodigoFormaDelCobro = @CodigoFormaDelCobro");
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
            SQL.AppendLine("@CodigoFormaDelCobro" + InsSql.VarCharTypeForDb(5) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CobroDeFacturaRapidaDepositoTransfDetalle WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoFormaDelCobro = @CodigoFormaDelCobro)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CobroDeFacturaRapidaDepositoTransfDetalle WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoFormaDelCobro = @CodigoFormaDelCobro");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_CobroDeFacturaRapidaDepositoTransfDetalleCanBeDeleted @ConsecutivoCompania,@CodigoFormaDelCobro, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".CobroDeFacturaRapidaDepositoTransfDetalle");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND CodigoFormaDelCobro = @CodigoFormaDelCobro");
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
            SQL.AppendLine("@CodigoFormaDelCobro" + InsSql.VarCharTypeForDb(5));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         CodigoFormaDelCobro,");
            SQL.AppendLine("         NumeroDelDocumento,");
            SQL.AppendLine("         CodigoBanco,");
            SQL.AppendLine("         Monto,");
            SQL.AppendLine("         NumeroDocumentoAprobacion,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".CobroDeFacturaRapidaDepositoTransfDetalle");
            SQL.AppendLine("      WHERE CobroDeFacturaRapidaDepositoTransfDetalle.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND CobroDeFacturaRapidaDepositoTransfDetalle.CodigoFormaDelCobro = @CodigoFormaDelCobro");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpSelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SELECT ");
            SQL.AppendLine("        ConsecutivoCompania,");
            SQL.AppendLine("        CodigoFormaDelCobro,");
            SQL.AppendLine("        NumeroDelDocumento,");
            SQL.AppendLine("        CodigoBanco,");
            SQL.AppendLine("        Monto,");
            SQL.AppendLine("        NumeroDocumentoAprobacion,");
            SQL.AppendLine("        fldTimeStamp");
            SQL.AppendLine("    FROM CobroDeFacturaRapidaDepositoTransfDetalle");
            SQL.AppendLine(" 	WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpDelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	DELETE FROM CobroDeFacturaRapidaDepositoTransfDetalle");
            SQL.AppendLine(" 	WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInsDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("	    EXEC Adm.Gp_CobroDeFacturaRapidaDepositoTransfDetalleDelDet @ConsecutivoCompania = @ConsecutivoCompania, @CodigoFormaDelCobro = @CodigoFormaDelCobro");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO Adm.CobroDeFacturaRapidaDepositoTransfDetalle(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        CodigoFormaDelCobro,");
			SQL.AppendLine("	        NumeroDelDocumento,");
			SQL.AppendLine("	        CodigoBanco,");
			SQL.AppendLine("	        Monto,");
			SQL.AppendLine("	        NumeroDocumentoAprobacion)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @CodigoFormaDelCobro,");
			SQL.AppendLine("	        NumeroDelDocumento,");
			SQL.AppendLine("	        CodigoBanco,");
			SQL.AppendLine("	        Monto,");
			SQL.AppendLine("	        NumeroDocumentoAprobacion");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataCobroDeFacturaRapidaDepositoTransfDetalle/GpDetailCobroDeFacturaRapidaDepositoTransfDetalle',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        NumeroDelDocumento " + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("	        CodigoBanco " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        Monto " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        NumeroDocumentoAprobacion " + InsSql.VarCharTypeForDb(30) + ") AS XmlDocDetailOfCobroDeFacturaRapidaDepositoTransf");
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
            bool vResult = insDbo.Create(DbSchema + ".CobroDeFacturaRapidaDepositoTransfDetalle", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_CobroDeFacturaRapidaDepositoTransfDetalle_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaDepositoTransfDetalleINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaDepositoTransfDetalleUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaDepositoTransfDetalleDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaDepositoTransfDetalleGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaDepositoTransfDetalleSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaDepositoTransfDetalleDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaDepositoTransfDetalleInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".CobroDeFacturaRapidaDepositoTransfDetalle", eDboType.Tabla)) {
                CrearVistas();
                CrearProcedimientos();
                vResult = true;
            }
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = false;
            LibStoredProc insSp = new LibStoredProc();
            LibViews insVista = new LibViews();
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaDepositoTransfDetalleINS");
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaDepositoTransfDetalleUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaDepositoTransfDetalleDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaDepositoTransfDetalleGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaDepositoTransfDetalleInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaDepositoTransfDetalleDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaDepositoTransfDetalleSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_CobroDeFacturaRapidaDepositoTransfDetalle_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCobroDeFacturaRapidaDepositoTransfDetalleED

} //End of namespace Galac.Adm.Dal.Venta

