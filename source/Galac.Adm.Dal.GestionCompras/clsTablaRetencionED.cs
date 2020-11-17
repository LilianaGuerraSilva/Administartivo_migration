using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Dal.GestionCompras {
    public class clsTablaRetencionED: LibED {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsTablaRetencionED(): base(){
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("TablaRetencion", DbSchema) + " ( ");
            SQL.AppendLine("TipoDePersona" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnTabRetTipoDePers NOT NULL, ");
            SQL.AppendLine("Codigo" + InsSql.VarCharTypeForDb(6) + " CONSTRAINT nnTabRetCodigo NOT NULL, ");
            SQL.AppendLine("CodigoSeniat" + InsSql.VarCharTypeForDb(3) + " CONSTRAINT d_TabRetCoSe DEFAULT (''), ");
            SQL.AppendLine("TipoDePago" + InsSql.VarCharTypeForDb(50) + " CONSTRAINT d_TabRetTiDePa DEFAULT (''), ");
            SQL.AppendLine("Comentarios" + InsSql.VarCharTypeForDb(255) + " CONSTRAINT d_TabRetCo DEFAULT (''), ");
            SQL.AppendLine("BaseImponible" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_TabRetBaIm DEFAULT (0), ");
            SQL.AppendLine("Tarifa" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_TabRetTa DEFAULT (0), ");
            SQL.AppendLine("ParaPagosMayoresDe" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_TabRetPaPaMaDe DEFAULT (0), ");
            SQL.AppendLine("FechaAplicacion" + InsSql.DateTypeForDb() + " CONSTRAINT d_TabRetFeAp DEFAULT (''), ");
            SQL.AppendLine("Sustraendo" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_TabRetSu DEFAULT (0), ");
            SQL.AppendLine("AcumulaParaPJND" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnTabRetAcumulaPar NOT NULL, ");
            SQL.AppendLine("SecuencialDePlantilla" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT d_TabRetSeDePl DEFAULT (''), ");
            SQL.AppendLine("CodigoMoneda" + InsSql.VarCharTypeForDb(4) + " CONSTRAINT d_TabRetCoMo DEFAULT (''), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_TablaRetencion PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(TipoDePersona ASC, Codigo ASC)");
            SQL.AppendLine(", CONSTRAINT fk_TablaRetencionMoneda FOREIGN KEY (CodigoMoneda)");
            SQL.AppendLine("REFERENCES Comun.Moneda(Codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT TablaRetencion.TipoDePersona, " + DbSchema + ".Gv_EnumTipodePersonaRetencion.StrValue AS TipoDePersonaStr, TablaRetencion.Codigo, TablaRetencion.CodigoSeniat, TablaRetencion.TipoDePago");
            SQL.AppendLine(", TablaRetencion.Comentarios, TablaRetencion.BaseImponible, TablaRetencion.Tarifa, TablaRetencion.ParaPagosMayoresDe");
            SQL.AppendLine(", TablaRetencion.FechaAplicacion, TablaRetencion.Sustraendo, TablaRetencion.AcumulaParaPJND, TablaRetencion.SecuencialDePlantilla");
            SQL.AppendLine(", TablaRetencion.CodigoMoneda");
            SQL.AppendLine(", TablaRetencion.fldTimeStamp, CAST(TablaRetencion.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + "dbo" + ".TablaRetencion");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipodePersonaRetencion");
            SQL.AppendLine("ON " + "dbo" + ".TablaRetencion.TipoDePersona COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipodePersonaRetencion.DbValue");
            SQL.AppendLine("INNER JOIN dbo.Moneda ON  " + "dbo" + ".TablaRetencion.CodigoMoneda = dbo.Moneda.Codigo");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@TipoDePersona" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(6) + ",");
            SQL.AppendLine("@CodigoSeniat" + InsSql.VarCharTypeForDb(3) + " = '',");
            SQL.AppendLine("@TipoDePago" + InsSql.VarCharTypeForDb(50) + " = '',");
            SQL.AppendLine("@Comentarios" + InsSql.VarCharTypeForDb(255) + " = '',");
            SQL.AppendLine("@BaseImponible" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@Tarifa" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@ParaPagosMayoresDe" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@FechaAplicacion" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@Sustraendo" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@AcumulaParaPJND" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@SecuencialDePlantilla" + InsSql.VarCharTypeForDb(5) + " = '',");
            SQL.AppendLine("@CodigoMoneda" + InsSql.VarCharTypeForDb(4) + "");
            return SQL.ToString();
        }

        private string SqlSpIns() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SET DATEFORMAT @DateFormat");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".TablaRetencion(");
            SQL.AppendLine("            TipoDePersona,");
            SQL.AppendLine("            Codigo,");
            SQL.AppendLine("            CodigoSeniat,");
            SQL.AppendLine("            TipoDePago,");
            SQL.AppendLine("            Comentarios,");
            SQL.AppendLine("            BaseImponible,");
            SQL.AppendLine("            Tarifa,");
            SQL.AppendLine("            ParaPagosMayoresDe,");
            SQL.AppendLine("            FechaAplicacion,");
            SQL.AppendLine("            Sustraendo,");
            SQL.AppendLine("            AcumulaParaPJND,");
            SQL.AppendLine("            SecuencialDePlantilla,");
            SQL.AppendLine("            CodigoMoneda)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @TipoDePersona,");
            SQL.AppendLine("            @Codigo,");
            SQL.AppendLine("            @CodigoSeniat,");
            SQL.AppendLine("            @TipoDePago,");
            SQL.AppendLine("            @Comentarios,");
            SQL.AppendLine("            @BaseImponible,");
            SQL.AppendLine("            @Tarifa,");
            SQL.AppendLine("            @ParaPagosMayoresDe,");
            SQL.AppendLine("            @FechaAplicacion,");
            SQL.AppendLine("            @Sustraendo,");
            SQL.AppendLine("            @AcumulaParaPJND,");
            SQL.AppendLine("            @SecuencialDePlantilla,");
            SQL.AppendLine("            @CodigoMoneda)");
            SQL.AppendLine("            SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("        COMMIT TRAN");
            SQL.AppendLine("        RETURN @ReturnValue ");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpUpdParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@TipoDePersona" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(6) + ",");
            SQL.AppendLine("@CodigoSeniat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@TipoDePago" + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("@Comentarios" + InsSql.VarCharTypeForDb(255) + ",");
            SQL.AppendLine("@BaseImponible" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@Tarifa" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@ParaPagosMayoresDe" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@FechaAplicacion" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@Sustraendo" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@AcumulaParaPJND" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@SecuencialDePlantilla" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@CodigoMoneda" + InsSql.VarCharTypeForDb(4) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT TipoDePersona FROM " + DbSchema + ".TablaRetencion WHERE TipoDePersona = @TipoDePersona AND Codigo = @Codigo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".TablaRetencion WHERE TipoDePersona = @TipoDePersona AND Codigo = @Codigo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_TablaRetencionCanBeUpdated @TipoDePersona,@Codigo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".TablaRetencion");
            SQL.AppendLine("            SET CodigoSeniat = @CodigoSeniat,");
            SQL.AppendLine("               TipoDePago = @TipoDePago,");
            SQL.AppendLine("               Comentarios = @Comentarios,");
            SQL.AppendLine("               BaseImponible = @BaseImponible,");
            SQL.AppendLine("               Tarifa = @Tarifa,");
            SQL.AppendLine("               ParaPagosMayoresDe = @ParaPagosMayoresDe,");
            SQL.AppendLine("               FechaAplicacion = @FechaAplicacion,");
            SQL.AppendLine("               Sustraendo = @Sustraendo,");
            SQL.AppendLine("               AcumulaParaPJND = @AcumulaParaPJND,");
            SQL.AppendLine("               SecuencialDePlantilla = @SecuencialDePlantilla,");
            SQL.AppendLine("               CodigoMoneda = @CodigoMoneda");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND TipoDePersona = @TipoDePersona");
            SQL.AppendLine("               AND Codigo = @Codigo");
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
            SQL.AppendLine("@TipoDePersona" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(6) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT TipoDePersona FROM " + DbSchema + ".TablaRetencion WHERE TipoDePersona = @TipoDePersona AND Codigo = @Codigo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".TablaRetencion WHERE TipoDePersona = @TipoDePersona AND Codigo = @Codigo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_TablaRetencionCanBeDeleted @TipoDePersona,@Codigo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".TablaRetencion");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND TipoDePersona = @TipoDePersona");
            SQL.AppendLine("               AND Codigo = @Codigo");
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
            SQL.AppendLine("@TipoDePersona" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(6));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         " + DbSchema + ".Gv_TablaRetencion_B1.TipoDePersona,");
            SQL.AppendLine("         " + DbSchema + ".Gv_TablaRetencion_B1.Codigo,");
            SQL.AppendLine("         " + DbSchema + ".Gv_TablaRetencion_B1.CodigoSeniat,");
            SQL.AppendLine("         " + DbSchema + ".Gv_TablaRetencion_B1.TipoDePago,");
            SQL.AppendLine("         " + DbSchema + ".Gv_TablaRetencion_B1.Comentarios,");
            SQL.AppendLine("         " + DbSchema + ".Gv_TablaRetencion_B1.BaseImponible,");
            SQL.AppendLine("         " + DbSchema + ".Gv_TablaRetencion_B1.Tarifa,");
            SQL.AppendLine("         " + DbSchema + ".Gv_TablaRetencion_B1.ParaPagosMayoresDe,");
            SQL.AppendLine("         " + DbSchema + ".Gv_TablaRetencion_B1.FechaAplicacion,");
            SQL.AppendLine("         " + DbSchema + ".Gv_TablaRetencion_B1.Sustraendo,");
            SQL.AppendLine("         " + DbSchema + ".Gv_TablaRetencion_B1.AcumulaParaPJND,");
            SQL.AppendLine("         " + DbSchema + ".Gv_TablaRetencion_B1.SecuencialDePlantilla,");
            SQL.AppendLine("         " + DbSchema + ".Gv_TablaRetencion_B1.CodigoMoneda,");
            SQL.AppendLine("         CAST(" + DbSchema + ".Gv_TablaRetencion_B1.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         " + DbSchema + ".Gv_TablaRetencion_B1.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_TablaRetencion_B1");
            SQL.AppendLine("             INNER JOIN dbo.Moneda ON " + DbSchema + ".Gv_TablaRetencion_B1.CodigoMoneda = dbo.Moneda.Codigo");
            SQL.AppendLine("      WHERE " + DbSchema + ".Gv_TablaRetencion_B1.TipoDePersona = @TipoDePersona");
            SQL.AppendLine("         AND " + DbSchema + ".Gv_TablaRetencion_B1.Codigo = @Codigo");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_TablaRetencion_B1.TipoDePersonaStr,");
            SQL.AppendLine("      " + DbSchema + ".Gv_TablaRetencion_B1.Codigo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_TablaRetencion_B1.TipoDePago,");
            SQL.AppendLine("      " + DbSchema + ".Gv_TablaRetencion_B1.BaseImponible,");
            SQL.AppendLine("      " + DbSchema + ".Gv_TablaRetencion_B1.Tarifa,");
            SQL.AppendLine("      " + DbSchema + ".Gv_TablaRetencion_B1.ParaPagosMayoresDe,");
            SQL.AppendLine("      " + DbSchema + ".Gv_TablaRetencion_B1.Sustraendo,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_TablaRetencion_B1.TipoDePersona");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_TablaRetencion_B1");
            SQL.AppendLine("      INNER JOIN dbo.Moneda ON  " + DbSchema + ".Gv_TablaRetencion_B1.CodigoMoneda = dbo.Moneda.Codigo");
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
            SQL.AppendLine("      " + DbSchema + ".TablaRetencion.TipoDePersona,");
            SQL.AppendLine("      " + DbSchema + ".TablaRetencion.Codigo,");
            SQL.AppendLine("      " + DbSchema + ".TablaRetencion.TipoDePago,");
            SQL.AppendLine("      " + DbSchema + ".TablaRetencion.CodigoMoneda");
            SQL.AppendLine("      ," + DbSchema + ".TablaRetencion.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".TablaRetencion");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".TablaRetencion", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            //vResult = insVistas.Create(DbSchema + ".Gv_EnumTipodePersonaRetencion", LibTpvCreator.SqlViewStandardEnum(typeof(eTipodePersonaRetencion), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_TablaRetencion_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            //vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_TablaRetencionINS", SqlSpInsParameters(), SqlSpIns(), true);
            //vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_TablaRetencionUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            //vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_TablaRetencionDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_TablaRetencionGET", SqlSpGetParameters(), SqlSpGet(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_TablaRetencionSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            //vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_TablaRetencionGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
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
            //if (insDbo.Exists(DbSchema + ".TablaRetencion", eDboType.Tabla)) {
                CrearVistas();
                CrearProcedimientos();
                vResult = true;
            //}
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = false;
            LibStoredProc insSp = new LibStoredProc();
            LibViews insVista = new LibViews();
            //vResult = insSp.Drop(DbSchema + ".Gp_TablaRetencionINS");
            //vResult = insSp.Drop(DbSchema + ".Gp_TablaRetencionUPD") && vResult;
            //vResult = insSp.Drop(DbSchema + ".Gp_TablaRetencionDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_TablaRetencionGET");
            //vResult = insSp.Drop(DbSchema + ".Gp_TablaRetencionGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_TablaRetencionSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_TablaRetencion_B1") && vResult;
            //vResult = insVista.Drop(DbSchema + ".Gv_EnumTipodePersonaRetencion") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsTablaRetencionED

} //End of namespace Galac.Adm.Dal.GestionCompras

