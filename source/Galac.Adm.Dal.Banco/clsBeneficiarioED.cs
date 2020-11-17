using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Dal.Banco {
    [LibMefDalComponentMetadata(typeof(clsBeneficiarioED))]
    public class clsBeneficiarioED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsBeneficiarioED(): base(){
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
            get { return "Beneficiario"; }
        }
        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("Beneficiario", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnBenConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnBenConsecutiv NOT NULL, ");
            SQL.AppendLine("Codigo" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT nnBenCodigo NOT NULL, ");
            SQL.AppendLine("NumeroRIF" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT nnBenNumeroRIF NOT NULL, ");
            SQL.AppendLine("NombreBeneficiario" + InsSql.VarCharTypeForDb(80) + " CONSTRAINT nnBenNombreBene NOT NULL, ");
            SQL.AppendLine("Origen" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_BenOr DEFAULT ('0'), ");
            SQL.AppendLine("TipoDeBeneficiario" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnBenTipoDeBene NOT NULL, ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(20) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_Beneficiario PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Consecutivo ASC)");
            SQL.AppendLine(",CONSTRAINT u_Benniaigo UNIQUE NONCLUSTERED (ConsecutivoCompania,Codigo)");
            SQL.AppendLine(",CONSTRAINT u_BenniaRIF UNIQUE NONCLUSTERED (ConsecutivoCompania,NumeroRIF)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT ConsecutivoCompania, Consecutivo, Codigo, NumeroRIF");
            SQL.AppendLine(", NombreBeneficiario, Origen, " + DbSchema + ".Gv_EnumOrigenBeneficiario.StrValue AS OrigenStr, TipoDeBeneficiario, " + DbSchema + ".Gv_EnumTipoDeBeneficiario.StrValue AS TipoDeBeneficiarioStr, NombreOperador");
            SQL.AppendLine(", FechaUltimaModificacion");
            SQL.AppendLine(", Beneficiario.fldTimeStamp, CAST(Beneficiario.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".Beneficiario");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumOrigenBeneficiario");
            SQL.AppendLine("ON " + DbSchema + ".Beneficiario.Origen COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumOrigenBeneficiario.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoDeBeneficiario");
            SQL.AppendLine("ON " + DbSchema + ".Beneficiario.TipoDeBeneficiario COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDeBeneficiario.DbValue");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@NumeroRIF" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@NombreBeneficiario" + InsSql.VarCharTypeForDb(80) + " = '',");
            SQL.AppendLine("@Origen" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@TipoDeBeneficiario" + InsSql.CharTypeForDb(1) + " = '0',");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".Beneficiario(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            Codigo,");
            SQL.AppendLine("            NumeroRIF,");
            SQL.AppendLine("            NombreBeneficiario,");
            SQL.AppendLine("            Origen,");
            SQL.AppendLine("            TipoDeBeneficiario,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @Codigo,");
            SQL.AppendLine("            @NumeroRIF,");
            SQL.AppendLine("            @NombreBeneficiario,");
            SQL.AppendLine("            @Origen,");
            SQL.AppendLine("            @TipoDeBeneficiario,");
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
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@NumeroRIF" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@NombreBeneficiario" + InsSql.VarCharTypeForDb(80) + ",");
            SQL.AppendLine("@Origen" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@TipoDeBeneficiario" + InsSql.CharTypeForDb(1) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Beneficiario WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Beneficiario WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_BeneficiarioCanBeUpdated @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".Beneficiario");
            SQL.AppendLine("            SET Codigo = @Codigo,");
            SQL.AppendLine("               NumeroRIF = @NumeroRIF,");
            SQL.AppendLine("               NombreBeneficiario = @NombreBeneficiario,");
            SQL.AppendLine("               Origen = @Origen,");
            SQL.AppendLine("               TipoDeBeneficiario = @TipoDeBeneficiario,");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Beneficiario WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Beneficiario WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_BeneficiarioCanBeDeleted @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".Beneficiario");
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
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         Consecutivo,");
            SQL.AppendLine("         Codigo,");
            SQL.AppendLine("         NumeroRIF,");
            SQL.AppendLine("         NombreBeneficiario,");
            SQL.AppendLine("         Origen,");
            SQL.AppendLine("         TipoDeBeneficiario,");
            SQL.AppendLine("         NombreOperador,");
            SQL.AppendLine("         FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".Beneficiario");
            SQL.AppendLine("      WHERE Beneficiario.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND Beneficiario.Consecutivo = @Consecutivo");
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
            SQL.AppendLine("      Codigo,");
            SQL.AppendLine("      NumeroRIF,");
            SQL.AppendLine("      NombreBeneficiario,");
            SQL.AppendLine("      TipoDeBeneficiarioStr,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      ConsecutivoCompania,");
            SQL.AppendLine("      Consecutivo,");
            SQL.AppendLine("      TipoDeBeneficiario");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Beneficiario_B1");
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
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGetFK() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("      " + DbSchema + ".Beneficiario.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Beneficiario.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Beneficiario.Codigo,");
            SQL.AppendLine("      " + DbSchema + ".Beneficiario.NumeroRIF,");
            SQL.AppendLine("      " + DbSchema + ".Beneficiario.NombreBeneficiario,");
            SQL.AppendLine("      " + DbSchema + ".Beneficiario.TipoDeBeneficiario");
            SQL.AppendLine("      FROM " + DbSchema + ".Beneficiario");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        private string SqlSpInstParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@NumeroRIF" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@NombreBeneficiario" + InsSql.VarCharTypeForDb(80) + ",");
            SQL.AppendLine("@Origen" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@TipoDeBeneficiario" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb());
            return SQL.ToString();
        }
       private string SqlSpInst() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".Beneficiario");
            SQL.AppendLine("            SET Codigo = @Codigo,");
            SQL.AppendLine("               NumeroRIF = @NumeroRIF,");
            SQL.AppendLine("               NombreBeneficiario = @NombreBeneficiario,");
            SQL.AppendLine("               Origen = @Origen,");
            SQL.AppendLine("               TipoDeBeneficiario = @TipoDeBeneficiario,");
            SQL.AppendLine("               NombreOperador = @NombreOperador,");
            SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
            SQL.AppendLine("               WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND Consecutivo = @Consecutivo");
            SQL.AppendLine("	IF @@ROWCOUNT = 0");
            SQL.AppendLine("        INSERT INTO " + DbSchema + ".Beneficiario(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            Codigo,");
            SQL.AppendLine("            NumeroRIF,");
            SQL.AppendLine("            NombreBeneficiario,");
            SQL.AppendLine("            Origen,");
            SQL.AppendLine("            TipoDeBeneficiario,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("        VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @Codigo,");
            SQL.AppendLine("            @NumeroRIF,");
            SQL.AppendLine("            @NombreBeneficiario,");
            SQL.AppendLine("            @Origen,");
            SQL.AppendLine("            @TipoDeBeneficiario,");
            SQL.AppendLine("            @NombreOperador,");
            SQL.AppendLine("            @FechaUltimaModificacion)");
            SQL.AppendLine(" 	IF @@ERROR = 0");
            SQL.AppendLine(" 		COMMIT TRAN");
            SQL.AppendLine(" 	ELSE");
            SQL.AppendLine(" 		ROLLBACK");
            SQL.AppendLine("END ");
            return SQL.ToString();
        }
        #endregion //Queries
        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".Beneficiario", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }
        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumOrigenBeneficiario", LibTpvCreator.SqlViewStandardEnum(typeof(eOrigenBeneficiario), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDeBeneficiario", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDeBeneficiario), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_Beneficiario_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }
        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_BeneficiarioINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_BeneficiarioUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_BeneficiarioDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_BeneficiarioGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_BeneficiarioSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_BeneficiarioGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_BeneficiarioINST", SqlSpInstParameters(),SqlSpInst(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_BeneficiarioSELECTList", SqlSpSELECTListParameters(), SqlSpSELECTList(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_BeneficiarioInsIntegracion", SqlSpInsIntegracionParameters(), SqlSpInsIntegracion(),true  ) && vResult;
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
            if (insDbo.Exists(DbSchema + ".Beneficiario", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_BeneficiarioINS");
            vResult = insSp.Drop(DbSchema + ".Gp_BeneficiarioUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_BeneficiarioDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_BeneficiarioGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_BeneficiarioGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_BeneficiarioSCH") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_BeneficiarioINST") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_Beneficiario_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumOrigenBeneficiario") && vResult;
			vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDeBeneficiario") && vResult;
			vResult = insSp.Drop(DbSchema + ".Gp_BeneficiarioSELECTList") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_BeneficiarioInsIntegracion") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }



        private string SqlSpSELECTListParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@XmlDataList" + InsSql.XmlTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpSELECTList() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SET NOCOUNT ON;");
            SQL.AppendLine("	DECLARE @ReturnValue  " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	    BEGIN");
            SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataList");
            SQL.AppendLine("	    SELECT ");
            SQL.AppendLine("         Beneficiario.ConsecutivoCompania,");
            SQL.AppendLine("         Beneficiario.Consecutivo,");
            SQL.AppendLine("         Beneficiario.Codigo,");
            SQL.AppendLine("         Beneficiario.NumeroRIF,");
            SQL.AppendLine("         XmlDocListOfBeneficiario.NombreBeneficiario,");
            SQL.AppendLine("         Beneficiario.Origen,");
            SQL.AppendLine("         Beneficiario.TipoDeBeneficiario,");
            SQL.AppendLine("         Beneficiario.NombreOperador,");
            SQL.AppendLine("         Beneficiario.FechaUltimaModificacion ");
            SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("          ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("          CedulaDeIdentidad" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("          NombreBeneficiario" + InsSql.VarCharTypeForDb(80)+ ") AS XmlDocListOfBeneficiario");
            SQL.AppendLine("          INNER JOIN "+  DbSchema + ".Beneficiario AS Beneficiario ON   ");
            SQL.AppendLine("          Beneficiario.NumeroRIF = XmlDocListOfBeneficiario.CedulaDeIdentidad  AND ");
            SQL.AppendLine("          Beneficiario.ConsecutivoCompania = @ConsecutivoCompania  ");
            SQL.AppendLine("	    EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("	    SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("	    RETURN @ReturnValue");
            SQL.AppendLine("	END");
            SQL.AppendLine("	ELSE");
            SQL.AppendLine("	    RETURN -1");
            SQL.AppendLine("END");
            return SQL.ToString();
        }


        #endregion //Metodos Generados

        private string SqlSpInsIntegracionParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@NumeroRIF" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@NombreBeneficiario" + InsSql.VarCharTypeForDb(80) + " = '',");
            SQL.AppendLine("@Origen" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@TipoDeBeneficiario" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + " = '01/01/1900'");
            return SQL.ToString();
        }


        private string SqlSpInsIntegracion() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("   BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".Beneficiario");
            SQL.AppendLine("            SET Codigo = @Codigo,");
            SQL.AppendLine("               NumeroRIF = @NumeroRIF,");
            SQL.AppendLine("               NombreBeneficiario = @NombreBeneficiario,");
            SQL.AppendLine("               Origen = @Origen,");
            SQL.AppendLine("               TipoDeBeneficiario = @TipoDeBeneficiario,");
            SQL.AppendLine("               NombreOperador = @NombreOperador,");
            SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
            SQL.AppendLine("            WHERE  ");
            SQL.AppendLine("               ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND  NumeroRIF = @NumeroRIF ");
            SQL.AppendLine("	     IF @@ROWCOUNT = 0");
            SQL.AppendLine("	        BEGIN                                                       ");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".Beneficiario(");
            SQL.AppendLine("                   ConsecutivoCompania,");
            SQL.AppendLine("                   Consecutivo,");
            SQL.AppendLine("                   Codigo,");
            SQL.AppendLine("                   NumeroRIF,");
            SQL.AppendLine("                   NombreBeneficiario,");
            SQL.AppendLine("                   Origen,");
            SQL.AppendLine("                   TipoDeBeneficiario,");
            SQL.AppendLine("                   NombreOperador,");
            SQL.AppendLine("                   FechaUltimaModificacion)");
            SQL.AppendLine("                  VALUES(");
            SQL.AppendLine("                    @ConsecutivoCompania,");
            SQL.AppendLine("                    @Consecutivo,");
            SQL.AppendLine("                    @Codigo,");
            SQL.AppendLine("                    @NumeroRIF,");
            SQL.AppendLine("                    @NombreBeneficiario,");
            SQL.AppendLine("                    @Origen,");
            SQL.AppendLine("                    @TipoDeBeneficiario,");
            SQL.AppendLine("                    @NombreOperador,");
            SQL.AppendLine("                    @FechaUltimaModificacion)");
            SQL.AppendLine(" 	           SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine(" 	           END                          ");
            SQL.AppendLine(" 	      ELSE                              ");
            SQL.AppendLine(" 	           SET @ReturnValue = 1         ");
            SQL.AppendLine("   COMMIT TRAN                               ");
            SQL.AppendLine("   RETURN @ReturnValue                      ");
            SQL.AppendLine("   END");
            return SQL.ToString();
          
        }

    } //End of class clsBeneficiarioED

} //End of namespace Galac.Adm.Dal.Banco

