﻿using System.Text;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.DDL.VersionesReestructuracion {

    class clsVersionTemporalNoOficial:clsVersionARestructurar {
        public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarParametroUsaDivisaComoMonedaPrincipalDeIngresoDeDatos();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarParametroUsaDivisaComoMonedaPrincipalDeIngresoDeDatos() {
            AgregarNuevoParametro("UsaDivisaComoMonedaPrincipalDeIngresoDeDatos", "Bancos", 7, "7.2-Moneda", 2, "", eTipoDeDatoParametros.String, "", 'N', "N");
        }

    }
}

