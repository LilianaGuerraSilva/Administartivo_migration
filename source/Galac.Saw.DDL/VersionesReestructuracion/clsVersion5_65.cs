using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using Galac.Comun.Ccl.TablasLey;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_65 : clsVersionARestructurar {

        public clsVersion5_65(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.65";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            CambiaLongitudDeCampoEnTablaInformeCxCCxP();
            EliminaCamposDeTablasSettDefinitionSettValueByCompany();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void CambiaLongitudDeCampoEnTablaInformeCxCCxP() {
            if (TableExists("dbo.tblInformeCxCCxP") && ColumnExists("dbo.tblInformeCxCCxP", "Numero"))
            {
                DeleteAllDataForTable("dbo.tblInformeCxCCxP");
                ModifyLengthOfColumnString("dbo.tblInformeCxCCxP", "Numero", 25, "");                
            }
        }

        private void EliminaCamposDeTablasSettDefinitionSettValueByCompany() {
            string vSql = string.Empty;
            if (TableExists("Comun.SettValueByCompany")) {
                vSql = "DELETE FROM Comun.SettValueByCompany WHERE NameSettDefinition IN ('EscogerCompaniaAlEntrar', 'EscogerUltimaCompaniaUsada', 'FormaDeEscogerCompania')";
                Execute(vSql, 0);
            }

            if (TableExists("Comun.SettDefinition")) {
                vSql = "DELETE FROM [Comun].[SettDefinition] WHERE Name IN ('EscogerCompaniaAlEntrar', 'EscogerUltimaCompaniaUsada', 'FormaDeEscogerCompania')";
                Execute(vSql, 0);
            }
        }

    }
}
