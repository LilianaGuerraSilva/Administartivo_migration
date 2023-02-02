using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Dal.Banco {
	[LibMefDalComponentMetadata(typeof(clsTransferenciaEntreCuentasBancariasED))]
	public class clsTransferenciaEntreCuentasBancariasED : LibED, ILibMefDalComponent {
		#region Constructores
		public clsTransferenciaEntreCuentasBancariasED() : base() {
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
			get { return "TransferenciaEntreCuentasBancarias"; }
		}

		bool ILibMefDalComponent.InstallTable() {
			return InstalarTabla();
		}
		#endregion

		#region Queries
		private string SqlCreateTable() {
			StringBuilder SQL = new StringBuilder();
			SQL.AppendLine(InsSql.CreateTable("TransferenciaEntreCuentasBancarias", DbSchema) + " ( ");
			SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnTraEntCueBanConsecCia NOT NULL, ");
			SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnTraEntCueBanConsecutiv NOT NULL, ");
			SQL.AppendLine("Status" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_TraEntCueBanSt DEFAULT ('0'), ");
			SQL.AppendLine("Fecha" + InsSql.DateTypeForDb() + " CONSTRAINT nnTraEntCueBanFecha NOT NULL, ");
			SQL.AppendLine("NumeroDocumento" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT nnTraEntCueBanNumeroDocu NOT NULL, ");
			SQL.AppendLine("Descripcion" + InsSql.VarCharTypeForDb(255) + " CONSTRAINT nnTraEntCueBanDescripcio NOT NULL, ");
			SQL.AppendLine("FechaDeAnulacion" + InsSql.DateTypeForDb() + " CONSTRAINT d_TraEntCueBanFeDeAn DEFAULT (''), ");
            SQL.AppendLine("CodigoCuentaBancariaOrigen" + InsSql.VarCharTypeForDb(8) + " CONSTRAINT nnTraEntCueBanCodigoCuen NOT NULL, ");
			SQL.AppendLine("CambioABolivaresEgreso" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_TraEntCueBanCaABEg DEFAULT (1), ");
			SQL.AppendLine("MontoTransferenciaEgreso" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnTraEntCueBanMtoEgreso NOT NULL, ");
			SQL.AppendLine("CodigoConceptoEgreso" + InsSql.VarCharTypeForDb(8) + " CONSTRAINT nnTraEntCueBanConcEgreso NOT NULL, ");
			SQL.AppendLine("GeneraComisionEgreso" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnTraEntCueBanGenComiEgr NOT NULL, ");
			SQL.AppendLine("MontoComisionEgreso" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_TraEntCueBanMtoCoEgr DEFAULT (0), ");
			SQL.AppendLine("CodigoConceptoComisionEgreso" + InsSql.VarCharTypeForDb(8) + " CONSTRAINT nnTraEntCueBanConcCoEgr NOT NULL, ");
            SQL.AppendLine("GeneraIGTFComisionEgreso" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnTraEntCueBanGeneraIGTF NOT NULL, ");
            SQL.AppendLine("CodigoCuentaBancariaDestino" + InsSql.VarCharTypeForDb(8) + " CONSTRAINT d_TraEntCueBanCoCuBaDe DEFAULT (''), ");
			SQL.AppendLine("CambioABolivaresIngreso" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_TraEntCueBanCaABIn DEFAULT (1), ");
			SQL.AppendLine("MontoTransferenciaIngreso" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnTraEntCueBanMtoIngreso NOT NULL, ");
			SQL.AppendLine("CodigoConceptoIngreso" + InsSql.VarCharTypeForDb(8) + " CONSTRAINT nnTraEntCueBanConcIngreso NOT NULL, ");
			SQL.AppendLine("GeneraComisionIngreso" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnTraEntCueBanGenComiIng NOT NULL, ");
			SQL.AppendLine("MontoComisionIngreso" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_TraEntCueBanMtoCoIng DEFAULT (0), ");
			SQL.AppendLine("CodigoConceptoComisionIngreso" + InsSql.VarCharTypeForDb(8) + " CONSTRAINT nnTraEntCueBanConcCoIng NOT NULL, ");
            SQL.AppendLine("GeneraIGTFComisionIngreso" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnTraEntCueBanGeneraIGTF NOT NULL, ");
			SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
			SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
			SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
			SQL.AppendLine("CONSTRAINT p_TransferenciaEntreCuentasBancarias PRIMARY KEY CLUSTERED");
			SQL.AppendLine("(ConsecutivoCompania ASC, Consecutivo ASC)");
			SQL.AppendLine(")");
			return SQL.ToString();
		}

		private string SqlViewB1() {
			StringBuilder SQL = new StringBuilder();
			SQL.AppendLine("SELECT TransferenciaEntreCuentasBancarias.ConsecutivoCompania, TransferenciaEntreCuentasBancarias.Consecutivo, TransferenciaEntreCuentasBancarias.Status, " + DbSchema + ".Gv_EnumStatusTransferenciaBancaria.StrValue AS StatusStr, TransferenciaEntreCuentasBancarias.Fecha");
			SQL.AppendLine(", TransferenciaEntreCuentasBancarias.NumeroDocumento, TransferenciaEntreCuentasBancarias.Descripcion, TransferenciaEntreCuentasBancarias.FechaDeAnulacion, TransferenciaEntreCuentasBancarias.CodigoCuentaBancariaOrigen");
			SQL.AppendLine(", TransferenciaEntreCuentasBancarias.CambioABolivaresEgreso, TransferenciaEntreCuentasBancarias.MontoTransferenciaEgreso, TransferenciaEntreCuentasBancarias.CodigoConceptoEgreso, TransferenciaEntreCuentasBancarias.GeneraComisionEgreso");
			SQL.AppendLine(", TransferenciaEntreCuentasBancarias.MontoComisionEgreso, TransferenciaEntreCuentasBancarias.CodigoConceptoComisionEgreso, TransferenciaEntreCuentasBancarias.GeneraIGTFComisionEgreso");
			SQL.AppendLine(", TransferenciaEntreCuentasBancarias.CodigoCuentaBancariaDestino, TransferenciaEntreCuentasBancarias.CambioABolivaresIngreso, TransferenciaEntreCuentasBancarias.MontoTransferenciaIngreso, TransferenciaEntreCuentasBancarias.CodigoConceptoIngreso");
			SQL.AppendLine(", TransferenciaEntreCuentasBancarias.GeneraComisionIngreso, TransferenciaEntreCuentasBancarias.MontoComisionIngreso, TransferenciaEntreCuentasBancarias.CodigoConceptoComisionIngreso, TransferenciaEntreCuentasBancarias.GeneraIGTFComisionIngreso");
			SQL.AppendLine(", TransferenciaEntreCuentasBancarias.NombreOperador, TransferenciaEntreCuentasBancarias.FechaUltimaModificacion");
			SQL.AppendLine(", TransferenciaEntreCuentasBancarias.fldTimeStamp, CAST(TransferenciaEntreCuentasBancarias.fldTimeStamp AS bigint) AS fldTimeStampBigint");
			SQL.AppendLine("FROM " + DbSchema + ".TransferenciaEntreCuentasBancarias");
			SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumStatusTransferenciaBancaria");
			SQL.AppendLine("ON " + DbSchema + ".TransferenciaEntreCuentasBancarias.Status COLLATE MODERN_SPANISH_CS_AS");
			SQL.AppendLine(" = " + DbSchema + ".Gv_EnumStatusTransferenciaBancaria.DbValue");
			return SQL.ToString();
		}

		private string SqlSpInsParameters() {
			StringBuilder SQL = new StringBuilder();
			SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
			SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
			SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
			SQL.AppendLine("@Status" + InsSql.CharTypeForDb(1) + " = '0',");
			SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + " = '01/01/1900',");
			SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(20) + " = '',");
			SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(255) + " = '',");
			SQL.AppendLine("@FechaDeAnulacion" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@CodigoCuentaBancariaOrigen" + InsSql.VarCharTypeForDb(8) + ",");
			SQL.AppendLine("@CambioABolivaresEgreso" + InsSql.DecimalTypeForDb(25, 4) + " = 1,");
			SQL.AppendLine("@MontoTransferenciaEgreso" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
			SQL.AppendLine("@CodigoConceptoEgreso" + InsSql.VarCharTypeForDb(8) + ",");
			SQL.AppendLine("@GeneraComisionEgreso" + InsSql.CharTypeForDb(1) + " = 'N',");
			SQL.AppendLine("@MontoComisionEgreso" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
			SQL.AppendLine("@CodigoConceptoComisionEgreso" + InsSql.VarCharTypeForDb(8) + ",");
            SQL.AppendLine("@GeneraIGTFComisionEgreso" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@CodigoCuentaBancariaDestino" + InsSql.VarCharTypeForDb(8) + ",");
			SQL.AppendLine("@CambioABolivaresIngreso" + InsSql.DecimalTypeForDb(25, 4) + " = 1,");
			SQL.AppendLine("@MontoTransferenciaIngreso" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
			SQL.AppendLine("@CodigoConceptoIngreso" + InsSql.VarCharTypeForDb(8) + ",");
			SQL.AppendLine("@GeneraComisionIngreso" + InsSql.CharTypeForDb(1) + " = 'N',");
			SQL.AppendLine("@MontoComisionIngreso" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
			SQL.AppendLine("@CodigoConceptoComisionIngreso" + InsSql.VarCharTypeForDb(8) + ",");
            SQL.AppendLine("@GeneraIGTFComisionIngreso" + InsSql.CharTypeForDb(1) + " = 'N',");
			SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + " = '',");
			SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + " = '01/01/1900'");
			return SQL.ToString();
		}

		private string SqlSpIns() {
			StringBuilder SQL = new StringBuilder();
			SQL.AppendLine("BEGIN");
			SQL.AppendLine("   SET NOCOUNT ON;");
			SQL.AppendLine("   SET DATEFORMAT @DateFormat");
			SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
			SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
			SQL.AppendLine("      BEGIN");
			SQL.AppendLine("        BEGIN TRAN");
			SQL.AppendLine("            INSERT INTO " + DbSchema + ".TransferenciaEntreCuentasBancarias(");
			SQL.AppendLine("            ConsecutivoCompania,");
			SQL.AppendLine("            Consecutivo,");
			SQL.AppendLine("            Status,");
			SQL.AppendLine("            Fecha,");
			SQL.AppendLine("            NumeroDocumento,");
			SQL.AppendLine("            Descripcion,");
			SQL.AppendLine("            FechaDeAnulacion,");
			SQL.AppendLine("            CodigoCuentaBancariaOrigen,");
			SQL.AppendLine("            CambioABolivaresEgreso,");
			SQL.AppendLine("            MontoTransferenciaEgreso,");
			SQL.AppendLine("            CodigoConceptoEgreso,");
			SQL.AppendLine("            GeneraComisionEgreso,");
			SQL.AppendLine("            MontoComisionEgreso,");
			SQL.AppendLine("            CodigoConceptoComisionEgreso,");
			SQL.AppendLine("            GeneraIGTFComisionEgreso,");
			SQL.AppendLine("            CodigoCuentaBancariaDestino,");
			SQL.AppendLine("            CambioABolivaresIngreso,");
			SQL.AppendLine("            MontoTransferenciaIngreso,");
			SQL.AppendLine("            CodigoConceptoIngreso,");
			SQL.AppendLine("            GeneraComisionIngreso,");
			SQL.AppendLine("            MontoComisionIngreso,");
			SQL.AppendLine("            CodigoConceptoComisionIngreso,");
			SQL.AppendLine("            GeneraIGTFComisionIngreso,");
			SQL.AppendLine("            NombreOperador,");
			SQL.AppendLine("            FechaUltimaModificacion)");
			SQL.AppendLine("            VALUES(");
			SQL.AppendLine("            @ConsecutivoCompania,");
			SQL.AppendLine("            @Consecutivo,");
			SQL.AppendLine("            @Status,");
			SQL.AppendLine("            @Fecha,");
			SQL.AppendLine("            @NumeroDocumento,");
			SQL.AppendLine("            @Descripcion,");
			SQL.AppendLine("            @FechaDeAnulacion,");
			SQL.AppendLine("            @CodigoCuentaBancariaOrigen,");
			SQL.AppendLine("            @CambioABolivaresEgreso,");
			SQL.AppendLine("            @MontoTransferenciaEgreso,");
			SQL.AppendLine("            @CodigoConceptoEgreso,");
			SQL.AppendLine("            @GeneraComisionEgreso,");
			SQL.AppendLine("            @MontoComisionEgreso,");
			SQL.AppendLine("            @CodigoConceptoComisionEgreso,");
			SQL.AppendLine("            @GeneraIGTFComisionEgreso,");
			SQL.AppendLine("            @CodigoCuentaBancariaDestino,");
			SQL.AppendLine("            @CambioABolivaresIngreso,");
			SQL.AppendLine("            @MontoTransferenciaIngreso,");
			SQL.AppendLine("            @CodigoConceptoIngreso,");
			SQL.AppendLine("            @GeneraComisionIngreso,");
			SQL.AppendLine("            @MontoComisionIngreso,");
			SQL.AppendLine("            @CodigoConceptoComisionIngreso,");
			SQL.AppendLine("            @GeneraIGTFComisionIngreso,");
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
			SQL.AppendLine("@Status" + InsSql.CharTypeForDb(1) + ",");
			SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + ",");
			SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(20) + ",");
			SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(255) + ",");
			SQL.AppendLine("@FechaDeAnulacion" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@CodigoCuentaBancariaOrigen" + InsSql.VarCharTypeForDb(8) + ",");
			SQL.AppendLine("@CambioABolivaresEgreso" + InsSql.DecimalTypeForDb(25, 4) + ",");
			SQL.AppendLine("@MontoTransferenciaEgreso" + InsSql.DecimalTypeForDb(25, 4) + ",");
			SQL.AppendLine("@CodigoConceptoEgreso" + InsSql.VarCharTypeForDb(8) + ",");
			SQL.AppendLine("@GeneraComisionEgreso" + InsSql.CharTypeForDb(1) + ",");
			SQL.AppendLine("@MontoComisionEgreso" + InsSql.DecimalTypeForDb(25, 4) + ",");
			SQL.AppendLine("@CodigoConceptoComisionEgreso" + InsSql.VarCharTypeForDb(8) + ",");
            SQL.AppendLine("@GeneraIGTFComisionEgreso" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@CodigoCuentaBancariaDestino" + InsSql.VarCharTypeForDb(8) + ",");
			SQL.AppendLine("@CambioABolivaresIngreso" + InsSql.DecimalTypeForDb(25, 4) + ",");
			SQL.AppendLine("@MontoTransferenciaIngreso" + InsSql.DecimalTypeForDb(25, 4) + ",");
			SQL.AppendLine("@CodigoConceptoIngreso" + InsSql.VarCharTypeForDb(8) + ",");
			SQL.AppendLine("@GeneraComisionIngreso" + InsSql.CharTypeForDb(1) + ",");
			SQL.AppendLine("@MontoComisionIngreso" + InsSql.DecimalTypeForDb(25, 4) + ",");
			SQL.AppendLine("@CodigoConceptoComisionIngreso" + InsSql.VarCharTypeForDb(8) + ",");
            SQL.AppendLine("@GeneraIGTFComisionIngreso" + InsSql.CharTypeForDb(1) + ",");
			SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + ",");
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
			SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".TransferenciaEntreCuentasBancarias WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
			SQL.AppendLine("   BEGIN");
			SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".TransferenciaEntreCuentasBancarias WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
			SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
			SQL.AppendLine("      BEGIN");
			SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_TransferenciaEntreCuentasBancariasCanBeUpdated @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
			//SQL.AppendLine("--IF @CanBeChanged = 1 --True");
			//SQL.AppendLine("--BEGIN");
			SQL.AppendLine("         BEGIN TRAN");
			SQL.AppendLine("         UPDATE " + DbSchema + ".TransferenciaEntreCuentasBancarias");
			SQL.AppendLine("            SET Status = @Status,");
			SQL.AppendLine("               Fecha = @Fecha,");
			SQL.AppendLine("               NumeroDocumento = @NumeroDocumento,");
			SQL.AppendLine("               Descripcion = @Descripcion,");
			SQL.AppendLine("               FechaDeAnulacion = @FechaDeAnulacion,");
			SQL.AppendLine("               CodigoCuentaBancariaOrigen = @CodigoCuentaBancariaOrigen,");
			SQL.AppendLine("               CambioABolivaresEgreso = @CambioABolivaresEgreso,");
			SQL.AppendLine("               MontoTransferenciaEgreso = @MontoTransferenciaEgreso,");
			SQL.AppendLine("               CodigoConceptoEgreso = @CodigoConceptoEgreso,");
			SQL.AppendLine("               GeneraComisionEgreso = @GeneraComisionEgreso,");
			SQL.AppendLine("               MontoComisionEgreso = @MontoComisionEgreso,");
			SQL.AppendLine("               CodigoConceptoComisionEgreso = @CodigoConceptoComisionEgreso,");
            SQL.AppendLine("               GeneraIGTFComisionEgreso = @GeneraIGTFComisionEgreso,");
			SQL.AppendLine("               CodigoCuentaBancariaDestino = @CodigoCuentaBancariaDestino,");
			SQL.AppendLine("               CambioABolivaresIngreso = @CambioABolivaresIngreso,");
			SQL.AppendLine("               MontoTransferenciaIngreso = @MontoTransferenciaIngreso,");
			SQL.AppendLine("               CodigoConceptoIngreso = @CodigoConceptoIngreso,");
			SQL.AppendLine("               GeneraComisionIngreso = @GeneraComisionIngreso,");
			SQL.AppendLine("               MontoComisionIngreso = @MontoComisionIngreso,");
			SQL.AppendLine("               CodigoConceptoComisionIngreso = @CodigoConceptoComisionIngreso,");
            SQL.AppendLine("               GeneraIGTFComisionIngreso = @GeneraIGTFComisionIngreso,");
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
			SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".TransferenciaEntreCuentasBancarias WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
			SQL.AppendLine("   BEGIN");
			SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".TransferenciaEntreCuentasBancarias WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
			SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
			SQL.AppendLine("      BEGIN");
			SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_TransferenciaEntreCuentasBancariasCanBeDeleted @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
			//SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
			//SQL.AppendLine("--BEGIN");
			SQL.AppendLine("         BEGIN TRAN");
			SQL.AppendLine("         DELETE FROM " + DbSchema + ".TransferenciaEntreCuentasBancarias");
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
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.ConsecutivoCompania,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.Consecutivo,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.Status,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.Fecha,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.NumeroDocumento,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.Descripcion,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.FechaDeAnulacion,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.CodigoCuentaBancariaOrigen,");
			SQL.AppendLine("         CuentaBOrigen.NombreCuenta AS NombreCuentaBancariaOrigen,");
			SQL.AppendLine("         CuentaBOrigen.SaldoDisponible AS SaldoCuentaBancariaOrigen,");
			SQL.AppendLine("         CuentaBOrigen.CodigoMoneda AS CodigoMonedaCuentaBancariaOrigen,");
			SQL.AppendLine("         CuentaBOrigen.NombreDeLaMoneda AS NombreMonedaCuentaBancariaOrigen,");
			SQL.AppendLine("         CuentaBOrigen.ManejaDebitoBancario AS ManejaDebitoCuentaBancariaOrigen,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.CambioABolivaresEgreso,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.MontoTransferenciaEgreso,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.CodigoConceptoEgreso,");
			SQL.AppendLine("         ConceptoBEgreso.Descripcion AS DescripcionConceptoEgreso,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.GeneraComisionEgreso,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.MontoComisionEgreso,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.CodigoConceptoComisionEgreso,");
			SQL.AppendLine("         ISNULL(ConceptoBCoEgreso.Descripcion,'') AS DescripcionConceptoComisionEgreso,");
            SQL.AppendLine("         TransferenciaEntreCuentasBancarias.GeneraIGTFComisionEgreso,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.CodigoCuentaBancariaDestino,");
			SQL.AppendLine("         CuentaBDestino.NombreCuenta AS NombreCuentaBancariaDestino,");
			SQL.AppendLine("         CuentaBDestino.SaldoDisponible AS SaldoCuentaBancariaDestino,");
			SQL.AppendLine("         CuentaBDestino.CodigoMoneda AS CodigoMonedaCuentaBancariaDestino,");
			SQL.AppendLine("         CuentaBDestino.NombreDeLaMoneda AS NombreMonedaCuentaBancariaDestino,");
			SQL.AppendLine("         CuentaBDestino.ManejaCreditoBancario AS ManejaCreditoCuentaBancariaDestino,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.CambioABolivaresIngreso,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.MontoTransferenciaIngreso,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.CodigoConceptoIngreso,");
			SQL.AppendLine("         ConceptoBIngreso.Descripcion AS DescripcionConceptoIngreso,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.GeneraComisionIngreso,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.MontoComisionIngreso,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.CodigoConceptoComisionIngreso,");
			SQL.AppendLine("         ISNULL(ConceptoBCoIngreso.Descripcion,'') AS DescripcionConceptoComisionIngreso,");
            SQL.AppendLine("         TransferenciaEntreCuentasBancarias.GeneraIGTFComisionIngreso,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.NombreOperador,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.FechaUltimaModificacion,");
			SQL.AppendLine("         CAST(TransferenciaEntreCuentasBancarias.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
			SQL.AppendLine("         TransferenciaEntreCuentasBancarias.fldTimeStamp");
			SQL.AppendLine("      FROM " + DbSchema + ".TransferenciaEntreCuentasBancarias");
			SQL.AppendLine("             INNER JOIN Saw.Gv_CuentaBancaria_B1 CuentaBOrigen ON " + DbSchema + ".TransferenciaEntreCuentasBancarias.CodigoCuentaBancariaOrigen = CuentaBOrigen.Codigo");
			SQL.AppendLine("             INNER JOIN Adm.Gv_ConceptoBancario_B1 ConceptoBEgreso ON " + DbSchema + ".TransferenciaEntreCuentasBancarias.CodigoConceptoEgreso = ConceptoBEgreso.Codigo");
			SQL.AppendLine("             LEFT JOIN Adm.Gv_ConceptoBancario_B1 ConceptoBCoEgreso ON " + DbSchema + ".TransferenciaEntreCuentasBancarias.CodigoConceptoComisionEgreso = ConceptoBCoEgreso.Codigo");
			SQL.AppendLine("             INNER JOIN Saw.Gv_CuentaBancaria_B1 CuentaBDestino ON " + DbSchema + ".TransferenciaEntreCuentasBancarias.CodigoCuentaBancariaDestino = CuentaBDestino.Codigo");
			SQL.AppendLine("             INNER JOIN Adm.Gv_ConceptoBancario_B1 ConceptoBIngreso ON " + DbSchema + ".TransferenciaEntreCuentasBancarias.CodigoConceptoIngreso = ConceptoBIngreso.Codigo");
			SQL.AppendLine("             LEFT JOIN Adm.Gv_ConceptoBancario_B1 ConceptoBCoIngreso ON " + DbSchema + ".TransferenciaEntreCuentasBancarias.CodigoConceptoComisionIngreso = ConceptoBCoIngreso.Codigo");
			SQL.AppendLine("      WHERE TransferenciaEntreCuentasBancarias.ConsecutivoCompania = @ConsecutivoCompania");
			SQL.AppendLine("         AND TransferenciaEntreCuentasBancarias.Consecutivo = @Consecutivo");
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
			SQL.AppendLine("      " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.Consecutivo,");
			SQL.AppendLine("      " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.StatusStr,");
			SQL.AppendLine("      " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.Fecha,");
			SQL.AppendLine("      " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.NumeroDocumento,");
			SQL.AppendLine("      " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.CodigoCuentaBancariaOrigen,");
			SQL.AppendLine("      " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.CodigoConceptoEgreso,");
			SQL.AppendLine("      " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.CodigoCuentaBancariaDestino,");
			SQL.AppendLine("      " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.CodigoConceptoIngreso,");
			SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
			SQL.AppendLine("      " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.ConsecutivoCompania,");
			SQL.AppendLine("      " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.Status");
			SQL.AppendLine("      FROM " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1");
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
			SQL.AppendLine("      " + DbSchema + ".TransferenciaEntreCuentasBancarias.ConsecutivoCompania,");
			SQL.AppendLine("      " + DbSchema + ".TransferenciaEntreCuentasBancarias.Consecutivo,");
			SQL.AppendLine("      " + DbSchema + ".TransferenciaEntreCuentasBancarias.Status,");
			SQL.AppendLine("      " + DbSchema + ".TransferenciaEntreCuentasBancarias.Fecha,");
			SQL.AppendLine("      " + DbSchema + ".TransferenciaEntreCuentasBancarias.NumeroDocumento,");
			SQL.AppendLine("      " + DbSchema + ".TransferenciaEntreCuentasBancarias.CodigoCuentaBancariaOrigen,");
			SQL.AppendLine("      " + DbSchema + ".TransferenciaEntreCuentasBancarias.CodigoConceptoEgreso,");
			SQL.AppendLine("      " + DbSchema + ".TransferenciaEntreCuentasBancarias.CodigoConceptoComisionEgreso,");
			SQL.AppendLine("      " + DbSchema + ".TransferenciaEntreCuentasBancarias.CodigoCuentaBancariaDestino,");
			SQL.AppendLine("      " + DbSchema + ".TransferenciaEntreCuentasBancarias.CodigoConceptoIngreso,");
			SQL.AppendLine("      " + DbSchema + ".TransferenciaEntreCuentasBancarias.CodigoConceptoComisionIngreso");
			//SQL.AppendLine("      ," + DbSchema + ".TransferenciaEntreCuentasBancarias.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
			SQL.AppendLine("      FROM " + DbSchema + ".TransferenciaEntreCuentasBancarias");
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
			bool vResult = insDbo.Create(DbSchema + ".TransferenciaEntreCuentasBancarias", SqlCreateTable(), false, eDboType.Tabla);
			return vResult;
		}

		bool CrearVistas() {
			bool vResult = false;
			LibViews insVistas = new LibViews();
			vResult = insVistas.Create(DbSchema + ".Gv_EnumStatusTransferenciaBancaria", LibTpvCreator.SqlViewStandardEnum(typeof(eStatusTransferenciaBancaria), InsSql), true, true);
			vResult = insVistas.Create(DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1", SqlViewB1(), true);
			insVistas.Dispose();
			return vResult;
		}

		bool CrearProcedimientos() {
			bool vResult = false;
			LibStoredProc insSps = new LibStoredProc();
			vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_TransferenciaEntreCuentasBancariasINS", SqlSpInsParameters(), SqlSpIns(), true);
			vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_TransferenciaEntreCuentasBancariasUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
			vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_TransferenciaEntreCuentasBancariasDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
			vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_TransferenciaEntreCuentasBancariasGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
			vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_TransferenciaEntreCuentasBancariasSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
			vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_TransferenciaEntreCuentasBancariasGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
			vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_TransferenciaEntreCuentasBancariasContaSCH", SqlSpContabSchParameters(), SqlSpContabSch(), true) && vResult;
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
			if (insDbo.Exists(DbSchema + ".TransferenciaEntreCuentasBancarias", eDboType.Tabla)) {
				CrearVistas();
				CrearProcedimientos();
				vResult = true;
			}
			return vResult;
		}

		public bool BorrarVistasYSps() {
			bool vResult;
			LibStoredProc insSp = new LibStoredProc();
			LibViews insVista = new LibViews();
			vResult = insSp.Drop(DbSchema + ".Gp_TransferenciaEntreCuentasBancariasContaSCH");
			vResult = insSp.Drop(DbSchema + ".Gp_TransferenciaEntreCuentasBancariasGetFk") && vResult;
			vResult = insSp.Drop(DbSchema + ".Gp_TransferenciaEntreCuentasBancariasSCH") && vResult;
			vResult = insSp.Drop(DbSchema + ".Gp_TransferenciaEntreCuentasBancariasGET") && vResult;
			vResult = insSp.Drop(DbSchema + ".Gp_TransferenciaEntreCuentasBancariasDEL") && vResult;
			vResult = insSp.Drop(DbSchema + ".Gp_TransferenciaEntreCuentasBancariasUPD") && vResult;
			vResult = insSp.Drop(DbSchema + ".Gp_TransferenciaEntreCuentasBancariasINS") && vResult;
			vResult = insVista.Drop(DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1") && vResult;
			vResult = insVista.Drop(DbSchema + ".Gv_EnumStatusTransferenciaBancaria") && vResult;
			insSp.Dispose();
			insVista.Dispose();
			return vResult;
		}

		private string SqlSpContabSchParameters() {
			StringBuilder SQL = new StringBuilder();
			SQL.AppendLine("@SQLWhere" + InsSql.VarCharTypeForDb(2000) + " = null,");
			SQL.AppendLine("@SQLOrderBy" + InsSql.VarCharTypeForDb(500) + " = null,");
			SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + " = null,");
			SQL.AppendLine("@UseTopClausule" + InsSql.VarCharTypeForDb(1) + " = 'S'");
			return SQL.ToString();
		}

		private string SqlSpContabSch() {
			StringBuilder vSQL = new StringBuilder();
			StringBuilder vSqlComprobantePeriodo = new StringBuilder();
			string vSqlStdSeparator = InsSql.ToSqlValue(LibGalac.Aos.Base.LibText.StandardSeparator());

			vSqlComprobantePeriodo.AppendLine("      (SELECT Comprobante.NoDocumentoOrigen, COMPROBANTE.GeneradoPor, COMPROBANTE.ConsecutivoDocOrigen, ");
			vSqlComprobantePeriodo.AppendLine("      Periodo.ConsecutivoCompania, Periodo.FechaAperturaDelPeriodo, Periodo.FechaCierreDelPeriodo  ");
			vSqlComprobantePeriodo.AppendLine("      FROM COMPROBANTE INNER JOIN PERIODO ");
			vSqlComprobantePeriodo.AppendLine("          ON  PERIODO.ConsecutivoPeriodo  = COMPROBANTE.ConsecutivoPeriodo");
			vSqlComprobantePeriodo.AppendLine("          AND PERIODO.ConsecutivoPeriodo  = COMPROBANTE.ConsecutivoPeriodo) ");
	
			vSQL.AppendLine("BEGIN");
			vSQL.AppendLine("   SET NOCOUNT ON;");
			vSQL.AppendLine("   DECLARE @strSQL AS " + InsSql.VarCharTypeForDb(7000));
			vSQL.AppendLine("   DECLARE @TopClausule AS " + InsSql.VarCharTypeForDb(10));
			vSQL.AppendLine("   DECLARE @SqlStatusNumero AS " + InsSql.VarCharTypeForDb(500));

			vSQL.AppendLine("   SET @SqlStatusNumero = 'CAST(Adm.Gv_TransferenciaEntreCuentasBancarias_B1.Status AS varchar) + ' + '''' + " + vSqlStdSeparator + " +  '''' +  ' + CAST(Adm.Gv_TransferenciaEntreCuentasBancarias_B1.NumeroDocumento AS varchar) '");

			vSQL.AppendLine("   IF(@UseTopClausule = 'S') ");
			vSQL.AppendLine("    SET @TopClausule = 'TOP 500'");
			vSQL.AppendLine("   ELSE ");
			vSQL.AppendLine("    SET @TopClausule = ''");
			vSQL.AppendLine("   SET @strSQL = ");
			vSQL.AppendLine("    ' SET DateFormat ' + @DateFormat + ");

			vSQL.AppendLine("    ' SELECT ' + @TopClausule + '");
			vSQL.AppendLine("       " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.Consecutivo,");
			vSQL.AppendLine("       " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.StatusStr,");
			vSQL.AppendLine("       " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.Fecha,");
			vSQL.AppendLine("       " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.NumeroDocumento,");
			vSQL.AppendLine("       " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.CodigoCuentaBancariaOrigen,");
			vSQL.AppendLine("       " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.CodigoConceptoEgreso,");
			vSQL.AppendLine("       " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.CodigoCuentaBancariaDestino,");
			vSQL.AppendLine("       " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.CodigoConceptoIngreso,");
			vSQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
			vSQL.AppendLine("       " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.ConsecutivoCompania,");
			vSQL.AppendLine("       " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.Consecutivo,");
			vSQL.AppendLine("       " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.Status");

			vSQL.AppendLine("      FROM " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1 ");
			vSQL.AppendLine("      LEFT JOIN " + vSqlComprobantePeriodo + " AS ComprobantePeriodo");
			vSQL.AppendLine("      ON  " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.Consecutivo = ComprobantePeriodo.ConsecutivoDocOrigen AND ' + @SqlStatusNumero ");
			vSQL.AppendLine("      + ' = ComprobantePeriodo.NoDocumentoOrigen ");
			vSQL.AppendLine("      AND ComprobantePeriodo.GeneradoPor = ' + QUOTENAME('O','''') + '");
			vSQL.AppendLine("      AND ComprobantePeriodo.ConsecutivoCompania = " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.ConsecutivoCompania");
			vSQL.AppendLine("      AND ComprobantePeriodo.FechaAperturaDelPeriodo < " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.Fecha");
			vSQL.AppendLine("      AND ComprobantePeriodo.FechaCierreDelPeriodo   > " + DbSchema + ".Gv_TransferenciaEntreCuentasBancarias_B1.Fecha");

			vSQL.AppendLine("'   IF (NOT @SQLWhere IS NULL) AND (@SQLWhere <> '')");
			vSQL.AppendLine("      SET @strSQL = @strSQL + ' WHERE ' + @SQLWhere + ' AND ComprobantePeriodo.ConsecutivoDocOrigen IS NULL '  ");
			vSQL.AppendLine("   IF (NOT @SQLOrderBy IS NULL) AND (@SQLOrderBy <> '')");
			vSQL.AppendLine("      SET @strSQL = @strSQL + ' ORDER BY ' + @SQLOrderBy");
			vSQL.AppendLine("   EXEC(@strSQL)");
			vSQL.AppendLine("END");

			return vSQL.ToString();
		}
		#endregion //Metodos Generados

	} //End of class clsTransferenciaEntreCuentasBancariasED

} //End of namespace Galac.Adm.Dal.Banco

