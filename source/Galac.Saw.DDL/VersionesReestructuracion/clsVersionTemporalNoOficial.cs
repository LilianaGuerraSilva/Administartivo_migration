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
			AmpliarCampoObservacionesSolicitudDePago();
			AgregaNuevosRegistrosTipoFormaDelCobro();
			AgregarColumnasCajaAperturaoMS();
			CrearCampoCompaniaUsaInformesFinancieros();
			DisposeConnectionNoTransaction();
			return true;
		}

		private void AmpliarCampoObservacionesSolicitudDePago() {
			string vSQL = "ALTER TABLE " + LibDbo.ToFullDboName("Saw.SolicitudesDePago") + " ALTER COLUMN Observaciones VARCHAR(60) NULL";
			Execute(vSQL, 0);
		}

		private void AgregaNuevosRegistrosTipoFormaDelCobro() {
			LibDatabase insDb = new LibDatabase();
			string vNextCode = insDb.NextStrConsecutive("Saw.formaDelCobro", "Codigo", "", true, 5);
			Execute(SqlInsertarFormaDeCobro(vNextCode, "TARJETAMS", eTipoDeFormaDePago.TarjetaMS));
			vNextCode = insDb.NextStrConsecutive("Saw.formaDelCobro", "Codigo", "", true, 5);
			Execute(SqlInsertarFormaDeCobro(vNextCode, "ZELLE", eTipoDeFormaDePago.Zelle));
			vNextCode = insDb.NextStrConsecutive("Saw.formaDelCobro", "Codigo", "", true, 5);
			Execute(SqlInsertarFormaDeCobro(vNextCode, "PAGOMOVIL", eTipoDeFormaDePago.PagoMovil));
			vNextCode = insDb.NextStrConsecutive("Saw.formaDelCobro", "Codigo", "", true, 5);
			Execute(SqlInsertarFormaDeCobro(vNextCode, "TRANSFERENCIAMS", eTipoDeFormaDePago.TransferenciaMS));
			vNextCode = insDb.NextStrConsecutive("Saw.formaDelCobro", "Codigo", "", true, 5);
			Execute(SqlInsertarFormaDeCobro(vNextCode, "C2P", eTipoDeFormaDePago.C2P));
		}

		string SqlInsertarFormaDeCobro(string valCodigo, string valNombre, eTipoDeFormaDePago valTipoDePago) {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("INSERT INTO Saw.FormaDelCobro (Codigo, Nombre, TipoDePago) VALUES (");
			vSql.AppendLine(InsSql.ToSqlValue(valCodigo) + ", " + InsSql.ToSqlValue(valNombre) + ", " + InsSql.EnumToSqlValue((int)valTipoDePago) + ")");
			return vSql.ToString();
		}

		private void AgregarColumnasCajaAperturaoMS() {
			if (AddColumnDecimal("Adm.CajaApertura", "MontoC2P", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoC2P", "0", "MontoC2P");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoTarjetaMS", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoTaMs", "0", "MontoTarjetaMS");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoTransferenciaMS", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoTran", "0", "MontoTransferenciaMS");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoPagoMovil", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoPaMo", "0", "MontoPagoMovil");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoZelle", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoZell", "0", "MontoZelle");
			}

		}

		private void CrearCampoCompaniaUsaInformesFinancieros() {
			AddColumnBoolean("dbo.Compania", "UsaInformesFinancieros", "CONSTRAINT UsaInfFinan NOT NULL", false);
		}
	}
}   