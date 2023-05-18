using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_65: clsVersionARestructurar {
        public clsVersion6_65(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            CrearCampoCompania_EstaIntegradaG360();
            DisposeConnectionNoTransaction();
            return true;
        }
        private void CrearCampoCompania_EstaIntegradaG360() {
            AddColumnBoolean("dbo.Compania", "ConectadaConG360", "CONSTRAINT ConecConG360 NOT NULL", false);
        }
    }
}
