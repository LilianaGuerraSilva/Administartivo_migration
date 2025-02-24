using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.UI.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.Uil.Venta {
    public class VentaMessagesHandler {
        static VentaMessagesHandler _Default = new VentaMessagesHandler();

        public static void RegisterMessages() {
            LibBusinessProcess.Register(_Default, "MensajeHomologacionCaja", EjecutarMensajeHomologacionCaja);

        }

        private static void EjecutarMensajeHomologacionCaja(LibBusinessProcessMessage valMessage) {
            var vMessage = LibString.Split(valMessage.Content.ToString(), "|");
            string valContent = vMessage[0];
            string valCaption = vMessage[1];
            LibMessages.MessageBox.Information(null, valContent, valCaption);
        }
    }
}
