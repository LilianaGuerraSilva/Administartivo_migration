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
using LibGalac.Aos.DefGen;
using Galac.Saw.Dal.Tablas;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_78 : clsVersionARestructurar {
        public clsVersion6_78(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            CrearAuditoriaConfiguracion();
            CrearParametroUsaMaquinaFiscal();
            AjustarParametroUsaCobroDirecto();
            LimpiaParametroAccionAlAnularFactDeMesesAnt();
            CorregirInconsistenciasEnCajasQueNoUtilizanMF();
            DisposeConnectionNoTransaction();
            return true;
        }
        private void CrearAuditoriaConfiguracion() {
            new clsAuditoriaConfiguracionED().InstalarTabla();
        }
        private void CrearParametroUsaMaquinaFiscal() {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");

            AgregarNuevoParametro("UsaMaquinaFiscal", "Factura", 2, "2.2.- Facturación (Continuación)", 1, "", eTipoDeDatoParametros.String, "", 'N', "N");

            vSql.AppendLine("UPDATE Comun.SettValueByCompany");
            vSql.AppendLine(" SET Value = " + insSql.ToSqlValue("S"));
            vSql.AppendLine(" WHERE NameSettDefinition LIKE " + insSql.ToSqlValue("UsaMaquinaFiscal"));
            vSql.AppendLine(" AND ConsecutivoCompania IN ");
            vSql.AppendLine(" (SELECT DISTINCT ConsecutivoCompania FROM Adm.Caja ");
            vSql.AppendLine(" WHERE UsaMaquinaFiscal = " + insSql.ToSqlValue("S") + ")");
            Execute(vSql.ToString(), -1);
        }

        private void AjustarParametroUsaCobroDirecto() {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");

            vSql.AppendLine("UPDATE Comun.SettValueByCompany");
            vSql.AppendLine(" SET Value = " + insSql.ToSqlValue("S"));
            vSql.AppendLine(" WHERE NameSettDefinition LIKE " + insSql.ToSqlValue("UsaCobroDirecto"));
            vSql.AppendLine(" AND ConsecutivoCompania IN ");
            vSql.AppendLine(" (SELECT DISTINCT ConsecutivoCompania FROM Adm.Caja ");
            vSql.AppendLine(" WHERE UsaMaquinaFiscal = " + insSql.ToSqlValue("S") + ")");
            Execute(vSql.ToString(), -1);
        }
        private void LimpiaParametroAccionAlAnularFactDeMesesAnt() {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");

            vSql.AppendLine("UPDATE Comun.SettValueByCompany");
            vSql.AppendLine(" SET Value = " + insSql.ToSqlValue("2"));
            vSql.AppendLine(" WHERE NameSettDefinition LIKE " + insSql.ToSqlValue("AccionAlAnularFactDeMesesAnt"));
            Execute(vSql.ToString(), -1);
        }

        private void CorregirInconsistenciasEnCajasQueNoUtilizanMF() {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");

            vSql.AppendLine("INSERT INTO Adm.AuditoriaConfiguracion ");
            vSql.AppendLine(" (ConsecutivoCompania, ConsecutivoAuditoria, VersionPrograma, FechayHora, Accion, Motivo, ConfiguracionOriginal, ConfiguracionNueva, NombreOperador, FechaUltimaModificacion)");
            vSql.AppendLine(" SELECT ConsecutivoCompania, ROW_NUMBER() OVER (PARTITION BY ConsecutivoCompania ORDER BY ConsecutivoCompania ASC), " + insSql.ToSqlValue("26.3") + ", ");
            vSql.AppendLine(_TodayAsSqlValue + ", " + insSql.ToSqlValue("REESTRUCTURACION") + ", " + insSql.ToSqlValue("Corrección Configuración Inicial") + ", ");
            vSql.AppendLine(insSql.ToSqlValue("NombreCaja: ") + "+ NombreCaja + " + insSql.ToSqlValue("; UsaMaquinaFiscal: N; Serial: ") + "+ SerialDeMaquinaFiscal, ");
            vSql.AppendLine(insSql.ToSqlValue("Serial: ") + ", ");
            vSql.AppendLine(" NombreOperador, ");
            vSql.AppendLine(_TodayAsSqlValue);
            vSql.AppendLine(" FROM Adm.Caja");
            vSql.AppendLine(" WHERE UsaMaquinaFiscal = " + insSql.ToSqlValue("N"));
            vSql.AppendLine(" AND SerialDeMaquinaFiscal <> " + insSql.ToSqlValue(""));
            Execute(vSql.ToString(), -1);
            vSql.Clear();

            vSql.AppendLine("UPDATE Adm.Caja");
            vSql.AppendLine(" SET SerialDeMaquinaFiscal = " + insSql.ToSqlValue(""));
            vSql.AppendLine(" WHERE UsaMaquinaFiscal = " + insSql.ToSqlValue("N"));
            vSql.AppendLine(" AND SerialDeMaquinaFiscal <> " + insSql.ToSqlValue(""));
            Execute(vSql.ToString(), -1);
        }

    }
}
