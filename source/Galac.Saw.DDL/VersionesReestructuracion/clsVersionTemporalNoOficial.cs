﻿using System.Text;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using LibGalac.Aos.Dal;


namespace Galac.Saw.DDL.VersionesReestructuracion {

    class clsVersionTemporalNoOficial:clsVersionARestructurar {
        public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();            
            DisposeConnectionNoTransaction();            
            return true;
        }      
    }
}

