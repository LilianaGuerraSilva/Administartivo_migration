using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal; 
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Dal.Banco { 
    public class clsMovimientoBancarioED: LibED {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsMovimientoBancarioED(): base(){
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("MovimientoBancario", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnMovBanConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoMovimiento" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnMovBanConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoCtaBancaria" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT nnMovBanCodigoCtaB NOT NULL, ");
            SQL.AppendLine("CodigoConcepto" + InsSql.VarCharTypeForDb(8) + " CONSTRAINT nnMovBanCodigoConc NOT NULL, ");
            SQL.AppendLine("Fecha" + InsSql.DateTypeForDb() + " CONSTRAINT nnMovBanFecha NOT NULL, ");
            SQL.AppendLine("TipoConcepto" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_MovBanTiCo DEFAULT ('0'), ");
            SQL.AppendLine("Monto" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_MovBanMo DEFAULT (0), ");
            SQL.AppendLine("NumeroDocumento" + InsSql.VarCharTypeForDb(15) + " CONSTRAINT d_MovBanNuDo DEFAULT (''), ");
            SQL.AppendLine("Descripcion" + InsSql.VarCharTypeForDb(255) + " CONSTRAINT d_MovBanDe DEFAULT (''), ");
            SQL.AppendLine("GeneraImpuestoBancario" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnMovBanGeneraImpu NOT NULL, ");
            SQL.AppendLine("NroMovimientoRelacionado" + InsSql.VarCharTypeForDb(15) + " CONSTRAINT d_MovBanNrMoRe DEFAULT (''), ");
            SQL.AppendLine("GeneradoPor" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_MovBanGePo DEFAULT ('0'), ");
            SQL.AppendLine("CambioABolivares" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_MovBanCaAB DEFAULT (0), ");
            SQL.AppendLine("ImprimirCheque" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnMovBanImprimirCh NOT NULL, ");
            SQL.AppendLine("ConciliadoSN" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnMovBanConciliado NOT NULL, ");
            SQL.AppendLine("NroConciliacion" + InsSql.VarCharTypeForDb(9) + " CONSTRAINT d_MovBanNrCo DEFAULT (''), ");
            SQL.AppendLine("GenerarAsientoDeRetiroEnCuenta" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnMovBanGenerarAsi NOT NULL, ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_MovimientoBancario PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, ConsecutivoMovimiento ASC, CodigoCtaBancaria ASC, CodigoConcepto ASC, Fecha ASC)");
            SQL.AppendLine(", CONSTRAINT fk_MovimientoBancarioCuentaBancaria FOREIGN KEY (ConsecutivoCompania, CodigoCtaBancaria)");
            SQL.AppendLine("REFERENCES Saw.CuentaBancaria(ConsecutivoCompania, Codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_MovimientoBancarioConceptoBancario FOREIGN KEY (CodigoConcepto)");
            SQL.AppendLine("REFERENCES Adm.ConceptoBancario(Codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT MovimientoBancario.ConsecutivoCompania, MovimientoBancario.ConsecutivoMovimiento, MovimientoBancario.CodigoCtaBancaria, MovimientoBancario.CodigoConcepto");
            SQL.AppendLine(", MovimientoBancario.Fecha, MovimientoBancario.TipoConcepto, " + DbSchema + ".Gv_EnumIngresoEgreso.StrValue AS TipoConceptoStr, MovimientoBancario.Monto, MovimientoBancario.NumeroDocumento");
            SQL.AppendLine(", MovimientoBancario.Descripcion, MovimientoBancario.GeneraImpuestoBancario, MovimientoBancario.NroMovimientoRelacionado, MovimientoBancario.GeneradoPor, " + DbSchema + ".Gv_EnumGeneradoPor.StrValue AS GeneradoPorStr");
            SQL.AppendLine(", MovimientoBancario.CambioABolivares, MovimientoBancario.ImprimirCheque, MovimientoBancario.ConciliadoSN, MovimientoBancario.NroConciliacion");
            SQL.AppendLine(", MovimientoBancario.GenerarAsientoDeRetiroEnCuenta, MovimientoBancario.NombreOperador, MovimientoBancario.FechaUltimaModificacion");
            SQL.AppendLine(", MovimientoBancario.fldTimeStamp, CAST(MovimientoBancario.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + "dbo" + ".MovimientoBancario");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumIngresoEgreso");
            SQL.AppendLine("ON " + "dbo" + ".MovimientoBancario.TipoConcepto COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumIngresoEgreso.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumGeneradoPor");
            SQL.AppendLine("ON " + "dbo" + ".MovimientoBancario.GeneradoPor COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumGeneradoPor.DbValue");
            SQL.AppendLine("INNER JOIN Saw.CuentaBancaria ON  " + "dbo" + ".MovimientoBancario.CodigoCtaBancaria = Saw.CuentaBancaria.Codigo");
            SQL.AppendLine("      AND " + "dbo" + ".MovimientoBancario.ConsecutivoCompania = Saw.CuentaBancaria.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN Adm.ConceptoBancario ON  " + "dbo" + ".MovimientoBancario.CodigoConcepto = Adm.ConceptoBancario.Codigo");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoMovimiento" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoCtaBancaria" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@CodigoConcepto" + InsSql.VarCharTypeForDb(8) + ",");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@TipoConcepto" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Monto" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(15) + " = '',");
            SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(255) + " = '',");
            SQL.AppendLine("@GeneraImpuestoBancario" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@NroMovimientoRelacionado" + InsSql.VarCharTypeForDb(15) + " = '',");
            SQL.AppendLine("@GeneradoPor" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@CambioABolivares" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@ImprimirCheque" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@ConciliadoSN" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@NroConciliacion" + InsSql.VarCharTypeForDb(9) + " = '',");
            SQL.AppendLine("@GenerarAsientoDeRetiroEnCuenta" + InsSql.CharTypeForDb(1) + " = 'N',");
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
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
           // SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + "dbo" + ".MovimientoBancario(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            ConsecutivoMovimiento,");
            SQL.AppendLine("            CodigoCtaBancaria,");
            SQL.AppendLine("            CodigoConcepto,");
            SQL.AppendLine("            Fecha,");
            SQL.AppendLine("            TipoConcepto,");
            SQL.AppendLine("            Monto,");
            SQL.AppendLine("            NumeroDocumento,");
            SQL.AppendLine("            Descripcion,");
            SQL.AppendLine("            GeneraImpuestoBancario,");
            SQL.AppendLine("            NroMovimientoRelacionado,");
            SQL.AppendLine("            GeneradoPor,");
            SQL.AppendLine("            CambioABolivares,");
            SQL.AppendLine("            ImprimirCheque,");
            SQL.AppendLine("            ConciliadoSN,");
            SQL.AppendLine("            NroConciliacion,");
            SQL.AppendLine("            GenerarAsientoDeRetiroEnCuenta,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @ConsecutivoMovimiento,");
            SQL.AppendLine("            @CodigoCtaBancaria,");
            SQL.AppendLine("            @CodigoConcepto,");
            SQL.AppendLine("            @Fecha,");
            SQL.AppendLine("            @TipoConcepto,");
            SQL.AppendLine("            @Monto,");
            SQL.AppendLine("            @NumeroDocumento,");
            SQL.AppendLine("            @Descripcion,");
            SQL.AppendLine("            @GeneraImpuestoBancario,");
            SQL.AppendLine("            @NroMovimientoRelacionado,");
            SQL.AppendLine("            @GeneradoPor,");
            SQL.AppendLine("            @CambioABolivares,");
            SQL.AppendLine("            @ImprimirCheque,");
            SQL.AppendLine("            @ConciliadoSN,");
            SQL.AppendLine("            @NroConciliacion,");
            SQL.AppendLine("            @GenerarAsientoDeRetiroEnCuenta,");
            SQL.AppendLine("            @NombreOperador,");
            SQL.AppendLine("            @FechaUltimaModificacion)");
            SQL.AppendLine("            SET @ReturnValue = @@ROWCOUNT");
            //SQL.AppendLine("        COMMIT TRAN");
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
            SQL.AppendLine("@ConsecutivoMovimiento" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoCtaBancaria" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@CodigoConcepto" + InsSql.VarCharTypeForDb(8) + ",");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@TipoConcepto" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Monto" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(15) + ",");
            SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(255) + ",");
            SQL.AppendLine("@GeneraImpuestoBancario" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@NroMovimientoRelacionado" + InsSql.VarCharTypeForDb(15) + ",");
            SQL.AppendLine("@GeneradoPor" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@CambioABolivares" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@ImprimirCheque" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ConciliadoSN" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@NroConciliacion" + InsSql.VarCharTypeForDb(9) + ",");
            SQL.AppendLine("@GenerarAsientoDeRetiroEnCuenta" + InsSql.CharTypeForDb(1) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + "dbo" + ".MovimientoBancario WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoMovimiento = @ConsecutivoMovimiento AND CodigoCtaBancaria = @CodigoCtaBancaria AND CodigoConcepto = @CodigoConcepto AND Fecha = @Fecha)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + "dbo" + ".MovimientoBancario WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoMovimiento = @ConsecutivoMovimiento AND CodigoCtaBancaria = @CodigoCtaBancaria AND CodigoConcepto = @CodigoConcepto AND Fecha = @Fecha");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_MovimientoBancarioCanBeUpdated @ConsecutivoCompania,@ConsecutivoMovimiento,@CodigoCtaBancaria,@CodigoConcepto,@Fecha, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + "dbo" + ".MovimientoBancario");
            SQL.AppendLine("            SET TipoConcepto = @TipoConcepto,");
            SQL.AppendLine("               Monto = @Monto,");
            SQL.AppendLine("               NumeroDocumento = @NumeroDocumento,");
            SQL.AppendLine("               Descripcion = @Descripcion,");
            SQL.AppendLine("               GeneraImpuestoBancario = @GeneraImpuestoBancario,");
            SQL.AppendLine("               NroMovimientoRelacionado = @NroMovimientoRelacionado,");
            SQL.AppendLine("               GeneradoPor = @GeneradoPor,");
            SQL.AppendLine("               CambioABolivares = @CambioABolivares,");
            SQL.AppendLine("               ImprimirCheque = @ImprimirCheque,");
            SQL.AppendLine("               ConciliadoSN = @ConciliadoSN,");
            SQL.AppendLine("               NroConciliacion = @NroConciliacion,");
            SQL.AppendLine("               GenerarAsientoDeRetiroEnCuenta = @GenerarAsientoDeRetiroEnCuenta,");
            SQL.AppendLine("               NombreOperador = @NombreOperador,");
            SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoMovimiento = @ConsecutivoMovimiento");
            SQL.AppendLine("               AND CodigoCtaBancaria = @CodigoCtaBancaria");
            SQL.AppendLine("               AND CodigoConcepto = @CodigoConcepto");
            SQL.AppendLine("               AND Fecha = @Fecha");
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
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoMovimiento" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoCtaBancaria" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@CodigoConcepto" + InsSql.VarCharTypeForDb(8) + ",");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@TimeStampAsInt" + InsSql.BigintTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpDel() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SET DATEFORMAT @DateFormat");
            SQL.AppendLine("   DECLARE @CurrentTimeStamp timestamp");
            SQL.AppendLine("   DECLARE @ValidationMsg " + InsSql.VarCharTypeForDb(1500) + " --No puede ser más");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            //SQL.AppendLine("--DECLARE @CanBeDeleted bit");
            SQL.AppendLine("   SET @ReturnValue = -1");
            SQL.AppendLine("   SET @ValidationMsg = ''");
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + "dbo" + ".MovimientoBancario WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoMovimiento = @ConsecutivoMovimiento AND CodigoCtaBancaria = @CodigoCtaBancaria AND CodigoConcepto = @CodigoConcepto AND Fecha = @Fecha)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + "dbo" + ".MovimientoBancario WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoMovimiento = @ConsecutivoMovimiento AND CodigoCtaBancaria = @CodigoCtaBancaria AND CodigoConcepto = @CodigoConcepto AND Fecha = @Fecha");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_MovimientoBancarioCanBeDeleted @ConsecutivoCompania,@ConsecutivoMovimiento,@CodigoCtaBancaria,@CodigoConcepto,@Fecha, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + "dbo" + ".MovimientoBancario");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoMovimiento = @ConsecutivoMovimiento");
            SQL.AppendLine("               AND CodigoCtaBancaria = @CodigoCtaBancaria");
            SQL.AppendLine("               AND CodigoConcepto = @CodigoConcepto");
            SQL.AppendLine("               AND Fecha = @Fecha");
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
            SQL.AppendLine("@ConsecutivoMovimiento" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoCtaBancaria" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@CodigoConcepto" + InsSql.VarCharTypeForDb(8) + ",");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         MovimientoBancario.ConsecutivoCompania,");
            SQL.AppendLine("         MovimientoBancario.ConsecutivoMovimiento,");
            SQL.AppendLine("         MovimientoBancario.CodigoCtaBancaria,");
            SQL.AppendLine("         Gv_CuentaBancaria_B1.nombrecuenta AS NombreCtaBancaria,");
            SQL.AppendLine("         MovimientoBancario.CodigoConcepto,");
            SQL.AppendLine("         Adm.Gv_ConceptoBancario_B1.descripcion AS DescripcionConcepto,");
            SQL.AppendLine("         MovimientoBancario.Fecha,");
            SQL.AppendLine("         MovimientoBancario.TipoConcepto,");
            SQL.AppendLine("         MovimientoBancario.Monto,");
            SQL.AppendLine("         MovimientoBancario.NumeroDocumento,");
            SQL.AppendLine("         MovimientoBancario.Descripcion,");
            SQL.AppendLine("         MovimientoBancario.GeneraImpuestoBancario,");
            SQL.AppendLine("         MovimientoBancario.NroMovimientoRelacionado,");
            SQL.AppendLine("         MovimientoBancario.GeneradoPor,");
            SQL.AppendLine("         Gv_CuentaBancaria_B1.NombreDeLaMoneda AS Moneda,");
            SQL.AppendLine("         MovimientoBancario.CambioABolivares,");
            SQL.AppendLine("         MovimientoBancario.ImprimirCheque,");
            SQL.AppendLine("         MovimientoBancario.ConciliadoSN,");
            SQL.AppendLine("         MovimientoBancario.NroConciliacion,");
            SQL.AppendLine("         MovimientoBancario.GenerarAsientoDeRetiroEnCuenta,");
            SQL.AppendLine("         MovimientoBancario.NombreOperador,");
            SQL.AppendLine("         MovimientoBancario.FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(MovimientoBancario.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         MovimientoBancario.fldTimeStamp");
            SQL.AppendLine("      FROM " + "dbo" + ".MovimientoBancario");
            SQL.AppendLine("             INNER JOIN dbo.Gv_CuentaBancaria_B1 ON " + "dbo" + ".MovimientoBancario.CodigoCtaBancaria = dbo.Gv_CuentaBancaria_B1.Codigo");
            SQL.AppendLine("             INNER JOIN Adm.Gv_ConceptoBancario_B1 ON " + "dbo" + ".MovimientoBancario.CodigoConcepto = Adm.Gv_ConceptoBancario_B1.Codigo");
            SQL.AppendLine("      WHERE MovimientoBancario.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND MovimientoBancario.ConsecutivoMovimiento = @ConsecutivoMovimiento");
            SQL.AppendLine("         AND MovimientoBancario.CodigoCtaBancaria = @CodigoCtaBancaria");
            SQL.AppendLine("         AND MovimientoBancario.CodigoConcepto = @CodigoConcepto");
            SQL.AppendLine("         AND MovimientoBancario.Fecha = @Fecha");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_MovimientoBancario_B1.ConsecutivoMovimiento,");
            SQL.AppendLine("      " + DbSchema + ".Gv_MovimientoBancario_B1.CodigoCtaBancaria,");
            SQL.AppendLine("      " + DbSchema + ".Gv_MovimientoBancario_B1.CodigoConcepto,");
            SQL.AppendLine("      " + DbSchema + ".Gv_MovimientoBancario_B1.Fecha,");
            SQL.AppendLine("      " + DbSchema + ".Gv_MovimientoBancario_B1.TipoConceptoStr,");
            SQL.AppendLine("      " + DbSchema + ".Gv_MovimientoBancario_B1.Monto,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_MovimientoBancario_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_MovimientoBancario_B1.TipoConcepto,");
            SQL.AppendLine("      " + DbSchema + ".Gv_MovimientoBancario_B1.NumeroDocumento");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_MovimientoBancario_B1");
            SQL.AppendLine("      INNER JOIN dbo.Gv_CuentaBancaria_B1 ON  " + DbSchema + ".Gv_MovimientoBancario_B1.CodigoCtaBancaria = dbo.Gv_CuentaBancaria_B1.Codigo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_MovimientoBancario_B1.ConsecutivoCompania = dbo.Gv_CuentaBancaria_B1.ConsecutivoCompania");
            SQL.AppendLine("      INNER JOIN Adm.Gv_ConceptoBancario_B1 ON  " + DbSchema + ".Gv_MovimientoBancario_B1.CodigoConcepto = Adm.Gv_ConceptoBancario_B1.Codigo");
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
            SQL.AppendLine("      " + "dbo" + ".MovimientoBancario.ConsecutivoCompania,");
            SQL.AppendLine("      " + "dbo" + ".MovimientoBancario.ConsecutivoMovimiento,");
            SQL.AppendLine("      " + "dbo" + ".MovimientoBancario.CodigoCtaBancaria,");
            SQL.AppendLine("      " + "dbo" + ".MovimientoBancario.CodigoConcepto,");
            SQL.AppendLine("      " + "dbo" + ".MovimientoBancario.Fecha,");
            SQL.AppendLine("      " + "dbo" + ".MovimientoBancario.TipoConcepto,");
            SQL.AppendLine("      " + "dbo" + ".MovimientoBancario.NumeroDocumento");
            SQL.AppendLine("      FROM " + "dbo" + ".MovimientoBancario");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create("dbo" + ".MovimientoBancario", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumIngresoEgreso", LibTpvCreator.SqlViewStandardEnum(typeof(eIngresoEgreso), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumGeneradoPor", LibTpvCreator.SqlViewStandardEnum(typeof(eGeneradoPor), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_MovimientoBancario_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_MovimientoBancarioINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_MovimientoBancarioUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_MovimientoBancarioDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_MovimientoBancarioGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_MovimientoBancarioSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_MovimientoBancarioGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
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
            if (insDbo.Exists("dbo" + ".MovimientoBancario", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_MovimientoBancarioINS");
            vResult = insSp.Drop(DbSchema + ".Gp_MovimientoBancarioUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_MovimientoBancarioDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_MovimientoBancarioGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_MovimientoBancarioGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_MovimientoBancarioSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_MovimientoBancario_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumIngresoEgreso") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumGeneradoPor") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsMovimientoBancarioED

} //End of namespace Galac.Adm.Dal.Banco

