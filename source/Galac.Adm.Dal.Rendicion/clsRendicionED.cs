using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Dal.CajaChica {
    [LibMefDalComponentMetadata(typeof(clsRendicionED))]
    public class clsRendicionED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsRendicionED(): base(){
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
            get { return "Rendicion"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("Rendicion", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnRenConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnRenConsecutiv NOT NULL, ");
            SQL.AppendLine("Numero" + InsSql.VarCharTypeForDb(15) + " CONSTRAINT nnRenNumero NOT NULL, ");
            SQL.AppendLine("TipoDeDocumento" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_RenTiDeDo DEFAULT ('0'), ");
            SQL.AppendLine("ConsecutivoBeneficiario" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnRenConsecutiv NOT NULL, ");
            SQL.AppendLine("FechaApertura" + InsSql.DateTypeForDb() + " CONSTRAINT d_RenFeAp DEFAULT (''), ");
            SQL.AppendLine("FechaCierre" + InsSql.DateTypeForDb() + " CONSTRAINT d_RenFeCi DEFAULT (''), ");
            SQL.AppendLine("FechaAnulacion" + InsSql.DateTypeForDb() + " CONSTRAINT d_RenFeAn DEFAULT (''), ");
            SQL.AppendLine("StatusRendicion" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_RenStRe DEFAULT ('0'), ");
            SQL.AppendLine("TotalAdelantos" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_RenToAd DEFAULT (0), ");
            SQL.AppendLine("TotalGastos" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_RenToGa DEFAULT (0), ");
            SQL.AppendLine("TotalIVA" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_RenToIV DEFAULT (0), ");
            SQL.AppendLine("CodigoCuentaBancaria" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT d_RenCoCuBa DEFAULT (''), ");
			SQL.AppendLine("GeneraImpuestoBancario" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnRenGeneraImpu NOT NULL, ");
            SQL.AppendLine("CodigoConceptoBancario" + InsSql.VarCharTypeForDb(8) + " CONSTRAINT d_RenCoCoBa DEFAULT (''), ");
            SQL.AppendLine("NumeroDocumento" + InsSql.VarCharTypeForDb(15) + " CONSTRAINT d_RenNuDo DEFAULT (''), ");
            SQL.AppendLine("BeneficiarioCheque" + InsSql.VarCharTypeForDb(60) + " CONSTRAINT d_RenBeCh DEFAULT (''), ");
            SQL.AppendLine("CodigoCtaBancariaCajaChica" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT d_RenCoCtBaCaCh DEFAULT (''), ");
            SQL.AppendLine("Descripcion" + InsSql.VarCharTypeForDb(50) + " CONSTRAINT d_RenDe DEFAULT (''), ");
            SQL.AppendLine("Observaciones" + InsSql.VarCharTypeForDb(200) + " CONSTRAINT d_RenOb DEFAULT (''), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_Rendicion PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Consecutivo ASC)");
            SQL.AppendLine(", CONSTRAINT fk_RendicionBeneficiario FOREIGN KEY (ConsecutivoCompania, ConsecutivoBeneficiario)");
            SQL.AppendLine("REFERENCES Saw.Beneficiario(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(",CONSTRAINT u_NumConsec UNIQUE NONCLUSTERED (ConsecutivoCompania ASC, Numero ASC)");
            
            
            //SQL.AppendLine(", CONSTRAINT fk_RendicionCuentaBancaria FOREIGN KEY (ConsecutivoCompania, CodigoCuentaBancaria)");
            //SQL.AppendLine("REFERENCES Saw.CuentaBancaria(ConsecutivoCompania, Codigo)");
            //SQL.AppendLine("ON UPDATE CASCADE");
            //SQL.AppendLine(", CONSTRAINT fk_RendicionCuentaBancaria FOREIGN KEY (ConsecutivoCompania, CodigoCtaBancariaCajaChica)");
            //SQL.AppendLine("REFERENCES dbo.CuentaBancaria(ConsecutivoCompania, Codigo)");
            //SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT Rendicion.ConsecutivoCompania, Rendicion.Consecutivo, Rendicion.Numero, Rendicion.TipoDeDocumento, " + DbSchema + ".Gv_EnumTipoDeDocumentoRendicion.StrValue AS TipoDeDocumentoStr");
            SQL.AppendLine(", Rendicion.ConsecutivoBeneficiario, Rendicion.FechaApertura, Rendicion.FechaCierre, Rendicion.StatusRendicion, " + DbSchema + ".Gv_EnumStatusRendicion.StrValue AS StatusRendicionStr");
            SQL.AppendLine(", Rendicion.TotalAdelantos, Rendicion.TotalGastos, Rendicion.CodigoCuentaBancaria, Rendicion.CodigoConceptoBancario");
            SQL.AppendLine(", Rendicion.NumeroDocumento, Rendicion.BeneficiarioCheque, Rendicion.CodigoCtaBancariaCajaChica, Rendicion.Descripcion, Rendicion.Observaciones");
            SQL.AppendLine(", Rendicion.NombreOperador, Rendicion.FechaUltimaModificacion");
            SQL.AppendLine(",  CrossNombreCuentaBancaria.NombreCuenta AS NombreCuentaBancaria");
            SQL.AppendLine(", CrossNombreCuentaBancariaCajaChica.NombreCuenta AS NombreCuentaBancariaCajaChica");
			SQL.AppendLine(", Saw.Beneficiario.NombreBeneficiario AS NombreBeneficiario");
            SQL.AppendLine(", Saw.Beneficiario.Codigo AS CodigoBeneficiario");
            SQL.AppendLine(", Rendicion.fldTimeStamp, CAST(Rendicion.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".Rendicion");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoDeDocumentoRendicion");
            SQL.AppendLine("ON " + DbSchema + ".Rendicion.TipoDeDocumento COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDeDocumentoRendicion.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumStatusRendicion");
            SQL.AppendLine("ON " + DbSchema + ".Rendicion.StatusRendicion COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumStatusRendicion.DbValue");
            SQL.AppendLine("INNER JOIN Saw.Beneficiario ON  " + DbSchema + ".Rendicion.ConsecutivoBeneficiario = Saw.Beneficiario.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Rendicion.ConsecutivoCompania = Saw.Beneficiario.ConsecutivoCompania");
           // SQL.AppendLine("  LEFT OUTER JOIN Saw.Gv_CuentaBancaria_B1 ON (" + DbSchema +  ".Rendicion.CodigoCtaBancariaCajaChica = Saw.Gv_CuentaBancaria_B1.Codigo ");
           // SQL.AppendLine("      AND " + DbSchema + ".Rendicion.ConsecutivoCompania = Saw.Gv_CuentaBancaria_B1.ConsecutivoCompania) ");
            SQL.AppendLine("OUTER APPLY (SELECT NombreCuenta FROM Saw.CuentaBancaria WHERE " + DbSchema +  ".Rendicion.CodigoCuentaBancaria = Saw.CuentaBancaria.Codigo ");
            SQL.AppendLine("      AND " + DbSchema + ".Rendicion.ConsecutivoCompania = Saw.CuentaBancaria.ConsecutivoCompania) AS CrossNombreCuentaBancaria(NombreCuenta) ");
            SQL.AppendLine("OUTER APPLY (SELECT NombreCuenta FROM Saw.CuentaBancaria WHERE " + DbSchema + ".Rendicion.CodigoCtaBancariaCajaChica = Saw.CuentaBancaria.Codigo ");
            SQL.AppendLine("      AND " + DbSchema + ".Rendicion.ConsecutivoCompania = Saw.CuentaBancaria.ConsecutivoCompania) AS CrossNombreCuentaBancariaCajaChica(NombreCuenta) ");
            
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Numero" + InsSql.VarCharTypeForDb(15) + " = '',");
            SQL.AppendLine("@TipoDeDocumento" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@ConsecutivoBeneficiario" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@FechaApertura" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@FechaCierre" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@FechaAnulacion" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@StatusRendicion" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@TotalAdelantos" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TotalGastos" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TotalIVA" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@CodigoCuentaBancaria" + InsSql.VarCharTypeForDb(5) + ",");
			SQL.AppendLine("@GeneraImpuestoBancario" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@CodigoConceptoBancario" + InsSql.VarCharTypeForDb(8) + " = '',");
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(15) + " = '',");
            SQL.AppendLine("@BeneficiarioCheque" + InsSql.VarCharTypeForDb(30) + " = '',");
            SQL.AppendLine("@CodigoCtaBancariaCajaChica" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(50) + " = '',");
            SQL.AppendLine("@Observaciones" + InsSql.VarCharTypeForDb(200) + " = '',");
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
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".Rendicion(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            Numero,");
            SQL.AppendLine("            TipoDeDocumento,");
            SQL.AppendLine("            ConsecutivoBeneficiario,");
            SQL.AppendLine("            FechaApertura,");
            SQL.AppendLine("            FechaCierre,");
            SQL.AppendLine("            FechaAnulacion,");
            SQL.AppendLine("            StatusRendicion,");
            SQL.AppendLine("            TotalAdelantos,");
            SQL.AppendLine("            TotalGastos,");
            SQL.AppendLine("            TotalIVA,");
            SQL.AppendLine("            CodigoCuentaBancaria,");
			SQL.AppendLine("            GeneraImpuestoBancario,");
            SQL.AppendLine("            CodigoConceptoBancario,");
            SQL.AppendLine("            NumeroDocumento,");
            SQL.AppendLine("            BeneficiarioCheque,");
            SQL.AppendLine("            CodigoCtaBancariaCajaChica,");
            SQL.AppendLine("            Descripcion,");
            SQL.AppendLine("            Observaciones,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @Numero,");
            SQL.AppendLine("            @TipoDeDocumento,");
            SQL.AppendLine("            @ConsecutivoBeneficiario,");
            SQL.AppendLine("            @FechaApertura,");
            SQL.AppendLine("            @FechaCierre,");
            SQL.AppendLine("            @FechaAnulacion,");
            SQL.AppendLine("            @StatusRendicion,");
            SQL.AppendLine("            @TotalAdelantos,");
            SQL.AppendLine("            @TotalGastos,");
            SQL.AppendLine("            @TotalIVA,");
            SQL.AppendLine("            @CodigoCuentaBancaria,");
			SQL.AppendLine("            @GeneraImpuestoBancario,");
            SQL.AppendLine("            @CodigoConceptoBancario,");
            SQL.AppendLine("            @NumeroDocumento,");
            SQL.AppendLine("            @BeneficiarioCheque,");
            SQL.AppendLine("            @CodigoCtaBancariaCajaChica,");
            SQL.AppendLine("            @Descripcion,");
            SQL.AppendLine("            @Observaciones,");
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
            SQL.AppendLine("@Numero" + InsSql.VarCharTypeForDb(15) + ",");
            SQL.AppendLine("@TipoDeDocumento" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ConsecutivoBeneficiario" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@FechaApertura" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@FechaCierre" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@FechaAnulacion" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@StatusRendicion" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@TotalAdelantos" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TotalGastos" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TotalIVA" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@CodigoCuentaBancaria" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@GeneraImpuestoBancario" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@CodigoConceptoBancario" + InsSql.VarCharTypeForDb(8) + ",");
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(15) + ",");
            SQL.AppendLine("@BeneficiarioCheque" + InsSql.VarCharTypeForDb(60) + ",");
            SQL.AppendLine("@CodigoCtaBancariaCajaChica" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("@Observaciones" + InsSql.VarCharTypeForDb(200) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Rendicion WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Rendicion WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_RendicionCanBeUpdated @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".Rendicion");
            SQL.AppendLine("            SET Numero = @Numero,");
            SQL.AppendLine("               TipoDeDocumento = @TipoDeDocumento,");
            SQL.AppendLine("               ConsecutivoBeneficiario = @ConsecutivoBeneficiario,");
            SQL.AppendLine("               FechaApertura = @FechaApertura,");
            SQL.AppendLine("               FechaCierre = @FechaCierre,");
            SQL.AppendLine("               FechaAnulacion = @FechaAnulacion,");
            SQL.AppendLine("               StatusRendicion = @StatusRendicion,");
            SQL.AppendLine("               TotalAdelantos = @TotalAdelantos,");
            SQL.AppendLine("               TotalGastos = @TotalGastos,");
            SQL.AppendLine("               TotalIVA = @TotalIVA,");
            SQL.AppendLine("               CodigoCuentaBancaria = @CodigoCuentaBancaria,");
            SQL.AppendLine("               GeneraImpuestoBancario = @GeneraImpuestoBancario,");
            SQL.AppendLine("               CodigoConceptoBancario = @CodigoConceptoBancario,");
            SQL.AppendLine("               NumeroDocumento = @NumeroDocumento,");
            SQL.AppendLine("               BeneficiarioCheque = @BeneficiarioCheque,");
            SQL.AppendLine("               CodigoCtaBancariaCajaChica = @CodigoCtaBancariaCajaChica,");
            SQL.AppendLine("               Descripcion = @Descripcion,");
            SQL.AppendLine("               Observaciones = @Observaciones,");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Rendicion WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Rendicion WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_RendicionCanBeDeleted @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".Rendicion");
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
        //################AQUI   NO ESTA TRAYENDO INFORMACION DE CUENTA BANCARIA

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         Rendicion.ConsecutivoCompania,");
            SQL.AppendLine("         Rendicion.Consecutivo,");
            SQL.AppendLine("         Rendicion.Numero,");
            SQL.AppendLine("         Rendicion.TipoDeDocumento,");
            SQL.AppendLine("         Rendicion.ConsecutivoBeneficiario,");
            SQL.AppendLine("         Gv_Beneficiario_B1.NombreBeneficiario AS NombreBeneficiario,");
            SQL.AppendLine("         Gv_Beneficiario_B1.Codigo AS CodigoBeneficiario,");
            SQL.AppendLine("         Rendicion.FechaApertura,");
            SQL.AppendLine("         Rendicion.FechaCierre,");
            SQL.AppendLine("         Rendicion.FechaAnulacion,");
            SQL.AppendLine("         Rendicion.StatusRendicion,");
            SQL.AppendLine("         Rendicion.TotalAdelantos,");
            SQL.AppendLine("         Rendicion.TotalGastos,");
            SQL.AppendLine("         Rendicion.TotalIVA,");
            SQL.AppendLine("         Rendicion.CodigoCuentaBancaria,");
            SQL.AppendLine("         CrossNombreCuentaBancaria.NombreCuenta AS NombreCuentaBancaria,");
            SQL.AppendLine("         Rendicion.GeneraImpuestoBancario,");
            SQL.AppendLine("         Rendicion.CodigoConceptoBancario,");
            SQL.AppendLine("         Adm.ConceptoBancario.descripcion AS NombreConceptoBancario,");
            SQL.AppendLine("         Rendicion.NumeroDocumento,");
            SQL.AppendLine("         Rendicion.BeneficiarioCheque,");
            SQL.AppendLine("         Rendicion.CodigoCtaBancariaCajaChica,");
            SQL.AppendLine("         CrossNombreCuentaBancariaCajaChica.NombreCuenta AS NombreCuentaBancariaCajaChica,");
            SQL.AppendLine("         Rendicion.Descripcion,");
            SQL.AppendLine("         Rendicion.Observaciones,");
            SQL.AppendLine("         Rendicion.NombreOperador,");
            SQL.AppendLine("         Rendicion.FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(Rendicion.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         Rendicion.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".Rendicion");
            SQL.AppendLine("             INNER JOIN Saw.Gv_Beneficiario_B1 ON " + DbSchema + ".Rendicion.ConsecutivoBeneficiario = Saw.Gv_Beneficiario_B1.Consecutivo");
            //SQL.AppendLine("             LEFT OUTER JOIN Saw.Gv_CuentaBancaria_B1 ON (" + DbSchema + ".Rendicion.CodigoCuentaBancaria = Saw.Gv_CuentaBancaria_B1.Codigo OR " + DbSchema + ".Rendicion.CodigoCtaBancariaCajaChica = Saw.Gv_CuentaBancaria_B1.Codigo ");
            //SQL.AppendLine("      AND " + DbSchema + ".Rendicion.ConsecutivoCompania = Saw.Gv_CuentaBancaria_B1.ConsecutivoCompania) ");
            //SQL.AppendLine("             INNER JOIN Saw.Gv_CuentaBancaria_B1 ON " + DbSchema + ".Rendicion.CodigoCtaBancariaCajaChica = Saw.Gv_CuentaBancaria_B1.Codigo");
            SQL.AppendLine("             LEFT OUTER JOIN  Adm.ConceptoBancario ON Adm.ConceptoBancario.Codigo  =  Rendicion.CodigoConceptoBancario");
            SQL.AppendLine("OUTER APPLY (SELECT NombreCuenta FROM Saw.Gv_CuentaBancaria_B1 WHERE " + DbSchema + ".Rendicion.CodigoCuentaBancaria = Saw.Gv_CuentaBancaria_B1.Codigo ");
            SQL.AppendLine("      AND " + DbSchema + ".Rendicion.ConsecutivoCompania = Saw.Gv_CuentaBancaria_B1.ConsecutivoCompania) AS CrossNombreCuentaBancaria(NombreCuenta) ");
            SQL.AppendLine("OUTER APPLY (SELECT NombreCuenta FROM Saw.Gv_CuentaBancaria_B1 WHERE " + DbSchema + ".Rendicion.CodigoCtaBancariaCajaChica = Saw.Gv_CuentaBancaria_B1.Codigo ");
            SQL.AppendLine("      AND " + DbSchema + ".Rendicion.ConsecutivoCompania = Saw.Gv_CuentaBancaria_B1.ConsecutivoCompania) AS CrossNombreCuentaBancariaCajaChica(NombreCuenta) ");
            SQL.AppendLine("      WHERE Rendicion.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND Rendicion.Consecutivo = @Consecutivo");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_Rendicion_B1.Numero,");
            SQL.AppendLine("      " + "Saw" + ".Gv_Beneficiario_B1.NombreBeneficiario AS NombreBeneficiario,");
            SQL.AppendLine("      " + "Saw" + ".Gv_Beneficiario_B1.Codigo AS CodigoBeneficiario,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Rendicion_B1.FechaApertura,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Rendicion_B1.StatusRendicionStr,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Rendicion_B1.CodigoCtaBancariaCajaChica,");
            SQL.AppendLine("      " +" CrossNombreCuentaBancariaCajaChica.NombreCuenta AS NombreCuentaBancariaCajaChica,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Rendicion_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Rendicion_B1.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Rendicion_B1.StatusRendicion");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Rendicion_B1");
            SQL.AppendLine("      INNER JOIN Saw.Gv_Beneficiario_B1 ON  " + DbSchema + ".Gv_Rendicion_B1.ConsecutivoBeneficiario = Saw.Gv_Beneficiario_B1.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Rendicion_B1.ConsecutivoCompania = Saw.Gv_Beneficiario_B1.ConsecutivoCompania");
            SQL.AppendLine("OUTER APPLY (SELECT NombreCuenta FROM Saw.Gv_CuentaBancaria_B1 WHERE " + DbSchema + ".Gv_Rendicion_B1.CodigoCuentaBancaria = Saw.Gv_CuentaBancaria_B1.Codigo ");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Rendicion_B1.ConsecutivoCompania = Saw.Gv_CuentaBancaria_B1.ConsecutivoCompania) AS CrossNombreCuentaBancaria(NombreCuenta) ");
            SQL.AppendLine("OUTER APPLY (SELECT NombreCuenta FROM Saw.Gv_CuentaBancaria_B1 WHERE " + DbSchema + ".Gv_Rendicion_B1.CodigoCtaBancariaCajaChica = Saw.Gv_CuentaBancaria_B1.Codigo ");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Rendicion_B1.ConsecutivoCompania = Saw.Gv_CuentaBancaria_B1.ConsecutivoCompania) AS CrossNombreCuentaBancariaCajaChica(NombreCuenta) ");
            
            //SQL.AppendLine("      LEFT OUTER JOIN Saw.Gv_CuentaBancaria_B1 ON (" + DbSchema + ".Gv_Rendicion_B1.CodigoCtaBancariaCajaChica = Saw.Gv_CuentaBancaria_B1.Codigo ");
            //SQL.AppendLine("      AND " + DbSchema + ".Gv_Rendicion_B1.ConsecutivoCompania = Saw.Gv_CuentaBancaria_B1.ConsecutivoCompania) ");
            //SQL.AppendLine("      INNER JOIN Saw.Gv_CuentaBancaria_B1 ON  " + DbSchema + ".Gv_Rendicion_B1.CodigoCuentaBancaria = Saw.Gv_CuentaBancaria_B1.Codigo");
            //SQL.AppendLine("      AND " + DbSchema + ".Gv_Rendicion_B1.ConsecutivoCompania = Saw.Gv_CuentaBancaria_B1.ConsecutivoCompania");
            //SQL.AppendLine("      INNER JOIN Saw.Gv_CuentaBancaria_B1 ON  " + DbSchema + ".Gv_Rendicion_B1.CodigoCtaBancariaCajaChica = Saw.Gv_CuentaBancaria_B1.Codigo");
            //SQL.AppendLine("      AND " + DbSchema + ".Gv_Rendicion_B1.ConsecutivoCompania = Saw.Gv_CuentaBancaria_B1.ConsecutivoCompania");
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
            SQL.AppendLine("      " + DbSchema + ".Rendicion.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Rendicion.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Rendicion.ConsecutivoBeneficiario,");
            SQL.AppendLine("      " + DbSchema + ".Rendicion.FechaApertura,");
            SQL.AppendLine("      " + DbSchema + ".Rendicion.StatusRendicion,");
            SQL.AppendLine("      " + DbSchema + ".Rendicion.CodigoCuentaBancaria,");
            SQL.AppendLine("      " + DbSchema + ".Rendicion.CodigoCtaBancariaCajaChica");
            //SQL.AppendLine("      ," + DbSchema + ".Rendicion.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".Rendicion");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
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
            SQL.AppendLine("       " + DbSchema + ".Gv_Rendicion_B1.Numero,");
            SQL.AppendLine("       " + DbSchema + ".Gv_Rendicion_B1.FechaApertura,");
            SQL.AppendLine("       " + DbSchema + ".Gv_Rendicion_B1.StatusRendicionStr,");
            SQL.AppendLine("       " + DbSchema + ".Gv_Rendicion_B1.CodigoCtaBancariaCajaChica,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("       " + DbSchema + ".Gv_Rendicion_B1.ConsecutivoCompania,");
            SQL.AppendLine("       " + DbSchema + ".Gv_Rendicion_B1.Consecutivo,");
            SQL.AppendLine("       " + DbSchema + ".Gv_Rendicion_B1.StatusRendicion");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Rendicion_B1 " );

            SQL.AppendLine("      LEFT OUTER JOIN ( select dbo.Comprobante.NoDocumentoOrigen,dbo.COMPROBANTE.GeneradoPor,dbo.COMPROBANTE.ConsecutivoDocOrigen, ");
            SQL.AppendLine("      dbo.Periodo.ConsecutivoCompania,dbo.Periodo.FechaAperturaDelPeriodo,dbo.Periodo.FechaCierreDelPeriodo  from   dbo.COMPROBANTE");
            SQL.AppendLine("      INNER JOIN dbo.PERIODO ON  dbo.PERIODO.ConsecutivoPeriodo  = dbo.COMPROBANTE.ConsecutivoPeriodo");
            SQL.AppendLine("      AND dbo.PERIODO.ConsecutivoPeriodo  = dbo.COMPROBANTE.ConsecutivoPeriodo) ComprobantePeriodo");

            SQL.AppendLine("      ON  " + DbSchema + ".Gv_Rendicion_B1.Consecutivo  = ComprobantePeriodo.ConsecutivoDocOrigen");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Rendicion_B1.Numero = ComprobantePeriodo.NoDocumentoOrigen AND ComprobantePeriodo.GeneradoPor = ' + QUOTENAME('M','''') + '");
            
            SQL.AppendLine("      AND  ComprobantePeriodo.ConsecutivoCompania =     " + DbSchema + ".Gv_Rendicion_B1.ConsecutivoCompania");
            SQL.AppendLine("      AND  ComprobantePeriodo.FechaAperturaDelPeriodo < " + DbSchema + ".Gv_Rendicion_B1.FechaCierre");
            SQL.AppendLine("      AND  ComprobantePeriodo.FechaCierreDelPeriodo   > " + DbSchema + ".Gv_Rendicion_B1.FechaCierre");
            
 
 
    
    

            SQL.AppendLine("'   IF (NOT @SQLWhere IS NULL) AND (@SQLWhere <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' WHERE ' + @SQLWhere + ' AND ComprobantePeriodo.ConsecutivoDocOrigen is null '  ");

            SQL.AppendLine("   IF (NOT @SQLOrderBy IS NULL) AND (@SQLOrderBy <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' ORDER BY ' + @SQLOrderBy");
            SQL.AppendLine("   EXEC(@strSQL)");
            SQL.AppendLine("END");
            return SQL.ToString();
        }


             //dbo.COMPROBANTE ON  " + DbSchema + ".Gv_Rendicion_B1.Consecutivo  = dbo.COMPROBANTE.ConsecutivoDocOrigen

        #endregion //Queries
        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".Rendicion", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }
        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDeDocumentoRendicion", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDeDocumentoRendicion), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumStatusRendicion", LibTpvCreator.SqlViewStandardEnum(typeof(eStatusRendicion), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_Rendicion_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }
        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RendicionINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RendicionUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RendicionDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RendicionGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RendicionSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RendicionGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_RendicionContabSCH", SqlSpContabSchParameters(), SqlSpContabSch(), true) && vResult;
            
            insSps.Dispose();
            return vResult;
        }
        public bool InstalarTabla() {
            bool vResult = false;
            if (CrearTabla()) {
                CrearVistas();
                CrearProcedimientos();
                clsDetalleDeRendicionED insDetailDetDeRen = new clsDetalleDeRendicionED();
                vResult = insDetailDetDeRen.InstalarTabla();
            }
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = false;
            if (insDbo.Exists(DbSchema + ".Rendicion", eDboType.Tabla)) {
                CrearVistas();
                CrearProcedimientos();
                vResult = new clsDetalleDeRendicionED().InstalarVistasYSps();
            }
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = false;
            LibStoredProc insSp = new LibStoredProc();
            LibViews insVista = new LibViews();
            vResult = new clsDetalleDeRendicionED().BorrarVistasYSps();
            vResult = insSp.Drop(DbSchema + ".Gp_RendicionINS") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RendicionUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RendicionDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RendicionGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RendicionGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_RendicionSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_Rendicion_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDeDocumentoRendicion") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumStatusRendicion") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsRendicionesED

} //End of namespace Galac.Saw.Dal.Rendicion

