﻿using System.Text;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {

    class clsVersionTemporalNoOficial:clsVersionARestructurar {
        public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            CrearNuevosCamposWinCont();
            AgregarTipoDeComprobante();
            ActualizarParametroUsaMEEnParametrosConciliacion();
            DisposeConnectionNoTransaction();            
            return true;
        }

        private void CrearNuevosCamposWinCont() {
            if (!ColumnExists("Cuenta", "EsMonedaExtranjera")) {
                AddColumnBoolean("Cuenta", "EsMonedaExtranjera", "", false);
            }
            if (!ColumnExists("Cuenta", "CodigoMoneda")) {
                AddColumnString("Cuenta", "CodigoMoneda", 4, "", "VES");
                AddForeignKey("Moneda", "Cuenta", new string[] { "Codigo" }, new string[] { "CodigoMoneda" }, false);
            }
            if (!ColumnExists("Asiento", "EsAsientoDiferenciaCambiaria")) {
                AddColumnBoolean("Asiento", "EsAsientoDiferenciaCambiaria", "", false);
            }
            if (!ColumnExists("Asiento", "TasaDeCambio")) {
                AddColumnCurrency("Asiento", "TasaDeCambio", "", 0);
            }
            if (!ColumnExists("Contab.ParametrosConciliacion", "UsaMonedaExtranjera")) {
                AddColumnBoolean("Contab.ParametrosConciliacion", "UsaMonedaExtranjera", "", false);
            }
            if (!ColumnExists("Periodo", "GananciaPerdidaCambiaria")) {
                AddColumnString("Periodo", "GananciaPerdidaCambiaria", 30, "", "");
            }
            if (!ColumnExists("Periodo", "GenerarComprobanteDeDiferenciaCambiaria")) {
                AddColumnEnumerative("Periodo", "GenerarComprobanteDeDiferenciaCambiaria", "", "0");
            }
        }

        private void AgregarTipoDeComprobante() {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");
            vSql.AppendLine("SET DATEFORMAT DMY ");
            vSql.AppendLine("IF NOT EXISTS (SELECT * FROM Contab.TipoDeComprobante WHERE Codigo='9Z' AND Nombre = 'Ganancia/Pérdida Cambiaria')");
            vSql.AppendLine("BEGIN");
            vSql.AppendLine("INSERT INTO Contab.TipoDeComprobante(Codigo,Nombre,NombreOperador,FechaUltimaModificacion,fldOrigen)");
            vSql.AppendLine("VALUES ('9Z','Ganancia/Pérdida Cambiaria','JEFE'," + insSql.ToSqlValue(LibDate.Today()) + ",'0')");
            vSql.AppendLine("END");
            Execute(vSql.ToString(), 0);
        }

        private void ActualizarParametroUsaMEEnParametrosConciliacion() {
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE tbParametrosConciliacion ");
            vSql.AppendLine("SET tbParametrosConciliacion.UsaMonedaExtranjera = tbSettValueByCompany.Value ");
            vSql.AppendLine("FROM Contab.ParametrosConciliacion tbParametrosConciliacion ");
            vSql.AppendLine("INNER JOIN Comun.SettValueByCompany tbSettValueByCompany ON ");
            vSql.AppendLine("tbParametrosConciliacion.ConsecutivoCompania =tbSettValueByCompany.ConsecutivoCompania ");
            vSql.AppendLine("INNER JOIN COMPANIA tbCOMPANIA ON ");
            vSql.AppendLine("tbParametrosConciliacion.ConsecutivoCompania = tbCOMPANIA.ConsecutivoCompania ");
            vSql.AppendLine("WHERE tbSettValueByCompany.NameSettDefinition = 'UsaMonedaExtranjera' ");
            vSql.AppendLine("AND tbCOMPANIA.UsaModuloDeContabilidad=" + insSql.ToSqlValue(true));
            Execute(vSql.ToString(), 0);
        }
    }
}

