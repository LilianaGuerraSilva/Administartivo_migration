using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using System.Text;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_59 : clsVersionARestructurar {
        public clsVersion6_59(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.59";
        }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            ActivaModoMejoradoPorDefecto();
            DisposeConnectionNoTransaction();
            return true;
        }
        private void ActivaModoMejoradoPorDefecto() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine($"UPDATE Adm.Caja SET UsarModoDotNet={InsSql.ToSqlValue(true)}");
            vSql.AppendLine("WHERE Consecutivo <> 0 ");
            vSql.AppendLine($"AND FamiliaImpresoraFiscal IN({InsSql.ToSqlValue("0")},{InsSql.ToSqlValue("2")},{InsSql.ToSqlValue("3")})");
            vSql.AppendLine($"AND UsaMaquinaFiscal ={InsSql.ToSqlValue(true)}");
            Execute(vSql.ToString(), 0);
        }
    }
}