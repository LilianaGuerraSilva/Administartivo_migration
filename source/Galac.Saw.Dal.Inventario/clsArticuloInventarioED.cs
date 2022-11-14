using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.Base;

namespace Galac.Saw.Dal.Inventario {
    [LibMefDalComponentMetadata(typeof(clsArticuloInventarioED))]
    public class clsArticuloInventarioED:LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsArticuloInventarioED() : base() {
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
            get { return "ArticuloInventario"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        //private string SqlCreateTable() {
        //    StringBuilder SQL = new StringBuilder();
        //    SQL.AppendLine(InsSql.CreateTable("ArticuloInventario", DbSchema) + " ( ");
        //    SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnArtInvConsecutiv NOT NULL, ");
        //    SQL.AppendLine("Codigo" + InsSql.VarCharTypeForDb(15) + " CONSTRAINT nnArtInvCodigo NOT NULL, ");
        //    SQL.AppendLine("Descripcion" + InsSql.VarCharTypeForDb(255) + " CONSTRAINT nnArtInvDescripcio NOT NULL, ");
        //    SQL.AppendLine("LineaDeProducto" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_ArtInvLiDePr DEFAULT (''), ");
        //    SQL.AppendLine("StatusdelArticulo" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_ArtInvStAr DEFAULT ('0'), ");
        //    SQL.AppendLine("TipoDeArticulo" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_ArtInvTiDeAr DEFAULT ('0'), ");
        //    SQL.AppendLine("AlicuotaIVA" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_ArtInvAlIV DEFAULT ('0'), ");
        //    SQL.AppendLine("PrecioSinIVA" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_ArtInvPrSiIV DEFAULT (0), ");
        //    SQL.AppendLine("PrecioConIVA" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_ArtInvPrCoIV DEFAULT (0), ");
        //    SQL.AppendLine("Existencia" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_ArtInvEx DEFAULT (0), ");
        //    SQL.AppendLine("Categoria" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_ArtInvCa DEFAULT (''), ");
        //    SQL.AppendLine("Marca" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_ArtInvMa DEFAULT (''), ");
        //    SQL.AppendLine("FechaDeVencimiento" + InsSql.DateTypeForDb() + " CONSTRAINT d_ArtInvFeDeVe DEFAULT (''), ");
        //    SQL.AppendLine("UnidadDeVenta" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_ArtInvUnDeVe DEFAULT (''), ");
        //    SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
        //    SQL.AppendLine("TipoArticuloInv" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnArtInvTipoArticu NOT NULL, ");
        //    SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
        //    SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
        //    SQL.AppendLine("CONSTRAINT p_ArticuloInventario PRIMARY KEY CLUSTERED");
        //    SQL.AppendLine("(ConsecutivoCompania ASC, Codigo ASC)");
        //    SQL.AppendLine(", CONSTRAINT fk_ArticuloInventarioLineaDeProducto FOREIGN KEY (ConsecutivoCompania, LineaDeProducto)");
        //    SQL.AppendLine("REFERENCES dbo.LineaDeProducto(ConsecutivoCompania, Nombre)");
        //    SQL.AppendLine("ON UPDATE CASCADE");
        //    SQL.AppendLine(", CONSTRAINT fk_ArticuloInventarioCategoria FOREIGN KEY (ConsecutivoCompania, Categoria)");
        //    SQL.AppendLine("REFERENCES Saw.Categoria(ConsecutivoCompania, Descripcion)");
        //    SQL.AppendLine("ON UPDATE CASCADE");
        //    SQL.AppendLine(", CONSTRAINT fk_ArticuloInventarioUnidadDeVenta FOREIGN KEY (UnidadDeVenta)");
        //    SQL.AppendLine("REFERENCES Saw.UnidadDeVenta(Nombre)");
        //    SQL.AppendLine("ON UPDATE CASCADE");
        //    SQL.AppendLine(")");
        //    return SQL.ToString();
        //}

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(" SELECT  ArticuloInventario.Codigo + ISNULL(ExistenciaPorGrupo.CodigoColor, '') + ISNULL(ExistenciaPorGrupo.CodigoTalla, '') AS CodigoDelArticulo, ");
            SQL.AppendLine(" ArticuloInventario.Descripcion + (CASE WHEN (Color.DescripcionColor IS NULL) THEN '' ELSE Color.DescripcionColor END) ");
            SQL.AppendLine("+ (CASE WHEN (Talla.DescripcionTalla IS NULL) THEN '' ELSE Talla.DescripcionTalla END) AS Descripcion");
            SQL.AppendLine(", ArticuloInventario.LineaDeProducto");
            SQL.AppendLine(", ArticuloInventario.PrecioSinIVA  ");
            SQL.AppendLine(", ArticuloInventario.PrecioConIVA ");
            SQL.AppendLine(", ArticuloInventario.PrecioSinIva2 ");
            SQL.AppendLine(", ArticuloInventario.PrecioConIva2 ");
            SQL.AppendLine(", ArticuloInventario.PrecioSinIva3 ");
            SQL.AppendLine(", ArticuloInventario.PrecioConIva3 ");
            SQL.AppendLine(", ArticuloInventario.PrecioSinIva4 ");
            SQL.AppendLine(", ArticuloInventario.PrecioConIva4 ");
            SQL.AppendLine(", ISNULL(RenglonExistenciaAlmacen.CodigoSerial,0) AS Serial");
            SQL.AppendLine(", ISNULL(RenglonExistenciaAlmacen.CodigoRollo,0) AS Rollo ");
            SQL.AppendLine(", ArticuloInventario.AlicuotaIva");
            SQL.AppendLine(", ISNULL(ExistenciaPorGrupo.CodigoGrupo,'') AS CodigoGrupo");
            SQL.AppendLine(", ArticuloInventario.TipoArticuloInv");
            SQL.AppendLine(", ArticuloInventario.Codigo");
            SQL.AppendLine(", ArticuloInventario.Categoria");
            SQL.AppendLine(", ArticuloInventario.UnidadDeVenta");
            SQL.AppendLine(", ArticuloInventario.ConsecutivoCompania");
            SQL.AppendLine(", ArticuloInventario.PorcentajeBaseImponible");
            SQL.AppendLine(", ArticuloInventario.UsaBalanza");
            SQL.AppendLine(", ArticuloInventario.Peso");
            SQL.AppendLine(", ISNULL(CamposMonedaExtranjera.MePrecioSinIVA,0) AS MePrecioSinIva");
            SQL.AppendLine(", ISNULL(CamposMonedaExtranjera.MePrecioConIVA,0) AS MePrecioConIva");
            SQL.AppendLine(", ISNULL(CamposMonedaExtranjera.MePrecioSinIVA2,0) AS MePrecioSinIva2");
            SQL.AppendLine(", ISNULL(CamposMonedaExtranjera.MePrecioConIVA2,0) AS MePrecioConIva2");
            SQL.AppendLine(", ISNULL(CamposMonedaExtranjera.MePrecioSinIVA3,0) AS MePrecioSinIva3");
            SQL.AppendLine(", ISNULL(CamposMonedaExtranjera.MePrecioConIVA3,0) AS MePrecioConIva3");
            SQL.AppendLine(", ISNULL(CamposMonedaExtranjera.MePrecioSinIVA4,0) AS MePrecioSinIva4");
            SQL.AppendLine(", ISNULL(CamposMonedaExtranjera.MePrecioConIVA4,0) AS MePrecioConIva4");


            SQL.AppendLine(", (CASE WHEN ArticuloInventario.TipoArticuloInv = '1' THEN  (ISNULL(SUM(ExistenciaPorAlmacen.Cantidad), 0) + ISNULL(SUM(ExistenciaPorGrupo.Existencia), 0) + ISNULL(SUM(RenglonExistenciaAlmacen.Cantidad), 0) ");
            SQL.AppendLine("- SUM(ArticuloInventario.CantArtReservado))");
            SQL.AppendLine("ELSE ");
            SQL.AppendLine("(CASE WHEN  (RenglonExistenciaAlmacen.CodigoSerial IS NULL )  OR  (RenglonExistenciaAlmacen.CodigoRollo IS NULL ) ");
            SQL.AppendLine(" OR  RenglonExistenciaAlmacen.CodigoSerial = '' THEN ISNULL(SUM(ExistenciaPorAlmacen.Cantidad),0)");
            SQL.AppendLine("ELSE ISNULL(SUM(RenglonExistenciaAlmacen.Cantidad),0) END )  END) AS Existencia");

            SQL.AppendLine(", Gv_EnumTipoDeArticulo.StrValue AS TipoDeArticuloStr");
            SQL.AppendLine(", ArticuloInventario.StatusdelArticulo");
            SQL.AppendLine(", ArticuloInventario.TipoDeArticulo");

            SQL.AppendLine(", ArticuloInventario.ArancelesCodigo");
            SQL.AppendLine(", ArticuloInventario.TipoDeMercanciaProduccion ");
            SQL.AppendLine(" FROM ArticuloInventario ");
            SQL.AppendLine(" LEFT JOIN ExistenciaPorAlmacen ON (ExistenciaPorAlmacen.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania ");
            SQL.AppendLine(" AND ExistenciaPorAlmacen.CodigoArticulo = ArticuloInventario.Codigo) 	");

            SQL.AppendLine(" LEFT JOIN RenglonExistenciaAlmacen  ON (RenglonExistenciaAlmacen.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania ");
            SQL.AppendLine(" AND RenglonExistenciaAlmacen.CodigoArticulo = ArticuloInventario.Codigo)");

            SQL.AppendLine(" LEFT JOIN ExistenciaPorGrupo ON (ArticuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo ");
            SQL.AppendLine(" AND ArticuloInventario.CodigoGrupo = (CASE WHEN ExistenciaPorGrupo.CodigoGrupo = '0' THEN '' ELSE ExistenciaPorGrupo.CodigoGrupo END) ");
            SQL.AppendLine(" AND ArticuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania )");

            SQL.AppendLine(" LEFT JOIN Saw.Color ON (Saw.Color.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania ");
            SQL.AppendLine(" AND Saw.Color.CodigoColor = ExistenciaPorGrupo.CodigoColor )");

            SQL.AppendLine(" LEFT JOIN Saw.Talla ON( Saw.Talla.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania ");
            SQL.AppendLine(" AND Saw.Talla.CodigoTalla = ExistenciaPorGrupo.CodigoTalla )");

            SQL.AppendLine(" LEFT JOIN dbo.CamposMonedaExtranjera ON( dbo.CamposMonedaExtranjera.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania ");
            SQL.AppendLine(" AND dbo.CamposMonedaExtranjera.Codigo = ArticuloInventario.Codigo )");

            SQL.AppendLine(" INNER JOIN Gv_EnumStatusArticulo");
            SQL.AppendLine(" ON ArticuloInventario.StatusdelArticulo COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = Gv_EnumStatusArticulo.DbValue");
            SQL.AppendLine(" INNER JOIN Gv_EnumTipoDeArticulo");
            SQL.AppendLine(" ON ArticuloInventario.TipoDeArticulo COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = Gv_EnumTipoDeArticulo.DbValue");
            SQL.AppendLine(" INNER JOIN Gv_EnumTipoDeAlicuota");
            SQL.AppendLine(" ON ArticuloInventario.AlicuotaIVA COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = Gv_EnumTipoDeAlicuota.DbValue");
            SQL.AppendLine(" INNER JOIN Gv_EnumTipoArticuloInv");
            SQL.AppendLine(" ON ArticuloInventario.TipoArticuloInv COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = Gv_EnumTipoArticuloInv.DbValue");

            SQL.AppendLine(" WHERE ArticuloInventario.Codigo NOT  IN ");
            SQL.AppendLine(" ( 'RD_AliExenta  @' , 'RD_AliGeneral @' , 'RD_AliReducida@', 'RD_AliExtendida', 'RD_ComXPorcDeAlmacen @'");
            SQL.AppendLine("  , 'RD_AliExentaNC @',  'RD_AliGeneralNC @', 'RD_AliReducidaNC @', 'RD_AliExtendidaNC @', 'ND-NC IGTF @') ");
            SQL.AppendLine(" GROUP BY ArticuloInventario.Codigo, ExistenciaPorGrupo.CodigoColor, ExistenciaPorGrupo.CodigoTalla, ArticuloInventario.Descripcion");
            SQL.AppendLine(" , Saw.Color.DescripcionColor, Saw.Talla.DescripcionTalla, ArticuloInventario.LineaDeProducto ");
            SQL.AppendLine(" , ArticuloInventario.ConsecutivoCompania, ArticuloInventario.PrecioSinIVA, ArticuloInventario.PrecioConIVA  ");
            SQL.AppendLine(" , ArticuloInventario.PrecioSinIVA2, ArticuloInventario.PrecioConIVA2, ArticuloInventario.PrecioSinIVA3, ArticuloInventario.PrecioConIVA3");
            SQL.AppendLine(" , ArticuloInventario.PrecioSinIVA4, ArticuloInventario.PrecioConIVA4 ");
            SQL.AppendLine(" ,RenglonExistenciaAlmacen.CodigoSerial, RenglonExistenciaAlmacen.CodigoRollo, ArticuloInventario.AlicuotaIva, ExistenciaPorGrupo.CodigoGrupo");
            SQL.AppendLine(" , ArticuloInventario.TipoArticuloInv, ArticuloInventario.PorcentajeBaseImponible");
            SQL.AppendLine(" , ArticuloInventario.Categoria, ArticuloInventario.UnidadDeVenta, Gv_EnumTipoDeArticulo.StrValue, ArticuloInventario.StatusdelArticulo");
            SQL.AppendLine(" , ArticuloInventario.TipoDeArticulo, ArticuloInventario.UsaBalanza");
            SQL.AppendLine(" , ArticuloInventario.Peso, ArticuloInventario.ArancelesCodigo, ArticuloInventario.TipoDeMercanciaProduccion, MePrecioSinIva, MePrecioConIva, MePrecioSinIva2");
            SQL.AppendLine(", MePrecioConIva2, MePrecioSinIva3, MePrecioConIva3, MePrecioSinIva4, MePrecioConIva4");
            return SQL.ToString();

        }

