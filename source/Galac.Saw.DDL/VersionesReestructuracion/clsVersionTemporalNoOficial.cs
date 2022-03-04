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
            CrearNuevosCamposImpTransacBancarias();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void CrearNuevosCamposImpTransacBancarias(){
            if (AddColumnDecimal("Adm.ImpTransacBancarias", "AlicuotaC1Al4", 25, 4, "", 0)){
                AddDefaultConstraint("Adm.ImpTransacBancarias", "d_ImpBanAlC14", "0", "AlicuotaC1Al4");
            }
            if (AddColumnDecimal("Adm.ImpTransacBancarias", "AlicuotaC5", 25, 4, "", 0)){
                AddDefaultConstraint("Adm.ImpTransacBancarias", "d_ImpBanAlC5", "0", "AlicuotaC5");
            }
            if (AddColumnDecimal("Adm.ImpTransacBancarias", "AlicuotaC6", 25, 4, "", 0)){
                AddDefaultConstraint("Adm.ImpTransacBancarias", "d_ImpBanAlC6", "0", "AlicuotaC6");
            }
        }
    }
}

