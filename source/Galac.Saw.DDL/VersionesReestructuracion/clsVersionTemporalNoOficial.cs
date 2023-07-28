using System.Text;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using LibGalac.Aos.Dal;
using System;
using LibGalac.Aos.Base.Dal;
using System.Xml.Linq;
using LibGalac.Aos.Brl;
using System.Collections.Generic;
using System.Linq;
using Galac.Adm.Ccl.Vendedor;
using LibGalac.Aos.Catching;
using Galac.Adm.Dal.Vendedor;
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Brl.Tablas;

namespace Galac.Saw.DDL.VersionesReestructuracion {

	class clsVersionTemporalNoOficial: clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();			
			CamposMonedaExtranjeraEnCajaApertura();
			DisposeConnectionNoTransaction();
			return true;
		}

		private void CamposMonedaExtranjeraEnCajaApertura() {
			if (AddColumnDecimal("Adm.CajaApertura", "MontoAperturaME", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoApME", "0", "MontoAperturaME");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoCierreME", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoCiME", "0", "MontoCierreME");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoEfectivoME", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoEfME", "0", "MontoEfectivoME");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoTarjetaME", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoTaME", "0", "MontoTarjetaME");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoChequeME", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoChME", "0", "MontoChequeME");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoDepositoME", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoDeME", "0", "MontoDepositoME");
			}
			if (AddColumnDecimal("Adm.CajaApertura", "MontoAnticipoME", 25, 4, "", 0)) {
				AddDefaultConstraint("Adm.CajaApertura", "d_CajApeMoAnME", "0", "MontoAnticipoME");
			}
			Execute("UPDATE Adm.CajaApertura SET CodigoMoneda = 'VED', Cambio = 1 WHERE CodigoMoneda IS NULL OR CodigoMoneda = '' OR CodigoMoneda = 'VES' OR Cambio IS NULL OR Cambio = 0");
			if (!ForeignKeyNameExists("fk_CajaAperturaMoneda")) {
				AddForeignKey("dbo.Moneda", "Adm.CajaApertura", new string[] { "Codigo" }, new string[] { "CodigoMoneda" }, false, true);
			}
		}

		
	}
}   