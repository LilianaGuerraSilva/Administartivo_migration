using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Dal.CajaChica {
    public class clsProveedorED: LibED {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsProveedorED(): base(){
            DbSchema = "dbo";
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("Proveedor", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnProConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoProveedor" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT nnProCodigoProv NOT NULL, ");
            SQL.AppendLine("NombreProveedor" + InsSql.VarCharTypeForDb(60) + " CONSTRAINT nnProNombreProv NOT NULL, ");
            SQL.AppendLine("Contacto" + InsSql.VarCharTypeForDb(35) + " CONSTRAINT d_ProCo DEFAULT (''), ");
            SQL.AppendLine("NumeroRIF" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_ProNuRI DEFAULT (''), ");
            SQL.AppendLine("NumeroNIT" + InsSql.VarCharTypeForDb(12) + " CONSTRAINT d_ProNuNI DEFAULT (''), ");
            SQL.AppendLine("TipoDePersona" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_ProTiDePe DEFAULT ('0'), ");
            SQL.AppendLine("CodigoRetencionUsual" + InsSql.VarCharTypeForDb(6) + " CONSTRAINT nnProCodigoRete NOT NULL, ");
            SQL.AppendLine("Telefonos" + InsSql.VarCharTypeForDb(40) + " CONSTRAINT d_ProTe DEFAULT (''), ");
            SQL.AppendLine("Direccion" + InsSql.VarCharTypeForDb(255) + " CONSTRAINT d_ProDi DEFAULT (''), ");
            SQL.AppendLine("Fax" + InsSql.VarCharTypeForDb(25) + " CONSTRAINT d_ProFa DEFAULT (''), ");
            SQL.AppendLine("Email" + InsSql.VarCharTypeForDb(40) + " CONSTRAINT d_ProEm DEFAULT (''), ");
            SQL.AppendLine("TipodeProveedor" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_ProTiPr DEFAULT (''), ");
            SQL.AppendLine("TipoDeProveedorDeLibrosFiscales" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_ProTiDePrDeLiFi DEFAULT ('0'), ");
            SQL.AppendLine("PorcentajeRetencionIVA" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_ProPoReIV DEFAULT (0), ");
            SQL.AppendLine("CuentaContableCxP" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_ProCuCoCxP DEFAULT (''), ");
            SQL.AppendLine("CuentaContableGastos" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_ProCuCoGa DEFAULT (''), ");
            SQL.AppendLine("CuentaContableAnticipo" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_ProCuCoAn DEFAULT (''), ");
            SQL.AppendLine("CodigoLote" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT d_ProCoLo DEFAULT (''), ");
            SQL.AppendLine("Beneficiario" + InsSql.VarCharTypeForDb(60) + " CONSTRAINT d_ProBe DEFAULT (''), ");
            SQL.AppendLine("UsarBeneficiarioImpCheq" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnProUsarBenefi NOT NULL, ");
            SQL.AppendLine("TipoDocumentoIdentificacion" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_ProTiDoId DEFAULT ('0'), ");
            SQL.AppendLine("EsAgenteDeRetencionIva" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnProEsAgenteDe NOT NULL, ");
            SQL.AppendLine("Nombre" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_ProNo DEFAULT (''), ");
            SQL.AppendLine("ApellidoPaterno" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_ProApPa DEFAULT (''), ");
            SQL.AppendLine("ApellidoMaterno" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_ProApMa DEFAULT (''), ");
            SQL.AppendLine("CodigoContribuyente" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_ProCoCo DEFAULT (''), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_Proveedor PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, CodigoProveedor ASC)");
            SQL.AppendLine(", CONSTRAINT fk_ProveedorTablaRetencion FOREIGN KEY (CodigoRetencionUsual)");
            SQL.AppendLine("REFERENCES dbo.TablaRetencion(codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_ProveedorTipoProveedor FOREIGN KEY (ConsecutivoCompania, TipodeProveedor)");
            SQL.AppendLine("REFERENCES Saw.TipoProveedor(ConsecutivoCompania, nombre)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_ProveedorCuenta FOREIGN KEY (ConsecutivoCompania, CuentaContableAnticipo)");
            SQL.AppendLine("REFERENCES dbo.Cuenta(ConsecutivoCompania, codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(",CONSTRAINT u_ProNombreProveedor UNIQUE NONCLUSTERED (ConsecutivoCompania, NombreProveedor)");
            SQL.AppendLine(",CONSTRAINT u_ProNumeroRIF UNIQUE NONCLUSTERED (ConsecutivoCompania, NumeroRIF)");
            SQL.AppendLine(",CONSTRAINT u_ProBeneficiario UNIQUE NONCLUSTERED (ConsecutivoCompania, Beneficiario)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT Proveedor.ConsecutivoCompania, Proveedor.CodigoProveedor, Proveedor.NombreProveedor, Proveedor.Contacto");
            SQL.AppendLine(", Proveedor.NumeroRIF, Proveedor.NumeroNIT, Proveedor.TipoDePersona, " + DbSchema + ".Gv_EnumTipodePersonaRetencion.StrValue AS TipoDePersonaStr, Proveedor.CodigoRetencionUsual");
            SQL.AppendLine(", Proveedor.Telefonos, Proveedor.Direccion, Proveedor.Fax, Proveedor.Email");
            SQL.AppendLine(", Proveedor.TipodeProveedor, Proveedor.TipoDeProveedorDeLibrosFiscales, " + DbSchema + ".Gv_EnumTipoDeProveedorDeLibrosFiscales.StrValue AS TipoDeProveedorDeLibrosFiscalesStr, Proveedor.PorcentajeRetencionIVA, Proveedor.CuentaContableCxP");
            SQL.AppendLine(", Proveedor.CuentaContableGastos, Proveedor.CuentaContableAnticipo, Proveedor.CodigoLote, Proveedor.Beneficiario");
            SQL.AppendLine(", Proveedor.UsarBeneficiarioImpCheq, Proveedor.TipoDocumentoIdentificacion, " + DbSchema + ".Gv_EnumTipoDocumentoIdentificacion.StrValue AS TipoDocumentoIdentificacionStr, Proveedor.EsAgenteDeRetencionIva, Proveedor.Nombre");
            SQL.AppendLine(", Proveedor.ApellidoPaterno, Proveedor.ApellidoMaterno, Proveedor.CodigoContribuyente, Proveedor.NombreOperador");
            SQL.AppendLine(", Proveedor.FechaUltimaModificacion");
            SQL.AppendLine(", Proveedor.fldTimeStamp, CAST(Proveedor.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".Proveedor");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipodePersonaRetencion");
            SQL.AppendLine("ON " + DbSchema + ".Proveedor.TipoDePersona COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipodePersonaRetencion.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoDeProveedorDeLibrosFiscales");
            SQL.AppendLine("ON " + DbSchema + ".Proveedor.TipoDeProveedorDeLibrosFiscales COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDeProveedorDeLibrosFiscales.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoDocumentoIdentificacion");
            SQL.AppendLine("ON " + DbSchema + ".Proveedor.TipoDocumentoIdentificacion COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDocumentoIdentificacion.DbValue");
            SQL.AppendLine("INNER JOIN dbo.TablaRetencion ON  " + DbSchema + ".Proveedor.CodigoRetencionUsual = dbo.TablaRetencion.codigo AND " + DbSchema + ".Proveedor.TipoDePersona = dbo.TablaRetencion.TipoDePersona");
            SQL.AppendLine("INNER JOIN Saw.TipoProveedor ON  " + DbSchema + ".Proveedor.TipodeProveedor = Saw.TipoProveedor.nombre");
            SQL.AppendLine("      AND " + DbSchema + ".Proveedor.ConsecutivoCompania = Saw.TipoProveedor.ConsecutivoCompania");
            //SQL.AppendLine("INNER JOIN dbo.Cuenta ON  " + DbSchema + ".Proveedor.CuentaContableAnticipo = dbo.Cuenta.codigo");
            //SQL.AppendLine("      AND " + DbSchema + ".Proveedor.ConsecutivoCompania = dbo.Cuenta.ConsecutivoPeriodo");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoProveedor" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@NombreProveedor" + InsSql.VarCharTypeForDb(60) + " = '',");
            SQL.AppendLine("@Contacto" + InsSql.VarCharTypeForDb(35) + " = '',");
            SQL.AppendLine("@NumeroRIF" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@NumeroNIT" + InsSql.VarCharTypeForDb(12) + " = '',");
            SQL.AppendLine("@TipoDePersona" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@CodigoRetencionUsual" + InsSql.VarCharTypeForDb(6) + ",");
            SQL.AppendLine("@Telefonos" + InsSql.VarCharTypeForDb(40) + " = '',");
            SQL.AppendLine("@Direccion" + InsSql.VarCharTypeForDb(255) + " = '',");
            SQL.AppendLine("@Fax" + InsSql.VarCharTypeForDb(25) + " = '',");
            SQL.AppendLine("@Email" + InsSql.VarCharTypeForDb(40) + " = '',");
            SQL.AppendLine("@TipodeProveedor" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@TipoDeProveedorDeLibrosFiscales" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@PorcentajeRetencionIVA" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@CuentaContableCxP" + InsSql.VarCharTypeForDb(30) + " = '',");
            SQL.AppendLine("@CuentaContableGastos" + InsSql.VarCharTypeForDb(30) + " = '',");
            SQL.AppendLine("@CuentaContableAnticipo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@CodigoLote" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@Beneficiario" + InsSql.VarCharTypeForDb(60) + " = '',");
            SQL.AppendLine("@UsarBeneficiarioImpCheq" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@TipoDocumentoIdentificacion" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@EsAgenteDeRetencionIva" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@ApellidoPaterno" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@ApellidoMaterno" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@CodigoContribuyente" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + " = '01/01/1900'");
            return SQL.ToString();
        }

        private string SqlSpIns() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".Proveedor(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            CodigoProveedor,");
            SQL.AppendLine("            NombreProveedor,");
            SQL.AppendLine("            Contacto,");
            SQL.AppendLine("            NumeroRIF,");
            SQL.AppendLine("            NumeroNIT,");
            SQL.AppendLine("            TipoDePersona,");
            SQL.AppendLine("            CodigoRetencionUsual,");
            SQL.AppendLine("            Telefonos,");
            SQL.AppendLine("            Direccion,");
            SQL.AppendLine("            Fax,");
            SQL.AppendLine("            Email,");
            SQL.AppendLine("            TipodeProveedor,");
            SQL.AppendLine("            TipoDeProveedorDeLibrosFiscales,");
            SQL.AppendLine("            PorcentajeRetencionIVA,");
            SQL.AppendLine("            CuentaContableCxP,");
            SQL.AppendLine("            CuentaContableGastos,");
            SQL.AppendLine("            CuentaContableAnticipo,");
            SQL.AppendLine("            CodigoLote,");
            SQL.AppendLine("            Beneficiario,");
            SQL.AppendLine("            UsarBeneficiarioImpCheq,");
            SQL.AppendLine("            TipoDocumentoIdentificacion,");
            SQL.AppendLine("            EsAgenteDeRetencionIva,");
            SQL.AppendLine("            Nombre,");
            SQL.AppendLine("            ApellidoPaterno,");
            SQL.AppendLine("            ApellidoMaterno,");
            SQL.AppendLine("            CodigoContribuyente,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @CodigoProveedor,");
            SQL.AppendLine("            @NombreProveedor,");
            SQL.AppendLine("            @Contacto,");
            SQL.AppendLine("            @NumeroRIF,");
            SQL.AppendLine("            @NumeroNIT,");
            SQL.AppendLine("            @TipoDePersona,");
            SQL.AppendLine("            @CodigoRetencionUsual,");
            SQL.AppendLine("            @Telefonos,");
            SQL.AppendLine("            @Direccion,");
            SQL.AppendLine("            @Fax,");
            SQL.AppendLine("            @Email,");
            SQL.AppendLine("            @TipodeProveedor,");
            SQL.AppendLine("            @TipoDeProveedorDeLibrosFiscales,");
            SQL.AppendLine("            @PorcentajeRetencionIVA,");
            SQL.AppendLine("            @CuentaContableCxP,");
            SQL.AppendLine("            @CuentaContableGastos,");
            SQL.AppendLine("            @CuentaContableAnticipo,");
            SQL.AppendLine("            @CodigoLote,");
            SQL.AppendLine("            @Beneficiario,");
            SQL.AppendLine("            @UsarBeneficiarioImpCheq,");
            SQL.AppendLine("            @TipoDocumentoIdentificacion,");
            SQL.AppendLine("            @EsAgenteDeRetencionIva,");
            SQL.AppendLine("            @Nombre,");
            SQL.AppendLine("            @ApellidoPaterno,");
            SQL.AppendLine("            @ApellidoMaterno,");
            SQL.AppendLine("            @CodigoContribuyente,");
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
            SQL.AppendLine("@CodigoProveedor" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@NombreProveedor" + InsSql.VarCharTypeForDb(60) + ",");
            SQL.AppendLine("@Contacto" + InsSql.VarCharTypeForDb(35) + ",");
            SQL.AppendLine("@NumeroRIF" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@NumeroNIT" + InsSql.VarCharTypeForDb(12) + ",");
            SQL.AppendLine("@TipoDePersona" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@CodigoRetencionUsual" + InsSql.VarCharTypeForDb(6) + ",");
            SQL.AppendLine("@Telefonos" + InsSql.VarCharTypeForDb(40) + ",");
            SQL.AppendLine("@Direccion" + InsSql.VarCharTypeForDb(255) + ",");
            SQL.AppendLine("@Fax" + InsSql.VarCharTypeForDb(25) + ",");
            SQL.AppendLine("@Email" + InsSql.VarCharTypeForDb(40) + ",");
            SQL.AppendLine("@TipodeProveedor" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@TipoDeProveedorDeLibrosFiscales" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@PorcentajeRetencionIVA" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@CuentaContableCxP" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@CuentaContableGastos" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@CuentaContableAnticipo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@CodigoLote" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@Beneficiario" + InsSql.VarCharTypeForDb(60) + ",");
            SQL.AppendLine("@UsarBeneficiarioImpCheq" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@TipoDocumentoIdentificacion" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@EsAgenteDeRetencionIva" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@ApellidoPaterno" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@ApellidoMaterno" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@CodigoContribuyente" + InsSql.VarCharTypeForDb(20) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Proveedor WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Proveedor WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_ProveedorCanBeUpdated @ConsecutivoCompania,@CodigoProveedor, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".Proveedor");
            SQL.AppendLine("            SET NombreProveedor = @NombreProveedor,");
            SQL.AppendLine("               Contacto = @Contacto,");
            SQL.AppendLine("               NumeroRIF = @NumeroRIF,");
            SQL.AppendLine("               NumeroNIT = @NumeroNIT,");
            SQL.AppendLine("               TipoDePersona = @TipoDePersona,");
            SQL.AppendLine("               CodigoRetencionUsual = @CodigoRetencionUsual,");
            SQL.AppendLine("               Telefonos = @Telefonos,");
            SQL.AppendLine("               Direccion = @Direccion,");
            SQL.AppendLine("               Fax = @Fax,");
            SQL.AppendLine("               Email = @Email,");
            SQL.AppendLine("               TipodeProveedor = @TipodeProveedor,");
            SQL.AppendLine("               TipoDeProveedorDeLibrosFiscales = @TipoDeProveedorDeLibrosFiscales,");
            SQL.AppendLine("               PorcentajeRetencionIVA = @PorcentajeRetencionIVA,");
            SQL.AppendLine("               CuentaContableCxP = @CuentaContableCxP,");
            SQL.AppendLine("               CuentaContableGastos = @CuentaContableGastos,");
            SQL.AppendLine("               CuentaContableAnticipo = @CuentaContableAnticipo,");
            SQL.AppendLine("               CodigoLote = @CodigoLote,");
            SQL.AppendLine("               Beneficiario = @Beneficiario,");
            SQL.AppendLine("               UsarBeneficiarioImpCheq = @UsarBeneficiarioImpCheq,");
            SQL.AppendLine("               TipoDocumentoIdentificacion = @TipoDocumentoIdentificacion,");
            SQL.AppendLine("               EsAgenteDeRetencionIva = @EsAgenteDeRetencionIva,");
            SQL.AppendLine("               Nombre = @Nombre,");
            SQL.AppendLine("               ApellidoPaterno = @ApellidoPaterno,");
            SQL.AppendLine("               ApellidoMaterno = @ApellidoMaterno,");
            SQL.AppendLine("               CodigoContribuyente = @CodigoContribuyente,");
            SQL.AppendLine("               NombreOperador = @NombreOperador,");
            SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND CodigoProveedor = @CodigoProveedor");
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
            SQL.AppendLine("@CodigoProveedor" + InsSql.VarCharTypeForDb(10) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Proveedor WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Proveedor WHERE ConsecutivoCompania = @ConsecutivoCompania AND CodigoProveedor = @CodigoProveedor");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_ProveedorCanBeDeleted @ConsecutivoCompania,@CodigoProveedor, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".Proveedor");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND CodigoProveedor = @CodigoProveedor");
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
            SQL.AppendLine("@CodigoProveedor" + InsSql.VarCharTypeForDb(10));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         Proveedor.ConsecutivoCompania,");
            SQL.AppendLine("         Proveedor.CodigoProveedor,");
            SQL.AppendLine("         Proveedor.NombreProveedor,");
            SQL.AppendLine("         Proveedor.Contacto,");
            SQL.AppendLine("         Proveedor.NumeroRIF,");
            SQL.AppendLine("         Proveedor.NumeroNIT,");
            SQL.AppendLine("         Proveedor.TipoDePersona,");
            SQL.AppendLine("         Proveedor.CodigoRetencionUsual,");
            SQL.AppendLine("         Proveedor.Telefonos,");
            SQL.AppendLine("         Proveedor.Direccion,");
            SQL.AppendLine("         Proveedor.Fax,");
            SQL.AppendLine("         Proveedor.Email,");
            SQL.AppendLine("         Proveedor.TipodeProveedor,");
            SQL.AppendLine("         Proveedor.TipoDeProveedorDeLibrosFiscales,");
            SQL.AppendLine("         Proveedor.PorcentajeRetencionIVA,");
            SQL.AppendLine("         Proveedor.CuentaContableCxP,");
            SQL.AppendLine("         Proveedor.CuentaContableGastos,");
            SQL.AppendLine("         Proveedor.CuentaContableAnticipo,");
            SQL.AppendLine("         Proveedor.CodigoLote,");
            SQL.AppendLine("         Proveedor.Beneficiario,");
            SQL.AppendLine("         Proveedor.UsarBeneficiarioImpCheq,");
            SQL.AppendLine("         Proveedor.TipoDocumentoIdentificacion,");
            SQL.AppendLine("         Proveedor.EsAgenteDeRetencionIva,");
            SQL.AppendLine("         Proveedor.Nombre,");
            SQL.AppendLine("         Proveedor.ApellidoPaterno,");
            SQL.AppendLine("         Proveedor.ApellidoMaterno,");
            SQL.AppendLine("         Proveedor.CodigoContribuyente,");
            SQL.AppendLine("         Proveedor.NombreOperador,");
            SQL.AppendLine("         Proveedor.FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(Proveedor.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         Proveedor.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".Proveedor");
            SQL.AppendLine("             INNER JOIN dbo.Gv_TablaRetencion_B1 ON " + DbSchema + ".Proveedor.CodigoRetencionUsual = dbo.Gv_TablaRetencion_B1.codigo");
            SQL.AppendLine("             INNER JOIN Saw.Gv_TipoProveedor_B1 ON " + DbSchema + ".Proveedor.TipodeProveedor = Saw.Gv_TipoProveedor_B1.nombre");
            SQL.AppendLine("             INNER JOIN dbo.Gv_Cuenta_B1 ON " + DbSchema + ".Proveedor.CuentaContableAnticipo = dbo.Gv_Cuenta_B1.codigo");
            SQL.AppendLine("      WHERE Proveedor.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND Proveedor.CodigoProveedor = @CodigoProveedor");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CodigoProveedor,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.NombreProveedor,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.NumeroRIF,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CodigoRetencionUsual,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Contacto,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Telefonos");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Proveedor_B1");
            SQL.AppendLine("      INNER JOIN dbo.TablaRetencion ON  " + DbSchema + ".Gv_Proveedor_B1.CodigoRetencionUsual = dbo.TablaRetencion.codigo AND " + DbSchema + ".Gv_Proveedor_B1.TipoDePersona = dbo.TablaRetencion.TipoDePersona");
            SQL.AppendLine("      INNER JOIN Saw.Gv_TipoProveedor_B1 ON  " + DbSchema + ".Gv_Proveedor_B1.TipodeProveedor = Saw.Gv_TipoProveedor_B1.nombre");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Proveedor_B1.ConsecutivoCompania = Saw.Gv_TipoProveedor_B1.ConsecutivoCompania");
            //SQL.AppendLine("      INNER JOIN dbo.Gv_Cuenta_B1 ON  " + DbSchema + ".Gv_Proveedor_B1.CuentaContableAnticipo = dbo.Gv_Cuenta_B1.codigo");
            //SQL.AppendLine("      AND " + DbSchema + ".Gv_Proveedor_B1.ConsecutivoCompania = dbo.Gv_Cuenta_B1.ConsecutivoPeriodo");
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
            SQL.AppendLine("      " + DbSchema + ".Proveedor.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Proveedor.CodigoProveedor,");
            SQL.AppendLine("      " + DbSchema + ".Proveedor.NombreProveedor,");
            SQL.AppendLine("      " + DbSchema + ".Proveedor.Contacto,");
            SQL.AppendLine("      " + DbSchema + ".Proveedor.CodigoRetencionUsual,");
            SQL.AppendLine("      " + DbSchema + ".Proveedor.Telefonos,");
            SQL.AppendLine("      " + DbSchema + ".Proveedor.TipodeProveedor,");
            SQL.AppendLine("      " + DbSchema + ".Proveedor.CuentaContableAnticipo");
            SQL.AppendLine("      FROM " + DbSchema + ".Proveedor");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".Proveedor", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipodePersonaRetencion", LibTpvCreator.SqlViewStandardEnum(typeof(eTipodePersonaRetencion), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDeProveedorDeLibrosFiscales", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDeProveedorDeLibrosFiscales), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDocumentoIdentificacion", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDocumentoIdentificacion), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_Proveedor_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ProveedorINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ProveedorUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ProveedorDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ProveedorGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ProveedorSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ProveedorGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".Proveedor", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_ProveedorINS");
            vResult = insSp.Drop(DbSchema + ".Gp_ProveedorUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ProveedorDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ProveedorGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ProveedorGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ProveedorSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_Proveedor_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipodePersonaRetencion") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDeProveedorDeLibrosFiscales") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDocumentoIdentificacion") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsProveedorED

} //End of namespace Galac.Dbo.Dal.CajaChica

