using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.Venta;
using Galac.Comun.Ccl.SttDef;

namespace Galac.Adm.Dal.Venta {
    [LibMefDalComponentMetadata(typeof(clsRenglonFacturaED))]
    public class clsRenglonFacturaED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsRenglonFacturaED(): base(){
            DbSchema = "dbo";
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
            get { return "renglonFactura"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            //SQL.AppendLine(InsSql.CreateTable("renglonFactura", DbSchema) + " ( ");
            //SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnFacRapDetConsecutiv NOT NULL, ");
            //SQL.AppendLine("NumeroFactura" + InsSql.VarCharTypeForDb(11) + " CONSTRAINT nnFacRapDetNumeroFact NOT NULL, ");
            //SQL.AppendLine("TipoDeDocumento" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnFacRapDetTipoDeDocu NOT NULL, ");
            //SQL.AppendLine("ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnFacRapDetConsecutiv NOT NULL, ");
            //SQL.AppendLine("Articulo" + InsSql.VarCharTypeForDb(15) + " CONSTRAINT d_FacRapDetAr DEFAULT (''), ");
            //SQL.AppendLine("Descripcion" + InsSql.VarCharTypeForDb(255) + " CONSTRAINT d_FacRapDetDe DEFAULT (''), ");
            //SQL.AppendLine("CodigoVendedor1" + InsSql.VarCharTypeForDb(15) + " CONSTRAINT d_FacRapDetCoVe1 DEFAULT (''), ");
            //SQL.AppendLine("CodigoVendedor2" + InsSql.VarCharTypeForDb(15) + " CONSTRAINT d_FacRapDetCoVe2 DEFAULT (''), ");
            //SQL.AppendLine("CodigoVendedor3" + InsSql.VarCharTypeForDb(15) + " CONSTRAINT d_FacRapDetCoVe3 DEFAULT (''), ");
            //SQL.AppendLine("AlicuotaIVA" + InsSql.VarCharTypeForDb(1) + " CONSTRAINT d_FacRapDetAlIV DEFAULT (''), ");
            //SQL.AppendLine("Cantidad" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnFacRapDetCantidad NOT NULL, ");
            //SQL.AppendLine("PrecioSinIVA" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_FacRapDetPrSiIV DEFAULT (0), ");
            //SQL.AppendLine("PrecioConIVA" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_FacRapDetPrCoIV DEFAULT (0), ");
            //SQL.AppendLine("PorcentajeDescuento" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_FacRapDetPoDe DEFAULT (0), ");
            //SQL.AppendLine("TotalRenglon" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_FacRapDetToRe DEFAULT (0), ");
            //SQL.AppendLine("PorcentajeBaseImponible" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_FacRapDetPoBaIm DEFAULT (0), ");
            //SQL.AppendLine("Serial" + InsSql.VarCharTypeForDb(50) + " CONSTRAINT d_FacRapDetSe DEFAULT (''), ");
            //SQL.AppendLine("Rollo" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_FacRapDetRo DEFAULT (''), ");
            //SQL.AppendLine("PorcentajeAlicuota" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_FacRapDetPoAl DEFAULT (0), ");
            //SQL.AppendLine("CampoExtraEnRenglonFactura1" + InsSql.VarCharTypeForDb(60) + " CONSTRAINT d_FacRapDetCaExEnReFa1 DEFAULT (''), ");
            //SQL.AppendLine("CampoExtraEnRenglonFactura2" + InsSql.VarCharTypeForDb(60) + " CONSTRAINT d_FacRapDetCaExEnReFa2 DEFAULT (''), ");
            //SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            //SQL.AppendLine("CONSTRAINT p_renglonFactura PRIMARY KEY CLUSTERED");
            //SQL.AppendLine("(ConsecutivoCompania ASC, NumeroFactura ASC, TipoDeDocumento ASC, ConsecutivoRenglon ASC)");
            //SQL.AppendLine(",CONSTRAINT fk_renglonFacturaFacturaRapida FOREIGN KEY (ConsecutivoCompania, NumeroFactura, TipoDeDocumento)");
            //SQL.AppendLine("REFERENCES Adm.FacturaRapida(ConsecutivoCompania, Numero, TipoDeDocumento)");
            //SQL.AppendLine("ON DELETE CASCADE");
            //SQL.AppendLine("ON UPDATE CASCADE");
            //SQL.AppendLine(", CONSTRAINT fk_renglonFacturaArticuloInventario FOREIGN KEY (ConsecutivoCompania, Articulo)");
            //SQL.AppendLine("REFERENCES Saw.ArticuloInventario(ConsecutivoCompania, Codigo)");
            //SQL.AppendLine("ON UPDATE CASCADE");
            //SQL.AppendLine(", CONSTRAINT fk_renglonFacturaArticuloInventario FOREIGN KEY (ConsecutivoCompania, Descripcion)");
            //SQL.AppendLine("REFERENCES Saw.ArticuloInventario(ConsecutivoCompania, Descripcion)");
            //SQL.AppendLine("ON UPDATE CASCADE");
            //SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT renglonFactura.ConsecutivoCompania, renglonFactura.NumeroFactura, renglonFactura.TipoDeDocumento, " + DbSchema + ".Gv_EnumTipoDocumentoFactura.StrValue AS TipoDeDocumentoStr, renglonFactura.ConsecutivoRenglon");
            SQL.AppendLine(", renglonFactura.Articulo, renglonFactura.Descripcion, renglonFactura.AlicuotaIVA, renglonFactura.Cantidad");
            SQL.AppendLine(", renglonFactura.PrecioSinIVA, renglonFactura.PrecioConIVA, renglonFactura.PorcentajeDescuento, renglonFactura.TotalRenglon");
            SQL.AppendLine(", renglonFactura.PorcentajeBaseImponible, renglonFactura.Serial, renglonFactura.Rollo, renglonFactura.PorcentajeAlicuota");
            SQL.AppendLine(", renglonFactura.fldTimeStamp, CAST(renglonFactura.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".renglonFactura");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoDocumentoFactura");
            SQL.AppendLine("ON " + DbSchema + ".renglonFactura.TipoDeDocumento COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDocumentoFactura.DbValue");
            SQL.AppendLine("INNER JOIN ArticuloInventario ON  " + DbSchema + ".renglonFactura.Articulo = ArticuloInventario.Codigo");
            SQL.AppendLine("      AND " + DbSchema + ".renglonFactura.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroFactura" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@TipoDeDocumento" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Articulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(255) + ",");
            SQL.AppendLine("@CodigoVendedor1" + InsSql.VarCharTypeForDb(5) + " = '',");
            SQL.AppendLine("@CodigoVendedor2" + InsSql.VarCharTypeForDb(5) + " = '',");
            SQL.AppendLine("@CodigoVendedor3" + InsSql.VarCharTypeForDb(5) + " = '',");
            SQL.AppendLine("@AlicuotaIva" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PrecioSinIVA" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PrecioConIVA" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PorcentajeDescuento" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TotalRenglon" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PorcentajeBaseImponible" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@Serial" + InsSql.VarCharTypeForDb(50) + " = '0',");
            SQL.AppendLine("@Rollo" + InsSql.VarCharTypeForDb(20) + " = '0',");
            SQL.AppendLine("@PorcentajeAlicuota" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@CampoExtraEnRenglonFactura1" + InsSql.VarCharTypeForDb(60) + " = '',");
            SQL.AppendLine("@CampoExtraEnRenglonFactura2" + InsSql.VarCharTypeForDb(60) + " = ''");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".renglonFactura(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            NumeroFactura,");
            SQL.AppendLine("            TipoDeDocumento,");
            SQL.AppendLine("            ConsecutivoRenglon,");
            SQL.AppendLine("            Articulo,");
            SQL.AppendLine("            Descripcion,");
            SQL.AppendLine("            CodigoVendedor1,");
            SQL.AppendLine("            CodigoVendedor2,");
            SQL.AppendLine("            CodigoVendedor3,");
            SQL.AppendLine("            AlicuotaIva,");
            SQL.AppendLine("            Cantidad,");
            SQL.AppendLine("            PrecioSinIVA,");
            SQL.AppendLine("            PrecioConIVA,");
            SQL.AppendLine("            PorcentajeDescuento,");
            SQL.AppendLine("            TotalRenglon,");
            SQL.AppendLine("            PorcentajeBaseImponible,");
            SQL.AppendLine("            Serial,");
            SQL.AppendLine("            Rollo,");
            SQL.AppendLine("            PorcentajeAlicuota,");
            SQL.AppendLine("            CampoExtraEnRenglonFactura1,");
            SQL.AppendLine("            CampoExtraEnRenglonFactura2)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @NumeroFactura,");
            SQL.AppendLine("            @TipoDeDocumento,");
            SQL.AppendLine("            @ConsecutivoRenglon,");
            SQL.AppendLine("            @Articulo,");
            SQL.AppendLine("            @Descripcion,");
            SQL.AppendLine("            @CodigoVendedor1,");
            SQL.AppendLine("            @CodigoVendedor2,");
            SQL.AppendLine("            @CodigoVendedor3,");
            SQL.AppendLine("            @AlicuotaIva,");
            SQL.AppendLine("            @Cantidad,");
            SQL.AppendLine("            @PrecioSinIVA,");
            SQL.AppendLine("            @PrecioConIVA,");
            SQL.AppendLine("            @PorcentajeDescuento,");
            SQL.AppendLine("            @TotalRenglon,");
            SQL.AppendLine("            @PorcentajeBaseImponible,");
            SQL.AppendLine("            @Serial,");
            SQL.AppendLine("            @Rollo,");
            SQL.AppendLine("            @PorcentajeAlicuota,");
            SQL.AppendLine("            @CampoExtraEnRenglonFactura1,");
            SQL.AppendLine("            @CampoExtraEnRenglonFactura2)");
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
            SQL.AppendLine("@NumeroFactura" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@TipoDeDocumento" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Articulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(255) + ",");
            SQL.AppendLine("@CodigoVendedor1" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@CodigoVendedor2" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@CodigoVendedor3" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@AlicuotaIva" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PrecioSinIVA" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PrecioConIVA" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PorcentajeDescuento" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TotalRenglon" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PorcentajeBaseImponible" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@Serial" + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("@Rollo" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@PorcentajeAlicuota" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@CampoExtraEnRenglonFactura1" + InsSql.VarCharTypeForDb(60) + ",");
            SQL.AppendLine("@CampoExtraEnRenglonFactura2" + InsSql.VarCharTypeForDb(60) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".renglonFactura WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroFactura = @NumeroFactura AND TipoDeDocumento = @TipoDeDocumento AND ConsecutivoRenglon = @ConsecutivoRenglon)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".renglonFactura WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroFactura = @NumeroFactura AND TipoDeDocumento = @TipoDeDocumento AND ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_renglonFacturaCanBeUpdated @ConsecutivoCompania,@NumeroFactura,@TipoDeDocumento,@ConsecutivoRenglon, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".renglonFactura");
            SQL.AppendLine("            SET Articulo = @Articulo,");
            SQL.AppendLine("               Descripcion = @Descripcion,");
            SQL.AppendLine("               CodigoVendedor1 = @CodigoVendedor1,");
            SQL.AppendLine("               CodigoVendedor2 = @CodigoVendedor2,");
            SQL.AppendLine("               CodigoVendedor3 = @CodigoVendedor3,");
            SQL.AppendLine("               AlicuotaIva = @AlicuotaIva,");
            SQL.AppendLine("               Cantidad = @Cantidad,");
            SQL.AppendLine("               PrecioSinIVA = @PrecioSinIVA,");
            SQL.AppendLine("               PrecioConIVA = @PrecioConIVA,");
            SQL.AppendLine("               PorcentajeDescuento = @PorcentajeDescuento,");
            SQL.AppendLine("               TotalRenglon = @TotalRenglon,");
            SQL.AppendLine("               PorcentajeBaseImponible = @PorcentajeBaseImponible,");
            SQL.AppendLine("               Serial = @Serial,");
            SQL.AppendLine("               Rollo = @Rollo,");
            SQL.AppendLine("               PorcentajeAlicuota = @PorcentajeAlicuota,");
            SQL.AppendLine("               CampoExtraEnRenglonFactura1 = @CampoExtraEnRenglonFactura1,");
            SQL.AppendLine("               CampoExtraEnRenglonFactura2 = @CampoExtraEnRenglonFactura2");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND NumeroFactura = @NumeroFactura");
            SQL.AppendLine("               AND TipoDeDocumento = @TipoDeDocumento");
            SQL.AppendLine("               AND ConsecutivoRenglon = @ConsecutivoRenglon");
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
            SQL.AppendLine("@NumeroFactura" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@TipoDeDocumento" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".renglonFactura WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroFactura = @NumeroFactura AND TipoDeDocumento = @TipoDeDocumento AND ConsecutivoRenglon = @ConsecutivoRenglon)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".renglonFactura WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroFactura = @NumeroFactura AND TipoDeDocumento = @TipoDeDocumento AND ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_renglonFacturaCanBeDeleted @ConsecutivoCompania,@NumeroFactura,@TipoDeDocumento,@ConsecutivoRenglon, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".renglonFactura");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND NumeroFactura = @NumeroFactura");
            SQL.AppendLine("               AND TipoDeDocumento = @TipoDeDocumento");
            SQL.AppendLine("               AND ConsecutivoRenglon = @ConsecutivoRenglon");
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
            SQL.AppendLine("@NumeroFactura" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@TipoDeDocumento" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         NumeroFactura,");
            SQL.AppendLine("         TipoDeDocumento,");
            SQL.AppendLine("         ConsecutivoRenglon,");
            SQL.AppendLine("         Articulo,");
            SQL.AppendLine("         Descripcion,");
            SQL.AppendLine("         CodigoVendedor1,");
            SQL.AppendLine("         CodigoVendedor2,");
            SQL.AppendLine("         CodigoVendedor3,");
            SQL.AppendLine("         AlicuotaIVA,");
            SQL.AppendLine("         Cantidad,");
            SQL.AppendLine("         PrecioSinIVA,");
            SQL.AppendLine("         PrecioConIVA,");
            SQL.AppendLine("         PorcentajeDescuento,");
            SQL.AppendLine("         TotalRenglon,");
            SQL.AppendLine("         PorcentajeBaseImponible,");
            SQL.AppendLine("         Serial,");
            SQL.AppendLine("         Rollo,");
            SQL.AppendLine("         PorcentajeAlicuota,");
            SQL.AppendLine("         CampoExtraEnRenglonFactura1,");
            SQL.AppendLine("         CampoExtraEnRenglonFactura2");
            SQL.AppendLine("      FROM " + DbSchema + ".renglonFactura");
            SQL.AppendLine("      WHERE renglonFactura.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND renglonFactura.NumeroFactura = @NumeroFactura");
            SQL.AppendLine("         AND renglonFactura.TipoDeDocumento = @TipoDeDocumento");
            SQL.AppendLine("         AND renglonFactura.ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroFactura" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@TipoDeDocumento" + InsSql.CharTypeForDb(1));
            return SQL.ToString();
        }

        private string SqlSpSelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SELECT ");
            SQL.AppendLine("        ConsecutivoCompania,");
            SQL.AppendLine("        NumeroFactura,");
            SQL.AppendLine("        TipoDeDocumento,");
            SQL.AppendLine("        ConsecutivoRenglon,");
            SQL.AppendLine("        Articulo,");
            SQL.AppendLine("        Descripcion,");
            SQL.AppendLine("        CodigoVendedor1,");
            SQL.AppendLine("        CodigoVendedor2,");
            SQL.AppendLine("        CodigoVendedor3,");
            SQL.AppendLine("        AlicuotaIva,");
            SQL.AppendLine("        Cantidad,");
            SQL.AppendLine("        PrecioSinIVA,");
            SQL.AppendLine("        PrecioConIVA,");
            SQL.AppendLine("        PorcentajeDescuento,");
            SQL.AppendLine("        TotalRenglon,");
            SQL.AppendLine("        PorcentajeBaseImponible,");
            SQL.AppendLine("        Serial,");
            SQL.AppendLine("        Rollo,");
            SQL.AppendLine("        PorcentajeAlicuota,");
            SQL.AppendLine("        CampoExtraEnRenglonFactura1,");
            SQL.AppendLine("        CampoExtraEnRenglonFactura2,");
            SQL.AppendLine("        fldTimeStamp");
            SQL.AppendLine("    FROM renglonFactura");
            SQL.AppendLine(" 	WHERE TipoDeDocumento = @TipoDeDocumento");
            SQL.AppendLine(" 	AND NumeroFactura = @NumeroFactura");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroFactura" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@TipoDeDocumento" + InsSql.CharTypeForDb(1));
            return SQL.ToString();
        }

        private string SqlSpDelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	DELETE FROM renglonFactura");
            SQL.AppendLine(" 	WHERE TipoDeDocumento = @TipoDeDocumento");
            SQL.AppendLine(" 	AND NumeroFactura = @NumeroFactura");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInsDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroFactura" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@TipoDeDocumento" + InsSql.CharTypeForDb(1) + ",");
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
            SQL.AppendLine("	    EXEC dbo.Gp_renglonFacturaRapidaDelDet @ConsecutivoCompania = @ConsecutivoCompania, @NumeroFactura = @NumeroFactura,  @TipoDeDocumento = @TipoDeDocumento");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO dbo.renglonFactura(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        NumeroFactura,");
			SQL.AppendLine("	        TipoDeDocumento,");
			SQL.AppendLine("	        ConsecutivoRenglon,");
			SQL.AppendLine("	        Articulo,");
			SQL.AppendLine("	        Descripcion,");
			SQL.AppendLine("	        CodigoVendedor1,");
			SQL.AppendLine("	        CodigoVendedor2,");
			SQL.AppendLine("	        CodigoVendedor3,");
			SQL.AppendLine("	        AlicuotaIva,");
			SQL.AppendLine("	        Cantidad,");
			SQL.AppendLine("	        PrecioSinIVA,");
			SQL.AppendLine("	        PrecioConIVA,");
			SQL.AppendLine("	        PorcentajeDescuento,");
			SQL.AppendLine("	        TotalRenglon,");
			SQL.AppendLine("	        PorcentajeBaseImponible,");
			SQL.AppendLine("	        Serial,");
			SQL.AppendLine("	        Rollo,");
			SQL.AppendLine("	        PorcentajeAlicuota,");
			SQL.AppendLine("	        CampoExtraEnRenglonFactura1,");
			SQL.AppendLine("	        CampoExtraEnRenglonFactura2)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @NumeroFactura,");
			SQL.AppendLine("	        TipoDeDocumento,");
			SQL.AppendLine("	        ConsecutivoRenglon,");
			SQL.AppendLine("	        Articulo,");
			SQL.AppendLine("	        Descripcion,");
			SQL.AppendLine("	        CodigoVendedor1,");
			SQL.AppendLine("	        CodigoVendedor2,");
			SQL.AppendLine("	        CodigoVendedor3,");
			SQL.AppendLine("	        AlicuotaIva,");
			SQL.AppendLine("	        Cantidad,");
			SQL.AppendLine("	        PrecioSinIVA,");
			SQL.AppendLine("	        PrecioConIVA,");
			SQL.AppendLine("	        PorcentajeDescuento,");
			SQL.AppendLine("	        TotalRenglon,");
			SQL.AppendLine("	        PorcentajeBaseImponible,");
			SQL.AppendLine("	        Serial,");
			SQL.AppendLine("	        Rollo,");
			SQL.AppendLine("	        PorcentajeAlicuota,");
			SQL.AppendLine("	        CampoExtraEnRenglonFactura1,");
			SQL.AppendLine("	        CampoExtraEnRenglonFactura2");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDatarenglonFactura/GpDetailrenglonFactura',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        TipoDeDocumento " + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("	        ConsecutivoRenglon " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        Articulo " + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("	        Descripcion " + InsSql.VarCharTypeForDb(255) + ",");
            SQL.AppendLine("	        CodigoVendedor1 " + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("	        CodigoVendedor2 " + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("	        CodigoVendedor3 " + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("	        AlicuotaIva " + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("	        Cantidad " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        PrecioSinIVA " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        PrecioConIVA " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        PorcentajeDescuento " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        TotalRenglon " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        PorcentajeBaseImponible " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        Serial " + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("	        Rollo " + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("	        PorcentajeAlicuota " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        CampoExtraEnRenglonFactura1 " + InsSql.VarCharTypeForDb(60) + ",");
            SQL.AppendLine("	        CampoExtraEnRenglonFactura2 " + InsSql.VarCharTypeForDb(60) + ") AS XmlDocDetailOfFacturaRapida");
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
            //bool vResult = insDbo.Create(DbSchema + ".renglonFactura", SqlCreateTable(), false, eDboType.Tabla);
            return true;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDocumentoFactura", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDocumentoFactura), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_renglonFacturaRapida_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_renglonFacturaRapidaINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_renglonFacturaRapidaUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_renglonFacturaRapidaDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_renglonFacturaRapidaGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_renglonFacturaRapidaSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_renglonFacturaRapidaDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_renglonFacturaRapidaINSDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
            insSps.Dispose();
            return vResult;
        }

        public bool InstalarTabla() {
            bool vResult = false;
            //if (CrearTabla()) {
                CrearVistas();
                CrearProcedimientos();
                vResult = true;
            //}
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = false;
            //if (insDbo.Exists(DbSchema + ".renglonFactura", eDboType.Tabla)) {
                CrearVistas();
                CrearProcedimientos();
                vResult = true;
            //}
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = false;
            LibStoredProc insSp = new LibStoredProc();
            LibViews insVista = new LibViews();
            vResult = insSp.Drop(DbSchema + ".Gp_renglonFacturaRapidaINS");
            vResult = insSp.Drop(DbSchema + ".Gp_renglonFacturaRapidaUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_renglonFacturaRapidaDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_renglonFacturaRapidaGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_renglonFacturaRapidaInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_renglonFacturaRapidaDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_renglonFacturaRapidaSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_renglonFacturaRapida_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDocumentoFactura") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsRenglonFacturaED

} //End of namespace Galac.Adm.Dal.Venta

