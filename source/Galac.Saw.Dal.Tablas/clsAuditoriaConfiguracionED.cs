using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Dal.Tablas {
    [LibMefDalComponentMetadata(typeof(clsAuditoriaConfiguracionED))]
    public class clsAuditoriaConfiguracionED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsAuditoriaConfiguracionED(): base(){
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
            get { return "AuditoriaConfiguracion"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("AuditoriaConfiguracion", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnAudConConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoAuditoria" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnAudConConsecutiv NOT NULL, ");
            SQL.AppendLine("VersionPrograma" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT nnAudConVersionPro NOT NULL, ");
            SQL.AppendLine("FechayHora" + InsSql.DateTypeForDb() + " CONSTRAINT d_AudConFeyHoAu DEFAULT (GETDATE()), ");
            SQL.AppendLine("Accion" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_AudConAc DEFAULT (''), ");
            SQL.AppendLine("Motivo" + InsSql.VarCharTypeForDb(255) + " CONSTRAINT d_AudConMo DEFAULT (''), ");
            SQL.AppendLine("ConfiguracionOriginal" + InsSql.VarCharTypeForDb(500) + " CONSTRAINT d_AudConCoOr DEFAULT (''), ");
            SQL.AppendLine("ConfiguracionNueva" + InsSql.VarCharTypeForDb(500) + " CONSTRAINT d_AudConCoNu DEFAULT (''), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_AuditoriaConfiguracion PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, ConsecutivoAuditoria ASC, VersionPrograma ASC)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlSpInsParameters(bool valEsParaSpAuditar) {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoAuditoria" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@VersionPrograma" + InsSql.VarCharTypeForDb(10) + ",");
            if (!valEsParaSpAuditar) {
                SQL.AppendLine("@FechayHora" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            }
            SQL.AppendLine("@Accion" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@Motivo" + InsSql.VarCharTypeForDb(255) + " = '',");
            SQL.AppendLine("@ConfiguracionOriginal" + InsSql.VarCharTypeForDb(500) + " = '',");
            SQL.AppendLine("@ConfiguracionNueva" + InsSql.VarCharTypeForDb(500) + " = '',");
            SQL.Append("@NombreOperador" + InsSql.VarCharTypeForDb(10) + " = ''");
            if (!valEsParaSpAuditar) {
                SQL.AppendLine(",");
                SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + " = '01/01/1900'");
            } else {
                SQL.AppendLine();
            }
            return SQL.ToString();
        }

        private string SqlSpIns(bool valEsParaSpAuditar) {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SET DATEFORMAT @DateFormat");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".AuditoriaConfiguracion(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            ConsecutivoAuditoria,");
            SQL.AppendLine("            VersionPrograma,");
            if (!valEsParaSpAuditar) {
                SQL.AppendLine("            FechayHora,");
            }
            SQL.AppendLine("            Accion,");
            SQL.AppendLine("            Motivo,");
            SQL.AppendLine("            ConfiguracionOriginal,");
            SQL.AppendLine("            ConfiguracionNueva,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @ConsecutivoAuditoria,");
            SQL.AppendLine("            @VersionPrograma,");
            if (!valEsParaSpAuditar) {
                SQL.AppendLine("            @FechayHora,");
            }
            SQL.AppendLine("            @Accion,");
            SQL.AppendLine("            @Motivo,");
            SQL.AppendLine("            @ConfiguracionOriginal,");
            SQL.AppendLine("            @ConfiguracionNueva,");
            SQL.AppendLine("            @NombreOperador,");
            if (!valEsParaSpAuditar) {
                SQL.AppendLine("            @FechaUltimaModificacion)");
            } else {
                SQL.AppendLine("            CAST(GETDATE() as date))");
            }
            SQL.AppendLine("            SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("        COMMIT TRAN");
            SQL.AppendLine("        RETURN @ReturnValue ");
            SQL.AppendLine("	END");
            SQL.AppendLine("	ELSE");
            SQL.AppendLine("		RETURN -1");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".AuditoriaConfiguracion", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        
        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AuditoriaConfiguracionINS", SqlSpInsParameters(false), SqlSpIns(false), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AuditoriaConfiguracionAUD", SqlSpInsParameters(true), SqlSpIns(true), true);
            insSps.Dispose();
            return vResult;
        }

        public bool InstalarTabla() {
            bool vResult = false;
            if (CrearTabla()) {
                CrearProcedimientos();
                vResult = true;
            }
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = false;
            if (insDbo.Exists(DbSchema + ".AuditoriaConfiguracion", eDboType.Tabla)) {
                CrearProcedimientos();
                vResult = true;
            }
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = false;
            LibStoredProc insSp = new LibStoredProc();
            LibViews insVista = new LibViews();
            vResult = insSp.Drop(DbSchema + ".Gp_AuditoriaConfiguracionINS");
            vResult = insSp.Drop(DbSchema + ".Gp_AuditoriaConfiguracionAUD");
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsAuditoriaConfiguracionED

} //End of namespace Galac.Saw.Dal.Tablas

