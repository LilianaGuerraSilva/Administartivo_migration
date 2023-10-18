using Galac.Adm.Dal.Vendedor;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_70: clsVersionARestructurar {
        public clsVersion6_70(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
			CampoVueltoPagoMovilEnCajaApertura();
			CamposInfoAdicionalRenglonCobroFactura();
			DisposeConnectionNoTransaction();
            return true;
        }

		private void CampoVueltoPagoMovilEnCajaApertura() {
			if (AddColumnDecimal("Adm.CajaApertura", "MontoVueltoPM", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoVuPM", "0", "MontoVueltoPM");
			}
		}

		private void CamposInfoAdicionalRenglonCobroFactura() {
			if (AddColumnString("RenglonCobroDeFactura", "InfoAdicional", 250, "", "")) {
				AddDefaultConstraint("RenglonCobroDeFactura", "d_RenCobDeFacInAd", "", "InfoAdicional");
			}
		}
	}
}
