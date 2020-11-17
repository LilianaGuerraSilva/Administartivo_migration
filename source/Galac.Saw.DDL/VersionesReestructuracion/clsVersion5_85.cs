using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal.Settings;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_85 : clsVersionARestructurar {
        public clsVersion5_85(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.85";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarParametrosBaseDeCalculoParaAlicuotaEspecialEImprimirMensajeAplicacionDecreto();
            ActualizarParametroBaseDeCalculoParaAlicuotaEspecial();
            EliminarParametroConsiderarBaseImponibleAlAplicarAlicuota();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarParametrosBaseDeCalculoParaAlicuotaEspecialEImprimirMensajeAplicacionDecreto() {
            AgregarNuevoParametro("BaseDeCalculoParaAlicuotaEspecial", "DatosGenerales", 1, "1.1.-Compania", 1, "", '0', "", 'N',"3");
            AgregarNuevoParametro("ImprimirMensajeAplicacionDecreto", "DatosGenerales", 1, "1.1.-Compania", 1, "", '2', "", 'N', "N");            
        }

        private void ActualizarParametroBaseDeCalculoParaAlicuotaEspecial() {
            StringBuilder vSql = new StringBuilder();
            QAdvSql vSqlBuilder = new QAdvSql("");
            if (RecordCountOfSql("SELECT Name FROM Comun.SettDefinition WHERE Name = 'ConsiderarBaseImponibleAlAplicarAlicuota'") > 0) {
                vSql.Clear();
                vSql.AppendLine("UPDATE A SET A.Value = B.Value FROM Comun.SettValueByCompany A ");
                vSql.AppendLine("CROSS JOIN (SELECT (CASE WHEN Value = " +  vSqlBuilder.ToSqlValue("S") + " THEN " + vSqlBuilder.ToSqlValue("1"));
                vSql.AppendLine(" ELSE " + vSqlBuilder.ToSqlValue("0") + " END) AS Value, ConsecutivoCompania FROM Comun.SettValueByCompany");
                vSql.AppendLine(" WHERE NameSettDefinition  = " + vSqlBuilder.ToSqlValue("ConsiderarBaseImponibleAlAplicarAlicuota") + ") B");
                vSql.AppendLine(" WHERE A.NameSettDefinition  = " + vSqlBuilder.ToSqlValue("BaseDeCalculoParaAlicuotaEspecial") + " AND A.ConsecutivoCompania = B.ConsecutivoCompania");
                Execute(vSql.ToString(), -1);
            }
        }

        private void EliminarParametroConsiderarBaseImponibleAlAplicarAlicuota() {
            string vSql = string.Empty;
            if (TableExists("Comun.SettValueByCompany")) {
                vSql = "DELETE FROM Comun.SettValueByCompany WHERE NameSettDefinition = 'ConsiderarBaseImponibleAlAplicarAlicuota' ";
                Execute(vSql, 0);
            }

            if (TableExists("Comun.SettDefinition")) {
                vSql = "DELETE FROM [Comun].[SettDefinition] WHERE Name = 'ConsiderarBaseImponibleAlAplicarAlicuota' ";
                Execute(vSql, 0);
            }
        }

    }
}
