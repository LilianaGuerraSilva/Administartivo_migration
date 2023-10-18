using System.Text;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.Tablas;


namespace Galac.Saw.DDL.VersionesReestructuracion {

	class clsVersionTemporalNoOficial: clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
			DisposeConnectionNoTransaction();
			return true;
		}
	}
}   