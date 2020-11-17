using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Dal.CajaChica {
    public class clsRenglonImpuestoMunicipalRetED: LibED {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsRenglonImpuestoMunicipalRetED(): base(){
            DbSchema = "dbo";
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("RenglonImpuestoMunicipalRet", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnRenImpMunRetConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnRenImpMunRetConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoCxp" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnRenImpMunRetConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoRetencion" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT nnRenImpMunRetCodigoRete NOT NULL, ");
            SQL.AppendLine("MontoBaseImponible" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnRenImpMunRetMontoBaseI NOT NULL, ");
            SQL.AppendLine("AlicuotaRetencion" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnRenImpMunRetAlicuotaRe NOT NULL, ");
            SQL.AppendLine("MontoRetencion" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnRenImpMunRetMontoReten NOT NULL, ");
            SQL.AppendLine("TipoDeTransaccion" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_RenImpMunRetTiDeTr DEFAULT ('0'), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_RenglonImpuestoMunicipalRet PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Consecutivo ASC)");
            SQL.AppendLine(",CONSTRAINT fk_RenglonImpuestoMunicipalRetCxP FOREIGN KEY (ConsecutivoCompania)");
            SQL.AppendLine("REFERENCES dbo.CxP(ConsecutivoCompania, ConsecutivoCxP)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT RenglonImpuestoMunicipalRet.ConsecutivoCompania, RenglonImpuestoMunicipalRet.Consecutivo, RenglonImpuestoMunicipalRet.ConsecutivoCxp, RenglonImpuestoMunicipalRet.CodigoRetencion");
            SQL.AppendLine(", RenglonImpuestoMunicipalRet.MontoBaseImponible, RenglonImpuestoMunicipalRet.AlicuotaRetencion, RenglonImpuestoMunicipalRet.MontoRetencion, RenglonImpuestoMunicipalRet.TipoDeTransaccion, " + DbSchema + ".Gv_EnumTipoDeTransaccionMunicipal.StrValue AS TipoDeTransaccionStr");
            SQL.AppendLine(", RenglonImpuestoMunicipalRet.NombreOperador, RenglonImpuestoMunicipalRet.FechaUltimaModificacion");
            SQL.AppendLine(", Comun.ClasificadorActividadEconomica.CodigoActividad AS CodigoActividad");
            SQL.AppendLine(", RenglonImpuestoMunicipalRet.fldTimeStamp, CAST(RenglonImpuestoMunicipalRet.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".RenglonImpuestoMunicipalRet");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoDeTransaccionMunicipal");
            SQL.AppendLine("ON " + DbSchema + ".RenglonImpuestoMunicipalRet.TipoDeTransaccion COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDeTransaccionMunicipal.DbValue");
            SQL.AppendLine("INNER JOIN Comun.ClasificadorActividadEconomica ON  " + DbSchema + ".RenglonImpuestoMunicipalRet.CodigoRetencion = Comun.ClasificadorActividadEconomica.Codigo");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoCxp" + InsSql.NumericTypeForDb(10, 0) + " = 0,");
            SQL.AppendLine("@CodigoRetencion" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@MontoBaseImponible" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@AlicuotaRetencion" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoRetencion" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TipoDeTransaccion" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + " = '',");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".RenglonImpuestoMunicipalRet(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            ConsecutivoCxp,");
            SQL.AppendLine("            CodigoRetencion,");
            SQL.AppendLine("            MontoBaseImponible,");
            SQL.AppendLine("            AlicuotaRetencion,");
            SQL.AppendLine("            MontoRetencion,");
            SQL.AppendLine("            TipoDeTransaccion,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @ConsecutivoCxp,");
            SQL.AppendLine("            @CodigoRetencion,");
            SQL.AppendLine("            @MontoBaseImponible,");
            SQL.AppendLine("            @AlicuotaRetencion,");
            SQL.AppendLine("            @MontoRetencion,");
            SQL.AppendLine("            @TipoDeTransaccion,");
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
            SQL.AppendLine("@ConsecutivoCxp" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoRetencion" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@MontoBaseImponible" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@AlicuotaRetencion" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoRetencion" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TipoDeTransaccion" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".RenglonImpuestoMunicipalRet WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".RenglonImpuestoMunicipalRet WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_RenglonImpuestoMunicipalRetCanBeUpdated @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".RenglonImpuestoMunicipalRet");
            SQL.AppendLine("            SET ConsecutivoCxp = @ConsecutivoCxp,");
            SQL.AppendLine("               CodigoRetencion = @CodigoRetencion,");
            SQL.AppendLine("               MontoBaseImponible = @MontoBaseImponible,");
            SQL.AppendLine("               AlicuotaRetencion = @AlicuotaRetencion,");
            SQL.AppendLine("               MontoRetencion = @MontoRetencion,");
            SQL.AppendLine("               TipoDeTransaccion = @TipoDeTransaccion,");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".RenglonImpuestoMunicipalRet WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".RenglonImpuestoMunicipalRet WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_RenglonImpuestoMunicipalRetCanBeDeleted @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".RenglonImpuestoMunicipalRet");
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
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         Consecutivo,");
            SQL.AppendLine("         ConsecutivoCxp,");
            SQL.AppendLine("         CodigoRetencion,");
            SQL.AppendLine("         MontoBaseImponible,");
            SQL.AppendLine("         AlicuotaRetencion,");
            SQL.AppendLine("         MontoRetencion,");
            SQL.AppendLine("         TipoDeTransaccion,");
            SQL.AppendLine("         NombreOperador,");
            SQL.AppendLine("         FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".RenglonImpuestoMunicipalRet");
            SQL.AppendLine("      WHERE RenglonImpuestoMunicipalRet.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND RenglonImpuestoMunicipalRet.Consecutivo = @Consecutivo");
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
            SQL.AppendLine("        Consecutivo,");
            SQL.AppendLine("        ConsecutivoCxp,");
            SQL.AppendLine("        CodigoRetencion,");
            SQL.AppendLine("        MontoBaseImponible,");
            SQL.AppendLine("        AlicuotaRetencion,");
            SQL.AppendLine("        MontoRetencion,");
            SQL.AppendLine("        TipoDeTransaccion,");
            SQL.AppendLine("        NombreOperador,");
            SQL.AppendLine("        FechaUltimaModificacion,");
            SQL.AppendLine("        fldTimeStamp");
            SQL.AppendLine("    FROM RenglonImpuestoMunicipalRet");
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
            SQL.AppendLine("	DELETE FROM RenglonImpuestoMunicipalRet");
            SQL.AppendLine(" 	WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInsDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0)+ ",");
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
            SQL.AppendLine("	    EXEC dbo.Gp_RenglonImpuestoMunicipalRetDelDet @ConsecutivoCompania = @ConsecutivoCompania");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO dbo.RenglonImpuestoMunicipalRet(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        Consecutivo,");
			SQL.AppendLine("	        ConsecutivoCxp,");
			SQL.AppendLine("	        CodigoRetencion,");
			SQL.AppendLine("	        MontoBaseImponible,");
			SQL.AppendLine("	        AlicuotaRetencion,");
			SQL.AppendLine("	        MontoRetencion,");
			SQL.AppendLine("	        TipoDeTransaccion,");
			SQL.AppendLine("	        NombreOperador,");
			SQL.AppendLine("	        FechaUltimaModificacion)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			//SQL.AppendLine("	        @Consecutivo,");
            SQL.AppendLine("	        Consecutivo,");
			SQL.AppendLine("	        ConsecutivoCxp,");
			SQL.AppendLine("	        CodigoRetencion,");
			SQL.AppendLine("	        MontoBaseImponible,");
			SQL.AppendLine("	        AlicuotaRetencion,");
			SQL.AppendLine("	        MontoRetencion,");
			SQL.AppendLine("	        TipoDeTransaccion,");
			SQL.AppendLine("	        NombreOperador,");
			SQL.AppendLine("	        FechaUltimaModificacion");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataRenglonImpuestoMunicipalRet/GpDetailRenglonImpuestoMunicipalRet',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        ConsecutivoCxp " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        CodigoRetencion " + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("	        MontoBaseImponible " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        AlicuotaRetencion " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        MontoRetencion " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        TipoDeTransaccion " + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("	        NombreOperador " + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("	        FechaUltimaModificacion " + InsSql.DateTypeForDb() + ") AS XmlDocDetailOfCxP");
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
            bool vResult = insDbo.Create(DbSchema + ".RenglonImpuestoMunicipalRet", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDeTransaccionMunicipal", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDeTransaccionMunicipal), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_RenglonImpuestoMunicipalRet_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonImpuestoMunicipalRetINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonImpuestoMunicipalRetUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonImpuestoMunicipalRetDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonImpuestoMunicipalRetGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            //vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonImpuestoMunicipalRetSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            //vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonImpuestoMunicipalRetDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            //vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonImpuestoMunicipalRetInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".RenglonImpuestoMunicipalRet", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonImpuestoMunicipalRetINS");
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonImpuestoMunicipalRetUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonImpuestoMunicipalRetDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonImpuestoMunicipalRetGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonImpuestoMunicipalRetInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonImpuestoMunicipalRetDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonImpuestoMunicipalRetSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_RenglonImpuestoMunicipalRet_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDeTransaccionMunicipal") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsRenglonImpuestoMunicipalRetED

} //End of namespace Galac.Dbo.Dal.CajaChica

