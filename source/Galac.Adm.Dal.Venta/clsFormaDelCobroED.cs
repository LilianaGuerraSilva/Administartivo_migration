using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.Base;

namespace Galac.Adm.Dal.Venta {
    [LibMefDalComponentMetadata(typeof(clsFormaDelCobroED))]
    public class clsFormaDelCobroED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsFormaDelCobroED(): base(){
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
            get { return "FormaDelCobro"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("FormaDelCobro", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnForDelCobConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnForDelCobConsecutiv NOT NULL, ");
            SQL.AppendLine("Codigo" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT nnForDelCobCodigo NOT NULL, ");
            SQL.AppendLine("Nombre" + InsSql.VarCharTypeForDb(50) + " CONSTRAINT d_ForDelCobNo DEFAULT (''), ");
            SQL.AppendLine("TipoDePago" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_ForDelCobTiDePa DEFAULT ('1'), ");
            SQL.AppendLine("CodigoCuentaBancaria" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT d_ForDelCobCoCuBa DEFAULT (''), ");
            SQL.AppendLine("CodigoMoneda" + InsSql.VarCharTypeForDb(4) + " CONSTRAINT d_ForDelCobCoMo DEFAULT (''), ");
            SQL.AppendLine("CodigoTheFactory" + InsSql.VarCharTypeForDb(2) + " CONSTRAINT d_ForDelCobCoThFa DEFAULT ('01'), ");
            SQL.AppendLine("Origen" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_ForDelCobOr DEFAULT ('0'), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_FormaDelCobro PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Consecutivo ASC)");
            SQL.AppendLine(", CONSTRAINT fk_FormaDelCobroMoneda FOREIGN KEY (CodigoMoneda)");
            SQL.AppendLine("REFERENCES dbo.Moneda(Codigo)");
            SQL.AppendLine(",CONSTRAINT u_ForDelCobniaigo UNIQUE NONCLUSTERED (ConsecutivoCompania,Codigo)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT FormaDelCobro.ConsecutivoCompania, FormaDelCobro.Consecutivo, FormaDelCobro.Codigo, FormaDelCobro.Nombre");
            SQL.AppendLine(", FormaDelCobro.TipoDePago, " + DbSchema + ".Gv_EnumTipoDeFormaDePago.StrValue AS TipoDePagoStr, FormaDelCobro.CodigoCuentaBancaria, FormaDelCobro.CodigoMoneda, FormaDelCobro.CodigoTheFactory, FormaDelCobro.Origen, " + DbSchema + ".Gv_EnumOrigen.StrValue AS OrigenStr");
            SQL.AppendLine(", FormaDelCobro.fldTimeStamp, CAST(FormaDelCobro.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".FormaDelCobro");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoDeFormaDePago");
            SQL.AppendLine("ON " + DbSchema + ".FormaDelCobro.TipoDePago COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDeFormaDePago.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumOrigen");
            SQL.AppendLine("ON " + DbSchema + ".FormaDelCobro.Origen COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumOrigen.DbValue");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(5) + " = '',");
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(50) + " = '',");
            SQL.AppendLine("@TipoDePago" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@CodigoCuentaBancaria" + InsSql.VarCharTypeForDb(5) + "= '',");
            SQL.AppendLine("@CodigoMoneda" + InsSql.VarCharTypeForDb(4) + ",");
            SQL.AppendLine("@CodigoTheFactory" + InsSql.VarCharTypeForDb(2) + " = '01',");
            SQL.AppendLine("@Origen" + InsSql.CharTypeForDb(1) + " = '0'");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".FormaDelCobro(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            Codigo,");
            SQL.AppendLine("            Nombre,");
            SQL.AppendLine("            TipoDePago,");
            SQL.AppendLine("            CodigoCuentaBancaria,");
            SQL.AppendLine("            CodigoMoneda,");
            SQL.AppendLine("            CodigoTheFactory,");
            SQL.AppendLine("            Origen)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @Codigo,");
            SQL.AppendLine("            @Nombre,");
            SQL.AppendLine("            @TipoDePago,");
            SQL.AppendLine("            @CodigoCuentaBancaria,");
            SQL.AppendLine("            @CodigoMoneda,");
            SQL.AppendLine("            @CodigoTheFactory,");
            SQL.AppendLine("            @Origen)");
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
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("@TipoDePago" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@CodigoCuentaBancaria" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@CodigoMoneda" + InsSql.VarCharTypeForDb(4) + ",");
            SQL.AppendLine("@CodigoTheFactory" + InsSql.VarCharTypeForDb(2) + ",");
            SQL.AppendLine("@Origen" + InsSql.CharTypeForDb(1) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".FormaDelCobro WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".FormaDelCobro WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_FormaDelCobroCanBeUpdated @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".FormaDelCobro");
            SQL.AppendLine("            SET Codigo = @Codigo,");
            SQL.AppendLine("               Nombre = @Nombre,");
            SQL.AppendLine("               TipoDePago = @TipoDePago,");
            SQL.AppendLine("               CodigoCuentaBancaria = @CodigoCuentaBancaria,");
            SQL.AppendLine("               CodigoMoneda = @CodigoMoneda,");
            SQL.AppendLine("               CodigoTheFactory = @CodigoTheFactory,");
            SQL.AppendLine("               Origen = @Origen");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".FormaDelCobro WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".FormaDelCobro WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_FormaDelCobroCanBeDeleted @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".FormaDelCobro");
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
            SQL.AppendLine("         FormaDelCobro.ConsecutivoCompania,");
            SQL.AppendLine("         FormaDelCobro.Consecutivo,");
            SQL.AppendLine("         FormaDelCobro.Codigo,");
            SQL.AppendLine("         FormaDelCobro.Nombre,");
            SQL.AppendLine("         FormaDelCobro.TipoDePago,");
            SQL.AppendLine("         FormaDelCobro.CodigoCuentaBancaria,");
            SQL.AppendLine("         FormaDelCobro.CodigoMoneda,");
            SQL.AppendLine("         FormaDelCobro.CodigoTheFactory,");
            SQL.AppendLine("         FormaDelCobro.Origen,");
            SQL.AppendLine("         CAST(FormaDelCobro.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         FormaDelCobro.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".FormaDelCobro");
            SQL.AppendLine("      WHERE FormaDelCobro.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND FormaDelCobro.Consecutivo = @Consecutivo");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_FormaDelCobro_B1.Codigo,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_FormaDelCobro_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_FormaDelCobro_B1.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_FormaDelCobro_B1.Nombre,");
            SQL.AppendLine("      " + DbSchema + ".Gv_FormaDelCobro_B1.TipoDePago,");
            SQL.AppendLine("      " + DbSchema + ".Gv_FormaDelCobro_B1.CodigoCuentaBancaria,");
            SQL.AppendLine("      " + DbSchema + ".Gv_FormaDelCobro_B1.CodigoMoneda,");
            SQL.AppendLine("      " + DbSchema + ".Gv_FormaDelCobro_B1.CodigoTheFactory");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_FormaDelCobro_B1");
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
            SQL.AppendLine("      " + DbSchema + ".FormaDelCobro.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".FormaDelCobro.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".FormaDelCobro.Codigo,");
            SQL.AppendLine("      " + DbSchema + ".FormaDelCobro.Nombre,");
            SQL.AppendLine("      " + DbSchema + ".FormaDelCobro.CodigoCuentaBancaria,");
            SQL.AppendLine("      " + DbSchema + ".FormaDelCobro.CodigoMoneda");
            //SQL.AppendLine("      ," + DbSchema + ".FormaDelCobro.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".FormaDelCobro");
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
            bool vResult = insDbo.Create(DbSchema + ".FormaDelCobro", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDeFormaDePago", LibTpvCreator.SqlViewStandardEnum(typeof(eFormaDeCobro), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumOrigen", LibTpvCreator.SqlViewStandardEnum(typeof(eOrigen), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_FormaDelCobro_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_FormaDelCobroINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_FormaDelCobroUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_FormaDelCobroDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_FormaDelCobroGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_FormaDelCobroSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_FormaDelCobroGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".FormaDelCobro", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_FormaDelCobroINS");
            vResult = insSp.Drop(DbSchema + ".Gp_FormaDelCobroUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_FormaDelCobroDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_FormaDelCobroGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_FormaDelCobroGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_FormaDelCobroSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_FormaDelCobro_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDeFormaDePago") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumOrigen") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsFormaDelCobroED

} //End of namespace Galac.Saw.Dal.Tablas

