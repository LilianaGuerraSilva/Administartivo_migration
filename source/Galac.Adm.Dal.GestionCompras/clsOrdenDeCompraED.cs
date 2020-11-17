using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Dal.GestionCompras {
    [LibMefDalComponentMetadata(typeof(clsOrdenDeCompraED))]
    public class clsOrdenDeCompraED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsOrdenDeCompraED(): base(){
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
            get { return "OrdenDeCompra"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("OrdenDeCompra", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeComConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeComConsecutiv NOT NULL, ");
            SQL.AppendLine("Serie" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT nnOrdDeComSerie NOT NULL, ");
            SQL.AppendLine("Numero" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT nnOrdDeComNumero NOT NULL, ");
            SQL.AppendLine("Fecha" + InsSql.DateTypeForDb() + " CONSTRAINT nnOrdDeComFecha NOT NULL, ");
            SQL.AppendLine("ConsecutivoProveedor" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeComConsecutiv NOT NULL, ");
            SQL.AppendLine("NumeroCotizacion" + InsSql.VarCharTypeForDb(11) + " CONSTRAINT d_OrdDeComNuCo DEFAULT (''), ");
            SQL.AppendLine("Moneda" + InsSql.VarCharTypeForDb(80) + " CONSTRAINT d_OrdDeComMo DEFAULT (''), ");
            SQL.AppendLine("CodigoMoneda" + InsSql.VarCharTypeForDb(4) + " CONSTRAINT d_OrdDeComCoMo DEFAULT (''), ");
            SQL.AppendLine("CambioABolivares" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnOrdDeComCambioABol NOT NULL, ");
            SQL.AppendLine("TotalRenglones" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_OrdDeComToRe DEFAULT (0), ");
            SQL.AppendLine("TotalCompra" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_OrdDeComToCo DEFAULT ((0)), ");
            SQL.AppendLine("TipoDeCompra" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_OrdDeComTiDeCo DEFAULT ('0'), ");
            SQL.AppendLine("Comentarios" + InsSql.VarCharTypeForDb(255) + " CONSTRAINT d_OrdDeComCo DEFAULT (''), ");
            SQL.AppendLine("StatusOrdenDeCompra" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_OrdDeComStOrDeCo DEFAULT ('0'), ");
            SQL.AppendLine("FechaDeAnulacion" + InsSql.DateTypeForDb() + " CONSTRAINT d_OrdDeComFeDeAn DEFAULT (''), ");
            SQL.AppendLine("CondicionesDeEntrega" + InsSql.VarCharTypeForDb(500) + " CONSTRAINT d_OrdDeComCoDeEn DEFAULT (''), ");
            SQL.AppendLine("CondicionesDePago" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeComCondicione NOT NULL, ");
            SQL.AppendLine("CondicionesDeImportacion" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_OrdDeComCoDeIm DEFAULT ('0'), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_OrdenDeCompra PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Consecutivo ASC)");
            SQL.AppendLine(", CONSTRAINT fk_OrdenDeCompraProveedor FOREIGN KEY (ConsecutivoCompania, ConsecutivoProveedor)");
            SQL.AppendLine("REFERENCES Adm.Proveedor(ConsecutivoCompania, consecutivo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_OrdenDeCompraCondicionesDePago FOREIGN KEY (ConsecutivoCompania, CondicionesDePago)");
            SQL.AppendLine("REFERENCES Saw.CondicionesDePago(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(",CONSTRAINT u_OrdDeComniarieerodorpodecom UNIQUE NONCLUSTERED (ConsecutivoCompania,Serie,Numero,ConsecutivoProveedor,TipoDeCompra)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT OrdenDeCompra.ConsecutivoCompania, OrdenDeCompra.Consecutivo, OrdenDeCompra.Serie, OrdenDeCompra.Numero");
            SQL.AppendLine(", OrdenDeCompra.Fecha, OrdenDeCompra.ConsecutivoProveedor, OrdenDeCompra.NumeroCotizacion, OrdenDeCompra.Moneda, OrdenDeCompra.CodigoMoneda");
            SQL.AppendLine(", OrdenDeCompra.CambioABolivares, OrdenDeCompra.TotalRenglones, OrdenDeCompra.TotalCompra, OrdenDeCompra.TipoDeCompra, " + DbSchema + ".Gv_EnumTipoCompra.StrValue AS TipoDeCompraStr");
            SQL.AppendLine(", OrdenDeCompra.Comentarios, OrdenDeCompra.StatusOrdenDeCompra, " + DbSchema + ".Gv_EnumStatusCompra.StrValue AS StatusOrdenDeCompraStr, OrdenDeCompra.FechaDeAnulacion, OrdenDeCompra.CondicionesDeEntrega");
            SQL.AppendLine(", OrdenDeCompra.CondicionesDePago, OrdenDeCompra.CondicionesDeImportacion, " + DbSchema + ".Gv_EnumCondicionDeImportacion.StrValue AS CondicionesDeImportacionStr, OrdenDeCompra.NombreOperador, OrdenDeCompra.FechaUltimaModificacion");
            SQL.AppendLine(", Adm.Proveedor.codigoProveedor AS CodigoProveedor");
            SQL.AppendLine(", Adm.Proveedor.nombreProveedor AS NombreProveedor");
            SQL.AppendLine(", Saw.CondicionesDePago.Descripcion AS DescripcionCondicionesDePago");
            SQL.AppendLine(", OrdenDeCompra.fldTimeStamp, CAST(OrdenDeCompra.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".OrdenDeCompra");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoCompra");
            SQL.AppendLine("ON " + DbSchema + ".OrdenDeCompra.TipoDeCompra COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoCompra.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumStatusCompra");
            SQL.AppendLine("ON " + DbSchema + ".OrdenDeCompra.StatusOrdenDeCompra COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumStatusCompra.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumCondicionDeImportacion");
            SQL.AppendLine("ON " + DbSchema + ".OrdenDeCompra.CondicionesDeImportacion COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumCondicionDeImportacion.DbValue");
            SQL.AppendLine("INNER JOIN Adm.Proveedor ON  " + DbSchema + ".OrdenDeCompra.ConsecutivoProveedor = Adm.Proveedor.consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".OrdenDeCompra.ConsecutivoCompania = Adm.Proveedor.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN Saw.CondicionesDePago ON  " + DbSchema + ".OrdenDeCompra.CondicionesDePago = Saw.CondicionesDePago.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".OrdenDeCompra.ConsecutivoCompania = Saw.CondicionesDePago.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Serie" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@Numero" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@ConsecutivoProveedor" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroCotizacion" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@Moneda" + InsSql.VarCharTypeForDb(80) + " = '',");
            SQL.AppendLine("@CodigoMoneda" + InsSql.VarCharTypeForDb(4) + " = '',");
            SQL.AppendLine("@CambioABolivares" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TotalRenglones" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TotalCompra" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TipoDeCompra" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Comentarios" + InsSql.VarCharTypeForDb(255) + " = '',");
            SQL.AppendLine("@StatusOrdenDeCompra" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@FechaDeAnulacion" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@CondicionesDeEntrega" + InsSql.VarCharTypeForDb(500) + " = '',");
            SQL.AppendLine("@CondicionesDePago" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CondicionesDeImportacion" + InsSql.CharTypeForDb(1) + " = '0',");
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
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".OrdenDeCompra(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            Serie,");
            SQL.AppendLine("            Numero,");
            SQL.AppendLine("            Fecha,");
            SQL.AppendLine("            ConsecutivoProveedor,");
            SQL.AppendLine("            NumeroCotizacion,");
            SQL.AppendLine("            Moneda,");
            SQL.AppendLine("            CodigoMoneda,");
            SQL.AppendLine("            CambioABolivares,");
            SQL.AppendLine("            TotalRenglones,");
            SQL.AppendLine("            TotalCompra,");
            SQL.AppendLine("            TipoDeCompra,");
            SQL.AppendLine("            Comentarios,");
            SQL.AppendLine("            StatusOrdenDeCompra,");
            SQL.AppendLine("            FechaDeAnulacion,");
            SQL.AppendLine("            CondicionesDeEntrega,");
            SQL.AppendLine("            CondicionesDePago,");
            SQL.AppendLine("            CondicionesDeImportacion,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @Serie,");
            SQL.AppendLine("            @Numero,");
            SQL.AppendLine("            @Fecha,");
            SQL.AppendLine("            @ConsecutivoProveedor,");
            SQL.AppendLine("            @NumeroCotizacion,");
            SQL.AppendLine("            @Moneda,");
            SQL.AppendLine("            @CodigoMoneda,");
            SQL.AppendLine("            @CambioABolivares,");
            SQL.AppendLine("            @TotalRenglones,");
            SQL.AppendLine("            @TotalCompra,");
            SQL.AppendLine("            @TipoDeCompra,");
            SQL.AppendLine("            @Comentarios,");
            SQL.AppendLine("            @StatusOrdenDeCompra,");
            SQL.AppendLine("            @FechaDeAnulacion,");
            SQL.AppendLine("            @CondicionesDeEntrega,");
            SQL.AppendLine("            @CondicionesDePago,");
            SQL.AppendLine("            @CondicionesDeImportacion,");
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
            SQL.AppendLine("@Serie" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@Numero" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@ConsecutivoProveedor" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroCotizacion" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@Moneda" + InsSql.VarCharTypeForDb(80) + ",");
            SQL.AppendLine("@CodigoMoneda" + InsSql.VarCharTypeForDb(4) + ",");
            SQL.AppendLine("@CambioABolivares" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TotalRenglones" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TotalCompra" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TipoDeCompra" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Comentarios" + InsSql.VarCharTypeForDb(255) + ",");
            SQL.AppendLine("@StatusOrdenDeCompra" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@FechaDeAnulacion" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@CondicionesDeEntrega" + InsSql.VarCharTypeForDb(500) + ",");
            SQL.AppendLine("@CondicionesDePago" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CondicionesDeImportacion" + InsSql.CharTypeForDb(1) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".OrdenDeCompra WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".OrdenDeCompra WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_OrdenDeCompraCanBeUpdated @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".OrdenDeCompra");
            SQL.AppendLine("            SET Serie = @Serie,");
            SQL.AppendLine("               Numero = @Numero,");
            SQL.AppendLine("               Fecha = @Fecha,");
            SQL.AppendLine("               ConsecutivoProveedor = @ConsecutivoProveedor,");
            SQL.AppendLine("               NumeroCotizacion = @NumeroCotizacion,");
            SQL.AppendLine("               Moneda = @Moneda,");
            SQL.AppendLine("               CodigoMoneda = @CodigoMoneda,");
            SQL.AppendLine("               CambioABolivares = @CambioABolivares,");
            SQL.AppendLine("               TotalRenglones = @TotalRenglones,");
            SQL.AppendLine("               TotalCompra = @TotalCompra,");
            SQL.AppendLine("               TipoDeCompra = @TipoDeCompra,");
            SQL.AppendLine("               Comentarios = @Comentarios,");
            SQL.AppendLine("               StatusOrdenDeCompra = @StatusOrdenDeCompra,");
            SQL.AppendLine("               FechaDeAnulacion = @FechaDeAnulacion,");
            SQL.AppendLine("               CondicionesDeEntrega = @CondicionesDeEntrega,");
            SQL.AppendLine("               CondicionesDePago = @CondicionesDePago,");
            SQL.AppendLine("               CondicionesDeImportacion = @CondicionesDeImportacion,");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".OrdenDeCompra WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".OrdenDeCompra WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_OrdenDeCompraCanBeDeleted @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".OrdenDeCompra");
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
            SQL.AppendLine("         OrdenDeCompra.ConsecutivoCompania,");
            SQL.AppendLine("         OrdenDeCompra.Consecutivo,");
            SQL.AppendLine("         OrdenDeCompra.Serie,");
            SQL.AppendLine("         OrdenDeCompra.Numero,");
            SQL.AppendLine("         OrdenDeCompra.Fecha,");
            SQL.AppendLine("         OrdenDeCompra.ConsecutivoProveedor,");
            SQL.AppendLine("         Gv_Proveedor_B1.codigoProveedor AS CodigoProveedor,");
            SQL.AppendLine("         Gv_Proveedor_B1.nombreProveedor AS NombreProveedor,");
            SQL.AppendLine("         OrdenDeCompra.NumeroCotizacion,");
            SQL.AppendLine("         OrdenDeCompra.Moneda,");
            SQL.AppendLine("         OrdenDeCompra.CodigoMoneda,");
            SQL.AppendLine("         OrdenDeCompra.CambioABolivares,");
            SQL.AppendLine("         OrdenDeCompra.TotalRenglones,");
            SQL.AppendLine("         OrdenDeCompra.TotalCompra,");
            SQL.AppendLine("         OrdenDeCompra.TipoDeCompra,");
            SQL.AppendLine("         OrdenDeCompra.Comentarios,");
            SQL.AppendLine("         OrdenDeCompra.StatusOrdenDeCompra,");
            SQL.AppendLine("         OrdenDeCompra.FechaDeAnulacion,");
            SQL.AppendLine("         OrdenDeCompra.CondicionesDeEntrega,");
            SQL.AppendLine("         OrdenDeCompra.CondicionesDePago,");
            SQL.AppendLine("         Gv_CondicionesDePago_B1.Descripcion AS DescripcionCondicionesDePago,");
            SQL.AppendLine("         OrdenDeCompra.CondicionesDeImportacion,");
            SQL.AppendLine("         OrdenDeCompra.NombreOperador,");
            SQL.AppendLine("         OrdenDeCompra.FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(OrdenDeCompra.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         OrdenDeCompra.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".OrdenDeCompra");
            SQL.AppendLine("             INNER JOIN Adm.Gv_Proveedor_B1 ON " + DbSchema + ".OrdenDeCompra.ConsecutivoProveedor = Adm.Gv_Proveedor_B1.consecutivo AND " + DbSchema + ".OrdenDeCompra.ConsecutivoCompania = Adm.Gv_Proveedor_B1.ConsecutivoCompania");
            SQL.AppendLine("             INNER JOIN Saw.Gv_CondicionesDePago_B1 ON " + DbSchema + ".OrdenDeCompra.CondicionesDePago = Saw.Gv_CondicionesDePago_B1.Consecutivo AND " + DbSchema + ".OrdenDeCompra.ConsecutivoCompania = Saw.Gv_CondicionesDePago_B1.ConsecutivoCompania");
            SQL.AppendLine("      WHERE OrdenDeCompra.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND OrdenDeCompra.Consecutivo = @Consecutivo");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeCompra_B1.Serie,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeCompra_B1.Numero,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.codigoProveedor AS CodigoProveedor,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.nombreProveedor AS NombreProveedor,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeCompra_B1.NumeroCotizacion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeCompra_B1.StatusOrdenDeCompraStr,");
            SQL.AppendLine("      Saw.Gv_CondicionesDePago_B1.Descripcion AS DescripcionCondicionesDePago,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeCompra_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeCompra_B1.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeCompra_B1.Fecha,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeCompra_B1.StatusOrdenDeCompra,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeCompra_B1.Moneda,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeCompra_B1.CodigoMoneda,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeCompra_B1.CambioABolivares,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeCompra_B1.Comentarios");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_OrdenDeCompra_B1");
            SQL.AppendLine("      INNER JOIN Adm.Gv_Proveedor_B1 ON  " + DbSchema + ".Gv_OrdenDeCompra_B1.ConsecutivoProveedor = Adm.Gv_Proveedor_B1.consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_OrdenDeCompra_B1.ConsecutivoCompania = Adm.Gv_Proveedor_B1.ConsecutivoCompania");
            SQL.AppendLine("      INNER JOIN Saw.Gv_CondicionesDePago_B1 ON  " + DbSchema + ".Gv_OrdenDeCompra_B1.CondicionesDePago = Saw.Gv_CondicionesDePago_B1.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_OrdenDeCompra_B1.ConsecutivoCompania = Saw.Gv_CondicionesDePago_B1.ConsecutivoCompania");
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
            SQL.AppendLine("      " + DbSchema + ".OrdenDeCompra.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".OrdenDeCompra.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".OrdenDeCompra.Serie,");
            SQL.AppendLine("      " + DbSchema + ".OrdenDeCompra.Numero,");
            SQL.AppendLine("      " + DbSchema + ".OrdenDeCompra.Fecha,");
            SQL.AppendLine("      " + DbSchema + ".OrdenDeCompra.ConsecutivoProveedor,");
            SQL.AppendLine("      " + DbSchema + ".OrdenDeCompra.NumeroCotizacion,");
            SQL.AppendLine("      " + DbSchema + ".OrdenDeCompra.StatusOrdenDeCompra,");
            SQL.AppendLine("      " + DbSchema + ".OrdenDeCompra.CondicionesDePago");
           // SQL.AppendLine("      ," + DbSchema + ".OrdenDeCompra.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".OrdenDeCompra");
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
            bool vResult = insDbo.Create(DbSchema + ".OrdenDeCompra", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumStatusCompra",LibTpvCreator.SqlViewStandardEnum(typeof(eStatusCompra),InsSql),true,true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoCompra",LibTpvCreator.SqlViewStandardEnum(typeof(eTipoCompra),InsSql),true,true);            
            vResult = insVistas.Create(DbSchema + ".Gv_EnumCondicionDeImportacion", LibTpvCreator.SqlViewStandardEnum(typeof(eCondicionDeImportacion), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_OrdenDeCompra_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeCompraINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeCompraUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeCompraDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeCompraGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeCompraSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeCompraGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
            insSps.Dispose();
            return vResult;
        }

        public bool InstalarTabla() {
            bool vResult = false;
            if (CrearTabla()) {
                CrearVistas();
                CrearProcedimientos();
                clsOrdenDeCompraDetalleArticuloInventarioED insDetailOrdDeComDetArtInv = new clsOrdenDeCompraDetalleArticuloInventarioED();
                vResult = insDetailOrdDeComDetArtInv.InstalarTabla();
            }
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = false;
            if (insDbo.Exists(DbSchema + ".OrdenDeCompra", eDboType.Tabla)) {
                CrearVistas();
                CrearProcedimientos();
                vResult = new clsOrdenDeCompraDetalleArticuloInventarioED().InstalarVistasYSps();
            }
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = false;
            LibStoredProc insSp = new LibStoredProc();
            LibViews insVista = new LibViews();
            vResult = new clsOrdenDeCompraDetalleArticuloInventarioED().BorrarVistasYSps();
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeCompraINS") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeCompraUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeCompraDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeCompraGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeCompraGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeCompraSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_OrdenDeCompra_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumCondicionDeImportacion") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumStatusCompra") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoCompra") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsOrdenDeCompraED

} //End of namespace Galac.Adm.Dal.GestionCompras

