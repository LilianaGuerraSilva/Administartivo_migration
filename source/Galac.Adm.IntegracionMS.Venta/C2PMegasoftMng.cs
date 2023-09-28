using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.IntegracionMS.Venta {
    public class C2PMegasoftMng : IC2PMegaSoftMng {
        string _NumeroControlVueltoPagoMovil;
        string IC2PMegaSoftMng.NumeroControlVueltoPagoMovil { get { return _NumeroControlVueltoPagoMovil; } }

        decimal _MontoVueltoPagoMovil;
        decimal IC2PMegaSoftMng.MontoVueltoPagoMovil { get { return _MontoVueltoPagoMovil; } }

        void IC2PMegaSoftMng.EjecutaVueltoPagoMovil(string valNombreCliente, string valNroFactura, decimal valMontoRestantePorPagar) {
            C2PMegasoftViewModel vViewModel = new C2PMegasoftViewModel(valNombreCliente, valNroFactura, valMontoRestantePorPagar);//TODO:Se pasa código mientras tanto, va el nombre del cliente que aún no se recibe acá para pasarlo a la siguiente view
            LibMessages.EditViewModel.ShowEditor(vViewModel, true);
            _NumeroControlVueltoPagoMovil = vViewModel.NumeroControl;
            _MontoVueltoPagoMovil = LibConvert.ToDec(vViewModel.Vuelto);
        }
    }
}