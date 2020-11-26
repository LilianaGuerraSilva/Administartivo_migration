using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;

namespace Galac.Adm.Ccl.Venta {
    public interface ICXCPdn : ILibPdn {
        bool Insert(int vConsecutivoCompania, XElement vData);
        void CambiarStatusDeCxcACancelada(int valConsecutivoCompania, string valNumeroDeFactura, int valTipoDeDocumento, decimal valMontoAbonado);
    }
}
