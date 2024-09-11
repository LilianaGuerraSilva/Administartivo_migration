using System.Text;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Brl.Tablas;
using System.ComponentModel.DataAnnotations;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using System;
using System.Data;
using LibGalac.Aos.Cnf;
using Galac.Saw.Lib;
using LibGalac.Aos.DefGen;

namespace Galac.Saw.DDL.VersionesReestructuracion {

	class clsVersionTemporalNoOficial : clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
			AgregarColumnasEnCompania();
            CreacionDeParametros();
            CrearLoteDeInventario();
			AjustesNotaEntradaSalida();
            AjustesRenglonFactura();
            AjustesRenglonCompra();
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
			StringBuilder vSql=new StringBuilder();
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
                ConfigHelper.AddKeyToAppSettings("CLAVE-E",string.Empty);
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
    }
}
