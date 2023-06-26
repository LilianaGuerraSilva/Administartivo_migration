using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.Cliente;

namespace Galac.Saw.Dal.Cliente {
    public class clsClienteED:LibED {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsClienteED() : base() {
            DbSchema = "dbo";
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("Cliente",DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + " CONSTRAINT nnCliConsecutiv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10,0) + " CONSTRAINT nnCliConsecutiv NOT NULL, ");
            SQL.AppendLine("Codigo" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT nnCliCodigo NOT NULL, ");
            SQL.AppendLine("Nombre" + InsSql.VarCharTypeForDb(80) + " CONSTRAINT nnCliNombre NOT NULL, ");
            SQL.AppendLine("NumeroRIF" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_CliNuRI DEFAULT (''), ");
            SQL.AppendLine("NumeroNIT" + InsSql.VarCharTypeForDb(12) + " CONSTRAINT d_CliNuNI DEFAULT (''), ");
            SQL.AppendLine("Direccion" + InsSql.VarCharTypeForDb(255) + " CONSTRAINT d_CliDi DEFAULT (''), ");
            SQL.AppendLine("Ciudad" + InsSql.VarCharTypeForDb(100) + " CONSTRAINT d_CliCi DEFAULT (''), ");
            SQL.AppendLine("ZonaPostal" + InsSql.VarCharTypeForDb(7) + " CONSTRAINT d_CliZoPo DEFAULT (''), ");
            SQL.AppendLine("Telefono" + InsSql.VarCharTypeForDb(40) + " CONSTRAINT d_CliTe DEFAULT (''), ");
            SQL.AppendLine("FAX" + InsSql.VarCharTypeForDb(25) + " CONSTRAINT d_CliFAX DEFAULT (''), ");
            SQL.AppendLine("Status" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_CliSt DEFAULT ('0'), ");
            SQL.AppendLine("Contacto" + InsSql.VarCharTypeForDb(35) + " CONSTRAINT d_CliCo DEFAULT (''), ");
            SQL.AppendLine("ZonaDeCobranza" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_CliZoDeCo DEFAULT (''), ");
            SQL.AppendLine("CodigoVendedor" + InsSql.VarCharTypeForDb(5) + " CONSTRAINT d_CliCoVe DEFAULT (''), ");
            SQL.AppendLine("ConsecutivoVendedor" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnClienteConVen NOT NULL, ");
            SQL.AppendLine("RazonInactividad" + InsSql.VarCharTypeForDb(35) + " CONSTRAINT d_CliRaIn DEFAULT (''), ");
            SQL.AppendLine("Email" + InsSql.VarCharTypeForDb(100) + " CONSTRAINT d_CliEm DEFAULT (''), ");
            SQL.AppendLine("ActivarAvisoAlEscoger" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCliActivarAvi NOT NULL, ");
            SQL.AppendLine("TextoDelAviso" + InsSql.VarCharTypeForDb(150) + " CONSTRAINT d_CliTeDeAv DEFAULT (''), ");
            SQL.AppendLine("CuentaContableCxC" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_CliCuCoCxC DEFAULT (''), ");
            SQL.AppendLine("CuentaContableIngresos" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_CliCuCoIn DEFAULT (''), ");
            SQL.AppendLine("CuentaContableAnticipo" + InsSql.VarCharTypeForDb(30) + " CONSTRAINT d_CliCuCoAn DEFAULT (''), ");
            SQL.AppendLine("InfoGalac" + InsSql.VarCharTypeForDb(1) + " CONSTRAINT d_CliInGa DEFAULT (''), ");
            SQL.AppendLine("SectorDeNegocio" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_CliSeDeNe DEFAULT (''), ");
            SQL.AppendLine("CodigoLote" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT d_CliCoLo DEFAULT (''), ");
            SQL.AppendLine("NivelDePrecio" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_CliNiDePr DEFAULT ('0'), ");
            SQL.AppendLine("Origen" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_CliOr DEFAULT ('0'), ");
            SQL.AppendLine("DiaCumpleanos" + InsSql.NumericTypeForDb(10,0) + " CONSTRAINT d_CliDiCu DEFAULT (0), ");
            SQL.AppendLine("MesCumpleanos" + InsSql.NumericTypeForDb(10,0) + " CONSTRAINT d_CliMeCu DEFAULT (0), ");
            SQL.AppendLine("CorrespondenciaXEnviar" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCliCorrespond NOT NULL, ");
            SQL.AppendLine("EsExtranjero" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnCliEsExtranje NOT NULL, ");
            SQL.AppendLine("ClienteDesdeFecha" + InsSql.DateTypeForDb() + " CONSTRAINT d_CliClDeFe DEFAULT (''), ");
            SQL.AppendLine("AQueSeDedicaElCliente" + InsSql.VarCharTypeForDb(100) + " CONSTRAINT d_CliAQuSeDeElCl DEFAULT (''), ");
            SQL.AppendLine("TipoDocumentoIdentificacion" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_CliTiDoId DEFAULT ('0'), ");
            SQL.AppendLine("TipoDeContribuyente" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_CliTiDeCo DEFAULT ('0'), ");
            SQL.AppendLine("CampoDefinible1" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT d_CliCampo DEFAULT (''), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_Cliente PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, Codigo ASC)");
            SQL.AppendLine(", CONSTRAINT fk_ClienteCiudad FOREIGN KEY (Ciudad)");
            SQL.AppendLine("REFERENCES Comun.Ciudad(NombreCiudad)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_ClienteZonaCobranza FOREIGN KEY (ConsecutivoCompania, ZonaDeCobranza)");
            SQL.AppendLine("REFERENCES Saw.ZonaCobranza(ConsecutivoCompania, Nombre)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_ClienteVendedor FOREIGN KEY (ConsecutivoCompania, ConsecutivoVendedor)");
            SQL.AppendLine("REFERENCES Adm.Vendedor(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON UPDATE NO ACTION");
            SQL.AppendLine(", CONSTRAINT fk_ClienteCuenta FOREIGN KEY (ConsecutivoCompania, CuentaContableCxC)");
            SQL.AppendLine("REFERENCES dbo.Cuenta(ConsecutivoCompania, codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_ClienteCuenta FOREIGN KEY (ConsecutivoCompania, CuentaContableIngresos)");
            SQL.AppendLine("REFERENCES dbo.Cuenta(ConsecutivoCompania, codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_ClienteCuenta FOREIGN KEY (ConsecutivoCompania, CuentaContableAnticipo)");
            SQL.AppendLine("REFERENCES dbo.Cuenta(ConsecutivoCompania, codigo)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(", CONSTRAINT fk_ClienteSectorDeNegocio FOREIGN KEY (SectorDeNegocio)");
            SQL.AppendLine("REFERENCES dbo.SectorDeNegocio(Descripcion)");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(",CONSTRAINT u_CliNombre UNIQUE NONCLUSTERED (ConsecutivoCompania, Nombre)");
            SQL.AppendLine(",CONSTRAINT u_CliNumeroRIF UNIQUE NONCLUSTERED (ConsecutivoCompania, NumeroRIF)");
            SQL.AppendLine(",CONSTRAINT u_CliNumeroNIT UNIQUE NONCLUSTERED (ConsecutivoCompania, NumeroNIT)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT Cliente.ConsecutivoCompania, Cliente.Consecutivo, Cliente.Codigo, Cliente.Nombre, Cliente.NumeroRIF");
            SQL.AppendLine(", Cliente.NumeroNIT, Cliente.Direccion, Cliente.Ciudad, Cliente.ZonaPostal");
            SQL.AppendLine(", Cliente.Telefono, Cliente.FAX, Cliente.Status, " + DbSchema + ".Gv_EnumStatusCliente.StrValue AS StatusStr, Cliente.Contacto");
            SQL.AppendLine(", Cliente.ZonaDeCobranza, Cliente.CodigoVendedor, Cliente.ConsecutivoVendedor, Cliente.RazonInactividad, Cliente.Email");
            SQL.AppendLine(", Cliente.ActivarAvisoAlEscoger, Cliente.TextoDelAviso, Cliente.CuentaContableCxC, Cliente.CuentaContableIngresos");
            SQL.AppendLine(", Cliente.CuentaContableAnticipo, Cliente.InfoGalac, Cliente.SectorDeNegocio, Cliente.CodigoLote, Cliente.NivelDePrecio, " + DbSchema + ".Gv_EnumNivelDePrecio.StrValue AS NivelDePrecioStr");
            SQL.AppendLine(", Cliente.Origen, " + DbSchema + ".Gv_EnumOrigenFacturacionOManual.StrValue AS OrigenStr, Cliente.DiaCumpleanos, Cliente.MesCumpleanos, Cliente.CorrespondenciaXEnviar");
            SQL.AppendLine(", Cliente.EsExtranjero, Cliente.ClienteDesdeFecha, Cliente.NombreOperador, Cliente.AQueSeDedicaElCliente, Cliente.TipoDocumentoIdentificacion, " + DbSchema + ".Gv_EnumTipoDocumentoIdentificacion.StrValue AS TipoDocumentoIdentificacionStr, Cliente.TipoDeContribuyente, " + DbSchema + ".Gv_EnumTipoDeContribuyente.StrValue AS TipoDeContribuyenteStr");
            SQL.AppendLine(", Cliente.FechaUltimaModificacion");
            SQL.AppendLine(", Cliente.fldTimeStamp, CAST(Cliente.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + "dbo" + ".Cliente");
            SQL.AppendLine("LEFT OUTER JOIN " + DbSchema + ".Gv_EnumStatusCliente");
            SQL.AppendLine("ON " + "dbo" + ".Cliente.Status COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumStatusCliente.DbValue");
            SQL.AppendLine("LEFT OUTER JOIN " + DbSchema + ".Gv_EnumNivelDePrecio");
            SQL.AppendLine("ON " + "dbo" + ".Cliente.NivelDePrecio COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumNivelDePrecio.DbValue");
            SQL.AppendLine("LEFT OUTER JOIN " + DbSchema + ".Gv_EnumOrigenFacturacionOManual");
            SQL.AppendLine("ON " + "dbo" + ".Cliente.Origen COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumOrigenFacturacionOManual.DbValue");
            SQL.AppendLine("LEFT OUTER JOIN " + DbSchema + ".Gv_EnumTipoDocumentoIdentificacion");
            SQL.AppendLine("ON " + "dbo" + ".Cliente.TipoDocumentoIdentificacion COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDocumentoIdentificacion.DbValue");
            SQL.AppendLine("LEFT JOIN " + DbSchema + ".Gv_EnumTipoDeContribuyente");
            SQL.AppendLine("ON " + "dbo" + ".Cliente.TipoDeContribuyente COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDeContribuyente.DbValue");
            SQL.AppendLine(" LEFT OUTER JOIN Comun.SectorDeNegocio ");
            SQL.AppendLine(" ON (Cliente.ZonaDeCobranza = comun.SectorDeNegocio.Descripcion) ");
            SQL.AppendLine("WHERE " + "dbo" + ".Cliente.Codigo <> 'RD_Cliente'");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10,0) + " = 0,");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(80) + " = '',");
            SQL.AppendLine("@NumeroRIF" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@NumeroNIT" + InsSql.VarCharTypeForDb(12) + " = '',");
            SQL.AppendLine("@Direccion" + InsSql.VarCharTypeForDb(255) + " = '',");
            SQL.AppendLine("@Ciudad" + InsSql.VarCharTypeForDb(100) + ",");
            SQL.AppendLine("@ZonaPostal" + InsSql.VarCharTypeForDb(7) + " = '',");
            SQL.AppendLine("@Telefono" + InsSql.VarCharTypeForDb(40) + " = '',");
            SQL.AppendLine("@FAX" + InsSql.VarCharTypeForDb(25) + " = '',");
            SQL.AppendLine("@Status" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Contacto" + InsSql.VarCharTypeForDb(35) + " = '',");
            SQL.AppendLine("@ZonaDeCobranza" + InsSql.VarCharTypeForDb(100) + ",");
            SQL.AppendLine("@CodigoVendedor" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@ConsecutivoVendedor" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@RazonInactividad" + InsSql.VarCharTypeForDb(35) + " = '',");
            SQL.AppendLine("@Email" + InsSql.VarCharTypeForDb(100) + " = '',");
            SQL.AppendLine("@ActivarAvisoAlEscoger" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@TextoDelAviso" + InsSql.VarCharTypeForDb(150) + " = '',");
            SQL.AppendLine("@CuentaContableCxC" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@CuentaContableIngresos" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@CuentaContableAnticipo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@InfoGalac" + InsSql.VarCharTypeForDb(1) + " = '',");
            SQL.AppendLine("@SectorDeNegocio" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@CodigoLote" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@NivelDePrecio" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Origen" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@DiaCumpleanos" + InsSql.NumericTypeForDb(10,0) + " = 0,");
            SQL.AppendLine("@MesCumpleanos" + InsSql.NumericTypeForDb(10,0) + " = 0,");
            SQL.AppendLine("@CorrespondenciaXEnviar" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@EsExtranjero" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@ClienteDesdeFecha" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@AQueSeDedicaElCliente" + InsSql.VarCharTypeForDb(100) + " = '',");
            SQL.AppendLine("@TipoDocumentoIdentificacion" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@TipoDeContribuyente" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@CampoDefinible1" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + " = '01/01/1900'");
            return SQL.ToString();
        }

        private string SqlSpIns() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SET DATEFORMAT @DateFormat");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10,0) + "");
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".Cliente(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            Consecutivo,");
            SQL.AppendLine("            Codigo,");
            SQL.AppendLine("            Nombre,");
            SQL.AppendLine("            NumeroRIF,");
            SQL.AppendLine("            NumeroNIT,");
            SQL.AppendLine("            Direccion,");
            SQL.AppendLine("            Ciudad,");
            SQL.AppendLine("            ZonaPostal,");
            SQL.AppendLine("            Telefono,");
            SQL.AppendLine("            FAX,");
            SQL.AppendLine("            Status,");
            SQL.AppendLine("            Contacto,");
            SQL.AppendLine("            ZonaDeCobranza,");
            SQL.AppendLine("            CodigoVendedor,");
            SQL.AppendLine("            ConsecutivoVendedor,");
            SQL.AppendLine("            RazonInactividad,");
            SQL.AppendLine("            Email,");
            SQL.AppendLine("            ActivarAvisoAlEscoger,");
            SQL.AppendLine("            TextoDelAviso,");
            SQL.AppendLine("            CuentaContableCxC,");
            SQL.AppendLine("            CuentaContableIngresos,");
            SQL.AppendLine("            CuentaContableAnticipo,");
            SQL.AppendLine("            InfoGalac,");
            SQL.AppendLine("            SectorDeNegocio,");
            SQL.AppendLine("            CodigoLote,");
            SQL.AppendLine("            NivelDePrecio,");
            SQL.AppendLine("            Origen,");
            SQL.AppendLine("            DiaCumpleanos,");
            SQL.AppendLine("            MesCumpleanos,");
            SQL.AppendLine("            CorrespondenciaXEnviar,");
            SQL.AppendLine("            EsExtranjero,");
            SQL.AppendLine("            ClienteDesdeFecha,");
            SQL.AppendLine("            AQueSeDedicaElCliente,");
            SQL.AppendLine("            TipoDocumentoIdentificacion,");
            SQL.AppendLine("            TipoDeContribuyente,");
            SQL.AppendLine("            CampoDefinible1,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @Consecutivo,");
            SQL.AppendLine("            @Codigo,");
            SQL.AppendLine("            @Nombre,");
            SQL.AppendLine("            @NumeroRIF,");
            SQL.AppendLine("            @NumeroNIT,");
            SQL.AppendLine("            @Direccion,");
            SQL.AppendLine("            @Ciudad,");
            SQL.AppendLine("            @ZonaPostal,");
            SQL.AppendLine("            @Telefono,");
            SQL.AppendLine("            @FAX,");
            SQL.AppendLine("            @Status,");
            SQL.AppendLine("            @Contacto,");
            SQL.AppendLine("            @ZonaDeCobranza,");
            SQL.AppendLine("            @CodigoVendedor,");
            SQL.AppendLine("            @ConsecutivoVendedor,");
            SQL.AppendLine("            @RazonInactividad,");
            SQL.AppendLine("            @Email,");
            SQL.AppendLine("            @ActivarAvisoAlEscoger,");
            SQL.AppendLine("            @TextoDelAviso,");
            SQL.AppendLine("            @CuentaContableCxC,");
            SQL.AppendLine("            @CuentaContableIngresos,");
            SQL.AppendLine("            @CuentaContableAnticipo,");
            SQL.AppendLine("            @InfoGalac,");
            SQL.AppendLine("            @SectorDeNegocio,");
            SQL.AppendLine("            @CodigoLote,");
            SQL.AppendLine("            @NivelDePrecio,");
            SQL.AppendLine("            @Origen,");
            SQL.AppendLine("            @DiaCumpleanos,");
            SQL.AppendLine("            @MesCumpleanos,");
            SQL.AppendLine("            @CorrespondenciaXEnviar,");
            SQL.AppendLine("            @EsExtranjero,");
            SQL.AppendLine("            @ClienteDesdeFecha,");
            SQL.AppendLine("            @AQueSeDedicaElCliente,");
            SQL.AppendLine("            @TipoDocumentoIdentificacion,");
            SQL.AppendLine("            @TipoDeContribuyente,");
            SQL.AppendLine("            @CampoDefinible1,");
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
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(80) + ",");
            SQL.AppendLine("@NumeroRIF" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@NumeroNIT" + InsSql.VarCharTypeForDb(12) + ",");
            SQL.AppendLine("@Direccion" + InsSql.VarCharTypeForDb(255) + ",");
            SQL.AppendLine("@Ciudad" + InsSql.VarCharTypeForDb(100) + ",");
            SQL.AppendLine("@ZonaPostal" + InsSql.VarCharTypeForDb(7) + ",");
            SQL.AppendLine("@Telefono" + InsSql.VarCharTypeForDb(40) + ",");
            SQL.AppendLine("@FAX" + InsSql.VarCharTypeForDb(25) + ",");
            SQL.AppendLine("@Status" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Contacto" + InsSql.VarCharTypeForDb(35) + ",");
            SQL.AppendLine("@ZonaDeCobranza" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@CodigoVendedor" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@ConsecutivoVendedor" + InsSql.VarCharTypeForDb(5) + ",");
            SQL.AppendLine("@RazonInactividad" + InsSql.VarCharTypeForDb(35) + ",");
            SQL.AppendLine("@Email" + InsSql.VarCharTypeForDb(100) + ",");
            SQL.AppendLine("@ActivarAvisoAlEscoger" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@TextoDelAviso" + InsSql.VarCharTypeForDb(150) + ",");
            SQL.AppendLine("@CuentaContableCxC" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@CuentaContableIngresos" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@CuentaContableAnticipo" + InsSql.VarCharTypeForDb(30) + ",");
            SQL.AppendLine("@InfoGalac" + InsSql.VarCharTypeForDb(1) + ",");
            SQL.AppendLine("@SectorDeNegocio" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@CodigoLote" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@NivelDePrecio" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@Origen" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@DiaCumpleanos" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@MesCumpleanos" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@CorrespondenciaXEnviar" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@EsExtranjero" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ClienteDesdeFecha" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@AQueSeDedicaElCliente" + InsSql.VarCharTypeForDb(100) + ",");
            SQL.AppendLine("@TipoDocumentoIdentificacion" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@TipoDeContribuyente" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@CampoDefinible1" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + ",");
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
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10,0) + "");
            //SQL.AppendLine("--DECLARE @CanBeChanged bit");
            SQL.AppendLine("   SET @ReturnValue = -1");
            SQL.AppendLine("   SET @ValidationMsg = ''");
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Cliente WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @Codigo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Cliente WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @Codigo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_ClienteCanBeUpdated @ConsecutivoCompania,@Codigo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".Cliente");
            SQL.AppendLine("            SET Consecutivo = @Consecutivo,");
            SQL.AppendLine("               Nombre = @Nombre,");
            SQL.AppendLine("               NumeroRIF = @NumeroRIF,");
            SQL.AppendLine("               NumeroNIT = @NumeroNIT,");
            SQL.AppendLine("               Direccion = @Direccion,");
            SQL.AppendLine("               Ciudad = @Ciudad,");
            SQL.AppendLine("               ZonaPostal = @ZonaPostal,");
            SQL.AppendLine("               Telefono = @Telefono,");
            SQL.AppendLine("               FAX = @FAX,");
            SQL.AppendLine("               Status = @Status,");
            SQL.AppendLine("               Contacto = @Contacto,");
            SQL.AppendLine("               ZonaDeCobranza = @ZonaDeCobranza,");
            SQL.AppendLine("               CodigoVendedor = @CodigoVendedor,");
            SQL.AppendLine("               ConsecutivoVendedor = @ConsecutivoVendedor,");
            SQL.AppendLine("               RazonInactividad = @RazonInactividad,");
            SQL.AppendLine("               Email = @Email,");
            SQL.AppendLine("               ActivarAvisoAlEscoger = @ActivarAvisoAlEscoger,");
            SQL.AppendLine("               TextoDelAviso = @TextoDelAviso,");
            SQL.AppendLine("               CuentaContableCxC = @CuentaContableCxC,");
            SQL.AppendLine("               CuentaContableIngresos = @CuentaContableIngresos,");
            SQL.AppendLine("               CuentaContableAnticipo = @CuentaContableAnticipo,");
            SQL.AppendLine("               InfoGalac = @InfoGalac,");
            SQL.AppendLine("               SectorDeNegocio = @SectorDeNegocio,");
            SQL.AppendLine("               CodigoLote = @CodigoLote,");
            SQL.AppendLine("               NivelDePrecio = @NivelDePrecio,");
            SQL.AppendLine("               Origen = @Origen,");
            SQL.AppendLine("               DiaCumpleanos = @DiaCumpleanos,");
            SQL.AppendLine("               MesCumpleanos = @MesCumpleanos,");
            SQL.AppendLine("               CorrespondenciaXEnviar = @CorrespondenciaXEnviar,");
            SQL.AppendLine("               EsExtranjero = @EsExtranjero,");
            SQL.AppendLine("               ClienteDesdeFecha = @ClienteDesdeFecha,");
            SQL.AppendLine("               AQueSeDedicaElCliente = @AQueSeDedicaElCliente,");
            SQL.AppendLine("               TipoDocumentoIdentificacion = @TipoDocumentoIdentificacion,");
            SQL.AppendLine("               TipoDeContribuyente = @TipoDeContribuyente,");
            SQL.AppendLine("               CampoDefinible1 = @CampoDefinible1,");
            SQL.AppendLine("               NombreOperador = @NombreOperador,");
            SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND Codigo = @Codigo");
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
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@TimeStampAsInt" + InsSql.BigintTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpDel() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @CurrentTimeStamp timestamp");
            SQL.AppendLine("   DECLARE @ValidationMsg " + InsSql.VarCharTypeForDb(1500) + " --No puede ser más");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10,0) + "");
            //SQL.AppendLine("--DECLARE @CanBeDeleted bit");
            SQL.AppendLine("   SET @ReturnValue = -1");
            SQL.AppendLine("   SET @ValidationMsg = ''");
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".Cliente WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @Codigo)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Cliente WHERE ConsecutivoCompania = @ConsecutivoCompania AND Codigo = @Codigo");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_ClienteCanBeDeleted @ConsecutivoCompania,@Codigo, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".Cliente");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND Codigo = @Codigo");
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
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(10));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         Cliente.ConsecutivoCompania,");
            SQL.AppendLine("         Cliente.Consecutivo,");
            SQL.AppendLine("         Cliente.Codigo,");
            SQL.AppendLine("         Cliente.Nombre,");
            SQL.AppendLine("         Cliente.NumeroRIF,");
            SQL.AppendLine("         Cliente.NumeroNIT,");
            SQL.AppendLine("         Cliente.Direccion,");
            SQL.AppendLine("         Cliente.Ciudad,");
            SQL.AppendLine("         Cliente.ZonaPostal,");
            SQL.AppendLine("         Cliente.Telefono,");
            SQL.AppendLine("         Cliente.FAX,");
            SQL.AppendLine("         Cliente.Status,");
            SQL.AppendLine("         Cliente.Contacto,");
            SQL.AppendLine("         Cliente.ZonaDeCobranza,");
            SQL.AppendLine("         Cliente.CodigoVendedor,");
            SQL.AppendLine("         Cliente.RazonInactividad,");
            SQL.AppendLine("         Cliente.Email,");
            SQL.AppendLine("         Cliente.ActivarAvisoAlEscoger,");
            SQL.AppendLine("         Cliente.TextoDelAviso,");
            SQL.AppendLine("         Cliente.CuentaContableCxC,");
            SQL.AppendLine("         Cliente.CuentaContableIngresos,");
            SQL.AppendLine("         Cliente.CuentaContableAnticipo,");
            SQL.AppendLine("         Cliente.InfoGalac,");
            SQL.AppendLine("         Cliente.SectorDeNegocio,");
            SQL.AppendLine("         Cliente.CodigoLote,");
            SQL.AppendLine("         Cliente.NivelDePrecio,");
            SQL.AppendLine("         Cliente.Origen,");
            SQL.AppendLine("         Cliente.DiaCumpleanos,");
            SQL.AppendLine("         Cliente.MesCumpleanos,");
            SQL.AppendLine("         Cliente.CorrespondenciaXEnviar,");
            SQL.AppendLine("         Cliente.EsExtranjero,");
            SQL.AppendLine("         Cliente.ClienteDesdeFecha,");
            SQL.AppendLine("         Cliente.AQueSeDedicaElCliente,");
            SQL.AppendLine("         Cliente.TipoDocumentoIdentificacion,");
            SQL.AppendLine("         Cliente.TipoDeContribuyente,");
            SQL.AppendLine("         Cliente.CampoDefinible1,");
            SQL.AppendLine("         Cliente.NombreOperador,");
            SQL.AppendLine("         Cliente.FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(Cliente.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         Cliente.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".Cliente");
            SQL.AppendLine("      WHERE Cliente.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND Cliente.Codigo = @Codigo");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.Codigo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.Nombre,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.NumeroRIF,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.Telefono,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.TipoDocumentoIdentificacion,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.Ciudad,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.ZonaPostal,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.ZonaDeCobranza,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.CodigoVendedor,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.SectorDeNegocio,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.StatusStr,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.ActivarAvisoAlEscoger,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.RazonInactividad,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.TextoDelAviso,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.Direccion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.FAX,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.ClienteDesdeFecha,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Cliente_B1.TipoDeContribuyente");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Cliente_B1");
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
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10,0) + ",");
            SQL.AppendLine("@XmlDataDetail" + InsSql.XmlTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpGetFK() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @hdoc " + InsSql.NumericTypeForDb(10,0));
            SQL.AppendLine("   EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("      " + DbSchema + ".Cliente.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Cliente.Codigo,");
            SQL.AppendLine("      " + DbSchema + ".Cliente.Nombre,");
            SQL.AppendLine("      " + DbSchema + ".Cliente.NumeroRIF,");
            SQL.AppendLine("      " + DbSchema + ".Cliente.Ciudad,");
            SQL.AppendLine("      " + DbSchema + ".Cliente.ZonaPostal,");
            SQL.AppendLine("      " + DbSchema + ".Cliente.Telefono,");
            SQL.AppendLine("      " + DbSchema + ".Cliente.ZonaDeCobranza,");
            SQL.AppendLine("      " + DbSchema + ".Cliente.CodigoVendedor,");
            SQL.AppendLine("      " + DbSchema + ".Cliente.CuentaContableCxC,");
            SQL.AppendLine("      " + DbSchema + ".Cliente.CuentaContableIngresos,");
            SQL.AppendLine("      " + DbSchema + ".Cliente.CuentaContableAnticipo,");
            SQL.AppendLine("      " + DbSchema + ".Cliente.SectorDeNegocio");
            //SQL.AppendLine("      ," + DbSchema + ".Cliente.[Programador - personaliza este sp y coloca solo los campos que te interesa exponer a quienes lo consumen]");
            SQL.AppendLine("   FROM " + DbSchema + ".Cliente");
            SQL.AppendLine("      INNER JOIN OPENXML( @hdoc, 'GpData/GpResult',2)");
            SQL.AppendLine("      WITH (");
            SQL.AppendLine("      Codigo " + InsSql.VarCharTypeForDb(20) + ") AS XmlDocCliente");
            SQL.AppendLine("      ON dbo.cliente.Codigo = XmlDocCliente.Codigo");
            SQL.AppendLine("   WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries
        bool CrearTabla() {
            //bool vResult = insDbo.Create(DbSchema + ".Cliente", SqlCreateTable(), false, eDboType.Tabla);
            return true;
        }
        bool CrearVistas() {
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumStatusCliente",LibTpvCreator.SqlViewStandardEnum(typeof(eStatusCliente),InsSql),true,true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumNivelDePrecio",LibTpvCreator.SqlViewStandardEnum(typeof(eNivelDePrecio),InsSql),true,true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumOrigenFacturacionOManual",LibTpvCreator.SqlViewStandardEnum(typeof(eOrigenFacturacionOManual),InsSql),true,true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDocumentoIdentificacion",LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDocumentoIdentificacion),InsSql),true,true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDeContribuyente",LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDeContribuyente),InsSql),true,true);
            vResult = insVistas.Create(DbSchema + ".Gv_Cliente_B1",SqlViewB1(),true);
            insVistas.Dispose();
            return vResult;
        }
        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ClienteINS",SqlSpInsParameters(),SqlSpIns(),true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ClienteUPD",SqlSpUpdParameters(),SqlSpUpd(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ClienteDEL",SqlSpDelParameters(),SqlSpDel(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ClienteGET",SqlSpGetParameters(),SqlSpGet(),true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ClienteSCH",SqlSpSearchParameters(),SqlSpSearch(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ClienteGetFk",SqlSpGetFKParameters(),SqlSpGetFK(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ObtenerPaginaDeClientesPorNombre",SqlObtenerPaginaDeClientesParametros(),SqlObtenerPaginaDeClientesPorNombreSentencia(),true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ObtenerPaginaDeClientesPorRIF",SqlObtenerPaginaDeClientesParametros(),SqlObtenerPaginaDeClientesPorRIFSentencia(),true) && vResult;
            insSps.Dispose();
            return vResult;
        }
        public bool InstalarTabla() {
            bool vResult = false;
            if(CrearTabla()) {
                CrearVistas();
                CrearProcedimientos();
                vResult = true;
            }
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = false;
            if(insDbo.Exists("dbo" + ".Cliente",eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_ClienteINS");
            vResult = insSp.Drop(DbSchema + ".Gp_ClienteUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ClienteDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ClienteGET");
            vResult = insSp.Drop(DbSchema + ".Gp_ClienteGetFk") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ClienteSCH") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ObtenerPaginaDeClientesPorRIF") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_ObtenerPaginaDeClientesPorNombre") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_Cliente_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados

        private string SqlObtenerPaginaDeClientesPorNombreSentencia() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("	WITH [Nombres filtrados]");
            SQL.AppendLine("	AS");
            SQL.AppendLine("	(");
            SQL.AppendLine("		SELECT cl.NumeroRIF, cl.Nombre FROM cliente cl");
            SQL.AppendLine("		WHERE cl.ConsecutivoCompania=@consecutivoCompania");
            SQL.AppendLine("		AND cl.Nombre LIKE '%'+@filtro+'%'");
            SQL.AppendLine("		AND cl.Codigo NOT LIKE 'RD_%'");
            SQL.AppendLine("	),");
            SQL.AppendLine("	[Nombres Rankeados]");
            SQL.AppendLine("	AS");
            SQL.AppendLine("	(");
            SQL.AppendLine("		SELECT nf.NumeroRIF,nf.Nombre, ROW_NUMBER() OVER (ORDER BY(nf.Nombre)) AS Ranking");
            SQL.AppendLine("		FROM [Nombres Filtrados] nf");
            SQL.AppendLine("	)");
            SQL.AppendLine("	SELECT nr.NumeroRIF, nr.Nombre FROM [Nombres Rankeados] nr");
            SQL.AppendLine("	WHERE nr.Ranking BETWEEN (@pagina-1)*@articulosPorPagina+1 AND @pagina*@articulosPorPagina");
            return SQL.ToString();
        }

        private string SqlObtenerPaginaDeClientesPorRIFSentencia() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("	WITH [RIF filtrados]");
            SQL.AppendLine("	AS");
            SQL.AppendLine("	(");
            SQL.AppendLine("		SELECT cl.NumeroRIF, cl.Nombre FROM cliente cl");
            SQL.AppendLine("		WHERE cl.ConsecutivoCompania=@consecutivoCompania");
            SQL.AppendLine("		AND cl.NumeroRIF LIKE '%'+@filtro+'%'");
            SQL.AppendLine("		AND cl.Codigo NOT LIKE 'RD_%'");
            SQL.AppendLine("	),");
            SQL.AppendLine("	[RIF Rankeados]");
            SQL.AppendLine("	AS");
            SQL.AppendLine("	(");
            SQL.AppendLine("		SELECT nf.NumeroRIF, nf.Nombre, ROW_NUMBER() OVER (ORDER BY(nf.Nombre)) AS Ranking");
            SQL.AppendLine("		FROM [RIF Filtrados] nf");
            SQL.AppendLine("	)");
            SQL.AppendLine("	SELECT nr.NumeroRIF,nr.Nombre FROM [RIF Rankeados] nr");
            SQL.AppendLine("	WHERE nr.Ranking BETWEEN (@pagina-1)*@articulosPorPagina+1 AND @pagina*@articulosPorPagina");
            return SQL.ToString();
        }

        private string SqlObtenerPaginaDeClientesParametros() {
            StringBuilder SQL = new StringBuilder();
            SQL.Append("	@filtro" + InsSql.VarCharTypeForDb(50) + ", ");
            SQL.Append("    @consecutivoCompania " + InsSql.NumericTypeForDb(15,0) + ",");
            SQL.Append("    @pagina " + InsSql.NumericTypeForDb(15,0) + ",");
            SQL.Append("	@articulosPorPagina" + InsSql.NumericTypeForDb(15,0));
            return SQL.ToString();
        }

    } //End of class clsClienteED

} //End of namespace Galac.Saw.Dal.Clientes

