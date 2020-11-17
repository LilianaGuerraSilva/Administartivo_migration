using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.GestionCompras;
using System.IO;
using Galac.Comun.Ccl.TablasLey;
using LibGalac.Aos.Base;
namespace Galac.Adm.Dal.GestionCompras {
    [LibMefDalComponentMetadata(typeof(clsProveedorED))]
    public class clsProveedorED : LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsProveedorED(): base(){
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
            get { return "Proveedor"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("Proveedor", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnProConsecutiv NOT NULL, ");
            SQL.AppendLine("CodigoProveedor" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT nnProCodigoProv NOT NULL, ");
            SQL.AppendLine("Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnProConsecutiv NOT NULL, ");
            SQL.AppendLine("NombreProveedor" + InsSql.VarCharTypeForDb(160) + " CONSTRAINT nnProNombreProv NOT NULL, ");
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
            SQL.AppendLine("NumeroCuentaBancaria" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_ProNuCuBa DEFAULT (''), ");
            SQL.AppendLine("CodigoContribuyente" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_ProCoCo DEFAULT (''), ");
            SQL.AppendLine("NumeroRUC" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT d_ProNuRU DEFAULT (''), ");
            SQL.AppendLine("TipoDePersonaLibrosElectronicos" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_ProTiDePeLiEl DEFAULT ('1'), ");
            SQL.AppendLine("CodigoPaisResidencia" + InsSql.VarCharTypeForDb(4) + " CONSTRAINT d_ProCoPaRe DEFAULT (''), ");
            SQL.AppendLine("CodigoConveniosSunat" + InsSql.VarCharTypeForDb(2) + " CONSTRAINT d_ProCoCoSu DEFAULT (''), ");
            SQL.AppendLine("NombreOperador" + InsSql.VarCharTypeForDb(10) + ", ");
            SQL.AppendLine("FechaUltimaModificacion" + InsSql.DateTypeForDb() + ", ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_Proveedor PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, CodigoProveedor ASC)");            
            SQL.AppendLine(", CONSTRAINT fk_ProveedorTipoProveedor FOREIGN KEY (ConsecutivoCompania, TipodeProveedor)");
            SQL.AppendLine("REFERENCES Saw.TipoProveedor(ConsecutivoCompania, nombre)");
            SQL.AppendLine("ON UPDATE CASCADE");            
            SQL.AppendLine(",CONSTRAINT u_ProNumeroRIF UNIQUE NONCLUSTERED (ConsecutivoCompania, NumeroRIF)");
            //SQL.AppendLine(",CONSTRAINT u_ProBeneficiario UNIQUE NONCLUSTERED (ConsecutivoCompania, Beneficiario)");
            SQL.AppendLine(",CONSTRAINT u_ProConsecutivo UNIQUE NONCLUSTERED (ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT Proveedor.ConsecutivoCompania, Proveedor.CodigoProveedor, Proveedor.Consecutivo, Proveedor.NombreProveedor");
            SQL.AppendLine(", Proveedor.Contacto, Proveedor.NumeroRIF, Proveedor.NumeroNIT, Proveedor.TipoDePersona, (CASE WHEN " + DbSchema + ".Gv_EnumTipodePersonaRetencion.StrValue IS NULL THEN 'Jurídica Domiciliada' ELSE " + DbSchema + ".Gv_EnumTipodePersonaRetencion.StrValue END) AS TipoDePersonaStr");
            SQL.AppendLine(", Proveedor.CodigoRetencionUsual, Proveedor.Telefonos, Proveedor.Direccion, Proveedor.Fax");
            SQL.AppendLine(", Proveedor.Email, Proveedor.TipodeProveedor, (CASE WHEN Proveedor.TipoDeProveedorDeLibrosFiscales IS NULL THEN '0' ELSE Proveedor.TipoDeProveedorDeLibrosFiscales END) AS TipoDeProveedorDeLibrosFiscales, (CASE WHEN " + DbSchema + ".Gv_EnumTipoDeProveedorDeLibrosFiscales.StrValue IS NULL THEN 'Con RIF' ELSE " + DbSchema + ".Gv_EnumTipoDeProveedorDeLibrosFiscales.StrValue END) AS TipoDeProveedorDeLibrosFiscalesStr, (CASE WHEN Proveedor.PorcentajeRetencionIVA IS NULL THEN 0 ELSE Proveedor.PorcentajeRetencionIVA END) AS  PorcentajeRetencionIVA");
            SQL.AppendLine(", (CASE WHEN Proveedor.CuentaContableCxP IS NULL THEN '' ELSE Proveedor.CuentaContableCxP END) AS CuentaContableCxP, (CASE WHEN Proveedor.CuentaContableGastos IS NULL THEN '' ELSE Proveedor.CuentaContableGastos END) AS CuentaContableGastos, (CASE WHEN Proveedor.CuentaContableAnticipo IS NULL THEN '' ELSE Proveedor.CuentaContableAnticipo END) AS CuentaContableAnticipo, (CASE WHEN Proveedor.CodigoLote IS NULL THEN '' ELSE Proveedor.CodigoLote END) AS CodigoLote");
            SQL.AppendLine(", Proveedor.Beneficiario, Proveedor.UsarBeneficiarioImpCheq, (CASE WHEN Proveedor.TipoDocumentoIdentificacion IS NULL THEN '4' ELSE Proveedor.TipoDocumentoIdentificacion END) AS TipoDocumentoIdentificacion, (CASE WHEN " + DbSchema + ".Gv_EnumTipoDocumentoIdentificacion.StrValue IS NULL THEN 'Otros Tipos de Documentos' ELSE " + DbSchema + ".Gv_EnumTipoDocumentoIdentificacion.StrValue END) AS TipoDocumentoIdentificacionStr, Proveedor.EsAgenteDeRetencionIva");
            SQL.AppendLine(", Proveedor.Nombre, Proveedor.ApellidoPaterno, Proveedor.ApellidoMaterno, Proveedor.NumeroCuentaBancaria");
            SQL.AppendLine(", Proveedor.CodigoContribuyente, Proveedor.NumeroRUC, Proveedor.NombreOperador, Proveedor.FechaUltimaModificacion");
            SQL.AppendLine(", Proveedor.TipoDePersonaLibrosElectronicos, " + DbSchema + ".Gv_EnumTipoDePersonaLibrosElectronicos.StrValue AS TipoDePersonaLibrosElectronicosStr, Proveedor.CodigoPaisResidencia, Proveedor.CodigoConveniosSunat");
            SQL.AppendLine(", Proveedor.fldTimeStamp, CAST(Proveedor.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine(" FROM " + DbSchema + ".Proveedor");
            SQL.AppendLine(" LEFT OUTER JOIN " + DbSchema + ".Gv_EnumTipodePersonaRetencion");
            SQL.AppendLine(" ON " + DbSchema + ".Proveedor.TipoDePersona COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipodePersonaRetencion.DbValue");
            SQL.AppendLine(" LEFT OUTER JOIN " + DbSchema + ".Gv_EnumTipoDeProveedorDeLibrosFiscales");
            SQL.AppendLine(" ON " + DbSchema + ".Proveedor.TipoDeProveedorDeLibrosFiscales COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDeProveedorDeLibrosFiscales.DbValue");
            SQL.AppendLine(" LEFT OUTER JOIN " + DbSchema + ".Gv_EnumTipoDocumentoIdentificacion");
            SQL.AppendLine(" ON " + DbSchema + ".Proveedor.TipoDocumentoIdentificacion COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDocumentoIdentificacion.DbValue");
            SQL.AppendLine("INNER JOIN " + DbSchema + ".Gv_EnumTipoDePersonaLibrosElectronicos");
            SQL.AppendLine("ON " + DbSchema + ".Proveedor.TipoDePersonaLibrosElectronicos COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = " + DbSchema + ".Gv_EnumTipoDePersonaLibrosElectronicos.DbValue");
			SQL.AppendLine(" INNER JOIN Saw.TipoProveedor ON  " + DbSchema + ".Proveedor.TipodeProveedor = Saw.TipoProveedor.nombre");            
			SQL.AppendLine("      AND " + DbSchema + ".Proveedor.ConsecutivoCompania = Saw.TipoProveedor.ConsecutivoCompania");
            SQL.AppendLine(" LEFT OUTER JOIN Comun.Gv_TablaRetencionProveedor ON (" + DbSchema + ".Proveedor.CodigoRetencionUsual = Comun.Gv_TablaRetencionProveedor.Codigo ");
            SQL.AppendLine(" AND " + DbSchema + ".Proveedor.TipoDePersona = Comun.Gv_TablaRetencionProveedor.TipoDePersona)");                       
            //SQL.AppendLine("INNER JOIN Dbo.Cuenta ON  " + DbSchema + ".Proveedor.CuentaContableAnticipo = Dbo.Cuenta.codigo");
            //SQL.AppendLine("      AND " + DbSchema + ".Proveedor.ConsecutivoCompania = Dbo.Cuenta.ConsecutivoPeriodo");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoProveedor" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + " = 0,");
            SQL.AppendLine("@NombreProveedor" + InsSql.VarCharTypeForDb(160) + " = '',");
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
            SQL.AppendLine("@NumeroCuentaBancaria" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@CodigoContribuyente" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@NumeroRUC" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@TipoDePersonaLibrosElectronicos" + InsSql.CharTypeForDb(1) + " = '1',");
            SQL.AppendLine("@CodigoPaisResidencia" + InsSql.VarCharTypeForDb(4) + ",");
            SQL.AppendLine("@CodigoConveniosSunat" + InsSql.VarCharTypeForDb(2) + ",");
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
            SQL.AppendLine("            Consecutivo,");
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
            SQL.AppendLine("            NumeroCuentaBancaria,");
            SQL.AppendLine("            CodigoContribuyente,");
            SQL.AppendLine("            NumeroRUC,");
            SQL.AppendLine("            TipoDePersonaLibrosElectronicos,");
            SQL.AppendLine("            CodigoPaisResidencia,");
            SQL.AppendLine("            CodigoConveniosSunat,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @CodigoProveedor,");
            SQL.AppendLine("            @Consecutivo,");
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
            SQL.AppendLine("            @NumeroCuentaBancaria,");
            SQL.AppendLine("            @CodigoContribuyente,");
            SQL.AppendLine("            @NumeroRUC,");
            SQL.AppendLine("            @TipoDePersonaLibrosElectronicos,");
            SQL.AppendLine("            @CodigoPaisResidencia,");
            SQL.AppendLine("            @CodigoConveniosSunat,");
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
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NombreProveedor" + InsSql.VarCharTypeForDb(160) + ",");
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
            SQL.AppendLine("@NumeroCuentaBancaria" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@CodigoContribuyente" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@NumeroRUC" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@TipoDePersonaLibrosElectronicos" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@CodigoPaisResidencia" + InsSql.VarCharTypeForDb(4) + ",");
            SQL.AppendLine("@CodigoConveniosSunat" + InsSql.VarCharTypeForDb(2) + ",");
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
            SQL.AppendLine("            SET Consecutivo = @Consecutivo,");
            SQL.AppendLine("               NombreProveedor = @NombreProveedor,");
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
            SQL.AppendLine("               NumeroCuentaBancaria = @NumeroCuentaBancaria,");
            SQL.AppendLine("               CodigoContribuyente = @CodigoContribuyente,");
            SQL.AppendLine("               NumeroRUC = @NumeroRUC,");
            SQL.AppendLine("               TipoDePersonaLibrosElectronicos = @TipoDePersonaLibrosElectronicos,");
            SQL.AppendLine("               CodigoPaisResidencia = @CodigoPaisResidencia,");
            SQL.AppendLine("               CodigoConveniosSunat = @CodigoConveniosSunat,");
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
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.ConsecutivoCompania,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.CodigoProveedor,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.Consecutivo,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.NombreProveedor,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.Contacto,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.NumeroRIF,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.NumeroNIT,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.TipoDePersona,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.TipoDePersonaStr,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.CodigoRetencionUsual,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.Telefonos,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.Direccion,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.Fax,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.Email,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.TipodeProveedor,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.TipoDeProveedorDeLibrosFiscales,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.PorcentajeRetencionIVA,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.CuentaContableCxP,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.CuentaContableGastos,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.CuentaContableAnticipo,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.CodigoLote,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.Beneficiario,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.UsarBeneficiarioImpCheq,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.TipoDocumentoIdentificacion,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.EsAgenteDeRetencionIva,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.Nombre,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.ApellidoPaterno,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.ApellidoMaterno,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.NumeroCuentaBancaria,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.CodigoContribuyente,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.NumeroRUC,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.TipoDePersonaLibrosElectronicos,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.CodigoPaisResidencia,");                                    
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.CodigoConveniosSunat,");            
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.NombreOperador,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.FechaUltimaModificacion,");
            SQL.AppendLine("         CAST(" + DbSchema + ".Gv_Proveedor_B1.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         " + DbSchema + ".Gv_Proveedor_B1.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Proveedor_B1");
            SQL.AppendLine("             INNER JOIN Saw.Gv_TipoProveedor_B1 ON " + DbSchema + ".Gv_Proveedor_B1.TipodeProveedor = Saw.Gv_TipoProveedor_B1.nombre");
            SQL.AppendLine("             AND Adm.Gv_Proveedor_B1.ConsecutivoCompania = Saw.Gv_TipoProveedor_B1.ConsecutivoCompania");            
			SQL.AppendLine("      WHERE " + DbSchema + ".Gv_Proveedor_B1.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND " + DbSchema + ".Gv_Proveedor_B1.CodigoProveedor = @CodigoProveedor");
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
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.NombreProveedor,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Contacto,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.NumeroRIF,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.NumeroNIT,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.TipoDePersona,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.TipoDePersonaStr,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CodigoRetencionUsual,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Telefonos,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Direccion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Fax,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Email,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.TipodeProveedor,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.TipoDeProveedorDeLibrosFiscales,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.PorcentajeRetencionIVA,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CuentaContableCxP,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CuentaContableGastos,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CuentaContableAnticipo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CodigoLote,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Beneficiario,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.UsarBeneficiarioImpCheq,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.TipoDocumentoIdentificacion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.EsAgenteDeRetencionIva,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Nombre,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.ApellidoPaterno,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.ApellidoMaterno,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.NumeroCuentaBancaria,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CodigoContribuyente,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.TipoDePersonaLibrosElectronicosStr,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CodigoPaisResidencia,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CodigoConveniosSunat");           
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Proveedor_B1");
            SQL.AppendLine("      INNER JOIN Saw.Gv_TipoProveedor_B1 ON  " + DbSchema + ".Gv_Proveedor_B1.TipodeProveedor = Saw.Gv_TipoProveedor_B1.nombre");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Proveedor_B1.ConsecutivoCompania = Saw.Gv_TipoProveedor_B1.ConsecutivoCompania");            
            SQL.AppendLine("'   IF (NOT @SQLWhere IS NULL) AND (@SQLWhere <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' WHERE ' + @SQLWhere");
            SQL.AppendLine("   IF (NOT @SQLOrderBy IS NULL) AND (@SQLOrderBy <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' ORDER BY ' + @SQLOrderBy");
            SQL.AppendLine("   EXEC(@strSQL)");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSearchForPagoParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@SQLWhere" + InsSql.VarCharTypeForDb(2000) + " = null,");
            SQL.AppendLine("@SQLOrderBy" + InsSql.VarCharTypeForDb(500) + " = null,");
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + " = null,");
            SQL.AppendLine("@UseTopClausule" + InsSql.VarCharTypeForDb(1) + " = 'S'");
            return SQL.ToString();
        }

        private string SqlSpSearchForPago() {
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
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.NombreProveedor,");
            SQL.AppendLine("      ''COLPIVOTE'' AS ColControl,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Contacto,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.NumeroRIF,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.NumeroNIT,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.TipoDePersona,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.TipoDePersonaStr,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CodigoRetencionUsual,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Telefonos,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Direccion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Fax,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Email,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.TipodeProveedor,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.TipoDeProveedorDeLibrosFiscales,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.PorcentajeRetencionIVA,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CuentaContableCxP,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CuentaContableGastos,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CuentaContableAnticipo,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CodigoLote,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Beneficiario,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.UsarBeneficiarioImpCheq,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.TipoDocumentoIdentificacion,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.EsAgenteDeRetencionIva,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.Nombre,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.ApellidoPaterno,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.ApellidoMaterno,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.NumeroCuentaBancaria,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CodigoContribuyente, ");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.TipoDePersonaLibrosElectronicosStr,");
            SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CodigoPaisResidencia, ");
			SQL.AppendLine("      " + DbSchema + ".Gv_Proveedor_B1.CodigoConveniosSunat");
            SQL.AppendLine("      FROM " + DbSchema + ".Gv_Proveedor_B1");
            SQL.AppendLine("      INNER JOIN Saw.Gv_TipoProveedor_B1 ON  " + DbSchema + ".Gv_Proveedor_B1.TipodeProveedor = Saw.Gv_TipoProveedor_B1.nombre");
            SQL.AppendLine("      AND " + DbSchema + ".Gv_Proveedor_B1.ConsecutivoCompania = Saw.Gv_TipoProveedor_B1.ConsecutivoCompania");

            SQL.AppendLine(" WHERE");
            SQL.AppendLine(" " + DbSchema + ".Gv_Proveedor_B1.CodigoProveedor IN ( SELECT DISTINCT cxp.Codigoproveedor FROM cxp WHERE ((cxp.Status = '+ QUOTENAME('0','''')+ ') OR (cxp.Status = '+ QUOTENAME('3','''')+ ')) ");
            SQL.AppendLine(" AND cxp.ConsecutivoCompania = " + DbSchema + ".Gv_Proveedor_B1.ConsecutivoCompania) ");

            SQL.AppendLine("'   IF (NOT @SQLWhere IS NULL) AND (@SQLWhere <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' AND ' + @SQLWhere");
            SQL.AppendLine("   IF (NOT @SQLOrderBy IS NULL) AND (@SQLOrderBy <> '')");
            SQL.AppendLine("      SET @strSQL = @strSQL + ' ORDER BY ' + @SQLOrderBy");
            SQL.AppendLine("   EXEC(@strSQL)");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpGetFKParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) );            
            return SQL.ToString();
        }

        private string SqlSpInsAuxiliarFromProveedorParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoPeriodo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@TipoDeAuxiliar" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(35) + " = '',");
            SQL.AppendLine("@NoRif" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@NoNit" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + " = '01/01/1900'");
            return SQL.ToString();
        }

        private string SqlSpInsAuxiliarFromProveedor() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoPeriodo FROM Periodo WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo)");
            SQL.AppendLine("	BEGIN");
            SQL.AppendLine("        BEGIN TRAN");
            SQL.AppendLine("            INSERT INTO dbo.Auxiliar(");
            SQL.AppendLine("            ConsecutivoPeriodo,");
            SQL.AppendLine("            TipoDeAuxiliar,");
            SQL.AppendLine("            Codigo,");
            SQL.AppendLine("            Nombre,");
            SQL.AppendLine("            NoRif,");
            SQL.AppendLine("            NoNit,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoPeriodo,");
            SQL.AppendLine("            @TipoDeAuxiliar,");
            SQL.AppendLine("            @Codigo,");
            SQL.AppendLine("            @Nombre,");
            SQL.AppendLine("            @NoRif,");
            SQL.AppendLine("            @NoNit,");
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

        private string SqlSpUpdAuxiliarFromProveedorParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoPeriodo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@TipoDeAuxiliar" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@Nombre" + InsSql.VarCharTypeForDb(35) + " = '',");
            SQL.AppendLine("@NoRif" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@NoNit" + InsSql.VarCharTypeForDb(10) + " = '',");
            SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@TimeStampAsInt" + InsSql.BigintTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpUpdAuxiliarFromProveedor() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @CurrentTimeStamp timestamp");
            SQL.AppendLine("   DECLARE @ValidationMsg " + InsSql.VarCharTypeForDb(1500) + " --No puede ser más");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            //SQL.AppendLine("--DECLARE @CanBeChanged bit");
            SQL.AppendLine("   SET @ReturnValue = -1");
            SQL.AppendLine("   SET @ValidationMsg = ''");
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoPeriodo FROM dbo.Auxiliar WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Codigo = @Codigo)");
            SQL.AppendLine("   BEGIN");
            //SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM dbo.Auxiliar WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Codigo = @Codigo");
            //SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            //SQL.AppendLine("      BEGIN");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE dbo.Auxiliar");
            SQL.AppendLine("            SET ");
            SQL.AppendLine("               Nombre = @Nombre,");
            SQL.AppendLine("               NoRif = @NoRif,");
            SQL.AppendLine("               NoNit = @NoNit,");
            SQL.AppendLine("               NombreOperador = @NombreOperador,");
            SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
            SQL.AppendLine("            WHERE ");
            SQL.AppendLine("               ConsecutivoPeriodo = @ConsecutivoPeriodo");
            SQL.AppendLine("               AND TipoDeAuxiliar = @TipoDeAuxiliar");
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
            //SQL.AppendLine("      END");
            //SQL.AppendLine("      ELSE");
            //SQL.AppendLine("         RAISERROR('El registro ha sido modificado o eliminado por otro usuario.', 14, 1)");
            SQL.AppendLine("   END");
            SQL.AppendLine("   ELSE");
            SQL.AppendLine("      RAISERROR('El registro no existe.', 14, 1)");
            SQL.AppendLine("   RETURN @ReturnValue");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelAuxiliarFromProveedorParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoPeriodo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@Codigo" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@TipoDeAuxiliar" + InsSql.CharTypeForDb(1) + " = '0'");
            return SQL.ToString();
        }

