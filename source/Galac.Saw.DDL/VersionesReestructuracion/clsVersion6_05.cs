namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_05:clsVersionARestructurar {
        public clsVersion6_05(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.05";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregaCampoEnParametroConciliacion();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregaCampoEnParametroConciliacion() {
            if (!ColumnExists("Contab.ParametrosConciliacion", "ExpresarBalancesEnDiferentesMonedas")) {
                AddColumnBoolean("Contab.ParametrosConciliacion", "ExpresarBalancesEnDiferentesMonedas", "CONSTRAINT nnParExpresarBalances NOT NULL", false);
            }
        }
    }
}
