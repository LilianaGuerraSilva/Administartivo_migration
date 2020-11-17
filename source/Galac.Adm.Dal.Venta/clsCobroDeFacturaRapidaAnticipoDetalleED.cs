using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Dal.Venta {
    [LibMefDalComponentMetadata(typeof(clsCobroDeFacturaRapidaAnticipoDetalleED))]
    public class clsCobroDeFacturaRapidaAnticipoDetalleED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsCobroDeFacturaRapidaAnticipoDetalleED(): base(){
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
            get { return "CobroDeFacturaRapidaAnticipoDetalle"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("CobroDeFacturaRapidaAnticipoDetalle", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnCobDeFacRapAntDetConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoFormaDelCobro" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT nnCobDeFacRapAntDetCodigoForm NOT NULL, ");
            SQL.AppendLine("CodigoAnticipo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnCobDeFacRapAntDetCodigoAnti NOT NULL, ");
            SQL.AppendLine("NumeroAnticipo" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_CobDeFacRapAntDetNuAn DEFAULT (''), ");
            SQL.AppendLine("Monto" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobDeFacRapAntDetMo DEFAULT (0), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_CobroDeFacturaRapidaAnticipoDetalle PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, CodigoFormaDelCobro ASC)");
            SQL.AppendLine(",CONSTRAINT fk_CobroDeFacturaRapidaAnticipoDetalleCobroDeFacturaRapidaAnticipo FOREIGN KEY (ConsecutivoCompania)");
            SQL.AppendLine("REFERENCES Adm.CobroDeFacturaRapidaAnticipo(ConsecutivoCompania, NumeroFactura)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_CobroDeFacturaRapidaAnticipoDetalleFormaDelCobro FOREIGN KEY (CodigoFormaDelCobro)");
            SQL.AppendLine("REFERENCES SAW.FormaDelCobro(Codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_CobroDeFacturaRapidaAnticipoDetalleAnticipo FOREIGN KEY (ConsecutivoCompania, CodigoAnticipo)");
            SQL.AppendLine("REFERENCES dbo.Anticipo(ConsecutivoCompania, codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT CobroDeFacturaRapidaAnticipoDetalle.ConsecutivoCompania, CobroDeFacturaRapidaAnticipoDetalle.CodigoFormaDelCobro, CobroDeFacturaRapidaAnticipoDetalle.CodigoAnticipo, CobroDeFacturaRapidaAnticipoDetalle.NumeroAnticipo");
            SQL.AppendLine(", CobroDeFacturaRapidaAnticipoDetalle.Monto");
            SQL.AppendLine(", CobroDeFacturaRapidaAnticipoDetalle.fldTimeStamp, CAST(CobroDeFacturaRapidaAnticipoDetalle.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".CobroDeFacturaRapidaAnticipoDetalle");
            SQL.AppendLine("INNER JOIN SAW.FormaDelCobro ON  " + DbSchema + ".CobroDeFacturaRapidaAnticipoDetalle.CodigoFormaDelCobro = SAW.FormaDelCobro.Codigo");
            SQL.AppendLine("INNER JOIN dbo.Anticipo ON  " + DbSchema + ".CobroDeFacturaRapidaAnticipoDetalle.CodigoAnticipo = dbo.Anticipo.codigo");
            SQL.AppendLine("      AND " + DbSchema + ".CobroDeFacturaRapidaAnticipoDetalle.ConsecutivoCompania = dbo.Anticipo.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoFormaDelCobro" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@CodigoAnticipo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroAnticipo" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@Monto" + InsSql.DecimalTypeForDb(25, 4) + " = 0");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".CobroDeFacturaRapidaAnticipoDetalle(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            CodigoFormaDelCobro,");
            SQL.AppendLine("            CodigoAnticipo,");
            SQL.AppendLine("            NumeroAnticipo,");
            SQL.AppendLine("            Monto)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @CodigoFormaDelCobro,");
            SQL.AppendLine("            @CodigoAnticipo,");
            SQL.AppendLine("            @NumeroAnticipo,");
            SQL.AppendLine("            @Monto)");
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
            SQL.AppendLine("@CodigoFormaDelCobro" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@CodigoAnticipo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroAnticipo" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@Monto" + InsSql.DecimalTypeForDb(25, 4) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CobroDeFacturaRapidaAnticipoDetalle WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoFormaDelCobro = @CodigoFormaDelCobro)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CobroDeFacturaRapidaAnticipoDetalle WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoFormaDelCobro = @CodigoFormaDelCobro");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_CobroDeFacturaRapidaAnticipoDetalleCanBeUpdated @ConsecutivoCompania,@CodigoFormaDelCobro, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".CobroDeFacturaRapidaAnticipoDetalle");
            SQL.AppendLine("            SET CodigoAnticipo = @CodigoAnticipo,");
            SQL.AppendLine("               NumeroAnticipo = @NumeroAnticipo,");
            SQL.AppendLine("               Monto = @Monto");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND CodigoFormaDelCobro = @CodigoFormaDelCobro");
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
            SQL.AppendLine("@CodigoFormaDelCobro" + InsSql.VarCharTypeForDb(5) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CobroDeFacturaRapidaAnticipoDetalle WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoFormaDelCobro = @CodigoFormaDelCobro)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CobroDeFacturaRapidaAnticipoDetalle WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoFormaDelCobro = @CodigoFormaDelCobro");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_CobroDeFacturaRapidaAnticipoDetalleCanBeDeleted @ConsecutivoCompania,@CodigoFormaDelCobro, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".CobroDeFacturaRapidaAnticipoDetalle");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND CodigoFormaDelCobro = @CodigoFormaDelCobro");
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
            SQL.AppendLine("@CodigoFormaDelCobro" + InsSql.VarCharTypeForDb(5));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         CodigoFormaDelCobro,");
            SQL.AppendLine("         CodigoAnticipo,");
            SQL.AppendLine("         NumeroAnticipo,");
            SQL.AppendLine("         Monto,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".CobroDeFacturaRapidaAnticipoDetalle");
            SQL.AppendLine("      WHERE CobroDeFacturaRapidaAnticipoDetalle.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND CobroDeFacturaRapidaAnticipoDetalle.CodigoFormaDelCobro = @CodigoFormaDelCobro");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpSelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SELECT ");
            SQL.AppendLine("        ConsecutivoCompania,");
            SQL.AppendLine("        CodigoFormaDelCobro,");
            SQL.AppendLine("        CodigoAnticipo,");
            SQL.AppendLine("        NumeroAnticipo,");
            SQL.AppendLine("        Monto,");
            SQL.AppendLine("        fldTimeStamp");
            SQL.AppendLine("    FROM CobroDeFacturaRapidaAnticipoDetalle");
            SQL.AppendLine(" 	WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpDelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	DELETE FROM CobroDeFacturaRapidaAnticipoDetalle");
            SQL.AppendLine(" 	WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInsDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@XmlDataDetail" + InsSql.XmlTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpInsDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SET NOCOUNT ON;");
            SQL.AppendLine("	DECLARE @ReturnValue  " + InsSql.NumericTypeForDb(10, 0));
	        SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
	        SQL.AppendLine("	    BEGIN");
            SQL.AppendLine("	    EXEC Adm.Gp_CobroDeFacturaRapidaAnticipoDetalleDelDet @ConsecutivoCompania = @ConsecutivoCompania, @CodigoFormaDelCobro = @CodigoFormaDelCobro");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO Adm.CobroDeFacturaRapidaAnticipoDetalle(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        CodigoFormaDelCobro,");
			SQL.AppendLine("	        CodigoAnticipo,");
			SQL.AppendLine("	        NumeroAnticipo,");
			SQL.AppendLine("	        Monto)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @CodigoFormaDelCobro,");
			SQL.AppendLine("	        CodigoAnticipo,");
			SQL.AppendLine("	        NumeroAnticipo,");
			SQL.AppendLine("	        Monto");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataCobroDeFacturaRapidaAnticipoDetalle/GpDetailCobroDeFacturaRapidaAnticipoDetalle',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        CodigoAnticipo " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        NumeroAnticipo " + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("	        Monto " + InsSql.DecimalTypeForDb(25, 4) + ") AS XmlDocDetailOfCobroDeFacturaRapidaAnticipo");
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
            bool vResult = insDbo.Create(DbSchema + ".CobroDeFacturaRapidaAnticipoDetalle", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_CobroDeFacturaRapidaAnticipoDetalle_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaAnticipoDetalleINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaAnticipoDetalleUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaAnticipoDetalleDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaAnticipoDetalleGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaAnticipoDetalleSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaAnticipoDetalleDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaAnticipoDetalleInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".CobroDeFacturaRapidaAnticipoDetalle", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaAnticipoDetalleINS");
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaAnticipoDetalleUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaAnticipoDetalleDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaAnticipoDetalleGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaAnticipoDetalleInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaAnticipoDetalleDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaAnticipoDetalleSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_CobroDeFacturaRapidaAnticipoDetalle_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCobroDeFacturaRapidaAnticipoDetalleED

} //End of namespace Galac.Adm.Dal.Venta