        //private string SqlSpInsParameters() {
        //    StringBuilder SQL = new StringBuilder();
        //    SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
        //    SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
        //    SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(15) + ",");
        //    SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(255) + " = '',");
        //    SQL.AppendLine("@LineaDeProducto" + InsSql.VarCharTypeForDb(20) + ",");
        //    SQL.AppendLine("@StatusdelArticulo" + InsSql.CharTypeForDb(1) + " = '0',");
        //    SQL.AppendLine("@TipoDeArticulo" + InsSql.CharTypeForDb(1) + " = '0',");
        //    SQL.AppendLine("@AlicuotaIVA" + InsSql.CharTypeForDb(1) + " = '0',");
        //    SQL.AppendLine("@PrecioSinIVA" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
        //    SQL.AppendLine("@PrecioConIVA" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
        //    SQL.AppendLine("@Existencia" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
        //    SQL.AppendLine("@Categoria" + InsSql.VarCharTypeForDb(20) + ",");
        //    SQL.AppendLine("@Marca" + InsSql.VarCharTypeForDb(30) + " = '',");
        //    SQL.AppendLine("@FechaDeVencimiento" + InsSql.DateTypeForDb() + " = '01/01/1900',");
        //    SQL.AppendLine("@UnidadDeVenta" + InsSql.VarCharTypeForDb(20) + ",");
        //    SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + " = '',");
        //    SQL.AppendLine("@TipoArticuloInv" + InsSql.CharTypeForDb(1) + " = '0',");
        //    SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + " = '01/01/1900'");
        //    return SQL.ToString();
        //}

        //private string SqlSpIns() {
        //    StringBuilder SQL = new StringBuilder();
        //    SQL.AppendLine("BEGIN");
        //    SQL.AppendLine("   SET NOCOUNT ON;");
        //    SQL.AppendLine("   SET DATEFORMAT @DateFormat");
        //    SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
        //    SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
        //    SQL.AppendLine("	BEGIN");
        //    SQL.AppendLine("        BEGIN TRAN");
        //    SQL.AppendLine("            INSERT INTO " + DbSchema + ".ArticuloInventario(");
        //    SQL.AppendLine("            ConsecutivoCompania,");
        //    SQL.AppendLine("            Codigo,");
        //    SQL.AppendLine("            Descripcion,");
        //    SQL.AppendLine("            LineaDeProducto,");
        //    SQL.AppendLine("            StatusdelArticulo,");
        //    SQL.AppendLine("            TipoDeArticulo,");
        //    SQL.AppendLine("            AlicuotaIVA,");
        //    SQL.AppendLine("            PrecioSinIVA,");
        //    SQL.AppendLine("            PrecioConIVA,");
        //    SQL.AppendLine("            Existencia,");
        //    SQL.AppendLine("            Categoria,");
        //    SQL.AppendLine("            Marca,");
        //    SQL.AppendLine("            FechaDeVencimiento,");
        //    SQL.AppendLine("            UnidadDeVenta,");
        //    SQL.AppendLine("            NombreOperador,");
        //    SQL.AppendLine("            TipoArticuloInv,");
        //    SQL.AppendLine("            FechaUltimaModificacion)");
        //    SQL.AppendLine("            VALUES(");
        //    SQL.AppendLine("            @ConsecutivoCompania,");
        //    SQL.AppendLine("            @Codigo,");
        //    SQL.AppendLine("            @Descripcion,");
        //    SQL.AppendLine("            @LineaDeProducto,");
        //    SQL.AppendLine("            @StatusdelArticulo,");
        //    SQL.AppendLine("            @TipoDeArticulo,");
        //    SQL.AppendLine("            @AlicuotaIVA,");
        //    SQL.AppendLine("            @PrecioSinIVA,");
        //    SQL.AppendLine("            @PrecioConIVA,");
        //    SQL.AppendLine("            @Existencia,");
        //    SQL.AppendLine("            @Categoria,");
        //    SQL.AppendLine("            @Marca,");
        //    SQL.AppendLine("            @FechaDeVencimiento,");
        //    SQL.AppendLine("            @UnidadDeVenta,");
        //    SQL.AppendLine("            @NombreOperador,");
        //    SQL.AppendLine("            @TipoArticuloInv,");
        //    SQL.AppendLine("            @FechaUltimaModificacion)");
        //    SQL.AppendLine("            SET @ReturnValue = @@ROWCOUNT");
        //    SQL.AppendLine("        COMMIT TRAN");
        //    SQL.AppendLine("        RETURN @ReturnValue ");
        //    SQL.AppendLine("	END");
        //    SQL.AppendLine("	ELSE");
        //    SQL.AppendLine("		RETURN -1");
        //    SQL.AppendLine("END");
        //    return SQL.ToString();
        //}

