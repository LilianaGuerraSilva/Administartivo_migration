using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Ccl.DispositivosExternos;
using LibGalac.Aos.Base.Dal;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Dal.Venta {
    [LibMefDalComponentMetadata(typeof(clsCajaED))]
    public class clsCajaED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCajaED() : base() {
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
            get { return "Caja"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("Caja", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnCajConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnCajConsecutiv NOT NULL, ");
            SQL.AppendLine("NombreCaja" + InsSql.VarCharTypeForDb(60) + " CONSTRAINT nnCajNombreCaja NOT NULL, ");
            SQL.AppendLine("UsaGaveta" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCajUsaGaveta NOT NULL, ");
            SQL.AppendLine("Puerto" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_CajPu DEFAULT ('0'), ");
            SQL.AppendLine("Comando" + InsSql.VarCharTypeForDb(50) + " CONSTRAINT d_CajCo DEFAULT (''), ");
            SQL.AppendLine("PermitirAbrirSinSupervisor" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCajPermitirAb NOT NULL, ");
            SQL.AppendLine("UsaAccesoRapido" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCajUsaAccesoR NOT NULL, ");
            SQL.AppendLine("UsaMaquinaFiscal" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCajUsaMaquina NOT NULL, ");
            SQL.AppendLine("FamiliaImpresoraFiscal" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_CajFaImFi DEFAULT ('0'), ");
            SQL.AppendLine("ModeloDeMaquinaFiscal" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_CajMoDeMaFi DEFAULT ('0'), ");
            SQL.AppendLine("SerialDeMaquinaFiscal" + InsSql.VarCharTypeForDb(15) + " CONSTRAINT d_CajSeDeMaFi DEFAULT (''), ");
            SQL.AppendLine("TipoConexion" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_CajTiCo DEFAULT ('0'), ");
            SQL.AppendLine("PuertoMaquinaFiscal" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_CajPuMaFi DEFAULT ('0'), ");
            SQL.AppendLine("AbrirGavetaDeDinero" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCajAbrirGavet NOT NULL, ");
            SQL.AppendLine("UltimoNumeroCompFiscal" + InsSql.VarCharTypeForDb(12) + " CONSTRAINT d_CajUlNuCoFi DEFAULT (''), ");
            SQL.AppendLine("UltimoNumeroNCFiscal" + InsSql.VarCharTypeForDb(12) + " CONSTRAINT d_CajUlNuNC DEFAULT (''), ");
            SQL.AppendLine("UltimoNumeroNDFiscal" + InsSql.VarCharTypeForDb(12) + " CONSTRAINT d_CajUlNuND DEFAULT (''), ");
            SQL.AppendLine("IpParaConexion" + InsSql.VarCharTypeForDb(15) + " CONSTRAINT d_CajIpPaCo DEFAULT (''), ");
            SQL.AppendLine("MascaraSubred" + InsSql.VarCharTypeForDb(15) + " CONSTRAINT d_CajMaSu DEFAULT (''), ");
            SQL.AppendLine("Gateway" + InsSql.VarCharTypeForDb(15) + " CONSTRAINT d_CajGa DEFAULT (''), ");
            SQL.AppendLine("PermitirDescripcionDelArticuloExtendida" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCajPermitirDe NOT NULL, ");
            SQL.AppendLine("PermitirNombreDelClienteExtendido" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCajPermitirNo NOT NULL, ");
            SQL.AppendLine("UsarModoDotNet" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCajUsarModoDo NOT NULL, ");
            SQL.AppendLine("RegistroDeRetornoEnTxt" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_RegRetEnTxt DEFAULT ('N'), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_Caja PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Consecutivo ASC)");
            SQL.AppendLine(",CONSTRAINT u_CajConsecutivo UNIQUE NONCLUSTERED (ConsecutivoCompania, Consecutivo))");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT ConsecutivoCompania, Consecutivo, NombreCaja, UsaGaveta");
            SQL.AppendLine(", Puerto, " + DbSchema + ".Gv_EnumPuerto.StrValue AS PuertoStr, Comando, PermitirAbrirSinSupervisor, UsaAccesoRapido");
            SQL.AppendLine(", UsaMaquinaFiscal, FamiliaImpresoraFiscal, " + DbSchema + ".Gv_EnumFamiliaImpresoraFiscal.StrValue AS FamiliaImpresoraFiscalStr, ModeloDeMaquinaFiscal, " + DbSchema + ".Gv_EnumImpresoraFiscal.StrValue AS ModeloDeMaquinaFiscalStr, SerialDeMaquinaFiscal");
            SQL.AppendLine(", PuertoMaquinaFiscal, " + DbSchema + ".Gv_EnumPuerto.StrValue AS PuertoMaquinaFiscalStr, AbrirGavetaDeDinero, UltimoNumeroCompFiscal, UltimoNumeroNCFiscal, UltimoNumeroNDFiscal, TipoConexion, " + DbSchema + ".Gv_EnumTipoConexion.StrValue AS TipoConexionStr ");
            SQL.AppendLine(", IpParaConexion, MascaraSubred, Gateway, PermitirDescripcionDelArticuloExtendida");
            SQL.AppendLine(", PermitirNombreDelClienteExtendido, UsarModoDotNet, RegistroDeRetornoEnTxt, NombreOperador, FechaUltimaModificacion");
            SQL.AppendLine(", Caja.fldTimeStamp, CAST(Caja.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".Caja");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumPuerto");
            SQL.AppendLine("ON " + DbSchema + ".Caja.Puerto COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumPuerto.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumFamiliaImpresoraFiscal");
            SQL.AppendLine("ON " + DbSchema + ".Caja.FamiliaImpresoraFiscal COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumFamiliaImpresoraFiscal.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumImpresoraFiscal");
            SQL.AppendLine("ON " + DbSchema + ".Caja.ModeloDeMaquinaFiscal COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumImpresoraFiscal.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoConexion");
            SQL.AppendLine("ON " + DbSchema + ".Caja.TipoConexion COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoConexion.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumPuertoImpFiscal");
            SQL.AppendLine("ON " + DbSchema + ".Caja.PuertoMaquinaFiscal COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumPuertoImpFiscal.DbValue");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NombreCaja" + InsSql.VarCharTypeForDb(60) + " = '',");
            SQL.AppendLine("@UsaGaveta" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@Puerto" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Comando" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@PermitirAbrirSinSupervisor" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@UsaAccesoRapido" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@UsaMaquinaFiscal" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@FamiliaImpresoraFiscal" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@ModeloDeMaquinaFiscal" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@SerialDeMaquinaFiscal" + InsSql.VarCharTypeForDb(15) + " = '',");
            SQL.AppendLine("@TipoConexion" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@PuertoMaquinaFiscal" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@AbrirGavetaDeDinero" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@UltimoNumeroCompFiscal" + InsSql.VarCharTypeForDb(12) + " = '',");
            SQL.AppendLine("@UltimoNumeroNCFiscal" + InsSql.VarCharTypeForDb(12) + " = '',");
            SQL.AppendLine("@UltimoNumeroNDFiscal" + InsSql.VarCharTypeForDb(12) + " = '',");
            SQL.AppendLine("@IpParaConexion" + InsSql.VarCharTypeForDb(15) + " = '',");
            SQL.AppendLine("@MascaraSubred" + InsSql.VarCharTypeForDb(15) + " = '',");
            SQL.AppendLine("@Gateway" + InsSql.VarCharTypeForDb(15) + " = '',");
            SQL.AppendLine("@PermitirDescripcionDelArticuloExtendida" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@PermitirNombreDelClienteExtendido" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@UsarModoDotNet" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@RegistroDeRetornoEnTxt" + InsSql.CharTypeForDb(1) + " = 'N',");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".Caja(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            NombreCaja,");
            SQL.AppendLine("            UsaGaveta,");
            SQL.AppendLine("            Puerto,");
            SQL.AppendLine("            Comando,");
            SQL.AppendLine("            PermitirAbrirSinSupervisor,");
            SQL.AppendLine("            UsaAccesoRapido,");
            SQL.AppendLine("            UsaMaquinaFiscal,");
            SQL.AppendLine("            FamiliaImpresoraFiscal,");
            SQL.AppendLine("            ModeloDeMaquinaFiscal,");
            SQL.AppendLine("            SerialDeMaquinaFiscal,");
            SQL.AppendLine("            TipoConexion,");
            SQL.AppendLine("            PuertoMaquinaFiscal,");
            SQL.AppendLine("            AbrirGavetaDeDinero,");
            SQL.AppendLine("            UltimoNumeroCompFiscal,");
            SQL.AppendLine("            UltimoNumeroNCFiscal,");
            SQL.AppendLine("            UltimoNumeroNDFiscal,");
            SQL.AppendLine("            IpParaConexion,");
            SQL.AppendLine("            MascaraSubred,");
            SQL.AppendLine("            Gateway,");
            SQL.AppendLine("            PermitirDescripcionDelArticuloExtendida,");
            SQL.AppendLine("            PermitirNombreDelClienteExtendido,");
            SQL.AppendLine("            UsarModoDotNet,");
            SQL.AppendLine("            RegistroDeRetornoEnTxt,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @NombreCaja,");
            SQL.AppendLine("            @UsaGaveta,");
            SQL.AppendLine("            @Puerto,");
            SQL.AppendLine("            @Comando,");
            SQL.AppendLine("            @PermitirAbrirSinSupervisor,");
            SQL.AppendLine("            @UsaAccesoRapido,");
            SQL.AppendLine("            @UsaMaquinaFiscal,");
            SQL.AppendLine("            @FamiliaImpresoraFiscal,");
            SQL.AppendLine("            @ModeloDeMaquinaFiscal,");
            SQL.AppendLine("            @SerialDeMaquinaFiscal,");
            SQL.AppendLine("            @TipoConexion,");
            SQL.AppendLine("            @PuertoMaquinaFiscal,");
            SQL.AppendLine("            @AbrirGavetaDeDinero,");
            SQL.AppendLine("            @UltimoNumeroCompFiscal,");
            SQL.AppendLine("            @UltimoNumeroNCFiscal,");
            SQL.AppendLine("            @UltimoNumeroNDFiscal,");
            SQL.AppendLine("            @IpParaConexion,");
            SQL.AppendLine("            @MascaraSubred,");
            SQL.AppendLine("            @Gateway,");
            SQL.AppendLine("            @PermitirDescripcionDelArticuloExtendida,");
            SQL.AppendLine("            @PermitirNombreDelClienteExtendido,");
            SQL.AppendLine("            @UsarModoDotNet,");
            SQL.AppendLine("            @RegistroDeRetornoEnTxt,");
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
            SQL.AppendLine("@NombreCaja" + InsSql.VarCharTypeForDb(60) + ",");
            SQL.AppendLine("@UsaGaveta" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Puerto" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Comando" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@PermitirAbrirSinSupervisor" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@UsaAccesoRapido" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@UsaMaquinaFiscal" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@FamiliaImpresoraFiscal" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ModeloDeMaquinaFiscal" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@SerialDeMaquinaFiscal" + InsSql.VarCharTypeForDb(15) + ",");
            SQL.AppendLine("@TipoConexion" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@PuertoMaquinaFiscal" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@AbrirGavetaDeDinero" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@UltimoNumeroCompFiscal" + InsSql.VarCharTypeForDb(12) + ",");
            SQL.AppendLine("@UltimoNumeroNCFiscal" + InsSql.VarCharTypeForDb(12) + ",");
            SQL.AppendLine("@UltimoNumeroNDFiscal" + InsSql.VarCharTypeForDb(12) + ",");
            SQL.AppendLine("@IpParaConexion" + InsSql.VarCharTypeForDb(15) + ",");
            SQL.AppendLine("@MascaraSubred" + InsSql.VarCharTypeForDb(15) + ",");
            SQL.AppendLine("@Gateway" + InsSql.VarCharTypeForDb(15) + ",");
            SQL.AppendLine("@PermitirDescripcionDelArticuloExtendida" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@PermitirNombreDelClienteExtendido" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@UsarModoDotNet" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@RegistroDeRetornoEnTxt" + InsSql.CharTypeForDb(1) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Caja WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Caja WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_CajaCanBeUpdated @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".Caja");
            SQL.AppendLine("            SET NombreCaja = @NombreCaja,");
            SQL.AppendLine("               UsaGaveta = @UsaGaveta,");
            SQL.AppendLine("               Puerto = @Puerto,");
            SQL.AppendLine("               Comando = @Comando,");
            SQL.AppendLine("               PermitirAbrirSinSupervisor = @PermitirAbrirSinSupervisor,");
            SQL.AppendLine("               UsaAccesoRapido = @UsaAccesoRapido,");
            SQL.AppendLine("               UsaMaquinaFiscal = @UsaMaquinaFiscal,");
            SQL.AppendLine("               FamiliaImpresoraFiscal = @FamiliaImpresoraFiscal,");
            SQL.AppendLine("               ModeloDeMaquinaFiscal = @ModeloDeMaquinaFiscal,");
            SQL.AppendLine("               SerialDeMaquinaFiscal = @SerialDeMaquinaFiscal,");
            SQL.AppendLine("               TipoConexion = @TipoConexion,");
            SQL.AppendLine("               PuertoMaquinaFiscal = @PuertoMaquinaFiscal,");
            SQL.AppendLine("               AbrirGavetaDeDinero = @AbrirGavetaDeDinero,");
            SQL.AppendLine("               UltimoNumeroCompFiscal = @UltimoNumeroCompFiscal,");
            SQL.AppendLine("               UltimoNumeroNCFiscal = @UltimoNumeroNCFiscal,");
            SQL.AppendLine("               UltimoNumeroNDFiscal = @UltimoNumeroNDFiscal,");
            SQL.AppendLine("               IpParaConexion = @IpParaConexion,");
            SQL.AppendLine("               MascaraSubred = @MascaraSubred,");
            SQL.AppendLine("               Gateway = @Gateway,");
            SQL.AppendLine("               PermitirDescripcionDelArticuloExtendida = @PermitirDescripcionDelArticuloExtendida,");
            SQL.AppendLine("               PermitirNombreDelClienteExtendido = @PermitirNombreDelClienteExtendido,");
            SQL.AppendLine("               UsarModoDotNet = @UsarModoDotNet,");
            SQL.AppendLine("               RegistroDeRetornoEnTxt = @RegistroDeRetornoEnTxt,");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Caja WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Caja WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @Consecutivo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_CajaCanBeDeleted @ConsecutivoCompania,@Consecutivo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".Caja");
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
            SQL.AppendLine("         ConsecutivoCompania,");
            SQL.AppendLine("         Consecutivo,");
            SQL.AppendLine("         NombreCaja,");
            SQL.AppendLine("         UsaGaveta,");
            SQL.AppendLine("         Puerto,");
            SQL.AppendLine("         Comando,");
            SQL.AppendLine("         PermitirAbrirSinSupervisor,");
            SQL.AppendLine("         UsaAccesoRapido,");
            SQL.AppendLine("         UsaMaquinaFiscal,");
            SQL.AppendLine("         FamiliaImpresoraFiscal,");
            SQL.AppendLine("         ModeloDeMaquinaFiscal,");
            SQL.AppendLine("         SerialDeMaquinaFiscal,");
            SQL.AppendLine("         TipoConexion,");
            SQL.AppendLine("         PuertoMaquinaFiscal,");
            SQL.AppendLine("         AbrirGavetaDeDinero,");
            SQL.AppendLine("         UltimoNumeroCompFiscal,");
            SQL.AppendLine("         UltimoNumeroNCFiscal,");
            SQL.AppendLine("         UltimoNumeroNDFiscal,");
            SQL.AppendLine("         IpParaConexion,");
            SQL.AppendLine("         MascaraSubred,");
            SQL.AppendLine("         Gateway,");
            SQL.AppendLine("         PermitirDescripcionDelArticuloExtendida,");
            SQL.AppendLine("         PermitirNombreDelClienteExtendido,");
            SQL.AppendLine("         UsarModoDotNet,");
            SQL.AppendLine("         RegistroDeRetornoEnTxt,");
            SQL.AppendLine("         NombreOperador,");
            SQL.AppendLine("         FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".Caja");
            SQL.AppendLine("      WHERE Caja.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND Caja.Consecutivo = @Consecutivo");
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
            //SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      ConsecutivoCompania,");
            SQL.AppendLine("      Consecutivo,");
            SQL.AppendLine("      NombreCaja,");
            SQL.AppendLine("      ModeloDeMaquinaFiscal,");
            SQL.AppendLine("      UsaMaquinaFiscal,");
            SQL.AppendLine("      UsaGaveta,");
            SQL.AppendLine("      Puerto,");
            SQL.AppendLine("      Comando,");
            SQL.AppendLine("      PermitirAbrirSinSupervisor,");
            SQL.AppendLine("      UsaAccesoRapido,");
            SQL.AppendLine("      UsaMaquinaFiscal,");
            SQL.AppendLine("      TipoConexion,");
            SQL.AppendLine("      FamiliaImpresoraFiscal,");
            SQL.AppendLine("      ModeloDeMaquinaFiscal,");
            SQL.AppendLine("      SerialDeMaquinaFiscal,");
            SQL.AppendLine("      PuertoMaquinaFiscal,");
            SQL.AppendLine("      AbrirGavetaDeDinero,");
            SQL.AppendLine("      UltimoNumeroCompFiscal,");
            SQL.AppendLine("      UltimoNumeroNCFiscal,");
            SQL.AppendLine("      UltimoNumeroNDFiscal,");
            SQL.AppendLine("      IpParaConexion,");
            SQL.AppendLine("      MascaraSubred,");
            SQL.AppendLine("      Gateway,");
            SQL.AppendLine("      PermitirDescripcionDelArticuloExtendida,");
            SQL.AppendLine("      PermitirNombreDelClienteExtendido,");
            SQL.AppendLine("      UsarModoDotNet,");
            SQL.AppendLine("      RegistroDeRetornoEnTxt,");
            SQL.AppendLine("      NombreOperador,");
            SQL.AppendLine("      FechaUltimaModificacion ");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Caja_B1");
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
            SQL.AppendLine("      " + DbSchema + ".Caja.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Caja.Consecutivo");
            SQL.AppendLine("      FROM " + DbSchema + ".Caja");
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

        private string SqlSpActualizaUltimoNumComprobanteParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Numero" + InsSql.VarCharTypeForDb(12) + ",");
            SQL.AppendLine("@TipoDocumento" + InsSql.VarCharTypeForDb(1));
            return SQL.ToString();
        }

        private string SqlSpActualizaUltimoNumComprobante() {
            StringBuilder SQL = new StringBuilder();
            QAdvSql insSqlUtil = new QAdvSql("");
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("UPDATE Adm.Caja");
            SQL.AppendLine($"SET  UltimoNumeroCompFiscal = CASE WHEN @TipoDocumento ={insSqlUtil.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal)} THEN @Numero ELSE UltimoNumeroCompFiscal END,");
            SQL.AppendLine($"     UltimoNumeroNCFiscal   = CASE WHEN @TipoDocumento ={insSqlUtil.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal)} THEN @Numero ELSE UltimoNumeroNCFiscal END,");
            SQL.AppendLine($"     UltimoNumeroNDFiscal   = CASE WHEN @TipoDocumento ={insSqlUtil.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeDebitoComprobanteFiscal)} THEN @Numero ELSE UltimoNumeroNDFiscal END");
            SQL.AppendLine("	  WHERE Consecutivo = @Consecutivo AND");
            SQL.AppendLine("	  ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine(" SET @ReturnValue =  @@ROWCOUNT ");
            SQL.AppendLine(" RETURN @ReturnValue ");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".Caja", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas() {
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumPuerto", LibTpvCreator.SqlViewStandardEnum(typeof(ePuerto), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumFamiliaImpresoraFiscal", LibTpvCreator.SqlViewStandardEnum(typeof(eFamiliaImpresoraFiscal), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumImpresoraFiscal", LibTpvCreator.SqlViewStandardEnum(typeof(eImpresoraFiscal), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoConexion", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoConexion), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumPuertoImpFiscal", LibTpvCreator.SqlViewStandardEnum(typeof(ePuerto), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_Caja_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }

        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CajaINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CajaUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CajaDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CajaGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CajaSCH", SqlSpSearchParameters(), SqlSpSearch(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_CajaGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ActualizaUltimoNumComprobante", SqlSpActualizaUltimoNumComprobanteParameters(), SqlSpActualizaUltimoNumComprobante(), true) && vResult;
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
            if (insDbo.Exists(DbSchema + ".Caja", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_CajaINS");
            vResult = insSp.Drop(DbSchema + ".Gp_CajaUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CajaDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CajaGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CajaGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_CajaSCH") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_Caja_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumPuerto") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumFamiliaImpresoraFiscal") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumImpresoraFiscal") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoConexion") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumPuertoImpFiscal") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gp_ActualizaUltimoNumComprobante") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados
    } //End of class clsCajaED
} //End of namespace Galac.Adm.Dal.Venta

