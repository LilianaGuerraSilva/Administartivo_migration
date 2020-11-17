using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using LibGalac.Aos.Base;

namespace Galac.Adm.Dal.GestionCompras {
    [LibMefDalComponentMetadata(typeof(clsCargaInicialED))]
    public class clsCargaInicialED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsCargaInicialED(): base(){
            DbSchema = "Adm";
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
            get { return "CargaInicial"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("CargaInicial", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnCosProConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnCosProConsecutiv NOT NULL, ");
            SQL.AppendLine("Fecha" + InsSql.DateTypeForDb() + " CONSTRAINT d_CosProFe DEFAULT (''), ");
            SQL.AppendLine("Existencia" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CosProEx DEFAULT (0), ");
            SQL.AppendLine("Costo" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CosProCo DEFAULT (0), ");
            SQL.AppendLine("CodigoArticulo" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT nnCosProCodigoArti NOT NULL, ");
            SQL.AppendLine("EsCargaInicial" + InsSql.VarCharTypeForDb(1) + " CONSTRAINT nnCosProEsCargaIni NOT NULL, ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_CargaInicial PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Consecutivo ASC)");
            SQL.AppendLine(", CONSTRAINT fk_CargaInicialArticuloInventario FOREIGN KEY (ConsecutivoCompania, CodigoArticulo)");
            SQL.AppendLine("REFERENCES dbo.ArticuloInventario(ConsecutivoCompania, codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(",CONSTRAINT u_CosProniaigocha UNIQUE NONCLUSTERED (ConsecutivoCompania,CodigoArticulo,Fecha)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT CargaInicial.ConsecutivoCompania, CargaInicial.Consecutivo, CargaInicial.Fecha, ArticuloInventario.Descripcion, ArticuloInventario.LineaDeProducto");
            SQL.AppendLine(", CargaInicial.Existencia, CargaInicial.Costo, CargaInicial.CodigoArticulo, CargaInicial.EsCargaInicial");
            SQL.AppendLine(", CargaInicial.fldTimeStamp, CAST(CargaInicial.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".CargaInicial");
            SQL.AppendLine("INNER JOIN dbo.ArticuloInventario ON  " + DbSchema + ".CargaInicial.CodigoArticulo = dbo.ArticuloInventario.codigo");
            SQL.AppendLine("      AND " + DbSchema + ".CargaInicial.ConsecutivoCompania = dbo.ArticuloInventario.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@Existencia" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@Costo" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@EsCargaInicial" + InsSql.VarCharTypeForDb(1) + " = ''");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".CargaInicial(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            Fecha,");
            SQL.AppendLine("            Existencia,");
            SQL.AppendLine("            Costo,");
            SQL.AppendLine("            CodigoArticulo,");
            SQL.AppendLine("            EsCargaInicial)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @Fecha,");
            SQL.AppendLine("            @Existencia,");
            SQL.AppendLine("            @Costo,");
            SQL.AppendLine("            @CodigoArticulo,");
            SQL.AppendLine("            @EsCargaInicial)");
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
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@Existencia" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@Costo" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@EsCargaInicial" + InsSql.VarCharTypeForDb(1) + ",");
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
            SQL.AppendLine("   DECLARE @CanBeChanged bit");
            SQL.AppendLine("   SET @CanBeChanged=1");
            SQL.AppendLine("   SET @ReturnValue = -1");
            SQL.AppendLine("   SET @ValidationMsg = ''");
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CargaInicial WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CargaInicial WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_CargaInicialCanBeUpdated @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            SQL.AppendLine("  IF @CanBeChanged = 1 --True");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".CargaInicial");
            SQL.AppendLine("           SET Fecha = @Fecha,");
            SQL.AppendLine("               Existencia = @Existencia,");
            SQL.AppendLine("               Costo = @Costo,");
            SQL.AppendLine("               CodigoArticulo = @CodigoArticulo,");
            SQL.AppendLine("               EsCargaInicial = @EsCargaInicial");
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
            SQL.AppendLine("   END");
            SQL.AppendLine("   ELSE");
            SQL.AppendLine("	RAISERROR('El registro no puede ser modificado: %s', 14, 1, @ValidationMsg)");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CargaInicial WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CargaInicial WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_CargaInicialCanBeDeleted @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".CargaInicial");
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
            SQL.AppendLine("         CargaInicial.ConsecutivoCompania,");
            SQL.AppendLine("         CargaInicial.Consecutivo,");
            SQL.AppendLine("         CargaInicial.Fecha,");
            SQL.AppendLine("         CargaInicial.Existencia,");
            SQL.AppendLine("         CargaInicial.Costo,");
            SQL.AppendLine("         CargaInicial.CodigoArticulo,");
            SQL.AppendLine("         CargaInicial.EsCargaInicial,");
            SQL.AppendLine("         CAST(CargaInicial.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         CargaInicial.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".CargaInicial");
            SQL.AppendLine("             INNER JOIN dbo.ArticuloInventario ON " + DbSchema + ".CargaInicial.CodigoArticulo = dbo.ArticuloInventario.codigo");
            SQL.AppendLine("      WHERE CargaInicial.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND CargaInicial.Consecutivo = @Consecutivo");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_CargaInicial_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_CargaInicial_B1.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_CargaInicial_B1.CodigoArticulo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_CargaInicial_B1.Descripcion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_CargaInicial_B1.Existencia,");
            SQL.AppendLine("      " + DbSchema + ".Gv_CargaInicial_B1.Costo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_CargaInicial_B1.LineaDeProducto");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_CargaInicial_B1");
            SQL.AppendLine("      INNER JOIN dbo.ArticuloInventario ON  " + DbSchema + ".Gv_CargaInicial_B1.CodigoArticulo = dbo.ArticuloInventario.codigo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_CargaInicial_B1.ConsecutivoCompania = dbo.ArticuloInventario.ConsecutivoCompania");
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
            SQL.AppendLine("      " + DbSchema + ".CargaInicial.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".CargaInicial.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".CargaInicial.CodigoArticulo,");
            SQL.AppendLine("      " + DbSchema + ".CargaInicial.Existencia,");
            SQL.AppendLine("      " + DbSchema + ".CargaInicial.Costo");
            SQL.AppendLine("      FROM " + DbSchema + ".CargaInicial");
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
            bool vResult = insDbo.Create(DbSchema + ".CargaInicial", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = true;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_CargaInicial_B1", SqlViewB1(), true) && vResult;
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CargaInicialINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CargaInicialUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CargaInicialDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CargaInicialGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CargaInicialSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CargaInicialGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CargaInicialGETUPDRecord", SqlSpGetUpdatedRecordParameters(), SqlSpGetUpdatedRecord(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".CargaInicial", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_CargaInicialINS");
            vResult = insSp.Drop(DbSchema + ".Gp_CargaInicialUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CargaInicialDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CargaInicialGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CargaInicialGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CargaInicialSCH") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CargaInicialGETUPDRecord") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_CargaInicial_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados

        private string SqlSpGetUpdatedRecordParameters() {
            StringBuilder vSQLparameters = new StringBuilder();
            vSQLparameters.AppendLine("@consecutivoCompania" + InsSql.NumericTypeForDb(10,0) +",");
            vSQLparameters.AppendLine("@consecutivo" + InsSql.NumericTypeForDb(10, 0));
            return vSQLparameters.ToString();
        }

        private string SqlSpGetUpdatedRecord() {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("SELECT ConsecutivoCompania, Consecutivo, Fecha, Existencia, Costo, CodigoArticulo, EsCargaInicial, CAST(CargaInicial.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            vSQL.AppendLine("FROM Adm.CargaInicial"); 
            vSQL.AppendLine("WHERE");
            vSQL.AppendLine("	ConsecutivoCompania = @consecutivoCompania");
            vSQL.AppendLine("	AND Consecutivo = @consecutivo;");
            return vSQL.ToString();
        }

    } //End of class clsCargaInicialED

} //End of namespace Galac.Adm.Dal.GestionCompras

