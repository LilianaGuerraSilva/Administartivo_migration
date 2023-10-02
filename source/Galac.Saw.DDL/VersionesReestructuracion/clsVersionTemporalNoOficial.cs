using System.Text;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.Tablas;


namespace Galac.Saw.DDL.VersionesReestructuracion {

	class clsVersionTemporalNoOficial: clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();					
			CamposMonedaExtranjeraEnCajaApertura();
			CamposInfoAdicionalRenglonCobroFactura();
			DisposeConnectionNoTransaction();
			return true;
		}

		private void CamposMonedaExtranjeraEnCajaApertura() {
			if (AddColumnDecimal("Adm.CajaApertura", "MontoVueltoPM", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoVuPM", "0", "MontoVueltoPM");
			}
		}

		private void CamposInfoAdicionalRenglonCobroFactura() {
			if (AddColumnString("RenglonCobroDeFactura", "InfoAdicional", 250,"",string.Empty)) {
				AddDefaultConstraint("RenglonCobroDeFactura", "d_RenCobDeFacInAd", string.Empty, "InfoAdicional");
			}
		}
	}
}   