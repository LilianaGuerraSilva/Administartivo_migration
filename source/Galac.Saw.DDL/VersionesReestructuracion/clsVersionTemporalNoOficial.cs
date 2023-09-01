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
			ExtiendeLongitudCampoDefinibleCliente();
            DisposeConnectionNoTransaction();
			return true;
		}

		private void ExtiendeLongitudCampoDefinibleCliente() {
			AlterColumnIfExist("Cliente", "CampoDefinible1", InsSql.VarCharTypeForDb(60), "", "");
		}
	}
}   