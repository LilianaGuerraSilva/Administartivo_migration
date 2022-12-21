using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Dal.Venta {
    [LibMefDalComponentMetadata(typeof(clsContratoED))]
    public class clsContratoED : LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsContratoED() : base() {
            DbSchema = "Dbo";
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
            get { return "Contrato"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("Contrato", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnConConsecutiv NOT NULL, ");
            SQL.AppendLine("NumeroContrato" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT nnConNumeroCont NOT NULL, ");
            SQL.AppendLine("StatusContrato" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_ConStCo DEFAULT ('0'), ");
            SQL.AppendLine("CodigoCliente" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT d_ConCoCl DEFAULT (''), ");
            SQL.AppendLine("DuracionDelContrato" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_ConDuDeCo DEFAULT ('0'), ");
            SQL.AppendLine("FechaDeInicio" + InsSql.DateTypeForDb() + " CONSTRAINT d_ConFeDeIn DEFAULT (''), ");
            SQL.AppendLine("FechaFinal" + InsSql.DateTypeForDb() + " CONSTRAINT d_ConFeFi DEFAULT (''), ");
            SQL.AppendLine("Observaciones" + InsSql.VarCharTypeForDb(255) + " CONSTRAINT d_ConOb DEFAULT (''), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("ConsecutivoVendedor" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT d_ConCoVe DEFAULT (0), ");
            SQL.AppendLine("Moneda" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT d_ConMo DEFAULT (''), ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_Contrato PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, NumeroContrato ASC)");
            SQL.AppendLine(", CONSTRAINT fk_ContratoCliente FOREIGN KEY (ConsecutivoCompania, CodigoCliente)");
            SQL.AppendLine("REFERENCES Saw.Cliente(ConsecutivoCompania, codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_ContratoVendedor FOREIGN KEY (ConsecutivoCompania, CodigoVendedor)");
            SQL.AppendLine("REFERENCES dbo.Vendedor(ConsecutivoCompania, codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT Contrato.ConsecutivoCompania, Contrato.NumeroContrato, Contrato.StatusContrato, " + DbSchema + ".Gv_EnumStatusContrato.StrValue AS StatusContratoStr, Contrato.CodigoCliente");
            SQL.AppendLine(", Contrato.DuracionDelContrato, " + DbSchema + ".Gv_EnumDuracionDelContrato.StrValue AS DuracionDelContratoStr, Contrato.FechaDeInicio, Contrato.FechaFinal, Contrato.Observaciones");
            SQL.AppendLine(", Contrato.NombreOperador, Contrato.ConsecutivoVendedor, Contrato.Moneda, Contrato.FechaUltimaModificacion");
            SQL.AppendLine(", Cliente.nombre AS NombreCliente");
            SQL.AppendLine(", Contrato.fldTimeStamp, CAST(Contrato.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".Contrato");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumStatusContrato");
            SQL.AppendLine("ON " + DbSchema + ".Contrato.StatusContrato COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumStatusContrato.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumDuracionDelContrato");
            SQL.AppendLine("ON " + DbSchema + ".Contrato.DuracionDelContrato COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumDuracionDelContrato.DbValue");
            SQL.AppendLine("INNER JOIN dbo.Cliente ON  " + DbSchema + ".Contrato.CodigoCliente = dbo.Cliente.codigo");
            SQL.AppendLine("      AND " + DbSchema + ".Contrato.ConsecutivoCompania = dbo.Cliente.ConsecutivoCompania");
            //SQL.AppendLine("INNER JOIN dbo.Vendedor ON  " + DbSchema + ".Contrato.CodigoVendedor = dbo.Vendedor.codigo");
            //SQL.AppendLine("      AND " + DbSchema + ".Contrato.ConsecutivoCompania = dbo.Vendedor.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroContrato" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@StatusContrato" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@CodigoCliente" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@DuracionDelContrato" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@FechaDeInicio" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@FechaFinal" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@Observaciones" + InsSql.VarCharTypeForDb(255) + " = '',");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@ConsecutivoVendedor" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Moneda" + InsSql.VarCharTypeForDb(10) + " = '',");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".Contrato(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            NumeroContrato,");
            SQL.AppendLine("            StatusContrato,");
            SQL.AppendLine("            CodigoCliente,");
            SQL.AppendLine("            DuracionDelContrato,");
            SQL.AppendLine("            FechaDeInicio,");
            SQL.AppendLine("            FechaFinal,");
            SQL.AppendLine("            Observaciones,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            ConsecutivoVendedor,");
            SQL.AppendLine("            Moneda,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @NumeroContrato,");
            SQL.AppendLine("            @StatusContrato,");
            SQL.AppendLine("            @CodigoCliente,");
            SQL.AppendLine("            @DuracionDelContrato,");
            SQL.AppendLine("            @FechaDeInicio,");
            SQL.AppendLine("            @FechaFinal,");
            SQL.AppendLine("            @Observaciones,");
            SQL.AppendLine("            @NombreOperador,");
            SQL.AppendLine("            @ConsecutivoVendedor,");
            SQL.AppendLine("            @Moneda,");
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
            SQL.AppendLine("@NumeroContrato" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@StatusContrato" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@CodigoCliente" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@DuracionDelContrato" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@FechaDeInicio" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@FechaFinal" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@Observaciones" + InsSql.VarCharTypeForDb(255) + ",");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@ConsecutivoVendedor" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Moneda" + InsSql.VarCharTypeForDb(10) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Contrato WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroContrato = @NumeroContrato)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Contrato WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroContrato = @NumeroContrato");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_ContratoCanBeUpdated @ConsecutivoCompania,@NumeroContrato, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".Contrato");
            SQL.AppendLine("            SET StatusContrato = @StatusContrato,");
            SQL.AppendLine("               CodigoCliente = @CodigoCliente,");
            SQL.AppendLine("               DuracionDelContrato = @DuracionDelContrato,");
            SQL.AppendLine("               FechaDeInicio = @FechaDeInicio,");
            SQL.AppendLine("               FechaFinal = @FechaFinal,");
            SQL.AppendLine("               Observaciones = @Observaciones,");
            SQL.AppendLine("               NombreOperador = @NombreOperador,");
            SQL.AppendLine("               ConsecutivoVendedor = @ConsecutivoVendedor,");
            SQL.AppendLine("               Moneda = @Moneda,");
            SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND NumeroContrato = @NumeroContrato");
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
            SQL.AppendLine("@NumeroContrato" + InsSql.VarCharTypeForDb(5) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Contrato WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroContrato = @NumeroContrato)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Contrato WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroContrato = @NumeroContrato");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_ContratoCanBeDeleted @ConsecutivoCompania,@NumeroContrato, @CurrentTimeStamp, @ValidationMsg out");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".Contrato");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND NumeroContrato = @NumeroContrato");
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
            SQL.AppendLine("@NumeroContrato" + InsSql.VarCharTypeForDb(5));

            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         Contrato.ConsecutivoCompania,");
            SQL.AppendLine("         Contrato.NumeroContrato,");
            SQL.AppendLine("         Contrato.StatusContrato,");
            SQL.AppendLine("         Contrato.CodigoCliente,");
            SQL.AppendLine("         Gv_Cliente_B1.Nombre AS NombreCliente,");
            SQL.AppendLine("         Contrato.DuracionDelContrato,");
            SQL.AppendLine("         Contrato.FechaDeInicio,");
            SQL.AppendLine("         Contrato.FechaFinal,");
            SQL.AppendLine("         Contrato.Observaciones,");
            SQL.AppendLine("         Contrato.NombreOperador,");
            SQL.AppendLine("         Contrato.ConsecutivoVendedor,");
            SQL.AppendLine("         Gv_Vendedor_B1.nombre AS NombreVendedor,");
            SQL.AppendLine("         Contrato.Moneda,");
            SQL.AppendLine("         Contrato.FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(Contrato.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         Contrato.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".Contrato");
            SQL.AppendLine("             INNER JOIN dbo.Gv_Cliente_B1 ON " + DbSchema + ".Contrato.CodigoCliente = dbo.Gv_Cliente_B1.codigo");
            SQL.AppendLine("             INNER JOIN Adm.Gv_Vendedor_B1 ON " + DbSchema + ".Contrato.ConsecutivoVendedor = Adm.Gv_Vendedor_B1.consecutivo");
            SQL.AppendLine("      WHERE Contrato.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND Contrato.NumeroContrato = @NumeroContrato");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_Contrato_B1.NumeroContrato,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Contrato_B1.StatusContratoStr,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Contrato_B1.CodigoCliente,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Contrato_B1.NombreCliente,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Contrato_B1.FechaDeInicio,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Contrato_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Contrato_B1.StatusContrato");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Contrato_B1");
            //SQL.AppendLine("      INNER JOIN dbo.Gv_Cliente_B1 ON  " + DbSchema + ".Gv_Contrato_B1.CodigoCliente = dbo.Gv_Cliente_B1.codigo");
            //SQL.AppendLine("      AND " + DbSchema + ".Gv_Contrato_B1.ConsecutivoCompania = dbo.Gv_Cliente_B1.ConsecutivoCompania");
            //SQL.AppendLine("      INNER JOIN dbo.Gv_Vendedor_B1 ON  " + DbSchema + ".Gv_Contrato_B1.CodigoVendedor = dbo.Gv_Vendedor_B1.codigo");
            //SQL.AppendLine("      AND " + DbSchema + ".Gv_Contrato_B1.ConsecutivoCompania = dbo.Gv_Vendedor_B1.ConsecutivoCompania");
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
            SQL.AppendLine("      " + DbSchema + ".Contrato.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Contrato.NumeroContrato,");
            SQL.AppendLine("      " + DbSchema + ".Contrato.StatusContrato,");
            SQL.AppendLine("      " + DbSchema + ".Contrato.CodigoCliente,");
            SQL.AppendLine("      " + DbSchema + ".Contrato.FechaDeInicio,");
            SQL.AppendLine("      " + DbSchema + ".Contrato.ConsecutivoVendedor");
            SQL.AppendLine("      FROM " + DbSchema + ".Contrato");
            SQL.AppendLine("      WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("          AND NumeroContrato IN (");
            SQL.AppendLine("            SELECT  NumeroContrato ");
            SQL.AppendLine("            FROM OPENXML( @hdoc, 'GpData/GpResult',2) ");
            SQL.AppendLine("            WITH (NumeroContrato varchar(5)) AS XmlFKTmp) ");
            SQL.AppendLine(" EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {            
            return true;
        }

        bool CrearVistas() {
            bool vResult = true;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumStatusContrato", LibTpvCreator.SqlViewStandardEnum(typeof(eStatusContrato), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumDuracionDelContrato", LibTpvCreator.SqlViewStandardEnum(typeof(eDuracionDelContrato), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_Contrato_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = true;
            LibStoredProc insSps = new LibStoredProc();            
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ContratoGET", SqlSpGetParameters(), SqlSpGet(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ContratoSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ContratoGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
            insSps.Dispose();
            return vResult;
        }

        public bool InstalarTabla() {
            bool vResult = true;
            if (CrearTabla()) {
                vResult = CrearVistas() && vResult;
                vResult = CrearProcedimientos() && vResult;
            }
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = true;
            if (insDbo.Exists(DbSchema + ".Contrato", eDboType.Tabla)) {
                 vResult = CrearVistas() && vResult;
                 vResult = CrearProcedimientos() && vResult;                
            }
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = true;
            LibStoredProc insSp = new LibStoredProc();
            LibViews insVista = new LibViews();            
            vResult = insSp.Drop(DbSchema + ".Gp_ContratoSCH");
            vResult = insVista.Drop(DbSchema + ".Gv_Contrato_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumStatusContrato") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumDuracionDelContrato") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados
    } //End of class clsContratoED

} //End of namespace Galac.Adm.Dal.Venta

