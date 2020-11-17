using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Dal.Banco {
    public class clsSolicitudesDePagoED: LibED {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsSolicitudesDePagoED(): base(){
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("SolicitudesDePago", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnSolDePagConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoSolicitud" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnSolDePagConsecutiv NOT NULL, ");
            SQL.AppendLine("NumeroDocumentoOrigen" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnSolDePagNumeroDocu NOT NULL, ");
            SQL.AppendLine("FechaSolicitud" + InsSql.DateTypeForDb() + " CONSTRAINT nnSolDePagFechaSolic NOT NULL, ");
            SQL.AppendLine("Status" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnSolDePagStatus NOT NULL, ");
            SQL.AppendLine("GeneradoPor" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnSolDePagGeneradoPo NOT NULL, ");
            SQL.AppendLine("Observaciones" + InsSql.VarCharTypeForDb(40) + " CONSTRAINT d_SolDePagOb DEFAULT (''), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(20) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_SolicitudesDePago PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, ConsecutivoSolicitud ASC)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT ConsecutivoCompania, ConsecutivoSolicitud, NumeroDocumentoOrigen, FechaSolicitud");
            SQL.AppendLine(", Status, " + DbSchema + ".Gv_EnumStatusSolicitud.StrValue AS StatusStr, GeneradoPor, " + DbSchema + ".Gv_EnumSolicitudGeneradaPor.StrValue AS GeneradoPorStr, Observaciones, NombreOperador");
            SQL.AppendLine(", FechaUltimaModificacion");
            SQL.AppendLine(", SolicitudesDePago.fldTimeStamp, CAST(SolicitudesDePago.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine(" FROM " + DbSchema + ".SolicitudesDePago");
            SQL.AppendLine(" INNER JOIN " + DbSchema + ".Gv_EnumStatusSolicitud");
            SQL.AppendLine(" ON " + DbSchema + ".SolicitudesDePago.Status COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumStatusSolicitud.DbValue");
            SQL.AppendLine(" INNER JOIN " + DbSchema + ".Gv_EnumSolicitudGeneradaPor");
            SQL.AppendLine(" ON " + DbSchema + ".SolicitudesDePago.GeneradoPor COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumSolicitudGeneradaPor.DbValue");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoSolicitud" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroDocumentoOrigen" + InsSql.NumericTypeForDb(10, 0) + " = 0,");
            SQL.AppendLine("@FechaSolicitud" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@Status" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@GeneradoPor" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Observaciones" + InsSql.VarCharTypeForDb(40) + " = '',");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(20) + " = '',");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".SolicitudesDePago(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            ConsecutivoSolicitud,");
            SQL.AppendLine("            NumeroDocumentoOrigen,");
            SQL.AppendLine("            FechaSolicitud,");
            SQL.AppendLine("            Status,");
            SQL.AppendLine("            GeneradoPor,");
            SQL.AppendLine("            Observaciones,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @ConsecutivoSolicitud,");
            SQL.AppendLine("            @NumeroDocumentoOrigen,");
            SQL.AppendLine("            @FechaSolicitud,");
            SQL.AppendLine("            @Status,");
            SQL.AppendLine("            @GeneradoPor,");
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
            SQL.AppendLine("@ConsecutivoSolicitud" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroDocumentoOrigen" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@FechaSolicitud" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@Status" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@GeneradoPor" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Observaciones" + InsSql.VarCharTypeForDb(40) + ",");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(20) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".SolicitudesDePago WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoSolicitud = @ConsecutivoSolicitud)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".SolicitudesDePago WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoSolicitud = @ConsecutivoSolicitud");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_SolicitudesDePagoCanBeUpdated @ConsecutivoCompania,@ConsecutivoSolicitud, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".SolicitudesDePago");
            SQL.AppendLine("            SET NumeroDocumentoOrigen = @NumeroDocumentoOrigen,");
            SQL.AppendLine("               FechaSolicitud = @FechaSolicitud,");
            SQL.AppendLine("               Status = @Status,");
            SQL.AppendLine("               GeneradoPor = @GeneradoPor,");
            SQL.AppendLine("               Observaciones = @Observaciones,");
            SQL.AppendLine("               NombreOperador = @NombreOperador,");
            SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoSolicitud = @ConsecutivoSolicitud");
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
            SQL.AppendLine("@ConsecutivoSolicitud" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".SolicitudesDePago WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoSolicitud = @ConsecutivoSolicitud)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".SolicitudesDePago WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoSolicitud = @ConsecutivoSolicitud");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_SolicitudesDePagoCanBeDeleted @ConsecutivoCompania,@ConsecutivoSolicitud, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".SolicitudesDePago");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoSolicitud = @ConsecutivoSolicitud");
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
            SQL.AppendLine("@ConsecutivoSolicitud" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         ConsecutivoSolicitud,");
            SQL.AppendLine("         NumeroDocumentoOrigen,");
            SQL.AppendLine("         FechaSolicitud,");
            SQL.AppendLine("         Status,");
            SQL.AppendLine("         GeneradoPor,");
            SQL.AppendLine("         Observaciones,");
            SQL.AppendLine("         NombreOperador,");
            SQL.AppendLine("         FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".SolicitudesDePago");
            SQL.AppendLine("      WHERE SolicitudesDePago.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND SolicitudesDePago.ConsecutivoSolicitud = @ConsecutivoSolicitud");
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
            SQL.AppendLine("      NumeroDocumentoOrigen,");
            SQL.AppendLine("      FechaSolicitud,");
            SQL.AppendLine("      StatusStr,");
            SQL.AppendLine("      GeneradoPorStr,");
            SQL.AppendLine("      Observaciones,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      ConsecutivoCompania,");
            SQL.AppendLine("      ConsecutivoSolicitud,");
            SQL.AppendLine("      Status,");
            SQL.AppendLine("      GeneradoPor");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_SolicitudesDePago_B1");
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
            SQL.AppendLine("      " + DbSchema + ".SolicitudesDePago.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".SolicitudesDePago.ConsecutivoSolicitud,");
            SQL.AppendLine("      " + DbSchema + ".SolicitudesDePago.NumeroDocumentoOrigen,");
            SQL.AppendLine("      " + DbSchema + ".SolicitudesDePago.FechaSolicitud,");
            SQL.AppendLine("      " + DbSchema + ".SolicitudesDePago.GeneradoPor");
            SQL.AppendLine("      FROM " + DbSchema + ".SolicitudesDePago");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries
        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".SolicitudesDePago", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }
        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumStatusSolicitud", LibTpvCreator.SqlViewStandardEnum(typeof(eStatusSolicitud), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumSolicitudGeneradaPor", LibTpvCreator.SqlViewStandardEnum(typeof(eSolicitudGeneradaPor), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_SolicitudesDePago_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }
        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SolicitudesDePagoINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SolicitudesDePagoUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SolicitudesDePagoDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SolicitudesDePagoGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SolicitudesDePagoSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SolicitudesDePagoGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_SolicitudesDePagoGETNumeroDocumentoOrigen", SqlSpGetNumeroDocumentoOrigenParameters(), SqlSpGetNumeroDocumentoOrigen(), true) && vResult;
            insSps.Dispose();
            return vResult;
        }
        public bool InstalarTabla() {
            bool vResult = false;
            if (CrearTabla()) {
                CrearVistas();
                CrearProcedimientos();
                clsRenglonSolicitudesDePagoED insDetailRenSolDePag = new clsRenglonSolicitudesDePagoED();
                vResult = insDetailRenSolDePag.InstalarTabla();
            }
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = false;
            if (insDbo.Exists(DbSchema + ".SolicitudesDePago", eDboType.Tabla)) {
                CrearVistas();
                CrearProcedimientos();
                vResult = new clsRenglonSolicitudesDePagoED().InstalarVistasYSps();
            }
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = false;
            LibStoredProc insSp = new LibStoredProc();
            LibViews insVista = new LibViews();
            vResult = insSp.Drop(DbSchema + ".Gp_SolicitudesDePagoINS");
            vResult = insSp.Drop(DbSchema + ".Gp_SolicitudesDePagoUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_SolicitudesDePagoDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_SolicitudesDePagoGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_SolicitudesDePagoGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_SolicitudesDePagoSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_SolicitudesDePago_B1") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_SolicitudesDePagoGETNumeroDocumentoOrigen") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados



        private string SqlSpGetNumeroDocumentoOrigenParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroDocumentoOrigen" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGetNumeroDocumentoOrigen() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         ConsecutivoSolicitud,");
            SQL.AppendLine("         NumeroDocumentoOrigen,");
            SQL.AppendLine("         FechaSolicitud,");
            SQL.AppendLine("         Status,");
            SQL.AppendLine("         GeneradoPor,");
            SQL.AppendLine("         Observaciones,");
            SQL.AppendLine("         NombreOperador,");
            SQL.AppendLine("         FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".SolicitudesDePago");
            SQL.AppendLine("      WHERE SolicitudesDePago.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND SolicitudesDePago.NumeroDocumentoOrigen = @NumeroDocumentoOrigen");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
    } //End of class clsSolicitudesDePagoED

} //End of namespace Galac.Adm.Dal.Banco

