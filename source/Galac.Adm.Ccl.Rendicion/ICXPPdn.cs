using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.CajaChica {
    public interface ICXPPdn:ILibPdn {
  
        bool insert(List<CxP> list);
        IList<CxP> buscarCxp(StringBuilder parametros); 
        bool eliminarCXP(List<CxP> list);
        bool actualizar(List<CxP> list);
    
    }

} 
