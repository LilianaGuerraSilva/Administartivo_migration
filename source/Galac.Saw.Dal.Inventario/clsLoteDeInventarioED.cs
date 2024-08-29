using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Dal.Inventario {
    [LibMefDalComponentMetadata(typeof(clsLoteDeInventarioED))]
    public class clsLoteDeInventarioED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsLoteDeInventarioED(): base(){
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
            get { return "LoteDeInventario"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("LoteDeInventario", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnLotDeInvConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnLotDeInvConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoLote" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT nnLotDeInvCodigoLote NOT NULL, ");
            SQL.AppendLine("CodigoArticulo" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT nnLotDeInvCodigoArti NOT NULL, ");
            SQL.AppendLine("FechaDeElaboracion" + InsSql.DateTypeForDb() + " CONSTRAINT d_LotDeInvFeDeEl DEFAULT (''), ");
            SQL.AppendLine("FechaDeVencimiento" + InsSql.DateTypeForDb() + " CONSTRAINT d_LotDeInvFeDeVe DEFAULT (''), ");
            SQL.AppendLine("Existencia" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_LotDeInvEx DEFAULT (0), ");
            SQL.AppendLine("StatusLoteInv" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_LotDeInvStLoIn DEFAULT ('0'), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_LoteDeInventario PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Consecutivo ASC)");
            SQL.AppendLine(", CONSTRAINT fk_LoteDeInventarioArticuloInventario FOREIGN KEY (ConsecutivoCompania, CodigoArticulo)");
            SQL.AppendLine("REFERENCES ArticuloInventario(ConsecutivoCompania, Codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(",CONSTRAINT u_LotDeInvniaoteulo UNIQUE NONCLUSTERED (ConsecutivoCompania,CodigoLote,CodigoArticulo)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT LoteDeInventario.ConsecutivoCompania, LoteDeInventario.Consecutivo, LoteDeInventario.CodigoLote, LoteDeInventario.CodigoArticulo,ArticuloInventario.Descripcion As DescripcionArticulo");
            SQL.AppendLine(", LoteDeInventario.FechaDeElaboracion, LoteDeInventario.FechaDeVencimiento, LoteDeInventario.Existencia, LoteDeInventario.StatusLoteInv, " + DbSchema + ".Gv_EnumStatusLoteDeInventario.StrValue AS StatusLoteInvStr");
            SQL.AppendLine(", LoteDeInventario.NombreOperador, LoteDeInventario.FechaUltimaModificacion");
            SQL.AppendLine(", LoteDeInventario.fldTimeStamp, CAST(LoteDeInventario.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".LoteDeInventario");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumStatusLoteDeInventario");
            SQL.AppendLine("ON " + DbSchema + ".LoteDeInventario.StatusLoteInv COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumStatusLoteDeInventario.DbValue");
            SQL.AppendLine("INNER JOIN dbo.ArticuloInventario");
            SQL.AppendLine("ON " + DbSchema + ".LoteDeInventario.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania");
            SQL.AppendLine("AND " + DbSchema + ".LoteDeInventario.CodigoArticulo = ArticuloInventario.Codigo");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoLote" + InsSql.VarCharTypeForDb(30) + " = '',");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@FechaDeElaboracion" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@FechaDeVencimiento" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@Existencia" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@StatusLoteInv" + InsSql.CharTypeForDb(1) + " = '0',");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".LoteDeInventario(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            CodigoLote,");
            SQL.AppendLine("            CodigoArticulo,");
            SQL.AppendLine("            FechaDeElaboracion,");
            SQL.AppendLine("            FechaDeVencimiento,");
            SQL.AppendLine("            Existencia,");
            SQL.AppendLine("            StatusLoteInv,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @CodigoLote,");
            SQL.AppendLine("            @CodigoArticulo,");
            SQL.AppendLine("            @FechaDeElaboracion,");
            SQL.AppendLine("            @FechaDeVencimiento,");
            SQL.AppendLine("            @Existencia,");
            SQL.AppendLine("            @StatusLoteInv,");
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
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoLote" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@FechaDeElaboracion" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@FechaDeVencimiento" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@Existencia" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@StatusLoteInv" + InsSql.CharTypeForDb(1) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".LoteDeInventario WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".LoteDeInventario WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_LoteDeInventarioCanBeUpdated @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".LoteDeInventario");
            SQL.AppendLine("            SET CodigoLote = @CodigoLote,");
            SQL.AppendLine("               CodigoArticulo = @CodigoArticulo,");
            SQL.AppendLine("               FechaDeElaboracion = @FechaDeElaboracion,");
            SQL.AppendLine("               FechaDeVencimiento = @FechaDeVencimiento,");
            SQL.AppendLine("               Existencia = @Existencia,");
            SQL.AppendLine("               StatusLoteInv = @StatusLoteInv,");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".LoteDeInventario WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".LoteDeInventario WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_LoteDeInventarioCanBeDeleted @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".LoteDeInventario");
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
            SQL.AppendLine("         LoteDeInventario.ConsecutivoCompania,");
            SQL.AppendLine("         LoteDeInventario.Consecutivo,");
            SQL.AppendLine("         LoteDeInventario.CodigoLote,");
            SQL.AppendLine("         LoteDeInventario.CodigoArticulo,");
            SQL.AppendLine("         LoteDeInventario.FechaDeElaboracion,");
            SQL.AppendLine("         LoteDeInventario.FechaDeVencimiento,");
            SQL.AppendLine("         LoteDeInventario.Existencia,");
            SQL.AppendLine("         LoteDeInventario.StatusLoteInv,");
            SQL.AppendLine("         LoteDeInventario.NombreOperador,");
            SQL.AppendLine("         LoteDeInventario.FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(LoteDeInventario.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         LoteDeInventario.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".LoteDeInventario");
            SQL.AppendLine("      WHERE LoteDeInventario.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND LoteDeInventario.Consecutivo = @Consecutivo");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_LoteDeInventario_B1.CodigoLote,");
            SQL.AppendLine("      " + DbSchema + ".Gv_LoteDeInventario_B1.CodigoArticulo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_LoteDeInventario_B1.DescripcionArticulo,");            
            SQL.AppendLine("      " + DbSchema + ".Gv_LoteDeInventario_B1.FechaDeElaboracion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_LoteDeInventario_B1.FechaDeVencimiento,");
            SQL.AppendLine("      " + DbSchema + ".Gv_LoteDeInventario_B1.Existencia,");
            SQL.AppendLine("      " + DbSchema + ".Gv_LoteDeInventario_B1.StatusLoteInvStr,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_LoteDeInventario_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_LoteDeInventario_B1.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_LoteDeInventario_B1.StatusLoteInv");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_LoteDeInventario_B1");
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
            SQL.AppendLine("      " + DbSchema + ".LoteDeInventario.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".LoteDeInventario.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".LoteDeInventario.CodigoLote,");
            SQL.AppendLine("      " + DbSchema + ".LoteDeInventario.CodigoArticulo,");
            SQL.AppendLine("      " + DbSchema + ".LoteDeInventario.FechaDeElaboracion,");
            SQL.AppendLine("      " + DbSchema + ".LoteDeInventario.FechaDeVencimiento,");
            SQL.AppendLine("      " + DbSchema + ".LoteDeInventario.Existencia,");
            SQL.AppendLine("      " + DbSchema + ".LoteDeInventario.StatusLoteInv");
            //SQL.AppendLine("      ," + DbSchema + ".LoteDeInventario.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".LoteDeInventario");
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
            bool vResult = insDbo.Create(DbSchema + ".LoteDeInventario", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumStatusLoteDeInventario", LibTpvCreator.SqlViewStandardEnum(typeof(eStatusLoteDeInventario), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_LoteDeInventario_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_LoteDeInventarioINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_LoteDeInventarioUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_LoteDeInventarioDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_LoteDeInventarioGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_LoteDeInventarioSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_LoteDeInventarioGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
            insSps.Dispose();
            return vResult;
        }

        public bool InstalarTabla() {
            bool vResult = false;
            if (CrearTabla()) {
                CrearVistas();
                CrearProcedimientos();
                clsLoteDeInventarioMovimientoED insDetailLotDeInvMov = new clsLoteDeInventarioMovimientoED();
                vResult = insDetailLotDeInvMov.InstalarTabla();
            }
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = false;
            if (insDbo.Exists(DbSchema + ".LoteDeInventario", eDboType.Tabla)) {
                CrearVistas();
                CrearProcedimientos();
                vResult = new clsLoteDeInventarioMovimientoED().InstalarVistasYSps();
            }
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = false;
            LibStoredProc insSp = new LibStoredProc();
            LibViews insVista = new LibViews();
            vResult = new clsLoteDeInventarioMovimientoED().BorrarVistasYSps();
            vResult = insSp.Drop(DbSchema + ".Gp_LoteDeInventarioINS") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_LoteDeInventarioUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_LoteDeInventarioDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_LoteDeInventarioGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_LoteDeInventarioGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_LoteDeInventarioSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_LoteDeInventario_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumStatusLoteDeInventario") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsLoteDeInventarioED

} //End of namespace Galac.Saw.Dal.Inventario