        //private string SqlSpUpdParameters() {
        //    StringBuilder SQL = new StringBuilder();
        //    SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
        //    SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
        //    SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(15) + ",");
        //    SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(255) + ",");
        //    SQL.AppendLine("@LineaDeProducto" + InsSql.VarCharTypeForDb(20) + ",");
        //    SQL.AppendLine("@StatusdelArticulo" + InsSql.CharTypeForDb(1) + ",");
        //    SQL.AppendLine("@TipoDeArticulo" + InsSql.CharTypeForDb(1) + ",");
        //    SQL.AppendLine("@AlicuotaIVA" + InsSql.CharTypeForDb(1) + ",");
        //    SQL.AppendLine("@PrecioSinIVA" + InsSql.DecimalTypeForDb(25, 4) + ",");
        //    SQL.AppendLine("@PrecioConIVA" + InsSql.DecimalTypeForDb(25, 4) + ",");
        //    SQL.AppendLine("@Existencia" + InsSql.DecimalTypeForDb(25, 4) + ",");
        //    SQL.AppendLine("@Categoria" + InsSql.VarCharTypeForDb(20) + ",");
        //    SQL.AppendLine("@Marca" + InsSql.VarCharTypeForDb(30) + ",");
        //    SQL.AppendLine("@FechaDeVencimiento" + InsSql.DateTypeForDb() + ",");
        //    SQL.AppendLine("@UnidadDeVenta" + InsSql.VarCharTypeForDb(20) + ",");
        //    SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + ",");
        //    SQL.AppendLine("@TipoArticuloInv" + InsSql.CharTypeForDb(1) + ",");
        //    SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + ",");
        //    SQL.AppendLine("@TimeStampAsInt" + InsSql.BigintTypeForDb());
        //    return SQL.ToString();
        //}

        //private string SqlSpUpd() {
        //    StringBuilder SQL = new StringBuilder();
        //    SQL.AppendLine("BEGIN");
        //    SQL.AppendLine("   SET NOCOUNT ON;");
        //    SQL.AppendLine("   SET DATEFORMAT @DateFormat");
        //    SQL.AppendLine("   DECLARE @CurrentTimeStamp timestamp");
        //    SQL.AppendLine("   DECLARE @ValidationMsg " + InsSql.VarCharTypeForDb(1500) + " --No puede ser más");
        //    SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
        //    //SQL.AppendLine("--DECLARE @CanBeChanged bit");
        //    SQL.AppendLine("   SET @ReturnValue = -1");
        //    SQL.AppendLine("   SET @ValidationMsg = ''");
        //    SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".ArticuloInventario WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @Codigo)");
        //    SQL.AppendLine("   BEGIN");
        //    SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".ArticuloInventario WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @Codigo");
        //    SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
        //    SQL.AppendLine("      BEGIN");
        //    SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_ArticuloInventarioCanBeUpdated @ConsecutivoCompania,@Codigo, @CurrentTimeStamp, @ValidationMsg out");
        //    //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
        //    //SQL.AppendLine("--BEGIN");
        //    SQL.AppendLine("         BEGIN TRAN");
        //    SQL.AppendLine("         UPDATE " + DbSchema + ".ArticuloInventario");
        //    SQL.AppendLine("            SET Descripcion = @Descripcion,");
        //    SQL.AppendLine("               LineaDeProducto = @LineaDeProducto,");
        //    SQL.AppendLine("               StatusdelArticulo = @StatusdelArticulo,");
        //    SQL.AppendLine("               TipoDeArticulo = @TipoDeArticulo,");
        //    SQL.AppendLine("               AlicuotaIVA = @AlicuotaIVA,");
        //    SQL.AppendLine("               PrecioSinIVA = @PrecioSinIVA,");
        //    SQL.AppendLine("               PrecioConIVA = @PrecioConIVA,");
        //    SQL.AppendLine("               Existencia = @Existencia,");
        //    SQL.AppendLine("               Categoria = @Categoria,");
        //    SQL.AppendLine("               Marca = @Marca,");
        //    SQL.AppendLine("               FechaDeVencimiento = @FechaDeVencimiento,");
        //    SQL.AppendLine("               UnidadDeVenta = @UnidadDeVenta,");
        //    SQL.AppendLine("               NombreOperador = @NombreOperador,");
        //    SQL.AppendLine("               TipoArticuloInv = @TipoArticuloInv,");
        //    SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
        //    SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
        //    SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
        //    SQL.AppendLine("               AND Codigo = @Codigo");
        //    SQL.AppendLine("         SET @ReturnValue = @@ROWCOUNT");
        //    SQL.AppendLine("         IF @@ERROR = 0");
        //    SQL.AppendLine("         BEGIN");
        //    SQL.AppendLine("            COMMIT TRAN");
        //    SQL.AppendLine("            IF @ReturnValue = 0");
        //    SQL.AppendLine("               RAISERROR('El registro ha sido modificado o eliminado por otro usuario', 14, 1)");
        //    SQL.AppendLine("         END");
        //    SQL.AppendLine("         ELSE");
        //    SQL.AppendLine("         BEGIN");
        //    SQL.AppendLine("            SET @ReturnValue = -1");
        //    SQL.AppendLine("            SET @ValidationMsg = 'Se ha producido un error al Modificar: ' + CAST(@@ERROR AS NVARCHAR(8))");
        //    SQL.AppendLine("            RAISERROR(@ValidationMsg, 14 ,1)");
        //    SQL.AppendLine("            ROLLBACK");
        //    SQL.AppendLine("         END");
        //    //SQL.AppendLine("--END");
        //    //SQL.AppendLine("--ELSE");
        //    //SQL.AppendLine("--	RAISERROR('El registro no puede ser modificado: %s', 14, 1, @ValidationMsg)");
        //    SQL.AppendLine("      END");
        //    SQL.AppendLine("      ELSE");
        //    SQL.AppendLine("         RAISERROR('El registro ha sido modificado o eliminado por otro usuario.', 14, 1)");
        //    SQL.AppendLine("   END");
        //    SQL.AppendLine("   ELSE");
        //    SQL.AppendLine("      RAISERROR('El registro no existe.', 14, 1)");
        //    SQL.AppendLine("   RETURN @ReturnValue");
        //    SQL.AppendLine("END");
        //    return SQL.ToString();
        //}

        //private string SqlSpDelParameters() {
        //    StringBuilder SQL = new StringBuilder();
        //    SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
        //    SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(15) + ",");
        //    SQL.AppendLine("@TimeStampAsInt" + InsSql.BigintTypeForDb());
        //    return SQL.ToString();
        //}

