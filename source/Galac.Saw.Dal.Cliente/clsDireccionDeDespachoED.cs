using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Saw.Ccl.Cliente;

namespace Galac.Saw.Dal.Cliente {
    [LibMefDalComponentMetadata(typeof(clsDireccionDeDespachoED))]
    public class clsDireccionDeDespachoED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsDireccionDeDespachoED(): base(){
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
            get { return "DireccionDeDespacho"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("DireccionDeDespacho", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnDirDeDesConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoCliente" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT nnDirDeDesCodigoClie NOT NULL, ");
            SQL.AppendLine("ConsecutivoDireccion" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnDirDeDesConsecutiv NOT NULL, ");
            SQL.AppendLine("PersonaContacto" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_DirDeDesPeCo DEFAULT (''), ");
            SQL.AppendLine("Direccion" + InsSql.VarCharTypeForDb(100) + " CONSTRAINT nnDirDeDesDireccion NOT NULL, ");
            SQL.AppendLine("Ciudad" + InsSql.VarCharTypeForDb(100) + " CONSTRAINT d_DirDeDesCi DEFAULT (''), ");
            SQL.AppendLine("ZonaPostal" + InsSql.VarCharTypeForDb(7) + " CONSTRAINT d_DirDeDesZoPo DEFAULT (''), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_DireccionDeDespacho PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, CodigoCliente ASC, ConsecutivoDireccion ASC)");
            SQL.AppendLine(",CONSTRAINT fk_DireccionDeDespachoCliente FOREIGN KEY (ConsecutivoCompania, CodigoCliente)");
            SQL.AppendLine("REFERENCES Saw.Cliente(ConsecutivoCompania, Codigo)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_DireccionDeDespachoCliente FOREIGN KEY (ConsecutivoCompania, CodigoCliente)");
            SQL.AppendLine("REFERENCES Saw.Cliente(ConsecutivoCompania, codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_DireccionDeDespachoCiudad FOREIGN KEY (Ciudad)");
            SQL.AppendLine("REFERENCES Comun.Ciudad(NombreCiudad)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT DireccionDeDespacho.ConsecutivoCompania, DireccionDeDespacho.CodigoCliente, DireccionDeDespacho.ConsecutivoDireccion, DireccionDeDespacho.PersonaContacto");
            SQL.AppendLine(", DireccionDeDespacho.Direccion, DireccionDeDespacho.Ciudad, DireccionDeDespacho.ZonaPostal");
            SQL.AppendLine(", DireccionDeDespacho.fldTimeStamp, CAST(DireccionDeDespacho.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM dbo.DireccionDeDespacho");
            SQL.AppendLine("INNER JOIN dbo.Cliente ON dbo.DireccionDeDespacho.CodigoCliente = dbo.Cliente.codigo");
            SQL.AppendLine("      AND dbo.DireccionDeDespacho.ConsecutivoCompania = dbo.Cliente.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN Comun.Ciudad ON dbo.DireccionDeDespacho.Ciudad = Comun.Ciudad.NombreCiudad");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoCliente" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@ConsecutivoDireccion" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@PersonaContacto" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@Direccion" + InsSql.VarCharTypeForDb(100) + " = '',");
            SQL.AppendLine("@Ciudad" + InsSql.VarCharTypeForDb(100) + ",");
            SQL.AppendLine("@ZonaPostal" + InsSql.VarCharTypeForDb(7) + " = ''");
            return SQL.ToString();
        }

        private string SqlSpIns() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".DireccionDeDespacho(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            CodigoCliente,");
            SQL.AppendLine("            ConsecutivoDireccion,");
            SQL.AppendLine("            PersonaContacto,");
            SQL.AppendLine("            Direccion,");
            SQL.AppendLine("            Ciudad,");
            SQL.AppendLine("            ZonaPostal)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @CodigoCliente,");
            SQL.AppendLine("            @ConsecutivoDireccion,");
            SQL.AppendLine("            @PersonaContacto,");
            SQL.AppendLine("            @Direccion,");
            SQL.AppendLine("            @Ciudad,");
            SQL.AppendLine("            @ZonaPostal)");
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
            SQL.AppendLine("@CodigoCliente" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@ConsecutivoDireccion" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@PersonaContacto" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@Direccion" + InsSql.VarCharTypeForDb(100) + ",");
            SQL.AppendLine("@Ciudad" + InsSql.VarCharTypeForDb(100) + ",");
            SQL.AppendLine("@ZonaPostal" + InsSql.VarCharTypeForDb(7) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM dbo.DireccionDeDespacho WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoCliente = @CodigoCliente AND ConsecutivoDireccion = @ConsecutivoDireccion)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM dbo.DireccionDeDespacho WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoCliente = @CodigoCliente AND ConsecutivoDireccion = @ConsecutivoDireccion");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_DireccionDeDespachoCanBeUpdated @ConsecutivoCompania,@CodigoCliente,@ConsecutivoDireccion, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE dbo.DireccionDeDespacho");
            SQL.AppendLine("            SET PersonaContacto = @PersonaContacto,");
            SQL.AppendLine("               Direccion = @Direccion,");
            SQL.AppendLine("               Ciudad = @Ciudad,");
            SQL.AppendLine("               ZonaPostal = @ZonaPostal");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND CodigoCliente = @CodigoCliente");
            SQL.AppendLine("               AND ConsecutivoDireccion = @ConsecutivoDireccion");
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
            SQL.AppendLine("@CodigoCliente" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@ConsecutivoDireccion" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM dbo.DireccionDeDespacho WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoCliente = @CodigoCliente AND ConsecutivoDireccion = @ConsecutivoDireccion)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM dbo.DireccionDeDespacho WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoCliente = @CodigoCliente AND ConsecutivoDireccion = @ConsecutivoDireccion");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_DireccionDeDespachoCanBeDeleted @ConsecutivoCompania,@CodigoCliente,@ConsecutivoDireccion, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM dbo.DireccionDeDespacho");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND CodigoCliente = @CodigoCliente");
            SQL.AppendLine("               AND ConsecutivoDireccion = @ConsecutivoDireccion");
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
            SQL.AppendLine("@CodigoCliente" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@ConsecutivoDireccion" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         CodigoCliente,");
            SQL.AppendLine("         ConsecutivoDireccion,");
            SQL.AppendLine("         PersonaContacto,");
            SQL.AppendLine("         Direccion,");
            SQL.AppendLine("         Ciudad,");
            SQL.AppendLine("         ZonaPostal,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM dbo.DireccionDeDespacho");
            SQL.AppendLine("      WHERE DireccionDeDespacho.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND DireccionDeDespacho.CodigoCliente = @CodigoCliente");
            SQL.AppendLine("         AND DireccionDeDespacho.ConsecutivoDireccion = @ConsecutivoDireccion");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoCliente" + InsSql.VarCharTypeForDb(10));
            return SQL.ToString();
        }

        private string SqlSpSelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SELECT ");
            SQL.AppendLine("        ConsecutivoCompania,");
            SQL.AppendLine("        CodigoCliente,");
            SQL.AppendLine("        ConsecutivoDireccion,");
            SQL.AppendLine("        PersonaContacto,");
            SQL.AppendLine("        Direccion,");
            SQL.AppendLine("        Ciudad,");
            SQL.AppendLine("        ZonaPostal,");
            SQL.AppendLine("        fldTimeStamp");
            SQL.AppendLine("    FROM DireccionDeDespacho");
            SQL.AppendLine(" 	WHERE CodigoCliente = @CodigoCliente");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoCliente" + InsSql.VarCharTypeForDb(10));
            return SQL.ToString();
        }

        private string SqlSpDelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	DELETE FROM DireccionDeDespacho");
            SQL.AppendLine(" 	WHERE CodigoCliente = @CodigoCliente");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInsDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoCliente" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@XmlDataDetail" + InsSql.XmlTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpInsDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SET NOCOUNT ON;");
            SQL.AppendLine("	DECLARE @ReturnValue  " + InsSql.NumericTypeForDb(10, 0));
	        SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
	        SQL.AppendLine("	    BEGIN");
            SQL.AppendLine("	    EXEC Saw.Gp_DireccionDeDespachoDelDet @ConsecutivoCompania = @ConsecutivoCompania, @CodigoCliente = @CodigoCliente");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO dbo.DireccionDeDespacho(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        CodigoCliente,");
			SQL.AppendLine("	        ConsecutivoDireccion,");
			SQL.AppendLine("	        PersonaContacto,");
			SQL.AppendLine("	        Direccion,");
			SQL.AppendLine("	        Ciudad,");
			SQL.AppendLine("	        ZonaPostal)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @CodigoCliente,");
			SQL.AppendLine("	        ConsecutivoDireccion,");
			SQL.AppendLine("	        PersonaContacto,");
			SQL.AppendLine("	        Direccion,");
			SQL.AppendLine("	        Ciudad,");
			SQL.AppendLine("	        ZonaPostal");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataDireccionDeDespacho/GpDetailDireccionDeDespacho',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        ConsecutivoDireccion " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        PersonaContacto " + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("	        Direccion " + InsSql.VarCharTypeForDb(100) + ",");
            SQL.AppendLine("	        Ciudad " + InsSql.VarCharTypeForDb(100) + ",");
            SQL.AppendLine("	        ZonaPostal " + InsSql.VarCharTypeForDb(7) + ") AS XmlDocDetailOfCliente");
            SQL.AppendLine("	    EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("	    SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("	    RETURN @ReturnValue");
	        SQL.AppendLine("	END");
	        SQL.AppendLine("	ELSE");
            SQL.AppendLine("	    RETURN -1");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_DireccionDeDespacho_B1.PersonaContacto,");
            SQL.AppendLine("      " + DbSchema + ".Gv_DireccionDeDespacho_B1.Direccion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_DireccionDeDespacho_B1.Ciudad,");
            SQL.AppendLine("      " + DbSchema + ".Gv_DireccionDeDespacho_B1.ZonaPostal,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_DireccionDeDespacho_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_DireccionDeDespacho_B1.CodigoCliente,");
            SQL.AppendLine("      " + DbSchema + ".Gv_DireccionDeDespacho_B1.ConsecutivoDireccion");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_DireccionDeDespacho_B1");
            SQL.AppendLine("      INNER JOIN Saw.Gv_Cliente_B1 ON  " + DbSchema + ".Gv_DireccionDeDespacho_B1.CodigoCliente = Saw.Gv_Cliente_B1.codigo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_DireccionDeDespacho_B1.ConsecutivoCompania = Saw.Gv_Cliente_B1.ConsecutivoCompania");
            SQL.AppendLine("      INNER JOIN Comun.Gv_Ciudad_B1 ON  " + DbSchema + ".Gv_DireccionDeDespacho_B1.Ciudad = Comun.Gv_Ciudad_B1.NombreCiudad");
            SQL.AppendLine("'   IF (NOT @SQLWhere IS NULL) AND (@SQLWhere <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' WHERE ' + @SQLWhere");
            SQL.AppendLine("   IF (NOT @SQLOrderBy IS NULL) AND (@SQLOrderBy <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' ORDER BY ' + @SQLOrderBy");
            SQL.AppendLine("   EXEC(@strSQL)");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".DireccionDeDespacho", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_DireccionDeDespacho_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_DireccionDeDespachoINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_DireccionDeDespachoUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_DireccionDeDespachoDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_DireccionDeDespachoGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_DireccionDeDespachoSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_DireccionDeDespachoDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_DireccionDeDespachoInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
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
            if (insDbo.Exists("dbo.DireccionDeDespacho", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_DireccionDeDespachoINS");
            vResult = insSp.Drop(DbSchema + ".Gp_DireccionDeDespachoUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_DireccionDeDespachoDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_DireccionDeDespachoGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_DireccionDeDespachoInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_DireccionDeDespachoDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_DireccionDeDespachoSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_DireccionDeDespacho_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsDireccionDeDespachoED

} //End of namespace Galac.Saw.Dal.Cliente

