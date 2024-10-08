using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Dal.Inventario {
    [LibMefDalComponentMetadata(typeof(clsRenglonNotaESED))]
    public class clsRenglonNotaESED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsRenglonNotaESED(): base(){
            DbSchema = "dbo";
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
            get { return "RenglonNotaES"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("RenglonNotaES", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnRenNotESConsecutiv NOT NULL, ");
            SQL.AppendLine("NumeroDocumento" + InsSql.VarCharTypeForDb(11) + " CONSTRAINT nnRenNotESNumeroDocu NOT NULL, ");
            SQL.AppendLine("ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnRenNotESConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoArticulo" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT nnRenNotESCodigoArti NOT NULL, ");
            SQL.AppendLine("Cantidad" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_RenNotESCa DEFAULT (0), ");
            SQL.AppendLine("TipoArticuloInv" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnRenNotESTipoArticu NOT NULL, ");
            SQL.AppendLine("Serial" + InsSql.VarCharTypeForDb(50) + " CONSTRAINT d_RenNotESSe DEFAULT (''), ");
            SQL.AppendLine("Rollo" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_RenNotESRo DEFAULT (''), ");
            SQL.AppendLine("CostoUnitario" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_RenNotESCoUn DEFAULT (0), ");
            SQL.AppendLine("CostoUnitarioME" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_RenNotESCoUnME DEFAULT (0), ");
            SQL.AppendLine("LoteDeInventario" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_RenNotESLoDeIn DEFAULT (''), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_RenglonNotaES PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, NumeroDocumento ASC, ConsecutivoRenglon ASC)");
            SQL.AppendLine(",CONSTRAINT fk_RenglonNotaESNotaDeEntradaSalida FOREIGN KEY (ConsecutivoCompania, NumeroDocumento)");
            SQL.AppendLine("REFERENCES dbo.NotaDeEntradaSalida(ConsecutivoCompania, NumeroDocumento)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_RenglonNotaESArticuloInventario FOREIGN KEY (ConsecutivoCompania, CodigoArticulo)");
            SQL.AppendLine("REFERENCES dbo.ArticuloInventario(ConsecutivoCompania, Codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT RenglonNotaES.ConsecutivoCompania, RenglonNotaES.NumeroDocumento, RenglonNotaES.ConsecutivoRenglon, RenglonNotaES.CodigoArticulo");
            SQL.AppendLine(", RenglonNotaES.Cantidad, RenglonNotaES.TipoArticuloInv, " + DbSchema + ".Gv_EnumTipoArticuloInv.StrValue AS TipoArticuloInvStr, RenglonNotaES.Serial, RenglonNotaES.Rollo");
            SQL.AppendLine(", RenglonNotaES.CostoUnitario, RenglonNotaES.CostoUnitarioME, RenglonNotaES.LoteDeInventario");
            SQL.AppendLine(", dbo.ArticuloInventario.Descripcion AS DescripcionArticulo");
            SQL.AppendLine(", RenglonNotaES.fldTimeStamp, CAST(RenglonNotaES.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".RenglonNotaES");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoArticuloInv");
            SQL.AppendLine("ON " + DbSchema + ".RenglonNotaES.TipoArticuloInv COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoArticuloInv.DbValue");
            SQL.AppendLine("INNER JOIN dbo.ArticuloInventario ON  " + DbSchema + ".RenglonNotaES.CodigoArticulo = dbo.ArticuloInventario.Codigo");
            SQL.AppendLine("      AND " + DbSchema + ".RenglonNotaES.ConsecutivoCompania = dbo.ArticuloInventario.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TipoArticuloInv" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Serial" + InsSql.VarCharTypeForDb(50) + " = '',");
            SQL.AppendLine("@Rollo" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@CostoUnitario" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@CostoUnitarioME" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@LoteDeInventario" + InsSql.VarCharTypeForDb(30) + "");
            return SQL.ToString();
        }

        private string SqlSpIns() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".RenglonNotaES(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            NumeroDocumento,");
            SQL.AppendLine("            ConsecutivoRenglon,");
            SQL.AppendLine("            CodigoArticulo,");
            SQL.AppendLine("            Cantidad,");
            SQL.AppendLine("            TipoArticuloInv,");
            SQL.AppendLine("            Serial,");
            SQL.AppendLine("            Rollo,");
            SQL.AppendLine("            CostoUnitario,");
            SQL.AppendLine("            CostoUnitarioME,");
            SQL.AppendLine("            LoteDeInventario)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @NumeroDocumento,");
            SQL.AppendLine("            @ConsecutivoRenglon,");
            SQL.AppendLine("            @CodigoArticulo,");
            SQL.AppendLine("            @Cantidad,");
            SQL.AppendLine("            @TipoArticuloInv,");
            SQL.AppendLine("            @Serial,");
            SQL.AppendLine("            @Rollo,");
            SQL.AppendLine("            @CostoUnitario,");
            SQL.AppendLine("            @CostoUnitarioME,");
            SQL.AppendLine("            @LoteDeInventario)");
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
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TipoArticuloInv" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Serial" + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("@Rollo" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@CostoUnitario" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@CostoUnitarioME" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@LoteDeInventario" + InsSql.VarCharTypeForDb(30) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".RenglonNotaES WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroDocumento = @NumeroDocumento AND ConsecutivoRenglon = @ConsecutivoRenglon)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".RenglonNotaES WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroDocumento = @NumeroDocumento AND ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_RenglonNotaESCanBeUpdated @ConsecutivoCompania,@NumeroDocumento,@ConsecutivoRenglon, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".RenglonNotaES");
            SQL.AppendLine("            SET CodigoArticulo = @CodigoArticulo,");
            SQL.AppendLine("               Cantidad = @Cantidad,");
            SQL.AppendLine("               TipoArticuloInv = @TipoArticuloInv,");
            SQL.AppendLine("               Serial = @Serial,");
            SQL.AppendLine("               Rollo = @Rollo,");
            SQL.AppendLine("               CostoUnitario = @CostoUnitario,");
            SQL.AppendLine("               CostoUnitarioME = @CostoUnitarioME,");
            SQL.AppendLine("               LoteDeInventario = @LoteDeInventario");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND NumeroDocumento = @NumeroDocumento");
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
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(11) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".RenglonNotaES WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroDocumento = @NumeroDocumento AND ConsecutivoRenglon = @ConsecutivoRenglon)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".RenglonNotaES WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroDocumento = @NumeroDocumento AND ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_RenglonNotaESCanBeDeleted @ConsecutivoCompania,@NumeroDocumento,@ConsecutivoRenglon, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".RenglonNotaES");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND NumeroDocumento = @NumeroDocumento");
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
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         NumeroDocumento,");
            SQL.AppendLine("         ConsecutivoRenglon,");
            SQL.AppendLine("         CodigoArticulo,");
            SQL.AppendLine("         Cantidad,");
            SQL.AppendLine("         TipoArticuloInv,");
            SQL.AppendLine("         Serial,");
            SQL.AppendLine("         Rollo,");
            SQL.AppendLine("         CostoUnitario,");
            SQL.AppendLine("         CostoUnitarioME,");
            SQL.AppendLine("         LoteDeInventario,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".RenglonNotaES");
            SQL.AppendLine("      WHERE RenglonNotaES.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND RenglonNotaES.NumeroDocumento = @NumeroDocumento");
            SQL.AppendLine("         AND RenglonNotaES.ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(11));
            return SQL.ToString();
        }

        private string SqlSpSelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SELECT ");
            SQL.AppendLine("        RenglonNotaES.ConsecutivoCompania,");
            SQL.AppendLine("        NumeroDocumento,");
            SQL.AppendLine("        ConsecutivoRenglon,");
            SQL.AppendLine("        RenglonNotaES.CodigoArticulo,");
            SQL.AppendLine("        Cantidad,");
            SQL.AppendLine("        TipoArticuloInv,");
            SQL.AppendLine("        Serial,");
            SQL.AppendLine("        Rollo,");
            SQL.AppendLine("        CostoUnitario,");
            SQL.AppendLine("        CostoUnitarioME,");
            SQL.AppendLine("        LoteDeInventario,");
            SQL.AppendLine("        Saw.LoteDeInventario.FechaDeElaboracion,");
            SQL.AppendLine("        Saw.LoteDeInventario.FechaDeVencimiento,");
            SQL.AppendLine("        RenglonNotaES.fldTimeStamp");
            SQL.AppendLine("    FROM RenglonNotaES");
            SQL.AppendLine("    LEFT JOIN  Saw.LoteDeInventario On RenglonNotaES.ConsecutivoCompania = Saw.LoteDeInventario.ConsecutivoCompania ");
            SQL.AppendLine("    AND RenglonNotaES.CodigoArticulo = Saw.LoteDeInventario.CodigoArticulo ");
            SQL.AppendLine("    AND RenglonNotaES.LoteDeInventario = Saw.LoteDeInventario.CodigoLote ");
            SQL.AppendLine(" 	WHERE NumeroDocumento = @NumeroDocumento");
            SQL.AppendLine(" 	AND RenglonNotaES.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(11));
            return SQL.ToString();
        }

        private string SqlSpDelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	DELETE FROM RenglonNotaES");
            SQL.AppendLine(" 	WHERE NumeroDocumento = @NumeroDocumento");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInsDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@XmlDataDetail" + InsSql.XmlTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpInsDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SET NOCOUNT ON;");
            SQL.AppendLine("	DECLARE @ReturnValue  " + InsSql.NumericTypeForDb(10, 0));
	        SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
	        SQL.AppendLine("	    BEGIN");
            SQL.AppendLine("	    EXEC dbo.Gp_RenglonNotaESDelDet @ConsecutivoCompania = @ConsecutivoCompania, @NumeroDocumento = @NumeroDocumento");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO dbo.RenglonNotaES(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        NumeroDocumento,");
			SQL.AppendLine("	        ConsecutivoRenglon,");
			SQL.AppendLine("	        CodigoArticulo,");
			SQL.AppendLine("	        Cantidad,");
			SQL.AppendLine("	        TipoArticuloInv,");
			SQL.AppendLine("	        Serial,");
			SQL.AppendLine("	        Rollo,");
			SQL.AppendLine("	        CostoUnitario,");
			SQL.AppendLine("	        CostoUnitarioME,");
			SQL.AppendLine("	        LoteDeInventario)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @NumeroDocumento,");
			SQL.AppendLine("	        ConsecutivoRenglon,");
			SQL.AppendLine("	        CodigoArticulo,");
			SQL.AppendLine("	        Cantidad,");
			SQL.AppendLine("	        TipoArticuloInv,");
			SQL.AppendLine("	        Serial,");
			SQL.AppendLine("	        Rollo,");
			SQL.AppendLine("	        CostoUnitario,");
			SQL.AppendLine("	        CostoUnitarioME,");
			SQL.AppendLine("	        LoteDeInventario");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataRenglonNotaES/GpDetailRenglonNotaES',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        ConsecutivoRenglon " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        CodigoArticulo " + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("	        Cantidad " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        TipoArticuloInv " + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("	        Serial " + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("	        Rollo " + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("	        CostoUnitario " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        CostoUnitarioME " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        LoteDeInventario " + InsSql.VarCharTypeForDb(30) + ") AS XmlDocDetailOfNotaDeEntradaSalida");
            SQL.AppendLine("	    EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("	    SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("	    RETURN @ReturnValue");
	        SQL.AppendLine("	END");
	        SQL.AppendLine("	ELSE");
            SQL.AppendLine("	    RETURN -1");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSearchParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@SQLWhere" + InsSql.VarCharTypeForDb(2000) + " = null,");
            SQL.AppendLine("@SQLOrderBy" + InsSql.VarCharTypeForDb(500) + " = null,");
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + " = null,");
            SQL.AppendLine("@UseTopClausule" + InsSql.VarCharTypeForDb(1) + " = 'S'");
            return SQL.ToString();
        }

        private string SqlSpSearch() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @strSQL AS " + InsSql.VarCharTypeForDb(7000));
            SQL.AppendLine("   DECLARE @TopClausule AS " + InsSql.VarCharTypeForDb(10));
            SQL.AppendLine("   IF(@UseTopClausule = 'S') ");
            SQL.AppendLine("    SET @TopClausule = 'TOP 500'");
            SQL.AppendLine("   ELSE ");
            SQL.AppendLine("    SET @TopClausule = ''");
            SQL.AppendLine("   SET @strSQL = ");
            SQL.AppendLine("    ' SET DateFormat ' + @DateFormat + ");
            SQL.AppendLine("    ' SELECT ' + @TopClausule + '");
            SQL.AppendLine("      " + DbSchema + ".Gv_RenglonNotaES_B1.CodigoArticulo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_ArticuloInventario_B1.Descripcion AS DescripcionArticulo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_RenglonNotaES_B1.Cantidad,");
            SQL.AppendLine("      " + DbSchema + ".Gv_RenglonNotaES_B1.LoteDeInventario,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_RenglonNotaES_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_RenglonNotaES_B1.NumeroDocumento,");
            SQL.AppendLine("      " + DbSchema + ".Gv_RenglonNotaES_B1.ConsecutivoRenglon");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_RenglonNotaES_B1");
            SQL.AppendLine("      INNER JOIN Saw.Gv_ArticuloInventario_B1 ON  " + DbSchema + ".Gv_RenglonNotaES_B1.CodigoArticulo = Saw.Gv_ArticuloInventario_B1.Descripcion");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_RenglonNotaES_B1.ConsecutivoCompania = Saw.Gv_ArticuloInventario_B1.ConsecutivoCompania");
            SQL.AppendLine("      INNER JOIN Saw.Gv_LoteDeInventario_B1 ON  " + DbSchema + ".Gv_RenglonNotaES_B1.LoteDeInventario = Saw.Gv_LoteDeInventario_B1.CodigoLote");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_RenglonNotaES_B1.ConsecutivoCompania = Saw.Gv_LoteDeInventario_B1.ConsecutivoCompania");
            SQL.AppendLine("'   IF (NOT @SQLWhere IS NULL) AND (@SQLWhere <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' WHERE ' + @SQLWhere");
            SQL.AppendLine("   IF (NOT @SQLOrderBy IS NULL) AND (@SQLOrderBy <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' ORDER BY ' + @SQLOrderBy");
            SQL.AppendLine("   EXEC(@strSQL)");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            //bool vResult = insDbo.Create(DbSchema + ".RenglonNotaES", SqlCreateTable(), false, eDboType.Tabla);
            return true;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoArticuloInv", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoArticuloInv), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_RenglonNotaES_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonNotaESINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonNotaESUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonNotaESDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonNotaESGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonNotaESSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), false) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonNotaESDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonNotaESInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".RenglonNotaES", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonNotaESINS");
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonNotaESUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonNotaESDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonNotaESGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonNotaESInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonNotaESDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonNotaESSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_RenglonNotaES_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoArticuloInv") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsRenglonNotaESED

} //End of namespace Galac.Saw.Dal.Inventario

