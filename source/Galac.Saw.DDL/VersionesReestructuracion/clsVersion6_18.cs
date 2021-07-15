using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using System.Text;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;


namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_18 : clsVersionARestructurar {

        public clsVersion6_18(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.18";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarParametroFormaDeCalcularElPrecioConIvaEnRenglonFactura();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarParametroFormaDeCalcularElPrecioConIvaEnRenglonFactura() {
            AgregarNuevoParametro("FormaDeCalculoDePrecioRenglonFactura", "Factura", 2, "2.1.- Facturación", 1, "", eTipoDeDatoParametros.Enumerativo, "", 'N', LibConvert.EnumToDbValue((int)eFormaDeCalculoDePrecioRenglonFactura.APartirDelPrecioConIVA));
        }
    }
}

