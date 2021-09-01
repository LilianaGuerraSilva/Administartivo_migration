using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using System.Text;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using LibGalac.Aos.Base.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_20 : clsVersionARestructurar {

        public clsVersion6_20(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.20";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarNuevosCamposEnFactura();
            ModificarLongitudNoCotizacionOrigen();
            CrearNuevosCamposWinCont();
            AgregarTipoDeComprobante();
            ActualizarParametroUsaMEEnParametrosConciliacion();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarNuevosCamposEnFactura() {
            AddColumnEnumerative("dbo.factura", "GeneradoPor", "", 0);
        }

        private void ModificarLongitudNoCotizacionOrigen() {
            AlterColumnIfExist("dbo.factura", "NoCotizacionDeOrigen", InsSql.VarCharTypeForDb(20), "", "");
        }

        private void CrearNuevosCamposWinCont() {
            AddColumnBoolean("Cuenta", "EsMonedaExtranjera", "", false);            
            if (AddColumnString("Cuenta", "CodigoMoneda", 4, "", "VES")) {
                AddForeignKey("Moneda", "Cuenta", new string[] { "Codigo" }, new string[] { "CodigoMoneda" }, false);
            }
            AddColumnBoolean("Asiento", "EsAsientoDiferenciaCambiaria", "", false);
            AddColumnCurrency("Asiento", "TasaDeCambio", "", 0);
            AddColumnBoolean("Contab.ParametrosConciliacion", "UsaMonedaExtranjera", "", false);
            AddColumnString("Periodo", "GananciaPerdidaCambiaria", 30, "", "");
            AddColumnEnumerative("Periodo", "GenerarComprobanteDeDiferenciaCambiaria", "", 1); //DV=Anual=1
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