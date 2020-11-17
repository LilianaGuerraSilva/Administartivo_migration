using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.CAnticipo;

namespace Galac.Adm.Dal.CAnticipo {
    [LibMefDalComponentMetadata(typeof(clsAnticipoCobradoED))]
    public class clsAnticipoCobradoED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsAnticipoCobradoED(): base(){
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
            get { return "AnticipoCobrado"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("AnticipoCobrado", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnAntCobConsecutiv NOT NULL, ");
            SQL.AppendLine("NumeroCobranza" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT nnAntCobNumeroCobr NOT NULL, ");
            SQL.AppendLine("Secuencial" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnAntCobSecuencial NOT NULL, ");
            SQL.AppendLine("ConsecutivoAnticipoUsado" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnAntCobConsecutiv NOT NULL, ");
            SQL.AppendLine("NumeroAnticipo" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_AntCobNuAn DEFAULT (''), ");
            SQL.AppendLine("MontoOriginal" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_AntCobMoOr DEFAULT (0), ");
            SQL.AppendLine("MontoRestanteAlDia" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_AntCobMoReAlDi DEFAULT (0), ");
            SQL.AppendLine("SimboloMoneda" + InsSql.VarCharTypeForDb(4) + " CONSTRAINT d_AntCobSiMo DEFAULT (''), ");
            SQL.AppendLine("CodigoMoneda" + InsSql.VarCharTypeForDb(4) + " CONSTRAINT d_AntCobCoMo DEFAULT (''), ");
            SQL.AppendLine("MontoTotalDelAnticipo" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_AntCobMoToDeAn DEFAULT (0), ");
            SQL.AppendLine("MontoAplicado" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_AntCobMoAp DEFAULT (0), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_AnticipoCobrado PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, NumeroCobranza ASC, Secuencial ASC)");
            SQL.AppendLine(", CONSTRAINT fk_AnticipoCobradoAnticipo FOREIGN KEY (ConsecutivoCompania, ConsecutivoAnticipoUsado)");
            SQL.AppendLine("REFERENCES dbo.Anticipo(ConsecutivoCompania, ConsecutivoAnticipo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_AnticipoCobradoAnticipo FOREIGN KEY (ConsecutivoCompania, NumeroAnticipo)");
            SQL.AppendLine("REFERENCES dbo.Anticipo(ConsecutivoCompania, Numero)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_AnticipoCobradoMoneda FOREIGN KEY (CodigoMoneda)");
            SQL.AppendLine("REFERENCES dbo.Moneda(Codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT AnticipoCobrado.ConsecutivoCompania, AnticipoCobrado.NumeroCobranza, AnticipoCobrado.Secuencial, AnticipoCobrado.ConsecutivoAnticipoUsado");
            SQL.AppendLine(", AnticipoCobrado.NumeroAnticipo, AnticipoCobrado.MontoOriginal, AnticipoCobrado.MontoRestanteAlDia, AnticipoCobrado.SimboloMoneda");
            SQL.AppendLine(", AnticipoCobrado.CodigoMoneda, AnticipoCobrado.MontoTotalDelAnticipo, AnticipoCobrado.MontoAplicado");
            SQL.AppendLine(", AnticipoCobrado.fldTimeStamp, CAST(AnticipoCobrado.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".AnticipoCobrado");
            SQL.AppendLine("INNER JOIN dbo.Anticipo ON  " + DbSchema + ".AnticipoCobrado.ConsecutivoAnticipoUsado = dbo.Anticipo.ConsecutivoAnticipo");
            SQL.AppendLine("      AND " + DbSchema + ".AnticipoCobrado.ConsecutivoCompania = dbo.Anticipo.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN dbo.Anticipo as Anticipos ON  " + DbSchema + ".AnticipoCobrado.NumeroAnticipo = Anticipos.Numero");
            SQL.AppendLine("      AND " + DbSchema + ".AnticipoCobrado.ConsecutivoCompania = dbo.Anticipo.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN dbo.Moneda ON  " + DbSchema + ".AnticipoCobrado.CodigoMoneda = dbo.Moneda.Codigo");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroCobranza" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@Secuencial" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoAnticipoUsado" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroAnticipo" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@MontoOriginal" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoRestanteAlDia" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@SimboloMoneda" + InsSql.VarCharTypeForDb(4) + " = '',");
            SQL.AppendLine("@CodigoMoneda" + InsSql.VarCharTypeForDb(4) + ",");
            SQL.AppendLine("@MontoTotalDelAnticipo" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoAplicado" + InsSql.DecimalTypeForDb(25, 4) + " = 0");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".AnticipoCobrado(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            NumeroCobranza,");
            SQL.AppendLine("            Secuencial,");
            SQL.AppendLine("            ConsecutivoAnticipoUsado,");
            SQL.AppendLine("            NumeroAnticipo,");
            SQL.AppendLine("            MontoOriginal,");
            SQL.AppendLine("            MontoRestanteAlDia,");
            SQL.AppendLine("            SimboloMoneda,");
            SQL.AppendLine("            CodigoMoneda,");
            SQL.AppendLine("            MontoTotalDelAnticipo,");
            SQL.AppendLine("            MontoAplicado)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @NumeroCobranza,");
            SQL.AppendLine("            @Secuencial,");
            SQL.AppendLine("            @ConsecutivoAnticipoUsado,");
            SQL.AppendLine("            @NumeroAnticipo,");
            SQL.AppendLine("            @MontoOriginal,");
            SQL.AppendLine("            @MontoRestanteAlDia,");
            SQL.AppendLine("            @SimboloMoneda,");
            SQL.AppendLine("            @CodigoMoneda,");
            SQL.AppendLine("            @MontoTotalDelAnticipo,");
            SQL.AppendLine("            @MontoAplicado)");
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
            SQL.AppendLine("@NumeroCobranza" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@Secuencial" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoAnticipoUsado" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroAnticipo" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@MontoOriginal" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoRestanteAlDia" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@SimboloMoneda" + InsSql.VarCharTypeForDb(4) + ",");
            SQL.AppendLine("@CodigoMoneda" + InsSql.VarCharTypeForDb(4) + ",");
            SQL.AppendLine("@MontoTotalDelAnticipo" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoAplicado" + InsSql.DecimalTypeForDb(25, 4) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".AnticipoCobrado WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroCobranza = @NumeroCobranza AND Secuencial = @Secuencial)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".AnticipoCobrado WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroCobranza = @NumeroCobranza AND Secuencial = @Secuencial");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_AnticipoCobradoCanBeUpdated @ConsecutivoCompania,@NumeroCobranza,@Secuencial, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".AnticipoCobrado");
            SQL.AppendLine("            SET ConsecutivoAnticipoUsado = @ConsecutivoAnticipoUsado,");
            SQL.AppendLine("               NumeroAnticipo = @NumeroAnticipo,");
            SQL.AppendLine("               MontoOriginal = @MontoOriginal,");
            SQL.AppendLine("               MontoRestanteAlDia = @MontoRestanteAlDia,");
            SQL.AppendLine("               SimboloMoneda = @SimboloMoneda,");
            SQL.AppendLine("               CodigoMoneda = @CodigoMoneda,");
            SQL.AppendLine("               MontoTotalDelAnticipo = @MontoTotalDelAnticipo,");
            SQL.AppendLine("               MontoAplicado = @MontoAplicado");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND NumeroCobranza = @NumeroCobranza");
            SQL.AppendLine("               AND Secuencial = @Secuencial");
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
            SQL.AppendLine("@NumeroCobranza" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@Secuencial" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".AnticipoCobrado WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroCobranza = @NumeroCobranza AND Secuencial = @Secuencial)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".AnticipoCobrado WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroCobranza = @NumeroCobranza AND Secuencial = @Secuencial");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_AnticipoCobradoCanBeDeleted @ConsecutivoCompania,@NumeroCobranza,@Secuencial, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".AnticipoCobrado");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND NumeroCobranza = @NumeroCobranza");
            SQL.AppendLine("               AND Secuencial = @Secuencial");
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
            SQL.AppendLine("@NumeroCobranza" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@Secuencial" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         AnticipoCobrado.ConsecutivoCompania,");
            SQL.AppendLine("         AnticipoCobrado.NumeroCobranza,");
            SQL.AppendLine("         AnticipoCobrado.Secuencial,");
            SQL.AppendLine("         AnticipoCobrado.ConsecutivoAnticipoUsado,");
            SQL.AppendLine("         AnticipoCobrado.NumeroAnticipo,");
            SQL.AppendLine("         AnticipoCobrado.MontoOriginal,");
            SQL.AppendLine("         AnticipoCobrado.MontoRestanteAlDia,");
            SQL.AppendLine("         AnticipoCobrado.SimboloMoneda,");
            SQL.AppendLine("         AnticipoCobrado.CodigoMoneda,");
            SQL.AppendLine("         AnticipoCobrado.MontoTotalDelAnticipo,");
            SQL.AppendLine("         AnticipoCobrado.MontoAplicado,");
            SQL.AppendLine("         CAST(AnticipoCobrado.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         AnticipoCobrado.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".AnticipoCobrado");
            SQL.AppendLine("             INNER JOIN Adm.Gv_Anticipo_B1 ON " + DbSchema + ".AnticipoCobrado.ConsecutivoAnticipoUsado = Adm.Gv_Anticipo_B1.ConsecutivoAnticipo");
            SQL.AppendLine("             INNER JOIN Adm.Gv_Anticipo_B1 ON " + DbSchema + ".AnticipoCobrado.NumeroAnticipo = Adm.Gv_Anticipo_B1.Numero");
            SQL.AppendLine("             INNER JOIN " + DbSchema + ".Gv_Moneda_B1 ON " + DbSchema + ".AnticipoCobrado.CodigoMoneda = " + DbSchema + ".Gv_Moneda_B1.Codigo");
            SQL.AppendLine("      WHERE AnticipoCobrado.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND AnticipoCobrado.NumeroCobranza = @NumeroCobranza");
            SQL.AppendLine("         AND AnticipoCobrado.Secuencial = @Secuencial");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_AnticipoCobrado_B1.ConsecutivoAnticipoUsado,");
            SQL.AppendLine("      " + DbSchema + ".Gv_AnticipoCobrado_B1.MontoOriginal,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_AnticipoCobrado_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_AnticipoCobrado_B1.NumeroCobranza,");
            SQL.AppendLine("      " + DbSchema + ".Gv_AnticipoCobrado_B1.Secuencial");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_AnticipoCobrado_B1");
            SQL.AppendLine("      INNER JOIN Adm.Gv_Anticipo_B1 ON  " + DbSchema + ".Gv_AnticipoCobrado_B1.ConsecutivoAnticipoUsado = Adm.Gv_Anticipo_B1.ConsecutivoAnticipo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_AnticipoCobrado_B1.ConsecutivoCompania = Adm.Gv_Anticipo_B1.ConsecutivoCompania");
            SQL.AppendLine("      INNER JOIN Adm.Gv_Anticipo_B1 ON  " + DbSchema + ".Gv_AnticipoCobrado_B1.NumeroAnticipo = Adm.Gv_Anticipo_B1.Numero");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_AnticipoCobrado_B1.ConsecutivoCompania = Adm.Gv_Anticipo_B1.ConsecutivoCompania");
            SQL.AppendLine("      INNER JOIN " + DbSchema + ".Gv_Moneda_B1 ON  " + DbSchema + ".Gv_AnticipoCobrado_B1.CodigoMoneda = " + DbSchema + ".Gv_Moneda_B1.Codigo");
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
            SQL.AppendLine("      " + DbSchema + ".AnticipoCobrado.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".AnticipoCobrado.NumeroCobranza,");
            SQL.AppendLine("      " + DbSchema + ".AnticipoCobrado.Secuencial,");
            SQL.AppendLine("      " + DbSchema + ".AnticipoCobrado.ConsecutivoAnticipoUsado,");
            SQL.AppendLine("      " + DbSchema + ".AnticipoCobrado.NumeroAnticipo,");
            SQL.AppendLine("      " + DbSchema + ".AnticipoCobrado.CodigoMoneda");
            //SQL.AppendLine("      ," + DbSchema + ".AnticipoCobrado.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".AnticipoCobrado");
            SQL.AppendLine("      WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("          AND Secuencial IN (");
            SQL.AppendLine("            SELECT  Secuencial ");
            SQL.AppendLine("            FROM OPENXML( @hdoc, 'GpData/GpResult',2) ");
            SQL.AppendLine("            WITH (Secuencial int) AS XmlFKTmp) ");
            SQL.AppendLine(" EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".AnticipoCobrado", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_AnticipoCobrado_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AnticipoCobradoINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AnticipoCobradoUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AnticipoCobradoDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AnticipoCobradoGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AnticipoCobradoSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AnticipoCobradoGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".AnticipoCobrado", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_AnticipoCobradoINS");
            vResult = insSp.Drop(DbSchema + ".Gp_AnticipoCobradoUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_AnticipoCobradoDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_AnticipoCobradoGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_AnticipoCobradoGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_AnticipoCobradoSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_AnticipoCobrado_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsAnticipoCobradoED

} //End of namespace Galac.Adm.Dal.CAnticipo