        private string SqlSpDelAuxiliarFromProveedor() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   DECLARE @ValidationMsg " + InsSql.VarCharTypeForDb(1500) + " --No puede ser más");
            SQL.AppendLine("   DECLARE @ReturnValue " + InsSql.NumericTypeForDb(10, 0) + "");
            //SQL.AppendLine("--DECLARE @CanBeDeleted bit");
            SQL.AppendLine("   SET @ReturnValue = -1");
            SQL.AppendLine("   SET @ValidationMsg = ''");
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoPeriodo FROM dbo.Auxiliar WHERE ConsecutivoPeriodo = @ConsecutivoPeriodo AND Codigo = @Codigo)");
            SQL.AppendLine("   BEGIN");
            //SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM dbo.Auxiliar WHERE Codigo = @Codigo AND Codigo = @Codigo");
            //SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            //SQL.AppendLine("      BEGIN");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM dbo.Auxiliar");
            SQL.AppendLine("            WHERE ");
            SQL.AppendLine("               ConsecutivoPeriodo = @ConsecutivoPeriodo");
            SQL.AppendLine("               AND TipoDeAuxiliar = @TipoDeAuxiliar");
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
            //SQL.AppendLine("      END");
            //SQL.AppendLine("      ELSE");
            //SQL.AppendLine("         RAISERROR('El registro ha sido modificado o eliminado por otro usuario.', 14, 1)");
            SQL.AppendLine("   END");
            SQL.AppendLine("   ELSE");
            SQL.AppendLine("      RAISERROR('El registro no existe.', 14, 1)");
            SQL.AppendLine("   RETURN @ReturnValue");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpGetFK() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("      " + DbSchema + ".Proveedor.ConsecutivoCompania,");
            SQL.AppendLine("      " + DbSchema + ".Proveedor.Consecutivo,");
            SQL.AppendLine("      " + DbSchema + ".Proveedor.CodigoProveedor,");
            SQL.AppendLine("      " + DbSchema + ".Proveedor.NombreProveedor,");
            //SQL.AppendLine("      " + DbSchema + ".Proveedor.Contacto,");
            //SQL.AppendLine("      " + DbSchema + ".Proveedor.CodigoRetencionUsual,");
            //SQL.AppendLine("      " + DbSchema + ".Proveedor.Telefonos,");
            SQL.AppendLine("      " + DbSchema + ".Proveedor.TipodeProveedor,");
            //SQL.AppendLine("      " + DbSchema + ".Proveedor.CuentaContableAnticipo");
            SQL.AppendLine("       " + DbSchema + ".Proveedor.CodigoPaisResidencia,");
            SQL.AppendLine("       " + DbSchema + ".Proveedor.CodigoConveniosSunat");
            SQL.AppendLine("      FROM " + DbSchema + ".Proveedor");
            SQL.AppendLine("      WHERE " + DbSchema + ".Proveedor.ConsecutivoCompania = @ConsecutivoCompania");
            //SQL.AppendLine("      AND " + DbSchema + ".Proveedor.Consecutivo = @ConsecutivoProveedor");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInstParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@CodigoProveedor" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@Consecutivo" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NombreProveedor" + InsSql.VarCharTypeForDb(160) + ",");
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
            SQL.AppendLine("@NumeroCuentaBancaria" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@CodigoContribuyente" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@TipoDePersonaLibrosElectronicos" + InsSql.CharTypeForDb(1) + ",");
			SQL.AppendLine("@CodigoPaisResidencia" + InsSql.VarCharTypeForDb(4) + ",");
			SQL.AppendLine("@CodigoConveniosSunat" + InsSql.VarCharTypeForDb(2) + ",");
			SQL.AppendLine("@NumeroRUC" + InsSql.VarCharTypeForDb(20) + ",");            
			SQL.AppendLine("@NombreOperador" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@FechaUltimaModificacion" + InsSql.DateTypeForDb());
            return SQL.ToString();
        }

