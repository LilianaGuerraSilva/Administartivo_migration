using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Dal.GestionCompras {
    [LibMefDalComponentMetadata(typeof(clsCompraDetalleSerialRolloED))]
    public class clsCompraDetalleSerialRolloED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsCompraDetalleSerialRolloED(): base(){
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
            get { return "CompraDetalleSerialRollo"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("CompraDetalleSerialRollo", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnComDetSerRolConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnComDetSerRolConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnComDetSerRolConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoArticulo" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_ComDetSerRolCoAr DEFAULT (''), ");
            SQL.AppendLine("Serial" + InsSql.VarCharTypeForDb(50) + " CONSTRAINT d_ComDetSerRolSe DEFAULT (''), ");
            SQL.AppendLine("Rollo" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_ComDetSerRolRo DEFAULT (''), ");
            SQL.AppendLine("Cantidad" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnComDetSerRolCantidad NOT NULL, ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_CompraDetalleSerialRollo PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, ConsecutivoCompra ASC, Consecutivo ASC)");
            SQL.AppendLine(",CONSTRAINT fk_CompraDetalleSerialRolloCompra FOREIGN KEY (ConsecutivoCompania, ConsecutivoCompra)");
            SQL.AppendLine("REFERENCES Adm.Compra(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT ConsecutivoCompania, ConsecutivoCompra, Consecutivo, CodigoArticulo");
            SQL.AppendLine(", Serial, Rollo, Cantidad");
            SQL.AppendLine(", CompraDetalleSerialRollo.fldTimeStamp, CAST(CompraDetalleSerialRollo.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".CompraDetalleSerialRollo");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + " = '',");
            SQL.AppendLine("@Serial" + InsSql.VarCharTypeForDb(50) + " = '',");
            SQL.AppendLine("@Rollo" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 4) + " = 0");
            return SQL.ToString();
        }

        private string SqlSpIns() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".CompraDetalleSerialRollo(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            ConsecutivoCompra,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            CodigoArticulo,");
            SQL.AppendLine("            Serial,");
            SQL.AppendLine("            Rollo,");
            SQL.AppendLine("            Cantidad)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @ConsecutivoCompra,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @CodigoArticulo,");
            SQL.AppendLine("            @Serial,");
            SQL.AppendLine("            @Rollo,");
            SQL.AppendLine("            @Cantidad)");
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
            SQL.AppendLine("@ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoArticulo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@Serial" + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("@Rollo" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@Cantidad" + InsSql.DecimalTypeForDb(25, 4) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CompraDetalleSerialRollo WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCompra = @ConsecutivoCompra AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CompraDetalleSerialRollo WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCompra = @ConsecutivoCompra AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_CompraDetalleSerialRolloCanBeUpdated @ConsecutivoCompania,@ConsecutivoCompra,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".CompraDetalleSerialRollo");
            SQL.AppendLine("            SET CodigoArticulo = @CodigoArticulo,");
            SQL.AppendLine("               Serial = @Serial,");
            SQL.AppendLine("               Rollo = @Rollo,");
            SQL.AppendLine("               Cantidad = @Cantidad");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoCompra = @ConsecutivoCompra");
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
            SQL.AppendLine("@ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CompraDetalleSerialRollo WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCompra = @ConsecutivoCompra AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CompraDetalleSerialRollo WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCompra = @ConsecutivoCompra AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_CompraDetalleSerialRolloCanBeDeleted @ConsecutivoCompania,@ConsecutivoCompra,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".CompraDetalleSerialRollo");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoCompra = @ConsecutivoCompra");
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
            SQL.AppendLine("@ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         ConsecutivoCompra,");
            SQL.AppendLine("         Consecutivo,");
            SQL.AppendLine("         CodigoArticulo,");
            SQL.AppendLine("         Serial,");
            SQL.AppendLine("         Rollo,");
            SQL.AppendLine("         Cantidad,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".CompraDetalleSerialRollo");
            SQL.AppendLine("      WHERE CompraDetalleSerialRollo.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND CompraDetalleSerialRollo.ConsecutivoCompra = @ConsecutivoCompra");
            SQL.AppendLine("         AND CompraDetalleSerialRollo.Consecutivo = @Consecutivo");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpSelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SELECT ");
            SQL.AppendLine("        ConsecutivoCompania,");
            SQL.AppendLine("        ConsecutivoCompra,");
            SQL.AppendLine("        Consecutivo,");
            SQL.AppendLine("        CodigoArticulo,");
            SQL.AppendLine("        Serial,");
            SQL.AppendLine("        Rollo,");
            SQL.AppendLine("        Cantidad,");
            SQL.AppendLine("        fldTimeStamp");
            SQL.AppendLine("    FROM CompraDetalleSerialRollo");
            SQL.AppendLine(" 	WHERE ConsecutivoCompra = @ConsecutivoCompra");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpDelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	DELETE FROM CompraDetalleSerialRollo");
            SQL.AppendLine(" 	WHERE ConsecutivoCompra = @ConsecutivoCompra");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInsDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@XmlDataDetail" + InsSql.XmlTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpInsDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SET NOCOUNT ON;");
            SQL.AppendLine("	DECLARE @ReturnValue  " + InsSql.NumericTypeForDb(10, 0));
	        SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
	        SQL.AppendLine("	    BEGIN");
            SQL.AppendLine("	    EXEC Adm.Gp_CompraDetalleSerialRolloDelDet @ConsecutivoCompania = @ConsecutivoCompania, @ConsecutivoCompra = @ConsecutivoCompra");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO Adm.CompraDetalleSerialRollo(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        ConsecutivoCompra,");
			SQL.AppendLine("	        Consecutivo,");
			SQL.AppendLine("	        CodigoArticulo,");
			SQL.AppendLine("	        Serial,");
			SQL.AppendLine("	        Rollo,");
			SQL.AppendLine("	        Cantidad)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @ConsecutivoCompra,");
			SQL.AppendLine("	        Consecutivo,");
			SQL.AppendLine("	        CodigoArticulo,");
			SQL.AppendLine("	        Serial,");
			SQL.AppendLine("	        Rollo,");
			SQL.AppendLine("	        Cantidad");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataCompraDetalleSerialRollo/GpDetailCompraDetalleSerialRollo',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        Consecutivo " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        CodigoArticulo " + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("	        Serial " + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("	        Rollo " + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("	        Cantidad " + InsSql.DecimalTypeForDb(25, 4) + ") AS XmlDocDetailOfCompra");
            SQL.AppendLine("	    EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("	    SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("	    RETURN @ReturnValue");
	        SQL.AppendLine("	END");
	        SQL.AppendLine("	ELSE");
            SQL.AppendLine("	    RETURN -1");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlViewRenglonCompraXSerial() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT ConsecutivoCompania, ConsecutivoCompra AS NumeroSecuencialCompra, Consecutivo AS ConsecutivoRenglo, CodigoArticulo, Serial, Rollo, Cantidad  ");
            SQL.AppendLine("FROM " + DbSchema + ".CompraDetalleSerialRollo");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".CompraDetalleSerialRollo", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_CompraDetalleSerialRollo_B1", SqlViewB1(), true);
            
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleSerialRolloINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleSerialRolloUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleSerialRolloDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleSerialRolloGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleSerialRolloSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleSerialRolloDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleSerialRolloInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".CompraDetalleSerialRollo", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleSerialRolloINS");
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleSerialRolloUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleSerialRolloDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleSerialRolloGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleSerialRolloInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleSerialRolloDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleSerialRolloSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_CompraDetalleSerialRollo_B1") && vResult;
           
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCompraDetalleSerialRolloED

} //End of namespace Galac.Adm.Dal.GestionCompras

