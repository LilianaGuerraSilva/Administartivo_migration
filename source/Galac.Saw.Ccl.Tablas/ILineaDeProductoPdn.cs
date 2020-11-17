using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;


namespace Galac.Saw.Ccl.Tablas {

    public interface ILineaDeProductoPdn:ILibPdn {

        XElement FindByConsecutivoCompaniaNombre(int valConsecutivoCompania, string valNombre);
        void InsertaPrimeraLineaDeProducto(int valConsecutivoCompania);
        string GetNextConsecutivoLineaDeProducto(int valConsecutivoCompania);
    }
}

