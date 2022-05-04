using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Dal.GestionCompras {
    [LibMefDalComponentMetadata(typeof(clsOrdenDeCompraDetalleArticuloInventarioED))]
    public class clsOrdenDeCompraDetalleArticuloInventarioED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsOrdenDeCompraDetalleArticuloInventarioED(): base(){
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
            get { return "OrdenDeCompraDetalleArticuloInventario"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("OrdenDeCompraDetalleArticuloInventario", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeComDetArtInvConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoOrdenDeCompra" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeComDetArtInvConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeComDetArtInvConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoArticulo" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT nnOrdDeComDetArtInvCodigoArti NOT NULL, ");
            SQL.AppendLine("DescripcionArticulo" + InsSql.VarCharTypeForDb(7000) + " CONSTRAINT nnOrdDeComDetArtInvDescripcio NOT NULL, ");
            SQL.AppendLine("Cantidad" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnOrdDeComDetArtInvCantidad NOT NULL, ");
            SQL.AppendLine("CostoUnitario" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnOrdDeComDetArtInvCostoUnita NOT NULL, ");
            SQL.AppendLine("CantidadRecibida" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_OrdDeComDetArtInvCaRe DEFAULT (0), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_OrdenDeCompraDetalleArticuloInventario PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, ConsecutivoOrdenDeCompra ASC, Consecutivo ASC)");
            SQL.AppendLine(",CONSTRAINT fk_OrdenDeCompraDetalleArticuloInventarioOrdenDeCompra FOREIGN KEY (ConsecutivoCompania, ConsecutivoOrdenDeCompra)");
            SQL.AppendLine("REFERENCES Adm.OrdenDeCompra(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine("ON UPDATE CASCADE");
            //SQL.AppendLine(", CONSTRAINT fk_OrdenDeCompraDetalleArticuloInventarioArticuloInventario FOREIGN KEY (ConsecutivoCompania, CodigoArticulo)");
            //SQL.AppendLine("REFERENCES Saw.ArticuloInventario(ConsecutivoCompania, Codigo)");
            //SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT OrdenDeCompraDetalleArticuloInventario.ConsecutivoCompania, OrdenDeCompraDetalleArticuloInventario.ConsecutivoOrdenDeCompra, OrdenDeCompraDetalleArticuloInventario.Consecutivo, OrdenDeCompraDetalleArticuloInventario.CodigoArticulo");
            SQL.AppendLine(", OrdenDeCompraDetalleArticuloInventario.DescripcionArticulo, OrdenDeCompraDetalleArticuloInventario.Cantidad, OrdenDeCompraDetalleArticuloInventario.CostoUnitario, OrdenDeCompraDetalleArticuloInventario.CantidadRecibida");
            SQL.AppendLine(", OrdenDeCompraDetalleArticuloInventario.fldTimeStamp, CAST(OrdenDeCompraDetalleArticuloInventario.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".OrdenDeCompraDetalleArticuloInventario");
            SQL.AppendLine("INNER JOIN dbo.Gv_ArticuloInventario_B2 ON  " + DbSchema + ".OrdenDeCompraDetalleArticuloInventario.CodigoArticulo = dbo.Gv_ArticuloInventario_B2.CodigoCompuesto");
            SQL.AppendLine("      AND " + DbSchema + ".OrdenDeCompraDetalleArticuloInventario.ConsecutivoCompania = dbo.Gv_ArticuloInventario_B2.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoOrdenDeCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@DescripcionArticulo" + InsSql.VarCharTypeForDb(7000) + " = '',");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@CostoUnitario" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@CantidadRecibida" + InsSql.DecimalTypeForDb(25, 4) + " = 0");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".OrdenDeCompraDetalleArticuloInventario(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            ConsecutivoOrdenDeCompra,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            CodigoArticulo,");
            SQL.AppendLine("            DescripcionArticulo,");
            SQL.AppendLine("            Cantidad,");
            SQL.AppendLine("            CostoUnitario,");
            SQL.AppendLine("            CantidadRecibida)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @ConsecutivoOrdenDeCompra,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @CodigoArticulo,");
            SQL.AppendLine("            @DescripcionArticulo,");
            SQL.AppendLine("            @Cantidad,");
            SQL.AppendLine("            @CostoUnitario,");
            SQL.AppendLine("            @CantidadRecibida)");
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
            SQL.AppendLine("@ConsecutivoOrdenDeCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@DescripcionArticulo" + InsSql.VarCharTypeForDb(7000) + ",");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@CostoUnitario" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@CantidadRecibida" + InsSql.DecimalTypeForDb(25, 4) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".OrdenDeCompraDetalleArticuloInventario WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoOrdenDeCompra = @ConsecutivoOrdenDeCompra AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".OrdenDeCompraDetalleArticuloInventario WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoOrdenDeCompra = @ConsecutivoOrdenDeCompra AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioCanBeUpdated @ConsecutivoCompania,@ConsecutivoOrdenDeCompra,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".OrdenDeCompraDetalleArticuloInventario");
            SQL.AppendLine("            SET CodigoArticulo = @CodigoArticulo,");
            SQL.AppendLine("               DescripcionArticulo = @DescripcionArticulo,");
            SQL.AppendLine("               Cantidad = @Cantidad,");
            SQL.AppendLine("               CostoUnitario = @CostoUnitario,");
            SQL.AppendLine("               CantidadRecibida = @CantidadRecibida");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoOrdenDeCompra = @ConsecutivoOrdenDeCompra");
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
            SQL.AppendLine("@ConsecutivoOrdenDeCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".OrdenDeCompraDetalleArticuloInventario WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoOrdenDeCompra = @ConsecutivoOrdenDeCompra AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".OrdenDeCompraDetalleArticuloInventario WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoOrdenDeCompra = @ConsecutivoOrdenDeCompra AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioCanBeDeleted @ConsecutivoCompania,@ConsecutivoOrdenDeCompra,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".OrdenDeCompraDetalleArticuloInventario");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoOrdenDeCompra = @ConsecutivoOrdenDeCompra");
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
            SQL.AppendLine("@ConsecutivoOrdenDeCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         ConsecutivoOrdenDeCompra,");
            SQL.AppendLine("         Consecutivo,");
            SQL.AppendLine("         CodigoArticulo,");
            SQL.AppendLine("         DescripcionArticulo,");
            SQL.AppendLine("         Cantidad,");
            SQL.AppendLine("         CostoUnitario,");
            SQL.AppendLine("         CantidadRecibida,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".OrdenDeCompraDetalleArticuloInventario");
            SQL.AppendLine("      WHERE OrdenDeCompraDetalleArticuloInventario.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND OrdenDeCompraDetalleArticuloInventario.ConsecutivoOrdenDeCompra = @ConsecutivoOrdenDeCompra");
            SQL.AppendLine("         AND OrdenDeCompraDetalleArticuloInventario.Consecutivo = @Consecutivo");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoOrdenDeCompra" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpSelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SELECT ");
            SQL.AppendLine("        ConsecutivoCompania,");
            SQL.AppendLine("        ConsecutivoOrdenDeCompra,");
            SQL.AppendLine("        Consecutivo,");
            SQL.AppendLine("        CodigoArticulo,");
            SQL.AppendLine("        DescripcionArticulo,");
            SQL.AppendLine("        Cantidad,");
            SQL.AppendLine("        CostoUnitario,");
            SQL.AppendLine("        CantidadRecibida,");
            SQL.AppendLine("        fldTimeStamp");
            SQL.AppendLine("    FROM OrdenDeCompraDetalleArticuloInventario");
            SQL.AppendLine(" 	WHERE ConsecutivoOrdenDeCompra = @ConsecutivoOrdenDeCompra");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoOrdenDeCompra" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpDelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	DELETE FROM OrdenDeCompraDetalleArticuloInventario");
            SQL.AppendLine(" 	WHERE ConsecutivoOrdenDeCompra = @ConsecutivoOrdenDeCompra");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInsDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoOrdenDeCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("	    EXEC Adm.Gp_OrdenDeCompraDetalleArticuloInventarioDelDet @ConsecutivoCompania = @ConsecutivoCompania, @ConsecutivoOrdenDeCompra = @ConsecutivoOrdenDeCompra");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO Adm.OrdenDeCompraDetalleArticuloInventario(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        ConsecutivoOrdenDeCompra,");
			SQL.AppendLine("	        Consecutivo,");
			SQL.AppendLine("	        CodigoArticulo,");
			SQL.AppendLine("	        DescripcionArticulo,");
			SQL.AppendLine("	        Cantidad,");
			SQL.AppendLine("	        CostoUnitario,");
			SQL.AppendLine("	        CantidadRecibida)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @ConsecutivoOrdenDeCompra,");
			SQL.AppendLine("	        Consecutivo,");
			SQL.AppendLine("	        CodigoArticulo,");
			SQL.AppendLine("	        DescripcionArticulo,");
			SQL.AppendLine("	        Cantidad,");
			SQL.AppendLine("	        CostoUnitario,");
			SQL.AppendLine("	        CantidadRecibida");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataOrdenDeCompraDetalleArticuloInventario/GpDetailOrdenDeCompraDetalleArticuloInventario',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        Consecutivo " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        CodigoArticulo " + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("	        DescripcionArticulo " + InsSql.VarCharTypeForDb(7000) + ",");
            SQL.AppendLine("	        Cantidad " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        CostoUnitario " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        CantidadRecibida " + InsSql.DecimalTypeForDb(25, 4) + ") AS XmlDocDetailOfOrdenDeCompra");
            SQL.AppendLine("	    EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("	    SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("	    RETURN @ReturnValue");
	        SQL.AppendLine("	END");
	        SQL.AppendLine("	ELSE");
            SQL.AppendLine("	    RETURN -1");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        private string SqlSpInstParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoOrdenDeCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@DescripcionArticulo" + InsSql.VarCharTypeForDb(7000) + ",");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@CostoUnitario" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@CantidadRecibida" + InsSql.DecimalTypeForDb(25, 4));
            return SQL.ToString();
        }
       private string SqlSpInst() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".OrdenDeCompraDetalleArticuloInventario");
            SQL.AppendLine("            SET CodigoArticulo = @CodigoArticulo,");
            SQL.AppendLine("               DescripcionArticulo = @DescripcionArticulo,");
            SQL.AppendLine("               Cantidad = @Cantidad,");
            SQL.AppendLine("               CostoUnitario = @CostoUnitario,");
            SQL.AppendLine("               CantidadRecibida = @CantidadRecibida");
            SQL.AppendLine("               WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoOrdenDeCompra = @ConsecutivoOrdenDeCompra");
            SQL.AppendLine("               AND Consecutivo = @Consecutivo");
            SQL.AppendLine("	IF @@ROWCOUNT = 0");
            SQL.AppendLine("        INSERT INTO " + DbSchema + ".OrdenDeCompraDetalleArticuloInventario(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            ConsecutivoOrdenDeCompra,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            CodigoArticulo,");
            SQL.AppendLine("            DescripcionArticulo,");
            SQL.AppendLine("            Cantidad,");
            SQL.AppendLine("            CostoUnitario,");
            SQL.AppendLine("            CantidadRecibida)");
            SQL.AppendLine("        VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @ConsecutivoOrdenDeCompra,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @CodigoArticulo,");
            SQL.AppendLine("            @DescripcionArticulo,");
            SQL.AppendLine("            @Cantidad,");
            SQL.AppendLine("            @CostoUnitario,");
            SQL.AppendLine("            @CantidadRecibida)");
            SQL.AppendLine(" 	IF @@ERROR = 0");
            SQL.AppendLine(" 		COMMIT TRAN");
            SQL.AppendLine(" 	ELSE");
            SQL.AppendLine(" 		ROLLBACK");
            SQL.AppendLine("END ");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".OrdenDeCompraDetalleArticuloInventario", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_OrdenDeCompraDetalleArticuloInventario_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioINST", SqlSpInstParameters(),SqlSpInst(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".OrdenDeCompraDetalleArticuloInventario", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioINS");
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioSelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeCompraDetalleArticuloInventarioINST") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_OrdenDeCompraDetalleArticuloInventario_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsOrdenDeCompraDetalleArticuloInventarioED

} //End of namespace Galac.Adm.Dal.GestionCompras

