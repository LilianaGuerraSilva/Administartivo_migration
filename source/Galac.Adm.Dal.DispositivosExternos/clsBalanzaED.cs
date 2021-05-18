using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using Galac.Adm.Ccl.DispositivosExternos;
using LibGalac.Aos.Dal.Contracts;

namespace Galac.Adm.Dal.DispositivosExternos {
    [LibMefDalComponentMetadata(typeof(clsBalanzaED))]
    public class clsBalanzaED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsBalanzaED(): base(){
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
            get { return "Balanza"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("Balanza", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnBalConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnBalConsecutiv NOT NULL, ");
            SQL.AppendLine("Modelo" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_BalMo DEFAULT ('0'), ");
            SQL.AppendLine("Nombre" + InsSql.VarCharTypeForDb(40) + " CONSTRAINT d_BalNo DEFAULT (''), ");
            SQL.AppendLine("Puerto" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_BalPu DEFAULT ('0'), ");
            SQL.AppendLine("BitsDatos" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_BalBiDa DEFAULT ('0'), ");
            SQL.AppendLine("Paridad" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_BalPa DEFAULT ('0'), ");
            SQL.AppendLine("BitDeParada" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_BalBiDePa DEFAULT ('0'), ");
            SQL.AppendLine("BaudRate" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_BalBaRa DEFAULT ('0'), ");
            SQL.AppendLine("ControlDeFlujo" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_BalCoDeFl DEFAULT ('0'), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_Balanza PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Consecutivo ASC)");
            SQL.AppendLine(",CONSTRAINT u_Balniaelobre UNIQUE NONCLUSTERED (ConsecutivoCompania,Modelo,Nombre)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT ConsecutivoCompania, Consecutivo, Modelo, " + DbSchema + ".Gv_EnumModeloBalanza.StrValue AS ModeloStr, Nombre");
            SQL.AppendLine(", Puerto, " + DbSchema + ".Gv_EnumPuertoBalanza.StrValue AS PuertoStr, BitsDatos, " + DbSchema + ".Gv_EnumBitsDatos.StrValue AS BitsDatosStr, Paridad, " + DbSchema + ".Gv_EnumParidad.StrValue AS ParidadStr, BitDeParada, " + DbSchema + ".Gv_EnumBitDeParada.StrValue AS BitDeParadaStr");
            SQL.AppendLine(", BaudRate, " + DbSchema + ".Gv_EnumBaudRate.StrValue AS BaudRateStr, ControlDeFlujo, " + DbSchema + ".Gv_EnumControlFlujo.StrValue AS ControlDeFlujoStr");
            SQL.AppendLine(", Balanza.fldTimeStamp, CAST(Balanza.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".Balanza");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumModeloBalanza");
            SQL.AppendLine("ON " + DbSchema + ".Balanza.Modelo COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumModeloBalanza.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumPuertoBalanza");
            SQL.AppendLine("ON " + DbSchema + ".Balanza.Puerto COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumPuertoBalanza.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumBitsDatos");
            SQL.AppendLine("ON " + DbSchema + ".Balanza.BitsDatos COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumBitsDatos.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumParidad");
            SQL.AppendLine("ON " + DbSchema + ".Balanza.Paridad COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumParidad.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumBitDeParada");
            SQL.AppendLine("ON " + DbSchema + ".Balanza.BitDeParada COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumBitDeParada.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumBaudRate");
            SQL.AppendLine("ON " + DbSchema + ".Balanza.BaudRate COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumBaudRate.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumControlFlujo");
            SQL.AppendLine("ON " + DbSchema + ".Balanza.ControlDeFlujo COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumControlFlujo.DbValue");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Modelo" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(40) + " = '',");
            SQL.AppendLine("@Puerto" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@BitsDatos" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Paridad" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@BitDeParada" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@BaudRate" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@ControlDeFlujo" + InsSql.CharTypeForDb(1) + " = '0'");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".Balanza(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            Modelo,");
            SQL.AppendLine("            Nombre,");
            SQL.AppendLine("            Puerto,");
            SQL.AppendLine("            BitsDatos,");
            SQL.AppendLine("            Paridad,");
            SQL.AppendLine("            BitDeParada,");
            SQL.AppendLine("            BaudRate,");
            SQL.AppendLine("            ControlDeFlujo)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @Modelo,");
            SQL.AppendLine("            @Nombre,");
            SQL.AppendLine("            @Puerto,");
            SQL.AppendLine("            @BitsDatos,");
            SQL.AppendLine("            @Paridad,");
            SQL.AppendLine("            @BitDeParada,");
            SQL.AppendLine("            @BaudRate,");
            SQL.AppendLine("            @ControlDeFlujo)");
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
            SQL.AppendLine("@Modelo" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(40) + ",");
            SQL.AppendLine("@Puerto" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@BitsDatos" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Paridad" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@BitDeParada" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@BaudRate" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ControlDeFlujo" + InsSql.CharTypeForDb(1) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Balanza WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Balanza WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_BalanzaCanBeUpdated @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".Balanza");
            SQL.AppendLine("            SET Modelo = @Modelo,");
            SQL.AppendLine("               Nombre = @Nombre,");
            SQL.AppendLine("               Puerto = @Puerto,");
            SQL.AppendLine("               BitsDatos = @BitsDatos,");
            SQL.AppendLine("               Paridad = @Paridad,");
            SQL.AppendLine("               BitDeParada = @BitDeParada,");
            SQL.AppendLine("               BaudRate = @BaudRate,");
            SQL.AppendLine("               ControlDeFlujo = @ControlDeFlujo");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Balanza WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Balanza WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_BalanzaCanBeDeleted @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".Balanza");
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
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         Consecutivo,");
            SQL.AppendLine("         Modelo,");
            SQL.AppendLine("         Nombre,");
            SQL.AppendLine("         Puerto,");
            SQL.AppendLine("         BitsDatos,");
            SQL.AppendLine("         Paridad,");
            SQL.AppendLine("         BitDeParada,");
            SQL.AppendLine("         BaudRate,");
            SQL.AppendLine("         ControlDeFlujo,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".Balanza");
            SQL.AppendLine("      WHERE Balanza.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND Balanza.Consecutivo = @Consecutivo");
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
            SQL.AppendLine("      Nombre,");
            //SQL.AppendLine("--   ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      ConsecutivoCompania,");
            SQL.AppendLine("      Consecutivo,");
            SQL.AppendLine("      Nombre,");
            SQL.AppendLine("      Puerto,");
            SQL.AppendLine("      Modelo");           
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Balanza_B1");
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
            SQL.AppendLine("      " + DbSchema + ".Balanza.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Balanza.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Balanza.Nombre,");
            SQL.AppendLine("      " + DbSchema + ".Balanza.Puerto,");
            SQL.AppendLine("      " + DbSchema + ".Balanza.Modelo");
            //SQL.AppendLine("      ," + DbSchema + ".Balanza.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".Balanza");
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
            bool vResult = insDbo.Create(DbSchema + ".Balanza", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumModeloBalanza", LibTpvCreator.SqlViewStandardEnum(typeof(eModeloDeBalanza), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumPuertoBalanza", LibTpvCreator.SqlViewStandardEnum(typeof(ePuerto), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumBitsDatos", LibTpvCreator.SqlViewStandardEnum(typeof(eBitsDeDatos), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumParidad", LibTpvCreator.SqlViewStandardEnum(typeof(eParidad), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumBitDeParada", LibTpvCreator.SqlViewStandardEnum(typeof(eBitsDeParada), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumBaudRate", LibTpvCreator.SqlViewStandardEnum(typeof(eBaudRate), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumControlFlujo", LibTpvCreator.SqlViewStandardEnum(typeof(eControlDeFlujo), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_Balanza_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_BalanzaINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_BalanzaUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_BalanzaDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_BalanzaGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_BalanzaSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_BalanzaGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".Balanza", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_BalanzaINS");
            vResult = insSp.Drop(DbSchema + ".Gp_BalanzaUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_BalanzaDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_BalanzaGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_BalanzaGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_BalanzaSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_Balanza_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumModeloBalanza") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumPuertoBalanza") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumBitsDatos") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumParidad") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumBitDeParada") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumBaudRate") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumControlFlujo") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsBalanzaED

} //End of namespace Galac.Adm.Dal.DispositivosExternos

