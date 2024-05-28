using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.GestionProduccion;

namespace Galac.Adm.Dal.GestionProduccion {
    [LibMefDalComponentMetadata(typeof(clsOrdenDeProduccionDetalleArticuloED))]
    public class clsOrdenDeProduccionDetalleArticuloED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsOrdenDeProduccionDetalleArticuloED(): base(){
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
            get { return "OrdenDeProduccionDetalleArticulo"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("OrdenDeProduccionDetalleArticulo", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeProDetArtConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoOrdenDeProduccion" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeProDetArtConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeProDetArtConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeProDetArtConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoArticulo" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT nnOrdDeProDetArtCodigoArti NOT NULL, ");
            SQL.AppendLine("CantidadOriginalLista" + InsSql.DecimalTypeForDb(25, 8) + " CONSTRAINT d_OrdDeProDetArtCaOrLi DEFAULT (0), ");
            SQL.AppendLine("CantidadSolicitada" + InsSql.DecimalTypeForDb(25, 8) + " CONSTRAINT d_OrdDeProDetArtCaSo DEFAULT (0), ");
            SQL.AppendLine("CantidadProducida" + InsSql.DecimalTypeForDb(25, 8) + " CONSTRAINT d_OrdDeProDetArtCaPr DEFAULT (0), ");
            SQL.AppendLine("CostoUnitario" + InsSql.DecimalTypeForDb(25, 8) + " CONSTRAINT d_OrdDeProDetArtCoUn DEFAULT (0), ");
            SQL.AppendLine("MontoSubTotal" + InsSql.DecimalTypeForDb(25, 8) + " CONSTRAINT d_OrdDeProDetArtMoSuTo DEFAULT (0), ");
            SQL.AppendLine("AjustadoPostCierre" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnOrdDeProDetArtAjustadoPo NOT NULL, ");
            SQL.AppendLine("CantidadAjustada" + InsSql.DecimalTypeForDb(25, 8) + " CONSTRAINT d_OrdDeProDetArtCaAj DEFAULT (0), ");
            SQL.AppendLine("PorcentajeCostoEstimado" + InsSql.DecimalTypeForDb(25, 8) + " CONSTRAINT d_OrdDeProDetArtPoCoEs DEFAULT (0), ");
            SQL.AppendLine("PorcentajeCostoCierre" + InsSql.DecimalTypeForDb(25, 8) + " CONSTRAINT d_OrdDeProDetArtPoCoCi DEFAULT (0), ");
            SQL.AppendLine("Costo" + InsSql.DecimalTypeForDb(25, 8) + " CONSTRAINT d_OrdDeProDetArtCo DEFAULT (0), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_OrdenDeProduccionDetalleArticulo PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, ConsecutivoOrdenDeProduccion ASC, Consecutivo ASC)");
            SQL.AppendLine(",CONSTRAINT fk_OrdenDeProduccionDetalleArticuloOrdenDeProduccion FOREIGN KEY (ConsecutivoCompania, ConsecutivoOrdenDeProduccion)");
            SQL.AppendLine("REFERENCES Adm.OrdenDeProduccion(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT OrdenDeProduccionDetalleArticulo.ConsecutivoCompania, OrdenDeProduccionDetalleArticulo.ConsecutivoOrdenDeProduccion, OrdenDeProduccionDetalleArticulo.Consecutivo, OrdenDeProduccionDetalleArticulo.ConsecutivoAlmacen");
            SQL.AppendLine(", OrdenDeProduccionDetalleArticulo.CodigoArticulo, OrdenDeProduccionDetalleArticulo.CantidadOriginalLista, OrdenDeProduccionDetalleArticulo.CantidadSolicitada, OrdenDeProduccionDetalleArticulo.CantidadProducida");
            SQL.AppendLine(", OrdenDeProduccionDetalleArticulo.CostoUnitario, OrdenDeProduccionDetalleArticulo.MontoSubTotal, OrdenDeProduccionDetalleArticulo.AjustadoPostCierre, OrdenDeProduccionDetalleArticulo.CantidadAjustada");
            SQL.AppendLine(", OrdenDeProduccionDetalleArticulo.PorcentajeCostoEstimado, OrdenDeProduccionDetalleArticulo.PorcentajeCostoCierre, OrdenDeProduccionDetalleArticulo.Costo");
            SQL.AppendLine(", ArticuloInventario.Descripcion AS DescripcionArticulo");
            SQL.AppendLine(", ArticuloInventario.UnidadDeVenta AS UnidadDeVenta");
            SQL.AppendLine(", Adm.OrdenDeProduccion.StatusOp AS StatusOp");
            SQL.AppendLine(", OrdenDeProduccionDetalleArticulo.fldTimeStamp, CAST(OrdenDeProduccionDetalleArticulo.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".OrdenDeProduccionDetalleArticulo");
            SQL.AppendLine("INNER JOIN Adm.OrdenDeProduccion ON  " + DbSchema + ".OrdenDeProduccionDetalleArticulo.ConsecutivoOrdenDeProduccion = Adm.OrdenDeProduccion.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = Adm.OrdenDeProduccion.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN Saw.Almacen ON  " + DbSchema + ".OrdenDeProduccionDetalleArticulo.ConsecutivoAlmacen = Saw.Almacen.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = Saw.Almacen.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN ArticuloInventario ON  " + DbSchema + ".OrdenDeProduccionDetalleArticulo.CodigoArticulo = ArticuloInventario.Codigo");
            SQL.AppendLine("      AND " + DbSchema + ".OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoOrdenDeProduccion" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@CantidadOriginalLista" + InsSql.DecimalTypeForDb(25, 8) + " = 0,");
            SQL.AppendLine("@CantidadSolicitada" + InsSql.DecimalTypeForDb(25, 8) + " = 0,");
            SQL.AppendLine("@CantidadProducida" + InsSql.DecimalTypeForDb(25, 8) + " = 0,");
            SQL.AppendLine("@CostoUnitario" + InsSql.DecimalTypeForDb(25, 8) + " = 0,");
            SQL.AppendLine("@MontoSubTotal" + InsSql.DecimalTypeForDb(25, 8) + " = 0,");
            SQL.AppendLine("@AjustadoPostCierre" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@CantidadAjustada" + InsSql.DecimalTypeForDb(25, 8) + " = 0,");
            SQL.AppendLine("@PorcentajeCostoEstimado" + InsSql.DecimalTypeForDb(25, 8) + " = 0,");
            SQL.AppendLine("@PorcentajeCostoCierre" + InsSql.DecimalTypeForDb(25, 8) + " = 0,");
            SQL.AppendLine("@Costo" + InsSql.DecimalTypeForDb(25, 8) + " = 0");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".OrdenDeProduccionDetalleArticulo(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            ConsecutivoOrdenDeProduccion,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            ConsecutivoAlmacen,");
            SQL.AppendLine("            CodigoArticulo,");
            SQL.AppendLine("            CantidadOriginalLista,");
            SQL.AppendLine("            CantidadSolicitada,");
            SQL.AppendLine("            CantidadProducida,");
            SQL.AppendLine("            CostoUnitario,");
            SQL.AppendLine("            MontoSubTotal,");
            SQL.AppendLine("            AjustadoPostCierre,");
            SQL.AppendLine("            CantidadAjustada,");
            SQL.AppendLine("            PorcentajeCostoEstimado,");
            SQL.AppendLine("            PorcentajeCostoCierre,");
            SQL.AppendLine("            Costo)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @ConsecutivoOrdenDeProduccion,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @ConsecutivoAlmacen,");
            SQL.AppendLine("            @CodigoArticulo,");
            SQL.AppendLine("            @CantidadOriginalLista,");
            SQL.AppendLine("            @CantidadSolicitada,");
            SQL.AppendLine("            @CantidadProducida,");
            SQL.AppendLine("            @CostoUnitario,");
            SQL.AppendLine("            @MontoSubTotal,");
            SQL.AppendLine("            @AjustadoPostCierre,");
            SQL.AppendLine("            @CantidadAjustada,");
            SQL.AppendLine("            @PorcentajeCostoEstimado,");
            SQL.AppendLine("            @PorcentajeCostoCierre,");
            SQL.AppendLine("            @Costo)");
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
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@CantidadOriginalLista" + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("@CantidadSolicitada" + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("@CantidadProducida" + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("@CostoUnitario" + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("@MontoSubTotal" + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("@AjustadoPostCierre" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@CantidadAjustada" + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("@PorcentajeCostoEstimado" + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("@PorcentajeCostoCierre" + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("@Costo" + InsSql.DecimalTypeForDb(25, 8) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".OrdenDeProduccionDetalleArticulo WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoOrdenDeProduccion = @ConsecutivoOrdenDeProduccion AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".OrdenDeProduccionDetalleArticulo WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoOrdenDeProduccion = @ConsecutivoOrdenDeProduccion AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloCanBeUpdated @ConsecutivoCompania,@ConsecutivoOrdenDeProduccion,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".OrdenDeProduccionDetalleArticulo");
            SQL.AppendLine("            SET ConsecutivoAlmacen = @ConsecutivoAlmacen,");
            SQL.AppendLine("               CodigoArticulo = @CodigoArticulo,");
            SQL.AppendLine("               CantidadOriginalLista = @CantidadOriginalLista,");
            SQL.AppendLine("               CantidadSolicitada = @CantidadSolicitada,");
            SQL.AppendLine("               CantidadProducida = @CantidadProducida,");
            SQL.AppendLine("               CostoUnitario = @CostoUnitario,");
            SQL.AppendLine("               MontoSubTotal = @MontoSubTotal,");
            SQL.AppendLine("               AjustadoPostCierre = @AjustadoPostCierre,");
            SQL.AppendLine("               CantidadAjustada = @CantidadAjustada,");
            SQL.AppendLine("               PorcentajeCostoEstimado = @PorcentajeCostoEstimado,");
            SQL.AppendLine("               PorcentajeCostoCierre = @PorcentajeCostoCierre,");
            SQL.AppendLine("               Costo = @Costo");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".OrdenDeProduccionDetalleArticulo WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoOrdenDeProduccion = @ConsecutivoOrdenDeProduccion AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".OrdenDeProduccionDetalleArticulo WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoOrdenDeProduccion = @ConsecutivoOrdenDeProduccion AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloCanBeDeleted @ConsecutivoCompania,@ConsecutivoOrdenDeProduccion,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".OrdenDeProduccionDetalleArticulo");
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
            SQL.AppendLine("         CodigoArticulo,");
            SQL.AppendLine("         CantidadOriginalLista,");
            SQL.AppendLine("         CantidadSolicitada,");
            SQL.AppendLine("         CantidadProducida,");
            SQL.AppendLine("         CostoUnitario,");
            SQL.AppendLine("         MontoSubTotal,");
            SQL.AppendLine("         AjustadoPostCierre,");
            SQL.AppendLine("         CantidadAjustada,");
            SQL.AppendLine("         PorcentajeCostoEstimado,");
            SQL.AppendLine("         PorcentajeCostoCierre,");
            SQL.AppendLine("         Costo,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".OrdenDeProduccionDetalleArticulo");
            SQL.AppendLine("      WHERE OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND OrdenDeProduccionDetalleArticulo.ConsecutivoOrdenDeProduccion = @ConsecutivoOrdenDeProduccion");
            SQL.AppendLine("         AND OrdenDeProduccionDetalleArticulo.Consecutivo = @Consecutivo");
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
            SQL.AppendLine("        CodigoArticulo,");
            SQL.AppendLine("        CantidadOriginalLista,");
            SQL.AppendLine("        CantidadSolicitada,");
            SQL.AppendLine("        CantidadProducida,");
            SQL.AppendLine("        CostoUnitario,");
            SQL.AppendLine("        MontoSubTotal,");
            SQL.AppendLine("        AjustadoPostCierre,");
            SQL.AppendLine("        CantidadAjustada,");
            SQL.AppendLine("        PorcentajeCostoEstimado,");
            SQL.AppendLine("        PorcentajeCostoCierre,");
            SQL.AppendLine("        Costo,");
            SQL.AppendLine("        fldTimeStamp");
            SQL.AppendLine("    FROM OrdenDeProduccionDetalleArticulo");
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
            SQL.AppendLine("	DELETE FROM OrdenDeProduccionDetalleArticulo");
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
            SQL.AppendLine("	    EXEC Adm.Gp_OrdenDeProduccionDetalleArticuloDelDet @ConsecutivoCompania = @ConsecutivoCompania, @ConsecutivoOrdenDeProduccion = @ConsecutivoOrdenDeProduccion");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO Adm.OrdenDeProduccionDetalleArticulo(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        ConsecutivoOrdenDeProduccion,");
			SQL.AppendLine("	        Consecutivo,");
			SQL.AppendLine("	        ConsecutivoAlmacen,");
			SQL.AppendLine("	        CodigoArticulo,");
			SQL.AppendLine("	        CantidadOriginalLista,");
			SQL.AppendLine("	        CantidadSolicitada,");
			SQL.AppendLine("	        CantidadProducida,");
			SQL.AppendLine("	        CostoUnitario,");
			SQL.AppendLine("	        MontoSubTotal,");
			SQL.AppendLine("	        AjustadoPostCierre,");
			SQL.AppendLine("	        CantidadAjustada,");
			SQL.AppendLine("	        PorcentajeCostoEstimado,");
			SQL.AppendLine("	        PorcentajeCostoCierre,");
			SQL.AppendLine("	        Costo)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @ConsecutivoOrdenDeProduccion,");
			SQL.AppendLine("	        Consecutivo,");
			SQL.AppendLine("	        ConsecutivoAlmacen,");
			SQL.AppendLine("	        CodigoArticulo,");
			SQL.AppendLine("	        CantidadOriginalLista,");
			SQL.AppendLine("	        CantidadSolicitada,");
			SQL.AppendLine("	        CantidadProducida,");
			SQL.AppendLine("	        CostoUnitario,");
			SQL.AppendLine("	        MontoSubTotal,");
			SQL.AppendLine("	        AjustadoPostCierre,");
			SQL.AppendLine("	        CantidadAjustada,");
			SQL.AppendLine("	        PorcentajeCostoEstimado,");
			SQL.AppendLine("	        PorcentajeCostoCierre,");
			SQL.AppendLine("	        Costo");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataOrdenDeProduccionDetalleArticulo/GpDetailOrdenDeProduccionDetalleArticulo',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        Consecutivo " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        ConsecutivoAlmacen " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        CodigoArticulo " + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("	        CantidadOriginalLista " + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("	        CantidadSolicitada " + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("	        CantidadProducida " + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("	        CostoUnitario " + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("	        MontoSubTotal " + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("	        AjustadoPostCierre " + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("	        CantidadAjustada " + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("	        PorcentajeCostoEstimado " + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("	        PorcentajeCostoCierre " + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("	        Costo " + InsSql.DecimalTypeForDb(25, 8) + ") AS XmlDocDetailOfOrdenDeProduccion");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeProduccionDetalleArticulo_B1.CodigoArticulo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeProduccionDetalleArticulo_B1.DescripcionArticulo,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeProduccionDetalleArticulo_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeProduccionDetalleArticulo_B1.ConsecutivoOrdenDeProduccion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeProduccionDetalleArticulo_B1.Consecutivo");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_OrdenDeProduccionDetalleArticulo_B1");
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
            bool vResult = insDbo.Create(DbSchema + ".OrdenDeProduccionDetalleArticulo", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_OrdenDeProduccionDetalleArticulo_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".OrdenDeProduccionDetalleArticulo", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloINS");
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloSCH") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionDetalleArticuloSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_OrdenDeProduccionDetalleArticulo_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsOrdenDeProduccionDetalleArticuloED

} //End of namespace Galac.Adm.Dal.GestionProduccion

