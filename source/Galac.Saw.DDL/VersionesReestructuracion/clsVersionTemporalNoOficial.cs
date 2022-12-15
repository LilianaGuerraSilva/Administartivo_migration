using System.Text;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using LibGalac.Aos.Dal;
using System;
using LibGalac.Aos.Base.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {
	class clsVersionTemporalNoOficial : clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
			CrearParametroCostoTerminadoCalculadoAPartirDe();
			CrearCamposParaElManejoDeMonedaExtranjeraEnGestionProduccion();
			DisposeConnectionNoTransaction();
			return true;
		}

		private void CrearParametroCostoTerminadoCalculadoAPartirDe() {
			AgregarNuevoParametro("CostoTerminadoCalculadoAPartirDe", "Inventario", 5, "5.5.- Producción", 5, "", eTipoDeDatoParametros.Enumerativo, "", 'N', "0");
		}
		private void CrearCamposParaElManejoDeMonedaExtranjeraEnGestionProduccion() {
			QAdvSql InsSql = new QAdvSql("");
			if (AddColumnEnumerative("Adm.OrdenDeProduccion", "CostoTerminadoCalculadoAPartirDe", "", (int)eFormaDeCalcularCostoTerminado.APartirDeCostoEnMonedaLocal)) {
				AddDefaultConstraint("Adm.OrdenDeProduccion", "d_OrdDeProCosTerCalAParDe", InsSql.ToSqlValue((int)eFormaDeCalcularCostoTerminado.APartirDeCostoEnMonedaLocal), "CostoTerminadoCalculadoAPartirDe");
			}
			if (AddColumnString("Adm.OrdenDeProduccion", "CodigoMonedaCostoProduccion", 4, "", "VED")) {
				AddDefaultConstraint("Adm.OrdenDeProduccion", "d_OrdDeProCoMoCoPr", InsSql.ToSqlValue("VED"), "CodigoMonedaCostoProduccion");
			}
			if (AddColumnDecimal("Adm.OrdenDeProduccion", "CambioCostoProduccion", 25, 4, "", (decimal)1.0)) {
				AddDefaultConstraint("Adm.OrdenDeProduccion", "d_OrdDeProCaCoPr", InsSql.ToSqlValue((decimal)1.0), "CambioCostoProduccion");
			}
			if (AddColumnCurrency("dbo.RenglonNotaES", "CostoUnitarioME", "", (decimal)0.0)) {
				AddDefaultConstraint("dbo.RenglonNotaES", "d_RenNotESCoUnME", InsSql.ToSqlValue((decimal)1.0), "CostoUnitarioME");
				StringBuilder vSql = new StringBuilder();
				vSql.AppendLine("UPDATE dbo.RenglonNotaES");
				vSql.AppendLine("SET CostoUnitarioME = CostoUnitario");
				Execute(vSql.ToString());
			}
		}
	}
}
