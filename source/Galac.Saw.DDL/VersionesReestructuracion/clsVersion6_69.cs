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
    class clsVersion6_69: clsVersionARestructurar {
        public clsVersion6_69(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
			AgregaNuevosRegistrosTipoFormaDelCobro();
			ExtiendeLongitudCampoDefinibleCliente();
			CamposMonedaExtranjeraEnCajaApertura();
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
			vSql.AppendLine("(" + InsSql.ToSqlValue(valCodigo) + " , " + InsSql.ToSqlValue("VUELTO") + ", " + _insSql.EnumToSqlValue((int)valFormaDelCobro) + ")");
			Execute(vSql.ToString());
		}

		private void CamposMonedaExtranjeraEnCajaApertura() {
			if (AddColumnDecimal("Adm.CajaApertura", "MontoVuelto", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoVuel", "0", "MontoVuelto");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoVueltoME", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoVuelME", "0", "MontoVueltoME");
			}
		}
	}
}
