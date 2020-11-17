using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_81 : clsVersionARestructurar {
        public clsVersion5_81(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.81";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            ModificarFechaDecretoEspecial();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void ModificarFechaDecretoEspecial() {
            StringBuilder vSql = new StringBuilder();
            QAdvSql vQAdvSQL = new QAdvSql("");

            string vFechaInicio = "";
            string vFechaFin = "";
            vFechaInicio = new DateTime(2017, 09, 11).ToString("yyyy-MM-dd HH:mm:ss");
            vFechaFin = new DateTime(2018, 12, 31).ToString("yyyy-MM-dd HH:mm:ss");

            vSql.AppendLine("UPDATE Comun.SettValueByCompany");
            vSql.AppendLine(" SET Value = " + vQAdvSQL.ToSqlValue(vFechaInicio) );
            vSql.AppendLine(" WHERE Comun.SettValueByCompany.NameSettDefinition = " + vQAdvSQL.ToSqlValue("FechaInicioAlicuotaIva10Porciento"));
            Execute(vSql.ToString(), -1);
            vSql.Clear();

            vSql.AppendLine("UPDATE Comun.SettValueByCompany");
            vSql.AppendLine(" SET Value = " + vQAdvSQL.ToSqlValue(vFechaFin));
            vSql.AppendLine(" WHERE Comun.SettValueByCompany.NameSettDefinition = " + vQAdvSQL.ToSqlValue("FechaFinAlicuotaIva10Porciento"));
            Execute(vSql.ToString(), -1);
        
        }
    }
}
