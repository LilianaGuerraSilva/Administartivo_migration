using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Banco {
    public interface IConceptoBancarioPdn: ILibPdn {
        object ConsultaCampoConceptoBancario(string valCampo, eConceptoBancarioPorDefecto valEnumConcepto); 
        System.Xml.Linq.XElement LisConceptosBancariosPorDefecto();
    }
}
