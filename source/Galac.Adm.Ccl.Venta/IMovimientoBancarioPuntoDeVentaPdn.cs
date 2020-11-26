using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.SttDef;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Ccl.Venta {
    public interface IMovimientoBancarioPuntoDeVentaPdn {
        int GenerarProximoConsecutivoMovimiento(int valConsecutivoCompania);
        bool ActualizarSaldoCuentaBancariaPuntoDeVenta(int consecutivoCompania,XElement vData);
        bool Insert(int valConsecutivoCompania,int valConsecutivoMovimiento,string valNumeroCobranza,XElement valData);
        bool Insert(List<MovimientoBancario> list);
    }
}