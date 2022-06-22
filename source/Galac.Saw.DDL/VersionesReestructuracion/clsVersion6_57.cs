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
    class clsVersion6_57 : clsVersionARestructurar {
        public clsVersion6_57(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.57";
        }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarColumnasATablaCompras();
            ActualizarAlicuotaenFactura();
            DisposeConnectionNoTransaction();
            return true;
        }
        private void AgregarColumnasATablaCompras() {
            AddColumnDecimal("Adm.Compra", "CambioCostoUltimaCompra", 25, 4, "CONSTRAINT nnComCaCoUlCo NOT NULL", 1);
            if (AddColumnString("Adm.Compra", "CodigoMonedaCostoUltimaCompra", 4, "", "VED")) {
                AddDefaultConstraint("Adm.Compra", "d_ComCoMoCoUlCo", _insSql.ToSqlValue(""), "CodigoMonedaCostoUltimaCompra");
            }
        }
        private void ActualizarAlicuotaenFactura() {
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE factura ");
            vSql.AppendLine("SET AlicuotaIGTF = 3 ");
            vSql.AppendLine("WHERE (BaseImponibleIGTF > 0 OR IGTFML > 0 OR IGTFME > 0) AND AlicuotaIGTF = 0 AND Fecha >= " + insSql.ToSqlValue(LibConvert.ToDate("27/03/2022")));
            Execute(vSql.ToString(), 0);
        }
    }
}