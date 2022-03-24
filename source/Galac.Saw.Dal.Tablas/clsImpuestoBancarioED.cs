using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Dal.Tablas {
    [LibMefDalComponentMetadata(typeof(clsImpuestoBancarioED))]
    public class clsImpuestoBancarioED:LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsImpuestoBancarioED() : base() {
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
            get { return "ImpTransacBancarias"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("ImpTransacBancarias",DbSchema) + " ( ");
            SQL.AppendLine("FechaDeInicioDeVigencia" + InsSql.DateTypeForDb() + " CONSTRAINT FechaDeIni NOT NULL, ");
            SQL.AppendLine("AlicuotaAlDebito" + InsSql.DecimalTypeForDb(25,4) + " CONSTRAINT d_ImpBanAlAlDe DEFAULT (0), ");
            SQL.AppendLine("AlicuotaAlCredito" + InsSql.DecimalTypeForDb(25,4) + " CONSTRAINT d_ImpBanAlAlCr DEFAULT (0), ");
            SQL.AppendLine("AlicuotaC1Al4" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_ImpBanAlC14 DEFAULT (0), ");
            SQL.AppendLine("AlicuotaC5" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_ImpBanAlC5 DEFAULT (0), ");
            SQL.AppendLine("AlicuotaC6" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_ImpBanAlC6 DEFAULT (0), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_ImpTransacBancarias PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(FechaDeInicioDeVigencia ASC)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT FechaDeInicioDeVigencia, AlicuotaAlDebito, AlicuotaAlCredito, AlicuotaC1Al4");
            SQL.AppendLine(", AlicuotaC5, AlicuotaC6");
            SQL.AppendLine(", ImpTransacBancarias.fldTimeStamp, CAST(ImpTransacBancarias.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".ImpTransacBancarias");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@FechaDeInicioDeVigencia" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@AlicuotaAlDebito" + InsSql.DecimalTypeForDb(25,4) + " = 0,");
            SQL.AppendLine("@AlicuotaAlCredito" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@AlicuotaC1Al4" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@AlicuotaC5" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@AlicuotaC6" + InsSql.DecimalTypeForDb(25, 4) + " = 0");
            return SQL.ToString();
        }

        private string SqlSpIns() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SET DATEFORMAT @DateFormat");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10,0) + "");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".ImpTransacBancarias(");
            SQL.AppendLine("            FechaDeInicioDeVigencia,");
            SQL.AppendLine("            AlicuotaAlDebito,");
            SQL.AppendLine("            AlicuotaAlCredito,");
            SQL.AppendLine("            AlicuotaC1Al4,");
            SQL.AppendLine("            AlicuotaC5,");
            SQL.AppendLine("            AlicuotaC6)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @FechaDeInicioDeVigencia,");
            SQL.AppendLine("            @AlicuotaAlDebito,");
            SQL.AppendLine("            @AlicuotaAlCredito,");
            SQL.AppendLine("            @AlicuotaC1Al4,");
            SQL.AppendLine("            @AlicuotaC5,");
            SQL.AppendLine("            @AlicuotaC6)");
            SQL.AppendLine("            SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("        COMMIT TRAN");
            SQL.AppendLine("        RETURN @ReturnValue ");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpUpdParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@FechaDeInicioDeVigencia" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@AlicuotaAlDebito" + InsSql.DecimalTypeForDb(25,4) + ",");
            SQL.AppendLine("@AlicuotaAlCredito" + InsSql.DecimalTypeForDb(25,4) + ",");
            SQL.AppendLine("@AlicuotaC1Al4" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@AlicuotaC5" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@AlicuotaC6" + InsSql.DecimalTypeForDb(25, 4) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT FechaDeInicioDeVigencia FROM " + DbSchema + ".ImpTransacBancarias WHERE FechaDeInicioDeVigencia = @FechaDeInicioDeVigencia)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".ImpTransacBancarias WHERE FechaDeInicioDeVigencia = @FechaDeInicioDeVigencia");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            //SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_ImpuestoBancarioCanBeUpdated @FechaDeInicioDeVigencia, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".ImpTransacBancarias");
            SQL.AppendLine("            SET AlicuotaAlDebito = @AlicuotaAlDebito,");
            SQL.AppendLine("               AlicuotaAlCredito = @AlicuotaAlCredito,");
            SQL.AppendLine("               AlicuotaC1Al4 = @AlicuotaC1Al4,");
            SQL.AppendLine("               AlicuotaC5 = @AlicuotaC5,");
            SQL.AppendLine("               AlicuotaC6 = @AlicuotaC6");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND FechaDeInicioDeVigencia = @FechaDeInicioDeVigencia");
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
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@FechaDeInicioDeVigencia" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@TimeStampAsInt" + InsSql.BigintTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpDel() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SET DATEFORMAT @DateFormat");
            SQL.AppendLine("   DECLARE @CurrentTimeStamp timestamp");
            SQL.AppendLine("   DECLARE @ValidationMsg " + InsSql.VarCharTypeForDb(1500) + " --No puede ser más");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10,0) + "");
            //SQL.AppendLine("--DECLARE @CanBeDeleted bit");
            SQL.AppendLine("   SET @ReturnValue = -1");
            SQL.AppendLine("   SET @ValidationMsg = ''");
            SQL.AppendLine("   IF EXISTS(SELECT FechaDeInicioDeVigencia FROM " + DbSchema + ".ImpTransacBancarias WHERE FechaDeInicioDeVigencia = @FechaDeInicioDeVigencia)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".ImpTransacBancarias WHERE FechaDeInicioDeVigencia = @FechaDeInicioDeVigencia");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_ImpTransacBancariasCanBeDeleted @FechaDeInicioDeVigencia, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".ImpTransacBancarias");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND FechaDeInicioDeVigencia = @FechaDeInicioDeVigencia");
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
            SQL.AppendLine("@FechaDeInicioDeVigencia" + InsSql.DateTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         FechaDeInicioDeVigencia,");
            SQL.AppendLine("         AlicuotaAlDebito,");
            SQL.AppendLine("         AlicuotaAlCredito,");
            SQL.AppendLine("         AlicuotaC1Al4,");
            SQL.AppendLine("         AlicuotaC5,");
            SQL.AppendLine("         AlicuotaC6,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".ImpTransacBancarias");
            SQL.AppendLine("      WHERE ImpTransacBancarias.FechaDeInicioDeVigencia = @FechaDeInicioDeVigencia");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSearchParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@SQLWhere" + InsSql.VarCharTypeForDb(2000) + " = null,");
            SQL.AppendLine("@SQLOrderBy" + InsSql.VarCharTypeForDb(500) + " = null,");
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + " = null,");
            SQL.AppendLine("@UseTopClausule" + InsSql.VarCharTypeForDb(1) + " = 'N'");
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
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      AlicuotaAlDebito,");
            SQL.AppendLine("      AlicuotaAlCredito,");
			SQL.AppendLine("      AlicuotaC1Al4,");
            SQL.AppendLine("      AlicuotaC5,");
            SQL.AppendLine("      AlicuotaC6,");
            SQL.AppendLine("      FechaDeInicioDeVigencia");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_ImpTransacBancarias_B1");
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
            SQL.AppendLine("      " + DbSchema + ".ImpTransacBancarias.FechaDeInicioDeVigencia");
            //SQL.AppendLine("      ," + DbSchema + ".ImpTransacBancarias.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".ImpTransacBancarias");
            SQL.AppendLine("      WHERE ");
            SQL.AppendLine("         FechaDeInicioDeVigencia IN (");
            SQL.AppendLine("            SELECT  FechaDeInicioDeVigencia ");
            SQL.AppendLine("            FROM OPENXML( @hdoc, 'GpData/GpResult',2) ");
            SQL.AppendLine("            WITH (FechaDeInicioDeVigencia smalldatetime) AS XmlFKTmp) ");
            SQL.AppendLine(" EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInstParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@FechaDeInicioDeVigencia" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@AlicuotaAlDebito" + InsSql.DecimalTypeForDb(25,4) + ",");
            SQL.AppendLine("@AlicuotaAlCredito" + InsSql.DecimalTypeForDb(25,4) + ",");
			SQL.AppendLine("@AlicuotaC1Al4" + InsSql.DecimalTypeForDb(25,4) + ",");
			SQL.AppendLine("@AlicuotaC5" + InsSql.DecimalTypeForDb(25,4) + ",");
			SQL.AppendLine("@AlicuotaC6" + InsSql.DecimalTypeForDb(25,4));
			
            return SQL.ToString();
        }

        private string SqlSpInst() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".ImpTransacBancarias");
            SQL.AppendLine("            SET AlicuotaAlDebito = @AlicuotaAlDebito,");
            SQL.AppendLine("               AlicuotaAlCredito = @AlicuotaAlCredito,");
			SQL.AppendLine("               AlicuotaC1Al4 = @AlicuotaC1Al4,");
			SQL.AppendLine("               AlicuotaC5 = @AlicuotaC5,");
			SQL.AppendLine("               AlicuotaC6 = @AlicuotaC6");
            SQL.AppendLine("               WHERE FechaDeInicioDeVigencia = @FechaDeInicioDeVigencia");
            SQL.AppendLine("	IF @@ROWCOUNT = 0");
            SQL.AppendLine("        INSERT INTO " + DbSchema + ".ImpTransacBancarias(");
            SQL.AppendLine("            FechaDeInicioDeVigencia,");
            SQL.AppendLine("            AlicuotaAlDebito,");
			SQL.AppendLine("            AlicuotaAlCredito,");
			SQL.AppendLine("            AlicuotaC1Al4,");
			SQL.AppendLine("            AlicuotaC5,");
            SQL.AppendLine("            AlicuotaC6)");
            SQL.AppendLine("        VALUES(");
            SQL.AppendLine("            @FechaDeInicioDeVigencia,");
            SQL.AppendLine("            @AlicuotaAlDebito,");
			SQL.AppendLine("            @AlicuotaAlCredito,");
			SQL.AppendLine("            @AlicuotaC1Al4,");
			SQL.AppendLine("            @AlicuotaC5,");
            SQL.AppendLine("            @AlicuotaC6)");
            SQL.AppendLine(" 	IF @@ERROR = 0");
            SQL.AppendLine(" 		COMMIT TRAN");
            SQL.AppendLine(" 	ELSE");
            SQL.AppendLine(" 		ROLLBACK");
            SQL.AppendLine("END ");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".ImpTransacBancarias",SqlCreateTable(),false,eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas() {
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_ImpTransacBancarias_B1",SqlViewB1(),true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ImpTransacBancariasINS",SqlSpInsParameters(),SqlSpIns(),true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ImpTransacBancariasUPD",SqlSpUpdParameters(),SqlSpUpd(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ImpTransacBancariasDEL",SqlSpDelParameters(),SqlSpDel(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ImpTransacBancariasGET",SqlSpGetParameters(),SqlSpGet(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ImpTransacBancariasSCH",SqlSpSearchParameters(),SqlSpSearch(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ImpTransacBancariasGetFk",SqlSpGetFKParameters(),SqlSpGetFK(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ImpTransacBancariasINST",SqlSpInstParameters(),SqlSpInst(),true) && vResult;
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
            if(insDbo.Exists(DbSchema + ".ImpTransacBancarias",eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_ImpTransacBancariasINS");
            vResult = insSp.Drop(DbSchema + ".Gp_ImpTransacBancariasUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ImpTransacBancariasDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ImpTransacBancariasGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ImpTransacBancariasGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ImpTransacBancariasSCH") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ImpTransacBancariasINST") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_ImpTransacBancarias_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsImpuestoBancarioED

} //End of namespace Galac.Saw.Dal.Tablas

