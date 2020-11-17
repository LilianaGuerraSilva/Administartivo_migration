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
    class clsVersion5_97 : clsVersionARestructurar {
        public clsVersion5_97(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.97";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            CorrigeValoresDiaDeAplicacionEnCxp();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void CorrigeValoresDiaDeAplicacionEnCxp() {
            StringBuilder vSQL = new StringBuilder();
            if (ColumnExists("dbo.cxP", "DiaDeAplicacion")) {
                vSQL.AppendLine("SELECT ConsecutivoCompania FROM dbo.Compania");
                vSQL.AppendLine("WHERE TipoDeContribuyenteIVA = " + InsSql.EnumToSqlValue(2));
                DataSet ds = ExecuteDataset(vSQL.ToString(), -1);
                DataTableReader rd = ds.Tables[0].CreateDataReader();
                while (rd.Read()) {
                    CorregirPorEmpresa((int)rd[0]);
                }
            }
            
        }

        private void CorregirPorEmpresa(int valConsecutivoCompania) {
            if (ColumnExists("dbo.cxP", "DiaDeAplicacion")) {
                StringBuilder vSQL = new StringBuilder();
                vSQL.AppendLine("UPDATE dbo.cxP SET DiaDeAplicacion = 1 ");
                vSQL.AppendLine("WHERE ConsecutivoCompania = " + valConsecutivoCompania.ToString());
                vSQL.AppendLine(" AND MesdeAplicacion  IN (2,4,6,9,11) AND DiaDeAplicacion > 28");
                vSQL.AppendLine("AND (MesDeAplicacion <> " + InsSql.Month( "cxp.FechaAplicacionRetIva","") + " OR AnoDeAplicacion  <> " + InsSql.Year( "cxp.FechaAplicacionRetIva","") + ")");
                Execute(vSQL.ToString());
            }
        }

    }
}
