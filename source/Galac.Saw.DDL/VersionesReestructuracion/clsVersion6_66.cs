using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_66: clsVersionARestructurar {
        public clsVersion6_66(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
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
                AddNotNullConstraint("factura", "ProveedorImprentaDigital", InsSql.CharTypeForDb(1));
            }
        }
    }
}
