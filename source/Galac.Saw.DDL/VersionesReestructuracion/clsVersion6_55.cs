using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using System.Text;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_55 : clsVersionARestructurar {

        public clsVersion6_55(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.55";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
			CrearNuevosCamposFactura();
			DisposeConnectionNoTransaction();
            return true;
        }

		private void CrearNuevosCamposFactura() {
			if (AddColumnCurrency("dbo.Factura", "BaseImponibleIGTF", "", 0)) {
				AddDefaultConstraint("dbo.Factura", "d_FacBaImIG", "0", "BaseImponibleIGTF");
			}
			if (AddColumnCurrency("dbo.Factura", "IGTFML", "", 0)) {
				AddDefaultConstraint("dbo.Factura", "d_FacIGT", "0", "IGTFML");
			}
			if (AddColumnCurrency("dbo.Factura", "IGTFME", "", 0)) {
				AddDefaultConstraint("dbo.Factura", "d_FacIGTME", "0", "IGTFME");
			}
			if (AddColumnCurrency("dbo.Factura", "AlicuotaIGTF", "", 0)) {
				AddDefaultConstraint("dbo.Factura", "d_FacAlIG", "0", "AlicuotaIGTF");
			}
		}
	}
}