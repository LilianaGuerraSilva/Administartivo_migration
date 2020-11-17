using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
namespace Galac.Adm.Ccl.GestionCompras {

    public interface IProveedorPdn : ILibPdn {
        string ValidaRifWeb(string valRif);
        Proveedor GetProveedor(int ConsecutivoCompania, string CodigoProveedor);
    }
}
