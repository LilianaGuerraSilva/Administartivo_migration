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
    class clsVersion6_73: clsVersionARestructurar {
        public clsVersion6_73(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregaParametroGeneral();
            DeleteSettValueByCompany();
            DisposeConnectionNoTransaction();
            return true;
        }
        private void AgregaParametroGeneral() {
            AgregarNuevoParametro("SuscripcionGVentas", "DatosGenerales", 1, "1.2.-General", 2, "", '2', "", 'S', "0");
            AgregarNuevoParametro("SerialConectorGVentas", "DatosGenerales", 1, "1.2.-General", 2, "", '2', "", 'S', "");
            AgregarNuevoParametro("NumeroIDGVentas", "DatosGenerales", 1, "1.2.-General", 2, "", '2', "", 'N', "");
        }

        private void DeleteSettValueByCompany() {
            Execute("DELETE FROM Comun.SettValueByCompany WHERE NameSettDefinition = 'SolicitarIngresoDeTasaDeCambioAlEmitir'", 0);           
        }
    }
}
