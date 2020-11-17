using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal.PASOnLine;
using LibGalac.Aos.Dal.Settings;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_84 : clsVersionARestructurar {
        public clsVersion5_84(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.84";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            ActualizarTablaCxP();
            ActualizaEstatusAlicuotaEspecial();
            AgregarParametrosBaseImponibleAlAplicarAlicuota();
            DisposeConnectionNoTransaction();
            return true;
        }


        public void ActualizarTablaCxP() {
            if (!ColumnExists("CxP", "AplicaIvaAlicuotaEspecial")) {
                AddColumnBoolean("CxP", "AplicaIvaAlicuotaEspecial", "CONSTRAINT AplicaIvaA NOT NULL", false);
            }
            if (!ColumnExists("CxP", "MontoGravableAlicuotaEspecial1")) {
                AddColumnCurrency("CxP", "MontoGravableAlicuotaEspecial1", "MontoGravableAlicuotaEspecial1 NULL", 0);
            }
            if (!ColumnExists("CxP", "MontoIVAAlicuotaEspecial1")) {
                AddColumnCurrency("CxP", "MontoIVAAlicuotaEspecial1", "MontoIVAAlicuotaEspecial1 NULL", 0);
            }
            if (!ColumnExists("CxP", "PorcentajeIvaAlicuotaEspecial1")) {
                AddColumnCurrency("CxP", "PorcentajeIvaAlicuotaEspecial1", "PorcentajeIvaAlicuotaEspecial1 NULL", 0);
            }
            if (!ColumnExists("CxP", "MontoGravableAlicuotaEspecial2")) {
                AddColumnCurrency("CxP", "MontoGravableAlicuotaEspecial2", "MontoGravableAlicuotaEspecial2 NULL", 0);
            }
            if (!ColumnExists("CxP", "MontoIVAAlicuotaEspecial2")) {
                AddColumnCurrency("CxP", "MontoIVAAlicuotaEspecial2", "MontoIVAAlicuotaEspecial2 NULL", 0);
            }
            if (!ColumnExists("CxP", "PorcentajeIvaAlicuotaEspecial2")) {
                AddColumnCurrency("CxP", "PorcentajeIvaAlicuotaEspecial2", "PorcentajeIvaAlicuotaEspecial2 NULL", 0);
            }
        }

        private void ActualizaEstatusAlicuotaEspecial() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Comun.AlicuotaImpuestoEspecial SET Estatus = " + InsSql.ToSqlValue("0"));
            vSql.AppendLine("WHERE Codigo = " + InsSql.ToSqlValue("000002") + " AND Estatus = " + InsSql.ToSqlValue("1"));
            Execute(vSql.ToString(), 0);
            vSql.Clear();
            vSql.AppendLine("UPDATE Comun.AlicuotaImpuestoEspecial SET Estatus = " + InsSql.ToSqlValue("0"));
            vSql.AppendLine("WHERE Codigo = " + InsSql.ToSqlValue("000003") + " AND Estatus = " + InsSql.ToSqlValue("1"));
            Execute(vSql.ToString(), 0);
        }

        private void AgregarParametrosBaseImponibleAlAplicarAlicuota() {
            AgregarNuevoParametro("ConsiderarBaseImponibleAlAplicarAlicuota", "DatosGenerales", 1, "1.1.-Compania", 1, "", '2', "", 'N', "N");
        }

    }
}
