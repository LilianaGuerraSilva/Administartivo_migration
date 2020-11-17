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

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_00:clsVersionARestructurar {
        public clsVersion6_00(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.00";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();            
            AgregarParametroImprFactura();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarParametroImprFactura() {
            AgregarNuevoParametro("ImprimirComprobanteFiscalEnContrato","Factura",2,"2.3.- Impresión de Factura",3,"",'2',"",'S',"N");
        }

    }
}
