using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Dal.Banco {
	[LibMefDalComponentMetadata(typeof(clsCuentaBancariaED))]
	public class clsCuentaBancariaED : LibED, ILibMefDalComponent {
		#region Variables
		#endregion //Variables

		#region Propiedades
		#endregion //Propiedades

		#region Constructores
		public clsCuentaBancariaED() : base() {
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
			get { return "CuentaBancaria"; }
		}

		bool ILibMefDalComponent.InstallTable() {
			return InstalarTabla();
		}
		#endregion

		#region Queries
		private string SqlCreateTable() {
			StringBuilder SQL = new StringBuilder();
			SQL.AppendLine(InsSql.CreateTable("CuentaBancaria", DbSchema) + " ( ");
			SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnCueBanConsecutiv NOT NULL, ");
			SQL.AppendLine("Codigo" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT nnCueBanCodigo NOT NULL, ");
			SQL.AppendLine("Status" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_CueBanSt DEFAULT ('0'), ");
			SQL.AppendLine("NumeroCuenta" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_CueBanNuCu DEFAULT (''), ");
			SQL.AppendLine("NombreCuenta" + InsSql.VarCharTypeForDb(40) + " CONSTRAINT d_CueBanNoCu DEFAULT (''), ");
			SQL.AppendLine("CodigoBanco" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT d_CueBanCoBa DEFAULT (0), ");
			SQL.AppendLine("NombreSucursal" + InsSql.VarCharTypeForDb(40) + " CONSTRAINT d_CueBanNoSu DEFAULT (''), ");
			SQL.AppendLine("TipoCtaBancaria" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_CueBanTiCtBa DEFAULT ('0'), ");
			SQL.AppendLine("ManejaDebitoBancario" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCueBanManejaDebi NOT NULL, ");
			SQL.AppendLine("ManejaCreditoBancario" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCueBanManejaCred NOT NULL, ");
			SQL.AppendLine("SaldoDisponible" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CueBanSaDi DEFAULT (0), ");
			SQL.AppendLine("NombreDeLaMoneda" + InsSql.VarCharTypeForDb(80) + " CONSTRAINT nnCueBanNombreDeLa NOT NULL, ");
			SQL.AppendLine("NombrePlantillaCheque" + InsSql.VarCharTypeForDb(50) + " CONSTRAINT d_CueBanNoPlCh DEFAULT (''), ");
			SQL.AppendLine("CuentaContable" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_CueBanCuCo DEFAULT (''), ");
			SQL.AppendLine("CodigoMoneda" + InsSql.VarCharTypeForDb(4) + " CONSTRAINT d_CueBanCoMo DEFAULT (''), ");
			SQL.AppendLine("TipoDeAlicuotaPorContribuyente" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_CueBanTiDeAlPoCo DEFAULT ('0'), ");
            SQL.AppendLine("ExcluirDelInformeDeDeclaracionIGTF" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_CueBanExcluirDel DEFAULT ('N'), ");
			SQL.AppendLine("GeneraMovBancarioPorIGTF" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCueBanGeneraMovB NOT NULL, ");
			SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(20) + ", ");
			SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
			SQL.AppendLine("EsCajaChica" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCueBanEsCajaChic NOT NULL, ");
			SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
			SQL.AppendLine("CONSTRAINT p_CuentaBancaria PRIMARY KEY CLUSTERED");
			SQL.AppendLine("(ConsecutivoCompania ASC, Codigo ASC)");
			SQL.AppendLine(", CONSTRAINT fk_CuentaBancariaBanco FOREIGN KEY (CodigoBanco)");
			SQL.AppendLine("REFERENCES Comun.Banco(Consecutivo)");
			SQL.AppendLine("ON UPDATE CASCADE");
			SQL.AppendLine(", CONSTRAINT fk_CuentaBancariaMoneda FOREIGN KEY (CodigoMoneda)");
			SQL.AppendLine("REFERENCES dbo.Moneda(Codigo)");
			SQL.AppendLine("ON UPDATE CASCADE");
			SQL.AppendLine(")");
			return SQL.ToString();
		}

		private string SqlViewB1() {
			StringBuilder SQL = new StringBuilder();
			SQL.AppendLine("SELECT CuentaBancaria.ConsecutivoCompania, CuentaBancaria.Codigo, CuentaBancaria.Status, " + DbSchema + ".Gv_EnumStatusCtaBancaria.StrValue AS StatusStr, CuentaBancaria.NumeroCuenta");
			SQL.AppendLine(", CuentaBancaria.NombreCuenta, CuentaBancaria.CodigoBanco, CuentaBancaria.NombreSucursal, CuentaBancaria.TipoCtaBancaria, " + DbSchema + ".Gv_EnumTipoDeCtaBancaria.StrValue AS TipoCtaBancariaStr");
			SQL.AppendLine(", CuentaBancaria.ManejaDebitoBancario, CuentaBancaria.ManejaCreditoBancario, CuentaBancaria.SaldoDisponible, CuentaBancaria.NombreDeLaMoneda");
			SQL.AppendLine(", CuentaBancaria.NombrePlantillaCheque, CuentaBancaria.CuentaContable, CuentaBancaria.CodigoMoneda");
            SQL.AppendLine(", CuentaBancaria.TipoDeAlicuotaPorContribuyente, " + DbSchema + ".Gv_EnumTipoAlicPorContIGTF.StrValue AS TipoDeAlicuotaPorContribuyenteStr, CuentaBancaria.ExcluirDelInformeDeDeclaracionIGTF, CuentaBancaria.GeneraMovBancarioPorIGTF, CuentaBancaria.NombreOperador");
			SQL.AppendLine(", CuentaBancaria.FechaUltimaModificacion, CuentaBancaria.EsCajaChica");
			SQL.AppendLine(", Comun.Banco.Nombre AS NombreBanco");
			SQL.AppendLine(", CuentaBancaria.fldTimeStamp, CAST(CuentaBancaria.fldTimeStamp AS bigint) AS fldTimeStampBigint");
			SQL.AppendLine("FROM " + DbSchema + ".CuentaBancaria");
			SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumStatusCtaBancaria");
			SQL.AppendLine("ON " + DbSchema + ".CuentaBancaria.Status COLLATE MODERN_SPANISH_CS_AS");
			SQL.AppendLine(" = " + DbSchema + ".Gv_EnumStatusCtaBancaria.DbValue");
			SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoDeCtaBancaria");
			SQL.AppendLine("ON " + DbSchema + ".CuentaBancaria.TipoCtaBancaria COLLATE MODERN_SPANISH_CS_AS");
			SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDeCtaBancaria.DbValue");
			SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoAlicPorContIGTF");
			SQL.AppendLine("ON " + DbSchema + ".CuentaBancaria.TipoDeAlicuotaPorContribuyente COLLATE MODERN_SPANISH_CS_AS");
			SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoAlicPorContIGTF.DbValue");
			SQL.AppendLine("INNER JOIN Comun.Banco ON  " + DbSchema + ".CuentaBancaria.CodigoBanco = Comun.Banco.Consecutivo");
			return SQL.ToString();
		}

		private string SqlSpInsParameters() {
			StringBuilder SQL = new StringBuilder();
			SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
			SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(5) + ",");
			SQL.AppendLine("@Status" + InsSql.CharTypeForDb(1) + " = '0',");
			SQL.AppendLine("@NumeroCuenta" + InsSql.VarCharTypeForDb(30) + " = '',");
			SQL.AppendLine("@NombreCuenta" + InsSql.VarCharTypeForDb(40) + " = '',");
			SQL.AppendLine("@CodigoBanco" + InsSql.NumericTypeForDb(10, 0) + ",");
			SQL.AppendLine("@NombreSucursal" + InsSql.VarCharTypeForDb(40) + " = '',");
			SQL.AppendLine("@TipoCtaBancaria" + InsSql.CharTypeForDb(1) + " = '0',");
			SQL.AppendLine("@ManejaDebitoBancario" + InsSql.CharTypeForDb(1) + " = 'N',");
			SQL.AppendLine("@ManejaCreditoBancario" + InsSql.CharTypeForDb(1) + " = 'N',");
			SQL.AppendLine("@SaldoDisponible" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
			SQL.AppendLine("@NombreDeLaMoneda" + InsSql.VarCharTypeForDb(80) + ",");
			SQL.AppendLine("@NombrePlantillaCheque" + InsSql.VarCharTypeForDb(50) + " = '',");
			SQL.AppendLine("@CuentaContable" + InsSql.VarCharTypeForDb(30) + ",");
			SQL.AppendLine("@CodigoMoneda" + InsSql.VarCharTypeForDb(4) + ",");
			SQL.AppendLine("@EsCajaChica" + InsSql.CharTypeForDb(1) + " = 'N' , ");
			SQL.AppendLine("@TipoDeAlicuotaPorContribuyente" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@ExcluirDelInformeDeDeclaracionIGTF" + InsSql.CharTypeForDb(1) + " = 'N',");
			SQL.AppendLine("@GeneraMovBancarioPorIGTF" + InsSql.CharTypeForDb(1) + " = 'N',");
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
			SQL.AppendLine("            INSERT INTO " + DbSchema + ".CuentaBancaria(");
			SQL.AppendLine("            ConsecutivoCompania,");
			SQL.AppendLine("            Codigo,");
			SQL.AppendLine("            Status,");
			SQL.AppendLine("            NumeroCuenta,");
			SQL.AppendLine("            NombreCuenta,");
			SQL.AppendLine("            CodigoBanco,");
			SQL.AppendLine("            NombreSucursal,");
			SQL.AppendLine("            TipoCtaBancaria,");
			SQL.AppendLine("            ManejaDebitoBancario,");
			SQL.AppendLine("            ManejaCreditoBancario,");
			SQL.AppendLine("            SaldoDisponible,");
			SQL.AppendLine("            NombreDeLaMoneda,");
			SQL.AppendLine("            NombrePlantillaCheque,");
			SQL.AppendLine("            CuentaContable,");
			SQL.AppendLine("            CodigoMoneda,");
			SQL.AppendLine("            TipoDeAlicuotaPorContribuyente,");
            SQL.AppendLine("            ExcluirDelInformeDeDeclaracionIGTF,");
			SQL.AppendLine("            GeneraMovBancarioPorIGTF,");
			SQL.AppendLine("            NombreOperador,");
			SQL.AppendLine("            FechaUltimaModificacion,");
			SQL.AppendLine("            EsCajaChica)");
			SQL.AppendLine("            VALUES(");
			SQL.AppendLine("            @ConsecutivoCompania,");
			SQL.AppendLine("            @Codigo,");
			SQL.AppendLine("            @Status,");
			SQL.AppendLine("            @NumeroCuenta,");
			SQL.AppendLine("            @NombreCuenta,");
			SQL.AppendLine("            @CodigoBanco,");
			SQL.AppendLine("            @NombreSucursal,");
			SQL.AppendLine("            @TipoCtaBancaria,");
			SQL.AppendLine("            @ManejaDebitoBancario,");
			SQL.AppendLine("            @ManejaCreditoBancario,");
			SQL.AppendLine("            @SaldoDisponible,");
			SQL.AppendLine("            @NombreDeLaMoneda,");
			SQL.AppendLine("            @NombrePlantillaCheque,");
			SQL.AppendLine("            @CuentaContable,");
			SQL.AppendLine("            @CodigoMoneda,");
			SQL.AppendLine("            @TipoDeAlicuotaPorContribuyente,");
            SQL.AppendLine("            @ExcluirDelInformeDeDeclaracionIGTF,");
			SQL.AppendLine("            @GeneraMovBancarioPorIGTF,");
			SQL.AppendLine("            @NombreOperador,");
			SQL.AppendLine("            @FechaUltimaModificacion,");
			SQL.AppendLine("            @EsCajaChica)");
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
			SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(5) + ",");
			SQL.AppendLine("@Status" + InsSql.CharTypeForDb(1) + ",");
			SQL.AppendLine("@NumeroCuenta" + InsSql.VarCharTypeForDb(30) + ",");
			SQL.AppendLine("@NombreCuenta" + InsSql.VarCharTypeForDb(40) + ",");
			SQL.AppendLine("@CodigoBanco" + InsSql.NumericTypeForDb(10, 0) + ",");
			SQL.AppendLine("@NombreSucursal" + InsSql.VarCharTypeForDb(40) + ",");
			SQL.AppendLine("@TipoCtaBancaria" + InsSql.CharTypeForDb(1) + ",");
			SQL.AppendLine("@ManejaDebitoBancario" + InsSql.CharTypeForDb(1) + ",");
			SQL.AppendLine("@ManejaCreditoBancario" + InsSql.CharTypeForDb(1) + ",");
			SQL.AppendLine("@SaldoDisponible" + InsSql.DecimalTypeForDb(25, 4) + ",");
			SQL.AppendLine("@NombreDeLaMoneda" + InsSql.VarCharTypeForDb(80) + ",");
			SQL.AppendLine("@NombrePlantillaCheque" + InsSql.VarCharTypeForDb(50) + ",");
			SQL.AppendLine("@CuentaContable" + InsSql.VarCharTypeForDb(30) + ",");
			SQL.AppendLine("@CodigoMoneda" + InsSql.VarCharTypeForDb(4) + ",");
			SQL.AppendLine("@EsCajaChica" + InsSql.CharTypeForDb(1) + ",");
			SQL.AppendLine("@TipoDeAlicuotaPorContribuyente" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ExcluirDelInformeDeDeclaracionIGTF" + InsSql.CharTypeForDb(1) + ",");
			SQL.AppendLine("@GeneraMovBancarioPorIGTF" + InsSql.CharTypeForDb(1) + ",");
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
			SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CuentaBancaria WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @Codigo)");
			SQL.AppendLine("   BEGIN");
			SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CuentaBancaria WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @Codigo");
			SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
			SQL.AppendLine("      BEGIN");
			SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_CuentaBancariaCanBeUpdated @ConsecutivoCompania,@Codigo, @CurrentTimeStamp, @ValidationMsg out");
			//SQL.AppendLine("--IF @CanBeChanged = 1 --True");
			//SQL.AppendLine("--BEGIN");
			//SQL.AppendLine("         BEGIN TRAN");
			SQL.AppendLine("         UPDATE " + DbSchema + ".CuentaBancaria");
			SQL.AppendLine("            SET Status = @Status,");
			SQL.AppendLine("               NumeroCuenta = @NumeroCuenta,");
			SQL.AppendLine("               NombreCuenta = @NombreCuenta,");
			SQL.AppendLine("               CodigoBanco = @CodigoBanco,");
			SQL.AppendLine("               NombreSucursal = @NombreSucursal,");
			SQL.AppendLine("               TipoCtaBancaria = @TipoCtaBancaria,");
			SQL.AppendLine("               ManejaDebitoBancario = @ManejaDebitoBancario,");
			SQL.AppendLine("               ManejaCreditoBancario = @ManejaCreditoBancario,");
			SQL.AppendLine("               SaldoDisponible = @SaldoDisponible,");
			SQL.AppendLine("               NombreDeLaMoneda = @NombreDeLaMoneda,");
			SQL.AppendLine("               NombrePlantillaCheque = @NombrePlantillaCheque,");
			SQL.AppendLine("               CuentaContable = @CuentaContable,");
			SQL.AppendLine("               CodigoMoneda = @CodigoMoneda,");
			SQL.AppendLine("               TipoDeAlicuotaPorContribuyente = @TipoDeAlicuotaPorContribuyente,");
            SQL.AppendLine("               ExcluirDelInformeDeDeclaracionIGTF = @ExcluirDelInformeDeDeclaracionIGTF,");
			SQL.AppendLine("               GeneraMovBancarioPorIGTF = @GeneraMovBancarioPorIGTF,");
			SQL.AppendLine("               NombreOperador = @NombreOperador,");
			SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion,");
			SQL.AppendLine("               EsCajaChica = @EsCajaChica");
			SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
			SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
			SQL.AppendLine("               AND Codigo = @Codigo");
			SQL.AppendLine("         SET @ReturnValue = @@ROWCOUNT");
			SQL.AppendLine("         IF @@ERROR = 0");
			SQL.AppendLine("         BEGIN");
			//SQL.AppendLine("            COMMIT TRAN");
			SQL.AppendLine("            IF @ReturnValue = 0");
			SQL.AppendLine("               RAISERROR('El registro ha sido modificado o eliminado por otro usuario', 14, 1)");
			SQL.AppendLine("         END");
			SQL.AppendLine("         ELSE");
			SQL.AppendLine("         BEGIN");
			SQL.AppendLine("            SET @ReturnValue = -1");
			SQL.AppendLine("            SET @ValidationMsg = 'Se ha producido un error al Modificar: ' + CAST(@@ERROR AS NVARCHAR(8))");
			SQL.AppendLine("            RAISERROR(@ValidationMsg, 14 ,1)");
			//SQL.AppendLine("            ROLLBACK");
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
			SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(5) + ",");
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
			SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CuentaBancaria WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @Codigo)");
			SQL.AppendLine("   BEGIN");
			SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CuentaBancaria WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @Codigo");
			SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
			SQL.AppendLine("      BEGIN");
			SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_CuentaBancariaCanBeDeleted @ConsecutivoCompania,@Codigo, @CurrentTimeStamp, @ValidationMsg out");
			//SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
			//SQL.AppendLine("--BEGIN");
			//SQL.AppendLine("         BEGIN TRAN");
			SQL.AppendLine("         DELETE FROM " + DbSchema + ".CuentaBancaria");
			SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
			SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
			SQL.AppendLine("               AND Codigo = @Codigo");
			SQL.AppendLine("         SET @ReturnValue = @@ROWCOUNT");
			SQL.AppendLine("         IF @@ERROR = 0");
			SQL.AppendLine("         BEGIN");
			//SQL.AppendLine("            COMMIT TRAN");
			SQL.AppendLine("            IF @ReturnValue = 0");
			SQL.AppendLine("               RAISERROR('El registro ha sido modificado o eliminado por otro usuario', 14, 1)");
			SQL.AppendLine("         END");
			SQL.AppendLine("         ELSE");
			SQL.AppendLine("         BEGIN");
			SQL.AppendLine("            SET @ReturnValue = -1");
			SQL.AppendLine("            SET @ValidationMsg = 'Se ha producido un error al Eliminar: ' + CAST(@@ERROR AS NVARCHAR(8))");
			SQL.AppendLine("            RAISERROR(@ValidationMsg, 14 ,1)");
			//SQL.AppendLine("            ROLLBACK");
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
			SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(5));
			return SQL.ToString();
		}

		private string SqlSpGet() {
			StringBuilder SQL = new StringBuilder();
			SQL.AppendLine("BEGIN");
			SQL.AppendLine("   SET NOCOUNT ON;");
			SQL.AppendLine("   SELECT ");
			SQL.AppendLine("         CuentaBancaria.ConsecutivoCompania,");
			SQL.AppendLine("         CuentaBancaria.Codigo,");
			SQL.AppendLine("         CuentaBancaria.Status,");
			SQL.AppendLine("         CuentaBancaria.NumeroCuenta,");
			SQL.AppendLine("         CuentaBancaria.NombreCuenta,");
			SQL.AppendLine("         CuentaBancaria.CodigoBanco,");
			SQL.AppendLine("         Comun.Gv_Banco_B1.Codigo AS CodigoBancoPant,");
			SQL.AppendLine("         Comun.Gv_Banco_B1.Nombre AS NombreBanco,");
			SQL.AppendLine("         CuentaBancaria.NombreSucursal,");
			SQL.AppendLine("         CuentaBancaria.TipoCtaBancaria,");
			SQL.AppendLine("         CuentaBancaria.ManejaDebitoBancario,");
			SQL.AppendLine("         CuentaBancaria.ManejaCreditoBancario,");
			SQL.AppendLine("         CuentaBancaria.SaldoDisponible,");
			SQL.AppendLine("         CuentaBancaria.NombreDeLaMoneda,");
			SQL.AppendLine("         CuentaBancaria.NombrePlantillaCheque,");
			SQL.AppendLine("         CuentaBancaria.CuentaContable,");
			SQL.AppendLine("         CuentaBancaria.CodigoMoneda,");
			SQL.AppendLine("         CuentaBancaria.TipoDeAlicuotaPorContribuyente,");
            SQL.AppendLine("         CuentaBancaria.ExcluirDelInformeDeDeclaracionIGTF,");
			SQL.AppendLine("         CuentaBancaria.GeneraMovBancarioPorIGTF,");
			SQL.AppendLine("         CuentaBancaria.NombreOperador,");
			SQL.AppendLine("         CuentaBancaria.FechaUltimaModificacion,");
			SQL.AppendLine("         CuentaBancaria.EsCajaChica,");
			SQL.AppendLine("         CAST(CuentaBancaria.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
			SQL.AppendLine("         CuentaBancaria.fldTimeStamp");
			SQL.AppendLine("      FROM " + DbSchema + ".CuentaBancaria");
			SQL.AppendLine("             INNER JOIN Comun.Gv_Banco_B1 ON " + DbSchema + ".CuentaBancaria.CodigoBanco = Comun.Gv_Banco_B1.Consecutivo");
			SQL.AppendLine("             INNER JOIN dbo.Gv_Moneda_B1 ON " + DbSchema + ".CuentaBancaria.CodigoMoneda = dbo.Gv_Moneda_B1.Codigo");
			SQL.AppendLine("      WHERE CuentaBancaria.ConsecutivoCompania = @ConsecutivoCompania");
			SQL.AppendLine("         AND CuentaBancaria.Codigo = @Codigo");
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
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.Codigo,");
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.StatusStr,");
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.NumeroCuenta,");
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.NombreCuenta,");
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.CodigoBanco,");
			SQL.AppendLine("      Comun.Gv_Banco_B1.Nombre AS NombreBanco,");
			SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.ConsecutivoCompania,");
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.Status,");
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.TipoCtaBancariaStr,");
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.TipoCtaBancaria,");
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.NombreDeLaMoneda,");
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.CodigoMoneda,");
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.SaldoDisponible,");
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.CuentaContable,");
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.ManejaDebitoBancario,");
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.ManejaCreditoBancario,");
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.NombrePlantillaCheque,");
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.TipoDeAlicuotaPorContribuyente,");
			SQL.AppendLine("      " + DbSchema + ".Gv_CuentaBancaria_B1.GeneraMovBancarioPorIGTF");
			SQL.AppendLine("      FROM " + DbSchema + ".Gv_CuentaBancaria_B1");
			SQL.AppendLine("      INNER JOIN Comun.Gv_Banco_B1 ON  " + DbSchema + ".Gv_CuentaBancaria_B1.CodigoBanco = Comun.Gv_Banco_B1.Consecutivo");
			SQL.AppendLine("      INNER JOIN dbo.Gv_Moneda_B1 ON  " + DbSchema + ".Gv_CuentaBancaria_B1.CodigoMoneda = dbo.Gv_Moneda_B1.Codigo");
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
			SQL.AppendLine("      " + DbSchema + ".CuentaBancaria.ConsecutivoCompania,");
			SQL.AppendLine("      " + DbSchema + ".CuentaBancaria.Codigo,");
			SQL.AppendLine("      " + DbSchema + ".CuentaBancaria.Status,");
			SQL.AppendLine("      " + DbSchema + ".CuentaBancaria.NumeroCuenta,");
			SQL.AppendLine("      " + DbSchema + ".CuentaBancaria.NombreCuenta,");
			SQL.AppendLine("      " + DbSchema + ".CuentaBancaria.CodigoBanco,");
			SQL.AppendLine("      " + DbSchema + ".CuentaBancaria.TipoCtaBancaria,");
			SQL.AppendLine("      " + DbSchema + ".CuentaBancaria.NombreDeLaMoneda,");
			SQL.AppendLine("      " + DbSchema + ".CuentaBancaria.CuentaContable,");
			SQL.AppendLine("      " + DbSchema + ".CuentaBancaria.CodigoMoneda");
			//SQL.AppendLine("      ," + DbSchema + ".CuentaBancaria.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
			SQL.AppendLine("      FROM " + DbSchema + ".CuentaBancaria");
			SQL.AppendLine("   RETURN @@ROWCOUNT");
			SQL.AppendLine("END");
			return SQL.ToString();
		}

		public string SqlSpActualizaSaldoDisponibleParameters() {
			StringBuilder SQL = new StringBuilder();
			SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
			SQL.AppendLine("@CodigoCuenta" + InsSql.VarCharTypeForDb(5) + ",");
			SQL.AppendLine("@Monto" + InsSql.DecimalTypeForDb(25, 4) + ",");
			SQL.AppendLine("@IngresoEgreso" + InsSql.CharTypeForDb(1) + ",");
			SQL.AppendLine("@mAction" + InsSql.NumericTypeForDb(3, 0) + ",");
			SQL.AppendLine("@MontoOriginal" + InsSql.DecimalTypeForDb(25, 4) + ",");
			SQL.AppendLine("@SeModificoTipoConcepto" + InsSql.CharTypeForDb(1) + ",");
			SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(20) + ",");
			SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + "");
			return SQL.ToString();
		}

		public string SqlSpActualizaSaldoDisponible() {
			StringBuilder SQL = new StringBuilder();
			SQL.AppendLine("BEGIN");
			SQL.AppendLine("   SET NOCOUNT ON;");
			SQL.AppendLine("   DECLARE @ValidationMsg " + InsSql.VarCharTypeForDb(1500) + " --No puede ser más");
			SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
			SQL.AppendLine("   DECLARE @Diferencia " + InsSql.DecimalTypeForDb(25, 4) + "");
			SQL.AppendLine("   SET @ReturnValue = -1");
			SQL.AppendLine("   SET @ValidationMsg = ''");

			SQL.AppendLine("   IF @mAction = " + ((int) eAccionSR.Insertar).ToString());
			SQL.AppendLine("   BEGIN ");
			SQL.AppendLine("     SET @Diferencia = @Monto - @MontoOriginal");
			SQL.AppendLine("     IF @IngresoEgreso = " + ((int) eIngresoEgreso.Egreso).ToString());
			SQL.AppendLine("        SET @Diferencia = -@Diferencia");
			SQL.AppendLine("   END ");

			SQL.AppendLine("   IF @mAction = " + ((int) eAccionSR.Eliminar).ToString());
			SQL.AppendLine("   BEGIN ");
			SQL.AppendLine("     IF @IngresoEgreso = " + ((int) eIngresoEgreso.Ingreso).ToString());
			SQL.AppendLine("        SET @Diferencia = -@Monto");
			SQL.AppendLine("     ELSE");
			SQL.AppendLine("        SET @Diferencia = @Monto");
			SQL.AppendLine("   END ");

			SQL.AppendLine("   IF @mAction = " + ((int) eAccionSR.Modificar).ToString() + " AND @SeModificoTipoConcepto = 'N' ");
			SQL.AppendLine("   BEGIN ");
			SQL.AppendLine("     IF @Monto > @MontoOriginal ");
			SQL.AppendLine("     BEGIN ");
			SQL.AppendLine("        SET @Diferencia = @Monto - @MontoOriginal ");
			SQL.AppendLine("        IF @IngresoEgreso = " + ((int) eIngresoEgreso.Egreso).ToString());
			SQL.AppendLine("        BEGIN ");
			SQL.AppendLine("            SET @Diferencia = @Monto - @MontoOriginal ");
			SQL.AppendLine("            SET @Diferencia = -@Diferencia ");
			SQL.AppendLine("        END ");
			SQL.AppendLine("     END ");
			SQL.AppendLine("     ELSE ");
			SQL.AppendLine("     BEGIN ");
			SQL.AppendLine("         IF @Monto < @MontoOriginal ");
			SQL.AppendLine("         BEGIN ");
			SQL.AppendLine("            SET @Diferencia = @MontoOriginal - @Monto ");
			SQL.AppendLine("            IF @IngresoEgreso = " + ((int) eIngresoEgreso.Ingreso).ToString());
			SQL.AppendLine("              SET @Diferencia = -@Diferencia ");
			SQL.AppendLine("         END ");
			SQL.AppendLine("         ELSE ");
			SQL.AppendLine("         IF @Monto = @MontoOriginal ");
			SQL.AppendLine("            SET @Diferencia = 0 ");
			SQL.AppendLine("     END ");
			SQL.AppendLine("   END ");
			SQL.AppendLine("   ELSE ");
			SQL.AppendLine("   BEGIN ");
			SQL.AppendLine("        IF @mAction = " + ((int) eAccionSR.Modificar).ToString() + " AND @SeModificoTipoConcepto = 'S' ");
			SQL.AppendLine("        BEGIN ");
			SQL.AppendLine("           SET @Diferencia = @Monto + @MontoOriginal ");
			SQL.AppendLine("              IF @IngresoEgreso = " + ((int) eIngresoEgreso.Egreso).ToString());
			SQL.AppendLine("                SET @Diferencia = -@Diferencia ");
			SQL.AppendLine("         END ");
			SQL.AppendLine("   END ");

			SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CuentaBancaria WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @CodigoCuenta)");
			SQL.AppendLine("   BEGIN");
			//SQL.AppendLine("         BEGIN TRAN");
			SQL.AppendLine("         UPDATE " + DbSchema + ".CuentaBancaria");
			SQL.AppendLine("            SET SaldoDisponible = SaldoDisponible + @Diferencia,");
			SQL.AppendLine("               NombreOperador = @NombreOperador,");
			SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
			SQL.AppendLine("            WHERE ConsecutivoCompania = @ConsecutivoCompania");
			SQL.AppendLine("               AND Codigo = @CodigoCuenta");
			SQL.AppendLine("         SET @ReturnValue = @@ROWCOUNT");
			SQL.AppendLine("         IF @@ERROR = 0");
			SQL.AppendLine("         BEGIN");
			//SQL.AppendLine("            COMMIT TRAN");
			SQL.AppendLine("            IF @ReturnValue = 0");
			SQL.AppendLine("               RAISERROR('El registro ha sido modificado o eliminado por otro usuario', 14, 1)");
			SQL.AppendLine("         END");
			SQL.AppendLine("         ELSE");
			SQL.AppendLine("         BEGIN");
			SQL.AppendLine("            SET @ReturnValue = -1");
			SQL.AppendLine("            SET @ValidationMsg = 'Se ha producido un error al Modificar: ' + CAST(@@ERROR AS NVARCHAR(8))");
			SQL.AppendLine("            RAISERROR(@ValidationMsg, 14 ,1)");
			//SQL.AppendLine("            ROLLBACK");
			SQL.AppendLine("         END");
			SQL.AppendLine("   END");
			SQL.AppendLine("   ELSE");
			SQL.AppendLine("      RAISERROR('El registro no existe.', 14, 1)");
			SQL.AppendLine("   RETURN @ReturnValue");
			SQL.AppendLine("END");
			return SQL.ToString();
		}

		private string SqlSpRecalculaSaldoParameters() {
			StringBuilder SQL = new StringBuilder();
			SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " ");
			return SQL.ToString();
		}

		public string SqlSpRecalculaSaldo() {
			StringBuilder SQL = new StringBuilder();
			SQL.AppendLine("BEGIN");
			SQL.AppendLine("   SET NOCOUNT ON;");
			SQL.AppendLine("   DECLARE @ValidationMsg " + InsSql.VarCharTypeForDb(1500) + " --No puede ser más");
			SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
			//SQL.AppendLine("--DECLARE @CanBeChanged bit");
			SQL.AppendLine("   SET @ReturnValue = -1");
			SQL.AppendLine("   SET @ValidationMsg = ''");
			SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CuentaBancaria WHERE ConsecutivoCompania = @ConsecutivoCompania) ");
			SQL.AppendLine("   BEGIN");
			//SQL.AppendLine("         BEGIN TRAN");
			SQL.AppendLine("         UPDATE " + DbSchema + ".CuentaBancaria ");
			SQL.AppendLine("           SET SaldoDisponible = tmpMovBan.NewSaldo ");
			SQL.AppendLine("           FROM ");
			SQL.AppendLine("           (SELECT MovimientoBancario.CodigoCtaBancaria, ");
			SQL.AppendLine("           sum(CASE WHEN MovimientoBancario.TipoConcepto='0' THEN MovimientoBancario.Monto ");
			SQL.AppendLine("           ELSE -MovimientoBancario.Monto END) AS NewSaldo ");
			SQL.AppendLine("           FROM MovimientoBancario INNER JOIN CuentaBancaria ");
			SQL.AppendLine("           ON (MovimientoBancario.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania ");
			SQL.AppendLine("           and MovimientoBancario.CodigoCtaBancaria = CuentaBancaria.Codigo) ");
			SQL.AppendLine("           WHERE MovimientoBancario.ConsecutivoCompania=@ConsecutivoCompania ");
			SQL.AppendLine("           GROUP BY CodigoCtaBancaria) tmpMovBan ");
			SQL.AppendLine("           WHERE tmpMovBan.CodigoCtaBancaria = CuentaBancaria.Codigo ");
			SQL.AppendLine("               AND CuentaBancaria.ConsecutivoCompania=@ConsecutivoCompania ");
			SQL.AppendLine("         SET @ReturnValue = @@ROWCOUNT");
			SQL.AppendLine("         IF @@ERROR = 0");
			SQL.AppendLine("         BEGIN");
			//SQL.AppendLine("            COMMIT TRAN");
			SQL.AppendLine("            IF @ReturnValue = 0");
			SQL.AppendLine("               RAISERROR('No se actualizó ninguna Cuenta Bancaria', 14, 1)");
			SQL.AppendLine("         END");
			SQL.AppendLine("         ELSE");
			SQL.AppendLine("         BEGIN");
			SQL.AppendLine("            SET @ReturnValue = -1");
			SQL.AppendLine("            SET @ValidationMsg = 'Se ha producido un error al Modificar: ' + CAST(@@ERROR AS NVARCHAR(8))");
			SQL.AppendLine("            RAISERROR(@ValidationMsg, 14 ,1)");
			//SQL.AppendLine("            ROLLBACK");
			SQL.AppendLine("         END");
			SQL.AppendLine("   END");
			SQL.AppendLine("   ELSE");
			SQL.AppendLine("      RAISERROR('El registro no existe.', 14, 1)");
			SQL.AppendLine("   RETURN @ReturnValue");
			SQL.AppendLine("END");
			return SQL.ToString();
		}
		#endregion //Queries

		bool CrearTabla() {
			bool vResult = insDbo.Create(DbSchema + ".CuentaBancaria", SqlCreateTable(), false, eDboType.Tabla);
			return vResult;
		}
		bool CrearVistas() {
			bool vResult = false;
			LibViews insVistas = new LibViews();
			vResult = insVistas.Create(DbSchema + ".Gv_EnumStatusCtaBancaria", LibTpvCreator.SqlViewStandardEnum(typeof(eStatusCtaBancaria), InsSql), true, true);
			vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDeCtaBancaria", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDeCtaBancaria), InsSql), true, true);
			vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoAlicPorContIGTF", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoAlicPorContIGTF), InsSql), true, true);
			vResult = insVistas.Create(DbSchema + ".Gv_CuentaBancaria_B1", SqlViewB1(), true);
			insVistas.Dispose();
			return vResult;
		}
		bool CrearProcedimientos() {
			bool vResult = false;
			LibStoredProc insSps = new LibStoredProc();
			vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CuentaBancariaINS", SqlSpInsParameters(), SqlSpIns(), true);
			vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CuentaBancariaUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
			vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CuentaBancariaDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
			vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CuentaBancariaGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
			vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CuentaBancariaSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
			vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CuentaBancariaGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
			vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CuentaBancariaActualizaSaldoDisponible", SqlSpActualizaSaldoDisponibleParameters(), SqlSpActualizaSaldoDisponible(), true) && vResult;
			vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CuentaBancariaRecalculaSaldo", SqlSpRecalculaSaldoParameters(), SqlSpRecalculaSaldo(), true) && vResult;
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
			if (insDbo.Exists(DbSchema + ".CuentaBancaria", eDboType.Tabla)) {
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
			vResult = insSp.Drop(DbSchema + ".Gp_CuentaBancariaINS");
			vResult = insSp.Drop(DbSchema + ".Gp_CuentaBancariaUPD") && vResult;
			vResult = insSp.Drop(DbSchema + ".Gp_CuentaBancariaDEL") && vResult;
			vResult = insSp.Drop(DbSchema + ".Gp_CuentaBancariaGET") && vResult;
			vResult = insSp.Drop(DbSchema + ".Gp_CuentaBancariaGetFk") && vResult;
			vResult = insSp.Drop(DbSchema + ".Gp_CuentaBancariaSCH") && vResult;
			vResult = insSp.Drop(DbSchema + ".Gp_CuentaBancariaActualizaSaldoDisponible") && vResult;
			vResult = insSp.Drop(DbSchema + ".Gp_CuentaBancariaRecalculaSaldo") && vResult;
			vResult = insVista.Drop(DbSchema + ".Gv_CuentaBancaria_B1") && vResult;
			vResult = insVista.Drop(DbSchema + ".Gv_EnumStatusCtaBancaria") && vResult;
			vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDeCtaBancaria") && vResult;
			vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoAlicPorContIGTF") && vResult;
			insSp.Dispose();
			insVista.Dispose();
			return vResult;
		}
		#endregion //Metodos Generados

	} //End of class clsCuentaBancariaED

} //End of namespace Galac.Adm.Dal.Banco

