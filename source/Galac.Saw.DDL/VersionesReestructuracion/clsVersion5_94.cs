using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal.Settings;
using LibGalac.Aos.Base;
using System.Threading;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_94 : clsVersionARestructurar {
        public clsVersion5_94(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.94";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregaAlicuotaIVANueva();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregaAlicuotaIVANueva() {
            DateTime FechaInicioDeVigencia = new DateTime(2018,9,1);
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine ("IF NOT EXISTS (SELECT FechaDeInicioDeVigencia  ");
	        vSQL.AppendLine ("   FROM dbo.alicuotaIVA WHERE FechaDeInicioDeVigencia = " + InsSql.ToSqlValue(FechaInicioDeVigencia) + ")");
            vSQL.AppendLine(" 	    INSERT INTO dbo.alicuotaIVA (FechaDeInicioDeVigencia,MontoAlicuotaGeneral,MontoAlicuota2,MontoAlicuota3,PorcentajePasajeAereo)");
	        vSQL.AppendLine(" 	    VALUES ");
	        vSQL.AppendLine("       ( " + InsSql.ToSqlValue(FechaInicioDeVigencia) + ", 16,8,31,50)");
            Execute (vSQL.ToString(),0);
        }

    }
}
