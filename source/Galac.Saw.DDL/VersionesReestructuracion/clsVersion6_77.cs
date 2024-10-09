using Galac.Adm.Dal.Vendedor;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Lib;
using LibGalac.Aos.Cnf;
using System.Data;

namespace Galac.Saw.DDL.VersionesReestructuracion {
	class clsVersion6_77 : clsVersionARestructurar {
		public clsVersion6_77(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
            AgregarColumnasEnCompania();
            CreacionDeParametros();
            CrearLoteDeInventario();
            AjustesNotaEntradaSalida();
            AjustesRenglonFactura();
            AjustesRenglonCompra();
            AjustesRenglonConteoFisico();
            AjustesOrdenProduccion();
            DisposeConnectionNoTransaction();
			return true;
		}

        void CreacionDeParametros() {
            AgregarNuevoParametro("UsaLoteFechaDeVencimiento", "Inventario", 5, "5.1.- Inventario", 1, "", eTipoDeDatoParametros.String, "", 'N', "N");
        }
        void AjustesNotaEntradaSalida() {
            if (TableExists("dbo.RenglonNotaES")) {
                DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "dbo.NotaDeEntradaSalida", "dbo.RenglonNotaES");
                DeletePrimaryKey("dbo.RenglonNotaES");
                AddPrimaryKey("dbo.RenglonNotaES", "ConsecutivoCompania,NumeroDocumento,ConsecutivoRenglon");
                AddForeignKey("dbo.NotaDeEntradaSalida", "dbo.RenglonNotaES", new string[] { "ConsecutivoCompania,NumeroDocumento" }, new string[] { "ConsecutivoCompania,NumeroDocumento" }, true, true);

                AddColumnString("dbo.RenglonNotaES", "LoteDeInventario", 30, "", "");
            }
        }

        void CrearLoteDeInventario() {
            if (!TableExists("Saw.LoteDeInventario")) {
                new Galac.Saw.Dal.Inventario.clsLoteDeInventarioED().InstalarTabla();
            }
        }

        private void AgregarColumnasEnCompania() {
            if (AddColumnString("Compania", "ImprentaDigitalUrl", 500, "", "")) {
                AddDefaultConstraint("Compania", "ImDiUrl", "''", "ImprentaDigitalUrl");
            }
            if (AddColumnString("Compania", "ImprentaDigitalNombreCampoUsuario", 50, "", "")) {
                AddDefaultConstraint("Compania", "ImDiCUsr", "''", "ImprentaDigitalNombreCampoUsuario");
            }
            if (AddColumnString("Compania", "ImprentaDigitalNombreCampoClave", 50, "", "")) {
                AddDefaultConstraint("Compania", "ImDiCCla", "''", "ImprentaDigitalNombreCampoClave");
            }
            if (AddColumnString("Compania", "ImprentaDigitalUsuario", 100, "", "")) {
                AddDefaultConstraint("Compania", "ImDiUsr", "''", "ImprentaDigitalUsuario");
            }
            if (AddColumnString("Compania", "ImprentaDigitalClave", 500, "", "")) {
                AddDefaultConstraint("Compania", "ImDiClv", "''", "ImprentaDigitalClave");
            }
            TrasladarDatosImprentaDigitalACompania();
        }

