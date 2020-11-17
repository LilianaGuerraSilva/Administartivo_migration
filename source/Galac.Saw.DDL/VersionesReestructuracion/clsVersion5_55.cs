using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using System.Data;
using LibGalac.Aos.Dal;


namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_55 : clsVersionARestructurar {
         public clsVersion5_55(string valCurrentDataBaseName):base(valCurrentDataBaseName){
            _VersionDataBase="5.55";
        }


        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            EjecutaProcesoDeMigracionAOSTablaRetencionYTarifaN2();
            CreaObjetosDeControlDespacho();
            ActualizarCodigoRetencionEmpresasQueNoRetienen();
            AumentarCaracteresNumeroCxP();
            BorrarFuncionesDeBDD();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void BorrarFuncionesDeBDD() {
           new LibDbo().Drop("dbo.Gf_ProveedorCanDel", eDboType.Funcion);
           new LibDbo().Drop("dbo.Gf_S10_ProveedorExists", eDboType.Funcion);
        }

        private bool EjecutaProcesoDeMigracionAOSTablaRetencionYTarifaN2() {
            new LibDbo().Drop("dbo.TablaRetencion", eDboType.Tabla);
            if (!TableExists("Comun.TablaRetencion")) {
                clsCrearDatabase insBd = new clsCrearDatabase();
                insBd.CrearTablaRetencion();

            }
            if (!TableExists("Comun.TarifaN2")) {
                clsCrearDatabase insBd = new clsCrearDatabase();
                insBd.CrearTarifaN2();
            }
            return true;
        }

        private bool CreaObjetosDeControlDespacho() {
            AgregarNuevoParametro("UsaControlDespacho", "Cotización", 3, "3.1.- Cotización ", 1, "", '2', "", 'N', "0");
            if (!ColumnExists("Factura", "NoControlDespachoDeOrigen")) {
                AddColumnString("Factura", "NoControlDespachoDeOrigen", 30, "", "");
            }
            if (!TableExists("ControlDespacho")) {
                CrearTablaControlDespacho();
            }
            if (!TableExists("DetalleDeControlDespacho")) {
                CrearTablaDetalleDeControlDespacho();
            }
            return true;
        }


        private void CrearTablaControlDespacho() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(InsSql.CreateTable("ControlDespacho", "") + " (");
            vSql.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + "CONSTRAINT nnControlDespachoConsecutiv NOT NULL");
            vSql.AppendLine(", Numero" + InsSql.VarCharTypeForDb(30) + "CONSTRAINT nnControlDespachoNumero NOT NULL");
            vSql.AppendLine(", NumeroCotizacion" + InsSql.VarCharTypeForDb(11) + "CONSTRAINT nnControlDespachoNumeroCoti NOT NULL");
            vSql.AppendLine(", CodigoCliente" + InsSql.VarCharTypeForDb(10) + "CONSTRAINT nnControlDespachoCodigoClie NOT NULL");
            vSql.AppendLine(", FechaApertura" + InsSql.DateTypeForDb());
            vSql.AppendLine(", FechaCierre" + InsSql.DateTypeForDb());
            vSql.AppendLine(", StatusControl" + InsSql.CharTypeForDb(1));
            vSql.AppendLine(", Observaciones" + InsSql.VarCharTypeForDb(255));
            vSql.AppendLine(", NombreOperador" + InsSql.VarCharTypeForDb(10));
            vSql.AppendLine(", FechaUltimaModificacion" + InsSql.DateTypeForDb());
            vSql.AppendLine(", fldTimeStamp" + InsSql.TimeStampTypeForDb());
            vSql.AppendLine(", CONSTRAINT p_ControlDespacho PRIMARY KEY CLUSTERED (ConsecutivoCompania, Numero)");
            vSql.AppendLine(", CONSTRAINT fk_ControlDespachoCoti FOREIGN KEY (ConsecutivoCompania, NumeroCotizacion) REFERENCES Cotizacion(ConsecutivoCompania, Numero) ON UPDATE CASCADE");
            vSql.AppendLine(", CONSTRAINT u_ControlDespachoNumero UNIQUE NONCLUSTERED (ConsecutivoCompania, Numero)");
            vSql.AppendLine(")");
            Execute(vSql.ToString(), -1);
        }

        private void CrearTablaDetalleDeControlDespacho() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(InsSql.CreateTable("DetalleDeControlDespacho", "") + " (");
            vSql.AppendLine("ConsecutivoCompania" + InsSql.NumericTypeForDb(10, 0) + "CONSTRAINT nnDetalleDeControlDespachoConsecutiv NOT NULL");
            vSql.AppendLine(", NumeroControlDespacho" + InsSql.VarCharTypeForDb(30) + "CONSTRAINT nnDetalleDeControlDespachoNumero NOT NULL");
            vSql.AppendLine(", ConsecutivoRenglon" + InsSql.NumericTypeForDb(9, 0) + "CONSTRAINT nnDetalleDeControlDespachoConsecutiv NOT NULL");
            vSql.AppendLine(", CodigoArticulo" + InsSql.VarCharTypeForDb(30) + "CONSTRAINT nnDetalleDeControlDespachoCodigoArti NOT NULL");
            vSql.AppendLine(", DescripcionArticulo" + InsSql.VarCharTypeForDb(7000) + "CONSTRAINT nnDetalleDeControlDespachoDescripcio NOT NULL");
            vSql.AppendLine(", CantidadDespachada" + InsSql.NumericTypeForDb(12, 2) + "CONSTRAINT nnDetalleDeControlDespachoCantidadDe NOT NULL");
            vSql.AppendLine(", NumeroEmpaque" + InsSql.VarCharTypeForDb(40) + "CONSTRAINT nnDetalleDeControlDespachoNumeroEmpa NOT NULL");
            vSql.AppendLine(", FechaDeEmpaque" + InsSql.DateTypeForDb());
            vSql.AppendLine(", fldTimeStamp " + InsSql.TimeStampTypeForDb());
            vSql.AppendLine(", CONSTRAINT p_DetalleDeControlDespacho PRIMARY KEY CLUSTERED (ConsecutivoCompania, NumeroControlDespacho, ConsecutivoRenglon)");
            vSql.AppendLine(", CONSTRAINT fk_ControlDespachoArti FOREIGN KEY (ConsecutivoCompania, CodigoArticulo) REFERENCES ArticuloInventario(ConsecutivoCompania, Codigo) ON UPDATE CASCADE");
            vSql.AppendLine(", CONSTRAINT fk_ControlDespachoCont FOREIGN KEY (ConsecutivoCompania, NumeroControlDespacho) REFERENCES ControlDespacho(ConsecutivoCompania, Numero) ON DELETE CASCADE");
            vSql.AppendLine(")");
            Execute(vSql.ToString(), -1);
        }

        private void ActualizarCodigoRetencionEmpresasQueNoRetienen() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Adm.Proveedor SET Adm.Proveedor.CodigoRetencionUsual = 'NORET' ");
            vSql.AppendLine(" FROM  Adm.Proveedor INNER JOIN Comun.SettValueByCompany ");
            vSql.AppendLine(" ON (Adm.Proveedor.ConsecutivoCompania = Comun.SettValueByCompany.ConsecutivoCompania) ");
            vSql.AppendLine(" WHERE NameSettDefinition = 'EnDondeRetenerISLR' ");
            vSql.AppendLine(" AND Value = '0' ");
            Execute(vSql.ToString(), -1);
        }

        private bool AumentarCaracteresNumeroCxP() {
           ExecuteDropConstraint("dbo.cxP", "u_cxPNumeroCodigoProveedor", true);
           ModifyLengthOfColumnString("dbo.cxP", "Numero", 25, "Not Null");
           AddUniqueKey("dbo.cxP", "ConsecutivoCompania,Numero,CodigoProveedor", "cxPNumeroCodigoProveedor");
           ModifyLengthOfColumnString("dbo.DetalleDeConciliacion", "NumeroDocumento", 25, "Not Null");
           ModifyLengthOfColumnString("dbo.DocumentoPagado", "NumeroDelDocumentoPagado", 25, "Not Null");
           ModifyLengthOfColumnString("dbo.Pago", "NumeroCheque", 25, "Not Null");
           ModifyLengthOfColumnString("dbo.RetPago", "NumeroReferencia", 25, "Not Null");
           ModifyLengthOfColumnString("dbo.RetDocumentoPagado", "NumeroDelDocumento", 25, "");
           ModifyLengthOfColumnString("dbo.MovimientoBancario", "NumeroDocumento", 25, "");
           if(TableHasPrimaryKey("dbo.OPFalsoRetencion")) {
              DeletePrimaryKeyEnRevision(_CurrentDataBaseName, "OPFalsoRetencion");
           };
           ModifyLengthOfColumnString("dbo.OPFalsoRetencion", "NumeroPago", 25, "Not Null");
           ModifyLengthOfColumnString("dbo.OPFalsoRetencion", "NumeroDelDocumentoPagado", 25, "");
           if(!TableHasPrimaryKey("dbo.OPFalsoRetencion")) {
              AddPrimaryKeyEnRevision("dbo.OPFalsoRetencion", "ConsecutivoCompania,NumeroPago,CodigoRetencion");
           };
           return true;
        }
		
        private bool AddPrimaryKeyEnRevision(string valTableName, string valListOfFields) {
           bool vResult = false;
           string vName;
           string vOwner;
           LibDbo.SplitObjectNameInNameAndOwner(valTableName, out vName, out vOwner);
           Execute("ALTER TABLE " + vOwner + "." + vName + " ADD CONSTRAINT p_" + vName + " PRIMARY KEY(" + valListOfFields + ")");
           vResult = true;
           return vResult;
        }
		
        private System.Data.Common.DbParameter GetInputParamStringEnRevision(string valParamName, string valParamValue, int valSize) {
           System.Data.Common.DbParameter vParam = new QAdvDb().GetCurrentFactory().CreateParameter();
           vParam.ParameterName = valParamName;
           vParam.Direction = ParameterDirection.Input;
           vParam.DbType = DbType.String;
           vParam.Size = valSize;
           vParam.Value = valParamValue;
           return vParam;
        }

        private DataTable DbTableSchemaEnRevision() {
            DataTable vResult = null;
            //string vSql = " EXEC sp_tables  @table_type = \"'TABLE'\" ";
            List<System.Data.Common.DbParameter> vParameters = new List<System.Data.Common.DbParameter>();
            vParameters.Add(GetInputParamStringEnRevision("@table_type", "'TABLE'", 100));
            DataSet vDs = ExecuteSp("sp_tables", vParameters, -1);
            if ((vDs != null) && (vDs.Tables != null) && (vDs.Tables.Count > 0)) {
               vResult = vDs.Tables[0];
            }
            return vResult;
        }

        private bool DeletePrimaryKeyEnRevision(string valDataBaseName, string valTableName) {
           bool vResult;
           string vPkName;
           string vCurrentTN;
           System.Data.DataTable vDt;
           vResult = false;
           vPkName = PrimaryKeyConstraintName(valTableName);
           if(LibString.Len(vPkName) > 0) {
              vDt = DbTableSchemaEnRevision();
              if(LibDataTable.DataTableHasRows(vDt)) {
                 for(int vIndex = 0; vIndex < vDt.Rows.Count; vIndex++) {
                    vCurrentTN = vDt.Rows[vIndex]["TABLE_NAME"].ToString();
                    if(!LibString.S1IsEqualToS2(valTableName, vCurrentTN)) {
                       DeleteAllrelationShipsBetweenTables(valDataBaseName, valTableName, vCurrentTN);
                    }
                 }
                 ExecuteDropConstraint(valTableName, vPkName, false);
              }
           }
           vResult = (LibString.Len(PrimaryKeyConstraintName(valTableName)) == 0);
           return vResult;
        }

    }
}
