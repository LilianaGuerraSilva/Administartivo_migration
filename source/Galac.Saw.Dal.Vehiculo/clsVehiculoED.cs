using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.Vehiculo;
using LibGalac.Aos.Dal.Contracts;

namespace Galac.Saw.Dal.Vehiculo {
    [LibMefDalComponentMetadata(typeof(clsVehiculoED))]
    public class clsVehiculoED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsVehiculoED(): base(){
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
            get { return "Vehiculo"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("Vehiculo", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnVehConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnVehConsecutiv NOT NULL, ");
            SQL.AppendLine("Placa" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT nnVehPlaca NOT NULL, ");
            SQL.AppendLine("serialVIN" + InsSql.VarCharTypeForDb(40) + " CONSTRAINT d_VehseVI DEFAULT (''), ");
            SQL.AppendLine("NombreModelo" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_VehNoMo DEFAULT (''), ");
            SQL.AppendLine("Ano" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT d_VehAn DEFAULT (0), ");
            SQL.AppendLine("CodigoColor" + InsSql.VarCharTypeForDb(3) + " CONSTRAINT d_VehCoCo DEFAULT (''), ");
            SQL.AppendLine("CodigoCliente" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT d_VehCoCl DEFAULT (''), ");
            SQL.AppendLine("NumeroPoliza" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_VehNuPo DEFAULT (''), ");
            SQL.AppendLine("SerialMotor" + InsSql.VarCharTypeForDb(40) + " CONSTRAINT d_VehSeMo DEFAULT (''), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(20) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_Vehiculo PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Consecutivo ASC)");
            SQL.AppendLine(", CONSTRAINT fk_VehiculoModelo FOREIGN KEY (NombreModelo)");
            SQL.AppendLine("REFERENCES Saw.Modelo(Nombre)");
            SQL.AppendLine("ON UPDATE CASCADE, ");
            SQL.AppendLine("CONSTRAINT fk_VehiculoColor FOREIGN KEY (ConsecutivoCompania, CodigoColor)");
            SQL.AppendLine("REFERENCES Saw.Color(ConsecutivoCompania, CodigoColor)");
            SQL.AppendLine("ON UPDATE CASCADE,");
            SQL.AppendLine("CONSTRAINT fk_VehiculoCliente FOREIGN KEY (ConsecutivoCompania, CodigoCliente)");
            SQL.AppendLine("REFERENCES dbo.Cliente(ConsecutivoCompania, Codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(",CONSTRAINT u_VehPlaca UNIQUE NONCLUSTERED (ConsecutivoCompania ASC, Placa ASC)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT Vehiculo.ConsecutivoCompania, Vehiculo.Consecutivo, Vehiculo.Placa, Vehiculo.serialVIN");
            SQL.AppendLine(", Vehiculo.NombreModelo, Vehiculo.Ano, Vehiculo.CodigoColor, Vehiculo.CodigoCliente");
            SQL.AppendLine(", Vehiculo.NumeroPoliza, Vehiculo.SerialMotor, Vehiculo.NombreOperador, Vehiculo.FechaUltimaModificacion");
            SQL.AppendLine(", dbo.Cliente.Nombre AS NombreCliente");
            SQL.AppendLine(", Vehiculo.fldTimeStamp, CAST(Vehiculo.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".Vehiculo");
            SQL.AppendLine("INNER JOIN Saw.Modelo ON  " + DbSchema + ".Vehiculo.NombreModelo = Saw.Modelo.Nombre");
            SQL.AppendLine("INNER JOIN Saw.Color ON  " + DbSchema + ".Vehiculo.CodigoColor = Saw.Color.CodigoColor");
            SQL.AppendLine("      AND " + DbSchema + ".Vehiculo.ConsecutivoCompania = Saw.Color.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN dbo.Cliente ON  " + DbSchema + ".Vehiculo.CodigoCliente = dbo.Cliente.Codigo");
            SQL.AppendLine("      AND " + DbSchema + ".Vehiculo.ConsecutivoCompania = dbo.Cliente.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Placa" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@serialVIN" + InsSql.VarCharTypeForDb(40) + " = '',");
            SQL.AppendLine("@NombreModelo" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@Ano" + InsSql.NumericTypeForDb(10, 0) + " = 0,");
            SQL.AppendLine("@CodigoColor" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@CodigoCliente" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@NumeroPoliza" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@SerialMotor" + InsSql.VarCharTypeForDb(40) + " = '',");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".Vehiculo(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            Placa,");
            SQL.AppendLine("            serialVIN,");
            SQL.AppendLine("            NombreModelo,");
            SQL.AppendLine("            Ano,");
            SQL.AppendLine("            CodigoColor,");
            SQL.AppendLine("            CodigoCliente,");
            SQL.AppendLine("            NumeroPoliza,");
            SQL.AppendLine("            SerialMotor,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @Placa,");
            SQL.AppendLine("            @serialVIN,");
            SQL.AppendLine("            @NombreModelo,");
            SQL.AppendLine("            @Ano,");
            SQL.AppendLine("            @CodigoColor,");
            SQL.AppendLine("            @CodigoCliente,");
            SQL.AppendLine("            @NumeroPoliza,");
            SQL.AppendLine("            @SerialMotor,");
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
            SQL.AppendLine("@Placa" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@serialVIN" + InsSql.VarCharTypeForDb(40) + ",");
            SQL.AppendLine("@NombreModelo" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@Ano" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoColor" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@CodigoCliente" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@NumeroPoliza" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@SerialMotor" + InsSql.VarCharTypeForDb(40) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Vehiculo WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Vehiculo WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_VehiculoCanBeUpdated @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".Vehiculo");
            SQL.AppendLine("            SET Placa = @Placa,");
            SQL.AppendLine("               serialVIN = @serialVIN,");
            SQL.AppendLine("               NombreModelo = @NombreModelo,");
            SQL.AppendLine("               Ano = @Ano,");
            SQL.AppendLine("               CodigoColor = @CodigoColor,");
            SQL.AppendLine("               CodigoCliente = @CodigoCliente,");
            SQL.AppendLine("               NumeroPoliza = @NumeroPoliza,");
            SQL.AppendLine("               SerialMotor = @SerialMotor,");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Vehiculo WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Vehiculo WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_VehiculoCanBeDeleted @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".Vehiculo");
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
            SQL.AppendLine("         Vehiculo.ConsecutivoCompania,");
            SQL.AppendLine("         Vehiculo.Consecutivo,");
            SQL.AppendLine("         Vehiculo.Placa,");
            SQL.AppendLine("         Vehiculo.serialVIN,");
            SQL.AppendLine("         Vehiculo.NombreModelo,");
            SQL.AppendLine("         Gv_Modelo_B1.Marca AS Marca,");
            SQL.AppendLine("         Vehiculo.Ano,");
            SQL.AppendLine("         Vehiculo.CodigoColor,");
            SQL.AppendLine("         Gv_Color_B1.DescripcionColor AS DescripcionColor,");
            SQL.AppendLine("         Vehiculo.CodigoCliente,");
            SQL.AppendLine("         dbo.Gv_Cliente_B1.Nombre AS NombreCliente,");
            SQL.AppendLine("         dbo.Gv_Cliente_B1.NumeroRif AS RIFCliente,");
            SQL.AppendLine("         Vehiculo.NumeroPoliza,");
            SQL.AppendLine("         Vehiculo.SerialMotor,");
            SQL.AppendLine("         Vehiculo.NombreOperador,");
            SQL.AppendLine("         Vehiculo.FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(Vehiculo.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         Vehiculo.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".Vehiculo");
            SQL.AppendLine("             INNER JOIN Saw.Gv_Modelo_B1 ON " + DbSchema + ".Vehiculo.NombreModelo = Saw.Gv_Modelo_B1.Nombre");
            SQL.AppendLine("             INNER JOIN Saw.Gv_Color_B1 ON " + DbSchema + ".Vehiculo.CodigoColor = Saw.Gv_Color_B1.CodigoColor");
            SQL.AppendLine("             AND " + DbSchema + ".Vehiculo.ConsecutivoCompania = Saw.Gv_Color_B1.ConsecutivoCompania");
            SQL.AppendLine("             INNER JOIN dbo.Gv_Cliente_B1 ON " + DbSchema + ".Vehiculo.CodigoCliente = dbo.Gv_Cliente_B1.Codigo");
            SQL.AppendLine("             AND " + DbSchema + ".Vehiculo.ConsecutivoCompania = dbo.Gv_Cliente_B1.ConsecutivoCompania");
            SQL.AppendLine("      WHERE Vehiculo.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND Vehiculo.Consecutivo = @Consecutivo");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_Vehiculo_B1.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vehiculo_B1.serialVIN,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vehiculo_B1.NombreModelo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Modelo_B1.Marca,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vehiculo_B1.Ano,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vehiculo_B1.CodigoCliente,");
            SQL.AppendLine("      dbo.Gv_Cliente_B1.Nombre AS NombreCliente,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vehiculo_B1.NumeroPoliza,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vehiculo_B1.SerialMotor,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Color_B1.DescripcionColor,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vehiculo_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vehiculo_B1.Placa,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vehiculo_B1.CodigoColor");            
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Vehiculo_B1");
            SQL.AppendLine("      INNER JOIN Saw.Gv_Modelo_B1 ON  " + DbSchema + ".Gv_Vehiculo_B1.NombreModelo = Saw.Gv_Modelo_B1.Nombre");
            SQL.AppendLine("      INNER JOIN Saw.Gv_Color_B1 ON  " + DbSchema + ".Gv_Vehiculo_B1.CodigoColor = Saw.Gv_Color_B1.CodigoColor");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Vehiculo_B1.ConsecutivoCompania = Saw.Gv_Color_B1.ConsecutivoCompania");
            SQL.AppendLine("      INNER JOIN dbo.Gv_Cliente_B1 ON  " + DbSchema + ".Gv_Vehiculo_B1.CodigoCliente = dbo.Gv_Cliente_B1.Codigo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Vehiculo_B1.ConsecutivoCompania = dbo.Gv_Cliente_B1.ConsecutivoCompania");
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
            SQL.AppendLine("      " + DbSchema + ".Vehiculo.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Vehiculo.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Vehiculo.Placa,");
            SQL.AppendLine("      " + DbSchema + ".Vehiculo.serialVIN,");
            SQL.AppendLine("      " + DbSchema + ".Vehiculo.NombreModelo,");
            SQL.AppendLine("      " + DbSchema + ".Vehiculo.Ano,");
            SQL.AppendLine("      " + DbSchema + ".Vehiculo.CodigoColor,");
            SQL.AppendLine("      " + DbSchema + ".Vehiculo.CodigoCliente");
            //SQL.AppendLine("      ," + DbSchema + ".Vehiculo.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".Vehiculo");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries
        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".Vehiculo", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }
        bool CrearVistas() {
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_Vehiculo_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }
        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VehiculoINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VehiculoUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VehiculoDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VehiculoGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VehiculoSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VehiculoGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".Vehiculo", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_VehiculoINS");
            vResult = insSp.Drop(DbSchema + ".Gp_VehiculoUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_VehiculoDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_VehiculoGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_VehiculoGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_VehiculoSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_Vehiculo_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsVehiculoED

} //End of namespace Galac.Saw.Dal.Vehiculo