        private string SqlSpInst() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".Proveedor");
            SQL.AppendLine("            SET Consecutivo = @Consecutivo,");
            SQL.AppendLine("               NombreProveedor = @NombreProveedor,");
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
            SQL.AppendLine("               NumeroCuentaBancaria = @NumeroCuentaBancaria,");
            SQL.AppendLine("               CodigoContribuyente = @CodigoContribuyente,");
            SQL.AppendLine("               NumeroRUC = @NumeroRUC,");
            SQL.AppendLine("               TipoDePersonaLibrosElectronicos = @TipoDePersonaLibrosElectronicos,");
            SQL.AppendLine("               CodigoPaisResidencia = @CodigoPaisResidencia,");
            SQL.AppendLine("               CodigoConveniosSunat = @CodigoConveniosSunat,");            
			SQL.AppendLine("               NombreOperador = @NombreOperador,");
            SQL.AppendLine("               FechaUltimaModificacion = @FechaUltimaModificacion");
            SQL.AppendLine("               WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND CodigoProveedor = @CodigoProveedor");
            SQL.AppendLine("	IF @@ROWCOUNT = 0");
            SQL.AppendLine("        INSERT INTO " + DbSchema + ".Proveedor(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            CodigoProveedor,");
            SQL.AppendLine("            Consecutivo,");
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
            SQL.AppendLine("            NumeroCuentaBancaria,");
            SQL.AppendLine("            CodigoContribuyente,");
            SQL.AppendLine("            NumeroRUC,");
            SQL.AppendLine("            TipoDePersonaLibrosElectronicos,");
            SQL.AppendLine("            CodigoPaisResidencia,");
     		SQL.AppendLine("            CodigoConveniosSunat,");
            SQL.AppendLine("            NombreOperador,");
            SQL.AppendLine("            FechaUltimaModificacion)");
            SQL.AppendLine("        VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @CodigoProveedor,");
            SQL.AppendLine("            @Consecutivo,");
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
            SQL.AppendLine("            @NumeroCuentaBancaria,");
            SQL.AppendLine("            @CodigoContribuyente,");
            SQL.AppendLine("            @NumeroRUC,");
            SQL.AppendLine("            @TipoDePersonaLibrosElectronicos,");
            SQL.AppendLine("            @CodigoPaisResidencia,");
            SQL.AppendLine("            @CodigoConveniosSunat,");
            SQL.AppendLine("            @NombreOperador,");
            SQL.AppendLine("            @FechaUltimaModificacion)");
            SQL.AppendLine(" 	IF @@ERROR = 0");
            SQL.AppendLine(" 		COMMIT TRAN");
            SQL.AppendLine(" 	ELSE");
            SQL.AppendLine(" 		ROLLBACK");
            SQL.AppendLine("END ");
            return SQL.ToString();
        }

        #endregion //Queries

        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".Proveedor", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }

        bool CrearVistas() {
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipodePersonaRetencion", LibTpvCreator.SqlViewStandardEnum(typeof(eTipodePersonaRetencion), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDeProveedorDeLibrosFiscales", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDeProveedorDeLibrosFiscales), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDocumentoIdentificacion", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDocumentoIdentificacion), InsSql), true, true);
            vResult = insVistas.Create(DbSchema + ".Gv_EnumTipoDePersonaLibrosElectronicos", LibTpvCreator.SqlViewStandardEnum(typeof(eTipoDePersonaLibrosElectronicos), InsSql), true, true);
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
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ProveedorForPagoSCH", SqlSpSearchForPagoParameters(), SqlSpSearchForPago(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ProveedorGetFk", SqlSpGetFKParameters(), SqlSpGetFK(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_ProveedorINST", SqlSpInstParameters(), SqlSpInst(), true) && vResult;

            //ERROR!!! - ESTO DEBE PROGRAMARSE A NIVEL DE LA CAPA DE NEGOCIOS
            //vResult = insSps.CreateStoredProcedure("dbo" + ".Gp_AuxiliarINSFromProveedor", SqlSpInsAuxiliarFromProveedorParameters(),SqlSpInsAuxiliarFromProveedor(), true) && vResult;
            //vResult = insSps.CreateStoredProcedure("dbo" + ".Gp_AuxiliarUPDFromProveedor", SqlSpUpdAuxiliarFromProveedorParameters(), SqlSpUpdAuxiliarFromProveedor(), true) && vResult;
            //vResult = insSps.CreateStoredProcedure("dbo" + ".Gp_AuxiliarDELFromProveedor", SqlSpDelAuxiliarFromProveedorParameters(), SqlSpDelAuxiliarFromProveedor(), true) && vResult;
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
            vResult = insSp.Drop(DbSchema + ".Gp_ProveedorINST") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_Proveedor_B1") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipodePersonaRetencion") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDeProveedorDeLibrosFiscales") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDocumentoIdentificacion") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_EnumTipoDePersonaLibrosElectronicos") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsProveedorED

} //End of namespace Galac.Adm.Dal.GestionCompras

