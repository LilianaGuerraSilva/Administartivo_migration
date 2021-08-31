using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using System.Text;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_20 : clsVersionARestructurar {

        public clsVersion6_20(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.20";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarNuevosCamposEnFactura();
            ModificarLongitudNoCotizacionOrigen();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarNuevosCamposEnFactura(){
            if (!ColumnExists("dbo.factura", "GeneradoPor")){
                AddColumnEnumerative("dbo.factura", "GeneradoPor","", 0);
            }
        }

        private void ModificarLongitudNoCotizacionOrigen()
        {
            AlterColumnIfExist("dbo.factura", "NoCotizacionDeOrigen", InsSql.VarCharTypeForDb(20), "", "");
        }
    }
}