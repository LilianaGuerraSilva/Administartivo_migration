using System.Text;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using LibGalac.Aos.Dal;
using System;
using LibGalac.Aos.Base.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersionTemporalNoOficial: clsVersionARestructurar {
        public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();            
            CrearParametrosImprentaDigital();
            CrearCamposParaImprentaDigitalEnFactura();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void CrearParametrosImprentaDigital() {
            AgregarNuevoParametro("UsaImprentaDigital", "Factura", 2, "2.8.- Imprenta Digital", 8, "", eTipoDeDatoParametros.String, "", 'N', "N");
            AgregarNuevoParametro("FechaInicioImprentaDigital", "Factura", 2, "2.8.- Imprenta Digital", 8, "", eTipoDeDatoParametros.String, "", 'N', LibConvert.ToStr(LibDate.MinDateForDB()));
            AgregarNuevoParametro("ProveedorImprentaDigital", "Factura", 2, "2.8.- Imprenta Digital", 8, "", eTipoDeDatoParametros.Enumerativo, "", 'N', "0");
        }  

        private void CrearCamposParaImprentaDigitalEnFactura() {
            AddColumnString("factura", "MotivoDeAnulacion", 150, "", "");
            if (AddColumnEnumerative("factura", "ProveedorImprentaDigital", "", 0)) {
                AddDefaultConstraint("factura", "nnFacturaProveedorImpDig", "'0'", "ProveedorImprentaDigital");
            }
        }
    }
}