        //private string SqlSpDel() {
        //    StringBuilder SQL = new StringBuilder();
        //    SQL.AppendLine("BEGIN");
        //    SQL.AppendLine("   SET NOCOUNT ON;");
        //    SQL.AppendLine("   DECLARE @CurrentTimeStamp timestamp");
        //    SQL.AppendLine("   DECLARE @ValidationMsg " + InsSql.VarCharTypeForDb(1500) + " --No puede ser más");
        //    SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
        //    //SQL.AppendLine("--DECLARE @CanBeDeleted bit");
        //    SQL.AppendLine("   SET @ReturnValue = -1");
        //    SQL.AppendLine("   SET @ValidationMsg = ''");
        //    SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".ArticuloInventario WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @Codigo)");
        //    SQL.AppendLine("   BEGIN");
        //    SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".ArticuloInventario WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @Codigo");
        //    SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
        //    SQL.AppendLine("      BEGIN");
        //    SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_ArticuloInventarioCanBeDeleted @ConsecutivoCompania,@Codigo, @CurrentTimeStamp, @ValidationMsg out");
        //    //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
        //    //SQL.AppendLine("--BEGIN");
        //    SQL.AppendLine("         BEGIN TRAN");
        //    SQL.AppendLine("         DELETE FROM " + DbSchema + ".ArticuloInventario");
        //    SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
        //    SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
        //    SQL.AppendLine("               AND Codigo = @Codigo");
        //    SQL.AppendLine("         SET @ReturnValue = @@ROWCOUNT");
        //    SQL.AppendLine("         IF @@ERROR = 0");
        //    SQL.AppendLine("         BEGIN");
        //    SQL.AppendLine("            COMMIT TRAN");
        //    SQL.AppendLine("            IF @ReturnValue = 0");
        //    SQL.AppendLine("               RAISERROR('El registro ha sido modificado o eliminado por otro usuario', 14, 1)");
        //    SQL.AppendLine("         END");
        //    SQL.AppendLine("         ELSE");
        //    SQL.AppendLine("         BEGIN");
        //    SQL.AppendLine("            SET @ReturnValue = -1");
        //    SQL.AppendLine("            SET @ValidationMsg = 'Se ha producido un error al Eliminar: ' + CAST(@@ERROR AS NVARCHAR(8))");
        //    SQL.AppendLine("            RAISERROR(@ValidationMsg, 14 ,1)");
        //    SQL.AppendLine("            ROLLBACK");
        //    SQL.AppendLine("         END");
        //    //SQL.AppendLine("--END");
        //    //SQL.AppendLine("--ELSE");
        //    //SQL.AppendLine("--	RAISERROR('El registro no puede ser eliminado: %s', 14, 1, @ValidationMsg)");
        //    SQL.AppendLine("      END");
        //    SQL.AppendLine("      ELSE");
        //    SQL.AppendLine("         RAISERROR('El registro ha sido modificado o eliminado por otro usuario.', 14, 1)");
        //    SQL.AppendLine("   END");
        //    SQL.AppendLine("   ELSE");
        //    SQL.AppendLine("      RAISERROR('El registro no existe.', 14, 1)");
        //    SQL.AppendLine("   RETURN @ReturnValue");
        //    SQL.AppendLine("END");
        //    return SQL.ToString();
        //}

        //private string SqlSpGetParameters() {
        //    StringBuilder SQL = new StringBuilder();
        //    SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
        //    SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(15));
        //    return SQL.ToString();
        //}

