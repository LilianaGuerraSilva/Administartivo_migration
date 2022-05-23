using System.Text;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using LibGalac.Aos.Dal;
using System;
using LibGalac.Aos.Base.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersionTemporalNoOficial :clsVersionARestructurar {
        public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarColumnasATablaCompras();
            ActualizarAlicuotaenFactura();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarColumnasATablaCompras() {
            AddColumnDecimal("Adm.Compra", "CambioCostoUltimaCompra", 25, 4, "CONSTRAINT nnComCaCoUlCo NOT NULL", 1);
            AddColumnString("Adm.Compra", "CodigoMonedaCostoUltimaCompra", 4, "", "VED");
        }

        private void ActualizarAlicuotaenFactura(){
			QAdvSql insSql = new QAdvSql("");
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("UPDATE factura ");
			vSql.AppendLine("SET AlicuotaIGTF = 3 ");
			vSql.AppendLine("WHERE (BaseImponibleIGTF > 0 OR IGTFML > 0 OR IGTFME > 0) AND AlicuotaIGTF = 0 AND Fecha >= " + insSql.ToSqlValue(LibConvert.ToDate("27/03/2022")));
			Execute(vSql.ToString(), 0);
        }
    }
}
