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
    class clsVersion5_98 : clsVersionARestructurar {
        public clsVersion5_98(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.98";
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
                vSQL.AppendLine("AND MesdeAplicacion  =  2 AND DiaDeAplicacion > 28");
                Execute(vSQL.ToString());
                vSQL.Clear();
                vSQL.AppendLine("UPDATE dbo.cxP SET DiaDeAplicacion = 1 ");
                vSQL.AppendLine("WHERE ConsecutivoCompania = " + valConsecutivoCompania.ToString());
                vSQL.AppendLine(" AND MesdeAplicacion  IN (4,6,9,11) AND DiaDeAplicacion > 30");
                Execute(vSQL.ToString());
            }
        }

    }
}
