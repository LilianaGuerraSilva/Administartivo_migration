using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Dal.Venta {
    [LibMefDalComponentMetadata(typeof(clsCobranzaED))]
    public class clsCobranzaED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsCobranzaED(): base(){
            DbSchema = "Dbo";
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
            get { return "Cobranza"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("Cobranza", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnCobConsecutiv NOT NULL, ");
            SQL.AppendLine("Numero" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT nnCobNumero NOT NULL, ");
            SQL.AppendLine("StatusCobranza" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCobStatusCobr NOT NULL, ");
            SQL.AppendLine("Fecha" + InsSql.DateTypeForDb() + " CONSTRAINT nnCobFecha NOT NULL, ");
            SQL.AppendLine("FechaAnulacion" + InsSql.DateTypeForDb() + " CONSTRAINT d_CobFeAn DEFAULT (''), ");
            SQL.AppendLine("CodigoCliente" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT d_CobCoCl DEFAULT (''), ");
            SQL.AppendLine("ConsecutivoCobrador" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT d_CobCoCo DEFAULT (0), ");
            SQL.AppendLine("TotalDocumentos" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobToDo DEFAULT (0), ");
            SQL.AppendLine("RetencionISLR" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobReIS DEFAULT (0), ");
            SQL.AppendLine("TotalCobrado" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobToCo DEFAULT (0), ");
            SQL.AppendLine("CobradoEfectivo" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobCoEf DEFAULT (0), ");
            SQL.AppendLine("CobradoCheque" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobCoCh DEFAULT (0), ");
            SQL.AppendLine("NumerodelCheque" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT d_CobNuCh DEFAULT (''), ");
            SQL.AppendLine("CobradoTarjeta" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobCoTa DEFAULT (0), ");
            SQL.AppendLine("CualTarjeta" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_CobCuTa DEFAULT ('0'), ");
            SQL.AppendLine("NroDeLaTarjeta" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_CobNrDeLaTa DEFAULT (''), ");
            SQL.AppendLine("Origen" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_CobOr DEFAULT ('0'), ");
            SQL.AppendLine("TotalOtros" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobToOt DEFAULT (0), ");
            SQL.AppendLine("NombreBanco" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_CobNoBa DEFAULT (''), ");
            SQL.AppendLine("CodigoCuentaBancaria" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT d_CobCoCuBa DEFAULT (''), ");
            SQL.AppendLine("CodigoConcepto" + InsSql.VarCharTypeForDb(8) + " CONSTRAINT d_CobCoCo DEFAULT (''), ");
            SQL.AppendLine("Moneda" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT d_CobMo DEFAULT (''), ");
            SQL.AppendLine("CambioABolivares" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobCaAB DEFAULT (0), ");
            SQL.AppendLine("RetencionIVA" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobReIV DEFAULT (0), ");
            SQL.AppendLine("NroComprobanteRetIVA" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_CobNrCoReIV DEFAULT (''), ");
            SQL.AppendLine("StatusRetencionIVA" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_CobStReIV DEFAULT ('0'), ");
            SQL.AppendLine("GeneraMovBancario" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCobGeneraMovB NOT NULL, ");
            SQL.AppendLine("CobradoAnticipo" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobCoAn DEFAULT (0), ");
            SQL.AppendLine("Vuelto" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobVu DEFAULT (0), ");
            SQL.AppendLine("DescProntoPago" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobDePrPa DEFAULT (0), ");
            SQL.AppendLine("DescProntoPagoPorc" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobDePrPaPo DEFAULT (0), ");
            SQL.AppendLine("ComisionVendedor" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CobCoVe DEFAULT (0), ");
            SQL.AppendLine("AplicaCreditoBancario" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCobAplicaCred NOT NULL, ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_Cobranza PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Numero ASC)");
            SQL.AppendLine(", CONSTRAINT fk_CobranzaCliente FOREIGN KEY (ConsecutivoCompania, CodigoCliente)");
            SQL.AppendLine("REFERENCES Saw.Cliente(ConsecutivoCompania, codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_CobranzaVende FOREIGN KEY (ConsecutivoCompania, ConsecutivoCobrador)");
            SQL.AppendLine("REFERENCES Adm.Vendedor(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON UPDATE NO ACTION");
            SQL.AppendLine(", CONSTRAINT fk_CobranzaMoneda FOREIGN KEY (Moneda)");
            SQL.AppendLine("REFERENCES Comun.Moneda(Nombre)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(",CONSTRAINT u_CobNumero UNIQUE NONCLUSTERED (ConsecutivoCompania, Numero)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT Cobranza.ConsecutivoCompania, Cobranza.Numero, Cobranza.StatusCobranza, " + DbSchema + ".Gv_EnumStatusCobranza.StrValue AS StatusCobranzaStr, Cobranza.Fecha");
            SQL.AppendLine(", Cobranza.FechaAnulacion, Cobranza.CodigoCliente, Cobranza.CodigoCobrador, Cobranza.TotalDocumentos");
            SQL.AppendLine(", Cobranza.RetencionISLR, Cobranza.TotalCobrado, Cobranza.CobradoEfectivo, Cobranza.CobradoCheque");
            SQL.AppendLine(", Cobranza.NumerodelCheque, Cobranza.CobradoTarjeta, Cobranza.CualTarjeta, " + DbSchema + ".Gv_EnumTarjeta.StrValue AS CualTarjetaStr, Cobranza.NroDeLaTarjeta");
            SQL.AppendLine(", Cobranza.Origen, " + DbSchema + ".Gv_EnumOrigenFacturacionOManual.StrValue AS OrigenStr, Cobranza.TotalOtros, Cobranza.NombreBanco, Cobranza.CodigoCuentaBancaria");
            SQL.AppendLine(", Cobranza.CodigoConcepto, Cobranza.Moneda, Cobranza.CambioABolivares, Cobranza.RetencionIVA");
            SQL.AppendLine(", Cobranza.NroComprobanteRetIVA, Cobranza.StatusRetencionIVA, " + DbSchema + ".Gv_EnumStatusRetencionIVACobranza.StrValue AS StatusRetencionIVAStr, Cobranza.GeneraMovBancario, Cobranza.CobradoAnticipo");
            SQL.AppendLine(", Cobranza.Vuelto, Cobranza.DescProntoPago, Cobranza.DescProntoPagoPorc, Cobranza.ComisionVendedor");
            SQL.AppendLine(", Cobranza.AplicaCreditoBancario, Cobranza.NombreOperador, Cobranza.FechaUltimaModificacion");
            SQL.AppendLine(", Saw.Cliente.nombre AS NombreCliente");
            SQL.AppendLine(", Cobranza.fldTimeStamp, CAST(Cobranza.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".Cobranza");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumStatusCobranza");
            SQL.AppendLine("ON " + DbSchema + ".Cobranza.StatusCobranza COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumStatusCobranza.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTarjeta");
            SQL.AppendLine("ON " + DbSchema + ".Cobranza.CualTarjeta COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTarjeta.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumOrigenFacturacionOManual");
            SQL.AppendLine("ON " + DbSchema + ".Cobranza.Origen COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumOrigenFacturacionOManual.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumStatusRetencionIVACobranza");
            SQL.AppendLine("ON " + DbSchema + ".Cobranza.StatusRetencionIVA COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumStatusRetencionIVACobranza.DbValue");
            SQL.AppendLine("INNER JOIN Saw.Cliente ON  " + DbSchema + ".Cobranza.CodigoCliente = Saw.Cliente.codigo");
            SQL.AppendLine("      AND " + DbSchema + ".Cobranza.ConsecutivoCompania = Saw.Cliente.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN dbo.Vendedor ON  " + DbSchema + ".Cobranza.CodigoCobrador = dbo.Vendedor.codigo");
            SQL.AppendLine("      AND " + DbSchema + ".Cobranza.ConsecutivoCompania = dbo.Vendedor.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN Comun.Moneda ON  " + DbSchema + ".Cobranza.Moneda = Comun.Moneda.Nombre");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Numero" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@StatusCobranza" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@FechaAnulacion" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@CodigoCliente" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@CodigoCobrador" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@TotalDocumentos" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@RetencionISLR" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TotalCobrado" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@CobradoEfectivo" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@CobradoCheque" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@NumerodelCheque" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@CobradoTarjeta" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@CualTarjeta" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@NroDeLaTarjeta" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@Origen" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@TotalOtros" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@NombreBanco" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@CodigoCuentaBancaria" + InsSql.VarCharTypeForDb(5) + " = '',");
            SQL.AppendLine("@CodigoConcepto" + InsSql.VarCharTypeForDb(8) + " = '',");
            SQL.AppendLine("@Moneda" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@CambioABolivares" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@RetencionIVA" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@NroComprobanteRetIVA" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@StatusRetencionIVA" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@GeneraMovBancario" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@CobradoAnticipo" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@Vuelto" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@DescProntoPago" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@DescProntoPagoPorc" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@ComisionVendedor" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@AplicaCreditoBancario" + InsSql.CharTypeForDb(1) + " = 'N',");
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
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".Cobranza(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Numero,");
            SQL.AppendLine("            StatusCobranza,");
            SQL.AppendLine("            Fecha,");
            SQL.AppendLine("            FechaAnulacion,");
            SQL.AppendLine("            CodigoCliente,");
            SQL.AppendLine("            CodigoCobrador,");
            SQL.AppendLine("            TotalDocumentos,");
            SQL.AppendLine("            RetencionISLR,");
            SQL.AppendLine("            TotalCobrado,");
            SQL.AppendLine("            CobradoEfectivo,");
            SQL.AppendLine("            CobradoCheque,");
            SQL.AppendLine("            NumerodelCheque,");
            SQL.AppendLine("            CobradoTarjeta,");
            SQL.AppendLine("            CualTarjeta,");
            SQL.AppendLine("            NroDeLaTarjeta,");
            SQL.AppendLine("            Origen,");
            SQL.AppendLine("            TotalOtros,");
            SQL.AppendLine("            NombreBanco,");
            SQL.AppendLine("            CodigoCuentaBancaria,");
            SQL.AppendLine("            CodigoConcepto,");
            SQL.AppendLine("            Moneda,");
            SQL.AppendLine("            CambioABolivares,");
            SQL.AppendLine("            RetencionIVA,");
            SQL.AppendLine("            NroComprobanteRetIVA,");
            SQL.AppendLine("            StatusRetencionIVA,");
            SQL.AppendLine("            GeneraMovBancario,");
            SQL.AppendLine("            CobradoAnticipo,");
            SQL.AppendLine("            Vuelto,");
            SQL.AppendLine("            DescProntoPago,");
            SQL.AppendLine("            DescProntoPagoPorc,");
            SQL.AppendLine("            ComisionVendedor,");
            SQL.AppendLine("            AplicaCreditoBancario,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Numero,");
            SQL.AppendLine("            @StatusCobranza,");
            SQL.AppendLine("            @Fecha,");
            SQL.AppendLine("            @FechaAnulacion,");
            SQL.AppendLine("            @CodigoCliente,");
            SQL.AppendLine("            @CodigoCobrador,");
            SQL.AppendLine("            @TotalDocumentos,");
            SQL.AppendLine("            @RetencionISLR,");
            SQL.AppendLine("            @TotalCobrado,");
            SQL.AppendLine("            @CobradoEfectivo,");
            SQL.AppendLine("            @CobradoCheque,");
            SQL.AppendLine("            @NumerodelCheque,");
            SQL.AppendLine("            @CobradoTarjeta,");
            SQL.AppendLine("            @CualTarjeta,");
            SQL.AppendLine("            @NroDeLaTarjeta,");
            SQL.AppendLine("            @Origen,");
            SQL.AppendLine("            @TotalOtros,");
            SQL.AppendLine("            @NombreBanco,");
            SQL.AppendLine("            @CodigoCuentaBancaria,");
            SQL.AppendLine("            @CodigoConcepto,");
            SQL.AppendLine("            @Moneda,");
            SQL.AppendLine("            @CambioABolivares,");
            SQL.AppendLine("            @RetencionIVA,");
            SQL.AppendLine("            @NroComprobanteRetIVA,");
            SQL.AppendLine("            @StatusRetencionIVA,");
            SQL.AppendLine("            @GeneraMovBancario,");
            SQL.AppendLine("            @CobradoAnticipo,");
            SQL.AppendLine("            @Vuelto,");
            SQL.AppendLine("            @DescProntoPago,");
            SQL.AppendLine("            @DescProntoPagoPorc,");
            SQL.AppendLine("            @ComisionVendedor,");
            SQL.AppendLine("            @AplicaCreditoBancario,");
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
            SQL.AppendLine("@Numero" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@StatusCobranza" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@FechaAnulacion" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@CodigoCliente" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@CodigoCobrador" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@TotalDocumentos" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@RetencionISLR" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TotalCobrado" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@CobradoEfectivo" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@CobradoCheque" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@NumerodelCheque" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@CobradoTarjeta" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@CualTarjeta" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@NroDeLaTarjeta" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@Origen" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@TotalOtros" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@NombreBanco" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@CodigoCuentaBancaria" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@CodigoConcepto" + InsSql.VarCharTypeForDb(8) + ",");
            SQL.AppendLine("@Moneda" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@CambioABolivares" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@RetencionIVA" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@NroComprobanteRetIVA" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@StatusRetencionIVA" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@GeneraMovBancario" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@CobradoAnticipo" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@Vuelto" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@DescProntoPago" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@DescProntoPagoPorc" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@ComisionVendedor" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@AplicaCreditoBancario" + InsSql.CharTypeForDb(1) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Cobranza WHERE ConsecutivoCompania = @ConsecutivoCompania AND Numero = @Numero)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Cobranza WHERE ConsecutivoCompania = @ConsecutivoCompania AND Numero = @Numero");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_CobranzaCanBeUpdated @ConsecutivoCompania,@Numero, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".Cobranza");
            SQL.AppendLine("            SET StatusCobranza = @StatusCobranza,");
            SQL.AppendLine("               Fecha = @Fecha,");
            SQL.AppendLine("               FechaAnulacion = @FechaAnulacion,");
            SQL.AppendLine("               CodigoCliente = @CodigoCliente,");
            SQL.AppendLine("               CodigoCobrador = @CodigoCobrador,");
            SQL.AppendLine("               TotalDocumentos = @TotalDocumentos,");
            SQL.AppendLine("               RetencionISLR = @RetencionISLR,");
            SQL.AppendLine("               TotalCobrado = @TotalCobrado,");
            SQL.AppendLine("               CobradoEfectivo = @CobradoEfectivo,");
            SQL.AppendLine("               CobradoCheque = @CobradoCheque,");
            SQL.AppendLine("               NumerodelCheque = @NumerodelCheque,");
            SQL.AppendLine("               CobradoTarjeta = @CobradoTarjeta,");
            SQL.AppendLine("               CualTarjeta = @CualTarjeta,");
            SQL.AppendLine("               NroDeLaTarjeta = @NroDeLaTarjeta,");
            SQL.AppendLine("               Origen = @Origen,");
            SQL.AppendLine("               TotalOtros = @TotalOtros,");
            SQL.AppendLine("               NombreBanco = @NombreBanco,");
            SQL.AppendLine("               CodigoCuentaBancaria = @CodigoCuentaBancaria,");
            SQL.AppendLine("               CodigoConcepto = @CodigoConcepto,");
            SQL.AppendLine("               Moneda = @Moneda,");
            SQL.AppendLine("               CambioABolivares = @CambioABolivares,");
            SQL.AppendLine("               RetencionIVA = @RetencionIVA,");
            SQL.AppendLine("               NroComprobanteRetIVA = @NroComprobanteRetIVA,");
            SQL.AppendLine("               StatusRetencionIVA = @StatusRetencionIVA,");
            SQL.AppendLine("               GeneraMovBancario = @GeneraMovBancario,");
            SQL.AppendLine("               CobradoAnticipo = @CobradoAnticipo,");
            SQL.AppendLine("               Vuelto = @Vuelto,");
            SQL.AppendLine("               DescProntoPago = @DescProntoPago,");
            SQL.AppendLine("               DescProntoPagoPorc = @DescProntoPagoPorc,");
            SQL.AppendLine("               ComisionVendedor = @ComisionVendedor,");
            SQL.AppendLine("               AplicaCreditoBancario = @AplicaCreditoBancario,");
            SQL.AppendLine("               NombreOperador = @NombreOperador,");
            SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND Numero = @Numero");
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
            SQL.AppendLine("@Numero" + InsSql.VarCharTypeForDb(10) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Cobranza WHERE ConsecutivoCompania = @ConsecutivoCompania AND Numero = @Numero)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Cobranza WHERE ConsecutivoCompania = @ConsecutivoCompania AND Numero = @Numero");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_CobranzaCanBeDeleted @ConsecutivoCompania,@Numero, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".Cobranza");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND Numero = @Numero");
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
            SQL.AppendLine("@Numero" + InsSql.VarCharTypeForDb(10));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         Cobranza.ConsecutivoCompania,");
            SQL.AppendLine("         Cobranza.Numero,");
            SQL.AppendLine("         Cobranza.StatusCobranza,");
            SQL.AppendLine("         Cobranza.Fecha,");
            SQL.AppendLine("         Cobranza.FechaAnulacion,");
            SQL.AppendLine("         Cobranza.CodigoCliente,");
            SQL.AppendLine("         Gv_Cliente_B1.nombre AS NombreCliente,");
            SQL.AppendLine("         Cobranza.CodigoCobrador,");
            SQL.AppendLine("         Gv_Vendedor_B1.nombre AS NombreCobrador,");
            SQL.AppendLine("         Cobranza.TotalDocumentos,");
            SQL.AppendLine("         Cobranza.RetencionISLR,");
            SQL.AppendLine("         Cobranza.TotalCobrado,");
            SQL.AppendLine("         Cobranza.CobradoEfectivo,");
            SQL.AppendLine("         Cobranza.CobradoCheque,");
            SQL.AppendLine("         Cobranza.NumerodelCheque,");
            SQL.AppendLine("         Cobranza.CobradoTarjeta,");
            SQL.AppendLine("         Cobranza.CualTarjeta,");
            SQL.AppendLine("         Cobranza.NroDeLaTarjeta,");
            SQL.AppendLine("         Cobranza.Origen,");
            SQL.AppendLine("         Cobranza.TotalOtros,");
            SQL.AppendLine("         Cobranza.NombreBanco,");
            SQL.AppendLine("         Cobranza.CodigoCuentaBancaria,");
            SQL.AppendLine("         Cobranza.CodigoConcepto,");
            SQL.AppendLine("         Cobranza.Moneda,");
            SQL.AppendLine("         Cobranza.CambioABolivares,");
            SQL.AppendLine("         Cobranza.RetencionIVA,");
            SQL.AppendLine("         Cobranza.NroComprobanteRetIVA,");
            SQL.AppendLine("         Cobranza.StatusRetencionIVA,");
            SQL.AppendLine("         Cobranza.GeneraMovBancario,");
            SQL.AppendLine("         Cobranza.CobradoAnticipo,");
            SQL.AppendLine("         Cobranza.Vuelto,");
            SQL.AppendLine("         Cobranza.DescProntoPago,");
            SQL.AppendLine("         Cobranza.DescProntoPagoPorc,");
            SQL.AppendLine("         Cobranza.ComisionVendedor,");
            SQL.AppendLine("         Cobranza.AplicaCreditoBancario,");
            SQL.AppendLine("         Cobranza.NombreOperador,");
            SQL.AppendLine("         Cobranza.FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(Cobranza.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         Cobranza.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".Cobranza");
            SQL.AppendLine("             INNER JOIN Saw.Gv_Cliente_B1 ON " + DbSchema + ".Cobranza.CodigoCliente = Saw.Gv_Cliente_B1.codigo");
            SQL.AppendLine("             INNER JOIN dbo.Gv_Vendedor_B1 ON " + DbSchema + ".Cobranza.CodigoCobrador = dbo.Gv_Vendedor_B1.codigo");
            SQL.AppendLine("             INNER JOIN Comun.Gv_Moneda_B1 ON " + DbSchema + ".Cobranza.Moneda = Comun.Gv_Moneda_B1.Nombre");
            SQL.AppendLine("      WHERE Cobranza.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND Cobranza.Numero = @Numero");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_Cobranza_B1.Numero,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cobranza_B1.StatusCobranzaStr,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cobranza_B1.Fecha,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cobranza_B1.CodigoCliente,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.nombre AS NombreCliente,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cobranza_B1.CodigoCobrador,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cobranza_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cobranza_B1.StatusCobranza");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Cobranza_B1");
            SQL.AppendLine("      INNER JOIN Saw.Gv_Cliente_B1 ON  " + DbSchema + ".Gv_Cobranza_B1.CodigoCliente = Saw.Gv_Cliente_B1.codigo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Cobranza_B1.ConsecutivoCompania = Saw.Gv_Cliente_B1.ConsecutivoCompania");
            SQL.AppendLine("      INNER JOIN dbo.Gv_Vendedor_B1 ON  " + DbSchema + ".Gv_Cobranza_B1.CodigoCobrador = dbo.Gv_Vendedor_B1.codigo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Cobranza_B1.ConsecutivoCompania = dbo.Gv_Vendedor_B1.ConsecutivoCompania");
            SQL.AppendLine("      INNER JOIN Comun.Gv_Moneda_B1 ON  " + DbSchema + ".Gv_Cobranza_B1.Moneda = Comun.Gv_Moneda_B1.Nombre");
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
            SQL.AppendLine("      " + DbSchema + ".Cobranza.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Cobranza.Numero,");
            SQL.AppendLine("      " + DbSchema + ".Cobranza.StatusCobranza,");
            SQL.AppendLine("      " + DbSchema + ".Cobranza.Fecha,");
            SQL.AppendLine("      " + DbSchema + ".Cobranza.CodigoCliente,");
            SQL.AppendLine("      " + DbSchema + ".Cobranza.CodigoCobrador,");
            SQL.AppendLine("      " + DbSchema + ".Cobranza.Moneda");
            SQL.AppendLine("      ," + DbSchema + ".Cobranza.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".Cobranza");
            SQL.AppendLine("      WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("          AND Numero IN (");
            SQL.AppendLine("            SELECT  Numero ");
            SQL.AppendLine("            FROM OPENXML( @hdoc, 'GpData/GpResult',2) ");
            SQL.AppendLine("            WITH (Numero varchar(10)) AS XmlFKTmp) ");
            SQL.AppendLine(" EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".Cobranza", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumStatusCobranza", LibTpvCreator.SqlViewStandardEnum(typeof(eStatusCobranza), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTarjeta", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDeTarjeta), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumOrigenFacturacionOManual", LibTpvCreator.SqlViewStandardEnum(typeof(eOrigenFacturacionOManual), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumStatusRetencionIVACobranza", LibTpvCreator.SqlViewStandardEnum(typeof(eStatusRetencionIVACobranza), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_Cobranza_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobranzaINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobranzaUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobranzaDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobranzaGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobranzaSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CobranzaGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
            insSps.Dispose();
            return vResult;
        }

        public bool InstalarTabla() {
            bool vResult = false;
            if (CrearTabla()) {
                CrearVistas();
                CrearProcedimientos();
                //clsDocumentoCobradoED insDetailDocCob = new clsDocumentoCobradoED();
                //vResult = insDetailDocCob.InstalarTabla();
                //clsDetalleDeCobranzaED insDetailDetDeCob = new clsDetalleDeCobranzaED();
                //vResult = vResult && insDetailDetDeCob.InstalarTabla();
            }
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = false;
            if (insDbo.Exists(DbSchema + ".Cobranza", eDboType.Tabla)) {
                CrearVistas();
                CrearProcedimientos();
                //vResult = new clsDocumentoCobradoED().InstalarVistasYSps();
                //vResult = vResult && new clsDetalleDeCobranzaED().InstalarVistasYSps();
            }
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = false;
            LibStoredProc insSp = new LibStoredProc();
            LibViews insVista = new LibViews();
            //vResult = new clsDocumentoCobradoED().BorrarVistasYSps();
            //vResult = new clsDetalleDeCobranzaED().BorrarVistasYSps() && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobranzaINS") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobranzaUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobranzaDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobranzaGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobranzaGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CobranzaSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_Cobranza_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumStatusCobranza") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTarjeta") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumOrigenFacturacionOManual") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumStatusRetencionIVACobranza") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCobranzaED

} //End of namespace Galac.Adm.Dal.Venta

