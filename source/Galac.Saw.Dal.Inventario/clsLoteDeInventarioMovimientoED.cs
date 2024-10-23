using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Dal.Inventario {
    [LibMefDalComponentMetadata(typeof(clsLoteDeInventarioMovimientoED))]
    public class clsLoteDeInventarioMovimientoED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsLoteDeInventarioMovimientoED(): base(){
            DbSchema = "Saw";
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
            get { return "LoteDeInventarioMovimiento"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("LoteDeInventarioMovimiento", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnLotDeInvMovConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoLote" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnLotDeInvMovConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnLotDeInvMovConsecutiv NOT NULL, ");
            SQL.AppendLine("Fecha" + InsSql.DateTypeForDb() + " CONSTRAINT d_LotDeInvMovFe DEFAULT (''), ");
            SQL.AppendLine("Modulo" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_LotDeInvMovMo DEFAULT ('0'), ");
            SQL.AppendLine("TipoOperacion" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_LotDeInvMovTiOp DEFAULT ('0'), ");
            SQL.AppendLine("Cantidad" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_LotDeInvMovCa DEFAULT (0), ");
            SQL.AppendLine("ConsecutivoDocumentoOrigen" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT d_LotDeInvMovCoDoOr DEFAULT (0), ");
            SQL.AppendLine("NumeroDocumentoOrigen" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_LotDeInvMovNuDoOr DEFAULT (''), ");
            SQL.AppendLine("StatusDocumentoOrigen" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_LotDeInvMovStDoOr DEFAULT ('0'), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_LoteDeInventarioMovimiento PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, ConsecutivoLote ASC, Consecutivo ASC)");
            SQL.AppendLine(",CONSTRAINT fk_LoteDeInventarioMovimientoLoteDeInventario FOREIGN KEY (ConsecutivoCompania, ConsecutivoLote)");
            SQL.AppendLine("REFERENCES Saw.LoteDeInventario(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT ConsecutivoCompania, ConsecutivoLote, Consecutivo, Fecha");
            SQL.AppendLine(", Modulo, " + DbSchema + ".Gv_EnumOrigenLoteInv.StrValue AS ModuloStr, Cantidad, ConsecutivoDocumentoOrigen, NumeroDocumentoOrigen");
            SQL.AppendLine(", TipoOperacion, StatusDocumentoOrigen, " + DbSchema + ".Gv_EnumStatusDocOrigenLoteInv.StrValue AS StatusDocumentoOrigenStr");
            SQL.AppendLine(", LoteDeInventarioMovimiento.fldTimeStamp, CAST(LoteDeInventarioMovimiento.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".LoteDeInventarioMovimiento");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumOrigenLoteInv");
            SQL.AppendLine("ON " + DbSchema + ".LoteDeInventarioMovimiento.Modulo COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumOrigenLoteInv.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumStatusDocOrigenLoteInv");
            SQL.AppendLine("ON " + DbSchema + ".LoteDeInventarioMovimiento.StatusDocumentoOrigen COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumStatusDocOrigenLoteInv.DbValue");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoLote" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@Modulo" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@TipoOperacion" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@ConsecutivoDocumentoOrigen" + InsSql.NumericTypeForDb(10, 0) + " = 0,");
            SQL.AppendLine("@NumeroDocumentoOrigen" + InsSql.VarCharTypeForDb(30) + " = '',");
            SQL.AppendLine("@StatusDocumentoOrigen" + InsSql.CharTypeForDb(1) + " = '0'");
            return SQL.ToString();
        }

        private string SqlSpIns() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SET DATEFORMAT @DateFormat");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".LoteDeInventarioMovimiento(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            ConsecutivoLote,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            Fecha,");
            SQL.AppendLine("            Modulo,");
            SQL.AppendLine("            TipoOperacion,");
            SQL.AppendLine("            Cantidad,");
            SQL.AppendLine("            ConsecutivoDocumentoOrigen,");
            SQL.AppendLine("            NumeroDocumentoOrigen,");
            SQL.AppendLine("            StatusDocumentoOrigen)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @ConsecutivoLote,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @Fecha,");
            SQL.AppendLine("            @Modulo,");
            SQL.AppendLine("            @TipoOperacion,");
            SQL.AppendLine("            @Cantidad,");
            SQL.AppendLine("            @ConsecutivoDocumentoOrigen,");
            SQL.AppendLine("            @NumeroDocumentoOrigen,");
            SQL.AppendLine("            @StatusDocumentoOrigen)");
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
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoLote" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@Modulo" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@TipoOperacion" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@ConsecutivoDocumentoOrigen" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroDocumentoOrigen" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@StatusDocumentoOrigen" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@TimeStampAsInt" + InsSql.BigintTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpUpd() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SET DATEFORMAT @DateFormat");
            SQL.AppendLine("   DECLARE @CurrentTimeStamp timestamp");
            SQL.AppendLine("   DECLARE @ValidationMsg " + InsSql.VarCharTypeForDb(1500) + " --No puede ser más");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            //SQL.AppendLine("--DECLARE @CanBeChanged bit");
            SQL.AppendLine("   SET @ReturnValue = -1");
            SQL.AppendLine("   SET @ValidationMsg = ''");
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".LoteDeInventarioMovimiento WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoLote = @ConsecutivoLote AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".LoteDeInventarioMovimiento WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoLote = @ConsecutivoLote AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_LoteDeInventarioMovimientoCanBeUpdated @ConsecutivoCompania,@ConsecutivoLote,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".LoteDeInventarioMovimiento");
            SQL.AppendLine("            SET Fecha = @Fecha,");
            SQL.AppendLine("               Modulo = @Modulo,");
            SQL.AppendLine("               TipoOperacion = @TipoOperacion,");
            SQL.AppendLine("               Cantidad = @Cantidad,");
            SQL.AppendLine("               ConsecutivoDocumentoOrigen = @ConsecutivoDocumentoOrigen,");
            SQL.AppendLine("               NumeroDocumentoOrigen = @NumeroDocumentoOrigen,");
            SQL.AppendLine("               StatusDocumentoOrigen = @StatusDocumentoOrigen");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoLote = @ConsecutivoLote");
            SQL.AppendLine("               AND Consecutivo = @Consecutivo");
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
            SQL.AppendLine("@ConsecutivoLote" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".LoteDeInventarioMovimiento WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoLote = @ConsecutivoLote AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".LoteDeInventarioMovimiento WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoLote = @ConsecutivoLote AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_LoteDeInventarioMovimientoCanBeDeleted @ConsecutivoCompania,@ConsecutivoLote,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".LoteDeInventarioMovimiento");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoLote = @ConsecutivoLote");
            SQL.AppendLine("               AND Consecutivo = @Consecutivo");
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
            SQL.AppendLine("@ConsecutivoLote" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         ConsecutivoLote,");
            SQL.AppendLine("         Consecutivo,");
            SQL.AppendLine("         Fecha,");
            SQL.AppendLine("         Modulo,");
            SQL.AppendLine("         TipoOperacion,");
            SQL.AppendLine("         Cantidad,");
            SQL.AppendLine("         ConsecutivoDocumentoOrigen,");
            SQL.AppendLine("         NumeroDocumentoOrigen,");
            SQL.AppendLine("         StatusDocumentoOrigen,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".LoteDeInventarioMovimiento");
            SQL.AppendLine("      WHERE LoteDeInventarioMovimiento.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND LoteDeInventarioMovimiento.ConsecutivoLote = @ConsecutivoLote");
            SQL.AppendLine("         AND LoteDeInventarioMovimiento.Consecutivo = @Consecutivo");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoLote" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpSelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SELECT ");
            SQL.AppendLine("        ConsecutivoCompania,");
            SQL.AppendLine("        ConsecutivoLote,");
            SQL.AppendLine("        Consecutivo,");
            SQL.AppendLine("        Fecha,");
            SQL.AppendLine("        Modulo,");
            SQL.AppendLine("        TipoOperacion,");
            SQL.AppendLine("        Cantidad,");
            SQL.AppendLine("        ConsecutivoDocumentoOrigen,");
            SQL.AppendLine("        NumeroDocumentoOrigen,");
            SQL.AppendLine("        StatusDocumentoOrigen,");
            SQL.AppendLine("        fldTimeStamp");
            SQL.AppendLine("    FROM LoteDeInventarioMovimiento");
            SQL.AppendLine(" 	WHERE ConsecutivoLote = @ConsecutivoLote");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoLote" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpDelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	DELETE FROM LoteDeInventarioMovimiento");
            SQL.AppendLine(" 	WHERE ConsecutivoLote = @ConsecutivoLote");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInsDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoLote" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@XmlDataDetail" + InsSql.XmlTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpInsDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SET NOCOUNT ON;");
            SQL.AppendLine("   SET DATEFORMAT @DateFormat");
            SQL.AppendLine("	DECLARE @ReturnValue  " + InsSql.NumericTypeForDb(10, 0));
	        SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
	        SQL.AppendLine("	    BEGIN");
            SQL.AppendLine("	    EXEC Saw.Gp_LoteDeInventarioMovimientoDelDet @ConsecutivoCompania = @ConsecutivoCompania, @ConsecutivoLote = @ConsecutivoLote");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO Saw.LoteDeInventarioMovimiento(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        ConsecutivoLote,");
			SQL.AppendLine("	        Consecutivo,");
			SQL.AppendLine("	        Fecha,");
			SQL.AppendLine("	        Modulo,");
			SQL.AppendLine("	        TipoOperacion,");
			SQL.AppendLine("	        Cantidad,");
			SQL.AppendLine("	        ConsecutivoDocumentoOrigen,");
			SQL.AppendLine("	        NumeroDocumentoOrigen,");
			SQL.AppendLine("	        StatusDocumentoOrigen)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @ConsecutivoLote,");
			SQL.AppendLine("	        Consecutivo,");
			SQL.AppendLine("	        Fecha,");
			SQL.AppendLine("	        Modulo,");
			SQL.AppendLine("	        TipoOperacion,");
			SQL.AppendLine("	        Cantidad,");
			SQL.AppendLine("	        ConsecutivoDocumentoOrigen,");
			SQL.AppendLine("	        NumeroDocumentoOrigen,");
			SQL.AppendLine("	        StatusDocumentoOrigen");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataLoteDeInventarioMovimiento/GpDetailLoteDeInventarioMovimiento',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        Consecutivo " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        Fecha " + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("	        Modulo " + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("	        TipoOperacion " + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("	        Cantidad " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        ConsecutivoDocumentoOrigen " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        NumeroDocumentoOrigen " + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("	        StatusDocumentoOrigen " + InsSql.CharTypeForDb(1) + ") AS XmlDocDetailOfLoteDeInventario");
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
            bool vResult = insDbo.Create(DbSchema + ".LoteDeInventarioMovimiento", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumOrigenLoteInv", LibTpvCreator.SqlViewStandardEnum(typeof(eOrigenLoteInv), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipodeOperacion", LibTpvCreator.SqlViewStandardEnum(typeof(eTipodeOperacion), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumStatusDocOrigenLoteInv", LibTpvCreator.SqlViewStandardEnum(typeof(eStatusDocOrigenLoteInv), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_LoteDeInventarioMovimiento_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_LoteDeInventarioMovimientoINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_LoteDeInventarioMovimientoUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_LoteDeInventarioMovimientoDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_LoteDeInventarioMovimientoGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_LoteDeInventarioMovimientoSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_LoteDeInventarioMovimientoDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_LoteDeInventarioMovimientoInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".LoteDeInventarioMovimiento", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_LoteDeInventarioMovimientoINS");
            vResult = insSp.Drop(DbSchema + ".Gp_LoteDeInventarioMovimientoUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_LoteDeInventarioMovimientoDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_LoteDeInventarioMovimientoGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_LoteDeInventarioMovimientoInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_LoteDeInventarioMovimientoDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_LoteDeInventarioMovimientoSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_LoteDeInventarioMovimiento_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumOrigenLoteInv") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipodeOperacion") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumStatusDocOrigenLoteInv") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsLoteDeInventarioMovimientoED

} //End of namespace Galac.Saw.Dal.Inventario

