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

namespace Galac.Saw.DDL.VersionesReestructuracion {

	class clsVersionTemporalNoOficial : clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
			AgregarColumnasEnCompania();
			TrasladarDatosImprentaDigitalACompania();
			DisposeConnectionNoTransaction();
			return true;
		}

		private void AgregarColumnasEnCompania() {
			AddColumnString("Compania", "ImprentaDigitalUrl", 500, "", "");
			AddColumnString("Compania", "ImprentaDigitalNombreCampoUsuario", 50, "", "");
			AddColumnString("Compania", "ImprentaDigitalNombreCampoClave", 50, "", "");
			AddColumnString("Compania", "ImprentaDigitalUsuario", 100, "", "");
			AddColumnString("Compania", "ImprentaDigitalClave", 500, "", "");
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
	}
}
