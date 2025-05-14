using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;

namespace Galac.Saw.Dal.Inventario {
    [LibMefDalComponentMetadata(typeof(clsExistenciaPorAlmacenDetLoteInvED))]
    public class clsExistenciaPorAlmacenDetLoteInvED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsExistenciaPorAlmacenDetLoteInvED(): base(){
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
            get { return "ExistenciaPorAlmacenDetLoteInv"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("ExistenciaPorAlmacenDetLoteInv", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnExiPorAlmDetLotInvConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnExiPorAlmDetLotInvConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoArticulo" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT nnExiPorAlmDetLotInvCodigoArti NOT NULL, ");
            SQL.AppendLine("ConsecutivoLoteInventario" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnExiPorAlmDetLotInvConsecutiv NOT NULL, ");
            SQL.AppendLine("Cantidad" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnExiPorAlmDetLotInvCantidad NOT NULL, ");
            SQL.AppendLine("Ubicacion" + InsSql.VarCharTypeForDb(100) + " CONSTRAINT d_ExiPorAlmDetLotInvUb DEFAULT (''), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_ExistenciaPorAlmacenDetLoteInv PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, ConsecutivoAlmacen ASC, CodigoArticulo ASC, ConsecutivoLoteInventario ASC)");
            SQL.AppendLine(",CONSTRAINT fk_ExistenciaPorAlmacenDetLoteInvExistenciaPorAlmacen FOREIGN KEY (ConsecutivoCompania, ConsecutivoAlmacen, CodigoArticulo)");
            SQL.AppendLine("REFERENCES dbo.ExistenciaPorAlmacen(ConsecutivoCompania, ConsecutivoAlmacen, CodigoArticulo)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_ExistenciaPorAlmacenDetLoteInvLoteDeInventario FOREIGN KEY (ConsecutivoCompania, ConsecutivoLoteInventario)");
            SQL.AppendLine("REFERENCES Saw.LoteDeInventario(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT ExistenciaPorAlmacenDetLoteInv.ConsecutivoCompania, ExistenciaPorAlmacenDetLoteInv.ConsecutivoAlmacen, ExistenciaPorAlmacenDetLoteInv.CodigoArticulo, ExistenciaPorAlmacenDetLoteInv.ConsecutivoLoteInventario");
            SQL.AppendLine(", ExistenciaPorAlmacenDetLoteInv.Cantidad, ExistenciaPorAlmacenDetLoteInv.Ubicacion");
            SQL.AppendLine(", ExistenciaPorAlmacenDetLoteInv.fldTimeStamp, CAST(ExistenciaPorAlmacenDetLoteInv.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".ExistenciaPorAlmacenDetLoteInv");
            SQL.AppendLine("INNER JOIN Saw.LoteDeInventario ON  " + DbSchema + ".ExistenciaPorAlmacenDetLoteInv.ConsecutivoLoteInventario = Saw.LoteDeInventario.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".ExistenciaPorAlmacenDetLoteInv.ConsecutivoCompania = Saw.LoteDeInventario.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlViewExistenciaB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT Saw.LoteDeInventario.ConsecutivoCompania, Saw.LoteDeInventario.Consecutivo, Saw.LoteDeInventario.CodigoLote, Saw.LoteDeInventario.CodigoArticulo, dbo.ArticuloInventario.Descripcion As DescripcionArticulo");
            SQL.AppendLine(", Saw.LoteDeInventario.FechaDeElaboracion, Saw.LoteDeInventario.FechaDeVencimiento, dbo.ExistenciaPorAlmacenDetLoteInv.Cantidad AS Existencia, Saw.LoteDeInventario.StatusLoteInv");
            SQL.AppendLine(", Saw.Almacen.Consecutivo As ConsecutivoAlmacen");
            SQL.AppendLine(", Saw.Almacen.Codigo AS CodigoAlmacen");
            SQL.AppendLine(", Saw.Almacen.NombreAlmacen");
            SQL.AppendLine("FROM Saw.LoteDeInventario");
            SQL.AppendLine("INNER JOIN dbo.ArticuloInventario");
            SQL.AppendLine("ON Saw.LoteDeInventario.CodigoArticulo = ArticuloInventario.Codigo");
            SQL.AppendLine("AND Saw.LoteDeInventario.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN dbo.ExistenciaPorAlmacenDetLoteInv");
            SQL.AppendLine("ON Saw.LoteDeInventario.ConsecutivoCompania = dbo.ExistenciaPorAlmacenDetLoteInv.ConsecutivoCompania");
            SQL.AppendLine("AND Saw.LoteDeInventario.CodigoArticulo = dbo.ExistenciaPorAlmacenDetLoteInv.CodigoArticulo");
            SQL.AppendLine("AND Saw.LoteDeInventario.Consecutivo = dbo.ExistenciaPorAlmacenDetLoteInv.ConsecutivoLoteInventario");
            SQL.AppendLine("INNER JOIN Saw.Almacen");
            SQL.AppendLine("ON dbo.ExistenciaPorAlmacenDetLoteInv.ConsecutivoCompania = Saw.Almacen.ConsecutivoCompania");
            SQL.AppendLine("AND dbo.ExistenciaPorAlmacenDetLoteInv.ConsecutivoAlmacen = Saw.Almacen.Consecutivo");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@ConsecutivoLoteInventario" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@Ubicacion" + InsSql.VarCharTypeForDb(100) + " = ''");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".ExistenciaPorAlmacenDetLoteInv(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            ConsecutivoAlmacen,");
            SQL.AppendLine("            CodigoArticulo,");
            SQL.AppendLine("            ConsecutivoLoteInventario,");
            SQL.AppendLine("            Cantidad,");
            SQL.AppendLine("            Ubicacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @ConsecutivoAlmacen,");
            SQL.AppendLine("            @CodigoArticulo,");
            SQL.AppendLine("            @ConsecutivoLoteInventario,");
            SQL.AppendLine("            @Cantidad,");
            SQL.AppendLine("            @Ubicacion)");
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
            SQL.AppendLine("@ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@ConsecutivoLoteInventario" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@Ubicacion" + InsSql.VarCharTypeForDb(100) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".ExistenciaPorAlmacenDetLoteInv WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoAlmacen = @ConsecutivoAlmacen AND CodigoArticulo = @CodigoArticulo AND ConsecutivoLoteInventario = @ConsecutivoLoteInventario)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".ExistenciaPorAlmacenDetLoteInv WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoAlmacen = @ConsecutivoAlmacen AND CodigoArticulo = @CodigoArticulo AND ConsecutivoLoteInventario = @ConsecutivoLoteInventario");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_ExistenciaPorAlmacenDetLoteInvCanBeUpdated @ConsecutivoCompania,@ConsecutivoAlmacen,@CodigoArticulo,@ConsecutivoLoteInventario, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".ExistenciaPorAlmacenDetLoteInv");
            SQL.AppendLine("            SET Cantidad = @Cantidad,");
            SQL.AppendLine("               Ubicacion = @Ubicacion");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoAlmacen = @ConsecutivoAlmacen");
            SQL.AppendLine("               AND CodigoArticulo = @CodigoArticulo");
            SQL.AppendLine("               AND ConsecutivoLoteInventario = @ConsecutivoLoteInventario");
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
            SQL.AppendLine("@ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@ConsecutivoLoteInventario" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".ExistenciaPorAlmacenDetLoteInv WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoAlmacen = @ConsecutivoAlmacen AND CodigoArticulo = @CodigoArticulo AND ConsecutivoLoteInventario = @ConsecutivoLoteInventario)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".ExistenciaPorAlmacenDetLoteInv WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoAlmacen = @ConsecutivoAlmacen AND CodigoArticulo = @CodigoArticulo AND ConsecutivoLoteInventario = @ConsecutivoLoteInventario");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_ExistenciaPorAlmacenDetLoteInvCanBeDeleted @ConsecutivoCompania,@ConsecutivoAlmacen,@CodigoArticulo,@ConsecutivoLoteInventario, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".ExistenciaPorAlmacenDetLoteInv");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoAlmacen = @ConsecutivoAlmacen");
            SQL.AppendLine("               AND CodigoArticulo = @CodigoArticulo");
            SQL.AppendLine("               AND ConsecutivoLoteInventario = @ConsecutivoLoteInventario");
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
            SQL.AppendLine("@ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@ConsecutivoLoteInventario" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         ConsecutivoAlmacen,");
            SQL.AppendLine("         CodigoArticulo,");
            SQL.AppendLine("         ConsecutivoLoteInventario,");
            SQL.AppendLine("         Cantidad,");
            SQL.AppendLine("         Ubicacion,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".ExistenciaPorAlmacenDetLoteInv");
            SQL.AppendLine("      WHERE ExistenciaPorAlmacenDetLoteInv.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND ExistenciaPorAlmacenDetLoteInv.ConsecutivoAlmacen = @ConsecutivoAlmacen");
            SQL.AppendLine("         AND ExistenciaPorAlmacenDetLoteInv.CodigoArticulo = @CodigoArticulo");
            SQL.AppendLine("         AND ExistenciaPorAlmacenDetLoteInv.ConsecutivoLoteInventario = @ConsecutivoLoteInventario");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30));
            return SQL.ToString();
        }

