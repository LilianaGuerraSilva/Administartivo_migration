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
			CrearCampoCompania_UsaInformesFinancieros();
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
			Execute(SqlInsertarFormaDeCobro(vNextCode, "COBROCONTARJETA", eTipoDeFormaDePago.CobroConTarjeta));
			vNextCode = insDb.NextStrConsecutive("Saw.formaDelCobro", "Codigo", "", true, 5);
			Execute(SqlInsertarFormaDeCobro(vNextCode, "COBROZELLE", eTipoDeFormaDePago.CobroZelle));
			vNextCode = insDb.NextStrConsecutive("Saw.formaDelCobro", "Codigo", "", true, 5);
			Execute(SqlInsertarFormaDeCobro(vNextCode, "COBROPAGOMOVIL", eTipoDeFormaDePago.CobroPagoMovil));
			vNextCode = insDb.NextStrConsecutive("Saw.formaDelCobro", "Codigo", "", true, 5);
			Execute(SqlInsertarFormaDeCobro(vNextCode, "COBROTRANSFERENCIA", eTipoDeFormaDePago.CobroTransferencia));
			vNextCode = insDb.NextStrConsecutive("Saw.formaDelCobro", "Codigo", "", true, 5);
			Execute(SqlInsertarFormaDeCobro(vNextCode, "COBROC2P", eTipoDeFormaDePago.CobroC2P));
		}

		string SqlInsertarFormaDeCobro(string valCodigo, string valNombre, eTipoDeFormaDePago valTipoDePago) {
			StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("INSERT INTO Saw.FormaDelCobro (Codigo, Nombre, TipoDePago) VALUES (");
			vSql.AppendLine(InsSql.ToSqlValue(valCodigo) + ", " + InsSql.ToSqlValue(valNombre) + ", " + InsSql.EnumToSqlValue((int)valTipoDePago) + ")");
			return vSql.ToString();
		}

		private void CrearCampoCompania_UsaInformesFinancieros() {
			AddColumnBoolean("dbo.Compania", "UsaInformesFinancieros", "CONSTRAINT UsaInfFinan NOT NULL", false);
		}
	}
}   