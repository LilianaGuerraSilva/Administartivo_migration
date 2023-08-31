using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.IntegracionMS.Venta {
    public interface IC2PMegaSoftMng {
        string NumeroControlVueltoPagoMovil { get; }
        decimal MontoVueltoPagoMovil { get; }
        void EjecutaVueltoPagoMovil(string valNombreCliente, string valNroFactura, decimal valMontoRestantePorPagar);
    }
}
