using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Brl;
using System.Data;
using LibGalac.Aos.Dal.Usal;
using System.Collections.Generic;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base.Dal;
using System.Linq;
using System.Xml.Linq;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_12:clsVersionARestructurar {

        public clsVersion6_12(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.12";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarParametroDePlantillaComprobanteISRL();
            AgregarColumnasEnReglasDeContabilizacion();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarParametroDePlantillaComprobanteISRL() {
            AgregarNuevoParametro("NombrePlantillaComprobanteDeRetISRL","CxP/Compras",6,"6.4.- Retención ISRL",4,"",'2',"",'N',"rpxComprobanteDeRetencion");
        }

        private void AgregarColumnasEnReglasDeContabilizacion() {
            if(!ColumnExists("Saw.ReglasDeContabilizacion","ManejarDiferenciaCambiariaEnCobranza")) {
                AddColumnBoolean("Saw.ReglasDeContabilizacion","ManejarDiferenciaCambiariaEnCobranza","",false);
            }
            if(!ColumnExists("Saw.ReglasDeContabilizacion","ManejarDiferenciaCambiariaEnPagos")) {
                AddColumnBoolean("Saw.ReglasDeContabilizacion","ManejarDiferenciaCambiariaEnPagos","",false);
            }
            if(!ColumnExists("Saw.ReglasDeContabilizacion","CuentaDiferenciaCambiaria")) {
                AddColumnString("Saw.ReglasDeContabilizacion","CuentaDiferenciaCambiaria",30,"","");
            }
        }

    }
}

