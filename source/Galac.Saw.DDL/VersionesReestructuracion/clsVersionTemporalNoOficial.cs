using System.Text;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Brl.Tablas;
using System.ComponentModel.DataAnnotations;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.DDL.VersionesReestructuracion {

	class clsVersionTemporalNoOficial: clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
            AgregarParametroObtenerTasaDeCambioDelBCV();
			CampoVueltoPagoMovilEnCajaApertura();
			CamposInfoAdicionalRenglonCobroFactura();
			DisposeConnectionNoTransaction();
			return true;
		}

        private void AgregarParametroObtenerTasaDeCambioDelBCV() {
            AgregarNuevoParametro("ObtenerAutomaticamenteTasaDeCambioDelBCV", "Bancos", 7, "7.2-Moneda", 2, "", eTipoDeDatoParametros.String, "", 'N', "N");
        }

		private void CampoVueltoPagoMovilEnCajaApertura() {
			if (AddColumnDecimal("Adm.CajaApertura", "MontoVueltoPM", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoVuPM", "0", "MontoVueltoPM");
			}
		}

		private void CamposInfoAdicionalRenglonCobroFactura() {
			if (AddColumnString("RenglonCobroDeFactura", "InfoAdicional", 250, "", "")) {
				AddDefaultConstraint("RenglonCobroDeFactura", "d_RenCobDeFacInAd", InsSql.ToSqlValue(""), "InfoAdicional");
			}
		}
	}
}   