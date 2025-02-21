using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.GestionProduccion;

namespace Galac.Adm.Dal.GestionProduccion {
    [LibMefDalComponentMetadata(typeof(clsOrdenDeProduccionDetalleMaterialesED))]
    public class clsOrdenDeProduccionDetalleMaterialesED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsOrdenDeProduccionDetalleMaterialesED(): base(){
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
            get { return "OrdenDeProduccionDetalleMateriales"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("OrdenDeProduccionDetalleMateriales", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeProDetMatConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoOrdenDeProduccion" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeProDetMatConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeProDetMatConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT d_OrdDeProDetMatCoAl DEFAULT (0), ");
            SQL.AppendLine("ConsecutivoLoteDeInventario" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT d_OrdDeProDetMatCoLoDeIn DEFAULT (0), ");
            SQL.AppendLine("CodigoArticulo" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT nnOrdDeProDetMatCodigoArti NOT NULL, ");
            SQL.AppendLine("Cantidad" + InsSql.DecimalTypeForDb(25, 8) + " CONSTRAINT nnOrdDeProDetMatCantidad NOT NULL, ");
            SQL.AppendLine("CantidadReservadaInventario" + InsSql.DecimalTypeForDb(25, 8) + " CONSTRAINT d_OrdDeProDetMatCaReIn DEFAULT (0), ");
            SQL.AppendLine("CantidadConsumida" + InsSql.DecimalTypeForDb(25, 8) + " CONSTRAINT d_OrdDeProDetMatCaCo DEFAULT (0), ");
            SQL.AppendLine("CostoUnitarioArticuloInventario" + InsSql.DecimalTypeForDb(25, 8) + " CONSTRAINT d_OrdDeProDetMatCoUnArIn DEFAULT (0), ");
            SQL.AppendLine("MontoSubtotal" + InsSql.DecimalTypeForDb(25, 8) + " CONSTRAINT d_OrdDeProDetMatMoSu DEFAULT (0), ");
            SQL.AppendLine("AjustadoPostCierre" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnOrdDeProDetMatAjustadoPo NOT NULL, ");
            SQL.AppendLine("CantidadAjustada" + InsSql.DecimalTypeForDb(25, 8) + " CONSTRAINT d_OrdDeProDetMatCaAj DEFAULT (0), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_OrdenDeProduccionDetalleMateriales PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, ConsecutivoOrdenDeProduccion ASC, Consecutivo ASC)");
            SQL.AppendLine(",CONSTRAINT fk_OrdenDeProduccionDetalleMaterialesOrdenDeProduccion FOREIGN KEY (ConsecutivoCompania, ConsecutivoOrdenDeProduccion)");
            SQL.AppendLine("REFERENCES Adm.OrdenDeProduccion(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT OrdenDeProduccionDetalleMateriales.ConsecutivoCompania, OrdenDeProduccionDetalleMateriales.ConsecutivoOrdenDeProduccion, OrdenDeProduccionDetalleMateriales.Consecutivo, OrdenDeProduccionDetalleMateriales.ConsecutivoAlmacen");
            SQL.AppendLine(", OrdenDeProduccionDetalleMateriales.CodigoArticulo, OrdenDeProduccionDetalleMateriales.Cantidad, OrdenDeProduccionDetalleMateriales.CantidadReservadaInventario, OrdenDeProduccionDetalleMateriales.CantidadConsumida");
            SQL.AppendLine(", OrdenDeProduccionDetalleMateriales.CostoUnitarioArticuloInventario, OrdenDeProduccionDetalleMateriales.MontoSubtotal, OrdenDeProduccionDetalleMateriales.AjustadoPostCierre, OrdenDeProduccionDetalleMateriales.CantidadAjustada");
			SQL.AppendLine(", OrdenDeProduccionDetalleMateriales.ConsecutivoLoteDeInventario");
            SQL.AppendLine(", dbo.ArticuloInventario.Descripcion AS DescripcionArticulo");
            SQL.AppendLine(", dbo.ArticuloInventario.UnidadDeVenta AS UnidadDeVenta");
            SQL.AppendLine(", OrdenDeProduccionDetalleMateriales.fldTimeStamp, CAST(OrdenDeProduccionDetalleMateriales.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".OrdenDeProduccionDetalleMateriales");
            SQL.AppendLine("INNER JOIN Saw.Almacen ON  " + DbSchema + ".OrdenDeProduccionDetalleMateriales.ConsecutivoAlmacen = Saw.Almacen.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".OrdenDeProduccionDetalleMateriales.ConsecutivoCompania = Saw.Almacen.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN dbo.ArticuloInventario ON  " + DbSchema + ".OrdenDeProduccionDetalleMateriales.CodigoArticulo = dbo.ArticuloInventario.Codigo");
            SQL.AppendLine("      AND " + DbSchema + ".OrdenDeProduccionDetalleMateriales.ConsecutivoCompania = dbo.ArticuloInventario.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoOrdenDeProduccion" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoLoteDeInventario" + InsSql.NumericTypeForDb(10, 0) + " = 0,");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 8) + " = 0,");
            SQL.AppendLine("@CantidadReservadaInventario" + InsSql.DecimalTypeForDb(25, 8) + " = 0,");
            SQL.AppendLine("@CantidadConsumida" + InsSql.DecimalTypeForDb(25, 8) + " = 0,");
            SQL.AppendLine("@CostoUnitarioArticuloInventario" + InsSql.DecimalTypeForDb(25, 8) + " = 0,");
            SQL.AppendLine("@MontoSubtotal" + InsSql.DecimalTypeForDb(25, 8) + " = 0,");
            SQL.AppendLine("@AjustadoPostCierre" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@CantidadAjustada" + InsSql.DecimalTypeForDb(25, 8) + " = 0");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".OrdenDeProduccionDetalleMateriales(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            ConsecutivoOrdenDeProduccion,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            ConsecutivoAlmacen,");
            SQL.AppendLine("            ConsecutivoLoteDeInventario,");
            SQL.AppendLine("            CodigoArticulo,");
            SQL.AppendLine("            Cantidad,");
            SQL.AppendLine("            CantidadReservadaInventario,");
            SQL.AppendLine("            CantidadConsumida,");
            SQL.AppendLine("            CostoUnitarioArticuloInventario,");
            SQL.AppendLine("            MontoSubtotal,");
            SQL.AppendLine("            AjustadoPostCierre,");
            SQL.AppendLine("            CantidadAjustada)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @ConsecutivoOrdenDeProduccion,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @ConsecutivoAlmacen,");
            SQL.AppendLine("            @ConsecutivoLoteDeInventario,");
            SQL.AppendLine("            @CodigoArticulo,");
            SQL.AppendLine("            @Cantidad,");
            SQL.AppendLine("            @CantidadReservadaInventario,");
            SQL.AppendLine("            @CantidadConsumida,");
            SQL.AppendLine("            @CostoUnitarioArticuloInventario,");
            SQL.AppendLine("            @MontoSubtotal,");
            SQL.AppendLine("            @AjustadoPostCierre,");
            SQL.AppendLine("            @CantidadAjustada)");
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
            SQL.AppendLine("@ConsecutivoOrdenDeProduccion" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoLoteDeInventario" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("@CantidadReservadaInventario" + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("@CantidadConsumida" + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("@CostoUnitarioArticuloInventario" + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("@MontoSubtotal" + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("@AjustadoPostCierre" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@CantidadAjustada" + InsSql.DecimalTypeForDb(25, 8) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".OrdenDeProduccionDetalleMateriales WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoOrdenDeProduccion = @ConsecutivoOrdenDeProduccion AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".OrdenDeProduccionDetalleMateriales WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoOrdenDeProduccion = @ConsecutivoOrdenDeProduccion AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_OrdenDeProduccionDetalleMaterialesCanBeUpdated @ConsecutivoCompania,@ConsecutivoOrdenDeProduccion,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".OrdenDeProduccionDetalleMateriales");
            SQL.AppendLine("            SET ConsecutivoAlmacen = @ConsecutivoAlmacen,");
            SQL.AppendLine("               ConsecutivoLoteDeInventario = @ConsecutivoLoteDeInventario,");
            SQL.AppendLine("               CodigoArticulo = @CodigoArticulo,");
            SQL.AppendLine("               Cantidad = @Cantidad,");
            SQL.AppendLine("               CantidadReservadaInventario = @CantidadReservadaInventario,");
            SQL.AppendLine("               CantidadConsumida = @CantidadConsumida,");
            SQL.AppendLine("               CostoUnitarioArticuloInventario = @CostoUnitarioArticuloInventario,");
            SQL.AppendLine("               MontoSubtotal = @MontoSubtotal,");
            SQL.AppendLine("               AjustadoPostCierre = @AjustadoPostCierre,");
            SQL.AppendLine("               CantidadAjustada = @CantidadAjustada");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoOrdenDeProduccion = @ConsecutivoOrdenDeProduccion");
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
            SQL.AppendLine("@ConsecutivoOrdenDeProduccion" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".OrdenDeProduccionDetalleMateriales WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoOrdenDeProduccion = @ConsecutivoOrdenDeProduccion AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".OrdenDeProduccionDetalleMateriales WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoOrdenDeProduccion = @ConsecutivoOrdenDeProduccion AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_OrdenDeProduccionDetalleMaterialesCanBeDeleted @ConsecutivoCompania,@ConsecutivoOrdenDeProduccion,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".OrdenDeProduccionDetalleMateriales");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoOrdenDeProduccion = @ConsecutivoOrdenDeProduccion");
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
            SQL.AppendLine("@ConsecutivoOrdenDeProduccion" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         ConsecutivoOrdenDeProduccion,");
            SQL.AppendLine("         Consecutivo,");
            SQL.AppendLine("         ConsecutivoAlmacen,");
            SQL.AppendLine("         ConsecutivoLoteDeInventario,");
            SQL.AppendLine("         CodigoArticulo,");
            SQL.AppendLine("         Cantidad,");
            SQL.AppendLine("         CantidadReservadaInventario,");
            SQL.AppendLine("         CantidadConsumida,");
            SQL.AppendLine("         CostoUnitarioArticuloInventario,");
            SQL.AppendLine("         MontoSubtotal,");
            SQL.AppendLine("         AjustadoPostCierre,");
            SQL.AppendLine("         CantidadAjustada,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".OrdenDeProduccionDetalleMateriales");
            SQL.AppendLine("      WHERE OrdenDeProduccionDetalleMateriales.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND OrdenDeProduccionDetalleMateriales.ConsecutivoOrdenDeProduccion = @ConsecutivoOrdenDeProduccion");
            SQL.AppendLine("         AND OrdenDeProduccionDetalleMateriales.Consecutivo = @Consecutivo");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoOrdenDeProduccion" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpSelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SELECT ");
            SQL.AppendLine("        ConsecutivoCompania,");
            SQL.AppendLine("        ConsecutivoOrdenDeProduccion,");
            SQL.AppendLine("        Consecutivo,");
            SQL.AppendLine("        ConsecutivoAlmacen,");
            SQL.AppendLine("        ConsecutivoLoteDeInventario,");
            SQL.AppendLine("        CodigoArticulo,");
            SQL.AppendLine("        Cantidad,");
            SQL.AppendLine("        CantidadReservadaInventario,");
            SQL.AppendLine("        CantidadConsumida,");
            SQL.AppendLine("        CostoUnitarioArticuloInventario,");
            SQL.AppendLine("        MontoSubtotal,");
            SQL.AppendLine("        AjustadoPostCierre,");
            SQL.AppendLine("        CantidadAjustada,");
            SQL.AppendLine("        fldTimeStamp");
            SQL.AppendLine("    FROM OrdenDeProduccionDetalleMateriales");
            SQL.AppendLine(" 	WHERE ConsecutivoOrdenDeProduccion = @ConsecutivoOrdenDeProduccion");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoOrdenDeProduccion" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpDelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	DELETE FROM OrdenDeProduccionDetalleMateriales");
            SQL.AppendLine(" 	WHERE ConsecutivoOrdenDeProduccion = @ConsecutivoOrdenDeProduccion");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInsDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoOrdenDeProduccion" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("	    EXEC Adm.Gp_OrdenDeProduccionDetalleMaterialesDelDet @ConsecutivoCompania = @ConsecutivoCompania, @ConsecutivoOrdenDeProduccion = @ConsecutivoOrdenDeProduccion");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO Adm.OrdenDeProduccionDetalleMateriales(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        ConsecutivoOrdenDeProduccion,");
			SQL.AppendLine("	        Consecutivo,");
			SQL.AppendLine("	        ConsecutivoAlmacen,");
			SQL.AppendLine("	        ConsecutivoLoteDeInventario,");
			SQL.AppendLine("	        CodigoArticulo,");
			SQL.AppendLine("	        Cantidad,");
			SQL.AppendLine("	        CantidadReservadaInventario,");
			SQL.AppendLine("	        CantidadConsumida,");
			SQL.AppendLine("	        CostoUnitarioArticuloInventario,");
			SQL.AppendLine("	        MontoSubtotal,");
			SQL.AppendLine("	        AjustadoPostCierre,");
			SQL.AppendLine("	        CantidadAjustada)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @ConsecutivoOrdenDeProduccion,");
			SQL.AppendLine("	        Consecutivo,");
			SQL.AppendLine("	        ConsecutivoAlmacen,");
			SQL.AppendLine("	        ConsecutivoLoteDeInventario,");
			SQL.AppendLine("	        CodigoArticulo,");
			SQL.AppendLine("	        Cantidad,");
			SQL.AppendLine("	        CantidadReservadaInventario,");
			SQL.AppendLine("	        CantidadConsumida,");
			SQL.AppendLine("	        CostoUnitarioArticuloInventario,");
			SQL.AppendLine("	        MontoSubtotal,");
			SQL.AppendLine("	        AjustadoPostCierre,");
			SQL.AppendLine("	        CantidadAjustada");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataOrdenDeProduccionDetalleMateriales/GpDetailOrdenDeProduccionDetalleMateriales',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        Consecutivo " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        ConsecutivoAlmacen " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        ConsecutivoLoteDeInventario " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        CodigoArticulo " + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("	        Cantidad " + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("	        CantidadReservadaInventario " + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("	        CantidadConsumida " + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("	        CostoUnitarioArticuloInventario " + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("	        MontoSubtotal " + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("	        AjustadoPostCierre " + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("	        CantidadAjustada " + InsSql.DecimalTypeForDb(25, 8) + ") AS XmlDocDetailOfOrdenDeProduccion");
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
            bool vResult = insDbo.Create(DbSchema + ".OrdenDeProduccionDetalleMateriales", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_OrdenDeProduccionDetalleMateriales_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionDetalleMaterialesINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionDetalleMaterialesUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionDetalleMaterialesDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionDetalleMaterialesGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionDetalleMaterialesSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionDetalleMaterialesDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionDetalleMaterialesInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".OrdenDeProduccionDetalleMateriales", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionDetalleMaterialesINS");
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionDetalleMaterialesUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionDetalleMaterialesDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionDetalleMaterialesGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionDetalleMaterialesInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionDetalleMaterialesDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionDetalleMaterialesSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_OrdenDeProduccionDetalleMateriales_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsOrdenDeProduccionDetalleMaterialesED

} //End of namespace Galac.Adm.Dal.GestionProduccion

