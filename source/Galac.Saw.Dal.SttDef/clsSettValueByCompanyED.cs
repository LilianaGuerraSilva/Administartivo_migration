using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Dal.SttDef {
    public class clsSettValueByCompanyED: LibED {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsSettValueByCompanyED(): base(){
            DbSchema = "Comun";
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("SettValueByCompany", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnSetValByComConsecutiv NOT NULL, ");
            SQL.AppendLine("NameSettDefinition" + InsSql.VarCharTypeForDb(50) + " CONSTRAINT nnSetValByComNameSettDe NOT NULL, ");
            SQL.AppendLine("Value" + InsSql.VarCharTypeForDb(200) + " CONSTRAINT d_SetValByComVa DEFAULT (''), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(20) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_SettValueByCompany PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, NameSettDefinition ASC)");
            SQL.AppendLine(", CONSTRAINT fk_SettValueByCompanySettDefinition FOREIGN KEY (NameSettDefinition)");
            SQL.AppendLine("REFERENCES Comun.SettDefinition(Name)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT SettValueByCompany.ConsecutivoCompania, SettValueByCompany.NameSettDefinition, SettValueByCompany.Value, SettValueByCompany.NombreOperador");
            SQL.AppendLine(", SettValueByCompany.FechaUltimaModificacion");
            SQL.AppendLine(", SettValueByCompany.fldTimeStamp, CAST(SettValueByCompany.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine(", IsSetForAllEnterprise");
            SQL.AppendLine("FROM " + DbSchema + ".SettValueByCompany");
            SQL.AppendLine("INNER JOIN Comun.SettDefinition ON  " + DbSchema + ".SettValueByCompany.NameSettDefinition = Comun.SettDefinition.Name");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NameSettDefinition" + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("@Value" + InsSql.VarCharTypeForDb(200) + " = '',");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".SettValueByCompany(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            NameSettDefinition,");
            SQL.AppendLine("            Value,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @NameSettDefinition,");
            SQL.AppendLine("            @Value,");
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
            SQL.AppendLine("@NameSettDefinition" + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("@Value" + InsSql.VarCharTypeForDb(200) + ",");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() );
          
            return SQL.ToString();
        }

        private string SqlSpUpd() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @ValidationMsg " + InsSql.VarCharTypeForDb(1500) + " --No puede ser más");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            //SQL.AppendLine("--DECLARE @CanBeChanged bit");
            SQL.AppendLine("   SET @ReturnValue = -1");
            SQL.AppendLine("   SET @ValidationMsg = ''");
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".SettValueByCompany WHERE ConsecutivoCompania = @ConsecutivoCompania AND NameSettDefinition = @NameSettDefinition)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".SettValueByCompany");
            SQL.AppendLine("            SET Value = @Value,");
            SQL.AppendLine("               NombreOperador = @NombreOperador,");
            SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
            SQL.AppendLine("            WHERE  ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND NameSettDefinition = @NameSettDefinition");
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
            SQL.AppendLine("@NameSettDefinition" + InsSql.VarCharTypeForDb(50) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".SettValueByCompany WHERE ConsecutivoCompania = @ConsecutivoCompania AND NameSettDefinition = @NameSettDefinition)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".SettValueByCompany WHERE ConsecutivoCompania = @ConsecutivoCompania AND NameSettDefinition = @NameSettDefinition");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_SettValueByCompanyCanBeDeleted @ConsecutivoCompania,@NameSettDefinition, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".SettValueByCompany");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND NameSettDefinition = @NameSettDefinition");
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
            SQL.AppendLine("@NameSettDefinition" + InsSql.VarCharTypeForDb(50));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         SettValueByCompany.ConsecutivoCompania,");
            SQL.AppendLine("         SettValueByCompany.NameSettDefinition,");
            SQL.AppendLine("         SettValueByCompany.Value,");
            SQL.AppendLine("         SettValueByCompany.NombreOperador,");
            SQL.AppendLine("         SettValueByCompany.FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(SettValueByCompany.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         SettValueByCompany.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".SettValueByCompany");
            SQL.AppendLine("             INNER JOIN Comun.Gv_SettDefinition_B1 ON " + DbSchema + ".SettValueByCompany.NameSettDefinition = Comun.Gv_SettDefinition_B1.Name");
            SQL.AppendLine("      WHERE SettValueByCompany.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND SettValueByCompany.NameSettDefinition = @NameSettDefinition");
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
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_SettValueByCompany_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_SettValueByCompany_B1.NameSettDefinition");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_SettValueByCompany_B1");
            SQL.AppendLine("      INNER JOIN Comun.Gv_SettDefinition_B1 ON  " + DbSchema + ".Gv_SettValueByCompany_B1.NameSettDefinition = Comun.Gv_SettDefinition_B1.Name");
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
            SQL.AppendLine("      " + DbSchema + ".SettValueByCompany.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".SettValueByCompany.NameSettDefinition");
            SQL.AppendLine("      FROM " + DbSchema + ".SettValueByCompany");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries
        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".SettValueByCompany", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }
        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_SettValueByCompany_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }
        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SettValueByCompanyINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SettValueByCompanyUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SettValueByCompanyDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SettValueByCompanyGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SettValueByCompanySCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SettValueByCompanyGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SettValueByCompanyINSList", SqlSpInsListParameters(), SqlSpInsList(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".SettValueByCompany", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_SettValueByCompanyINS");
            vResult = insSp.Drop(DbSchema + ".Gp_SettValueByCompanyUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_SettValueByCompanyDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_SettValueByCompanyGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_SettValueByCompanyGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_SettValueByCompanySCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_SettValueByCompany_B1") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_SettValueByCompanyINSList") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


        private string SqlSpInsListParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@XmlDataDetail" + InsSql.XmlTypeForDb() + ",");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() );
            return SQL.ToString();
        }

        private string SqlSpInsList() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SET NOCOUNT ON;");
            SQL.AppendLine("	DECLARE @ReturnValue  " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	    BEGIN");
            SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
            SQL.AppendLine("        INSERT INTO " + DbSchema + ".SettValueByCompany(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            NameSettDefinition,");
            SQL.AppendLine("            value,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("	    SELECT ");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            NameSettDefinition,");
            SQL.AppendLine("            value,");
            SQL.AppendLine("            @NombreOperador,");
            SQL.AppendLine("            @FechaUltimaModificacion ");
            SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("        NameSettDefinition" + InsSql.VarCharTypeForDb(50) + " , ");
            SQL.AppendLine("        value" + InsSql.VarCharTypeForDb(200) + " , ");
            SQL.AppendLine("        NombreOperador" + InsSql.VarCharTypeForDb(20) + ", ");
            SQL.AppendLine("        FechaUltimaModificacion" + InsSql.DateTypeForDb() + " ");
            SQL.AppendLine("	    ) AS XmlDocSettValueByCompany");
            SQL.AppendLine("	    EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("	    SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("	    RETURN @ReturnValue");
            SQL.AppendLine("	END");
            SQL.AppendLine("	ELSE");
            SQL.AppendLine("	    RETURN -1");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

    } //End of class clsSettValueByCompanyED

} //End of namespace Galac.Saw.Dal.SttDef

