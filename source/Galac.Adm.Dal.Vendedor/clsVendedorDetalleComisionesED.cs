using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.Vendedor;
using LibGalac.Aos.Base;

namespace Galac.Adm.Dal.Vendedor {
    [LibMefDalComponentMetadata(typeof(clsVendedorDetalleComisionesED))]
    public class clsVendedorDetalleComisionesED : LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsVendedorDetalleComisionesED(): base(){
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
            get { return "VendedorDetalleComisiones"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine(InsSql.CreateTable("VendedorDetalleComisiones", DbSchema) + " ( ");
            vSQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnVenDetComConsecutiv NOT NULL, ");
            vSQL.AppendLine("ConsecutivoVendedor" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnVenDetComConsecutiv NOT NULL, ");
            vSQL.AppendLine("ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnVenDetComConsecutiv NOT NULL, ");
            vSQL.AppendLine("NombreDeLineaDeProducto" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT nnVenDetComNombreDeLi NOT NULL, ");
            vSQL.AppendLine("TipoDeComision" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnVenDetComTipoDeComi NOT NULL, ");
            vSQL.AppendLine("Monto" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnVenDetComMonto NOT NULL, ");
            vSQL.AppendLine("Porcentaje" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnVenDetComPorcentaje NOT NULL, ");
            vSQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            vSQL.AppendLine("CONSTRAINT p_VendedorDetalleComisiones PRIMARY KEY CLUSTERED");
            vSQL.AppendLine("(ConsecutivoCompania ASC, ConsecutivoVendedor ASC, ConsecutivoRenglon ASC)");
            vSQL.AppendLine(",CONSTRAINT fk_VendedorDetalleComisionesVendedor FOREIGN KEY (ConsecutivoCompania, ConsecutivoVendedor)");
            vSQL.AppendLine("REFERENCES Adm.Vendedor(ConsecutivoCompania, Consecutivo)");
            vSQL.AppendLine("ON DELETE NO ACTION");
            vSQL.AppendLine("ON UPDATE NO ACTION");
            vSQL.AppendLine(")");
            return vSQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT ConsecutivoCompania, ConsecutivoVendedor, ConsecutivoRenglon, NombreDeLineaDeProducto");
            SQL.AppendLine(", TipoDeComision, " + DbSchema + ".Gv_EnumTipoComision.StrValue AS TipoDeComisionStr, Monto, Porcentaje");
            SQL.AppendLine(", VendedorDetalleComisiones.fldTimeStamp, CAST(VendedorDetalleComisiones.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".VendedorDetalleComisiones");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoComision");
            SQL.AppendLine("ON " + DbSchema + ".VendedorDetalleComisiones.TipoDeComision COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoComision.DbValue");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoVendedor" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NombreDeLineaDeProducto" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@TipoDeComision" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Monto" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@Porcentaje" + InsSql.DecimalTypeForDb(25, 4) + " = 0");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".VendedorDetalleComisiones(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            ConsecutivoVendedor,");
            SQL.AppendLine("            ConsecutivoRenglon,");
            SQL.AppendLine("            NombreDeLineaDeProducto,");
            SQL.AppendLine("            TipoDeComision,");
            SQL.AppendLine("            Monto,");
            SQL.AppendLine("            Porcentaje)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @ConsecutivoVendedor,");
            SQL.AppendLine("            @ConsecutivoRenglon,");
            SQL.AppendLine("            @NombreDeLineaDeProducto,");
            SQL.AppendLine("            @TipoDeComision,");
            SQL.AppendLine("            @Monto,");
            SQL.AppendLine("            @Porcentaje)");
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
            SQL.AppendLine("@ConsecutivoVendedor" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NombreDeLineaDeProducto" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@TipoDeComision" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Monto" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@Porcentaje" + InsSql.DecimalTypeForDb(25, 4) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".VendedorDetalleComisiones WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoVendedor = @ConsecutivoVendedor AND ConsecutivoRenglon = @ConsecutivoRenglon)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".VendedorDetalleComisiones WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoVendedor = @ConsecutivoVendedor AND ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_VendedorDetalleComisionesCanBeUpdated @ConsecutivoCompania,@ConsecutivoVendedor,@ConsecutivoRenglon, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".VendedorDetalleComisiones");
            SQL.AppendLine("            SET NombreDeLineaDeProducto = @NombreDeLineaDeProducto,");
            SQL.AppendLine("               TipoDeComision = @TipoDeComision,");
            SQL.AppendLine("               Monto = @Monto,");
            SQL.AppendLine("               Porcentaje = @Porcentaje");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoVendedor = @ConsecutivoVendedor");
            SQL.AppendLine("               AND ConsecutivoRenglon = @ConsecutivoRenglon");
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
            SQL.AppendLine("@ConsecutivoVendedor" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".VendedorDetalleComisiones WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoVendedor = @ConsecutivoVendedor AND ConsecutivoRenglon = @ConsecutivoRenglon)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".VendedorDetalleComisiones WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoVendedor = @ConsecutivoVendedor AND ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_VendedorDetalleComisionesCanBeDeleted @ConsecutivoCompania,@ConsecutivoVendedor,@ConsecutivoRenglon, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".VendedorDetalleComisiones");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoVendedor = @ConsecutivoVendedor");
            SQL.AppendLine("               AND ConsecutivoRenglon = @ConsecutivoRenglon");
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
            SQL.AppendLine("@ConsecutivoVendedor" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         ConsecutivoVendedor,");
            SQL.AppendLine("         ConsecutivoRenglon,");
            SQL.AppendLine("         NombreDeLineaDeProducto,");
            SQL.AppendLine("         TipoDeComision,");
            SQL.AppendLine("         Monto,");
            SQL.AppendLine("         Porcentaje,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".VendedorDetalleComisiones");
            SQL.AppendLine("      WHERE VendedorDetalleComisiones.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND VendedorDetalleComisiones.ConsecutivoVendedor = @ConsecutivoVendedor");
            SQL.AppendLine("         AND VendedorDetalleComisiones.ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoVendedor" + InsSql.NumericTypeForDb(10, 0));
            //SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpSelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SELECT ");
            SQL.AppendLine("        ConsecutivoCompania,");
            SQL.AppendLine("        ConsecutivoVendedor,");
            SQL.AppendLine("        ConsecutivoRenglon,");
            SQL.AppendLine("        NombreDeLineaDeProducto,");
            SQL.AppendLine("        TipoDeComision,");
            SQL.AppendLine("        Monto,");
            SQL.AppendLine("        Porcentaje,");
            SQL.AppendLine("        fldTimeStamp");
            SQL.AppendLine("    FROM VendedorDetalleComisiones");
            SQL.AppendLine(" 	WHERE ConsecutivoVendedor = @ConsecutivoVendedor");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoVendedor" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpDelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	DELETE FROM VendedorDetalleComisiones");
            SQL.AppendLine(" 	WHERE ConsecutivoVendedor = @ConsecutivoVendedor");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInsDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoVendedor" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@XmlDataDetail" + InsSql.XmlTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpInsDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SET NOCOUNT ON;");
            SQL.AppendLine("	DECLARE @ReturnValue  " + InsSql.NumericTypeForDb(10, 0));
	        SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Dbo.Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
	        SQL.AppendLine("	    BEGIN");
            SQL.AppendLine("	    EXEC Adm.Gp_VendedorDetalleComisionesDelDet @ConsecutivoCompania = @ConsecutivoCompania, @ConsecutivoVendedor = @ConsecutivoVendedor");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO Adm.VendedorDetalleComisiones(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        ConsecutivoVendedor,");
			SQL.AppendLine("	        ConsecutivoRenglon,");
			SQL.AppendLine("	        NombreDeLineaDeProducto,");
			SQL.AppendLine("	        TipoDeComision,");
			SQL.AppendLine("	        Monto,");
			SQL.AppendLine("	        Porcentaje)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @ConsecutivoVendedor,");
			SQL.AppendLine("	        ConsecutivoRenglon,");
			SQL.AppendLine("	        NombreDeLineaDeProducto,");
			SQL.AppendLine("	        TipoDeComision,");
			SQL.AppendLine("	        Monto,");
			SQL.AppendLine("	        Porcentaje");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataVendedorDetalleComisiones/GpDetailVendedorDetalleComisiones',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        ConsecutivoRenglon " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        NombreDeLineaDeProducto " + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("	        TipoDeComision " + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("	        Monto " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        Porcentaje " + InsSql.DecimalTypeForDb(25, 4) + ") AS XmlDocDetailOfVendedor");
            SQL.AppendLine("	    EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("	    SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("	    RETURN @ReturnValue");
	        SQL.AppendLine("	END");
	        SQL.AppendLine("	ELSE");
            SQL.AppendLine("	    RETURN -1");
            SQL.AppendLine("END");
            string vPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\view.sql";
            LibFile.WriteLineInFile(vPath, SQL.ToString(), true);
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".VendedorDetalleComisiones", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoComision", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoComision), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_VendedorDetalleComisiones_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VendedorDetalleComisionesINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VendedorDetalleComisionesUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VendedorDetalleComisionesDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VendedorDetalleComisionesGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VendedorDetalleComisionesSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VendedorDetalleComisionesDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VendedorDetalleComisionesInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".VendedorDetalleComisiones", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_VendedorDetalleComisionesINS");
            vResult = insSp.Drop(DbSchema + ".Gp_VendedorDetalleComisionesUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_VendedorDetalleComisionesDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_VendedorDetalleComisionesGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_VendedorDetalleComisionesInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_VendedorDetalleComisionesDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_VendedorDetalleComisionesSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_VendedorDetalleComisiones_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoComision") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados

    } //End of class clsVendedorDetalleComisionesED

} //End of namespace Galac.Adm.Dal.Vendedor