        //private string SqlSpGet() {
        //    StringBuilder SQL = new StringBuilder();
        //    SQL.AppendLine("BEGIN");
        //    SQL.AppendLine("   SET NOCOUNT ON;");
        //    SQL.AppendLine("   SELECT ");
        //    SQL.AppendLine("         ArticuloInventario.ConsecutivoCompania,");
        //    SQL.AppendLine("         ArticuloInventario.Codigo,");
        //    SQL.AppendLine("         ArticuloInventario.Descripcion,");
        //    SQL.AppendLine("         ArticuloInventario.LineaDeProducto,");
        //    SQL.AppendLine("         ArticuloInventario.StatusdelArticulo,");
        //    SQL.AppendLine("         ArticuloInventario.TipoDeArticulo,");
        //    SQL.AppendLine("         ArticuloInventario.AlicuotaIVA,");
        //    SQL.AppendLine("         ArticuloInventario.PrecioSinIVA,");
        //    SQL.AppendLine("         ArticuloInventario.PrecioConIVA,");
        //    SQL.AppendLine("         ArticuloInventario.Existencia,");
        //    SQL.AppendLine("         ArticuloInventario.Categoria,");
        //    SQL.AppendLine("         ArticuloInventario.Marca,");
        //    SQL.AppendLine("         ArticuloInventario.FechaDeVencimiento,");
        //    SQL.AppendLine("         ArticuloInventario.UnidadDeVenta,");
        //    SQL.AppendLine("         ArticuloInventario.NombreOperador,");
        //    SQL.AppendLine("         ArticuloInventario.TipoArticuloInv,");
        //    SQL.AppendLine("         ArticuloInventario.FechaUltimaModificacion,");
        //    SQL.AppendLine("         CAST(ArticuloInventario.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
        //    SQL.AppendLine("         ArticuloInventario.fldTimeStamp");
        //    SQL.AppendLine("      FROM " + DbSchema + ".ArticuloInventario");
        //    SQL.AppendLine("             INNER JOIN dbo.Gv_LineaDeProducto_B1 ON " + DbSchema + ".ArticuloInventario.LineaDeProducto = dbo.Gv_LineaDeProducto_B1.Nombre");
        //    SQL.AppendLine("             INNER JOIN Saw.Gv_Categoria_B1 ON " + DbSchema + ".ArticuloInventario.Categoria = Saw.Gv_Categoria_B1.Descripcion");
        //    SQL.AppendLine("             INNER JOIN Saw.Gv_UnidadDeVenta_B1 ON " + DbSchema + ".ArticuloInventario.UnidadDeVenta = Saw.Gv_UnidadDeVenta_B1.Nombre");
        //    SQL.AppendLine("      WHERE ArticuloInventario.ConsecutivoCompania = @ConsecutivoCompania");
        //    SQL.AppendLine("         AND ArticuloInventario.Codigo = @Codigo");
        //    SQL.AppendLine("   RETURN @@ROWCOUNT");
        //    SQL.AppendLine("END");
        //    return SQL.ToString();
        //}

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
            SQL.AppendLine("    Gv_ArticuloInventario_B1.Codigo, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.Descripcion,");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.LineaDeProducto,");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.TipoDeArticuloStr,");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.PrecioSinIVA, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.PrecioSinIVA2, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.PrecioSinIVA3, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.PrecioSinIVA4, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.PrecioConIVA, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.PrecioConIVA2, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.PrecioConIVA3, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.PrecioConIVA4, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.Categoria,");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.Existencia,");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.PorcentajeBaseImponible,");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.Peso,");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.UnidadDeVenta,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.ConsecutivoCompania, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.StatusdelArticulo, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.TipoDeArticulo, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.AlicuotaIVA, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.UsaBalanza, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.ArancelesCodigo, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.TipoArticuloInv, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.CodigoGrupo, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.MePrecioSinIva, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.MePrecioSinIva2, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.MePrecioSinIva3, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.MePrecioSinIva4, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.MePrecioConIva, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.MePrecioConIva2, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.MePrecioConIva3, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.MePrecioConIva4, ");
            SQL.AppendLine("    Comun.Aranceles.AdValorem, ");
            SQL.AppendLine("    Comun.Aranceles.Seguro, ");
            SQL.AppendLine("    Gv_ArticuloInventario_B1.TipoDeMercanciaProduccion ");
            SQL.AppendLine("    FROM Gv_ArticuloInventario_B1 LEFT JOIN Comun.Aranceles ON Gv_ArticuloInventario_B1.ArancelesCodigo = Comun.Aranceles.Codigo ");
            SQL.AppendLine("'   IF (NOT @SQLWhere IS NULL) AND (@SQLWhere <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' WHERE ' + @SQLWhere");
            SQL.AppendLine("   IF (NOT @SQLOrderBy IS NULL) AND (@SQLOrderBy <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' ORDER BY ' + @SQLOrderBy");
            SQL.AppendLine("   EXEC(@strSQL)");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        //private string SqlSpSearch() {
        //    StringBuilder SQL = new StringBuilder();
        //    SQL.AppendLine("BEGIN");
        //    SQL.AppendLine("   SET NOCOUNT ON;");
        //    SQL.AppendLine("   DECLARE @strSQL AS " + InsSql.VarCharTypeForDb(7000));
        //    SQL.AppendLine("   DECLARE @TopClausule AS " + InsSql.VarCharTypeForDb(10));
        //    SQL.AppendLine("   IF(@UseTopClausule = 'S') ");
        //    SQL.AppendLine("    SET @TopClausule = 'TOP 500'");
        //    SQL.AppendLine("   ELSE ");
        //    SQL.AppendLine("    SET @TopClausule = ''");
        //    SQL.AppendLine("   SET @strSQL = ");
        //    SQL.AppendLine("    ' SET DateFormat ' + @DateFormat + ");
        //    SQL.AppendLine("    ' SELECT ' + @TopClausule + '");
        //    SQL.AppendLine("    Gv_ArticuloInventario_B1.Codigo, '");
        //    SQL.AppendLine("    Gv_ArticuloInventario_B1.Descripcion,'");
        //    SQL.AppendLine("    Gv_ArticuloInventario_B1.LineaDeProducto,'");
        //    SQL.AppendLine("    Gv_ArticuloInventario_B1.TipoDeArticuloStr,'");
        //    SQL.AppendLine("    Gv_ArticuloInventario_B1.PrecioSinIVA, '");
        //    SQL.AppendLine("    Gv_ArticuloInventario_B1.PrecioConIVA, '");
        //    SQL.AppendLine("    Gv_ArticuloInventario_B1.Categoria, '");
        //    SQL.AppendLine("    Gv_ArticuloInventario_B1.Existencia,'");
        //    SQL.AppendLine("    Gv_ArticuloInventario_B1.PorcentajeBaseImponible,'");
        //    SQL.AppendLine("    ''COLPIVOTE'' AS ColControl,");
        //    SQL.AppendLine("    Gv_ArticuloInventario_B1.ConsecutivoCompania, '");
        //    SQL.AppendLine("    Gv_ArticuloInventario_B1.StatusdelArticulo, '");
        //    SQL.AppendLine("    Gv_ArticuloInventario_B1.TipoDeArticulo, '");
        //    SQL.AppendLine("    Gv_ArticuloInventario_B1.AlicuotaIVA '");
        //    SQL.AppendLine("    FROM Gv_ArticuloInventario_B1 '");
        //    SQL.AppendLine("    INNER JOIN LineaDeProducto ON  Gv_ArticuloInventario_B1.LineaDeProducto = LineaDeProducto.Nombre '");
        //    SQL.AppendLine("    AND Gv_ArticuloInventario_B1.ConsecutivoCompania = LineaDeProducto.ConsecutivoCompania '");
        //    SQL.AppendLine("    INNER JOIN Saw.Gv_Categoria_B1 ON  Gv_ArticuloInventario_B1.Categoria = Saw.Gv_Categoria_B1.Descripcion '");
        //    SQL.AppendLine("    AND Gv_ArticuloInventario_B1.ConsecutivoCompania = Saw.Gv_Categoria_B1.ConsecutivoCompania '");
        //    SQL.AppendLine("    INNER JOIN Saw.Gv_UnidadDeVenta_B1 ON  Gv_ArticuloInventario_B1.UnidadDeVenta = Saw.Gv_UnidadDeVenta_B1.Nombre'");
        //    SQL.AppendLine("'   IF (NOT @SQLWhere IS NULL) AND (@SQLWhere <> '')");
        //    SQL.AppendLine("      SET @strSQL = @strSQL + ' WHERE ' + @SQLWhere");
        //    SQL.AppendLine("   IF (NOT @SQLOrderBy IS NULL) AND (@SQLOrderBy <> '')");
        //    SQL.AppendLine("      SET @strSQL = @strSQL + ' ORDER BY ' + @SQLOrderBy");
        //    SQL.AppendLine("   EXEC(@strSQL)");
        //    SQL.AppendLine("END");
        //    return SQL.ToString();
        //}

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
            SQL.AppendLine("      " + DbSchema + ".ArticuloInventario.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".ArticuloInventario.Codigo,");
            SQL.AppendLine("      " + DbSchema + ".ArticuloInventario.Descripcion,");
            SQL.AppendLine("      " + DbSchema + ".ArticuloInventario.LineaDeProducto,");
            SQL.AppendLine("      " + DbSchema + ".ArticuloInventario.StatusdelArticulo,");
            SQL.AppendLine("      " + DbSchema + ".ArticuloInventario.TipoDeArticulo,");
            SQL.AppendLine("      " + DbSchema + ".ArticuloInventario.TipoArticuloInv,");
            SQL.AppendLine("      " + DbSchema + ".ArticuloInventario.Categoria,");
            SQL.AppendLine("      " + DbSchema + ".ArticuloInventario.UnidadDeVenta,");
            SQL.AppendLine("      " + DbSchema + ".ArticuloInventario.UsaBalanza,");
            SQL.AppendLine("      " + DbSchema + ".ArticuloInventario.AlicuotaIVA,");
            SQL.AppendLine("      " + DbSchema + ".ArticuloInventario.Existencia,");
            SQL.AppendLine("      " + DbSchema + ".ArticuloInventario.CostoUnitario,");
            SQL.AppendLine("      " + DbSchema + ".ArticuloInventario.MeCostoUnitario");
            //SQL.AppendLine("      ," + DbSchema + ".ArticuloInventario.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".ArticuloInventario");
            SQL.AppendLine("      WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("          AND Codigo IN (");
            SQL.AppendLine("            SELECT  Codigo ");
            SQL.AppendLine("            FROM OPENXML( @hdoc, 'GpData/GpResult',2) ");
            SQL.AppendLine("            WITH (Codigo varchar(30)) AS XmlDocOfFK) ");
            SQL.AppendLine(" EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlViewSearchArticulo() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT  ArticuloInventario.Codigo + ISNULL(ExistenciaPorGrupo.CodigoColor, '') + ISNULL(ExistenciaPorGrupo.CodigoTalla, '') AS CodigoDelArticulo");
            SQL.AppendLine(", ArticuloInventario.Descripcion + (CASE WHEN (Color.DescripcionColor IS NULL) THEN '' ELSE Color.DescripcionColor END)");
            SQL.AppendLine("+ (CASE WHEN (Talla.DescripcionTalla IS NULL) THEN '' ELSE Talla.DescripcionTalla END) AS Descripcion");
            SQL.AppendLine(", ArticuloInventario.LineaDeProducto");
            SQL.AppendLine(", ArticuloInventario.PrecioSinIVA");
            SQL.AppendLine(", ArticuloInventario.PrecioConIVA");
            SQL.AppendLine(", ISNULL(RenglonExistenciaAlmacen.CodigoSerial,0) AS Serial");
            SQL.AppendLine(", ISNULL(RenglonExistenciaAlmacen.CodigoRollo,0) AS Rollo");
            SQL.AppendLine(", ArticuloInventario.AlicuotaIva");
            SQL.AppendLine(", ISNULL(ExistenciaPorGrupo.CodigoGrupo,'') AS CodigoGrupo");
            SQL.AppendLine(", ArticuloInventario.TipoArticuloInv");
            SQL.AppendLine(", ArticuloInventario.Codigo");
            SQL.AppendLine(", ArticuloInventario.Categoria");
            SQL.AppendLine(", ArticuloInventario.UnidadDeVenta");
            SQL.AppendLine(", ArticuloInventario.ConsecutivoCompania");
            SQL.AppendLine(", ArticuloInventario.PorcentajeBaseImponible");
            SQL.AppendLine(", ArticuloInventario.Peso");
            SQL.AppendLine(", ArticuloInventario.UnidadDeVenta");
            SQL.AppendLine(", ArticuloInventario.ArancelesCodigo");
            SQL.AppendLine(", ISNULL(ExistenciaPorAlmacen.CodigoAlmacen, '') AS CodigoAlmacen");
            SQL.AppendLine(", (CASE WHEN ArticuloInventario.TipoArticuloInv = '1' THEN  (ISNULL(ExistenciaPorAlmacen.Cantidad, 0) + ISNULL(ExistenciaPorGrupo.Existencia, 0) + ISNULL(RenglonExistenciaAlmacen.Cantidad, 0)");
            SQL.AppendLine("- SUM(ISNULL(renglonFactura.Cantidad, 0)) - SUM(ISNULL(renglonCotizacion.Cantidad, 0)))");
            SQL.AppendLine(" ELSE");
            SQL.AppendLine(" (CASE WHEN  (RenglonExistenciaAlmacen.CodigoSerial IS NULL )  OR  (RenglonExistenciaAlmacen.CodigoRollo IS NULL )");
            SQL.AppendLine(" OR  RenglonExistenciaAlmacen.CodigoSerial = '' THEN ISNULL(ExistenciaPorAlmacen.Cantidad,0)");
            SQL.AppendLine(" ELSE ISNULL(RenglonExistenciaAlmacen.Cantidad,0) END )  END) AS Existencia ");
            SQL.AppendLine(", ArticuloInventario.UsaBalanza ");
            SQL.AppendLine(", ArticuloInventario.TipoDeMercanciaProduccion ");
            SQL.AppendLine(" FROM");
            SQL.AppendLine("	ArticuloInventario LEFT JOIN ExistenciaPorAlmacen ON (ExistenciaPorAlmacen.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania");
            SQL.AppendLine("	AND ExistenciaPorAlmacen.CodigoArticulo = ArticuloInventario.Codigo)");
            SQL.AppendLine("	LEFT JOIN   RenglonExistenciaAlmacen  ON (RenglonExistenciaAlmacen.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania");
            SQL.AppendLine("	AND RenglonExistenciaAlmacen.CodigoArticulo = ArticuloInventario.Codigo)");
            SQL.AppendLine("    LEFT JOIN ExistenciaPorGrupo ON (ArticuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo");
            SQL.AppendLine("	AND ArticuloInventario.CodigoGrupo = (CASE WHEN ExistenciaPorGrupo.CodigoGrupo = '0' THEN '' ELSE ExistenciaPorGrupo.CodigoGrupo END)");
            SQL.AppendLine("	AND ArticuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania )");
            SQL.AppendLine("    LEFT JOIN Saw.Color ON (Saw.Color.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania");
            SQL.AppendLine("	AND Saw.Color.CodigoColor = ExistenciaPorGrupo.CodigoColor )");
            SQL.AppendLine("	LEFT JOIN Saw.Talla ON( Saw.Talla.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania");
            SQL.AppendLine("	AND Saw.Talla.CodigoTalla = ExistenciaPorGrupo.CodigoTalla )");
            SQL.AppendLine("	LEFT JOIN factura INNER JOIN renglonFactura");
            SQL.AppendLine("							ON (factura.Numero = renglonFactura.NumeroFactura");
            SQL.AppendLine("							AND factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania");
            SQL.AppendLine("							AND factura.ReservarMercancia = 'S' AND factura.StatusFactura = '0')");
            SQL.AppendLine("	ON ArticuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania");
            SQL.AppendLine("		AND ArticuloInventario.Codigo = renglonFactura.Articulo");
            SQL.AppendLine("	LEFT JOIN renglonCotizacion");
            SQL.AppendLine("					INNER JOIN cotizacion");
            SQL.AppendLine("					ON (renglonCotizacion.NumeroCotizacion = cotizacion.Numero");
            SQL.AppendLine("					AND renglonCotizacion.ConsecutivoCompania = cotizacion.ConsecutivoCompania )");
            SQL.AppendLine("	ON (ArticuloInventario.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania");
            SQL.AppendLine("	AND ArticuloInventario.Codigo = renglonCotizacion.CodigoArticulo)");
            SQL.AppendLine(" WHERE");
            SQL.AppendLine(" ArticuloInventario.Codigo NOT  IN( 'RD_AliExenta  @' , 'RD_AliGeneral @' , 'RD_AliReducida@', 'RD_AliExtendida', 'RD_ComXPorcDeAlmacen @'");
            SQL.AppendLine(", 'RD_AliExentaNC @',  'RD_AliGeneralNC @', 'RD_AliReducidaNC @', 'RD_AliExtendidaNC @', 'ND-NC IGTF @')");
            SQL.AppendLine(" GROUP BY ArticuloInventario.Codigo, ExistenciaPorGrupo.CodigoColor, ExistenciaPorGrupo.CodigoTalla, ArticuloInventario.Descripcion, Saw.Color.DescripcionColor");
            SQL.AppendLine(", Saw.Talla.DescripcionTalla, ArticuloInventario.LineaDeProducto, ExistenciaPorAlmacen.Cantidad, ExistenciaPorGrupo.Existencia");
            SQL.AppendLine(", RenglonExistenciaAlmacen.Cantidad, ArticuloInventario.ConsecutivoCompania, ArticuloInventario.PrecioSinIVA, ArticuloInventario.PrecioConIVA");
            SQL.AppendLine(", RenglonExistenciaAlmacen.CodigoSerial, RenglonExistenciaAlmacen.CodigoRollo, ArticuloInventario.AlicuotaIva, ExistenciaPorGrupo.CodigoGrupo");
            SQL.AppendLine(", ArticuloInventario.TipoArticuloInv, ArticuloInventario.PorcentajeBaseImponible, ExistenciaPorAlmacen.CodigoAlmacen");
            SQL.AppendLine(", ArticuloInventario.Categoria, ArticuloInventario.UnidadDeVenta,ArticuloInventario.UsaBalanza, ArticuloInventario.Peso, ArticuloInventario.ArancelesCodigo, ArticuloInventario.TipoDeMercanciaProduccion ");
            return SQL.ToString();
        }
        private string SqlViewB2() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("   SELECT");
            SQL.AppendLine("       (ExistenciaPorGrupo.CodigoArticulo + CASE WHEN(ExistenciaPorGrupo.CodigoColor IS NULL)  THEN ' ' ELSE ExistenciaPorGrupo.CodigoColor END   +CASE WHEN(ExistenciaPorGrupo.CodigoTalla IS NULL)  THEN ' ' ELSE ExistenciaPorGrupo.CodigoTalla END  ) As CodigoCompuesto,");
            SQL.AppendLine("       (CASE WHEN(ArticuloInventario.campodefinible1 IS NULL)  THEN ' ' ELSE ArticuloInventario.campodefinible1 + ' ' END + CASE WHEN(ArticuloInventario.campodefinible2 IS NULL)  THEN ' ' ELSE ArticuloInventario.campodefinible2 + ' ' END + (articuloInventario.Descripcion + ' ' + CASE WHEN(Color.DescripcionColor IS NULL)  THEN ' ' ELSE Color.descripcionColor END + ' ' + CASE WHEN(Talla.DescripcionTalla IS NULL)  THEN ' ' ELSE Talla.descripcionTalla END   )) As Descripcion,");
            SQL.AppendLine("       ArticuloInventario.LineaDeProducto, ");
            SQL.AppendLine("       ArticuloInventario.PrecioSinIVA, ");
            SQL.AppendLine("       ArticuloInventario.CostoUnitario, ");
            SQL.AppendLine("       ArticuloInventario.PrecioConIVA, ");
            SQL.AppendLine("       ArticuloInventario.AlicuotaIva, ");
            SQL.AppendLine("       ArticuloInventario.CodigoGrupo,  ");
            SQL.AppendLine("       ArticuloInventario.TipoArticuloInv, ");
            SQL.AppendLine("       ArticuloInventario.Codigo, articuloInventario.ConsecutivoCompania,");
            SQL.AppendLine("       ArticuloInventario.PorcentajeBaseImponible,");
            SQL.AppendLine("       ArticuloInventario.StatusdelArticulo,");
			SQL.AppendLine("       ArticuloInventario.TipoDeArticulo,");
            SQL.AppendLine("       ArticuloInventario.UnidadDeVenta, ");
            SQL.AppendLine("       ArticuloInventario.Categoria, ");
            SQL.AppendLine("       Comun.Aranceles.Codigo AS ArancelesCodigo,");
            SQL.AppendLine("       Comun.Aranceles.Descripcion AS ArancelesDescripcion,");
            SQL.AppendLine("       Comun.Aranceles.AdValorem, ");
            SQL.AppendLine("       Comun.Aranceles.Seguro, ");
            SQL.AppendLine("       ArticuloInventario.TipoDeMercanciaProduccion, ");
            SQL.AppendLine("       ArticuloInventario.Existencia, ");
            SQL.AppendLine("       ArticuloInventario.CantidadMaxima, ");
            SQL.AppendLine("       ArticuloInventario.CantidadMinima, ");
            SQL.AppendLine("       ArticuloInventario.MeCostoUnitario, ");
            SQL.AppendLine("       ArticuloInventario.CampoDefinible1, ");
            SQL.AppendLine("       ArticuloInventario.CampoDefinible2, ");
            SQL.AppendLine("       ArticuloInventario.CampoDefinible3, ");
            SQL.AppendLine("       ArticuloInventario.CampoDefinible4, ");
            SQL.AppendLine("       ArticuloInventario.CampoDefinible5, ");
            SQL.AppendLine("       ArticuloInventario.UnidadDeVentaSecundaria ");
            SQL.AppendLine("     FROM Saw.Talla RIGHT JOIN");
            SQL.AppendLine("       Comun.Aranceles RIGHT JOIN");
            SQL.AppendLine("       ExistenciaPorGrupo INNER JOIN");
            SQL.AppendLine("         ArticuloInventario LEFT  JOIN");
            SQL.AppendLine("           CamposMonedaExtranjera ");
            SQL.AppendLine("           ON ArticuloInventario.Codigo = CamposMonedaExtranjera.Codigo AND CamposMonedaExtranjera.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania ");
            SQL.AppendLine("         ON ArticuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo AND ArticuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania ");
            SQL.AppendLine("         ON Comun.Aranceles.Codigo = ArticuloInventario.Codigo ");
            SQL.AppendLine("       ON Saw.Talla.CodigoTalla = ExistenciaPorGrupo.CodigoTalla AND Saw.Talla.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania LEFT  JOIN");
            SQL.AppendLine("         Saw.Color ");
            SQL.AppendLine("           ON ExistenciaPorGrupo.CodigoColor = Saw.Color.CodigoColor AND ExistenciaPorGrupo.ConsecutivoCompania = Saw.Color.ConsecutivoCompania");
            SQL.AppendLine("     WHERE(ArticuloInventario.TipoArticuloInv = '3' ");
            SQL.AppendLine("       OR ArticuloInventario.TipoArticuloInv = '1' ");
            SQL.AppendLine("       OR ArticuloInventario.TipoArticuloInv = '2' ");
            SQL.AppendLine("       OR ArticuloInventario.TipoArticuloInv = '4')");
            SQL.AppendLine("       AND articuloInventario.Codigo NOT IN('RD_AliExenta  @', 'RD_AliGeneral @', 'RD_AliReducida@', 'RD_AliExtendida', 'RD_ComXPorcDeAlmacen @', 'RD_AliExentaNC @', 'RD_AliGeneralNC @', 'RD_AliReducidaNC @', 'RD_AliExtendidaNC @', 'ND-NC IGTF @')");
            SQL.AppendLine("       AND articuloInventario.TipoDeArticulo <> '2'");
            SQL.AppendLine("       AND articuloInventario.TipoDeArticulo <> '1'");
            SQL.AppendLine("    UNION ");
            SQL.AppendLine("  SELECT ");
            SQL.AppendLine("       ArticuloInventario.Codigo, ");
            SQL.AppendLine("       ArticuloInventario.Descripcion,");
            SQL.AppendLine("       ArticuloInventario.LineaDeProducto,");
            SQL.AppendLine("       ArticuloInventario.PrecioSinIVA,");
            SQL.AppendLine("       ArticuloInventario.CostoUnitario, ");
            SQL.AppendLine("       ArticuloInventario.PrecioConIVA,");
            SQL.AppendLine("       ArticuloInventario.AlicuotaIva,");
            SQL.AppendLine("       ArticuloInventario.CodigoGrupo,");
            SQL.AppendLine("       ArticuloInventario.TipoArticuloInv,");
            SQL.AppendLine("       ArticuloInventario.Codigo,");
            SQL.AppendLine("       ArticuloInventario.ConsecutivoCompania,");
            SQL.AppendLine("       ArticuloInventario.PorcentajeBaseImponible,");
            SQL.AppendLine("       ArticuloInventario.StatusdelArticulo,");
            SQL.AppendLine("       ArticuloInventario.TipoDeArticulo,");
            SQL.AppendLine("       ArticuloInventario.UnidadDeVenta, ");
            SQL.AppendLine("       ArticuloInventario.Categoria, ");
            SQL.AppendLine("       Comun.Aranceles.Codigo AS ArancelesCodigo,");
            SQL.AppendLine("       Comun.Aranceles.Descripcion AS ArancelesDescripcion,");
            SQL.AppendLine("       Comun.Aranceles.AdValorem, ");
            SQL.AppendLine("       Comun.Aranceles.Seguro, ");
            SQL.AppendLine("       ArticuloInventario.TipoDeMercanciaProduccion, ");
            SQL.AppendLine("       ArticuloInventario.Existencia, ");
            SQL.AppendLine("       ArticuloInventario.CantidadMaxima, ");
            SQL.AppendLine("       ArticuloInventario.CantidadMinima, ");
            SQL.AppendLine("       ArticuloInventario.MeCostoUnitario, ");
            SQL.AppendLine("       ArticuloInventario.CampoDefinible1, ");
            SQL.AppendLine("       ArticuloInventario.CampoDefinible2, ");
            SQL.AppendLine("       ArticuloInventario.CampoDefinible3, ");
            SQL.AppendLine("       ArticuloInventario.CampoDefinible4, ");
            SQL.AppendLine("       ArticuloInventario.CampoDefinible5, ");
            SQL.AppendLine("       ArticuloInventario.UnidadDeVentaSecundaria ");
            SQL.AppendLine("     FROM ArticuloInventario LEFT JOIN");
            SQL.AppendLine("       Comun.Aranceles ");
            SQL.AppendLine("         ON ArticuloInventario.Codigo = Comun.Aranceles.Codigo LEFT JOIN");
            SQL.AppendLine("       CamposMonedaExtranjera");
            SQL.AppendLine("         ON ArticuloInventario.Codigo = CamposMonedaExtranjera.Codigo AND CamposMonedaExtranjera.consecutivoCompania = ArticuloInventario.ConsecutivoCompania");
            SQL.AppendLine("     WHERE");
            SQL.AppendLine("       articuloInventario.TipoDeArticulo <> '2'");
            SQL.AppendLine("       AND(articuloInventario.AlicuotaIva IN('1', '0', '2', '3', '5', '4', '6', '7'))");
            SQL.AppendLine("       AND ArticuloInventario.TipoArticuloInv = '0'");
            SQL.AppendLine("       AND articuloInventario.Codigo NOT IN ('RD_AliExenta  @', 'RD_AliGeneral @', 'RD_AliReducida@', 'RD_AliExtendida', 'RD_ComXPorcDeAlmacen @', 'RD_AliExentaNC @', 'RD_AliGeneralNC @', 'RD_AliReducidaNC @', 'RD_AliExtendidaNC @', 'ND-NC IGTF @')");
            return SQL.ToString();

        }

        private string SqlSpSearchForCompraParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@SQLWhere" + InsSql.VarCharTypeForDb(2000) + " = null,");
            SQL.AppendLine("@SQLOrderBy" + InsSql.VarCharTypeForDb(500) + " = null,");
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + " = null,");
            SQL.AppendLine("@UseTopClausule" + InsSql.VarCharTypeForDb(1) + " = 'S'");
            return SQL.ToString();
        }

        private string SqlSpSearchForCompra() {
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
            SQL.AppendLine("   ' SET DateFormat ' + @DateFormat + ");
            SQL.AppendLine("   ' SELECT ' + @TopClausule + '");
            SQL.AppendLine("      CodigoCompuesto,");
            SQL.AppendLine("      Descripcion,");
            SQL.AppendLine("      LineaDeProducto, ");
            SQL.AppendLine("      PrecioSinIVA, ");
            SQL.AppendLine("      PrecioConIVA, ");
            SQL.AppendLine("      AlicuotaIva, ");
            SQL.AppendLine("      CodigoGrupo,  ");
            SQL.AppendLine("      CostoUnitario,  ");
            SQL.AppendLine("      TipoArticuloInv, ");
            SQL.AppendLine("      Codigo, ");
            SQL.AppendLine("      ConsecutivoCompania,");
            SQL.AppendLine("      PorcentajeBaseImponible,");
            SQL.AppendLine("      ArancelesCodigo,");
            SQL.AppendLine("      ArancelesDescripcion,");
            SQL.AppendLine("      AdValorem, ");
            SQL.AppendLine("      Seguro, ");
            SQL.AppendLine("      TipoDeMercanciaProduccion, ");
            SQL.AppendLine("      Existencia, ");
            SQL.AppendLine("      CantidadMaxima, ");
            SQL.AppendLine("      CantidadMinima, ");
            SQL.AppendLine("      TipoDeArticulo");
            SQL.AppendLine("     FROM Gv_ArticuloInventario_B2");
            SQL.AppendLine("'   IF (NOT @SQLWhere IS NULL) AND (@SQLWhere <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' WHERE ' + @SQLWhere");
            SQL.AppendLine("   IF (NOT @SQLOrderBy IS NULL) AND (@SQLOrderBy <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' ORDER BY ' + @SQLOrderBy");
            SQL.AppendLine("   EXEC(@strSQL)");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        #endregion //Queries

        //bool CrearTabla() {
        //    bool vResult = insDbo.Create(DbSchema + ".ArticuloInventario", SqlCreateTable(), false, eDboType.Tabla);
        //    return vResult;
        //}

        bool CrearVistas() {
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumStatusArticulo",LibTpvCreator.SqlViewStandardEnum(typeof(eStatusArticulo),InsSql),true,true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDeArticulo",LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDeArticulo),InsSql),true,true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDeAlicuota",LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDeAlicuota),InsSql),true,true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoArticuloInv",LibTpvCreator.SqlViewStandardEnum(typeof(eTipoArticuloInv),InsSql),true,true);
            vResult = insVistas.Create(DbSchema + ".Gv_ArticuloInventario_B1",SqlViewB1(),true);
            vResult = insVistas.Create(DbSchema + ".Gv_ArticuloInventario_B2",SqlViewB2(),true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            //vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ArticuloInventarioINS", SqlSpInsParameters(), SqlSpIns(), true);
            //vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ArticuloInventarioUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            //vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ArticuloInventarioDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            //vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ArticuloInventarioGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ArticuloInventarioSCH",SqlSpSearchParameters(),SqlSpSearch(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ArticuloInventarioCompraSCH",SqlSpSearchForCompraParameters(),SqlSpSearchForCompra(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ObtenerPaginaDeArticulosPorDescripcion",
                                                    SqlObtenerPaginaDeArticulosParametros(),
                                                    SqlObtenerPaginaDeArticulosPorDescripcionSentencia(),
                                                    true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ObtenerPaginaDeArticulosPorCodigo",
                                                    SqlObtenerPaginaDeArticulosParametros(),
                                                    SqlObtenerPaginaDeArticulosPorCodigoSentencia(),
                                                    true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ArticuloInventarioGetFk",SqlSpGetFKParameters(),SqlSpGetFK(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ArticuloInventarioCompraGetFk",SqlSpGetFKForCompraParameters(),SqlSpGetFKForCompra(),true) && vResult;
            insSps.Dispose();
            return vResult;
        }

        public bool InstalarTabla() {
            bool vResult = true;
            //if (CrearTabla()) {
            //CrearVistas();
            //CrearProcedimientos();
            //clsProductoCompuestoED insDetailProCom = new clsProductoCompuestoED();
            //vResult = insDetailProCom.InstalarTabla();
            //clsExistenciaPorGrupoED insDetailExiPorGru = new clsExistenciaPorGrupoED();
            //vResult = vResult && insDetailExiPorGru.InstalarTabla();
            //clsCodigoDeBarrasED insDetailCodDeBar = new clsCodigoDeBarrasED();
            //vResult = vResult && insDetailCodDeBar.InstalarTabla();
            //}
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = true;
            if(insDbo.Exists(DbSchema + ".ArticuloInventario",eDboType.Tabla)) {
                CrearVistas();
                CrearProcedimientos();
                //    vResult = new clsProductoCompuestoED().InstalarVistasYSps();
                //    vResult = vResult && new clsExistenciaPorGrupoED().InstalarVistasYSps();
                //    vResult = vResult && new clsCodigoDeBarrasED().InstalarVistasYSps();
            }
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = true;
            LibStoredProc insSp = new LibStoredProc();
            LibViews insVista = new LibViews();
            //vResult = new clsProductoCompuestoED().BorrarVistasYSps();
            //vResult = new clsExistenciaPorGrupoED().BorrarVistasYSps() && vResult;
            //vResult = new clsCodigoDeBarrasED().BorrarVistasYSps() && vResult;
            //vResult = insSp.Drop(DbSchema + ".Gp_ArticuloInventarioINS") && vResult;
            //vResult = insSp.Drop(DbSchema + ".Gp_ArticuloInventarioUPD") && vResult;
            //vResult = insSp.Drop(DbSchema + ".Gp_ArticuloInventarioDEL") && vResult;
            //vResult = insSp.Drop(DbSchema + ".Gp_ArticuloInventarioGET") && vResult;
            //vResult = insSp.Drop(DbSchema + ".Gp_ArticuloInventarioGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ArticuloInventarioCompraGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ObtenerPaginaDeArticulosPorCodigo") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ObtenerPaginaDeArticulosPorDescripcion") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ArticuloInventarioSCH") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ArticuloInventarioCompraSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_ArticuloInventario_B2") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_ArticuloInventario_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumStatusArticulo") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDeArticulo") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDeAlicuota") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoArticuloInv") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados

        private string SqlObtenerPaginaDeArticulosParametros() {
            StringBuilder SQL = new StringBuilder();
            SQL.Append("	@filtro" + InsSql.VarCharTypeForDb(50) + ", ");
            SQL.Append("    @consecutivoCompania " + InsSql.NumericTypeForDb(15,0) + ",");
            SQL.Append("    @pagina " + InsSql.NumericTypeForDb(15,0) + ",");
            SQL.Append("	@articulosPorPagina" + InsSql.NumericTypeForDb(15,0));
            return SQL.ToString();
        }

        private string SqlObtenerPaginaDeArticulosPorDescripcionSentencia() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("	WITH [Articulos Filtrados]");
            SQL.AppendLine("	AS");
            SQL.AppendLine("	(SELECT art.Codigo, art.Descripcion");
            SQL.AppendLine("	FROM ArticuloInventario art ");
            SQL.AppendLine("	WHERE art.StatusdelArticulo = " + InsSql.EnumToSqlValue((int)eStatusArticulo.Vigente));
            SQL.AppendLine("	AND art.ConsecutivoCompania=@consecutivoCompania");
            SQL.AppendLine("	AND art.[Descripcion] LIKE '%' + @filtro + '%'");
            SQL.AppendLine("	AND art.[Codigo] NOT LIKE 'RD_%'");
            SQL.AppendLine("	),");
            SQL.AppendLine("	[Articulos Rankeados]");
            SQL.AppendLine("	AS");
            SQL.AppendLine("	(SELECT art.Codigo, art.Descripcion,ROW_NUMBER() OVER(ORDER BY art.Descripcion) AS Ranking");
            SQL.AppendLine("	FROM [Articulos Filtrados] art)");
            SQL.AppendLine("	SELECT art.Codigo,art.Descripcion FROM [Articulos Rankeados] art");
            SQL.AppendLine("	WHERE art.Ranking BETWEEN (@pagina-1)*@articulosPorPagina+1 AND @pagina*@articulosPorPagina");
            return SQL.ToString();
        }

        private string SqlObtenerPaginaDeArticulosPorCodigoSentencia() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("	WITH [Articulos Filtrados]");
            SQL.AppendLine("	AS");
            SQL.AppendLine("	(SELECT art.Codigo, art.Descripcion");
            SQL.AppendLine("	FROM ArticuloInventario art ");
            SQL.AppendLine("	WHERE art.StatusdelArticulo = "+ InsSql.EnumToSqlValue((int)eStatusArticulo.Vigente));
            SQL.AppendLine("	AND art.ConsecutivoCompania=@consecutivoCompania");
            SQL.AppendLine("	AND art.[Codigo] LIKE '%' + @filtro + '%'");
            SQL.AppendLine("	AND art.[Codigo] NOT LIKE 'RD_%'");
            SQL.AppendLine("	),");
            SQL.AppendLine("	[Articulos Rankeados]");
            SQL.AppendLine("	AS");
            SQL.AppendLine("	(SELECT art.Codigo, art.Descripcion,ROW_NUMBER() OVER(ORDER BY art.Descripcion) AS Ranking");
            SQL.AppendLine("	FROM [Articulos Filtrados] art)");
            SQL.AppendLine("	SELECT * FROM [Articulos Rankeados] art");
            SQL.AppendLine("	WHERE art.Ranking BETWEEN (@pagina-1)*@articulosPorPagina+1 AND @pagina*@articulosPorPagina	");
            return SQL.ToString();
        }

        private string SqlSpGetFKForCompraParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@XmlData" + InsSql.XmlTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpGetFKForCompra() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine(" DECLARE @hdoc int ");
            SQL.AppendLine(" EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlData ");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("      ConsecutivoCompania,");
            SQL.AppendLine("      Codigo,");
            SQL.AppendLine("      Descripcion,");
            SQL.AppendLine("      LineaDeProducto,");
            SQL.AppendLine("      StatusdelArticulo,");
            SQL.AppendLine("      TipoDeArticulo,");
            SQL.AppendLine("      TipoArticuloInv,");
            SQL.AppendLine("      Categoria,");
            SQL.AppendLine("      UnidadDeVenta,");
            SQL.AppendLine("      CodigoCompuesto,");
            SQL.AppendLine("      CodigoGrupo,");
            SQL.AppendLine("      AlicuotaIVA,");
            SQL.AppendLine("      CostoUnitario,");
            SQL.AppendLine("      MeCostoUnitario");
            //SQL.AppendLine("      ," + DbSchema + ".ArticuloInventario.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_ArticuloInventario_B2");
            SQL.AppendLine("      WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("          AND (Codigo IN (");
            SQL.AppendLine("            SELECT  Codigo ");
            SQL.AppendLine("            FROM OPENXML( @hdoc, 'GpData/GpResult',2) ");
            SQL.AppendLine("            WITH (Codigo varchar(30)) AS XmlDocOfFK) ");

            SQL.AppendLine("            OR CodigoCompuesto IN (");
            SQL.AppendLine("            SELECT  Codigo ");
            SQL.AppendLine("            FROM OPENXML( @hdoc, 'GpData/GpResult',2) ");
            SQL.AppendLine("            WITH (Codigo varchar(30)) AS XmlDocOfFK2) )");
            SQL.AppendLine(" EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
    } //End of class clsArticuloInventarioED

} //End of namespace Galac.Saw.Dal.Inventario

