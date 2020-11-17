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
    class clsVersion5_96 : clsVersionARestructurar {
        public clsVersion5_96(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.96";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregaCamposFechaAplicacionAPlanillaIVA();
            AjustaValoresFechaAplicacion();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregaCamposFechaAplicacionAPlanillaIVA() {
            if (!ColumnExists("dbo.planillaForma00030", "FechaDesdeAplicacion")) {
                AddColumnDate("dbo.planillaForma00030", "FechaDesdeAplicacion", "");
            }
            if (!ColumnExists("dbo.planillaForma00030", "FechaHastaAplicacion")) {
                AddColumnDate("dbo.planillaForma00030", "FechaHastaAplicacion", "");
            }
        
        }

        private void AjustaValoresFechaAplicacion() {
            StringBuilder vSQL = new StringBuilder();
            if (ColumnExists("dbo.planillaForma00030", "FechaDesdeAplicacion")) {
                vSQL.AppendLine("UPDATE dbo.planillaForma00030 SET FechaDesdeAplicacion = CONVERT(datetime, '01/' + CAST(Mes AS VARCHAR) + '/' + CAST(AnoAPlicacion AS VARCHAR), 103)");
                
                Execute( vSQL.ToString(),-1); 
            }
            vSQL.Clear();
            if (ColumnExists("dbo.planillaForma00030", "FechaHastaAplicacion")) {
                vSQL.AppendLine("UPDATE dbo.planillaForma00030 SET FechaHastaAplicacion = CAST (CONVERT ( char(11), DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,CONVERT(datetime, '01/' + CAST(CASE WHEN planillaForma00030.Mes = 12 THEN 1 ELSE planillaForma00030.Mes + 1 END  AS VARCHAR) + '/' + CAST(CASE WHEN planillaForma00030.Mes = 12 THEN planillaForma00030.AnoAplicacion + 1 ELSE planillaForma00030.AnoAplicacion END     AS VARCHAR),103)),0)), 113) AS DATETIME)");
                
                Execute( vSQL.ToString(),-1); 
            }
        }

    }
}