        private string SqlSpSelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SELECT ");
            SQL.AppendLine("        ConsecutivoCompania,");
            SQL.AppendLine("        ConsecutivoAlmacen,");
            SQL.AppendLine("        CodigoArticulo,");
            SQL.AppendLine("        ConsecutivoLoteInventario,");
            SQL.AppendLine("        Cantidad,");
            SQL.AppendLine("        Ubicacion,");
            SQL.AppendLine("        fldTimeStamp");
            SQL.AppendLine("    FROM ExistenciaPorAlmacenDetLoteInv");
            SQL.AppendLine(" 	WHERE CodigoArticulo = @CodigoArticulo");
            SQL.AppendLine(" 	AND ConsecutivoAlmacen = @ConsecutivoAlmacen");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30));
            return SQL.ToString();
        }

        private string SqlSpDelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	DELETE FROM ExistenciaPorAlmacenDetLoteInv");
            SQL.AppendLine(" 	WHERE CodigoArticulo = @CodigoArticulo");
            SQL.AppendLine(" 	AND ConsecutivoAlmacen = @ConsecutivoAlmacen");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInsDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
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
            SQL.AppendLine("	    EXEC dbo.Gp_ExistenciaPorAlmacenDetLoteInvDelDet @ConsecutivoCompania = @ConsecutivoCompania, @ConsecutivoAlmacen = @ConsecutivoAlmacen, @CodigoArticulo = @CodigoArticulo");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO dbo.ExistenciaPorAlmacenDetLoteInv(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        ConsecutivoAlmacen,");
			SQL.AppendLine("	        CodigoArticulo,");
			SQL.AppendLine("	        ConsecutivoLoteInventario,");
			SQL.AppendLine("	        Cantidad,");
			SQL.AppendLine("	        Ubicacion)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @ConsecutivoAlmacen,");
			SQL.AppendLine("	        CodigoArticulo,");
			SQL.AppendLine("	        ConsecutivoLoteInventario,");
			SQL.AppendLine("	        Cantidad,");
			SQL.AppendLine("	        Ubicacion");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataExistenciaPorAlmacenDetLoteInv/GpDetailExistenciaPorAlmacenDetLoteInv',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        CodigoArticulo " + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("	        ConsecutivoLoteInventario " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        Cantidad " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        Ubicacion " + InsSql.VarCharTypeForDb(100) + ") AS XmlDocDetailOfExistenciaPorAlmacen");
            SQL.AppendLine("	    EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("	    SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("	    RETURN @ReturnValue");
	        SQL.AppendLine("	END");
	        SQL.AppendLine("	ELSE");
            SQL.AppendLine("	    RETURN -1");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSearchExistenciaParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@SQLWhere" + InsSql.VarCharTypeForDb(2000) + " = null,");
            SQL.AppendLine("@SQLOrderBy" + InsSql.VarCharTypeForDb(500) + " = null,");
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + " = null,");
            SQL.AppendLine("@UseTopClausule" + InsSql.VarCharTypeForDb(1) + " = 'S'");
            return SQL.ToString();
        }
        private string SqlSpSearchExistencia() {
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
            SQL.AppendLine("      Saw.Gv_ExistenciaLoteDeInventario_B1.CodigoLote,");
            SQL.AppendLine("      Saw.Gv_ExistenciaLoteDeInventario_B1.CodigoArticulo,");
            SQL.AppendLine("      Saw.Gv_ExistenciaLoteDeInventario_B1.DescripcionArticulo,");
            SQL.AppendLine("      Saw.Gv_ExistenciaLoteDeInventario_B1.FechaDeElaboracion,");
            SQL.AppendLine("      Saw.Gv_ExistenciaLoteDeInventario_B1.FechaDeVencimiento,");
            SQL.AppendLine("      Saw.Gv_ExistenciaLoteDeInventario_B1.Existencia,");
            SQL.AppendLine("      Saw.Gv_ExistenciaLoteDeInventario_B1.StatusLoteInvStr,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      Saw.Gv_ExistenciaLoteDeInventario_B1.ConsecutivoCompania,");
            SQL.AppendLine("      Saw.Gv_ExistenciaLoteDeInventario_B1.Consecutivo,");
            SQL.AppendLine("      Saw.Gv_ExistenciaLoteDeInventario_B1.StatusLoteInv,");
            SQL.AppendLine("      Saw.Gv_ExistenciaLoteDeInventario_B1.ConsecutivoAlmacen,");
            SQL.AppendLine("      Saw.Gv_ExistenciaLoteDeInventario_B1.CodigoAlmacen,");
            SQL.AppendLine("      Saw.Gv_ExistenciaLoteDeInventario_B1.NombreAlmacen");
            SQL.AppendLine("      FROM Saw.Gv_ExistenciaLoteDeInventario_B1");
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
            bool vResult = insDbo.Create(DbSchema + ".ExistenciaPorAlmacenDetLoteInv", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_ExistenciaPorAlmacenDetLoteInv_B1", SqlViewB1(), true);
            vResult = insVistas.Create("Saw.Gv_ExistenciaLoteDeInventario_B1", SqlViewExistenciaB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ExistenciaPorAlmacenDetLoteInvINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ExistenciaPorAlmacenDetLoteInvUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ExistenciaPorAlmacenDetLoteInvDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ExistenciaPorAlmacenDetLoteInvGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ExistenciaPorAlmacenDetLoteInvSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ExistenciaPorAlmacenDetLoteInvDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ExistenciaPorAlmacenDetLoteInvInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure("Saw.Gp_LoteDeInventarioSCHExistencia", SqlSpSearchExistenciaParameters(), SqlSpSearchExistencia(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".ExistenciaPorAlmacenDetLoteInv", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_ExistenciaPorAlmacenDetLoteInvINS");
            vResult = insSp.Drop(DbSchema + ".Gp_ExistenciaPorAlmacenDetLoteInvUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ExistenciaPorAlmacenDetLoteInvDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ExistenciaPorAlmacenDetLoteInvGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ExistenciaPorAlmacenDetLoteInvInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ExistenciaPorAlmacenDetLoteInvDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ExistenciaPorAlmacenDetLoteInvSelDet") && vResult;
            vResult = insSp.Drop("Saw.Gp_LoteDeInventarioSCHExistencia") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_ExistenciaPorAlmacenDetLoteInv_B1") && vResult;
            vResult = insVista.Drop("Saw.Gv_ExistenciaLoteDeInventario_B1") && vResult;

            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsExistenciaPorAlmacenDetLoteInvED

} //End of namespace Galac.Saw.Dal.Inventario

