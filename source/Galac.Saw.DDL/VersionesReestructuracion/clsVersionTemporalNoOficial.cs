using System.Text;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using LibGalac.Aos.Dal;
using System;

namespace Galac.Saw.DDL.VersionesReestructuracion {
	class clsVersionTemporalNoOficial : clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
            AgregarColumnasATablaCompras();
            AgregarParametroNombrePlantillaSubFacturaConOtrosCargos();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarColumnasATablaCompras() {
            AddColumnDecimal("Adm.Compra", "CambioCostoUltimaCompra", 25, 4, "CONSTRAINT nnComCaCoUlCo NOT NULL", 1);
            if (AddColumnString("Adm.Compra", "CodigoMonedaCostoUltimaCompra", 4, "", "VED")) {
                AddForeignKey("dbo.Moneda", "Adm.Compra", new string[] { "Codigo" }, new string[] { "CodigoMonedaCostoUltimaCompra" }, false, false);
            }
        }
		
		private void AgregarParametroNombrePlantillaSubFacturaConOtrosCargos(){
			AgregarNuevoParametro("NombrePlantillaSubFacturaConOtrosCargos", "Factura", 2, " 2.4.- Modelo de Factura ", 4 , "",'2', "", 'N', "rpxSubFacturaConOtrosCargos");
		}
	}
}
