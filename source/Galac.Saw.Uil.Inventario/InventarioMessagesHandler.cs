using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Uil.Inventario.ViewModel;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static LibGalac.Aos.Base.LibMessagesBase;

namespace Galac.Saw.Uil.Inventario {
    public class InventarioMessagesHandler {
        static InventarioMessagesHandler _Default = new InventarioMessagesHandler();

        public static void RegisterMessages() {
            LibBusinessProcess.Register(_Default, "InsertarLoteInventarioDesdeModuloExterno", EjecutarInsertarLoteInventarioDesdeModuloExterno);

        }

        private static void EjecutarInsertarLoteInventarioDesdeModuloExterno(LibBusinessProcessMessage valMessage) {
            var vMessage = LibString.Split(valMessage.Content.ToString(), "|");
            string valCodigoLote = vMessage[0];
            string CodigoArticulo = vMessage[1];
            eTipoArticuloInv TipoArticuloInv = (eTipoArticuloInv)LibConvert.ToInt(vMessage[2]);
            new LoteDeInventarioMngViewModel().ExecuteCreateCommandEspecial(ref valCodigoLote, CodigoArticulo, TipoArticuloInv);
            valMessage.Result = valCodigoLote;
        }
    }
}
