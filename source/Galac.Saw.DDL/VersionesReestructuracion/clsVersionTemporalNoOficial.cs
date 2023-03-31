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
    class clsVersionTemporalNoOficial : clsVersionARestructurar {
        public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregaColumnasReglasDeContabilizacionOrdenDeProduccion();
            CrearParametrosImprentaDigital();
            CrearCampoCompania_EstaIntegradaG360();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void CrearParametrosImprentaDigital() {
            AgregarNuevoParametro("UsaImprentaDigital", "Factura", 2, "2.8.- Imprenta Digital", 8, "", eTipoDeDatoParametros.String, "", 'N', "N");
            AgregarNuevoParametro("FechaInicioImprentaDigital", "Factura", 2, "2.8.- Imprenta Digital", 8, "", eTipoDeDatoParametros.String, "", 'N', LibConvert.ToStr(LibDate.MinDateForDB()));
            AgregarNuevoParametro("ProveedorImprentaDigital", "Factura", 2, "2.8.- Imprenta Digital", 8, "", eTipoDeDatoParametros.Enumerativo, "", 'N', "0");
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

        private void CrearCampoCompania_EstaIntegradaG360() {
            AddColumnBoolean("dbo.Compania", "ConectadaConG360", "CONSTRAINT ConecConG360 NOT NULL", false);
        }
    }
}
