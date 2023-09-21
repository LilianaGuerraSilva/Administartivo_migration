using System.Text;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.Tablas;


namespace Galac.Saw.DDL.VersionesReestructuracion {

	class clsVersionTemporalNoOficial: clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();			
			AgregaNuevosRegistrosTipoFormaDelCobro();		
			ExtiendeLongitudCampoDefinibleCliente();
			CamposMonedaExtranjeraEnCajaApertura();
			CamposInfoAdicionalRenglonCobroFactura();
			DisposeConnectionNoTransaction();
			return true;
		}

		private void ExtiendeLongitudCampoDefinibleCliente() {
			AlterColumnIfExist("Cliente", "CampoDefinible1", InsSql.VarCharTypeForDb(60), "", "");
		}
		private void AgregaNuevosRegistrosTipoFormaDelCobro() {
			LibDatabase insDb = new LibDatabase();
			string vNextCode = insDb.NextStrConsecutive("Saw.formaDelCobro", "Codigo", "", true, 5);
			InsertFormaDelCobro(vNextCode, eTipoDeFormaDePago.VueltoEfectivo);
			vNextCode = insDb.NextStrConsecutive("Saw.formaDelCobro", "Codigo", "", true, 5);
			InsertFormaDelCobro(vNextCode, eTipoDeFormaDePago.VueltoC2P);
		}

		private void InsertFormaDelCobro(string valCodigo, eTipoDeFormaDePago valFormaDelCobro) {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("INSERT INTO Saw.FormaDelCobro (Codigo, Nombre, TipoDePago) VALUES ");
			vSql.AppendLine("(" + InsSql.ToSqlValue(valCodigo) + ", " + InsSql.ToSqlValue("VUELTO") + ", " + InsSql.EnumToSqlValue((int)valFormaDelCobro) + ")");
			Execute(vSql.ToString());
		}

		private void CamposMonedaExtranjeraEnCajaApertura() {
			if (AddColumnDecimal("Adm.CajaApertura", "MontoVuelto", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoVu", "0", "MontoVuelto");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoVueltoME", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoVuME", "0", "MontoVueltoME");
			}
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