        private void TrasladarDatosImprentaDigitalACompania() {
            int vConsecutivoCompania;
            StringBuilder vSql = new StringBuilder();
            vSql.Append("SELECT ConsecutivoCompania FROM Comun.SettValueByCompany WHERE NameSettDefinition ='UsaImprentaDigital' AND Value= " + _insSql.ToSqlValue(true));
            string vKeyValue = "";
            DataSet vDataSet = ExecuteDataset(vSql.ToString(), 0);
            vSql = new StringBuilder();
            if (vDataSet != null && vDataSet.Tables[0].Rows.Count > 0) {
                vConsecutivoCompania = LibConvert.ToInt(vDataSet.Tables[0].Rows[0]["ConsecutivoCompania"]);
                vKeyValue = LibAppSettings.ReadAppSettingsKey("DIRECCIONURL");
                vSql.AppendLine("UPDATE Compania SET ImprentaDigitalUrl= " + _insSql.ToSqlValue(vKeyValue));
                vKeyValue = LibAppSettings.ReadAppSettingsKey("CAMPOUSUARIO");
                vSql.AppendLine(", ImprentaDigitalNombreCampoUsuario= " + _insSql.ToSqlValue(vKeyValue));
                vKeyValue = LibAppSettings.ReadAppSettingsKey("CAMPOCLAVE");
                vSql.AppendLine(", ImprentaDigitalNombreCampoClave= " + _insSql.ToSqlValue(vKeyValue));
                vKeyValue = LibAppSettings.ReadAppSettingsKey("USUARIO");
                vSql.AppendLine(", ImprentaDigitalUsuario= " + _insSql.ToSqlValue(vKeyValue));
                vKeyValue = LibAppSettings.ReadAppSettingsKey("CLAVE-E");
                vSql.AppendLine(", ImprentaDigitalClave= " + _insSql.ToSqlValue(vKeyValue));
                vSql.AppendLine(" WHERE ConsecutivoCompania=" + _insSql.ToSqlValue(vConsecutivoCompania));
                Execute(vSql.ToString(), 0);
                //                
                ConfigHelper.AddKeyToAppSettings("DIRECCIONURL", string.Empty);
                ConfigHelper.AddKeyToAppSettings("CAMPOUSUARIO", string.Empty);
                ConfigHelper.AddKeyToAppSettings("CAMPOCLAVE", string.Empty);
                ConfigHelper.AddKeyToAppSettings("USUARIO", string.Empty);
                ConfigHelper.AddKeyToAppSettings("CLAVE", string.Empty);
                ConfigHelper.AddKeyToAppSettings("CLAVE-E", string.Empty);
            }
        }

        private void AjustesRenglonFactura() {
            AddColumnString("dbo.renglonFactura", "LoteDeInventario", 30, "", "");
        }

        private void AjustesRenglonCompra() {
            if (!ColumnExists("Adm.CompraDetalleArticuloInventario", "ConsecutivoLoteDeInventario")) {
                AddColumnInteger("Adm.CompraDetalleArticuloInventario", "ConsecutivoLoteDeInventario", "", 0);
                AddDefaultConstraint("Adm.CompraDetalleArticuloInventario", "d_ComDetArtInvCoLoIn", "0", "ConsecutivoLoteDeInventario");
            }
        }
        private void AjustesRenglonConteoFisico() {
            if (!ColumnExists("RenglonConteoFisico", "CodigoLote")) {
                AddColumnString("RenglonConteoFisico", "CodigoLote", 30, "", "");
            }
        }

        private void AjustesOrdenProduccion() {
            if (!ColumnExists("Adm.OrdenDeProduccionDetalleArticulo", "ConsecutivoLoteDeInventario")) {
                AddColumnInteger("Adm.OrdenDeProduccionDetalleArticulo", "ConsecutivoLoteDeInventario", "", 0);
                AddDefaultConstraint("Adm.OrdenDeProduccionDetalleArticulo", "d_OrdDeProDetArtCoLo", "0", "ConsecutivoLoteDeInventario");
            }
            if (!ColumnExists("Adm.OrdenDeProduccionDetalleMateriales", "ConsecutivoLoteDeInventario")) {
                AddColumnInteger("Adm.OrdenDeProduccionDetalleMateriales", "ConsecutivoLoteDeInventario", "", 0);
                AddDefaultConstraint("Adm.OrdenDeProduccionDetalleMateriales", "d_OrdDeProDetMatCoLo", "0", "ConsecutivoLoteDeInventario");
            }
            Execute("UPDATE Adm.OrdenDeProduccionDetalleArticulo  SET PorcentajeCostoEstimado = 100  WHERE PorcentajeCostoEstimado = 0");
            Execute("UPDATE Adm.OrdenDeProduccionDetalleArticulo  SET PorcentajeCostoCierre = 100    WHERE PorcentajeCostoCierre = 0");
        }
    }
}
