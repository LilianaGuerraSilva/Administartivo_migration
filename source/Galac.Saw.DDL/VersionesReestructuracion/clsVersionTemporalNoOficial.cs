using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;

namespace Galac.Saw.DDL.VersionesReestructuracion
{

    class clsVersionTemporalNoOficial : clsVersionARestructurar {
		public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
		public override bool UpdateToVersion() {
			StartConnectionNoTransaction();
            CrearParametrosCreditoElectronico();
            DisposeConnectionNoTransaction();
			return true;
		}

        private void CrearParametrosCreditoElectronico()
        {
            AgregarNuevoParametro("UsaCreditoEelectronico", "Factura", 2, "2.2.- Facturaci�n (Continuaci�n) ", 2, "", eTipoDeDatoParametros.String, "", 'N', "N");
            AgregarNuevoParametro("NombreCreditoElectronico", "Factura", 2, "2.2.- Facturaci�n (Continuaci�n) ", 2, "", eTipoDeDatoParametros.String, "", 'N', "Cr�dito Electr�nico");
            AgregarNuevoParametro("DiasUsualesCreditoElectronico", "Factura", 2, "2.2.- Facturaci�n (Continuaci�n) ", 2, "", eTipoDeDatoParametros.Int, "", 'N', "14");
            AgregarNuevoParametro("DiasMaximoCreditoElectronico", "Factura", 2, "2.2.- Facturaci�n (Continuaci�n) ", 2, "", eTipoDeDatoParametros.Int, "", 'N', "14");
        }
    }
}
