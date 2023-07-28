using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.Base.Dal;

namespace Galac.Adm.Dal.Venta {
    [LibMefDalComponentMetadata(typeof(clsCajaAperturaED))]
    public class clsCajaAperturaED:LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsCajaAperturaED() : base() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Miembros de ILibMefDalComponent
        string ILibMefDalComponent.DbSchema {
            get {
                return DbSchema;
            }
        }

        string ILibMefDalComponent.Name {
            get {
                return GetType().Name;
            }
        }

        string ILibMefDalComponent.Table {
            get {
                return "CajaApertura";
            }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("CajaApertura",DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + " CONSTRAINT nnCajApeConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10,0) + " CONSTRAINT nnCajApeConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoCaja" + InsSql.NumericTypeForDb(10,0) + " CONSTRAINT nnCajApeConsecutiv NOT NULL, ");
            SQL.AppendLine("NombreDelUsuario" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT nnCajApeNombreDelU NOT NULL, ");
            SQL.AppendLine("MontoApertura" + InsSql.DecimalTypeForDb(25,4) + " CONSTRAINT d_CajApeMoAp DEFAULT (0), ");
            SQL.AppendLine("MontoCierre" + InsSql.DecimalTypeForDb(25,4) + " CONSTRAINT d_CajApeMoCi DEFAULT (0), ");
            SQL.AppendLine("MontoEfectivo" + InsSql.DecimalTypeForDb(25,4) + " CONSTRAINT d_CajApeMoEf DEFAULT (0), ");
            SQL.AppendLine("MontoTarjeta" + InsSql.DecimalTypeForDb(25,4) + " CONSTRAINT d_CajApeMoTa DEFAULT (0), ");
            SQL.AppendLine("MontoCheque" + InsSql.DecimalTypeForDb(25,4) + " CONSTRAINT d_CajApeMoCh DEFAULT (0), ");
            SQL.AppendLine("MontoDeposito" + InsSql.DecimalTypeForDb(25,4) + " CONSTRAINT d_CajApeMoDe DEFAULT (0), ");
            SQL.AppendLine("MontoAnticipo" + InsSql.DecimalTypeForDb(25,4) + " CONSTRAINT d_CajApeMoAn DEFAULT (0), ");
            SQL.AppendLine("Fecha" + InsSql.DateTypeForDb() + " CONSTRAINT d_CajApeFe DEFAULT (''), ");
            SQL.AppendLine("HoraApertura" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT d_CajApeHoAp DEFAULT (''), ");
            SQL.AppendLine("HoraCierre" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT d_CajApeHoCi DEFAULT (''), ");
            SQL.AppendLine("CajaCerrada" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCajApeCajaCerrad NOT NULL, ");
            SQL.AppendLine("CodigoMoneda" + InsSql.VarCharTypeForDb(4) + " CONSTRAINT d_CajApeCoMo DEFAULT ('VED'), ");
            SQL.AppendLine("Cambio" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CajApeCa DEFAULT (1), ");
            SQL.AppendLine("MontoAperturaME" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CajApeMoApME DEFAULT (0), ");
            SQL.AppendLine("MontoCierreME" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CajApeMoCiME DEFAULT (0), ");
            SQL.AppendLine("MontoEfectivoME" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CajApeMoEfME DEFAULT (0), ");
            SQL.AppendLine("MontoTarjetaME" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CajApeMoTaME DEFAULT (0), ");
            SQL.AppendLine("MontoChequeME" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CajApeMoChME DEFAULT (0), ");
            SQL.AppendLine("MontoDepositoME" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CajApeMoDeME DEFAULT (0), ");
            SQL.AppendLine("MontoAnticipoME" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_CajApeMoAnME DEFAULT (0), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_CajaApertura PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Consecutivo ASC, ConsecutivoCaja ASC)");
            SQL.AppendLine(", CONSTRAINT fk_CajaAperturaCaja FOREIGN KEY (ConsecutivoCompania, ConsecutivoCaja)");
            SQL.AppendLine("REFERENCES Adm.Caja(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_CajaAperturaGUser FOREIGN KEY (NombreDelUsuario)");
            SQL.AppendLine("REFERENCES Lib.GUser(UserName) ");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_CajaAperturaMoneda FOREIGN KEY (CodigoMoneda)");
            SQL.AppendLine("REFERENCES Moneda(Codigo)");
            SQL.AppendLine("ON UPDATE CASCADE) ");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT CajaApertura.ConsecutivoCompania, CajaApertura.Consecutivo, CajaApertura.ConsecutivoCaja, CajaApertura.NombreDelUsuario");
            SQL.AppendLine(", CajaApertura.MontoApertura, CajaApertura.MontoCierre, CajaApertura.MontoEfectivo, CajaApertura.MontoTarjeta");
            SQL.AppendLine(", CajaApertura.MontoCheque, CajaApertura.MontoDeposito, CajaApertura.MontoAnticipo, CajaApertura.Fecha");
            SQL.AppendLine(", CajaApertura.HoraApertura, CajaApertura.HoraCierre, CajaApertura.CajaCerrada, CajaApertura.CodigoMoneda");
            SQL.AppendLine(", CajaApertura.Cambio, CajaApertura.MontoAperturaME, CajaApertura.MontoCierreME, CajaApertura.MontoEfectivoME");
            SQL.AppendLine(", CajaApertura.MontoTarjetaME, CajaApertura.MontoChequeME, CajaApertura.MontoDepositoME, CajaApertura.MontoAnticipoME");
            SQL.AppendLine(", CajaApertura.NombreOperador, CajaApertura.FechaUltimaModificacion");
            SQL.AppendLine(", CajaApertura.fldTimeStamp, CAST(CajaApertura.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".CajaApertura");
            SQL.AppendLine("INNER JOIN Adm.Caja ON  " + DbSchema + ".CajaApertura.ConsecutivoCaja = Adm.Caja.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".CajaApertura.ConsecutivoCompania = Adm.Caja.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN Lib.GUser ON  " + DbSchema + ".CajaApertura.NombreDelUsuario = Lib.GUser.UserName");
            SQL.AppendLine("INNER JOIN Moneda ON  " + DbSchema + ".CajaApertura.CodigoMoneda = Moneda.Codigo");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();            
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@ConsecutivoCaja" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@NombreDelUsuario" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@MontoApertura" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@MontoCierre" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@MontoEfectivo" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@MontoTarjeta" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@MontoCheque" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@MontoDeposito" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@MontoAnticipo" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@HoraApertura" + InsSql.VarCharTypeForDb(5) + " = '',");
            SQL.AppendLine("@HoraCierre" + InsSql.VarCharTypeForDb(5) + " = '',");
            SQL.AppendLine("@CajaCerrada" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@CodigoMoneda" + InsSql.VarCharTypeForDb(4) + ",");
            SQL.AppendLine("@Cambio" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoAperturaME" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoCierreME" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoEfectivoME" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoTarjetaME" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoChequeME" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoDepositoME" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoAnticipoME" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + " = '01/01/1900'");
            return SQL.ToString();
        }

        private string SqlSpIns() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");            
            SQL.AppendLine("   SET DATEFORMAT @DateFormat");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10,0) + "");
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".CajaApertura(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            ConsecutivoCaja,");
            SQL.AppendLine("            NombreDelUsuario,");
            SQL.AppendLine("            MontoApertura,");
            SQL.AppendLine("            MontoCierre,");
            SQL.AppendLine("            MontoEfectivo,");
            SQL.AppendLine("            MontoTarjeta,");
            SQL.AppendLine("            MontoCheque,");
            SQL.AppendLine("            MontoDeposito,");
            SQL.AppendLine("            MontoAnticipo,");
            SQL.AppendLine("            Fecha,");
            SQL.AppendLine("            HoraApertura,");
            SQL.AppendLine("            HoraCierre,");
            SQL.AppendLine("            CajaCerrada,");
            SQL.AppendLine("            CodigoMoneda,");
            SQL.AppendLine("            Cambio,");
            SQL.AppendLine("            MontoAperturaME,");
            SQL.AppendLine("            MontoCierreME,");
            SQL.AppendLine("            MontoEfectivoME,");
            SQL.AppendLine("            MontoTarjetaME,");
            SQL.AppendLine("            MontoChequeME,");
            SQL.AppendLine("            MontoDepositoME,");
            SQL.AppendLine("            MontoAnticipoME,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @ConsecutivoCaja,");
            SQL.AppendLine("            @NombreDelUsuario,");
            SQL.AppendLine("            @MontoApertura,");
            SQL.AppendLine("            @MontoCierre,");
            SQL.AppendLine("            @MontoEfectivo,");
            SQL.AppendLine("            @MontoTarjeta,");
            SQL.AppendLine("            @MontoCheque,");
            SQL.AppendLine("            @MontoDeposito,");
            SQL.AppendLine("            @MontoAnticipo,");
            SQL.AppendLine("            @Fecha,");
            SQL.AppendLine("            @HoraApertura,");
            SQL.AppendLine("            @HoraCierre,");
            SQL.AppendLine("            @CajaCerrada,");
            SQL.AppendLine("            @CodigoMoneda,");
            SQL.AppendLine("            @Cambio,");
            SQL.AppendLine("            @MontoAperturaME,");
            SQL.AppendLine("            @MontoCierreME,");
            SQL.AppendLine("            @MontoEfectivoME,");
            SQL.AppendLine("            @MontoTarjetaME,");
            SQL.AppendLine("            @MontoChequeME,");
            SQL.AppendLine("            @MontoDepositoME,");
            SQL.AppendLine("            @MontoAnticipoME,");
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
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@ConsecutivoCaja" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@NombreDelUsuario" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@MontoApertura" + InsSql.DecimalTypeForDb(25,4) + ",");
            SQL.AppendLine("@MontoCierre" + InsSql.DecimalTypeForDb(25,4) + ",");
            SQL.AppendLine("@MontoEfectivo" + InsSql.DecimalTypeForDb(25,4) + ",");
            SQL.AppendLine("@MontoTarjeta" + InsSql.DecimalTypeForDb(25,4) + ",");
            SQL.AppendLine("@MontoCheque" + InsSql.DecimalTypeForDb(25,4) + ",");
            SQL.AppendLine("@MontoDeposito" + InsSql.DecimalTypeForDb(25,4) + ",");
            SQL.AppendLine("@MontoAnticipo" + InsSql.DecimalTypeForDb(25,4) + ",");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@HoraApertura" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@HoraCierre" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@CajaCerrada" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@CodigoMoneda" + InsSql.VarCharTypeForDb(4) + ",");
            SQL.AppendLine("@Cambio" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoAperturaME" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoCierreME" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoEfectivoME" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoTarjetaME" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoChequeME" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoDepositoME" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoAnticipoME" + InsSql.DecimalTypeForDb(25, 4) + ",");
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
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10,0) + "");
            //SQL.AppendLine("--DECLARE @CanBeChanged bit");
            SQL.AppendLine("   SET @ReturnValue = -1");
            SQL.AppendLine("   SET @ValidationMsg = ''");
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CajaApertura WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo AND ConsecutivoCaja = @ConsecutivoCaja)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CajaApertura WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo AND ConsecutivoCaja = @ConsecutivoCaja");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_CajaAperturaCanBeUpdated @ConsecutivoCompania,@Consecutivo,@ConsecutivoCaja, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".CajaApertura");
            SQL.AppendLine("            SET NombreDelUsuario = @NombreDelUsuario,");
            SQL.AppendLine("               MontoApertura = @MontoApertura,");
            SQL.AppendLine("               MontoCierre = @MontoCierre,");
            SQL.AppendLine("               MontoEfectivo = @MontoEfectivo,");
            SQL.AppendLine("               MontoTarjeta = @MontoTarjeta,");
            SQL.AppendLine("               MontoCheque = @MontoCheque,");
            SQL.AppendLine("               MontoDeposito = @MontoDeposito,");
            SQL.AppendLine("               MontoAnticipo = @MontoAnticipo,");
            SQL.AppendLine("               Fecha = @Fecha,");
            SQL.AppendLine("               HoraApertura = @HoraApertura,");
            SQL.AppendLine("               HoraCierre = @HoraCierre,");
            SQL.AppendLine("               CajaCerrada = @CajaCerrada,");
            SQL.AppendLine("               CodigoMoneda = @CodigoMoneda,");
            SQL.AppendLine("               Cambio = @Cambio,");
            SQL.AppendLine("               MontoAperturaME = @MontoAperturaME,");
            SQL.AppendLine("               MontoCierreME = @MontoCierreME,");
            SQL.AppendLine("               MontoEfectivoME = @MontoEfectivoME,");
            SQL.AppendLine("               MontoTarjetaME = @MontoTarjetaME,");
            SQL.AppendLine("               MontoChequeME = @MontoChequeME,");
            SQL.AppendLine("               MontoDepositoME = @MontoDepositoME,");
            SQL.AppendLine("               MontoAnticipoME = @MontoAnticipoME,");
            SQL.AppendLine("               NombreOperador = @NombreOperador,");
            SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND Consecutivo = @Consecutivo");
            SQL.AppendLine("               AND ConsecutivoCaja = @ConsecutivoCaja");
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
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@ConsecutivoCaja" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@TimeStampAsInt" + InsSql.BigintTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpDel() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @CurrentTimeStamp timestamp");
            SQL.AppendLine("   DECLARE @ValidationMsg " + InsSql.VarCharTypeForDb(1500) + " --No puede ser más");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10,0) + "");
            //SQL.AppendLine("--DECLARE @CanBeDeleted bit");
            SQL.AppendLine("   SET @ReturnValue = -1");
            SQL.AppendLine("   SET @ValidationMsg = ''");
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CajaApertura WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo AND ConsecutivoCaja = @ConsecutivoCaja)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CajaApertura WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo AND ConsecutivoCaja = @ConsecutivoCaja");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_CajaAperturaCanBeDeleted @ConsecutivoCompania,@Consecutivo,@ConsecutivoCaja, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".CajaApertura");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND Consecutivo = @Consecutivo");
            SQL.AppendLine("               AND ConsecutivoCaja = @ConsecutivoCaja");
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
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@ConsecutivoCaja" + InsSql.NumericTypeForDb(10,0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         CajaApertura.ConsecutivoCompania,");
            SQL.AppendLine("         CajaApertura.Consecutivo,");
            SQL.AppendLine("         CajaApertura.ConsecutivoCaja,");
            SQL.AppendLine("         Gv_Caja_B1.NombreCaja AS NombreCaja,");
            SQL.AppendLine("         CajaApertura.NombreDelUsuario,");
            SQL.AppendLine("         CajaApertura.MontoApertura,");
            SQL.AppendLine("         CajaApertura.MontoCierre,");
            SQL.AppendLine("         CajaApertura.MontoEfectivo,");
            SQL.AppendLine("         CajaApertura.MontoTarjeta,");
            SQL.AppendLine("         CajaApertura.MontoCheque,");
            SQL.AppendLine("         CajaApertura.MontoDeposito,");
            SQL.AppendLine("         CajaApertura.MontoAnticipo,");
            SQL.AppendLine("         CajaApertura.Fecha,");
            SQL.AppendLine("         CajaApertura.HoraApertura,");
            SQL.AppendLine("         CajaApertura.HoraCierre,");
            SQL.AppendLine("         CajaApertura.CajaCerrada,");
            SQL.AppendLine("         CajaApertura.CodigoMoneda,");
            SQL.AppendLine("         CajaApertura.Cambio,");
            SQL.AppendLine("         CajaApertura.MontoAperturaME,");
            SQL.AppendLine("         CajaApertura.MontoCierreME,");
            SQL.AppendLine("         CajaApertura.MontoEfectivoME,");
            SQL.AppendLine("         CajaApertura.MontoTarjetaME,");
            SQL.AppendLine("         CajaApertura.MontoChequeME,");
            SQL.AppendLine("         CajaApertura.MontoDepositoME,");
            SQL.AppendLine("         CajaApertura.MontoAnticipoME,");
            SQL.AppendLine("         CajaApertura.NombreOperador,");
            SQL.AppendLine("         CajaApertura.FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(CajaApertura.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         CajaApertura.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".CajaApertura");
            SQL.AppendLine("             INNER JOIN Adm.Gv_Caja_B1 ON " + DbSchema + ".CajaApertura.ConsecutivoCaja = Adm.Gv_Caja_B1.Consecutivo ");
            SQL.AppendLine("             AND " + DbSchema + ".CajaApertura.ConsecutivoCompania = Adm.Gv_Caja_B1.ConsecutivoCompania ");
            SQL.AppendLine("             INNER JOIN Lib.Gv_GUser_B1 ON " + DbSchema + ".CajaApertura.NombreDelUsuario = Lib.Gv_GUser_B1.UserName");
            SQL.AppendLine("             INNER JOIN dbo.Gv_Moneda_B1 ON " + DbSchema + ".CajaApertura.CodigoMoneda = dbo.Gv_Moneda_B1.Codigo");
			SQL.AppendLine("      WHERE CajaApertura.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND CajaApertura.Consecutivo = @Consecutivo");
            SQL.AppendLine("         AND CajaApertura.ConsecutivoCaja = @ConsecutivoCaja");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSearchParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");
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
            SQL.AppendLine("   DECLARE @ConsecutivoCompaniaStr AS " + InsSql.VarCharTypeForDb(5));
            SQL.AppendLine("   DECLARE @TopClausule AS " + InsSql.VarCharTypeForDb(10));
            SQL.AppendLine("   SET @ConsecutivoCompaniaStr = CONVERT(VARCHAR(5),@ConsecutivoCompania)");
            SQL.AppendLine("   IF(@UseTopClausule = 'S') ");
            SQL.AppendLine("    SET @TopClausule = 'TOP 500'");
            SQL.AppendLine("   ELSE ");
            SQL.AppendLine("    SET @TopClausule = ''");
            SQL.AppendLine("   SET @strSQL = ");
            SQL.AppendLine("    ' SET DateFormat ' + @DateFormat + '");
            SQL.AppendLine("    ;WITH CTE_CajaApertura( ");
            SQL.AppendLine("    ConsecutivoCompania,ConsecutivoCaja,Consecutivo) ");
            SQL.AppendLine("    AS (SELECT TOP 200 ");
            SQL.AppendLine("    ConsecutivoCompania,ConsecutivoCaja,MAX(Consecutivo) AS MaxCaja");
            SQL.AppendLine("    FROM " + DbSchema + ".Gv_CajaApertura_B1 ");
            SQL.AppendLine("    WHERE ConsecutivoCompania =' + @ConsecutivoCompaniaStr + '");
            SQL.AppendLine("    GROUP BY ConsecutivoCompania,ConsecutivoCaja ");
            SQL.AppendLine("    ORDER BY ConsecutivoCaja )");
            SQL.AppendLine("      SELECT ' + @TopClausule + '");
            SQL.AppendLine("      " + DbSchema + ".Gv_CajaApertura_B1.NombreDelUsuario,");            
            SQL.AppendLine("      " + DbSchema + ".Gv_CajaApertura_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_CajaApertura_B1.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_CajaApertura_B1.ConsecutivoCaja,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Caja_B1.NombreCaja AS NombreCaja,");
            SQL.AppendLine("      " + DbSchema + ".Gv_CajaApertura_B1.HoraApertura,");
            SQL.AppendLine("      " + DbSchema + ".Gv_CajaApertura_B1.HoraCierre,");
            SQL.AppendLine("      " + DbSchema + ".Gv_CajaApertura_B1.CajaCerrada,");
            SQL.AppendLine("      " + DbSchema + ".Gv_CajaApertura_B1.MontoApertura,");
            SQL.AppendLine("      " + DbSchema + ".Gv_CajaApertura_B1.MontoCierreME,");
            SQL.AppendLine("      " + DbSchema + ".Gv_CajaApertura_B1.MontoAperturaME,");
            SQL.AppendLine("      " + DbSchema + ".Gv_CajaApertura_B1.MontoCierre,");
            SQL.AppendLine("      " + DbSchema + ".Gv_CajaApertura_B1.Cambio,");
            SQL.AppendLine("      " + DbSchema + ".Gv_CajaApertura_B1.CodigoMoneda,");			
			SQL.AppendLine("      " + DbSchema + ".Gv_CajaApertura_B1.Fecha,");
            SQL.AppendLine(" CAST( " + DbSchema + ".Gv_CajaApertura_B1.fldTimeStamp AS bigint) AS fldTimeStampBigint ");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_CajaApertura_B1");
            SQL.AppendLine("      INNER JOIN Adm.Gv_Caja_B1 ON  " + DbSchema + ".Gv_CajaApertura_B1.ConsecutivoCaja = Adm.Gv_Caja_B1.Consecutivo ");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_CajaApertura_B1.ConsecutivoCompania = Adm.Gv_Caja_B1.ConsecutivoCompania");
            SQL.AppendLine("      INNER JOIN Lib.Gv_GUser_B1 ON  " + DbSchema + ".Gv_CajaApertura_B1.NombreDelUsuario = Lib.Gv_GUser_B1.UserName ");
			SQL.AppendLine("      INNER JOIN dbo.Gv_Moneda_B1 ON  " + DbSchema + ".Gv_CajaApertura_B1.CodigoMoneda = dbo.Gv_Moneda_B1.Codigo ");
            SQL.AppendLine("      INNER JOIN CTE_CajaApertura ON ");
            SQL.AppendLine(DbSchema + ".Gv_CajaApertura_B1.ConsecutivoCaja = CTE_CajaApertura.ConsecutivoCaja AND ");
            SQL.AppendLine(DbSchema + ".Gv_CajaApertura_B1.Consecutivo = CTE_CajaApertura.Consecutivo '");
            SQL.AppendLine("   IF (NOT @SQLWhere IS NULL) AND (@SQLWhere <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' WHERE ' + @SQLWhere");
            SQL.AppendLine("   IF (NOT @SQLOrderBy IS NULL) AND (@SQLOrderBy <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' ORDER BY ' + @SQLOrderBy");
            SQL.AppendLine("   EXEC(@strSQL)");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpGetFKParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");
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
            SQL.AppendLine("      " + DbSchema + ".CajaApertura.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".CajaApertura.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".CajaApertura.ConsecutivoCaja,");
            SQL.AppendLine("      " + DbSchema + ".CajaApertura.NombreDelUsuario,");
            SQL.AppendLine("      " + DbSchema + ".CajaApertura.CodigoMoneda");
            //            SQL.AppendLine("      ," + DbSchema + ".CajaApertura.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".CajaApertura");
            SQL.AppendLine("      WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("          AND ConsecutivoCaja IN (");
            SQL.AppendLine("            SELECT  ConsecutivoCaja ");
            SQL.AppendLine("            FROM OPENXML( @hdoc, 'GpData/GpResult',2) ");
            SQL.AppendLine("            WITH (ConsecutivoCaja int) AS XmlFKTmp) ");
            SQL.AppendLine(" EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpUsuarioAsignadoParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@NombreDelUsuario" + InsSql.VarCharTypeForDb(20) + ", ");
            SQL.AppendLine("@ConsecutivoCaja" + InsSql.NumericTypeForDb(10,0) + ", ");
            SQL.AppendLine("@CajaCerrada" + InsSql.VarCharTypeForDb(1) + ", ");
            SQL.AppendLine("@ResumenDiario" + InsSql.VarCharTypeForDb(1));
            return SQL.ToString();
        }

        private string SqlSpUsuarioAsignadoGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(" IF @ResumenDiario ='N' ");
            SQL.AppendLine(" BEGIN ");
            SQL.AppendLine(" IF (SELECT  COUNT(Adm.CajaApertura.NombreDelUsuario) AS CantidadApertura ");
            SQL.AppendLine("   FROM Adm.CajaApertura");
            SQL.AppendLine("   WHERE NombreDelUsuario = @NombreDelUsuario ");
            SQL.AppendLine("   AND CajaCerrada = @CajaCerrada ");
            SQL.AppendLine("   AND ConsecutivoCaja = @ConsecutivoCaja ");
            SQL.AppendLine("   AND ConsecutivoCompania = @ConsecutivoCompania)> 0 ");
            SQL.AppendLine("   SELECT 'S' AS UsuarioYaAsignado ");
            SQL.AppendLine(" ELSE ");
            SQL.AppendLine("   SELECT 'N' AS UsuarioYaAsignado ");
            SQL.AppendLine(" END ");
            SQL.AppendLine(" ELSE ");
            SQL.AppendLine(" BEGIN ");
            SQL.AppendLine(" IF (SELECT  COUNT(Adm.CajaApertura.NombreDelUsuario) AS CantidadApertura ");
            SQL.AppendLine("   FROM Adm.CajaApertura");
            SQL.AppendLine("   WHERE NombreDelUsuario = @NombreDelUsuario ");
            SQL.AppendLine("   AND ConsecutivoCaja = @ConsecutivoCaja ");
            SQL.AppendLine("   AND ConsecutivoCompania = @ConsecutivoCompania)> 0 ");
            SQL.AppendLine("   SELECT 'S' AS UsuarioYaAsignado ");
            SQL.AppendLine(" ELSE ");
            SQL.AppendLine("   SELECT 'N' AS UsuarioYaAsignado ");
            SQL.AppendLine(" END ");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".CajaApertura",SqlCreateTable(),false,eDboType.Tabla);
            return vResult;
        }

        private string SqlSpCajasCerradasParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@CajaCerrada" + InsSql.VarCharTypeForDb(1) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@ConsecutivoCaja" + InsSql.NumericTypeForDb(10,0) + "");
            return SQL.ToString();
        }

        private string SqlSpCajasCerradasGet() {
            StringBuilder SQL = new StringBuilder();           
            SQL.AppendLine(" SET NOCOUNT ON; ");
            SQL.AppendLine(" BEGIN ");
            SQL.AppendLine(" IF(SELECT COUNT(*) AS RecCount ");
            SQL.AppendLine(" FROM " + DbSchema + ".CajaApertura ");            
            SQL.AppendLine(" WHERE ConsecutivoCaja =  @ConsecutivoCaja ");
            SQL.AppendLine(" AND ConsecutivoCompania = @ConsecutivoCompania ");
            SQL.AppendLine(" AND CajaCerrada = @CajaCerrada ) > 0 ");
            SQL.AppendLine(" SELECT @CajaCerrada AS ReqCajasCerradas ");
            SQL.AppendLine(" ELSE SELECT(CASE WHEN @CajaCerrada = 'S' THEN ");
            SQL.AppendLine(" 'N' ELSE 'S' END) AS ReqCajasCerradas END ");            
            return SQL.ToString();
        }

        private string SqlSpCajaAperturaCerrarParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");            
            SQL.AppendLine("@ConsecutivoCaja" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@HoraCierre" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@MontoEfectivo" + InsSql.NumericTypeForDb(30,5) + ",");
            SQL.AppendLine("@MontoCheque" + InsSql.NumericTypeForDb(30,5) + ",");
            SQL.AppendLine("@MontoTarjeta" + InsSql.NumericTypeForDb(30,5) + ",");
            SQL.AppendLine("@MontoDeposito" + InsSql.NumericTypeForDb(30,5) + ",");
            SQL.AppendLine("@MontoAnticipo" + InsSql.NumericTypeForDb(30,5) + ",");
            SQL.AppendLine("@MontoCierre" + InsSql.NumericTypeForDb(30,5) + ",");
            SQL.AppendLine("@MontoEfectivoME" + InsSql.NumericTypeForDb(30, 5) + ",");
            SQL.AppendLine("@MontoChequeME" + InsSql.NumericTypeForDb(30, 5) + ",");
            SQL.AppendLine("@MontoTarjetaME" + InsSql.NumericTypeForDb(30, 5) + ",");
            SQL.AppendLine("@MontoDepositoME" + InsSql.NumericTypeForDb(30, 5) + ",");
            SQL.AppendLine("@MontoAnticipoME" + InsSql.NumericTypeForDb(30, 5) + ",");
            SQL.AppendLine("@MontoCierreME" + InsSql.NumericTypeForDb(30, 5) + ",");
            SQL.AppendLine("@Cambio" + InsSql.NumericTypeForDb(30,5) + ",");
            SQL.AppendLine("@CodigoMoneda" + InsSql.VarCharTypeForDb(4) + ",");
            SQL.AppendLine("@CajaCerrada" + InsSql.BoolTypeForDb() + "");
            return SQL.ToString();
        }

        private string SqlSpCajaAperturaCerrar() {
            StringBuilder SQL = new StringBuilder();
            QAdvSql _QAdvSql = new QAdvSql("");
            SQL.AppendLine(" SET NOCOUNT ON; ");
            SQL.AppendLine(" BEGIN");
            SQL.AppendLine(" UPDATE ");
            SQL.AppendLine(" " + DbSchema + ".CajaApertura ");
            SQL.AppendLine(" SET CajaCerrada = @CajaCerrada, ");
            SQL.AppendLine(" HoraCierre  = @HoraCierre, ");
            SQL.AppendLine(" MontoEfectivo = @MontoEfectivo, ");
            SQL.AppendLine(" MontoCheque = @MontoCheque, ");
            SQL.AppendLine(" MontoTarjeta = @MontoTarjeta, ");
            SQL.AppendLine(" MontoDeposito = @MontoDeposito, ");
            SQL.AppendLine(" MontoAnticipo = @MontoAnticipo, ");
            SQL.AppendLine(" MontoCierre = @MontoCierre, ");
            SQL.AppendLine(" MontoEfectivoME = @MontoEfectivoME, ");
            SQL.AppendLine(" MontoChequeME = @MontoChequeME, ");
            SQL.AppendLine(" MontoTarjetaME = @MontoTarjetaME, ");
            SQL.AppendLine(" MontoDepositoME = @MontoDepositoME, ");
            SQL.AppendLine(" MontoAnticipoME = @MontoAnticipoME, ");
            SQL.AppendLine(" MontoCierreME = @MontoCierreME, ");
            SQL.AppendLine(" Cambio = @Cambio, ");
            SQL.AppendLine(" CodigoMoneda = @CodigoMoneda, ");			
            SQL.AppendLine(" FechaUltimaModificacion = @FechaUltimaModificacion ");
            SQL.AppendLine(" WHERE ");
            SQL.AppendLine(" ConsecutivoCaja = @ConsecutivoCaja ");
            SQL.AppendLine(" AND ConsecutivoCompania = @ConsecutivoCompania ");
            SQL.AppendLine(" AND CajaCerrada <> " + _QAdvSql.ToSqlValue(true));
            SQL.AppendLine(" SELECT  @@ROWCOUNT AS RowsAfects ");
            SQL.AppendLine(" END");
            return SQL.ToString();
        }        

        bool CrearVistas() {
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_CajaApertura_B1",SqlViewB1(),true);
            insVistas.Dispose();
            return vResult;
        }
        #endregion //Queries     

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CajaAperturaINS",SqlSpInsParameters(),SqlSpIns(),true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CajaAperturaUPD",SqlSpUpdParameters(),SqlSpUpd(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CajaAperturaDEL",SqlSpDelParameters(),SqlSpDel(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CajaAperturaGET",SqlSpGetParameters(),SqlSpGet(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CajaAperturaSCH",SqlSpSearchParameters(),SqlSpSearch(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CajaAperturaGetFk",SqlSpGetFKParameters(),SqlSpGetFK(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CajasCerradasGet",SqlSpCajasCerradasParameters(),SqlSpCajasCerradasGet(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CajaAperturaCerrar",SqlSpCajaAperturaCerrarParameters(),SqlSpCajaAperturaCerrar(),true) && vResult;            
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CajaUsuarioAsignadoGet",SqlSpUsuarioAsignadoParameters(),SqlSpUsuarioAsignadoGet(),true) && vResult;            
            insSps.Dispose();
            return vResult;
        }

        public bool InstalarTabla() {
            bool vResult = false;
            if(CrearTabla()) {
                CrearVistas();
                CrearProcedimientos();
                vResult = true;
            }
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = false;
            if(insDbo.Exists(DbSchema + ".CajaApertura",eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_CajaAperturaINS");
            vResult = insSp.Drop(DbSchema + ".Gp_CajaAperturaUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CajaAperturaDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CajaAperturaGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CajaAperturaGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CajaAperturaSCH") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CajasCerradasGet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CajaAperturaCerrar") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CajaUsuarioAsignadoGet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_regCajaAbiertaGet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CajaAperturaSCHByCaja") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_CajaApertura_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
    } //End of class clsCajaAperturaED

} //End of namespace Galac.Adm.Dal.Venta

