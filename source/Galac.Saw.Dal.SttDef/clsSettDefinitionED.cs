using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Dal.SttDef {
    public class clsSettDefinitionED: LibED {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsSettDefinitionED(): base(){
            DbSchema = "Comun";
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("SettDefinition", DbSchema) + " ( ");
            SQL.AppendLine("Name" + InsSql.VarCharTypeForDb(50) + " CONSTRAINT nnSetDefName NOT NULL, ");
            SQL.AppendLine("Module" + InsSql.VarCharTypeForDb(50) + " CONSTRAINT d_SetDefMo DEFAULT (''), ");
            SQL.AppendLine("LevelModule" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnSetDefLevelModul NOT NULL, ");
            SQL.AppendLine("GroupName" + InsSql.VarCharTypeForDb(50) + " CONSTRAINT nnSetDefGroupName NOT NULL, ");
            SQL.AppendLine("LevelGroup" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnSetDefLevelGroup NOT NULL, ");
            SQL.AppendLine("Label" + InsSql.VarCharTypeForDb(50) + " CONSTRAINT d_SetDefLa DEFAULT (''), ");
            SQL.AppendLine("DataType" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_SetDefDaTy DEFAULT ('0'), ");
            SQL.AppendLine("Validationrules" + InsSql.VarCharTypeForDb(300) + " CONSTRAINT d_SetDefVa DEFAULT (''), ");
            SQL.AppendLine("IsSetForAllEnterprise" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnSetDefIsSetForAl NOT NULL, ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_SettDefinition PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(Name ASC)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT Name, Module, LevelModule, GroupName");
            SQL.AppendLine(", LevelGroup, Label, DataType, " + DbSchema + ".Gv_EnumTipoDeDatoParametros.StrValue AS DataTypeStr, Validationrules");
            SQL.AppendLine(", IsSetForAllEnterprise");
            SQL.AppendLine(", SettDefinition.fldTimeStamp, CAST(SettDefinition.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".SettDefinition");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoDeDatoParametros");
            SQL.AppendLine("ON " + DbSchema + ".SettDefinition.DataType COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDeDatoParametros.DbValue");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@Name" + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("@Module" + InsSql.VarCharTypeForDb(50) + " = '',");
            SQL.AppendLine("@LevelModule" + InsSql.NumericTypeForDb(10, 0) + " = 0,");
            SQL.AppendLine("@GroupName" + InsSql.VarCharTypeForDb(50) + " = '',");
            SQL.AppendLine("@LevelGroup" + InsSql.NumericTypeForDb(10, 0) + " = 0,");
            SQL.AppendLine("@Label" + InsSql.VarCharTypeForDb(50) + " = '',");
            SQL.AppendLine("@DataType" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Validationrules" + InsSql.VarCharTypeForDb(300) + " = '',");
            SQL.AppendLine("@IsSetForAllEnterprise" + InsSql.CharTypeForDb(1) + " = 'N'");
            return SQL.ToString();
        }

        private string SqlSpIns() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".SettDefinition(");
            SQL.AppendLine("            Name,");
            SQL.AppendLine("            Module,");
            SQL.AppendLine("            LevelModule,");
            SQL.AppendLine("            GroupName,");
            SQL.AppendLine("            LevelGroup,");
            SQL.AppendLine("            Label,");
            SQL.AppendLine("            DataType,");
            SQL.AppendLine("            Validationrules,");
            SQL.AppendLine("            IsSetForAllEnterprise)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @Name,");
            SQL.AppendLine("            @Module,");
            SQL.AppendLine("            @LevelModule,");
            SQL.AppendLine("            @GroupName,");
            SQL.AppendLine("            @LevelGroup,");
            SQL.AppendLine("            @Label,");
            SQL.AppendLine("            @DataType,");
            SQL.AppendLine("            @Validationrules,");
            SQL.AppendLine("            @IsSetForAllEnterprise)");
            SQL.AppendLine("            SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("        COMMIT TRAN");
            SQL.AppendLine("        RETURN @ReturnValue ");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpUpdParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@Name" + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("@Module" + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("@LevelModule" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@GroupName" + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("@LevelGroup" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Label" + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("@DataType" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Validationrules" + InsSql.VarCharTypeForDb(300) + ",");
            SQL.AppendLine("@IsSetForAllEnterprise" + InsSql.CharTypeForDb(1) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT Name FROM " + DbSchema + ".SettDefinition WHERE Name = @Name)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".SettDefinition WHERE Name = @Name");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_SettDefinitionCanBeUpdated @Name, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".SettDefinition");
            SQL.AppendLine("            SET Module = @Module,");
            SQL.AppendLine("               LevelModule = @LevelModule,");
            SQL.AppendLine("               GroupName = @GroupName,");
            SQL.AppendLine("               LevelGroup = @LevelGroup,");
            SQL.AppendLine("               Label = @Label,");
            SQL.AppendLine("               DataType = @DataType,");
            SQL.AppendLine("               Validationrules = @Validationrules,");
            SQL.AppendLine("               IsSetForAllEnterprise = @IsSetForAllEnterprise");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND Name = @Name");
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
            SQL.AppendLine("@Name" + InsSql.VarCharTypeForDb(50) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT Name FROM " + DbSchema + ".SettDefinition WHERE Name = @Name)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".SettDefinition WHERE Name = @Name");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_SettDefinitionCanBeDeleted @Name, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".SettDefinition");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND Name = @Name");
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
            SQL.AppendLine("@Name" + InsSql.VarCharTypeForDb(50));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         Name,");
            SQL.AppendLine("         Module,");
            SQL.AppendLine("         LevelModule,");
            SQL.AppendLine("         GroupName,");
            SQL.AppendLine("         LevelGroup,");
            SQL.AppendLine("         Label,");
            SQL.AppendLine("         DataType,");
            SQL.AppendLine("         Validationrules,");
            SQL.AppendLine("         IsSetForAllEnterprise,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".SettDefinition");
            SQL.AppendLine("      WHERE SettDefinition.Name = @Name");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSearchParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@SQLWhere" + InsSql.VarCharTypeForDb(2000) + " = null,");
            SQL.AppendLine("@SQLOrderBy" + InsSql.VarCharTypeForDb(500) + " = null,");
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + " = null,");
            SQL.AppendLine("@UseTopClausule" + InsSql.VarCharTypeForDb(1) + " = 'N'");
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
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      Name,");
            SQL.AppendLine("      GroupName");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_SettDefinition_B1");
            SQL.AppendLine("'   IF (NOT @SQLWhere IS NULL) AND (@SQLWhere <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' WHERE ' + @SQLWhere");
            SQL.AppendLine("   IF (NOT @SQLOrderBy IS NULL) AND (@SQLOrderBy <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' ORDER BY ' + @SQLOrderBy");
            SQL.AppendLine("   EXEC(@strSQL)");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpGetFKParameters() {
            return "";
        }

        private string SqlSpGetFK() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("      " + DbSchema + ".SettDefinition.Name,");
            SQL.AppendLine("      " + DbSchema + ".SettDefinition.GroupName");
            SQL.AppendLine("      FROM " + DbSchema + ".SettDefinition");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries
        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".SettDefinition", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }
        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDeDatoParametros", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDeDatoParametros), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_SettDefinition_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }
        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SettDefinitionINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SettDefinitionUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SettDefinitionDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SettDefinitionGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SettDefinitionSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SettDefinitionGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SettDefinitionINST", SqlSpInstParameters(), SqlSpInst(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".SettDefinition", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_SettDefinitionINS");
            vResult = insSp.Drop(DbSchema + ".Gp_SettDefinitionUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_SettDefinitionDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_SettDefinitionGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_SettDefinitionGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_SettDefinitionSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_SettDefinition_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDeDatoParametros") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_SettDefinitionINST") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados

        private string SqlSpInstParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@Name" + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("@Module" + InsSql.VarCharTypeForDb(50) + " = '',");
            SQL.AppendLine("@LevelModule" + InsSql.NumericTypeForDb(10, 0) + " = 0,");
            SQL.AppendLine("@GroupName" + InsSql.VarCharTypeForDb(50) + " = '',");
            SQL.AppendLine("@LevelGroup" + InsSql.NumericTypeForDb(10, 0) + " = 0,");
            SQL.AppendLine("@Label" + InsSql.VarCharTypeForDb(50) + " = '',");
            SQL.AppendLine("@Datatype" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Validationrules" + InsSql.VarCharTypeForDb(300) + " = '' , ");
            SQL.AppendLine("@IsSetForAllEnterprise" + InsSql.CharTypeForDb(1)  );
            return SQL.ToString();
        }

        private string SqlSpInst() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".SettDefinition");
            SQL.AppendLine("            SET Module = @Module,");
            SQL.AppendLine("               LevelModule = @LevelModule,");
            SQL.AppendLine("               GroupName = @GroupName,");
            SQL.AppendLine("               LevelGroup = @LevelGroup,");
            SQL.AppendLine("               label = @Label,");
            SQL.AppendLine("               Datatype = @Datatype,");
            SQL.AppendLine("               Validationrules = @Validationrules,");
            SQL.AppendLine("               IsSetForAllEnterprise = @IsSetForAllEnterprise");
            SQL.AppendLine("            WHERE  Name = @Name");
            SQL.AppendLine("	     IF @@ROWCOUNT = 0");
            SQL.AppendLine("	      BEGIN                                ");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".SettDefinition(");
            SQL.AppendLine("            Name,");
            SQL.AppendLine("            Module,");
            SQL.AppendLine("            LevelModule,");
            SQL.AppendLine("            GroupName,");
            SQL.AppendLine("            levelGroup,");
            SQL.AppendLine("            Label,");
            SQL.AppendLine("            Datatype,");
            SQL.AppendLine("            Validationrules, ");
            SQL.AppendLine("            IsSetForAllEnterprise ) ");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @Name,");
            SQL.AppendLine("            @Module,");
            SQL.AppendLine("            @LevelModule,");
            SQL.AppendLine("            @GroupName,");
            SQL.AppendLine("            @LevelGroup,");
            SQL.AppendLine("            @Label,");
            SQL.AppendLine("            @Datatype,");
            SQL.AppendLine("            @Validationrules,");
            SQL.AppendLine("            @IsSetForAllEnterprise)");
            SQL.AppendLine(" 	           SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine(" 	           END                          ");
            SQL.AppendLine(" 	      ELSE                              ");
            SQL.AppendLine(" 	           SET @ReturnValue = @@ROWCOUNT ");
            SQL.AppendLine("   COMMIT TRAN                              ");
            SQL.AppendLine("   RETURN @ReturnValue                      ");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
    } //End of class clsSettDefinitionED

} //End of namespace Galac.Saw.Dal.SttDef

