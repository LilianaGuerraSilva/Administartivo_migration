﻿using System.Text;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;
using System.Threading;

namespace Galac.Saw.DDL.VersionesReestructuracion {

    class clsVersionTemporalNoOficial:clsVersionARestructurar {
        public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarParametroLimiteIngresoTasaDeCambio();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarParametroLimiteIngresoTasaDeCambio() {
            AgregarNuevoParametro("UsarLimiteMaximoParaIngresoDeTasaDeCambio","Bancos",7,"7.2-Moneda",2,"",'2',"",'S',"N");
            AgregarNuevoParametro("MaximoLimitePermitidoParaLaTasaDeCambio","Bancos",7,"7.2-Moneda",2,"",'3',"",'S',"30");
        }
    }
}

