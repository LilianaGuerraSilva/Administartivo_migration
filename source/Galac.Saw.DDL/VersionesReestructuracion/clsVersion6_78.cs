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
using Galac.Saw.Lib;
using LibGalac.Aos.Cnf;
using System.Data;
using LibGalac.Aos.DefGen;
using Galac.Saw.Dal.Tablas;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_78 : clsVersionARestructurar {
        public clsVersion6_78(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            CrearAuditoriaConfiguracion();
            DisposeConnectionNoTransaction();
            return true;
        }
        private void CrearAuditoriaConfiguracion() {
            new clsAuditoriaConfiguracionED().InstalarTabla();
        }
    }
}
