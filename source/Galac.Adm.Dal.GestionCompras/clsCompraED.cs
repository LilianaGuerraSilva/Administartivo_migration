using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Dal.GestionCompras {
    [LibMefDalComponentMetadata(typeof(clsCompraED))]
    public class clsCompraED:LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsCompraED() : base() {
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
                return "Compra";
            }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("Compra",DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + " CONSTRAINT nnComConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10,0) + " CONSTRAINT nnComConsecutiv NOT NULL, ");
            SQL.AppendLine("Serie" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT nnComSerie NOT NULL, ");
            SQL.AppendLine("Numero" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT nnComNumero NOT NULL, ");
            SQL.AppendLine("Fecha" + InsSql.DateTypeForDb() + " CONSTRAINT nnComFecha NOT NULL, ");
            SQL.AppendLine("ConsecutivoProveedor" + InsSql.NumericTypeForDb(10,0) + " CONSTRAINT nnComConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10,0) + " CONSTRAINT nnComConsecutiv NOT NULL, ");
            SQL.AppendLine("Moneda" + InsSql.VarCharTypeForDb(80) + " CONSTRAINT d_ComMo DEFAULT (''), ");
            SQL.AppendLine("CodigoMoneda" + InsSql.VarCharTypeForDb(4) + " CONSTRAINT d_ComCoMo DEFAULT (''), ");
            SQL.AppendLine("CambioABolivares" + InsSql.DecimalTypeForDb(25,4) + " CONSTRAINT nnComCambioABol NOT NULL, ");
            SQL.AppendLine("GenerarCXP" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnComGenerarCXP NOT NULL, ");
            SQL.AppendLine("UsaSeguro" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnComUsaSeguro NOT NULL, ");
            SQL.AppendLine("TipoDeDistribucion" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_ComTiDeDi DEFAULT ('0'), ");
            SQL.AppendLine("TasaAduanera" + InsSql.DecimalTypeForDb(25,4) + " CONSTRAINT d_ComTaAd DEFAULT (0), ");
            SQL.AppendLine("TasaDolar" + InsSql.DecimalTypeForDb(25,4) + " CONSTRAINT d_ComTaDo DEFAULT (0), ");
            SQL.AppendLine("ValorUT" + InsSql.DecimalTypeForDb(25,4) + " CONSTRAINT d_ComVaUT DEFAULT (0), ");
            SQL.AppendLine("TotalRenglones" + InsSql.DecimalTypeForDb(25,4) + " CONSTRAINT d_ComToRe DEFAULT (0), ");
            SQL.AppendLine("TotalOtrosGastos" + InsSql.DecimalTypeForDb(25,4) + " CONSTRAINT d_ComToOtGa DEFAULT (0), ");
            SQL.AppendLine("TotalCompra" + InsSql.DecimalTypeForDb(25,4) + " CONSTRAINT d_ComToCo DEFAULT (0), ");
            SQL.AppendLine("Comentarios" + InsSql.VarCharTypeForDb(255) + " CONSTRAINT d_ComCo DEFAULT (''), ");
            SQL.AppendLine("StatusCompra" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_ComStCo DEFAULT ('0'), ");
            SQL.AppendLine("TipoDeCompra" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_ComTiDeCo DEFAULT ('0'), ");
            SQL.AppendLine("FechaDeAnulacion" + InsSql.DateTypeForDb() + " CONSTRAINT d_ComFeDeAn DEFAULT (''), ");
            SQL.AppendLine("ConsecutivoOrdenDeCompra" + InsSql.NumericTypeForDb(20,0) + " CONSTRAINT d_ComCoOrDeCo DEFAULT (0), ");
            SQL.AppendLine("NumeroDeOrdenDeCompra" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT d_ComNuDeOrDeCo DEFAULT (''), ");
            SQL.AppendLine("NoFacturaNotaEntrega" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_ComNoFaNoEn DEFAULT (''), ");
            SQL.AppendLine("TipoDeCompraParaCxP" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_ComTiDeCoPaCxP DEFAULT ('0'), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_Compra PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Consecutivo ASC)");
            SQL.AppendLine(", CONSTRAINT fk_CompraProveedor FOREIGN KEY (ConsecutivoCompania, ConsecutivoProveedor)");
            SQL.AppendLine("REFERENCES Adm.Proveedor(ConsecutivoCompania, consecutivo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_CompraAlmacen FOREIGN KEY (ConsecutivoCompania, ConsecutivoAlmacen)");
            SQL.AppendLine("REFERENCES Saw.Almacen(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_CompraMoneda FOREIGN KEY (CodigoMoneda)");
            SQL.AppendLine("REFERENCES dbo.Moneda(Codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");            
            SQL.AppendLine(",CONSTRAINT u_Comniarieerodorpodecom UNIQUE NONCLUSTERED (ConsecutivoCompania,Serie,Numero,ConsecutivoProveedor,TipoDeCompra)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT Compra.ConsecutivoCompania, Compra.Consecutivo, Compra.Serie, Compra.Numero");
            SQL.AppendLine(", Compra.Fecha, Compra.ConsecutivoProveedor, Compra.ConsecutivoAlmacen, Compra.Moneda");
            SQL.AppendLine(", Compra.CodigoMoneda, Compra.CambioABolivares, Compra.GenerarCXP, Compra.UsaSeguro");
            SQL.AppendLine(", Compra.TipoDeDistribucion, " + DbSchema + ".Gv_EnumTipoDeDistribucion.StrValue AS TipoDeDistribucionStr, Compra.TasaAduanera, Compra.TasaDolar, Compra.ValorUT, Compra.TotalRenglones, Compra.TotalOtrosGastos, Compra.TotalCompra");
            SQL.AppendLine(", Compra.Comentarios, Compra.StatusCompra, " + DbSchema + ".Gv_EnumStatusCompra.StrValue AS StatusCompraStr, Compra.TipoDeCompra, " + DbSchema + ".Gv_EnumTipoCompra.StrValue AS TipoDeCompraStr, Compra.FechaDeAnulacion");
            SQL.AppendLine(", OrdenDeCompra.Consecutivo AS ConsecutivoOrdenDeCompra, ISNULL(OrdenDeCompra.Numero,'') AS NumeroDeOrdenDeCompra, Compra.NoFacturaNotaEntrega, Compra.TipoDeCompraParaCxP, " + DbSchema + ".Gv_EnumTipoOrdenDeCompra.StrValue AS TipoDeCompraParaCxPStr");
            SQL.AppendLine(", Compra.NombreOperador, Compra.FechaUltimaModificacion");
            SQL.AppendLine(", Adm.Proveedor.codigoProveedor AS CodigoProveedor");
            SQL.AppendLine(", Adm.Proveedor.nombreProveedor AS NombreProveedor");
            SQL.AppendLine(", Saw.Almacen.Codigo AS CodigoAlmacen");
            SQL.AppendLine(", Compra.fldTimeStamp, CAST(Compra.fldTimeStamp AS bigint) AS fldTimeStampBigint");            
            SQL.AppendLine("FROM " + DbSchema + ".Compra");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoDeDistribucion");
            SQL.AppendLine("ON " + DbSchema + ".Compra.TipoDeDistribucion COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDeDistribucion.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumStatusCompra");
            SQL.AppendLine("ON " + DbSchema + ".Compra.StatusCompra COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumStatusCompra.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoCompra");
            SQL.AppendLine("ON " + DbSchema + ".Compra.TipoDeCompra COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoCompra.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoOrdenDeCompra");
            SQL.AppendLine("ON " + DbSchema + ".Compra.TipoDeCompraParaCxP COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoOrdenDeCompra.DbValue");
            SQL.AppendLine("INNER JOIN Adm.Proveedor ON  " + DbSchema + ".Compra.ConsecutivoProveedor = Adm.Proveedor.consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Compra.ConsecutivoCompania = Adm.Proveedor.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN Saw.Almacen ON  " + DbSchema + ".Compra.ConsecutivoAlmacen = Saw.Almacen.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Compra.ConsecutivoCompania = Saw.Almacen.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN dbo.Moneda ON  " + DbSchema + ".Compra.CodigoMoneda = dbo.Moneda.Codigo");
            SQL.AppendLine("LEFT OUTER JOIN " + DbSchema + ".OrdenDeCompra ON  " + DbSchema + ".Compra.ConsecutivoOrdenDeCompra = " + DbSchema + ".OrdenDeCompra.consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Compra.ConsecutivoCompania = " + DbSchema + ".OrdenDeCompra.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@Serie" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@Numero" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@ConsecutivoProveedor" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@Moneda" + InsSql.VarCharTypeForDb(80) + " = '',");
            SQL.AppendLine("@CodigoMoneda" + InsSql.VarCharTypeForDb(4) + ",");
            SQL.AppendLine("@CambioABolivares" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@GenerarCXP" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@UsaSeguro" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@TipoDeDistribucion" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@TasaAduanera" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@TasaDolar" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@ValorUT" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@TotalRenglones" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@TotalOtrosGastos" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@TotalCompra" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@Comentarios" + InsSql.VarCharTypeForDb(255) + " = '',");
            SQL.AppendLine("@StatusCompra" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@TipoDeCompra" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@FechaDeAnulacion" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@ConsecutivoOrdenDeCompra" + InsSql.NumericTypeForDb(10,0) + " = 0,");
            SQL.AppendLine("@NoFacturaNotaEntrega" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@TipoDeCompraParaCxP" + InsSql.CharTypeForDb(1) + " = '0',");
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
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".Compra(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            Serie,");
            SQL.AppendLine("            Numero,");
            SQL.AppendLine("            Fecha,");
            SQL.AppendLine("            ConsecutivoProveedor,");
            SQL.AppendLine("            ConsecutivoAlmacen,");
            SQL.AppendLine("            Moneda,");
            SQL.AppendLine("            CodigoMoneda,");
            SQL.AppendLine("            CambioABolivares,");
            SQL.AppendLine("            GenerarCXP,");
            SQL.AppendLine("            UsaSeguro,");
            SQL.AppendLine("            TipoDeDistribucion,");
            SQL.AppendLine("            TasaAduanera,");
            SQL.AppendLine("            TasaDolar,");
            SQL.AppendLine("            ValorUT,");
            SQL.AppendLine("            TotalRenglones,");
            SQL.AppendLine("            TotalOtrosGastos,");
            SQL.AppendLine("            TotalCompra,");
            SQL.AppendLine("            Comentarios,");
            SQL.AppendLine("            StatusCompra,");
            SQL.AppendLine("            TipoDeCompra,");
            SQL.AppendLine("            FechaDeAnulacion,");
            SQL.AppendLine("            ConsecutivoOrdenDeCompra,");
            SQL.AppendLine("            NoFacturaNotaEntrega,");
            SQL.AppendLine("            TipoDeCompraParaCxP,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @Serie,");
            SQL.AppendLine("            @Numero,");
            SQL.AppendLine("            @Fecha,");
            SQL.AppendLine("            @ConsecutivoProveedor,");
            SQL.AppendLine("            @ConsecutivoAlmacen,");
            SQL.AppendLine("            @Moneda,");
            SQL.AppendLine("            @CodigoMoneda,");
            SQL.AppendLine("            @CambioABolivares,");
            SQL.AppendLine("            @GenerarCXP,");
            SQL.AppendLine("            @UsaSeguro,");
            SQL.AppendLine("            @TipoDeDistribucion,");
            SQL.AppendLine("            @TasaAduanera,");
            SQL.AppendLine("            @TasaDolar,");
            SQL.AppendLine("            @ValorUT,");
            SQL.AppendLine("            @TotalRenglones,");
            SQL.AppendLine("            @TotalOtrosGastos,");
            SQL.AppendLine("            @TotalCompra,");
            SQL.AppendLine("            @Comentarios,");
            SQL.AppendLine("            @StatusCompra,");
            SQL.AppendLine("            @TipoDeCompra,");
            SQL.AppendLine("            @FechaDeAnulacion,");
            SQL.AppendLine("            @ConsecutivoOrdenDeCompra,");
            SQL.AppendLine("            @NoFacturaNotaEntrega,");
            SQL.AppendLine("            @TipoDeCompraParaCxP,");
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
            SQL.AppendLine("@Serie" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@Numero" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@ConsecutivoProveedor" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@ConsecutivoAlmacen" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@Moneda" + InsSql.VarCharTypeForDb(80) + ",");
            SQL.AppendLine("@CodigoMoneda" + InsSql.VarCharTypeForDb(4) + ",");
            SQL.AppendLine("@CambioABolivares" + InsSql.DecimalTypeForDb(25,4) + ",");
            SQL.AppendLine("@GenerarCXP" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@UsaSeguro" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@TipoDeDistribucion" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@TasaAduanera" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@TasaDolar" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@ValorUT" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@TotalRenglones" + InsSql.DecimalTypeForDb(25,4) + ",");
            SQL.AppendLine("@TotalOtrosGastos" + InsSql.DecimalTypeForDb(25,4) + ",");
            SQL.AppendLine("@TotalCompra" + InsSql.DecimalTypeForDb(25,4) + ",");
            SQL.AppendLine("@Comentarios" + InsSql.VarCharTypeForDb(255) + ",");
            SQL.AppendLine("@StatusCompra" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@TipoDeCompra" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@FechaDeAnulacion" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@ConsecutivoOrdenDeCompra" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@NoFacturaNotaEntrega" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@TipoDeCompraParaCxP" + InsSql.CharTypeForDb(1) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Compra WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Compra WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_CompraCanBeUpdated @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".Compra");
            SQL.AppendLine("            SET Serie = @Serie,");
            SQL.AppendLine("               Numero = @Numero,");
            SQL.AppendLine("               Fecha = @Fecha,");
            SQL.AppendLine("               ConsecutivoProveedor = @ConsecutivoProveedor,");
            SQL.AppendLine("               ConsecutivoAlmacen = @ConsecutivoAlmacen,");
            SQL.AppendLine("               Moneda = @Moneda,");
            SQL.AppendLine("               CodigoMoneda = @CodigoMoneda,");
            SQL.AppendLine("               CambioABolivares = @CambioABolivares,");
            SQL.AppendLine("               GenerarCXP = @GenerarCXP,");
            SQL.AppendLine("               UsaSeguro = @UsaSeguro,");
            SQL.AppendLine("               TipoDeDistribucion = @TipoDeDistribucion,");
            SQL.AppendLine("               TasaAduanera = @TasaAduanera,");
            SQL.AppendLine("               ValorUT = @ValorUT,");
            SQL.AppendLine("               TotalRenglones = @TotalRenglones,");
            SQL.AppendLine("               TotalOtrosGastos = @TotalOtrosGastos,");
            SQL.AppendLine("               TotalCompra = @TotalCompra,");
            SQL.AppendLine("               Comentarios = @Comentarios,");
            SQL.AppendLine("               StatusCompra = @StatusCompra,");
            SQL.AppendLine("               TipoDeCompra = @TipoDeCompra,");
            SQL.AppendLine("               FechaDeAnulacion = @FechaDeAnulacion,");
            SQL.AppendLine("               ConsecutivoOrdenDeCompra = @ConsecutivoOrdenDeCompra,");
            SQL.AppendLine("               NoFacturaNotaEntrega = @NoFacturaNotaEntrega,");
            SQL.AppendLine("               TipoDeCompraParaCxP = @TipoDeCompraParaCxP,");
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
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10,0) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Compra WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Compra WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_CompraCanBeDeleted @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".Compra");
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
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10,0));
            return SQL.ToString();
        }    
        
        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         Compra.ConsecutivoCompania,");
            SQL.AppendLine("         Compra.Consecutivo,");
            SQL.AppendLine("         Compra.Serie,");
            SQL.AppendLine("         Compra.Numero,");
            SQL.AppendLine("         Compra.Fecha,");
            SQL.AppendLine("         Compra.ConsecutivoProveedor,");
            SQL.AppendLine("         Gv_Proveedor_B1.codigoProveedor AS CodigoProveedor,");
            SQL.AppendLine("         Gv_Proveedor_B1.nombreProveedor AS NombreProveedor,");
            SQL.AppendLine("         Compra.ConsecutivoAlmacen,");
            SQL.AppendLine("         Gv_Almacen_B1.Codigo AS CodigoAlmacen,");
            SQL.AppendLine("         Gv_Almacen_B1.NombreAlmacen AS NombreAlmacen,");
            SQL.AppendLine("         Compra.Moneda,");
            SQL.AppendLine("         Compra.CodigoMoneda,");
            SQL.AppendLine("         Compra.CambioABolivares,");
            SQL.AppendLine("         Compra.GenerarCXP,");
            SQL.AppendLine("         Compra.UsaSeguro,");
            SQL.AppendLine("         Compra.TipoDeDistribucion,");
            SQL.AppendLine("         Compra.TasaAduanera,");
            SQL.AppendLine("         Compra.TasaDolar,");
            SQL.AppendLine("         Compra.ValorUT,");
            SQL.AppendLine("         Compra.TotalRenglones,");
            SQL.AppendLine("         Compra.TotalOtrosGastos,");
            SQL.AppendLine("         Compra.TotalCompra,");
            SQL.AppendLine("         Compra.Comentarios,");
            SQL.AppendLine("         Compra.StatusCompra,");
            SQL.AppendLine("         Compra.TipoDeCompra,");
            SQL.AppendLine("         Compra.FechaDeAnulacion,");
            SQL.AppendLine("         Compra.ConsecutivoOrdenDeCompra,");
            SQL.AppendLine("         Compra.NoFacturaNotaEntrega,");
            SQL.AppendLine("         Compra.TipoDeCompraParaCxP,");
            SQL.AppendLine("         Compra.NombreOperador,");
            SQL.AppendLine("         Compra.FechaUltimaModificacion,");
            SQL.AppendLine("         ISNULL(OrdenDeCompra.Numero,'') AS NumeroDeOrdenDeCompra,");
            SQL.AppendLine("         CAST(Compra.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         Compra.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".Compra");
            SQL.AppendLine("             INNER JOIN Adm.Gv_Proveedor_B1 ON " + DbSchema + ".Compra.ConsecutivoProveedor = Adm.Gv_Proveedor_B1.consecutivo AND " + DbSchema + ".Compra.ConsecutivoCompania = Adm.Gv_Proveedor_B1.ConsecutivoCompania");
            SQL.AppendLine("             INNER JOIN Saw.Gv_Almacen_B1 ON " + DbSchema + ".Compra.ConsecutivoAlmacen = Saw.Gv_Almacen_B1.Consecutivo AND " + DbSchema + ".Compra.ConsecutivoCompania = Saw.Gv_Almacen_B1.ConsecutivoCompania");
            SQL.AppendLine("             INNER JOIN dbo.Gv_Moneda_B1 ON " + DbSchema + ".Compra.CodigoMoneda = Gv_Moneda_B1.Codigo");
            SQL.AppendLine("             LEFT OUTER JOIN " + DbSchema + ".OrdenDeCompra ON  " + DbSchema + ".Compra.ConsecutivoOrdenDeCompra = " + DbSchema + ".OrdenDeCompra.consecutivo AND " + DbSchema + ".Compra.ConsecutivoCompania = " + DbSchema + ".OrdenDeCompra.ConsecutivoCompania");            
            SQL.AppendLine("      WHERE Compra.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND Compra.Consecutivo = @Consecutivo");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_Compra_B1.Serie,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Compra_B1.Numero,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.codigoProveedor AS CodigoProveedor,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.nombreProveedor AS NombreProveedor,");
            SQL.AppendLine("      Saw.Gv_Almacen_B1.Codigo AS CodigoAlmacen,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Compra_B1.TipoDeDistribucionStr,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Compra_B1.StatusCompraStr,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Compra_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Compra_B1.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Compra_B1.Fecha,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Compra_B1.TipoDeDistribucion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Compra_B1.StatusCompra,"); 
            SQL.AppendLine("      " + DbSchema + ".Gv_Compra_B1.NumeroDeOrdenDeCompra ");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Compra_B1");
            SQL.AppendLine("      INNER JOIN Adm.Gv_Proveedor_B1 ON  " + DbSchema + ".Gv_Compra_B1.ConsecutivoProveedor = Adm.Gv_Proveedor_B1.consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Compra_B1.ConsecutivoCompania = Adm.Gv_Proveedor_B1.ConsecutivoCompania");
            SQL.AppendLine("      INNER JOIN Saw.Gv_Almacen_B1 ON  " + DbSchema + ".Gv_Compra_B1.ConsecutivoAlmacen = Saw.Gv_Almacen_B1.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Compra_B1.ConsecutivoCompania = Saw.Gv_Almacen_B1.ConsecutivoCompania");
            SQL.AppendLine("      INNER JOIN dbo.Gv_Moneda_B1 ON  " + DbSchema + ".Gv_Compra_B1.CodigoMoneda = Gv_Moneda_B1.Codigo");
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
            SQL.AppendLine("      " + DbSchema + ".Compra.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Compra.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Compra.Serie,");
            SQL.AppendLine("      " + DbSchema + ".Compra.Numero,");
            SQL.AppendLine("      " + DbSchema + ".Compra.Fecha,");
            SQL.AppendLine("      " + DbSchema + ".Compra.ConsecutivoProveedor,");
            SQL.AppendLine("      " + DbSchema + ".Compra.ConsecutivoAlmacen,");
            SQL.AppendLine("      " + DbSchema + ".Compra.CodigoMoneda,");
            SQL.AppendLine("      " + DbSchema + ".Compra.TipoDeDistribucion,");
            SQL.AppendLine("      " + DbSchema + ".Compra.StatusCompra");
            // SQL.AppendLine("      ," + DbSchema + ".Compra.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".Compra");
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
            bool vResult = insDbo.Create(DbSchema + ".Compra",SqlCreateTable(),false,eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas() {
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDeDistribucion",LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDeDistribucion),InsSql),true,true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoOrdenDeCompra",LibTpvCreator.SqlViewStandardEnum(typeof(eTipoOrdenDeCompra),InsSql),true,true);                        
            vResult = insVistas.Create(DbSchema + ".Gv_Compra_B1",SqlViewB1(),true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraINS",SqlSpInsParameters(),SqlSpIns(),true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraUPD",SqlSpUpdParameters(),SqlSpUpd(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDEL",SqlSpDelParameters(),SqlSpDel(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraGET",SqlSpGetParameters(),SqlSpGet(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraSCH",SqlSpSearchParameters(),SqlSpSearch(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraGetFk",SqlSpGetFKParameters(),SqlSpGetFK(),true) && vResult;            
            insSps.Dispose();
            return vResult;
        }

        public bool InstalarTabla() {
            bool vResult = false;
            if(CrearTabla()) {
                CrearVistas();
                CrearProcedimientos();
                clsCompraDetalleArticuloInventarioED insDetailComDetArtInv = new clsCompraDetalleArticuloInventarioED();
                vResult = insDetailComDetArtInv.InstalarTabla();
                clsCompraDetalleGastoED insDetailComDetGas = new clsCompraDetalleGastoED();
                vResult = vResult && insDetailComDetGas.InstalarTabla();
                clsCompraDetalleSerialRolloED insDetailComDetSerRol = new clsCompraDetalleSerialRolloED();
                vResult = vResult && insDetailComDetSerRol.InstalarTabla();
            }
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = false;
            if(insDbo.Exists(DbSchema + ".Compra",eDboType.Tabla)) {
                CrearVistas();
                CrearProcedimientos();
                vResult = new clsCompraDetalleArticuloInventarioED().InstalarVistasYSps();
                vResult = vResult && new clsCompraDetalleGastoED().InstalarVistasYSps();
                vResult = vResult && new clsCompraDetalleSerialRolloED().InstalarVistasYSps();
            }
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = false;
            LibStoredProc insSp = new LibStoredProc();
            LibViews insVista = new LibViews();
            vResult = new clsCompraDetalleArticuloInventarioED().BorrarVistasYSps();
            vResult = new clsCompraDetalleGastoED().BorrarVistasYSps() && vResult;
            vResult = new clsCompraDetalleSerialRolloED().BorrarVistasYSps() && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraINS") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_Compra_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDeDistribucion") && vResult;            
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoOrdenDeCompra") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCompraED

} //End of namespace Galac.Adm.Dal.GestionCompras

