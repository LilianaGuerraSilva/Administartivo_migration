using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.DefGen;

namespace Galac.Saw.Dal.Inventario {
    [LibMefDalComponentMetadata(typeof(clsNotaDeEntradaSalidaED))]
    public class clsNotaDeEntradaSalidaED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsNotaDeEntradaSalidaED(): base(){
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
            get { return "NotaDeEntradaSalida"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("NotaDeEntradaSalida", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnNotDeEntSalConsecutiv NOT NULL, ");
            SQL.AppendLine("NumeroDocumento" + InsSql.VarCharTypeForDb(11) + " CONSTRAINT nnNotDeEntSalNumeroDocu NOT NULL, ");
            SQL.AppendLine("TipodeOperacion" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_NotDeEntSalTiOp DEFAULT ('0'), ");
            SQL.AppendLine("CodigoCliente" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT d_NotDeEntSalCoCl DEFAULT (''), ");
            SQL.AppendLine("CodigoAlmacen" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT d_NotDeEntSalCoAl DEFAULT (''), ");
            SQL.AppendLine("Fecha" + InsSql.DateTypeForDb() + " CONSTRAINT nnNotDeEntSalFecha NOT NULL, ");
            SQL.AppendLine("Comentarios" + InsSql.VarCharTypeForDb(255) + " CONSTRAINT d_NotDeEntSalCo DEFAULT (''), ");
            SQL.AppendLine("CodigoLote" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT d_NotDeEntSalCoLo DEFAULT (''), ");
            SQL.AppendLine("StatusNotaEntradaSalida" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_NotDeEntSalStNoEnSa DEFAULT ('0'), ");
            SQL.AppendLine("ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT d_NotDeEntSalCoAl DEFAULT (0), ");
            SQL.AppendLine("GeneradoPor" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_NotDeEntSalGePo DEFAULT ('0'), ");
            SQL.AppendLine("ConsecutivoDocumentoOrigen" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT d_NotDeEntSalCoDoOr DEFAULT (0), ");
            SQL.AppendLine("TipoNotaProduccion" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_NotDeEntSalTiNoPr DEFAULT ('0'), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_NotaDeEntradaSalida PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, NumeroDocumento ASC)");
            SQL.AppendLine(", CONSTRAINT fk_NotaDeEntradaSalidaCliente FOREIGN KEY (ConsecutivoCompania, CodigoCliente)");
            SQL.AppendLine("REFERENCES dbo.Cliente(ConsecutivoCompania, codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_NotaDeEntradaSalidaAlmacen FOREIGN KEY (ConsecutivoCompania, CodigoAlmacen)");
            SQL.AppendLine("REFERENCES Saw.Almacen(ConsecutivoCompania, Codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_NotaDeEntradaSalidaAlmacen FOREIGN KEY (ConsecutivoCompania, ConsecutivoAlmacen)");
            SQL.AppendLine("REFERENCES Saw.Almacen(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(",CONSTRAINT u_NotDeEntSalNumeroDocumento UNIQUE NONCLUSTERED (ConsecutivoCompania, NumeroDocumento)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT NotaDeEntradaSalida.ConsecutivoCompania, NotaDeEntradaSalida.NumeroDocumento, NotaDeEntradaSalida.TipodeOperacion, " + DbSchema + ".Gv_EnumTipodeOperacion.StrValue AS TipodeOperacionStr, NotaDeEntradaSalida.CodigoCliente");
            SQL.AppendLine(", NotaDeEntradaSalida.CodigoAlmacen, NotaDeEntradaSalida.Fecha, NotaDeEntradaSalida.Comentarios, NotaDeEntradaSalida.CodigoLote");
            SQL.AppendLine(", NotaDeEntradaSalida.StatusNotaEntradaSalida, " + DbSchema + ".Gv_EnumStatusNotaEntradaSalida.StrValue AS StatusNotaEntradaSalidaStr, NotaDeEntradaSalida.ConsecutivoAlmacen, NotaDeEntradaSalida.GeneradoPor, " + DbSchema + ".Gv_EnumTipoGeneradoPorNotaDeEntradaSalida.StrValue AS GeneradoPorStr, NotaDeEntradaSalida.ConsecutivoDocumentoOrigen");
            SQL.AppendLine(", NotaDeEntradaSalida.TipoNotaProduccion, " + DbSchema + ".Gv_EnumTipoNotaProduccion.StrValue AS TipoNotaProduccionStr, NotaDeEntradaSalida.NombreOperador, NotaDeEntradaSalida.FechaUltimaModificacion");
            SQL.AppendLine(", dbo.Cliente.nombre AS NombreCliente");
            SQL.AppendLine(", NotaDeEntradaSalida.fldTimeStamp, CAST(NotaDeEntradaSalida.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".NotaDeEntradaSalida");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipodeOperacion");
            SQL.AppendLine("ON " + DbSchema + ".NotaDeEntradaSalida.TipodeOperacion COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipodeOperacion.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumStatusNotaEntradaSalida");
            SQL.AppendLine("ON " + DbSchema + ".NotaDeEntradaSalida.StatusNotaEntradaSalida COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumStatusNotaEntradaSalida.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoGeneradoPorNotaDeEntradaSalida");
            SQL.AppendLine("ON " + DbSchema + ".NotaDeEntradaSalida.GeneradoPor COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoGeneradoPorNotaDeEntradaSalida.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoNotaProduccion");
            SQL.AppendLine("ON " + DbSchema + ".NotaDeEntradaSalida.TipoNotaProduccion COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoNotaProduccion.DbValue");
            SQL.AppendLine("INNER JOIN dbo.Cliente ON  " + DbSchema + ".NotaDeEntradaSalida.CodigoCliente = dbo.Cliente.codigo");
            SQL.AppendLine("      AND " + DbSchema + ".NotaDeEntradaSalida.ConsecutivoCompania = dbo.Cliente.ConsecutivoCompania");            
            SQL.AppendLine("INNER JOIN Saw.Almacen ON  " + DbSchema + ".NotaDeEntradaSalida.ConsecutivoAlmacen = Saw.Almacen.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".NotaDeEntradaSalida.ConsecutivoCompania = Saw.Almacen.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@TipodeOperacion" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@CodigoCliente" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@CodigoAlmacen" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@Comentarios" + InsSql.VarCharTypeForDb(255) + " = '',");
            SQL.AppendLine("@CodigoLote" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@StatusNotaEntradaSalida" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@GeneradoPor" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@ConsecutivoDocumentoOrigen" + InsSql.NumericTypeForDb(10, 0) + " = 0,");
            SQL.AppendLine("@TipoNotaProduccion" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + " = '01/01/1900'");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".NotaDeEntradaSalida(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            NumeroDocumento,");
            SQL.AppendLine("            TipodeOperacion,");
            SQL.AppendLine("            CodigoCliente,");
            SQL.AppendLine("            CodigoAlmacen,");
            SQL.AppendLine("            Fecha,");
            SQL.AppendLine("            Comentarios,");
            SQL.AppendLine("            CodigoLote,");
            SQL.AppendLine("            StatusNotaEntradaSalida,");
            SQL.AppendLine("            ConsecutivoAlmacen,");
            SQL.AppendLine("            GeneradoPor,");
            SQL.AppendLine("            ConsecutivoDocumentoOrigen,");
            SQL.AppendLine("            TipoNotaProduccion,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @NumeroDocumento,");
            SQL.AppendLine("            @TipodeOperacion,");
            SQL.AppendLine("            @CodigoCliente,");
            SQL.AppendLine("            @CodigoAlmacen,");
            SQL.AppendLine("            @Fecha,");
            SQL.AppendLine("            @Comentarios,");
            SQL.AppendLine("            @CodigoLote,");
            SQL.AppendLine("            @StatusNotaEntradaSalida,");
            SQL.AppendLine("            @ConsecutivoAlmacen,");
            SQL.AppendLine("            @GeneradoPor,");
            SQL.AppendLine("            @ConsecutivoDocumentoOrigen,");
            SQL.AppendLine("            @TipoNotaProduccion,");
            SQL.AppendLine("            @NombreOperador,");
            SQL.AppendLine("            @FechaUltimaModificacion)");
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
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@TipodeOperacion" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@CodigoCliente" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@CodigoAlmacen" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@Comentarios" + InsSql.VarCharTypeForDb(255) + ",");
            SQL.AppendLine("@CodigoLote" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@StatusNotaEntradaSalida" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@GeneradoPor" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ConsecutivoDocumentoOrigen" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@TipoNotaProduccion" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".NotaDeEntradaSalida WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroDocumento = @NumeroDocumento)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".NotaDeEntradaSalida WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroDocumento = @NumeroDocumento");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_NotaDeEntradaSalidaCanBeUpdated @ConsecutivoCompania,@NumeroDocumento, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".NotaDeEntradaSalida");
            SQL.AppendLine("            SET TipodeOperacion = @TipodeOperacion,");
            SQL.AppendLine("               CodigoCliente = @CodigoCliente,");
            SQL.AppendLine("               CodigoAlmacen = @CodigoAlmacen,");
            SQL.AppendLine("               Fecha = @Fecha,");
            SQL.AppendLine("               Comentarios = @Comentarios,");
            SQL.AppendLine("               CodigoLote = @CodigoLote,");
            SQL.AppendLine("               StatusNotaEntradaSalida = @StatusNotaEntradaSalida,");
            SQL.AppendLine("               ConsecutivoAlmacen = @ConsecutivoAlmacen,");
            SQL.AppendLine("               GeneradoPor = @GeneradoPor,");
            SQL.AppendLine("               ConsecutivoDocumentoOrigen = @ConsecutivoDocumentoOrigen,");
            SQL.AppendLine("               TipoNotaProduccion = @TipoNotaProduccion,");
            SQL.AppendLine("               NombreOperador = @NombreOperador,");
            SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND NumeroDocumento = @NumeroDocumento");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".NotaDeEntradaSalida WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroDocumento = @NumeroDocumento)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".NotaDeEntradaSalida WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroDocumento = @NumeroDocumento");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_NotaDeEntradaSalidaCanBeDeleted @ConsecutivoCompania,@NumeroDocumento, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".NotaDeEntradaSalida");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND NumeroDocumento = @NumeroDocumento");
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
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(11));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         NotaDeEntradaSalida.ConsecutivoCompania,");
            SQL.AppendLine("         NotaDeEntradaSalida.NumeroDocumento,");
            SQL.AppendLine("         NotaDeEntradaSalida.TipodeOperacion,");
            SQL.AppendLine("         NotaDeEntradaSalida.CodigoCliente,");
            SQL.AppendLine("         dbo.Cliente.nombre AS NombreCliente,");
            SQL.AppendLine("         NotaDeEntradaSalida.CodigoAlmacen,");
            SQL.AppendLine("         Gv_Almacen_B1.NombreAlmacen AS NombreAlmacen,");
            SQL.AppendLine("         NotaDeEntradaSalida.Fecha,");
            SQL.AppendLine("         NotaDeEntradaSalida.Comentarios,");
            SQL.AppendLine("         NotaDeEntradaSalida.CodigoLote,");
            SQL.AppendLine("         NotaDeEntradaSalida.StatusNotaEntradaSalida,");
            SQL.AppendLine("         NotaDeEntradaSalida.ConsecutivoAlmacen,");
            SQL.AppendLine("         NotaDeEntradaSalida.GeneradoPor,");
            SQL.AppendLine("         NotaDeEntradaSalida.ConsecutivoDocumentoOrigen,");
            SQL.AppendLine("         NotaDeEntradaSalida.TipoNotaProduccion,");
            SQL.AppendLine("         NotaDeEntradaSalida.NombreOperador,");
            SQL.AppendLine("         NotaDeEntradaSalida.FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(NotaDeEntradaSalida.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         NotaDeEntradaSalida.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".NotaDeEntradaSalida");
            SQL.AppendLine("             INNER JOIN dbo.Cliente ON " + DbSchema + ".NotaDeEntradaSalida.ConsecutivoCompania = dbo.Cliente.ConsecutivoCompania AND " + DbSchema + ".NotaDeEntradaSalida.CodigoCliente = dbo.Cliente.codigo");            
            SQL.AppendLine("             INNER JOIN Saw.Gv_Almacen_B1 ON " + DbSchema + ".NotaDeEntradaSalida.ConsecutivoCompania = Saw.Gv_Almacen_B1.ConsecutivoCompania AND " + DbSchema + ".NotaDeEntradaSalida.ConsecutivoAlmacen = Saw.Gv_Almacen_B1.Consecutivo");
            SQL.AppendLine("      WHERE NotaDeEntradaSalida.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND NotaDeEntradaSalida.NumeroDocumento = @NumeroDocumento");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_NotaDeEntradaSalida_B1.NumeroDocumento,");
            SQL.AppendLine("      " + DbSchema + ".Gv_NotaDeEntradaSalida_B1.TipodeOperacionStr,");
            SQL.AppendLine("      " + DbSchema + ".Gv_NotaDeEntradaSalida_B1.CodigoCliente,");
            SQL.AppendLine("      " + DbSchema + ".Cliente.nombre AS NombreCliente,");
            SQL.AppendLine("      " + DbSchema + ".Gv_NotaDeEntradaSalida_B1.CodigoAlmacen,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_NotaDeEntradaSalida_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_NotaDeEntradaSalida_B1.TipodeOperacion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_NotaDeEntradaSalida_B1.Fecha,");
            SQL.AppendLine("      " + DbSchema + ".Gv_NotaDeEntradaSalida_B1.Comentarios");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_NotaDeEntradaSalida_B1");
            SQL.AppendLine("      INNER JOIN dbo.Cliente ON  " + DbSchema + ".Gv_NotaDeEntradaSalida_B1.CodigoCliente = dbo.Cliente.codigo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_NotaDeEntradaSalida_B1.ConsecutivoCompania = dbo.Cliente.ConsecutivoCompania");            
            SQL.AppendLine("      INNER JOIN Saw.Gv_Almacen_B1 ON  " + DbSchema + ".Gv_NotaDeEntradaSalida_B1.ConsecutivoAlmacen = Saw.Gv_Almacen_B1.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_NotaDeEntradaSalida_B1.ConsecutivoCompania = Saw.Gv_Almacen_B1.ConsecutivoCompania");
            SQL.AppendLine("'   IF (NOT @SQLWhere IS NULL) AND (@SQLWhere <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' WHERE ' + @SQLWhere");
            SQL.AppendLine("   IF (NOT @SQLOrderBy IS NULL) AND (@SQLOrderBy <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' ORDER BY ' + @SQLOrderBy");
            SQL.AppendLine("   EXEC(@strSQL)");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpGetFKParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@XmlData" + InsSql.XmlTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpGetFK() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine(" DECLARE @hdoc int ");
            SQL.AppendLine(" EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlData ");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("      " + DbSchema + ".NotaDeEntradaSalida.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".NotaDeEntradaSalida.NumeroDocumento,");
            SQL.AppendLine("      " + DbSchema + ".NotaDeEntradaSalida.TipodeOperacion,");
            SQL.AppendLine("      " + DbSchema + ".NotaDeEntradaSalida.CodigoCliente,");
            SQL.AppendLine("      " + DbSchema + ".NotaDeEntradaSalida.CodigoAlmacen,");
            SQL.AppendLine("      " + DbSchema + ".NotaDeEntradaSalida.Fecha,");
            SQL.AppendLine("      " + DbSchema + ".NotaDeEntradaSalida.Comentarios,");
            SQL.AppendLine("      " + DbSchema + ".NotaDeEntradaSalida.ConsecutivoAlmacen");
            //SQL.AppendLine("      ," + DbSchema + ".NotaDeEntradaSalida.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".NotaDeEntradaSalida");
            SQL.AppendLine("      WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("          AND NumeroDocumento IN (");
            SQL.AppendLine("            SELECT  NumeroDocumento ");
            SQL.AppendLine("            FROM OPENXML( @hdoc, 'GpData/GpResult',2) ");
            SQL.AppendLine("            WITH (NumeroDocumento varchar(11)) AS XmlFKTmp) ");
            SQL.AppendLine(" EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            //bool vResult = insDbo.Create(DbSchema + ".NotaDeEntradaSalida", SqlCreateTable(), false, eDboType.Tabla);
            return true;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipodeOperacion", LibTpvCreator.SqlViewStandardEnum(typeof(eTipodeOperacion), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumStatusNotaEntradaSalida", LibTpvCreator.SqlViewStandardEnum(typeof(eStatusNotaEntradaSalida), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoGeneradoPorNotaDeEntradaSalida", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoGeneradoPorNotaDeEntradaSalida), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoNotaProduccion", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoNotaProduccion), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_NotaDeEntradaSalida_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_NotaDeEntradaSalidaINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_NotaDeEntradaSalidaUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_NotaDeEntradaSalidaDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_NotaDeEntradaSalidaGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_NotaDeEntradaSalidaSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_NotaDeEntradaSalidaGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
            insSps.Dispose();
            return vResult;
        }

        public bool InstalarTabla() {
            bool vResult = false;
            if (CrearTabla()) {
                CrearVistas();
                CrearProcedimientos();
                clsRenglonNotaESED insDetailRenNotES = new clsRenglonNotaESED();
                vResult = insDetailRenNotES.InstalarTabla();
            }
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = false;
            if (insDbo.Exists(DbSchema + ".NotaDeEntradaSalida", eDboType.Tabla)) {
                CrearVistas();
                CrearProcedimientos();
                vResult = new clsRenglonNotaESED().InstalarVistasYSps();
            }
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = false;
            LibStoredProc insSp = new LibStoredProc();
            LibViews insVista = new LibViews();
            vResult = new clsRenglonNotaESED().BorrarVistasYSps();
            vResult = insSp.Drop(DbSchema + ".Gp_NotaDeEntradaSalidaINS") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_NotaDeEntradaSalidaUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_NotaDeEntradaSalidaDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_NotaDeEntradaSalidaGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_NotaDeEntradaSalidaGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_NotaDeEntradaSalidaSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_NotaDeEntradaSalida_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipodeOperacion") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumStatusNotaEntradaSalida") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoGeneradoPorNotaDeEntradaSalida") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoNotaProduccion") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsNotaDeEntradaSalidaED

} //End of namespace Galac.Saw.Dal.Inventario

