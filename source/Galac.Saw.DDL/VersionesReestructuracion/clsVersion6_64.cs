using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_64: clsVersionARestructurar {
        public clsVersion6_64(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregaColumnasReglasDeContabilizacionOrdenDeProduccion();
            CorrecionCampoSettDefinition();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregaColumnasReglasDeContabilizacionOrdenDeProduccion() {
            if (!ColumnExists("Saw.ReglasDeContabilizacion", "TipoContabilizacionOrdenDeProduccion")) {
                if (AddColumnEnumerative("Saw.ReglasDeContabilizacion", "TipoContabilizacionOrdenDeProduccion", "", LibConvert.EnumToDbValue(0))) {
                    AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConTiCoOrDePr", "'0'", "TipoContabilizacionOrdenDeProduccion");
                }
                if (AddColumnEnumerative("Saw.ReglasDeContabilizacion", "ContabIndividualOrdenDeProduccion", "", LibConvert.EnumToDbValue(1))) {
                    AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCoInOrDePr", "'1'", "ContabIndividualOrdenDeProduccion");
                }
                if (AddColumnEnumerative("Saw.ReglasDeContabilizacion", "ContabPorLoteOrdenDeProduccion", "", LibConvert.EnumToDbValue(0))) {
                    AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCoPoLoOrDePr", "'0'", "ContabPorLoteOrdenDeProduccion");
                }
                if (AddColumnString("Saw.ReglasDeContabilizacion", "CuentaOrdenDeProduccionProductoTerminado", 30, "", "")) {
                    AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCuOrDePrPrTe", _insSql.ToSqlValue(""), "CuentaOrdenDeProduccionProductoTerminado");
                }
                if (AddColumnString("Saw.ReglasDeContabilizacion", "CuentaOrdenDeProduccionMateriaPrima", 30, "", "")) {
                    AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCuOrDePrMaPr", _insSql.ToSqlValue(""), "CuentaOrdenDeProduccionMateriaPrima");
                }
                if (AddColumnString("Saw.ReglasDeContabilizacion", "OrdenDeProduccionTipoComprobante", 2, "", "")) {
                    AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConOrDePrTiCo", _insSql.ToSqlValue(""), "OrdenDeProduccionTipoComprobante");
                }
                if (AddColumnBoolean("Saw.ReglasDeContabilizacion", "EditarComprobanteAfterInsertOrdenDeProduccion", "", false)) {
                    AddNotNullConstraint("Saw.ReglasDeContabilizacion", "EditarComprobanteAfterInsertOrdenDeProduccion", InsSql.CharTypeForDb(1));
                }
            }
        }
    
        private void CorrecionCampoSettDefinition() {
            QAdvSql InsSql = new QAdvSql("");;
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Comun.SettDefinition SET GroupName = '7.5.- Transferencias Bancarias' WHERE GroupName = '7.5.- Movimiento Bancario'");
            vSql.AppendLine(" AND (Name = 'ConceptoBancarioReversoTransfEgreso' OR Name = 'ConceptoBancarioReversoTransfIngreso')");
            Execute(vSql.ToString(), 0);
        }
    }
}
