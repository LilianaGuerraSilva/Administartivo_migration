using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.GestionProduccion;

namespace Galac.Adm.Dal.GestionProduccion {
    [LibMefDalComponentMetadata(typeof(clsListaDeMaterialesED))]
    public class clsListaDeMaterialesED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsListaDeMaterialesED(): base(){
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
            get { return "ListaDeMateriales"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("ListaDeMateriales", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnLisDeMatConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnLisDeMatConsecutiv NOT NULL, ");
            SQL.AppendLine("Codigo" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT nnLisDeMatCodigo NOT NULL, ");
            SQL.AppendLine("Nombre" + InsSql.VarCharTypeForDb(255) + " CONSTRAINT d_LisDeMatNo DEFAULT (''), ");
            SQL.AppendLine("CodigoArticuloInventario" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_LisDeMatCoArIn DEFAULT (''), ");
            SQL.AppendLine("FechaCreacion" + InsSql.DateTypeForDb() + " CONSTRAINT nnLisDeMatFechaCreac NOT NULL, ");
            SQL.AppendLine("ManejaMerma" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnLisDeMatManejaMerm NOT NULL, ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(20) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_ListaDeMateriales PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Consecutivo ASC)");
            SQL.AppendLine(",CONSTRAINT u_LisDeMatniaigo UNIQUE NONCLUSTERED (ConsecutivoCompania,Codigo)");
            SQL.AppendLine(",CONSTRAINT u_LisDeMatniabre UNIQUE NONCLUSTERED (ConsecutivoCompania,Nombre)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT ListaDeMateriales.ConsecutivoCompania, ListaDeMateriales.Consecutivo, ListaDeMateriales.Codigo, ListaDeMateriales.Nombre");
            SQL.AppendLine(", ListaDeMateriales.CodigoArticuloInventario, ListaDeMateriales.FechaCreacion, ListaDeMateriales.ManejaMerma, ListaDeMateriales.NombreOperador, ListaDeMateriales.FechaUltimaModificacion");
            SQL.AppendLine(", ListaDeMateriales.fldTimeStamp, CAST(ListaDeMateriales.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".ListaDeMateriales");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(30) + " = '',");
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(255) + " = '',");
            SQL.AppendLine("@CodigoArticuloInventario" + InsSql.VarCharTypeForDb(30) + " = '',");
            SQL.AppendLine("@FechaCreacion" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@ManejaMerma" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(20) + " = '',");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".ListaDeMateriales(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            Codigo,");
            SQL.AppendLine("            Nombre,");
            SQL.AppendLine("            CodigoArticuloInventario,");
            SQL.AppendLine("            FechaCreacion,");
            SQL.AppendLine("            ManejaMerma,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @Codigo,");
            SQL.AppendLine("            @Nombre,");
            SQL.AppendLine("            @CodigoArticuloInventario,");
            SQL.AppendLine("            @FechaCreacion,");
            SQL.AppendLine("            @ManejaMerma,");
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
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(255) + ",");
            SQL.AppendLine("@CodigoArticuloInventario" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@FechaCreacion" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@ManejaMerma" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(20) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".ListaDeMateriales WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".ListaDeMateriales WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_ListaDeMaterialesCanBeUpdated @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".ListaDeMateriales");
            SQL.AppendLine("            SET Codigo = @Codigo,");
            SQL.AppendLine("               Nombre = @Nombre,");
            SQL.AppendLine("               CodigoArticuloInventario = @CodigoArticuloInventario,");
            SQL.AppendLine("               FechaCreacion = @FechaCreacion,");
            SQL.AppendLine("               ManejaMerma = @ManejaMerma,");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".ListaDeMateriales WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".ListaDeMateriales WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_ListaDeMaterialesCanBeDeleted @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".ListaDeMateriales");
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
            SQL.AppendLine("         ListaDeMateriales.ConsecutivoCompania,");
            SQL.AppendLine("         ListaDeMateriales.Consecutivo,");
            SQL.AppendLine("         ListaDeMateriales.Codigo,");
            SQL.AppendLine("         ListaDeMateriales.Nombre,");
            SQL.AppendLine("         ListaDeMateriales.CodigoArticuloInventario,");
            SQL.AppendLine("         ListaDeMateriales.FechaCreacion,");
            SQL.AppendLine("         ListaDeMateriales.ManejaMerma,");
            SQL.AppendLine("         ListaDeMateriales.NombreOperador,");
            SQL.AppendLine("         ListaDeMateriales.FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(ListaDeMateriales.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         ListaDeMateriales.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".ListaDeMateriales");
            SQL.AppendLine("      WHERE ListaDeMateriales.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND ListaDeMateriales.Consecutivo = @Consecutivo");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_ListaDeMateriales_B1.Codigo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_ListaDeMateriales_B1.Nombre,");
            SQL.AppendLine("      " + DbSchema + ".Gv_ListaDeMateriales_B1.CodigoArticuloInventario,");            
            SQL.AppendLine("      " + DbSchema + ".Gv_ListaDeMateriales_B1.FechaCreacion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_ListaDeMateriales_B1.ManejaMerma,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      '''' AS DescripcionArticuloInventario,");
            SQL.AppendLine("      " + DbSchema + ".Gv_ListaDeMateriales_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_ListaDeMateriales_B1.Consecutivo");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_ListaDeMateriales_B1");            
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
            SQL.AppendLine("      " + DbSchema + ".ListaDeMateriales.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".ListaDeMateriales.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".ListaDeMateriales.Codigo,");
            SQL.AppendLine("      " + DbSchema + ".ListaDeMateriales.Nombre,");
            SQL.AppendLine("      " + DbSchema + ".ListaDeMateriales.CodigoArticuloInventario,");
            SQL.AppendLine("      " + DbSchema + ".ListaDeMateriales.FechaCreacion");
            //SQL.AppendLine("      ," + DbSchema + ".ListaDeMateriales.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".ListaDeMateriales");
            SQL.AppendLine("      WHERE " + DbSchema + ".ListaDeMateriales.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("          AND  " + DbSchema + ".ListaDeMateriales.Consecutivo IN (");
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
            bool vResult = insDbo.Create(DbSchema + ".ListaDeMateriales", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_ListaDeMateriales_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ListaDeMaterialesINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ListaDeMaterialesUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ListaDeMaterialesDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ListaDeMaterialesGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ListaDeMaterialesSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ListaDeMaterialesGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
            insSps.Dispose();
            return vResult;
        }

        public bool InstalarTabla() {
            bool vResult = false;
            if (CrearTabla()) {
                CrearVistas();
                CrearProcedimientos();
                clsListaDeMaterialesDetalleArticuloED insDetailLisDeMatDetArt = new clsListaDeMaterialesDetalleArticuloED();
                vResult = insDetailLisDeMatDetArt.InstalarTabla();
                clsListaDeMaterialesDetalleSalidasED insDetailLisDeMatDetSal = new clsListaDeMaterialesDetalleSalidasED();
                vResult = vResult && insDetailLisDeMatDetSal.InstalarTabla();
            }
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = false;
            if (insDbo.Exists(DbSchema + ".ListaDeMateriales", eDboType.Tabla)) {
                CrearVistas();
                CrearProcedimientos();
                vResult = new clsListaDeMaterialesDetalleArticuloED().InstalarVistasYSps();
                vResult = vResult && new clsListaDeMaterialesDetalleSalidasED().InstalarVistasYSps();
            }
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = false;
            LibStoredProc insSp = new LibStoredProc();
            LibViews insVista = new LibViews();
            vResult = new clsListaDeMaterialesDetalleArticuloED().BorrarVistasYSps();
            vResult = new clsListaDeMaterialesDetalleSalidasED().BorrarVistasYSps() && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ListaDeMaterialesINS") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ListaDeMaterialesUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ListaDeMaterialesDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ListaDeMaterialesGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ListaDeMaterialesGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ListaDeMaterialesSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_ListaDeMateriales_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsListaDeMaterialesED

} //End of namespace Galac.Adm.Dal.GestionProduccion

