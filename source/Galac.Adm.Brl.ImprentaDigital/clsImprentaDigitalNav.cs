using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Cnf;
using LibGalac.Aos.DefGen;
using Galac.Adm.Ccl.ImprentaDigital;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using System.Threading.Tasks;

namespace Galac.Adm.Brl.ImprentaDigital {
    public class clsImprentaDigitalNav:clsImprentaDigitalBase {
  

        public clsImprentaDigitalNav() {
            
        }


        public override void ConfigurarDocumento() {
            throw new NotImplementedException();
        }

        public override Task< bool> EnviarDocumento() {
            throw new NotImplementedException();
        }
    }
}
