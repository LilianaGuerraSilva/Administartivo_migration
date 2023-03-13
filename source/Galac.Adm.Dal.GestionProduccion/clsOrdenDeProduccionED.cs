using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.GestionProduccion;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Dal.GestionProduccion {
    [LibMefDalComponentMetadata(typeof(clsOrdenDeProduccionED))]
    public class clsOrdenDeProduccionED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsOrdenDeProduccionED(): base(){
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
            get { return "OrdenDeProduccion"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("OrdenDeProduccion", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeProConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeProConsecutiv NOT NULL, ");
            SQL.AppendLine("Codigo" + InsSql.VarCharTypeForDb(15) + " CONSTRAINT nnOrdDeProCodigo NOT NULL, ");
            SQL.AppendLine("Descripcion" + InsSql.VarCharTypeForDb(250) + " CONSTRAINT d_OrdDeProDe DEFAULT (''), ");
            SQL.AppendLine("StatusOp" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_OrdDeProStOp DEFAULT ('0'), ");
            SQL.AppendLine("ConsecutivoAlmacenProductoTerminado" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeProConseProduc NOT NULL, ");
            SQL.AppendLine("ConsecutivoAlmacenMateriales" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeProConseMaterial NOT NULL, ");
            SQL.AppendLine("FechaCreacion" + InsSql.DateTypeForDb() + " CONSTRAINT d_OrdDeProFeCr DEFAULT (''), ");
            SQL.AppendLine("FechaInicio" + InsSql.DateTypeForDb() + " CONSTRAINT d_OrdDeProFeIn DEFAULT (''), ");
            SQL.AppendLine("FechaFinalizacion" + InsSql.DateTypeForDb() + " CONSTRAINT d_OrdDeProFeFi DEFAULT (''), ");
            SQL.AppendLine("FechaAnulacion" + InsSql.DateTypeForDb() + " CONSTRAINT d_OrdDeProFeAn DEFAULT (''), ");
            SQL.AppendLine("FechaAjuste" + InsSql.DateTypeForDb() + " CONSTRAINT d_OrdDeProFeAj DEFAULT (''), ");
            SQL.AppendLine("AjustadaPostCierre" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnOrdDeProAjustadaPo NOT NULL, ");
            SQL.AppendLine("Observacion" + InsSql.VarCharTypeForDb(600) + " CONSTRAINT d_OrdDeProOb DEFAULT (''), ");
            SQL.AppendLine("MotivoDeAnulacion" + InsSql.VarCharTypeForDb(600) + " CONSTRAINT d_OrdDeProMoDeAn DEFAULT (''), ");
            SQL.AppendLine("NumeroDecimales" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnOrdDeProNumDec NOT NULL, ");
            SQL.AppendLine("CostoTerminadoCalculadoAPartirDe" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_OrdDeProCosTerCalAParDe DEFAULT ('0'), ");
            SQL.AppendLine("CodigoMonedaCostoProduccion" + InsSql.VarCharTypeForDb(4) + " CONSTRAINT d_OrdDeProCoMoCoPr DEFAULT (''), ");
            SQL.AppendLine("CambioCostoProduccion" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_OrdDeProCaCoPr DEFAULT (1), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(20) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_OrdenDeProduccion PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Consecutivo ASC)");
            SQL.AppendLine(", CONSTRAINT fk_OrdenDeProduccionAlmacenArticuloterminado FOREIGN KEY (ConsecutivoCompania, ConsecutivoAlmacenProductoTerminado)");
            SQL.AppendLine("REFERENCES Saw.Almacen(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_OrdenDeProduccionAlmacenMateriales FOREIGN KEY (ConsecutivoCompania, ConsecutivoAlmacenMateriales)");
            SQL.AppendLine("REFERENCES Saw.Almacen(ConsecutivoCompania, Consecutivo)");            
            SQL.AppendLine(",CONSTRAINT u_OrdDeProConsecutivo UNIQUE NONCLUSTERED (ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine(",CONSTRAINT u_OrdDeProniaigo UNIQUE NONCLUSTERED (ConsecutivoCompania,Codigo)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT OrdenDeProduccion.ConsecutivoCompania, OrdenDeProduccion.Consecutivo, OrdenDeProduccion.Codigo, OrdenDeProduccion.Descripcion");
            SQL.AppendLine(", OrdenDeProduccion.StatusOp, " + DbSchema + ".Gv_EnumTipoStatusOrdenProduccion.StrValue AS StatusOpStr, OrdenDeProduccion.ConsecutivoAlmacenProductoTerminado, OrdenDeProduccion.ConsecutivoAlmacenMateriales, OrdenDeProduccion.FechaCreacion");
            SQL.AppendLine(", OrdenDeProduccion.FechaInicio, OrdenDeProduccion.FechaFinalizacion, OrdenDeProduccion.FechaAnulacion, OrdenDeProduccion.FechaAjuste, OrdenDeProduccion.AjustadaPostCierre");
            SQL.AppendLine(", OrdenDeProduccion.Observacion, OrdenDeProduccion.MotivoDeAnulacion, OrdenDeProduccion.CostoTerminadoCalculadoAPartirDe");
            SQL.AppendLine(", OrdenDeProduccion.CodigoMonedaCostoProduccion, OrdenDeProduccion.CambioCostoProduccion");
            SQL.AppendLine(", OrdenDeProduccion.NombreOperador, OrdenDeProduccion.FechaUltimaModificacion");
            SQL.AppendLine(", OrdenDeProduccion.fldTimeStamp, CAST(OrdenDeProduccion.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".OrdenDeProduccion");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoStatusOrdenProduccion");
            SQL.AppendLine("ON " + DbSchema + ".OrdenDeProduccion.StatusOp COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoStatusOrdenProduccion.DbValue");
            SQL.AppendLine("INNER JOIN Saw.Almacen  AS APT ON " + DbSchema + ".OrdenDeProduccion.ConsecutivoAlmacenProductoTerminado = APT.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".OrdenDeProduccion.ConsecutivoCompania = APT.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN Saw.Almacen  AS AM ON " + DbSchema + ".OrdenDeProduccion.ConsecutivoAlmacenMateriales = AM.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".OrdenDeProduccion.ConsecutivoCompania = AM.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(15) + " = '',");
            SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(250) + " = '',");
            SQL.AppendLine("@StatusOp" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@ConsecutivoAlmacenProductoTerminado" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoAlmacenMateriales" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@FechaCreacion" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@FechaInicio" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@FechaFinalizacion" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@FechaAnulacion" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@FechaAjuste" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@AjustadaPostCierre" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@Observacion" + InsSql.VarCharTypeForDb(600) + " = '',");
            SQL.AppendLine("@MotivoDeAnulacion" + InsSql.VarCharTypeForDb(600) + " = '',");
            SQL.AppendLine("@NumeroDecimales" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CostoTerminadoCalculadoAPartirDe" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@CodigoMonedaCostoProduccion" + InsSql.VarCharTypeForDb(4) + " = '',");
            SQL.AppendLine("@CambioCostoProduccion" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
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
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".OrdenDeProduccion(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            Codigo,");
            SQL.AppendLine("            Descripcion,");
            SQL.AppendLine("            StatusOp,");
            SQL.AppendLine("            ConsecutivoAlmacenProductoTerminado,");
            SQL.AppendLine("            ConsecutivoAlmacenMateriales,");
            SQL.AppendLine("            FechaCreacion,");
            SQL.AppendLine("            FechaInicio,");
            SQL.AppendLine("            FechaFinalizacion,");
            SQL.AppendLine("            FechaAnulacion,");
            SQL.AppendLine("            FechaAjuste,");
            SQL.AppendLine("            AjustadaPostCierre,");
            SQL.AppendLine("            Observacion,");
            SQL.AppendLine("            MotivoDeAnulacion,");
            SQL.AppendLine("            NumeroDecimales,");
            SQL.AppendLine("            CostoTerminadoCalculadoAPartirDe,");
            SQL.AppendLine("            CodigoMonedaCostoProduccion,");
            SQL.AppendLine("            CambioCostoProduccion,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @Codigo,");
            SQL.AppendLine("            @Descripcion,");
            SQL.AppendLine("            @StatusOp,");
            SQL.AppendLine("            @ConsecutivoAlmacenProductoTerminado,");
            SQL.AppendLine("            @ConsecutivoAlmacenMateriales,");
            SQL.AppendLine("            @FechaCreacion,");
            SQL.AppendLine("            @FechaInicio,");
            SQL.AppendLine("            @FechaFinalizacion,");
            SQL.AppendLine("            @FechaAnulacion,");
            SQL.AppendLine("            @FechaAjuste,");
            SQL.AppendLine("            @AjustadaPostCierre,");
            SQL.AppendLine("            @Observacion,");
            SQL.AppendLine("            @MotivoDeAnulacion,");
            SQL.AppendLine("            @NumeroDecimales,");
            SQL.AppendLine("            @CostoTerminadoCalculadoAPartirDe,");
            SQL.AppendLine("            @CodigoMonedaCostoProduccion,");
            SQL.AppendLine("            @CambioCostoProduccion,");
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
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(15) + ",");
            SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(250) + ",");
            SQL.AppendLine("@StatusOp" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ConsecutivoAlmacenProductoTerminado" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoAlmacenMateriales" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@FechaCreacion" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@FechaInicio" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@FechaFinalizacion" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@FechaAnulacion" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@FechaAjuste" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@AjustadaPostCierre" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Observacion" + InsSql.VarCharTypeForDb(600) + ",");
            SQL.AppendLine("@MotivoDeAnulacion" + InsSql.VarCharTypeForDb(600) + ",");
            SQL.AppendLine("@CostoTerminadoCalculadoAPartirDe" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@CodigoMonedaCostoProduccion" + InsSql.VarCharTypeForDb(4) + ",");
            SQL.AppendLine("@CambioCostoProduccion" + InsSql.DecimalTypeForDb(25, 4) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".OrdenDeProduccion WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".OrdenDeProduccion WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_OrdenDeProduccionCanBeUpdated @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".OrdenDeProduccion");
            SQL.AppendLine("            SET Codigo = @Codigo,");
            SQL.AppendLine("               Descripcion = @Descripcion,");
            SQL.AppendLine("               StatusOp = @StatusOp,");
            SQL.AppendLine("               ConsecutivoAlmacenProductoTerminado = @ConsecutivoAlmacenProductoTerminado,");
            SQL.AppendLine("               ConsecutivoAlmacenMateriales = @ConsecutivoAlmacenMateriales,");
            SQL.AppendLine("               FechaCreacion = @FechaCreacion,");
            SQL.AppendLine("               FechaInicio = @FechaInicio,");
            SQL.AppendLine("               FechaFinalizacion = @FechaFinalizacion,");
            SQL.AppendLine("               FechaAnulacion = @FechaAnulacion,");
            SQL.AppendLine("               FechaAjuste = @FechaAjuste,");
            SQL.AppendLine("               AjustadaPostCierre = @AjustadaPostCierre,");
            SQL.AppendLine("               Observacion = @Observacion,");
            SQL.AppendLine("               MotivoDeAnulacion = @MotivoDeAnulacion,");
            SQL.AppendLine("               CostoTerminadoCalculadoAPartirDe = @CostoTerminadoCalculadoAPartirDe,");
            SQL.AppendLine("               CodigoMonedaCostoProduccion = @CodigoMonedaCostoProduccion,");
            SQL.AppendLine("               CambioCostoProduccion = @CambioCostoProduccion,");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".OrdenDeProduccion WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".OrdenDeProduccion WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_OrdenDeProduccionCanBeDeleted @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".OrdenDeProduccion");
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
			 SQL.AppendLine("         OrdenDeProduccion.ConsecutivoCompania,");
            SQL.AppendLine("         OrdenDeProduccion.Consecutivo,");
            SQL.AppendLine("         OrdenDeProduccion.Codigo,");
            SQL.AppendLine("         OrdenDeProduccion.Descripcion,");
            SQL.AppendLine("         OrdenDeProduccion.StatusOp,");
            SQL.AppendLine("         OrdenDeProduccion.ConsecutivoAlmacenProductoTerminado,");
            SQL.AppendLine("         APT.Codigo AS CodigoAlmacenProductoTerminado,");
            SQL.AppendLine("         APT.NombreAlmacen AS NombreAlmacenProductoTerminado,");
            SQL.AppendLine("         OrdenDeProduccion.ConsecutivoAlmacenMateriales,");
            SQL.AppendLine("         AM.Codigo AS CodigoAlmacenMateriales,");
            SQL.AppendLine("         AM.NombreAlmacen AS NombreAlmacenMateriales,");
            SQL.AppendLine("         OrdenDeProduccion.FechaCreacion,");
            SQL.AppendLine("         OrdenDeProduccion.FechaInicio,");
            SQL.AppendLine("         OrdenDeProduccion.FechaFinalizacion,");
            SQL.AppendLine("         OrdenDeProduccion.FechaAnulacion,");
            SQL.AppendLine("         OrdenDeProduccion.FechaAjuste,");
            SQL.AppendLine("         OrdenDeProduccion.AjustadaPostCierre,");
            SQL.AppendLine("         OrdenDeProduccion.Observacion,");
            SQL.AppendLine("         OrdenDeProduccion.MotivoDeAnulacion,");
            SQL.AppendLine("         OrdenDeProduccion.CostoTerminadoCalculadoAPartirDe,");
            SQL.AppendLine("         OrdenDeProduccion.CodigoMonedaCostoProduccion,");
            SQL.AppendLine("         OrdenDeProduccion.CambioCostoProduccion,");
            SQL.AppendLine("         OrdenDeProduccion.NombreOperador,");
            SQL.AppendLine("         OrdenDeProduccion.FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(OrdenDeProduccion.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         OrdenDeProduccion.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".OrdenDeProduccion");
            SQL.AppendLine("             INNER JOIN Saw.Gv_Almacen_B1 AS APT ON " + DbSchema + ".OrdenDeProduccion.ConsecutivoCompania = APT.ConsecutivoCompania AND " + DbSchema + ".OrdenDeProduccion.ConsecutivoAlmacenProductoTerminado = APT.Consecutivo");
            SQL.AppendLine("             INNER JOIN Saw.Gv_Almacen_B1 AS AM ON " + DbSchema + ".OrdenDeProduccion.ConsecutivoCompania = AM.ConsecutivoCompania AND " + DbSchema + ".OrdenDeProduccion.ConsecutivoAlmacenMateriales = AM.Consecutivo");
            SQL.AppendLine("      WHERE OrdenDeProduccion.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND OrdenDeProduccion.Consecutivo = @Consecutivo");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeProduccion_B1.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeProduccion_B1.Codigo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeProduccion_B1.Descripcion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeProduccion_B1.StatusOpStr,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeProduccion_B1.FechaCreacion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeProduccion_B1.FechaInicio,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeProduccion_B1.FechaFinalizacion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeProduccion_B1.FechaAnulacion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeProduccion_B1.FechaAjuste,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeProduccion_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_OrdenDeProduccion_B1.StatusOp");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_OrdenDeProduccion_B1");
            SQL.AppendLine("      INNER JOIN Saw.Gv_Almacen_B1 AS APT ON  " + DbSchema + ".Gv_OrdenDeProduccion_B1.ConsecutivoAlmacenProductoTerminado = APT.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_OrdenDeProduccion_B1.ConsecutivoCompania = APT.ConsecutivoCompania");
            SQL.AppendLine("      INNER JOIN Saw.Gv_Almacen_B1 AS AM ON  " + DbSchema + ".Gv_OrdenDeProduccion_B1.ConsecutivoAlmacenMateriales = AM.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_OrdenDeProduccion_B1.ConsecutivoCompania = AM.ConsecutivoCompania");
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
            SQL.AppendLine("      " + DbSchema + ".OrdenDeProduccion.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".OrdenDeProduccion.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".OrdenDeProduccion.ConsecutivoAlmacenProductoTerminado,");
            SQL.AppendLine("      " + DbSchema + ".OrdenDeProduccion.ConsecutivoAlmacenMateriales,");
            SQL.AppendLine("      " + DbSchema + ".OrdenDeProduccion.StatusOp");
            //  SQL.AppendLine("      ," + DbSchema + ".OrdenDeProduccion.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".OrdenDeProduccion");
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

        private string SqlSpContabSchParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@SQLWhere" + InsSql.VarCharTypeForDb(2000) + " = null,");
            SQL.AppendLine("@SQLOrderBy" + InsSql.VarCharTypeForDb(500) + " = null,");
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + " = null,");
            SQL.AppendLine("@UseTopClausule" + InsSql.VarCharTypeForDb(1) + " = 'S'");
            return SQL.ToString();
        }

        private string SqlSpContabSch() {
            StringBuilder vSQL = new StringBuilder();
            StringBuilder vSqlComprobantePeriodo = new StringBuilder();
            string vSqlStdSeparator = InsSql.ToSqlValue(LibGalac.Aos.Base.LibText.StandardSeparator());

            vSqlComprobantePeriodo.AppendLine("      (SELECT ISNULL(Comprobante.NoDocumentoOrigen, 0) AS NoDocumentoOrigen, ISNULL(COMPROBANTE.GeneradoPor, " + InsSql.ToSqlValue("") + ") AS GeneradoPor, ISNULL(COMPROBANTE.ConsecutivoDocOrigen, " + InsSql.ToSqlValue("") + ") AS ConsecutivoDocOrigen, ");
            vSqlComprobantePeriodo.AppendLine("      Periodo.ConsecutivoCompania ");
            vSqlComprobantePeriodo.AppendLine("      FROM COMPROBANTE RIGHT JOIN PERIODO ");
            vSqlComprobantePeriodo.AppendLine("          ON  PERIODO.ConsecutivoPeriodo  = COMPROBANTE.ConsecutivoPeriodo ) ");

            vSQL.AppendLine("BEGIN");
            vSQL.AppendLine("   SET NOCOUNT ON;");
            vSQL.AppendLine("   DECLARE @strSQL AS " + InsSql.VarCharTypeForDb(7000));
            vSQL.AppendLine("   DECLARE @TopClausule AS " + InsSql.VarCharTypeForDb(10));
            vSQL.AppendLine("   DECLARE @SqlStatusNumero AS " + InsSql.VarCharTypeForDb(500));

            vSQL.AppendLine("   SET @SqlStatusNumero = 'CAST(Adm.Gv_OrdenDeProduccion_B1.StatusOp AS varchar) + ' + '''' + " + vSqlStdSeparator + " +  '''' +  ' + CAST(Adm.Gv_OrdenDeProduccion_B1.Consecutivo AS varchar) '");

            vSQL.AppendLine("   IF(@UseTopClausule = 'S') ");
            vSQL.AppendLine("    SET @TopClausule = 'TOP 500'");
            vSQL.AppendLine("   ELSE ");
            vSQL.AppendLine("    SET @TopClausule = ''");
            vSQL.AppendLine("   SET @strSQL = ");
            vSQL.AppendLine("    ' SET DateFormat ' + @DateFormat + ");

            vSQL.AppendLine("    ' SELECT ' + @TopClausule + '");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.Codigo,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.StatusOpStr,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.Descripcion,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.ConsecutivoAlmacenProductoTerminado,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.ConsecutivoAlmacenMateriales,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.FechaCreacion,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.FechaInicio,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.FechaFinalizacion,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.FechaAnulacion,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.FechaAjuste,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.AjustadaPostCierre,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.Observacion,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.MotivoDeAnulacion,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.CostoTerminadoCalculadoAPartirDe,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.CodigoMonedaCostoProduccion,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.CambioCostoProduccion,");
            vSQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.ConsecutivoCompania,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.Consecutivo,");
            vSQL.AppendLine("       " + DbSchema + ".Gv_OrdenDeProduccion_B1.StatusOp");

            vSQL.AppendLine("      FROM " + DbSchema + ".Gv_OrdenDeProduccion_B1 ");
            vSQL.AppendLine("      LEFT JOIN " + vSqlComprobantePeriodo + " AS ComprobantePeriodo");
            vSQL.AppendLine("      ON  " + DbSchema + ".Gv_OrdenDeProduccion_B1.Consecutivo = ComprobantePeriodo.ConsecutivoDocOrigen AND ' + @SqlStatusNumero ");
            vSQL.AppendLine("      + ' = ComprobantePeriodo.NoDocumentoOrigen ");
            vSQL.AppendLine("      AND ComprobantePeriodo.ConsecutivoCompania = " + DbSchema + ".Gv_OrdenDeProduccion_B1.ConsecutivoCompania");
            vSQL.AppendLine("      AND ComprobantePeriodo.GeneradoPor = ' + QUOTENAME('P','''') + '");
            vSQL.AppendLine("      AND ComprobantePeriodo.ConsecutivoDocOrigen IS NULL ");

            vSQL.AppendLine("'   IF (NOT @SQLWhere IS NULL) AND (@SQLWhere <> '')");
            vSQL.AppendLine("      SET @strSQL = @strSQL + ' WHERE ' + @SQLWhere ");
            vSQL.AppendLine("   IF (NOT @SQLOrderBy IS NULL) AND (@SQLOrderBy <> '')");
            vSQL.AppendLine("      SET @strSQL = @strSQL + ' ORDER BY ' + @SQLOrderBy");
            vSQL.AppendLine("   EXEC(@strSQL)");
            vSQL.AppendLine("END");

            return vSQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".OrdenDeProduccion", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoStatusOrdenProduccion", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoStatusOrdenProduccion), InsSql), true, true);
			vResult = insVistas.Create(DbSchema + ".Gv_EnumCostoTerminadoCalculadoAPartirDe", LibTpvCreator.SqlViewStandardEnum(typeof(eFormaDeCalcularCostoTerminado), InsSql), true, true);
			vResult = insVistas.Create(DbSchema + ".Gv_OrdenDeProduccion_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_OrdenDeProduccionDatContaSCH", SqlSpContabSchParameters(), SqlSpContabSch(), true) && vResult;
            insSps.Dispose();
            return vResult;
        }

        public bool InstalarTabla() {
            bool vResult = false;
            if (CrearTabla()) {
                CrearVistas();
                CrearProcedimientos();
                clsOrdenDeProduccionDetalleArticuloED insDetailOrdDeProDetArt = new clsOrdenDeProduccionDetalleArticuloED();
                vResult = insDetailOrdDeProDetArt.InstalarTabla();
            }
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = false;
            if (insDbo.Exists(DbSchema + ".OrdenDeProduccion", eDboType.Tabla)) {
                CrearVistas();
                CrearProcedimientos();
                vResult = new clsOrdenDeProduccionDetalleArticuloED().InstalarVistasYSps();
            }
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = false;
            LibStoredProc insSp = new LibStoredProc();
            LibViews insVista = new LibViews();
            vResult = new clsOrdenDeProduccionDetalleArticuloED().BorrarVistasYSps();
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionDatContaSCH") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionINS") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_OrdenDeProduccionSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_OrdenDeProduccion_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoStatusOrdenProduccion") && vResult;
			vResult = insVista.Drop(DbSchema + ".Gv_EnumCostoTerminadoCalculadoAPartirDe") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        
        #endregion //Metodos Generados

    } //End of class clsOrdenDeProduccionED

} //End of namespace Galac.Adm.Dal.GestionProduccion

