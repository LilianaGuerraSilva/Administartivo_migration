using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Dal.GestionCompras {
    [LibMefDalComponentMetadata(typeof(clsCompraDetalleGastoED))]
    public class clsCompraDetalleGastoED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsCompraDetalleGastoED(): base(){
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
            get { return "CompraDetalleGasto"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("CompraDetalleGasto", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnComDetGasConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnComDetGasConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoCxP" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnComDetGasConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnComDetGasConsecutiv NOT NULL, ");
            SQL.AppendLine("TipoDeCosto" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_ComDetGasTiDeCo DEFAULT ('0'), ");
            SQL.AppendLine("Monto" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_ComDetGasMo DEFAULT (0), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_CompraDetalleGasto PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, ConsecutivoCompra ASC, ConsecutivoRenglon ASC)");
            SQL.AppendLine(",CONSTRAINT fk_CompraDetalleGastoCompra FOREIGN KEY (ConsecutivoCompania, ConsecutivoCompra)");
            SQL.AppendLine("REFERENCES Adm.Compra(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(",CONSTRAINT u_ComDetGasniapraCxp UNIQUE NONCLUSTERED (ConsecutivoCompania,ConsecutivoCompra,ConsecutivoCxp)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT ConsecutivoCompania, ConsecutivoCompra, ConsecutivoCxP, ConsecutivoRenglon");
            SQL.AppendLine(", TipoDeCosto, " + DbSchema + ".Gv_EnumTipoDeCosto.StrValue AS TipoDeCostoStr, Monto");
            SQL.AppendLine(", CompraDetalleGasto.fldTimeStamp, CAST(CompraDetalleGasto.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".CompraDetalleGasto");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoDeCosto");
            SQL.AppendLine("ON " + DbSchema + ".CompraDetalleGasto.TipoDeCosto COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDeCosto.DbValue");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoCxP" + InsSql.NumericTypeForDb(10, 0) + " = 0,");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@TipoDeCosto" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Monto" + InsSql.DecimalTypeForDb(25, 4) + " = 0");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".CompraDetalleGasto(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            ConsecutivoCompra,");
            SQL.AppendLine("            ConsecutivoCxP,");
            SQL.AppendLine("            ConsecutivoRenglon,");
            SQL.AppendLine("            TipoDeCosto,");
            SQL.AppendLine("            Monto)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @ConsecutivoCompra,");
            SQL.AppendLine("            @ConsecutivoCxP,");
            SQL.AppendLine("            @ConsecutivoRenglon,");
            SQL.AppendLine("            @TipoDeCosto,");
            SQL.AppendLine("            @Monto)");
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
            SQL.AppendLine("@ConsecutivoCxP" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@TipoDeCosto" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Monto" + InsSql.DecimalTypeForDb(25, 4) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CompraDetalleGasto WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCompra = @ConsecutivoCompra AND ConsecutivoRenglon = @ConsecutivoRenglon)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CompraDetalleGasto WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCompra = @ConsecutivoCompra AND ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_CompraDetalleGastoCanBeUpdated @ConsecutivoCompania,@ConsecutivoCompra,@ConsecutivoRenglon, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".CompraDetalleGasto");
            SQL.AppendLine("            SET ConsecutivoCxP = @ConsecutivoCxP,");
            SQL.AppendLine("               TipoDeCosto = @TipoDeCosto,");
            SQL.AppendLine("               Monto = @Monto");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoCompra = @ConsecutivoCompra");
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
            SQL.AppendLine("@ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".CompraDetalleGasto WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCompra = @ConsecutivoCompra AND ConsecutivoRenglon = @ConsecutivoRenglon)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".CompraDetalleGasto WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoCompra = @ConsecutivoCompra AND ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_CompraDetalleGastoCanBeDeleted @ConsecutivoCompania,@ConsecutivoCompra,@ConsecutivoRenglon, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".CompraDetalleGasto");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoCompra = @ConsecutivoCompra");
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
            SQL.AppendLine("@ConsecutivoCompra" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         ConsecutivoCompra,");
            SQL.AppendLine("         ConsecutivoCxP,");
            SQL.AppendLine("         ConsecutivoRenglon,");
            SQL.AppendLine("         TipoDeCosto,");
            SQL.AppendLine("         Monto,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".CompraDetalleGasto");
            SQL.AppendLine("      WHERE CompraDetalleGasto.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND CompraDetalleGasto.ConsecutivoCompra = @ConsecutivoCompra");
            SQL.AppendLine("         AND CompraDetalleGasto.ConsecutivoRenglon = @ConsecutivoRenglon");
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
            SQL.AppendLine("        Adm.CompraDetalleGasto.ConsecutivoCompania,");
            SQL.AppendLine("        Adm.CompraDetalleGasto.ConsecutivoCompra,");
            SQL.AppendLine("        Adm.CompraDetalleGasto.ConsecutivoCxP,");
            SQL.AppendLine("        Adm.CompraDetalleGasto.ConsecutivoRenglon,");
            SQL.AppendLine("        Adm.CompraDetalleGasto.TipoDeCosto,");
            SQL.AppendLine("        Adm.CompraDetalleGasto.Monto,");
            SQL.AppendLine("        dbo.CxP.CodigoProveedor,");
            SQL.AppendLine("        dbo.CxP.Numero AS CxpNumero,");
            SQL.AppendLine("        Adm.Proveedor.NombreProveedor,");
            SQL.AppendLine("        Adm.CompraDetalleGasto.fldTimeStamp");
            SQL.AppendLine("    FROM CompraDetalleGasto");
            SQL.AppendLine("    INNER JOIN dbo.CxP ON Adm.CompraDetalleGasto.ConsecutivoCompania = dbo.CxP.ConsecutivoCompania AND Adm.CompraDetalleGasto.ConsecutivoCxP = dbo.CxP.ConsecutivoCxP  ");
            SQL.AppendLine("    INNER JOIN Adm.Proveedor ON Adm.Proveedor.ConsecutivoCompania = dbo.CxP.ConsecutivoCompania AND Adm.Proveedor.CodigoProveedor = dbo.CxP.CodigoProveedor  ");
            SQL.AppendLine(" 	WHERE ConsecutivoCompra = @ConsecutivoCompra");
            SQL.AppendLine(" 	AND Adm.CompraDetalleGasto.ConsecutivoCompania = @ConsecutivoCompania");
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
            SQL.AppendLine("	DELETE FROM CompraDetalleGasto");
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
            SQL.AppendLine("	    EXEC Adm.Gp_CompraDetalleGastoDelDet @ConsecutivoCompania = @ConsecutivoCompania, @ConsecutivoCompra = @ConsecutivoCompra");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO Adm.CompraDetalleGasto(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        ConsecutivoCompra,");
			SQL.AppendLine("	        ConsecutivoCxP,");
			SQL.AppendLine("	        ConsecutivoRenglon,");
			SQL.AppendLine("	        TipoDeCosto,");
			SQL.AppendLine("	        Monto)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @ConsecutivoCompra,");
			SQL.AppendLine("	        ConsecutivoCxP,");
			SQL.AppendLine("	        ConsecutivoRenglon,");
			SQL.AppendLine("	        TipoDeCosto,");
			SQL.AppendLine("	        Monto");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataCompraDetalleGasto/GpDetailCompraDetalleGasto',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        ConsecutivoCxP " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        ConsecutivoRenglon " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        TipoDeCosto " + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("	        Monto " + InsSql.DecimalTypeForDb(25, 4) + ") AS XmlDocDetailOfCompra");
            SQL.AppendLine("	    EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("	    SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("	    RETURN @ReturnValue");
	        SQL.AppendLine("	END");
	        SQL.AppendLine("	ELSE");
            SQL.AppendLine("	    RETURN -1");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".CompraDetalleGasto", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDeCosto", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDeCosto), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_CompraDetalleGasto_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleGastoINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleGastoUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleGastoDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleGastoGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleGastoSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleGastoDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CompraDetalleGastoInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".CompraDetalleGasto", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleGastoINS");
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleGastoUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleGastoDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleGastoGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleGastoInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleGastoDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CompraDetalleGastoSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_CompraDetalleGasto_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDeCosto") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCompraDetalleGastoED

} //End of namespace Galac.Adm.Dal.GestionCompras

