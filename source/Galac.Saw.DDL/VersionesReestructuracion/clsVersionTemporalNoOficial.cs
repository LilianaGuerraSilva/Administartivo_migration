using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersionTemporalNoOficial : clsVersionARestructurar {
        public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            ParametrosCreditoElectronico();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void ParametrosCreditoElectronico() {
            string vGroupNameNuevo = "2.9.- Cobro de Factura";
            string vGroupNameActual = "2.2.- Facturación (Continuación) ";
            int vLevelGroupNuevo = 9;
            AgregarNuevoParametro("UsaCreditoElectronico", "Factura", 2, vGroupNameNuevo, 9, "", eTipoDeDatoParametros.String, "", 'N', "N");
            AgregarNuevoParametro("NombreCreditoElectronico", "Factura", 2, vGroupNameNuevo, 9, "", eTipoDeDatoParametros.String, "", 'N', "CASHEA");
            AgregarNuevoParametro("DiasDeCreditoPorCuotaCreditoElectronico", "Factura", 2, vGroupNameNuevo, 9, "", eTipoDeDatoParametros.Int, "", 'N', "14");
            AgregarNuevoParametro("CantidadCuotasUsualesCreditoElectronico", "Factura", 2, vGroupNameNuevo, 9, "", eTipoDeDatoParametros.Int, "", 'N', "6");
            AgregarNuevoParametro("MaximaCantidadCuotasCreditoElectronico", "Factura", 2, vGroupNameNuevo, 9, "", eTipoDeDatoParametros.Int, "", 'N', "6");
            AgregarNuevoParametro("UsaClienteUnicoCreditoElectronico", "Factura", 2, vGroupNameNuevo, 9, "", eTipoDeDatoParametros.String, "", 'N', "N");
            AgregarNuevoParametro("CodigoClienteCreditoElectronico", "Factura", 2, vGroupNameNuevo, 9, "", eTipoDeDatoParametros.String, "", 'N', "");
            AgregarNuevoParametro("GenerarUnaUnicaCuotaCreditoElectronico", "Factura", 2, vGroupNameNuevo, 9, "", eTipoDeDatoParametros.String, "", 'N', "N");

            MoverGroupName("EmitirDirecto", vGroupNameActual, vGroupNameNuevo, vLevelGroupNuevo);
            MoverGroupName("UsaCobroDirecto", vGroupNameActual, vGroupNameNuevo, vLevelGroupNuevo);
            MoverGroupName("UsaCobroDirectoEnMultimoneda", vGroupNameActual, vGroupNameNuevo, vLevelGroupNuevo);
            MoverGroupName("CuentaBancariaCobroDirecto", vGroupNameActual, vGroupNameNuevo, vLevelGroupNuevo);
            MoverGroupName("UsaMediosElectronicosDeCobro", vGroupNameActual, vGroupNameNuevo, vLevelGroupNuevo);
            MoverGroupName("ConceptoBancarioCobroDirecto", vGroupNameActual, vGroupNameNuevo, vLevelGroupNuevo);
            MoverGroupName("CuentaBancariaCobroMultimoneda", vGroupNameActual, vGroupNameNuevo, vLevelGroupNuevo);
            MoverGroupName("ConceptoBancarioCobroMultimoneda", vGroupNameActual, vGroupNameNuevo, vLevelGroupNuevo);
        }
    }
}