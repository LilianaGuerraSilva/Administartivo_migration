using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
//using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Dal.Venta {
    [LibMefDalComponentMetadata(typeof(clsCobroDeFacturaRapidaDetalleED))]
    public class clsCobroDeFacturaRapidaDetalleED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsCobroDeFacturaRapidaDetalleED(): base(){
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
            get { return "CobroDeFacturaRapidaDetalle"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("CobroDeFacturaRapidaDetalle", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnCobDeFacRapDetConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoFormaDelCobro" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT nnCobDeFacRapDetCodigoForm NOT NULL, ");
            SQL.AppendLine("MontoEfectivo" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobDeFacRapDetMoEf DEFAULT (0), ");
            SQL.AppendLine("MontoCheque" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobDeFacRapDetMoCh DEFAULT (0), ");
            SQL.AppendLine("MontoTarjeta" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobDeFacRapDetMoTa DEFAULT (0), ");
            SQL.AppendLine("MontoDeposito" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobDeFacRapDetMoDe DEFAULT (0), ");
            SQL.AppendLine("MontoAnticipo" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobDeFacRapDetMoAn DEFAULT (0), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_CobroDeFacturaRapidaDetalle PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, CodigoFormaDelCobro ASC)");
            SQL.AppendLine(",CONSTRAINT fk_CobroDeFacturaRapidaDetalleCobroDeFacturaRapida FOREIGN KEY (ConsecutivoCompania)");
            SQL.AppendLine("REFERENCES Adm.CobroDeFacturaRapida(ConsecutivoCompania, NumeroFactura)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_CobroDeFacturaRapidaDetalleFormaDelCobro FOREIGN KEY (CodigoFormaDelCobro)");
            SQL.AppendLine("REFERENCES SAW.FormaDelCobro(Codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT CobroDeFacturaRapidaDetalle.ConsecutivoCompania, CobroDeFacturaRapidaDetalle.CodigoFormaDelCobro, CobroDeFacturaRapidaDetalle.MontoEfectivo, CobroDeFacturaRapidaDetalle.MontoCheque");
            SQL.AppendLine(", CobroDeFacturaRapidaDetalle.MontoTarjeta, CobroDeFacturaRapidaDetalle.MontoDeposito, CobroDeFacturaRapidaDetalle.MontoAnticipo");
            SQL.AppendLine(", CobroDeFacturaRapidaDetalle.fldTimeStamp, CAST(CobroDeFacturaRapidaDetalle.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".CobroDeFacturaRapidaDetalle");
            SQL.AppendLine("INNER JOIN SAW.FormaDelCobro ON  " + DbSchema + ".CobroDeFacturaRapidaDetalle.CodigoFormaDelCobro = SAW.FormaDelCobro.Codigo");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoFormaDelCobro" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@MontoEfectivo" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoCheque" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoTarjeta" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoDeposito" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoAnticipo" + InsSql.DecimalTypeForDb(25, 4) + " = 0");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".CobroDeFacturaRapidaDetalle(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            CodigoFormaDelCobro,");
            SQL.AppendLine("            MontoEfectivo,");
            SQL.AppendLine("            MontoCheque,");
            SQL.AppendLine("            MontoTarjeta,");
            SQL.AppendLine("            MontoDeposito,");
            SQL.AppendLine("            MontoAnticipo)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @CodigoFormaDelCobro,");
            SQL.AppendLine("            @MontoEfectivo,");
            SQL.AppendLine("            @MontoCheque,");
            SQL.AppendLine("            @MontoTarjeta,");
            SQL.AppendLine("            @MontoDeposito,");
            SQL.AppendLine("            @MontoAnticipo)");
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
            SQL.AppendLine("@MontoEfectivo" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoCheque" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoTarjeta" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoDeposito" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoAnticipo" + InsSql.DecimalTypeForDb(25, 4) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CobroDeFacturaRapidaDetalle WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoFormaDelCobro = @CodigoFormaDelCobro)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CobroDeFacturaRapidaDetalle WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoFormaDelCobro = @CodigoFormaDelCobro");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_CobroDeFacturaRapidaDetalleCanBeUpdated @ConsecutivoCompania,@CodigoFormaDelCobro, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".CobroDeFacturaRapidaDetalle");
            SQL.AppendLine("            SET MontoEfectivo = @MontoEfectivo,");
            SQL.AppendLine("               MontoCheque = @MontoCheque,");
            SQL.AppendLine("               MontoTarjeta = @MontoTarjeta,");
            SQL.AppendLine("               MontoDeposito = @MontoDeposito,");
            SQL.AppendLine("               MontoAnticipo = @MontoAnticipo");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CobroDeFacturaRapidaDetalle WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoFormaDelCobro = @CodigoFormaDelCobro)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CobroDeFacturaRapidaDetalle WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoFormaDelCobro = @CodigoFormaDelCobro");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_CobroDeFacturaRapidaDetalleCanBeDeleted @ConsecutivoCompania,@CodigoFormaDelCobro, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".CobroDeFacturaRapidaDetalle");
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
            SQL.AppendLine("         MontoEfectivo,");
            SQL.AppendLine("         MontoCheque,");
            SQL.AppendLine("         MontoTarjeta,");
            SQL.AppendLine("         MontoDeposito,");
            SQL.AppendLine("         MontoAnticipo,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".CobroDeFacturaRapidaDetalle");
            SQL.AppendLine("      WHERE CobroDeFacturaRapidaDetalle.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND CobroDeFacturaRapidaDetalle.CodigoFormaDelCobro = @CodigoFormaDelCobro");
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
            SQL.AppendLine("        MontoEfectivo,");
            SQL.AppendLine("        MontoCheque,");
            SQL.AppendLine("        MontoTarjeta,");
            SQL.AppendLine("        MontoDeposito,");
            SQL.AppendLine("        MontoAnticipo,");
            SQL.AppendLine("        fldTimeStamp");
            SQL.AppendLine("    FROM CobroDeFacturaRapidaDetalle");
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
            SQL.AppendLine("	DELETE FROM CobroDeFacturaRapidaDetalle");
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
            SQL.AppendLine("	    EXEC Adm.Gp_CobroDeFacturaRapidaDetalleDelDet @ConsecutivoCompania = @ConsecutivoCompania, @CodigoFormaDelCobro = @CodigoFormaDelCobro");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO Adm.CobroDeFacturaRapidaDetalle(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        CodigoFormaDelCobro,");
			SQL.AppendLine("	        MontoEfectivo,");
			SQL.AppendLine("	        MontoCheque,");
			SQL.AppendLine("	        MontoTarjeta,");
			SQL.AppendLine("	        MontoDeposito,");
			SQL.AppendLine("	        MontoAnticipo)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @CodigoFormaDelCobro,");
			SQL.AppendLine("	        MontoEfectivo,");
			SQL.AppendLine("	        MontoCheque,");
			SQL.AppendLine("	        MontoTarjeta,");
			SQL.AppendLine("	        MontoDeposito,");
			SQL.AppendLine("	        MontoAnticipo");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataCobroDeFacturaRapidaDetalle/GpDetailCobroDeFacturaRapidaDetalle',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        MontoEfectivo " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        MontoCheque " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        MontoTarjeta " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        MontoDeposito " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        MontoAnticipo " + InsSql.DecimalTypeForDb(25, 4) + ") AS XmlDocDetailOfCobroDeFacturaRapida");
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
            bool vResult = insDbo.Create(DbSchema + ".CobroDeFacturaRapidaDetalle", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_CobroDeFacturaRapidaDetalle_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaDetalleINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaDetalleUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaDetalleDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaDetalleGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaDetalleSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaDetalleDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobroDeFacturaRapidaDetalleInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".CobroDeFacturaRapidaDetalle", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaDetalleINS");
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaDetalleUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaDetalleDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaDetalleGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaDetalleInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaDetalleDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobroDeFacturaRapidaDetalleSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_CobroDeFacturaRapidaDetalle_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCobroDeFacturaRapidaDetalleED

} //End of namespace Galac.Adm.Dal.Venta

