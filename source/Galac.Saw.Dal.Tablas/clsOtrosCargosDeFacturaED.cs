using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Saw.Ccl.Tablas;
using LibGalac.Aos.Base;

namespace Galac.Saw.Dal.Tablas {
    [LibMefDalComponentMetadata(typeof(clsOtrosCargosDeFacturaED))]
    public class clsOtrosCargosDeFacturaED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsOtrosCargosDeFacturaED(): base(){
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
            get { return "otrosCargosDeFactura"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("otrosCargosDeFactura", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnotrCarDeFacConsecutiv NOT NULL, ");
            SQL.AppendLine("Codigo" + InsSql.VarCharTypeForDb(15) + " CONSTRAINT nnotrCarDeFacCodigo NOT NULL, ");
            SQL.AppendLine("Descripcion" + InsSql.VarCharTypeForDb(255) + " CONSTRAINT nnotrCarDeFacDescripcio NOT NULL, ");
            SQL.AppendLine("Status" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_otrCarDeFacSt DEFAULT ('0'), ");
            SQL.AppendLine("SeCalculaEnBasea" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_otrCarDeFacSeCaEnBa DEFAULT ('0'), ");
            SQL.AppendLine("Monto" + InsSql.DecimalTypeForDb(15, 3) + " CONSTRAINT d_otrCarDeFacMo DEFAULT (0), ");
            SQL.AppendLine("BaseFormula" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_otrCarDeFacBaFo DEFAULT ('0'), ");
            SQL.AppendLine("PorcentajeSobreBase" + InsSql.DecimalTypeForDb(15, 3) + " CONSTRAINT d_otrCarDeFacPoSoBa DEFAULT (0), ");
            SQL.AppendLine("Sustraendo" + InsSql.DecimalTypeForDb(15, 3) + " CONSTRAINT d_otrCarDeFacSu DEFAULT (0), ");
            SQL.AppendLine("ComoAplicaAlTotalFactura" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_otrCarDeFacCoApAlToFa DEFAULT ('0'), ");
            SQL.AppendLine("CuentaContableOtrosCargos" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_otrCarDeFacCuCoOtCa DEFAULT (''), ");
            SQL.AppendLine("PorcentajeComision" + InsSql.DecimalTypeForDb(15, 3) + " CONSTRAINT d_otrCarDeFacPoCo DEFAULT (0), ");
            SQL.AppendLine("ExcluirDeComision" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnotrCarDeFacExcluirDeC NOT NULL, ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_otrosCargosDeFactura PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Codigo ASC)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT ConsecutivoCompania, Codigo, Descripcion, Status, " + DbSchema + ".Gv_EnumStatusOtrosCargosyDescuentosDeFactura.StrValue AS StatusStr");
            SQL.AppendLine(", SeCalculaEnBasea, " + DbSchema + ".Gv_EnumBaseCalculoOtrosCargosDeFactura.StrValue AS SeCalculaEnBaseaStr, Monto, BaseFormula, " + DbSchema + ".Gv_EnumBaseFormulaOtrosCargosDeFactura.StrValue AS BaseFormulaStr, PorcentajeSobreBase");
            SQL.AppendLine(", Sustraendo, ComoAplicaAlTotalFactura, " + DbSchema + ".Gv_EnumComoAplicaOtrosCargosDeFactura.StrValue AS ComoAplicaAlTotalFacturaStr, CuentaContableOtrosCargos, PorcentajeComision");
            SQL.AppendLine(", ExcluirDeComision, NombreOperador, FechaUltimaModificacion");
            SQL.AppendLine(", otrosCargosDeFactura.fldTimeStamp, CAST(otrosCargosDeFactura.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".otrosCargosDeFactura");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumStatusOtrosCargosyDescuentosDeFactura");
            SQL.AppendLine("ON " + DbSchema + ".otrosCargosDeFactura.Status COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumStatusOtrosCargosyDescuentosDeFactura.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumBaseCalculoOtrosCargosDeFactura");
            SQL.AppendLine("ON " + DbSchema + ".otrosCargosDeFactura.SeCalculaEnBasea COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumBaseCalculoOtrosCargosDeFactura.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumBaseFormulaOtrosCargosDeFactura");
            SQL.AppendLine("ON " + DbSchema + ".otrosCargosDeFactura.BaseFormula COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumBaseFormulaOtrosCargosDeFactura.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumComoAplicaOtrosCargosDeFactura");
            SQL.AppendLine("ON " + DbSchema + ".otrosCargosDeFactura.ComoAplicaAlTotalFactura COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumComoAplicaOtrosCargosDeFactura.DbValue");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(15) + ",");
            SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(255) + " = '',");
            SQL.AppendLine("@Status" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@SeCalculaEnBasea" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Monto" + InsSql.DecimalTypeForDb(15, 3) + " = 0,");
            SQL.AppendLine("@BaseFormula" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@PorcentajeSobreBase" + InsSql.DecimalTypeForDb(15, 3) + " = 0,");
            SQL.AppendLine("@Sustraendo" + InsSql.DecimalTypeForDb(15, 3) + " = 0,");
            SQL.AppendLine("@ComoAplicaAlTotalFactura" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@CuentaContableOtrosCargos" + InsSql.VarCharTypeForDb(30) + " = '',");
            SQL.AppendLine("@PorcentajeComision" + InsSql.DecimalTypeForDb(15, 3) + " = 0,");
            SQL.AppendLine("@ExcluirDeComision" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + " = '01/01/1900'");
            return SQL.ToString();
        }

        private string SqlSpIns() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".otrosCargosDeFactura(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Codigo,");
            SQL.AppendLine("            Descripcion,");
            SQL.AppendLine("            Status,");
            SQL.AppendLine("            SeCalculaEnBasea,");
            SQL.AppendLine("            Monto,");
            SQL.AppendLine("            BaseFormula,");
            SQL.AppendLine("            PorcentajeSobreBase,");
            SQL.AppendLine("            Sustraendo,");
            SQL.AppendLine("            ComoAplicaAlTotalFactura,");
            SQL.AppendLine("            CuentaContableOtrosCargos,");
            SQL.AppendLine("            PorcentajeComision,");
            SQL.AppendLine("            ExcluirDeComision,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Codigo,");
            SQL.AppendLine("            @Descripcion,");
            SQL.AppendLine("            @Status,");
            SQL.AppendLine("            @SeCalculaEnBasea,");
            SQL.AppendLine("            @Monto,");
            SQL.AppendLine("            @BaseFormula,");
            SQL.AppendLine("            @PorcentajeSobreBase,");
            SQL.AppendLine("            @Sustraendo,");
            SQL.AppendLine("            @ComoAplicaAlTotalFactura,");
            SQL.AppendLine("            @CuentaContableOtrosCargos,");
            SQL.AppendLine("            @PorcentajeComision,");
            SQL.AppendLine("            @ExcluirDeComision,");
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
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(15) + ",");
            SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(255) + ",");
            SQL.AppendLine("@Status" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@SeCalculaEnBasea" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Monto" + InsSql.DecimalTypeForDb(15, 3) + ",");
            SQL.AppendLine("@BaseFormula" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@PorcentajeSobreBase" + InsSql.DecimalTypeForDb(15, 3) + ",");
            SQL.AppendLine("@Sustraendo" + InsSql.DecimalTypeForDb(15, 3) + ",");
            SQL.AppendLine("@ComoAplicaAlTotalFactura" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@CuentaContableOtrosCargos" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@PorcentajeComision" + InsSql.DecimalTypeForDb(15, 3) + ",");
            SQL.AppendLine("@ExcluirDeComision" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + ",");
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
            SQL.AppendLine("   SET @ReturnValue = -1");
            SQL.AppendLine("   SET @ValidationMsg = ''");
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".otrosCargosDeFactura WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @Codigo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".otrosCargosDeFactura WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @Codigo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_otrosCargosDeFacturaCanBeUpdated @ConsecutivoCompania,@Codigo, @CurrentTimeStamp, @ValidationMsg out");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".otrosCargosDeFactura");
            SQL.AppendLine("            SET Descripcion = @Descripcion,");
            SQL.AppendLine("               Status = @Status,");
            SQL.AppendLine("               SeCalculaEnBasea = @SeCalculaEnBasea,");
            SQL.AppendLine("               Monto = @Monto,");
            SQL.AppendLine("               BaseFormula = @BaseFormula,");
            SQL.AppendLine("               PorcentajeSobreBase = @PorcentajeSobreBase,");
            SQL.AppendLine("               Sustraendo = @Sustraendo,");
            SQL.AppendLine("               ComoAplicaAlTotalFactura = @ComoAplicaAlTotalFactura,");
            SQL.AppendLine("               CuentaContableOtrosCargos = @CuentaContableOtrosCargos,");
            SQL.AppendLine("               PorcentajeComision = @PorcentajeComision,");
            SQL.AppendLine("               ExcluirDeComision = @ExcluirDeComision,");
            SQL.AppendLine("               NombreOperador = @NombreOperador,");
            SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND Codigo = @Codigo");
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
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(15) + ",");
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
            SQL.AppendLine("   SET @ReturnValue = -1");
            SQL.AppendLine("   SET @ValidationMsg = ''");
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".otrosCargosDeFactura WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @Codigo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".otrosCargosDeFactura WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @Codigo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_otrosCargosDeFacturaCanBeDeleted @ConsecutivoCompania,@Codigo, @CurrentTimeStamp, @ValidationMsg out");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".otrosCargosDeFactura");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND Codigo = @Codigo");
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
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(15));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         Codigo,");
            SQL.AppendLine("         Descripcion,");
            SQL.AppendLine("         Status,");
            SQL.AppendLine("         SeCalculaEnBasea,");
            SQL.AppendLine("         Monto,");
            SQL.AppendLine("         BaseFormula,");
            SQL.AppendLine("         PorcentajeSobreBase,");
            SQL.AppendLine("         Sustraendo,");
            SQL.AppendLine("         ComoAplicaAlTotalFactura,");
            SQL.AppendLine("         CuentaContableOtrosCargos,");
            SQL.AppendLine("         PorcentajeComision,");
            SQL.AppendLine("         ExcluirDeComision,");
            SQL.AppendLine("         NombreOperador,");
            SQL.AppendLine("         FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".otrosCargosDeFactura");
            SQL.AppendLine("      WHERE otrosCargosDeFactura.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND otrosCargosDeFactura.Codigo = @Codigo");
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
            SQL.AppendLine("      Codigo,");
            SQL.AppendLine("      Descripcion,");
            SQL.AppendLine("      SeCalculaEnBasea,");
            SQL.AppendLine("      Monto,");
            SQL.AppendLine("      BaseFormula,");
            SQL.AppendLine("      PorcentajeSobreBase,");
            SQL.AppendLine("      Sustraendo,");
            SQL.AppendLine("      ComoAplicaAlTotalFactura,");
            SQL.AppendLine("      CuentaContableOtrosCargos,");
            SQL.AppendLine("      PorcentajeComision,");
            SQL.AppendLine("      ExcluirDeComision,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      ConsecutivoCompania,");
            SQL.AppendLine("      Status");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_otrosCargosDeFactura_B1");
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
            SQL.AppendLine("      " + DbSchema + ".otrosCargosDeFactura.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".otrosCargosDeFactura.Codigo,");
            SQL.AppendLine("      " + DbSchema + ".otrosCargosDeFactura.Descripcion,");
            SQL.AppendLine("      " + DbSchema + ".otrosCargosDeFactura.Status,");
            SQL.AppendLine("      " + DbSchema + ".otrosCargosDeFactura.SeCalculaEnBasea,");
            SQL.AppendLine("      " + DbSchema + ".otrosCargosDeFactura.Monto,");
            SQL.AppendLine("      " + DbSchema + ".otrosCargosDeFactura.BaseFormula,");
            SQL.AppendLine("      " + DbSchema + ".otrosCargosDeFactura.PorcentajeSobreBase,");
            SQL.AppendLine("      " + DbSchema + ".otrosCargosDeFactura.Sustraendo,");
            SQL.AppendLine("      " + DbSchema + ".otrosCargosDeFactura.ComoAplicaAlTotalFactura,");
            SQL.AppendLine("      " + DbSchema + ".otrosCargosDeFactura.CuentaContableOtrosCargos,");
            SQL.AppendLine("      " + DbSchema + ".otrosCargosDeFactura.PorcentajeComision,");
            SQL.AppendLine("      " + DbSchema + ".otrosCargosDeFactura.ExcluirDeComision");
            SQL.AppendLine("      FROM " + DbSchema + ".otrosCargosDeFactura");
            SQL.AppendLine("      WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("          AND Codigo IN (");
            SQL.AppendLine("            SELECT  Codigo ");
            SQL.AppendLine("            FROM OPENXML( @hdoc, 'GpData/GpResult',2) ");
            SQL.AppendLine("            WITH (Codigo varchar(15)) AS XmlFKTmp) ");
            SQL.AppendLine(" EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".otrosCargosDeFactura", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumStatusOtrosCargosyDescuentosDeFactura", LibTpvCreator.SqlViewStandardEnum(typeof(eStatusOtrosCargosyDescuentosDeFactura), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumBaseCalculoOtrosCargosDeFactura", LibTpvCreator.SqlViewStandardEnum(typeof(eBaseCalculoOtrosCargosDeFactura), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumBaseFormulaOtrosCargosDeFactura", LibTpvCreator.SqlViewStandardEnum(typeof(eBaseFormulaOtrosCargosDeFactura), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumComoAplicaOtrosCargosDeFactura", LibTpvCreator.SqlViewStandardEnum(typeof(eComoAplicaOtrosCargosDeFactura), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_otrosCargosDeFactura_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OtrosCargosDeFacturaINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OtrosCargosDeFacturaUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OtrosCargosDeFacturaDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OtrosCargosDeFacturaGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OtrosCargosDeFacturaSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OtrosCargosDeFacturaGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".otrosCargosDeFactura", eDboType.Tabla)) {
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
            if (insDbo.Exists(DbSchema + ".Gp_OtrosCargosDeFacturaINS",eDboType.Procedimiento)) {
                vResult = insSp.Drop(DbSchema + ".Gp_OtrosCargosDeFacturaINS") && vResult;
                vResult = insSp.Drop(DbSchema + ".Gp_OtrosCargosDeFacturaUPD") && vResult;
                vResult = insSp.Drop(DbSchema + ".Gp_OtrosCargosDeFacturaDEL") && vResult;
                vResult = insSp.Drop(DbSchema + ".Gp_OtrosCargosDeFacturaGET") && vResult;
                vResult = insSp.Drop(DbSchema + ".Gp_OtrosCargosDeFacturaGetFk") && vResult;
                vResult = insSp.Drop(DbSchema + ".Gp_OtrosCargosDeFacturaSCH") && vResult;
            } else {
                vResult=true;
            }
            if (insDbo.Exists(DbSchema + ".Gv_OtrosCargosDeFactura_B1", eDboType.Vista)) {
                vResult = insVista.Drop(DbSchema + ".Gv_OtrosCargosDeFactura_B1") && vResult;
                vResult = insVista.Drop(DbSchema + ".Gv_EnumStatusOtrosCargosyDescuentosDeFactura") && vResult;
                vResult = insVista.Drop(DbSchema + ".Gv_EnumBaseCalculoOtrosCargosDeFactura") && vResult;
                vResult = insVista.Drop(DbSchema + ".Gv_EnumBaseFormulaOtrosCargosDeFactura") && vResult;
                vResult = insVista.Drop(DbSchema + ".Gv_EnumComoAplicaOtrosCargosDeFactura") && vResult;
            } else {
                vResult = true;
            }
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsotrosCargosDeFacturaED

} //End of namespace Galac.Saw.Dal.Tablas

