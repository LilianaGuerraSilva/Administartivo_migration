using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.GestionProduccion;

namespace Galac.Adm.Dal.GestionProduccion {
    [LibMefDalComponentMetadata(typeof(clsListaDeMaterialesDetalleSalidasED))]
    public class clsListaDeMaterialesDetalleSalidasED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsListaDeMaterialesDetalleSalidasED(): base(){
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
            get { return "ListaDeMaterialesDetalleSalidas"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("ListaDeMaterialesDetalleSalidas", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnLisDeMatDetSalConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoListaDeMateriales" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnLisDeMatDetSalConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnLisDeMatDetSalConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoArticuloInventario" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_LisDeMatDetSalCoArIn DEFAULT (''), ");
            SQL.AppendLine("Cantidad" + InsSql.DecimalTypeForDb(25, 8) + " CONSTRAINT nnLisDeMatDetSalCantidad NOT NULL, ");
            SQL.AppendLine("PorcentajeDeCosto" + InsSql.DecimalTypeForDb(25, 8) + " CONSTRAINT nnLisDeMatDetSalPorcentaje NOT NULL, ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_ListaDeMaterialesDetalleSalidas PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, ConsecutivoListaDeMateriales ASC, Consecutivo ASC)");
            SQL.AppendLine(",CONSTRAINT fk_ListaDeMaterialesDetalleSalidasListaDeMateriales FOREIGN KEY (ConsecutivoCompania, ConsecutivoListaDeMateriales)");
            SQL.AppendLine("REFERENCES Adm.ListaDeMateriales(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT ListaDeMaterialesDetalleSalidas.ConsecutivoCompania, ListaDeMaterialesDetalleSalidas.ConsecutivoListaDeMateriales, ListaDeMaterialesDetalleSalidas.Consecutivo, ListaDeMaterialesDetalleSalidas.CodigoArticuloInventario");
            SQL.AppendLine(", ListaDeMaterialesDetalleSalidas.Cantidad, ListaDeMaterialesDetalleSalidas.PorcentajeDeCosto");
            SQL.AppendLine(", dbo.ArticuloInventario.UnidadDeVenta AS UnidadDeVenta");
            SQL.AppendLine(", ListaDeMaterialesDetalleSalidas.fldTimeStamp, CAST(ListaDeMaterialesDetalleSalidas.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".ListaDeMaterialesDetalleSalidas");
            SQL.AppendLine("INNER JOIN dbo.ArticuloInventario ON  " + DbSchema + ".ListaDeMaterialesDetalleSalidas.CodigoArticuloInventario = dbo.ArticuloInventario.Codigo");
            SQL.AppendLine("      AND " + DbSchema + ".ListaDeMaterialesDetalleSalidas.ConsecutivoCompania = dbo.ArticuloInventario.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoListaDeMateriales" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticuloInventario" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 8) + " = 0,");
            SQL.AppendLine("@PorcentajeDeCosto" + InsSql.DecimalTypeForDb(25, 8) + " = 0");
            return SQL.ToString();
        }

        private string SqlSpIns() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".ListaDeMaterialesDetalleSalidas(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            ConsecutivoListaDeMateriales,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            CodigoArticuloInventario,");
            SQL.AppendLine("            Cantidad,");
            SQL.AppendLine("            PorcentajeDeCosto)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @ConsecutivoListaDeMateriales,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @CodigoArticuloInventario,");
            SQL.AppendLine("            @Cantidad,");
            SQL.AppendLine("            @PorcentajeDeCosto)");
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
            SQL.AppendLine("@ConsecutivoListaDeMateriales" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticuloInventario" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("@PorcentajeDeCosto" + InsSql.DecimalTypeForDb(25, 8) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".ListaDeMaterialesDetalleSalidas WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoListaDeMateriales = @ConsecutivoListaDeMateriales AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".ListaDeMaterialesDetalleSalidas WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoListaDeMateriales = @ConsecutivoListaDeMateriales AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_ListaDeMaterialesDetalleSalidasCanBeUpdated @ConsecutivoCompania,@ConsecutivoListaDeMateriales,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".ListaDeMaterialesDetalleSalidas");
            SQL.AppendLine("            SET CodigoArticuloInventario = @CodigoArticuloInventario,");
            SQL.AppendLine("               Cantidad = @Cantidad,");
            SQL.AppendLine("               PorcentajeDeCosto = @PorcentajeDeCosto");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoListaDeMateriales = @ConsecutivoListaDeMateriales");
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
            SQL.AppendLine("@ConsecutivoListaDeMateriales" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".ListaDeMaterialesDetalleSalidas WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoListaDeMateriales = @ConsecutivoListaDeMateriales AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".ListaDeMaterialesDetalleSalidas WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoListaDeMateriales = @ConsecutivoListaDeMateriales AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_ListaDeMaterialesDetalleSalidasCanBeDeleted @ConsecutivoCompania,@ConsecutivoListaDeMateriales,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".ListaDeMaterialesDetalleSalidas");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoListaDeMateriales = @ConsecutivoListaDeMateriales");
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
            SQL.AppendLine("@ConsecutivoListaDeMateriales" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         ConsecutivoListaDeMateriales,");
            SQL.AppendLine("         Consecutivo,");
            SQL.AppendLine("         CodigoArticuloInventario,");
            SQL.AppendLine("         Cantidad,");
            SQL.AppendLine("         PorcentajeDeCosto,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".ListaDeMaterialesDetalleSalidas");
            SQL.AppendLine("      WHERE ListaDeMaterialesDetalleSalidas.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND ListaDeMaterialesDetalleSalidas.ConsecutivoListaDeMateriales = @ConsecutivoListaDeMateriales");
            SQL.AppendLine("         AND ListaDeMaterialesDetalleSalidas.Consecutivo = @Consecutivo");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoListaDeMateriales" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpSelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SELECT ");
            SQL.AppendLine("        Adm.ListaDeMaterialesDetalleSalidas.ConsecutivoCompania,");
            SQL.AppendLine("        Adm.ListaDeMaterialesDetalleSalidas.ConsecutivoListaDeMateriales,");
            SQL.AppendLine("        Adm.ListaDeMaterialesDetalleSalidas.Consecutivo,");
            SQL.AppendLine("        Adm.ListaDeMaterialesDetalleSalidas.CodigoArticuloInventario,");
            SQL.AppendLine("        dbo.ArticuloInventario.Descripcion AS DescripcionArticuloInventario,");
            SQL.AppendLine("        Adm.ListaDeMaterialesDetalleSalidas.Cantidad,");
            SQL.AppendLine("        Adm.ListaDeMaterialesDetalleSalidas.PorcentajeDeCosto,");
            SQL.AppendLine("        dbo.ArticuloInventario.UnidadDeVenta AS UnidadDeVenta,");
            SQL.AppendLine("        dbo.ArticuloInventario.TipoArticuloInv AS TipoArticuloInv,");
            SQL.AppendLine("        Adm.ListaDeMaterialesDetalleSalidas.fldTimeStamp");
            SQL.AppendLine("    FROM ListaDeMaterialesDetalleSalidas");
            SQL.AppendLine("    INNER JOIN dbo.ArticuloInventario ON dbo.ArticuloInventario.ConsecutivoCompania = Adm.ListaDeMaterialesDetalleSalidas.ConsecutivoCompania and  dbo.ArticuloInventario.Codigo = Adm.ListaDeMaterialesDetalleSalidas.CodigoArticuloInventario");
            SQL.AppendLine(" 	WHERE Adm.ListaDeMaterialesDetalleSalidas.ConsecutivoListaDeMateriales = @ConsecutivoListaDeMateriales");
            SQL.AppendLine(" 	AND Adm.ListaDeMaterialesDetalleSalidas.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoListaDeMateriales" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpDelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	DELETE FROM ListaDeMaterialesDetalleSalidas");
            SQL.AppendLine(" 	WHERE ConsecutivoListaDeMateriales = @ConsecutivoListaDeMateriales");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInsDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoListaDeMateriales" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@XmlDataDetail" + InsSql.XmlTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpInsDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SET NOCOUNT ON;");
            SQL.AppendLine("	DECLARE @ReturnValue  " + InsSql.NumericTypeForDb(10, 0));
	        SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
	        SQL.AppendLine("	    BEGIN");
            SQL.AppendLine("	    EXEC Adm.Gp_ListaDeMaterialesDetalleSalidasDelDet @ConsecutivoCompania = @ConsecutivoCompania, @ConsecutivoListaDeMateriales = @ConsecutivoListaDeMateriales");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO Adm.ListaDeMaterialesDetalleSalidas(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        ConsecutivoListaDeMateriales,");
			SQL.AppendLine("	        Consecutivo,");
			SQL.AppendLine("	        CodigoArticuloInventario,");
			SQL.AppendLine("	        Cantidad,");
			SQL.AppendLine("	        PorcentajeDeCosto)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @ConsecutivoListaDeMateriales,");
			SQL.AppendLine("	        Consecutivo,");
			SQL.AppendLine("	        CodigoArticuloInventario,");
			SQL.AppendLine("	        Cantidad,");
			SQL.AppendLine("	        PorcentajeDeCosto");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataListaDeMaterialesDetalleSalidas/GpDetailListaDeMaterialesDetalleSalidas',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        Consecutivo " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        CodigoArticuloInventario " + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("	        Cantidad " + InsSql.DecimalTypeForDb(25, 8) + ",");
            SQL.AppendLine("	        PorcentajeDeCosto " + InsSql.DecimalTypeForDb(25, 8) + ") AS XmlDocDetailOfListaDeMateriales");
            SQL.AppendLine("	    EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("	    SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("	    RETURN @ReturnValue");
	        SQL.AppendLine("	END");
	        SQL.AppendLine("	ELSE");
            SQL.AppendLine("	    RETURN -1");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".ListaDeMaterialesDetalleSalidas", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_ListaDeMaterialesDetalleSalidas_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ListaDeMaterialesDetalleSalidasINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ListaDeMaterialesDetalleSalidasUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ListaDeMaterialesDetalleSalidasDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ListaDeMaterialesDetalleSalidasGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ListaDeMaterialesDetalleSalidasSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ListaDeMaterialesDetalleSalidasDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ListaDeMaterialesDetalleSalidasInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".ListaDeMaterialesDetalleSalidas", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_ListaDeMaterialesDetalleSalidasINS");
            vResult = insSp.Drop(DbSchema + ".Gp_ListaDeMaterialesDetalleSalidasUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ListaDeMaterialesDetalleSalidasDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ListaDeMaterialesDetalleSalidasGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ListaDeMaterialesDetalleSalidasInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ListaDeMaterialesDetalleSalidasDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ListaDeMaterialesDetalleSalidasSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_ListaDeMaterialesDetalleSalidas_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsListaDeMaterialesDetalleSalidasED

} //End of namespace Galac.Adm.Dal.GestionProduccion

