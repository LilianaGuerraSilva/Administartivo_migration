using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.CajaChica {
   public interface IAnticipoPdn : ILibPdn {

        bool actualizar(List<Anticipo> list); 
     
    }
}
