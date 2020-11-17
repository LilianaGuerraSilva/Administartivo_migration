using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Dal.GestionCompras {
    [LibMefDalComponentMetadata(typeof(clsCompraDetalleArticuloInventarioED))]
    public class clsCompraDetalleArticuloInventarioED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsCompraDetalleArticuloInventarioED(): base(){
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
            get { return "CompraDetalleArticuloInventario"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("CompraDetalleArticuloInventario", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnComDetArtInvConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnComDetArtInvConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnComDetArtInvConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoArticulo" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT nnComDetArtInvCodigoArti NOT NULL, ");
            SQL.AppendLine("Cantidad" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnComDetArtInvCantidad NOT NULL, ");
            SQL.AppendLine("PrecioUnitario" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnComDetArtInvPrecioUnit NOT NULL, ");
            SQL.AppendLine("CantidadRecibida" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_ComDetArtInvCaRe DEFAULT (0), ");
            SQL.AppendLine("PorcentajeDeDistribucion" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_ComDetArtInvPoDeDi DEFAULT (0), ");
            SQL.AppendLine("MontoDistribucion" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_ComDetArtInvMoDi DEFAULT (0), ");
            SQL.AppendLine("PorcentajeSeguro" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_ComDetArtInvPoSe DEFAULT (0), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_CompraDetalleArticuloInventario PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, ConsecutivoCompra ASC, Consecutivo ASC)");
            SQL.AppendLine(",CONSTRAINT fk_CompraDetalleArticuloInventarioCompra FOREIGN KEY (ConsecutivoCompania, ConsecutivoCompra)");
            SQL.AppendLine("REFERENCES Adm.Compra(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine("ON UPDATE CASCADE");
            //SQL.AppendLine(", CONSTRAINT fk_CompraDetalleArticuloInventarioArticuloInventario FOREIGN KEY (ConsecutivoCompania, CodigoArticulo)");
            //SQL.AppendLine("REFERENCES dbo.ArticuloInventario(ConsecutivoCompania, Codigo)");
            //SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT CompraDetalleArticuloInventario.ConsecutivoCompania, CompraDetalleArticuloInventario.ConsecutivoCompra, CompraDetalleArticuloInventario.Consecutivo, CompraDetalleArticuloInventario.CodigoArticulo");
            SQL.AppendLine(", CompraDetalleArticuloInventario.Cantidad, CompraDetalleArticuloInventario.PrecioUnitario, CompraDetalleArticuloInventario.CantidadRecibida, CompraDetalleArticuloInventario.PorcentajeDeDistribucion");
            SQL.AppendLine(", CompraDetalleArticuloInventario.MontoDistribucion, CompraDetalleArticuloInventario.PorcentajeSeguro");
            SQL.AppendLine(", CompraDetalleArticuloInventario.fldTimeStamp, CAST(CompraDetalleArticuloInventario.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".CompraDetalleArticuloInventario");
            SQL.AppendLine("INNER JOIN dbo.Gv_ArticuloInventario_B2 ON  " + DbSchema + ".CompraDetalleArticuloInventario.CodigoArticulo = dbo.Gv_ArticuloInventario_B2.CodigoCompuesto");
            SQL.AppendLine("      AND " + DbSchema + ".CompraDetalleArticuloInventario.ConsecutivoCompania = dbo.Gv_ArticuloInventario_B2.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PrecioUnitario" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@CantidadRecibida" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PorcentajeDeDistribucion" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoDistribucion" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PorcentajeSeguro" + InsSql.DecimalTypeForDb(25, 4) + " = 0");
            return SQL.ToString();
        }

        private string SqlSpIns() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".CompraDetalleArticuloInventario(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            ConsecutivoCompra,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            CodigoArticulo,");
            SQL.AppendLine("            Cantidad,");
            SQL.AppendLine("            PrecioUnitario,");
            SQL.AppendLine("            CantidadRecibida,");
            SQL.AppendLine("            PorcentajeDeDistribucion,");
            SQL.AppendLine("            MontoDistribucion,");
            SQL.AppendLine("            PorcentajeSeguro)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @ConsecutivoCompra,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @CodigoArticulo,");
            SQL.AppendLine("            @Cantidad,");
            SQL.AppendLine("            @PrecioUnitario,");
            SQL.AppendLine("            @CantidadRecibida,");
            SQL.AppendLine("            @PorcentajeDeDistribucion,");
            SQL.AppendLine("            @MontoDistribucion,");
            SQL.AppendLine("            @PorcentajeSeguro)");
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
            SQL.AppendLine("@ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PrecioUnitario" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@CantidadRecibida" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PorcentajeDeDistribucion" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoDistribucion" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PorcentajeSeguro" + InsSql.DecimalTypeForDb(25, 4) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CompraDetalleArticuloInventario WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCompra = @ConsecutivoCompra AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CompraDetalleArticuloInventario WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCompra = @ConsecutivoCompra AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_CompraDetalleArticuloInventarioCanBeUpdated @ConsecutivoCompania,@ConsecutivoCompra,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".CompraDetalleArticuloInventario");
            SQL.AppendLine("            SET CodigoArticulo = @CodigoArticulo,");
            SQL.AppendLine("               Cantidad = @Cantidad,");
            SQL.AppendLine("               PrecioUnitario = @PrecioUnitario,");
            SQL.AppendLine("               CantidadRecibida = @CantidadRecibida,");
            SQL.AppendLine("               PorcentajeDeDistribucion = @PorcentajeDeDistribucion,");
            SQL.AppendLine("               MontoDistribucion = @MontoDistribucion,");
            SQL.AppendLine("               PorcentajeSeguro = @PorcentajeSeguro");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoCompra = @ConsecutivoCompra");
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
            SQL.AppendLine("@ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CompraDetalleArticuloInventario WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCompra = @ConsecutivoCompra AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CompraDetalleArticuloInventario WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCompra = @ConsecutivoCompra AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_CompraDetalleArticuloInventarioCanBeDeleted @ConsecutivoCompania,@ConsecutivoCompra,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".CompraDetalleArticuloInventario");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoCompra = @ConsecutivoCompra");
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
            SQL.AppendLine("@ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         ConsecutivoCompra,");
            SQL.AppendLine("         Consecutivo,");
            SQL.AppendLine("         CodigoArticulo,");
            SQL.AppendLine("         Cantidad,");
            SQL.AppendLine("         PrecioUnitario,");
            SQL.AppendLine("         CantidadRecibida,");
            SQL.AppendLine("         PorcentajeDeDistribucion,");
            SQL.AppendLine("         MontoDistribucion,");
            SQL.AppendLine("         PorcentajeSeguro,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".CompraDetalleArticuloInventario");
            SQL.AppendLine("      WHERE CompraDetalleArticuloInventario.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND CompraDetalleArticuloInventario.ConsecutivoCompra = @ConsecutivoCompra");
            SQL.AppendLine("         AND CompraDetalleArticuloInventario.Consecutivo = @Consecutivo");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpSelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SELECT ");
            SQL.AppendLine("        CompraDetalleArticuloInventario.ConsecutivoCompania,");
            SQL.AppendLine("        CompraDetalleArticuloInventario.ConsecutivoCompra,");
            SQL.AppendLine("        CompraDetalleArticuloInventario.Consecutivo,");
            SQL.AppendLine("        CompraDetalleArticuloInventario.CodigoArticulo,");
            SQL.AppendLine("        dbo.Gv_ArticuloInventario_B2.Descripcion AS DescripcionArticulo,");
            SQL.AppendLine("        CompraDetalleArticuloInventario.Cantidad,");
            SQL.AppendLine("        CompraDetalleArticuloInventario.PrecioUnitario,");
            SQL.AppendLine("        CompraDetalleArticuloInventario.CantidadRecibida,");
            SQL.AppendLine("        CompraDetalleArticuloInventario.PorcentajeDeDistribucion,");
            SQL.AppendLine("        CompraDetalleArticuloInventario.MontoDistribucion,");
			SQL.AppendLine("        CompraDetalleArticuloInventario.PorcentajeSeguro,");
            SQL.AppendLine("        dbo.Gv_ArticuloInventario_B2.AlicuotaIva,");
            SQL.AppendLine("        CompraDetalleArticuloInventario.fldTimeStamp");
            SQL.AppendLine("    FROM CompraDetalleArticuloInventario");
            SQL.AppendLine("    INNER JOIN dbo.Gv_ArticuloInventario_B2 ON " + DbSchema + ".CompraDetalleArticuloInventario.CodigoArticulo = dbo.Gv_ArticuloInventario_B2.CodigoCompuesto");
            SQL.AppendLine("     AND " + DbSchema + ".CompraDetalleArticuloInventario.ConsecutivoCompania = dbo.Gv_ArticuloInventario_B2.ConsecutivoCompania");
            SQL.AppendLine(" 	WHERE CompraDetalleArticuloInventario.ConsecutivoCompra = @ConsecutivoCompra");
            SQL.AppendLine(" 	AND CompraDetalleArticuloInventario.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpDelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	DELETE FROM CompraDetalleArticuloInventario");
            SQL.AppendLine(" 	WHERE ConsecutivoCompra = @ConsecutivoCompra");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInsDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@XmlDataDetail" + InsSql.XmlTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpInsDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SET NOCOUNT ON;");
            SQL.AppendLine("	DECLARE @ReturnValue  " + InsSql.NumericTypeForDb(10, 0));
	        SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
	        SQL.AppendLine("	    BEGIN");
            SQL.AppendLine("	    EXEC Adm.Gp_CompraDetalleArticuloInventarioDelDet @ConsecutivoCompania = @ConsecutivoCompania, @ConsecutivoCompra = @ConsecutivoCompra");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO Adm.CompraDetalleArticuloInventario(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        ConsecutivoCompra,");
			SQL.AppendLine("	        Consecutivo,");
			SQL.AppendLine("	        CodigoArticulo,");
			SQL.AppendLine("	        Cantidad,");
			SQL.AppendLine("	        PrecioUnitario,");
			SQL.AppendLine("	        CantidadRecibida,");
			SQL.AppendLine("	        PorcentajeDeDistribucion,");
			SQL.AppendLine("	        MontoDistribucion,");
			SQL.AppendLine("	        PorcentajeSeguro)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @ConsecutivoCompra,");
			SQL.AppendLine("	        Consecutivo,");
			SQL.AppendLine("	        CodigoArticulo,");
			SQL.AppendLine("	        Cantidad,");
			SQL.AppendLine("	        PrecioUnitario,");
			SQL.AppendLine("	        CantidadRecibida,");
			SQL.AppendLine("	        PorcentajeDeDistribucion,");
			SQL.AppendLine("	        MontoDistribucion,");
			SQL.AppendLine("	        PorcentajeSeguro");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataCompraDetalleArticuloInventario/GpDetailCompraDetalleArticuloInventario',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        Consecutivo " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        CodigoArticulo " + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("	        Cantidad " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        PrecioUnitario " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        CantidadRecibida " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        PorcentajeDeDistribucion " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        MontoDistribucion " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        PorcentajeSeguro " + InsSql.DecimalTypeForDb(25, 4) + ") AS XmlDocDetailOfCompra");
            SQL.AppendLine("	    EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("	    SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("	    RETURN @ReturnValue");
	        SQL.AppendLine("	END");
	        SQL.AppendLine("	ELSE");
            SQL.AppendLine("	    RETURN -1");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlViewRenglonCompra() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT ConsecutivoCompania, ConsecutivoCompra AS NumeroSecuencialCompra, Consecutivo AS ConsecutivoRenglon, CodigoArticulo, Cantidad, PrecioUnitario AS CostoUnitario, CantidadRecibida");
            SQL.AppendLine("FROM " + DbSchema + ".CompraDetalleArticuloInventario");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".CompraDetalleArticuloInventario", SqlCreateTable(), false, eDboType.Tabla);            
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_CompraDetalleArticuloInventario_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleArticuloInventarioINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleArticuloInventarioUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleArticuloInventarioDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleArticuloInventarioGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleArticuloInventarioSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleArticuloInventarioDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleArticuloInventarioInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".CompraDetalleArticuloInventario", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleArticuloInventarioINS");
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleArticuloInventarioUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleArticuloInventarioDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleArticuloInventarioGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleArticuloInventarioInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleArticuloInventarioDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleArticuloInventarioSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_CompraDetalleArticuloInventario_B1") && vResult;
           
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCompraDetalleArticuloInventarioED

} //End of namespace Galac.Adm.Dal.GestionCompras

