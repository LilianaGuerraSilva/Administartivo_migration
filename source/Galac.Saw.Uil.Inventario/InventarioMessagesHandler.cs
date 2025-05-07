using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Uil.Inventario.ViewModel;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.UI.Mvvm.Messaging;
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
            LibBusinessProcess.Register(_Default, "MenjajeSobregiroConExistenciaNegativa", EjecutarMenjajeSobregiroConExistenciaNegativa);

        }

        private static void EjecutarInsertarLoteInventarioDesdeModuloExterno(LibBusinessProcessMessage valMessage) {
            var vMessage = LibString.Split(valMessage.Content.ToString(), "|");
            string valCodigoLote = vMessage[0];
            string CodigoArticulo = vMessage[1];
            eTipoArticuloInv TipoArticuloInv = (eTipoArticuloInv)LibConvert.ToInt(vMessage[2]);
            new LoteDeInventarioMngViewModel().ExecuteCreateCommandEspecial(ref valCodigoLote, CodigoArticulo, TipoArticuloInv);
            valMessage.Result = valCodigoLote;
        }

        private static void EjecutarMenjajeSobregiroConExistenciaNegativa(LibBusinessProcessMessage valMessage) {            
            string valCodigosArticulo = valMessage.Content.ToString();
            valMessage.Result = LibMessages.MessageBox.YesNo(null, $"El resultado de la operación actual generará una existencia negativa para el articulo: {valCodigosArticulo}. ¿Desea continuar con el proceso? ", "Nota de Entrada/Salida");
        }
    }
}
