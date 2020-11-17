using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.Ccl.GestionCompras {
    public interface ICompraDetalleGastoPdn {
        bool CxpEstaSiendoUsadaEnOtroCompra(int valConsecutivoCompania, int valConsecutivoCxP, int valConsecutivoCompra);
    }
}
