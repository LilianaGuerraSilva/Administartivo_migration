using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using System.Text;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_52 : clsVersionARestructurar {

        public clsVersion6_52(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.52";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            ExecuteAlterColumnEmail();
            ExecuteAlterColumnNumeroControl();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void ExecuteAlterColumnEmail(){
            ModifyLengthOfColumnString("Adm.Proveedor","Email",100,"");
        }
        private void ExecuteAlterColumnNumeroControl()
        {
            ModifyLengthOfColumnString("dbo.OPFalsoRetencion", "NumeroControl", 20, "");
        }
    }
}