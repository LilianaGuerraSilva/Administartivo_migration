using Galac.Adm.Ccl.GestionProduccion;
using Galac.Adm.Uil.GestionProduccion.ViewModel;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.UI.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galac.Adm.Uil.GestionProduccion {
    public class OrdenDeProduccionMessagesHandler {
        static OrdenDeProduccionMessagesHandler _Default = new OrdenDeProduccionMessagesHandler();

        public static void RegisterMessages() {
            LibBusinessProcess.Register(_Default, "ConsultarOrdenDeProduccion", EjecutarConsultarOrdenDeProduccion);

        }

        private static void EjecutarConsultarOrdenDeProduccion(LibBusinessProcessMessage valMessage) {
            int _Consecutivo = (int)valMessage.Content;
            OrdenDeProduccionViewModel vViewModel = new OrdenDeProduccionViewModel(new OrdenDeProduccion() { ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), Consecutivo = _Consecutivo }, eAccionSR.Consultar);
            vViewModel.InitializeViewModel(eAccionSR.Consultar);
            LibMessages.EditViewModel.ShowEditor(vViewModel, true);
        }
    }
}
