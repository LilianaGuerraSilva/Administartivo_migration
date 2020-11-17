using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.CAnticipo;

namespace Galac.Adm.Dal.CAnticipo {
    [LibMefDalComponentMetadata(typeof(clsAnticipoED))]
    public class clsAnticipoED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsAnticipoED(): base(){
            DbSchema = "dbo";
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
            get { return "Anticipo"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("Anticipo", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnAntConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoAnticipo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnAntConsecutiv NOT NULL, ");
            SQL.AppendLine("Status" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_AntSt DEFAULT ('0'), ");
            SQL.AppendLine("Tipo" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_AntTi DEFAULT ('0'), ");
            SQL.AppendLine("Fecha" + InsSql.DateTypeForDb() + " CONSTRAINT d_AntFe DEFAULT (''), ");
            SQL.AppendLine("Numero" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT nnAntNumero NOT NULL, ");
            SQL.AppendLine("CodigoCliente" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT nnAntCodigoClie NOT NULL, ");
            SQL.AppendLine("CodigoProveedor" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT nnAntCodigoProv NOT NULL, ");
            SQL.AppendLine("Moneda" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT nnAntMoneda NOT NULL, ");
            SQL.AppendLine("Cambio" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_AntCa DEFAULT (0), ");
            SQL.AppendLine("GeneraMovBancario" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnAntGeneraMovB NOT NULL, ");
            SQL.AppendLine("CodigoCuentaBancaria" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT nnAntCodigoCuen NOT NULL, ");
            SQL.AppendLine("CodigoConceptoBancario" + InsSql.VarCharTypeForDb(8) + " CONSTRAINT nnAntCodigoConc NOT NULL, ");
            SQL.AppendLine("GeneraImpuestoBancario" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnAntGeneraImpu NOT NULL, ");
            SQL.AppendLine("FechaAnulacion" + InsSql.DateTypeForDb() + " CONSTRAINT d_AntFeAn DEFAULT (''), ");
            SQL.AppendLine("FechaCancelacion" + InsSql.DateTypeForDb() + " CONSTRAINT d_AntFeCa DEFAULT (''), ");
            SQL.AppendLine("FechaDevolucion" + InsSql.DateTypeForDb() + " CONSTRAINT d_AntFeDe DEFAULT (''), ");
            SQL.AppendLine("Descripcion" + InsSql.VarCharTypeForDb(255) + " CONSTRAINT d_AntDe DEFAULT (''), ");
            SQL.AppendLine("MontoTotal" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_AntMoTo DEFAULT (0), ");
            SQL.AppendLine("MontoUsado" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_AntMoUs DEFAULT (0), ");
            SQL.AppendLine("MontoDevuelto" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_AntMoDe DEFAULT (0), ");
            SQL.AppendLine("MontoDiferenciaEnDevolucion" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_AntMoDiEnDe DEFAULT (0), ");
            SQL.AppendLine("DiferenciaEsIDB" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnAntDiferencia NOT NULL, ");
            SQL.AppendLine("EsUnaDevolucion" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnAntEsUnaDevol NOT NULL, ");
            SQL.AppendLine("NumeroDelAnticipoDevuelto" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT d_AntNuDeAnDe DEFAULT (0), ");
            SQL.AppendLine("NumeroCheque" + InsSql.VarCharTypeForDb(15) + " CONSTRAINT nnAntNumeroCheq NOT NULL, ");
            SQL.AppendLine("AsociarAnticipoACotiz" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnAntAsociarAnt NOT NULL, ");
            SQL.AppendLine("NumeroCotizacion" + InsSql.VarCharTypeForDb(11) + " CONSTRAINT d_AntNuCo DEFAULT (''), ");
            SQL.AppendLine("ConsecutivoRendicion" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnAntConsecutiv NOT NULL, ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("CodigoMoneda" + InsSql.VarCharTypeForDb(4) + " CONSTRAINT d_AntCoMo DEFAULT (''), ");
            SQL.AppendLine("NombreBeneficiario" + InsSql.VarCharTypeForDb(60) + " CONSTRAINT d_AntNoBe DEFAULT (''), ");
            SQL.AppendLine("CodigoBeneficiario" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT d_AntCoBe DEFAULT (''), ");
            SQL.AppendLine("AsociarAnticipoACaja" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnAntAsociarAnt NOT NULL, ");
            SQL.AppendLine("ConsecutivoCaja" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnAntConsecutiv NOT NULL, ");
            SQL.AppendLine("NumeroDeCobranzaAsociado" + InsSql.VarCharTypeForDb(12) + " CONSTRAINT d_AntNuDeCoAs DEFAULT (''), ");
            SQL.AppendLine("GeneradoPor" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_AntGePo DEFAULT ('0'), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_Anticipo PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, ConsecutivoAnticipo ASC)");
            SQL.AppendLine(", CONSTRAINT fk_AnticipoCliente FOREIGN KEY (ConsecutivoCompania, CodigoCliente)");
            SQL.AppendLine("REFERENCES Cliente(ConsecutivoCompania, codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_AnticipoProveedor FOREIGN KEY (ConsecutivoCompania, CodigoProveedor)");
            SQL.AppendLine("REFERENCES Adm.Proveedor(ConsecutivoCompania, codigoProveedor)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_AnticipoMoneda FOREIGN KEY (Moneda)");
            SQL.AppendLine("REFERENCES dbo.Moneda(Nombre)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_AnticipoCuentaBancaria FOREIGN KEY (ConsecutivoCompania, CodigoCuentaBancaria)");
            SQL.AppendLine("REFERENCES Saw.CuentaBancaria(ConsecutivoCompania, Codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_AnticipoConceptoBancario FOREIGN KEY (CodigoConceptoBancario)");
            SQL.AppendLine("REFERENCES Adm.ConceptoBancario(codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_AnticipoCotizacion FOREIGN KEY (ConsecutivoCompania, NumeroCotizacion)");
            SQL.AppendLine("REFERENCES dbo.Cotizacion(ConsecutivoCompania, Numero)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_AnticipoRendicion FOREIGN KEY (ConsecutivoCompania, ConsecutivoRendicion)");
            SQL.AppendLine("REFERENCES Adm.Rendicion(ConsecutivoCompania, consecutivo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_AnticipoMoneda FOREIGN KEY (CodigoMoneda)");
            SQL.AppendLine("REFERENCES dbo.Moneda(Codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            //SQL.AppendLine(", CONSTRAINT fk_AnticipoCaja FOREIGN KEY (ConsecutivoCompania, ConsecutivoCaja)");
            //SQL.AppendLine("REFERENCES Adm.Caja(ConsecutivoCompania, consecutivo)");
            //SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(",CONSTRAINT u_AntConsecutivoAnticipo UNIQUE NONCLUSTERED (ConsecutivoCompania, ConsecutivoAnticipo)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT Anticipo.ConsecutivoCompania, Anticipo.ConsecutivoAnticipo, Anticipo.Status, " + DbSchema + ".Gv_EnumStatusAnticipo.StrValue AS StatusStr, Anticipo.Tipo, " + DbSchema + ".Gv_EnumTipoDeAnticipo.StrValue AS TipoStr");
            SQL.AppendLine(", Anticipo.Fecha, Anticipo.Numero, Anticipo.CodigoCliente, Anticipo.CodigoProveedor");
            SQL.AppendLine(", Anticipo.Moneda, Anticipo.Cambio, Anticipo.GeneraMovBancario, Anticipo.CodigoCuentaBancaria");
            SQL.AppendLine(", Anticipo.CodigoConceptoBancario, Anticipo.GeneraImpuestoBancario, Anticipo.FechaAnulacion, Anticipo.FechaCancelacion");
            SQL.AppendLine(", Anticipo.FechaDevolucion, Anticipo.Descripcion, Anticipo.MontoTotal, Anticipo.MontoUsado");
            SQL.AppendLine(", Anticipo.MontoDevuelto, Anticipo.MontoDiferenciaEnDevolucion, Anticipo.DiferenciaEsIDB, Anticipo.EsUnaDevolucion");
            SQL.AppendLine(", Anticipo.NumeroDelAnticipoDevuelto, Anticipo.NumeroCheque, Anticipo.AsociarAnticipoACotiz, Anticipo.NumeroCotizacion");
            SQL.AppendLine(", Anticipo.ConsecutivoRendicion, Anticipo.NombreOperador, Anticipo.FechaUltimaModificacion, Anticipo.CodigoMoneda");
            SQL.AppendLine(", Anticipo.NombreBeneficiario, Anticipo.CodigoBeneficiario, Anticipo.AsociarAnticipoACaja");
            SQL.AppendLine(", Anticipo.ConsecutivoCaja, Anticipo.NumeroDeCobranzaAsociado, Anticipo.GeneradoPor, " + DbSchema + ".Gv_EnumGeneradoPor.StrValue AS GeneradoPorStr");
            SQL.AppendLine(", Cliente.nombre AS NombreCliente");
            SQL.AppendLine(", Adm.Proveedor.nombreProveedor AS NombreProveedor");
            SQL.AppendLine(", Anticipo.fldTimeStamp, CAST(Anticipo.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".Anticipo");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumStatusAnticipo");
            SQL.AppendLine("ON " + DbSchema + ".Anticipo.Status COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumStatusAnticipo.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoDeAnticipo");
            SQL.AppendLine("ON " + DbSchema + ".Anticipo.Tipo COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDeAnticipo.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumGeneradoPor");
            SQL.AppendLine("ON " + DbSchema + ".Anticipo.GeneradoPor COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumGeneradoPor.DbValue");
            SQL.AppendLine("LEFT OUTER  JOIN Cliente ON  " + DbSchema + ".Anticipo.CodigoCliente = Cliente.codigo");
            SQL.AppendLine("      AND " + DbSchema + ".Anticipo.ConsecutivoCompania = Cliente.ConsecutivoCompania");
            SQL.AppendLine("LEFT OUTER  JOIN Adm.Proveedor ON  " + DbSchema + ".Anticipo.CodigoProveedor = Adm.Proveedor.codigoProveedor");
            SQL.AppendLine("      AND " + DbSchema + ".Anticipo.ConsecutivoCompania = Adm.Proveedor.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN dbo.Moneda ON  " + DbSchema + ".Anticipo.Moneda = dbo.Moneda.Nombre");
            SQL.AppendLine("INNER JOIN Saw.CuentaBancaria ON  " + DbSchema + ".Anticipo.CodigoCuentaBancaria = Saw.CuentaBancaria.Codigo");
            SQL.AppendLine("      AND " + DbSchema + ".Anticipo.ConsecutivoCompania = Saw.CuentaBancaria.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN Adm.ConceptoBancario ON  " + DbSchema + ".Anticipo.CodigoConceptoBancario = Adm.ConceptoBancario.codigo");
            SQL.AppendLine("LEFT OUTER  JOIN dbo.Cotizacion ON  " + DbSchema + ".Anticipo.NumeroCotizacion = dbo.Cotizacion.Numero");
            SQL.AppendLine("      AND " + DbSchema + ".Anticipo.ConsecutivoCompania = dbo.Cotizacion.ConsecutivoCompania");
            SQL.AppendLine("LEFT OUTER  JOIN Adm.Rendicion ON  " + DbSchema + ".Anticipo.ConsecutivoRendicion = Adm.Rendicion.consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Anticipo.ConsecutivoCompania = Adm.Rendicion.ConsecutivoCompania");
            SQL.AppendLine("INNER JOIN dbo.Moneda as Monedas ON  " + DbSchema + ".Anticipo.CodigoMoneda = Monedas.Codigo");
            SQL.AppendLine("INNER JOIN Adm.Caja ON  " + DbSchema + ".Anticipo.ConsecutivoCaja = Adm.Caja.Consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Anticipo.ConsecutivoCompania = Adm.Caja.Consecutivo");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoAnticipo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Status" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Tipo" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@Numero" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@CodigoCliente" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@CodigoProveedor" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@Moneda" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@Cambio" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@GeneraMovBancario" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@CodigoCuentaBancaria" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@CodigoConceptoBancario" + InsSql.VarCharTypeForDb(8) + ",");
            SQL.AppendLine("@GeneraImpuestoBancario" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@FechaAnulacion" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@FechaCancelacion" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@FechaDevolucion" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(255) + " = '',");
            SQL.AppendLine("@MontoTotal" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoUsado" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoDevuelto" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoDiferenciaEnDevolucion" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@DiferenciaEsIDB" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@EsUnaDevolucion" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@NumeroDelAnticipoDevuelto" + InsSql.NumericTypeForDb(10, 0) + " = 0,");
            SQL.AppendLine("@NumeroCheque" + InsSql.VarCharTypeForDb(15) + " = '',");
            SQL.AppendLine("@AsociarAnticipoACotiz" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@NumeroCotizacion" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@ConsecutivoRendicion" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@CodigoMoneda" + InsSql.VarCharTypeForDb(4) + ",");
            SQL.AppendLine("@NombreBeneficiario" + InsSql.VarCharTypeForDb(60) + " = '',");
            SQL.AppendLine("@CodigoBeneficiario" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@AsociarAnticipoACaja" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@ConsecutivoCaja" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroDeCobranzaAsociado" + InsSql.VarCharTypeForDb(12) + " = '',");
            SQL.AppendLine("@GeneradoPor" + InsSql.CharTypeForDb(1) + " = '0'");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".Anticipo(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            ConsecutivoAnticipo,");
            SQL.AppendLine("            Status,");
            SQL.AppendLine("            Tipo,");
            SQL.AppendLine("            Fecha,");
            SQL.AppendLine("            Numero,");
            SQL.AppendLine("            CodigoCliente,");
            SQL.AppendLine("            CodigoProveedor,");
            SQL.AppendLine("            Moneda,");
            SQL.AppendLine("            Cambio,");
            SQL.AppendLine("            GeneraMovBancario,");
            SQL.AppendLine("            CodigoCuentaBancaria,");
            SQL.AppendLine("            CodigoConceptoBancario,");
            SQL.AppendLine("            GeneraImpuestoBancario,");
            SQL.AppendLine("            FechaAnulacion,");
            SQL.AppendLine("            FechaCancelacion,");
            SQL.AppendLine("            FechaDevolucion,");
            SQL.AppendLine("            Descripcion,");
            SQL.AppendLine("            MontoTotal,");
            SQL.AppendLine("            MontoUsado,");
            SQL.AppendLine("            MontoDevuelto,");
            SQL.AppendLine("            MontoDiferenciaEnDevolucion,");
            SQL.AppendLine("            DiferenciaEsIDB,");
            SQL.AppendLine("            EsUnaDevolucion,");
            SQL.AppendLine("            NumeroDelAnticipoDevuelto,");
            SQL.AppendLine("            NumeroCheque,");
            SQL.AppendLine("            AsociarAnticipoACotiz,");
            SQL.AppendLine("            NumeroCotizacion,");
            SQL.AppendLine("            ConsecutivoRendicion,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion,");
            SQL.AppendLine("            CodigoMoneda,");
            SQL.AppendLine("            NombreBeneficiario,");
            SQL.AppendLine("            CodigoBeneficiario,");
            SQL.AppendLine("            AsociarAnticipoACaja,");
            SQL.AppendLine("            ConsecutivoCaja,");
            SQL.AppendLine("            NumeroDeCobranzaAsociado,");
            SQL.AppendLine("            GeneradoPor)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @ConsecutivoAnticipo,");
            SQL.AppendLine("            @Status,");
            SQL.AppendLine("            @Tipo,");
            SQL.AppendLine("            @Fecha,");
            SQL.AppendLine("            @Numero,");
            SQL.AppendLine("            @CodigoCliente,");
            SQL.AppendLine("            @CodigoProveedor,");
            SQL.AppendLine("            @Moneda,");
            SQL.AppendLine("            @Cambio,");
            SQL.AppendLine("            @GeneraMovBancario,");
            SQL.AppendLine("            @CodigoCuentaBancaria,");
            SQL.AppendLine("            @CodigoConceptoBancario,");
            SQL.AppendLine("            @GeneraImpuestoBancario,");
            SQL.AppendLine("            @FechaAnulacion,");
            SQL.AppendLine("            @FechaCancelacion,");
            SQL.AppendLine("            @FechaDevolucion,");
            SQL.AppendLine("            @Descripcion,");
            SQL.AppendLine("            @MontoTotal,");
            SQL.AppendLine("            @MontoUsado,");
            SQL.AppendLine("            @MontoDevuelto,");
            SQL.AppendLine("            @MontoDiferenciaEnDevolucion,");
            SQL.AppendLine("            @DiferenciaEsIDB,");
            SQL.AppendLine("            @EsUnaDevolucion,");
            SQL.AppendLine("            @NumeroDelAnticipoDevuelto,");
            SQL.AppendLine("            @NumeroCheque,");
            SQL.AppendLine("            @AsociarAnticipoACotiz,");
            SQL.AppendLine("            @NumeroCotizacion,");
            SQL.AppendLine("            @ConsecutivoRendicion,");
            SQL.AppendLine("            @NombreOperador,");
            SQL.AppendLine("            @FechaUltimaModificacion,");
            SQL.AppendLine("            @CodigoMoneda,");
            SQL.AppendLine("            @NombreBeneficiario,");
            SQL.AppendLine("            @CodigoBeneficiario,");
            SQL.AppendLine("            @AsociarAnticipoACaja,");
            SQL.AppendLine("            @ConsecutivoCaja,");
            SQL.AppendLine("            @NumeroDeCobranzaAsociado,");
            SQL.AppendLine("            @GeneradoPor)");
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
            SQL.AppendLine("@ConsecutivoAnticipo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Status" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Tipo" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@Numero" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@CodigoCliente" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@CodigoProveedor" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@Moneda" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@Cambio" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@GeneraMovBancario" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@CodigoCuentaBancaria" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@CodigoConceptoBancario" + InsSql.VarCharTypeForDb(8) + ",");
            SQL.AppendLine("@GeneraImpuestoBancario" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@FechaAnulacion" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@FechaCancelacion" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@FechaDevolucion" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@Descripcion" + InsSql.VarCharTypeForDb(255) + ",");
            SQL.AppendLine("@MontoTotal" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoUsado" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoDevuelto" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoDiferenciaEnDevolucion" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@DiferenciaEsIDB" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@EsUnaDevolucion" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@NumeroDelAnticipoDevuelto" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroCheque" + InsSql.VarCharTypeForDb(15) + ",");
            SQL.AppendLine("@AsociarAnticipoACotiz" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@NumeroCotizacion" + InsSql.VarCharTypeForDb(11) + ",");
            SQL.AppendLine("@ConsecutivoRendicion" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@CodigoMoneda" + InsSql.VarCharTypeForDb(4) + ",");
            SQL.AppendLine("@NombreBeneficiario" + InsSql.VarCharTypeForDb(60) + ",");
            SQL.AppendLine("@CodigoBeneficiario" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@AsociarAnticipoACaja" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ConsecutivoCaja" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroDeCobranzaAsociado" + InsSql.VarCharTypeForDb(12) + ",");
            SQL.AppendLine("@GeneradoPor" + InsSql.CharTypeForDb(1) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Anticipo WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoAnticipo = @ConsecutivoAnticipo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Anticipo WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoAnticipo = @ConsecutivoAnticipo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_AnticipoCanBeUpdated @ConsecutivoCompania,@ConsecutivoAnticipo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".Anticipo");
            SQL.AppendLine("            SET Status = @Status,");
            SQL.AppendLine("               Tipo = @Tipo,");
            SQL.AppendLine("               Fecha = @Fecha,");
            SQL.AppendLine("               Numero = @Numero,");
            SQL.AppendLine("               CodigoCliente = @CodigoCliente,");
            SQL.AppendLine("               CodigoProveedor = @CodigoProveedor,");
            SQL.AppendLine("               Moneda = @Moneda,");
            SQL.AppendLine("               Cambio = @Cambio,");
            SQL.AppendLine("               GeneraMovBancario = @GeneraMovBancario,");
            SQL.AppendLine("               CodigoCuentaBancaria = @CodigoCuentaBancaria,");
            SQL.AppendLine("               CodigoConceptoBancario = @CodigoConceptoBancario,");
            SQL.AppendLine("               GeneraImpuestoBancario = @GeneraImpuestoBancario,");
            SQL.AppendLine("               FechaAnulacion = @FechaAnulacion,");
            SQL.AppendLine("               FechaCancelacion = @FechaCancelacion,");
            SQL.AppendLine("               FechaDevolucion = @FechaDevolucion,");
            SQL.AppendLine("               Descripcion = @Descripcion,");
            SQL.AppendLine("               MontoTotal = @MontoTotal,");
            SQL.AppendLine("               MontoUsado = @MontoUsado,");
            SQL.AppendLine("               MontoDevuelto = @MontoDevuelto,");
            SQL.AppendLine("               MontoDiferenciaEnDevolucion = @MontoDiferenciaEnDevolucion,");
            SQL.AppendLine("               DiferenciaEsIDB = @DiferenciaEsIDB,");
            SQL.AppendLine("               EsUnaDevolucion = @EsUnaDevolucion,");
            SQL.AppendLine("               NumeroDelAnticipoDevuelto = @NumeroDelAnticipoDevuelto,");
            SQL.AppendLine("               NumeroCheque = @NumeroCheque,");
            SQL.AppendLine("               AsociarAnticipoACotiz = @AsociarAnticipoACotiz,");
            SQL.AppendLine("               NumeroCotizacion = @NumeroCotizacion,");
            SQL.AppendLine("               ConsecutivoRendicion = @ConsecutivoRendicion,");
            SQL.AppendLine("               NombreOperador = @NombreOperador,");
            SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion,");
            SQL.AppendLine("               CodigoMoneda = @CodigoMoneda,");
            SQL.AppendLine("               NombreBeneficiario = @NombreBeneficiario,");
            SQL.AppendLine("               CodigoBeneficiario = @CodigoBeneficiario,");
            SQL.AppendLine("               AsociarAnticipoACaja = @AsociarAnticipoACaja,");
            SQL.AppendLine("               ConsecutivoCaja = @ConsecutivoCaja,");
            SQL.AppendLine("               NumeroDeCobranzaAsociado = @NumeroDeCobranzaAsociado,");
            SQL.AppendLine("               GeneradoPor = @GeneradoPor");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoAnticipo = @ConsecutivoAnticipo");
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
            SQL.AppendLine("@ConsecutivoAnticipo" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Anticipo WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoAnticipo = @ConsecutivoAnticipo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Anticipo WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoAnticipo = @ConsecutivoAnticipo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_AnticipoCanBeDeleted @ConsecutivoCompania,@ConsecutivoAnticipo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".Anticipo");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoAnticipo = @ConsecutivoAnticipo");
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
            SQL.AppendLine("@ConsecutivoAnticipo" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         Anticipo.ConsecutivoCompania,");
            SQL.AppendLine("         Anticipo.ConsecutivoAnticipo,");
            SQL.AppendLine("         Anticipo.Status,");
            SQL.AppendLine("         Anticipo.Tipo,");
            SQL.AppendLine("         Anticipo.Fecha,");
            SQL.AppendLine("         Anticipo.Numero,");
            SQL.AppendLine("         Anticipo.CodigoCliente,");
            SQL.AppendLine("         Gv_Cliente_B1.nombre AS NombreCliente,");
            SQL.AppendLine("         Anticipo.CodigoProveedor,");
            SQL.AppendLine("         Gv_Proveedor_B1.nombreProveedor AS NombreProveedor,");
            SQL.AppendLine("         Anticipo.Moneda,");
            SQL.AppendLine("         Anticipo.Cambio,");
            SQL.AppendLine("         Anticipo.GeneraMovBancario,");
            SQL.AppendLine("         Anticipo.CodigoCuentaBancaria,");
            SQL.AppendLine("         Gv_CuentaBancaria_B1.Nombrecuenta AS NombreCuentaBancaria,");
            SQL.AppendLine("         Anticipo.CodigoConceptoBancario,");
            SQL.AppendLine("         Gv_ConceptoBancario_B1.descripcion AS NombreConceptoBancario,");
            SQL.AppendLine("         Anticipo.GeneraImpuestoBancario,");
            SQL.AppendLine("         Anticipo.FechaAnulacion,");
            SQL.AppendLine("         Anticipo.FechaCancelacion,");
            SQL.AppendLine("         Anticipo.FechaDevolucion,");
            SQL.AppendLine("         Anticipo.Descripcion,");
            SQL.AppendLine("         Anticipo.MontoTotal,");
            SQL.AppendLine("         Anticipo.MontoUsado,");
            SQL.AppendLine("         Anticipo.MontoDevuelto,");
            SQL.AppendLine("         Anticipo.MontoDiferenciaEnDevolucion,");
            SQL.AppendLine("         Anticipo.DiferenciaEsIDB,");
            SQL.AppendLine("         Anticipo.EsUnaDevolucion,");
            SQL.AppendLine("         Anticipo.NumeroDelAnticipoDevuelto,");
            SQL.AppendLine("         Anticipo.NumeroCheque,");
            SQL.AppendLine("         Anticipo.AsociarAnticipoACotiz,");
            SQL.AppendLine("         Anticipo.NumeroCotizacion,");
            SQL.AppendLine("         Anticipo.ConsecutivoRendicion,");
            SQL.AppendLine("         Anticipo.NombreOperador,");
            SQL.AppendLine("         Anticipo.FechaUltimaModificacion,");
            SQL.AppendLine("         Anticipo.CodigoMoneda,");
            SQL.AppendLine("         Anticipo.NombreBeneficiario,");
            SQL.AppendLine("         Anticipo.CodigoBeneficiario,");
            SQL.AppendLine("         Anticipo.AsociarAnticipoACaja,");
            SQL.AppendLine("         Anticipo.ConsecutivoCaja,");
            //SQL.AppendLine("         Gv_Caja_B1.NombreCaja AS NombreCaja,");
            SQL.AppendLine("         Anticipo.NumeroDeCobranzaAsociado,");
            SQL.AppendLine("         Anticipo.GeneradoPor,");
            SQL.AppendLine("         CAST(Anticipo.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         Anticipo.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".Anticipo");
            SQL.AppendLine("             INNER JOIN dbo.Gv_Cliente_B1 ON " + DbSchema + ".Anticipo.CodigoCliente = dbo.Gv_Cliente_B1.codigo");
            SQL.AppendLine("             INNER JOIN Adm.Gv_Proveedor_B1 ON " + DbSchema + ".Anticipo.CodigoProveedor = Adm.Gv_Proveedor_B1.codigoProveedor");
            SQL.AppendLine("             INNER JOIN " + DbSchema + ".Gv_Moneda_B1 ON " + DbSchema + ".Anticipo.Moneda = " + DbSchema + ".Gv_Moneda_B1.Nombre");
            SQL.AppendLine("             INNER JOIN Saw.Gv_CuentaBancaria_B1 ON " + DbSchema + ".Anticipo.CodigoCuentaBancaria = Saw.Gv_CuentaBancaria_B1.Codigo");
            SQL.AppendLine("             INNER JOIN Adm.Gv_ConceptoBancario_B1 ON " + DbSchema + ".Anticipo.CodigoConceptoBancario = Adm.Gv_ConceptoBancario_B1.codigo");
            //SQL.AppendLine("             INNER JOIN Comun.Gv_Cotizacion_B1 ON " + DbSchema + ".Anticipo.NumeroCotizacion = Comun.Gv_Cotizacion_B1.Numero");
            SQL.AppendLine("             INNER JOIN Adm.Gv_Rendicion_B1 ON " + DbSchema + ".Anticipo.ConsecutivoRendicion = Adm.Gv_Rendicion_B1.consecutivo");
            SQL.AppendLine("             INNER JOIN " + DbSchema + ".Gv_Moneda_B1 AS Gv_Monedas ON " + DbSchema + ".Anticipo.CodigoMoneda = Gv_Monedas.Codigo");
            //SQL.AppendLine("             INNER JOIN dbo.Gv_Caja_B1 ON " + DbSchema + ".Anticipo.ConsecutivoCaja = dbo.Gv_Caja_B1.consecutivo");
            SQL.AppendLine("      WHERE Anticipo.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND Anticipo.ConsecutivoAnticipo = @ConsecutivoAnticipo");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_Anticipo_B1.StatusStr,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Anticipo_B1.TipoStr,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Anticipo_B1.Fecha,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Anticipo_B1.Numero,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Anticipo_B1.CodigoCliente,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.nombre AS NombreCliente,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Anticipo_B1.CodigoProveedor,");
            SQL.AppendLine("      Adm.Gv_Proveedor_B1.nombreProveedor AS NombreProveedor,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Anticipo_B1.MontoTotal,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Anticipo_B1.MontoUsado,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Anticipo_B1.MontoDevuelto,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Anticipo_B1.NumeroCheque,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Anticipo_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Anticipo_B1.ConsecutivoAnticipo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Anticipo_B1.Status,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Anticipo_B1.Tipo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Anticipo_B1.ConsecutivoRendicion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Anticipo_B1.ConsecutivoCaja");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Anticipo_B1");
            SQL.AppendLine("      LEFT OUTER JOIN dbo.Gv_Cliente_B1 ON  " + DbSchema + ".Gv_Anticipo_B1.CodigoCliente = dbo.Gv_Cliente_B1.codigo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Anticipo_B1.ConsecutivoCompania = dbo.Gv_Cliente_B1.ConsecutivoCompania");
            SQL.AppendLine("      LEFT OUTER JOIN Adm.Gv_Proveedor_B1 ON  " + DbSchema + ".Gv_Anticipo_B1.CodigoProveedor = Adm.Gv_Proveedor_B1.codigoProveedor");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Anticipo_B1.ConsecutivoCompania = Adm.Gv_Proveedor_B1.ConsecutivoCompania");
            SQL.AppendLine("      INNER JOIN " + DbSchema + ".Gv_Moneda_B1 ON  " + DbSchema + ".Gv_Anticipo_B1.Moneda = " + DbSchema + ".Gv_Moneda_B1.Nombre");
            SQL.AppendLine("      INNER JOIN Saw.Gv_CuentaBancaria_B1 ON  " + DbSchema + ".Gv_Anticipo_B1.CodigoCuentaBancaria = Saw.Gv_CuentaBancaria_B1.Codigo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Anticipo_B1.ConsecutivoCompania = Saw.Gv_CuentaBancaria_B1.ConsecutivoCompania");
            SQL.AppendLine("      INNER JOIN Adm.Gv_ConceptoBancario_B1 ON  " + DbSchema + ".Gv_Anticipo_B1.CodigoConceptoBancario = Adm.Gv_ConceptoBancario_B1.codigo");
            //SQL.AppendLine("      INNER JOIN Comun.Gv_Cotizacion_B1 ON  " + DbSchema + ".Gv_Anticipo_B1.NumeroCotizacion = Comun.Gv_Cotizacion_B1.Numero");
            //SQL.AppendLine("      AND " + DbSchema + ".Gv_Anticipo_B1.ConsecutivoCompania = Comun.Gv_Cotizacion_B1.ConsecutivoCompania");
            SQL.AppendLine("      LEFT OUTER JOIN Adm.Gv_Rendicion_B1 ON  " + DbSchema + ".Gv_Anticipo_B1.ConsecutivoRendicion = Adm.Gv_Rendicion_B1.consecutivo");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Anticipo_B1.ConsecutivoCompania = Adm.Gv_Rendicion_B1.ConsecutivoCompania");
            SQL.AppendLine("      INNER JOIN " + DbSchema + ".Gv_Moneda_B1  AS Gv_Monedas ON  " + DbSchema + ".Gv_Anticipo_B1.CodigoMoneda = Gv_Monedas.Codigo");
            //SQL.AppendLine("      INNER JOIN dbo.Gv_Caja_B1 ON  " + DbSchema + ".Gv_Anticipo_B1.ConsecutivoCaja = dbo.Gv_Caja_B1.consecutivo");
            //SQL.AppendLine("      AND " + DbSchema + ".Gv_Anticipo_B1.ConsecutivoCompania = dbo.Gv_Caja_B1.ConsecutivoCompania");
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
            SQL.AppendLine("      " + DbSchema + ".Anticipo.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Anticipo.ConsecutivoAnticipo,");
            SQL.AppendLine("      " + DbSchema + ".Anticipo.Status,");
            SQL.AppendLine("      " + DbSchema + ".Anticipo.Fecha,");
            SQL.AppendLine("      " + DbSchema + ".Anticipo.Numero,");
            SQL.AppendLine("      " + DbSchema + ".Anticipo.CodigoCliente,");
            SQL.AppendLine("      " + DbSchema + ".Anticipo.CodigoProveedor,");
            SQL.AppendLine("      " + DbSchema + ".Anticipo.Moneda,");
            SQL.AppendLine("      " + DbSchema + ".Anticipo.CodigoCuentaBancaria,");
            SQL.AppendLine("      " + DbSchema + ".Anticipo.CodigoConceptoBancario,");
            SQL.AppendLine("      " + DbSchema + ".Anticipo.NumeroCheque,");
            SQL.AppendLine("      " + DbSchema + ".Anticipo.NumeroCotizacion,");
            SQL.AppendLine("      " + DbSchema + ".Anticipo.ConsecutivoRendicion,");
            SQL.AppendLine("      " + DbSchema + ".Anticipo.CodigoMoneda,");
            SQL.AppendLine("      " + DbSchema + ".Anticipo.ConsecutivoCaja");
            //SQL.AppendLine("      ," + DbSchema + ".Anticipo.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("      FROM " + DbSchema + ".Anticipo");
            SQL.AppendLine("      WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("          AND ConsecutivoAnticipo IN (");
            SQL.AppendLine("            SELECT  ConsecutivoAnticipo ");
            SQL.AppendLine("            FROM OPENXML( @hdoc, 'GpData/GpResult',2) ");
            SQL.AppendLine("            WITH (ConsecutivoAnticipo int) AS XmlFKTmp) ");
            SQL.AppendLine(" EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".Anticipo", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumStatusAnticipo", LibTpvCreator.SqlViewStandardEnum(typeof(eStatusAnticipo), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDeAnticipo", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDeAnticipo), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumGeneradoPor", LibTpvCreator.SqlViewStandardEnum(typeof(eGeneradoPor), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_Anticipo_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AnticipoINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AnticipoUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AnticipoDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AnticipoGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AnticipoSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_AnticipoGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".Anticipo", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_AnticipoINS");
            vResult = insSp.Drop(DbSchema + ".Gp_AnticipoUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_AnticipoDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_AnticipoGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_AnticipoGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_AnticipoSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_Anticipo_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumStatusAnticipo") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDeAnticipo") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumGeneradoPor") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsAnticipoED

} //End of namespace Galac.Adm.Dal.CAnticipo

