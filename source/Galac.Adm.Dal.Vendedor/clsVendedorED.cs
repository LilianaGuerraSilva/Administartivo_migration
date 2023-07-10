using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.Vendedor;

namespace Galac.Adm.Dal.Vendedor {
    [LibMefDalComponentMetadata(typeof(clsVendedorED))]
    public class clsVendedorED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsVendedorED(): base(){
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
            get { return "Vendedor"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("Vendedor", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnVenConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnVenConsecut NOT NULL, ");
            SQL.AppendLine("Codigo" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT nnVenCodigo NOT NULL, ");
            SQL.AppendLine("Nombre" + InsSql.VarCharTypeForDb(35) + " CONSTRAINT d_VenNo DEFAULT (''), ");
            SQL.AppendLine("RIF" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_VenRIF DEFAULT (''), ");
            SQL.AppendLine("StatusVendedor" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_VenStVe DEFAULT ('0'), ");
            SQL.AppendLine("Direccion" + InsSql.VarCharTypeForDb(255) + " CONSTRAINT d_VenDi DEFAULT (''), ");
            SQL.AppendLine("Ciudad" + InsSql.VarCharTypeForDb(100) + " CONSTRAINT d_VenCi DEFAULT (''), ");
            SQL.AppendLine("ZonaPostal" + InsSql.VarCharTypeForDb(7) + " CONSTRAINT d_VenZoPo DEFAULT (''), ");
            SQL.AppendLine("Telefono" + InsSql.VarCharTypeForDb(40) + " CONSTRAINT d_VenTe DEFAULT (''), ");
            SQL.AppendLine("Fax" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_VenFa DEFAULT (''), ");
            SQL.AppendLine("Email" + InsSql.VarCharTypeForDb(40) + " CONSTRAINT d_VenEm DEFAULT (''), ");
            SQL.AppendLine("Notas" + InsSql.VarCharTypeForDb(255) + " CONSTRAINT d_VenNo DEFAULT (''), ");
            SQL.AppendLine("ConsecutivoRutaDeComercializacion" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT d_VenCoRuDeCo DEFAULT (0), ");
            SQL.AppendLine("ComisionPorVenta" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenCoPoVe DEFAULT (0), ");
            SQL.AppendLine("ComisionPorCobro" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenCoPoCo DEFAULT (0), ");
            SQL.AppendLine("TopeInicialVenta1" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenToInVe1 DEFAULT (0), ");
            SQL.AppendLine("TopeFinalVenta1" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenToFiVe1 DEFAULT (0), ");
            SQL.AppendLine("PorcentajeVentas1" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenPoVe1 DEFAULT (0), ");
            SQL.AppendLine("TopeFinalVenta2" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenToFiVe2 DEFAULT (0), ");
            SQL.AppendLine("PorcentajeVentas2" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenPoVe2 DEFAULT (0), ");
            SQL.AppendLine("TopeFinalVenta3" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenToFiVe3 DEFAULT (0), ");
            SQL.AppendLine("PorcentajeVentas3" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenPoVe3 DEFAULT (0), ");
            SQL.AppendLine("TopeFinalVenta4" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenToFiVe4 DEFAULT (0), ");
            SQL.AppendLine("PorcentajeVentas4" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenPoVe4 DEFAULT (0), ");
            SQL.AppendLine("TopeFinalVenta5" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenToFiVe5 DEFAULT (0), ");
            SQL.AppendLine("PorcentajeVentas5" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenPoVe5 DEFAULT (0), ");
            SQL.AppendLine("TopeInicialCobranza1" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenToInCo1 DEFAULT (0), ");
            SQL.AppendLine("TopeFinalCobranza1" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenToFiCo1 DEFAULT (0), ");
            SQL.AppendLine("PorcentajeCobranza1" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenPoCo1 DEFAULT (0), ");
            SQL.AppendLine("TopeFinalCobranza2" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenToFiCo2 DEFAULT (0), ");
            SQL.AppendLine("PorcentajeCobranza2" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenPoCo2 DEFAULT (0), ");
            SQL.AppendLine("TopeFinalCobranza3" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenToFiCo3 DEFAULT (0), ");
            SQL.AppendLine("PorcentajeCobranza3" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenPoCo3 DEFAULT (0), ");
            SQL.AppendLine("TopeFinalCobranza4" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenToFiCo4 DEFAULT (0), ");
            SQL.AppendLine("PorcentajeCobranza4" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenPoCo4 DEFAULT (0), ");
            SQL.AppendLine("TopeFinalCobranza5" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenToFiCo5 DEFAULT (0), ");
            SQL.AppendLine("PorcentajeCobranza5" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_VenPoCo5 DEFAULT (0), ");
            SQL.AppendLine("UsaComisionPorVenta" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnVenUsaComisio NOT NULL, ");
            SQL.AppendLine("UsaComisionPorCobranza" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnVenUsaComisio NOT NULL, ");
            SQL.AppendLine("CodigoLote" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT d_VenCoLo DEFAULT (''), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_Vendedor PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Consecutivo ASC)");
            SQL.AppendLine(", CONSTRAINT fk_VendedorCiudad FOREIGN KEY (Ciudad)");
            SQL.AppendLine("REFERENCES Comun.Ciudad(NombreCiudad)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_VendedorRutaDeComercializacion FOREIGN KEY (ConsecutivoCompania, ConsecutivoRutaDeComercializacion)");
            SQL.AppendLine("REFERENCES Saw.RutaDeComercializacion(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(",CONSTRAINT u_Venniabre UNIQUE NONCLUSTERED (ConsecutivoCompania,Nombre)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT Vendedor.ConsecutivoCompania, Vendedor.Consecutivo, Vendedor.Codigo, Vendedor.Nombre");
            SQL.AppendLine(", Vendedor.RIF, Vendedor.StatusVendedor, " + DbSchema + ".Gv_EnumStatusVendedor.StrValue AS StatusVendedorStr, Vendedor.Direccion, Vendedor.Ciudad");
            SQL.AppendLine(", Vendedor.ZonaPostal, Vendedor.Telefono, Vendedor.Fax, Vendedor.Email");
            SQL.AppendLine(", Vendedor.Notas, RutaDeComercializacion.Descripcion AS RutaDeComercializacion, Vendedor.ComisionPorVenta, Vendedor.ComisionPorCobro");
            SQL.AppendLine(", Vendedor.TopeInicialVenta1, Vendedor.TopeFinalVenta1, Vendedor.PorcentajeVentas1, Vendedor.TopeFinalVenta2");
            SQL.AppendLine(", Vendedor.PorcentajeVentas2, Vendedor.TopeFinalVenta3, Vendedor.PorcentajeVentas3, Vendedor.TopeFinalVenta4");
            SQL.AppendLine(", Vendedor.PorcentajeVentas4, Vendedor.TopeFinalVenta5, Vendedor.PorcentajeVentas5, Vendedor.TopeInicialCobranza1");
            SQL.AppendLine(", Vendedor.TopeFinalCobranza1, Vendedor.PorcentajeCobranza1, Vendedor.TopeFinalCobranza2, Vendedor.PorcentajeCobranza2");
            SQL.AppendLine(", Vendedor.TopeFinalCobranza3, Vendedor.PorcentajeCobranza3, Vendedor.TopeFinalCobranza4, Vendedor.PorcentajeCobranza4");
            SQL.AppendLine(", Vendedor.TopeFinalCobranza5, Vendedor.PorcentajeCobranza5, Vendedor.UsaComisionPorVenta, Vendedor.UsaComisionPorCobranza");
            SQL.AppendLine(", Vendedor.CodigoLote, Vendedor.NombreOperador, Vendedor.FechaUltimaModificacion");
            SQL.AppendLine(", Vendedor.fldTimeStamp, CAST(Vendedor.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".Vendedor");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumStatusVendedor");
            SQL.AppendLine("ON " + DbSchema + ".Vendedor.StatusVendedor COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumStatusVendedor.DbValue");
            SQL.AppendLine("INNER JOIN Comun.Ciudad ON  " + DbSchema + ".Vendedor.Ciudad = Comun.Ciudad.NombreCiudad");
            SQL.AppendLine("INNER JOIN Saw.RutaDeComercializacion ON  " + DbSchema + ".Vendedor.ConsecutivoRutaDeComercializacion = Saw.RutaDeComercializacion.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Vendedor.ConsecutivoCompania = Saw.RutaDeComercializacion.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(35) + " = '',");
            SQL.AppendLine("@RIF" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@StatusVendedor" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Direccion" + InsSql.VarCharTypeForDb(255) + " = '',");
            SQL.AppendLine("@Ciudad" + InsSql.VarCharTypeForDb(100) + ",");
            SQL.AppendLine("@ZonaPostal" + InsSql.VarCharTypeForDb(7) + " = '',");
            SQL.AppendLine("@Telefono" + InsSql.VarCharTypeForDb(40) + " = '',");
            SQL.AppendLine("@Fax" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@Email" + InsSql.VarCharTypeForDb(40) + " = '',");
            SQL.AppendLine("@Notas" + InsSql.VarCharTypeForDb(255) + " = '',");
            SQL.AppendLine("@ConsecutivoRutaDeComercializacion" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ComisionPorVenta" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@ComisionPorCobro" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TopeInicialVenta1" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TopeFinalVenta1" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PorcentajeVentas1" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TopeFinalVenta2" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PorcentajeVentas2" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TopeFinalVenta3" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PorcentajeVentas3" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TopeFinalVenta4" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PorcentajeVentas4" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TopeFinalVenta5" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PorcentajeVentas5" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TopeInicialCobranza1" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TopeFinalCobranza1" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PorcentajeCobranza1" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TopeFinalCobranza2" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PorcentajeCobranza2" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TopeFinalCobranza3" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PorcentajeCobranza3" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TopeFinalCobranza4" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PorcentajeCobranza4" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@TopeFinalCobranza5" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PorcentajeCobranza5" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@UsaComisionPorVenta" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@UsaComisionPorCobranza" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@CodigoLote" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + " = '01/01/1900'");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".Vendedor(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            Codigo,");
            SQL.AppendLine("            Nombre,");
            SQL.AppendLine("            RIF,");
            SQL.AppendLine("            StatusVendedor,");
            SQL.AppendLine("            Direccion,");
            SQL.AppendLine("            Ciudad,");
            SQL.AppendLine("            ZonaPostal,");
            SQL.AppendLine("            Telefono,");
            SQL.AppendLine("            Fax,");
            SQL.AppendLine("            Email,");
            SQL.AppendLine("            Notas,");
            SQL.AppendLine("            ConsecutivoRutaDeComercializacion,");
            SQL.AppendLine("            ComisionPorVenta,");
            SQL.AppendLine("            ComisionPorCobro,");
            SQL.AppendLine("            TopeInicialVenta1,");
            SQL.AppendLine("            TopeFinalVenta1,");
            SQL.AppendLine("            PorcentajeVentas1,");
            SQL.AppendLine("            TopeFinalVenta2,");
            SQL.AppendLine("            PorcentajeVentas2,");
            SQL.AppendLine("            TopeFinalVenta3,");
            SQL.AppendLine("            PorcentajeVentas3,");
            SQL.AppendLine("            TopeFinalVenta4,");
            SQL.AppendLine("            PorcentajeVentas4,");
            SQL.AppendLine("            TopeFinalVenta5,");
            SQL.AppendLine("            PorcentajeVentas5,");
            SQL.AppendLine("            TopeInicialCobranza1,");
            SQL.AppendLine("            TopeFinalCobranza1,");
            SQL.AppendLine("            PorcentajeCobranza1,");
            SQL.AppendLine("            TopeFinalCobranza2,");
            SQL.AppendLine("            PorcentajeCobranza2,");
            SQL.AppendLine("            TopeFinalCobranza3,");
            SQL.AppendLine("            PorcentajeCobranza3,");
            SQL.AppendLine("            TopeFinalCobranza4,");
            SQL.AppendLine("            PorcentajeCobranza4,");
            SQL.AppendLine("            TopeFinalCobranza5,");
            SQL.AppendLine("            PorcentajeCobranza5,");
            SQL.AppendLine("            UsaComisionPorVenta,");
            SQL.AppendLine("            UsaComisionPorCobranza,");
            SQL.AppendLine("            CodigoLote,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @Codigo,");
            SQL.AppendLine("            @Nombre,");
            SQL.AppendLine("            @RIF,");
            SQL.AppendLine("            @StatusVendedor,");
            SQL.AppendLine("            @Direccion,");
            SQL.AppendLine("            @Ciudad,");
            SQL.AppendLine("            @ZonaPostal,");
            SQL.AppendLine("            @Telefono,");
            SQL.AppendLine("            @Fax,");
            SQL.AppendLine("            @Email,");
            SQL.AppendLine("            @Notas,");
            SQL.AppendLine("            @ConsecutivoRutaDeComercializacion,");
            SQL.AppendLine("            @ComisionPorVenta,");
            SQL.AppendLine("            @ComisionPorCobro,");
            SQL.AppendLine("            @TopeInicialVenta1,");
            SQL.AppendLine("            @TopeFinalVenta1,");
            SQL.AppendLine("            @PorcentajeVentas1,");
            SQL.AppendLine("            @TopeFinalVenta2,");
            SQL.AppendLine("            @PorcentajeVentas2,");
            SQL.AppendLine("            @TopeFinalVenta3,");
            SQL.AppendLine("            @PorcentajeVentas3,");
            SQL.AppendLine("            @TopeFinalVenta4,");
            SQL.AppendLine("            @PorcentajeVentas4,");
            SQL.AppendLine("            @TopeFinalVenta5,");
            SQL.AppendLine("            @PorcentajeVentas5,");
            SQL.AppendLine("            @TopeInicialCobranza1,");
            SQL.AppendLine("            @TopeFinalCobranza1,");
            SQL.AppendLine("            @PorcentajeCobranza1,");
            SQL.AppendLine("            @TopeFinalCobranza2,");
            SQL.AppendLine("            @PorcentajeCobranza2,");
            SQL.AppendLine("            @TopeFinalCobranza3,");
            SQL.AppendLine("            @PorcentajeCobranza3,");
            SQL.AppendLine("            @TopeFinalCobranza4,");
            SQL.AppendLine("            @PorcentajeCobranza4,");
            SQL.AppendLine("            @TopeFinalCobranza5,");
            SQL.AppendLine("            @PorcentajeCobranza5,");
            SQL.AppendLine("            @UsaComisionPorVenta,");
            SQL.AppendLine("            @UsaComisionPorCobranza,");
            SQL.AppendLine("            @CodigoLote,");
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
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(35) + ",");
            SQL.AppendLine("@RIF" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@StatusVendedor" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Direccion" + InsSql.VarCharTypeForDb(255) + ",");
            SQL.AppendLine("@Ciudad" + InsSql.VarCharTypeForDb(100) + ",");
            SQL.AppendLine("@ZonaPostal" + InsSql.VarCharTypeForDb(7) + ",");
            SQL.AppendLine("@Telefono" + InsSql.VarCharTypeForDb(40) + ",");
            SQL.AppendLine("@Fax" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@Email" + InsSql.VarCharTypeForDb(40) + ",");
            SQL.AppendLine("@Notas" + InsSql.VarCharTypeForDb(255) + ",");
            SQL.AppendLine("@ConsecutivoRutaDeComercializacion" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ComisionPorVenta" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@ComisionPorCobro" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TopeInicialVenta1" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TopeFinalVenta1" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PorcentajeVentas1" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TopeFinalVenta2" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PorcentajeVentas2" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TopeFinalVenta3" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PorcentajeVentas3" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TopeFinalVenta4" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PorcentajeVentas4" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TopeFinalVenta5" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PorcentajeVentas5" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TopeInicialCobranza1" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TopeFinalCobranza1" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PorcentajeCobranza1" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TopeFinalCobranza2" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PorcentajeCobranza2" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TopeFinalCobranza3" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PorcentajeCobranza3" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TopeFinalCobranza4" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PorcentajeCobranza4" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@TopeFinalCobranza5" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PorcentajeCobranza5" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@UsaComisionPorVenta" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@UsaComisionPorCobranza" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@CodigoLote" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Vendedor WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Vendedor WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_VendedorCanBeUpdated @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".Vendedor");
            SQL.AppendLine("            SET Codigo = @Codigo,");
            SQL.AppendLine("               Nombre = @Nombre,");
            SQL.AppendLine("               RIF = @RIF,");
            SQL.AppendLine("               StatusVendedor = @StatusVendedor,");
            SQL.AppendLine("               Direccion = @Direccion,");
            SQL.AppendLine("               Ciudad = @Ciudad,");
            SQL.AppendLine("               ZonaPostal = @ZonaPostal,");
            SQL.AppendLine("               Telefono = @Telefono,");
            SQL.AppendLine("               Fax = @Fax,");
            SQL.AppendLine("               Email = @Email,");
            SQL.AppendLine("               Notas = @Notas,");
            SQL.AppendLine("               ConsecutivoRutaDeComercializacion = @ConsecutivoRutaDeComercializacion,");
            SQL.AppendLine("               ComisionPorVenta = @ComisionPorVenta,");
            SQL.AppendLine("               ComisionPorCobro = @ComisionPorCobro,");
            SQL.AppendLine("               TopeInicialVenta1 = @TopeInicialVenta1,");
            SQL.AppendLine("               TopeFinalVenta1 = @TopeFinalVenta1,");
            SQL.AppendLine("               PorcentajeVentas1 = @PorcentajeVentas1,");
            SQL.AppendLine("               TopeFinalVenta2 = @TopeFinalVenta2,");
            SQL.AppendLine("               PorcentajeVentas2 = @PorcentajeVentas2,");
            SQL.AppendLine("               TopeFinalVenta3 = @TopeFinalVenta3,");
            SQL.AppendLine("               PorcentajeVentas3 = @PorcentajeVentas3,");
            SQL.AppendLine("               TopeFinalVenta4 = @TopeFinalVenta4,");
            SQL.AppendLine("               PorcentajeVentas4 = @PorcentajeVentas4,");
            SQL.AppendLine("               TopeFinalVenta5 = @TopeFinalVenta5,");
            SQL.AppendLine("               PorcentajeVentas5 = @PorcentajeVentas5,");
            SQL.AppendLine("               TopeInicialCobranza1 = @TopeInicialCobranza1,");
            SQL.AppendLine("               TopeFinalCobranza1 = @TopeFinalCobranza1,");
            SQL.AppendLine("               PorcentajeCobranza1 = @PorcentajeCobranza1,");
            SQL.AppendLine("               TopeFinalCobranza2 = @TopeFinalCobranza2,");
            SQL.AppendLine("               PorcentajeCobranza2 = @PorcentajeCobranza2,");
            SQL.AppendLine("               TopeFinalCobranza3 = @TopeFinalCobranza3,");
            SQL.AppendLine("               PorcentajeCobranza3 = @PorcentajeCobranza3,");
            SQL.AppendLine("               TopeFinalCobranza4 = @TopeFinalCobranza4,");
            SQL.AppendLine("               PorcentajeCobranza4 = @PorcentajeCobranza4,");
            SQL.AppendLine("               TopeFinalCobranza5 = @TopeFinalCobranza5,");
            SQL.AppendLine("               PorcentajeCobranza5 = @PorcentajeCobranza5,");
            SQL.AppendLine("               UsaComisionPorVenta = @UsaComisionPorVenta,");
            SQL.AppendLine("               UsaComisionPorCobranza = @UsaComisionPorCobranza,");
            SQL.AppendLine("               CodigoLote = @CodigoLote,");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Vendedor WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Vendedor WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_VendedorCanBeDeleted @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".Vendedor");
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
            SQL.AppendLine("         Vendedor.ConsecutivoCompania,");
            SQL.AppendLine("         Vendedor.Consecutivo,");
            SQL.AppendLine("         Vendedor.Codigo,");
            SQL.AppendLine("         Vendedor.Nombre,");
            SQL.AppendLine("         Vendedor.RIF,");
            SQL.AppendLine("         Vendedor.StatusVendedor,");
            SQL.AppendLine("         Vendedor.Direccion,");
            SQL.AppendLine("         Vendedor.Ciudad,");
            SQL.AppendLine("         Vendedor.ZonaPostal,");
            SQL.AppendLine("         Vendedor.Telefono,");
            SQL.AppendLine("         Vendedor.Fax,");
            SQL.AppendLine("         Vendedor.Email,");
            SQL.AppendLine("         Vendedor.Notas,");
            SQL.AppendLine("         Vendedor.ConsecutivoRutaDeComercializacion,");
            SQL.AppendLine("         Gv_RutaDeComercializacion_B1.Descripcion AS RutaDeComercializacion,");
            SQL.AppendLine("         Vendedor.ComisionPorVenta,");
            SQL.AppendLine("         Vendedor.ComisionPorCobro,");
            SQL.AppendLine("         Vendedor.TopeInicialVenta1,");
            SQL.AppendLine("         Vendedor.TopeFinalVenta1,");
            SQL.AppendLine("         Vendedor.PorcentajeVentas1,");
            SQL.AppendLine("         Vendedor.TopeFinalVenta2,");
            SQL.AppendLine("         Vendedor.PorcentajeVentas2,");
            SQL.AppendLine("         Vendedor.TopeFinalVenta3,");
            SQL.AppendLine("         Vendedor.PorcentajeVentas3,");
            SQL.AppendLine("         Vendedor.TopeFinalVenta4,");
            SQL.AppendLine("         Vendedor.PorcentajeVentas4,");
            SQL.AppendLine("         Vendedor.TopeFinalVenta5,");
            SQL.AppendLine("         Vendedor.PorcentajeVentas5,");
            SQL.AppendLine("         Vendedor.TopeInicialCobranza1,");
            SQL.AppendLine("         Vendedor.TopeFinalCobranza1,");
            SQL.AppendLine("         Vendedor.PorcentajeCobranza1,");
            SQL.AppendLine("         Vendedor.TopeFinalCobranza2,");
            SQL.AppendLine("         Vendedor.PorcentajeCobranza2,");
            SQL.AppendLine("         Vendedor.TopeFinalCobranza3,");
            SQL.AppendLine("         Vendedor.PorcentajeCobranza3,");
            SQL.AppendLine("         Vendedor.TopeFinalCobranza4,");
            SQL.AppendLine("         Vendedor.PorcentajeCobranza4,");
            SQL.AppendLine("         Vendedor.TopeFinalCobranza5,");
            SQL.AppendLine("         Vendedor.PorcentajeCobranza5,");
            SQL.AppendLine("         Vendedor.UsaComisionPorVenta,");
            SQL.AppendLine("         Vendedor.UsaComisionPorCobranza,");
            SQL.AppendLine("         Vendedor.CodigoLote,");
            SQL.AppendLine("         Vendedor.NombreOperador,");
            SQL.AppendLine("         Vendedor.FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(Vendedor.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         Vendedor.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".Vendedor");
            SQL.AppendLine("             INNER JOIN Comun.Gv_Ciudad_B1 ON " + DbSchema + ".Vendedor.Ciudad = Comun.Gv_Ciudad_B1.NombreCiudad");
            SQL.AppendLine("             INNER JOIN Saw.Gv_RutaDeComercializacion_B1 ON " + DbSchema + ".Vendedor.ConsecutivoRutaDeComercializacion = Saw.Gv_RutaDeComercializacion_B1.Consecutivo");
            SQL.AppendLine("      WHERE Vendedor.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND Vendedor.Consecutivo = @Consecutivo");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_Vendedor_B1.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vendedor_B1.Codigo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vendedor_B1.Nombre,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vendedor_B1.RIF,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vendedor_B1.StatusVendedorStr,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vendedor_B1.RutaDeComercializacion,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vendedor_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vendedor_B1.StatusVendedor,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vendedor_B1.Ciudad,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vendedor_B1.ZonaPostal,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Vendedor_B1.Telefono");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Vendedor_B1");
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
            SQL.AppendLine("      " + DbSchema + ".Vendedor.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Vendedor.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Vendedor.Nombre,");
            SQL.AppendLine("      " + DbSchema + ".Vendedor.RIF,");
            SQL.AppendLine("      " + DbSchema + ".Vendedor.StatusVendedor,");
            SQL.AppendLine("      " + DbSchema + ".Vendedor.Ciudad,");
            SQL.AppendLine("      " + DbSchema + ".Vendedor.ZonaPostal,");
            SQL.AppendLine("      " + DbSchema + ".Vendedor.Telefono,");
            SQL.AppendLine("      " + DbSchema + ".Vendedor.ConsecutivoRutaDeComercializacion");
            SQL.AppendLine("      FROM " + DbSchema + ".Vendedor");
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
            bool vResult = insDbo.Create(DbSchema + ".Vendedor", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }
        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumStatusVendedor", LibTpvCreator.SqlViewStandardEnum(typeof(eStatusVendedor), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_Vendedor_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VendedorINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VendedorUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VendedorDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VendedorGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VendedorSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_VendedorGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
            insSps.Dispose();
            return vResult;
        }

        public bool InstalarTabla() {
            bool vResult = false;
            if (CrearTabla()) {
                CrearVistas();
                CrearProcedimientos();
                clsVendedorDetalleComisionesED insDetailVenDetalleComisiones = new clsVendedorDetalleComisionesED();
                vResult = insDetailVenDetalleComisiones.InstalarTabla();
            }
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = false;
            if (insDbo.Exists(DbSchema + ".Vendedor", eDboType.Tabla)) {
                CrearVistas();
                CrearProcedimientos();
                vResult = new clsVendedorDetalleComisionesED().InstalarVistasYSps();
            }
            return vResult;
        }

        public bool BorrarVistasYSps() {
            bool vResult = false;
            LibStoredProc insSp = new LibStoredProc();
            LibViews insVista = new LibViews();
            vResult = new clsVendedorDetalleComisionesED().BorrarVistasYSps();
            vResult = insSp.Drop(DbSchema + ".Gp_VendedorINS") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_VendedorUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_VendedorDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_VendedorGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_VendedorGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_VendedorSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_Vendedor_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumStatusVendedor") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados

    } //End of class clsVendedorED

} //End of namespace Galac.Adm.Dal.Vendedor

