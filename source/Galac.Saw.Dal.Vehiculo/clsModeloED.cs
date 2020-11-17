using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Saw.Ccl.Vehiculo;

namespace Galac.Saw.Dal.Vehiculo {
   [LibMefDalComponentMetadata(typeof(clsModeloED))]
   public class clsModeloED : LibED, ILibMefDalComponent {
      #region Variables
      #endregion //Variables
      #region Propiedades
      #endregion //Propiedades
      #region Constructores
      public clsModeloED()
         : base() {
         DbSchema = "Saw";
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
         get { return "Modelo"; }
      }
      bool ILibMefDalComponent.InstallTable() {
         return InstalarTabla();
      }
      #endregion
      #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("Modelo", DbSchema) + " ( ");
            SQL.AppendLine("Nombre" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT nnModNombre NOT NULL, ");
            SQL.AppendLine("Marca" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT nnModMarca NOT NULL, ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_Modelo PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(Nombre ASC)");
            SQL.AppendLine(", CONSTRAINT fk_ModeloMarca FOREIGN KEY (Marca)");
            SQL.AppendLine("REFERENCES Saw.Marca(Nombre)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT Modelo.Nombre, Modelo.Marca");
            SQL.AppendLine(", Modelo.fldTimeStamp, CAST(Modelo.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".Modelo");
            SQL.AppendLine("INNER JOIN Saw.Marca ON  " + DbSchema + ".Modelo.Marca = Saw.Marca.Nombre");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@Marca" + InsSql.VarCharTypeForDb(20) + "");
            return SQL.ToString();
        }
        private string SqlSpIns() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".Modelo(");
            SQL.AppendLine("            Nombre,");
            SQL.AppendLine("            Marca)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @Nombre,");
            SQL.AppendLine("            @Marca)");
            SQL.AppendLine("            SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("        COMMIT TRAN");
            SQL.AppendLine("        RETURN @ReturnValue ");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpUpdParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@Marca" + InsSql.VarCharTypeForDb(20) + ",");
         SQL.AppendLine("@NombreOriginal" + InsSql.VarCharTypeForDb(20) + ",");
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
         SQL.AppendLine("   IF EXISTS(SELECT Nombre FROM " + DbSchema + ".Modelo WHERE Nombre = @NombreOriginal)");
         SQL.AppendLine("   BEGIN");
         SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Modelo WHERE Nombre = @NombreOriginal");
         SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
         SQL.AppendLine("      BEGIN");
         SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_ModeloCanBeUpdated @Nombre, @CurrentTimeStamp, @ValidationMsg out");
         //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
         //SQL.AppendLine("--BEGIN");
         SQL.AppendLine("         BEGIN TRAN");
         SQL.AppendLine("         UPDATE " + DbSchema + ".Modelo");
         SQL.AppendLine("            SET Nombre = @Nombre,");
         SQL.AppendLine("            Marca = @Marca");
         SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
         SQL.AppendLine("               AND Nombre = @NombreOriginal");
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
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(20) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT Nombre FROM " + DbSchema + ".Modelo WHERE Nombre = @Nombre)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Modelo WHERE Nombre = @Nombre");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_ModeloCanBeDeleted @Nombre, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".Modelo");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND Nombre = @Nombre");
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
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(20));
            return SQL.ToString();
        }
        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         Modelo.Nombre,");
            SQL.AppendLine("         Modelo.Marca,");
            SQL.AppendLine("         CAST(Modelo.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         Modelo.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".Modelo");
            SQL.AppendLine("             INNER JOIN Saw.Gv_Marca_B1 ON " + DbSchema + ".Modelo.Marca = Saw.Gv_Marca_B1.Nombre");
            SQL.AppendLine("      WHERE Modelo.Nombre = @Nombre");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_Modelo_B1.Nombre,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Modelo_B1.Marca,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Modelo_B1");
            SQL.AppendLine("      INNER JOIN Saw.Gv_Marca_B1 ON  " + DbSchema + ".Gv_Modelo_B1.Marca = Saw.Gv_Marca_B1.Nombre");
            SQL.AppendLine("'   IF (NOT @SQLWhere IS NULL) AND (@SQLWhere <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' WHERE ' + @SQLWhere");
            SQL.AppendLine("   IF (NOT @SQLOrderBy IS NULL) AND (@SQLOrderBy <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' ORDER BY ' + @SQLOrderBy");
            SQL.AppendLine("   EXEC(@strSQL)");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        private string SqlSpGetFKParameters() {
            return "";
        }

        private string SqlSpGetFK() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("      " + DbSchema + ".Modelo.Nombre,");
            SQL.AppendLine("      " + DbSchema + ".Modelo.Marca");
            //SQL.AppendLine("      ," + DbSchema + ".Modelo.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".Modelo");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries
        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".Modelo", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }
        bool CrearVistas() {
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_Modelo_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }
        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ModeloINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ModeloUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ModeloDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ModeloGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ModeloSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ModeloGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".Modelo", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_ModeloINS");
            vResult = insSp.Drop(DbSchema + ".Gp_ModeloUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ModeloDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ModeloGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ModeloGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ModeloSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_Modelo_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsModeloED

} //End of namespace Galac.Saw.Dal.Vehiculo

