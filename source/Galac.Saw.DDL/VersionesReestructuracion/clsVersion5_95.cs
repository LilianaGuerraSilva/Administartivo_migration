using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal.Settings;
using LibGalac.Aos.Base;
using System.Threading;
using System.Data;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_95 : clsVersionARestructurar {
        public clsVersion5_95(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.95";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregaCampoDiaDeAplicacion();
            AjustarValoresDelCampoDiaAplicacionEnCxp();          
            DisposeConnectionNoTransaction();
            return true;
        }        

        private void AgregaCampoDiaDeAplicacion() {
            if (!ColumnExists("dbo.cxP", "DiaDeAplicacion")) {
                AddColumnInteger("dbo.cxP", "DiaDeAplicacion", "");
            }
        }

        private void AjustarValoresDelCampoDiaAplicacionEnCxp() {
           if (ColumnExists("dbo.cxP", "DiaDeAplicacion")) {
                StringBuilder vSQL = new StringBuilder();
                vSQL.AppendLine("SELECT ConsecutivoCompania, TipoDeContribuyenteIva FROM dbo.Compania");
                DataSet  ds = ExecuteDataset(vSQL.ToString(), -1);
                DataTableReader rd = ds.Tables[0].CreateDataReader();
                while (rd.Read()) {
                    AjustarEmpresas((int)rd[0], (string) rd[1]);
                }
            }
        }

        private void AjustarEmpresas(int valConsecutivoCompania, string  valTipoContribuyente) {
            switch (valTipoContribuyente) {
                case "2":
                    AjustarDiaAplicacionParaEspeciales(valConsecutivoCompania);
                break;
            default:
                    AjustarDiaAplicacionParaOrdinariosYFormales(valConsecutivoCompania);
                    break;
            }
        }

        private void AjustarDiaAplicacionParaOrdinariosYFormales(int valConsecutivoCompania) {
            if (ColumnExists("dbo.cxP", "DiaDeAplicacion")) {
                StringBuilder vSQL = new StringBuilder();
                vSQL.AppendLine("UPDATE dbo.cxP SET DiaDeAplicacion = 1");
                vSQL.AppendLine("WHERE ConsecutivoCompania = " +  valConsecutivoCompania.ToString());
                vSQL.AppendLine("AND (DiaDeAplicacion IS NULL OR DiaDeAplicacion = 0)");
                Execute(vSQL.ToString());
            }
        }

        private void AjustarDiaAplicacionParaEspeciales(int valConsecutivoCompania) {
            if (ColumnExists("dbo.cxP", "DiaDeAplicacion")) {
                StringBuilder vSQL = new StringBuilder();
                vSQL.AppendLine("UPDATE dbo.cxP SET DiaDeAplicacion = Day(Fecha)");
                vSQL.AppendLine("WHERE ConsecutivoCompania = " + valConsecutivoCompania.ToString());
                vSQL.AppendLine("AND (DiaDeAplicacion IS NULL OR DiaDeAplicacion = 0)");
                vSQL.AppendLine("AND (MesDeAplicacion = MONTH(Fecha))  AND (AnoDeAplicacion = YEAR(Fecha))");
                vSQL.AppendLine("AND SeHizoLaRetencionIva = " + InsSql.ToSqlValue(false));
                Execute(vSQL.ToString());
                vSQL.Clear();
                vSQL.AppendLine("UPDATE dbo.cxP SET DiaDeAplicacion = Day(FechaAplicacionRetIva)");
                vSQL.AppendLine("WHERE ConsecutivoCompania = " + valConsecutivoCompania.ToString());
                vSQL.AppendLine("AND (DiaDeAplicacion IS NULL OR DiaDeAplicacion = 0)");
                vSQL.AppendLine("AND SeHizoLaRetencionIva = " + InsSql.ToSqlValue(true));
                Execute(vSQL.ToString());
                vSQL.Clear();
                vSQL.AppendLine("UPDATE dbo.cxP SET DiaDeAplicacion = 1");
                vSQL.AppendLine("WHERE ConsecutivoCompania = " + valConsecutivoCompania.ToString());
                vSQL.AppendLine("AND (DiaDeAplicacion IS NULL OR DiaDeAplicacion = 0)");
                Execute(vSQL.ToString());
            }
        }      
    }
}
