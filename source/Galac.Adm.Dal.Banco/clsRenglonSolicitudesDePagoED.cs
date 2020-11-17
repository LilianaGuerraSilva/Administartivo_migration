using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using Galac.Adm.Ccl.Banco;
using LibGalac.Aos.Base.Dal;

namespace Galac.Adm.Dal.Banco {
    public class clsRenglonSolicitudesDePagoED: LibED {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsRenglonSolicitudesDePagoED(): base(){
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("RenglonSolicitudesDePago", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnRenSolDePagConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoSolicitud" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnRenSolDePagConsecutiv NOT NULL, ");
            SQL.AppendLine("consecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnRenSolDePagconsecutiv NOT NULL, ");
            SQL.AppendLine("CuentaBancaria" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT nnRenSolDePagCuentaBanc NOT NULL, ");
            SQL.AppendLine("ConsecutivoBeneficiario" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnRenSolDePagConsecutiv NOT NULL, ");
            SQL.AppendLine("FormaDePago" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_RenSolDePagFoDePa DEFAULT ('0'), ");
            SQL.AppendLine("Status" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_RenSolDePagSt DEFAULT ('0'), ");
            SQL.AppendLine("Monto" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnRenSolDePagMonto NOT NULL, ");
            SQL.AppendLine("NumeroDocumento" + InsSql.VarCharTypeForDb(15) + " CONSTRAINT nnRenSolDePagNumeroDocu NOT NULL, ");
            SQL.AppendLine("Contabilizado" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnRenSolDePagContabiliz NOT NULL, ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_RenglonSolicitudesDePago PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, ConsecutivoSolicitud ASC, consecutivoRenglon ASC)");
            SQL.AppendLine(",CONSTRAINT fk_RenglonSolicitudesDePagoSolicitudesDePago FOREIGN KEY (ConsecutivoCompania, ConsecutivoSolicitud)");
            SQL.AppendLine("REFERENCES Saw.SolicitudesDePago(ConsecutivoCompania, ConsecutivoSolicitud)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT RenglonSolicitudesDePago.ConsecutivoCompania, RenglonSolicitudesDePago.ConsecutivoSolicitud, RenglonSolicitudesDePago.consecutivoRenglon, RenglonSolicitudesDePago.CuentaBancaria");
            SQL.AppendLine(", RenglonSolicitudesDePago.ConsecutivoBeneficiario, RenglonSolicitudesDePago.FormaDePago, " + DbSchema + ".Gv_EnumTipoDeFormaDePagoSolicitud.StrValue AS FormaDePagoStr, RenglonSolicitudesDePago.Status, " + DbSchema + ".Gv_EnumStatusSolicitudRenglon.StrValue AS StatusStr, RenglonSolicitudesDePago.Monto");
            SQL.AppendLine(", RenglonSolicitudesDePago.NumeroDocumento, RenglonSolicitudesDePago.Contabilizado");
            SQL.AppendLine(", RenglonSolicitudesDePago.fldTimeStamp, CAST(RenglonSolicitudesDePago.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine(" FROM " + DbSchema + ".RenglonSolicitudesDePago");
            SQL.AppendLine(" INNER JOIN " + DbSchema + ".Gv_EnumTipoDeFormaDePagoSolicitud");
            SQL.AppendLine(" ON " + DbSchema + ".RenglonSolicitudesDePago.FormaDePago COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDeFormaDePagoSolicitud.DbValue");
            SQL.AppendLine(" INNER JOIN " + DbSchema + ".Gv_EnumStatusSolicitudRenglon");
            SQL.AppendLine(" ON " + DbSchema + ".RenglonSolicitudesDePago.Status COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumStatusSolicitudRenglon.DbValue");
            SQL.AppendLine(" INNER JOIN Saw.CuentaBancaria ON  " + DbSchema + ".RenglonSolicitudesDePago.CuentaBancaria = Saw.CuentaBancaria.Codigo");
            SQL.AppendLine("      AND " + DbSchema + ".RenglonSolicitudesDePago.ConsecutivoCompania = Saw.CuentaBancaria.ConsecutivoCompania");
            SQL.AppendLine(" INNER JOIN Saw.Beneficiario ON  " + DbSchema + ".RenglonSolicitudesDePago.ConsecutivoBeneficiario = Saw.Beneficiario.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".RenglonSolicitudesDePago.ConsecutivoCompania = Saw.Beneficiario.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoSolicitud" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@consecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CuentaBancaria" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@ConsecutivoBeneficiario" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@FormaDePago" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Status" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Monto" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(15) + " = '',");
            SQL.AppendLine("@Contabilizado" + InsSql.CharTypeForDb(1) + " = 'N'");
            return SQL.ToString();
        }

        private string SqlSpIns() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("   BEGIN TRAN");
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("   INSERT INTO " + DbSchema + ".RenglonSolicitudesDePago(");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         ConsecutivoSolicitud,");
            SQL.AppendLine("         consecutivoRenglon,");
            SQL.AppendLine("         CuentaBancaria,");
            SQL.AppendLine("         ConsecutivoBeneficiario,");
            SQL.AppendLine("         FormaDePago,");
            SQL.AppendLine("         Status,");
            SQL.AppendLine("         Monto,");
            SQL.AppendLine("         NumeroDocumento,");
            SQL.AppendLine("         Contabilizado)");
            SQL.AppendLine("      VALUES(");
            SQL.AppendLine("         @ConsecutivoCompania,");
            SQL.AppendLine("         @ConsecutivoSolicitud,");
            SQL.AppendLine("         @consecutivoRenglon,");
            SQL.AppendLine("         @CuentaBancaria,");
            SQL.AppendLine("         @ConsecutivoBeneficiario,");
            SQL.AppendLine("         @FormaDePago,");
            SQL.AppendLine("         @Status,");
            SQL.AppendLine("         @Monto,");
            SQL.AppendLine("         @NumeroDocumento,");
            SQL.AppendLine("         @Contabilizado)");
            SQL.AppendLine("   SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("   COMMIT TRAN");
            SQL.AppendLine("   RETURN @ReturnValue ");
            SQL.AppendLine("	END");
            SQL.AppendLine("	ELSE");
            SQL.AppendLine("		RETURN -1");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpUpdParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoSolicitud" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@consecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CuentaBancaria" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@ConsecutivoBeneficiario" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@FormaDePago" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Status" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Monto" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(15) + ",");
            SQL.AppendLine("@Contabilizado" + InsSql.CharTypeForDb(1) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".RenglonSolicitudesDePago WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoSolicitud = @ConsecutivoSolicitud AND consecutivoRenglon = @consecutivoRenglon)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".RenglonSolicitudesDePago WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoSolicitud = @ConsecutivoSolicitud AND consecutivoRenglon = @consecutivoRenglon");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_RenglonSolicitudesDePagoCanBeUpdated @ConsecutivoCompania,@ConsecutivoSolicitud,@consecutivoRenglon, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".RenglonSolicitudesDePago");
            SQL.AppendLine("            SET CuentaBancaria = @CuentaBancaria,");
            SQL.AppendLine("               ConsecutivoBeneficiario = @ConsecutivoBeneficiario,");
            SQL.AppendLine("               FormaDePago = @FormaDePago,");
            SQL.AppendLine("               Status = @Status,");
            SQL.AppendLine("               Monto = @Monto,");
            SQL.AppendLine("               NumeroDocumento = @NumeroDocumento,");
            SQL.AppendLine("               Contabilizado = @Contabilizado");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoSolicitud = @ConsecutivoSolicitud");
            SQL.AppendLine("               AND consecutivoRenglon = @consecutivoRenglon");
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
            SQL.AppendLine("@ConsecutivoSolicitud" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@consecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".RenglonSolicitudesDePago WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoSolicitud = @ConsecutivoSolicitud AND consecutivoRenglon = @consecutivoRenglon)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".RenglonSolicitudesDePago WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoSolicitud = @ConsecutivoSolicitud AND consecutivoRenglon = @consecutivoRenglon");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_RenglonSolicitudesDePagoCanBeDeleted @ConsecutivoCompania,@ConsecutivoSolicitud,@consecutivoRenglon, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".RenglonSolicitudesDePago");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoSolicitud = @ConsecutivoSolicitud");
            SQL.AppendLine("               AND consecutivoRenglon = @consecutivoRenglon");
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
            SQL.AppendLine("@ConsecutivoSolicitud" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@consecutivoRenglon" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         RenglonSolicitudesDePago.ConsecutivoCompania,");
            SQL.AppendLine("         RenglonSolicitudesDePago.ConsecutivoSolicitud,");
            SQL.AppendLine("         RenglonSolicitudesDePago.consecutivoRenglon,");
            SQL.AppendLine("         RenglonSolicitudesDePago.CuentaBancaria,");
            SQL.AppendLine("         RenglonSolicitudesDePago.ConsecutivoBeneficiario,");
            SQL.AppendLine("         RenglonSolicitudesDePago.FormaDePago,");
            SQL.AppendLine("         RenglonSolicitudesDePago.Status,");
            SQL.AppendLine("         RenglonSolicitudesDePago.Monto,");
            SQL.AppendLine("         RenglonSolicitudesDePago.NumeroDocumento,");
            SQL.AppendLine("         RenglonSolicitudesDePago.Contabilizado,");
            SQL.AppendLine("         CAST(RenglonSolicitudesDePago.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         RenglonSolicitudesDePago.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".RenglonSolicitudesDePago");
            SQL.AppendLine("             INNER JOIN Saw.Gv_CuentaBancaria_B1 ON " + DbSchema + ".RenglonSolicitudesDePago.CuentaBancaria = Saw.Gv_CuentaBancaria_B1.Codigo");
            SQL.AppendLine("             INNER JOIN Saw.Gv_Beneficiario_B1 ON " + DbSchema + ".RenglonSolicitudesDePago.ConsecutivoBeneficiario = Saw.Gv_Beneficiario_B1.Consecutivo");
            SQL.AppendLine("      WHERE RenglonSolicitudesDePago.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND RenglonSolicitudesDePago.ConsecutivoSolicitud = @ConsecutivoSolicitud");
            SQL.AppendLine("         AND RenglonSolicitudesDePago.consecutivoRenglon = @consecutivoRenglon");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoSolicitud" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpSelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SELECT ");
            SQL.AppendLine("        ConsecutivoCompania,");
            SQL.AppendLine("        ConsecutivoSolicitud,");
            SQL.AppendLine("        consecutivoRenglon,");
            SQL.AppendLine("        CuentaBancaria,");
            SQL.AppendLine("        ConsecutivoBeneficiario,");
            SQL.AppendLine("        FormaDePago,");
            SQL.AppendLine("        Status,");
            SQL.AppendLine("        Monto,");
            SQL.AppendLine("        NumeroDocumento,");
            SQL.AppendLine("        Contabilizado,");
            SQL.AppendLine("        fldTimeStamp");
            SQL.AppendLine("    FROM RenglonSolicitudesDePago");
            SQL.AppendLine(" 	WHERE ConsecutivoSolicitud = @ConsecutivoSolicitud");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoSolicitud" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpDelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	DELETE FROM RenglonSolicitudesDePago");
            SQL.AppendLine(" 	WHERE ConsecutivoSolicitud = @ConsecutivoSolicitud");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInsDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoSolicitud" + InsSql.NumericTypeForDb(10, 0)+ ",");
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
            SQL.AppendLine("	    EXEC Saw.Gp_RenglonSolicitudesDePagoDelDet @ConsecutivoCompania = @ConsecutivoCompania, @ConsecutivoSolicitud = @ConsecutivoSolicitud");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO Saw.RenglonSolicitudesDePago(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        ConsecutivoSolicitud,");
			SQL.AppendLine("	        consecutivoRenglon,");
			SQL.AppendLine("	        CuentaBancaria,");
			SQL.AppendLine("	        ConsecutivoBeneficiario,");
			SQL.AppendLine("	        FormaDePago,");
			SQL.AppendLine("	        Status,");
			SQL.AppendLine("	        Monto,");
			SQL.AppendLine("	        NumeroDocumento,");
			SQL.AppendLine("	        Contabilizado)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @ConsecutivoSolicitud,");
			SQL.AppendLine("	        consecutivoRenglon,");
			SQL.AppendLine("	        CuentaBancaria,");
			SQL.AppendLine("	        ConsecutivoBeneficiario,");
			SQL.AppendLine("	        FormaDePago,");
			SQL.AppendLine("	        Status,");
			SQL.AppendLine("	        Monto,");
			SQL.AppendLine("	        NumeroDocumento,");
			SQL.AppendLine("	        Contabilizado");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataRenglonSolicitudesDePago/GpDetailRenglonSolicitudesDePago',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        consecutivoRenglon " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        CuentaBancaria " + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("	        ConsecutivoBeneficiario " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        FormaDePago " + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("	        Status " + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("	        Monto " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        NumeroDocumento " + InsSql.VarCharTypeForDb(15) + ",");
            SQL.AppendLine("	        Contabilizado " + InsSql.CharTypeForDb(1) + ") AS XmlDocDetailOfSolicitudesDePago");
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
            bool vResult = insDbo.Create(DbSchema + ".RenglonSolicitudesDePago", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }
        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDeFormaDePagoSolicitud", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDeFormaDePagoSolicitud), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumStatusSolicitudRenglon", LibTpvCreator.SqlViewStandardEnum(typeof(eStatusSolicitudRenglon), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_RenglonSolicitudesDePago_B1", SqlViewB1(), true);
            vResult = insVistas.Create(DbSchema + ".Gv_SolicitudesDePagoFormaDePago_B1", SqlViewSolicitudesDePagoFormaDePagoB1(), true);
            insVistas.Dispose();
            return vResult;
        }
        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonSolicitudesDePagoINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonSolicitudesDePagoUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonSolicitudesDePagoDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonSolicitudesDePagoGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonSolicitudesDePagoSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonSolicitudesDePagoDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RenglonSolicitudesDePagoInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SolicitudesDePagoStatusUPD", SqlSpSolicitudesDePagoStatusUpdParameters(), SqlSpSolicitudesDePagoStatusUpd(), true ) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SolicitudesDePagoFormaDePagoSCH",SqlSpSearchFormaDePagoParameters(), SqlSpSearchFormaDePago(), true ) && vResult;
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
            if (insDbo.Exists(DbSchema + ".RenglonSolicitudesDePago", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonSolicitudesDePagoINS");
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonSolicitudesDePagoUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonSolicitudesDePagoDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonSolicitudesDePagoGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonSolicitudesDePagoInsDet");
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonSolicitudesDePagoDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RenglonSolicitudesDePagoSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_RenglonSolicitudesDePago_B1") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_SolicitudesDePagoStatusUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_SolicitudesDePagoFormaDePagoSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_SolicitudesDePagoFormaDePago_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados

        #region Metodos Nuevos

        private string SqlSpSearchFormaDePagoParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@SQLWhere" + InsSql.VarCharTypeForDb(2000) + " = null,");
            SQL.AppendLine("@SQLOrderBy" + InsSql.VarCharTypeForDb(500) + " = null,");
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + " = null");
            return SQL.ToString();
        }

        private string SqlSpSearchFormaDePago() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @strSQL AS " + InsSql.VarCharTypeForDb(7000));
            SQL.AppendLine("   SET @strSQL = 'SET DateFormat ' + @DateFormat + ");
            SQL.AppendLine("      ' SELECT TOP 500 ");
            SQL.AppendLine("      NumeroDocumentoOrigen,");
            SQL.AppendLine("      Observaciones,");
            SQL.AppendLine("      FechaSolicitud,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      ConsecutivoCompania,");
            SQL.AppendLine("      ConsecutivoSolicitud");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_SolicitudesDePagoFormaDePago_B1");
            SQL.AppendLine("'   IF (NOT @SQLWhere IS NULL) AND (@SQLWhere <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' WHERE ' + @SQLWhere");
            SQL.AppendLine("   IF (NOT @SQLOrderBy IS NULL) AND (@SQLOrderBy <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' ORDER BY ' + @SQLOrderBy");
            SQL.AppendLine("   EXEC(@strSQL)");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSolicitudesDePagoStatusUpdParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoSolicitud" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + " ");
            return SQL.ToString();
        }


        private string SqlSpSolicitudesDePagoStatusUpd() {
            StringBuilder SQL = new StringBuilder();
            QAdvSql insQAdvSql = new QAdvSql("");
            string vStatus = "";
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("     DECLARE @Up_Status" + InsSql.CharTypeForDb(1) + "");
            SQL.AppendLine("     DECLARE @Up_NombreOperador" + InsSql.VarCharTypeForDb(20) + "");
            SQL.AppendLine("     DECLARE @Procesados" + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("     DECLARE @PorProcesar" + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("     DECLARE @Registros" + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("     DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".SolicitudesDePago WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoSolicitud = @ConsecutivoSolicitud)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT   @PorProcesar = COUNT(Status)   FROM  " + DbSchema + ".Gv_RenglonSolicitudesDePago_B1   WHERE  " + insQAdvSql.SqlEnumValueWithAnd("", "Status", (int)eStatusSolicitudRenglon.PorProcesar) + " AND  ConsecutivoSolicitud = @ConsecutivoSolicitud  AND  ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("      SELECT   @Procesados = COUNT(Status)   FROM  " + DbSchema + ".Gv_RenglonSolicitudesDePago_B1   WHERE  " + insQAdvSql.SqlEnumValueWithAnd("", "Status", (int)eStatusSolicitudRenglon.Procesada) + " AND  ConsecutivoSolicitud = @ConsecutivoSolicitud  AND  ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("      SELECT   @Registros = COUNT(Status)   FROM  " + DbSchema + ".Gv_RenglonSolicitudesDePago_B1   WHERE  " + "  ConsecutivoSolicitud = @ConsecutivoSolicitud  AND  ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    IF @Registros =  @Procesados ");
            vStatus = insQAdvSql.ToSqlValue(LibGalac.Aos.Base.LibConvert.EnumToDbValue((int)eStatusSolicitud.Procesada));
            SQL.AppendLine("       SET @Up_Status = " + vStatus + ";");
            SQL.AppendLine("    ELSE");
            SQL.AppendLine("    IF @Registros =  @PorProcesar");
            vStatus = insQAdvSql.ToSqlValue(LibGalac.Aos.Base.LibConvert.EnumToDbValue((int)eStatusSolicitud.PorProcesar));
            SQL.AppendLine("       SET @Up_Status = " + vStatus + ";");
            SQL.AppendLine("    ELSE");
            vStatus = insQAdvSql.ToSqlValue(LibGalac.Aos.Base.LibConvert.EnumToDbValue((int)eStatusSolicitud.ParcialmenteProcesada));
            SQL.AppendLine("       SET @Up_Status = " + vStatus + ";");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".SolicitudesDePago");
            SQL.AppendLine("            SET Status = @Up_Status,");
            SQL.AppendLine("               NombreOperador = @NombreOperador,");
            SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
            SQL.AppendLine("            WHERE  ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoSolicitud = @ConsecutivoSolicitud");
            SQL.AppendLine("         SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("            COMMIT TRAN");
            SQL.AppendLine("   END");
            SQL.AppendLine("   ELSE");
            SQL.AppendLine("      RAISERROR('El registro no existe.', 14, 1)");
            SQL.AppendLine("   RETURN @ReturnValue");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Metodos Generados


        private string SqlViewSolicitudesDePagoFormaDePagoB1() {
            StringBuilder SQL = new StringBuilder();
            string vCondicion = "";
            QAdvSql insQAdvSql = new QAdvSql("");
            vCondicion = insQAdvSql.SqlEnumValueWithAnd("", DbSchema + ".RenglonSolicitudesDePago.Status", (int)eStatusSolicitudRenglon.Procesada);
            SQL.AppendLine("SELECT ");
            SQL.AppendLine(DbSchema + ".Gv_SolicitudesDePago_B1.NumeroDocumentoOrigen,");
            SQL.AppendLine(DbSchema + ".Gv_SolicitudesDePago_B1.Observaciones,");
            SQL.AppendLine(DbSchema + ".Gv_SolicitudesDePago_B1.FechaSolicitud,");
            SQL.AppendLine(DbSchema + ".Gv_SolicitudesDePago_B1.ConsecutivoCompania,");
            SQL.AppendLine(DbSchema + ".Gv_SolicitudesDePago_B1.ConsecutivoSolicitud,");
            SQL.AppendLine(DbSchema + ".RenglonSolicitudesDePago.FormaDePago,");
            SQL.AppendLine(DbSchema + ".Gv_SolicitudesDePago_B1.GeneradoPor,");
            SQL.AppendLine(insQAdvSql.IIF(vCondicion, insQAdvSql.ToSqlValue(false), insQAdvSql.ToSqlValue(true), true) + " AS PuedoProcesar");
            SQL.AppendLine(" FROM  " + DbSchema + ".Gv_SolicitudesDePago_B1 INNER JOIN ");
            SQL.AppendLine(DbSchema + ".RenglonSolicitudesDePago ON ");
            SQL.AppendLine(DbSchema + ".Gv_SolicitudesDePago_B1.ConsecutivoCompania = " + DbSchema + ".RenglonSolicitudesDePago.ConsecutivoCompania AND ");
            SQL.AppendLine(DbSchema + ".Gv_SolicitudesDePago_B1.ConsecutivoSolicitud = " + DbSchema + ".RenglonSolicitudesDePago.ConsecutivoSolicitud");
            SQL.AppendLine(" GROUP BY ");
            SQL.AppendLine(DbSchema + ".Gv_SolicitudesDePago_B1.NumeroDocumentoOrigen,");
            SQL.AppendLine(DbSchema + ".Gv_SolicitudesDePago_B1.Observaciones,");
            SQL.AppendLine(DbSchema + ".Gv_SolicitudesDePago_B1.FechaSolicitud,");
            SQL.AppendLine(DbSchema + ".Gv_SolicitudesDePago_B1.ConsecutivoCompania,");
            SQL.AppendLine(DbSchema + ".Gv_SolicitudesDePago_B1.ConsecutivoSolicitud,");
            SQL.AppendLine(DbSchema + ".RenglonSolicitudesDePago.FormaDePago,");
            SQL.AppendLine(DbSchema + ".RenglonSolicitudesDePago.Status,");
            SQL.AppendLine(DbSchema + ".Gv_SolicitudesDePago_B1.GeneradoPor");
            return SQL.ToString();
        }
    } //End of class clsRenglonSolicitudesDePagoED

} //End of namespace Galac.Dbo.Dal.RenglonSolicitudesDePago

