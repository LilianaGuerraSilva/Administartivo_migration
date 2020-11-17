using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Dal.Inventario {
    [LibMefDalComponentMetadata(typeof(clsAlmacenED))]
    public class clsAlmacenED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsAlmacenED(): base(){
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
            get { return "Almacen"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("Almacen", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnAlmConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnAlmConsecutiv NOT NULL, ");
            SQL.AppendLine("Codigo" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT nnAlmCodigo NOT NULL, ");
            SQL.AppendLine("NombreAlmacen" + InsSql.VarCharTypeForDb(40) + " CONSTRAINT d_AlmNoAl DEFAULT (''), ");
            SQL.AppendLine("TipoDeAlmacen" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_AlmTiDeAl DEFAULT ('0'), ");
            SQL.AppendLine("ConsecutivoCliente" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnAlmConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoCc" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT d_AlmCoCc DEFAULT (''), ");
            SQL.AppendLine("Descripcion" + InsSql.VarCharTypeForDb(40) + " CONSTRAINT d_AlmDe DEFAULT (''), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(20) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_Almacen PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Consecutivo ASC)");
            SQL.AppendLine(", CONSTRAINT fk_AlmacenCliente FOREIGN KEY (ConsecutivoCompania, ConsecutivoCliente)");
            SQL.AppendLine("REFERENCES dbo.Cliente(ConsecutivoCompania, consecutivo)");
			SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT u_Almacen UNIQUE NONCLUSTERED");
           SQL.AppendLine("(ConsecutivoCompania ASC, Codigo ASC)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT Almacen.ConsecutivoCompania, Almacen.Consecutivo, Almacen.Codigo, Almacen.NombreAlmacen, Almacen.TipoDeAlmacen, " + DbSchema + ".Gv_EnumTipoDeAlmacen.StrValue AS TipoDeAlmacenStr");
            SQL.AppendLine(", Almacen.ConsecutivoCliente, Almacen.CodigoCc, Almacen.Descripcion, Almacen.NombreOperador");
            SQL.AppendLine(", Almacen.FechaUltimaModificacion");
            SQL.AppendLine(", Almacen.fldTimeStamp, CAST(Almacen.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".Almacen");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoDeAlmacen");
            SQL.AppendLine("ON " + DbSchema + ".Almacen.TipoDeAlmacen COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDeAlmacen.DbValue");
            SQL.AppendLine("INNER JOIN dbo.Cliente ON  " + DbSchema + ".Almacen.ConsecutivoCliente = dbo.Cliente.consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Almacen.ConsecutivoCompania = dbo.Cliente.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " = 0,");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@NombreAlmacen" + InsSql.VarCharTypeForDb(40) + " = '',");
            SQL.AppendLine("@TipoDeAlmacen" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@ConsecutivoCliente" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoCc" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(40) + ",");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + " = '01/01/1900'");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".Almacen(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            Codigo,");
            SQL.AppendLine("            NombreAlmacen,");
            SQL.AppendLine("            TipoDeAlmacen,");
            SQL.AppendLine("            ConsecutivoCliente,");
            SQL.AppendLine("            CodigoCc,");
            SQL.AppendLine("            Descripcion,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @Codigo,");
            SQL.AppendLine("            @NombreAlmacen,");
            SQL.AppendLine("            @TipoDeAlmacen,");
            SQL.AppendLine("            @ConsecutivoCliente,");
            SQL.AppendLine("            @CodigoCc,");
            SQL.AppendLine("            @Descripcion,");
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
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@NombreAlmacen" + InsSql.VarCharTypeForDb(40) + ",");
            SQL.AppendLine("@TipoDeAlmacen" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ConsecutivoCliente" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoCc" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(40) + ",");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Almacen WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Almacen WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_AlmacenCanBeUpdated @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".Almacen");
            SQL.AppendLine("            SET Codigo = @Codigo,");
            SQL.AppendLine("               NombreAlmacen = @NombreAlmacen,");
            SQL.AppendLine("               TipoDeAlmacen = @TipoDeAlmacen,");
            SQL.AppendLine("               ConsecutivoCliente = @ConsecutivoCliente,");
            SQL.AppendLine("               CodigoCc = @CodigoCc,");
            SQL.AppendLine("               Descripcion = @Descripcion,");
            SQL.AppendLine("               NombreOperador = @NombreOperador,");
            SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Almacen WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Almacen WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_AlmacenCanBeDeleted @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".Almacen");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
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
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         Almacen.ConsecutivoCompania,");
            SQL.AppendLine("         Almacen.Consecutivo,");
            SQL.AppendLine("         Almacen.Codigo,");
            SQL.AppendLine("         Almacen.NombreAlmacen,");
            SQL.AppendLine("         Almacen.TipoDeAlmacen,");
            SQL.AppendLine("         Almacen.ConsecutivoCliente,");
            SQL.AppendLine("         Cliente.nombre AS NombreCliente,");
            SQL.AppendLine("         Almacen.CodigoCc,");
            SQL.AppendLine("         Almacen.Descripcion,");
            SQL.AppendLine("         Almacen.NombreOperador,");
            SQL.AppendLine("         Almacen.FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(Almacen.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         Almacen.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".Almacen");
            SQL.AppendLine("             INNER JOIN dbo.Cliente ON " + DbSchema + ".Almacen.ConsecutivoCliente = dbo.Cliente.consecutivo");
            SQL.AppendLine("             AND " + DbSchema + ".Almacen.ConsecutivoCompania = dbo.Cliente.ConsecutivoCompania");
            //SQL.AppendLine("             INNER JOIN dbo.Gv_CentroDeCostos_B1 ON " + DbSchema + ".Almacen.CodigoCc = dbo.Gv_CentroDeCostos_B1.Codigo");
            //SQL.AppendLine("             INNER JOIN dbo.Gv_CentroDeCostos_B1 ON " + DbSchema + ".Almacen.Descripcion = dbo.Gv_CentroDeCostos_B1.Descripcion");
            SQL.AppendLine("      WHERE " + DbSchema + ".Almacen.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND " + DbSchema + ".Almacen.Consecutivo = @Consecutivo");
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
            SQL.AppendLine("   SET @strSQL = 'SET DateFormat ' + @DateFormat + ");
            SQL.AppendLine("      ' SELECT TOP 500 ");
            SQL.AppendLine("      " + DbSchema + ".Gv_Almacen_B1.Codigo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Almacen_B1.NombreAlmacen,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Almacen_B1.TipoDeAlmacenStr,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Almacen_B1.CodigoCc,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Almacen_B1.Descripcion,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Almacen_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Almacen_B1.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Almacen_B1.TipoDeAlmacen,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Almacen_B1.ConsecutivoCliente,");
            SQL.AppendLine("      dbo.Cliente.nombre AS NombreCliente");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Almacen_B1");
            SQL.AppendLine("      INNER JOIN dbo.Cliente ON  " + DbSchema + ".Gv_Almacen_B1.ConsecutivoCliente = dbo.Cliente.consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Almacen_B1.ConsecutivoCompania = dbo.Cliente.ConsecutivoCompania");
            //SQL.AppendLine("      INNER JOIN dbo.Gv_CentroDeCostos_B1 ON  " + DbSchema + ".Gv_Almacen_B1.CodigoCc = dbo.Gv_CentroDeCostos_B1.Codigo");
            //SQL.AppendLine("      AND " + DbSchema + ".Gv_Almacen_B1.ConsecutivoCompania = dbo.Gv_CentroDeCostos_B1.ConsecutivoPeriodo");
            //SQL.AppendLine("      INNER JOIN dbo.Gv_CentroDeCostos_B1 ON  " + DbSchema + ".Gv_Almacen_B1.Descripcion = dbo.Gv_CentroDeCostos_B1.Descripcion");
            //SQL.AppendLine("      AND " + DbSchema + ".Gv_Almacen_B1.ConsecutivoCompania = dbo.Gv_CentroDeCostos_B1.ConsecutivoPeriodo");
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
            SQL.AppendLine("      " + DbSchema + ".Almacen.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Almacen.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Almacen.Codigo,");
            SQL.AppendLine("      " + DbSchema + ".Almacen.NombreAlmacen,");
            SQL.AppendLine("      " + DbSchema + ".Almacen.TipoDeAlmacen,");
            SQL.AppendLine("      " + DbSchema + ".Almacen.ConsecutivoCliente,");
            SQL.AppendLine("      " + DbSchema + ".Almacen.CodigoCc,");
            SQL.AppendLine("      " + DbSchema + ".Almacen.Descripcion");
            //SQL.AppendLine("      ," + DbSchema + ".Almacen.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".Almacen");
            SQL.AppendLine("      WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("          AND Consecutivo IN (");
            SQL.AppendLine("            SELECT  Consecutivo ");
            SQL.AppendLine("            FROM OPENXML( @hdoc, 'GpData/GpResult',2) ");
            SQL.AppendLine("            WITH (Consecutivo int) AS XmlFKTmp) ");
            SQL.AppendLine(" EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries
        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".Almacen", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }
        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDeAlmacen", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDeAlmacen), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_Almacen_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }
        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AlmacenINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AlmacenUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AlmacenDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AlmacenGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AlmacenSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AlmacenGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".Almacen", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_AlmacenINS");
            vResult = insSp.Drop(DbSchema + ".Gp_AlmacenUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_AlmacenDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_AlmacenGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_AlmacenGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_AlmacenSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_Almacen_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDeAlmacen") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsAlmacenED

} //End of namespace Galac.Saw.Dal.Inventario

