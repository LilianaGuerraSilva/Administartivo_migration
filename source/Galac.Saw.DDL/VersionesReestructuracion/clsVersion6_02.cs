using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal.Settings;
using LibGalac.Aos.Base;
using System.Threading;
using System.Data;
using LibGalac.Aos.Dal.Usal;
using LibGalac.Aos.Dal;
using LibGalac.Aos.DefGen;
using System.Transactions;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_02:clsVersionARestructurar {
        public clsVersion6_02(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.02";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregaCampoEnComunValorUT();
            AgregaCampoEnRetPago();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregaCampoEnComunValorUT() {
            if(!ColumnExists("Comun.ValorUT","MontoUTImpuestosMunicipales")) {
                AddColumnCurrency("Comun.ValorUT","MontoUTImpuestosMunicipales","",0.012m);
            }
        }

        private void AgregaCampoEnRetPago() {
            if(!ColumnExists("dbo.RetPago","PrimerNumeroCompRetencion")) {
                AddColumnInteger("dbo.RetPago","PrimerNumeroCompRetencion","");
            }
        }       
    }
}
