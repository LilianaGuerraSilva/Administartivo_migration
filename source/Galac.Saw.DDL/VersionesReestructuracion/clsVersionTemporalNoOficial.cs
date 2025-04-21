using System.Text;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Brl.Tablas;
using System.ComponentModel.DataAnnotations;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using System;
using System.Data;
using LibGalac.Aos.Cnf;
using Galac.Saw.Lib;
using LibGalac.Aos.DefGen;

namespace Galac.Saw.DDL.VersionesReestructuracion {

	class clsVersionTemporalNoOficial : clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
			QuitarUniqueCaja();
            DisposeConnectionNoTransaction();
			return true;
		}

		private void QuitarUniqueCaja() {			
			DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.Caja", "Adm.CajaApertura");
			DeleteAllrelationShipsBetweenTables(_CurrentDataBaseName, "Adm.Caja", "factura");
			ExecuteDropConstraint("Adm.Caja", "u_CajConsecutivo", true);
			AddForeignKey("Adm.Caja", "Adm.CajaApertura", new string[] { "ConsecutivoCompania,Consecutivo" }, new string[] { "ConsecutivoCompania,ConsecutivoCaja" }, false, true);
			AddForeignKey("Adm.Caja", "factura", new string[] { "ConsecutivoCompania,Consecutivo" }, new string[] { "ConsecutivoCompania,ConsecutivoCaja" }, false, true);
		}
    }
}
