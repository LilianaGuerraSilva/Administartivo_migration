using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using System.Text;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_14:clsVersionARestructurar {

        public clsVersion6_14(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.14";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarParametroLimiteIngresoTasaDeCambio();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarParametroLimiteIngresoTasaDeCambio() {
            AgregarNuevoParametro("UsarLimiteMaximoParaIngresoDeTasaDeCambio","Bancos",7,"7.2-Moneda",2,"",'2',"",'N',"N");
            AgregarNuevoParametro("MaximoLimitePermitidoParaLaTasaDeCambio","Bancos",7,"7.2-Moneda",2,"",'3',"",'N',"30");
        }
    }
}

