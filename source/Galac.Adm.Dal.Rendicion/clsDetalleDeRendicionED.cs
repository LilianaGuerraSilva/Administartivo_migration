using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Dal.Contracts;
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Dal.CajaChica {
    [LibMefDalComponentMetadata(typeof(clsDetalleDeRendicionED))]
    public class clsDetalleDeRendicionED: LibED, ILibMefDalComponent {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsDetalleDeRendicionED(): base(){
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
            get { return "DetalleDeRendicion"; }
        }

        bool ILibMefDalComponent.InstallTable() {
            return InstalarTabla();
        }
        #endregion
        #region Queries

        private string SqlCreateTable() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine(InsSql.CreateTable("DetalleDeRendicion", DbSchema) + " ( ");
            SQL.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnDetDeRenConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoRendicion" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnDetDeRenConsecutiv NOT NULL, ");
            SQL.AppendLine("ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnDetDeRenConsecutiv NOT NULL, ");
            SQL.AppendLine("NumeroDocumento" + InsSql.VarCharTypeForDb(25) + " CONSTRAINT nnDetDeRenNumeroDocu NOT NULL, ");
            SQL.AppendLine("NumeroControl" + InsSql.VarCharTypeForDb(20) + " CONSTRAINT nnDetDeRenNumeroCont NOT NULL, ");
            SQL.AppendLine("Fecha" + InsSql.DateTypeForDb() + " CONSTRAINT nnDetDeRenFecha NOT NULL, ");
            SQL.AppendLine("CodigoProveedor" + InsSql.VarCharTypeForDb(10) + " CONSTRAINT nnDetDeRenCodigoProv NOT NULL, ");
            SQL.AppendLine("MontoExento" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnDetDeRenMontoExent NOT NULL, ");
            SQL.AppendLine("MontoGravable" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnDetDeRenMontoGrava NOT NULL, ");
            SQL.AppendLine("MontoIVA" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT nnDetDeRenMontoIVA NOT NULL, ");
            SQL.AppendLine("MontoRetencion" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_DetDeRenMoRe DEFAULT (0), ");
            SQL.AppendLine("AplicaParaLibroDeCompras" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnDetDeRenAplicaPara NOT NULL, ");
            SQL.AppendLine("ObservacionesCxP" + InsSql.VarCharTypeForDb(50) + " CONSTRAINT d_DetDeRenObCxP DEFAULT (''), ");
            SQL.AppendLine("GeneradaPor" + InsSql.CharTypeForDb(1) + " CONSTRAINT d_DetDeRenGePo DEFAULT ('0'), ");
            SQL.AppendLine("AplicaIvaAlicuotaEspecial" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnDetDeRenAplicaIvaA NOT NULL, ");
            SQL.AppendLine("MontoGravableAlicuotaEspecial1" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_DetDeRenMoGrAlEs1 DEFAULT (0), ");
            SQL.AppendLine("MontoIVAAlicuotaEspecial1" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_DetDeRenMoIVEs1 DEFAULT (0), ");
            SQL.AppendLine("PorcentajeIvaAlicuotaEspecial1" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_DetDeRenPoIvAlEs1 DEFAULT (0), ");
            SQL.AppendLine("MontoGravableAlicuotaEspecial2" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_DetDeRenMoGrAlEs2 DEFAULT (0), ");
            SQL.AppendLine("MontoIVAAlicuotaEspecial2" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_DetDeRenMoIVEs2 DEFAULT (0), ");
            SQL.AppendLine("PorcentajeIvaAlicuotaEspecial2" + InsSql.DecimalTypeForDb(25, 4) + " CONSTRAINT d_DetDeRenPoIvAlEs2 DEFAULT (0), ");
            SQL.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb() + ",");
            SQL.AppendLine("CONSTRAINT p_DetalleDeRendicion PRIMARY KEY CLUSTERED");
            SQL.AppendLine("(ConsecutivoCompania ASC, ConsecutivoRendicion ASC, ConsecutivoRenglon ASC)");
            SQL.AppendLine(",CONSTRAINT fk_DetalleDeRendicionRendicion FOREIGN KEY (ConsecutivoCompania, ConsecutivoRendicion)");
            SQL.AppendLine("REFERENCES Adm.Rendicion(ConsecutivoCompania, Consecutivo)");
            SQL.AppendLine("ON DELETE CASCADE");
            SQL.AppendLine("ON UPDATE CASCADE");
            SQL.AppendLine(")");
            return SQL.ToString();
        }

        private string SqlViewB1() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT DetalleDeRendicion.ConsecutivoCompania, DetalleDeRendicion.ConsecutivoRendicion, DetalleDeRendicion.ConsecutivoRenglon, DetalleDeRendicion.NumeroDocumento");
            SQL.AppendLine(", DetalleDeRendicion.NumeroControl, DetalleDeRendicion.Fecha, DetalleDeRendicion.CodigoProveedor, DetalleDeRendicion.MontoExento");
            SQL.AppendLine(", DetalleDeRendicion.MontoGravable, DetalleDeRendicion.MontoIVA, DetalleDeRendicion.MontoRetencion, DetalleDeRendicion.AplicaParaLibroDeCompras");
            SQL.AppendLine(", DetalleDeRendicion.ObservacionesCxP, DetalleDeRendicion.GeneradaPor, Saw.Gv_EnumGeneradoPor.StrValue AS GeneradaPorStr, DetalleDeRendicion.AplicaIvaAlicuotaEspecial, DetalleDeRendicion.MontoGravableAlicuotaEspecial1");
            SQL.AppendLine(", DetalleDeRendicion.MontoIVAAlicuotaEspecial1, DetalleDeRendicion.PorcentajeIvaAlicuotaEspecial1, DetalleDeRendicion.MontoGravableAlicuotaEspecial2, DetalleDeRendicion.MontoIVAAlicuotaEspecial2");
            SQL.AppendLine(", DetalleDeRendicion.PorcentajeIvaAlicuotaEspecial2");
            SQL.AppendLine(", Adm.Proveedor.NombreProveedor AS NombreProveedor");
            SQL.AppendLine(", DetalleDeRendicion.fldTimeStamp, CAST(DetalleDeRendicion.fldTimeStamp AS bigint) AS fldTimeStampBigint");
            SQL.AppendLine("FROM " + DbSchema + ".DetalleDeRendicion");
            SQL.AppendLine("INNER JOIN Saw.Gv_EnumGeneradoPor");
            SQL.AppendLine("ON " + DbSchema + ".DetalleDeRendicion.GeneradaPor COLLATE MODERN_SPANISH_CS_AS");
            SQL.AppendLine(" = Saw.Gv_EnumGeneradoPor.DbValue");
            SQL.AppendLine("INNER JOIN Adm.Proveedor ON  " + DbSchema + ".DetalleDeRendicion.CodigoProveedor = Adm.Proveedor.CodigoProveedor");
            SQL.AppendLine("      AND " + DbSchema + ".DetalleDeRendicion.ConsecutivoCompania = Adm.Proveedor.ConsecutivoCompania");
            return SQL.ToString();
        }

        private string SqlSpInsParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoRendicion" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(25) + " = '',");
            SQL.AppendLine("@NumeroControl" + InsSql.VarCharTypeForDb(20) + " = '',");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + " = '01/01/1900',");
            SQL.AppendLine("@CodigoProveedor" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@MontoExento" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoGravable" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoIVA" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoRetencion" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@AplicaParaLibroDeCompras" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@ObservacionesCxP" + InsSql.VarCharTypeForDb(50) + " = '',");
            SQL.AppendLine("@GeneradaPor" + InsSql.CharTypeForDb(1) + " = '0',");
            SQL.AppendLine("@AplicaIvaAlicuotaEspecial" + InsSql.CharTypeForDb(1) + " = 'N',");
            SQL.AppendLine("@MontoGravableAlicuotaEspecial1" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoIVAAlicuotaEspecial1" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PorcentajeIvaAlicuotaEspecial1" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoGravableAlicuotaEspecial2" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@MontoIVAAlicuotaEspecial2" + InsSql.DecimalTypeForDb(25, 4) + " = 0,");
            SQL.AppendLine("@PorcentajeIvaAlicuotaEspecial2" + InsSql.DecimalTypeForDb(25, 4) + " = 0");
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
            SQL.AppendLine("            INSERT INTO " + DbSchema + ".DetalleDeRendicion(");
            SQL.AppendLine("            ConsecutivoCompania,");
            SQL.AppendLine("            ConsecutivoRendicion,");
            SQL.AppendLine("            ConsecutivoRenglon,");
            SQL.AppendLine("            NumeroDocumento,");
            SQL.AppendLine("            NumeroControl,");
            SQL.AppendLine("            Fecha,");
            SQL.AppendLine("            CodigoProveedor,");
            SQL.AppendLine("            MontoExento,");
            SQL.AppendLine("            MontoGravable,");
            SQL.AppendLine("            MontoIVA,");
            SQL.AppendLine("            MontoRetencion,");
            SQL.AppendLine("            AplicaParaLibroDeCompras,");
            SQL.AppendLine("            ObservacionesCxP,");
            SQL.AppendLine("            GeneradaPor,");
            SQL.AppendLine("            AplicaIvaAlicuotaEspecial,");
            SQL.AppendLine("            MontoGravableAlicuotaEspecial1,");
            SQL.AppendLine("            MontoIVAAlicuotaEspecial1,");
            SQL.AppendLine("            PorcentajeIvaAlicuotaEspecial1,");
            SQL.AppendLine("            MontoGravableAlicuotaEspecial2,");
            SQL.AppendLine("            MontoIVAAlicuotaEspecial2,");
            SQL.AppendLine("            PorcentajeIvaAlicuotaEspecial2)");
            SQL.AppendLine("            VALUES(");
            SQL.AppendLine("            @ConsecutivoCompania,");
            SQL.AppendLine("            @ConsecutivoRendicion,");
            SQL.AppendLine("            @ConsecutivoRenglon,");
            SQL.AppendLine("            @NumeroDocumento,");
            SQL.AppendLine("            @NumeroControl,");
            SQL.AppendLine("            @Fecha,");
            SQL.AppendLine("            @CodigoProveedor,");
            SQL.AppendLine("            @MontoExento,");
            SQL.AppendLine("            @MontoGravable,");
            SQL.AppendLine("            @MontoIVA,");
            SQL.AppendLine("            @MontoRetencion,");
            SQL.AppendLine("            @AplicaParaLibroDeCompras,");
            SQL.AppendLine("            @ObservacionesCxP,");
            SQL.AppendLine("            @GeneradaPor,");
            SQL.AppendLine("            @AplicaIvaAlicuotaEspecial,");
            SQL.AppendLine("            @MontoGravableAlicuotaEspecial1,");
            SQL.AppendLine("            @MontoIVAAlicuotaEspecial1,");
            SQL.AppendLine("            @PorcentajeIvaAlicuotaEspecial1,");
            SQL.AppendLine("            @MontoGravableAlicuotaEspecial2,");
            SQL.AppendLine("            @MontoIVAAlicuotaEspecial2,");
            SQL.AppendLine("            @PorcentajeIvaAlicuotaEspecial2)");
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
            SQL.AppendLine("@ConsecutivoRendicion" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@NumeroDocumento" + InsSql.VarCharTypeForDb(25) + ",");
            SQL.AppendLine("@NumeroControl" + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("@Fecha" + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("@CodigoProveedor" + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("@MontoExento" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoGravable" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoIVA" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoRetencion" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@AplicaParaLibroDeCompras" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@ObservacionesCxP" + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("@GeneradaPor" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@AplicaIvaAlicuotaEspecial" + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("@MontoGravableAlicuotaEspecial1" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoIVAAlicuotaEspecial1" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PorcentajeIvaAlicuotaEspecial1" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoGravableAlicuotaEspecial2" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@MontoIVAAlicuotaEspecial2" + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("@PorcentajeIvaAlicuotaEspecial2" + InsSql.DecimalTypeForDb(25, 4) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".DetalleDeRendicion WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoRendicion = @ConsecutivoRendicion AND ConsecutivoRenglon = @ConsecutivoRenglon)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".DetalleDeRendicion WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoRendicion = @ConsecutivoRendicion AND ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeChanged bit; EXEC @CanBeChanged = " + DbSchema + ".Gp_DetalleDeRendicionCanBeUpdated @ConsecutivoCompania,@ConsecutivoRendicion,@ConsecutivoRenglon, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeChanged = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         UPDATE " + DbSchema + ".DetalleDeRendicion");
            SQL.AppendLine("            SET NumeroDocumento = @NumeroDocumento,");
            SQL.AppendLine("               NumeroControl = @NumeroControl,");
            SQL.AppendLine("               Fecha = @Fecha,");
            SQL.AppendLine("               CodigoProveedor = @CodigoProveedor,");
            SQL.AppendLine("               MontoExento = @MontoExento,");
            SQL.AppendLine("               MontoGravable = @MontoGravable,");
            SQL.AppendLine("               MontoIVA = @MontoIVA,");
            SQL.AppendLine("               MontoRetencion = @MontoRetencion,");
            SQL.AppendLine("               AplicaParaLibroDeCompras = @AplicaParaLibroDeCompras,");
            SQL.AppendLine("               ObservacionesCxP = @ObservacionesCxP,");
            SQL.AppendLine("               GeneradaPor = @GeneradaPor,");
            SQL.AppendLine("               AplicaIvaAlicuotaEspecial = @AplicaIvaAlicuotaEspecial,");
            SQL.AppendLine("               MontoGravableAlicuotaEspecial1 = @MontoGravableAlicuotaEspecial1,");
            SQL.AppendLine("               MontoIVAAlicuotaEspecial1 = @MontoIVAAlicuotaEspecial1,");
            SQL.AppendLine("               PorcentajeIvaAlicuotaEspecial1 = @PorcentajeIvaAlicuotaEspecial1,");
            SQL.AppendLine("               MontoGravableAlicuotaEspecial2 = @MontoGravableAlicuotaEspecial2,");
            SQL.AppendLine("               MontoIVAAlicuotaEspecial2 = @MontoIVAAlicuotaEspecial2,");
            SQL.AppendLine("               PorcentajeIvaAlicuotaEspecial2 = @PorcentajeIvaAlicuotaEspecial2");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoRendicion = @ConsecutivoRendicion");
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
            SQL.AppendLine("@ConsecutivoRendicion" + InsSql.NumericTypeForDb(10, 0) + ",");
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
            SQL.AppendLine("   IF EXISTS(SELECT ConsecutivoCompania FROM " + DbSchema + ".DetalleDeRendicion WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoRendicion = @ConsecutivoRendicion AND ConsecutivoRenglon = @ConsecutivoRenglon)");
            SQL.AppendLine("   BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".DetalleDeRendicion WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoRendicion = @ConsecutivoRendicion AND ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("      IF (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt)");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("--Para Validaciones de FK Lógicas crear e invocar:DECLARE @CanBeDeleted bit; EXEC @CanBeDeleted = " + DbSchema + ".Gp_DetalleDeRendicionCanBeDeleted @ConsecutivoCompania,@ConsecutivoRendicion,@ConsecutivoRenglon, @CurrentTimeStamp, @ValidationMsg out");
            //SQL.AppendLine("--IF @CanBeDeleted = 1 --True");
            //SQL.AppendLine("--BEGIN");
            SQL.AppendLine("         BEGIN TRAN");
            SQL.AppendLine("         DELETE FROM " + DbSchema + ".DetalleDeRendicion");
            SQL.AppendLine("            WHERE fldTimeStamp = @CurrentTimeStamp");
            SQL.AppendLine("               AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("               AND ConsecutivoRendicion = @ConsecutivoRendicion");
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
            SQL.AppendLine("@ConsecutivoRendicion" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoRenglon" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpGet() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("   SET NOCOUNT ON;");
            SQL.AppendLine("   SELECT ");
            SQL.AppendLine("         " + DbSchema + ".DetalleDeRendicion.ConsecutivoCompania,");
            SQL.AppendLine("         ConsecutivoRendicion,");
            SQL.AppendLine("         ConsecutivoRenglon,");
            SQL.AppendLine("         NumeroDocumento,");
            SQL.AppendLine("         NumeroControl,");
            SQL.AppendLine("         Fecha,");
            SQL.AppendLine("         "+DbSchema+".DetalleDeRendicion.CodigoProveedor,");
            SQL.AppendLine("         Adm.Proveedor.NombreProveedor,");
            SQL.AppendLine("         MontoExento,");
            SQL.AppendLine("         MontoGravable,");
            SQL.AppendLine("         MontoIVA,");
            SQL.AppendLine("         MontoRetencion,");
            SQL.AppendLine("         AplicaParaLibroDeCompras,");
            SQL.AppendLine("         ObservacionesCxP,");
            SQL.AppendLine("         GeneradaPor,");
            SQL.AppendLine("         AplicaIvaAlicuotaEspecial,");
            SQL.AppendLine("         MontoGravableAlicuotaEspecial1,");
            SQL.AppendLine("         MontoIVAAlicuotaEspecial1,");
            SQL.AppendLine("         PorcentajeIvaAlicuotaEspecial1,");
            SQL.AppendLine("         MontoGravableAlicuotaEspecial2,");
            SQL.AppendLine("         MontoIVAAlicuotaEspecial2,");
            SQL.AppendLine("         PorcentajeIvaAlicuotaEspecial2,");
            SQL.AppendLine("         CAST(" + DbSchema + ".DetalleDeRendicion.fldTimeStamp AS bigint) AS fldTimeStampBigint,");
            SQL.AppendLine("         " + DbSchema + ".DetalleDeRendicion.fldTimeStamp");
            SQL.AppendLine("      FROM " + DbSchema + ".DetalleDeRendicion");
            SQL.AppendLine("      INNER JOIN  " + "Adm" + ".Proveedor On (Adm.Proveedor.CodigoProveedor = " + DbSchema + ".DetalleDeRendicion.CodigoProveedor AND Adm.Proveedor.ConsecutivoCompania = " + DbSchema + ".DetalleDeRendicion.ConsecutivoCompania )");
            SQL.AppendLine("      WHERE " + DbSchema + ".DetalleDeRendicion.ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("         AND " + DbSchema + ".DetalleDeRendicion.ConsecutivoRendicion = @ConsecutivoRendicion");
            SQL.AppendLine("         AND " + DbSchema + ".DetalleDeRendicion.ConsecutivoRenglon = @ConsecutivoRenglon");
            SQL.AppendLine("   RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpSelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoRendicion" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpSelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SELECT ");
            SQL.AppendLine("       " + DbSchema + ".DetalleDeRendicion.ConsecutivoCompania,");
            SQL.AppendLine("        ConsecutivoRendicion,");
            SQL.AppendLine("        ConsecutivoRenglon,");
            SQL.AppendLine("        NumeroDocumento,");
            SQL.AppendLine("        NumeroControl,");
            SQL.AppendLine("        Fecha,");
            SQL.AppendLine("         " + DbSchema + ".DetalleDeRendicion.CodigoProveedor,");
            SQL.AppendLine("         Adm.Proveedor.NombreProveedor,");
            SQL.AppendLine("        MontoExento,");
            SQL.AppendLine("        MontoGravable,");
            SQL.AppendLine("        MontoIVA,");
            SQL.AppendLine("        MontoRetencion,");
            SQL.AppendLine("        AplicaParaLibroDeCompras,");
            SQL.AppendLine("        ObservacionesCxP,");
            SQL.AppendLine("        GeneradaPor,");
            SQL.AppendLine("        AplicaIvaAlicuotaEspecial,");
            SQL.AppendLine("        MontoGravableAlicuotaEspecial1,");
            SQL.AppendLine("        MontoIVAAlicuotaEspecial1,");
            SQL.AppendLine("        PorcentajeIvaAlicuotaEspecial1,");
            SQL.AppendLine("        MontoGravableAlicuotaEspecial2,");
            SQL.AppendLine("        MontoIVAAlicuotaEspecial2,");
            SQL.AppendLine("        PorcentajeIvaAlicuotaEspecial2,");
            SQL.AppendLine("        " + DbSchema + ".DetalleDeRendicion.fldTimeStamp");
            SQL.AppendLine("    FROM DetalleDeRendicion");
            SQL.AppendLine("      INNER JOIN  " + "Adm" + ".Proveedor On (Adm.Proveedor.CodigoProveedor = " + DbSchema + ".DetalleDeRendicion.CodigoProveedor AND Adm.Proveedor.ConsecutivoCompania = " + DbSchema + ".DetalleDeRendicion.ConsecutivoCompania )");
            SQL.AppendLine(" 	WHERE ConsecutivoRendicion = @ConsecutivoRendicion");
            SQL.AppendLine(" 	AND " + DbSchema + ".DetalleDeRendicion.ConsecutivoCompania= @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpDelDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoRendicion" + InsSql.NumericTypeForDb(10, 0));
            return SQL.ToString();
        }

        private string SqlSpDelDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	DELETE FROM DetalleDeRendicion");
            SQL.AppendLine(" 	WHERE ConsecutivoRendicion = @ConsecutivoRendicion");
            SQL.AppendLine(" 	AND ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("    RETURN @@ROWCOUNT");
            SQL.AppendLine("END");
            return SQL.ToString();
        }

        private string SqlSpInsDetailParameters() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("@DateFormat" + InsSql.VarCharTypeForDb(3) + ",");
            SQL.AppendLine("@ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("@ConsecutivoRendicion" + InsSql.NumericTypeForDb(10, 0)+ ",");
            SQL.AppendLine("@XmlDataDetail" + InsSql.XmlTypeForDb() + ",");
            SQL.AppendLine("@TimeStampAsInt" + InsSql.BigintTypeForDb() + ",");
            SQL.AppendLine("@ValidaMaestro" + InsSql.CharTypeForDb(1));
            return SQL.ToString();
        }

        private string SqlSpInsDetail() {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN");
            SQL.AppendLine("	SET NOCOUNT ON;");
            SQL.AppendLine("   SET DATEFORMAT @DateFormat");
            SQL.AppendLine("   DECLARE @CurrentTimeStamp timestamp");
            SQL.AppendLine("   DECLARE @ValidationMsg " + InsSql.VarCharTypeForDb(1500) + " --No puede ser más");
            SQL.AppendLine("   SET @ValidationMsg = ''");
            SQL.AppendLine("	DECLARE @ReturnValue  " + InsSql.NumericTypeForDb(10, 0));
	        SQL.AppendLine("	IF EXISTS(SELECT ConsecutivoCompania FROM Compania WHERE ConsecutivoCompania = @ConsecutivoCompania)");
	        SQL.AppendLine("	    BEGIN");
            SQL.AppendLine("      SELECT @CurrentTimeStamp = fldTimeStamp FROM " + DbSchema + ".Rendicion WHERE ConsecutivoCompania = @ConsecutivoCompania AND Consecutivo = @ConsecutivoRendicion");
            SQL.AppendLine("      IF (  (CAST(@CurrentTimeStamp AS bigint) = @TimeStampAsInt) OR  ( @ValidaMaestro = 'N'))");
            SQL.AppendLine("      BEGIN");
            SQL.AppendLine("	    EXEC Adm.Gp_DetalleDeRendicionDelDet @ConsecutivoCompania = @ConsecutivoCompania, @ConsecutivoRendicion = @ConsecutivoRendicion");
		    SQL.AppendLine("	    DECLARE @hdoc " + InsSql.NumericTypeForDb(10, 0));
            SQL.AppendLine("	    EXEC sp_xml_preparedocument @hdoc OUTPUT, @XmlDataDetail");
		    SQL.AppendLine("	    INSERT INTO Adm.DetalleDeRendicion(");
			SQL.AppendLine("	        ConsecutivoCompania,");
			SQL.AppendLine("	        ConsecutivoRendicion,");
			SQL.AppendLine("	        ConsecutivoRenglon,");
			SQL.AppendLine("	        NumeroDocumento,");
			SQL.AppendLine("	        NumeroControl,");
			SQL.AppendLine("	        Fecha,");
			SQL.AppendLine("	        CodigoProveedor,");
			SQL.AppendLine("	        MontoExento,");
			SQL.AppendLine("	        MontoGravable,");
			SQL.AppendLine("	        MontoIVA,");
			SQL.AppendLine("	        MontoRetencion,");
			SQL.AppendLine("	        AplicaParaLibroDeCompras,");
			SQL.AppendLine("	        ObservacionesCxP,");
			SQL.AppendLine("	        GeneradaPor,");
			SQL.AppendLine("	        AplicaIvaAlicuotaEspecial,");
			SQL.AppendLine("	        MontoGravableAlicuotaEspecial1,");
			SQL.AppendLine("	        MontoIVAAlicuotaEspecial1,");
			SQL.AppendLine("	        PorcentajeIvaAlicuotaEspecial1,");
			SQL.AppendLine("	        MontoGravableAlicuotaEspecial2,");
			SQL.AppendLine("	        MontoIVAAlicuotaEspecial2,");
			SQL.AppendLine("	        PorcentajeIvaAlicuotaEspecial2)");
		    SQL.AppendLine("	    SELECT ");
			SQL.AppendLine("	        @ConsecutivoCompania,");
			SQL.AppendLine("	        @ConsecutivoRendicion,");
			SQL.AppendLine("	        ConsecutivoRenglon,");
			SQL.AppendLine("	        NumeroDocumento,");
			SQL.AppendLine("	        NumeroControl,");
			SQL.AppendLine("	        Fecha,");
			SQL.AppendLine("	        CodigoProveedor,");
			SQL.AppendLine("	        MontoExento,");
			SQL.AppendLine("	        MontoGravable,");
			SQL.AppendLine("	        MontoIVA,");
			SQL.AppendLine("	        MontoRetencion,");
			SQL.AppendLine("	        AplicaParaLibroDeCompras,");
			SQL.AppendLine("	        ObservacionesCxP,");
			SQL.AppendLine("	        GeneradaPor,");
			SQL.AppendLine("	        AplicaIvaAlicuotaEspecial,");
			SQL.AppendLine("	        MontoGravableAlicuotaEspecial1,");
			SQL.AppendLine("	        MontoIVAAlicuotaEspecial1,");
			SQL.AppendLine("	        PorcentajeIvaAlicuotaEspecial1,");
			SQL.AppendLine("	        MontoGravableAlicuotaEspecial2,");
			SQL.AppendLine("	        MontoIVAAlicuotaEspecial2,");
			SQL.AppendLine("	        PorcentajeIvaAlicuotaEspecial2");
		    SQL.AppendLine("	    FROM OPENXML( @hdoc, 'GpData/GpResult/GpDataDetalleDeRendicion/GpDetailDetalleDeRendicion',2) ");
            SQL.AppendLine("	    WITH (");
            SQL.AppendLine("	        ConsecutivoRenglon " + InsSql.NumericTypeForDb(10, 0) + ",");
            SQL.AppendLine("	        NumeroDocumento " + InsSql.VarCharTypeForDb(25) + ",");
            SQL.AppendLine("	        NumeroControl " + InsSql.VarCharTypeForDb(20) + ",");
            SQL.AppendLine("	        Fecha " + InsSql.DateTypeForDb() + ",");
            SQL.AppendLine("	        CodigoProveedor " + InsSql.VarCharTypeForDb(10) + ",");
            SQL.AppendLine("	        MontoExento " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        MontoGravable " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        MontoIVA " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        MontoRetencion " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        AplicaParaLibroDeCompras " + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("	        ObservacionesCxP " + InsSql.VarCharTypeForDb(50) + ",");
            SQL.AppendLine("	        GeneradaPor " + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("	        AplicaIvaAlicuotaEspecial " + InsSql.CharTypeForDb(1) + ",");
            SQL.AppendLine("	        MontoGravableAlicuotaEspecial1 " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        MontoIVAAlicuotaEspecial1 " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        PorcentajeIvaAlicuotaEspecial1 " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        MontoGravableAlicuotaEspecial2 " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        MontoIVAAlicuotaEspecial2 " + InsSql.DecimalTypeForDb(25, 4) + ",");
            SQL.AppendLine("	        PorcentajeIvaAlicuotaEspecial2 " + InsSql.DecimalTypeForDb(25, 4) + ") AS XmlDocDetailOfRendicion");
            SQL.AppendLine("	    EXEC sp_xml_removedocument @hdoc");
            SQL.AppendLine("	    SET @ReturnValue = @@ROWCOUNT");
            SQL.AppendLine("	    RETURN @ReturnValue");
            SQL.AppendLine("      END");
            SQL.AppendLine("      ELSE");
            SQL.AppendLine("         RAISERROR('El registro ha sido modificado o eliminado por otro usuario.', 14, 1)");
	        SQL.AppendLine("	END");
	        SQL.AppendLine("	ELSE");
            SQL.AppendLine("	    RETURN -1");
            SQL.AppendLine("END");
            return SQL.ToString();
        }
        #endregion //Queries
        bool CrearTabla() {
            bool vResult = insDbo.Create(DbSchema + ".DetalleDeRendicion", SqlCreateTable(), false, eDboType.Tabla);
            return vResult;
        }
        bool CrearVistas(){
            bool vResult = false;
            LibViews insVistas = new LibViews();
            vResult = insVistas.Create(DbSchema + ".Gv_DetalleDeRendicion_B1", SqlViewB1(), true);
            insVistas.Dispose();
            return vResult;
        }
        bool CrearProcedimientos() {
            bool vResult = false;
            LibStoredProc insSps = new LibStoredProc();
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_DetalleDeRendicionINS", SqlSpInsParameters(), SqlSpIns(), true);
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_DetalleDeRendicionUPD", SqlSpUpdParameters(), SqlSpUpd(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_DetalleDeRendicionDEL", SqlSpDelParameters(), SqlSpDel(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_DetalleDeRendicionGET", SqlSpGetParameters(), SqlSpGet(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_DetalleDeRendicionSelDet", SqlSpSelDetailParameters(), SqlSpSelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_DetalleDeRendicionDelDet", SqlSpDelDetailParameters(), SqlSpDelDetail(), true) && vResult;
            vResult = insSps.CreateStoredProcedure(DbSchema + ".Gp_DetalleDeRendicionInsDet", SqlSpInsDetailParameters(), SqlSpInsDetail(), true) && vResult;
            insSps.Dispose();
            return vResult;
        }
        public bool InstalarTabla() {
            bool vResult = false;
            if (CrearTabla()) {
                //CrearVistas();
                //CrearProcedimientos();
                vResult = true;
            }
            return vResult;
        }

        public bool InstalarVistasYSps() {
            bool vResult = false;
            if (insDbo.Exists(DbSchema + ".DetalleDeRendicion", eDboType.Tabla)) {
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
            vResult = insSp.Drop(DbSchema + ".Gp_DetalleDeRendicionINS");
            vResult = insSp.Drop(DbSchema + ".Gp_DetalleDeRendicionUPD") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_DetalleDeRendicionDEL") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_DetalleDeRendicionGET") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_DetalleDeRendicionInsDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_DetalleDeRendicionDelDet") && vResult;
            vResult = insSp.Drop(DbSchema + ".Gp_DetalleDeRendicionSelDet") && vResult;
            vResult = insVista.Drop(DbSchema + ".Gv_DetalleDeRendicion_B1") && vResult;
            insSp.Dispose();
            insVista.Dispose();
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsDetalleDeRendicionED

} //End of namespace Galac.Saw.Dal.Rendicion

